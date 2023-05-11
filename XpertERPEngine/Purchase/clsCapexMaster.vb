Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsCapexMaster

#Region "Variables"
    Public Code As String
    Public Description As String
    Public Budget As Decimal
    Public RevBudget As Decimal
    Public Tolerence As Decimal
    Public RevNo As String
    Public IncBudget As Decimal
    Public CurrentBudget As Decimal
    Public DocDate As DateTime
    Public Provisional As Boolean
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_CAPEX_MASTER.CODE as [Code],TSPL_CAPEX_MASTER.DESCRIPTION as [DESCRIPTION],TSPL_CAPEX_MASTER.Created_By as [Created By],TSPL_CAPEX_MASTER.Created_Date as [Created Date],TSPL_CAPEX_MASTER.Modified_By as [Modify By],TSPL_CAPEX_MASTER.Modified_Date as [Modify Date]  from TSPL_CAPEX_MASTER"
        str = clsCommon.ShowSelectForm("STMTRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function GetName(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select TSPL_CAPEX_MASTER.DESCRIPTION from TSPL_CAPEX_MASTER where TSPL_CAPEX_MASTER.Code='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCapexMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_CAPEX_MASTER where CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

            Return True
        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCapexMaster
        Dim obj As clsCapexMaster = Nothing
        Dim qry As String = "select TSPL_CAPEX_MASTER.CODE, TSPL_CAPEX_MASTER.DESCRIPTION,TSPL_CAPEX_MASTER.Budget as [Budget],TSPL_CAPEX_MASTER.Revised_Budget  as [Revised Budget],TSPL_CAPEX_MASTER.Tolerence as [Tolerence],TSPL_CAPEX_MASTER.Revision_No as [Revision No],TSPL_CAPEX_MASTER.Inc_Budget as [Inc Budget],TSPL_CAPEX_MASTER.Current_Budget,TSPL_CAPEX_MASTER.Doc_Date,TSPL_CAPEX_MASTER.Provisional FROM TSPL_CAPEX_MASTER  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CAPEX_MASTER.CODE = (select MIN(TSPL_CAPEX_MASTER.CODE) from TSPL_CAPEX_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_CAPEX_MASTER.CODE = (select Max(TSPL_CAPEX_MASTER.CODE) from TSPL_CAPEX_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_CAPEX_MASTER.CODE = (select Min(TSPL_CAPEX_MASTER.CODE) from TSPL_CAPEX_MASTER where  TSPL_CAPEX_MASTER.CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_CAPEX_MASTER.CODE = (select Max(TSPL_CAPEX_MASTER.CODE) from TSPL_CAPEX_MASTER where TSPL_CAPEX_MASTER.CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_CAPEX_MASTER.CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCapexMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.RevBudget = clsCommon.myCdbl(dt.Rows(0)("Revised Budget"))
            obj.Tolerence = clsCommon.myCdbl(dt.Rows(0)("Tolerence"))
            obj.RevNo = clsCommon.myCstr(dt.Rows(0)("Revision No"))
            obj.Budget = clsCommon.myCdbl(dt.Rows(0)("Budget"))
            obj.IncBudget = clsCommon.myCdbl(dt.Rows(0)("Inc Budget"))
            obj.Provisional = (clsCommon.myCdbl(dt.Rows(0)("Provisional")) = 1)
            obj.CurrentBudget = clsCommon.myCdbl(dt.Rows(0)("Current_Budget"))
            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Doc_Date"))) > 0 Then
                obj.DocDate = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
            End If
        End If
        Return obj
    End Function
    
    Public Shared Function SaveData(ByVal obj As clsCapexMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                isSaved = True
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsCapexMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim rbudget As String = Nothing
        Dim revno As String = Nothing
        Try

            Dim coll As New Hashtable()
            RevNo = "0"
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Budget", obj.Budget)
            clsCommon.AddColumnsForChange(coll, "Revised_Budget", obj.RevBudget)
            clsCommon.AddColumnsForChange(coll, "Tolerence", obj.Tolerence)
            clsCommon.AddColumnsForChange(coll, "Inc_Budget", clsCommon.myCstr(obj.IncBudget))
            clsCommon.AddColumnsForChange(coll, "Revision_No", clsCommon.myCstr(obj.RevNo))
            clsCommon.AddColumnsForChange(coll, "Current_Budget", clsCommon.myCstr(obj.CurrentBudget))
            clsCommon.AddColumnsForChange(coll, "Provisional", IIf(obj.Provisional, 1, 0))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.DocDate, clsDocType.CAPEXMASTER, "", "")
                clsCommon.AddColumnsForChange(coll, "CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.DocDate, "dd/MMM/yyyy hh:mm tt"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_CAPEX_MASTER where TSPL_CAPEX_MASTER.CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPEX_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Code), "TSPL_CAPEX_MASTER", "CODE", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CAPEX_MASTER", OMInsertOrUpdate.Update, "TSPL_CAPEX_MASTER.CODE='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso chkLimitBugetMaster(obj.Code, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_CAPEX_MASTER.CODE from TSPL_CAPEX_MASTER where TSPL_CAPEX_MASTER.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function chkSubBudget(ByVal CapexCode As String, ByVal trans As SqlTransaction) As Decimal
        Dim SubBudgetAmt As Decimal = 0

        SubBudgetAmt = clsDBFuncationality.getSingleValue("select sum(Budget)-sum(SubCapexAmount) as Budget  from (select ((case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end)*Tolerence/100+(case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end)) as Budget ,code,0 as SubCapexAmount from tspl_capex_master where code='" & CapexCode & "' union all" & _
                      " SELECT 0 as CapexAmount,Capex_Code,isnull(SUM(Budget),0) AS Budget FROM (SELECT ((Budget*Tolerence) /100)+Budget AS Budget,Capex_Code FROM (select case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end as Budget,Tolerence,Capex_Code from TSPL_CAPEX_BUDGET_MASTER where Capex_Code ='" & CapexCode & "' )AS XX )  AS XXX group by Capex_Code) as YY group by CODE", trans)
        Return clsCommon.myCdbl(SubBudgetAmt)
    End Function
    Public Shared Function chkMainBuget(ByVal CapexCode As String, ByVal trans As SqlTransaction) As Decimal
        Dim BudgetAmt As Decimal = 0

        BudgetAmt = clsDBFuncationality.getSingleValue("select isnull(((case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end)*Tolerence/100+(case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end)),0) as Budget  from tspl_capex_master where code='" & CapexCode & "'", trans)
        Return clsCommon.myCdbl(BudgetAmt)
    End Function
    Public Shared Function chkLimitBugetMaster(ByVal CapexCode As String, ByVal trans As SqlTransaction) As Boolean '', ByVal Buget As Decimal, ByVal Tolerence As Decimal, ByVal SubCapexCode As String

        If clsCommon.myLen(CapexCode) <= 0 Then
            Return True
        End If
        Dim SubBudgetAmt As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT SUM(Budget) AS Budget FROM (SELECT ((Budget*Tolerence) /100)+Budget AS Budget FROM (select case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end as Budget,Tolerence from TSPL_CAPEX_BUDGET_MASTER where Capex_Code ='" & CapexCode & "' )AS XX)AS XXX", trans))
        Dim BudgetAmt As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select ((case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end)*Tolerence/100+(case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end)) as Budget  from tspl_capex_master where code='" & CapexCode & "'", trans))

        If SubBudgetAmt <= BudgetAmt Then
            Return True
        Else
            'clsCommon.MyMessageBoxShow("Capex Sub Buget with tolerence is " & strLimitSubBuget & " is less then " & strLimitBuget & "  ")
            Throw New Exception("Total Sub Capex Budget amount (" & SubBudgetAmt & ") cannot exceed Main Capex Budget amount (" & BudgetAmt & ")  ")
        End If
        Return True
        'Dim strLimitBuget As Decimal = 0
        'Dim strLimitBugetTol As Decimal = 0
        'Dim strLimitSubBuget As Decimal = 0
        'Dim strLimitSubBugetTol As Decimal = 0
        'Dim strLimitBugetwithTol As Decimal = 0
        'Dim strLimitSubBugetWithtol As Decimal = 0
        'Dim revno As Decimal = 0
        'Dim Strcurrentbugetwithtol As Decimal = 0
        'If clsCommon.myLen(CapexCode) > 0 Then
        '    strLimitBuget = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when sum(isnull(Revised_Budget,0))=0 then sum(isnull(Budget,0)) else sum(isnull(Revised_Budget,0)) end as Budget  from tspl_capex_master where code='" & CapexCode & "'"))
        '    strLimitBugetTol = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (isnull(Tolerence,0)) as Tolerence   from tspl_capex_master where code='" & CapexCode & "'"))
        '    If clsCommon.myLen(SubCapexCode) > 0 Then
        '        strLimitSubBuget = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT SUM(Budget) AS Budget FROM (SELECT ((Budget*Tolerence) /100)+Budget AS Budget FROM (select case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end as Budget,Tolerence from TSPL_CAPEX_BUDGET_MASTER where Capex_Code ='" & CapexCode & "' and code not in ('" & SubCapexCode & "')) AS XX)AS XXX"))
        '    Else
        '        strLimitSubBuget = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT SUM(Budget) AS Budget FROM (SELECT ((Budget*Tolerence) /100)+Budget AS Budget FROM (select case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end as Budget,Tolerence from TSPL_CAPEX_BUDGET_MASTER where Capex_Code ='" & CapexCode & "' )AS XX)AS XXX"))
        '    End If


        '    'If clsCommon.myCdbl(strLimitBugetTol) > 0 Then
        '    strLimitBugetwithTol = (strLimitBuget / 100) * strLimitBugetTol
        '    strLimitBuget = strLimitBuget + strLimitBugetwithTol

        '    Strcurrentbugetwithtol = Buget + ((Buget * Tolerence) / 100)
        '    strLimitSubBugetWithtol = strLimitSubBuget '+ ((strLimitSubBuget * Tolerence) / 100)
        '    'strLimitSubBuget = strLimitSubBugetWithtol

        '    'End If

        '    If clsCommon.myCdbl(strLimitSubBuget) <= clsCommon.myCdbl(strLimitBuget) Then
        '        Return True
        '    Else
        '        'clsCommon.MyMessageBoxShow("Capex Sub Buget with tolerence is " & strLimitSubBuget & " is less then " & strLimitBuget & "  ")
        '        Throw New Exception("Total Sub Capex Budget " & strLimitSubBuget & " cannot exceed Main Capex Budget  " & strLimitBuget & "  ")
        '        Return False
        '    End If

        'End If

        'Return True
    End Function
    Public Shared Function ChkAcquisitionEntry(ByVal StrCapexCode As String) As Boolean
        Dim Count As Decimal = 0

        Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as net_Amt from TSPL_ACQUISITION_DETAIL" & _
                             " left join TSPL_ACQUISITION_head on TSPL_ACQUISITION_DETAIL.Acquisition_Code =TSPL_ACQUISITION_head.Acquisition_Code" & _
                             " where TSPL_ACQUISITION_DETAIL.capex_code='" & StrCapexCode & "' and TSPL_ACQUISITION_head.status=1"))
        If Count > 0 Then
            Return False
        Else
            Return True
        End If
        Return True
    End Function
End Class

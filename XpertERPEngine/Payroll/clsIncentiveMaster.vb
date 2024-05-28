Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsIncentiveMaster

#Region "Variables"
    Public INCENTIVE_CODE As String
    Public DESCRIPTION As String
    Public INCENTIVE_TYPE As String
    Public INCENTIVE_DATE As Date
    Public START_DATE As Date
    Public END_DATE As Date? = Nothing
    Public CREATED_BY As String
    Public Comp_Code As String
    Public SCHEME_FOR As String
    Public Calc_Type As String
    Public Rate_type As String
    Public Starting_Shift As String
    Public Ending_Shift As String
    Public Qty_Type As String = "ACTQ"
    Public Shared ObjList As List(Of clsIncentiveMasterDetail)
    
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsIncentiveMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function CheckValidCode(ByVal Doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select count(*) from TSPL_INCENTIVE_MASTER_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and INCENTIVE_CODE='" + Doc_No + "'"
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_INCENTIVE_DETAIL where INCENTIVE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INCENTIVE_MASTER_HEAD where INCENTIVE_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsIncentiveMaster
        Dim obj As New clsIncentiveMaster()
        Dim objtr As New clsIncentiveMaster()

        ObjList = New List(Of clsIncentiveMasterDetail)

        Dim qry As String = " SELECT TSPL_INCENTIVE_MASTER_HEAD.Incentive_code,TSPL_INCENTIVE_MASTER_HEAD.Description,TSPL_INCENTIVE_MASTER_HEAD.Incentive_Date," & _
                            " TSPL_INCENTIVE_MASTER_HEAD.Start_Date,TSPL_INCENTIVE_MASTER_HEAD.End_Date,TSPL_INCENTIVE_MASTER_HEAD.Incentive_Type," & _
                            " TSPL_INCENTIVE_MASTER_HEAD.Created_By,TSPL_INCENTIVE_MASTER_HEAD.Comp_Code,TSPL_INCENTIVE_MASTER_HEAD.SCHEME_FOR,TSPL_INCENTIVE_MASTER_HEAD.Calc_Type,TSPL_INCENTIVE_MASTER_HEAD.Rate_type,TSPL_INCENTIVE_MASTER_HEAD.Starting_Shift,TSPL_INCENTIVE_MASTER_HEAD.Ending_Shift,TSPL_INCENTIVE_MASTER_HEAD.Qty_Type FROM TSPL_INCENTIVE_MASTER_HEAD  WHERE 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " AND INCENTIVE_CODE = (select MIN(INCENTIVE_CODE) from TSPL_INCENTIVE_MASTER_HEAD)"
            Case NavigatorType.Last
                qry += " AND INCENTIVE_CODE = (select Max(INCENTIVE_CODE) from TSPL_INCENTIVE_MASTER_HEAD)"
            Case NavigatorType.Next
                qry += " AND INCENTIVE_CODE = (select Min(INCENTIVE_CODE) from TSPL_INCENTIVE_MASTER_HEAD where  INCENTIVE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND INCENTIVE_CODE = (select Max(INCENTIVE_CODE) from TSPL_INCENTIVE_MASTER_HEAD where INCENTIVE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND INCENTIVE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.INCENTIVE_CODE = dt.Rows(0)("INCENTIVE_CODE")
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.INCENTIVE_TYPE = clsCommon.myCstr(dt.Rows(0)("INCENTIVE_TYPE"))
            obj.INCENTIVE_DATE = clsCommon.GetPrintDate(dt.Rows(0)("INCENTIVE_DATE"), "dd/MMM/yyyy")
            
            obj.START_DATE = clsCommon.GetPrintDate(dt.Rows(0)("START_DATE"), "dd/MMM/yyyy")
            obj.END_DATE = clsCommon.GetPrintDate(dt.Rows(0)("END_DATE"), "dd/MMM/yyyy")
            
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.SCHEME_FOR = clsCommon.myCstr(dt.Rows(0)("SCHEME_FOR"))
            obj.Calc_Type = clsCommon.myCstr(dt.Rows(0)("Calc_Type"))
            obj.Rate_type = clsCommon.myCstr(dt.Rows(0)("Rate_Type"))
            obj.Starting_Shift = clsCommon.myCstr(dt.Rows(0)("Starting_Shift"))
            obj.Ending_Shift = clsCommon.myCstr(dt.Rows(0)("Ending_Shift"))

            obj.Qty_Type = clsCommon.myCstr(dt.Rows(0)("Qty_Type"))
            strCode = dt.Rows(0)("INCENTIVE_CODE")
        End If
       
        clsIncentiveMaster.ObjList = clsIncentiveMasterDetail.GetIncentiveDetail(obj, trans)
        Return obj
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select INCENTIVE_CODE as Code,DESCRIPTION as Description,case when INCENTIVE_TYPE='QB' then 'Quantity Based' when INCENTIVE_TYPE='QSLAB' then 'SLAB Based' when INCENTIVE_TYPE='TSB' then 'TS Based' when INCENTIVE_TYPE='QTSSLAB' then 'Quantity and TS Based' when INCENTIVE_TYPE='QSLABTSSLAB' then 'SLAB and TS Based' end as [Incentive Type],INCENTIVE_DATE as [Incentive Date]," & _
                            " START_DATE as [Start Date],END_DATE as [End Date],TSPL_INCENTIVE_MASTER_HEAD.SCHEME_FOR AS [Scheme For] from TSPL_INCENTIVE_MASTER_HEAD "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_INCENTIVE_MASTER_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_INCENTIVE_MASTER_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("PP", qry, "Code", whrCls, currCode, "Code", isButtonClicked)
        Return str
    End Function
   

    Public Function SaveData(ByVal obj As clsIncentiveMaster, ByVal objList As List(Of clsIncentiveMasterDetail), ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                If clsCommon.myLen(obj.INCENTIVE_CODE) = 0 Then
                    obj.INCENTIVE_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.INCENTIVE_DATE, "dd/MMM/yyyy"), clsDocType.IncentiveMaster, "", "")
                End If
            End If

            Dim qry As String = "DELETE FROM TSPL_INCENTIVE_DETAIL WHERE INCENTIVE_CODE='" + obj.INCENTIVE_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.INCENTIVE_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            Else
                strDocNo = obj.INCENTIVE_CODE
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", obj.INCENTIVE_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "INCENTIVE_TYPE", obj.INCENTIVE_TYPE)
            clsCommon.AddColumnsForChange(coll, "SCHEME_FOR", obj.SCHEME_FOR)
            clsCommon.AddColumnsForChange(coll, "Calc_Type", obj.Calc_Type)
            clsCommon.AddColumnsForChange(coll, "Rate_type", obj.Rate_type)
            clsCommon.AddColumnsForChange(coll, "Starting_Shift", obj.Starting_Shift)
            clsCommon.AddColumnsForChange(coll, "Ending_Shift", obj.Ending_Shift)

            clsCommon.AddColumnsForChange(coll, "INCENTIVE_DATE", clsCommon.GetPrintDate(obj.INCENTIVE_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "START_DATE", clsCommon.GetPrintDate(obj.START_DATE, "dd/MMM/yyyy"))
            '' ADDED BY PANCH RAJ ON 25/09/2016
            clsCommon.AddColumnsForChange(coll, "Qty_Type", obj.Qty_Type)

            If Not obj.END_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "END_DATE", clsCommon.GetPrintDate(obj.END_DATE, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_INCENTIVE_MASTER_HEAD where INCENTIVE_CODE = '" & obj.INCENTIVE_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCENTIVE_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.INCENTIVE_CODE + " Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCENTIVE_MASTER_HEAD", OMInsertOrUpdate.Update, "TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE='" + obj.INCENTIVE_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsIncentiveMasterDetail.SaveData(obj.INCENTIVE_CODE, objList, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

End Class


Public Class clsIncentiveMasterDetail
#Region "Variables"
    '' grid columns

    Public INCENTIVE_CODE As String
    Public INCENTIVE_TYPE As String
    Public LINE_NO As Integer
    Public SLAB_FROM As Decimal
    Public SLAB_TO As Decimal
    Public TS_FROM As Decimal
    Public TS_TO As Decimal

    Public FAT_FROM As Decimal
    Public FAT_TO As Decimal
    Public SNF_FROM As Decimal
    Public SNF_TO As Decimal
    Public Type As String = ""

    Public RATE As Decimal
    Public RATE_UOM As String
    Public FOR_PERIOD As String
    Public Parameter_Type As String = ""
    Public OPERATER_TYPE As String = ""


#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsIncentiveMasterDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsIncentiveMasterDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "INCENTIVE_TYPE", obj.INCENTIVE_TYPE)
                    clsCommon.AddColumnsForChange(coll, "FOR_PERIOD", obj.FOR_PERIOD)
                    clsCommon.AddColumnsForChange(coll, "RATE", obj.RATE)
                    clsCommon.AddColumnsForChange(coll, "RATE_UOM", obj.RATE_UOM)
                    clsCommon.AddColumnsForChange(coll, "SLAB_FROM", obj.SLAB_FROM)
                    clsCommon.AddColumnsForChange(coll, "SLAB_TO", obj.SLAB_TO)
                    clsCommon.AddColumnsForChange(coll, "TS_FROM", obj.TS_FROM)
                    clsCommon.AddColumnsForChange(coll, "TS_TO", obj.TS_TO)

                    clsCommon.AddColumnsForChange(coll, "FAT_FROM", obj.FAT_FROM)
                    clsCommon.AddColumnsForChange(coll, "FAT_TO", obj.FAT_TO)
                    clsCommon.AddColumnsForChange(coll, "SNF_FROM", obj.SNF_FROM)
                    clsCommon.AddColumnsForChange(coll, "SNF_TO", obj.SNF_TO)
                    clsCommon.AddColumnsForChange(coll, "Type", obj.Type)


                    clsCommon.AddColumnsForChange(coll, "Parameter_Type", obj.Parameter_Type)
                    clsCommon.AddColumnsForChange(coll, "OPERATER_TYPE", obj.OPERATER_TYPE)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCENTIVE_DETAIL", OMInsertOrUpdate.Insert, "TSPL_INCENTIVE_DETAIL.INCENTIVE_CODE='" + strDocNo + "'", trans)
                Next

            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

        Return True
    End Function
    Public Shared Function GetIncentiveDetail(ByVal obj As clsIncentiveMaster, ByVal trans As SqlTransaction) As List(Of clsIncentiveMasterDetail)
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_INCENTIVE_DETAIL  WHERE INCENTIVE_CODE='" & obj.INCENTIVE_CODE & "' ORDER BY LINE_NO"
        Dim DT As DataTable
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As New clsIncentiveMasterDetail
        Dim ObjList As New List(Of clsIncentiveMasterDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsIncentiveMasterDetail

                objtr.Line_No = clsCommon.myCdbl(dr("LINE_NO"))
                objtr.FOR_PERIOD = clsCommon.myCstr(dr("FOR_PERIOD"))
                objtr.INCENTIVE_CODE = clsCommon.myCstr(dr("INCENTIVE_CODE"))
                objtr.INCENTIVE_TYPE = clsCommon.myCstr(dr("INCENTIVE_TYPE"))
                objtr.RATE = clsCommon.myCdbl(dr("RATE"))
                objtr.RATE_UOM = clsCommon.myCstr(dr("RATE_UOM"))
                objtr.SLAB_FROM = clsCommon.myCdbl(dr("SLAB_FROM"))
                objtr.SLAB_TO = clsCommon.myCdbl(dr("SLAB_TO"))
                objtr.TS_FROM = clsCommon.myCdbl(dr("TS_FROM"))
                objtr.TS_TO = clsCommon.myCdbl(dr("TS_TO"))

                objtr.FAT_FROM = clsCommon.myCdbl(dr("FAT_FROM"))
                objtr.FAT_TO = clsCommon.myCdbl(dr("FAT_TO"))
                objtr.SNF_FROM = clsCommon.myCdbl(dr("SNF_FROM"))
                objtr.SNF_TO = clsCommon.myCdbl(dr("SNF_TO"))
                objtr.Type = clsCommon.myCstr(dr("Type"))

                objtr.Parameter_Type = clsCommon.myCstr(dr("Parameter_Type"))
                objtr.OPERATER_TYPE = clsCommon.myCstr(dr("OPERATER_TYPE"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

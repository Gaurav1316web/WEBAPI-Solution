Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsPayHeadDefinitions

#Region "Variables"
    Public PAY_HEAD_CODE As String
    Public PAY_HEAD_NAME As String
    Public PRINT_NAME As String
    Public HEAD_TYPE As String
    Public SUB_HEAD_TYPE As String
    Public PERIODICITY As String
    Public CALC_BASIS As String
    Public ROUND_OFF_TYPE As String
    Public ISEARNING As Boolean
    Public IsHiddenComponent As Boolean
    Public Account_Code As String
    Public Description As String
    Public GROUP_SEQ As Integer
    Public PRINT_GROUP_SEQ As Integer
    Public ARREAR_TYPE As String
    Public GL_Employer_Account As String
    Public IsFullnFinal As Boolean
    Public IsTDSExempted As Boolean
    Public IsCreateAPInvoice As Boolean = False
    Public Do_Not_Include_In_Gross_Salary_For_Over_Time As Boolean
    Public Deduction_Code As String = String.Empty
    Public MaximumHRA As Decimal = 0.0
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPayHeadDefinitions
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
            qry = "delete from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPayHeadDefinitions
        Dim obj As clsPayHeadDefinitions = Nothing
        Dim qry As String = "select TSPL_PAYHEAD_MASTER.*,TSPL_GL_ACCOUNTS.description from TSPL_PAYHEAD_MASTER  " & _
        " left join TSPL_GL_ACCOUNTS on TSPL_PAYHEAD_MASTER.Account_Code=TSPL_GL_ACCOUNTS.Account_Code  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and PAY_HEAD_CODE = (select MIN(PAY_HEAD_CODE) from TSPL_PAYHEAD_MASTER)"
            Case NavigatorType.Last
                qry += " and PAY_HEAD_CODE = (select Max(PAY_HEAD_CODE) from TSPL_PAYHEAD_MASTER)"
            Case NavigatorType.Next
                qry += " and PAY_HEAD_CODE = (select Min(PAY_HEAD_CODE) from TSPL_PAYHEAD_MASTER where  PAY_HEAD_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and PAY_HEAD_CODE = (select Max(PAY_HEAD_CODE) from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and PAY_HEAD_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPayHeadDefinitions()
            obj.PAY_HEAD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_HEAD_CODE"))
            obj.PAY_HEAD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_HEAD_NAME"))
            obj.PRINT_NAME = clsCommon.myCstr(dt.Rows(0)("PRINT_NAME"))
            obj.HEAD_TYPE = clsCommon.myCstr(dt.Rows(0)("HEAD_TYPE"))
            obj.SUB_HEAD_TYPE = clsCommon.myCstr(dt.Rows(0)("SUB_HEAD_TYPE"))
            obj.PERIODICITY = clsCommon.myCstr(dt.Rows(0)("PERIODICITY"))
            obj.CALC_BASIS = clsCommon.myCstr(dt.Rows(0)("CALC_BASIS"))
            obj.ROUND_OFF_TYPE = clsCommon.myCstr(dt.Rows(0)("ROUND_OFF_TYPE"))
            obj.ISEARNING = clsCommon.myCBool(dt.Rows(0)("ISEARNING"))
            obj.IsHiddenComponent = clsCommon.myCBool(dt.Rows(0)("IsHiddenComponent"))
            obj.Account_Code = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.ARREAR_TYPE = clsCommon.myCstr(dt.Rows(0)("ARREAR_TYPE"))
            obj.GROUP_SEQ = clsCommon.myCstr(dt.Rows(0)("GROUP_SEQ"))
            obj.PRINT_GROUP_SEQ = clsCommon.myCstr(dt.Rows(0)("PRINT_GROUP_SEQ"))
            obj.GL_Employer_Account = clsCommon.myCstr(dt.Rows(0)("GL_Employer_Account"))
            obj.IsFullnFinal = clsCommon.myCBool(dt.Rows(0)("IsFullnFinal"))
            obj.IsTDSExempted = clsCommon.myCBool(dt.Rows(0)("IsTDSExempted"))
            obj.IsCreateAPInvoice = clsCommon.myCBool(dt.Rows(0)("IsCreateAPInvoice"))
            obj.Deduction_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
            obj.Do_Not_Include_In_Gross_Salary_For_Over_Time = (clsCommon.myCdbl(dt.Rows(0)("Do_Not_Include_In_Gross_Salary_For_Over_Time")) = 1)
            obj.MaximumHRA = clsCommon.myCdbl(dt.Rows(0)("MaximumHRA"))
        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsPayHeadDefinitions, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PAY_HEAD_NAME", obj.PAY_HEAD_NAME)
            clsCommon.AddColumnsForChange(coll, "PRINT_NAME", obj.PRINT_NAME)
            clsCommon.AddColumnsForChange(coll, "HEAD_TYPE", obj.HEAD_TYPE)
            clsCommon.AddColumnsForChange(coll, "ARREAR_TYPE", obj.ARREAR_TYPE, True)
            clsCommon.AddColumnsForChange(coll, "SUB_HEAD_TYPE", obj.SUB_HEAD_TYPE)
            clsCommon.AddColumnsForChange(coll, "PERIODICITY", obj.PERIODICITY)
            clsCommon.AddColumnsForChange(coll, "CALC_BASIS", obj.CALC_BASIS)
            clsCommon.AddColumnsForChange(coll, "ROUND_OFF_TYPE", obj.ROUND_OFF_TYPE)
            clsCommon.AddColumnsForChange(coll, "ISEARNING", obj.ISEARNING)
            clsCommon.AddColumnsForChange(coll, "IsHiddenComponent", obj.IsHiddenComponent)
            clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code, True)
            clsCommon.AddColumnsForChange(coll, "GROUP_SEQ", obj.GROUP_SEQ)
            clsCommon.AddColumnsForChange(coll, "PRINT_GROUP_SEQ", obj.PRINT_GROUP_SEQ)
            clsCommon.AddColumnsForChange(coll, "GL_Employer_Account", obj.GL_Employer_Account, True)
            clsCommon.AddColumnsForChange(coll, "IsFullnFinal", obj.IsFullnFinal)
            clsCommon.AddColumnsForChange(coll, "IsTDSExempted", obj.IsTDSExempted)
            clsCommon.AddColumnsForChange(coll, "IsCreateAPInvoice", obj.IsCreateAPInvoice)
            clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code, True)
            clsCommon.AddColumnsForChange(coll, "Do_Not_Include_In_Gross_Salary_For_Over_Time", IIf(obj.Do_Not_Include_In_Gross_Salary_For_Over_Time, 1, 0))
            clsCommon.AddColumnsForChange(coll, "MaximumHRA", obj.MaximumHRA)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If clsCommon.myLen(obj.PAY_HEAD_CODE) <= 0 Then
                    obj.PAY_HEAD_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.PayHeadDefinitions, "", "")
                    If clsCommon.myLen(obj.PAY_HEAD_CODE) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PAY_HEAD_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE= '" & obj.PAY_HEAD_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYHEAD_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYHEAD_MASTER", OMInsertOrUpdate.Update, "PAY_HEAD_CODE='" + obj.PAY_HEAD_CODE + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function FinderForPayHead(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsPayHeadDefinitions
        Return FinderForPayHead(strCode, isButtonClicked, "")
    End Function
    ''Type 0- all,1-deduction, 2-earning,
    Public Shared Function FinderForPayHead(ByVal strCode As String, ByVal isButtonClicked As Boolean, ByVal cond As String) As clsPayHeadDefinitions
        Dim obj As clsPayHeadDefinitions = Nothing
        Dim qry As String = " select PAY_HEAD_CODE as Code, PAY_HEAD_NAME as Name, PRINT_NAME as 'Print Name' ,HEAD_TYPE as 'Pay Head Type'," & _
        " SUB_HEAD_TYPE as 'Sub Pay Head Type', PERIODICITY as 'Periodicity', CALC_BASIS as 'Calculation Basis', ROUND_OFF_TYPE as 'Round Off Type', " & _
        " case when  IsHiddenComponent = 0 then 'N' else 'Y' end as IsHiddenComponent,TSPL_PAYHEAD_MASTER.Account_Code,TSPL_GL_ACCOUNTS.description from TSPL_PAYHEAD_MASTER  " & _
        " left join TSPL_GL_ACCOUNTS on TSPL_PAYHEAD_MASTER.Account_Code=TSPL_GL_ACCOUNTS.Account_Code "
        'Dim QryWhr = ""
        'If Type = 1 Then
        '    QryWhr = "ISEARNING=0 "
        'ElseIf Type = 2 Then
        '    QryWhr = "ISEARNING=1 "
        'End If

        strCode = clsCommon.ShowSelectForm("PAY_HEAD", qry, "Code", cond, strCode, "PAY_HEAD_CODE", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = clsPayHeadDefinitions.GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function
    Public Shared Function CheckNameExistness(ByVal strName As String, ByVal strExCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select PAY_HEAD_CODE from TSPL_PAYHEAD_MASTER where PAY_HEAD_NAME ='" + strName + "'  and PAY_HEAD_CODE <> '" + strExCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select PAY_HEAD_CODE from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetPayHeadList(Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsPayHeadDefinitions)
        Dim objTr As New clsPayHeadDefinitions
        Dim objList As New List(Of clsPayHeadDefinitions)
        Dim qry As String = "select * from TSPL_PAYHEAD_MASTER "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        For Each dr As DataRow In dt.Rows
            objTr = clsPayHeadDefinitions.GetData(dr.Item("Pay_Head_Code"), NavigatorType.Current, trans)
            objList.Add(objTr)
        Next
        Return objList
    End Function
    Public Shared Function GetPayHeadEarningDeductionType(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim qry As String = "select ISEARNING from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE ='" + Code + "'   "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

    End Function
    '' for viney
    Public Shared Function GetPayHeadEarningDeductionTypeUD(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim qry As String = "select ISEARNING from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE ='" + Code + "' and  HEAD_TYPE ='UD'  "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

    End Function
    Public Shared Function ValidatePayHeadSequence(ByVal Code As String, ByVal SeqNo As Integer, ByVal IsEarning As Integer, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Pay_Head_Code from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE <>'" & Code & "' and IsEarning='" & IsEarning & "'  and Group_Seq=" & SeqNo & " "
        Dim PH_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return PH_Code
    End Function
    Public Shared Function ValidatePayHeadPrintSequence(ByVal Code As String, ByVal PrintSeqNo As Integer, ByVal IsEarning As Integer, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Pay_Head_Code from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE <>'" & Code & "' and IsEarning='" & IsEarning & "'  and PRINT_GROUP_SEQ =" & PrintSeqNo & " "
        Dim PH_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return PH_Code
    End Function
    Public Shared Function CheckIncrementPayhead(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select ISEARNING ,SUB_HEAD_TYPE  from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE  ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("ISEARNING") = True Then
                If clsCommon.CompairString(dt.Rows(0).Item("SUB_HEAD_TYPE"), "Arrear") <> CompairStringResult.Equal Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If


    End Function
End Class

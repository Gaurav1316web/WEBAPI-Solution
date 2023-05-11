Imports common
Imports System.Data.SqlClient


Public Class clsBankBranchMaster
#Region "veriables"
    Public Branch_Code As String = Nothing
    Public Branch_Name As String = Nothing
    Public IFSCCode As String = Nothing
    Public BankCode As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Branch_Code as Code,Branch_Name as [Branch Name], IFSC_Code as [IFSC Code], Bank_Code as [Bank Code] from tspl_bank_branch_Master   "
        str = clsCommon.ShowSelectForm("BNKBRNCHMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    '' This Code For Retriving Data 

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBankBranchMaster
        Dim obj As clsBankBranchMaster = Nothing
        Dim qst As String = "select  * from tspl_bank_branch_master where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and  Branch_Code ='" & strCode & "' "
            Case NavigatorType.Next
                qst += " and  Branch_Code in (select min(t.Branch_Code ) from TSPL_BAnk_Branch_MASTER  as t where t.Branch_Code  >'" + strCode + "')"
            Case NavigatorType.First
                qst += " and Branch_Code in (select min(t.Branch_Code ) from TSPL_Bank_Branch_MASTER  as t )"
            Case NavigatorType.Last
                qst += " and  Branch_Code in (select max(t.Branch_Code ) from TSPL_Bank_Branch_MASTER  as t )"
            Case NavigatorType.Previous
                qst += " and  Branch_Code in (select max(t.Branch_Code ) from TSPL_Bank_Branch_MASTER  as t where t.Branch_Code  <'" + strCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBankBranchMaster
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Branch_Name = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
            obj.IFSCCode = clsCommon.myCstr(dt.Rows(0)("IFSC_Code"))
            obj.BankCode = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
        End If
        Return obj
    End Function
    '' For Delete of Data 
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = "Delete from TSPL_Bank_Branch_Master where Branch_Code='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
    '' For Save Data 
    Public Function SaveData(ByVal obj As clsBankBranchMaster, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
            clsCommon.AddColumnsForChange(coll, "Branch_Name", obj.Branch_Name)
            clsCommon.AddColumnsForChange(coll, "IFSC_Code", obj.IFSCCode)
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.BankCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            Dim qry As String = "SELECT Count(*) FROM TSPL_Bank_Branch_MASTER where Branch_Code = '" & obj.Branch_Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bank_Branch_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bank_Branch_MASTER ", OMInsertOrUpdate.Update, " Branch_Code='" + obj.Branch_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

End Class

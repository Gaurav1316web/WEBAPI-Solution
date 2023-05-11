Imports common
Imports System.Data.SqlClient
Public Class Accountsegment
#Region "Account Variable"
    Public Account_Seg_Code1 As String = Nothing
    Public Account_Seg_Code2 As String = Nothing
    Public Account_Seg_Code3 As String = Nothing
    Public Account_Seg_Code4 As String = Nothing
    Public Account_Seg_Code5 As String = Nothing
    Public Account_Seg_Code6 As String = Nothing
    Public Account_Seg_Code7 As String = Nothing
    Public Account_Seg_Code8 As String = Nothing
    Public Account_Seg_Code9 As String = Nothing
    Public Account_Seg_Code10 As String = Nothing
    Public Account_Seg_Desc1 As String = Nothing
    Public Account_Seg_Desc2 As String = Nothing
    Public Account_Seg_Desc3 As String = Nothing
    Public Account_Seg_Desc4 As String = Nothing
    Public Account_Seg_Desc5 As String = Nothing
    Public Account_Seg_Desc6 As String = Nothing
    Public Account_Seg_Desc7 As String = Nothing
    Public Account_Seg_Desc8 As String = Nothing
    Public Account_Seg_Desc9 As String = Nothing
    Public Account_Seg_Desc10 As String = Nothing
    Public Account_Group_Code As String = Nothing


#End Region
    Public Shared Function Getaccountcodedesc(ByVal Accountcode As String, ByVal trans As SqlTransaction) As Accountsegment
        Dim obj As Accountsegment = Nothing
        Dim qry As String = "SELECT Account_Seg_Code1 , Account_Seg_Desc1,Account_Seg_Code2 , Account_Seg_Desc2,Account_Seg_Code3 , Account_Seg_Desc3,Account_Seg_Code4 , Account_Seg_Desc4,Account_Seg_Code5 , Account_Seg_Desc5,Account_Seg_Code6 , Account_Seg_Desc6,Account_Seg_Code7 , Account_Seg_Desc7,Account_Seg_Code8 , Account_Seg_Desc8,Account_Seg_Code9 , Account_Seg_Desc9,Account_Seg_Code10 , Account_Seg_Desc10,Account_Group_Code  FROM TSPL_GL_ACCOUNTS WHERE Account_Code = '" + Accountcode + "'"
        Dim ds As New DataSet()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New Accountsegment()
            obj.Account_Seg_Code1 = Convert.ToString(dt.Rows(0)("Account_Seg_Code1"))
            obj.Account_Seg_Desc1 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc1"))
            obj.Account_Seg_Code2 = Convert.ToString(dt.Rows(0)("Account_Seg_Code2"))
            obj.Account_Seg_Desc2 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc2"))
            obj.Account_Seg_Code3 = Convert.ToString(dt.Rows(0)("Account_Seg_Code3"))
            obj.Account_Seg_Desc3 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc3"))
            obj.Account_Seg_Code4 = Convert.ToString(dt.Rows(0)("Account_Seg_Code4"))
            obj.Account_Seg_Desc4 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc4"))
            obj.Account_Seg_Code5 = Convert.ToString(dt.Rows(0)("Account_Seg_Code5"))
            obj.Account_Seg_Desc5 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc5"))
            obj.Account_Seg_Code6 = Convert.ToString(dt.Rows(0)("Account_Seg_Code6"))
            obj.Account_Seg_Desc6 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc6"))
            obj.Account_Seg_Code7 = Convert.ToString(dt.Rows(0)("Account_Seg_Code7"))
            obj.Account_Seg_Desc7 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc7"))
            obj.Account_Seg_Code8 = Convert.ToString(dt.Rows(0)("Account_Seg_Code8"))
            obj.Account_Seg_Desc8 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc8"))
            obj.Account_Seg_Code9 = Convert.ToString(dt.Rows(0)("Account_Seg_Code9"))
            obj.Account_Seg_Desc9 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc9"))
            obj.Account_Seg_Code10 = Convert.ToString(dt.Rows(0)("Account_Seg_Code10"))
            obj.Account_Seg_Desc10 = Convert.ToString(dt.Rows(0)("Account_Seg_Desc10"))
            obj.Account_Group_Code = Convert.ToString(dt.Rows(0)("Account_Group_Code"))

        End If
        Return obj
    End Function
End Class

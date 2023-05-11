Imports System.Data.SqlClient
Imports common
Public Class UserLoginInfo
    Dim strQ As String
    Dim Ds As DataSet
    Private Sub UserLoginInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        LoadData()
    End Sub
    Private Sub LoadData()
        'strQ = "select User_Code as [User Code],IP_Address as [IP Address],MAC_Address as [MAC Address],Machine_Name as [User Machine Name],Login_DateTime as [Login Time],Comp_Code as [Company Code]  from TSPL_UserLogin_Info where Logout_DateTime is null order by Login_DateTime "
        strQ = "select User_Code as [User Code],IP_Address as [IP Address],MAC_Address as [MAC Address],Machine_Name as [User Machine Name],Login_DateTime as [Login Time],Comp_Code as [Company Code]   from  TSPL_UserLogin_Info where Logout_DateTime is null and login_code in (select MAX(Login_Code ) from TSPL_UserLogin_Info group by User_Code,MAC_Address  ) order by Login_DateTime "
        Ds = connectSql.RunSQLReturnDS(strQ)
        'grdLoginInfo.AutoGenerateColumns = False
        grdLoginInfo.DataSource = Ds.Tables(0)
        For ii As Integer = 0 To grdLoginInfo.Columns.Count - 1
            grdLoginInfo.Columns(ii).ReadOnly = True
            grdLoginInfo.Columns(ii).Width = 100
        Next
        grdLoginInfo.AllowAddNewRow = False
        grdLoginInfo.ShowGroupPanel = False
        grdLoginInfo.AllowColumnReorder = False
        grdLoginInfo.AllowRowReorder = False
        grdLoginInfo.EnableSorting = False
    End Sub
End Class

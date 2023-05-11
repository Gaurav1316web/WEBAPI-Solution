Imports common
Imports System.Data.SqlClient

Public Class frmCityMasterNew : Inherits frmBase
    Dim isAddingNew As Boolean = True
    Dim isNewEntry As Boolean = False

    Private Sub frmCityMasterNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mbtnDelete.Visible = True
        mbtnExport.Visible = True
        mbtnImport.Visible = True

        If clsCommon.myLen(BillNo) > 0 Then
            LoadData1(BillNo)
        End If
        If isAddingNew Then
            MyBase.newdata()
        End If
    End Sub

    Private Sub LoadData1(ByVal BillNo As String)
        BlankAllControls()
        Dim obj As clsCityMaster = clsCityMaster.GetData(BillNo, NavigatorType.Current)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.City_Code) > 0 Then
            txtCode.Text = obj.City_Code
            txtdes.Text = obj.City_Name
        End If
    End Sub

    Private Sub BlankAllControls()
        txtCode.Text = ""
        txtdes.Text = ""
    End Sub

    Public Overrides Function CancelData() As Boolean
        LoadData(txtCode.Text)
        Return True
    End Function

    Public Overrides Function AddNew() As Boolean
        isNewEntry = True
        txtCode.Enabled = True
        BlankAllControls()
        txtCode.Focus()
        Return True
    End Function

    Public Overrides Function EditData() As Boolean
        If clsCommon.myLen(txtCode.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Code Found to Edit", Application.CompanyName)
            Return False
        End If
        isNewEntry = False
        Return True
    End Function

    Public Overrides Function AfterEdit() As Boolean
        txtCode.Enabled = False
        Return True
    End Function

    Public Overrides Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Text) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Code")
            Return False
        End If
        If clsCommon.myLen(txtdes.Text) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Name")
            Return False
        End If
        Return True
    End Function

    Public Overrides Function SaveData() As Boolean
        Dim obj As New clsCityMaster()
        obj.City_Code = txtCode.Text
        obj.City_Name = txtdes.Text

        Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
        If isSaved Then
            common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            LoadData(txtCode.Text)
        End If
        Return isSaved

    End Function

    Public Overrides Function ListData() As Boolean
        'Dim qry As String = "select City_Code as Code,City_Name as Description from TSPL_CITY_MASTER"
        'Dim strCode As String = clsCommon.ShowSelectForm(qry, "Code", "", "")
        'If (clsCommon.myLen(strCode) > 0) Then
        '    LoadData(strCode)
        'End If
    End Function

    Public Overrides Function DeleteData() As Boolean

        If clsCommon.myLen(txtCode.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Code found to Delete", Me.Name)
            Return False
        ElseIf Not (common.clsCommon.MyMessageBoxShow("Delete the Current City" + Environment.NewLine + "Are you sure?", Me.Name, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
            Return False
        End If
        If (clsCityMaster.DeleteData(txtCode.Text)) Then
            common.clsCommon.MyMessageBoxShow("Data Delete Sucessfully", Me.Name)
            Return True
        End If
    End Function

    Public Overrides Function ImportData() As Boolean
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "City Code", "City Name") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strcode As String = grow.Cells(0).Value.ToString()

                    Dim strname As String = grow.Cells(1).Value.ToString()

                    If (String.IsNullOrEmpty(strcode)) Or strcode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("City Code have some incorrect value")
                        trans.Rollback()
                        Exit Function
                    End If
                    If (String.IsNullOrEmpty(strname)) Or strname.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow(" City Name have some incorrect value")
                        trans.Rollback()
                        Exit Function
                    End If
                    Dim sql1 As String = "select count(*) from tspl_City_master where city_code='" + strcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(sql1))
                    If (i = 0) Then

                        connectSql.RunSpTransaction("sp_CityMaster_insert", New SqlParameter("@citycode", strcode), New SqlParameter("@cityname", strname), New SqlParameter("@createdby", clsCommon.myCstr(My.Settings.UserName)), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", clsCommon.myCstr(My.Settings.UserName)), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@comp_code", clsCommon.myCstr(My.Settings.Company)))
                    Else
                        connectSql.RunSpTransaction("sp_CityMaster_update", New SqlParameter("@citycode", strcode), New SqlParameter("@cityname", strname), New SqlParameter("@modifiedby", clsCommon.myCstr(My.Settings.UserName)), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@comp_code", clsCommon.myCstr(My.Settings.Company)))
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try

        End If
        Me.Controls.Remove(gv)
    End Function

    Public Overrides Function ExportData() As Boolean
        Dim str As String
        str = "select city_Code As [City Code],city_name as [City Name] from tspl_city_master"
        transportSql.ExporttoExcel(str, Me)
    End Function

    
End Class
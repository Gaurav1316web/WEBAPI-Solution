Imports common
Public Class FrmReconciliationSetting
    Inherits FrmMainTranScreen

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmReconciliationSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadAccounts()
        LoadReport()
    End Sub

    Sub LoadAccounts()
        Dim strqry As String = "select Account_Code as Code,[Description] from TSPL_GL_ACCOUNTS"
        cbgAccount.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgAccount.ValueMember = "Code"
        cbgAccount.DisplayMember = "Description"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmReconciliationSetting)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag0
    End Sub

    Sub LoadReport()
        cboReportName.DataSource = clsRecoSettingReportName.GetReportName()
        cboReportName.ValueMember = "Code"
        cboReportName.DisplayMember = "Name"
    End Sub

    Private Sub cboReportName_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboReportName.SelectedValueChanged
        cboReportComponent.DataSource = clsRecoSettingReportComponent.GetReportComponent(clsCommon.myCstr(cboReportName.SelectedValue))
        cboReportComponent.ValueMember = "Code"
        cboReportComponent.DisplayMember = "Name"
    End Sub

    Private Sub cboReportComponent_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboReportComponent.SelectedValueChanged
        LoadData()
    End Sub

    Private Sub LoadData()
        LoadAccounts()
        cbgAccount.CheckedValue = clsReconciliationSetting.GetData(clsCommon.myCstr(cboReportName.SelectedValue), clsCommon.myCstr(cboReportComponent.SelectedValue))
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim Arr As List(Of clsReconciliationSetting) = Nothing
            Dim arlst As ArrayList = cbgAccount.CheckedValue
            If arlst IsNot Nothing AndAlso arlst.Count > 0 Then
                Arr = New List(Of clsReconciliationSetting)()
                For Each strACCode As String In arlst
                    Dim obj As New clsReconciliationSetting()
                    obj.Account_Code = strACCode
                    obj.Report_Component = clsCommon.myCstr(cboReportComponent.SelectedValue)
                    obj.Report_Name = clsCommon.myCstr(cboReportName.SelectedValue)
                    Arr.Add(obj)
                Next
            End If
            clsReconciliationSetting.SaveData(Arr, clsCommon.myCstr(cboReportName.SelectedValue), clsCommon.myCstr(cboReportComponent.SelectedValue))
            clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class

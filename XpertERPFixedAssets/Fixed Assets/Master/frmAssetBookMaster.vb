
Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmAssetBookMaster
    Inherits FrmMainTranScreen
    Dim Qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False

    Private Sub frmAssetBookMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetMaxLength()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        LoadDepType()
        LoadTaxDepType()
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        txtCode.Value = ""
        Reset()
        txtCode.Focus()
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAssetGroups)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            rmiExport.Enabled = True
            rmiImport.Enabled = True
        Else
            rmiExport.Enabled = False
            rmiImport.Enabled = False
        End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub LoadDepType()
        'If CheckDDLType = False Then
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Name", GetType(String))
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr("Code") = "F"
            dr("Name") = "Formula"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "M"
            dr("Name") = "Manual"
            dt.Rows.Add(dr)

            cboDepType.DataSource = dt
            cboDepType.ValueMember = "Code"
            cboDepType.DisplayMember = "Name"

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        'End If

    End Sub

    Public Sub LoadTaxDepType()
        'If CheckDTType = False Then
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Name", GetType(String))
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr("Code") = "F"
            dr("Name") = "Formula"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("code") = "M"
            dr("Name") = "Manual"
            dt.Rows.Add(dr)

            cboTaxDepType.DataSource = dt
            cboTaxDepType.ValueMember = "Code"
            cboTaxDepType.DisplayMember = "Name"

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        'End If
    End Sub

    Private Sub Reset()

        txtCode.Value = Nothing
        txtDescription.Text = ""
        txtDepMethod.Value = Nothing
        lblDepMethod.Text = ""
        txtDepMethodTax.Value = Nothing
        lblDepMethodTax.Text = ""
        txtDepPeriod.Value = Nothing
        lblDepPeriod.Text = ""
        txtStartDate.Value = clsCommon.GETSERVERDATE
        txtDepRate.Text = 0
        cboDepType.SelectedValue = "F"
        cboTaxDepType.SelectedValue = "F"
        txtDepTaxRate.Text = 0
        txtEstLife.Text = 0
        txtSourceOrgValue.Text = 0
        txtSourceValue.Text = 0
        txtSalvageRate.Text = 0
        txtEstLife.Text = 0
        txtSourceOrgValue.Text = 0
        txtSourceValue.Text = 0
        txtSalvageRate.Text = 0
        txtnetvalue.Text = 0
        txtSalvageValue.Text = 0
        txtCode.MyReadOnly = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False

    End Sub
    Private Sub SetMaxLength()
        txtCode.MyMaxLength = 30
        txtDescription.MaxLength = 100

    End Sub

    Private Sub txtCategoryCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Qry = "select count(*) from TSPL_FA_BOOK_MASTER where Book_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If

            If txtCode.MyReadOnly OrElse isButtonClicked Then
                'Qry = "Select Book_Code as [Code], Description, Is_Default as [Default], Convert(VARCHAR,Last_Maintained_Date, 103) as [Last Maintained], Inactive from TSPL_FA_BOOK_MASTER "
                'txtGroupCode.Value = clsCommon.ShowSelectForm("AssetCategorySelector", Qry, "Code", "", txtGroupCode.Value, "", isButtonClicked)
                txtCode.Value = clsAssetBookMaster.getFinder("", txtCode.Value, isButtonClicked)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub txtCategoryCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Qry = "select count(*) from TSPL_FA_BOOK_MASTER where Book_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmAssetBookMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim Arr As New List(Of clsAssetBookMaster)
                Dim obj As New clsAssetBookMaster()
                obj.Book_Code = clsCommon.myCstr(txtCode.Value)
                obj.Book_Name = clsCommon.myCstr(txtDescription.Text)
                obj.Book_Dep_Type = clsCommon.myCstr(cboDepType.SelectedValue)
                obj.Book_Estimated_Life = clsCommon.myCdbl(txtEstLife.Text)
                obj.Book_Net_Value = clsCommon.myCdbl(txtnetvalue.Text)
                obj.Book_Salvage_Rate = clsCommon.myCdbl(txtSalvageRate.Text)
                obj.Book_Salvage_Value = clsCommon.myCdbl(txtSalvageValue.Text)
                obj.Book_Source_Original_value = clsCommon.myCdbl(txtSourceOrgValue.Text)
                obj.Book_Source_value = clsCommon.myCdbl(txtSourceValue.Text)
                obj.Dep_Method_Code = clsCommon.myCstr(txtDepMethod.Value)
                obj.Dep_Method_Tax_Code = clsCommon.myCstr(txtDepMethodTax.Value)
                obj.Dep_Period_Code = clsCommon.myCstr(txtDepPeriod.Value)
                obj.Dep_Rate = clsCommon.myCdbl(txtDepRate.Value)
                obj.Dep_Tax_Rate = clsCommon.myCstr(txtDepTaxRate.Value)
                obj.Tax_Dep_Type = clsCommon.myCstr(cboTaxDepType.SelectedValue)
                obj.Start_Date = txtStartDate.Value

                Arr.Add(obj)
                If (clsAssetBookMaster.SaveData(Arr)) Then
                    myMessages.insert()
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean

        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            RadMessageBox.Show("Please enter Group Description")
            txtDescription.Focus()
            Return False
        End If
        If txtSalvageRate.Value > 100 Then
            txtSalvageRate.Focus()
            Throw New Exception("Salvage % can not be greater than 100. ")
        End If
        If cboDepType.SelectedValue <> "F" Then

            If clsCommon.myCdbl(txtDepRate.Text) = 0 Then
                txtDepRate.Focus()
                Throw New Exception("Please Fill Depriciation  Rate")
            End If
        End If
        If cboTaxDepType.SelectedValue <> "F" Then
            If clsCommon.myCdbl(txtDepTaxRate.Text) = 0 Then
                txtDepTaxRate.Focus()
                Throw New Exception("Please Fill Depriciation Tax Rate")
            End If
        End If
        Return True
    End Function

    Private Sub LoadData(ByVal strCategoryCode As String, ByVal navType As common.NavigatorType)
        Try
            Dim obj As New clsAssetBookMaster()
            obj = clsAssetBookMaster.GetData(strCategoryCode, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Book_Code) > 0) Then
                txtCode.MyReadOnly = True
                txtCode.Value = obj.Book_Code
                txtDescription.Text = obj.Book_Name
                cboDepType.SelectedValue = obj.Book_Dep_Type
                txtEstLife.Text = obj.Book_Estimated_Life
                txtnetvalue.Text = obj.Book_Net_Value
                txtSalvageRate.Text = obj.Book_Salvage_Rate
                txtSalvageValue.Text = obj.Book_Salvage_Value
                txtSourceOrgValue.Text = obj.Book_Source_Original_value
                txtSourceValue.Text = obj.Book_Source_value
                txtDepMethod.Value = obj.Dep_Method_Code
                txtDepMethodTax.Value = obj.Dep_Method_Tax_Code
                txtDepPeriod.Value = obj.Dep_Period_Code
                txtDepRate.Value = obj.Dep_Rate
                txtDepTaxRate.Value = obj.Dep_Tax_Rate
                cboTaxDepType.SelectedValue = obj.Tax_Dep_Type
                txtStartDate.Value = obj.Start_Date

                btnSave.Text = "Update"
                btnDelete.Enabled = True

            Else
                txtCode.Value = ""
                Reset()
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Export()
    End Sub

    Private Sub Export()
        Try
            Qry = " select Book_Code as [Book Code],Book_Name as [Description],Dep_Method_Code as [Dep Method Code],Dep_Method_Tax_Code as [Dep Method Code Tax],Dep_Period_Code as [Dep Period Code],Start_Date as [Start Date],Dep_Rate as [Dep Rate],Dep_Tax_Rate as [Dep Rate Tax]," & _
                  " Book_Source_Original_value as [Source Original Value],Book_Source_value as [Source Value],Book_Estimated_Life as [Estimated Life],Book_Salvage_Rate as [Salvage Rate],Book_Salvage_Value as [Salvage Value],Book_Net_Value as [Net Value],Book_Dep_Type as [Book Dep Type],Tax_Dep_Type as [Tax Dep Type] from TSPL_FA_BOOK_MASTER"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = " Select '' as [Book Code],'' as [Description],'' as [Dep Method Code],'' as [Dep Method Code Tax],'' as [Dep Period Code],'' as [Start Date],'' as [Dep Rate],'' as [Dep Rate Tax]," & _
                      " '' as [Source Original Value],'' as [Source Value],0 as [Estimated Life],0 as [Salvage Rate],0 as [Salvage Value],0 as [Net Value],'' as [Book Dep Type],'' as [Tax Dep Type] "
            End If
            transportSql.ExporttoExcel(Qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Import()
    End Sub
    Private Sub Import()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        ''Book_Code,Book_Name,Dep_Method_Code,Dep_Method_Tax_Code,Dep_Period_Code,Start_Date,Dep_Rate,Dep_Tax_Rate,
        'Book_Source_Original_value, Book_Estimated_Life, Book_Source_value, Book_Salvage_Rate, Book_Salvage_Value, Book_Net_Value, Book_Dep_Type, Tax_Dep_Type
        If transportSql.importExcel(gv, "Book Code", "Description", "Dep Method Code", "Dep Method Code Tax", "Dep Period Code", "Start Date", "Dep Rate", "Dep Rate Tax", "Source Original Value", "Source Value", "Estimated Life", "Salvage Rate", "Salvage Value", "Net Value", "Book Dep Type", "Tax Dep Type") Then
            Try
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsAssetBookMaster)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsAssetBookMaster()

                    obj.Book_Code = clsCommon.myCstr(grow.Cells("Book Code").Value)

                    obj.Book_Name = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(obj.Book_Name) > 100 Then
                        Throw New Exception("The Maximum Length of Description on Line No '" + LineNo + "' Is Greater Than 100")
                    End If

                    obj.Dep_Method_Code = clsCommon.myCstr(grow.Cells("Dep Method Code").Value)
                    'If clsCommon.myLen(obj.Dep_Method_Code) <= 0 Then
                    '    Throw New Exception("Dep Method Code on Line No '" + LineNo + "' Is blank")
                    'End If
                    ''Book Code", "Description", "Dep Method Code", "Dep Method Code Tax", "Dep Period Code", "Start Date", "Dep Rate", "Dep Rate Tax", "Source Original Value",
                    ' "Source Value", "Estimated Life", "Salvage Rate", "Salvage Value", "Net Value", "Book Dep Type", "Tax Dep Type"
                    obj.Dep_Method_Tax_Code = clsCommon.myCstr(grow.Cells("Dep Method Code Tax").Value)
                    'If clsCommon.myLen(obj.Dep_Method_Tax_Code) <= 0 Then
                    '    Throw New Exception("Dep Method Code Tax on Line No '" + LineNo + "' Is blank")
                    'End If
                    obj.Dep_Period_Code = clsCommon.myCstr(grow.Cells("Dep Period Code").Value)
                    'If clsCommon.myLen(obj.Dep_Period_Code) <= 0 Then
                    '    Throw New Exception("Dep Period Code on Line No '" + LineNo + "' Is blank")
                    'End If
                    obj.Start_Date = clsCommon.myCstr(grow.Cells("Start Date").Value)
                    If clsCommon.myLen(obj.Start_Date) > 0 Then
                        obj.Start_Date = clsCommon.GetPrintDate(grow.Cells("Start Date").Value, "dd-MMM-yyyy")
                    End If
                    obj.Dep_Rate = clsCommon.myCdbl(grow.Cells("Dep Rate").Value)
                    obj.Dep_Tax_Rate = clsCommon.myCdbl(grow.Cells("Dep Rate Tax").Value)
                    obj.Book_Source_Original_value = clsCommon.myCdbl(grow.Cells("Source Original Value").Value)
                    obj.Book_Source_value = clsCommon.myCdbl(grow.Cells("Source Value").Value)
                    obj.Book_Estimated_Life = clsCommon.myCdbl(grow.Cells("Estimated Life").Value)
                    obj.Book_Salvage_Rate = clsCommon.myCdbl(grow.Cells("Salvage Rate").Value)
                    obj.Book_Salvage_Value = clsCommon.myCdbl(grow.Cells("Salvage Value").Value)
                    obj.Book_Net_Value = clsCommon.myCdbl(grow.Cells("Net Value").Value)
                    obj.Book_Dep_Type = clsCommon.myCstr(grow.Cells("Book Dep Type").Value)
                    obj.Tax_Dep_Type = clsCommon.myCstr(grow.Cells("Tax Dep Type").Value)

                    Arr.Add(obj)
                Next

                If (clsAssetBookMaster.SaveData(Arr)) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExit.Click
        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmDepreciationField_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsAssetBookMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully")
                    txtCode.Value = ""
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub txtDepMethod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepMethod._MYValidating
        Dim qry As String = " select Code,Description from TSPL_DEPRECIATION_METHOD "
        txtDepMethod.Value = clsCommon.ShowSelectForm("ACQDETDepMethod", qry, "Code", "", txtDepMethod.Value, "", isButtonClicked)
        lblDepMethod.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DEPRECIATION_METHOD where Code='" + txtDepMethod.Value + "'"))
    End Sub

    Private Sub txtDepMethodTax__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepMethodTax._MYValidating
        Dim qry As String = " select Code,Description from TSPL_DEPRECIATION_METHOD "
        txtDepMethodTax.Value = clsCommon.ShowSelectForm("ACQDETDepTMethod", qry, "Code", "", txtDepMethodTax.Value, "", isButtonClicked)
        lblDepMethodTax.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DEPRECIATION_METHOD where Code='" + txtDepMethodTax.Value + "'"))
    End Sub

    Private Sub txtDepPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepPeriod._MYValidating
        Dim qry As String = " select period_Code as Code,period_Desc as Description from TSPL_DEPRECIATION_PERIODS "
        txtDepPeriod.Value = clsCommon.ShowSelectForm("ACQDETDepPeriod", qry, "Code", "", txtDepPeriod.Value, "", isButtonClicked)
        lblDepPeriod.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  period_Desc from TSPL_DEPRECIATION_PERIODS where period_Code='" + txtDepPeriod.Value + "'"))
    End Sub


    Private Sub txtSalvageRate_TextChanged(sender As Object, e As EventArgs) Handles txtSalvageRate.TextChanged
        txtSalvageValue.Text = txtSourceValue.Value * txtSalvageRate.Value / 100
    End Sub
End Class

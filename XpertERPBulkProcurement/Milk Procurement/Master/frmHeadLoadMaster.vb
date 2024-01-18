Imports System.Data.SqlClient
Imports common
Imports Telerik

Public Class frmHeadLoadMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colDCSUploaderNo As String = "DCS Uploader No"
    Const colDCSCode As String = "DCS Code"
    Const colDCSName As String = "DCS Name"
    Const colBMCUploaderNo As String = "BMC Uploader No"
    Const colBMCCode As String = "BMC Code"
    Const colBMCName As String = "BMC Name"
    Const colHeadLoadBasis As String = "Head Load Basis"
    Const colHeadLoadRate As String = "Head Load Rate"
    Dim HeadLoadBasis As String = Nothing
    Dim isLoadData As Boolean = False
    Dim isCopyData As Boolean = False

#End Region

    Private Sub frmHeadLoadMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
        txtRate.Enabled = False
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
        End If
    End Sub



    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoDCSUploaderNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSUploaderNo.FormatString = ""
        repoDCSUploaderNo.HeaderText = "DCS Uploader No"
        repoDCSUploaderNo.Name = colDCSUploaderNo
        repoDCSUploaderNo.Width = 140
        repoDCSUploaderNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDCSUploaderNo)

        Dim repoDCSCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSCode.FormatString = ""
        repoDCSCode.HeaderText = "DCS Code"
        repoDCSCode.Name = colDCSCode
        repoDCSCode.Width = 100
        repoDCSCode.ReadOnly = True
        repoDCSCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDCSCode)

        Dim repoDCSName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSName.FormatString = ""
        repoDCSName.HeaderText = "DCS Name"
        repoDCSName.Name = colDCSName
        repoDCSName.Width = 100
        repoDCSName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDCSName)

        Dim repoBMCUploaderNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBMCUploaderNo.FormatString = ""
        repoBMCUploaderNo.HeaderText = "BMC Uploader No"
        repoBMCUploaderNo.Name = colBMCUploaderNo
        repoBMCUploaderNo.Width = 140
        repoBMCUploaderNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBMCUploaderNo)

        Dim repoBMCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBMCCode.FormatString = ""
        repoBMCCode.HeaderText = "BMC Code"
        repoBMCCode.Name = colBMCCode
        repoBMCCode.Width = 100
        repoBMCCode.ReadOnly = True
        repoBMCCode.IsVisible = False

        gv1.MasterTemplate.Columns.Add(repoBMCCode)

        Dim repoBMCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBMCName.FormatString = ""
        repoBMCName.HeaderText = "BMC Name"
        repoBMCName.Name = colBMCName
        repoBMCName.Width = 100
        repoBMCName.ReadOnly = True

        gv1.MasterTemplate.Columns.Add(repoBMCName)

        Dim repoHeadLoadBasis As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoHeadLoadBasis.FormatString = ""
        repoHeadLoadBasis.HeaderText = "Head Load Basis"
        repoHeadLoadBasis.Name = colHeadLoadBasis
        repoHeadLoadBasis.Width = 130
        repoHeadLoadBasis.DataSource = loadHeadLoadBasis()
        repoHeadLoadBasis.DisplayMember = "Name"
        repoHeadLoadBasis.ValueMember = "Name"
        repoHeadLoadBasis.ReadOnly = False

        gv1.MasterTemplate.Columns.Add(repoHeadLoadBasis)

        Dim repoHeadLoadRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoHeadLoadRate.FormatString = ""
        repoHeadLoadRate.HeaderText = "Head Load Rate"
        repoHeadLoadRate.Name = colHeadLoadRate
        repoHeadLoadRate.Width = 130
        repoHeadLoadRate.Minimum = 0
        repoHeadLoadRate.ReadOnly = False
        repoHeadLoadRate.ShowUpDownButtons = False

        gv1.MasterTemplate.Columns.Add(repoHeadLoadRate)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        gv1.Rows.AddNew()
        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try

            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            txtDescription.Focus()
            Throw New Exception("Description can't be blank.")
        End If
        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsHeadLoadMaster()
                obj.Document_No = txtDocumentNo.Value
                obj.Description = txtDescription.Text
                obj.Document_date = clsCommon.myCDate(txtDate.Value)
                obj.Start_Date = clsCommon.myCDate(txtstartDate.Value)
                obj.Arr = New List(Of clsHeadLoadDCS)

                For Each grow As GridViewRowInfo In gv1.Rows

                    Dim objTr As New clsHeadLoadDCS()
                    objTr.VLC_CODE = clsCommon.myCstr((grow.Cells("DCS Code").Value))
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Head Load Basis").Value), "Rate/Kg") = CompairStringResult.Equal Then
                        objTr.Head_Load_Basis = "K"
                    Else
                        objTr.Head_Load_Basis = "L"
                    End If
                    If grow.Cells("Head Load Rate").Value IsNot Nothing Then
                        objTr.Head_Load_Rate = clsCommon.myCDecimal((grow.Cells("Head Load Rate").Value))

                        obj.Arr.Add(objTr)
                    End If


                Next

                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        isNewEntry = True
        txtDocumentNo.Value = ""
        txtDescription.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtstartDate.Value = clsCommon.GETSERVERDATE()
        btnSave.Text = "Save"
        txtRate.Value = 0
        txtRate.Enabled = False
        chkRate.Checked = False
        LoadBlankGrid()
        isInsideLoadData = False
        btnDelete.Enabled = True
        lblStatus.Status = ERPTransactionStatus.Pending
        ReStoreGridLayout()
        cmbHeadLoadBasis.Text = ""
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click

        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocumentNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsHeadLoadMaster.PostData(MyBase.Form_ID, txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsHeadLoadMaster.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    btnAddNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        If clsCommon.myLen(txtDocumentNo) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
        End If
        txtDocumentNo.Value = clsHeadLoadMaster.getFinder(txtDocumentNo.Value, isButtonClicked)
        LoadData(txtDocumentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HEAD_LOAD where Document_No='" + txtDocumentNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            LoadBlankGrid()
            Dim obj As New clsHeadLoadMaster()
            obj = clsHeadLoadMaster.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0) Then
                If isCopyData = False Then
                    isNewEntry = False
                    btnSave.Text = "Update"
                    If obj.Status = 1 Then
                        lblStatus.Status = ERPTransactionStatus.Approved
                        btnDelete.Enabled = False
                        btnSave.Enabled = False
                        btnPost.Enabled = False
                        btnImport.Enabled = False
                    Else
                        lblStatus.Status = ERPTransactionStatus.Pending
                        btnDelete.Enabled = True
                        btnImport.Enabled = True
                    End If
                Else
                    isNewEntry = True
                    btnSave.Text = "Save"
                    lblStatus.Status = ERPTransactionStatus.Pending
                    btnDelete.Enabled = True
                    btnImport.Enabled = True
                End If


                txtDocumentNo.Value = obj.Document_No
                txtDate.Value = obj.Document_date
                txtstartDate.Value = obj.Start_Date
                txtDescription.Text = obj.Description

                cmbHeadLoadBasis.SelectedIndex = 0
            End If

            isLoadData = True
            isInsideLoadData = True
            setGridData(isLoadData)
            isInsideLoadData = False

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Private Sub frmHeadLoadMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnSave.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseUnpost.Visible = True
            End If
        End If
    End Sub


    Sub CancelPressed()
        Me.Close()
    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If cmbHeadLoadBasis.Text = "Select" Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Head Load Basis", Me.Text)
            Exit Sub
        End If
        isLoadData = False
        setGridData(isLoadData)
    End Sub

    Private Sub setGridData(ByVal isLoadData As Boolean)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = True
        Dim str As String = ""

        If isLoadData = True Then
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                LoadBlankGrid()
                Addnew()
                Exit Sub
            End If
            If txtDocumentNo.Value = "Default" Then
                str = "select TSPL_HEAD_LOAD_DCS.Document_No as Document_No, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Uploader No], TSPL_VLC_MASTER_HEAD.VLC_CODE as [DCS Code], TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name] ,
        TSPL_MCC_MASTER.MCC_Code_VLC_Uploader as [BMC Uploader No] ,TSPL_MCC_MASTER.MCC_Code as [BMC Code] , TSPL_MCC_MASTER.MCC_NAME as [BMC Name],(case when TSPL_HEAD_LOAD_DCS.Head_Load_Basis = '' then 'Rate/Kg' else (case when TSPL_HEAD_LOAD_DCS.Head_Load_Basis = 'K' then 'Rate/Kg' else 'Rate/Ltr' end) end) as [Head Load Basis] , TSPL_HEAD_LOAD_DCS.Head_Load_Rate as [Head Load Rate]
          from TSPL_HEAD_LOAD_DCS  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE = TSPL_HEAD_LOAD_DCS.VLC_CODE
         left  join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC where TSPL_HEAD_LOAD_DCS.Document_No = '" + txtDocumentNo.Value + "' order by Document_No "
            Else
                str = "select TSPL_HEAD_LOAD_DCS.Document_No as Document_No, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Uploader No], TSPL_VLC_MASTER_HEAD.VLC_CODE as [DCS Code], TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name] ,
        TSPL_MCC_MASTER.MCC_Code_VLC_Uploader as [BMC Uploader No] ,TSPL_MCC_MASTER.MCC_Code as [BMC Code] , TSPL_MCC_MASTER.MCC_NAME as [BMC Name]
          from TSPL_HEAD_LOAD_DCS  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE = TSPL_HEAD_LOAD_DCS.VLC_CODE
         left  join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC where TSPL_HEAD_LOAD_DCS.Document_No = '" + txtDocumentNo.Value + "' order by Document_No "
            End If
        Else

            str = "Select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [DCS Uploader No], TSPL_VLC_MASTER_HEAD.VLC_Code As [DCS Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [DCS Name],
    TSPL_MCC_MASTER.MCC_Code_VLC_Uploader As [BMC Uploader No] ,TSPL_MCC_MASTER.MCC_Code As [BMC Code] , TSPL_MCC_MASTER.MCC_NAME As [BMC Name]  From TSPL_VLC_MASTER_HEAD
     Left  Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC where TSPL_VLC_MASTER_HEAD.isOwnBMC = 0"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)

        If dt.Rows.Count > 0 Then
            gv1.DataSource = dt
            gv1.AutoExpandGroups = True
            gv1.ShowGroupPanel = False
            gv1.ShowRowHeaderColumn = False
            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = False
            gv1.BestFitColumns()
            FormatGrid(isLoadData)
        End If

    End Sub

    Private Sub FormatGrid(isLoadData As Boolean)
        gv1.AllowAddNewRow = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        If gv1.Columns("Document_No") IsNot Nothing Then
            gv1.Columns("Document_No").IsVisible = False

        End If
        gv1.Columns("BMC Code").IsVisible = False

        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).Width = 140
        Next

        If txtDocumentNo.Value <> "Default" Then
            Dim repoHeadLoadBasis As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            repoHeadLoadBasis.FormatString = ""
            repoHeadLoadBasis.HeaderText = "Head Load Basis"
            repoHeadLoadBasis.Name = "Head Load Basis"
            repoHeadLoadBasis.Width = 130
            repoHeadLoadBasis.DataSource = loadHeadLoadBasis()
            repoHeadLoadBasis.DisplayMember = "Name"
            repoHeadLoadBasis.ValueMember = "Name"
            repoHeadLoadBasis.ReadOnly = False

            Dim repoHeadLoadRate As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoHeadLoadRate.HeaderText = "Head Load Rate"
            repoHeadLoadRate.Name = "Head Load Rate"
            repoHeadLoadRate.Width = 130
            repoHeadLoadRate.ReadOnly = False
            repoHeadLoadRate.ShowUpDownButtons = False
            repoHeadLoadRate.FormatString = ""

            If isLoadData = False Then
                gv1.MasterTemplate.Columns.Insert(6, repoHeadLoadBasis)

                gv1.MasterTemplate.Columns.Insert(7, repoHeadLoadRate)
            Else
                gv1.MasterTemplate.Columns.Insert(7, repoHeadLoadBasis)
                gv1.MasterTemplate.Columns.Insert(8, repoHeadLoadRate)
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select case when Head_Load_Basis = 'K' then 'Rate/Kg' else 'Rate/Ltr' end as Head_Load_Basis, Head_Load_Rate as [Head Load Rate] ,VLC_CODE from TSPL_HEAD_LOAD_DCS where Document_No = '" & txtDocumentNo.Value & "'")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    For jj As Integer = 0 To dt.Rows.Count - 1
                        If clsCommon.CompairString(gv1.Rows(ii).Cells("DCS Code").Value, dt.Rows(jj)("VLC_CODE")) = CompairStringResult.Equal Then
                            gv1.Rows(ii).Cells("Head Load Basis").Value = clsCommon.myCstr(dt.Rows(jj)("Head_Load_Basis"))
                            gv1.Rows(ii).Cells("Head Load Rate").Value = clsCommon.myCstr(dt.Rows(jj)("Head Load Rate"))
                            Exit For
                        Else

                            gv1.Rows(ii).Cells("Head Load Basis").Value = cmbHeadLoadBasis.Text
                            If chkRate.Checked Then
                                gv1.Rows(ii).Cells("Head Load Rate").Value = txtRate.Value

                            Else
                                txtRate.Value = 0
                                gv1.Rows(ii).Cells("Head Load Rate").Value = 0.00

                            End If

                        End If
                    Next
                Next
            Else
                For ii As Integer = 0 To gv1.Rows.Count - 1

                    gv1.Rows(ii).Cells("Head Load Basis").Value = cmbHeadLoadBasis.Text
                    If chkRate.Checked Then
                        gv1.Rows(ii).Cells("Head Load Rate").Value = txtRate.Value

                    End If
                Next
            End If
        End If
        ReStoreGridLayout()
    End Sub

    Private Function loadHeadLoadBasis() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "K"
        dr("Name") = "Rate/Kg"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "Rate/Ltr"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Private Sub cmbHeadLoadBasis_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbHeadLoadBasis.SelectedIndexChanged

        If cmbHeadLoadBasis.SelectedIndex = 1 Then
            HeadLoadBasis = "K"
            cmbHeadLoadBasis.Text = "Rate/Kg"
        ElseIf cmbHeadLoadBasis.SelectedIndex = 2 Then
            cmbHeadLoadBasis.Text = "Rate/Ltr"
            HeadLoadBasis = "L"
        End If

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If gv1.Rows.Count > 1 Then

            clsCommon.MyExportToExcelGrid("", gv1, Nothing, "Head Load Master")
        Else
            clsCommon.MyMessageBoxShow(Me, "No record found", Me.Text)
        End If
    End Sub

    Private Sub btnCC_Click(sender As Object, e As EventArgs) Handles btnCC.Click
        Try
            Addnew()
            isCopyData = True
            Dim qry As String = "select Document_No as DocumentNo ,Description,convert(varchar(12),Start_Date,103) AS [Start Date],convert(varchar(12),Document_date,103) as DocumentDate,case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_HEAD_LOAD"
            Dim strCode As String = clsCommon.ShowSelectForm("HeadLoad", qry, "DocumentNo", "", "DocumentNo")
            If clsCommon.myLen(strCode) > 0 Then
                LoadData(strCode, NavigatorType.Current)
                txtDocumentNo.Value = Nothing
                isNewEntry = True
            End If
            isCopyData = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gvImport As New RadGridView()

        Me.Controls.Add(gvImport)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gvImport, "DCS Uploader No", "DCS Code", "DCS Name", "BMC Uploader No", "BMC Name", "Head Load Basis", "Head Load Rate") Then
            Try
                clsCommon.ProgressBarPercentShow()
                For ii As Integer = 0 To gvImport.Rows.Count - 1
                    If clsCommon.myLen(gvImport.Rows(ii).Cells("DCS Code").Value) > 0 Then

                        clsCommon.ProgressBarPercentUpdate((gvImport.Rows(ii).Index + 1) * 100 / (gvImport.Rows.Count + 1), "Importing  : " & (gvImport.Rows(ii).Index + 1) & "/" & gvImport.Rows.Count & "")
                        Try

                            gv1.Rows(ii).Cells("DCS Code").Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value)
                            gv1.Rows(ii).Cells("DCS Uploader No").Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Uploader No").Value)
                            gv1.Rows(ii).Cells("DCS Name").Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Name").Value)
                            gv1.Rows(ii).Cells("BMC Uploader No").Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("BMC Uploader No").Value)
                            gv1.Rows(ii).Cells("BMC Name").Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("BMC Name").Value)
                            gv1.Rows(ii).Cells("Head Load Basis").Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Head Load Basis").Value)
                            gv1.Rows(ii).Cells("Head Load Rate").Value = Math.Round(clsCommon.myCdbl(gvImport.Rows(ii).Cells("Head Load Rate").Value), 2)
                            If clsCommon.myLen(txtDocumentNo.Value) = 0 Then
                                If gv1.Rows.Count = gvImport.Rows.Count Then
                                Else
                                    gv1.Rows.AddNew()

                                End If
                            End If

                        Catch ex As Exception
                            gv1.Rows.RemoveAt(ii)
                            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        End Try
                    End If
                Next

                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data imported successfully", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                Throw New Exception(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gvImport)
    End Sub

    Private Sub chkRate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkRate.ToggleStateChanged
        If chkRate.Checked Then
            txtRate.Enabled = True
        Else
            txtRate.Enabled = False
        End If
    End Sub

    Private Sub btnReverseUnpost_Click_(sender As Object, e As EventArgs) Handles btnReverseUnpost.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsHeadLoadMaster.ReverseAndUnpost(txtDocumentNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class









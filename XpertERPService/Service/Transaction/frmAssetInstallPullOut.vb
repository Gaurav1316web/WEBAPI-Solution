'-Creted By--[Pankaj Jha]
'=============BM00000003002,Updated By Rohit june 30 , 2014 on 4:25 PM.==========================
'==============BM00000003063,Updated By Rohit,(Remark : Add Code to Load Agreement no from TSPL_VISI_MASTER also.)===========
'' updation by richA AGARWAL  against ticket no BM00000004876
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class frmAssetInstallPullOut
    Inherits FrmMainTranScreen
    Dim Qry As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ArrDb As New List(Of String)
    Const colLineNo As String = "LineNo"
    Const colSelect As String = "Select"
    Const colAssetID As String = "AssetID"
    Const colAssetMake As String = "AssetMake"
    Const colAssetNo As String = "AssetNo"
    Const colModelNo As String = "ModelNo"
    Const colAssetSize As String = "AssetSize"
    Const colSlNo As String = "SlNo"
    Const colTagNO As String = "TagNo"
    Const colSecurityAmount As String = "SecirityAmount"
    'Const colRoute As String = "RouteCode"
    'Const colROuteDesc As String = "RouteName"
    Const coLTtype As String = "Type"
    Const coLAssetTtype As String = "Asset Type"
    Const colTransDate As String = "Trans_Date" '' being treated as pullout date during pullout and pulloutandInstall while as install date during install
    Const colInstallDate As String = "InstallDate" '' only being used when pulloutandinstall for installdate
    Const colClosingDate As String = "AgreementClosingDate"
    Const colRefundAmount As String = "RefundAmount"
    Const colChequeDate As String = "ChequeDate"
    Const colChequeNo As String = "ChequeNo"
    Const colFOC As String = "colFOC"
    Const colAgreementDate As String = "Agreement Date"
    Const colAgreementNo As String = "Agreement No"

    Const colChequeDateP As String = "ChequeDateP"
    Const colChequeNoP As String = "ChequeNoP"
    Const colFOCP As String = "colFOCP"

    Dim IsInsideLoadData As Boolean = True
    Dim DtCustomerMaster, DtAssetInstallPullOutData, DtDispatchDetail As DataTable
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmAssetInstallPullOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        chkInstall.IsChecked = True
        RadLabel1.Text = "Install To Customer No."
        dgvVisi.Columns(colInstallDate).IsVisible = False
        GroupBox1.Visible = False
        dgvVisi.Location = New Point(4, 3)
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        ArrDb.Add(objCommonVar.CurrDatabase)
        DtCustomerMaster = clsDBFuncationality.GetDataTable("select * from TSPL_CUSTOMER_MASTER")
        'If clsCommon.myLen(Me.Tag) > 0 Then
        '    LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        'End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAssetInstallPullOut)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGrid()
        dgvVisi.Rows.Clear()
        dgvVisi.Columns.Clear()

        Dim LineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        LineNo = New GridViewDecimalColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 61
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvVisi.MasterTemplate.Columns.Add(LineNo)

        Dim CollSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        CollSelect.FormatString = ""
        CollSelect.HeaderText = "Select"
        CollSelect.Name = colSelect
        CollSelect.Width = 61
        CollSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        dgvVisi.MasterTemplate.Columns.Add(CollSelect)

        Dim AssetID As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetID.FormatString = ""
        AssetID.HeaderText = "Asset ID"
        AssetID.Name = colAssetID
        AssetID.Width = 121
        AssetID.MaxLength = 50
        AssetID.ReadOnly = False
        AssetID.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetID)

        Dim AssetMake As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetMake.FormatString = ""
        AssetMake.HeaderText = "Asset Make"
        AssetMake.Name = colAssetMake
        AssetMake.Width = 121
        AssetMake.MaxLength = 50
        AssetMake.ReadOnly = True
        AssetMake.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetMake)

        Dim AssetNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetNo.FormatString = ""
        AssetNo.HeaderText = "Asset No"
        AssetNo.Name = colAssetNo
        AssetNo.Width = 121
        AssetNo.MaxLength = 50
        AssetNo.ReadOnly = True
        AssetNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetNo)

        Dim ModelNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModelNo.FormatString = ""
        ModelNo.HeaderText = "Model No"
        ModelNo.Name = colModelNo
        ModelNo.Width = 121
        ModelNo.MaxLength = 50
        ModelNo.ReadOnly = True
        ModelNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(ModelNo)

        Dim AssetType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetType.FormatString = ""
        AssetType.HeaderText = "Asset Type"
        AssetType.Name = coLAssetTtype
        AssetType.Width = 121
        AssetType.MaxLength = 50
        AssetType.ReadOnly = True
        AssetType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetType)

        Dim AssetSize As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetSize.FormatString = ""
        AssetSize.HeaderText = "Asset Size"
        AssetSize.Name = colAssetSize
        AssetSize.Width = 121
        AssetSize.MaxLength = 50
        AssetSize.ReadOnly = True
        AssetSize.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetSize)

        Dim AssetSlNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetSlNo.FormatString = ""
        AssetSlNo.HeaderText = "Asset Sl. No."
        AssetSlNo.Name = colSlNo
        AssetSlNo.Width = 121
        AssetSlNo.MaxLength = 50
        AssetSlNo.ReadOnly = True
        AssetSlNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetSlNo)

        Dim AssetTagNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetTagNo.FormatString = ""
        AssetTagNo.HeaderText = "Asset Tag No"
        AssetTagNo.Name = colTagNO
        AssetTagNo.Width = 121
        AssetTagNo.MaxLength = 50
        AssetTagNo.ReadOnly = True
        AssetTagNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetTagNo)



        'Dim Location As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'Location.FormatString = ""
        'Location.HeaderText = "Location"
        'Location.Name = colLoc
        'Location.Width = 101
        'Location.MaxLength = 50
        'Location.ReadOnly = False
        'Location.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'dgvVisi.MasterTemplate.Columns.Add(Location)

        'Dim Route As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'Route.FormatString = ""
        'Route.HeaderText = "Route"
        'Route.Name = colRoute
        'Route.Width = 101
        'Route.MaxLength = 50
        'Route.ReadOnly = False
        'Route.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'dgvVisi.MasterTemplate.Columns.Add(Route)


        Dim Type As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        Type.FormatString = ""
        Type.HeaderText = "Type"
        Type.Name = coLTtype
        Type.Width = 101
        Type.ReadOnly = False
        Type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Type.DataSource = GetItemType()
        Type.ValueMember = "Code"
        Type.DisplayMember = "Code"
        dgvVisi.MasterTemplate.Columns.Add(Type)

        Dim transDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        transDate.FormatString = ""
        transDate.Format = DateTimePickerFormat.Custom
        transDate.CustomFormat = "dd/MMM/yyyy"
        If chkInstall.IsChecked Then
            transDate.HeaderText = "Installation Date"
        Else
            transDate.HeaderText = "Pullout Date"
        End If
        transDate.Name = colTransDate
        transDate.Width = 101
        transDate.ReadOnly = False
        transDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(transDate)

        If chkPullOut.IsChecked Or chkPulloutAndInstall.IsChecked Then 'dgvVisi.Columns.Contains("Pullout Date") Then
            Dim AgreementClosingDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            AgreementClosingDate.FormatString = ""
            AgreementClosingDate.Format = DateTimePickerFormat.Custom
            AgreementClosingDate.CustomFormat = "dd/MMM/yyyy"
            AgreementClosingDate.Name = colClosingDate
            AgreementClosingDate.HeaderText = "Agreement Closing Date"
            AgreementClosingDate.IsVisible = True
            AgreementClosingDate.Width = 200
            AgreementClosingDate.ReadOnly = False
            AgreementClosingDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            dgvVisi.MasterTemplate.Columns.Add(AgreementClosingDate)

            Dim AssetRefundAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
            AssetRefundAmount.FormatString = ""
            AssetRefundAmount.HeaderText = "Refund Amount"
            AssetRefundAmount.Name = colRefundAmount
            AssetRefundAmount.Width = 130
            AssetRefundAmount.ReadOnly = False
            AssetRefundAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            dgvVisi.MasterTemplate.Columns.Add(AssetRefundAmount)

            Dim chequeNo_sec As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            chequeNo_sec = New GridViewTextBoxColumn()
            chequeNo_sec.FormatString = ""
            chequeNo_sec.HeaderText = "Cheque No"
            chequeNo_sec.Name = colChequeNo
            chequeNo_sec.Width = 200
            chequeNo_sec.ReadOnly = False
            chequeNo_sec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvVisi.MasterTemplate.Columns.Add(chequeNo_sec)

            Dim chequeDate_sec As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            chequeDate_sec.CustomFormat = "dd/MM/yyyy"
            chequeDate_sec.FormatString = "{0:d}"
            chequeDate_sec.HeaderText = "Cheque Date"
            chequeDate_sec.Name = colChequeDate
            chequeDate_sec.Width = 150
            chequeDate_sec.ReadOnly = False
            chequeDate_sec.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            dgvVisi.MasterTemplate.Columns.Add(chequeDate_sec)


            Dim FOC_sec As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            FOC_sec.FormatString = ""
            FOC_sec.DataSource = loadFOC()
            FOC_sec.DisplayMember = "Status"
            FOC_sec.ValueMember = "Status"
            FOC_sec.HeaderText = "FOC(YES/NO)"
            FOC_sec.Name = colFOC
            FOC_sec.Width = 160
            FOC_sec.ReadOnly = False
            FOC_sec.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            dgvVisi.MasterTemplate.Columns.Add(FOC_sec)
        End If

        If chkInstall.IsChecked Or chkPulloutAndInstall.IsChecked Then

            Dim AgreementNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            AgreementNo = New GridViewTextBoxColumn()
            AgreementNo.FormatString = ""
            AgreementNo.HeaderText = "Agreement No"
            AgreementNo.Name = colAgreementNo
            AgreementNo.Width = 200
            AgreementNo.ReadOnly = False
            AgreementNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            dgvVisi.MasterTemplate.Columns.Add(AgreementNo)

            Dim AgreementDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            AgreementDate.CustomFormat = "dd/MM/yyyy"
            AgreementDate.FormatString = "{0:d}"
            AgreementDate.HeaderText = "Agreement Date"
            AgreementDate.Name = colAgreementDate
            AgreementDate.Width = 150
            AgreementDate.ReadOnly = False
            AgreementDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            dgvVisi.MasterTemplate.Columns.Add(AgreementDate)

        End If

        Dim InstallDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        InstallDate.FormatString = ""
        InstallDate.Format = DateTimePickerFormat.Custom
        InstallDate.CustomFormat = "dd/MMM/yyyy"
        InstallDate.Name = colInstallDate
        InstallDate.IsVisible = False
        If chkPulloutAndInstall.IsChecked Then
            dgvVisi.Columns(colTransDate).HeaderText = "Pullout Date"
            InstallDate.HeaderText = "Install Date"
            InstallDate.IsVisible = True
        End If
        InstallDate.Width = 101
        InstallDate.ReadOnly = False
        transDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(InstallDate)

        Dim AssetSecurityAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetSecurityAmount.FormatString = ""
        AssetSecurityAmount.HeaderText = "Security Amount"
        AssetSecurityAmount.Name = colSecurityAmount
        AssetSecurityAmount.Width = 121
        AssetSecurityAmount.MaxLength = 50
        AssetSecurityAmount.ReadOnly = False
        AssetSecurityAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetSecurityAmount)


        Dim chequeNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        chequeNo = New GridViewTextBoxColumn()
        chequeNo.FormatString = ""
        chequeNo.HeaderText = "Cheque No"
        chequeNo.Name = colChequeNoP
        chequeNo.Width = 200
        chequeNo.ReadOnly = False
        chequeNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvVisi.MasterTemplate.Columns.Add(chequeNo)

        Dim chequeDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        chequeDate.CustomFormat = "dd/MM/yyyy"
        chequeDate.FormatString = "{0:d}"
        chequeDate.HeaderText = "Cheque Date"
        chequeDate.Name = colChequeDateP
        chequeDate.Width = 150
        chequeDate.ReadOnly = False
        chequeDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(chequeDate)


        Dim FOC As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        FOC.FormatString = ""
        FOC.DataSource = loadFOC()
        FOC.DisplayMember = "Status"
        FOC.ValueMember = "Status"
        FOC.HeaderText = "FOC(YES/NO)"
        FOC.Name = colFOCP
        FOC.Width = 160
        FOC.ReadOnly = False
        FOC.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(FOC)

        dgvVisi.EnableFiltering = True
        dgvVisi.AllowDeleteRow = False
        dgvVisi.ShowGroupPanel = False
        dgvVisi.AllowColumnReorder = False
        dgvVisi.AllowRowReorder = False
        dgvVisi.EnableSorting = False
        dgvVisi.AllowAddNewRow = False
        dgvVisi.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvVisi.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Function loadFOC() As DataTable
        Dim dt As New DataTable
        Try
            dt.Columns.Add("Status", GetType(String))
            Dim dr As DataRow = dt.NewRow()
            dr("Status") = "YES"
            dt.Rows.Add(dr)
            dr = dt.NewRow()
            dr("Status") = "NO"
            dt.Rows.Add(dr)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return dt
    End Function

    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "New"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Refurnished"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub Reset()
        fndCustomer.Value = ""
        lblCustomerName.Text = ""
        lblRoute.Text = ""
        chkDispatchOnly.Checked = False
        'chkDispatchOnly.Visible = False
        If Not chkPulloutAndInstall.IsChecked Then
            RadLabel1.Text = "Customer No."
            GroupBox1.Visible = True
            dgvVisi.Top = GroupBox1.Top
            GroupBox1.Visible = False
        End If
        LoadBlankGrid()
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        Qry = "select Cust_Code as [CustomerCode],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(select case when Status='N' then 'Active' else 'Inactive' end ) as [Status] from TSPL_CUSTOMER_MASTER  "
        fndCustomer.Value = clsCommon.ShowSelectForm("CustFinderInVisiPullout", Qry, "CustomerCode", "", fndCustomer.Value, "", isButtonClicked)
        LoadCustomer(fndCustomer.Value, NavigatorType.Current)
    End Sub

    Private Sub fndCustomer__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCustomer._MYNavigator
        LoadCustomer(fndCustomer.Value, NavType)
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then
                Dim Arr As New List(Of clsAssetInstallPullOut)
                'Dim Arr1 As New List(Of clsAssetInstallPullOut)
                For Each grow As GridViewRowInfo In dgvVisi.Rows
                    Dim objTr As New clsAssetInstallPullOut()
                    If grow.Cells(colSelect).Value = True Then
                        If chkInstall.IsChecked Then
                            objTr.InstallCustomer_Id = clsCommon.myCstr(fndCustomer.Value)
                            objTr.installDate = clsCommon.GetPrintDate(grow.Cells(colTransDate).Value, "dd/MMM/yyyy")

                            objTr.Agreement_date = clsCommon.GetPrintDate(grow.Cells(colAgreementDate).Value, "dd/MMM/yyyy")
                            objTr.Agreement_No = clsCommon.myCstr(grow.Cells(colAgreementNo).Value)
                            objTr.Cheque_No_sec = clsCommon.myCstr(grow.Cells(colChequeNoP).Value)
                            objTr.FOC_sec = clsCommon.myCstr(grow.Cells(colFOCP).Value)
                            objTr.Item_Id = clsCommon.myCstr(grow.Cells(colAssetNo).Value)
                            objTr.Cheque_date_sec = clsCommon.GetPrintDate(grow.Cells(colChequeDateP).Value, "dd/MMM/yyyy")

                        ElseIf chkPullOut.IsChecked Then
                            objTr.PulloutCustomer_Id = clsCommon.myCstr(fndCustomer.Value)
                            objTr.pulloutDate = clsCommon.GetPrintDate(grow.Cells(colTransDate).Value, "dd/MMM/yyyy")

                            objTr.ClosingDate = clsCommon.GetPrintDate(grow.Cells(colClosingDate).Value, "dd/MMM/yyyy")
                            objTr.Refund_Amount = clsCommon.myCstr(grow.Cells(colRefundAmount).Value)
                            objTr.Cheque_No = clsCommon.myCstr(grow.Cells(colChequeNo).Value)
                            objTr.FOC = clsCommon.myCstr(grow.Cells(colFOC).Value)
                            objTr.Cheque_date = clsCommon.GetPrintDate(grow.Cells(colChequeDate).Value, "dd/MMM/yyyy")
                            objTr.Item_Id = clsCommon.myCstr(grow.Cells(colAssetNo).Value)
                        Else
                            objTr.InstallCustomer_Id = clsCommon.myCstr(fndCustomer1.Value)
                            objTr.PulloutCustomer_Id = clsCommon.myCstr(fndCustomer.Value)
                            objTr.installDate = clsCommon.GetPrintDate(grow.Cells(colInstallDate).Value, "dd/MMM/yyyy")
                            objTr.pulloutDate = clsCommon.GetPrintDate(grow.Cells(colTransDate).Value, "dd/MMM/yyyy")

                            objTr.Agreement_date = clsCommon.GetPrintDate(grow.Cells(colAgreementDate).Value, "dd/MMM/yyyy")
                            objTr.Agreement_No = clsCommon.myCstr(grow.Cells(colAgreementNo).Value)
                            objTr.Cheque_No_sec = clsCommon.myCstr(grow.Cells(colChequeNoP).Value)
                            objTr.FOC_sec = clsCommon.myCstr(grow.Cells(colFOCP).Value)
                            objTr.Cheque_date_sec = clsCommon.GetPrintDate(grow.Cells(colChequeDateP).Value, "dd/MMM/yyyy")
                            objTr.ClosingDate = clsCommon.GetPrintDate(grow.Cells(colClosingDate).Value, "dd/MMM/yyyy")
                            objTr.Refund_Amount = clsCommon.myCstr(grow.Cells(colRefundAmount).Value)
                            objTr.Cheque_No = clsCommon.myCstr(grow.Cells(colChequeNo).Value)
                            objTr.FOC = clsCommon.myCstr(grow.Cells(colFOC).Value)
                            objTr.Cheque_date = clsCommon.GetPrintDate(grow.Cells(colChequeDate).Value, "dd/MMM/yyyy")
                            objTr.Item_Id = clsCommon.myCstr(grow.Cells(colAssetNo).Value)

                        End If

                        objTr.Asset_Id = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                        'objTr.AssetMake = clsCommon.myCstr(grow.Cells(colAssetMake).Value)
                        'objTr.Location = clsCommon.myCstr(grow.Cells(colLoc).Value)
                        'objTr.Route = clsCommon.myCstr(grow.Cells(colRoute).Value)
                        If chkInstall.IsChecked Then
                            objTr.Trans_Type = "Installed"
                        ElseIf chkPullOut.IsChecked Then
                            objTr.Trans_Type = "PulledOut"
                        ElseIf chkPulloutAndInstall.IsChecked Then
                            objTr.Trans_Type = "Both"
                        End If
                        objTr.Type = clsCommon.myCstr(grow.Cells(coLTtype).Value)
                        objTr.Trans_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
                        Arr.Add(objTr)
                        'If chkPulloutAndInstall.IsChecked Then
                        '    objTr.Trans_Type = "Installed"
                        '    objTr.Customer_Id = clsCommon.myCstr(fndCustomer1.Value)
                        '    objTr.Trans_Date = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells(colInstallDate).Value), "dd/MMM/yyyy")
                        '    Arr1.Add(objTr)
                        'End If
                    End If
                Next
                'If chkPulloutAndInstall.IsChecked Then
                '    Dim issaved As Boolean = True
                '    issaved = issaved AndAlso clsAssetInstallPullOut.SaveData(Arr, ArrDb)
                '    issaved = issaved AndAlso clsAssetInstallPullOut.SaveData(Arr1, ArrDb)
                '    If issaved Then
                '        RadMessageBox.Show("Data Saved Successfully")
                '        LoadCustomer(fndCustomer.Value, NavigatorType.Current)
                '    End If
                If (clsAssetInstallPullOut.SaveData(Arr, ArrDb)) Then
                    RadMessageBox.Show("Data Saved Successfully")
                    LoadCustomer(fndCustomer.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(fndCustomer.Value) > 0 Then
            Dim Counter As Integer = 0
            For i As Integer = 0 To dgvVisi.Rows.Count - 1
                If dgvVisi.Rows(i).Cells(colSelect).Value = True Then
                    Counter += 1
                    If clsCommon.myLen(dgvVisi.Rows(i).Cells(colTransDate).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please select Date At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        Return False
                    ElseIf clsCommon.myLen(dgvVisi.Rows(i).Cells(coLTtype).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please Select Type At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        Return False
                        'ElseIf clsCommon.myLen(dgvVisi.Rows(i).Cells(colLoc).Value) <= 0 Then
                        '    clsCommon.MyMessageBoxShow("Please Select Location At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        '    Return False
                        'ElseIf clsCommon.myLen(dgvVisi.Rows(i).Cells(colRoute).Value) <= 0 Then
                        '    clsCommon.MyMessageBoxShow("Please Select Route At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        '    Return False

                    ElseIf chkInstall.IsChecked = True AndAlso chkOldEntries.Checked = False Then
                        Dim q1 As String = ""
                        q1 = "select Max(cn) from (select count(*) as cn from tspl_asset_agreement_details where asset_ID='" & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colAssetID).Value) & "' and outlet_no='" & clsCommon.myCstr(fndCustomer.Value) & "' and received_yn='Y' and agreement_from_date <='" & clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colTransDate).Value, "dd/MMM/yyyy") & "' and agreement_to_date >='" & clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colTransDate).Value, "dd/MMM/yyyy") & "' " _
                        & " Union " _
                        & " select count(*) as cn from TSPL_VISI_MASTER where Visi_Id='" & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colAssetID).Value) & "' and Customer_Id='" & clsCommon.myCstr(fndCustomer.Value) & "')  tt"

                        Dim cnt As Integer = clsDBFuncationality.getSingleValue(q1)
                        If cnt = 0 Then
                            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable("select Agreement_No,agreement_date,Received_yn,agreement_from_date,agreement_to_date from tspl_asset_agreement_details where outlet_no='" & clsCommon.myCstr(fndCustomer.Value) & "' and asset_id='" & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colAssetID).Value) & "'")
                            If dtbl.Rows.Count <= 0 Then
                                clsCommon.MyMessageBoxShow("No agreement found against said asset and customer")
                                Return False
                            Else
                                Dim s As String = "Agreement No " & dtbl.Rows(0)("Agreement_No") & Environment.NewLine & "Agreement Date " & dtbl.Rows(0)("Agreement_Date") & Environment.NewLine & "Received status  " & dtbl.Rows(0)("Received_yn") & Environment.NewLine & "Agreement Period From " & dtbl.Rows(0)("agreement_from_date") & " To " & dtbl.Rows(0)("agreement_to_date")
                                clsCommon.MyMessageBoxShow("Asset Agreement Status is Invalid... Check Agreement Status and Validity Period for the asset at line no " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) & Environment.NewLine & s)
                                Return False
                            End If

                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colAgreementNo).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select Agreement No at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colAgreementDate).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select Agreement Date at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                    ElseIf chkPullOut.IsChecked AndAlso chkOldEntries.Checked = False Then

                        Qry = "Select Visi_Installation_date from TSPL_VISI_MASTER Where Visi_Id='" + dgvVisi.Rows(i).Cells(colAssetID).Value + "' AND Customer_Id='" + fndCustomer.Value + "'"
                        Dim InstallationDate As Date = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(Qry), "dd/MMM/yyyy")
                        Dim PulloutDate As Date = clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colTransDate).Value, "dd/MMM/yyyy")
                        Dim ts As TimeSpan = PulloutDate - InstallationDate
                        If CInt(ts.TotalDays) < 0 Then
                            clsCommon.MyMessageBoxShow("You cann't pullout Asset - " + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colAssetID).Value) + " Before date - " + InstallationDate + " at Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                            Return False
                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colClosingDate).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select Closing Date of Agreement at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colFOC).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select FOC at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colFOC).Value) = 2 And clsCommon.myLen(dgvVisi.Rows(i).Cells(colRefundAmount).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select Refund Amount at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                    ElseIf chkPulloutAndInstall.IsChecked AndAlso chkOldEntries.Checked = False Then
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colTransDate).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select Date of Pull Out at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colClosingDate).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select Closing Date of Agreement at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colFOC).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select FOC at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colFOC).Value) = 2 And clsCommon.myLen(dgvVisi.Rows(i).Cells(colRefundAmount).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select Refund Amount at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If
                        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(dgvVisi.Rows(i).Cells(colInstallDate).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please Select Date to install at line no.  " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value))
                            Return False
                        End If

                        Dim q1 As String = "select sum(cc) from (select count(*) as cc from tspl_asset_agreement_details where asset_ID='" & dgvVisi.Rows(i).Cells(colAssetID).Value & "' and outlet_no='" & fndCustomer1.Value & "' and received_yn='Y' and  agreement_from_date<='" & clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colInstallDate).Value, "dd/MMM/yyyy") & "' and agreement_to_date>='" & clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colInstallDate).Value, "dd/MMM/yyyy") & "'"
                        q1 &= " Union select COUNT(*) from TSPL_ASSET_INSTALL_PULLOUT_NEW where asset_ID='" & dgvVisi.Rows(i).Cells(colAssetID).Value & "' and Install_Customer_Id='" & fndCustomer.Value & "'  and  agreement_date<='" & clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colAgreementDate).Value, "dd/MMM/yyyy") & "' and agreement_date>='" & clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colAgreementDate).Value, "dd/MMM/yyyy") & "' and Trans_Type='Installed') as tt"

                        Dim cnt As Integer = clsDBFuncationality.getSingleValue(q1)
                        If cnt = 0 Then

                            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable("select Agreement_No,agreement_date,Received_yn,agreement_from_date,agreement_to_date from tspl_asset_agreement_details where outlet_no='" & clsCommon.myCstr(fndCustomer1.Value) & "' and asset_id='" & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colAssetID).Value) & "'")
                            If dtbl.Rows.Count <= 0 Then
                                clsCommon.MyMessageBoxShow("No agreement found against said asset and customer")
                                Return False
                            Else
                                Dim s As String = "Agreement No " & dtbl.Rows(0)("Agreement_No") & Environment.NewLine & "Agreement Date " & dtbl.Rows(0)("Agreement_Date") & Environment.NewLine & "Received status  " & dtbl.Rows(0)("Received_yn") & Environment.NewLine & "Agreement Period From " & dtbl.Rows(0)("agreement_from_date") & " To " & dtbl.Rows(0)("agreement_to_date")
                                clsCommon.MyMessageBoxShow("Asset Agreement Status is Invalid... Check Agreement Status and Validity Period for the asset at line no " & clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) & Environment.NewLine & s)
                                Return False
                            End If
                        End If

                        Qry = "Select Visi_Installation_date from TSPL_VISI_MASTER Where Visi_Id='" + dgvVisi.Rows(i).Cells(colAssetID).Value + "' AND Customer_Id='" + fndCustomer.Value + "'"
                        Dim InstallationDate As Date = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(Qry), "dd/MMM/yyyy")
                        Dim PulloutDate As Date = clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colTransDate).Value, "dd/MMM/yyyy")
                        Dim ts As TimeSpan = PulloutDate - InstallationDate
                        If CInt(ts.TotalDays) < 0 Then
                            clsCommon.MyMessageBoxShow("You cann't pullout Asset - " + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colAssetID).Value) + " Before date - " + InstallationDate + " at Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                            Return False
                        End If
                        InstallationDate = clsCommon.GetPrintDate(clsCommon.myCDate(dgvVisi.Rows(i).Cells(colInstallDate).Value), "dd/MMM/yyyy")
                        PulloutDate = clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colTransDate).Value, "dd/MMM/yyyy")
                        ts = InstallationDate - PulloutDate
                        If CInt(ts.TotalDays) < 0 Then
                            clsCommon.MyMessageBoxShow("You cann't Install Asset - " + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colAssetID).Value) + " Before date - " + PulloutDate + " at Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                            Return False
                        End If

                    End If
                End If
            Next
            If Counter > 0 Then
                Return True
            Else
                clsCommon.MyMessageBoxShow("Please Select Atleast Single Row")
                Return False
            End If
        Else
            clsCommon.MyMessageBoxShow("Please Select Customer")
            fndCustomer.Focus()
            Return False
        End If
        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(fndCustomer.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please Select The Pullout from customer")
            fndCustomer.Focus()
            Return False
        End If
        If chkPulloutAndInstall.IsChecked And clsCommon.myLen(fndCustomer1.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please Select The Install to customer")
            fndCustomer1.Focus()
            Return False
        End If

        Return True
    End Function


    Private Sub LoadCustomer(ByVal strCustCode As String, ByVal navType As common.NavigatorType)
        Try
            Reset()
            IsInsideLoadData = True
            dt = clsVisiMaster.GetDataForVisiInstallPullout(strCustCode, navType)
            If dt.Rows.Count > 0 Then
                fndCustomer.Value = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                lblCustomerName.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                lblRoute.Text = clsCommon.myCstr(dt.Rows(0)("Route_No")) + " - " + clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            End If
            If chkInstall.IsChecked Then
                LoadDetails(fndCustomer.Value, "Install")
                dgvVisi.AllowAddNewRow = True
                '    clsCommon.MyMessageBoxShow(dgvVisi.CurrentRow.Cells(coLTtype).Value)
            Else
                LoadDetails(fndCustomer.Value, "Pullout")
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub

    Private Sub LoadDetails(ByVal strCustCode As String, ByVal StrTransType As String, Optional ByVal StrSerialNo As String = "")
        Try
            DtDispatchDetail = clsDBFuncationality.GetDataTable("select serial_no,Vendor_Code,RGP_Date,coalesce(TSPL_RGP_DETAIL.agreement_no,TSPL_ASSET_INSTALL_PULLOUT_NEW.agreement_no) as agreement_no,agreement_date,foc_sec,cheque_date_sec,Cheque_no_sec from TSPL_RGP_DETAIL inner join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No left join TSPL_ASSET_INSTALL_PULLOUT_NEW on TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id=serial_no")
            If strCustCode <> "" Then LoadBlankGrid()
            If clsCommon.CompairString(StrTransType, "Install") = CompairStringResult.Equal Then
                If chkDispatchOnly.Checked Then
                    Qry = "Select Case When (ISNULL(Visi_Installation_date,'')<>'' AND ISNULL(Customer_Id,'')<>'') Then CAST(1 AS bit) Else CAST(0 as Bit) End As [Select],               visi_Id, visimakecode.DESCRIPTION as VisiMake, Asset_No, visimodeNoCode.DESCRIPTION as Model_no, visisizeCode.DESCRIPTION as Visi_Size, Visi_Installation_date, Pull_Out_Date, CAse When TSPL_VISI_MASTER.Type='N' Then 'New' When TSPL_VISI_MASTER.Type='R' Then 'Refurnished' Else '' End as Visi_Type, TSPL_VISI_MASTER.Location,visiAssetTypeCode.DESCRIPTION as  ASSET_TYPE, TSPL_LOCATION_MASTER.Location_Desc, TSPL_VISI_MASTER.Route, TSPL_ROUTE_MASTER.Route_Desc ,TSPL_VISI_MASTER.Serial_No ,TSPL_VISI_MASTER.Tag_No,coalesce(TSPL_VISI_MASTER.security_amount,sec_amount) as [Security Amount],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_NO_sec as [Cheque No Sec],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_DATE_sec as [Cheque Date Sec],TSPL_ASSET_INSTALL_PULLOUT_new.FOC_sec,coalesce(TSPL_VISI_MASTER.agreement_no,TSPL_ASSET_INSTALL_PULLOUT_new.agreement_no) AS [Agreement No],TSPL_ASSET_INSTALL_PULLOUT_new.agreement_date as [Agreement Date]  from TSPL_VISI_MASTER LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_VISI_MASTER.Route LEFT OUTER JOIN TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_VISI_MASTER.Location left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visimakecode on visimakecode.CODE =TSPL_VISI_MASTER.VisiMake                    left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visiModeNoCode on visiModeNoCode .CODE =TSPL_VISI_MASTER.Model_No                  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visisizeCode on visisizeCode.CODE =TSPL_VISI_MASTER.Visi_Size     left outer join  TSPL_ITEM_CATEGORY_LEVEL_VALUES visiAssetTypeCode on visiAssetTypeCode.CODE =TSPL_VISI_MASTER.asset_type   left join TSPL_ASSET_INSTALL_PULLOUT_new on Asset_Id=Serial_No and install_customer_id='" + strCustCode + "' and coalesce(customer_id,'')<>'' WHERE (Customer_Id='" + strCustCode + "' OR ISNULL(Customer_Id,'')='')  and Visi_Id in (select TSPL_RGP_DETAIL.serial_no from TSPL_RGP_DETAIL where TSPL_RGP_DETAIL.RGP_No in (select RGP_No from TSPL_RGP_HEAD where TSPL_RGP_HEAD.Vendor_Code='" & strCustCode & "' and TSPL_RGP_HEAD.Status=1))"
                Else
                    If strCustCode <> "" Then
                        Qry = "Select Distinct Case When (ISNULL(Visi_Installation_date,'')<>'' AND ISNULL(Customer_Id,'')<>'') Then CAST(1 AS bit) Else CAST(0 as Bit) End As [Select],               visi_Id, visimakecode.DESCRIPTION as VisiMake, Asset_No, visimodeNoCode.DESCRIPTION as Model_no, visisizeCode.DESCRIPTION as Visi_Size, Visi_Installation_date, Pull_Out_Date, CAse When TSPL_VISI_MASTER.Type='N' Then 'New' When TSPL_VISI_MASTER.Type='R' Then 'Refurnished' Else '' End as Visi_Type, TSPL_VISI_MASTER.Location,visiAssetTypeCode.DESCRIPTION as  ASSET_TYPE, TSPL_LOCATION_MASTER.Location_Desc, TSPL_VISI_MASTER.Route, TSPL_ROUTE_MASTER.Route_Desc ,TSPL_VISI_MASTER.Serial_No ,TSPL_VISI_MASTER.Tag_No,coalesce(TSPL_VISI_MASTER.security_amount,sec_amount) as [Security Amount],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_NO_sec as [Cheque No Sec],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_DATE_sec as [Cheque Date Sec],TSPL_ASSET_INSTALL_PULLOUT_new.FOC_sec,coalesce(TSPL_VISI_MASTER.agreement_no,TSPL_ASSET_INSTALL_PULLOUT_new.agreement_no) AS [Agreement No],TSPL_ASSET_INSTALL_PULLOUT_new.agreement_date as [Agreement Date]  from TSPL_VISI_MASTER LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_VISI_MASTER.Route LEFT OUTER JOIN TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_VISI_MASTER.Location left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visimakecode on visimakecode.CODE =TSPL_VISI_MASTER.VisiMake                    left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visiModeNoCode on visiModeNoCode .CODE =TSPL_VISI_MASTER.Model_No                  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visisizeCode on visisizeCode.CODE =TSPL_VISI_MASTER.Visi_Size     left outer join  TSPL_ITEM_CATEGORY_LEVEL_VALUES visiAssetTypeCode on visiAssetTypeCode.CODE =TSPL_VISI_MASTER.asset_type  left join TSPL_ASSET_INSTALL_PULLOUT_new on Asset_Id=Serial_No and install_customer_id='" + strCustCode + "'  and coalesce(customer_id,'')<>'' WHERE  Customer_Id='" + strCustCode + "'"
                    Else
                        Qry = "Select Case When (ISNULL(Visi_Installation_date,'')<>'' AND ISNULL(Customer_Id,'')<>'') Then CAST(1 AS bit) Else CAST(0 as Bit) End As [Select],               visi_Id, visimakecode.DESCRIPTION as VisiMake, Asset_No, visimodeNoCode.DESCRIPTION as Model_no, visisizeCode.DESCRIPTION as Visi_Size, Visi_Installation_date, Pull_Out_Date, CAse When TSPL_VISI_MASTER.Type='N' Then 'New' When TSPL_VISI_MASTER.Type='R' Then 'Refurnished' Else '' End as Visi_Type, TSPL_VISI_MASTER.Location,visiAssetTypeCode.DESCRIPTION as  ASSET_TYPE, TSPL_LOCATION_MASTER.Location_Desc, TSPL_VISI_MASTER.Route, TSPL_ROUTE_MASTER.Route_Desc ,TSPL_VISI_MASTER.Serial_No ,TSPL_VISI_MASTER.Tag_No,coalesce(TSPL_VISI_MASTER.security_amount,sec_amount) as [Security Amount],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_NO_sec as [Cheque No Sec],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_DATE_sec as [Cheque Date Sec],TSPL_ASSET_INSTALL_PULLOUT_new.FOC_sec,coalesce(TSPL_VISI_MASTER.agreement_no,TSPL_ASSET_INSTALL_PULLOUT_new.agreement_no) AS [Agreement No],TSPL_ASSET_INSTALL_PULLOUT_new.agreement_date as [Agreement Date]  from TSPL_VISI_MASTER LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_VISI_MASTER.Route LEFT OUTER JOIN TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_VISI_MASTER.Location left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visimakecode on visimakecode.CODE =TSPL_VISI_MASTER.VisiMake                    left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visiModeNoCode on visiModeNoCode .CODE =TSPL_VISI_MASTER.Model_No                  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visisizeCode on visisizeCode.CODE =TSPL_VISI_MASTER.Visi_Size     left outer join  TSPL_ITEM_CATEGORY_LEVEL_VALUES visiAssetTypeCode on visiAssetTypeCode.CODE =TSPL_VISI_MASTER.asset_type  left join TSPL_ASSET_INSTALL_PULLOUT_new on Asset_Id=Serial_No and install_customer_id='" + strCustCode + "'  and coalesce(customer_id,'')<>'' WHERE  " & IIf(StrSerialNo = "", "Customer_Id='" + strCustCode + "'", "visi_Id='" & StrSerialNo & "'") & ""
                    End If
                End If

            Else
                Qry = "Select  CAST(0 as Bit) As [Select],visi_Id, visimakecode.DESCRIPTION as VisiMake, Asset_No, visimodeNoCode.DESCRIPTION as Model_no, visisizeCode.DESCRIPTION as Visi_Size, Visi_Installation_date, Pull_Out_Date, CAse When TSPL_VISI_MASTER.Type='N' Then 'New' When TSPL_VISI_MASTER.Type='R' Then 'Refurnished' Else '' End as Visi_Type, TSPL_VISI_MASTER.Location,visiAssetTypeCode.DESCRIPTION as  ASSET_TYPE, TSPL_LOCATION_MASTER.Location_Desc, TSPL_VISI_MASTER.Route, TSPL_ROUTE_MASTER.Route_Desc ,TSPL_VISI_MASTER.Serial_No ,TSPL_VISI_MASTER.Tag_No,coalesce(TSPL_VISI_MASTER.security_amount,sec_amount) as [Security Amount],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_NO_sec as [Cheque No Sec],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_DATE_sec as [Cheque Date Sec],TSPL_ASSET_INSTALL_PULLOUT_new.FOC_sec,null AS [Agreement No],null as [Agreement Date] ,agreement_closing_date as [Agreement Closing Date],refund_amount as [Refund Amount],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_NO as [Cheque No],TSPL_ASSET_INSTALL_PULLOUT_new.CHEQUE_DATE as [Cheque Date],TSPL_ASSET_INSTALL_PULLOUT_new.FOC  from TSPL_VISI_MASTER LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_VISI_MASTER.Route LEFT OUTER JOIN TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_VISI_MASTER.Location left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visimakecode on visimakecode.CODE =TSPL_VISI_MASTER.VisiMake                    left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visiModeNoCode on visiModeNoCode .CODE =TSPL_VISI_MASTER.Model_No                  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES visisizeCode on visisizeCode.CODE =TSPL_VISI_MASTER.Visi_Size     left outer join  TSPL_ITEM_CATEGORY_LEVEL_VALUES visiAssetTypeCode on visiAssetTypeCode.CODE =TSPL_VISI_MASTER.asset_type   left join TSPL_ASSET_INSTALL_PULLOUT_new on Asset_Id=Serial_No and install_customer_id='" + strCustCode + "' and coalesce(customer_id,'')<>'' WHERE Customer_Id='" + strCustCode + "'"
            End If
            dt = clsDBFuncationality.GetDataTable(Qry)
            Dim ii As Integer = 0
            For Each dr As DataRow In dt.Rows
                If strCustCode <> "" Then
                    dgvVisi.Rows.AddNew()
                End If
                ii += 1
                dgvVisi.CurrentRow.Cells(colLineNo).Value = ii
                dgvVisi.CurrentRow.Cells(colSelect).Value = True 'dr("Select")
                IsInsideLoadData = True
                dgvVisi.CurrentRow.Cells(colAssetID).Value = clsCommon.myCstr(dr("Visi_Id"))
                IsInsideLoadData = False
                dgvVisi.CurrentRow.Cells(colAssetMake).Value = clsCommon.myCstr(dr("VisiMake"))
                dgvVisi.CurrentRow.Cells(colAssetNo).Value = clsCommon.myCstr(dr("Asset_No"))
                dgvVisi.CurrentRow.Cells(colModelNo).Value = clsCommon.myCstr(dr("Model_No"))
                dgvVisi.CurrentRow.Cells(colAssetSize).Value = clsCommon.myCstr(dr("Visi_Size"))
                dgvVisi.CurrentRow.Cells(coLTtype).Value = clsCommon.myCstr(dr("Visi_Type"))
                dgvVisi.CurrentRow.Cells(coLAssetTtype).Value = clsCommon.myCstr(dr("ASSET_TYPE"))
                dgvVisi.CurrentRow.Cells(colSlNo).Value = clsCommon.myCstr(dr("Serial_No"))
                dgvVisi.CurrentRow.Cells(colTagNO).Value = clsCommon.myCstr(dr("Tag_No"))
                dgvVisi.CurrentRow.Cells(colSecurityAmount).Value = clsCommon.myCstr(dr("Security Amount"))
                If chkPullOut.IsChecked Or chkPulloutAndInstall.IsChecked Then
                    dgvVisi.CurrentRow.Cells(colRefundAmount).Value = clsCommon.myCstr(dr("Refund Amount"))
                    dgvVisi.CurrentRow.Cells(colClosingDate).Value = dr("Agreement Closing Date")
                    dgvVisi.CurrentRow.Cells(colChequeDate).Value = dr("Cheque Date")
                    dgvVisi.CurrentRow.Cells(colChequeNo).Value = clsCommon.myCstr(dr("Cheque No"))
                    dgvVisi.CurrentRow.Cells(colFOC).Value = clsCommon.myCstr(dr("FOC"))
                End If
                If chkInstall.IsChecked Or chkPulloutAndInstall.IsChecked Then
                    Dim drdispatch() As DataRow = DtDispatchDetail.Select("serial_no='" & clsCommon.myCstr(dr("Visi_Id")) & "' and vendor_code='" & fndCustomer.Validate & "'")
                    If drdispatch.Length > 0 Then
                        dgvVisi.CurrentRow.Cells(colAgreementNo).Value = clsCommon.myCstr(drdispatch("agreement_no"))
                        dgvVisi.CurrentRow.Cells(colAgreementDate).Value = clsCommon.myCDate(drdispatch("agreement_date"))
                        dgvVisi.CurrentRow.Cells(colFOCP).Value = clsCommon.myCstr(drdispatch("Foc_sec")).ToUpper
                        dgvVisi.CurrentRow.Cells(colChequeDateP).Value = clsCommon.myCDate(drdispatch("Cheque_date_sec"))
                        dgvVisi.CurrentRow.Cells(colChequeNoP).Value = clsCommon.myCstr(drdispatch("Cheque_no_sec"))
                    Else
                        dgvVisi.CurrentRow.Cells(colAgreementNo).Value = clsCommon.myCstr(dr("Agreement No"))
                        dgvVisi.CurrentRow.Cells(colAgreementDate).Value = dr("Agreement Date")
                        dgvVisi.CurrentRow.Cells(colFOCP).Value = clsCommon.myCstr(dr("Foc_sec")).ToUpper
                        dgvVisi.CurrentRow.Cells(colChequeDateP).Value = dr("Cheque Date Sec")
                        dgvVisi.CurrentRow.Cells(colChequeNoP).Value = clsCommon.myCstr(dr("Cheque No sec"))
                    End If
                End If
                'dgvVisi.CurrentRow.Cells(colLoc).Value = clsCommon.myCstr(dr("Location"))
                'dgvVisi.CurrentRow.Cells(colRoute).Value = clsCommon.myCstr(dr("Route"))

                If dgvVisi.CurrentRow.Cells(colSelect).Value = True Then
                    dgvVisi.CurrentRow.Cells(colTransDate).Value = clsCommon.myCstr(dr("Visi_Installation_date"))
                    dgvVisi.CurrentRow.Cells(colSelect).ReadOnly = True
                    dgvVisi.CurrentRow.Cells(colTransDate).ReadOnly = True
                Else
                    dgvVisi.CurrentRow.Cells(colAssetSize).Value = Nothing
                    dgvVisi.CurrentRow.Cells(colSelect).ReadOnly = False
                    dgvVisi.CurrentRow.Cells(colTransDate).ReadOnly = False
                End If
                If Not chkPullOut.IsChecked Then
                    If clsCommon.myCstr(dgvVisi.CurrentRow.Cells(colAgreementNo).Value) <> "" Then
                        dgvVisi.CurrentRow.Cells(colAgreementNo).ReadOnly = True
                        dgvVisi.CurrentRow.Cells(colAgreementDate).ReadOnly = True
                        dgvVisi.CurrentRow.Cells(colFOCP).ReadOnly = True
                        dgvVisi.CurrentRow.Cells(colChequeNoP).ReadOnly = True
                        dgvVisi.CurrentRow.Cells(colChequeDateP).ReadOnly = True
                        dgvVisi.CurrentRow.Cells(colSecurityAmount).ReadOnly = True
                    Else
                        dgvVisi.CurrentRow.Cells(colAgreementNo).ReadOnly = False
                        dgvVisi.CurrentRow.Cells(colAgreementDate).ReadOnly = False
                        dgvVisi.CurrentRow.Cells(colFOCP).ReadOnly = False
                        dgvVisi.CurrentRow.Cells(colChequeNoP).ReadOnly = False
                        dgvVisi.CurrentRow.Cells(colChequeDateP).ReadOnly = False
                        dgvVisi.CurrentRow.Cells(colSecurityAmount).ReadOnly = False
                    End If
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndCustomer1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer1._MYValidating
        Try
            Qry = "select Cust_Code as [CustomerCode],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(select case when Status='N' then 'Active' else 'Inactive' end ) as [Status] from TSPL_CUSTOMER_MASTER  "
            fndCustomer1.Value = clsCommon.ShowSelectForm("CustFinderInVisiPullout", Qry, "CustomerCode", "", fndCustomer1.Value, "", isButtonClicked)

            Dim strCustcode As String = clsCommon.myCstr(fndCustomer1.Value)
            IsInsideLoadData = True
            dt = clsVisiMaster.GetDataForVisiInstallPullout(strCustcode, NavigatorType.Current)
            If dt.Rows.Count > 0 Then
                fndCustomer1.Value = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                lblCustomerName1.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                lblRoute1.Text = clsCommon.myCstr(dt.Rows(0)("Route_No")) + " - " + clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub

    Private Sub fndCustomer1__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCustomer1._MYNavigator
        Try
            Dim strCustcode As String = clsCommon.myCstr(fndCustomer1.Value)
            IsInsideLoadData = True
            dt = clsVisiMaster.GetDataForVisiInstallPullout(strCustcode, NavType)
            If dt.Rows.Count > 0 Then
                fndCustomer1.Value = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                lblCustomerName1.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                lblRoute1.Text = clsCommon.myCstr(dt.Rows(0)("Route_No")) + " - " + clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub


    Private Sub OpenLocation(ByVal StrCode As String, ByVal IsButtonCLicked As Boolean)
        'Qry = "Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER "
        'dgvVisi.CurrentRow.Cells(colLoc).Value = clsCommon.ShowSelectForm("LocFilter", Qry, "Code", "Location_Type='Physical'", StrCode, "Code", IsButtonCLicked)
    End Sub
    Private Sub OpenRoute(ByVal StrCode As String, ByVal IsButtonCLicked As Boolean)
        'Qry = "Select Route_No as Code, Route_Desc  as Description from TSPL_ROUTE_MASTER"
        'dgvVisi.CurrentRow.Cells(colRoute).Value = clsCommon.ShowSelectForm("RouteFilter", Qry, "Code", "", StrCode, "Code", IsButtonCLicked)
    End Sub

    Private Sub dgvVisi_CellValueChanged_1(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvVisi.CellValueChanged
        'If IsInsideLoadData = False Then
        'Dim strCode As String
        'If e.Column Is dgvVisi.Columns(colLoc) Then
        'strCode = dgvVisi.CurrentRow.Cells(colLoc).Value
        'OpenLocation(strCode, False)
        'ElseIf e.Column Is dgvVisi.Columns(colRoute) Then
        '   strCode = dgvVisi.CurrentRow.Cells(colRoute).Value
        '  OpenRoute(strCode, False)
        'End If
        'End If
        Try
            If Not IsInsideLoadData Then
                If e.Column Is dgvVisi.Columns(colAssetID) Then
                    Dim itmcode As String = clsCustomerMaster.getFinderForAssets(" tt2.[Visi Id] is NULL ", fndCustomer.Value, True)
                    LoadDetails("", "Install", itmcode)
                    dgvVisi.AllowAddNewRow = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkPullOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPullOut.Click

    End Sub

    Private Sub chkInstall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInstall.ToggleStateChanged, chkPullOut.ToggleStateChanged, chkPulloutAndInstall.ToggleStateChanged

        Try

            If chkInstall.IsChecked Then
                RadLabel1.Text = "Install To Customer No."
                dgvVisi.Columns(colInstallDate).IsVisible = False
                dgvVisi.Columns(colTransDate).HeaderText = "Installation Date"
                GroupBox1.Visible = False
                dgvVisi.Location = New Point(4, 3)
                If clsCommon.myLen(fndCustomer.Value) > 0 Then
                    LoadDetails(fndCustomer.Value, "Install")
                    dgvVisi.AllowAddNewRow = True
                End If
                chkDispatchOnly.Visible = True

            ElseIf chkPullOut.IsChecked Then
                RadLabel1.Text = "Pull Out From Customer No."
                GroupBox1.Visible = False
                dgvVisi.Columns(colInstallDate).IsVisible = False
                dgvVisi.Columns(colTransDate).HeaderText = "PullOut  Date"
                dgvVisi.Location = New Point(4, 3)
                If clsCommon.myLen(fndCustomer.Value) > 0 Then
                    LoadDetails(fndCustomer.Value, "Pulout")
                End If

            Else
                If clsCommon.myLen(fndCustomer.Value) > 0 Then
                    LoadDetails(fndCustomer.Value, "Pulout")
                End If
                RadLabel1.Text = "PullOut From Customer No."
                GroupBox1.Visible = True
                dgvVisi.Columns(colTransDate).HeaderText = "PullOut Date"
                dgvVisi.Columns(colInstallDate).IsVisible = True
                dgvVisi.Columns(colInstallDate).HeaderText = "Installation Date"
                dgvVisi.Location = New Point(4, 97)

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub



    Private Sub btnNew1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew1.Click
        Reset()
    End Sub

    Private Sub btnNew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()

    End Sub
    '========================Add Code by Rohit to Import and Export on May 23,2014=========================

    'Private Sub Export_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles All.Click
    '    Dim str As String
    '    ' If chkInstall.IsChecked = True Then
    '    str = "select Customer_Id as [Customer Code],cm.Customer_name  as [Customer Name],Asset_No as [Asset No],Model_No as [Model No],Serial_No as [Asset Id],Asset_Installation_Date as [Installation Date]" _
    '          & " from TSPL_VISI_MASTER vm inner join  TSPL_ASSET_INSTALL_PULLOUT_NEW pn on serial_no=Asset_Id inner join TSPL_CUSTOMER_MASTER cm on cm.Cust_Code=Install_Customer_Id  where Asset_Pullout_Date is not null   "
    '    transportSql.ExporttoExcel(str, Me)
    '    'ElseIf chkPullOut.IsChecked = True Then
    '    str = " select Pullout_Customer_Id as [Customer Code],cm.Customer_name  as [Customer Name],Asset_No as [Asset No],Model_No as [Model No],Serial_No as [Asset Id],Asset_Pullout_Date  as [PullOut Date]" _
    '          & " from TSPL_VISI_MASTER vm inner join  TSPL_ASSET_INSTALL_PULLOUT_NEW pn on serial_no=Asset_Id inner join TSPL_CUSTOMER_MASTER cm on cm.Cust_Code=Pullout_Customer_Id  where Asset_Installation_Date is not null  "
    '    transportSql.ExporttoExcel(str, Me)
    '    ' ElseIf chkPulloutAndInstall.IsChecked = True Then
    '    str = "select Customer_Id as [Customer Code],cm.Customer_name  as [Customer Name],Asset_No as [Asset No],Model_No as [Model No],Serial_No as [Asset Id],Asset_Pullout_Date as [PullOut Date],null as [Installation Date]" _
    '          & " from TSPL_VISI_MASTER vm inner join  TSPL_ASSET_INSTALL_PULLOUT_NEW pn on serial_no=Asset_Id inner join TSPL_CUSTOMER_MASTER cm on cm.Cust_Code=Install_Customer_Id  where Asset_Pullout_Date is not null   " _
    '          & " union " _
    '          & " select Pullout_Customer_Id as [Customer Code],cm.Customer_name  as [Customer Name],Asset_No as [Asset No],Model_No as [Model No],Serial_No as [Asset Id],null,Asset_Installation_Date as [Installation Date]" _
    '          & " from TSPL_VISI_MASTER vm inner join  TSPL_ASSET_INSTALL_PULLOUT_NEW pn on serial_no=Asset_Id inner join TSPL_CUSTOMER_MASTER cm on cm.Cust_Code=Pullout_Customer_Id  where Asset_Installation_Date is not null  "
    '    transportSql.ExporttoExcel(str, Me)
    '    'End If
    'End Sub

    Private Sub Install_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Install.Click
        Dim Str As String = "select Install_Customer_Id as [Customer Code],cm.Customer_name  as [Customer Name],Asset_No as [Asset No],Model_No as [Model No],Serial_No as [Asset Id],Asset_Installation_Date as [Installation Date]" _
             & " from TSPL_VISI_MASTER vm inner join  TSPL_ASSET_INSTALL_PULLOUT_NEW pn on serial_no=Asset_Id inner join TSPL_CUSTOMER_MASTER cm on cm.Cust_Code=Install_Customer_Id where Asset_Installation_Date is not null "
        transportSql.ExporttoExcel(Str, Me)
    End Sub

    Private Sub PullOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PullOut.Click
        Dim Str As String = "select Pullout_Customer_Id as [Customer Code],cm.Customer_name  as [Customer Name],Asset_No as [Asset No],Model_No as [Model No],Serial_No as [Asset Id],Asset_Pullout_Date as [PullOut Date]" _
              & " from TSPL_VISI_MASTER vm inner join  TSPL_ASSET_INSTALL_PULLOUT_NEW pn on serial_no=Asset_Id inner join TSPL_CUSTOMER_MASTER cm on cm.Cust_Code=Pullout_Customer_Id  where Asset_Pullout_Date is not null  "
        transportSql.ExporttoExcel(Str, Me)
    End Sub

    Private Sub PullOutAndInstall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PullOutAndInstall.Click
        Dim Str As String = "select Pullout_Customer_Id as [Customer Code],cm.Customer_name  as [Customer Name],Asset_No as [Asset No],Model_No as [Model No],Serial_No as [Asset Id],Asset_Pullout_Date as [PullOut Date],null as [Installation Date]" _
              & " from TSPL_VISI_MASTER vm inner join  TSPL_ASSET_INSTALL_PULLOUT_NEW pn on serial_no=Asset_Id inner join TSPL_CUSTOMER_MASTER cm on cm.Cust_Code=Pullout_Customer_Id where Asset_Pullout_Date is not null   " _
              & " union " _
              & " select Install_Customer_Id as [Customer Code],cm.Customer_name  as [Customer Name],Asset_No as [Asset No],Model_No as [Model No],Serial_No as [Asset Id],null,Asset_Installation_Date as [Installation Date]" _
              & " from TSPL_VISI_MASTER vm inner join  TSPL_ASSET_INSTALL_PULLOUT_NEW pn on serial_no=Asset_Id inner join TSPL_CUSTOMER_MASTER cm on cm.Cust_Code=Install_Customer_Id   where Asset_Installation_Date is not null  "
        transportSql.ExporttoExcel(Str, Me)
    End Sub

    Function ConvertDate(ByVal in_date As String)
        Dim ss = Convert.ToDateTime(in_date).Day & "-" & MonthName(Convert.ToDateTime(in_date).Month, True) & "-" & Convert.ToDateTime(in_date).Year
        Return ss
    End Function

    '    Private Sub Import_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PullOutAndinstallSheet.Click
    '        Dim gv As New RadGridView()
    '        Me.Controls.Add(gv)
    '        Dim currentdate As Date = Date.Today
    '        'If transportSql.importExcel(gv, "Customer Code", "Customer Name", "Asset No", "Model No", "Asset Id", "Installation Date") Then

    '        If transportSql.importExcel(gv, "Customer Code", "Customer Name", "Asset No", "Model No", "Asset Id", "Installation Date", "PullOut Date") Then
    '            Dim trans As SqlTransaction
    '            Try
    '                trans = clsDBFuncationality.GetTransactin()
    '                clsCommon.ProgressBarShow()
    '                For Each grow As GridViewRowInfo In gv.Rows
    '                    Dim Custcode As String = clsCommon.myCstr(grow.Cells(0).Value)
    '                    If Custcode.Length > 50 Then
    '                        Throw New Exception("Check the length of 'Cust Code'.")
    '                        trans.Rollback()
    '                        Exit Sub
    '                    End If

    '                    Dim CustName As String = clsCommon.myCstr(grow.Cells(1).Value)
    '                    If Custcode = "" And CustName <> "" Then
    '                        Dim dr() As DataRow = DtCustomerMaster.Select("Customer_Name = '" & CustName & "'")
    '                        If dr.Length > 0 Then
    '                            Custcode = dr(0)("Cust_Code")
    '                        End If

    '                    End If
    '                    If Custcode <> "" And CustName = "" Then
    '                        Dim dr() As DataRow = DtCustomerMaster.Select("Cust_Code = '" & Custcode & "'")
    '                        If dr.Length > 0 Then
    '                            CustName = dr(0)("Customer_Name")
    '                        End If

    '                    End If

    '                    Dim CountCustCode As String '= clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Custcode + "'", trans)
    '                    If Not IsNothing(Custcode) Then
    '                        CountCustCode = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Custcode + "'", trans)
    '                    Else
    '                        CountCustCode = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Customer_name='" + CustName + "'", trans)
    '                    End If
    '                    If CountCustCode = "0" OrElse (Custcode Is Nothing And CustName Is Nothing) Then
    '                        Throw New Exception("This  '" + Custcode + "'  Customer does not exist")
    '                        trans.Rollback()
    '                        Exit Sub
    '                    End If


    '                    Dim AssetNo As String = clsCommon.myCstr(grow.Cells(2).Value)
    '                    If AssetNo.Length > 50 Then
    '                        Throw New Exception("Check the length of 'Item Code'.")
    '                        trans.Rollback()
    '                        Exit Sub
    '                    End If

    '                    Dim countAssetCode As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_MASTER where item_DESC='" + AssetNo + "'", trans)
    '                    If countAssetCode = "" OrElse countAssetCode Is Nothing Then
    '                        Throw New Exception("This  '" + AssetNo + "'  Item does not exist")
    '                        trans.Rollback()
    '                        Exit Sub
    '                    End If

    '                    Dim ModelNo As String = clsCommon.myCstr(grow.Cells(3).Value)
    '                    Dim Asset_Id As String = clsCommon.myCstr(grow.Cells(4).Value)
    '                    Dim InstallationDate As String = Nothing
    '                    If grow.Cells(5).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(5).Value) > 0 Then 'And clsCommon.myLen(grow.Cells(5).Value) < 11) Then
    '                        InstallationDate = clsCommon.GetPrintDate((grow.Cells(5).Value), "dd-MM-yyyy")
    '                        ''Else
    '                        ''    Throw New Exception("Please insert Date in Format- i.e. (yyyy-MM-dd)")
    '                    End If

    '                    Dim PulledOutDate As String = Nothing
    '                    If grow.Cells(6).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(6).Value) > 0 Then 'And clsCommon.myLen(grow.Cells(6).Value) < 11) Then
    '                        PulledOutDate = clsCommon.GetPrintDate((grow.Cells(6).Value), "dd-MM-yyyy")
    '                    End If
    '                    If IsNothing(InstallationDate) And IsNothing(PulledOutDate) Then
    '                        MessageBox.Show("Some data dose not Includes Dates (Intallation or Pulled Out)")
    '                        GoTo a
    '                    End If
    '                    Dim sql1 As String
    '                    'If Not IsNothing(InstallationDate) Then
    '                    sql1 = "select count(*) from TSPL_VISI_MASTER where " _
    '                            & "  Serial_No= '" & Asset_Id & "' and Model_No='" + ModelNo + "'"
    '                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
    '                    'End If
    '                    Dim isEntered As Integer = 0
    '                    Dim Outdate As String
    '                    Dim Indate As String
    '                    If Not IsNothing(InstallationDate) Then
    '                        Indate = Convert.ToDateTime(InstallationDate).Day & "-" & MonthName(Convert.ToDateTime(InstallationDate).Month, True) & "-" & Convert.ToDateTime(InstallationDate).Year
    '                        sql1 = "select count(*) from TSPL_ASSET_INSTALL_PULLOUT_NEW where Asset_Installation_Date =  '" + Indate + "' and Install_Customer_Id='" + Custcode + "' and asset_id='" + Asset_Id + "' "
    '                    Else
    '                        Outdate = Convert.ToDateTime(PulledOutDate).Day & "-" & MonthName(Convert.ToDateTime(PulledOutDate).Month) & "-" & Convert.ToDateTime(PulledOutDate).Year
    '                        sql1 = "select count(*) from TSPL_ASSET_INSTALL_PULLOUT_NEW where Asset_Pullout_Date ='" + Outdate + "' and Pullout_Customer_Id='" + Custcode + "' and asset_id='" + Asset_Id + "' "
    '                    End If
    '                    isEntered = CInt(clsDBFuncationality.getSingleValue(sql1, trans))

    '                    If (i > 0) Then
    '                        If isEntered = 0 Then
    '                            If Not IsNothing(InstallationDate) Then
    '                                Dim qry As String = "insert into TSPL_ASSET_INSTALL_PULLOUT_NEW(Asset_Id,Install_Customer_Id ,Trans_type,Trans_date ,Asset_Installation_Date,Created_by,Created_date,Modified_By,Modified_date)" _
    '                                & " values('" + Asset_Id + "','" + Convert.ToString(Custcode) + "','Installed','" + ConvertDate(Today.Date.ToString) + "', '" + Indate + "','" + objCommonVar.CurrentUser + "','" + ConvertDate(Today.Date.ToString) + "','" + objCommonVar.CurrentUser + "','" + ConvertDate(Today.Date.ToString) + "')"
    '                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '                            Else
    '                                Dim qry As String = "insert into TSPL_ASSET_INSTALL_PULLOUT_NEW(Asset_Id,Pullout_Customer_Id ,Trans_type,Trans_date ,Asset_Pullout_date,Created_by,Created_date,Modified_By,Modified_date)" _
    '                                & " values('" + Asset_Id + "','" + Convert.ToString(Custcode) + "','PulledOut','" + ConvertDate(Today.Date.ToString) + "','" + Outdate + "','" + objCommonVar.CurrentUser + "','" + ConvertDate(Today.Date.ToString) + "','" + objCommonVar.CurrentUser + "','" + ConvertDate(Today.Date.ToString) + "')"
    '                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

    '                            End If
    '                            Dim sQry As String = "update TSPL_VISI_MASTER set Customer_Id = '" & Custcode & "' and Customer_name='" & CustName & "' where Serial_No='" & Asset_Id & "' and Model_No='" & ModelNo & "' and Asset_No='" & AssetNo & "'"
    '                            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
    '                        End If
    '                        'Else
    '                        '    Dim qry As String = "update TSPL_VISI_MASTER set Customer_Id = '" & Custcode & "' and Customer_name='" & CustName & "' where Serial_No='" & Asset_Id & "' and Asset_No='" & AssetNo & "' and Model_No='" & ModelNo & "'"
    '                        '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '                    End If
    'a:              Next
    '                trans.Commit()
    '                clsCommon.ProgressBarHide()
    '                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
    '            Catch ex As Exception
    '                trans.Rollback()
    '                clsCommon.ProgressBarHide()
    '                myMessages.myExceptions(ex)
    '            Finally
    '                clsCommon.ProgressBarHide()
    '            End Try

    '        End If
    '        Me.Controls.Remove(gv)
    '    End Sub

    Private Sub InstallSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallSheet.Click
        DtAssetInstallPullOutData = clsDBFuncationality.GetDataTable("select asset_id,Install_Customer_Id,Asset_Installation_Date,coalesce(Asset_Pullout_Date,(select MIN(Asset_Pullout_Date) " _
               & " from TSPL_ASSET_INSTALL_PULLOUT_NEW tapns where tapn.Install_Customer_Id=tapns.Pullout_Customer_Id and " _
               & " tapn.Asset_Id=tapns.Asset_Id and tapns.Asset_Pullout_Date>tapn.Asset_Installation_Date and " _
               & " tapn.Trans_Type<>'Both'))AS [Asset_PullOut_date] from TSPL_ASSET_INSTALL_PULLOUT_NEW tapn order by Trans_Type")
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Customer Code", "Customer Name", "Asset No", "Model No", "Asset Id", "Installation Date") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                If IsExcelValid(5, gv) = False Then
                    Exit Sub
                End If
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim Custcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If Custcode.Length > 50 Then
                        Throw New Exception("Check the length of 'Cust Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim CustName As String = clsCommon.myCstr(grow.Cells(1).Value)

                    If Custcode = "" And CustName <> "" Then
                        Custcode = clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER where Customer_Name like '% " & CustName & " %'")
                    End If
                    If Custcode <> "" And CustName = "" Then
                        CustName = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code like '% " & Custcode & " %'")
                    End If

                    Dim CountCustCode As String '= clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Custcode + "'", trans)
                    If Not IsNothing(Custcode) Then
                        CountCustCode = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Custcode + "'", trans)
                    Else
                        CountCustCode = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Customer_name='" + CustName + "'", trans)
                    End If
                    If CountCustCode = "" OrElse (Custcode Is Nothing And CustName Is Nothing) Then
                        Throw New Exception("This  '" + Custcode + "'  Customer does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If


                    Dim AssetNo As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If AssetNo.Length > 50 Then
                        Throw New Exception("Check the length of 'Item Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim countAssetCode As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_MASTER where item_DESC='" + AssetNo + "'", trans)
                    If countAssetCode = "" OrElse countAssetCode Is Nothing Then
                        Throw New Exception("This  '" + AssetNo + "'  Item does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim ModelNo As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim Asset_Id As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim InstallationDate As String = Nothing
                    If grow.Cells(5).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(5).Value) > 0 Then 'And clsCommon.myLen(grow.Cells(5).Value) < 11) Then
                        InstallationDate = clsCommon.GetPrintDate((grow.Cells(5).Value), "dd-MM-yyyy")
                        ''Else
                        ''    Throw New Exception("Please insert Date in Format- i.e. (yyyy-MM-dd)")
                    End If


                    If IsNothing(InstallationDate) Then
                        MessageBox.Show("Some data dose not Includes Dates (Intallation)")
                        GoTo a
                    End If
                    Dim dr() As DataRow = DtAssetInstallPullOutData.Select("Asset_Installation_Date<='" & ConvertDate(InstallationDate) & "' and Asset_PullOut_date>='" & ConvertDate(InstallationDate) & "' and  Asset_id='" & Asset_Id & "' and Install_Customer_Id<>'" & Custcode & "'")
                    If dr.Length > 0 Then
                        MessageBox.Show("This Asset [" & ModelNo & "] on Date [" & ConvertDate(InstallationDate) & "]" _
                                        & " is Already Assigned To [" & dr(0)("Install_Customer_Id") & ",Please Recheck Data..]")
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    Dim sql1 As String
                    'If Not IsNothing(InstallationDate) Then
                    sql1 = "select count(*) from TSPL_VISI_MASTER where " _
                            & "  Serial_No= '" & Asset_Id & "' and Model_No='" + ModelNo + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                    'End If
                    Dim isEntered As Integer = 0
                    Dim ss = Convert.ToDateTime(InstallationDate).Day & "-" & MonthName(Convert.ToDateTime(InstallationDate).Month, True) & "-" & Convert.ToDateTime(InstallationDate).Year
                    sql1 = "select count(*) from TSPL_ASSET_INSTALL_PULLOUT_NEW where Asset_Installation_Date = '" + ss + "'" _
                    & " and Install_Customer_Id='" + Custcode + "' and asset_id='" + Asset_Id + "'"
                    isEntered = CInt(clsDBFuncationality.getSingleValue(sql1, trans))

                    If (i > 0) Then
                        If isEntered = 0 Then
                            Dim qry As String = "insert into TSPL_ASSET_INSTALL_PULLOUT_NEW(Asset_Id,Install_Customer_Id ,Trans_type,Trans_date ,Asset_Installation_Date,Created_by,Created_date,Modified_By,Modified_date)" _
                              & " values('" + Asset_Id + "','" + Convert.ToString(Custcode) + "','Installed','" + ConvertDate(Today.Date.ToString) + "', '" + ss + "','" + objCommonVar.CurrentUser + "','" + ConvertDate(Today.Date.ToString) + "','" + objCommonVar.CurrentUser + "','" + ConvertDate(Today.Date.ToString) + "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            Dim sQry As String = "update TSPL_VISI_MASTER set Customer_Id = '" & Custcode & "' and Customer_name='" & CustName & "' where Serial_No='" & Asset_Id & "' and Model_No='" & ModelNo & "' and Asset_No='" & AssetNo & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If

                        'Else
                        '    Dim qry As String = "update TSPL_VISI_MASTER set Customer_Id = '" & Custcode & "' and Customer_name='" & CustName & "' where Serial_No='" & Asset_Id & "' and Asset_No='" & AssetNo & "' and Model_No='" & ModelNo & "'"
                        '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
a:              Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub PullOutSheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PullOutSheet.Click
        DtAssetInstallPullOutData = clsDBFuncationality.GetDataTable("select asset_id,Install_Customer_Id,Asset_Installation_Date,coalesce(Asset_Pullout_Date,(select MIN(Asset_Pullout_Date) " _
                   & " from TSPL_ASSET_INSTALL_PULLOUT_NEW tapns where tapn.Install_Customer_Id=tapns.Pullout_Customer_Id and " _
                   & " tapn.Asset_Id=tapns.Asset_Id and tapns.Asset_Pullout_Date>tapn.Asset_Installation_Date and " _
                   & " tapn.Trans_Type<>'Both'))AS [Asset_PullOut_date] from TSPL_ASSET_INSTALL_PULLOUT_NEW tapn order by Trans_Type")
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        'If transportSql.importExcel(gv, "Customer Code", "Customer Name", "Asset No", "Model No", "Asset Id", "Installation Date") Then

        If transportSql.importExcel(gv, "Customer Code", "Customer Name", "Asset No", "Model No", "Asset Id", "PullOut Date") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                If IsExcelValid(6, gv) = False Then
                    Exit Sub
                End If
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim Custcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If Custcode.Length > 50 Then
                        Throw New Exception("Check the length of 'Cust Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim CustName As String = clsCommon.myCstr(grow.Cells(1).Value)

                    If Custcode = "" And CustName <> "" Then
                        Custcode = clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER where Customer_Name like '% " & CustName & " %'")
                    End If
                    If Custcode <> "" And CustName = "" Then
                        CustName = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code like '% " & Custcode & " %'")
                    End If

                    Dim CountCustCode As String '= clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Custcode + "'", trans)
                    If Not IsNothing(Custcode) Then
                        CountCustCode = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Custcode + "'", trans)
                    Else
                        CountCustCode = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Customer_name='" + CustName + "'", trans)
                    End If
                    If CountCustCode = "" OrElse (Custcode Is Nothing And CustName Is Nothing) Then
                        Throw New Exception("This  '" + Custcode + "'  Customer does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If


                    Dim AssetNo As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If AssetNo.Length > 50 Then
                        Throw New Exception("Check the length of 'Item Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim countAssetCode As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_MASTER where item_DESC='" + AssetNo + "'", trans)
                    If countAssetCode = "" OrElse countAssetCode Is Nothing Then
                        Throw New Exception("This  '" + AssetNo + "'  Item does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim ModelNo As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim Asset_Id As String = clsCommon.myCstr(grow.Cells(4).Value)

                    Dim PulledOutDate As String = Nothing
                    If grow.Cells(5).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(5).Value) > 0 Then 'And clsCommon.myLen(grow.Cells(6).Value) < 11) Then
                        PulledOutDate = clsCommon.GetPrintDate((grow.Cells(5).Value), "dd-MM-yyyy")
                    End If
                    If IsNothing(PulledOutDate) Then
                        MessageBox.Show("Some data does not Includes Dates (Intallation or Pulled Out)")
                        GoTo a
                    End If
                    Dim sql1 As String

                    Dim dr() As DataRow = DtAssetInstallPullOutData.Select("Asset_Pullout_Date<='" & ConvertDate(PulledOutDate) & "' and Asset_PullOut_date>='" & ConvertDate(PulledOutDate) & "' and  Asset_id='" & Asset_Id & "' and Install_Customer_Id<>'" & Custcode & "'")
                    If dr.Length > 0 Then
                        MessageBox.Show("This Asset [" & ModelNo & "] on Date [" & ConvertDate(PulledOutDate) & "]" _
                                        & " is Already Assigned To [" & dr(0)("Install_Customer_Id") & ",Please Recheck Data..]")
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If

                    'If Not IsNothing(InstallationDate) Then
                    sql1 = "select count(*) from TSPL_VISI_MASTER where " _
                            & "  Serial_No= '" & Asset_Id & "' and Model_No='" + ModelNo + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                    'End If
                    Dim isEntered As Integer = 0

                    Dim Outdate = Convert.ToDateTime(PulledOutDate).Day & "-" & MonthName(Convert.ToDateTime(PulledOutDate).Month) & "-" & Convert.ToDateTime(PulledOutDate).Year
                    sql1 = "select count(*) from TSPL_ASSET_INSTALL_PULLOUT_NEW where Asset_Pullout_Date =  '" + Outdate + "' and Pullout_Customer_Id='" + Custcode + "' and asset_id='" + Asset_Id + "' "
                    isEntered = CInt(clsDBFuncationality.getSingleValue(sql1, trans))

                    If (i > 0) Then
                        If isEntered = 0 Then
                            Dim qry As String = "insert into TSPL_ASSET_INSTALL_PULLOUT_NEW(Asset_Id,Pullout_Customer_Id ,Trans_type,Trans_date ,Asset_Pullout_date,Created_by,Created_date,Modified_By,Modified_date)" _
                       & " values('" + Asset_Id + "','" + Convert.ToString(Custcode) + "','PulledOut','" + ConvertDate(Today.Date.ToString) + "','" + Outdate + "','" + objCommonVar.CurrentUser + "','" + ConvertDate(Today.Date.ToString) + "','" + objCommonVar.CurrentUser + "','" + ConvertDate(Today.Date.ToString) + "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            Dim sQry As String = "update TSPL_VISI_MASTER set Customer_Id = '" & Custcode & "' and Customer_name='" & CustName & "' where Serial_No='" & Asset_Id & "' and Model_No='" & ModelNo & "' and Asset_No='" & AssetNo & "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    End If
a:              Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Public Function IsExcelValid(ByVal LastColIndex As Integer, ByVal gvExcel As RadGridView)
        Dim sb As New System.Text.StringBuilder
        For Each row As GridViewRowInfo In gvExcel.Rows()
            For ix As Integer = 0 To LastColIndex
                If IsNothing(row.Cells(ix).Value) Then
                    For iy As Integer = 0 To LastColIndex
                        sb.AppendLine(String.Join(",", row.Cells(iy).Value))
                        sb.AppendLine(vbNewLine)
                    Next
                End If
            Next
        Next
        If sb.Length > 0 Then
            If MessageBox.Show("Some Data is not correct in Excel .Do You want to import incorrect Data.?", "Excel Sheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim sfd1 As New SaveFileDialog
                sfd1.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
                If sfd1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    IO.File.WriteAllText(IO.Path.Combine(Application.StartupPath, sfd1.FileName), sb.ToString)
                    If MessageBox.Show("Do You want to import correct Data.?", "Excel Sheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
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
        End If
        Return True
    End Function
    '============================================================================================================

    Private Sub chkDispatchOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDispatchOnly.CheckedChanged
        If chkDispatchOnly.Checked Then
            If clsCommon.myLen(fndCustomer.Value) > 0 Then
                LoadDetails(fndCustomer.Value, "Install")
            Else
                clsCommon.MyMessageBoxShow("Please Select The Customer to Install First")
                chkDispatchOnly.Checked = False
                fndCustomer.Focus()
            End If
        Else

            LoadDetails(fndCustomer.Value, "Install")
        End If

    End Sub
    Private Sub RMInstallSheetAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMInstallSheetAll.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            If transportSql.importExcel(gv1, "S N", "REGION", "STATE", "CITY", "ZM/ASM", "SERVICE EXECUTIVE", "VENDOR", "CUSTOMER", "customer code", "RETAIL /VENDING", "ADDRESS", "LANDMARK", "CONT PRESON", "CONT NO", "SECURITY/FOC", "DD/CHEQUE NO", "AMOUNT", "YEAR", "M/C SR NO", "Type of M/C", "M/C MAKE", "DF CAPACITY", "M/C TAG NO", "AGREEMENT NO") Then

                clsCommon.ProgressBarShow()
                ' Try
                Dim strCustomerName As String = ""
                Dim strCustomerAddress As String = ""
                Dim strtagNo As String = ""
                Dim strSRNo As String = ""
                Dim strAssetType As String = ""
                Dim strFOCsec As String = ""
                Dim strAssetSize As String = ""
                Dim strCustomerCode As String = ""
                Dim strAssetID As String = ""
                Dim Amount As Decimal = 0

                Dim check As Integer = 0
                Dim counter As Integer = 2
                For Each grow As GridViewRowInfo In gv1.Rows
                    'strCustomerName = clsCommon.myCstr(grow.Cells("CUSTOMER").Value).Replace("'", "''")
                    'strCustomerAddress = clsCommon.myCstr(grow.Cells("ADDRESS").Value).Replace("'", "''")
                    'If clsCommon.myLen(strCustomerName) <= 0 Or clsCommon.myLen(strCustomerAddress) <= 0 Then
                    '    Throw New Exception("Please fill Customer/Address at line no. " + clsCommon.myCstr(counter) + "")
                    'Else
                    '    Qry = "Select Count(*) from TSPL_CUSTOMER_MASTER where Customer_Name='" & strCustomerName & "' and Add1='" & strCustomerAddress & "'"
                    '    check = clsDBFuncationality.getSingleValue(Qry, trans)
                    '    If check <= 0 Then
                    '        Throw New Exception("Customer not exist in customer master main data,see at line no. " + clsCommon.myCstr(counter) + "")
                    '    End If
                    'End If
                    strCustomerCode = clsCommon.myCstr(grow.Cells("Customer code").Value).Replace("'", "''")
                    If clsCommon.myLen(strCustomerCode) <= 0 Then
                        Throw New Exception("Please fill Customer code at line no. " + clsCommon.myCstr(counter) + "")
                    Else
                        Qry = "Select Count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomerCode & "' "
                        check = clsDBFuncationality.getSingleValue(Qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Customer not exist in customer master main data,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If


                    strtagNo = clsCommon.myCstr(grow.Cells("M/C TAG NO").Value).Replace("'", "''")
                    strSRNo = clsCommon.myCstr(grow.Cells("M/C SR NO").Value).Replace("'", "''")
                    If clsCommon.myLen(strtagNo) <= 0 Or clsCommon.myLen(strSRNo) <= 0 Then
                        Throw New Exception("Please fill M/C TAG NO/M/C SR NO at line no. " + clsCommon.myCstr(counter) + "")
                    Else
                        Qry = "Select COUNT(*) from TSPL_VISI_MASTER where Serial_No='" & strSRNo & "' or Tag_No='" & strtagNo & "'"
                        check = clsDBFuncationality.getSingleValue(Qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Asset ID not exist in asset master main data regarding M/C TAG NO and M/C SR NO,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    strAssetSize = clsCommon.myCstr(grow.Cells("DF CAPACITY").Value)
                    ' strAssetType = clsCommon.myCstr(grow.Cells("Type of M/C").Value)
                    'strCustomerCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER where Customer_Name='" & strCustomerName & "' and Add1='" & strCustomerAddress & "'", trans))
                    strAssetID = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Visi_Id from TSPL_VISI_MASTER where Serial_No='" & strSRNo & "' or  Tag_No='" & strtagNo & "'", trans))
                    Dim strAssetNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Asset_No from TSPL_VISI_MASTER where Visi_Id='" & strAssetID & "'", trans))

                    If clsCommon.myLen(strAssetNo) <= 0 Then
                        Throw New Exception("No item found in item master master main data regarding M/C TAG NO and M/C SR NO,see at line no. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strTransType As String = "Installed"
                    strAssetType = "New"

                    Dim strAssetInstallationDate As String = clsCommon.myCstr(grow.Cells("YEAR").Value)
                    'If clsCommon.CompairString(clsCommon.myCstr(counter), "1609") = CompairStringResult.Equal Then
                    '    strAssetInstallationDate = "01-01-" + strAssetInstallationDate
                    '    If IsDate(strAssetInstallationDate) Then
                    '    Else
                    '        Throw New Exception("Date is incorrect format,see at line no. " + clsCommon.myCstr(counter) + "")
                    '    End If
                    'End If

                    strAssetInstallationDate = "01-01-" + strAssetInstallationDate
                    If IsDate(strAssetInstallationDate) Then
                    Else
                        Throw New Exception("Date is incorrect format,see at line no. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim strAgreementNo As String = clsCommon.myCstr(grow.Cells("AGREEMENT NO").Value)
                    Dim strFoc As String = clsCommon.myCstr(grow.Cells("SECURITY/FOC").Value).ToUpper()

                    If clsCommon.CompairString(strFoc, "FOC") = CompairStringResult.Equal Then
                        strFOCsec = "YES"
                    Else
                        strFOCsec = "NO"
                    End If
                    Dim strChequeNo As String = clsCommon.myCstr(grow.Cells("DD/CHEQUE NO").Value)

                    If clsCommon.myLen(grow.Cells("AMOUNT").Value) > 0 Then
                        If IsNumeric(grow.Cells("AMOUNT").Value) Then
                            Amount = clsCommon.myCdbl(grow.Cells("AMOUNT").Value)
                        Else
                            Throw New Exception("Amount should be numeric,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    ' Dim strAgreementNo As String = clsCommon.myCstr(grow.Cells("AGREEMENT NO").Value)

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Install_Customer_Id", strCustomerCode)
                    clsCommon.AddColumnsForChange(coll, "Asset_Installation_Date", clsCommon.GetPrintDate(strAssetInstallationDate, "dd/MMM/yyyy"))

                    clsCommon.AddColumnsForChange(coll, "agreement_no", strAgreementNo)
                    clsCommon.AddColumnsForChange(coll, "agreement_date", clsCommon.GetPrintDate(strAssetInstallationDate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "FOC_sec", strFOCsec)
                    clsCommon.AddColumnsForChange(coll, "Item_code", strAssetNo)
                    If clsCommon.myCstr(strChequeNo).ToUpper() <> "NA" AndAlso clsCommon.myLen(strChequeNo) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "cheque_no_sec", strChequeNo)
                        If clsCommon.myCstr(strAssetInstallationDate) <> "" Then
                            clsCommon.AddColumnsForChange(coll, "cheque_date_sec", clsCommon.GetPrintDate(strAssetInstallationDate, "dd/MMM/yyyy"))
                        End If
                    End If

                    clsCommon.AddColumnsForChange(coll, "sec_Amount", Amount)

                    clsCommon.AddColumnsForChange(coll, "Trans_Type", strTransType)
                    clsCommon.AddColumnsForChange(coll, "Asset_Type", strAssetType)
                    clsCommon.AddColumnsForChange(coll, "Trans_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    'clsCommon.AddColumnsForChange(coll, "Customer_Name", clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_Id + "'", trans)))
                    'clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                    'clsCommon.AddColumnsForChange(coll, "Route", obj.Route)
                    'clsCommon.AddColumnsForChange(coll, "Asset_Make", obj.AssetMake)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim InsCust As String = ""
                    Dim PulCst As String = ""
                    Dim puldt As String = ""
                    Dim insdt As String = ""
                    PulCst = ""
                    puldt = ""
                    InsCust = " and Install_Customer_Id='" & strCustomerCode & "'"
                    insdt = " and Asset_Installation_Date='" & clsCommon.GetPrintDate(strAssetInstallationDate, "dd/MMM/yyyy") & "'"

                    Dim whrcls As String = "Asset_Id='" + strAssetID + "'  AND Trans_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' AND Trans_Type='" + strTransType + "' " + PulCst + InsCust + insdt + puldt
                    Qry = "Select COunt(*) from TSPL_ASSET_INSTALL_PULLOUT_NEW WHERE " + whrcls + ""
                    If strAssetID <> "" Then
                        whrcls = "Item_code ='" & strAssetNo & "' and Asset_Id='" + strAssetID + "'  AND Trans_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' AND Trans_Type='" + strTransType + "' " + PulCst + InsCust + insdt + puldt
                    End If
                    If clsDBFuncationality.getSingleValue(Qry, trans) <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Asset_Id", strAssetID)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                        clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDb, "TSPL_ASSET_INSTALL_PULLOUT_NEW", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDb, "TSPL_ASSET_INSTALL_PULLOUT_NEW", OMInsertOrUpdate.Update, whrcls, trans)
                    End If

                    '-------------------------Updates Data In Visi Master-------------------------------------------
                    Dim coll1 As New Hashtable()
                    If (clsCommon.CompairString(strAssetType, "New") = CompairStringResult.Equal) Then
                        clsCommon.AddColumnsForChange(coll1, "Type", "N")
                    End If
                    If clsCommon.CompairString(strTransType, "Installed") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll1, "Customer_Id", strCustomerCode)
                        clsCommon.AddColumnsForChange(coll1, "Customer_Name", clsDBFuncationality.getSingleValue("select customer_Name from tspl_customer_master where cust_code='" & strCustomerCode & "'", trans))
                        clsCommon.AddColumnsForChange(coll1, "visi_Installation_date", clsCommon.GetPrintDate(clsCommon.myCDate(strAssetInstallationDate), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll1, "Pull_Out_Date", Nothing, True)
                        clsCommon.AddColumnsForChange(coll1, "agreement_no", strAgreementNo)
                        If clsCommon.myCstr(strChequeNo) <> "NA" AndAlso clsCommon.myLen(strChequeNo) > 0 Then
                            clsCommon.AddColumnsForChange(coll1, "CHEQUE_NO", strChequeNo)
                            clsCommon.AddColumnsForChange(coll1, "CHEQUE_DATE", clsCommon.GetPrintDate(strAssetInstallationDate, "dd/MMM/yyyy"))
                        End If

                        clsCommon.AddColumnsForChange(coll1, "Security_Amount", Amount)

                    End If

                    clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll1, ArrDb, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_id = '" + strAssetID + "' and TSPL_VISI_MASTER.Asset_No = '" + strAssetNo + "'", trans)
                    '------------------------------------------------------------------------------------------------
                    counter += 1
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Data imported Successfully", Me.Text)
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        Me.Controls.Remove(gv1)
    End Sub
End Class

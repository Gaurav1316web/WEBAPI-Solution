''''18/06/2012---Updation by --[Pankaj kumar]-- Commented Grid Formating Event Code So that Grid Cell's Color Could Not be change on clicking 
'''' Updated By abhishek as on 9/01/2013 1:13pm For Save Data Before Posting
'''''by vipin on 04/02/2013 for posting check on update  
'12/09/2013--Version--2.0.2.32---Updation by --[Pankaj kumar]-- Added functionality for creating Empty VCGL
'---preeti Gupta---Ticket No.-BM00000003015--01/07/2014
''updation by richa agarwal against ticket no BM00000003745(add condition in customer finder),BM00000007466
Imports common
Imports System.Data.SqlClient

Public Class frmVCGLEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Public strAPInvoice As String = Nothing
    Private isCellValueChangedOpen As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Dim AllowTransferVSPAmtToFarmerinVCGL As Boolean = False
    Private objRemittance As clsRemittance
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colLineNo As String = "LNO"
    Const colRowType As String = "ROWType"
    Const colVCGLCode As String = "VCGLCODE"
    Const colVCGLName As String = "VCGLNAME"
    Const colDrAmt As String = "DRAMT"
    Const colCrAmt As String = "CRAMT"
    Const colACCode As String = "NAME"
    Const colACName As String = "QTY"
    Const colRemarks As String = "REMARKS"
    Public IsFormLoad As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Public strDocumentNo As String = ""
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnVCGLEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        RadMenu1.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("FarmerInVendor", "varchar(20) null")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_VCGL_Head", coll, Nothing, True, False, "", "Document_No", "Document_Date")

        ''If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        ''    If funSetUserAccess() = False Then Exit Sub
        ''End If
        AllowTransferVSPAmtToFarmerinVCGL = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowTransferVSPAmtToFarmerinVCGL, clsFixedParameterCode.AllowTransferVSPAmtToFarmerinVCGL, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        'txtDocNo.MyReadOnly = True
        MyLabel2.Visible = False
        txtPostDate.Visible = False
        LoadBlankGrid()
        IsFormLoad = True
        LoadVCType()
        IsFormLoad = False
        cboDocType.SelectedIndex = 1
        AddNew()
        If clsCommon.myLen(strAPInvoice) > 0 Then
            LoadData(strAPInvoice)
        End If

        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        SetLength()
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag))
        End If
    End Sub

    Public Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtRemarks.MaxLength = 200
        txtDesc.MaxLength = 500
    End Sub



    Sub LoadVCType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "V"
        dr("Name") = "Vendor"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Customer"
        dt.Rows.Add(dr)

        If AllowTransferVSPAmtToFarmerinVCGL Then
            dr = dt.NewRow()
            dr("Code") = "F"
            dr("Name") = "Farmer"
            dt.Rows.Add(dr)
        End If

        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtRemarks.Text = ""
        chkOnHold.Checked = False
        TxtVendorNo.Value = ""
        lblVendorName.Text = ""
        cboDocType.SelectedIndex = 0
        lblTotDrAmt.Text = ""
        lblTotCrAmt.Text = ""
        lblTotRAmt.Text = ""
        lblAmtType.Text = ""
        txtDesc.Text = ""
        txtDataAndTimeSelection.Value = clsCommon.GETSERVERDATE()
        txtTapalNo.Text = ""
        txtDataAndTimeSelection.Checked = False
    End Sub

    Private Sub TxtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtVendorNo._MYValidating
        Try
            '' Anubhooti 13-Mar-2015 (Fetch Alies Name On Vendor Finder)
            Dim Qry As String
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "V") = CompairStringResult.Equal Then
                Qry = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name]  from TSPL_VENDOR_MASTER"
                TxtVendorNo.Value = clsCommon.ShowSelectForm("VCGLSelector1", Qry, "Code", " Status ='N'", TxtVendorNo.Value, "Code", isButtonClicked)
                lblVendorName.Text = clsDBFuncationality.getSingleValue("select  Vendor_Name  from TSPL_VENDOR_MASTER where Vendor_Code='" + TxtVendorNo.Value + "'")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "F") = CompairStringResult.Equal Then
                Qry = "select tspl_mp_master.MP_Code as Code ,tspl_mp_master.MP_Name as Name,TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code] ,VLC_Name as [VLC Name] from tspl_mp_master left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =tspl_mp_master.VLC_Code "
                TxtVendorNo.Value = clsCommon.ShowSelectForm("VCGLSelector1", Qry, "Code", "", TxtVendorNo.Value, "Code", isButtonClicked)
                lblVendorName.Text = clsDBFuncationality.getSingleValue("select  tspl_mp_master.MP_Name  from tspl_mp_master where MP_Code='" + TxtVendorNo.Value + "'")
                txtFarmerVendor.Value = ""
                lblFarmerVendor.Text = ""
            Else
                Qry = "select Cust_Code as Code,Customer_Name as Name,ISNULL(TSPL_CUSTOMER_MASTER.alies_name,'') As [Alies Name]   from TSPL_CUSTOMER_MASTER"
                TxtVendorNo.Value = clsCommon.ShowSelectForm("VCGLSelector2", Qry, "Code", "Status ='N' AND OnHold='N'", TxtVendorNo.Value, "Code", isButtonClicked)
                lblVendorName.Text = clsDBFuncationality.getSingleValue("select  Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code='" + TxtVendorNo.Value + "'")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function GetRowType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = Nothing

        'If Not chkEmpty.Checked Then
        dr = dt.NewRow()
        dr("Code") = "Vendor"
        dt.Rows.Add(dr)
        'End If

        dr = dt.NewRow()
        dr("Code") = "Customer"
        dt.Rows.Add(dr)

        If AllowTransferVSPAmtToFarmerinVCGL Then
            dr = dt.NewRow()
            dr("Code") = "Farmer"
            dt.Rows.Add(dr)
        End If

        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "F") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "GL"
            dt.Rows.Add(dr)
        End If


        Return dt

    End Function

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)


        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Type"
        repoRowType.Name = colRowType
        repoRowType.Width = 80
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

        gv1.MasterTemplate.Columns.Add(repoRowType)

        Dim repoVCGLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCGLCode.FormatString = ""
        repoVCGLCode.HeaderText = "Code"
        repoVCGLCode.Name = colVCGLCode
        repoVCGLCode.HeaderImage = My.Resources.search4
        repoVCGLCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoVCGLCode.Width = 100
        repoVCGLCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoVCGLCode)

        Dim repoVCGLName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCGLName.FormatString = ""
        repoVCGLName.HeaderText = "Name"
        repoVCGLName.Name = colVCGLName
        repoVCGLName.Width = 250
        repoVCGLName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVCGLName)

        Dim repoDrAmt As GridViewCalculatorColumn = New GridViewCalculatorColumn()
        repoDrAmt = New GridViewCalculatorColumn()
        repoDrAmt.FormatString = ""
        repoDrAmt.HeaderText = "Debit Amount"
        'repoDrAmt.Minimum = 0
        repoDrAmt.Name = colDrAmt
        repoDrAmt.Width = 100
        'repoDrAmt.ShowUpDownButtons = False
        'repoDrAmt.Step = 0
        repoDrAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDrAmt)


        Dim repoCrAmt As GridViewCalculatorColumn = New GridViewCalculatorColumn()
        repoCrAmt = New GridViewCalculatorColumn()
        repoCrAmt.FormatString = ""
        repoCrAmt.HeaderText = "Credit Amount"
        repoCrAmt.Name = colCrAmt
        repoCrAmt.Width = 100
        'repoCrAmt.Minimum = 0
        'repoDrAmt.ShowUpDownButtons = False
        'repoDrAmt.Step = 0
        repoCrAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCrAmt)

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "GL Account"
        repoACCode.Name = colACCode
        repoACCode.ReadOnly = True
        repoACCode.Width = 70
        gv1.MasterTemplate.Columns.Add(repoACCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "GL Description"
        repoACName.Name = colACName
        repoACName.ReadOnly = True
        repoACName.Width = 200
        gv1.MasterTemplate.Columns.Add(repoACName)


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.ReadOnly = False
        repoRemarks.Width = 200
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colRowType) Then
                        If clsCommon.myLen(txtLocSegment.Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please select Location Segment", Me.Text)
                            gv1.CurrentRow.Cells(colRowType).Value = Nothing
                            txtLocSegment.Focus()
                            Exit Sub
                        End If
                        gv1.CurrentRow.Cells(colVCGLCode).Value = Nothing
                        gv1.CurrentRow.Cells(colVCGLName).Value = Nothing
                        gv1.CurrentRow.Cells(colDrAmt).Value = Nothing
                        gv1.CurrentRow.Cells(colCrAmt).Value = Nothing
                        gv1.CurrentRow.Cells(colACCode).Value = Nothing
                        gv1.CurrentRow.Cells(colACName).Value = Nothing
                    ElseIf e.Column Is gv1.Columns(colVCGLCode) Then
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colRowType).Value) < 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please select Row Type", Me.Text)
                            gv1.CurrentRow.Cells(colVCGLCode).Value = Nothing
                            Exit Sub
                        End If
                        OpenVCGLAccount(False)
                    ElseIf e.Column Is gv1.Columns(colDrAmt) Then
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) < 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Account Code not found for enter Amount", Me.Text)
                            gv1.CurrentRow.Cells(colDrAmt).Value = 0
                        End If
                        gv1.CurrentRow.Cells(colCrAmt).Value = 0
                        UpdateAllTotals()
                    ElseIf e.Column Is gv1.Columns(colCrAmt) Then
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) < 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Account Code not found for enter Amount", Me.Text)
                            gv1.CurrentRow.Cells(colCrAmt).Value = 0
                        End If
                        gv1.CurrentRow.Cells(colDrAmt).Value = 0
                        UpdateAllTotals()
                    End If
                    setGridFocus()
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
    End Sub

    Private Sub OpenVCGLAccount(ByVal isButtonClick As Boolean)
        Dim qry As String = String.Empty
        Dim whrcls As String = String.Empty
        Dim dt As DataTable = Nothing
        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), "Vendor") = CompairStringResult.Equal Then
            qry = "select Vendor_Code as Code,Vendor_Name as Name ,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name] from TSPL_VENDOR_MASTER"
            gv1.CurrentRow.Cells(colVCGLCode).Value = clsCommon.ShowSelectForm("VCGLGridFinderV", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value), "", isButtonClick)
            '' Anubhooti 13-Mar-2015 (Fetch Alies Name On Vendor Finder)
            'qry = "select TSPL_VENDOR_MASTER.Vendor_Code  ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_ACCOUNT_SET.Payable_Account,TSPL_GL_ACCOUNTS.Description  from TSPL_VENDOR_MASTER "
            'qry += " left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_ACCOUNT_SET.Payable_Account where TSPL_VENDOR_MASTER.Vendor_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value) + "'"
            qry = "select TSPL_VENDOR_MASTER.Vendor_Code  ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_GL_ACCOUNTS.Account_Code AS Payable_Account,TSPL_GL_ACCOUNTS.Description  from TSPL_VENDOR_MASTER " &
             " left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(TSPL_VENDOR_ACCOUNT_SET.Payable_Account,0,LEN(TSPL_VENDOR_ACCOUNT_SET.Payable_Account)-3) where TSPL_VENDOR_MASTER.Vendor_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value) + "' AND TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & clsCommon.myCstr(txtLocSegment.Value) & "'"

            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colVCGLName).Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                gv1.CurrentRow.Cells(colACCode).Value = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
            Else
                gv1.CurrentRow.Cells(colVCGLName).Value = ""
                gv1.CurrentRow.Cells(colACCode).Value = ""
                gv1.CurrentRow.Cells(colACName).Value = ""
                Throw New Exception("Please create GL account with " & txtLocSegment.Value & " location.")
            End If

        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), "Customer") = CompairStringResult.Equal Then
            qry = "select Cust_Code as Code,Customer_Name as Name,ISNULL(TSPL_CUSTOMER_MASTER.alies_name,'') As [Alies Name] from TSPL_CUSTOMER_MASTER"
            gv1.CurrentRow.Cells(colVCGLCode).Value = clsCommon.ShowSelectForm("VCGLGridFinderC", qry, "Code", "Status ='N' AND OnHold='N'", clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value), "", isButtonClick)
            Dim strGLAccount As String = "TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct"
            If chkEmpty.Checked Then
                strGLAccount = "TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit"
            End If
            'qry = "select TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, " + strGLAccount + " as GLAccount, TSPL_GL_ACCOUNTS.Description  from TSPL_CUSTOMER_MASTER"
            'qry += " left outer join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account "
            'qry += " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=" + strGLAccount + " where Cust_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value) + "'"
            qry = "select TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_GL_ACCOUNTS.Account_Code as GLAccount, TSPL_GL_ACCOUNTS.Description  from TSPL_CUSTOMER_MASTER" &
             " left outer join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account " &
             " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(" + strGLAccount + ",0,LEN(" + strGLAccount + ")-3)  where Cust_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value) + "'  AND TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & clsCommon.myCstr(txtLocSegment.Value) & "'"

            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colVCGLName).Value = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                gv1.CurrentRow.Cells(colACCode).Value = clsCommon.myCstr(dt.Rows(0)("GLAccount"))
                gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
            Else
                gv1.CurrentRow.Cells(colVCGLName).Value = ""
                gv1.CurrentRow.Cells(colACCode).Value = ""
                gv1.CurrentRow.Cells(colACName).Value = ""
                Throw New Exception("Please create GL account with " & txtLocSegment.Value & " location.")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), "GL") = CompairStringResult.Equal Then
            qry = "select Account_Code as Code,Description as Name from TSPL_GL_ACCOUNTS "
            whrcls = "Account_Code LIKE '%" + txtLocSegment.Value + "%' and ControlAccount ='N'"
            gv1.CurrentRow.Cells(colVCGLCode).Value = clsCommon.ShowSelectForm("VCGLGridFinderGL", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value), "", isButtonClick)

            qry = "select Account_Code  ,Description  from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value) + "'"
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colVCGLName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                gv1.CurrentRow.Cells(colACCode).Value = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
                gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
            Else
                gv1.CurrentRow.Cells(colVCGLName).Value = ""
                gv1.CurrentRow.Cells(colACCode).Value = ""
                gv1.CurrentRow.Cells(colACName).Value = ""
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), "F") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), "Farmer") = CompairStringResult.Equal Then
            qry = "select Account_Code as Code,Description as Name from TSPL_GL_ACCOUNTS "
            whrcls = "Account_Code LIKE '%" + txtLocSegment.Value + "%' and ControlAccount ='N'"
            gv1.CurrentRow.Cells(colVCGLCode).Value = clsCommon.ShowSelectForm("VCGLGridFinderGL", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value), "", isButtonClick)

            qry = "select Account_Code  ,Description  from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value) + "'"
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colVCGLName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                gv1.CurrentRow.Cells(colACCode).Value = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
                gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
            Else
                gv1.CurrentRow.Cells(colVCGLName).Value = ""
                gv1.CurrentRow.Cells(colACCode).Value = ""
                gv1.CurrentRow.Cells(colACName).Value = ""
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), "Farmer") = CompairStringResult.Equal Then
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "V") = CompairStringResult.Equal Then
                Dim strVendorType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Form_Type from tspl_vendor_master where vendor_code='" & TxtVendorNo.Value & "'"))
                If clsCommon.CompairString(clsCommon.myCstr(strVendorType), "VSP") = CompairStringResult.Equal Then
                    qry = "select tspl_mp_master.MP_Code as Code ,tspl_mp_master.MP_Name as [MP Name],TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code] ,VLC_Name as [VLC Name] from tspl_mp_master left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =tspl_mp_master.VLC_Code left join TSPL_VEndor_master on TSPL_VEndor_master.vendor_code=TSPL_VLC_MASTER_HEAD.vsp_code"
                    whrcls = " form_type='VSP' and TSPL_VLC_MASTER_HEAD.vsp_code in ('" & TxtVendorNo.Value & "')"

                    gv1.CurrentRow.Cells(colVCGLCode).Value = clsCommon.ShowSelectForm("VCGLGridFinderF", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value), "", isButtonClick)

                    qry = "select TSPL_GL_ACCOUNTS.Account_Code AS Payable_Account,TSPL_GL_ACCOUNTS.Description  from TSPL_VENDOR_ACCOUNT_SET left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(TSPL_VENDOR_ACCOUNT_SET.Payable_Account,0,LEN(TSPL_VENDOR_ACCOUNT_SET.Payable_Account)-3) where TSPL_VENDOR_ACCOUNT_SET.IsFarmer=1 and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & clsCommon.myCstr(txtLocSegment.Value) & "'"

                    dt = clsDBFuncationality.GetDataTable(qry)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        gv1.CurrentRow.Cells(colVCGLName).Value = clsCommon.myCstr(clsMpMaster.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value), Nothing))
                        gv1.CurrentRow.Cells(colACCode).Value = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                    Else
                        gv1.CurrentRow.Cells(colVCGLName).Value = ""
                        gv1.CurrentRow.Cells(colACCode).Value = ""
                        gv1.CurrentRow.Cells(colACName).Value = ""
                        Throw New Exception("Please create Farmer Payable GL account with " & txtLocSegment.Value & " location.")
                    End If
                Else
                    Throw New Exception("Vendor should be of VSP Type")
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "F") = CompairStringResult.Equal Then

                qry = "select tspl_mp_master.MP_Code as Code ,tspl_mp_master.MP_Name as [MP Name],TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code] ,VLC_Name as [VLC Name] from tspl_mp_master left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =tspl_mp_master.VLC_Code "

                whrcls = "tspl_mp_master.MP_Code <>'" & clsCommon.myCstr(TxtVendorNo.Value) & " '"
                gv1.CurrentRow.Cells(colVCGLCode).Value = clsCommon.ShowSelectForm("VCGLGridFinderFa", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value), "", isButtonClick)

                qry = "select TSPL_GL_ACCOUNTS.Account_Code AS Payable_Account,TSPL_GL_ACCOUNTS.Description  from TSPL_VENDOR_ACCOUNT_SET left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(TSPL_VENDOR_ACCOUNT_SET.Payable_Account,0,LEN(TSPL_VENDOR_ACCOUNT_SET.Payable_Account)-3) where TSPL_VENDOR_ACCOUNT_SET.IsFarmer=1 and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & clsCommon.myCstr(txtLocSegment.Value) & "'"

                dt = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colVCGLName).Value = clsCommon.myCstr(clsMpMaster.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(colVCGLCode).Value), Nothing))
                    gv1.CurrentRow.Cells(colACCode).Value = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                Else
                    gv1.CurrentRow.Cells(colVCGLName).Value = ""
                    gv1.CurrentRow.Cells(colACCode).Value = ""
                    gv1.CurrentRow.Cells(colACName).Value = ""
                    Throw New Exception("Please create Farmer Payable GL account with " & txtLocSegment.Value & " location.")
                End If

            Else
                Throw New Exception("Please select Vendor/Farmer as Adjustment type.")
            End If

        End If
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotDrAmt As Double = 0
        Dim dblTotCrAmt As Double = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblTotDrAmt = dblTotDrAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDrAmt).Value)
                dblTotCrAmt = dblTotCrAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colCrAmt).Value)
            End If
        Next
        lblTotDrAmt.Text = clsCommon.myFormat(dblTotDrAmt)
        lblTotCrAmt.Text = clsCommon.myFormat(dblTotCrAmt)

        Dim dblNetAmt As Double = dblTotDrAmt - dblTotCrAmt
        lblAmtType.Text = IIf(dblNetAmt < 0, "Cr", "Dr")
        lblTotRAmt.Text = clsCommon.myFormat(Math.Abs(dblNetAmt))
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtPostDate.Value = txtDate.Value
        cboDocType.Enabled = True
        txtLocSegment.Enabled = True
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtLocSegment.Value = ""
        cboDocType.SelectedIndex = 0
        gv1.Rows.AddNew()
    End Sub

    Function AllowToSave() As Boolean

        If btnSave.Text = "Update" Then
            Dim strchk As String = "select Status from TSPL_VCGL_Head where Document_No='" + txtDocNo.Value + "'"
            Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
                Return False
            End If
        End If
        UpdateAllTotals()
        If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor/Customer", Me.Text)
            TxtVendorNo.Focus()
            Return False
        End If

        If clsCommon.CompairString(cboDocType.Text, "F") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtFarmerVendor.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Plese select VSP Code.", Me.Text)
                TxtVendorNo.Focus()
                Return False
            End If

        End If

        If clsCommon.myLen(txtLocSegment.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Location Segment", Me.Text)
            txtLocSegment.Focus()
            Return False
        Else
            clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "General Ledger", "VCGL Entry", txtLocSegment.Value, txtDate.Value, Nothing)
        End If
        If AllowTransferVSPAmtToFarmerinVCGL Then
            Dim dblfarmercount As Double = 0
            Dim dbldatacount As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strRowType As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value)
                If clsCommon.myLen(strRowType) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(strRowType), "Farmer") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(strRowType), "GL") = CompairStringResult.Equal Then
                        dblfarmercount = dblfarmercount + 1
                    End If
                    dbldatacount = dbldatacount + 1
                End If
            Next
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "F") = CompairStringResult.Equal Then
                If dblfarmercount > 0 Then
                    If dblfarmercount < dbldatacount Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please select Type Farmer/GL only", Me.Text)
                        Return False
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Type Farmer/GL only", Me.Text)
                    Return False
                End If
            End If
            If dblfarmercount > 0 Then
                If dblfarmercount < dbldatacount Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Type Farmer/GL only", Me.Text)
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)

    End Sub

    Sub SaveData(ByVal chekPostBtn As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsVCGLHead()
                obj.Document_No = txtDocNo.Value
                obj.Description = txtDesc.Text
                obj.Document_Date = txtDate.Value
                obj.Document_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                If clsCommon.CompairString(obj.Document_Type, "F") = CompairStringResult.Equal Then
                    obj.FarmerInVendor = txtFarmerVendor.Value
                    'lblFarmerVendor.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Vendor_Name from TSPL_VENDOR_MASTER where vendor_Code = '" + obj.FarmerInVendor + "' "))
                End If
                obj.Location_Segment = txtLocSegment.Value
                    obj.VC_Code = TxtVendorNo.Value
                    obj.VC_Name = lblVendorName.Text
                    obj.Remarks = txtRemarks.Text
                    obj.Tot_Dr_Amount = clsCommon.myCdbl(lblTotDrAmt.Text)
                    obj.Tot_Cr_Amount = clsCommon.myCdbl(lblTotCrAmt.Text)
                    obj.Amount_Type = lblAmtType.Text
                    obj.Amount = clsCommon.myCdbl(lblTotRAmt.Text)
                    obj.On_Hold = chkOnHold.Checked
                    If clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal And chkEmpty.Checked Then
                        obj.Is_Empty = 1
                    End If
                    obj.TapalNo = clsCommon.myCstr(txtTapalNo.Text)
                    If txtDataAndTimeSelection.Checked Then
                        obj.DateAndTime = txtDataAndTimeSelection.Value
                    End If
                    obj.Arr = New List(Of clsVCGLDetail)
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim objTr As New clsVCGLDetail()
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                        objTr.VCGL_Code = clsCommon.myCstr(grow.Cells(colVCGLCode).Value)
                        objTr.VCGL_Name = clsCommon.myCstr(grow.Cells(colVCGLName).Value)
                        objTr.Dr_Amount = clsCommon.myCdbl(grow.Cells(colDrAmt).Value)
                        objTr.Cr_Amount = clsCommon.myCdbl(grow.Cells(colCrAmt).Value)
                        objTr.GL_Account_Code = clsCommon.myCstr(grow.Cells(colACCode).Value)
                        objTr.GL_Account_Desc = clsCommon.myCstr(grow.Cells(colACName).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)

                        If (clsCommon.myLen(objTr.GL_Account_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    Next


                    If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one GL Acount", Me.Text)
                    Return
                    End If
                    If (obj.SaveData(obj, isNewEntry)) Then
                        txtDocNo.Value = obj.Document_No
                        If chekPostBtn = True Then

                        Else
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                        LoadData(obj.Document_No)
                    End If
                End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True


            cboDocType.Enabled = False
            txtLocSegment.Enabled = False
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()

            Dim obj As New clsVCGLHead()
            obj = clsVCGLHead.GetData(strDocumentNo)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                txtDocNo.Value = obj.Document_No
                txtDesc.Text = obj.Description
                txtDate.Value = obj.Document_Date
                cboDocType.SelectedValue = obj.Document_Type
                txtLocSegment.Value = obj.Location_Segment
                TxtVendorNo.Value = obj.VC_Code
                '===============================update by richa agarwal 3 July,2018 ticket no. KDI/02/07/18-000383 pick vendor name from vendor master table instead of transaction table
                If clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal Then
                    lblVendorName.Text = clsCustomerMaster.GetName(obj.VC_Code, Nothing)
                ElseIf clsCommon.CompairString(obj.Document_Type, "F") = CompairStringResult.Equal Then
                    lblVendorName.Text = clsMpMaster.GetName(obj.VC_Code, Nothing)
                Else
                    lblVendorName.Text = clsVendorMaster.GetName(obj.VC_Code, Nothing)
                End If

                txtRemarks.Text = obj.Remarks
                lblTotDrAmt.Text = clsCommon.myFormat(obj.Tot_Dr_Amount)
                lblTotCrAmt.Text = clsCommon.myFormat(obj.Tot_Cr_Amount)
                lblAmtType.Text = obj.Amount_Type
                lblTotRAmt.Text = clsCommon.myFormat(obj.Amount)
                chkOnHold.Checked = obj.On_Hold
                If obj.Is_Empty = 1 Then
                    chkEmpty.Checked = True
                Else
                    chkEmpty.Checked = False
                End If
                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    txtDataAndTimeSelection.Value = obj.DateAndTime
                    txtDataAndTimeSelection.Checked = True
                End If
                txtTapalNo.Text = clsCommon.myCstr(obj.TapalNo)

                If clsCommon.CompairString(obj.Document_Type, "F") = CompairStringResult.Equal Then
                    txtFarmerVendor.Value = obj.FarmerInVendor
                    lblFarmerVendor.Text = clsVendorMaster.GetName(obj.FarmerInVendor, Nothing)
                End If

                For Each objTr As clsVCGLDetail In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVCGLCode).Value = objTr.VCGL_Code
                    '===============================update by richa agarwal 3 July,2018 ticket no.KDI/02/07/18-000383 pick vendor name from vendor master table instead of transaction table
                    If clsCommon.CompairString(objTr.Row_Type, "Customer") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVCGLName).Value = clsCustomerMaster.GetName(objTr.VCGL_Code, Nothing)
                    ElseIf clsCommon.CompairString(objTr.Row_Type, "Farmer") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVCGLName).Value = clsMpMaster.GetName(objTr.VCGL_Code, Nothing)
                    ElseIf clsCommon.CompairString(objTr.Row_Type, "GL") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVCGLName).Value = clsGLAccount.GetName(objTr.VCGL_Code, Nothing)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVCGLName).Value = clsVendorMaster.GetName(objTr.VCGL_Code, Nothing)
                    End If
                    ''-------------------- 
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDrAmt).Value = objTr.Dr_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCrAmt).Value = objTr.Cr_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colACCode).Value = objTr.GL_Account_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colACName).Value = objTr.GL_Account_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                Next


                If obj.Status = ERPTransactionStatus.Pending Then
                    gv1.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            '--Commented By Pankaj [Doc date Will be  posting date]
            'If txtPostDate.Value < txtDate.Value Then
            '    common.clsCommon.MyMessageBoxShow("Post Date can't be less then Document Date")
            '    Exit Sub
            'End If
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsVCGLHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                    LoadData(txtDocNo.Value)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsVCGLHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VCGL"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Visible = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Visible = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            btnPost.Visible = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try

            Dim qry As String = " "
            Dim whrclas As String = " "
            If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                whrclas += " AND TSPL_VCGL_Head.Location_Segment in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
            Select Case NavType
                Case NavigatorType.First
                    qry += "select MIN(TSPL_VCGL_Head.Document_No) from TSPL_VCGL_Head Where 1=1 " + whrclas + " "
                Case NavigatorType.Last
                    qry += "select Max(TSPL_VCGL_Head.Document_No) from TSPL_VCGL_Head Where 1=1  " + whrclas + " "
                Case NavigatorType.Next
                    qry += "select Min(TSPL_VCGL_Head.Document_No) from TSPL_VCGL_Head  where  Document_No>'" + txtDocNo.Value + "' " + whrclas + ""
                Case NavigatorType.Previous
                    qry += "select Max(TSPL_VCGL_Head.Document_No) from TSPL_VCGL_Head  where Document_No<'" + txtDocNo.Value + "' " + whrclas + ""
            End Select
            LoadData(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '' Anubhooti 13-Mar-2015 (Fetch Alies Name On Vendor Finder) (KDI/02/07/18-000383 richa )
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Document_No,convert(varchar,Document_Date,103) as Date,VC_Code as [Vendor/Customer/Farmer],case when ISNULL(Document_Type,'')='C' then TSPL_CUSTOMER_MASTER.Customer_Name when ISNULL(Document_Type,'')='F' then TSPL_MP_MASTER.MP_Name else TSPL_VENDOR_MASTER.Vendor_Name end as [Vendor/Customer/Farmer Name],CASE WHEN ISNULL(Document_Type,'')='C' THEN ISNULL(TSPL_CUSTOMER_MASTER.alies_name,'') WHEN ISNULL(Document_Type,'')='V' THEN ISNULL(TSPL_VENDOR_MASTER.alies_name,'') else ISNULL(TSPL_MP_MASTER.MP_Name,'') END [Alies Name],Remarks,case when TSPL_VCGL_Head.status=1 then 'Posted' else 'Pending' end Status from TSPL_VCGL_Head "
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VCGL_HEAD.VC_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
               " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_HEAD.VC_Code=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_MP_MASTER ON TSPL_VCGL_HEAD.VC_Code=TSPL_MP_MASTER.MP_code "
        Dim whrclas As String = " "
        If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
            whrclas += " Location_Segment in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("VCGLEntryCode", qry, "Document_No", whrclas, txtDocNo.Value, "", isButtonClicked))
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        ' PrintData()
        printvoucher(txtDocNo.Value, txtLocSegment.Value)
    End Sub

    Sub printvoucher(ByVal StrCode As String, ByVal LocSeg As String)
        Try
            If txtDocNo.Value = "" AndAlso clsCommon.myLen(StrCode) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select the Document No.", Me.Text)
                Exit Sub
            End If
            Dim CompAdd As String = clsDBFuncationality.getSingleValue("select top(1) Add1+' '+Add2+' '+Add3  from TSPL_LOCATION_MASTER  where Location_Type ='Physical' and Loc_Segment_Code  = '" + LocSeg + "' ")
            Dim qry As String
            qry = " select TSPL_VCGL_Head.GL_Account_Code,TSPL_GL_ACCOUNTS.Description AS GL_Account_Desc ,tspl_location_master.add1 as Loc_Add1,tspl_location_master.Add2 as Loc_Add2,tspl_location_master.add3 as Loc_Add3, Document_No, TSPL_VCGL_Head.description,convert(varchar(12),Document_Date,103) as date,Location_Segment as unit,VC_Code vccode,VC_Name as vcname,Remarks ,case when Amount_Type='Cr' then Amount else 0 end as Debit,case when Amount_Type='Dr' then Amount else 0 end as credit,TSPL_VCGL_Head.Created_By,TSPL_COMPANY_MASTER.Comp_Name,tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end as Add1,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 " & _
                  " ,TSPL_VCGL_Head.TapalNo,TSPL_VCGL_Head.DateAndTime from TSPL_VCGL_Head LEFT OUTER JOIN TSPL_GL_ACCOUNTS  ON TSPL_GL_ACCOUNTS.Account_Code =TSPL_VCGL_Head.GL_Account_Code left outer join TSPL_COMPANY_MASTER on TSPL_VCGL_Head.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code" & _
                    " left outer join tspl_location_master on tspl_location_master.Location_Code =TSPL_VCGL_Head.location_segment  " & _
                  " where Document_No='" + StrCode + "' " & _
                  " union all " & _
                  " select TSPL_VCGL_Detail.GL_Account_Code ,TSPL_VCGL_Detail.GL_Account_Desc , tspl_location_master.add1 as Loc_Add1,tspl_location_master.Add2 as Loc_Add2,tspl_location_master.add3 as Loc_Add3,  TSPL_VCGL_Detail.Document_No, description,convert(varchar(12),TSPL_VCGL_Head.Document_Date,103) as date,TSPL_VCGL_Head.Location_Segment as unit,TSPL_VCGL_Detail.VCGL_Code as vccode,TSPL_VCGL_Detail.VCGL_Name as vcname,TSPL_VCGL_Detail.Remarks,Dr_Amount as Debit ,Cr_Amount as Credit,TSPL_VCGL_Head.Created_By,TSPL_COMPANY_MASTER.Comp_Name,'" + CompAdd + "' as Add1,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_VCGL_Head.TapalNo,TSPL_VCGL_Head.DateAndTime   from TSPL_VCGL_Detail  " & _
                  " left outer join TSPL_VCGL_Head on TSPL_VCGL_Detail.Document_No=TSPL_VCGL_Head.Document_No  " & _
                  " left outer join TSPL_COMPANY_MASTER on TSPL_VCGL_Head.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  " & _
                   " left outer join tspl_location_master on tspl_location_master.Location_Code =TSPL_VCGL_Head.location_segment  " & _
                  " where TSPL_VCGL_Head.Document_No='" + StrCode + "' "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "VCGLVoucher", "Journal Voucher")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub PrintData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Document No not found to print", Me.Text)
        End If
        Dim Arr As New ArrayList
        Arr.Add(txtDocNo.Value)
        frmRptAPInvoice.PrintData("", "", True, Arr, False, Nothing, False, Nothing)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colVCGLCode) = CompairStringResult.Equal Then
                OpenVCGLAccount(True)
                gv1.CurrentColumn = gv1.Columns(colACName)
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            PrintData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        'Try
        '    Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
        '    cell.GradientStyle = GradientStyles.Solid
        '    cell.BackColor = Color.FromArgb(243, 181, 51)
        'Catch ex As Exception
        '   ' common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtLocSegment__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocSegment._MYValidating
        Dim qry As String = " select Segment_code as Code ,Description as Name  from TSPL_GL_SEGMENT_CODE "
        Dim whrclas As String = " Seg_No =7 "
        If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
            whrclas += " AND Segment_code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        End If
        txtLocSegment.Value = clsCommon.ShowSelectForm("VCGLLocaSegment", qry, "Code", whrclas, txtLocSegment.Value, "", isButtonClicked)
    End Sub

    Sub SetVCText()
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "V") = CompairStringResult.Equal Then
            RadLabel2.Text = "Vendor"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "F") = CompairStringResult.Equal Then
            RadLabel2.Text = "Farmer"
        Else
            RadLabel2.Text = "Customer"
        End If
        repoRowType.DataSource = GetRowType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged
        chkEmpty.Checked = False
        chkEmpty.Enabled = False
        SetVCText()
        If Not IsFormLoad Then
            lblVendorNoForFarmer.Enabled = False
            txtFarmerVendor.Enabled = False
            lblFarmerVendor.Enabled = False
            txtFarmerVendor.Value = ""
            lblFarmerVendor.Text = ""
            If clsCommon.CompairString(cboDocType.SelectedValue, "C") = CompairStringResult.Equal Then
                chkEmpty.Enabled = True
            ElseIf clsCommon.CompairString(cboDocType.SelectedValue, "F") = CompairStringResult.Equal Then
                lblVendorNoForFarmer.Enabled = True
                txtFarmerVendor.Enabled = True
                lblFarmerVendor.Enabled = True
            End If
        End If

    End Sub

    Private Sub cboDocType_SelectedIndexChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangingCancelEventArgs) Handles cboDocType.SelectedIndexChanging
        SetVCText()
    End Sub
    '-Commented By Pankaj [Doc date Will be tha Posting date]--_Ranjana mam
    'Private Sub txtDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
    '    txtPostDate.Value = txtDate.Value
    'End Sub


    Private Sub chkEmpty_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkEmpty.ToggleStateChanged
        repoRowType.DataSource = GetRowType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        If chkEmpty.Checked Then
            cboDocType.SelectedValue = "C"
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsVCGLHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        'If transportSql.importExcel(gv, "Document Date", "Document Type", "Location", "Vendor Code", "Vendor Name", " Remarks", " Total Cradit Amount", " Total Debit Amount", "Amount", "Amount Type", "GL Account", "GL Account Desc", "Description", "VCGL_Code", "VCGL_Name", "Row Type") Then
        If transportSql.importExcel(gv, "Vendor Code", "Vendor Name", "Document Date", "Document Type", "Location", " Remarks", " Total Credit Amount", " Total Debit Amount", "Amount", "Amount Type", "GL Account", "GL Account Desc", "Description", "VCGL_Code", "VCGL_Name", "Row Type", "VSP Code For MP") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim DocDate As String = String.Empty
                Dim DocType As String = String.Empty
                Dim Location As String = String.Empty
                Dim strHeaderName_VC As String = String.Empty
                Dim strHeaderCode_VC As String = String.Empty
                Dim Remarks As String = ""
                Dim TotalCrt As String = ""
                Dim TotalDbt As String = ""
                Dim TotalAmount As String = ""
                Dim AmountType As String = ""
                Dim GLAccount As String = ""
                Dim GLAccountDesc As String = ""
                Dim Description As String = ""
                Dim VCGL_Code As String = ""
                Dim VCGL_Name As String = ""
                Dim RowType As String = ""
                Dim line As Integer = 0
                Dim check As String = ""
                Dim HeaderRemarks As String = String.Empty
                Dim VSPCodeForMP As String = String.Empty
                Dim qry As String = String.Empty
                Dim obj As New clsVCGLHead()
                clsCommon.ProgressBarShow()
                Dim counter As Integer = 1
                For Each grow As GridViewRowInfo In gv.Rows

                    line += 1
                    ''richa GKD/24/06/19-000182 write code again for import functionality using class 
                    DocDate = clsCommon.myCDate(grow.Cells("Document Date").Value)
                    If clsCommon.myLen(DocDate) <= 0 Then
                        Throw New Exception("Fill Document Date At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    DocType = clsCommon.myCstr(grow.Cells("Document Type").Value)
                    If clsCommon.myLen(DocType) <= 0 Then
                        Throw New Exception("Fill Document Type At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Location = clsCommon.myCstr(grow.Cells("Location").Value)
                    If clsCommon.myLen(Location) <= 0 Then
                        Throw New Exception("Fill Location At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    strHeaderCode_VC = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                    If clsCommon.myLen(strHeaderCode_VC) <= 0 Then
                        Throw New Exception("Fill Vendor Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    strHeaderName_VC = clsCommon.myCstr(grow.Cells("Vendor Name").Value)
                    If clsCommon.myLen(strHeaderName_VC) <= 0 Then
                        Throw New Exception("Fill Vendor Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(DocType, "V") = CompairStringResult.Equal Then
                        Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" & strHeaderCode_VC & "'", trans))
                    ElseIf clsCommon.CompairString(DocType, "C") = CompairStringResult.Equal Then
                        Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" & strHeaderCode_VC & "'", trans))
                    ElseIf clsCommon.CompairString(DocType, "F") = CompairStringResult.Equal Then
                        Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MP_name from TSPL_MP_MASTER where MP_Code='" & strHeaderCode_VC & "'", trans))
                        VSPCodeForMP = clsCommon.myCstr(grow.Cells("VSP Code For MP").Value)
                    End If

                    AmountType = clsCommon.myCstr(grow.Cells("Amount Type").Value)
                    If clsCommon.myLen(AmountType) <= 0 Then
                        Throw New Exception("Fill Amount Type At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    HeaderRemarks = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    TotalCrt = ""
                    TotalDbt = ""
                    If clsCommon.CompairString(AmountType, "Cr") = CompairStringResult.Equal Then
                        TotalCrt = clsCommon.myCdbl(grow.Cells("Total Credit Amount").Value)
                        If TotalCrt <= 0 Then
                            Throw New Exception("Fill Credit Amount At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        TotalDbt = clsCommon.myCdbl(grow.Cells("Total Debit Amount").Value)
                        If TotalDbt <= 0 Then
                            Throw New Exception("Fill Debit Amount At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    TotalAmount = clsCommon.myCdbl(grow.Cells("Amount").Value)

                    If clsCommon.myLen(TotalAmount) <= 0 Then
                        Throw New Exception("Fill Total Amount At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If TotalAmount < 0 Then
                        Throw New Exception("Total Amount can't be in -ve At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    VCGL_Code = clsCommon.myCstr(grow.Cells("VCGL_Code").Value)
                    If clsCommon.myLen(VCGL_Code) <= 0 Then
                        Throw New Exception("Fill VCGL_Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    VCGL_Name = clsCommon.myCstr(grow.Cells("VCGL_Name").Value)
                    If clsCommon.myLen(VCGL_Name) <= 0 Then
                        Throw New Exception("Fill VCGL_Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    RowType = clsCommon.myCstr(grow.Cells("Row Type").Value)
                    If clsCommon.myLen(RowType) <= 0 Then
                        Throw New Exception("Fill Row Type At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.CompairString(RowType, "Vendor") = CompairStringResult.Equal Then
                        VCGL_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" & VCGL_Code & "'", trans))
                    ElseIf clsCommon.CompairString(RowType, "Customer") = CompairStringResult.Equal Then
                        VCGL_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" & VCGL_Code & "'", trans))
                    End If

                    If clsCommon.CompairString(RowType, "Vendor") = CompairStringResult.Equal Then
                        qry = "select TSPL_GL_ACCOUNTS.Account_Code from TSPL_VENDOR_MASTER " &
                         " left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(TSPL_VENDOR_ACCOUNT_SET.Payable_Account,0,LEN(TSPL_VENDOR_ACCOUNT_SET.Payable_Account)-3) where TSPL_VENDOR_MASTER.Vendor_Code='" + VCGL_Code + "' AND TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & clsCommon.myCstr(Location) & "'"
                        GLAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                        qry = "select TSPL_GL_ACCOUNTS.Description from TSPL_VENDOR_MASTER " &
                        " left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(TSPL_VENDOR_ACCOUNT_SET.Payable_Account,0,LEN(TSPL_VENDOR_ACCOUNT_SET.Payable_Account)-3) where TSPL_VENDOR_MASTER.Vendor_Code='" + VCGL_Code + "' AND TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & clsCommon.myCstr(Location) & "'"
                        GLAccountDesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(GLAccount) <= 0 Then
                            Throw New Exception("Please create GL account with " & txtLocSegment.Value & " location for Vendor " & VCGL_Code & " At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(RowType, "Customer") = CompairStringResult.Equal Then

                        qry = "select  TSPL_GL_ACCOUNTS.Account_Code as GLAccount from TSPL_CUSTOMER_MASTER" &
                         " left outer join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account " &
                         " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct,0,LEN(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct)-3)  where Cust_Code='" + VCGL_Code + "'  AND TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & Location & "'"
                        GLAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                        qry = "select TSPL_GL_ACCOUNTS.Description  from TSPL_CUSTOMER_MASTER" &
                        " left outer join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account " &
                        " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Seg_Code1 =SUBSTRING(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct,0,LEN(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct)-3)  where Cust_Code='" + VCGL_Code + "'  AND TSPL_GL_ACCOUNTS.Account_Seg_Code7='" & Location & "'"
                        GLAccountDesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                        If clsCommon.myLen(GLAccount) <= 0 Then
                            Throw New Exception("Please create GL account with " & txtLocSegment.Value & " location for Vendor " & VCGL_Code & " At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If


                    obj = New clsVCGLHead()
                    obj.Document_Date = DocDate
                    obj.Document_Type = DocType
                    obj.Location_Segment = Location
                    obj.VC_Code = strHeaderCode_VC
                    obj.VC_Name = strHeaderName_VC
                    obj.Remarks = HeaderRemarks
                    obj.Tot_Dr_Amount = clsCommon.myCdbl(TotalDbt)
                    obj.Tot_Cr_Amount = clsCommon.myCdbl(TotalCrt)
                    obj.Amount_Type = AmountType
                    obj.Amount = clsCommon.myCdbl(TotalDbt) + clsCommon.myCdbl(TotalCrt)
                    obj.On_Hold = 0
                    obj.Is_Empty = 0
                    obj.Description = Description
                    If clsCommon.CompairString(RowType, "GL") = CompairStringResult.Equal Then
                        obj.FarmerInVendor = VSPCodeForMP
                    End If
                    obj.Arr = New List(Of clsVCGLDetail)

                    '' grid value
                    Dim objTr As New clsVCGLDetail()
                    objTr.Line_No = 1
                    objTr.Row_Type = RowType
                    objTr.VCGL_Code = VCGL_Code
                    objTr.VCGL_Name = VCGL_Name
                    objTr.Dr_Amount = clsCommon.myCdbl(TotalDbt)
                    objTr.Cr_Amount = clsCommon.myCdbl(TotalCrt)
                    If clsCommon.CompairString(RowType, "GL") = CompairStringResult.Equal Then
                        objTr.GL_Account_Code = VCGL_Code
                        objTr.GL_Account_Desc = VCGL_Name
                    Else
                        objTr.GL_Account_Code = GLAccount
                        objTr.GL_Account_Desc = GLAccountDesc
                    End If

                    obj.Arr.Add(objTr)

                    obj.SaveData(obj, True, trans)

                    counter += 1
                    clsCommon.ProgressBarUpdate("Imported Records  : " & counter & "/" & gv.Rows.Count)

                Next
                clsCommon.ProgressBarHide()
                trans.Commit()
                If clsCommon.myLen(check) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                End If

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        funExport()
    End Sub
    Public Sub funExport()
        Try
            Dim strCmd As String = "select TSPL_VCGL_Head.VC_Code as [Vendor Code],TSPL_VCGL_Head.VC_Name as [vendor Name],convert(varchar,TSPL_VCGL_Head.Document_Date,103) as [Document Date], TSPL_VCGL_Head.Document_Type as [Document Type],TSPL_VCGL_Head.Location_Segment as Location,TSPL_VCGL_Head.Remarks,TSPL_VCGL_Head.Tot_Cr_Amount as [Total Credit Amount],TSPL_VCGL_Head.Tot_Dr_Amount as [Total Debit Amount],TSPL_VCGL_Head.Amount,TSPL_VCGL_Head.Amount_Type as [Amount Type],TSPL_VCGL_Head.GL_Account_Code as [GL Account],TSPL_VCGL_Detail.GL_Account_Desc as [GL Account Desc],TSPL_VCGL_Head.Description,TSPL_VCGL_Detail.VCGL_Code,TSPL_VCGL_Detail.VCGL_Name,TSPL_VCGL_Detail.Row_type as [Row Type], TSPL_VCGL_Head.FarmerInVendor as [VSP Code For MP] from TSPL_VCGL_Head inner join TSPL_VCGL_Detail on TSPL_VCGL_Detail.Document_No=TSPL_VCGL_Head.Document_No "
            transportSql.ExporttoExcel(strCmd, "", "", Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "VCGL Entry")
        End Try
    End Sub

    Private Sub txtFarmerVendor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFarmerVendor._MYValidating
        Try
            'select Vendor_Code as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER where Form_Type = 'VSP'
            'Dim Qry As String = "select  TSPL_VLC_MASTER_HEAD.VSP_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as VendorName , TSPL_MP_MASTER.MP_Code as [MP Code] , TSPL_MP_MASTER.MP_Name as [MP Name] from TSPL_MP_MASTER left outer join TSPL_VLC_MASTER_HEAD on TSPL_MP_MASTER.VLC_Code = TSPL_VLC_MASTER_HEAD.VLC_Code left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code "
            'txtFarmerVendor.Value = clsCommon.ShowSelectForm("VCGL@FarmerVendor", Qry, "Code", " TSPL_MP_MASTER.MP_Code= '" + TxtVendorNo.Value + "' ", txtFarmerVendor.Value, "Code", isButtonClicked)
            Dim Qry As String = "select Vendor_Code as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER "
            txtFarmerVendor.Value = clsCommon.ShowSelectForm("VCGL@FarmerVendor", Qry, "Code", " Form_Type = 'VSP' ", txtFarmerVendor.Value, "Code", isButtonClicked)
            lblFarmerVendor.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where vendor_Code = '" + txtFarmerVendor.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

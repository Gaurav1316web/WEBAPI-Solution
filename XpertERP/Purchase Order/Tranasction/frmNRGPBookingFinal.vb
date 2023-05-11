''10/09/2014---Created by --[Panch Raj]-- Ticket no : BM00000003594 
''BM00000004499--BM00000004883

Imports common
Imports Microsoft.Office.Interop
Imports System.IO

Public Class frmNRGPBookingFinal
    Inherits FrmMainTranScreen
#Region "Variables"

    Private MainFormId As String = clsUserMgtCode.frmCSABooking
    Private Trans_Type As String = "Booking"

    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsFormLoad As Boolean = False

    Const colLineNo As String = "LineNO"
    Const colItemCode As String = "ItemCode"
    Const colItemdesc As String = "ItemName"
    Const colCSA_ITEM_TYPE As String = "colCSA_ITEM_TYPE"
    Const colBOOK_QTY As String = "colBOOK_QTY"
    Const colBOOK_QTY_UOM As String = "colBOOK_QTY_UOM"
    Const colTAX_PAID As String = "colTAX_PAID"
    Const colBOOK_Rate As String = "colBOOK_Rate"
    Const colBOOK_RATE_UOM As String = "colBOOK_RATE_UOM"
    Const colTOTAL_QTY As String = "colTOTAL_QTY"

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public strDocumentNo As String = ""
    Dim objList As New List(Of clsCSABookingDetail)
    Dim obj As New clsCSABooking
    Public Const strCostTransaction As String = "Production Entry"
    Public MOActive As Boolean = False

    Dim arrLoc As String = Nothing
#End Region

    Public Sub New(ByVal FormId As String)
        InitializeComponent()
        MainFormId = FormId
    End Sub

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                txtlocation.Value = obj.Default_LocCode
                lbl_location.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(MainFormId)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        '' get mo setting
        'MOActive = ClsMFSeetings.Get_MO_BO_Setting()
        Me.btnPrint.Visible = False
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post transaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete transaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New transaction")
        RadPageView1.SelectedPage = RadPageViewPage1

        If clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSARequest)) = CompairStringResult.Equal Then
            RadPageViewPage1.Text = "Request"
            Trans_Type = "Request"
            lblReceiptCode.Text = "Request Code"
            rbtn_group.Visible = False
            rbtn_Item.Visible = False
            lblreqtype.Visible = True
            ddlReqType.Visible = True
            txtlocation.Visible = True
            lbl_location.Visible = True
            MyLabel2.Visible = True
            loadtype()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSABooking)) = CompairStringResult.Equal Then
            RadPageViewPage1.Text = "Booking"
            Trans_Type = "Booking"
            lblReceiptCode.Text = "Booking Code"
            rbtn_group.Visible = True
            rbtn_Item.Visible = True
            lblreqtype.Visible = False
            ddlReqType.Visible = False
            txtlocation.Visible = False
            lbl_location.Visible = False
            MyLabel2.Visible = False
        End If

        LoadBlankGrid()
        funReset()
        SetLength()
        gv1.AllowDeleteRow = True
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub loadtype()
        Dim dtreqtype As New DataTable
        dtreqtype.Columns.Add("Code", GetType(String))
        dtreqtype.Columns.Add("Name", GetType(String))

        dtreqtype.Rows.Add("", "None")
        dtreqtype.Rows.Add("Mail", "Mail")
        dtreqtype.Rows.Add("Phone", "Phone")
        ddlReqType.DataSource = dtreqtype
        ddlReqType.ValueMember = "Code"
        ddlReqType.DisplayMember = "Name"
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 200
    End Sub


    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtCSAName.Text = ""
        txtlocation.Value = ""
        lbl_location.Text = ""
        ddlReqType.SelectedValue = ""
        dtpDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim Line_No As New GridViewTextBoxColumn
        Dim CSA_ITEM_TYPE As New GridViewComboBoxColumn
        Dim BOOK_QTY_UOM As New GridViewTextBoxColumn
        Dim TAX_PAID As New GridViewComboBoxColumn
        Dim BOOK_QTY As New GridViewDecimalColumn
        Dim BOOK_RATE_UOM As New GridViewTextBoxColumn
        Dim BOOK_Rate As New GridViewDecimalColumn
        Dim TOTAL_QTY As New GridViewDecimalColumn

        Line_No.FormatString = ""
        Line_No.HeaderText = "Line No"
        Line_No.Name = colLineNo
        Line_No.Width = 50
        Line_No.ReadOnly = True
        Line_No.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Line_No)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.Width = 100
        repoicode.HeaderText = "Item Code"
        repoicode.Name = colItemCode
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoicode.IsVisible = False
        gv1.Columns.Add(repoicode)

        Dim repoitemname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitemname.Name = colItemdesc
        repoitemname.FormatString = ""
        repoitemname.Width = 300
        repoitemname.HeaderText = "Description"
        repoitemname.ReadOnly = True
        repoitemname.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoitemname)

        CSA_ITEM_TYPE.FormatString = ""
        CSA_ITEM_TYPE.HeaderText = "CSA Item Type"
        CSA_ITEM_TYPE.Name = colCSA_ITEM_TYPE
        CSA_ITEM_TYPE.Width = 120
        CSA_ITEM_TYPE.ReadOnly = False
        CSA_ITEM_TYPE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        CSA_ITEM_TYPE.DataSource = clsCSABooking.LoadItemCSAType()
        CSA_ITEM_TYPE.ValueMember = "Code"
        CSA_ITEM_TYPE.DisplayMember = "Name"
        gv1.Columns.Add(CSA_ITEM_TYPE)

        BOOK_QTY.FormatString = ""
        BOOK_QTY.HeaderText = "Booking Qty"
        BOOK_QTY.Name = colBOOK_QTY
        BOOK_QTY.Width = 100
        BOOK_QTY.ReadOnly = False
        BOOK_QTY.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BOOK_QTY)


        BOOK_QTY_UOM.FormatString = ""
        BOOK_QTY_UOM.HeaderText = "Booking UOM"
        BOOK_QTY_UOM.Name = colBOOK_QTY_UOM
        BOOK_QTY_UOM.Width = 100
        BOOK_QTY_UOM.ReadOnly = False
        BOOK_QTY_UOM.HeaderImage = Global.ERP.My.Resources.Resources.search4
        BOOK_QTY_UOM.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.Columns.Add(BOOK_QTY_UOM)

        TAX_PAID.FormatString = ""
        TAX_PAID.HeaderText = "Tax Paid"
        TAX_PAID.Name = colTAX_PAID
        TAX_PAID.Width = 100
        TAX_PAID.ReadOnly = False
        TAX_PAID.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        TAX_PAID.DataSource = clsCSABooking.GetTaxPaid()
        TAX_PAID.ValueMember = "Code"
        TAX_PAID.DisplayMember = "Name"
        gv1.Columns.Add(TAX_PAID)

        BOOK_Rate.FormatString = ""
        BOOK_Rate.HeaderText = "Rate"
        BOOK_Rate.Name = colBOOK_Rate
        BOOK_Rate.Width = 100
        'BOOK_Rate.ReadOnly = True
        BOOK_Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BOOK_Rate)

        BOOK_RATE_UOM.FormatString = ""
        BOOK_RATE_UOM.HeaderText = "Rate UOM"
        BOOK_RATE_UOM.Name = colBOOK_RATE_UOM
        BOOK_RATE_UOM.Width = 100
        BOOK_RATE_UOM.ReadOnly = False
        BOOK_RATE_UOM.HeaderImage = Global.ERP.My.Resources.Resources.search4
        BOOK_RATE_UOM.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.Columns.Add(BOOK_RATE_UOM)

        If clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSARequest)) = CompairStringResult.Equal Then
            gv1.Columns(colTAX_PAID).IsVisible = False
            gv1.Columns(colTAX_PAID).VisibleInColumnChooser = False
            gv1.Columns(colBOOK_Rate).IsVisible = False
            gv1.Columns(colBOOK_Rate).VisibleInColumnChooser = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSABooking)) = CompairStringResult.Equal Then
            gv1.Columns(colTAX_PAID).IsVisible = True
            gv1.Columns(colTAX_PAID).VisibleInColumnChooser = True
            gv1.Columns(colBOOK_Rate).IsVisible = True
            gv1.Columns(colBOOK_Rate).VisibleInColumnChooser = True
        End If

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub


    Sub funReset()

        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        dtpDate.Focus()

        gv1.Rows.Clear()
        gv1.Rows.AddNew()
        Me.txtBatchNo.Value = Nothing

        Me.txtDesc.Text = ""
        txtDesc.Text = clsCSABooking.GetBookingDescrptn(Trans_Type)

        txtCode.MyReadOnly = False

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        If clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSARequest)) = CompairStringResult.Equal Then
            rbtn_Item.IsChecked = True
            gv1.Columns(colCSA_ITEM_TYPE).IsVisible = True
            gv1.Columns(colItemCode).IsVisible = True
            gv1.Columns(colItemdesc).IsVisible = True
            gv1.Columns(colCSA_ITEM_TYPE).ReadOnly = True
            gv1.Columns(colBOOK_RATE_UOM).VisibleInColumnChooser = False
            gv1.Columns(colBOOK_RATE_UOM).IsVisible = False
            LOCATIONRIGTHS()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSABooking)) = CompairStringResult.Equal Then
            rbtn_group.IsChecked = True
            gv1.Columns(colCSA_ITEM_TYPE).IsVisible = True
            gv1.Columns(colItemCode).IsVisible = False
            gv1.Columns(colItemdesc).IsVisible = False
            gv1.Columns(colCSA_ITEM_TYPE).ReadOnly = False
            gv1.Columns(colBOOK_RATE_UOM).VisibleInColumnChooser = True
            gv1.Columns(colBOOK_RATE_UOM).IsVisible = True
        End If
        ReStoreGridLayout()
    End Sub

    Function AllowToSave() As Boolean
        Try
            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                dtpDate.Select()
                Return False
            End If

            If clsCommon.myLen(txtBatchNo.Value) <= 0 Then
                myMessages.blankValue("CSA Code")
                txtBatchNo.Focus()
                Return False
            End If

            If clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSARequest)) = CompairStringResult.Equal Then
                If clsCommon.myLen(txtlocation.Value) <= 0 Then
                    myMessages.blankValue("Location Code")
                    txtlocation.Focus()
                    Return False
                End If

                If clsCommon.CompairString(ddlReqType.SelectedValue, "") = CompairStringResult.Equal Then
                    myMessages.blankValue("Request Type ")
                    ddlReqType.Focus()
                    ddlReqType.Select()
                    Return False
                End If
            End If


            If Me.gv1.Rows.Count = 0 Then
                myMessages.blankValue("List ")
                Return False
            End If
            Dim totalRow As Integer = 0

            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colCSA_ITEM_TYPE).Value) > 0 Then
                    If grow.Cells(colBOOK_QTY).Value <= 0 Then
                        clsCommon.MyMessageBoxShow("Booked Quantity at row no " & (grow.Index + 1) & " must be greater than zero.")
                        Return False
                    End If

                    If clsCommon.myLen(grow.Cells(colBOOK_QTY_UOM).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please enter Booked Quantity UOM at row no " & (grow.Index + 1) & "")
                        Return False
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSABooking)) = CompairStringResult.Equal Then
                        If clsCommon.myLen(grow.Cells(colBOOK_Rate).Value) <= 0 Then
                            clsCommon.MyMessageBoxShow("Please enter Booked Rate at row no " & (grow.Index + 1) & "")
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(grow.Cells(colBOOK_RATE_UOM).Value) <= 0 Then
                        If clsCommon.CompairString(clsUserMgtCode.frmCSARequest, MainFormId) = CompairStringResult.Equal Then
                            isCellValueChangedOpen = True
                            grow.Cells(colBOOK_RATE_UOM).Value = clsCommon.myCstr(grow.Cells(colBOOK_QTY_UOM).Value)
                            isCellValueChangedOpen = False
                        Else
                            clsCommon.MyMessageBoxShow("Please enter Booked Rate UOM at row no " & (grow.Index + 1) & "")
                            Return False
                        End If
                    End If
                    If clsCommon.CompairString(clsUserMgtCode.frmCSABooking, MainFormId) = CompairStringResult.Equal Then
                        If clsCommon.myLen(grow.Cells(colTAX_PAID).Value) <= 0 Then
                            clsCommon.MyMessageBoxShow("Please select Tax Paid at row no " & (grow.Index + 1) & "")
                            Return False
                        End If
                    End If


                    totalRow = totalRow + 1
                    For jj As Integer = (grow.Index + 1) To gv1.Rows.Count - 1
                        If clsCommon.myLen(grow.Cells(colItemCode).Value) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colCSA_ITEM_TYPE).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(colCSA_ITEM_TYPE).Value)) = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow("Duplicate value not allowed,see at row no. " + clsCommon.myCstr(jj + 1) + "")
                            Return False
                        End If

                        If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colItemCode).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow("Duplicate item value not allowed,see at row no. " + clsCommon.myCstr(jj + 1) + "")
                            Return False
                        End If
                    Next

                End If
            Next

            If totalRow = 0 Then
                myMessages.blankValue("List is Empty")
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    '---
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Function SaveData(ByVal ChekBtnPost As Boolean) As Boolean
        Try
            txtDesc.Focus()
            txtDesc.Select()
            If AllowToSave() Then
                'clsCommon.ProgressBarShow()

                Dim obj As New clsCSABooking
                obj.BOOKING_NO = Me.txtCode.Value
                obj.DESCRIPTION = Me.txtDesc.Text
                obj.BOOKING_DATE = Me.dtpDate.Value
                obj.CSA_CODE = Me.txtBatchNo.Value

                If clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSARequest)) = CompairStringResult.Equal Then
                    obj.Trans_Type = Trans_Type
                    obj.Request_Mode = clsCommon.myCstr(ddlReqType.SelectedValue)
                    obj.Location_Code = clsCommon.myCstr(txtlocation.Value)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSABooking)) = CompairStringResult.Equal Then
                    obj.Trans_Type = Trans_Type
                    obj.Request_Mode = ""
                    obj.Location_Code = ""
                End If

                If rbtn_group.IsChecked Then
                    obj.Booking_Type = "Group-Wise"
                Else
                    obj.Booking_Type = "Item-Wise"
                End If

                Dim obj1 As clsCSABookingDetail
                objList = New List(Of clsCSABookingDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCSA_ITEM_TYPE).Value)) > 0 Then
                        obj1 = New clsCSABookingDetail()
                        obj1.BOOKING_NO = txtCode.Value
                        obj1.Itemcode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj1.BOOK_QTY = clsCommon.myCdbl(grow.Cells(colBOOK_QTY).Value)
                        obj1.BOOK_QTY_UOM = clsCommon.myCstr(grow.Cells(colBOOK_QTY_UOM).Value)
                        obj1.BOOK_Rate = clsCommon.myCdbl(grow.Cells(colBOOK_Rate).Value)
                        obj1.BOOK_RATE_UOM = clsCommon.myCstr(grow.Cells(colBOOK_RATE_UOM).Value)
                        obj1.CSA_ITEM_TYPE = clsCommon.myCstr(grow.Cells(colCSA_ITEM_TYPE).Value)
                        obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSARequest)) = CompairStringResult.Equal Then
                            obj1.TAX_PAID = clsCommon.myCstr("No")
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSABooking)) = CompairStringResult.Equal Then
                            obj1.TAX_PAID = clsCommon.myCstr(grow.Cells(colTAX_PAID).Value)
                        End If
                        'obj1.TOTAL_QTY = clsCommon.myCdbl(grow.Cells(colTOTAL_QTY).Value)

                        objList.Add(obj1)

                    End If
                Next

                Dim issaved As Boolean = False
                issaved = obj.SaveData(obj, objList, isNewEntry, clsCommon.myCstr(txtCode.Value))
                UcAttachment1.SaveData(txtCode.Value)
                If issaved = True Then
                    'clsCommon.ProgressBarHide()
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.BOOKING_NO, NavigatorType.Current)
                    Return True
                End If

                'clsCommon.ProgressBarHide()
                Return False
            End If
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            'clsCommon.ProgressBarShow()

            funReset()
            txtCode.MyReadOnly = True
            btnSave.Enabled = True
            btnDelete.Enabled = True

            obj = New clsCSABooking()
            obj = clsCSABooking.GetData(strCode, arrLoc, NavTyep, Trans_Type)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.CSA_CODE) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                If obj.POSTED Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                Dim ii As Int16 = 0
                LoadBlankGrid()
                txtCode.Value = obj.BOOKING_NO
                Me.txtDesc.Text = clsCommon.myCstr(obj.DESCRIPTION)

                Me.dtpDate.Value = obj.BOOKING_DATE

                Me.txtBatchNo.Value = obj.CSA_CODE
                Me.txtCSAName.Text = obj.CSA_NAME
                rbtn_group.IsChecked = True
                If obj.Booking_Type = "Item-Wise" Then
                    rbtn_Item.IsChecked = True
                End If

                If clsCommon.CompairString(clsCommon.myCstr(MainFormId), clsCommon.myCstr(clsUserMgtCode.frmCSARequest)) = CompairStringResult.Equal Then
                    ddlReqType.SelectedValue = clsCommon.myCstr(obj.Request_Mode)
                    txtlocation.Value = clsCommon.myCstr(obj.Location_Code)
                    lbl_location.Text = clsLocation.GetName(obj.Location_Code, Nothing)
                End If

                If (clsCSABooking.ObjList IsNot Nothing AndAlso clsCSABooking.ObjList.Count > 0) Then
                    For Each objTr As clsCSABookingDetail In clsCSABooking.ObjList
                        gv1.Rows.AddNew()
                        'gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(Me.gv1.Rows.Count)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_QTY).Value = clsCommon.myCdbl(objTr.BOOK_QTY)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_QTY_UOM).Value = clsCommon.myCstr(objTr.BOOK_QTY_UOM)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCSA_ITEM_TYPE).Value = clsCommon.myCstr(objTr.CSA_ITEM_TYPE)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Itemcode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemdesc).Value = objTr.Itemname
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_RATE_UOM).Value = clsCommon.myCstr(objTr.BOOK_RATE_UOM)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_PAID).Value = clsCommon.myCstr(objTr.TAX_PAID)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_Rate).Value = clsCommon.myCdbl(objTr.BOOK_Rate)
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTAL_QTY).Value = clsCommon.myCdbl(objTr.TOTAL_QTY)

                    Next
                    UcAttachment1.LoadData(txtCode.Value)
                Else
                    gv1.Rows.AddNew()
                End If
            End If
            isInsideLoadData = False
            'clsCommon.ProgressBarHide()

        Catch ex As Exception
            isInsideLoadData = False
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message)
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

            If (myMessages.postConfirm()) Then
                SaveData(True)
                clsCSABooking.PostData(txtCode.Value, arrLoc, True, Trans_Type)
                common.clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
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

                'clsCommon.ProgressBarShow()
                If (clsCSABooking.DeleteData(txtCode.Value, Trans_Type)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    'clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
                'clsCommon.ProgressBarHide()
            End If
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function


    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim check As Boolean = False
        check = clsCSABooking.CheckValidCode(Me.txtCode.Value)

        If check Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            Dim whrCls As String = " tspl_csa_booking_head.trans_type='" + Trans_Type + "' "

            If clsCommon.CompairString(MainFormId, clsUserMgtCode.frmCSARequest) = CompairStringResult.Equal Then
                whrCls += " and TSPL_CSA_BOOKING_HEAD.Location_Code in (" + arrLoc + ") "
            End If

            txtCode.Value = clsCommon.myCstr(clsCSABooking.GetFinder(whrCls, txtCode.Value, isButtonClicked))
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            funReset()
        End If
    End Sub


    Private Sub frmCSABooking_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
                isCellValueChangedOpen = False
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
                funReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
                SaveData(False)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
                PostData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
                DeleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
                CloseForm()
            End If

            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colItemCode) Then
                isCellValueChangedOpen = True
                gv1.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("active=1 and tspl_item_master.item_type in ('F','T') and isnull(Is_FreshItem,0)<>1", clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), True)
                gv1.CurrentRow.Cells(colItemdesc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), Nothing)
                gv1.CurrentRow.Cells(colCSA_ITEM_TYPE).Value = clsCSABooking.GetCSAType(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value))
                isCellValueChangedOpen = False
            End If
            Dim whrCls As String = ""
            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colBOOK_QTY_UOM) Then
                isCellValueChangedOpen = True
                If clsCommon.myLen(gv1.CurrentRow.Cells(colCSA_ITEM_TYPE).Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) <= 0 Then
                    whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in (select item_code from tspl_item_master where csa_type='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCSA_ITEM_TYPE).Value) + "' and isnull(Is_FreshItem,0)<>1))"
                ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
                    whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in ('" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "'))"
                End If

                gv1.CurrentRow.Cells(colBOOK_QTY_UOM).Value = clsUOMInfo.GetFinder(whrCls, gv1.CurrentRow.Cells(colBOOK_QTY_UOM).Value, True)
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colBOOK_RATE_UOM) Then
                isCellValueChangedOpen = True
                If clsCommon.myLen(gv1.CurrentRow.Cells(colCSA_ITEM_TYPE).Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) <= 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colCSA_ITEM_TYPE).Value), "CPD-DESI GHEE") = CompairStringResult.Equal Then
                        whrCls = " unit_code in (select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y')"
                    Else
                        whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in (select item_code from tspl_item_master where isnull(Is_FreshItem,0)<>1 and csa_type='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCSA_ITEM_TYPE).Value) + "'))"
                    End If
                ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colCSA_ITEM_TYPE).Value), "CPD-DESI GHEE") = CompairStringResult.Equal Then
                        whrCls = " unit_code in (select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y')"
                    Else
                        whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "')"
                    End If
                End If

                gv1.CurrentRow.Cells(colBOOK_RATE_UOM).Value = clsUOMInfo.GetFinder(whrCls, gv1.CurrentRow.Cells(colBOOK_RATE_UOM).Value, True)
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'If txtCode.Value = "" Then
        '    myMessages.blankValue("Requisition Number")
        'Else
        '    funPrint()
        'End If
    End Sub



    Private Sub txtBatchNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBatchNo._MYValidating
        Try
            Me.txtBatchNo.Value = clsCustomerMaster.getFinder("CSA_Type='Y'", txtBatchNo.Value, isButtonClicked)
            If clsCommon.myLen(Me.txtBatchNo.Value) > 0 Then
                txtCSAName.Text = clsCustomerMaster.GetName(Me.txtBatchNo.Value, Nothing)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        'and TSPL_PP_BATCH_ORDER_HEAD.location_code in (" + arrLoc + ")
    End Sub


    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        funReset()

    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Dim whrCls As String = ""
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colBOOK_QTY_UOM) Then
                        If clsCommon.myLen(e.Row.Cells(colCSA_ITEM_TYPE).Value) > 0 AndAlso clsCommon.myLen(e.Row.Cells(colItemCode).Value) <= 0 Then
                            whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in (select item_code from tspl_item_master where csa_type='" + clsCommon.myCstr(e.Row.Cells(colCSA_ITEM_TYPE).Value) + "' and isnull(Is_FreshItem,0)<>1))"
                        ElseIf clsCommon.myLen(e.Row.Cells(colItemCode).Value) > 0 Then
                            whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in ('" + clsCommon.myCstr(e.Row.Cells(colItemCode).Value) + "'))"
                        End If

                        e.Row.Cells(colBOOK_QTY_UOM).Value = clsUOMInfo.GetFinder(whrCls, e.Row.Cells(colBOOK_QTY_UOM).Value, False)
                    End If

                    If e.Column Is gv1.Columns(colBOOK_RATE_UOM) Then
                        If clsCommon.myLen(e.Row.Cells(colCSA_ITEM_TYPE).Value) > 0 AndAlso clsCommon.myLen(e.Row.Cells(colItemCode).Value) <= 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(e.Row.Cells(colCSA_ITEM_TYPE).Value), "CPD-DESI GHEE") = CompairStringResult.Equal Then
                                whrCls = " unit_code in (select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y')"
                            Else
                                whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in (select item_code from tspl_item_master where isnull(Is_FreshItem,0)<>1 and csa_type='" + clsCommon.myCstr(e.Row.Cells(colCSA_ITEM_TYPE).Value) + "'))"
                            End If
                        ElseIf clsCommon.myLen(e.Row.Cells(colItemCode).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(e.Row.Cells(colCSA_ITEM_TYPE).Value), "CPD-DESI GHEE") = CompairStringResult.Equal Then
                                whrCls = " unit_code in (select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y')"
                            Else
                                whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(e.Row.Cells(colItemCode).Value) + "')"
                            End If
                        End If

                        e.Row.Cells(colBOOK_RATE_UOM).Value = clsUOMInfo.GetFinder(whrCls, e.Row.Cells(colBOOK_RATE_UOM).Value, False)
                    End If

                    If e.Column Is gv1.Columns(colItemCode) Then
                        gv1.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("active=1 and tspl_item_master.item_type in ('F','T') and isnull(Is_FreshItem,0)<>1", clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), False)
                        gv1.CurrentRow.Cells(colItemdesc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), Nothing)
                        gv1.CurrentRow.Cells(colCSA_ITEM_TYPE).Value = clsCSABooking.GetCSAType(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value))
                        gv1.CurrentRow.Cells(colBOOK_QTY_UOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_ITEM_UOM_DETAIL.UOM_CODE from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.item_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "' AND TSPL_ITEM_UOM_DETAIL.Default_UOM='1' "))
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(Me.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Me.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmiSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiSaveLayout.Click
        If clsCommon.myLen(Me.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = Me.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
        End If
    End Sub

    Private Sub rmiDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiDeleteLayout.Click
        clsGridLayout.DeleteData(Me.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Delete layout successfully", "Information")
    End Sub

    Private Sub rbtn_group_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtn_group.ToggleStateChanged, rbtn_Item.ToggleStateChanged
        If rbtn_group.IsChecked Then
            gv1.Columns(colCSA_ITEM_TYPE).IsVisible = True
            gv1.Columns(colItemCode).IsVisible = False
            gv1.Columns(colItemdesc).IsVisible = False
            gv1.Columns(colCSA_ITEM_TYPE).ReadOnly = False
        ElseIf rbtn_Item.IsChecked Then
            gv1.Columns(colCSA_ITEM_TYPE).IsVisible = True
            gv1.Columns(colItemCode).IsVisible = True
            gv1.Columns(colItemdesc).IsVisible = True
            gv1.Columns(colCSA_ITEM_TYPE).ReadOnly = True
        End If

        If clsCommon.CompairString(MainFormId, clsUserMgtCode.frmCSARequest) = CompairStringResult.Equal Then
            gv1.Columns(colBOOK_RATE_UOM).VisibleInColumnChooser = False
            gv1.Columns(colBOOK_RATE_UOM).IsVisible = False
        End If
    End Sub

    Private Sub txtlocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtlocation._MYValidating
        Try
            Dim whrcls As String = Nothing
            whrcls = clsCommon.myCstr(" Tspl_Location_Master.Type in ('Plant','Depot') and Tspl_Location_Master.Location_Code in (" + arrLoc + ")")
            Me.txtlocation.Value = clsLocation.getFinder(whrcls, txtlocation.Value, isButtonClicked)
            If clsCommon.myLen(Me.txtlocation.Value) > 0 Then
                lbl_location.Text = clsLocation.GetName(Me.txtlocation.Value, Nothing)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class

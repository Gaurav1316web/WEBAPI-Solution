Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
'============= Ticket No: BM00000010016 by Parteek'
Public Class frmWreckage
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim dt As DataTable
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsFormLoad As Boolean = False
    Const Location_Name As String = ""
    Dim DecimalPointQty As Integer = 3
    Dim DecimalPointFatSNFPer As Integer = 3

    '' issue tab columns
    Const colIssueSno As String = "colIssueSno"
    Const colIssue_Code As String = "colIssue_Code"
    Const colFrom_Loaction_Code As String = "colFrom_Loaction_Code"
    Const colTo_Location_Code As String = "colTo_Location_Code"

    Const colIssueItemCode As String = "colIssueItemCode"
    Const colIssueItemName As String = "colIssueItemName"
    Const colIssueItemType As String = "colIssueItemType"
    Const colIssueItemProductType As String = "colIssueItemProductType"
    Const colIssueUom As String = "colIssueUom"
    Const colIssueUOMDesc As String = "colIssueUOMDesc"
    Const colAvail_Qty As String = "colAvail_Qty"

    Const colAvail_FAT_Per As String = "colAvail_FAT_Per"
    Const colAvail_FAT_KG As String = "colAvail_FAT_KG"
    Const colAvail_SNF_Per As String = "colAvail_SNF_Per"
    Const colAvail_SNF_KG As String = "colAvail_SNF_KG"

    Const colIssueRemarks As String = "colIssueRemarks"
    'Const colIssue_Status As String = "colIssue_Status"
    Const colIssue_Fat_Rate As String = "colIssue_Fat_Rate"
    Const colIssue_SNF_Rate As String = "colIssue_SNF_Rate"
    Const colIssue_Fat_Amt As String = "colIssue_Fat_Amt"
    Const colIssue_SNF_Amt As String = "colIssue_SNF_Amt"
    '' end issue tab columns

    '' stage tab
    Const colARSno As String = "colARSno"
    Const colStage_Code As String = "colStage_Code"
    Const colStage_Desc As String = "colStage_Desc"
    Const colReceived_Qty As String = "colReceived_Qty"
    Const colUnit_Code As String = "colUnit_Code"
    Const colSPUnit_Desc As String = "colSPUnit_Desc"
    Const colLog_Sheet_No As String = "colLog_Sheet_No"
    Const colStatus As String = "colStatus"
    Const colSPRemarks As String = "colSPRemarks"
    Const colSPProdCategory As String = "colSPProdCategory"
    Const colSPSection As String = "colSPSection"
    Const colStageBatch_Code As String = "colStageBatch_Code"
    Public objListSP As List(Of clsPPPELogSheetDetail) = New List(Of clsPPPELogSheetDetail)
    '' end stage tab

    Const colLineNo As String = "LineNO"
    'Const colPPCode As String = "PPCode"
    'Const colProdLineCode As String = "ProdLineCode"
    Const colBOMCode As String = "BOMCode"

    Const colItemCode As String = "ItemCode"
    Const colItemDesc As String = "ItemDesc"
    'Const colUOM As String = "UOM"
    Const colBatchQty As String = "BatchQTY"
    Const colReceiptQty As String = "ReceiptQty"
    Const colRejHead As String = "RejHead"
    Const colRejQty As String = "RejQty"
    Const colBreakageHead As String = "BreakageHead"
    Const colBreakageQty As String = "BreakageQty"
    Const colLabTesting As String = "LabTesting"
    Const colFINAL_PROD_Qty As String = "colFINAL_PROD_Qty"
    Const colSP_Loaction_Code As String = "colSP_Loaction_Code"
    Const colSP_Loaction_Desc As String = "colSP_Loaction_Desc"
    'Const colStartTime As String = "StartTime"
    'Const colEndTime As String = "EndTime"
    'Const colMfgDate As String = "MfgDate"
    'Const colExpDate As String = "ExpDate"
    Const colFIFO_Cost As String = "colFIFO_Cost"
    Const colLIFO_Cost As String = "colLIFO_Cost"
    Const colAVG_Cost As String = "colAVG_Cost"
    Const colIsPickAutoSrNo As String = "colIsPickAutoSrNo"
    Const colIsSerialseItem As String = "colIsSerialseItem"

    Const colCost_Method As String = "colCost_Method"

    Const colShiftCode As String = "colShiftCode"
    Const colSectionCode As String = "colSectionCode"

    Const colFAT_Per As String = "colFAT_Per"
    Const colSNF_Per As String = "colSNF_Per"
    Const colFAT_KG As String = "colFAT_KG"
    Const colSNF_KG As String = "colSNF_KG"

    '' qc tab 
    '' QC tab columns
    Const colQCSno As String = "colQCSno"
    Const colQCType As String = "colQCType"
    Const colQCItemCode As String = "colQCItemCode"
    Const colQCItemName As String = "colQCItemName"
    Const colQCparamcode As String = "paramcode"
    Const colQCparam_desc As String = "param_desc"
    Const colQCparam_type As String = "paramtype"
    Const colQCparam_nature As String = "paramnature"
    Const colQCrange1 As String = "range1"
    Const colQCBooleanStatus As String = "colQCBooleanStatus"
    Const colQCAlphaValue As String = "colQCAlphaValue"
    Const colActual_Range As String = "colActual_Range"
    Const colActual_Status As String = "colActual_Status"
    Const colActual_Value As String = "colActual_Value"
    Const colQc_Status As String = "colQc_Status"
    Const colQCremarks As String = "colQCremarks"
    '' qc tab end

    '' wreckage and flashing
    Const colWFSNO As String = "colWFSNO"
    Const colWFItem_Code As String = "colWFItem_Code"
    Const colWFItemName As String = "colWFItemName"
    Const colWFUnit_Code As String = "colWFUnit_Code"
    Const colWFUnit_Desc As String = "colWFUnit_Desc"
    Const colWFBACK_QTY As String = "colWFBACK_QTY"
    Const colWFWRECKAGE_QTY As String = "colWFWRECKAGE_QTY"
    Const colWFLocation_Code As String = "colWFLocation_Code"
    Const colWFAvail_FAT_Per As String = "colWFAvail_FAT_Per"
    Const colWFAvail_SNF_Per As String = "colWFAvail_SNF_Per"
    Const colWFAvail_FAT_KG As String = "colWFAvail_FAT_KG"
    Const colWFAvail_SNF_KG As String = "colWFAvail_SNF_KG"
    Const colWFRemarks As String = "colWFRemarks"
    Const colQty As String = "colQty"
    Const colScrapLocation_Code As String = "colScrapLocation_Code"
    '' end wreckage and flashing

    Dim ButtonToolTip As ToolTip = New ToolTip()


    Public strDocumentNo As String = ""
    'Dim objList As New List(Of clsProductionEntryDetail)
    'Dim obj As New clsProductionEntry
    Dim obj1 As New clsWreckageBooking
    Public Const strCostTransaction As String = "Production Entry"
    Public MOActive As Boolean = False

    Dim arrLoc As String = Nothing
    Dim arrCategory As String = Nothing
    Dim Trans As String = Nothing
    Dim isCellValueChanged As Boolean = False
    Public AllowBookWreckageFromSubLocationOrSection As Boolean = False
#End Region

    Private Sub frmWreckage_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "TSPL_WRECKAGE_ENTRY" + Environment.NewLine + _
                                              "TSPL_WRECKAGE_BOOKING ")
            If btnPost.Enabled = False AndAlso btnSave.Enabled = False Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnunpost.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub frmWreckage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
       
        '--=================added by preeti gupta[29/12/2016]=======================
        AllowBookWreckageFromSubLocationOrSection = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BookWreckageFromSublocationOrSection, clsFixedParameterCode.BookWreckageFromSublocationOrSection, Nothing)) = 1, True, False)
        '==============================
        SetUserMgmtNew()

        DecimalPointQty = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing)))
        If DecimalPointQty <= 0 Then
            DecimalPointQty = 3
        End If
        '' get decimal point for fat snf percentage
        DecimalPointFatSNFPer = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNFPerDecimalPoint, clsFixedParameterCode.ProductionFATSNFPerDecimalPoint, Nothing)))
        If DecimalPointFatSNFPer <= 0 Then
            DecimalPointFatSNFPer = 3
        End If

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")


        LoadBlankWreckageGrid()


        SetLength()
        LOCATIONRIGTHS()
        funReset()
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        'Me.ddltype.DataSource = clsWreckage.GetCategoryTable()
        'Me.ddltype.DisplayMember = "Name"
        'Me.ddltype.ValueMember = "Code"
        'HideColumns()
        'funReset()
        'LoadType()
    End Sub
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then

            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmWreckageBooking)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting

    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtComment.MaxLength = 200
    End Sub

    Public Shared Function GetARType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "Not Complete"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Complete"
        dt.Rows.Add(dr)

        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowToSkipStageQLLogSheetInProd, clsFixedParameterCode.AllowToSkipStageQLLogSheetInProd, Nothing)) > 0) Then
            dr = dt.NewRow()
            dr("Code") = "2"
            dr("Name") = "Skip"
            dt.Rows.Add(dr)
        End If

        Return dt
    End Function

    Public Shared Function GetQCType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Ok"
        dr("Name") = "Ok"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Not Ok"
        dr("Name") = "Not Ok"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Public Shared Function GetQCActualStatus() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Yes"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "No"
        dr("Name") = "No"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub funReset()

        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        btnCancel.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Pending
        dtpDate.Focus()
        ' txtLocation.Enabled = True
        BlankAllControls()
        Me.txtLocation.Value = Nothing
        Me.txtDesc.Text = ""
        Me.txtComment.Text = ""
        lblConsmSectionCode.Text = ""
        Me.txtConsSection.Value = Nothing
        txtCode.MyReadOnly = False
        LOCATIONRIGTHS()
        grdWreckage.DataSource = Nothing
        LoadType()
        LoadBlankWreckageGrid()
        HideColumns()

        'Me.ddltype.SelectedIndex = 0
    End Sub
    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtComment.Text = ""
        dtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        ' grdWreckage.Rows.Clear()
        ' grdWreckage.Rows.Clear()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        '   funReset()
        'txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isInsideLoadData = True
        obj1 = clsWreckageBooking.GetData(strCode, arrLoc, NavTyep)
        If obj1 IsNot Nothing AndAlso clsCommon.myLen(obj1.Wrekage_ENTRY_CODE) > 0 Then

            isNewEntry = False
            If obj1.Publish Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                btnCancel.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0
            LoadBlankWreckageGrid()
            txtCode.Value = obj1.Wrekage_ENTRY_CODE
            Me.ddltype.Text = obj1.Category
            Me.txtDesc.Text = clsCommon.myCstr(obj1.Description)
            Me.txtComment.Text = clsCommon.myCstr(obj1.Comment)
            Me.txtLocation.Value = obj1.Location_Code
            Me.lblLocation.Text = obj1.Location_Name
            Me.txtConsSection.Value = obj1.CONSM_SECTION_CODE
            Me.txtDesc.Text = obj1.Description
            Me.txtComment.Text = obj1.Comment
            Me.dtpDate.Value = obj1.PROD_DATE
            Me.dtDate.Value = obj1.PROD_DATE
            Dim arr_WrckageItem As New List(Of String)
            arr_WrckageItem = New List(Of String)
            If obj1.WFBook IsNot Nothing AndAlso obj1.WFBook.Count > 0 Then
                grdWreckage.Rows.Clear()
                For Each objtr As clsWreckageBooking In obj1.WFBook
                    grdWreckage.Rows.AddNew()
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFSNO).Value = objtr.SNO
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFItem_Code).Value = objtr.Item_Code
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFItemName).Value = objtr.Item_Desc
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFUnit_Code).Value = objtr.Unit_Code
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFUnit_Desc).Value = objtr.Unit_Desc
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).Value = objtr.BACK_QTY
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).Value = objtr.WRECKAGE_QTY
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFLocation_Code).Value = objtr.Location_Code
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFAvail_FAT_KG).Value = objtr.Avail_FAT_KG
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFAvail_FAT_Per).Value = objtr.Avail_FAT_Per
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFAvail_SNF_KG).Value = objtr.Avail_SNF_KG
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFAvail_SNF_Per).Value = objtr.Avail_SNF_Per
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFAvail_FAT_KG).ReadOnly = True
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFAvail_FAT_Per).ReadOnly = False
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFAvail_SNF_KG).ReadOnly = True
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFAvail_SNF_Per).ReadOnly = False
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colQty).Value = objtr.Scrap_Qty
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFRemarks).Value = objtr.Remarks
                    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colScrapLocation_Code).Value = objtr.ScrapLocation
                    If objtr.BACK_QTY > 0 Then
                        grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).ReadOnly = True
                        grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).ReadOnly = False
                        grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFLocation_Code).ReadOnly = False
                    End If
                    If objtr.WRECKAGE_QTY > 0 Then
                        grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).ReadOnly = False
                        grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).ReadOnly = True
                        grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFLocation_Code).ReadOnly = True
                    End If

                    If clsCommon.myLen(objtr.Item_Code) > 0 AndAlso Not arr_WrckageItem.Contains(objtr.Item_Code) Then
                        arr_WrckageItem.Add(objtr.Item_Code)
                    End If
                    If clsCommon.myCstr(obj1.Category) = "Scrap" Then
                        grdWreckage.Columns(colQty).IsVisible = True
                        grdWreckage.Columns(colScrapLocation_Code).IsVisible = True
                        grdWreckage.Columns(colWFBACK_QTY).IsVisible = False
                        grdWreckage.Columns(colWFWRECKAGE_QTY).IsVisible = False
                        grdWreckage.Columns(colWFLocation_Code).IsVisible = False

                        grdWreckage.Columns(colWFAvail_FAT_Per).IsVisible = True
                        grdWreckage.Columns(colWFAvail_FAT_KG).IsVisible = True
                        grdWreckage.Columns(colWFAvail_SNF_Per).IsVisible = True
                        grdWreckage.Columns(colWFAvail_SNF_KG).IsVisible = True

                    ElseIf clsCommon.myCstr(obj1.Category) = "Warehouse Wreckage" Then
                        grdWreckage.Columns(colQty).IsVisible = False
                        grdWreckage.Columns(colWFBACK_QTY).IsVisible = False
                        grdWreckage.Columns(colWFWRECKAGE_QTY).IsVisible = True
                        grdWreckage.Columns(colWFLocation_Code).IsVisible = False
                        grdWreckage.Columns(colScrapLocation_Code).IsVisible = False

                        grdWreckage.Columns(colWFAvail_FAT_Per).IsVisible = False
                        grdWreckage.Columns(colWFAvail_FAT_KG).IsVisible = False
                        grdWreckage.Columns(colWFAvail_SNF_Per).IsVisible = False
                        grdWreckage.Columns(colWFAvail_SNF_KG).IsVisible = False
                    Else
                        grdWreckage.Columns(colQty).IsVisible = False
                        grdWreckage.Columns(colScrapLocation_Code).IsVisible = False
                        grdWreckage.Columns(colWFBACK_QTY).IsVisible = True
                        grdWreckage.Columns(colWFWRECKAGE_QTY).IsVisible = True
                        grdWreckage.Columns(colWFLocation_Code).IsVisible = True
                        grdWreckage.Columns(colWFAvail_FAT_Per).IsVisible = True
                        grdWreckage.Columns(colWFAvail_FAT_KG).IsVisible = True
                        grdWreckage.Columns(colWFAvail_SNF_Per).IsVisible = True
                        grdWreckage.Columns(colWFAvail_SNF_KG).IsVisible = True
                    End If
                Next
                txtCode.MyReadOnly = True
                isNewEntry = False
                btnSave.Text = "Update"
                grdWreckage.Rows.AddNew()
            Else

                btnSave.Text = "Save"
                grdWreckage.Rows.AddNew()
            End If
        End If
        isInsideLoadData = False
    End Sub



    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
        LoadBlankWreckageGrid()
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
                If (clsWreckageBooking.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
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



    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        funReset()
        'LoadBlankWreckageGrid()
    End Sub

    Private Sub grdWreckage_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles grdWreckage.CellFormatting
        'If clsCommon.myCdbl(grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value) <= 0 AndAlso clsCommon.myCdbl(grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value) <= 0 Then
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).ReadOnly = False
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).ReadOnly = False
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFLocation_Code).ReadOnly = False
        'End If
        'If clsCommon.myCdbl(grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value) > 0 Then
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).ReadOnly = False
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).ReadOnly = True
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFLocation_Code).ReadOnly = True

        'End If
        'If clsCommon.myCdbl(grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value) > 0 Then
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).ReadOnly = True
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).ReadOnly = False
        '    grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFLocation_Code).ReadOnly = False

        'End If
    End Sub

    Private Sub grdWreckage_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles grdWreckage.CurrentColumnChanged
        If grdWreckage.RowCount > 0 Then
            Dim intCurrRow As Integer = grdWreckage.CurrentRow.Index
            grdWreckage.CurrentRow.Cells(colWFSNO).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvWreckage.Rows.Count - 1 Then
                grdWreckage.Rows.AddNew()
                grdWreckage.CurrentRow = gvWreckage.Rows(intCurrRow)
            End If
        End If
    End Sub


    Private Sub LoadBlankWreckageGrid()
        grdWreckage.Rows.Clear()
        grdWreckage.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colWFSNO
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        grdWreckage.MasterTemplate.Columns.Add(reposno)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colWFItem_Code
        repoicode.Width = 100
        repoicode.HeaderText = "Item Code"
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoicode.ReadOnly = False
        grdWreckage.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colWFItemName
        repoiname.Width = 150
        repoiname.HeaderText = "Item Description"
        repoiname.ReadOnly = True
        grdWreckage.MasterTemplate.Columns.Add(repoiname)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colWFUnit_Code
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = False
        grdWreckage.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colWFUnit_Desc
        repouomname.Width = 100
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        grdWreckage.MasterTemplate.Columns.Add(repouomname)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.Name = colQty
        repoQty.Width = 140
        repoQty.HeaderText = "Quantity"
        repoQty.DecimalPlaces = DecimalPointQty
        repoQty.IsVisible = False
        repoQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        grdWreckage.MasterTemplate.Columns.Add(repoQty)

        Dim repoScrapLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoScrapLocationCode.FormatString = ""
        repoScrapLocationCode.Name = colScrapLocation_Code
        repoScrapLocationCode.Width = 100
        repoScrapLocationCode.HeaderText = "Location"
        repoScrapLocationCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoScrapLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoScrapLocationCode.ReadOnly = False
        grdWreckage.MasterTemplate.Columns.Add(repoScrapLocationCode)

        Dim repoAvail_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_Qty.FormatString = ""
        repoAvail_Qty.Name = colWFBACK_QTY
        repoAvail_Qty.Width = 120
        repoAvail_Qty.HeaderText = "Back Quantity"
        repoAvail_Qty.DecimalPlaces = DecimalPointQty
        repoAvail_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        grdWreckage.MasterTemplate.Columns.Add(repoAvail_Qty)

        Dim repoWr_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoWr_Qty.FormatString = ""
        repoWr_Qty.Name = colWFWRECKAGE_QTY
        repoWr_Qty.Width = 120
        repoWr_Qty.HeaderText = "Wreckage Quantity"
        repoWr_Qty.DecimalPlaces = DecimalPointQty
        repoWr_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        grdWreckage.MasterTemplate.Columns.Add(repoWr_Qty)


        Dim repoFromLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLocationCode.FormatString = ""
        repoFromLocationCode.Name = colWFLocation_Code
        repoFromLocationCode.Width = 100
        repoFromLocationCode.HeaderText = "Back To Location"
        repoFromLocationCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoFromLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFromLocationCode.ReadOnly = False
        grdWreckage.MasterTemplate.Columns.Add(repoFromLocationCode)


        Dim repoAvail_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colWFAvail_FAT_Per
        repoAvail_FAT_Per.Width = 100
        repoAvail_FAT_Per.HeaderText = "FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = False
        grdWreckage.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        Dim repoAvail_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_KG.FormatString = ""
        repoAvail_FAT_KG.Name = colWFAvail_FAT_KG
        repoAvail_FAT_KG.Width = 100
        repoAvail_FAT_KG.HeaderText = "FAT KG"
        repoAvail_FAT_KG.DecimalPlaces = DecimalPointQty
        repoAvail_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_FAT_KG.ReadOnly = True
        grdWreckage.MasterTemplate.Columns.Add(repoAvail_FAT_KG)

        Dim repoAvail_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_Per.FormatString = ""
        repoAvail_SNF_Per.Name = colWFAvail_SNF_Per
        repoAvail_SNF_Per.Width = 120
        repoAvail_SNF_Per.HeaderText = "SNF%"
        repoAvail_SNF_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_SNF_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_SNF_Per.ReadOnly = False
        grdWreckage.MasterTemplate.Columns.Add(repoAvail_SNF_Per)

        Dim repoAvail_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_KG.FormatString = ""
        repoAvail_SNF_KG.Name = colWFAvail_SNF_KG
        repoAvail_SNF_KG.Width = 120
        repoAvail_SNF_KG.HeaderText = "SNF KG"
        repoAvail_SNF_KG.DecimalPlaces = DecimalPointQty
        repoAvail_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_SNF_KG.ReadOnly = True
        grdWreckage.MasterTemplate.Columns.Add(repoAvail_SNF_KG)

        Dim repoIssueRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueRemarks.FormatString = ""
        repoIssueRemarks.Name = colWFRemarks
        repoIssueRemarks.Width = 120
        repoIssueRemarks.MaxLength = 200
        repoIssueRemarks.HeaderText = "Remarks"
        grdWreckage.MasterTemplate.Columns.Add(repoIssueRemarks)

        '-------------------------------------------------

        grdWreckage.AllowDeleteRow = True
        grdWreckage.AllowAddNewRow = False
        'gvWreckage.ShowGroupPanel = False
        grdWreckage.AllowColumnReorder = False
        grdWreckage.AllowRowReorder = False
        grdWreckage.EnableSorting = False
        grdWreckage.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        grdWreckage.MasterTemplate.ShowRowHeaderColumn = False
        grdWreckage.EnableFiltering = False
        grdWreckage.Rows.AddNew()
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Function SaveData(ByVal ChekBtnPost As Boolean) As Boolean
        Try
            If AllowToSave() Then
                Dim obj As New clsWreckageBooking


                obj.PRODUCTION_ENTRY_CODE = Me.txtCode.Value
                obj.Item_Desc = Me.txtDesc.Text
                obj.PROD_DATE = Me.dtDate.Value
                obj.Description = Me.txtDesc.Text
                obj.Location_Code = clsCommon.myCstr(Me.txtLocation.Value)
                obj.Comment = Me.txtComment.Text
                obj.Category = Me.ddltype.Text
                obj.CONSM_SECTION_CODE = Me.txtConsSection.Value

                obj.WFBook = New List(Of clsWreckageBooking)

                '' assign value to wreckage and flashing
                For Each grow As GridViewRowInfo In grdWreckage.Rows
                    Dim objtr1 As New clsWreckageBooking
                    objtr1.SNO = CInt(grow.Cells(colWFSNO).Value)
                    objtr1.Item_Code = clsCommon.myCstr(grow.Cells(colWFItem_Code).Value)
                    objtr1.Unit_Code = clsCommon.myCstr(grow.Cells(colWFUnit_Code).Value)
                    objtr1.BACK_QTY = clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value)
                    objtr1.WRECKAGE_QTY = clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value)
                    objtr1.Location_Code = clsCommon.myCstr(grow.Cells(colWFLocation_Code).Value)

                    objtr1.Avail_FAT_KG = clsCommon.myCdbl(grow.Cells(colWFAvail_FAT_KG).Value)
                    objtr1.Avail_FAT_Per = clsCommon.myCdbl(grow.Cells(colWFAvail_FAT_Per).Value)
                    objtr1.Avail_SNF_KG = clsCommon.myCdbl(grow.Cells(colWFAvail_SNF_KG).Value)
                    objtr1.Avail_SNF_Per = clsCommon.myCdbl(grow.Cells(colWFAvail_SNF_Per).Value)
                    objtr1.Scrap_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objtr1.ScrapLocation = clsCommon.myCstr(grow.Cells(colScrapLocation_Code).Value)
                    objtr1.Remarks = clsCommon.myCstr(grow.Cells(colWFRemarks).Value).Replace("'", "`")
                    If clsCommon.myLen(objtr1.Item_Code) > 0 Then
                        obj.WFBook.Add(objtr1)
                    End If
                Next

                Dim issaved As Boolean = False
                issaved = clsWreckageBooking.SaveData(obj, isNewEntry, clsCommon.myCstr(txtCode.Value))


                If issaved = True Then
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.Wrekage_ENTRY_CODE, NavigatorType.Current)
                    Return True
                End If

                Return False
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function

    Function AllowToSave(Optional ByVal isPost As Boolean = False) As Boolean

        If AllowFutureDateTransaction(dtDate.Value, Nothing) = False Then
            Return False
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            myMessages.blankValue("Location Code")
            txtLocation.Focus()
            Return False
        End If
        If AllowBookWreckageFromSubLocationOrSection AndAlso clsCommon.CompairString(ddltype.Text, "Warehouse Wreckage") <> CompairStringResult.Equal Then
            If clsCommon.myLen(txtConsSection.Value) <= 0 Then
                myMessages.blankValue("Section Code")
                txtConsSection.Focus()
                Return False
            End If
        End If

        For Each grow As GridViewRowInfo In grdWreckage.Rows
            If clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value) > 0 AndAlso clsCommon.CompairString(ddltype.Text, "Warehouse Wreckage") <> CompairStringResult.Equal Then
                If clsCommon.myLen(grow.Cells(colWFLocation_Code).Value) <= 0 Then
                    myMessages.blankValue("Enter Back to Location at line no- " & (grow.Index + 1) & "")
                    grow.Cells(colWFLocation_Code).ReadOnly = False
                    Return False
                End If

            End If
        Next
        '====================Added by preeti gupta=====================[stop dublicate Item]
        Dim Icode As String = Nothing
        Dim oldicode As String = ""
        If clsCommon.myLen(colWFItem_Code) <= 0 Then
            grdWreckage.CurrentRow = grdWreckage.Rows(0)
            clsCommon.MyMessageBoxShow("Fill atleast one row in grid.")
            Return False
        End If
        For ii As Integer = 0 To grdWreckage.Rows.Count - 1
            Icode = clsCommon.myCstr(grdWreckage.Rows(ii).Cells(colWFItem_Code).Value)

            If clsCommon.myLen(Icode) > 0 Then

                For jj As Integer = ii + 1 To grdWreckage.Rows.Count - 1
                    oldicode = clsCommon.myCstr(grdWreckage.Rows(jj).Cells(colWFItem_Code).Value)

                    If clsCommon.CompairString(Icode, oldicode) = CompairStringResult.Equal Then
                        grdWreckage.CurrentRow = grdWreckage.Rows(jj + 1)
                        clsCommon.MyMessageBoxShow("Duplicate item at row no. " + clsCommon.myCstr(jj + 1) + "")
                        Return False
                    End If
                Next
            End If
        Next
        '-----------------qc grid----------------------------------------------
        'If isPost = True Then
        'Dim dt As DataTable = clsProductionEntryRM.GetRM(txtCode.Value)
        For Each grow As GridViewRowInfo In grdWreckage.Rows
            If clsCommon.myLen(grow.Cells(colWFItem_Code).Value) <= 0 Then
                Continue For
            End If
            ''check balance for wreckage qty
            If clsCommon.CompairString(ddltype.SelectedValue, "Wreckage") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value) > 0 Then
                    Dim Bal As Decimal = 0
                    If clsCommon.CompairString(clsItemMaster.GetItemProductType(grow.Cells(colWFItem_Code).Value, Nothing), "MI") = CompairStringResult.Equal Then
                        Bal = clsInventoryMovementNew.getBalance(grow.Cells(colWFItem_Code).Value, txtLocation.Value, txtConsSection.Value, txtCode.Value, dtDate.Value, Nothing, grow.Cells(colWFUnit_Code).Value)
                    Else
                        Bal = clsItemLocationDetails.getBalance(grow.Cells(colWFItem_Code).Value, txtLocation.Value, txtCode.Value, dtDate.Value, Nothing, grow.Cells(colWFUnit_Code).Value, 0)
                    End If
                    If Bal < clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value) Then
                        Throw New Exception("Item Code: " & grow.Cells(colWFItem_Code).Value & " Entered Qty:" & clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value) & " Available Qty: " & Bal & "")
                    End If
                End If
            End If

            'Dim found As Boolean = False
            'For Each dr As DataRow In dt.Rows
            '    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colWFItem_Code).Value), clsCommon.myCstr(dr.Item("Consm_Item_Code"))) = CompairStringResult.Equal Then
            '        dr.Item("CONSM_QTY") = ((clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value) + clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value)) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(grow.Cells(colWFItem_Code).Value), clsCommon.myCstr(grow.Cells(colWFUnit_Code).Value), Nothing) + clsCommon.myCdbl(dr.Item("CONSM_QTY")) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsCommon.myCstr(dr.Item("Consm_Unit_Code")), Nothing)) / clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsCommon.myCstr(dr.Item("Consm_Unit_Code")), Nothing)
            '        found = True
            '        Exit For
            '    End If
            'Next
            'If found = False Then
            '    Dim drNew As DataRow = dt.NewRow
            '    drNew.Item("Consm_Item_Code") = clsCommon.myCstr(grow.Cells(colWFItem_Code).Value)
            '    drNew.Item("CONSM_QTY") = clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value) + clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value)
            '    drNew.Item("Consm_Unit_Code") = clsCommon.myCstr(grow.Cells(colWFUnit_Code).Value)
            '    drNew.Item("FAT_Pers") = clsCommon.myCdbl(grow.Cells(colWFAvail_FAT_Per).Value)
            '    drNew.Item("SNF_Pers") = clsCommon.myCdbl(grow.Cells(colWFAvail_SNF_Per).Value)
            '    dt.Rows.Add(drNew)

            'Else

            'End If
        Next

        'End If
        Return True
    End Function

    Private Sub grdWreckage_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdWreckage.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is grdWreckage.Columns(colWFItem_Code) Then
                    isCellValueChanged = True
                    Dim arrList As New ArrayList
                    Dim arr1 As New ArrayList
                    Dim intRow As Integer = grdWreckage.CurrentRow.Index
                    arr1.Add(clsCommon.myCstr(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value))
                    If clsCommon.CompairString(ddltype.Text, "Wreckage") = CompairStringResult.Equal Then
                        If (clsCommon.myLen(txtConsSection.Value) > 0) Then
                            arr1 = clsWreckageBooking.getSectionStockItemMultipleFinder("", Me.txtConsSection.Value, ddltype.Text, arrList)
                        Else
                            arr1 = clsWreckageBooking.getSectionStockItemMultipleFinder("", Me.txtLocation.Value, ddltype.Text, arrList)
                        End If
                    ElseIf clsCommon.CompairString(ddltype.Text, "Scrap") = CompairStringResult.Equal Then
                        arr1 = clsWreckageBooking.getItemFinder("", "", ddltype.Text, arrList)
                    ElseIf clsCommon.CompairString(ddltype.Text, "Warehouse Wreckage") = CompairStringResult.Equal Then
                        arr1 = clsWreckageBooking.getFGItemFinder("", "", ddltype.Text, arrList)
                    End If




                    If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                        For ii As Integer = 0 To arr1.Count - 1
                            If ii = 0 Then
                                grdWreckage.Rows(intRow).Cells(colWFItem_Code).Value = clsCommon.myCstr(arr1(ii))
                                grdWreckage.Rows(intRow).Cells(colWFItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(arr1(ii)), Nothing) ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Desc from tspl_item_master  left outer join TSPL_WARRANTY_MASTER on TSPL_WARRANTY_MASTER.Code=tspl_item_master.Warranty_Code where  tspl_item_master.item_code = '" + clsCommon.myCstr(arr1(ii)) + "'"))
                                grdWreckage.Rows(intRow).Cells(colWFUnit_Code).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(arr1(ii)), Nothing) ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Unit_Code from tspl_item_master  left outer join TSPL_WARRANTY_MASTER on TSPL_WARRANTY_MASTER.Code=tspl_item_master.Warranty_Code where  tspl_item_master.item_code = '" + clsCommon.myCstr(arr1(ii)) + "'"))
                                grdWreckage.Rows(intRow).Cells(colWFUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(grdWreckage.Rows(intRow).Cells(colWFUnit_Code).Value))
                                If clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MI") = CompairStringResult.Equal Or clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MP") = CompairStringResult.Equal Then
                                    '  FillQCGrid(True, grdWreckage.Rows(intRow).Cells(colWFItem_Code).Value)
                                End If
                            Else
                                grdWreckage.Rows.AddNew()

                                grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFSNO).Value = clsCommon.myCdbl(grdWreckage.Rows(intRow).Cells(colWFSNO).Value)
                                grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFItem_Code).Value = clsCommon.myCstr(arr1(ii))
                                grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(arr1(ii)), Nothing)
                                grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFUnit_Code).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(arr1(ii)), Nothing)
                                grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFUnit_Code).Value))
                                If clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MI") = CompairStringResult.Equal Or clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MP") = CompairStringResult.Equal Then
                                    '  FillQCGrid(True, grdWreckage.Rows(grdWreckage.Rows.Count - 1).Cells(colWFItem_Code).Value)
                                End If

                                grdWreckage.Rows.Move(grdWreckage.Rows.Count - 1, intRow)
                            End If ''ii cond

                        Next
                        grdWreckage.CurrentRow = grdWreckage.Rows(intRow)
                        grdWreckage.CurrentColumn = grdWreckage.Columns(colWFItem_Code)
                        If clsCommon.myLen(grdWreckage.Columns(colWFItem_Code)) > 0 Then
                            For jj As Integer = 0 To grdWreckage.Rows.Count - 1
                                grdWreckage.Rows(jj).Cells(colWFSNO).Value = clsCommon.myCdbl(jj) + 1
                            Next
                        End If
                    Else
                        grdWreckage.CurrentRow.Cells(colWFItem_Code).Value = ""
                        grdWreckage.CurrentRow.Cells(colWFItemName).Value = ""
                    End If

                    grdWreckage.Rows.AddNew()
                    isCellValueChanged = False

                End If
                'ALL WORK DONE BY PREETI GUPTA , BY KUNAL'S SYSTEM ON 28-12-2016
                If e.Column Is grdWreckage.Columns(colWFUnit_Code) Then
                    isCellValueChanged = True
                    OpenUnitCode(True)
                    isCellValueChanged = False
                End If


                If e.Column Is grdWreckage.Columns(colScrapLocation_Code) Then
                    isCellValueChanged = True
                    'If ddltype.Text = "Wreckage" Then
                    '    grdWreckage.CurrentRow.Cells(colScrapLocation_Code).Value = clsLocation.getFinder("((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtLocation.Value & "')", grdWreckage.CurrentRow.Cells(colScrapLocation_Code).Value, False)
                    'ElseIf ddltype.Text = "Scrap" Then
                    grdWreckage.CurrentRow.Cells(colScrapLocation_Code).Value = clsLocation.getFinder(" (((Is_Section='Y' or Is_Sub_Location='Y')) and Main_Location_Code='" & txtLocation.Value & "') OR Location_Code='" & txtLocation.Value & "'", grdWreckage.CurrentRow.Cells(colScrapLocation_Code).Value, False)
                    'End If
                    isCellValueChanged = False
                End If
                '===========================================================================================================================================
                If e.Column Is grdWreckage.Columns(colWFLocation_Code) Then
                    isCellValueChanged = True

                    grdWreckage.CurrentRow.Cells(colWFLocation_Code).Value = clsLocation.getFinder(" (((Is_Section='Y' or Is_Sub_Location='Y')) and Main_Location_Code='" & txtLocation.Value & "') OR Location_Code='" & txtLocation.Value & "'", grdWreckage.CurrentRow.Cells(colWFLocation_Code).Value, False)

                    isCellValueChanged = False
                End If


                If e.Column Is grdWreckage.Columns(colWFBACK_QTY) Then
                    isCellValueChanged = True
                    grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).ReadOnly = True
                    grdWreckage.CurrentRow.Cells(colWFLocation_Code).ReadOnly = False
                    If grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100

                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Wreckage")
                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Wreckage")
                    ElseIf grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100

                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Wreckage")
                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Wreckage")
                    End If

                    isCellValueChanged = False
                End If
                If e.Column Is grdWreckage.Columns(colWFWRECKAGE_QTY) Then
                    isCellValueChanged = True
                    grdWreckage.CurrentRow.Cells(colWFBACK_QTY).ReadOnly = True
                    grdWreckage.CurrentRow.Cells(colWFLocation_Code).ReadOnly = True

                    If grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFUnit_Code).Value, grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value, grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, Nothing)
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFUnit_Code).Value, grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value, grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, Nothing)

                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Wreckage")
                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Wreckage")
                    ElseIf grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100

                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Wreckage")
                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Wreckage")
                    End If
                    isCellValueChanged = False
                End If
                '' Scrap
                If e.Column Is grdWreckage.Columns(colQty) Then
                    isCellValueChanged = True
                    'grdWreckage.CurrentRow.Cells(colQty).ReadOnly = True

                    If grdWreckage.CurrentRow.Cells(colQty).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colQty).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colQty).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFUnit_Code).Value, grdWreckage.CurrentRow.Cells(colQty).Value, grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, Nothing)
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFUnit_Code).Value, grdWreckage.CurrentRow.Cells(colQty).Value, grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, Nothing)

                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Scarp")
                        UpdateBatchFatSNF(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Scrap")
                    End If
                    isCellValueChanged = False
                End If
                '' End
                If e.Column Is grdWreckage.Columns(colWFAvail_FAT_Per) Then
                    isCellValueChanged = True
                    grdWreckage.CurrentRow.Cells(colWFBACK_QTY).ReadOnly = True
                    grdWreckage.CurrentRow.Cells(colWFLocation_Code).ReadOnly = True

                    If grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                    ElseIf grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                    ElseIf grdWreckage.CurrentRow.Cells(colQty).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colQty).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colQty).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                    End If
                    isCellValueChanged = False
                End If

                If e.Column Is grdWreckage.Columns(colWFAvail_SNF_Per) Then
                    isCellValueChanged = True
                    grdWreckage.CurrentRow.Cells(colWFBACK_QTY).ReadOnly = True
                    grdWreckage.CurrentRow.Cells(colWFLocation_Code).ReadOnly = True

                    If grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                    ElseIf grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                    ElseIf grdWreckage.CurrentRow.Cells(colQty).Value > 0 Then
                        grdWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = grdWreckage.CurrentRow.Cells(colQty).Value * grdWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        grdWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = grdWreckage.CurrentRow.Cells(colQty).Value * grdWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                    End If
                    isCellValueChanged = False
                End If

            End If

        End If

    End Sub

    Sub UpdateBatchFatSNF(ByVal Item_Code As String, ByVal Value As Decimal, ByVal Type As String, ByVal QC_Type As String)

        If clsCommon.CompairString(QC_Type, "Wreckage") = CompairStringResult.Equal Then
            '' update fat/snf in add/remove tab
            For Each grow As GridViewRowInfo In grdWreckage.Rows
                If clsCommon.CompairString(grow.Cells(colWFItem_Code).Value, Item_Code) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
                        grow.Cells(colWFAvail_FAT_Per).Value = Value
                        grow.Cells(colWFAvail_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colWFUnit_Code).Value, IIf(clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value) > 0, clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value), clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value)), grow.Cells(colWFAvail_FAT_Per).Value, Nothing)
                    ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
                        grow.Cells(colWFAvail_SNF_Per).Value = Value
                        grow.Cells(colWFAvail_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colWFUnit_Code).Value, IIf(clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value) > 0, clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value), clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value)), grow.Cells(colWFAvail_SNF_Per).Value, Nothing)
                    End If
                    Exit Sub
                End If
            Next
        End If ''end cond.

    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(arrLoc) > 0 Then
                WhrCls += "  and  Location_Code in (" + arrLoc + ")"
            End If
            txtLocation.Value = clsLocation.getFinder(WhrCls, Me.txtLocation.Value, isButtonClicked)
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsLocation.GetName(Me.txtLocation.Value, Nothing)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim check As Boolean = False
        check = clsWreckageBooking.CheckValidCode(Me.txtCode.Value)

        If check Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsWreckageBooking.GetFinder(" TSPL_WRECKAGE_Entry.LOCATION_CODE in (" + arrLoc + ") ", txtCode.Value, isButtonClicked))
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            funReset()
        End If
    End Sub


    Private Sub txtConsSection__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtConsSection._MYValidating

        Try
            Dim WhrCls As String = " Location_Type='Physical' "
            If clsCommon.myLen(arrLoc) > 0 Then
                WhrCls += "  and  Main_Location_Code in (" + arrLoc + ")"
            End If
            txtConsSection.Value = clsWreckageBooking.GetFind(WhrCls, Me.txtLocation.Value, isButtonClicked)
            If clsCommon.myLen(txtConsSection.Value) > 0 Then
                lblConsmSectionCode.Text = clsWreckageBooking.GetName(WhrCls, Me.txtConsSection.Value, Nothing)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If AllowToSave(True) And SaveData(True) Then
                    If FrmMainTranScreen.ValidateTransactionAccToFinYear("Production Entry", dtpDate.Value) = False Then
                        Exit Sub
                    End If
                    clsWreckageBooking.Post(Form_ID, txtCode.Value, arrLoc, True)
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub ddltype_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddltype.SelectedIndexChanged
        HideColumns()
    End Sub
   
    Sub LoadType()

        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Wreckage")
        dt.Rows.Add("Scrap")
        dt.Rows.Add("Warehouse Wreckage")
        ddltype.DataSource = dt
        ddltype.ValueMember = "Code"
        ddltype.DisplayMember = "Code"

    End Sub
    Sub HideColumns()
        Try

            'If ddltype.SelectedItem Is Nothing Then
            '    Exit Sub
            'End If
            If ddltype.Text = "Wreckage" Then
                grdWreckage.Columns(colQty).IsVisible = False
                grdWreckage.Columns(colWFBACK_QTY).IsVisible = True
                grdWreckage.Columns(colWFWRECKAGE_QTY).IsVisible = True
                grdWreckage.Columns(colWFLocation_Code).IsVisible = True
                grdWreckage.Columns(colScrapLocation_Code).IsVisible = False
                grdWreckage.Columns(colWFAvail_FAT_Per).IsVisible = True
                grdWreckage.Columns(colWFAvail_FAT_KG).IsVisible = True
                grdWreckage.Columns(colWFAvail_SNF_Per).IsVisible = True
                grdWreckage.Columns(colWFAvail_SNF_KG).IsVisible = True
                'grdWreckage.Columns(colQty).HeaderText = "Quantity"
            ElseIf ddltype.Text = "Scrap" Then
                grdWreckage.Columns(colQty).IsVisible = True
                grdWreckage.Columns(colWFBACK_QTY).IsVisible = False
                grdWreckage.Columns(colWFWRECKAGE_QTY).IsVisible = False
                grdWreckage.Columns(colWFLocation_Code).IsVisible = False
                grdWreckage.Columns(colScrapLocation_Code).IsVisible = True
                grdWreckage.Columns(colWFAvail_FAT_Per).IsVisible = True
                grdWreckage.Columns(colWFAvail_FAT_KG).IsVisible = True
                grdWreckage.Columns(colWFAvail_SNF_Per).IsVisible = True
                grdWreckage.Columns(colWFAvail_SNF_KG).IsVisible = True
                ' grdWreckage.Columns(colQty).HeaderText = "Quantity"
            ElseIf ddltype.Text = "Warehouse Wreckage" Then
                grdWreckage.Columns(colQty).IsVisible = False
                grdWreckage.Columns(colWFBACK_QTY).IsVisible = False
                grdWreckage.Columns(colWFWRECKAGE_QTY).IsVisible = True
                grdWreckage.Columns(colWFLocation_Code).IsVisible = False
                grdWreckage.Columns(colScrapLocation_Code).IsVisible = False
                grdWreckage.Columns(colWFAvail_FAT_Per).IsVisible = False
                grdWreckage.Columns(colWFAvail_FAT_KG).IsVisible = False
                grdWreckage.Columns(colWFAvail_SNF_Per).IsVisible = False
                grdWreckage.Columns(colWFAvail_SNF_KG).IsVisible = False
                'grdWreckage.Columns(colQty).HeaderText = "Wreckage Quantity"
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenUnitCode(ByVal isButtonClick As Boolean)
       
        Dim WhrCls As String = " tspl_item_master.Item_Code='" & grdWreckage.CurrentRow.Cells(colWFItem_Code).Value & "'  "
            Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_ITEM_UOM_DETAIL.UOM_Description as [UOM Description] from TSPL_ITEM_UOM_DETAIL left outer join TSPL_ITEM_MASTER on tspl_item_master.item_code=tspl_item_uom_detail.item_code  "
        grdWreckage.CurrentRow.Cells(colWFUnit_Code).Value = clsCommon.ShowSelectForm("TSPL_UOM_HEAD", qry, "Code", WhrCls, grdWreckage.CurrentRow.Cells(colWFUnit_Code).Value, "", False)
        grdWreckage.CurrentRow.Cells(colWFUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(grdWreckage.CurrentRow.Cells(colWFUnit_Code).Value))
    End Sub

    Sub OpenItemCode(ByVal isButtonClick As Boolean)

        Dim WhrCls As String = " and tspl_item_master.Active ='1' "
        Dim qry As String = "Select Item_Code as Code, Item_Desc as Description from TSPL_ITEM_MASTER "
        grdWreckage.CurrentRow.Cells(colWFItem_Code).Value = clsCommon.ShowSelectForm("WreckageItem", qry, "Code", WhrCls, grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, "", False)
        grdWreckage.CurrentRow.Cells(colWFItemName).Value = clsItemMaster.GetItemName(grdWreckage.CurrentRow.Cells(colWFItem_Code).Value, Nothing)
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub
    Function CancelData() As Boolean
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
            clsWreckageBooking.CancelData(Me.Form_ID, txtCode.Value)
            clsCommon.MyMessageBoxShow("Successfully Cancelled", Me.Text)
            FunReset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
    ''richa BHA/17/08/18-000454
    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_WRECKAGE_ENTRY where Posted='0' and WRECKAGE_ENTRY_CODE='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If

            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If


                If clsWreckageBooking.UnpostData(txtCode.Value, Me.Form_ID) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Unpost and Recreated", Me.Text)
                        btnunpost.Visible = False
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    ' Ticket No : TEC/29/10/18-000347 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Receipt Code")
                Exit Sub
            End If
            clsERPFuncationality.ShowHistoryData(txtCode.Value, "WRECKAGE_ENTRY_CODE", "TSPL_WRECKAGE_ENTRY")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
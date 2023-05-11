' Created By Pankaj Jha on 24/06/204 Against Ticket No: BM00000002720
''richa agarwal BM00000009808,BM00000009809
'======================Modifiy by preeti gupta against ticket no[BM00000008127]
'' Parteek ticket no BM00000009863 
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmPoBulkProc
    Inherits FrmMainTranScreen
    Dim allowManualrate As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isIntimationReqd As Integer = 0
    Dim TankerFromMaster As Integer = 0
    Public Const colSlNo As String = "SLNO"
    Public Const colChamberSealNo As String = "colChamberSealNo"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colRate As String = "colRate"
    Public Const colUOM As String = "UOM"
    Public Const colDIPValue As String = "colDIPValue"
    Public Const ColSealNo As String = "ColSealNo"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colMilkTypeCode As String = "colMilkTypeCode"
    Public Const colDIPStatus As String = "colDIPStatus"
    Public Const colSampleLifted As String = "colSampleLifted"
    Public strDocCode As String = ""
    Public isCellValueChangedOpen = False
    Public strLoggedInTo As String = String.Empty ' It Will Store Either MCC or Plant as Login Location
    Public strLoginMccOrPlantCode As String = String.Empty 'It Will store the Location Code of Currently Logged In Mcc or Pant
    Public strLoginMccOrPlantDesc As String = String.Empty 'It Will store the Location Desc of Currently Logged In Mcc or Pant
    Public errorControl As clsErrorControl = New clsErrorControl()
    Public obj As clsPOBulkProc = Nothing
    Dim insideLoadData As Boolean = False

 

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Me.Close()
        'Me.Dispose()
        'GC.Collect()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub fndLocationBulk__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocationBulk._MYValidating
        Dim strLocations = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strLocations = "and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndLocationBulk.Value = clsLocation.getFinder("(type='PLANT' or Location_category='MCC')" & strLocations, fndLocationBulk.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationBulk.Value) > 0 Then
            lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
        Else
            lblLocationDecBulk.Text = ""
        End If
        strLocations = Nothing
    End Sub
    Sub UpdateCurrentLoginStatus()
        Dim strQry As String = "select case when isnull(Type,'')='PLANT' then type when ISNULL(Location_Category ,'')='MCC' then  Location_Category else 'NA'end    from TSPL_LOCATION_MASTER left outer join  tspl_user_master on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where TSPL_USER_MASTER.User_Code ='" & objCommonVar.CurrentUserCode & "'"
        strLoggedInTo = clsDBFuncationality.getSingleValue(strQry)
        If clsCommon.CompairString(strLoggedInTo, "MCC") = CompairStringResult.Equal Or clsCommon.CompairString(strLoggedInTo, "PLANT") = CompairStringResult.Equal Then
            Dim qry As String = "select location_Code  from TSPL_LOCATION_MASTER left outer join  tspl_user_master on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where TSPL_USER_MASTER.User_Code ='" & objCommonVar.CurrentUserCode & "'"
            strLoginMccOrPlantCode = clsDBFuncationality.getSingleValue(qry)
            strLoginMccOrPlantDesc = clsLocation.GetName(strLoginMccOrPlantCode, Nothing)
        Else
            strLoginMccOrPlantCode = "NA"
            strLoginMccOrPlantDesc = "NA"
        End If
        'lblLoggedMccOrPlantName.Text = strLoginMccOrPlantDesc & "(Location: " & strLoggedInTo & ")"
        strQry = Nothing
    End Sub
    Private Sub fndVendorBulk__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndVendorBulk._MYValidating
        Try
            If clsCommon.CompairString(cmbGEType.SelectedValue, "") = CompairStringResult.Equal Then
                errorControl.SetError(cmbGEType, "Please Select PO  type ")
                cmbGEType.Focus()
                Throw New Exception("Please Select PO  type before selecting vendor ")
            End If
            If clsCommon.CompairString(cmbGEType.SelectedValue, "P") = CompairStringResult.Equal Then
                fndVendorBulk.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Vendor_Type_CHA in ('M') ", fndVendorBulk.Value, isButtonClicked)
            ElseIf clsCommon.CompairString(cmbGEType.SelectedValue, "J") = CompairStringResult.Equal Then
                fndVendorBulk.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Vendor_Type_CHA in ('J') ", fndVendorBulk.Value, isButtonClicked)
            End If

            If clsCommon.myLen(fndVendorBulk.Value) > 0 Then
                lblVendorNameBulk.Text = clsVendorMaster.GetName(fndVendorBulk.Value, Nothing)
            Else
                lblVendorNameBulk.Text = ""
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub txtTankerNoBulk_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse (Asc(e.KeyChar) >= 65 AndAlso Asc(e.KeyChar) <= 90) OrElse (Asc(e.KeyChar) >= 97 AndAlso Asc(e.KeyChar) <= 122) OrElse Asc(e.KeyChar) = Keys.Back OrElse Asc(e.KeyChar) = Keys.Delete OrElse Asc(e.KeyChar) = 22 OrElse Asc(e.KeyChar) = 3 Then
        Else
            e.Handled = True
        End If
    End Sub
    Sub reset()
        cmbGEType.SelectedValue = ""
        txtQty.Text = "0"
        txtRate.Text = "0"
        txtPriceCode.Text = ""
        txtMilktypeCode.Value = ""
        lblMilkType.Text = ""
        lblMilkTypeCode.Text = ""
        cmbGEType.SelectedValue = ""
        fndLocationBulk.Enabled = True
        fndVendorBulk.Enabled = True
        fndGateEntryNO.Value = ""
        btnReverse.Visible = False
        fndGateEntryNO.MyReadOnly = False
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm:ss tt")
        fndLocationBulk.Value = ""
        lblLocationDecBulk.Text = ""
        fndVendorBulk.Value = ""
        lblVendorNameBulk.Text = ""
        loadBlankGv()
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnPost.Enabled = False
        btn_amendment.Enabled = False
        btnDelete.Enabled = False
        dtpDateAndTimeBulk.Value = dt
        dtpDateAndTimeBulk.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
        UpdateCurrentLoginStatus()

        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpDateAndTimeBulk.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpDateAndTimeBulk.CustomFormat = "dd/MM/yyyy"
        End If
        '==========================================================
        loadBlankGvItemBulk()
        dt = Nothing
        fndLocationBulk.Value = clsPOBulkProc.getUsersDefaultLocation()
        lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
        RadPageView1.SelectedPage = RadPageViewPage1
        FindAndRestoreGridLayout(Me)
        'FindAndSetTabStopFalse(Me)
    End Sub
    Sub loadBlankGv()
       loadBlankGvItemBulk
        'ReStoreGridLayout()
    End Sub
    Sub loadBlankGvItemBulk()
        gvItemBulk.Tag = "Bulk"
        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing
        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        gvItemBulk.Columns(colItemCode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        gvItemBulk.Columns(colItemCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItemBulk.Columns(colItemCode).ReadOnly = False

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = True

        gvItemBulk.Columns.Add(colQty, "Qty")
        gvItemBulk.Columns(colQty).Width = 150
        gvItemBulk.Columns(colQty).ReadOnly = False

        gvItemBulk.Columns.Add(colRate, "Rate")
        gvItemBulk.Columns(colRate).Width = 150
        gvItemBulk.Columns(colRate).ReadOnly = False
        If allowManualrate = 1 Then
            gvItemBulk.Columns(colRate).IsVisible = True
        Else
            gvItemBulk.Columns(colRate).IsVisible = False
        End If


        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        RadGroupBox1.Width = Me.Width - 200
        gvItemBulk.AllowAddNewRow = False
        'gvItemBulk.AllowColumnReorder = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        'gvItemBulk.AllowColumnChooser = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
    End Sub
    Sub LoadGEType()
        cmbGEType.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Purchase"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "J"
        dr("Name") = "Job Work"
        dt.Rows.Add(dr)

        cmbGEType.DataSource = dt
        cmbGEType.DisplayMember = "Name"
        cmbGEType.ValueMember = "Code"
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub FrmGateEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gvItemBulk.CurrentCell IsNot Nothing Then
        
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)   
        End If
    End Sub

    Private Sub FrmGateEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        allowManualrate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowManualPriceONBulkPO, clsFixedParameterCode.AllowManualPriceONBulkPO, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
      
        LoadGEType()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S to Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D to Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C to Close The Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P To Post Transaction")
        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, "BulkProc", NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), "BulkProc", NavigatorType.Current)
        End If
        If allowManualrate = 1 Then
            Panel1.Visible = False
        End If
    End Sub
    Public Sub SetUserMgmtNew()
        Try
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmGateEntry)
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")

            End If
            btnSave.Visible = MyBase.isModifyFlag
            btnDelete.Visible = MyBase.isDeleteFlag
            If MyBase.isReverse Then
                btnReverse.Enabled = True
            Else
                btnReverse.Enabled = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsPOBulkProc.ReverseAndUnpost(fndGateEntryNO.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndGateEntryNO.Value, "BulkProc", NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvItemBulk_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItemBulk.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            
            If e.Column Is gvItemBulk.Columns(colItemCode) Then
                gvItemBulk.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("Product_Type ='mi'", gvItemBulk.CurrentRow.Cells(colItemCode).Value, False)
                If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colItemCode).Value) > 0 Then
                    gvItemBulk.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsItemMaster.GetItemName(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing))
                    gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & gvItemBulk.CurrentRow.Cells(colItemCode).Value & "' and Default_UOM='1' "))
                    If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colUOM).Value) <= 0 Then
                        gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing)
                    End If
                    'Else

                    '    gvItemBulk.CurrentRow.Cells(colItemDesc).Value = ""
                    '    gvItemBulk.CurrentRow.Cells(colUOM).Value = ""
                    '    gvItemBulk.CurrentRow.Cells(colQty).Value = ""
                    '    gvItemBulk.CurrentRow.Cells(colFat).Value = ""
                    '    gvItemBulk.CurrentRow.Cells(colFatKG).Value = ""
                    '    gvItemBulk.CurrentRow.Cells(colSNF).Value = ""
                    '    gvItemBulk.CurrentRow.Cells(colSNFKG).Value = ""
                End If
            End If
        End If
        isCellValueChangedOpen = False
    End Sub
   

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim arr As List(Of String) = New List(Of String)
        If clsCommon.myLen(fndGateEntryNO.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Enter  PO No To delete ")
        Else
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select COUNT(*) as row_Count from  tspl_gate_entry_details where Po_No='" & fndGateEntryNO.Value & "'")
            If isUsed > 0 Then
                clsCommon.MyMessageBoxShow("PO  No is in use")
                Exit Sub
            End If
            If myMessages.deleteConfirm() Then
                arr.Add(fndGateEntryNO.Value)
                If clsPOBulkProc.deleteData(fndGateEntryNO.Value, Nothing) Then
                    reset()
                    myMessages.delete()
                End If
        End If
        End If
    End Sub
  
    Function allowToSave() As Boolean
        Try            
            If clsCommon.CompairString(cmbGEType.SelectedValue, "") = CompairStringResult.Equal Then
                errorControl.SetError(cmbGEType, "Please Select PO  type ")
                cmbGEType.Focus()
                Throw New Exception("Please Select PO  type ")
            Else
                errorControl.SetError(cmbGEType, "")
            End If

            If clsCommon.myLen(fndLocationBulk.Value) <= 0 Then
                errorControl.SetError(fndLocationBulk, "Please select the location.It is mandatory ")
                fndLocationBulk.Focus()
                Throw New Exception("Please select the location.It is mandatory")
            Else
                errorControl.SetError(fndLocationBulk, "")
            End If
            If clsCommon.myLen(fndVendorBulk.Value) <= 0 Then
                errorControl.SetError(fndVendorBulk, "Please select the vendor.It is mandatory ")
                fndVendorBulk.Focus()
                Throw New Exception("Please select the vendor.It is manadatory")
            Else
                errorControl.SetError(fndVendorBulk, "")
            End If
            If clsCommon.myLen(txtMilktypeCode.Value) <= 0 Then
                errorControl.SetError(txtMilktypeCode, "Please select the MilkType.It is mandatory ")
                txtMilktypeCode.Focus()
                Throw New Exception("Please select the MilkType.It is mandatory")
            Else
                errorControl.SetError(txtMilktypeCode, "")
            End If
            Dim dblQty As Double = 0
                Dim intcount As Integer = 0
                For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                dblQty = clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colQty).Value)
                    If dblQty > 0 Then
                        intcount += 1
                    End If
                Next
                If intcount = 0 Then
                Throw New Exception("Please enter atleast one chamber qty. ")
            Else
                txtQty.Text = dblQty
            End If

            If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colItemCode).Value) <= 0 Then
                Throw New Exception("Please Enter Item Code At Row No 1 in Item Grid")
            End If

            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where item_code='" & gvItemBulk.Rows(0).Cells(colItemCode).Value & "'")) = 0 Then
                Throw New Exception("Please Enter Valid Item Code At Row No 1 in Item Grid")
            End If

            If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colQty).Value) <= 0 Then
                Throw New Exception("Please Enter Item Qty At Row No 1 in Item Grid")
            End If
            If IsNumeric(gvItemBulk.Rows(0).Cells(colQty).Value) = False Then
                Throw New Exception(" Item Qty Must be a Number At Row No 1 in Item Grid")
            End If

            If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) <= 0 Then
                    Throw New Exception(" Item Qty Must be a Number and Not Zero or Negative At Row No 1 in Item Grid")
            End If

         
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") > clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                Throw New Exception(" PO Date Can not be upcoming Date ")
            End If
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") < clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") And clsCommon.myLen(fndGateEntryNO.Value) = 0 Then
                If clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowGateEntryInPrevDate, Nothing) = 0 Then
                    dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    Throw New Exception(" PO Date Can not be Prev Date, Please Contact to Administrator ")
                End If
            End If
            If allowManualrate = 0 Then
                Dim strPriceCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                       "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                       "where  TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm tt") & "' and  expirydate >= '" & clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm tt") & "' and TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & txtMilktypeCode.Value & "'   and " & _
                                       "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                       "Tspl_Vendor_Price_Chart_mapping where  " & _
                                       "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & fndVendorBulk.Value & "'  )  order by Price_Date desc")
                If clsCommon.myLen(strPriceCode) = 0 Then
                    Throw New Exception("Please create price chart for Vendor : " + fndVendorBulk.Value)
                Else
                    txtPriceCode.Text = strPriceCode
                    txtRate.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Standard_Rate from TSPL_Bulk_Price_MASTER where Price_Code='" & strPriceCode & "'"))
                End If
            End If
            Return True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

    Sub SaveData(ByVal isPost As Boolean)
        Try
            Dim trans As SqlTransaction = Nothing
            obj = New clsPOBulkProc()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementCounterOnEntryType, clsFixedParameterCode.BulkProcurementCounterOnEntryType, trans)) = 0, False, True) ''Make Setting Balwinder
                If isPODocumentTypeWise Then
                    If clsCommon.myLen(cmbGEType.SelectedValue) <= 0 Then
                        cmbGEType.Focus()
                        Throw New Exception("Please select PO  Type")
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(cmbGEType.SelectedValue), "P") = CompairStringResult.Equal Then
                        obj.PO_No = clsERPFuncationality.GetNextCode(trans, dtpDateAndTimeBulk.Value, clsDocType.POBulkP, clsDocTransactionType.BulkProcPurchase, fndLocationBulk.Value)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbGEType.SelectedValue), "J") = CompairStringResult.Equal Then
                        obj.PO_No = clsERPFuncationality.GetNextCode(trans, dtpDateAndTimeBulk.Value, clsDocType.POBulkP, clsDocTransactionType.BulkProcJobWork, fndLocationBulk.Value)
                    Else
                        cmbGEType.Focus()
                        Throw New Exception("Wrong PO  Type")
                    End If
                Else
                    obj.PO_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.POBulkP, clsDocTransactionType.BulkProc, fndLocationBulk.Value)
                End If


                If clsCommon.myLen(obj.PO_No) <= 0 Then
                    clsCommon.MyMessageBoxShow("Error in PO   No genertion")
                    Exit Sub
                End If
            Else
                obj.PO_No = clsCommon.myCstr(fndGateEntryNO.Value)
            End If
            fndGateEntryNO.Value = obj.PO_No

                obj.Date_And_Time = clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
                obj.location_Code = clsCommon.myCstr(fndLocationBulk.Value)
                obj.Location_Desc = clsCommon.myCstr(lblLocationDecBulk.Text)
                obj.Vendor_Code = clsCommon.myCstr(fndVendorBulk.Value)
            obj.Vendor_Desc = clsCommon.myCstr(lblVendorNameBulk.Text)
            obj.MIKL_TYPE_CODE = clsCommon.myCstr(txtMilktypeCode.Value)
            obj.Rate = clsCommon.myCdbl(txtRate.Text)
            obj.Price_Code = clsCommon.myCstr(txtPriceCode.Text)
            obj.Qty = clsCommon.myCdbl(txtQty.Text)
            obj.Gate_Entry_Type = cmbGEType.SelectedValue
            If Not isPost Then
                obj.isPosted = 0
            End If
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            obj.Arr = New List(Of clsPOBulkProcDetails)
                For Each grow As GridViewRowInfo In gvItemBulk.Rows
                Dim objTr As New clsPOBulkProcDetails()
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objTr.ManualRate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
                Next
          
            If clsPOBulkProc.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        myMessages.insert()
                    Else
                        myMessages.update()
                    End If
                End If
                btnSave.Text = "Update"
                fndGateEntryNO.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                btn_amendment.Enabled = False
                obj = Nothing
                Exit Sub
                LoadData(obj.PO_No, "BulkProc", NavigatorType.Current)

            End If
            trans.Rollback()
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            btn_amendment.Enabled = False
            fndGateEntryNO.MyReadOnly = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub postData()
        Try
            Dim strDocType As String = String.Empty
          
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsPOBulkProc.postData(fndGateEntryNO.Value, strDocType, Me.Form_ID)) Then
                    msg = "Successfully Posted"
                Else
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
            End If
            dt = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
  
    Sub LoadData(ByVal strGateEntryNo As String, ByVal docType As String, ByVal nav As NavigatorType)
        Dim obj As clsPOBulkProc = Nothing
        obj = clsPOBulkProc.getData(strGateEntryNo, "BulkProc", nav)
        If obj IsNot Nothing Then
            insideLoadData = True
            fndLocationBulk.Enabled = False
            fndGateEntryNO.Value = obj.PO_No
            dtpDateAndTimeBulk.Value = obj.Date_And_Time
            fndLocationBulk.Value = obj.location_Code
            lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
            lblVendorNameBulk.Text = clsVendorMaster.GetName(fndVendorBulk.Value, Nothing)
            lblLocationDecBulk.Text = obj.Location_Desc
            fndVendorBulk.Value = obj.Vendor_Code
            lblVendorNameBulk.Text = obj.Vendor_Desc
            txtRate.Text = obj.Rate
            txtPriceCode.Text = obj.Price_Code
            txtMilktypeCode.Value = obj.MIKL_TYPE_CODE
            cmbGEType.SelectedValue = obj.Gate_Entry_Type
            txtQty.Text = obj.Qty
            loadBlankGvItemBulk()
            If obj.isPosted = 1 Then
                lblPending.Status = ERPTransactionStatus.Approved
                btnSave.Enabled = False
                btnDelete.Enabled = False
                btnPost.Enabled = False
                btn_amendment.Enabled = True
            Else
                lblPending.Status = ERPTransactionStatus.Pending
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                btn_amendment.Enabled = False
            End If
            btnSave.Text = "Update"
           
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    gvItemBulk.Rows.Clear()
                    For Each objTr As clsPOBulkProcDetails In obj.Arr
                        gvItemBulk.Rows.AddNew()
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code                      
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colRate).Value = objTr.ManualRate
                    Next
                End If
                insideLoadData = False
        Else
            reset()
        End If
   
        obj = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then
            SaveData(False)
        End If
    End Sub

    Private Sub fndGateEntryNO__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndGateEntryNO._MYNavigator
       
        LoadData(fndGateEntryNO.Value, "BulkProc", NavType)
    End Sub

    Private Sub fndGateEntryNO__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNO._MYValidating
        Dim whrcls As String = String.Empty       
        fndGateEntryNO.Value = clsPOBulkProc.getFinder("", fndGateEntryNO.Value, isButtonClicked)
        If clsCommon.myLen(fndGateEntryNO.Value) > 0 Then
            LoadData(fndGateEntryNO.Value, "BulkProc", NavigatorType.Current)
        Else
            reset()
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItemBulk.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItemBulk.Columns.Count - 1 Step ii + 1
                        gvItemBulk.Columns(ii).IsVisible = False
                        gvItemBulk.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItemBulk.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            RadPageViewPage1.Text = "Gate" & Environment.NewLine & "Entry"
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItemBulk.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItemBulk.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItemBulk.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub LoadGrid()

        loadBlankGv()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Description as ItemCode from TSPL_FIXED_PARAMETER where Type='MCCDefaultMilkItem' and Code='MilkSetting'")
        If dt.Rows.Count > 0 Then
            gvItemBulk.Rows.Clear()
            Dim intLineNo As Integer = 0
            For Each dr As DataRow In dt.Rows
                gvItemBulk.Rows.AddNew()
                intLineNo += 1
                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = intLineNo
                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("ItemCode"))
                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(clsCommon.myCstr(dr("ItemCode")), Nothing)
                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & clsCommon.myCstr(dr("ItemCode")) & "' and Default_UOM='1' "))
                If clsCommon.myLen(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value) <= 0 Then
                    gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value, Nothing)
                End If

            Next
        End If
    End Sub
    Private Sub dtpDateAndTimeBulk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpDateAndTimeBulk.Validating
        Try
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") > clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                'dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                Throw New Exception(" PO Date Can not be upcoming Date ")
            End If
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") < clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                If clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowGateEntryInPrevDate, Nothing) = 0 Then
                    dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    Throw New Exception(" PO Date Can not be Prev Date, Please Contact to Administrator ")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        End Try
    End Sub


    Private Sub MyLabel2_Click(sender As Object, e As EventArgs)

    End Sub
   
    'KUNAL > TICKET : BM00000009843 > DATE : 17-NOV-2016
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
         
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndGateEntryNO.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
   

    Private Sub cmbGEType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)
        If insideLoadData = False Then
            fndVendorBulk.Value = Nothing
            lblVendorNameBulk.Text = ""
        End If
    End Sub

    Private Sub txtMilktypeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMilktypeCode._MYValidating
        Dim whr As String = ""
        txtMilktypeCode.Value = clsMilkTypeMaster.getFinder(whr, txtMilktypeCode.Value, isButtonClicked)
        If clsCommon.myLen(txtMilktypeCode.Value) > 0 Then
            lblMilkTypeCode.Text = clsMilkTypeMaster.getMilkTypeName(txtMilktypeCode.Value, Nothing)
            lblMilkType.Text = clsMilkTypeMaster.getMilkType(txtMilktypeCode.Value, Nothing)
            LoadGrid()
        End If
    End Sub
End Class

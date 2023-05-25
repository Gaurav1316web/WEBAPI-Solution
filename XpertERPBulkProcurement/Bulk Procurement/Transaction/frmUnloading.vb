''richa agarwal BM00000006476 07/05/2015
Imports common
Imports System.Data.SqlClient
Public Class FrmUnloading
    Inherits FrmMainTranScreen
    Dim FinalChamberwise As Integer = 0
    Dim MCCChamberwise As Integer = 0
    Dim AllowBulkProcTransDateSameasGateEntryDate As Integer = 0
    Public AllowCanInformationintoGridForTankerDispatch As Boolean = False
    Dim TankerFromMaster As Integer = 0
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim IsWeighmentUnloadingSequencewise As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colHSN As String = "HSNCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colUnloadingStatus As String = "colUnloadingStatus"
    Const colIsBatchItem As String = "colIsBatchItem"
    Const colIsCanType As String = "colIsCanType"
    Const colBatchNo As String = "colBatchNo"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colStartTime As String = "colStartTime"
    Public Const colEndTime As String = "colEndTime"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colISelect As String = "colISelect"
    Public Const colUnloadingSeqNo As String = "colWeighmentSeqNo"
    Public Const colSubLoc As String = "colSubLoc"
    Public strDocCode As String = ""
    Dim docType As String = String.Empty
    Dim obj As clsUnloading = Nothing
    Dim isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim isCreateBulkProcPriceChartItemWise As Integer = 0
    Public AutoCreateUnloading As Boolean = False
    Dim settTankerDispatchIntermittentSingleGateIn As Boolean = False
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub reset(ByVal isResetUnloadingNoFinder As Boolean)
        If isResetUnloadingNoFinder Then
            fndUnloadingNo.Value = ""
        End If
        fndReferenceNo.Value = ""
        lblVendorName.Text = ""
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        chkJobWork.Enabled = False
        chkJobWork.Checked = False
        fndGateEntryNo.Value = ""
        dtpUnloadingDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpGateEntryDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpWeighmentDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpQCDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        FndTankerNo.Value = ""
        txtQCNo.Text = ""
        txtLocation.Text = ""
        lblLocationName.Text = ""
        fndSubLocation.Value = ""
        lblSubLocationName.Text = ""
        txtWeighmentNo.Text = ""
        loadBlankItemGrid("")
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPrint.Enabled = False
        btnPost.Enabled = False
        lblPending.Status = ERPTransactionStatus.Pending
        fndUnloadingNo.MyReadOnly = False
        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpUnloadingDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpWeighmentDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpQCDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpUnloadingDateTime.CustomFormat = "dd/MM/yyyy "
            dtpWeighmentDateTime.CustomFormat = "dd/MM/yyyy"
            dtpQCDateTime.CustomFormat = "dd/MM/yyyy"
            dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy"
        End If
        '==========================================================
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset(True)
    End Sub
    Sub LoadChamberstatus(ByVal strDoctype As String)
        If clsCommon.CompairString(strDocType, "BulkProc") = CompairStringResult.Equal Then
            If TankerFromMaster = 1 Then
                FinalChamberwise = 1
            Else
                FinalChamberwise = 0
            End If
        ElseIf clsCommon.CompairString(strDocType, "MccProc") = CompairStringResult.Equal Then
            If MCCChamberwise = 1 Then
                FinalChamberwise = 1
            Else
                FinalChamberwise = 0
            End If
        End If
    End Sub
    Sub loadBlankItemGrid(ByVal strDoctype As String)
        LoadChamberstatus(strDoctype)

        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing

        gvItem.Columns.Add(colSlNo, "SL. NO.")
        gvItem.Columns(colSlNo).Width = 60
        gvItem.Columns(colSlNo).ReadOnly = True

        Dim repoISelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoISelect.FormatString = ""
        repoISelect.HeaderText = "Select"
        repoISelect.Name = colISelect
        repoISelect.Width = 100
        repoISelect.ReadOnly = True
        repoISelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoISelect)
        repoISelect.IsVisible = False

        If AllowCanInformationintoGridForTankerDispatch Then
            Dim repoisCanType As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoisCanType.HeaderText = "Is Can"
            repoisCanType.Name = colIsCanType
            repoisCanType.ReadOnly = True
            repoisCanType.IsVisible = True
            repoisCanType.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gvItem.MasterTemplate.Columns.Add(repoisCanType)
        End If


        gvItem.Columns.Add(colItemCode, "Item Code")
        gvItem.Columns(colItemCode).Width = 100
        gvItem.Columns(colItemCode).ReadOnly = True

        gvItem.Columns.Add(colItemDesc, "Item Desc")
        gvItem.Columns(colItemDesc).Width = 320
        gvItem.Columns(colItemDesc).ReadOnly = True

        gvItem.Columns.Add(colHSN, "HSN Code")
        gvItem.Columns(colHSN).Width = 100
        gvItem.Columns(colHSN).ReadOnly = True

        gvItem.Columns.Add(colQty, "Qty")
        gvItem.Columns(colQty).Width = 120
        gvItem.Columns(colQty).ReadOnly = False
        gvItem.Columns(colQty).IsVisible = False

        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 120
        gvItem.Columns(colUOM).ReadOnly = True
        gvItem.Columns(colUOM).IsVisible = False

        gvItem.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItem.Columns(colChamberDesc).Width = 150
        gvItem.Columns(colChamberDesc).ReadOnly = True
        gvItem.Columns(colChamberDesc).IsVisible = True

        gvItem.Columns.Add(colSubLoc, "Silo Location")
        gvItem.Columns(colSubLoc).Width = 150
        gvItem.Columns(colSubLoc).ReadOnly = False
        gvItem.Columns(colSubLoc).IsVisible = True


        Dim repoDT As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDT.Format = DateTimePickerFormat.Custom
        repoDT.HeaderText = "Start Time"
        repoDT.CustomFormat = "hh:mm tt"
        repoDT.FormatString = "{0:hh:mm tt}"
        repoDT.Name = colStartTime
        repoDT.Width = 100
        repoDT.EditorType = GridViewDateTimeEditorType.TimePicker
        repoDT.ReadOnly = False
        repoDT.IsVisible = False
        If FinalChamberwise = 1 Then
            repoDT.IsVisible = True
        End If
        gvItem.MasterTemplate.Columns.Add(repoDT)

        repoDT = New GridViewDateTimeColumn()
        repoDT.Format = DateTimePickerFormat.Custom
        repoDT.HeaderText = "End Time"
        repoDT.CustomFormat = "hh:mm tt"
        repoDT.FormatString = "{0:hh:mm tt}"
        repoDT.Name = colEndTime
        repoDT.Width = 100
        repoDT.EditorType = GridViewDateTimeEditorType.TimePicker
        repoDT.ReadOnly = False
        repoDT.IsVisible = False
        If FinalChamberwise = 1 Then
            repoDT.IsVisible = True
        End If
        gvItem.MasterTemplate.Columns.Add(repoDT)

        gvItem.Columns.Add(colUnloadingSeqNo, "Sequence No")
        gvItem.Columns(colUnloadingSeqNo).Width = 120
        gvItem.Columns(colUnloadingSeqNo).ReadOnly = True
        gvItem.Columns(colUnloadingSeqNo).IsVisible = False

        Dim repoUnloadingStatus As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoUnloadingStatus.FormatString = ""
        repoUnloadingStatus.HeaderText = "UnLoading Status"
        repoUnloadingStatus.Name = colUnloadingStatus
        repoUnloadingStatus.Width = 100
        repoUnloadingStatus.ReadOnly = True
        repoUnloadingStatus.IsVisible = False
        repoUnloadingStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoUnloadingStatus)

        If FinalChamberwise = 1 Then
            repoISelect.IsVisible = True
            gvItem.Columns(colChamberDesc).IsVisible = True
            gvItem.Columns(colUnloadingSeqNo).IsVisible = True
            gvItem.Columns(colUnloadingStatus).IsVisible = True

            ' If isCreateBulkProcPriceChartItemWise = 1 Then
            gvItem.Columns(colSubLoc).IsVisible = True
            ' End If
        End If

        gvItem.Columns.Add(colFat, "FAT (%)")
        gvItem.Columns(colFat).Width = 75
        gvItem.Columns(colFat).ReadOnly = True
        gvItem.Columns(colFat).IsVisible = False

        gvItem.Columns.Add(colSNF, "SNF (%)")
        gvItem.Columns(colSNF).Width = 75
        gvItem.Columns(colSNF).ReadOnly = True
        gvItem.Columns(colSNF).IsVisible = False

        gvItem.Columns.Add(colFatKG, "FAT (KG)")
        gvItem.Columns(colFatKG).Width = 75
        gvItem.Columns(colFatKG).ReadOnly = True
        gvItem.Columns(colFatKG).IsVisible = False

        gvItem.Columns.Add(colSNFKG, "SNF (KG)")
        gvItem.Columns(colSNFKG).Width = 75
        gvItem.Columns(colSNFKG).ReadOnly = True
        gvItem.Columns(colSNFKG).IsVisible = False

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Batch No"
        repoSpecification.Name = colBatchNo
        repoSpecification.Width = 150
        repoSpecification.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSpecification)

        Dim repoIsBatchItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsBatchItem.HeaderText = "Is Batch Item"
        repoIsBatchItem.Name = colIsBatchItem
        repoIsBatchItem.ReadOnly = True
        repoIsBatchItem.IsVisible = False
        repoIsBatchItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsBatchItem)

        gvItem.Rows.AddNew()
        gvItem.Rows(0).Cells(colSlNo).Value = "1"

        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = False
        gvItem.AllowRowReorder = True
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = False
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AllowColumnChooser = True

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave(False) Then
            If SaveData(False) Then
                loadData(fndUnloadingNo.Value, NavigatorType.Current)
            Else
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Not Saved ")
                End If
            End If
        End If
    End Sub

    Public Function SaveData(ByVal isPost As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            obj = New clsUnloading()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                If AllowBulkProcTransDateSameasGateEntryDate = 1 Then
                    dtpUnloadingDateTime.Value = clsDBFuncationality.getSingleValue("Select Date_And_Time from Tspl_Gate_Entry_Details where Gate_Entry_No='" & fndGateEntryNo.Value & "'", trans)
                End If
                Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementCounterOnEntryType, clsFixedParameterCode.BulkProcurementCounterOnEntryType, trans)) = 0, False, True) ''Make Setting Balwinder
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select Gate_Entry_Type,Doc_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'", trans)
                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                    Throw New Exception("Gate Entry No" + fndGateEntryNo.Value + " Not found ")
                End If
                Dim isBulkPro As Boolean = clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Doc_Type")), "BulkProc") = CompairStringResult.Equal
                If isPODocumentTypeWise AndAlso isBulkPro Then
                    Dim strGateEntryType As String = clsCommon.myCstr(dtTemp.Rows(0)("Gate_Entry_Type"))
                    If clsCommon.myLen(strGateEntryType) <= 0 Then
                        Throw New Exception("Gate Entry Type not found on Gate Entry screen")
                    End If
                    If clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                        obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dtpUnloadingDateTime.Value, clsDocType.Unloading, clsDocTransactionType.BulkProcPurchase, txtLocation.Text)
                    ElseIf clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                        obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dtpUnloadingDateTime.Value, clsDocType.Unloading, clsDocTransactionType.BulkProcJobWork, txtLocation.Text)
                    Else
                        Throw New Exception("Wrong Gate Entry Type")
                    End If
                Else
                    If chkJobWork.Checked Then
                        obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dtpUnloadingDateTime.Value, clsDocType.Unloading, IIf(isBulkPro, clsDocTransactionType.BulkProcJobWorkOutward, clsDocTransactionType.MCCProcJobWorkOutward), txtSubLocation.Value)
                    Else
                        obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dtpUnloadingDateTime.Value, clsDocType.Unloading, clsDocTransactionType.NA, txtLocation.Text)
                    End If
                End If
                If clsCommon.myLen(obj.Unloading_No) <= 0 Then
                    Throw New Exception("Error In Unloading  No Genertion")
                End If
            Else
                obj.Unloading_No = clsCommon.myCstr(fndUnloadingNo.Value)
            End If
            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Joblocation_Code = txtSubLocation.Value
            fndUnloadingNo.Value = obj.Unloading_No
            obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNo.Value)
            obj.Unloading_Date_Time = clsCommon.GetPrintDate(dtpUnloadingDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(FndTankerNo.Value)
            obj.Weighment_No = clsCommon.myCstr(txtWeighmentNo.Text)
            obj.QC_No = clsCommon.myCstr(txtQCNo.Text)
            obj.location_Code = clsCommon.myCstr(txtLocation.Text)
            obj.Sub_location_Code = clsCommon.myCstr(fndSubLocation.Value)
            obj.Item_Code = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value)
            obj.Item_Desc = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemDesc).Value)
            obj.UOM = clsCommon.myCstr(gvItem.Rows(0).Cells(colUOM).Value)
            obj.Qty = clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value)
            obj.fat_per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFat).Value)
            obj.snf_Per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNF).Value)
            obj.SNF_KG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value)
            obj.fat_KG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value)
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
            ''richa agarwal remove settings as per Ranjana Mam 
            'If FinalChamberwise = 1 Then
            obj.Arr = New List(Of clsUnloadingChemberNoDetails)
            For Each grow As GridViewRowInfo In gvItem.Rows
                Dim objTr As New clsUnloadingChemberNoDetails()
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objTr.Unloading_Sequence = clsCommon.myCdbl(grow.Cells(colUnloadingSeqNo).Value)
                objTr.Sublocation_Code = clsCommon.myCstr(grow.Cells(colSubLoc).Value)
                objTr.IsBatchWise = IIf(clsCommon.myCBool(grow.Cells(colIsBatchItem).Value) = True, "Y", "N")
                objTr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                If clsCommon.myLen(grow.Cells(colStartTime).Value) > 0 Then
                    objTr.StartTime = clsCommon.myCDate(grow.Cells(colStartTime).Value)
                End If
                If clsCommon.myLen(grow.Cells(colEndTime).Value) > 0 Then
                    objTr.EndTime = clsCommon.myCDate(grow.Cells(colEndTime).Value)
                End If
                objTr.UnLoading_Status = IIf(clsCommon.myCBool(grow.Cells(colISelect).Value) = True, 1, 0)
                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            'End If
            clsUnloading.saveData(obj, trans)
            trans.Commit()
            If AutoCreateUnloading = False Then
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully")
                    End If
                End If
            End If
            'loadData(obj.Unloading_No, NavigatorType.Current)
            Return True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
            'btnSave.Text = "Save"
            'btnDelete.Enabled = False
            'btnPost.Enabled = False
            'btnPrint.Enabled = False
            'fndUnloadingNo.MyReadOnly = False
            Return False
        End Try
    End Function
    Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        obj = clsUnloading.getData(fndUnloadingNo.Value, NavigatorType.Current)
        Dim arr As List(Of String) = New List(Of String)
        trans = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndUnloadingNo.Value) > 0 Then
                If obj IsNot Nothing Then
                    If clsCleaning.isCleaningDone(obj.Gate_Entry_No, trans) Then
                        Throw New Exception("Document is in use. Can't Delete")
                    End If
                End If
                If deleteConfirm() Then
                    arr.Add(fndUnloadingNo.Value)
                    If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.frmUnloading, trans) Then
                        If clsUnloading.deleteData(fndUnloadingNo.Value, trans) Then
                            trans.Commit()
                            reset(True)
                            clsCommon.MyMessageBoxShow("Deleted Successfully")
                        Else
                            clsCommon.MyMessageBoxShow("Could Not Deleted. Try Again")
                            trans.Rollback()
                        End If
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow("Please select a Unloading No To delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub

    Sub PostData()
        Dim str As String = String.Empty
        Try
           
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave(True) Then
                    Exit Sub
                End If
                If SaveData(True) Then
                    If (clsUnloading.postData(fndUnloadingNo.Value, Me.Form_ID, Nothing)) Then
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
                    loadData(fndUnloadingNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmUnloading_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                              "TSPL_MILK_UNLOADING " + Environment.NewLine + _
                                              "TSPL_Milk_Unloading_Chember_Details (  Only in case of chamber wise setting ON) " + Environment.NewLine + _
                                              "TSPL_MILK_UNLOADING_History ( For History) ")
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PrintData()
        clsCommon.MyMessageBoxShow("No Print Format Found")
    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Private Sub fndGateEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNo._MYValidating
        'fndGateEntryNo.Value = clsUnloading.getGateEntryFinder("xx.[Is Posted]='Yes' and xx.GateEntryNo  not in (select TSPL_MILK_UNLOADING.Gate_Entry_No from TSPL_MILK_UNLOADING where TSPL_MILK_UNLOADING.gate_entry_no<>'" & fndGateEntryNo.Value & "' )  and ISNULL(xx.[Weighment No] ,'')<>''  and ISNULL(xx.[QC No] ,'')<>'' and xx.Accepted>0 ", fndGateEntryNo.Value, isButtonClicked)
        'If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
        '    LoadGateEntryData(fndGateEntryNo.Value)
        'Else
        '    reset(False)
        'End If
    End Sub
    Function allowToSave(ByVal isPost As Boolean) As Boolean
        Try
            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(dtpUnloadingDateTime.Value, Nothing) = False Then
                dtpUnloadingDateTime.Select()
                Return False
            End If
            '=======================================================
            If clsCommon.myLen(FndTankerNo.Value) <= 0 Then
                Throw New Exception("Please Select Tanker No")
                errorControl.SetError(FndTankerNo, "Please Select Tanker No")
            Else
                errorControl.ResetError(FndTankerNo)
            End If

            If clsCommon.myLen(fndGateEntryNo.Value) <= 0 Then
                Throw New Exception("Please Select GateEntry No")
                errorControl.SetError(fndGateEntryNo, "Please Select Gate Entry No")
            Else
                errorControl.ResetError(fndGateEntryNo)
            End If


            Dim WDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select weighment_date from TSPL_Weighment_Detail where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            Dim QDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Qc_out_date_time from TSPL_QUALITY_CHECK where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            Dim GDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select date_and_time from Tspl_Gate_Entry_Details where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")

            '==================Added by preeti Gupta Against ticket No[KDI/13/06/18-000363]
            If clsCommon.GetDateWithStartTime(dtpUnloadingDateTime.Value) < clsCommon.GetDateWithStartTime(dtpQCDateTime.Value) Then
                Throw New Exception("Unloading Date can not be less than QC Date")
            End If
            '===================================================================================================================

            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from tspl_milk_unloading where gate_entry_no='" & fndGateEntryNo.Value & "' and unloading_no <>'" & fndUnloadingNo.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
            If FinalChamberwise = 1 AndAlso isPost = True Then
                Dim Counter As Integer = 0
                For ii As Integer = 0 To gvItem.Rows.Count - 1

                    Dim UnLoading_Status As Integer = IIf(clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True, 1, 0)
                    If UnLoading_Status = 1 Then
                        Counter += 1
                    End If



                Next
                If gvItem.Rows.Count <> Counter Then
                    Throw New Exception("Please update all Unloading status.")
                End If
            End If
            Dim strDocType1 As String = clsDBFuncationality.getSingleValue("select Doc_Type from TSPL_weighment_detail where gate_entry_no='" & clsCommon.myCstr(fndGateEntryNo.Value) & "' ")
            If AllowCanInformationintoGridForTankerDispatch = True AndAlso clsCommon.CompairString(strDocType1, "MccProc") = CompairStringResult.Equal Then
                Dim Countertoselect As Integer = 0
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    If clsCommon.myCBool(gvItem.Rows(ii).Cells(colIsCanType).Value) = False Then
                        Dim UnLoading_Status As Integer = IIf(clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True, 1, 0)
                        If UnLoading_Status = 1 Then
                            Countertoselect += 1
                        End If
                    End If

                Next
                If Countertoselect = 0 Then
                    Throw New Exception("Please update all Unloading status.")
                End If

            End If
            'If FinalChamberwise = 1 AndAlso isCreateBulkProcPriceChartItemWise = 1 Then
            Dim gridrowcount As Integer = 0
            gridrowcount = gvItem.Rows.Count
            Dim isSiloEnteredintoGrid As Boolean = False
            If FinalChamberwise = 1 Then
                gridrowcount = gvItem.Rows.Count
                If gridrowcount > 1 Then
                    Dim Counter As Integer = 0
                    For ii As Integer = 0 To gvItem.Rows.Count - 1
                        Dim UnLoading_Status As Integer = IIf(clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True, 1, 0)
                        If UnLoading_Status = 1 Then
                            Dim strsubLoc As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colSubLoc).Value)
                            If clsCommon.myLen(strsubLoc) = 0 Then
                                Throw New Exception("Please enter item silo location.")
                            Else
                                isSiloEnteredintoGrid = True
                            End If
                            fndSubLocation.Value = strsubLoc
                        End If
                    Next
                End If
            End If


            If isCreateBulkProcPriceChartItemWise = 0 Then
                If gridrowcount = 1 AndAlso isSiloEnteredintoGrid = False Then
                    If clsCommon.myLen(fndSubLocation.Value) <= 0 Then
                        Throw New Exception("Please Select Silo ")
                        errorControl.SetError(fndSubLocation, "Please Select Silo ")
                    Else
                        errorControl.ResetError(fndSubLocation)
                    End If
                End If

            End If


            ''richa agarwal 24 June,2019 TEC/25/06/19-000566
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colItemCode).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colQty).Value)
                If clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True Then
                    If dblQty > 0 AndAlso clsCommon.myCBool(clsItemMaster.IsBatchItem(strICode)) Then
                        If clsCommon.myLen(gvItem.Rows(ii).Cells(colBatchNo).Value) <= 0 Then
                            Throw New Exception("Please provide Batch no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Sub FrmUnloading_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        isCreateBulkProcPriceChartItemWise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, Nothing))
        Panel3.Enabled = False
        SetUserMgmtNew()
        MCCChamberwise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        IsWeighmentUnloadingSequencewise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowUnloadingandWeighmentSequencewise, clsFixedParameterCode.ShowUnloadingandWeighmentSequencewise, Nothing))
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        AllowBulkProcTransDateSameasGateEntryDate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcTransDateSameasGateEntryDate, clsFixedParameterCode.AllowBulkProcTransDateSameasGateEntryDate, Nothing))
        AllowCanInformationintoGridForTankerDispatch = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCanInformationintoGridForTankerDispatch, clsFixedParameterCode.AllowCanInformationintoGridForTankerDispatch, Nothing)) = 0, False, True)
        settTankerDispatchIntermittentSingleGateIn = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchIntermittentSingleGateIn, clsFixedParameterCode.TankerDispatchIntermittentSingleGateIn, Nothing)) = 1)
        reset(True)
        MyBase.ReStoreGridLayoutMain(Me)
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        If clsCommon.myLen(strDocCode) > 0 Then
            loadData(strDocCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmUnloading)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadGateEntryData(ByVal strGateEntryNo As String)
        Dim strUnNo As String = clsDBFuncationality.getSingleValue("select unloading_No from TSPL_MILK_UNLOADING where gate_entry_no='" & strGateEntryNo & "'")
        If clsCommon.myLen(strUnNo) > 0 Then
            loadData(strUnNo, NavigatorType.Current)
            Exit Sub
        End If
        Dim objGt As New clsGateEntry()
        reset(False)
        objGt = clsGateEntry.getData(strGateEntryNo, NavigatorType.Current)
        If objGt IsNot Nothing Then
            lblVendorName.Text = objGt.Vendor_Desc
            txtSubLocation.Value = objGt.Sublocation_Code
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            chkJobWork.Checked = IIf(objGt.IsAgainstJobWork = 1, True, False)
            fndGateEntryNo.Value = objGt.Gate_Entry_No
            FndTankerNo.Value = objGt.Tanker_No
            fndReferenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + objGt.Gate_Entry_No + "' "))
            Dim strWeighmentNo As String = clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_weighment_detail where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
            Dim strDocType As String = clsDBFuncationality.getSingleValue("select Doc_Type from TSPL_weighment_detail where gate_entry_no='" & objGt.Gate_Entry_No & "' ")

            If clsCommon.myLen(strWeighmentNo) > 0 Then
                txtWeighmentNo.Text = clsCommon.myCstr(strWeighmentNo)
            Else
                txtWeighmentNo.Text = ""
            End If
            Dim strQcNo As String = clsDBFuncationality.getSingleValue("select Qc_No from TSPL_Quality_Check where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
            If clsCommon.myLen(strQcNo) > 0 Then
                txtQCNo.Text = clsCommon.myCstr(strQcNo)
            Else
                txtQCNo.Text = ""
            End If
            Dim strLocCode As String = objGt.location_Code
            Dim strLocDesc As String = clsLocation.GetName(strLocCode, Nothing)
            chkJobWork.Checked = IIf(objGt.IsAgainstJobWork = 1, True, False)
            If clsCommon.myLen(strLocCode) > 0 Then
                txtLocation.Text = strLocCode
                lblLocationName.Text = strLocDesc
            Else
                txtLocation.Text = ""
                lblLocationName.Text = ""
            End If
            If objGt.IsAgainstJobWork = 1 Then
                Dim strsQry As String = "select Location_Code,Location_Desc from tspl_location_master where is_sub_location='Y' and Main_Location_Code='" & txtLocation.Text & "'  and Location_Type='Virtual' and UseInJobWork=1 "
                Dim dts As DataTable = clsDBFuncationality.GetDataTable(strsQry)
                If dts.Rows.Count > 0 Then
                    fndSubLocation.Value = clsCommon.myCstr(dts.Rows(0)("Location_Code"))
                    lblSubLocationName.Text = clsCommon.myCstr(dts.Rows(0)("Location_Desc"))
                    fndSubLocation.Enabled = False
                Else
                    clsCommon.MyMessageBoxShow("Please Create Virtual Silo Location for Location " & txtLocation.Text & " ")
                    reset(False)
                    Exit Sub
                End If
            Else
                fndSubLocation.Value = ""
                lblSubLocationName.Text = ""
                fndSubLocation.Enabled = True
            End If

            LoadChamberstatus(strDocType)
            ''richa agarwal remove settings as per Ranjana Mam 
            loadBlankItemGrid(strDocType)
            If FinalChamberwise = 0 Then
                gvItem.Rows(0).Cells(colSlNo).Value = "1"
                gvItem.Rows(0).Cells(colItemCode).Value = objGt.Item_Code
                gvItem.Rows(0).Cells(colItemDesc).Value = clsItemMaster.GetItemName(objGt.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colUOM).Value = clsItemMaster.GetStockUnit(objGt.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objGt.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colQty).Value = ""
                gvItem.Rows(0).Cells(colFat).Value = objGt.fat_per
                gvItem.Rows(0).Cells(colSNF).Value = objGt.snf_Per
                gvItem.Rows(0).Cells(colFatKG).Value = ""
                gvItem.Rows(0).Cells(colSNFKG).Value = ""
            End If

            If objGt.Arr IsNot Nothing AndAlso objGt.Arr.Count > 0 Then
                gvItem.Rows.Clear()
                Dim ChamberNo As Integer = clsUnloading.isWeigmentDoneChamberwise(strWeighmentNo, Nothing)
                For Each objTr As clsGateEntryChemberNoDetails In objGt.Arr
                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objGt.Qty_In_Kg / 100
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objGt.Qty_In_Kg / 100
                    ''richa agarwal TEC/25/06/19-000566
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objTr.Item_Code)
                    If clsCommon.myCBool(clsItemMaster.IsBatchItem(objTr.Item_Code)) Then
                        gvItem.Columns(colBatchNo).IsVisible = True
                    Else
                        gvItem.Columns(colBatchNo).IsVisible = False
                    End If

                    If IsWeighmentUnloadingSequencewise = 1 Then
                        If ChamberNo > 0 And gvItem.Rows(gvItem.Rows.Count - 1).Index = ChamberNo - 1 Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = False
                        Else
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = True
                        End If
                    Else
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = False
                    End If

                    Dim strChallanNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Challan_No from  tspl_gate_entry_details where gate_entry_no ='" & clsCommon.myCstr(fndGateEntryNo.Value) & "'"))

                    Dim isCanType As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isCanType from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No ='" & clsCommon.myCstr(strChallanNo) & "' and Item_code='" & clsCommon.myCstr(objTr.Item_Code) & "' and Qty_KG=" & clsCommon.myCdbl(objTr.Chamber_Qty) & " ")) = 0, False, True)
                    If AllowCanInformationintoGridForTankerDispatch = True Then
                        If isCanType = True Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).Value = True
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUnloadingStatus).Value = True
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIsCanType).Value = True
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUnloadingSeqNo).Value = gvItem.Rows.Count
                        Else
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIsCanType).Value = False
                        End If
                    End If


                Next
            End If


            loadGateInQCWeighmentDateTime()
        End If
    End Sub
    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsUnloading.getData(str, navtype)
        If obj IsNot Nothing Then
            reset(False)
            lblVendorName.Text = clsDBFuncationality.getSingleValue("select isnull(tspl_gate_entry_details.Vendor_Desc,'') as Vendor_Desc from tspl_gate_entry_details where gate_entry_no='" & obj.Gate_Entry_No & "' ")
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
            txtSubLocation.Value = obj.Joblocation_Code
            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            End If
            fndUnloadingNo.Value = obj.Unloading_No
            fndGateEntryNo.Value = obj.Gate_Entry_No
            dtpUnloadingDateTime.Value = obj.Unloading_Date_Time
            FndTankerNo.Value = obj.Tanker_No
            fndReferenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
            txtWeighmentNo.Text = obj.Weighment_No
            txtQCNo.Text = obj.QC_No
            Dim strDocType As String = clsDBFuncationality.getSingleValue("select Doc_Type from TSPL_weighment_detail where gate_entry_no='" & obj.Gate_Entry_No & "' ")
            txtLocation.Text = obj.location_Code
            lblLocationName.Text = clsLocation.GetName(obj.location_Code, Nothing)
            fndSubLocation.Value = obj.Sub_location_Code
            lblSubLocationName.Text = clsLocation.GetName(obj.Sub_location_Code, Nothing)
            Dim intJobWork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(IsAgainstJobWork,0) from tspl_gate_entry_details where gate_entry_no='" & obj.Gate_Entry_No & "' "))
            If intJobWork = 1 Then
                chkJobWork.Checked = True
            Else
                chkJobWork.Checked = False
            End If
            LoadChamberstatus(strDocType)

            ''richa agarwal remove settings as per Ranjana Mam 
            loadBlankItemGrid(strDocType)
            If FinalChamberwise = 0 Then
                gvItem.Rows(0).Cells(colSlNo).Value = "1"
                gvItem.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                gvItem.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                gvItem.Rows(0).Cells(colUOM).Value = clsItemMaster.GetStockUnit(obj.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colQty).Value = obj.Qty
                gvItem.Rows(0).Cells(colFat).Value = obj.fat_per
                gvItem.Rows(0).Cells(colSNF).Value = obj.snf_Per
                gvItem.Rows(0).Cells(colFatKG).Value = obj.Qty * obj.fat_per / 100
                gvItem.Rows(0).Cells(colSNFKG).Value = obj.Qty * obj.snf_Per / 100
            End If
            'If FinalChamberwise = 1 Then

            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                gvItem.Rows.Clear()
                Dim ChamberNo As Integer = clsUnloading.isWeigmentDoneChamberwise(obj.Weighment_No, Nothing)
                For Each objTr As clsUnloadingChemberNoDetails In obj.Arr
                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUnloadingSeqNo).Value = objTr.Unloading_Sequence
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objTr.Chamber_Qty / 100
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objTr.Chamber_Qty / 100
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).Value = IIf(objTr.UnLoading_Status = 1, True, False)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colStartTime).Value = objTr.StartTime
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colEndTime).Value = objTr.EndTime
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSubLoc).Value = objTr.Sublocation_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colBatchNo).Value = objTr.Batch_No
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIsBatchItem).Value = IIf(objTr.IsBatchWise = "Y", True, False)
                    If clsCommon.myCBool(clsItemMaster.IsBatchItem(objTr.Item_Code)) Then
                        gvItem.Columns(colBatchNo).IsVisible = True
                    Else
                        gvItem.Columns(colBatchNo).IsVisible = False
                    End If
                    If obj.isPosted = 0 Then
                        If IsWeighmentUnloadingSequencewise = 1 Then
                            If gvItem.Rows(gvItem.Rows.Count - 1).Index <= ChamberNo - 1 Then
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = False
                            Else
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = True
                            End If
                        Else
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = False
                        End If
                    Else
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = True
                    End If

                    Dim strChallanNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Challan_No from  tspl_gate_entry_details where gate_entry_no ='" & clsCommon.myCstr(fndGateEntryNo.Value) & "'"))

                    If AllowCanInformationintoGridForTankerDispatch = True Then

                        Dim isCanType As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isCanType from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No ='" & clsCommon.myCstr(strChallanNo) & "' and Item_code='" & clsCommon.myCstr(objTr.Item_Code) & "' and Qty_KG=" & clsCommon.myCdbl(objTr.Chamber_Qty) & " ")) = 0, False, True)
                        If isCanType = True Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIsCanType).Value = True
                        Else
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIsCanType).Value = False
                        End If
                    End If

                Next
            End If
            'Else

            'End If
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnPrint.Enabled = True
            If obj.isPosted = 1 Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
            End If
            fndUnloadingNo.MyReadOnly = True
            loadGateInQCWeighmentDateTime()
        Else
            reset(True)
        End If
    End Sub

    Private Sub fndUnloadingNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndUnloadingNo._MYNavigator
        loadData(fndUnloadingNo.Value, NavType)
    End Sub


    Private Sub fndUnloadingNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndUnloadingNo._MYValidating
        Dim WhrCls As String = ""

        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls = "  TSPL_MILK_UNLOADING.Location_code in ( " & objCommonVar.strCurrUserLocations & " )"
            End If
        End If
        fndUnloadingNo.Value = clsUnloading.getFinder(WhrCls, fndUnloadingNo.Value, isButtonClicked)
        If clsCommon.myLen(fndUnloadingNo.Value) > 0 Then
            loadData(fndUnloadingNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub gvItem_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvItem.CellDoubleClick
        If FinalChamberwise = 1 And IsWeighmentUnloadingSequencewise = 0 Then
            Dim intCount As Integer = 0
            Dim UnloadingSeq As Integer = 0
            Dim dblTareWt As Integer = 0
            Dim WeighmentSeq As Integer = 0
            If e.Column Is gvItem.Columns(colISelect) Then
                gvItem.CurrentRow.Cells(colISelect).Value = True
                dblTareWt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Tare_Weight from TSPL_Weighment_Chember_Details where Weighment_No='" & txtWeighmentNo.Text & "' and line_No='" & gvItem.CurrentRow.Index + 1 & "'"))
                If dblTareWt > 0 Then
                    Dim TempDocType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Doc_Type from tspl_weighment_detail where weighment_no='" & txtWeighmentNo.Text & "'", Nothing))
                    If clsQualityCheck.isIntermittentDoc(clsQualityCheck.getChallanNo(fndGateEntryNo.Value, Nothing), Nothing) AndAlso settTankerDispatchIntermittentSingleGateIn = True AndAlso MCCChamberwise = 1 AndAlso clsCommon.CompairString(TempDocType, "MccProc") = CompairStringResult.Equal Then

                    Else
                        clsCommon.MyMessageBoxShow("You cannot change the status. Weighment has been done for this chamber .")
                        Exit Sub
                    End If

                Else
                        WeighmentSeq = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 Line_No from TSPL_MILK_UNLOADING left outer join TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No=TSPL_Milk_unloading_Chember_Details.Unloading_No where Weighment_No='" & txtWeighmentNo.Text & "' and UnLoading_Status=1 order by Unloading_Sequence desc"))
                    If WeighmentSeq > 0 Then
                        dblTareWt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Tare_Weight from TSPL_Weighment_Chember_Details where Weighment_No='" & txtWeighmentNo.Text & "' and line_No='" & WeighmentSeq & "'"))
                        If AllowCanInformationintoGridForTankerDispatch = True Then
                            If clsCommon.myCBool(gvItem.CurrentRow.Cells(colIsCanType).Value) = False Then
                                If dblTareWt = 0 Then
                                    clsCommon.MyMessageBoxShow("Please enter Tare weight for Chamber No " & clsCommon.myCstr(WeighmentSeq))
                                    gvItem.CurrentRow.Cells(colISelect).Value = False
                                    Exit Sub
                                End If
                            End If
                        Else

                            If dblTareWt = 0 Then
                                clsCommon.MyMessageBoxShow("Please enter Tare weight for Chamber No " & clsCommon.myCstr(WeighmentSeq))
                                gvItem.CurrentRow.Cells(colISelect).Value = False
                                Exit Sub
                            End If
                        End If

                    End If
                End If



                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    If AllowCanInformationintoGridForTankerDispatch = True Then
                        If gvItem.Rows(ii).Index <> e.RowIndex Then
                            If clsCommon.myCBool(gvItem.Rows(ii).Cells(colIsCanType).Value) = False Then
                                If clsCommon.myCBool(gvItem.Rows(ii).Cells(colUnloadingStatus).Value) = True Then
                                    intCount += 1
                                End If
                            End If
                        End If
                        If clsCommon.myCBool(gvItem.Rows(ii).Cells(colIsCanType).Value) = False Then

                            If clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True Then
                                UnloadingSeq += 1
                            End If
                        End If
                    Else
                        If gvItem.Rows(ii).Index <> e.RowIndex Then
                            If clsCommon.myCBool(gvItem.Rows(ii).Cells(colUnloadingStatus).Value) = True Then
                                intCount += 1
                            End If
                        End If
                        If clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True Then
                            UnloadingSeq += 1
                        End If
                    End If

                Next


                If intCount = 0 Then
                    If clsCommon.MyMessageBoxShow("Do you want to unload this chamber ?, Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        gvItem.CurrentRow.Cells(colUnloadingStatus).Value = True
                        gvItem.CurrentRow.Cells(colISelect).Value = True
                        gvItem.CurrentRow.Cells(colUnloadingSeqNo).Value = UnloadingSeq
                    Else
                        gvItem.CurrentRow.Cells(colUnloadingStatus).Value = False
                        gvItem.CurrentRow.Cells(colISelect).Value = False
                        gvItem.CurrentRow.Cells(colUnloadingSeqNo).Value = 0
                    End If
                Else
                    gvItem.CurrentRow.Cells(colISelect).Value = False
                End If
            End If
        End If

    End Sub

    Private Sub gvItem_CellEndEdit(sender As Object, e As GridViewCellEventArgs) Handles gvItem.CellEndEdit
        ' done by priti BHA/03/07/18-000123 for option of silo chamber wise with diff item
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            ' If FinalChamberwise = 1 AndAlso isCreateBulkProcPriceChartItemWise = 1 Then
            If FinalChamberwise = 1 Then
                If e.Column Is gvItem.Columns(colSubLoc) Then
                    If clsCommon.myLen(txtLocation.Text) <= 0 Then
                        clsCommon.MyMessageBoxShow(" Please select a tanker first ")
                        Exit Sub
                    End If
                    Dim whrCls As String = String.Empty
                    If Not clsMccMaster.isCurrentUserHO() Then
                        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                            whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
                        End If
                    End If
                    ' done by priti BHA/27/07/18-000202 to show silo Location in grid mapped with item in location item mapping screen
                    Dim ShowLocationItemLocationwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSiloLocationItemLocationwise, clsFixedParameterCode.ShowSiloLocationItemLocationwise, Nothing))
                    Dim strItemLoc As String = ""
                    If ShowLocationItemLocationwise = 1 Then
                        strItemLoc = " and Location_Code in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value) & "')"
                    End If
                    If AllowJobWorkonGateEntryBulkProc = 1 And chkJobWork.Checked Then
                        gvItem.CurrentRow.Cells(colSubLoc).Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & txtLocation.Text & "'  and Location_Type='Virtual' and UseInJobWork=1 " & whrCls & strItemLoc, fndSubLocation.Value, True)
                    Else
                        gvItem.CurrentRow.Cells(colSubLoc).Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & txtLocation.Text & "' " & whrCls & strItemLoc, clsCommon.myCstr(gvItem.CurrentRow.Cells(colSubLoc).Value), True)
                    End If
                    'If clsCommon.myLen(fndSubLocation.Value) > 0 Then
                    '    lblSubLocationName.Text = clsLocation.GetName(fndSubLocation.Value, Nothing)
                    'Else
                    '    lblLocationName.Text = ""
                    'End If
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub gvItem_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvItem.CellFormatting
      
    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        
        'If Not isCellValueChangedOpen Then
        '    isCellValueChangedOpen = True
        '    If clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value) > 0 AndAlso clsCommon.CompairString(gvItem.CurrentColumn.HeaderText, colQty) = CompairStringResult.Equal Then
        '        gvItem.Rows(0).Cells(colFatKG).Value = gvItem.Rows(0).Cells(colQty).Value * gvItem.Rows(0).Cells(colFat).Value / 100
        '        gvItem.Rows(0).Cells(colSNFKG).Value = gvItem.Rows(0).Cells(colQty).Value * gvItem.Rows(0).Cells(colSNF).Value / 100
        '    End If
        '    isCellValueChangedOpen = False
        'End If
    End Sub

    Private Sub fndSubLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSubLocation._MYValidating
        If clsCommon.myLen(txtLocation.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(" Please select a tanker first ")
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        If AllowJobWorkonGateEntryBulkProc = 1 And chkJobWork.Checked Then
            fndSubLocation.Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & txtLocation.Text & "'  and Location_Type='Virtual' and UseInJobWork=1 " & whrCls, fndSubLocation.Value, isButtonClicked)
        Else
            fndSubLocation.Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & txtLocation.Text & "' " & whrCls, fndSubLocation.Value, isButtonClicked)
        End If
        If clsCommon.myLen(fndSubLocation.Value) > 0 Then
            lblSubLocationName.Text = clsLocation.GetName(fndSubLocation.Value, Nothing)
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub FndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndTankerNo._MYValidating
        fndGateEntryNo.Value = clsUnloading.getGateEntryFinder("xx.[Is Posted]='Yes' and xx.GateEntryNo  not in (select TSPL_MILK_UNLOADING.Gate_Entry_No from TSPL_MILK_UNLOADING where TSPL_MILK_UNLOADING.gate_entry_no<>'" & fndGateEntryNo.Value & "' )  and ISNULL(xx.[Weighment No] ,'')<>''  and ISNULL(xx.[QC No] ,'')<>'' and xx.Accepted>0   and isPosted=1 ", FndTankerNo.Value, isButtonClicked)
        If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
            LoadGateEntryData(fndGateEntryNo.Value)
        Else
            reset(False)
        End If
    End Sub

    Sub loadGateInQCWeighmentDateTime(Optional trans As SqlTransaction = Nothing)
        Try
            Dim qry As String = ""
            If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
                qry = " select Date_And_Time from tspl_gate_entry_details where gate_entry_no='" & fndGateEntryNo.Value & "'"
                dtpGateEntryDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
            If clsCommon.myLen(txtWeighmentNo.Text) > 0 Then
                qry = " select Weighment_Date from tspl_weighment_detail where weighment_no='" & txtWeighmentNo.Text & "'"
                dtpWeighmentDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
            If clsCommon.myLen(txtQCNo.Text) > 0 Then
                qry = "select Qc_In_Date_Time from tspl_quality_check where QC_No='" & txtQCNo.Text & "'"
                dtpQCDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsUnloading.ReverseAndUnpost(fndUnloadingNo.Value) Then
                    clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    loadData(fndUnloadingNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class

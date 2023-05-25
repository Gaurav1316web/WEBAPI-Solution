Imports common
Imports System.Data.SqlClient
Public Class FrmMilkUnloading
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public strDocCode As String = ""
    Dim docType As String = String.Empty
    Dim obj As clsMilkUnloading = Nothing
    Dim isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub reset(ByVal isResetUnloadingNoFinder As Boolean)
        If isResetUnloadingNoFinder Then
            fndUnloadingNo.Value = ""
        End If
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
        loadBlankItemGrid()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPrint.Enabled = False
        btnPost.Enabled = False
        lblPending.Status = ERPTransactionStatus.Pending
        fndUnloadingNo.MyReadOnly = False
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset(True)
    End Sub
    Sub loadBlankItemGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing

        gvItem.Columns.Add(colSlNo, "SL. NO.")
        gvItem.Columns(colSlNo).Width = 60
        gvItem.Columns(colSlNo).ReadOnly = True

        gvItem.Columns.Add(colItemCode, "Item Code")
        gvItem.Columns(colItemCode).Width = 100
        gvItem.Columns(colItemCode).ReadOnly = True

        gvItem.Columns.Add(colItemDesc, "Item Desc")
        gvItem.Columns(colItemDesc).Width = 320
        gvItem.Columns(colItemDesc).ReadOnly = True

        gvItem.Columns.Add(colQty, "Qty")
        gvItem.Columns(colQty).Width = 120
        gvItem.Columns(colQty).ReadOnly = False
        gvItem.Columns(colQty).IsVisible = False

        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 120
        gvItem.Columns(colUOM).ReadOnly = True
        gvItem.Columns(colUOM).IsVisible = False

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
        If allowToSave() Then SaveData(False)
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Dim trans As SqlTransaction = Nothing
        Try
            obj = New clsMilkUnloading()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpUnloadingDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.MilkUnloading, "", txtLocation.Text)
                If clsCommon.myLen(obj.Unloading_No) <= 0 Then
                    clsCommon.MyMessageBoxShow("Error In Unloading  No Genertion")
                    Exit Sub
                End If
            Else
                obj.Unloading_No = clsCommon.myCstr(fndUnloadingNo.Value)
            End If
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

            If clsMilkUnloading.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully")
                    End If
                End If
                loadData(obj.Unloading_No, NavigatorType.Current)
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            btnPrint.Enabled = False
            fndUnloadingNo.MyReadOnly = False
            trans.Rollback()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try

    End Sub
    Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        obj = clsMilkUnloading.getData(fndUnloadingNo.Value, NavigatorType.Current)
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
                    If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.FrmMilkUnloading, trans) Then
                        If clsMilkUnloading.deleteData(fndUnloadingNo.Value, trans) Then
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
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsMilkUnloading.postData(fndUnloadingNo.Value, Me.Form_ID, Nothing)) Then
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
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmMilkUnloading_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        'fndGateEntryNo.Value = clsMilkUnloading.getGateEntryFinder("xx.[Is Posted]='Yes' and xx.GateEntryNo  not in (select TSPL_JOB_MILK_UNLOADING.Gate_Entry_No from TSPL_JOB_MILK_UNLOADING where TSPL_JOB_MILK_UNLOADING.gate_entry_no<>'" & fndGateEntryNo.Value & "' )  and ISNULL(xx.[Weighment No] ,'')<>''  and ISNULL(xx.[QC No] ,'')<>'' and xx.Accepted>0 ", fndGateEntryNo.Value, isButtonClicked)
        'If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
        '    LoadGateEntryData(fndGateEntryNo.Value)
        'Else
        '    reset(False)
        'End If
    End Sub
    Function allowToSave() As Boolean
        Try
            '===================Added by preeti Gupta==============
            If AllowFutureDateTransaction(dtpUnloadingDateTime.Value, Nothing) = False Then
                dtpUnloadingDateTime.Select()
                Return False
            End If
            '===========================================================
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

            If clsCommon.myLen(fndSubLocation.Value) <= 0 Then
                Throw New Exception("Please Select Silo ")
                errorControl.SetError(fndSubLocation, "Please Select Silo ")
            Else
                errorControl.ResetError(fndSubLocation)
            End If
            ''richa Against ticket no. BM00000003720 on 04/09/2014
            'If clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value) = 0 Then
            '    Throw New Exception("Qty can't be zero")
            'End If
            ''--------------------------------------------
            'If clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value) < 0 Then
            '    Throw New Exception("Qty can't be negative")
            'End If

            'Dim grsWeight As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select gross_weight from TSPL_Milk_Weighment_Detail where gate_entry_no='" & fndGateEntryNo.Value & "'"))
            'If clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value) > grsWeight Then
            '    Throw New Exception("Qty can't greator than gross weight entered at weighment")
            'End If

            Dim WDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select weighment_date from TSPL_Milk_Weighment_Detail where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            Dim QDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Qc_out_date_time from TSPL_Milk_QUALITY_CHECK where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            Dim GDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select date_and_time from Tspl_Milk_Gate_Entry_Details where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            If WDate > dtpUnloadingDateTime.Value Then
                Throw New Exception("Unloading Date time should not be less than weighment date")
            End If

            If QDate > dtpUnloadingDateTime.Value Then
                Throw New Exception("Unloading Date time should not be less than Quality Check date")
            End If

            If GDate > dtpUnloadingDateTime.Value Then
                Throw New Exception("Unloading Date time should not be less than Gate Entry date")
            End If
            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from TSPL_JOB_MILK_UNLOADING where gate_entry_no='" & fndGateEntryNo.Value & "' and unloading_no <>'" & fndUnloadingNo.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowUnloadingDateAfterCurrentDate, Nothing)) = 0 Then
                Dim dt As Date = clsCommon.GETSERVERDATE()
                If clsCommon.myCDate(dtpUnloadingDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
                    dtpUnloadingDateTime.Value = dt
                    Throw New Exception("Unloading Date should not be Larger than current date")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Sub FrmMilkUnloading_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
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
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMilkUnloading)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadGateEntryData(ByVal strGateEntryNo As String)
        Dim strUnNo As String = clsDBFuncationality.getSingleValue("select unloading_No from TSPL_JOB_MILK_UNLOADING where gate_entry_no='" & strGateEntryNo & "'")
        If clsCommon.myLen(strUnNo) > 0 Then
            loadData(strUnNo, NavigatorType.Current)
            Exit Sub
        End If
        Dim objGt As New clsMilkGateEntry()
        reset(False)
        objGt = clsMilkGateEntry.getData(strGateEntryNo, NavigatorType.Current)
        If objGt IsNot Nothing Then
            fndGateEntryNo.Value = objGt.Gate_Entry_No
            FndTankerNo.Value = objGt.Tanker_No
            Dim strWeighmentNo As String = clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_Milk_Weighment_Detail where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
            If clsCommon.myLen(strWeighmentNo) > 0 Then
                txtWeighmentNo.Text = clsCommon.myCstr(strWeighmentNo)
            Else
                txtWeighmentNo.Text = ""
            End If
            Dim strQcNo As String = clsDBFuncationality.getSingleValue("select Qc_No from TSPL_Milk_QUALITY_CHECK where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
            If clsCommon.myLen(strQcNo) > 0 Then
                txtQCNo.Text = clsCommon.myCstr(strQcNo)
            Else
                txtQCNo.Text = ""
            End If
            Dim strLocCode As String = objGt.location_Code
            Dim strLocDesc As String = clsLocation.GetName(strLocCode, Nothing)
            If clsCommon.myLen(strLocCode) > 0 Then
                txtLocation.Text = strLocCode
                lblLocationName.Text = strLocDesc
            Else
                txtLocation.Text = ""
                lblLocationName.Text = ""
            End If
            fndSubLocation.Value = ""
            lblSubLocationName.Text = ""
            gvItem.Rows(0).Cells(colSlNo).Value = "1"
            gvItem.Rows(0).Cells(colItemCode).Value = objGt.Item_Code
            gvItem.Rows(0).Cells(colItemDesc).Value = clsItemMaster.GetItemName(objGt.Item_Code, Nothing)
            gvItem.Rows(0).Cells(colUOM).Value = clsItemMaster.GetStockUnit(objGt.Item_Code, Nothing)
            gvItem.Rows(0).Cells(colQty).Value = ""
            gvItem.Rows(0).Cells(colFat).Value = objGt.fat_per
            gvItem.Rows(0).Cells(colSNF).Value = objGt.snf_Per
            gvItem.Rows(0).Cells(colFatKG).Value = ""
            gvItem.Rows(0).Cells(colSNFKG).Value = ""
            loadGateInQCWeighmentDateTime()
        End If
    End Sub
    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsMilkUnloading.getData(str, navtype)
        If obj IsNot Nothing Then
            reset(False)
            fndUnloadingNo.Value = obj.Unloading_No
            fndGateEntryNo.Value = obj.Gate_Entry_No
            dtpUnloadingDateTime.Value = obj.Unloading_Date_Time
            FndTankerNo.Value = obj.Tanker_No
            txtWeighmentNo.Text = obj.Weighment_No
            txtQCNo.Text = obj.QC_No
            txtLocation.Text = obj.location_Code
            lblLocationName.Text = clsLocation.GetName(obj.location_Code, Nothing)
            fndSubLocation.Value = obj.Sub_location_Code
            lblSubLocationName.Text = clsLocation.GetName(obj.Sub_location_Code, Nothing)
            gvItem.Rows(0).Cells(colSlNo).Value = "1"
            gvItem.Rows(0).Cells(colItemCode).Value = obj.Item_Code
            gvItem.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
            gvItem.Rows(0).Cells(colUOM).Value = clsItemMaster.GetStockUnit(obj.Item_Code, Nothing)
            gvItem.Rows(0).Cells(colQty).Value = obj.Qty
            gvItem.Rows(0).Cells(colFat).Value = obj.fat_per
            gvItem.Rows(0).Cells(colSNF).Value = obj.snf_Per
            gvItem.Rows(0).Cells(colFatKG).Value = obj.Qty * obj.fat_per / 100
            gvItem.Rows(0).Cells(colSNFKG).Value = obj.Qty * obj.snf_Per / 100
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
                WhrCls = "  Location_code in ( " & objCommonVar.strCurrUserLocations & " )"
            End If
        End If
        fndUnloadingNo.Value = clsMilkUnloading.getFinder(WhrCls, fndUnloadingNo.Value, isButtonClicked)
        If clsCommon.myLen(fndUnloadingNo.Value) > 0 Then
            loadData(fndUnloadingNo.Value, NavigatorType.Current)
        End If
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
        fndSubLocation.Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & txtLocation.Text & "' " & whrCls, fndSubLocation.Value, isButtonClicked)
        If clsCommon.myLen(fndSubLocation.Value) > 0 Then
            lblSubLocationName.Text = clsLocation.GetName(fndSubLocation.Value, Nothing)
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub FndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndTankerNo._MYValidating
        fndGateEntryNo.Value = clsMilkUnloading.getGateEntryFinder("xx.[Is Posted]='Yes' and xx.GateEntryNo  not in (select TSPL_JOB_MILK_UNLOADING.Gate_Entry_No from TSPL_JOB_MILK_UNLOADING where TSPL_JOB_MILK_UNLOADING.gate_entry_no<>'" & fndGateEntryNo.Value & "' )  and ISNULL(xx.[Weighment No] ,'')<>''  and ISNULL(xx.[QC No] ,'')<>'' and xx.Accepted>0 ", FndTankerNo.Value, isButtonClicked)
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
                qry = " select Date_And_Time from Tspl_Milk_Gate_Entry_Details where gate_entry_no='" & fndGateEntryNo.Value & "'"
                dtpGateEntryDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
            If clsCommon.myLen(txtWeighmentNo.Text) > 0 Then
                qry = " select Weighment_Date from TSPL_Milk_Weighment_Detail where weighment_no='" & txtWeighmentNo.Text & "'"
                dtpWeighmentDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
            If clsCommon.myLen(txtQCNo.Text) > 0 Then
                qry = "select Qc_In_Date_Time from TSPL_Milk_QUALITY_CHECK where QC_No='" & txtQCNo.Text & "'"
                dtpQCDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

End Class

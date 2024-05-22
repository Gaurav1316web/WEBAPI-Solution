'--------Created By Richa 28/07/2014 Against Ticket No BM00000003157 and BM00000003247
''-------------updation by richa Against Ticket no BM00000003767
'' Updation By Richa Agarwal Against Ticket No. BM00000003776 on 09/09/2014, against Ticket No BM00000003954 on 23/09/2014
Imports common
Imports System.Data.SqlClient

Public Class FrmWeighmentEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Public strDocCode As String = ""
    Dim ApplyMultiChamber As Boolean = False
    Public isCellValueChangedOpen = False
    Public isInsideloaddata = False
    Public TareWeight As Double = 0
    Dim ApplyTSPriceAtBulkSale As Boolean = False
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colWeighmentSeqNo As String = "colWeighmentSeqNo"
    Public Const colUOM As String = "UOM"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
#End Region

#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub fndGateEntryNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndGateEntryNo._MYValidating
        'fndGateEntryNo.Value = clsGateEntrySale.getFinder(" Posted=1 and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") and  Document_No not in (Select GateEntry_Document_No from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No<> '" + fndWeighmentcode.Value + "' and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") )  ", fndGateEntryNo.Value, isButtonClicked)
        'Qry = "SELECT Tanker_No AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" + fndGateEntryNo.Value + "' "
        'lblTankerNoValue.Text = clsDBFuncationality.getSingleValue(Qry)
        'LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Tanker_Name from TSPL_TANKER_MASTER where Tanker_No  ='" + lblTankerNoValue.Text + "' ")
        'Qry = "SELECT Location_Code AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" + fndGateEntryNo.Value + "' "
        'lblLocationCode.Text = clsDBFuncationality.getSingleValue(Qry)
        'LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")

    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'lblLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub



    Private Sub FrmWeighmentEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ApplyMultiChamber = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyMultiChamberInBulkWeighmentEntry, clsFixedParameterCode.ApplyMultiChamberInBulkWeighmentEntry, Nothing)) = 1, True, False))
        ApplyTSPriceAtBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, Nothing)) = 1, True, False))
        If ApplyMultiChamber Then
            SplitContainer3.Panel1Collapsed = True
            loadBlankGvItemBulk()
        Else
            SplitContainer3.Panel2Collapsed = True
        End If
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoTankerWeightment, clsFixedParameterCode.IsAutoTankerWeightment, Nothing)) > 0 Then
            txtGrossWeight.ReadOnly = True
            UcWeighing1.Enabled = True

            UcWeighing1.form_ID = MyBase.Form_ID
            UcWeighing1.LoadPortAndMachine()
            UcWeighing1.LoadSettingAndStart()
        Else
            UcWeighing1.Enabled = False
        End If

        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoTankerWeighmentForBulkSale, clsFixedParameterCode.IsAutoTankerWeighmentForBulkSale, Nothing)) > 0 Then
            TxtTareWeight.ReadOnly = True
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmWeighmentEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnclose.Visible = MyBase.isCancel_Flag
    End Sub

    Sub Reset()
        If ApplyMultiChamber Then
            loadBlankGvItemBulk()
        End If
        fndWeighmentcode.Value = ""
        fndGateEntryNo.Value = ""
        lblGateEntryNo.Text = ""
        'lblTankerNoValue.Text = ""
        'LblTankerName.Text = ""
        FndTankerNo.Value = ""
        lblLocationCode.Text = ""
        LblLocationName.Text = ""
        txtNetWeight.Value = 0
        txtGrossWeight.Value = 0
        TxtTareWeight.Value = 0
        LOCATIONRIGTHS()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtWeighmentdate.Value = clsCommon.GETSERVERDATE()
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtWeighmentdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtWeighmentdate.CustomFormat = "dd/MM/yyyy"
        End If
        fndWeighmentcode.MyReadOnly = False
        txtGrossWeight.Enabled = False
        txtNetWeight.Enabled = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        lblQCStatus.Visible = False
        chkPendingGrossWeight.Checked = False
    End Sub

    Sub loadBlankGvItemBulk()
        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing

        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        gvItemBulk.Columns(colItemCode).ReadOnly = False

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItemBulk.Columns(colChamberDesc).Width = 150
        gvItemBulk.Columns(colChamberDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = False

        gvItemBulk.Columns.Add(colQty, "Chamber Capacity ")
        gvItemBulk.Columns(colQty).Width = 120
        gvItemBulk.Columns(colQty).ReadOnly = True

        gvItemBulk.Columns.Add(colTareWeight, "Tare Weight")
        gvItemBulk.Columns(colTareWeight).Width = 75
        gvItemBulk.Columns(colTareWeight).ReadOnly = True

        gvItemBulk.Columns.Add(colGrossWeight, "Gross Weight")
        gvItemBulk.Columns(colGrossWeight).Width = 75
        gvItemBulk.Columns(colGrossWeight).ReadOnly = False

        gvItemBulk.Columns.Add(colNetWeight, "Net Weight")
        gvItemBulk.Columns(colNetWeight).Width = 75
        gvItemBulk.Columns(colNetWeight).ReadOnly = True

        gvItemBulk.Columns.Add(colWeighmentSeqNo, "Sequence No")
        gvItemBulk.Columns(colWeighmentSeqNo).Width = 120
        gvItemBulk.Columns(colWeighmentSeqNo).ReadOnly = True

        gvItemBulk.AllowAddNewRow = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
    End Sub

    Private Sub FrmWeighmentEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            Try
                If clsCommon.CompairString(lblQCStatus.Text, "QC Done") = CompairStringResult.Equal Then
                    If txtGrossWeight.ReadOnly = True Then
                        txtGrossWeight.Text = UcWeighing1.LiveReading
                    End If
                Else
                    If TxtTareWeight.ReadOnly = True Then
                        TxtTareWeight.Text = UcWeighing1.LiveReading
                    End If
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub CloseForm()
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try

        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub SaveData(Optional ByVal ispost As Boolean = False)
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As New ClsWeighmentEntry()
        Try
            If AllowToSave(ispost) Then

                obj.Weighment_No = fndWeighmentcode.Value
                obj.Weighment_Date = txtWeighmentdate.Value
                obj.GateEntry_Document_No = lblGateEntryNo.Text
                obj.Location_Code = lblLocationCode.Text
                ' obj.Tanker_No = lblTankerNoValue.Text
                obj.Tanker_No = FndTankerNo.Value
                obj.Tare_Weight = TxtTareWeight.Value
                obj.Gross_Weight = txtGrossWeight.Value
                obj.Net_Weight = txtNetWeight.Value
                'Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Weighment_No) from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + obj.Weighment_No + "'", trans)
                'If (qry = 0) Then
                '    isNewEntry = True
                'Else
                '    isNewEntry = False
                'End If

                If ApplyMultiChamber Then
                    obj.Arr = New List(Of clsWeighmentEntryChemberNoDetails)
                    For Each grow As GridViewRowInfo In gvItemBulk.Rows
                        Dim objTr As New clsWeighmentEntryChemberNoDetails()
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                        objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Gross_Weight = clsCommon.myCdbl(grow.Cells(colGrossWeight).Value)
                        objTr.Tare_Weight = clsCommon.myCdbl(grow.Cells(colTareWeight).Value)
                        objTr.Net_Weight = clsCommon.myCdbl(grow.Cells(colNetWeight).Value)
                        objTr.Weighment_Sequence = clsCommon.myCdbl(grow.Cells(colWeighmentSeqNo).Value)
                        If (clsCommon.myLen(objTr.Chamber_Desc) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    Next
                End If

                If (ClsWeighmentEntry.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        'If isNewEntry Then
                        clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                        LoadData(obj.Weighment_No, NavigatorType.Current)
                        'Else
                        '    clsCommon.MyMessageBoxShow("Data updated successfully", Me.Text)
                        '    LoadData(obj.Weighment_No, NavigatorType.Current)
                        'End If
                    End If

                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Function AllowToSave(Optional ByVal ispost As Boolean = False) As Boolean
        'If clsCommon.myLen(fndGateEntryNo.Value) <= 0 Then
        '    fndGateEntryNo.Focus()
        '    Throw New Exception("Gate Entry No cannot be left blank")
        'End If

        ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
        If AllowFutureDateTransaction(txtWeighmentdate.Value, Nothing) = False Then
            txtWeighmentdate.Focus()
            txtWeighmentdate.Select()
            Return False
        End If

        If clsCommon.myLen(FndTankerNo.Value) <= 0 Then
            FndTankerNo.Focus()
            Throw New Exception("Tanker No cannot be left blank")
        End If
        If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date,Document_Date,103) from TSPL_GATEENTRY_SALE where Document_No ='" + lblGateEntryNo.Text + "'")) > clsCommon.myCDate(txtWeighmentdate.Value) Then
            txtWeighmentdate.Focus()
            Throw New Exception("Weightment Entry Date cannot be less than from Gate Entry No Date")
        End If
        If Not ApplyMultiChamber Then
            If clsCommon.myCdbl(TxtTareWeight.Value) <= 0 Then
                TxtTareWeight.Focus()
                Throw New Exception("Tare Weight cannot be Zero or blank")
            End If
            If txtGrossWeight.Enabled = True Then
                If clsCommon.myCdbl(txtGrossWeight.Value) <= clsCommon.myCdbl(TxtTareWeight.Value) Then
                    txtGrossWeight.Focus()
                    Throw New Exception("Gross Weight should be greater than from Tare Weight")
                End If
            End If
        Else
            If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value) <= 0 Then
                Throw New Exception("Tare Weight cannot be Zero or blank at line no 1")
            End If
            If ispost Then
                Dim i As Integer = 0
                For i = 0 To gvItemBulk.Rows.Count - 1
                    If clsCommon.myCdbl(gvItemBulk.Rows(i).Cells(colGrossWeight).Value) <= 0 Then
                        Throw New Exception("Gross Weight cannot be Zero or blank at line no " + i.ToString())
                    End If
                Next
            End If
        End If
        'If clsCommon.myLen(SubLocation) > 0 Then
        '    balqty = ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value), lblLocationCode.Text, SubLocation, txtDocNo.Value, txtDate.Value, Nothing, "KG")
        'Else
        '    balqty = ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value), lblLocationCode.Text, "", txtDocNo.Value, txtDate.Value, Nothing, "KG")
        'End If
        'dblToleranceQty = ClsLoadingTanker.GetTolerane(balqty, dispatchqty)

        'If dblToleranceQty < dispatchqty Then
        '    Throw New Exception("You cannot dispatch because stock quantity exceeds the tolerance limit.You have " + Environment.NewLine + " Stock Qty  " + clsCommon.myCstr(balqty) + "  " + Environment.NewLine + " With Tolerance Qty  " + clsCommon.myCstr(dblToleranceQty) + "  " + Environment.NewLine + " .")
        'End If


        Return True

    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        isInsideloaddata = True
        Reset()
        Dim obj As ClsWeighmentEntry = ClsWeighmentEntry.GetData(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndWeighmentcode.Value = obj.Weighment_No
            txtWeighmentdate.Value = obj.Weighment_Date
            lblGateEntryNo.Text = obj.GateEntry_Document_No
            'lblTankerNoValue.Text = obj.Tanker_No
            FndTankerNo.Value = obj.Tanker_No
            ' LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Tanker_Name from TSPL_TANKER_MASTER where Tanker_No  ='" + lblTankerNoValue.Text + "' ")
            lblLocationCode.Text = obj.Location_Code
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")

            TxtTareWeight.Value = obj.Tare_Weight
            txtGrossWeight.Value = obj.Gross_Weight
            txtNetWeight.Value = obj.Net_Weight

            ''richa 10/10/2014
            lblQCStatus.Visible = True
            If ApplyMultiChamber Then
                lblQCStatus.Text = clsDBFuncationality.getSingleValue("Select case when Isnull(TSPL_Quality_Check_BulkSale.Posted,0)  =0 then 'QC Pending' else 'QC Done' end as [QC_Status]  from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_TANKER_MASTER_SALE  on TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER_SALE.tanker_code Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_Quality_Check_BulkSale.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No='" & obj.Weighment_No & "'")
            Else
                lblQCStatus.Text = clsDBFuncationality.getSingleValue("Select case when Isnull(TSPL_Quality_Check_BulkSale.Posted,0)  =0 then 'QC Pending' else 'QC Done' end as [QC_Status]  from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_TANKER_MASTER  on TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_Quality_Check_BulkSale.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No='" & obj.Weighment_No & "'")
            End If


            ''================
            'richa 02/03/2015
            Dim check1 As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Count(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No) as Weighment_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE Inner Join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No =TSPL_Quality_Check_BulkSale.LoadingTanker_No  where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No ='" + fndWeighmentcode.Value + "' and TSPL_Quality_Check_BulkSale.Posted=1"))
            If check1 > 0 Then
                txtGrossWeight.Enabled = True
            ElseIf check1 <= 0 Then
                txtGrossWeight.Enabled = False
            End If

            ''----------------
            fndWeighmentcode.MyReadOnly = True
            btnsave.Text = "Update"

            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            '-----------------------------------------------------------------------------------------------''
            If ApplyMultiChamber Then
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    gvItemBulk.Rows.Clear()
                    For Each objTr As clsWeighmentEntryChemberNoDetails In obj.Arr
                        gvItemBulk.Rows.AddNew()
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myFormat(objTr.Gross_Weight)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myFormat(objTr.Tare_Weight)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(objTr.Net_Weight)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colWeighmentSeqNo).Value = objTr.Weighment_Sequence
                    Next
                End If
            End If
            '==================end here============
        End If
        obj = Nothing
        isInsideloaddata = False
    End Sub

    Sub LoadDataPendingGrossweight(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Reset()
        Dim obj As ClsWeighmentEntry = ClsWeighmentEntry.GetDataforpendingGrossweight(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndWeighmentcode.Value = obj.Weighment_No
            txtWeighmentdate.Value = obj.Weighment_Date
            lblGateEntryNo.Text = obj.GateEntry_Document_No
            FndTankerNo.Value = obj.Tanker_No
            lblLocationCode.Text = obj.Location_Code
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")

            TxtTareWeight.Value = obj.Tare_Weight
            txtGrossWeight.Value = obj.Gross_Weight
            txtNetWeight.Value = obj.Net_Weight

            lblQCStatus.Visible = True
            If ApplyMultiChamber Then
                lblQCStatus.Text = clsDBFuncationality.getSingleValue("Select case when Isnull(TSPL_Quality_Check_BulkSale.Posted,0)  =0 then 'QC Pending' else 'QC Done' end as [QC_Status]  from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_TANKER_MASTER_SALE  on TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER_SALE.Tanker_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_Quality_Check_BulkSale.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No='" & obj.Weighment_No & "'")
            Else
                lblQCStatus.Text = clsDBFuncationality.getSingleValue("Select case when Isnull(TSPL_Quality_Check_BulkSale.Posted,0)  =0 then 'QC Pending' else 'QC Done' end as [QC_Status]  from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_TANKER_MASTER  on TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_Quality_Check_BulkSale.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No='" & obj.Weighment_No & "'")
            End If



            fndWeighmentcode.MyReadOnly = True
            btnsave.Text = "Update"

            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If

        End If
        obj = Nothing
    End Sub

    Private Sub DeleteData()
        Dim arr As List(Of String) = New List(Of String)
        Try
            Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(LoadingTanker_No) from TSPL_LOADING_TANKER_DETAIL_BULKSALE where Weighment_No='" + fndWeighmentcode.Value + "'")
            If (qry = 0) Then
                If (deleteConfirm()) Then
                    arr.Add(fndWeighmentcode.Value)
                    If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.FrmWeighmentEntry, Nothing) Then
                        If (ClsWeighmentEntry.DeleteData(fndWeighmentcode.Value)) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                            Reset()
                        End If
                    End If
                End If
            Else
                Throw New Exception("You cannot delete this entry because it is used in Loading Tanker")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub fndWeighmentcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndWeighmentcode._MYNavigator
        Dim qry As String = String.Empty
        Try

            Qry = "select count(*) from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + fndWeighmentcode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If check > 0 Then
                fndWeighmentcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndWeighmentcode.MyReadOnly = False
            End If
            If chkPendingGrossWeight.Checked Then
                LoadDataPendingGrossweight(fndWeighmentcode.Value, NavType)
                chkPendingGrossWeight.Checked = True
            Else
                LoadData(fndWeighmentcode.Value, NavType)
                chkPendingGrossWeight.Checked = False
            End If
            'LoadData(fndWeighmentcode.Value, NavType)
            ' Dim check1 As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Count(Weighment_No) as Weighment_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE where Weighment_No ='" + fndWeighmentcode.Value + "'"))
            Dim check1 As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Count(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No) as Weighment_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE Inner Join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No =TSPL_Quality_Check_BulkSale.LoadingTanker_No  where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No ='" + fndWeighmentcode.Value + "' and TSPL_Quality_Check_BulkSale.Posted=1"))
            If check1 > 0 Then
                txtGrossWeight.Enabled = True
            ElseIf check1 <= 0 Then
                txtGrossWeight.Enabled = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndWeighmentcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWeighmentcode._MYValidating
        'Dim qry As String = "Select Weighment_No as Code,Weighment_Date from TSPL_WEIGHMENT_DETAIL_BULKSALE "
        ' Dim qry As String = "Select TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No as Code,convert(varchar,TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_Date,103) as Date,case when (Select Posted from TSPL_Quality_Check_BulkSale where Weighment_No ='" + fndWeighmentcode.Value + "')=0 then 'QC Pending' else 'QC Done' end as [QC Status] ,TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No as [Tanker no],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],case when TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_TANKER_MASTER  on TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code "
        Dim qry As String = "Select TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No as Code,convert(varchar,TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_Date,103) as Date,case when Isnull(TSPL_Quality_Check_BulkSale.Posted,0)  =0 then 'QC Pending' else 'QC Done' end as [QC Status] ,TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No as [Tanker no],TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],case when TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_Quality_Check_BulkSale.Weighment_No "
        Dim strwhrcls As String = " TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ")"
        ' Dim strval As String = ""
        If chkPendingGrossWeight.Checked Then
            ' strval = "yes"
            strwhrcls = strwhrcls + " and TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted=0"
        End If
        fndWeighmentcode.Value = clsCommon.ShowSelectForm("WeighmentEntry", qry, "Code", strwhrcls, fndWeighmentcode.Value, "", isButtonClicked)
        LoadData(fndWeighmentcode.Value, NavigatorType.Current)

        'Dim check As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Count(Weighment_No) as Weighment_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE Inner Join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No =TSPL_Quality_Check_BulkSale.LoadingTanker_No  where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No ='" + fndWeighmentcode.Value + "' and TSPL_Quality_Check_BulkSale.Posted='Y'"))
        Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No) as Weighment_No  from TSPL_WEIGHMENT_DETAIL_BULKSALE  inner join TSPL_Quality_Check_BulkSale on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_Quality_Check_BulkSale.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No='" + fndWeighmentcode.Value + "' and TSPL_Quality_Check_BulkSale.Posted=1 "))
        If check > 0 Then
            txtGrossWeight.Enabled = True
        ElseIf check <= 0 Then
            txtGrossWeight.Enabled = False
        End If
        If btnPost.Enabled Then
            chkPendingGrossWeight.Checked = True
        Else
            chkPendingGrossWeight.Checked = False
        End If
        qry = Nothing
        strwhrcls = Nothing
    End Sub

    Private Sub txtGrossWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGrossWeight.TextChanged
        If clsCommon.myCdbl(txtGrossWeight.Value) > 0 Then
            txtNetWeight.Value = clsCommon.myCdbl(clsCommon.myCdbl(txtGrossWeight.Value) - clsCommon.myCdbl(TxtTareWeight.Value))
        Else
            txtNetWeight.Value = 0
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Dim msg As String = String.Empty
        Dim qry As String = String.Empty
        Dim dt As DataTable = Nothing
        Try

            Dim grossweight As Double = 0
            isFlag = True
            If clsCommon.myLen(fndWeighmentcode.Value) > 0 Then
                If ApplyMultiChamber Then
                    grossweight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select max(Gross_Weight) from TSPL_WeighmentBulkSale_Chember_Details where Weighment_No='" + fndWeighmentcode.Value + "'"))
                Else
                    grossweight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Gross_Weight from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + fndWeighmentcode.Value + "'"))
                End If

                If (grossweight > 0 And txtGrossWeight.Enabled = True) Then
                    If (myMessages.postConfirm()) Then
                        SaveData(True)
                        If (ClsWeighmentEntry.PostData(MyBase.Form_ID, arrLoc, fndWeighmentcode.Value)) Then
                            msg = "Successfully Posted"
                            common.clsCommon.MyMessageBoxShow(msg)
                            LoadData(fndWeighmentcode.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    If txtGrossWeight.Enabled = False Then
                        Throw New Exception("You cannot post this entry before Posting of QC")
                    Else
                        Throw New Exception("You cannot post this entry before entering Gross Weight")
                    End If

                End If

            Else
                Throw New Exception("Weighment No not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
            msg = Nothing
            Qry = Nothing
            dt = Nothing
        End Try
    End Sub

    Private Sub FndTankerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndTankerNo._MYValidating
        If chkPendingGrossWeight.Checked Then
            fndWeighmentcode.Value = ClsWeighmentEntry.getTankerFinder(" TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted=0 and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") ", FndTankerNo.Value, isButtonClicked)
            LoadData(fndWeighmentcode.Value, NavigatorType.Current)
            Dim check1 As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Count(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No) as Weighment_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE Inner Join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No =TSPL_Quality_Check_BulkSale.LoadingTanker_No  where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No ='" + fndWeighmentcode.Value + "' and TSPL_Quality_Check_BulkSale.Posted=1"))
            If check1 > 0 Then
                txtGrossWeight.Enabled = True
            ElseIf check1 <= 0 Then
                txtGrossWeight.Enabled = False
            End If
            chkPendingGrossWeight.Checked = True

        Else
            lblGateEntryNo.Text = clsGateEntrySale.getTankerFinder(" Posted=1 and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") and  Document_No not in (Select GateEntry_Document_No from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No<> '" + fndWeighmentcode.Value + "' and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") ) and ISNULL(TSPL_GATEENTRY_SALE.Dispatch_No ,'')='' and ISNULL(TSPL_GATEENTRY_SALE.SaleReturnAgaintGEN,'')='' and (TSPL_GATEENTRY_SALE.IsSaleReturn ='Y' or TSPL_GATEENTRY_SALE.IsSaleReturn ='N') ", FndTankerNo.Value, isButtonClicked)
            Qry = "SELECT Tanker_No AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" & lblGateEntryNo.Text & "' "
            FndTankerNo.Value = clsDBFuncationality.getSingleValue(Qry)
            'LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Tanker_Name from TSPL_TANKER_MASTER where Tanker_No  ='" + lblTankerNoValue.Text + "' ")
            Qry = "SELECT Location_Code AS Code  FROM TSPL_GATEENTRY_SALE where Document_No='" & lblGateEntryNo.Text & "' "
            lblLocationCode.Text = clsDBFuncationality.getSingleValue(Qry)
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
            If ApplyMultiChamber Then
                LoadDataingvitembulk(FndTankerNo.Value)
            Else

            End If
        End If
    End Sub

    Public Sub LoadDataingvitembulk(ByVal tankercode As String)
        isInsideloaddata = True
        Try
            gvItemBulk.Rows.Clear()
            Dim qry As String = Nothing
            Dim dt As DataTable = Nothing
            qry = "select * from TSPL_TANKER_MASTER_SALE left outer join TSPL_TANKER_MASTER_SALE_DETAIL on TSPL_TANKER_MASTER_SALE_DETAIL.Tanker_Code = TSPL_TANKER_MASTER_SALE.Tanker_Code	where TSPL_TANKER_MASTER_SALE.Tanker_Code='" + clsCommon.myCstr(tankercode) + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strMainQry As String = clsGateEntrySale.getItemsDetail()
            Dim strQry As String = ""
            Dim dtDetail As New DataTable()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvItemBulk.Rows.AddNew()
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = gvItemBulk.Rows.Count
                    If ApplyTSPriceAtBulkSale = True Then
                        strQry = strMainQry + " where  TSPL_GATEENTRY_SALE.Tanker_No='" + clsCommon.myCstr(tankercode) + "' and TSPL_GATEENTRY_SALE.Document_No='" + clsCommon.myCstr(lblGateEntryNo.Text) + "' and isnull(TSPL_SALES_ORDER_DETAIL_BULKSALE.item_code,'')!='' "
                        dtDetail = clsDBFuncationality.GetDataTable(strQry)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dtDetail.Rows(0)("item_code"))
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dtDetail.Rows(0)("item_name"))
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(dtDetail.Rows(0)("Unit_code"))
                    Else
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = clsFixedParameter.GetData(clsFixedParameterType.BSDefaultMilkItem, clsFixedParameterCode.BSDefaultMilkItem, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.BSDefaultMilkItem, clsFixedParameterCode.BSDefaultMilkItem, Nothing)) & "'")
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = clsFixedParameter.GetData(clsFixedParameterType.DefaultItemUOMForBulkSale, clsFixedParameterCode.DefaultItemUOMForBulkSale, Nothing)
                    End If
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("chamber_capacity").ToString())
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = clsCommon.myCstr(dr("chamber_desc").ToString())
                    If gvItemBulk.Rows.Count <> 1 Then
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colTareWeight).Value = 0
                    Else
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myCstr(dr("Tare_Weight").ToString())
                    End If

                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colNetWeight).Value = ""
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colGrossWeight).Value = 0
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colWeighmentSeqNo).Value = ""
                Next
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
        isInsideloaddata = False
    End Sub

    Private Sub gvItemBulk_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvItemBulk.CellFormatting
        If e.Column Is gvItemBulk.Columns(colGrossWeight) Then
            Dim LoadingCount As Integer = 0
            LoadingCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 Line_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE left outer join TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE.loading_No where Weighment_No='" & fndWeighmentcode.Value & "' and Loading_Status=1 order by loading_Sequence desc"))
            If e.RowIndex = LoadingCount - 1 Then
                If UcWeighing1.Enabled = False Then
                    gvItemBulk.CurrentRow.Cells(colGrossWeight).ReadOnly = False
                End If
            Else
                gvItemBulk.CurrentRow.Cells(colGrossWeight).ReadOnly = True
            End If
        End If
    End Sub


    Private Sub gvItemBulk_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItemBulk.CellValueChanged
        Try
            If isInsideloaddata = False Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItemBulk.Columns(colItemCode) Then
                        OpenICodeList(False)
                    ElseIf e.Column Is gvItemBulk.Columns(colItemCode) Then
                        OpenUOMList(False)
                    End If
                    If e.Column Is gvItemBulk.Columns(colGrossWeight) Then
                        gvItemBulk.CurrentRow.Cells(colGrossWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value, 0))
                        calculate()
                        Dim p As Integer = 0
                        For p = 0 To gvItemBulk.Rows.Count - 1
                            updaterow()
                        Next
                    End If
                    If e.Column Is gvItemBulk.Columns(colTareWeight) Then

                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        gvItemBulk.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("Product_Type='MI' and Active=1", gvItemBulk.CurrentRow.Cells(colItemCode).Value, False)
        If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colItemCode).Value) > 0 Then
            gvItemBulk.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsItemMaster.GetItemName(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing))
            gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & gvItemBulk.CurrentRow.Cells(colItemCode).Value & "' and Default_UOM='1' "))
            If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colUOM).Value) <= 0 Then
                gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing)
            End If
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
        End If
    End Sub

    Sub calculate()
        Try
            TareWeight = clsDBFuncationality.getSingleValue("select tare_weight from TSPL_TANKER_MASTER_SALE where tanker_code='" + clsCommon.myCstr(FndTankerNo.Value) + "'")
            Dim qry As Double = Nothing
            Dim twt As Double = Nothing
            Dim seqno As Integer = 0
            For Each grow As GridViewRowInfo In gvItemBulk.Rows
                If clsCommon.myCdbl(grow.Cells(colWeighmentSeqNo).Value) = (clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colWeighmentSeqNo).Value) - 1) Then
                    twt = clsCommon.myCdbl(grow.Cells(colGrossWeight).Value)
                    seqno = CInt(gvItemBulk.CurrentRow.Cells(colWeighmentSeqNo).Value)
                Else
                    seqno = Math.Max(seqno, CInt(clsCommon.myCdbl(grow.Cells(colWeighmentSeqNo).Value)))
                End If
            Next
            If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colWeighmentSeqNo).Value) <= 0 Then
                gvItemBulk.CurrentRow.Cells(colWeighmentSeqNo).Value = seqno + 1
            End If
            If twt > 0 Then
                gvItemBulk.CurrentRow.Cells(colTareWeight).Value = twt
            Else
                gvItemBulk.CurrentRow.Cells(colTareWeight).Value = TareWeight
            End If

            gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown(clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value) - clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value), 2)

        Catch ex As Exception

        End Try
    End Sub

    Sub updaterow()
        Dim i As Integer = 0
        Dim k As Integer = 0
        Dim t As Integer = 0
        TareWeight = clsDBFuncationality.getSingleValue("select tare_weight from TSPL_TANKER_MASTER_SALE where tanker_code='" + clsCommon.myCstr(FndTankerNo.Value) + "'")
        For i = 0 To gvItemBulk.Rows.Count - 1
            For k = 0 To gvItemBulk.Rows.Count - 1
                If clsCommon.myCdbl(gvItemBulk.Rows(i).Cells(colWeighmentSeqNo).Value) = (clsCommon.myCdbl(gvItemBulk.Rows(k).Cells(colWeighmentSeqNo).Value) - 1) Then
                    If clsCommon.myCdbl(gvItemBulk.Rows(k).Cells(colWeighmentSeqNo).Value) = 1 Then
                        gvItemBulk.Rows(k).Cells(colTareWeight).Value = TareWeight
                    Else
                        gvItemBulk.Rows(k).Cells(colTareWeight).Value = clsCommon.myCdbl(gvItemBulk.Rows(i).Cells(colGrossWeight).Value)
                    End If
                    gvItemBulk.Rows(k).Cells(colNetWeight).Value = MyMath.RoundDown(clsCommon.myCdbl(gvItemBulk.Rows(k).Cells(colGrossWeight).Value) - clsCommon.myCdbl(gvItemBulk.Rows(k).Cells(colTareWeight).Value), 2)

                End If
            Next
        Next
    End Sub

    Private Function GetDataTable(dtg As RadGridView) As DataTable
        Dim dt As New DataTable()

        Return dt
    End Function

    Private Sub frmMilkReceiptMCC_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            UcWeighing1.CloseCOMPORT()
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                btnPrint.Visible = True
            Else
                btnPrint.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    'sanjay Ticket No-UDL/26/04/19-000292
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim frmCRV As New frmCrystalReportViewer()

        Dim Qry As String = Nothing
        Qry = "select  TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No,convert(varchar(15),TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_date,103) as Weighment_date " & _
            ",TSPL_CUSTOMER_MASTER.cust_code as Customer_Code " & _
             ",TSPL_WEIGHMENT_DETAIL_BULKSALE.TANKER_NO " & _
            ",TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No  , TSPL_COMPANY_MASTER.Comp_Name " & _
            ",TSPL_CUSTOMER_MASTER.customer_Name AS Customer_Name,TSPL_WEIGHMENT_DETAIL_BULKSALE.Gross_Weight as Gross_Weight, TSPL_WEIGHMENT_DETAIL_BULKSALE.Tare_Weight,TSPL_WEIGHMENT_DETAIL_BULKSALE.Net_Weight " & _
             "  from TSPL_WEIGHMENT_DETAIL_BULKSALE " & _
            " left outer join TSPL_GATEENTRY_SALE  on TSPL_GATEENTRY_SALE.document_no =TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No " & _
            " left join TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.cust_code=TSPL_GATEENTRY_SALE.customer_code  " & _
            " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code=TSPL_WEIGHMENT_DETAIL_BULKSALE.comp_code " & _
            "  where TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No ='" + fndWeighmentcode.Value + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt1.Rows.Count > 0 Then
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt1, "crptWeighmetBulkSale", "Tanker Weighment Slip")
        End If
    End Sub
End Class

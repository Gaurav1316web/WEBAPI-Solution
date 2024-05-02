'--------Created By Richa 28/07/2014 Against Ticket No BM00000003158
'' Updation By Richa Agarwal Against Ticket No. BM00000003776 on 10/09/2014
'' Updation By Richa Agarwal Against Ticket No. BM00000004012 on 15/09/2014
'' Updation By Richa Agarwal Against Ticket No. BM00000004767 on 25/11/2014,BM00000006672 on 21/05/2015

Imports common
Imports System.Data.SqlClient
Public Class FrmLoadingTanker
    Inherits FrmMainTranScreen

#Region "Variables"
    Public isInsideloaddata = False
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Public strDocCode As String = ""
    Dim ApplyMultiChamber As Boolean = False

    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colloadingStatus As String = "colloadingStatus"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colISelect As String = "colISelect"
    Public Const colloadingSeqNo As String = "colWeighmentSeqNo"

#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub fndWeighmentEntryNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWeighmentEntryNo._MYValidating
        'Qry = "SELECT Weighment_No AS Code  FROM TSPL_WEIGHMENT_DETAIL_BULKSALE "
        'fndWeighmentEntryNo.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Posted=0 and Weighment_No not in (Select Weighment_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No<> '" + fndLoadingcode.Value + "' ) ", fndWeighmentEntryNo.Value, "", isButtonClicked)
        'fndWeighmentEntryNo.Value = ClsWeighmentEntry.getFinder("Posted=0 and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") and  Weighment_No not in (Select Weighment_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No<> '" + fndLoadingcode.Value + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ")  )", fndWeighmentEntryNo.Value, isButtonClicked)
        'Qry = "SELECT Tanker_No AS Code  FROM TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + fndWeighmentEntryNo.Value + "' "
        'lblTankerNoValue.Text = clsDBFuncationality.getSingleValue(Qry)
        '' LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_TANKER_MASTER where Tanker_No  ='" + lblTankerNoValue.Text + "' ")
        'LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Tanker_Name from TSPL_TANKER_MASTER where Tanker_No  ='" + lblTankerNoValue.Text + "' ")
        'Qry = "SELECT Location_Code AS Code  FROM TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + fndWeighmentEntryNo.Value + "' "
        'lblLocationCode.Text = clsDBFuncationality.getSingleValue(Qry)
        'LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")

    End Sub

    Private Sub FrmLoadingTanker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ApplyMultiChamber = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyMultiChamberInBulkWeighmentEntry, clsFixedParameterCode.ApplyMultiChamberInBulkWeighmentEntry, Nothing)) = 1, True, False))
        If ApplyMultiChamber Then
            SplitContainer2.Panel2Collapsed = False
        Else
            SplitContainer2.Panel2Collapsed = True
        End If
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmLoadingTanker)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
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
    Sub Reset()
        If ApplyMultiChamber Then
            loadBlankItemGrid()
        End If
        fndLoadingcode.Value = ""
        fndWeighmentEntryNo.Value = ""
        LblWeighmentCode.Text = ""
        'lblTankerNoValue.Text = ""
        'LblTankerName.Text = ""
        FndTankerNo.Value = ""
        lblLocationCode.Text = ""
        LblLocationName.Text = ""
        txtQuantity.Value = 0
        FndSiloNo.Value = ""
        LblSiloName.Text = ""
        FndItemCode.Value = ""
        LblItemName.Text = ""
        LblAvailableQty.Text = "0"
        UsLock1.Status = ERPTransactionStatus.Pending
        txtLoadingdate.Value = clsCommon.GETSERVERDATE()
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtLoadingdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtLoadingdate.CustomFormat = "dd/MM/yyyy"
        End If
        fndLoadingcode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        isNewEntry = True
        LOCATIONRIGTHS()
        ''richa agarwal 11/10/2014
        FndItemCode.Value = clsFixedParameter.GetData(clsFixedParameterType.BSDefaultMilkItem, clsFixedParameterCode.BSDefaultMilkItem, Nothing)
        If clsCommon.myLen(FndItemCode.Value) > 0 Then
            LblItemName.Text = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & FndItemCode.Value & "'")
        End If
        ''=====================
    End Sub
    Sub loadBlankItemGrid()
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
        repoISelect.IsVisible = True

        gvItem.Columns.Add(colItemCode, "Item Code")
        gvItem.Columns(colItemCode).Width = 100
        gvItem.Columns(colItemCode).ReadOnly = True

        gvItem.Columns.Add(colItemDesc, "Item Desc")
        gvItem.Columns(colItemDesc).Width = 320
        gvItem.Columns(colItemDesc).ReadOnly = True

        gvItem.Columns.Add(colQty, "Qty")
        gvItem.Columns(colQty).Width = 120
        gvItem.Columns(colQty).ReadOnly = True
        gvItem.Columns(colQty).IsVisible = True

        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 120
        gvItem.Columns(colUOM).ReadOnly = True
        gvItem.Columns(colUOM).IsVisible = True

        gvItem.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItem.Columns(colChamberDesc).Width = 150
        gvItem.Columns(colChamberDesc).ReadOnly = True
        gvItem.Columns(colChamberDesc).IsVisible = True

        gvItem.Columns.Add(colloadingSeqNo, "Sequence No")
        gvItem.Columns(colloadingSeqNo).Width = 120
        gvItem.Columns(colloadingSeqNo).ReadOnly = True
        gvItem.Columns(colloadingSeqNo).IsVisible = True

        Dim repoloadingStatus As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoloadingStatus.FormatString = ""
        repoloadingStatus.HeaderText = "Loading Status"
        repoloadingStatus.Name = colloadingStatus
        repoloadingStatus.Width = 100
        repoloadingStatus.ReadOnly = True
        repoloadingStatus.IsVisible = True
        repoloadingStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoloadingStatus)

        'gvItem.Rows.AddNew()
        'gvItem.Rows(0).Cells(colSlNo).Value = "1"

        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = False
        gvItem.AllowRowReorder = True
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = False
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AllowColumnChooser = True

    End Sub

    Public Sub LoadDataingvitembulk(ByVal weighmentcode As String)
        isInsideloaddata = True
        Try
            gvItem.Rows.Clear()
            Dim qry As String = Nothing
            Dim dt As DataTable = Nothing
            qry = "select * from TSPL_WeighmentBulkSale_Chember_Details where TSPL_WeighmentBulkSale_Chember_Details.Weighment_No='" + weighmentcode + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = gvItem.Rows.Count
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(clsCommon.myCstr(dr("Item_Code")), Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(dr("UOM"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Chamber_Qty"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = clsCommon.myCstr(dr("Chamber_Desc"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colloadingSeqNo).Value = clsCommon.myCdbl(dr("Weighment_Sequence"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).Value = False
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = False
                Next
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
        isInsideloaddata = False
    End Sub

    Private Sub FrmLoadingTanker_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
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
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub SaveData()
        Dim obj As New ClsLoadingTanker()
        Try
            If AllowToSave() Then
                obj.LoadingTanker_No = fndLoadingcode.Value
                obj.LoadingTanker_Date = txtLoadingdate.Value
                obj.Weighment_No = LblWeighmentCode.Text
                obj.Location_Code = lblLocationCode.Text
                'obj.Tanker_No = lblTankerNoValue.Text
                obj.Tanker_No = FndTankerNo.Value
                obj.Silo_No = FndSiloNo.Value
                obj.Item_Code = FndItemCode.Value
                ' obj.Quantity = txtQuantity.Value
                obj.Quantity = 0

                If ApplyMultiChamber Then
                    obj.Arr = New List(Of clsloadingChemberNoDetails)
                    For Each grow As GridViewRowInfo In gvItem.Rows
                        Dim objTr As New clsloadingChemberNoDetails()
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                        objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.loading_Sequence = clsCommon.myCdbl(grow.Cells(colloadingSeqNo).Value)
                        objTr.Loading_Status = IIf(clsCommon.myCBool(grow.Cells(colISelect).Value) = True, 1, 0)
                        If (clsCommon.myLen(objTr.Chamber_Desc) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    Next
                End If

                If (ClsLoadingTanker.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then

                        clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                        LoadData(obj.LoadingTanker_No, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        'If clsCommon.myLen(fndWeighmentEntryNo.Value) <= 0 Then
        '    fndWeighmentEntryNo.Focus()
        '    Throw New Exception("Weighment Entry No cannot be left blank")
        'End If

        ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
        If AllowFutureDateTransaction(txtLoadingdate.Value, Nothing) = False Then
            txtLoadingdate.Focus()
            txtLoadingdate.Select()
            Return False
        End If

        If clsCommon.myLen(FndTankerNo.Value) <= 0 Then
            FndTankerNo.Focus()
            Throw New Exception("Tanker No cannot be left blank")
        End If
        'If clsCommon.CompairString(clsCommon.myCstr(LblLocationName.Text).ToUpper(), "PLANT") = CompairStringResult.Equal Then
        '    If clsCommon.myCdbl(FndSiloNo.Value) <= 0 Then
        '        FndSiloNo.Focus()
        '        Throw New Exception("Silo No cannot be left blank")
        '    End If
        'End If
        If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, Weighment_Date,103) from TSPL_WEIGHMENT_DETAIL_BULKSALE  where Weighment_No ='" + LblWeighmentCode.Text + "'")) > clsCommon.myCDate(txtLoadingdate.Value) Then
            txtLoadingdate.Focus()
            Throw New Exception("Loading Date cannot be less than from Weighment Entry No Date")
        End If
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Isnull(Type,'') FROM tspl_location_master where Location_Code='" + lblLocationCode.Text + "'")).ToUpper(), "PLANT") = CompairStringResult.Equal Then
            If clsCommon.myLen(FndSiloNo.Value) <= 0 Then
                FndSiloNo.Focus()
                Throw New Exception("Silo No cannot be left blank")
            End If
        End If

        If clsCommon.myLen(FndItemCode.Value) <= 0 Then
            FndItemCode.Focus()
            Throw New Exception("Item Code cannot be left blank")
        End If

        'If clsCommon.myCdbl(txtQuantity.Value) = 0 Then
        '    txtQuantity.Focus()
        '    Throw New Exception("Quantity cannot be 0")
        'End If

        'If clsCommon.myCdbl(txtQuantity.Value) > clsCommon.myCdbl(LblAvailableQty.Text) Then
        '    txtQuantity.Focus()
        '    Throw New Exception("Quantity cannot be greater than Available Qty")
        'End If
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsLoadingTanker = ClsLoadingTanker.GetData(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndLoadingcode.Value = obj.LoadingTanker_No
            txtLoadingdate.Value = obj.LoadingTanker_Date
            LblWeighmentCode.Text = obj.Weighment_No
            'lblTankerNoValue.Text = obj.Tanker_No
            FndTankerNo.Value = obj.Tanker_No
            ' LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Tanker_Name from TSPL_TANKER_MASTER where Tanker_No  ='" + lblTankerNoValue.Text + "' ")
            lblLocationCode.Text = obj.Location_Code
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
            FndItemCode.Value = obj.Item_Code
            'LblItemName.Text = clsDBFuncationality.getSingleValue("Select item_desc from TSPL_VENDOR_ITEM_DETAIL where item_no ='" + FndItemCode.Value + "' ")
            LblItemName.Text = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + FndItemCode.Value + "' ")
            FndSiloNo.Value = obj.Silo_No
            LblSiloName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code ='" + FndSiloNo.Value + "' ")
            txtQuantity.Value = obj.Quantity
            If ApplyMultiChamber Then
                gvItem.Rows.Clear()
                For Each objTr As clsloadingChemberNoDetails In obj.Arr
                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colloadingSeqNo).Value = objTr.loading_Sequence
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).Value = IIf(objTr.Loading_Status = 1, True, False)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colloadingStatus).Value = objTr.Loading_Status
                    If obj.Posted = 0 Then
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = False
                    Else
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).ReadOnly = True
                    End If
                Next
            End If
            
            fndLoadingcode.MyReadOnly = True
            btnsave.Text = "Update"
            ' btndelete.Enabled = True
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
            ''richa Against Ticket No. BM00000003768 on 04/09/2014
            'GetAvailableQty()
        Else
            Reset()

        End If
        obj = Nothing
    End Sub
    Private Sub DeleteData()
        Dim arr As List(Of String) = New List(Of String)
        Try
            If (deleteConfirm()) Then
                arr.Add(fndLoadingcode.Value)
                If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.FrmLoadingTanker, Nothing) Then
                    If (ClsLoadingTanker.DeleteData(fndLoadingcode.Value)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                        Reset()
                    End If
                End If

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

    Private Sub fndLoadingcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndLoadingcode._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No='" + fndLoadingcode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If check > 0 Then
                fndLoadingcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndLoadingcode.MyReadOnly = False
            End If

            LoadData(fndLoadingcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndLoadingcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLoadingcode._MYValidating
        'Dim qry As String = "Select LoadingTanker_No as Code,LoadingTanker_Date from TSPL_LOADING_TANKER_DETAIL_BULKSALE "
        ' Dim qry As String = "Select TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No as Code,Convert(varchar,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date,103)  as Date,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No as [Weighment No],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No as [Tanker no],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc],case when TSPL_LOADING_TANKER_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_LOADING_TANKER_DETAIL_BULKSALE  Left Outer Join TSPL_TANKER_MASTER  on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No  =SubLocationMaster.Location_Code"
        Dim qry As String = "Select TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No as Code,Convert(varchar,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date,103)  as Date,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No as [Weighment No],TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No as [Gate Entry No],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Tanker_No as [Tanker no],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No as [Silo No],SubLocationMaster.Location_Desc as [Silo Desc],case when TSPL_LOADING_TANKER_DETAIL_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_LOADING_TANKER_DETAIL_BULKSALE Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocationMaster on TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No  =SubLocationMaster.Location_Code Left Outer Join TSPL_WEIGHMENT_DETAIL_BULKSALE on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_LOADING_TANKER_DETAIL_BULKSALE.Weighment_No "
        fndLoadingcode.Value = clsCommon.ShowSelectForm("LoadingTanker", qry, "Code", " TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") ", fndLoadingcode.Value, "", isButtonClicked)
        LoadData(fndLoadingcode.Value, NavigatorType.Current)
        qry = Nothing
        
    End Sub

    Private Sub FndSiloNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndSiloNo._MYValidating
        'Qry = "Select Sub_Location_Code As Code,Description from TSPL_Sub_Location_Master "
        'FndSiloNo.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Location_Code='" + lblLocationCode.Text + "'", FndSiloNo.Value, "", isButtonClicked)
        'LblSiloName.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_Sub_Location_Master where Sub_Location_Code ='" + FndSiloNo.Value + "' ")
        FndSiloNo.Value = clsLocation.getFinder("is_sub_location='Y' and Loc_Segment_Code in (Select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + lblLocationCode.Text + "')", FndSiloNo.Value, isButtonClicked)
        If clsCommon.myLen(FndSiloNo.Value) > 0 Then
            LblSiloName.Text = clsLocation.GetName(FndSiloNo.Value, Nothing)
        Else
            LblSiloName.Text = ""
        End If

    End Sub

    Private Sub FndItemCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndItemCode._MYValidating
        'Qry = "Select item_no As Code,item_desc as Description from TSPL_VENDOR_ITEM_DETAIL  "
        'FndItemCode.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " item_no in (Select Item_Code from TSPL_ITEM_MASTER where Product_Type ='MI' and Active=1) and  Location_Code in (Select Location_Code From TSPL_Sub_Location_Master where Sub_Location_Code='" + FndSiloNo.Value + "')", FndItemCode.Value, "", isButtonClicked)
        ''richa ERO/08/05/19-000597 show item code from sale order if sale order is created
        If clsCommon.myLen(FndTankerNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Tanker first")
            FndTankerNo.Focus()
            Exit Sub
        End If
        Dim stritemcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code  from TSPL_SALES_ORDER_DETAIL_BULKSALE where Document_No =(select Bulk_SO_No  from TSPL_GATEENTRY_SALE where document_no =  (select GateEntry_Document_No  from TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No ='" & clsCommon.myCstr(LblWeighmentCode.Text) & "'))"))
        Dim strwhrcls As String = " Product_Type ='MI' and Active=1 "
        If clsCommon.myLen(stritemcode) > 0 Then
            strwhrcls += " and Item_code='" & stritemcode & "' "
        End If
        Qry = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER "
        FndItemCode.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", strwhrcls, FndItemCode.Value, "", isButtonClicked)
        LblItemName.Text = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + FndItemCode.Value + "' ")
        'GetAvailableQty()
        Qry = Nothing
    End Sub
    Private Sub GetAvailableQty()
        Dim strICode As String = FndItemCode.Value
        Dim strLocation As String = lblLocationCode.Text
        Dim strSubLocation As String = clsCommon.myCstr(FndSiloNo.Value)
        LblAvailableQty.Text = ClsLoadingTanker.getBalance(strICode, strLocation, strSubLocation, fndLoadingcode.Value, txtLoadingdate.Value, Nothing, "KG")
        strICode = Nothing
        strLocation = Nothing
        strSubLocation = Nothing
    End Sub


    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Dim msg As String = ""
        Dim qry As String = ""
        Dim dt As DataTable = Nothing
        Try
            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (ClsLoadingTanker.PostData(MyBase.Form_ID, arrLoc, fndLoadingcode.Value)) Then
                    msg = "Successfully posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(fndLoadingcode.Value, NavigatorType.Current)
                End If
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
            msg = Nothing
            qry = Nothing
            dt = Nothing
        End Try
    End Sub


    Private Sub FndTankerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndTankerNo._MYValidating
        LblWeighmentCode.Text = ClsWeighmentEntry.getTankerFinder("Posted=0 and TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ") and  Weighment_No not in (Select Weighment_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No<> '" + fndLoadingcode.Value + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code in (" + arrLoc + ")  )", FndTankerNo.Value, isButtonClicked)
        Qry = "SELECT Tanker_No AS Code  FROM TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + LblWeighmentCode.Text + "' "
        ' lblTankerNoValue.Text = clsDBFuncationality.getSingleValue(Qry)
        FndTankerNo.Value = clsDBFuncationality.getSingleValue(Qry)
        Qry = "SELECT Location_Code AS Code  FROM TSPL_WEIGHMENT_DETAIL_BULKSALE where Weighment_No='" + LblWeighmentCode.Text + "' "
        lblLocationCode.Text = clsDBFuncationality.getSingleValue(Qry)
        LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
        If ApplyMultiChamber Then
            LoadDataingvitembulk(LblWeighmentCode.Text)
        End If
    End Sub

    Private Sub gvItem_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvItem.CellDoubleClick
        Dim intCount As Integer = 0
        Dim loadingSeq As Integer = 0
        Dim dblGrossWt As Integer = 0
        Dim WeighmentSeq As Integer = 0
        If e.Column Is gvItem.Columns(colISelect) Then

            gvItem.CurrentRow.Cells(colISelect).Value = True

            dblGrossWt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Gross_Weight from TSPL_WeighmentBulkSale_Chember_Details where Weighment_No='" & LblWeighmentCode.Text & "' and line_No='" & gvItem.CurrentRow.Index + 1 & "'"))
            If dblGrossWt > 0 Then
                clsCommon.MyMessageBoxShow("You cannot change the status. Weighment has been done for this chamber .")
                Exit Sub
            Else
                WeighmentSeq = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 Line_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE left outer join TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_LOADING_TANKER_CHAMBER_DETAIL_BULKSALE.loading_No where Weighment_No='" & LblWeighmentCode.Text & "' and Loading_Status=1 order by loading_Sequence desc"))
                If WeighmentSeq > 0 Then
                    dblGrossWt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Gross_Weight from TSPL_WeighmentBulkSale_Chember_Details where Weighment_No='" & LblWeighmentCode.Text & "' and line_No='" & WeighmentSeq & "'"))
                    If dblGrossWt = 0 Then
                        clsCommon.MyMessageBoxShow("Please enter Tare weight for Chamber No " & clsCommon.myCstr(WeighmentSeq))
                        gvItem.CurrentRow.Cells(colISelect).Value = False
                        Exit Sub
                    End If
                End If
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    If gvItem.Rows(ii).Index <> e.RowIndex Then
                        If clsCommon.myCBool(gvItem.Rows(ii).Cells(colloadingStatus).Value) = True Then
                            intCount += 0
                        End If
                    End If
                    If clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True Then
                        loadingSeq += 1
                    End If
                Next
                If intCount = 0 Then
                    If clsCommon.MyMessageBoxShow("Do you want to load this chamber ?, Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        gvItem.CurrentRow.Cells(colloadingStatus).Value = True
                        gvItem.CurrentRow.Cells(colISelect).Value = True
                        gvItem.CurrentRow.Cells(colloadingSeqNo).Value = loadingSeq
                    Else
                        gvItem.CurrentRow.Cells(colloadingStatus).Value = False
                        gvItem.CurrentRow.Cells(colISelect).Value = False
                        gvItem.CurrentRow.Cells(colloadingSeqNo).Value = 0
                    End If
                Else
                    gvItem.CurrentRow.Cells(colISelect).Value = False
                End If
            End If
            
        End If
    End Sub
End Class

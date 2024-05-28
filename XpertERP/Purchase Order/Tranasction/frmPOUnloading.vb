Imports common
''By balwinder againt ticket no BM00000010083 on 10/11/2016
Public Class frmPOUnloading
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
    Private isEnterManualWeight As Boolean = False
    Const colTRNo As String = "colTRNo"
    Const colSNo As String = "colSNo"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colUOM As String = "colUOM"
    Const colGrossWeight As String = "colGrossWeight"
    Const colTareWeight As String = "colTareWeight"
    Const colNetWeight As String = "colNetWeight"
    Const colUnloadSNo As String = "colUnloadSNo"
    Const colIsUnloadItem As String = "colIsUnloadItem"
    Const colUnloadBy As String = "colUnloadBy"
    Const colUnloadDate As String = "colUnloadDate"
    Const colWeightBy As String = "colWeightBy"
    Const colWeightDate As String = "colWeightDate"
#End Region

    Private Sub frmMilkGateEntryIn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.POUnloading)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoTxt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "TR No"
        repoTxt.Width = 70
        repoTxt.Name = colTRNo
        repoTxt.ReadOnly = True
        repoTxt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTxt)

        Dim repoNum As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "SNo"
        repoNum.Name = colSNo
        repoNum.Width = 50
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)


        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "Item Code"
        repoTxt.Width = 100
        repoTxt.Name = colICode
        repoTxt.ReadOnly = True
        repoTxt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "Item"
        repoTxt.Width = 200
        repoTxt.Name = colIName
        repoTxt.ReadOnly = True
        repoTxt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "UOM"
        repoTxt.Width = 100
        repoTxt.Name = colUOM
        repoTxt.ReadOnly = True
        repoTxt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTxt)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n3}"
        repoNum.HeaderText = "Gross Weight"
        repoNum.Name = colGrossWeight
        repoNum.Width = 100
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n3}"
        repoNum.HeaderText = "Tare Weight"
        repoNum.Name = colTareWeight
        repoNum.Width = 100
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n3}"
        repoNum.HeaderText = "Net Weight"
        repoNum.Name = colNetWeight
        repoNum.Width = 100
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Unloading SNo"
        repoNum.Name = colUnloadSNo
        repoNum.Width = 70
        repoNum.ReadOnly = True
        repoNum.IsVisible = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)

        Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Unload"
        repoCheckBox.Name = colIsUnloadItem
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = True
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoCheckBox)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "Unload By"
        repoTxt.Width = 100
        repoTxt.Name = colUnloadBy
        repoTxt.ReadOnly = True
        repoTxt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTxt)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Expiry Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colUnloadDate
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDate)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = "Weight By"
        repoTxt.Width = 100
        repoTxt.Name = colWeightBy
        repoTxt.ReadOnly = True
        repoTxt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTxt)

        repoDate = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Expiry Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colWeightDate
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDate)

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
    End Sub

    Private Sub frmMilkGateEntryIn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Sub AddNew()
        Try
            isNewEntry = True
            BlankAllControls()
            LoadBlankGrid()
            BlankGRNAllControls()
            txtGateEntryNo.Enabled = True
            txtGrossWeight.Enabled = True
            txtDate.Enabled = True
            txtGrossWeight.ReadOnly = Not isEnterManualWeight
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtGateEntryNo.Value = ""
        txtGrossWeight.Text = ""
        UsGrossWeight.Status = ERPTransactionStatus.Pending
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtGateEntryNo.Value) <= 0 Then
            txtGateEntryNo.Focus()
            errorControl.SetError(txtGateEntryNo, "Please select GRN No")
            Throw New Exception("Please select GRN No")
        End If
        If txtGrossWeight.Value <= 0 Then
            txtGrossWeight.Focus()
            errorControl.SetError(txtGrossWeight, "Please enter Gross Weight")
            Throw New Exception("Please enter Gross Weight")
        End If
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As clsPOWeighment = clsPOWeighment.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Weighment_Code) > 0) Then
                isNewEntry = False
                txtGateEntryNo.Enabled = False
                txtGrossWeight.Enabled = False
                txtDate.Enabled = False
                If obj.Status = ERPTransactionStatus.Approved Then
                  
                End If
                UsGrossWeight.Status = obj.Status
                txtCode.Value = obj.Weighment_Code
                txtDate.Value = obj.Weighment_Date
                txtGateEntryNo.Value = obj.Against_GRN_No
                LoadGateEntryData(obj.Against_GRN_No, False)
                If obj.Gross_Weight = 0 Then
                    txtGrossWeight.Text = ""
                Else
                    txtGrossWeight.Value = obj.Gross_Weight
                End If
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Try
                        isInsideLoadData = True
                        For Each objtr As clsPOWeighmentDetail In obj.Arr
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = objtr.SNo
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Item_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Item_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objtr.UOM
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTRNo).Value = objtr.TR_No
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colGrossWeight).Value = objtr.Gross_Weight
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTareWeight).Value = objtr.Tare_Weight
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value = objtr.Net_Weight
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnloadSNo).Value = objtr.Unload_SNo
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsUnloadItem).Value = objtr.Is_Unload_Item
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnloadBy).Value = objtr.Unload_By
                            If objtr.Unload_Date IsNot Nothing Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnloadDate).Value = objtr.Unload_Date
                            End If
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colWeightBy).Value = objtr.Weight_By
                            If objtr.Weight_Date IsNot Nothing Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colWeightDate).Value = objtr.Weight_Date
                            End If
                        Next
                    Catch ex As Exception
                        isInsideLoadData = False
                        Throw New Exception(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date as WeightDate,TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No as GRNNo,TSPL_PO_WEIGHTMENT_HEAD.Gross_Weight as GrossWeight,case when TSPL_PO_WEIGHTMENT_HEAD.Status=1 then 'Posted' else 'Pending' end as Status from TSPL_PO_WEIGHTMENT_HEAD " + _
      " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No "
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = "   (TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") or TSPL_GRN_HEAD.Ship_To_Location in (" + objCommonVar.strCurrUserLocations + ")) "
        End If
        LoadData(clsCommon.ShowSelectForm("POWFMain", qry, "Weighment_Code", whrClas, txtCode.Value, "TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date desc", isButtonClicked, "Weighment_Date"), NavigatorType.Current)
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where Weighment_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtGateEntryNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGateEntryNo._MYValidating
        Try
            Dim qry As String = "select * from (  select TSPL_GRN_HEAD.GRN_No,max(TSPL_GRN_HEAD.GRN_Date) as GRN_Date,max(TSPL_GRN_HEAD.Vendor_Code) as Vendor_Code,max(TSPL_GRN_HEAD.Vendor_Name) as Vendor_Name,max(TSPL_GRN_HEAD.Bill_To_Location) as Bill_To_Location,max(TSPL_GRN_HEAD.Ship_To_Location) as Ship_To_Location" + Environment.NewLine + _
            " from TSPL_GRN_DETAIL " + Environment.NewLine + _
            " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No " + Environment.NewLine + _
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code " + Environment.NewLine + _
            " where TSPL_GRN_HEAD.Status = 1 And TSPL_ITEM_MASTER.Is_Auto_Weighment = 1" + Environment.NewLine + _
            " group by TSPL_GRN_HEAD.GRN_No  ) xxx  "
            Dim whrClas As String = " not exists(select 1 from TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= xxx.GRN_No and TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code not in ('" + txtCode.Value + "')) "
            LoadGateEntryData(clsCommon.ShowSelectForm("POWFGRN", qry, "GRN_No", whrClas, txtGateEntryNo.Value, "", isButtonClicked), True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadGateEntryData(ByVal strCode As String, ByVal FillDetailData As Boolean)
        Try
            BlankGRNAllControls()
            If clsCommon.myLen(strCode) > 0 Then
                Dim obj As clsGRNHead = clsGRNHead.GetData(strCode, NavigatorType.Current, Nothing)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GRN_No) > 0) Then
                    txtGateEntryNo.Value = obj.GRN_No
                    lblGDGRNDate.Text = clsCommon.GetPrintDate(obj.GRN_Date, "dd/MM/yyyy")
                    lblGDVendorCode.Text = obj.Vendor_Code
                    lblGDVendorName.Text = obj.Vendor_Name
                    lblGDVehicleNo.Text = obj.VehicleNo
                    lblGDCarrier.Text = obj.Carrier
                    lblGDBillToLocation.Text = obj.Bill_To_Location
                    lblGDBillToLocationName.Text = obj.BillToLocationName
                    lblGDShipToLocation.Text = obj.Ship_To_Location
                    lblGDShipToLocationName.Text = obj.ShipToLocationName
                    If FillDetailData Then
                        Try
                            isInsideLoadData = True
                            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                                LoadBlankGrid()
                                For Each objtr As clsGRNDetail In obj.Arr
                                    gv1.Rows.AddNew()
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = objtr.Line_No
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Item_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Item_Desc
                                Next
                            End If
                        Catch ex As Exception
                            isInsideLoadData = False
                            Throw New Exception(ex.Message)
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub BlankGRNAllControls()
        lblGDGRNDate.Text = ""
        lblGDVendorCode.Text = ""
        lblGDVendorName.Text = ""
        lblGDVehicleNo.Text = ""
        lblGDCarrier.Text = ""
        lblGDBillToLocation.Text = ""
        lblGDBillToLocationName.Text = ""
        lblGDShipToLocation.Text = ""
        lblGDShipToLocationName.Text = ""
    End Sub

    Sub SaveOneItem()
        Try
            If MyBase.isModifyFlag Then
                If clsCommon.myCdbl(gv1.CurrentRow.Cells(colUnloadSNo).Value) = 0 Then
                    Dim objtr As New clsPOWeighmentDetail()
                    objtr.Item_Code = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    objtr.Item_Name = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    objtr.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value)
                    If clsCommon.MyMessageBoxShow("Unloading Item: " + objtr.Item_Code + "(" + objtr.Item_Name + ")" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        clsPOWeighmentDetail.SaveUnloadItem(txtCode.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colTRNo).Value), objtr)
                        LoadData(txtCode.Value, NavigatorType.Current)
                    Else
                        gv1.CurrentRow.Cells(colTareWeight).Value = 0
                    End If
                Else
                    Throw New Exception("Item already unloaded")
                End If
            Else
                Throw New Exception("Not permit to save data")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            SaveOneItem()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnhistory_Click(sender As Object, e As EventArgs) Handles btnhistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtCode.Value, "Document_No", "TSPL_PO_WEIGHTMENT_HEAD", "TSPL_PO_WEIGHTMENT_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
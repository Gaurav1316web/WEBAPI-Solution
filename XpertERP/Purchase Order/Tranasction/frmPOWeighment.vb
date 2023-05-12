Imports common
''By balwinder againt ticket no BM00000010083 on 10/11/2016
Public Class frmPOWeighment
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
    Private isEnterManualWeight As Boolean = False
    Private AllowtoEnterNetWeightManuallyinPOWeighmentScreen As Boolean = False
    Const colTRNo As String = "colTRNo"
    Const colSNo As String = "colSNo"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colUOM As String = "colUOM"
    Const colGrossWeight As String = "colGrossWeight"
    Const colTareWeight As String = "colTareWeight"
    Const colExtraWeight As String = "colExtraWeight"
    Const colNetWeight As String = "colNetWeight"
    Const colUnloadSNo As String = "colUnloadSNo"
    Const colIsUnloadItem As String = "colIsUnloadItem"
    Const colUnloadBy As String = "colUnloadBy"
    Const colUnloadDate As String = "colUnloadDate"
    Const colWeightBy As String = "colWeightBy"
    Const colWeightDate As String = "colWeightDate"
    Const colGRNQty As String = "colGRNQty"
    Const colIsAutoWeighment As String = "colIsAutoWeighment"
#End Region

    Private Sub frmMilkGateEntryIn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(BtnPost, "Press Alt+P Post Trasnaction")

            UcWeighing1.form_ID = MyBase.Form_ID
            UcWeighing1.LoadPortAndMachine()
            UcWeighing1.LoadSettingAndStart()

            isEnterManualWeight = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.POWeighmentManual, clsFixedParameterCode.POWeighmentManual, Nothing)) = 1
            AllowtoEnterNetWeightManuallyinPOWeighmentScreen = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoEnterNetWeightManuallyinPOWeighmentScreen, clsFixedParameterCode.AllowtoEnterNetWeightManuallyinPOWeighmentScreen, Nothing)) = 1
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.POWeighment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        BtnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
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
        repoNum.HeaderText = "GRN Qty"
        repoNum.Name = colGRNQty
        repoNum.Width = 100
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)

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
        repoNum.DecimalPlaces = 3
        repoNum.HeaderText = "Tare Weight"
        repoNum.Name = colTareWeight
        repoNum.Width = 100
        repoNum.ReadOnly = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n3}"
        repoNum.HeaderText = "Extra Weight"
        repoNum.Name = colExtraWeight
        repoNum.Width = 100
        repoNum.ReadOnly = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n3}"
        repoNum.HeaderText = "Net Weight"
        repoNum.Name = colNetWeight
        repoNum.Width = 100
        repoNum.ReadOnly = False
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

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Unload"
        repoCheckBox.Name = colIsAutoWeighment
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoCheckBox)

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
        Try
            If e.KeyCode = Keys.F2 Then
                Try
                    Dim dclReading As Decimal = UcWeighing1.LiveReading
                    If dclReading > 0 Then
                        If isNewEntry Then
                            txtGrossWeight.Value = dclReading
                            IsAutoWeighment.Checked = True
                        Else
                            Dim isfound As Boolean = False
                            For ii As Integer = 0 To gv1.Rows.Count - 1
                                If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsUnloadItem).Value) Then
                                    Dim convFat As Decimal = clsWeightConversionInfo.GetWeightConverionFactor(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), "KG", clsCommon.myCstr(gv1.Rows(ii).Cells(colUOM).Value), Nothing)
                                    isInsideLoadData = True
                                    gv1.Rows(ii).Cells(colTareWeight).Value = Math.Round(dclReading * convFat, 3, MidpointRounding.AwayFromZero)
                                    If Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colGrossWeight).Value) - (clsCommon.myCdbl(gv1.Rows(ii).Cells(colTareWeight).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colExtraWeight).Value)), 3, MidpointRounding.ToEven) < 0 Then
                                        Throw New Exception("Sum of Tare Weight and Extra Weight cannot be less than Gross Weight")
                                    End If
                                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colGrossWeight).Value) < Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colGrossWeight).Value) - (clsCommon.myCdbl(gv1.Rows(ii).Cells(colTareWeight).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colExtraWeight).Value)), 3, MidpointRounding.ToEven) Then
                                        Throw New Exception("Sum of Tare Weight and Extra Weight cannot be greater than Gross Weight")
                                    End If

                                    gv1.Rows(ii).Cells(colIsAutoWeighment).Value = True
                                    isInsideLoadData = False
                                    gv1.CurrentRow = gv1.Rows(ii)
                                    SaveOneItem()
                                    isfound = True
                                    Exit For
                                End If
                            Next
                            If Not isfound Then
                                clsCommon.MyMessageBoxShow("No Unloaded item found to save", Me.Text)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
                End Try
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
                AddNew()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
                SaveData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso BtnPost.Enabled Then
                PostData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                DeleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
                CloseForm()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                        "TSPL_PO_WEIGHTMENT_HEAD " + Environment.NewLine +
                                        "TSPL_PO_WEIGHTMENT_DETAIL " + Environment.NewLine +
                                        "Press Alt+P for Post Trasnaction" + Environment.NewLine +
                                        "TSPL_MRN_HEAD (Auto MRN) " + Environment.NewLine +
                                        "TSPL_MRN_DETAIL ")
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    RGBUpdate.Visible = True
                    RGBUpdate.BringToFront()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        
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
            btnSave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            txtGateEntryNo.Enabled = True
            txtGrossWeight.Enabled = True
            txtDate.Enabled = True
            IsAutoWeighment.Checked = False
            txtGrossWeight.ReadOnly = Not isEnterManualWeight
            txtExtraWeight.Value = "0"
            txtNetWeight.Value = "0"
            RGBUpdate.Visible = False
            RGBUpdate.SendToBack()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
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

        'Ticket No- UDL/06/08/18-000211 , Weighment Date is always greater than Equal to GRN Date
        If clsCommon.myCDate(txtDate.Value) < clsCommon.myCDate(txtGRNDate.Value) Then
            txtDate.Focus()
            errorControl.SetError(txtGrossWeight, "Weighment Date is always greater than GRN Date")
            Throw New Exception("Weighment Date is always greater than GRN Date")
        End If

        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsPOWeighment()
                obj.Weighment_Code = txtCode.Value
                obj.Weighment_Date = txtDate.Value
                obj.Against_GRN_No = txtGateEntryNo.Value
                obj.Gross_Weight = txtGrossWeight.Value
                obj.Is_Auto_Weighment = IsAutoWeighment.Checked
                obj.Arr = New List(Of clsPOWeighmentDetail)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim objtr As New clsPOWeighmentDetail
                    objtr.TR_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colTRNo).Value)
                    objtr.SNo = clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNo).Value)
                    objtr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                    objtr.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colUOM).Value)
                    objtr.Gross_Weight = clsCommon.myCdbl(gv1.Rows(ii).Cells(colGrossWeight).Value)
                    objtr.Tare_Weight = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTareWeight).Value)
                    objtr.Extra_Weight = clsCommon.myCdbl(gv1.Rows(ii).Cells(colExtraWeight).Value)
                    objtr.Net_Weight = clsCommon.myCdbl(gv1.Rows(ii).Cells(colNetWeight).Value)
                    objtr.Unload_SNo = clsCommon.myCdbl(gv1.Rows(ii).Cells(colUnloadSNo).Value)
                    objtr.GRN_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colGRNQty).Value)
                    objtr.Is_Unload_Item = clsCommon.myCBool(gv1.Rows(ii).Cells(colIsUnloadItem).Value)
                    objtr.Unload_By = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnloadBy).Value)
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colUnloadDate).Value) > 0 Then
                        objtr.Unload_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colUnloadDate).Value)
                    End If
                    objtr.Weight_By = clsCommon.myCstr(gv1.Rows(ii).Cells(colWeightBy).Value)
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colWeightDate).Value) > 0 Then
                        objtr.Weight_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colWeightDate).Value)
                    End If
                    obj.Arr.Add(objtr)
                Next
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow("Data saved successfully")
                LoadData(obj.Weighment_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
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
                    btndelete.Enabled = False
                    BtnPost.Enabled = False
                    btnSave.Enabled = False
                End If
                UsGrossWeight.Status = obj.Status

                IsAutoWeighment.Checked = obj.Is_Auto_Weighment
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
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objtr.UOM
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Item_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTRNo).Value = objtr.TR_No
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colGrossWeight).Value = objtr.Gross_Weight
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTareWeight).Value = objtr.Tare_Weight
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExtraWeight).Value = objtr.Extra_Weight
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
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNQty).Value = objtr.GRN_Qty
                        Next
                        isInsideLoadData = False
                    Catch ex As Exception
                        isInsideLoadData = False
                        Throw New Exception(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                clsPOWeighment.PostData(txtCode.Value)
                clsCommon.MyMessageBoxShow("Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                clsPOWeighment.DeleteData(txtCode.Value)
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub BtnPost_Click(sender As Object, e As EventArgs) Handles BtnPost.Click
        PostData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date as WeightDate,TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No as GRNNo,TSPL_PO_WEIGHTMENT_HEAD.Gross_Weight as GrossWeight,case when TSPL_PO_WEIGHTMENT_HEAD.Status=1 then 'Posted' else 'Pending' end as Status from TSPL_PO_WEIGHTMENT_HEAD " + _
      " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No "
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = "  (TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") or TSPL_GRN_HEAD.Ship_To_Location in (" + objCommonVar.strCurrUserLocations + ")) "
        End If
        LoadData(clsCommon.ShowSelectForm("POWFMain", qry, "Weighment_Code", whrClas, txtCode.Value, "TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_PO_WEIGHTMENT_HEAD where Weighment_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtGateEntryNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGateEntryNo._MYValidating
        Try
            Dim qry As String = "select * from (  select TSPL_GRN_HEAD.GRN_No,max(TSPL_GRN_HEAD.GRN_Date) as GRN_Date,max(TSPL_GRN_HEAD.Vendor_Code) as Vendor_Code,max(TSPL_GRN_HEAD.Vendor_Name) as Vendor_Name,max(TSPL_GRN_HEAD.Bill_To_Location) as Bill_To_Location,max(TSPL_GRN_HEAD.Ship_To_Location) as Ship_To_Location " + Environment.NewLine +
            " from TSPL_GRN_DETAIL " + Environment.NewLine +
            " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code " + Environment.NewLine +
            " where TSPL_GRN_HEAD.Status = 1 And TSPL_ITEM_MASTER.Is_Auto_Weighment = 1 and  TSPL_GRN_DETAIL.Row_Type='Item' "
            If objCommonVar.RCDFCFP = True Then
                qry += " And TSPL_GRN_HEAD.VisualQCStatus IN (1,3,5) "
            End If
            qry += " group by TSPL_GRN_HEAD.GRN_No  ) xxx  "
            Dim whrClas As String = " not exists(select 1 from TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= xxx.GRN_No and TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code not in ('" + txtCode.Value + "')) "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas += "  and  xxx.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            LoadGateEntryData(clsCommon.ShowSelectForm("POWFGRN", qry, "GRN_No", whrClas, txtGateEntryNo.Value, "", isButtonClicked), True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadGateEntryData(ByVal strCode As String, ByVal FillDetailData As Boolean)
        Try
            BlankGRNAllControls()
            If clsCommon.myLen(strCode) > 0 Then
                Dim obj As clsGRNHead = clsGRNHead.GetData(strCode, NavigatorType.Current, Nothing)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GRN_No) > 0) Then
                    txtGateEntryNo.Value = obj.GRN_No
                    txtGRNDate.Value = obj.GRN_Date
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
                                    If objtr.Row_Type = "Item" Then
                                        gv1.Rows.AddNew()
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = objtr.Line_No
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Item_Code
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Item_Desc
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNQty).Value = objtr.GRN_Qty
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objtr.Unit_code
                                    End If
                                   
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
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub BlankGRNAllControls()
        txtGRNDate.Value = clsCommon.GETSERVERDATE()
        lblGDVendorCode.Text = ""
        lblGDVendorName.Text = ""
        lblGDVehicleNo.Text = ""
        lblGDCarrier.Text = ""
        lblGDBillToLocation.Text = ""
        lblGDBillToLocationName.Text = ""
        lblGDShipToLocation.Text = ""
        lblGDShipToLocationName.Text = ""
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colTareWeight) Then
                    If isEnterManualWeight AndAlso clsCommon.myCBool(gv1.CurrentRow.Cells(colIsUnloadItem).Value) Then
                        gv1.CurrentRow.Cells(colTareWeight).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colTareWeight).ReadOnly = True
                    End If
                End If

                If e.Column Is gv1.Columns(colNetWeight) Then
                    If AllowtoEnterNetWeightManuallyinPOWeighmentScreen AndAlso clsCommon.myCBool(gv1.CurrentRow.Cells(colIsUnloadItem).Value) Then
                        gv1.CurrentRow.Cells(colNetWeight).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colNetWeight).ReadOnly = True
                    End If
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub SaveOneItem()
        Try
            If MyBase.isModifyFlag Then
                Dim objtr As New clsPOWeighmentDetail()
                objtr.Extra_Weight = clsCommon.myCdbl(gv1.CurrentRow.Cells(colExtraWeight).Value)
                If AllowtoEnterNetWeightManuallyinPOWeighmentScreen = True Then
                    objtr.Net_Weight = clsCommon.myCdbl(gv1.CurrentRow.Cells(colNetWeight).Value)
                    objtr.Tare_Weight = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colGrossWeight).Value) - (objtr.Net_Weight - objtr.Extra_Weight), 3, MidpointRounding.ToEven)
                Else
                    objtr.Tare_Weight = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTareWeight).Value)
                    objtr.Net_Weight = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colGrossWeight).Value) - (objtr.Tare_Weight + objtr.Extra_Weight), 3, MidpointRounding.ToEven)
                End If

                'objtr.GRN_Qty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colGRNQty).Value)
                objtr.Is_Auto_Weighment = clsCommon.myCBool(gv1.CurrentRow.Cells(colIsAutoWeighment).Value)
                objtr.Gross_Weight = clsCommon.myCdbl(gv1.CurrentRow.Cells(colGrossWeight).Value)
                Dim arrTRNo As List(Of String) = Nothing
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If ii = gv1.CurrentRow.Index Then
                        Continue For
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)) = CompairStringResult.Equal Then
                        If arrTRNo Is Nothing Then
                            arrTRNo = New List(Of String)
                        End If
                        arrTRNo.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colTRNo).Value))
                    End If
                Next

                If clsCommon.MyMessageBoxShow("Item:" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "[" + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) + "]" + Environment.NewLine + "Tare Weight: " + clsCommon.myCstr(objtr.Tare_Weight) + Environment.NewLine + "Net Weight: " + clsCommon.myCstr(objtr.Net_Weight) + Environment.NewLine + "Save the data", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsPOWeighmentDetail.SaveTareWeightment(txtCode.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colTRNo).Value), objtr, arrTRNo)
                    LoadData(txtCode.Value, NavigatorType.Current)
                Else
                    isCellValueChangedOpen = True ''UDL/14/06/18-000186 by balwinder on 20/06/2018 show message again.
                    gv1.CurrentRow.Cells(colTareWeight).Value = 0
                    isCellValueChangedOpen = False
                End If
            Else
                Throw New Exception("Not permit to save data")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colNetWeight) AndAlso AllowtoEnterNetWeightManuallyinPOWeighmentScreen = True Then
                        SaveOneItem()
                    ElseIf e.Column Is gv1.Columns(colTareWeight) AndAlso AllowtoEnterNetWeightManuallyinPOWeighmentScreen = False Then
                        SaveOneItem()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Public Sub PrintData(ByVal StrCode As String)
        If clsCommon.myLen(StrCode) > 0 Then
            Dim strQuery As String = Nothing
            If objCommonVar.RCDFCFP = True Then
                strQuery = "select FORMAT(getdate(),'dd/MMM/yyyy') as Print_Date,FORMAT(getdate(),'hh:mm:ss tt') as Print_Time
                            ,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code 
                            ,max(TSPL_LOCATION_MASTER.Location_Desc) as Comp_Name 
                            , max(TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end  
                            +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  
                            + case when LEN(TSPL_LOCATION_MASTER.City_Code)>0 then ', '+TSPL_LOCATION_MASTER.City_Code else ' ' end  
                            + case when len(TSPL_STATE_MASTER.STATE_NAME)>0 then ', '+ TSPL_STATE_MASTER.STATE_NAME else ' ' end  
                            + case when len(TSPL_LOCATION_MASTER.Pin_code)>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_code as varchar)  else ' ' end) as Address
                            ,max(TSPL_COMPANY_MASTER.ServiceTax_Reg_No) as ServiceTax_Reg_No,max(TSPL_COMPANY_MASTER.Tin_No) as Tin_No 
                            ,(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then sum(TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight) else max(TSPL_PO_WEIGHTMENT_HEAD.Out_Gross_Weight) end) as Gross_Weight 
                            ,(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then sum(TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight) else max(TSPL_PO_WEIGHTMENT_HEAD.Out_Tare_Weight) end) AS Tare_Weight 
                            ,(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then sum(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) else max(TSPL_PO_WEIGHTMENT_HEAD.Out_Net_Weight) end) as Net_Weight
                             ,max(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then  FORMAT(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,'dd/MMM/yyyy')  else FORMAT(TSPL_PO_WEIGHTMENT_DETAIL.Weight_Date,'dd/MMM/yyyy')  end) as In_Date 
                            ,max(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then FORMAT(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,'hh:mm:ss tt')  else FORMAT(TSPL_PO_WEIGHTMENT_DETAIL.Weight_Date,'hh:mm:ss tt') end) as In_Time 
                            ,max(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then FORMAT(TSPL_PO_WEIGHTMENT_HEAD.Out_Tare_Date,'dd/MMM/yyyy')  else FORMAT(TSPL_PO_WEIGHTMENT_HEAD.Out_Gross_Date,'dd/MMM/yyyy')  end) as Out_Date 
                            ,max(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then FORMAT(TSPL_PO_WEIGHTMENT_HEAD.Out_Tare_Date,'hh:mm:ss tt')  else FORMAT(TSPL_PO_WEIGHTMENT_HEAD.Out_Gross_Date,'hh:mm:ss tt')  end) as Out_Time 
                            ,max(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then TSPL_GRN_HEAD.Vendor_Code else TSPL_PO_WEIGHTMENT_HEAD.Out_Party end) as Vendor_Code 
                            ,max(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then TSPL_GRN_HEAD.Vendor_Name else TSPL_PO_WEIGHTMENT_HEAD.Out_Party end) as Vendor_Name 
                            ,max(case when TSPL_PO_WEIGHTMENT_HEAD.Type='IN' Then TSPL_GRN_HEAD.VehicleNo else TSPL_PO_WEIGHTMENT_HEAD.Out_VehicleNo end) as VehicleNo 
                            ,TSPL_PO_WEIGHTMENT_HEAD.Type,max(TSPL_PO_WEIGHTMENT_HEAD.Driver_Name) as Driver_Name,
							max(TSPL_PO_WEIGHTMENT_HEAD.Driver_MobileNo) as Driver_MobileNo ,
							max(TSPL_GRN_HEAD.Ref_No) as RALNo, max(TSPL_GRN_HEAD.GRN_No) as GRNNO,
							max(convert(date,TSPL_GRN_HEAD.GRN_Date,103)) as GRNDate,
							max(convert(date,TSPL_GRN_HEAD.Invoice_Date,103)) as BillDate,
							max(TSPL_GRN_HEAD.[Invoice/Challan_No]) as BillNo,
							max(convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103)) as WeighDate,
							max(TSPL_PO_WEIGHTMENT_detail.Unload_Date) as OutDateTime,
							max(TSPL_PO_WEIGHTMENT_DETAIL.EXTRA_WEIGHT) AS EXTRA_WEIGHT,
							max(TSPL_GRN_DETAIL.GRN_QTY) as challan_qty,
							max(TSPL_PO_WEIGHTMENT_DETAIL.ITEM_CODE) AS ITEM_CODE,
							MAX(TSPL_ITEM_MASTER.Item_Desc) AS ITEM_NAME,
							MAX(TSPL_PO_WEIGHTMENT_GUNNY.QTY) AS BAG_QTY
                            from TSPL_PO_WEIGHTMENT_HEAD left join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code  
                            left join TSPL_GRN_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No  
                            left join TSPL_COMPANY_MASTER on TSPL_GRN_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  
                            left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GRN_HEAD.Bill_To_Location				
                            LEFT OUTER JOIN TSPL_STATE_MASTER ON TSPL_STATE_MASTER.STATE_CODE  =TSPL_LOCATION_MASTER.State 	
							LEFT OUTER JOIN TSPL_GRN_DETAIL ON TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
							LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_PO_WEIGHTMENT_DETAIL.ITEM_CODE
							LEFT OUTER JOIN TSPL_PO_WEIGHTMENT_GUNNY ON TSPL_PO_WEIGHTMENT_GUNNY.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code
                            where TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code = '" + StrCode + "'  
                            group by TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Type


"
            Else
                strQuery = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,convert(varchar(13),TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as Weighment_Date ,TSPL_PO_WEIGHTMENT_DETAIL.Item_Code,TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight,TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight,TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight,TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No,convert(varchar(13),TSPL_GRN_HEAD.GRN_Date,103) as GRN_Date," &
                 "TSPL_GRN_HEAD.Against_PO,TSPL_GRN_HEAD.Vendor_Code,TSPL_GRN_HEAD.Vendor_Name,TSPL_GRN_HEAD.VehicleNo from TSPL_PO_WEIGHTMENT_HEAD left join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code " &
                 "left join TSPL_GRN_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No " &
                 "left join TSPL_COMPANY_MASTER on TSPL_GRN_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  where TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code='" + StrCode + " '"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptTankerWeighmentSlip", "Tanker Weighment Slip")
                frmCRV = Nothing
            End If
        Else
            clsCommon.MyMessageBoxShow("Please Select Weighmrnt Code first.")
        End If
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            PrintData(txtCode.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdateWeighment_Click(sender As Object, e As EventArgs) Handles btnUpdateWeighment.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                Throw New Exception("Select Weighment No first!")
            End If
            If clsCommon.myCDecimal(txtExtraWeight.Value) <= 0 AndAlso clsCommon.myCDecimal(txtNetWeight.Value) <= 0 Then
                Throw New Exception("Enter weight for update!")
            End If
            Dim qry As String = ""
            If clsCommon.myCDecimal(txtExtraWeight.Value) > 0 Then
                qry = "update TSPL_PO_WEIGHTMENT_DETAIL set Extra_Weight='" + clsCommon.myCstr(txtExtraWeight.Value) + "' where weighment_code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
            If clsCommon.myCDecimal(txtNetWeight.Value) > 0 Then
                qry = "update TSPL_PO_WEIGHTMENT_DETAIL set Net_Weight='" + clsCommon.myCstr(txtNetWeight.Value) + "' where weighment_code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If

            qry = "update TSPL_PO_WEIGHTMENT_DETAIL set Tare_Weight=Gross_Weight-(Net_Weight+Extra_Weight) where weighment_code='" + txtCode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUpdateToSRN_Click(sender As Object, e As EventArgs) Handles btnUpdateToSRN.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                Throw New Exception("Select Weighment No first!")
            End If
            If clsCommon.myCDecimal(txtExtraWeight.Value) <= 0 AndAlso clsCommon.myCDecimal(txtNetWeight.Value) <= 0 Then
                Throw New Exception("Enter weight for update!")
            End If
            If UsGrossWeight.Status <> ERPTransactionStatus.Approved Then
                BtnPost.Focus()
                Throw New Exception("Weighment pending for post.")
            End If

            Dim qry As String = ""
            qry = "SELECT MAX(TSPL_SRN_HEAD.Status) AS TT
                      from tspl_srn_DETAIL
                      left outer join TSPL_PO_WEIGHTMENT_HEAD on tspl_srn_DETAIL.GRN_ID= TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
                      left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                      and TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=tspl_srn_DETAIL.Item_Code
                      LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No=tspl_srn_DETAIL.SRN_No
                      where TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code='" + txtCode.Value + "'
                      and tspl_srn_DETAIL.GRN_ID='" + txtGateEntryNo.Value + "'
                      and TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No='" + txtGateEntryNo.Value + "' "
            Dim IntDocStatus As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If IntDocStatus = 1 Then
                Throw New Exception("Unpost SRN first.")
            End If


            If clsCommon.myCDecimal(txtExtraWeight.Value) > 0 Then
                qry = "update TSPL_PO_WEIGHTMENT_DETAIL set Extra_Weight='" + clsCommon.myCstr(txtExtraWeight.Value) + "' where weighment_code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
            If clsCommon.myCDecimal(txtNetWeight.Value) > 0 Then
                qry = "update TSPL_PO_WEIGHTMENT_DETAIL set Net_Weight='" + clsCommon.myCstr(txtNetWeight.Value) + "' where weighment_code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If

            qry = "update TSPL_PO_WEIGHTMENT_DETAIL set Tare_Weight=Gross_Weight-(Net_Weight+Extra_Weight) where weighment_code='" + txtCode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "UPDATE tspl_srn_DETAIL SET tspl_srn_DETAIL.MRN_Qty=TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight ,tspl_srn_DETAIL.SRN_Qty=TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight
                      from tspl_srn_DETAIL
                      left outer join TSPL_PO_WEIGHTMENT_HEAD on tspl_srn_DETAIL.GRN_ID= TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
                      left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                      and TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=tspl_srn_DETAIL.Item_Code
                      where TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code='" + txtCode.Value + "'
                      and tspl_srn_DETAIL.GRN_ID='" + txtGateEntryNo.Value + "'
                      and TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No='" + txtGateEntryNo.Value + "' "
                clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
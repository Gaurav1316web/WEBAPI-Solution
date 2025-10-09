Imports common

Public Class FrmSalesOrderGatePass

#Region "Variables"
    Private isNewEntry As Boolean = False
    Const ColApply As String = "ColApply"
    Const ColDocNo As String = "ColDocNo"
    Const ColDocDate As String = "ColDocDatet"
    Const ColToSalesmanCode As String = "ColToSalesmanCode"
    Const ColToSalesmanname As String = "ColToSalesmanname"
    Const ColRoute_No As String = "ColRoute_No"
    Const ColRoute_Desc As String = "ColRoute_Desc"
    Const ColType As String = "ColType"
    Const ColPriceCode As String = "ColPriceCode"
    Const ColPriceDesc As String = "ColPriceDesc"
    Const ColCustCode As String = "ColCustCode"
    Const ColCustName As String = "ColCustName"
    Dim isInsideLoadData As Boolean = False
    Dim blnLoad As Boolean = False
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colUnit As String = "colUnit"
    Const colQty As String = "colQty"
    Const colCrateIssue As String = "colCrateIssue"
    Const colLineNo As String = "colLineNo"
    Const colPKID As String = "colPKID"
    Const colHSNCode As String = "colHSNCode"
    Const colSchemeItem As String = "colSchemeItem"
#End Region
    Private Sub SetUserMgmtNew()
        Me.Form_ID = clsUserMgtCode.FrmSalesOrderGatePass
        MyBase.SetUserMgmt(clsUserMgtCode.FrmSalesOrderGatePass)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnGPCancel.Visible = MyBase.isCancel_Flag
        btnReverse.Visible = False
    End Sub
    Private Sub FrmSalesOrderGatePass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        CreateTable()
        AddNew()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub
    Private Sub AddNew()
        isNewEntry = True
        UsLock1.Status = common.ERPTransactionStatus.Pending
        txtDocCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE
        txtDispatchCode.Value = ""
        lblDispatchCode.Text = ""
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        txtSubLocation.Value = ""
        lblSubLocationDesc.Text = ""
        txtTransporterCode.Value = ""
        lblTransporterName.Text = ""
        txtVehicleCode.Value = ""
        lblVehicleNo.Text = ""
        txtLoadingSlip.Text = ""
        txtDriverName.Text = ""
        txtDriverMobNo.Text = ""
        LoadBlankGrid()
    End Sub
    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False
        Dim repoPK_ID As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPK_ID = New GridViewDecimalColumn()
        repoPK_ID.FormatString = ""
        repoPK_ID.HeaderText = "ID"
        repoPK_ID.Name = colPKID
        repoPK_ID.Width = 50
        repoPK_ID.ReadOnly = True
        repoPK_ID.IsVisible = False
        repoPK_ID.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoPK_ID)
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoLineNo)
        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemCode)
        Dim ItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Desc"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 100
        ItemDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemDesc)
        Dim CustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustCode.FormatString = ""
        CustCode.HeaderText = "Cust Code"
        CustCode.Name = ColCustCode
        CustCode.Width = 100
        CustCode.ReadOnly = True
        CustCode.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(CustCode)
        Dim CustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustName.FormatString = ""
        CustName.HeaderText = "Cust Name"
        CustName.Name = ColCustName
        CustName.Width = 100
        CustName.ReadOnly = True
        CustName.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(CustName)
        Dim Unit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Unit.FormatString = ""
        Unit.HeaderText = "Unit"
        Unit.Name = colUnit
        Unit.Width = 100
        Unit.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Unit)
        Dim HSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        HSN.FormatString = ""
        HSN.HeaderText = "HSN Code"
        HSN.Name = colHSNCode
        HSN.Width = 100
        HSN.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(HSN)
        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        If clsCommon.myCstr(txtDocCode.Value) IsNot Nothing AndAlso clsCommon.myLen(txtDocCode.Value) > 0 Then
            repoQty.ReadOnly = True
        Else
            repoQty.ReadOnly = False
        End If
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoQty)

        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
    End Sub
    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary key")
        coll.Add("Document_Date", "datetime not NULL")
        coll.Add("Dispatch_Code", "varchar(30) NOT NULL REFERENCES TSPL_SD_SHIPMENT_HEAD(Document_Code)")
        coll.Add("Location_Code", "Varchar(12) NOt NULL  References TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("Sub_Location", "Varchar(12) NULL  References TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("Transporter_Code", "Varchar(50) Null")
        coll.Add("Vehicle_Code", "Varchar(50) Null")
        coll.Add("Vehicle_No", "Varchar(100) NULL")
        coll.Add("Loading_Slip", "varchar(12) NULL")
        coll.Add("Driver_Name", "varchar(30) NULL")
        coll.Add("Driver_Mob_No", "varchar(12) NULL")
        coll.Add("Remarks", "varchar(200) NULL")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "Datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_TENDER_GATE_PASS", coll, "", True, False, "", "Document_Code", "Document_Date", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "varchar(30) NOT NULL REFERENCES TSPL_CUSTOMER_TENDER_GATE_PASS(Document_Code)")
        coll.Add("Item_Code", "Varchar(50) not null")
        coll.Add("Unit_Code", "Varchar(50) not null")
        coll.Add("HSN_Code", "Varchar(50) not null")
        coll.Add("Qty", "decimal(18, 6) null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_TENDER_GATE_PASS_DETAIL", coll, "", True, False, "TSPL_CUSTOMER_TENDER_GATE_PASS", "Document_Code", "", True)
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmSalesOrderGatePass_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
                btnNew.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
                'SaveData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnSave.Enabled AndAlso MyBase.isDeleteFlag Then
                btnDelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
                btnPost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
                Me.Close()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                If MyBase.isReverse Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = clsFixedParameterType.SIR
                    frm.strCode = clsFixedParameterCode.SIReversAndCreate
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnReverse.Visible = True
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave()
        Try
            If clsCommon.myLen(txtDispatchCode.Value) <= 0 Then
                Throw New Exception("Please select Dispatch Code.")
            End If
            If clsCommon.myLen(txtTransporterCode.Value) <= 0 Then
                Throw New Exception("Please select Transporter")
            End If
            If clsCommon.myLen(txtVehicleCode.Value) <= 0 Then
                Throw New Exception("Please select Vehicle")
            End If
            If clsCommon.myLen(lblVehicleNo.Text) <= 0 Then
                Throw New Exception("Please Enter Vehicle No")
            End If
            If clsCommon.myLen(txtLoadingSlip.Text) <= 0 Then
                Throw New Exception("Please enter Loading Slip")
            End If
            If clsCommon.myLen(txtDriverName.Text) <= 0 Then
                Throw New Exception("Please enter Driver Name")
            End If
            If clsCommon.myLen(txtDriverMobNo.Text) <= 0 Then
                Throw New Exception("Please enter Driver Mobile No.")
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select location")
            End If
            If clsCommon.myLen(txtLocation.Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                Throw New Exception("Please select sub location")
            End If
            For ii As Integer = 0 To Gv1.Rows.Count - 1
                If clsCommon.myCdbl(Gv1.Rows(ii).Cells(colQty).Value) <= 0 AndAlso clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(ii).Cells(colItemCode).Value)) > 0 Then
                    Throw New Exception("Enter Qty at line no -" & clsCommon.myCstr(ii + 1))
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub SaveData()
        'Try
        '    If AllowToSave() Then
        '        Dim obj As New clsCustomerTenderOrder()
        '        obj.Document_Code = txtDocCode.Value
        '        obj.Document_Date = txtDate.Value
        '        obj.RAL_No = txtRALNo.Value
        '        obj.Cust_Code = txtCustomerCode.Value
        '        obj.Location_Code = txtLocation.Value
        '        obj.Sub_Location = txtSubLocation.Value
        '        obj.Ref_No = txtRefNo.Text
        '        obj.Ref_Date = txtRefDate.Value
        '        obj.Document_Amt = 0
        '        obj.Remarks = txtRemark.Text
        '        obj.Tax_Group = txtTaxGroup.Value
        '        obj.TaxGroupName = lblTaxGrpName.Text
        '        If rbtnTaxCalAutomatic.IsChecked Then
        '            obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
        '        ElseIf rbtnTaxCalManual.IsChecked Then
        '            obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
        '        End If
        '        If (gv2.Rows.Count > 0) Then
        '            obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
        '            obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
        '            obj.TAX1_Base_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
        '            obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 1) Then
        '            obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)
        '            obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
        '            obj.TAX2_Base_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTBaseAmt).Value)
        '            obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 2) Then
        '            obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)
        '            obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
        '            obj.TAX3_Base_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTBaseAmt).Value)
        '            obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 3) Then
        '            obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)
        '            obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
        '            obj.TAX4_Base_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTBaseAmt).Value)
        '            obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 4) Then
        '            obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)
        '            obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
        '            obj.TAX5_Base_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTBaseAmt).Value)
        '            obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 5) Then
        '            obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)
        '            obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
        '            obj.TAX6_Base_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTBaseAmt).Value)
        '            obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 6) Then
        '            obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)
        '            obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
        '            obj.TAX7_Base_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTBaseAmt).Value)
        '            obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 7) Then
        '            obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)
        '            obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
        '            obj.TAX8_Base_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTBaseAmt).Value)
        '            obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 8) Then
        '            obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)
        '            obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
        '            obj.TAX9_Base_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTBaseAmt).Value)
        '            obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
        '        End If
        '        If (gv2.Rows.Count > 9) Then
        '            obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)
        '            obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
        '            obj.TAX10_Base_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTBaseAmt).Value)
        '            obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
        '        End If
        '        obj.Doc_Amt_Without_Tax = clsCommon.myCdbl(txtDocAmtWithoutTax.Text)
        '        obj.Tax_Amt = clsCommon.myCdbl(txtTaxAmt.Text)
        '        obj.Document_Amt = clsCommon.myCdbl(txtDocAmt.Text)
        '        obj.Arr = GetTRData()
        '        obj.SaveData(obj, isNewEntry)
        '        UcAttachment1.SaveData(obj.Document_Code)
        '        clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
        '        LoadData(obj.Document_Code, NavigatorType.Current)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
    End Sub
    'Function GetTRData() As List(Of clsCustomerTenderOrderDetail)
    '    Dim Arr As New List(Of clsCustomerTenderOrderDetail)
    '    For ii As Integer = 0 To Gv1.RowCount - 1
    '        If clsCommon.myLen(Gv1.Rows(ii).Cells(colICode).Value) > 0 Then
    '            Dim objTr As New clsCustomerTenderOrderDetail()
    '            objTr.RowType = clsCommon.myCstr(Gv1.Rows(ii).Cells(colRowType).Value)
    '            objTr.Item_Code = clsCommon.myCstr(Gv1.Rows(ii).Cells(colICode).Value)
    '            objTr.Unit_Code = clsCommon.myCstr(Gv1.Rows(ii).Cells(colUOM).Value)
    '            objTr.Tender_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTenderRate).Value)
    '            objTr.Item_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colRate).Value)
    '            objTr.Item_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colItemAmt).Value)
    '            objTr.Qty = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colQty).Value)
    '            objTr.TAX1 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax1).Value)
    '            objTr.TAX1_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax1_BaseAmt).Value)
    '            objTr.TAX1_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax1_Rate).Value)
    '            objTr.TAX1_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax1_Amt).Value)
    '            objTr.Tax2 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax2).Value)
    '            objTr.Tax2_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax2_BaseAmt).Value)
    '            objTr.Tax2_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax2_Rate).Value)
    '            objTr.Tax2_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax2_Amt).Value)
    '            objTr.Tax3 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax3).Value)
    '            objTr.Tax3_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax3_BaseAmt).Value)
    '            objTr.Tax3_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax3_Rate).Value)
    '            objTr.Tax3_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax3_Amt).Value)
    '            objTr.Tax4 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax4).Value)
    '            objTr.Tax4_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax4_BaseAmt).Value)
    '            objTr.Tax4_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax4_Rate).Value)
    '            objTr.Tax4_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax4_Amt).Value)
    '            objTr.Tax5 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax5).Value)
    '            objTr.Tax5_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax5_BaseAmt).Value)
    '            objTr.Tax5_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax5_Rate).Value)
    '            objTr.Tax5_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax5_Amt).Value)
    '            objTr.Tax6 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax6).Value)
    '            objTr.Tax6_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax6_BaseAmt).Value)
    '            objTr.Tax6_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax6_Rate).Value)
    '            objTr.Tax6_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax6_Amt).Value)
    '            objTr.Tax7 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax7).Value)
    '            objTr.Tax7_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax7_BaseAmt).Value)
    '            objTr.Tax7_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax7_Rate).Value)
    '            objTr.Tax7_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax7_Amt).Value)
    '            objTr.TAX1 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax1).Value)
    '            objTr.TAX1_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax1_BaseAmt).Value)
    '            objTr.TAX1_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax1_Rate).Value)
    '            objTr.TAX1_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax1_Amt).Value)
    '            objTr.Tax9 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax9).Value)
    '            objTr.Tax9_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax9_BaseAmt).Value)
    '            objTr.Tax9_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax9_Rate).Value)
    '            objTr.Tax9_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax9_Amt).Value)
    '            objTr.Tax10 = clsCommon.myCstr(Gv1.Rows(ii).Cells(colTax10).Value)
    '            objTr.Tax10_Base_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax10_BaseAmt).Value)
    '            objTr.Tax10_Rate = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax10_Rate).Value)
    '            objTr.Tax10_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTax10_Amt).Value)
    '            objTr.Total_Tax_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTotalTaxAmt).Value)
    '            objTr.Total_Amt = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colTotalAmt).Value)
    '            objTr.Inclusive_Tax = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colInclusiveTax).Value)
    '            objTr.Inclusive_TPT = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colInclusiveTPT).Value)
    '            If clsCommon.myLen(objTr.Item_Code) > 0 Then
    '                Arr.Add(objTr)
    '            End If
    '        End If
    '    Next
    '    Return Arr
    'End Function

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGPCancel_Click(sender As Object, e As EventArgs) Handles btnGPCancel.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtDocCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocCode._MYNavigator
        Try
            Dim strwherecls As String = ""
            Dim strQry As String = ""
            strwherecls = Xtra.CustomerPermission()
            strQry = "select count(*) from TSPL_CUSTOMER_TENDER_GATE_PASS where Document_Code='" & txtDocCode.Value & "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
            If count = 0 Then
                txtDocCode.MyReadOnly = False
            Else
                txtDocCode.MyReadOnly = True
            End If
            LoadData(txtDocCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocCode._MYValidating
        Try
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            Dim qry As String = "select Document_Code as Code,Document_Date,Dispatch_Code,Location_Code,Remarks,case when Status=1 then 'Approved' else 'Pending' end as Status
from TSPL_CUSTOMER_TENDER_GATE_PASS  "
            Dim whrClas As String = ""
            LoadData(clsCommon.ShowSelectForm("CT_GPDocfnd", qry, "Code", whrClas, txtDocCode.Value, "Code", isButtonClicked, "TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Date"), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDispatchCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDispatchCode._MYValidating
        Try
            Dim qry As String = "select Document_Code as Code,Document_Date as [Doc Date],Customer_Code as [Customer Code] from TSPL_SD_SHIPMENT_HEAD "
            Dim Whrcls As String = " Screen_Type='CT' and  status=1"
            txtDispatchCode.Value = clsCommon.ShowSelectForm("CT-GPfnd", qry, "Code", Whrcls, txtDispatchCode.Value, "Code", isButtonClicked)
            lblDispatchCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SD_SHIPMENT_HEAD where Document_Code='" & txtDispatchCode.Value & "'"))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Category not in('MCC')"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("CT-GPLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
            If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                    txtSubLocation.Enabled = True
                Else
                    txtSubLocation.Enabled = False
                End If
                txtSubLocation.Value = ""
                lblSubLocationDesc.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                txtSubLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" & txtLocation.Value & "'", txtSubLocation.Value, isButtonClicked)
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocationDesc.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                Else
                    lblSubLocationDesc.Text = ""
                End If
            Else
                txtLocation.Value = ""
                lblSubLocationDesc.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTransporterCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTransporterCode._MYValidating
        Try
            Dim qry As String = "select Transport_Id,Transporter_Name from TSPL_TRANSPORT_MASTER"
            txtTransporterCode.Value = clsCommon.ShowSelectForm("DSTransport No", qry, "Transport_Id", "", txtTransporterCode.Value, "Transport_Id", isButtonClicked)
            lblTransporterName.Text = connectSql.RunScalar("Select Transporter_Name  from TSPL_TRANSPORT_MASTER where Transport_Id = '" & Convert.ToString(txtTransporterCode.Value) & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVehicleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVehicleCode._MYValidating
        Try
            Dim qry As String = "Select distinct  vehicle_id ,Number as VehicleNo from TSPL_VEHICLE_MASTER"
            txtVehicleCode.Value = clsCommon.ShowSelectForm("Vehicle No", qry, "vehicle_id", "", txtVehicleCode.Value, "vehicle_id", isButtonClicked)
            lblVehicleNo.Text = connectSql.RunScalar("Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id = '" & Convert.ToString(txtVehicleCode.Value) & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
Imports common
Public Class FrmSalesOrderGatePass
#Region "Variables"
    Private isNewEntry As Boolean = False
    Const ColApply As String = "ColApply"
    Const ColCustCode As String = "ColCustCode"
    Const ColCustName As String = "ColCustName"
    Dim isInsideLoadData As Boolean = False
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colUnit As String = "colUnit"
    Const colQty As String = "colQty"
    Const colLineNo As String = "colLineNo"
    Const colHSNCode As String = "colHSNCode"
    Dim OneTimeCheck As Boolean = False
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
        txtRemark.Text = ""
        LoadBlankGrid()
        FieldControl(True)
    End Sub
    Private Sub FieldControl(ByVal flag As Boolean)
        txtDate.Enabled = flag
        txtDispatchCode.Enabled = flag
        lblDispatchCode.Enabled = flag
        txtTransporterCode.Enabled = flag
        lblTransporterName.Enabled = flag
        txtVehicleCode.Enabled = flag
        txtLoadingSlip.Enabled = flag
        txtVehicleCode.Enabled = flag
        txtVehicleCode.Enabled = flag

    End Sub
    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False
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
        repoQty.ReadOnly = True
        'If clsCommon.myCstr(txtDocCode.Value) IsNot Nothing AndAlso clsCommon.myLen(txtDocCode.Value) > 0 Then
        '    repoQty.ReadOnly = True
        'Else
        '    repoQty.ReadOnly = False
        'End If
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
        Try
            If AllowToSave() Then
                Dim obj As New clsCustomerTenderGatePass()
                obj.Document_Code = txtDocCode.Value
                obj.Document_Date = txtDate.Value
                obj.Dispatch_Code = txtDispatchCode.Value
                obj.Transporter_Code = txtTransporterCode.Value
                obj.Location_Code = txtLocation.Value
                obj.Sub_Location = txtSubLocation.Value
                obj.Vehicle_Code = txtVehicleCode.Value
                obj.Vehicle_No = lblVehicleNo.Text
                obj.Loading_Slip = txtLoadingSlip.Text
                obj.Driver_Name = txtDriverName.Text
                obj.Driver_Mob_No = txtDriverMobNo.Text
                obj.Remarks = txtRemark.Text
                obj.Arr = GetTRData()
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function GetTRData() As List(Of clsCustomerTenderGatePassDetail)
        Dim Arr As New List(Of clsCustomerTenderGatePassDetail)
        For ii As Integer = 0 To Gv1.RowCount - 1
            If clsCommon.myLen(Gv1.Rows(ii).Cells(colItemCode).Value) > 0 Then
                Dim objTr As New clsCustomerTenderGatePassDetail()
                objTr.Item_Code = clsCommon.myCstr(Gv1.Rows(ii).Cells(colItemCode).Value)
                objTr.Unit_Code = clsCommon.myCstr(Gv1.Rows(ii).Cells(colUnit).Value)
                objTr.HSN_Code = clsCommon.myCstr(Gv1.Rows(ii).Cells(colHSNCode).Value)
                objTr.Qty = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colQty).Value)
                If clsCommon.myLen(objTr.Item_Code) > 0 Then
                    Arr.Add(objTr)
                End If
            End If
        Next
        Return Arr
    End Function
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            PostData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub PostData()
        Try
            If clsCommon.myLen(txtDocCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" & txtDocCode.Value & "]" & Environment.NewLine & "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsCustomerTenderGatePass.PostData(clsCommon.myCstr(txtDocCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtDocCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            DeleteData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                clsCustomerTenderGatePass.DeleteData(txtDocCode.Value)
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data not found!", Me.Text)
            Else
                PrintGatePass(txtDocCode.Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub PrintGatePass(ByVal Code As String)
        Try
            Dim atchqry As String = GetAttachQry(Code)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "crptAPSGatePassJPR", "APS Gate Pass")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function GetAttachQry(ByVal StrCode As String) As String
        Dim Qry As String = " select '" & objCommonVar.CurrentUserCode & "' as UserName, TSPL_COMPANY_MASTER.Insurance_No, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Insurance_Comp_Name,TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.ISO_No,TSPL_COMPANY_MASTER.add1 + case when len(TSPL_COMPANY_MASTER.add2)> 0 then ', ' + TSPL_COMPANY_MASTER.add2 else '' end + case when LEN( isnull(TSPL_COMPANY_MASTER.Add3, '') )> 0 then ', ' + isnull(TSPL_COMPANY_MASTER.Add3, '') else ' ' end as Comp_Address,tspl_company_master.GSTReg_No,TSPL_CUSTOMER_TENDER_GATE_PASS.loading_slip,
TSPL_CUSTOMER_TENDER_GATE_PASS.Location_code,TSPL_CUSTOMER_TENDER_GATE_PASS.Sub_Location,TSPL_CUSTOMER_TENDER_GATE_PASS.vehicle_no,TSPL_TRANSPORT_MASTER.Transporter_Name,TSPL_SD_SHIPMENT_HEAD.Document_Date as DispatchDate,TSPL_ROUTE_MASTER.Zone_Code,TSPL_CUSTOMER_TENDER_GATE_PASS.Driver_Name,TSPL_CUSTOMER_TENDER_GATE_PASS.Driver_Mob_No,TSPL_CUSTOMER_TENDER_GATE_PASS.Remarks,TSPL_SD_SHIPMENT_HEAD.Against_Cust_Order,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Code as GPCode,TSPL_CUSTOMER_MASTER.Area_Code,TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Date as GPDate,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SHIPMENT_HEAD.IsReplacement,TSPL_CUSTOMER_TENDER_GATE_PASS_Detail.Item_Code,tspl_item_master.Short_Description,TSPL_CUSTOMER_TENDER_GATE_PASS_Detail.Unit_Code,TSPL_CUSTOMER_TENDER_GATE_PASS_Detail.Qty,
tspl_company_master.Email as CompEmail,tspl_company_master.Phone1 as comp_Phone,TSPL_CUSTOMER_MASTER.Email as Cust_Email,TSPL_CUSTOMER_MASTER.Phone1 as CustPhone
from TSPL_CUSTOMER_TENDER_GATE_PASS
left join TSPL_CUSTOMER_TENDER_GATE_PASS_Detail on TSPL_CUSTOMER_TENDER_GATE_PASS_Detail.Document_Code = TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Code 
left join tspl_vehicle_master on tspl_vehicle_master.Vehicle_id = TSPL_CUSTOMER_TENDER_GATE_PASS.Vehicle_Code 
left join tspl_company_master on tspl_company_master.comp_code = '" & objCommonVar.CurrentCompanyCode & "'
left join tspl_item_master on tspl_item_master.item_code = TSPL_CUSTOMER_TENDER_GATE_PASS_Detail.Item_code 
left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id = TSPL_VEHICLE_MASTER.Transport_Id 
left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_CUSTOMER_TENDER_GATE_PASS.Dispatch_Code
left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No= TSPL_SD_SHIPMENT_HEAD.Route_No
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
where TSPL_CUSTOMER_TENDER_GATE_PASS.Document_Code='" & StrCode & "'"
        Return Qry
    End Function
    Private Sub btnGPCancel_Click(sender As Object, e As EventArgs) Handles btnGPCancel.Click
        Try
            Dim frm1 As New FrmPWD(Nothing)
            frm1.strType = clsFixedParameterType.Transactionupdate
            frm1.strCode = clsFixedParameterCode.GatePassCancel
            frm1.ShowDialog()
            If frm1.isPasswordCorrect Then
                CancelData()
                OneTimeCheck = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelData()
        Try
            If clsCommon.myLen(txtDocCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Document No not found to Post", Me.Text)
                Exit Sub
            End If
            Dim isCancel As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode = '" + txtDocCode.Value + "' and Status = '1'"))
            If isCancel Then
                common.clsCommon.MyMessageBoxShow(Me, "Record Already canceled.", Me.Text)
                Exit Sub
            End If
            If myMessages.cancelConfirm() Then
                If clsCustomerTenderGatePass.CancelData(txtDocCode.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully canceled", Me.Text)
                    LoadData(txtDocCode.Value, NavigatorType.Current)
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                End If
                'clsDBFuncationality.ExecuteNonQuery("Update TSPL_DAIRYSALE_GATEPASS_MASTER set post='Y' where gpcode='" & txtCode.Value & "'")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" & Environment.NewLine & "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsCustomerTenderGatePass.ReverseAndUnpost(txtDocCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            Dim obj As New clsCustomerTenderGatePass()
            obj = clsCustomerTenderGatePass.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                LoadBlankGrid()
                AddNew()
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                FieldControl(False)
                txtDocCode.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtDispatchCode.Value = obj.Dispatch_Code
                lblDispatchCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SD_SHIPMENT_HEAD where Document_Code='" & txtDispatchCode.Value & "'"))
                txtTransporterCode.Value = obj.Transporter_Code
                lblTransporterName.Text = connectSql.RunScalar("Select Transporter_Name  from TSPL_TRANSPORT_MASTER where Transport_Id = '" & Convert.ToString(txtTransporterCode.Value) & "'")
                txtVehicleCode.Value = obj.Vehicle_Code
                lblVehicleNo.Text = obj.Vehicle_No
                txtLocation.Value = obj.Location_Code
                lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
                If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                        txtSubLocation.Value = obj.Sub_Location
                    Else
                        txtSubLocation.Value = ""
                    End If
                End If
                txtLoadingSlip.Text = obj.Loading_Slip
                txtDriverMobNo.Text = obj.Driver_Mob_No
                txtDriverName.Text = obj.Driver_Name
                txtRemark.Text = obj.Remarks
                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsCustomerTenderGatePassDetail In obj.Arr
                        Gv1.Rows.AddNew()
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = sl
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(objTr.Item_Code) & "' ")
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = objTr.HSN_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        sl += 1
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
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
            Dim Whrcls As String = " Screen_Type='CT' and  status=1 and NOT EXISTS (SELECT 1 FROM TSPL_CUSTOMER_TENDER_GATE_PASS WHERE TSPL_CUSTOMER_TENDER_GATE_PASS.Dispatch_Code = TSPL_SD_SHIPMENT_HEAD.Document_Code )"
            txtDispatchCode.Value = clsCommon.ShowSelectForm("CT-GPfnd", qry, "Code", Whrcls, txtDispatchCode.Value, "Code", isButtonClicked)
            lblDispatchCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SD_SHIPMENT_HEAD where Document_Code='" & txtDispatchCode.Value & "'"))
            If clsCommon.myLen(txtDispatchCode.Value) > 0 Then
                FillData(txtDispatchCode.Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FillData(ByVal strCode As String)
        Try
            Dim strqry As String = "select TSPL_SD_SHIPMENT_HEAD.Document_Code, TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SHIPMENT_HEAD.Sub_Location_code,
TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.Transporter_Name,TSPL_SD_SHIPMENT_HEAD.Vehicle_Code,TSPL_SD_SHIPMENT_HEAD.VehicleNo,
TSPL_SD_SHIPMENT_HEAD.Description from TSPL_SD_SHIPMENT_HEAD where TSPL_SD_SHIPMENT_HEAD.Document_Code='" & strCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtLocation.Value = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
                lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
                If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                        txtSubLocation.Enabled = True
                    Else
                        txtSubLocation.Enabled = False
                    End If
                    txtSubLocation.Value = clsCommon.myCstr(dt.Rows(0)("Sub_Location_code"))
                    lblSubLocationDesc.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                End If
                txtTransporterCode.Value = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
                lblTransporterName.Text = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
                txtVehicleCode.Value = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
                lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
                txtRemark.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
                LoadBlankGrid()
                strqry = "select Item_Code,Unit_code,Qty from TSPL_SD_SHIPMENT_DETAIL where Document_Code='" & strCode & "'"
                Dim sl As Integer = 0
                Dim dtd As DataTable = clsDBFuncationality.GetDataTable(strqry)
                If dtd IsNot Nothing AndAlso dtd.Rows.Count > 0 Then
                    For Each dr As DataRow In dtd.Rows
                        Gv1.Rows.AddNew()
                        Gv1.Rows(sl).Cells(colLineNo).Value = sl + 1
                        Gv1.Rows(sl).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                        Gv1.Rows(sl).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' ")
                        Gv1.Rows(sl).Cells(colHSNCode).Value = clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' ")
                        Gv1.Rows(sl).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_code"))
                        Gv1.Rows(sl).Cells(colQty).Value = clsCommon.myCdbl(dr("Qty"))
                        sl += 1
                    Next
                End If
            Else
                Throw New Exception("Data not found!")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Loc_Status='N' and Location_Category not in('MCC') and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y' or isSaleLocation=1 "
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
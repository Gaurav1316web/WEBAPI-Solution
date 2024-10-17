Imports common
Public Class frmShortSupplyPenalty
#Region "Variable"
    Dim isNewEntry As Boolean = False
    Dim isPost As Boolean = False
    Dim isLoad As Boolean = False
    Const colPurchaseOrder As String = "colPurchaseOrder"
    Const colGRNNo As String = "colGRNNo"
    Const colGRNDate As String = "colGRNDate"
    Const colWeighmentCode As String = "colWeighmentCode"
    Const colWeighmentDate As String = "colWeighmentDate"
    Const colGrossWeight As String = "colGrossWeight"
    Const colTareWeight As String = "colTareWeight"
    Const colExtraWeight As String = "colExtraWeight"
    Const colNetWeight As String = "colNetWeight"
    Const colSRNNo As String = "colSRNNo"
    Const colSRNDate As String = "colSRNDate"
    Const colSRNQty As String = "colSRNQty"
    Const colUOM As String = "colUOM"
    Const colPINo As String = "colPINo"
    Const colPIStatus As String = "colPIStatus"
#End Region
    Private Sub frmShortSupplyPenalty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Try
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddNew()
        txtDocNo.Value = Nothing
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = Nothing
        lblBillToLocation.Text = Nothing
        txtTenderNo.Value = Nothing
        txtVendorNo.Value = Nothing
        lblVendorName.Text = Nothing
        txtItem.Value = Nothing
        lblItem.Text = Nothing
        txtRemarks.Text = Nothing
        txtSRN_PI.Value = Nothing
        lblRALQty.Text = 0
        lblSRNQty.Text = 0
        lblPenaltyQty.Text = 0
        lblApplicable.Text = 0
        lblShortQty.Text = 0
        lblRate.Text = 0
        txtPenaltyRate.Value = 0
        lblPenaltyAmt.Text = 0
        MyLabel6.Text = "Short Qty"
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
        btnReverseAndUnpost.Visible = False
        UsLock1.Status = ERPTransactionStatus.Pending
        btnAPInvoice.Visible = False
        txtSRN_PI.Enabled = False
        LoadBlankGrid()
    End Sub

    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()

        Dim repoPurchase As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPurchase.FormatString = ""
        repoPurchase.HeaderText = "Purchase Order"
        repoPurchase.Name = colPurchaseOrder
        repoPurchase.Width = 150
        gv1.MasterTemplate.Columns.Add(repoPurchase)

        Dim repoGRNNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGRNNo.FormatString = ""
        repoGRNNo.HeaderText = "GRN No"
        repoGRNNo.Name = colGRNNo
        repoGRNNo.Width = 150
        gv1.MasterTemplate.Columns.Add(repoGRNNo)

        Dim repoGRNDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGRNDate.FormatString = ""
        repoGRNDate.HeaderText = "GRN Date"
        repoGRNDate.Name = colGRNDate
        repoGRNDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoGRNDate)

        Dim repoWeighment As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoWeighment.FormatString = ""
        repoWeighment.HeaderText = "Weighment Code"
        repoWeighment.Name = colWeighmentCode
        repoWeighment.Width = 150
        gv1.MasterTemplate.Columns.Add(repoWeighment)

        Dim repoWeighmentDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoWeighmentDate.FormatString = ""
        repoWeighmentDate.HeaderText = "Weighment Date"
        repoWeighmentDate.Name = colWeighmentDate
        repoWeighmentDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoWeighmentDate)

        Dim repoGrossWeight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGrossWeight.FormatString = ""
        repoGrossWeight.HeaderText = "Gross Weight"
        repoGrossWeight.Name = colGrossWeight
        repoGrossWeight.Width = 80
        gv1.MasterTemplate.Columns.Add(repoGrossWeight)

        Dim repoTareWeight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTareWeight.FormatString = ""
        repoTareWeight.HeaderText = "Tare Weight"
        repoTareWeight.Name = colTareWeight
        repoTareWeight.Width = 80
        gv1.MasterTemplate.Columns.Add(repoTareWeight)

        Dim repoExtraWeight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoExtraWeight.FormatString = ""
        repoExtraWeight.HeaderText = "Extra Weight"
        repoExtraWeight.Name = colExtraWeight
        repoExtraWeight.Width = 80
        gv1.MasterTemplate.Columns.Add(repoExtraWeight)

        Dim repoNetWeight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetWeight.FormatString = ""
        repoNetWeight.HeaderText = "Net Weight"
        repoNetWeight.Name = colNetWeight
        repoNetWeight.Width = 80
        gv1.MasterTemplate.Columns.Add(repoNetWeight)

        Dim repoSRNNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSRNNo.FormatString = ""
        repoSRNNo.HeaderText = "SRN No"
        repoSRNNo.Name = colSRNNo
        repoSRNNo.Width = 150
        gv1.MasterTemplate.Columns.Add(repoSRNNo)

        Dim repoSRNDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSRNDate.FormatString = ""
        repoSRNDate.HeaderText = "SRN Date"
        repoSRNDate.Name = colSRNDate
        repoSRNDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoSRNDate)

        Dim repoSRNQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSRNQty.FormatString = ""
        repoSRNQty.HeaderText = "SRN Qty"
        repoSRNQty.Name = colSRNQty
        repoSRNQty.Width = 80
        gv1.MasterTemplate.Columns.Add(repoSRNQty)

        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUOM)

        Dim repoPI As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPI.FormatString = ""
        repoPI.HeaderText = "PI No"
        repoPI.Name = colPINo
        repoPI.Width = 150
        gv1.MasterTemplate.Columns.Add(repoPI)

        Dim repoPIStatus As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPIStatus.FormatString = ""
        repoPIStatus.HeaderText = "PI Status"
        repoPIStatus.Name = colPIStatus
        repoPIStatus.Width = 150
        gv1.MasterTemplate.Columns.Add(repoPIStatus)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.ReadOnly = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub txtSRN_PI__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSRN_PI._MYValidating
        Try
            Dim whr As String = Nothing
            Dim Qry As String = GetPI()
            If clsCommon.myLen(txtBillToLocation.Value) > 0 AndAlso clsCommon.myLen(txtTenderNo.Value) > 0 AndAlso clsCommon.myLen(txtVendorNo.Value) > 0 AndAlso clsCommon.myLen(txtItem.Value) > 0 Then
                Qry = "Select Top 1 * from(" + GetPI() + " )xxx "
                'whr = " IsNull(TSPL_PI_HEAD.PI_No,'')<>'' and TSPL_TENDER_PENALTY.Vendor_Code='" + txtVendorNo.Value + "' and  TSPL_ITEM_MASTER.Item_Code='" + txtItem.Value + "' and TSPL_TENDER_PENALTY.Tender_No='" + txtTenderNo.Value + "'  and TSPL_TENDER_PENALTY.Location_Code='" + txtBillToLocation.Value + "' "
                whr = " IsNull(PI_No,'')<>'' and Vendor='" + txtVendorNo.Value + "' and  Item='" + txtItem.Value + "' and Tender_No='" + txtTenderNo.Value + "'  and Location='" + txtBillToLocation.Value + "' "
            End If
            txtSRN_PI.Value = clsCommon.ShowSelectForm("Transporter@PI", Qry, "PI_No", whr, txtSRN_PI.Value, " [Created Date] desc", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function GetPI() As String
        Dim Qry As String = "Select TSPL_PI_HEAD.PI_No,TSPL_TENDER_PENALTY_DETAIL.SRN_No,TSPL_TENDER_PENALTY.Document_No,TSPL_TENDER_PENALTY.Document_Date,TSPL_TENDER_PENALTY.Tender_No, TSPL_TENDER_PENALTY.Location_Code as Location, TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_TENDER_PENALTY.Vendor_Code as Vendor,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TENDER_PENALTY.Item_Code as Item, TSPL_ITEM_MASTER.Item_Desc as ItemName,TSPL_TENDER_PENALTY.Remarks,case when TSPL_TENDER_PENALTY.Status='0' then 'Pending' else 'Approved' end as [Status],TSPL_TENDER_PENALTY.Created_Date As [Created Date]
                FROM TSPL_TENDER_PENALTY 
                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TENDER_PENALTY.Location_Code 
                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_PENALTY.Vendor_Code 
                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TENDER_PENALTY.Item_Code
                left outer join TSPL_TENDER_PENALTY_DETAIL on TSPL_TENDER_PENALTY_DETAIL.Document_No=TSPL_TENDER_PENALTY.Document_No
                left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_TENDER_PENALTY_DETAIL.SRN_No "
        Return Qry
    End Function

    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical' and Rejected_Type='N' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtBillToLocation.Value = clsCommon.ShowSelectForm("VMasterFND", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
            lblBillToLocation.Text = clsLocation.GetName(txtBillToLocation.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTenderNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTenderNo._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            Dim qry As String = "select xx.DocumentCode,max(xx.DocumentDate) as DocumentDate,xx.Location,max(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,xx.Vendor_Code,max(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,xx.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as ItemDesc from (
                                select DocumentCode,max(DocumentDate) as DocumentDate,Location,Vendor_Code,Item_Code,1 as RI,1 as Chk from (
                                select TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_HEADER.DocumentDate,TSPL_TENDER_DETAIL.Location,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_TENDER_DETAIL.Item_Code from TSPL_TENDER_DETAIL
                                left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode
                                where TSPL_TENDER_HEADER.Posted=1  and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "'
                                )x Group by DocumentCode,Location,Vendor_Code,Item_Code
                                union all
                                select TSPL_TENDER_PENALTY.Tender_No as DocumentCode,null as  DocumentDate, Location_Code as Location,Vendor_Code,Item_Code,-1 as RI,0 as Chk from TSPL_TENDER_PENALTY where TSPL_TENDER_PENALTY.Document_No not in ('" + txtDocNo.Value + "')
                                )xx 
                                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Location
                                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code
                                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code
                                Group by xx.DocumentCode,xx.Location,xx.Vendor_Code,xx.Item_Code having sum(xx.RI)>0 and sum(xx.Chk)>0 order by DocumentDate"

            Dim whr As String = "TSPL_TENDER_HEADER.Posted=1 and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "')"
            txtTenderNo.Value = clsTenderHead.getFinder(whr, txtTenderNo.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtTenderNo.Value = ""
        End Try
    End Sub

    Private Sub txtVendorNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendorNo._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                Throw New Exception("Please select Tender")
            End If
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER"
            Dim whr As String = " TSPL_VENDOR_MASTER.Status='N' and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "' and TSPL_TENDER_DETAIL.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code) "
            txtVendorNo.Value = clsVendorMaster.getFinder(whr, txtVendorNo.Value, isButtonClicked)
            lblVendorName.Text = clsVendorMaster.GetName(txtVendorNo.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtVendorNo.Value = ""
        End Try
    End Sub

    Private Sub txtItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItem._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                Throw New Exception("Please select Tender")
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                Throw New Exception("Please select Vendor")
            End If
            Dim whr As String = "  exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "' and TSPL_TENDER_DETAIL.Vendor_Code='" + txtVendorNo.Value + "' and TSPL_TENDER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) "
            txtItem.Value = clsItemMaster.getFinder(whr, txtItem.Value, isButtonClicked)
            lblItem.Text = clsItemMaster.GetItemName(txtItem.Value, Nothing)
            ReturnRALQty()
            fillPI()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtItem.Value = ""
        End Try
    End Sub

    Sub fillPI()
        Try
            Dim whr As String = Nothing
            If clsCommon.myLen(txtBillToLocation.Value) > 0 AndAlso clsCommon.myLen(txtTenderNo.Value) > 0 AndAlso clsCommon.myLen(txtVendorNo.Value) > 0 AndAlso clsCommon.myLen(txtItem.Value) > 0 Then
                whr = " where IsNull(TSPL_PI_HEAD.PI_No,'')<>'' And TSPL_TENDER_PENALTY.Vendor_Code='" + txtVendorNo.Value + "' and  TSPL_ITEM_MASTER.Item_Code='" + txtItem.Value + "' and TSPL_TENDER_PENALTY.Tender_No='" + txtTenderNo.Value + "'  and TSPL_TENDER_PENALTY.Location_Code='" + txtBillToLocation.Value + "'"
            End If
            Dim Qry As String = "Select top 1 * from (" + GetPI() + whr + ")xyz "
            Qry += " Order By xyz.[Created Date] Desc"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtSRN_PI.Value = clsCommon.myCstr(dt.Rows(0)("PI_No"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                txtBillToLocation.Focus()
                Throw New Exception("Please Select " + txtBillToLocation.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                txtTenderNo.Focus()
                Throw New Exception("Please Select " + txtTenderNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                txtVendorNo.Focus()
                Throw New Exception("Please Select " + txtVendorNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtItem.Value) <= 0 Then
                txtItem.Focus()
                Throw New Exception("Please Select " + txtItem.MyLinkLable1.Text)
            End If

            Dim Qry As String = "Select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No As [Purchase Order],TSPL_GRN_HEAD.GRN_No As 'GRN No',convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as 'GRN Date',TSPL_GRN_HEAD.VehicleNo As 'Vehicle No',TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as 'Weighment Code',convert(varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as 'Weighment Date',TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight as 'Gross Weight',TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight as 'Tare Weight',TSPL_PO_WEIGHTMENT_DETAIL.Extra_Weight as 'Extra Weight',TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as 'Net Weight',TSPL_SRN_HEAD.SRN_No as 'SRN No',convert(varchar,TSPL_SRN_HEAD.SRN_Date,103) as  'SRN Date',isNull(TSPL_SRN_DETAIL.SRN_Qty,0) as 'SRN Qty',TSPL_PO_WEIGHTMENT_DETAIL.UOM,TSPL_PI_HEAD.PI_No As [PI No],Case When Isnull(TSPL_PI_HEAD.Status,0)=1 Then 'Approved' Else 'Unapproved' End As [PI Status]
                    from TSPL_GRN_DETAIL
                    left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No
                    left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id
					LEFT Outer Join TSPL_PURCHASE_ORDER_DETAIL On TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No
                    left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_HEAD.GRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_HEAD on  TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No
					left outer join TSPL_TENDER_PENALTY_DETAIL on TSPL_TENDER_PENALTY_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No
                    left outer join TSPL_TENDER_PENALTY ON TSPL_TENDER_PENALTY.Document_No = TSPL_TENDER_PENALTY_DETAIL.Document_No
                    left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id = TSPL_SRN_DETAIL.SRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
					left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No = TSPL_PI_DETAIL.PI_No 
                    left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code= TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code and  TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_DEDUCTION_SECURITY on TSPL_SRN_DEDUCTION_SECURITY.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION_SECURITY.Item_Code=TSPL_SRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_DEDUCTION on TSPL_SRN_DEDUCTION.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION.Item_Code=TSPL_SRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_TENDER on TSPL_SRN_TENDER.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_TENDER.Item_Code=TSPL_SRN_DETAIL.Item_Code and isnull(TSPL_SRN_TENDER.Penalty,0)>0
                    left outer join TSPL_TENDER_SCHEDULE_PENALTY on  TSPL_TENDER_SCHEDULE_PENALTY.PK_Id=TSPL_SRN_TENDER.Against_Tender_Schedule_Penalty_PK_Id
                    left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_PURCHASE_ORDER_HEAD.RefTendorNo
                    left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                    where TSPL_PURCHASE_ORDER_HEAD.Against_Tender='Y' And  TSPL_PURCHASE_ORDER_HEAD.RefTendorNo='" + txtTenderNo.Value + "' and TSPL_QC_CHECK_HEAD.QC_Status<>'Rejected'  and TSPL_GRN_DETAIL.Item_Code='" + txtItem.Value + "' and TSPL_GRN_HEAD.Vendor_Code='" + txtVendorNo.Value + "' and TSPL_GRN_HEAD.Bill_To_Location='" + txtBillToLocation.Value + "' and ISNULL( TSPL_GRN_HEAD.IsCancel,0)=0  
                    Order by CONVERT(date, TSPL_GRN_HEAD.GRN_Date,103),isnull(TSPL_SRN_HEAD.Status,0) desc "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                isLoad = False
                LoadBlankGrid()
                For Each rows In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPurchaseOrder).Value = clsCommon.myCstr(rows("Purchase Order"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNNo).Value = clsCommon.myCstr(rows("GRN No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNDate).Value = clsCommon.GetPrintDate(rows("GRN Date"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWeighmentCode).Value = clsCommon.myCstr(rows("Weighment Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWeighmentDate).Value = clsCommon.GetPrintDate(rows("Weighment Date"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myCdbl(rows("Gross Weight"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myCdbl(rows("Tare Weight"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colExtraWeight).Value = clsCommon.myCdbl(rows("Extra Weight"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myCdbl(rows("Net Weight"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = clsCommon.myCstr(rows("SRN No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNDate).Value = clsCommon.GetPrintDate(rows("SRN Date"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNQty).Value = clsCommon.myCdbl(rows("SRN Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(rows("UOM"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPINo).Value = clsCommon.myCstr(rows("PI No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPIStatus).Value = clsCommon.myCstr(rows("PI Status"))
                Next
                gv1.ReadOnly = True
                SetGridFormat()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
            gv1.ShowGroupPanel = False
            gv1.ShowRowHeaderColumn = False
            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).BestFit()
            Next
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item5 As New GridViewSummaryItem(colGrossWeight, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item4 As New GridViewSummaryItem(colTareWeight, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item6 As New GridViewSummaryItem(colNetWeight, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item8 As New GridViewSummaryItem(colSRNQty, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            gv1.AutoSizeRows = False
            gv1.BestFitColumns()
            If Not isLoad Then
                calculate()
            End If
        End If
    End Sub

    Sub calculate()
        ReturnRALQty()
        If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
            Dim SRNQty As Double = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                SRNQty += clsCommon.myCdbl(grow.Cells(colSRNQty).Value)
            Next
            'lblRALQty.Text = RALQty
            lblSRNQty.Text = SRNQty
            lblPenaltyQty.Text = Math.Round(clsCommon.myCdbl(lblRALQty.Text) - SRNQty, 2)
        Else
            lblRALQty.Text = 0
            lblSRNQty.Text = 0
            lblPenaltyQty.Text = 0
        End If
    End Sub

    Sub ReturnRALQty()
        Dim Qry As String = "  Select Sum(TSPL_TENDER_SCHEDULE.Schedule_Qty)Schedule_Qty,Max(TSPL_TENDER_SCHEDULE.Schedule_Short_Per)Schedule_Short_Per,
 Max(TSPL_TENDER_DETAIL.Rate)Item_Cost
 from TSPL_TENDER_DETAIL
 Left Outer Join TSPL_TENDER_SCHEDULE On TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode 
 And TSPL_TENDER_SCHEDULE.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code 
 And TSPL_TENDER_SCHEDULE.Item_Code=TSPL_TENDER_DETAIL.Item_Code
 And TSPL_TENDER_SCHEDULE.Location_Code=TSPL_TENDER_DETAIL.Location 
where TSPL_TENDER_DETAIL.Vendor_Code='" + txtVendorNo.Value + "' and TSPL_TENDER_DETAIL.Item_Code='" + txtItem.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "' and TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblRALQty.Text = clsCommon.myCdbl(dt.Rows(0)("Schedule_Qty"))
            lblApplicable.Text = clsCommon.myCdbl(dt.Rows(0)("Schedule_Short_Per"))
            lblRate.Text = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Item_Cost")), 2)
            lblShortQty.Text = clsCommon.myCdbl(dt.Rows(0)("Schedule_Qty")) * (clsCommon.myCdbl(dt.Rows(0)("Schedule_Short_Per")) / 100)
            If Not MyLabel6.Text.Contains(clsCommon.myCstr(dt.Rows(0)("Schedule_Short_Per"))) Then
                MyLabel6.Text += "(>" + clsCommon.myCstr(dt.Rows(0)("Schedule_Short_Per")) + "%)"
            End If
        End If
    End Sub

    Private Sub txtPenaltyRate_TextChanged(sender As Object, e As EventArgs) Handles txtPenaltyRate.TextChanged
        Try
            If clsCommon.myCdbl(lblPenaltyQty.Text) > clsCommon.myCdbl(lblShortQty.Text) Then
                lblPenaltyAmt.Text = Math.Round((clsCommon.myCdbl(lblPenaltyQty.Text) * clsCommon.myCdbl(lblRate.Text)) * (clsCommon.myCdbl(txtPenaltyRate.Value) / 100))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                txtBillToLocation.Focus()
                Throw New Exception("Please select " + txtBillToLocation.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                txtTenderNo.Focus()
                Throw New Exception("Please select " + txtTenderNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                txtVendorNo.Focus()
                Throw New Exception("Please select " + txtVendorNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtItem.Value) <= 0 Then
                txtItem.Focus()
                Throw New Exception("Please select " + txtItem.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtSRN_PI.Value) <= 0 Then
                txtSRN_PI.Focus()
                Throw New Exception("PI No can't be blank !")
            End If

            If clsCommon.myLen(lblPenaltyAmt.Text) <= 0 Then
                Throw New Exception("Document can't save beacuse penalty amount is : " + clsCommon.myCstr(lblPenaltyAmt.Text))
            End If

            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                For i As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(i).Cells(colPINo).Value) <= 0 Then
                        Throw New Exception("PI not created at Line No : " + clsCommon.myCstr((i + 1)))
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colPIStatus).Value), "Approved") <> CompairStringResult.Equal Then
                        Throw New Exception("PI not approved at Line No : " + clsCommon.myCstr((i + 1)))
                    End If
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            If Not isPost Then
                If common.clsCommon.MyMessageBoxShow("Are you sure to save data ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            Else
                If common.clsCommon.MyMessageBoxShow("Are you sure to post data ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            If AllowToSave() Then
                Dim obj As New clsShortSupplyPenalty()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Location = txtBillToLocation.Value
                obj.RAL_No = txtTenderNo.Value
                obj.Vendor_No = txtVendorNo.Value
                obj.Item_Code = txtItem.Value
                obj.Remarks = txtRemarks.Text
                obj.PI_No = txtSRN_PI.Value
                obj.RAL_Qty = lblRALQty.Text
                obj.SRN_Qty = lblSRNQty.Text
                obj.Short_Excess_Qty = lblPenaltyQty.Text
                obj.Penalty_Applicable_Per = lblApplicable.Text
                obj.Applicable_Short_Qty = lblShortQty.Text
                obj.Item_Rate = lblRate.Text
                obj.Penalty_Rate = txtPenaltyRate.Value
                obj.Penalty_Amount = lblPenaltyAmt.Text
                If isPost Then
                    obj.Status = 1
                    obj.isPost = True
                Else
                    obj.isPost = False
                    obj.Status = 0
                End If
                If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                    obj.Arr = New List(Of clsShortSupplyPenaltyDetail)
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim objTR As New clsShortSupplyPenaltyDetail()
                        objTR.GRN_No = clsCommon.myCstr(grow.Cells(colGRNNo).Value)
                        objTR.GRN_Date = clsCommon.myCDate(grow.Cells(colGRNDate).Value)
                        'objTR.Vehicle_No = clsCommon.myCstr(grow.Cells("Vehicle No").Value)
                        objTR.Weighment_Code = clsCommon.myCstr(grow.Cells(colWeighmentCode).Value)
                        objTR.Weighment_Date = clsCommon.myCDate(grow.Cells(colWeighmentDate).Value)
                        objTR.Gross_Weight = clsCommon.myCdbl(grow.Cells(colGrossWeight).Value)
                        objTR.Tare_Weight = clsCommon.myCdbl(grow.Cells(colTareWeight).Value)
                        objTR.Extra_Weight = clsCommon.myCdbl(grow.Cells(colExtraWeight).Value)
                        objTR.Net_Weight = clsCommon.myCdbl(grow.Cells(colNetWeight).Value)
                        objTR.SRN_No = clsCommon.myCstr(grow.Cells(colSRNNo).Value)
                        objTR.SRN_Date = clsCommon.myCDate(grow.Cells(colSRNDate).Value)
                        objTR.SRN_Qty = clsCommon.myCdbl(grow.Cells(colSRNQty).Value)
                        objTR.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        If clsCommon.myLen(objTR.GRN_No) > 0 Then
                            obj.Arr.Add(objTR)
                        End If
                    Next
                End If
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    isNewEntry = False
                Else
                    isNewEntry = True
                End If

                If obj.SaveData(obj, isNewEntry) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully.", Me.Text)
                    End If
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            'If clsCommon.myLen(strCode) <= 0 Then
            '    Throw New Exception("Document No can't be blank.")
            'End If
            Dim obj As New clsShortSupplyPenalty()

            isLoad = True
            btnSave.Enabled = True
            btnPost.Enabled = False
            btnDelete.Enabled = False
            isNewEntry = True
            btnSave.Text = "Save"
            AddNew()
            obj = clsShortSupplyPenalty.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtBillToLocation.Value = obj.Location
                lblBillToLocation.Text = obj.Location_Desc
                txtTenderNo.Value = obj.RAL_No
                txtVendorNo.Value = obj.Vendor_No
                lblVendorName.Text = obj.Vendor_Name
                txtItem.Value = obj.Item_Code
                lblItem.Text = obj.Item_Desc
                txtRemarks.Text = obj.Remarks
                txtSRN_PI.Value = obj.PI_No
                lblRALQty.Text = obj.RAL_Qty
                lblSRNQty.Text = obj.SRN_Qty
                lblPenaltyQty.Text = obj.Short_Excess_Qty
                lblApplicable.Text = obj.Penalty_Applicable_Per
                lblShortQty.Text = obj.Applicable_Short_Qty
                lblRate.Text = obj.Item_Rate
                txtPenaltyRate.Value = obj.Penalty_Rate
                lblPenaltyAmt.Text = obj.Penalty_Amount

                If obj.Status = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnAPInvoice.Visible = True
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnAPInvoice.Visible = False
                End If
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsShortSupplyPenaltyDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPurchaseOrder).Value = clsCommon.myCstr(objTr.PurchaseOrder)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNNo).Value = clsCommon.myCstr(objTr.GRN_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNDate).Value = clsCommon.GetPrintDate(objTr.GRN_Date)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWeighmentCode).Value = clsCommon.myCstr(objTr.Weighment_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWeighmentDate).Value = clsCommon.GetPrintDate(objTr.Weighment_Date)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myCdbl(objTr.Gross_Weight)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myCdbl(objTr.Tare_Weight)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colExtraWeight).Value = clsCommon.myCdbl(objTr.Extra_Weight)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myCdbl(objTr.Net_Weight)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = clsCommon.myCstr(objTr.SRN_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNDate).Value = clsCommon.GetPrintDate(objTr.SRN_Date)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNQty).Value = clsCommon.myCdbl(objTr.SRN_Qty)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(objTr.UOM)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPINo).Value = clsCommon.myCstr(objTr.PI_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPIStatus).Value = clsCommon.myCstr(objTr.PI_Status)
                    Next
                End If
                SetGridFormat()
                isLoad = False
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
            isNewEntry = True
        Catch ex As Exception
            isLoad = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Document Code can't be blank !")
            End If

            If clsCommon.MyMessageBoxShow("Are you sure to delete data ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsShortSupplyPenalty.DeleteData(txtDocNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Document Code can't be blank !")
            End If
            SaveData(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim Qry As String = "select Document_No,Document_Date,Location_Code,Tendor_No,Vendor_No,Item_Code,Remarks,PI_No,RAL_Qty,SRN_Qty,Penalty_Qty,Penalty_Applicable_Per,Short_Qty,Item_Rate,Penalty_Rate,Penalty_Amount,Status from TSPL_SHORT_SUPPLY_PENALTY "
            txtDocNo.Value = clsCommon.ShowSelectForm("DocNoFndd", Qry, "Document_No", "", txtDocNo.Value, "Document_No", isButtonClicked)
            LoadData(txtDocNo.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmShortSupplyPenalty_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseAndUnpost.Visible = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReverseAndUnpost_Click(sender As Object, e As EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Are you sure to reverse and unpost ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    If clsShortSupplyPenalty.ReverseAndUnpost(txtDocNo.Value, txtTenderNo.Value, txtItem.Value, txtVendorNo.Value, txtBillToLocation.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Reverse and Unposted successfully.", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Document code can't be blank !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Document No can't be blank !")
            End If
            Dim Qry As String = "Select TSPL_SHORT_SUPPLY_PENALTY.Document_No,	TSPL_SHORT_SUPPLY_PENALTY.Document_Date,	TSPL_SHORT_SUPPLY_PENALTY.Location_Code,	TSPL_SHORT_SUPPLY_PENALTY.Tendor_No,	TSPL_SHORT_SUPPLY_PENALTY.Vendor_No,TSPL_Vendor_Master.Vendor_Name,	TSPL_SHORT_SUPPLY_PENALTY.Item_Code,TSPL_ITEM_MASTER.Item_Desc,	TSPL_SHORT_SUPPLY_PENALTY.Remarks,	TSPL_SHORT_SUPPLY_PENALTY.PI_No,	TSPL_SHORT_SUPPLY_PENALTY.RAL_Qty,	TSPL_SHORT_SUPPLY_PENALTY.SRN_Qty,	TSPL_SHORT_SUPPLY_PENALTY.Penalty_Qty,	TSPL_SHORT_SUPPLY_PENALTY.Penalty_Applicable_Per,	TSPL_SHORT_SUPPLY_PENALTY.Short_Qty,	TSPL_SHORT_SUPPLY_PENALTY.Item_Rate,	TSPL_SHORT_SUPPLY_PENALTY.Penalty_Rate,	TSPL_SHORT_SUPPLY_PENALTY.Penalty_Amount,TSPL_TENDER_HEADER.DocumentDate As Tendor_Date,TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Document_No,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.GRN_No,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.GRN_Date,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Weighment_Code,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Weighment_Date,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Gross_Weight,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Tare_Weight,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Extra_Weight,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Net_Weight,	TSPL_SHORT_SUPPLY_PENALTY_DETAIL.SRN_No,TSPL_SHORT_SUPPLY_PENALTY_DETAIL.SRN_Date,TSPL_SHORT_SUPPLY_PENALTY_DETAIL.SRN_Qty As SRNQty,TSPL_SHORT_SUPPLY_PENALTY_DETAIL.UOM,
                                TSPL_company_master.Comp_Name,TSPL_company_master.Logo_Img,TSPL_company_master.Logo_Img2,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.Add3
                                from TSPL_SHORT_SUPPLY_PENALTY
                                Left Outer Join TSPL_SHORT_SUPPLY_PENALTY_DETAIL On TSPL_SHORT_SUPPLY_PENALTY_DETAIL.Document_No=TSPL_SHORT_SUPPLY_PENALTY.Document_No
                                left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_SHORT_SUPPLY_PENALTY.Tendor_No
                                left Outer Join TSPL_Vendor_Master On TSPL_Vendor_Master.Vendor_Code=TSPL_SHORT_SUPPLY_PENALTY.Vendor_No
                                left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SHORT_SUPPLY_PENALTY.Item_Code
								left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_SHORT_SUPPLY_PENALTY.Location_Code
                                left Outer Join TSPL_company_master On TSPL_company_master.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                                 where TSPL_SHORT_SUPPLY_PENALTY.Document_No='" + txtDocNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "ShortSupplyPenalty", "Short Supply Penalty", Nothing)
                frmCRV = Nothing
            Else
                Throw New Exception("Data Not Found !")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAPInvoice_Click(sender As Object, e As EventArgs) Handles btnAPInvoice.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim APInvoice As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocNo='" + txtDocNo.Value + "'"))
                If clsCommon.myLen(APInvoice) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnAPInvoiceEntry, APInvoice)
                Else
                    clsCommon.MyMessageBoxShow(Me, "AP Invoice not created !", Me.Text)
                End If
            Else
                    clsCommon.MyMessageBoxShow(Me, "Document No can't be blank !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
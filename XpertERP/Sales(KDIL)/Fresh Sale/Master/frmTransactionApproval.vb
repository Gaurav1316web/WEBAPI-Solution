'---shivani
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Public Class FrmTransactionApproval
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isnewentry As Boolean = False
    Public Const colSlNo As String = "colSlNo"
    Public Const colParNo As String = "colParNo"
    Public Const colParValue As String = "colParValue"

    Public Const colSRNNo As String = "colSRNNo"
    Public Const colGateEntryNo As String = "colGateEntryNo"
    Public Const colModifyBy As String = "colModifyBy"
    Public Const colSRNDATe As String = "colSRNDATe"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colUOM As String = "colUOM"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colFat As String = "colFat"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFatKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colBasicRate As String = "colBasicRate"
    Public Const colNewBasicRate As String = "colNewBasicRate"
    Public Const colDeduc As String = "colDeduc"
    Public Const colIncen As String = "colIncen"
    Public Const colSpecialDeduc As String = "colSpecialDeduc"
    Public Const colNetRate As String = "colNetRate"
    Public Const colNewNetRate As String = "colNewNetRate"
    Public Const colSelect As String = "colSelect"

    Public Const colLocCode As String = "colLocCode"
    Public Const colLocDesc As String = "colLocDesc"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorDesc As String = "colVendorDesc"
    Public Const colTankerNo As String = "colTankerNo"
    Public Const colApprovalType As String = "colApprovalType"
    Public Const colQcNo As String = "colQcNo"
    Public Const colRemarks As String = "colRemarks"
    Public Const colQcDate As String = "colQcDate"
    Public Const colSpclDeductionAmt As String = "colSpclDeductionAmt"
    Public Const colbtnCol As String = "colbtnCol"
    Public Const colbtnQC As String = "colbtnQC"
    Public Const colDocNo As String = "colDocNo"
    Public Const colDocDate As String = "colDocDate"
    Public Const colApprovalRemarks As String = "colApprovalRemarks"

    '' columns for dairy booking 
    Public Const colDemandAmount As String = "colDemandAmount"
    Public Const colAvailAmount As String = "colAvailAmount"
    Public Const colShortExcess As String = "colShortExcess"
    Public Const colCreditLimit As String = "colCreditLimit"
    Dim formtype As String = Nothing
    Dim strTranAppPass As String = ""
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub SetUserMgmtNew()
        '=====shivani
        If formtype = clsUserMgtCode.FrmTransactionApproval Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmTransactionApproval)
            strTranAppPass = clsUserMgtCode.FrmTransactionApproval
        ElseIf formtype = clsUserMgtCode.FrmBulkTransactionApproval Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmBulkTransactionApproval)
            strTranAppPass = clsUserMgtCode.FrmBulkTransactionApproval
        ElseIf formtype = clsUserMgtCode.FrmMCCTransactionApproval Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmMCCTransactionApproval)
            strTranAppPass = clsUserMgtCode.FrmMCCTransactionApproval
        ElseIf formtype = clsUserMgtCode.FrmFreshTransactionApproval Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmFreshTransactionApproval)
            strTranAppPass = clsUserMgtCode.FrmFreshTransactionApproval
        End If
        '================


        'MyBase.SetUserMgmt(clsUserMgtCode.FrmTransactionApproval)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnApprove.Visible = MyBase.isModifyFlag
        btnUnapprove.Visible = MyBase.isModifyFlag

    End Sub
    Sub LoadScreenName()
        isInsideLoadData = True
        Dim qry As String = "select 'Select' as [Code],'Select' as [Name] union all select Program_Code as [Code] ,Program_Name as [Name]   from TSPL_PROGRAM_MASTER where  Program_Code in ('DEL-NOTE-FS','DEL-ORD-PS','DISPATCH-BS','M-Material','VSP-Item','SHIPMENT-PS','CSA-INV-TRN','M-SRN-B','M-RECEIPT','M-QC','LC-CREATION','BOOK-DS','" + clsUserMgtCode.frmCSADeliveryOrder + "','" + clsUserMgtCode.mbtnPurchaseInvoice + "','" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "','" + clsUserMgtCode.frmDemandBooking + "')"
        cmbScreenName.DataSource = clsDBFuncationality.GetDataTable(qry)
        cmbScreenName.ValueMember = "Code"
        cmbScreenName.DisplayMember = "Name"
        cmbScreenName.SelectedIndex = 0
        isInsideLoadData = False
    End Sub

    Private Sub FrmTransactionApproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        LoadScreenName()
        'AddNew()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnApprove, "Press Alt+A for Approve ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Adding New ")
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Hidden
    End Sub
    Sub loadUnapprovedQcDataForBulkMilkProcBlankGrid()
        Try
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Dim colChkBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            colChkBox.HeaderText = "Select "
            colChkBox.Name = colSelect
            colChkBox.ReadOnly = False
            colChkBox.Width = 50
            colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(colChkBox)

            Dim repoSLNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "SL.No"
            repoSLNo.Name = colSlNo
            repoSLNo.Width = 60
            repoSLNo.ReadOnly = True
            repoSLNo.BestFit()
            Gv1.MasterTemplate.Columns.Add(repoSLNo)

            repoSLNo = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "Approval Type"
            repoSLNo.Name = colApprovalType
            repoSLNo.Width = 100
            repoSLNo.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSLNo)

            Dim repoTnkrNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTnkrNo.FormatString = ""
            repoTnkrNo.HeaderText = "Tanker No"
            repoTnkrNo.Name = colTankerNo
            repoTnkrNo.Width = 100
            repoTnkrNo.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoTnkrNo)

            Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "Qc No"
            repoSRNNO.Name = colQcNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)

            Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoSRNDate.FormatString = "{0:d}"
            repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
            repoSRNDate.HeaderText = "QC Date"
            repoSRNDate.Name = colQcDate
            repoSRNDate.Width = 100
            repoSRNDate.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNDate)


            Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLCode.FormatString = ""
            repoLCode.HeaderText = "Loc Code"
            repoLCode.Name = colLocCode
            repoLCode.Width = 100
            repoLCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLCode)



            Dim repoLDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLDesc.FormatString = ""
            repoLDesc.HeaderText = "Loc Desc"
            repoLDesc.Name = colLocDesc
            repoLDesc.Width = 100
            repoLDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLDesc)


            Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVCode.FormatString = ""
            repoVCode.HeaderText = "Vendor Code"
            repoVCode.Name = colVendorCode
            repoVCode.Width = 100
            repoVCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVCode)

            Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVDesc.FormatString = ""
            repoVDesc.HeaderText = "Vendor Desc"
            repoVDesc.Name = colVendorDesc
            repoVDesc.Width = 100
            repoVDesc.ReadOnly = True

            Gv1.MasterTemplate.Columns.Add(repoVDesc)

            Dim repoDeduAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoDeduAmt.FormatString = ""
            repoDeduAmt.HeaderText = "Special Deduction Amount"
            repoDeduAmt.Name = colSpclDeductionAmt
            repoDeduAmt.Width = 100
            repoDeduAmt.ReadOnly = False
            Gv1.MasterTemplate.Columns.Add(repoDeduAmt)

            repoSLNo = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "Remarks"
            repoSLNo.Name = colRemarks
            repoSLNo.Width = 300
            repoSLNo.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSLNo)

            Dim RepobtnCol As GridViewCommandColumn = New GridViewCommandColumn()
            RepobtnCol.HeaderText = "Details "
            RepobtnCol.Name = colbtnCol
            RepobtnCol.ReadOnly = False
            RepobtnCol.Width = 150
            RepobtnCol.DefaultText = "Click Here..."
            RepobtnCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(RepobtnCol)

            

            Gv1.AllowAddNewRow = False
            Gv1.AllowColumnChooser = True
            Gv1.ShowGroupPanel = False
            Gv1.AllowColumnReorder = True
            Gv1.AllowRowReorder = True
            Gv1.EnableSorting = True
            Gv1.MasterTemplate.ShowRowHeaderColumn = False
            Gv1.MasterTemplate.ShowColumnHeaders = True
            Gv1.EnableAlternatingRowColor = True
            Gv1.TableElement.TableHeaderHeight = 40
            Gv1.EnableFiltering = True
            'Gv1.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
            'Gv1.BestFitColumns()
            Gv1.AutoSizeRows = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub loadUnapprovedQcDataForBulkMilkProcData()
        Try
            Dim qry As String = "  select TSPL_TRANSACTION_APPROVAL.Document_No,TSPL_TRANSACTION_APPROVAL.Approval_type,TSPL_QUALITY_CHECK.Tanker_No,TSPL_QUALITY_CHECK.remarks ,TSPL_QUALITY_CHECK.QC_In_Date_Time,TSPL_QUALITY_CHECK.location_Code,TSPL_QUALITY_CHECK.Vendor_Code ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_VENDOR_MASTER.Vendor_Name    from TSPL_TRANSACTION_APPROVAL left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No=TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_QUALITY_CHECK.location_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_QUALITY_CHECK.Vendor_Code  where  TSPL_TRANSACTION_APPROVAL.Program_Code='M-QC' and TSPL_TRANSACTION_APPROVAL.Approve=0 "
            Dim whrcls As String = ""
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrcls = " and TSPL_QUALITY_CHECK.location_Code in ( " & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qry = qry & whrcls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FndDocumnetNo.Value = ""
                LblDocDate.Text = ""
                FndDocumnetNo.Enabled = False
                ddApprovalType.Text = "Special Approval"
                loadUnapprovedQcDataForBulkMilkProcBlankGrid()
                Gv1.Visible = True
                For i As Integer = 0 To dt.Rows.Count - 1
                    Gv1.Rows.AddNew()
                    Gv1.Rows(i).Cells(colSelect).Value = False
                    Gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                    Gv1.Rows(i).Cells(colQcNo).Value = dt.Rows(i)("Document_No")
                    Gv1.Rows(i).Cells(colQcDate).Value = dt.Rows(i)("QC_In_Date_Time")
                    Gv1.Rows(i).Cells(colTankerNo).Value = dt.Rows(i)("Tanker_No")
                    Gv1.Rows(i).Cells(colLocCode).Value = dt.Rows(i)("location_Code")
                    Gv1.Rows(i).Cells(colLocDesc).Value = dt.Rows(i)("Location_Desc")
                    Gv1.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    Gv1.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    Gv1.Rows(i).Cells(colApprovalType).Value = dt.Rows(i)("Approval_type")
                    Gv1.Rows(i).Cells(colRemarks).Value = dt.Rows(i)("remarks")
                    Gv1.Rows(i).Cells(colSpclDeductionAmt).Value = 0
                    If clsCommon.CompairString(dt.Rows(i)("Approval_type"), "Special Approval") = CompairStringResult.Equal Then
                        Gv1.Rows(i).Cells(colSpclDeductionAmt).ReadOnly = True
                    Else
                        Gv1.Rows(i).Cells(colSpclDeductionAmt).ReadOnly = False
                    End If
                    Gv1.Rows(i).Cells(colbtnCol).Value = "Click Here..."
                Next
            Else
                FndDocumnetNo.Enabled = True
                ddApprovalType.Text = "Rate"
                Gv1.Visible = False
                clsCommon.MyMessageBoxShow("No QC Found to approve")
                btnReset.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub loadGridDataAll()
        Try
            Dim qry As String = ""

           

            If clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = " select TSPL_TRANSACTION_APPROVAL.Document_No ,TSPL_TRANSACTION_APPROVAL.Approval_type ,TSPL_TRANSACTION_APPROVAL.Doc_Date,tspl_sd_sale_invoice_head.Customer_Code as CustCode ,tspl_sd_sale_invoice_head.Bill_To_Location as LocCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName,TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL left outer join tspl_sd_sale_invoice_head on tspl_sd_sale_invoice_head.document_code=TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_sd_sale_invoice_head.Customer_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_sd_sale_invoice_head.Bill_To_Location  where  TSPL_TRANSACTION_APPROVAL.approve=0 and   TSPL_TRANSACTION_APPROVAL.program_code='CSA-INV-TRN'"
            End If
            If clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = " select TSPL_TRANSACTION_APPROVAL.Document_No ,TSPL_TRANSACTION_APPROVAL.Approval_type ,TSPL_TRANSACTION_APPROVAL.Doc_Date,TSPL_CSA_DO_HEAD.Cust_Code as CustCode ,TSPL_CSA_DO_HEAD.to_location_code as LocCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName,TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL left outer join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.doc_no=TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_DO_HEAD.cust_code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_CSA_DO_HEAD.to_location_code  where  TSPL_TRANSACTION_APPROVAL.approve=0 and   TSPL_TRANSACTION_APPROVAL.program_code='" + clsUserMgtCode.frmCSADeliveryOrder + "'"
            End If
            If clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = " select TSPL_TRANSACTION_APPROVAL.Document_No,TSPL_TRANSACTION_APPROVAL.Approval_type ,TSPL_TRANSACTION_APPROVAL.Doc_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code as CustCode ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.location_code as LocCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName,TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.location_code  where  TSPL_TRANSACTION_APPROVAL.approve=0 and   TSPL_TRANSACTION_APPROVAL.program_code='DEL-NOTE-FS'"
            End If
            If clsCommon.CompairString(clsUserMgtCode.frmDemandBooking, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = "select TSPL_TRANSACTION_APPROVAL.Document_No, TSPL_TRANSACTION_APPROVAL.Approval_type, TSPL_TRANSACTION_APPROVAL.Doc_Date, TSPL_TRANSACTION_APPROVAL.Cust_Code as CustCode, TSPL_DEMAND_BOOKING_MASTER.Location_Code as LocCode, TSPL_CUSTOMER_MASTER.Customer_Name as CustName, TSPL_LOCATION_MASTER.Location_Desc as LocDesc from TSPL_TRANSACTION_APPROVAL left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_TRANSACTION_APPROVAL.cust_code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.Location_Code where TSPL_TRANSACTION_APPROVAL.approve = 0 and TSPL_TRANSACTION_APPROVAL.program_code = '" + clsUserMgtCode.frmDemandBooking + "'"
            End If
            If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
                Dim WhrCustCategory As String = ""
                If chkCustCategoryMappInUserMaster = True Then
                    WhrCustCategory += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
                End If
                qry = " select * from ( select   TSPL_BOOKING_DETAIL.DocumentAmount as Demand_Amount,(Booking.OutStandingAmt+TSPL_BOOKING_DETAIL.DocumentAmount) as OutStandingAmt,(Booking.OutStandingAmt) as Short,Booking.CreditLimit,Approval.* from ( select distinct TSPL_LOCATION_MASTER.Location_Code,TSPL_TRANSACTION_APPROVAL.Document_No,TSPL_TRANSACTION_APPROVAL.Approval_type ,TSPL_TRANSACTION_APPROVAL.Doc_Date, " &
                      " TSPL_BOOKING_DETAIL.Cust_Code as CustCode ,TSPL_BOOKING_DETAIL.Loc_Code as LocCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName," &
                      " TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL " &
                      " left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.document_no=TSPL_TRANSACTION_APPROVAL.Document_No  " &
                      " and TSPL_TRANSACTION_APPROVAL.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code  and TSPL_TRANSACTION_APPROVAL.Loc_Code=TSPL_BOOKING_DETAIL.Loc_Code   " &
                      " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code " &
                      " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BOOKING_DETAIL.Loc_Code  " &
                      " where  TSPL_TRANSACTION_APPROVAL.approve=0 and   TSPL_TRANSACTION_APPROVAL.program_code='BOOK-DS' " + WhrCustCategory + " ) as Approval " &
                      " inner join ( select TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Cust_Code," &
                      " TSPL_CUSTOMER_MASTER.Customer_Name,sum(TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_BOOKING_DETAIL.Item_Rate) as DocumentAmount from TSPL_BOOKING_DETAIL " &
                      " left join TSPL_CUSTOMER_MASTER on TSPL_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code group by TSPL_BOOKING_DETAIL.Document_No,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name) as  TSPL_BOOKING_DETAIL " &
                      " on Approval.Document_No=TSPL_BOOKING_DETAIL.Document_No and Approval.CustCode=TSPL_BOOKING_DETAIL.Cust_Code " &
                      " left join (" &
                      " select sum(case when RI=1 then -1 else 1  end *  OutStandingAmt) as OutStandingAmt,sum(case when RI=1 then -1 else 1  end *  Credit) as CreditLimit,Cust_Code from ( " &
                      " select SUM(isnull(TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_BOOKING_DETAIL.Item_Rate,0) ) as OutStandingAmt , 1 as RI,0 as Credit,Cust_Code from TSPL_BOOKING_DETAIL INNER JOIN TSPL_BOOKING_MATSER ON  TSPL_BOOKING_MATSER.dOCUMENT_nO=TSPL_BOOKING_DETAIL.Document_No where  TSPL_BOOKING_MATSER.From_Screen_Code<>'BOOK-DS_FSH'  group by TSPL_BOOKING_DETAIL.Cust_Code  " &
                      " union all " &
                      " select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI, " &
                      " isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as Credit,Customer_Code  from  TSPL_Customer_Invoice_Head " &
                      " left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  " &
                      " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " &
                      " where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  group by Customer_Code " &
                      " union all " &
                      " select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI,isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as Credit," &
                      " Cust_Code  from  TSPL_RECEIPT_HEADER where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' group by Cust_Code " &
                      " union all " &
                      " select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI,isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as credit,Cust_Code  " &
                      " from  TSPL_RECEIPT_HEADER where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' group by Cust_Code " &
                      " union all " &
                      " select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI,isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as Credit,Cust_Code  " &
                      " from  TSPL_RECEIPT_HEADER where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N' and TSPL_RECEIPT_HEADER.Receipt_No not in (Select tspl_receipt_header.receipt_no  from tspl_receipt_header where tspl_receipt_header.receipt_no in (select distinct Against_Receipt_No  from TSPL_BOOKING_PAYMENT_MODE_DETAIL ) and tspl_receipt_header.Receipt_Type ='P' and tspl_receipt_header.Posted ='Y' and isnull(tspl_receipt_header.IsChkReverse,'')='N'   ) group by Cust_Code " &
                      " union all " &
                      " select  sum(amount) as OutStandingAmt,-1 as RI,0 as Credit,vendor_code from TSPL_BANK_GUARANTEE_MASTER where Type='Customer'  " &
                      " and Bank_Guarantee_Type='RC' group by vendor_code " &
                      " union all " &
                      " select  sum(amount) as OutStandingAmt,1 as RI,0 as Credit,vendor_code from TSPL_BANK_GUARANTEE_MASTER where Type='Customer'  and Bank_Guarantee_Type='RT' group by vendor_code) xxx " &
                      " group by Cust_Code " &
                      " ) as Booking on   Booking.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code  and Approval.CustCode=Booking.Cust_Code where 1=1 " &
                      " ) z left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=z.CustCode where 1=1"

                Dim strCustCatforCurrentUser As String = ""

                strCustCatforCurrentUser = Xtra.CustomerCategory()
                If clsCommon.myLen(strCustCatforCurrentUser) > 0 Then
                    qry += " and isnull(TSPL_CUSTOMER_MASTER.customer_category,'') in (" + strCustCatforCurrentUser + ")"
                End If

                ''left join TSPL_BOOKING_DETAIL on Booking.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code
                ''and Approval.CustCode=Booking.Cust_Code

            End If
            If clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = " select TSPL_TRANSACTION_APPROVAL.Document_No,TSPL_TRANSACTION_APPROVAL.Approval_type ,TSPL_TRANSACTION_APPROVAL.Doc_Date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code as CustCode ,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.bill_to_location as LocCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName,TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.document_code=TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.bill_to_location where  TSPL_TRANSACTION_APPROVAL.approve=0 and   TSPL_TRANSACTION_APPROVAL.program_code='DEL-ORD-PS'"
            End If
            If clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = " select TSPL_TRANSACTION_APPROVAL.Document_No,TSPL_TRANSACTION_APPROVAL.Approval_type ,TSPL_TRANSACTION_APPROVAL.Doc_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as CustCode ,TSPL_SD_SHIPMENT_HEAD.bill_to_location as LocCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName,TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_HEAD.bill_to_location where  TSPL_TRANSACTION_APPROVAL.approve=0 and   TSPL_TRANSACTION_APPROVAL.program_code='SHIPMENT-PS'"
            End If

            If clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = " select TSPL_TRANSACTION_APPROVAL.Document_No ,TSPL_TRANSACTION_APPROVAL.Approval_type,TSPL_TRANSACTION_APPROVAL.Doc_Date,tspl_dispatch_bulksale.Customer_Code as CustCode ,tspl_dispatch_bulksale.location_code as LocCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName,TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL left outer join tspl_dispatch_bulksale on tspl_dispatch_bulksale.document_no=TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_dispatch_bulksale.Customer_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_dispatch_bulksale.location_code where  TSPL_TRANSACTION_APPROVAL.approve=0 and   TSPL_TRANSACTION_APPROVAL.program_code='DISPATCH-BS'"
            End If
            If clsCommon.CompairString(clsUserMgtCode.FrmLCCreation, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = "  select TSPL_TRANSACTION_APPROVAL.Document_No,TSPL_TRANSACTION_APPROVAL.Approval_type ,TSPL_TRANSACTION_APPROVAL.Doc_Date,tspl_lc_creation_MT.BenefecieryCode as CustCode ,tspl_lc_creation_MT.Location_Code as LocCode,tspl_vendor_master.vendor_name as CustName,TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL left outer join tspl_lc_creation_MT on tspl_lc_creation_MT.LCCreationNo=TSPL_TRANSACTION_APPROVAL.Document_No left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=tspl_lc_creation_MT.BenefecieryCode left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_lc_creation_MT.location_code where  TSPL_TRANSACTION_APPROVAL.approve=0 and TSPL_TRANSACTION_APPROVAL.program_code='LC-CREATION'"
            End If
            If clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                qry = "   select TSPL_TRANSACTION_APPROVAL.Document_No ,TSPL_TRANSACTION_APPROVAL.Approval_type ,TSPL_TRANSACTION_APPROVAL.Doc_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as CustCode ,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as LocCode,TSPL_CUSTOMER_MASTER.Customer_Name as CustName,TSPL_LOCATION_MASTER.Location_Desc as LocDesc   from TSPL_TRANSACTION_APPROVAL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_TRANSACTION_APPROVAL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location where  TSPL_TRANSACTION_APPROVAL.approve=0 and TSPL_TRANSACTION_APPROVAL.program_code='" + clsUserMgtCode.frmMCCMaterial + "'"
            End If
            Dim whrcls As String = ""
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                        whrcls = " and location_code in ( " & objCommonVar.strCurrUserLocations & ")"
                    Else
                        whrcls = " and TSPL_LOCATION_MASTER.location_code in ( " & objCommonVar.strCurrUserLocations & ")"
                    End If
                End If
            End If
            qry = qry & whrcls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FndDocumnetNo.Value = ""
                LblDocDate.Text = ""
                FndDocumnetNo.Enabled = False
                ddApprovalType.Text = ""
                loadBlankGridAll()
                Gv1.Visible = True
                For i As Integer = 0 To dt.Rows.Count - 1
                    Gv1.Rows.AddNew()
                    Gv1.Rows(i).Cells(colSelect).Value = False
                    Gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                    Gv1.Rows(i).Cells(colDocNo).Value = dt.Rows(i)("Document_No")
                    Gv1.Rows(i).Cells(colDocDate).Value = dt.Rows(i)("Doc_Date")
                    Gv1.Rows(i).Cells(colLocCode).Value = dt.Rows(i)("LocCode")
                    Gv1.Rows(i).Cells(colLocDesc).Value = dt.Rows(i)("LocDesc")
                    Gv1.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("CustCode")
                    Gv1.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("CustName")
                    Gv1.Rows(i).Cells(colApprovalType).Value = dt.Rows(i)("Approval_type")
                    If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                        Gv1.Rows(i).Cells(colDemandAmount).Value = clsCommon.myCdbl(dt.Rows(i)("Demand_Amount"))
                        Gv1.Rows(i).Cells(colAvailAmount).Value = clsCommon.myCdbl(dt.Rows(i)("OutStandingAmt"))
                        Gv1.Rows(i).Cells(colShortExcess).Value = clsCommon.myCdbl(dt.Rows(i)("Short"))
                        Gv1.Rows(i).Cells(colCreditLimit).Value = clsCommon.myCdbl(dt.Rows(i)("CreditLimit"))
                    End If
                    Gv1.Rows(i).Cells(colbtnCol).Value = "Click Here..."
                Next
            Else
                FndDocumnetNo.Enabled = True
                ddApprovalType.Text = ""
                Gv1.Visible = False
                clsCommon.MyMessageBoxShow("No Document Found to approve")
                btnReset.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(strTranAppPass, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            If clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                Dim obj1 As ClsTransactionApproval
                If Gv1.Rows.Count > 0 Then
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(i).Cells(colSelect).Value = True Then
                            obj1 = New ClsTransactionApproval
                            obj1.Form_ID = MyBase.Form_ID
                            obj1.Screen_Name = cmbScreenName.Text
                            obj1.Program_Code = cmbScreenName.SelectedValue
                            obj1.Document_No = Gv1.Rows(i).Cells(colSRNNo).Value
                            obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colSRNDATe).Value, "dd/MMM/yyyy")
                            obj1.Approval_Type = Gv1.Rows(i).Cells(colApprovalType).Value
                            obj1.Approval_Remarks = Gv1.Rows(i).Cells(colApprovalRemarks).Value

                            If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Cust_Code = Gv1.Rows(i).Cells(colVendorCode).Value
                            End If
                            '' create sms content
                            Dim dtSMSEmail As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,EMail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.FrmTransactionApproval + "'")
                            Dim strSMSContent As String = ""
                            Dim strEMailContent As String = ""
                            If dtSMSEmail.Rows.Count > 0 Then
                                strSMSContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("SMS_Text"))
                                strEMailContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("EMail_Text"))
                            End If

                            'SMSCode Start
                            If clsCommon.myLen(strSMSContent) > 0 Then
                                Dim objSMSH As New clsSMSHead()
                                objSMSH.SMS_Text = strSMSContent
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj1.Doc_Date, "dd/MMM/yyyy"))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, obj1.Document_No)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.DocAmount, clsCommon.myCdbl(Gv1.Rows(i).Cells(colDemandAmount).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BookingAmount, clsCommon.myCdbl(Gv1.Rows(i).Cells(colDemandAmount).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Code, clsCommon.myCstr(Gv1.Rows(i).Cells(colVendorCode).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(Gv1.Rows(i).Cells(colVendorDesc).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(Gv1.Rows(i).Cells(colLocCode).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(Gv1.Rows(i).Cells(colLocDesc).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_Type, "Approved")
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.AvailBalance, clsCommon.myCdbl(Gv1.Rows(i).Cells(colAvailAmount).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.ShortExcess, clsCommon.myCdbl(Gv1.Rows(i).Cells(colShortExcess).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.CreditLimit, clsCommon.myCdbl(Gv1.Rows(i).Cells(colCreditLimit).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_By, objCommonVar.CurrentUserCode)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_By_Name, objCommonVar.CurrentUser)
                                obj1.SMS_Content = objSMSH.SMS_Text
                            End If

                            'email content Start
                            If clsCommon.myLen(strEMailContent) > 0 Then
                                Dim objEmailH As New clsEMailHead
                                objEmailH.Email_Text = strEMailContent
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj1.Doc_Date, "dd/MMM/yyyy"))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, obj1.Document_No)
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.DocAmount, clsCommon.myCdbl(Gv1.Rows(i).Cells(colDemandAmount).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BookingAmount, clsCommon.myCdbl(Gv1.Rows(i).Cells(colDemandAmount).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Code, clsCommon.myCstr(Gv1.Rows(i).Cells(colVendorCode).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(Gv1.Rows(i).Cells(colVendorDesc).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(Gv1.Rows(i).Cells(colLocCode).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(Gv1.Rows(i).Cells(colLocDesc).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_Type, "Approved")
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.AvailBalance, clsCommon.myCdbl(Gv1.Rows(i).Cells(colAvailAmount).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.ShortExcess, clsCommon.myCdbl(Gv1.Rows(i).Cells(colShortExcess).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.CreditLimit, clsCommon.myCdbl(Gv1.Rows(i).Cells(colCreditLimit).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_By, objCommonVar.CurrentUserCode)
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_By_Name, objCommonVar.CurrentUser)
                                obj1.Email_Content = objEmailH.Email_Text
                            End If

                            Dim qry1 As Integer = clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Document_No='" + obj1.Document_No + "'")
                            If (qry1 = 0) Then
                                isnewentry = True
                            Else
                                isnewentry = False
                            End If
                            If (ClsTransactionApproval.SaveData(obj1, isnewentry)) Then
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                clsDBFuncationality.ExecuteNonQuery("update tspl_bulk_milk_srn set isApproved=1,Approved_Rate=" & clsCommon.myCdbl(Gv1.Rows(i).Cells(colNewBasicRate).Value) & " where SRN_NO='" & obj1.Document_No & "'")
                            End If
                        End If
                    Next
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    AddNew()
                Else
                    clsCommon.MyMessageBoxShow("No SRN Found To Approve", Me.Text)
                End If
                Exit Sub
            End If

            If clsCommon.CompairString(clsUserMgtCode.frmQualityCheck, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                Dim obj1 As ClsTransactionApproval
                If Gv1.Rows.Count > 0 Then
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(i).Cells(colSelect).Value = True Then
                            obj1 = New ClsTransactionApproval
                            obj1.Screen_Name = cmbScreenName.Text
                            obj1.Program_Code = cmbScreenName.SelectedValue
                            obj1.Document_No = Gv1.Rows(i).Cells(colQcNo).Value
                            obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colQcDate).Value, "dd/MMM/yyyy")
                            obj1.Approval_Type = Gv1.Rows(i).Cells(colApprovalType).Value
                            Dim qry1 As Integer = clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Document_No='" + obj1.Document_No + "'")
                            If (qry1 = 0) Then
                                isnewentry = True
                            Else
                                isnewentry = False
                            End If
                            If (ClsTransactionApproval.SaveData(obj1, isnewentry)) Then
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                clsDBFuncationality.ExecuteNonQuery("update tspl_quality_check set is_Param_Accepted=2,DeductionAmount=" & clsCommon.myCdbl(Gv1.Rows(i).Cells(colSpclDeductionAmt).Value) & ",isPosted=1 where QC_No='" & obj1.Document_No & "'")
                                If clsCommon.CompairString(obj1.Approval_Type, "Special Approval") = CompairStringResult.Equal Then
                                    obj1.Approval_Type = "Special Deduction"
                                    ClsTransactionApproval.SaveData(obj1, True)
                                End If
                            End If
                        End If
                    Next
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    AddNew()
                Else
                    clsCommon.MyMessageBoxShow("No QC Found To Approve", Me.Text)
                End If
                Exit Sub
            End If

            If clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                Dim obj1 As ClsTransactionApproval
                If Gv1.Rows.Count > 0 Then
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(i).Cells(colSelect).Value = True Then
                            obj1 = New ClsTransactionApproval
                            obj1.Screen_Name = cmbScreenName.Text
                            obj1.Program_Code = cmbScreenName.SelectedValue
                            obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                            obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            obj1.Approval_Type = Gv1.Rows(i).Cells(colApprovalType).Value
                            Dim qry1 As Integer = clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Document_No='" + obj1.Document_No + "'")
                            If (qry1 = 0) Then
                                isnewentry = True
                            Else
                                isnewentry = False
                            End If
                            If (ClsTransactionApproval.SaveData(obj1, isnewentry)) Then
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                'clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SHIPMENT_HEAD set Status=1 where Document_Code='" & obj1.Document_No & "'")
                                'clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SALE_INVOICE_HEAD set Status=1 WHERE Document_Code=(select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD WHERE Document_Code='" & obj1.Document_No & "')")
                            End If
                        End If
                    Next
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    AddNew()
                Else
                    clsCommon.MyMessageBoxShow("No Data Found To Approve", Me.Text)
                End If
                Exit Sub
            End If

            If clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, cmbScreenName.SelectedValue) = CompairStringResult.Equal Or clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, cmbScreenName.SelectedValue) = CompairStringResult.Equal Or clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Or clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Or clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Or clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Or clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Or clsCommon.CompairString(clsUserMgtCode.FrmLCCreation, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                Dim obj1 As ClsTransactionApproval
                If Gv1.Rows.Count > 0 Then
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(i).Cells(colSelect).Value = True Then
                            obj1 = New ClsTransactionApproval
                            obj1.Screen_Name = cmbScreenName.Text
                            obj1.Program_Code = cmbScreenName.SelectedValue
                            obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                            obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            obj1.Approval_Type = Gv1.Rows(i).Cells(colApprovalType).Value
                            Dim qry1 As Integer = clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Document_No='" + obj1.Document_No + "'")
                            If (qry1 = 0) Then
                                isnewentry = True
                            Else
                                isnewentry = False
                            End If
                            If (ClsTransactionApproval.SaveData(obj1, isnewentry)) Then
                                If clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SALE_INVOICE_HEAD set Is_Approved=1 where Document_code='" + obj1.Document_No + "' and trans_type='CSA'")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                End If
                                If clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                End If
                                If clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set Is_Credit_Approved=1,status=3 where Document_No='" + obj1.Document_No + "'")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DO_Posted=3 where Delivery_No='" + obj1.Document_No + "'")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                End If
                                If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set Booking_Status=3 where Document_No='" + obj1.Document_No + "' and cust_code='" & Gv1.Rows(i).Cells(colVendorCode).Value & "' and Loc_Code='" & Gv1.Rows(i).Cells(colLocCode).Value & "'")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "' and cust_code='" & Gv1.Rows(i).Cells(colVendorCode).Value & "' and Loc_Code='" & Gv1.Rows(i).Cells(colLocCode).Value & "'")
                                End If
                                If clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE set Is_Credit_Approved=1,status=3 where Document_Code='" + obj1.Document_No + "'")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                End If
                                If clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SHIPMENT_HEAD set Is_Advance_Approved=1 where Document_Code='" + obj1.Document_No + "'")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                End If

                                If clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_Dispatch_BulkSale set Approved='Y',Status='Approved' where Document_No='" & obj1.Document_No & "'")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                End If
                                If clsCommon.CompairString(clsUserMgtCode.FrmLCCreation, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update tspl_lc_creation_MT set Approved='Y',Status='Approved' where LCCreationNo='" & obj1.Document_No & "'")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                End If
                            End If
                        End If
                    Next
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    AddNew()
                Else
                    clsCommon.MyMessageBoxShow("No Document Found To Approve", Me.Text)
                End If
                Exit Sub
            End If
            If clsCommon.CompairString(clsUserMgtCode.frmDemandBooking, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                Dim obj1 As ClsTransactionApproval
                If Gv1.Rows.Count > 0 Then
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(i).Cells(colSelect).Value = True Then
                            obj1 = New ClsTransactionApproval
                            obj1.Screen_Name = cmbScreenName.Text
                            obj1.Program_Code = cmbScreenName.SelectedValue
                            obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                            obj1.Cust_Code = Gv1.Rows(i).Cells(colVendorCode).Value
                            obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            obj1.Approval_Type = Gv1.Rows(i).Cells(colApprovalType).Value
                            Dim qry1 As Integer = clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Document_No='" + obj1.Document_No + "'")
                            If (qry1 = 0) Then
                                isnewentry = True
                            Else
                                isnewentry = False
                            End If
                            If (ClsTransactionApproval.SaveData(obj1, isnewentry)) Then
                                If clsCommon.CompairString(clsUserMgtCode.frmDemandBooking, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_DEMAND_BOOKING_MASTER set Posted=1 where Document_No='" + obj1.Document_No + "' ")
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj1.Document_No + "'")
                                End If

                            End If
                        End If
                    Next
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    AddNew()
                Else
                    clsCommon.MyMessageBoxShow("No Document Found To Approve", Me.Text)
                End If
                Exit Sub
            End If
            Dim obj As New ClsTransactionApproval()
            obj.Screen_Name = cmbScreenName.Text
            obj.Program_Code = cmbScreenName.SelectedValue
            obj.Document_No = FndDocumnetNo.Value
            obj.Doc_Date = LblDocDate.Text
            obj.Approval_Type = ddApprovalType.Text
            Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Document_No='" + obj.Document_No + "'")
            If (qry = 0) Then
                isnewentry = True
            Else
                isnewentry = False
            End If
            If (ClsTransactionApproval.SaveData(obj, isnewentry)) Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='1' where Document_No='" + obj.Document_No + "'")
                ' If clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE set Is_Credit_Approved=1,status=3 where Document_Code='" + obj.Document_No + "'")
                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SHIPMENT_HEAD set Is_Advance_Approved=1 where Document_Code='" + obj.Document_No + "'")
                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set Is_Credit_Approved=1,status=3 where Document_No='" + obj.Document_No + "'")
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set DO_Posted=3 where Delivery_No='" + obj.Document_No + "'")
                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SALE_INVOICE_HEAD set Is_Approved=1 where Document_code='" + obj.Document_No + "' and trans_type='CSA'")
                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                'clsDBFuncationality.ExecuteNonQuery("update tspl_bulk_milk_srn set isApproved=1 where SRN_NO='" & obj.Document_No & "'")
                ''richa 31/12/2014
                'ElseIf clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_Dispatch_BulkSale set Approved='Y',Status='Approved' where Document_No='" & obj.Document_No & "'")

                ''------------
            End If
            clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
            AddNew()
            'End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub Unapprove()

        If clsCommon.myLen(cmbScreenName.SelectedValue) > 0 AndAlso clsCommon.CompairString(cmbScreenName.SelectedValue, "Select") <> CompairStringResult.Equal Then
            Dim obj1 As ClsTransactionApproval
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
            Try
                If Gv1.Rows.Count > 0 Then
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        If Gv1.Rows(i).Cells(colSelect).Value = True Then
                            obj1 = New ClsTransactionApproval
                            obj1.Screen_Name = cmbScreenName.Text
                            obj1.Program_Code = cmbScreenName.SelectedValue
                            If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Cust_Code = Gv1.Rows(i).Cells(colVendorCode).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.FrmLCCreation, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmQualityCheck, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colQcNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colQcDate).Value, "dd/MMM/yyyy")
                                'obj1.Approval_Remarks = Gv1.Rows(i).Cells(colRemarks).Value
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmMilkReceipt, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colSRNNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colSRNDATe).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmVSPItemIssue, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                            ElseIf clsCommon.CompairString(clsUserMgtCode.frmDemandBooking, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                obj1.Screen_Name = cmbScreenName.Text
                                obj1.Program_Code = cmbScreenName.SelectedValue
                                obj1.Document_No = Gv1.Rows(i).Cells(colDocNo).Value
                                obj1.Cust_Code = Gv1.Rows(i).Cells(colVendorCode).Value
                                obj1.Doc_Date = clsCommon.GetPrintDate(Gv1.Rows(i).Cells(colDocDate).Value, "dd/MMM/yyyy")
                                obj1.Approval_Type = Gv1.Rows(i).Cells(colApprovalType).Value


                            End If

                            obj1.Approval_Type = ddApprovalType.Text

                            'Dim objEmail As New clsEMailHead
                            'objEmail.Table()
                            '' create sms content
                            Dim dtSMSEmail As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,EMail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.FrmTransactionApproval + "'", trans)
                            Dim strSMSContent As String = ""
                            Dim strEMailContent As String = ""
                            If dtSMSEmail.Rows.Count > 0 Then
                                strSMSContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("SMS_Text"))
                                strEMailContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("EMail_Text"))
                            End If
                            'SMSCode Start

                            If clsCommon.myLen(strSMSContent) > 0 Then
                                Dim objSMSH As New clsSMSHead()
                                objSMSH.SMS_Text = strSMSContent
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj1.Doc_Date, "dd/MMM/yyyy"))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, obj1.Document_No)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.DocAmount, clsCommon.myCdbl(Gv1.Rows(i).Cells(colDemandAmount).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BookingAmount, clsCommon.myCdbl(Gv1.Rows(i).Cells(colDemandAmount).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Code, clsCommon.myCstr(Gv1.Rows(i).Cells(colVendorCode).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(Gv1.Rows(i).Cells(colVendorDesc).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(Gv1.Rows(i).Cells(colLocCode).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(Gv1.Rows(i).Cells(colLocDesc).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_Type, "Rejected")
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.AvailBalance, clsCommon.myCdbl(Gv1.Rows(i).Cells(colAvailAmount).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.ShortExcess, clsCommon.myCdbl(Gv1.Rows(i).Cells(colShortExcess).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.CreditLimit, clsCommon.myCdbl(Gv1.Rows(i).Cells(colCreditLimit).Value))
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_By, objCommonVar.CurrentUserCode)
                                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_By_Name, objCommonVar.CurrentUser)
                                obj1.SMS_Content = objSMSH.SMS_Text
                            End If

                            'email content Start
                            If clsCommon.myLen(strEMailContent) > 0 Then
                                Dim objEmailH As New clsEMailHead
                                objEmailH.Email_Text = strEMailContent
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj1.Doc_Date, "dd/MMM/yyyy"))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, obj1.Document_No)
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.DocAmount, clsCommon.myCdbl(Gv1.Rows(i).Cells(colDemandAmount).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BookingAmount, clsCommon.myCdbl(Gv1.Rows(i).Cells(colDemandAmount).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Code, clsCommon.myCstr(Gv1.Rows(i).Cells(colVendorCode).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(Gv1.Rows(i).Cells(colVendorDesc).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(Gv1.Rows(i).Cells(colLocCode).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(Gv1.Rows(i).Cells(colLocDesc).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_Type, "Rejected")
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.AvailBalance, clsCommon.myCdbl(Gv1.Rows(i).Cells(colAvailAmount).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.ShortExcess, clsCommon.myCdbl(Gv1.Rows(i).Cells(colShortExcess).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.CreditLimit, clsCommon.myCdbl(Gv1.Rows(i).Cells(colCreditLimit).Value))
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_By, objCommonVar.CurrentUserCode)
                                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_By_Name, objCommonVar.CurrentUser)
                                obj1.Email_Content = objEmailH.Email_Text
                            End If
                            Dim qry1 As Integer = clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_TRANSACTION_APPROVAL where Document_No='" + obj1.Document_No + "'", trans)
                            If (qry1 = 0) Then
                                isnewentry = True
                            Else
                                isnewentry = False
                            End If
                            If (ClsTransactionApproval.SaveData(obj1, isnewentry, trans)) Then
                                If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "' and Cust_Code='" & Gv1.Rows(i).Cells(colVendorCode).Value & "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_BOOKING_DETAIL set Booking_Status=5 where Document_No='" & obj1.Document_No & "' and Cust_Code='" & Gv1.Rows(i).Cells(colVendorCode).Value & "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SALE_INVOICE_HEAD set Is_Approved=2 where Document_code='" + obj1.Document_No + "' and trans_type='CSA'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set Is_Credit_Approved=2,status=5 where Document_No='" + obj1.Document_No + "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE set Is_Credit_Approved=2,status=5 where Document_Code='" + obj1.Document_No + "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_Dispatch_BulkSale set Approved='N',Status='UnApproved' where Document_No='" & obj1.Document_No & "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.FrmLCCreation, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update tspl_lc_creation_MT set Approved='N',Status='UnApproved' where LCCreationNo='" & obj1.Document_No & "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmQualityCheck, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update tspl_quality_check set is_Param_Accepted=0,DeductionAmount=0,isPosted=0 where QC_No='" & obj1.Document_No & "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmMilkReceipt, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update tspl_bulk_milk_srn set isApproved=2 where SRN_NO='" & obj1.Document_No & "'", trans)

                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SHIPMENT_HEAD set Is_Advance_Approved=2 where Document_Code='" + obj1.Document_No + "'", trans)
                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SD_SHIPMENT_HEAD set Is_Advance_Approved=2 where Document_Code='" + obj1.Document_No + "'", trans)
                                    'ElseIf clsCommon.CompairString(clsUserMgtCode.frmVSPItemIssue, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                ElseIf clsCommon.CompairString(clsUserMgtCode.frmDemandBooking, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSACTION_APPROVAL set Approve='2' where Document_No='" + obj1.Document_No + "'", trans)
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_DEMAND_BOOKING_MASTER set Posted=2 where Document_No='" + obj1.Document_No + "'", trans)

                                End If

                            End If
                        End If
                    Next
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    AddNew()
                Else
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("No QC Found To Unpprove", Me.Text)
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try

        End If

    End Sub
    Sub LoadDocCode(ByVal strcode As String)
        Try
            Dim obj As ClsTransactionApproval = ClsTransactionApproval.getdata(strcode)
            If obj IsNot Nothing Then

                cmbScreenName.Text = obj.Screen_Name
                cmbScreenName.SelectedValue = obj.Program_Code
                FndDocumnetNo.Value = obj.Document_No
                LblDocDate.Text = obj.Doc_Date
                ddApprovalType.Text = obj.Approval_Type

            End If
            'FndDocumnetNo.Enabled = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub AddNew()
        ' cmbScreenName.Text = ""
        FndDocumnetNo.Value = ""
        LblDocDate.Text = ""
        ddApprovalType.Text = "Rate"
        LoadScreenName()
        FndDocumnetNo.Enabled = True
        isInsideLoadData = False
        cmbScreenName.SelectedIndex = 0
        cmbScreenName.Text = "Select"
        ' ddApprovalType.SelectedIndex = 0
        Gv1.Visible = False
        GridQC.Visible = False
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Hidden
        lblQCNo.Text = ""
        lblQCDate.Text = ""
    End Sub

    Private Sub cmbScreenName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbScreenName.SelectedIndexChanged
        Dim strcode As String = cmbScreenName.Text
        If isInsideLoadData = False Then
            If clsCommon.CompairString(clsUserMgtCode.frmQualityCheck, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                isInsideLoadData = True
                loadUnapprovedQcDataForBulkMilkProcData()
                isInsideLoadData = False
                Exit Sub
            ElseIf clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                isInsideLoadData = True
                LoadSRNDATA()
                isInsideLoadData = False
                Exit Sub
            ElseIf clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                isInsideLoadData = True
                loadGridDataAll()
                isInsideLoadData = False
                Exit Sub

            ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, cmbScreenName.SelectedValue) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, cmbScreenName.SelectedValue) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal OrElse clsCommon.CompairString(clsUserMgtCode.FrmLCCreation, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                isInsideLoadData = True
                loadGridDataAll()
                isInsideLoadData = False
                If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                End If
                Exit Sub
            ElseIf clsCommon.CompairString(clsUserMgtCode.frmDemandBooking, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                isInsideLoadData = True
                loadGridDataAll()
                isInsideLoadData = False
                Exit Sub
            Else
                FndDocumnetNo.Value = ""
                LblDocDate.Text = ""
                FndDocumnetNo.Enabled = True
                ddApprovalType.Text = ""
                Gv1.Visible = False
                GridQC.Visible = False
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Hidden
            End If
        End If
        If isInsideLoadData = False Then
            LoadDocCode(strcode)
        End If

    End Sub

    Private Sub FndDocumnetNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndDocumnetNo._MYValidating
        Dim qry As String = "select Document_No from TSPL_TRANSACTION_APPROVAL"

        FndDocumnetNo.Value = clsCommon.ShowSelectForm("screen", qry, "Document_No", " program_code='" & cmbScreenName.SelectedValue & "' and approve='0'", FndDocumnetNo.Value, "TSPL_TRANSACTION_APPROVAL.Document_No", isButtonClicked)

        If clsCommon.myLen(FndDocumnetNo.Value) > 0 Then

            LblDocDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Doc_Date  FROM TSPL_TRANSACTION_APPROVAL Where Document_No='" + FndDocumnetNo.Value + "'"))
            ddApprovalType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Approval_Type  FROM TSPL_TRANSACTION_APPROVAL Where Document_No='" + FndDocumnetNo.Value + "'"))
        Else

            LblDocDate.Text = ""

        End If
    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        If clsCommon.CompairString(cmbScreenName.Text, "Select") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Please Select At least one document.")
            Exit Sub
        End If
        Dim rowsCount As Integer = 0
        Dim ExtraMsg As String = ""
        If Gv1.Visible = True And Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    rowsCount = rowsCount + 1
                End If
            Next
            If rowsCount > 0 Then
                ExtraMsg = Environment.NewLine & "you are going to approve  total " & rowsCount & " " & cmbScreenName.Text & " Document(s)"
            Else
                ExtraMsg = ""
            End If
        End If
        If clsCommon.MyMessageBoxShow("Are you sure to approve selected document(s)." & ExtraMsg, Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            SaveData()
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        AddNew()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
        'Try
        '    'Dim objEmailH As New clsEMailHead
        '    'objEmailH.Email_Subject = "AAAAAAAAAAAAAA Subject"
        '    'objEmailH.Email_Text = "AAAAAAAAAAAAAA Body"
        '    'objEmailH.Attachment_1_Path = "D:\BAL\SpreadExport\app.config"
        '    'objEmailH.Attachment_2_Path = "C:\Users\tecxpert\Desktop\11\Balwinder.doc"
        '    'objEmailH.arrEMail = New List(Of String)
        '    'objEmailH.arrEMail.Add("balwinder06@gmail.com")
        '    'objEmailH.arrEMail.Add("balwinder.singh@tecxpert.in")
        '    'objEmailH.SaveData(Me.Form_ID, objEmailH, Nothing)

        '    Dim objEmailH As New clsSMSHead
        '    objEmailH.SMS_Text = "asfklasjlfasdfdsf"
        '    objEmailH.arrMobilNo = New List(Of String)
        '    objEmailH.arrMobilNo.Add("9876543210")
        '    objEmailH.SaveData(Me.Form_ID, objEmailH, Nothing)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try

    End Sub


    Sub loadBlankGrid()
        Try
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Dim colChkBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            colChkBox.HeaderText = "Select "
            colChkBox.Name = colSelect
            colChkBox.ReadOnly = False
            colChkBox.Width = 50
            colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(colChkBox)

            Dim repoSLNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "SL.No"
            repoSLNo.Name = colSlNo
            repoSLNo.Width = 60
            repoSLNo.ReadOnly = True
            repoSLNo.BestFit()
            Gv1.MasterTemplate.Columns.Add(repoSLNo)

            repoSLNo = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "Approval Type"
            repoSLNo.Name = colApprovalType
            repoSLNo.Width = 100
            repoSLNo.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSLNo)

            Dim repoTnkrNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTnkrNo.FormatString = ""
            repoTnkrNo.HeaderText = "Tanker No"
            repoTnkrNo.Name = colTankerNo
            repoTnkrNo.Width = 100
            repoTnkrNo.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoTnkrNo)

            Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "SRN No"
            repoSRNNO.Name = colSRNNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)

            Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoSRNDate.FormatString = "{0:d}"
            repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
            repoSRNDate.HeaderText = "SRN Date"
            repoSRNDate.Name = colSRNDATe
            repoSRNDate.Width = 100
            repoSRNDate.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNDate)

            repoSRNNO = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "Gate Entry No"
            repoSRNNO.Name = colGateEntryNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)


            repoSRNNO = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "Modify By"
            repoSRNNO.Name = colModifyBy
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)

            Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLCode.FormatString = ""
            repoLCode.HeaderText = "Loc Code"
            repoLCode.Name = colLocCode
            repoLCode.Width = 100
            repoLCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLCode)



            Dim repoLDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLDesc.FormatString = ""
            repoLDesc.HeaderText = "Loc Desc"
            repoLDesc.Name = colLocDesc
            repoLDesc.Width = 100
            repoLDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLDesc)


            Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVCode.FormatString = ""
            repoVCode.HeaderText = "Vendor Code"
            repoVCode.Name = colVendorCode
            repoVCode.Width = 100
            repoVCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVCode)

            Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVDesc.FormatString = ""
            repoVDesc.HeaderText = "Vendor Desc"
            repoVDesc.Name = colVendorDesc
            repoVDesc.Width = 100
            repoVDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVDesc)

            Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoICode.FormatString = ""
            repoICode.HeaderText = "Item Code"
            repoICode.Name = colItemCode
            repoICode.Width = 100
            repoICode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoICode)

            Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoIName.FormatString = ""
            repoIName.HeaderText = "Item Desc"
            repoIName.Name = colItemDesc
            repoIName.Width = 180
            repoIName.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoIName)


            Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoUOM.FormatString = ""
            repoUOM.HeaderText = "UOM"
            repoUOM.Name = colUOM
            repoUOM.ReadOnly = True
            repoUOM.Width = 80
            repoUOM.WrapText = True
            repoUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoUOM)



            Dim repoNetWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoNetWeight.FormatString = ""
            repoNetWeight.HeaderText = "Net Weight"
            repoNetWeight.Name = colNetWeight
            repoNetWeight.ReadOnly = True
            repoNetWeight.Width = 100
            repoNetWeight.WrapText = True
            repoNetWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoNetWeight)



            Dim repoFatPer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoFatPer.FormatString = ""
            repoFatPer.HeaderText = "FAT(%)"
            repoFatPer.Name = colFat
            repoFatPer.ReadOnly = True
            repoFatPer.Width = 80
            repoFatPer.WrapText = True
            'repoFatPer.IsVisible = False
            repoFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoFatPer)

            Dim repoSNFPer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSNFPer.FormatString = ""
            repoSNFPer.HeaderText = "SNF(%)"
            repoSNFPer.Name = colSNF
            repoSNFPer.ReadOnly = True
            repoSNFPer.Width = 80
            repoSNFPer.WrapText = True
            'repoSNFPer.IsVisible = False
            repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoSNFPer)


            Dim repoFATKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoFATKG.FormatString = ""
            repoFATKG.HeaderText = "FAT(KG)"
            repoFATKG.Name = colFatKG
            repoFATKG.ReadOnly = True
            repoFATKG.Width = 80
            repoFATKG.WrapText = True
            repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoFATKG)

            Dim repoSNFKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSNFKG.FormatString = ""
            repoSNFKG.HeaderText = "SNF(KG)"
            repoSNFKG.Name = colSNFKG
            repoSNFKG.ReadOnly = True
            repoSNFKG.Width = 80
            repoSNFKG.WrapText = True
            repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoSNFKG)


            Dim repoAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoAmount.FormatString = ""
            repoAmount.HeaderText = "Basic Rate"
            repoAmount.Name = colBasicRate
            repoAmount.ReadOnly = True
            repoAmount.Width = 100
            repoAmount.WrapText = True
            repoAmount.IsVisible = True
            repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoAmount)

            repoAmount = New GridViewTextBoxColumn()
            repoAmount.FormatString = ""
            repoAmount.HeaderText = " New Basic Rate"
            repoAmount.Name = colNewBasicRate
            repoAmount.ReadOnly = False
            repoAmount.Width = 100
            repoAmount.WrapText = True
            repoAmount.IsVisible = True
            repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoAmount)

            Dim repoDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoDeduc.FormatString = ""
            repoDeduc.HeaderText = "Deduction"
            repoDeduc.Name = colDeduc
            repoDeduc.ReadOnly = True
            repoDeduc.Width = 80
            repoDeduc.WrapText = True
            repoDeduc.IsVisible = True
            repoDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoDeduc)

            Dim repoIncen As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoIncen.FormatString = ""
            repoIncen.HeaderText = "Incentive"
            repoIncen.Name = colIncen
            repoIncen.ReadOnly = True
            repoIncen.Width = 80
            repoIncen.WrapText = True
            repoIncen.IsVisible = True
            repoIncen.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoIncen)

            Dim repoSpclDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSpclDeduc.FormatString = ""
            repoSpclDeduc.HeaderText = "Special Deduction"
            repoSpclDeduc.Name = colSpecialDeduc
            repoSpclDeduc.ReadOnly = True
            repoSpclDeduc.Width = 80
            repoSpclDeduc.WrapText = True
            repoSpclDeduc.IsVisible = True
            repoSpclDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoSpclDeduc)


            Dim repoActAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoActAmt.FormatString = ""
            repoActAmt.HeaderText = "Net Rate"
            repoActAmt.Name = colNetRate
            repoActAmt.ReadOnly = True
            repoActAmt.Width = 80
            repoActAmt.WrapText = True
            repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoActAmt)

            repoActAmt = New GridViewTextBoxColumn()
            repoActAmt.FormatString = ""
            repoActAmt.HeaderText = "New Net Rate"
            repoActAmt.Name = colNewNetRate
            repoActAmt.ReadOnly = True
            repoActAmt.Width = 80
            repoActAmt.WrapText = True
            repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoActAmt)

            Dim repoAppRemark As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoAppRemark.FormatString = ""
            repoAppRemark.HeaderText = "Remark"
            repoAppRemark.Name = colApprovalRemarks
            repoAppRemark.Width = 100
            repoAppRemark.ReadOnly = False
            Gv1.MasterTemplate.Columns.Add(repoAppRemark)

            Dim RepobtnCol As GridViewCommandColumn = New GridViewCommandColumn()
            RepobtnCol.HeaderText = "Details "
            RepobtnCol.Name = colbtnCol
            RepobtnCol.ReadOnly = False
            RepobtnCol.Width = 150
            RepobtnCol.DefaultText = "Click Here..."
            RepobtnCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(RepobtnCol)

            Dim RepobtnQC As GridViewCommandColumn = New GridViewCommandColumn()
            RepobtnQC.HeaderText = "QC Details "
            RepobtnQC.Name = colbtnQC
            RepobtnQC.ReadOnly = False
            RepobtnQC.Width = 150
            RepobtnQC.DefaultText = "QC Detail"
            RepobtnQC.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(RepobtnQC)

            Gv1.AllowAddNewRow = False
            Gv1.AllowColumnChooser = True
            Gv1.ShowGroupPanel = False
            Gv1.AllowColumnReorder = True
            Gv1.AllowRowReorder = True
            Gv1.EnableSorting = True
            Gv1.MasterTemplate.ShowRowHeaderColumn = False
            Gv1.MasterTemplate.ShowColumnHeaders = True
            Gv1.EnableAlternatingRowColor = True
            Gv1.TableElement.TableHeaderHeight = 40
            Gv1.EnableFiltering = True
            ' Gv1.BestFitColumns()
            Gv1.AutoSizeRows = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub loadBlankQCGrid()
        Try
            GridQC.Rows.Clear()
            GridQC.Columns.Clear()

            Dim repoQCParmetterNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoQCParmetterNo.FormatString = ""
            repoQCParmetterNo.HeaderText = "QC Parameter"
            repoQCParmetterNo.Name = colParNo
            repoQCParmetterNo.Width = 100
            repoQCParmetterNo.ReadOnly = True
            repoQCParmetterNo.BestFit()
            GridQC.MasterTemplate.Columns.Add(repoQCParmetterNo)

            Dim repoQCParmetterValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoQCParmetterValue.FormatString = ""
            repoQCParmetterValue.HeaderText = "QC Value"
            repoQCParmetterValue.Name = colParValue
            repoQCParmetterValue.Width = 100
            repoQCParmetterValue.ReadOnly = True
            repoQCParmetterValue.BestFit()
            GridQC.MasterTemplate.Columns.Add(repoQCParmetterValue)

            GridQC.AllowAddNewRow = False
            GridQC.AllowColumnChooser = True
            GridQC.ShowGroupPanel = False
            GridQC.AllowColumnReorder = True
            GridQC.AllowRowReorder = True
            GridQC.EnableSorting = True
            GridQC.MasterTemplate.ShowRowHeaderColumn = False
            GridQC.MasterTemplate.ShowColumnHeaders = True
            GridQC.EnableAlternatingRowColor = True
            GridQC.TableElement.TableHeaderHeight = 40
            GridQC.EnableFiltering = True

            GridQC.AutoSizeRows = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub loadBlankGridAll()
        Try
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Dim colChkBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            colChkBox.HeaderText = "Select "
            colChkBox.Name = colSelect
            colChkBox.ReadOnly = False
            colChkBox.Width = 50
            colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(colChkBox)

            Dim repoSLNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "SL.No"
            repoSLNo.Name = colSlNo
            repoSLNo.Width = 60
            repoSLNo.ReadOnly = True
            repoSLNo.BestFit()
            Gv1.MasterTemplate.Columns.Add(repoSLNo)

            repoSLNo = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "Approval Type"
            repoSLNo.Name = colApprovalType
            repoSLNo.Width = 100
            repoSLNo.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSLNo)

            Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "Doc No"
            repoSRNNO.Name = colDocNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)

            Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoSRNDate.FormatString = "{0:d}"
            repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
            repoSRNDate.HeaderText = "Doc Date"
            repoSRNDate.Name = colDocDate
            repoSRNDate.Width = 100
            repoSRNDate.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNDate)


            Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLCode.FormatString = ""
            repoLCode.HeaderText = "Loc Code"
            repoLCode.Name = colLocCode
            repoLCode.Width = 100
            repoLCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLCode)



            Dim repoLDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLDesc.FormatString = ""
            repoLDesc.HeaderText = "Loc Desc"
            repoLDesc.Name = colLocDesc
            repoLDesc.Width = 100
            repoLDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLDesc)


            Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVCode.FormatString = ""
            repoVCode.HeaderText = "Vendor/Customer Code"
            repoVCode.Name = colVendorCode
            repoVCode.Width = 100
            repoVCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVCode)

            Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVDesc.FormatString = ""
            repoVDesc.HeaderText = "Vendor/Customer Desc"
            repoVDesc.Name = colVendorDesc
            repoVDesc.Width = 100
            repoVDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVDesc)

            Dim repoAppRemark As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoAppRemark.FormatString = ""
            repoAppRemark.HeaderText = "Remark"
            repoAppRemark.Name = colApprovalRemarks
            repoAppRemark.Width = 100
            repoAppRemark.ReadOnly = False
            Gv1.MasterTemplate.Columns.Add(repoAppRemark)

            '' for dairy booking only
            If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                Dim repoDemAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoDemAmt.FormatString = ""
                repoDemAmt.HeaderText = "Demand Amount"
                repoDemAmt.Name = colDemandAmount
                repoDemAmt.Width = 100
                repoDemAmt.ReadOnly = True
                Gv1.MasterTemplate.Columns.Add(repoDemAmt)

                Dim repoAvailBal As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoAvailBal.FormatString = ""
                repoAvailBal.HeaderText = "Available Balance"
                repoAvailBal.Name = colAvailAmount
                repoAvailBal.Width = 100
                repoAvailBal.ReadOnly = True
                Gv1.MasterTemplate.Columns.Add(repoAvailBal)

                Dim repoShort As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoShort.FormatString = ""
                repoShort.HeaderText = "Short/Excess"
                repoShort.Name = colShortExcess
                repoShort.Width = 100
                repoShort.ReadOnly = True
                Gv1.MasterTemplate.Columns.Add(repoShort)

                Dim repoCreditLimit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoCreditLimit.FormatString = ""
                repoCreditLimit.HeaderText = "Credit Limit"
                repoCreditLimit.Name = colCreditLimit
                repoCreditLimit.Width = 100
                repoCreditLimit.ReadOnly = True
                Gv1.MasterTemplate.Columns.Add(repoCreditLimit)
            End If
           
            Dim RepobtnCol As GridViewCommandColumn = New GridViewCommandColumn()
            RepobtnCol.HeaderText = "Detail"
            RepobtnCol.Name = colbtnCol
            RepobtnCol.ReadOnly = False
            RepobtnCol.Width = 150
            RepobtnCol.DefaultText = "Click Here..."
            RepobtnCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(RepobtnCol)


            Gv1.AllowAddNewRow = False
            Gv1.AllowColumnChooser = True
            Gv1.ShowGroupPanel = False
            Gv1.AllowColumnReorder = True
            Gv1.AllowRowReorder = True
            Gv1.EnableSorting = True
            Gv1.MasterTemplate.ShowRowHeaderColumn = False
            Gv1.MasterTemplate.ShowColumnHeaders = True
            Gv1.EnableAlternatingRowColor = True
            Gv1.TableElement.TableHeaderHeight = 40
            Gv1.EnableFiltering = True
            'Gv1.BestFitColumns()
            Gv1.AutoSizeRows = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadSRNDATA()
        Try
            Dim qry As String = "select TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_TRANSACTION_APPROVAL.Approval_type,TSPL_Bulk_MILK_SRN.tanker_No,TSPL_Bulk_MILK_SRN.gate_entry_no as [Gate Entry No],TSPL_Bulk_MILK_SRN.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name  ,TSPL_Bulk_MILK_SRN.SRN_Date,TSPL_Bulk_MILK_SRN.Item_Code,TSPL_Bulk_MILK_SRN.Item_Desc,TSPL_Bulk_MILK_SRN.UOM,TSPL_Bulk_MILK_SRN.Net_Weight,TSPL_Bulk_MILK_SRN.fat_per,TSPL_Bulk_MILK_SRN.snf_Per,TSPL_Bulk_MILK_SRN.fat_KG,TSPL_Bulk_MILK_SRN.SNF_KG,TSPL_Bulk_MILK_SRN.BasicRate,TSPL_Bulk_MILK_SRN.NetRate,TSPL_Bulk_MILK_SRN.Incentive,TSPL_Bulk_MILK_SRN.Deduction,TSPL_QUALITY_CHECK.DeductionAmount,TSPL_Bulk_MILK_SRN.modify_by    from tspl_transaction_approval left outer join TSPL_Bulk_MILK_SRN on tspl_transaction_approval.document_no=tspl_bulk_milk_srn.Srn_no  left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No=TSPL_Bulk_MILK_SRN.QC_No  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Bulk_MILK_SRN.Loc_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Bulk_MILK_SRN.Vendor_Code   where isnull(TSPL_TRANSACTION_APPROVAL.Program_code,'')='" & clsUserMgtCode.frmBulkMilkSRN & "' and ISNULL( TSPL_TRANSACTION_APPROVAL.Approve,0) =0 and isnull(srn_return_no,'')=''"
            Dim whrcls As String = ""
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrcls = " and TSPL_Bulk_MILK_SRN.Loc_Code in ( " & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qry = qry & whrcls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FndDocumnetNo.Value = ""
                LblDocDate.Text = ""
                FndDocumnetNo.Enabled = False
                ddApprovalType.Text = "Rate"
                loadBlankGrid()
                Gv1.Visible = True
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
                For i As Integer = 0 To dt.Rows.Count - 1
                    Gv1.Rows.AddNew()
                    Gv1.Rows(i).Cells(colSelect).Value = False
                    Gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                    Gv1.Rows(i).Cells(colSRNNo).Value = dt.Rows(i)("SRN_NO")
                    Gv1.Rows(i).Cells(colSRNDATe).Value = dt.Rows(i)("SRN_Date")
                    Gv1.Rows(i).Cells(colItemCode).Value = dt.Rows(i)("Item_Code")
                    Gv1.Rows(i).Cells(colItemDesc).Value = dt.Rows(i)("Item_Desc")
                    Gv1.Rows(i).Cells(colUOM).Value = dt.Rows(i)("UOM")
                    Gv1.Rows(i).Cells(colNetWeight).Value = clsCommon.myFormat(dt.Rows(i)("Net_Weight"))
                    Gv1.Rows(i).Cells(colFat).Value = clsCommon.myFormat(dt.Rows(i)("fat_per"))
                    Gv1.Rows(i).Cells(colSNF).Value = clsCommon.myFormat(dt.Rows(i)("snf_Per"))
                    Gv1.Rows(i).Cells(colFatKG).Value = clsCommon.myFormat(dt.Rows(i)("fat_KG"), False, True, False, 3, True)
                    Gv1.Rows(i).Cells(colSNFKG).Value = clsCommon.myFormat(dt.Rows(i)("SNF_KG"), False, True, False, 3, True)
                    Gv1.Rows(i).Cells(colBasicRate).Value = clsCommon.myFormat(dt.Rows(i)("BasicRate"))
                    Gv1.Rows(i).Cells(colNetRate).Value = clsCommon.myFormat(dt.Rows(i)("NetRate"))
                    Gv1.Rows(i).Cells(colIncen).Value = clsCommon.myFormat(dt.Rows(i)("Incentive"))
                    Gv1.Rows(i).Cells(colDeduc).Value = clsCommon.myFormat(dt.Rows(i)("Deduction"))
                    Gv1.Rows(i).Cells(colSpecialDeduc).Value = clsCommon.myFormat(dt.Rows(i)("DeductionAmount"))
                    Gv1.Rows(i).Cells(colNewBasicRate).Value = clsCommon.myFormat(dt.Rows(i)("BasicRate"))
                    Gv1.Rows(i).Cells(colNewNetRate).Value = clsCommon.myFormat(dt.Rows(i)("NetRate"))
                    Gv1.Rows(i).Cells(colTankerNo).Value = dt.Rows(i)("tanker_No")
                    Gv1.Rows(i).Cells(colLocCode).Value = dt.Rows(i)("Loc_Code")
                    Gv1.Rows(i).Cells(colLocDesc).Value = dt.Rows(i)("Location_Desc")
                    Gv1.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    Gv1.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    Gv1.Rows(i).Cells(colGateEntryNo).Value = dt.Rows(i)("Gate Entry No")
                    Gv1.Rows(i).Cells(colModifyBy).Value = dt.Rows(i)("modify_by")
                    Gv1.Rows(i).Cells(colApprovalType).Value = dt.Rows(i)("Approval_type")
                    Gv1.Rows(i).Cells(colbtnCol).Value = "Click Here..."
                    Gv1.Rows(i).Cells(colbtnQC).Value = "Click Here..."
                Next
            Else
                FndDocumnetNo.Enabled = True
                ddApprovalType.Text = ""
                Gv1.Visible = False
                clsCommon.MyMessageBoxShow("No SRN Found to approve")
                btnReset.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Gv1_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellClick
        If clsCommon.CompairString(clsUserMgtCode.frmQualityCheck, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colQcNo).Value) > 0 Then
                'funPrint(Gv1.CurrentRow.Cells(colQcNo).Value)
                Dim frm As New FrmQualityCheck
                frm.SetUserMgmt(clsUserMgtCode.frmQualityCheck)
                frm.strDocCode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colQcNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New FrmCSASaleInvoice
                frm.SetUserMgmt(clsUserMgtCode.frmCSASaleInvoice)
                frm.StrDocNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New frmDeliveryNoteFreshSale
                frm.SetUserMgmt(clsUserMgtCode.frmDeliveryNoteFreshSale)
                frm.StrDocNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New frmBookingDairyMultipleCustomer
                frm.SetUserMgmt(clsUserMgtCode.frmbookingdairy)
                frm.StrDocNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New frmDeliveryOrderProductSale
                frm.SetUserMgmt(clsUserMgtCode.frmSalesOrderProductSale)
                frm.StrDocNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New frmShipmentProductSale
                frm.SetUserMgmt(clsUserMgtCode.frmShipmentProductSale)
                frm.DocumentNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New FrmDispatchBulkSale
                frm.SetUserMgmt(clsUserMgtCode.FrmDispatchBulkSale)
                frm.DocumentNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.FrmLCCreation, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New FrmLCCreation
                frm.SetUserMgmt(clsUserMgtCode.FrmLCCreation)
                frm.DocumentNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colSRNNo).Value) > 0 Then
                Dim frm As New FrmBulkMilkSRN
                frm.SetUserMgmt(clsUserMgtCode.frmBulkMilkSRN)
                frm.DocumentNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colSRNNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If

        If clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New FrmCSADeliveryOrder
                frm.SetUserMgmt(clsUserMgtCode.frmCSADeliveryOrder)
                frm.StrDocNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        If clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDocNo).Value) > 0 Then
                Dim frm As New frmMCCMaterialSale
                frm.SetUserMgmt(clsUserMgtCode.frmMCCMaterial)
                frm.DocumentNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDocNo).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If
        End If
        ''====QC Value Check
        If clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
            If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnQC) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colSRNNo).Value) > 0 Then
                loadBlankQCGrid()
                GridQC.Visible = True
                Dim qry As String = ""
                Dim semiQry As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select QC_No from TSPL_Bulk_MILK_SRN where SRN_No='" & (Gv1.CurrentRow.Cells(colSRNNo).Value & "'")))
                qry = " select TSPL_QC_Parameter_Detail.Param_Field_Desc as [QC Parameter],TSPL_QC_Parameter_Detail.Param_Field_Value as [QC Value],TSPL_Bulk_MILK_SRN.QC_No,convert(varchar,TSPL_Bulk_MILK_SRN.Qc_Date ,103) as QC_Date from TSPL_Bulk_MILK_SRN left outer join TSPL_QC_Parameter_Detail on TSPL_QC_Parameter_Detail.QC_No=TSPL_Bulk_MILK_SRN.QC_No where TSPL_QC_Parameter_Detail.QC_No='" & semiQry & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        GridQC.Rows.AddNew()
                        GridQC.Rows(GridQC.Rows.Count - 1).Cells(colParNo).Value = clsCommon.myCstr(dr("QC Parameter"))
                        GridQC.Rows(GridQC.Rows.Count - 1).Cells(colParValue).Value = clsCommon.myCdbl(dr("QC Value"))
                    Next
                    lblQCNo.Text = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                    lblQCDate.Text = clsCommon.myCstr(dt.Rows(0)("QC_Date"))

                    RadPageView1.SelectedPage = RadPageViewPage3
                    GridQC.BestFitColumns()
                End If
            End If

        End If

    End Sub
    Public Sub funPrint(ByVal strDocNo As String)
        Try

            Dim Qry As String = " select Weighment_No ,TSPL_QUALITY_CHECK.remarks,case when ISNULL(weighment_no,'')='' then 'Not Done' else 'Done' end as Weighment_Status,convert(varchar,Weighment_Date,103)  as Weighment_Date,case when TSPL_QUALITY_CHECK.Doc_Type ='MccProc'  then TSPL_MCC_MASTER .MCC_NAME else  case when TSPL_QUALITY_CHECK.Doc_Type ='BulkProc' then TSPL_VENDOR_MASTER.Vendor_Name "
            Qry += "end end as MCC_Vendor_Name ,"

            Qry += " case when TSPL_QUALITY_CHECK.Doc_Type ='MccProc'  then"

            Qry += " TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end  + case when LEN(TSPL_CITY_MASTER_for_MCC.City_Name )>0 then ', '+TSPL_CITY_MASTER_for_MCC.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_for_MCC.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_for_MCC.STATE_NAME  else '' end +   "
            Qry += " case when LEN(TSPL_MCC_MASTER.Pin_code   )>0 then ', '+TSPL_MCC_MASTER.Pin_code  else ' ' end + case when len(TSPL_MCC_MASTER.Email   )>0 then ', ' +TSPL_MCC_MASTER.Email   else '' end  "
            Qry += "   else case when TSPL_QUALITY_CHECK.Doc_Type ='BulkProc'  then"

            Qry += " TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when len(TSPL_VENDOR_MASTER.add3)>0 then ', '+TSPL_VENDOR_MASTER.add3 else '' end  + case when LEN(TSPL_CITY_MASTER_for_Vendor.City_Name  )>0 then ', '+TSPL_CITY_MASTER_for_Vendor.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_for_Vendor.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_for_Vendor.STATE_NAME  else '' end +   "
            Qry += " case when LEN(TSPL_VENDOR_MASTER.Pin_code   )>0 then ', '+TSPL_VENDOR_MASTER.Pin_code  else ' ' end + case when len(TSPL_VENDOR_MASTER.Email   )>0 then ', ' +TSPL_VENDOR_MASTER.Email   else '' end end  end"

            Qry += " as MCC_Vendor_Add,TSPL_ITEM_MASTER .Item_Code ,TSPL_ITEM_MASTER .Item_Desc,isPosted,TSPL_QUALITY_CHECK.Dip_Value ,is_Param_Accepted,case when isPosted ='0' and is_Param_Accepted ='0' then 'Pending' else  case  when isPosted ='1' and is_Param_Accepted ='0' then 'Rejected' else case when isPosted ='0' and is_Param_Accepted = is_Param_Accepted then 'Pending' else  case when isPosted ='1' and is_Param_Accepted ='1' then 'Accepted'  else case when isPosted ='1' and is_Param_Accepted ='2' then 'Accepted with Special Approval' end end end end end  as ParameterAccepted , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name )>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then TSPL_STATE_MASTER.STATE_NAME  else '' end +   "
            Qry += " case when LEN(TSPL_COMPANY_MASTER.Tin_No  )>0 then ', '+TSPL_COMPANY_MASTER.Tin_No else ' ' end  "

            Qry += " as Comp_address ,TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc ,"

            Qry += " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name )>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Location.STATE_NAME  )>0 then TSPL_STATE_MASTER_For_Location.STATE_NAME  else '' end +   "
            Qry += " case when LEN(TSPL_LOCATION_MASTER.Tin_No  )>0 then ', '+TSPL_LOCATION_MASTER.Tin_No else ' ' end  "

            Qry += "  as Loc_address,TSPL_QUALITY_CHECK.QC_No ,TSPL_QUALITY_CHECK.QC_In_Date_Time ,TSPL_QUALITY_CHECK.QC_Out_Date_Time ,TSPL_QUALITY_CHECK.Tanker_No ,TSPL_QUALITY_CHECK.Gate_Entry_No ,convert(varchar,TSPL_QUALITY_CHECK.Gate_Entry_Date_And_Time,103) as Gate_Entry_Date_And_Time ,TSPL_QUALITY_CHECK.Challan_No ,TSPL_QUALITY_CHECK.Challan_Date ,TSPL_QC_Parameter_Detail.Param_Field_Desc ,TSPL_QC_Parameter_Detail.Param_Field_Value,TSPL_QC_Parameter_Detail.Param_type "
            Qry += "   from TSPL_QUALITY_CHECK"
            Qry += " left outer join TSPL_QC_Parameter_Detail on TSPL_QC_Parameter_Detail.QC_No =TSPL_QUALITY_CHECK.QC_No "
            Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_QUALITY_CHECK.comp_code "
            Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_COMPANY_MASTER.City_Code "
            Qry += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State "
            Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_QUALITY_CHECK.location_Code "
            Qry += " left outer join TSPL_CITY_MASTER  as TSPL_CITY_MASTER_For_Location on TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.location_Code "
            Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Location on TSPL_STATE_MASTER_For_Location.STATE_CODE =TSPL_LOCATION_MASTER.State  "
            Qry += "  left outer join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_QUALITY_CHECK.Item_Code"
            Qry += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Code "
            Qry += " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_for_MCC on TSPL_CITY_MASTER_for_MCC .City_Code =TSPL_MCC_MASTER.City_code "
            Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_for_MCC on TSPL_STATE_MASTER_for_MCC .STATE_CODE =TSPL_MCC_MASTER.State_Code "
            Qry += " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Vendor_Code  =TSPL_QUALITY_CHECK .Vendor_Code"
            Qry += " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_for_Vendor on TSPL_CITY_MASTER_for_Vendor .City_Code =TSPL_VENDOR_MASTER.City_code "
            Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_for_Vendor on TSPL_STATE_MASTER_for_Vendor .STATE_CODE =TSPL_VENDOR_MASTER.State_Code"
            Qry += " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.code=TSPL_QC_Parameter_Detail.Param_Field_Code"
            Qry += "   where IsForPrintOnQC =1 and TSPL_QUALITY_CHECK.QC_No ='" + strDocNo + "'"
            'Qry += " and TSPL_QC_Parameter_Detail.Param_Field_Desc <>'AUTO SNF' and TSPL_QC_Parameter_Detail.Param_Field_Desc <>'Auto FAT' and TSPL_QC_Parameter_Detail.Param_Field_Desc <>'AUTO CLR'"

            Qry = " select xx.*, case when xx.Param_Type='NA'  then 1 when  xx.Param_Type='FAT' then 2 when xx.Param_Type='SNF' then 3 when xx.Param_Type='CLR' then 4 when xx.Param_Type='OTHERS' then 5 else 6 end as Ordering from ( " & Qry & " ) xx  order by ordering"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptQualityCheck", "Quality Check")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                isInsideLoadData = True
                If e.Column Is Gv1.Columns(colSelect) AndAlso clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                    Gv1.CurrentRow.Cells(colNewBasicRate).ReadOnly = Not clsCommon.myCBool(Gv1.CurrentRow.Cells(colSelect).Value)
                ElseIf e.Column Is Gv1.Columns(colSelect) AndAlso clsCommon.CompairString(clsUserMgtCode.frmQualityCheck, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(Gv1.CurrentRow.Cells(colApprovalType).Value, "Special Deduction") = CompairStringResult.Equal Then
                        Gv1.CurrentRow.Cells(colSpclDeductionAmt).ReadOnly = Not clsCommon.myCBool(Gv1.CurrentRow.Cells(colSelect).Value)
                    End If
                ElseIf e.Column Is Gv1.Columns(colNewBasicRate) AndAlso clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, cmbScreenName.SelectedValue) = CompairStringResult.Equal Then
                    Gv1.CurrentRow.Cells(colNewNetRate).Value = (clsCommon.myCdbl(Gv1.CurrentRow.Cells(colNewBasicRate).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colIncen).Value) + clsCommon.myCdbl(Gv1.CurrentRow.Cells(colDeduc).Value)) - clsCommon.myCdbl(Gv1.CurrentRow.Cells(colSpecialDeduc).Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub Gv1_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gv1.VisibleChanged
        btnuSel.Visible = Gv1.Visible
        btnSelect.Visible = Gv1.Visible
    End Sub

    Private Sub btnuSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnuSel.Click
        'If clsCommon.CompairString(btnuSel.Text, "Select All") = CompairStringResult.Equal Then
        '    btnuSel.Text = "Unselect All"
        '    CheckAll()
        'Else
        '    btnuSel.Text = "Select All"
        UnCheckAll()
        'End If
    End Sub
    Private Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        'If clsCommon.CompairString(btnuSel.Text, "Select All") = CompairStringResult.Equal Then
        '    btnuSel.Text = "Unselect All"
        CheckAll()
        'Else
        '    btnuSel.Text = "Select All"
        'UnCheckAll()
        'End If
    End Sub
    Sub CheckAll()
        If Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then
            For i As Integer = 0 To Gv1.ChildRows.Count - 1
                Gv1.ChildRows(i).Cells(colSelect).Value = True
            Next
        End If
    End Sub
    Sub UnCheckAll()
        If Gv1 IsNot Nothing AndAlso Gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To Gv1.ChildRows.Count - 1
                Gv1.ChildRows(i).Cells(colSelect).Value = False
            Next
        End If
    End Sub

    Private Sub btnUnapprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnapprove.Click
        Try
            If clsCommon.CompairString(cmbScreenName.Text, "Select") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Please Select At least one document.")
                Exit Sub
            End If
            Dim rowsCount As Integer = 0
            Dim ExtraMsg As String = ""
            If Gv1.Visible = True And Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then
                For i As Integer = 0 To Gv1.Rows.Count - 1
                    If Gv1.Rows(i).Cells(colSelect).Value = True Then
                        rowsCount = rowsCount + 1
                        If clsCommon.CompairString(Gv1.Rows(i).Cells(colApprovalType).Value, "Special Deduction") = CompairStringResult.Equal Then
                            Throw New Exception("Can't be Unapproved. It is already approved, Document at Row No:  " & (i + 1))
                        End If
                    End If
                Next
                If rowsCount > 0 Then
                    ExtraMsg = Environment.NewLine & "you are going to unapprove  total " & rowsCount & " " & cmbScreenName.Text & " Document(s)"
                Else
                    ExtraMsg = ""
                End If
            End If
            If clsCommon.MyMessageBoxShow("Are you sure to unapprove selected document(s)." & ExtraMsg, Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Unapprove()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

   
    
End Class

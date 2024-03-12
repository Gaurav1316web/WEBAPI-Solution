Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class RptMccSaleAdjustment
    Inherits FrmMainTranScreen

#Region "Class Variables"
#End Region

    Private Sub SetUserMgmtNew()
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub RptMccSaleAdjustment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            RadPageView1.SelectedPage = RadPageViewPage1
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = clsCommon.GETSERVERDATE().AddDays(10)
            rbtnVLCWise.IsChecked = True
            setFromAndToDate()
            SetAdhocPer()
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Try
            Dim qry As String = ""
            Dim arrLoc As String = ""
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            End If
            qry = " select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on location_Code= mcc_Code " _
                & " and (loc_segment_Code in (" & arrLoc & ") or mcc_Code in (" & arrLoc & ")))xx "
            txtMCC.Value = clsCommon.ShowSelectForm("MCCSAAD", qry, "Code", "", txtMCC.Value, "", isButtonClicked)
            lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Value + "'"))
            If cbxPaymentCycle.Checked = False Then
                SetToDate()
            End If
            txtVSP.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            Dim qry As String = ""
            Dim arrLoc As String = ""
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            qry = " select VSP_Code as Code,VLC_Name as [Name],VLC_Code as [VLC Code],VLC_Code_VLC_Uploader as [Uploader Code] from TSPL_VLC_MASTER_HEAD where MCC='" + txtMCC.Value + "' "
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPSAAD", qry, "Code", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv1
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            gv1.DataSource = Nothing
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                Throw New Exception("Please Select MCC")
            End If
            Dim BaseQry As String
            Dim qry As String = ""
            If chkSkippedDocument.Checked Then
                qry = "select MCCCode,TSPL_MCC_MASTER.MCC_NAME,Doc_No,Doc_Date,From_Date,To_Date,Vendor_Code,case when Vendor_Code='ALL' then  'ALL' else TSPL_VLC_MASTER_HEAD.VLC_Code end as VLC_Code,case when Vendor_Code='ALL' then  'ALL' else TSPL_VLC_MASTER_HEAD.VLC_Name end as VLC_Name,Source_Doc_No,Source_Doc_Type,Balance_Amount  from (" + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,case when TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Source_Doc_Type in ('MCC-SALE','MCC-SALE-RET') then TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code else TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Vendor_Code end Vendor_Code,TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Source_Doc_No,TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Source_Doc_Type,TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Balance_Amount " + Environment.NewLine + _
                "from TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT" + Environment.NewLine + _
                "left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Doc_No" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Vendor_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and " + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' " + Environment.NewLine + _
                "and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    'qry += " and case when TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Source_Doc_Type in ('MCC-SALE','MCC-SALE-RET') then TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code else TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Vendor_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                    qry += " and  TSPL_PAYMENT_PROCESS_SKIP_DOCUMENT.Vendor_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") " + Environment.NewLine
                End If
                qry += "Union all " + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,'ALL' as Vendor_Code,'ALL' as Source_Doc_No,'MCC-SALE' as Source_Doc_Type ,0 as Balance_Amount from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and Is_Skip_Previous_MCC_Sale=1 and" + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,'ALL' as Vendor_Code,'ALL' as Source_Doc_No,'MCC-SALE-RET' as Source_Doc_Type ,0 as Balance_Amount from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and Is_Skip_Previous_MCC_Sale_Return=1 and" + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,'ALL' as Vendor_Code,'ALL' as Source_Doc_No,'VSP-ITEM-ISSUE' as Source_Doc_Type ,0 as Balance_Amount from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and Is_Skip_Previous_Item_Issue=1 and" + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,'ALL' as Vendor_Code,'ALL' as Source_Doc_No,'VSP-ITEM-ISSUE-RETURN' as Source_Doc_Type ,0 as Balance_Amount from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and Is_Skip_Previous_Item_Issue_Return=1 and" + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,'ALL' as Vendor_Code,'ALL' as Source_Doc_No,'VSP-ITEM-ISSUE-RETURN' as Source_Doc_Type ,0 as Balance_Amount from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and Is_Skip_Previous_Item_Issue_Return=1 and" + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,'ALL' as Vendor_Code,'ALL' as Source_Doc_No,'DEBIT-NOTE' as Source_Doc_Type ,0 as Balance_Amount from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and Is_Skip_Previous_Debit_Note=1 and" + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,'ALL' as Vendor_Code,'ALL' as Source_Doc_No,'CREDIT-NOTE' as Source_Doc_Type ,0 as Balance_Amount from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and Is_Skip_Previous_Credit_Note=1 and" + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,'ALL' as Vendor_Code,'ALL' as Source_Doc_No,'ADVANCE' as Source_Doc_Type ,0 as Balance_Amount from TSPL_PAYMENT_PROCESS_HEAD" + Environment.NewLine + _
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                "where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 and Is_Skip_Previous_Advacee_Payment=1 and" + Environment.NewLine + _
                "TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine + _
                ")xx" + Environment.NewLine + _
                "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xx.MCCCode" + Environment.NewLine + _
                "left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xx.Vendor_Code "

            Else
                BaseQry = "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Qty,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Service_Charge_Amt,TSPL_PAYMENT_PROCESS_DETAIL.Total_Invoice_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,(TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount-(select ISNULL(sum(Reduce_Deduc_Amt),0) from TSPL_PAYMENT_PROCESS_MCC_SALE where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No and TSPL_PAYMENT_PROCESS_MCC_SALE.Customer_CODE=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE)) as MCC_Sale_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount_Knock_Off,1 as RI " + Environment.NewLine + _
                " from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine + _
                " left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No" + Environment.NewLine + _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine + _
                " where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1" + Environment.NewLine + _
                " and TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' " + Environment.NewLine + _
                " and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                End If
                BaseQry += " union all" + Environment.NewLine + _
                " select TSPL_MILK_SRN_HEAD.MCC_CODE as MCCCode, VSP_CODE,ACC_Qty as Milk_Qty,  TSPL_MILK_SRN_DETAIL.AMOUNT  as Milk_Amount,0 as Incentive_Amount,0 as Total_EMP_Amount,0 as Service_Charge_Amt,0 as Total_Invoice_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,0 as Payable_Amount,0 as MCC_Sale_Amount,0 as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,2 as RI from TSPL_MILK_SRN_DETAIL" + Environment.NewLine + _
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine + _
                " where TSPL_MILK_SRN_HEAD.Posted=1 and TSPL_MILK_SRN_HEAD.MCC_CODE='" + txtMCC.Value + "' and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    BaseQry += " and VSP_CODE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                End If
                BaseQry += " union all" + Environment.NewLine + _
                " select TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as MCCCode,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code as VSP_CODE,0 as Milk_Qty,0 as Milk_Amount,0 as Incentive_Amount,0 as Total_EMP_Amount,0 as Service_Charge_Amt,0 as Total_Invoice_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,0 as Payable_Amount,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as MCC_Sale_Amount,0 as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,3 as RI  " + Environment.NewLine + _
                " from TSPL_SD_SALE_INVOICE_HEAD  " + Environment.NewLine + _
                " left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code     " + Environment.NewLine + _
                " where TSPL_SD_SALE_INVOICE_HEAD.Status=1 and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + txtMCC.Value + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                End If
                BaseQry += " union all" + Environment.NewLine + _
                " select TSPL_VLC_MASTER_HEAD.MCC as MCCCode,TSPL_VLC_MASTER_HEAD.VSP_Code ,0 as Milk_Qty,0 as Milk_Amount,0 as Incentive_Amount,0 as Total_EMP_Amount,0 as Service_Charge_Amt,0 as Total_Invoice_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,0 as Payable_Amount,0 as MCC_Sale_Amount,(TSPL_PAYMENT_HEADER.Payment_Amount+isnull(TDS_Amount ,0)) as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,4 as RI  " + Environment.NewLine + _
                " from TSPL_PAYMENT_HEADER  " + Environment.NewLine + _
                " inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_HEADER.Vendor_Code     " + Environment.NewLine + _
                " where TSPL_PAYMENT_HEADER.Posted=1 and TSPL_PAYMENT_HEADER.Payment_Type IN ('AV','OA') and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' and TSPL_PAYMENT_HEADER.Payment_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_HEADER.Payment_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                End If
                If rbtnVLCWise.IsChecked Then
                    qry = "select MCCCode,max(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,xxx.VSP_CODE,max(TSPL_VENDOR_MASTER.Vendor_Name) as Vendor_Name,max(TSPL_VLC_MASTER_HEAD.VLC_Code) as VLC_Code,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_Name,max(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader" + Environment.NewLine +
                    " ,sum( Milk_Qty * case when RI=1 then 1 else 0 end ) as ProcessQty" + Environment.NewLine +
                    " ,sum( Milk_Qty * case when RI=1 then 1 else 0 end ) as ActualQty" + Environment.NewLine +
                    " ,sum( Milk_Qty * case when RI=1 then 1 else 0 end )-sum( Milk_Qty * case when RI=1 then 1 else 0 end ) as DiffQty" + Environment.NewLine +
                    " ,sum( Milk_Amount * case when RI=1 then 1 else 0 end ) as ProcessAmount" + Environment.NewLine +
                    " ,convert(decimal(18,2), ROUND( sum(Milk_Amount * case when RI=2 then 1 else 0 end ),2,0)) as ActualAmount" + Environment.NewLine +
                    " ,convert(decimal(18,2), ( convert(decimal(18,2), ROUND(sum(Milk_Amount * case when RI=2 then 1 else 0 end ),2,0))- sum( Milk_Amount * case when RI=1 then 1 else 0 end ))) as  DiffAmount" + Environment.NewLine +
                    " ,sum(Incentive_Amount) as Incentive_Amount,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Service_Charge_Amt) as Service_Charge_Amt,sum(Total_Invoice_Amount) as Total_Invoice_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Credit_Note_Amount) as Credit_Note_Amount" + Environment.NewLine +
                    " ,sum( MCC_Sale_Amount * case when RI=1 then 1 else 0 end ) as ProcessMCCSaleAmount" + Environment.NewLine +
                    " ,sum( MCC_Sale_Amount * case when RI=3 then 1 else 0 end ) as ActualMCCSaleAmount" + Environment.NewLine +
                    " ,sum(convert(decimal(18,2), MCC_Sale_Amount * case when RI=3 then 1 else case when RI=1 then -1 else 0 end end)) as DiffMCCSaleAmount" + Environment.NewLine +
                    " ,sum(Advance_Payment_Amount * case when RI=1 then 1 else 0 end) as ProcessAdvance_Payment_Amount,sum(Advance_Payment_Amount_Knock_Off) as Advance_Payment_Amount_Knock_Off" + Environment.NewLine +
                    " ,sum(Advance_Payment_Amount * case when RI=4 then 1 else 0 end) as ActualAdvance_Payment_Amount" + Environment.NewLine +
                    " ,sum(convert(decimal(18,2), Advance_Payment_Amount * case when RI=4 then 1 else case when RI=1 then -1 else 0 end end)) as DiffAdvance_Payment_Amount" + Environment.NewLine +
                    " ,sum(Payable_Amount) as Payable_Amount,convert (decimal(18,2),(sum(Payable_Amount) / case when  sum(Total_Invoice_Amount) =0 then 1 else sum(Total_Invoice_Amount) end *100 )) as Payable_Amount_Percentage" + Environment.NewLine +
                    " ,convert(decimal(18,2), ROUND( sum(Milk_Amount * case when RI=2 then 1 else 0 end ),2,0))+sum(Credit_Note_Amount)-sum(Deduction_Amount)-sum( MCC_Sale_Amount * case when RI=3 then 1 else 0 end ) [Total Amount]" + Environment.NewLine +
                    " from (" + Environment.NewLine + BaseQry + Environment.NewLine +
                           ")xxx " + Environment.NewLine +
                    " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCCCode" + Environment.NewLine +
                    " inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xxx.VSP_CODE" + Environment.NewLine +
                    " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xxx.VSP_CODE" + Environment.NewLine +
                    " group by xxx.MCCCode,xxx.VSP_CODE"
                ElseIf rbtnMCCWise.IsChecked Then
                    qry = "select MCCCode,max(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME " + Environment.NewLine +
                   " ,sum( Milk_Qty * case when RI=1 then 1 else 0 end ) as ProcessQty" + Environment.NewLine +
                   " ,sum( Milk_Qty * case when RI=2 then 1 else 0 end ) as ActualQty" + Environment.NewLine +
                   " ,sum(convert(decimal(18,2),  Milk_Qty * case when RI=2 then 1 else case when RI=1 then -1 else 0 end end)) as DiffQty" + Environment.NewLine +
                   " ,sum( Milk_Amount * case when RI=1 then 1 else 0 end ) as ProcessAmount" + Environment.NewLine +
                   " ,convert(decimal(18,2), ROUND( sum(Milk_Amount * case when RI=2 then 1 else 0 end ),2,0)) as ActualAmount" + Environment.NewLine +
                   " ,convert(decimal(18,2), ( convert(decimal(18,2), ROUND(sum(Milk_Amount * case when RI=2 then 1 else 0 end ),2,0))- sum( Milk_Amount * case when RI=1 then 1 else 0 end ))) as  DiffAmount" + Environment.NewLine +
                   " ,sum(Incentive_Amount) as Incentive_Amount,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Service_Charge_Amt) as Service_Charge_Amt,sum(Total_Invoice_Amount) as Total_Invoice_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Credit_Note_Amount) as Credit_Note_Amount" + Environment.NewLine +
                   " ,sum( MCC_Sale_Amount * case when RI=1 then 1 else 0 end ) as ProcessMCCSaleAmount" + Environment.NewLine +
                   " ,sum( MCC_Sale_Amount * case when RI=3 then 1 else 0 end ) as ActualMCCSaleAmount" + Environment.NewLine +
                   " ,sum(convert(decimal(18,2), MCC_Sale_Amount * case when RI=3 then 1 else case when RI=1 then -1 else 0 end end)) as DiffMCCSaleAmount" + Environment.NewLine +
                   " ,sum(Advance_Payment_Amount * case when RI=1 then 1 else 0 end) as ProcessAdvance_Payment_Amount,sum(Advance_Payment_Amount_Knock_Off) as Advance_Payment_Amount_Knock_Off" + Environment.NewLine +
                   " ,sum(Advance_Payment_Amount * case when RI=4 then 1 else 0 end) as ActualAdvance_Payment_Amount" + Environment.NewLine +
                   " ,sum(convert(decimal(18,2), Advance_Payment_Amount * case when RI=4 then 1 else case when RI=1 then -1 else 0 end end)) as DiffAdvance_Payment_Amount" + Environment.NewLine +
                   " ,sum(Payable_Amount) as Payable_Amount,convert (decimal(18,2),(sum(Payable_Amount) / case when  sum(Total_Invoice_Amount) =0 then 1 else sum(Total_Invoice_Amount) end *100 )) as Payable_Amount_Percentage " + Environment.NewLine +
                   " ,convert(decimal(18,2), ROUND( sum(Milk_Amount * case when RI=2 then 1 else 0 end ),2,0))+sum(Credit_Note_Amount)-sum(Deduction_Amount)-sum( MCC_Sale_Amount * case when RI=3 then 1 else 0 end ) [Total Amount]" + Environment.NewLine +
                   " from (" + Environment.NewLine + BaseQry + Environment.NewLine +
                   " )xxx " + Environment.NewLine +
                   " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCCCode" + Environment.NewLine +
                   " inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xxx.VSP_CODE" + Environment.NewLine +
                   " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xxx.VSP_CODE" + Environment.NewLine +
                   " group by xxx.MCCCode "
                ElseIf rbtnAdhoc.IsChecked Then
                    qry = "select row_number() over (order by MCCCode ) as Sno, MCCCode,max(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,xxx.VSP_CODE,max(TSPL_VENDOR_MASTER.Vendor_Name) as Vendor_Name,max(TSPL_VLC_MASTER_HEAD.VLC_Code) as VLC_Code,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_Name,max(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader" + Environment.NewLine + _
                    " ,sum( Milk_Qty  ) as ActualQty" + Environment.NewLine + _
                    " ,convert(decimal(18,2), ROUND( sum(Milk_Amount )+sum(Total_EMP_Amount ),2,0)) as ActualAmount" + Environment.NewLine + _
                    " ,convert(decimal(18,2), convert(decimal(18,2), ROUND( sum(Milk_Amount)+sum(Total_EMP_Amount ),2,0))*(" + clsCommon.myCstr(txtPer.Value) + "/100.00)) as AdhocAmount" + Environment.NewLine + _
                    "  ,convert(decimal(18,2), convert(decimal(18,2), ROUND( sum(MCC_Sale_Amount  ),2,0))) as mcc_sale_Amonut" + Environment.NewLine + _
                    " ,convert(decimal(18,2), convert(decimal(18,2), ROUND( sum(Advance_Payment_Amount  ),2,0))) as Advance_Payment " + Environment.NewLine + _
                    " ,convert(decimal(18,2), convert(decimal(18,2), ROUND( sum(Payable_Amount ),2,0))) as Payable_Amount " + Environment.NewLine + _
                     " ,convert(decimal(18,2), convert(decimal(18,2), ROUND( sum(Payable_Amount ),2,0))*(" + clsCommon.myCstr(txtPer.Value) + "/100.00)) as Payable_Amount_Per " + Environment.NewLine + _
                    " from ( select * from ( " + Environment.NewLine + BaseQry + Environment.NewLine + _
                    " )xx where RI=1 ) xxx " + Environment.NewLine + _
                    " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCCCode" + Environment.NewLine + _
                    " inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xxx.VSP_CODE" + Environment.NewLine + _
                    " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xxx.VSP_CODE" + Environment.NewLine + _
                    " group by xxx.MCCCode,xxx.VSP_CODE"
                Else
                    Throw New Exception("Wrong report type")
                End If
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found")
            End If

            gv1.DataSource = dt

            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = True
            gv1.AllowRowReorder = False
            gv1.AllowAddNewRow = False
            gv1.EnableSorting = True
            gv1.EnableFiltering = True
            gv1.EnableAlternatingRowColor = True
            gv1.AutoSizeRows = False
            gv1.AllowRowResize = True
            gv1.VerticalScrollState = ScrollState.AlwaysShow
            gv1.HorizontalScrollState = ScrollState.AlwaysShow
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
            gv1.ShowFilteringRow = True

            SetGridFormatOfGV()
            Panel1.Enabled = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetGridFormatOfGV()
        Try
            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next
            If chkSkippedDocument.Checked Then
                gv1.Columns("MCCCode").IsVisible = True
                gv1.Columns("MCCCode").Width = 100
                gv1.Columns("MCCCode").HeaderText = "MCC Code"

                gv1.Columns("MCC_NAME").IsVisible = True
                gv1.Columns("MCC_NAME").Width = 150
                gv1.Columns("MCC_NAME").HeaderText = "MCC Name"

                gv1.Columns("Doc_No").IsVisible = True
                gv1.Columns("Doc_No").Width = 100
                gv1.Columns("Doc_No").HeaderText = "Doc No"

                gv1.Columns("Doc_Date").IsVisible = True
                gv1.Columns("Doc_Date").Width = 100
                gv1.Columns("Doc_Date").HeaderText = "Doc Date"

                gv1.Columns("From_Date").IsVisible = True
                gv1.Columns("From_Date").Width = 100
                gv1.Columns("From_Date").HeaderText = "From Date"

                gv1.Columns("To_Date").IsVisible = True
                gv1.Columns("To_Date").Width = 100
                gv1.Columns("To_Date").HeaderText = "To Date"

                gv1.Columns("Vendor_Code").IsVisible = True
                gv1.Columns("Vendor_Code").Width = 100
                gv1.Columns("Vendor_Code").HeaderText = "VSP Code"

                gv1.Columns("VLC_Code").IsVisible = True
                gv1.Columns("VLC_Code").Width = 100
                gv1.Columns("VLC_Code").HeaderText = "VLC Code"

                gv1.Columns("VLC_Name").IsVisible = True
                gv1.Columns("VLC_Name").Width = 150
                gv1.Columns("VLC_Name").HeaderText = "VLC Name"

                'gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
                'gv1.Columns("VLC_Code_VLC_Uploader").Width = 150
                'gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Uploder Code"

                gv1.Columns("Source_Doc_No").IsVisible = False
                gv1.Columns("Source_Doc_No").Width = 150
                gv1.Columns("Source_Doc_No").HeaderText = "Source Document No"

                gv1.Columns("Source_Doc_Type").IsVisible = True
                gv1.Columns("Source_Doc_Type").Width = 100
                gv1.Columns("Source_Doc_Type").HeaderText = "Source Document Type"

                gv1.Columns("Balance_Amount").IsVisible = True
                gv1.Columns("Balance_Amount").Width = 100
                gv1.Columns("Balance_Amount").HeaderText = "Amount"

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim summaryItem As New GridViewSummaryItem()

                Dim item1 As GridViewSummaryItem

                item1 = New GridViewSummaryItem("Balance_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)

                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.AutoExpandGroups = True
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else
                If rbtnVLCWise.IsChecked Then
                    gv1.Columns("MCCCode").IsVisible = True
                    gv1.Columns("MCCCode").Width = 100
                    gv1.Columns("MCCCode").HeaderText = "MCC Code"

                    gv1.Columns("MCC_NAME").IsVisible = True
                    gv1.Columns("MCC_NAME").Width = 150
                    gv1.Columns("MCC_NAME").HeaderText = "MCC Name"

                    gv1.Columns("VSP_CODE").IsVisible = True
                    gv1.Columns("VSP_CODE").Width = 100
                    gv1.Columns("VSP_CODE").HeaderText = "VSP Code"

                    gv1.Columns("Vendor_Name").IsVisible = False
                    gv1.Columns("Vendor_Name").Width = 150
                    gv1.Columns("Vendor_Name").HeaderText = "VSP Name"

                    gv1.Columns("VLC_Code").IsVisible = True
                    gv1.Columns("VLC_Code").Width = 100
                    gv1.Columns("VLC_Code").HeaderText = "VLC Code"

                    gv1.Columns("VLC_Name").IsVisible = True
                    gv1.Columns("VLC_Name").Width = 150
                    gv1.Columns("VLC_Name").HeaderText = "VLC Name"

                    gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
                    gv1.Columns("VLC_Code_VLC_Uploader").Width = 150
                    gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Uploder Code"

                    gv1.Columns("ProcessQty").IsVisible = True
                    gv1.Columns("ProcessQty").Width = 100
                    gv1.Columns("ProcessQty").HeaderText = "Process Qty"

                    gv1.Columns("ActualQty").IsVisible = True
                    gv1.Columns("ActualQty").Width = 100
                    gv1.Columns("ActualQty").HeaderText = "Actual Qty"

                    gv1.Columns("DiffQty").IsVisible = True
                    gv1.Columns("DiffQty").Width = 100
                    gv1.Columns("DiffQty").HeaderText = "Diff Qty"

                    gv1.Columns("ProcessAmount").IsVisible = True
                    gv1.Columns("ProcessAmount").Width = 100
                    gv1.Columns("ProcessAmount").HeaderText = "Process Amount"

                    gv1.Columns("ActualAmount").IsVisible = True
                    gv1.Columns("ActualAmount").Width = 100
                    gv1.Columns("ActualAmount").HeaderText = "Actual Amount"

                    gv1.Columns("DiffAmount").IsVisible = True
                    gv1.Columns("DiffAmount").Width = 100
                    gv1.Columns("DiffAmount").HeaderText = "Diff Amount"

                    gv1.Columns("Incentive_Amount").IsVisible = True
                    gv1.Columns("Incentive_Amount").Width = 100
                    gv1.Columns("Incentive_Amount").HeaderText = "Incentive Amount"

                    gv1.Columns("Total_EMP_Amount").IsVisible = True
                    gv1.Columns("Total_EMP_Amount").Width = 100
                    gv1.Columns("Total_EMP_Amount").HeaderText = "Total EMP"

                    gv1.Columns("Service_Charge_Amt").IsVisible = True
                    gv1.Columns("Service_Charge_Amt").Width = 100
                    gv1.Columns("Service_Charge_Amt").HeaderText = "Service Charge"

                    gv1.Columns("Total_Invoice_Amount").IsVisible = True
                    gv1.Columns("Total_Invoice_Amount").Width = 100
                    gv1.Columns("Total_Invoice_Amount").HeaderText = "Total Invoice Amount"

                    gv1.Columns("Deduction_Amount").IsVisible = True
                    gv1.Columns("Deduction_Amount").Width = 100
                    gv1.Columns("Deduction_Amount").HeaderText = "Deduction Amount"

                    gv1.Columns("Credit_Note_Amount").IsVisible = True
                    gv1.Columns("Credit_Note_Amount").Width = 100
                    gv1.Columns("Credit_Note_Amount").HeaderText = "Credit Note Amount"

                    gv1.Columns("ProcessMCCSaleAmount").IsVisible = True
                    gv1.Columns("ProcessMCCSaleAmount").Width = 100
                    gv1.Columns("ProcessMCCSaleAmount").HeaderText = "Process MCC Sale Amount"

                    gv1.Columns("ActualMCCSaleAmount").IsVisible = True
                    gv1.Columns("ActualMCCSaleAmount").Width = 100
                    gv1.Columns("ActualMCCSaleAmount").HeaderText = "Actual MCC Sale Amount"

                    gv1.Columns("DiffMCCSaleAmount").IsVisible = True
                    gv1.Columns("DiffMCCSaleAmount").Width = 100
                    gv1.Columns("DiffMCCSaleAmount").HeaderText = "Diff MCC Sale Amount"

                    gv1.Columns("ProcessAdvance_Payment_Amount").IsVisible = True
                    gv1.Columns("ProcessAdvance_Payment_Amount").Width = 100
                    gv1.Columns("ProcessAdvance_Payment_Amount").HeaderText = "Process Advance Payment Amount"

                    gv1.Columns("Advance_Payment_Amount_Knock_Off").IsVisible = True
                    gv1.Columns("Advance_Payment_Amount_Knock_Off").Width = 100
                    gv1.Columns("Advance_Payment_Amount_Knock_Off").HeaderText = "Advance Payment Knock Off Amount"

                    gv1.Columns("ActualAdvance_Payment_Amount").IsVisible = True
                    gv1.Columns("ActualAdvance_Payment_Amount").Width = 100
                    gv1.Columns("ActualAdvance_Payment_Amount").HeaderText = "Actual Advance Payment Amount"

                    gv1.Columns("DiffAdvance_Payment_Amount").IsVisible = True
                    gv1.Columns("DiffAdvance_Payment_Amount").Width = 100
                    gv1.Columns("DiffAdvance_Payment_Amount").HeaderText = "Diff Advance Payment Amount"

                    gv1.Columns("Payable_Amount").IsVisible = True
                    gv1.Columns("Payable_Amount").Width = 100
                    gv1.Columns("Payable_Amount").HeaderText = "Payable Amount"

                    gv1.Columns("Payable_Amount_Percentage").IsVisible = True
                    gv1.Columns("Payable_Amount_Percentage").Width = 100
                    gv1.Columns("Payable_Amount_Percentage").HeaderText = "Payable Amount(%)"


                    gv1.Columns("Total Amount").IsVisible = True
                    gv1.Columns("Total Amount").Width = 100
                    gv1.Columns("Total Amount").HeaderText = "Total Amount"

                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim summaryItem As New GridViewSummaryItem()

                    Dim item1 As GridViewSummaryItem

                    item1 = New GridViewSummaryItem("ProcessQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("DiffQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ProcessAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Incentive_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Total_EMP_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Service_Charge_Amt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Total_Invoice_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Credit_Note_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ProcessMCCSaleAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualMCCSaleAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("DiffMCCSaleAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ProcessAdvance_Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Advance_Payment_Amount_Knock_Off", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualAdvance_Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("DiffAdvance_Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Payable_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    gv1.ShowGroupPanel = False
                    gv1.MasterTemplate.AutoExpandGroups = True
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                ElseIf rbtnMCCWise.IsChecked Then
                    gv1.Columns("MCCCode").IsVisible = True
                    gv1.Columns("MCCCode").Width = 100
                    gv1.Columns("MCCCode").HeaderText = "MCC Code"

                    gv1.Columns("MCC_NAME").IsVisible = True
                    gv1.Columns("MCC_NAME").Width = 150
                    gv1.Columns("MCC_NAME").HeaderText = "MCC Name"

                    gv1.Columns("ProcessQty").IsVisible = True
                    gv1.Columns("ProcessQty").Width = 100
                    gv1.Columns("ProcessQty").HeaderText = "Process Qty"

                    gv1.Columns("ActualQty").IsVisible = True
                    gv1.Columns("ActualQty").Width = 100
                    gv1.Columns("ActualQty").HeaderText = "Actual Qty"

                    gv1.Columns("DiffQty").IsVisible = True
                    gv1.Columns("DiffQty").Width = 100
                    gv1.Columns("DiffQty").HeaderText = "Diff Qty"

                    gv1.Columns("ProcessAmount").IsVisible = True
                    gv1.Columns("ProcessAmount").Width = 100
                    gv1.Columns("ProcessAmount").HeaderText = "Process Amount"

                    gv1.Columns("ActualAmount").IsVisible = True
                    gv1.Columns("ActualAmount").Width = 100
                    gv1.Columns("ActualAmount").HeaderText = "Actual Amount"

                    gv1.Columns("DiffAmount").IsVisible = True
                    gv1.Columns("DiffAmount").Width = 100
                    gv1.Columns("DiffAmount").HeaderText = "Diff Amount"

                    gv1.Columns("Incentive_Amount").IsVisible = True
                    gv1.Columns("Incentive_Amount").Width = 100
                    gv1.Columns("Incentive_Amount").HeaderText = "Incentive Amount"

                    gv1.Columns("Total_EMP_Amount").IsVisible = True
                    gv1.Columns("Total_EMP_Amount").Width = 100
                    gv1.Columns("Total_EMP_Amount").HeaderText = "Total EMP"

                    gv1.Columns("Service_Charge_Amt").IsVisible = True
                    gv1.Columns("Service_Charge_Amt").Width = 100
                    gv1.Columns("Service_Charge_Amt").HeaderText = "Service Charge"

                    gv1.Columns("Total_Invoice_Amount").IsVisible = True
                    gv1.Columns("Total_Invoice_Amount").Width = 100
                    gv1.Columns("Total_Invoice_Amount").HeaderText = "Total Invoice Amount"

                    gv1.Columns("Deduction_Amount").IsVisible = True
                    gv1.Columns("Deduction_Amount").Width = 100
                    gv1.Columns("Deduction_Amount").HeaderText = "Deduction Amount"

                    gv1.Columns("Credit_Note_Amount").IsVisible = True
                    gv1.Columns("Credit_Note_Amount").Width = 100
                    gv1.Columns("Credit_Note_Amount").HeaderText = "Credit Note Amount"

                    gv1.Columns("ProcessMCCSaleAmount").IsVisible = True
                    gv1.Columns("ProcessMCCSaleAmount").Width = 100
                    gv1.Columns("ProcessMCCSaleAmount").HeaderText = "Process MCC Sale Amount"

                    gv1.Columns("ActualMCCSaleAmount").IsVisible = True
                    gv1.Columns("ActualMCCSaleAmount").Width = 100
                    gv1.Columns("ActualMCCSaleAmount").HeaderText = "Actual MCC Sale Amount"

                    gv1.Columns("DiffMCCSaleAmount").IsVisible = True
                    gv1.Columns("DiffMCCSaleAmount").Width = 100
                    gv1.Columns("DiffMCCSaleAmount").HeaderText = "Diff MCC Sale Amount"

                    gv1.Columns("ProcessAdvance_Payment_Amount").IsVisible = True
                    gv1.Columns("ProcessAdvance_Payment_Amount").Width = 100
                    gv1.Columns("ProcessAdvance_Payment_Amount").HeaderText = "Process Advance Payment Amount"

                    gv1.Columns("Advance_Payment_Amount_Knock_Off").IsVisible = True
                    gv1.Columns("Advance_Payment_Amount_Knock_Off").Width = 100
                    gv1.Columns("Advance_Payment_Amount_Knock_Off").HeaderText = "Advance Payment Knock Off Amount"

                    gv1.Columns("ActualAdvance_Payment_Amount").IsVisible = True
                    gv1.Columns("ActualAdvance_Payment_Amount").Width = 100
                    gv1.Columns("ActualAdvance_Payment_Amount").HeaderText = "Actual Advance Payment Amount"

                    gv1.Columns("DiffAdvance_Payment_Amount").IsVisible = True
                    gv1.Columns("DiffAdvance_Payment_Amount").Width = 100
                    gv1.Columns("DiffAdvance_Payment_Amount").HeaderText = "Diff Advance Payment Amount"

                    gv1.Columns("Payable_Amount").IsVisible = True
                    gv1.Columns("Payable_Amount").Width = 100
                    gv1.Columns("Payable_Amount").HeaderText = "Payable Amount"

                    gv1.Columns("Payable_Amount_Percentage").IsVisible = True
                    gv1.Columns("Payable_Amount_Percentage").Width = 100
                    gv1.Columns("Payable_Amount_Percentage").HeaderText = "Payable Amount(%)"

                    gv1.Columns("Total Amount").IsVisible = True
                    gv1.Columns("Total Amount").Width = 100
                    gv1.Columns("Total Amount").HeaderText = "Total Amount"

                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim summaryItem As New GridViewSummaryItem()

                    Dim item1 As GridViewSummaryItem

                    item1 = New GridViewSummaryItem("ProcessQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("DiffQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ProcessAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Incentive_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Total_EMP_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Service_Charge_Amt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Total_Invoice_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Credit_Note_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ProcessMCCSaleAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualMCCSaleAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("DiffMCCSaleAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ProcessAdvance_Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Advance_Payment_Amount_Knock_Off", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualAdvance_Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("DiffAdvance_Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Payable_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    gv1.ShowGroupPanel = False
                    gv1.MasterTemplate.AutoExpandGroups = True
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                ElseIf rbtnAdhoc.IsChecked Then
                    gv1.Columns("MCCCode").IsVisible = True
                    gv1.Columns("MCCCode").Width = 100
                    gv1.Columns("MCCCode").HeaderText = "MCC Code"

                    gv1.Columns("MCC_NAME").IsVisible = True
                    gv1.Columns("MCC_NAME").Width = 150
                    gv1.Columns("MCC_NAME").HeaderText = "MCC Name"

                    gv1.Columns("VSP_CODE").IsVisible = True
                    gv1.Columns("VSP_CODE").Width = 100
                    gv1.Columns("VSP_CODE").HeaderText = "VSP Code"

                    gv1.Columns("Vendor_Name").IsVisible = False
                    gv1.Columns("Vendor_Name").Width = 150
                    gv1.Columns("Vendor_Name").HeaderText = "VSP Name"

                    gv1.Columns("VLC_Code").IsVisible = True
                    gv1.Columns("VLC_Code").Width = 100
                    gv1.Columns("VLC_Code").HeaderText = "VLC Code"

                    gv1.Columns("VLC_Name").IsVisible = True
                    gv1.Columns("VLC_Name").Width = 150
                    gv1.Columns("VLC_Name").HeaderText = "VLC Name"

                    gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
                    gv1.Columns("VLC_Code_VLC_Uploader").Width = 150
                    gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Uploder Code"

                    gv1.Columns("ActualQty").IsVisible = True
                    gv1.Columns("ActualQty").Width = 100
                    gv1.Columns("ActualQty").HeaderText = "Actual Qty"

                    gv1.Columns("ActualAmount").IsVisible = True
                    gv1.Columns("ActualAmount").Width = 100
                    gv1.Columns("ActualAmount").HeaderText = "Actual Amount"

                    gv1.Columns("AdhocAmount").IsVisible = True
                    gv1.Columns("AdhocAmount").Width = 100
                    gv1.Columns("AdhocAmount").HeaderText = "Adhoc Amount"

                    gv1.Columns("mcc_sale_Amonut").IsVisible = True
                    gv1.Columns("mcc_sale_Amonut").Width = 100
                    gv1.Columns("mcc_sale_Amonut").HeaderText = "MCC Sale Amount"

                    gv1.Columns("Advance_Payment").IsVisible = True
                    gv1.Columns("Advance_Payment").Width = 100
                    gv1.Columns("Advance_Payment").HeaderText = "Advance Payment"

                    gv1.Columns("Payable_Amount").IsVisible = True
                    gv1.Columns("Payable_Amount").Width = 100
                    gv1.Columns("Payable_Amount").HeaderText = "Payable Amount"



                    gv1.Columns("Payable_Amount_Per").IsVisible = True
                    gv1.Columns("Payable_Amount_Per").Width = 100
                    gv1.Columns("Payable_Amount_Per").HeaderText = "% Payable Amount"


                    gv1.Columns("Vendor_Name").IsVisible = True
                    gv1.Columns("Vendor_Name").Width = 100
                    gv1.Columns("Vendor_Name").HeaderText = "VSP Name"


                    gv1.Columns("Sno").IsVisible = True
                    gv1.Columns("Sno").Width = 100
                    gv1.Columns("Sno").HeaderText = "Sno"

                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim summaryItem As New GridViewSummaryItem()

                    Dim item1 As GridViewSummaryItem

                    item1 = New GridViewSummaryItem("ActualQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("ActualAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("AdhocAmount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)


                    item1 = New GridViewSummaryItem("mcc_sale_Amonut", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)


                    item1 = New GridViewSummaryItem("Advance_Payment", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)


                    item1 = New GridViewSummaryItem("Payable_Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                    item1 = New GridViewSummaryItem("Payable_Amount_Per", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)



                    gv1.ShowGroupPanel = False
                    gv1.MasterTemplate.AutoExpandGroups = True
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
            End If
            

            ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If gv1.Columns.Count > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function GetReportID() As String
        Dim str As String = "MMSA"
        If chkSkippedDocument.Checked Then
            str += "SKIPDoc"
        Else
            If rbtnVLCWise.IsChecked Then
                str += "VLC"
            ElseIf rbtnMCCWise.IsChecked Then
                str += "MCC"
            ElseIf rbtnAdhoc.IsChecked Then
                str += "Adhoc"
            End If
        End If

        
        Return str
    End Function

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click

        gv1.DataSource = Nothing
        AddNew()
    End Sub

    Private Sub tbtnAdhoc_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnAdhoc.ToggleStateChanged
        SetAdhocPer()
    End Sub

    Sub SetAdhocPer()
        txtPer.Enabled = rbtnAdhoc.IsChecked
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            If gv1.Columns.Count > 0 Then
                gv1.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = GetReportID()
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gv1.SaveLayout(obj.GridLayout)
                obj.GridColumns = gv1.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
                End If
                ''stuti regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
   
    Private Sub cbxPaymentCycle_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles cbxPaymentCycle.ToggleStateChanged
        setFromAndToDate()
    End Sub

    Sub setFromAndToDate()
        If cbxPaymentCycle.Checked Then
            txtToDate.Enabled = True
        Else
            txtToDate.Enabled = False
            txtFromDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                SetToDate()
            End If
        End If
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Sub SetToDate()
        If cbxPaymentCycle.Checked = False Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code ='" & txtMCC.Value & "'")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    txtToDate.Value = txtFromDate.Value
                    Exit Sub
                End If
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
            End If
        End If
    End Sub

    Private Sub txtMonth_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        setFromAndToDate()
    End Sub

     
    Private Sub chkSkippedDocument_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSkippedDocument.ToggleStateChanged
        Panel2.Enabled = Not chkSkippedDocument.Checked
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Print(EnumExportTo.PDF)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMccSaleAdjustment & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
               

                If clsCommon.myLen(txtMCC.Value) > 0 Then
                    arrHeader.Add("MCC : " + txtMCC.Value)
                End If
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    arrHeader.Add("VSP :" + clsCommon.GetMulcallStringWithComma(txtVSP.arrValueMember))
                End If

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub txtBankCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBankCode._MYValidating
        Dim strWhrclas As String = ""
        Dim Qry As String = clsERPFuncationality.glbankqueryNew(strWhrclas)


        strWhrclas += " and TSPL_bank_master.INACTIVE ='Active' "

        txtBankCode.Value = clsCommon.ShowSelectForm("BankSlctr@Payment", Qry, "Code", strWhrclas, txtBankCode.Value, "Code", isButtonClicked)
        lblBankDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + txtBankCode.Value + "'")
        txtPaymentMode.Value = connectSql.RunScalar("select TSPL_PAYMENT_CODE.Payment_Code   from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code=  (select DISTINCT (case when Bank_type = 'C' THEN 'CASH' WHEN BANK_TYPE = 'B' THEN 'CHEQUE' WHEN BANK_TYPE = 'O' THEN 'OTHER' WHEN Bank_type = 'P' THEN 'PETTYCASH' END ) AS [Paymet Type] from TSPL_BANK_MASTER Where BANK_CODE='" + txtBankCode.Value + "' )")

    End Sub

    Private Sub txtPaymentMode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentMode._MYValidating
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtBankCode.Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtBankCode.Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS')", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtBankCode.Value)) <= 0 Then
                Throw New Exception("Please select Bank Code")
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtPaymentMode.Value)) <= 0 Then
                Throw New Exception("Please select Payment Mode")
            End If

            If clsCommon.MyMessageBoxShow("Are you sure to create Payment Entry of Advance type", "Advance Payment", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                funImport(False)

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub funImport(ByVal IsForPost As Boolean)
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Count As String = ""
        If transportSql.importExcel(gv, "MCCCode", "MCC_NAME", "VSP_CODE", "Vendor_Name", "VLC_Code", "VLC_Name", "VLC_Code_VLC_Uploader", "Payment Advance Amount", "Total Amount") Then
            Dim trans As SqlTransaction = Nothing
            Dim linno As Integer = 0
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarPercentShow()
                Dim obj As clsPaymentHeader
                For Each grow As GridViewRowInfo In gv.Rows
                    clsCommon.ProgressBarPercentUpdate((grow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (grow.Index + 1) & "/" & gv.Rows.Count & "")
                    Count = clsCommon.myCstr(grow.Index + 2)
                    If clsCommon.myCdbl(grow.Cells("Payment Advance Amount").Value) > 0 Then
                        obj = New clsPaymentHeader()
                        obj.Payment_No = ""

                        obj.Entry_Desc = "Advance Payment Entry Against Created of VSP-Code  " & clsCommon.myCstr(grow.Cells("VSP_Code").Value) & ""
                        If clsCommon.myLen(obj.Entry_Desc) > 250 Then
                            Throw New Exception("Description Length can not be more than 250.")
                        End If

                        obj.Payment_Date = clsCommon.myCDate(dtpPayment.Value)
                        obj.Payment_Post_Date = obj.Payment_Date

                        obj.Payment_Type = "AV"



                        obj.Vendor_Code = clsCommon.myCstr(grow.Cells("VSP_Code").Value)


                        If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                            obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Code from TSPL_VENDOR_MASTER WHERE Vendor_Code='" + obj.Vendor_Code + "'", trans))
                            If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                                Throw New Exception("Vendor Code does not exist.")
                            End If
                            obj.Vendor_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Name from TSPL_VENDOR_MASTER WHERE Vendor_Code='" + obj.Vendor_Code + "'", trans))
                        Else
                            Throw New Exception("Please select Vendor Code.")
                        End If

                        obj.Bank_Code = clsCommon.myCstr(txtBankCode.Value)
                        If clsCommon.myLen(obj.Bank_Code) > 0 Then
                            obj.Bank_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE from TSPL_BANK_MASTER WHERE Bank_Code='" + obj.Bank_Code + "'", trans))
                            If clsCommon.myLen(obj.Bank_Code) <= 0 Then
                                Throw New Exception("Bank Code does not exist.")
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_bank_master.INACTIVE from TSPL_BANK_MASTER Where Bank_Code='" & obj.Bank_Code & "'", trans)), "Active") <> CompairStringResult.Equal Then
                                Throw New Exception("Bank Code should be Active .")
                            End If
                        Else
                            Throw New Exception("Please select Bank Code.")
                        End If



                        obj.Payment_Code = clsCommon.myCstr(txtPaymentMode.Value)
                        If clsCommon.myLen(obj.Payment_Code) > 0 Then
                            obj.Payment_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_PAYMENT_CODE.Payment_Code from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code='" + obj.Payment_Code + "'", trans))
                            If clsCommon.myLen(obj.Payment_Code) <= 0 Then
                                Throw New Exception("Payment Mode does not exist.")
                            End If
                        Else
                            Throw New Exception("Enter Payment Mode.")
                        End If


                        If clsCommon.myCdbl(grow.Cells("Payment Advance Amount").Value) > clsCommon.myCdbl(grow.Cells("Total Amount").Value) Then
                            Throw New Exception("Payment Advance Amount cannot be greater than Total Amount for VSP " & clsCommon.myCstr(grow.Cells("VSP_CODE").Value) & "")
                        End If

                        obj.Account_Payee = 0
                        obj.Location_GL_Code = ""

                        obj.memorndmamt = 0.0

                        obj.CHECK_PRINT = 0


                        If clsCommon.CompairString(obj.Payment_Type, "AV") = CompairStringResult.Equal Then
                            obj.Total_Prepayment = clsCommon.myCdbl(grow.Cells("Payment Advance Amount").Value)
                            obj.Payment_Amount = clsCommon.myCdbl(grow.Cells("Payment Advance Amount").Value)
                            obj.Balance_Amt = clsCommon.myCdbl(grow.Cells("Payment Advance Amount").Value)
                            obj.TDS_Amount = 0
                        End If


                        obj.Is_Security = 0

                        obj.IsChkReverse = "N"
                        obj.Bank_Charges = 0
                        obj.objRemittance = Nothing
                        obj.PurchaseOrder_No = ""
                        obj.Loan_Code = ""
                        obj.Account_Payee_Name = ""
                        obj.CURRENCY_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE='" & obj.Vendor_Code & "'", trans))
                        obj.BASE_CURRENCY_CODE = objCommonVar.BaseCurrencyCode
                        obj.PAYMENT_AMOUNT_BASE_CURRENCY = obj.Payment_Amount
                        obj.ConvRateOld = 1
                        obj.ConvRate = 1
                        If clsCommon.CompairString(obj.CURRENCY_CODE, obj.BASE_CURRENCY_CODE) = CompairStringResult.Equal Then
                            obj.ConvRate = 1
                        End If
                        obj.PAYMENT_AMOUNT_BASE_CURRENCY = obj.Payment_Amount * obj.ConvRate
                        obj.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC,3) from TSPL_Bank_Master Where Bank_Code='" + obj.Bank_Code + "'", trans))
                        obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_Location_master where location_code='" + clsCommon.myCstr(grow.Cells("MCCCode").Value) + "'", trans))
                        If clsCommon.myLen(clsCommon.myCdbl(obj.Location_GL_Code)) <= 0 Then
                            Throw New Exception("Location Segment not found for Location " + clsCommon.myCstr(grow.Cells("MCCCode").Value) + "")
                        End If
                        obj.PDC_Cheque = "N'"
                        obj.Applied_Payment = ""
                        obj.ArrTr = Nothing

                        obj.SaveData1(obj, True, trans)
                        clsPaymentHeader.PostData(obj.Payment_No, "MPayable", trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow("Error at Line: " + Count + Environment.NewLine + ex.Message)
            Finally
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub
    Public Sub AddNew()
        GrpPaymentDetails.Visible = False
        GrpPaymentDetails.Enabled = False
        txtBankCode.Value = ""
        lblBankDesc.Text = ""
        txtPaymentMode.Value = ""
        dtpPayment.Value = clsCommon.GETSERVERDATE()
        Panel1.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        AddNew()
    End Sub

    Private Sub btnCreatePayment_Click(sender As Object, e As EventArgs) Handles btnCreatePayment.Click
        AddNew()
        GrpPaymentDetails.Visible = True
        GrpPaymentDetails.Enabled = True
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Try
            gv1.DataSource = Nothing
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                Throw New Exception("Please Select MCC")
            End If
            Dim BaseQry As String
            Dim qry As String = ""
            BaseQry = "select TSPL_LOCATION_MASTER.Location_Code as MCCCode,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Qty,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Service_Charge_Amt,TSPL_PAYMENT_PROCESS_DETAIL.Total_Invoice_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,(TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount-(select ISNULL(sum(Reduce_Deduc_Amt),0) from TSPL_PAYMENT_PROCESS_MCC_SALE where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No and TSPL_PAYMENT_PROCESS_MCC_SALE.Customer_CODE=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE)) as MCC_Sale_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount_Knock_Off,1 as RI " + Environment.NewLine +
            " from TSPL_PAYMENT_PROCESS_DETAIL" + Environment.NewLine +
            " left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No" + Environment.NewLine +
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code" + Environment.NewLine +
            " where TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1" + Environment.NewLine +
            " and TSPL_LOCATION_MASTER.Location_Code='" + txtMCC.Value + "' " + Environment.NewLine +
            " and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'" + Environment.NewLine
            If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                End If
                BaseQry += " union all" + Environment.NewLine +
                " select TSPL_MILK_SRN_HEAD.MCC_CODE as MCCCode, VSP_CODE,ACC_Qty as Milk_Qty,  TSPL_MILK_SRN_DETAIL.AMOUNT  as Milk_Amount,0 as Incentive_Amount,0 as Total_EMP_Amount,0 as Service_Charge_Amt,0 as Total_Invoice_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,0 as Payable_Amount,0 as MCC_Sale_Amount,0 as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,2 as RI from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                " where TSPL_MILK_SRN_HEAD.Posted=1 and TSPL_MILK_SRN_HEAD.MCC_CODE='" + txtMCC.Value + "' and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    BaseQry += " and VSP_CODE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                End If
                BaseQry += " union all" + Environment.NewLine +
                " select TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as MCCCode,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code as VSP_CODE,0 as Milk_Qty,0 as Milk_Amount,0 as Incentive_Amount,0 as Total_EMP_Amount,0 as Service_Charge_Amt,0 as Total_Invoice_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,0 as Payable_Amount,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as MCC_Sale_Amount,0 as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,3 as RI  " + Environment.NewLine +
                " from TSPL_SD_SALE_INVOICE_HEAD  " + Environment.NewLine +
                " left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code     " + Environment.NewLine +
                " where TSPL_SD_SALE_INVOICE_HEAD.Status=1 and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + txtMCC.Value + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                End If
                BaseQry += " union all" + Environment.NewLine +
                " select TSPL_VLC_MASTER_HEAD.MCC as MCCCode,TSPL_VLC_MASTER_HEAD.VSP_Code ,0 as Milk_Qty,0 as Milk_Amount,0 as Incentive_Amount,0 as Total_EMP_Amount,0 as Service_Charge_Amt,0 as Total_Invoice_Amount,0 as Deduction_Amount,0 as Credit_Note_Amount,0 as Payable_Amount,0 as MCC_Sale_Amount,(TSPL_PAYMENT_HEADER.Payment_Amount+isnull(TDS_Amount ,0)) as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,4 as RI  " + Environment.NewLine +
                " from TSPL_PAYMENT_HEADER  " + Environment.NewLine +
                " inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_HEADER.Vendor_Code     " + Environment.NewLine +
                " where TSPL_PAYMENT_HEADER.Posted=1 and TSPL_PAYMENT_HEADER.Payment_Type IN ('AV','OA') and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' and TSPL_PAYMENT_HEADER.Payment_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_PAYMENT_HEADER.Payment_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")" + Environment.NewLine
                End If
                If rbtnVLCWise.IsChecked Then
                qry = "select MCCCode,max(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,xxx.VSP_CODE,max(TSPL_VENDOR_MASTER.Vendor_Name) as Vendor_Name,max(TSPL_VLC_MASTER_HEAD.VLC_Code) as VLC_Code,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_Name,max(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader" + Environment.NewLine +
                                        " ,0 as [Payment Advance Amount],convert(decimal(18,2), ROUND( sum(Milk_Amount * case when RI=2 then 1 else 0 end ),2,0))+sum(Credit_Note_Amount)-sum(Deduction_Amount)-sum( MCC_Sale_Amount * case when RI=3 then 1 else 0 end ) [Total Amount]" + Environment.NewLine +
                    " from (" + Environment.NewLine + BaseQry + Environment.NewLine +
                           ")xxx " + Environment.NewLine +
                    " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCCCode" + Environment.NewLine +
                    " inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xxx.VSP_CODE" + Environment.NewLine +
                    " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xxx.VSP_CODE" + Environment.NewLine +
                    " group by xxx.MCCCode,xxx.VSP_CODE"

            End If

            'transportSql.ExporttoExcel(qry, "", Me)
            transportSql.BulkExport("Advance Payment Entry", qry, "", "xls")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class

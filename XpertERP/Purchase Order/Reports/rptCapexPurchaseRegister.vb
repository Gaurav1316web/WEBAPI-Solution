Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class rptCapexPurchaseRegister

#Region "Variables"
    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrCapex As ArrayList
    Public arrSubCapex As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLocation As ArrayList
    Public arrCustomer As ArrayList
    Public arrCustGroup As ArrayList
    Public arrItemGroup As ArrayList
    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim strPivotForAddChargeFinalOutersumQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
#End Region

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnBulkExp.Visible = MyBase.isExport
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub

    Public Function getquery() As String
        Dim uom As String = txtUOM.Value
        Dim capexarr As ArrayList = txtcapex.arrValueMember
        Dim subcapexarr As ArrayList = txtsubcapex.arrValueMember
        Dim locationarr As ArrayList = txtLocation.arrValueMember
        Dim Itemarr As ArrayList = txtItem.arrValueMember
        Dim VendorArr As ArrayList = txtvendor.arrValueMember
        Dim VendorGrpArr As ArrayList = txtVenGroup.arrValueMember

        Dim CapesQuery As String = Nothing
        Dim FAQry As String = Nothing
        Dim qry As String
        qry = "SELECT 'PO' as [Type],TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as [Doc No] ,MAX(CONVERT(VARCHAR,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)) as [Doc Date],TSPL_PURCHASE_ORDER_DETAIL.Item_Code as [Item Code],case when max(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type) ='J' then TSPL_PURCHASE_ORDER_DETAIL.Item_Code else  MAX(TSPL_PURCHASE_ORDER_DETAIL.ITEM_DESC) end  as [Item Description],MAX(TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_QTY) as [Purchase Order Qty],MAX(TSPL_PURCHASE_ORDER_DETAIL.Unit_code) as [Unit Code]," & _
            " MAX(TSPL_PURCHASE_ORDER_DETAIL.Item_Cost) as [Item Cost],MAX(TSPL_PURCHASE_ORDER_DETAIL.Item_Net_Amt) as [Total Amount],TSPL_GRN_DETAIL.GRN_No as [GRN No],MAX(CONVERT(VARCHAR,TSPL_GRN_HEAD.GRN_Date,103)) as [GRN Date],MAX(TSPL_GRN_DETAIL.GRN_Qty) as [GRN Qty],TSPL_MRN_DETAIL.MRN_No as [MRN No],MAX(CONVERT(VARCHAR,TSPL_MRN_HEAD.MRN_Date,103)) as [MRN Date],MAX(TSPL_MRN_DETAIL.MRN_Qty) as [MRN Qty]," & _
            " TSPL_SRN_DETAIL.SRN_No as [SRN No],MAX(CONVERT(VARCHAR,TSPL_SRN_HEAD.SRN_Date,103)) as [SRN Date],MAX(TSPL_SRN_DETAIL.SRN_Qty) as [SRN Qty],TSPL_PI_DETAIL.PI_No as [Invoice No],MAX(CONVERT(VARCHAR,TSPL_PI_HEAD.PI_Date,103)) as [Invoice Date],MAX(TSPL_PI_DETAIL.PI_Qty) as [Invoice Qty],MAX(TSPL_PI_DETAIL.Item_Net_Amt) as [Invoice Amount],TSPL_PR_DETAIL.PR_No as [Purchase Return No]," & _
            " MAX(CONVERT(VARCHAR,TSPL_PR_HEAD.PR_Date,103)) as [Purchase Return Date],MAX(TSPL_PR_DETAIL.PR_Qty) as [Purchase Return Qty],MAX(TSPL_PURCHASE_ORDER_HEAD.Capex_Code) as [Capex Code],MAX(TSPL_CAPEX_MASTER.Description) as [Capex Description],max((case when (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0))=0 then (isnull(TSPL_CAPEX_MASTER.Budget,0))else " & _
            " (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0)) end)*TSPL_CAPEX_MASTER.Tolerence/100+(case when (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0))=0 then (isnull(TSPL_CAPEX_MASTER.Budget,0)) else (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0)) end)) as MainCapxBudget,(select sum(p.PO_Total_Amt) from TSPL_PURCHASE_ORDER_HEAD as p where p.IsCancel<>1 and isnull(p.capex_code,'')=MAX(TSPL_PURCHASE_ORDER_HEAD.Capex_Code) and isnull(p.Capex_SubCode,'')=MAX(TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode)) as [Capex Total Amount]," & _
            " MAX(TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode) as [Capex Sub Code],MAX(TSPL_CAPEX_BUDGET_MASTER.Description) as [Capex Sub Description],case when MAX(ISNULL(TSPL_CAPEX_BUDGET_MASTER.Revised_Budget,0))=0 then MAX(ISNULL(TSPL_CAPEX_BUDGET_MASTER.Budget,0)) ELSE MAX(ISNULL(TSPL_CAPEX_BUDGET_MASTER.Revised_Budget,0)) END as [Capex Sub Budget],max(TSPL_CAPEX_BUDGET_MASTER.Tolerence) as Tolerence,MAX(TSPL_PURCHASE_ORDER_HEAD.Vendor_Code) as [Vendor Code],MAX(TSPL_VENDOR_MASTER.VENDOR_Name) as [Vendor Name],MAX(TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location) AS [Location] FROM TSPL_PURCHASE_ORDER_DETAIL" & _
            " LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO=TSPL_PURCHASE_ORDER_HEAD.PURCHASEORDER_NO" & _
            " LEFT OUTER JOIN TSPL_GRN_DETAIL ON TSPL_GRN_DETAIL.PO_ID=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code" & _
            " LEFT OUTER JOIN TSPL_GRN_HEAD ON TSPL_GRN_HEAD.AGAINST_PO=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No" & _
            " LEFT OUTER JOIN TSPL_MRN_DETAIL ON TSPL_MRN_DETAIL.PO_ID=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_MRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code AND TSPL_MRN_DETAIL.GRN_Id=TSPL_GRN_DETAIL.GRN_No" & _
            " LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.AGAINST_PO=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_DETAIL.GRN_No AND TSPL_MRN_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No" & _
            " LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.PO_ID=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code AND TSPL_SRN_DETAIL.GRN_Id=TSPL_GRN_DETAIL.GRN_No AND TSPL_SRN_DETAIL.MRN_Id=TSPL_MRN_DETAIL.MRN_No" & _
            " LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.AGAINST_PO=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_SRN_HEAD.Against_GRN=TSPL_GRN_DETAIL.GRN_No AND TSPL_SRN_HEAD.AGAINST_MRN=TSPL_MRN_DETAIL.MRN_No AND TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No" & _
            " LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_DETAIL.PO_ID=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_PI_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code AND TSPL_PI_DETAIL.GRN_Id=TSPL_GRN_DETAIL.GRN_No AND TSPL_PI_DETAIL.MRN_Id=TSPL_MRN_DETAIL.MRN_No AND TSPL_PI_DETAIL.SRN_Id=TSPL_SRN_DETAIL.SRN_No" & _
            " LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.AGAINST_PO=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_PI_HEAD.Against_GRN=TSPL_GRN_DETAIL.GRN_No AND TSPL_PI_HEAD.AGAINST_MRN=TSPL_MRN_DETAIL.MRN_No AND TSPL_PI_HEAD.AGAINST_SRN=TSPL_SRN_DETAIL.SRN_No AND TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No" & _
            " LEFT OUTER JOIN TSPL_PR_DETAIL ON TSPL_PR_DETAIL.PO_ID=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_PR_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code AND TSPL_PR_DETAIL.GRN_Id=TSPL_GRN_DETAIL.GRN_No AND TSPL_PR_DETAIL.MRN_Id=TSPL_MRN_DETAIL.MRN_No AND TSPL_PR_DETAIL.SRN_Id=TSPL_SRN_DETAIL.SRN_No AND TSPL_PR_DETAIL.PI_Id=TSPL_PI_DETAIL.PI_No" & _
            " LEFT OUTER JOIN TSPL_PR_HEAD ON TSPL_PR_HEAD.AGAINST_PO=TSPL_PURCHASE_ORDER_DETAIL.PURCHASEORDER_NO AND TSPL_PR_HEAD.Against_GRN=TSPL_GRN_DETAIL.GRN_No AND TSPL_PR_HEAD.AGAINST_MRN=TSPL_MRN_DETAIL.MRN_No AND TSPL_PR_HEAD.AGAINST_SRN=TSPL_SRN_DETAIL.SRN_No AND TSPL_PR_HEAD.AGAINST_PI=TSPL_PI_DETAIL.PI_No AND TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No" & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PURCHASE_ORDER_HEAD.Vendor_Code" & _
            " LEFT OUTER JOIN TSPL_CAPEX_MASTER ON TSPL_CAPEX_MASTER.Code =TSPL_PURCHASE_ORDER_HEAD.Capex_Code" & _
            " LEFT OUTER JOIN TSPL_CAPEX_BUDGET_MASTER ON TSPL_CAPEX_BUDGET_MASTER.Code =TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode" & _
            " where TSPL_PURCHASE_ORDER_HEAD.Category='Capex' "
        If clsCommon.myLen(txtUOM.Value) > 0 Then
            qry += " AND TSPL_PURCHASE_ORDER_DETAIL.Unit_code ='" + clsCommon.myCstr(txtUOM.Value) + "'"
        End If
        If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
            qry += " AND TSPL_PURCHASE_ORDER_HEAD.Capex_Code IN (" + clsCommon.GetMulcallString(capexarr) + ")"
        End If
        If subcapexarr IsNot Nothing AndAlso subcapexarr.Count > 0 Then
            qry += " AND TSPL_PURCHASE_ORDER_HEAD.Capex_SubCode IN (" + clsCommon.GetMulcallString(subcapexarr) + ")"
        End If
        If locationarr IsNot Nothing AndAlso locationarr.Count > 0 Then
            qry += " AND TSPL_PURCHASE_ORDER_DETAIL.Location IN (" + clsCommon.GetMulcallString(locationarr) + ")"
        End If
        If VendorArr IsNot Nothing AndAlso VendorArr.Count > 0 Then
            qry += " AND TSPL_PURCHASE_ORDER_HEAD.Vendor_Code IN (" + clsCommon.GetMulcallString(VendorArr) + ")"
        End If
        If VendorGrpArr IsNot Nothing AndAlso VendorGrpArr.Count > 0 Then
            qry += " AND TSPL_VENDOR_MASTER.Vendor_Group_Code IN (" + clsCommon.GetMulcallString(VendorGrpArr) + ")"
        End If
        If Itemarr IsNot Nothing AndAlso Itemarr.Count > 0 Then
            qry += " AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code IN (" + clsCommon.GetMulcallString(Itemarr) + ")"
        End If
        qry += " AND convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103) AND convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103) "
        qry += " GROUP BY TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,TSPL_GRN_DETAIL.GRN_No,TSPL_MRN_DETAIL.MRN_No,TSPL_SRN_DETAIL.SRN_No,TSPL_PI_DETAIL.PI_No,TSPL_PR_DETAIL.PR_No,TSPL_PURCHASE_ORDER_DETAIL.Item_Code"
        '======================================
        'If (chkFA.Checked = False Or chkCapex.Checked = False) Then
        '    qry += " Union all " & _
        '        ""

        'End If

        '======================================
        qry += " Union All " & _
               " select 'Capex Transfer' as [Type],TSPL_IssueReturn_HEAD.Doc_No as [Document Code],convert(varchar,TSPL_IssueReturn_HEAD.Doc_Date,103) as [Document Date], TSPL_IssueReturn_DETAIL.Item_Code as Item_Code,TSPL_IssueReturn_DETAIL.Item_Desc as Item_Desc,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty," & _
               " TSPL_IssueReturn_DETAIL.Unit_code as Unit_Code,TSPL_IssueReturn_DETAIL.Unit_Cost as Item_Cost,TSPL_IssueReturn_DETAIL.Item_Net_Amt as [Net Amount],'' as GRN_No,Null as GRNDate, 0 as GRN_Qty,'' as MRNNo,Null as MRNDate,0 as MRN_Qty,'' as SRNNo,Null as SRN_Date,0 as SRN_Qty,'' as Invoice_No,Null as Invoice_Date,0 as Invoice_Qty, 0 as Invoice_Amount,'' as Purchase_Return_no,Null as Purchase_Return_Date, 0 as Purchase_Return_Qty,TSPL_IssueReturn_HEAD.Capex_Code, TSPL_CAPEX_MASTER.Description as Capex_Desc," & _
               " ((case when (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0))=0 then (isnull(TSPL_CAPEX_MASTER.Budget,0))else  (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0)) end)*TSPL_CAPEX_MASTER.Tolerence/100+(case when (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0))=0 then (isnull(TSPL_CAPEX_MASTER.Budget,0)) else (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0)) end)) as MainCapxBudget ,TSPL_IssueReturn_DETAIL.Item_Net_Amt as Capex_Total_Amount,TSPL_IssueReturn_HEAD.Capex_SubCode, TSPL_CAPEX_BUDGET_MASTER.Description as Capex_Sub_Desc,case when (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Revised_Budget,0))=0 then (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Budget,0)) ELSE (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Revised_Budget,0)) end as Capex_Sub_Budget ,TSPL_CAPEX_BUDGET_MASTER.Tolerence as Tolerance," & _
               " '' as Vendor_Code,'' as Vendor_Name,TSPL_IssueReturn_HEAD.From_Location " & _
               " from TSPL_IssueReturn_DETAIL " & _
               " left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_DETAIL.Doc_No=TSPL_IssueReturn_HEAD.Doc_No " & _
               " LEFT OUTER JOIN TSPL_CAPEX_MASTER ON TSPL_CAPEX_MASTER.Code =TSPL_IssueReturn_HEAD.Capex_Code " & _
               " LEFT OUTER JOIN TSPL_CAPEX_BUDGET_MASTER ON TSPL_CAPEX_BUDGET_MASTER.Code =TSPL_IssueReturn_HEAD.Capex_SubCode " & _
               " where TSPL_IssueReturn_HEAD.Doc_Type='TransferCX'  and TSPL_IssueReturn_HEAD.Capex_SubCode is not null " & _
               " and TSPL_IssueReturn_HEAD.Status=1 AND convert(date,TSPL_IssueReturn_HEAD.Doc_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103) " & _
               " AND convert(date,TSPL_IssueReturn_HEAD.Doc_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103) "

        If chkFA.Checked Then
            qry += " union all" & _
                   " select 'FA' as [Type],TSPL_ASSET_WORK_HEAD.Document_Code as [Document Code],convert(varchar,TSPL_ASSET_WORK_HEAD.Document_Date,103) as [Document Date]," & _
                   " '' as Item_Code,'' as Item_Desc,0 as Qty,'' as Unit_Code,0 as Item_Cost,TSPL_ASSET_WORK_DETAIL.Item_Net_Amt as [Net Amount],'' as GRN_No,Null as GRNDate," & _
                   " 0 as GRN_Qty,'' as MRNNo,Null as MRNDate,0 as MRN_Qty,'' as SRNNo,Null as SRN_Date,0 as SRN_Qty,'' as Invoice_No,Null as Invoice_Date,0 as Invoice_Qty," & _
                   " 0 as Invoice_Amount,'' as Purchase_Return_no,Null as Purchase_Return_Date, 0 as Purchase_Return_Qty,TSPL_ACQUISITION_DETAIL.Capex_Code," & _
                   " TSPL_CAPEX_MASTER.Description as Capex_Desc,((case when (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0))=0 then (isnull(TSPL_CAPEX_MASTER.Budget,0))else  (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0)) end)*TSPL_CAPEX_MASTER.Tolerence/100+(case when (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0))=0 then (isnull(TSPL_CAPEX_MASTER.Budget,0)) else (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0)) end)) as MainCapxBudget ,TSPL_ASSET_WORK_DETAIL.Item_Net_Amt as Capex_Total_Amount,TSPL_ACQUISITION_DETAIL.Capex_SubCode," & _
                   " TSPL_CAPEX_BUDGET_MASTER.Description as Capex_Sub_Desc,case when (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Revised_Budget,0))=0 then (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Budget,0)) ELSE (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Revised_Budget,0)) end as Capex_Sub_Budget" & _
                   " ,TSPL_CAPEX_BUDGET_MASTER.Tolerence as Tolerance,TSPL_ASSET_WORK_head.vendor_Code as Vendor_Code,tspl_vendor_Master.vendor_Name as Vendor_Name,TSPL_ASSET_WORK_head.Location_Code from TSPL_ACQUISITION_DETAIL " & _
                   " left outer join TSPL_ACQUISITION_head on TSPL_ACQUISITION_head.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code " & _
                   " left outer join TSPL_ASSET_WORK_DETAIL on TSPL_ASSET_WORK_DETAIL.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code " & _
                   " left outer join TSPL_ASSET_WORK_HEAD on TSPL_ASSET_WORK_DETAIL.Document_Code=TSPL_ASSET_WORK_HEAD.Document_Code " & _
                   " LEFT OUTER JOIN TSPL_CAPEX_MASTER ON TSPL_CAPEX_MASTER.Code =TSPL_ACQUISITION_DETAIL.Capex_Code " & _
                   " LEFT OUTER JOIN TSPL_CAPEX_BUDGET_MASTER ON TSPL_CAPEX_BUDGET_MASTER.Code =TSPL_ACQUISITION_DETAIL.Capex_SubCode  " & _
                   " left join tspl_vendor_Master on tspl_vendor_Master.vendor_Code=TSPL_ASSET_WORK_head.vendor_Code" & _
                   " where TSPL_ACQUISITION_DETAIL.capexType='Capex'  and TSPL_ASSET_WORK_HEAD.Status=1 and isnull(TSPL_ASSET_WORK_HEAD.refdoctype,'')<>'WO'" & _
                   " AND convert(date,TSPL_ACQUISITION_head.Acquisition_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103) AND convert(date,TSPL_ACQUISITION_head.Acquisition_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103) "

            If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                qry += " AND TSPL_ACQUISITION_DETAIL.Capex_Code IN (" + clsCommon.GetMulcallString(capexarr) + ")"
            End If
            If subcapexarr IsNot Nothing AndAlso subcapexarr.Count > 0 Then
                qry += " AND TSPL_ACQUISITION_DETAIL.Capex_SubCode IN (" + clsCommon.GetMulcallString(subcapexarr) + ")"
            End If
            If locationarr IsNot Nothing AndAlso locationarr.Count > 0 Then
                qry += " AND TSPL_ASSET_WORK_head.Location_Code  IN (" + clsCommon.GetMulcallString(locationarr) + ")"
            End If
            If VendorArr IsNot Nothing AndAlso VendorArr.Count > 0 Then
                qry += " AND TSPL_ASSET_WORK_head.vendor_Code IN (" + clsCommon.GetMulcallString(VendorArr) + ")"
            End If
            If VendorGrpArr IsNot Nothing AndAlso VendorGrpArr.Count > 0 Then
                qry += " AND TSPL_VENDOR_MASTER.Vendor_Group_Code IN (" + clsCommon.GetMulcallString(VendorGrpArr) + ")"
            End If
            'If Itemarr IsNot Nothing AndAlso Itemarr.Count > 0 Then
            '    qry += " AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code IN (" + clsCommon.GetMulcallString(Itemarr) + ")"
            'End If
            qry += " union all" & _
                 " select 'FA' as [Type],TSPL_IssueItemToAssembledAsset_Head.Doc_No  as [Document Code],convert(varchar,TSPL_IssueItemToAssembledAsset_Head.Doc_Date ,103) as [Document Date]," & _
                " TSPL_IssueItemToAssembledAsset_Detail.item_code as Item_Code,TSPL_IssueItemToAssembledAsset_Detail.item_desc as Item_Desc,0 as Qty,'' as Unit_Code,0 as Item_Cost,TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt as [Net Amount]," & _
                " '' as GRN_No,Null as GRNDate,0 as GRN_Qty,'' as MRNNo,Null as MRNDate,0 as MRN_Qty,'' as SRNNo,Null as SRN_Date,0 as SRN_Qty,'' as Invoice_No," & _
                " Null as Invoice_Date,0 as Invoice_Qty,0 as Invoice_Amount,'' as Purchase_Return_no,Null as Purchase_Return_Date, 0 as Purchase_Return_Qty," & _
                " TSPL_ACQUISITION_DETAIL.Capex_Code,TSPL_CAPEX_MASTER.Description as Capex_Desc,((case when (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0))=0 then (isnull(TSPL_CAPEX_MASTER.Budget,0))else  (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0)) end)*TSPL_CAPEX_MASTER.Tolerence/100+(case when (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0))=0 then (isnull(TSPL_CAPEX_MASTER.Budget,0)) else (isnull(TSPL_CAPEX_MASTER.Revised_Budget,0)) end)) as MainCapxBudget,  CASE WHEN  TSPL_IssueItemToAssembledAsset_Head.Doc_Type ='Issue' THEN  TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt ELSE TSPL_IssueItemToAssembledAsset_Detail.Item_Net_Amt*(-1) END  as Capex_Total_Amount," & _
                " TSPL_ACQUISITION_DETAIL.Capex_SubCode,TSPL_CAPEX_BUDGET_MASTER.Description as Capex_Sub_Desc," & _
                " case when (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Revised_Budget,0))=0 then (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Budget,0)) ELSE (ISNULL(TSPL_CAPEX_BUDGET_MASTER.Revised_Budget,0)) end as Capex_Sub_Budget" & _
                " ,TSPL_CAPEX_BUDGET_MASTER.Tolerence as Tolerance,'' as Vendor_Code,'' as Vendor_Name,TSPL_IssueItemToAssembledAsset_Head.From_Location " & _
                " from TSPL_ACQUISITION_DETAIL" & _
                " left outer join TSPL_ACQUISITION_head on TSPL_ACQUISITION_head.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code " & _
                " left outer join TSPL_IssueItemToAssembledAsset_Detail on TSPL_IssueItemToAssembledAsset_Detail.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code " & _
                " left outer join TSPL_IssueItemToAssembledAsset_Head on TSPL_IssueItemToAssembledAsset_Detail.Doc_No =TSPL_IssueItemToAssembledAsset_Head.Doc_No " & _
                " LEFT OUTER JOIN TSPL_CAPEX_MASTER ON TSPL_CAPEX_MASTER.Code =TSPL_ACQUISITION_DETAIL.Capex_Code LEFT OUTER JOIN TSPL_CAPEX_BUDGET_MASTER ON TSPL_CAPEX_BUDGET_MASTER.Code =TSPL_ACQUISITION_DETAIL.Capex_SubCode  " & _
                " where TSPL_ACQUISITION_DETAIL.capexType='Capex' and TSPL_IssueItemToAssembledAsset_Detail .CheckCapexLimit =1 and TSPL_IssueItemToAssembledAsset_Head.Status=1" & _
             " AND convert(date,TSPL_ACQUISITION_head.Acquisition_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103) AND convert(date,TSPL_ACQUISITION_head.Acquisition_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103) "

            If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
                qry += " AND TSPL_ACQUISITION_DETAIL.Capex_Code IN (" + clsCommon.GetMulcallString(capexarr) + ")"
            End If
            If subcapexarr IsNot Nothing AndAlso subcapexarr.Count > 0 Then
                qry += " AND TSPL_ACQUISITION_DETAIL.Capex_SubCode IN (" + clsCommon.GetMulcallString(subcapexarr) + ")"
            End If
            If locationarr IsNot Nothing AndAlso locationarr.Count > 0 Then
                qry += " AND TSPL_IssueItemToAssembledAsset_Head.From_Location IN (" + clsCommon.GetMulcallString(locationarr) + ")"
            End If
            'If VendorArr IsNot Nothing AndAlso VendorArr.Count > 0 Then
            '    qry += " AND TSPL_ASSET_WORK_head.vendor_Code IN (" + clsCommon.GetMulcallString(VendorArr) + ")"
            'End If
            'If VendorGrpArr IsNot Nothing AndAlso VendorGrpArr.Count > 0 Then
            '    qry += " AND TSPL_VENDOR_MASTER.Vendor_Group_Code IN (" + clsCommon.GetMulcallString(VendorGrpArr) + ")"
            'End If
            If Itemarr IsNot Nothing AndAlso Itemarr.Count > 0 Then
                qry += " AND TSPL_IssueItemToAssembledAsset_Detail.item_code IN (" + clsCommon.GetMulcallString(Itemarr) + ")"
            End If
        End If
        Dim Checked As Boolean = False
        If chkCapex.Checked Then
            Checked = True
            CapesQuery = " select [Type], [Capex Code],max([Capex Description])as [Capex Description],max([Capex Total Amount PO])+sum([Capex Total Amount FA])  as [Capex Total Amount],max([Capex Sub Code]) as [Capex Sub Code],max([Capex Sub Description]) as [Capex Sub Description],max([Capex Sub Budget]) as [Capex Sub Budget],max(Tolerence) as Tolerence,max([Capex Sub Budget])*max(Tolerence)/100 as [Tolerence value],(max([Capex Sub Budget])+(max([Capex Sub Budget])*max(Tolerence)/100))-(max([Capex Total Amount PO])+sum([Capex Total Amount FA]))  as [Capex Balance]from ( "
            CapesQuery += " select *,case when type='PO' then [Capex Total Amount] else 0 end as  [Capex Total Amount PO] ,case when type='FA' then [Capex Total Amount] else 0 end as  [Capex Total Amount FA]"
            CapesQuery += " from ( "
            CapesQuery += "" & (qry) & ""
            CapesQuery += " ) as xx ) as Capex group by Capex.[Capex Code],Capex.[Type]"
            qry = CapesQuery
        End If
        If chkFA.Checked AndAlso chkCapex.Checked Then
            Checked = True
            FAQry = " select [Capex Code],max([Capex Description])as [Capex Description],sum([Capex Total Amount]) as [Capex Total Amount],max([Capex Sub Code]) as [Capex Sub Code],max([Capex Sub Description]) as [Capex Sub Description],max([Capex Sub Budget]) as [Capex Sub Budget],max(Tolerence) as Tolerence,max([Tolerence Value]) as [Tolerence Value],max([Capex Sub Budget])+MAX([Tolerence value])-(SUM([Capex Total Amount])) as [Capex Balance] from ( "
            FAQry += "" & (CapesQuery) & ""
            FAQry += " ) as Capex1 group by Capex1.[Capex Code] order by [Capex Code] "
            qry = FAQry
        End If
        If Checked = False Then
            qry = "SELECT [Type],[Doc No] ,[Doc Date],[Item Code],[Item Description],[Purchase Order Qty],[Unit Code]," & _
            " [Item Cost],[Total Amount],[GRN No],[GRN Date],[GRN Qty],[MRN No],[MRN Date],[MRN Qty]," & _
            " [SRN No],[SRN Date],[SRN Qty],[Invoice No],[Invoice Date],[Invoice Qty],[Invoice Amount],[Purchase Return No]," & _
            " [Purchase Return Date],[Purchase Return Qty],[Capex Code],[Capex Description], MainCapxBudget,sum([Total Amount]) over(partition by [Capex Sub Code] order by [Doc No]) as [Capex Total Amount]," & _
            " [Capex Sub Code],[Capex Sub Description],[Capex Sub Budget],Tolerence,[Vendor Code],[Vendor Name],[Location],convert(decimal(18,2),(SELECT (case when ISNULL(TSPL_CAPEX_BUDGET_MASTER .Revised_Budget,0 )>0 then TSPL_CAPEX_BUDGET_MASTER.Revised_Budget else TSPL_CAPEX_BUDGET_MASTER .Budget end) as [Budget] FROM TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code=[Capex Sub Code])-sum([Total Amount]) over(partition by [Capex Sub Code] order by [Doc No])) as [Balance Sub Capex]  from (" & qry & ") as Final"
        End If
        Return qry

    End Function

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If Not IsNothing(txtItem.arrValueMember) Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If Not IsNothing(txtVenGroup.arrValueMember) Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVenGroup.arrDispalyMember))
            End If
            If Not IsNothing(txtvendor.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtvendor.arrDispalyMember))
            End If
            If btnAll.CheckState = True Or btnPosted.CheckState = True Or btnUnposted.CheckState = True Then
                arrHeader.Add("Status : " + IIf(btnAll.IsChecked, "ALL", IIf(btnPosted.IsChecked, "Posted", "Unposted")))
            End If

            clsCommon.ProgressBarShow()
            txtLocation.Enabled = False
            txtcapex.Enabled = False
            txtsubcapex.Enabled = False
            txtvendor.Enabled = False
            txtVenGroup.Enabled = False

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Unit_Code = txtUOM.Value
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            strRunQuery = getquery()

            If BulkExport = 1 Then
                transportSql.BulkExport("Capex Purchase Register", strRunQuery, "", "csv")
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("Capex Purchase Register", strRunQuery, "", "xls")
            End If

            RadPageViewPage2.Text = "Report"
            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)

                Exit Sub
            Else
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            'FindAndRestoreGridLayout(Me)
            Gv1.MasterTemplate.AllowAddNewRow = False
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2


        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtUOM.Value = ""
        Document_No = ""
        LoadCategory()
        txtLocation.arrValueMember = Nothing
        txtcapex.arrValueMember = Nothing
        txtsubcapex.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtvendor.arrValueMember = Nothing
        txtVenGroup.arrValueMember = Nothing
        rbtnCategoryAll.IsChecked = True

        txtLocation.Enabled = True
        txtcapex.Enabled = True
        txtsubcapex.Enabled = True
        txtItem.Enabled = True
        txtvendor.Enabled = True
        txtVenGroup.Enabled = True

        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = "Report"
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)

                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = ReportId()
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Function ReportId()
        Dim Report_Id As String = ""
        Report_Id = MyBase.Form_ID
        If chkFA.Checked = True And chkCapex.Checked = True Then
            Report_Id += "CF"
        ElseIf chkCapex.Checked = True Then
            Report_Id += "C"
        End If
        Return Report_Id
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptPurchaseRegisterReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub RptPurchaseRegisterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtUOM.Value = Unit_Code
            txtLocation.arrValueMember = arrLocation
            txtItem.arrValueMember = arrItem
            txtvendor.arrValueMember = arrCustomer
            txtcapex.arrValueMember = arrCapex
            txtsubcapex.arrValueMember = arrSubCapex
            If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                rbtnCategorySelect.IsChecked = True
                For Each str As String In arrCat.Keys
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvCategory.Rows(ii).Cells("SEL").Value = True
                            gvCategory.Rows(ii).Tag = arrCat(str)
                        End If
                    Next
                Next
            End If
            Print(True)
            Me.Visible = True
        End If
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtvendor._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtvendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Name", txtvendor.arrValueMember, txtvendor.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtvendor, "Vendor_name", "TSPL_VENDOR_master", "Vendor_Code")
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtItem, "Item_Desc", "TSPL_ITEM_MASTER", "Item_Code")
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual') " & stateCond & "  "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtCapex_My_Click(sender As Object, e As EventArgs) Handles txtcapex._My_Click
        Dim qry As String = "select code as code,description as name from tspl_capex_master"
        txtcapex.arrValueMember = clsCommon.ShowMultipleSelectForm("capexPur", qry, "code", "Name", txtcapex.arrValueMember, txtcapex.arrDispalyMember)
        txtsubcapex.arrValueMember = Nothing
    End Sub

    Private Sub txtsubCapex_My_Click(sender As Object, e As EventArgs) Handles txtsubcapex._My_Click
        Dim qry As String = "select code as code,description as name from TSPL_CAPEX_BUDGET_MASTER "
        If txtcapex.arrValueMember IsNot Nothing AndAlso txtcapex.arrValueMember.Count > 0 Then
            qry += " where Capex_code IN (" + clsCommon.GetMulcallString(txtcapex.arrValueMember) + ")"
        End If
        txtsubcapex.arrValueMember = clsCommon.ShowMultipleSelectForm("subcapexPur", qry, "code", "Name", txtsubcapex.arrValueMember, txtsubcapex.arrDispalyMember)
    End Sub

    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Public Sub New()
        'Me.Visible = False
        ' This call is required by the designer.
        InitializeComponent()
        'Me.Visible = True
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
        End If
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtVenGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_desc as Name from TSPL_VENDOR_group"
        txtVenGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGroupMulSel", qry, "Code", "Name", txtVenGroup.arrValueMember, txtVenGroup.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtVenGroup, "Group_desc", "TSPL_VENDOR_group", "Ven_Group_Code")
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCapexRegister & "'"))
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If Not IsNothing(txtItem.arrValueMember) Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If Not IsNothing(txtVenGroup.arrValueMember) Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVenGroup.arrDispalyMember))
            End If
            If Not IsNothing(txtvendor.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtvendor.arrDispalyMember))
            End If
            If btnAll.CheckState = True Or btnPosted.CheckState = True Or btnUnposted.CheckState = True Then
                arrHeader.Add("Status : " + IIf(btnAll.IsChecked, "ALL", IIf(btnPosted.IsChecked, "Posted", "Unposted")))
            End If

            If exporter = EnumExportTo.Excel Then

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        Print(Exporter.Refresh, 1)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        Print(Exporter.Refresh, 2)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export", Me.Text)
            Exit Sub
        End If
        Export(Exporter.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export", Me.Text)
            Exit Sub
        End If
        Export(Exporter.PDF)
    End Sub

End Class



Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptVSPAssetIssue1
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Public sQuery As String = Nothing
    Public strMCC As ArrayList
    Public strItem As ArrayList
    Public strVSP As ArrayList
    Dim arrBack As New List(Of String)
    Dim FlagDrillDownNoData As Boolean = False
    '==============created by shivani Tyagi========against[BM00000008839]===================>
    Private Sub LOCATIONRIGTHS()
        'Try
        '    Dim obj As New clsMCCCodes()
        '    obj = clsMCCCodes.GetData()

        '    If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
        '        arrLoc = obj.arrLocCodes
        '    End If

        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptVSPAssetIssue1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    

    Private Sub RptVSPAssetIssue1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
    End Sub
    Sub Reset()
        rbtnSummary.CheckState = CheckState.Checked
        LOCATIONRIGTHS()
        cboType.SelectedIndex = -1
        FlagDrillDownNoData = False
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        lblVSP.Visible = True
        TxtMultiVendor.Visible = True
        chkAssetIssueWithPurchase.Checked = False
        'rbtnSummary.IsChecked = True
        gv.DataSource = Nothing
        TxtMultiVendor.arrValueMember = Nothing
        TxtMultiMCC.arrValueMember = Nothing
        TxtMultiItem.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
   
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        If chkAssetIssueWithPurchase.Checked = True Then
            PageSetupReport_ID = PageSetupReport_ID + "P"
        End If
        If chkCurrentStock.Checked = True Then
            PageSetupReport_ID = MyBase.Form_ID + "CS"
        End If
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptVSPAssetIssue1 & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                    Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(TxtMultiMCC.arrValueMember)
                    arrHeader.Add((" MCC : " + strLocationName + " "))

                End If
                If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                    Dim strvendor As String = clsCommon.GetMulcallStringWithComma(TxtMultiVendor.arrValueMember)
                    arrHeader.Add((" Vendor : " + strvendor + " "))

                End If
                If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                    Dim stritem As String = clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrValueMember)
                    arrHeader.Add((" Item : " + stritem + " "))

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
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("VSP ASSET ISSUE REPORT", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        Dim fromDate As String = txtFromDate.Value
        Dim Todate As String = txtToDate.Value
        '==================updated by preeti Gupta Against Ticket no[KDI/14/05/18-000312]==================
        Dim strqry As String = Nothing
        'Dim WithDateFilter As Boolean = False
        'If chkCurrentStock.Checked = True AndAlso rbtnDetail.IsChecked = True Then
        '    WithDateFilter = True
        'End If

        If chkAssetIssueWithPurchase.Checked = True Then

            Dim strWhrAdj As String = ""
            Dim strWhrSRN As String = ""
            Dim strWhrAsset As String = ""
            Dim strWhrInventory As String = ""
            Dim strWhrTransfer As String = ""
            Dim strWhrTransferOut As String = ""
            If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                strWhrAdj += " and TSPL_ADJUSTMENT_HEADER.Loc_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                strWhrSRN += " and TSPL_SRN_HEAD.Bill_To_Location in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                strWhrAsset += " and TSPL_VSPAsset_HEAD.From_Location  in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                strWhrInventory += " and TSPL_INVENTORY_MOVEMENT.Location_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                strWhrTransfer += " and TSPL_TRANSFER_ORDER_HEAD.To_Location in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                strWhrTransferOut += " and TSPL_TRANSFER_ORDER_HEAD.From_Location in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
            End If

            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                strWhrAdj += " and TSPL_ADJUSTMENT_Detail.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                strWhrSRN += " and TSPL_SRN_DETAIL.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                strWhrAsset += " and TSPL_VSPAsset_DETAIL.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                strWhrInventory += " and TSPL_INVENTORY_MOVEMENT.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                strWhrTransfer += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                strWhrTransferOut += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
            End If

            'strqry = " WITH TBL_ASSET_ISSUE AS ( Select ROW_NUMBER() OVER(ORDER BY MCC , DocType_SNO , convert (Date,Doc_Date,103) asc ) as SNO, ROW_NUMBER() OVER(ORDER BY MCC , DocType_SNO , convert (Date,Doc_Date,103) asc )+1 as SNO2  ,XXXFinal.DocType_SNO,XXXFinal.MCC ,XXXFinal.DocType ,XXXFinal.Doc_No, XXXFinal.Doc_Date,XXXFinal.Particular_From, XXXFinal.Particular_To, XXXFinal.ITEM_CODE ,case when DocType = 'Opening' then isnull (XXXFinal. Opening_Qty,0) else  isnull(InventroyMovement.OPBal,0) end as Opening_Qty, case when DocType = 'Opening' then isnull (Opening_Value,0) else isnull (InventroyMovement.OPBalCost,0) end as   Opening_Value,XXXFinal.Purchase_Qty , XXXFinal.TRF_Received , XXXFinal.Purchase_Received_Value , XXXFinal.ISSUE_TRF_Qty, XXXFinal.Lost_Qty,XXXFinal. Issue_Lost_Value,isnull (InventroyMovement.Balance_Qty,0) as Closing_Qty ,isnull (InventroyMovement.Balance_Cost,0)  as Closing_Value,  

            '                 SUM(isnull(InventroyMovement.Balance_Qty, 0)) OVER ( PARTITION BY  XXXFinal.MCC  ORDER BY XXXFinal.MCC ,DocType_SNO, XXXFinal.Doc_No, XXXFinal.ITEM_CODE) As RunningBalance_Qty 

            '                ,SUM (isnull (InventroyMovement.Balance_Cost,0)) OVER ( PARTITION BY  XXXFinal.MCC  ORDER BY XXXFinal.MCC ,DocType_SNO, XXXFinal.Doc_No, XXXFinal.ITEM_CODE) AS RunningClosing_Value

            '                , case when XXXFinal.DocType =  'LOSS AT CENTRE' then 0 else SUM (isnull (XXXFinal.ISSUE_TRF_Qty,0)) OVER ( PARTITION BY  XXXFinal.MCC  ORDER BY XXXFinal.MCC ,DocType_SNO, XXXFinal.Doc_No, XXXFinal.ITEM_CODE) end AS RunningBalance_Qty_Vendor 

            '                ,case when XXXFinal.DocType =  'LOSS AT CENTRE' then 0 else SUM ( case when isnull ( XXXFinal.ISSUE_TRF_Qty,0) > 0 then isnull (XXXFinal.Issue_Lost_Value,0) else 0 end ) OVER ( PARTITION BY  XXXFinal.MCC  ORDER BY XXXFinal.MCC ,DocType_SNO, XXXFinal.Doc_No, XXXFinal.ITEM_CODE) end AS RunningClosing_Value_Vendor, Live_Location

            '                from(

            '                  Select  1 As DocType_SNO,  Loc_code As MCC, 'Opening' as DocType, TSPL_ADJUSTMENT_HEADER.Adjustment_No   as Doc_No, convert (varchar, TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Doc_Date   ,Loc_code  as Particular_From,  '' as Particular_To, TSPL_ADJUSTMENT_Detail.Item_Code , Item_Quantity as Opening_Qty , Item_Cost as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, Loc_code as Live_Location from TSPL_ADJUSTMENT_Detail left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_Detail.Adjustment_No
            '                  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ADJUSTMENT_Detail.ITEM_CODE
            '                  where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_ADJUSTMENT_HEADER.Posted = 'Y' and TSPL_ADJUSTMENT_HEADER.Trans_Type = 'IN'
            '                  and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAdj + "

            '                Union All

            '                Select  2 As DocType_SNO, TSPL_SRN_HEAD.Bill_To_Location As MCC, 'SRN' as DocType, TSPL_SRN_DETAIL.SRN_No as Doc_No ,convert (varchar,TSPL_SRN_HEAD.SRN_Date,103) as Doc_Date , TSPL_SRN_HEAD.Bill_To_Location as Particular_From , '' as Particular_To, TSPL_SRN_DETAIL.ITEM_CODE,0 as Opening_Qty , 0 as Opening_Value ,TSPL_SRN_DETAIL.SRN_Qty as Purchase_Qty, 0 as TRF_Received, TSPL_SRN_DETAIL.Amount as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_SRN_HEAD.Bill_To_Location as Live_Location  
            '                From TSPL_SRN_DETAIL 
            '                Left Outer Join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO = TSPL_SRN_DETAIL.SRN_NO
            '                Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SRN_DETAIL.ITEM_CODE
            '                where TSPL_ITEM_MASTER.Item_Type = 'A'
            '                and convert(date,TSPL_SRN_HEAD.SRN_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=convert(date,'" + Todate + "',103) " + strWhrSRN + "

            '                Union All 

            '                Select  3 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC,'ASSET ISSUE' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,''  as Particular_From,  Issue_To as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , -1* Issued_Qty as ISSUE_TRF_Qty, 0 as Lost_Qty, -1 * Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location    From TSPL_VSPAsset_DETAIL
            '                Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Issue'
            '                and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAsset + "

            '                Union All 

            '                Select  4 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC, 'ASSET Return' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,TSPL_VSPAsset_HEAD.Issue_To as Particular_From,   '' as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , Issued_Qty_againstret as ISSUE_TRF_Qty, 0 as Lost_Qty, Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location    From TSPL_VSPAsset_DETAIL
            '                Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Return' and TSPL_VSPAsset_HEAD.IS_LOST = 0
            '                and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAsset + "

            '                Union All 

            '                Select  5 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC,'LOSS AT VSP' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,''  as Particular_From,  Issue_To as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , Issued_Qty_againstret * -1 as ISSUE_TRF_Qty, Issued_Qty_againstret as Lost_Qty, Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location    From TSPL_VSPAsset_DETAIL
            '                Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Return' and TSPL_VSPAsset_HEAD.IS_LOST = 1
            '                and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)   " + strWhrAsset + "

            '                Union All 

            '                Select  6 As DocType_SNO, TSPL_ADJUSTMENT_HEADER.Loc_Code  As MCC,'LOSS AT CENTRE' as DocType, TSPL_ADJUSTMENT_HEADER.Adjustment_No   as Doc_No, convert (varchar, TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Adjustment_Date ,Loc_code  as Particular_From,  '' as Particular_To, TSPL_ADJUSTMENT_Detail.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, Item_Quantity as Lost_Qty, Item_Cost as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, Loc_code as Live_Location from TSPL_ADJUSTMENT_Detail left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_Detail.Adjustment_No
            '                Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ADJUSTMENT_Detail.ITEM_CODE
            '                where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_ADJUSTMENT_HEADER.Posted = 'Y' and TSPL_ADJUSTMENT_HEADER.Trans_Type = 'Out'
            '                and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAdj + "
            '                )XXXFinal

            '                Left outer join 

            '                ( 
            '                  Select  Source_Doc_No,Item_Code,max(Location_Code) As Location_Code,SUM(STOCK_QTY * (Case When PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPBal],SUM(Cost  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPBalCost] , SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Balance_Qty],  SUM(cost * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Balance_Cost] from (
            '                  Select  Fat_Amt,SNF_Amt,0 As Fat_Rate,0 As SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 As IsFromMilk,0 As MilkFatPer,0 As MilkSNFPer,0 As MilkFATKG,0 As MilkSNFKG,Case When cust_code Is Not null And len(cust_code)>0 Then cust_code Else Case When Vendor_Code Is Not null And len(Vendor_Code)>0 Then Vendor_Code Else Other_Location_Code End End As SourceCode,Case When cust_code Is Not null And len(cust_code)>0 Then Cust_Name Else Case When Vendor_Code Is Not null And len(Vendor_Code)>0 Then Vendor_Name Else Other_Location_Desc End End As SourceName, Case When cust_code Is Not null And len(cust_code)>0 Then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor , (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then TSPL_INVENTORY_MOVEMENT.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then TSPL_INVENTORY_MOVEMENT.LIFO_Cost else TSPL_INVENTORY_MOVEMENT.Avg_Cost end end ) as Cost from TSPL_INVENTORY_MOVEMENT 
            '                  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.ITEM_CODE
            '                  Left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
            '                  where TSPL_ITEM_MASTER.Item_Type = 'A' and  convert(date,Punching_Date,103)>=convert(date,'" & fromDate & "',103) AND convert(date,Punching_Date,103)<=convert(date,'" & Todate & "',103)  " + strWhrInventory + "

            '                  ) Final Group By Source_Doc_No, Item_Code 
            '                  ) InventroyMovement on InventroyMovement.Source_Doc_No = XXXFinal.Doc_No And InventroyMovement.Item_Code = XXXFinal.Item_Code

            '          )


            '"
            Dim docDateColumn As String = ""
            If rbtnDetail.IsChecked = True Then
                docDateColumn = " , XXXFinal.Doc_No "
            End If

            'strqry = "  WITH TBL_ASSET_ISSUE AS ( Select  
            '            ROW_NUMBER() OVER(  ORDER BY MCC,XXXFinal.Item_Code, convert(date,Doc_Date,103) , DocType_SNO  " + docDateColumn + " asc ) as SNO,
            '            ROW_NUMBER() OVER( ORDER BY MCC,XXXFinal.Item_Code, convert(date,Doc_Date,103) ,DocType_SNO " + docDateColumn + "   asc )+1 as SNO2 ,
            '            XXXFinal.DocType_SNO,XXXFinal.MCC ,XXXFinal.DocType ,XXXFinal.Doc_No, XXXFinal.Doc_Date,XXXFinal.Particular_From, XXXFinal.Particular_To, XXXFinal.ITEM_CODE ,case when DocType = 'Opening' then isnull (XXXFinal. Opening_Qty,0) else  isnull(InventroyMovement.OPBal,0) end as Opening_Qty, 
            '            case when DocType = 'Opening' then isnull (Opening_Value,0) else isnull (InventroyMovement.OPBalCost,0) end as   Opening_Value,XXXFinal.Purchase_Qty , XXXFinal.TRF_Received , XXXFinal.Purchase_Received_Value , XXXFinal.ISSUE_TRF_Qty, XXXFinal.Lost_Qty,XXXFinal. Issue_Lost_Value,isnull (InventroyMovement.Balance_Qty,0) as Closing_Qty ,isnull (InventroyMovement.Balance_Cost,0)  as Closing_Value,

            '            SUM(isnull(InventroyMovement.Balance_Qty, 0)) OVER ( PARTITION BY  XXXFinal.MCC, XXXFinal.Item_Code " + docDateColumn + "   ORDER BY XXXFinal.MCC, XXXFinal.Item_Code , convert(date,Doc_Date,103) ,DocType_SNO) As RunningBalance_Qty 

            '            ,SUM (isnull (InventroyMovement.Balance_Cost,0)) OVER ( PARTITION BY  XXXFinal.MCC, XXXFinal.Item_Code " + docDateColumn + " ORDER BY XXXFinal.MCC, XXXFinal.Item_Code , convert(date,Doc_Date,103) ,DocType_SNO) AS RunningClosing_Value

            '            , case when XXXFinal.DocType =  'LOSS AT CENTRE' then 0   else SUM ( case when isnull ( XXXFinal.Lost_Qty,0) > 0 then isnull ( XXXFinal.Lost_Qty,0) else  isnull (XXXFinal.ISSUE_TRF_Qty,0)  end ) OVER ( PARTITION BY  XXXFinal.MCC,XXXFinal.Item_Code,XXXFinal.Live_Location   ORDER BY XXXFinal.MCC ,DocType_SNO, XXXFinal.Doc_No, XXXFinal.ITEM_CODE) end AS RunningBalance_Qty_Vendor 

            '            ,case when XXXFinal.DocType =  'LOSS AT CENTRE' then 0 else SUM ( isnull (XXXFinal.Issue_Lost_Value,0) ) OVER ( PARTITION BY  XXXFinal.MCC,XXXFinal.Item_Code,XXXFinal.Live_Location   ORDER BY XXXFinal.MCC ,DocType_SNO, XXXFinal.Doc_No, XXXFinal.ITEM_CODE) end AS RunningClosing_Value_Vendor, 							
            '            Live_Location

            '                from(

            '                  Select  0 As DocType_SNO,  Loc_code As MCC, 'Opening' as DocType, TSPL_ADJUSTMENT_HEADER.Adjustment_No   as Doc_No, convert (varchar, TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Doc_Date   ,Loc_code  as Particular_From,  '' as Particular_To, TSPL_ADJUSTMENT_Detail.Item_Code , Item_Quantity as Opening_Qty , Item_Cost as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, Loc_code as Live_Location from TSPL_ADJUSTMENT_Detail left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_Detail.Adjustment_No
            '                  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ADJUSTMENT_Detail.ITEM_CODE
            '                  where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_ADJUSTMENT_HEADER.Posted = 'Y' and TSPL_ADJUSTMENT_HEADER.Trans_Type = 'IN'
            '                  and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAdj + "

            '                Union All
            '                   Select  1 As DocType_SNO,  TSPL_TRANSFER_ORDER_HEAD.To_Location As MCC, 'Transfer' as DocType, TSPL_TRANSFER_ORDER_HEAD.Document_No   as Doc_No, convert (varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as Doc_Date   ,TSPL_TRANSFER_ORDER_HEAD.To_Location  as Particular_From,  '' as Particular_To, TSPL_TRANSFER_ORDER_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, TSPL_TRANSFER_ORDER_DETAIL.In_Qty as TRF_Received, TSPL_TRANSFER_ORDER_DETAIL.Amount as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_TRANSFER_ORDER_HEAD.To_Location as Live_Location from TSPL_TRANSFER_ORDER_DETAIL left outer join  TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No = TSPL_TRANSFER_ORDER_DETAIL.Document_No
            '                  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_TRANSFER_ORDER_DETAIL.ITEM_CODE
            '                  where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_TRANSFER_ORDER_HEAD.Status = 1 and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type = 'I'
            '                  and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" + Todate + "',103)   " + strWhrTransfer + " 
            '                Union All
            '                   Select  1 As DocType_SNO,  TSPL_TRANSFER_ORDER_HEAD.From_Location As MCC, 'Transfer' as DocType, TSPL_TRANSFER_ORDER_HEAD.Document_No   as Doc_No, convert (varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as Doc_Date   ,TSPL_TRANSFER_ORDER_HEAD.From_Location  as Particular_From,  '' as Particular_To, TSPL_TRANSFER_ORDER_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, -1 * TSPL_TRANSFER_ORDER_DETAIL.In_Qty as TRF_Received, -1 * TSPL_TRANSFER_ORDER_DETAIL.Amount as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_TRANSFER_ORDER_HEAD.From_Location as Live_Location from TSPL_TRANSFER_ORDER_DETAIL left outer join  TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No = TSPL_TRANSFER_ORDER_DETAIL.Document_No
            '                  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_TRANSFER_ORDER_DETAIL.ITEM_CODE
            '                  where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_TRANSFER_ORDER_HEAD.Status = 1 and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type = 'O'
            '                  and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" + Todate + "',103)   " + strWhrTransferOut + " 

            '                Union All

            '                Select  2 As DocType_SNO, TSPL_SRN_HEAD.Bill_To_Location As MCC, 'SRN' as DocType, TSPL_SRN_DETAIL.SRN_No as Doc_No ,convert (varchar,TSPL_SRN_HEAD.SRN_Date,103) as Doc_Date , TSPL_SRN_HEAD.Bill_To_Location as Particular_From , '' as Particular_To, TSPL_SRN_DETAIL.ITEM_CODE,0 as Opening_Qty , 0 as Opening_Value ,TSPL_SRN_DETAIL.SRN_Qty as Purchase_Qty, 0 as TRF_Received, TSPL_SRN_DETAIL.Amount as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_SRN_HEAD.Bill_To_Location as Live_Location  
            '                From TSPL_SRN_DETAIL 
            '                Left Outer Join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO = TSPL_SRN_DETAIL.SRN_NO
            '                Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SRN_DETAIL.ITEM_CODE
            '                where TSPL_ITEM_MASTER.Item_Type = 'A'
            '                and convert(date,TSPL_SRN_HEAD.SRN_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=convert(date,'" + Todate + "',103) " + strWhrSRN + "

            '                Union All 

            '                Select  3 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC,'ASSET ISSUE' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,''  as Particular_From,  Issue_To as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , -1* Issued_Qty as ISSUE_TRF_Qty, 0 as Lost_Qty, -1 * Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location    From TSPL_VSPAsset_DETAIL
            '                Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Issue'
            '                and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAsset + "

            '                Union All 

            '                Select  4 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC, 'ASSET Return' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,TSPL_VSPAsset_HEAD.Issue_To as Particular_From,   '' as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , Issued_Qty_againstret as ISSUE_TRF_Qty, 0 as Lost_Qty, Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location    From TSPL_VSPAsset_DETAIL
            '                Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Return' and TSPL_VSPAsset_HEAD.IS_LOST = 0
            '                and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAsset + "

            '                Union All 

            '                Select  5 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC,'LOSS AT VSP' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,''  as Particular_From,  Issue_To as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , Issued_Qty_againstret * -1 as ISSUE_TRF_Qty, Issued_Qty_againstret as Lost_Qty, Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location    From TSPL_VSPAsset_DETAIL
            '                Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Return' and TSPL_VSPAsset_HEAD.IS_LOST = 1
            '                and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)   " + strWhrAsset + "

            '                Union All 

            '                Select  6 As DocType_SNO, TSPL_ADJUSTMENT_HEADER.Loc_Code  As MCC,'LOSS AT CENTRE' as DocType, TSPL_ADJUSTMENT_HEADER.Adjustment_No   as Doc_No, convert (varchar, TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Adjustment_Date ,Loc_code  as Particular_From,  '' as Particular_To, TSPL_ADJUSTMENT_Detail.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, Item_Quantity as Lost_Qty, Item_Cost as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, Loc_code as Live_Location from TSPL_ADJUSTMENT_Detail left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_Detail.Adjustment_No
            '                Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ADJUSTMENT_Detail.ITEM_CODE
            '                where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_ADJUSTMENT_HEADER.Posted = 'Y' and TSPL_ADJUSTMENT_HEADER.Trans_Type = 'Out'
            '                and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAdj + "
            '                )XXXFinal

            '                Left outer join 

            '                ( 
            '                  Select  Source_Doc_No,Item_Code,max(Location_Code) As Location_Code,SUM(STOCK_QTY * (Case When PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPBal],SUM(Cost  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPBalCost] , SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Balance_Qty],  SUM(Net_cost * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Balance_Cost] from (
            '                  Select  Fat_Amt,SNF_Amt,0 As Fat_Rate,0 As SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 As IsFromMilk,0 As MilkFatPer,0 As MilkSNFPer,0 As MilkFATKG,0 As MilkSNFKG,Case When cust_code Is Not null And len(cust_code)>0 Then cust_code Else Case When Vendor_Code Is Not null And len(Vendor_Code)>0 Then Vendor_Code Else Other_Location_Code End End As SourceCode,Case When cust_code Is Not null And len(cust_code)>0 Then Cust_Name Else Case When Vendor_Code Is Not null And len(Vendor_Code)>0 Then Vendor_Name Else Other_Location_Desc End End As SourceName, Case When cust_code Is Not null And len(cust_code)>0 Then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor , (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then TSPL_INVENTORY_MOVEMENT.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then TSPL_INVENTORY_MOVEMENT.LIFO_Cost else TSPL_INVENTORY_MOVEMENT.Avg_Cost end end ) as Cost, Net_Cost from TSPL_INVENTORY_MOVEMENT 
            '                  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.ITEM_CODE
            '                  Left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
            '                  where TSPL_ITEM_MASTER.Item_Type = 'A' and  convert(date,Punching_Date,103)>=convert(date,'" & fromDate & "',103) AND convert(date,Punching_Date,103)<=convert(date,'" & Todate & "',103)  " + strWhrInventory + "

            '                  ) Final Group By Source_Doc_No, Item_Code 
            '                  ) InventroyMovement on InventroyMovement.Source_Doc_No = XXXFinal.Doc_No And InventroyMovement.Item_Code = XXXFinal.Item_Code

            '          )


            '"

            strqry = "  WITH TBL_ASSET_ISSUE AS ( 
                        select PPPPFinal.SNO,PPPPFinal.DocType_SNO,DocType,MCC,Doc_No,Doc_Date,Particular_From,Particular_To,Item_Code, FinalClosingQty - Closing_Qty as FinalOpening_Qty , FinalClosingValue-Closing_Value as FinalOpening_Value,

                        Opening_Qty,Opening_Value,Purchase_Qty,TRF_Received,Purchase_Received_Value,ISSUE_TRF_Qty ,Lost_Qty ,Issue_Lost_Value,Closing_Qty,Closing_Value,FinalClosingQty,FinalClosingValue,case when len (Particular_To) > 0 then (FinalClosingQty - Closing_Qty ) - FinalClosingQty  else 0 end as FinalClosingQtyVendor,case when len (Particular_To) > 0 then (FinalClosingValue - Closing_Value ) - FinalClosingValue  else 0 end as FinalClosingValueVendor ,Live_Location, LostValue from (
                        select XXXFinal.SNO , XXXFinal.DocType_SNO,DocType ,XXXFinal.MCC ,XXXFinal.Doc_No , XXXFinal.Doc_Date , XXXFinal.Particular_From , XXXFinal.Particular_To , XXXFinal.Item_Code , Opening_Qty,Opening_Value,Purchase_Qty,TRF_Received,Purchase_Received_Value,ISSUE_TRF_Qty ,Lost_Qty ,Issue_Lost_Value,Closing_Qty,Closing_Value,
                        SUM (Closing_Qty) OVER (PARTITION BY MCC,Item_code ORDER BY SNO) as FinalClosingQty,SUM (Closing_Value) OVER (PARTITION BY MCC,Item_code ORDER BY SNO) as FinalClosingValue, Live_Location , LostValue


                       from (
                       select ROW_NUMBER() OVER(  ORDER BY MCC,final.Item_Code, convert(date,Doc_Date,103) , DocType_SNO   , final.Doc_No  asc ) as SNO,ROW_NUMBER() OVER(  ORDER BY MCC,final.Item_Code, convert(date,Doc_Date,103) , DocType_SNO   , final.Doc_No  asc ) as SNO2,MCC,final.DocType_SNO,DocType,Doc_No,Doc_Date,Particular_From,Particular_To,Item_Code,Opening_Qty,Opening_Value,Purchase_Qty,TRF_Received,Purchase_Received_Value,ISSUE_TRF_Qty ,Lost_Qty ,Issue_Lost_Value, Opening_Qty + Purchase_Qty + TRF_Received +case when Lost_Qty >0 then 0 else ISSUE_TRF_Qty end  Closing_Qty, Opening_Value + Purchase_Received_Value +  case when Lost_Qty >0 then 0 else Issue_Lost_Value end as Closing_Value, Live_Location, LostValue from 

                       (                         

                              Select  0 As DocType_SNO,  Loc_code As MCC, 'Opening' as DocType, TSPL_ADJUSTMENT_HEADER.Adjustment_No   as Doc_No, convert (varchar, TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Doc_Date   ,Loc_code  as Particular_From,  '' as Particular_To, TSPL_ADJUSTMENT_Detail.Item_Code , Item_Quantity as Opening_Qty , Item_Cost as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, Loc_code as Live_Location , 0 as LostValue from TSPL_ADJUSTMENT_Detail left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_Detail.Adjustment_No
                              Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ADJUSTMENT_Detail.ITEM_CODE
                              where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_ADJUSTMENT_HEADER.Posted = 'Y' and TSPL_ADJUSTMENT_HEADER.Trans_Type = 'IN'
                              and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAdj + " 

                            Union All
                               Select  1 As DocType_SNO,  TSPL_TRANSFER_ORDER_HEAD.To_Location As MCC, 'Transfer' as DocType, TSPL_TRANSFER_ORDER_HEAD.Document_No   as Doc_No, convert (varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as Doc_Date   ,TSPL_TRANSFER_ORDER_HEAD.To_Location  as Particular_From,  '' as Particular_To, TSPL_TRANSFER_ORDER_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, TSPL_TRANSFER_ORDER_DETAIL.In_Qty as TRF_Received, TSPL_TRANSFER_ORDER_DETAIL.Amount as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_TRANSFER_ORDER_HEAD.To_Location as Live_Location , 0 as LostValue from TSPL_TRANSFER_ORDER_DETAIL left outer join  TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No = TSPL_TRANSFER_ORDER_DETAIL.Document_No
                              Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_TRANSFER_ORDER_DETAIL.ITEM_CODE
                              where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_TRANSFER_ORDER_HEAD.Status = 1 and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type = 'I'
                              and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" + Todate + "',103)   " + strWhrTransfer + " 
                            Union All
                               Select  1 As DocType_SNO,  TSPL_TRANSFER_ORDER_HEAD.From_Location As MCC, 'Transfer' as DocType, TSPL_TRANSFER_ORDER_HEAD.Document_No   as Doc_No, convert (varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as Doc_Date   ,TSPL_TRANSFER_ORDER_HEAD.From_Location  as Particular_From,  '' as Particular_To, TSPL_TRANSFER_ORDER_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, -1 * TSPL_TRANSFER_ORDER_DETAIL.In_Qty as TRF_Received, -1 * TSPL_TRANSFER_ORDER_DETAIL.Amount as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_TRANSFER_ORDER_HEAD.From_Location as Live_Location , 0 as LostValue from TSPL_TRANSFER_ORDER_DETAIL left outer join  TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No = TSPL_TRANSFER_ORDER_DETAIL.Document_No
                              Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_TRANSFER_ORDER_DETAIL.ITEM_CODE
                              where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_TRANSFER_ORDER_HEAD.Status = 1 and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type = 'O'
                              and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" + Todate + "',103)   " + strWhrTransferOut + " 
                            
                            Union All

                            Select  2 As DocType_SNO, TSPL_SRN_HEAD.Bill_To_Location As MCC, 'SRN' as DocType, TSPL_SRN_DETAIL.SRN_No as Doc_No ,convert (varchar,TSPL_SRN_HEAD.SRN_Date,103) as Doc_Date , TSPL_SRN_HEAD.Bill_To_Location as Particular_From , '' as Particular_To, TSPL_SRN_DETAIL.ITEM_CODE,0 as Opening_Qty , 0 as Opening_Value ,TSPL_SRN_DETAIL.SRN_Qty as Purchase_Qty, 0 as TRF_Received, TSPL_SRN_DETAIL.Amount as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, 0 as Lost_Qty, 0 as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_SRN_HEAD.Bill_To_Location as Live_Location  , 0 as LostValue
                            From TSPL_SRN_DETAIL 
                            Left Outer Join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO = TSPL_SRN_DETAIL.SRN_NO
                            Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SRN_DETAIL.ITEM_CODE
                            where TSPL_ITEM_MASTER.Item_Type = 'A'
                            and convert(date,TSPL_SRN_HEAD.SRN_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_SRN_HEAD.SRN_Date,103)<=convert(date,'" + Todate + "',103) " + strWhrSRN + "

                            Union All 

                            Select  3 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC,'ASSET ISSUE' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,''  as Particular_From,  Issue_To as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , -1* Issued_Qty as ISSUE_TRF_Qty, 0 as Lost_Qty, -1 * Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location , 0 as LostValue   From TSPL_VSPAsset_DETAIL
                            Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Issue'
                             and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAsset + "

                            Union All 

                            Select  4 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC, 'ASSET Return' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,TSPL_VSPAsset_HEAD.Issue_To as Particular_From,   '' as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , Issued_Qty_againstret as ISSUE_TRF_Qty, 0 as Lost_Qty, Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location , 0 as LostValue   From TSPL_VSPAsset_DETAIL
                            Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Return' and TSPL_VSPAsset_HEAD.IS_LOST = 0
                             and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAsset + "

                            Union All 

                            Select  5 As DocType_SNO, TSPL_VSPAsset_HEAD.From_Location  As MCC,'LOSS AT VSP' as DocType,TSPL_VSPAsset_HEAD.Doc_No, Convert (varchar, TSPL_VSPAsset_HEAD.Doc_Date,103) as Doc_Date,''  as Particular_From,  Issue_To as Particular_To, TSPL_VSPAsset_DETAIL.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , Issued_Qty_againstret * -1 as ISSUE_TRF_Qty, Issued_Qty_againstret as Lost_Qty, Amount as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, TSPL_VSPAsset_HEAD.Issue_To as Live_Location,Amount as LostValue    From TSPL_VSPAsset_DETAIL
                            Left Join TSPL_VSPAsset_HEAD on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No where TSPL_VSPAsset_HEAD.Status = 1 And Doc_Type = 'Return' and TSPL_VSPAsset_HEAD.IS_LOST = 1
                            and convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103)<=convert(date,'" + Todate + "',103)   " + strWhrAsset + "

                            Union All 

                            Select  6 As DocType_SNO, TSPL_ADJUSTMENT_HEADER.Loc_Code  As MCC,'LOSS AT CENTRE' as DocType, TSPL_ADJUSTMENT_HEADER.Adjustment_No   as Doc_No, convert (varchar, TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Adjustment_Date ,Loc_code  as Particular_From,  '' as Particular_To, TSPL_ADJUSTMENT_Detail.Item_Code , 0 as Opening_Qty , 0 as Opening_Value ,0 as Purchase_Qty, 0 as TRF_Received, 0 as Purchase_Received_Value , 0 as ISSUE_TRF_Qty, Item_Quantity as Lost_Qty, Item_Cost as  Issue_Lost_Value , 0 as  Closing_Qty, 0 as Closing_Value, Loc_code as Live_Location, 0 as LostValue from TSPL_ADJUSTMENT_Detail left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_Detail.Adjustment_No
                            Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_ADJUSTMENT_Detail.ITEM_CODE
                            where TSPL_ITEM_MASTER.Item_Type = 'A' and TSPL_ADJUSTMENT_HEADER.Posted = 'Y' and TSPL_ADJUSTMENT_HEADER.Trans_Type = 'Out'
                            and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + fromDate + "',103) AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + Todate + "',103)  " + strWhrAdj + "
                            ) final

                    ) XXXFinal
                    ) PPPPFinal		

                    )            

            "

            If rbtnSummary.IsChecked = True Then
                'strqry += " select XXXXFINAL.MCC, max(TBL_MCC.Name) as MCC_Name XXXXFINAL.Doc_Date ,sum (Opening_Qty)  as Opening_Qty,sum (Opening_Value)  as Opening_Value , sum (Purchase_Qty) as  Purchase_Qty, sum (TRF_Received) as  TRF_Received,
                '            sum (Purchase_Received_Value) as  Purchase_Received_Value, sum (ISSUE_TRF_Qty) as  ISSUE_TRF_Qty,sum (Lost_Qty) as  Lost_Qty,sum (Issue_Lost_Value) as  Issue_Lost_Value,sum (Closing_Qty) as  Closing_Qty,sum (Closing_Value) as  Closing_Value,sum (RunningBalance_Qty) as  RunningBalance_Qty,sum (RunningClosing_Value) as  RunningClosing_Value,max (Live_Location) as  Live_Location from (
                '            select W1.MCC , W1 .Doc_Date , case when P1.SNO_MCC_BY_Min =  W1.SNO_MCC_BY then  Opening_Qty  else Opening_Qty * 0  end Opening_Qty, case when P1.SNO_MCC_BY_Min =  W1.SNO_MCC_BY then  Opening_Value  else Opening_Value * 0  end Opening_Value , W1.Purchase_Qty,   W1.TRF_Received,
                '              W1.Purchase_Received_Value,   W1.ISSUE_TRF_Qty,  W1.Lost_Qty, W1.Issue_Lost_Value,   W1.Closing_Qty  as Closing_Qty ,   W1.Closing_Value as Closing_Value , case when P1.SNO_MCC_BY_MAX =  W1.SNO_MCC_BY then W1.RunningBalance_Qty else 0 end RunningBalance_Qty,   case when P1.SNO_MCC_BY_MAX =  W1.SNO_MCC_BY then W1.RunningClosing_Value else 0 end RunningClosing_Value,   W1.Live_Location from (
                '            select  ROW_NUMBER() OVER( PARTITION BY  T1.MCC,T1.Doc_Date  ORDER BY T1.MCC ,  convert (Date,T1.Doc_Date,103) asc ) as SNO_MCC_BY, T1.SNO,  T1.MCC , T1.Doc_Date ,  case when T1.DocType = 'Opening' then T1.Opening_Qty else isnull (T2.RunningBalance_Qty,0) end as Opening_Qty, case when T1.DocType = 'Opening' then T1.Opening_Value  else isnull (T2.RunningClosing_Value,0) end as Opening_Value , convert(decimal (18,2), T1.Purchase_Qty) as Purchase_Qty, T1.TRF_Received, T1.Purchase_Received_Value, T1.ISSUE_TRF_Qty, T1.Lost_Qty , T1.Issue_Lost_Value, T1.Closing_Qty, T1.Closing_Value , T1.RunningBalance_Qty,T1.RunningClosing_Value,  T1.RunningBalance_Qty_Vendor , T1.RunningClosing_Value_Vendor, T1.Live_Location  FROM TBL_ASSET_ISSUE T1 
                '            left outer join TBL_ASSET_ISSUE T2 on T1.SNO = T2.SNO2 and T1.MCC = T2.MCC 
                '            ) W1
                '            left outer join (
                '            select MCC,Doc_Date ,  Min (SNO_MCC_BY ) as SNO_MCC_BY_Min , MAX (SNO_MCC_BY) as SNO_MCC_BY_MAX , sum (Purchase_Qty) as  Purchase_Qty, sum (TRF_Received) as  TRF_Received,
                '            sum (Purchase_Received_Value) as  Purchase_Received_Value, sum (ISSUE_TRF_Qty) as  ISSUE_TRF_Qty,sum (Lost_Qty) as  Lost_Qty,sum (Issue_Lost_Value) as  Issue_Lost_Value,sum (Closing_Qty) as  Closing_Qty,sum (Closing_Value) as  Closing_Value,sum (RunningBalance_Qty) as  RunningBalance_Qty,sum (RunningClosing_Value) as  RunningClosing_Value,sum (RunningBalance_Qty_Vendor) as  RunningBalance_Qty_Vendor,sum (RunningClosing_Value_Vendor) as  RunningClosing_Value_Vendor,max (Live_Location) as  Live_Location
                '             from (
                '            select  ROW_NUMBER() OVER( PARTITION BY  T1.MCC,T1.Doc_Date  ORDER BY T1.MCC ,  convert (Date,T1.Doc_Date,103) asc ) as SNO_MCC_BY, T1.SNO,  T1.MCC , T1.Doc_Date ,  case when T1.DocType = 'Opening' then T1.Opening_Qty else isnull (T2.RunningBalance_Qty,0) end as Opening_Qty, case when T1.DocType = 'Opening' then T1.Opening_Value  else isnull (T2.RunningClosing_Value,0) end as Opening_Value , T1.Purchase_Qty, T1.TRF_Received, T1.Purchase_Received_Value, T1.ISSUE_TRF_Qty, T1.Lost_Qty , T1.Issue_Lost_Value, T1.Closing_Qty, T1.Closing_Value , T1.RunningBalance_Qty,T1.RunningClosing_Value,  T1.RunningBalance_Qty_Vendor , T1.RunningClosing_Value_Vendor, T1.Live_Location  FROM TBL_ASSET_ISSUE T1 
                '            left outer join TBL_ASSET_ISSUE T2 on T1.SNO = T2.SNO2 and T1.MCC = T2.MCC 


                '            ) Final Group by MCC,Doc_Date
                '            ) P1 on P1.MCC = W1.MCC and P1.Doc_Date = W1.Doc_Date
                '            ) XXXXFINAL  left outer join (Select Location_code as Code , Location_Desc as Name from TSPL_LOCATION_MASTER )TBL_MCC on TBL_MCC.Code = XXXXFINAL.MCC group by XXXXFINAL.MCC, XXXXFINAL.Doc_Date order by XXXXFINAL.MCC, convert (date, XXXXFINAL.Doc_Date,103)  "

                '         strqry += " select ZZZ.Doc_Date , ZZZ.Doc_Date , isnull (ZZZ.DateWiseClosingQty,0) as DateWiseClosingQty, Final.MCC,Final. MCC_Name,Final.ITEM_CODE ,Final.item_desc ,Final.Doc_Date ,Final.Opening_Qty + isnull (ZZZ.DateWiseClosingQty,0) as Opening_Qty  ,Final.Opening_Value +  isnull (ZZZ.DateWiseClosingValue,0) as Opening_Value , Final.Purchase_Qty, Final.  TRF_Received,
                '                     Final.Purchase_Received_Value, Final.ISSUE_TRF_Qty,Final.Lost_Qty,Final.Issue_Lost_Value,Final.Closing_Qty,Final.Closing_Value,Final.  RunningBalance_Qty +  isnull (ZZZ.DateWiseClosingQty,0) as RunningBalance_Qty,Final.RunningClosing_Value + isnull (ZZZ.DateWiseClosingValue,0) as RunningClosing_Value  from (
                '                     select XXXXFINAL.MCC,max(TBL_MCC.Name) as MCC_Name,XXXXFINAL.ITEM_CODE ,max(TSPL_ITEM_MASTER.item_desc) as item_desc ,XXXXFINAL.Doc_Date ,sum (Opening_Qty)  as Opening_Qty,sum (Opening_Value)  as Opening_Value , sum (Purchase_Qty) as  Purchase_Qty, sum (TRF_Received) as  TRF_Received,
                '                     sum (Purchase_Received_Value) as  Purchase_Received_Value, sum (  ISSUE_TRF_Qty) as  ISSUE_TRF_Qty,sum (Lost_Qty) as  Lost_Qty,sum (Issue_Lost_Value) as  Issue_Lost_Value,sum (Closing_Qty) as  Closing_Qty,sum (Closing_Value) as  Closing_Value,sum (Closing_Qty) as  RunningBalance_Qty,sum (Closing_Value) as  RunningClosing_Value,max (Live_Location) as  Live_Location from (
                '                     select W1.MCC ,W1.ITEM_CODE, W1 .Doc_Date , case when P1.SNO_MCC_BY_Min =  W1.SNO_MCC_BY then  Opening_Qty  else Opening_Qty * 0  end Opening_Qty, case when P1.SNO_MCC_BY_Min =  W1.SNO_MCC_BY then  Opening_Value  else Opening_Value * 0  end Opening_Value , W1.Purchase_Qty,   W1.TRF_Received,
                '                       W1.Purchase_Received_Value,   W1.ISSUE_TRF_Qty,  W1.Lost_Qty, W1.Issue_Lost_Value,   W1.Closing_Qty  as Closing_Qty ,   W1.Closing_Value as Closing_Value , case when P1.SNO_MCC_BY_MAX =  W1.SNO_MCC_BY then W1.RunningBalance_Qty else 0 end RunningBalance_Qty,   case when P1.SNO_MCC_BY_MAX =  W1.SNO_MCC_BY then W1.RunningClosing_Value else 0 end RunningClosing_Value,   W1.Live_Location from (
                '                     select  ROW_NUMBER() OVER( PARTITION BY  T1.MCC,T1.Doc_Date  ORDER BY T1.MCC ,T1.ITEM_CODE,  convert (Date,T1.Doc_Date,103) asc ) as SNO_MCC_BY, T1.SNO,  T1.MCC ,T1.ITEM_CODE, T1.Doc_Date ,  case when T1.DocType = 'Opening' then T1.Opening_Qty else isnull (T2.RunningBalance_Qty,0) end as Opening_Qty, case when T1.DocType = 'Opening' then T1.Opening_Value  else isnull (T2.RunningClosing_Value,0) end as Opening_Value , convert(decimal (18,2), T1.Purchase_Qty) as Purchase_Qty, T1.TRF_Received, T1.Purchase_Received_Value, T1.ISSUE_TRF_Qty +T1.lost_Qty as ISSUE_TRF_Qty 							
                ', T1.Lost_Qty , T1.Issue_Lost_Value, T1.Closing_Qty, T1.Closing_Value , T1.RunningBalance_Qty,T1.RunningClosing_Value,  T1.RunningBalance_Qty_Vendor , T1.RunningClosing_Value_Vendor, T1.Live_Location  FROM TBL_ASSET_ISSUE T1 
                '                     left outer join TBL_ASSET_ISSUE T2 on T1.SNO = T2.SNO2 and T1.MCC = T2.MCC 
                '                     ) W1
                '                     left outer join (
                '                     select MCC,Doc_Date ,ITEM_CODE ,  Min (SNO_MCC_BY ) as SNO_MCC_BY_Min , MAX (SNO_MCC_BY) as SNO_MCC_BY_MAX , sum (Purchase_Qty) as  Purchase_Qty, sum (TRF_Received) as  TRF_Received,
                '                     sum (Purchase_Received_Value) as  Purchase_Received_Value, sum (ISSUE_TRF_Qty) as  ISSUE_TRF_Qty,sum (Lost_Qty) as  Lost_Qty,sum (Issue_Lost_Value) as  Issue_Lost_Value,sum (Closing_Qty) as  Closing_Qty,sum (Closing_Value) as  Closing_Value,sum (RunningBalance_Qty) as  RunningBalance_Qty,sum (RunningClosing_Value) as  RunningClosing_Value,sum (RunningBalance_Qty_Vendor) as  RunningBalance_Qty_Vendor,sum (RunningClosing_Value_Vendor) as  RunningClosing_Value_Vendor,max (Live_Location) as  Live_Location
                '                     from (
                '                     select  ROW_NUMBER() OVER( PARTITION BY  T1.MCC,T1.Doc_Date  ORDER BY T1.MCC  ,T1.ITEM_CODE ,convert (Date,T1.Doc_Date,103) asc ) as SNO_MCC_BY, T1.SNO,  T1.MCC ,T1.ITEM_CODE , T1.Doc_Date ,  case when T1.DocType = 'Opening' then T1.Opening_Qty else isnull (T2.RunningBalance_Qty,0) end as Opening_Qty, case when T1.DocType = 'Opening' then T1.Opening_Value  else isnull (T2.RunningClosing_Value,0) end as Opening_Value , T1.Purchase_Qty, T1.TRF_Received, T1.Purchase_Received_Value, 

                'T1.ISSUE_TRF_Qty + T1.lost_Qty as ISSUE_TRF_Qty 					
                ', T1.Lost_Qty , T1.Issue_Lost_Value, T1.Closing_Qty, T1.Closing_Value , T1.RunningBalance_Qty,T1.RunningClosing_Value,  T1.RunningBalance_Qty_Vendor , T1.RunningClosing_Value_Vendor, T1.Live_Location  FROM TBL_ASSET_ISSUE T1 
                '                     left outer join TBL_ASSET_ISSUE T2 on T1.SNO = T2.SNO2 and T1.MCC = T2.MCC 


                '                     ) Final Group by MCC,ITEM_CODE, Doc_Date
                '                     ) P1 on P1.MCC = W1.MCC and P1.Doc_Date = W1.Doc_Date 	and P1.Item_Code = W1.Item_Code


                '                     ) XXXXFINAL  left outer join (Select Location_code as Code , Location_Desc as Name from TSPL_LOCATION_MASTER )TBL_MCC on TBL_MCC.Code = XXXXFINAL.MCC  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=XXXXFINAL.item_code group by XXXXFINAL.MCC, XXXXFINAL.ITEM_CODE ,  XXXXFINAL.Doc_Date 

                '                    ) Final 

                'left outer join (select distinct  MCC, Item_Code, Doc_Date, sum (Closing_Qty) as DateWiseClosingQty, sum (Closing_Value) as DateWiseClosingValue from TBL_ASSET_ISSUE group by MCC, Item_Code, Doc_Date  ) ZZZ on ZZZ.MCC = Final.MCC and ZZZ.Item_Code = Final.Item_Code 
                'and Final.Doc_Date  > ZZZ.Doc_Date 							
                'order by Final.MCC,ITEM_CODE, convert (date, Final.Doc_Date,103)

                '                   "

                strqry += "     select MCC , max(MCC_NAME) as MCC_NAME , Doc_Date , Item_Code ,max(Item_Desc) as Item_Desc ,sum ( case when SNO23 = 1 then Opening_Qty else 0 end) Opening_Qty , sum ( case when SNO23 = 1 then Opening_Value else 0 end) Opening_Value , sum(Purchase_Qty) as Purchase_Qty , sum (TRF_Received ) as TRF_Received, sum (Purchase_Received_Value) as Purchase_Received_Value  , sum(ISSUE_TRF_Qty) as ISSUE_TRF_Qty , sum (Lost_Qty) as Lost_Qty ,sum ( case when SNO23 = CountRecod then RunningBalance_Qty else 0 end) RunningBalance_Qty , sum ( case when SNO23 = CountRecod then RunningClosing_Value else 0 end) RunningClosing_Value   from (
                                select 
                                ROW_NUMBER() OVER( Partition by  TTTT.MCC,TTTT.Item_Code, Item_Desc,convert(date,TTTT.Doc_Date,103) ORDER BY TTTT.MCC,TTTT.Item_Code, convert(date,TTTT.Doc_Date,103) asc ) as SNO23, count (SNO) OVER( Partition by  TTTT.MCC,TTTT.Item_Code, convert(date,TTTT.Doc_Date,103) ORDER BY TTTT.MCC,TTTT.Item_Code, convert(date,TTTT.Doc_Date,103) asc ) as CountRecod, SNO  ,TTTT.MCC , TTTT.MCC_NAME ,TTTT. Doc_Date ,TTTT. Item_Code ,  Item_Desc ,   TTTT. Opening_Qty, TTTT.Opening_Value , TTTT.Purchase_Qty , TTTT.TRF_Received , TTTT.Purchase_Received_Value , TTTT.ISSUE_TRF_Qty , TTTT.Lost_Qty , TTTT.Issue_Lost_Value, TTTT.RunningBalance_Qty, TTTT.RunningClosing_Value 
                                from (
                                select Final.SNO, Final.MCC ,TSPL_MCC_MASTER.MCC_NAME , Final.DocType , Final.Doc_No , Final.Doc_Date , Final.Particular_From ,Particular_From.Location_Desc as Particular_From_Name, Final.Particular_To , Live_Location.Customer_Name as Particular_To_Name,Final.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,
                                Opening_Qty+Final.FinalOpening_Qty as Opening_Qty,
                                Final.FinalOpening_Value + Opening_Value  as Opening_Value,
                                Purchase_Qty , TRF_Received , Purchase_Received_Value , ISSUE_TRF_Qty , final.Lost_Qty , final.Issue_Lost_Value, case when Final.DocType = 'Opening' then final.Closing_Qty else final.FinalClosingQty end as RunningBalance_Qty , case when Final.DocType = 'Opening' then final.Closing_Value else Final.FinalClosingValue end as RunningClosing_Value , 
                                case when len (final.Particular_To) > 0 then  case when Final.DocType = 'LOSS AT VSP' then LiveLocationOpningQty -  final.LiveLocationLostQty  - Lost_Qty  else  Final.FinalOpening_Qty - Final.FinalClosingQty end end as RunningBalance_Qty_Vendor
                                ,case when len (final.Particular_To) > 0 then case when Final.DocType = 'LOSS AT VSP' then LiveLocationOpningValue - LiveLocationLostValue -  LostValue else Final.FinalOpening_Value-Final.FinalClosingValue  end end as RunningClosing_Value_Vendor , final.Live_Location from (
                                select distinct TBL_ASSET_ISSUE.* 
                                , case when TBL_ASSET_ISSUE.DocType = 'LOSS AT VSP' then SUM (TBL_ASSET_ISSUE.FinalClosingQtyVendor  ) OVER (PARTITION BY TBL_ASSET_ISSUE.MCC,TBL_ASSET_ISSUE.Item_code ORDER BY TBL_ASSET_ISSUE.SNO) else 0 end as LiveLocationOpningQty, case when TBL_ASSET_ISSUE.DocType = 'LOSS AT VSP' then  TBL_ASSET_ISSUE3.Lost_Qty else 0 end  as LiveLocationLostQty 
                                ,case when TBL_ASSET_ISSUE.DocType = 'LOSS AT VSP' then SUM (TBL_ASSET_ISSUE.FinalClosingValueVendor  ) OVER (PARTITION BY TBL_ASSET_ISSUE.MCC,TBL_ASSET_ISSUE.Item_code ORDER BY TBL_ASSET_ISSUE.SNO) else 0 end as LiveLocationOpningValue, case when TBL_ASSET_ISSUE.DocType = 'LOSS AT VSP' then  TBL_ASSET_ISSUE3.LostValue else 0 end  as LiveLocationLostValue
                                from TBL_ASSET_ISSUE left outer join TBL_ASSET_ISSUE as TBL_ASSET_ISSUE2 on  TBL_ASSET_ISSUE.MCC = TBL_ASSET_ISSUE2.MCC and TBL_ASSET_ISSUE.Item_Code = TBL_ASSET_ISSUE2.Item_Code and TBL_ASSET_ISSUE.Live_Location = TBL_ASSET_ISSUE2.Live_Location and TBL_ASSET_ISSUE.sno > TBL_ASSET_ISSUE2.SNO 

                                left outer join TBL_ASSET_ISSUE as TBL_ASSET_ISSUE3 on  TBL_ASSET_ISSUE.MCC = TBL_ASSET_ISSUE3.MCC and TBL_ASSET_ISSUE.Item_Code = TBL_ASSET_ISSUE3.Item_Code and TBL_ASSET_ISSUE.Live_Location = TBL_ASSET_ISSUE3.Live_Location and TBL_ASSET_ISSUE.sno - TBL_ASSET_ISSUE3.SNO =1 
 
                                ) Final left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = Final.MCC
                                left  outer join TSPL_LOCATION_MASTER as Particular_From  on Particular_From.Location_Code = Final.Particular_From 
                                left  outer join TSPL_LOCATION_MASTER as Particular_To on Particular_To.Location_Code = Final.Particular_To
                                left outer join TSPL_CUSTOMER_MASTER as Live_Location on Live_Location.Cust_Code = Final.Live_Location
                                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = Final.Item_Code
                                ) TTTT 

                                ) ddddd group by  MCC ,  Doc_Date , Item_Code
                                order by MCC ,  Item_Code , convert (date, Doc_Date,103)  "



                Else
                '         strqry += " select  T1.SNO , T1.MCC ,TBL_MCC.Name as MCC_Name, T1.DocType , T1.Doc_No , T1.Doc_Date , T1.Particular_From ,TBL_Particular_From.Name as Particular_From_Name , T1.Particular_To ,TBL_Particular_To.Name as TBL_Particular_To_Name,T1.Item_Code , TSPL_ITEM_MASTER.Item_Desc as Item_Desc,case when T1.DocType = 'Opening' then T1.Opening_Qty else sum(T2.RunningBalance_Qty ) over (PARTITION BY T2.MCC, T2.Item_Code,T2.Doc_Date order by T2.MCC, T2.Item_Code,T2.Doc_No) end + case when T1.DocType = 'Opening' then 0 else isnull (ZZZ.DateWiseClosingQty,0) end as Opening_Qty, case when T1.DocType = 'Opening' then T1.Opening_Value  else sum(T2.RunningClosing_Value ) over (PARTITION BY T2.MCC, T2.Item_Code,T2.Doc_Date order by T2.MCC, T2.Item_Code,T2.Doc_No) end  + case when T1.DocType = 'Opening' then 0 else isnull (ZZZ.DateWiseClosingValue,0) end as Opening_Value ,convert(decimal (18,2), T1.Purchase_Qty ) as Purchase_Qty, T1.TRF_Received, T1.Purchase_Received_Value, T1.ISSUE_TRF_Qty, T1.Lost_Qty , T1.Issue_Lost_Value, T1.Closing_Qty, T1.Closing_Value ,  sum(T1.RunningBalance_Qty ) over (PARTITION BY T1.MCC, T1.Item_Code,T1.Doc_Date order by T1.MCC, T1.Item_Code,T1.Doc_No) + case when T1.DocType = 'Opening' then 0 else isnull (ZZZ.DateWiseClosingQty,0) end as RunningBalance_Qty, sum(T1.RunningClosing_Value ) over (PARTITION BY T1.MCC, T1.Item_Code,T1.Doc_Date order by T1.MCC, T1.Item_Code,T1.Doc_No)  + case when T1.DocType = 'Opening' then 0 else isnull (ZZZ.DateWiseClosingValue,0) end as RunningClosing_Value,  -1 * T1.RunningBalance_Qty_Vendor as RunningBalance_Qty_Vendor, -1 *  T1.RunningClosing_Value_Vendor as RunningClosing_Value_Vendor, T1.Live_Location, TBL_Live_Location.Name as  Live_Location_Name  FROM TBL_ASSET_ISSUE T1 
                '                     left outer join TBL_ASSET_ISSUE T2 on T1.SNO = T2.SNO2 and T1.MCC = T2.MCC  
                '                     left outer join (Select Location_code as Code , Location_Desc as Name from TSPL_LOCATION_MASTER )TBL_MCC on TBL_MCC.Code = T1.MCC
                'left outer join (Select Location_code as Code , Location_Desc as Name from TSPL_LOCATION_MASTER  Union  select Cust_Code as Code , Customer_Name as Name from TSPL_CUSTOMER_MASTER )TBL_Particular_From on TBL_Particular_From.Code = T1.Particular_From

                'left outer join (Select Location_code as Code , Location_Desc as Name from TSPL_LOCATION_MASTER  Union select Cust_Code as Code , Customer_Name as  Name from TSPL_CUSTOMER_MASTER )TBL_Particular_To on TBL_Particular_To.Code = T1.Particular_To

                'left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = T1.ITEM_CODE

                'left outer join (Select Location_code as Code , Location_Desc as Name from TSPL_LOCATION_MASTER  Union select Cust_Code as Code , Customer_Name as Name from TSPL_CUSTOMER_MASTER )TBL_Live_Location on TBL_Live_Location.Code = T1.Live_Location
                '                     left outer join (Select distinct T1.MCC, T1.Item_Code,T1.Doc_Date,sum(T1.RunningBalance_Qty ) over (PARTITION BY T1.MCC, T1.Item_Code,T1.Doc_Date order by T1.MCC, T1.Item_Code)  as DateWiseClosingQty, sum(T1.RunningClosing_Value ) over (PARTITION BY T1.MCC, T1.Item_Code,T1.Doc_Date order by T1.MCC, T1.Item_Code)  as DateWiseClosingValue FROM TBL_ASSET_ISSUE T1 
                '                     left outer join TBL_ASSET_ISSUE T2 on T1.SNO = T2.SNO2 and T1.MCC = T2.MCC  ) ZZZ on ZZZ.MCC = T1.MCC and ZZZ.Item_Code = t1.Item_Code 
                'and t1.Doc_Date  > ZZZ.Doc_Date   
                ' order by SNO asc


                '                   "
                If chkCurrentStock.Checked = True Then
                    strqry += " select xx.MCC as [MCC Code],mcc_name as [MCC Name],(case when len(xx.Particular_To)>0 then xx.Particular_To else xx.Particular_from end) as [VSP Code],(case when len(xx.Particular_To_Name)>0 then xx.Particular_To_Name else xx.Particular_From_Name end) as [VSP Name],xx.item_code as [Item Code],Item_Desc as [Item Name],Opening_Qty as [Opening Quantity],xx.RunningBalance_Qty_Vendor as [Quantity] from (select max(sno) as  sno,item_code,(case when len(Particular_To)>0 then Particular_To else Particular_from end) as Particular_To,mcc from TBL_ASSET_ISSUE 
                                where ((Particular_From is not null and Particular_From<>'') or (Particular_To is not null and Particular_To<>''))
                                group by item_code,mcc,(case when len(Particular_To)>0 then Particular_To else Particular_from end) )temp inner join (
                                select SNO,"
                Else
                    strqry += "  select "
                End If

                'case when len (final.Particular_To) > 0 or Final.DocType = 'ASSET Return' then  case when Final.DocType = 'LOSS AT VSP' then isnull (LiveLocationOpningQty,0) -  isnull (final.LiveLocationLostQty,0)  - isnull (Lost_Qty,0)  else  isnull (Final.FinalOpening_Qty,0) - isnull (Final.FinalClosingQty,0) end else 0 end  +  case when len (isnull (final.Particular_To,'')) > 0  or Final.DocType = 'ASSET Return' then  case when Final.DocType <> 'LOSS AT VSP'  then  sum ( isnull (Lost_Qty,0))  OVER (PARTITION BY MCC,final.Item_code,live_location ORDER BY SNO) else 0 end  else 0 end  as RunningBalance_Qty_Vendor
                ' ,case when len (final.Particular_To) > 0  or Final.DocType = 'ASSET Return' then case when Final.DocType = 'LOSS AT VSP' then isnull (LiveLocationOpningValue,0) - isnull (LiveLocationLostValue ,0) -  isnull (LostValue,0) else isnull (Final.FinalOpening_Value,0) - isnull (Final.FinalClosingValue,0)  end end  + case when Final.DocType <> 'LOSS AT VSP' or Final.DocType = 'ASSET Return' and len (isnull (final.Particular_To,'')) > 0  then  sum ( isnull (LostValue,0))  OVER (PARTITION BY MCC,final.Item_code,live_location ORDER BY SNO) else 0 end as RunningClosing_Value_Vendor ,
                strqry += " SSSS.MCC ,MCC_NAME,DocType,Doc_No,Doc_Date,Particular_From,Particular_From_Name,Particular_To,Particular_To_Name,Item_Code,Item_Desc,Opening_Qty,Opening_Value,Purchase_Qty , TRF_Received , Purchase_Received_Value , ISSUE_TRF_Qty , Lost_Qty , Issue_Lost_Value,RunningBalance_Qty,RunningClosing_Value, case when len (Particular_To) > 0 or DocType = 'ASSET Return' then  sum ( abs(Opening_Qty) - abs(RunningBalance_Qty) - Lost_Qty) OVER (PARTITION BY MCC,Item_code,live_location ORDER BY SNO) else 0 end as RunningBalance_Qty_Vendor 
                            , case when len (Particular_To) > 0 or DocType = 'ASSET Return' then  sum ( abs(Opening_Value) - abs(RunningClosing_Value) - LostValue) OVER (PARTITION BY MCC,Item_code,live_location ORDER BY SNO) else 0 end as RunningClosing_Value_Vendor, Live_Location from ( select SNO,  Final.MCC ,TSPL_MCC_MASTER.MCC_NAME , Final.DocType , Final.Doc_No , Final.Doc_Date , Final.Particular_From ,Particular_From.Location_Desc as Particular_From_Name, Final.Particular_To , Live_Location.Customer_Name as Particular_To_Name,Final.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,case when Final.DocType = 'Opening' then  Opening_Qty else Final.FinalOpening_Qty end as Opening_Qty, case when Final.DocType = 'Opening' then Opening_Value else Final.FinalOpening_Value end as Opening_Value, Purchase_Qty , TRF_Received , Purchase_Received_Value , ISSUE_TRF_Qty , final.Lost_Qty , final.Issue_Lost_Value, case when Final.DocType = 'Opening' then final.Closing_Qty else final.FinalClosingQty end as RunningBalance_Qty , case when Final.DocType = 'Opening' then final.Closing_Value else Final.FinalClosingValue end as RunningClosing_Value , 
                            LostValue,final.Live_Location from (
                            select distinct TBL_ASSET_ISSUE.* 
                            , case when TBL_ASSET_ISSUE.DocType = 'LOSS AT VSP' then SUM (TBL_ASSET_ISSUE.FinalClosingQtyVendor  ) OVER (PARTITION BY TBL_ASSET_ISSUE.MCC,TBL_ASSET_ISSUE.Item_code ORDER BY TBL_ASSET_ISSUE.SNO) else 0 end as LiveLocationOpningQty, case when TBL_ASSET_ISSUE.DocType = 'LOSS AT VSP' then  TBL_ASSET_ISSUE3.Lost_Qty else 0 end  as LiveLocationLostQty 
                            ,case when TBL_ASSET_ISSUE.DocType = 'LOSS AT VSP' then SUM (TBL_ASSET_ISSUE.FinalClosingValueVendor  ) OVER (PARTITION BY TBL_ASSET_ISSUE.MCC,TBL_ASSET_ISSUE.Item_code ORDER BY TBL_ASSET_ISSUE.SNO) else 0 end as LiveLocationOpningValue, case when TBL_ASSET_ISSUE.DocType = 'LOSS AT VSP' then  TBL_ASSET_ISSUE3.LostValue else 0 end  as LiveLocationLostValue
                            from TBL_ASSET_ISSUE left outer join TBL_ASSET_ISSUE as TBL_ASSET_ISSUE2 on  TBL_ASSET_ISSUE.MCC = TBL_ASSET_ISSUE2.MCC and TBL_ASSET_ISSUE.Item_Code = TBL_ASSET_ISSUE2.Item_Code and TBL_ASSET_ISSUE.Live_Location = TBL_ASSET_ISSUE2.Live_Location and TBL_ASSET_ISSUE.sno > TBL_ASSET_ISSUE2.SNO 

                            left outer join TBL_ASSET_ISSUE as TBL_ASSET_ISSUE3 on  TBL_ASSET_ISSUE.MCC = TBL_ASSET_ISSUE3.MCC and TBL_ASSET_ISSUE.Item_Code = TBL_ASSET_ISSUE3.Item_Code and TBL_ASSET_ISSUE.Live_Location = TBL_ASSET_ISSUE3.Live_Location and TBL_ASSET_ISSUE.sno - TBL_ASSET_ISSUE3.SNO =1 
 
                            ) Final left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = Final.MCC
                            left  outer join TSPL_LOCATION_MASTER as Particular_From  on Particular_From.Location_Code = Final.Particular_From 
                            left  outer join TSPL_LOCATION_MASTER as Particular_To on Particular_To.Location_Code = Final.Particular_To
                            left outer join TSPL_CUSTOMER_MASTER as Live_Location on Live_Location.Cust_Code = Final.Live_Location
                            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = Final.Item_Code ) SSSS "
                If chkCurrentStock.Checked = True Then
                    strqry += " )xx on temp.sno=xx.sno and temp.item_code=xx.Item_Code and temp.Particular_To=(case when len(xx.Particular_To)>0 then xx.Particular_To else xx.Particular_from end) inner join tspl_vendor_master on tspl_vendor_master.vendor_code=temp.Particular_To where xx.MCC is not null and xx.MCC<>'' and ((xx.Particular_From is not null and xx.Particular_From<>'') or (xx.Particular_To is not null and xx.Particular_To<>'')) "
                    If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                        strqry += " and temp.Particular_To in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                    End If
                    strqry += " order by [MCC Code],[Item Code] asc,Opening_Qty desc"
                Else
                    strqry += "  order by SNO "
                End If


            End If
            Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid2()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
            gv.BestFitColumns()
            Return
        End If

        sQuery = " (select case when TSPL_INVENTORY_MOVEMENT.InOut = 'O' then 'VSP Asset Issue' else 'VSP Asset Issue Return' end  as Transaction_Type,case when TSPL_INVENTORY_MOVEMENT.InOut = 'O' then 'OUT' else 'IN' end as InOut,case when TSPL_INVENTORY_MOVEMENT.inout ='O' then TSPL_VSPAsset_HEAD.Doc_No else '' end as Doc_No,case when TSPL_INVENTORY_MOVEMENT.inout ='O' then TSPL_VSPAsset_HEAD.Doc_Date else Null end  as Doc_Date,TSPL_VSPAsset_HEAD.Doc_Type,TSPL_VSPAsset_HEAD.From_Location as MCC,MCC_NAME ,TSPL_VSPAsset_HEAD.Issue_To AS Vsp_Code, TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VLC_MASTER_HEAD.VLC_CODE ,VLC_Name ,VLC_Code_VLC_Uploader,TSPL_MCC_ROUTE_MASTER.Route_Code ,Route_Name,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_INVENTORY_MOVEMENT.UOM ,tspl_serial_item.auto_sr_no ,  0 as Qty ,case when TSPL_INVENTORY_MOVEMENT.inout ='O' then (case when len(tspl_serial_item.auto_sr_no) >0 then 1 else  TSPL_INVENTORY_MOVEMENT.Qty end)  else 0 end as Issued_Qty, case when TSPL_INVENTORY_MOVEMENT.inout ='O' then    (case when len(TSPL_SERIAL_ITEM.Auto_Sr_No) >0 then 1*TSPL_VSPAsset_DETAIL.Unit_Cost  else  TSPL_INVENTORY_MOVEMENT.Net_Cost end)  else 0 end as Issued_Value ,0 as InQty,case when TSPL_INVENTORY_MOVEMENT.inout ='I' then TSPL_INVENTORY_MOVEMENT.Source_Doc_No  else '' end as [Return Document Code],case when TSPL_INVENTORY_MOVEMENT.inout ='I' then  TSPL_INVENTORY_MOVEMENT.Source_Doc_Date else Null end  as [Return Document Date],VSHRET.Doc_Type as [Return Document Type], case when TSPL_INVENTORY_MOVEMENT.inout ='I' then   (case when len(tspl_serial_item.auto_sr_no) >0 then 1 else  TSPL_INVENTORY_MOVEMENT.Qty end) else 0 end as Return_qty,case when TSPL_INVENTORY_MOVEMENT.inout ='I' then   TSPL_INVENTORY_MOVEMENT.Net_Cost  else 0 end as Return_value  from TSPL_INVENTORY_MOVEMENT  " & _
           " left join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No " & _
           " Left Outer Join TSPL_VSPAsset_HEAD VSHRET on VSHRET.Issue_No = TSPL_VSPAsset_HEAD.Doc_No" & _
           " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VSPAsset_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP' " & _
           " LEFT OUTER join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPAsset_HEAD.Issue_To " & _
           " left join TSPL_VSPAsset_DETAIL on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No AND TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_VSPAsset_DETAIL.Item_Code " & _
           " Left Outer Join TSPL_VSPAsset_DETAIL VSDRET on  VSDRET.Doc_No = VSHRET.Doc_No AND TSPL_INVENTORY_MOVEMENT.Item_Code = VSDRET.Item_Code " & _
           "  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT.Location_Code  " & _
           " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_VLC_MASTER_HEAD.Route_Code and TSPL_MCC_ROUTE_MASTER.MCC_Code =   TSPL_MCC_MASTER.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code   left join tspl_serial_item on tspl_serial_item.document_code=TSPL_VSPAsset_HEAD.Doc_No and tspl_serial_item.item_code=TSPL_INVENTORY_MOVEMENT.Item_Code where  TSPL_ITEM_MASTER.item_type='A' and  convert(date,TSPL_INVENTORY_MOVEMENT.Punching_Date,103) >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_INVENTORY_MOVEMENT.Punching_Date,103)<=convert(date,'" + Todate + "',103) AND TSPL_INVENTORY_MOVEMENT.Trans_Type in ('MCC-AISSUE','MCC-ARETURN') and TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_MCC_MASTER.MCC_Code "

        If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_MCC_MASTER.MCC_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
        End If
        If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_ITEM_MASTER.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
        End If
        sQuery += ")"




        If cboType.SelectedIndex = -1 Then
            If rbtnSummary.IsChecked = True Then
                If chkCurrentStock.Checked = True Then
                    sQuery = "  select MCC,max(MCC_NAME )MCC_NAME,VSP_CODE,MAX(TSPL_VENDOR_MASTER.Vendor_Name) AS VSP_NAME,Item_Code ,max(Item_Desc ) as Item_Desc,uom,sum(openingQty) as Op_qty,sum(INQty) as Issue_Qty,sum(OutQty ) as Return_qty,sum(openingQty+INQty-OutQty) as CL_Qty " &
                                                 " from ( "
                Else
                    sQuery = "  select MCC,max(MCC_NAME )MCC_NAME,Item_Code ,max(Item_Desc ) as Item_Desc,uom,sum(openingQty) as Op_qty,sum(INQty) as Issue_Qty,sum(OutQty ) as Return_qty,sum(openingQty+INQty-OutQty) as CL_Qty " &
                                            " from ( "
                End If
                sQuery += " select MCC ,max(MCC_NAME )MCC_NAME,Item_Code ,max(Item_Desc ) as Item_Desc,uom," &
                        " sum((INQty+OutQty) *type) as openingQty," &
                        " 0 as INQty,0 as InValue,0 as OutQty,0 as OutValue,VSP_CODE  from (" &
                        " select 'VSP Asset Issue' as Transaction_Type,InOut,TSPL_INVENTORY_MOVEMENT.Source_Doc_No ,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date ,TSPL_INVENTORY_MOVEMENT.Location_Code  as MCC,MCC_NAME ,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,tspl_serial_item.auto_sr_no ,  " &
                        " case when len(tspl_serial_item.auto_sr_no) >0 then 1 else  TSPL_INVENTORY_MOVEMENT.Qty  end as OutQty," &
                         " TSPL_INVENTORY_MOVEMENT.Net_Cost as OutValue," &
                        " 0 as INQty," &
                        " 0 as InValue," &
                        " 1 as Type, TSPL_VSPAsset_HEAD.Issue_To as VSP_CODE" &
                        " from TSPL_INVENTORY_MOVEMENT   left join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VSPAsset_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  LEFT OUTER join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPAsset_HEAD.Issue_To  left join TSPL_VSPAsset_DETAIL on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No AND TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_VSPAsset_DETAIL.Item_Code  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT.Location_Code    left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_VLC_MASTER_HEAD.Route_Code and TSPL_MCC_ROUTE_MASTER.MCC_Code =   TSPL_MCC_MASTER.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code left join tspl_serial_item on tspl_serial_item.document_code=TSPL_VSPAsset_HEAD.Doc_No and tspl_serial_item.item_code=TSPL_INVENTORY_MOVEMENT.Item_Code "
                sQuery += "where  item_type='A' AND convert(date,Punching_Date,103)<(convert(date,'" & fromDate & "',103)) and TSPL_INVENTORY_MOVEMENT .Trans_Type = 'IC-AD' and " &
                             " inout='O'and " &
                            " TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_MCC_MASTER.MCC_Code    "
                If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MCC_MASTER.MCC_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                End If
                If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                End If
                If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_ITEM_MASTER.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                End If
                sQuery += "   union all" &
                            " select 'VSP Asset Issue' as Transaction_Type,InOut,TSPL_INVENTORY_MOVEMENT.Source_Doc_No ,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date ,TSPL_INVENTORY_MOVEMENT.Location_Code  as MCC,MCC_NAME ,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,tspl_serial_item.auto_sr_no ,  " &
                            " 0   as OutQty," &
                            " 0 as OutValue," &
                            " case when len(tspl_serial_item.auto_sr_no) >0 then 1 else  TSPL_INVENTORY_MOVEMENT.Qty end as INQty," &
                            " TSPL_INVENTORY_MOVEMENT.Net_Cost as InValue," &
                            " -1 as type, TSPL_VSPAsset_HEAD.Issue_To as VSP_CODE " &
                            " from TSPL_INVENTORY_MOVEMENT   left join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VSPAsset_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  LEFT OUTER join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPAsset_HEAD.Issue_To  left join TSPL_VSPAsset_DETAIL on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No AND TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_VSPAsset_DETAIL.Item_Code  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT.Location_Code    left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_VLC_MASTER_HEAD.Route_Code and TSPL_MCC_ROUTE_MASTER.MCC_Code =   TSPL_MCC_MASTER.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code left join tspl_serial_item on tspl_serial_item.document_code=TSPL_VSPAsset_HEAD.Doc_No and tspl_serial_item.item_code=TSPL_INVENTORY_MOVEMENT.Item_Code "

                sQuery += "  where  item_type='A' AND convert(date,Punching_Date,103)<(convert(date,'" & fromDate & "',103)) and TSPL_INVENTORY_MOVEMENT .Trans_Type = 'IC-AD' and " &
               " inout='I'and " &
               " TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_MCC_MASTER.MCC_Code    "
                If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MCC_MASTER.MCC_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                End If
                If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                End If
                If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_ITEM_MASTER.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                End If
                sQuery += " union all" &
                                   " select 'VSP Asset Issue' as Transaction_Type,InOut,TSPL_INVENTORY_MOVEMENT.Source_Doc_No ,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date ,TSPL_INVENTORY_MOVEMENT.Location_Code  as MCC,MCC_NAME ,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,tspl_serial_item.auto_sr_no , " &
                                   " 0   as OutQty," &
                                   " 0 as OutValue," &
                                    " case when len(tspl_serial_item.auto_sr_no) >0 then 1 else  TSPL_INVENTORY_MOVEMENT.Qty end as INQty," &
                                    " TSPL_INVENTORY_MOVEMENT.Net_Cost as InValue," &
                                   " 1 as type, TSPL_VSPAsset_HEAD.Issue_To as VSP_CODE " &
                                   " from TSPL_INVENTORY_MOVEMENT   left join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VSPAsset_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  LEFT OUTER join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPAsset_HEAD.Issue_To  left join TSPL_VSPAsset_DETAIL on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No AND TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_VSPAsset_DETAIL.Item_Code  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT.Location_Code    left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_VLC_MASTER_HEAD.Route_Code and TSPL_MCC_ROUTE_MASTER.MCC_Code =   TSPL_MCC_MASTER.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code left join tspl_serial_item on tspl_serial_item.document_code=TSPL_VSPAsset_HEAD.Doc_No and tspl_serial_item.item_code=TSPL_INVENTORY_MOVEMENT.Item_Code "
                sQuery += " where  item_type='A' AND convert(date,Punching_Date,103)<(convert(date,'" & fromDate & "',103)) and TSPL_INVENTORY_MOVEMENT .Trans_Type = 'MCC-AISSUE' and " &
                                  " inout='O'and " &
                                   " TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_MCC_MASTER.MCC_Code     "
                If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MCC_MASTER.MCC_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                End If
                If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                End If
                If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_ITEM_MASTER.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                End If
                sQuery += " union all" &
                              " select 'VSP Asset Issue' as Transaction_Type,InOut,TSPL_INVENTORY_MOVEMENT.Source_Doc_No ,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date ,TSPL_INVENTORY_MOVEMENT.Location_Code  as MCC,MCC_NAME ,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,tspl_serial_item.auto_sr_no ,  " &
                               " case when len(tspl_serial_item.auto_sr_no) >0 then 1 else  TSPL_INVENTORY_MOVEMENT.Qty end   as OutQty," &
                              " TSPL_INVENTORY_MOVEMENT.Net_Cost as OutValue," &
                                " 0  as INQty," &
                                " 0 as InValue," &
                                " -1 as type, TSPL_VSPAsset_HEAD.Issue_To as VSP_CODE " &
                                " from TSPL_INVENTORY_MOVEMENT   left join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VSPAsset_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  LEFT OUTER join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPAsset_HEAD.Issue_To  left join TSPL_VSPAsset_DETAIL on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No AND TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_VSPAsset_DETAIL.Item_Code  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT.Location_Code    left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_VLC_MASTER_HEAD.Route_Code and TSPL_MCC_ROUTE_MASTER.MCC_Code =   TSPL_MCC_MASTER.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code left join tspl_serial_item on tspl_serial_item.document_code=TSPL_VSPAsset_HEAD.Doc_No and tspl_serial_item.item_code=TSPL_INVENTORY_MOVEMENT.Item_Code "
                sQuery += " where  item_type='A' AND convert(date,Punching_Date,103)<(convert(date,'" & fromDate & "',103)) and TSPL_INVENTORY_MOVEMENT .Trans_Type = 'MCC-ARETURN' and " &
                    " inout='I'and " &
                   "  TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_MCC_MASTER.MCC_Code   "
                If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MCC_MASTER.MCC_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                End If
                If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                End If
                If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_ITEM_MASTER.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                End If
                sQuery += " ) as Opening group by MCC,VSP_CODE ,Item_Code ,UOM "

                '--=============bada wala union ===================================================
                sQuery += "  union all" &
                              " select MCC ,(MCC_NAME )MCC_NAME,Item_Code ,(Item_Desc ) as Item_Desc,uom,0 as openingQty,INQty as INQty,InValue as InValue,OutQty as OutQty,OutValue as OutValue,VSP_CODE  from (" &
                               " select 'VSP Asset Issue' as Transaction_Type,InOut,TSPL_INVENTORY_MOVEMENT.Source_Doc_No ,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date ,TSPL_INVENTORY_MOVEMENT.Location_Code  as MCC,MCC_NAME ,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,tspl_serial_item.auto_sr_no ,  " &
                               " 0   as OutQty," &
                               " 0 as OutValue," &
                               " case when len(tspl_serial_item.auto_sr_no) >0 then 1 else  TSPL_INVENTORY_MOVEMENT.Qty end as INQty," &
                               " TSPL_INVENTORY_MOVEMENT.Net_Cost as InValue," &
                               " 1 as type, TSPL_VSPAsset_HEAD.Issue_To as VSP_CODE " &
                               " from TSPL_INVENTORY_MOVEMENT   left join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VSPAsset_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  LEFT OUTER join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPAsset_HEAD.Issue_To  left join TSPL_VSPAsset_DETAIL on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No AND TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_VSPAsset_DETAIL.Item_Code  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT.Location_Code    left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_VLC_MASTER_HEAD.Route_Code and TSPL_MCC_ROUTE_MASTER.MCC_Code =   TSPL_MCC_MASTER.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code left join tspl_serial_item on tspl_serial_item.document_code=TSPL_VSPAsset_HEAD.Doc_No and tspl_serial_item.item_code=TSPL_INVENTORY_MOVEMENT.Item_Code "
                sQuery += " where  item_type='A' AND convert(date,Punching_Date,103)>=convert(date,'" & fromDate & "',103) AND convert(date,Punching_Date,103)<=convert(date,'" & Todate & "',103)  and TSPL_INVENTORY_MOVEMENT .Trans_Type = 'MCC-AISSUE' and " &
                              " inout='O'and " &
                              " TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_MCC_MASTER.MCC_Code  "
                If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_MCC_MASTER.MCC_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                End If
                If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                End If
                If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                    sQuery += " and TSPL_ITEM_MASTER.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                End If
                sQuery += " union all" &
        " select 'VSP Asset Issue' as Transaction_Type,InOut,TSPL_INVENTORY_MOVEMENT.Source_Doc_No ,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date ,TSPL_INVENTORY_MOVEMENT.Location_Code  as MCC,MCC_NAME ,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,tspl_serial_item.auto_sr_no ,  " &
        " case when len(tspl_serial_item.auto_sr_no) >0 then 1 else  TSPL_INVENTORY_MOVEMENT.Qty end   as OutQty," &
        " TSPL_INVENTORY_MOVEMENT.Net_Cost as OutValue," &
        " 0  as INQty," &
        " 0 as InValue," &
        " -1 as type, TSPL_VSPAsset_HEAD.Issue_To as VSP_CODE " &
        " from TSPL_INVENTORY_MOVEMENT   left join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VSPAsset_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  LEFT OUTER join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VSPAsset_HEAD.Issue_To  left join TSPL_VSPAsset_DETAIL on  TSPL_VSPAsset_DETAIL.Doc_No = TSPL_VSPAsset_HEAD.Doc_No AND TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_VSPAsset_DETAIL.Item_Code  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_INVENTORY_MOVEMENT.Location_Code    left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code  = TSPL_VLC_MASTER_HEAD.Route_Code and TSPL_MCC_ROUTE_MASTER.MCC_Code =   TSPL_MCC_MASTER.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code left join tspl_serial_item on tspl_serial_item.document_code=TSPL_VSPAsset_HEAD.Doc_No and tspl_serial_item.item_code=TSPL_INVENTORY_MOVEMENT.Item_Code "
                sQuery += "where  item_type='A' AND convert(date,Punching_Date,103)>=convert(date,'" & fromDate & "',103) AND convert(date,Punching_Date,103)<=convert(date,'" & Todate & "',103)  and TSPL_INVENTORY_MOVEMENT .Trans_Type = 'MCC-ARETURN' and " &
        " inout='I'and " &
        " TSPL_INVENTORY_MOVEMENT.Location_Code = TSPL_MCC_MASTER.MCC_Code   "
                If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_MCC_MASTER.MCC_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
                    End If
                    If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code in  (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") "
                    End If
                    If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                        sQuery += " and TSPL_ITEM_MASTER.Item_Code in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
                    End If
                sQuery += " ) as Opening "
                If chkCurrentStock.Checked = True Then
                    sQuery += " ) as zz  LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.VENDOR_CODE=zz.VSP_CODE group by MCC,VSP_CODE ,Item_Code ,UOM "
                    strqry += " " + sQuery + " "
                Else
                    sQuery += " ) as zz  group by MCC ,Item_Code ,UOM "
                    strqry += " " + sQuery + " "
                End If


            ElseIf rbtnDetail.IsChecked = True Then
                strqry += " " + sQuery + " "

            End If

            Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(strqry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
        Else
            Dim strqry1 As String = " select Transaction_Type as [Transaction Type], InOut,Doc_No as [Document No] ,Doc_Date as [Document Date], MCC as [MCC Code], MCC_NAME as [MCC Name] , Vsp_Code as [Vendor Code],  Vendor_Name as [Vendor Name],Item_Code ,  Item_Desc as [Item Name] ,Issued_Qty as Qty,Return_Qty  ,UOM ,Route_Code as [Route Code], Route_Name as [Route Name], VLC_CODE as [VLC Code],VLC_Name as [VLC Name] , VLC_Code_VLC_Uploader as [VLC Data Code] from " + sQuery + " as dd"
            Dim dtgv1 As DataTable = clsDBFuncationality.GetDataTable(strqry1)
        If dtgv1 IsNot Nothing And dtgv1.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.ReadOnly = True
            gv.DataSource = dtgv1
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGriddrill()
            gv.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else

                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                If FlagDrillDownNoData = False AndAlso cboType.SelectedIndex = 0 AndAlso rbtnSummary.IsChecked = True Then
                    FlagDrillDownNoData = True
                End If
            End If
        ReStoreGridLayout()
        End If

    End Sub
    Sub FormatGrid()


        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False

        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If rbtnSummary.IsChecked = True Then
            If cboType.SelectedIndex = -1 Then

                If chkCurrentStock.Checked = True Then
                    If gv.Columns.Contains("VSP_CODE") Then
                        gv.Columns("VSP_CODE").IsVisible = True
                        gv.Columns("VSP_CODE").Width = 100
                        gv.Columns("VSP_CODE").HeaderText = "VSP Code"
                    End If
                    If gv.Columns.Contains("VSP_NAME") Then
                        gv.Columns("VSP_NAME").IsVisible = True
                        gv.Columns("VSP_NAME").Width = 100
                        gv.Columns("VSP_NAME").HeaderText = "VSP Name"
                    End If
                End If

                gv.Columns("MCC").IsVisible = True
                gv.Columns("MCC").Width = 100
                gv.Columns("MCC").HeaderText = "MCC Code"

                gv.Columns("MCC_NAME").IsVisible = True
                gv.Columns("MCC_NAME").Width = 100
                gv.Columns("MCC_NAME").HeaderText = "MCC Name"

                'gv.Columns("Vsp_Code").IsVisible = True
                'gv.Columns("Vsp_Code").Width = 100
                'gv.Columns("Vsp_Code").HeaderText = " VSP Code"

                'gv.Columns("Vendor_Name").IsVisible = True
                'gv.Columns("Vendor_Name").Width = 100
                'gv.Columns("Vendor_Name").HeaderText = "VSP Name"

                gv.Columns("Item_Code").IsVisible = True
                gv.Columns("Item_Code").Width = 80
                gv.Columns("Item_Code").HeaderText = "Item Code"

                gv.Columns("Item_Desc").IsVisible = True
                gv.Columns("Item_Desc").Width = 150
                gv.Columns("Item_Desc").HeaderText = "Item Desc"

                gv.Columns("UOM").IsVisible = True
                gv.Columns("UOM").Width = 50
                gv.Columns("UOM").HeaderText = "Unit Code"

                gv.Columns("Op_qty").IsVisible = True
                gv.Columns("Op_qty").Width = 100
                gv.Columns("Op_qty").HeaderText = "Opening"

                gv.Columns("Issue_Qty").IsVisible = True
                gv.Columns("Issue_Qty").Width = 100
                gv.Columns("Issue_Qty").HeaderText = "Issued Qty"

                gv.Columns("Return_qty").IsVisible = True
                gv.Columns("Return_qty").Width = 100
                gv.Columns("Return_qty").HeaderText = "Return Qty"

                gv.Columns("CL_Qty").IsVisible = True
                gv.Columns("CL_Qty").Width = 100
                gv.Columns("CL_Qty").HeaderText = "Closing"



                'Dim intCount As Integer = 0

                Dim item1 As New GridViewSummaryItem("Op_qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Issue_Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Return_qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("CL_Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
            End If

        Else
            gv.Columns("Doc_No").IsVisible = True
            gv.Columns("Doc_No").Width = 100
            gv.Columns("Doc_No").HeaderText = "Document Code"

            gv.Columns("Doc_Date").IsVisible = True
            gv.Columns("Doc_Date").Width = 100
            gv.Columns("Doc_Date").HeaderText = "Document Date"

            gv.Columns("Transaction_Type").IsVisible = True
            gv.Columns("Transaction_Type").Width = 100
            gv.Columns("Transaction_Type").HeaderText = "Transaction Type"

            gv.Columns("MCC").IsVisible = True
            gv.Columns("MCC").Width = 100
            gv.Columns("MCC").HeaderText = "MCC Code"

            gv.Columns("MCC_NAME").IsVisible = True
            gv.Columns("MCC_NAME").Width = 100
            gv.Columns("MCC_NAME").HeaderText = "MCC Name"

            gv.Columns("Vsp_Code").IsVisible = True
            gv.Columns("Vsp_Code").Width = 100
            gv.Columns("Vsp_Code").HeaderText = " VSP Code"

            gv.Columns("Vendor_Name").IsVisible = True
            gv.Columns("Vendor_Name").Width = 100
            gv.Columns("Vendor_Name").HeaderText = "VSP Name"

            gv.Columns("Item_Code").IsVisible = True
            gv.Columns("Item_Code").Width = 80
            gv.Columns("Item_Code").HeaderText = "Item Code"

            gv.Columns("Item_Desc").IsVisible = True
            gv.Columns("Item_Desc").Width = 150
            gv.Columns("Item_Desc").HeaderText = "Item Desc"

            gv.Columns("auto_sr_no").IsVisible = True
            gv.Columns("auto_sr_no").Width = 80
            gv.Columns("auto_sr_no").HeaderText = "Serial No."

            gv.Columns("UOM").IsVisible = True
            gv.Columns("UOM").Width = 50
            gv.Columns("UOM").HeaderText = "Unit Code"

            gv.Columns("Issued_Qty").IsVisible = True
            gv.Columns("Issued_Qty").Width = 50
            gv.Columns("Issued_Qty").HeaderText = "Issue Qty"

            gv.Columns("Issued_Value").IsVisible = True
            gv.Columns("Issued_Value").Width = 50
            gv.Columns("Issued_Value").HeaderText = "Issue Value"

            gv.Columns("Route_Code").IsVisible = True
            gv.Columns("Route_Code").Width = 50
            gv.Columns("Route_Code").HeaderText = "Route Code"

            gv.Columns("Route_Name").IsVisible = True
            gv.Columns("Route_Name").Width = 50
            gv.Columns("Route_Name").HeaderText = "Route Name"

            gv.Columns("VLC_CODE").IsVisible = True
            gv.Columns("VLC_CODE").Width = 50
            gv.Columns("VLC_CODE").HeaderText = "VLC Code"


            gv.Columns("VLC_Name").IsVisible = True
            gv.Columns("VLC_Name").Width = 50
            gv.Columns("VLC_Name").HeaderText = "VLC Name"

            gv.Columns("Return Document Code").IsVisible = True
            gv.Columns("Return Document Code").Width = 100
            gv.Columns("Return Document Code").HeaderText = "Return Document Code"

            gv.Columns("Return Document Date").IsVisible = True
            gv.Columns("Return Document Date").Width = 100
            gv.Columns("Return Document Date").HeaderText = "Return Document Date"

            gv.Columns("Return_qty").IsVisible = True
            gv.Columns("Return_qty").Width = 50
            gv.Columns("Return_qty").HeaderText = "Return Qty"

            gv.Columns("Return_Value").IsVisible = True
            gv.Columns("Return_Value").Width = 50
            gv.Columns("Return_Value").HeaderText = "Return Value"

            gv.Columns("VLC_Code_VLC_Uploader").IsVisible = True
            gv.Columns("VLC_Code_VLC_Uploader").Width = 50
            gv.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Uploader Code"

            Dim item5 As New GridViewSummaryItem("Issued_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Issued_Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Return_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("Return_value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
        End If





        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Sub FormatGrid2()


        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False

        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If chkCurrentStock.Checked = True Then
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).IsVisible = True
            Next
        ElseIf rbtnDetail.IsChecked = True Then
            If cboType.SelectedIndex = -1 Then

                gv.Columns("MCC").IsVisible = True
                gv.Columns("MCC").Width = 100
                gv.Columns("MCC").HeaderText = "MCC Code"

                gv.Columns("MCC_Name").IsVisible = True
                gv.Columns("MCC_Name").Width = 100
                gv.Columns("MCC_Name").HeaderText = "MCC Name"

                gv.Columns("Particular_From_Name").IsVisible = True
                gv.Columns("Particular_From_Name").Width = 100
                gv.Columns("Particular_From_Name").HeaderText = "Particular From Name"

                gv.Columns("Particular_To_Name").IsVisible = True
                gv.Columns("Particular_To_Name").Width = 100
                gv.Columns("Particular_To_Name").HeaderText = "Particular To Name"

                gv.Columns("Item_Desc").IsVisible = True
                gv.Columns("Item_Desc").Width = 100
                gv.Columns("Item_Desc").HeaderText = "Item Name"

                'gv.Columns("DocType_SNO").IsVisible = False
                'gv.Columns("DocType_SNO").Width = 100
                'gv.Columns("DocType_SNO").HeaderText = "DocType_SNO"
                If gv.Columns.Contains("SNO") = True Then
                    gv.Columns("SNO").IsVisible = False
                    gv.Columns("SNO").Width = 100
                    gv.Columns("SNO").HeaderText = "SNO"
                End If

                If gv.Columns.Contains("DocType") = True Then
                    gv.Columns("DocType").IsVisible = True
                    gv.Columns("DocType").Width = 100
                    gv.Columns("DocType").HeaderText = "Doc Type"
                End If

                If gv.Columns.Contains("Doc_No") = True Then
                    gv.Columns("Doc_No").IsVisible = True
                    gv.Columns("Doc_No").Width = 100
                    gv.Columns("Doc_No").HeaderText = "Ref.No."
                End If


                gv.Columns("Doc_Date").IsVisible = True
                gv.Columns("Doc_Date").Width = 100
                gv.Columns("Doc_Date").HeaderText = "Date"

                gv.Columns("Particular_From").IsVisible = True
                gv.Columns("Particular_From").Width = 100
                gv.Columns("Particular_From").HeaderText = "Particular From"

                gv.Columns("Particular_To").IsVisible = True
                gv.Columns("Particular_To").Width = 100
                gv.Columns("Particular_To").HeaderText = "Particular To"


                gv.Columns("Item_Code").IsVisible = True
                gv.Columns("Item_Code").Width = 80
                gv.Columns("Item_Code").HeaderText = "Item Code"

                'gv.Columns("Item_Desc").IsVisible = True
                'gv.Columns("Item_Desc").Width = 150
                'gv.Columns("Item_Desc").HeaderText = "Item Desc"

                'gv.Columns("UOM").IsVisible = True
                'gv.Columns("UOM").Width = 50
                'gv.Columns("UOM").HeaderText = "Unit Code"

                gv.Columns("Opening_Qty").IsVisible = True
                gv.Columns("Opening_Qty").Width = 100
                gv.Columns("Opening_Qty").HeaderText = "Opening Qty"

                'gv.Columns("Opening_Qty").IsVisible = True
                'gv.Columns("Opening_Qty").Width = 100
                'gv.Columns("Opening_Qty").HeaderText = "Opening Qty"

                gv.Columns("Opening_Value").IsVisible = True
                gv.Columns("Opening_Value").Width = 100
                gv.Columns("Opening_Value").HeaderText = "Opening Value"

                'gv.Columns("Opening_Value").IsVisible = True
                'gv.Columns("Opening_Value").Width = 100
                'gv.Columns("Opening_Value").HeaderText = "Opening Value"

                gv.Columns("Purchase_Qty").IsVisible = True
                gv.Columns("Purchase_Qty").Width = 100
                gv.Columns("Purchase_Qty").HeaderText = "Purchase Qty"

                gv.Columns("TRF_Received").IsVisible = True
                gv.Columns("TRF_Received").Width = 100
                gv.Columns("TRF_Received").HeaderText = "Trf. Received Qty"

                gv.Columns("Purchase_Received_Value").IsVisible = True
                gv.Columns("Purchase_Received_Value").Width = 100
                gv.Columns("Purchase_Received_Value").HeaderText = "Purchase/Received Value"

                gv.Columns("ISSUE_TRF_Qty").IsVisible = True
                gv.Columns("ISSUE_TRF_Qty").Width = 100
                gv.Columns("ISSUE_TRF_Qty").HeaderText = "Issue/Trf. Qty"

                gv.Columns("Lost_Qty").IsVisible = True
                gv.Columns("Lost_Qty").Width = 100
                gv.Columns("Lost_Qty").HeaderText = "Lost Qty"

                gv.Columns("Issue_Lost_Value").IsVisible = True
                gv.Columns("Issue_Lost_Value").Width = 100
                gv.Columns("Issue_Lost_Value").HeaderText = "Issue/Trf./Lost Value"

                If gv.Columns.Contains("Closing_Qty") = True Then
                    gv.Columns("Closing_Qty").IsVisible = False
                    gv.Columns("Closing_Qty").Width = 100
                    gv.Columns("Closing_Qty").HeaderText = "Closing_Qty"
                End If

                If gv.Columns.Contains("Closing_Value") = True Then
                    gv.Columns("Closing_Value").IsVisible = False
                    gv.Columns("Closing_Value").Width = 100
                    gv.Columns("Closing_Value").HeaderText = "Closing_Value"
                End If

                If gv.Columns.Contains("Live_Location") = True Then
                    gv.Columns("Live_Location").IsVisible = True
                    gv.Columns("Live_Location").Width = 100
                    gv.Columns("Live_Location").HeaderText = "Live Location"
                End If

                If gv.Columns.Contains("RunningBalance_Qty") = True Then
                    gv.Columns("RunningBalance_Qty").IsVisible = True
                    gv.Columns("RunningBalance_Qty").Width = 100
                    gv.Columns("RunningBalance_Qty").HeaderText = "Closing Qty"
                End If

                If gv.Columns.Contains("RunningClosing_Value") = True Then
                    gv.Columns("RunningClosing_Value").IsVisible = True
                    gv.Columns("RunningClosing_Value").Width = 100
                    gv.Columns("RunningClosing_Value").HeaderText = "Closing Value"
                End If

                If gv.Columns.Contains("RunningBalance_Qty_Vendor") = True Then
                    gv.Columns("RunningBalance_Qty_Vendor").IsVisible = True
                    gv.Columns("RunningBalance_Qty_Vendor").Width = 100
                    gv.Columns("RunningBalance_Qty_Vendor").HeaderText = "VSP Closing Qty"
                End If

                If gv.Columns.Contains("RunningClosing_Value_Vendor") = True Then
                    gv.Columns("RunningClosing_Value_Vendor").IsVisible = True
                    gv.Columns("RunningClosing_Value_Vendor").Width = 100
                    gv.Columns("RunningClosing_Value_Vendor").HeaderText = "VSP Closing Value"
                End If


                'Dim item1 As New GridViewSummaryItem("Op_qty", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item1)
                'Dim item2 As New GridViewSummaryItem("Issue_Qty", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item2)
                'Dim item3 As New GridViewSummaryItem("Return_qty", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item3)
                'Dim item4 As New GridViewSummaryItem("CL_Qty", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item4)
            End If

        Else

            gv.Columns("MCC").IsVisible = True
            gv.Columns("MCC").Width = 100
            gv.Columns("MCC").HeaderText = "MCC Code"

            gv.Columns("MCC_Name").IsVisible = True
            gv.Columns("MCC_Name").Width = 100
            gv.Columns("MCC_Name").HeaderText = "MCC Name"

            'gv.Columns("DocType_SNO").IsVisible = False
            'gv.Columns("DocType_SNO").Width = 100
            'gv.Columns("DocType_SNO").HeaderText = "DocType_SNO"

            'gv.Columns("SNO").IsVisible = False
            'gv.Columns("SNO").Width = 100
            'gv.Columns("SNO").HeaderText = "SNO"

            'gv.Columns("DocType").IsVisible = True
            'gv.Columns("DocType").Width = 100
            'gv.Columns("DocType").HeaderText = "Doc Type"

            'gv.Columns("Doc_No").IsVisible = True
            'gv.Columns("Doc_No").Width = 100
            'gv.Columns("Doc_No").HeaderText = "Ref.No."

            gv.Columns("Doc_Date").IsVisible = True
            gv.Columns("Doc_Date").Width = 100
            gv.Columns("Doc_Date").HeaderText = "Date"

            'gv.Columns("Particular_From").IsVisible = True
            'gv.Columns("Particular_From").Width = 100
            'gv.Columns("Particular_From").HeaderText = "Particular From"

            'gv.Columns("Particular_To").IsVisible = True
            'gv.Columns("Particular_To").Width = 100
            'gv.Columns("Particular_To").HeaderText = "Particular To"


            gv.Columns("Item_Code").IsVisible = True
            gv.Columns("Item_Code").Width = 80
            gv.Columns("Item_Code").HeaderText = "Item Code"

            gv.Columns("Item_Desc").IsVisible = True
            gv.Columns("Item_Desc").Width = 150
            gv.Columns("Item_Desc").HeaderText = "Item Name"

            'gv.Columns("UOM").IsVisible = True
            'gv.Columns("UOM").Width = 50
            'gv.Columns("UOM").HeaderText = "Unit Code"

            gv.Columns("Opening_Qty").IsVisible = True
            gv.Columns("Opening_Qty").Width = 100
            gv.Columns("Opening_Qty").HeaderText = "Opening Qty"



            gv.Columns("Opening_Value").IsVisible = True
            gv.Columns("Opening_Value").Width = 100
            gv.Columns("Opening_Value").HeaderText = "Opening Value"



            gv.Columns("Purchase_Qty").IsVisible = True
            gv.Columns("Purchase_Qty").Width = 100
            gv.Columns("Purchase_Qty").HeaderText = "Purchase Qty"

            gv.Columns("TRF_Received").IsVisible = True
            gv.Columns("TRF_Received").Width = 100
            gv.Columns("TRF_Received").HeaderText = "Trf. Received Qty"

            gv.Columns("Purchase_Received_Value").IsVisible = True
            gv.Columns("Purchase_Received_Value").Width = 100
            gv.Columns("Purchase_Received_Value").HeaderText = "Purchase/Received Value"

            gv.Columns("ISSUE_TRF_Qty").IsVisible = True
            gv.Columns("ISSUE_TRF_Qty").Width = 100
            gv.Columns("ISSUE_TRF_Qty").HeaderText = "Issue/Trf. Qty"

            gv.Columns("Lost_Qty").IsVisible = True
            gv.Columns("Lost_Qty").Width = 100
            gv.Columns("Lost_Qty").HeaderText = "Lost Qty"

            'gv.Columns("Issue_Lost_Value").IsVisible = True
            'gv.Columns("Issue_Lost_Value").Width = 100
            'gv.Columns("Issue_Lost_Value").HeaderText = "Issue/Trf./Lost Value"

            'gv.Columns("Closing_Qty").IsVisible = False
            'gv.Columns("Closing_Qty").Width = 100
            'gv.Columns("Closing_Qty").HeaderText = "Closing_Qty"

            'gv.Columns("Closing_Value").IsVisible = False
            'gv.Columns("Closing_Value").Width = 100
            'gv.Columns("Closing_Value").HeaderText = "Closing_Value"

            'gv.Columns("Live_Location").IsVisible = True
            'gv.Columns("Live_Location").Width = 100
            'gv.Columns("Live_Location").HeaderText = "Live Location"

            gv.Columns("RunningBalance_Qty").IsVisible = True
            gv.Columns("RunningBalance_Qty").Width = 100
            gv.Columns("RunningBalance_Qty").HeaderText = "Closing Qty"

            gv.Columns("RunningClosing_Value").IsVisible = True
            gv.Columns("RunningClosing_Value").Width = 100
            gv.Columns("RunningClosing_Value").HeaderText = "Closing Value"

            'gv.Columns("RunningBalance_Qty_Vendor").IsVisible = True
            'gv.Columns("RunningBalance_Qty_Vendor").Width = 100
            'gv.Columns("RunningBalance_Qty_Vendor").HeaderText = "VSP Closing Qty"

            'gv.Columns("RunningClosing_Value_Vendor").IsVisible = True
            'gv.Columns("RunningClosing_Value_Vendor").Width = 100
            'gv.Columns("RunningClosing_Value_Vendor").HeaderText = "VSP Closing Value"

        End If





        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub FormatGriddrill()
        'Added by preeti agsinst ticket no[KDI/07/06/18-000348]

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True

        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        
        Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
               
        Dim item3 As New GridViewSummaryItem("Return_Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                
          

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        If cboType.SelectedIndex = -1 And rbtnSummary.IsChecked = True Then
            DrillDown()
        ElseIf (cboType.SelectedIndex = 0 And rbtnSummary.IsChecked = True) Then
            If gv.Rows.Count > 0 AndAlso gv.Columns.Contains("Document No") Then
                Dim strTransType As String = clsCommon.myCstr(gv.CurrentRow.Cells("Document No").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, strTransType)
            End If
        ElseIf rbtnDetail.IsChecked = True Then
            If gv.Rows.Count > 0 AndAlso gv.Columns.Contains("Doc_No") Then
                Dim strTransType As String = clsCommon.myCstr(gv.CurrentRow.Cells("Doc_No").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, strTransType)
            End If
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub DrillDown()
        Try
            If clsCommon.CompairString(cboType.SelectedIndex, -1) = CompairStringResult.Equal Then
                If Not arrBack.Contains("Summary") Then
                    arrBack.Add("Summary")
                End If
                cboType.SelectedIndex = 0
                strVSP = New ArrayList()
                strVSP = TxtMultiVendor.arrValueMember
                If chkCurrentStock.Checked = True Then
                    Dim tmp As New ArrayList()
                    tmp.Add(clsCommon.myCstr(gv.CurrentRow.Cells("VSP_Code").Value))
                    TxtMultiVendor.arrValueMember = tmp
                End If


                strItem = New ArrayList()
                strItem = TxtMultiItem.arrValueMember
                Dim tmp1 As New ArrayList()
                tmp1.Add(clsCommon.myCstr(gv.CurrentRow.Cells("Item_Code").Value))
                TxtMultiItem.arrValueMember = tmp1

                strMCC = New ArrayList()
                strMCC = TxtMultiMCC.arrValueMember
                Dim tmp2 As New ArrayList()
                tmp2.Add(clsCommon.myCstr(gv.CurrentRow.Cells("MCC").Value))
                TxtMultiMCC.arrValueMember = tmp2
                FlagDrillDownNoData = False
                Load_Report()
                If FlagDrillDownNoData = True Then
                    cboType.SelectedIndex = -1
                    FlagDrillDownNoData = False
                End If
            End If
            PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If cboType.SelectedIndex = 0 Then
                arrBack.Remove("Summary")
                TxtMultiItem.arrValueMember = strItem
                TxtMultiMCC.arrValueMember = strMCC
                TxtMultiVendor.arrValueMember = strVSP
                cboType.SelectedIndex = -1
                Load_Report()
            Else
                RadPageView1.SelectedPage = RadPageViewPage1
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub TxtMultiMCC__My_Click(sender As Object, e As EventArgs) Handles TxtMultiMCC._My_Click
        Dim qry As String = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER"
        TxtMultiMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeRGP", qry, "Code", "Name", TxtMultiMCC.arrValueMember, TxtMultiMCC.arrDispalyMember)
    End Sub
    Private Sub TxtMultiVendor__My_Click(sender As Object, e As EventArgs) Handles TxtMultiVendor._My_Click
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' And Status='N' "
        TxtMultiVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeRGP", qry, "Code", "Name", TxtMultiVendor.arrValueMember, TxtMultiVendor.arrDispalyMember)
    End Sub
    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click
        Dim qry As String = "select Item_Code as [Code] ,Item_Desc as [Name] from TSPL_ITEM_MASTER  where Item_Type ='A'"
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeRGP", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub

    Private Sub lblSaveLayout_Click(sender As Object, e As EventArgs) Handles lblSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub lblDeleteLayout_Click(sender As Object, e As EventArgs) Handles lblDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub chkAssetIssueWithPurchase_CheckedChanged(sender As Object, e As EventArgs) Handles chkAssetIssueWithPurchase.CheckedChanged
        If chkAssetIssueWithPurchase.Checked = True Then
            lblVSP.Visible = False
            TxtMultiVendor.Visible = False
            rbtnDetail.IsChecked = True
        Else
            lblVSP.Visible = True
            TxtMultiVendor.Visible = True
        End If
    End Sub

    Private Sub ChkCurrentStock_CheckedChanged(sender As Object, e As EventArgs) Handles chkCurrentStock.CheckedChanged
        If chkCurrentStock.Checked = True Then
            'rbtnDetail.IsChecked = True
            'chkAssetIssueWithPurchase.Checked = True
            lblVSP.Visible = True
            TxtMultiVendor.Visible = True
            RadGroupBox4.Enabled = False
        Else
            lblVSP.Visible = False
            TxtMultiVendor.Visible = False
            RadGroupBox4.Enabled = True
        End If
    End Sub

    Private Sub rbtnSummary_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnSummary.CheckStateChanged
        If rbtnSummary.IsChecked = True Then
            chkAssetIssueWithPurchase.Checked = False
        End If
    End Sub

End Class

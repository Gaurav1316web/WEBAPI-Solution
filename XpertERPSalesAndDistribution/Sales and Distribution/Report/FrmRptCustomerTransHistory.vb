Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.IO

Public Class FrmRptCustomerTransHistory
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim dtMain As DataTable = Nothing
    Dim strQry As String = Nothing
    Dim FormType As String = Nothing
    Private _strProgramCode As String


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub FunReset()
        rbtnDocWise.Checked = True
        ChkSummary.CheckState = False
        PnlSumm.Enabled = ChkSummary.Checked
        RadGroupBox1.Enabled = True
        TxtMultiCustomerCategory.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Sub Print()
        Dim IsPDCCheque As String = ""
        Dim CompanyAdd As String = ""
        Dim Comp_Name As String = ""
        Dim childvendrcode As String = ""
        Try
            Dim arr As New ArrayList
            arr.Add("TDS")
            arr.Add("I")
            arr.Add("C")
            arr.Add("D")
            arr.Add("AV")
            arr.Add("OA")
            arr.Add("PY")
            arr.Add("MI")
            arr.Add("RV")
            arr.Add("Customer")
            arr.Add("RC")
            arr.Add("PAE")

            Dim qry As String
            Dim strFromDate As String = clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
            Dim runDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy hh:mm tt")
            Comp_Name = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count = 1 Then
                qry = "select top(1) Add1+' '+Add2+' '+Add3  from TSPL_LOCATION_MASTER  where Location_Type ='Physical' and Loc_Segment_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                CompanyAdd = clsDBFuncationality.getSingleValue(qry)
            Else
                qry = "select top(1) Add1+' '+Add2+' '+Add3  from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
                CompanyAdd = clsDBFuncationality.getSingleValue(qry)
            End If


            Dim strFIlterCheck As String = ""
            Dim strCheckForSumm As String = ""
            Dim strVenFilterPDC As String = ""
            If txCustGroup.arrValueMember IsNot Nothing AndAlso txCustGroup.arrValueMember.Count > 0 Then
                strFIlterCheck += " and XXX.Cust_Group_Code in (" + clsCommon.GetMulcallString(txCustGroup.arrValueMember) + ")  " '----------changed here customer is used changed to vendor
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                strFIlterCheck += " and (Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                strFIlterCheck += ")"
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strFIlterCheck += " and [Loc Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                    strFIlterCheck += " and [Loc Code] in (" + objCommonVar.strCurrUserLocationsSegment + ")  "
                End If
            End If

            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Account  in (" + clsCommon.GetMulcallString(txtAccountSet.arrValueMember) + ")  "
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If


            Dim DocNoPart As String = String.Empty
            Dim DocNo As String = String.Empty
            Dim CustCode As String = String.Empty

            Dim DocQry As String = String.Empty
            If ChkSummary.Checked = True Then

            End If

            DocQry = "With CTETemp as "
            ''richa ERO/19/11/19-001124 add sale no and sale return no 

            If ChkSummary.Checked = False Then
                DocQry += "  ( Select '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + Comp_Name + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, " + Environment.NewLine & _
                          " ROW_NUMBER() over (PARTITION By DocNo ORDER By CONVERT(DATE,RefDocDate,103)) AS RowNo, CustCode As [CustomerCode],CustName AS [Customer Name] " + Environment.NewLine & _
                          " ,DocNo As [DocumentNo],DocType AS [Doc Type],DocTypeCode AS [Doc Type Code],xxx.Cust_Group_Code AS [Cust Group Code],xxx.Route_No AS [Route No] " + Environment.NewLine & _
                          " ,xxx.Zone_Code AS [Zone Code],[Order Number] AS [Order Number],case when  CONVERT(VARCHAR,[Due Date],103)='01/01/1900' then null else CONVERT(VARCHAR,[Due Date],103) end AS [Due Date] " + Environment.NewLine & _
                          " ,[Against Sale No] AS [Against Sale No],[Against Sale Return No] As [Against Sale Return No],[Against MCC Material Sale Return] AS [Against MCC Material Sale Return] " + Environment.NewLine & _
                          " ,[AgainstScrap] AS [AgainstScrap],[Against VCGL] AS [Against VCGL],[Description] AS [Description],[Source Doc No] ,[Remarks] As [Remarks] " + Environment.NewLine & _
                          " ,[Child Cust Code],CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END AS MainCustCode,CASE WHEN ISNULL([Child Cust Name] " + Environment.NewLine & _
                          " ,'')<>'' THEN [Child Cust Name]  ELSE CustName  END AS MainCustName,[Loc Code] AS  [Loc Code],[Loc Desp] AS  [Loc Desp] " + Environment.NewLine & _
                          " ,CONVERT(VARCHAR,DocDate,103) AS [Document Date],RefDocNo AS [Ref Doc No],CONVERT (VARCHAR,RefDocDate,103) AS [Ref Doc Date],SubDocType AS [Sub Doc Type] " + Environment.NewLine & _
                          " ,DrAmt AS DrAmt,CrAmt ,CASE WHEN CrAmt =0 THEN DrAmt ELSE CrAmt * -1 END AS [Trans Amt] ,BalAmt " + Environment.NewLine & _
                          " ,CASE WHEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')<>''  THEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'') ELSE CustCode END AS ParentCode "
            Else
                If rbtnDocWise.Checked = True Then
                    DocNoPart = " DocNo "
                    DocNo = " RTRIM(DocNo) "
                    CustCode = " MAX(CustCode) "
                ElseIf rbtnCustWise.Checked = True Then
                    DocNoPart = " MAX(DocNo) "
                    DocNo = " MAX(RTRIM(DocNo)) "
                    CustCode = " CustCode "
                End If
                DocQry += "  ( Select '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + Comp_Name + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, " + Environment.NewLine & _
                            " ROW_NUMBER() over (PARTITION By " & DocNoPart & " ORDER By MAX(RefDocDate)) AS RowNo, " & CustCode & " AS [CustomerCode],MAX(CustName) AS [Customer Name]," + Environment.NewLine & _
                            " " & DocNo & " AS [DocumentNo] ,MAX(DocType)  As [Doc Type],MAX(DocTypeCode) AS [Doc Type Code],MAX(xxx.Cust_Group_Code ) AS [Cust Group Code],MAX(xxx.Route_No) AS [Route No] " + Environment.NewLine & _
                            " ,MAX(xxx.Zone_Code) AS [Zone Code],MAX([Order Number]) AS [Order Number], " + Environment.NewLine & _
                            " case when  CONVERT(VARCHAR,MAX([Due Date]),103)='01/01/1900' then null else CONVERT(VARCHAR,MAX([Due Date]),103) end AS [Due Date], " + Environment.NewLine & _
                            " MAX([Against Sale No]) AS [Against Sale No], " + Environment.NewLine & _
                            " MAX([Against Sale Return No]) As [Against Sale Return No],MAX([Against MCC Material Sale Return]) AS [Against MCC Material Sale Return], " + Environment.NewLine & _
                            " MAX(AgainstScrap) AS [AgainstScrap],MAX([Against VCGL]) AS [Against VCGL], " + Environment.NewLine & _
                            " MAX(XXX.Description) AS [Description],MAX(Remarks) As [Remarks],MAX([Child Cust Code]) As [Child Cust Code], " + Environment.NewLine & _
                            " CASE WHEN ISNULL(MAX([Child Cust Code]),'')<>'' THEN MAX([Child Cust Code]) ELSE MAX(CustCode) END AS MainCustCode, " + Environment.NewLine & _
                            " CASE WHEN ISNULL(MAX([Child Cust Name]),'')<>'' THEN MAX([Child Cust Name]) ELSE MAX(CustName) END AS MainCustName, " + Environment.NewLine & _
                            " MAX([Source Doc No]) AS [Source Doc No] ,MAX([Loc Code]) AS  [Loc Code],MAX([Loc Desp]) AS  [Loc Desp], " + Environment.NewLine & _
                            " CONVERT(VARCHAR,MAX(DOCDATE),103) AS [Document Date],MAX(RefDocNo) AS [Ref Doc No],CONVERT (VARCHAR,MAX(RefDocDate),103) AS [Ref Doc Date], " + Environment.NewLine & _
                            " MAX(SubDocType) AS [Sub Doc Type],SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,CASE WHEN SUM(CrAmt) =0 THEN SUM(DrAmt) * -1 ELSE SUM(CrAmt) END AS [Trans Amt], " + Environment.NewLine & _
                            " MAX(BalAmt) AS BalAmt,CASE WHEN ISNULL(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No  ),'') <> '' THEN ISNULL(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No ),'') ELSE MAX(CustCode) END AS ParentCode   "
            End If
            ''richa ERO/26/12/19-001165 , ERO/21/07/20-001291
            DocQry += "  From " + Environment.NewLine &
            " ( " + Environment.NewLine &
            " --  AR INVOICE " + Environment.NewLine &
                " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,TSPL_Customer_Invoice_Head.Document_No AS DocNo," + Environment.NewLine &
                " 'AR Invoice' AS DocType,'IN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code," + Environment.NewLine &
                " '' AS [Order Number],NULL AS [Due Date],tspl_customer_invoice_head.Against_Sale_No AS [Against Sale No],TSPL_CUSTOMER_INVOICE_HEAD.Against_sale_return_No As [Against Sale Return No],TSPL_CUSTOMER_INVOICE_HEAD.Against_mcc_material_sale_return AS [Against MCC Material Sale Return],TSPL_CUSTOMER_INVOICE_HEAD.AgainstScrap AS [AgainstScrap],TSPL_CUSTOMER_INVOICE_HEAD.Against_VCGL AS [Against VCGL], " + Environment.NewLine &
                "  TSPL_CUSTOMER_INVOICE_HEAD.Description  AS [Description],TSPL_CUSTOMER_INVOICE_HEAD.Remarks As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code], " + Environment.NewLine &
                " ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, '' AS RefDocNo, Null AS RefDocDate,''  As SubDocType, " + Environment.NewLine &
                " TSPL_Customer_Invoice_Head.Document_Total  AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='I'  " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " --  CREDIT NOTE AGAINST INVOICE " + Environment.NewLine &
                " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName, " + Environment.NewLine &
                "  CASE when TSPL_Customer_Invoice_Head.refdoctype ='CUS_INC_ENT' then TSPL_Customer_Invoice_Head.document_no else ARInvoiceNoofSaleInvoice .Document_No end AS DocNo,'Credit Note' AS DocType, " + Environment.NewLine &
                " 'CN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number], " + Environment.NewLine &
                " NULL AS [Due Date], CASE WHEN ISNULL(TSPL_Customer_Invoice_Head.RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS [Against Sale No],TSPL_CUSTOMER_INVOICE_HEAD.Against_sale_return_No As [Against Sale Return No],TSPL_CUSTOMER_INVOICE_HEAD.Against_mcc_material_sale_return AS [Against MCC Material Sale Return],TSPL_CUSTOMER_INVOICE_HEAD.AgainstScrap AS [AgainstScrap],TSPL_CUSTOMER_INVOICE_HEAD.Against_VCGL AS [Against VCGL], " + Environment.NewLine &
                "  TSPL_CUSTOMER_INVOICE_HEAD.Description  AS [Description],TSPL_CUSTOMER_INVOICE_HEAD.Remarks As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code], " + Environment.NewLine &
                " ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, " + Environment.NewLine &
                " CASE when TSPL_Customer_Invoice_Head.refdoctype ='CUS_INC_ENT' then TSPL_Customer_Invoice_Head.remarks WHEN ISNULL(TSPL_Customer_Invoice_Head.RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(TSPL_Customer_Invoice_Head.RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate, " + Environment.NewLine &
                " 'Credit Note'  As SubDocType,0 AS DrAmt,ISNULL(TSPL_Customer_Invoice_Head.Document_Total ,0) AS CrAmt,0 AS TransAmt,0 AS BalAmt From TSPL_Customer_Invoice_Head  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " left outer join TSPL_Customer_Invoice_Head AS ARInvoiceNoofSaleInvoice on ARInvoiceNoofSaleInvoice.Against_Sale_No  =TSPL_Customer_Invoice_Head.RefDocNo  " + Environment.NewLine &
                " WHERE TSPL_Customer_Invoice_Head.Status=1 " + Environment.NewLine &
                " AND TSPL_Customer_Invoice_Head.Document_Type='C' AND ISNULL(TSPL_Customer_Invoice_Head.RefDocNo,'')<>''  " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " -- CREDIT NOTE SEPEARTED " + Environment.NewLine &
                " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName," + Environment.NewLine &
                " CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Credit Note' AS DocType, " + Environment.NewLine &
                " 'CN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number]," + Environment.NewLine &
                " NULL AS [Due Date],tspl_customer_invoice_head.Against_Sale_No AS [Against Sale No],TSPL_CUSTOMER_INVOICE_HEAD.Against_sale_return_No As [Against Sale Return No],TSPL_CUSTOMER_INVOICE_HEAD.Against_mcc_material_sale_return AS [Against MCC Material Sale Return],TSPL_CUSTOMER_INVOICE_HEAD.AgainstScrap AS [AgainstScrap],TSPL_CUSTOMER_INVOICE_HEAD.Against_VCGL AS [Against VCGL], " + Environment.NewLine &
                "  TSPL_CUSTOMER_INVOICE_HEAD.Description  AS [Description],TSPL_CUSTOMER_INVOICE_HEAD.Remarks As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code], " + Environment.NewLine &
                " ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, " + Environment.NewLine &
                " CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate, " + Environment.NewLine &
                " 'Credit Note'  As SubDocType,0 AS DrAmt,TSPL_Customer_Invoice_Head.Document_Total  AS CrAmt,0 AS TransAmt,0 AS BalAmt From TSPL_Customer_Invoice_Head  " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='C' AND ISNULL(RefDocNo,'')=''  " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " -- DEBIT NOTE SEPERATED " + Environment.NewLine &
               " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName," + Environment.NewLine &
               " CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Debit Note' AS DocType, " + Environment.NewLine &
               " 'DN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number], " + Environment.NewLine &
               " NULL AS [Due Date],tspl_customer_invoice_head.Against_Sale_No AS [Against Sale No],TSPL_CUSTOMER_INVOICE_HEAD.Against_sale_return_No As [Against Sale Return No],TSPL_CUSTOMER_INVOICE_HEAD.Against_mcc_material_sale_return AS [Against MCC Material Sale Return],TSPL_CUSTOMER_INVOICE_HEAD.AgainstScrap AS [AgainstScrap],TSPL_CUSTOMER_INVOICE_HEAD.Against_VCGL AS [Against VCGL], " + Environment.NewLine &
               "  TSPL_CUSTOMER_INVOICE_HEAD.Description  AS [Description],TSPL_CUSTOMER_INVOICE_HEAD.Remarks As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code]," + Environment.NewLine &
               " ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate,'' AS RefDocNo, NULL AS RefDocDate," + Environment.NewLine &
               " ''  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head  " + Environment.NewLine &
               " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine &
               " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
               " WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='D' AND ISNULL(RefDocNo,'')='' " + Environment.NewLine &
               " UNION ALL " + Environment.NewLine &
            " -- DEBIT NOTE AGAINST INVOICE " + Environment.NewLine &
                " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo, " + Environment.NewLine &
                " 'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date], " + Environment.NewLine &
                " tspl_customer_invoice_head.Against_Sale_No AS [Against Sale No],TSPL_CUSTOMER_INVOICE_HEAD.Against_sale_return_No As [Against Sale Return No],TSPL_CUSTOMER_INVOICE_HEAD.Against_mcc_material_sale_return AS [Against MCC Material Sale Return],TSPL_CUSTOMER_INVOICE_HEAD.AgainstScrap AS [AgainstScrap],TSPL_CUSTOMER_INVOICE_HEAD.Against_VCGL AS [Against VCGL], TSPL_CUSTOMER_INVOICE_HEAD.Description  AS [Description],TSPL_CUSTOMER_INVOICE_HEAD.Remarks As [Remarks]," + Environment.NewLine &
                " '' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp], " + Environment.NewLine &
                " TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, TSPL_Customer_Invoice_Head.Document_No AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate," + Environment.NewLine &
                " 'Debit Note'  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='D' AND ISNULL(RefDocNo,'')<>'' " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " -- PAYMENT ENTRIES (APPLY DOCUMENT/RECEIPT)  " + Environment.NewLine &
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_DETAIL.Document_No AS DocNo, " + Environment.NewLine &
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' ELSE NULL END AS DocType, " + Environment.NewLine &
                " ISNULL(TSPL_RECEIPT_HEADER.Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(TSPL_CUSTOMER_MASTER.Zone_Code,'') AS Zone_Code," + Environment.NewLine &
                " '' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date], " + Environment.NewLine &
                " isnull(TSPL_CUSTOMER_iNVOICE_HEAD.Against_Sale_No,'') AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],TSPL_RECEIPT_HEADER.Entry_Desc  AS [Description], " + Environment.NewLine &
                " '' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], " + Environment.NewLine &
                " '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp], " + Environment.NewLine &
                " CONVERT(datetime2 ,TSPL_RECEIPT_DETAIL.Document_Date,101) AS DocDate,TSPL_RECEIPT_HEADER.Receipt_No AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate, " + Environment.NewLine &
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' " + Environment.NewLine &
                " WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' " + Environment.NewLine &
                " WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, 0 as DrAmt, TSPL_RECEIPT_DETAIL.Applied_Amount  AS CrAmt,0 AS TransAmt," + Environment.NewLine &
                " 0 AS BalAmt FROM TSPL_RECEIPT_DETAIL " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No  = TSPL_RECEIPT_DETAIL.Receipt_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_iNVOICE_HEAD ON TSPL_CUSTOMER_iNVOICE_HEAD.dOCUMENT_NO=TSPL_RECEIPT_DETAIL.dOCUMENT_NO " + Environment.NewLine &
                " WHERE TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " -- REFUND " + Environment.NewLine &
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code as CustCode,TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Receipt_No AS DocNo, " + Environment.NewLine &
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' ELSE NULL END AS DocType, " + Environment.NewLine &
                " ISNULL(TSPL_RECEIPT_HEADER.Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(TSPL_CUSTOMER_MASTER.Zone_Code,'') AS Zone_Code," + Environment.NewLine &
                " '' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date], " + Environment.NewLine &
                " '' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],TSPL_RECEIPT_HEADER.Entry_Desc  AS [Description],'' As [Remarks], " + Environment.NewLine &
                " '' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) As [Loc Code] , " + Environment.NewLine &
                " ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate,'' as RefDocNo,NULL AS RefDocDate,'' As SubDocType,TSPL_RECEIPT_HEADER.Receipt_Amount as DrAmt, " + Environment.NewLine &
                " 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM  TSPL_RECEIPT_HEADER " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " -- ADVANCE/ON-ACCOUNT " + Environment.NewLine &
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode,TSPL_RECEIPT_HEADER.Customer_Name AS CustName,Receipt_No AS DocNo," + Environment.NewLine &
                " CASE WHEN Receipt_Type='P' THEN 'Advance' WHEN Receipt_Type='O' THEN 'On Account' ELSE NULL END AS DocType,ISNULL(Receipt_Type,'') AS DocTypeCode," + Environment.NewLine &
                " ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(TSPL_CUSTOMER_MASTER.Zone_Code,'') AS Zone_Code,'' AS [Order Number]," + Environment.NewLine &
                " CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No]," + Environment.NewLine &
                " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],TSPL_RECEIPT_HEADER.Entry_Desc AS [Description],'' As [Remarks]," + Environment.NewLine &
                " '' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) AS [Loc Code]," + Environment.NewLine &
                " ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],Receipt_Date AS DocDate,'' as RefDocNo,NULL AS RefDocDate,''  AS SubDocType, 0  AS DrAmt," + Environment.NewLine &
                " Receipt_Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM  TSPL_RECEIPT_HEADER " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('P','O') AND ISNULL(SecurityDeposit,'')='N' " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " -- APPLY DOCUMENT AGAINST ADVANCE/ON-ACCOUNT " + Environment.NewLine &
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Applied_Receipt  AS DocNo, " + Environment.NewLine &
                " CASE WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'P' THEN 'Advance' WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'O' THEN 'On Account' WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'U' THEN 'UnApplied' END AS DocType, " + Environment.NewLine &
                " (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt) AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code," + Environment.NewLine &
                " ISNULL(Route_No,'') AS Route_No,ISNULL(TSPL_CUSTOMER_MASTER.Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date], " + Environment.NewLine &
                " '' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],TSPL_RECEIPT_HEADER.Entry_Desc  AS [Description],'' As [Remarks], " + Environment.NewLine &
                " ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code ,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], '' AS [Source Doc No]," + Environment.NewLine &
                " reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],CASE WHEN TSPL_RECEIPT_DETAIL.RECEIPT_TYPE IN ('I','C','D') THEN CONVERT(DATE,TSPL_CUSTOMER_iNVOICE_HEAD.DOCUMENT_DATE,103) ELSE TSPL_RECEIPT_HEADER.Receipt_Date END AS DocDate," + Environment.NewLine &
                " TSPL_RECEIPT_HEADER.Receipt_No  AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate," + Environment.NewLine &
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, " + Environment.NewLine &
                " TSPL_RECEIPT_DETAIL.Applied_Amount as DrAmt, 0   AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_RECEIPT_HEADER " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No  = TSPL_RECEIPT_DETAIL.Receipt_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_iNVOICE_HEAD ON TSPL_CUSTOMER_iNVOICE_HEAD.dOCUMENT_NO=TSPL_RECEIPT_DETAIL.dOCUMENT_NO " + Environment.NewLine &
                " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' AND ISNULL(Applied_Receipt,'')<>'' " + Environment.NewLine &
                " UNION ALL" + Environment.NewLine &
           " -- UNAPPLIED ENTRY AGAINST RECEIPT " + Environment.NewLine &
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Receipt_No AS DocNo, " + Environment.NewLine &
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END   AS DocType, " + Environment.NewLine &
                " TSPL_RECEIPT_HEADER.Receipt_Type AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code, " + Environment.NewLine &
                " ISNULL(Route_No,'') AS Route_No,ISNULL(TSPL_CUSTOMER_MASTER.Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date], " + Environment.NewLine &
                " '' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],TSPL_RECEIPT_HEADER.Entry_Desc  AS [Description],'' As [Remarks], " + Environment.NewLine &
                " ISNULL(TSPL_CUSTOMER_MASTER.Cust_Code  ,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], '' AS [Source Doc No]," + Environment.NewLine &
                " reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate," + Environment.NewLine &
                " RHM.Receipt_No  AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate," + Environment.NewLine &
                " CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' WHEN RHM.Receipt_Type='P' THEN 'Advance' WHEN RHM.Receipt_Type='O' THEN 'On Account' WHEN RHM.Receipt_Type='A' THEN 'Apply Document' WHEN RHM.Receipt_Type='U' THEN 'UnApplied' WHEN RHM.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, " + Environment.NewLine &
                " 0 as DrAmt, TSPL_RECEIPT_HEADER.Receipt_Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_RECEIPT_HEADER " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.UnApplied_No = TSPL_RECEIPT_HEADER.Receipt_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('U') " + Environment.NewLine &
                " AND ISNULL(RHM.UnApplied_No,'')<>'' " + Environment.NewLine &
                " UNION ALL" + Environment.NewLine &
            " -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT-BULK) " + Environment.NewLine &
                " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,TSPL_Customer_Invoice_Head.Document_No  AS DocNo, " + Environment.NewLine &
                " 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(TSPL_CUSTOMER_MASTER.Zone_Code,'') AS Zone_Code, " + Environment.NewLine &
                " '' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL]," + Environment.NewLine &
                " '' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name]," + Environment.NewLine &
                " '' AS [Source Doc No],ISNULL (TSPL_INVOICE_MASTER_BULKSALE.Location_Code,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ," + Environment.NewLine &
                " TSPL_Customer_Invoice_Head.Document_Date AS DocDate ,RHM.Receipt_No AS RefDocNo, RHM.Receipt_Date AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType, TSPL_BANK_REVERSE.Amount AS DrAmt, " + Environment.NewLine &
                " 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_INVOICE_MASTER_BULKSALE " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No =TSPL_Customer_Invoice_Head.Document_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.Receipt_No = TSPL_RECEIPT_DETAIL.Receipt_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE .Document_No = RHM.Receipt_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_INVOICE_MASTER_BULKSALE.Location_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('A','R') AND ISNULL(RHM.SecurityDeposit ,'')='N'  " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine &
                " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,RHM.Receipt_No  AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode," + Environment.NewLine &
                " ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(TSPL_CUSTOMER_MASTER.Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No], " + Environment.NewLine &
                " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code]," + Environment.NewLine &
                " '' As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ," + Environment.NewLine &
                " RHM.Receipt_Date AS DocDate ,RHM.Receipt_No AS RefDocNo, RHM.Receipt_Date AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType," + Environment.NewLine &
                " TSPL_BANK_REVERSE.Amount AS  DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_BANK_REVERSE  " + Environment.NewLine &
                " LEFT OUTER JOIN  TSPL_RECEIPT_HEADER RHM ON TSPL_BANK_REVERSE.Document_No = RHM.Receipt_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('P','O') AND ISNULL(RHM.SecurityDeposit ,'')='N'  " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
                "  -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine &
                "  SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,TSPL_RECEIPT_DETAIL.Document_No AS DocNo, 'Bank Reverse' AS DocType," + Environment.NewLine &
                " 'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(TSPL_CUSTOMER_MASTER.Route_No,'') AS Route_No,ISNULL(TSPL_CUSTOMER_MASTER.Zone_Code,'') AS Zone_Code," + Environment.NewLine &
                " ISNULL(TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ,'') AS [Order Number],NULL  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No]," + Environment.NewLine &
                " '' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks], " + Environment.NewLine &
                " ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name],'' AS [Source Doc No], " + Environment.NewLine &
                " Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ," + Environment.NewLine &
                " RHM.Receipt_Date AS DocDate ,TSPL_BANK_REVERSE.Reverse_Code AS RefDocNo, TSPL_BANK_REVERSE.Reversal_Date  AS RefDocDate, " + Environment.NewLine &
                " CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType,  TSPL_BANK_REVERSE.Amount AS  DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine &
                " FROM TSPL_BANK_REVERSE " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.Receipt_No = TSPL_BANK_REVERSE.Document_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No = RHM.Receipt_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = RHM.Cust_Code " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No = TSPL_RECEIPT_DETAIL.Document_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_SD_SHIPMENT_HEAD.Document_Code " + Environment.NewLine &
                " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('A','R') AND ISNULL(RHM.SecurityDeposit ,'')='N'  " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " --  VCGL DATA(FIRST QUERY) " + Environment.NewLine &
                " SELECT TSPL_VCGL_Head.VC_Code AS CustCode, TSPL_VCGL_Head.VC_Name AS CustName, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode," + Environment.NewLine &
                " ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No]," + Environment.NewLine &
                " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code]," + Environment.NewLine &
                " '' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp], " + Environment.NewLine &
                " CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) AS DocDate,'' AS RefDocNo,Null AS RefDocDate,''  As SubDocType," + Environment.NewLine &
                " CASE WHEN  TSPL_VCGL_Head.Amount_Type='Cr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END  AS DrAmt ,CASE WHEN  TSPL_VCGL_Head.Amount_Type='Dr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END AS CrAmt," + Environment.NewLine &
                " 0 AS TransAmt,0 AS BalAmt  FROM TSPL_VCGL_Head " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No " + Environment.NewLine &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code= TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
                " WHERE TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " + Environment.NewLine &
                " UNION ALL " + Environment.NewLine &
            " -- VCGL DATA(SECOND QUERY) " + Environment.NewLine &
               " SELECT TSPL_VCGL_Detail.VCGL_Code AS CustCode, TSPL_VCGL_Detail.VCGL_Name AS CustName, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode," + Environment.NewLine &
               " ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No]," + Environment.NewLine &
               " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks]," + Environment.NewLine &
               " '' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp]," + Environment.NewLine &
               " CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) AS DocDate, '' AS RefDocNo,NULL AS RefDocDate,''  As SubDocType, TSPL_VCGL_Detail.Dr_Amount AS DrAmt , " + Environment.NewLine &
               " TSPL_VCGL_Detail.Cr_Amount AS CrAmt ,0 AS TransAmt,0 AS BalAmt FROM TSPL_VCGL_Detail " + Environment.NewLine &
               " LEFT OUTER JOIN  TSPL_VCGL_Head ON  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No " + Environment.NewLine &
               " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment " + Environment.NewLine &
               " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No " + Environment.NewLine &
               " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code= TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine &
               " WHERE TSPL_VCGL_Head.Status=1 AND  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " + Environment.NewLine &
            " )xxx " + Environment.NewLine &
           " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =xxx.CustCode " + Environment.NewLine &
           " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = XXX.Cust_Group_Code " + Environment.NewLine &
            " WHERE 1=1 " & strFIlterCheck & " AND CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' "
            If ChkSummary.Checked = True Then
                If rbtnDocWise.Checked = True Then
                    DocQry += " GROUP BY DocNo "
                Else
                    DocQry += " GROUP BY CustCode "
                End If
            End If
            DocQry += " )  "
            If ChkSummary.Checked = True Then
                DocQry += " SELECT *, DrAmt-CrAmt AS Balance FROM CTETemp "
            Else
                ''richa 2 May,2019 order by according to document date 
                DocQry += " SELECT  *, CASE WHEN RowNoNew=1 Then CumAmt END AS Balance FROM ( " + Environment.NewLine & _
                          " SELECT *, SUM([Trans Amt]) Over (Partition By DocumentNo Order By RowNo) as CumAmt, ROW_NUMBER() over (PARTITION By DocumentNo ORDER By RowNo Desc) AS RowNoNew " + Environment.NewLine & _
                          " FROM CTETemp ) ZZZ Order By convert(date,[Document Date],103),DocumentNo, ZZZ.RowNo "



            End If

       
            dtMain = clsDBFuncationality.GetDataTable(DocQry)

            If dtMain.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("Data not found")
                gv.DataSource = Nothing
                Exit Sub
            End If
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtMain
            SetGridFormat(False)
            ReStoreGridLayout()
            If blnRefresh = False Then
                If ChkSummary.Checked = True Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.Purchase, dtMain, "CustomerTransListLandScapeSummary", "Customer Transaction List (Summary)")
                Else
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.Purchase, dtMain, "CustomerTransListLandScape", "Customer Transaction List")
                End If
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox1.Enabled = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

    Sub SetGridFormat(ByVal IsFromDrillDown As Boolean)
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.GroupDescriptors.Clear()
        gv.ShowGroupPanel = False
        Dim summaryRowItem As New GridViewSummaryRowItem()

        gv.AllowAddNewRow = False

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("CustomerCode").IsVisible = True
        gv.Columns("CustomerCode").Width = 50
        gv.Columns("CustomerCode").HeaderText = "Customer Code"

        gv.Columns("Customer Name").IsVisible = True
        gv.Columns("Customer Name").Width = 100
        gv.Columns("Customer Name").HeaderText = "Customer Name"

        If (rbtnCustWise.Checked = False AndAlso ChkSummary.Checked = True) OrElse (ChkSummary.Checked = False AndAlso rbtnDocWise.Checked = True) Then
            gv.Columns("DocumentNo").IsVisible = True
            gv.Columns("DocumentNo").Width = 100
            gv.Columns("DocumentNo").HeaderText = "DocumentNo"

            gv.Columns("Doc Type").IsVisible = True
            gv.Columns("Doc Type").Width = 100
            gv.Columns("Doc Type").HeaderText = "Document Type"

            gv.Columns("Due Date").IsVisible = True
            gv.Columns("Due Date").Width = 100
            gv.Columns("Due Date").HeaderText = "Due Date"
            gv.Columns("Due Date").FormatString = "{0:d}"

            gv.Columns("Doc Type Code").IsVisible = True
            gv.Columns("Doc Type Code").Width = 250
            gv.Columns("Doc Type Code").HeaderText = "Doc Type Code"

            gv.Columns("AgainstScrap").IsVisible = True
            gv.Columns("AgainstScrap").Width = 100
            gv.Columns("AgainstScrap").HeaderText = "Jobwork Invoice No"

            gv.Columns("Child Cust Code").IsVisible = True
            gv.Columns("Child Cust Code").Width = 100
            gv.Columns("Child Cust Code").HeaderText = "Child Cust Code"

            gv.Columns("Cust Group Code").IsVisible = True
            gv.Columns("Cust Group Code").Width = 100
            gv.Columns("Cust Group Code").HeaderText = "Cust Group Code"

            gv.Columns("Ref Doc No").IsVisible = True
            gv.Columns("Ref Doc No").Width = 60

            gv.Columns("Ref Doc Date").IsVisible = True
            gv.Columns("Ref Doc Date").HeaderText = "Ref Doc Date"
            gv.Columns("Ref Doc Date").Width = 120

            gv.Columns("Sub Doc Type").IsVisible = True
            gv.Columns("Sub Doc Type").Width = 100
            gv.Columns("Sub Doc Type").HeaderText = "Sub Doc Type"


            gv.Columns("ParentCode").IsVisible = True
            gv.Columns("ParentCode").Width = 100
            gv.Columns("ParentCode").HeaderText = "ParentCode"

            gv.Columns("Loc Code").IsVisible = True
            gv.Columns("Loc Code").Width = 100
            gv.Columns("Loc Code").HeaderText = "Loc Code"

            gv.Columns("Loc Desp").IsVisible = True
            gv.Columns("Loc Desp").Width = 100
            gv.Columns("Loc Desp").HeaderText = "Loc Desp"

            gv.Columns("DrAmt").IsVisible = True
            gv.Columns("DrAmt").Width = 100
            gv.Columns("DrAmt").HeaderText = "DrAmt"

            gv.Columns("CrAmt").IsVisible = True
            gv.Columns("CrAmt").Width = 100
            gv.Columns("CrAmt").HeaderText = "CrAmt"

        End If

        gv.Columns("Balance").IsVisible = True
        gv.Columns("Balance").Width = 100
        gv.Columns("Balance").HeaderText = "Balance Amount"

        Dim ttllAmt As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ttllAmt)
        ttllAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ttllAmt)
        ttllAmt = New GridViewSummaryItem("Balance", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ttllAmt)
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        If ChkSummary.Checked = True Then
         
        Else
            Dim summaryItem As New GridViewSummaryItem()
            summaryItem.FormatString = "{0:F2}"
            summaryItem.Name = "CumAmt"
            summaryItem.AggregateExpression = "SUM([Trans Amt])"
            summaryRowItem.Add(summaryItem)
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If
        'gv.BestFitColumns()

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.FrmRptCustomerTransHistory & "'"))
            If txCustGroup.arrValueMember IsNot Nothing AndAlso txCustGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txCustGroup.arrDispalyMember))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrValueMember))
            End If
            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                arrHeader.Add("Account Set : " + clsCommon.GetMulcallStringWithComma(txtAccountSet.arrDispalyMember))
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If gv.Rows.Count <= 0 Then
                Throw New Exception("No data found for Export.")
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Customer Transaction Report", gv, arrHeader, Me.Text)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Customer Transaction Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        FunReset()
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If ChkSummary.IsChecked = True Then
            VarID += "_SU"
        End If
        gv.VarID = VarID
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        GetReportGridID()
        blnRefresh = True
        PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnCustWise.Checked = True, "C", "D")
        TemplateGridview = gv
        Print()
        blnRefresh = False
    End Sub

    Private Sub FrmRptCustomerTransList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag AndAlso btnPrint.Enabled Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso MyBase.isModifyFlag AndAlso BtnClose.Enabled Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag AndAlso btnRefresh.Enabled Then
            FunReset()
        End If
    End Sub

    Private Sub FrmRptCustomerTransList_Load(sender As Object, e As EventArgs) Handles Me.Load
        'dtpFromdate.Value = "01/Apr/2015"
        dtpFromdate.Value = New DateTime(DateTime.Today.Year - 1, 4, 1)
        dtptodate.Value = clsCommon.GETSERVERDATE()

        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset all Parameters")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

        ChkSummary.Checked = False
        PnlSumm.Enabled = ChkSummary.Checked
    End Sub

    Private Sub txCustGroup__My_Click(sender As Object, e As EventArgs) Handles txCustGroup._My_Click
        strQry = "select  Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER order by Cust_Group_Code "
        txCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGrpTran", strQry, "Code", "Name", txCustGroup.arrValueMember, txCustGroup.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as Name,TSPL_CUSTOMER_MASTER.Parent_Customer_No as [Parent Code],P1.Customer_Name as [Parent Name] FROM TSPL_CUSTOMER_MASTER Left Outer Join TSPL_CUSTOMER_MASTER P1 on TSPL_CUSTOMER_MASTER.Parent_Customer_No =P1.Cust_Code where 1=1 "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            strQry += " and TSPL_CUSTOMER_MASTER.cust_code in (" + objCommonVar.strCurrUserCustomers + ") "
        End If
        strQry += " order by TSPL_CUSTOMER_MASTER.Cust_Code "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerTrans", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtAccountSet__My_Click(sender As Object, e As EventArgs) Handles txtAccountSet._My_Click
        strQry = "select Cust_Account as Code, Cust_Acct_Desc as Description from TSPL_CUSTOMER_ACCOUNT_SET"
        txtAccountSet.arrValueMember = clsCommon.ShowMultipleSelectForm("AccSetTrans", strQry, "Code", "Description", txtAccountSet.arrValueMember, txtAccountSet.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        strQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        strQry += " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationTrans", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub


    Private Sub ChkSummary_CheckStateChanged(sender As Object, e As EventArgs) Handles ChkSummary.CheckStateChanged
        PnlSumm.Enabled = ChkSummary.Checked
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCatMSel4", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub
End Class


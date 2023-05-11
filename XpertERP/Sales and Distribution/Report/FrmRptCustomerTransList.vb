'BM00000006224
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common

Public Class FrmRptCustomerTransList
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim dtMain As DataTable = Nothing
    Dim strQry As String = Nothing
    Dim FormType As String = Nothing
    Private _strProgramCode As String


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmRptCustomerTransList)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnQuickExport.Visible = MyBase.isExport
    End Sub
    Sub FunReset()
        rbtnDocWise.Checked = True
        ChkSummary.CheckState = False
        PnlSumm.Enabled = ChkSummary.Checked
        RadGroupBox1.Enabled = True
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
            End If

            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Account  in (" + clsCommon.GetMulcallString(txtAccountSet.arrValueMember) + ")  "
            End If

            '----------------------------------------------------- BM00000007757
            Dim DocNoPart As String = String.Empty
            Dim DocNo As String = String.Empty
            Dim CustCode As String = String.Empty

            Dim DocQry As String = String.Empty
            If ChkSummary.Checked = True Then

            End If

            DocQry = "With CTETemp as "


            If ChkSummary.Checked = False Then
                DocQry += "  ( Select '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + Comp_Name + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, " + Environment.NewLine & _
                          " ROW_NUMBER() over (PARTITION By DocNo ORDER By CONVERT(DATE,RefDocDate,103)) AS RowNo, CustCode As [CustomerCode],CustName AS [Customer Name] " + Environment.NewLine & _
                          " ,DocNo As [DocumentNo],DocType AS [Doc Type],DocTypeCode AS [Doc Type Code],xxx.Cust_Group_Code AS [Cust Group Code],xxx.Route_No AS [Route No] " + Environment.NewLine & _
                          " ,xxx.Zone_Code AS [Zone Code],[Order Number] AS [Order Number],case when  CONVERT(VARCHAR,[Due Date],103)='01/01/1900' then null else CONVERT(VARCHAR,[Due Date],103) end AS [Due Date] " + Environment.NewLine & _
                          " ,[Against Sale No] AS [Against Sale No],[Against Sale Return No] As [Against Sale Return No],[Against MCC Material Sale Return] AS [Against MCC Material Sale Return] " + Environment.NewLine & _
                          " ,[AgainstScrap] AS [AgainstScrap],[Against VCGL] AS [Against VCGL],[Description] AS [Description],[Source Doc No] ,[Remarks] As [Remarks] " + Environment.NewLine & _
                          " ,[Child Cust Code],CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END AS MainCustCode,CASE WHEN ISNULL([Child Cust Name] " + Environment.NewLine & _
                          " ,'')<>'' THEN [Child Cust Name]  ELSE CustName  END AS MainCustName,[Loc Code] AS  [Loc Code],[Loc Desp] AS  [Loc Desp] " + Environment.NewLine & _
                          " ,CONVERT(VARCHAR,DocDate,103) AS [Document Date],RefDocNo AS [Ref Doc No],CONVERT (VARCHAR,RefDocDate,101) AS [Ref Doc Date],SubDocType AS [Sub Doc Type] " + Environment.NewLine & _
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
                            " CONVERT(VARCHAR,MAX(DOCDATE),103) AS [Document Date],MAX(RefDocNo) AS [Ref Doc No],CONVERT (VARCHAR,MAX(RefDocDate),101) AS [Ref Doc Date], " + Environment.NewLine & _
                            " MAX(SubDocType) AS [Sub Doc Type],SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,CASE WHEN SUM(CrAmt) =0 THEN SUM(DrAmt) * -1 ELSE SUM(CrAmt) END AS [Trans Amt], " + Environment.NewLine & _
                            " MAX(BalAmt) AS BalAmt,CASE WHEN ISNULL(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No  ),'') <> '' THEN ISNULL(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No ),'') ELSE MAX(CustCode) END AS ParentCode   "
            End If
            DocQry += "  From " + Environment.NewLine & _
            " ( " + Environment.NewLine & _
            " --  AR INVOICE " + Environment.NewLine & _
                " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,TSPL_Customer_Invoice_Head.Document_No AS DocNo," + Environment.NewLine & _
                " 'AR Invoice' AS DocType,'IN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code," + Environment.NewLine & _
                " '' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap], " + Environment.NewLine & _
                " '' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code], " + Environment.NewLine & _
                " ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, '' AS RefDocNo, Null AS RefDocDate,''  As SubDocType, " + Environment.NewLine & _
                " TSPL_Customer_Invoice_Head.Document_Total  AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head  " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='I'  " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " --  CREDIT NOTE AGAINST INVOICE " + Environment.NewLine & _
                " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName, " + Environment.NewLine & _
                " CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Credit Note' AS DocType, " + Environment.NewLine & _
                " 'CN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number], " + Environment.NewLine & _
                " NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL], " + Environment.NewLine & _
                " '' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code], " + Environment.NewLine & _
                " ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, " + Environment.NewLine & _
                " CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate, " + Environment.NewLine & _
                " 'Credit Note'  As SubDocType,0 AS DrAmt,ISNULL(TSPL_Customer_Invoice_Head.Document_Total ,0) AS CrAmt,0 AS TransAmt,0 AS BalAmt From TSPL_Customer_Invoice_Head  " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_Customer_Invoice_Head.Status=1 " + Environment.NewLine & _
                " AND TSPL_Customer_Invoice_Head.Document_Type='C' AND ISNULL(RefDocNo,'')<>''  " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " -- CREDIT NOTE SEPEARTED " + Environment.NewLine & _
                " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName," + Environment.NewLine & _
                " CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Credit Note' AS DocType, " + Environment.NewLine & _
                " 'CN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number]," + Environment.NewLine & _
                " NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL], " + Environment.NewLine & _
                " '' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code], " + Environment.NewLine & _
                " ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, " + Environment.NewLine & _
                " CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate, " + Environment.NewLine & _
                " 'Credit Note'  As SubDocType,0 AS DrAmt,TSPL_Customer_Invoice_Head.Document_Total  AS CrAmt,0 AS TransAmt,0 AS BalAmt From TSPL_Customer_Invoice_Head  " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='C' AND ISNULL(RefDocNo,'')=''  " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " -- DEBIT NOTE SEPERATED " + Environment.NewLine & _
               " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName," + Environment.NewLine & _
               " CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Debit Note' AS DocType, " + Environment.NewLine & _
               " 'DN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number], " + Environment.NewLine & _
               " NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL], " + Environment.NewLine & _
               " '' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code]," + Environment.NewLine & _
               " ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate,'' AS RefDocNo, NULL AS RefDocDate," + Environment.NewLine & _
               " ''  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head  " + Environment.NewLine & _
               " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine & _
               " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
               " WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='D' AND ISNULL(RefDocNo,'')='' " + Environment.NewLine & _
               " UNION ALL " + Environment.NewLine & _
            " -- DEBIT NOTE AGAINST INVOICE " + Environment.NewLine & _
                " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo, " + Environment.NewLine & _
                " 'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No], " + Environment.NewLine & _
                " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks]," + Environment.NewLine & _
                " '' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp], " + Environment.NewLine & _
                " TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, TSPL_Customer_Invoice_Head.Document_No AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate," + Environment.NewLine & _
                " 'Debit Note'  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='D' AND ISNULL(RefDocNo,'')<>'' " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " -- PAYMENT ENTRIES (APPLY DOCUMENT/RECEIPT)  " + Environment.NewLine & _
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_DETAIL.Document_No AS DocNo, " + Environment.NewLine & _
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' ELSE NULL END AS DocType, " + Environment.NewLine & _
                " ISNULL(TSPL_RECEIPT_HEADER.Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code," + Environment.NewLine & _
                " '' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date], " + Environment.NewLine & _
                " '' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description], " + Environment.NewLine & _
                " '' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], " + Environment.NewLine & _
                " '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp], " + Environment.NewLine & _
                " CONVERT(datetime2 ,TSPL_RECEIPT_DETAIL.Document_Date,101) AS DocDate,TSPL_RECEIPT_HEADER.Receipt_No AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate, " + Environment.NewLine & _
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' " + Environment.NewLine & _
                " WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' " + Environment.NewLine & _
                " WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, 0 as DrAmt, TSPL_RECEIPT_DETAIL.Applied_Amount  AS CrAmt,0 AS TransAmt," + Environment.NewLine & _
                " 0 AS BalAmt FROM TSPL_RECEIPT_DETAIL " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No  = TSPL_RECEIPT_DETAIL.Receipt_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') AND ISNULL(SecurityDeposit,'')='N' " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " -- REFUND " + Environment.NewLine & _
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code as CustCode,TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Receipt_No AS DocNo, " + Environment.NewLine & _
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' ELSE NULL END AS DocType, " + Environment.NewLine & _
                " ISNULL(TSPL_RECEIPT_HEADER.Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code," + Environment.NewLine & _
                " '' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date], " + Environment.NewLine & _
                " '' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks], " + Environment.NewLine & _
                " '' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) As [Loc Code] , " + Environment.NewLine & _
                " ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate,'' as RefDocNo,NULL AS RefDocDate,'' As SubDocType,TSPL_RECEIPT_HEADER.Receipt_Amount as DrAmt, " + Environment.NewLine & _
                " 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM  TSPL_RECEIPT_HEADER " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " -- ADVANCE/ON-ACCOUNT " + Environment.NewLine & _
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode,TSPL_RECEIPT_HEADER.Customer_Name AS CustName,Receipt_No AS DocNo," + Environment.NewLine & _
                " CASE WHEN Receipt_Type='P' THEN 'Advance' WHEN Receipt_Type='O' THEN 'On Account' ELSE NULL END AS DocType,ISNULL(Receipt_Type,'') AS DocTypeCode," + Environment.NewLine & _
                " ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number]," + Environment.NewLine & _
                " CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No]," + Environment.NewLine & _
                " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks]," + Environment.NewLine & _
                " '' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) AS [Loc Code]," + Environment.NewLine & _
                " ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],Receipt_Date AS DocDate,'' as RefDocNo,NULL AS RefDocDate,''  AS SubDocType, 0  AS DrAmt," + Environment.NewLine & _
                " Receipt_Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM  TSPL_RECEIPT_HEADER " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('P','O') AND ISNULL(SecurityDeposit,'')='N' " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " -- APPLY DOCUMENT AGAINST ADVANCE/ON-ACCOUNT " + Environment.NewLine & _
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Applied_Receipt  AS DocNo, " + Environment.NewLine & _
                " CASE WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'P' THEN 'Advance' WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'O' THEN 'On Account' WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'U' THEN 'UnApplied' END AS DocType, " + Environment.NewLine & _
                " (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt) AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code," + Environment.NewLine & _
                " ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date], " + Environment.NewLine & _
                " '' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks], " + Environment.NewLine & _
                " ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code ,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], '' AS [Source Doc No]," + Environment.NewLine & _
                " reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate," + Environment.NewLine & _
                " TSPL_RECEIPT_HEADER.Receipt_No  AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate," + Environment.NewLine & _
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, " + Environment.NewLine & _
                " TSPL_RECEIPT_DETAIL.Applied_Amount as DrAmt, 0   AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_RECEIPT_HEADER " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No  = TSPL_RECEIPT_DETAIL.Receipt_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') AND ISNULL(SecurityDeposit,'')='N' AND ISNULL(Applied_Receipt,'')<>'' " + Environment.NewLine & _
                " UNION ALL" + Environment.NewLine & _
           " -- UNAPPLIED ENTRY AGAINST RECEIPT " + Environment.NewLine & _
                " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Receipt_No AS DocNo, " + Environment.NewLine & _
                " CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END   AS DocType, " + Environment.NewLine & _
                " TSPL_RECEIPT_HEADER.Receipt_Type AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code, " + Environment.NewLine & _
                " ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date], " + Environment.NewLine & _
                " '' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks], " + Environment.NewLine & _
                " ISNULL(TSPL_CUSTOMER_MASTER.Cust_Code  ,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], '' AS [Source Doc No]," + Environment.NewLine & _
                " reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate," + Environment.NewLine & _
                " RHM.Receipt_No  AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate," + Environment.NewLine & _
                " CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' WHEN RHM.Receipt_Type='P' THEN 'Advance' WHEN RHM.Receipt_Type='O' THEN 'On Account' WHEN RHM.Receipt_Type='A' THEN 'Apply Document' WHEN RHM.Receipt_Type='U' THEN 'UnApplied' WHEN RHM.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, " + Environment.NewLine & _
                " 0 as DrAmt, TSPL_RECEIPT_HEADER.Receipt_Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_RECEIPT_HEADER " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.UnApplied_No = TSPL_RECEIPT_HEADER.Receipt_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('U') " + Environment.NewLine & _
                " AND ISNULL(RHM.UnApplied_No,'')<>'' " + Environment.NewLine & _
                " UNION ALL" + Environment.NewLine & _
            " -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT-BULK) " + Environment.NewLine & _
                " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,TSPL_Customer_Invoice_Head.Document_No  AS DocNo, " + Environment.NewLine & _
                " 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code, " + Environment.NewLine & _
                " '' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL]," + Environment.NewLine & _
                " '' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name]," + Environment.NewLine & _
                " '' AS [Source Doc No],ISNULL (TSPL_INVOICE_MASTER_BULKSALE.Location_Code,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ," + Environment.NewLine & _
                " TSPL_Customer_Invoice_Head.Document_Date AS DocDate ,RHM.Receipt_No AS RefDocNo, RHM.Receipt_Date AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType, TSPL_BANK_REVERSE.Amount AS DrAmt, " + Environment.NewLine & _
                " 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_INVOICE_MASTER_BULKSALE " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No =TSPL_Customer_Invoice_Head.Document_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.Receipt_No = TSPL_RECEIPT_DETAIL.Receipt_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE .Document_No = RHM.Receipt_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_INVOICE_MASTER_BULKSALE.Location_Code " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('A','R') AND ISNULL(RHM.SecurityDeposit ,'')='N'  " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine & _
                " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,RHM.Receipt_No  AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode," + Environment.NewLine & _
                " ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No], " + Environment.NewLine & _
                " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code]," + Environment.NewLine & _
                " '' As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ," + Environment.NewLine & _
                " RHM.Receipt_Date AS DocDate ,RHM.Receipt_No AS RefDocNo, RHM.Receipt_Date AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType," + Environment.NewLine & _
                " TSPL_BANK_REVERSE.Amount AS  DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_BANK_REVERSE  " + Environment.NewLine & _
                " LEFT OUTER JOIN  TSPL_RECEIPT_HEADER RHM ON TSPL_BANK_REVERSE.Document_No = RHM.Receipt_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('P','O') AND ISNULL(RHM.SecurityDeposit ,'')='N'  " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
                "  -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine & _
                "  SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,TSPL_RECEIPT_DETAIL.Document_No AS DocNo, 'Bank Reverse' AS DocType," + Environment.NewLine & _
                " 'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(TSPL_CUSTOMER_MASTER.Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code," + Environment.NewLine & _
                " ISNULL(TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ,'') AS [Order Number],NULL  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No]," + Environment.NewLine & _
                " '' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks], " + Environment.NewLine & _
                " ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name],'' AS [Source Doc No], " + Environment.NewLine & _
                " Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ," + Environment.NewLine & _
                " RHM.Receipt_Date AS DocDate ,TSPL_BANK_REVERSE.Reverse_Code AS RefDocNo, TSPL_BANK_REVERSE.Reversal_Date  AS RefDocDate, " + Environment.NewLine & _
                " CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType,  TSPL_BANK_REVERSE.Amount AS  DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                " FROM TSPL_BANK_REVERSE " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.Receipt_No = TSPL_BANK_REVERSE.Document_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No = RHM.Receipt_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = RHM.Cust_Code " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No = TSPL_RECEIPT_DETAIL.Document_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_SD_SHIPMENT_HEAD.Document_Code " + Environment.NewLine & _
                " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('A','R') AND ISNULL(RHM.SecurityDeposit ,'')='N'  " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " --  VCGL DATA(FIRST QUERY) " + Environment.NewLine & _
                " SELECT TSPL_VCGL_Head.VC_Code AS CustCode, TSPL_VCGL_Head.VC_Name AS CustName, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode," + Environment.NewLine & _
                " ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No]," + Environment.NewLine & _
                " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code]," + Environment.NewLine & _
                " '' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp], " + Environment.NewLine & _
                " CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) AS DocDate,'' AS RefDocNo,Null AS RefDocDate,''  As SubDocType," + Environment.NewLine & _
                " CASE WHEN  TSPL_VCGL_Head.Amount_Type='Cr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END  AS DrAmt ,CASE WHEN  TSPL_VCGL_Head.Amount_Type='Dr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END AS CrAmt," + Environment.NewLine & _
                " 0 AS TransAmt,0 AS BalAmt  FROM TSPL_VCGL_Head " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code= TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                " WHERE TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " + Environment.NewLine & _
                " UNION ALL " + Environment.NewLine & _
            " -- VCGL DATA(SECOND QUERY) " + Environment.NewLine & _
               " SELECT TSPL_VCGL_Detail.VCGL_Code AS CustCode, TSPL_VCGL_Detail.VCGL_Name AS CustName, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode," + Environment.NewLine & _
               " ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No]," + Environment.NewLine & _
               " '' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks]," + Environment.NewLine & _
               " '' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp]," + Environment.NewLine & _
               " CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) AS DocDate, '' AS RefDocNo,NULL AS RefDocDate,''  As SubDocType, TSPL_VCGL_Detail.Dr_Amount AS DrAmt , " + Environment.NewLine & _
               " TSPL_VCGL_Detail.Cr_Amount AS CrAmt ,0 AS TransAmt,0 AS BalAmt FROM TSPL_VCGL_Detail " + Environment.NewLine & _
               " LEFT OUTER JOIN  TSPL_VCGL_Head ON  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No " + Environment.NewLine & _
               " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment " + Environment.NewLine & _
               " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No " + Environment.NewLine & _
               " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code= TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
               " WHERE TSPL_VCGL_Head.Status=1 AND  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " + Environment.NewLine & _
            " )xxx " + Environment.NewLine & _
           " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =xxx.CustCode " + Environment.NewLine & _
           " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = XXX.Cust_Group_Code " + Environment.NewLine & _
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
                'DocQry += " SELECT *, SUM([Trans Amt]) Over (Partition By DocumentNo Order By RowNo) as CumAmt FROM CTETemp "
                DocQry += " SELECT  *, CASE WHEN RowNoNew=1 Then CumAmt END AS Balance FROM ( " + Environment.NewLine & _
                          " SELECT *, SUM([Trans Amt]) Over (Partition By DocumentNo Order By RowNo) as CumAmt, ROW_NUMBER() over (PARTITION By DocumentNo ORDER By RowNo Desc) AS RowNoNew " + Environment.NewLine & _
                          " FROM CTETemp ) ZZZ Order By CustomerCode ,DocumentNo, ZZZ.RowNo "
            End If

            'DocQry += " Select * from "
            'DocQry += " ( " + Environment.NewLine & _
            '" Select *, Case When CumAmt<0 Then 0 Else CumAmt End as CumAmt1 from " + Environment.NewLine & _
            '"   ( Select *, Case When (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo)<0 Then (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<CTETEMP.RowNo) Else [Trans Amt] End as [TransAmt1], (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo) AS CumAmt FROM CTETEMP) YYY  " + Environment.NewLine & _
            '" UNION ALL " + Environment.NewLine & _
            '" Select YYY.RunDate,YYY.CompanyAddress,YYY.CompanyName,YYY.FilterFromDate,YYY.FilterToDate,YYY.RowNo, YYY.CustomerCode AS CustomerCode,YYY.[Customer Name]  AS [Customer Name],YYY.[Ref Doc No] as [Document No],'Credit Note' AS [Doc Type],'CN' AS [Doc Type Code],YYY.[Cust Group Code] AS [Cust Group Code] ,YYY.[Route No]  AS Route_No,YYY.[Zone Code] AS Zone_Code,YYY.[Order Number] AS [Order Number],YYY.[Due Date] AS [Due Date],yyy.[Against Sale No]  AS [Against Sale No],YYY.[Against Sale Return No]  As [Against Sale Return No],YYY.[Against MCC Material Sale Return] AS [Against MCC Material Sale Return],YYY.AgainstScrap  AS [AgainstScrap],YYY.[Against VCGL]  AS [Against VCGL],YYY.Description  AS [Description],YYY.[Source Doc No] AS [Source Doc No],YYY.Remarks  As [Remarks],YYY.[Child Cust Code]  AS [Child Cust Code],YYY.MainCustCode AS MainCustCode,YYY.MainCustName AS MainCustName,  YYY.[Loc Code] As [Loc Code],YYY.[Loc Desp] AS [Loc Desp], YYY.[Document Date] AS [Document Date],YYY.DocumentNo AS [Ref Doc No],YYY.[Ref Doc Date] AS [Ref Doc Date],'AR Invoice'  As [Sub Doc Type],YYY.DrAmt AS DrAmt,YYY.CrAmt  AS CrAmt,YYY.[Trans Amt] AS [Trans Amt],YYY.BalAmt AS BalAmt,YYY.ParentCode AS ParentCode, CumAmt As TransAmt1,CumAmt AS CumAmt,CumAmt AS CumAmt1 FROM (Select *, (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo) AS CumAmt FROM CTETEMP) YYY WHERE [Doc Type Code]='CN' AND CumAmt<0) ZZZ ORDER BY RowNo  "

            ''
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

            If blnRefresh = False Then
                If ChkSummary.Checked = True Then
                    frmCrystalReportViewer.funreport(CrystalReportFolder.Purchase, dtMain, "CustomerTransListLandScapeSummary", "Customer Transaction List (Summary)")
                Else
                    frmCrystalReportViewer.funreport(CrystalReportFolder.Purchase, dtMain, "CustomerTransListLandScape", "Customer Transaction List")
                End If
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox1.Enabled = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

        'If ChkSummary.Checked = True Then
        '    gv.GroupDescriptors.Clear()
        '    gv.GroupDescriptors.Add(New GridGroupByExpression("DocumentNo As [DocNo] format ""{0}: {1}"" group by DocumentNo"))
        'End If


        If ChkSummary.Checked = True Then
            'Dim CumAmt1 As New GridViewSummaryItem("CumAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(CumAmt1)
            'gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else
            Dim summaryItem As New GridViewSummaryItem()
            summaryItem.FormatString = "{0:F2}"
            summaryItem.Name = "CumAmt"
            summaryItem.AggregateExpression = "SUM([Trans Amt])"
            summaryRowItem.Add(summaryItem)
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
        gv.BestFitColumns()
        '   gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnQuickExport_Click(sender As Object, e As EventArgs) Handles BtnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & FormType & "'"))
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If gv.Rows.Count <= 0 Then
                Throw New Exception("No data found for Export.")
            End If
            clsCommon.MyExportToExcelGrid("Customer Transaction Report ", gv, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        FunReset()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        blnRefresh = True
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
        dtpFromdate.Value = "01/Apr/2015"
        dtptodate.Value = clsCommon.GETSERVERDATE()

        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset all Parameters")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        ButtonToolTip.SetToolTip(BtnQuickExport, "Quick Export")

        ChkSummary.Checked = False
        PnlSumm.Enabled = ChkSummary.Checked
    End Sub

    Private Sub txCustGroup__My_Click(sender As Object, e As EventArgs) Handles txCustGroup._My_Click
        strQry = "select  Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER order by Cust_Group_Code "
        txCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGrpTran", strQry, "Code", "Name", txCustGroup.arrValueMember, txCustGroup.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as Name,TSPL_CUSTOMER_MASTER.Parent_Customer_No as [Parent Code],P1.Customer_Name as [Parent Name] FROM TSPL_CUSTOMER_MASTER Left Outer Join TSPL_CUSTOMER_MASTER P1 on TSPL_CUSTOMER_MASTER.Parent_Customer_No =P1.Cust_Code   order by TSPL_CUSTOMER_MASTER.Cust_Code"
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
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub


    Private Sub ChkSummary_CheckStateChanged(sender As Object, e As EventArgs) Handles ChkSummary.CheckStateChanged
        PnlSumm.Enabled = ChkSummary.Checked
    End Sub

    
End Class
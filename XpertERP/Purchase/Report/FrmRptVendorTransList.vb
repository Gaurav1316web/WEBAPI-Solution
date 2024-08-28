' BM00000006187
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class FrmRptVendorTransList
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim dtMain As DataTable = Nothing
    Dim strQry As String = Nothing
    Dim FormType As String = Nothing


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmRptVendorTransList)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSplit.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Sub FunReset()
        rbtnDocWise.Checked = True
        ChkSummary.Checked = False
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
            arr.Add("Vendor")
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
            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                strFIlterCheck += " and XXX.Vendor_Group_Code in (" + clsCommon.GetMulcallString(txtVendorGroup.arrValueMember) + ")  " '----------changed here customer is used changed to vendor
            End If
         
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strFIlterCheck += " and (VenCode in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
                strFIlterCheck += ")"
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strFIlterCheck += " and [Loc Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
            End If

            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                strFIlterCheck += " and TSPL_VENDOR_GROUP.Acct_Set_Code  in (" + clsCommon.GetMulcallString(txtAccountSet.arrValueMember) + ")  "
            End If

            If clsCommon.CompairString(ddlDocType.SelectedIndex, "0") = CompairStringResult.Equal Then
                strFIlterCheck += " and xxx.DocType in ('Advance','On Account','Debit Note') "
            ElseIf clsCommon.CompairString(ddlDocType.SelectedIndex, "1") = CompairStringResult.Equal Then
                strFIlterCheck += " and xxx.DocType ='Advance' "
            ElseIf clsCommon.CompairString(ddlDocType.SelectedIndex, "2") = CompairStringResult.Equal Then
                strFIlterCheck += " and xxx.DocType ='On Account' "
            ElseIf clsCommon.CompairString(ddlDocType.SelectedIndex, "3") = CompairStringResult.Equal Then
                strFIlterCheck += " and xxx.DocType in ('Debit Note') "
            End If


            '----------------------------------------------------- BM00000007758
            Dim DocNoPart As String = String.Empty
            Dim DocNo As String = String.Empty
            Dim VenCode As String = String.Empty

            Dim DocQry As String = String.Empty
            If ChkSummary.Checked = True Then

            End If

            DocQry = "With CTETemp as "
            
            ''KDI/11/07/18-000400 pick vendor name from vendor master
            If ChkSummary.Checked = False Then
                'DocQry += "  ( Select '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + Comp_Name + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, " + Environment.NewLine & _
                '          " ROW_NUMBER() over (PARTITION By rtrim(DocNo) ORDER By RefDocNo, CONVERT(VARCHAR,RefDocDate,101)) AS RowNo, VenCode AS [VendorCode],VenName AS [Vendor Name],Desp ,ChequeNo ," + Environment.NewLine & _
                '          " RTRIM(DocNo) AS [DocumentNo] ,DocType  As [Doc Type],DocTypeCode AS [Doc Type Code],xxx.Vendor_Group_Code AS [Ven Group Code],xxx.Route_No AS [Route No]" + Environment.NewLine & _
                '          " ,xxx.Zone_Code AS [Zone Code],[Order Number] AS [Order Number],case when  CONVERT(VARCHAR,[Due Date],103)='01/01/1900' then null else CONVERT(VARCHAR,[Due Date],103) end AS [Due Date],[Against Sale No] AS [Against Sale No],[Against Sale Return No] As [Against Sale Return No],[Against MCC Material Sale Return] AS [Against MCC Material Sale Return],AgainstScrap AS [AgainstScrap],[Against VCGL] AS [Against VCGL],XXX.Description AS [Description],Remarks As [Remarks],[Child Cust Code] As [Child Cust Code],CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE VenCode END AS MainCustCode," + Environment.NewLine & _
                '          " CASE WHEN ISNULL([Child Cust Name],'')<>'' THEN [Child Cust Name]  ELSE VenName END AS MainCustName,[Source Doc No] AS [Source Doc No] ,[Loc Code] AS  [Loc Code],[Loc Desp] AS  [Loc Desp],CONVERT(VARCHAR,DOCDATE,103) AS [Document Date],RefDocNo AS [Ref Doc No],CONVERT(VARCHAR,RefDocDate,103) AS [Ref Doc Date],SubDocType AS [Sub Doc Type],DrAmt AS DrAmt,CrAmt AS CrAmt,CASE WHEN CrAmt =0 THEN DrAmt * -1 ELSE CrAmt END AS [Trans Amt],BalAmt AS BalAmt,CASE WHEN ISNULL(TSPL_VENDOR_MASTER.Parent_Vendor_Code,'') <> '' THEN ISNULL(TSPL_VENDOR_MASTER.Parent_Vendor_Code,'') ELSE VenCode END AS ParentCode,AppliedDocNo ,AppliedDocType   "
                DocQry += "  ( Select '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + Comp_Name + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, " + Environment.NewLine & _
                        " ROW_NUMBER() over (PARTITION By rtrim(DocNo) ORDER By RefDocNo, CONVERT(VARCHAR,RefDocDate,101)) AS RowNo, VenCode AS [VendorCode],TSPL_VENDOR_MASTER.Vendor_Name AS [Vendor Name],Desp ,ChequeNo ," + Environment.NewLine & _
                        " RTRIM(DocNo) AS [DocumentNo] ,DocType  As [Doc Type],DocTypeCode AS [Doc Type Code],xxx.Vendor_Group_Code AS [Ven Group Code],xxx.Route_No AS [Route No]" + Environment.NewLine & _
                        " ,xxx.Zone_Code AS [Zone Code],[Order Number] AS [Order Number],case when  CONVERT(VARCHAR,[Due Date],103)='01/01/1900' then null else CONVERT(VARCHAR,[Due Date],103) end AS [Due Date],[Against Sale No] AS [Against Sale No],[Against Sale Return No] As [Against Sale Return No],[Against MCC Material Sale Return] AS [Against MCC Material Sale Return],AgainstScrap AS [AgainstScrap],[Against VCGL] AS [Against VCGL],XXX.Description AS [Description],Remarks As [Remarks],[Child Cust Code] As [Child Cust Code],CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE VenCode END AS MainCustCode," + Environment.NewLine & _
                        " CASE WHEN ISNULL([Child Cust Name],'')<>'' THEN [Child Cust Name]  ELSE VenName END AS MainCustName,[Source Doc No] AS [Source Doc No] ,[Loc Code] AS  [Loc Code],[Loc Desp] AS  [Loc Desp],CONVERT(VARCHAR,DOCDATE,103) AS [Document Date],RefDocNo AS [Ref Doc No],CONVERT(VARCHAR,RefDocDate,103) AS [Ref Doc Date],SubDocType AS [Sub Doc Type],DrAmt AS DrAmt,CrAmt AS CrAmt,CASE WHEN DrAmt =0 THEN CrAmt * -1 ELSE DrAmt END AS [Trans Amt],BalAmt AS BalAmt,CASE WHEN ISNULL(TSPL_VENDOR_MASTER.Parent_Vendor_Code,'') <> '' THEN ISNULL(TSPL_VENDOR_MASTER.Parent_Vendor_Code,'') ELSE VenCode END AS ParentCode,AppliedDocNo ,AppliedDocType   "
            Else
                If rbtnDocWise.Checked = True Then
                    DocNoPart = " MainQuery.DocNo "
                    DocNo = " RTRIM(MainQuery.DocNo) "
                    VenCode = " MAX(MainQuery.VenCode) "
                ElseIf rbtnVenWise.Checked = True Then
                    DocNoPart = " MAX(MainQuery.DocNo) "
                    DocNo = " MAX(RTRIM(MainQuery.DocNo)) "
                    VenCode = " MainQuery.VenCode "
                End If


              
                ''KDI/11/07/18-000400 pick vendor name from vendor master
                DocQry += "  ( Select '" + runDate + "'  as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + Comp_Name + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, " + Environment.NewLine & _
                " ROW_NUMBER() over (PARTITION By " & DocNoPart & " ORDER By MAX(RefDocDate)) AS RowNo," & VenCode & " AS [VendorCode],MAX(VenName) AS [Vendor Name]," + Environment.NewLine & _
                " " & DocNo & " AS [DocumentNo] ,MAX(DocType)  As [Doc Type],MAX(DocTypeCode) AS [Doc Type Code],MAX(MainQuery.Vendor_Group_Code) AS [Ven Group Code],MAX(MainQuery.Route_No) AS [Route No] " + Environment.NewLine & _
                " ,MAX(MainQuery.Zone_Code) AS [Zone Code],MAX([Order Number]) AS [Order Number], " + Environment.NewLine & _
                " case when  CONVERT(VARCHAR,MAX([Due Date]),103)='01/01/1900' then null else CONVERT(VARCHAR,MAX([Due Date]),103) end AS [Due Date], " + Environment.NewLine & _
                " MAX([Against Sale No]) AS [Against Sale No], " + Environment.NewLine & _
                " MAX([Against Sale Return No]) As [Against Sale Return No],MAX([Against MCC Material Sale Return]) AS [Against MCC Material Sale Return], " + Environment.NewLine & _
                " MAX(AgainstScrap) AS [AgainstScrap],MAX([Against VCGL]) AS [Against VCGL], " + Environment.NewLine & _
                " MAX(MainQuery.Description) AS [Description],MAX(Remarks) As [Remarks],MAX([Child Cust Code]) As [Child Cust Code], " + Environment.NewLine & _
                " MAX(MainCustCode) AS MainCustCode,MAX(MainCustName)AS MainCustName, " + Environment.NewLine & _
                " MAX([Source Doc No]) AS [Source Doc No] ,MAX([Loc Code]) AS  [Loc Code],MAX([Loc Desp]) AS  [Loc Desp], " + Environment.NewLine & _
                " CONVERT(VARCHAR,MAX(DOCDATE),103) AS [Document Date],MAX(RefDocNo) AS [Ref Doc No],CONVERT (VARCHAR,MAX(RefDocDate),101) AS [Ref Doc Date], " + Environment.NewLine & _
                " MAX(SubDocType) AS [Sub Doc Type],SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,sum([Trans Amt] ) as  [Trans Amt], " + Environment.NewLine & _
                " MAX(BalAmt) AS BalAmt,MAX(ParentCode)  AS ParentCode from (   " + Environment.NewLine & _
                " Select ROW_NUMBER() over (PARTITION By DocNo ORDER By CONVERT(DATE,RefDocDate,103)) AS RowNo, VenCode As [VenCode],TSPL_VENDOR_MASTER.Vendor_Name AS [VenName] " + Environment.NewLine & _
                " ,DocNo As [DocNo],DocType AS [DocType],DocTypeCode AS [DocTypeCode],xxx.Vendor_Group_Code AS [Vendor_Group_Code],xxx.Route_No AS [Route_No] " + Environment.NewLine & _
                " ,xxx.Zone_Code AS [Zone_Code],[Order Number] AS [Order Number],case when  CONVERT(VARCHAR,[Due Date],103)='01/01/1900' then null else CONVERT(VARCHAR,[Due Date],103) end AS [Due Date] ,[Against Sale No] AS [Against Sale No],[Against Sale Return No] As [Against Sale Return No],[Against MCC Material Sale Return] AS [Against MCC Material Sale Return] " + Environment.NewLine & _
                " ,[AgainstScrap] AS [AgainstScrap],[Against VCGL] AS [Against VCGL],xxx.Desp  AS [Description],[Source Doc No] ,[Remarks] As [Remarks] " + Environment.NewLine & _
                " ,[Child Cust Code],CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE VenCode END AS MainCustCode,CASE WHEN ISNULL([Child Cust Name] " + Environment.NewLine & _
                " ,'')<>'' THEN [Child Cust Name]  ELSE VenName  END AS MainCustName,[Loc Code] AS  [Loc Code],[Loc Desp] AS  [Loc Desp] " + Environment.NewLine & _
                " ,CONVERT(VARCHAR,DocDate,103) AS [DocDate],RefDocNo AS [RefDocNo],CONVERT (VARCHAR,RefDocDate,101) AS [RefDocDate],SubDocType AS [SubDocType] " + Environment.NewLine & _
                " ,DrAmt AS DrAmt,CrAmt , CASE WHEN CrAmt =0  then DrAmt ELSE CrAmt * -1 END AS [Trans Amt] ,BalAmt ,CASE WHEN ISNULL(TSPL_VENDOR_MASTER.Parent_Vendor_Code,'')<>''  THEN ISNULL(TSPL_VENDOR_MASTER.Parent_Vendor_Code,'') ELSE VenCode END AS ParentCode,AppliedDocNo ,AppliedDocType  "

            End If

            DocQry += "  From " + Environment.NewLine & _
            " ( " + Environment.NewLine & _
            " -- DEBIT NOTE SEPERATED " + Environment.NewLine & _
            " SELECT TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AS VenCode,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name AS VenName,ISNULL(TSPL_VENDOR_INVOICE_HEAD.Description,'') AS [Desp],'' AS ChequeNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_VENDOR_INVOICE_HEAD.RefDocNo ELSE TSPL_VENDOR_INVOICE_HEAD.Document_No END AS DocNo,'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Vendor_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date   AS DocDate,'' AS RefDocNo, NULL AS RefDocDate,''  As SubDocType,TSPL_VENDOR_INVOICE_HEAD.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt, '' as AppliedDocNo, ''  as AppliedDocType  " + Environment.NewLine & _
            " From TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  = TSPL_VENDOR_MASTER.Vendor_Code   " + Environment.NewLine & _
            " WHERE LEN(TSPL_VENDOR_INVOICE_HEAD.Posting_Date) IS NOT NULL AND TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' AND ISNULL(RefDocNo,'')='' AND ISNULL(Against_PurchaseReturn_No,'')= '' " + Environment.NewLine & _
            " UNION ALL " + Environment.NewLine & _
            " -- PAYMENT " + Environment.NewLine & _
            " SELECT TSPL_PAYMENT_HEADER.Vendor_Code  as VenCode,TSPL_PAYMENT_HEADER.Vendor_Name AS VenName,ISNULL(TSPL_PAYMENT_HEADER.Entry_Desc,'') AS [Desp],'' AS ChequeNo,TSPL_PAYMENT_HEADER.Payment_No  AS DocNo,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='PY' THEN 'Apply Document' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AV' THEN 'Advance' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='OA' THEN 'On Account' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='F' THEN 'Receipt' ELSE NULL END AS DocType,ISNULL(TSPL_PAYMENT_HEADER.Payment_Type ,'') AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Vendor_Group_Code,ISNULL(Route_No,'') AS Route_No,'' AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_PAYMENT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3)) As [Loc Code] ,ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103)  AS DocDate,'' as RefDocNo,NULL AS RefDocDate,'' As SubDocType,TSPL_PAYMENT_HEADER.Payment_Amount AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt, '' as AppliedDocNo, ''  as AppliedDocType " + Environment.NewLine & _
            " FROM TSPL_PAYMENT_HEADER " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3)) " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
            " WHERE  TSPL_PAYMENT_HEADER.Posted='1' AND  TSPL_PAYMENT_HEADER.Payment_Type  IN ('F') " + Environment.NewLine & _
            " UNION ALL " + Environment.NewLine & _
            " -- ADVANCE/ON-ACCOUNT " + Environment.NewLine & _
            " SELECT TSPL_PAYMENT_HEADER.Vendor_Code AS VenCode,TSPL_PAYMENT_HEADER.Vendor_Name AS VenName,ISNULL(TSPL_PAYMENT_HEADER.Entry_Desc,'') AS [Desp],TSPL_PAYMENT_HEADER.Cheque_No AS ChequeNo,TSPL_PAYMENT_HEADER.Payment_No  AS DocNo,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AV' THEN 'Advance' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='OA' THEN 'On Account' ELSE NULL END AS DocType,ISNULL(TSPL_PAYMENT_HEADER.Payment_Type ,'') AS DocTypeCode,ISNULL(Vendor_Group_Code ,'')  AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_PAYMENT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3)) AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103)  AS DocDate,'' as RefDocNo,CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103) AS RefDocDate,''  AS SubDocType, TSPL_PAYMENT_HEADER.Payment_Amount AS DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt, '' as AppliedDocNo, ''  as AppliedDocType " + Environment.NewLine & _
            " FROM TSPL_PAYMENT_HEADER" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3)) " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine & _
            " WHERE  TSPL_PAYMENT_HEADER.Posted='1' AND  TSPL_PAYMENT_HEADER.Payment_Type  IN ('AV','OA') " + Environment.NewLine & _
            " UNION ALL " + Environment.NewLine & _
            " -- APPLY DOCUMENT AGAINST ADVANCE/ON-ACCOUNT/Debit Note " + Environment.NewLine & _
            " SELECT TSPL_PAYMENT_HEADER.Vendor_Code  AS VenCode, TSPL_PAYMENT_HEADER.Vendor_Name  AS VenName,ISNULL(TSPL_PAYMENT_HEADER.Entry_Desc,'') AS [Desp],TSPL_PAYMENT_HEADER.Cheque_No AS ChequeNo,TSPL_PAYMENT_HEADER.Applied_Payment   AS DocNo," + Environment.NewLine & _
            " CASE WHEN (SELECT RHI.Payment_Type FROM TSPL_PAYMENT_HEADER RHI WHERE RHI.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment )= 'AV' THEN 'Advance' " + Environment.NewLine & _
            " WHEN (SELECT RHI.Payment_Type FROM TSPL_PAYMENT_HEADER RHI WHERE RHI.Payment_No=TSPL_PAYMENT_HEADER.Applied_Payment)= 'OA' THEN 'On Account' WHEN (SELECT CIH.Document_Type FROM TSPL_VENDOR_INVOICE_HEAD CIH WHERE CIH.Document_No =TSPL_PAYMENT_HEADER.Applied_Payment)= 'D' THEN 'Debit Note' END AS DocType," + Environment.NewLine & _
            " Case when  (SELECT CIH.Document_Type FROM TSPL_VENDOR_INVOICE_HEAD CIH WHERE CIH.Document_No =TSPL_PAYMENT_HEADER.Applied_Payment)= 'D' THEN 'DN' else (SELECT RHI.Payment_Type  FROM TSPL_PAYMENT_HEADER RHI WHERE RHI.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment) end AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_PAYMENT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_PAYMENT_HEADER.Vendor_Code ,'') AS [Child Cust Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name ,'') As [Child Cust Name], '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103) AS DocDate,TSPL_PAYMENT_HEADER.Payment_No   AS RefDocNo,CONVERT(VARCHAR, TSPL_PAYMENT_HEADER.Payment_Date   ,103) AS RefDocDate,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='RC' THEN 'Receipt' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='PV' THEN 'Advance' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='OA' THEN 'On Account' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'Apply Document' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='U' THEN 'UnApplied' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='RC' THEN 'Receipt' END  AS SubDocType,0 as DrAmt," & _
            " case when isnull(TSPL_VENDOR_INVOICE_HEAD.Document_Type ,'')='D' then isnull(TSPL_PAYMENT_DETAIL.Applied_Amount,0) *-1 else isnull(TSPL_PAYMENT_DETAIL.Applied_Amount,0) end AS CrAmt, " & _
            " 0 AS TransAmt,0 AS BalAmt, ISNULL(TSPL_PAYMENT_DETAIL.Document_No,'') as AppliedDocNo, isnull(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'R')as AppliedDocType " + Environment.NewLine & _
            " FROM TSPL_PAYMENT_HEADER" + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_PAYMENT_DETAIL.Payment_No    " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3))  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD  ON TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_DETAIL.Document_No " + Environment.NewLine & _
            " WHERE  TSPL_PAYMENT_HEADER.Posted='1' AND  TSPL_PAYMENT_HEADER.Payment_Type  IN ('AD')  AND ISNULL(Applied_Payment ,'')<>'' " + Environment.NewLine & _
            " UNION ALL" + Environment.NewLine & _
            " -- BANK REVERSE (FOR ADVANCE/ON ACCOUNT) " + Environment.NewLine & _
            " SELECT TSPL_BANK_REVERSE.Vendor_Code AS VenCode, TSPL_BANK_REVERSE.Vendor_Name  AS VenName,ISNULL(TSPL_BANK_REVERSE.Reason,'') AS  [Desp],RHM.Cheque_No AS ChequeNo ,RHM.Payment_No   AS DocNo, " & _
            " CASE WHEN (select rh .Payment_Type from TSPL_PAYMENT_HEADER rh where rh .Payment_No  = RHM.Payment_No )='OA' THEN 'On Account' WHEN (select rh .Payment_Type from TSPL_PAYMENT_HEADER rh where rh .Payment_No = RHM.Payment_No)='AV' THEN 'Advance' ELSE '' END AS DocType, (select rh .Payment_Type from TSPL_PAYMENT_HEADER rh where rh .Payment_No = RHM.Payment_No) AS DocTypeCode," & _
            " ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,ISNULL(Route_No,'') AS Route_No,'' AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] , CONVERT(VARCHAR,RHM.Payment_Date,103) AS DocDate ,TSPL_BANK_REVERSE.Reverse_Code AS RefDocNo,CONVERT(VARCHAR, RHM.Payment_Date,103) AS RefDocDate,'Bank Reverse' AS SubDocType,0 AS  DrAmt,TSPL_BANK_REVERSE.Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt,ISNULL(TSPL_PAYMENT_DETAIL.Payment_No,'') as AppliedDocNo, ISNULL(TSPL_PAYMENT_DETAIL.Payment_Type,'') as AppliedDocType " + Environment.NewLine & _
            " FROM TSPL_BANK_REVERSE " + Environment.NewLine & _
            " LEFT OUTER JOIN  TSPL_PAYMENT_HEADER RHM ON TSPL_BANK_REVERSE.Document_No = RHM.Payment_No " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No = RHM.Payment_No " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3)) " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_BANK_REVERSE.Vendor_Code   = TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine & _
            "  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD  ON TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_DETAIL .Document_No " + Environment.NewLine & _
            " WHERE TSPL_BANK_REVERSE.Source_Type='AP' AND TSPL_BANK_REVERSE.post='P' AND RHM.Payment_Type IN ('AV','OA') " + Environment.NewLine & _
            " UNION ALL " + Environment.NewLine & _
            " -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine & _
            " SELECT TSPL_BANK_REVERSE.Vendor_Code AS VenCode, TSPL_BANK_REVERSE.Vendor_Name  AS VenName,ISNULL(TSPL_BANK_REVERSE.Reason,'') AS  [Desp],RHM.Cheque_No AS ChequeNo ," & _
            " RHM.Applied_Payment AS DocNo,   CASE WHEN (select rh .Payment_Type from TSPL_PAYMENT_HEADER rh where rh .Payment_No  = RHM.Applied_Payment )='OA' THEN 'On Account' WHEN (select rh .Payment_Type from TSPL_PAYMENT_HEADER rh where rh .Payment_No = RHM.Applied_Payment)='AV' THEN 'Advance' ELSE '' END AS DocType, (select rh .Payment_Type from TSPL_PAYMENT_HEADER rh where rh .Payment_No = RHM.Applied_Payment) AS DocTypeCode," & _
            " ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],NULL  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_VENDOR_MASTER.Vendor_Code ,'') AS [Child Cust Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name ,'') As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ,CONVERT(VARCHAR, RHM.Payment_Date ,103)  AS DocDate ,TSPL_BANK_REVERSE.Reverse_Code AS RefDocNo,CONVERT(VARCHAR, TSPL_BANK_REVERSE.Reversal_Date ,103) AS RefDocDate,'Bank Reverse' AS SubDocType,0 AS  DrAmt," & _
            " case when RHM.Payment_Type='AD' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'') ='D' then TSPL_PAYMENT_DETAIL.Applied_Amount when RHM.Payment_Type='AD' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'') <>'D' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE 0 end  as CrAmt, " + Environment.NewLine & _
            " 0 AS TransAmt,0 AS BalAmt,ISNULL(TSPL_PAYMENT_DETAIL.Payment_No,'') as AppliedDocNo, ISNULL(TSPL_PAYMENT_DETAIL.Payment_Type,'') as AppliedDocType FROM TSPL_BANK_REVERSE  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_PAYMENT_HEADER RHM ON RHM.Payment_No  = TSPL_BANK_REVERSE.Document_No  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_PAYMENT_DETAIL  ON TSPL_PAYMENT_DETAIL.Payment_No  = RHM.Payment_No  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code  = RHM.Vendor_Code     " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3))  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD  ON TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_DETAIL.Document_No  " + Environment.NewLine & _
            " WHERE TSPL_BANK_REVERSE.Source_Type='AP' AND TSPL_BANK_REVERSE.post='P' AND RHM.Payment_Type  IN ('AD','PY','RC')" + Environment.NewLine & _
            " UNION ALL " + Environment.NewLine & _
            " -- Debit NOTE WHICH APPLIED ON APPLY DOCUMENT " + Environment.NewLine & _
            " SELECT TSPL_PAYMENT_HEADER.Vendor_Code  AS VenCode, TSPL_PAYMENT_HEADER.Vendor_Name  AS VenName,ISNULL(TSPL_PAYMENT_HEADER.Entry_Desc,'') AS [Desp],TSPL_PAYMENT_HEADER.Cheque_No AS ChequeNo,TSPL_PAYMENT_DETAIL.Document_No   AS DocNo," + Environment.NewLine & _
            " CASE WHEN (SELECT CIH.Document_Type FROM TSPL_VENDOR_INVOICE_HEAD CIH WHERE CIH.Document_No =TSPL_PAYMENT_DETAIL.Document_No)= 'D' THEN 'Debit Note' END AS DocType," + Environment.NewLine & _
            " Case when  (SELECT CIH.Document_Type FROM TSPL_VENDOR_INVOICE_HEAD CIH WHERE CIH.Document_No =TSPL_PAYMENT_DETAIL.Document_No)= 'D' THEN 'DN' end AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_PAYMENT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_PAYMENT_HEADER.Vendor_Code ,'') AS [Child Cust Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name ,'') As [Child Cust Name], '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103) AS DocDate,TSPL_PAYMENT_HEADER.Payment_No   AS RefDocNo,CONVERT(VARCHAR, TSPL_PAYMENT_HEADER.Payment_Date   ,103) AS RefDocDate,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='RC' THEN 'Receipt' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='PV' THEN 'Advance' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='OA' THEN 'On Account' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'Apply Document' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='U' THEN 'UnApplied' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='RC' THEN 'Receipt' END  AS SubDocType,0 as DrAmt,case when isnull(TSPL_VENDOR_INVOICE_HEAD.Document_Type ,'')='D' then isnull(TSPL_PAYMENT_DETAIL.Applied_Amount,0) *-1 else isnull(TSPL_PAYMENT_DETAIL.Applied_Amount,0) end AS CrAmt,  0 AS TransAmt,0 AS BalAmt, ISNULL(TSPL_PAYMENT_DETAIL.Document_No,'') as AppliedDocNo, isnull(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'R')as AppliedDocType  FROM TSPL_PAYMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_PAYMENT_DETAIL.Payment_No    " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3))  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD  ON TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_DETAIL.Document_No " + Environment.NewLine & _
            " WHERE  TSPL_PAYMENT_HEADER.Posted='1' AND  TSPL_PAYMENT_HEADER.Payment_Type  IN ('AD')  AND ISNULL(Applied_Payment ,'')<>'' and  TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D'" + Environment.NewLine & _
            " UNION ALL " + Environment.NewLine & _
            " -- BANK REVERSE (FOR Credit Note) " + Environment.NewLine & _
            " SELECT TSPL_BANK_REVERSE.Vendor_Code AS VenCode, TSPL_BANK_REVERSE.Vendor_Name  AS VenName,ISNULL(TSPL_BANK_REVERSE.Reason,'') AS  [Desp],RHM.Cheque_No AS ChequeNo , TSPL_PAYMENT_DETAIL.Document_No AS DocNo,   CASE WHEN (SELECT CIH.Document_Type FROM TSPL_VENDOR_INVOICE_HEAD CIH WHERE CIH.Document_No =TSPL_PAYMENT_DETAIL.Document_No)= 'D' THEN 'Debit Note' END AS DocType,Case when  (SELECT CIH.Document_Type FROM TSPL_VENDOR_INVOICE_HEAD CIH WHERE CIH.Document_No =TSPL_PAYMENT_DETAIL.Document_No)= 'D' THEN 'DN' end AS DocTypeCode, ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],NULL  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_VENDOR_MASTER.Vendor_Code ,'') AS [Child Cust Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name ,'') As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ,CONVERT(VARCHAR, RHM.Payment_Date ,103)  AS DocDate ,TSPL_BANK_REVERSE.Reverse_Code AS RefDocNo,CONVERT(VARCHAR, TSPL_BANK_REVERSE.Reversal_Date ,103) AS RefDocDate,'Bank Reverse' AS SubDocType,0 AS  DrAmt,case when RHM.Payment_Type='AD' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'') ='D' then TSPL_PAYMENT_DETAIL.Applied_Amount when RHM.Payment_Type='AD' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'') <>'D' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE 0 end  as CrAmt,  0 AS TransAmt,0 AS BalAmt,ISNULL(TSPL_PAYMENT_DETAIL.Payment_No,'') as AppliedDocNo, ISNULL(TSPL_PAYMENT_DETAIL.Payment_Type,'') as AppliedDocType FROM TSPL_BANK_REVERSE  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_PAYMENT_HEADER RHM ON RHM.Payment_No  = TSPL_BANK_REVERSE.Document_No  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_PAYMENT_DETAIL  ON TSPL_PAYMENT_DETAIL.Payment_No  = RHM.Payment_No  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code  = RHM.Vendor_Code     " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3))  " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD  ON TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_DETAIL.Document_No  " + Environment.NewLine & _
            " WHERE TSPL_BANK_REVERSE.Source_Type='AP' AND TSPL_BANK_REVERSE.post='P' AND RHM.Payment_Type  IN ('AD','PY','RC') and  TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' " + Environment.NewLine & _
            " )xxx " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code =xxx.VenCode " + Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_VENDOR_GROUP ON TSPL_VENDOR_GROUP.Ven_Group_Code = XXX.Vendor_Group_Code " + Environment.NewLine & _
            " WHERE 1=1 " & strFIlterCheck & " AND CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' "
            If ChkSummary.Checked = True Then
                'If rbtnDocWise.Checked = True Then
                '    DocQry += " GROUP BY DocNo "
                'Else
                '    DocQry += " GROUP BY VenCode "
                'End If
                If rbtnDocWise.Checked = True Then
                    DocQry += " ) MainQuery  GROUP BY DocNo "
                Else
                    DocQry += " ) MainQuery  GROUP BY VenCode "
                End If
            End If
            DocQry += " )  "

            If ChkSummary.Checked = True Then
                DocQry += " SELECT *, [Trans Amt]  AS Balance FROM CTETemp "
            Else
                'DocQry += " SELECT *, SUM([Trans Amt]) Over (Partition By DocumentNo Order By RowNo) as CumAmt FROM CTETemp "
                DocQry += " SELECT  *, CASE WHEN RowNoNew=1 Then CumAmt END AS Balance FROM ( " + Environment.NewLine & _
                            " SELECT *, SUM([Trans Amt]) Over (Partition By DocumentNo Order By RowNo) as CumAmt, ROW_NUMBER() over (PARTITION By DocumentNo ORDER By RowNo Desc) AS RowNoNew " + Environment.NewLine & _
                            " FROM CTETemp ) ZZZ Order By VendorCode  ,DocumentNo , ZZZ.RowNo"
            End If




                'DocQry += "  From " + Environment.NewLine & _
                '" ( " + Environment.NewLine & _
                '" --  AP INVOICE " + Environment.NewLine & _
                '" SELECT TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AS VenCode,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name AS VenName,ISNULL(TSPL_VENDOR_INVOICE_HEAD.Description,'') AS [Desp],'' AS ChequeNo,TSPL_VENDOR_INVOICE_HEAD.Document_No AS DocNo,'AP Invoice' AS DocType,'IN' AS DocTypeCode,ISNULL(Vendor_Group_Code,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  AS DocDate, '' AS RefDocNo, Null AS RefDocDate,''  As SubDocType,0  AS DrAmt,TSPL_VENDOR_INVOICE_HEAD.Document_Total AS CrAmt,0 AS TransAmt,0 AS BalAmt  " + Environment.NewLine & _
                '" From TSPL_VENDOR_INVOICE_HEAD" + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
                '" WHERE LEN(TSPL_VENDOR_INVOICE_HEAD.Posting_Date) IS NOT NULL  AND TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" --  CREDIT NOTE AGAINST INVOICE " + Environment.NewLine & _
                '" SELECT TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  AS VenCode,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name  AS VenName,ISNULL(TSPL_VENDOR_INVOICE_HEAD.Description,'') AS [Desp],'' AS ChequeNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_VENDOR_INVOICE_HEAD.RefDocNo ELSE TSPL_VENDOR_INVOICE_HEAD.Document_No END AS DocNo,'Credit Note' AS DocType,'CN' AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date   AS DocDate, CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_VENDOR_INVOICE_HEAD.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ELSE NULL END AS RefDocDate,'Credit Note'  As SubDocType,0 AS DrAmt,ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Total ,0) AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                '" From TSPL_VENDOR_INVOICE_HEAD" + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
                '" WHERE LEN(TSPL_VENDOR_INVOICE_HEAD.Posting_Date) IS NOT NULL AND TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' AND ISNULL(RefDocNo,'')<>'' " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- CREDIT NOTE SEPEARTED " + Environment.NewLine & _
                '"SELECT TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AS VenCode,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name AS VenName,ISNULL(TSPL_VENDOR_INVOICE_HEAD.Description,'') AS [Desp],'' AS ChequeNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_VENDOR_INVOICE_HEAD.RefDocNo ELSE TSPL_VENDOR_INVOICE_HEAD.Document_No END AS DocNo,'Credit Note' AS DocType,'CN' AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date   AS DocDate, CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_VENDOR_INVOICE_HEAD.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ELSE NULL END AS RefDocDate,'Credit Note'  As SubDocType,0 AS DrAmt,TSPL_VENDOR_INVOICE_HEAD.Document_Total  AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                '" From TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
                '" WHERE LEN(TSPL_VENDOR_INVOICE_HEAD.Posting_Date) IS NOT NULL  AND TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' AND ISNULL(RefDocNo,'')='' " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- DEBIT NOTE SEPERATED " + Environment.NewLine & _
                '" SELECT TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AS VenCode,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name AS VenName,ISNULL(TSPL_VENDOR_INVOICE_HEAD.Description,'') AS [Desp],'' AS ChequeNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_VENDOR_INVOICE_HEAD.RefDocNo ELSE TSPL_VENDOR_INVOICE_HEAD.Document_No END AS DocNo,'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date   AS DocDate,'' AS RefDocNo, NULL AS RefDocDate,''  As SubDocType,TSPL_VENDOR_INVOICE_HEAD.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  " + Environment.NewLine & _
                '" From TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  = TSPL_VENDOR_MASTER.Vendor_Code   " + Environment.NewLine & _
                '" WHERE LEN(TSPL_VENDOR_INVOICE_HEAD.Posting_Date) IS NOT NULL AND TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' AND ISNULL(RefDocNo,'')='' " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- DEBIT NOTE AGAINST INVOICE " + Environment.NewLine & _
                '" SELECT TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  AS CustCode,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name  AS CustName,ISNULL(TSPL_VENDOR_INVOICE_HEAD.Description,'') AS [Desp],'' AS ChequeNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_VENDOR_INVOICE_HEAD.RefDocNo ELSE TSPL_VENDOR_INVOICE_HEAD.Document_No END AS DocNo,'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Vendor_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date   AS DocDate, TSPL_VENDOR_INVOICE_HEAD.Document_No AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ELSE NULL END AS RefDocDate,'Debit Note'  As SubDocType,TSPL_VENDOR_INVOICE_HEAD.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  " + Environment.NewLine & _
                '" From TSPL_VENDOR_INVOICE_HEAD" + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_VENDOR_INVOICE_HEAD.Loc_Code  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  = TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine & _
                '" WHERE LEN(TSPL_VENDOR_INVOICE_HEAD.Posting_Date) IS NOT NULL AND TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' AND ISNULL(RefDocNo,'')<>'' " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- PAYMENT ENTRIES (APPLY DOCUMENT/RECEIPT)  " + Environment.NewLine & _
                '" SELECT TSPL_PAYMENT_HEADER.Vendor_Code  AS VenCode, TSPL_PAYMENT_HEADER.Vendor_Name AS VenName,ISNULL(TSPL_PAYMENT_HEADER.Entry_Desc,'') AS [Desp],'' AS ChequeNo,TSPL_PAYMENT_DETAIL.Document_No AS DocNo " + Environment.NewLine & _
                '" ,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='PY' THEN 'Payment' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'Apply Document' ELSE NULL END AS DocType,ISNULL(TSPL_PAYMENT_HEADER.Payment_Type ,'') AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Vendor_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_PAYMENT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_VENDOR_MASTER.Vendor_Code ,'') AS [Child Cust Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name ,'') As [Child Cust Name], '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp], " + Environment.NewLine & _
                '" CONVERT(VARCHAR, TSPL_PAYMENT_HEADER.Payment_Date ,103) AS DocDate,TSPL_PAYMENT_DETAIL.Payment_No AS RefDocNo,CONVERT(VARCHAR, TSPL_PAYMENT_HEADER.Payment_Date   ,103) AS RefDocDate, " + Environment.NewLine & _
                '" CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='PY' THEN 'Payment' WHEN  TSPL_PAYMENT_HEADER.Payment_Type ='AV' THEN 'Advance' WHEN  TSPL_PAYMENT_HEADER.Payment_Type ='OA' THEN 'On Account' WHEN  TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'Apply Document' WHEN  TSPL_PAYMENT_HEADER.Payment_Type ='U' THEN 'UnApplied' WHEN  TSPL_PAYMENT_HEADER.Payment_Type ='RC' THEN 'Receipt' END  AS SubDocType,  TSPL_PAYMENT_DETAIL.Applied_Amount  as DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                '" FROM TSPL_PAYMENT_DETAIL" + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No  = TSPL_PAYMENT_DETAIL.Payment_No    " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3))  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code  = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
                '" WHERE  TSPL_PAYMENT_HEADER.Posted='1' AND  TSPL_PAYMENT_HEADER.Payment_Type  IN ('AD','PY') " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- PAYMENT " + Environment.NewLine & _
                '" SELECT TSPL_PAYMENT_HEADER.Vendor_Code  as VenCode,TSPL_PAYMENT_HEADER.Vendor_Name  AS CustName,ISNULL(TSPL_PAYMENT_HEADER.Entry_Desc,'') AS [Desp],'' AS ChequeNo,TSPL_PAYMENT_HEADER.Payment_No  AS DocNo,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='PY' THEN 'Apply Document' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AV' THEN 'Advance' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='OA' THEN 'On Account' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='F' THEN 'Receipt' ELSE NULL END AS DocType,ISNULL(TSPL_PAYMENT_HEADER.Payment_Type ,'') AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,ISNULL(Route_No,'') AS Route_No,'' AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_PAYMENT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3)) As [Loc Code] ,ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103)  AS DocDate,'' as RefDocNo,NULL AS RefDocDate,'' As SubDocType,TSPL_PAYMENT_HEADER.Payment_Amount AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                '" FROM TSPL_PAYMENT_HEADER " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3)) " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
                '" WHERE  TSPL_PAYMENT_HEADER.Posted='1' AND  TSPL_PAYMENT_HEADER.Payment_Type  IN ('F') " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- ADVANCE/ON-ACCOUNT " + Environment.NewLine & _
                '" SELECT TSPL_PAYMENT_HEADER.Vendor_Code AS VenCode,TSPL_PAYMENT_HEADER.Vendor_Name AS VenName,ISNULL(TSPL_PAYMENT_HEADER.Entry_Desc,'') AS [Desp],TSPL_PAYMENT_HEADER.Cheque_No AS ChequeNo,TSPL_PAYMENT_HEADER.Payment_No  AS DocNo,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AV' THEN 'Advance' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='OA' THEN 'On Account' ELSE NULL END AS DocType,ISNULL(TSPL_PAYMENT_HEADER.Payment_Type ,'') AS DocTypeCode,ISNULL(Vendor_Group_Code ,'')  AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_PAYMENT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3)) AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103)  AS DocDate,'' as RefDocNo,CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103) AS RefDocDate,''  AS SubDocType, TSPL_PAYMENT_HEADER.Payment_Amount AS DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                '" FROM TSPL_PAYMENT_HEADER" + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3)) " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine & _
                '" WHERE  TSPL_PAYMENT_HEADER.Posted='1' AND  TSPL_PAYMENT_HEADER.Payment_Type  IN ('AV','OA') " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- APPLY DOCUMENT AGAINST ADVANCE/ON-ACCOUNT " + Environment.NewLine & _
                '" SELECT TSPL_PAYMENT_HEADER.Vendor_Code  AS VenCode, TSPL_PAYMENT_HEADER.Vendor_Name  AS VenName,ISNULL(TSPL_PAYMENT_HEADER.Entry_Desc,'') AS [Desp],TSPL_PAYMENT_HEADER.Cheque_No AS ChequeNo,TSPL_PAYMENT_HEADER.Applied_Payment   AS DocNo," + Environment.NewLine & _
                '" CASE WHEN (SELECT RHI.Payment_Type FROM TSPL_PAYMENT_HEADER RHI WHERE RHI.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment )= 'AV' THEN 'Advance' " + Environment.NewLine & _
                '" WHEN (SELECT RHI.Payment_Type FROM TSPL_PAYMENT_HEADER RHI WHERE RHI.Payment_No=TSPL_PAYMENT_HEADER.Applied_Payment)= 'OA' THEN 'On Account' END AS DocType," + Environment.NewLine & _
                '" (SELECT RHI.Payment_Type  FROM TSPL_PAYMENT_HEADER RHI WHERE RHI.Payment_No =TSPL_PAYMENT_HEADER.Applied_Payment) AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_PAYMENT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_PAYMENT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_PAYMENT_HEADER.Vendor_Code ,'') AS [Child Cust Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name ,'') As [Child Cust Name], '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],CONVERT(VARCHAR,TSPL_PAYMENT_HEADER.Payment_Date,103) AS DocDate,TSPL_PAYMENT_HEADER.Payment_No   AS RefDocNo,CONVERT(VARCHAR, TSPL_PAYMENT_HEADER.Payment_Date   ,103) AS RefDocDate,CASE WHEN TSPL_PAYMENT_HEADER.Payment_Type ='RC' THEN 'Receipt' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='PV' THEN 'Advance' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='OA' THEN 'On Account' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='AD' THEN 'Apply Document' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='U' THEN 'UnApplied' WHEN TSPL_PAYMENT_HEADER.Payment_Type ='RC' THEN 'Receipt' END  AS SubDocType,0 as DrAmt,TSPL_PAYMENT_DETAIL.Applied_Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                '" FROM TSPL_PAYMENT_HEADER" + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_PAYMENT_DETAIL.Payment_No    " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_PAYMENT_HEADER.Debit_Account ,'')),1,3))  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine & _
                '" WHERE  TSPL_PAYMENT_HEADER.Posted='1' AND  TSPL_PAYMENT_HEADER.Payment_Type  IN ('AD')  AND ISNULL(Applied_Payment ,'')<>'' " + Environment.NewLine & _
                '" UNION ALL" + Environment.NewLine & _
                '" -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine & _
                '" SELECT TSPL_BANK_REVERSE.Vendor_Code AS VenCode, TSPL_BANK_REVERSE.Vendor_Name  AS VenName,ISNULL(TSPL_BANK_REVERSE.Reason,'') AS  [Desp],RHM.Cheque_No AS ChequeNo ,RHM.Payment_No   AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,ISNULL(Route_No,'') AS Route_No,'' AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] , CONVERT(VARCHAR,RHM.Payment_Date,103) AS DocDate ,RHM.Payment_No  AS RefDocNo,CONVERT(VARCHAR, RHM.Payment_Date,103) AS RefDocDate,CASE WHEN RHM.Payment_Type='RC' THEN 'Receipt' ELSE '' END  AS SubDocType,0 AS  DrAmt,TSPL_BANK_REVERSE.Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                '" FROM TSPL_BANK_REVERSE " + Environment.NewLine & _
                '" LEFT OUTER JOIN  TSPL_PAYMENT_HEADER RHM ON TSPL_BANK_REVERSE.Document_No = RHM.Payment_No    " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3))  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_BANK_REVERSE.Vendor_Code   = TSPL_VENDOR_MASTER.Vendor_Code   " + Environment.NewLine & _
                '" WHERE TSPL_BANK_REVERSE.Source_Type='AP' AND TSPL_BANK_REVERSE.post='P' AND RHM.Payment_Type IN ('AV','OA') " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine & _
                '" SELECT TSPL_BANK_REVERSE.Vendor_Code AS VenCode, TSPL_BANK_REVERSE.Vendor_Name  AS VenName,ISNULL(TSPL_BANK_REVERSE.Reason,'') AS  [Desp],RHM.Cheque_No AS ChequeNo ,TSPL_PAYMENT_DETAIL.Document_No AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,ISNULL(TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ,'') AS [Order Number],NULL  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_VENDOR_MASTER.Vendor_Code ,'') AS [Child Cust Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name ,'') As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] ,CONVERT(VARCHAR, RHM.Payment_Date ,103)  AS DocDate ,TSPL_BANK_REVERSE.Reverse_Code AS RefDocNo,CONVERT(VARCHAR, TSPL_BANK_REVERSE.Reversal_Date ,103) AS RefDocDate,CASE WHEN RHM.Payment_Type ='R' THEN 'Receipt' ELSE '' END  AS SubDocType,0 AS  DrAmt,TSPL_BANK_REVERSE.Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_BANK_REVERSE  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_PAYMENT_HEADER RHM ON RHM.Payment_No  = TSPL_BANK_REVERSE.Document_No  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_PAYMENT_DETAIL  ON TSPL_PAYMENT_DETAIL.Payment_No  = RHM.Payment_No  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code  = RHM.Vendor_Code     " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Debit_Account ,'')),1,3))  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No = TSPL_PAYMENT_DETAIL.Document_No  " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_SD_SHIPMENT_HEAD.Document_Code  WHERE TSPL_BANK_REVERSE.Source_Type='AP' AND TSPL_BANK_REVERSE.post='P' AND RHM.Payment_Type  IN ('AD','PY','RC')" + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" --  VCGL DATA(FIRST QUERY) " + Environment.NewLine & _
                '" SELECT TSPL_VCGL_Head.VC_Code AS VenCode, TSPL_VCGL_Head.VC_Name AS VenName,ISNULL(TSPL_VCGL_Head.Remarks,'')  AS  [Desp] ,'' AS ChequeNo, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode,ISNULL(Vendor_Group_Code ,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp], CONVERT(varchar , TSPL_VCGL_Head.Document_Date,103) AS DocDate,'' AS RefDocNo,Null AS RefDocDate,''  As SubDocType,CASE WHEN  TSPL_VCGL_Head.Amount_Type='Cr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END  AS DrAmt ,CASE WHEN  TSPL_VCGL_Head.Amount_Type='Dr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END AS CrAmt,0 AS TransAmt,0 AS BalAmt  " + Environment.NewLine & _
                '" FROM TSPL_VCGL_Head " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VCGL_Head.VC_Code= TSPL_VENDOR_MASTER.Vendor_Code   " + Environment.NewLine & _
                '" WHERE TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " + Environment.NewLine & _
                '" UNION ALL " + Environment.NewLine & _
                '" -- VCGL DATA(SECOND QUERY) " + Environment.NewLine & _
                '" SELECT TSPL_VCGL_Detail.VCGL_Code AS CustCode, TSPL_VCGL_Detail.VCGL_Name AS CustName,ISNULL(TSPL_VCGL_Detail.Remarks,'')  AS  [Desp] ,'' AS ChequeNo , TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode,ISNULL(Vendor_Group_Code,'') AS Ven_Group_Code,'' AS Route_No,'' AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp], CONVERT(varchar, TSPL_VCGL_Head.Document_Date,103) AS DocDate, '' AS RefDocNo,NULL AS RefDocDate,''  As SubDocType, TSPL_VCGL_Detail.Dr_Amount AS DrAmt , TSPL_VCGL_Detail.Cr_Amount AS CrAmt ,0 AS TransAmt,0 AS BalAmt " + Environment.NewLine & _
                '" FROM TSPL_VCGL_Detail" + Environment.NewLine & _
                '" LEFT OUTER JOIN  TSPL_VCGL_Head ON  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VCGL_Detail.VCGL_Code= TSPL_VENDOR_MASTER.Vendor_Code  " + Environment.NewLine & _
                '" WHERE TSPL_VCGL_Head.Status=1 AND  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " + Environment.NewLine & _
                '" )xxx " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code =xxx.VenCode " + Environment.NewLine & _
                '" LEFT OUTER JOIN TSPL_VENDOR_GROUP ON TSPL_VENDOR_GROUP.Ven_Group_Code = XXX.Ven_Group_Code " + Environment.NewLine & _
                '" WHERE 1=1 " & strFIlterCheck & " AND CONVERT(date,DocDate ,103)  >='" + strFromDate + "' and CONVERT(date,DocDate ,103)  <='" + strToDate + "' "
                'If ChkSummary.Checked = True Then
                '    If rbtnDocWise.Checked = True Then
                '        DocQry += " GROUP BY DocNo "
                '    Else
                '        DocQry += " GROUP BY VenCode "
                '    End If
                'End If
                'DocQry += " )  "
                'If ChkSummary.Checked = True Then
                '    DocQry += " SELECT *, CrAmt - DrAmt AS Balance FROM CTETemp "
                'Else
                '    'DocQry += " SELECT *, SUM([Trans Amt]) Over (Partition By DocumentNo Order By RowNo) as CumAmt FROM CTETemp "
                '    DocQry += " SELECT  *, CASE WHEN RowNoNew=1 Then CumAmt END AS Balance FROM ( " + Environment.NewLine & _
                '              " SELECT *, SUM([Trans Amt]) Over (Partition By DocumentNo Order By RowNo) as CumAmt, ROW_NUMBER() over (PARTITION By DocumentNo ORDER By RowNo Desc) AS RowNoNew " + Environment.NewLine & _
                '              " FROM CTETemp ) ZZZ Order By VendorCode  ,DocumentNo , ZZZ.RowNo"
                'End If

                ''
                    dtMain = clsDBFuncationality.GetDataTable(DocQry)

            If dtMain.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data not found", Me.Text)
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
                Dim frmCRV As New frmCrystalReportViewer()
                If ChkSummary.Checked = True Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorTransListLandScapeSummary", "Vendor Transaction List (Summary)")
                Else
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtMain, "VendorTransListLandScape", "Vendor Transaction List")
                End If
                frmCRV = Nothing
            End If

                    RadPageView1.SelectedPage = RadPageViewPage2
                    RadGroupBox1.Enabled = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

        gv.Columns("VendorCode").IsVisible = True
        gv.Columns("VendorCode").Width = 50
        gv.Columns("VendorCode").HeaderText = "Vendor Code"

        gv.Columns("Vendor Name").IsVisible = True
        gv.Columns("Vendor Name").Width = 100
        gv.Columns("Vendor Name").HeaderText = "Vendor Name"

        If (rbtnVenWise.Checked = False AndAlso ChkSummary.Checked = True) OrElse (ChkSummary.Checked = False AndAlso rbtnDocWise.Checked = True) Then
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

            gv.Columns("Ven Group Code").IsVisible = True
            gv.Columns("Ven Group Code").Width = 100
            gv.Columns("Ven Group Code").HeaderText = "Ven Group Code"

            gv.Columns("Ref Doc Date").IsVisible = True
            gv.Columns("Ref Doc Date").HeaderText = "Ref Doc Date"
            gv.Columns("Ref Doc Date").Width = 120
            gv.Columns("Ref Doc Date").FormatString = "{0:dd/MMM/yyyy}"

            gv.Columns("Ref Doc No").IsVisible = True
            gv.Columns("Ref Doc No").Width = 250
            gv.Columns("Ref Doc No").HeaderText = "Ref Doc No"

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

            If ChkSummary.Checked = False Then
                gv.Columns("AppliedDocNo").IsVisible = True
                gv.Columns("AppliedDocNo").Width = 150
                gv.Columns("AppliedDocNo").HeaderText = "Applied Doc No"

                gv.Columns("AppliedDocType").IsVisible = True
                gv.Columns("AppliedDocType").Width = 150
                gv.Columns("AppliedDocType").HeaderText = "Applied Doc Type"
            End If

        End If
        If rbtnVenWise.Checked = True Then
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
        'If ChkSummary.Checked = True Then
        '    gv.GroupDescriptors.Clear()
        '    gv.GroupDescriptors.Add(New GridGroupByExpression("DocumentNo As [DocNo] format ""{0}: {1}"" group by DocumentNo"))
        '    'gv.ShowGroupPanel = True
        'End If
        gv.MasterTemplate.ExpandAllGroups()

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
            gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        FunReset()
    End Sub

    Private Sub FrmRptVendorTransList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag AndAlso btnPrint.Enabled Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso MyBase.isModifyFlag AndAlso BtnClose.Enabled Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag AndAlso btnRefresh.Enabled Then
            FunReset()
        End If
    End Sub

    Private Sub FrmRptVendorTransList_Load(sender As Object, e As EventArgs) Handles Me.Load
        dtpFromdate.Value = "01/Apr/2015"
        dtptodate.Value = clsCommon.GETSERVERDATE()

        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset all Parameters")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        'ButtonToolTip.SetToolTip(BtnQuickExport, "Quick Export")

        ChkSummary.Checked = False
        PnlSumm.Enabled = ChkSummary.Checked
    End Sub

    Private Sub txtVendorGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendorGroup._My_Click
        strQry = "select Ven_Group_Code as Code, Group_Desc as Name from TSPL_VENDOR_GROUP order by Ven_Group_Code "
        txtVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorGrpSelector@VendorLedger", strQry, "Code", "Name", txtVendorGroup.arrValueMember, txtVendorGroup.arrDispalyMember)
    End Sub
    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        strQry = "select TSPL_VENDOR_MASTER.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,TSPL_VENDOR_MASTER.Parent_Vendor_Code as [Parent Code],P1.Vendor_Name as [Parent Name]   from TSPL_VENDOR_MASTER  Left Outer Join TSPL_VENDOR_MASTER P1 on TSPL_VENDOR_MASTER.Parent_Vendor_Code =P1.Vendor_Code where  TSPL_VENDOR_MASTER.Status='N'  order by TSPL_VENDOR_MASTER.Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorSelector@VendorLedger", strQry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub
    Private Sub txtAccountSet__My_Click(sender As Object, e As EventArgs) Handles txtAccountSet._My_Click
        strQry = "select Acct_Set_Code as Code, Acct_Set_Desc as Description from TSPL_VENDOR_ACCOUNT_SET"
        txtAccountSet.arrValueMember = clsCommon.ShowMultipleSelectForm("AcSetSelector@VendorLedger", strQry, "Code", "Description", txtAccountSet.arrValueMember, txtAccountSet.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        strQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        strQry += " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationSelector@VendorLedger", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        GetReportGridID()
        If ChkSummary.Checked = False AndAlso rbtnVenWise.Checked = True Then
            clsCommon.MyMessageBoxShow(Me, "Please tick Summary option with Customer wise option", Me.Text)
            Exit Sub
        End If
        blnRefresh = True
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv
        Print()
        blnRefresh = False
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If ChkSummary.Checked = True Then
            VarID += "_S"
        End If

        If clsCommon.CompairString(ddlDocType.SelectedIndex, "0") = CompairStringResult.Equal Then
            VarID += "_L"
        ElseIf clsCommon.CompairString(ddlDocType.SelectedIndex, "1") = CompairStringResult.Equal Then
            VarID += "_D"
        ElseIf clsCommon.CompairString(ddlDocType.SelectedIndex, "2") = CompairStringResult.Equal Then
            VarID += "_C"
        ElseIf clsCommon.CompairString(ddlDocType.SelectedIndex, "3") = CompairStringResult.Equal Then
            VarID += "_N"
        End If
        gv.VarID = VarID

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print()
    End Sub

    Private Sub ChkSummary_CheckStateChanged(sender As Object, e As EventArgs) Handles ChkSummary.CheckStateChanged
        PnlSumm.Enabled = ChkSummary.Checked
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = ""
        If ChkSummary.Checked = False Then
            ReportID = MyBase.Form_ID
        ElseIf ChkSummary.Checked = True Then
            If rbtnVenWise.Checked = True Then
                ReportID = MyBase.Form_ID + "VW"
            ElseIf rbtnDocWise.Checked = True Then
                ReportID = MyBase.Form_ID + "DW"
            End If
        End If
        Return ReportID
    End Function

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.FrmRptVendorAgeingDetails & "'"))
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            If gv.Rows.Count <= 0 Then
                Throw New Exception("No data found for Export.")
            End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Vendor Transaction Report ", gv, arrHeader, Me.Text)
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            ' Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.FrmRptVendorAgeingDetails & "'"))

            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVendorGroup.arrDispalyMember))
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                arrHeader.Add("Account Set : " + clsCommon.GetMulcallStringWithComma(txtAccountSet.arrDispalyMember))
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If clsCommon.myLen(ddlDocType.Text) > 0 Then
                arrHeader.Add("Doc Type : " + ddlDocType.Text)
            End If

            If gv.Rows.Count <= 0 Then
                Throw New Exception("No data found for Export.")
            End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Vendor Transaction Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Ticket No-ERO/21/11/19-001127
    Private Sub ExcelGrid_Click(sender As Object, e As EventArgs) Handles ExcelGrid.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.FrmRptVendorAgeingDetails & "'"))
            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVendorGroup.arrDispalyMember))
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                arrHeader.Add("Account Set : " + clsCommon.GetMulcallStringWithComma(txtAccountSet.arrDispalyMember))
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If clsCommon.myLen(ddlDocType.Text) > 0 Then
                arrHeader.Add("Doc Type : " + ddlDocType.Text)
            End If

            If gv.Rows.Count <= 0 Then
                Throw New Exception("No data found for Export.")
            End If
           
            clsCommon.MyExportToExcelGrid("Vendor Transaction Report", gv, arrHeader, Me.Text, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDFGrid_Click(sender As Object, e As EventArgs) Handles PDFGrid.Click
        Try
            Dim FilePath As String = "C:\\ERPTempFolder\\" + Me.Text + clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss") + ".pdf"
            Dim pdfExporter As New ExportToPDF(gv)
            pdfExporter.Font = New System.Drawing.Font("Verdana", 6)
            pdfExporter.TableBorderThickness = 1
            pdfExporter.FitToPageWidth = True
            pdfExporter.ExportVisualSettings = True
            pdfExporter.ExportHierarchy = True
            pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport
            pdfExporter.PageTitle = "Vendor Transaction Report"
            pdfExporter.RunExport(FilePath)
            System.Diagnostics.Process.Start(FilePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptPaymentProcessReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim strDocumentCodeDetails As String = Nothing
    Dim strVSPCodeDetails As String = Nothing
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Function setFormId() As String
        Dim fromId As String = ""
        If rdbSummary.IsChecked = True Then
            fromId = MyBase.Form_ID + "_S"
        ElseIf rdbDetails.IsChecked = True OrElse clscommon.myLen(strDocumentCodeDetails) > 0 OrElse clscommon.myLen(strVSPCodeDetails) > 0 Then
            fromId = MyBase.Form_ID + "_D"
            'Else
            '    fromId = MyBase.Form_ID + "_N"
        End If
        Return fromId
    End Function

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Reset()
    End Sub
    Sub Reset()
        ' ToDate.Value = clsCommon.GETSERVERDATE()
        ' fromDate.Value = ToDate.Value.AddMonths(-1)
        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        btnBack.Visible = False
        txtVSP.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtDocumentNo.arrValueMember = Nothing

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        LoadData()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rdbSummary.IsChecked Then
            VarID += "_S"
        ElseIf rdbDetails.IsChecked Then
            VarID += "_D"
        End If
        Gv1.VarID = VarID
    End Sub

    Public Sub LoadData(Optional ByVal strDocumentNoForDetails As String = "", Optional ByVal strVSPCodeForDetails As String = "")
        Try
            PageSetupReport_ID = setFormId()
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim whr As String = " where 2=2 and TSPL_PAYMENT_PROCESS_HEAD.FarmType='PP' "
            Dim dt As New DataTable

            If rbFromDate.IsChecked = True Then
                whr += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) > = convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
            ElseIf rbToDate.IsChecked = True Then
                whr += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) > = convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
            ElseIf rbBothFromToDate.IsChecked = True Then
                whr += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) > = convert (date, '" + fromDate.Value + "',103)   and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
            End If

            If clsCommon.myLen(strVSPCodeForDetails) > 0 Then
                whr += " and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE in ('" + strVSPCodeForDetails + "')"
            Else
                If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                    whr += " and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")"
                End If
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If clsCommon.myLen(strDocumentNoForDetails) > 0 Then
                whr += " and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in ('" + strDocumentNoForDetails + "')"
            Else
                If txtDocumentNo.arrValueMember IsNot Nothing AndAlso txtDocumentNo.arrValueMember.Count > 0 Then
                    whr += " and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in (" + clsCommon.GetMulcallString(txtDocumentNo.arrValueMember) + ")"
                End If
            End If


            If ChkPosted.IsChecked = True Then
                whr += " and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 "
            ElseIf ChkUnPosted.IsChecked = True Then
                whr += " and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 0 "
            End If
            Dim PaymentEntryAmountMul As String = "0"
            If rdbDetails.IsChecked = True OrElse clsCommon.myLen(strDocumentNoForDetails) > 0 Then
                PaymentEntryAmountMul = "1"
            End If
            qry = " Select TSPL_PAYMENT_PROCESS_HEAD.Doc_No, convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date],Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader], 'Invoice' as [Doc Type], TSPL_PAYMENT_PROCESS_INVOICE.Doc_No as [Payment Process], TSPL_PAYMENT_PROCESS_INVOICE.VSP_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name] ,convert (varchar,TSPL_PAYMENT_PROCESS_INVOICE.Milk_Purchase_Invoice_Date,103) as [Document Date],TSPL_PAYMENT_PROCESS_INVOICE.Milk_Purchase_Invoice_No as [Document No] , 0 as Dr, TSPL_PAYMENT_PROCESS_INVOICE.Inv_Amount as [Cr] , 0 as [DrPE]
                    from TSPL_PAYMENT_PROCESS_INVOICE 
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_INVOICE.Doc_No 
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_INVOICE.VSP_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE 
                    " + whr + "

                    Union All
                    select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date],Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status] ,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader],  'Sale' as [Doc Type], TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_MCC_SALE.Customer_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name], convert (varchar, TSPL_PAYMENT_PROCESS_MCC_SALE.Shipment_Doc_Date,103) as [Document Date] , TSPL_PAYMENT_PROCESS_MCC_SALE.Shipment_Doc_No as [Document No] , TSPL_PAYMENT_PROCESS_MCC_SALE.Amount as Dr, 0 as Cr , 0 as [DrPE]
                    from TSPL_PAYMENT_PROCESS_MCC_SALE 
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_MCC_SALE.Customer_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE 
                    " + whr + " 

                    union all
                    select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date],Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader],  'Sale Return' as [Doc Type], TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Customer_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name], convert (varchar, TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Return_Doc_Date,103) as [Document Date] , TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Return_Doc_No as [Document No] , 0 as Dr, TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Amount as Cr , 0 as [DrPE]
                    from TSPL_PAYMENT_PROCESS_MCC_SALE_Return 
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Customer_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                    " + whr + "

                    union all
                    select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date],Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader],  'Item Issue' as [Doc Type], TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name] ,convert (varchar, TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Item_Issue_Doc_Date,103) as [Document Date] , TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Item_Issue_Doc_No as [Document No] ,  TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Amount  as Dr,0 as Cr  , 0 as [DrPE]
                    from TSPL_PAYMENT_PROCESS_ITEM_ISSUE 
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                    " + whr + "

                    union All
                    select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date],Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader],  'Item Issue Return' as [Doc Type], TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name], convert (varchar, TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_Date,103) as [Document Date] , TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No as [Document No] ,  0  as Dr,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount as Cr  , 0 as [DrPE]
                    from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN 
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                    " + whr + "

                    Union all
                    select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date],Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader],  'Deduction' as [Doc Type], TSPL_PAYMENT_PROCESS_DEDUCTION .Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_DEDUCTION .Vendor_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name], Null as [Document Date] , TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as [Document No] ,  TSPL_PAYMENT_PROCESS_DEDUCTION.Amount  as Dr,0 as Cr  , 0 as [DrPE] 
                    from TSPL_PAYMENT_PROCESS_DEDUCTION 
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_DEDUCTION .Vendor_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE 
                    " + whr + "

                    Union all
                    select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date] ,Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader],  'Credit Note' as [Doc Type], TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name], convert (varchar, TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_Date,103) as [Document Date] , TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No as [Document No] ,  0  as Dr,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Cr  , 0 as [DrPE]
                    from TSPL_PAYMENT_PROCESS_CREDIT_NOTE 
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                    " + whr + "

                    Union all
                    select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date], Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code], TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader], 'Advance Payment' as [Doc Type], TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Vendor_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name], convert (varchar, TSPL_PAYMENT_HEADER.Payment_Date,103) as [Document Date] , TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_No as [Document No] ,  TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_Amount  as Dr,0 as Cr , 0 as [DrPE]
                    from TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT  
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Doc_No 
                    left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Payment_No
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_ADVANCE_PAYMENT.Vendor_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE 
                    " + whr + "

                    Union all
                    select TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date],Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader],  'Asset Lost' as [Doc Type], TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name] ,convert (varchar, TSPL_PAYMENT_HEADER.Payment_Date,103) as [Document Date] , TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No as [Document No] ,  TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_Amount  as Dr,0 as Cr , 0 as [DrPE]
                    from TSPL_PAYMENT_PROCESS_ASSET_LOST
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_ASSET_LOST.Doc_No
                    left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_PROCESS_ASSET_LOST.Payment_No
                    left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE = TSPL_PAYMENT_PROCESS_ASSET_LOST.Vendor_CODE
                    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                    " + whr + "

                    Union all
                    SELECT TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Doc_Date] ,Convert (varchar,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) as [From Date] , convert (varchar, TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as [To Date] , case when  TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 then 'Posted' else 'Pending' end as [Status],TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC CODE Uploader],  'Payment Entry' as [Doc Type], TSPL_PAYMENT_PROCESS_DETAIL .Doc_No as [Payment Process] ,TSPL_PAYMENT_PROCESS_DETAIL .VSP_CODE as [VSP Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [VSP Name], convert (varchar, TSPL_PAYMENT_HEADER.Payment_Date,103) as [Document Date] , TSPL_PAYMENT_HEADER.Payment_No as [Document No] ,  TSPL_PAYMENT_HEADER.Payment_Amount * " + PaymentEntryAmountMul + "  as Dr,0 as Cr , TSPL_PAYMENT_HEADER.Payment_Amount as [DrPE] 
                    from TSPL_PAYMENT_HEADER
                    inner join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No =  REPLACE(TSPL_PAYMENT_HEADER.Entry_Desc, 'Against Bulk Payment Process. ', '') and TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                    left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE " + whr + " "

            If rdbSummary.IsChecked = True AndAlso clsCommon.myLen(strVSPCodeForDetails) <= 0 AndAlso clsCommon.myLen(strDocumentNoForDetails) <= 0 Then
                qry = " select max([MCC Code]) as [MCC Code], max(case when len (TSPL_GL_SEGMENT_CODE.Description) > 0 then TSPL_GL_SEGMENT_CODE.Description else  TSPL_LOCATION_MASTER.Location_Desc end) as [MCC Name],max([VLC CODE Uploader]) as [VLC CODE Uploader], [VSP Code],max([VSP Name]) as [VSP Name],max([From Date]) as [From Date] ,max([To Date]) as [To Date] ,max(Doc_Date) as [Document Date],'Payment Process' as [Doc Type],Doc_No as [Document No],max(Status) as [Doc Status] ,sum ( XXXFinal.dr) as Dr, sum (XXXFinal.cr ) as Cr , sum (XXXFinal.cr ) - sum ( XXXFinal.dr) as [Payable Amt] from ( 
                        " + qry + " 
                        ) XXXFinal 
                        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = XXXFinal.[MCC Code]
                        left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = XXXFinal.[MCC Code]
                        group by Doc_No , [VSP Code] order by convert (date, max([Document Date]),103), [Document No] asc    "
            ElseIf rdbDetails.IsChecked = True OrElse clsCommon.myLen(strVSPCodeForDetails) >= 0 OrElse clsCommon.myLen(strDocumentNoForDetails) >= 0 Then
                qry = " select XXXFinal.Doc_No ,XXXFinal.[Doc_Date] as [Doc_Date], XXXFinal.[From Date],XXXFinal.[To Date],XXXFinal.Status,XXXFinal.[VSP Code],XXXFinal.[VSP Name], XXXFinal.[Document Date] ,XXXFinal.[Doc Type] , XXXFinal.[Document No], XXXFinal.Dr, XXXFinal.Cr ,  XXXFinal.DrPE  from (
                      " + qry + "
                      ) XXXFinal order by  convert (date, Doc_Date,103), Doc_No, [VSP Code]  asc "
            End If



            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                If rdbDetails.IsChecked = True OrElse clsCommon.myLen(strDocumentNoForDetails) > 0 Then
                    Gv1.Columns("DrPE").IsVisible = False
                    Gv1.Columns("doc_No").HeaderText = "Payment Process" + Environment.NewLine + "Code"
                    Gv1.Columns("doc_date").HeaderText = "Payment Process" + Environment.NewLine + "Date"
                End If
                Gv1.Columns("From Date").HeaderText = "Payment Cycle" + Environment.NewLine + "From Date"
                Gv1.Columns("To Date").HeaderText = "Payment Cycle" + Environment.NewLine + "To Date"
                ' Gv1.Columns("Trans Type").IsVisible = False

                'Gv1.Columns("Document No").IsPinned = True
                'Gv1.Columns("Document No").PinPosition = PinnedColumnPosition.Right

                'Gv1.Columns("Dr").IsPinned = True
                'Gv1.Columns("Dr").PinPosition = PinnedColumnPosition.Right

                'Gv1.Columns("Cr").IsPinned = True
                'Gv1.Columns("Cr").PinPosition = PinnedColumnPosition.Right

                'If rdbSummary.IsChecked = True Then
                '    Gv1.Columns("Payable Amt").IsPinned = True
                '    Gv1.Columns("Payable Amt").PinPosition = PinnedColumnPosition.Right
                'End If

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                'Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"

                '================================
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim summaryDr As New GridViewSummaryItem()
                If rdbDetails.IsChecked = True OrElse clsCommon.myLen(strDocumentNoForDetails) > 0 Then
                    summaryDr.Name = "Dr"
                    summaryDr.AggregateExpression = "sum(Dr) - sum(DrPE)"
                    summaryRowItem.Add(summaryDr)
                Else
                    Dim itemDr As New GridViewSummaryItem("Dr", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(itemDr)
                End If

                Dim itemCr As New GridViewSummaryItem("Cr", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemCr)
                If rdbSummary.IsChecked = True Then
                    Dim itemPayableAmt As New GridViewSummaryItem("Payable Amt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(itemPayableAmt)
                End If
                '================================

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(setFormId()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtDocumentNo._My_Click

        Dim str As String = ""
        Dim qry As String = ""
        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMCCFinderInPaymentProcess, clsFixedParameterCode.ShowMCCFinderInPaymentProcess, Nothing)) = 1) = True Then
            qry = " select TSPL_PAYMENT_PROCESS_HEAD.Doc_No as [DocumentNo] ,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date as [Doc Date] ,TSPL_PAYMENT_PROCESS_HEAD.From_Date as [From Date] ,TSPL_PAYMENT_PROCESS_HEAD.To_Date as [To Date] ,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [Plant Code],TSPL_GL_SEGMENT_CODE.description as [Plant Name], isnull (TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected,'') as [MCC Code]  , isnull (TSPL_MCC_MASTER.MCC_NAME,'') as [MCC Name] ,TSPL_PAYMENT_PROCESS_HEAD.Created_By as [Created By] ,TSPL_PAYMENT_PROCESS_HEAD.Created_Date as [Created Date] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_By as [Modified By] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_Date as [Modified Date] ,TSPL_PAYMENT_PROCESS_HEAD.Comp_Code as [Comp Code] ,case when isnull(TSPL_PAYMENT_PROCESS_HEAD.isPosted,0)=0 then 'NO' else 'YES' end as [Posting Status] ,TSPL_PAYMENT_PROCESS_HEAD.Posting_Date as [Posting Date],TSPL_PAYMENT_PROCESS_HEAD.DocRefNoForUploader as [NEFT Uploader Ref No],PMode.Payment_Mode as [Payment Mode],PMode.Payable_Amount as [Payable Amount] " &
                  " From TSPL_PAYMENT_PROCESS_HEAD left outer join TSPL_GL_SEGMENT_CODE  on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code " &
                  " left join (select Doc_No as PP_Code,Max(Payment_Mode) as Payment_Mode,sum(Payable_Amount) as Payable_Amount  from TSPL_PAYMENT_PROCESS_DETAIL group by Doc_No) PMode on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=PMode.PP_Code " &
                  " left Outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected where FarmType='PP'  "
        Else
            qry = " select TSPL_PAYMENT_PROCESS_HEAD.Doc_No as [DocumentNo] ,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date as [Doc Date] ,TSPL_PAYMENT_PROCESS_HEAD.From_Date as [From Date] ,TSPL_PAYMENT_PROCESS_HEAD.To_Date as [To Date] ,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_GL_SEGMENT_CODE.description as [MCC Name] ,TSPL_PAYMENT_PROCESS_HEAD.Created_By as [Created By] ,TSPL_PAYMENT_PROCESS_HEAD.Created_Date as [Created Date] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_By as [Modified By] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_Date as [Modified Date] ,TSPL_PAYMENT_PROCESS_HEAD.Comp_Code as [Comp Code] ,case when isnull(TSPL_PAYMENT_PROCESS_HEAD.isPosted,0)=0 then 'NO' else 'YES' end as [Posting Status] ,TSPL_PAYMENT_PROCESS_HEAD.Posting_Date as [Posting Date],TSPL_PAYMENT_PROCESS_HEAD.DocRefNoForUploader as [NEFT Uploader Ref No],PMode.Payment_Mode as [Payment Mode],PMode.Payable_Amount as [Payable Amount] " &
                  " From TSPL_PAYMENT_PROCESS_HEAD left outer join TSPL_GL_SEGMENT_CODE  on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code " &
                  " left join (select Doc_No as PP_Code,Max(Payment_Mode) as Payment_Mode,sum(Payable_Amount) as Payable_Amount  from TSPL_PAYMENT_PROCESS_DETAIL group by Doc_No) PMode on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=PMode.PP_Code  where FarmType='PP' " &
                  "  "
        End If

        If rbFromDate.IsChecked = True Then
            qry += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) > = convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
        ElseIf rbToDate.IsChecked = True Then
            qry += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) > = convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
        ElseIf rbBothFromToDate.IsChecked = True Then
            qry += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) > = convert (date, '" + fromDate.Value + "',103)   and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
        End If

        txtDocumentNo.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentProcessReport@DocNo", qry, "DocumentNo", "DocumentNo", txtDocumentNo.arrValueMember, txtDocumentNo.arrDispalyMember)
    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Dim qry As String
        qry = "  select Cust_Code as 'Code' , Customer_Name as Name from TSPL_CUSTOMER_MASTER where Cust_Group_Code = 'VSP'  order by Cust_Code "
        txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentProcessReport@VSP", qry, "Code", "Name", txtVSP.arrValueMember, txtVSP.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Name],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country  from TSPL_Location_MASTER   where    Location_Type='Physical' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentProcessReport@LocFinder", qry, "LocationSegmentCode", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = setFormId()
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(setFormId(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBatchItemReport1 & "'"))
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVSP.arrDispalyMember IsNot Nothing AndAlso txtVSP.arrDispalyMember.Count > 0 Then
                arrHeader.Add("VSP : " + clsCommon.GetMulcallStringWithComma(txtVSP.arrDispalyMember))
            End If
            If txtDocumentNo.arrDispalyMember IsNot Nothing AndAlso txtDocumentNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document : " + clsCommon.GetMulcallStringWithComma(txtDocumentNo.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Payment Process Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Payment Process Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If rdbSummary.IsChecked = True AndAlso clsCommon.myLen(strDocumentCodeDetails) <= 0 Then
                strDocumentCodeDetails = clsCommon.myCstr(Gv1.Rows(e.RowIndex).Cells.Item("Document No").Value)
                strVSPCodeDetails = clsCommon.myCstr(Gv1.Rows(e.RowIndex).Cells.Item("VSP Code").Value)
                LoadData(strDocumentCodeDetails, strVSPCodeDetails)
                btnBack.Visible = True
                btnGo.Enabled = False
                Enabledisablecontrol(False)
                'strDocumentCodeDetails = ""
                'strVSPCodeDetails = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Dim strDocumentCodeDetails As String = ""
        'Dim strVSPCodeDetails As String = ""
        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        LoadData()
        btnBack.Visible = False
        btnGo.Enabled = True
        Enabledisablecontrol(True)
    End Sub

    Private Sub rbFromDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbFromDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Sub ControlEmpty()

        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        btnBack.Visible = False
        'txtVSP.arrValueMember = Nothing
        'txtVSP.arrDispalyMember = Nothing
        'txtLocation.arrValueMember = Nothing
        'txtLocation.arrDispalyMember = Nothing
        txtDocumentNo.arrValueMember = Nothing
        txtDocumentNo.arrDispalyMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rbToDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbToDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub rbBothFromToDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbBothFromToDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub rbNone_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbNone.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fromDate_ValueChanged(sender As Object, e As EventArgs) Handles fromDate.ValueChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub ToDate_ValueChanged(sender As Object, e As EventArgs) Handles ToDate.ValueChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try
    End Sub

    Public Sub Enabledisablecontrol(ByVal isEnable As Boolean)
        gbDateRangeApply.Enabled = isEnable
        RadGroupBox3.Enabled = isEnable
        RadGroupBox2.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
        txtLocation.Enabled = isEnable
        txtVSP.Enabled = isEnable
        txtDocumentNo.Enabled = isEnable
    End Sub

    Private Sub rdbSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbSummary.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub rdbDetails_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbDetails.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub chkBoth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBoth.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub ChkPosted_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkPosted.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub ChkUnPosted_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkUnPosted.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub
End Class

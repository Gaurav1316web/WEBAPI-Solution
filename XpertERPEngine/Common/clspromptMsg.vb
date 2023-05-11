Imports common
Imports System.Data.SqlClient
Public Class clsPendingDocsPopupHead
#Region "Variables"
    Public PromptCode As String = Nothing
    Public Remarks As String = Nothing
    Public Declare_Type As Integer = 0
    Public Pending_Count As Integer = 0
    Public Arr As List(Of clsPendingDocsPopupDetail) = Nothing
    Dim qry As String = Nothing
    Dim StrQuery As String = Nothing
    Dim dtAuthen As DataTable = Nothing
#End Region
    Public Sub New()

    End Sub
    'function to check user's posting rights for all screens...
    Public Sub Load_Authorisation(ByVal ProgramName As String)
        Try
            StrQuery = "select max(TSPL_GROUP_PROGRAM_MAPPING.Authorized_Flag) as Authorized_Flag from TSPL_GROUP_PROGRAM_MAPPING " & _
                    " inner join TSPL_Program_Master on TSPL_Program_Master.Program_Code=TSPL_GROUP_PROGRAM_MAPPING.Program_Code " & _
                     " where TSPL_Program_Master.Program_Code='" + ProgramName + "' and " & _
                     "TSPL_GROUP_PROGRAM_MAPPING.Group_Code in (select Group_Code  from TSPL_USER_GROUP_MAPPING where User_Code ='" + objCommonVar.CurrentUserCode + "') and TSPL_GROUP_PROGRAM_MAPPING.Authorized_Flag=1 "
            dtAuthen = New DataTable()
            dtAuthen = clsDBFuncationality.GetDataTable(StrQuery)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'individual modules and their screens to generate single query ...
    Private Sub Fill_PurchaseModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnPurchaseRequistion)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Purchase' As Module, 'Purchase Requisition' As Screen, TSPL_REQUISITION_HEAD.Requisition_Id  as [Doc Code] , Convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) As  [Doc Date] , " & _
                        "  Case When TSPL_REQUISITION_HEAD.Status = 1 Then 'Y' Else 'N' End As Status  , TSPL_REQUISITION_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.mbtnPurchaseRequistion & "' as Form_type    From TSPL_REQUISITION_HEAD   " & _
                         " where 2=2 and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_REQUISITION_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_REQUISITION_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnPurchaseOrder)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                        " Select 'Purchase' As Module, 'Purchase Order' As Screen, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No [Doc Code], TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date [Doc Date]," & _
                        " Case When TSPL_PURCHASE_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As [Status], TSPL_PURCHASE_ORDER_HEAD.Created_By as [Created By] ,'" & clsUserMgtCode.mbtnPurchaseOrder & "' as Form_type " & _
                        " From TSPL_PURCHASE_ORDER_HEAD    Where 1=1 and TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade = 0   and close_yn ='N' " & _
                         "  and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                            " and TSPL_PURCHASE_ORDER_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PURCHASE_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnGRN)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "    " & _
                        " Select 'Purchase' As Module, 'Gate Received Note' As Screen, TSPL_GRN_HEAD.GRN_No [Doc Code], TSPL_GRN_HEAD.GRN_Date [Doc Date], " & _
                        " Case When TSPL_GRN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_GRN_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.mbtnGRN & "' as Form_type  From TSPL_GRN_HEAD   " & _
                         " where 2=2 and convert(date,TSPL_GRN_HEAD.GRN_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_GRN_HEAD.GRN_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                            " and TSPL_GRN_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_GRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnMRN)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                        " Select 'Purchase' As Module, 'Material Received Note' As Screen, TSPL_MRN_HEAD.MRN_No [Doc Code], TSPL_MRN_HEAD.MRN_Date [Doc Date]," & _
                        " Case When TSPL_MRN_HEAD.Status = 1 Then 'Y' Else 'N' End As [Status], TSPL_MRN_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.mbtnMRN & "' as Form_type    From TSPL_MRN_HEAD   " & _
                         " where 2=2 and convert(date,TSPL_MRN_HEAD.MRN_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_MRN_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnSRN)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Purchase' As Module, 'Store Received Note' As Screen, TSPL_SRN_HEAD.SRN_No [Doc Code], TSPL_SRN_HEAD.SRN_Date [Doc Date]," & _
                        " Case When TSPL_SRN_HEAD.Status = 1 Then 'Y' Else 'N' End As [Status], TSPL_SRN_HEAD.Created_By [Created By],'" & clsUserMgtCode.mbtnSRN & "' as Form_type  From TSPL_SRN_HEAD    Where TSPL_SRN_HEAD.Document_Type = 'SRN'  " & _
                         "  and convert(date,TSPL_SRN_HEAD.SRN_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and TSPL_SRN_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnPurchaseInvoice)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                        " Select 'Purchase' As Module, 'Purchase Invoice' As Screen, TSPL_PI_HEAD.PI_No [Doc Code], TSPL_PI_HEAD.PI_Date [Doc Date], " & _
                        " Case When TSPL_PI_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_PI_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.mbtnPurchaseInvoice & "' as Form_type    From TSPL_PI_HEAD   " & _
                        " where 2=2 and convert(date,TSPL_PI_HEAD.PI_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_PI_HEAD.PI_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                         " and TSPL_PI_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PI_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnPurchaseReturn)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                        " Select 'Purchase' As Module, 'Purchase Return' As Screen, TSPL_PR_HEAD.PR_No [Doc Code], TSPL_PR_HEAD.PR_Date [Doc Date] , " & _
                        " Case When TSPL_PR_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_PR_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.mbtnPurchaseReturn & "' as Form_type    From TSPL_PR_HEAD   " & _
                         " where 2=2 and convert(date,TSPL_PR_HEAD.PR_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_PR_HEAD.PR_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_PR_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PR_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnRGP_NRGP_Rpt)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Purchase' As Module, 'RGP/NRGP' As Screen, TSPL_RGP_HEAD.RGP_No [Doc Code], TSPL_RGP_HEAD.RGP_Date [Doc Date], " & _
                        " Case When TSPL_RGP_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_RGP_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.mbtnRGP_NRGP_Rpt & "' as Form_type   From TSPL_RGP_HEAD   " & _
                         " where 2=2 and convert(date,TSPL_RGP_HEAD.RGP_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_RGP_HEAD.RGP_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_RGP_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_RGP_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.mbtnIssueReturn)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        "  Select 'Purchase' As Module, 'Issue/Return Entry' As Screen, TSPL_IssueReturn_HEAD.Doc_No [Doc Code], " & _
                        " TSPL_IssueReturn_HEAD.Doc_Date [Doc Date], Case When TSPL_IssueReturn_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_IssueReturn_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.mbtnIssueReturn & "' as Form_type   From TSPL_IssueReturn_HEAD" & _
                        " where 2=2 and convert(date,TSPL_IssueReturn_HEAD.Doc_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_IssueReturn_HEAD.Doc_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                         " and TSPL_IssueReturn_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_IssueReturn_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.ScrapSale)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
               "Select 'Purchase' As Module, 'ScrapSale' As Screen , TSPL_SCRAPSALE_HEAD.shipment_No as [Doc Code], TSPL_SCRAPSALE_HEAD.shipment_Date as [Doc Date], (case when TSPL_SCRAPSALE_HEAD.ispost = 1 Then 'Y' Else 'N' end) as  Status , TSPL_SCRAPSALE_HEAD.Created_By as [Created By] ,'" & clsUserMgtCode.ScrapSale & "' as Form_type  FROM TSPL_SCRAPSALE_HEAD " & _
                    " WHERE 1=1 and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) >= convert(date,'" + fromDate + "',103) and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) <= convert(date,'" + toDate + "',103) " & _
                    " and TSPL_SCRAPSALE_HEAD.ispost  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SCRAPSALE_HEAD.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_SalesModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        Load_Authorisation(clsUserMgtCode.FrmBookingEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                      " Select 'Product Sale' As Module, 'Booking' As Screen, TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code [Doc Code], " & _
                      " TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Date [Doc Date], Case When TSPL_BOOKING_MASTER_PRODUCTSALE.Status = 1 Then 'Y' Else 'N' End As Status, " & _
                      " TSPL_BOOKING_MASTER_PRODUCTSALE.Created_By [Created By] ,'" & clsUserMgtCode.FrmBookingEntry & "' as Form_type    From TSPL_BOOKING_MASTER_PRODUCTSALE  " & _
                      " where 2=2 and convert(date,TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                       " and TSPL_BOOKING_MASTER_PRODUCTSALE.Status  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_BOOKING_MASTER_PRODUCTSALE.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.frmSalesOrderProductSale)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        "  Select 'Product Sale' As Module, 'Sale order Product Sale' As Screen, TSPL_SD_SALES_ORDER_HEAD.Document_Code [Doc Code]," & _
                        " TSPL_SD_SALES_ORDER_HEAD.Document_Date [Doc Date], Case When TSPL_SD_SALES_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As status," & _
                        " TSPL_SD_SALES_ORDER_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmSalesOrderProductSale & "' as Form_type   From TSPL_SD_SALES_ORDER_HEAD    Where 1=1 and TSPL_SD_SALES_ORDER_HEAD.Trans_Type = 'PS'  " & _
                         "  and convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_SD_SALES_ORDER_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALES_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmDeliveryPrderProductSale)
        ' ----------------------------------------------------------------------------
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Product Sale' As Module,  'Delivery order Product Sale' As Screen, TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code [Doc Code]," & _
                        " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date [Doc Date], Case When TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posted = 1 Then 'Y' Else 'N' End As [Status]," & _
                        " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Created_By ,'" & clsUserMgtCode.frmDeliveryPrderProductSale & "' as Form_type   From TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE" & _
                         " where 2=2 and convert(date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.frmShipmentProductSale)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                        " Select 'Product Sale' As Module, 'Dispatch Product Sale' As Screen, TSPL_SD_SHIPMENT_HEAD.Document_Code [Doc Code]," & _
                        " TSPL_SD_SHIPMENT_HEAD.Document_Date [Doc Date], Case When TSPL_SD_SHIPMENT_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, " & _
                        " TSPL_SD_SHIPMENT_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmShipmentProductSale & "' as Form_type  From TSPL_SD_SHIPMENT_HEAD    Where TSPL_SD_SHIPMENT_HEAD.Trans_Type = 'PS'  " & _
                         "  and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                 " and TSPL_SD_SHIPMENT_HEAD.Status  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.FrmBookingEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Fresh Sale' As Module, 'Booking Fresh Sale' As Screen,TSPL_BOOKING_MATSER.Document_No [Doc Code], TSPL_BOOKING_MATSER.Document_Date [Doc Date], " & _
                        " Case When TSPL_BOOKING_MATSER.Posted = 1 Then 'Y' Else 'N' End As Status,TSPL_BOOKING_MATSER.Created_By [Created By] ,'" & clsUserMgtCode.FrmBookingEntry & "' as Form_type  From TSPL_BOOKING_MATSER   " & _
                          " where 2=2 and convert(date,TSPL_BOOKING_MATSER.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and TSPL_BOOKING_MATSER.Posted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_BOOKING_MATSER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.frmInvoiceFreshSale)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Fresh Sale' As Module, 'Invoice Fresh Sale' As Screen, TSPL_SD_SALE_INVOICE_HEAD.Document_Code [Doc Code]," & _
                        " TSPL_SD_SALE_INVOICE_HEAD.Document_Date [Doc Date], Case When TSPL_SD_SALE_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status," & _
                        " TSPL_SD_SALE_INVOICE_HEAD.Created_By [Created By], '" & clsUserMgtCode.frmInvoiceFreshSale & "' as Form_type  From TSPL_SD_SALE_INVOICE_HEAD    Where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'FS'   " & _
                         "  and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_SD_SALE_INVOICE_HEAD.Status  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
        Load_Authorisation(clsUserMgtCode.FrmDispatchBulkSale)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Bulk Sale' As Module, 'Bulk Dispatch' As Screen, TSPL_Dispatch_BulkSale.Document_No [Doc Code], " & _
                        " TSPL_Dispatch_BulkSale.Document_Date [Doc Date], Case When TSPL_Dispatch_BulkSale.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_Dispatch_BulkSale.Created_By [Created By] ,'" & clsUserMgtCode.FrmDispatchBulkSale & "' as Form_type  From TSPL_Dispatch_BulkSale" & _
                         " where 2=2 and convert(date,TSPL_Dispatch_BulkSale.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_Dispatch_BulkSale.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and TSPL_Dispatch_BulkSale.Posted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_Dispatch_BulkSale.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        ' ----------------------------------------------------------------------------
    End Sub

    Private Sub Fill_BulkModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.frmMilkSRN)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                       " Select 'Bulk Procurement' As Module, ' Milk SRN' As Screen, TSPL_MILK_SRN_HEAD.DOC_CODE [Doc Code], TSPL_MILK_SRN_HEAD.DOC_DATE [Doc Date], " & _
                       " Case When TSPL_MILK_SRN_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_SRN_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmMilkSRN & "' as From_Type  From TSPL_MILK_SRN_HEAD  " & _
                        " where 2=2 and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                         " and TSPL_MILK_SRN_HEAD.Posted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and  TSPL_MILK_SRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.frmBulkMilkPurchaseInvoice)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        "  Select 'Bulk Procurement' As Module, 'Milk Purchase Invoice' As Screen, TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE [Doc Code]," & _
                        " TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE [Doc Date], Case When TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted = 1 Then 'Y' Else 'N' End Status," & _
                        " TSPL_MILK_PURCHASE_INVOICE_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmBulkMilkPurchaseInvoice & "' as Form_type From TSPL_MILK_PURCHASE_INVOICE_HEAD  " & _
                         " where 2=2 and convert(date, TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.frmMilkTransferIn)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                        " Select 'Bulk Procurement' As Module, 'Milk Transfer In' As Screen, TSPL_MILK_TRANSFER_IN.Receipt_Challan_No [Doc Code], " & _
                        " TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date [Doc Date], Case When TSPL_MILK_TRANSFER_IN.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_TRANSFER_IN.Created_By [Created By],'" & clsUserMgtCode.frmMilkTransferIn & "' as Form_type  From TSPL_MILK_TRANSFER_IN " & _
                         " where 2=2 and convert(date, TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and TSPL_MILK_TRANSFER_IN.isPosted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MILK_TRANSFER_IN.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_MCCMilkProcurement(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.frmMilkSample)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                       "  Select 'MCC Milk Procurement' As Module, 'Milk Sample' As Screen, TSPL_MILK_SAMPLE_HEAD.DOC_CODE [Doc Code]," & _
                       " TSPL_MILK_SAMPLE_HEAD.DOC_DATE [Doc Date], Case When TSPL_MILK_SAMPLE_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_SAMPLE_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmMilkSample & "' as Form_type " & _
                       "  From TSPL_MILK_SAMPLE_HEAD" & _
                       " where 2=2 and convert(date, TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and  TSPL_MILK_SAMPLE_HEAD.Posted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MILK_SAMPLE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

        Load_Authorisation(clsUserMgtCode.MilkTruckSheet)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                         "  Select 'MCC Milk Procurement' As Module, 'Milk Truck Sheet' As Screen, Tspl_Milk_Truck_Sheet_Head.DOC_CODE [Doc Code]," & _
                         " Tspl_Milk_Truck_Sheet_Head.DOC_DATE [Doc Date], Case When Tspl_Milk_Truck_Sheet_Head.Posted = 1 Then 'Y' Else 'N' End As Status, Tspl_Milk_Truck_Sheet_Head.Created_By [Created By],'" & clsUserMgtCode.MilkTruckSheet & "' as Form_type " & _
                         " From Tspl_Milk_Truck_Sheet_Head   " & _
                           " where 2=2 and convert(date, Tspl_Milk_Truck_Sheet_Head.DOC_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date, Tspl_Milk_Truck_Sheet_Head.DOC_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                              " and  Tspl_Milk_Truck_Sheet_Head.Posted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and Tspl_Milk_Truck_Sheet_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.mbtnStoreAdjustment)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                           " Select 'MCC Milk Procurement' As Module, 'Tanker Dispatch' As Screen, TSPL_MCC_Dispatch_Challan.Chalan_NO [Doc Code]," & _
                            " TSPL_MCC_Dispatch_Challan.Dispatch_Date [Doc Date], Case When TSPL_MCC_Dispatch_Challan.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MCC_Dispatch_Challan.Created_By [Created By] ,'" & clsUserMgtCode.mbtnStoreAdjustment & "' as Form_type " & _
                            " From TSPL_MCC_Dispatch_Challan   " & _
                            " where 2=2 and convert(date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                             " and TSPL_MCC_Dispatch_Challan.isPosted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MCC_Dispatch_Challan.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmMCCDispatch)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "     " & _
                            " Select 'MCC Milk Procurement' As Module, 'Tanker Location Change' As Screen, TSPL_MCC_DISPATCH_TRANSFER.Doc_No [Doc Code]," & _
                            " TSPL_MCC_DISPATCH_TRANSFER.Doc_Date [Doc Date], Case When TSPL_MCC_DISPATCH_TRANSFER.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MCC_DISPATCH_TRANSFER.Created_By ,'" & clsUserMgtCode.frmMCCDispatch & "' as Form_type  " & _
                            "  From TSPL_MCC_DISPATCH_TRANSFER  " & _
                             " where 2=2 and convert(date, TSPL_MCC_DISPATCH_TRANSFER.Doc_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_MCC_DISPATCH_TRANSFER.Doc_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                              " and TSPL_MCC_DISPATCH_TRANSFER.isPosted  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MCC_DISPATCH_TRANSFER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmVSPAssetIssue)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                            " Select 'MCC Milk Procurement' As Module, 'VSP Asset Issue' As Screen, TSPL_VSPAsset_HEAD.Doc_No [Doc Code]," & _
                            " Convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103) [Doc Date], Case When TSPL_VSPAsset_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_VSPAsset_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmVSPAssetIssue & "' as Form_type  " & _
                            "  From TSPL_VSPAsset_HEAD  " & _
                             " where 2=2 and convert(date, TSPL_VSPAsset_HEAD.Doc_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_VSPAsset_HEAD.Doc_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                              " and TSPL_VSPAsset_HEAD.Status  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_VSPAsset_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmMCCMaterial)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                            "  Select 'MCC Milk Procurement' As Module, 'MCC Material Sale' As Screen, TSPL_SD_SHIPMENT_HEAD.Document_Code [Doc Code], " & _
                            " TSPL_SD_SHIPMENT_HEAD.Document_Date [Doc Date], Case When TSPL_SD_SHIPMENT_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SHIPMENT_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmMCCMaterial & "' as Form_type   " & _
                            " From TSPL_SD_SHIPMENT_HEAD    Where TSPL_SD_SHIPMENT_HEAD.Trans_Type = 'MCC'  " & _
                            "  and convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                             " and TSPL_SD_SHIPMENT_HEAD.Status  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmMCCMaterialSaleReturn)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                "  Select 'MCC Milk Procurement' As Module, 'MCC Material Sale Return' As Screen," & _
                " TSPL_SD_SALE_RETURN_HEAD.Document_Code [Doc Code], TSPL_SD_SALE_RETURN_HEAD.Document_Date [Doc Date], Case When TSPL_SD_SALE_RETURN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status," & _
                " TSPL_SD_SALE_RETURN_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmMCCMaterialSaleReturn & "' as Form_type   From TSPL_SD_SALE_RETURN_HEAD    Where TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'MCC'   " & _
                "  and convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                 " and TSPL_SD_SALE_RETURN_HEAD.Status  in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALE_RETURN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmVSPItemIssue)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                " Select 'MCC Milk Procurement' As Module, 'VSP Item Issue' As Screen, TSPL_VSPItem_HEAD.Doc_No [Doc Code], Convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) As [Doc Date]," & _
                " Case When TSPL_VSPItem_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_VSPItem_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmVSPItemIssue & "' as Form_type    From TSPL_VSPItem_HEAD " & _
                " where 2=2 and convert(date, TSPL_VSPItem_HEAD.Doc_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_VSPItem_HEAD.Doc_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                 " and TSPL_VSPItem_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_VSPItem_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_MaterialManagement(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.mbtnStoreAdjustment)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                       " Select 'Material Management' As Module, 'Store Adjustment' As Screen, TSPL_ADJUSTMENT_HEADER.Adjustment_No [Doc Code]," & _
                       " TSPL_ADJUSTMENT_HEADER.Adjustment_Date [Doc Date],  CASE WHEN TSPL_ADJUSTMENT_HEADER.Posted = 'N' THEN 'N' ELSE 'Y' END [Status], TSPL_ADJUSTMENT_HEADER.Created_By [Created By],'" & clsUserMgtCode.mbtnStoreAdjustment & "' as Form_type   From TSPL_ADJUSTMENT_HEADER   " & _
                        " where 2=2 and convert(date, TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_ADJUSTMENT_HEADER.Posted in ('N' ,'Y') "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_ADJUSTMENT_HEADER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.Transfer)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Material Management' As Module, 'Transfer' As Screen, TSPL_TRANSFER_ORDER_HEAD.Document_No [Doc Code], TSPL_TRANSFER_ORDER_HEAD.Document_Date [Doc Date]," & _
                        " Case When TSPL_TRANSFER_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_TRANSFER_ORDER_HEAD.Created_By [Created By]  ,'" & clsUserMgtCode.Transfer & "' as Form_type   From TSPL_TRANSFER_ORDER_HEAD  " & _
                         " where 2=2 and convert(date, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_TRANSFER_ORDER_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_TRANSFER_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_CSAModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.frmCSATransfer)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        "  Select 'CSA Sale' As Module, 'CSA Transfer' As Screen, TSPL_CSA_TRANSFER_HEAD.DOC_CODE [Doc Code], TSPL_CSA_TRANSFER_HEAD.Transfer_Date [Doc Date], " & _
                        " Case When TSPL_CSA_TRANSFER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_CSA_TRANSFER_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmCSATransfer & "' as Form_type    From TSPL_CSA_TRANSFER_HEAD  " & _
                         " where 2=2 and convert(date, TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and TSPL_CSA_TRANSFER_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_CSA_TRANSFER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmProductionPlanningDairy)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "    " & _
                        " Select 'CSA Sale' As Module, 'Sale Patti' As Screen, TSPL_SD_SALE_INVOICE_HEAD.Document_Code [Doc Code], TSPL_SD_SALE_INVOICE_HEAD.Document_Date [Doc Date]," & _
                        " Case When TSPL_SD_SALE_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_INVOICE_HEAD.Created_By [Created By]  ,'" & clsUserMgtCode.frmProductionPlanningDairy & "' as Form_type  From TSPL_SD_SALE_INVOICE_HEAD " & _
                        " Where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'CSA'  " & _
                         "  and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                            " and  TSPL_SD_SALE_INVOICE_HEAD.Status in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_ProcessProduction(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.frmProductionPlanningDairy)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                      " Select 'Process Production' As Module, 'Production Planning' As Screen," & _
                      " TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code [Doc Code], TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date [Doc Date], Case When TSPL_PP_PRODUCTION_PLAN_HEAD.Is_Post = 1 Then 'Y' Else 'N' End As Status," & _
                      " TSPL_PP_PRODUCTION_PLAN_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmProductionPlanningDairy & "' as Form_type   From TSPL_PP_PRODUCTION_PLAN_HEAD   " & _
                       " where 2=2 and convert(date, TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                        " and  TSPL_PP_PRODUCTION_PLAN_HEAD.Is_Post in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmBatchOrderDairy)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                        "  Select 'Process Production' As Module, 'Batch Order' As Screen," & _
                        " TSPL_PP_BATCH_ORDER_HEAD.Batch_Code [Doc Code], TSPL_PP_BATCH_ORDER_HEAD.Batch_Date [Doc Date], Case When TSPL_PP_BATCH_ORDER_HEAD.Is_Post = 1 Then 'Y' Else 'N' End As Status, " & _
                        " TSPL_PP_BATCH_ORDER_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmBatchOrderDairy & "' as Form_type    From TSPL_PP_BATCH_ORDER_HEAD    Where TSPL_PP_BATCH_ORDER_HEAD.Closed_YN = 0   " & _
                         "  and convert(date, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and  TSPL_PP_BATCH_ORDER_HEAD.Is_Post in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += "  and TSPL_PP_BATCH_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmProductionEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                        " Select 'Process Production' As Module, 'Production Issue Entry' As Screen, TSPL_PP_ISSUE_HEAD.Issue_Code [Doc Code], TSPL_PP_ISSUE_HEAD.Issue_Date [Doc Date]," & _
                        " Case When TSPL_PP_ISSUE_HEAD.Is_post = 1 Then 'Y' Else 'N' End As Status, TSPL_PP_ISSUE_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmProductionEntry & "' as Form_type    From TSPL_PP_ISSUE_HEAD    " & _
                         " where 2=2 and convert(date, TSPL_PP_ISSUE_HEAD.Issue_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_PP_ISSUE_HEAD.Issue_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and  TSPL_PP_ISSUE_HEAD.Is_post in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PP_ISSUE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmProcessProductionStandardization)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        "   Select 'Process Production' As Module, 'Production Standardization' As Screen, TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code [Doc Code], " & _
                        " TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date [Doc Date], Case When TSPL_PP_STANDARDIZATION_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_PP_STANDARDIZATION_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmProcessProductionStandardization & "' as Form_type  " & _
                        "  From TSPL_PP_STANDARDIZATION_HEAD  " & _
                        " where 2=2 and convert(date, TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                        " and  TSPL_PP_STANDARDIZATION_HEAD.Posted in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PP_STANDARDIZATION_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.ScrapSale)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                        " Select 'Process Production' As Module, 'Stage Process' As Screen, TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE [Doc Code], " & _
                        " TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE [Doc Date], Case When TSPL_PP_STAGE_PROCESS_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_PP_STAGE_PROCESS_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.ScrapSale & "' as Form_type  " & _
                        " From TSPL_PP_STAGE_PROCESS_HEAD" & _
                         " where 2=2 and convert(date, TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and  TSPL_PP_STAGE_PROCESS_HEAD.Posted in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PP_STAGE_PROCESS_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmProcessProductionStandardization)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        " Select 'Process Production' As Module, 'Production Entry' As Screen, TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE [Doc Code]," & _
                         " TSPL_PP_PRODUCTION_ENTRY.PROD_DATE [Doc Date], Case When TSPL_PP_PRODUCTION_ENTRY.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_PP_PRODUCTION_ENTRY.Created_By [Created By],'" & clsUserMgtCode.frmProcessProductionStandardization & "' as Form_type   " & _
                         " From TSPL_PP_PRODUCTION_ENTRY  " & _
                           " where 2=2 and convert(date, TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                             " and   TSPL_PP_PRODUCTION_ENTRY.POSTED in (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PP_PRODUCTION_ENTRY.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_GeneralLedger(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.journalEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                       " Select 'General Ledger' As Module, 'Journal Entry' As Screen, TSPL_JOURNAL_MASTER.Voucher_No [Doc Code], TSPL_JOURNAL_MASTER.Voucher_Date [Doc Date], " & _
                       " Case When TSPL_JOURNAL_MASTER.Authorized = 'A' Then 'Y' Else 'N' End As Authorized, TSPL_JOURNAL_MASTER.Created_By [Created By] ,'" + clsUserMgtCode.journalEntry + "' as Form_type   From TSPL_JOURNAL_MASTER " & _
                       " where 2=2 and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                         " and TSPL_JOURNAL_MASTER.Authorized IN ('A' ,'N') "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_JOURNAL_MASTER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.mbtnVCGLEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                        " Select 'General Ledger' As Module, 'VCGL' As Screen, TSPL_VCGL_Head.Document_No [Doc Code], TSPL_VCGL_Head.Document_Date [Doc Date], " & _
                        " Case When TSPL_VCGL_Head.Status = 1 Then 'Y' Else 'N' End As status, TSPL_VCGL_Head.Created_By [Created By],'" + clsUserMgtCode.mbtnVCGLEntry + "' as Form_type   From TSPL_VCGL_Head  " & _
                        " where 2=2 and convert(date,TSPL_VCGL_Head.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_VCGL_Head.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                         " and TSPL_VCGL_Head.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_VCGL_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_ExportSale(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.frmEXSalesQuotation)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
               " Select 'Export Sale' As Module, 'Export Sale Quotation' As Screen, TSPL_SD_SALES_Quotation_HEAD.Document_Code [Doc Code], " & _
               " TSPL_SD_SALES_Quotation_HEAD.Document_Date [Doc Date], Case When TSPL_SD_SALES_Quotation_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, " & _
               " TSPL_SD_SALES_Quotation_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmEXSalesQuotation & "' as Form_type  From TSPL_SD_SALES_Quotation_HEAD   " & _
               " where 2=2 and convert(date, TSPL_SD_SALES_Quotation_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALES_Quotation_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                 " and TSPL_SD_SALES_Quotation_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALES_Quotation_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmSalesOrderMT)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                "   Select 'Export Sale' As Module, 'Export Sales Order' As Screen, " & _
                " TSPL_SD_SALES_ORDER_HEAD.Document_Code [Doc Code], TSPL_SD_SALES_ORDER_HEAD.Document_Date [Doc Date], " & _
                " Case When TSPL_SD_SALES_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, " & _
                " TSPL_SD_SALES_ORDER_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmSalesOrderMT & "' as Form_type   From TSPL_SD_SALES_ORDER_HEAD    Where TSPL_SD_SALES_ORDER_HEAD.SalesOrder_Type = 'EX'   " & _
                "  and convert(date, TSPL_SD_SALES_ORDER_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and TSPL_SD_SALES_ORDER_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALES_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmEXPorformaInvoice)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                "  Select 'Export Sale' As Module, 'Export Proforma Invoice' As Screen, TSPL_EX_PI_HEAD.Document_Code [Doc Code], TSPL_EX_PI_HEAD.Document_Date [Doc Date], " & _
                " Case When TSPL_EX_PI_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_EX_PI_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmEXPorformaInvoice & "' as Form_type   From TSPL_EX_PI_HEAD    " & _
                " where 2=2 and convert(date, TSPL_EX_PI_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EX_PI_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                 " and TSPL_EX_PI_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EX_PI_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmEXCommercialInvoice)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                " Select 'Export Sale' As Module, 'Export Commercial Invoice & Packing List' As Screen, " & _
                " TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code [Doc Code], TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date [Doc Date]," & _
                 " Case When TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmEXCommercialInvoice & "' as Form_type " & _
                 " From TSPL_EX_COMMERCIAL_INVOICE_HEAD    Where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Type = 'Ex' " & _
                 " and convert(date,  TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmSalesInvoiceMT)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 "  Select 'Export Sale' As Module, 'Export Sales Invoice' As Screen, TSPL_SD_SALE_INVOICE_HEAD.Document_Code [Doc Code], TSPL_SD_SALE_INVOICE_HEAD.Document_Date [Doc Date], " & _
                 " Case When TSPL_SD_SALE_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_INVOICE_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmSalesInvoiceMT & "' as Form_type   From TSPL_SD_SALE_INVOICE_HEAD   " & _
                 " Where TSPL_SD_SALE_INVOICE_HEAD.Document_Type = 'EX' And TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'EXP'  " & _
                 " and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_SD_SALE_INVOICE_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmSalesReturnMT)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 "  Select 'Export Sale' As Module, " & _
                 " 'Export Sale Return' As Screen, TSPL_SD_SALE_RETURN_HEAD.Document_Code [Doc Code], TSPL_SD_SALE_RETURN_HEAD.Document_Date [Doc Date], " & _
                 " Case When TSPL_SD_SALE_RETURN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_RETURN_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmSalesReturnMT & "' as Form_type   From TSPL_SD_SALE_RETURN_HEAD   " & _
                 " Where TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'EXP' And TSPL_SD_SALE_RETURN_HEAD.Document_Type = 'EX'  " & _
                  " and convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_SD_SALE_RETURN_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALE_RETURN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_MerchantTradeModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.FrmPurchaseOrderMT)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 " Select 'Merchant Trade' As Module," & _
                 " 'Merchant Purchase Order' As Screen, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No [Doc Code], TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date [Doc Date]," & _
                " Case When TSPL_PURCHASE_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_PURCHASE_ORDER_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.FrmPurchaseOrderMT & "' as Form_type  From TSPL_PURCHASE_ORDER_HEAD  " & _
                " Where TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade = 1   " & _
                " and convert(date, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and TSPL_PURCHASE_ORDER_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PURCHASE_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmSalesOrderMT)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 " Select 'Merchant Trade' As Module, 'Merchant Sales Order' As Screen, " & _
                 " TSPL_SD_SALES_ORDER_HEAD.Document_Code [Doc Code], TSPL_SD_SALES_ORDER_HEAD.Document_Date [Doc Date], Case When TSPL_SD_SALES_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, " & _
                 " TSPL_SD_SALES_ORDER_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmSalesOrderMT & "' as Form_type  From TSPL_SD_SALES_ORDER_HEAD    Where TSPL_SD_SALES_ORDER_HEAD.SalesOrder_Type = 'MT'   " & _
                 " and convert(date, TSPL_SD_SALES_ORDER_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                 " and TSPL_SD_SALES_ORDER_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALES_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmProformaInvoiceMT)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                 " Select 'Merchant Trade' As Module, 'Merchant Proforma Invoice' As Screen, TSPL_EX_PI_HEAD.Document_Code [Doc Code], TSPL_EX_PI_HEAD.Document_Date [Doc Date], " & _
                 " Case When TSPL_EX_PI_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_EX_PI_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmProformaInvoiceMT & "' as Form_type   From TSPL_EX_PI_HEAD    " & _
                 " where 2=2 and convert(date, TSPL_EX_PI_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EX_PI_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and TSPL_EX_PI_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EX_PI_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmLCRequest)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 " Select 'Merchant Trade' As Module, 'LC Request' As Screen, TSPL_LC_REQUEST_MT.LCRequestNo [Doc Code], TSPL_LC_REQUEST_MT.LCRequest_Date [Doc Date], " & _
                 " Case When TSPL_LC_REQUEST_MT.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_LC_REQUEST_MT.Created_By [Created By] ,'" & clsUserMgtCode.FrmLCRequest & "' as Form_type   From TSPL_LC_REQUEST_MT    " & _
                 " where 2=2 and convert(date, TSPL_LC_REQUEST_MT.LCRequest_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_LC_REQUEST_MT.LCRequest_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_LC_REQUEST_MT.Posted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_LC_REQUEST_MT.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmLCCreation)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                 " Select 'Merchant Trade' As Module, 'LC Creation' As Screen, TSPL_LC_CREATION_MT.LCCreationNo [Doc Code], TSPL_LC_CREATION_MT.LCCreation_Date [Doc Date]," & _
                 " Case When TSPL_LC_CREATION_MT.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_LC_CREATION_MT.Created_By [Created By] ,'" & clsUserMgtCode.FrmLCCreation & "' as Form_type   From TSPL_LC_CREATION_MT   " & _
                 " where 2=2 and convert(date, TSPL_LC_CREATION_MT.LCCreation_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_LC_CREATION_MT.LCCreation_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_LC_CREATION_MT.Posted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_LC_CREATION_MT.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmDocumentAcceptance)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                  "   Select 'Merchant Trade' As Module, 'Document Acceptance' As Screen, TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo [Doc Code], " & _
                  " TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptance_Date [Doc Date], Case When TSPL_DOCUMENT_ACCEPTANCE_MT.Posted = 1 Then 'Y' Else 'N' End As Status," & _
                  " TSPL_DOCUMENT_ACCEPTANCE_MT.Created_By [Created By],'" & clsUserMgtCode.FrmDocumentAcceptance & "' as Form_type   From TSPL_DOCUMENT_ACCEPTANCE_MT " & _
                  " where 2=2 and convert(date, TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptance_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptance_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                    " and TSPL_DOCUMENT_ACCEPTANCE_MT.Posted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_DOCUMENT_ACCEPTANCE_MT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmFixedDeposit)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                  " Select 'Merchant Trade' As Module, 'Fixed Deposit' As Screen, " & _
                  " TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No [Doc Code], TSPL_FIXED_DEPOSIT_MASTER_MT.Document_Date [Doc Date], Case When TSPL_FIXED_DEPOSIT_MASTER_MT.Posted = 1 Then 'Y' Else 'N' End As Status, " & _
                  " TSPL_FIXED_DEPOSIT_MASTER_MT.Created_By [Created By]  ,'" & clsUserMgtCode.FrmFixedDeposit & "' as Form_type  From TSPL_FIXED_DEPOSIT_MASTER_MT  " & _
                   " where 2=2 and convert(date, TSPL_FIXED_DEPOSIT_MASTER_MT.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_FIXED_DEPOSIT_MASTER_MT.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                     " and TSPL_FIXED_DEPOSIT_MASTER_MT.Posted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_FIXED_DEPOSIT_MASTER_MT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmCommercialInvoiceMT)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                   " Select 'Merchant Trade' As Module, " & _
                   "   'Merchant Commercial Invoice & Packing List' As Screen, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code [Doc Code]," & _
                   " TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date [Doc Date], Case When TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status," & _
                   " TSPL_EX_COMMERCIAL_INVOICE_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmCommercialInvoiceMT & "' as Form_type   From TSPL_EX_COMMERCIAL_INVOICE_HEAD    Where TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Type = 'MT' " & _
                   "  and convert(date, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                      " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmInvoiceFreshSale)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                " Select 'Merchant Trade' As Module, 'Merchant SRN' As Screen, TSPL_SRN_HEAD.SRN_No [Doc Code], TSPL_SRN_HEAD.SRN_Date [Doc Date], " & _
                " Case When TSPL_SRN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SRN_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmInvoiceFreshSale & "' as Form_type   From TSPL_SRN_HEAD    Where TSPL_SRN_HEAD.Document_Type = 'MT'  " & _
                " and convert(date, TSPL_SRN_HEAD.SRN_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                    " and TSPL_SRN_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SRN_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmSalesInvoiceMT)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 " Select 'Merchant Trade' As Module, 'Merchant Sales Invoice' As Screen, TSPL_SD_SALE_INVOICE_HEAD.Document_Code [Doc Code], TSPL_SD_SALE_INVOICE_HEAD.Document_Date [Doc Date]," & _
                 " Case When TSPL_SD_SALE_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_INVOICE_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmSalesInvoiceMT & "' as Form_type   From TSPL_SD_SALE_INVOICE_HEAD   " & _
                 " Where TSPL_SD_SALE_INVOICE_HEAD.Document_Type = 'MT' And TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'EXP'   " & _
                 " and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_SD_SALE_INVOICE_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmDispatchMultipleFreshSale)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 " Select 'Merchant Trade' As Module," & _
                 " 'Merchant Sale Return' As Screen, TSPL_SD_SALE_RETURN_HEAD.Document_Code [Doc Code], TSPL_SD_SALE_RETURN_HEAD.Document_Date [Doc Date]," & _
                  " Case When TSPL_SD_SALE_RETURN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_RETURN_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmDispatchMultipleFreshSale & "' as Form_type   From TSPL_SD_SALE_RETURN_HEAD   " & _
                  " Where TSPL_SD_SALE_RETURN_HEAD.Document_Type = 'MT' And TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'EXP'  " & _
                  " and convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_SD_SALE_RETURN_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_SD_SALE_RETURN_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
    End Sub
    Private Sub Fill_MilkJobWorkModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.FrmMilkJobWork)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                " Select 'Milk Job Work' As Module, 'Milk RGP' As Screen, TSPL_MILK_RGP_HEAD.RGP_No [Doc Code], Convert(date,TSPL_MILK_RGP_HEAD.RGP_Date,103) As [Doc Date], " & _
                " Case When TSPL_MILK_RGP_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_RGP_HEAD.Created_By [Created By],'" & clsUserMgtCode.FrmMilkJobWork & "' as Form_type   From TSPL_MILK_RGP_HEAD   " & _
                " where 2=2 and convert(date, TSPL_MILK_RGP_HEAD.RGP_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MILK_RGP_HEAD.RGP_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and TSPL_MILK_RGP_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MILK_RGP_HEAD.Created_By   in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmMilkGateEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                " Select 'Milk Job Work' As Module, 'Milk Gate Entry' As Screen, TSPL_MILK_GATE_ENTRY_DETAILS.Gate_Entry_No [Doc Code], " & _
                " TSPL_MILK_GATE_ENTRY_DETAILS.Date_And_Time [Doc Date], Case When TSPL_MILK_GATE_ENTRY_DETAILS.isPosted = 1 Then 'Y' Else 'N' End As Status," & _
                " TSPL_MILK_GATE_ENTRY_DETAILS.Created_By  ,'" & clsUserMgtCode.FrmMilkGateEntry & "' as Form_type   From TSPL_MILK_GATE_ENTRY_DETAILS   " & _
                " where 2=2 and convert(date, TSPL_MILK_GATE_ENTRY_DETAILS.Date_And_Time,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MILK_GATE_ENTRY_DETAILS.Date_And_Time,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and  TSPL_MILK_GATE_ENTRY_DETAILS.isPosted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MILK_GATE_ENTRY_DETAILS.Created_By    in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

        Load_Authorisation(clsUserMgtCode.FrmMilkWeighment)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 "   Select 'Milk Job Work' As Module, " & _
                 " 'Milk Weighment' As Screen, TSPL_MILK_WEIGHMENT_DETAIL.Weighment_No [Doc Code], TSPL_MILK_WEIGHMENT_DETAIL.Weighment_date [Doc Date]," & _
                 " Case When TSPL_MILK_WEIGHMENT_DETAIL.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_WEIGHMENT_DETAIL.Created_By [Created By] ,'" & clsUserMgtCode.FrmMilkWeighment & "' as Form_type    From TSPL_MILK_WEIGHMENT_DETAIL  " & _
                  " where 2=2 and convert(date, TSPL_MILK_WEIGHMENT_DETAIL.Weighment_date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MILK_WEIGHMENT_DETAIL.Weighment_date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and  TSPL_MILK_WEIGHMENT_DETAIL.isPosted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MILK_WEIGHMENT_DETAIL.Created_By   in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmJobMilkQualityCheck)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                "  Select 'Milk Job Work' As Module, 'Milk Quality Check' As Screen, TSPL_MILK_QUALITY_CHECK.Weighment_No [Doc Code], TSPL_MILK_QUALITY_CHECK.Weighment_Date [Doc Date], " & _
                " Case When TSPL_MILK_QUALITY_CHECK.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_QUALITY_CHECK.Created_By [Created By] ,'" & clsUserMgtCode.FrmJobMilkQualityCheck & "' as Form_type    From TSPL_MILK_QUALITY_CHECK  " & _
                " where 2=2 and convert(date, TSPL_MILK_QUALITY_CHECK.Weighment_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MILK_QUALITY_CHECK.Weighment_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and  TSPL_MILK_QUALITY_CHECK.isPosted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and  TSPL_MILK_QUALITY_CHECK.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmMilkUnloading)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                "  Select 'Milk Job Work' As Module, 'Milk Unloading' As Screen, TSPL_JOB_MILK_UNLOADING.Unloading_No [Doc Code], TSPL_JOB_MILK_UNLOADING.Unloading_Date_Time [Doc Date], " & _
                " Case When TSPL_JOB_MILK_UNLOADING.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_JOB_MILK_UNLOADING.Created_By [Created By] ,'" & clsUserMgtCode.FrmMilkUnloading & "' as Form_type    From TSPL_JOB_MILK_UNLOADING   " & _
                " where 2=2 and convert(date, TSPL_JOB_MILK_UNLOADING.Unloading_Date_Time,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_JOB_MILK_UNLOADING.Unloading_Date_Time,103) <=convert(date,'" + toDate + "',103 )" & _
                 " and TSPL_JOB_MILK_UNLOADING.isPosted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_JOB_MILK_UNLOADING.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmJobMilkSRN)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                " Select 'Milk Job Work' As Module, 'Milk SRN' As Screen, TSPL_JOB_MILK_SRN.SRN_NO [Doc Code], TSPL_JOB_MILK_SRN.SRN_Date [Doc Date]," & _
                " Case When TSPL_JOB_MILK_SRN.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_JOB_MILK_SRN.Created_By [Created By] ,'" & clsUserMgtCode.FrmJobMilkSRN & "' as Form_type   From TSPL_JOB_MILK_SRN " & _
                " where 2=2 and convert(date, TSPL_JOB_MILK_SRN.SRN_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_JOB_MILK_SRN.SRN_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and TSPL_JOB_MILK_SRN.isPosted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_JOB_MILK_SRN.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

    End Sub
    Private Sub Fill_ServicesModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        '===============Added by Preeti===========================
        Load_Authorisation(clsUserMgtCode.frmComplaintDetailEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                " Select 'Service' As Module, 'Complaint Detail Entry' As Screen, TSPL_COMPLAINT_DETAIL.comp_id [Doc Code], " & _
                " TSPL_COMPLAINT_DETAIL.comp_date [Doc Date], Case When TSPL_COMPLAINT_DETAIL.post_status = '1' Then 'Y' Else 'N' End As Status," & _
                " TSPL_COMPLAINT_DETAIL.created_by [Created By] ,'" & clsUserMgtCode.frmComplaintDetailEntry & "' as Form_type  From TSPL_COMPLAINT_DETAIL  " & _
                 " where 2=2 and convert(date, TSPL_COMPLAINT_DETAIL.comp_date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_COMPLAINT_DETAIL.comp_date,103) <=convert(date,'" + toDate + "',103 )"
                ' and TSPL_COMPLAINT_DETAIL.post_status  = '0' "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_COMPLAINT_DETAIL.created_by in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmAssetDistatch)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 "  Select 'Service' As Module, 'Service Dispatch' As Screen, " & _
                 " TSPL_RGP_HEAD.RGP_No [Doc Code], TSPL_RGP_HEAD.RGP_Date [Doc Date], Case When TSPL_RGP_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_RGP_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmAssetDistatch & "' as Form_type  " & _
                 " From TSPL_RGP_HEAD    Where TSPL_RGP_HEAD.Doc_Type = 'Disp'  " & _
                 " and convert(date, TSPL_RGP_HEAD.RGP_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_RGP_HEAD.RGP_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                    " and TSPL_RGP_HEAD.Status  IN ( 0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_RGP_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmCartMaintenanceEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                " Select 'Service' As Module, 'Cart Maintenance Entry' As Screen, " & _
                " TSPL_CART_MAINTENANCE.DocNo [Doc Code], TSPL_CART_MAINTENANCE.Date [Doc Date], Case When TSPL_CART_MAINTENANCE.Status = '1' Then 'Y' Else 'N' End As Status, TSPL_CART_MAINTENANCE.created_by [Created By] ,'" & clsUserMgtCode.frmCartMaintenanceEntry & "' as Form_type " & _
                "  From TSPL_CART_MAINTENANCE   " & _
                " where 2=2 and convert(date, TSPL_CART_MAINTENANCE.Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_CART_MAINTENANCE.Date,103) <=convert(date,'" + toDate + "',103 )"
                ' " and TSPL_CART_MAINTENANCE.Status  = '0' "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_CART_MAINTENANCE.created_by in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

    End Sub
    Private Sub Fill_CommonServiceModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)

        Load_Authorisation(clsUserMgtCode.bankTransfer)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                      " Select 'Common Service' As Module, 'Contra Vouchers' As Screen, TSPL_BANK_TRANSFER.Transfer_No [Doc Code], TSPL_BANK_TRANSFER.Transfer_Date [Doc Date]," & _
                      " Case When TSPL_BANK_TRANSFER.Post = 'p' Then 'Y' Else 'N' End As Status, TSPL_BANK_TRANSFER.Created_By [Created By] ,'" & clsUserMgtCode.bankTransfer & "' as Form_type    From TSPL_BANK_TRANSFER   " & _
                       " where 2=2 and convert(date, TSPL_BANK_TRANSFER.Transfer_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date, TSPL_BANK_TRANSFER.Transfer_Date,103) <=convert(date,'" + toDate + "',103 )"
                ' " and  coalesce(Post,'')<>'P' "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_BANK_TRANSFER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

        Load_Authorisation(clsUserMgtCode.reverseTransaction)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                " SELECT 'Common Service' As Module, 'Reverse Transaction' As Screen,TSPL_BANK_REVERSE.Reverse_Code as [Doc Code]," & _
                " TSPL_BANK_REVERSE.Reversal_Date as [Doc Date], (CASE  WHEN TSPL_BANK_REVERSE.Post = 'P' THEN 'Y'  ELSE 'N' END) AS Status ,TSPL_BANK_REVERSE.Created_By as [Created By] ,'" & clsUserMgtCode.reverseTransaction & "' as Form_type" & _
                " FROM TSPL_BANK_REVERSE WHERE  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103) >= convert(date,'" + fromDate + "',103) and convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) <= convert(date,'" + toDate + "',103) "
                ' " and (TSPL_BANK_REVERSE.Post = 'P' OR TSPL_BANK_REVERSE.Post IS NULL)"

                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_BANK_REVERSE.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
                End If
                qry += " union all "

            End If
        End If
    End Sub
    Private Sub Fill_FixedAssetModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        Load_Authorisation(clsUserMgtCode.FAAcquisitionEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
              " Select 'Fixed Asset' As Module, 'Acquisition Entry' As Screen, TSPL_ACQUISITION_HEAD.Acquisition_Code [Doc Code], " & _
              " TSPL_ACQUISITION_HEAD.Acquisition_Date [Doc Date], Case When TSPL_ACQUISITION_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_ACQUISITION_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.FAAcquisitionEntry & "' as Form_type " & _
              "   From TSPL_ACQUISITION_HEAD " & _
               " where 2=2 and convert(date, TSPL_ACQUISITION_HEAD.Acquisition_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_ACQUISITION_HEAD.Acquisition_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and TSPL_ACQUISITION_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_ACQUISITION_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FADisposalEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 " Select 'Fixed Asset' As Module, 'Disposal Entry' As Screen, TSPL_ASSET_SCRAP_HEAD.Document_No [Doc Code], " & _
                 " TSPL_ASSET_SCRAP_HEAD.Document_Date [Doc Date], Case When TSPL_ASSET_SCRAP_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_ASSET_SCRAP_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.FADisposalEntry & "' as Form_type " & _
                 " From TSPL_ASSET_SCRAP_HEAD " & _
                  " where 2=2 and convert(date, TSPL_ASSET_SCRAP_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_ASSET_SCRAP_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                    " and TSPL_ASSET_SCRAP_HEAD.Status  IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_ASSET_SCRAP_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

        Load_Authorisation(clsUserMgtCode.frmIssueItemsToAsset)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 "  Select 'Fixed Asset' As Module, 'Issue Items to Assemble Assset' As Screen, TSPL_IssueItemToAssembledAsset_Head.Doc_No [Doc Code], " & _
                 " TSPL_IssueItemToAssembledAsset_Head.Doc_Date [Doc Date], Case When TSPL_IssueItemToAssembledAsset_Head.Status = 1 Then 'Y' Else 'N' End As Status," & _
                 " TSPL_IssueItemToAssembledAsset_Head.Created_By [Created By],'" & clsUserMgtCode.frmIssueItemsToAsset & "' as Form_type   From TSPL_IssueItemToAssembledAsset_Head   " & _
                  " where 2=2 and convert(date, TSPL_IssueItemToAssembledAsset_Head.Doc_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_IssueItemToAssembledAsset_Head.Doc_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                    " and TSPL_IssueItemToAssembledAsset_Head.Status  IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_IssueItemToAssembledAsset_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FAAssetWork)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                 "  Select 'Fixed Asset' As Module," & _
                 "  'Assset Work Expanses' As Screen, TSPL_ASSET_WORK_HEAD.Document_Code [Doc Code], TSPL_ASSET_WORK_HEAD.Document_Date [Doc Date]," & _
                  " Case When TSPL_ASSET_WORK_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_ASSET_WORK_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.FAAssetWork & "' as Form_type   From TSPL_ASSET_WORK_HEAD   " & _
                  " where 2=2 and convert(date, TSPL_ASSET_WORK_HEAD.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_ASSET_WORK_HEAD.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_ASSET_WORK_HEAD.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_ASSET_WORK_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

    End Sub
    Private Sub Fill_PayrollModule(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        Load_Authorisation(clsUserMgtCode.frmEmpSalary)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                        "   Select 'Payroll' As Module, 'Employee Salary' As Screen, TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE [Doc Code], TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM [Doc Date], " & _
                        " Case When TSPL_EMPLOYEE_SALARY.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_EMPLOYEE_SALARY.Created_By [Created By] ,'" & clsUserMgtCode.frmEmpSalary & "' as Form_type  From TSPL_EMPLOYEE_SALARY    " & _
                         " where 2=2 and convert(date, TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM,103) <=convert(date,'" + toDate + "',103 )" & _
                            " and TSPL_EMPLOYEE_SALARY.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EMPLOYEE_SALARY.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

        Load_Authorisation(clsUserMgtCode.frmLeaveApplication)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
            "   Select 'Payroll' As Module, 'Leave Application' As Screen, TSPL_LEAVE_APPLICATION.LVAPPLICATION_CODE [Doc Code], TSPL_LEAVE_APPLICATION.APPLICATION_DATE [Doc Date]," & _
            " Case When TSPL_LEAVE_APPLICATION.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_LEAVE_APPLICATION.Created_By [Created By],'" & clsUserMgtCode.frmLeaveApplication & "' as Form_type  From TSPL_LEAVE_APPLICATION    " & _
             " where 2=2 and convert(date, TSPL_LEAVE_APPLICATION.APPLICATION_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_LEAVE_APPLICATION.APPLICATION_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                " and TSPL_LEAVE_APPLICATION.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_LEAVE_APPLICATION.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

        Load_Authorisation(clsUserMgtCode.frmLeaveAdjustment)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                            "   Select 'Payroll' As Module, 'Leave Adjustment' As Screen, TSPL_LEAVE_ADJUSTMENT.LVADJUSTMENT_CODE [Doc Code], TSPL_LEAVE_ADJUSTMENT.ADJUSTMENT_DATE [Doc Date]," & _
                            " Case When TSPL_LEAVE_ADJUSTMENT.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_LEAVE_ADJUSTMENT.Created_By,'" & clsUserMgtCode.frmLeaveAdjustment & "'  as Form_type  From TSPL_LEAVE_ADJUSTMENT    " & _
                            " where 2=2 and convert(date, TSPL_LEAVE_ADJUSTMENT.ADJUSTMENT_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_LEAVE_ADJUSTMENT.ADJUSTMENT_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                              " and TSPL_LEAVE_ADJUSTMENT.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_LEAVE_ADJUSTMENT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmAllowanceDetails)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                            "   Select 'Payroll' As Module, 'Allowance Details' As Screen, TSPL_ALLOWANCE.ALLOWANCE_CODE [Doc Code], TSPL_ALLOWANCE.ALLOWANCE_DATE [Doc Date]," & _
                            " Case When TSPL_ALLOWANCE.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_ALLOWANCE.Created_By [Created By],'" & clsUserMgtCode.frmAllowanceDetails & "' as Form_type  From TSPL_ALLOWANCE   " & _
                             " where 2=2 and convert(date, TSPL_ALLOWANCE.ALLOWANCE_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_ALLOWANCE.ALLOWANCE_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                              " and TSPL_ALLOWANCE.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_ALLOWANCE.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "

            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmDeductionDetails)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                "   Select 'Payroll' As Module, 'Deduction Details' As Screen, TSPL_DEDUCTION.DEDUCTION_CODE [Doc Code], TSPL_DEDUCTION.DEDUCTION_DATE [Doc Date], " & _
                " Case When TSPL_DEDUCTION.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_DEDUCTION.Created_By [Created By] ,'" & clsUserMgtCode.frmDeductionDetails & "' as Form_type  From TSPL_DEDUCTION    " & _
                " where 2=2 and convert(date, TSPL_DEDUCTION.DEDUCTION_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_DEDUCTION.DEDUCTION_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                   " and TSPL_DEDUCTION.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_DEDUCTION.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If

        End If
        Load_Authorisation(clsUserMgtCode.frmReimbursementDetails)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                                "   Select 'Payroll' As Module, 'Employee Reimbursement' As Screen, TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_CODE [Doc Code], TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_DATE [Doc Date]," & _
                                " Case When TSPL_EMP_REIMBURSEMENT.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_EMP_REIMBURSEMENT.Created_By [Created By] ,'" & clsUserMgtCode.frmReimbursementDetails & "' as Form_type  From TSPL_EMP_REIMBURSEMENT   " & _
                                 " where 2=2 and convert(date, TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                                  " and TSPL_EMP_REIMBURSEMENT.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EMP_REIMBURSEMENT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

        Load_Authorisation(clsUserMgtCode.frmGenerateBonus)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                                 " Select 'Payroll' As Module, 'Generate Bonus' As Screen, TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE [Doc Code], TSPL_EMPLOYEE_BONUS.FROM_PAY_PERIOD_CODE [Doc Date]," & _
                                 " Case When TSPL_EMPLOYEE_BONUS.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_EMPLOYEE_BONUS.Created_By [Created By],'" & clsUserMgtCode.frmGenerateBonus & "' as Form_type    From TSPL_EMPLOYEE_BONUS  " & _
                                 " where 2=2 and convert(date, TSPL_EMPLOYEE_BONUS.FROM_PAY_PERIOD_CODE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EMPLOYEE_BONUS.FROM_PAY_PERIOD_CODE,103) <=convert(date,'" + toDate + "',103 )" & _
                                  " and TSPL_EMPLOYEE_BONUS.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EMPLOYEE_BONUS.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "

            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmLoanGeneration)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                          "   Select 'Payroll' As Module, 'Loan Generation' As Screen, TSPL_LOAN_GENERATION.LOAN_GENERATION_CODE [Doc Code], TSPL_LOAN_GENERATION.GENERATION_DATE [Doc Date], " & _
                          " Case When TSPL_LOAN_GENERATION.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_LOAN_GENERATION.Created_By [Created By] ,'" & clsUserMgtCode.frmLoanGeneration & "' as Form_type  From TSPL_LOAN_GENERATION  " & _
                          " where 2=2 and convert(date, TSPL_LOAN_GENERATION.GENERATION_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_LOAN_GENERATION.GENERATION_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                           " and TSPL_LOAN_GENERATION.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_LOAN_GENERATION.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmLoanAdjustment)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                "    Select 'Payroll' As Module, 'Loan Adjustment' As Screen, TSPL_LOAN_ADJUSTMENT.LOANADJUSTMENT_CODE [Doc Code], TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_DATE [Doc Date]," & _
                " Case When TSPL_LOAN_ADJUSTMENT.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_LOAN_ADJUSTMENT.Created_By [Created By],'" & clsUserMgtCode.frmLoanAdjustment & "' as Form_type   From TSPL_LOAN_ADJUSTMENT    " & _
                " where 2=2 and convert(date, TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                  " and TSPL_LOAN_ADJUSTMENT.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_LOAN_ADJUSTMENT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "

            End If

        End If
        Load_Authorisation(clsUserMgtCode.frmDailyAttendance)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                 " Select 'Payroll' As Module, 'Daily Attendance' As Screen, TSPL_DAILY_ATTENDANCE.DLA_CODE [Doc Code], TSPL_DAILY_ATTENDANCE.Created_Date [Doc Date], " & _
                 " Case When TSPL_DAILY_ATTENDANCE.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_DAILY_ATTENDANCE.Created_By [Created By] ,'" & clsUserMgtCode.frmDailyAttendance & "'  as Form_type From TSPL_DAILY_ATTENDANCE " & _
                   " where 2=2 and convert(date, TSPL_DAILY_ATTENDANCE.Created_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_DAILY_ATTENDANCE.Created_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                    " and TSPL_DAILY_ATTENDANCE.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_DAILY_ATTENDANCE.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmHourlyAttendance)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                            "  Select 'Payroll' As Module, 'Hourly Attendance' As Screen, TSPL_HOURLY_ATTENDANCE.DLA_CODE [Doc Code], TSPL_HOURLY_ATTENDANCE.Created_Date [Doc Date]," & _
                            " Case When TSPL_HOURLY_ATTENDANCE.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_HOURLY_ATTENDANCE.Created_By [Created By] ,'" & clsUserMgtCode.frmHourlyAttendance & "' as Form_type  From TSPL_HOURLY_ATTENDANCE  " & _
                             " where 2=2 and convert(date, TSPL_HOURLY_ATTENDANCE.Created_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_HOURLY_ATTENDANCE.Created_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                              " and TSPL_HOURLY_ATTENDANCE.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_HOURLY_ATTENDANCE.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmMonthlyAttendance)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                              "   Select 'Payroll' As Module, 'Monthly Attendance' As Screen, TSPL_MONTHLY_ATTENDANCE.MTA_CODE [Doc Code], TSPL_MONTHLY_ATTENDANCE.Created_Date [Doc Date]," & _
                              " Case When TSPL_MONTHLY_ATTENDANCE.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_MONTHLY_ATTENDANCE.Created_By [Created By],'" & clsUserMgtCode.frmMonthlyAttendance & "' as Form_type   From TSPL_MONTHLY_ATTENDANCE " & _
                              " where 2=2 and convert(date, TSPL_MONTHLY_ATTENDANCE.Created_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MONTHLY_ATTENDANCE.Created_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                                 " and TSPL_MONTHLY_ATTENDANCE.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MONTHLY_ATTENDANCE.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmAdjustmentVoucher)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                             "   Select 'Payroll' As Module, 'Employee Adjustment Voucher' As Screen, TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE [Doc Code], TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE [Doc Date], " & _
                             " Case When TSPL_ADJUSTMENT_VOUCHER.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_ADJUSTMENT_VOUCHER.Created_By [Created By],'" & clsUserMgtCode.frmAdjustmentVoucher & "' as Form_type   From TSPL_ADJUSTMENT_VOUCHER   " & _
                              " where 2=2 and convert(date, TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                               " and TSPL_ADJUSTMENT_VOUCHER.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_ADJUSTMENT_VOUCHER.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmSalaryGeneration)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += " " & _
                            " Select 'Payroll' As Module, 'Salary Generation' As Screen, TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE [Doc Code], TSPL_GENERATE_SALARY.GENERATE_DATE [Doc Date]," & _
                            " Case When TSPL_GENERATE_SALARY.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_GENERATE_SALARY.Created_By [Created By] ,'" & clsUserMgtCode.frmSalaryGeneration & "' as Form_type   From TSPL_GENERATE_SALARY   " & _
                             " where 2=2 and convert(date, TSPL_GENERATE_SALARY.GENERATE_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_GENERATE_SALARY.GENERATE_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                               " and TSPL_GENERATE_SALARY.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_GENERATE_SALARY.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmLTAClaim)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                             "   Select 'Payroll' As Module, 'LTA Claim' As Screen, TSPL_LTA_Claim_Head.LTA_CODE [Doc Code], TSPL_LTA_Claim_Head.Created_Date [Doc Date]," & _
                             " Case When TSPL_LTA_Claim_Head.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_LTA_Claim_Head.Created_By [Created By],'" & clsUserMgtCode.frmLTAClaim & "' as Form_type   From TSPL_LTA_Claim_Head  " & _
                              " where 2=2 and convert(date, TSPL_LTA_Claim_Head.Created_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_LTA_Claim_Head.Created_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                                " and TSPL_LTA_Claim_Head.Posted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_LTA_Claim_Head.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmMediclaimEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                            "  Select 'Payroll' As Module, 'Employee Mediclaim Entry' As Screen, TSPL_MEDICLAIM_HEAD.DOCUMENT_CODE [Doc Code], TSPL_MEDICLAIM_HEAD.DATE [Doc Date]," & _
                            " Case When TSPL_MEDICLAIM_HEAD.Status = 'Y' Then 'Y' Else 'N' End As Status, TSPL_MEDICLAIM_HEAD.Created_By [Created By],'" & clsUserMgtCode.frmMediclaimEntry & "' as Form_type   From TSPL_MEDICLAIM_HEAD    " & _
                            " where 2=2 and convert(date, TSPL_MEDICLAIM_HEAD.DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MEDICLAIM_HEAD.DATE,103) <=convert(date,'" + toDate + "',103 )"
                ' " and TSPL_MEDICLAIM_HEAD.Status = 'N' "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MEDICLAIM_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmFullAndFinalSettlement)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                             "   Select 'Payroll' As Module, 'Full And Final Settlement' As Screen, TSPL_FF_SETTLEMENT_HEAD.EMP_CODE [Doc Code], " & _
                             " TSPL_FF_SETTLEMENT_HEAD.Created_Date [Doc Date], Case When TSPL_FF_SETTLEMENT_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_FF_SETTLEMENT_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmFullAndFinalSettlement & "' as Form_type " & _
                             " From TSPL_FF_SETTLEMENT_HEAD  " & _
                             " where 2=2 and convert(date, TSPL_FF_SETTLEMENT_HEAD.Created_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_FF_SETTLEMENT_HEAD.Created_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                                 " and TSPL_FF_SETTLEMENT_HEAD.Posted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_FF_SETTLEMENT_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmEmployeeShiftChange)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                            " Select 'Payroll' As Module, 'Employee Shift Change' As Screen, TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.EMP_SHIFT_CODE [Doc Code], " & _
                            " TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.SHIFT_APP_DATE [Doc Date], Case When TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.POSTED = 1 Then 'Y' Else 'N' End As Status," & _
                                " TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.frmEmployeeShiftChange & "' as Form_type   From TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD   " & _
                                 " where 2=2 and convert(date, TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.SHIFT_APP_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.SHIFT_APP_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                                      " and TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmEmployeeTransfer)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                                "  Select 'Payroll' As Module, 'Employee Transfer' As Screen, " & _
                                " TSPL_EMPLOYEE_TRANSFER.Document_Code [Doc Code], TSPL_EMPLOYEE_TRANSFER.Document_Date [Doc Date], Case When TSPL_EMPLOYEE_TRANSFER.POSTED = 1 Then 'Y' Else 'N' End As Status," & _
                                " TSPL_EMPLOYEE_TRANSFER.Created_By [Created By],'" & clsUserMgtCode.FrmEmployeeTransfer & "' as Form_type   From TSPL_EMPLOYEE_TRANSFER  " & _
                                 " where 2=2 and convert(date, TSPL_EMPLOYEE_TRANSFER.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EMPLOYEE_TRANSFER.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                                    " and TSPL_EMPLOYEE_TRANSFER.POSTED IN (0,1) "

                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EMPLOYEE_TRANSFER.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.FrmEmpIncrement)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                                "   Select 'Payroll' As Module, 'Employee Increment' As Screen," & _
                                " TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE [Doc Code], CONVERT(DATE, TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE,103) [Doc Date], Case When TSPL_EMPLOYEE_INCREMENT_HEAD.POSTED = 1 Then 'Y' Else 'N' End As Status, " & _
                                " TSPL_EMPLOYEE_INCREMENT_HEAD.Created_By [Created By] ,'" & clsUserMgtCode.FrmEmpIncrement & "' as Form_type   From TSPL_EMPLOYEE_INCREMENT_HEAD" & _
                                " where 2=2 and convert(date, TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE,103) <=convert(date,'" + toDate + "',103 )" & _
                                  " and TSPL_EMPLOYEE_INCREMENT_HEAD.POSTED IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_EMPLOYEE_INCREMENT_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

    End Sub
    Private Sub Fill_Receivables(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        Load_Authorisation(clsUserMgtCode.mbtnARInvoiceEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                      " Select 'Receivable' As Module, 'AR Invoice' As Screen," & _
                      " TSPL_Customer_Invoice_Head.Document_No [Doc Code], CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) [Doc Code], Case When TSPL_Customer_Invoice_Head.Status = 0 Then 'N' Else 'Y' End Status, " & _
                      " TSPL_Customer_Invoice_Head.Created_By [Created By] ,'" & clsUserMgtCode.mbtnARInvoiceEntry & "' as Form_type   From TSPL_Customer_Invoice_Head  " & _
                       " where 2=2 and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                          " and TSPL_Customer_Invoice_Head.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += "  and TSPL_Customer_Invoice_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If

        Load_Authorisation(clsUserMgtCode.ReceiptEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
               " Select 'Receivable' As Module, 'Receipt Entry' As Screen," & _
               " TSPL_RECEIPT_HEADER.Receipt_No [Doc Code], TSPL_RECEIPT_HEADER.Receipt_Date [Doc Date], (CASE WHEN COALESCE(TSPL_RECEIPT_HEADER.Posted, '') = '' THEN 'N' WHEN COALESCE(TSPL_RECEIPT_HEADER.Posted, '') = 'N' THEN 'N'  ELSE 'Y' END) AS Status, TSPL_RECEIPT_HEADER.Created_By [Created By],'" & clsUserMgtCode.ReceiptEntry & "' as Form_type   From TSPL_RECEIPT_HEADER   " & _
               " where 2=2 and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <=convert(date,'" + toDate + "',103 )"
                ' " and TSPL_RECEIPT_HEADER.Posted='N' "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_RECEIPT_HEADER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If


        Load_Authorisation(clsUserMgtCode.ReceiptAdjustmentEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
                 "SELECT 'Receivable' As Module, 'Adjustment Entry' As Screen, TSPL_Receipt_Adjustment_Header.Adjustment_No as [Doc Code], TSPL_Receipt_Adjustment_Header.Adjustment_Date as [Doc Date],  (CASE WHEN COALESCE(TSPL_Receipt_Adjustment_Header.Is_Post, '') = '' THEN 'N'  WHEN COALESCE(TSPL_Receipt_Adjustment_Header.Is_Post, '') = 'N'  THEN 'N'  ELSE 'Y' END) AS Status , TSPL_Receipt_Adjustment_Header.Created_By as [Created By],'" & clsUserMgtCode.ReceiptAdjustmentEntry & "' as Form_type FROM TSPL_Receipt_Adjustment_Header Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_SALE_INVOICE_HEAD ON TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) >= convert(date,'" + fromDate + "',103) and convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date   ,103) <= convert(date,'" + toDate + "',103) "
                ' qry += " and (TSPL_Receipt_Adjustment_Header.Is_Post is NULL OR TSPL_Receipt_Adjustment_Header.Is_Post = 'N')"
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += "and TSPL_Receipt_Adjustment_Header.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
                End If
                qry += " union all "
            End If
        End If


    End Sub
    Private Sub Fill_Payables(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        Load_Authorisation(clsUserMgtCode.PaymentEntryNew)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                               " Select 'Payable' As Module, 'Payment Entry' As Screen, " & _
                               " TSPL_PAYMENT_HEADER.Payment_No [Doc Code], TSPL_PAYMENT_HEADER.Payment_Date [Doc Date], Case When TSPL_PAYMENT_HEADER.Posted = 1 Then 'Y' Else 'N' End As posted," & _
                               " TSPL_PAYMENT_HEADER.Created_By [Created By],'" & clsUserMgtCode.PaymentEntryNew & "' as Form_type   From TSPL_PAYMENT_HEADER  " & _
                                " where 2=2 and convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103) <=convert(date,'" + toDate + "',103 )" & _
                                " and TSPL_PAYMENT_HEADER.Posted IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_PAYMENT_HEADER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.mbtnAPInvoiceEntry)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
               "SELECT 'Payable' As Module, 'AP Invoice Entry' As Screen, TSPL_VENDOR_INVOICE_HEAD.Document_No [Doc Code] , " & _
                    " CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date,103) AS [Doc Date],(case when coalesce(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'') = '' then 'N' else 'Y' end)  AS Status,TSPL_VENDOR_INVOICE_HEAD.Created_By as [Created By],'" & clsUserMgtCode.mbtnAPInvoiceEntry & "' as Form_type " & _
                    "  FROM TSPL_VENDOR_INVOICE_HEAD " & _
                    " WHERE  convert(date,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date,103) >= convert(date,'" + fromDate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date ,103) <= convert(date,'" + toDate + "',103) "
                ' " And ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')= '' "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_VENDOR_INVOICE_HEAD.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
                End If
                qry += " union all  "
            End If
        End If

    End Sub
    Private Sub Fill_Farmer(ByVal user As ArrayList, ByVal fromDate As Date, ByVal toDate As Date)
        Load_Authorisation(clsUserMgtCode.frmMCCMaterialFarmer)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "   " & _
                               " Select 'Farmer Payment' As Module, 'MCC Material Sale Farmer' As Screen, TSPL_MCC_Sale_Farmer_Head.Document_Code [Doc Code], TSPL_MCC_Sale_Farmer_Head.Document_Date [Doc Date], Case When TSPL_MCC_Sale_Farmer_Head.Status = 1 Then 'Y' Else 'N' End As posted,                               TSPL_MCC_Sale_Farmer_Head.Created_By [Created By],'" & clsUserMgtCode.frmMCCMaterialFarmer & "' as Form_type   From TSPL_MCC_Sale_Farmer_Head  where 2=2 and convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103) <=convert(date,'" + toDate + "',103 ) and TSPL_MCC_Sale_Farmer_Head.Status IN (0,1) "
                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MCC_Sale_Farmer_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmMCCMaterialSaleReturnFarmer)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
               "Select 'Farmer Payment' As Module, 'MCC Material Sale Return Farmer' As Screen, TSPL_MCC_Sale_Return_Head_Farmer.Document_Code [Doc Code], TSPL_MCC_Sale_Return_Head_Farmer.Document_Date [Doc Date], Case When TSPL_MCC_Sale_Return_Head_Farmer.Status = 1 Then 'Y' Else 'N' End As posted,                                 TSPL_MCC_Sale_Return_Head_Farmer.Created_By [Created By],'" & clsUserMgtCode.frmMCCMaterialSaleReturnFarmer & "' as Form_type   From TSPL_MCC_Sale_Return_Head_Farmer  where 2=2 and convert(date,TSPL_MCC_Sale_Return_Head_Farmer.Document_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MCC_Sale_Return_Head_Farmer.Document_Date,103) <=convert(date,'" + toDate + "',103 ) and TSPL_MCC_Sale_Return_Head_Farmer.Status IN (0,1)  "

                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MCC_Sale_Return_Head_Farmer.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
                End If
                qry += " union all "
            End If
        End If
        Load_Authorisation(clsUserMgtCode.frmFarmerPaymentAdjustment)
        If dtAuthen.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                qry += "  " & _
               "Select 'Farmer Payment' As Module, 'Farmer Payment Adjustment' As Screen, TSPL_MP_Pay_Adj_Head.Adjustment_No [Doc Code], TSPL_MP_Pay_Adj_Head.Adjustment_Date [Doc Date], Case When TSPL_MP_Pay_Adj_Head.Is_Post = 'Y' Then 'Y' Else 'N' End As posted, TSPL_MP_Pay_Adj_Head.Created_By [Created By],'" & clsUserMgtCode.frmFarmerPaymentAdjustment & "' as Form_type   From TSPL_MP_Pay_Adj_Head                                 where 2=2 and convert(date,TSPL_MP_Pay_Adj_Head.Adjustment_Date,103)  >=convert(date,'" + fromDate + "',103) and convert(date,TSPL_MP_Pay_Adj_Head.Adjustment_Date,103) <=convert(date,'" + toDate + "',103 ) and TSPL_MP_Pay_Adj_Head.Is_Post IN (0,1)   "

                If user IsNot Nothing AndAlso user.Count > 0 Then
                    qry += " and TSPL_MP_Pay_Adj_Head.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
                End If
                qry += "  "
            End If
        End If
    End Sub
    'merging and making a single query to load all pending docs from everywhere ...
    Public Function GeneratePendingDocumentsQuery(ByVal fromdate As Date, ByVal Todate As Date, ByVal user As ArrayList, Optional ByVal hasUserAuthorization As Boolean = True) As String
        qry = ""
        qry = "  Select cast(0 as bit) as IsChecked , ff.Module as [Module] , ff.Screen as [Screen]  ,  ff.[Doc Code] as [Doc Code] , ff.[Doc Date] as [Doc Date] , ff.Status as [Status] , ff.Form_type as [Program Code] , ff.[Created By] from ( "

        Fill_PurchaseModule(user, fromdate, Todate)
        Fill_SalesModule(user, fromdate, Todate)
        Fill_BulkModule(user, fromdate, Todate)
        Fill_MCCMilkProcurement(user, fromdate, Todate)
        Fill_MaterialManagement(user, fromdate, Todate)
        Fill_CSAModule(user, fromdate, Todate)
        Fill_ProcessProduction(user, fromdate, Todate)
        Fill_GeneralLedger(user, fromdate, Todate)
        Fill_ExportSale(user, fromdate, Todate)
        Fill_MerchantTradeModule(user, fromdate, Todate)
        Fill_MilkJobWorkModule(user, fromdate, Todate)
        Fill_ServicesModule(user, fromdate, Todate)
        Fill_CommonServiceModule(user, fromdate, Todate)
        Fill_FixedAssetModule(user, fromdate, Todate)
        Fill_PayrollModule(user, fromdate, Todate)
        Fill_Receivables(user, fromdate, Todate)
        Fill_Payables(user, fromdate, Todate)
        Fill_Farmer(user, fromdate, Todate)


        qry += " ) as ff where 1=1 AND ff.status = 'N'  "
        If hasUserAuthorization = True Then
            qry += "AND ff.[Created By] in  ( '" + objCommonVar.CurrentUserCode + "') "
        End If
        'AND ff.[Doc Code] NOT IN (select PENDING_DOC_CODE  from TSPL_PROMPT_MSG_PENDING_DETAIL) "
        If qry.Contains("union all  ) as ff") Then
            qry = qry.Replace("union all  ) as ff", "  ) as ff")
        End If
        Return qry
    End Function
    Public Function SaveData(ByVal obj As clsPendingDocsPopupHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim sQuery As String = Nothing

        Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin
        If obj.PromptCode IsNot Nothing AndAlso clsCommon.myLen(obj.PromptCode) > 0 Then
            Dim qry As String = "delete from TSPL_PROMPT_MSG_PENDING_detail where Prompt_code='" + obj.PromptCode + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, Trans)
        End If
        If clsCommon.myLen(obj.PromptCode) <= 0 Then
            'sQuery = " Select count(*) from TSPL_PROMPT_MSG_PENDING_HEAD  "
            'Dim Count As Integer = clsDBFuncationality.getSingleValue(sQuery, Trans)

            'sQuery = " Select coalesce(max(Prompt_code),'') Prompt_code from TSPL_PROMPT_MSG_PENDING_HEAD "
            'Dim Prompt_code As String = clsDBFuncationality.getSingleValue(sQuery, Trans)

            'If Count = 0 Then
            '    obj.PromptCode = "PRMPT-001"
            'Else
            '    obj.PromptCode = clsCommon.incval(Prompt_code)
            'End If
            ' Ticket No :   KDI/07/05/18-000300 By Prabhakar

            Try
                Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count (*) from TSPL_DOCPREFIX_MASTER where Doc_Type = 'List of Pending Documents'", Trans))
                If count <= 0 Then
                    clsDBFuncationality.ExecuteNonQuery(" insert into TSPL_DOCPREFIX_MASTER (Doc_Type,Doc_Trans_Type,Location_Code,Doc_Prfeix,Fin_Year,Next_Number,Created_by,Created_Date,Modify_By,Modify_Date,Separator,Comp_Code, MinSizeofSeries,Year_Separator) values ('List of Pending Documents','','','PRMPT','2018',1,'admin','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy") + "','admin','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy") + "','/','" + objCommonVar.CurrentCompanyCode + "',8,'-') ", Trans)
                End If
            Catch ex As Exception

            End Try
            obj.PromptCode = clsERPFuncationality.GetNextCode(Trans, clsCommon.GETSERVERDATE(Trans), clsDocType.ListofPendingDocuments, "", "")
        End If
        Try
            Dim Col1 As New Hashtable
            clsCommon.AddColumnsForChange(Col1, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(Col1, "Is_Declare", obj.Declare_Type)
            clsCommon.AddColumnsForChange(Col1, "Pending_Count", obj.Pending_Count)
            clsCommon.AddColumnsForChange(Col1, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(Col1, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(Col1, "Prompt_code", obj.PromptCode)
                clsCommon.AddColumnsForChange(Col1, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(Col1, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(Col1, "TSPL_PROMPT_MSG_PENDING_HEAD", OMInsertOrUpdate.Insert, "", Trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(Col1, "TSPL_PROMPT_MSG_PENDING_HEAD", OMInsertOrUpdate.Update, "TSPL_PROMPT_MSG_PENDING_HEAD.Prompt_code='" + obj.PromptCode + "'", Trans)
            End If

            If isSaved Then
                Trans.Commit()
                clsPendingDocsPopupDetail.SaveDataInBulk(obj.PromptCode, obj.Arr)
            End If


        Catch ex As Exception
            Trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetPostStatus(ByVal fromdate As Date, ByVal Todate As Date, ByVal user As ArrayList, ByVal documentCode As String, Optional ByVal hasUserAuthorization As Boolean = True) As String
        Dim query As String = ""

        Dim obj As clsPendingDocsPopupHead = New clsPendingDocsPopupHead()
        query = obj.GeneratePendingDocumentsQuery(fromdate, Todate, user, hasUserAuthorization)

        If query.Contains("AND ff.status = 'N'") Then
            query = query.Replace("AND ff.status = 'N'", "")
            query += " AND ff.[Doc Code]  IN  ('" + documentCode + "') "
        End If

        Return query
    End Function
End Class
Public Class clsPendingDocsPopupDetail
#Region "Variables"
    Public PromptCode As String = Nothing
    Public SNo As Integer = 0
    Public ProgramCode As String = Nothing
    Public Doc_Code As String = Nothing
    Public LAST_STATUS As String = Nothing

#End Region
    Public Shared Function SaveDataInBulk(ByVal promptNo As String, ByVal Arr As List(Of clsPendingDocsPopupDetail)) As Boolean
        Dim dtPendingDocs As DataTable = Nothing
        Try
            ' Runtime datatable creation ---
            dtPendingDocs = New DataTable("TSPL_PROMPT_MSG_PENDING_DETAIL")
            dtPendingDocs.Columns.Add("Prompt_code", GetType(String))
            dtPendingDocs.Columns.Add("ProgramCode", GetType(String))
            dtPendingDocs.Columns.Add("PENDING_DOC_CODE", GetType(String))
            dtPendingDocs.Columns.Add("LAST_STATUS", GetType(String))

            ' Saving values ---
            If promptNo IsNot Nothing AndAlso clsCommon.myLen(promptNo) > 0 Then
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    For Each obj As clsPendingDocsPopupDetail In Arr
                        Dim row1 As DataRow = dtPendingDocs.NewRow
                        row1("Prompt_code") = promptNo
                        row1("ProgramCode") = obj.ProgramCode
                        row1("PENDING_DOC_CODE") = obj.Doc_Code
                        row1("LAST_STATUS") = obj.LAST_STATUS
                        dtPendingDocs.Rows.Add(row1)
                    Next
                End If
            End If

            ' Columns Mapping ---
            Using Connectionsql As New SqlConnection(objCommonVar.ConnString)
                Using sqlTransaction As SqlTransaction = clsDBFuncationality.GetTransactin
                    Using sqlBulkCopy As New SqlBulkCopy(Connectionsql)
                        sqlBulkCopy.DestinationTableName = "TSPL_PROMPT_MSG_PENDING_DETAIL"
                        'sqlBulkCopy.ColumnMappings.Add("Prompt_code", "Prompt_code")
                        'sqlBulkCopy.ColumnMappings.Add("ProgramCode", "ProgramCode")
                        'sqlBulkCopy.ColumnMappings.Add("PENDING_DOC_CODE", "PENDING_DOC_CODE")
                        'sqlBulkCopy.ColumnMappings.Add("LAST_STATUS", "LAST_STATUS")
                        Try
                            If dtPendingDocs.Rows.Count > 0 Then
                                Connectionsql.Open()
                                sqlBulkCopy.WriteToServer(dtPendingDocs)
                                Connectionsql.Close()
                            End If
                        Catch
                            Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin
                            If promptNo IsNot Nothing AndAlso clsCommon.myLen(promptNo) > 0 Then
                                Dim qry As String = "delete from TSPL_PROMPT_MSG_PENDING_detail where Prompt_code='" + promptNo + "' "
                                clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                            End If
                            Return False
                        End Try
                    End Using

                End Using

            End Using
        Catch ex As Exception
            Try
                Dim Trans As SqlTransaction = clsDBFuncationality.GetTransactin
                If promptNo IsNot Nothing AndAlso clsCommon.myLen(promptNo) > 0 Then
                    Dim qry As String = "delete from TSPL_PROMPT_MSG_PENDING_detail where Prompt_code='" + promptNo + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, Trans)
                End If
            Catch ex1 As Exception
            End Try
            Return False
        End Try
        Return True
    End Function
End Class
Public Class clsPendingDeclarationsReport
    Public Shared Function Load_PendingDocs(ByVal fromdate As Date, ByVal Todate As Date, ByVal user As ArrayList, Optional documentNo As String = "") As String
        Dim qry As String = ""
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry = " SELECT   final.Module, final.Screen,  final.[Doc Code], final.[Doc Date], final.[Posting Date], TSPL_PROMPT_MSG_PENDING_DETAIL.PENDING_DOC_CODE, TSPL_PROMPT_MSG_PENDING_HEAD.Is_Declare, TSPL_PROMPT_MSG_PENDING_HEAD.Created_By as [Declared By], cast(TSPL_PROMPT_MSG_PENDING_HEAD.Created_Date as date) as [Declared Date], CASE WHEN final.Status = 'Y' THEN 'Posted' ELSE 'Pending' END Status, TSPL_PROMPT_MSG_PENDING_DETAIL.LAST_STATUS,  final.Created_By AS [Created By] FROM ("

        qry += " Select 'Payable' As Module, 'AP Invoice Entry' As Screen, TSPL_VENDOR_INVOICE_HEAD.Document_No as [Doc Code] , " & _
                " Convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) As [Doc Date], " & _
                " TSPL_VENDOR_INVOICE_HEAD.Posting_Date [Posting Date], " & _
                " (Case When Len(TSPL_VENDOR_INVOICE_HEAD.Posting_Date) Is Null Then 'N' Else 'Y' End) As Status," & _
                " TSPL_VENDOR_INVOICE_HEAD.Created_By    From TSPL_VENDOR_INVOICE_HEAD   " & _
                " where 1=1  AND TSPL_VENDOR_INVOICE_HEAD.Document_No IN (SELECT documents FROM @ListOfDocs)"
        'and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) " & _
        '               " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '               " and  TSPL_VENDOR_INVOICE_HEAD.Posting_Date Is Null  "

        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_VENDOR_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Payable' As Module, 'Payment Entry' As Screen, " & _
                " TSPL_PAYMENT_HEADER.Payment_No, TSPL_PAYMENT_HEADER.Payment_Date , TSPL_PAYMENT_HEADER.Payment_Post_Date, Case When TSPL_PAYMENT_HEADER.Posted = 1 Then 'Y' Else 'N' End As posted," & _
                " TSPL_PAYMENT_HEADER.Created_By    From TSPL_PAYMENT_HEADER  " & _
                 " where 1=1   AND TSPL_PAYMENT_HEADER.Payment_No IN (SELECT documents FROM @ListOfDocs) "
        'and convert(date,TSPL_PAYMENT_HEADER.Payment_Post_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_PAYMENT_HEADER.Payment_Post_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_PAYMENT_HEADER.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PAYMENT_HEADER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'New added by preeti gupta 
        qry += " UNION ALL " & _
               "SELECT 'Payable' As Module, 'AP Invoice Entry' As Screen, TSPL_VENDOR_INVOICE_HEAD.Document_No [Doc Code] , " & _
                    " CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date,103) AS [Doc Date] , Posting_Date , (case when coalesce(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'') = '' then 'N' else 'Y' end)  AS posted ,TSPL_VENDOR_INVOICE_HEAD.Created_By as [Created By] " & _
                    "  FROM TSPL_VENDOR_INVOICE_HEAD " & _
                    " where 1=1  AND TSPL_VENDOR_INVOICE_HEAD.Document_No IN (SELECT  documents FROM @ListOfDocs) "
        'AND  convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) >= convert(date,'" + fromdate + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date ,103) <= convert(date,'" + Todate + "',103) "

        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_VENDOR_INVOICE_HEAD.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ' New added by preeti gupta
        qry += " UNION ALL " & _
                " SELECT 'Receivable' As Module, 'Adjustment Entry' As Screen, TSPL_Receipt_Adjustment_Header.Adjustment_No as [Doc Code], TSPL_Receipt_Adjustment_Header.Adjustment_Date as [Doc Date],Post_Date as Posting_Date ,(CASE WHEN COALESCE(TSPL_Receipt_Adjustment_Header.Is_Post, '') = '' THEN 'N' ELSE 'Y'  END) AS posted , TSPL_Receipt_Adjustment_Header.Created_By as [Created By]  " & _
                " FROM TSPL_Receipt_Adjustment_Header Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_SALE_INVOICE_HEAD ON TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
                 " where 1=1  AND TSPL_Receipt_Adjustment_Header.Adjustment_No IN (SELECT documents FROM @ListOfDocs) "
        'and convert(date,TSPL_Receipt_Adjustment_Header.Post_Date ,103) >= convert(date,'" + fromdate + "',103) and convert(date,TSPL_Receipt_Adjustment_Header.Post_Date   ,103) <= convert(date,'" + Todate + "',103) "

        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += "and TSPL_Receipt_Adjustment_Header.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Receivable' As Module, 'AR Invoice' As Screen," & _
                " TSPL_Customer_Invoice_Head.Document_No, TSPL_Customer_Invoice_Head.Document_Date, Posting_Date, Case When TSPL_Customer_Invoice_Head.Status = 0 Then 'N' Else 'Y' End Status, " & _
                " TSPL_Customer_Invoice_Head.Created_By    From TSPL_Customer_Invoice_Head  " & _
                " where 1=1  AND TSPL_Customer_Invoice_Head.Document_No IN (SELECT documents FROM @ListOfDocs) "
        'and convert(date,TSPL_Customer_Invoice_Head.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_Customer_Invoice_Head.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '   " and TSPL_Customer_Invoice_Head.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += "  and TSPL_Customer_Invoice_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Receivable' As Module, 'Receipt Entry' As Screen," & _
                " TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Date, Receipt_Post_Date, TSPL_RECEIPT_HEADER.Posted, TSPL_RECEIPT_HEADER.Created_By    From TSPL_RECEIPT_HEADER   " & _
                " where 1=1  AND TSPL_RECEIPT_HEADER.Receipt_No IN (SELECT  documents FROM @ListOfDocs) "
        'and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_RECEIPT_HEADER.Posted in ('N','Y') "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_RECEIPT_HEADER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'General Ledger' As Module, 'Journal Entry' As Screen, TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, Posting_Date ," & _
                " Case When TSPL_JOURNAL_MASTER.Authorized = 'A' Then 'Y' Else 'N' End As Authorized, TSPL_JOURNAL_MASTER.Created_By    From TSPL_JOURNAL_MASTER " & _
                " where 1=1 AND TSPL_JOURNAL_MASTER.Voucher_No IN (SELECT  documents FROM @ListOfDocs) "
        'and convert(date,TSPL_JOURNAL_MASTER.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_JOURNAL_MASTER.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_JOURNAL_MASTER.Authorized in ('N','Y') "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_JOURNAL_MASTER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'General Ledger' As Module, 'VCGL' As Screen, TSPL_VCGL_Head.Document_No, TSPL_VCGL_Head.Document_Date,Posting_Date, " & _
                " Case When TSPL_VCGL_Head.Status = 1 Then 'Y' Else 'N' End As status, TSPL_VCGL_Head.Created_By    From TSPL_VCGL_Head  " & _
                " where 1=1 AND TSPL_VCGL_Head.Document_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_VCGL_Head.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_VCGL_Head.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '" and TSPL_VCGL_Head.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_VCGL_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All " & _
                " Select 'Product Sale' As Module, 'Booking' As Screen, TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code, " & _
                " TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Date, Posting_Date, Case When TSPL_BOOKING_MASTER_PRODUCTSALE.Status = 1 Then 'Y' Else 'N' End As Status, " & _
                " TSPL_BOOKING_MASTER_PRODUCTSALE.Created_By    From TSPL_BOOKING_MASTER_PRODUCTSALE  " & _
                " where 1=1 AND TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_BOOKING_MASTER_PRODUCTSALE.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_BOOKING_MASTER_PRODUCTSALE.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_BOOKING_MASTER_PRODUCTSALE.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_BOOKING_MASTER_PRODUCTSALE.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All  " & _
                "  Select 'Product Sale' As Module, 'Sale order Product Sale' As Screen, TSPL_SD_SALES_ORDER_HEAD.Document_Code," & _
                " TSPL_SD_SALES_ORDER_HEAD.Document_Date,Posting_Date, Case When TSPL_SD_SALES_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As status," & _
                " TSPL_SD_SALES_ORDER_HEAD.Created_By From TSPL_SD_SALES_ORDER_HEAD where 1=1 AND TSPL_SD_SALES_ORDER_HEAD.Trans_Type = 'PS' and TSPL_SD_SALES_ORDER_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)    "

        '                 "  and convert(date,TSPL_SD_SALES_ORDER_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALES_ORDER_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_SD_SALES_ORDER_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALES_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Product Sale' As Module,  'Delivery order Product Sale' As Screen, TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code," & _
                " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date, Posting_Date, Case When TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posted = 1 Then 'Y' Else 'N' End As Posted," & _
                " TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Created_By  From TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE" & _
                 " where 1=1 AND TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Product Sale' As Module, 'Dispatch Product Sale' As Screen, TSPL_SD_SHIPMENT_HEAD.Document_Code," & _
                " TSPL_SD_SHIPMENT_HEAD.Document_Date, COALESCE(Posting_Date,'') Posting_Date, Case When TSPL_SD_SHIPMENT_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, " & _
                " TSPL_SD_SHIPMENT_HEAD.Created_By From TSPL_SD_SHIPMENT_HEAD    where 1=1  AND TSPL_SD_SHIPMENT_HEAD.Trans_Type = 'PS' AND TSPL_SD_SHIPMENT_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        '                "  and convert(date,COALESCE(TSPL_SD_SHIPMENT_HEAD.Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,COALESCE(TSPL_SD_SHIPMENT_HEAD.Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '        " and TSPL_SD_SHIPMENT_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SHIPMENT_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Fresh Sale' As Module, 'Booking Fresh Sale' As Screen,TSPL_BOOKING_MATSER.Document_No, TSPL_BOOKING_MATSER.Document_Date, CONVERT(DATE,MODIFIED_DATE,103) Posting_Date ," & _
                " Case When TSPL_BOOKING_MATSER.Posted = 1 Then 'Y' Else 'N' End As Status,TSPL_BOOKING_MATSER.Created_By From TSPL_BOOKING_MATSER   " & _
                  " where 1=1 AND TSPL_BOOKING_MATSER.Document_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_BOOKING_MATSER.MODIFIED_DATE,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_BOOKING_MATSER.MODIFIED_DATE,103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and TSPL_BOOKING_MATSER.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_BOOKING_MATSER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Fresh Sale' As Module, 'Invoice Fresh Sale' As Screen, TSPL_SD_SALE_INVOICE_HEAD.Document_Code," & _
                " TSPL_SD_SALE_INVOICE_HEAD.Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Posting_Date, Case When TSPL_SD_SALE_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status," & _
                " TSPL_SD_SALE_INVOICE_HEAD.Created_By From TSPL_SD_SALE_INVOICE_HEAD where 1=1  AND TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'FS'  AND TSPL_SD_SALE_INVOICE_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        '                 "  and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_SD_SALE_INVOICE_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALE_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        qry += " Union All " & _
                " Select 'Purchase' As Module, 'Purchase Requisition' As Screen, TSPL_REQUISITION_HEAD.Requisition_Id, Convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) As Requisition_Date, Posting_Date, " & _
                "  Case When TSPL_REQUISITION_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_REQUISITION_HEAD.Created_By    From TSPL_REQUISITION_HEAD   " & _
                 " where 1=1 AND TSPL_REQUISITION_HEAD.Requisition_Id IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_REQUISITION_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_REQUISITION_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_REQUISITION_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_REQUISITION_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All " & _
                " Select 'Purchase' As Module, 'Purchase Order' As Screen, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, Posting_Date, " & _
                " Case When TSPL_PURCHASE_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_PURCHASE_ORDER_HEAD.Created_By  " & _
                " From TSPL_PURCHASE_ORDER_HEAD    where 1=1 AND TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade = 0   and close_yn ='N' " & _
        '                 "  and convert(date,TSPL_PURCHASE_ORDER_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                    " and TSPL_PURCHASE_ORDER_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PURCHASE_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
                " Select 'Purchase' As Module, 'Gate Received Note' As Screen, TSPL_GRN_HEAD.GRN_No, TSPL_GRN_HEAD.GRN_Date, Posting_Date , " & _
                " Case When TSPL_GRN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_GRN_HEAD.Created_By    From TSPL_GRN_HEAD   " & _
                 " where 1=1 AND TSPL_GRN_HEAD.GRN_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_GRN_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_GRN_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                    " and TSPL_GRN_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_GRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Purchase' As Module, 'Material Received Note' As Screen, TSPL_MRN_HEAD.MRN_No, TSPL_MRN_HEAD.MRN_Date, Posting_Date ," & _
                " Case When TSPL_MRN_HEAD.Status = 1 Then 'Y' Else 'N' End As staus, TSPL_MRN_HEAD.Created_By    From TSPL_MRN_HEAD   " & _
                 " where 1=1 AND TSPL_MRN_HEAD.MRN_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_MRN_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_MRN_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_MRN_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Purchase' As Module, 'Store Received Note' As Screen, TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date, Posting_Date ," & _
                " Case When TSPL_SRN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SRN_HEAD.Created_By    From TSPL_SRN_HEAD    where 1=1 AND TSPL_SRN_HEAD.SRN_No IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_SRN_HEAD.Document_Type = 'SRN'  " & _
        '                 "  and convert(date,TSPL_SRN_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SRN_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and TSPL_SRN_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Purchase' As Module, 'Purchase Invoice' As Screen, TSPL_PI_HEAD.PI_No, TSPL_PI_HEAD.PI_Date,Posting_Date , " & _
                " Case When TSPL_PI_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_PI_HEAD.Created_By    From TSPL_PI_HEAD   " & _
                " where 1=1 AND TSPL_PI_HEAD.PI_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_PI_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_PI_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_PI_HEAD.Status in (0,1)"
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PI_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Purchase' As Module, 'Purchase Return' As Screen, TSPL_PR_HEAD.PR_No, TSPL_PR_HEAD.PR_Date, Posting_Date , " & _
                " Case When TSPL_PR_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_PR_HEAD.Created_By    From TSPL_PR_HEAD   " & _
                 " where 1=1 AND TSPL_PR_HEAD.PR_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_PR_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_PR_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_PR_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PR_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Purchase' As Module, 'RGP/NRGP' As Screen, TSPL_RGP_HEAD.RGP_No, TSPL_RGP_HEAD.RGP_Date, Posting_Date, " & _
                " Case When TSPL_RGP_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_RGP_HEAD.Created_By    From TSPL_RGP_HEAD   " & _
                 " where 1=1 AND TSPL_RGP_HEAD.RGP_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_RGP_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_RGP_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_RGP_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_RGP_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                "  Select 'Purchase' As Module, 'Issue/Return Entry' As Screen, TSPL_IssueReturn_HEAD.Doc_No, " & _
                " TSPL_IssueReturn_HEAD.Doc_Date, Posting_Date , Case When TSPL_IssueReturn_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_IssueReturn_HEAD.Created_By  From TSPL_IssueReturn_HEAD" & _
                " where 1=1 AND TSPL_IssueReturn_HEAD.Doc_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_IssueReturn_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_IssueReturn_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_IssueReturn_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_IssueReturn_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Bulk Sale' As Module, 'Bulk Dispatch' As Screen, TSPL_Dispatch_BulkSale.Document_No, " & _
                " TSPL_Dispatch_BulkSale.Document_Date, Posting_Date, Case When TSPL_Dispatch_BulkSale.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_Dispatch_BulkSale.Created_By  From TSPL_Dispatch_BulkSale" & _
                 " where 1=1 AND TSPL_Dispatch_BulkSale.Document_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,TSPL_Dispatch_BulkSale.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_Dispatch_BulkSale.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and TSPL_Dispatch_BulkSale.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_Dispatch_BulkSale.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Bulk Procurement' As Module, ' Milk SRN' As Screen, TSPL_MILK_SRN_HEAD.DOC_CODE, TSPL_MILK_SRN_HEAD.DOC_DATE, Posting_Date, " & _
                " Case When TSPL_MILK_SRN_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_SRN_HEAD.Created_By    From TSPL_MILK_SRN_HEAD  " & _
                 " where 1=1 AND TSPL_MILK_SRN_HEAD.DOC_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MILK_SRN_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_MILK_SRN_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_MILK_SRN_HEAD.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and  TSPL_MILK_SRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                "  Select 'Bulk Procurement' As Module, 'Milk Purchase Invoice' As Screen, TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE," & _
                " TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE, Posting_Date , Case When TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted = 1 Then 'Y' Else 'N' End Status," & _
                " TSPL_MILK_PURCHASE_INVOICE_HEAD.Created_By From TSPL_MILK_PURCHASE_INVOICE_HEAD  " & _
                 " where 1=1 AND TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MILK_PURCHASE_INVOICE_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_MILK_PURCHASE_INVOICE_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Bulk Procurement' As Module, 'Milk Transfer In' As Screen, TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, " & _
                " TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date, Posting_Date, Case When TSPL_MILK_TRANSFER_IN.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_TRANSFER_IN.Created_By  From TSPL_MILK_TRANSFER_IN " & _
                 " where 1=1 AND TSPL_MILK_TRANSFER_IN.Receipt_Challan_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MILK_TRANSFER_IN.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_MILK_TRANSFER_IN.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_MILK_TRANSFER_IN.isPosted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MILK_TRANSFER_IN.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Material Management' As Module, 'Store Adjustment' As Screen, TSPL_ADJUSTMENT_HEADER.Adjustment_No," & _
                " TSPL_ADJUSTMENT_HEADER.Adjustment_Date, Posting_Date, TSPL_ADJUSTMENT_HEADER.Posted, TSPL_ADJUSTMENT_HEADER.Created_By    From TSPL_ADJUSTMENT_HEADER   " & _
                 " where 1=1 AND TSPL_ADJUSTMENT_HEADER.Adjustment_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_ADJUSTMENT_HEADER.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_ADJUSTMENT_HEADER.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_ADJUSTMENT_HEADER.Posted in ('N','Y') "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_ADJUSTMENT_HEADER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Material Management' As Module, 'Transfer' As Screen, TSPL_TRANSFER_ORDER_HEAD.Document_No, TSPL_TRANSFER_ORDER_HEAD.Document_Date, Posting_Date ," & _
                " Case When TSPL_TRANSFER_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_TRANSFER_ORDER_HEAD.Created_By    From TSPL_TRANSFER_ORDER_HEAD  " & _
                 " where 1=1 AND TSPL_TRANSFER_ORDER_HEAD.Document_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_TRANSFER_ORDER_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_TRANSFER_ORDER_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_TRANSFER_ORDER_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_TRANSFER_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                " Select 'Common Service' As Module, 'Contra Vouchers' As Screen, TSPL_BANK_TRANSFER.Transfer_No, TSPL_BANK_TRANSFER.Transfer_Date, Transfer_Posting_Date , " & _
                " Case When TSPL_BANK_TRANSFER.Post = 'p' Then 'Y' Else 'N' End As Status, TSPL_BANK_TRANSFER.Created_By    From TSPL_BANK_TRANSFER   " & _
                 " where 1=1 AND TSPL_BANK_TRANSFER.Transfer_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_BANK_TRANSFER.Transfer_Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_BANK_TRANSFER.Transfer_Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                    " and  coalesce(Post,'') <> 'P' "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_BANK_TRANSFER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'NEW ADDED BY PREETI GUPTA
        qry += " Union All  " & _
        " SELECT 'Common Service' As Module, 'Reverse Transaction' As Screen,TSPL_BANK_REVERSE.Reverse_Code as [Doc Code]," & _
        " TSPL_BANK_REVERSE.Reversal_Date as [Doc Date], Modify_Date AS Posting_Date, (CASE  WHEN TSPL_BANK_REVERSE.Post = 'P' THEN 'Y'  ELSE 'N' END) AS Status ,TSPL_BANK_REVERSE.Created_By as [Created By] " & _
        " FROM TSPL_BANK_REVERSE where 1=1 AND TSPL_BANK_REVERSE.Reverse_Code IN (SELECT documents FROM @ListOfDocs)  "
        'AND convert(date,TSPL_BANK_REVERSE.Modify_Date ,103) >= convert(date,'" + fromdate + "',103) and convert(date,TSPL_BANK_REVERSE.Modify_Date,103) <= convert(date,'" + Todate + "',103) "

        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_BANK_REVERSE.Created_By IN (" + clsCommon.GetMulcallString(user) + ")"
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                "  Select 'CSA Sale' As Module, 'CSA Transfer' As Screen, TSPL_CSA_TRANSFER_HEAD.DOC_CODE, TSPL_CSA_TRANSFER_HEAD.Transfer_Date, Posting_Date , " & _
                " Case When TSPL_CSA_TRANSFER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_CSA_TRANSFER_HEAD.Created_By    From TSPL_CSA_TRANSFER_HEAD  " & _
                 " where 1=1 AND TSPL_CSA_TRANSFER_HEAD.DOC_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_CSA_TRANSFER_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_CSA_TRANSFER_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and TSPL_CSA_TRANSFER_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_CSA_TRANSFER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
                " Select 'CSA Sale' As Module, 'Sale Patti' As Screen, TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, Posting_Date ," & _
                " Case When TSPL_SD_SALE_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_INVOICE_HEAD.Created_By    From TSPL_SD_SALE_INVOICE_HEAD " & _
                " Where 1 = 1 AND TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'CSA' AND TSPL_SD_SALE_INVOICE_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)"

        '                "  and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and  TSPL_SD_SALE_INVOICE_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALE_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All " & _
                " Select 'Process Production' As Module, 'Production Planning' As Screen," & _
                " TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code, TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date, Modified_Date as Posting_Date , Case When TSPL_PP_PRODUCTION_PLAN_HEAD.Is_Post = 1 Then 'Y' Else 'N' End As Status," & _
                " TSPL_PP_PRODUCTION_PLAN_HEAD.Created_By    From TSPL_PP_PRODUCTION_PLAN_HEAD   " & _
                 " where 1=1 AND TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_PP_PRODUCTION_PLAN_HEAD.Modified_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_PP_PRODUCTION_PLAN_HEAD.Modified_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and  TSPL_PP_PRODUCTION_PLAN_HEAD.Is_Post in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All" & _
                "  Select 'Process Production' As Module, 'Batch Order' As Screen," & _
                " TSPL_PP_BATCH_ORDER_HEAD.Batch_Code, TSPL_PP_BATCH_ORDER_HEAD.Batch_Date, Modified_Date as Posting_Date , Case When TSPL_PP_BATCH_ORDER_HEAD.Is_Post = 1 Then 'Y' Else 'N' End As Status, " & _
                " TSPL_PP_BATCH_ORDER_HEAD.Created_By    From TSPL_PP_BATCH_ORDER_HEAD where 1=1 AND TSPL_PP_BATCH_ORDER_HEAD.Batch_Code IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_PP_BATCH_ORDER_HEAD.Closed_YN = 0   " & _
        '                "  and convert(date, TSPL_PP_BATCH_ORDER_HEAD.Modified_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_PP_BATCH_ORDER_HEAD.Modified_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and  TSPL_PP_BATCH_ORDER_HEAD.Is_Post in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += "  and TSPL_PP_BATCH_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All" & _
                " Select 'Process Production' As Module, 'Production Issue Entry' As Screen, TSPL_PP_ISSUE_HEAD.Issue_Code, TSPL_PP_ISSUE_HEAD.Issue_Date, Modified_Date as Posting_Date, " & _
                " Case When TSPL_PP_ISSUE_HEAD.Is_post = 1 Then 'Y' Else 'N' End As Status, TSPL_PP_ISSUE_HEAD.Created_By    From TSPL_PP_ISSUE_HEAD    " & _
                 " where 1=1 AND TSPL_PP_ISSUE_HEAD.Issue_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_PP_ISSUE_HEAD.Modified_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_PP_ISSUE_HEAD.Modified_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and  TSPL_PP_ISSUE_HEAD.Is_post in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PP_ISSUE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                "   Select 'Process Production' As Module, 'Production Standardization' As Screen, TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code, " & _
                " TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date, Modified_Date as Posting_Date, Case When TSPL_PP_STANDARDIZATION_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_PP_STANDARDIZATION_HEAD.Created_By  " & _
                "  From TSPL_PP_STANDARDIZATION_HEAD  " & _
                " where 1=1 AND TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_PP_STANDARDIZATION_HEAD.Modified_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_PP_STANDARDIZATION_HEAD.Modified_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                " and  TSPL_PP_STANDARDIZATION_HEAD.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PP_STANDARDIZATION_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All" & _
                " Select 'Process Production' As Module, 'Stage Process' As Screen, TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE, " & _
                " TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE, Modified_Date as Posting_Date, Case When TSPL_PP_STAGE_PROCESS_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_PP_STAGE_PROCESS_HEAD.Created_By  " & _
                " From TSPL_PP_STAGE_PROCESS_HEAD" & _
                 " where 1=1 AND TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_PP_STAGE_PROCESS_HEAD.Modified_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_PP_STAGE_PROCESS_HEAD.Modified_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and  TSPL_PP_STAGE_PROCESS_HEAD.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PP_STAGE_PROCESS_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All" & _
                " Select 'Process Production' As Module, 'Production Entry' As Screen, TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE," & _
                 " TSPL_PP_PRODUCTION_ENTRY.PROD_DATE, POSTING_DATE, Case When TSPL_PP_PRODUCTION_ENTRY.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_PP_PRODUCTION_ENTRY.Created_By   " & _
                 " From TSPL_PP_PRODUCTION_ENTRY  " & _
                   " where 1=1 AND TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_PP_PRODUCTION_ENTRY.POSTING_DATE,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_PP_PRODUCTION_ENTRY.POSTING_DATE,103) <=convert(date,'" + Todate + "',103 )" & _
        '                     " and   TSPL_PP_PRODUCTION_ENTRY.POSTED in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PP_PRODUCTION_ENTRY.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All  " & _
                "  Select 'MCC Milk Procurement' As Module, 'Milk Sample' As Screen, TSPL_MILK_SAMPLE_HEAD.DOC_CODE," & _
                " TSPL_MILK_SAMPLE_HEAD.DOC_DATE, POSTING_DATE, Case When TSPL_MILK_SAMPLE_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_SAMPLE_HEAD.Created_By " & _
                "  From TSPL_MILK_SAMPLE_HEAD" & _
                " where 1=1 AND TSPL_MILK_SAMPLE_HEAD.DOC_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MILK_SAMPLE_HEAD.POSTING_DATE,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_MILK_SAMPLE_HEAD.POSTING_DATE,103) <=convert(date,'" + Todate + "',103 )" & _
        '                    " and  TSPL_MILK_SAMPLE_HEAD.Posted in (0,1)"
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MILK_SAMPLE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "   Union All" & _
                 "  Select 'MCC Milk Procurement' As Module, 'Milk Truck Sheet' As Screen, Tspl_Milk_Truck_Sheet_Head.DOC_CODE," & _
                 " Tspl_Milk_Truck_Sheet_Head.DOC_DATE, Posting_Date, Case When Tspl_Milk_Truck_Sheet_Head.Posted = 1 Then 'Y' Else 'N' End As Status, Tspl_Milk_Truck_Sheet_Head.Created_By   " & _
                 " From Tspl_Milk_Truck_Sheet_Head   " & _
                   " where 1=1 AND Tspl_Milk_Truck_Sheet_Head.DOC_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, Tspl_Milk_Truck_Sheet_Head.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, Tspl_Milk_Truck_Sheet_Head.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                      " and  Tspl_Milk_Truck_Sheet_Head.Posted in (0,1)"
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and Tspl_Milk_Truck_Sheet_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All  " & _
                   " Select 'MCC Milk Procurement' As Module, 'Tanker Dispatch' As Screen, TSPL_MCC_Dispatch_Challan.Chalan_NO," & _
                    " TSPL_MCC_Dispatch_Challan.Dispatch_Date, Posting_Date,  Case When TSPL_MCC_Dispatch_Challan.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MCC_Dispatch_Challan.Created_By  " & _
                    " From TSPL_MCC_Dispatch_Challan   " & _
                    " where 1=1 AND TSPL_MCC_Dispatch_Challan.Chalan_NO IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MCC_Dispatch_Challan.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_MCC_Dispatch_Challan.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                    " and TSPL_MCC_Dispatch_Challan.isPosted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MCC_Dispatch_Challan.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All   " & _
                    " Select 'MCC Milk Procurement' As Module, 'Tanker Location Change' As Screen, TSPL_MCC_DISPATCH_TRANSFER.Doc_No," & _
                    " TSPL_MCC_DISPATCH_TRANSFER.Doc_Date, Posting_Date,  Case When TSPL_MCC_DISPATCH_TRANSFER.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MCC_DISPATCH_TRANSFER.Created_By  " & _
                    "  From TSPL_MCC_DISPATCH_TRANSFER  " & _
                     " where 1=1 AND TSPL_MCC_DISPATCH_TRANSFER.Doc_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MCC_DISPATCH_TRANSFER.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_MCC_DISPATCH_TRANSFER.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                      " and TSPL_MCC_DISPATCH_TRANSFER.isPosted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MCC_DISPATCH_TRANSFER.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                    " Select 'MCC Milk Procurement' As Module, 'VSP Asset Issue' As Screen, TSPL_VSPAsset_HEAD.Doc_No," & _
                    " Convert(date,TSPL_VSPAsset_HEAD.Doc_Date,103), Posting_Date , Case When TSPL_VSPAsset_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_VSPAsset_HEAD.Created_By  " & _
                    "  From TSPL_VSPAsset_HEAD  " & _
                     " where 1=1 AND TSPL_VSPAsset_HEAD.Doc_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_VSPAsset_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_VSPAsset_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                     " and TSPL_VSPAsset_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_VSPAsset_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All " & _
                    "  Select 'MCC Milk Procurement' As Module, 'MCC Material Sale' As Screen, TSPL_SD_SHIPMENT_HEAD.Document_Code, " & _
                    " TSPL_SD_SHIPMENT_HEAD.Document_Date,  isnull(Posting_Date,'') Posting_Date , Case When TSPL_SD_SHIPMENT_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SHIPMENT_HEAD.Created_By   " & _
                    " From TSPL_SD_SHIPMENT_HEAD    where 1=1  AND TSPL_SD_SHIPMENT_HEAD.Trans_Type = 'MCC' AND TSPL_SD_SHIPMENT_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        '                   "  and convert(date, isnull(TSPL_SD_SHIPMENT_HEAD.Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date, isnull(TSPL_SD_SHIPMENT_HEAD.Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                    " and TSPL_SD_SHIPMENT_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SHIPMENT_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All  " & _
        "  Select 'MCC Milk Procurement' As Module, 'MCC Material Sale Return' As Screen," & _
        " TSPL_SD_SALE_RETURN_HEAD.Document_Code, TSPL_SD_SALE_RETURN_HEAD.Document_Date, Posting_Date, Case When TSPL_SD_SALE_RETURN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status," & _
        " TSPL_SD_SALE_RETURN_HEAD.Created_By    From TSPL_SD_SALE_RETURN_HEAD    where 1=1 AND TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'MCC'  AND TSPL_SD_SALE_RETURN_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "

        '       "  and convert(date, TSPL_SD_SALE_RETURN_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_SD_SALE_RETURN_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '        " and TSPL_SD_SALE_RETURN_HEAD.Status in (0,1)"
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALE_RETURN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
        " Select 'MCC Milk Procurement' As Module, 'VSP Item Issue' As Screen, TSPL_VSPItem_HEAD.Doc_No, Convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) As Doc_Date, Posting_Date ," & _
        " Case When TSPL_VSPItem_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_VSPItem_HEAD.Created_By    From TSPL_VSPItem_HEAD " & _
        " where 1=1 AND TSPL_VSPItem_HEAD.Doc_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_VSPItem_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_VSPItem_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '         " and TSPL_VSPItem_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_VSPItem_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'SALE MODULE --------
        qry += " Union All " & _
        " Select 'Export Sale' As Module, 'Export Sale Quotation' As Screen, TSPL_SD_SALES_Quotation_HEAD.Document_Code, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Document_Date, Posting_Date, Case When TSPL_SD_SALES_Quotation_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Created_By    From TSPL_SD_SALES_Quotation_HEAD   " & _
        " where 1=1 AND TSPL_SD_SALES_Quotation_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_SD_SALES_Quotation_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALES_Quotation_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_SD_SALES_Quotation_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALES_Quotation_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
        "   Select 'Export Sale' As Module, 'Export Sales Order' As Screen, " & _
        " TSPL_SD_SALES_ORDER_HEAD.Document_Code, TSPL_SD_SALES_ORDER_HEAD.Document_Date, Posting_Date, " & _
        " Case When TSPL_SD_SALES_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, " & _
        " TSPL_SD_SALES_ORDER_HEAD.Created_By    From TSPL_SD_SALES_ORDER_HEAD where 1=1   and TSPL_SD_SALES_ORDER_HEAD.SalesOrder_Type = 'EX' and Trans_Type = 'EXP' AND TSPL_SD_SALES_ORDER_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_SD_SALES_ORDER_HEAD.SalesOrder_Type = 'EX'   " & _
        '       "  and convert(date, TSPL_SD_SALES_ORDER_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALES_ORDER_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '         " and TSPL_SD_SALES_ORDER_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALES_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
        "  Select 'Export Sale' As Module, 'Export Proforma Invoice' As Screen, TSPL_EX_PI_HEAD.Document_Code, TSPL_EX_PI_HEAD.Document_Date, Posting_Date , " & _
        " Case When TSPL_EX_PI_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_EX_PI_HEAD.Created_By    From TSPL_EX_PI_HEAD    " & _
        " where 1=1 AND TSPL_EX_PI_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_EX_PI_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_EX_PI_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '         " and TSPL_EX_PI_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EX_PI_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
        " Select 'Export Sale' As Module, 'Export Commercial Invoice & Packing List' As Screen, " & _
        " TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date, Posting_Date ," & _
         " Case When TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Created_By " & _
         " From TSPL_EX_COMMERCIAL_INVOICE_HEAD    where 1=1 AND TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Type = 'Ex' " & _
        '        " and convert(date,  TSPL_EX_COMMERCIAL_INVOICE_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
         "  Select 'Export Sale' As Module, 'Export Sales Invoice' As Screen, TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, Posting_Date , " & _
         " Case When TSPL_SD_SALE_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_INVOICE_HEAD.Created_By    From TSPL_SD_SALE_INVOICE_HEAD   " & _
         " where 1=1 AND TSPL_SD_SALE_INVOICE_HEAD.Document_Type = 'EX' And TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'EXP'  AND TSPL_SD_SALE_INVOICE_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "

        '        " and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_SD_SALE_INVOICE_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALE_INVOICE_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All  " & _
         "  Select 'Export Sale' As Module, " & _
         " 'Export Sale Return' As Screen, TSPL_SD_SALE_RETURN_HEAD.Document_Code, TSPL_SD_SALE_RETURN_HEAD.Document_Date, Posting_Date, " & _
         " Case When TSPL_SD_SALE_RETURN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_RETURN_HEAD.Created_By    From TSPL_SD_SALE_RETURN_HEAD   " & _
         " where 1=1  AND TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'EXP' And TSPL_SD_SALE_RETURN_HEAD.Document_Type = 'EX'  AND TSPL_SD_SALE_RETURN_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "

        '         " and convert(date, TSPL_SD_SALE_RETURN_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_SD_SALE_RETURN_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALE_RETURN_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ' -  - - - - - - - -- --  - 
        qry += "  Union All   " & _
          " Select 'Merchant Trade' As Module," & _
          " 'Merchant Purchase Order' As Screen, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, Posting_Date ," & _
         " Case When TSPL_PURCHASE_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_PURCHASE_ORDER_HEAD.Created_By    From TSPL_PURCHASE_ORDER_HEAD  " & _
         " where 1=1 AND TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade = 1   " & _
        '        " and convert(date, TSPL_PURCHASE_ORDER_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_PURCHASE_ORDER_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_PURCHASE_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
         " Select 'Merchant Trade' As Module, 'Merchant Sales Order' As Screen, " & _
         " TSPL_SD_SALES_ORDER_HEAD.Document_Code, TSPL_SD_SALES_ORDER_HEAD.Document_Date, Posting_Date,  Case When TSPL_SD_SALES_ORDER_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, " & _
         " TSPL_SD_SALES_ORDER_HEAD.Created_By    From TSPL_SD_SALES_ORDER_HEAD    where 1=1 AND TSPL_SD_SALES_ORDER_HEAD.SalesOrder_Type = 'MT' AND  Trans_Type = 'EXP' AND TSPL_SD_SALES_ORDER_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_SD_SALES_ORDER_HEAD.SalesOrder_Type = 'MT'   " & _
        '        " and convert(date, TSPL_SD_SALES_ORDER_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALES_ORDER_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '        " and TSPL_SD_SALES_ORDER_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALES_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
         " Select 'Merchant Trade' As Module, 'Merchant Proforma Invoice' As Screen, TSPL_EX_PI_HEAD.Document_Code, TSPL_EX_PI_HEAD.Document_Date, Posting_Date , " & _
         " Case When TSPL_EX_PI_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_EX_PI_HEAD.Created_By    From TSPL_EX_PI_HEAD    " & _
         " where 1=1 AND TSPL_EX_PI_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_EX_PI_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_EX_PI_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_EX_PI_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EX_PI_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
         " Select 'Merchant Trade' As Module, 'LC Request' As Screen, TSPL_LC_REQUEST_MT.LCRequestNo, TSPL_LC_REQUEST_MT.LCRequest_Date, Posting_Date ," & _
         " Case When TSPL_LC_REQUEST_MT.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_LC_REQUEST_MT.Created_By    From TSPL_LC_REQUEST_MT    " & _
         " where 1=1 AND TSPL_LC_REQUEST_MT.LCRequestNo IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_LC_REQUEST_MT.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_LC_REQUEST_MT.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_LC_REQUEST_MT.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_LC_REQUEST_MT.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
         " Select 'Merchant Trade' As Module, 'LC Creation' As Screen, TSPL_LC_CREATION_MT.LCCreationNo, TSPL_LC_CREATION_MT.LCCreation_Date, Posting_Date ," & _
         " Case When TSPL_LC_CREATION_MT.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_LC_CREATION_MT.Created_By    From TSPL_LC_CREATION_MT   " & _
         " where 1=1 AND TSPL_LC_CREATION_MT.LCCreationNo IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_LC_CREATION_MT.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_LC_CREATION_MT.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_LC_CREATION_MT.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_LC_CREATION_MT.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
         "   Select 'Merchant Trade' As Module, 'Document Acceptance' As Screen, TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo, " & _
         " TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptance_Date, Posting_Date,  Case When TSPL_DOCUMENT_ACCEPTANCE_MT.Posted = 1 Then 'Y' Else 'N' End As Status," & _
         " TSPL_DOCUMENT_ACCEPTANCE_MT.Created_By    From TSPL_DOCUMENT_ACCEPTANCE_MT " & _
         " where 1=1 AND TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_DOCUMENT_ACCEPTANCE_MT.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_DOCUMENT_ACCEPTANCE_MT.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_DOCUMENT_ACCEPTANCE_MT.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_DOCUMENT_ACCEPTANCE_MT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
         " Select 'Merchant Trade' As Module, 'Fixed Deposit' As Screen, " & _
         " TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No, TSPL_FIXED_DEPOSIT_MASTER_MT.Document_Date, Posting_Date , Case When TSPL_FIXED_DEPOSIT_MASTER_MT.Posted = 1 Then 'Y' Else 'N' End As Status, " & _
         " TSPL_FIXED_DEPOSIT_MASTER_MT.Created_By    From TSPL_FIXED_DEPOSIT_MASTER_MT  " & _
          " where 1=1 AND TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_FIXED_DEPOSIT_MASTER_MT.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_FIXED_DEPOSIT_MASTER_MT.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_FIXED_DEPOSIT_MASTER_MT.Posted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_FIXED_DEPOSIT_MASTER_MT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
          " Select 'Merchant Trade' As Module, " & _
          "   'Merchant Commercial Invoice & Packing List' As Screen, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code," & _
          " TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date, Posting_Date , Case When TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status," & _
          " TSPL_EX_COMMERCIAL_INVOICE_HEAD.Created_By    From TSPL_EX_COMMERCIAL_INVOICE_HEAD    where 1=1 AND TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Type = 'MT' " & _
        '         "  and convert(date, TSPL_EX_COMMERCIAL_INVOICE_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '            " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Status in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All" & _
        " Select 'Merchant Trade' As Module, 'Merchant SRN' As Screen, TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date, Posting_Date, " & _
        " Case When TSPL_SRN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SRN_HEAD.Created_By    From TSPL_SRN_HEAD    where 1=1 AND TSPL_SRN_HEAD.SRN_No IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_SRN_HEAD.Document_Type = 'MT'  " & _
        '       " and convert(date, TSPL_SRN_HEAD.Posting_Date,103)  >= convert(date,'" + fromdate + "',103) and convert(date,TSPL_SRN_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_SRN_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SRN_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
        " Select 'Merchant Trade' As Module, 'Merchant Sales Invoice' As Screen, TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, Posting_Date ," & _
        " Case When TSPL_SD_SALE_INVOICE_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_INVOICE_HEAD.Created_By    From TSPL_SD_SALE_INVOICE_HEAD   " & _
        " Where  1=1  AND TSPL_SD_SALE_INVOICE_HEAD.Document_Type = 'MT' And TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'EXP' AND TSPL_SD_SALE_INVOICE_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "

        '        " and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_SD_SALE_INVOICE_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALE_INVOICE_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
        " Select 'Merchant Trade' As Module," & _
        " 'Merchant Sale Return' As Screen, TSPL_SD_SALE_RETURN_HEAD.Document_Code, TSPL_SD_SALE_RETURN_HEAD.Document_Date, Posting_Date ," & _
         " Case When TSPL_SD_SALE_RETURN_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_SD_SALE_RETURN_HEAD.Created_By    From TSPL_SD_SALE_RETURN_HEAD   " & _
         " where 1=1 AND TSPL_SD_SALE_RETURN_HEAD.Document_Type = 'MT' And TSPL_SD_SALE_RETURN_HEAD.Trans_Type = 'EXP' AND TSPL_SD_SALE_RETURN_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "

        '        " and convert(date, TSPL_SD_SALE_RETURN_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '         " and TSPL_SD_SALE_RETURN_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_SD_SALE_RETURN_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All" & _
         " Select 'Milk Job Work' As Module, 'Milk RGP' As Screen, TSPL_MILK_RGP_HEAD.RGP_No, Convert(date,TSPL_MILK_RGP_HEAD.RGP_Date,103) As RGP_Date, Posting_Date, " & _
         " Case When TSPL_MILK_RGP_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_RGP_HEAD.Created_By    From TSPL_MILK_RGP_HEAD   " & _
         " where 1=1 AND TSPL_MILK_RGP_HEAD.RGP_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MILK_RGP_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_MILK_RGP_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_MILK_RGP_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MILK_RGP_HEAD.Created_By   in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
        " Select 'Milk Job Work' As Module, 'Milk Gate Entry' As Screen, TSPL_MILK_GATE_ENTRY_DETAILS.Gate_Entry_No, " & _
        " TSPL_MILK_GATE_ENTRY_DETAILS.Date_And_Time, Posting_Date, Case When TSPL_MILK_GATE_ENTRY_DETAILS.isPosted = 1 Then 'Y' Else 'N' End As Status," & _
        " TSPL_MILK_GATE_ENTRY_DETAILS.Created_By    From TSPL_MILK_GATE_ENTRY_DETAILS   " & _
        " where 1=1 AND TSPL_MILK_GATE_ENTRY_DETAILS.Gate_Entry_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MILK_GATE_ENTRY_DETAILS.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_MILK_GATE_ENTRY_DETAILS.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and  TSPL_MILK_GATE_ENTRY_DETAILS.isPosted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MILK_GATE_ENTRY_DETAILS.Created_By    in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
        "   Select 'Milk Job Work' As Module, " & _
        " 'Milk Weighment' As Screen, TSPL_MILK_WEIGHMENT_DETAIL.Weighment_No, TSPL_MILK_WEIGHMENT_DETAIL.Weighment_date, Posting_Date , " & _
        " Case When TSPL_MILK_WEIGHMENT_DETAIL.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_WEIGHMENT_DETAIL.Created_By    From TSPL_MILK_WEIGHMENT_DETAIL  " & _
         " where 1=1 AND TSPL_MILK_WEIGHMENT_DETAIL.Weighment_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MILK_WEIGHMENT_DETAIL.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_MILK_WEIGHMENT_DETAIL.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and  TSPL_MILK_WEIGHMENT_DETAIL.isPosted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MILK_WEIGHMENT_DETAIL.Created_By   in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
        "  Select 'Milk Job Work' As Module, 'Milk Quality Check' As Screen, TSPL_MILK_QUALITY_CHECK.Weighment_No, TSPL_MILK_QUALITY_CHECK.Weighment_Date, Posting_Date ," & _
        " Case When TSPL_MILK_QUALITY_CHECK.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_MILK_QUALITY_CHECK.Created_By    From TSPL_MILK_QUALITY_CHECK  " & _
        " where 1=1 AND TSPL_MILK_QUALITY_CHECK.Weighment_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_MILK_QUALITY_CHECK.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_MILK_QUALITY_CHECK.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and  TSPL_MILK_QUALITY_CHECK.isPosted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and  TSPL_MILK_QUALITY_CHECK.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
        "  Select 'Milk Job Work' As Module, 'Milk Unloading' As Screen, TSPL_JOB_MILK_UNLOADING.Unloading_No, TSPL_JOB_MILK_UNLOADING.Unloading_Date_Time, Posting_Date ," & _
        " Case When TSPL_JOB_MILK_UNLOADING.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_JOB_MILK_UNLOADING.Created_By    From TSPL_JOB_MILK_UNLOADING   " & _
        " where 1=1 AND TSPL_JOB_MILK_UNLOADING.Unloading_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_JOB_MILK_UNLOADING.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_JOB_MILK_UNLOADING.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '         " and TSPL_JOB_MILK_UNLOADING.isPosted in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_JOB_MILK_UNLOADING.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All" & _
        " Select 'Milk Job Work' As Module, 'Milk SRN' As Screen, TSPL_JOB_MILK_SRN.SRN_NO, TSPL_JOB_MILK_SRN.SRN_Date, Posting_Date, " & _
        " Case When TSPL_JOB_MILK_SRN.isPosted = 1 Then 'Y' Else 'N' End As Status, TSPL_JOB_MILK_SRN.Created_By    From TSPL_JOB_MILK_SRN " & _
        " where 1=1 AND TSPL_JOB_MILK_SRN.SRN_NO IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_JOB_MILK_SRN.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_JOB_MILK_SRN.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_JOB_MILK_SRN.isPosted  in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_JOB_MILK_SRN.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
        " Select 'Service' As Module, 'Complaint Detail Entry' As Screen, TSPL_COMPLAINT_DETAIL.comp_id, " & _
        " TSPL_COMPLAINT_DETAIL.comp_date, modify_date as Posting_Date, Case When TSPL_COMPLAINT_DETAIL.post_status = 1 Then 'Y' Else 'N' End As Status," & _
        " TSPL_COMPLAINT_DETAIL.created_by    From TSPL_COMPLAINT_DETAIL  " & _
         " where 1=1 AND TSPL_COMPLAINT_DETAIL.comp_id IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_COMPLAINT_DETAIL.modify_date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_COMPLAINT_DETAIL.modify_date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_COMPLAINT_DETAIL.post_status  in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_COMPLAINT_DETAIL.created_by in (" + clsCommon.GetMulcallString(user) + ") "
        End If

        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All  " & _
         "  Select 'Service' As Module, 'Service Dispatch' As Screen, " & _
         " TSPL_RGP_HEAD.RGP_No, TSPL_RGP_HEAD.RGP_Date, Posting_Date,  Case When TSPL_RGP_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_RGP_HEAD.Created_By " & _
         " From TSPL_RGP_HEAD    where 1=1 AND TSPL_RGP_HEAD.RGP_No IN (SELECT documents FROM @ListOfDocs)  "
        'TSPL_RGP_HEAD.Doc_Type = 'Disp'  " & _
        '        " and convert(date, TSPL_RGP_HEAD.Posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_RGP_HEAD.Posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_RGP_HEAD.Status  in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_RGP_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All  " & _
        " Select 'Service' As Module, 'Cart Maintenance Entry' As Screen, " & _
        " TSPL_CART_MAINTENANCE.DocNo, TSPL_CART_MAINTENANCE.Date, modify_date as Posting_Date , Case When TSPL_CART_MAINTENANCE.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_CART_MAINTENANCE.created_by " & _
        "  From TSPL_CART_MAINTENANCE   " & _
        " where 1=1 AND TSPL_CART_MAINTENANCE.DocNo IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_CART_MAINTENANCE.modify_date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_CART_MAINTENANCE.modify_date,103) <=convert(date,'" + Todate + "',103 )" & _
        '          " and TSPL_CART_MAINTENANCE.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_CART_MAINTENANCE.created_by in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
        " Select 'Fixed Asset' As Module, 'Acquisition Entry' As Screen, TSPL_ACQUISITION_HEAD.Acquisition_Code, " & _
        " TSPL_ACQUISITION_HEAD.Acquisition_Date, case when Post_Date is null then modify_date else Post_Date end as Posting_Date, Case When TSPL_ACQUISITION_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_ACQUISITION_HEAD.Created_By  " & _
        "   From TSPL_ACQUISITION_HEAD " & _
         " where 1=1 AND TSPL_ACQUISITION_HEAD.Acquisition_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, case when Post_Date is null then modify_date else Post_Date end,103)  >=convert(date,'" + fromdate + "',103) and convert(date, case when Post_Date is null then modify_date else Post_Date end,103) <=convert(date,'" + Todate + "',103 )" & _
        '            " and TSPL_ACQUISITION_HEAD.Status  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_ACQUISITION_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
        " Select 'Fixed Asset' As Module, 'Disposal Entry' As Screen, TSPL_ASSET_SCRAP_HEAD.Document_No, " & _
        " TSPL_ASSET_SCRAP_HEAD.Document_Date, posting_Date, Case When TSPL_ASSET_SCRAP_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_ASSET_SCRAP_HEAD.Created_By  " & _
        " From TSPL_ASSET_SCRAP_HEAD " & _
         " where 1=1 AND TSPL_ASSET_SCRAP_HEAD.Document_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_ASSET_SCRAP_HEAD.posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_ASSET_SCRAP_HEAD.posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_ASSET_SCRAP_HEAD.Status in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_ASSET_SCRAP_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "   Union All  " & _
         "  Select 'Fixed Asset' As Module, 'Issue Items to Assemble Assset' As Screen, TSPL_IssueItemToAssembledAsset_Head.Doc_No, " & _
         " TSPL_IssueItemToAssembledAsset_Head.Doc_Date,isnull(Posting_Date,'') posting_Date,  Case When TSPL_IssueItemToAssembledAsset_Head.Status = 1 Then 'Y' Else 'N' End As Status," & _
         " TSPL_IssueItemToAssembledAsset_Head.Created_By    From TSPL_IssueItemToAssembledAsset_Head   " & _
          " where 1=1 AND TSPL_IssueItemToAssembledAsset_Head.Doc_No IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_IssueItemToAssembledAsset_Head.posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_IssueItemToAssembledAsset_Head.posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '            " and TSPL_IssueItemToAssembledAsset_Head.Status  in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_IssueItemToAssembledAsset_Head.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        ' ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
         "  Select 'Fixed Asset' As Module," & _
         "  'Assset Work Expanses' As Screen, TSPL_ASSET_WORK_HEAD.Document_Code, TSPL_ASSET_WORK_HEAD.Document_Date,  isnull(Post_Date,'') posting_Date ," & _
          " Case When TSPL_ASSET_WORK_HEAD.Status = 1 Then 'Y' Else 'N' End As Status, TSPL_ASSET_WORK_HEAD.Created_By    From TSPL_ASSET_WORK_HEAD   " & _
          " where 1=1 AND TSPL_ASSET_WORK_HEAD.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Post_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Post_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '           " and TSPL_ASSET_WORK_HEAD.Status in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_ASSET_WORK_HEAD.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
          "   Select 'Payroll' As Module, 'Employee Salary' As Screen, TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE, TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM, isnull(Posting_Date,'') posting_Date ," & _
          " Case When TSPL_EMPLOYEE_SALARY.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_EMPLOYEE_SALARY.Created_By    From TSPL_EMPLOYEE_SALARY    " & _
           " where 1=1 AND TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '              " and TSPL_EMPLOYEE_SALARY.POSTED  in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_SALARY.Created_By in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
            "   Select 'Payroll' As Module, 'Leave Application' As Screen, TSPL_LEAVE_APPLICATION.LVAPPLICATION_CODE, TSPL_LEAVE_APPLICATION.APPLICATION_DATE, isnull(Posting_Date,'') posting_Date ," & _
            " Case When TSPL_LEAVE_APPLICATION.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_LEAVE_APPLICATION.Created_By    From TSPL_LEAVE_APPLICATION    " & _
             " where 1=1 AND TSPL_LEAVE_APPLICATION.LVAPPLICATION_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '               " and TSPL_LEAVE_APPLICATION.POSTED  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_LEAVE_APPLICATION.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
            "   Select 'Payroll' As Module, 'Leave Adjustment' As Screen, TSPL_LEAVE_ADJUSTMENT.LVADJUSTMENT_CODE, TSPL_LEAVE_ADJUSTMENT.ADJUSTMENT_DATE, isnull(Posting_Date,'') posting_Date ," & _
            " Case When TSPL_LEAVE_ADJUSTMENT.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_LEAVE_ADJUSTMENT.Created_By    From TSPL_LEAVE_ADJUSTMENT    " & _
            " where 1=1 AND TSPL_LEAVE_ADJUSTMENT.LVADJUSTMENT_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '              " and TSPL_LEAVE_ADJUSTMENT.POSTED in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_LEAVE_ADJUSTMENT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
            "   Select 'Payroll' As Module, 'Allowance Details' As Screen, TSPL_ALLOWANCE.ALLOWANCE_CODE, TSPL_ALLOWANCE.ALLOWANCE_DATE, isnull(Posting_Date,'') posting_Date," & _
            " Case When TSPL_ALLOWANCE.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_ALLOWANCE.Created_By    From TSPL_ALLOWANCE   " & _
             " where 1=1 AND TSPL_ALLOWANCE.ALLOWANCE_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '              " and TSPL_ALLOWANCE.POSTED  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_ALLOWANCE.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                "   Select 'Payroll' As Module, 'Deduction Details' As Screen, TSPL_DEDUCTION.DEDUCTION_CODE, TSPL_DEDUCTION.DEDUCTION_DATE, isnull(Posting_Date,'') posting_Date, " & _
                " Case When TSPL_DEDUCTION.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_DEDUCTION.Created_By    From TSPL_DEDUCTION    " & _
                " where 1=1 AND TSPL_DEDUCTION.DEDUCTION_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,  isnull(Posting_Date,'') ,103)  >=convert(date,'" + fromdate + "',103) and convert(date, isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and TSPL_DEDUCTION.POSTED  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_DEDUCTION.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                "   Select 'Payroll' As Module, 'Employee Reimbursement' As Screen, TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_CODE, TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_DATE, isnull(Posting_Date,'') posting_Date ," & _
                " Case When TSPL_EMP_REIMBURSEMENT.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_EMP_REIMBURSEMENT.Created_By    From TSPL_EMP_REIMBURSEMENT   " & _
                 " where 1=1 AND TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_EMP_REIMBURSEMENT.POSTED in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EMP_REIMBURSEMENT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                " Select 'Payroll' As Module, 'Generate Bonus' As Screen, TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE, TSPL_EMPLOYEE_BONUS.FROM_PAY_PERIOD_CODE, isnull(Posting_Date,'') posting_Date ," & _
                " Case When TSPL_EMPLOYEE_BONUS.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_EMPLOYEE_BONUS.Created_By    From TSPL_EMPLOYEE_BONUS  " & _
                " where 1=1 AND TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,'') ,103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_EMPLOYEE_BONUS.POSTED in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_BONUS.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All " & _
                "   Select 'Payroll' As Module, 'Loan Generation' As Screen, TSPL_LOAN_GENERATION.LOAN_GENERATION_CODE, TSPL_LOAN_GENERATION.GENERATION_DATE,  isnull(Posting_Date,'') posting_Date, " & _
                " Case When TSPL_LOAN_GENERATION.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_LOAN_GENERATION.Created_By    From TSPL_LOAN_GENERATION  " & _
                " where 1=1 AND TSPL_LOAN_GENERATION.LOAN_GENERATION_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                 " and TSPL_LOAN_GENERATION.POSTED in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_LOAN_GENERATION.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                "    Select 'Payroll' As Module, 'Loan Adjustment' As Screen, TSPL_LOAN_ADJUSTMENT.LOANADJUSTMENT_CODE, TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_DATE, isnull(Posting_Date,'') posting_Date ," & _
                " Case When TSPL_LOAN_ADJUSTMENT.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_LOAN_ADJUSTMENT.Created_By    From TSPL_LOAN_ADJUSTMENT    " & _
                " where 1=1 AND TSPL_LOAN_ADJUSTMENT.LOANADJUSTMENT_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                  " and TSPL_LOAN_ADJUSTMENT.POSTED in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_LOAN_ADJUSTMENT.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
                " Select 'Payroll' As Module, 'Daily Attendance' As Screen, TSPL_DAILY_ATTENDANCE.DLA_CODE, TSPL_DAILY_ATTENDANCE.Created_Date, isnull(Posting_Date,'') posting_Date , " & _
                " Case When TSPL_DAILY_ATTENDANCE.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_DAILY_ATTENDANCE.Created_By    From TSPL_DAILY_ATTENDANCE " & _
                  " where 1=1 AND TSPL_DAILY_ATTENDANCE.DLA_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                   " and TSPL_DAILY_ATTENDANCE.POSTED in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_DAILY_ATTENDANCE.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All" & _
                    "  Select 'Payroll' As Module, 'Hourly Attendance' As Screen, TSPL_HOURLY_ATTENDANCE.DLA_CODE, TSPL_HOURLY_ATTENDANCE.Created_Date, isnull(Posting_Date,'') posting_Date , " & _
                    " Case When TSPL_HOURLY_ATTENDANCE.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_HOURLY_ATTENDANCE.Created_By    From TSPL_HOURLY_ATTENDANCE  " & _
                     " where 1=1 AND TSPL_HOURLY_ATTENDANCE.DLA_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,'') ,103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                     " and TSPL_HOURLY_ATTENDANCE.POSTED in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_HOURLY_ATTENDANCE.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                     "   Select 'Payroll' As Module, 'Monthly Attendance' As Screen, TSPL_MONTHLY_ATTENDANCE.MTA_CODE, TSPL_MONTHLY_ATTENDANCE.Created_Date, isnull(Posting_Date,'') posting_Date , " & _
                     " Case When TSPL_MONTHLY_ATTENDANCE.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_MONTHLY_ATTENDANCE.Created_By    From TSPL_MONTHLY_ATTENDANCE " & _
                     " where 1=1 AND TSPL_MONTHLY_ATTENDANCE.MTA_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                       " and TSPL_MONTHLY_ATTENDANCE.POSTED in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MONTHLY_ATTENDANCE.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                    "   Select 'Payroll' As Module, 'Employee Adjustment Voucher' As Screen, TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE, TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE, isnull(Posting_Date,'') posting_Date , " & _
                    " Case When TSPL_ADJUSTMENT_VOUCHER.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_ADJUSTMENT_VOUCHER.Created_By    From TSPL_ADJUSTMENT_VOUCHER   " & _
                     " where 1=1 AND TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,'') ,103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,'') ,103) <=convert(date,'" + Todate + "',103 )" & _
        '                      " and TSPL_ADJUSTMENT_VOUCHER.POSTED  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_ADJUSTMENT_VOUCHER.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All   " & _
                    " Select 'Payroll' As Module, 'Salary Generation' As Screen, TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE, TSPL_GENERATE_SALARY.GENERATE_DATE, isnull(Posting_Date,'') posting_Date ," & _
                    " Case When TSPL_GENERATE_SALARY.POSTED = 1 Then 'Y' Else 'N' End As Status, TSPL_GENERATE_SALARY.Created_By    From TSPL_GENERATE_SALARY   " & _
                     " where 1=1 AND TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                       " and TSPL_GENERATE_SALARY.POSTED  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_GENERATE_SALARY.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                    "   Select 'Payroll' As Module, 'LTA Claim' As Screen, TSPL_LTA_Claim_Head.LTA_CODE, TSPL_LTA_Claim_Head.Created_Date, isnull(Posting_Date,'') posting_Date ," & _
                    " Case When TSPL_LTA_Claim_Head.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_LTA_Claim_Head.Created_By    From TSPL_LTA_Claim_Head  " & _
                     " where 1=1 AND TSPL_LTA_Claim_Head.LTA_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Posting_Date,'') ,103)  >=convert(date,'" + fromdate + "',103) and convert(date, isnull(Posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                       " and TSPL_LTA_Claim_Head.Posted  in (0,1) "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_LTA_Claim_Head.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All  " & _
                    "  Select 'Payroll' As Module, 'Employee Mediclaim Entry' As Screen, TSPL_MEDICLAIM_HEAD.DOCUMENT_CODE, TSPL_MEDICLAIM_HEAD.DATE,  isnull(Modified_Date,'') posting_Date ," & _
                    " Case When TSPL_MEDICLAIM_HEAD.Status = 'Y' Then 'Y' Else 'N' End As Status, TSPL_MEDICLAIM_HEAD.Created_By    From TSPL_MEDICLAIM_HEAD    " & _
                    " where 1=1 AND TSPL_MEDICLAIM_HEAD.DOCUMENT_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(Modified_Date,'') ,103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(Modified_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                      " and TSPL_MEDICLAIM_HEAD.Status in ('N' ,'Y') "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_MEDICLAIM_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All " & _
                    "   Select 'Payroll' As Module, 'Full And Final Settlement' As Screen, TSPL_FF_SETTLEMENT_HEAD.EMP_CODE, " & _
                    " TSPL_FF_SETTLEMENT_HEAD.Created_Date,isnull(posting_Date,'') as Posting_Date, Case When TSPL_FF_SETTLEMENT_HEAD.Posted = 1 Then 'Y' Else 'N' End As Status, TSPL_FF_SETTLEMENT_HEAD.Created_By " & _
                    " From TSPL_FF_SETTLEMENT_HEAD  " & _
                    " where 1=1 AND TSPL_FF_SETTLEMENT_HEAD.EMP_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(posting_Date,''),103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                        " and TSPL_FF_SETTLEMENT_HEAD.Posted  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_FF_SETTLEMENT_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All   " & _
                    " Select 'Payroll' As Module, 'Employee Shift Change' As Screen, TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.EMP_SHIFT_CODE, " & _
                    " TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.SHIFT_APP_DATE, isnull(posting_Date,'') as Posting_Date, Case When TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.POSTED = 1 Then 'Y' Else 'N' End As Status," & _
                        " TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.Created_By    From TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD   " & _
                         " where 1=1 AND TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.EMP_SHIFT_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, isnull(posting_Date,'') ,103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(posting_Date,''),103) <=convert(date,'" + Todate + "',103 )" & _
        '                              " and TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.POSTED  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += " Union All  " & _
                        "  Select 'Payroll' As Module, 'Employee Transfer' As Screen, " & _
                        " TSPL_EMPLOYEE_TRANSFER.Document_Code, TSPL_EMPLOYEE_TRANSFER.Document_Date,  isnull(posting_Date,'') posting_Date , Case When TSPL_EMPLOYEE_TRANSFER.POSTED = 1 Then 'Y' Else 'N' End As Status," & _
                        " TSPL_EMPLOYEE_TRANSFER.Created_By    From TSPL_EMPLOYEE_TRANSFER  " & _
                         " where 1=1 AND TSPL_EMPLOYEE_TRANSFER.Document_Code IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date,  isnull(posting_Date,'') ,103)  >=convert(date,'" + fromdate + "',103) and convert(date,isnull(posting_Date,'') ,103) <=convert(date,'" + Todate + "',103 )" & _
        '                            " and TSPL_EMPLOYEE_TRANSFER.POSTED  in (0,1)  "

        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_TRANSFER.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        qry += "  Union All " & _
                        "   Select 'Payroll' As Module, 'Employee Increment' As Screen," & _
                        " TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE, TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE, posting_Date, Case When TSPL_EMPLOYEE_INCREMENT_HEAD.POSTED = 1 Then 'Y' Else 'N' End As Status, " & _
                        " TSPL_EMPLOYEE_INCREMENT_HEAD.Created_By    From TSPL_EMPLOYEE_INCREMENT_HEAD" & _
                        " where 1=1 AND TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE IN (SELECT documents FROM @ListOfDocs)  "
        'and convert(date, TSPL_EMPLOYEE_INCREMENT_HEAD.posting_Date,103)  >=convert(date,'" + fromdate + "',103) and convert(date,TSPL_EMPLOYEE_INCREMENT_HEAD.posting_Date,103) <=convert(date,'" + Todate + "',103 )" & _
        '                          " and TSPL_EMPLOYEE_INCREMENT_HEAD.POSTED  in (0,1)  "
        If user IsNot Nothing AndAlso user.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_INCREMENT_HEAD.Created_By  in (" + clsCommon.GetMulcallString(user) + ") "
        End If
        qry += " ) final  "

        Return qry
    End Function
End Class


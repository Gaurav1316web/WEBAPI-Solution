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
'Created By Sanjay, 28/Aug/2020

Public Class frmBillChecklist
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCheckPrinting)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isModifyFlag

    End Sub


#End Region
#Region "Events"

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub frmPrintCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        AddNew()

    End Sub

    Private Sub frmPrintCheck_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
#End Region

          
    Private Sub FndDocCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocCode._MYValidating
        Try
            Dim qry As String = ""
            Dim whrClas As String = ""
            If rdbSRN.Checked = True Then
                qry = "select SRN_No as Code,FORMAT(CAST(SRN_Date AS DATETIME),'dd/MM/yyyy hh:mm tt') as Date,TSPL_SRN_HEAD.Vendor_Code as [Vendor Code], TSPL_SRN_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],SRN_Total_Amt as Amount,case when TSPL_SRN_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],Against_QC_Code as [Against QC Code],Against_QC_Date as [Against QC Date],TSPL_USER_MASTER.User_Name as [User Name] "
                qry += ",Against_MRN as [Against MRN Code],against_grn as [Against GRN Code] "
                qry += ",Against_PO as [Against PO Code] "
                qry += " from TSPL_SRN_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_SRN_HEAD.Vendor_Code left join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code =TSPL_SRN_HEAD.Created_By  "
                whrClas += " isnull(TSPL_SRN_HEAD.Against_PO,'') not in ( Select TSPL_SRN_HEAD.Against_PO  from TSPL_SRN_HEAD left Outer Join TSPL_PURCHASE_ORDER_HEAD on TSPL_SRN_HEAD.Against_PO =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No where TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade =1) "
                whrClas += " and convert(date,SRN_Date,103)>= Convert(date,'" + txtFromDate.Value + "',103) and convert(date,SRN_Date,103)<= Convert(date,'" + txtToDate.Value + "',103)"
                fndDocCode.Value = clsCommon.ShowSelectForm("BCListCofnd", qry, "Code", whrClas, fndDocCode.Value, "Date desc", isButtonClicked)

            ElseIf rdbAPInvoice.Checked = True Then
                qry = "select TSPL_VENDOR_INVOICE_HEAD.Document_No as DocumentNo,Invoice_Entry_Date as Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],Vendor_Invoice_No as [Vendor Invoice No],Against_POInvoice_No as [PO Invoice No],Vendor_Invoice_Date as [Vendor Invoice Date],(case when len(Posting_Date) is null then 'UnPosted' else 'Posted' end) as [Status],Account_Set as AccountSet,Against_PurchaseReturn_No as [PO Return No],TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition as [Acquisition No],TSPL_VENDOR_INVOICE_HEAD.Against_VCGL As [Against VCGL],ISNULL(TSPL_VENDOR_INVOICE_HEAD.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_VENDOR_INVOICE_HEAD.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code]," &
                                "TSPL_VENDOR_INVOICE_head.refDocNo as [Reference Doc No],Against_POInvoice_No as [Against PO Invoice No] ,Against_PurchaseReturn_No as [Against Purchase Return No]," &
                                "Against_Acquisition as [Against Acquisition],TSPL_VENDOR_INVOICE_head.invoice_type as [Invoice Type]," &
                                "against_millkpurchaseinvoice_No as [Against Milk Purchase Invoice No],Against_bulkmillkpurchaseinvoice_No as [Against Bulk Milk Purchase Invoice No]," &
                                "against_asset_work as [Against Asset Work],case when Document_Type='C' then 'Credit Note'  when Document_Type ='D' then 'Debit Note'  when document_type='I' then 'Invoice' end  as [Document Type],case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered=0 then 'No' else 'Yes' end as GSTRegistered,Purchase_Tax_Invoice,Purchase_Tax_Invoice_Type " &
                                " ,TSPL_VENDOR_INVOICE_HEAD.Against_Salary_Generation_Code as [Against Salary Generation],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC Name],TSPL_MCC_MASTER.Plant_Code as [Plant Code] ,TSPL_LOCATION_MASTER.Location_Desc as [Plant Name] from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 " &
                                 " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                                 " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                                 "  Left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC  Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code   "
                whrClas += " TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type in ('AP','VC') and ISNULL(RefDocNo,'')='' "
                whrClas += " and convert(date,Invoice_Entry_Date,103)>= Convert(date,'" + txtFromDate.Value + "',103) and convert(date,Invoice_Entry_Date,103)<= Convert(date,'" + txtToDate.Value + "',103)"
                fndDocCode.Value = clsCommon.ShowSelectForm("BCListFND", qry, "DocumentNo", whrClas, fndDocCode.Value, "DocumentNo", isButtonClicked)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub Btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        AddNew()
    End Sub

    Private Sub rdbAPInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles rdbAPInvoice.CheckedChanged
        AddNew()
    End Sub

    Private Sub Btn_Go_Click(sender As Object, e As EventArgs) Handles btn_Go.Click
        Try
            If clsCommon.myLen(fndDocCode.Value) > 0 Then
                If rdbSRN.Checked = True Then
                    Dim obj As New clsSRNHead()
                    obj = clsSRNHead.GetData(fndDocCode.Value, NavigatorType.Current)
                    If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.SRN_No) > 0) Then
                        lbl_Party.Text = obj.Vendor_Name
                        lbl_PO.Text = obj.Against_PO
                        lbl_GRN.Text = obj.Against_GRN
                        lbl_Value.Text = obj.SRN_Total_Amt
                        lbl_Billno.Text = obj.Ref_No
                        lbl_ServiceBillNo.Text = obj.Inv_No
                        lbl_Remarks.Text = obj.Description
                        Dim strDocNo As String = clsCommon.myCstr(fndDocCode.Value)
                        Dim qry As String = "  select TSPL_SRN_DETAIL.Item_Code as [Item Code] " &
                          ", TSPL_SRN_DETAIL.Item_Desc As [Item Description],TSPL_ITEM_MASTER.HSN_Code As [HSN Code],TSPL_SRN_DETAIL.Unit_code As [UOM] " &
                          ",Convert(decimal(18,2),TSPL_SRN_DETAIL.GRN_Qty) as [Challan Qty],Convert(decimal(18,2),TSPL_SRN_DETAIL.MRN_Qty) as [Received Qty] " &
                          ",Convert(decimal(18,2),TSPL_SRN_DETAIL.Short_Qty) AS [Short Ecxess Qty] " &
                          ",Convert(decimal(18,2),TSPL_SRN_DETAIL.SRN_Qty) as [Accepted Qty],Convert(decimal(18,2),TSPL_SRN_DETAIL.Rejected_Qty) as [Rejected Qty],TSPL_SRN_DETAIL.PO_id as [PO NO] " &
                          ",TSPL_SRN_HEAD.Against_Requisition as [Indent No],Convert(decimal(18,2),TSPL_SRN_DETAIL.Landed_Cost_Amount) as [Amount] " &
                          " FROM  TSPL_SRN_DETAIL INNER JOIN TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No INNER JOIN TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  INNER JOIN TSPL_VENDOR_MASTER ON TSPL_SRN_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code  " &
                          " Left outer join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER .Location_Code=TSPL_SRN_HEAD.Bill_To_Location  left outer join TSPL_SHIP_TO_LOCATION On TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SRN_HEAD.Ship_To_Location  left outer join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code= TSPL_SRN_DETAIL.Item_Code  left outer join tspl_state_master As tspl_state_master_for_location_state On tspl_state_master_for_location_state.state_code=tspl_location_master.state    left outer join TSPL_STATE_MASTER On TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code  left outer join tspl_user_master As user_master1 On user_master1.user_code=TSPL_SRN_HEAD.Created_By   left outer join tspl_user_master As user_master2 On user_master2.user_code=TSPL_SRN_HEAD.Modify_By  left join TSPL_PURCHASE_ORDER_HEAD On isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'') =isnull(TSPL_SRN_DETAIL.PO_ID,'')  " &
                        " where 2=2 and TSPL_SRN_HEAD.SRN_No in ('" + strDocNo + "')   order by tspl_srn_detail.line_no"

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        gvDocs.GroupDescriptors.Clear()
                        gvDocs.MasterTemplate.SummaryRowsBottom.Clear()
                        gvDocs.DataSource = dt
                        gvDocs.MasterTemplate.BestFitColumns()
                        gvDocs.EnableFiltering = True
                        For i As Integer = 0 To gvDocs.Columns.Count - 1
                            gvDocs.Columns(i).ReadOnly = True
                            gvDocs.Columns(i).BestFit()
                        Next

                    End If
                ElseIf rdbAPInvoice.Checked = True Then
                    Dim obj As New clsVedorInvoiceHead()
                    obj = clsVedorInvoiceHead.GetData(fndDocCode.Value, "AP','VC")
                    If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                        lbl_Party.Text = obj.Vendor_Name 'clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
                        lbl_PO.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & obj.Document_No & "'"))
                        lbl_GRN.Text = ""
                        lbl_Value.Text = obj.Document_Total
                        lbl_Billno.Text = ""
                        lbl_ServiceBillNo.Text = obj.Vendor_Invoice_No
                        lbl_Remarks.Text = obj.Description
                        Dim strDocNo As String = clsCommon.myCstr(fndDocCode.Value)
                        Dim qry As String = ""
                        'qry = "Select TSPL_VENDOR_INVOICE_DETAIL.*,TSPL_SAC_MASTER.Code as SAC_Code,TSPL_SAC_MASTER.Description as SAC_Name,TSPL_TDS_DEDUCTION_HEAD.Description as Deduction_Name,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as Deduction_Section,TSPL_ACQUISITION_DETAIL.Asset_Name,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DEDUCTION_MASTER.Description end as DEDUCTION_DESC_New from TSPL_VENDOR_INVOICE_DETAIL left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_VENDOR_INVOICE_DETAIL.Deduction_Code left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode left join TSPL_ACQUISITION_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code left outer join TSPL_SAC_MASTER on TSPL_SAC_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.SAC_Code where Document_No='" + fndDocCode.Value + "' ORDER BY Detail_Line_No"
                        qry = "Select max(ACCode) as [Account No],max(ACName) as [Account Name],max(Hirerachy_Code) as [Hirerachy Code] ,max(Cost_Centre_Code) as [Cost Centre Code], case when SUM(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end   as [Debit Amount] ,case when SUM( CrAmt-DrAmt)>0 then SUM( CrAmt-DrAmt) else 0 end   as [Credit Amount]" & Environment.NewLine &
                     "   from(Select  '' as FromDate,'' as ToDate, '' as Location,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_HEAD.Document_Type,'' as Vendor, '" & strDocNo & "' as Document, Final.Document_No,final.ACCode,Final.ACName,DrAmt,CrAmt,Hirerachy_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Cost_Centre_Code,Hirerachy_Code3,Hirerachy_Code4  ,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Created_By  ,TSPL_VENDOR_INVOICE_HEAD.Created_Date,TSPL_VENDOR_INVOICE_HEAD.Due_Date,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,CreatedBy.User_Name as CreateBy,AuthorisedBy.User_Name as ApproveBy,case when ((TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS null ) Or (TSPL_VENDOR_INVOICE_HEAD.Posting_Date='') ) then 'Pending' else 'Posted' end as InvStatus,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Bill Inward Voucher' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end end end as InvoiceType  , ( select case when LEN(ISNULL(RefDocType,''))>0 then case when RefDocType='S' then 'SRN' else case when RefDocType='AP' then 'AP Invoice' end end +' : '+RefDocNo  +' - ' +(case when RefDocType='S' then (Select top 1 convert(varchar(100),SRN_Date,110)   from TSPL_SRN_HEAD where SRN_No =RefDocNo) else (select top 1 convert(varchar(100),Invoice_Entry_Date,110)  from TSPL_VENDOR_INVOICE_HEAD where RefDocNo  = RefDocNo) end)  else '' end from TSPL_VENDOR_INVOICE_HEAD where Document_No=Final.Document_No) as RefDocDescription,final.Detail_Line_No,TSPL_VENDOR_INVOICE_HEAD.TapalNo,TSPL_VENDOR_INVOICE_HEAD.DateAndTime   from ( " & Environment.NewLine &
                     " SELECT TSPL_JOURNAL_DETAILS.Detail_Line_No ,VI.RefDocNo,VI.RefDocType, TSPL_JOURNAL_MASTER.Source_doc_No as Document_No,TSPL_JOURNAL_DETAILS.Account_code as ACCode, TSPL_JOURNAL_DETAILS.Account_Desc as ACName,case when TSPL_JOURNAL_DETAILS.Amount>=0 then  TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt,case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt,TSPL_JOURNAL_DETAILS.Hirerachy_Code as Hirerachy_Code,TSPL_JOURNAL_DETAILS.Cost_Centre_Code as Cost_Centre_Code, VI.Hirerachy_Code3 ,VI.Hirerachy_Code4  " & Environment.NewLine &
                     " FROM TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code  "

                        qry = qry + " LEFT OUTER JOIN (select Detail_Line_No,TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code," & Environment.NewLine &
                        " TSPL_VENDOR_INVOICE_DETAIL.Cost_Centre_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4, TSPL_VENDOR_INVOICE_HEAD.RefDocNo, TSPL_VENDOR_INVOICE_HEAD.RefDocType " & Environment.NewLine &
                        " from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No  " & Environment.NewLine &
                        " GROUP BY TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, " & Environment.NewLine &
                        " TSPL_VENDOR_INVOICE_DETAIL.Cost_Centre_Code,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4,TSPL_VENDOR_INVOICE_HEAD.RefDocNo,TSPL_VENDOR_INVOICE_HEAD.RefDocType,Detail_Line_No ) VI" & Environment.NewLine &
                        "  on VI.Document_No  =TSPL_JOURNAL_MASTER.Source_Doc_No and VI.GL_Account_Code =TSPL_JOURNAL_DETAILS.Account_code AND TSPL_JOURNAL_DETAILS.Detail_Line_No=VI.Detail_Line_No " & Environment.NewLine &
                        " left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code =VI.Cost_Centre_Code  left join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code =VI.Hirerachy_Code  where  TSPL_JOURNAL_MASTER.Source_Doc_No = '" & strDocNo & "' " & Environment.NewLine &
                        " group by TSPL_JOURNAL_DETAILS.Detail_Line_No ,VI.RefDocNo,VI.RefDocType, TSPL_JOURNAL_MASTER.Source_doc_No ,TSPL_JOURNAL_DETAILS.Account_code , TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_DETAILS.Amount,TSPL_JOURNAL_DETAILS.Hirerachy_Code " & Environment.NewLine &
                        " ,TSPL_JOURNAL_DETAILS.Cost_Centre_Code,VI.Hirerachy_Code3,VI.Hirerachy_Code4 " & Environment.NewLine &
                         " )Final left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=final.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code left outer join TSPL_USER_MASTER as CreatedBy on CreatedBy.User_Code=TSPL_VENDOR_INVOICE_HEAD.Created_By left outer join TSPL_USER_MASTER as AuthorisedBy on AuthorisedBy .User_Code=TSPL_VENDOR_INVOICE_HEAD.Modify_By left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code =final.Cost_Centre_Code where 2=2  and TSPL_VENDOR_INVOICE_HEAD.Document_No in ('" & strDocNo & "') " & Environment.NewLine &
                        " )xxx     group by Document_No  ,ACCode ,Detail_Line_No "

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        gvDocs.GroupDescriptors.Clear()
                        gvDocs.MasterTemplate.SummaryRowsBottom.Clear()
                        gvDocs.DataSource = dt
                        gvDocs.MasterTemplate.BestFitColumns()
                        gvDocs.EnableFiltering = True
                        For i As Integer = 0 To gvDocs.Columns.Count - 1
                            gvDocs.Columns(i).ReadOnly = True
                            gvDocs.Columns(i).BestFit()
                        Next
                    End If
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(fndDocCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Document No not found to print", Me.Text)
                Exit Sub
            End If
            Dim strDocNo As String = clsCommon.myCstr(fndDocCode.Value)
            If rdbAPInvoice.Checked = True Then
                Dim qry As String = "select '" + lbl_PO.Text + "' as voucher_no,'" + lbl_Value.Text + "' as amount,'" + lbl_Remarks.Text + "' as Remarks,'" + txtCheckedBy.Text + "' as CheckedBy,'" + txtProductor.Text + "' as Productor,'" + txtCheckedByAc.Text + "' as CheckedByAc,'" + txtPlantHeadRemarks.Text + "' as PlantHeadRemarks, '' as SSC_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code, TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,xxxx.RefDocNo ,xxxx.RefDocType ,xxxx.Document_No ,xxxx.ACCode ,xxxx.ACName ,xxxx.DrAmt ,xxxx.CrAmt ,xxxx.Comp_Name ,xxxx.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name,xxxx.Invoice_Entry_Date ,xxxx.Due_Date ,xxxx.Description ,xxxx.Vendor_Invoice_Date ,xxxx.CreateBy ,xxxx.ApproveBy ,xxxx.InvStatus ,xxxx.InvoiceType ,xxxx.Vendor_Invoice_No , '' as Remarks,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_LOCATION_MASTER.Add1 ,TSPL_LOCATION_MASTER.Add2 ,TSPL_LOCATION_MASTER.Add3 ,TSPL_LOCATION_MASTER.Add4 ,XXXX.Document_Type,TSPL_COMPANY_MASTER .Logo_Img,XXXX.TapalNo,XXXX.DateAndTime from (select MAX( FromDate) as FromDate,max(ToDate) as ToDate,max(Location) as Location,max(Document_Type) as Document_Type,max(Loc_Code) as Loc_Code,max(Vendor) as Vendor,max(Document) as Document,max(Document_No) as Document_No," & Environment.NewLine &
                 " max(ACCode) as ACCode,max(ACName) as ACName, case when SUM(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end   as DrAmt ,case when SUM( CrAmt-DrAmt)>0 then SUM( CrAmt-DrAmt) else 0 end   as CrAmt,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name,max(Vendor_Code) as Vendor_Code,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(RefDocNo) as RefDocNo,max(RefDocType) as RefDocType,max(Vendor_Name) as Vendor_NameOLD,max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Due_Date) as Due_Date,max(Description) as Description,max(Vendor_Invoice_No) as Vendor_Invoice_No,max(Vendor_Invoice_Date) as Vendor_Invoice_Date,max(CreateBy) as CreateBy,max(ApproveBy) as ApproveBy,max(InvStatus) as InvStatus,max(InvoiceType) as InvoiceType,max(RefDocDescription) as RefDocDescription,max(Hirerachy_Code) as Hirerachy_Code ,max(Cost_Centre_Code) as Cost_Centre_Code,MAX(Hirerachy_Code3) AS Hirerachy_Code3,MAX(Hirerachy_Code4) AS Hirerachy_Code4,MAX(TapalNo) AS TapalNo,MAX(DateAndTime) AS DateAndTime  from(select  '' as FromDate,'' as ToDate, '' as Location,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_HEAD.Document_Type,'' as Vendor, '" & strDocNo & "' as Document, Final.Document_No,final.ACCode,Final.ACName,DrAmt,CrAmt,Hirerachy_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Cost_Centre_Code,Hirerachy_Code3,Hirerachy_Code4  ,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Created_By  ,TSPL_VENDOR_INVOICE_HEAD.Created_Date,TSPL_VENDOR_INVOICE_HEAD.Due_Date,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,CreatedBy.User_Name as CreateBy,AuthorisedBy.User_Name as ApproveBy,case when ((TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS null ) Or (TSPL_VENDOR_INVOICE_HEAD.Posting_Date='') ) then 'Pending' else 'Posted' end as InvStatus,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Bill Inward Voucher' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end end end as InvoiceType  , ( select case when LEN(ISNULL(RefDocType,''))>0 then case when RefDocType='S' then 'SRN' else case when RefDocType='AP' then 'AP Invoice' end end +' : '+RefDocNo  +' - ' +(case when RefDocType='S' then (Select top 1 convert(varchar(100),SRN_Date,110)   from TSPL_SRN_HEAD where SRN_No =RefDocNo) else (select top 1 convert(varchar(100),Invoice_Entry_Date,110)  from TSPL_VENDOR_INVOICE_HEAD where RefDocNo  = RefDocNo) end)  else '' end from TSPL_VENDOR_INVOICE_HEAD where Document_No=Final.Document_No) as RefDocDescription,final.Detail_Line_No,TSPL_VENDOR_INVOICE_HEAD.TapalNo,TSPL_VENDOR_INVOICE_HEAD.DateAndTime   from ( " & Environment.NewLine &
                 " SELECT TSPL_JOURNAL_DETAILS.Detail_Line_No ,VI.RefDocNo,VI.RefDocType, TSPL_JOURNAL_MASTER.Source_doc_No as Document_No,TSPL_JOURNAL_DETAILS.Account_code as ACCode, TSPL_JOURNAL_DETAILS.Account_Desc as ACName,case when TSPL_JOURNAL_DETAILS.Amount>=0 then  TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt,case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt,TSPL_JOURNAL_DETAILS.Hirerachy_Code as Hirerachy_Code,TSPL_JOURNAL_DETAILS.Cost_Centre_Code as Cost_Centre_Code, VI.Hirerachy_Code3 ,VI.Hirerachy_Code4  " & Environment.NewLine &
                 " FROM TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code  "

                qry = qry + " LEFT OUTER JOIN (select Detail_Line_No,TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code," & Environment.NewLine &
                " TSPL_VENDOR_INVOICE_DETAIL.Cost_Centre_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4, TSPL_VENDOR_INVOICE_HEAD.RefDocNo, TSPL_VENDOR_INVOICE_HEAD.RefDocType " & Environment.NewLine &
                " from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No  " & Environment.NewLine &
                " GROUP BY TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, " & Environment.NewLine &
                " TSPL_VENDOR_INVOICE_DETAIL.Cost_Centre_Code,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4,TSPL_VENDOR_INVOICE_HEAD.RefDocNo,TSPL_VENDOR_INVOICE_HEAD.RefDocType,Detail_Line_No ) VI" & Environment.NewLine &
                "  on VI.Document_No  =TSPL_JOURNAL_MASTER.Source_Doc_No and VI.GL_Account_Code =TSPL_JOURNAL_DETAILS.Account_code AND TSPL_JOURNAL_DETAILS.Detail_Line_No=VI.Detail_Line_No " & Environment.NewLine &
                " left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code =VI.Cost_Centre_Code  left join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code =VI.Hirerachy_Code  where  TSPL_JOURNAL_MASTER.Source_Doc_No = '" & strDocNo & "' " & Environment.NewLine &
                " group by TSPL_JOURNAL_DETAILS.Detail_Line_No ,VI.RefDocNo,VI.RefDocType, TSPL_JOURNAL_MASTER.Source_doc_No ,TSPL_JOURNAL_DETAILS.Account_code , TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_DETAILS.Amount,TSPL_JOURNAL_DETAILS.Hirerachy_Code " & Environment.NewLine &
                " ,TSPL_JOURNAL_DETAILS.Cost_Centre_Code,VI.Hirerachy_Code3,VI.Hirerachy_Code4 " & Environment.NewLine &
                 " )Final left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=final.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code left outer join TSPL_USER_MASTER as CreatedBy on CreatedBy.User_Code=TSPL_VENDOR_INVOICE_HEAD.Created_By left outer join TSPL_USER_MASTER as AuthorisedBy on AuthorisedBy .User_Code=TSPL_VENDOR_INVOICE_HEAD.Modify_By left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code =final.Cost_Centre_Code where 2=2  and TSPL_VENDOR_INVOICE_HEAD.Document_No in ('" & strDocNo & "') " & Environment.NewLine &
                " )xxx     group by Document_No  ,ACCode ,Detail_Line_No " & Environment.NewLine &
                " )xxxx left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =xxxx.Comp_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xxxx.Loc_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = xxxx.Vendor_Code   left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state   order by xxxx.Document_No,xxxx.DrAmt desc,xxxx.CrAmt desc"


                Dim qry1 As String = "select  TSPL_ITEM_MASTER.HSN_Code, TSPL_SRN_DETAIL.Item_Code ,TSPL_SRN_DETAIL.Item_Desc,TSPL_VENDOR_INVOICE_HEAD .Description ,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType   from TSPL_SRN_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SRN_DETAIL.Item_Code where RefDocType ='S'and TSPL_VENDOR_INVOICE_HEAD .Document_No in('" & strDocNo & "') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= ''  "
                Dim frmCRV As New frmCrystalReportViewer()

                frmCRV.funsubreport(CrystalReportFolder.Purchase, qry, qry1, "rptBillCheckListAPInvoice", "AP Invoice")

                frmCRV = Nothing

            ElseIf rdbSRN.Checked = True Then
                Dim strquery As String = "select '" + lbl_PO.Text + "' as voucher_no,'" + lbl_Value.Text + "' as amount,'" + lbl_Remarks.Text + "' as Remarks,'" + txtCheckedBy.Text + "' as CheckedBy,'" + txtProductor.Text + "' as Productor,'" + txtCheckedByAc.Text + "' as CheckedByAc,'" + txtPlantHeadRemarks.Text + "' as PlantHeadRemarks" &
                ",isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103),'') as PurchaseOrder_Date,TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_SRN_HEAD.Against_MRN,tspl_srn_detail.short_Qty, Location_Code,convert(varchar,TSPL_SRN_HEAD.Challan_Date,103) as CHA_Date,TSPL_SRN_DETAIL.Specification as Det_Specification,TSPL_SRN_DETAIL.Remarks  as DetRemarks ,TSPL_VENDOR_MASTER.CST as vndr_cst,TSPL_VENDOR_MASTER.Tin_No as vndr_tin,TSPL_LOCATION_MASTER.CST_No as location_cst,TSPL_SRN_HEAD.Ship_To_Location,TSPL_SHIP_TO_LOCATION.Ship_To_Desc,(TSPL_SHIP_TO_LOCATION.Add1+' '+TSPL_SHIP_TO_LOCATION.add2+' '+TSPL_SHIP_TO_LOCATION.add3) as ship_addr,TSPL_SHIP_TO_LOCATION.City_Code as ship_city,TSPL_SHIP_TO_LOCATION.State as ship_state,TSPL_SRN_DETAIL.MRP,Location_Desc  as Location_Company ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.add4,''))>0 then '- '+isnull(TSPL_LOCATION_MASTER.add4,'') else ' ' end   as company_address,TSPL_LOCATION_MASTER.City_Code as TSPL_LOCATION_MASTER_City_Code ,TSPL_LOCATION_MASTER.State as TSPL_LOCATION_MASTER_state ,TSPL_LOCATION_MASTER.Country as TSPL_LOCATION_MASTER_country, TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as vendor_address, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end+case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then '- '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Location_Desc,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Location_Desc,'') else ' ' end    as address1,TSPL_LOCATION_MASTER .TIN_No ,( case when TSPL_LOCATION_MASTER.Phone2 <> '' then TSPL_LOCATION_MASTER.Phone1 +','+TSPL_LOCATION_MASTER.Phone2 else TSPL_LOCATION_MASTER.Phone1 end) as Location_Phn, TSPL_SRN_HEAD.Description,TSPL_SRN_HEAD.Comments, user_master1.User_Name as Created_By,user_master2.User_Name as Modify_By, TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date,TSPL_VENDOR_MASTER.Vendor_Name, TSPL_VENDOR_MASTER.Tin_No as Vendor_Tin_No, TSPL_VENDOR_MASTER.Phone1 as Vendor_Contact, (case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else SRN_Date end ) as Challan_Date, TSPL_SRN_HEAD.Ref_No  " &
                          "as Challan_No, TSPL_SRN_HEAD.Inv_No, convert(varchar,TSPL_SRN_HEAD.Inv_Date,103) as Inv_Date, TSPL_SRN_HEAD.GRNo,TSPL_SRN_HEAD.Amount_Less_Discount ,TSPL_SRN_HEAD.GENo,TSPL_SRN_HEAD.SRN_Total_Amt, " &
                          "convert(varchar,TSPL_SRN_HEAD.GEDate,103) as GEDate, TSPL_SRN_HEAD.VehicleNo,TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.Carrier,TSPL_SRN_HEAD.Remarks,TSPL_SRN_DETAIL.Landed_Cost_Rate,TSPL_SRN_DETAIL.Landed_Cost_Amount , TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL.UOM_WEIGHT,TSPL_SRN_DETAIL.UOM_WEIGHT_VALUE,TSPL_SRN_DETAIL.Row_Type,TSPL_SRN_DETAIL.Amt_Less_Discount," &
                    "TSPL_SRN_DETAIL.Item_Cost as basicRate,TSPL_SRN_DETAIL.Item_Net_Amt as BasicTotal,TSPL_SRN_DETAIL.Unit_Cost_Tax_Rate as UCTR," &
                    "TSPL_SRN_DETAIL.Unit_Cost_Tax as uctax,TSPL_SRN_DETAIL.Item_Desc,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.SRN_Qty,TSPL_SRN_DETAIL.Rejected_Qty,TSPL_SRN_DETAIL.Short_Qty,TSPL_SRN_DETAIL.GRN_Qty,TSPL_SRN_HEAD.Vendor_Code,TSPL_SRN_HEAD.SRN_Total_Amt,TSPL_SRN_DETAIL.ITEM_COST," &
                     "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " &
                    "tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SRN_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," &
                    "isnull (TSPL_SRN_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SRN_HEAD.tax3_amt,0) as txt3amt," &
                    "tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SRN_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," &
                    "isnull (TSPL_SRN_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SRN_HEAD.tax6_amt,0) as txt6amt " &
                    ",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SRN_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," &
                    "isnull (TSPL_SRN_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SRN_HEAD.tax9_amt,0) as txt9amt," &
                    "tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SRN_HEAD.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname, " &
                    "TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_SRN_DETAIL.SRN_Qty," &
                    "case when tax1.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax1_amt else null end as Tax1Recoverable," &
                    "case when tax2.Tax_Recoverable='Y' then TSPL_SRN_HEAD.TAX2_Amt else null end as Tax2Recoverable, " &
                    "case when tax3.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax3_amt else null end as Tax3Recoverable, " &
                    "case when tax4.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax4_amt else null end as Tax4Recoverable, " &
                    "case when tax5.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax5_amt else null end as Tax5Recoverable, " &
                    "case when tax6.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax6_amt else null end as Tax6Recoverable," &
                    "case when tax7.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax7_amt else null end as Tax7Recoverable, " &
                    "case when tax8.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax8_amt else null end as Tax8Recoverable, " &
                    "case when tax9.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax9_amt else null end as Tax9Recoverable," &
                    "case when tax10.Tax_Recoverable='Y' then TSPL_SRN_HEAD.tax10_amt else null end as Tax10Recoverable, " &
                    "TSPL_SRN_HEAD.TAX1,TSPL_SRN_HEAD.TAX2,TSPL_SRN_HEAD.TAX3,TSPL_SRN_HEAD.TAX4,TSPL_SRN_HEAD.TAX5,TSPL_SRN_HEAD.tax6," &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX1_Rate ,0),103)+'%' as txt1Rate," &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX2_Rate   ,0),103)+'%' as txt2Rate, " &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX3_Rate  ,0),103)+'%' as txt3Rate, " &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX4_Rate  ,0),103)+'%' as txt4Rate, " &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX5_Rate  ,0),103)+'%' as txt5Rate, " &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX6_Rate  ,0),103)+'%' as txt6Rate, " &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX7_Rate  ,0),103)+'%' as txt7Rate, " &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX8_Rate  ,0),103)+'%' as txt8Rate, " &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX9_Rate  ,0),103)+'%' as txt9Rate, " &
                    "convert(varchar,isnull (TSPL_SRN_HEAD.TAX10_Rate  ,0),103)+'%' as txt10Rate," &
                    "TSPL_SRN_DETAIL.Amt_Less_Discount as Value,(select SUM(rejected_qty) from tspl_srn_detail where srn_no=TSPL_SRN_HEAD.SRN_No) as Rej_qty, (select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_SRN_DETAIl left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_SRN_DETAIL.MRN_Id and TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where SRN_No =TSPL_SRN_HEAD.SRN_No and TSPL_MRN_HEAD.IsCancel=0 )as MrnTotQty, (select SUM(SRN_qty) from tspl_srn_detail where srn_no=TSPL_SRN_HEAD.SRN_No) as SRNQtyTotal, (select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" &
                    " ( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= TSPL_SRN_HEAD.SRN_No " &
                    " GROUP by TSPL_PI_DETAIL.PI_No)xxx) as PInvNo  ,    " &
                           " TSPL_SRN_HEAD.Add_Charge_Name1 as Add1Name, " &
                         " TSPL_SRN_HEAD.Add_Charge_Amt1 as Add1 , " &
                         "     TSPL_SRN_HEAD.Add_Charge_Name2 as Add2Name, " &
                         "   TSPL_SRN_HEAD.Add_Charge_Amt2 as Add2 , " &
                         "    TSPL_SRN_HEAD.Add_Charge_Name3 as Add3Name, " &
                         "   TSPL_SRN_HEAD.Add_Charge_Amt3 as Add3 , " &
                         "    TSPL_SRN_HEAD.Add_Charge_Name4 as Add4Name, " &
                         "    TSPL_SRN_HEAD.Add_Charge_Amt4 as Add4 , " &
                         "     TSPL_SRN_HEAD.Add_Charge_Name5 as Add5Name, " &
                          "     TSPL_SRN_HEAD.Add_Charge_Amt5 as Add5 , " &
                          "     TSPL_SRN_HEAD.Add_Charge_Name6 as Add6Name, " &
                          "    TSPL_SRN_HEAD.Add_Charge_Amt6 as Add6 , " &
                          "    TSPL_SRN_HEAD.Add_Charge_Name7 as Add7Name, " &
                          "     TSPL_SRN_HEAD.Add_Charge_Amt7 as Add7 , " &
                          "       TSPL_SRN_HEAD.Add_Charge_Name8 as Add8Name, " &
                          "      TSPL_SRN_HEAD.Add_Charge_Amt8 as Add8 , " &
                           "      TSPL_SRN_HEAD.Add_Charge_Name9 as Add9Name, " &
                           "      TSPL_SRN_HEAD.Add_Charge_Amt9 as Add9 , " &
                           "      TSPL_SRN_HEAD.Add_Charge_Name10 as Add10Name, " &
                           "     TSPL_SRN_HEAD.Add_Charge_Amt10 as Add10,TSPL_SRN_HEAD.Against_RGP,TSPL_SRN_DETAIL .Specification ,TSPL_SRN_HEAD.Against_Requisition ,TSPL_SRN_DETAIL.PO_Qty,TSPL_SRN_DETAIL.GRN_Qty,TSPL_SRN_DETAIL.MRN_Qty,TSPL_SRN_DETAIL.PO_id,TSPL_SRN_DETAIL.Req_No,TSPL_SRN_HEAD.Against_GRN,TSPL_SRN_HEAD.Form_38, "
                strquery += " TSPL_SRN_HEAD.Against_PO "
                strquery += ",'' as Status"
                strquery += ", case when tax1.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax1_amt else null end as Tax1NonRecoverable," &
                                    " case when tax2.Tax_Recoverable='N' then TSPL_SRN_HEAD.TAX2_Amt else null end as Tax2NonRecoverable, " &
                                    " case when tax3.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax3_amt else null end as Tax3NonRecoverable, " &
                                    " case when tax4.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax4_amt else null end as Tax4NonRecoverable, " &
                                    " case when tax5.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax5_amt else null end as Tax5NonRecoverable, " &
                                    " case when tax6.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax6_amt else null end as Tax6NonRecoverable," &
                                    " case when tax7.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax7_amt else null end as Tax7NonRecoverable, " &
                                    " case when tax8.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax8_amt else null end as Tax8NonRecoverable, " &
                                    " case when tax9.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax9_amt else null end as Tax9NonRecoverable," &
                                    " case when tax10.Tax_Recoverable='N' then TSPL_SRN_HEAD.tax10_amt else null end as Tax10NonRecoverable"
                strquery += " FROM  TSPL_SRN_DETAIL INNER JOIN TSPL_SRN_HEAD ON TSPL_SRN_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No " &
                        "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " &
                        "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_SRN_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code " &
                        "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SRN_HEAD.tax1  " &
                        "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SRN_HEAD.tax2 " &
                        "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SRN_HEAD .TAX3 " &
                        "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SRN_HEAD .tax4 " &
                        "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SRN_HEAD .tax5 " &
                        "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SRN_HEAD .TAX6  " &
                        "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SRN_HEAD .TAX7  " &
                        "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SRN_HEAD .TAX8 " &
                        "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SRN_HEAD .TAX9 " &
                        " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SRN_HEAD .TAX10  " &
                        "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SRN_HEAD.Bill_To_Location  " &
                        "left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SRN_HEAD.Ship_To_Location " &
                        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SRN_DETAIL.Item_Code " &
                        " left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state  " &
                        "  left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code " &
                        " left outer join tspl_user_master as user_master1 on user_master1.user_code=TSPL_SRN_HEAD.Created_By  " &
                        " left outer join tspl_user_master as user_master2 on user_master2.user_code=TSPL_SRN_HEAD.Modify_By " &
                        " left join TSPL_PURCHASE_ORDER_HEAD on isnull(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,'') =isnull(TSPL_SRN_DETAIL.PO_ID,'')" &
                    " where TSPL_SRN_HEAD.SRN_No='" & strDocNo & "' "
                strquery = strquery + " order by tspl_srn_detail.line_no"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.Purchase, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptBillCheckListSRN", "Store Receipt Note")
                    frmCRV = Nothing
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        fndDocCode.Value = ""
        lbl_Party.Text = ""
        lbl_PO.Text = ""
        lbl_GRN.Text = ""
        lbl_Value.Text = ""
        lbl_Billno.Text = ""
        lbl_ServiceBillNo.Text = ""
        lbl_Remarks.Text = ""
        txtCheckedBy.Text = ""
        txtProductor.Text = ""
        txtCheckedByAc.Text = ""
        txtPlantHeadRemarks.Text = ""
        gvDocs.DataSource = Nothing
    End Sub


End Class

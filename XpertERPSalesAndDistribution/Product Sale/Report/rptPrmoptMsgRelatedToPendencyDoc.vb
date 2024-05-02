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
Imports System.Data.SqlClient
'================Create by Preeti Gupta=========
Public Class RptPrmoptMsgRelatedToPendencyDoc
#Region "Variables"
    Dim FORMTYPE As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim screenCode As String = Nothing
    Dim documentCode As String = Nothing
    Dim listOfUsers As New ArrayList()
#End Region
    Enum ScreenNames
        Screen_DeclaredDocumentList = 1
        Screen_PendingDocumentList = 2
    End Enum
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExcel.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isPrintFlag
    End Sub
    Public Sub Load_Report()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        Dim createdByForInner As String = ""  ' <-- Never change "" to Nothing , because it is in use further.
        Dim CreatedByForOutter As String = ""  ' <-- Never change "" to Nothing , because it is in use further.
        Dim moduleFilter As String = ""

        If fromDate.Value > ToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From-Date cannot be Greater than To-Date")
            fromDate.Focus()
            Exit Sub
        End If

        Dim intQuery As String = ""
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "admin") = CompairStringResult.Equal Then
            intQuery = clsPendingDeclarationsReport.Load_PendingDocs(fromDate.Value, ToDate.Value, Nothing)
        Else
            intQuery = clsPendingDeclarationsReport.Load_PendingDocs(fromDate.Value, ToDate.Value, txtmultuser.arrValueMember)
            createdByForInner = " and TSPL_PROMPT_MSG_PENDING_HEAD.Created_By in ('" + objCommonVar.CurrentUserCode + "') "
        End If

        Dim objPendingDox As clsPendingDocsPopupHead = New clsPendingDocsPopupHead()
        Dim arrUser As ArrayList = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)

        If txtmultuser.arrValueMember IsNot Nothing AndAlso txtmultuser.arrValueMember.Count > 0 Then
            createdByForInner = " and TSPL_PROMPT_MSG_PENDING_HEAD.Created_By in (" + clsCommon.GetMulcallString(txtmultuser.arrValueMember) + ") "
            CreatedByForOutter = " and endQry.[Declared By] IN (" + clsCommon.GetMulcallString(txtmultuser.arrValueMember) + ") "
        End If
        If txtmultModule.arrValueMember IsNot Nothing AndAlso txtmultModule.arrValueMember.Count > 0 Then
            moduleFilter = " AND endQry.Module in  (" + clsCommon.GetMulcallString(txtmultModule.arrValueMember) + ") "
        End If
        Dim countQry As String = ""
        Dim docsCounts As Integer = 0
        If clsCommon.myLen(createdByForInner) > 0 Then
            countQry = "select  count(pending_doc_Code)  from  TSPL_PROMPT_MSG_PENDING_DETAIL LEFT OUTER JOIN TSPL_PROMPT_MSG_PENDING_HEAD ON TSPL_PROMPT_MSG_PENDING_HEAD.Prompt_code = TSPL_PROMPT_MSG_PENDING_DETAIL.Prompt_code WHERE 1 = 1 and CONVERT(date, TSPL_PROMPT_MSG_PENDING_HEAD.Created_Date, 103) >= CONVERT(date, '" + fromDate.Value + "', 103) AND CONVERT(date, TSPL_PROMPT_MSG_PENDING_HEAD.Created_Date, 103) <= CONVERT(date, '" + ToDate.Value + "', 103)" + createdByForInner
        Else
            countQry = "select  count(pending_doc_Code)  from  TSPL_PROMPT_MSG_PENDING_DETAIL LEFT OUTER JOIN TSPL_PROMPT_MSG_PENDING_HEAD ON TSPL_PROMPT_MSG_PENDING_HEAD.Prompt_code = TSPL_PROMPT_MSG_PENDING_DETAIL.Prompt_code WHERE 1 = 1 and CONVERT(date, TSPL_PROMPT_MSG_PENDING_HEAD.Created_Date, 103) >= CONVERT(date, '" + fromDate.Value + "', 103) AND CONVERT(date, TSPL_PROMPT_MSG_PENDING_HEAD.Created_Date, 103) <= CONVERT(date, '" + ToDate.Value + "', 103)" + createdByForInner
        End If
        If clsCommon.myLen(countQry) > 0 Then
            docsCounts = clsDBFuncationality.getSingleValue(countQry)
        End If
        If docsCounts > 0 Then
            qry = " declare @ListOfDocs table ( documents varchar(max)); insert into @ListOfDocs (documents)  (select  pending_doc_Code  from  TSPL_PROMPT_MSG_PENDING_DETAIL LEFT OUTER JOIN TSPL_PROMPT_MSG_PENDING_HEAD ON TSPL_PROMPT_MSG_PENDING_HEAD.Prompt_code = TSPL_PROMPT_MSG_PENDING_DETAIL.Prompt_code WHERE 1 = 1 " + createdByForInner + " and CONVERT(date, TSPL_PROMPT_MSG_PENDING_HEAD.Created_Date, 103) >= CONVERT(date, '" + fromDate.Value + "', 103) AND CONVERT(date, TSPL_PROMPT_MSG_PENDING_HEAD.Created_Date, 103) <= CONVERT(date, '" + ToDate.Value + "', 103)); select * from ( SELECT ROW_NUMBER() OVER (ORDER BY Module) AS SNO,  fin.Module, fin.Screen, fin.[Doc Code], CONVERT(varchar, fin.[Doc Date], 103) AS [Doc Date],  lower(fin.[Created By]) as [Doc Created By],  ( case when fin.LAST_STATUS = 'pending' then 'pending' else  fin.Status end  ) as [Status], lower(fin.[Declared By]) as [Declared By] , case when isnull(fin.Is_Declare,0) = 0 then 'No' else 'Yes' end [Declare Status], convert(varchar, fin.[Declared Date], 103) as [Declared Date] FROM ( " + intQuery + "  LEFT OUTER JOIN TSPL_PROMPT_MSG_PENDING_DETAIL ON TSPL_PROMPT_MSG_PENDING_DETAIL.PENDING_DOC_CODE = final.[Doc Code] LEFT OUTER JOIN TSPL_PROMPT_MSG_PENDING_HEAD ON TSPL_PROMPT_MSG_PENDING_HEAD.Prompt_code = TSPL_PROMPT_MSG_PENDING_DETAIL.Prompt_code ) AS FIN WHERE 1 = 1  and CONVERT(date, [Declared Date] , 103) >= CONVERT(date, '" + fromDate.Value + "', 103) AND CONVERT(date, [Declared Date] , 103) <= CONVERT(date, '" + ToDate.Value + "', 103) ) as endQry where 1=1 " + CreatedByForOutter + moduleFilter

            If rbtBothStatus.IsChecked Then
                qry += " AND endQry.[Status] IN  ('Posted','Pending')  "
            End If
            If rbtPendingDocs.IsChecked Then
                qry += "  AND endQry.[Status] IN ('Pending')  "
            End If
            If rbtPostedDocs.IsChecked Then
                qry += " AND endQry.[Status] IN ('Posted')  "
            End If

            qry += " order by  Module "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.EnableFiltering = True
                RadPageView1.SelectedPage = RadPageViewPage2
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).IsVisible = True
                    If ii = 0 Then
                        Gv1.Columns(ii).Width = 50
                    Else
                        Gv1.Columns(ii).Width = 150
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
            End If
            Gv1.BestFitColumns()
            ReStoreGridLayout()
        Else
            clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
        End If

    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        txtmultModule.arrValueMember = Nothing
        txtmultuser.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtBothStatus.IsChecked = True
    End Sub
    Private Sub txtmultuser__My_Click(sender As Object, e As EventArgs) Handles txtmultuser._My_Click
        Dim qry As String = "select user_code as Code,user_name as Name from tspl_user_master"
        txtmultuser.arrValueMember = clsCommon.ShowMultipleSelectForm("UserMult", qry, "Code", "Name", txtmultuser.arrValueMember, txtmultuser.arrDispalyMember)
    End Sub
    Private Sub RptPrmoptMsgRelatedToPendencyDoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            Reset()
            'Dim screenType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowPendingDocumentsListScreenOverDeclaredDocumentList, clsFixedParameterType.ShowPendingDocumentsListScreenOverDeclaredDocumentList, Nothing))
            If clsCommon.CompairString(clsUserMgtCode.rptPromptMsgPendindDoc, "PROMPTPENDOC") = CompairStringResult.Equal Then
                Load_ScreenTemplates(ScreenNames.Screen_DeclaredDocumentList)
            Else
                Load_ScreenTemplates(ScreenNames.Screen_PendingDocumentList)
            End If
            listOfUsers = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Load_ScreenTemplates(ByVal screenTypeEnum As [Enum])

        If screenTypeEnum.Equals(ScreenNames.Screen_PendingDocumentList) Then
            Me.Text = "Pending Documents List"
            gbxStatusFilters.Hide()
            lblUser.Hide()
            txtmultuser.Hide()
        Else
            lblUser.Show()
            txtmultuser.Show()
            Dim OnOff As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowDocsStatusFilters, clsFixedParameterType.ShowDocsStatusFilters, Nothing))
            If OnOff = 1 Then
                gbxStatusFilters.Visible = True
                rbtBothStatus.Visible = True
                rbtBothStatus.IsChecked = True
                rbtPendingDocs.Visible = True
                rbtPostedDocs.Visible = True
            Else
                gbxStatusFilters.Visible = False
                rbtBothStatus.Visible = False
                rbtPendingDocs.Visible = False
                rbtPostedDocs.Visible = False
            End If

        End If
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim screenType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowPendingDocumentsListScreenOverDeclaredDocumentList, clsFixedParameterType.ShowPendingDocumentsListScreenOverDeclaredDocumentList, Nothing))
            If clsCommon.CompairString(clsUserMgtCode.rptPromptMsgPendindDoc, "PROMPTPENDOC") = CompairStringResult.Equal Then
                Load_Reports(ScreenNames.Screen_DeclaredDocumentList)
            Else
                Load_Reports(ScreenNames.Screen_PendingDocumentList)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Load_Reports(ByVal enumScreenNames As [Enum])
        If enumScreenNames.Equals(ScreenNames.Screen_DeclaredDocumentList) Then
            Load_Report()
        Else
            Load_PendingDocsList()
        End If
    End Sub
    Public Sub Load_PendingDocsList()
        Try
            Dim arrUser As ArrayList = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)
            Dim obj As clsPendingDocsPopupHead = New clsPendingDocsPopupHead()
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "admin") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                SQLReader_LoadReport(obj.GeneratePendingDocumentsQuery(fromDate.Value, ToDate.Value, arrUser, False))
            Else
                SQLReader_LoadReport(obj.GeneratePendingDocumentsQuery(fromDate.Value, ToDate.Value, arrUser, True))
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub SQLReader_LoadReport(ByVal mySQLQuery As String)
        Dim connetionString As String
        Dim sqlCnn As SqlConnection
        Dim sqlCmd As SqlCommand
        Dim sql As String
        connetionString = objCommonVar.ConnString
        Dim qry As String = " select ROW_NUMBER() OVER (PARTITION BY FFF.[Status] ORDER BY FFF.[MODULE] ) [S.No.] , fff.[Module],  fff.[Screen], fff.[Doc Code] , CONVERT(VARCHAR,fff.[Doc Date],103) [Doc Date] , fff.[Program Code] ,  case when fff.[Status] = 'N' THEN 'Pending' end [Status],  fff.[Created By]  from  ( " + mySQLQuery + " ) as fff where 1=1 "
        If txtmultuser.arrValueMember IsNot Nothing AndAlso txtmultuser.arrValueMember.Count > 0 Then
            qry += " AND FFF.[Created By] IN (" + clsCommon.GetMulcallString(txtmultuser.arrValueMember) + ") "
        End If
        If txtmultModule.arrValueMember IsNot Nothing AndAlso txtmultModule.arrValueMember.Count > 0 Then
            qry += " AND FFF.[Module] in  (" + clsCommon.GetMulcallString(txtmultModule.arrValueMember) + ") "
        End If
        qry += " ORDER BY  FFF.[Module] "
        sql = qry
        sqlCnn = New SqlConnection(connetionString)
        Try
            sqlCnn.Open()
            sqlCmd = New SqlCommand(sql, sqlCnn)
            sqlCmd.CommandTimeout = 600
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader()
            Dim dt As DataTable = New DataTable("ListOfPendingDocs")
            dt.Columns.Add("S.No.", GetType(String))
            dt.Columns.Add("Module", GetType(String))
            dt.Columns.Add("Screen", GetType(String))
            dt.Columns.Add("Doc Code", GetType(String))
            dt.Columns.Add("Doc Date", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("Program Code", GetType(String))
            dt.Columns.Add("Created By", GetType(String))
            While sqlReader.Read()
                Dim dr As DataRow = dt.NewRow()
                dr("S.No.") = sqlReader("S.No.")
                dr("Module") = sqlReader("Module")
                dr("Screen") = sqlReader("Screen")
                dr("Doc Code") = sqlReader("Doc Code")
                dr("Doc Date") = sqlReader("Doc Date")
                dr("Status") = sqlReader("Status")
                dr("Program Code") = sqlReader("Program Code")
                dr("Created By") = sqlReader("Created By")
                dt.Rows.Add(dr)
            End While
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                For Each col As GridViewColumn In Gv1.Columns
                    col.ReadOnly = True
                    col.BestFit()
                Next
                Gv1.EnableFiltering = True
                Gv1.ShowFilteringRow = True
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No data found", Me.Text)
            End If
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", Me.Text)
            End If
            ' stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
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
    Private Sub txtmultModule__My_Click(sender As Object, e As EventArgs) Handles txtmultModule._My_Click
        Dim qry As String = "Select xxx.Code,  xxx.Name From (Select 'Payable' As Code,    'Payable' As Name  Union  Select 'Receivable' As Code,    'Receivable' As Name  Union  Select 'General Ledger' As Code,    'General Ledger' As Name  Union  Select 'Product Sale' As Code,    'Product Sale' As Name  Union  Select 'Fresh Sale' As Code,    'Fresh Sale' As Name  Union  Select 'Purchase' As Code,    'Purchase' As Name  Union  Select 'Bulk Sale' As Code,    'Bulk Sale' As Name  Union  Select 'Bulk Procurement' As Code,    'Bulk Procurement' As Name  Union  Select 'Material Management' As Code,    'Material Management' As Name  Union  Select 'Common Service' As Code,    'Common Service' As Name  Union  Select 'CSA Sale' As Code,    'CSA Sale' As Name  Union  Select 'Process Production' As Code,    'Process Production' As Name  Union  Select 'MCC Milk Procurement' As Code,    'MCC Milk Procurement' As Name  Union  Select 'Export Sale' As Code,    'Export Sale' As Name  Union  Select 'Merchant Trade' As Code,    'Merchant Trade' As Name  Union  Select 'Milk Job Work' As Code,    'Milk Job Work' As Name  Union  Select 'Service' As Code,    'Service' As Name  Union  Select 'Fixed Asset' As Code,    'Fixed Asset' As Name  Union  Select 'Payroll' As Code,    'Payroll' As Name) xxx"
        txtmultModule.arrValueMember = clsCommon.ShowMultipleSelectForm("UserMult", qry, "Code", "Name", txtmultModule.arrValueMember, txtmultModule.arrDispalyMember)
    End Sub
    Public Sub DrillDownGv(ByVal strScreenCode As String, ByVal strDocumentCode As String)
        screenCode = strScreenCode
        documentCode = strDocumentCode
        If clsCommon.myLen(screenCode) > 0 AndAlso clsCommon.myLen(documentCode) > 0 Then

            Select Case screenCode
                Case "AP Invoice Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, documentCode)
                Case "Payment Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, documentCode)
                Case "AR Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, documentCode)
                Case "Receipt Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, documentCode)
                Case "Journal Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, documentCode)
                Case "VCGL"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, documentCode)
                Case "Booking"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBookingProductSale, documentCode)
                Case "Sale order Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderProductSale, documentCode)
                Case "Delivery order Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderProductSale, documentCode)
                Case "Dispatch Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, documentCode)
                Case "Booking Fresh Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBookingEntry, documentCode)
                Case "Invoice Fresh Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, documentCode)
                Case "Purchase Requisition"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseRequistion, documentCode)
                Case "Purchase Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, documentCode)
                Case "Gate Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, documentCode)
                Case "Material Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnMRN, documentCode)
                Case "Store Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, documentCode)
                Case "Purchase Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, documentCode)
                Case "Purchase Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, documentCode)
                Case "RGP/NRGP"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, documentCode)
                Case "Issue/Return Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, documentCode)
                Case "Bulk Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, documentCode)
                Case "Milk SRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, documentCode)
                Case "Milk Purchase Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, documentCode)
                Case "Milk Transfer In"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, documentCode)
                Case "Store Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, documentCode)
                Case "Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, documentCode)
                Case "Contra Vouchers"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, documentCode)
                Case "CSA Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, documentCode)
                Case "Sale Patti"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, documentCode)
                Case "Production Planning"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionPlanningDairy, documentCode)
                Case "Batch Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBatchOrderDairy, documentCode)
                Case "Production Issue Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, documentCode)
                Case "Production Standardization"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, documentCode)
                Case "Stage Process"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, documentCode)
                Case "Production Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, documentCode)
                Case "Milk Sample"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSample, documentCode)
                'Case "Milk Truck Sheet"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MilkTruckSheet, documentCode)
                Case "Tanker Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, documentCode)
                'Case "Tanker Location Change"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchTransfer, documentCode)
                Case "VSP Asset Issue"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, documentCode)
                Case "MCC Material Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, documentCode)
                Case "MCC Material Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, documentCode)
                'Case "VSP Item Issue"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, documentCode)
                Case "Export Sale Quotation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesQuotation, documentCode)
                Case "Export Sales Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesOrder, documentCode)
                Case "Export Proforma Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXPorformaInvoice, documentCode)
                Case "Export Commercial Invoice & Packing List"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXCommercialInvoice, documentCode)
                Case "Export Sales Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, documentCode)
                Case "Export Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, documentCode)
                Case "Merchant Purchase Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmPurchaseOrderMT, documentCode)
                Case "Merchant Sales Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderMT, documentCode)
                Case "Merchant Proforma Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProformaInvoiceMT, documentCode)
                Case "LC Request"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmLCRequest, documentCode)
                Case "LC Creation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmLCCreation, documentCode)
                Case "Document Acceptance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDocumentAcceptance, documentCode)
                Case "Fixed Deposit"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmFixedDeposit, documentCode)
                Case "Merchant Commercial Invoice & Packing List"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCommercialInvoiceMT, documentCode)
                Case "Merchant Sales Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, documentCode)
                Case "Merchant Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, documentCode)
                Case "Milk RGP"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkJobWork, documentCode)
                Case "Milk Gate Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkGateEntry, documentCode)
                Case "Milk Weighment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkWeighment, documentCode)
                Case "Milk Quality Check"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkQualityCheck, documentCode)
                Case "Milk Unloading"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkUnloading, documentCode)
                Case "Milk SRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, documentCode)
                Case "Complaint Detail Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmComplaintDetailEntry, documentCode)
                Case "Service Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssetDistatch, documentCode)
                Case "Cart Maintenance Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCartMaintenanceEntry, documentCode)
                Case "Acquisition Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FAAcquisitionEntry, documentCode)
                Case "Disposal Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FADisposalEntry, documentCode)
                Case "Issue Items to Assemble Assset"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmIssueItemsToAsset, documentCode)
                Case "Assset Work Expanses"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FAAssetWork, documentCode)
                Case "Employee Salary"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEmpSalary, documentCode)
                Case "Leave Application"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLeaveApplication, documentCode)
                Case "Leave Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLeaveAdjustment, documentCode)
                Case "Allowance Details"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAllowanceDetails, documentCode)
                Case "Deduction Details"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDeductionDetails, documentCode)
                Case "Employee Reimbursement"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmReimbursementDetails, documentCode)
                Case "Generate Bonus"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmGenerateBonus, documentCode)
                Case "Loan Generation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLoanGeneration, documentCode)
                Case "Loan Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLoanAdjustment, documentCode)
                Case "Daily Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDailyAttendance, documentCode)
                Case "Hourly Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmHourlyAttendance, documentCode)
                Case "Monthly Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMonthlyAttendance, documentCode)
                Case "Employee Adjustment Voucher"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAdjustmentVoucher, documentCode)
                Case "Salary Generation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalaryGeneration, documentCode)
                Case "LTA Claim"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLTAClaim, documentCode)
                Case "Employee Mediclaim Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMediclaimEntry, documentCode)
                Case "Full And Final Settlement"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmFullAndFinalSettlement, documentCode)
                Case "Employee Shift Change"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEmployeeShiftChange, documentCode)
                Case "Employee Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmEmployeeTransfer, documentCode)
                Case "Employee Increment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmEmpIncrement, documentCode)
                Case "Reverse Transaction"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, documentCode)
            End Select
        End If
    End Sub
    Public Sub DrillDown_ShowDialog(ByVal strScreenCode As String, ByVal strDocumentCode As String)

        If clsCommon.myLen(strScreenCode) > 0 AndAlso clsCommon.myLen(strDocumentCode) > 0 Then
            Select Case strScreenCode
                Case "AP Invoice Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strDocumentCode)
                Case "Payment Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, strDocumentCode)
                Case "AR Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, strDocumentCode)
                Case "Receipt Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, strDocumentCode)
                Case "Journal Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strDocumentCode)
                Case "VCGL"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, strDocumentCode)
                Case "Booking"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBookingProductSale, strDocumentCode)
                Case "Sale order Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderProductSale, strDocumentCode)
                Case "Delivery order Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderProductSale, strDocumentCode)
                Case "Dispatch Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strDocumentCode)
                Case "Booking Fresh Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBookingEntry, strDocumentCode)
                Case "Invoice Fresh Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, strDocumentCode)
                Case "Purchase Requisition"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseRequistion, strDocumentCode)
                Case "Purchase Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, strDocumentCode)
                Case "Gate Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, strDocumentCode)
                Case "Material Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnMRN, strDocumentCode)
                Case "Store Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strDocumentCode)
                Case "Purchase Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strDocumentCode)
                Case "Purchase Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strDocumentCode)
                Case "RGP/NRGP"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strDocumentCode)
                Case "Issue/Return Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strDocumentCode)
                Case "Bulk Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strDocumentCode)
                Case "Milk SRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strDocumentCode)
                Case "Milk Purchase Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, strDocumentCode)
                Case "Milk Transfer In"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strDocumentCode)
                Case "Store Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strDocumentCode)
                Case "Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strDocumentCode)
                Case "Contra Vouchers"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, strDocumentCode)
                Case "CSA Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strDocumentCode)
                Case "Sale Patti"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strDocumentCode)
                Case "Production Planning"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionPlanningDairy, strDocumentCode)
                Case "Batch Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBatchOrderDairy, strDocumentCode)
                Case "Production Issue Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strDocumentCode)
                Case "Production Standardization"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strDocumentCode)
                Case "Stage Process"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strDocumentCode)
                Case "Production Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strDocumentCode)
                Case "Milk Sample"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSample, strDocumentCode)
                'Case "Milk Truck Sheet"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MilkTruckSheet, strDocumentCode)
                Case "Tanker Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strDocumentCode)
                'Case "Tanker Location Change"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchTransfer, strDocumentCode)
                Case "VSP Asset Issue"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, strDocumentCode)
                Case "MCC Material Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, strDocumentCode)
                Case "MCC Material Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, strDocumentCode)
                Case "VSP Item Issue"
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strDocumentCode)
                    'Case "Export Sale Quotation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesQuotation, strDocumentCode)
                Case "Export Sales Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesOrder, strDocumentCode)
                Case "Export Proforma Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXPorformaInvoice, strDocumentCode)
                Case "Export Commercial Invoice & Packing List"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXCommercialInvoice, strDocumentCode)
                Case "Export Sales Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, strDocumentCode)
                Case "Export Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, strDocumentCode)
                Case "Merchant Purchase Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmPurchaseOrderMT, strDocumentCode)
                Case "Merchant Sales Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderMT, strDocumentCode)
                Case "Merchant Proforma Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProformaInvoiceMT, strDocumentCode)
                Case "LC Request"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmLCRequest, strDocumentCode)
                Case "LC Creation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmLCCreation, strDocumentCode)
                Case "Document Acceptance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDocumentAcceptance, strDocumentCode)
                Case "Fixed Deposit"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmFixedDeposit, strDocumentCode)
                Case "Merchant Commercial Invoice & Packing List"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCommercialInvoiceMT, strDocumentCode)
                Case "Merchant Sales Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, strDocumentCode)
                Case "Merchant Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, strDocumentCode)
                Case "Milk RGP"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkJobWork, strDocumentCode)
                Case "Milk Gate Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkGateEntry, strDocumentCode)
                Case "Milk Weighment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkWeighment, strDocumentCode)
                Case "Milk Quality Check"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkQualityCheck, strDocumentCode)
                Case "Milk Unloading"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkUnloading, strDocumentCode)
                Case "Milk SRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, strDocumentCode)
                Case "Complaint Detail Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmComplaintDetailEntry, strDocumentCode)
                Case "Service Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssetDistatch, strDocumentCode)
                Case "Cart Maintenance Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCartMaintenanceEntry, strDocumentCode)
                Case "Acquisition Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FAAcquisitionEntry, strDocumentCode)
                Case "Disposal Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FADisposalEntry, strDocumentCode)
                Case "Issue Items to Assemble Assset"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmIssueItemsToAsset, strDocumentCode)
                Case "Assset Work Expanses"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FAAssetWork, strDocumentCode)
                Case "Employee Salary"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEmpSalary, strDocumentCode)
                Case "Leave Application"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLeaveApplication, strDocumentCode)
                Case "Leave Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLeaveAdjustment, strDocumentCode)
                Case "Allowance Details"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAllowanceDetails, strDocumentCode)
                Case "Deduction Details"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDeductionDetails, strDocumentCode)
                Case "Employee Reimbursement"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmReimbursementDetails, strDocumentCode)
                Case "Generate Bonus"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmGenerateBonus, strDocumentCode)
                Case "Loan Generation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLoanGeneration, strDocumentCode)
                Case "Loan Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLoanAdjustment, strDocumentCode)
                Case "Daily Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDailyAttendance, strDocumentCode)
                Case "Hourly Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmHourlyAttendance, strDocumentCode)
                Case "Monthly Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMonthlyAttendance, strDocumentCode)
                Case "Employee Adjustment Voucher"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAdjustmentVoucher, strDocumentCode)
                Case "Salary Generation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalaryGeneration, strDocumentCode)
                Case "LTA Claim"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLTAClaim, strDocumentCode)
                Case "Employee Mediclaim Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMediclaimEntry, strDocumentCode)
                Case "Full And Final Settlement"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmFullAndFinalSettlement, strDocumentCode)
                Case "Employee Shift Change"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEmployeeShiftChange, strDocumentCode)
                Case "Employee Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmEmployeeTransfer, strDocumentCode)
                Case "Employee Increment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmEmpIncrement, strDocumentCode)
                Case "Reverse Transaction"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, strDocumentCode)
            End Select

        End If
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Dim tempQry As String = Nothing
        Dim tmpDt As DataTable = Nothing
        Try
            Dim screenType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowPendingDocumentsListScreenOverDeclaredDocumentList, clsFixedParameterType.ShowPendingDocumentsListScreenOverDeclaredDocumentList, Nothing))
            If screenType = 1 Then
                If clsCommon.CompairString(e.Column.Name, "Doc Code") = CompairStringResult.Equal Then
                    DrillDown_ShowDialog(clsCommon.myCstr(Gv1.CurrentRow.Cells("Screen").Value), clsCommon.myCstr(Gv1.CurrentRow.Cells("Doc Code").Value))
                    If listOfUsers IsNot Nothing AndAlso listOfUsers.Count > 0 Then
                    Else
                        listOfUsers = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)
                    End If
                    If clsCommon.CompairString(objCommonVar.CurrentUserCode, "admin") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                        tempQry = clsPendingDocsPopupHead.GetPostStatus(fromDate.Value, ToDate.Value, listOfUsers, clsCommon.myCstr(Gv1.CurrentRow.Cells("Doc Code").Value), False)
                    Else
                        tempQry = clsPendingDocsPopupHead.GetPostStatus(fromDate.Value, ToDate.Value, listOfUsers, clsCommon.myCstr(Gv1.CurrentRow.Cells("Doc Code").Value), True)
                    End If
                    tmpDt = clsDBFuncationality.GetDataTable(tempQry)
                    If tmpDt IsNot Nothing AndAlso tmpDt.Rows.Count > 0 Then
                        For Each r As DataRow In tmpDt.Rows
                            If r("Status") = "Y" Then
                                Gv1.Rows.Remove(Gv1.Rows(e.RowIndex))
                            End If
                        Next
                    End If
                End If
            Else
                If clsCommon.CompairString(e.Column.Name, "Doc Code") = CompairStringResult.Equal Then
                    screenCode = clsCommon.myCstr(Gv1.CurrentRow.Cells("Screen").Value)
                    documentCode = clsCommon.myCstr(Gv1.CurrentRow.Cells("Doc Code").Value)
                    If screenCode IsNot Nothing AndAlso documentCode IsNot Nothing Then
                        DrillDownGv(screenCode, documentCode)
                    Else
                        clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExpExcel_Click(sender As Object, e As EventArgs) Handles ExpExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPromptMsgPendindDoc & "'"))
                If txtmultuser.arrDispalyMember IsNot Nothing AndAlso txtmultuser.arrDispalyMember.Count > 0 Then
                    arrHeader.Add(" User : " + clsCommon.GetMulcallStringWithComma(txtmultuser.arrDispalyMember))
                End If
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'Dim dateSuffix As String = DateTime.Now.ToString()
                'If dateSuffix.Contains(":") Or dateSuffix.Contains("/") Then
                '    dateSuffix = dateSuffix.Replace(":", "_").Replace("/", "_")
                'End If
                'sfd.FileName = String.Concat(Me.Text, "_", dateSuffix)
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow("No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

         Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPromptMsgPendindDoc & "'"))
            If txtmultuser.arrDispalyMember IsNot Nothing AndAlso txtmultuser.arrDispalyMember.Count > 0 Then
                arrHeader.Add("User : " + clsCommon.GetMulcallStringWithComma(txtmultuser.arrDispalyMember))
            End If
            If txtmultModule.arrDispalyMember IsNot Nothing AndAlso txtmultModule.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Module : " + clsCommon.GetMulcallStringWithComma(txtmultModule.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class

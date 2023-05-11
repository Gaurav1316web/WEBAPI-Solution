'------------------created by Monika 29/04/2014-----------------------------
Imports common
Imports System.Net
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.IO

Public Class FrmMailSMSSettingNew2

#Region "Variables"
    Public FormId As String = Nothing

    ''----------------complaint detail entry-------------
    'Public Const Complnt_code As String = "$comp_id$"
    'Public Const Assetcode As String = "$item_code$"
    'Public Const outlet As String = "$cust_code$"
    'Public Const complnt_date As String = "$comp_date$"
    'Public Const SerivceDealer As String = "$Executive_Code$"
    ''---------------------------------------------------


    ''----------------HR EM Resignation Letter-------------
    'Public Const doccode As String = "$doccode$"
    'Public Const docdate As String = "$docdate$"
    'Public Const EmpCode As String = "$EmpCode$"
    'Public Const EmpName As String = "$EmpName$"
    'Public Const DepCode As String = "$DepCode$"
    'Public Const DepName As String = "$DepName$"
    'Public Const ResonOfResignation As String = "$ResonOfResignation$"
    'Public Const ResignationDate As String = "$ResignationDate$"
    'Public Const Remarks As String = "$Remarks$"
    'Public Const HandoverCode As String = "$HandoverCode$"
    'Public Const HandoverName As String = "$HandoverName$"
    ''---------------------------------------------------

    ''----------------Sale Order------------------------
    'Public Const SaleOrderNo As String = "$DocNo$"
    'Public Const SaleOrderDate As String = "$DocDate$"
    'Public Const VendorNo As String = "$VendorNo$"
    'Public Const VendorName As String = "$VendorName$"
    'Public Const ContactPerson As String = "$ContactPerson$"
    'Public Const TotalAmount As String = "$TotalAmount$"
    ''------------------------------------------------------

    ''----------------Delivery Note Fresh Sale------------------------
    'Public Const DeliveryNo As String = "$DocNo$"
    'Public Const DeliveryDate As String = "$DocDate$"
    'Public Const LocationCode As String = "$LocationCode$"
    'Public Const LocationName As String = "$LocationName$"
    'Public Const BookingNo As String = "$BookingNo$"
    ''------------------------------------------------------

    'Public Const CustomerNo As String = "$CustomerNo$"
    'Public Const CustomerName As String = "$CustomerName$"
    'Public Const InvoiceNo As String = "$Purchase InvoiceNo$"

    ''---------------Sale register------------------
    'Public Const FromDate As String = "$From Date$"
    'Public Const ToDate As String = "$To Date$"
    'Public Const ReportType As String = "$Summary Or Detail$"
    'Public Const InvoiceType As String = "$Invoice Type$"
    ''----------------Purchase Requistion------------------------
    'Public Const PurchaseRequisitionNo As String = "$PurchaseRequisitionNo$"
    'Public Const PurchaseRequisitionDate As String = "$PurchaseRequisitionDate$"

    ''Public Const VendorNo As String = "$VendorNo$"
    ''Public Const VendorName As String = "$VendorName$"
    ''Public Const ContactPerson As String = "$ContactPerson$"
    ''Public Const TotalAmount As String = "$TotalAmount$"
    ''------------------------------------------------------
    ''---------------Quality Check------------------
    'Public Const QcNo As String = "$QC No$"
    'Public Const inDateTime As String = "$In Date Time$"
    'Public Const outDateTime As String = "$Out Date Time$"

    'Public Const Form_Code As String = "$FormId$"
    'Public Const UserCode As String = "$UserCode$"

    ''-------------RFQ---------------------
    'Public RFQ_No As String = "$DOC No$"
    'Public RFQ_Date As String = "$DOC DATE$"
    'Public Request_No As String = "$REQ NO$"
    'Public Request_Date As String = "$REQ Date$"
    'Public Request_Amount As String = "$REQ Amt$"
    ''-----------------------------------------------------

    ' '' Anubhooti 25-Aug-2014 BM00000003528
    ''-------------Offer Letter HR---------------------
    'Public App_No As String = "$Applicant No$"
    'Public Offer_Date As String = "$Offer Date$"
    'Public DOJ As String = "$DOJ$"
    'Public Salary As String = "$Salary$"
    'Public ApplicantName As String = "$Applicant Name$"
    ' '' Anubhooti 25-Aug-2014 BM00000003528
    ''-------------Appointment Letter HR---------------------
    'Public Appointment_Date As String = "$Appointment Date$"
    ''-----------------------------------------------------

    ''=================CSA DO====================
    'Public DOC_NO As String = "$Document No$"
    'Public DOC_Date As String = "$Document Date$"
    'Public Cust_Name As String = "$CSA Name$"
    'Public From_Location As String = "$From Location$"
    'Public RT_Detail As String = "$RT Rate And UOM$"
    'Public CSA_Item_Type As String = "$CSA Item Type$"
    'Public Doc_Amount As String = "$Document Amount$"

    ''-------------Leave Application---------------------
    'Public Leave_App_No As String = "$Application No$"
    'Public Application_Date As String = "$Application Date$"
    'Public Leave_From As String = "$Leave From$"
    'Public Leave_To As String = "$Leave To$"
    'Public Leave_Type As String = "$Leave Type$"
    'Public Leave_Days As String = "$Leave Days$"
    'Public Leave_Reason As String = "$Leave Reason$"
    'Public Employee_Name As String = "$Employee Name$"
    'Public EMP_CODE As String = "$Employee Code$"

    ''-------------Employeee Master---------------------
    'Public Birth_Date As String = "$Birth Date$"
    'Public AnniversaryDate As String = "$Anniversary Date$"
    'Public ProbPeriodEnDate As String = "$Probation Period End Date$"

    ''----------------Milk Shift End------------------------
    'Public Const Doc_Code As String = "$DocNo$"
    ''Public Const Doc_Date As String = "$DocDate$"
    'Public Const Mcc_Code As String = "$Mcc_Code$"
    'Public Const Mcc_Name As String = "$Mcc_Name$"
    'Public Const Shift As String = "$Shift$"
    'Public User_Code As String = "$Created_By$"
    'Public State_Name As String = "$State$"
    'Public Total_collection As String = "$Total_collection$"
    'Public UOM As String = "$UOM$"

    ''----------------MCC Master------------------------
    'Public Const Shift_Open_Time As String = "$Shift_Open_Time$"
    'Public Const Total_Route As String = "$Total_Route$"
    'Public Const Total_Vlc As String = "$Total_Vlc$"
    'Public Const Shift_Close_Time As String = "$Shift_Close_Time$"
    ''------------------------------------------------------
    
#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtEmailText.Text) <= 0 AndAlso clsCommon.myLen(txtsms.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Fill Any Of The Setting E-Mail/SMS", Me.Text)
                Return False
            End If

            If clsCommon.myLen(txtEmailText.Text) > 0 AndAlso clsCommon.myLen(txtmailsub.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Fill Subject For E-Mail", Me.Text)
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Sub SaveDate()
        Try
            Dim obj As New clsEmailSMSSettingNew()
            obj.mailbody = txtEmailText.Text.Replace("'", "`")

            If clsCommon.myLen(obj.mailbody) > 2000 Then
                obj.mailbody = obj.mailbody.Substring(0, 2000)
            End If

            obj.mailsubjct = txtmailsub.Text.Replace("'", "`")

            If clsCommon.myLen(obj.mailsubjct) > 200 Then
                obj.mailsubjct = obj.mailsubjct.Substring(0, 200)
            End If

            obj.smsbody = txtsms.Text.Replace("'", "`")

            If clsCommon.myLen(obj.smsbody) > 2000 Then
                obj.smsbody = obj.smsbody.Substring(0, 2000)
            End If

            If ChkAttachment.Checked Then
                obj.atchmnt = "Y"
            Else
                obj.atchmnt = "N"
            End If
            obj.Formid = FormId

            obj.usercode = ""
            Dim ii As Integer = 0
            Dim counter As Integer = 0
            If rbtnSelect.IsChecked Then
                For ii = 0 To cbguser.Rows.Count - 1
                    Try
                        If cbguser.Rows(ii).Cells("Select").Value = True Then
                            obj.usercode = obj.usercode + "','" + cbguser.Rows(ii).Cells("Code").Value
                            counter += 1
                        End If
                    Catch ex1 As Exception
                    End Try
                Next
            End If
            

            If (RadPageViewPage3.Item.Visibility = ElementVisibility.Visible) AndAlso rbtnSelect.IsChecked = True AndAlso counter <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select atleast One User For Sending Mail", Me.Text)
                Return
            End If

            If clsEmailSMSSettingNew.SaveData(obj) Then
                clsCommon.MyMessageBoxShow("Data Save Successfully", Me.Text)
            Else
                clsCommon.MyMessageBoxShow("Check The Data You Insert", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveDate()
    End Sub

    Sub LoadUser()
        Dim qry As String = "select distinct emp_code as Code,emp_name as [User Name],birth_date as DOB,joining_date as [DOJ] from TSPL_EMPLOYEE_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt.Columns.Add("Select", GetType(Boolean))

        cbguser.DataSource = Nothing
        cbguser.DataSource = dt

        cbguser.Columns("Code").Width = 80
        cbguser.Columns("Code").ReadOnly = True

        cbguser.Columns("User Name").Width = 180
        cbguser.Columns("User Name").ReadOnly = True

        cbguser.Columns("DOB").Width = 80
        cbguser.Columns("DOB").ReadOnly = True

        cbguser.Columns("DOJ").Width = 80
        cbguser.Columns("DOJ").ReadOnly = True

        cbguser.AllowDeleteRow = False
        cbguser.AllowAddNewRow = False
        cbguser.ShowGroupPanel = False
        cbguser.AllowColumnReorder = False
        cbguser.AllowRowReorder = False
        cbguser.EnableSorting = False
        cbguser.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        cbguser.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub FrmMailSMSSettingNew2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage3.Item.Visibility = ElementVisibility.Visible

        rbtnAll.IsChecked = True

        If clsCommon.CompairString(FormId, clsUserMgtCode.FrmSaleRegisterDemo) = CompairStringResult.Equal Then
            RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
            
        End If
        LoadUser()
        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(FormId)

        If clsCommon.CompairString(FormId, clsUserMgtCode.frmComplaintDetailEntry) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Complnt_code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.complnt_date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Assetcode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.outlet)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.SerivceDealer)
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmCSADeliveryOrder) = CompairStringResult.Equal OrElse clsCommon.CompairString(FormId, clsUserMgtCode.frmCSASaleInvoice) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DOC_NO)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DOC_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Cust_Name)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.From_Location)
            If Not clsCommon.CompairString(FormId, clsUserMgtCode.frmCSASaleInvoice) = CompairStringResult.Equal Then
                ContextMenuStrip1.Items.Add(clsEmailSMSConstants.RT_Detail)
                ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CSA_Item_Type)
            End If
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Doc_Amount)
        ElseIf (clsCommon.CompairString(FormId, clsUserMgtCode.frmEXCommercialInvoice) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmEXPorformaInvoice) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmEXSalesOrder) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmEXSalesQuotation) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmSNShipment) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmSNSalesOrder) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmSNSaleInvoice) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmSNSaleReturn) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.FrmDispatchFreshSale) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmInvoiceFreshSale) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmSaleReturnFreshSale) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmBookingProductSale) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmDeliveryPrderProductSale) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmSaleInvoiceProductSale) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmShipmentProductSale) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmSaleReturnProductSale) = CompairStringResult.Equal) Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.SaleOrderNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.SaleOrderDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CustomerNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CustomerName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Form_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.UserCode)
            If Not (clsCommon.CompairString(FormId, clsUserMgtCode.frmSNSaleReturn) = CompairStringResult.Equal) AndAlso Not (clsCommon.CompairString(FormId, clsUserMgtCode.frmSNShipment) = CompairStringResult.Equal) Then
                ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ContactPerson)
            End If
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.TotalAmount)
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmDeliveryNoteFreshSale) = CompairStringResult.Equal OrElse clsCommon.CompairString(FormId, "DEL-NOT-FSCR") = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DeliveryNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DeliveryDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CustomerNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CustomerName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.LocationCode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.LocationName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.BookingNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.TotalAmount)
        ElseIf (clsCommon.CompairString(FormId, clsUserMgtCode.mbtnPurchaseReturn) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.mbtnPurchaseInvoice) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal) Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.SaleOrderNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.SaleOrderDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.VendorNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.VendorName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Form_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.UserCode)
            If (clsCommon.CompairString(FormId, clsUserMgtCode.mbtnPurchaseOrder) = CompairStringResult.Equal) Then
            Else
                ContextMenuStrip1.Items.Add(clsEmailSMSConstants.InvoiceNo)
            End If
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CustomerNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CustomerName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.TotalAmount)
            ''richa 11/07/2014
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.PurchaseRequisitionNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.PurchaseRequisitionDate)

            ''
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.FrmSaleRegisterDemo) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.FromDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ToDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.InvoiceType)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ReportType)

            '============Added By Preeti Gupta============
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmResignationLetter) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Doc_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DOC_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.EmpCode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.EmpName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DepCode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DepName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ResonOfResignation)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ResignationDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Remarks)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.HandoverCode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.HandoverName)

            '================End============
        ElseIf (clsCommon.CompairString(FormId, clsUserMgtCode.frmSaleQuotation) = CompairStringResult.Equal) Or (clsCommon.CompairString(FormId, clsUserMgtCode.frmEXSalesQuotation) = CompairStringResult.Equal) Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.SaleOrderNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.SaleOrderDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Form_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.UserCode)
        ElseIf (clsCommon.CompairString(FormId, clsUserMgtCode.frmQualityCheck) = CompairStringResult.Equal) Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.QcNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.inDateTime)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.outDateTime)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.VendorNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.VendorName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.LocationCode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.LocationName)
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.RFQ) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.RFQ_No)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.RFQ_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Request_No)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Request_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Request_Amount)

            '' Anubhooti 25-Aug-2014 (For Offer/Appointment Letter HR BM00000003528)
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmOfferLetterHR) = CompairStringResult.Equal Or clsCommon.CompairString(FormId, clsUserMgtCode.frmAppointmentLetterHR) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.App_No)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DOJ)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Salary)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ApplicantName)
            RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
            ChkAttachment.Visible = False
            If clsCommon.CompairString(FormId, clsUserMgtCode.frmOfferLetterHR) = CompairStringResult.Equal Then
                ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Offer_Date)
            Else
                ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Appointment_Date)
            End If

            '' Anubhooti 20-Oct-2015 (For Service Call SW BM00000008219)
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.FrmServiceCall) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Call_No)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Call_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Problem_Type)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Subject)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ItemPartNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.IssueNotice)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.AssignedTo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.AssignedBy)
            ' RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
            ChkAttachment.Visible = False
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmLeaveApplication) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Leave_App_No)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Application_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Leave_Days)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Leave_From)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Leave_To)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Leave_Type)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Leave_Reason)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.EMP_CODE)

            RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
            ChkAttachment.Visible = False
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmEmployee_Master) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.EMP_CODE)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Birth_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.AnniversaryDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ProbPeriodEnDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Employee_Name)
            
            RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
            ChkAttachment.Visible = False
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmMilkShiftEndMCC) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Doc_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DOC_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Mcc_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Mcc_Name)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Shift)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.State_Name)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Total_collection)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Total_Route)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.UOM)

            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.FAT)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.SNF)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Rate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Amount)

            ''richa agarwal against ticket no BM00000008361
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmMilkShiftEndMCC + "VSP") = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CompanyName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DOC_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Shift)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.VLCCode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.VLCUploaderCode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.VLCName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CowQty)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.BuffaloQty)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CowFat)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.BuffaloFat)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CowSNF)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.BuffaloSNF)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CowAmount)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.BuffaloAmount)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Total_collection)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.TotalAmount)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.UOM)
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.frmMCCMaster) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Doc_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DOC_Date)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Mcc_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Mcc_Name)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Shift)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.State_Name)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Total_collection)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Total_Route)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Total_Vlc)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Shift_Open_Time)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Shift_Close_Time)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.UOM)

            ' RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
            ChkAttachment.Visible = False
        ElseIf clsCommon.CompairString(FormId, clsUserMgtCode.FrmDispatchBulkSale) = CompairStringResult.Equal Then
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DeliveryNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.DeliveryDate)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CustomerNo)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.CustomerName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.LocationCode)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.LocationName)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.TotalAmount)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.Form_Code)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.ContactPerson)
            ContextMenuStrip1.Items.Add(clsEmailSMSConstants.UserCode)

        End If

        If obj IsNot Nothing Then
            txtmailsub.Text = obj.mailsubjct
            txtEmailText.Text = obj.mailbody
            txtsms.Text = obj.smsbody

            If obj.atchmnt = "Y" Then
                ChkAttachment.Checked = True
            Else
                ChkAttachment.Checked = False
            End If

            Dim ii As Integer = 0
            Dim jj As Integer = 0
            Dim j As Integer = 0
            Dim struser() As String
            obj.usercode = obj.usercode.Replace("'", "")

            Try
                If obj.usercode.Substring(0, 1) = "," Then
                    obj.usercode = obj.usercode.Substring(1, obj.usercode.Length - 1)
                End If
            Catch ex As Exception
            End Try

            struser = obj.usercode.Split(",")
            j = struser.Length

            rbtnSelect.IsChecked = False
            rbtnAll.IsChecked = True
            If clsCommon.myLen(obj.usercode) > 0 Then
                While (j > 0)
                    For ii = 0 To cbguser.Rows.Count - 1
                        Try
                            If cbguser.Rows(ii).Cells("Code").Value = struser(jj) Then
                                cbguser.Rows(ii).Cells("Select").Value = True
                                rbtnSelect.IsChecked = True
                                rbtnAll.IsChecked = False
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    jj += 1
                    j -= 1
                End While
            End If
        End If


    End Sub

    Private Sub ContextMenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked

        If RadPageView1.SelectedPage Is RadPageViewPage1 Then
            txtEmailText.Text = txtEmailText.Text.Insert(txtEmailText.SelectionStart, " " + e.ClickedItem.Text)
        ElseIf RadPageView1.SelectedPage Is RadPageViewPage2 Then
            txtsms.Text = txtsms.Text.Insert(txtsms.SelectionStart, " " + e.ClickedItem.Text)
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

    End Sub

    Private Sub rbtnAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAll.ToggleStateChanged, rbtnSelect.ToggleStateChanged
        cbguser.Enabled = rbtnSelect.IsChecked
    End Sub
End Class

'26/12/2012-11:45AM---Updation By--Pankaj Kumar---Applied Validations
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.IO

Public Class FrmTransactionCancelScreen
    Inherits FrmMainTranScreen
    Const ReportID As String = "FrmTransactionCancelScreenGrid"
    Dim tran As SqlTransaction
    Dim userCode, companyCode, Qry As String
    Public IsLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public clicked As Boolean = False
    Public IsNewEntry As Boolean = True
    Dim arrDocNo As List(Of String)

    Public Sub New(ByVal user As String, ByVal company As String)
        IsLoadData = True
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub FrmTransactionCancelScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsLoadData = False
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        Get_type()
        fillData("")
        'funReset()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub LoadBlankGrid()
        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()
        MasterTemplate.Columns.Clear()

        MasterTemplate.AllowDeleteRow = True
        MasterTemplate.AllowAddNewRow = False

        Dim gvSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvSelect.FormatString = ""
        gvSelect.Name = "gvSelect"
        gvSelect.Width = 30
        gvSelect.ReadOnly = False
        gvSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        MasterTemplate.MasterTemplate.Columns.Add(gvSelect)

        Dim gvComplaintId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvComplaintId.FormatString = ""
        gvComplaintId.HeaderText = "Complaint Id"
        gvComplaintId.Name = "gvComplaintId"
        gvComplaintId.Width = 100
        gvComplaintId.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvComplaintId)

        Dim gvComplaintDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        gvComplaintDate.FormatString = ""
        gvComplaintDate.HeaderText = "Complaint Date"
        gvComplaintDate.Name = "gvComplaintDate"
        gvComplaintDate.Width = 100
        gvComplaintDate.ReadOnly = True
        gvComplaintDate.CustomFormat = "dd/MMM/yyyy"
        MasterTemplate.MasterTemplate.Columns.Add(gvComplaintDate)

        Dim gvComplaintdescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvComplaintdescription.FormatString = ""
        gvComplaintdescription.HeaderText = "Description"
        gvComplaintdescription.Name = "gvComplaintdescription"
        gvComplaintdescription.Width = 200
        gvComplaintdescription.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvComplaintdescription)

        Dim gvOutLetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvOutLetCode.FormatString = ""
        gvOutLetCode.HeaderText = "Outlet Code"
        gvOutLetCode.Name = "gvOutLetCode"
        gvOutLetCode.Width = 100
        gvOutLetCode.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvOutLetCode)

        Dim gvOutLetname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvOutLetname.FormatString = ""
        gvOutLetname.HeaderText = "Outlet Name"
        gvOutLetname.Name = "gvOutLetname"
        gvOutLetname.Width = 100
        gvOutLetname.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvOutLetname)

        Dim gvType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvType.FormatString = ""
        gvType.HeaderText = "Type"
        gvType.Name = "gvType"
        gvType.Width = 100
        gvType.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvType)

        Dim gvCity As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvCity.FormatString = ""
        gvCity.HeaderText = "City"
        gvCity.Name = "gvCity"
        gvCity.Width = 100
        gvCity.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvCity)

        Dim gvState As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvState.FormatString = ""
        gvState.HeaderText = "State"
        gvState.Name = "gvState"
        gvState.Width = 100
        gvState.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvState)

        Dim gvCountry As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvCountry.FormatString = ""
        gvCountry.HeaderText = "Country"
        gvCountry.Name = "gvCountry"
        gvCountry.Width = 100
        gvCountry.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvCountry)

        Dim gvLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvLocation.FormatString = ""
        gvLocation.HeaderText = "Location"
        gvLocation.Name = "gvLocation"
        gvLocation.Width = 100
        gvLocation.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvLocation)

        Dim gvPhoneNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvPhoneNo.FormatString = ""
        gvPhoneNo.HeaderText = "Phone No"
        gvPhoneNo.Name = "gvPhoneNo"
        gvPhoneNo.Width = 100
        gvPhoneNo.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvPhoneNo)

        Dim gvAssetType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvAssetType.FormatString = ""
        gvAssetType.HeaderText = "Asset Type Code"
        gvAssetType.Name = "gvAssetType"
        gvAssetType.Width = 100
        gvAssetType.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvAssetType)

        Dim gvAssetTypeDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvAssetTypeDescription.FormatString = ""
        gvAssetTypeDescription.HeaderText = "Asset Type Description"
        gvAssetTypeDescription.Name = "gvAssetTypeDescription"
        gvAssetTypeDescription.Width = 100
        gvAssetTypeDescription.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvAssetTypeDescription)

        Dim gvMake As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvMake.FormatString = ""
        gvMake.HeaderText = "Make"
        gvMake.Name = "gvMake"
        gvMake.Width = 100
        gvMake.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvMake)

        Dim gvModel As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvModel.FormatString = ""
        gvModel.HeaderText = "Model"
        gvModel.Name = "gvModel"
        gvModel.Width = 100
        gvModel.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvModel)

        Dim gvSize As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvSize.FormatString = ""
        gvSize.HeaderText = "Size"
        gvSize.Name = "gvSize"
        gvSize.Width = 100
        gvSize.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvSize)

        Dim gvSerailNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvSerailNo.FormatString = ""
        gvSerailNo.HeaderText = "Serial No."
        gvSerailNo.Name = "gvSerailNo"
        gvSerailNo.Width = 100
        gvSerailNo.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvSerailNo)

        Dim gvTagNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvTagNo.FormatString = ""
        gvTagNo.HeaderText = "Tag No."
        gvTagNo.Name = "gvTagNo"
        gvTagNo.Width = 100
        gvTagNo.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvTagNo)

        Dim gvApexPendingW_ONo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvApexPendingW_ONo.FormatString = ""
        gvApexPendingW_ONo.HeaderText = "Apex Pending W/O No"
        gvApexPendingW_ONo.Name = "gvApexPendingW_ONo"
        gvApexPendingW_ONo.Width = 100
        gvApexPendingW_ONo.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvApexPendingW_ONo)

        Dim gvPrimaryReasonCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvPrimaryReasonCode.FormatString = ""
        gvPrimaryReasonCode.HeaderText = "Primary Reason Code"
        gvPrimaryReasonCode.Name = "gvPrimaryReasonCode"
        gvPrimaryReasonCode.Width = 100
        gvPrimaryReasonCode.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvPrimaryReasonCode)

        Dim gvPrimaryReasonDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvPrimaryReasonDescription.FormatString = ""
        gvPrimaryReasonDescription.HeaderText = "Primary Reason Description"
        gvPrimaryReasonDescription.Name = "gvPrimaryReasonDescription"
        gvPrimaryReasonDescription.Width = 100
        gvPrimaryReasonDescription.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvPrimaryReasonDescription)

        Dim gvComplaintTypeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvComplaintTypeCode.FormatString = ""
        gvComplaintTypeCode.HeaderText = "Complaint Type Code"
        gvComplaintTypeCode.Name = "gvComplaintTypeCode"
        gvComplaintTypeCode.Width = 100
        gvComplaintTypeCode.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvComplaintTypeCode)

        Dim gvComplaintTypeDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvComplaintTypeDescription.FormatString = ""
        gvComplaintTypeDescription.HeaderText = "Complaint Type Description"
        gvComplaintTypeDescription.Name = "gvComplaintTypeDescription"
        gvComplaintTypeDescription.Width = 100
        gvComplaintTypeDescription.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvComplaintTypeDescription)

        Dim gvSecondaryReasonCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvSecondaryReasonCode.FormatString = ""
        gvSecondaryReasonCode.HeaderText = "Secondary Reason Code"
        gvSecondaryReasonCode.Name = "gvSecondaryReasonCode"
        gvSecondaryReasonCode.Width = 100
        gvSecondaryReasonCode.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvSecondaryReasonCode)

        Dim gvSecondaryReasonDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvSecondaryReasonDescription.FormatString = ""
        gvSecondaryReasonDescription.HeaderText = "Secondary Reason Description"
        gvSecondaryReasonDescription.Name = "gvSecondaryReasonDescription"
        gvSecondaryReasonDescription.Width = 100
        gvSecondaryReasonDescription.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvSecondaryReasonDescription)

        Dim gvComplaintGivenBy As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvComplaintGivenBy.FormatString = ""
        gvComplaintGivenBy.HeaderText = "Complaint Given By"
        gvComplaintGivenBy.Name = "gvComplaintGivenBy"
        gvComplaintGivenBy.Width = 100
        gvComplaintGivenBy.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvComplaintGivenBy)

        Dim gvComplaintGivenTo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvComplaintGivenTo.FormatString = ""
        gvComplaintGivenTo.HeaderText = "Complaint Given To"
        gvComplaintGivenTo.Name = "gvComplaintGivenTo"
        gvComplaintGivenTo.Width = 100
        gvComplaintGivenTo.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvComplaintGivenTo)

        Dim gvFranchise As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvFranchise.FormatString = ""
        gvFranchise.HeaderText = "Franchise"
        gvFranchise.Name = "gvFranchise"
        gvFranchise.Width = 100
        gvFranchise.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvFranchise)

        Dim gvFranchiseDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvFranchiseDescription.FormatString = ""
        gvFranchiseDescription.HeaderText = "Franchise Description"
        gvFranchiseDescription.Name = "gvFranchiseDescription"
        gvFranchiseDescription.Width = 100
        gvFranchiseDescription.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvFranchiseDescription)

        Dim gvServiceExecutiveCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvServiceExecutiveCode.FormatString = ""
        gvServiceExecutiveCode.HeaderText = "Service Executive Code"
        gvServiceExecutiveCode.Name = "gvServiceExecutiveCode"
        gvServiceExecutiveCode.Width = 100
        gvServiceExecutiveCode.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvServiceExecutiveCode)

        Dim gvServiceExecutiveName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvServiceExecutiveName.FormatString = ""
        gvServiceExecutiveName.HeaderText = "Service Executive Name"
        gvServiceExecutiveName.Name = "gvServiceExecutiveName"
        gvServiceExecutiveName.Width = 100
        gvServiceExecutiveName.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvServiceExecutiveName)

        Dim gvCompletedDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvCompletedDate.FormatString = ""
        gvCompletedDate.HeaderText = "Completed Date"
        gvCompletedDate.Name = "gvCompletedDate"
        gvCompletedDate.Width = 100
        gvCompletedDate.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvCompletedDate)

        Dim gvResponseTime As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvResponseTime.FormatString = ""
        gvResponseTime.HeaderText = "Response Time"
        gvResponseTime.Name = "gvResponseTime"
        gvResponseTime.Width = 100
        gvResponseTime.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvResponseTime)

        Dim gvRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvRemarks.FormatString = ""
        gvRemarks.HeaderText = "Remarks"
        gvRemarks.Name = "gvRemarks"
        gvRemarks.Width = 100
        gvRemarks.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvRemarks)

        Dim gvStatus As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvStatus.FormatString = ""
        gvStatus.HeaderText = "Status"
        gvStatus.Name = "gvStatuss"
        gvStatus.Width = 100
        gvStatus.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvStatus)

        Dim gvPendingReason As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvPendingReason.FormatString = ""
        gvPendingReason.HeaderText = "Pending Reason Code"
        gvPendingReason.Name = "gvPendCode"
        gvPendingReason.Width = 100
        gvPendingReason.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvPendingReason)

        Dim gvPendingReasondesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvPendingReasondesc.FormatString = ""
        gvPendingReasondesc.HeaderText = "Pending Reason Description"
        gvPendingReasondesc.Name = "gvPendresn"
        gvPendingReasondesc.Width = 100
        gvPendingReasondesc.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(gvPendingReasondesc)


        Dim gvWorkOrderNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvWorkOrderNo.FormatString = ""
        gvWorkOrderNo.HeaderText = "Work Order No"
        gvWorkOrderNo.Name = "gvWorkOrderNo"
        gvWorkOrderNo.Width = 100
        gvWorkOrderNo.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(gvWorkOrderNo)

        Dim gvBillNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvBillNo.FormatString = ""
        gvBillNo.HeaderText = "Bill No"
        gvBillNo.Name = "gvBillNo"
        gvBillNo.Width = 100
        gvBillNo.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(gvBillNo)

        Dim gvBillAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        gvBillAmount.FormatString = ""
        gvBillAmount.HeaderText = "Bill Amount"
        gvBillAmount.Name = "gvBillAmount"
        gvBillAmount.Width = 100
        gvBillAmount.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(gvBillAmount)

        Dim gvAdditionalCharges As GridViewDecimalColumn = New GridViewDecimalColumn()
        gvAdditionalCharges.FormatString = ""
        gvAdditionalCharges.HeaderText = "Additional Charges"
        gvAdditionalCharges.Name = "gvAdditionalCharges"
        gvAdditionalCharges.Width = 100
        gvAdditionalCharges.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(gvAdditionalCharges)






        MasterTemplate.ShowGroupPanel = False
        MasterTemplate.AllowColumnReorder = False
        MasterTemplate.AllowRowReorder = False
        MasterTemplate.EnableSorting = False
        MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        MasterTemplate.MasterTemplate.ShowRowHeaderColumn = False
        MasterTemplate.AllowAddNewRow = False
        MasterTemplate.Rows.Clear()
        ReStoreGridLayout()
    End Sub



    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmTransactionCancelScreen)
        ''--preeti gupta ticket no-[BM00000003168]
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")

        'End If
        'btnSave.Visible = MyBase.isModifyFlag
    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            IsNewEntry = True
            If SaveData(IsNewEntry) Then
                ' clsCommon.MyMessageBoxShow("Data saved successfully.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If MasterTemplate.Rows.Count <= 0 Then
                Throw New Exception("Please insert atleast single account in grid.")
            Else
                'Dim BankType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Bank_type from TSPL_BANK_MASTER Where BANK_CODE='" + fndBankCode.Value + "'"))
                Dim Counter As Integer = 0
                arrDocNo = New List(Of String)
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If (row.Cells("gvSelect").Value = 1 Or row.Cells("gvSelect").Value = True) And clsCommon.myLen(row.Cells("gvSelect").Value) Then
                        Counter += 1
                        If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvWorkOrderNo").Value)) <= 0 Then
                            Throw New Exception("Please Fill Work Order No in Row No [" & Counter & "].")
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvBillNo").Value)) <= 0 Then
                            Throw New Exception("Please Fill Bill No in Row No [" & Counter & "].")
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvBillAmount").Value)) <= 0 Then
                            Throw New Exception("Please Fill Bill Amount in Row No [" & Counter & "].")
                        End If
                    End If
                Next
                If Counter <= 0 Then
                    Throw New Exception("Please Select atleast single account in grid.")
                End If
            End If

            'If clsCommon.myLen(txtEntryNo.Value) > 0 Then
            '    Qry = "select   Posted   from TSPL_RECEIPT_HEADER where QuickEntryNo='" + txtEntryNo.Value + "'"
            '    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry)), "Y") = CompairStringResult.Equal Then
            '        Throw New Exception("Posted Transaction")
            '    End If
            'End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal IsNewEntry As Boolean) As Boolean
        Try
            Dim sQuery As String = String.Empty
            If AllowToSave() Then
                tran = clsDBFuncationality.GetTransactin()
                Try
                    For Each row As GridViewRowInfo In MasterTemplate.Rows
                        If (row.Cells("gvSelect").Value = 1 Or row.Cells("gvSelect").Value = True) And clsCommon.myLen(row.Cells("gvSelect").Value) Then
                            'sQuery = "update tspl_complaint_detail set work_order_no='" & clsCommon.myCstr(row.Cells("gvWorkOrderNo").Value) & "'," _
                            '    & " bill_no='" & clsCommon.myCstr(row.Cells("gvBillNo").Value) & "',bill_amount=" & clsCommon.myCstr(row.Cells("gvBillNo").Value) & ",add_chrge='' where comp_id=''"

                            Dim Obj As clscomplaintdetail = New clscomplaintdetail()
                            Obj.comp_id = clsCommon.myCstr(row.Cells("gvComplaintId").Value)
                            Obj.DocDate = clsCommon.myCDate(row.Cells("gvComplaintDate").Value).ToString("dd/MMM/yyyy")
                            Obj.comp_date = clsCommon.myCDate(row.Cells("gvComplaintDate").Value).ToString("MMM/dd/yyyy h:mm:ss tt")
                            Obj.description = clsCommon.myCstr(row.Cells("gvComplaintdescription").Value)
                            Obj.outltcode = clsCommon.myCstr(row.Cells("gvOutLetCode").Value)
                            Obj.phnno = clsCommon.myCstr(row.Cells("gvPhoneNo").Value)
                            Obj.itemcode = clsCommon.myCstr(row.Cells("gvAssetType").Value)
                            Obj.itemmake = clsCommon.myCstr(row.Cells("gvMake").Value)
                            Obj.modelno = clsCommon.myCstr(row.Cells("gvModel").Value)
                            Obj.size = clsCommon.myCstr(row.Cells("gvSize").Value)
                            Obj.serialno = clsCommon.myCstr(row.Cells("gvSerailNo").Value)
                            Obj.tagno = clsCommon.myCstr(row.Cells("gvtagNO").Value)
                            Obj.apexno = clsCommon.myCstr(row.Cells("gvApexPendingW_ONo").Value)
                            Obj.compltypecode = clsCommon.myCstr(row.Cells("gvComplaintTypeCode").Value)
                            Obj.complgivnby = clsCommon.myCstr(row.Cells("gvComplaintGivenBy").Value)
                            Obj.complgivnto = clsCommon.myCstr(row.Cells("gvComplaintGivenTo").Value)
                            Obj.tdmcode = clsCommon.myCstr(row.Cells("gvComplaintTypeCode").Value)
                            'End If

                            Obj.remarks = clsCommon.myCstr(row.Cells("gvRemarks").Value)

                            Obj.workno = clsCommon.myCstr(row.Cells("gvWorkOrderNo").Value)

                            Obj.compl_status = "C"
                            Obj.executivecode = clsCommon.myCstr(row.Cells("gvServiceExecutiveCode").Value)


                            Obj.billno = clsCommon.myCstr(row.Cells("gvBillNo").Value)
                            Obj.billamt = clsCommon.myCdbl(row.Cells("gvBillAmount").Value)
                            Obj.addcharge = clsCommon.myCdbl(row.Cells("gvAdditionalCharges").Value)



                            Obj.secresn = clsCommon.myCstr(row.Cells("gvSecondaryReasonCode").Value)
                            Obj.pendresn = clsCommon.myCstr(row.Cells("gvPendCode").Value)

                            If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvCompletedDate").Value)) > 0 Then
                                Obj.status_date = clsCommon.myCDate(row.Cells("gvCompletedDate").Value).ToString("MMM/dd/yyyy h:mm:ss tt")
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvResponseTime").Value)) > 0 Then
                                Obj.responsetym = clsCommon.myCDate(row.Cells("gvResponseTime").Value).ToString("MMM/dd/yyyy h:mm:ss tt")
                            End If


                            ' If rdfranchise.IsChecked = True Then
                            Obj.franchiseyn = "Y"
                            'Else
                            'Obj.franchiseyn = "N"
                            'End If

                            Obj.primarycode = clsCommon.myCstr(row.Cells("gvPrimaryReasonCode").Value)
                            Obj.primarydesc = clsCommon.myCstr(row.Cells("gvPrimaryReasonDescription").Value)
                            Obj.secdrycode = clsCommon.myCstr(row.Cells("gvSecondaryReasonCode").Value)
                            Obj.pendcode = clsCommon.myCstr(row.Cells("gvPendCode").Value)


                            If btnSave.Text <> "Save" Then
                                Dim qry As String = "select count(*) from tspl_complaint_detail where comp_id='" & clsCommon.myCstr(row.Cells("gvComplaintId").Value) & "'"
                                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, tran)
                                If check = 0 Then
                                    clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                                End If
                            End If

                            'If AllowToSave() Then
                            Try
                                Dim isSaved As Boolean = clscomplaintdetail.SaveData(Obj, False, tran)
                                row.Cells("gvComplaintId").Value = Obj.comp_id
                                If isSaved Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                                    btnSave.Text = "Update"
                                    'btnPost.Enabled = True

                                Else
                                    btnSave.Text = "Save"
                                    common.clsCommon.MyMessageBoxShow(Me, "Data Could Not Saved", Me.Text)
                                End If
                            Catch ex As Exception
                                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                            End Try

                        End If
                    Next
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Sub fillData(ByVal EntryNo As String)
        Try
            funReset()
            IsLoadData = True
            Dim i As Integer = 0
            Dim CountPost As Integer = 0
            Dim ObjList As New List(Of clscomplaintdetail)
            ObjList = clscomplaintdetail.GetPendingData(CmbModule.SelectedValue)

            If ObjList IsNot Nothing AndAlso ObjList.Count > 0 Then
                For Each obj As clscomplaintdetail In ObjList
                    MasterTemplate.Rows.AddNew()
                    MasterTemplate.CurrentRow.Cells("gvComplaintId").Value = obj.comp_id
                    MasterTemplate.CurrentRow.Cells("gvComplaintDate").Value = clsCommon.myCDate(obj.comp_date) '.ToString("MM/dd/yyyy h:mm:ss tt")
                    MasterTemplate.CurrentRow.Cells("gvComplaintdescription").Value = obj.description
                    MasterTemplate.CurrentRow.Cells("gvOutLetCode").Value = obj.outltcode
                    MasterTemplate.CurrentRow.Cells("gvOutLetname").Value = obj.outltdesc
                    MasterTemplate.CurrentRow.Cells("gvType").Value = obj.outlttype
                    MasterTemplate.CurrentRow.Cells("gvLocation").Value = obj.location
                    MasterTemplate.CurrentRow.Cells("gvCity").Value = obj.city
                    MasterTemplate.CurrentRow.Cells("gvState").Value = obj.state
                    MasterTemplate.CurrentRow.Cells("gvCountry").Value = obj.country
                    MasterTemplate.CurrentRow.Cells("gvPhoneNo").Value = obj.phnno
                    MasterTemplate.CurrentRow.Cells("gvAssetType").Value = obj.itemcode
                    MasterTemplate.CurrentRow.Cells("gvAssetTypeDescription").Value = obj.itemdesc
                    MasterTemplate.CurrentRow.Cells("gvMake").Value = obj.itemmake
                    MasterTemplate.CurrentRow.Cells("gvModel").Value = obj.modelno
                    MasterTemplate.CurrentRow.Cells("gvSize").Value = obj.size
                    MasterTemplate.CurrentRow.Cells("gvSerailNo").Value = obj.serialno
                    MasterTemplate.CurrentRow.Cells("gvtagNO").Value = obj.tagno
                    MasterTemplate.CurrentRow.Cells("gvApexPendingW_ONo").Value = obj.apexno

                    MasterTemplate.CurrentRow.Cells("gvComplaintTypeCode").Value = obj.compltypecode
                    MasterTemplate.CurrentRow.Cells("gvComplaintTypeDescription").Value = obj.compltypedesc
                    MasterTemplate.CurrentRow.Cells("gvPrimaryReasonCode").Value = obj.primarycode
                    MasterTemplate.CurrentRow.Cells("gvPrimaryReasonDescription").Value = obj.primarydesc
                    MasterTemplate.CurrentRow.Cells("gvSecondaryReasonCode").Value = obj.secdrycode
                    MasterTemplate.CurrentRow.Cells("gvSecondaryReasonDescription").Value = obj.secresn
                    MasterTemplate.CurrentRow.Cells("gvComplaintGivenBy").Value = obj.complgivnby
                    MasterTemplate.CurrentRow.Cells("gvComplaintGivenTo").Value = obj.complgivnto
                    MasterTemplate.CurrentRow.Cells("gvFranchise").Value = obj.tdmcode
                    MasterTemplate.CurrentRow.Cells("gvFranchiseDescription").Value = obj.tdmdesc
                    MasterTemplate.CurrentRow.Cells("gvRemarks").Value = obj.remarks


                    MasterTemplate.CurrentRow.Cells("gvServiceExecutiveCode").Value = obj.executivecode
                    MasterTemplate.CurrentRow.Cells("gvServiceExecutiveName").Value = obj.executivedesc
                    MasterTemplate.CurrentRow.Cells("gvBillNo").Value = obj.billno
                    MasterTemplate.CurrentRow.Cells("gvBillAmount").Value = obj.billamt
                    MasterTemplate.CurrentRow.Cells("gvAdditionalCharges").Value = obj.addcharge


                    MasterTemplate.CurrentRow.Cells("gvWorkOrderNo").Value = obj.workno
                    MasterTemplate.CurrentRow.Cells("gvResponseTime").Value = obj.responsetym
                    MasterTemplate.CurrentRow.Cells("gvCompletedDate").Value = obj.status_date '.ToString("MM/dd/yyyy h:mm:ss tt")


                    MasterTemplate.CurrentRow.Cells("gvPendCode").Value = obj.pendcode
                    MasterTemplate.CurrentRow.Cells("gvPendresn").Value = obj.pendresn
                    MasterTemplate.CurrentRow.Cells("gvStatuss").Value = clsCommon.myCstr(CmbModule.Text) '"Pending"
                    '----------------------------------------------------------------
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Public Sub funReset()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        LoadBlankGrid()
        If clsCommon.CompairString(CmbModule.SelectedValue, "C") = CompairStringResult.Equal Then
            btnSave.Enabled = False
        Else
            btnSave.Enabled = True
        End If
        ' MasterTemplate.Rows.AddNew()
        IsLoadData = False
        IsNewEntry = True
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Public Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            MasterTemplate.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            MasterTemplate.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = MasterTemplate.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub FrmTransactionCancelScreen_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
                If SaveData(IsNewEntry) Then
                    'clsCommon.MyMessageBoxShow("Data saved successfully.")
                End If
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                CloseForm()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                funReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Get_type()
        Dim dt As New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Pending"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "C"
        dr("Name") = "Completed"
        dt.Rows.Add(dr)

        CmbModule.DataSource = dt
        CmbModule.ValueMember = "Code"
        CmbModule.DisplayMember = "Name"
    End Sub

    Private Sub BtnShow_Click(sender As Object, e As EventArgs) Handles BtnShow.Click
        Try
            fillData("")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= MasterTemplate.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To MasterTemplate.Columns.Count - 1 Step ii + 1
                    MasterTemplate.Columns(ii).IsVisible = False
                    MasterTemplate.Columns(ii).VisibleInColumnChooser = True
                Next

                MasterTemplate.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub BtnSelectAll_Click(sender As Object, e As EventArgs) Handles BtnSelectAll.Click
        If BtnSelectAll.Text = "Select All" Then
            BtnSelectAll.Text = "Unselect All"
            For Each row As GridViewRowInfo In MasterTemplate.Rows
                row.Cells("gvSelect").Value = True
            Next
        ElseIf BtnSelectAll.Text = "Unselect All" Then
            BtnSelectAll.Text = "Select All"
            For Each row As GridViewRowInfo In MasterTemplate.Rows
                row.Cells("gvSelect").Value = False
            Next
        End If
    End Sub
End Class

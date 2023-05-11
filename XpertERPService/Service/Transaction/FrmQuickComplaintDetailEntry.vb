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
Imports XpertERPEngine

Public Class FrmQuickComplaintDetailEntry
    Inherits FrmMainTranScreen
    Const ReportID As String = "QuickComplaintDetailEntryGrid"
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
    Private Sub FrmQuickComplaintDetailEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsLoadData = False
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
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
        MasterTemplate.EnableFiltering = True
        MasterTemplate.Rows.Clear()
        ReStoreGridLayout()
    End Sub



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmQuickComplaintDetailEntry)
        '--preeti gupta ticket no-[BM00000003168]
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            IsNewEntry = True
            If SaveData(IsNewEntry) Then
                ' clsCommon.MyMessageBoxShow("Data saved successfully.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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
                                    clsCommon.MyMessageBoxShow("No Data Found")
                                End If
                            End If

                            'If AllowToSave() Then
                            Try
                                Dim isSaved As Boolean = clscomplaintdetail.SaveData(Obj, False, tran)
                                row.Cells("gvComplaintId").Value = Obj.comp_id
                                If isSaved Then
                                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                                    btnSave.Text = "Update"
                                    'btnPost.Enabled = True

                                Else
                                    btnSave.Text = "Save"
                                    btnPost.Enabled = False
                                    common.clsCommon.MyMessageBoxShow("Data Could Not Saved", Me.Text)
                                End If
                            Catch ex As Exception
                                clsCommon.MyMessageBoxShow(ex.Message)
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
            ObjList = clscomplaintdetail.GetPendingData(CmbStatus.SelectedValue)

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
                    MasterTemplate.CurrentRow.Cells("gvStatuss").Value = clsCommon.myCstr(CmbStatus.Text) '"Pending"
                    '----------------------------------------------------------------
                Next
            Else
                clsCommon.MyMessageBoxShow("No Data Found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        clicked = True
        PostData()
    End Sub


    Public Sub PostData()
        Try
            Dim counter As Integer = 0
            For Each row As GridViewRowInfo In MasterTemplate.Rows
                If (row.Cells("gvSelect").Value = 1 Or row.Cells("gvSelect").Value = True) And clsCommon.myLen(row.Cells("gvSelect").Value) Then
                    counter += 1
                    Dim qry1 As String = "update tspl_complaint_detail set post_status='Y' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and comp_id='" + clsCommon.myCstr(row.Cells("gvComplaintId").Value) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry1)
                End If
            Next
            If counter > 0 Then
                btnPost.Enabled = False
                clsCommon.MyMessageBoxShow("Posted Successfully.")
            Else
                clsCommon.MyMessageBoxShow("No Row selected.Please Select Row First..")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Public Sub funReset()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        LoadBlankGrid()
        If clsCommon.CompairString(CmbStatus.SelectedValue, "C") = CompairStringResult.Equal Then
            btnPost.Enabled = True
            btnSave.Enabled = False
        Else
            btnPost.Enabled = False
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj = Nothing
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub FrmQuickComplaintDetailEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
                If SaveData(IsNewEntry) Then
                    'clsCommon.MyMessageBoxShow("Data saved successfully.")
                End If
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                PostData()
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                CloseForm()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                funReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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

        CmbStatus.DataSource = dt
        CmbStatus.ValueMember = "Code"
        CmbStatus.DisplayMember = "Name"
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
    Sub ImportAuto()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        connectSql.OpenConnection()
        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "Complain Id", "Complaint Date", "Description", "Outlet Code", "Outlet Name", "Type", "City", "State", "Country", "Phone No.", "Asset Type Code", "Asset Type Description", "Make", "Model", "Serial No.", "Tag No.", "Apex Pending W/O No.", "Primary Reason Description", "Primary Reason", "Complaint Type Code", "Secondary Reason Code", "Secondary Reason Description", "Complaint Given By", "Complaint Given To", "Franchise", "Franchise Description", "Service Executive Code", "Service Exceutive Name", "Completed Date", "Response Time", "remarks", "Pending Reason Code", "Pending Reason Description", "Work Order No.", "Bill No.", "Bill Amount", "Additional Charge", "Location", "Status") Then
            Dim trans As SqlTransaction = Nothing
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clscomplaintdetail()
                    i = i + 1
                    Dim strName As String = ""
                    'Dim strBBDesp As String = ""
                    'Dim strBBName As String = ""
                    Dim strCode As String = ""

                    strCode = clsCommon.myCstr(grow.Cells("Complain Id").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("Length of Complain Id can not be greater than 30")
                    End If
                    obj.comp_id = strCode

                    Dim comp_date As Date = Nothing
                    strName = clsCommon.myCstr(grow.Cells("Complaint Date").Value)
                    If clsCommon.myLen(comp_date) <= 0 Then
                        Throw New Exception("Complaint Date can not be blank ")
                    ElseIf IsDate(clsCommon.myCstr(grow.Cells("Complaint Date").Value)) = False Then
                        Throw New Exception("Format of Complaint Date is incorrect")
                    End If
                    obj.comp_date = clsCommon.GetPrintDate(strName, "dd/MM/yyyy")
                    obj.DocDate = clsCommon.GetPrintDate(strName, "dd/MM/yyyy")

                    strName = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Description can not be greater than 50 ")

                    End If
                    obj.description = strName

                    obj.outltcode = clsCommon.myCstr(grow.Cells("Outlet Code").Value)
                    If clsCommon.myLen(obj.outltcode) > 0 Then
                        obj.outltcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.outltcode + "'", trans))
                        If clsCommon.myLen(obj.outltcode) <= 0 Then
                            Throw New Exception("Outlet Code does not exist.")
                        End If
                        obj.outltdesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.outltcode + "'", trans))
                        obj.outlttype = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_desc from TSPL_CUSTOMER_CATEGORY_MASTER left join  tspl_customer_master on tspl_customer_master.Cust_Category_Code =TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE where cust_code='" + obj.outltcode + "'", trans))
                        obj.city = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select City_Name  from TSPL_CITY_MASTER left join tspl_customer_master on tspl_customer_master.City_Code =TSPL_CITY_MASTER.City_Code  where cust_code='" + obj.outltcode + "'", trans))
                        obj.state = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct state from tspl_customer_master where cust_code='" + obj.outltcode + "'", trans))
                        obj.country = "India"
                        obj.phnno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct phone1 from tspl_customer_master where cust_code='" + obj.outltcode + "'", trans))
                        obj.location = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_customer_master.add1+' '+tspl_customer_master.add2+' '+tspl_customer_master.add3 from tspl_customer_master where cust_code='" + obj.outltcode + "'", trans))

                    Else
                        Throw New Exception("Please select Outlet Code.")
                    End If

                    obj.serialno = clsCommon.myCstr(grow.Cells("Serial No.").Value)
                    If clsCommon.myLen(obj.serialno) > 0 Then
                        obj.serialno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Serial_No from tspl_visi_master left join tspl_customer_master on tspl_customer_master.Cust_Code =tspl_visi_master.Customer_Id where customer_id='" + obj.outltcode + "'", trans))
                        obj.itemcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select asset_no from tspl_visi_master left join tspl_customer_master on tspl_customer_master.Cust_Code =tspl_visi_master.Customer_Id where customer_id='" + obj.outltcode + "' and Serial_No='" + obj.serialno + "'", trans))
                        obj.itemdesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_visi_master left join tspl_customer_master on tspl_customer_master.Cust_Code =tspl_visi_master.Customer_Id left join tspl_item_master on tspl_item_master.Item_Code =tspl_visi_master.Asset_No where customer_id='" + obj.outltcode + "' and Serial_No='" + obj.serialno + "'", trans))
                        obj.tagno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tag_No from tspl_visi_master left join tspl_customer_master on tspl_customer_master.Cust_Code =tspl_visi_master.Customer_Id  where customer_id='" + obj.outltcode + "' and Serial_No='" + obj.serialno + "' and asset_no='" + obj.itemcode + "'", trans))
                        obj.modelno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Model_No from tspl_visi_master left join tspl_customer_master on tspl_customer_master.Cust_Code =tspl_visi_master.Customer_Id  where customer_id='" + obj.outltcode + "' and Serial_No='" + obj.serialno + "' and asset_no='" + obj.itemcode + "'", trans))
                        obj.size = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select visi_size from tspl_visi_master left join tspl_customer_master on tspl_customer_master.Cust_Code =tspl_visi_master.Customer_Id  where customer_id='" + obj.outltcode + "' and Serial_No='" + obj.serialno + "' and asset_no='" + obj.itemcode + "'", trans))
                        obj.itemmake = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VisiMake from tspl_visi_master left join tspl_customer_master on tspl_customer_master.Cust_Code =tspl_visi_master.Customer_Id  where customer_id='" + obj.outltcode + "' and Serial_No='" + obj.serialno + "' and asset_no='" + obj.itemcode + "'", trans))
                    End If
                    strName = clsCommon.myCstr(grow.Cells("Apex Pending W/O No.").Value)
                    If clsCommon.CompairString(strName, "Under Warranty") = CompairStringResult.Equal Then
                        obj.apexno = "UW"
                    ElseIf clsCommon.CompairString(strName, "Out Of Warranty") = CompairStringResult.Equal Then
                        obj.apexno = "OW"
                    ElseIf clsCommon.CompairString(strName, "On Your Freezer") = CompairStringResult.Equal Then
                        obj.apexno = "OF"
                    End If

                    obj.primarycode = clsCommon.myCstr(grow.Cells("Primary Reason").Value)
                    If clsCommon.myLen(obj.primarycode) > 0 Then
                        obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select REASON_CODE  from TSPL_PRIMARY_REASON_MASTER WHERE REASON_CODE='" + obj.primarycode + "'", trans))
                        If clsCommon.myLen(obj.primarycode) <= 0 Then
                            Throw New Exception("Outlet Code does not exist.")
                        End If
                        obj.primarydesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select DESCRIPTION from TSPL_PRIMARY_REASON_MASTER WHERE REASON_CODE='" + obj.primarycode + "'", trans))
                    Else
                        Throw New Exception("Please select Primary Reason Code.")
                    End If

                    obj.compltypecode = clsCommon.myCstr(grow.Cells("Complaint Type Code").Value)
                    If clsCommon.myLen(obj.compltypecode) > 0 Then
                        obj.compltypecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select COMPLAINT_CODE  from TSPL_COMPLAINT_GROUP_MASTER WHERE COMPLAINT_CODE='" + obj.compltypecode + "'", trans))
                        If clsCommon.myLen(obj.compltypecode) <= 0 Then
                            Throw New Exception("Complaint Type Code does not exist.")
                        End If
                    Else
                        Throw New Exception("Please select  Complaint Type Code.")
                    End If

                    obj.secdrycode = clsCommon.myCstr(grow.Cells("Secondary Reason Code").Value)
                    If clsCommon.myLen(obj.secdrycode) > 0 Then
                        obj.secdrycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select COMPLAINT_CODE  from TSPL_COMPLAINT_MASTER WHERE COMPLAINT_CODE='" + obj.secdrycode + "'", trans))
                        If clsCommon.myLen(obj.secdrycode) <= 0 Then
                            Throw New Exception("Complaint Type Code does not exist.")
                        End If
                        obj.secresn = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select DESCRIPTION from TSPL_COMPLAINT_MASTER WHERE COMPLAINT_CODE='" + obj.secdrycode + "'", trans))
                    Else
                        Throw New Exception("Please select Secondary Reason Code.")
                    End If
                    strName = clsCommon.myCstr(grow.Cells("Complaint Given By").Value)
                    If strName.Length > 20 Then
                        Throw New Exception("Length of Complaint Given By can not be greater than 30")
                    End If
                    obj.complgivnby = strName

                    strName = clsCommon.myCstr(grow.Cells("Complaint Given To").Value)
                    If strName.Length > 20 Then
                        Throw New Exception("Length of Complaint Given To can not be greater than 30")
                    End If
                    obj.complgivnto = strName

                    obj.tdmcode = clsCommon.myCstr(grow.Cells("Franchise").Value)
                    If clsCommon.myLen(obj.tdmcode) > 0 Then
                        obj.tdmcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code  from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.tdmcode + "'", trans))
                        If clsCommon.myLen(obj.tdmcode) <= 0 Then
                            Throw New Exception("Franchise does not exist.")
                        End If
                        obj.tdmdesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Name from TSPL_VENDOR_MASTER WHERE Vendor_Code='" + obj.tdmcode + "'", trans))
                    Else
                        Throw New Exception("Please select Franchise")
                    End If



                    obj.executivecode = clsCommon.myCstr(grow.Cells("Service Executive Code").Value)
                    If clsCommon.myLen(obj.executivecode) > 0 Then
                        obj.executivecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Code  from TSPL_EMPLOYEE_MASTER where Emp_Code ='" + obj.executivecode + "'", trans))
                        If clsCommon.myLen(obj.executivecode) <= 0 Then
                            Throw New Exception("Service Executive Code does not exist.")
                        End If
                        obj.executivedesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Emp_Name from TSPL_EMPLOYEE_MASTER WHERE Emp_Code='" + obj.executivecode + "'", trans))
                    Else
                        Throw New Exception("Please select Service Executive Code")
                    End If

                    Dim status_date As Date = Nothing
                    strName = clsCommon.myCstr(grow.Cells("Completed Date").Value)
                    If clsCommon.myLen(status_date) <= 0 Then
                        Throw New Exception("Completed Date can not be blank ")
                    ElseIf IsDate(clsCommon.myCstr(grow.Cells("Completed Date").Value)) = False Then
                        Throw New Exception("Format of Completed Date is incorrect")
                    End If
                    obj.status_date = clsCommon.GetPrintDate(strName, "dd/MM/yyyy")


                    strName = clsCommon.myCstr(grow.Cells("remarks").Value)
                    If strName.Length <= 0 Then
                        Throw New Exception("Length of remarks can not be left blank")
                    End If
                    obj.remarks = strName

                    obj.pendcode = clsCommon.myCstr(grow.Cells("Pending Reason Code").Value)
                    If clsCommon.myLen(obj.pendcode) > 0 Then
                        obj.pendcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PENDING_REASON_CODE  from TSPL_PENDING_REASON_MASTER where PENDING_REASON_CODE ='" + obj.pendcode + "'", trans))
                        If clsCommon.myLen(obj.pendcode) <= 0 Then
                            Throw New Exception("Pending Reason Code does not exist.")
                        End If
                        obj.pendresn = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select DESCRIPTION from TSPL_PENDING_REASON_MASTER WHERE PENDING_REASON_CODE='" + obj.pendcode + "'", trans))
                    Else
                        Throw New Exception("Please select Pending Reason Code")
                    End If

                    strName = clsCommon.myCstr(grow.Cells("Work Order No.").Value)
                    obj.workno = strName

                    strName = clsCommon.myCstr(grow.Cells("Bill No.").Value)
                    obj.billno = strName

                    strName = clsCommon.myCdbl(grow.Cells("Bill Amount").Value)
                    obj.billamt = strName

                    strName = clsCommon.myCdbl(grow.Cells("Additional Charge").Value)
                    'If strName.Length > 12 Then
                    '    Throw New Exception("Length of Additional Charge can not be greater than 30")
                    'End If
                    obj.addcharge = strName

                    strName = clsCommon.myCstr(grow.Cells("Response Time").Value)
                    obj.responsetym = strName

                    strName = clsCommon.myCstr(grow.Cells("Status").Value)
                    If clsCommon.CompairString(strName, "P") = CompairStringResult.Equal Then
                        obj.compl_status = strName
                    ElseIf clsCommon.CompairString(strName, "C") = CompairStringResult.Equal Then
                        obj.compl_status = strName
                    End If
                    clscomplaintdetail.SaveData(obj, True, trans)
                    grow = Nothing
                    obj = Nothing


                Next

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message & " At Line No : " & i)
            End Try
        End If


    End Sub
    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click

    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click

    End Sub
    Sub AutomaticExport()
        Dim str As String = ""
        str = "select comp_id as [Complain Id],convert(varchar,comp_date,103) as [Complaint Date],TSPL_COMPLAINT_DETAIL.description as [Description],TSPL_COMPLAINT_DETAIL.cust_code as [Outlet Code],Customer_Name as [Outlet Name] ,CUST_CATEGORY_DESC as [Type],City_name as [City],STATE_NAME as [State],'India' as [Country],phone_no as [Phone No.],TSPL_COMPLAINT_DETAIL.item_code as [Asset Type Code],Item_Desc as [Asset Type Description],item_make as [Make],model_no as [Model],size ,serial_no as [Serial No.],tag_no as [Tag No.],case when apex_no ='UW' then 'Under Warranty' when apex_no='OW' then 'Out Of Warranty' else 'On Your Freezer' end  as [Apex Pending W/O No.],primary_reason as [Primary Reason Description],primary_reason_code as [Primary Reason] ,   compl_type_code as [Complaint Type Code],sec_reason_code as [Secondary Reason Code],sec_reason as [Secondary Reason Description],compl_given_by as [Complaint Given By],compl_given_to as [Complaint Given To],TSPL_COMPLAINT_DETAIL.tdm_code as [Franchise],Vendor_Name as [Franchise Description],executive_code as [Service Executive Code],emp_name as [Service Exceutive Name],convert(varchar,status_date,103)  as [Completed Date],response_time as [Response Time],remarks  ,pend_reason_code as [Pending Reason Code],TSPL_PENDING_REASON_MASTER.description as [Pending Reason Description],work_order_no as [Work Order No.],bill_no as [Bill No.],bill_amount as [Bill Amount],add_chrge as [Additional Charge],(tspl_customer_master.add1+' '+tspl_customer_master.add2+' '+tspl_customer_master.add3) as Location ,compl_status as [Status]  from TSPL_COMPLAINT_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=Tspl_complaint_detail.item_code left outer join TSPL_COMPLAINT_GROUP_MASTER  on TSPL_COMPLAINT_GROUP_MASTER.complaint_code=tspl_complaint_detail.compl_type_code left outer join tspl_customer_master on  tspl_customer_master.cust_code=tspl_complaint_detail.cust_code	" & _
              " left join tspl_vendor_master on tspl_vendor_master.vendor_Code=TSPL_COMPLAINT_DETAIL.tdm_code " & _
              " left join tspl_Employee_master on tspl_Employee_master.emp_code=TSPL_COMPLAINT_DETAIL.executive_code " & _
              " left join tspl_city_master on tspl_city_master.City_Code =tspl_customer_master.City_Code" & _
              " left join tspl_state_master on tspl_state_master.STATE_CODE =tspl_customer_master.State " & _
              " left join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE =tspl_customer_master.Category_Struct_Code " & _
              " left join TSPL_PENDING_REASON_MASTER on TSPL_PENDING_REASON_MASTER.pending_reason_code=Tspl_complaint_detail.pend_reason_code"
        transportSql.ExporttoExcel(str, " and coalesce(post_status,'N')<>'Y' and tspl_complaint_detail.compl_status='" & CmbStatus.SelectedValue & "'", Me)
    End Sub
    Private Sub MasterTemplate_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles MasterTemplate.CellDoubleClick
        If MasterTemplate.Rows.Count > 0 Then
            Dim strTransType As String = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvComplaintId").Value)
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmComplaintDetailEntry, strTransType)
        End If
    End Sub

    Private Sub rmAutomatic_Click(sender As Object, e As EventArgs) Handles rmAutomatic.Click
        ImportAuto()
    End Sub
    Sub ImportManual()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        connectSql.OpenConnection()
        Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        If transportSql.importExcel(gv, "Complain Id", "Complaint Date", "Description", "Outlet Name", "Type", "City", "State", "Country", "Phone No.", "Asset Type Description", "Make", "Model", "size", "Serial No.", "Tag No.", "Apex Pending W/O No.", "Primary Reason Description", "Secondary Reason Description", "Complaint Given By", "Complaint Given To", "Franchise Description", "Service Exceutive Name", "Completed Date", "Response Time", "remarks", "Pending Reason Description", "Work Order No.", "Bill No.", "Bill Amount", "Additional Charge", "Location", "Status", "Manual", "Complaint Type Description") Then
            Dim trans As SqlTransaction = Nothing
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clscomplaintdetail()
                    i = i + 1
                    Dim strName As String = ""
                    'Dim strBBDesp As String = ""
                    'Dim strBBName As String = ""
                    Dim strCode As String = ""

                    strCode = clsCommon.myCstr(grow.Cells("Complain Id").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("Length of Complain Id can not be greater than 30")
                    End If
                    obj.comp_id = strCode

                    Dim comp_date As Date = Nothing
                    strName = clsCommon.myCstr(grow.Cells("Complaint Date").Value)
                    If clsCommon.myLen(comp_date) <= 0 Then
                        Throw New Exception("Complaint Date can not be blank ")
                    ElseIf IsDate(clsCommon.myCstr(grow.Cells("Complaint Date").Value)) = False Then
                        Throw New Exception("Format of Complaint Date is incorrect")
                    End If
                    obj.comp_date = clsCommon.GetPrintDate(strName, "dd/MM/yyyy")
                    obj.DocDate = clsCommon.GetPrintDate(strName, "dd/MM/yyyy")

                    strName = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strName.Length > 200 Then
                        Throw New Exception("Length of Description can not be greater than 200 ")

                    End If
                    obj.description = strName

                    strName = clsCommon.myCstr(grow.Cells("Outlet Name").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Outlet Name can not be greater than 30 ")
                    End If
                    obj.OutletNameManual = strName

                    strName = clsCommon.myCstr(grow.Cells("Serial No.").Value)
                    If strName.Length > 20 Then
                        Throw New Exception("Length of Serial No. can not be greater than 20 ")
                    End If
                    obj.serialno = strName

                    strName = clsCommon.myCstr(grow.Cells("Asset Type Description").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Asset_Type_NameManual can not be greater than 30 ")
                    End If
                    obj.Asset_Type_NameManual = strName
                    strName = clsCommon.myCstr(grow.Cells("Tag No.").Value)
                    If strName.Length > 80 Then
                        Throw New Exception("Length of Tag No. can not be greater than 80 ")
                    End If
                    obj.tagno = strName
                    strName = clsCommon.myCstr(grow.Cells("Model").Value)
                    If strName.Length > 150 Then
                        Throw New Exception("Length of Model can not be greater than 150 ")
                    End If
                    obj.modelno = strName

                    strName = clsCommon.myCstr(grow.Cells("size").Value)
                    If strName.Length > 100 Then
                        Throw New Exception("Length of size can not be greater than 100 ")
                    End If
                    obj.size = strName

                    strName = clsCommon.myCstr(grow.Cells("Make").Value)
                    If strName.Length > 150 Then
                        Throw New Exception("Length of Make can not be greater than 150 ")
                    End If
                    obj.itemmake = strName

                    obj.IsManual = 1

                    strName = clsCommon.myCstr(grow.Cells("Type").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Type can not be greater than 30 ")
                    End If
                    obj.OutletTypeManual = strName

                    strName = clsCommon.myCstr(grow.Cells("City").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of City can not be greater than 30 ")
                    End If
                    obj.CityManual = strName

                    strName = clsCommon.myCstr(grow.Cells("State").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of City can not be greater than 30 ")
                    End If
                    obj.StateManual = strName

                    strName = clsCommon.myCstr(grow.Cells("Country").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Country can not be greater than 30 ")
                    End If
                    obj.CountryManual = strName

                    strName = clsCommon.myCstr(grow.Cells("Location").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Location can not be greater than 30 ")
                    End If
                    obj.LocationManual = strName

                    strName = clsCommon.myCstr(grow.Cells("Phone No.").Value)
                    If strName.Length > 40 Then
                        Throw New Exception("Length of Phone No. can not be greater than 40 ")
                    End If
                    obj.phnno = strName

                    strName = clsCommon.myCstr(grow.Cells("Complaint Type Description").Value)
                    If strName.Length > 40 Then
                        Throw New Exception("Length of Complaint Type Description can not be greater than 40 ")
                    End If
                    obj.ComplaintTypeManual = strName


                    strName = clsCommon.myCstr(grow.Cells("Apex Pending W/O No.").Value)
                    If clsCommon.CompairString(strName, "Under Warranty") = CompairStringResult.Equal Then
                        obj.apexno = "UW"
                    ElseIf clsCommon.CompairString(strName, "Out Of Warranty") = CompairStringResult.Equal Then
                        obj.apexno = "OW"
                    ElseIf clsCommon.CompairString(strName, "On Your Freezer") = CompairStringResult.Equal Then
                        obj.apexno = "OF"
                        'Else
                        '    Throw New Exception("Apex Pending W/o No. is not Valid")
                    End If

                    strName = clsCommon.myCstr(grow.Cells("Primary Reason Description").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Primary Reason Description can not be greater than 30 ")
                    End If
                    obj.Primary_Reason_DescManual = strName

                    strName = clsCommon.myCstr(grow.Cells("Secondary Reason Description").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Secondary Reason Description can not be greater than 30 ")
                    End If
                    obj.Secondary_Reason_DescManual = strName

                    strName = clsCommon.myCstr(grow.Cells("Secondary Reason Description").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Secondary Reason Description can not be greater than 30 ")
                    End If
                    obj.Secondary_Reason_DescManual = strName

                    strName = clsCommon.myCstr(grow.Cells("Complaint Given By").Value)
                    If strName.Length > 150 Then
                        Throw New Exception("Length of Complaint Given By can not be greater than 150")
                    End If
                    obj.complgivnby = strName

                    strName = clsCommon.myCstr(grow.Cells("Complaint Given To").Value)
                    If strName.Length > 150 Then
                        Throw New Exception("Length of Complaint Given To can not be greater than 150")
                    End If
                    obj.complgivnto = strName

                    strName = clsCommon.myCstr(grow.Cells("Franchise Description").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Franchise Description can not be greater than 30")
                    End If
                    obj.Franchise_DescManual = strName

                    strName = clsCommon.myCstr(grow.Cells("Service Exceutive Name").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Service Exceutive Name can not be greater than 30")
                    End If
                    obj.Service_Executive_DescManual = strName

                    Dim status_date As Date = Nothing
                    strName = clsCommon.myCstr(grow.Cells("Completed Date").Value)
                    If clsCommon.myLen(status_date) <= 0 Then
                        Throw New Exception("Completed Date can not be blank ")
                    ElseIf IsDate(clsCommon.myCstr(grow.Cells("Completed Date").Value)) = False Then
                        Throw New Exception("Format of Completed Date is incorrect")
                    End If
                    obj.status_date = clsCommon.GetPrintDate(strName, "dd/MM/yyyy")


                    strName = clsCommon.myCstr(grow.Cells("remarks").Value)
                    If strName.Length <= 0 Then
                        Throw New Exception("Length of remarks can not be left blank")
                    End If
                    obj.remarks = strName

                    strName = clsCommon.myCstr(grow.Cells("Pending Reason Description").Value)
                    If strName.Length > 30 Then
                        Throw New Exception("Length of Pending Reason Description can not be left blank")
                    End If
                    obj.Pending_Reason_DescManual = strName



                    strName = clsCommon.myCstr(grow.Cells("Work Order No.").Value)
                    If strName.Length > 20 Then
                        Throw New Exception("Length of Work Order No. can not be greater than 20")
                    End If
                    obj.workno = strName

                    strName = clsCommon.myCstr(grow.Cells("Bill No.").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Bill No. can not be greater than 50")
                    End If
                    obj.billno = strName

                    strName = clsCommon.myCdbl(grow.Cells("Bill Amount").Value)
                    obj.billamt = strName

                    strName = clsCommon.myCdbl(grow.Cells("Additional Charge").Value)

                    obj.addcharge = strName

                    strName = clsCommon.myCstr(grow.Cells("Response Time").Value)
                    If strName.Length > 10 Then
                        Throw New Exception("Length of Response Time can not be greater than 10")
                    End If
                    obj.responsetym = strName

                    strName = clsCommon.myCstr(grow.Cells("Status").Value)
                    If clsCommon.CompairString(strName, "P") = CompairStringResult.Equal Then
                        obj.compl_status = strName
                    ElseIf clsCommon.CompairString(strName, "C") = CompairStringResult.Equal Then
                        obj.compl_status = strName
                    End If
                    clscomplaintdetail.SaveData(obj, True, trans)
                    grow = Nothing
                    obj = Nothing


                Next

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message & " At Line No : " & i)
            End Try
        End If


    End Sub
    Private Sub rmManual_Click(sender As Object, e As EventArgs) Handles rmManual.Click
        ImportManual()
    End Sub

    Private Sub rmAutoEx_Click(sender As Object, e As EventArgs) Handles rmAutoEx.Click
        AutomaticExport()
    End Sub
    Sub ManualExport()
        Dim str As String = ""
        str = " select comp_id as [Complain Id],convert(varchar,comp_date,103) as [Complaint Date],TSPL_COMPLAINT_DETAIL.description as [Description],outletnamemanual as [Outlet Name] ,outlettypemanual as [Type],citymanual as [City],statemanual as [State],CountryManual as [Country],phone_no as [Phone No.],Asset_Type_NameManual as [Asset Type Description],item_make as [Make],model_no as [Model],size ,serial_no as [Serial No.],tag_no as [Tag No.],case when apex_no ='UW' then 'Under Warranty' when apex_no='OW' then 'Out Of Warranty' else 'On Your Freezer' end  as [Apex Pending W/O No.],primary_reason_descManual as [Primary Reason Description] ,Secondary_Reason_DescManual as [Secondary Reason Description],compl_given_by as [Complaint Given By],compl_given_to as [Complaint Given To],Franchise_DescManual as [Franchise Description],Service_Executive_DescManual as [Service Exceutive Name],convert(varchar,status_date,103)  as [Completed Date],response_time as [Response Time],remarks  ,Pending_Reason_DescManual as [Pending Reason Description],work_order_no as [Work Order No.],bill_no as [Bill No.],bill_amount as [Bill Amount],add_chrge as [Additional Charge],LocationManual as Location ,compl_status as [Status], 1 as [Manual],ComplaintTypeManual as [Complaint Type Description]  from TSPL_COMPLAINT_DETAIL "
        transportSql.ExporttoExcel(str, " and coalesce(post_status,'N')<>'Y' and tspl_complaint_detail.compl_status='" & CmbStatus.SelectedValue & "'", Me)
    End Sub
    Private Sub rmManualEx_Click(sender As Object, e As EventArgs) Handles rmManualEx.Click
        ManualExport()
    End Sub
End Class

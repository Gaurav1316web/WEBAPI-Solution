' ----------------- Created By Anubhooti On 10-Sep-2015 Against BM00000006783-------------------- '
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
Imports System.IO
Imports XpertERPEngine

Public Class FrmServiceVisitDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
#Region "Main Items"
    Const ColSNo As String = "SNo"
    Const ColItemCode As String = "Main Item"
    Const ColItemDesp As String = "Item Desp"
    Const ColItemSerialNo As String = "Serial No."
    Const ColItemType As String = "Item Type"
    Const ColIssueCode As String = "Issue Code"
    Const ColIssueName As String = "Issue Desp"
    Const ColItemPartNo As String = "ItemPartNo"
    Const ColIsSerial As String = "Is Serial"
    Const ColWarrStatus As String = "Warranty Status"
    Const ColChargeStatus As String = "Charge Status"
    Const ColRevisionNo As String = "Revision No"
    Const ColRemarks As String = "Remarks"
    Const ColAttachment As String = "Attachment"
    Const ColServiceType As String = "Service Type"
    Const ColItemServiceType As String = "Item Service Type"
    Const ColQty As String = "Qty"
#End Region
#Region "Child Items"
    Const ColCSNo As String = "SNo"
    Const ColCItemCode As String = "Child Item"
    Const ColCItemDesp As String = "Item Desp"
    Const ColCItemSerialNo As String = "Serial No."
    Const ColCItemType As String = "Item Type"
    Const ColCIssueCode As String = "Issue Code"
    Const ColCIssueName As String = "Issue Desp"
    Const ColCItemPartNo As String = "ItemPartNo"
    Const ColCIsSerial As String = "Is Serial"
    Const ColCMainItemCode As String = "Main Item"
    Const ColCWarrStatus As String = "Warranty Status"
    Const ColCChargeStatus As String = "Charge Status"
    Const ColCRevisionNo As String = "Revision No"
    Const ColCRemarks As String = "Remarks"
    Const ColCAttachment As String = "Attachment"
    Const ColCServiceType As String = "Service Type"
    Const ColCItemServiceType As String = "Item Service Type"
    Const ColCQty As String = "Qty"
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmServiceAllocation)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub FunReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        TxtRemarks.Text = ""
        CmbStatus.SelectedValue = ""
        TxtEnqNo.Value = ""
        LblEnqNo.Text = ""
        TxtRoutedEng.Value = ""
        LblRoutedEng.Text = ""
        TxtSerPlace.Text = ""
        TxtSVRNo.Text = ""
        txtBrowse.Text = ""
        Me.RadPageView1.SelectedPage = RadPageViewPage3

        dtpDate.Value = clsCommon.GETSERVERDATE()
        dtpSerDate.Value = clsCommon.GETSERVERDATE()
        '' Blank Grid
        gvMainItem.DataSource = Nothing
        gvMainItem.Rows.Clear()
        gvMainItem.Columns.Clear()
        gvChildItem.DataSource = Nothing
        gvChildItem.Rows.Clear()
        gvChildItem.Columns.Clear()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub
    Sub LoadBlankGridMain()
        gvMainItem.Rows.Clear()
        gvMainItem.Columns.Clear()

        Dim repoSno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSno.FormatString = ""
        repoSno.HeaderText = "S No."
        repoSno.Name = ColSNo
        repoSno.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSno.Width = 75
        gvMainItem.MasterTemplate.Columns.Add(repoSno)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = ColItemCode
        repoItemCode.Width = 75
        gvMainItem.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoItemDesp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesp.FormatString = ""
        repoItemDesp.HeaderText = "Item Desp"
        repoItemDesp.Name = ColItemDesp
        repoItemDesp.Width = 150
        repoItemDesp.ReadOnly = False
        gvMainItem.MasterTemplate.Columns.Add(repoItemDesp)

        Dim repoSerialNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSerialNo.FormatString = ""
        repoSerialNo.HeaderText = "Serial No."
        repoSerialNo.Name = ColItemSerialNo
        repoSerialNo.Width = 60
        repoSerialNo.ReadOnly = True
        gvMainItem.MasterTemplate.Columns.Add(repoSerialNo)

        Dim repoItemType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemType.FormatString = ""
        repoItemType.HeaderText = "Item Type "
        repoItemType.Name = ColItemType
        repoItemType.Width = 70
        repoItemType.IsVisible = False
        repoItemType.ReadOnly = True
        gvMainItem.MasterTemplate.Columns.Add(repoItemType)

        Dim repoIssueCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueCode.FormatString = ""
        repoIssueCode.HeaderText = "Issue Code"
        repoIssueCode.Name = ColIssueCode
        repoIssueCode.Width = 80
        repoIssueCode.IsVisible = True
        repoIssueCode.ReadOnly = True
        gvMainItem.MasterTemplate.Columns.Add(repoIssueCode)

        Dim repoIssueDesp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueDesp.FormatString = ""
        repoIssueDesp.HeaderText = "Issue Desp"
        repoIssueDesp.Name = ColIssueName
        repoIssueDesp.Width = 150
        repoIssueDesp.IsVisible = True
        repoIssueDesp.ReadOnly = True
        gvMainItem.MasterTemplate.Columns.Add(repoIssueDesp)

        Dim repoPartNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPartNo.FormatString = ""
        repoPartNo.HeaderText = "Issue Part No."
        repoPartNo.Name = ColItemPartNo
        repoPartNo.Width = 80
        repoPartNo.IsVisible = True
        repoPartNo.ReadOnly = True
        gvMainItem.MasterTemplate.Columns.Add(repoPartNo)

        Dim repoIsSerial As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSerial.FormatString = ""
        repoIsSerial.HeaderText = "Is Serial"
        repoIsSerial.Name = ColIsSerial
        repoIsSerial.Width = 80
        repoIsSerial.IsVisible = False
        repoIsSerial.ReadOnly = True
        gvMainItem.MasterTemplate.Columns.Add(repoIsSerial)

        Dim repoWarrStatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoWarrStatus.FormatString = ""
        repoWarrStatus.HeaderText = "Warranty Status"
        repoWarrStatus.Name = ColWarrStatus
        repoWarrStatus.Width = 105
        repoWarrStatus.IsVisible = True
        repoWarrStatus.DataSource = ClsServiceEnquiry.GetWarranty()
        gvMainItem.MasterTemplate.Columns.Add(repoWarrStatus)

        Dim repoChargeStatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoChargeStatus.FormatString = ""
        repoChargeStatus.HeaderText = "Charge Status"
        repoChargeStatus.Name = ColChargeStatus
        repoChargeStatus.Width = 105
        repoChargeStatus.IsVisible = True
        repoChargeStatus.DataSource = ClsServiceEnquiry.GetCharge()
        gvMainItem.MasterTemplate.Columns.Add(repoChargeStatus)

        Dim repoRevisionNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRevisionNo.FormatString = ""
        repoRevisionNo.HeaderText = "Revision No"
        repoRevisionNo.Name = ColRevisionNo
        repoRevisionNo.Width = 80
        repoRevisionNo.IsVisible = False
        repoRevisionNo.ReadOnly = True
        gvMainItem.MasterTemplate.Columns.Add(repoRevisionNo)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = ColRemarks
        repoRemarks.Width = 200
        repoRemarks.IsVisible = True
        gvMainItem.MasterTemplate.Columns.Add(repoRemarks)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Attachment"
        ShowBtn.Name = ColAttachment
        ShowBtn.Width = 80
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMainItem.MasterTemplate.Columns.Add(ShowBtn)

        Dim repoServiceType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoServiceType.FormatString = ""
        repoServiceType.HeaderText = "Service Type"
        repoServiceType.Name = ColServiceType
        repoServiceType.Width = 100
        repoServiceType.IsVisible = True
        repoServiceType.DataSource = GetServiceType()
        gvMainItem.MasterTemplate.Columns.Add(repoServiceType)

        Dim repoItemServiceType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoItemServiceType.FormatString = ""
        repoItemServiceType.HeaderText = "Item Service Type"
        repoItemServiceType.Name = ColItemServiceType
        repoItemServiceType.Width = 120
        repoItemServiceType.IsVisible = True
        repoItemServiceType.DataSource = GetItemServiceType()
        gvMainItem.MasterTemplate.Columns.Add(repoItemServiceType)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Qty"
        repoQty.Name = ColQty
        repoQty.Width = 80
        repoQty.IsVisible = True
        gvMainItem.MasterTemplate.Columns.Add(repoQty)

        gvMainItem.AllowAddNewRow = False
        gvMainItem.ShowGroupPanel = False
        gvMainItem.AllowColumnReorder = False
        gvMainItem.AllowRowReorder = False
        gvMainItem.EnableSorting = False
        gvMainItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvMainItem.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    ' ----------------- Get_Service_Type ------------------------
    Public Shared Function GetServiceType() As DataTable
        Dim DT_Ser_Type As DataTable = New DataTable
        DT_Ser_Type.Columns.Add("Code", GetType(String))
        DT_Ser_Type.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Ser_Type.NewRow()
        DR("Name") = "Change"
        DR("Code") = "C"
        DT_Ser_Type.Rows.Add(DR)

        DR = DT_Ser_Type.NewRow()
        DR("Name") = "Repair"
        DR("Code") = "R"

        DT_Ser_Type.Rows.Add(DR)
        DT_Ser_Type.AcceptChanges()

        Return DT_Ser_Type
    End Function
    ' ----------------- Get_Service_Type ------------------------
    Public Shared Function GetItemServiceType() As DataTable
        Dim DT_ISer_Type As DataTable = New DataTable
        DT_ISer_Type.Columns.Add("Code", GetType(String))
        DT_ISer_Type.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_ISer_Type.NewRow()
        DR("Name") = "Damaged"
        DR("Code") = "D"
        DT_ISer_Type.Rows.Add(DR)

        DR = DT_ISer_Type.NewRow()
        DR("Name") = "Repairable"
        DR("Code") = "R"

        DT_ISer_Type.Rows.Add(DR)
        DT_ISer_Type.AcceptChanges()

        Return DT_ISer_Type
    End Function

    Private Sub FrmServiceVisitDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub FrmServiceVisitDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        FunReset()
        LoadBlankGridMain()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click

    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        FunReset()
    End Sub

    Private Sub TxtRoutedEng__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtRoutedEng._MYValidating
        Try
            TxtRoutedEng.Value = clsEmployeeMaster.getFinder("", TxtRoutedEng.Value, isButtonClicked)
            If clsCommon.myLen(TxtRoutedEng.Value) > 0 Then
                LblRoutedEng.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM tspl_employee_master WHERE EMP_CODE='" & TxtRoutedEng.Value & "'"))
            Else
                LblRoutedEng.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtEnqNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtEnqNo._MYValidating
        Try
            TxtEnqNo.Value = ClsServiceEnquiry.GetFinder("", TxtRoutedEng.Value, isButtonClicked)
            If clsCommon.myLen(TxtEnqNo.Value) > 0 Then
                LblRoutedEng.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Remarks,'') AS Remarks FROM TSPL_SW_SERVICE_ENQUIRY WHERE Service_Enquiry_Code='" & TxtRoutedEng.Value & "'"))
            Else
                LblRoutedEng.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadEnqDetails(ByVal EnqNo As String)
        Dim DT As DataTable = New DataTable()
        DT = clsDBFuncationality.GetDataTable("SELECT TSPL_SW_SERVICE_ENQUIRY.*,TSPL_SW_SERVICE_ALLOCATION.Engineer_Code FROM TSPL_SW_SERVICE_ENQUIRY  LEFT OUTER JOIN TSPL_SW_SERVICE_ALLOCATION ON TSPL_SW_SERVICE_ALLOCATION.Service_Enquiry_Code  = TSPL_SW_SERVICE_ENQUIRY.Service_Enquiry_Code WHERE Service_Enquiry_Code='" & EnqNo & "'")
        If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
            dtpSerDate.Value = clsCommon.myCDate(DT.Rows(0)("Service_Enquiry_Date"))
            LblDateofSale.Text = clsCommon.myCDate(DT.Rows(0)("Date_Of_Sale"))
            LblEnqD.Text = clsCommon.myCstr(DT.Rows(0)("Service_Enquiry_Date"))
            LblDealerN.Text = clsCommon.myCstr(DT.Rows(0)("Dealer_Code"))
            LblCustGrp.Text = clsCommon.myCstr(DT.Rows(0)("Cust_Group_Code"))
            TxtRoutedEng.Value = clsCommon.myCstr(DT.Rows(0)("Engineer_Code"))
            If clsCommon.myLen(TxtRoutedEng.Value) > 0 Then
                LblRoutedEng.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM tspl_employee_master WHERE EMP_CODE='" & TxtRoutedEng.Value & "'"))
            Else
                LblRoutedEng.Text = ""
            End If
        Else
            dtpSerDate.Value = ""
            LblDateofSale.Text = ""
            LblEnqD.Text = ""
            LblDealerN.Text = ""
            LblCustGrp.Text = ""
            TxtRoutedEng.Value = ""
            LblRoutedEng.Text = ""
        End If
    End Sub

    Private Sub btnShowDoc_Click(sender As Object, e As EventArgs) Handles btnShowDoc.Click
        Dim ds_attachment As DataTable
        Dim filename As String = ""
        Dim file_path As String = ""
        Dim file_extn As String = ""
        Try

            ds_attachment = New DataTable
            ds_attachment = ClsApplicantEntry.GetDocument(txtcode.Value, txtcode.Value)

            If (ds_attachment IsNot Nothing AndAlso ds_attachment.Rows.Count > 0) Then
                If clsCommon.myCstr(ds_attachment.Rows(0)("DocName")) <> "" Then
                    filename = clsCommon.myCstr(ds_attachment.Rows(0)("DocName"))
                    Dim blob As Byte() = ds_attachment.Rows(0)("DOCUMENT_FILE")
                    file_path = "C:\ERPTempFolder"
                    Dim dir As DirectoryInfo = New DirectoryInfo(file_path)
                    If dir.Exists = False Then
                        dir.Create()
                    End If
                    Dim index As Int16 = filename.LastIndexOf(".")
                    file_extn = filename.Substring(index)
                    filename = filename.Remove(index)
                    filename += (clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yy hh:mm:ss")).ToString()
                    filename = filename.Replace(" ", "")
                    filename = filename.Replace("/", "_")
                    filename = filename.Replace(":", "_")
                    Dim fs As FileStream = File.Create(file_path + "\\" + filename + file_extn)
                    fs.Write(blob, 0, blob.Length)
                    fs.Close()
                    System.Diagnostics.Process.Start(file_path + "\\" + filename + file_extn)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No document found", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No document found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        OpenFileDialog1.ShowDialog()
        txtBrowse.Text = OpenFileDialog1.SafeFileName
    End Sub

    Private Sub BtnDeleteDoc_Click(sender As Object, e As EventArgs) Handles BtnDeleteDoc.Click
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            myMessages.blankValue("Applicant ID ")
            txtcode.Focus()
        ElseIf ClsApplicantEntry.DeleteDocData(txtcode.Value, txtcode.Value) Then
            clsCommon.MyMessageBoxShow(Me, "Document Deleted Successfully.", Me.Text)
            txtBrowse.Text = ""
            OpenFileDialog1.Reset()
        End If
    End Sub
End Class
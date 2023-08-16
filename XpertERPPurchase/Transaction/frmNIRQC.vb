Imports common
Imports System.Data.SqlClient
Imports System

Public Class frmNIRQC
    Inherits FrmMainTranScreen
#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.capexmaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmCapexMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadQCStatus()
        AddNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for Post ")
    End Sub
    Sub LoadQCStatus()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "OK"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "Not OK"
        dt.Rows.Add(dr)


        cboVisualQCStatus.DataSource = dt
        cboVisualQCStatus.ValueMember = "Code"
        cboVisualQCStatus.DisplayMember = "Name"
    End Sub
    Sub AddNew()
        isNewEntry = True
        txtCode.Value = Nothing
        txtCode.Focus()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        txtDate.Text = clsCommon.GETSERVERDATE()
        BlankAllControls()
    End Sub
    Sub BlankAllControls()
        txtCode.Value = ""
        txtRemarks.Text = ""
        cboVisualQCStatus.SelectedValue = ""
        txtMRNNo.Value = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtRemarks.Text = ""
        BlankMRNFields()
    End Sub
    Sub BlankMRNFields()
        lblGRNNo.Text = ""
        lblGRNDate.Text = ""
        lblWeightmentNo.Text = ""
        lblWeightmentDate.Text = ""
        lblRAL.Text = ""
        lblVendorCode.Text = ""
        lblVendorName.Text = ""
        lblBillToLocationCode.Text = ""
        lblBillToLocationName.Text = ""
        lblItem.Text = ""
        lblItemName.Text = ""
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        AddNew()
        Dim obj As New clsNIRQC()
        obj = clsNIRQC.GetData(strCode, NavTyep, Nothing)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            isNewEntry = False
            txtCode.Value = obj.Document_No
            txtDate.Value = obj.Document_Date
            txtMRNNo.Value = obj.MRN_No
            txtRemarks.Text = obj.QC_Remarks
            cboVisualQCStatus.SelectedValue = clsCommon.myCstr(obj.QC_Status)
            UsLock1.Status = obj.Status
            If obj.Status = ERPTransactionStatus.Approved Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
            Else
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
            End If
            LoadMRNData()
        End If

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim obj As New clsNIRQC()
                obj.Document_No = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.MRN_No = txtMRNNo.Value
                obj.QC_Remarks = txtRemarks.Text
                obj.QC_Status = clsCommon.myCDecimal(cboVisualQCStatus.SelectedValue)
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myLen(cboVisualQCStatus.SelectedValue) <= 0 Then
            cboVisualQCStatus.Focus()
            Throw New Exception("Please select " + cboVisualQCStatus.MyLinkLable1.Text)
        End If
        If clsCommon.myLen(txtMRNNo.Value) <= 0 Then
            txtMRNNo.Focus()
            Throw New Exception("Please select " + txtMRNNo.MyLinkLable1.Text)
        End If
        Return True
    End Function
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsNIRQC.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub FrmCapexMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        txtCode.Value = clsNIRQC.getFinder("", txtCode.Value, isButtonClicked)
        If txtCode.Value <> "" Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            AddNew()
        End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub txtMRNNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMRNNo._MYValidating
        Dim qry As String = "select 
TSPL_MRN_DETAIL.MRN_No,TSPL_MRN_HEAD.MRN_Date,
TSPL_MRN_HEAD.Against_GRN,TSPL_GRN_HEAD.GRN_Date ,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo,TSPL_MRN_HEAD.Vendor_Code,TSPL_MRN_HEAD.Vendor_Name,TSPL_MRN_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc
from TSPL_MRN_DETAIL
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code 
left outer join TSPL_MRN_HEAD  on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No
left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_MRN_HEAD.Against_GRN
left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_MRN_HEAD.Against_GRN
left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MRN_HEAD.Bill_To_Location"
        Dim whrcls As String = "TSPL_MRN_HEAD.Status=1 and TSPL_MRN_HEAD.NIR_QC=1 and TSPL_ITEM_MASTER.NIR_QC=1
and not exists(select 1 from TSPL_NIR_QC where TSPL_NIR_QC.MRN_No=TSPL_MRN_DETAIL.MRN_No and TSPL_NIR_QC.Document_No not in ('" + txtMRNNo.Value + "'))  "
        txtMRNNo.Value = clsCommon.ShowSelectForm("NICQCMRNFnd", qry, "MRN_No", whrcls, txtMRNNo.Value, "", isButtonClicked)
        LoadMRNData()
    End Sub
    Private Sub LoadMRNData()
        BlankMRNFields()
        Dim qry As String = "select TSPL_MRN_HEAD.Against_GRN,TSPL_GRN_HEAD.GRN_Date ,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo,TSPL_MRN_HEAD.Vendor_Code,TSPL_MRN_HEAD.Vendor_Name,TSPL_MRN_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc
from TSPL_MRN_DETAIL
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code 
left outer join TSPL_MRN_HEAD  on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No
left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_MRN_HEAD.Against_GRN
left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_MRN_HEAD.Against_GRN
left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MRN_HEAD.Bill_To_Location
where TSPL_MRN_DETAIL.MRN_No='" + txtMRNNo.Value + "' and TSPL_MRN_HEAD.Status=1 and TSPL_MRN_HEAD.NIR_QC=1 and TSPL_ITEM_MASTER.NIR_QC=1"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblGRNNo.Text = clsCommon.myCstr(dt.Rows(0)("Against_GRN"))
            If dt.Rows(0)("GRN_Date") IsNot DBNull.Value Then
                lblGRNDate.Text = clsCommon.myCstr(dt.Rows(0)("GRN_Date"))
            End If
            lblWeightmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_Code"))
            If dt.Rows(0)("Weighment_Date") IsNot DBNull.Value Then
                lblWeightmentDate.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_Date"))
            End If
            lblRAL.Text = clsCommon.myCstr(dt.Rows(0)("RefTendorNo"))
            lblVendorCode.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            lblBillToLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            lblBillToLocationName.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            lblItem.Text = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            lblItemName.Text = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
        End If
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsNIRQC.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class

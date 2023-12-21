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


Public Class frmMCCGateEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Public strDocCode As String = ""
#End Region


    Private Sub FrmGateEntrySale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                          "========Table Name=========" + Environment.NewLine + _
                          "TSPL_MCC_GATE_ENTRY" + Environment.NewLine + _
                          "=========Setting Name======" + Environment.NewLine + _
                          "IsAutoTankerWeightment")
        End If
    End Sub

    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim LocationName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_LOCATION_MASTER where Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and Location_Code ='" & obj.Default_LocCode & "'"))
                If clsCommon.myLen(LocationName) > 0 Then
                    txtLocation.Value = obj.Default_LocCode
                    lblLocation.Text = obj.Default_LocName

                Else
                    txtLocation.Value = ""
                    lblLocation.Text = ""
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub FrmGateEntrySale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoTankerWeightment, clsFixedParameterCode.IsAutoTankerWeightment, Nothing)) = 1 Then
            Throw New Exception("Work only is Auto tanker weighment is on")
        End If
        SetUserMgmtNew()
        AddNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, NavigatorType.Current)
        End If
      
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMCCGateEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As clsMCCGateEntry = clsMCCGateEntry.GetData(strCode, arrLoc, NavTyep)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0 Then
                isNewEntry = False
                fndGateEntryNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtLocation.Value = obj.Location_Code
                lblLocation.Text = obj.Location_Name
                txtTransporter.Value = obj.Transporter_Code
                lblTransporter.Text = obj.Transporter_name
                UsLock1.Status = obj.Status
                txtRemarks.Text = obj.Remarks
                txtTankerNo.Value = obj.Tanker_No
                txtRemarks.Text = obj.Remarks
                If obj.Status = ERPTransactionStatus.Posted Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                End If
                fndGateEntryNo.MyReadOnly = True
            Else
                Throw New Exception("Document No not found ")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsMCCGateEntry()
                obj.Document_No = fndGateEntryNo.Value
                obj.Document_Date = txtDate.Value
                obj.Location_Code = txtLocation.Value
                obj.Tanker_No = txtTankerNo.Value
                obj.Transporter_Code = txtTransporter.Value
                obj.Remarks = txtRemarks.Text
                If (clsMCCGateEntry.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        ' = KUNAL > TICKET : BM00000009575 ========
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            Return False
        End If

        If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Please select Location")
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Please select Tanker")
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtTankerNo.Value)) <= 0 Then
            txtTankerNo.Focus()
            Throw New Exception("Please select Tanker")
        End If
        Return True
    End Function

    Private Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsMCCGateEntry.DeleteData(fndGateEntryNo.Value, arrLoc)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub AddNew()
        isNewEntry = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        fndGateEntryNo.Value = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtLocation.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        LOCATIONRIGTHS()
        fndGateEntryNo.MyReadOnly = False
        btnsave.Text = "Save"
        btnPost.Enabled = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        txtRemarks.Text = ""
        txtTransporter.Value = ""
        lblTransporter.Text = ""
        txtTankerNo.Value = ""
    End Sub

    Private Sub fndTransporter__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtTransporter._MYValidating
        txtTransporter.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Form_Type='TTM' ", txtTransporter.Value, isButtonClicked)

        If clsCommon.myLen(txtTransporter.Value) > 0 Then
            lblTransporter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where form_type='TTM' and Vendor_Code='" & txtTransporter.Value & "'"))
        Else
            lblTransporter.Text = ""
        End If
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtLocation._MYValidating
        txtLocation.Value = clsLocation.getFinder(" Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and tspl_location_master.location_code in (" + arrLoc + ")", txtLocation.Value, isButtonClicked)

        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
        Else
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub fndGateEntryNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndGateEntryNo._MYNavigator
        Dim qry As String = Nothing
        Try
            qry = "select count(*) from TSPL_GATEENTRY_SALE where Document_No='" + fndGateEntryNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndGateEntryNo.MyReadOnly = True
            ElseIf check <= 0 Then
                fndGateEntryNo.MyReadOnly = False
            End If

            LoadData(fndGateEntryNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndGateEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNo._MYValidating
        Dim qry As String = " select TSPL_MCC_GATE_ENTRY.Document_No,convert(varchar, TSPL_MCC_GATE_ENTRY.Document_Date,103) as Document_Date,TSPL_MCC_GATE_ENTRY.Location_Code,  TSPL_MCC_GATE_ENTRY.Tanker_No,TSPL_MCC_GATE_ENTRY.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name as Transporter_Name,TSPL_MCC_GATE_ENTRY.Remarks," + Environment.NewLine +
        " case when TSPL_MCC_GATE_ENTRY.Status=1 then 'Approved' else 'Pending' end as Status " + Environment.NewLine +
        " from TSPL_MCC_GATE_ENTRY " + Environment.NewLine +
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_GATE_ENTRY.Location_Code " + Environment.NewLine +
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MCC_GATE_ENTRY.Transporter_Code "
        Dim whrClas As String = "TSPL_MCC_GATE_ENTRY.Location_Code in (" + arrLoc + ")  "
        Reset()
        LoadData(clsCommon.ShowSelectForm("MCGEDF", qry, "Document_No", whrClas, fndGateEntryNo.Value, "Document_No", isButtonClicked), NavigatorType.Current)
        qry = Nothing
        whrClas = Nothing
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Dim msg As String = Nothing
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        Try

            isFlag = True
            If (myMessages.postConfirm()) Then
                If (clsMCCGateEntry.PostData(MyBase.Form_ID, arrLoc, fndGateEntryNo.Value)) Then
                    msg = "Successfully posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(fndGateEntryNo.Value, NavigatorType.Current)
                End If
            End If
            isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
            msg = Nothing
            qry = Nothing
            dt = Nothing
        End Try
    End Sub

    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Dim qry As String = "select Tanker_No as TankerNo, tanker_transporter_Code as [Transporter Code],TSPL_VENDOR_MASTER.Vendor_Name as [Transporter Name] from tspl_tanker_master left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=tspl_tanker_master.tanker_transporter_Code  "
        Dim whr As String = " isGateOut=1 "
        txtTankerNo.Value = clsCommon.ShowSelectForm("MCGES", qry, "TankerNo", whr, txtTankerNo.Value, "", isButtonClicked, "")
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " where " + whr)
        txtTransporter.Value = ""
        lblTransporter.Text = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtTransporter.Value = clsCommon.myCstr(dt.Rows(0)("Transporter Code"))
            lblTransporter.Text = clsCommon.myCstr(dt.Rows(0)("Transporter Name"))
        End If
    End Sub
End Class

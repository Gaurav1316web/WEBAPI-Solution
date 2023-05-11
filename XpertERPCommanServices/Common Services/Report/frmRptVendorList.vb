Imports common

Public Class FrmRptVendorList
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub

    Public Shared Sub PrintData(ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList)

        If isVendorSelect AndAlso ArrVendor.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
            Return
        End If
        Dim Vendor As String = ""
        If isVendorSelect Then
            Vendor = clsCommon.GetMulcallString(ArrVendor)
            Vendor = Vendor.Replace("'", "")

        End If
        Dim qry As String = "select '" + Vendor + "' as Vendor,vendormaster.Vendor_Code ,vendormaster.Vendor_Name ,vendormaster.Add1++ vendormaster.Add2 ++vendormaster.Add3 as 'Adress' ,vendormaster.Vendor_Group_Code_Desc ,vendormaster.Service_Tax_No ,vendormaster.Tin_No ,companymaster.Comp_Name  ,companymaster.Logo_Img ,companymaster.Logo_Img2 ,vendormaster.GSTFinalNo as [GSTIN No],TSPL_STATE_MASTER.GST_STATE_Code as [GST State Code],case when  vendormaster.GSTRegistered =1 then 'Yes' else 'No' end as Registered  from TSPL_VENDOR_MASTER vendormaster   left outer join TSPL_COMPANY_MASTER  companymaster on vendormaster.Comp_Code =companymaster.Comp_Code left outer join TSPL_STATE_MASTER  on TSPL_STATE_MASTER.STATE_CODE = vendormaster.State_Code  where 2=2  "


        If isVendorSelect Then
            Vendor = clsCommon.GetMulcallString(ArrVendor)
            Vendor = Vendor.Replace("'", "")
            qry += " and vendormaster.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        Else
            qry += ""
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim FRMcrys As New frmCrystalReportViewer
        FRMcrys.funreport(CrystalReportFolder.PurchaseOrder, dt, "VendorList", "FrmRptVendorList")
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub Print()
        PrintData(chkSelect.IsChecked, cbgVendor.CheckedValue)
    End Sub
    Sub loadVendor()
        Dim qry As String = "select Vendor_Code as VendorCode ,Vendor_Name  as VendorName from TSPL_VENDOR_MASTER Where Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "VendorCode"
        cbgVendor.DisplayMember = "VendorCode"
    End Sub

    Private Sub FrmRptVendorList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chkAll.IsChecked = True
        loadVendor()
        SetUserMgmtNew()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRptVendorList)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub chkAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkAll.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        chkAll.IsChecked = True
        loadVendor()
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    Private Function funSetUserAccess() As Boolean
        Try

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = "VEN-LST-RPT"
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access
                'btnsave.Enabled = False
            End If
            If strTemp(2) = "0" Then 'Grant modify access
                'btndelete.Enabled = False
            End If

            funSetUserAccess = True
        Catch er As Exception
            myMessages.myExceptions(er)
            Return False
        End Try
        Return True
    End Function




    Private Sub FrmRptVendorList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
End Class

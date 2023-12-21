Imports common
Imports System.Data.SqlClient

Public Class FrmMRDAReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.MRDAReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If


        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub



    Private Sub FrmMRDAReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadVendor()
        LoadMRDA()
        LoadLocation()

        chkLocAll.IsChecked = True
        'If objCommonVar.CurrentUser <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Public Sub LoadLocation()
        'Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  WHERE  Status='N' order by Vendor_Code"
        cgvvendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvvendor.ValueMember = "Vendor_Code"
        cgvvendor.DisplayMember = "Vendor_Name"
        rdomrdaall.IsChecked = True
    End Sub

    Sub LoadMRDA()
        Dim qry As String = "select rmda_no from TSPL_SRN_HEAD where RMDA_No!='' order by RMDA_No"
        cgvMRDA.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvMRDA.ValueMember = "rmda_no"
        cgvMRDA.DisplayMember = "rmda_no"
        chkvendorAll.IsChecked = True
    End Sub

    Private Sub rdomrdaall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdomrdaall.ToggleStateChanged
        cgvMRDA.Enabled = Not rdomrdaall.IsChecked
        If chkvenSelect.IsChecked = True Then
            'LoadVendor()
            chkvendorAll.IsChecked = True
        End If
    End Sub

    Private Sub chkvendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkvendorAll.ToggleStateChanged
        cgvvendor.Enabled = Not chkvendorAll.IsChecked
        If mrdaselect.IsChecked = True Then
            'LoadMRDA()
            rdomrdaall.IsChecked = True
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()
        Dim Vendor As String = ""
        Dim LocCode As String = ""
        Dim Vendor12 As String = ""
        Dim LOcCode12 As String = ""
        Try
            Dim locationArr As ArrayList = cbgLocation.CheckedValue

            If chkvenSelect.IsChecked AndAlso cgvvendor.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Vendor", Me.Text)
                Return
            End If
            If chkvenSelect.IsChecked AndAlso cgvvendor.CheckedValue.Count > 0 Then
                Vendor = clsCommon.GetMulcallString(cgvvendor.CheckedValue)

                Vendor12 = Vendor.Replace("'", "")
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                LocCode = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LOcCode12 = LocCode.Replace("'", "")
            End If
            If mrdaselect.IsChecked AndAlso cgvMRDA.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one MRDA No.", Me.Text)
                Return
            End If

            Dim fromdate As String = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy")
            Dim todate As String = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")

            Dim qry As String = "SELECT '" + fromdate + "' as FromDate,'" + todate + "' as Todate,'" + LOcCode12 + "'  as LocFilter,'" + Vendor12 + "'  as VendorFilter, TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date, TSPL_SRN_HEAD.Vendor_Name, TSPL_SRN_HEAD.Ship_To_Location, TSPL_SRN_HEAD.Bill_To_Location, TSPL_SRN_HEAD.RMDA_No, TSPL_SRN_HEAD.RMDA_Date,TSPL_SRN_HEAD.Remarks,TSPL_SRN_HEAD.Description, TSPL_SRN_DETAIL.Item_Code,   TSPL_SRN_DETAIL.Item_Desc, TSPL_SRN_DETAIL.Rejected_Qty, TSPL_SRN_DETAIL.Item_Cost,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.Rejected_Qty*TSPL_SRN_DETAIL.Item_Cost as Amount,TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,'" + fromdate + "' as Fromdate,'" + todate + "' as Todate    FROM         TSPL_SRN_HEAD INNER JOIN    TSPL_SRN_DETAIL ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No LEFT OUTER JOIN     TSPL_COMPANY_MASTER ON TSPL_SRN_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code "
            qry += "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SRN_HEAD.Bill_To_Location where 2=2"


            qry += " and CONVERT(date,TSPL_SRN_HEAD.RMDA_Date ,103) >=CONVERT(date,'" + dtpFromDate.Value + "',103)  and CONVERT(date,TSPL_SRN_HEAD.RMDA_Date ,103)  <=CONVERT(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select one location ", Me.Text)
                    Return
                End If
                qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
            End If

            If mrdaselect.IsChecked = True Then
                qry += "    and TSPL_SRN_HEAD.rmda_no in(" + (clsCommon.GetMulcallString(cgvMRDA.CheckedValue)) + ")"
            ElseIf chkvenSelect.IsChecked = True Then
                qry += "    and TSPL_SRN_HEAD.vendor_code in(" + (clsCommon.GetMulcallString(cgvvendor.CheckedValue)) + ")"

            End If




            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptMRDAReport", "MRDA Report")
            frmCRV = Nothing



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        LoadMRDA()
        LoadVendor()
        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        chkLocAll.IsChecked = True
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "MRDA-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub FrmMRDAReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If


    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

End Class

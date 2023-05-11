Imports common
Imports System
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class FrmTDSsectionSummary
    Inherits FrmMainTranScreen
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub
    Sub Print()
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")
        Dim docarr As ArrayList = cbgSection.CheckedValue

        Dim DocFilter As String = ""
        Dim LocFilter As String = ""
        If chkSelect.IsChecked = True AndAlso cbgSection.CheckedValue.Count > 0 Then
            DocFilter = clsCommon.GetMulcallString(cbgSection.CheckedValue)
            DocFilter = DocFilter.Replace("'", "")
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            LocFilter = LocFilter.Replace("'", "")
        End If

        Dim qry As String = "SELECT '" + DocFilter + "' as DocFilter,'" + LocFilter + "' as Locfilter,  TSPL_REMITTANCE.Section_Code, TSPL_REMITTANCE.Section_Description, TSPL_REMITTANCE.Calculated_Total_TDS,TSPL_REMITTANCE.Calculated_Surcharge, TSPL_REMITTANCE.Calculated_Edu_Cess, TSPL_REMITTANCE.Calculated_Sec_Educess, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER .Logo_Img,TSPL_COMPANY_MASTER .Logo_Img2,'" + fromdate + "' as FilterFromDate,'" + todate + "' as ToFilterDate FROM  TSPL_REMITTANCE LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_REMITTANCE.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  where convert(Date, Document_Date,103) >= Convert(Date,'" + fromdate + "',103) and Convert(date,Document_Date,103) <=Convert(Date ,'" + todate + "',103) "

        If chkSelect.IsChecked = True AndAlso cbgSection.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Document")
            Return
        ElseIf chkSelect.IsChecked = True AndAlso cbgSection.CheckedValue.Count > 0 Then
            qry += " and TSPL_REMITTANCE.Section_Code  in (" + clsCommon.GetMulcallString(docarr) + ")"
        End If
        qry += "  and TSPL_REMITTANCE.Remit_TDS ='Y'"
        If ChkLocSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select one location ")
                Return
            End If
            qry += "and substring(TSPL_REMITTANCE.Branch_GL_AC ,LEN(TSPL_REMITTANCE.Branch_GL_AC )-2,3)  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

            'qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim frmCrystalReportViewer As New frmCrystalReportViewer
        frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "frmTDSsectionsummary", "TDS Section Summary")



    End Sub
    Public Sub sectionLoad()
        Dim qry As String = "select TDS_Group as Code ,Description  from TSPL_TDS_SECTION_MASTER "
        cbgSection.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSection.ValueMember = "Code"

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TDSSectionSummaryReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmTDSsectionSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        SetUserMgmtNew()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        sectionLoad()
        chkAll.IsChecked = True
        chkLocAll.IsChecked = True

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
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

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TDS-SEC-RPT"
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
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()

        chkLocAll.IsChecked = True
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        sectionLoad()
        chkAll.IsChecked = True
    End Sub


    Private Sub chkAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAll.ToggleStateChanged
        cbgSection.Enabled = Not chkAll.IsChecked
    End Sub


    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub FrmTDSsectionSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C AndAlso MyBase.isDeleteFlag Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso MyBase.isDeleteFlag Then
            Reset()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso MyBase.isDeleteFlag Then
            Print()
        End If

    End Sub
End Class

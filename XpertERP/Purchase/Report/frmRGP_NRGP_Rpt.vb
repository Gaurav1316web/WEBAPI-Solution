''--22/06/2012--[Pankaj Kumar]---- Added New Report That Prints Multiple RGP/NRGP according to Selection Criteria-----Req By--Amit Sir
'--preeti gupta ticket no-[BM00000003142] 
Imports common
Imports System.Data.SqlClient

Public Class frmRGP_NRGP_Rpt
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub frmRGP_NRGP_Rpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnRGP_NRGP_Rpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub Reset()
        FIllTypeList()
        LoadDocuments()
        chkDocumentAll.IsChecked = True
        LoadLocation()
        chkLocAll.IsChecked = True
        LoadVendor()
        chkVendorAll.IsChecked = True
        LoadDiliveredBy()
        chkDlverdByAll.IsChecked = True
        dtpFromDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
    End Sub

    Sub FIllTypeList()
        ddlType.Items.Clear()
        ddlType.Items.Add("Both")
        ddlType.Items.Add("Returnable Gate Pass")
        ddlType.Items.Add("Non Returnable Gate Pass")
        ddlType.SelectedText = "Both"
    End Sub

    Sub LoadDocuments()
        Dim qry As String = "Select RGP_No as [Document No], RGP_Date as Date from TSPL_RGP_HEAD"
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Document No"
        cbgDocument.DisplayMember = "Name"
    End Sub

    Sub LoadLocation()
        Dim qry As String = "Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Date"
    End Sub

    Sub LoadVendor()
        Dim qry As String = "Select Vendor_Code as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
    End Sub

    Sub LoadDiliveredBy()
        Dim qry As String = "Select EMP_CODE as Code, Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        cbgDlverdBy.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDlverdBy.ValueMember = "Code"
        cbgDlverdBy.DisplayMember = "Name"
    End Sub

    Private Sub chkDocumentAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDocumentAll.ToggleStateChanged
        cbgDocument.Enabled = False
    End Sub

    Private Sub ChkDocumentSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkDocumentSelect.ToggleStateChanged
        cbgDocument.Enabled = True
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = False
    End Sub

    Private Sub chkVendorSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorSelect.ToggleStateChanged
        cbgVendor.Enabled = True
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkDlverdByAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDlverdByAll.ToggleStateChanged
        cbgDlverdBy.Enabled = False
    End Sub

    Private Sub chkDlveredBySelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDlveredBySelect.ToggleStateChanged
        cbgDlverdBy.Enabled = True
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintReport()
    End Sub

    Public Sub PrintReport()
        Dim LocCode As String
        Dim Vendor As String

        Dim FromDate As String = dtpFromDate.Value.ToString("dd-MM-yyyy")
        Dim Todate1 As String = dtpToDate.Value.ToString("dd-MM-yyyy")
        Try
            If ChkDocumentSelect.IsChecked = True AndAlso cbgDocument.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast Single Document Or Select All")
            End If
            If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast Single Vendor Or Select All")
            Else
                Vendor = clsCommon.GetMulcallString(cbgVendor.CheckedValue)
            End If
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast Single Location Or Select All")
            Else
                LocCode = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            End If
            If chkDlveredBySelect.IsChecked = True AndAlso cbgDlverdBy.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast Single Employee Or Select All")
           
            End If
            Dim strqry As String = "SELECT  TSPL_RGP_HEAD.Modify_By,'" + FromDate + "' as FormDate,'" + Todate1 + "' as ToDate,'" + LocCode.Replace("'", "") + "'  as LocFilter,'" + Vendor.Replace("'", "") + "'  as VendorFilter, (select emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE =TSPL_RGP_HEAD .Delivered_By )as DeliverdBy ,TSPL_LOCATION_MASTER.Tin_No as location_tin_no, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end   as Location_address,convert(varchar,TSPL_RGP_HEAD.RoadPermit_Date,103) as RoadPermit_Date ,TSPL_RGP_HEAD.Road_Permit_No ,TSPL_RGP_HEAD.VehicleNo ,TSPL_RGP_HEAD.Modify_By ,Department,TSPL_DEPARTMENT_MASTER .DEPARTMENT_NAME,TSPL_RGP_HEAD.RGP_No,  convert(varchar,TSPL_RGP_HEAD.RGP_Date,103) as RGP_Date, TSPL_RGP_HEAD.Doc_Type, TSPL_RGP_HEAD.Vendor_Code, " & _
                      "TSPL_RGP_HEAD.Vendor_Name, TSPL_RGP_HEAD.VehicleNo, TSPL_RGP_HEAD.GPNo, TSPL_RGP_HEAD.GPDate, TSPL_RGP_HEAD.Reason, " & _
                     " TSPL_RGP_HEAD.Remarks, TSPL_RGP_HEAD.Posting_Date, TSPL_RGP_HEAD.comp_code, TSPL_RGP_HEAD.Location, " & _
                      "TSPL_RGP_HEAD.Document_Amount,TSPL_RGP_HEAD.Created_By, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, " & _
                      "TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.Logo_Img, " & _
                     " TSPL_COMPANY_MASTER.Logo_Img2, TSPL_RGP_DETAIL.Line_No, TSPL_RGP_DETAIL.Item_Code, TSPL_RGP_DETAIL.Item_Desc, " & _
                     " TSPL_RGP_DETAIL.RGP_Qty, TSPL_RGP_DETAIL.Unit_code, TSPL_RGP_DETAIL.Item_Cost, TSPL_RGP_DETAIL.Amount, " & _
                      "TSPL_VENDOR_MASTER.Add1 AS venadd1, TSPL_VENDOR_MASTER.Add2 AS venadd2, TSPL_VENDOR_MASTER.Add3 AS venadd3, " & _
                      "TSPL_VENDOR_MASTER.City_Code_Desc as vencity,TSPL_VENDOR_MASTER.Lst_No,TSPL_VENDOR_MASTER.CST," & _
                      " ISNULL(TSPL_RGP_HEAD.AGAINST_NRGP,'') AS AGAINST_NRGP,ISNULL(TSPL_RGP_HEAD.AGAINST_RGP,'') AS AGAINST_RGP,TSPL_RGP_HEAD.Doc_Type," & _
                      " Case When TSPL_RGP_HEAD.Doc_Type='RGP' Then 'Returnable Gate pass'  When TSPL_RGP_HEAD.Doc_Type='RGPR' Then 'Returnable Gate pass Return'  When TSPL_RGP_HEAD.Doc_Type='NRGP' Then 'Non Returnable Gate Pass' else 'Non Returnable Gate Pass Return' end as RGPType " & _
                    " FROM TSPL_RGP_HEAD INNER JOIN " & _
                    "  TSPL_RGP_DETAIL ON TSPL_RGP_HEAD.RGP_No = TSPL_RGP_DETAIL.RGP_No INNER JOIN " & _
                     " TSPL_VENDOR_MASTER ON TSPL_RGP_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN " & _
                     " TSPL_COMPANY_MASTER ON TSPL_RGP_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_LOCATION_MASTER on TSPL_RGP_HEAD.Location=TSPL_LOCATION_MASTER .Location_Code left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =TSPL_RGP_HEAD .Department " & _
                     " Where CONVERT(Date, RGP_Date, 103)>=CONVERT(Date, '" + dtpFromDate.Value + "', 103) AND CONVERT(Date, RGP_Date, 103)<=CONVERT(Date, '" + dtpToDate.Value + "', 103)   "

            If ChkDocumentSelect.IsChecked AndAlso cbgDocument.CheckedValue.Count > 0 Then

                strqry += " AND TSPL_RGP_HEAD.RGP_No in (" + clsCommon.GetMulcallString(cbgDocument.CheckedValue) + ")"
            End If
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then

                strqry += " AND TSPL_RGP_HEAD.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count > 0 Then

                strqry += " AND TSPL_RGP_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
            End If
            If chkDlveredBySelect.IsChecked = True AndAlso cbgDlverdBy.CheckedValue.Count > 0 Then

                strqry += " AND TSPL_RGP_HEAD.Delivered_By in (" + clsCommon.GetMulcallString(cbgDlverdBy.CheckedValue) + ")"

            End If

            If ddlType.Text = "Returnable Gate Pass" Then
                strqry += " And TSPL_RGP_HEAD.Doc_Type IN ('RGP','RGPR')"
            ElseIf ddlType.Text = "Non Returnable Gate Pass" Then
                strqry += " And TSPL_RGP_HEAD.Doc_Type IN('NRGP','NRGPR')"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            'PurchaseOrderViewer.funreport(dt, "rptRGPForAll", "RGP Report")
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptRGPForAll", "RGP Report", "rptCompanyAddress.rpt")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    
    Private Sub frmRGP_NRGP_Rpt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
   
End Class

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'========shivani Tyagi
Public Class FrmPendingProvisionReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim arrLoc As String = Nothing
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmPendingProvisionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport

    End Sub
    Private Sub FrmPendingProvisionReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
        Reset()
    End Sub
    Sub LoadVendorType()
        isInsideLoadData = True
        Dim qry As String = "select distinct Vendor_Type  from TSPL_PROVISION_ENTRY "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        cbVendorType.DataSource = dt
        cbVendorType.ValueMember = "Vendor_Type"
        cbVendorType.DisplayMember = "Vendor_Type"
        isInsideLoadData = False
    End Sub
    Private Sub FrmPendingProvisionReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
    Sub LoadLocation()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If

        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"

    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name] from TSPL_VENDOR_MASTER Where Status='N'  "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(RbSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv
        Load_Report()
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadLocation()
        LoadVendor()
        'cboUnit.Text = "Kg"
        chkLocationAll.CheckState = CheckState.Checked
        chkVendorAll.CheckState = CheckState.Checked
        LoadVendorType()
        RbAll.CheckState = CheckState.Checked
        RbDetail.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        Try
            If gv.Rows.Count > 0 Then
                print(EnumExportTo.Excel)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPendingProvisionReport & "'"))
                If chkLocationSelect.IsChecked Then
                    Dim strLocationName As String = ""
                    For Each StrName As String In cbgLocation.CheckedDisplayMember
                        If clsCommon.myLen(strLocationName) > 0 Then
                            strLocationName += ", "
                        End If
                        strLocationName += StrName
                    Next
                    Dim strLocationCode As String = ""
                    For Each StrCode As String In cbgLocation.CheckedValue
                        If clsCommon.myLen(strLocationCode) > 0 Then
                            strLocationCode += ", "
                        End If
                        strLocationCode += StrCode
                    Next
                    arrHeader.Add((" Location Name: " + strLocationName + " "))
                End If
                If chkVendorSelect.IsChecked Then
                    Dim strVendorName As String = ""
                    For Each StrName As String In cbgVendor.CheckedDisplayMember
                        If clsCommon.myLen(strVendorName) > 0 Then
                            strVendorName += ", "
                        End If
                        strVendorName += StrName
                    Next
                    Dim strVendorcode As String = ""
                    For Each StrCode As String In cbgVendor.CheckedValue
                        If clsCommon.myLen(strVendorcode) > 0 Then
                            strVendorcode += ", "
                        End If
                        strVendorcode += StrCode
                    Next
                    arrHeader.Add(("Vendor Name: " + strVendorName + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Pending Provision Report", gv, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Pending Provision Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Location or select all.", Me.Text)
            Exit Sub
        End If
        If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vendor or select all.", Me.Text)
            Exit Sub
        End If
        Dim sQuery As String = ""
        If RbAll.IsChecked = True Then
            If RbDetail.IsChecked = True Then
                sQuery += "select Loc_Code +' -('+Location_Desc +')'  as Loc_Code, Location_Desc,TSPL_PROVISION_ENTRY.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,Doc_No ,convert(varchar,Doc_Date,103) as Doc_Date ,convert(decimal(18,2),Amount) as Amount ,TSPL_PROVISION_ENTRY.Vendor_Type ,Ref_Doc_No,TSPL_Primary_Vehicle_Master.Vehicle     from TSPL_PROVISION_ENTRY left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code   = TSPL_PROVISION_ENTRY.Loc_Code left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PROVISION_ENTRY.Vendor_Code"
                sQuery += " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_PROVISION_ENTRY.Route_CODE Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code"
                sQuery += " where 2 = 2 and TSPL_PROVISION_ENTRY.isPosted ='1' "
                If cbVendorType.Text = "Transporter For Product Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type   IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Others" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Secondary Transporter" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "MCC Lease Vendor" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Transporter For Fresh Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Transporter For Bulk Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                    sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_VENDOR_MASTER.Vendor_Code   IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
                End If

                sQuery += " and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) order by TSPL_PROVISION_ENTRY.DOC_DATE"

            
            ElseIf RbSummary.IsChecked = True Then
                sQuery += "select tt.Loc_Code,tt.Vendor_Name, convert(decimal(18,2),sum (tt.Amount )) as Amount,tt.Vendor_Type   from (select TSPL_PROVISION_ENTRY. Loc_Code +' -('+Location_Desc +')'  as Loc_Code, Location_Desc,TSPL_PROVISION_ENTRY.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,Doc_No ,Doc_Date ,Amount ,TSPL_PROVISION_ENTRY.Vendor_Type ,Ref_Doc_No     from TSPL_PROVISION_ENTRY left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code   = TSPL_PROVISION_ENTRY.Loc_Code left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PROVISION_ENTRY.Vendor_Code  where 2=2  and  TSPL_PROVISION_ENTRY.isPosted ='1' "
                If cbVendorType.Text = "Transporter For Product Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type   IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Others" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Secondary Transporter" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "MCC Lease Vendor" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Transporter For Fresh Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Transporter For Bulk Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_LOCATION_MASTER. Location_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_VENDOR_MASTER.Vendor_Code    IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
                End If

                sQuery += " and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103) " 'order by TSPL_PROVISION_ENTRY.DOC_DATE"
                sQuery += " )tt  group by Loc_Code ,Vendor_Name ,Vendor_Type "

                
            End If

        Else
            '=============================================
            If RbDetail.IsChecked = True Then
                sQuery += "select Loc_Code +' -('+Location_Desc +')'  as Loc_Code, Location_Desc,TSPL_PROVISION_ENTRY.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,Doc_No ,Doc_Date ,convert(decimal(18,2),Amount) as Amount ,TSPL_PROVISION_ENTRY.Vendor_Type ,Ref_Doc_No,TSPL_Primary_Vehicle_Master.Vehicle     from TSPL_PROVISION_ENTRY left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code   = TSPL_PROVISION_ENTRY.Loc_Code left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PROVISION_ENTRY.Vendor_Code"
                sQuery += " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_PROVISION_ENTRY.Route_CODE Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code"
                sQuery += " where 2 = 2 and TSPL_PROVISION_ENTRY.isPosted ='1' and TSPL_PROVISION_ENTRY.Status ='No' "
                If cbVendorType.Text = "Transporter For Product Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type   IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Others" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Secondary Transporter" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "
                End If
                If cbVendorType.Text = "MCC Lease Vendor" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Transporter For Fresh Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Transporter For Bulk Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_LOCATION_MASTER. Location_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_VENDOR_MASTER.Vendor_Code  IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
                End If
                sQuery += " and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)  order by TSPL_PROVISION_ENTRY.DOC_DATE"

               
               
            ElseIf RbSummary.IsChecked = True Then
                sQuery += "select tt.Loc_Code,tt.Vendor_Name,convert(decimal(18,2),sum (tt.Amount )) as Amount,tt.Vendor_Type   from (select TSPL_PROVISION_ENTRY. Loc_Code +' -('+Location_Desc +')'  as Loc_Code, Location_Desc,TSPL_PROVISION_ENTRY.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,Doc_No ,Doc_Date ,Amount ,TSPL_PROVISION_ENTRY.Vendor_Type ,Ref_Doc_No     from TSPL_PROVISION_ENTRY left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code   = TSPL_PROVISION_ENTRY.Loc_Code left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PROVISION_ENTRY.Vendor_Code  where 2=2  and  TSPL_PROVISION_ENTRY.isPosted ='1' "
                If cbVendorType.Text = "Transporter For Product Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type   IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Others" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Secondary Transporter" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "MCC Lease Vendor" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Transporter For Fresh Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If cbVendorType.Text = "Transporter For Bulk Sale" Then
                    sQuery += "and TSPL_PROVISION_ENTRY.Vendor_Type    IN ('" + cbVendorType.Text + "') "

                End If
                If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_LOCATION_MASTER. Location_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    sQuery += "and TSPL_VENDOR_MASTER.Vendor_Code    IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
                End If

                sQuery += " and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)  " 'order by TSPL_PROVISION_ENTRY.DOC_DATE "
                sQuery += " )tt  group by Loc_Code ,Vendor_Name ,Vendor_Type "

               

            End If
        End If
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        'Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        If (RbPending.IsChecked And RbSummary.IsChecked) Or (RbAll.IsChecked And RbSummary.IsChecked) = True Then
            gv.Columns("Loc_Code").IsVisible = True
            gv.Columns("Loc_Code").Width = 100
            gv.Columns("Loc_Code").HeaderText = "Location"

            'gv.Columns("Location_Name").IsVisible = False
            'gv.Columns("Location_Name").Width = 100
            'gv.Columns("Location_Name").HeaderText = "Location Name"


            'gv.Columns("Vendor_Code").IsVisible = False
            'gv.Columns("Vendor_Code").Width = 100
            'gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

            gv.Columns("Vendor_Name").IsVisible = True
            gv.Columns("Vendor_Name").Width = 100
            gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

            gv.Columns("Amount").IsVisible = True
            gv.Columns("Amount").Width = 100
            gv.Columns("Amount").HeaderText = "Amount"

            gv.Columns("Vendor_Type").IsVisible = True
            gv.Columns("Vendor_Type").Width = 150
            gv.Columns("Vendor_Type").HeaderText = "Vendor Type"


        ElseIf (RbPending.IsChecked And RbDetail.IsChecked) Or (RbAll.IsChecked And RbDetail.IsChecked) = True Then
            gv.Columns("Loc_Code").IsVisible = True
            gv.Columns("Loc_Code").Width = 100
            gv.Columns("Loc_Code").HeaderText = "Location"

            gv.Columns("Location_Desc").IsVisible = True
            gv.Columns("Location_Desc").Width = 100
            gv.Columns("Location_Desc").HeaderText = "Location Name"

            gv.Columns("Vendor_Code").IsVisible = True
            gv.Columns("Vendor_Code").Width = 100
            gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

            gv.Columns("Vendor_Name").IsVisible = True
            gv.Columns("Vendor_Name").Width = 100
            gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

            gv.Columns("Doc_No").IsVisible = True
            gv.Columns("Doc_No").Width = 100
            gv.Columns("Doc_No").HeaderText = "Provision No."

            gv.Columns("Doc_Date").IsVisible = True
            gv.Columns("Doc_Date").Width = 100
            gv.Columns("Doc_Date").HeaderText = " Provision Date"
            gv.Columns("Doc_Date").FormatString = "{0:d}"

            gv.Columns("Amount").IsVisible = True
            gv.Columns("Amount").Width = 100
            gv.Columns("Amount").HeaderText = "Amount"

            gv.Columns("Vendor_Type").IsVisible = True
            gv.Columns("Vendor_Type").Width = 150
            gv.Columns("Vendor_Type").HeaderText = "Vendor Type"

            gv.Columns("Ref_Doc_No").IsVisible = True
            gv.Columns("Ref_Doc_No").Width = 100
            gv.Columns("Ref_Doc_No").HeaderText = " Document No."

        End If

        If gv.Columns.Contains("Vehicle") Then
            gv.Columns("Vehicle").Width = 100
            gv.Columns("Vehicle").IsVisible = ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gv.GroupDescriptors.Add(New GridGroupByExpression("Loc_Code as Item format ""{0}: {1}"" Group By Loc_Code"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub chkVendorAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        If gv.Rows.Count > 0 Then
            print(EnumExportTo.PDF)
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
        End If
    End Sub
End Class

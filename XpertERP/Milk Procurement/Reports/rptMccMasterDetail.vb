Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class rptMccMasterDetail
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        cmbReportType.Text = "Select"
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        txtMCC.arrValueMember = Nothing
        txtDistrict.arrValueMember = Nothing
        txtBlock.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        txtRevenueVillage.arrValueMember = Nothing
        txtGrampanchayat.arrValueMember = Nothing
        txtPanchayatSamiti.arrValueMember = Nothing
        txtVidhanSabha.arrValueMember = Nothing

        chkMP.Checked = False
        chkMP.Visible = True
        ControlEnableDisable(True)
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        txtMCC.Enabled = isEnable
        txtDistrict.Enabled = isEnable
        txtBlock.Enabled = isEnable
        txtZone.Enabled = isEnable
        txtRevenueVillage.Enabled = isEnable
        txtGrampanchayat.Enabled = isEnable
        txtPanchayatSamiti.Enabled = isEnable
        txtVidhanSabha.Enabled = isEnable
        chkMP.Enabled = isEnable
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            PageSetupReport_ID = MyBase.Form_ID
            If clsCommon.CompairString(cmbReportType.Text, "Select") <> CompairStringResult.Equal Then
                PageSetupReport_ID = PageSetupReport_ID + clsCommon.myCstr(cmbReportType.Text)
            Else
                If chkMP.Checked = True Then
                    PageSetupReport_ID = MyBase.Form_ID + "MP"
                End If
            End If
            TemplateGridview = Gv1
            Dim BaseQry As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim whre As String = ""
            If clsCommon.CompairString(cmbReportType.Text, "VSP") = CompairStringResult.Equal Then
                PageSetupReport_ID = PageSetupReport_ID + IIf(chkActiveStatus.Checked = True, "S", "")
                qry = clsMccMasterDetailReport.getQueryMccMasterDetailVSP(chkActiveStatus.Checked)
            ElseIf clsCommon.CompairString(cmbReportType.Text, "Transporter") = CompairStringResult.Equal Then
                PageSetupReport_ID = PageSetupReport_ID + IIf(chkActiveStatus.Checked = True, "S", "")
                qry = clsMccMasterDetailReport.getQueryMccMasterDetailTransporter(chkActiveStatus.Checked)
            ElseIf clsCommon.CompairString(cmbReportType.Text, "Employee") = CompairStringResult.Equal Then
                qry = clsMccMasterDetailReport.getQueryMccMasterDetailEmployee()
            Else
                If txtDistrict.arrValueMember IsNot Nothing AndAlso txtDistrict.arrValueMember.Count > 0 Then
                    If chkMP.Checked = False Then
                        whre += " and  TSPL_VENDOR_MASTER.DISTRICT_Code in (" + clsCommon.GetMulcallString(txtDistrict.arrValueMember) + ") "
                    Else
                        whre += " and  TSPL_MP_MASTER.DISTRICT_Code in (" + clsCommon.GetMulcallString(txtDistrict.arrValueMember) + ") "
                    End If
                End If
                If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                    If chkMP.Checked = False Then
                        whre += " and  TSPL_VENDOR_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
                    Else
                        whre += " and  TSPL_MP_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
                    End If
                End If

                If txtBlock.arrValueMember IsNot Nothing AndAlso txtBlock.arrValueMember.Count > 0 Then
                    If chkMP.Checked = False Then
                        whre += " and  TSPL_VENDOR_MASTER.BLOCK_CODE in (" + clsCommon.GetMulcallString(txtBlock.arrValueMember) + ") "
                    Else
                        whre += " and  TSPL_MP_MASTER.BLOCK_CODE in (" + clsCommon.GetMulcallString(txtBlock.arrValueMember) + ") "
                    End If
                End If
                If txtRevenueVillage.arrValueMember IsNot Nothing AndAlso txtRevenueVillage.arrValueMember.Count > 0 Then
                    If chkMP.Checked = False Then
                        whre += " and  TSPL_VENDOR_MASTER.REVENUE_VILLAGE_CODE in (" + clsCommon.GetMulcallString(txtRevenueVillage.arrValueMember) + ") "
                    Else
                        whre += " and  TSPL_MP_MASTER.REVENUE_VILLAGE_CODE in (" + clsCommon.GetMulcallString(txtRevenueVillage.arrValueMember) + ") "
                    End If
                End If

                If txtGrampanchayat.arrValueMember IsNot Nothing AndAlso txtGrampanchayat.arrValueMember.Count > 0 Then
                    If chkMP.Checked = False Then
                        whre += " and  TSPL_VENDOR_MASTER.GRAMPANCHAYAT_CODE in (" + clsCommon.GetMulcallString(txtGrampanchayat.arrValueMember) + ") "
                    Else
                        whre += " and  TSPL_MP_MASTER.GRAMPANCHAYAT_CODE in (" + clsCommon.GetMulcallString(txtGrampanchayat.arrValueMember) + ") "
                    End If
                End If

                If txtPanchayatSamiti.arrValueMember IsNot Nothing AndAlso txtPanchayatSamiti.arrValueMember.Count > 0 Then
                    If chkMP.Checked = False Then
                        whre += " and  TSPL_VENDOR_MASTER.PANCHAYAT_SAMITI_CODE in (" + clsCommon.GetMulcallString(txtPanchayatSamiti.arrValueMember) + ") "
                    Else
                        whre += " and  TSPL_MP_MASTER.PANCHAYAT_SAMITI_CODE in (" + clsCommon.GetMulcallString(txtPanchayatSamiti.arrValueMember) + ") "
                    End If
                End If

                If txtVidhanSabha.arrValueMember IsNot Nothing AndAlso txtVidhanSabha.arrValueMember.Count > 0 Then
                    If chkMP.Checked = False Then
                        whre += " and  TSPL_VENDOR_MASTER.VIDHAN_SABHA_CODE in (" + clsCommon.GetMulcallString(txtVidhanSabha.arrValueMember) + ") "
                    Else
                        whre += " and  TSPL_MP_MASTER.VIDHAN_SABHA_CODE in (" + clsCommon.GetMulcallString(txtVidhanSabha.arrValueMember) + ") "
                    End If
                End If

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    whre = " and TSPL_MCC_MASTER.MCC_CODE in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                End If

                If chkMP.Checked = True Then
                    qry = " select TSPL_MCC_MASTER.MCC_CODE as [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC Name], VSP_Code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name] , TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Name], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code] ,tspl_mp_master.MP_Code as [MP Code], tspl_mp_master.MP_Name as [MP Name],tspl_mp_master.MP_CODE_VLC_UPLOADER as [MP Uploader Code],TSPL_MP_MASTER.Telphone as [Phone] , TSPL_MP_MASTER.Fax as [Aadhar No],tspl_mp_master.Jan_Aadhar_No as [Jan Aadhar No],case when isnull (TSPL_MP_MASTER.Jan_Aadhar_No_Verified,'')>0 then 'Y' else 'N' end as [Jan Aadhar Verified],TSPL_MP_MASTER.JA_aadhar, TSPL_MP_MASTER.JA_acc, TSPL_MP_MASTER.JA_age, TSPL_MP_MASTER.JA_bankBranch, TSPL_MP_MASTER.JA_bankName, TSPL_MP_MASTER.JA_ifsc, TSPL_MP_MASTER.JA_caste, TSPL_MP_MASTER.JA_categoryDescEng, TSPL_MP_MASTER.JA_disability, TSPL_MP_MASTER.JA_disabilityPercentage, TSPL_MP_MASTER.JA_disabilityType, TSPL_MP_MASTER.JA_dlNo, TSPL_MP_MASTER.JA_dob, TSPL_MP_MASTER.JA_eid, TSPL_MP_MASTER.JA_email, TSPL_MP_MASTER.JA_enrId, TSPL_MP_MASTER.JA_fnameEng, TSPL_MP_MASTER.JA_fnameHnd, TSPL_MP_MASTER.JA_gender, TSPL_MP_MASTER.JA_income, TSPL_MP_MASTER.JA_isorphan, TSPL_MP_MASTER.JA_isStateGovtEmp, TSPL_MP_MASTER.JA_jan_mid, TSPL_MP_MASTER.JA_janaadhaarId, TSPL_MP_MASTER.JA_maritalStatus, TSPL_MP_MASTER.JA_micr, TSPL_MP_MASTER.JA_mnameEng, TSPL_MP_MASTER.JA_mnameHnd, TSPL_MP_MASTER.JA_mobile, TSPL_MP_MASTER.JA_nameEng, TSPL_MP_MASTER.JA_nameHnd, TSPL_MP_MASTER.JA_occupation, TSPL_MP_MASTER.JA_relationTyp, TSPL_MP_MASTER.JA_panNo, TSPL_MP_MASTER.JA_passport, TSPL_MP_MASTER.JA_qualification, TSPL_MP_MASTER.JA_rghs_no, TSPL_MP_MASTER.JA_snameEng, TSPL_MP_MASTER.JA_snameHnd, TSPL_MP_MASTER.JA_voterId,tspl_mp_master.BankName as [Bank Code],case when len (isnull (TSPL_BANK_MASTER.DESCRIPTION,'')) > 0 then TSPL_BANK_MASTER.DESCRIPTION else tspl_mp_master.BankName end as [Bank Name],tspl_mp_master.IFCICode as [IFSC Code],tspl_mp_master.BankBranch as [Branch Name],tspl_mp_master.AccountNO as [Account No],TSPL_MP_MASTER.DISTRICT_Code as [District Code],TSPL_DISTRICT_MASTER.Name as [District Name],TSPL_MP_MASTER.Zone_Code as [Zone Code], TSPL_ZONE_MASTER.Description as [Zone Name],TSPL_MP_MASTER.BLOCK_CODE as [Block Code],TSPL_BLOCK_MASTER.BLOCK_NAME as [Block Name] ,TSPL_MP_MASTER.REVENUE_VILLAGE_CODE as [Revenue Village Code],TSPL_REVENUE_VILLAGE_MASTER.REVENUE_VILLAGE_NAME as [Revenue Village Name],TSPL_MP_MASTER.GRAMPANCHAYAT_CODE as [Grampanchayat Code],TSPL_GRAMPANCHAYAT_MASTER.GRAMPANCHAYAT_NAME as [Grampanchayat Name],TSPL_MP_MASTER.PANCHAYAT_SAMITI_CODE as [Panchayat Samiti Code],TSPL_PANCHAYAT_SAMITI_MASTER.PANCHAYAT_SAMITI_NAME as [Panchayat Samiti Name],TSPL_MP_MASTER.VIDHAN_SABHA_CODE as [Vidhan Sabha Code], TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_NAME as [Vidhan Sabha Name],( case when isOwnBMC= 1 then 'Y' else 'N' end )as [isOwnBMC(Y/N)],(CASE WHEN isOwnBMC =1 THEN TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ELSE '' END) AS [OWN BMC],(case when  TSPL_VLC_MASTER_HEAD.isOwnBMC =1 then  TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader else '' end)as [MCC Uploader Code],case when Apply_Cow_Price =1 then 'Y' else 'N' end as [Apply Cow Price(Y/N)],TSPL_VENDOR_MASTER.Registered_PDCS_CLUSTER as [DCS Type],(case when TSPL_VENDOR_MASTER.Registered_PDCS_CLUSTER = 'registered'  then TSPL_VENDOR_MASTER.RegistrationNo else '' end )as  [Reg No],  (case when TSPL_VENDOR_MASTER.Registered_PDCS_CLUSTER = 'registered'  then TSPL_VENDOR_MASTER.RegistrationDate else '' end )as  [Reg Date],TSPL_VENDOR_MASTER.Gender as [Gender] "
                Else
                    qry = " select isnull(TSPL_MCC_MASTER.plant_code,'') as [Plant],isnull(tspl_Plant_Code.Location_Desc ,'') as [Plant Name],isnull(TSPL_LOCATION_MASTER.Loc_Segment_Code,'') as [Loc Segment Code],TSPL_MCC_MASTER.MCC_CODE as [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC Name],isnull(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,'') as [MCC Uploader Code],tspl_mcc_route_master.Route_Code as [Route Code],tspl_mcc_route_master.Route_Name as [Route Name],tspl_mcc_route_master.Vehicle_Code as [Vehicle No]" &
                  " ,Transporter.Vendor_Code as [Transporter Code],Transporter.Vendor_Name as [Transporter Name],TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Name] " &
                  " ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code],VSP_Code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name] ,(case when TSPL_VLC_MASTER_HEAD.isOwnBMC =1 then 'Y' else 'N' end) as [isOwnBMC(Y/N)],(CASE WHEN TSPL_VLC_MASTER_HEAD.isOwnBMC = 1 THEN TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ELSE '' END) AS [OWN BMC],(case when  TSPL_VLC_MASTER_HEAD.isOwnBMC =1 then  TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader else '' end)as [MCC Uploader Code],case when TSPL_VLC_MASTER_HEAD.Apply_Cow_Price =1 then 'Y' else 'N' end as [Apply Cow Price(Y/N)]  " &
                  " ,TSPL_VENDOR_MASTER.Bank_Code as [Bank Code],TSPL_VENDOR_MASTER.Bank_Name as [Bank Name],TSPL_VENDOR_MASTER.IFSC_Code as [IFSC Code],TSPL_VENDOR_MASTER.Branch_Name as [Branch Name],TSPL_VENDOR_MASTER.Account_No as [Account No],TSPL_VENDOR_MASTER.Gender as [Gender],TSPL_VENDOR_MASTER.Registered_PDCS_CLUSTER as [DCS Type],(case when TSPL_VENDOR_MASTER.Registered_PDCS_CLUSTER = 'registered'  then TSPL_VENDOR_MASTER.RegistrationNo else '' end )as  [Reg No], ( case when TSPL_VENDOR_MASTER.Registered_PDCS_CLUSTER = 'registered'  then TSPL_VENDOR_MASTER.RegistrationDate else '' end) as  [Reg Date],TSPL_Primary_Vehicle_Master.Vehicle, TSPL_VENDOR_MASTER.DISTRICT_Code as [District Code],TSPL_DISTRICT_MASTER.Name as [District Name]
 ,TSPL_VENDOR_MASTER.Zone_Code as [Zone Code], TSPL_ZONE_MASTER.Description as [Zone Name],TSPL_VENDOR_MASTER.BLOCK_CODE as [Block Code],TSPL_BLOCK_MASTER.BLOCK_NAME as [Block Name] ,TSPL_VENDOR_MASTER.REVENUE_VILLAGE_CODE as [Revenue Village Code],TSPL_REVENUE_VILLAGE_MASTER.REVENUE_VILLAGE_NAME as [Revenue Village Name],TSPL_VENDOR_MASTER.GRAMPANCHAYAT_CODE as [Grampanchayat Code],TSPL_GRAMPANCHAYAT_MASTER.GRAMPANCHAYAT_NAME as [Grampanchayat Name],TSPL_VENDOR_MASTER.PANCHAYAT_SAMITI_CODE as [Panchayat Samiti Code],TSPL_PANCHAYAT_SAMITI_MASTER.PANCHAYAT_SAMITI_NAME as [Panchayat Samiti Name],TSPL_VENDOR_MASTER.VIDHAN_SABHA_CODE as [Vidhan Sabha Code], TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_NAME as [Vidhan Sabha Name] "
                End If
                qry += " from TSPL_VLC_MASTER_HEAD " &
                  " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code and TSPL_VENDOR_MASTER.Form_Type='VSP' " &
                  " left join (select distinct Route_CODE , VLC_CODE from TSPL_MCC_ROUTE_VLC_MAPPING) as TSPL_MCC_ROUTE_VLC_MAPPING on TSPL_MCC_ROUTE_VLC_MAPPING.VLC_CODE=TSPL_VLC_MASTER_HEAD.VLC_Code " &
                  " left join tspl_mcc_route_master on tspl_mcc_route_master.Route_Code=TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE " &
                  " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=tspl_mcc_route_master.Vehicle_Code " &
                  " left join TSPL_VENDOR_MASTER AS Transporter on Transporter.Vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code and Transporter.Form_Type='PTM' " &
                  " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC " &
                  " left outer join TSPL_LOCATION_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_LOCATION_MASTER.Location_Code " &
                  " left outer join TSPL_LOCATION_MASTER as  tspl_Plant_Code on tspl_Plant_Code.Location_Code  = TSPL_MCC_MASTER.plant_code "

                If chkMP.Checked = True Then
                    qry += "  left outer join tspl_mp_master on tspl_mp_master.VLC_Code = TSPL_VLC_MASTER_HEAD.VLC_Code
                              left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE = tspl_mp_master.BankName
                              left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_MP_MASTER.Zone_Code
                              left outer join TSPL_BLOCK_MASTER on TSPL_BLOCK_MASTER.BLOCK_CODE = TSPL_MP_MASTER.BLOCK_CODE
                              left outer join TSPL_REVENUE_VILLAGE_MASTER on TSPL_REVENUE_VILLAGE_MASTER.REVENUE_VILLAGE_CODE = TSPL_MP_MASTER.REVENUE_VILLAGE_CODE
                              left outer join TSPL_GRAMPANCHAYAT_MASTER on TSPL_GRAMPANCHAYAT_MASTER.GRAMPANCHAYAT_CODE = TSPL_MP_MASTER.GRAMPANCHAYAT_CODE
                              left outer join TSPL_PANCHAYAT_SAMITI_MASTER on TSPL_PANCHAYAT_SAMITI_MASTER.PANCHAYAT_SAMITI_CODE = TSPL_MP_MASTER.PANCHAYAT_SAMITI_CODE
                              left outer join TSPL_VIDHAN_SABHA_MASTER on TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_CODE = TSPL_MP_MASTER.VIDHAN_SABHA_CODE
                              left outer join TSPL_DISTRICT_MASTER on TSPL_DISTRICT_MASTER.Code = TSPL_MP_MASTER.DISTRICT_Code  
                              
                           "
                Else
                    qry += "  left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.Zone_Code
                              left outer join TSPL_BLOCK_MASTER on TSPL_BLOCK_MASTER.BLOCK_CODE = TSPL_VENDOR_MASTER.BLOCK_CODE
                              left outer join TSPL_REVENUE_VILLAGE_MASTER on TSPL_REVENUE_VILLAGE_MASTER.REVENUE_VILLAGE_CODE = TSPL_VENDOR_MASTER.REVENUE_VILLAGE_CODE
                              left outer join TSPL_GRAMPANCHAYAT_MASTER on TSPL_GRAMPANCHAYAT_MASTER.GRAMPANCHAYAT_CODE = TSPL_VENDOR_MASTER.GRAMPANCHAYAT_CODE
                              left outer join TSPL_PANCHAYAT_SAMITI_MASTER on TSPL_PANCHAYAT_SAMITI_MASTER.PANCHAYAT_SAMITI_CODE = TSPL_VENDOR_MASTER.PANCHAYAT_SAMITI_CODE
                              left outer join TSPL_VIDHAN_SABHA_MASTER on TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_CODE = TSPL_VENDOR_MASTER.VIDHAN_SABHA_CODE
                              left outer join TSPL_DISTRICT_MASTER on TSPL_DISTRICT_MASTER.Code = TSPL_VENDOR_MASTER.DISTRICT_Code"
                    'left outer join TSPL_VLC_MASTER_HEAD on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCCOwnBMC"
                End If
                qry += " where  2=2  " + whre + " " &
                        " order by TSPL_MCC_MASTER.MCC_NAME  "

            End If



            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If Gv1.Columns.Contains("Vehicle") Then
                Gv1.Columns("Vehicle").IsVisible = ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster
            End If

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).BestFit()
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMccMasterDetail & "'"))
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMccMasterDetail & "'"))

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub rptMccMasterDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        Reset()
    End Sub


    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = " select TSPL_MCC_MASTER.MCC_Code as Code , TSPL_MCC_MASTER.MCC_NAME as Name from TSPL_MCC_MASTER "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelMCC@MCCMSTDETRPT", qry, "Code", "Code", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub cmbReportType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbReportType.SelectedValueChanged

    End Sub

    Private Sub cmbReportType_SelectedIndexChanged(sender As Object, e As Data.PositionChangedEventArgs) Handles cmbReportType.SelectedIndexChanged
        Try
            If clsCommon.CompairString(cmbReportType.Text, "Select") = CompairStringResult.Equal Then
                chkMP.Visible = True
            Else
                chkMP.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtDistrict__My_Click(sender As Object, e As EventArgs) Handles txtDistrict._My_Click
        Dim qry As String = " select TSPL_DISTRICT_MASTER.Code as Code , TSPL_DISTRICT_MASTER.Name as Name from TSPL_DISTRICT_MASTER "
        txtDistrict.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelDistrict@MCCMSTDETRPT", qry, "Code", "Code", txtDistrict.arrValueMember, txtDistrict.arrDispalyMember)
    End Sub

    Private Sub txtBlock__My_Click(sender As Object, e As EventArgs) Handles txtBlock._My_Click
        Dim qry As String = " select TSPL_BLOCK_MASTER.BLOCK_CODE as Code , TSPL_BLOCK_MASTER.BLOCK_NAME as Name from TSPL_BLOCK_MASTER "
        txtBlock.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelBlock@MCCMSTDETRPT", qry, "Code", "Code", txtBlock.arrValueMember, txtBlock.arrDispalyMember)
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Dim qry As String = " select TSPL_ZONE_MASTER.Zone_Code as Code , TSPL_ZONE_MASTER.Description as Name from TSPL_ZONE_MASTER "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelZone@MCCMSTDETRPT", qry, "Code", "Code", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub txtRevenueVillage__My_Click(sender As Object, e As EventArgs) Handles txtRevenueVillage._My_Click
        Dim qry As String = " select TSPL_REVENUE_VILLAGE_MASTER.REVENUE_VILLAGE_CODE as Code , TSPL_REVENUE_VILLAGE_MASTER.REVENUE_VILLAGE_NAME as Name from TSPL_REVENUE_VILLAGE_MASTER "
        txtRevenueVillage.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelRevenueVillage@MCCMSTDETRPT", qry, "Code", "Code", txtRevenueVillage.arrValueMember, txtRevenueVillage.arrDispalyMember)
    End Sub

    Private Sub txtGrampanchayat__My_Click(sender As Object, e As EventArgs) Handles txtGrampanchayat._My_Click
        Dim qry As String = " select TSPL_GRAMPANCHAYAT_MASTER.GRAMPANCHAYAT_CODE as Code , TSPL_GRAMPANCHAYAT_MASTER.GRAMPANCHAYAT_NAME as Name from TSPL_GRAMPANCHAYAT_MASTER "
        txtGrampanchayat.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelGrampanchayat@MCCMSTDETRPT", qry, "Code", "Code", txtGrampanchayat.arrValueMember, txtGrampanchayat.arrDispalyMember)
    End Sub

    Private Sub txtPanchayatSamiti__My_Click(sender As Object, e As EventArgs) Handles txtPanchayatSamiti._My_Click
        Dim qry As String = " select TSPL_PANCHAYAT_SAMITI_MASTER.PANCHAYAT_SAMITI_CODE as Code , TSPL_PANCHAYAT_SAMITI_MASTER.PANCHAYAT_SAMITI_NAME as Name from TSPL_PANCHAYAT_SAMITI_MASTER "
        txtPanchayatSamiti.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelPanchayatSamiti@MCCMSTDETRPT", qry, "Code", "Code", txtPanchayatSamiti.arrValueMember, txtPanchayatSamiti.arrDispalyMember)
    End Sub

    Private Sub txtVidhanSabha__My_Click(sender As Object, e As EventArgs) Handles txtVidhanSabha._My_Click
        Dim qry As String = " select TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_CODE as Code , TSPL_VIDHAN_SABHA_MASTER.VIDHAN_SABHA_NAME as Name from TSPL_VIDHAN_SABHA_MASTER "
        txtVidhanSabha.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelVidhanSabha@MCCMSTDETRPT", qry, "Code", "Code", txtVidhanSabha.arrValueMember, txtVidhanSabha.arrDispalyMember)
    End Sub
End Class

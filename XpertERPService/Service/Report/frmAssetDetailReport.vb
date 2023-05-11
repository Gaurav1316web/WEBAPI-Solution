'------------------Made By Monika----------------07/05/2014---------------------
'' updation by richA AGARWAL  against ticket no BM00000004993
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports Telerik.WinControls
Imports XpertERPEngine

Public Class FrmAssetDetailReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt1 As DataTable = Nothing
#End Region

    Private Sub rdalloutlet_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdalloutlet.ToggleStateChanged, rdselectoutlet.ToggleStateChanged
        cbgCustomer.Enabled = rdselectoutlet.IsChecked
    End Sub

    Private Sub rdallasset_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdallasset.ToggleStateChanged, rdselectasset.ToggleStateChanged
        cbgasset.Enabled = rdselectasset.IsChecked
    End Sub

    Sub LoadOutlet()
        Dim qry As String = "select distinct TSPL_VISI_MASTER.Customer_id as [Outlet Code] ,tspl_customer_master.Customer_Name as [Outlet Description], Phone1 as [Phone No],Coalesce(Add1,'') + coalesce(Add2,'') + coalesce( add3,'')  as [Address]," _
                & " TSPL_CUSTOMER_MASTER.city_code as [City],region_code as [Region],State,Tin_No as [Tin No] from tspl_customer_master Left join tspl_City_Master on tspl_customer_master.city_Code=tspl_City_Master.city_Code left outer join TSPL_VISI_MASTER on TSPL_CUSTOMER_MASTER .cust_code =TSPL_VISI_MASTER.customer_id where tspl_visi_master.customer_id<>''"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.DataSource = dt

        cbgCustomer.DisplayMember = "Outlet Description" 'cbgasset
        cbgCustomer.ValueMember = "Outlet Code"
    End Sub

    Sub LoadAsset()
        Dim qry As String = "select distinct tspl_visi_master.asset_no as [Asset Code],tspl_item_master.item_desc as [Asset Description],VisiMake as [Make],Visi_Size as [Size],Asset_Type as [Type],Model_No as [Model] from tspl_visi_master left outer join tspl_item_master on tspl_item_master.item_code=tspl_visi_master.asset_no"
        cbgasset.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgasset.DisplayMember = "Asset Description"
        cbgasset.ValueMember = "Asset Code"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAssetDetailReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Sub Reset()
        rdallasset.IsChecked = True
        rdalloutlet.IsChecked = True


        GV.DataSource = Nothing
        GV.Rows.Clear()
        GV.Columns.Clear()
        Try
            cbgCustomer.CheckedValue = Nothing
            cbgasset.CheckedValue = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmAssetDetailReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        LoadOutlet()
        LoadAsset()
    End Sub

    Sub PrintData(ByVal Isprint As Exporter)
        Try

            GV.DataSource = Nothing
            GV.Rows.Clear()
            GV.Columns.Clear()

            If rdselectasset.IsChecked AndAlso cbgasset.CheckedValue.Count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Asset", Me.Text)
                Return
            End If

            If rdselectoutlet.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Outlet", Me.Text)
                Return
            End If

            Dim outletcode As String = ""
            Dim assetcode As String = ""
            Dim qry As String = ""

            'If rdselectoutlet.IsChecked Then
            '    outletcode = " and final.outletcode in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            'End If

            'If rdselectasset.IsChecked Then
            '    assetcode = " and final.item_code in (" + clsCommon.GetMulcallString(cbgasset.CheckedValue) + ")"
            'End If

            If rdselectoutlet.IsChecked Then
                outletcode = " and XX.[Outlet Code] in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If

            If rdselectasset.IsChecked Then
                assetcode = " and XX.Asset_Id in (" + clsCommon.GetMulcallString(cbgasset.CheckedValue) + ")"
            End If

            ' ''qry += "select ROW_NUMBER() over (order by final.item_code) as SNO,final.* from ("
            ' ''qry += "select  distinct asm.region_name as region,asm.STATE_NAME,asm.City_Name as town,asm.emp_name as [zm/asm],xxx.Emp_Name as service_ex,xxx1.Vendor_Name as franchisee,'Retail' as [retail/vending],xxx2.Cust_Code as outletcode,xxx2.Customer_Name as outlet,xxx2.City_Name as location, xxx2.State,xxx2.address, [Phone No],[Tin No],[Lst No],xxx2.Contact_Person_Name,xxx2.Contact_Person_Phone,xxx3.FOC_remarks,xxx3.CHEQUE_NO,xxx3.Amount,xxx3.year1,TSPL_VISI_MASTER.Asset_No as Item_Code,TSPL_ITEM_MASTER.Item_Desc,aaa.visimake,aaa.Visi_Size,aaa.model_no,TSPL_VISI_MASTER.Tag_No,TSPL_VISI_MASTER.Serial_No,xxx5.RGP_No as invoice_no,xxx5.rgp_date as invoice_date,xxx5.warranty_dt,TSPL_ASSET_AGREEMENT_DETAILS.AGREEMENT_NO,''as delivery_no,'' as delivery_dt,convert(date,TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Pullout_Date,103) as pullout_dt,TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_type as pullout_remarks,abc.Customer_Name as shifted_to,abc.address as shifted_add,abc.Contact_Person_Name as shift_person,abc.Contact_Person_Phone as shifted_phn_no,abc.CHEQUE_NO as shifted_chq_no,convert(date,abc.CHEQUE_DATE,103) as shifted_chq_dt,abc.Amount as shifted_amt,abc.AGREEMENT_NO as shifted_agreement_no,abc.delivery_no as shifted_dlvry,abc.delivery_dt as shifted_dlvry_dt"
            ' ''qry += " from TSPL_VISI_MASTER left outer join (select distinct TSPL_COMPLAINT_DETAIL.item_code,TSPL_COMPLAINT_DETAIL.serial_no,TSPL_COMPLAINT_DETAIL.executive_code,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_CITY_MASTER.City_Name,TSPL_STATE_MASTER.STATE_NAME from TSPL_COMPLAINT_DETAIL left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_COMPLAINT_DETAIL.executive_code left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_EMPLOYEE_MASTER.PERMA_CITY_CODE left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_EMPLOYEE_MASTER.PERMA_STATE_CODE) xxx on xxx.item_code=TSPL_VISI_MASTER.Asset_No and xxx.serial_no=TSPL_VISI_MASTER.Serial_No"
            ' ''qry += " left outer join (select distinct TSPL_ITEM_FRANCHISE_MAPPING.Item_Code,TSPL_ITEM_FRANCHISE_MAPPING.vendor_code,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_ITEM_FRANCHISE_MAPPING left outer join TSPL_VENDOR_MASTER on TSPL_ITEM_FRANCHISE_MAPPING.vendor_code=TSPL_VENDOR_MASTER.Vendor_Code)xxx1 on xxx1.Item_Code=TSPL_VISI_MASTER.Asset_No"
            ' ''qry += " left outer join (select distinct TSPL_COMPLAINT_DETAIL.item_code,TSPL_COMPLAINT_DETAIL.serial_no,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,(TSPL_CUSTOMER_MASTER.Add1+' '+TSPL_CUSTOMER_MASTER.Add2+' '+TSPL_CUSTOMER_MASTER.Add3) as address,TSPL_CITY_MASTER.City_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone, Phone1 as [Phone No], TSPL_CUSTOMER_MASTER.city_code as [City],region_code as [Region],State,Tin_No as [Tin No],Lst_No as [Lst No] from TSPL_COMPLAINT_DETAIL left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_COMPLAINT_DETAIL.cust_code left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code)xxx2 on xxx2.item_code=TSPL_VISI_MASTER.Asset_No and TSPL_VISI_MASTER.Serial_No=xxx2.serial_no"
            ' ''qry += " left outer join (select distinct TSPL_RGP_DETAIL.Item_Code,TSPL_RGP_DETAIL.serial_no,(case when TSPL_RGP_DETAIL.FOC='no' then 'Security' else 'FOC' end) as FOC_remarks,TSPL_RGP_DETAIL.CHEQUE_NO,TSPL_RGP_DETAIL.Amount,year(TSPL_RGP_DETAIL.CHEQUE_DATE) as year1 from TSPL_RGP_DETAIL)xxx3 on xxx3.Item_Code=TSPL_VISI_MASTER.Asset_No and xxx3.serial_no=TSPL_VISI_MASTER.serial_no"
            ' ''qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_VISI_MASTER.Asset_No"
            ' ''qry += " left outer join (select distinct TSPL_VISI_MASTER.Asset_No,TSPL_VISI_MASTER.Serial_No,TSPL_VISI_MASTER.Visi_Id,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as visimake,a.DESCRIPTION as Visi_Size,b.CODE  as model_no from TSPL_VISI_MASTER left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_VISI_MASTER.VisiMake left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES a on a.CODE=TSPL_VISI_MASTER.Visi_Size left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES b on b.CODE=TSPL_VISI_MASTER.Model_No) as aaa on aaa.Asset_No=TSPL_VISI_MASTER.Asset_No and aaa.Serial_No=TSPL_VISI_MASTER.Serial_No and aaa.Visi_Id=TSPL_VISI_MASTER.Visi_Id"
            ' ''qry += " left outer join (select TSPL_VISI_MASTER.Visi_Id,TSPL_RGP_DETAIL.CHEQUE_NO,TSPL_RGP_DETAIL.CHEQUE_DATE,TSPL_RGP_DETAIL.Amount,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone,(TSPL_CUSTOMER_MASTER.Add1+' '+TSPL_CUSTOMER_MASTER.Add2+' '+TSPL_CUSTOMER_MASTER.Add3) as address,TSPL_ASSET_AGREEMENT_DETAILS.AGREEMENT_NO,'' as delivery_no,'' as delivery_dt from TSPL_VISI_MASTER left outer join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.Item_Code=TSPL_VISI_MASTER.Asset_No and TSPL_RGP_DETAIL.serial_no=TSPL_VISI_MASTER.Serial_No left outer join TSPL_ASSET_INSTALL_PULLOUT_NEW on TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id=TSPL_VISI_MASTER.Visi_Id left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ASSET_INSTALL_PULLOUT_NEW.Install_Customer_Id left outer join TSPL_ASSET_AGREEMENT_DETAILS on TSPL_ASSET_AGREEMENT_DETAILS.ASSET_ID=TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id  where TSPL_ASSET_INSTALL_PULLOUT_NEW.Trans_Type='both') abc on abc.Visi_Id=TSPL_VISI_MASTER.Visi_Id"
            ' ''qry += " left outer join (select distinct TSPL_RGP_DETAIL.RGP_No,convert(date,TSPL_RGP_HEAD.RGP_Date,103) as rgp_date,TSPL_RGP_DETAIL.Item_Code,TSPL_RGP_DETAIL.serial_no,DATEADD(day,TSPL_ITEM_MASTER.Warranty_Period,convert(date,TSPL_RGP_HEAD.RGP_Date,103)) as warranty_dt from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_RGP_DETAIL.Item_Code)xxx5 on xxx5.Item_Code=TSPL_VISI_MASTER.Asset_No and xxx5.serial_no=TSPL_VISI_MASTER.Serial_No"
            ' ''qry += " left outer join TSPL_ASSET_AGREEMENT_DETAILS on TSPL_ASSET_AGREEMENT_DETAILS.ASSET_ID=TSPL_VISI_MASTER.Visi_Id"
            ' ''qry += " left outer join TSPL_ASSET_INSTALL_PULLOUT_NEW on TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id=TSPL_VISI_MASTER.Visi_Id and TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_type='PulledOut'"
            ' ''qry += " left outer join (select distinct abb.* from (select ROW_NUMBER() over(partition by TSPL_COMPLAINT_DETAIL.cust_code order by TSPL_COMPLAINT_DETAIL.cust_code) as sno,TSPL_COMPLAINT_DETAIL.item_code,TSPL_COMPLAINT_DETAIL.serial_no,TSPL_COMPLAINT_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.City_Code,TSPL_CUSTOMER_MASTER.State,TSPL_emptype_ASMZM_details.emp_code,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_REGION_MASTER.REGION_NAME,TSPL_STATE_MASTER.STATE_NAME,TSPL_CITY_MASTER.city_name from TSPL_COMPLAINT_DETAIL left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_COMPLAINT_DETAIL.cust_code left outer join TSPL_emptype_ASMZM_details on TSPL_emptype_ASMZM_details.city_code=TSPL_CUSTOMER_MASTER.City_Code and TSPL_emptype_ASMZM_details.state_code=TSPL_CUSTOMER_MASTER.State left outer join TSPL_EMPLOYEE_MASTER on TSPL_emptype_ASMZM_details.emp_code=TSPL_EMPLOYEE_MASTER.EMP_CODE left outer join TSPL_REGION_MASTER on TSPL_REGION_MASTER.REGION_CODE=TSPL_emptype_ASMZM_details.region_code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_emptype_ASMZM_details.state_code and TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.city_code=TSPL_CUSTOMER_MASTER.city_code and TSPL_City_master.city_code=TSPL_emptype_ASMZM_details.city_code)abb where abb.sno=1)asm on asm.item_code=tspl_visi_master.asset_no and asm.serial_no=tspl_visi_master.serial_no and asm.cust_code=tspl_visi_master.customer_id and asm.cust_code=xxx2.cust_code"
            ' ''qry += ")final where 1=1 " + outletcode + " " + assetcode + ""
            'qry = "Select ROW_NUMBER() over (order by XX.Asset_Id) as SNO, * from (Select TSPL_CUSTOMER_MASTER.Zone_Code, ROW_NUMBER() OVER (Partition By TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id Order By TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Installation_Date Desc) as RowNO,TSPL_CUSTOMER_MASTER.State ,TSPL_CUSTOMER_MASTER.City_Code ,TSPL_CUSTOMER_MASTER.Country,TSPL_CUSTOMER_MASTER.Service_Dealer_Code ,TSPL_CUSTOMER_MASTER.Customer_Class,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,(TSPL_CUSTOMER_MASTER.Add1+' '+TSPL_CUSTOMER_MASTER.Add2+' '+TSPL_CUSTOMER_MASTER.Add3) as address ,TSPL_CUSTOMER_MASTER.Phone1 ,TSPL_CUSTOMER_MASTER.Tin_No ,TSPL_CUSTOMER_MASTER.Lst_No ,TSPL_CUSTOMER_MASTER.Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone,case when TSPL_ASSET_INSTALL_PULLOUT_NEW.FOC_sec='NO' then 'Security' else 'FOC' end as FOC_remarks,TSPL_ASSET_INSTALL_PULLOUT_NEW.Cheque_No_Sec ,TSPL_ASSET_INSTALL_PULLOUT_NEW.Sec_Amount ,DATEPART(YEAR ,TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Installation_Date) AS 'Year', Asset_Installation_Date,TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id,TSPL_ITEM_MASTER.Item_Desc as [Asset Description],TSPL_VISI_MASTER.VisiMake as [M/C Make],TSPL_VISI_MASTER.Visi_Size as [DF Capacity],TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_type as [Type of M/C],TSPL_VISI_MASTER.Serial_No as [M/C SR.No.] ,TSPL_VISI_MASTER.Tag_No as [M/C Tag No.] from TSPL_VISI_MASTER left outer join TSPL_CUSTOMER_MASTER on TSPL_VISI_MASTER.Customer_Id=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_ASSET_INSTALL_PULLOUT_NEW ON TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id =TSPL_VISI_MASTER.Visi_Id lEFT oUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ASSET_INSTALL_PULLOUT_NEW.item_code) XX Where XX.RowNO=1 " + outletcode + " " + assetcode + " order by XX.Asset_Id "
            qry = "Select ROW_NUMBER() over (order by XX.Asset_Id) as SNO, * from (Select TSPL_CUSTOMER_MASTER.Zone_Code as Zone, ROW_NUMBER() OVER (Partition By TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id Order By TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Installation_Date Desc) as RowNO,TSPL_CUSTOMER_MASTER.State ,TSPL_CUSTOMER_MASTER.City_Code as City ,TSPL_CUSTOMER_MASTER.Country,TSPL_CUSTOMER_MASTER.Service_Dealer_Code as [Service Executive],TSPL_COMPLAINT_DETAIL.tdm_code as [Franchisee],TSPL_VENDOR_MASTER.Vendor_Name as [Franchisee Name] ,TSPL_CUSTOMER_MASTER.Customer_Class,TSPL_CUSTOMER_MASTER.Cust_Code as [Outlet Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Outlet Name] ,(TSPL_CUSTOMER_MASTER.Add1+' '+TSPL_CUSTOMER_MASTER.Add2+' '+TSPL_CUSTOMER_MASTER.Add3) as Address ,TSPL_CUSTOMER_MASTER.Phone1 as [Phone No] ,TSPL_CUSTOMER_MASTER.Tin_No as [Tin No] ,TSPL_CUSTOMER_MASTER.Lst_No as [Lst No],TSPL_CUSTOMER_MASTER.Contact_Person_Name as [Contact Person Name],TSPL_CUSTOMER_MASTER.Contact_Person_Phone as [Contact Person Phone],case when TSPL_ASSET_INSTALL_PULLOUT_NEW.FOC_sec='NO' then 'Security' else 'FOC' end as [FOC Remarks],TSPL_ASSET_INSTALL_PULLOUT_NEW.Cheque_No_Sec as [DD/ Cheque No] ,TSPL_ASSET_INSTALL_PULLOUT_NEW.Sec_Amount as Amount ,DATEPART(YEAR ,TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Installation_Date) AS 'Year', TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id,TSPL_ITEM_MASTER.Item_Desc as [Asset Description],TSPL_VISI_MASTER.VisiMake as [M/C Make],TSPL_VISI_MASTER.Visi_Size as [DF Capacity],TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_type as [Type of M/C],TSPL_VISI_MASTER.Serial_No as [M/C SR.No.] ,TSPL_VISI_MASTER.Tag_No as [M/C Tag No.] from TSPL_VISI_MASTER left outer join TSPL_CUSTOMER_MASTER on TSPL_VISI_MASTER.Customer_Id=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_ASSET_INSTALL_PULLOUT_NEW ON TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id =TSPL_VISI_MASTER.Visi_Id lEFT oUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ASSET_INSTALL_PULLOUT_NEW.item_code Left Outer Join TSPL_COMPLAINT_DETAIL on TSPL_COMPLAINT_DETAIL.cust_code  =TSPL_ASSET_INSTALL_PULLOUT_NEW.Install_Customer_Id  Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_COMPLAINT_DETAIL.tdm_code) XX Where XX.RowNO=1 " + outletcode + " " + assetcode + " order by XX.Asset_Id"
            dt1 = clsDBFuncationality.GetDataTable(qry)

            If dt1 Is Nothing Or dt1.Rows.Count <= 0 Then
                RadMessageBox.Show("No Data Found To Display", Me.Text)
                GV.DataSource = Nothing
                GV.GroupDescriptors.Clear()
                GV.MasterTemplate.SummaryRowsBottom.Clear()
                Return
            End If

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                GV.DataSource = Nothing
                GV.GroupDescriptors.Clear()
                GV.MasterTemplate.SummaryRowsBottom.Clear()
                GV.DataSource = dt1
                GV.Columns("RowNo").IsVisible = False
                GV.BestFitColumns(BestFitColumnMode.DisplayedCells)
                RadPageView1.SelectedPage = RadPageViewPage2
                ' FormatGridView()
            End If

            qry = "select Comp_Name,(Add1+' '+Add2+' '+Add3) as address,(Phone1+','+phone2) as phone,Tin_No,CST_LST from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(clsCommon.myCstr(dt.Rows(0)("Comp_Name")))
            arrHeader.Add(clsCommon.myCstr(dt.Rows(0)("address")))
            arrHeader.Add("Phone No.: " + clsCommon.myCstr(dt.Rows(0)("phone")))
            arrHeader.Add("TIN No. : " + clsCommon.myCstr(dt.Rows(0)("tin_no")) + "     CST No.:" + clsCommon.myCstr(dt.Rows(0)("CST_LST")))

            If Isprint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Asset Detail Report", GV, arrHeader, "Asset Detail Report")
            ElseIf Isprint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Asset Detail Report", GV, arrHeader, "Asset Detail Report", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FormatGridView()
        GV.TableElement.TableHeaderHeight = 40
        GV.MasterTemplate.ShowRowHeaderColumn = False

        For ii As Integer = 0 To GV.Columns.Count - 1
            GV.Columns(ii).ReadOnly = True
            GV.Columns(ii).IsVisible = False
        Next

        GV.Columns("SNO").Width = 60
        GV.Columns("SNO").IsVisible = True
        GV.Columns("SNO").HeaderText = "SNO"

        GV.Columns("region").Width = 80
        GV.Columns("region").IsVisible = True
        GV.Columns("region").HeaderText = "REGION"

        GV.Columns("state_name").Width = 100
        GV.Columns("state_name").IsVisible = True
        GV.Columns("state_name").HeaderText = "STATE"

        GV.Columns("town").Width = 100
        GV.Columns("town").IsVisible = True
        GV.Columns("town").HeaderText = "TOWN"

        GV.Columns("zm/asm").Width = 100
        GV.Columns("zm/asm").IsVisible = True
        GV.Columns("zm/asm").HeaderText = "ZM/ASM"

        GV.Columns("service_ex").Width = 100
        GV.Columns("service_ex").IsVisible = True
        GV.Columns("service_ex").HeaderText = "SERVICE EXECUTIVE"

        GV.Columns("franchisee").Width = 100
        GV.Columns("franchisee").IsVisible = True
        GV.Columns("franchisee").HeaderText = "FRANCHISEE"

        GV.Columns("retail/vending").Width = 80
        GV.Columns("retail/vending").IsVisible = True
        GV.Columns("retail/vending").HeaderText = "RETAIL/VENDING"

        GV.Columns("outlet").Width = 130
        GV.Columns("outlet").IsVisible = True
        GV.Columns("outlet").HeaderText = "NAME OF OUTLET"

        GV.Columns("location").Width = 100
        GV.Columns("location").IsVisible = True
        GV.Columns("location").HeaderText = "LOCATION"

        ' GV.Columns("State").Width = 100
        'GV.Columns("State").IsVisible = True

        GV.Columns("Phone No").Width = 100
        GV.Columns("Phone no").IsVisible = True

        GV.Columns("Tin No").Width = 100
        GV.Columns("Tin no").IsVisible = True

        GV.Columns("Lst No").Width = 100
        GV.Columns("Lst no").IsVisible = True

        GV.Columns("address").Width = 120
        GV.Columns("address").IsVisible = True
        GV.Columns("address").HeaderText = "ADDRESS"

        GV.Columns("contact_person_name").Width = 100
        GV.Columns("contact_person_name").IsVisible = True
        GV.Columns("contact_person_name").HeaderText = "CONT. PERSON"

        GV.Columns("contact_person_phone").Width = 100
        GV.Columns("contact_person_phone").IsVisible = True
        GV.Columns("contact_person_phone").HeaderText = "CONT. NUMBER"

        GV.Columns("foc_remarks").Width = 100
        GV.Columns("foc_remarks").IsVisible = True
        GV.Columns("foc_remarks").HeaderText = "SECURITY/FOC RMKS"

        GV.Columns("cheque_no").Width = 80
        GV.Columns("cheque_no").IsVisible = True
        GV.Columns("cheque_no").HeaderText = "DD/CHEQUE NO."

        GV.Columns("Amount").Width = 80
        GV.Columns("Amount").IsVisible = True
        GV.Columns("Amount").HeaderText = "AMOUNT"

        GV.Columns("year1").Width = 50
        GV.Columns("year1").IsVisible = True
        GV.Columns("year1").HeaderText = "YEAR"

        GV.Columns("item_desc").Width = 120
        GV.Columns("item_desc").IsVisible = True
        GV.Columns("item_desc").HeaderText = "ASSET DESCRIPTION"

        GV.Columns("model_no").Width = 80
        GV.Columns("model_no").IsVisible = True
        GV.Columns("model_no").HeaderText = "TYPE OF M/C"

        GV.Columns("visimake").Width = 80
        GV.Columns("visimake").IsVisible = True
        GV.Columns("visimake").HeaderText = "M/C MAKE"

        GV.Columns("visi_size").Width = 80
        GV.Columns("visi_size").IsVisible = True
        GV.Columns("visi_size").HeaderText = "DF CAPACITY"

        GV.Columns("serial_no").Width = 80
        GV.Columns("serial_no").IsVisible = True
        GV.Columns("serial_no").HeaderText = "M/C SR.NO."

        GV.Columns("tag_no").Width = 80
        GV.Columns("tag_no").IsVisible = True
        GV.Columns("tag_no").HeaderText = "M/C TAG NO."

        GV.Columns("invoice_no").Width = 60
        GV.Columns("invoice_no").IsVisible = True
        GV.Columns("invoice_no").HeaderText = "INVOICE NO."

        GV.Columns("invoice_date").Width = 60
        GV.Columns("invoice_date").IsVisible = True
        GV.Columns("invoice_date").HeaderText = "INVOICE DATE"

        GV.Columns("warranty_dt").Width = 60
        GV.Columns("warranty_dt").IsVisible = True
        GV.Columns("warranty_dt").HeaderText = "WARRANTY TILL"

        GV.Columns("agreement_no").Width = 60
        GV.Columns("agreement_no").IsVisible = True
        GV.Columns("agreement_no").HeaderText = "AGREEMENT NO."

        GV.Columns("delivery_no").Width = 60
        GV.Columns("delivery_no").IsVisible = True
        GV.Columns("delivery_no").HeaderText = "DELIVERY NOTE NO."

        GV.Columns("delivery_dt").Width = 60
        GV.Columns("delivery_dt").IsVisible = True
        GV.Columns("delivery_dt").HeaderText = "DELIVERY DATE"

        GV.Columns("pullout_remarks").Width = 100
        GV.Columns("pullout_remarks").IsVisible = True
        GV.Columns("pullout_remarks").HeaderText = "PULLOUT REMARKS"

        GV.Columns("pullout_dt").Width = 60
        GV.Columns("pullout_dt").IsVisible = True
        GV.Columns("pullout_dt").HeaderText = "PULLOUT DATE"

        GV.Columns("shifted_to").Width = 130
        GV.Columns("shifted_to").IsVisible = True
        GV.Columns("shifted_to").HeaderText = "SHIFTED TO NAME OF OUTLET"

        GV.Columns("shifted_add").Width = 100
        GV.Columns("shifted_add").IsVisible = True
        GV.Columns("shifted_add").HeaderText = "ADDRESS"

        GV.Columns("shift_person").Width = 100
        GV.Columns("shift_person").IsVisible = True
        GV.Columns("shift_person").HeaderText = "CONT. PERSON"

        GV.Columns("shifted_phn_no").Width = 80
        GV.Columns("shifted_phn_no").IsVisible = True
        GV.Columns("shifted_phn_no").HeaderText = "CONTACT NO."

        GV.Columns("shifted_chq_no").Width = 60
        GV.Columns("shifted_chq_no").IsVisible = True
        GV.Columns("shifted_chq_no").HeaderText = "DD/CHEQUE NO."

        GV.Columns("shifted_chq_dt").Width = 60
        GV.Columns("shifted_chq_dt").IsVisible = True
        GV.Columns("shifted_chq_dt").HeaderText = "DD/CHEQUE DATE"

        GV.Columns("shifted_amt").Width = 80
        GV.Columns("shifted_amt").IsVisible = True
        GV.Columns("shifted_amt").HeaderText = "AMOUNT"

        GV.Columns("shifted_agreement_no").Width = 60
        GV.Columns("shifted_agreement_no").IsVisible = True
        GV.Columns("shifted_agreement_no").HeaderText = "AGREEMENT NO."

        GV.Columns("shifted_dlvry").Width = 60
        GV.Columns("shifted_dlvry").IsVisible = True
        GV.Columns("shifted_dlvry").HeaderText = "DELIVERY NOTE NO."

        GV.Columns("shifted_dlvry_dt").Width = 60
        GV.Columns("shifted_dlvry_dt").IsVisible = True
        GV.Columns("shifted_dlvry_dt").HeaderText = "DELIVERY DATE"

        GV.MasterTemplate.AutoExpandGroups = True
        RadPageView1.SelectedPage = RadPageViewPage2

    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Refresh = 2
    End Enum

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        PrintData(Exporter.Refresh)
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        PrintData(Exporter.Excel)
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        PrintData(Exporter.PDF)
    End Sub
End Class

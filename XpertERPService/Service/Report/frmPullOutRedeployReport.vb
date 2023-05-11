Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports XpertERPEngine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource

Public Class frmPullOutRedeployReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPullOutRedeployReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

    End Sub
    Public Sub loadCustomerCode()
        Dim qry11 As String = "SELECT  Cust_Code,Customer_Name FROM TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry11)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Customer_Name"
    End Sub
    Private Sub CustomerBillWiseDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        loadCustomerCode()
        LoadAssets()
        reset()

    End Sub
    Sub LoadAssets()
        Dim qry As String = "select TSPL_VISI_MASTER.Visi_Id as 'AssetId' ,TSPL_ITEM_MASTER.Item_Desc as 'AssetDesc',TSPL_VISI_MASTER.Asset_Type,TSPL_VISI_MASTER.Serial_No,TSPL_VISI_MASTER.Model_No,TSPL_VISI_MASTER.Serial_No,TSPL_VISI_MASTER.Tag_No,TSPL_VISI_MASTER.VisiMake as 'AssetMake',TSPL_VISI_MASTER.Visi_Size as 'AssetSize'  from TSPL_VISI_MASTER left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_VISI_MASTER.Asset_No "
        cbgAssets.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgAssets.ValueMember = "AssetId"
        cbgAssets.DisplayMember = "AssetId"
    End Sub
    Private Sub reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        MyRadioButton2.IsChecked = True
        rbtnvAll.IsChecked = True
        cbgCustomer.CheckedAll()
        cbgAssets.CheckedAll()
        cbgCustomer.Enabled = False
        cbgAssets.Enabled = False
        radioInstall.Checked = True
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub print()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strAssets As String = ""
        Dim strDateRange As String = ""
        Try
            qry = " select convert(varchar,TSPL_ASSET_INSTALL_PULLOUT_NEW.Trans_Date,103) + case when tspl_asset_install_pullout_new.Trans_Type='Installed' then  tspl_asset_install_pullout_new.Install_Customer_Id else case when tspl_asset_install_pullout_new.Trans_Type='PulledOut' then tspl_asset_install_pullout_new.Pullout_Customer_Id else tspl_asset_install_pullout_new.Install_Customer_Id+tspl_asset_install_pullout_new.Pullout_Customer_Id end end As 'TransChange' , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pincode ,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, convert (varchar,TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Installation_Date,103) as 'InstallDate',convert (varchar,TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Pullout_Date,103) as 'PulloutDate',tspl_asset_install_pullout_new.Pullout_Customer_Id,cust1.Customer_Name as 'PulloutCustomerName',cust1.Contact_Person_Name as 'PulloutCustContactPerson',cust1.Phone1 as 'PulloutCustPhone1',cust1.Phone2 as  'PulloutCustPhone2',cust1.Add1 as 'PulloutCustAdd1',cust1.Add2 as 'PulloutCustAdd2',cust1.Add3 as 'PulloutCustAdd3', city1.City_Name as 'PulloutCustCity',state1.STATE_NAME  as 'PulloutCustState' ,tspl_asset_install_pullout_new.Install_Customer_Id ,cust2.Customer_Name as 'InstallCustomerName',cust2.Contact_Person_Name as 'InstallCustContactPerson',cust2.Phone1 as 'InstallCustPhone1',cust2.Phone2 as  'InstallCustPhone2',cust2.Add1 as 'InstallCustAdd1',cust2.Add2 as 'InstallCustAdd2',cust2.Add3 as 'InstallCustAdd3', city2.City_Name as 'InstallCustCity',state2.STATE_NAME as 'InstallCustState' ,TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id ,TSPL_ASSET_INSTALL_PULLOUT_NEW.Trans_Date as 'TransDate' ,TSPL_ASSET_INSTALL_PULLOUT_NEW.Trans_Type as 'TransType' ,TSPL_VISI_MASTER.VisiMake,TSPL_VISI_MASTER.Asset_Type,TSPL_VISI_MASTER.Visi_Size,TSPL_VISI_MASTER.Serial_No ,TSPL_VISI_MASTER.Tag_No ,TSPL_RGP_DETAIL.Security_Amount    from tspl_asset_install_pullout_new  left outer join TSPL_CUSTOMER_MASTER as cust1 on cust1.Cust_Code=tspl_asset_install_pullout_new.Pullout_Customer_Id   left outer join TSPL_CUSTOMER_MASTER as cust2 on cust2.Cust_Code=tspl_asset_install_pullout_new.Install_Customer_Id   left outer join TSPL_CITY_MASTER as city1 on city1.City_Code=cust1.City_Code  left outer join TSPL_CITY_MASTER as city2 on city2.City_Code=cust2.City_Code  left outer join TSPL_STATE_MASTER as state1 on state1.STATE_CODE=cust1.State  left outer join TSPL_STATE_MASTER as state2 on state2.STATE_CODE=cust2.State  left outer join TSPL_VISI_MASTER on TSPL_VISI_MASTER.Visi_Id=TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.Vendor_Code=TSPL_ASSET_INSTALL_PULLOUT_NEW.Install_Customer_Id   left outer join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.RGP_No  =TSPL_RGP_HEAD.RGP_No and TSPL_RGP_DETAIL.Item_Code=TSPL_VISI_MASTER.Asset_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_ASSET_INSTALL_PULLOUT_NEW.Comp_Code  "



            If cbgCustomer.CheckedValue.Count > 0 Then
                If radioInstall.Checked = True Then
                    strCustomer += " and tspl_asset_install_pullout_new.Install_Customer_Id in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") and tspl_asset_install_pullout_new.Trans_Type='Installed' "
                ElseIf radioPullout.Checked = True Then
                    strCustomer += " and tspl_asset_install_pullout_new.Pullout_Customer_Id in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") and tspl_asset_install_pullout_new.Trans_Type='PulledOut' "
                Else
                    strCustomer += " and tspl_asset_install_pullout_new.Pullout_Customer_Id in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") and tspl_asset_install_pullout_new.Trans_Type='Both'"
                End If
            Else
                strCustomer = ""
            End If
            If cbgAssets.CheckedValue.Count > 0 Then
                strAssets += " and TSPL_ASSET_INSTALL_PULLOUT_NEW.Asset_Id  in  (" + clsCommon.GetMulcallString(cbgAssets.CheckedValue) + ") "
            Else
                strAssets = ""

            End If

            strDateRange = " where  CONVERT(DATE, TSPL_ASSET_INSTALL_PULLOUT_NEW.Trans_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_ASSET_INSTALL_PULLOUT_NEW.Trans_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            qry += strDateRange
            qry += strAssets
            qry += strCustomer


            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            'gv1.DataSource = dt
            Dim frmCrystalReportViewer As New frmCrystalReportViewer
            frmCrystalReportViewer.funreport(CrystalReportFolder.ServiceReport, dt, "rptPulloutRedeployReport", "Asset PullOut Redeploy Report")

        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try


    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("Start Date Can Not Be Greater Then End Date")
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Customer")
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgAssets.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Assets")
            cbgAssets.Focus()
            Exit Sub
        End If
        print()


    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub



    Private Sub MyRadioButton1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton1.ToggleStateChanged
        If MyRadioButton1.IsChecked() Then
            cbgCustomer.UnCheckedAll()

            cbgCustomer.Enabled = True
        Else
            cbgCustomer.UnCheckedAll()
            cbgCustomer.Enabled = False
        End If

    End Sub

    Private Sub rbtnvselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnvselect.ToggleStateChanged
        If rbtnvselect.IsChecked() Then
            cbgAssets.UnCheckedAll()
            cbgAssets.Enabled = True
        Else
            cbgAssets.UnCheckedAll()
            cbgAssets.Enabled = False
        End If
    End Sub

    Private Sub MyRadioButton2_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton2.ToggleStateChanged
        If MyRadioButton2.IsChecked() Then
            cbgCustomer.CheckedAll()
            cbgCustomer.Enabled = False
        Else
            cbgCustomer.UnCheckedAll()

            cbgCustomer.Enabled = True
        End If

    End Sub

    Private Sub rbtnvAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnvAll.ToggleStateChanged
        If rbtnvAll.IsChecked() Then
            cbgAssets.CheckedAll()
            cbgAssets.Enabled = False
        Else
            cbgAssets.UnCheckedAll()

            cbgAssets.Enabled = True
        End If
    End Sub
End Class

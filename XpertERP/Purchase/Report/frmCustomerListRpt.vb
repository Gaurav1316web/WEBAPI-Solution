'''' for bug no BM00000000504
'''' for bug no BM00000000648
'--Updation By--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000001290]
Imports System.IO
Imports common

Public Class frmCustomerListRpt
    Inherits FrmMainTranScreen
    Dim trnsLst As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public ApplyOrderByNumeric As Boolean = False

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomersListReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnPrint.Visible = MyBase.isModifyF
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub frmCustomerListRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ApplyOrderByNumeric = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyOrderByNumeric, clsFixedParameterCode.ApplyOrderByNumeric, Nothing)) = 1, True, False)

        chkcustomerAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chkCustGrpAll.IsChecked = True
        chkActive.Checked = True
        chkZoneAll.IsChecked = True
        LoadCustomer()
        LoadCustomerGroup()
        LoadRoute()
        LoadZone()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Sub LoadCustomerGroup()
        Dim strquery As String = "Select Cust_Group_Code as [Code], Cust_Group_Desc as [Description] from TSPL_CUSTOMER_GROUP_MASTER"
        cbgCustGrp.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustGrp.ValueMember = "Code"
        cbgCustGrp.DisplayMember = "Description"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select cust_Code,customer_Name from tspl_customer_master where 1=1"
        If chkActive.Checked Then
            qry += " AND Status='N'"
            If chkSettlementPending.Checked Then
                qry += " AND OnHold='Y'"
            Else
                qry += " AND OnHold='N'"
            End If
        ElseIf chkInactive.Checked Then
            qry += " AND Status='Y'"
        ElseIf chkAll.Checked Then
            qry += ""
        End If
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
            If clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
                qry += " and tspl_customer_master.Cust_Code in (" + objCommonVar.strCurrUserCustomers + ") "
            End If
        End If
        qry += "order by cust_Code"
        cbgcustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgcustomer.ValueMember = "cust_Code"
        cbgcustomer.DisplayMember = "customer_Name"

    End Sub

    Sub LoadRoute()
        Dim strquery As String = "Select Route_No as [Code], Route_Desc as [Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgRoute.ValueMember = "Code"
        cbgRoute.DisplayMember = "Description"
    End Sub

    Sub LoadZone()
        Dim strquery As String = "select Zone_Code as Code ,Description as [Description] from TSPL_ZONE_MASTER"
        cbgZone.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgZone.ValueMember = "Code"
        cbgZone.DisplayMember = "Description"
    End Sub
    Private Sub chkcustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcustomerAll.ToggleStateChanged
        cbgcustomer.Enabled = Not chkcustomerAll.IsChecked
    End Sub

    Private Sub chkCustGrpAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgCustGrp.Enabled = Not chkCustGrpAll.IsChecked
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub Print()
        Dim qry As String
        Try
            If chkcustomerSelect.IsChecked AndAlso cbgcustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer.")
            End If
            If chkCustGrpSelect.IsChecked AndAlso cbgCustGrp.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer Group.")
            End If
            If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Route.")
            End If

            If chkZoneSelect.IsChecked AndAlso cbgZone.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Zone.")
            End If

            qry = " SELECT  (case when  isnull(convert (varchar,Agg_Made_Date,103),'') ='01/01/1753' then '' else  isnull(convert (varchar,Agg_Made_Date,103),'') end) as Agg_Made_Date,( case when  isnull(convert (varchar,Agg_Close_Date,103),'')='01/01/1753' then '' else isnull(convert (varchar,Agg_Close_Date,103),'') end ) as Agg_Close_Date,TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, (tspl_customer_master.Add1 + case when len(tspl_customer_master.add2)> 0 then ', ' else '' end + tspl_customer_master.Add2 +case when len(tspl_customer_master.Add3)> 0 then ', 'else '' end + Case When Len(tspl_customer_master.City_Code)>0 THEN ', ' else '' end+ tspl_customer_master.City_Code +case when len(tspl_customer_master.State)> 0 then ', ' else '' end  +tspl_customer_master.State ) as [Customer Address], TSPL_CUSTOMER_MASTER.Route_Desc,TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as [Zone Desc]  , TSPL_CUSTOMER_MASTER.Cust_Category_Code,  TSPL_CUSTOMER_MASTER.City_Code, TSPL_CUSTOMER_MASTER.Route_Group, TSPL_COMPANY_MASTER.Comp_Name,  TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC,  ISNULL(tspl_company_Master.ADD1,'') as address1, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Customer Grpoup Description],TSPL_CUSTOMER_MASTER.Tin_No,TSPL_CUSTOMER_MASTER.Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone , TSPL_CUSTOMER_MASTER.GSTNO , TSPL_STATE_MASTER.GST_STATE_Code as [GST STATE Code], case when  TSPL_CUSTOMER_MASTER.GST_Registered =1 then 'Yes' else 'No' end as Registered  FROM TSPL_CUSTOMER_MASTER INNER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Category_Code = TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_CUSTOMER_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code =TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code "
            qry += " LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_CUSTOMER_MASTER.Route_No  left outer join TSPL_STATE_MASTER  on TSPL_STATE_MASTER.STATE_CODE = TSPL_CUSTOMER_MASTER.State left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code  Where 2=2 "

            If Not chkcustomerAll.IsChecked = True Then
                qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" + (clsCommon.GetMulcallString(cbgcustomer.CheckedValue)) + ")"
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                    If clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
                        qry += " and tspl_customer_master.Cust_Code in (" + objCommonVar.strCurrUserCustomers + ") "
                    End If
                End If
            End If
            If chkCustGrpSelect.IsChecked And cbgCustGrp.CheckedValue.Count > 0 Then
                qry += " AND TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGrp.CheckedValue) + ")"
            End If

            If chkRouteSelect.IsChecked And cbgRoute.CheckedValue.Count > 0 Then
                qry += " AND TSPL_ROUTE_MASTER.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If

            If chkZoneSelect.IsChecked And cbgZone.CheckedValue.Count > 0 Then
                qry += " AND TSPL_ZONE_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(cbgZone.CheckedValue) + ")"
            End If

            If chkActive.Checked Then
                qry += " AND TSPL_CUSTOMER_MASTER.Status='N'"
                If chkSettlementPending.Checked Then
                    qry += " AND TSPL_CUSTOMER_MASTER.OnHold='Y'"
                Else
                    qry += " AND TSPL_CUSTOMER_MASTER.OnHold='N'"
                End If
            ElseIf chkInactive.Checked Then
                qry += " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.Purchase, dt, "crptCustomerLstReport", "Customer List Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub

    Public Sub Load_Report()

        Try

            Dim qry As String

            If chkcustomerSelect.IsChecked AndAlso cbgcustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer.")
            End If

            If chkCustGrpSelect.IsChecked AndAlso cbgCustGrp.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer Group.")
            End If

            If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Route.")
            End If
            If chkZoneSelect.IsChecked AndAlso cbgZone.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Zone.")
            End If

            '========update by preeti gupta Against ticket no[BHA/25/02/19-000822]
            'qry = " SELECT tabDistributor.Cust_Code As [Distributor Code],tabDistributor.Customer_Name As [Distributor Name],TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As [Customer Name], (tspl_customer_master.Add1 + case when len(tspl_customer_master.add2)> 0 then ', ' else '' end + tspl_customer_master.Add2 +case when len(tspl_customer_master.Add3)> 0 then ', 'else '' end + Case When Len(tspl_customer_master.City_Code)>0 THEN ', ' else '' end+ tspl_customer_master.City_Code +case when len(tspl_customer_master.State)> 0 then ', ' else '' end  +tspl_customer_master.State ) as [Customer Address], TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Customer Grpoup Description],TSPL_CUSTOMER_MASTER.PAN as [PAN No],TSPL_CUSTOMER_MASTER.Tin_No as [Tin No],(case when  isnull(convert (varchar,TSPL_CUSTOMER_MASTER.Agg_Made_Date,103),'') ='01/01/1753' then '' else  isnull(convert (varchar,TSPL_CUSTOMER_MASTER.Agg_Made_Date,103),'') end) as [Agreement Made Date],( case when  isnull(convert (varchar,TSPL_CUSTOMER_MASTER.Agg_Close_Date,103),'')='01/01/1753' then '' else isnull(convert (varchar,TSPL_CUSTOMER_MASTER.Agg_Close_Date,103),'') end ) as [Agreement Close Date],TSPL_CUSTOMER_MASTER.Contact_Person_Name AS [Contact Person Name],TSPL_CUSTOMER_MASTER.Contact_Person_Phone as [Contact Person],TSPL_CUSTOMER_MASTER.Route_Desc As [Route Description] ,TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as [Zone Desc]  ,TSPL_CUSTOMER_MASTER.GSTNO , TSPL_STATE_MASTER.GST_STATE_Code as [GST STATE Code], TSPL_STATE_MASTER.STATE_NAME as [State Name] , case when  TSPL_CUSTOMER_MASTER.GST_Registered =1 then 'Yes' else 'No' end as Registered   " &
            '      " FROM TSPL_CUSTOMER_MASTER "
            'qry += " Left Outer JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Category_Code = TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE "
            'qry += " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_CUSTOMER_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code =TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code Left Outer Join TSPL_CUSTOMER_MASTER as tabDistributor on tabDistributor.Cust_Code=TSPL_CUSTOMER_MASTER.Distributor_Code "
            'qry += " LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_CUSTOMER_MASTER.Route_No left outer join TSPL_STATE_MASTER  on TSPL_STATE_MASTER.STATE_CODE = TSPL_CUSTOMER_MASTER.State left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code Where 2=2 And TSPL_CUSTOMER_MASTER.IsDistributor='N' "
            ''----------------Varsha 27-10-23--------For added security amount then group by all
            qry = "SELECT max(tabDistributor.Cust_Code) As [Distributor Code],max(tabDistributor.Customer_Name) As [Distributor Name],TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Code], max(TSPL_CUSTOMER_MASTER.Customer_Name) As [Customer Name],
                     sum(Receipt_Amount * (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then -1 ELSE 1 END))    AS [Security Amount] 	,
                     max(tspl_customer_master.Add1 + case when len(tspl_customer_master.add2)> 0 then ', ' else '' end + tspl_customer_master.Add2 +case when len(tspl_customer_master.Add3)> 0 then ', 'else '' end + Case When Len(tspl_customer_master.City_Code)>0 THEN ', ' else '' end+ tspl_customer_master.City_Code +case when len(tspl_customer_master.State)> 0 then ', ' else '' end  +tspl_customer_master.State ) as [Customer Address], max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as [Customer Grpoup Description],max(TSPL_CUSTOMER_MASTER.PAN) as [PAN No],max(TSPL_CUSTOMER_MASTER.Tin_No) as [Tin No],max(case when  isnull(convert (varchar,TSPL_CUSTOMER_MASTER.Agg_Made_Date,103),'') ='01/01/1753' then '' else  isnull(convert (varchar,TSPL_CUSTOMER_MASTER.Agg_Made_Date,103),'') end) as [Agreement Made Date],max( case when  isnull(convert (varchar,TSPL_CUSTOMER_MASTER.Agg_Close_Date,103),'')='01/01/1753' then '' else isnull(convert (varchar,TSPL_CUSTOMER_MASTER.Agg_Close_Date,103),'') end ) as [Agreement Close Date],max(TSPL_CUSTOMER_MASTER.Contact_Person_Name) AS [Contact Person Name],max(TSPL_CUSTOMER_MASTER.Contact_Person_Phone) as [Contact Person], "
            If ApplyOrderByNumeric Then
                qry += " max(cast(TSPL_CUSTOMER_MASTER.Route_No as int)) As [Route No]"

            Else
                qry += " max(TSPL_CUSTOMER_MASTER.Route_No) As [Route No]"

            End If
            qry += ",max(TSPL_CUSTOMER_MASTER.Route_Desc) As [Route Description] ,max(TSPL_CUSTOMER_MASTER.Zone_Code) as [Zone Code],max(TSPL_ZONE_MASTER.Description) as [Zone Desc]  ,max(TSPL_CUSTOMER_MASTER.GSTNO) as GSTNO , max(TSPL_STATE_MASTER.GST_STATE_Code) as [GST STATE Code], max(TSPL_STATE_MASTER.STATE_NAME) as [State Name] , max(case when  TSPL_CUSTOMER_MASTER.GST_Registered =1 then 'Yes' else 'No' end) as Registered    FROM TSPL_CUSTOMER_MASTER  Left Outer JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Category_Code = TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_CUSTOMER_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code =TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code Left Outer Join TSPL_CUSTOMER_MASTER as tabDistributor on tabDistributor.Cust_Code=TSPL_CUSTOMER_MASTER.Distributor_Code  LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_CUSTOMER_MASTER.Route_No 
                       Left OUTER join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
                       left outer join TSPL_STATE_MASTER  on TSPL_STATE_MASTER.STATE_CODE = TSPL_CUSTOMER_MASTER.State left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code 
                     Where 2=2 And TSPL_CUSTOMER_MASTER.IsDistributor='N' 
            "

            If Not chkcustomerAll.IsChecked = True Then
                qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" + (clsCommon.GetMulcallString(cbgcustomer.CheckedValue)) + ")"
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                    If clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
                        qry += " and tspl_customer_master.Cust_Code in (" + objCommonVar.strCurrUserCustomers + ") "
                    End If
                End If
            End If
            If chkCustGrpSelect.IsChecked And cbgCustGrp.CheckedValue.Count > 0 Then
                qry += " AND TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGrp.CheckedValue) + ")"
            End If

            If chkRouteSelect.IsChecked And cbgRoute.CheckedValue.Count > 0 Then
                qry += " AND TSPL_ROUTE_MASTER.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If

            If chkZoneSelect.IsChecked And cbgZone.CheckedValue.Count > 0 Then
                qry += " AND TSPL_ZONE_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(cbgZone.CheckedValue) + ")"
            End If


            If chkActive.Checked Then
                qry += " AND TSPL_CUSTOMER_MASTER.Status='N'"
                If chkSettlementPending.Checked Then
                    qry += " AND TSPL_CUSTOMER_MASTER.OnHold='Y'"
                Else
                    qry += " AND TSPL_CUSTOMER_MASTER.OnHold='N'"
                End If
            ElseIf chkInactive.Checked Then
                qry += " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If
            qry += "Group by TSPL_CUSTOMER_MASTER.Cust_Code  order by [Route No]"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dt
                FormatGrid()
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        gv.Columns("Distributor Code").IsVisible = True
        gv.Columns("Distributor Code").Width = 120
        gv.Columns("Distributor Code").HeaderText = " Distributor Code"

        gv.Columns("Distributor Name").IsVisible = True
        gv.Columns("Distributor Name").Width = 120
        gv.Columns("Distributor Name").HeaderText = " Distributor Name"


        gv.Columns("Customer Code").IsVisible = True
        gv.Columns("Customer Code").Width = 120
        gv.Columns("Customer Code").HeaderText = " Customer Code"


        gv.Columns("Customer Name").IsVisible = True
        gv.Columns("Customer Name").Width = 120
        gv.Columns("Customer Name").HeaderText = " Customer Name"

        

        gv.Columns("Customer Address").IsVisible = True
        gv.Columns("Customer Address").Width = 130
        gv.Columns("Customer Address").HeaderText = " Customer Address"

        gv.Columns("Customer Grpoup Description").IsVisible = True
        gv.Columns("Customer Grpoup Description").Width = 100
        gv.Columns("Customer Grpoup Description").HeaderText = " Customer Group Description"

        gv.Columns("PAN No").IsVisible = True
        gv.Columns("PAN No").Width = 80
        gv.Columns("PAN No").HeaderText = "PAN No"

        gv.Columns("Tin No").IsVisible = True
        gv.Columns("Tin No").Width = 80
        gv.Columns("Tin No").HeaderText = "Tin No"

        gv.Columns("Agreement Made Date").IsVisible = True
        gv.Columns("Agreement Made Date").Width = 100
        gv.Columns("Agreement Made Date").HeaderText = " Agreement Made Date"
        gv.Columns("Agreement Made Date").FormatString = "{0:d}"

        gv.Columns("Agreement Close Date").IsVisible = True
        gv.Columns("Agreement Close Date").Width = 100
        gv.Columns("Agreement Close Date").HeaderText = " Agreement Close Date"
        gv.Columns("Agreement Close Date").FormatString = "{0:d}"

        gv.Columns("Contact Person Name").IsVisible = True
        gv.Columns("Contact Person Name").Width = 80
        gv.Columns("Contact Person Name").HeaderText = " Contact Person Name"

        gv.Columns("Contact Person").IsVisible = True
        gv.Columns("Contact Person").Width = 80
        gv.Columns("Contact Person").HeaderText = " Contact Person"

        gv.Columns("Route No").IsVisible = True
        gv.Columns("Route No").Width = 100
        gv.Columns("Route No").HeaderText = " Route No"


        gv.Columns("Route Description").IsVisible = True
        gv.Columns("Route Description").Width = 100
        gv.Columns("Route Description").HeaderText = " Route Description"

        gv.Columns("Zone Code").IsVisible = True
        gv.Columns("Zone Code").Width = 100

        gv.Columns("Zone Desc").IsVisible = True
        gv.Columns("Zone Desc").Width = 100

        gv.Columns("GSTNO").IsVisible = True
        gv.Columns("GSTNO").Width = 120
        gv.Columns("GSTNO").HeaderText = " GSTIN No"

        gv.Columns("GST STATE Code").IsVisible = True
        gv.Columns("GST STATE Code").Width = 100
        gv.Columns("GST STATE Code").HeaderText = " GST STATE Code"

        gv.Columns("STATE NAME").IsVisible = True
        gv.Columns("STATE NAME").Width = 100
        gv.Columns("STATE NAME").HeaderText = " STATE NAME"

        gv.Columns("Registered").IsVisible = True
        gv.Columns("Registered").Width = 100
        gv.Columns("Registered").HeaderText = " Registered"

        gv.Columns("Security Amount").IsVisible = True
        gv.Columns("Security Amount").Width = 100
        gv.Columns("Security Amount").HeaderText = " Security Amount"
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True



    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        chkcustomerAll.IsChecked = True
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadCustomer()
    End Sub




    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-LST-RPT"
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
    '            'btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub frmCustomerListRpt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub
    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        chkSettlementPending.Checked = False
        chkSettlementPending.Visible = chkActive.Checked
        LoadCustomer()
    End Sub

    Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInactive.CheckedChanged
        LoadCustomer()
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        LoadCustomer()
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub

    Private Sub chkSettlementPending_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSettlementPending.ToggleStateChanged
        LoadCustomer()
    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Print(EnumExportTo.Excel)
    End Sub


    Private Sub chkCustGrpAll_ToggleStateChanged_1(sender As Object, args As StateChangedEventArgs) Handles chkCustGrpAll.ToggleStateChanged
        cbgCustGrp.Enabled = Not chkCustGrpAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged_1(sender As Object, args As StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub


    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.CustomersListReport & "'"))
            Dim customerType As String = ""
            If chkActive.Checked = True Then
                customerType = chkActive.Text
            End If
            If chkInactive.Checked = True Then
                customerType = chkInactive.Text
            End If
            If chkAll.Checked = True Then
                customerType = chkAll.Text
            End If
            arrHeader.Add("Customer Type: " + customerType + "")

            If chkcustomerSelect.IsChecked Then
                Dim stCustomerName As String = ""
                For Each StrName As String In cbgcustomer.CheckedDisplayMember
                    If clsCommon.myLen(stCustomerName) > 0 Then
                        stCustomerName += ", "
                    End If
                    stCustomerName += StrName
                Next
                Dim strCustomerCode As String = ""
                For Each StrCode As String In cbgcustomer.CheckedValue
                    If clsCommon.myLen(strCustomerCode) > 0 Then
                        strCustomerCode += ", "
                    End If
                    strCustomerCode += StrCode
                Next
                arrHeader.Add(("Customer Name: " + stCustomerName + " "))
            End If

            If chkCustGrpSelect.IsChecked Then
                Dim stCustomerGroupName As String = ""
                For Each StrName As String In cbgCustGrp.CheckedDisplayMember
                    If clsCommon.myLen(stCustomerGroupName) > 0 Then
                        stCustomerGroupName += ", "
                    End If
                    stCustomerGroupName += StrName
                Next
                Dim strCustomerGroupCode As String = ""
                For Each StrCode As String In cbgcustomer.CheckedValue
                    If clsCommon.myLen(strCustomerGroupCode) > 0 Then
                        strCustomerGroupCode += ", "
                    End If
                    strCustomerGroupCode += StrCode
                Next
                arrHeader.Add(("Customer Group: " + stCustomerGroupName + " "))
            End If

            If chkRouteSelect.IsChecked Then
                Dim stCustomerRouteName As String = ""
                For Each StrName As String In cbgRoute.CheckedDisplayMember
                    If clsCommon.myLen(stCustomerRouteName) > 0 Then
                        stCustomerRouteName += ", "
                    End If
                    stCustomerRouteName += StrName
                Next
                Dim strCustomerRouteCode As String = ""
                For Each StrCode As String In cbgcustomer.CheckedValue
                    If clsCommon.myLen(strCustomerRouteCode) > 0 Then
                        strCustomerRouteCode += ", "
                    End If
                    strCustomerRouteCode += StrCode
                Next
                arrHeader.Add(("Route: " + stCustomerRouteName + " "))
            End If


            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Customer List Report", gv, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Customer List Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub



    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Print(EnumExportTo.PDF)
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub chkZoneAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkZoneAll.ToggleStateChanged
        cbgZone.Enabled = Not chkZoneAll.IsChecked
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class

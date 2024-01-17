
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports XpertERPEngine

Public Class rptCattleFeedSaleReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim StrPermission As String

    Private Sub rptCattleFeedSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        Reset()

    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = ""
        Dim WhrCls As String = ""
        qry = "select Location_Code AS Code,Location_Desc as Name  from TSPL_LOCATION_MASTER"
        WhrCls = " Is_Sub_Location = 'N' AND Location_Category <> 'MCC' and GIT_Type  <> 'Y' "

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ") "
        End If

        txtLocation.Value = clsCommon.ShowSelectForm("CattleFeedSale", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            txtSubLocation.Enabled = True
        Else
            txtSubLocation.Enabled = False
        End If

    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        If clsCommon.myLen(txtLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Location code before sub location", Me.Text)
            Exit Sub
        End If

        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            txtSubLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + txtLocation.Value + "'", txtSubLocation.Value, isButtonClicked)
        End If

    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click

        Dim qry As String = ""
        qry = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address
           , TSPL_VENDOR_MASTER.Vendor_Code as [VSP Code],Vendor_Name as [VSP Name],VLC_Code_VLC_Uploader as Vlc_Code
            from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'
            left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  
            left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id 
            left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code 
            left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code 
            left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
        where 2=2 and  TSPL_CUSTOMER_MASTER.CUSTOMER_FORM_TYPE='VSP'"

        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CattleFeedSale", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)


    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtLocation.Value = ""
        txtSubLocation.Value = ""
        txtCustomer.arrValueMember = Nothing
        ddCreditCash.SelectedIndex = 0
        EnableDisableControl(True)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub

    Private Sub LoadData()
        Try
            If chkBalanceWise.Checked Then
                If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                    If txtItemCode.arrValueMember.Count > 1 Then
                        clsCommon.MyMessageBoxShow(Me, "You can select only one item at a time when Balance Wise checked", Me.Text)
                        Exit Sub
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "You must select atleast one item when Balance Wise checked", Me.Text)
                    Exit Sub
                End If
                ddCreditCash.SelectedIndex = 3
            End If
            Dim finalQuery As String = ""
            Dim qry As String = ""

            Dim BaseQry As String = "select  TSPL_ITEM_MASTER.Item_Code, (VLC_Code_VLC_Uploader) as [DCS Code] , (VLC_Name) as [DCS Name],convert(varchar ,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Date,TSPL_SD_SHIPMENT_HEAD.Document_Date as Doc_Date ,
(TSPL_ITEM_MASTER.Short_Description) as [Short Description] , TSPL_SD_SHIPMENT_DETAIL. OrgUnit_code AS UOM , (TSPL_SD_SHIPMENT_DETAIL.Item_Cost) AS Rate , (TSPL_SD_SHIPMENT_DETAIL.Amount) as Amount, TSPL_SD_SHIPMENT_DETAIL.Qty   from TSPL_SD_SHIPMENT_HEAD
left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
left outer join tspl_customer_master on tspl_customer_master.Cust_Code = TSPL_SD_SHIPMENT_HEAD.customer_code
    left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code 
            left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code 
            left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
			where 2 = 2  and convert( date ,TSPL_SD_SHIPMENT_HEAD.Document_Date , 103) >= CONVERT(date, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "', 103)
              and convert( date ,TSPL_SD_SHIPMENT_HEAD.Document_Date , 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "', 103)"

            If clsCommon.myLen(txtLocation.Value) > 0 Then
                BaseQry += " And TSPL_SD_SHIPMENT_HEAD.Bill_To_Location = '" & txtLocation.Value & "' "
            End If

            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                BaseQry += " And TSPL_SD_SHIPMENT_HEAD.Sub_Location_code = '" & txtSubLocation.Value & "' "
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                BaseQry += "  and TSPL_SD_SHIPMENT_HEAD.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If

            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                BaseQry += "  and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "

            End If
            If clsCommon.CompairString(ddCreditCash.SelectedItem.Text, "Credit") = CompairStringResult.Equal Then
                qry = "select ROW_NUMBER() Over (Order By (Doc_Date)) As [SNo.], [DCS Name],[DCS Code] ,Doc_Date, Date , [Short Description],UOM ,Qty , Rate , Amount   from (
              SELECT  [DCS Code] , [DCS Name] ,Doc_Date, Date , [Short Description] ,  UOM , Rate , Amount , Qty FROM ("
                BaseQry += " AND TSPL_SD_SHIPMENT_HEAD.Is_CashSale = 'N'"
                BaseQry += " )XX ) xxx"
                finalQuery = "" & qry & "   " & BaseQry & ""
            ElseIf clsCommon.CompairString(ddCreditCash.SelectedItem.Text, "Cash") = CompairStringResult.Equal Then
                qry = "select ROW_NUMBER() Over (Order By (Doc_Date)) As [SNo.], [DCS Name],[DCS Code] ,Doc_Date, Date , [Short Description], UOM ,Qty , Rate , Amount   from (
              SELECT  [DCS Code] , [DCS Name] ,Doc_Date, Date , [Short Description] ,  UOM , Rate , Amount , Qty,Item_Code FROM ("
                BaseQry += " AND TSPL_SD_SHIPMENT_HEAD.Is_CashSale = 'Y'"
                BaseQry += " )XX ) xxx"
                finalQuery = "" & qry & "   " & BaseQry & ""
            ElseIf clsCommon.CompairString(ddCreditCash.SelectedItem.Text, "Both") = CompairStringResult.Equal Then
                qry = "select ROW_NUMBER() Over (Order By (Doc_Date)) As [SNo.], [DCS Name],[DCS Code] ,Doc_Date, Date , [Short Description] ,UOM,Qty , Rate , Amount   from 
(SELECT  [DCS Code] , max([DCS Name])[DCS Name] ,Doc_Date, Date , max([Short Description])[Short Description] , MAX(UOM) AS UOM , max(rate) as Rate , sum(Amount) Amount , sum(qty) Qty , Item_Code FROM ("

                BaseQry += "  AND TSPL_SD_SHIPMENT_HEAD.Is_CashSale = 'N' OR TSPL_SD_SHIPMENT_HEAD.Is_CashSale = 'Y' "
                BaseQry += " )XX "
                If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                    BaseQry += " where xx.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")"

                End If
                BaseQry += " GROUP  BY xx.Date , xx.[DCS Code], xx.Item_Code , xx.Doc_Date ) xxx "
                finalQuery = "" & qry & "   " & BaseQry & " "
            End If
            finalQuery += " ORDER BY [SNo.]"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(finalQuery)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormation()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If


        Catch ex As Exception

        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        gv1.Columns("Doc_Date").IsVisible = False
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        Dim qry As String = ""
        qry = "select Item_Code as Code , Item_Desc  as [Item Description] , Short_Description AS [Short Description] from TSPL_ITEM_MASTER where Item_Used_as ='S'"

        txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("CattleFeedSale", qry, "Code", "Item Description", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub
End Class
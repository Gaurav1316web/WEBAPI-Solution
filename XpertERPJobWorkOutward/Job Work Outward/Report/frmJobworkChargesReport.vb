Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmJobworkChargesReport
    Inherits FrmMainTranScreen
    'Dim dt As DataTable = Nothing
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub FrmJobworkChargesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Reset()
        LoadLocation()
        rbtnLocationAll.IsChecked = True
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        'Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"
        'Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""

        Dim whrCls As String = " and Location_Code in (select Main_Location_Code from TSPL_LOCATION_MASTER where Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical') "
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""

        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub

    Sub Reset()
        'fromDate.Value = clsCommon.GETSERVERDATE()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        txtVendor.arrValueMember = Nothing
        'txtLocation.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        'txtLocation.Enabled = True
        txtVendor.Enabled = True
        txtCustGroup.Enabled = True
        txtItem.Enabled = True
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(rdbSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv
        LoadData(Exporter.Refresh)
    End Sub
    Sub LoadData(ByVal IsPrint As Exporter)
        Try
            Dim From_Date As String = clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy")
            Dim To_Date As String = clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy")
            Dim strLoacation As String = String.Empty
            Dim strVendor As String = String.Empty
            Dim strVendorGroup As String = String.Empty
            Dim strItem As String = String.Empty
            Dim mainQry As String = String.Empty
            Dim Qry As String = String.Empty
            Dim wherDateSRN As String = " and CONVERT(DATE,TSPL_JWO_SRN_HEAD.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and  CONVERT(DATE,TSPL_JWO_SRN_HEAD.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' "
            Dim wherDateSRNRet As String = " and CONVERT(DATE,TSPL_JWO_SRN_RETURN.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and  CONVERT(DATE,TSPL_JWO_SRN_RETURN.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' "
            Dim wher As String = " Where 2=2 "
            Dim strLocCond As String = ""
            Dim strWhrCatg As String = ""
            Dim LocationFirstTime As Integer = 0
            Dim LocationAddress As String = String.Empty
            If rbtnLocationSelect.IsChecked Then
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To gvLocation.RowCount - 1
                    If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                        LocationFirstTime += 1
                        If LocationFirstTime = 1 Then
                            LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) & "'")
                        End If
                        If IsApplicable Then
                            strWhrCatg += " Or "
                        End If
                        strWhrCatg += " ((tspl_location_master.Main_Location_Code) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                        IsApplicable = True
                        Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            strWhrCatg += " and tspl_location_master.Location_Code in ("
                            Dim isFirstTime As Boolean = True
                            For Each strInn As String In arr.Keys
                                If Not isFirstTime Then
                                    strWhrCatg += ","
                                End If
                                strWhrCatg += "'" + strInn + "'"
                                isFirstTime = False
                            Next
                            strWhrCatg += ")"
                        End If
                    End If
                Next
                If Not IsApplicable Then
                    Throw New Exception("Please select at least one location")
                End If
                wher += " and (" + strWhrCatg + ")"
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strVendor = clsCommon.GetMulcallString(txtVendor.arrValueMember)
                wher = wher + " and TSPL_JWO_SRN_HEAD.Vendor_Code in (" & strVendor & ")"
            End If

            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                strVendorGroup = clsCommon.GetMulcallString(txtCustGroup.arrValueMember)
                wher = wher + " and TSPL_VENDOR_MASTER.Vendor_Group_Code in (" & strVendorGroup & ")"
            End If

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                strItem = clsCommon.GetMulcallString(txtItem.arrValueMember)
                wher = wher + " and TSPL_JWO_SRN_DETAIL.Item_Code in (" & strItem & ")"
            End If

            If IsPrint = Exporter.Refresh Then
                mainQry = " Select 'SRN' as [Transaction], TSPL_JWO_SRN_HEAD.Document_No,convert(varchar,TSPL_JWO_SRN_HEAD.Document_Date,103) as Document_Date , TSPL_JWO_SRN_HEAD.Document_Type, TSPL_JWO_SRN_HEAD.Loc_Code,TSPL_LOCATION_MASTER_From_Loc.Location_Desc,TSPL_JWO_SRN_HEAD.Job_Loc_Code,TSPL_LOCATION_MASTER.Location_Desc as Job_Location_Desc , TSPL_JWO_SRN_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name , TSPL_VENDOR_MASTER.Vendor_Group_Code,TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc ,TSPL_VENDOR_MASTER.GSTFinalNo ,TSPL_STATE_MASTER_Vendor.GST_STATE_Code , TSPL_STATE_MASTER_Vendor.STATE_NAME  ,TSPL_JWO_SRN_HEAD.Challan_No , convert (varchar,TSPL_JWO_SRN_HEAD.Challan_Date,103) as Challan_Date  , TSPL_JWO_SRN_HEAD.Tanker_No , TSPL_JWO_SRN_HEAD.Gate_Entry_No , TSPL_JWO_SRN_HEAD.Gate_Entry_Date , TSPL_JWO_SRN_HEAD.Unloading_No , TSPL_JWO_SRN_DETAIL.SNo , TSPL_JWO_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_ITEM_MASTER.HSN_Code ,TSPL_JWO_SRN_DETAIL.UOM , TSPL_JWO_SRN_DETAIL.Gross_Weight, TSPL_JWO_SRN_DETAIL.Tare_Weight , TSPL_JWO_SRN_DETAIL.Net_Weight , TSPL_JWO_SRN_DETAIL.Qty,TSPL_JWO_SRN_DETAIL.FAT_Per , TSPL_JWO_SRN_DETAIL.SNF_Per, TSPL_JWO_SRN_DETAIL.FAT_KG , TSPL_JWO_SRN_DETAIL.SNF_KG, TSPL_JWO_SRN_DETAIL.Job_Price_code ,TSPL_JWO_SRN_DETAIL.Job_Rate, TSPL_JWO_SRN_DETAIL.Job_Amount , case when TSPL_JWO_SRN_HEAD.Posted =1 then 'Approved' else 'Pending' end as  [Status]  from TSPL_JWO_SRN_DETAIL left outer join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_HEAD.Document_No =TSPL_JWO_SRN_DETAIL.Document_No left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_From_Loc on TSPL_LOCATION_MASTER_From_Loc.Location_Code = TSPL_JWO_SRN_HEAD.Loc_Code  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_JWO_SRN_HEAD.Job_Loc_Code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_JWO_SRN_HEAD.Vendor_Code left outer join TSPL_ITEM_MASTER on TSPL_JWO_SRN_DETAIL.item_code = TSPL_ITEM_MASTER.item_code  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Vendor on  TSPL_STATE_MASTER_Vendor.STATE_CODE = TSPL_VENDOR_MASTER.state_code " + wher + wherDateSRN + Environment.NewLine + _
                " union all " + Environment.NewLine + _
                " Select 'SRN Return' as [Transaction], TSPL_JWO_SRN_RETURN.Document_No , convert ( varchar,TSPL_JWO_SRN_RETURN.Document_Date,103) as Document_Date , TSPL_JWO_SRN_HEAD.Document_Type, TSPL_JWO_SRN_HEAD.Loc_Code,TSPL_LOCATION_MASTER_From_Loc.Location_Desc,TSPL_JWO_SRN_HEAD.Job_Loc_Code,TSPL_LOCATION_MASTER.Location_Desc as Job_Location_Desc , TSPL_JWO_SRN_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name , TSPL_VENDOR_MASTER.Vendor_Group_Code,TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc ,TSPL_VENDOR_MASTER.GSTFinalNo ,TSPL_STATE_MASTER_Vendor.GST_STATE_Code , TSPL_STATE_MASTER_Vendor.STATE_NAME  ,TSPL_JWO_SRN_HEAD.Challan_No , convert (varchar,TSPL_JWO_SRN_HEAD.Challan_Date,103) as Challan_Date  , TSPL_JWO_SRN_HEAD.Tanker_No , TSPL_JWO_SRN_HEAD.Gate_Entry_No , TSPL_JWO_SRN_HEAD.Gate_Entry_Date , TSPL_JWO_SRN_HEAD.Unloading_No , TSPL_JWO_SRN_DETAIL.SNo , TSPL_JWO_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_ITEM_MASTER.HSN_Code ,TSPL_JWO_SRN_DETAIL.UOM , TSPL_JWO_SRN_DETAIL.Gross_Weight, TSPL_JWO_SRN_DETAIL.Tare_Weight , TSPL_JWO_SRN_DETAIL.Net_Weight ,-1* TSPL_JWO_SRN_DETAIL.Qty as Qty,TSPL_JWO_SRN_DETAIL.FAT_Per , TSPL_JWO_SRN_DETAIL.SNF_Per, -1*TSPL_JWO_SRN_DETAIL.FAT_KG as FAT_KG, -1*TSPL_JWO_SRN_DETAIL.SNF_KG as SNF_KG, TSPL_JWO_SRN_DETAIL.Job_Price_code ,TSPL_JWO_SRN_DETAIL.Job_Rate,-1* TSPL_JWO_SRN_DETAIL.Job_Amount as Job_Amount , 'Approved' as  [Status]  " + Environment.NewLine + _
                " from TSPL_JWO_SRN_DETAIL" + Environment.NewLine + _
                " left outer join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_HEAD.Document_No =TSPL_JWO_SRN_DETAIL.Document_No " + Environment.NewLine + _
                " inner join TSPL_JWO_SRN_RETURN on TSPL_JWO_SRN_RETURN.JWO_SRN_No=TSPL_JWO_SRN_HEAD.Document_No" + Environment.NewLine + _
                " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_From_Loc on TSPL_LOCATION_MASTER_From_Loc.Location_Code = TSPL_JWO_SRN_HEAD.Loc_Code  " + Environment.NewLine + _
                " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_JWO_SRN_HEAD.Job_Loc_Code  " + Environment.NewLine + _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_JWO_SRN_HEAD.Vendor_Code " + Environment.NewLine + _
                " left outer join TSPL_ITEM_MASTER on TSPL_JWO_SRN_DETAIL.item_code = TSPL_ITEM_MASTER.item_code  " + Environment.NewLine + _
                " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Vendor on  TSPL_STATE_MASTER_Vendor.STATE_CODE = TSPL_VENDOR_MASTER.state_code" + Environment.NewLine + _
               wher + wherDateSRNRet

                If rdbSummary.IsChecked = True Then
                    Qry = " select * from ( select max([Transaction]) as [Transaction], xxx.Document_No as [Document No] , max( xxx.Document_Date) as [Document Date], max(xxx.Document_Type) as [Document Type] , max(xxx.Loc_Code)  as [Locoaction Code],max(xxx.Location_Desc) as [Location Desc]  , max(xxx.Job_Loc_Code)  as [Job Location Code],max(xxx.Job_Location_Desc) as [Job Location Desc] , max(xxx.Vendor_Code) as [Vendor Code] ,max(xxx.Vendor_Name) as [Vendor Name],max(xxx.Vendor_Group_Code)  as [Vendor Group Code],max(xxx.Vendor_Group_Code_Desc) as [Vendor Group Code Desc],max(xxx.GSTFinalNo) as [Vendor GSTNO],max(xxx.GST_STATE_Code) as [Vendor GST State Code],max(xxx.STATE_NAME) as [Vendor State Name] , max(xxx.Challan_No) as [Challan No] , max(xxx.Challan_Date) as [Challan Date] ,max(xxx.Tanker_No) as [Tanker No] , max(xxx.Gate_Entry_No) as [Gate Entry No] , max(xxx.Gate_Entry_Date) as [Gate Entry Date] , max(xxx.Unloading_No) as [Unloading No] , sum (xxx.Job_Amount) as [Total Job Charges],max(xxx.Status) as Status from (  " + mainQry + "  ) xxx group by xxx.Document_No  ) yyyy  order by convert (datetime, yyyy.[Document Date],103), yyyy.[Document No]  "
                End If
                If rdbDetail.IsChecked = True Then
                    Qry = " select * from ( select max([Transaction]) as [Transaction],xxx.Document_No as [Document No] , max( xxx.Document_Date) as [Document Date], max(xxx.Document_Type) as [Document Type] , max(xxx.Loc_Code)  as [Locoaction Code],max(xxx.Location_Desc) as [Location Desc]  , max(xxx.Job_Loc_Code)  as [Job Location Code],max(xxx.Job_Location_Desc) as [Job Location Desc] , max(xxx.Vendor_Code) as [Vendor Code],max(xxx.Vendor_Name) as [Vendor Name],max(xxx.Vendor_Group_Code)  as [Vendor Group Code],max(xxx.Vendor_Group_Code_Desc) as [Vendor Group Code Desc],max(xxx.GSTFinalNo) as [Vendor GSTNO],max(xxx.GST_STATE_Code) as [Vendor GST State Code],max(xxx.STATE_NAME) as [Vendor State Name]  , max(xxx.Challan_No) as [Challan No] , max(xxx.Challan_Date) as [Challan Date] ,max(xxx.Tanker_No) as [Tanker No] , max(xxx.Gate_Entry_No) as [Gate Entry No] , max(xxx.Gate_Entry_Date) as [Gate Entry Date] , max(xxx.Unloading_No) as [Unloading No] , xxx.Item_Code as [Item Code],max(xxx.Item_Desc) as [Item Desc], max(xxx.HSN_Code) as [HSN Code] , max(xxx.UOM) as UOM , max(xxx.Gross_Weight) as [Gross Weight] , max(xxx.Tare_Weight) as [Tare Weight] , max(xxx.Net_Weight) as [Net Weight] , max(xxx.Qty) as Qty , max(xxx.FAT_Per) as [FAT %] , max(xxx.SNF_Per) as [SNF %] , max(xxx.FAT_KG) as [FAT KG] , max(xxx.SNF_KG) as [SNF KG]  ,max(xxx.Job_Rate) as [Job Work Charges], max(xxx.Job_Amount) as [Total Job Charges] ,max(xxx.Status) as Status from ( " + mainQry + " ) xxx group by xxx.Document_No , xxx.Item_Code ) yyyy  order by convert (datetime, yyyy.[Document Date],103), yyyy.[Document No] "
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    gv.MasterTemplate.SummaryRowsBottom.Clear()
                    gv.DataSource = Nothing
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else

                    gv.DataSource = Nothing
                    gv.Columns.Clear()
                    gv.Rows.Clear()
                    gv.GroupDescriptors.Clear()
                    gv.MasterTemplate.SummaryRowsBottom.Clear()
                    gv.EnableFiltering = True
                    gv.BestFitColumns()
                    gv.DataSource = dt

                    SetGridFormation()
                    ReStoreGridLayout()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If

                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strTemp As String = ""
                arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            ElseIf IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Job Work Charges Report", gv, Nothing, Me.Text)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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

    Sub SetGridFormation()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        For Each col As GridViewColumn In gv.Columns
            If clsCommon.CompairString(col.Name, "Total Job Charges") = CompairStringResult.Equal Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                'ElseIf clsCommon.CompairString(col.Name, "Qty") = CompairStringResult.Equal Then
                '    Dim item2 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                '    summaryRowItem.Add(item2)
            End If
        Next
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'For Each col As GridViewColumn In gv.Columns
        '    col.BestFit()
        'Next
    End Sub

    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)
    '    ' Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "  'where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y'
    '    Dim qry As String = " select distinct xyz.Code , xyz.Name  from (select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y' union  select Main_Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where Location_Code in (select Location_Code  from TSPL_LOCATION_MASTER  where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y'))xyz  "
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    'End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        Dim qry As String = " SELECT Vendor_Code as Code,Vendor_Name as Name FROM TSPL_VENDOR_MASTER  WHERE Vendor_Code IN(select Jobwork_Vendor from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y')  " 'WHERE Vendor_Code IN(select Jobwork_Vendor FROM TSPL_LOCATION_MASTER WHERE LOCATION_CODE IN(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + "))
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
        'Else
        '    Dim qry As String = " SELECT Vendor_Code as Code,Vendor_Name as Name FROM TSPL_VENDOR_MASTER WHERE Vendor_Code IN(select Jobwork_Vendor from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y')"
        '    txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
        ' End If

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        'If (gv.Rows.Count <= 0) Then
        '    common.clsCommon.MyMessageBoxShow("No Data To Export")
        '    Exit Sub
        'End If
        'LoadData(Exporter.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If (gv.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("No Data To Export")
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmJobworkChargesReport & "'"))

            If Not IsNothing(txtItem.arrValueMember) Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If Not IsNothing(txtCustGroup.arrValueMember) Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            End If
            If Not IsNothing(txtVendor.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Job Work Charges Report", gv, arrHeader, "Job Work Charges Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs)
    '    Try
    '        If gv.Rows.Count > 0 Then
    '            Dim strDoc As String = Nothing
    '            strDoc = gv.CurrentRow.Cells("Document No").Value
    '            If clsCommon.myLen(strDoc) > 0 Then
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, strDoc)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
        ' FrmPendingRequisitionQty.SetDiplayMember(txtItem, "Item_Desc", "TSPL_ITEM_MASTER", "Item_Code")
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_desc as Name from TSPL_VENDOR_group"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
        'FrmPendingRequisitionQty.SetDiplayMember(txtCustGroup, "Group_desc", "TSPL_VENDOR_group", "Ven_Group_Code")
    End Sub


    Private Sub gv_DoubleClick(sender As Object, e As EventArgs) Handles gv.DoubleClick
        Try
            If gv.Rows.Count > 0 Then
                Dim strDoc As String = Nothing
                Dim strTrans As String = Nothing
                strDoc = gv.CurrentRow.Cells("Document No").Value
                strTrans = gv.CurrentRow.Cells("Transaction").Value
                If clsCommon.myLen(strDoc) > 0 Then
                    If clsCommon.CompairString(strTrans, "SRN") = CompairStringResult.Equal Then
                        '  clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, strDoc)
                    Else
                        ' clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN_Return, strDoc)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnLocationSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub
    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub
    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub

    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 4
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
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

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class

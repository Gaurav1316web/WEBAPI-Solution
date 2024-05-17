Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmJobworkTransfer
    Inherits FrmMainTranScreen
    'Private Const ReportID As String = "TransferRegister"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
    Dim qry As String
    Public arrLoc As Dictionary(Of String, Object) = Nothing
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmTransferRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport

    End Sub

    Private Sub FrmTransferRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = fromDate.Value
        'LoadLocation()
        LoadLocation2()
        gv.AllowEditRow = False
        gv.AllowDragToGroup = False
        gv.AllowAddNewRow = False
        RadPageView1.SelectedPage = RadPageViewPage1
        '------------------------------------------------------------
        'If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
        '    rbtnLocationSelect.IsChecked = True
        '    For Each str As String In arrLoc.Keys
        '        For ii As Integer = 0 To gvLocation.RowCount - 1
        '            If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
        '                gvLocation.Rows(ii).Cells("SEL").Value = True
        '                gvLocation.Rows(ii).Tag = arrLoc(str)
        '            End If
        '        Next
        '    Next
        'End If
        '--------------------------------------------------------
        rbtnLocationAll.IsChecked = True
    End Sub
    Sub LoadLocation2()
        gvLocation.DataSource = Nothing
        'Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"
        Dim whrCls As String = " and Location_Code in (select Main_Location_Code from TSPL_LOCATION_MASTER where Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical')"
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""
        'If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
        '    qry = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,Jobwork_Vendor,Is_Sub_Location from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'"
        'End If
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
    'Sub LoadLocation()
    '    'qry = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER"
    '    qry = " select distinct xyz.Code , xyz.Name as Description  from (select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y' union  select Main_Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where Location_Code in (select Location_Code  from TSPL_LOCATION_MASTER  where TSPL_LOCATION_MASTER.Is_Jobwork=1 and TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER.Is_Sub_Location='Y'))xyz "
    '    cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgLocation.ValueMember = "Code"
    '    cbgLocation.DisplayMember = "Description"
    '    cbgLocation.CheckedAll()
    'End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadData(ByVal IsPrint As Exporter)
        Try
            'If gvLocation.CheckedValue.Count <= 0 Then
            '    Throw New Exception("Select atleast single location.")
            'End If
            '========================================================================================================================================
            'Dim strWhrCatg As String = ""
            'Dim strWhrSubCag As String = ""
            'Dim LocationFirstTime As Integer = 0
            'Dim LocationAddress As String = String.Empty
            Dim locationCode_value As String = ""
            Dim sublocationCode_Value As String = ""

            'If rbtnLocationSelect.IsChecked Then
            '    Dim IsApplicable As Boolean = False
            '    For ii As Integer = 0 To gvLocation.RowCount - 1
            '        If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
            '            LocationFirstTime += 1
            '            If LocationFirstTime = 1 Then
            '                LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) & "'")
            '            End If
            '            If IsApplicable Then
            '                strWhrCatg += " , "
            '            End If
            '            strWhrCatg += "  '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "'  "
            '            IsApplicable = True
            '            Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
            '            If arr IsNot Nothing AndAlso arr.Count > 0 Then

            '                Dim isFirstTime As Boolean = True
            '                For Each strInn As String In arr.Keys
            '                    If Not isFirstTime Then
            '                        strWhrSubCag += ","
            '                    End If
            '                    strWhrSubCag += "'" + strInn + "'"
            '                    isFirstTime = False
            '                Next


            '            End If
            '        End If
            '    Next
            '    If Not IsApplicable Then
            '        Throw New Exception("Please select at least one location")
            '    End If

            '    If clsCommon.myLen(strWhrCatg) > 0 Then
            '        locationCode_value = " and TSPL_MILK_JOBWORK_TRANSFER_HEAD.Loc_Code in (" & strWhrCatg & ") "
            '        If clsCommon.myLen(strWhrSubCag) > 0 Then
            '            sublocationCode_Value = " and TSPL_MILK_JOBWORK_TRANSFER_HEAD.jobWork_location(" & strWhrSubCag & ") "
            '        End If
            '    End If



            'End If

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
                        'strWhrCatg += " ((case when tspl_location_master.Is_Section='N' and tspl_location_master.Is_Sub_Location='N' then tspl_location_master.Location_Code else tspl_location_master.Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
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
                strLocCond += " and (" + strWhrCatg + ")"
            End If




            '=========================================================================================================================================




            If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then

                qry = "  select * from ( " & _
                "  select TransType as [Transaction Type], max (Final.[From Location])  as [From Location], MAX (Final.[From Location Description]) as [From Location Description] ,max(Final.[From Location Address]) as [From Location Address] , max(Final.[From Location GSTIN]) as [From Location GSTIN] , max(Final.[From Location GST State]) as [From Location GST State] , max(Final.[From Location State Name]) as [From Location State Name], max(Final.[To Location]) as [To Location],max([To Location Description]) as [To Location Description] , max(Final.[To Location Address]) as [To Location Address] ,max(Final.[To Location GSTIN]) as [To Location GSTIN] , max(Final.[To Location GST State]) as  [To Location GST State], max(Final.[To Location State Name])  as [To Location State Name], max(Final.[Vendor Code]) as [Vendor Code] ,max(Final.[Vendor Name]) as [Vendor Name] ,max(Final.[SRN No]) as [SRN No],max(Final.[Tanker/Vehicle No]) as [Tanker/Vehicle No] ,(Final.[Document Code]) as [Document Code] , max(Final.[Document Date] ) as [Document Date], (Final.Item_Code) as [Item Code]  ,max(Final.Item_Desc) as [Item Desc],max(Final.HSN_Code) as [HSN Code] ,max(Final.UOM) as [UOM], max(Final.Net_Weight) as [Qty] , max(Final.snf_Per) as [SNF %] , max(Final.fat_per) as [FAT %] ,max(Final.fat_KG) as [FAT KG],max(Final.SNF_KG) as [SNF KG] ,max( Final.fat_Rate) as [FAT Rate],max( Final.SNF_Rate) as [SNF Rate]  ,max(Final.BasicRate) as [Basic Rate],max(Final.StandardRate) as [Standard Rate],max(Final.NetRate) as [Rate],max(Final.FormType) as [Form Type], max (Final.FatAmt) as [Fat Amt],max (Final.SnfAmt) as [SNF Amt],max(Final.FinalMilkRate) as [Final Milk Rate],max(Final.Milk_Transfer_In) as [Milk Transfer In] ,max(Final.Doc_Type) as [Doc Type],max(Final.Manual_Standard_Rate) as [Manual Standard Rate] , sum (Final.[Total Amount]) as [Total Amount],max(Final.Status) as Status from (  " & _
                "  select 'Transfer-Milk' as TransType, TSPL_MILK_JOBWORK_TRANSFER_HEAD.Loc_Code as [From Location] ,Location_Code_From_Loc.Location_Desc as [From Location Description],Location_Code_From_Loc.Add1 + case when len (Location_Code_From_Loc.Add2) > 0 then  ','+ Location_Code_From_Loc.Add2 else '' end +  case when len (Location_Code_From_Loc.Add3) > 0 then  ','+ Location_Code_From_Loc.Add3 else '' end +  case when len (Location_Code_From_Loc.Add4) > 0 then  ','+ Location_Code_From_Loc.Add4 else ''   end as   [From Location Address],Location_Code_From_Loc.GSTNO as [From Location GSTIN],TSPL_STATE_MASTER_From_Loc.GST_STATE_Code as [From Location GST State] , TSPL_STATE_MASTER_From_Loc.State_Name as [From Location State Name] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.JobWork_location  as  [To Location] , TSPL_LOCATION_MASTER.Location_Desc as [To Location Description],TSPL_LOCATION_MASTER.Add1 + case when len (TSPL_LOCATION_MASTER.Add2) > 0 then  ','+ TSPL_LOCATION_MASTER.Add2 else '' end + case when len (TSPL_LOCATION_MASTER.Add3) > 0 then  ','+ TSPL_LOCATION_MASTER.Add3 else '' end +   case when len (TSPL_LOCATION_MASTER.Add4) > 0 then  ','+ TSPL_LOCATION_MASTER.Add4 else ''   end as   [To Location Address],TSPL_LOCATION_MASTER.GSTNO as [To Location GSTIN],TSPL_STATE_MASTER_To_Loc.GST_STATE_Code as [To Location GST State] , TSPL_STATE_MASTER_To_Loc.State_Name as [To Location State Name],TSPL_MILK_JOBWORK_TRANSFER_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],TSPL_MILK_JOBWORK_TRANSFER_HEAD.SRN_NO as [SRN No],TSPL_MILK_JOBWORK_TRANSFER_HEAD.Tanker_No as [Tanker/Vehicle No],TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code as [Document Code],convert(varchar, TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date,103) as [Document Date] ,   TSPL_MILK_JOBWORK_TRANSFER_HEAD.Item_Code ,tspl_item_master.Item_Desc,tspl_item_master.HSN_Code ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.UOM , TSPL_MILK_JOBWORK_TRANSFER_HEAD.Net_Weight , TSPL_MILK_JOBWORK_TRANSFER_HEAD.snf_Per , TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_per ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_KG,TSPL_MILK_JOBWORK_TRANSFER_HEAD.SNF_KG , TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_Rate, TSPL_MILK_JOBWORK_TRANSFER_HEAD.SNF_Rate ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.BasicRate,TSPL_MILK_JOBWORK_TRANSFER_HEAD.StandardRate,TSPL_MILK_JOBWORK_TRANSFER_HEAD.NetRate,TSPL_MILK_JOBWORK_TRANSFER_HEAD.FormType, TSPL_MILK_JOBWORK_TRANSFER_HEAD.FatAmt,TSPL_MILK_JOBWORK_TRANSFER_HEAD.SnfAmt,TSPL_MILK_JOBWORK_TRANSFER_HEAD.FinalMilkRate,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Milk_Transfer_In ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Doc_Type,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Manual_Standard_Rate, TSPL_MILK_JOBWORK_TRANSFER_HEAD.Actual_Amount as [Total Amount] ,case when TSPL_MILK_JOBWORK_TRANSFER_HEAD.isPosted =1 then 'Approved' else 'Pending' end as  Status " & _
                "  from TSPL_MILK_JOBWORK_TRANSFER_HEAD  " & _
                "  left outer join TSPL_LOCATION_MASTER as Location_Code_From_Loc on Location_Code_From_Loc.Location_Code =TSPL_MILK_JOBWORK_TRANSFER_HEAD.Loc_Code " & _
                "  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_JOBWORK_TRANSFER_HEAD.JobWork_location " & _
                "  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_JOBWORK_TRANSFER_HEAD.Vendor_Code " & _
                "  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_Loc on TSPL_STATE_MASTER_From_Loc.state_Code =Location_Code_From_Loc.State " & _
                "  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_To_Loc on TSPL_STATE_MASTER_To_Loc.state_Code =TSPL_LOCATION_MASTER.State " & _
                "  left outer join tspl_item_master on tspl_item_master.Item_Code = TSPL_MILK_JOBWORK_TRANSFER_HEAD.Item_Code " & _
                " WHERE CONVERT(DATE,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                " AND CONVERT(DATE,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                " " + strLocCond + "  " & _
                "  union all " & _
                " select 'Transfer Return-Milk' as TransType, TSPL_MILK_JOBWORK_TRANSFER_HEAD.Loc_Code as [From Location] ,Location_Code_From_Loc.Location_Desc as [From Location Description]," & _
                " Location_Code_From_Loc.Add1 + case when len (Location_Code_From_Loc.Add2) > 0 then  ','+ Location_Code_From_Loc.Add2 else '' end +  case when len (Location_Code_From_Loc.Add3) > 0 then  ','+ Location_Code_From_Loc.Add3 else '' end +  case when len (Location_Code_From_Loc.Add4) > 0 then  ','+ Location_Code_From_Loc.Add4 else ''   end as   [From Location Address]," & _
                " Location_Code_From_Loc.GSTNO as [From Location GSTIN],TSPL_STATE_MASTER_From_Loc.GST_STATE_Code as [From Location GST State] , TSPL_STATE_MASTER_From_Loc.State_Name as [From Location State Name] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.JobWork_location  as  [To Location] , TSPL_LOCATION_MASTER.Location_Desc as [To Location Description],TSPL_LOCATION_MASTER.Add1 + case when len (TSPL_LOCATION_MASTER.Add2) > 0 then  ','+ TSPL_LOCATION_MASTER.Add2 else '' end + case when len (TSPL_LOCATION_MASTER.Add3) > 0 then  ','+ TSPL_LOCATION_MASTER.Add3 else '' end +   case when len (TSPL_LOCATION_MASTER.Add4) > 0 then  ','+ TSPL_LOCATION_MASTER.Add4 else ''   end as   [To Location Address],TSPL_LOCATION_MASTER.GSTNO as [To Location GSTIN],TSPL_STATE_MASTER_To_Loc.GST_STATE_Code as [To Location GST State] , TSPL_STATE_MASTER_To_Loc.State_Name as [To Location State Name],TSPL_MILK_JOBWORK_TRANSFER_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],TSPL_MILK_JOBWORK_TRANSFER_HEAD.SRN_NO as [SRN No],TSPL_MILK_JOBWORK_TRANSFER_HEAD.Tanker_No as [Tanker/Vehicle No],TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_No as [Document Code],convert(varchar, TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_Date,103) as [Document Date] ,   TSPL_MILK_JOBWORK_TRANSFER_HEAD.Item_Code ,tspl_item_master.Item_Desc,tspl_item_master.HSN_Code ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.UOM , TSPL_MILK_JOBWORK_TRANSFER_HEAD.Net_Weight*-1 as Net_Weight , TSPL_MILK_JOBWORK_TRANSFER_HEAD.snf_Per , TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_per ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_KG*-1 as fat_KG,TSPL_MILK_JOBWORK_TRANSFER_HEAD.SNF_KG*-1 as SNF_KG , TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_Rate, TSPL_MILK_JOBWORK_TRANSFER_HEAD.SNF_Rate ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.BasicRate,TSPL_MILK_JOBWORK_TRANSFER_HEAD.StandardRate,TSPL_MILK_JOBWORK_TRANSFER_HEAD.NetRate,TSPL_MILK_JOBWORK_TRANSFER_HEAD.FormType, TSPL_MILK_JOBWORK_TRANSFER_HEAD.FatAmt*-1 as FatAmt,TSPL_MILK_JOBWORK_TRANSFER_HEAD.SnfAmt*-1 as SnfAmt,TSPL_MILK_JOBWORK_TRANSFER_HEAD.FinalMilkRate,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Milk_Transfer_In ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Doc_Type,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Manual_Standard_Rate, TSPL_MILK_JOBWORK_TRANSFER_HEAD.Actual_Amount*-1 as [Total Amount] ,case when TSPL_MILK_JOBWORK_TRANSFER_HEAD.isPosted =1 then 'Approved' else 'Pending' end as  Status   from TSPL_MILK_JOBWORK_TRANSFER_RETURN " & _
                " left outer join TSPL_MILK_JOBWORK_TRANSFER_HEAD  on  TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code=TSPL_MILK_JOBWORK_TRANSFER_RETURN.JWO_Transfer_No " & _
                " left outer join TSPL_LOCATION_MASTER as Location_Code_From_Loc on Location_Code_From_Loc.Location_Code =TSPL_MILK_JOBWORK_TRANSFER_HEAD.Loc_Code   " & _
                " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_JOBWORK_TRANSFER_HEAD.JobWork_location " & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_JOBWORK_TRANSFER_HEAD.Vendor_Code " & _
                " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_Loc on TSPL_STATE_MASTER_From_Loc.state_Code =Location_Code_From_Loc.State  " & _
                " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_To_Loc on TSPL_STATE_MASTER_To_Loc.state_Code =TSPL_LOCATION_MASTER.State " & _
                " left outer join tspl_item_master on tspl_item_master.Item_Code = TSPL_MILK_JOBWORK_TRANSFER_HEAD.Item_Code " & _
                " WHERE CONVERT(DATE,TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                " AND CONVERT(DATE,TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                " " + strLocCond + "  " & _
                " union all " + Environment.NewLine + _
                " Select 'Transfer-Other' as TransType,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction as [From Location] ,Location_Code_From_Loc.Location_Desc as [From Location Description],Location_Code_From_Loc.Add1 + case when len (Location_Code_From_Loc.Add2) > 0 then  ','+ Location_Code_From_Loc.Add2 else '' end +  case when len (Location_Code_From_Loc.Add3) > 0 then  ','+ Location_Code_From_Loc.Add3 else '' end +  case when len (Location_Code_From_Loc.Add4) > 0 then  ','+ Location_Code_From_Loc.Add4 else ''   end as   [From Location Address] ,Location_Code_From_Loc.GSTNO as [From Location GSTIN],TSPL_STATE_MASTER_From_Loc.GST_STATE_Code as [From Location GST State] , TSPL_STATE_MASTER_From_Loc.State_Name as [From Location State Name],TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction as  [To Location] ,TSPL_LOCATION_MASTER.Location_Desc as [To Location Description],TSPL_LOCATION_MASTER.Add1 + case when len (TSPL_LOCATION_MASTER.Add2) > 0 then  ','+ TSPL_LOCATION_MASTER.Add2 else '' end + case when len (TSPL_LOCATION_MASTER.Add3) > 0 then  ','+ TSPL_LOCATION_MASTER.Add3 else '' end +   case when len (TSPL_LOCATION_MASTER.Add4) > 0 then  ','+ TSPL_LOCATION_MASTER.Add4 else ''   end as   [To Location Address],TSPL_LOCATION_MASTER.GSTNO as [To Location GSTIN],TSPL_STATE_MASTER_To_Loc.GST_STATE_Code as [To Location GST State] , TSPL_STATE_MASTER_To_Loc.State_Name as [To Location State Name] ,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],'' as [SRN No] ,TSPL_VEHICLE_MASTER.Description  as [Tanker/Vehicle No],TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO  as [Document Code],convert(varchar, TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_DATE,103) as [Document Date] , TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.Item_Code,tspl_item_master.Item_Desc,tspl_item_master.HSN_Code,TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.UOM, TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.Qty,'' as snf_Per , '' as fat_per , '' as fat_KG,'' as SNF_KG , '' as  fat_Rate, '' as SNF_Rate ,'' as BasicRate, '' as StandardRate,TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.Rate , '' as FormType, '' as FatAmt,'' as SnfAmt,'' as FinalMilkRate,'' as Milk_Transfer_In ,'' as Doc_Type,'' as Manual_Standard_Rate   , TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Amount as [Total Amount],case when TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Status=1 then 'Approved' else 'Pending' end as Status " & _
                " from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS " & _
                " left outer join TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO = TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO " & _
                " left outer join TSPL_LOCATION_MASTER as Location_Code_From_Loc on Location_Code_From_Loc.Location_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction " & _
                " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction " & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vendor_Code " & _
                " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_Loc on TSPL_STATE_MASTER_From_Loc.state_Code =Location_Code_From_Loc.State " & _
                " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_To_Loc on TSPL_STATE_MASTER_To_Loc.state_Code =TSPL_LOCATION_MASTER.State " & _
                " left outer join tspl_item_master on tspl_item_master.Item_Code = TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.Item_Code " & _
                " left outer join TSPL_VEHICLE_MASTER on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                " WHERE  " & _
                " CONVERT(DATE,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_DATE,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                " AND CONVERT(DATE,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_DATE,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                " " + strLocCond + "  " & _
                " union all " + Environment.NewLine + _
                " Select 'Transfer Return - Other' as TransType,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction as [From Location] ,Location_Code_From_Loc.Location_Desc as [From Location Description],Location_Code_From_Loc.Add1 + case when len (Location_Code_From_Loc.Add2) > 0 then  ','+ Location_Code_From_Loc.Add2 else '' end +  case when len (Location_Code_From_Loc.Add3) > 0 then  ','+ Location_Code_From_Loc.Add3 else '' end +  case when len (Location_Code_From_Loc.Add4) > 0 then  ','+ Location_Code_From_Loc.Add4 else ''   end as   [From Location Address] ,Location_Code_From_Loc.GSTNO as [From Location GSTIN],TSPL_STATE_MASTER_From_Loc.GST_STATE_Code as [From Location GST State] , TSPL_STATE_MASTER_From_Loc.State_Name as [From Location State Name],TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction as  [To Location] ,TSPL_LOCATION_MASTER.Location_Desc as [To Location Description],TSPL_LOCATION_MASTER.Add1 + case when len (TSPL_LOCATION_MASTER.Add2) > 0 then  ','+ TSPL_LOCATION_MASTER.Add2 else '' end + case when len (TSPL_LOCATION_MASTER.Add3) > 0 then  ','+ TSPL_LOCATION_MASTER.Add3 else '' end +   case when len (TSPL_LOCATION_MASTER.Add4) > 0 then  ','+ TSPL_LOCATION_MASTER.Add4 else ''   end as   [To Location Address],TSPL_LOCATION_MASTER.GSTNO as [To Location GSTIN],TSPL_STATE_MASTER_To_Loc.GST_STATE_Code as [To Location GST State] , TSPL_STATE_MASTER_To_Loc.State_Name as [To Location State Name] ,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],'' as [SRN No] ,TSPL_VEHICLE_MASTER.Description  as [Tanker/Vehicle No],TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_No as [Document Code],convert(varchar, TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_Date,103) as [Document Date] , TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.Item_Code,tspl_item_master.Item_Desc,tspl_item_master.HSN_Code,TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.UOM,-1 * TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.Qty as Qty,'' as snf_Per , '' as fat_per , '' as fat_KG,'' as SNF_KG , '' as  fat_Rate, '' as SNF_Rate ,'' as BasicRate, '' as StandardRate,TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.Rate , '' as FormType, '' as FatAmt,'' as SnfAmt,'' as FinalMilkRate,'' as Milk_Transfer_In ,'' as Doc_Type,'' as Manual_Standard_Rate   ,-1* TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Amount as [Total Amount],'Approved' as Status " & _
                " from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS " & _
                " left outer join TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO = TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO " & _
                " inner join TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN on TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.JWO_Transfer_No=TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO " + _
                " left outer join TSPL_LOCATION_MASTER as Location_Code_From_Loc on Location_Code_From_Loc.Location_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction " & _
                " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction " & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vendor_Code " & _
                " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_Loc on TSPL_STATE_MASTER_From_Loc.state_Code =Location_Code_From_Loc.State " & _
                " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_To_Loc on TSPL_STATE_MASTER_To_Loc.state_Code =TSPL_LOCATION_MASTER.State " & _
                " left outer join tspl_item_master on tspl_item_master.Item_Code = TSPL_JOB_WORK_OUTWARD_TRANSFER_Details.Item_Code " & _
                " left outer join TSPL_VEHICLE_MASTER on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                " WHERE  " & _
                " CONVERT(DATE,TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                " AND CONVERT(DATE,TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                " " + strLocCond + "  " & Environment.NewLine + _
                " ) Final Group by Final.TransType,Final.[Document Code], Final.Item_Code " & _
                " ) XXX ORDER BY convert(date,XXX.[Document Date] ,103), xxx.[Document Code] "



                dt = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                Else
                    gv.DataSource = Nothing
                    gv.Columns.Clear()
                    gv.Rows.Clear()
                    gv.GroupDescriptors.Clear()
                    gv.MasterTemplate.SummaryRowsBottom.Clear()
                    gv.EnableFiltering = True

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
                clsCommon.MyExportToExcelGrid(" Job Work Transfer", gv, Nothing, Me.Text)
            Else
                clsCommon.MyExportToPDF(Me.Text, gv, Nothing, Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        For Each col As GridViewColumn In gv.Columns
            If clsCommon.CompairString(col.Name, "Total Amount") = CompairStringResult.Equal Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            End If
        Next
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        For Each col As GridViewColumn In gv.Columns
            col.BestFit()
        Next
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData(Exporter.Refresh)
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = fromDate.Value
        'LoadLocation2()
        'rbtnLocationAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'If (gv.Rows.Count <= 0) Then
        '    common.clsCommon.MyMessageBoxShow("No Data To Export")
        '    Exit Sub
        'End If
        'LoadData(Exporter.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    'Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (gv.Rows.Count <= 0) Then
    '        common.clsCommon.MyMessageBoxShow("No Data To Export")
    '        Exit Sub
    '    End If
    '    LoadData(Exporter.PDF)
    'End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
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

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            If (gv.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmJobworkTransfer & "'"))

             If rbtnLocationSelect.CheckState = CheckState.Checked Then
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If grow.Cells("Sel").Value Then
                        arrHeader.Add("Location : " + clsCommon.myCstr(grow.Cells("Code").Value))
                    End If
                Next
            Else
                arrHeader.Add("Location : All")
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
                clsCommon.MyExportToPDF("Job Work Transfer", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs)
    '    Try
    '        If gv.Rows.Count > 0 Then
    '            Dim strDoc As String = Nothing
    '            strDoc = gv.CurrentRow.Cells("Document Code").Value
    '            If clsCommon.myLen(strDoc) > 0 Then
    '                Dim ckhDoctype As Double = clsDBFuncationality.getSingleValue("select count(*) from TSPL_MILK_JOBWORK_TRANSFER_HEAD where Document_Code ='" + strDoc + "'")
    '                If ckhDoctype > 0 Then
    '                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, strDoc)
    '                Else
    '                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferOther, strDoc)
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try


    'End Sub

    Private Sub gv_DoubleClick(sender As Object, e As EventArgs) Handles gv.DoubleClick
        Try
            If gv.Rows.Count > 0 Then
                Dim strDoc As String = Nothing
                Dim strDocType As String = Nothing
                strDoc = gv.CurrentRow.Cells("Document Code").Value
                strDocType = gv.CurrentRow.Cells("Transaction Type").Value
                If clsCommon.myLen(strDocType) > 0 Then
                    ' Dim ckhDoctype As Double = clsDBFuncationality.getSingleValue("select count(*) from TSPL_MILK_JOBWORK_TRANSFER_HEAD where Document_Code ='" + strDoc + "'")
                    If clsCommon.CompairString(strDocType, "Transfer-Milk") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, strDoc)
                    ElseIf clsCommon.CompairString(strDocType, "Transfer Return-Milk") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, strDoc)
                    ElseIf clsCommon.CompairString(strDocType, "Transfer Return - Other") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferOtherReturn, strDoc)
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferOther, strDoc)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

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

   
    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class

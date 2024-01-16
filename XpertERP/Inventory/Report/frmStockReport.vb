Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports System.Data
Imports common

Public Class FrmStockReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim StrPermission As String
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("STOCK-RPT+")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dr As DataTable = clsDBFuncationality.GetDataTable(sql)
        'dr = connectSql.RunSqlReturnDR(sql)
        'dr.Read()
        If dr.Rows.Count > 0 Then
            For ii As Integer = 0 To dr.Rows.Count - 1
                l1User = dr.Rows(ii)("User_Type").ToString()
                l2User = dr.Rows(ii)("Level1_Code").ToString()
                l3User = dr.Rows(ii)("Level2_Code").ToString()
                l4User = dr.Rows(ii)("Level3_Code").ToString()
                l5User = dr.Rows(ii)("Level4_Code").ToString()
            Next
        End If
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = " SELECT Segment_code as [Location], Description as [Location Description] FROM dbo.TSPL_GL_SEGMENT_CODE WHERE Seg_No=7 "
        qry += " and Segment_code in (" & StrPermission & ")"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code,Item_Desc from TSPL_ITEM_MASTER"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item_Code"
        cbgItem.DisplayMember = "Scheme Applicable"
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub

    Sub LoadMrp()
        Dim qry As String = " select distinct  CONVERT(varchar(20),MRP) as MRP ,Item_Code as Code from TSPL_INVENTORY_MOVEMENT where 2=2"
        If chkitemSelect.IsChecked = True AndAlso cbgitem.CheckedValue.Count > 0 Then
            qry += " and Item_Code in (" + clsCommon.GetMulcallString(cbgitem.CheckedValue) + ")"
        End If
        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            qry += " and Location_Code in( SELECT Location_Code FROM TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
        Else
            qry += " and Location_Code in( SELECT Location_Code FROM TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + StrPermission + "))"
        End If
        cbgmrp.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgmrp.ValueMember = "MRP"
        cbgmrp.DisplayMember = "Code"
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub FrmStockReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationSegment()
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadItem()
        chkItemAll.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        LoadMrp()

        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbFinish.IsChecked = True
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        rdbFinish.IsChecked = True
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadItem()
        chkItemAll.IsChecked = True
        LoadMrp()
        chkmrpall.IsChecked = True
        rdbSummary.IsChecked = True
        rbtnCompanyAll.IsChecked = True
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged, chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged, chkItemSelect.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim CompCode As String = ""
        Dim Locfilter As String = ""
        Dim frmCRV As New frmCrystalReportViewer()
        Dim FromDateFilter As String = fromDate.Value.ToString("dd/MM/yyyy")
        Dim TodateFIlter As String = ToDate.Value.ToString("dd/MM/yyyy")
        If rbtnCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count > 0 Then
            CompCode = clsCommon.GetMulcallString(cbgCompany.CheckedValue)
            CompCode = CompCode.Replace("'", "")
        End If
        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            Locfilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            Locfilter = Locfilter.Replace("'", "")
        End If
        If rbtnCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one company or select ALL", Me.Text)
            Return


        ElseIf chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Location or select ALL", Me.Text)
            Return


        ElseIf chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Item or select ALL", Me.Text)
            Return

        ElseIf chkmrpselect.IsChecked = True AndAlso cbgmrp.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one MRP or select ALL", Me.Text)
            Return

        End If
        ''Added By--Pankaj Kumar---21/09/12
        Dim strItemType As String = ""
        If rdbFinish.IsChecked = True Then
            strItemType = " and LEFT(" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.ItemType, 1)='F' "
        ElseIf rdbOthers.IsChecked = True Then
            strItemType = " and LEFT(" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.ItemType, 1)='O' "
        ElseIf rdbRaw.IsChecked = True Then
            strItemType = " and LEFT(" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.ItemType, 1)='R' "
        End If

        Dim strLocAll, strItemAll As String
        If chkLocationAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkItemAll.IsChecked = True Then
            strItemAll = "Y"
        Else
            strItemAll = "N"
        End If

        If rdbSummary.IsChecked = True Then

            strQuery = "SELECT  '" + FromDateFilter + "' as FromDate,'" + TodateFIlter + "' as Todate,'" + CompCode + "' as CompCode,'" + Locfilter + "' as LocFilter," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Desc, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,  isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) as Qty, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.InOut, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Entry_Date, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Net_Cost, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type, convert(date, '" & fromDate.Value & "',103) AS Fdate, " & _
                       "convert(date, '" & ToDate.Value & "',103) AS Tdate, 0 AS OP, 0 AS CB " & _
                       "FROM " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT INNER JOIN " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                       "WHERE " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Type <> 'Logical' and  convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) <= convert(date, '" & ToDate.Value & "',103) " + strItemType + ""
            Dim strQuery1 = "union all "
            Dim strQuery2 = "SELECT '" + FromDateFilter + "' as FromDate,'" + TodateFIlter + "' as Todate,'" + CompCode + "' as CompCode,'" + Locfilter + "' as LocFilter," + clsCommon.ReplicateDBString + " TSPL_INVENTORY_MOVEMENT.UOM, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Desc, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,0 as Qty, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.InOut, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Entry_Date, 0 AS Net_Cost, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type, convert(date, '" & fromDate.Value & "',103) AS Fdate, convert(date, '" & ToDate.Value & "',103) AS Tdate, " & _
                       "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type = 'Load Out' THEN " & _
                       "(CASE WHEN (SELECT  " + clsCommon.ReplicateDBString + "tspl_shipment_master.shipment_type FROM " + clsCommon.ReplicateDBString + "tspl_shipment_master " & _
                       "WHERE " + clsCommon.ReplicateDBString + "tspl_shipment_master.Shipment_No = " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No) = 'transfer' THEN 0 " & _
                       "ELSE isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) END) " & _
                       "else isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) END AS OP, 0 AS CB " & _
                       "FROM " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT INNER JOIN " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                       "WHERE " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Type <> 'Logical' and  convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) < convert(date, '" & fromDate.Value & "',103) " + strItemType + " "


            If strLocAll = "Y" Then
                strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + StrPermission + ")) "
                strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + StrPermission + ")) "
                If strItemAll = "Y" Then

                Else
                    strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                End If
            Else
                If strItemAll = "Y" Then
                    strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                Else
                    strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                End If
            End If
            '--Added by--Pankaj Kumar on ---20/09/2012
            If chkmrpselect.IsChecked Then
                strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.MRP in (" + clsCommon.GetMulcallString(cbgmrp.CheckedValue) + ") "
                strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.MRP in (" + clsCommon.GetMulcallString(cbgmrp.CheckedValue) + ") "
            End If

            Dim ArrDBName As ArrayList = Nothing
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            Dim strquery3 = strQuery & strQuery1 & strQuery2
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strquery3, ArrDBName, False)
            'strQuery2 = clsCommon.GetQueryWithAllSelectedDataBase(strQuery2, ArrDBName, False)

            frmCRV.funreport(CrystalReportFolder.InventoryReport, clsDBFuncationality.GetDataTable(strQuery), "crptStockReco", "Stock Reconciliation Report")
        Else
            Dim strQuery1 = "SELECT '" + FromDateFilter + "' as FromDate,'" + TodateFIlter + "' as Todate,'" + CompCode + "' as CompCode,'" + Locfilter + "' as LocFilter, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Desc, (" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Qty, " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.InOut, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Entry_Date, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Net_Cost, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code, " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type,'' AS Fdate, '' AS Tdate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_Type,'sale') as Shipment_Type " & _
                        "FROM " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT  INNER JOIN " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER  ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                        "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No = " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No " & _
                        "WHERE " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Type <> 'Logical' and convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) <= convert(date, '" & ToDate.Value & "',103) " + strItemType + " "
            Dim strQuery2 = "SELECT '" + FromDateFilter + "' as FromDate,'" + TodateFIlter + "' as Todate,'" + CompCode + "' as CompCode,'" + Locfilter + "' as LocFilter, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Desc, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,0 as Qty, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.InOut, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Entry_Date, 0 AS Net_Cost, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type, convert(date, '" & fromDate.Value & "',103) AS Fdate, convert(date, '" & ToDate.Value & "',103) AS Tdate, " & _
                       " CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type = 'Load Out' THEN " & _
                       "(CASE WHEN (SELECT  " + clsCommon.ReplicateDBString + "tspl_shipment_master.shipment_type FROM " + clsCommon.ReplicateDBString + "tspl_shipment_master " & _
                       "WHERE " + clsCommon.ReplicateDBString + "tspl_shipment_master.Shipment_No = " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No) = 'transfer' THEN 0 " & _
                       "ELSE isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) END) " & _
                       "else isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) END  AS OP, 0 AS CB " & _
                       "FROM " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT INNER JOIN " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                       "WHERE " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Type <> 'Logical' and  convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) < convert(date, '" & fromDate.Value & "',103) " + strItemType + " "
            Dim strQuery3 = "SELECT '" + FromDateFilter + "' as FromDate,'" + TodateFIlter + "' as Todate,'" + CompCode + "' as CompCode,'" + Locfilter + "' as LocFilter, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Desc, " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                        "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type = 'Load Out' THEN " & _
                       "(CASE WHEN (SELECT  " + clsCommon.ReplicateDBString + "tspl_shipment_master.shipment_type FROM " + clsCommon.ReplicateDBString + "tspl_shipment_master " & _
                       "WHERE " + clsCommon.ReplicateDBString + "tspl_shipment_master.Shipment_No = " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No) = 'transfer' THEN 0 " & _
                       "ELSE isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) END) " & _
                       " else isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) END  as Qty, " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.InOut, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Entry_Date, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Net_Cost, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type, convert(date, '" & fromDate.Value & "',103) AS Fdate, " & _
                        "convert(date, '" & ToDate.Value & "',103) AS Tdate, 0 AS OP, 0 AS CB " & _
                        "FROM " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT INNER JOIN " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                        "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                        "WHERE " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Type <> 'Logical' and convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) <= convert(date, '" & ToDate.Value & "',103) " + strItemType + " "
            Dim strQuery4 = "union all "
            Dim strQuery5 = "SELECT  '" + FromDateFilter + "' as FromDate,'" + TodateFIlter + "' as Todate,'" + CompCode + "' as CompCode,'" + Locfilter + "' as LocFilter, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Desc, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,0 as Qty, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.InOut, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date, " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Entry_Date, 0 AS Net_Cost, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type, convert(date, '" & fromDate.Value & "',103) AS Fdate, convert(date, '" & ToDate.Value & "',103) AS Tdate, " & _
                       " CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type = 'Load Out' THEN " & _
                       "(CASE WHEN (SELECT  " + clsCommon.ReplicateDBString + "tspl_shipment_master.shipment_type FROM " + clsCommon.ReplicateDBString + "tspl_shipment_master " & _
                       "WHERE " + clsCommon.ReplicateDBString + "tspl_shipment_master.Shipment_No = " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No) = 'transfer' THEN 0 " & _
                       "ELSE isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) END) " & _
                       "else isnull((" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) END  AS OP, 0 AS CB " & _
                       "FROM " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT INNER JOIN " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                       "" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                       "WHERE " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Type <> 'Logical' and convert(date," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103) < convert(date, '" & fromDate.Value & "',103) " + strItemType + " "


            If strLocAll = "Y" Then
                strQuery1 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + StrPermission + ")) "
                strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + StrPermission + ")) "
                strQuery3 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + StrPermission + ")) "
                strQuery5 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + StrPermission + ")) "
                If strItemAll = "Y" Then

                Else
                    strQuery1 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strQuery3 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strQuery5 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                End If
            Else
                If strItemAll = "Y" Then
                    strQuery1 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strQuery3 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strQuery5 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                Else
                    strQuery1 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strQuery3 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                    strQuery5 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code in (SELECT Location_Code FROM " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER WHERE Loc_Segment_Code IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                End If
            End If
            '----Added by--Pankaj Kumar---ON-20/09/2012--
            If chkmrpselect.IsChecked Then
                strQuery1 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.MRP in (" + clsCommon.GetMulcallString(cbgmrp.CheckedValue) + ") "
                strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.MRP in (" + clsCommon.GetMulcallString(cbgmrp.CheckedValue) + ") "
                strQuery3 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.MRP in (" + clsCommon.GetMulcallString(cbgmrp.CheckedValue) + ") "
                strQuery5 += " and " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.MRP in (" + clsCommon.GetMulcallString(cbgmrp.CheckedValue) + ") "
            End If

            Dim ArrDBName As ArrayList = Nothing
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            Dim strquery6 = strQuery3 & strQuery4 & strQuery5

            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery1, ArrDBName, False)
            Dim strSubReport1 = clsCommon.GetQueryWithAllSelectedDataBase(strQuery2, ArrDBName, False)
            Dim strSubReport2 = clsCommon.GetQueryWithAllSelectedDataBase(strquery6, ArrDBName, False)
            'strQuery2 = clsCommon.GetQueryWithAllSelectedDataBase(strQuery2, ArrDBName, False)

            frmCRV.funsubreport(CrystalReportFolder.InventoryReport, strQuery, strSubReport1, strSubReport1, "", "", "crptStockReportDetail", "Stock Reconciliation Detail Report", "crptOpeningBalance.rpt", "crptClosingBalance.rpt")
        End If
        frmCRV = Nothing
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged, rbtnCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    '    Private Function funSetUserAccess() As Boolean
    '        Try

    '            Dim strRights As String
    '            Dim strTemp() As String
    '            Dim strProgCode = "STOCK-RPT+"
    '            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '            strTemp = Split(strRights, ",")
    '            If strTemp(0) = "0" Then
    '                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '                funSetUserAccess = False
    '                blnRead = False
    '                Me.Close()
    '                Exit Function
    '            Else
    '                blnRead = True
    '            End If
    '            If strTemp(1) = "0" Then 'Grant modify access

    '            End If
    '            If strTemp(2) = "0" Then 'Grant modify access

    '            End If

    '            funSetUserAccess = True
    '        Catch er As Exception
    '            myMessages.myExceptions(er)
    '        End Try
    '    End Function

    Private Sub chkmrpall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkmrpall.ToggleStateChanged
        cbgmrp.Enabled = False
    End Sub

    Private Sub chkmrpselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkmrpselect.ToggleStateChanged
        cbgmrp.Enabled = True
    End Sub
End Class

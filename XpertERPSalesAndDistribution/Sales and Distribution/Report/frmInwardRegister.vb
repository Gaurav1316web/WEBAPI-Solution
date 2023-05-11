Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports XpertERPEngine
Imports common
Public Class frmInwardRegister
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Dim userCode, companyCode As String
    Dim sql As String
    Dim dr As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        dr = clsDBFuncationality.GetDataTable(sql)
        For Each row As DataRow In dr.Rows
            l1User = row(0).ToString()
            l2User = row(1).ToString()
            l3User = row(2).ToString()
            l4User = row(3).ToString()
            l5User = row(4).ToString()
        Next
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        'Dim qry As String = "select Location,TSPL_LOCATION_MASTER.Location_Desc as[Location Description] from (select distinct Location from TSPL_SALE_INVOICE_DETAIL)Final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location"
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Sub LoadRoute()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub

    Private Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub
    Sub print()
        Try
            Dim strSQL1Group, strSQL2Group, strReportTitle, strOrderColumn, strOrderBy, strSQLGroup3, strOrderby3 As String
            strSQL1Group = ""
            strSQL2Group = ""
            strSQLGroup3 = ""
            strReportTitle = ""
            strOrderBy = ""
            strOrderby3 = ""
            strOrderColumn = ""
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
                Return
            End If

            If rdbSku.IsChecked = True Then
                strSQL1Group = "TSPL_ADJUSTMENT_DETAIL.Item_Code"
                strSQL2Group = "TSPL_TRANSFER_DETAIL.Item_Code"
                strSQLGroup3 = "Father_Code"
                strReportTitle = "Inward Register Sku wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
                strOrderby3 = "(select a.Sku_Seq From TSPL_ITEM_MASTER a where a.Item_Code=TSPL_ITEM_MASTER.Father_Code)"
            ElseIf rdbPack.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS.Class_Code"
                strSQL2Group = " TSPL_ITEM_DETAILS.Class_Code"
                strSQLGroup3 = " TSPL_ITEM_DETAILS.Class_Code"
                strReportTitle = "Inward Register Pack wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
                strOrderby3 = "(select a.Pack_Seq From TSPL_ITEM_MASTER a where a.Item_Code=TSPL_ITEM_MASTER.Father_Code)"
            ElseIf rdbFlavour.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Code"
                strSQL2Group = "TSPL_ITEM_DETAILS_1.Class_Code"
                strSQLGroup3 = " TSPL_ITEM_DETAILS.Class_Code"
                strReportTitle = "Inward Register Flavour wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
                strOrderby3 = "(select a.Flavour_Seq From TSPL_ITEM_MASTER a where a.Item_Code=TSPL_ITEM_MASTER.Father_Code)"
            End If

            Dim strSql1 As String = "SELECT TSPL_ADJUSTMENT_HEADER.Document_No, CONVERT(date, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103) AS DocDate, " & _
            "TSPL_SALE_INVOICE_HEAD.Route_No, TSPL_SALE_INVOICE_HEAD.Route_Desc, TSPL_SALE_INVOICE_HEAD.Vehicle_Code, " & _
           " TSPL_ADJUSTMENT_DETAIL.Item_Code,isnull((case when TSPL_ADJUSTMENT_DETAIL.Unit_code in ('FC','EC')  then (isnull(TSPL_ADJUSTMENT_DETAIL.Item_Quantity,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_ADJUSTMENT_DETAIL.Item_Quantity,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0)  AS QTY, TSPL_ITEM_DETAILS.Class_Code AS Pack, " & _
            " TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " & strSQL1Group & " AS Group1, TSPL_LOCATION_MASTER.Location_Code, " & _
            "TSPL_LOCATION_MASTER.Location_Desc,'" & dtpFdate.Value & "' as FromDate,'" & DtpTodate.Value & "' as Todate, " & _
            "'" & strReportTitle & "' as ReportTitle," & strOrderColumn & " as OrderBy FROM  TSPL_ADJUSTMENT_HEADER INNER JOIN " & _
            "TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No INNER JOIN " & _
            "TSPL_SALE_INVOICE_HEAD ON TSPL_ADJUSTMENT_HEADER.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No INNER JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
            "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
            "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_DETAIL.Location_Code = TSPL_LOCATION_MASTER.Location_Code " & _
            "inner join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_ADJUSTMENT_DETAIL.Item_Code  and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_ADJUSTMENT_DETAIL.Unit_code " & _
           " WHERE CONVERT(date, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103) > = CONVERT(date,'" & dtpFdate.Value & "',103) and " & _
            " CONVERT(date, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103) < = CONVERT(date,'" & DtpTodate.Value & "',103) and " & _
            "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice' AND  " & _
            "(TSPL_ITEM_DETAILS.Class_Name = 'Size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'flavour')"
            Dim Un1 As String = " Union All "
            Dim strSql2 As String = "SELECT TSPL_TRANSFER_HEAD.Load_Out_No AS Document_No, TSPL_TRANSFER_HEAD.Transfer_Date AS DocDate, TSPL_TRANSFER_HEAD.Route_No, " & _
            "TSPL_ROUTE_MASTER.Route_Desc, TSPL_TRANSFER_HEAD.Vehicle_Code, TSPL_TRANSFER_DETAIL.Item_Code, " & _
            "  isnull((case when TSPL_TRANSFER_DETAIL.Uom in ('FC','EC')  then (isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0)  AS QTY, TSPL_ITEM_DETAILS.Class_Code AS Pack, TSPL_ITEM_DETAILS_1.Class_Code AS Flavour,  " & _
            " " & strSQL2Group & " AS Group1, TSPL_TRANSFER_HEAD.To_Location, TSPL_LOCATION_MASTER.Location_Desc AS Location_Desc, " & _
            "'" & dtpFdate.Value & "' as FromDate,'" & DtpTodate.Value & "' as Todate,'" & strReportTitle & "' as ReportTitle," & strOrderColumn & " as OrderBy " & _
            "FROM TSPL_TRANSFER_HEAD INNER JOIN TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
            "TSPL_ROUTE_MASTER ON TSPL_TRANSFER_HEAD.Route_No = TSPL_ROUTE_MASTER.Route_No INNER JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
            "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
            "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER.Location_Code " & _
            "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
      " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_TRANSFER_DETAIL.Item_Code  and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom " & _
          " WHERE TSPL_TRANSFER_HEAD.Transfer_Date > = CONVERT(date,'" & dtpFdate.Value & "',103) and " & _
            "TSPL_TRANSFER_HEAD.Transfer_Date < = CONVERT(date,'" & DtpTodate.Value & "',103) and " & _
            "(TSPL_TRANSFER_HEAD.Transfer_Type = 'LI')  AND " & _
            "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and Location_type='Physical'"

            Dim strSql3 As String = "SELECT TSPL_TRANSFER_HEAD.Load_Out_No AS Document_No, TSPL_TRANSFER_HEAD.Transfer_Date AS DocDate, TSPL_TRANSFER_HEAD.Route_No, " & _
           "TSPL_ROUTE_MASTER.Route_Desc, TSPL_TRANSFER_HEAD.Vehicle_Code, Father_Code as Item_Code, " & _
           " Leak  AS QTY, TSPL_ITEM_DETAILS.Class_Code AS Pack, TSPL_ITEM_DETAILS_1.Class_Code AS Flavour,  " & _
           " " & strSQLGroup3 & " AS Group1, TSPL_TRANSFER_HEAD.To_Location, TSPL_LOCATION_MASTER.Location_Desc AS Location_Desc, " & _
           "'" & dtpFdate.Value & "' as FromDate,'" & DtpTodate.Value & "' as Todate,'" & strReportTitle & "' as ReportTitle," & strOrderby3 & " as OrderBy " & _
           "FROM TSPL_TRANSFER_HEAD INNER JOIN TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
           "TSPL_ROUTE_MASTER ON TSPL_TRANSFER_HEAD.Route_No = TSPL_ROUTE_MASTER.Route_No INNER JOIN " & _
           "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
           "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
           "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER.Location_Code " & _
           "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_TRANSFER_DETAIL.Item_Code  and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom " & _
           " WHERE TSPL_TRANSFER_HEAD.Transfer_Date > = CONVERT(date,'" & dtpFdate.Value & "',103) and " & _
           "TSPL_TRANSFER_HEAD.Transfer_Date < = CONVERT(date,'" & DtpTodate.Value & "',103) and " & _
           "(TSPL_TRANSFER_HEAD.Transfer_Type = 'LI')  AND " & _
           "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and Location_type='Physical' and Leak <> 0 "

            Dim strLocAll, strRouteAll As String
            If chkLocationAll.IsChecked = True Then
                strLocAll = "Y"
            Else
                strLocAll = "N"
            End If
            If chkRouteAll.IsChecked = True Then
                strRouteAll = "Y"
            Else
                strRouteAll = "N"
            End If
            If strLocAll = "N" Then
                strSql1 += " and TSPL_ADJUSTMENT_DETAIL.Location_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                strSql2 += " and TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                strSql3 += " and TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If


            If strRouteAll = "N" Then
                strSql1 += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                strSql2 += " and TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                strSql3 += " and TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

            End If
            Dim strQuery As String = strSql1 & Un1 & strSql2 & Un1 & strSql3 & strOrderBy

            If rdodetail.IsChecked = True Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptInwardRegisteDetail", "Inward Register Detail")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptInwardRegisteSummary", "Inward Register Summary")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        dtpFdate.Value = clsCommon.GETSERVERDATE
        DtpTodate.Value = clsCommon.GETSERVERDATE
        LoadLocation()
        LoadRoute()
        chkLocationAll.IsChecked = True
        chkRouteAll.IsChecked = True
        rdodetail.IsChecked = True
        rdbSku.IsChecked = True
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub





    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.EmptyInwardSaleRegister1)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmInwardRegister_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub FrmEmptyInwardRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetUserMgmtNew()
        dtpFdate.Value = clsCommon.GETSERVERDATE
        DtpTodate.Value = clsCommon.GETSERVERDATE
        LoadLocation()
        LoadRoute()
        chkLocationAll.IsChecked = True
        chkRouteAll.IsChecked = True
        rdodetail.IsChecked = True
        rdbSku.IsChecked = True
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")






    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "INWD-RG"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
End Class

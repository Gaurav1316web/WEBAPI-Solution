
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
Imports common
Public Class FrmVehiclewiseSale
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                'dr.Read()
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        ' Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"

        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VehiclewiseSale1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmVehiclewiseSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            printdata()
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
    Private Sub FrmVehiclewiseSale_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadLocation()
        SetUserMgmtNew()
        chkLocatioAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        ddlConvert.Text = "Converted"

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")


    End Sub


    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VHL-SALE-RPT"
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

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub reset()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        ddlConvert.Text = "Converted"
    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printdata()
    End Sub
    Sub printdata()
        Dim LocFilter As String


        Try
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            Else
                LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocFilter = LocFilter.Replace("'", "")
            End If
            Dim strSubQry1 As String = String.Empty
            Dim strConverted As String = String.Empty
            Dim strLoc As String = String.Empty
            If chkLocatioAll.IsChecked = True Then
                strLoc = "Y"
            Else
                strLoc = "N"
            End If
            If ddlConvert.Text = "Converted" Then
                strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1)"
                strConverted = "Converted"
            ElseIf ddlConvert.Text = "8oz" Then
                strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1) * isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz'),0)"
                strConverted = "8oz"
            ElseIf ddlConvert.Text = "Raw" Then
                strSubQry1 = "1"
                strConverted = "Raw"
            End If

            Dim strSql As String = "SELECT '" + LocFilter + "' as LocFilter,TSPL_SALE_INVOICE_HEAD.Location, TSPL_LOCATION_MASTER.Location_Desc, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " & _
                     " TSPL_SALE_INVOICE_HEAD.Route_No, TSPL_SHIPMENT_MASTER.Route_Desc, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " & _
                     " TSPL_SHIPMENT_MASTER.Salesman_Code, TSPL_EMPLOYEE_MASTER.Emp_Name AS Salesname, TSPL_SALE_INVOICE_HEAD.Vehicle_No, " & _
                     " TSPL_SHIPMENT_MASTER.Trip_No, (TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/Conversion_Factor) * " & strSubQry1 & " as Invoice_Qty, '" & fromDate.Value & "' AS Fdate, '" & ToDate.Value & "' AS TDAte, '" & strConverted & "' AS ConvType " & _
                     "FROM TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
                     "TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                     " TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                     " TSPL_EMPLOYEE_MASTER ON TSPL_SALE_INVOICE_HEAD.Salesman_Code = TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                     " TSPL_SHIPMENT_MASTER ON TSPL_SALE_INVOICE_HEAD.Shipment_No = TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " & _
                     " TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                     " TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code where convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                     "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)"

            If strLoc = "N" Then
                strSql += " and TSPL_SALE_INVOICE_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptVehiclewiseSale", "Vehicle wise Sale")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocatioAll.IsChecked
    End Sub
End Class

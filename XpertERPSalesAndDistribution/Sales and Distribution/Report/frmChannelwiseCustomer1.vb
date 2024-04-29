Imports XpertERPEngine
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
Public Class FrmChannelwiseCustomer1
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If chkLocSelect.IsChecked = True AndAlso dgvLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast single Location Or Select All", Me.Text)
            Exit Sub
        End If
        print()

    End Sub
    Sub print()
        Dim strSubQry1, strConverted, strHierDesc, strHierlevel, strTitle, strItemGroup As String
        strSubQry1 = ""
        strConverted = ""
        strHierDesc = ""
        strHierlevel = ""
        strTitle = ""
        strItemGroup = ""
        If ddlConvert.Text = "Converted" Then
            strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1)"
            strConverted = "Converted"
        ElseIf ddlConvert.Text = "8oz" Then
            strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1) * isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz'),0)"
            strConverted = "8oz"
        ElseIf ddlConvert.Text = "Raw" Then
            'strSubQry1 = "isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Raw'),0)"
            strSubQry1 = "1"
            strConverted = "Raw"
        End If

        If ddlcategory.Text = "HOS" Then
            strHierDesc = "HOS"
            strHierlevel = "Level2_User_code"
        ElseIf ddlcategory.Text = "TDM" Then
            strHierDesc = "TDM"
            strHierlevel = "Level3_User_code"
        ElseIf ddlcategory.Text = "ADC" Then
            strHierDesc = "ADC"
            strHierlevel = "Level4_User_code"
        ElseIf ddlcategory.Text = "CE" Then
            strHierDesc = "CE"
            strHierlevel = "Level5_User_code"
        ElseIf ddlcategory.Text = "SalesMan" Then
            strHierDesc = "SalesMan"
            strHierlevel = "Level5_User_Code"
        End If

        If rdbSku.IsChecked = True Then
            strItemGroup = "TSPL_SALE_INVOICE_DETAIL.Item_Code"
            strTitle = "(Sku -wise)"
        ElseIf rdbPack.IsChecked = True Then
            strItemGroup = "TSPL_ITEM_DETAILS.Class_Code + TSPL_ITEM_DETAILS_1.Class_Code + TSPL_ITEM_DETAILS_2.Class_Code"
            strTitle = "(Pack -wise)"
        ElseIf rdbFlavour.IsChecked = True Then
            strItemGroup = "TSPL_ITEM_DETAILS_3.Class_Code"
            strTitle = "(Flavour -wise)"
        End If

        Dim strSql As String = "SELECT  '" & strHierDesc & "' + ' (' + TSPL_EMPLOYEE_MASTER.Emp_Name + ')' as Hier1,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " & strItemGroup & " as Item_Code, TSPL_ITEM_DETAILS.Class_Desc, " & _
                      "TSPL_CUSTOMER_MASTER.Channel_Code, TSPL_CUSTOMER_MASTER.Channel_Desc, TSPL_SALE_INVOICE_DETAIL.Invoice_Qty, " & _
                      "'" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate,'" & strConverted & "' as Converted,'" & strTitle & "' as Reporttitle " & _
                      "FROM TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
                      "TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                      "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
                      "TSPL_EMPLOYEE_MASTER ON TSPL_SALE_INVOICE_DETAIL." & strHierlevel & " = TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
                      "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                      "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
                      "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_3 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_3.Item_Code INNER JOIN " & _
                      "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_2 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_2.Item_Code " & _
                      "WHERE (TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Category') AND " & _
                      "(TSPL_ITEM_DETAILS_2.Class_Name = 'Pack') AND (TSPL_ITEM_DETAILS_3.Class_Name = 'Flavour') and  convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                     "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        If chkLocSelect.IsChecked = True AndAlso dgvLocation.CheckedValue.Count > 0 Then
            strSql += " AND TSPL_SALE_INVOICE_HEAD.Location  in (" + clsCommon.GetMulcallString(dgvLocation.CheckedValue) + ")"
        End If
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "crptChannelwiseCustomer", "Channel wise Customer")
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.Channelwisecustomer1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmChannelwiseCustomer1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
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



    Private Sub FrmChannelwiseCustomer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()
        LoadLocation()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub


    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CHL-CUS-RPT"
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
    Sub reset()
        ddlConvert.Text = "Converted"
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbSku.IsChecked = True
        ddlcategory.Text = "HOS"
        chkLocAll.IsChecked = True
    End Sub

    Sub LoadLocation()
        Dim qry As String = "Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        dgvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        dgvLocation.ValueMember = "Code"
        dgvLocation.DisplayMember = "Description"
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        dgvLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        dgvLocation.Enabled = True
    End Sub
End Class

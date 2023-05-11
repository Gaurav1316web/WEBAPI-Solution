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
Public Class FrmLoadOutStatusreport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    '  Dim dr As SqlDataReader

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
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()

    End Sub
    Sub print()
        Dim LocFilter As String
        Try
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            Else
                LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocFilter = LocFilter.Replace("'", "")

            End If
            'strQuery = "SELECT  '" + LocFilter + "' as LocFilter,  TSPL_SHIPMENT_MASTER.Shipment_No, CONVERT(varchar(10), " & _
            '           "TSPL_SHIPMENT_MASTER.Shipment_Date, 103) AS [LoadOut Date], " & _
            '          "TSPL_SHIPMENT_MASTER.Cust_Name, TSPL_SHIPMENT_MASTER.Salesman_Code, " & _
            '          "TSPL_SHIPMENT_MASTER.Vehicle_Code, TSPL_SHIPMENT_MASTER.Is_Post, " & _
            '          "convert(date,'" & FDate.Value & "',103) AS Fdate, convert(date,'" & ToDate.Value & "',103) AS TDate, TSPL_EMPLOYEE_MASTER.Emp_Name as SalesName, " & _
            '          "TSPL_LOCATION_MASTER.Location_Code as LocCode, TSPL_LOCATION_MASTER.Location_Desc as LocDesc FROM TSPL_SHIPMENT_MASTER INNER JOIN TSPL_EMPLOYEE_MASTER ON " & _
            '          "TSPL_SHIPMENT_MASTER.Salesman_Code = TSPL_EMPLOYEE_MASTER.EMP_CODE Left Outer Join TSPL_LOCATION_MASTER on TSPL_SHIPMENT_MASTER.Location=TSPL_LOCATION_MASTER.Location_Code " & _
            '          "WHERE (ISNULL(TSPL_SHIPMENT_MASTER.Is_Complete, 'N') = 'N')or (select case when TSPL_SHIPMENT_MASTER.Is_Complete = '' then 'N' end as Is_Complete)='N' and " & _
            '          "Shipment_Date >= convert(date,'" & FDate.Value & "',103) and Shipment_Date <= convert(date,'" & ToDate.Value & "',103) and Is_Post='Y'"



            strQuery = "  select '' as LocFilter,Transfer_No as Shipment_No  , [LoadOut Date]  AS [LoadOut Date], Cust_Name , Salesman_Code , Vehicle_Code, Is_Post,  Fdate, TDate,  SalesName,Location "
            strQuery += " ,(select case when Is_Complete = ''  then 'N' when Is_Complete = 'Open'  then 'N' when (isnull(Is_Complete,'N')='N') then 'N' end as Is_Complete) as vechile_return   from"
            strQuery += "("
            If (rdbtntransfer.IsChecked Or rdbtnAll.IsChecked) Then
                strQuery += "SELECT  '' as LocFilter,  TSPL_TRANSFER_HEAD .Transfer_No , CONVERT(varchar(10), TSPL_TRANSFER_HEAD.Transfer_Date, 103) AS [LoadOut Date], '' as Cust_Name, TSPL_TRANSFER_HEAD.Salesmancode as Salesman_Code , TSPL_TRANSFER_HEAD.Vehicle_Code, TSPL_TRANSFER_HEAD.Post as Is_Post, convert(date,'" & FDate.Value & "',103) AS Fdate, convert(date,'" & ToDate.Value & "',103) AS TDate,Is_Complete ,(select TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_EMPLOYEE_MASTER where TSPL_TRANSFER_HEAD.Salesmancode =TSPL_EMPLOYEE_MASTER.EMP_CODE) as SalesName,From_Location as Location,Trans_Type as Shipment_Type ,Reference_Doc_No as referenceno,'Transfer' as Type FROM TSPL_TRANSFER_HEAD "
            End If
            If (rdbtnAll.IsChecked) Then
                strQuery += "union all"
            End If
            If (rdbtnSale.IsChecked Or rdbtnAll.IsChecked) Then
                strQuery += " SELECT  '' as LocFilter,  TSPL_SALE_INVOICE_HEAD .Sale_Invoice_No as  Transfer_No, CONVERT(varchar(10), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date , 103) AS [LoadOut Date],  Cust_Name, TSPL_SALE_INVOICE_HEAD.Salesman_Code  , TSPL_SALE_INVOICE_HEAD.Vehicle_Code, TSPL_SALE_INVOICE_HEAD.Is_Post  as Is_Post, convert(date,'" & FDate.Value & "',103) AS Fdate, convert(date,'" & ToDate.Value & "',103) AS TDate,status as Is_Complete,(select TSPL_EMPLOYEE_MASTER.Emp_Name  from TSPL_EMPLOYEE_MASTER where TSPL_SALE_INVOICE_HEAD.Salesman_Code =TSPL_EMPLOYEE_MASTER.EMP_CODE) as SalesName,Location,Shipment_Type,'' as referenceno ,'sale' as Type FROM TSPL_SALE_INVOICE_HEAD "

            End If
            strQuery += "  ) abc  "
            strQuery += " WHERE ((Shipment_Type='Excise' and ISNULL(referenceno ,'')='' ) or (Shipment_Type='depot' and ISNULL(referenceno ,'')='' )or Shipment_Type='route' or Type='sale') "
            strQuery += "   and convert(date,[LoadOut Date] ,103) >= convert(date,'" & FDate.Value & "',103) and convert(date,[LoadOut Date] ,103)  <= convert(date,'" & ToDate.Value & "',103)  and  ((ISNULL(Is_Complete, 'N') = 'N')or (select case when Is_Complete = ''  then 'N' when Is_Complete = 'Open'  then 'N' end as Is_Complete)='N') and  Is_Post='Y'"
            If chkLocSelect.IsChecked And cbgLocation.Enabled = True Then
                strQuery += " and  Location in  (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            strQuery += " order by [LoadOut Date]  "

            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptLoadOutStatus", "Load Status Report")
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.LoadOutStatusreport1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmLoadOutStatusreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub


    Private Sub FrmLoadOutStatusreport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        reset()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")



    End Sub

    Public Sub reset()
        LoadLocation()
        chkLocAll.IsChecked = True
        rdbtnAll.IsChecked = True
        FDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
    End Sub


    Sub LoadLocation()
        'Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub



    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "LOUT-STS-RPT"
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
End Class

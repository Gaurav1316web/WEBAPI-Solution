Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Public Class FrmCustomerEmptyTrial2
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub FrmCustomerEmptyTrial2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            FunReset()
        End If
    End Sub


    Private Sub FrmCustomerEmptyTrial2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        chkSelectAll.IsChecked = True
        chkLOcAll.IsChecked = True
        LoadCustomer()
        LoadLocation()
        CbZero.Checked = True
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P for Print ")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnCustomerEmptyTrial)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        funPrint()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        FunClose()
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer COde], Customer_Name as [Customer Name]  from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    Sub LoadLocation()
        'Dim qry As String = "select Location_Code AS [Location Code], Location_Desc as [Location Description]  from TSPL_LOCATION_MASTER  where Location_Type='Physical'"
        '' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Public Sub funPrint()
        Dim LocationFilter As String = ""
        Dim CustomerCodeFilter As String = ""

        If chkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
            CustomerCodeFilter = clsCommon.GetMulcallString(cbgCustomer.CheckedValue)
            CustomerCodeFilter = CustomerCodeFilter.Replace("'", "")
        End If

        If chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            LocationFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            LocationFilter = LocationFilter.Replace("'", "")
        End If

        Dim ArrLoc As ArrayList = cbgLocation.CheckedValue
        If (dtpstart.Value > dtpend.Value) Then
            common.clsCommon.MyMessageBoxShow("'Start Date' Cann't be more than 'End date'")
        ElseIf chkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast One Customer Code")
            Return
        ElseIf chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast One Location")
            Return
        Else
            Dim address As String
            If cbgLocation.CheckedValue.Count = 1 Then
                address = " (Select MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_TDS_STATE_MASTER.State_Code ='' Then '' else ', '+Convert(Varchar, TSPL_TDS_STATE_MASTER.State_Name ) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) as Address From TSPL_LOCATION_MASTER Left Outer Join TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER.State= TSPL_TDS_STATE_MASTER.State_Code  "
                address += "  WHERE TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(ArrLoc) + "))"
            Else
                address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "

            End If
            Try
                Dim qry As String = "Select '" + CustomerCodeFilter + "' as CustomerCodeFilter,'" + LocationFilter + "' as LocationFilter, '" + dtpstart.Value + "' AS [startdate], '" + dtpend.Value + "' AS [enddate], '" + clsCommon.GETSERVERDATE() + "' as CurrDate, " & _
" TSPL_CUSTOMER_MASTER.Cust_Code as CustCode , TSPL_CUSTOMER_MASTER.Customer_Name as CustName, DrAmount, CrAmount, " + address + " as add1,  TSPL_COMPANY_MASTER.Logo_Img as img1, TSPL_COMPANY_MASTER.Logo_Img2 as img2, TSPL_COMPANY_MASTER.Comp_Name as CompName, Location, LocDesc " & _
" from (select MAX(xxx.CustCode) as CustCode , MAX(xxx.CustName) as CustName, Sum(xxx.DrAmount) as DrAmount, " & _
" SUm(xxx.CrAmount) as CrAmount, MAX(xxx.CompCode) as CompCode, MAX(Location) as location, Max(Location_Desc) as LocDesc , MAX(OrderDrCr) as OrderDrCr  from" & _
" (select TSPL_SALE_INVOICE_HEAD.Cust_Code as CustCode, TSPL_SALE_INVOICE_HEAD.Cust_Name as CustName, " & _
" TSPL_SALE_INVOICE_HEAD.Empty_Value as [DrAmount], 0 as [CrAmount], TSPL_SALE_INVOICE_HEAD.Comp_Code as CompCode, TSPL_SALE_INVOICE_HEAD.Location, 0 as OrderDrCr " & _
" from TSPL_SALE_INVOICE_HEAD Left OUTER JOIN TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_HEAD.Location where convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" + dtpstart.Value + "',103) " & _
" AND convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" + dtpend.Value + "',103)   and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  "


                If chkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                    qry += " and Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                End If


                If chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(ArrLoc) + ")) "
                End If

                qry += " UNION ALL " & _
" select  Customer_CODE as CustCode, Customer_NAME as CustName, 0 as DrAmount, TSPL_ADJUSTMENT_DETAIL.Item_Cost as CrAmount, " & _
" TSPL_ADJUSTMENT_HEADER.Comp_Code as CompCode, TSPL_ADJUSTMENT_DETAIL.Location_Code as Location, 1 as OrderDrCr from TSPL_ADJUSTMENT_DETAIL " & _
" left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No  " & _
" where TSPL_ADJUSTMENT_HEADER.ItemType='E' And Customer_CODE<>'' AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >= convert(date, '" + dtpstart.Value + "',103) " & _
"  AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date, '" + dtpend.Value + "',103) and TSPL_ADJUSTMENT_HEADER.posted='Y' "

                If chkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                    qry += " and Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
                End If


                If chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and location_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(ArrLoc) + "))  "
                End If


                qry += ") " & _
" xxx Left OUTER JOIN TSPL_COMPANY_MASTER on xxx.CompCode=TSPL_COMPANY_MASTER.Comp_Code " & _
" Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xxx.Location group by CustCode )" & _
" yyy LEFT OUTER JOIN TSPL_COMPANY_MASTER on yyy.CompCode=TSPL_COMPANY_MASTER.Comp_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=yyy.CustCode WHERE 2=2"

                If CbZero.Checked = True Then
                    qry += "  AND (DrAmount = CrAmount) OR (DrAmount>0 or CrAmount>0) "
                Else
                    qry += "  AND (DrAmount != CrAmount)"
                End If
                qry += "  And Cust_Code<>'' ORDER BY OrderDrCr"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptCustomerEmptyTrial", "Customer Empty Trial")
                End If
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
    End Sub

    Public Sub FunReset()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        chkSelectAll.IsChecked = True
        chkLOcAll.IsChecked = True
    End Sub


    Public Sub FunClose()
        Me.Close()
    End Sub

    Private Sub chkSelectAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSelectAll.ToggleStateChanged
        cbgCustomer.Enabled = False
    End Sub

    Private Sub chkSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSelect.ToggleStateChanged
        cbgCustomer.Enabled = True
    End Sub

    Private Sub chkLOcAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLOcAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLOcSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLOcSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub


    '---------------------Added By -----Pankaj Kumar-------------on--29/03/2012-------------
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-E-TR"
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

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    '-----------------------------------Code Ends Here-------------------------------
    'End Function
End Class

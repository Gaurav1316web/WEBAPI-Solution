Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Public Class FrmOutletEmptyReport1
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Dim userCode, companyCode As String
    Dim sql As String

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                ' dr.Read()
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer COde], Customer_Name as [Customer Name]  from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    Sub LoadLocation()
        'Dim qry As String = "select Location_Code AS [Location Code], Location_Desc as [Location Description]  from TSPL_LOCATION_MASTER  where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub





    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.OutletEmpty1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmOutletEmptyReport1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub



    Private Sub FrmOutletEmptyReport1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        chkSelectAll.IsChecked = True
        chkLOcAll.IsChecked = True
        LoadCustomer()
        LoadLocation()
        ChkZero.Checked = True
        LoadTemplate()
        chktempall.IsChecked = True

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")





    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "OUT-EMT-RPT"
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


    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
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

    Private Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub
    Sub print()
        Try
            Dim CustFilter As String = ""
            Dim LocFilter As String = ""
            Dim TempFilter As String = ""

            If chkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                CustFilter = clsCommon.GetMulcallString(cbgCustomer.CheckedValue)
                CustFilter = CustFilter.Replace("'", "")
            End If
            If chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocFilter = LocFilter.Replace("'", "")
            End If
            If chktempselect.IsChecked AndAlso cgvtemplate.CheckedValue.Count > 0 Then
                TempFilter = clsCommon.GetMulcallString(cgvtemplate.CheckedValue)
                TempFilter = TempFilter.Replace("'", "")
            End If

            If (dtpstart.Value > dtpend.Value) Then
                common.clsCommon.MyMessageBoxShow("'Start Date' Cann't be more than 'End date'")
            ElseIf chkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Customer Code")
                Return
            ElseIf chkLOcSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Location")
                Return
            ElseIf chktempselect.IsChecked AndAlso cgvtemplate.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Template")
                Return
            Else
                Dim strCustAll, strLocAll, strZeroBalance, strTempALl As String
                If chkSelectAll.IsChecked = True Then
                    strCustAll = "Y"
                Else
                    strCustAll = "N"
                End If

                If chkLOcAll.IsChecked = True Then
                    strLocAll = "Y"
                Else
                    strLocAll = "N"
                End If

                If chktempall.IsChecked = True Then
                    strTempALl = "Y"
                Else
                    strTempALl = "N"
                End If

                If ChkZero.Checked = True Then
                    strZeroBalance = "Y"
                Else
                    strZeroBalance = "N"
                End If
                Dim strSql1 As String = "select '" + CustFilter + "' as CustFilter,'" + LocFilter + "' as LocFilter,'" + TempFilter + "' as TempFilter,Cust_Code,Cust_Name,Sale_Invoice_Date as DocDate,(Invoice_Qty/Conversion_Factor) as EmptyOut, " & _
                "TSPL_LOCATION_MASTER.Loc_Segment_Code,Location_Desc,0 as EmptyIn,convert(date,'" & dtpstart.Value & "',103) as Fdate,convert(date,'" & dtpend.Value & "',103) as TDate,'" & strZeroBalance & "' as ZeroBal,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Doc_no from TSPL_SALE_INVOICE_HEAD " & _
                "inner join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  inner join " & _
                "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code inner join TSPL_ITEM_UOM_DETAIL on " & _
                "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "where TSPL_SALE_INVOICE_DETAIL.Empty_Value <> 0 and convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) "
                If rdbtnPosted.IsChecked Then
                    strSql1 += " and Is_Post='Y'"
               End If

                Dim un1 As String = "Union all "
                Dim strSql2 As String = "select '" + CustFilter + "' as CustFilter,'" + LocFilter + "' as LocFilter,'" + TempFilter + "' as TempFilter, Customer_CODE as Cust_Code,Customer_NAME as Cust_Name,convert(date,Adjustment_Date,103) as DocDate, " & _
                "0 as EmptyOut,TSPL_LOCATION_MASTER.Loc_Segment_Code,Location_Desc,(Item_Quantity/Conversion_Factor) as EmptyIn, " & _
                "convert(date,'" & dtpstart.Value & "',103) as Fdate,convert(date,'" & dtpend.Value & "',103) as TDate,'" & strZeroBalance & "' as ZeroBal,TSPL_ADJUSTMENT_HEADER.Adjustment_No from TSPL_ADJUSTMENT_HEADER inner join TSPL_ADJUSTMENT_DETAIL on " & _
                "TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No inner join TSPL_LOCATION_MASTER on " & _
                "TSPL_ADJUSTMENT_HEADER.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  inner join TSPL_ITEM_UOM_DETAIL on " & _
                "TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ADJUSTMENT_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "where Customer_CODE <> '' and convert(date,Adjustment_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                "convert(date,Adjustment_Date,103) <= convert(date, '" & dtpend.Value & "',103)  "
                If rdbtnPosted.IsChecked Then
                    strSql2 += " and Posted='Y'"
                End If
                If strLocAll = "N" Then
                    strSql1 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    strSql2 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If

                If strCustAll = "N" And strTempALl = "Y" Then
                    strSql1 += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                    strSql2 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                End If

                If strTempALl = "N" Then
                    strSql1 += " and  TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                    strSql2 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                End If

                Dim strQuery As String = strSql1 & un1 & strSql2

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                If dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else
                    If rdobtnSummary.IsChecked Then
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptOutletEmpty", "Outlet Empty Summary Report")
                    ElseIf rdobtndetails.IsChecked Then
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptOutletEmptyDetail", "Outlet Empty Detail Report")
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Sub reset()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        chkSelectAll.IsChecked = True
        chkLOcAll.IsChecked = True
        LoadCustomer()
        LoadLocation()
        ChkZero.Checked = True
        LoadTemplate()
        chktempall.IsChecked = True
    End Sub
End Class

'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - tspl_customer_category_master
'Start Date -
'End Date -
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmShipToLocationReport_vb
    Inherits FrmMainTranScreen

#Region "variables"
    Dim dr As DataTable
#End Region
#Region "Finder Load"
    Private Sub fndFromCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndFromCustomer.ConnectionString = connectSql.SqlCon()
        'fndFromCustomer.Query = "select ship_to_type_code as [Customer No.],ship_to_type_desc as [Customer Name],ship_to_code as [Ship To Location],ship_to_desc as [Descripion] from tspl_ship_to_location order by ship_to_type_code"
        'fndFromCustomer.ValueToSelect = "Customer No."
        'fndFromCustomer.ValueToSelect1 = "Customer Name"
        'fndFromCustomer.Caption = "Customer Details"
    End Sub

    Private Sub fndToCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndToCustomer.ConnectionString = connectSql.SqlCon()
        'fndToCustomer.Query = "select ship_to_type_code as [Customer No.],ship_to_type_desc as [Customer Name],ship_to_code as [Ship To Location],ship_to_desc as [Descripion] from tspl_ship_to_location order by ship_to_type_code"
        'fndToCustomer.ValueToSelect = "Customer No."
        'fndToCustomer.ValueToSelect1 = "Customer Name"
        'fndToCustomer.Caption = "Customer Details"
    End Sub
#End Region
#Region "ButtonClick Event"
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub
#End Region
#Region "Method"
    Private Sub funPrint()
        Dim f1 As String = fndFromCust.Value
        Dim t1 As String = fndToCust.Value
        '  FrmShipToLocationReportDetails.funReport(f1, t1)
        'FrmShipToLocationReportDetails.Show()
    End Sub
#End Region


    Private Sub FrmShipToLocationReport_vb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'AddHandler fndFromCustomer.txtValue.KeyPress, AddressOf FromCustomer_KeyPress
        'AddHandler fndToCustomer.txtValue.KeyPress, AddressOf ToCustomer_KeyPress

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("SHIPLCT-RPt")
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FromCustomer_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fndFromCustomer.txtValue.CharacterCasing = CharacterCasing.Upper
        'If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
        '    e.Handled = True
        'End If
    End Sub
    Private Sub ToCustomer_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fndToCustomer.txtValue.CharacterCasing = CharacterCasing.Upper
        'If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
        '    e.Handled = True
        'End If
    End Sub

    Private Sub fndFromCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If fndFromCustomer.txtValue.Text <> "" Then
        '    Dim s As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from tspl_customer_master where Cust_Code='" + fndFromCustomer.txtValue.Text + "'"))


        '    If s <> fndFromCustomer.txtValue.Text Then
        '        common.clsCommon.MyMessageBoxShow("Customer  doesn't exist")
        '        fndFromCustomer.txtValue.Text = ""
        '        fndFromCustomer.txtValue.Focus()
        '    Else

        '    End If
        'Else

        'End If
    End Sub

    Private Sub fndToCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If fndToCustomer.txtValue.Text <> "" Then
        '    Dim s As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from tspl_customer_master where Cust_Code='" + fndToCustomer.txtValue.Text + "'"))

        '    If s <> fndToCustomer.txtValue.Text Then
        '        common.clsCommon.MyMessageBoxShow("Customer  doesn't exist")
        '        fndToCustomer.txtValue.Text = ""
        '        fndToCustomer.txtValue.Focus()
        '    Else

        '    End If
        'Else

        'End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'fndFromCustomer.txtValue.Text = ""
        'fndToCustomer.txtValue.Text = ""
    End Sub

    Private Sub fndToCust__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndToCust._MYValidating
        Dim qry As String = "select ship_to_type_code as [Customer_No],ship_to_type_desc as [Customer Name],ship_to_code as [Ship To Location],ship_to_desc as [Descripion] from tspl_ship_to_location "
        fndToCust.Value = clsCommon.ShowSelectForm("fndFromCust", qry, "Customer_No", "", fndToCust.Value, "Customer_No", isButtonClicked)
    End Sub

    Private Sub fndFromCust__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFromCust._MYValidating
        Dim qry As String = "select ship_to_type_code as [Customer_No],ship_to_type_desc as [Customer Name],ship_to_code as [Ship To Location],ship_to_desc as [Descripion] from tspl_ship_to_location "
        fndFromCust.Value = clsCommon.ShowSelectForm("fndFromCust", qry, "Customer_No", "", fndFromCust.Value, "Customer_No", isButtonClicked)
    End Sub
End Class

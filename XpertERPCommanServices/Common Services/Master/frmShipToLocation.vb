'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - TSPL_SHIP_TO_LOCATION
'Start Date -
'End Date -
'-20/12/2012:11:00AM--Updation By--Pankaj Kumar--Applied Validations
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports System.Text.RegularExpressions
Imports common
Public Class frmShipToLocation
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public logfrm As Telerik.WinControls.UI.RadForm = Nothing
#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        Company1 = company
        UserCode1 = user
    End Sub
#End Region
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim tableName As String = "TSPL_TAX_MASTER"
    Dim tableCode As String = "Tax_Code"
    Dim codePrefix As String = "TAX"
    Dim objstr As String = "Tecxpert Software Pvt Ltd."
    Dim dt As Date = Date.Today

    Dim stlcode As String
    Dim stlname As String
    Dim stradd1 As String
    Dim stradd2 As String
    Dim stradd3 As String
    Dim stradd4 As String
    Dim strcity As String
    Dim strstate As String
    Dim strpostalcode As String
    Dim strcountry As String
    Dim strtelephone As String
    Dim stremail As String

    Dim userCode, companyCode As String
    Dim Company1 As String
    Dim UserCode1 As String
#End Region
#Region "passing data through constructor"
    Public Sub New(ByVal stlshiptloc As String, ByVal stlshiptdesc As String, ByVal stradd1 As String, ByVal stradd2 As String, ByVal stradd3 As String, ByVal stradd4 As String, ByVal strcity As String, ByVal strstate As String, ByVal strpostalcode As String, ByVal strcountry As String, ByVal strtelephone As String, ByVal stremail As String)
        InitializeComponent()
        stlcode = stlcode
        stlname = stlname
        stradd1 = stradd1
        stradd2 = stradd2
        stradd3 = stradd3
        stradd4 = stradd4
        strcity = strcity
        strstate = strstate
        strpostalcode = strpostalcode
        strcountry = strcountry
        strtelephone = strtelephone
        stremail = stremail
        ' MasterTemplate.AllowAddNewRow = False
        MasterTemplate.AllowEditRow = False
        MasterTemplate.CurrentRow.Cells(0).Value = stlshiptloc
        MasterTemplate.CurrentRow.Cells(1).Value = stlshiptdesc
        MasterTemplate.CurrentRow.Cells(2).Value = stradd1
        MasterTemplate.CurrentRow.Cells(3).Value = stradd2
        MasterTemplate.CurrentRow.Cells(4).Value = stradd3
        MasterTemplate.CurrentRow.Cells(5).Value = stradd4
        MasterTemplate.CurrentRow.Cells(6).Value = strcity
        MasterTemplate.CurrentRow.Cells(7).Value = strstate
        MasterTemplate.CurrentRow.Cells(8).Value = strpostalcode
        MasterTemplate.CurrentRow.Cells(9).Value = strcountry
        MasterTemplate.CurrentRow.Cells(10).Value = strtelephone
        MasterTemplate.CurrentRow.Cells(11).Value = stremail
        MasterTemplate.AllowAddNewRow = False
    End Sub
#End Region
#Region "Page Load"
    Private Sub frmShipToLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'fndCustomer.txtValue.MaxLength = 12
        'AddHandler fndCustomer.txtValue.KeyPress, AddressOf fndCustomer_KeyPress
        'AddHandler fndCustomer.ValueChanged, AddressOf fndCustomer_TextChanged
        'fndCustomer.txtValue.CharacterCasing = CharacterCasing.Upper
        'MasterTemplate.ReadOnly = True
        'MasterTemplate.AllowEditRow = False
        btnOpen.Enabled = False
        btnDelete.Enabled = False
        fndCustomer.Focus()
        fndCustomer.MyReadOnly = False
        'ButtonToolTip.SetToolTip(btn, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID

            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ShiptoLocation)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

#End Region
#Region "TextChanged"
    
    Private Sub fndCustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        LoadData()
    End Sub
    Sub LoadData()
        'txtCustomerName.Text = fndCustomer.Tag

        btnOpen.Enabled = False
        btnDelete.Enabled = False
        If fndCustomer.Value <> "" Then
            'dr = connectSql.RunSqlReturnDR("Select Ship_To_Type_Code from tspl_ship_To_location  where Ship_To_Type_Code='" + fndCustomer.Value + "'")
            sql = "Select Ship_To_Type_Code from tspl_ship_To_location  where Ship_To_Type_Code='" + fndCustomer.Value + "'"
            Dim str As String = ""

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    str = dr(0).ToString().ToUpper()
                Next
            End if

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                MasterTemplate.ReadOnly = True
            Else
                MasterTemplate.ReadOnly = False
                MasterTemplate.CurrentRow.Cells(1).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(2).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(3).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(4).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(5).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(6).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(7).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(8).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(9).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(10).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(11).ReadOnly = True
                ' MasterTemplate.CurrentRow.Cells(12).ReadOnly = True
            End If
            If str <> fndCustomer.Value Then
                ' txtCustomerName.Text = ""
                MasterTemplate.DataSource = Nothing
                MasterTemplate.AllowAddNewRow = True

                '  Dim dinfo As GridViewRowInfo = dgShipToLocation.Rows.AddNew
                For i As Integer = 0 To 1
                    MasterTemplate.AllowAddNewRow = True
                Next
            Else
                funFill()
            End If
        ElseIf fndCustomer.Value = "" Then
            txtCustomerName.Text = ""
            MasterTemplate.DataSource = Nothing
            MasterTemplate.AllowAddNewRow = True
        End If
    End Sub
#End Region
#Region "Methods"
    Private Sub funFill()
        MasterTemplate.AutoGenerateColumns = False
        MasterTemplate.DataSource = Nothing
        ds = connectSql.RunSQLReturnDS("select Ship_To_Code,Ship_To_Desc,Add1,Add2,Add3,Add4,City_Code,State,Pin_Code,Country,Telphone,Email from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + fndCustomer.Value + "'")
        MasterTemplate.DataSource = ds.Tables(0)
        MasterTemplate.Columns(0).Width = 80
        MasterTemplate.Columns(1).Width = 80
        MasterTemplate.Columns(2).Width = 80
        MasterTemplate.Columns(3).Width = 80
        MasterTemplate.Columns(4).Width = 80
        MasterTemplate.Columns(5).Width = 80
        MasterTemplate.Columns(6).Width = 80
        MasterTemplate.Columns(7).Width = 80
        MasterTemplate.Columns(8).Width = 80
        MasterTemplate.Columns(9).Width = 80
        MasterTemplate.Columns(10).Width = 80
        MasterTemplate.Columns(11).Width = 80

        MasterTemplate.Columns(0).FieldName = "Ship_To_Code"
        MasterTemplate.Columns(1).FieldName = "Ship_To_Desc"
        MasterTemplate.Columns(2).FieldName = "Add1"
        MasterTemplate.Columns(3).FieldName = "Add2"
        MasterTemplate.Columns(4).FieldName = "Add3"
        MasterTemplate.Columns(5).FieldName = "Add4"
        MasterTemplate.Columns(6).FieldName = "City_Code"
        MasterTemplate.Columns(7).FieldName = "State"
        MasterTemplate.Columns(8).FieldName = "Pin_Code"
        MasterTemplate.Columns(9).FieldName = "Country"
        MasterTemplate.Columns(10).FieldName = "Telphone"
        MasterTemplate.Columns(11).FieldName = "Email"

        'MasterTemplate.ReadOnly = True
        ' MasterTemplate.AllowEditRow = False

        'dr = connectSql.RunSqlReturnDR("select Ship_To_Type_Code from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + fndCustomer.Value + "'")
        sql = "select Ship_To_Type_Code from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + fndCustomer.Value + "'"


        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            btnOpen.Enabled = True
            btnNew.Enabled = True
            btnDelete.Enabled = True
        Else
            btnOpen.Enabled = False
            btnNew.Enabled = True
            btnDelete.Enabled = False
        End If


        'If userCode <> "ADMIN" Then
        ' If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub
    Private Sub funGridBind()
        MasterTemplate.AutoGenerateColumns = False
        '   logfrm = loginwin
        ds = connectSql.RunSQLReturnDS("select Ship_To_Code,Ship_To_Desc,Add1,Add2,Add3,Add4,City_Code,State,Pin_Code,Country,Telphone,Email from TSPL_SHIP_TO_LOCATION where Ship_To_Type_Code='" + fndCustomer.Value + "'")
        Dim dt As DataTable
        dt = ds.Tables(0)


        MasterTemplate.DataSource = dt

        MasterTemplate.Columns(0).Width = 80
        MasterTemplate.Columns(1).Width = 80
        MasterTemplate.Columns(2).Width = 80
        MasterTemplate.Columns(3).Width = 80
        MasterTemplate.Columns(4).Width = 80
        MasterTemplate.Columns(5).Width = 80
        MasterTemplate.Columns(6).Width = 80
        MasterTemplate.Columns(7).Width = 80
        MasterTemplate.Columns(8).Width = 80
        MasterTemplate.Columns(9).Width = 80
        MasterTemplate.Columns(10).Width = 80
        MasterTemplate.Columns(11).Width = 80

        MasterTemplate.Columns(0).FieldName = "Ship_To_Code"
        MasterTemplate.Columns(1).FieldName = "Ship_To_Desc"
        MasterTemplate.Columns(2).FieldName = "Add1"
        MasterTemplate.Columns(3).FieldName = "Add2"
        MasterTemplate.Columns(4).FieldName = "Add3"
        MasterTemplate.Columns(5).FieldName = "Add4"
        MasterTemplate.Columns(6).FieldName = "City_Code"
        MasterTemplate.Columns(7).FieldName = "State"
        MasterTemplate.Columns(8).FieldName = "Pin_Code"
        MasterTemplate.Columns(9).FieldName = "Country"
        MasterTemplate.Columns(10).FieldName = "Telphone"
        MasterTemplate.Columns(11).FieldName = "Email"
        MasterTemplate.ReadOnly = True
        If dt.Rows.Count > 0 Then
            MasterTemplate.ReadOnly = True
        Else
            MasterTemplate.ReadOnly = False
            'MasterTemplate.CurrentRow.Cells(1).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(2).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(3).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(4).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(5).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(6).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(7).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(8).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(9).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(10).ReadOnly = True
            'MasterTemplate.CurrentRow.Cells(11).ReadOnly = True
        End If
        ' logfrm.Hide()
        'Timer1.Enabled = False
        btnOpen.Enabled = True
        btnDelete.Enabled = True
        Timer1.Stop()
    End Sub

    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SHIP-LOC-M"
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
    '            btnNew.Enabled = False
    '            btnOpen.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            ' btnDelete.Enabled = False
    '            btnNew.Enabled = False
    '            btnOpen.Enabled = False
    '        End If

    '        'funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    'Code ends here


#End Region
#Region "KeyPress Event"
    Private Sub fndCustomer_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If

    End Sub
#End Region
#Region "Button Click Event"
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funreset()

    End Sub
    Sub funreset()
        Dim strstt As String = ddlShipToType.Text
        Dim strcusn As String = fndCustomer.Value
        Dim strcusnam As String = txtCustomerName.Text
        If clsCommon.myLen(strcusn) > 0 Then
            Dim fsld As New FrmShipToLocationDetails(UserCode1, Company1, strstt, strcusn, strcusnam)
            fsld.ShowDialog()
            MasterTemplate.AllowAddNewRow = True
            Timer1.Enabled = True
        Else
            common.clsCommon.MyMessageBoxShow("Please Select Customer")
            fndCustomer.Focus()
            Exit Sub
        End If
    End Sub



    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        Dim strstt As String = ddlShipToType.Text
        Dim strcusn As String = fndCustomer.Value
        Dim strcusnam As String = txtCustomerName.Text
        ds = RunSQLReturnDS("select Ship_To_Code,Ship_To_Desc,Add1,Add2,Add3,Add4,City_Code,State,Pin_Code,Country,Telphone,Email from TSPL_SHIP_TO_LOCATION where Ship_To_Type_Code='" + fndCustomer.Value + "'")
        Dim dt As DataTable
        dt = ds.Tables(0)
        Dim fsld As New FrmShipToLocationDetails(UserCode1, Company1, strstt, strcusn, strcusnam, dt)
        fsld.ShowDialog()
        Timer1.Enabled = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        'Dim rowselected As Integer = MasterTemplate.SelectedRows.Count

        'If (rowselected > 0) Then
        '    Int(rowCount = RadGridView.Rows.Count())
        '        for (int x = rowCount - 1; x >= 1; x--)
        '        {
        '            if (radGridView.Rows[x].IsSelected)
        '            RadGridView.Rows([x].Delete())
        '            Dim sr As String = MasterTemplate.Rows

        '  Dim str As String = Me.MasterTemplate.Rows(0).Cells(0).Value.ToString()
        ' Dim str As String = Me.MasterTemplate.
        ' Dim str1 As String = MasterTemplate.Rows(sr).Cells(0).Value.ToString()
        DeleteData()

    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndCustomer.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Public Sub funDelete()

        If myMessages.deleteConfirm() Then
            Try
                connectSql.RunSp("sp_TSPL_SHIP_TO_LOCATION_delete", New SqlParameter("@ShipToCode", MasterTemplate.CurrentRow.Cells(0).Value))
                myMessages.delete()
                Dim drin As GridViewDataRowInfo = TryCast(Me.MasterTemplate.CurrentRow, GridViewDataRowInfo)
                Me.MasterTemplate.Rows.Remove(Me.MasterTemplate.CurrentRow)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        Else
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
#End Region
    'Public Sub funTimer(ByVal a As String)
    '    Timer1.Enabled = a
    'End Sub
#Region "Finder Load"
    Private Sub fndCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndCustomer.ConnectionString = connectSql.SqlCon()
        'fndCustomer.Query = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER "
        'fndCustomer.Query = clsERPFuncationality.UserAvailableCustomers
        'fndCustomer.ValueToSelect = "Customer Code"
        'fndCustomer.ValueToSelect1 = "Customer Name"
        'fndCustomer.Caption = "Customer Details"
    End Sub
#End Region
#Region "CellDouble Click"
    Private Sub MasterTemplate_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles MasterTemplate.CellDoubleClick
        Dim strstt As String = ddlShipToType.Text
        Dim strcusn As String = fndCustomer.Value
        Dim strcusnam As String = txtCustomerName.Text
        If clsCommon.myLen(strcusn) > 0 Then
            ds = RunSQLReturnDS("select Ship_To_Code,Ship_To_Desc,Add1,Add2,Add3,Add4,City_Code,State,Pin_Code,Country,Telphone,Email from TSPL_SHIP_TO_LOCATION where Ship_To_Type_Code='" + fndCustomer.Value + "'")
            Dim dt As DataTable
            dt = ds.Tables(0)
            Dim fsld As New FrmShipToLocationDetails(UserCode1, Company1, strstt, strcusn, strcusnam, dt)
            fsld.SetUserMgmt(clsUserMgtCode.frmShipToLocationDetails)
            fsld.ShowDialog()
            Timer1.Enabled = True
        Else
            common.clsCommon.MyMessageBoxShow("Please Select Customer")
            fndCustomer.Focus()
            Exit Sub
        End If
    End Sub
#End Region
#Region "Timer Tick Event"
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If fndCustomer.Value <> "" Then
            funGridBind()
        End If
    End Sub
#End Region

#Region "Finder Leave Event"
    Private Sub fndCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndCustomer.Value <> "" Then
            'dr = connectSql.RunSqlReturnDR("select Cust_Code  from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "'")
            sql = "select Cust_Code  from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "'"
            Dim s As String
            s = clsDBFuncationality.getSingleValue(sql)

            If s <> fndCustomer.Value Then
                common.clsCommon.MyMessageBoxShow("Customer doesn't exist.")
                fndCustomer.Value = ""
                txtCustomerName.Text = ""
                fndCustomer.Focus()
                MasterTemplate.DataSource = Nothing
                btnOpen.Enabled = False
                btnDelete.Enabled = False
            Else
            End If
        Else
        End If
    End Sub
#End Region
#Region "Import/Export "

    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        sql = "select Ship_To_Type_Code,Ship_To_Type_Desc,Ship_To_Type,Ship_To_Code,Ship_To_Desc, Add1,Add2,Add3,Add4,City_Code,State,Pin_Code,Country,Telphone,Email from TSPL_SHIP_TO_LOCATION "
        transportSql.ExporttoExcel(sql, Me)
    End Sub

    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Ship_To_Type_Code", "Ship_To_Type_Desc", "Ship_To_Type", "Ship_To_Code", "Ship_To_Desc", "Add1", "Add2", "Add3", "Add4", "City_Code", "State", "Pin_Code", "Country", "Telphone", "Email") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strCCode As String
                    Dim strCName As String
                    Dim strStType As String
                    Dim strLCode As String
                    Dim strLDesc As String
                    Dim strAdd1 As String
                    Dim strAdd2 As String
                    Dim strAdd3 As String
                    Dim strAdd4 As String
                    Dim strCity As String
                    Dim strStProvince As String
                    Dim strPCode As String
                    Dim strCountry As String
                    Dim strTelephone As String
                    Dim strEmail As String
                    If grow.Cells(0).Value.ToString() = String.Empty Then
                        Throw New Exception("Customer Code can't be blank")
                        
                    ElseIf grow.Cells(0).Value.ToString().Length > 12 Then
                        Throw New Exception("Customer Code be greater than 12 length")
                    Else
                        strCCode = grow.Cells(0).Value.ToString().ToUpper()
                    End If
                    If grow.Cells(1).Value.ToString().Length > 50 Then
                        Throw New Exception("Customer Name cannot be greater than 50 length")
                       
                    Else
                        strCName = grow.Cells(1).Value.ToString()
                    End If
                    strStType = grow.Cells(2).Value.ToString()

                    If strStType = "S" Then
                        strStType = "S"
                    Else
                        Throw New Exception("ShiP To Type must be S.")

                    End If
                    If grow.Cells(3).Value.ToString() = String.Empty Then
                        Throw New Exception("Location Code")
                        
                    ElseIf grow.Cells(3).Value.ToString().Length > 12 Then
                        Throw New Exception("Location Code be greater than 12 length")
                    Else
                        strLCode = grow.Cells(3).Value.ToString().ToUpper()
                    End If

                    If grow.Cells(4).Value.ToString().Length > 50 Then
                        Throw New Exception("Location Description cannot be greater than 50 length")
                        
                    Else
                        strLDesc = grow.Cells(4).Value.ToString()
                    End If


                    If grow.Cells(5).Value.ToString().Length > 50 Then
                        Throw New Exception("Address1 cannot be greater than 50 length")
                         
                    Else
                        strAdd1 = grow.Cells(5).Value.ToString()
                    End If
                    If grow.Cells(6).Value.ToString().Length > 50 Then
                        Throw New Exception("Address2 cannot be greater than 50 length")
                         
                    Else
                        strAdd2 = grow.Cells(6).Value.ToString()
                    End If
                    If grow.Cells(7).Value.ToString().Length > 50 Then
                        Throw New Exception("Address3 cannot be greater than 50 length")
                         
                    Else
                        strAdd3 = grow.Cells(7).Value.ToString()
                    End If
                    If grow.Cells(8).Value.ToString().Length > 50 Then
                        Throw New Exception("Address4 cannot be greater than 50 length")
                       
                    Else
                        strAdd4 = grow.Cells(8).Value.ToString()
                    End If
                    If grow.Cells(9).Value.ToString().Length > 50 Then
                        Throw New Exception("City cannot be greater than 50 length")
                         
                    Else
                        strCity = grow.Cells(9).Value.ToString()
                    End If
                    If grow.Cells(10).Value.ToString().Length > 50 Then
                        Throw New Exception("State/Province cannot be greater than 50 length")
                      
                    Else
                        strStProvince = grow.Cells(10).Value.ToString()
                    End If
                    If grow.Cells(11).Value.ToString().Length > 9 Then
                        Throw New Exception("Postal Code cannot be greater than 9 length")
                        
                    ElseIf Not IsNumeric(grow.Cells(11).Value) And grow.Cells(11).Value.ToString().Length > 0 Then
                        Throw New Exception("Char value not allowed in Postal Code.")
                       
                    Else
                        strPCode = grow.Cells(11).Value.ToString()
                    End If
                    If grow.Cells(12).Value.ToString().Length > 50 Then
                        Throw New Exception("Country cannot be greater than 50 length")
                        
                    Else
                        strCountry = grow.Cells(12).Value.ToString()
                    End If
                    If grow.Cells(13).Value.ToString().Length > 50 Then
                        Throw New Exception("Telephone cannot be greater than 50 length")
                        
                    Else
                        strTelephone = grow.Cells(13).Value.ToString()
                    End If
                    If grow.Cells(14).Value.ToString().Length > 50 Then
                        Throw New Exception("Email cannot be greater than 50 length")
                        
                    ElseIf grow.Cells(14).Value.ToString <> String.Empty Then
                        strEmail = grow.Cells(14).Value.ToString()
                        Dim re As Regex = New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                        If Not re.IsMatch(strEmail) Then
                            Throw New Exception("Email has some incorrect values")
                            
                        End If
                    Else
                        strEmail = grow.Cells(14).Value.ToString()
                    End If
                    Dim sql1 As String = "select COUNT(*) from TSPL_SHIP_TO_LOCATION  where Ship_To_Code='" + strLCode + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_TSPL_SHIP_TO_LOCATION_insert", New SqlParameter("@ShipToTypeCode", strCCode), New SqlParameter("@ShipToTypeDesc", strCName), New SqlParameter("@ShipToType", strStType), New SqlParameter("@ShipToCode", strLCode), New SqlParameter("@ShipToDesc", strLDesc), New SqlParameter("@Add1", strAdd1), New SqlParameter("@Add2", strAdd2), New SqlParameter("@Add3", strAdd3), New SqlParameter("@Add4", strAdd4), New SqlParameter("@CityCode", strCity), New SqlParameter("@State", strStProvince), New SqlParameter("@PinCode", strPCode), New SqlParameter("@Country", strCountry), New SqlParameter("@Telephone", strTelephone), New SqlParameter("@Email", strEmail), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate(trans)), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_TSPL_SHIP_TO_LOCATION_update", New SqlParameter("@ShipToTypeCode", strCCode), New SqlParameter("@ShipToTypeDesc", strCName), New SqlParameter("@ShipToType", strStType), New SqlParameter("@ShipToCode", strLCode), New SqlParameter("@ShipToDesc", strLDesc), New SqlParameter("@Add1", strAdd1), New SqlParameter("@Add2", strAdd2), New SqlParameter("@Add3", strAdd3), New SqlParameter("@Add4", strAdd4), New SqlParameter("@CityCode", strCity), New SqlParameter("@State", strStProvince), New SqlParameter("@PinCode", strPCode), New SqlParameter("@Country", strCountry), New SqlParameter("@Telephone", strTelephone), New SqlParameter("@Email", strEmail), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate(trans)), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode))
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
#End Region
#Region "Print"
    Private Sub menuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuPrint.Click
        Dim stl As New FrmShipToLocationReport_vb()
        stl.ShowDialog()
    End Sub
#End Region


    Private Sub frmShipToLocation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()

        End If
    End Sub



    Private Sub fndCustomer__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        Dim str As String = "select count(*) from TSPL_CUSTOMER_MASTER where  Cust_Code ='" + fndCustomer.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            fndCustomer.MyReadOnly = False
        Else
            fndCustomer.MyReadOnly = True
        End If
        If fndCustomer.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Cust_Code as Code,Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER "
            fndCustomer.Value = clsCommon.ShowSelectForm("fmCUST_CODE", qry, "Code", "", fndCustomer.Value, "", isButtonClicked)
            txtCustomerName.Text = clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code ='" + fndCustomer.Value + "'")
            LoadData()
        End If
    End Sub

    Private Sub fndCustomer__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCustomer._MYNavigator
        Dim qst As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in ('" + fndCustomer.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select min(Cust_Code ) from TSPL_CUSTOMER_MASTER where Cust_Code  >'" + fndCustomer.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select MIN(Cust_Code ) from TSPL_CUSTOMER_MASTER)"

            Case NavigatorType.Last
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select Max(Cust_Code ) from TSPL_CUSTOMER_MASTER)"
            Case NavigatorType.Previous
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select Max(Cust_Code ) from TSPL_CUSTOMER_MASTER where Cust_Code  <'" + fndCustomer.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndCustomer.Value = clsCommon.myCstr(dt.Rows(0)("Customer Code"))
            txtCustomerName.Text = clsCommon.myCstr(dt.Rows(0)("Customer Name"))

        End If
        LoadData()
    End Sub

    Private Sub MasterTemplate_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles MasterTemplate.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    
    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

    End Sub

    Private Sub fndCustomer_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndCustomer.Load

    End Sub
End Class

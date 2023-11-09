Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common

Public Class FrmItemExciseMapping
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If fnditem.Value = "" Then
                myMessages.blankValue("Item Code")
                fnditem.Focus()
            ElseIf txtuom.Text = "" Then
                myMessages.blankValue("UOM Code")
                txtuom.Focus()
            Else
                If btnsave.Text = "Save" Then
                    funinsert()
                ElseIf btnsave.Text = "Update" Then
                    funupdate()
                End If
            End If
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fnditem.Value = "" Then
            myMessages.blankValue("Item Code")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Private Sub FrmItemExciseMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ItemExciseMapping)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            Import.Enabled = True
            Export.Enabled = True
        Else
            Import.Enabled = False
            Export.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmItemExciseMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtcess.MaxLength = 9
        txtexcise.MaxLength = 9
        txthcess.MaxLength = 9
        txtuom.MaxLength = 12
        funreset()
        ToolTip1.SetToolTip(btnnew, "New")

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITEX-MAP-M"
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
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub fnditem__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnditem._MYValidating
        Dim qry As String = "select item_code as Code,Item_desc as Description,Unit_Code as UOM from tspl_item_master"
        fnditem.Value = clsCommon.ShowSelectForm("fmItemMaster", qry, "Code", "", fnditem.Value, "Code", isButtonClicked)
        txtuom.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Code from tspl_item_master where item_code='" + fnditem.Value + "'"))

        Dim str As String = "select count(*) from TSPL_Item_Tax where item_code='" + fnditem.Value + "' "
        Dim i As Integer = CInt(connectSql.RunScalar(str))
        If i = 0 Then
            txtexcise.Text = ""
            ' txtuom.Text = ""
            txthcess.Text = ""
            txtcess.Text = ""
            btnsave.Text = "Save"
            btndelete.Enabled = False
        Else

            funfill()

        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub


    'Funtion for insertion of data
    Public Sub funinsert()
        Try
            Dim strexcess As String
            Dim strcess As String
            Dim strhcess As String

            Dim Excise_TaxCode As String
            Dim Ecess_TaxCode As String
            Dim Hcess_TaxCode As String


            If txtexcise.Text = "" Then
                strexcess = 0
            Else
                strexcess = txtexcise.Text
            End If

            If txtcess.Text = "" Then
                strcess = 0
            Else
                strcess = txtcess.Text
            End If

            If txthcess.Text = "" Then
                strhcess = 0
            Else
                strhcess = txthcess.Text
            End If


            If clsCommon.myLen(clsCommon.myCstr(Me.fndExciseTaxCode.Value)) > 0 Then
                Excise_TaxCode = "'" & clsCommon.myCstr(Me.fndExciseTaxCode.Value) & "'"
            Else
                Excise_TaxCode = "NULL"
            End If

            If clsCommon.myLen(clsCommon.myCstr(Me.fndEcessTaxCode.Value)) > 0 Then
                Ecess_TaxCode = "'" & clsCommon.myCstr(Me.fndEcessTaxCode.Value) & "'"
            Else
                Ecess_TaxCode = "NULL"
            End If

            If clsCommon.myLen(clsCommon.myCstr(Me.fndHcessTaxCode.Value)) > 0 Then
                Hcess_TaxCode = "'" & clsCommon.myCstr(Me.fndHcessTaxCode.Value) & "'"
            Else
                Hcess_TaxCode = "NULL"
            End If

            Dim qry As String = "insert into TSPL_Item_Tax(item_code,unit_code,Excise,Ecess,Hcess,Tax_Code_Excise,Tax_Code_Ecess,Tax_Code_Hcess) values('" + fnditem.Value + "','" + Convert.ToString(txtuom.Text) + "','" + Convert.ToString(strexcess) + "','" + Convert.ToString(strcess) + "','" + Convert.ToString(strhcess) + "'," & Excise_TaxCode & "," & Ecess_TaxCode & "," & Hcess_TaxCode & ") "



            connectSql.RunSql(qry)
            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub



    'Funtion for updation  of data
    Public Sub funupdate()
        Try


            Dim strexcess As String
            Dim strcess As String
            Dim strhcess As String

            Dim Excise_TaxCode As String
            Dim Ecess_TaxCode As String
            Dim Hcess_TaxCode As String


            If txtexcise.Text = "" Then
                strexcess = 0
            Else
                strexcess = txtexcise.Text
            End If

            If txtcess.Text = "" Then
                strcess = 0
            Else
                strcess = txtcess.Text
            End If

            If txthcess.Text = "" Then
                strhcess = 0
            Else
                strhcess = txthcess.Text
            End If

            If clsCommon.myLen(clsCommon.myCstr(Me.fndExciseTaxCode.Value)) > 0 Then
                Excise_TaxCode = "'" & clsCommon.myCstr(Me.fndExciseTaxCode.Value) & "'"
            Else
                Excise_TaxCode = "NULL"
            End If

            If clsCommon.myLen(clsCommon.myCstr(Me.fndEcessTaxCode.Value)) > 0 Then
                Ecess_TaxCode = "'" & clsCommon.myCstr(Me.fndEcessTaxCode.Value) & "'"
            Else
                Ecess_TaxCode = "NULL"
            End If

            If clsCommon.myLen(clsCommon.myCstr(Me.fndHcessTaxCode.Value)) > 0 Then
                Hcess_TaxCode = "'" & clsCommon.myCstr(Me.fndHcessTaxCode.Value) & "'"
            Else
                Hcess_TaxCode = "NULL"
            End If
            Dim qry As String = "update TSPL_Item_Tax set unit_code='" + Convert.ToString(txtuom.Text) + "',Excise='" + Convert.ToString(strexcess) + "',Ecess='" + Convert.ToString(strcess) + "',Hcess='" + Convert.ToString(strhcess) + "',Tax_Code_Excise=" & Excise_TaxCode & ",Tax_Code_Ecess=" & Ecess_TaxCode & ",Tax_Code_Hcess=" & Hcess_TaxCode & " where item_code='" + fnditem.Value + "'"
            connectSql.RunSql(qry)
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    'Function for deletion of data
    Public Sub fundelete()
        Try
            Dim qry As String = "delete from TSPL_Item_Tax where item_code='" + fnditem.Value + "'"
            connectSql.RunSql(qry)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub


    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Try

            Dim str As String = "select item_code,unit_code,Excise,Ecess,hcess,Tax_Code_Excise,Tax_Code_Ecess,Tax_Code_Hcess from TSPL_Item_Tax  where item_code = '" + fnditem.Value + "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(str)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    fnditem.Value = dr(0).ToString()
                    txtuom.Text = dr(1).ToString()
                    txtexcise.Text = dr(2).ToString()
                    txtcess.Text = dr(3).ToString()
                    txthcess.Text = dr(4).ToString()
                    fndExciseTaxCode.Value = dr("Tax_Code_Excise").ToString()
                    fndEcessTaxCode.Value = dr("Tax_Code_Ecess").ToString()
                    fndHcessTaxCode.Value = dr("Tax_Code_Hcess").ToString()

                Next
              


            End If
           
            btnsave.Enabled = True
            btndelete.Enabled = True
            btnsave.Text = "Update"
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If

        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will reset all the controls in screens
    Public Sub funreset()
        fnditem.Value = ""
        txtexcise.Text = ""
        txtuom.Text = ""
        txthcess.Text = ""
        txtcess.Text = ""
        fndExciseTaxCode.Value = Nothing
        fndEcessTaxCode.Value = Nothing
        fndHcessTaxCode.Value = Nothing

        btnsave.Text = "Save"
        btndelete.Enabled = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        Dim str As String
        str = "select item_code as [Item Code],unit_code as [Unit Code],Excise as [Excise],Tax_Code_Excise AS [Excise Tax Code] ,Ecess as [Ecess],Tax_Code_Ecess as [Ecess Tax Code],Hcess as [Hcess],Tax_Code_Hcess as [Hcess Tax Code] from TSPL_Item_Tax  "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Item Code", "Unit Code", "Excise", "Excise Tax Code", "Ecess", "Ecess Tax Code", "Hcess", "Hcess Tax Code") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim itemcode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If itemcode.Length > 12 Then
                        Throw New Exception("Check the length of 'Item Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim acc1 As String = "select count(*) from tspl_Item_master where item_code='" + itemcode + "'"
                    Dim no As Integer = CInt(connectSql.RunScalar(trans, acc1))
                    If no = 0 Then
                        Throw New Exception("This  '" + itemcode + "'  Itemcode  does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim uom As String = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    If uom.Length > 12 Then
                        Throw New Exception("Check the length of 'UOM'.")
                        trans.Rollback()
                        Exit Sub
                    End If




                    Dim strexcise As String = clsCommon.myCstr(grow.Cells("Excise").Value)
                    Dim strexciseTaxCode As String = clsCommon.myCstr(grow.Cells("Excise Tax Code").Value)
                    Dim strcheckTaxCode As String
                    Dim count As Integer = 0

                    If strexcise.Length < 18 And IsNumeric(strexcise) Then
                        If strexciseTaxCode.Length = 0 Then
                            Throw New Exception("Check the value of 'Excise Tax Code'.")
                            trans.Rollback()
                        Else
                            strcheckTaxCode = "select count(*) from TSPL_TAX_MASTER where tax_code='" + strexciseTaxCode + "'"
                            count = CInt(connectSql.RunScalar(trans, strcheckTaxCode))
                            If count = 0 Then
                                Throw New Exception("This  '" + strexciseTaxCode + "'  Tax Code does not exist")
                                trans.Rollback()
                                Exit Sub
                            End If
                        End If
                    Else
                        Throw New Exception("Check the value of 'Excise'.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strcess As String = clsCommon.myCstr(grow.Cells("Ecess").Value)
                    Dim strcessTaxCode As String = clsCommon.myCstr(grow.Cells("Ecess Tax Code").Value)

                    If strcess.Length < 18 And IsNumeric(strcess) Then
                        If strcessTaxCode.Length = 0 Then
                            Throw New Exception("Check the value of 'Ecess Tax Code'.")
                            trans.Rollback()
                        Else
                            strcheckTaxCode = "select count(*) from TSPL_TAX_MASTER where tax_code='" + strcessTaxCode + "'"
                            count = CInt(connectSql.RunScalar(trans, strcheckTaxCode))
                            If count = 0 Then
                                Throw New Exception("This  '" + strcessTaxCode + "'  Tax Code does not exist")
                                trans.Rollback()
                                Exit Sub
                            End If
                        End If
                    Else
                        Throw New Exception("Check the value of 'Ecess'.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim strhcess As String = clsCommon.myCstr(grow.Cells("Hcess").Value)
                    Dim strhcessTaxCode As String = clsCommon.myCstr(grow.Cells("Hcess Tax Code").Value)

                    If strhcess.Length < 18 And IsNumeric(strhcess) Then
                        If strhcessTaxCode.Length = 0 Then
                            Throw New Exception("Check the value of 'Hcess Tax Code'.")
                            trans.Rollback()
                        Else
                            strcheckTaxCode = "select count(*) from TSPL_TAX_MASTER where tax_code='" + strhcessTaxCode + "'"
                            count = CInt(connectSql.RunScalar(trans, strcheckTaxCode))
                            If count = 0 Then
                                Throw New Exception("This  '" + strhcessTaxCode + "'  Tax Code does not exist")
                                trans.Rollback()
                                Exit Sub
                            End If
                        End If
                    Else
                        Throw New Exception("Check the value of 'Hcess'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_Item_Tax where item_code='" + itemcode + "'  "
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        Dim qry As String = "insert into TSPL_Item_Tax(item_code,unit_code,Excise,Ecess,Hcess,Tax_Code_Excise,Tax_Code_Ecess,Tax_Code_Hcess) values('" + Convert.ToString(itemcode) + "','" + Convert.ToString(uom) + "','" + Convert.ToString(strexcise) + "','" + Convert.ToString(strcess) + "','" + Convert.ToString(strhcess) + "','" + Convert.ToString(strexciseTaxCode) + "','" + Convert.ToString(strcessTaxCode) + "','" + Convert.ToString(strhcessTaxCode) + "') "
                        connectSql.RunSqlTransaction(trans, qry)
                    Else
                        Dim qry As String = "update TSPL_Item_Tax set unit_code='" + Convert.ToString(uom) + "',Excise='" + Convert.ToString(strexcise) + "',Ecess='" + Convert.ToString(strcess) + "',Hcess='" + Convert.ToString(strhcess) + "',Tax_Code_Excise='" + Convert.ToString(strexciseTaxCode) + "',Tax_Code_Ecess='" + Convert.ToString(strcessTaxCode) + "',Tax_Code_Hcess='" + Convert.ToString(strhcessTaxCode) + "' where item_code='" + itemcode + "'"
                        connectSql.RunSqlTransaction(trans, qry)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub txtexcise_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtcess_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txthcess_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub fndEcessTaxCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndEcessTaxCode._MYValidating
        Dim qry As String = "select Tax_Code as Code,Tax_Code_Desc as Description,Tax_Liability_Account from TSPL_TAX_MASTER"
        fndEcessTaxCode.Value = clsCommon.ShowSelectForm("TSPL_TAX_MASTER", qry, "Code", "", fndEcessTaxCode.Value, "Code", isButtonClicked)


    End Sub

    Private Sub fndExciseTaxCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndExciseTaxCode._MYValidating
        Dim qry As String = "select Tax_Code as Code,Tax_Code_Desc as Description,Tax_Liability_Account from TSPL_TAX_MASTER"
        fndExciseTaxCode.Value = clsCommon.ShowSelectForm("TSPL_TAX_MASTER", qry, "Code", "", fndExciseTaxCode.Value, "Code", isButtonClicked)
    End Sub

    Private Sub fndHcessTaxCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndHcessTaxCode._MYValidating
        Dim qry As String = "select Tax_Code as Code,Tax_Code_Desc as Description,Tax_Liability_Account from TSPL_TAX_MASTER"
        fndHcessTaxCode.Value = clsCommon.ShowSelectForm("TSPL_TAX_MASTER", qry, "Code", "", fndHcessTaxCode.Value, "Code", isButtonClicked)
    End Sub
End Class

'****** Created By: Pankaj Kumar **********
'****** Start Date: 19/07/2012 **********
'****** Table: TSPL_CUSTOMER_INFO  ******** 
'--20/12/2012--Updation By --Pankaj Kumar--- Applied Validations
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
Imports System.Globalization
Public Class FrmCustomerInfo
    Inherits FrmMainTranScreen
    Private isNewEntry As Boolean = True
    Private isCellValueChangedOpen As Boolean = False
    Dim userCode, companyCode As String
    Dim CustId As String
    Dim Custname As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnCustomerInfo)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
#Region "Variable"
    Dim strCmd As String
    Dim myDt As DataTable
    Dim myDr As SqlDataReader
    Dim myDs As DataSet
    Dim myDataTable As DataTable
    Dim i As Integer
#End Region

#Region "Button Click"

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Me.Close()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (AllowToSave()) Then
            Try
                If btnSave.Text = "Save" Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                Dim obj As New clsCustomerInfo()
                obj.Cust_Code = clsCommon.myCstr(fndCustomer.Value)
                obj.Customer_Name = clsCommon.myCstr(txtCustomerName.Text)
                obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
                obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
                obj.Add3 = clsCommon.myCstr(txtAdd3.Text)
                obj.Cust_Group_Code = clsCommon.myCstr(fndCusgrp.Value)
                obj.City_Code = clsCommon.myCstr(fndCity.Value)
                obj.State = clsCommon.myCstr(fndstate.Value)
                obj.Country = clsCommon.myCstr(txtCountry.Text)
                obj.Phone1 = clsCommon.myCstr(txtPhone1.Text)
                obj.Phone2 = clsCommon.myCstr(txtPhone2.Text)
                obj.Fax = clsCommon.myCstr(txtfax.Text)
                obj.Email = clsCommon.myCstr(txtEmail.Text)
                obj.WebSite = clsCommon.myCstr(txtWeb.Text)
                obj.Contact_Person_Name = clsCommon.myCstr(txtContactName.Text)
                obj.Contact_Person_Phone = clsCommon.myCstr(txtContPhone.Text)
                obj.Contact_Person_Fax = clsCommon.myCstr(txtContactFax.Text)
                obj.Contact_Person_Email = clsCommon.myCstr(txtContactEmail.Text)
                obj.Contact_Person_Website = clsCommon.myCstr(txtContactWeb.Text)
                obj.Terms_Code = clsCommon.myCstr(fndTrmsCode.Value)
                obj.Cust_Account = clsCommon.myCstr(fndAccntSet.Value)
                obj.Tax_Group = clsCommon.myCstr(fndTxGrp.Value)
                If (grdTax.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(grdTax.Rows(0).Cells(0).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(grdTax.Rows(0).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 1) Then
                    obj.TAX2 = clsCommon.myCstr(grdTax.Rows(1).Cells(0).Value)
                    obj.TAX2_Rate = clsCommon.myCdbl(grdTax.Rows(1).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 2) Then
                    obj.TAX3 = clsCommon.myCstr(grdTax.Rows(2).Cells(0).Value)
                    obj.TAX3_Rate = clsCommon.myCdbl(grdTax.Rows(2).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 3) Then
                    obj.TAX4 = clsCommon.myCstr(grdTax.Rows(3).Cells(0).Value)
                    obj.TAX4_Rate = clsCommon.myCdbl(grdTax.Rows(3).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 4) Then
                    obj.TAX5 = clsCommon.myCstr(grdTax.Rows(4).Cells(0).Value)
                    obj.TAX5_Rate = clsCommon.myCdbl(grdTax.Rows(4).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 5) Then
                    obj.TAX6 = clsCommon.myCstr(grdTax.Rows(5).Cells(0).Value)
                    obj.TAX6_Rate = clsCommon.myCdbl(grdTax.Rows(5).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 6) Then
                    obj.TAX7 = clsCommon.myCstr(grdTax.Rows(6).Cells(0).Value)
                    obj.TAX7_Rate = clsCommon.myCdbl(grdTax.Rows(6).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 7) Then
                    obj.TAX8 = clsCommon.myCstr(grdTax.Rows(7).Cells(0).Value)
                    obj.TAX8_Rate = clsCommon.myCdbl(grdTax.Rows(7).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 8) Then
                    obj.TAX9 = clsCommon.myCstr(grdTax.Rows(8).Cells(0).Value)
                    obj.TAX9_Rate = clsCommon.myCdbl(grdTax.Rows(8).Cells(1).Value)
                End If
                If (grdTax.Rows.Count > 9) Then
                    obj.TAX10 = clsCommon.myCstr(grdTax.Rows(9).Cells(0).Value)
                    obj.TAX10_Rate = clsCommon.myCdbl(grdTax.Rows(9).Cells(1).Value)
                End If
                obj.Payment_Code = clsCommon.myCstr(fndPayCode.Value)
                obj.Service_Tax_No = clsCommon.myCstr(txtStaxNo.Text)
                obj.Tin_No = clsCommon.myCstr(txtTinNo.Text)
                obj.Lst_No = clsCommon.myCstr(txtLstNo.Text)
                obj.Form_Type = clsCommon.myCstr(drpformtype.Text)
                If chkHold.Checked = True Then
                    obj.OnHold = "Y"                      '******* for:Hold ******** 
                ElseIf chkHold.Checked = False Then
                    obj.OnHold = "N"                      '******* for:Remove Hold ********
                End If

                If chkDistributer.Checked = True Then
                    obj.chkdis = "Y"
                    obj.custdis = fndcust.Value
                ElseIf chkDistributer.Checked = False Then
                    obj.chkdis = "N"
                    obj.custdis = ""
                End If

                obj.Cust_Type = clsCommon.myCstr(ddlCustType.SelectedValue)
                obj.Remarks1 = clsCommon.myCstr(txtRemarks1.Text)
                obj.Remarks2 = clsCommon.myCstr(txtRemarks2.Text)
                obj.Additional1 = clsCommon.myCstr(txtAddInfo1.Text)
                obj.Additional2 = clsCommon.myCstr(txtAddInfo2.Text)
                obj.Additional3 = clsCommon.myCstr(txtAddInfo3.Text)
                obj.Credit_Limit = clsCommon.myCdbl(txtCredit.Text)
                obj.CST = clsCommon.myCstr(txtcst.Text)
                obj.ECC = clsCommon.myCstr(txtecc.Text)
                obj.Range = clsCommon.myCstr(txtrange.Text)
                obj.Collectorate = clsCommon.myCstr(txtcollect.Text)
                'obj.Collectorate = clsCommon.myCstr(txtcollect.Text)
                obj.PAN = clsCommon.myCstr(txtpan.Text)
                obj.Division = clsCommon.myCstr(txtdivision.Text)
                If chkcredit.Checked = True Then
                    obj.Credit_Customer = "Y"
                ElseIf chkcredit.Checked = False Then
                    obj.Credit_Customer = "N"
                End If

                If chkInterBranch.Checked = True Then
                    obj.Inter_Branch = "Y"
                Else
                    obj.Inter_Branch = "N"
                End If
                '-----------------------------Visi Detail-------
                For Each grow As GridViewRowInfo In dgvVisi.Rows
                    If (grow.Cells("Select").Value) = True Then
                        obj.ArrVisi.Add(clsCommon.myCstr(grow.Cells("Visi Id").Value))
                    End If
                Next
                '-----------------------------------------------
                Dim isSaved As Boolean = obj.SaveData(obj, obj.ArrVisi, isNewEntry)

                If btnSave.Text = "Save" Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved SuccessFully", Me.Text)
                    btnSave.Text = "Update"
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Data Updated SuccessFully", Me.Text)
                End If
                LoadData(fndCustomer.Value, NavigatorType.Current)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    'Public Sub ff()
    '    Dim ArrIn As New List(Of String)
    '    'Dim Arr As New List(Of clsCustomerToVisi)
    '    CustId = clsCommon.myCstr(fndCustomer.Value)
    '    Custname = clsCommon.myCstr(txtCustomerName.Text)

    '    For Each grow As GridViewRowInfo In dgvVisi.Rows
    '        'Dim obj As New clsCustomerToVisi()
    '        If (grow.Cells("Select").Value) = True Then
    '            Dim strVisiId As String = grow.Cells("Visi Id").Value
    '            ArrIn.Add(strVisiId)
    '        End If
    '    Next
    '    Dim QryVisi As String = "Update TSPL_VISI_MASTER set Customer_Id='" + CustId + "', Customer_name='" + Custname + "' where Visi_Id in (" + clsCommon.GetMulcallString(ArrIn) + ")"
    '    clsDBFuncationality.ExecuteNonQuery(QryVisi, trans)





    '    Dim ArrIn As New List(Of String)
    '    'Dim Arr As New List(Of clsCustomerToVisi)
    '    CustId = clsCommon.myCstr(fndCustomer.Value)
    '    Custname = clsCommon.myCstr(txtCustomerName.Text)
    '    For Each grow As GridViewRowInfo In dgvVisi.Rows
    '        Dim QryVisiRemove As String = "Update TSPL_VISI_MASTER SET Customer_Id='', Customer_Name='' where Customer_Id='" + CustId + "' And Visi_Id='" + clsCommon.myCstr(grow.Cells("Visi Id").Value) + "'"
    '        clsDBFuncationality.ExecuteNonQuery(QryVisiRemove, trans)
    '        'Dim obj As New clsCustomerToVisi()
    '        If (grow.Cells("Select").Value) = True Then
    '            Dim strVisiId As String = grow.Cells("Visi Id").Value
    '            ArrIn.Add(strVisiId)
    '        End If
    '    Next
    '    Dim QryVisi As String = "Update TSPL_VISI_MASTER set Customer_Id='" + CustId + "', Customer_name='" + Custname + "' where Visi_Id in (" + clsCommon.GetMulcallString(ArrIn) + ")"
    '    clsDBFuncationality.ExecuteNonQuery(QryVisi, trans)







    'End Sub









    Function AllowToSave() As Boolean
        Try
            If fndCustomer.Value <> "" Then
                If clsCommon.myLen(fndCusgrp.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select Customer Group", "Customer", MessageBoxButtons.OK, Me.Text)
                    fndCusgrp.Focus()
                    Return False
                ElseIf clsCommon.myLen(fndAccntSet.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select Customer Account Set", "Customer", MessageBoxButtons.OK, Me.Text)
                    fndAccntSet.Focus()
                    Return False
                ElseIf clsCommon.myLen(ddlCustType.SelectedValue) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select Customer Type", "Customer", MessageBoxButtons.OK, Me.Text)
                    ddlCustType.Focus()
                    Return False
                ElseIf chkDistributer.Checked = True Then
                    If fndcust.Value = "" Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please select the Customer ", "Customer", MessageBoxButtons.OK, Me.Text)
                        Return False
                    End If
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Customer Found to Save", "Customer", MessageBoxButtons.OK, Me.Text)
                Return False
            End If
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        fndCustomer.Value = ""
        Reset()
    End Sub

#End Region

#Region "Page Load"
    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadVisi()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for Delete")
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+R Reset the Window")
        SetUserMgmtNew()
        Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        pageCus.SelectedPage = RadPageViewPage1
        txtCredit.MaxLength = 12
        btnDelete.Enabled = False
        drpformtype.SelectedIndex = 0
        Reset()
        ValidateLength()
        ApplyReadOPR()
        grdTax.AllowDeleteRow = False
    End Sub

    Private Sub ValidateLength()
        fndCustomer.MyMaxLength = 12
        txtCustomerName.MaxLength = 200
        txtAdd1.MaxLength = 50
        txtAdd2.MaxLength = 50
        txtAdd3.MaxLength = 50
        txtCity.MaxLength = 50
        txtStateName.MaxLength = 50
        txtCountry.MaxLength = 50
        txtfax.MaxLength = 12
        txtPhone1.MaxLength = 20
        txtPhone2.MaxLength = 20
        txtEmail.MaxLength = 50
        txtWeb.MaxLength = 50
        txtContactFax.MaxLength = 20
        txtContPhone.MaxLength = 20
        txtContactEmail.MaxLength = 50
        txtContactName.MaxLength = 50
        txtContactWeb.MaxLength = 50
        txtRemarks1.MaxLength = 200
        txtRemarks2.MaxLength = 200
        txtAddInfo1.MaxLength = 50
        txtAddInfo2.MaxLength = 50
        txtAddInfo3.MaxLength = 50
        txtcst.MaxLength = 30
        txtecc.MaxLength = 30
        txtrange.MaxLength = 30
        txtcollect.MaxLength = 30
        txtpan.MaxLength = 30
        txtdivision.MaxLength = 30

    End Sub

    Private Sub ApplyReadOPR()
        txtCusgrp.ReadOnly = True
        txtCity.ReadOnly = True
        txtStateName.ReadOnly = True
    End Sub

#End Region

#Region "Finder"

    Private Sub fnCusGrp()

        Try
            Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," & _
                           " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + fndCusgrp.Value + "' "
            myDs = connectSql.RunSQLReturnDS(strCmd1)
            If myDs.Tables(0).Rows.Count > 0 Then
                Dim row As DataRow = myDs.Tables(0).Rows(0)
                Me.txtCusgrp.Text = clsCommon.myCstr(row(1).ToString().Trim())
                Me.fndTxGrp.Value = clsCommon.myCstr(row(2).ToString().Trim())
                Me.fndAccntSet.Value = clsCommon.myCstr(row(3).ToString().Trim())
                Me.fndTrmsCode.Value = clsCommon.myCstr(row(4).ToString().Trim())
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Private Function fnCity(ByVal strCityId As String) As String
        Dim strCityName As String = ""
        Try
            strCmd = "SELECT City_Code as [City Code],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER] where City_Code =  '" + strCityId + "'  "
            'myDr = connectSql.RunSqlReturnDR(strCmd)

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strCmd)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each myDr As DataRow In dt1.Rows
                    strCityName = clsCommon.myCstr(myDr(1).ToString().Trim())
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
        Return strCityName
    End Function
    Private Sub City_Event(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtCity.Text = fnCity(Me.fndCity.Value)
    End Sub

    Private Function fnCusCatg(ByVal strCatgId As String) As String
        Dim strName As String
        strName = ""
        Try
            strCmd = "SELECT [CUST_CATEGORY_CODE] as [Customer Category Code],[CUST_CATEGORY_DESC] as [Description],[Price_Code] FROM [TSPL_CUSTOMER_CATEGORY_MASTER] where CUST_CATEGORY_CODE='" + strCatgId + "' "
            'myDr = connectSql.RunSqlReturnDR(strCmd)

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strCmd)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each myDr As DataRow In dt1.Rows
                    strName = clsCommon.myCstr(myDr(2).ToString().Trim())
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
        Return strName
    End Function

    Private Function fnTaxGrp(ByVal strTaxGrpId As String) As String
        Dim strTaxGrpDesc As String
        strTaxGrpDesc = ""
        Try
            strCmd = "SELECT [Tax_Group_Code] AS [Tax Group Code],[Tax_Group_Desc] as [Description]," & _
                        " (select case when [Tax_Group_Type]='S' then 'Sale' else 'Purchase' end) as [Type] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + strTaxGrpId + "'"

            'myDr = connectSql.RunSqlReturnDR(strCmd)

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strCmd)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each myDr As DataRow In dt1.Rows
                    strTaxGrpDesc = clsCommon.myCstr(myDr(1).ToString().Trim())
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
        Return strTaxGrpDesc
    End Function
    Private Sub TaxGrp_Event(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtTxGrp.Text = fnTaxGrp(Me.fndTxGrp.Value)
    End Sub
#End Region

#Region "Function"


    Public Sub LoadVisi()
        Try
            'dgvVisi.Rows.Clear()
            'dgvVisi.Columns.Clear()
            dgvVisi.DataSource = Nothing
            Dim Qry As String = "select  CAST(0 as BIT ) as [Select], Visi_Id as [Visi Id], VisiMake as [Visi Make], Visi_Chasis_No as [Chasis No], Visi_Installation_date as [Installation Date] from TSPL_VISI_MASTER Where Customer_Id='' And  Customer_name=''"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            dgvVisi.DataSource = dt
            FormatGrid()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()
        'For Each grow As GridViewRowInfo In dgvVisi.Rows
        '    If grow.Cells("Select").Value = True Then
        '        grow.Cells("Select").ReadOnly = True
        '    Else
        '        grow.Cells("Select").ReadOnly = False
        '    End If
        'Next
        Me.dgvVisi.MasterTemplate.Columns("Select").ReadOnly = False
        Me.dgvVisi.MasterTemplate.Columns("Visi Id").Width = 71
        Me.dgvVisi.MasterTemplate.Columns("Visi Id").ReadOnly = True
        Me.dgvVisi.MasterTemplate.Columns("Visi Make").Width = 151
        Me.dgvVisi.MasterTemplate.Columns("Visi Make").ReadOnly = True
        Me.dgvVisi.MasterTemplate.Columns("Chasis No").Width = 151
        Me.dgvVisi.MasterTemplate.Columns("Chasis No").ReadOnly = True
        Me.dgvVisi.MasterTemplate.Columns("Installation Date").Width = 101
        Me.dgvVisi.MasterTemplate.Columns("Installation Date").ReadOnly = True
        dgvVisi.ShowFilteringRow = True
    End Sub

    Public Sub FillCustType()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt1.NewRow()
        dr("Code") = ""
        dr("Name") = "---Select---"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Route"
        dr("Name") = "Route"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Other"
        dr("Name") = "Other"
        dt1.Rows.Add(dr)

        ddlCustType.DataSource = dt1
        ddlCustType.DisplayMember = "Name"
        ddlCustType.ValueMember = "Code"
    End Sub

    Public Sub funDelete()
        Try
            If clsCommon.myLen(fndCustomer.Value) = 0 Then
                Throw New Exception("Please Select a Record for Delete")
                fndCustomer.Focus()
                Return
            Else
                If (myMessages.deleteConfirm()) Then
                    If (clsCustomerInfo.DeleteData(fndCustomer.Value)) Then
                        Dim QryVisi As String = "Update TSPL_VISI_MASTER set Customer_Id='', Customer_name='' where Customer_Id='" + fndCustomer.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(QryVisi)
                        common.clsCommon.MyMessageBoxShow(Me, "Record Deleted Successfully ", Me.Text)
                        Reset()
                    End If
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Public Sub Reset()
        fndCustomer.MyReadOnly = False
        Me.fndCusgrp.Value = ""
        Me.fndTrmsCode.Value = ""
        Me.fndAccntSet.Value = ""
        Me.fndTxGrp.Value = ""
        Me.fndPayCode.Value = ""
        Me.fndCity.Value = ""
        Me.txtTxGrp.Text = ""
        Me.txtCustomerName.Text = ""
        Me.txtCusgrp.Text = ""
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        chkHold.Checked = False
        chkcredit.Checked = False
        Me.fndstate.Value = ""
        Me.txtCountry.Text = ""
        Me.txtPhone1.Text = ""
        Me.txtPhone2.Text = ""
        Me.txtfax.Text = ""
        Me.txtEmail.Text = ""
        Me.txtTinNo.Text = ""
        Me.drpformtype.SelectedIndex = 0
        Me.txtWeb.Text = ""
        Me.txtContactName.Text = ""
        Me.txtContPhone.Text = ""
        Me.txtContactFax.Text = ""
        Me.txtContactWeb.Text = ""
        Me.txtContactEmail.Text = ""
        Me.grdTax.DataSource = Nothing
        Me.grdTax.Rows.Clear()
        Me.txtStaxNo.Text = ""
        Me.txtLstNo.Text = ""
        Me.txtRemarks1.Text = ""
        Me.txtRemarks2.Text = ""
        Me.txtAddInfo1.Text = ""
        Me.txtAddInfo2.Text = ""
        Me.txtAddInfo3.Text = ""
        Me.txtCredit.Text = "0.00"
        Me.txtcst.Text = ""
        Me.txtecc.Text = ""
        Me.txtrange.Text = ""
        Me.txtcollect.Text = ""
        Me.txtpan.Text = ""
        Me.txtdivision.Text = ""
        Me.chkInterBranch.Checked = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        txtStateName.Text = ""
        txtCity.Text = ""
        chkDistributer.Checked = False
        fndcust.Value = ""
        fndcust.Visible = False
        lblcust.Visible = False
        pageCus.Pages("RadPageViewPage3").Enabled = False '  Disables The Visi Page
        LoadVisi()
        FillCustType()
    End Sub


    Public Sub funitem_Export()
        Try
            ''strCmd = "select Cust_Code as [Customer Code],Item_Code as [Item Code],Unit_code as [Unit Code],Disc_Amt as [Discount Amount],valid_upto as [Valid Upto] from TSPL_CUSTOMER_ITEM_DISCOUNT_DETAILS "
            strCmd = "select Cust_Code as [Customer Code],Item_Code as [Item Code],Unit_code as [Unit Code],Disc_Amt as [Discount Amount], REPLACE( Convert(varchar(11) ,Start_Date,102),'.','-') as [Start Date] ,REPLACE( Convert(varchar(11) ,Valid_Upto,102),'.','-') as [Valid Upto] from TSPL_CUSTOMER_ITEM_DISCOUNT_DETAILS "
            ' strCmd = "select Cust_Code as [Customer Code],Item_Code as [Item Code],Unit_code as [Unit Code],Disc_Amt as [Discount Amount],  Start_Date as [Start Date] ,REPLACE( Convert(varchar(11) ,Valid_Upto,102),'.','-') as [Valid Upto] from TSPL_CUSTOMER_ITEM_DISCOUNT_DETAILS "
            transportSql.ExporttoExcel(strCmd, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Public Sub funitem_Import()
        Dim gv As New RadGridView()
        Dim intCounter As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Try
            If transportSql.importExcel(gv, "Customer Code", "Item Code", "Unit Code", "Discount Amount", "Start Date", "Valid Upto") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    clsCommon.ProgressBarShow()

                    Dim arrCust As New List(Of String)
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim strCustCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                        If Not arrCust.Contains(strCustCode) And clsCommon.myLen(clsCommon.myCstr(grow.Cells(1).Value)) <> 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(2).Value)) <> 0 Then
                            arrCust.Add(strCustCode)
                        End If
                    Next
                    Dim qry As String
                    'Dim qry As String = "Delete from TSPL_CUSTOMER_ITEM_DISCOUNT_DETAILS where Cust_Code in (" + clsCommon.GetMulcallString(arrCust) + ")"
                    'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    'Update by vipin 0n 6/6/2012 for error on import

                    For Each grow As GridViewRowInfo In gv.Rows
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(0).Value)) <> 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(1).Value)) <> 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(2).Value)) <> 0 Then

                            qry = "Delete from TSPL_CUSTOMER_ITEM_DISCOUNT_DETAILS where Cust_Code = ('" + clsCommon.myCstr(grow.Cells(0).Value) + "') and Item_Code ='" + clsCommon.myCstr(grow.Cells(1).Value) + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            Dim strCusCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                            If strCusCode = "" Then
                                Throw New Exception("Please Fill The Customer Code")
                            End If
                            If clsCommon.myLen(strCusCode) > 12 Then
                                Throw New Exception("Check the length of Customer Code")
                            End If
                            qry = "select cust_code from TSPL_Customer_MASTER  where cust_code='" + strCusCode + "'"
                            Dim CustCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(CustCode) <= 0 Then
                                Throw New Exception("This Customer Code does not exist")
                            End If
                            Dim stritemCode As String = clsCommon.myCstr(grow.Cells(1).Value)
                            If stritemCode = "" Then
                                Throw New Exception("Please Fill The Item Code")
                            End If
                            If clsCommon.myLen(stritemCode) > 50 Then
                                Throw New Exception("Check the length of Item Code")
                            End If

                            qry = "select Item_Code  from TSPL_ITEM_MASTER where Item_Code ='" + stritemCode + "'"
                            Dim stritmCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(stritmCode) <= 0 Then
                                Throw New Exception("This Item Code Does not exist")
                            End If

                            Dim strunitCode As String = clsCommon.myCstr(grow.Cells(2).Value)
                            If clsCommon.myLen(strunitCode) <= 0 Then
                                Throw New Exception("Please Fill The Unit Code")
                            End If
                            If clsCommon.myLen(strunitCode) > 12 Then
                                Throw New Exception("Check the length of Unit Code")
                            End If

                            Dim strdiscAmount As Double = clsCommon.myCdbl(grow.Cells(3).Value)



                            Dim validupto As String = Nothing
                            If clsCommon.myLen(grow.Cells(5).Value) > 0 Then
                                validupto = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells(5).Value), "dd/MMM/yyyy")
                            End If

                            Dim StrstartDate As String = Nothing
                            If clsCommon.myLen(grow.Cells(4).Value) > 0 Then
                                StrstartDate = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells(4).Value), "dd/MMM/yyyy")

                            ElseIf clsCommon.myLen(grow.Cells(4).Value) <= 0 Then
                                Throw New Exception("Item's Start Date can't blank")

                            End If



                            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_CUSTOMER_ITEM_DISCOUNT_DETAILS_INSERT", New SqlParameter("@Cust_Code", CustCode), New SqlParameter("@Item_Code", stritmCode), New SqlParameter("@Unit_Code", strunitCode), New SqlParameter("@Disc_Amt", Convert.ToDecimal(strdiscAmount)), New SqlParameter("@Start_Date", (StrstartDate)), New SqlParameter("@Valid_Upto", (validupto)))
                            intCounter += 1
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Error at row no:" + clsCommon.myCstr(intCounter) + Environment.NewLine + ex.Message)
                    '' myMessages.myExceptions(ex)
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Public Sub funExport()
        Try
            strCmd = " SELECT [Cust_Code] as [Customer Code],[Customer_Name] as [Name] , Cust_Type as [Customer Type], Add1 AS [ADDRESS1],Add2 AS [ADDRESS2], " & _
                     " Add3 AS [ADDRESS3], [Cust_Group_Code] as [Group Code], City_Code as [City Code],[State] as [State], " & _
                    " Country as [Country],Phone1 as [Phone Num1],Phone2 as [Phone Num2],Fax as [Fax Num],Email as [Email Id] , " & _
                    " WebSite As [Website],Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contect Person Phone] , " & _
                    " Contact_Person_Fax as [Contect Person Fax],Contact_Person_Website as [Contact Person website] , " & _
                    " Contact_Person_Email as [Contact Person Email],[Terms_Code] as [Terms Code],[Cust_Account] as [Account Set] , " & _
                    " [Tax_Group] as [Tax Group],TAX1 as [Tax1],TAX1_Rate as [Tax1 Rate],TAX2 as [Tax2],TAX2_Rate as [Tax2 Rate] , " & _
                    " TAX3 as [Tax3],TAX3_Rate as [Tax3 Rate],TAX4 as [Tax4],TAX4_Rate as [Tax4 Rate],TAX5 as [Tax5] , " & _
                    " TAX5_Rate as [Tax5 Rate],TAX6 as [Tax6],TAX6_Rate as [Tax6 Rate],TAX7 as [Tax7],TAX7_Rate as [Tax7 Rate] , " & _
        " TAX8 as [Tax8],TAX8_Rate as [Tax8 Rate],TAX9 as [Tax9],TAX9_Rate as [Tax9 Rate],TAX10 as [Tax10] , " & _
        " TAX10_Rate as [Tax10 Rate],Payment_Code as [Paymemt Code],Service_Tax_No as [Service Tax No] , " & _
        " Tin_No as [Tin No],Lst_No as [List No],Form_Type as [Form Type], " & _
        " Status as [Status],OnHold as [On Hold],Remarks1 as [Remarks1], " & _
        " Remarks2 as [Remarks2],Additional1 as [Additional1],Additional2 as [Additional2],Additional3 as [Additional3] , " & _
        " Credit_Limit as [Credit Limit] , Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify by], " & _
        " Modify_Date as [Modify date],  CST as [Cst],ECC as [Ecc],Range as [Range],Collectorate as [Collectorate],  " & _
        " PAN as [Pan],Division as [Division], Parent_Customer_No as [Parent Customer No],credit_customer as [Credit Customer], " & _
        " Inter_Branch as [Inter Branch],Distributor as [Distributor],custdist as [Distributor Customer] FROM [TSPL_CUSTOMER_INFO] "

            transportSql.ExporttoExcel(strCmd, Me)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Public Sub funImport()

        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Customer Code", "Name", "Customer Type", "ADDRESS1", "ADDRESS2", "ADDRESS3", "Group Code", "City Code", "State", "Country", "Phone Num1", "Phone Num2", "Fax Num", "Email Id", "Website", "Contact Person Name", "Contect Person Phone", "Contect Person Fax", "Contact Person website", "Contact Person Email", "Terms Code", "Account Set", "Tax Group", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Paymemt Code", "Service Tax No", "Tin No", "List No", "Form Type", "Status", "On Hold", "Remarks1", "Remarks2", "Additional1", "Additional2", "Additional3", "Credit Limit", "Created By", "Created Date", "Modify by", "Modify date", "Cst", "Ecc", "Range", "Collectorate", "Pan", "Division", "Parent Customer No", "Credit Customer", "Inter Branch", "Distributor", "Distributor Customer") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim LineNo As String
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 1)
                    Dim strCusCode As String = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                    If clsCommon.myLen(strCusCode) > 0 Then
                        If clsCommon.myLen(strCusCode) > 12 Then
                            Throw New Exception("Check the length  of Customer Code At Line no '" + LineNo + "'")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim strParentCstmrNo As String = clsCommon.myCstr(grow.Cells("Parent Customer No").Value)
                        If clsCommon.myLen(strParentCstmrNo) > 12 Then
                            Throw New Exception("The Maximum Length Of Parant Customer Can Be 12 At '" + LineNo + "'")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim strCusName As String = clsCommon.myCstr(grow.Cells("Name").Value)
                        If strCusName = "" Then
                            Throw New Exception("Please Fill The Customer Name At Line '" + LineNo + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                        If clsCommon.myLen(strCusName) > 200 Then
                            Throw New Exception("The Maximum Length Of Customer Name Can Be 200 At '" + LineNo + "'")
                            trans.Rollback()
                            Exit Sub
                        End If
                        Dim strCusGrp As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                        Dim qry1 As String = "select Cust_Group_Code  from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code ='" + strCusGrp + "'"
                        Dim custgroup As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                        If clsCommon.CompairString(custgroup, strCusGrp) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("The Customer Group Code '" + strCusGrp + "' At '" + LineNo + "' does not Exist")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim strCusTrms As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)
                        Dim qry2 As String = "select Terms_Code  from TSPL_TERMS_MASTER where Terms_Code ='" + strCusTrms + "'"
                        Dim trmscode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry2, trans))
                        If clsCommon.CompairString(trmscode, strCusTrms) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("This Customer Terms Code Does not Exist")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim strCusAccountSet As String = clsCommon.myCstr(grow.Cells("Account Set").Value)
                        Dim qry5 As String = "select Cust_account from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account ='" + strCusAccountSet + "'"
                        Dim custACCOUNT As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry5, trans))
                        If clsCommon.CompairString(custACCOUNT, strCusAccountSet) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception(" This Customer Account Set does not exist")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim strCusTaxGrp As String = clsCommon.myCstr(grow.Cells("Tax Group").Value)
                        Dim qry6 As String = "select tax_group_code from TSPL_TAX_GROUP_MASTER  where Tax_Group_Code  ='" + strCusTaxGrp + "'"
                        Dim custtaxgroup As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry6, trans))
                        If custtaxgroup = strCusTaxGrp Then
                        Else
                            Throw New Exception("This Customer Tax Group does not exist")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim CreditLimit As String = clsCommon.myCdbl(grow.Cells("Credit Limit").Value)
                        Dim Cst As String = clsCommon.myCstr(grow.Cells("Cst").Value)
                        Dim ECC As String = clsCommon.myCstr(grow.Cells("Ecc").Value)
                        Dim Range As String = clsCommon.myCstr(grow.Cells("Range").Value)
                        Dim Collectorate As String = clsCommon.myCstr(grow.Cells("Collectorate").Value)
                        Dim Pan As String = clsCommon.myCstr(grow.Cells("Pan").Value)
                        Dim Division As String = clsCommon.myCstr(grow.Cells("Division").Value)
                        Dim ParentCustNo As String = clsCommon.myCstr(grow.Cells("Parent Customer No").Value)

                        Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                        Dim add2 As String = clsCommon.myCstr(grow.Cells("Address2").Value)
                        Dim add3 As String = clsCommon.myCstr(grow.Cells("Address3").Value)

                        Dim csttypeCode As String = clsCommon.myCstr(grow.Cells("Customer Type").Value)
                        If csttypeCode = "" Then
                            Throw New Exception("Please fill The Customer Type for Customer Code '" + strCusCode + "'")
                            trans.Rollback()
                            Exit Sub
                        End If




                        Dim ctycode As String = clsCommon.myCstr(grow.Cells(11).Value)
                        Dim state As String = clsCommon.myCstr(grow.Cells(12).Value)
                        Dim cuntry As String = clsCommon.myCstr(grow.Cells(13).Value)
                        Dim phnenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                        Dim phnenum2 As String = clsCommon.myCstr(grow.Cells("Phone Num2").Value)
                        Dim faxnum As String = clsCommon.myCstr(grow.Cells("Fax Num").Value)
                        Dim Emaiiid As String = clsCommon.myCstr(grow.Cells("Email Id").Value)
                        Dim website As String = clsCommon.myCstr(grow.Cells("Website").Value)
                        Dim Cntpersonname As String = clsCommon.myCstr(grow.Cells("Contact Person Name").Value)
                        Dim cntpersonphnenum As String = clsCommon.myCstr(grow.Cells("Contect Person Phone").Value)
                        Dim cntpersonfax As String = clsCommon.myCstr(grow.Cells("Contect Person Fax").Value)
                        Dim cntpersonwebsite As String = clsCommon.myCstr(grow.Cells("Contact Person website").Value)
                        Dim cntpersonemail As String = clsCommon.myCstr(grow.Cells("Contact Person Email").Value)
                        Dim tax1 As String = clsCommon.myCstr(grow.Cells("Tax1").Value)
                        Dim tax1rate As String = clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)
                        Dim tax2 As String = clsCommon.myCstr(grow.Cells("Tax2").Value)
                        Dim tax2rate As String = clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)
                        Dim tax3 As String = clsCommon.myCstr(grow.Cells("Tax3").Value)
                        Dim tax3rate As String = clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)
                        Dim tax4 As String = clsCommon.myCstr(grow.Cells("Tax4").Value)
                        Dim tax4rate As String = clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)
                        Dim tax5 As String = clsCommon.myCstr(grow.Cells("Tax5").Value)
                        Dim tax5rate As String = clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)
                        Dim tax6 As String = clsCommon.myCstr(grow.Cells("Tax6").Value)
                        Dim tax6rate As String = clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)
                        Dim tax7 As String = clsCommon.myCstr(grow.Cells("Tax7").Value)
                        Dim tax7rate As String = clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)
                        Dim tax8 As String = clsCommon.myCstr(grow.Cells("Tax8").Value)
                        Dim tax8rate As String = clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)
                        Dim tax9 As String = clsCommon.myCstr(grow.Cells("Tax9").Value)
                        Dim tax9rate As String = clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)
                        Dim tax10 As String = clsCommon.myCstr(grow.Cells("Tax10").Value)
                        Dim tax10rate As String = clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)
                        Dim Additional1 As String = clsCommon.myCstr(grow.Cells("Additional1").Value)
                        Dim Additional2 As String = clsCommon.myCstr(grow.Cells("Additional2").Value)
                        Dim Additional3 As String = clsCommon.myCstr(grow.Cells("Additional3").Value)

                        Dim pymntcode As String = clsCommon.myCstr(grow.Cells("Paymemt Code").Value)
                        Dim servicetaxnum As String = clsCommon.myCstr(grow.Cells("Service Tax No").Value)
                        Dim tinno As String = clsCommon.myCstr(grow.Cells("Tin No").Value)
                        Dim lstnum As String = clsCommon.myCstr(grow.Cells("List No").Value)
                        Dim frmtype As String = clsCommon.myCstr(grow.Cells("Form Type").Value)
                        Dim status As String = clsCommon.myCstr(grow.Cells("Status").Value)
                        Dim remarks1 As String = clsCommon.myCstr(grow.Cells("Remarks1").Value)
                        Dim remarks2 As String = clsCommon.myCstr(grow.Cells("Remarks2").Value)
                        Dim state_code As String
                        Dim qry0 As String = "select state_code from tspl_tds_state_master where state_name='" & fndstate.Value & "'"

                        state_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry0, trans))

                        Dim strcredit As String = clsCommon.myCdbl(grow.Cells("Credit Customer").Value)
                        Dim interbranch As String
                        interbranch = clsCommon.myCstr(grow.Cells("Inter Branch").Value)
                        If interbranch.Length > 1 Then
                            Throw New Exception("Value of Inter Branch should be 'N' or 'Y' At Line '" + LineNo + "'")
                            trans.Rollback()
                            Exit Sub
                        ElseIf String.IsNullOrEmpty(interbranch) Then
                            interbranch = "N"
                        End If

                        Dim strdistrubutor As String = clsCommon.myCstr(grow.Cells("Distributor").Value)
                        If strdistrubutor = "Y" Or strdistrubutor = "N" Then
                        Else
                            common.clsCommon.MyMessageBoxShow(Me, "Value of Distributor type should be 'N' or 'Y' At Line '" + LineNo + "'")
                            trans.Rollback()
                            Exit Sub
                        End If

                        Dim strdistCust As String = clsCommon.myCstr(grow.Cells("Distributor Customer").Value)
                        If clsCommon.myLen(strdistCust) > 30 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Maximum Length Of Distributor Customer At Line '" + LineNo + "' Can be 30")
                            trans.Rollback()
                            Exit Sub
                        End If


                        Dim sql1 As String = "select count(*) from TSPL_CUSTOMER_INFO where Cust_Code='" + strCusCode + "'"
                        Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                        If (i = 0) Then
                            'connectSql.RunSp("sp_TSPL_CUSTOMER_INFO_INSERT", New SqlParameter("@Cust_Code", strCusCode), New SqlParameter("@Customer_Name", strCusName), New SqlParameter("@Add1", add1), New SqlParameter("@Add2", add2), New SqlParameter("@Add3", add3), New SqlParameter("@Cust_Group_Code", custgroup), New SqlParameter("@Cust_Type", csttypeCode), New SqlParameter("@City_Code", ctycode), New SqlParameter("@State", state), New SqlParameter("@Country", cuntry), New SqlParameter("@Phone1", phnenum1), New SqlParameter("@Phone2", phnenum2), New SqlParameter("@Fax", faxnum), New SqlParameter("@Email", Emaiiid), New SqlParameter("@WebSite", website), New SqlParameter("@Contact_Person_Name", Cntpersonname), New SqlParameter("@Contact_Person_Phone", cntpersonphnenum), New SqlParameter("@Contact_Person_Fax", cntpersonfax), New SqlParameter("@Contact_Person_Website", cntpersonwebsite), New SqlParameter("@Contact_Person_Email", cntpersonemail), New SqlParameter("@Terms_Code", trmscode), New SqlParameter("@Cust_Account", custACCOUNT), New SqlParameter("@Tax_Group", custtaxgroup), New SqlParameter("@TAX1", tax1), New SqlParameter("@TAX1_Rate", clsCommon.myCstr(tax1rate)), New SqlParameter("@TAX2", tax2), New SqlParameter("@TAX2_Rate", tax2rate), New SqlParameter("@TAX3", tax3), New SqlParameter("@TAX3_Rate", tax3rate), New SqlParameter("@TAX4", tax4), New SqlParameter("@TAX4_Rate", tax4rate), New SqlParameter("@TAX5", tax5), New SqlParameter("@TAX5_Rate", tax5rate), New SqlParameter("@TAX6", tax6), New SqlParameter("@TAX6_Rate", tax6rate), New SqlParameter("@TAX7", tax7), New SqlParameter("@TAX7_Rate", tax7rate), New SqlParameter("@TAX8", tax8), New SqlParameter("@TAX8_Rate", tax8rate), New SqlParameter("@TAX9", tax9), New SqlParameter("@TAX9_Rate", tax9rate), New SqlParameter("@TAX10", tax10), New SqlParameter("@TAX10_Rate", tax10rate), New SqlParameter("@Payment_Code", pymntcode), New SqlParameter("@Service_Tax_No", servicetaxnum), New SqlParameter("@Tin_No", tinno), New SqlParameter("@Lst_No", lstnum), New SqlParameter("@Form_Type", frmtype), New SqlParameter("@Status", status), New SqlParameter("@OnHold", "N"), New SqlParameter("@Remarks1", remarks1), New SqlParameter("@Remarks2", remarks2), New SqlParameter("@Additional1", Additional1), New SqlParameter("@Additional2", Additional2), New SqlParameter("@Additional3", Additional3), New SqlParameter("@Credit_Limit", CreditLimit), New SqlParameter("@Created_By", "ADMIN"), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", "CompanyCode"), New SqlParameter("@cst", Cst), New SqlParameter("@ecc", ECC), New SqlParameter("@range", Range), New SqlParameter("@collectorate", Collectorate), New SqlParameter("@pan", Pan), New SqlParameter("@division", Division), New SqlParameter("@Parent_Customer_No", ParentCustNo), New SqlParameter("@credit", strcredit), New SqlParameter("@InterBranch ", interbranch), New SqlParameter("@Distributor ", strdistrubutor), New SqlParameter("@CustDist ", strdistCust))
                            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_CUSTOMER_INFO_INSERT", New SqlParameter("@Cust_Code", strCusCode), New SqlParameter("@Customer_Name", strCusName), New SqlParameter("@Add1", add1), New SqlParameter("@Add2", add2), New SqlParameter("@Add3", add3), New SqlParameter("@Cust_Group_Code", custgroup), New SqlParameter("@Cust_Type", csttypeCode), New SqlParameter("@City_Code", ctycode), New SqlParameter("@State", state), New SqlParameter("@Country", cuntry), New SqlParameter("@Phone1", phnenum1), New SqlParameter("@Phone2", phnenum2), New SqlParameter("@Fax", faxnum), New SqlParameter("@Email", Emaiiid), New SqlParameter("@WebSite", website), New SqlParameter("@Contact_Person_Name", Cntpersonname), New SqlParameter("@Contact_Person_Phone", cntpersonphnenum), New SqlParameter("@Contact_Person_Fax", cntpersonfax), New SqlParameter("@Contact_Person_Website", cntpersonwebsite), New SqlParameter("@Contact_Person_Email", cntpersonemail), New SqlParameter("@Terms_Code", trmscode), New SqlParameter("@Cust_Account", custACCOUNT), New SqlParameter("@Tax_Group", custtaxgroup), New SqlParameter("@TAX1", tax1), New SqlParameter("@TAX1_Rate", clsCommon.myCstr(tax1rate)), New SqlParameter("@TAX2", tax2), New SqlParameter("@TAX2_Rate", tax2rate), New SqlParameter("@TAX3", tax3), New SqlParameter("@TAX3_Rate", tax3rate), New SqlParameter("@TAX4", tax4), New SqlParameter("@TAX4_Rate", tax4rate), New SqlParameter("@TAX5", tax5), New SqlParameter("@TAX5_Rate", tax5rate), New SqlParameter("@TAX6", tax6), New SqlParameter("@TAX6_Rate", tax6rate), New SqlParameter("@TAX7", tax7), New SqlParameter("@TAX7_Rate", tax7rate), New SqlParameter("@TAX8", tax8), New SqlParameter("@TAX8_Rate", tax8rate), New SqlParameter("@TAX9", tax9), New SqlParameter("@TAX9_Rate", tax9rate), New SqlParameter("@TAX10", tax10), New SqlParameter("@TAX10_Rate", tax10rate), New SqlParameter("@Payment_Code", pymntcode), New SqlParameter("@Service_Tax_No", servicetaxnum), New SqlParameter("@Tin_No", tinno), New SqlParameter("@Lst_No", lstnum), New SqlParameter("@Form_Type", frmtype), New SqlParameter("@Status", status), New SqlParameter("@OnHold", "N"), New SqlParameter("@Remarks1", remarks1), New SqlParameter("@Remarks2", remarks2), New SqlParameter("@Additional1", Additional1), New SqlParameter("@Additional2", Additional2), New SqlParameter("@Additional3", Additional3), New SqlParameter("@Credit_Limit", CreditLimit), New SqlParameter("@Created_By", "ADMIN"), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", "CompanyCode"), New SqlParameter("@cst", Cst), New SqlParameter("@ecc", ECC), New SqlParameter("@range", Range), New SqlParameter("@collectorate", Collectorate), New SqlParameter("@pan", Pan), New SqlParameter("@division", Division), New SqlParameter("@Parent_Customer_No", ParentCustNo), New SqlParameter("@credit", strcredit), New SqlParameter("@InterBranch ", interbranch), New SqlParameter("@Distributor ", strdistrubutor), New SqlParameter("@CustDist ", strdistCust))
                        Else
                            'connectSql.RunSp("sp_TSPL_CUSTOMER_INFO_UPDATE", New SqlParameter("@Cust_Code", strCusCode), New SqlParameter("@Customer_Name", strCusName), New SqlParameter("@Add1", add1), New SqlParameter("@Add2", add2), New SqlParameter("@Add3", add3), New SqlParameter("@Cust_Group_Code", custgroup), New SqlParameter("@Cust_Type", csttypeCode), New SqlParameter("@City_Code", ctycode), New SqlParameter("@State", state), New SqlParameter("@Country", cuntry), New SqlParameter("@Phone1", phnenum1), New SqlParameter("@Phone2", phnenum2), New SqlParameter("@Fax", faxnum), New SqlParameter("@Email", Emaiiid), New SqlParameter("@WebSite", website), New SqlParameter("@Contact_Person_Name", Cntpersonname), New SqlParameter("@Contact_Person_Phone", cntpersonphnenum), New SqlParameter("@Contact_Person_Fax", cntpersonfax), New SqlParameter("@Contact_Person_Website", cntpersonwebsite), New SqlParameter("@Contact_Person_Email", cntpersonemail), New SqlParameter("@Terms_Code", trmscode), New SqlParameter("@Cust_Account", custACCOUNT), New SqlParameter("@Tax_Group", custtaxgroup), New SqlParameter("@TAX1", tax1), New SqlParameter("@TAX1_Rate", clsCommon.myCstr(tax1rate)), New SqlParameter("@TAX2", tax2), New SqlParameter("@TAX2_Rate", tax2rate), New SqlParameter("@TAX3", tax3), New SqlParameter("@TAX3_Rate", tax3rate), New SqlParameter("@TAX4", tax4), New SqlParameter("@TAX4_Rate", tax4rate), New SqlParameter("@TAX5", tax5), New SqlParameter("@TAX5_Rate", tax5rate), New SqlParameter("@TAX6", tax6), New SqlParameter("@TAX6_Rate", tax6rate), New SqlParameter("@TAX7", tax7), New SqlParameter("@TAX7_Rate", tax7rate), New SqlParameter("@TAX8", tax8), New SqlParameter("@TAX8_Rate", tax8rate), New SqlParameter("@TAX9", tax9), New SqlParameter("@TAX9_Rate", tax9rate), New SqlParameter("@TAX10", tax10), New SqlParameter("@TAX10_Rate", tax10rate), New SqlParameter("@Payment_Code", pymntcode), New SqlParameter("@Service_Tax_No", servicetaxnum), New SqlParameter("@Tin_No", tinno), New SqlParameter("@Lst_No", lstnum), New SqlParameter("@Form_Type", frmtype), New SqlParameter("@Status", status), New SqlParameter("@OnHold", "N"), New SqlParameter("@Remarks1", remarks1), New SqlParameter("@Remarks2", remarks2), New SqlParameter("@Additional1", Additional1), New SqlParameter("@Additional2", Additional2), New SqlParameter("@Additional3", Additional3), New SqlParameter("@Credit_Limit", CreditLimit), New SqlParameter("@Modify_By", "ADMIN"), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@cst", Cst), New SqlParameter("@ecc", ECC), New SqlParameter("@range", Range), New SqlParameter("@collectorate", Collectorate), New SqlParameter("@pan", Pan), New SqlParameter("@division", Division), New SqlParameter("@Parent_Customer_No", ParentCustNo), New SqlParameter("@credit", strcredit), New SqlParameter("@InterBranch ", interbranch), New SqlParameter("@Distributor ", strdistrubutor), New SqlParameter("@CustDist ", strdistCust))
                            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_CUSTOMER_INFO_UPDATE", New SqlParameter("@Cust_Code", strCusCode), New SqlParameter("@Customer_Name", strCusName), New SqlParameter("@Add1", add1), New SqlParameter("@Add2", add2), New SqlParameter("@Add3", add3), New SqlParameter("@Cust_Group_Code", custgroup), New SqlParameter("@Cust_Type", csttypeCode), New SqlParameter("@City_Code", ctycode), New SqlParameter("@State", state), New SqlParameter("@Country", cuntry), New SqlParameter("@Phone1", phnenum1), New SqlParameter("@Phone2", phnenum2), New SqlParameter("@Fax", faxnum), New SqlParameter("@Email", Emaiiid), New SqlParameter("@WebSite", website), New SqlParameter("@Contact_Person_Name", Cntpersonname), New SqlParameter("@Contact_Person_Phone", cntpersonphnenum), New SqlParameter("@Contact_Person_Fax", cntpersonfax), New SqlParameter("@Contact_Person_Website", cntpersonwebsite), New SqlParameter("@Contact_Person_Email", cntpersonemail), New SqlParameter("@Terms_Code", trmscode), New SqlParameter("@Cust_Account", custACCOUNT), New SqlParameter("@Tax_Group", custtaxgroup), New SqlParameter("@TAX1", tax1), New SqlParameter("@TAX1_Rate", clsCommon.myCstr(tax1rate)), New SqlParameter("@TAX2", tax2), New SqlParameter("@TAX2_Rate", tax2rate), New SqlParameter("@TAX3", tax3), New SqlParameter("@TAX3_Rate", tax3rate), New SqlParameter("@TAX4", tax4), New SqlParameter("@TAX4_Rate", tax4rate), New SqlParameter("@TAX5", tax5), New SqlParameter("@TAX5_Rate", tax5rate), New SqlParameter("@TAX6", tax6), New SqlParameter("@TAX6_Rate", tax6rate), New SqlParameter("@TAX7", tax7), New SqlParameter("@TAX7_Rate", tax7rate), New SqlParameter("@TAX8", tax8), New SqlParameter("@TAX8_Rate", tax8rate), New SqlParameter("@TAX9", tax9), New SqlParameter("@TAX9_Rate", tax9rate), New SqlParameter("@TAX10", tax10), New SqlParameter("@TAX10_Rate", tax10rate), New SqlParameter("@Payment_Code", pymntcode), New SqlParameter("@Service_Tax_No", servicetaxnum), New SqlParameter("@Tin_No", tinno), New SqlParameter("@Lst_No", lstnum), New SqlParameter("@Form_Type", frmtype), New SqlParameter("@Status", status), New SqlParameter("@OnHold", "N"), New SqlParameter("@Remarks1", remarks1), New SqlParameter("@Remarks2", remarks2), New SqlParameter("@Additional1", Additional1), New SqlParameter("@Additional2", Additional2), New SqlParameter("@Additional3", Additional3), New SqlParameter("@Credit_Limit", CreditLimit), New SqlParameter("@Modify_By", "ADMIN"), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@cst", Cst), New SqlParameter("@ecc", ECC), New SqlParameter("@range", Range), New SqlParameter("@collectorate", Collectorate), New SqlParameter("@pan", Pan), New SqlParameter("@division", Division), New SqlParameter("@Parent_Customer_No", ParentCustNo), New SqlParameter("@credit", strcredit), New SqlParameter("@InterBranch ", interbranch), New SqlParameter("@Distributor ", strdistrubutor), New SqlParameter("@CustDist ", strdistCust))
                        End If
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

#End Region

#Region "Event"
    Private Sub btnNew_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.MouseHover
        ToolTip1.Show("New", btnNew)
    End Sub
    Private Sub fnTaxGrp()
        Try
            strCmd = " SELECT TSPL_TAX_GROUP_DETAILS.Tax_Code as [Tax] FROM TSPL_TAX_GROUP_MASTER INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_MASTER.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code" & _
                      " where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + fndTxGrp.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='S' order by TSPL_TAX_GROUP_DETAILS.Trans_Code"

            myDs = connectSql.RunSQLReturnDS(strCmd)
            If myDs.Tables(0).Rows.Count > 0 Then
                Dim Dr As DataRow
                i = 0
                grdTax.DataSource = Nothing
                grdTax.Rows.Clear()
                For Each Dr In myDs.Tables(0).Rows
                    Dim r As GridViewRowInfo = grdTax.Rows.AddNew()
                    r.Cells(0).Value = Dr(0).ToString()
                Next
            End If
            grdTax.AllowAddNewRow = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            strCmd = "select Cust_Code from TSPL_CUSTOMER_INFO where Cust_Code='" + fndCustomer.Value + "'"
            Dim CustCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strCmd))

            If clsCommon.CompairString(CustCode, fndCustomer.Value) = CompairStringResult.Equal Then
                Dim obj As New clsCustomerInfo()
                obj = clsCustomerInfo.GetData(strCode, NavTyep)
                Reset()
                If (obj IsNot Nothing) Then
                    fndCustomer.Value = obj.Cust_Code
                    txtCustomerName.Text = obj.Customer_Name
                    txtAdd1.Text = obj.Add1
                    txtAdd2.Text = obj.Add2
                    txtAdd3.Text = obj.Add3
                    fndCusgrp.Value = obj.Cust_Group_Code
                    txtCusgrp.Text = clsDBFuncationality.getSingleValue(" SELECT Cust_Group_Desc as [Description] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + fndCusgrp.Value + "'")
                    fndCity.Value = obj.City_Code
                    txtCity.Text = clsDBFuncationality.getSingleValue("SELECT [City_Name] FROM [TSPL_CITY_MASTER] where [City_Code]='" + fndCity.Value + "' ")
                    fndstate.Value = obj.State
                    txtStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  state_name  from TSPL_TDS_STATE_MASTER where state_code ='" + fndstate.Value + "'"))
                    txtCountry.Text = obj.Country
                    txtPhone1.Text = obj.Phone1
                    txtPhone2.Text = obj.Phone2
                    txtfax.Text = obj.Fax
                    txtEmail.Text = obj.Email
                    txtWeb.Text = obj.WebSite
                    txtContactName.Text = obj.Contact_Person_Name
                    txtContPhone.Text = obj.Contact_Person_Phone
                    txtContactFax.Text = obj.Contact_Person_Fax
                    txtContactEmail.Text = obj.Contact_Person_Email
                    txtContactWeb.Text = obj.Contact_Person_Website
                    fndTrmsCode.Value = obj.Terms_Code
                    fndAccntSet.Value = obj.Cust_Account
                    fndTxGrp.Value = obj.Tax_Group
                    txtTxGrp.Text = clsDBFuncationality.getSingleValue("SELECT [Tax_Group_Desc] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + fndTxGrp.Value + "'")
                    If (clsCommon.myLen(obj.TAX1) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX1
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX1_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX2) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX2
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX2_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX3) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX3
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX3_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX4) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX4
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX4_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX5) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX5
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX5_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX6) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX6
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX6_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX7) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX7
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX7_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX8) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX8
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX8_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX9) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX9
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX9_Rate
                    End If
                    If (clsCommon.myLen(obj.TAX10) > 0) Then
                        grdTax.Rows.AddNew()
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = obj.TAX10
                        grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = obj.TAX10_Rate
                    End If
                    fndPayCode.Value = obj.Payment_Code
                    txtStaxNo.Text = obj.Service_Tax_No
                    txtTinNo.Text = obj.Tin_No
                    txtLstNo.Text = obj.Lst_No
                    drpformtype.Text = obj.Form_Type
                    If obj.OnHold = "Y" Then
                        chkHold.Checked = True
                    Else
                        chkHold.Checked = False
                    End If
                    ddlCustType.SelectedValue = obj.Cust_Type
                    txtRemarks1.Text = obj.Remarks1
                    txtRemarks2.Text = obj.Remarks2
                    txtAddInfo1.Text = obj.Additional1
                    txtAddInfo2.Text = obj.Additional2
                    txtAddInfo3.Text = obj.Additional3
                    txtCredit.Text = clsCommon.myCdbl(obj.Credit_Limit)
                    txtcst.Text = obj.CST
                    txtecc.Text = obj.ECC
                    txtrange.Text = obj.Range
                    txtcollect.Text = obj.Collectorate
                    txtpan.Text = obj.PAN
                    txtdivision.Text = obj.Division
                    If obj.Credit_Customer = "Y" Then
                        chkcredit.Checked = True
                    Else
                        chkcredit.Checked = False
                    End If

                    If obj.Inter_Branch = "Y" Then
                        chkInterBranch.Checked = True
                    Else
                        chkInterBranch.Checked = False
                    End If

                    If obj.chkdis = "Y" Then
                        chkDistributer.Checked = True
                    Else
                        chkDistributer.Checked = False
                    End If

                    fndcust.Value = obj.custdis

                    '----------------------visi---------------------------

                    Dim qryLoadVisi As String = "select  CAST((case when Isnull(Customer_Id, '')='' then 0 else 1 end)as BIT) as [Select], Visi_Id as [Visi Id], VisiMake as [Visi Make], Visi_Chasis_No as [Chasis No], Visi_Installation_date as [Installation Date] from TSPL_VISI_MASTER Where Customer_Id='" + fndCustomer.Value + "' or Customer_Id=''"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryLoadVisi)
                    dgvVisi.DataSource = dt
                    FormatGrid()




                    '-----------------------------------------------------







                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                End If
            Else
                Reset()
                ddlCustType.Focus()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub grdTax_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles grdTax.EditorRequired

        Dim str As String = "select Tax_Rate as [Tax Rate] from TSPL_TAX_RATES where Tax_Code = '" + grdTax.CurrentRow.Cells(0).Value + "'"
        Dim gvMultiComboColum As GridViewComboBoxColumn = TryCast(grdTax.Columns(1), GridViewComboBoxColumn)

        myDs = connectSql.RunSQLReturnDS(str)
        gvMultiComboColum.DataSource = myDs.Tables(0)
        gvMultiComboColum.ValueMember = "Tax Rate"

    End Sub
    Private Sub keypress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPhone1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone1.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtPhone2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone2.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtFax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfax.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCredit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCredit.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48)) Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtBalOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48)) Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtComms_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48)) Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtContPhone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContPhone.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtContfax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContactFax.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave
        If txtEmail.Text = "" Then
        Else
            Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success Then
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter the proper format of e-mail address", Me.Text)
                txtEmail.Text = ""
                txtEmail.Focus()
            End If
        End If

    End Sub

    Private Sub txtContactEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContactEmail.Leave
        If txtContactEmail.Text = "" Then
            Return
        Else
            Dim check As Match = Regex.Match(txtContactEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success Then
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter the proper format of e-mail address", Me.Text)
                txtContactEmail.Text = ""
                txtContactEmail.Focus()
            End If
        End If
    End Sub
#End Region


    Private Sub frmCustomer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            Reset()
        End If
    End Sub



    Private Sub fndCusgrp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCusgrp._MYValidating

        Dim qry As String = " SELECT Cust_Group_Code as [CustomerGruopCode],Cust_Group_Desc as [Description]," & _
                            " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] "
        fndCusgrp.Value = clsCommon.ShowSelectForm("CUSGRP_CODE", qry, "CustomerGruopCode", "", fndCusgrp.Value, "", isButtonClicked)
        txtCusgrp.Text = clsDBFuncationality.getSingleValue(" SELECT Cust_Group_Desc as [Description] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + fndCusgrp.Value + "'")
        fnCusGrp()

    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCity._MYValidating

        Dim qry As String = "SELECT [City_Code] as [CityCode],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER]"
        fndCity.Value = clsCommon.ShowSelectForm("FNDCITY_CODE", qry, "CityCode", "", fndCity.Value, "", isButtonClicked)
        txtCity.Text = clsDBFuncationality.getSingleValue("SELECT [City_Name] FROM [TSPL_CITY_MASTER] where [City_Code]='" + fndCity.Value + "' ")

    End Sub

    Private Sub fndstate__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstate._MYValidating
        Dim qry As String = "select state_code as [StateCode],state_name as [State Name] from TSPL_TDS_STATE_MASTER"
        fndstate.Value = clsCommon.ShowSelectForm("FNDSTATE_CODE", qry, "StateCode", "", fndstate.Value, "", isButtonClicked)
        txtStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  state_name  from TSPL_TDS_STATE_MASTER where state_code ='" + fndstate.Value + "'"))
    End Sub

    Private Sub fndTrmsCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTrmsCode._MYValidating

        Dim qry As String = "SELECT [Terms_Code] as [TermsCode],[Terms_Desc] as [Description],[No_Days] as [No. of Days] FROM [TSPL_TERMS_MASTER]"
        fndTrmsCode.Value = clsCommon.ShowSelectForm("FNDTMCODE", qry, "TermsCode", "", fndTrmsCode.Value, "", isButtonClicked)

    End Sub

    Private Sub fndAccntSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAccntSet._MYValidating

        Dim qry As String = "SELECT  [Cust_Account] as [AccountSetCode],[Cust_Acct_Desc] as [Description] FROM [TSPL_CUSTOMER_ACCOUNT_SET]"
        fndAccntSet.Value = clsCommon.ShowSelectForm("CUSTACODE", qry, "AccountSetCode", "", fndAccntSet.Value, "", isButtonClicked)

    End Sub

    Private Sub fndPayCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPayCode._MYValidating

        Dim qry As String = "  SELECT [Payment_Code] as [PaymentCode],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE]"
        fndPayCode.Value = clsCommon.ShowSelectForm("FMPAYCODE", qry, "PaymentCode", "", fndPayCode.Value, "", isButtonClicked)

    End Sub

    Private Sub fndTxGrp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTxGrp._MYValidating

        Dim qry As String = "SELECT [Tax_Group_Code] AS [TaxGroupCode],[Tax_Group_Desc] as [Description]," & _
                       " (select case when [Tax_Group_Type]='S' then 'Sale' else 'Purchase' end) as [Type] FROM [TSPL_TAX_GROUP_MASTER] "
        fndTxGrp.Value = clsCommon.ShowSelectForm("TAXGROUPCODFND", qry, "TaxGroupCode", "Tax_Group_Type='S'", fndTxGrp.Value, "", isButtonClicked)
        txtTxGrp.Text = clsDBFuncationality.getSingleValue("SELECT [Tax_Group_Desc] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + fndTxGrp.Value + "'")
        fnTaxGrp()

    End Sub



    Private Sub fndCustomer__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        Dim str As String = "select count(*) from TSPL_CUSTOMER_INFO   where  Cust_Code ='" + fndCustomer.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndCustomer.MyReadOnly = False
        Else
            fndCustomer.MyReadOnly = True
        End If
        If fndCustomer.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Cust_Code as [CustomerCode],Customer_Name as [Name],Cust_Group_Code as [Customer Group] from TSPL_CUSTOMER_INFO  "
            fndCustomer.Value = clsCommon.ShowSelectForm("CUSTOMERINFOFINDER", qry, "CustomerCode", "", fndCustomer.Value, "", isButtonClicked)
            'txtCustomerName.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_INFO  where Cust_Code='" + fndCustomer.Value + "' ")
        End If
        LoadData(fndCustomer.Value, NavigatorType.Current)
    End Sub

    Private Sub fndCustomer__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCustomer._MYNavigator
        Try
            LoadData(fndCustomer.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        funExport()
    End Sub

    Private Sub RadMenuItem3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        funImport()
    End Sub

    Private Sub fndCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndCustomer.Leave
        ''Try
        ''    LoadData(fndCustomer.Value, Nothing)
        ''Catch ex As Exception
        ''    common.clsCommon.MyMessageBoxShow(ex.Message)
        ''End Try
    End Sub

    Private Sub fndcust__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcust._MYValidating
        Try

            Dim qry As String = "select Cust_Code as [CustomerCode],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_CUSTOMER_MASTER   "

            Dim whr As String = " Cust_Type_Code in('A','S')"
            fndcust.Value = clsCommon.ShowSelectForm("Customer", qry, "CustomerCode", whr, fndcust.Value, "", isButtonClicked)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkDistributer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDistributer.ToggleStateChanged
        If chkDistributer.Checked = True Then
            lblcust.Visible = True
            fndcust.Visible = True
            pageCus.Pages("RadPageViewPage3").Enabled = True  '  Enables The Visi Page 
        ElseIf chkDistributer.Checked = False Then
            lblcust.Visible = False
            fndcust.Visible = False
            pageCus.Pages("RadPageViewPage3").Enabled = False '  Disables The Visi Page
        End If
    End Sub

    Private Sub grdTax_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles grdTax.UserDeletingRow

    End Sub
End Class



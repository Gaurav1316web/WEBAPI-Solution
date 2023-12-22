Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient

Public Class frmPriceMasterPlan
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Const colTRCode As String = "colTRCode"
    Const colSNo As String = "colSNo"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colIAliasName As String = "colIAliasName"
    Const colItemTaxable As String = "colItemTaxable"
    Const colAgainstItemWiseTaxCode As String = "colAgainstItemWiseTaxCode"
    Const colUnit As String = "colUnit"
    Const colPriceCode As String = "colPriceCode"
    Const colPriceName As String = "colPriceName"
    Const colType As String = "colType"
    Const colMRP As String = "colMRP"
    Const colPriceComponentCode As String = "colPriceComponentCode"
    Const colPriceComponentDesc As String = "colPriceComponentDesc"
    Const colPriceComponentRate As String = "colPriceComponentRate"
    Const colPriceComponentCalculationMethod As String = "colPriceComponentCalculationMethod"
    Const colPriceComponentAmount As String = "colPriceComponentAmount"
    Const colBasicPrice As String = "colBasicPrice"
    Const colTaxGroupCode As String = "colTaxGroupCode"
    Const colTaxGroupName As String = "colTaxGroupName"
    Const colTax As String = "colTax"
    Const colTaxBaseAmt As String = "colTaxBaseAmt"
    Const colTaxRate As String = "colTaxRate"
    Const colTaxAmt As String = "colTaxAmt"
    Const colIsTaxable As String = "colIsTaxable"
    Const colIsSurTax As String = "colIsSurTax"
    Const colSurTaxCode As String = "colSurTaxCode"
    Const colIsTaxOnBaseAmount As String = "colIsTaxOnBaseAmount"
    Const colTotalTaxAmt As String = "colTotalTaxAmt"
    Const colSaleAmt As String = "colSaleAmt"
    Const colPriceID As String = "colPriceID"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim SettingCalculateTaxRatefromItemwsieTaxOnSale As Boolean = False ''BHA/21/05/19-000894 by balwinder on 4/06/2019
    Dim isLoadCopy As Boolean = False
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmPriceChartMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        gv1.Rows.AddNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        SettingCalculateTaxRatefromItemwsieTaxOnSale = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing)) = 1)
        gv1.Enabled = True
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkPricePlanning)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        txtCode.Value = ""
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = txtStartDate.Value.AddMonths(1)
        txtDate.Value = txtStartDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
        txtLocation.Value = ""
        lblLocation.Text = ""
        ''richa agarwal 20 May,2019 MIL/21/05/19-000088
        isInsideLoadData = True
        ''-----------
        'chkBackCalculation.Checked = False
        RadGroupBox2.Visible = chkBackCalculation.Checked
        chkAllUOM.Checked = True
        txtCode.MyReadOnly = False
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnsave.Enabled = True
        txtRemarks.Text = ""
        lblPricePlanCopyNo.Text = ""
        fndPricePlanCopy.Value = ""
        loadBlankGrid()
        isNewEntry = True
        isCellValueChangedOpen = False
        isInsideLoadData = False

    End Sub

    Sub loadBlankGrid()
        Try
            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTextBox.HeaderText = "TR Code"
            repoTextBox.Name = colTRCode
            repoTextBox.Width = 100
            repoTextBox.ReadOnly = True
            repoTextBox.IsVisible = False
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoNumBox.Name = colSNo
            repoNumBox.Width = 50
            repoNumBox.DecimalPlaces = 0
            repoNumBox.Minimum = 0
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            repoNumBox.ReadOnly = True
            repoNumBox.HeaderText = "SNO"
            gv1.MasterTemplate.Columns.Add(repoNumBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTextBox.HeaderText = "Item Code"
            repoTextBox.Name = colICode
            repoTextBox.Width = 100
            repoTextBox.ReadOnly = False
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderText = "Item Name"
            repoTextBox.Name = colIName
            repoTextBox.Width = 150
            repoTextBox.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderText = "Alias Name"
            repoTextBox.Name = colIAliasName
            repoTextBox.Width = 150
            repoTextBox.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoIsSurTax1.HeaderText = "Is Item Taxable"
            repoIsSurTax1.Name = colItemTaxable
            repoIsSurTax1.ReadOnly = True
            repoIsSurTax1.IsVisible = False
            repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoIsSurTax1)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.FormatString = ""
            repoTextBox.HeaderText = "Against Item Wise Tax Code"
            repoTextBox.Name = colAgainstItemWiseTaxCode
            repoTextBox.IsVisible = False
            repoTextBox.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTextBox.HeaderText = "UOM"
            repoTextBox.Name = colUnit
            repoTextBox.Width = 80
            repoTextBox.ReadOnly = False
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTextBox.HeaderText = "Price Code"
            repoTextBox.Name = colPriceCode
            repoTextBox.Width = 80
            repoTextBox.ReadOnly = False
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderText = "Price Name"
            repoTextBox.Name = colPriceName
            repoTextBox.Width = 150
            repoTextBox.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderText = "Type"
            repoTextBox.Name = colType
            repoTextBox.Width = 100
            repoTextBox.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoNumBox = New GridViewDecimalColumn()
            repoNumBox.Name = colMRP
            repoNumBox.Width = 100
            repoNumBox.DecimalPlaces = 2
            repoNumBox.Minimum = 0
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            repoNumBox.ReadOnly = False
            repoNumBox.HeaderText = "MRP"
            gv1.MasterTemplate.Columns.Add(repoNumBox)

            For ii As Integer = 1 To 10
                repoTextBox = New GridViewTextBoxColumn()
                repoTextBox.HeaderText = "Price Component " + clsCommon.myCstr(ii) + " Code"
                repoTextBox.Name = colPriceComponentCode + clsCommon.myCstr(ii)
                repoTextBox.Width = 150
                repoTextBox.ReadOnly = True
                repoTextBox.IsVisible = False
                repoTextBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoTextBox)

                repoTextBox = New GridViewTextBoxColumn()
                repoTextBox.HeaderText = "Price Component " + clsCommon.myCstr(ii)
                repoTextBox.Name = colPriceComponentDesc + clsCommon.myCstr(ii)
                repoTextBox.Width = 150
                repoTextBox.ReadOnly = True
                repoTextBox.IsVisible = (ii < 4)
                repoTextBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoTextBox)

                repoNumBox = New GridViewDecimalColumn()
                repoNumBox.Name = colPriceComponentRate + clsCommon.myCstr(ii)
                repoNumBox.Width = 100
                repoNumBox.DecimalPlaces = 2
                repoNumBox.Minimum = 0
                repoNumBox.Step = 0
                repoNumBox.ShowUpDownButtons = False
                repoNumBox.IsVisible = (ii < 4)
                repoNumBox.ReadOnly = False
                repoNumBox.HeaderText = "Price Component " + clsCommon.myCstr(ii) + " Rate "
                repoNumBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoNumBox)

                repoTextBox = New GridViewTextBoxColumn()
                repoTextBox.HeaderText = "Price Component " + clsCommon.myCstr(ii) + "Calculation Method "
                repoTextBox.Name = colPriceComponentCalculationMethod + clsCommon.myCstr(ii)
                repoTextBox.Width = 150
                repoTextBox.ReadOnly = True
                repoTextBox.IsVisible = False
                repoTextBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoTextBox)

                repoNumBox = New GridViewDecimalColumn()
                repoNumBox.Name = colPriceComponentAmount + clsCommon.myCstr(ii)
                repoNumBox.Width = 100
                repoNumBox.DecimalPlaces = 2
                repoNumBox.Minimum = 0
                repoNumBox.Step = 0
                repoNumBox.ShowUpDownButtons = False
                repoNumBox.IsVisible = (ii < 4)
                repoNumBox.ReadOnly = True
                repoNumBox.WrapText = True
                repoNumBox.HeaderText = "Price Component Amount " + clsCommon.myCstr(ii)
                gv1.MasterTemplate.Columns.Add(repoNumBox)
            Next

            repoNumBox = New GridViewDecimalColumn()
            repoNumBox.Name = colBasicPrice
            repoNumBox.Width = 100
            repoNumBox.DecimalPlaces = 2
            repoNumBox.Minimum = 0
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            repoNumBox.IsVisible = True
            repoNumBox.ReadOnly = True
            repoNumBox.HeaderText = "Basic Price"
            gv1.MasterTemplate.Columns.Add(repoNumBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTextBox.HeaderText = "Tax Group Code"
            repoTextBox.Name = colTaxGroupCode
            repoTextBox.Width = 100
            repoTextBox.ReadOnly = False
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderText = "Tax Group"
            repoTextBox.Name = colTaxGroupName
            repoTextBox.Width = 150
            repoTextBox.ReadOnly = True
            repoTextBox.WrapText = True
            gv1.MasterTemplate.Columns.Add(repoTextBox)

           

            For ii As Integer = 1 To 10

                repoNumBox = New GridViewDecimalColumn()
                repoNumBox.Name = colTaxBaseAmt + clsCommon.myCstr(ii)
                repoNumBox.Width = 100
                repoNumBox.DecimalPlaces = 2
                repoNumBox.Minimum = 0
                repoNumBox.Step = 0
                repoNumBox.ShowUpDownButtons = False
                repoNumBox.IsVisible = False
                repoNumBox.ReadOnly = True
                repoNumBox.HeaderText = "Tax " + clsCommon.myCstr(ii) + " Base Amount"
                repoNumBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoNumBox)

                repoTextBox = New GridViewTextBoxColumn()
                repoTextBox.HeaderText = "Tax " + clsCommon.myCstr(ii)
                repoTextBox.Name = colTax + clsCommon.myCstr(ii)
                repoTextBox.Width = 100
                repoTextBox.ReadOnly = True
                repoTextBox.IsVisible = (ii < 6)
                gv1.MasterTemplate.Columns.Add(repoTextBox)

                repoNumBox = New GridViewDecimalColumn()
                repoNumBox.Name = colTaxRate + clsCommon.myCstr(ii)
                repoNumBox.Width = 100
                repoNumBox.DecimalPlaces = 3
                repoNumBox.Minimum = 0
                repoNumBox.Step = 0
                repoNumBox.ShowUpDownButtons = False
                repoNumBox.IsVisible = (ii < 6)
                repoNumBox.ReadOnly = True
                repoNumBox.HeaderText = "Tax " + clsCommon.myCstr(ii) + " Rate"
                repoNumBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoNumBox)

                repoNumBox = New GridViewDecimalColumn()
                repoNumBox.Name = colTaxAmt + clsCommon.myCstr(ii)
                repoNumBox.Width = 100
                repoNumBox.DecimalPlaces = 5
                repoNumBox.Minimum = 0
                repoNumBox.Step = 0
                repoNumBox.ShowUpDownButtons = False
                repoNumBox.IsVisible = (ii < 6)
                repoNumBox.ReadOnly = True
                repoNumBox.HeaderText = "Tax " + clsCommon.myCstr(ii) + " Amt "
                repoNumBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoNumBox)

                Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
                repoCheckBox.HeaderText = "Is Taxable " + clsCommon.myCstr(ii)
                repoCheckBox.Name = colIsTaxable + clsCommon.myCstr(ii)
                repoCheckBox.ReadOnly = True
                repoCheckBox.IsVisible = False
                repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
                repoCheckBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoCheckBox)

                repoCheckBox = New GridViewCheckBoxColumn()
                repoCheckBox.HeaderText = "Is Surtax " + clsCommon.myCstr(ii)
                repoCheckBox.Name = colIsSurTax + clsCommon.myCstr(ii)
                repoCheckBox.ReadOnly = True
                repoCheckBox.IsVisible = False
                repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
                repoCheckBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoCheckBox)

                repoTextBox = New GridViewTextBoxColumn()
                repoTextBox.HeaderText = "Sur Tax Code " + clsCommon.myCstr(ii)
                repoTextBox.Name = colSurTaxCode + clsCommon.myCstr(ii)
                repoTextBox.Width = 100
                repoTextBox.ReadOnly = True
                repoTextBox.IsVisible = False
                repoTextBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoTextBox)

                repoCheckBox = New GridViewCheckBoxColumn()
                repoCheckBox.HeaderText = "Is Tax On Base Amount " + clsCommon.myCstr(ii)
                repoCheckBox.Name = colIsTaxOnBaseAmount + clsCommon.myCstr(ii)
                repoCheckBox.ReadOnly = True
                repoCheckBox.IsVisible = False
                repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
                repoCheckBox.WrapText = True
                gv1.MasterTemplate.Columns.Add(repoCheckBox)
            Next

            repoNumBox = New GridViewDecimalColumn()
            repoNumBox.Name = colTotalTaxAmt
            repoNumBox.Width = 100
            repoNumBox.DecimalPlaces = 2
            repoNumBox.Minimum = 0
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            repoNumBox.IsVisible = True
            repoNumBox.ReadOnly = True
            repoNumBox.HeaderText = "Total Tax Amt"
            gv1.MasterTemplate.Columns.Add(repoNumBox)

            repoNumBox = New GridViewDecimalColumn()
            repoNumBox.Name = colSaleAmt
            repoNumBox.Width = 100
            repoNumBox.DecimalPlaces = 2
            repoNumBox.Minimum = 0
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            repoNumBox.IsVisible = True
            repoNumBox.ReadOnly = True
            repoNumBox.HeaderText = "Sale Amt"
            gv1.MasterTemplate.Columns.Add(repoNumBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.FormatString = ""
            repoTextBox.HeaderText = "Price ID"
            repoTextBox.Name = colPriceID
            repoTextBox.Width = 150
            repoTextBox.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = True 'Update by preeti gupta Against ticket no[MIL/13/05/19-000081]
            gv1.AllowRowReorder = True
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
            gv1.AutoSizeRows = False
            gv1.AllowRowReorder = True

            'sanjay
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = False
            'sanjay

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If SettingCalculateTaxRatefromItemwsieTaxOnSale Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                    For jj As Integer = 1 To 10
                        Dim strJJ As String = clsCommon.myCstr(jj)
                        Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(clsCommon.myCstr(colTax + strJJ)).Value())
                        If clsCommon.myLen(strTaxCode) > 0 AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells(clsCommon.myCstr(colItemTaxable)).Value) AndAlso SettingCalculateTaxRatefromItemwsieTaxOnSale Then
                            Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colTaxGroupCode).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(clsCommon.myCstr("colTax" + strJJ)).Value), txtStartDate.Value, clsCommon.myCstr(gv1.Rows(ii).Cells(colType).Value))
                            If objTAXRate IsNot Nothing Then
                                If clsCommon.myCdbl(gv1.Rows(ii).Cells(clsCommon.myCstr(colTaxRate + strJJ)).Value) <> objTAXRate.TAX_Rate Then
                                    gv1.CurrentRow = gv1.Rows(ii)
                                    Throw New Exception("Error At line No" + clsCommon.myCstr(ii + 1) + " Tax " + strJJ + " Rate Should be " + clsCommon.myCstr(objTAXRate.TAX_Rate) + " because Tax Rate is fixed")
                                End If
                                gv1.Rows(ii).Cells(clsCommon.myCstr(colAgainstItemWiseTaxCode)).Value = objTAXRate.HCODE
                            End If
                        End If
                    Next

                End If
            Next
        End If
        UpdateAllRows()

        Return True
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.MilkPricePlanning, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsPricePlanHead()
                obj.Plan_Code = txtCode.Value
                obj.Plan_Date = txtDate.Value
                obj.Loc_Code = txtLocation.Value
                obj.Start_Date = txtStartDate.Value
                If txtEndDate.Checked Then
                    obj.End_Date = txtEndDate.Value
                End If
                obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                If clsCommon.myLen(clsCommon.myCstr(fndPricePlanCopy.Value)) > 0 Then
                    obj.PricePlanCopyNo = clsCommon.myCstr(fndPricePlanCopy.Value)
                Else
                    obj.PricePlanCopyNo = clsCommon.myCstr(lblPricePlanCopyNo.Text)
                End If


                obj.Is_ALL_UOM = chkAllUOM.Checked
                obj.Is_Back_Calculation = chkBackCalculation.Checked
                If chkBackCalculation.Checked Then
                    If rbtnBackCalculationWithTax.IsChecked Then
                        obj.Back_Calculation_Type = 1
                    Else
                        obj.Back_Calculation_Type = 2
                    End If
                End If
                obj.Arr = New List(Of clsPricePlanDetail)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                        Dim objtr As New clsPricePlanDetail
                        objtr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                        objtr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(gv1.Rows(ii).Cells(colAgainstItemWiseTaxCode).Value)
                        objtr.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                        objtr.Item_MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
                        objtr.Price_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceCode).Value)
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "1").Value)) > 0 Then
                            objtr.Price_Comp1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "1").Value)
                            objtr.Price_Rate1 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "1").Value)
                            objtr.Price_Amount1 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "1").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "2").Value)) > 0 Then
                            objtr.Price_Comp2 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "2").Value)
                            objtr.Price_Rate2 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "2").Value)
                            objtr.Price_Amount2 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "2").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "3").Value)) > 0 Then
                            objtr.Price_Comp3 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "3").Value)
                            objtr.Price_Rate3 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "3").Value)
                            objtr.Price_Amount3 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "3").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "4").Value)) > 0 Then
                            objtr.Price_Comp4 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "4").Value)
                            objtr.Price_Rate4 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "4").Value)
                            objtr.Price_Amount4 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "4").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "5").Value)) > 0 Then
                            objtr.Price_Comp5 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "5").Value)
                            objtr.Price_Rate5 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "5").Value)
                            objtr.Price_Amount5 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "5").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "6").Value)) > 0 Then
                            objtr.Price_Comp6 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "6").Value)
                            objtr.Price_Rate6 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "6").Value)
                            objtr.Price_Amount6 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "6").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "7").Value)) > 0 Then
                            objtr.Price_Comp7 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "7").Value)
                            objtr.Price_Rate7 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "7").Value)
                            objtr.Price_Amount7 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "7").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "8").Value)) > 0 Then
                            objtr.Price_Comp8 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "8").Value)
                            objtr.Price_Rate8 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "8").Value)
                            objtr.Price_Amount8 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "8").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "9").Value)) > 0 Then
                            objtr.Price_Comp9 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "9").Value)
                            objtr.Price_Rate9 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "9").Value)
                            objtr.Price_Amount9 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "9").Value)
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "10").Value)) > 0 Then
                            objtr.Price_Comp10 = clsCommon.myCstr(gv1.Rows(ii).Cells(colPriceComponentCode + "10").Value)
                            objtr.Price_Rate10 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentRate + "10").Value)
                            objtr.Price_Amount10 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPriceComponentAmount + "10").Value)
                        End If

                        objtr.Tax_group = clsCommon.myCstr(gv1.Rows(ii).Cells(colTaxGroupCode).Value)
                        objtr.Item_Basic_Price = clsCommon.myCdbl(gv1.Rows(ii).Cells(colBasicPrice).Value)


                        objtr.TAX1_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "1").Value)
                        objtr.TAX1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "1").Value)
                        objtr.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "1").Value)
                        objtr.TAX1_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "1").Value)

                        objtr.TAX2_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "2").Value)
                        objtr.TAX2 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "2").Value)
                        objtr.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "2").Value)
                        objtr.TAX2_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "2").Value)

                        objtr.TAX3_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "3").Value)
                        objtr.TAX3 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "3").Value)
                        objtr.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "3").Value)
                        objtr.TAX3_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "3").Value)

                        objtr.TAX4_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "4").Value)
                        objtr.TAX4 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "4").Value)
                        objtr.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "4").Value)
                        objtr.TAX4_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "4").Value)

                        objtr.TAX5_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "5").Value)
                        objtr.TAX5 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "5").Value)
                        objtr.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "5").Value)
                        objtr.TAX5_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "5").Value)

                        objtr.TAX6_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "6").Value)
                        objtr.TAX6 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "6").Value)
                        objtr.TAX6_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "6").Value)
                        objtr.TAX6_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "6").Value)

                        objtr.TAX7_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "7").Value)
                        objtr.TAX7 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "7").Value)
                        objtr.TAX7_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "7").Value)
                        objtr.TAX7_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "7").Value)

                        objtr.TAX8_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "8").Value)
                        objtr.TAX8 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "8").Value)
                        objtr.TAX8_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "8").Value)
                        objtr.TAX8_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "8").Value)

                        objtr.TAX9_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "9").Value)
                        objtr.TAX9 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "9").Value)
                        objtr.TAX9_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "9").Value)
                        objtr.TAX9_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "9").Value)

                        objtr.TAX10_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + "10").Value)
                        objtr.TAX10 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax + "10").Value)
                        objtr.TAX10_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate + "10").Value)
                        objtr.TAX10_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + "10").Value)

                        objtr.Total_Tax_Amt = clsCommon.myCstr(gv1.Rows(ii).Cells(colTotalTaxAmt).Value)
                        objtr.Item_Selling_Price = clsCommon.myCstr(gv1.Rows(ii).Cells(colSaleAmt).Value)

                        obj.Arr.Add(objtr)
                    End If
                Next
                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    isLoadCopy = False
                    LoadData(obj.Plan_Code, NavigatorType.Current)
                End If
            Else
                txtCode.MyReadOnly = False
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub FrmPriceChartMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete the current plan" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsPricePlanHead.DeleteData(txtCode.Value)
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            isInsideLoadData = True
            Reset()
            Dim obj As clsPricePlanHead = clsPricePlanHead.GetData(strCode, NavType, Nothing)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Plan_Code) > 0 Then
                isNewEntry = False
                If obj.Post_Status = ERPTransactionStatus.Approved AndAlso isLoadCopy = False Then
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    UsLock1.Status = obj.Post_Status
                Else
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                End If

                If isLoadCopy = False Then
                    txtCode.Value = obj.Plan_Code
                Else
                    fndPricePlanCopy.Value = obj.Plan_Code
                    isNewEntry = True
                End If
                txtDate.Value = obj.Plan_Date
                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
                txtStartDate.Value = obj.Start_Date
                If obj.End_Date IsNot Nothing Then
                    txtEndDate.Checked = True
                    txtEndDate.Value = obj.End_Date
                Else
                    txtEndDate.Checked = False
                End If
                txtRemarks.Text = clsCommon.myCstr(obj.remarks)
                lblPricePlanCopyNo.Text = clsCommon.myCstr(obj.PricePlanCopyNo)
                chkAllUOM.Checked = obj.Is_ALL_UOM
                chkBackCalculation.Checked = obj.Is_Back_Calculation
                If chkBackCalculation.Checked Then
                    If obj.Back_Calculation_Type = 1 Then
                        rbtnBackCalculationWithTax.IsChecked = True
                    Else
                        rbtnBackCalculationWithoutTax.IsChecked = True
                    End If
                End If
                LoadDetailData(obj.Arr, False, False)
                gv1.Rows.AddNew()
                txtCode.MyReadOnly = True
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub LoadDetailData(ByVal Arr As List(Of clsPricePlanDetail), ByVal isAddMasterCode As Boolean, ByVal isImport As Boolean)
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            ''RICHA AGARWAL 20 maY,2019 MIL/21/05/19-000088
            isInsideLoadData = True
            For Each objtr As clsPricePlanDetail In Arr
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTRCode).Value = objtr.Plan_TR_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Item_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsItemMaster.GetItemName(objtr.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIAliasName).Value = clsItemMaster.GetItemAliasName(objtr.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAgainstItemWiseTaxCode).Value = objtr.Against_Item_Wise_Tax_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objtr.Item_Code, Nothing)

                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.UOM
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objtr.Item_MRP
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = objtr.Price_Code

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "1").Value = objtr.Price_Comp1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "1").Value = objtr.Price_Rate1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "1").Value = objtr.Price_Amount1

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "2").Value = objtr.Price_Comp2
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "2").Value = objtr.Price_Rate2
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "2").Value = objtr.Price_Amount2

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "3").Value = objtr.Price_Comp3
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "3").Value = objtr.Price_Rate3
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "3").Value = objtr.Price_Amount3

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "4").Value = objtr.Price_Comp4
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "4").Value = objtr.Price_Rate4
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "4").Value = objtr.Price_Amount4

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "5").Value = objtr.Price_Comp5
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "5").Value = objtr.Price_Rate5
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "5").Value = objtr.Price_Amount5

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "6").Value = objtr.Price_Comp6
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "6").Value = objtr.Price_Rate6
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "6").Value = objtr.Price_Amount6

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "7").Value = objtr.Price_Comp7
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "7").Value = objtr.Price_Rate7
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "7").Value = objtr.Price_Amount7

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "8").Value = objtr.Price_Comp8
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "8").Value = objtr.Price_Rate8
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "8").Value = objtr.Price_Amount8

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "9").Value = objtr.Price_Comp9
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "9").Value = objtr.Price_Rate9
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "9").Value = objtr.Price_Amount9

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentCode + "10").Value = objtr.Price_Comp10
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentRate + "10").Value = objtr.Price_Rate10
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceComponentAmount + "10").Value = objtr.Price_Amount10

                AddPriceComonent(isAddMasterCode)

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxGroupCode).Value = objtr.Tax_group
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBasicPrice).Value = objtr.Item_Basic_Price

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "1").Value = objtr.TAX1_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "1").Value = objtr.TAX1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "1").Value = objtr.TAX1_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "1").Value = objtr.TAX1_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "2").Value = objtr.TAX2_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "2").Value = objtr.TAX2
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "2").Value = objtr.TAX2_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "2").Value = objtr.TAX2_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "3").Value = objtr.TAX3_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "3").Value = objtr.TAX3
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "3").Value = objtr.TAX3_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "3").Value = objtr.TAX3_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "4").Value = objtr.TAX4_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "4").Value = objtr.TAX4
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "4").Value = objtr.TAX4_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "4").Value = objtr.TAX4_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "5").Value = objtr.TAX5_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "5").Value = objtr.TAX5
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "5").Value = objtr.TAX5_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "5").Value = objtr.TAX5_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "6").Value = objtr.TAX6_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "6").Value = objtr.TAX6
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "6").Value = objtr.TAX6_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "6").Value = objtr.TAX6_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "7").Value = objtr.TAX7_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "7").Value = objtr.TAX7
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "7").Value = objtr.TAX7_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "7").Value = objtr.TAX7_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "8").Value = objtr.TAX8_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "8").Value = objtr.TAX8
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "8").Value = objtr.TAX8_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "8").Value = objtr.TAX8_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "9").Value = objtr.TAX9_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "9").Value = objtr.TAX9
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "9").Value = objtr.TAX9_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "9").Value = objtr.TAX9_Amt

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + "10").Value = objtr.TAX10_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + "10").Value = objtr.TAX10
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + "10").Value = objtr.TAX10_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + "10").Value = objtr.TAX10_Amt
                AddTaxGroupAuthority(isAddMasterCode, isImport)

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTaxAmt).Value = objtr.Total_Tax_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleAmt).Value = objtr.Item_Selling_Price

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceID).Value = objtr.ItemPriceID
            Next
            ''RICHA AGARWAL 20 maY,2019 MIL/21/05/19-000088
            isInsideLoadData = False
        End If
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select Plan_Code as PlanCode,convert(varchar,Plan_Date,103) as PlanDate,Loc_Code as LocationCode,TSPL_LOCATION_MASTER.Location_Desc as Location,Start_Date as StartDate,End_Date as EndDate,case when Post_Status=1 then 'Approved' else 'Pending' end as Status" + Environment.NewLine + _
            " ,isnull(TSPL_ITEM_PRICE_PLAN_HEADER.Remarks,'') as Remarks ,isnull(TSPL_ITEM_PRICE_PLAN_HEADER.PricePlanCopyNo,'') as [Price Plan Copy No] " & Environment.NewLine & _
        " from TSPL_ITEM_PRICE_PLAN_HEADER" + Environment.NewLine + _
        "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_ITEM_PRICE_PLAN_HEADER.Loc_Code"

        Dim whrcls As String = ""
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = " TSPL_ITEM_PRICE_PLAN_HEADER.Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        txtCode.Value = clsCommon.ShowSelectForm("PCPdLN", qry, "PlanCode", whrcls, txtCode.Value, "PlanCode", isButtonClicked)

        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            Reset()
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
        isLoadCopy = False
        gv1.Rows.AddNew()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to post")
            End If
            If clsCommon.MyMessageBoxShow("Post the current plan" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsPricePlanHead.PostData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                ''richa MIL/30/07/19-000114 comment loaddata
                'LoadData(txtCode.Value, NavigatorType.Current)
                btndelete.Enabled = False
                btnPost.Enabled = False
                btnsave.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow

        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvTS_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_Pasting(sender As Object, e As GridViewClipboardEventArgs) Handles gv1.Pasting
        e.Cancel = True
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colPriceCode) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colTaxGroupCode) OrElse e.Column Is gv1.Columns(colPriceComponentRate + "1") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "2") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "3") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "4") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "5") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "6") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "7") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "8") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "9") OrElse e.Column Is gv1.Columns(colPriceComponentRate + "10") Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gv1.Columns(colPriceCode) Then
                            OpenPriceCodeList(False)
                        ElseIf e.Column Is gv1.Columns(colTaxGroupCode) Then
                            OpenTaxGroupList(False)
                        End If
                        UpdateCurrentRow()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "Select Item_Code as Code, Item_Desc as Descriiption From TSPL_ITEM_MASTER"
            gv1.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("ItemFnder@PriceMstr", qry, "Code", " TSPL_item_master.Active=1", clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick)
            gv1.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
            gv1.CurrentRow.Cells(colIAliasName).Value = clsItemMaster.GetItemAliasName(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
            gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
            gv1.CurrentRow.Cells(colUnit).Value = ""
        Catch ex As Exception
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colIAliasName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colItemTaxable).Value = False
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "Select Unit_Code as Code, Unit_Desc as Description from TSPL_UNIT_MASTER LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("UOMFND@PriceMaster", qry, "Code", "TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colUnit).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenPriceCodeList(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "Select Distinct Price_Code as Code, Price_Code_Desc as [Description] ,isnull(Transfer,0) as Transfer from TSPL_PRICE_COMPONENT_MAPPING"
            gv1.CurrentRow.Cells(colPriceCode).Value = clsCommon.ShowSelectForm("PriceCode@PriceMaster", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colPriceCode).Value), "", isButtonClick)
            ''BlankPriceColumns()
            AddPriceComonent(True)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colPriceCode).Value = ""
            BlankPriceColumns()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub AddPriceComonent(ByVal isAddCurrentCode As Boolean)
        Dim qry As String = "SELECT [Price_code] as [Price Code] ,[Price_Code_Desc] as [Desc] ,[Remarks] as [Remarks] ,[Price_Comp_Code] as [Price Componant Code]  " & _
           ",[Price_Comp_Desc] as [Description], 0 as [Amount]" & _
           ",PRICE_CALCULATION_METHOD AS [Price Type],Transfer " & _
           " FROM [TSPL_PRICE_COMPONENT_MAPPING] where Price_code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colPriceCode).Value) + "' order by Price_Component_Map_Code Asc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.CurrentRow.Cells(colPriceName).Value = clsCommon.myCstr(dt.Rows(0)("Desc"))
            gv1.CurrentRow.Cells(colType).Value = IIf(clsCommon.myCdbl(dt.Rows(0)("Transfer")) = 1, "Transfer", "Sale")
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 10 Then
                    Throw New Exception("ERP Support only 10 Price componets")
                End If
                If isAddCurrentCode Then
                    gv1.CurrentRow.Cells(colPriceComponentCode + clsCommon.myCstr(ii + 1)).Value = clsCommon.myCstr(dt.Rows(ii)("Price Componant Code"))
                End If
                gv1.CurrentRow.Cells(colPriceComponentDesc + clsCommon.myCstr(ii + 1)).Value = clsCommon.myCstr(dt.Rows(ii)("Description"))
                gv1.CurrentRow.Cells(colPriceComponentCalculationMethod + clsCommon.myCstr(ii + 1)).Value = clsCommon.myCstr(dt.Rows(ii)("Price Type"))
            Next
        End If
    End Sub

    Sub BlankPriceColumns()
        gv1.CurrentRow.Cells(colPriceName).Value = ""
        gv1.CurrentRow.Cells(colType).Value = ""
        For ii As Integer = 1 To 10
            gv1.CurrentRow.Cells(colPriceComponentCode + clsCommon.myCstr(ii)).Value = ""
            gv1.CurrentRow.Cells(colPriceComponentDesc + clsCommon.myCstr(ii)).Value = ""
            gv1.CurrentRow.Cells(colPriceComponentRate + clsCommon.myCstr(ii)).Value = 0
            gv1.CurrentRow.Cells(colPriceComponentCalculationMethod + clsCommon.myCstr(ii)).Value = ""
            gv1.CurrentRow.Cells(colPriceComponentAmount + clsCommon.myCstr(ii)).Value = 0
        Next
    End Sub

    Sub OpenTaxGroupList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select Location")
            End If
            gv1.CurrentRow.Cells(colTaxGroupCode).Value = FinderForTaxGroup(txtLocation.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colType).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colTaxGroupCode).Value), isButtonClick, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value))
            BlankTaxGroup()
            AddTaxGroupAuthority(True, False)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTaxGroupCode).Value = ""
            BlankTaxGroup()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    ''MIL/01/05/19-000073 by balwinder on 02/05/2019
    Sub AddTaxGroupAuthority(ByVal isAddCurrentCode As Boolean, ByVal isImport As Boolean)
        Dim strTaxType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colType).Value)
        If clsCommon.CompairString(strTaxType, "Sale") = CompairStringResult.Equal Then
            strTaxType = "S"
        ElseIf clsCommon.CompairString(strTaxType, "Transfer") = CompairStringResult.Equal Then
            strTaxType = "T"
        Else
            Throw New Exception("No a valid tax Type")
        End If
        gv1.CurrentRow.Cells(colTaxGroupName).Value = clsTaxGroupMaster.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(colTaxGroupCode).Value), strTaxType, Nothing)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(clsCommon.myCstr(gv1.CurrentRow.Cells(colTaxGroupCode).Value), strTaxType, "", txtLocation.Value, True)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                Dim ii As Integer = 1
                For Each dr As DataRow In dt.Rows
                    Dim strII As String = clsCommon.myCstr(ii)
                    If isAddCurrentCode Then
                        gv1.CurrentRow.Cells(clsCommon.myCstr(colTax + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If clsCommon.myCBool(gv1.CurrentRow.Cells(clsCommon.myCstr(colItemTaxable)).Value) AndAlso SettingCalculateTaxRatefromItemwsieTaxOnSale Then
                            Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr(colICode)).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr(colTaxGroupCode)).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtStartDate.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colType).Value))
                            If objTAXRate IsNot Nothing Then
                                gv1.CurrentRow.Cells(clsCommon.myCstr(colAgainstItemWiseTaxCode)).Value = objTAXRate.HCODE
                                gv1.CurrentRow.Cells(clsCommon.myCstr(colTaxRate + strII)).Value = objTAXRate.TAX_Rate
                            End If
                        ElseIf Not SettingCalculateTaxRatefromItemwsieTaxOnSale AndAlso isImport Then
                        Else
                            gv1.CurrentRow.Cells(clsCommon.myCstr(colTaxRate + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                        End If
                    End If
                    gv1.CurrentRow.Cells(clsCommon.myCstr(colIsTaxable + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                    gv1.CurrentRow.Cells(clsCommon.myCstr(colIsSurTax + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                    gv1.CurrentRow.Cells(clsCommon.myCstr(colSurTaxCode + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                    gv1.CurrentRow.Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                    ii = ii + 1
                Next
            End If
        End If
    End Sub

    Public Shared Function FinderForTaxGroup(ByVal strTransLocation As String, ByVal strTaxType As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean, ByVal strItemCode As String) As String
        If clsCommon.myLen(strTransLocation) <= 0 Then
            Throw New Exception("Please first select Transaction location")
        End If
        If clsCommon.CompairString(strTaxType, "Sale") = CompairStringResult.Equal Then
            strTaxType = "S"
        ElseIf clsCommon.CompairString(strTaxType, "Transfer") = CompairStringResult.Equal Then
            strTaxType = "T"
        Else
            Throw New Exception("Not a valid tax Type")
        End If

        Dim whrCls As String = " and Tax_Type='" + strTaxType + "' "
        Dim whrCls_taxGrp As String = " and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" + strTaxType + "' "

        ''richa TEC/30/07/19-000967 if item is of Non taxable type then show only exempted type of taxes
        Dim whrExempted As String = String.Empty
        If clsCommon.myLen(strItemCode) > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTaxable,0) from TSPL_ITEM_MASTER where  Item_Code ='" & strItemCode & "'")), "0") = CompairStringResult.Equal Then
                whrExempted += " and isnull(TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,0)=1  "
            End If
        End If

        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from( select xxx.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc  from ("
        qry += " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where Location_Code = '" + strTransLocation + "' " + whrCls + " "
        qry += " group by Tax_Group_Code"
        qry += " )xxx"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xxx.Tax_Group_Code " + whrCls_taxGrp + " " + Environment.NewLine + _
        " where 2=(case when (select Description from TSPL_FIXED_PARAMETER where Type='Show only Active Taxes/Rates/Groups for GST' and Code='Show only Active Taxes/Rates/Groups for GST')=1 then case when TSPL_TAX_GROUP_MASTER.Active=1 then 2 else 1 end else 2 end) " + Environment.NewLine + _
        " " & whrExempted & " ) xxxx "
        Return clsCommon.ShowSelectForm("POtxGroupfndd", qry, "Code", "", strCurrCode, "Code", isButtonClicked)
    End Function

    Sub BlankTaxGroup()
        gv1.CurrentRow.Cells(colTaxGroupName).Value = ""
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            gv1.CurrentRow.Cells(clsCommon.myCstr(colTax + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr(colTaxBaseAmt + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr(colTaxRate + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr(colTaxAmt + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr(colIsTaxable + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr(colIsSurTax + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr(colSurTaxCode + strII)).Value = Nothing
        Next
    End Sub

    Private Sub UpdateCurrentRow()
        Try
            gv1.CurrentRow.Cells(colSNo).Value = (gv1.CurrentRow.Index + 1)
            Dim arrTaxableAuth As New List(Of String)
            Dim dblTotTaxAmt As Decimal = 0
            Dim dblMRP As Decimal = (clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value))
            gv1.CurrentRow.Cells(colMRP).Value = dblMRP
            Dim dblTotalPC As Decimal = 0
            For ii As Integer = 1 To 10
                Dim dblcurrComponetnAmt As Decimal = 0
                If clsCommon.myLen(gv1.CurrentRow.Cells(colPriceComponentCode + clsCommon.myCstr(ii)).Value) > 0 Then
                    Dim dblCurrComponetnRate As Decimal = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPriceComponentRate + clsCommon.myCstr(ii)).Value)
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colPriceComponentCalculationMethod + clsCommon.myCstr(ii)).Value), "Amount") = CompairStringResult.Equal Then
                        dblcurrComponetnAmt = dblCurrComponetnRate
                    Else
                        dblcurrComponetnAmt = Math.Round(dblMRP * dblCurrComponetnRate / 100, 2, MidpointRounding.ToEven)
                    End If
                End If
                dblcurrComponetnAmt = Math.Round(dblcurrComponetnAmt, 2, MidpointRounding.ToEven)
                gv1.CurrentRow.Cells(colPriceComponentAmount + clsCommon.myCstr(ii)).Value = dblcurrComponetnAmt
                dblTotalPC += dblcurrComponetnAmt
            Next

            Dim dblTaxBaseAmt As Double = dblMRP - dblTotalPC
            gv1.CurrentRow.Cells(colBasicPrice).Value = dblTaxBaseAmt

            Dim dblTotalNonTabxableRate As Double = 0
            Dim dblTotalNonTabxableAmount As Double = 0
            For ii As Integer = 1 To 10
                Dim strTaxCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colTax + clsCommon.myCstr(ii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    If Not clsCommon.myCBool(gv1.CurrentRow.Cells(colIsTaxable + clsCommon.myCstr(ii)).Value) Then
                        dblTotalNonTabxableRate = dblTotalNonTabxableRate + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxRate + clsCommon.myCstr(ii)).Value)
                    End If
                End If
            Next
            ''richa TEC/30/07/19-000967
            If rbtnBackCalculationWithTax.IsChecked = True Then
                dblTotalNonTabxableAmount = (dblTaxBaseAmt * 100) / (100 + dblTotalNonTabxableRate)
            Else
                dblTotalNonTabxableAmount = dblTaxBaseAmt
            End If

            For ii As Integer = 1 To 10
                Dim strTaxCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colTax + clsCommon.myCstr(ii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxRate + clsCommon.myCstr(ii)).Value)
                    Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSurTax + clsCommon.myCstr(ii)).Value)
                    Dim strSurTaxCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colSurTaxCode + clsCommon.myCstr(ii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colIsTaxable + clsCommon.myCstr(ii)).Value)
                    Dim IsTaxonBaseAmount As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colIsTaxOnBaseAmount + clsCommon.myCstr(ii)).Value)
                    Dim dblBaseAmt As Double = 0
                    Dim dblTaxAmt As Double = 0
                    If IsSurTax Then
                        Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(ii, strSurTaxCode)
                        dblBaseAmt = dblSurTaxAmt
                    Else
                        If chkBackCalculation.CheckState Then
                            ''richa 11 sep 2020
                            Dim dblOtherTaxAmt As Double = 0
                            If Not IsTaxonBaseAmount Then
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(clsCommon.myCstr(ii), arrTaxableAuth)
                            End If

                            dblBaseAmt = dblTotalNonTabxableAmount + dblOtherTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(clsCommon.myCstr(ii), arrTaxableAuth)
                            dblBaseAmt = (dblTaxBaseAmt + dblOtherTaxAmt)
                        End If
                    End If
                    dblBaseAmt = Math.Round(dblBaseAmt, 2, MidpointRounding.ToEven)
                    gv1.CurrentRow.Cells(colTaxBaseAmt + clsCommon.myCstr(ii)).Value = Math.Round(dblBaseAmt, 2)
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                    dblTaxAmt = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 5))
                    dblTotTaxAmt += dblTaxAmt
                    gv1.CurrentRow.Cells(colTaxAmt + clsCommon.myCstr(ii)).Value = dblTaxAmt
                    If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                        arrTaxableAuth.Add(strTaxCode.ToUpper())
                    End If
                Else
                    gv1.CurrentRow.Cells(colTax + clsCommon.myCstr(ii)).Value = Nothing
                    gv1.CurrentRow.Cells(colTaxBaseAmt + clsCommon.myCstr(ii)).Value = Nothing
                    gv1.CurrentRow.Cells(colTaxRate + clsCommon.myCstr(ii)).Value = Nothing
                    gv1.CurrentRow.Cells(colTaxAmt + clsCommon.myCstr(ii)).Value = Nothing
                    gv1.CurrentRow.Cells(colIsSurTax + clsCommon.myCstr(ii)).Value = Nothing
                    gv1.CurrentRow.Cells(colSurTaxCode + clsCommon.myCstr(ii)).Value = Nothing
                    gv1.CurrentRow.Cells(colIsTaxable + clsCommon.myCstr(ii)).Value = Nothing
                End If
            Next

            gv1.CurrentRow.Cells(colTotalTaxAmt).Value = Math.Round(dblTotTaxAmt, 5)
            Dim dblSalePrice As Decimal = 0
            If chkBackCalculation.CheckState Then
                If rbtnBackCalculationWithTax.IsChecked Then
                    dblSalePrice = dblTaxBaseAmt - dblTotTaxAmt
                Else
                    dblSalePrice = dblTaxBaseAmt
                End If
            Else
                dblSalePrice = dblTaxBaseAmt + dblTotTaxAmt
            End If
            dblSalePrice = Math.Round(dblSalePrice, 2, MidpointRounding.ToEven)
            gv1.CurrentRow.Cells(colSaleAmt).Value = dblSalePrice
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function GetCurrentRowSurTaxAmt(ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.CurrentRow.Cells(colTax + clsCommon.myCstr(ii)).Value)) = CompairStringResult.Equal Then
                Return clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt + clsCommon.myCstr(ii)).Value)
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowOtherTaxAmt(ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.CurrentRow.Cells(colTax + clsCommon.myCstr(ii)).Value)) = CompairStringResult.Equal Then
                    dblRet = dblRet + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt + clsCommon.myCstr(ii)).Value)
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical' and Location_Category not in('MCC')  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("BILLTOLO1PO", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub

    Private Sub chkBackCalculation_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBackCalculation.ToggleStateChanged
        ''richa agarwal add code with isInsideLoadData 20 may,2019 MIL/21/05/19-000088
        Try
            If (Not isInsideLoadData) Then
                RadGroupBox2.Visible = chkBackCalculation.Checked
                UpdateAllRows()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub UpdateAllRows()
        Try
            If gv1.RowCount > 0 AndAlso gv1.ColumnCount > 0 Then
                Dim oldCurrentRow As Integer = IIf(gv1.CurrentRow.Index < 0, 0, gv1.CurrentRow.Index)
                Dim oldCurrentColumne As Integer = IIf(gv1.CurrentColumn.Index < 0, 0, gv1.CurrentColumn.Index)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        UpdateCurrentRow()
                    End If
                Next
                gv1.CurrentRow = gv1.Rows(oldCurrentRow)
                gv1.CurrentColumn = gv1.Columns(oldCurrentColumne)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnBackCalculationWithTax_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnBackCalculationWithTax.ToggleStateChanged, rbtnBackCalculationWithoutTax.ToggleStateChanged
        ''richa agarwal add code with isInsideLoadData 20 may,2019 MIL/21/05/19-000088
        Try
            If (Not isInsideLoadData) Then
                UpdateAllRows()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            Dim query As String = "Select '' as ItemCode,'' as UOM,'' as PriceCode,0 as MRP,0 as PriceComponentRate1,0 as PriceComponentRate2,0 as PriceComponentRate3,'' as TaxGroup,0 as Tax1Rate,0 as Tax2Rate,0 as Tax3Rate,0 as Tax4Rate,0 as Tax5Rate"
            ListImpExpColumnsMandatory = New List(Of String)({"ItemCode", "UOM", "PriceCode", "MRP", "TaxGroup"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"ItemCode"})
            transportSql.ExporttoExcel(query, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Segment Code")
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select location")
            End If
            If Not isNewEntry Then
                Throw New Exception("Please first select New Button")
            End If
            loadBlankGrid()
            If transportSql.importExcel(dgv, "ItemCode", "UOM", "PriceCode", "MRP", "PriceComponentRate1", "PriceComponentRate2", "PriceComponentRate3", "TaxGroup", "Tax1Rate", "Tax2Rate", "Tax3Rate", "Tax4Rate", "Tax5Rate") Then
                Dim LineNo As Integer = 0
                Try
                    Dim arr As New List(Of clsPricePlanDetail)
                    clsCommon.ProgressBarPercentShow()
                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Validating  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        LineNo += 1
                        Dim obj As New clsPricePlanDetail
                        obj.SNo = LineNo
                        obj.Item_Code = clsCommon.myCstr(dgrv.Cells("ItemCode").Value)
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Continue For
                        End If
                        Dim qry As String = "select Item_Code from tspl_item_master where Item_Code='" + obj.Item_Code + "'"
                        obj.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Throw New Exception("No a valid item code")
                        End If

                        obj.UOM = clsCommon.myCstr(dgrv.Cells("UOM").Value)
                        If clsCommon.myLen(obj.UOM) <= 0 Then
                            Throw New Exception("Please enter UOM")
                        End If
                        qry = "Select TSPL_UNIT_MASTER.Unit_Code  from TSPL_UNIT_MASTER LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code where Item_Code='" + obj.Item_Code + "' and TSPL_UNIT_MASTER.Unit_Code='" + obj.UOM + "'"
                        obj.UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.myLen(obj.UOM) <= 0 Then
                            Throw New Exception("Not a valid UOM")
                        End If


                        obj.Price_Code = clsCommon.myCstr(dgrv.Cells("PriceCode").Value)
                        If clsCommon.myLen(obj.Price_Code) <= 0 Then
                            Throw New Exception("Please enter Price Code")
                        End If
                        qry = "Select Price_Code from TSPL_PRICE_COMPONENT_MAPPING where Price_Code='" + clsCommon.myCstr(dgrv.Cells("PriceCode").Value) + "'"
                        obj.Price_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.myLen(obj.Price_Code) <= 0 Then
                            Throw New Exception("Not a valid Price Code")
                        End If
                        obj.PriceType = "S"
                        qry = "Select Transfer from TSPL_PRICE_COMPONENT_MAPPING where Price_Code='" + clsCommon.myCstr(dgrv.Cells("PriceCode").Value) + "'"
                        Dim intTemp As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                        If intTemp = 1 Then
                            obj.PriceType = "T"
                        End If
                        obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("MRP").Value)
                        If obj.Item_MRP = 0 Then
                            Throw New Exception("Not a valid MRP")
                        End If
                        obj.Price_Rate1 = clsCommon.myCdbl(dgrv.Cells("PriceComponentRate1").Value)
                        obj.Price_Rate2 = clsCommon.myCdbl(dgrv.Cells("PriceComponentRate2").Value)
                        obj.Price_Rate3 = clsCommon.myCdbl(dgrv.Cells("PriceComponentRate3").Value)

                        obj.Tax_group = clsCommon.myCstr(dgrv.Cells("TaxGroup").Value)
                        If clsCommon.myLen(obj.Tax_group) <= 0 Then
                            Throw New Exception("Please enter Tax Group")
                        End If

                        qry = "select Tax_Group_Code as Code  from( select xxx.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc  from (" + Environment.NewLine + _
                        " Select Tax_Group_Code from TSPL_LOCATION_WISE_TAX_MASTER  where Location_Code = '" + txtLocation.Value + "'  and  Tax_Type='" + obj.PriceType + "' group by Tax_Group_Code )xxx " + Environment.NewLine + _
                        " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xxx.Tax_Group_Code  and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" + obj.PriceType + "'" + Environment.NewLine + _
                        " where 2=(case when (select Description from TSPL_FIXED_PARAMETER where Type='Show only Active Taxes/Rates/Groups for GST' and Code='Show only Active Taxes/Rates/Groups for GST')=1 then case when TSPL_TAX_GROUP_MASTER.Active=1 then 2 else 1 end else 2 end) "

                        ''richa TEC/30/07/19-000967 if item is of Non taxable type then show only exempted type of taxes
                        If clsCommon.myLen(obj.Item_Code) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTaxable,0) from TSPL_ITEM_MASTER where  Item_Code ='" & obj.Item_Code & "'")), "0") = CompairStringResult.Equal Then
                                qry += " and isnull(TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted,0)=1  "
                            End If
                        End If
                        qry += ") xxxx  where Tax_Group_Code='" + obj.Tax_group + "'"


                        obj.Tax_group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.myLen(obj.Tax_group) <= 0 Then
                            Throw New Exception("Not a valid Tax group")
                        End If
                        obj.TAX1_Rate = clsCommon.myCdbl(dgrv.Cells("Tax1Rate").Value)
                        GetValidTaxRate(obj.Tax_group, obj.PriceType, "1", obj.TAX1_Rate)
                        obj.TAX2_Rate = clsCommon.myCdbl(dgrv.Cells("Tax2Rate").Value)
                        GetValidTaxRate(obj.Tax_group, obj.PriceType, "2", obj.TAX2_Rate)
                        obj.TAX3_Rate = clsCommon.myCdbl(dgrv.Cells("Tax3Rate").Value)
                        GetValidTaxRate(obj.Tax_group, obj.PriceType, "3", obj.TAX3_Rate)
                        obj.TAX4_Rate = clsCommon.myCdbl(dgrv.Cells("Tax4Rate").Value)
                        GetValidTaxRate(obj.Tax_group, obj.PriceType, "4", obj.TAX4_Rate)
                        obj.TAX5_Rate = clsCommon.myCdbl(dgrv.Cells("Tax5Rate").Value)
                        GetValidTaxRate(obj.Tax_group, obj.PriceType, "5", obj.TAX5_Rate)
                        arr.Add(obj)
                    Next
                    Try
                        isInsideLoadData = True
                        LoadDetailData(arr, True, True)
                        UpdateAllRows()
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    Finally
                        isInsideLoadData = True
                    End Try

                    clsCommon.ProgressBarPercentHide()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no" + clsCommon.myCstr(LineNo) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Function GetValidTaxRate(ByVal strTaxGroup As String, ByVal strTaxType As String, ByVal strLineNo As String, ByVal TaxRate As Decimal) As Boolean
        If clsCommon.myLen(strTaxGroup) > 0 AndAlso TaxRate > 0 Then
            Dim qry As String = "select 1 from TSPL_TAX_GROUP_DETAILS " + Environment.NewLine + _
             " left outer join TSPL_TAX_RATES on TSPL_TAX_RATES.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type=TSPL_TAX_GROUP_DETAILS.Tax_Group_Type" + Environment.NewLine + _
             " where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + strTaxGroup + "' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='" + strTaxType + "' and TSPL_TAX_GROUP_DETAILS.Trans_Code='" + strLineNo + "'  and TSPL_TAX_RATES.Tax_Rate='" + clsCommon.myCstr(TaxRate) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Invalid Tax " + strLineNo + " Rate")
            End If
        End If
        Return True
    End Function


    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotalTaxAmt) Then
                If Not SettingCalculateTaxRatefromItemwsieTaxOnSale OrElse Not clsCommon.myCBool(gv1.CurrentRow.Cells(colItemTaxable).Value) Then
                    Dim frm As New FrmPOItemTaxDetails()
                    frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colSNo).Value)
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotalTaxAmt).Value)
                    frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colBasicPrice).Value)
                    ''New Column for location wise
                    frm.strTaxGroup = clsCommon.myCstr(gv1.CurrentRow.Cells(colTaxGroupCode).Value)
                    frm.strTransLocation = txtLocation.Value
                    frm.strTaxType = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colType).Value), "Transfer") = CompairStringResult.Equal, "T", "S")
                    frm.strVendorCustomerCode = ""
                    ''End of New Column for location wise
                    If clsCommon.myLen(frm.strItemCode) > 0 Then
                        frm.ArrIn = New List(Of clsTempItemTaxDetails)
                        For ii As Integer = 1 To 10
                            Dim strii As String = clsCommon.myCstr(ii)
                            Dim obj As New clsTempItemTaxDetails()
                            obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colTax + strii).Value)
                            If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                                obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                                obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxRate + strii).Value)
                                obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt + strii).Value)
                                obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt + strii).Value)
                                obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSurTax + strii).Value)
                                obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells(colSurTaxCode + strii).Value)
                                obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells(colIsTaxable + strii).Value)
                                frm.ArrIn.Add(obj)
                            End If
                        Next
                        frm.ShowDialog()
                        If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                            BlankTaxGroup()
                            For ii As Integer = 0 To frm.ArrOut.Count - 1
                                Dim strii As String = clsCommon.myCstr(ii + 1)
                                gv1.CurrentRow.Cells(colTax + strii).Value = frm.ArrOut(ii).AuthorityCode
                                gv1.CurrentRow.Cells(colTaxRate + strii).Value = frm.ArrOut(ii).Rate
                                gv1.CurrentRow.Cells(colTaxBaseAmt + strii).Value = frm.ArrOut(ii).BaseAmt
                                gv1.CurrentRow.Cells(colTaxAmt + strii).Value = frm.ArrOut(ii).TaxAmt
                                gv1.CurrentRow.Cells(colIsSurTax + strii).Value = frm.ArrOut(ii).isSurTax
                                gv1.CurrentRow.Cells(colSurTaxCode + strii).Value = frm.ArrOut(ii).SurTax
                                gv1.CurrentRow.Cells(colIsTaxable + strii).Value = frm.ArrOut(ii).IsTaxable
                            Next
                            gv1.CurrentRow.Cells(colTotalTaxAmt).Value = frm.dblTotTax
                            UpdateCurrentRow()
                        End If
                    End If
                End If
            ElseIf gv1.CurrentColumn Is gv1.Columns(colSNo) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colTRCode).Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPriceID).Value) > 0 Then
                Dim frm As New FrmFreeGrid
                Dim qry As String = "select  Item_Price_ID as PriceID,UOM,Item_MRP as MRP,convert(decimal(18,2), (Price_Amount1+Price_Amount2+Price_Amount3+Price_Amount4+Price_Amount5+Price_Amount6+Price_Amount7+Price_Amount8+Price_Amount9+Price_Amount10)) as ComponentAmount,convert(decimal(18,2), Item_Basic_Price) as BasicPrice,convert(decimal(18,2),(TAX1_Amt+TAX2_Amt+TAX3_Amt+TAX4_Amt+TAX5_Amt+TAX6_Amt+TAX7_Amt+TAX8_Amt+TAX9_Amt+TAX10_Amt)) as TotalTaxAmount,  convert(decimal(18,2), Item_Selling_Price) as SelePrice from TSPL_ITEM_PRICE_MASTER where Against_Plan_TR_Code in('" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTRCode).Value) + "') order by PriceID"
                frm.dt = clsDBFuncationality.GetDataTable(qry)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                    Exit Sub
                End If
                frm.strFormName = "All UOM Sale Price For : " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                'frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "ALLUOMPRice"
                frm.Show()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Price Plan Code : " + txtCode.Value)
            arrHeader.Add("Location : " & lblLocation.Text + "[" + txtLocation.Value + "]")
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    '==========Added by preti gupta Against ticket no[MIL/14/05/19-000083]
    Private Sub fndPricePlanCopy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPricePlanCopy._MYValidating
        isLoadCopy = True
        Dim qry As String = "select TSPL_ITEM_PRICE_PLAN_HEADER.plan_code as Code,convert(varchar,TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Date,103) as Plan_Date,TSPL_ITEM_PRICE_PLAN_HEADER.Loc_Code ,convert(varchar,TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date,103) as Start_Date,convert(varchar,TSPL_ITEM_PRICE_PLAN_HEADER.End_Date,103)  as End_Date,TSPL_ITEM_PRICE_PLAN_HEADER.Is_ALL_UOM from TSPL_ITEM_PRICE_PLAN_HEADER  "
        fndPricePlanCopy.Value = clsCommon.ShowSelectForm("PricePlanCopy", qry, "Code", "", fndPricePlanCopy.Value, "Code", isButtonClicked)
        If clsCommon.myLen(fndPricePlanCopy.Value) > 0 Then
            LoadData(fndPricePlanCopy.Value, NavigatorType.Current)
        Else
            Reset()
            isLoadCopy = False
        End If

    End Sub

    Private Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles BtnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to show history")
                txtCode.Focus()
            End If
            If clsCommon.myLen(txtCode.Value) > 0 Then
                clsERPFuncationalityold.ShowTransHistoryData(clsCommon.myCstr(txtCode.Value), "plan_code", "TSPL_ITEM_PRICE_PLAN_HEADER", "TSPL_ITEM_PRICE_PLAN_DETAIL")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
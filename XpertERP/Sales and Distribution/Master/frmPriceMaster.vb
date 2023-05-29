'-Created By-[Pankaj Kumar Chaudhary]--Against Ticket No--[]
'========BM00000003027,Updated By Rohit july 1,2014 on 6:10 PM.(Remark :Add Export to Excel options.)====
''updation by Richa Agarwal Against Ticket No. BM00000003706 (add field active)
Imports System.Globalization
Imports System.Threading
Imports System.Data.SqlClient
Imports System.Data
Imports common
Imports XpertERPSalesAndDistribution


Public Class FrmPriceMaster
    Inherits FrmMainTranScreen
    Dim IsFormLoad As Boolean = True
    Dim IsInsideLoadData As Boolean = False
    Dim userCode, companyCode As String
    Dim itemrate2, stockrate2 As Decimal
    Dim i As Integer = 0
    Dim value As Decimal
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim IsPackSizeLoad As Boolean = False
    Dim Is_Mrp As Boolean = False

    Dim PageMode As String
    Dim change As Boolean = True
    Dim canEdit As String = "Y"
    Dim ITemPRiceID As Integer = 0
    Dim EndDate As Date
    Dim flag = True
    Dim sql As String
    Dim ds As DataSet
    Dim dt As New DataTable()
    Dim PRICEIDNO As Decimal = 0
    Dim TotalPriceCompAmt As Double = 0
    Dim TotalTaxAmt As Decimal = 0
    Dim TotalTaxRate As Decimal = 0.0
    Dim IsNewEntry As Boolean = False
    Dim PriceList As Boolean
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Boolean = False
    Public isCheckCustomerType As Boolean = False
    Public arrCommission As List(Of clsPOSCommissionMapping)
    Dim Against_ItemwiseTaxCode As String = Nothing
    Sub LoadType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "S"
        dr("Name") = "Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "T"
        dr("Name") = "Transfer"
        dt.Rows.Add(dr)

        ddlType.DataSource = dt
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Name"
    End Sub
    Private Sub FrmPriceMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            IsFormLoad = True
            SetUserMgmtNew()
            SetLength()
            RadPageView1.SelectedPage = pvItemPrice
            PageMode = "New"
            ResetScreen()
            ButtonToolTip.SetToolTip(rbtnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P Print the Report")
            ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for Delete")
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save")
            ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
            IsFormLoad = False
            txtBasic.Enabled = False
            txtPurchaseCost.Enabled = False
            lblPriceCompTotal.Visible = True
            LoadType()
            CalculateTaxRatefromItemwsieTaxOnSale = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing)) = 1, True, False)
            PriceList = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPriceListMasterPostedData, clsFixedParameterCode.AllowPriceListMasterPostedData, Nothing)) = 1, True, False)
            isCheckCustomerType = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLoginType, clsFixedParameterCode.AllowLoginType, Nothing)) = 1, True, False)
            btnMapCommission.Visible = isCheckCustomerType
            btnPost.Visible = MyBase.isPostFlag AndAlso PriceList
            btnUnpost.Visible = btnPost.Visible
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                btnprint.Visible = False
            End If
            If PriceList = True Then
                'btnprint.Visible = True
            Else
                '  btnprint.Visible = False
                btnSave.Enabled = True
            End If
            '= KUNAL > TICKET : BM00000009590 > DATE : 19-09-2016
            If objCommonVar.IsDemoERP Then
                UcAttachment1.Form_ID = MyBase.Form_ID
                RadPageView1.Pages("pgAttachment").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("pgAttachment").Item.Visibility = ElementVisibility.Collapsed
            End If
            ''RICHA TEC/30/07/19-000971 TEC/30/07/19-000971
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.PriceMaster)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            Export.Enabled = True
            Import.Enabled = True
        Else
            Export.Enabled = False
            Import.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub SetLength()
    End Sub

    Private Sub fndPriceCode__MYValidating_2(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPriceCode._MYValidating
        sql = "Select Distinct Price_Code as Code, Price_Code_Desc as [Description] ,isnull(Transfer,0) as Transfer from TSPL_PRICE_COMPONENT_MAPPING"
        fndPriceCode.Value = clsCommon.ShowSelectForm("PriceCode@PriceMaster", sql, "Code", "", fndPriceCode.Value, "", isButtonClicked)
        'FillTaxAmounts(CDec(txtMRP.Text))
        FillPriceComponent(fndPriceCode.Value)
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        txtLocation.Value = clsLocation.getFinder("Location_Type='Physical'", "" + txtLocation.Value + "", isButtonClicked)
        lblLocationDesc.Text = clsLocation.GetName(txtLocation.Value, Nothing)
    End Sub

    Private Sub fndTaxGrp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTaxGrp._MYValidating
        Dim strGstActiveTaxGrp As String = Nothing
        If objCommonVar.GSTActiveTaxGroup Then
            strGstActiveTaxGrp = " and TSPL_TAX_GROUP_MASTER.Active=1"
        End If
        Dim qry As String = "SELECT [Tax_Group_Code] as [Code],[Tax_Group_Desc] as [Description],[Tax_Group_Type] as [Group Type] FROM [dbo].[TSPL_TAX_GROUP_MASTER]  where Tax_Group_Type in ('S','T') " & strGstActiveTaxGrp & ""
        ''richa TEC/30/07/19-000971 if item is of Non taxable type then show only exempted type of taxes
        If clsCommon.myLen(txtItemCode.Value) > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTaxable,0) from TSPL_ITEM_MASTER where  Item_Code ='" & txtItemCode.Value & "'")), "0") = CompairStringResult.Equal Then
                qry += " and TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 "
            End If
        End If
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TGPrice Master", qry, "Code", fndTaxGrp.Value)
        If dr IsNot Nothing Then
            fndTaxGrp.Value = clsCommon.myCstr(dr("Code"))
            txtTaxGrp.Text = clsTaxMaster.GetName(fndTaxGrp.Value)
            FillTaxDetail(fndTaxGrp.Value, clsCommon.myCstr(dr("Group Type")))
        End If


    End Sub

    Private Sub FillPriceComponent(ByVal strPriceCode As String)
        Try
            grdPriceComp.DataSource = Nothing
            grdPriceComp.Rows.Clear()
            sql = "SELECT [Price_code] as [Price Code] ,[Price_Code_Desc] as [Desc] ,[Remarks] as [Remarks] ,[Price_Comp_Code] as [Price Componant Code]  " & _
            ",[Price_Comp_Desc] as [Description], 0 as [Amount]" & _
            ",PRICE_CALCULATION_METHOD AS [Price Type],Transfer " & _
            " FROM [TSPL_PRICE_COMPONENT_MAPPING] where Price_code = '" + strPriceCode + "' order by Price_Component_Map_Code Asc"
            dt = clsDBFuncationality.GetDataTable(sql)
            grdPriceComp.DataSource = dt
            If dt.Rows.Count > 0 Then
                txtPriceCodeDesc.Text = clsCommon.myCstr(dt.Rows(0)("Desc"))

                If clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("Transfer")), 1) = CompairStringResult.Equal Then
                    ddlType.Text = "Transfer"
                    ddlType.Enabled = False
                ElseIf clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("Transfer")), 0) = CompairStringResult.Equal Then
                    ddlType.Text = "Sale"
                    ddlType.Enabled = False
                End If
            Else
                txtPriceCodeDesc.Text = ""
            End If
            FillPriceCompAmounts(clsCommon.myCdbl(txtMRP.Text))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FillPriceCompAmounts(ByVal MRP As Decimal, Optional ByVal trans As SqlTransaction = Nothing)
        sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + txtPriceId.Value + "' AND " & _
                  " Uom_Code='" + fnduom.Value + "'"
        Dim convertFact As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, sql))
        Dim amt As Decimal = 0
        Dim count As Integer = 0
        For Each dr As GridViewRowInfo In grdPriceComp.Rows
            If Not convertFact = 0 Then
                If count = 1 Then
                    If dr.Cells("Price Type").Value = "Percentage" Then
                        dr.Cells("TotalAmt").Value = Math.Round(CDec(dr.Cells("Amount").Value) * MRP / 100, 5, MidpointRounding.ToEven)
                        If CDec(dr.Cells("TotalAmt").Value) > 0 Then
                            dr.Cells("TotalAmt").Value = CDec(dr.Cells("TotalAmt").Value) - amt
                        Else
                            dr.Cells("TotalAmt").Value = 0.0
                        End If
                    Else
                        dr.Cells("TotalAmt").Value = dr.Cells("Amount").Value
                        dr.Cells("TotalAmt").Value = Math.Round(CDec(dr.Cells("TotalAmt").Value) / convertFact, 5, MidpointRounding.ToEven)
                    End If
                Else
                    If dr.Cells("Price Type").Value = "Percentage" Then
                        dr.Cells("TotalAmt").Value = Math.Round(CDec(dr.Cells("Amount").Value) * MRP / 100, 5, MidpointRounding.ToEven)
                    Else
                        dr.Cells("TotalAmt").Value = dr.Cells("Amount").Value
                        dr.Cells("TotalAmt").Value = Math.Round(CDec(dr.Cells("TotalAmt").Value) / convertFact, 5, MidpointRounding.ToEven)
                    End If
                End If

            End If
            amt += dr.Cells("TotalAmt").Value
        Next
        lblPriceCompTotal.Text = clsCommon.myCstr(amt)
    End Sub

    Dim IsLoadTaxData As Boolean = False
    Private Sub FillTaxDetail(ByVal strTaxGroupCode As String, ByVal tax_Type As String)
        Try
            IsLoadTaxData = True
            grdTax.DataSource = Nothing
            grdTax.Rows.Clear()
            sql = " SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code,max(TSPL_TAX_MASTER.Excisable) as  Excisable, MAX(TSPL_TAX_MASTER.Type) as TaxType,max(g.Tax_On_Base_Amount) as Tax_On_Base_Amount" &
                  " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN  TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code and R.Tax_Type='" + tax_Type + "' INNER JOIN TSPL_TAX_MASTER ON G.Tax_Code = TSPL_TAX_MASTER.Tax_Code " &
                  "  WHERE (G.Tax_Group_Code = '" + strTaxGroupCode + "') AND (G.Tax_Group_Type = '" + tax_Type + "') " &
                  "  GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " &
                  "  ORDER BY G.Trans_Code"
            dt = clsDBFuncationality.GetDataTable(sql)

            If CalculateTaxRatefromItemwsieTaxOnSale = True Then
                Dim dt1 As DataTable
                Dim qry As String = "select IsTaxable from tspl_item_master where Item_Code='" + clsCommon.myCstr(txtItemCode.Value) + "'"
                Dim IsTaxable As Int16 = 0
                IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

                If IsTaxable = 1 Then
                    qry = "select top 1 * from  TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " &
                      "left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE " &
                      "and TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE  " &
                      "where Status=1 and TSPL_ITEM_WISE_TAX.Type= 'S' and DOC_DATE < ='" & clsCommon.GetPrintDate(dtpStart.Value, "dd/MMM/yyyy") & "' and Item_Code='" & clsCommon.myCstr(txtItemCode.Value) & "' order by DOC_DATE desc "

                    dt1 = clsDBFuncationality.GetDataTable(qry)
                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        Against_ItemwiseTaxCode = clsCommon.myCstr(dt1.Rows(0)("HCODE"))

                        'sanjay
                        ''                       sql = " SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code," & _
                        ''      " (SELECT TOP 1 TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate from  TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " & _
                        ''            " left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE " & _
                        ''            " and TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE  " & _
                        ''            " where Status=1 and TSPL_ITEM_WISE_TAX.Type= 'S' " & _
                        ''            " and DOC_DATE < ='" & clsCommon.GetPrintDate(dtpStart.Value, "dd/MMM/yyyy") & "'" & _
                        ''            " and TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority=G.Tax_Code" & _
                        ''                        " and Item_Code='" & clsCommon.myCstr(txtItemCode.Value) & "' order by DOC_DATE desc )" & _
                        ''" AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code,max(TSPL_TAX_MASTER.Excisable) as  Excisable, MAX(TSPL_TAX_MASTER.Type) as TaxType" & _
                        ''                " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN  TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code and R.Tax_Type='" + tax_Type + "' INNER JOIN TSPL_TAX_MASTER ON G.Tax_Code = TSPL_TAX_MASTER.Tax_Code " & _
                        ''                "  WHERE (G.Tax_Group_Code = '" + strTaxGroupCode + "') AND (G.Tax_Group_Type = '" + tax_Type + "') " & _
                        ''                "  GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
                        ''                "  ORDER BY G.Trans_Code"

                        'sanjay
                        'Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr(colICode)).Value), txtTaxGroup.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "P")
                        'If objTAXRate IsNot Nothing Then
                        '    gv1.CurrentRow.Cells(clsCommon.myCstr(colAgainstItemWiseTaxCode)).Value = objTAXRate.HCODE
                        '    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTAXRate.TAX_Rate
                        'End If
                        'sanjay


                        dt = clsDBFuncationality.GetDataTable(sql)

                        For i As Int16 = 0 To dt.Rows.Count() - 1
                            Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(txtItemCode.Value), strTaxGroupCode, clsCommon.myCstr(dt.Rows(i).Item("Tax_Code")), clsCommon.GetPrintDate(dtpStart.Value, "dd/MMM/yyyy"), "S")
                            If objTAXRate IsNot Nothing Then
                                dt.Rows(i).Item("Tax_Rate") = objTAXRate.TAX_Rate
                            End If
                        Next

                        grdTax.Columns("Tax Rate").ReadOnly = True
                    Else
                        common.clsCommon.MyMessageBoxShow("Create Item Wise Tax.", Me.Text, MessageBoxButtons.OK)
                        fndTaxGrp.Value = ""
                        Exit Sub
                    End If
                End If
            End If

            grdTax.DataSource = dt
            FillTaxAmounts()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadTaxData = False
        End Try
    End Sub

    Private Sub FillTaxAmounts(Optional ByVal trans As SqlTransaction = Nothing)
        Try
            If Not IsInsideLoadData Then
                Dim arrTaxableAuth As New List(Of String)
                Dim CriteriaAmt As Decimal = 0
                If Not chkBackCalculation.Checked Then
                    If clsCommon.CompairString(ddlTaxMnpln.Text, "Landing Cost") = CompairStringResult.Equal Then
                        CriteriaAmt = clsCommon.myCdbl(txtLandingCost.Text)
                    Else
                        CriteriaAmt = clsCommon.myCdbl(txtBasic.Text)
                    End If
                    Dim ExciseDuty As Double = 0
                    Dim amt As Double = 0
                    Dim STax As Double = 0
                    Dim OtherTax As Double = 0

                    Dim TotalTaxableAmt As Double = 0

                    Dim MRPChk As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select NoMRP from TSPL_ITEM_MASTER where Item_Code ='" + txtItemCode.Value + "'", trans))
                    If grdTax.Rows.Count > 1 Then
                        For Each dr As GridViewRowInfo In grdTax.Rows
                            'If Not dr.Index = grdTax.Rows.Count - 1 Then
                            If dr.Cells("SurTax").Value.ToString.ToUpper = "N" And dr.Cells("Excisable").Value.ToString.ToUpper = "Y" Then
                                dr.Cells("Amount").Value = Math.Round(dr.Cells("Tax Rate").Value * (CriteriaAmt + TotalTaxableAmt) / 100, 5, MidpointRounding.ToEven)
                                ExciseDuty = dr.Cells("Amount").Value
                                amt += dr.Cells("Amount").Value
                            ElseIf dr.Cells("SurTax").Value.ToString.ToUpper = "N" And dr.Cells("Excisable").Value.ToString.ToUpper = "N" Then
                                dr.Cells("Amount").Value = Math.Round((((CriteriaAmt + TotalTaxableAmt) * clsCommon.myCdbl(txtAbtRate.Text)) / 100) * (dr.Cells("Tax Rate").Value / 100), 5, MidpointRounding.ToEven)
                                STax = STax + dr.Cells("Amount").Value
                                amt += clsCommon.myCdbl(dr.Cells("Amount").Value)
                            Else
                                Dim SurTaxCode As String = dr.Cells("SurTaxCode").Value
                                Dim SurTaxAmt As Decimal = 0
                                Dim drFindSurTax As Telerik.WinControls.UI.GridViewRowInfo
                                For Each drFindSurTax In grdTax.Rows
                                    If drFindSurTax.Cells("Tax Code").Value = SurTaxCode Then
                                        SurTaxAmt = Math.Round(drFindSurTax.Cells("Tax Rate").Value * CriteriaAmt / 100, 5, MidpointRounding.ToEven)
                                    End If
                                Next
                                dr.Cells("Amount").Value = Math.Round(dr.Cells("Tax Rate").Value * SurTaxAmt / 100, 5, MidpointRounding.ToEven)
                                OtherTax += dr.Cells("Amount").Value
                                amt += clsCommon.myCdbl(dr.Cells("Amount").Value)
                            End If

                            ''==================if taxable amount then taxable amt is added======================
                            If clsCommon.CompairString(dr.Cells("Taxable").Value, "Y") = CompairStringResult.Equal Then
                                TotalTaxableAmt += clsCommon.myCdbl(dr.Cells("Amount").Value)
                            End If
                            ''================================================================================


                            'Else
                            'If MRPChk = 1 Then
                            '    dr.Cells("Amount").Value = Math.Round((CriteriaAmt + amt) / (100) * dr.Cells("Tax Rate").Value, 5, MidpointRounding.ToEven)
                            'Else
                            '    dr.Cells("Amount").Value = Math.Round((CriteriaAmt + amt) * clsCommon.myCdbl(dr.Cells("Tax Rate").Value) / 100, 5, MidpointRounding.ToEven)
                            'End If
                            'STax = STax + dr.Cells("Amount").Value
                            'amt += clsCommon.myCdbl(dr.Cells("Amount").Value)
                            'End If
                        Next
                    ElseIf grdTax.Rows.Count = 1 Then
                        ExciseDuty = 0
                        OtherTax = 0
                        If MRPChk = 1 Then
                            grdTax.Rows(0).Cells("Amount").Value = Math.Round((CriteriaAmt + amt) / (100) * grdTax.Rows(0).Cells("Tax Rate").Value, 5, MidpointRounding.ToEven)
                        Else
                            grdTax.Rows(0).Cells("Amount").Value = Math.Round((CriteriaAmt + amt) * clsCommon.myCdbl(grdTax.Rows(0).Cells("Tax Rate").Value) / 100, 5, MidpointRounding.ToEven)
                        End If
                        STax = grdTax.Rows(0).Cells("Amount").Value
                    End If
                Else
                    Dim dblTotalNonTabxableRate As Double = 0
                    For ii As Integer = 0 To grdTax.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(grdTax.Rows(ii).Cells("Taxable").Value), "N") = CompairStringResult.Equal Then
                            dblTotalNonTabxableRate += clsCommon.myCdbl(grdTax.Rows(ii).Cells("Tax Rate").Value)
                            If clsCommon.CompairString(clsCommon.myCstr(grdTax.Rows(ii).Cells("Excisable").Value), "Y") = CompairStringResult.Equal Then
                                Throw New Exception("Wrong configuration of Tax -" + clsCommon.myCstr(grdTax.Rows(ii).Cells("Tax Code").Value) + ".A Tax can't be Excisable and non Taxable ")
                            End If
                        End If
                    Next
                    Dim dblTotalNonTabxableAmount As Double = 0
                    If chkBackCalWithTAX.Checked = True Then
                        dblTotalNonTabxableAmount = (txtBasic.Value * 100) / (100 + dblTotalNonTabxableRate)
                    Else
                        dblTotalNonTabxableAmount = txtBasic.Value
                    End If

                    For ii As Integer = 0 To grdTax.RowCount - 1
                        Dim dblTaxableAmount As Double = 0
                        If clsCommon.CompairString(clsCommon.myCstr(grdTax.Rows(ii).Cells("SurTax").Value), "Y") = CompairStringResult.Equal Then
                            For jj As Integer = 0 To ii - 1
                                If clsCommon.CompairString(clsCommon.myCstr(grdTax.Rows(ii).Cells("SurTaxCode").Value), clsCommon.myCstr(grdTax.Rows(jj).Cells("Tax Code").Value)) = CompairStringResult.Equal Then
                                    dblTaxableAmount += clsCommon.myCdbl(grdTax.Rows(jj).Cells("Amount").Value)
                                End If
                            Next
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grdTax.Rows(ii).Cells("Excisable").Value), "Y") = CompairStringResult.Equal Then
                            dblTaxableAmount = txtAbtAmount.Value
                        Else
                            ''richa 11 Sep 2020
                            Dim dblOtherTaxAmt As Double = 0
                            Dim strTaxCode As String = clsCommon.myCstr(grdTax.Rows(ii).Cells("Tax Code").Value)
                            Dim IsTaxOnBaseAmt As Boolean = clsCommon.myCBool(IIf(grdTax.Rows(ii).Cells("Tax_On_Base_Amount").Value = "Y", True, False))
                            Dim IsTaxable As Boolean = clsCommon.myCBool(IIf(grdTax.Rows(ii).Cells("Taxable").Value = "Y", True, False))
                            If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                                arrTaxableAuth.Add(strTaxCode.ToUpper())
                            End If
                            If Not IsTaxOnBaseAmt Then
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(ii, ii, arrTaxableAuth)
                            End If
                            dblTaxableAmount = dblTotalNonTabxableAmount + dblOtherTaxAmt
                        End If
                        grdTax.Rows(ii).Cells("Amount").Value = Math.Round(((dblTaxableAmount * clsCommon.myCdbl(grdTax.Rows(ii).Cells("Tax Rate").Value)) / 100), 5, MidpointRounding.ToEven)
                    Next
                End If
                UpdateAllTotal()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 0 To grdTax.RowCount - 1
                Dim strii As String = clsCommon.myCstr(ii)
                If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(grdTax.Rows(ii).Cells("Tax Code").Value)) = CompairStringResult.Equal Then
                    dblRet = dblRet + clsCommon.myCdbl(grdTax.Rows(ii).Cells("Amount").Value)
                End If
            Next
        Next
        Return dblRet
    End Function
    ''To bind item according to the pack size
    Private Sub LoadChildItems(ByVal strItemCode As String)
        'gvItem.DataSource = Nothing
        'Dim value As String = txtPriceId.Value.Substring(2, 6) 'ddlpacksize.Text
        'value = "%" + value + "%"
        'sql = "select Cast(0 as Bit) as Select, Item_Code as [Item Code], Item_Desc as [Description]  from tspl_item_master where item_code like '" + value + "' "
        'gvItem.DataSource = clsDBFuncationality.GetDataTable(sql)
    End Sub

    '-----------Loads Item Price Components------------
    Private Sub LoadDataInFinderGrid()
        Dim qry As String = "select Item_Price_ID,REPLACE(CONVERT(varchar(10), Start_Date,102),'.','-' ) as Date,Item_Basic_Net as MRP,UOM,Price_Code as [Price Code],Price_Code_Desc as [Price Description]"
        qry += " from TSPL_ITEM_PRICE_MASTER"
        qry += " where Item_Code='" + txtPriceId.Value + "'"
        qry += " order by Start_Date,Item_Basic_Net ,Price_Code"
        gv1.DataSource = clsDBFuncationality.GetDataTable(qry)

        gv1.Columns("Item_Price_ID").IsVisible = False
        gv1.Columns("Item_Price_ID").ReadOnly = True

        gv1.Columns("Date").Width = 100
        gv1.Columns("Date").ReadOnly = True

        gv1.Columns("MRP").Width = 60
        gv1.Columns("MRP").ReadOnly = True

        gv1.Columns("UOM").Width = 100
        gv1.Columns("UOM").ReadOnly = True

        gv1.Columns("Price Code").Width = 100
        gv1.Columns("Price Code").ReadOnly = True

        gv1.Columns("Price Description").Width = 300
        gv1.Columns("Price Description").ReadOnly = True

        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = True

        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = True
    End Sub

    Private Sub ResetScreen()
        txtAbtRate.Value = 0
        txtAbtAmount.Enabled = False
        txtAbtAmount.Value = 0
        txtPriceId.Value = ""
        txtRemarks.Text = ""
        txtItemCode.Value = ""
        fndTaxGrp.Value = ""
        fndPriceCode.Value = ""
        txtPriceCodeDesc.Text = ""
        lblItemDesc.Text = ""
        txtMRP.Text = ""
        txtBasic.Text = ""
        txtPurchaseCost.Text = ""
        lblPriceCompTotal.Text = "0.0"
        dtpStart.Format = DateTimePickerFormat.Short
        dtpStart.Value = Date.Now
        PageMode = "New"
        fnduom.Value = ""
        grdPriceComp.DataSource = Nothing
        grdPriceComp.Rows.Clear()
        grdTax.DataSource = Nothing
        grdTax.Rows.Clear()
        btnSave.Text = "Save"
        canEdit = "Y"
        ChkActive.Checked = True
        IsNewEntry = True
        chkAuto.Checked = True
        chkBackCalculation.Checked = True
        backCalculationChecked(chkBackCalculation.Checked)
        '= KUNAL > TICKET : BM00000009590 > DATE : 19-09-2016
        UcAttachment1.BlankAllControls()
        btnSave.Enabled = True
        btnDelete.Enabled = True
        arrCommission = Nothing
        grdTax.Columns("Tax Rate").ReadOnly = False
        Against_ItemwiseTaxCode = ""
        chkWithoutTax.Checked = False
        chkBackCalWithTAX.Checked = False
        txtMRP.Enabled = False
        txtTotalItemPrice.Text = ""
        cbgUOM.DataSource = Nothing
        txtPlanningCode.Text = ""
    End Sub

    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged
        If dtpStart.Value <> dtpStart.NullDate Then
            FillAbtDetail(dtpStart.Value.Date)
        End If
    End Sub

    Private Function chkForExistItemPrice(ByVal code As String, ByVal Start As Date, ByVal MRP As Decimal, ByVal priceCode As String, ByVal uom As String) As Boolean
        Dim sql As String = "select Count(Item_Code) from TSPL_ITEM_PRICE_MASTER where Start_Date = '" + Format(Start, "MM/dd/yyyy") + "' and Item_Code ='" + code + "' " & _
        " and Item_Basic_Net ='" + MRP.ToString() + "' AND Price_Code='" + priceCode + "' and UOM = '" + uom + "'"
        If CInt(connectSql.RunScalar(sql)) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub FillAbtDetail(ByVal Start As Date)
        Dim dtMaster As DataTable
        dtMaster = (New BAL.BALPriceComponant).GetAbtMasterOnDate(Start)
        If (dtMaster.Rows.Count > 0) Then
            txtAbtRate.Text = dtMaster.Rows(0)("Rate").ToString()
        Else
            ShowMsg("No abatement exist for this date. Enter abatement then create Item price rate")
        End If
    End Sub

    Private Sub ShowMsg(ByVal Msg As String)
        common.clsCommon.MyMessageBoxShow(Msg, "Price Master")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If ValidateSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.PriceMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim Arr As New List(Of clsPriceMaster)
                Dim dtUOMPrice As New DataTable()
                dtUOMPrice.Columns.Add("SN", GetType(Integer))
                dtUOMPrice.Columns.Add("UOM", GetType(String))
                dtUOMPrice.Columns.Add("Item_Price_Id", GetType(String))
                Dim obj As clsPriceMaster = Nothing
                dtUOMPrice.Rows.Add(1, fnduom.Value, txtPriceId.Value)
                If chkAuto.Checked Then
                    For Each grow As GridViewRowInfo In cbgUOM.Rows
                        If grow.Cells("Select").Value = True Then
                            dtUOMPrice.Rows.Add(grow.Index + 2, clsCommon.myCstr(grow.Cells("UOM").Value), clsCommon.myCstr(grow.Cells("PriceId").Value))
                        End If
                    Next
                End If
                dt = clsPriceMaster.GetAllConversionOfItem(txtItemCode.Value)
                For Each dr As DataRow In dtUOMPrice.Rows
                    obj = New clsPriceMaster()
                    dt.DefaultView.RowFilter = "UOM2='" & fnduom.Value & "' AND UOM1='" & dr("UOM") & "'"
                    obj.Item_Price_ID = clsCommon.myCstr(dr("Item_Price_Id"))
                    If clsCommon.myLen(obj.Item_Price_ID) > 0 Then
                        obj.IsNewEntry = False
                    Else
                        obj.IsNewEntry = True
                    End If
                    obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                    obj.Item_Code = clsCommon.myCstr(txtItemCode.Value)
                    obj.UOM = clsCommon.myCstr(dr("UOM"))
                    obj.Item_Basic_Price_Type = clsCommon.myCstr(ddlBasicRate.Text)
                    obj.Markup_On = clsCommon.myCstr(ddlMarkup.Text)
                    obj.Markup_Percent = clsCommon.myCdbl(txtMarkupPercent.Text)
                    obj.Basic_Price_On = clsCommon.myCstr(ddlBasicRateOn.Text)
                    obj.Landing_Cost = clsCommon.myCdbl(txtLandingCost.Text) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    obj.Purchase_Cost = clsCommon.myCdbl(txtPurchaseCost.Text) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    obj.Start_Date = dtpStart.Value
                    ''added by richa agarwal
                    If ChkActive.Checked Then
                        obj.Is_Active = 1
                    Else
                        obj.Is_Active = 0
                    End If
                    ''===============
                    If chkEnd.Checked Then
                        obj.End_Date = dtpEnd.Value
                    End If
                    If chkBackCalculation.Checked Then
                        obj.Price_Category = "Auto"
                    End If
                    ' obj.Is_With_Tax = IIf(chkBackCalWithTAX.IsChecked, "Y", "N")
                    obj.Is_With_Tax = IIf(chkBackCalWithTAX.Checked, "Y", "N")
                    obj.Tax_Manipulation_On = clsCommon.myCstr(ddlTaxMnpln.Text)
                    obj.Location_Code = clsCommon.myCstr(txtLocation.Value)
                    obj.Tax_group = clsCommon.myCstr(fndTaxGrp.Value)
                    obj.Against_ItemwiseTaxCode = clsCommon.myCstr(Against_ItemwiseTaxCode)
                    For Each grow As GridViewRowInfo In grdTax.Rows
                        If grow.Index + 1 = 1 Then
                            obj.TAX1 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX1_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX1_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 2 Then
                            obj.TAX2 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX2_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX2_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 3 Then
                            obj.TAX3 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX3_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX3_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 4 Then
                            obj.TAX4 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX4_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX4_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 5 Then
                            obj.TAX5 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX5_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX5_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 6 Then
                            obj.TAX6 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX6_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX6_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 7 Then
                            obj.TAX7 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX7_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX7_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 8 Then
                            obj.TAX8 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX8_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX8_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 9 Then
                            obj.TAX9 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX1_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX9_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 10 Then
                            obj.TAX10 = clsCommon.myCstr(grow.Cells("Tax Code").Value)
                            obj.TAX10_Rate = clsCommon.myCdbl(grow.Cells("Tax Rate").Value)
                            obj.TAX10_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        End If
                    Next

                    obj.Price_Code = clsCommon.myCstr(fndPriceCode.Value)
                    obj.Price_Code_Desc = clsCommon.myCstr(txtPriceCodeDesc.Text)
                    For Each grow As GridViewRowInfo In grdPriceComp.Rows
                        If grow.Index + 1 = 1 Then
                            obj.Price_Comp1 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc1 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate1 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount1 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 2 Then
                            obj.Price_Comp2 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc2 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate2 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount2 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 3 Then
                            obj.Price_Comp3 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc3 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate3 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount3 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 4 Then
                            obj.Price_Comp4 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc4 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate4 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount4 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 5 Then
                            obj.Price_Comp5 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc5 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate5 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount5 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 6 Then
                            obj.Price_Comp6 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc6 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate6 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount6 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 7 Then
                            obj.Price_Comp7 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc7 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate7 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount7 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 8 Then
                            obj.Price_Comp8 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc8 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate8 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount8 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 9 Then
                            obj.Price_Comp9 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc9 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate9 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount9 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        ElseIf grow.Index + 1 = 10 Then
                            obj.Price_Comp10 = clsCommon.myCstr(grow.Cells("Price Componant").Value)
                            obj.Price_Comp_Desc10 = clsCommon.myCstr(grow.Cells("Description").Value)
                            obj.Price_Rate10 = clsCommon.myCdbl(grow.Cells("Amount").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                            obj.Price_Amount10 = clsCommon.myCdbl(grow.Cells("TotalAmt").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        End If
                    Next
                    obj.Item_Basic_Net = clsCommon.myCdbl(txtMRP.Text) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    obj.Item_MRP = clsCommon.myCdbl(txtMRP.Text) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    obj.Item_Basic_Price = Math.Round(clsCommon.myCdbl(txtBasic.Text) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor")), 5, MidpointRounding.ToEven)
                    obj.Abatement_Rate = clsCommon.myCdbl(txtAbtRate.Text)
                    'obj.Abatement = clsCommon.myCdbl(txtAbtAmount.Text) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Dim Abt As Double = clsCommon.myCdbl(txtAbtAmount.Text) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    obj.Abatement = Abt.ToString("N6")
                    obj.Can_Edit = "Y"
                    If clsCommon.CompairString(ddlType.Text, "Transfer") = CompairStringResult.Equal Then
                        obj.type = "T"
                    ElseIf clsCommon.CompairString(ddlType.Text, "Sale") = CompairStringResult.Equal Then
                        obj.type = "S"
                    Else
                        obj.type = ""
                    End If
                    obj.Item_Selling_Price = clsCommon.myCdbl(lbllSellingPrice.Text) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Arr.Add(obj)
                Next
                If clsPriceMaster.SaveData(Arr, True, chkBackCalculation.Checked) Then
                    '= KUNAL > TICKET : BM00000009590 > DATE : 19-09-2016
                    If obj.Item_Price_ID.Length > 0 AndAlso obj.Item_Price_ID IsNot Nothing Then
                        UcAttachment1.SaveData(obj.Item_Price_ID)
                    End If
                    If isCheckCustomerType Then
                        If arrCommission IsNot Nothing AndAlso arrCommission.Count > 0 Then
                            clsPOSCommissionMapping.SaveData(arrCommission, txtPriceId.Value)
                        End If
                    End If
                    clsCommon.MyMessageBoxShow("Data saved succesfully.")
                    LoadData(obj.Item_Price_ID, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Function SetData(ByVal Arr As List(Of clsPOSCommissionMapping)) As List(Of clsPOSCommissionMapping)
        If Arr.Count > 0 Then
            arrCommission = Arr
        End If
        Return arrCommission
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()

    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then

                If (clsPriceMaster.PostData(MyBase.Form_ID, txtPriceId.Value)) Then
                    msg = "Successfully Posted"
                Else

                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(txtPriceId.Value, NavigatorType.Current)


            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub LoadData(ByVal strItemPriceId As String, ByVal NavType As NavigatorType)
        Try
            ResetScreen()
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnprint.Enabled = True
            btnDelete.Enabled = True
            Dim obj As New clsPriceMaster
            obj = clsPriceMaster.GetData(strItemPriceId, NavType, Nothing)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Price_ID) > 0 Then
                IsInsideLoadData = True
                txtPriceId.Value = obj.Item_Price_ID
                txtRemarks.Text = obj.Remarks

                txtItemCode.Value = obj.Item_Code
                LoadItemData(obj.Item_Code)
                ddlBasicRate.Text = clsCommon.myCstr(obj.Item_Basic_Price_Type)
                ddlBasicRateOn.Text = obj.Basic_Price_On
                ddlMarkup.Text = clsCommon.myCstr(obj.Markup_On)
                txtMarkupPercent.Text = clsCommon.myCstr(obj.Markup_Percent)
                dtpStart.Value = obj.Start_Date
                If clsCommon.myLen(obj.End_Date) > 0 Then
                    chkEnd.Checked = True
                    dtpEnd.Value = obj.End_Date
                End If
                fndTaxGrp.Value = obj.Tax_group
                lbllSellingPrice.Text = clsCommon.myCstr(obj.Item_Selling_Price)
                ddlTaxMnpln.Text = obj.Tax_Manipulation_On
                txtLocation.Value = obj.Location_Code
                lblLocationDesc.Text = obj.Location_Desc

                ''added by richa agarwal
                If obj.Is_Active Then
                    ChkActive.Checked = True
                Else
                    ChkActive.Checked = False
                End If
                ''===============
                fnduom.Value = obj.UOM
                Against_ItemwiseTaxCode = clsCommon.myCstr(obj.Against_ItemwiseTaxCode)
                FillTaxDetail(obj.Tax_group, IIf(obj.type = 0, "S", "T"))
                '--------------Back Calculation---------------------
                chkBackCalculation.Checked = False
                'chkBackCalWithTAX.IsChecked = False
                'chkBaackCalWOutTax.IsChecked = False
                chkBackCalWithTAX.Checked = False
                chkWithoutTax.Checked = False
                If clsCommon.CompairString(obj.Price_Category, "Auto") = CompairStringResult.Equal Then
                    chkAuto.Checked = True
                    chkBackCalculation.Checked = True
                    If clsCommon.CompairString(obj.Is_With_Tax, "Y") = CompairStringResult.Equal Then
                        chkBackCalWithTAX.Checked = True
                    Else
                        chkWithoutTax.Checked = True
                    End If
                End If
                For Each grow As GridViewRowInfo In grdTax.Rows
                    If grow.Index + 1 = 1 Then
                        'grow.Cells("Tax Code").Value=obj.TAX1
                        grow.Cells("Tax Rate").Value = obj.TAX1_Rate
                        grow.Cells("Amount").Value = obj.TAX1_Amt
                    ElseIf grow.Index + 1 = 2 Then
                        'grow.Cells("Tax Code").Value=obj.TAX2 
                        grow.Cells("Tax Rate").Value = obj.TAX2_Rate
                        grow.Cells("Amount").Value = obj.TAX2_Amt
                    ElseIf grow.Index + 1 = 3 Then
                        'grow.Cells("Tax Code").Value=obj.TAX3
                        grow.Cells("Tax Rate").Value = obj.TAX3_Rate
                        grow.Cells("Amount").Value = obj.TAX3_Amt
                    ElseIf grow.Index + 1 = 4 Then
                        'grow.Cells("Tax Code").Value=obj.TAX4
                        grow.Cells("Tax Rate").Value = obj.TAX4_Rate
                        grow.Cells("Amount").Value = obj.TAX4_Amt
                    ElseIf grow.Index + 1 = 5 Then
                        'grow.Cells("Tax Code").Value=obj.TAX5 
                        grow.Cells("Tax Rate").Value = obj.TAX5_Rate
                        grow.Cells("Amount").Value = obj.TAX5_Amt
                    ElseIf grow.Index + 1 = 6 Then
                        'grow.Cells("Tax Code").Value = obj.TAX6
                        grow.Cells("Tax Rate").Value = obj.TAX6_Rate
                        grow.Cells("Amount").Value = obj.TAX6_Amt
                    ElseIf grow.Index + 1 = 7 Then
                        'grow.Cells("Tax Code").Value=obj.TAX7 
                        grow.Cells("Tax Rate").Value = obj.TAX7_Rate
                        grow.Cells("Amount").Value = obj.TAX7_Amt
                    ElseIf grow.Index + 1 = 8 Then
                        'grow.Cells("Tax Code").Value=obj.TAX8
                        grow.Cells("Tax Rate").Value = obj.TAX8_Rate
                        grow.Cells("Amount").Value = obj.TAX8_Amt
                    ElseIf grow.Index + 1 = 9 Then
                        'grow.Cells("Tax Code").Value=obj.TAX9
                        grow.Cells("Tax Rate").Value = obj.TAX1_Rate
                        grow.Cells("Amount").Value = obj.TAX9_Amt
                    ElseIf grow.Index + 1 = 10 Then
                        'grow.Cells("Tax Code").Value = obj.TAX10
                        grow.Cells("Tax Rate").Value = obj.TAX10_Rate
                        grow.Cells("Amount").Value = obj.TAX10_Amt
                    End If
                Next

                fndPriceCode.Value = obj.Price_Code
                txtPriceCodeDesc.Text = obj.Price_Code_Desc
                FillPriceComponent(obj.Price_Code)
                For Each grow As GridViewRowInfo In grdPriceComp.Rows
                    If grow.Index + 1 = 1 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp1 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc1 
                        grow.Cells("Amount").Value = obj.Price_Rate1
                        grow.Cells("TotalAmt").Value = obj.Price_Amount1
                    ElseIf grow.Index + 1 = 2 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp2 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc2 
                        grow.Cells("Amount").Value = obj.Price_Rate2
                        grow.Cells("TotalAmt").Value = obj.Price_Amount2
                    ElseIf grow.Index + 1 = 3 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp3 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc3 
                        grow.Cells("Amount").Value = obj.Price_Rate3
                        grow.Cells("TotalAmt").Value = obj.Price_Amount3
                    ElseIf grow.Index + 1 = 4 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp4 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc4 
                        grow.Cells("Amount").Value = obj.Price_Rate4
                        grow.Cells("TotalAmt").Value = obj.Price_Amount4
                    ElseIf grow.Index + 1 = 5 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp5 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc5 
                        grow.Cells("Amount").Value = obj.Price_Rate5
                        grow.Cells("TotalAmt").Value = obj.Price_Amount5
                    ElseIf grow.Index + 1 = 6 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp6 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc6 
                        grow.Cells("Amount").Value = obj.Price_Rate6
                        grow.Cells("TotalAmt").Value = obj.Price_Amount6
                    ElseIf grow.Index + 1 = 7 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp7 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc7 
                        grow.Cells("Amount").Value = obj.Price_Rate7
                        grow.Cells("TotalAmt").Value = obj.Price_Amount7
                    ElseIf grow.Index + 1 = 8 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp8 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc8 
                        grow.Cells("Amount").Value = obj.Price_Rate8
                        grow.Cells("TotalAmt").Value = obj.Price_Amount8
                    ElseIf grow.Index + 1 = 9 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp9 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc9 
                        grow.Cells("Amount").Value = obj.Price_Rate9
                        grow.Cells("TotalAmt").Value = obj.Price_Amount9
                    ElseIf grow.Index + 1 = 10 Then
                        'grow.Cells("Price Componant").Value=obj.Price_Comp10 
                        'grow.Cells("Description").Value=obj.Price_Comp_Desc10 
                        grow.Cells("Amount").Value = obj.Price_Rate10
                        grow.Cells("TotalAmt").Value = obj.Price_Amount10
                    End If
                Next
                If clsCommon.CompairString(obj.type, 0) = CompairStringResult.Equal Then
                    ddlType.Text = "Sale"
                ElseIf clsCommon.CompairString(obj.type, 1) = CompairStringResult.Equal Then
                    ddlType.Text = "Transfer"
                Else
                    ddlType.Text = ""
                End If
                If clsCommon.myLen(obj.Against_Plan_TR_Code) > 0 Then
                    txtPlanningCode.Text = clsDBFuncationality.getSingleValue("select Plan_Code from TSPL_ITEM_PRICE_PLAN_DETAIL where plan_tr_code='" + obj.Against_Plan_TR_Code + "'")
                Else
                    txtPlanningCode.Text = ""
                End If
                txtAbtRate.Text = obj.Abatement_Rate
                txtBasic.Text = obj.Item_Basic_Price
                txtMRP.Text = obj.Item_MRP
                txtAbtAmount.Text = obj.Abatement
                txtLandingCost.Text = obj.Landing_Cost
                txtPurchaseCost.Text = obj.Purchase_Cost
                canEdit = obj.Can_Edit
                LoadItemUOM(txtItemCode.Value, fnduom.Value)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                IsNewEntry = False
                If PriceList Then
                    If (clsCommon.myCdbl(obj.Posted)) = 1 Then
                        btnSave.Enabled = False
                        btnPost.Enabled = False
                        btnUnpost.Enabled = True
                        btnprint.Enabled = True
                        btnDelete.Enabled = False
                    Else
                        btnUnpost.Enabled = False
                    End If
                End If

                '= KUNAL > TICKET : BM00000009590 > DATE : 19-09-2016
                If obj.Item_Price_ID IsNot Nothing AndAlso obj.Item_Price_ID.Length > 0 Then
                    UcAttachment1.LoadData(obj.Item_Price_ID)
                End If
            Else
                btnSave.Text = "Save"
            End If

            If Not PriceList Then
                btnSave.Enabled = True
            End If
            If clsCommon.myLen(clsCommon.myCstr(obj.Against_Plan_TR_Code)) > 0 Then
                btnSave.Enabled = False
                btnDelete.Enabled = False
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub

    Private Sub FUnFillEntry()
        Dim strtDate As String = clsCommon.GetPrintDate(dtpStart.Value.Date, "yyyy-MM-dd")
        LoadData(txtPriceId.Value, NavigatorType.Current)
        Dim PriceId As String = "select Item_Price_ID  from TSPL_ITEM_PRICE_MASTER where Item_Code='" + txtPriceId.Value + "' and [Start_Date]='" + strtDate + "' and UOM ='FC'"
        PriceId = clsDBFuncationality.getSingleValue(PriceId)
        LoadDataForPriceCode(PriceId)
    End Sub

    Private Function ValidateSave() As Boolean
        If canEdit <> "Y" Then
            ShowMsg("The Item Price is in use so can not be edited")
            Return False
        ElseIf clsCommon.myLen(txtItemCode.Value) = 0 Then
            ShowMsg("Item code can not be blank")
            txtItemCode.Focus()
            Return False
        ElseIf txtAbtAmount.Text = "" And clsCommon.CompairString(ddlBasicRate.Text, "Auto") = CompairStringResult.Equal And chkBackCalculation.Checked = False Then
            ShowMsg("Abatement should not be blank")
            txtAbtAmount.Focus()
            Return False
        ElseIf clsCommon.myLen(fndPriceCode.Value) = 0 Then
            ShowMsg("Price Code should not be blank")
            fndPriceCode.Focus()
            Return False
        ElseIf clsCommon.myLen(fndTaxGrp.Value) = 0 Then
            ShowMsg("Tax Code should not be blank")
            fndTaxGrp.Focus()
            Return False
        ElseIf clsCommon.myLen(fnduom.Value) = 0 Then
            ShowMsg("UOM must be selected")
            fnduom.Focus()
            Return False
        ElseIf clsCommon.myCdbl(txtBasic.Value) < 0 Then
            ShowMsg("Basic Rate cannot be in negative")
            txtBasic.Focus()
            Return False
        ElseIf Not clsCommon.CompairString(ddlBasicRate.Text, "Auto") = CompairStringResult.Equal Then
        End If
        If Is_Mrp = True And txtMRP.Enabled = True Then
            If txtMRP.Text = "" Then
                ShowMsg("MRP should not be blank")
                txtMRP.Focus()
                Return False
            ElseIf clsCommon.myCdbl(txtMRP.Text) = 0 Then
                ShowMsg("MRP should not be 0")
                txtMRP.Focus()
                Return False
            End If
        End If
        If (chkBackCalWithTAX.Checked = False AndAlso chkWithoutTax.Checked = False) OrElse (chkBackCalWithTAX.Checked = True AndAlso chkWithoutTax.Checked = True) Then
            ShowMsg("Please select any one Tax Inclusive/ Without Tax option")
            chkBackCalWithTAX.Focus()
            Return False
        End If
        '= KUNAL > TICKET : BM00000009590 > DATE : 19-09-2016
        UcAttachment1.AllowToSave()
        Return True

    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Try
            If clsCommon.myLen(txtPriceId.Value) > 0 Then
                'If clsPriceMaster.DeleteData(txtItemCode.Value, fnduom.Value, dtpStart.Value.Date, fndPriceCode.Value, clsCommon.myCdbl(txtMRP.Text), clsCommon.myCdbl(txtBasic.Text), Nothing) Then
                '    clsCommon.MyMessageBoxShow("Record Deleted Successfully", Me.Text)
                '    ResetScreen()
                'End If
                trans = clsDBFuncationality.GetTransactin()
                clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, clsCommon.myCstr(txtPriceId.Value), "tspl_item_price_master", "Item_Price_ID", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from tspl_item_price_master where Item_Price_ID ='" & clsCommon.myCstr(txtPriceId.Value) & "'", trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow("Record Deleted Successfully", Me.Text)
                ResetScreen()
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ResetScreen()
    End Sub

    Private Sub LoadDataForPriceCode(ByVal strPriceID As String)
        Try
            sql = "select * from tspl_item_price_master where item_price_id='" & strPriceID & "'"
            ds = connectSql.RunSQLReturnDS(sql)
            Dim dtMaster As DataTable = ds.Tables(0)
            If dtMaster.Rows.Count > 0 Then
                txtBasic.Text = Math.Round(clsCommon.myCdbl(dtMaster.Rows(0)("Item_Basic_Price").ToString()), 5, MidpointRounding.ToEven)
                txtAbtRate.Text = Math.Round(clsCommon.myCdbl(dtMaster.Rows(0)("Abatement_Rate").ToString()), 5, MidpointRounding.ToEven)
                txtAbtAmount.Text = Math.Round(clsCommon.myCdbl(dtMaster.Rows(0)("Abatement").ToString()), 5, MidpointRounding.ToEven)
                txtMRP.Text = Math.Round(clsCommon.myCdbl(dtMaster.Rows(0)("Item_Basic_Net").ToString()), 5, MidpointRounding.ToEven)
                fndPriceCode.Value = dtMaster.Rows(0)("Price_Code").ToString()
                dtpStart.Value = dtMaster.Rows(0)("Start_Date")
                canEdit = dtMaster.Rows(0)("can_Edit").ToString()
                ITemPRiceID = dtMaster.Rows(0)("Item_Price_Id").ToString()
                fnduom.Value = dtMaster.Rows(0)("UOM").ToString()
                sql = "SELECT [Price_code] as [Price Code] ,[Price_Code_Desc] as [Desc] ,[Remarks] as [Remarks] ,[Price_Comp_Code] as [Price Componant Code]  " & _
       ",[Price_Comp_Desc] as [Description]   ,isnull([Amount],0)+isnull([Discount_Percent],0) as [Amount]  " & _
       ",case Amount when 0 then 'Percentage' else  'Amount' end [Price Type] " & _
       " FROM [dbo].[TSPL_PRICE_COMPONENT_MAPPING] where Price_code = '" + fndPriceCode.Value + "' order by Price_Component_Map_Code Asc"
                Dim priceds As DataSet = connectSql.RunSQLReturnDS(sql)
                grdPriceComp.DataSource = priceds.Tables(0)
                txtPriceCodeDesc.Text = dtMaster.Rows(0)("Price_Code_Desc").ToString()
                For Each grow As GridViewRowInfo In grdPriceComp.Rows
                    grow.Cells("Amount").Value = Math.Round(clsCommon.myCdbl(dtMaster.Rows(0)("Price_Rate" + (grow.Index + 1).ToString()).ToString()), 5, MidpointRounding.ToEven)
                    grow.Cells("TotalAmt").Value = Math.Round(clsCommon.myCdbl(dtMaster.Rows(0)("Price_Amount" + (grow.Index + 1).ToString()).ToString()), 5, MidpointRounding.ToEven)
                    If grow.Index = 0 Then
                        'lblTradeMargin.Text = grow.Cells("TotalAmt").Value
                    End If
                Next
                fndTaxGrp.Value = dtMaster.Rows(0)("Tax_Group").ToString()
                sql = "Select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER WHERE Tax_group_Code='" + fndTaxGrp.Value + "'"
                txtTaxGrp.Text = connectSql.RunScalar(sql)
                'lblTotTaxOnPage.Text = 0
                Dim i As Integer
                grdTax.DataSource = Nothing
                grdTax.Rows.Clear()
                For i = 1 To 10
                    sql = "Select (case When Tax" + i.ToString() + " is NULL THEN '' else Tax" + i.ToString() + " end),Tax" + i.ToString() + "_Rate,Tax" + i.ToString() + "_Amt from tspl_item_price_master WHERE item_price_id='" & strPriceID & "'"
                    ds = connectSql.RunSQLReturnDS(sql)
                    If Not ds.Tables(0).Rows(0)(0).ToString() = "" Then
                        Dim taxCode As String = ds.Tables(0).Rows(0)(0).ToString()
                        Dim taxRate As Decimal = Math.Round(clsCommon.myCdbl(ds.Tables(0).Rows(0)(1).ToString()), 5, MidpointRounding.ToEven)
                        Dim taxAmt As Decimal = Math.Round(clsCommon.myCdbl(ds.Tables(0).Rows(0)(2).ToString()), 5, MidpointRounding.ToEven)
                        sql = "Select Taxable,Surtax,Surtax_Tax_Code From TSPL_TAX_GROUP_DETAILS WHERE Tax_Code='" + taxCode + "' and Tax_Group_Code='" + fndTaxGrp.Value + "'"
                        Dim dss As DataSet = connectSql.RunSQLReturnDS(sql)
                        Dim taxable As String = ""
                        Dim surtax As String = ""
                        Dim surtaxcode As String = ""
                        If dss.Tables(0).Rows.Count > 0 Then
                            taxable = dss.Tables(0).Rows(0)("Taxable").ToString()
                            surtax = dss.Tables(0).Rows(0)("Surtax").ToString()
                            surtaxcode = dss.Tables(0).Rows(0)("Surtax_Tax_Code").ToString()
                        End If
                        If Not ds.Tables(0).Rows(0)(0).ToString() = "" Then
                            sql = "Select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code='" + ds.Tables(0).Rows(0)(0).ToString() + "'"
                            Dim taxCodeDesc As String = connectSql.RunScalar(sql)
                            grdTax.Rows.Add(taxCode, taxCodeDesc, taxRate, taxAmt, taxable, surtax, surtaxcode)
                        End If
                    End If
                Next i
                Dim strQ As String = " select top 1  Excisable  from TSPL_TAX_MASTER where   Excisable='Y' and TSPL_TAX_MASTER.Tax_Code in " & _
                                     " (select L.Tax_Code from TSPL_TAX_GROUP_DETAILS L inner join TSPL_TAX_GROUP_MASTER M" & _
                                     " on L.Tax_Group_Code =M.Tax_Group_Code  where M.Tax_Group_Code ='" + fndTaxGrp.Value + "' " & _
                                     " and L.Tax_Group_Type ='S')"
                Dim CheckTaxType As String = connectSql.RunScalar(strQ)
                Dim cess As Decimal = 0
                'lblTotTaxOnPage.Text = 0
                For Each grow As GridViewRowInfo In grdTax.Rows
                    If grow.Index = 0 Then
                        If CheckTaxType = "Y" Then
                            'lblExiceDuty.Text = 0.0
                            'lblTotTaxOnPage.Text = 0.0
                            'lblSTax.Text = 0.0
                            'lblSTax1.Text = 0.0
                            'lblExiceDuty.Text = Math.Round(clsCommon.myCdbl(grow.Cells(3).Value), 5, MidpointRounding.ToEven)
                            'lblTotTaxOnPage.Text = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(lblTotTaxOnPage.Text) + clsCommon.myCdbl(grow.Cells(3).Value)), 5, MidpointRounding.ToEven)
                        Else
                            'lblSTax.Text = 0.0
                            'lblSTax1.Text = 0.0
                            'lblExiceDuty.Text = 0.0
                            'lblTotTaxOnPage.Text = 0.0
                            'lblSTax.Text = Math.Round(clsCommon.myCdbl(grow.Cells(3).Value), 5, MidpointRounding.ToEven)
                            'lblSTax1.Text = Math.Round(clsCommon.myCdbl(grow.Cells(3).Value), 5, MidpointRounding.ToEven)
                        End If
                    ElseIf grow.Index = grdTax.RowCount - 1 Then
                        'lblSTax.Text = 0.0
                        'lblSTax1.Text = 0.0
                        'lblSTax.Text = Math.Round(clsCommon.myCdbl(grow.Cells(3).Value), 5, MidpointRounding.ToEven)
                        'lblSTax1.Text = Math.Round(clsCommon.myCdbl(grow.Cells(3).Value), 5, MidpointRounding.ToEven)
                    Else
                        'lblTotTaxOnPage.Text = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(lblTotTaxOnPage.Text) + clsCommon.myCdbl(grow.Cells(3).Value)), 5, MidpointRounding.ToEven)
                        cess = Math.Round(clsCommon.myCdbl(cess + clsCommon.myCdbl(grow.Cells(3).Value)), 5, MidpointRounding.ToEven)
                    End If
                Next
                'lblMrp.Text = txtMRP.Text
                'RadLabel21.Text = ""
                'lblOtherMargin.Text = ""
                For j As Integer = 1 To grdPriceComp.Rows.Count - 1
                    Dim ch As String = grdPriceComp.Rows(j).Cells(1).Value
                    If CDec(grdPriceComp.Rows(j).Cells(4).Value) <> 0.0 Then
                        'RadLabel21.Text = RadLabel21.Text + Environment.NewLine + grdPriceComp.Rows(j).Cells(1).Value
                        'lblOtherMargin.Text = lblOtherMargin.Text + Environment.NewLine + grdPriceComp.Rows(j).Cells(4).Value
                    End If
                Next
                Try
                    EndDate = Convert.ToDateTime(dtMaster.Rows(0)("End Date"))
                Catch ex As Exception
                    EndDate = dtpStart.NullDate
                End Try
            End If
            PageMode = "Edit"
            txtMRP.ReadOnly = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtAbtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAbtRate.TextChanged
        txtAbtAmount.Text = clsCommon.myCstr(clsCommon.myCdbl(txtMRP.Text) * clsCommon.myCdbl(txtAbtRate.Text) / 100)
    End Sub

    Private Sub grdPriceComp_CellBeginEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs)
        If e.Row.Index = grdPriceComp.Rows.Count - 1 Then
            ' e.Cancel = True
        End If
    End Sub

    Private Sub exitmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Exportmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim query As String = "Select "
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub

    Public Sub print()
        If txtPriceId.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Please select the item code")
        Else
            Try
                Dim StrQuery As String = "SELECT i.Item_Code, ISNULL(i.Item_Basic_Net, 0) AS item_MRP, i.Price_Comp1, ISNULL(i.Price_Amount1, 0) AS price_Amount1, i.Price_Comp2,    ISNULL(i.Price_Amount2, 0) AS price_Amount2, i.Price_Comp3, ISNULL(i.Price_Amount3, 0) AS price_Amount3, i.Price_Comp4, ISNULL(i.Price_Amount4,   0) AS price_Amount4, i.Price_Comp5, ISNULL(i.Price_Amount5, 0) AS price_Amount5, ISNULL(i.Item_Basic_Price, 0) AS item_baisc_price,  ISNULL(i.TAX1_Amt, 0) AS tax1_amt, ISNULL(i.TAX2_Amt, 0) AS tax2_amt, ISNULL(i.TAX3_Amt, 0) AS tax3_amt, ISNULL(i.TAX4_Amt, 0) AS tax4_amt, m.Item_Desc, ISNULL(i.Item_Basic_Price, 0) + ISNULL(i.TAX1_Amt, 0) + ISNULL(i.TAX2_Amt, 0) + ISNULL(i.TAX3_Amt, 0) + ISNULL(i.TAX4_Amt, 0) + ISNULL(i.Price_Amount5, 0) AS Total, ISNULL(i.Item_Basic_Net, 0) - i.Price_Amount1 AS Retail_Price, ISNULL(i.Item_Basic_Net, 0) - (ISNULL(i.Price_Amount1, 0) + ISNULL(i.Price_Amount2, 0) + ISNULL(i.Price_Amount3, 0) + ISNULL(i.Price_Amount4, 0) + ISNULL(i.Price_Amount5, 0)) AS Net, i.Price_Amount5 AS TDT, i.Price_Comp_Desc1 AS desc1, i.Price_Comp_Desc2 AS desc2, i.Price_Comp_Desc3 AS desc3, i.Price_Comp_Desc4 AS desc4, i.Price_Comp_Desc5 AS desc5, CONVERT(VARCHAR(10), i.Start_Date, 103) AS start_date, '' AS From_date, '' AS To_date,i.UOM as UOM, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,(select Price_Amount1 from TSPL_ITEM_PRICE_MASTER where Price_Comp1 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT1,(select Price_Amount2 from TSPL_ITEM_PRICE_MASTER where Price_Comp2 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT2,(select Price_Amount3 from TSPL_ITEM_PRICE_MASTER where Price_Comp3 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT3,(select Price_Amount4 from TSPL_ITEM_PRICE_MASTER where Price_Comp4 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT4,(select Price_Amount5 from TSPL_ITEM_PRICE_MASTER where Price_Comp5 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT5,(select Price_Amount6 from TSPL_ITEM_PRICE_MASTER where Price_Comp6 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT6,(select Price_Amount7 from TSPL_ITEM_PRICE_MASTER where Price_Comp7 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT7,(select Price_Amount8 from TSPL_ITEM_PRICE_MASTER where Price_Comp8 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT8,(select Price_Amount9 from TSPL_ITEM_PRICE_MASTER where Price_Comp9 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT9,(select Price_Amount10 from TSPL_ITEM_PRICE_MASTER where Price_Comp10 ='TPT' and TSPL_ITEM_PRICE_MASTER.Item_Price_ID  =i.Item_Price_ID   ) as TPT10       FROM     TSPL_ITEM_PRICE_MASTER AS i INNER JOIN                      TSPL_ITEM_MASTER AS m ON i.Item_Code = m.Item_Code INNER JOIN TSPL_COMPANY_MASTER ON i.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code      WHERE i.item_code='" + txtPriceId.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "ItemPrice", "ItemPriceReport")
                frmCRV = Nothing
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message)
            End Try

        End If
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        Me.Close()
    End Sub

    Private Sub FrmPriceMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then
            'importFormatedExcel()
        End If
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            ResetScreen()
        End If
    End Sub



    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentRow IsNot Nothing Then
                Dim strPriceCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Price_ID").Value)
                If clsCommon.myLen(strPriceCode) > 0 Then
                    LoadDataForPriceCode(strPriceCode)
                    RadPageView1.SelectedPage = pvItemPrice
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtPriceId__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPriceId._MYValidating
        sql = "select count(*) from TSPL_ITEM_Price_MASTER where Item_Price_ID ='" + txtPriceId.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
        If no = 0 Then
            txtPriceId.MyReadOnly = False
        Else
            txtPriceId.MyReadOnly = True
        End If
        If txtPriceId.MyReadOnly OrElse isButtonClicked Then

            'sql = "Select Item_Price_ID as [Code], TSPL_ITEM_PRICE_MASTER.Item_Code+' - '+ TSPL_ITEM_MASTER.Item_Desc as [Item Code], UOM, Start_Date as [Start Date], Tax_group as [Tax Group], Price_Code as [Price Code], Item_Basic_Net as [Price] from  TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code"
            'txtPriceId.Value = clsCommon.ShowSelectForm("PricMasterID", sql, "Code", "", txtPriceId.Value, "", isButtonClicked)
            txtPriceId.Value = clsPriceMaster.getFinder("", txtPriceId.Value, isButtonClicked)
            LoadData(txtPriceId.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtPriceId__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtPriceId._MYNavigator
        Try
            sql = "select count(*) from TSPL_ITEM_Price_MASTER where Item_Price_ID ='" + txtPriceId.Value + "' "
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql)) <= 0 Then
                txtPriceId.MyReadOnly = False
            Else
                txtPriceId.MyReadOnly = True
            End If
            LoadData(txtPriceId.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FillItemWiseTaxDetail(ByVal strTaxGroupCode As String, ByVal tax_Type As String)
        Try
            IsLoadTaxData = True
            grdTax.DataSource = Nothing
            grdTax.Rows.Clear()
            sql = " SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code,max(TSPL_TAX_MASTER.Excisable) as  Excisable, MAX(TSPL_TAX_MASTER.Type) as TaxType" & _
                  " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN  TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code and R.Tax_Type='" + tax_Type + "' INNER JOIN TSPL_TAX_MASTER ON G.Tax_Code = TSPL_TAX_MASTER.Tax_Code " & _
                  "  WHERE (G.Tax_Group_Code = '" + strTaxGroupCode + "') AND (G.Tax_Group_Type = '" + tax_Type + "') " & _
                  "  GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
                  "  ORDER BY G.Trans_Code"
            dt = clsDBFuncationality.GetDataTable(sql)
            grdTax.DataSource = dt
            FillTaxAmounts()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadTaxData = False
        End Try
    End Sub


    Private Sub txtItemCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        sql = "Select Item_Code as Code, Item_Desc as Descriiption From TSPL_ITEM_MASTER"
        txtItemCode.Value = clsCommon.ShowSelectForm("ItemFinder@PriceMstr", sql, "Code", "", txtItemCode.Value, "", isButtonClicked)
        LoadItemData(txtItemCode.Value)
        LoadPriceData()
        grdTax.Columns("Tax Rate").ReadOnly = False

        cbgUOM.DataSource = Nothing
        If chkAuto.Checked Then
            LoadItemUOM(txtItemCode.Value, fnduom.Value)
        End If
        'sanjay
        'If CalculateTaxRatefromItemwsieTaxOnSale = True Then
        '    'Dim objTM As clsItemWiseTaxAuthority
        '    'objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(txtItemCode.Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "S")
        '    'If objTM IsNot Nothing Then
        '    '    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
        '    '    gv1.CurrentRow.Cells(colItemwiseTaxCode).Value = objTM.HCODE
        '    'End If
        '    Dim dt As DataTable
        '    Dim qry As String = "select IsTaxable from tspl_item_master where Item_Code='" + clsCommon.myCstr(txtItemCode.Value) + "'"
        '    Dim IsTaxable As Int16 = 0
        '    IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        '    If IsTaxable = 1 Then
        '        qry = "select top 1 * from  TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " & _
        '          "left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE " & _
        '          "and TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE  " & _
        '          "where Status=1 and TSPL_ITEM_WISE_TAX.Type= 'S' and DOC_DATE < ='" & clsCommon.GetPrintDate(dtpStart.Value, "dd/MMM/yyyy") & "' and Item_Code='" & clsCommon.myCstr(txtItemCode.Value) & "' order by DOC_DATE desc "

        '        dt = clsDBFuncationality.GetDataTable(qry)

        '        'If IsTaxable = 1 And (dt Is Nothing Or dt.Rows.Count = 0) Then
        '        '    common.clsCommon.MyMessageBoxShow("Create Item Wise Tax.", Me.Text, MessageBoxButtons.OK)
        '        '    Exit Sub
        '        'End If
        '        'If (dt Is Nothing Or dt.Rows.Count = 0) Then
        '        '    common.clsCommon.MyMessageBoxShow("Create Item Wise Tax.", Me.Text, MessageBoxButtons.OK)
        '        '    Exit Sub
        '        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '            fndTaxGrp.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Code"))
        '            txtTaxGrp.Text = clsTaxMaster.GetName(fndTaxGrp.Value)
        '            FillItemWiseTaxDetail(fndTaxGrp.Value, clsCommon.myCstr(dt.Rows(0)("Type")))
        '        Else
        '            common.clsCommon.MyMessageBoxShow("Create Item Wise Tax.", Me.Text, MessageBoxButtons.OK)
        '            txtItemCode.Value = ""
        '        End If
        '    End If
        'End If
        'sanjay

        'If chkAuto.Checked Then
        '    LoadItemUOM(txtItemCode.Value, fnduom.Value)
        'End If
        'CheckIsAutoCreate(txtItemCode.Value, fnduom.Value)
    End Sub

    Private Sub LoadItemData(ByVal strItemCode As String)
        lblItemDesc.Text = ""
        fnduom.Value = ""
        txtMRP.Text = ""
        txtMRP.Enabled = False
        dt = clsDBFuncationality.GetDataTable("Select Item_Desc, Unit_Code, Is_MRP from TSPL_ITEM_MASTER WHERE Item_Code='" & strItemCode & "'")
        If dt.Rows.Count > 0 Then
            lblItemDesc.Text = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            fnduom.Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            'CheckIsAutoCreate(txtItemCode.Value, fnduom.Value)
            ''richa 22 aug,2019 as discussed it with ranjana mam mrp should be enabled for all type of items
            'If clsCommon.myCdbl(dt.Rows(0)("Is_MRP")) = 1 Then
            '    txtMRP.Enabled = True
            '    Is_Mrp = True
            'Else
            '    txtMRP.Enabled = False
            '    Is_Mrp = False
            'End If
            'txtMRP.Enabled = True 
            Is_Mrp = True
        Else
        End If
    End Sub

    Private Sub fnduom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnduom._MYValidating
        sql = "Select Unit_Code as Code, Unit_Desc as Description from TSPL_UNIT_MASTER LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code"
        fnduom.Value = clsCommon.ShowSelectForm("UOMFND@PriceMaster", sql, "Code", "TSPL_ITEM_UOM_DETAIL.Item_Code='" + txtItemCode.Value + "'", fnduom.Value, "Code", isButtonClicked)
        'CheckIsAutoCreate(txtItemCode.Value, fnduom.Value)
        ''richa 13 Sep,2019 UDL/13/09/19-001000
        cbgUOM.DataSource = Nothing
        If chkAuto.Checked Then
            LoadItemUOM(txtItemCode.Value, fnduom.Value)
        End If

        Try
            If fndTaxGrp.Value.Trim <> "" Then
                'FillTaxAmounts(CDec(txtMRP.Text))
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
        LoadPriceData()
        'If chkAuto.Checked Then
        '    LoadItemUOM(txtItemCode.Value, fnduom.Value)
        'End If
    End Sub

    'Private Sub CheckIsAutoCreate(ByVal strItemCode As String, ByVal strUnitCode As String)
    '    Try
    '        If clsCommon.myLen(strItemCode) > 0 And clsCommon.myLen(strUnitCode) > 0 Then
    '            chkAuto.Enabled = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Case When Stocking_Unit='Y' Then 'True' Else 'False' End from TSPL_ITEM_UOM_DETAIL WHere Item_Code='" & strItemCode & "' AND Uom_Code='" & strUnitCode & "'"))
    '            If chkAuto.Enabled = False Then
    '                chkAuto.Checked = False
    '            End If
    '        Else
    '            chkAuto.Enabled = False
    '            chkAuto.Checked = False
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Private Sub fndPriceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        sql = "SELECT distinct Price_code as [Code] ,[Price_Code_Desc] as [Description] FROM [TSPL_PRICE_COMPONENT_MAPPING]"
        fndPriceCode.Value = clsCommon.ShowSelectForm("POPrice Master", sql, "Code", "", fndPriceCode.Value, "", isButtonClicked)
        LoadPriceData()
    End Sub
    Public Sub LoadPriceData()
        Try
            If fndPriceCode.Value.Trim <> "" Then
                FillPriceComponent(fndPriceCode.Value)
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub chkEnd_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkEnd.ToggleStateChanged
        dtpEnd.Enabled = chkEnd.Checked
    End Sub

    Private Sub chkNoMRP_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkNoMRP.ToggleStateChanged
        If chkNoMRP.Checked Then
            txtMRP.Text = txtBasic.Text
        Else
            txtMRP.Text = ""
        End If
    End Sub
    Private Sub cboItemType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlBasicRate.SelectedIndexChanged
        If clsCommon.CompairString(ddlBasicRate.Text, "Auto") = CompairStringResult.Equal Then
            txtBasic.Enabled = False
            ddlBasicRateOn.Enabled = True
        Else
            txtBasic.Enabled = True
            ddlBasicRateOn.Enabled = False
        End If
        UpdateAllTotal()
    End Sub

    Private Sub ddlBasicRateOn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlBasicRateOn.SelectedIndexChanged
        If clsCommon.CompairString(ddlBasicRateOn.Text, "Purchase Cost") = CompairStringResult.Equal Then
            txtPurchaseCost.Text = ""
            txtPurchaseCost.Enabled = True
        Else
            txtPurchaseCost.Text = ""
            txtPurchaseCost.Enabled = False
        End If
    End Sub

    Dim mrp As Double
    Dim landingCost As Double
    Dim markup As Double

    Private Sub txtMarkupPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMarkupPercent.TextChanged, txtMRP.TextChanged, ddlMarkup.SelectedIndexChanged, txtAbtRate.TextChanged, txtLandingCost.TextChanged, txtPurchaseCost.TextChanged
        If Not IsFormLoad Then
            If Not chkBackCalculation.Checked Then
                txtMRP.Enabled = True
                If clsCommon.CompairString(ddlBasicRate.Text, "Auto") = CompairStringResult.Equal Then
                    mrp = clsCommon.myCdbl(txtMRP.Text)
                    landingCost = clsCommon.myCdbl(txtLandingCost.Text)
                    markup = clsCommon.myCdbl(txtMarkupPercent.Text)
                    If clsCommon.CompairString(ddlMarkup.Text, "Landing Cost") = CompairStringResult.Equal Then
                        txtBasic.Text = clsCommon.myCstr((landingCost * markup / 100) + landingCost)
                        If clsCommon.CompairString(ddlBasicRateOn.Text, "Purchase Cost") = CompairStringResult.Equal Then
                            txtBasic.Text = clsCommon.myCstr((landingCost * markup / 100) + clsCommon.myCdbl(txtPurchaseCost.Text))
                        End If
                    ElseIf clsCommon.CompairString(ddlMarkup.Text, "MRP") = CompairStringResult.Equal Then
                        txtMRP.Enabled = True
                        txtBasic.Text = clsCommon.myCstr((mrp * markup / 100) + landingCost)
                        If clsCommon.CompairString(ddlBasicRateOn.Text, "Purchase Cost") = CompairStringResult.Equal Then
                            txtBasic.Text = clsCommon.myCstr((mrp * markup / 100) + clsCommon.myCdbl(txtPurchaseCost.Text))
                        End If
                    Else

                    End If
                End If
                txtAbtAmount.Text = clsCommon.myCdbl(txtMRP.Text) * clsCommon.myCdbl(txtAbtRate.Text) / 100
            End If
            txtAbtAmount.Value = txtMRP.Value * txtAbtRate.Value / 100
        End If
        UpdateAllTotal()
        FillTaxAmounts()
    End Sub

    Private Sub grdPriceComp_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdPriceComp.CellValueChanged
        Dim Amt As Double = 0
        If Not IsInsideLoadData Then
            If e.Column Is grdPriceComp.Columns("Amount") Then
                If clsCommon.CompairString(grdPriceComp.CurrentRow.Cells("Price Type").Value, "Percentage") = CompairStringResult.Equal Then
                    grdPriceComp.CurrentRow.Cells("TotalAmt").Value = Math.Round(clsCommon.myCdbl(txtMRP.Text) * clsCommon.myCdbl(grdPriceComp.CurrentRow.Cells("Amount").Value) / 100, 2)
                Else
                    grdPriceComp.CurrentRow.Cells("TotalAmt").Value = clsCommon.myCdbl(grdPriceComp.CurrentRow.Cells("Amount").Value)
                End If
                'UpdateAllTotal()
                FillTaxAmounts()
            End If
        End If
    End Sub

    Private Function CalculateComponentAmt() As Decimal
        TotalPriceCompAmt = 0
        Try
            For Each grow As GridViewRowInfo In grdPriceComp.Rows
                TotalPriceCompAmt += clsCommon.myCdbl(grow.Cells("TotalAmt").Value)
            Next
            lblPriceCompTotal.Text = clsCommon.myCstr(TotalPriceCompAmt)
            Return clsCommon.myCdbl(txtMRP.Text) - clsCommon.myCdbl(TotalPriceCompAmt)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function CalculateTotalTAXAmt() As Decimal
        TotalTaxAmt = 0
        Try
            For Each grow As GridViewRowInfo In grdTax.Rows
                TotalTaxAmt += clsCommon.myCdbl(grow.Cells("Amount").Value)
            Next
            lbllTotalTax.Text = clsCommon.myCstr(TotalTaxAmt)
            Return TotalTaxAmt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Private Sub grdTax_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles grdTax.EditorRequired
        If TypeOf grdTax.CurrentColumn Is GridViewComboBoxColumn Then
            Dim coltaxrate As GridViewComboBoxColumn = TryCast(grdTax.Columns("Tax Rate"), GridViewComboBoxColumn)
            sql = "select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Code='" + grdTax.CurrentRow.Cells("Tax Code").Value + "' AND Tax_Type='S'"
            ds = connectSql.RunSQLReturnDS(sql)
            coltaxrate.ValueMember = "Tax_Rate"
            coltaxrate.DataSource = ds.Tables(0)
        End If
    End Sub

    Private Sub MasterTemplate_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdTax.CellValueChanged
        If Not IsLoadTaxData Then
            If e.Column Is grdTax.Columns("Tax Rate") Then
                FillTaxAmounts()
            End If
        End If
    End Sub

    Private Sub UpdateAllTotal()
        Try
            If chkBackCalculation.Checked Then
                txtBasic.Text = clsCommon.myCstr(CalculateComponentAmt())
            End If
            lbllAbtAmt.Text = clsCommon.myCdbl(txtAbtAmount.Text)
            lbllAbtRate.Text = clsCommon.myCdbl(txtAbtRate.Text)
            lbllBasicRate.Text = clsCommon.myCdbl(txtBasic.Text)
            lbllLandingCost.Text = clsCommon.myCdbl(txtLandingCost.Text)
            lbllMarkupPercent.Text = clsCommon.myCdbl(txtMarkupPercent.Text)
            lbllMRP.Text = clsCommon.myCdbl(txtMRP.Text)
            CalculateTotalTAXAmt()
            If chkBackCalculation.Checked Then
                ' If chkBaackCalWOutTax.IsChecked Then
                If chkWithoutTax.Checked Then
                    lbllSellingPrice.Text = clsCommon.myCdbl(txtBasic.Text)
                    ''richa agarwal 13 Sep,2019 UDL/13/09/19-001000
                    txtTotalItemPrice.Text = Math.Round(clsCommon.myCdbl(txtBasic.Text) + clsCommon.myCdbl(lbllTotalTax.Text), 5)
                Else
                    lbllSellingPrice.Text = Math.Round(clsCommon.myCdbl(txtBasic.Text) - clsCommon.myCdbl(lbllTotalTax.Text), 5)
                    ''richa agarwal 13 Sep,2019 UDL/13/09/19-001000
                    txtTotalItemPrice.Text = clsCommon.myCdbl(txtBasic.Text)
                End If
            Else
                lbllSellingPrice.Text = Math.Round(clsCommon.myCdbl(txtBasic.Text) + clsCommon.myCdbl(lbllTotalTax.Text), 5)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub MyComboBox1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlTaxMnpln.SelectedIndexChanged
        Try
            If Not IsFormLoad Then
                FillTaxAmounts()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        'FrmPriceMasterExport.ShowDialog()
        'Try
        '    sql = "Select TSPL_ITEM_PRICE_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, UOM, Remarks, TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type, TSPL_ITEM_PRICE_MASTER.Basic_Price_on, Landing_Cost, Purchase_Cost, TSPL_ITEM_PRICE_MASTER.Item_MRP, Item_Basic_Price, Item_Basic_Net, Markup_On, Markup_Percent, Tax_Manipulation_On, Start_Date, End_Date, Tax_group, TAX1, TAX1_Rate, TAX1_Amt, TAX2, TAX2_Rate, TAX2_Amt, TAX3, TAX3_Rate, TAX3_Amt, TAX4, TAX4_Rate, TAX4_Amt, TAX5, TAX5_Rate, TAX5_Amt, TAX6, TAX6_Rate, TAX6_Amt, TAX7, TAX7_Rate, TAX7_Amt, TAX8, TAX8_Rate, TAX8_Amt, TAX9, TAX9_Rate, TAX9_Amt, TAX10, TAX10_Rate, TAX10_Amt, Price_Code, Abatement_Rate, Abatement  from TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code"
        '    transportSql.ExporttoExcel(sql, Me)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    'Private Sub rmiLandingCostCST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiLandingCostCST.Click
    '    LandingCostWithCST()
    'End Sub

    'Private Sub rmiLandingCostVAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiLandingCostVAT.Click
    '    LandingCostWithVAT()
    'End Sub

    'Private Sub rmiMrpCST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiMrpCST.Click
    '    MRPWithCST()
    'End Sub

    'Private Sub rmiMrpVAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiMrpVAT.Click
    '    MRPWithVAT()
    'End Sub

    Private Sub rmiMRPWithBackCalculation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiMRPWithBackCalculation.Click
        MRPWithBackCalculation()
    End Sub

    Private Sub LandingCostWithCST()
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Item_Code", "Item_Desc", "UOM", "Conversion Factor", "Remarks", "Item_Basic_Price_Type", "Basic_Price_on", "Landing_Cost", "Purchase_Cost", "Item_MRP", "Item_Basic_Price", "Item_Basic_Net", "Markup_On", "Markup_Percent", "Tax_Manipulation_On", "Start_Date", "End_Date", "Tax_group", "TAX1", "TAX1_Rate", "TAX1_Amt", "TAX2", "TAX2_Rate", "TAX2_Amt", "TAX3", "TAX3_Rate", "TAX3_Amt", "TAX4", "TAX4_Rate", "TAX4_Amt", "TAX5", "TAX5_Rate", "TAX5_Amt", "TAX6", "TAX6_Rate", "TAX6_Amt", "TAX7", "TAX7_Rate", "TAX7_Amt", "TAX8", "TAX8_Rate", "TAX8_Amt", "TAX9", "TAX9_Rate", "TAX9_Amt", "TAX10", "TAX10_Rate", "TAX10_Amt", "Price_Code", "Abatement_Rate", "Abatement", "Landing Cost-CST", "ITF_CODE") Then
            Dim LineNo As String
            Dim ii As Integer = 0
            Try
                ' clsCommon.ProgressBarShow()
                clsCommon.ProgressBarPercentShow()
                Dim Arr As New List(Of clsPriceMaster)
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    LineNo = clsCommon.myCstr(dgrv.Index + 2)
                    ii += 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100) / dgv.RowCount - 1, "Importing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(dgv.RowCount - 1))
                    Dim obj As New clsPriceMaster()
                    obj.Item_Code = clsCommon.myCstr(dgrv.Cells("Item_Code").Value)
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        obj.Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code From TSPL_ITem_MASTER WHERE Item_Code='" + obj.Item_Code + "'")
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Throw New Exception("Line " + LineNo + " : Item Code does not exist.")
                        End If

                        obj.UOM = clsCommon.myCstr(dgrv.Cells("UOM").Value)
                        If clsCommon.myLen(obj.UOM) > 0 Then
                            obj.UOM = clsDBFuncationality.getSingleValue("Select UOM_Code from TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + obj.Item_Code + "' AND UOM_Code='" + obj.UOM + "'")
                            If clsCommon.myLen(obj.UOM) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : UOM does not exist for this item.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter UOM.")
                        End If

                        obj.Remarks = clsCommon.myCstr(dgrv.Cells("Remarks").Value)
                        If clsCommon.myLen(obj.Remarks) > 200 Then
                            Throw New Exception("Line " + LineNo + " : Remarks can not be greator than 200.")
                        End If

                        obj.Item_Basic_Price_Type = clsCommon.myCstr(dgrv.Cells("Item_Basic_Price_Type").Value)
                        If Not (clsCommon.CompairString(obj.Item_Basic_Price_Type, "Auto") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Item_Basic_Price_Type, "Mannual") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Item_Basic_Price_Type can be like 'Auto' or 'Mannual'.")
                        End If

                        obj.Basic_Price_On = clsCommon.myCstr(dgrv.Cells("Basic_Price_On").Value)
                        If Not (clsCommon.CompairString(obj.Basic_Price_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Basic_Price_On, "Purchase Cost") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Basic_Price_On can be like 'Landing Cost' or 'Purchase Cost'.")
                        End If
                        obj.Landing_Cost = clsCommon.myCdbl(dgrv.Cells("Landing_Cost").Value)
                        obj.Purchase_Cost = clsCommon.myCdbl(dgrv.Cells("Purchase_Cost").Value)
                        obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value)
                        obj.Item_Basic_Price = clsCommon.myCdbl(dgrv.Cells("Item_Basic_Price").Value)
                        obj.Item_Basic_Net = clsCommon.myCdbl(dgrv.Cells("Item_Basic_Net").Value)
                        obj.Markup_On = clsCommon.myCstr(dgrv.Cells("Markup_On").Value)

                        If Not (clsCommon.CompairString(obj.Markup_On, "None") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Markup_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Markup_On, "MRP") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Markup_On can be like 'None' or 'Landing Cost' or 'MRP'.")
                        End If
                        obj.Markup_Percent = clsCommon.myCdbl(dgrv.Cells("Markup_Percent").Value)
                        obj.Tax_Manipulation_On = clsCommon.myCstr(dgrv.Cells("Tax_Manipulation_On").Value)
                        If Not (clsCommon.CompairString(obj.Tax_Manipulation_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Tax_Manipulation_On, "Basic Price") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Tax Manipulation can be of type 'Landing Cost' or 'Basic Price'.")
                        End If
                        obj.Start_Date = clsCommon.myCstr(dgrv.Cells("Start_Date").Value)
                        If clsCommon.myLen(dgrv.Cells("End_Date").Value) Then
                            obj.End_Date = clsCommon.myCstr(dgrv.Cells("End_Date").Value)
                        End If
                        obj.Tax_group = clsCommon.myCstr(dgrv.Cells("Tax_group").Value)
                        If clsCommon.myLen(obj.Tax_group) > 0 Then
                            obj.Tax_group = clsDBFuncationality.getSingleValue("Select Tax_Group_Code from TSPL_TAX_GROUP_MASTER WHERE Tax_Group_Code='" + obj.Tax_group + "'")
                            If clsCommon.myLen(obj.Tax_group) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Tax Group does not exist.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Tax Group.")
                        End If
                        obj.TAX1 = clsCommon.myCstr(dgrv.Cells("TAX1").Value)
                        If clsCommon.myLen(obj.TAX1) > 0 Then
                            obj.TAX1 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX1 + "'")
                            If clsCommon.myLen(obj.TAX1) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Tax1 does not exist.")
                            End If
                            obj.TAX1_Rate = clsCommon.myCdbl(dgrv.Cells("TAX1_Rate").Value)
                            obj.TAX1_Amt = clsCommon.myCdbl(dgrv.Cells("TAX1_Amt").Value)
                            obj.TAX2 = clsCommon.myCstr(dgrv.Cells("TAX2").Value)
                            If clsCommon.myLen(obj.TAX2) > 0 Then
                                obj.TAX2 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX2 + "'")
                                If clsCommon.myLen(obj.TAX2) <= 0 Then
                                    Throw New Exception("Line " + LineNo + " : Tax2 does not exist.")
                                End If
                                obj.TAX2_Rate = clsCommon.myCdbl(dgrv.Cells("TAX2_Rate").Value)
                                obj.TAX2_Amt = clsCommon.myCdbl(dgrv.Cells("TAX2_Amt").Value)
                                obj.TAX3 = clsCommon.myCstr(dgrv.Cells("TAX3").Value)
                                If clsCommon.myLen(obj.TAX3) > 0 Then
                                    obj.TAX3 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX3 + "'")
                                    If clsCommon.myLen(obj.TAX3) <= 0 Then
                                        Throw New Exception("Line " + LineNo + " : Tax3 does not exist.")
                                    End If
                                    obj.TAX3_Rate = clsCommon.myCdbl(dgrv.Cells("TAX3_Rate").Value)
                                    obj.TAX3_Amt = clsCommon.myCdbl(dgrv.Cells("TAX3_Amt").Value)
                                    obj.TAX4 = clsCommon.myCstr(dgrv.Cells("TAX4").Value)
                                    If clsCommon.myLen(obj.TAX4) > 0 Then
                                        obj.TAX4 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX4 + "'")
                                        If clsCommon.myLen(obj.TAX4) <= 0 Then
                                            Throw New Exception("Line " + LineNo + " : Tax4 does not exist.")
                                        End If
                                        obj.TAX4_Rate = clsCommon.myCdbl(dgrv.Cells("TAX4_Rate").Value)
                                        obj.TAX4_Amt = clsCommon.myCdbl(dgrv.Cells("TAX4_Amt").Value)
                                        obj.TAX5 = clsCommon.myCstr(dgrv.Cells("TAX5").Value)
                                        If clsCommon.myLen(obj.TAX5) > 0 Then
                                            obj.TAX5 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX5 + "'")
                                            If clsCommon.myLen(obj.TAX5) <= 0 Then
                                                Throw New Exception("Line " + LineNo + " : Tax5 does not exist.")
                                            End If
                                            obj.TAX5_Rate = clsCommon.myCdbl(dgrv.Cells("TAX5_Rate").Value)
                                            obj.TAX5_Amt = clsCommon.myCdbl(dgrv.Cells("TAX5_Amt").Value)
                                            obj.TAX6 = clsCommon.myCstr(dgrv.Cells("TAX6").Value)
                                            If clsCommon.myLen(obj.TAX6) > 0 Then
                                                obj.TAX6 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX6 + "'")
                                                If clsCommon.myLen(obj.TAX6) <= 0 Then
                                                    Throw New Exception("Line " + LineNo + " : Tax6 does not exist.")
                                                End If
                                                obj.TAX6_Rate = clsCommon.myCdbl(dgrv.Cells("TAX6_Rate").Value)
                                                obj.TAX6_Amt = clsCommon.myCdbl(dgrv.Cells("TAX6_Amt").Value)
                                                obj.TAX7 = clsCommon.myCstr(dgrv.Cells("TAX7").Value)
                                                If clsCommon.myLen(obj.TAX7) > 0 Then
                                                    obj.TAX7 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX7 + "'")
                                                    If clsCommon.myLen(obj.TAX7) <= 0 Then
                                                        Throw New Exception("Line " + LineNo + " : Tax7 does not exist.")
                                                    End If
                                                    obj.TAX7_Rate = clsCommon.myCdbl(dgrv.Cells("TAX7_Rate").Value)
                                                    obj.TAX7_Amt = clsCommon.myCdbl(dgrv.Cells("TAX7_Amt").Value)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Tax1.")
                        End If
                        obj.Price_Code = clsCommon.myCstr(dgrv.Cells("Price_Code").Value)
                        If clsCommon.myLen(obj.Price_Code) > 0 Then
                            obj.Price_Code = clsDBFuncationality.getSingleValue("Select MAX(Price_Code)  from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" + obj.Price_Code + "'")
                            If clsCommon.myLen(obj.Price_Code) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Price Code does not exist.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Price Code.")
                        End If
                        obj.Abatement_Rate = clsCommon.myCdbl(dgrv.Cells("Abatement_Rate").Value)
                        obj.Abatement = clsCommon.myCdbl(dgrv.Cells("Abatement").Value)
                        obj.IsNewEntry = True
                        Arr.Add(obj)
                    End If
                Next
                If (clsPriceMaster.SaveData(Arr, False)) Then
                    clsCommon.ProgressBarPercentHide()

                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub LandingCostWithVAT()
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Item_Code", "Item_Desc", "UOM", "Conversion Factor", "Remarks", "Item_Basic_Price_Type", "Basic_Price_on", "Landing_Cost", "Purchase_Cost", "Item_MRP", "Item_Basic_Price", "Item_Basic_Net", "Markup_On", "Markup_Percent", "Tax_Manipulation_On", "Start_Date", "End_Date", "Tax_group", "TAX1", "TAX1_Rate", "TAX1_Amt", "TAX2", "TAX2_Rate", "TAX2_Amt", "TAX3", "TAX3_Rate", "TAX3_Amt", "TAX4", "TAX4_Rate", "TAX4_Amt", "TAX5", "TAX5_Rate", "TAX5_Amt", "TAX6", "TAX6_Rate", "TAX6_Amt", "TAX7", "TAX7_Rate", "TAX7_Amt", "TAX8", "TAX8_Rate", "TAX8_Amt", "TAX9", "TAX9_Rate", "TAX9_Amt", "TAX10", "TAX10_Rate", "TAX10_Amt", "Price_Code", "Abatement_Rate", "Abatement", "Landing Cost-VAT", "ITF_CODE") Then
            Dim LineNo As String
            Dim ii As Integer = 0
            Try
                clsCommon.ProgressBarPercentShow()
                Dim Arr As New List(Of clsPriceMaster)
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    LineNo = clsCommon.myCstr(dgrv.Index + 2)
                    ii += 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100) / dgv.RowCount - 1, "Importing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(dgv.RowCount - 1))
                    Dim obj As New clsPriceMaster()
                    obj.Item_Code = clsCommon.myCstr(dgrv.Cells("Item_Code").Value)
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        obj.Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code From TSPL_ITem_MASTER WHERE Item_Code='" + obj.Item_Code + "'")
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Throw New Exception("Line " + LineNo + " : Item Code does not exist.")
                        End If

                        obj.UOM = clsCommon.myCstr(dgrv.Cells("UOM").Value)
                        If clsCommon.myLen(obj.UOM) > 0 Then
                            obj.UOM = clsDBFuncationality.getSingleValue("Select UOM_Code from TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + obj.Item_Code + "' AND UOM_Code='" + obj.UOM + "'")
                            If clsCommon.myLen(obj.UOM) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : UOM does not exist for this item.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter UOM.")
                        End If

                        obj.Remarks = clsCommon.myCstr(dgrv.Cells("Remarks").Value)
                        If clsCommon.myLen(obj.Remarks) > 200 Then
                            Throw New Exception("Line " + LineNo + " : Remarks can not be greator than 200.")
                        End If

                        obj.Item_Basic_Price_Type = clsCommon.myCstr(dgrv.Cells("Item_Basic_Price_Type").Value)
                        If Not (clsCommon.CompairString(obj.Item_Basic_Price_Type, "Auto") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Item_Basic_Price_Type, "Mannual") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Item_Basic_Price_Type can be like 'Auto' or 'Mannual'.")
                        End If

                        obj.Basic_Price_On = clsCommon.myCstr(dgrv.Cells("Basic_Price_On").Value)
                        If Not (clsCommon.CompairString(obj.Basic_Price_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Basic_Price_On, "Purchase Cost") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Basic_Price_On can be like 'Landing Cost' or 'Purchase Cost'.")
                        End If
                        obj.Landing_Cost = clsCommon.myCdbl(dgrv.Cells("Landing_Cost").Value)
                        obj.Purchase_Cost = clsCommon.myCdbl(dgrv.Cells("Purchase_Cost").Value)
                        obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value)
                        obj.Item_Basic_Price = clsCommon.myCdbl(dgrv.Cells("Item_Basic_Price").Value)
                        obj.Item_Basic_Net = clsCommon.myCdbl(dgrv.Cells("Item_Basic_Net").Value)
                        obj.Markup_On = clsCommon.myCstr(dgrv.Cells("Markup_On").Value)
                        If Not (clsCommon.CompairString(obj.Markup_On, "None") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Markup_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Markup_On, "MRP") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Markup_On can be like 'None' or 'Landing Cost' or 'MRP'.")
                        End If
                        obj.Markup_Percent = clsCommon.myCdbl(dgrv.Cells("Markup_Percent").Value)
                        obj.Tax_Manipulation_On = clsCommon.myCstr(dgrv.Cells("Tax_Manipulation_On").Value)
                        If Not (clsCommon.CompairString(obj.Tax_Manipulation_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Tax_Manipulation_On, "Basic Price") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Tax Manipulation can be of type 'Landing Cost' or 'Basic Price'.")
                        End If
                        obj.Start_Date = clsCommon.myCstr(dgrv.Cells("Start_Date").Value)
                        If clsCommon.myLen(dgrv.Cells("End_Date").Value) Then
                            obj.End_Date = clsCommon.myCstr(dgrv.Cells("End_Date").Value)
                        End If
                        obj.Tax_group = clsCommon.myCstr(dgrv.Cells("Tax_group").Value)
                        If clsCommon.myLen(obj.Tax_group) > 0 Then
                            obj.Tax_group = clsDBFuncationality.getSingleValue("Select Tax_Group_Code from TSPL_TAX_GROUP_MASTER WHERE Tax_Group_Code='" + obj.Tax_group + "'")
                            If clsCommon.myLen(obj.Tax_group) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Tax Group does not exist.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Tax Group.")
                        End If
                        obj.TAX1 = clsCommon.myCstr(dgrv.Cells("TAX1").Value)
                        If clsCommon.myLen(obj.TAX1) > 0 Then
                            obj.TAX1 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX1 + "'")
                            If clsCommon.myLen(obj.TAX1) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Tax1 does not exist.")
                            End If
                            obj.TAX1_Rate = clsCommon.myCdbl(dgrv.Cells("TAX1_Rate").Value)
                            obj.TAX1_Amt = clsCommon.myCdbl(dgrv.Cells("TAX1_Amt").Value)
                            obj.TAX2 = clsCommon.myCstr(dgrv.Cells("TAX2").Value)
                            If clsCommon.myLen(obj.TAX2) > 0 Then
                                obj.TAX2 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX2 + "'")
                                If clsCommon.myLen(obj.TAX2) <= 0 Then
                                    Throw New Exception("Line " + LineNo + " : Tax2 does not exist.")
                                End If
                                obj.TAX2_Rate = clsCommon.myCdbl(dgrv.Cells("TAX2_Rate").Value)
                                obj.TAX2_Amt = clsCommon.myCdbl(dgrv.Cells("TAX2_Amt").Value)
                                obj.TAX3 = clsCommon.myCstr(dgrv.Cells("TAX3").Value)
                                If clsCommon.myLen(obj.TAX3) > 0 Then
                                    obj.TAX3 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX3 + "'")
                                    If clsCommon.myLen(obj.TAX3) <= 0 Then
                                        Throw New Exception("Line " + LineNo + " : Tax3 does not exist.")
                                    End If
                                    obj.TAX3_Rate = clsCommon.myCdbl(dgrv.Cells("TAX3_Rate").Value)
                                    obj.TAX3_Amt = clsCommon.myCdbl(dgrv.Cells("TAX3_Amt").Value)
                                    obj.TAX4 = clsCommon.myCstr(dgrv.Cells("TAX4").Value)
                                    If clsCommon.myLen(obj.TAX4) > 0 Then
                                        obj.TAX4 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX4 + "'")
                                        If clsCommon.myLen(obj.TAX4) <= 0 Then
                                            Throw New Exception("Line " + LineNo + " : Tax4 does not exist.")
                                        End If
                                        obj.TAX4_Rate = clsCommon.myCdbl(dgrv.Cells("TAX4_Rate").Value)
                                        obj.TAX4_Amt = clsCommon.myCdbl(dgrv.Cells("TAX4_Amt").Value)
                                        obj.TAX5 = clsCommon.myCstr(dgrv.Cells("TAX5").Value)
                                        If clsCommon.myLen(obj.TAX5) > 0 Then
                                            obj.TAX5 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX5 + "'")
                                            If clsCommon.myLen(obj.TAX5) <= 0 Then
                                                Throw New Exception("Line " + LineNo + " : Tax5 does not exist.")
                                            End If
                                            obj.TAX5_Rate = clsCommon.myCdbl(dgrv.Cells("TAX5_Rate").Value)
                                            obj.TAX5_Amt = clsCommon.myCdbl(dgrv.Cells("TAX5_Amt").Value)
                                            obj.TAX6 = clsCommon.myCstr(dgrv.Cells("TAX6").Value)
                                            If clsCommon.myLen(obj.TAX6) > 0 Then
                                                obj.TAX6 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX6 + "'")
                                                If clsCommon.myLen(obj.TAX6) <= 0 Then
                                                    Throw New Exception("Line " + LineNo + " : Tax6 does not exist.")
                                                End If
                                                obj.TAX6_Rate = clsCommon.myCdbl(dgrv.Cells("TAX6_Rate").Value)
                                                obj.TAX6_Amt = clsCommon.myCdbl(dgrv.Cells("TAX6_Amt").Value)
                                                obj.TAX7 = clsCommon.myCstr(dgrv.Cells("TAX7").Value)
                                                If clsCommon.myLen(obj.TAX7) > 0 Then
                                                    obj.TAX7 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX7 + "'")
                                                    If clsCommon.myLen(obj.TAX7) <= 0 Then
                                                        Throw New Exception("Line " + LineNo + " : Tax7 does not exist.")
                                                    End If
                                                    obj.TAX7_Rate = clsCommon.myCdbl(dgrv.Cells("TAX7_Rate").Value)
                                                    obj.TAX7_Amt = clsCommon.myCdbl(dgrv.Cells("TAX7_Amt").Value)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Tax1.")
                        End If
                        obj.Price_Code = clsCommon.myCstr(dgrv.Cells("Price_Code").Value)
                        If clsCommon.myLen(obj.Price_Code) > 0 Then
                            obj.Price_Code = clsDBFuncationality.getSingleValue("Select MAX(Price_Code)  from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" + obj.Price_Code + "'")
                            If clsCommon.myLen(obj.Price_Code) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Price Code does not exist.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Price Code.")
                        End If
                        obj.Abatement_Rate = clsCommon.myCdbl(dgrv.Cells("Abatement_Rate").Value)
                        obj.Abatement = clsCommon.myCdbl(dgrv.Cells("Abatement").Value)
                        obj.IsNewEntry = True
                        Arr.Add(obj)
                    End If
                Next
                If (clsPriceMaster.SaveData(Arr, False)) Then
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub MRPWithCST()
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Item_Code", "Item_Desc", "UOM", "Conversion Factor", "Remarks", "Item_Basic_Price_Type", "Basic_Price_on", "Landing_Cost", "Purchase_Cost", "Item_MRP", "Item_Basic_Price", "Item_Basic_Net", "Markup_On", "Markup_Percent", "Tax_Manipulation_On", "Start_Date", "End_Date", "Tax_group", "TAX1", "TAX1_Rate", "TAX1_Amt", "TAX2", "TAX2_Rate", "TAX2_Amt", "TAX3", "TAX3_Rate", "TAX3_Amt", "TAX4", "TAX4_Rate", "TAX4_Amt", "TAX5", "TAX5_Rate", "TAX5_Amt", "TAX6", "TAX6_Rate", "TAX6_Amt", "TAX7", "TAX7_Rate", "TAX7_Amt", "TAX8", "TAX8_Rate", "TAX8_Amt", "TAX9", "TAX9_Rate", "TAX9_Amt", "TAX10", "TAX10_Rate", "TAX10_Amt", "Price_Code", "Abatement_Rate", "Abatement", "MRP-CST", "ITF_CODE") Then
            Dim LineNo As String
            Dim ii As Integer = 0
            Try
                clsCommon.ProgressBarPercentShow()
                Dim Arr As New List(Of clsPriceMaster)
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    LineNo = clsCommon.myCstr(dgrv.Index + 2)
                    ii += 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100) / dgv.RowCount - 1, "Importing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(dgv.RowCount - 1))
                    Dim obj As New clsPriceMaster()
                    obj.Item_Code = clsCommon.myCstr(dgrv.Cells("Item_Code").Value)
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        obj.Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code From TSPL_ITem_MASTER WHERE Item_Code='" + obj.Item_Code + "'")
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Throw New Exception("Line " + LineNo + " : Item Code does not exist.")
                        End If

                        obj.UOM = clsCommon.myCstr(dgrv.Cells("UOM").Value)
                        If clsCommon.myLen(obj.UOM) > 0 Then
                            obj.UOM = clsDBFuncationality.getSingleValue("Select UOM_Code from TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + obj.Item_Code + "' AND UOM_Code='" + obj.UOM + "'")
                            If clsCommon.myLen(obj.UOM) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : UOM does not exist for this item.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter UOM.")
                        End If

                        obj.Remarks = clsCommon.myCstr(dgrv.Cells("Remarks").Value)
                        If clsCommon.myLen(obj.Remarks) > 200 Then
                            Throw New Exception("Line " + LineNo + " : Remarks can not be greator than 200.")
                        End If

                        obj.Item_Basic_Price_Type = clsCommon.myCstr(dgrv.Cells("Item_Basic_Price_Type").Value)
                        If Not (clsCommon.CompairString(obj.Item_Basic_Price_Type, "Auto") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Item_Basic_Price_Type, "Mannual") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Item_Basic_Price_Type can be like 'Auto' or 'Mannual'.")
                        End If

                        obj.Basic_Price_On = clsCommon.myCstr(dgrv.Cells("Basic_Price_On").Value)
                        If Not (clsCommon.CompairString(obj.Basic_Price_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Basic_Price_On, "Purchase Cost") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Basic_Price_On can be like 'Landing Cost' or 'Purchase Cost'.")
                        End If
                        obj.Landing_Cost = clsCommon.myCdbl(dgrv.Cells("Landing_Cost").Value)
                        obj.Purchase_Cost = clsCommon.myCdbl(dgrv.Cells("Purchase_Cost").Value)
                        obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value)
                        obj.Item_Basic_Price = clsCommon.myCdbl(dgrv.Cells("Item_Basic_Price").Value)
                        obj.Item_Basic_Net = clsCommon.myCdbl(dgrv.Cells("Item_Basic_Net").Value)
                        obj.Markup_On = clsCommon.myCstr(dgrv.Cells("Markup_On").Value)
                        If Not (clsCommon.CompairString(obj.Markup_On, "None") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Markup_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Markup_On, "MRP") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Markup_On can be like 'None' or 'Landing Cost' or 'MRP'.")
                        End If
                        obj.Markup_Percent = clsCommon.myCdbl(dgrv.Cells("Markup_Percent").Value)
                        obj.Tax_Manipulation_On = clsCommon.myCstr(dgrv.Cells("Tax_Manipulation_On").Value)
                        If Not (clsCommon.CompairString(obj.Tax_Manipulation_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Tax_Manipulation_On, "Basic Price") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Tax Manipulation can be of type 'Landing Cost' or 'Basic Price'.")
                        End If
                        obj.Start_Date = clsCommon.myCstr(dgrv.Cells("Start_Date").Value)
                        If clsCommon.myLen(dgrv.Cells("End_Date").Value) Then
                            obj.End_Date = clsCommon.myCstr(dgrv.Cells("End_Date").Value)
                        End If
                        obj.Tax_group = clsCommon.myCstr(dgrv.Cells("Tax_group").Value)
                        If clsCommon.myLen(obj.Tax_group) > 0 Then
                            obj.Tax_group = clsDBFuncationality.getSingleValue("Select Tax_Group_Code from TSPL_TAX_GROUP_MASTER WHERE Tax_Group_Code='" + obj.Tax_group + "'")
                            If clsCommon.myLen(obj.Tax_group) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Tax Group does not exist.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Tax Group.")
                        End If
                        obj.TAX1 = clsCommon.myCstr(dgrv.Cells("TAX1").Value)
                        If clsCommon.myLen(obj.TAX1) > 0 Then
                            obj.TAX1 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX1 + "'")
                            If clsCommon.myLen(obj.TAX1) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Tax1 does not exist.")
                            End If
                            obj.TAX1_Rate = clsCommon.myCdbl(dgrv.Cells("TAX1_Rate").Value)
                            obj.TAX1_Amt = clsCommon.myCdbl(dgrv.Cells("TAX1_Amt").Value)
                            obj.TAX2 = clsCommon.myCstr(dgrv.Cells("TAX2").Value)
                            If clsCommon.myLen(obj.TAX2) > 0 Then
                                obj.TAX2 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX2 + "'")
                                If clsCommon.myLen(obj.TAX2) <= 0 Then
                                    Throw New Exception("Line " + LineNo + " : Tax2 does not exist.")
                                End If
                                obj.TAX2_Rate = clsCommon.myCdbl(dgrv.Cells("TAX2_Rate").Value)
                                obj.TAX2_Amt = clsCommon.myCdbl(dgrv.Cells("TAX2_Amt").Value)
                                obj.TAX3 = clsCommon.myCstr(dgrv.Cells("TAX3").Value)
                                If clsCommon.myLen(obj.TAX3) > 0 Then
                                    obj.TAX3 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX3 + "'")
                                    If clsCommon.myLen(obj.TAX3) <= 0 Then
                                        Throw New Exception("Line " + LineNo + " : Tax3 does not exist.")
                                    End If
                                    obj.TAX3_Rate = clsCommon.myCdbl(dgrv.Cells("TAX3_Rate").Value)
                                    obj.TAX3_Amt = clsCommon.myCdbl(dgrv.Cells("TAX3_Amt").Value)
                                    obj.TAX4 = clsCommon.myCstr(dgrv.Cells("TAX4").Value)
                                    If clsCommon.myLen(obj.TAX4) > 0 Then
                                        obj.TAX4 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX4 + "'")
                                        If clsCommon.myLen(obj.TAX4) <= 0 Then
                                            Throw New Exception("Line " + LineNo + " : Tax4 does not exist.")
                                        End If
                                        obj.TAX4_Rate = clsCommon.myCdbl(dgrv.Cells("TAX4_Rate").Value)
                                        obj.TAX4_Amt = clsCommon.myCdbl(dgrv.Cells("TAX4_Amt").Value)
                                        obj.TAX5 = clsCommon.myCstr(dgrv.Cells("TAX5").Value)
                                        If clsCommon.myLen(obj.TAX5) > 0 Then
                                            obj.TAX5 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX5 + "'")
                                            If clsCommon.myLen(obj.TAX5) <= 0 Then
                                                Throw New Exception("Line " + LineNo + " : Tax5 does not exist.")
                                            End If
                                            obj.TAX5_Rate = clsCommon.myCdbl(dgrv.Cells("TAX5_Rate").Value)
                                            obj.TAX5_Amt = clsCommon.myCdbl(dgrv.Cells("TAX5_Amt").Value)
                                            obj.TAX6 = clsCommon.myCstr(dgrv.Cells("TAX6").Value)
                                            If clsCommon.myLen(obj.TAX6) > 0 Then
                                                obj.TAX6 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX6 + "'")
                                                If clsCommon.myLen(obj.TAX6) <= 0 Then
                                                    Throw New Exception("Line " + LineNo + " : Tax6 does not exist.")
                                                End If
                                                obj.TAX6_Rate = clsCommon.myCdbl(dgrv.Cells("TAX6_Rate").Value)
                                                obj.TAX6_Amt = clsCommon.myCdbl(dgrv.Cells("TAX6_Amt").Value)
                                                obj.TAX7 = clsCommon.myCstr(dgrv.Cells("TAX7").Value)
                                                If clsCommon.myLen(obj.TAX7) > 0 Then
                                                    obj.TAX7 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX7 + "'")
                                                    If clsCommon.myLen(obj.TAX7) <= 0 Then
                                                        Throw New Exception("Line " + LineNo + " : Tax7 does not exist.")
                                                    End If
                                                    obj.TAX7_Rate = clsCommon.myCdbl(dgrv.Cells("TAX7_Rate").Value)
                                                    obj.TAX7_Amt = clsCommon.myCdbl(dgrv.Cells("TAX7_Amt").Value)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Tax1.")
                        End If
                        obj.Price_Code = clsCommon.myCstr(dgrv.Cells("Price_Code").Value)
                        If clsCommon.myLen(obj.Price_Code) > 0 Then
                            obj.Price_Code = clsDBFuncationality.getSingleValue("Select MAX(Price_Code)  from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" + obj.Price_Code + "'")
                            If clsCommon.myLen(obj.Price_Code) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Price Code does not exist.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Price Code.")
                        End If
                        obj.Abatement_Rate = clsCommon.myCdbl(dgrv.Cells("Abatement_Rate").Value)
                        obj.Abatement = clsCommon.myCdbl(dgrv.Cells("Abatement").Value)
                        obj.IsNewEntry = True
                        Arr.Add(obj)
                    End If
                Next
                If (clsPriceMaster.SaveData(Arr, False)) Then
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub MRPWithVAT()
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Item_Code", "Item_Desc", "UOM", "Conversion Factor", "Remarks", "Item_Basic_Price_Type", "Basic_Price_on", "Landing_Cost", "Purchase_Cost", "Item_MRP", "Item_Basic_Price", "Item_Basic_Net", "Markup_On", "Markup_Percent", "Tax_Manipulation_On", "Start_Date", "End_Date", "Tax_group", "TAX1", "TAX1_Rate", "TAX1_Amt", "TAX2", "TAX2_Rate", "TAX2_Amt", "TAX3", "TAX3_Rate", "TAX3_Amt", "TAX4", "TAX4_Rate", "TAX4_Amt", "TAX5", "TAX5_Rate", "TAX5_Amt", "TAX6", "TAX6_Rate", "TAX6_Amt", "TAX7", "TAX7_Rate", "TAX7_Amt", "TAX8", "TAX8_Rate", "TAX8_Amt", "TAX9", "TAX9_Rate", "TAX9_Amt", "TAX10", "TAX10_Rate", "TAX10_Amt", "Price_Code", "Abatement_Rate", "Abatement", "MRP-VAT", "ITF_CODE") Then
            Dim LineNo As String
            Dim ii As Integer = 0
            Try
                clsCommon.ProgressBarPercentShow()
                Dim Arr As New List(Of clsPriceMaster)
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    LineNo = clsCommon.myCstr(dgrv.Index + 2)
                    ii += 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100) / dgv.RowCount - 1, "Importing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(dgv.RowCount - 1))
                    Dim obj As New clsPriceMaster()
                    obj.Item_Code = clsCommon.myCstr(dgrv.Cells("Item_Code").Value)
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        obj.Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code From TSPL_ITem_MASTER WHERE Item_Code='" + obj.Item_Code + "'")
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Throw New Exception("Line " + LineNo + " : Item Code does not exist.")
                        End If

                        obj.UOM = clsCommon.myCstr(dgrv.Cells("UOM").Value)
                        If clsCommon.myLen(obj.UOM) > 0 Then
                            obj.UOM = clsDBFuncationality.getSingleValue("Select UOM_Code from TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + obj.Item_Code + "' AND UOM_Code='" + obj.UOM + "'")
                            If clsCommon.myLen(obj.UOM) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : UOM does not exist for this item.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter UOM.")
                        End If

                        obj.Remarks = clsCommon.myCstr(dgrv.Cells("Remarks").Value)
                        If clsCommon.myLen(obj.Remarks) > 200 Then
                            Throw New Exception("Line " + LineNo + " : Remarks can not be greator than 200.")
                        End If

                        obj.Item_Basic_Price_Type = clsCommon.myCstr(dgrv.Cells("Item_Basic_Price_Type").Value)
                        If Not (clsCommon.CompairString(obj.Item_Basic_Price_Type, "Auto") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Item_Basic_Price_Type, "Mannual") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Item_Basic_Price_Type can be like 'Auto' or 'Mannual'.")
                        End If

                        obj.Basic_Price_On = clsCommon.myCstr(dgrv.Cells("Basic_Price_On").Value)
                        If Not (clsCommon.CompairString(obj.Basic_Price_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Basic_Price_On, "Purchase Cost") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Basic_Price_On can be like 'Landing Cost' or 'Purchase Cost'.")
                        End If
                        obj.Landing_Cost = clsCommon.myCdbl(dgrv.Cells("Landing_Cost").Value)
                        obj.Purchase_Cost = clsCommon.myCdbl(dgrv.Cells("Purchase_Cost").Value)
                        obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value)
                        obj.Item_Basic_Price = clsCommon.myCdbl(dgrv.Cells("Item_Basic_Price").Value)
                        obj.Item_Basic_Net = clsCommon.myCdbl(dgrv.Cells("Item_Basic_Net").Value)
                        obj.Markup_On = clsCommon.myCstr(dgrv.Cells("Markup_On").Value)
                        If Not (clsCommon.CompairString(obj.Markup_On, "None") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Markup_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Markup_On, "MRP") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Markup_On can be like 'None' or 'Landing Cost' or 'MRP'.")
                        End If
                        obj.Markup_Percent = clsCommon.myCdbl(dgrv.Cells("Markup_Percent").Value)
                        obj.Tax_Manipulation_On = clsCommon.myCstr(dgrv.Cells("Tax_Manipulation_On").Value)
                        If Not (clsCommon.CompairString(obj.Tax_Manipulation_On, "Landing Cost") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Tax_Manipulation_On, "Basic Price") = CompairStringResult.Equal) Then
                            Throw New Exception("Line " + LineNo + " : Tax Manipulation can be of type 'Landing Cost' or 'Basic Price'.")
                        End If
                        obj.Start_Date = clsCommon.myCstr(dgrv.Cells("Start_Date").Value)
                        If clsCommon.myLen(dgrv.Cells("End_Date").Value) Then
                            obj.End_Date = clsCommon.myCstr(dgrv.Cells("End_Date").Value)
                        End If
                        obj.Tax_group = clsCommon.myCstr(dgrv.Cells("Tax_group").Value)
                        If clsCommon.myLen(obj.Tax_group) > 0 Then
                            obj.Tax_group = clsDBFuncationality.getSingleValue("Select Tax_Group_Code from TSPL_TAX_GROUP_MASTER WHERE Tax_Group_Code='" + obj.Tax_group + "'")
                            If clsCommon.myLen(obj.Tax_group) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Tax Group does not exist.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Tax Group.")
                        End If
                        obj.TAX1 = clsCommon.myCstr(dgrv.Cells("TAX1").Value)
                        If clsCommon.myLen(obj.TAX1) > 0 Then
                            obj.TAX1 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX1 + "'")
                            If clsCommon.myLen(obj.TAX1) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Tax1 does not exist.")
                            End If
                            obj.TAX1_Rate = clsCommon.myCdbl(dgrv.Cells("TAX1_Rate").Value)
                            obj.TAX1_Amt = clsCommon.myCdbl(dgrv.Cells("TAX1_Amt").Value)
                            obj.TAX2 = clsCommon.myCstr(dgrv.Cells("TAX2").Value)
                            If clsCommon.myLen(obj.TAX2) > 0 Then
                                obj.TAX2 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX2 + "'")
                                If clsCommon.myLen(obj.TAX2) <= 0 Then
                                    Throw New Exception("Line " + LineNo + " : Tax2 does not exist.")
                                End If
                                obj.TAX2_Rate = clsCommon.myCdbl(dgrv.Cells("TAX2_Rate").Value)
                                obj.TAX2_Amt = clsCommon.myCdbl(dgrv.Cells("TAX2_Amt").Value)
                                obj.TAX3 = clsCommon.myCstr(dgrv.Cells("TAX3").Value)
                                If clsCommon.myLen(obj.TAX3) > 0 Then
                                    obj.TAX3 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX3 + "'")
                                    If clsCommon.myLen(obj.TAX3) <= 0 Then
                                        Throw New Exception("Line " + LineNo + " : Tax3 does not exist.")
                                    End If
                                    obj.TAX3_Rate = clsCommon.myCdbl(dgrv.Cells("TAX3_Rate").Value)
                                    obj.TAX3_Amt = clsCommon.myCdbl(dgrv.Cells("TAX3_Amt").Value)
                                    obj.TAX4 = clsCommon.myCstr(dgrv.Cells("TAX4").Value)
                                    If clsCommon.myLen(obj.TAX4) > 0 Then
                                        obj.TAX4 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX4 + "'")
                                        If clsCommon.myLen(obj.TAX4) <= 0 Then
                                            Throw New Exception("Line " + LineNo + " : Tax4 does not exist.")
                                        End If
                                        obj.TAX4_Rate = clsCommon.myCdbl(dgrv.Cells("TAX4_Rate").Value)
                                        obj.TAX4_Amt = clsCommon.myCdbl(dgrv.Cells("TAX4_Amt").Value)
                                        obj.TAX5 = clsCommon.myCstr(dgrv.Cells("TAX5").Value)
                                        If clsCommon.myLen(obj.TAX5) > 0 Then
                                            obj.TAX5 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX5 + "'")
                                            If clsCommon.myLen(obj.TAX5) <= 0 Then
                                                Throw New Exception("Line " + LineNo + " : Tax5 does not exist.")
                                            End If
                                            obj.TAX5_Rate = clsCommon.myCdbl(dgrv.Cells("TAX5_Rate").Value)
                                            obj.TAX5_Amt = clsCommon.myCdbl(dgrv.Cells("TAX5_Amt").Value)
                                            obj.TAX6 = clsCommon.myCstr(dgrv.Cells("TAX6").Value)
                                            If clsCommon.myLen(obj.TAX6) > 0 Then
                                                obj.TAX6 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX6 + "'")
                                                If clsCommon.myLen(obj.TAX6) <= 0 Then
                                                    Throw New Exception("Line " + LineNo + " : Tax6 does not exist.")
                                                End If
                                                obj.TAX6_Rate = clsCommon.myCdbl(dgrv.Cells("TAX6_Rate").Value)
                                                obj.TAX6_Amt = clsCommon.myCdbl(dgrv.Cells("TAX6_Amt").Value)
                                                obj.TAX7 = clsCommon.myCstr(dgrv.Cells("TAX7").Value)
                                                If clsCommon.myLen(obj.TAX7) > 0 Then
                                                    obj.TAX7 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX7 + "'")
                                                    If clsCommon.myLen(obj.TAX7) <= 0 Then
                                                        Throw New Exception("Line " + LineNo + " : Tax7 does not exist.")
                                                    End If
                                                    obj.TAX7_Rate = clsCommon.myCdbl(dgrv.Cells("TAX7_Rate").Value)
                                                    obj.TAX7_Amt = clsCommon.myCdbl(dgrv.Cells("TAX7_Amt").Value)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Tax1.")
                        End If
                        obj.Price_Code = clsCommon.myCstr(dgrv.Cells("Price_Code").Value)
                        If clsCommon.myLen(obj.Price_Code) > 0 Then

                            obj.Price_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Price_Code)  from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" + obj.Price_Code + "'"))
                            If clsCommon.myLen(obj.Price_Code) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Price Code does not exist.Please Create This Price Code In Price Component Mapping.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Price Code.")
                        End If
                        obj.Abatement_Rate = clsCommon.myCdbl(dgrv.Cells("Abatement_Rate").Value)
                        obj.Abatement = clsCommon.myCdbl(dgrv.Cells("Abatement").Value)
                        obj.IsNewEntry = True
                        Arr.Add(obj)
                    End If
                Next
                If (clsPriceMaster.SaveData(Arr, False)) Then
                    clsCommon.ProgressBarPercentHide()

                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    'Private Sub MRPWithBackCalculation()
    '    Dim dgv As New RadGridView
    '    Me.Controls.Add(dgv)
    '    If transportSql.importExcel(dgv, "Item_Code", "Item_Desc", "UOM", "Remarks", "Item_MRP", "Start_Date", "End_Date", "Price_Code", "Price_Comp1", "Price_Comp_Desc1", "Price_Rate1", "Price_Amount1", "Price_Comp2", "Price_Comp_Desc2", "Price_Rate2", "Price_Amount2", "Price_Comp3", "Price_Comp_Desc3", "Price_Rate3", "Price_Amount3", "Price_Comp4", "Price_Comp_Desc4", "Price_Rate4", "Price_Amount4", "Price_Comp5", "Price_Comp_Desc5", "Price_Rate5", "Price_Amount5", "Price_Comp6", "Price_Comp_Desc6", "Price_Rate6", "Price_Amount6", "Price_Comp7", "Price_Comp_Desc7", "Price_Rate7", "Price_Amount7", "Price_Comp8", "Price_Comp_Desc8", "Price_Rate8", "Price_Amount8", "Price_Comp9", "Price_Comp_Desc9", "Price_Rate9", "Price_Amount9", "Price_Comp10", "Price_Comp_Desc10", "Price_Rate10", "Price_Amount10", "Calculate With Tax (Y/N)", "Tax_group", "TAX1", "TAX1_Rate", "TAX1_Amt", "TAX2", "TAX2_Rate", "TAX2_Amt", "TAX3", "TAX3_Rate", "TAX3_Amt", "TAX4", "TAX4_Rate", "TAX4_Amt", "TAX5", "TAX5_Rate", "TAX5_Amt", "TAX6", "TAX6_Rate", "TAX6_Amt", "TAX7", "TAX7_Rate", "TAX7_Amt", "TAX8", "TAX8_Rate", "TAX8_Amt", "TAX9", "TAX9_Rate", "TAX9_Amt", "TAX10", "TAX10_Rate", "TAX10_Amt", "Location_Code", "Type") Then
    '        Dim LineNo As String
    '        Dim count As Integer = 0
    '        Try
    '            clsCommon.ProgressBarPercentShow()
    '            Dim Arr As New List(Of clsPriceMaster)
    '            Dim TotalTaxAmt As Decimal
    '            Dim TotalMarginAmt As Decimal
    '            For Each dgrv As GridViewRowInfo In dgv.Rows
    '                LineNo = clsCommon.myCstr(dgrv.Index + 2)
    '                count += 1
    '                clsCommon.ProgressBarPercentUpdate((count * 100) / dgv.RowCount - 1, "Importing " + clsCommon.myCstr(count) + "/" + clsCommon.myCstr(dgv.RowCount - 1))
    '                Dim obj As New clsPriceMaster()
    '                obj.Item_Code = clsCommon.myCstr(dgrv.Cells("Item_Code").Value)
    '                If clsCommon.myLen(obj.Item_Code) > 0 Then
    '                    obj.Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code From TSPL_ITem_MASTER WHERE Item_Code='" + obj.Item_Code + "'")
    '                    If clsCommon.myLen(obj.Item_Code) <= 0 Then
    '                        Throw New Exception("Line " + LineNo + " : Item Code does not exist.")
    '                    End If

    '                    obj.UOM = clsCommon.myCstr(dgrv.Cells("UOM").Value)
    '                    If clsCommon.myLen(obj.UOM) > 0 Then
    '                        obj.UOM = clsDBFuncationality.getSingleValue("Select UOM_Code from TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + obj.Item_Code + "' AND UOM_Code='" + obj.UOM + "'")
    '                        If clsCommon.myLen(obj.UOM) <= 0 Then
    '                            Throw New Exception("Line " + LineNo + " : UOM does not exist for this item.")
    '                        End If
    '                    Else
    '                        Throw New Exception("Line " + LineNo + " : Enter UOM.")
    '                    End If
    '                    '=====================================added by preeti gupta============================
    '                    'Dim dtUOMPrice As New DataTable()
    '                    'dtUOMPrice.Columns.Add("UOM", GetType(String))
    '                    'dt = clsPriceMaster.GetAllConversionOfItem(clsCommon.myCstr(dgrv.Cells("Item_Code").Value))

    '                    'obj.Item_Basic_Net = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
    '                    'obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
    '                    '======================================================================================
    '                    obj.Remarks = clsCommon.myCstr(dgrv.Cells("Remarks").Value)
    '                    If clsCommon.myLen(obj.Remarks) > 200 Then
    '                        Throw New Exception("Line " + LineNo + " : Remarks can not be greator than 200.")
    '                    End If

    '                    obj.Item_Basic_Price_Type = "Auto"
    '                    obj.Price_Category = "Auto" '------------------------------Is Used to check whether With Back Calculation Or Not.
    '                    obj.Basic_Price_On = "Landing Cost"
    '                    obj.Landing_Cost = 0.0
    '                    obj.Purchase_Cost = 0.0
    '                    '===================preeti gupta=========
    '                    obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value)
    '                    obj.Item_Basic_Net = obj.Item_MRP
    '                    '======================================================
    '                    obj.Markup_On = "None"
    '                    obj.Markup_Percent = 0.0
    '                    obj.Tax_Manipulation_On = "Landing Cost"
    '                    obj.Can_Edit = "Y"
    '                    If clsCommon.myLen(dgrv.Cells("Start_Date").Value) <= 0 Then
    '                        Throw New Exception("Line " + LineNo + " : Start_Date can not be left blank.")
    '                    End If
    '                    obj.Start_Date = clsCommon.myCstr(dgrv.Cells("Start_Date").Value)
    '                    If clsCommon.myLen(dgrv.Cells("End_Date").Value) > 0 Then
    '                        obj.End_Date = clsCommon.myCstr(dgrv.Cells("End_Date").Value)
    '                    End If
    '                    TotalTaxAmt = 0.0
    '                    '--------------------------Get Total Tax Rate & Total Margin Amt-------------------------
    '                    TotalTaxRate = 0.0
    '                    TotalMarginAmt = 0.0
    '                    For ii As Integer = 1 To 10
    '                        TotalTaxRate += clsCommon.myCdbl(dgrv.Cells("TAX" & ii.ToString() & "_Rate").Value)
    '                        TotalMarginAmt += clsCommon.myCdbl(dgrv.Cells("Price_Amount" & ii.ToString() & "").Value)
    '                    Next
    '                    '---------------------------------------------------------------------
    '                    obj.Is_With_Tax = clsCommon.myCstr(dgrv.Cells("Calculate With Tax (Y/N)").Value)
    '                    obj.Item_Basic_Price = Math.Round((obj.Item_MRP - TotalMarginAmt) * 100 / (100 + TotalTaxRate), 5)
    '                    obj.Tax_group = clsCommon.myCstr(dgrv.Cells("Tax_group").Value)
    '                    If clsCommon.myLen(obj.Tax_group) > 0 Then
    '                        obj.Tax_group = clsDBFuncationality.getSingleValue("Select Tax_Group_Code from TSPL_TAX_GROUP_MASTER WHERE Tax_Group_Code='" + obj.Tax_group + "'")
    '                        If clsCommon.myLen(obj.Tax_group) <= 0 Then
    '                            Throw New Exception("Line " + LineNo + " : Tax Group does not exist.")
    '                        End If
    '                    Else
    '                        Throw New Exception("Line " + LineNo + " : Enter Tax Group.")
    '                    End If
    '                    obj.TAX1 = clsCommon.myCstr(dgrv.Cells("TAX1").Value)
    '                    If clsCommon.myLen(obj.TAX1) > 0 Then
    '                        obj.TAX1 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX1 + "'")
    '                        If clsCommon.myLen(obj.TAX1) <= 0 Then
    '                            Throw New Exception("Line " + LineNo + " : Tax1 does not exist.")
    '                        End If
    '                        obj.TAX1_Rate = clsCommon.myCdbl(dgrv.Cells("TAX1_Rate").Value)
    '                        obj.TAX1_Amt = obj.Item_Basic_Price * obj.TAX1_Rate / 100 'clsCommon.myCdbl(dgrv.Cells("TAX1_Amt").Value)
    '                        'TotalTaxAmt += obj.TAX1_Amt '----------------------------------------Tax1_Amt is Added.
    '                        obj.TAX2 = clsCommon.myCstr(dgrv.Cells("TAX2").Value)
    '                        If clsCommon.myLen(obj.TAX2) > 0 Then
    '                            obj.TAX2 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX2 + "'")
    '                            If clsCommon.myLen(obj.TAX2) <= 0 Then
    '                                Throw New Exception("Line " + LineNo + " : Tax2 does not exist.")
    '                            End If
    '                            obj.TAX2_Rate = clsCommon.myCdbl(dgrv.Cells("TAX2_Rate").Value)
    '                            obj.TAX2_Amt = obj.Item_Basic_Price * obj.TAX2_Rate / 100 'clsCommon.myCdbl(dgrv.Cells("TAX2_Amt").Value)
    '                            'TotalTaxAmt += obj.TAX3_Amt '----------------------------------------Tax2_Amt is Added.
    '                            obj.TAX3 = clsCommon.myCstr(dgrv.Cells("TAX3").Value)
    '                            If clsCommon.myLen(obj.TAX3) > 0 Then
    '                                obj.TAX3 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX3 + "'")
    '                                If clsCommon.myLen(obj.TAX3) <= 0 Then
    '                                    Throw New Exception("Line " + LineNo + " : Tax3 does not exist.")
    '                                End If
    '                                obj.TAX3_Rate = clsCommon.myCdbl(dgrv.Cells("TAX3_Rate").Value)
    '                                obj.TAX3_Amt = obj.Item_Basic_Price * obj.TAX3_Rate / 100 'clsCommon.myCdbl(dgrv.Cells("TAX3_Amt").Value)
    '                                'TotalTaxAmt += obj.TAX3_Amt '----------------------------------------Tax3_Amt is Added.
    '                                obj.TAX4 = clsCommon.myCstr(dgrv.Cells("TAX4").Value)
    '                                If clsCommon.myLen(obj.TAX4) > 0 Then
    '                                    obj.TAX4 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX4 + "'")
    '                                    If clsCommon.myLen(obj.TAX4) <= 0 Then
    '                                        Throw New Exception("Line " + LineNo + " : Tax4 does not exist.")
    '                                    End If
    '                                    obj.TAX4_Rate = clsCommon.myCdbl(dgrv.Cells("TAX4_Rate").Value)
    '                                    obj.TAX4_Amt = obj.Item_Basic_Price * obj.TAX4_Rate / 100 'clsCommon.myCdbl(dgrv.Cells("TAX4_Amt").Value)
    '                                    'TotalTaxAmt += obj.TAX4_Amt '----------------------------------------Tax4_Amt is Added.
    '                                    obj.TAX5 = clsCommon.myCstr(dgrv.Cells("TAX5").Value)
    '                                    If clsCommon.myLen(obj.TAX5) > 0 Then
    '                                        obj.TAX5 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX5 + "'")
    '                                        If clsCommon.myLen(obj.TAX5) <= 0 Then
    '                                            Throw New Exception("Line " + LineNo + " : Tax5 does not exist.")
    '                                        End If
    '                                        obj.TAX5_Rate = clsCommon.myCdbl(dgrv.Cells("TAX5_Rate").Value)
    '                                        obj.TAX5_Amt = obj.Item_Basic_Price * obj.TAX5_Rate / 100 'clsCommon.myCdbl(dgrv.Cells("TAX5_Amt").Value)
    '                                        'TotalTaxAmt += obj.TAX5_Amt '----------------------------------------Tax5_Amt is Added.
    '                                        obj.TAX6 = clsCommon.myCstr(dgrv.Cells("TAX6").Value)
    '                                        If clsCommon.myLen(obj.TAX6) > 0 Then
    '                                            obj.TAX6 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX6 + "'")
    '                                            If clsCommon.myLen(obj.TAX6) <= 0 Then
    '                                                Throw New Exception("Line " + LineNo + " : Tax6 does not exist.")
    '                                            End If
    '                                            obj.TAX6_Rate = clsCommon.myCdbl(dgrv.Cells("TAX6_Rate").Value)
    '                                            obj.TAX6_Amt = obj.Item_Basic_Price * obj.TAX6_Rate / 100 'clsCommon.myCdbl(dgrv.Cells("TAX6_Amt").Value)
    '                                            'TotalTaxAmt += obj.TAX6_Amt '----------------------------------------Tax6_Amt is Added.
    '                                            obj.TAX7 = clsCommon.myCstr(dgrv.Cells("TAX7").Value)
    '                                            If clsCommon.myLen(obj.TAX7) > 0 Then
    '                                                obj.TAX7 = clsDBFuncationality.getSingleValue("Select Tax_Code from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.TAX7 + "'")
    '                                                If clsCommon.myLen(obj.TAX7) <= 0 Then
    '                                                    Throw New Exception("Line " + LineNo + " : Tax7 does not exist.")
    '                                                End If
    '                                                obj.TAX7_Rate = clsCommon.myCdbl(dgrv.Cells("TAX7_Rate").Value)
    '                                                obj.TAX7_Amt = obj.Item_Basic_Price * obj.TAX7_Rate / 100 'clsCommon.myCdbl(dgrv.Cells("TAX7_Amt").Value)
    '                                                'TotalTaxAmt += obj.TAX7_Amt '----------------------------------------Tax7_Amt is Added.
    '                                            End If
    '                                        End If
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    Else
    '                        Throw New Exception("Line " + LineNo + " : Enter Tax1.")
    '                    End If
    '                    If clsCommon.CompairString(obj.Is_With_Tax, "Y") = CompairStringResult.Equal Then
    '                    Else
    '                        obj.Is_With_Tax = "N"
    '                        obj.Item_Basic_Price = Math.Round((obj.Item_MRP - TotalMarginAmt), 5)
    '                    End If


    '                    obj.Price_Code = clsCommon.myCstr(dgrv.Cells("Price_Code").Value)
    '                    If clsCommon.myLen(obj.Price_Code) > 0 Then

    '                        obj.Price_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Price_Code)  from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" + obj.Price_Code + "'"))
    '                        If clsCommon.myLen(obj.Price_Code) <= 0 Then
    '                            Throw New Exception("Line " + LineNo + " : Price Code does not exist.Please Create This Price Code In Price Component Mapping.")
    '                        End If
    '                    Else
    '                        Throw New Exception("Line " + LineNo + " : Enter Price Code.")
    '                    End If
    '                    '=================================Added by Preeti Gupta========================
    '                    obj.Location_Code = clsCommon.myCstr(dgrv.Cells("Location_Code").Value)
    '                    If clsCommon.myLen(obj.Location_Code) > 0 Then
    '                        obj.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from tspl_location_master WHERE Location_Code='" + obj.Location_Code + "'"))
    '                        If clsCommon.myLen(obj.Location_Code) <= 0 Then
    '                            Throw New Exception("Line " + LineNo + " : Location Code does not exist.Please Create This Location Code In Location Master.")
    '                        End If
    '                    Else
    '                        Throw New Exception("Line " + LineNo + " : Enter Location Code.")
    '                    End If
    '                    obj.type = clsCommon.myCstr(dgrv.Cells("Type").Value)
    '                    '===============================================================================
    '                    '---------Price Component-----------------------------------------------------------------------------

    '                    obj.Price_Comp1 = clsCommon.myCstr(dgrv.Cells("Price_Comp1").Value)
    '                    If clsCommon.myLen(obj.Price_Comp1) > 0 Then
    '                        obj.Price_Comp1 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp1 & "'")
    '                        If clsCommon.myLen(obj.Price_Comp1) <= 0 Then
    '                            Throw New Exception("Line " + LineNo + " : Price_Comp1 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                        End If
    '                        obj.Price_Comp_Desc1 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc1").Value)
    '                        obj.Price_Rate1 = clsCommon.myCdbl(dgrv.Cells("Price_Rate1").Value)
    '                        obj.Price_Amount1 = clsCommon.myCdbl(dgrv.Cells("Price_Amount1").Value)
    '                        'TotalMarginAmt += obj.Price_Amount1 '----------------------------------------Price_Component_amt1 is Added.
    '                        obj.Price_Comp2 = clsCommon.myCstr(dgrv.Cells("Price_Comp2").Value)
    '                        If clsCommon.myLen(obj.Price_Comp2) > 0 Then
    '                            obj.Price_Comp2 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp2 & "'")
    '                            If clsCommon.myLen(obj.Price_Comp2) <= 0 Then
    '                                Throw New Exception("Line " + LineNo + " : Price_Comp2 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                            End If
    '                            obj.Price_Comp_Desc2 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc2").Value)
    '                            obj.Price_Rate2 = clsCommon.myCdbl(dgrv.Cells("Price_Rate2").Value)
    '                            obj.Price_Amount2 = clsCommon.myCdbl(dgrv.Cells("Price_Amount2").Value)
    '                            'TotalMarginAmt += obj.Price_Amount2 '----------------------------------------Price_Component_amt2 is Added.
    '                            obj.Price_Comp3 = clsCommon.myCstr(dgrv.Cells("Price_Comp3").Value)
    '                            If clsCommon.myLen(obj.Price_Comp3) > 0 Then
    '                                obj.Price_Comp3 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp3 & "'")
    '                                If clsCommon.myLen(obj.Price_Comp3) <= 0 Then
    '                                    Throw New Exception("Line " + LineNo + " : Price_Comp3 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                                End If
    '                                obj.Price_Comp_Desc3 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc3").Value)
    '                                obj.Price_Rate3 = clsCommon.myCdbl(dgrv.Cells("Price_Rate3").Value)
    '                                obj.Price_Amount3 = clsCommon.myCdbl(dgrv.Cells("Price_Amount3").Value)
    '                                'TotalMarginAmt += obj.Price_Amount3 '----------------------------------------Price_Component_amt3 is Added.
    '                                obj.Price_Comp4 = clsCommon.myCstr(dgrv.Cells("Price_Comp4").Value)
    '                                If clsCommon.myLen(obj.Price_Comp4) > 0 Then
    '                                    obj.Price_Comp4 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp4 & "'")
    '                                    If clsCommon.myLen(obj.Price_Comp4) <= 0 Then
    '                                        Throw New Exception("Line " + LineNo + " : Price_Comp4 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                                    End If
    '                                    obj.Price_Comp_Desc4 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc4").Value)
    '                                    obj.Price_Rate4 = clsCommon.myCdbl(dgrv.Cells("Price_Rate4").Value)
    '                                    obj.Price_Amount4 = clsCommon.myCdbl(dgrv.Cells("Price_Amount4").Value)
    '                                    'TotalMarginAmt += obj.Price_Amount4 '----------------------------------------Price_Component_amt4 is Added.
    '                                    obj.Price_Comp5 = clsCommon.myCstr(dgrv.Cells("Price_Comp5").Value)
    '                                    If clsCommon.myLen(obj.Price_Comp5) > 0 Then
    '                                        obj.Price_Comp5 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp5 & "'")
    '                                        If clsCommon.myLen(obj.Price_Comp5) <= 0 Then
    '                                            Throw New Exception("Line " + LineNo + " : Price_Comp5 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                                        End If
    '                                        obj.Price_Comp_Desc5 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc5").Value)
    '                                        obj.Price_Rate5 = clsCommon.myCdbl(dgrv.Cells("Price_Rate5").Value)
    '                                        obj.Price_Amount5 = clsCommon.myCdbl(dgrv.Cells("Price_Amount5").Value)
    '                                        'TotalMarginAmt += obj.Price_Amount5 '----------------------------------------Price_Component_amt5 is Added.
    '                                        obj.Price_Comp6 = clsCommon.myCstr(dgrv.Cells("Price_Comp6").Value)
    '                                        If clsCommon.myLen(obj.Price_Comp6) > 0 Then
    '                                            obj.Price_Comp6 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp6 & "'")
    '                                            If clsCommon.myLen(obj.Price_Comp6) <= 0 Then
    '                                                Throw New Exception("Line " + LineNo + " : Price_Comp6 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                                            End If
    '                                            obj.Price_Comp_Desc6 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc6").Value)
    '                                            obj.Price_Rate6 = clsCommon.myCdbl(dgrv.Cells("Price_Rate6").Value)
    '                                            obj.Price_Amount6 = clsCommon.myCdbl(dgrv.Cells("Price_Amount6").Value)
    '                                            '   TotalMarginAmt += obj.Price_Amount6 '----------------------------------------Price_Component_amt6 is Added.
    '                                            obj.Price_Comp7 = clsCommon.myCstr(dgrv.Cells("Price_Comp7").Value)
    '                                            If clsCommon.myLen(obj.Price_Comp7) > 0 Then
    '                                                obj.Price_Comp7 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp7 & "'")
    '                                                If clsCommon.myLen(obj.Price_Comp7) <= 0 Then
    '                                                    Throw New Exception("Line " + LineNo + " : Price_Comp7 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                                                End If
    '                                                obj.Price_Comp_Desc7 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc7").Value)
    '                                                obj.Price_Rate7 = clsCommon.myCdbl(dgrv.Cells("Price_Rate7").Value)
    '                                                obj.Price_Amount7 = clsCommon.myCdbl(dgrv.Cells("Price_Amount7").Value)
    '                                                '      TotalMarginAmt += obj.Price_Amount7 '----------------------------------------Price_Component_amt7 is Added.
    '                                                obj.Price_Comp8 = clsCommon.myCstr(dgrv.Cells("Price_Comp6").Value)
    '                                                If clsCommon.myLen(obj.Price_Comp8) > 0 Then
    '                                                    obj.Price_Comp8 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp8 & "'")
    '                                                    If clsCommon.myLen(obj.Price_Comp8) <= 0 Then
    '                                                        Throw New Exception("Line " + LineNo + " : Price_Comp8 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                                                    End If
    '                                                    obj.Price_Comp_Desc8 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc8").Value)
    '                                                    obj.Price_Rate8 = clsCommon.myCdbl(dgrv.Cells("Price_Rate8").Value)
    '                                                    obj.Price_Amount8 = clsCommon.myCdbl(dgrv.Cells("Price_Amount8").Value)
    '                                                    'TotalMarginAmt += obj.Price_Amount8 '----------------------------------------Price_Component_amt8 is Added.
    '                                                    obj.Price_Comp9 = clsCommon.myCstr(dgrv.Cells("Price_Comp9").Value)
    '                                                    If clsCommon.myLen(obj.Price_Comp9) > 0 Then
    '                                                        obj.Price_Comp9 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp9 & "'")
    '                                                        If clsCommon.myLen(obj.Price_Comp9) <= 0 Then
    '                                                            Throw New Exception("Line " + LineNo + " : Price_Comp9 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                                                        End If
    '                                                        obj.Price_Comp_Desc9 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc9").Value)
    '                                                        obj.Price_Rate9 = clsCommon.myCdbl(dgrv.Cells("Price_Rate9").Value)
    '                                                        obj.Price_Amount9 = clsCommon.myCdbl(dgrv.Cells("Price_Amount9").Value)
    '                                                        'TotalMarginAmt += obj.Price_Amount9 '----------------------------------------Price_Component_amt9 is Added.
    '                                                        obj.Price_Comp10 = clsCommon.myCstr(dgrv.Cells("Price_Comp10").Value)
    '                                                        If clsCommon.myLen(obj.Price_Comp10) > 0 Then
    '                                                            obj.Price_Comp10 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp10 & "'")
    '                                                            If clsCommon.myLen(obj.Price_Comp10) <= 0 Then
    '                                                                Throw New Exception("Line " + LineNo + " : Price_Comp10 does not mapped with Price Code : " & obj.Price_Code & ".")
    '                                                            End If
    '                                                            obj.Price_Comp_Desc10 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc10").Value)
    '                                                            obj.Price_Rate10 = clsCommon.myCdbl(dgrv.Cells("Price_Rate10").Value)
    '                                                            obj.Price_Amount10 = clsCommon.myCdbl(dgrv.Cells("Price_Amount10").Value)
    '                                                            'TotalMarginAmt += obj.Price_Amount10 '----------------------------------------Price_Component_amt10 is Added.
    '                                                        End If
    '                                                    End If
    '                                                End If
    '                                            End If
    '                                        End If
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                    '-------------------
    '                    'obj.Item_Basic_Price = obj.Item_MRP - (TotalMarginAmt + TotalTaxAmt)
    '                    obj.Abatement_Rate = 0.0
    '                    obj.Abatement = 0.0
    '                    obj.IsNewEntry = True
    '                    Arr.Add(obj)
    '                End If
    '            Next
    '            If (clsPriceMaster.SaveData(Arr, False)) Then
    '                clsCommon.ProgressBarPercentHide()
    '                common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
    '            End If
    '        Catch ex As Exception
    '            clsCommon.ProgressBarPercentHide()
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    '    Me.Controls.Remove(dgv)
    'End Sub

    Private Sub ExportVendorCodeWise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportVendorCodeWise.Click

        FrmPriceMasterExport.isExportPriceCode = False
        FrmPriceMasterExport.IsForBackCalculation = False
        FrmPriceMasterExport.ShowDialog()
        'Try
        '    sql = "Select TSPL_ITEM_PRICE_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, UOM, Remarks, TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type, TSPL_ITEM_PRICE_MASTER.Basic_Price_on, Landing_Cost, Purchase_Cost, TSPL_ITEM_PRICE_MASTER.Item_MRP, Item_Basic_Price, Item_Basic_Net, Markup_On, Markup_Percent, Tax_Manipulation_On, Start_Date, End_Date, Tax_group, TAX1, TAX1_Rate, TAX1_Amt, TAX2, TAX2_Rate, TAX2_Amt, TAX3, TAX3_Rate, TAX3_Amt, TAX4, TAX4_Rate, TAX4_Amt, TAX5, TAX5_Rate, TAX5_Amt, TAX6, TAX6_Rate, TAX6_Amt, TAX7, TAX7_Rate, TAX7_Amt, TAX8, TAX8_Rate, TAX8_Amt, TAX9, TAX9_Rate, TAX9_Amt, TAX10, TAX10_Rate, TAX10_Amt, Price_Code, Abatement_Rate, Abatement  from TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code"
        '    transportSql.ExporttoExcel(sql, Me)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub ExportPriceCodeWise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportPriceCodewise.Click
        FrmPriceMasterExport.sql_price = "Select Distinct Price_Code as Code, Price_Code_Desc as [Description]  from TSPL_PRICE_COMPONENT_MAPPING"
        FrmPriceMasterExport.isExportPriceCode = True
        FrmPriceMasterExport.IsForBackCalculation = False
        FrmPriceMasterExport.ShowDialog()
    End Sub

    Private Sub rmiBackCalculation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiBackCalculation.Click
        Try
            FrmPriceMasterExport.IsForBackCalculation = True
            FrmPriceMasterExport.ShowDialog()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkBackCalculation_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBackCalculation.ToggleStateChanged
        Try
            backCalculationChecked(chkBackCalculation.Checked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub backCalculationChecked(ByVal IsChecked As Boolean)
        Try
            ddlBasicRate.Text = "Auto"
            ddlBasicRateOn.Text = "Landing Cost"
            ' ddlTaxMnpln.Text = "Landing Cost"
            ddlTaxMnpln.Text = "Basic Price"
            ddlMarkup.Text = "None"
            ddlBasicRate.Enabled = Not IsChecked
            ddlBasicRateOn.Enabled = Not IsChecked
            ddlMarkup.Enabled = Not IsChecked
            'txtAbtRate.Enabled = Not IsChecked
            'txtAbtRate.Text = ""
            'txtAbtAmount.Enabled = Not IsChecked
            'txtAbtAmount.Text = ""
            ' ddlTaxMnpln.Enabled = Not IsChecked
            ddlTaxMnpln.Enabled = False
            txtMRP.Enabled = IsChecked
            txtLandingCost.Enabled = Not IsChecked
            txtLandingCost.Text = ""
            txtMarkupPercent.Enabled = Not IsChecked
            txtMarkupPercent.Text = ""
            'chkWithoutTax.Checked = True
            lbllAbtAmt.Text = ""
            lbllAbtRate.Text = ""
            lbllBasicRate.Text = ""
            lbllLandingCost.Text = ""
            txtLandingCost.Text = ""
            lbllMarkupPercent.Text = ""
            lbllMRP.Text = ""
            lbllSellingPrice.Text = ""
            lbllTotalTax.Text = ""
            txtTotalItemPrice.Text = ""
            'UpdateAllTotal()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    'Private Sub chkBackCalWithTAX_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBackCalWithTAX.ToggleStateChanged
    '    UpdateAllTotal()
    '    CalculateTotalTAXAmt()
    'End Sub

    Private Sub chkAuto_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAuto.ToggleStateChanged
        Try
            cbgUOM.DataSource = Nothing
            If chkAuto.Checked Then
                LoadItemUOM(txtItemCode.Value, fnduom.Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadItemUOM(ByVal strItemCode As String, ByVal strUom As String, Optional ByVal IsLoadData As Boolean = False)
        Try
            If clsCommon.myLen(txtPriceId.Value) > 0 Then
                'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_ITEM_PRICE_MASTER Where Stock_Price_Id=(Select Stock_Price_Id from TSPL_ITEM_Price_master Where Item_Price_Id=" & txtPriceId.Value & ")")) > 1 Then
                '    chkAuto.Checked = True
                'Else
                '    chkAuto.Checked = False
                'End If
                chkAuto.Checked = True
                cbgUOM.DataSource = clsDBFuncationality.GetDataTable("select Cast(Case When ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Price_Id,'')<>'' Then 1 Else 0 End as Bit) as [Select], UOM_Code as UOM, TSPL_ITEM_PRICE_MASTER.Item_Price_Id as PriceId from TSPL_ITEM_UOM_DETAIL LEFT OUTER JOIN TSPL_ITEM_PRICE_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code WHERE TSPL_ITEM_UOM_DETAIL.Item_Code='" & strItemCode & "' AND TSPL_ITEM_UOM_DETAIL.UOM_Code<>'" & strUom & "' AND TSPL_ITEM_PRICE_MASTER.Stock_Price_Id=(Select Stock_Price_Id from TSPL_ITEM_Price_master Where Item_Price_Id=" & txtPriceId.Value & ")")
            Else
                ' cbgUOM.DataSource = clsDBFuncationality.GetDataTable("select Cast(Case When ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Price_Id,'')<>'' Then 1 Else 0 End as Bit) as [Select], UOM_Code as UOM, TSPL_ITEM_PRICE_MASTER.Item_Price_Id as PriceId from TSPL_ITEM_UOM_DETAIL LEFT OUTER JOIN TSPL_ITEM_PRICE_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code AND TSPL_ITEM_PRICE_MASTER.Item_Price_Id='' WHERE TSPL_ITEM_UOM_DETAIL.Item_Code='" & txtItemCode.Value & "' AND TSPL_ITEM_UOM_DETAIL.UOM_Code<>'" & strUom & "'")
                cbgUOM.DataSource = clsDBFuncationality.GetDataTable("select Cast(1 as Bit) as [Select], UOM_Code as UOM, TSPL_ITEM_PRICE_MASTER.Item_Price_Id as PriceId from TSPL_ITEM_UOM_DETAIL LEFT OUTER JOIN TSPL_ITEM_PRICE_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code AND TSPL_ITEM_PRICE_MASTER.Item_Price_Id='' WHERE TSPL_ITEM_UOM_DETAIL.Item_Code='" & txtItemCode.Value & "' AND TSPL_ITEM_UOM_DETAIL.UOM_Code<>'" & strUom & "'")
            End If

            If cbgUOM.DataSource IsNot Nothing Then
                If cbgUOM.ColumnCount > 0 Then
                    For ii As Integer = 1 To cbgUOM.ColumnCount - 1
                        cbgUOM.Columns(ii).ReadOnly = True
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function GetObjectValue(ByVal objPM As clsPriceMaster, ByVal index As Integer, ByVal ObjectType As TempEnumTaxObject) As Object
        Dim retVal As New Object
        Select Case index
            Case 0
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX1_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX1
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX1_Amt
                End Select
            Case 1
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX2_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX2
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX2_Amt
                End Select
            Case 2
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX3_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX3
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX3_Amt
                End Select
            Case 3
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX4_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX4
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX4_Amt
                End Select
            Case 4
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX5_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX5
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX5_Amt
                End Select
            Case 5
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX6_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX6
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX6_Amt
                End Select
            Case 6
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX7_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX7
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX7_Amt
                End Select
            Case 7
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX8_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX8
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX8_Amt
                End Select
            Case 8
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX9_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX9
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX9_Amt
                End Select
            Case 9
                Select Case ObjectType
                    Case TempEnumTaxObject.TaxRate
                        retVal = objPM.TAX10_Rate
                    Case TempEnumTaxObject.TaxCode
                        retVal = objPM.TAX10
                    Case TempEnumTaxObject.Amount
                        retVal = objPM.TAX10_Amt
                End Select
            Case Else
                Throw New Exception("Tax can't be more than 10")
        End Select

        Return retVal
    End Function

    Enum TempEnumTaxObject
        TaxRate = 0
        TaxCode = 1
        Amount = 2
    End Enum

    Private Function IsTaxRateExists(ByVal TaxCode As String, ByVal taxRate As Double) As Boolean
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select 1 from TSPL_TAX_RATES where Tax_Code='" + TaxCode + "' and Tax_Type='S' and Tax_Rate='" + clsCommon.myCstr(taxRate) + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Sub MRPWithBackCalculation()
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Item_Code", "Item_Desc", "UOM", "Remarks", "Item_MRP", "Start_Date", "End_Date", "Price_Code", "Price_Comp1", "Price_Comp_Desc1", "Price_Rate1", "Price_Amount1", "Price_Comp2", "Price_Comp_Desc2", "Price_Rate2", "Price_Amount2", "Price_Comp3", "Price_Comp_Desc3", "Price_Rate3", "Price_Amount3", "Price_Comp4", "Price_Comp_Desc4", "Price_Rate4", "Price_Amount4", "Price_Comp5", "Price_Comp_Desc5", "Price_Rate5", "Price_Amount5", "Price_Comp6", "Price_Comp_Desc6", "Price_Rate6", "Price_Amount6", "Price_Comp7", "Price_Comp_Desc7", "Price_Rate7", "Price_Amount7", "Price_Comp8", "Price_Comp_Desc8", "Price_Rate8", "Price_Amount8", "Price_Comp9", "Price_Comp_Desc9", "Price_Rate9", "Price_Amount9", "Price_Comp10", "Price_Comp_Desc10", "Price_Rate10", "Price_Amount10", "Calculate With Tax (Y/N)", "Tax_group", "TAX1", "TAX1_Rate", "TAX1_Amt", "TAX2", "TAX2_Rate", "TAX2_Amt", "TAX3", "TAX3_Rate", "TAX3_Amt", "TAX4", "TAX4_Rate", "TAX4_Amt", "TAX5", "TAX5_Rate", "TAX5_Amt", "TAX6", "TAX6_Rate", "TAX6_Amt", "TAX7", "TAX7_Rate", "TAX7_Amt", "TAX8", "TAX8_Rate", "TAX8_Amt", "TAX9", "TAX9_Rate", "TAX9_Amt", "TAX10", "TAX10_Rate", "TAX10_Amt", "Location_Code", "Type", "Is_Active") Then
            Dim LineNo As String
            Dim count As Integer = 0
            Dim TempTaxRate As Decimal = 0
            Try
                clsCommon.ProgressBarPercentShow()
                Dim Arr As New List(Of clsPriceMaster)
                Dim TotalTaxAmt As Decimal
                Dim TotalMarginAmt As Decimal
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    LineNo = clsCommon.myCstr(dgrv.Index + 2)
                    count += 1
                    clsCommon.ProgressBarPercentUpdate((count * 100) / dgv.RowCount - 1, "Importing " + clsCommon.myCstr(count) + "/" + clsCommon.myCstr(dgv.RowCount - 1))
                    Dim obj As New clsPriceMaster()
                    obj.Item_Code = clsCommon.myCstr(dgrv.Cells("Item_Code").Value)
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        obj.Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code From TSPL_ITem_MASTER WHERE Item_Code='" + obj.Item_Code + "'")
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Throw New Exception("Line " + LineNo + " : Item Code does not exist.")
                        End If

                        obj.UOM = clsCommon.myCstr(dgrv.Cells("UOM").Value)
                        If clsCommon.myLen(obj.UOM) > 0 Then
                            obj.UOM = clsDBFuncationality.getSingleValue("Select UOM_Code from TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + obj.Item_Code + "' AND UOM_Code='" + obj.UOM + "'")
                            If clsCommon.myLen(obj.UOM) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : UOM does not exist for this item.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter UOM.")
                        End If
                        '=====================================added by preeti gupta============================
                        'Dim dtUOMPrice As New DataTable()
                        'dtUOMPrice.Columns.Add("UOM", GetType(String))
                        'dt = clsPriceMaster.GetAllConversionOfItem(clsCommon.myCstr(dgrv.Cells("Item_Code").Value))

                        'obj.Item_Basic_Net = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        'obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value) * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                        '======================================================================================
                        obj.Remarks = clsCommon.myCstr(dgrv.Cells("Remarks").Value)
                        If clsCommon.myLen(obj.Remarks) > 200 Then
                            Throw New Exception("Line " + LineNo + " : Remarks can not be greater than 200.")
                        End If
                        obj.type = clsCommon.myCstr(dgrv.Cells("Type").Value)
                        obj.Is_Active = clsCommon.myCdbl(dgrv.Cells("Is_Active").Value)
                        obj.Item_Basic_Price_Type = "Auto"
                        obj.Price_Category = "Auto" '------------------------------Is Used to check whether With Back Calculation Or Not.
                        obj.Basic_Price_On = "Landing Cost"
                        obj.Landing_Cost = 0.0
                        obj.Purchase_Cost = 0.0
                        '===================preeti gupta=========
                        obj.Item_MRP = clsCommon.myCdbl(dgrv.Cells("Item_MRP").Value)
                        obj.Item_Basic_Net = obj.Item_MRP
                        '======================================================
                        obj.Markup_On = "None"
                        obj.Markup_Percent = 0.0
                        obj.Tax_Manipulation_On = "Basic Price"
                        obj.Can_Edit = "Y"
                        If clsCommon.myLen(dgrv.Cells("Start_Date").Value) <= 0 Then
                            Throw New Exception("Line " + LineNo + " : Start_Date can not be left blank.")
                        End If
                        obj.Start_Date = clsCommon.myCstr(dgrv.Cells("Start_Date").Value)
                        If clsCommon.myLen(dgrv.Cells("End_Date").Value) > 0 Then
                            obj.End_Date = clsCommon.myCstr(dgrv.Cells("End_Date").Value)
                        End If
                        obj.Abatement_Rate = 0
                        Dim dtMaster As DataTable
                        dtMaster = (New BAL.BALPriceComponant).GetAbtMasterOnDate(obj.Start_Date)
                        If (dtMaster.Rows.Count > 0) Then
                            obj.Abatement_Rate = dtMaster.Rows(0)("Rate").ToString()
                        End If
                        obj.Abatement = obj.Item_MRP * obj.Abatement_Rate / 100

                        TotalTaxAmt = 0.0
                        '--------------------------Get Total Tax Rate & Total Margin Amt-------------------------
                        TotalTaxRate = 0.0
                        TotalMarginAmt = 0.0
                        For ii As Integer = 1 To 10
                            TotalTaxRate += clsCommon.myCdbl(dgrv.Cells("TAX" & ii.ToString() & "_Rate").Value)
                            TotalMarginAmt += clsCommon.myCdbl(dgrv.Cells("Price_Amount" & ii.ToString() & "").Value)
                        Next
                        '---------------------------------------------------------------------

                        obj.Item_Basic_Price = Math.Round((obj.Item_MRP - TotalMarginAmt), 5)
                        Dim objTaxGroup As New clsTaxGroupMaster()


                        objTaxGroup = objTaxGroup.GetData(clsCommon.myCstr(dgrv.Cells("Tax_group").Value), obj.type)
                        If objTaxGroup Is Nothing OrElse objTaxGroup.Arr Is Nothing OrElse objTaxGroup.Arr.Count <= 0 Then
                            Throw New Exception("Line " + LineNo + " : Tax Group does not exist.")
                        End If

                        If CalculateTaxRatefromItemwsieTaxOnSale = True Then

                            Dim qry As String = ""
                            Dim dt1 As DataTable = Nothing
                            qry = "select top 1 * from  TSPL_ITEM_WISE_TAX left outer join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_GROUP.HCODE " & _
                     "left outer join TSPL_ITEM_WISE_TAX_AUTHORITY on TSPL_ITEM_WISE_TAX.HCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE " & _
                     "and TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE  " & _
                     "where Status=1 and TSPL_ITEM_WISE_TAX.Type= 'S' and DOC_DATE < ='" & clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy") & "' and Item_Code='" & clsCommon.myCstr(dgrv.Cells("Item_Code").Value) & "' order by DOC_DATE desc "

                            dt1 = clsDBFuncationality.GetDataTable(qry)
                            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                Against_ItemwiseTaxCode = clsCommon.myCstr(dt1.Rows(0)("HCODE"))
                            Else
                                obj.Against_ItemwiseTaxCode = ""
                            End If

                            Dim objTAXRate As clsItemWiseTaxAuthority = Nothing

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX1").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX1").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If

                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX1_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 1 does not match with Item Wise Tax Master.")
                            End If


                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX2").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX2").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX2_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 2 does not match with Item Wise Tax Master.")
                            End If

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX3").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX3").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX3_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 3 does not match with Item Wise Tax Master.")
                            End If

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX4").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX4").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX4_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 4 does not match with Item Wise Tax Master.")
                            End If

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX5").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX5").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX5_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 5 does not match with Item Wise Tax Master.")
                            End If

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX6").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX6").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX6_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 6 does not match with Item Wise Tax Master.")
                            End If

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX7").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX7").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX7_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 7 does not match with Item Wise Tax Master.")
                            End If

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX8").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX8").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX8_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 8 does not match with Item Wise Tax Master.")
                            End If

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX9").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX9").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX9_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 9 does not match with Item Wise Tax Master.")
                            End If

                            objTAXRate = Nothing
                            If clsCommon.myCstr(dgrv.Cells("TAX10").Value) <> "" Then
                                objTAXRate = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(dgrv.Cells("Item_Code").Value), clsCommon.myCstr(dgrv.Cells("Tax_group").Value), clsCommon.myCstr(dgrv.Cells("TAX10").Value), clsCommon.GetPrintDate(dgrv.Cells("Start_Date").Value, "dd/MMM/yyyy"), "S")
                            End If
                            If objTAXRate Is Nothing Then
                                TempTaxRate = 0
                            Else
                                TempTaxRate = clsCommon.myCdbl(objTAXRate.TAX_Rate)
                            End If
                            If clsCommon.myCdbl(TempTaxRate) <> clsCommon.myCdbl(dgrv.Cells("TAX10_Rate").Value) Then
                                Throw New Exception("Line " + LineNo + " : Tax Rate 10 does not match with Item Wise Tax Master.")
                            End If

                        End If

                        obj.TAX1_Rate = clsCommon.myCdbl(dgrv.Cells("TAX1_Rate").Value)
                        obj.TAX2_Rate = clsCommon.myCdbl(dgrv.Cells("TAX2_Rate").Value)
                        obj.TAX3_Rate = clsCommon.myCdbl(dgrv.Cells("TAX3_Rate").Value)
                        obj.TAX4_Rate = clsCommon.myCdbl(dgrv.Cells("TAX4_Rate").Value)
                        obj.TAX5_Rate = clsCommon.myCdbl(dgrv.Cells("TAX5_Rate").Value)
                        obj.TAX6_Rate = clsCommon.myCdbl(dgrv.Cells("TAX6_Rate").Value)
                        obj.TAX7_Rate = clsCommon.myCdbl(dgrv.Cells("TAX7_Rate").Value)
                        obj.TAX8_Rate = clsCommon.myCdbl(dgrv.Cells("TAX8_Rate").Value)
                        obj.TAX9_Rate = clsCommon.myCdbl(dgrv.Cells("TAX9_Rate").Value)
                        obj.TAX10_Rate = clsCommon.myCdbl(dgrv.Cells("TAX10_Rate").Value)

                        obj.Tax_group = objTaxGroup.Tax_Group_Code
                        Dim dblTotalNonTabxableRate As Double = 0
                        For ii As Integer = 0 To objTaxGroup.Arr.Count - 1
                            Dim objTaxGroupTR As clsTaxGroupDetail = objTaxGroup.Arr(ii)
                            If Not objTaxGroupTR.Taxable Then
                                If objTaxGroupTR.Excisable Then
                                    Throw New Exception("Line " + LineNo + " : Wrong configuration of Tax Group-" + objTaxGroupTR.Tax_Group_Code + " Tax authority-" + objTaxGroupTR.Tax_Code + ".A Tax can't be Excisable and non Taxable ")
                                End If
                                dblTotalNonTabxableRate = dblTotalNonTabxableRate + clsCommon.myCdbl(GetObjectValue(obj, ii, TempEnumTaxObject.TaxRate))
                            End If
                            Select Case ii
                                Case 0
                                    obj.TAX1 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX1_Rate) Then
                                        Throw New Exception("Line " + LineNo + " : Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX1_Rate) + " Not exists ")
                                    End If
                                Case 1
                                    obj.TAX2 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX2_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX2_Rate) + " Not exists ")
                                    End If
                                Case 2
                                    obj.TAX3 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX3_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX3_Rate) + " Not exists ")
                                    End If
                                Case 3
                                    obj.TAX4 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX4_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX4_Rate) + " Not exists ")
                                    End If
                                Case 4
                                    obj.TAX5 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX5_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX5_Rate) + " Not exists ")
                                    End If
                                Case 5
                                    obj.TAX6 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX6_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX6_Rate) + " Not exists ")
                                    End If
                                Case 6
                                    obj.TAX7 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX7_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX7_Rate) + " Not exists ")
                                    End If
                                Case 7
                                    obj.TAX8 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX8_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX8_Rate) + " Not exists ")
                                    End If
                                Case 8
                                    obj.TAX9 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX9_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX9_Rate) + " Not exists ")
                                    End If
                                Case 9
                                    obj.TAX10 = objTaxGroupTR.Tax_Code
                                    If Not IsTaxRateExists(objTaxGroupTR.Tax_Code, obj.TAX10_Rate) Then
                                        Throw New Exception("Line " + LineNo + " :  Tax authority " + objTaxGroupTR.Tax_Code + " Tax Rate " + clsCommon.myCstr(obj.TAX10_Rate) + " Not exists ")
                                    End If
                            End Select
                        Next

                        ''richa agarwal 23 Aug,2019 TEC/30/07/19-000971
                        Dim dblTotalNonTabxableAmount As Double = 0
                        If clsCommon.CompairString(clsCommon.myCstr(dgrv.Cells("Calculate With Tax (Y/N)").Value), "Y") = CompairStringResult.Equal Then
                            dblTotalNonTabxableAmount = (obj.Item_Basic_Price * 100) / (100 + dblTotalNonTabxableRate)
                        Else
                            dblTotalNonTabxableAmount = obj.Item_Basic_Price
                        End If

                        For ii As Integer = 0 To objTaxGroup.Arr.Count - 1
                            Dim objTaxGroupTR As clsTaxGroupDetail = objTaxGroup.Arr(ii)
                            Dim dblTaxableAmount As Double = 0
                            If objTaxGroupTR.Surtax Then
                                For jj As Integer = 0 To ii - 1
                                    If clsCommon.CompairString(objTaxGroupTR.Surtax_Tax_Code, clsCommon.myCstr(GetObjectValue(obj, jj, TempEnumTaxObject.TaxCode))) = CompairStringResult.Equal Then
                                        dblTaxableAmount += clsCommon.myCdbl(GetObjectValue(obj, jj, TempEnumTaxObject.Amount))
                                    End If
                                Next
                            ElseIf objTaxGroupTR.Excisable Then
                                dblTaxableAmount = obj.Abatement
                            Else
                                dblTaxableAmount = dblTotalNonTabxableAmount
                            End If
                            Dim dblCalTaxAmt As Double = Math.Round(((dblTaxableAmount * GetObjectValue(obj, ii, TempEnumTaxObject.TaxRate)) / 100), 5, MidpointRounding.ToEven)
                            Select Case ii
                                Case 0
                                    obj.TAX1_Amt = dblCalTaxAmt
                                Case 1
                                    obj.TAX2_Amt = dblCalTaxAmt
                                Case 2
                                    obj.TAX3_Amt = dblCalTaxAmt
                                Case 3
                                    obj.TAX4_Amt = dblCalTaxAmt
                                Case 4
                                    obj.TAX5_Amt = dblCalTaxAmt
                                Case 5
                                    obj.TAX6_Amt = dblCalTaxAmt
                                Case 6
                                    obj.TAX7_Amt = dblCalTaxAmt
                                Case 7
                                    obj.TAX8_Amt = dblCalTaxAmt
                                Case 8
                                    obj.TAX9_Amt = dblCalTaxAmt
                                Case 9
                                    obj.TAX10_Amt = dblCalTaxAmt
                            End Select
                        Next

                        obj.Price_Code = clsCommon.myCstr(dgrv.Cells("Price_Code").Value)
                        If clsCommon.myLen(obj.Price_Code) > 0 Then
                            obj.Price_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Price_Code)  from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" + obj.Price_Code + "'"))
                            If clsCommon.myLen(obj.Price_Code) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Price Code does not exist.Please Create This Price Code In Price Component Mapping.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Price Code.")
                        End If
                        '=================================Added by Preeti Gupta========================
                        obj.Location_Code = clsCommon.myCstr(dgrv.Cells("Location_Code").Value)
                        If clsCommon.myLen(obj.Location_Code) > 0 Then
                            obj.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from tspl_location_master WHERE Location_Code='" + obj.Location_Code + "'"))
                            If clsCommon.myLen(obj.Location_Code) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Location Code does not exist.Please Create This Location Code In Location Master.")
                            End If
                        Else
                            Throw New Exception("Line " + LineNo + " : Enter Location Code.")
                        End If

                        '===============================================================================
                        '---------Price Component-----------------------------------------------------------------------------

                        obj.Price_Comp1 = clsCommon.myCstr(dgrv.Cells("Price_Comp1").Value)
                        If clsCommon.myLen(obj.Price_Comp1) > 0 Then
                            obj.Price_Comp1 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp1 & "'")
                            If clsCommon.myLen(obj.Price_Comp1) <= 0 Then
                                Throw New Exception("Line " + LineNo + " : Price_Comp1 does not mapped with Price Code : " & obj.Price_Code & ".")
                            End If
                            obj.Price_Comp_Desc1 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc1").Value)
                            obj.Price_Rate1 = clsCommon.myCdbl(dgrv.Cells("Price_Rate1").Value)
                            obj.Price_Amount1 = clsCommon.myCdbl(dgrv.Cells("Price_Amount1").Value)
                            'TotalMarginAmt += obj.Price_Amount1 '----------------------------------------Price_Component_amt1 is Added.
                            obj.Price_Comp2 = clsCommon.myCstr(dgrv.Cells("Price_Comp2").Value)


                            If clsCommon.myLen(obj.Price_Comp2) > 0 Then
                                obj.Price_Comp2 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp2 & "'")
                                If clsCommon.myLen(obj.Price_Comp2) <= 0 Then
                                    Throw New Exception("Line " + LineNo + " : Price_Comp2 does not mapped with Price Code : " & obj.Price_Code & ".")
                                End If
                                obj.Price_Comp_Desc2 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc2").Value)
                                obj.Price_Rate2 = clsCommon.myCdbl(dgrv.Cells("Price_Rate2").Value)
                                obj.Price_Amount2 = clsCommon.myCdbl(dgrv.Cells("Price_Amount2").Value)
                                'TotalMarginAmt += obj.Price_Amount2 '----------------------------------------Price_Component_amt2 is Added.
                                obj.Price_Comp3 = clsCommon.myCstr(dgrv.Cells("Price_Comp3").Value)
                                If clsCommon.myLen(obj.Price_Comp3) > 0 Then
                                    obj.Price_Comp3 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp3 & "'")
                                    If clsCommon.myLen(obj.Price_Comp3) <= 0 Then
                                        Throw New Exception("Line " + LineNo + " : Price_Comp3 does not mapped with Price Code : " & obj.Price_Code & ".")
                                    End If
                                    obj.Price_Comp_Desc3 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc3").Value)
                                    obj.Price_Rate3 = clsCommon.myCdbl(dgrv.Cells("Price_Rate3").Value)
                                    obj.Price_Amount3 = clsCommon.myCdbl(dgrv.Cells("Price_Amount3").Value)
                                    'TotalMarginAmt += obj.Price_Amount3 '----------------------------------------Price_Component_amt3 is Added.
                                    obj.Price_Comp4 = clsCommon.myCstr(dgrv.Cells("Price_Comp4").Value)
                                    If clsCommon.myLen(obj.Price_Comp4) > 0 Then
                                        obj.Price_Comp4 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp4 & "'")
                                        If clsCommon.myLen(obj.Price_Comp4) <= 0 Then
                                            Throw New Exception("Line " + LineNo + " : Price_Comp4 does not mapped with Price Code : " & obj.Price_Code & ".")
                                        End If
                                        obj.Price_Comp_Desc4 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc4").Value)
                                        obj.Price_Rate4 = clsCommon.myCdbl(dgrv.Cells("Price_Rate4").Value)
                                        obj.Price_Amount4 = clsCommon.myCdbl(dgrv.Cells("Price_Amount4").Value)
                                        'TotalMarginAmt += obj.Price_Amount4 '----------------------------------------Price_Component_amt4 is Added.
                                        obj.Price_Comp5 = clsCommon.myCstr(dgrv.Cells("Price_Comp5").Value)
                                        If clsCommon.myLen(obj.Price_Comp5) > 0 Then
                                            obj.Price_Comp5 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp5 & "'")
                                            If clsCommon.myLen(obj.Price_Comp5) <= 0 Then
                                                Throw New Exception("Line " + LineNo + " : Price_Comp5 does not mapped with Price Code : " & obj.Price_Code & ".")
                                            End If
                                            obj.Price_Comp_Desc5 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc5").Value)
                                            obj.Price_Rate5 = clsCommon.myCdbl(dgrv.Cells("Price_Rate5").Value)
                                            obj.Price_Amount5 = clsCommon.myCdbl(dgrv.Cells("Price_Amount5").Value)
                                            'TotalMarginAmt += obj.Price_Amount5 '----------------------------------------Price_Component_amt5 is Added.
                                            obj.Price_Comp6 = clsCommon.myCstr(dgrv.Cells("Price_Comp6").Value)
                                            If clsCommon.myLen(obj.Price_Comp6) > 0 Then
                                                obj.Price_Comp6 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp6 & "'")
                                                If clsCommon.myLen(obj.Price_Comp6) <= 0 Then
                                                    Throw New Exception("Line " + LineNo + " : Price_Comp6 does not mapped with Price Code : " & obj.Price_Code & ".")
                                                End If
                                                obj.Price_Comp_Desc6 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc6").Value)
                                                obj.Price_Rate6 = clsCommon.myCdbl(dgrv.Cells("Price_Rate6").Value)
                                                obj.Price_Amount6 = clsCommon.myCdbl(dgrv.Cells("Price_Amount6").Value)
                                                '   TotalMarginAmt += obj.Price_Amount6 '----------------------------------------Price_Component_amt6 is Added.
                                                obj.Price_Comp7 = clsCommon.myCstr(dgrv.Cells("Price_Comp7").Value)
                                                If clsCommon.myLen(obj.Price_Comp7) > 0 Then
                                                    obj.Price_Comp7 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp7 & "'")
                                                    If clsCommon.myLen(obj.Price_Comp7) <= 0 Then
                                                        Throw New Exception("Line " + LineNo + " : Price_Comp7 does not mapped with Price Code : " & obj.Price_Code & ".")
                                                    End If
                                                    obj.Price_Comp_Desc7 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc7").Value)
                                                    obj.Price_Rate7 = clsCommon.myCdbl(dgrv.Cells("Price_Rate7").Value)
                                                    obj.Price_Amount7 = clsCommon.myCdbl(dgrv.Cells("Price_Amount7").Value)
                                                    '      TotalMarginAmt += obj.Price_Amount7 '----------------------------------------Price_Component_amt7 is Added.
                                                    obj.Price_Comp8 = clsCommon.myCstr(dgrv.Cells("Price_Comp6").Value)
                                                    If clsCommon.myLen(obj.Price_Comp8) > 0 Then
                                                        obj.Price_Comp8 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp8 & "'")
                                                        If clsCommon.myLen(obj.Price_Comp8) <= 0 Then
                                                            Throw New Exception("Line " + LineNo + " : Price_Comp8 does not mapped with Price Code : " & obj.Price_Code & ".")
                                                        End If
                                                        obj.Price_Comp_Desc8 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc8").Value)
                                                        obj.Price_Rate8 = clsCommon.myCdbl(dgrv.Cells("Price_Rate8").Value)
                                                        obj.Price_Amount8 = clsCommon.myCdbl(dgrv.Cells("Price_Amount8").Value)
                                                        'TotalMarginAmt += obj.Price_Amount8 '----------------------------------------Price_Component_amt8 is Added.
                                                        obj.Price_Comp9 = clsCommon.myCstr(dgrv.Cells("Price_Comp9").Value)
                                                        If clsCommon.myLen(obj.Price_Comp9) > 0 Then
                                                            obj.Price_Comp9 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp9 & "'")
                                                            If clsCommon.myLen(obj.Price_Comp9) <= 0 Then
                                                                Throw New Exception("Line " + LineNo + " : Price_Comp9 does not mapped with Price Code : " & obj.Price_Code & ".")
                                                            End If
                                                            obj.Price_Comp_Desc9 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc9").Value)
                                                            obj.Price_Rate9 = clsCommon.myCdbl(dgrv.Cells("Price_Rate9").Value)
                                                            obj.Price_Amount9 = clsCommon.myCdbl(dgrv.Cells("Price_Amount9").Value)
                                                            'TotalMarginAmt += obj.Price_Amount9 '----------------------------------------Price_Component_amt9 is Added.
                                                            obj.Price_Comp10 = clsCommon.myCstr(dgrv.Cells("Price_Comp10").Value)
                                                            If clsCommon.myLen(obj.Price_Comp10) > 0 Then
                                                                obj.Price_Comp10 = clsDBFuncationality.getSingleValue("select Price_Comp_Code from TSPL_PRICE_COMPONENT_MAPPING WHERE Price_Code='" & obj.Price_Code & "' AND Price_Comp_Code='" & obj.Price_Comp10 & "'")
                                                                If clsCommon.myLen(obj.Price_Comp10) <= 0 Then
                                                                    Throw New Exception("Line " + LineNo + " : Price_Comp10 does not mapped with Price Code : " & obj.Price_Code & ".")
                                                                End If
                                                                obj.Price_Comp_Desc10 = clsCommon.myCstr(dgrv.Cells("Price_Comp_Desc10").Value)
                                                                obj.Price_Rate10 = clsCommon.myCdbl(dgrv.Cells("Price_Rate10").Value)
                                                                obj.Price_Amount10 = clsCommon.myCdbl(dgrv.Cells("Price_Amount10").Value)
                                                                'TotalMarginAmt += obj.Price_Amount10 '----------------------------------------Price_Component_amt10 is Added.
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        '-------------------
                        obj.Item_Selling_Price = obj.Item_Basic_Price
                        obj.Is_With_Tax = clsCommon.myCstr(dgrv.Cells("Calculate With Tax (Y/N)").Value)
                        If clsCommon.CompairString(obj.Is_With_Tax, "Y") = CompairStringResult.Equal Then
                            obj.Item_Selling_Price = obj.Item_Basic_Price - (obj.TAX1_Amt + obj.TAX2_Amt + obj.TAX3_Amt + obj.TAX4_Amt + obj.TAX5_Amt + obj.TAX6_Amt + obj.TAX7_Amt + obj.TAX8_Amt + obj.TAX9_Amt + obj.TAX10_Amt)
                        Else
                            obj.Is_With_Tax = "N"
                        End If

                        obj.IsNewEntry = True
                        Arr.Add(obj)
                    End If
                Next
                If (clsPriceMaster.SaveData(Arr, False)) Then
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub btnMapCommission_Click(sender As Object, e As EventArgs) Handles btnMapCommission.Click
        Dim BasicRate As Decimal = Double.Parse(IIf(txtBasic.Text = "", "0", txtBasic.Text))
        If BasicRate > 0 Then
            Dim obj As New FrmPOSCommissionMapping()
            obj.BASIC_RATE = BasicRate
            obj.ITEM_PRICE_ID = Integer.Parse(IIf(txtPriceId.Value = "", "0", txtPriceId.Value))
            obj.StartPosition = FormStartPosition.CenterParent


            obj.ShowDialog()
        Else
            clsCommon.MyMessageBoxShow("Please Enter Basic Price")
        End If
    End Sub

    Private Sub btnUnpost_Click(sender As Object, e As EventArgs) Handles btnUnpost.Click
        Try
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                clsPriceMaster.UnPostData(txtPriceId.Value)
                clsCommon.MyMessageBoxShow("Unposted successfully", Me.Text)
                LoadData(txtPriceId.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles BtnHistory.Click
        Try
            If clsCommon.myLen(txtPriceId.Value) <= 0 Then
                Throw New Exception("Code not found to show history")
                txtPriceId.Focus()
            End If
            If clsCommon.myLen(txtPriceId.Value) > 0 Then
                clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(txtPriceId.Value), "Item_Price_Id", "TSPL_ITEM_PRICE_MASTER")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkTaxInclusive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBackCalWithTAX.ToggleStateChanged
        If chkWithoutTax.Checked = True Then
            chkBackCalWithTAX.Checked = False
        Else
            chkWithoutTax.Checked = False
        End If
        If chkWithoutTax.Checked = True OrElse chkBackCalWithTAX.Checked = True Then
            txtMRP.Enabled = True
        Else
            txtMRP.Enabled = False
        End If

        UpdateAllTotal()
        CalculateTotalTAXAmt()
        FillTaxAmounts()
    End Sub

    Private Sub chkWithoutTax_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkWithoutTax.ToggleStateChanged
        If chkWithoutTax.Checked = True Then
            chkBackCalWithTAX.Checked = False
        Else
            chkWithoutTax.Checked = False
        End If
        If chkWithoutTax.Checked = True OrElse chkBackCalWithTAX.Checked = True Then
            txtMRP.Enabled = True
        Else
            txtMRP.Enabled = False
        End If
        UpdateAllTotal()
        CalculateTotalTAXAmt()
        FillTaxAmounts()
    End Sub



End Class

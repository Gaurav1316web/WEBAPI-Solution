'========BM00000003027,Updated By Rohit july 1,2014 on 6:10 PM.(Remark :Add Export to Excel options.)====
Imports System.Data.SqlClient
Imports Common


Public Class FrmPriceMasterExport
    Dim Sql As String = ""
    Dim vendrcode As String = ""
    Dim prcecode As String = ""
    Public IsForBackCalculation As Boolean = False
    '================================Added by Rohit=================
    Public isExportPriceCode As Boolean = False
    Public sql_price As String = ""
    '==============================================================
    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub SplitContainer2_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer2.Panel2.Paint

    End Sub

    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub rbtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnExport.Click
        Try
            If gv1.Rows.Count > 0 Then

                'transportSql.ExporttoExcelWithoutFilter(Sql, "", "", Me)
                clsCommon.MyExportToExcelGrid(Nothing, gv1, Nothing, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data found To Export", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnRefresh.Click
        Try
            If Not IsForBackCalculation Then
                If radioAll.IsChecked Then
                    Sql = "Select TSPL_ITEM_PRICE_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, UOM,TSPL_ITEM_UOM_DETAIL.conversion_factor as [Conversion Factor], TSPL_ITEM_PRICE_MASTER.Remarks, TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type, TSPL_ITEM_PRICE_MASTER.Basic_Price_on, Landing_Cost, Purchase_Cost, TSPL_ITEM_PRICE_MASTER.Item_MRP, Item_Basic_Price, Item_Basic_Net, Markup_On, Markup_Percent, Tax_Manipulation_On, convert(date,Start_Date,103) as Start_Date, case when End_Date is null then '' else convert (varchar,  End_Date,103) end as End_Date, Tax_group, TAX1, TAX1_Rate, TAX1_Amt, TAX2, TAX2_Rate, TAX2_Amt, TAX3, TAX3_Rate, TAX3_Amt, TAX4, TAX4_Rate, TAX4_Amt, TAX5, TAX5_Rate, TAX5_Amt, TAX6, TAX6_Rate, TAX6_Amt, TAX7, TAX7_Rate, TAX7_Amt, TAX8, TAX8_Rate, TAX8_Amt, TAX9, TAX9_Rate, TAX9_Amt, TAX10, TAX10_Rate, TAX10_Amt, TSPL_ITEM_PRICE_MASTER.Price_Code, Abatement_Rate, Abatement,TSPL_ITEM_MASTER.ITF_CODE,TSPL_ITEM_PRICE_MASTER.Is_Active  from TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_item_master.item_code and TSPL_ITEM_UOM_DETAIL.uom_code=tspl_item_master.unit_code where 2=2 " + prcecode + ""
                ElseIf radioLandingCostCst.IsChecked Then
                    Sql = "Select TSPL_ITEM_PRICE_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, UOM,TSPL_ITEM_UOM_DETAIL.conversion_factor as [Conversion Factor], TSPL_ITEM_PRICE_MASTER.Remarks, TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type, TSPL_ITEM_PRICE_MASTER.Basic_Price_on, Landing_Cost, Purchase_Cost, TSPL_ITEM_PRICE_MASTER.Item_MRP, Item_Basic_Price, Item_Basic_Net, Markup_On, Markup_Percent, Tax_Manipulation_On, convert(varchar,Start_Date,103) as Start_Date, case when End_Date is null then '' else convert (varchar,  End_Date,103) end as End_Date, Tax_group, TAX1, TAX1_Rate, TAX1_Amt, TAX2, TAX2_Rate, TAX2_Amt, TAX3, TAX3_Rate, TAX3_Amt, TAX4, TAX4_Rate, TAX4_Amt, TAX5, TAX5_Rate, TAX5_Amt, TAX6, TAX6_Rate, TAX6_Amt, TAX7, TAX7_Rate, TAX7_Amt, TAX8, TAX8_Rate, TAX8_Amt, TAX9, TAX9_Rate, TAX9_Amt, TAX10, TAX10_Rate, TAX10_Amt, TSPL_ITEM_PRICE_MASTER.Price_Code, Abatement_Rate, Abatement,'' as [Landing Cost-CST],TSPL_ITEM_MASTER.ITF_CODE,TSPL_ITEM_PRICE_MASTER.Is_Active  from TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_item_master.item_code and TSPL_ITEM_UOM_DETAIL.uom_code=tspl_item_master.unit_code where Markup_On='Landing Cost' and Tax_group='CST' " + prcecode + ""
                ElseIf radioLandingCostVat.IsChecked Then
                    Sql = "Select TSPL_ITEM_PRICE_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, UOM,TSPL_ITEM_UOM_DETAIL.conversion_factor as [Conversion Factor], TSPL_ITEM_PRICE_MASTER.Remarks, TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type, TSPL_ITEM_PRICE_MASTER.Basic_Price_on, Landing_Cost, Purchase_Cost, TSPL_ITEM_PRICE_MASTER.Item_MRP, Item_Basic_Price, Item_Basic_Net, Markup_On, Markup_Percent, Tax_Manipulation_On, convert(varchar,Start_Date,103) as Start_Date, case when End_Date is null then '' else convert (varchar,  End_Date,103) end as End_Date, Tax_group, TAX1, TAX1_Rate, TAX1_Amt, TAX2, TAX2_Rate, TAX2_Amt, TAX3, TAX3_Rate, TAX3_Amt, TAX4, TAX4_Rate, TAX4_Amt, TAX5, TAX5_Rate, TAX5_Amt, TAX6, TAX6_Rate, TAX6_Amt, TAX7, TAX7_Rate, TAX7_Amt, TAX8, TAX8_Rate, TAX8_Amt, TAX9, TAX9_Rate, TAX9_Amt, TAX10, TAX10_Rate, TAX10_Amt, TSPL_ITEM_PRICE_MASTER.Price_Code, Abatement_Rate, Abatement,'' as [Landing Cost-VAT],TSPL_ITEM_MASTER.ITF_CODE,TSPL_ITEM_PRICE_MASTER.Is_Active  from TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_item_master.item_code and TSPL_ITEM_UOM_DETAIL.uom_code=tspl_item_master.unit_code where Markup_On='Landing Cost' and Tax_group='VAT' " + prcecode + ""
                ElseIf radioMrpCst.IsChecked Then
                    Sql = "Select TSPL_ITEM_PRICE_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, UOM,TSPL_ITEM_UOM_DETAIL.conversion_factor as [Conversion Factor], TSPL_ITEM_PRICE_MASTER.Remarks, TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type, TSPL_ITEM_PRICE_MASTER.Basic_Price_on, Landing_Cost, Purchase_Cost, TSPL_ITEM_PRICE_MASTER.Item_MRP, Item_Basic_Price, Item_Basic_Net, Markup_On, Markup_Percent, Tax_Manipulation_On, convert(varchar,Start_Date,103) as Start_Date, case when End_Date is null then '' else convert (varchar,  End_Date,103) end as End_Date, Tax_group, TAX1, TAX1_Rate, TAX1_Amt, TAX2, TAX2_Rate, TAX2_Amt, TAX3, TAX3_Rate, TAX3_Amt, TAX4, TAX4_Rate, TAX4_Amt, TAX5, TAX5_Rate, TAX5_Amt, TAX6, TAX6_Rate, TAX6_Amt, TAX7, TAX7_Rate, TAX7_Amt, TAX8, TAX8_Rate, TAX8_Amt, TAX9, TAX9_Rate, TAX9_Amt, TAX10, TAX10_Rate, TAX10_Amt, TSPL_ITEM_PRICE_MASTER.Price_Code, Abatement_Rate, Abatement,'' as [MRP-CST],TSPL_ITEM_MASTER.ITF_CODE,TSPL_ITEM_PRICE_MASTER.Is_Active  from TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_item_master.item_code and TSPL_ITEM_UOM_DETAIL.uom_code=tspl_item_master.unit_code where Markup_On='MRP' AND Tax_group='CST' " + prcecode + ""
                ElseIf radioMrpVat.IsChecked Then
                    Sql = "Select TSPL_ITEM_PRICE_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, UOM,TSPL_ITEM_UOM_DETAIL.conversion_factor as [Conversion Factor], TSPL_ITEM_PRICE_MASTER.Remarks, TSPL_ITEM_PRICE_MASTER.Item_Basic_Price_Type, TSPL_ITEM_PRICE_MASTER.Basic_Price_on, Landing_Cost, Purchase_Cost, TSPL_ITEM_PRICE_MASTER.Item_MRP, Item_Basic_Price, Item_Basic_Net, Markup_On, Markup_Percent, Tax_Manipulation_On, convert(varchar,Start_Date,103) as Start_Date, case when End_Date is null then '' else convert (varchar,  End_Date,103) end as End_Date, Tax_group, TAX1, TAX1_Rate, TAX1_Amt, TAX2, TAX2_Rate, TAX2_Amt, TAX3, TAX3_Rate, TAX3_Amt, TAX4, TAX4_Rate, TAX4_Amt, TAX5, TAX5_Rate, TAX5_Amt, TAX6, TAX6_Rate, TAX6_Amt, TAX7, TAX7_Rate, TAX7_Amt, TAX8, TAX8_Rate, TAX8_Amt, TAX9, TAX9_Rate, TAX9_Amt, TAX10, TAX10_Rate, TAX10_Amt, TSPL_ITEM_PRICE_MASTER.Price_Code, Abatement_Rate, Abatement,'' as [MRP-VAT],TSPL_ITEM_MASTER.ITF_CODE,TSPL_ITEM_PRICE_MASTER.Is_Active  from TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_item_master.item_code and TSPL_ITEM_UOM_DETAIL.uom_code=tspl_item_master.unit_code where Markup_On='MRP' AND Tax_group='VAT' " + prcecode + ""
                End If
            Else
                '------Price With Back Calculation---------------------------
                Sql = "Select TSPL_ITEM_PRICE_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, UOM, TSPL_ITEM_PRICE_MASTER.Remarks, TSPL_ITEM_PRICE_MASTER.Item_MRP, convert(varchar,Start_Date,103) as Start_Date, case when End_Date is null then '' else convert (varchar,  End_Date,103) end as End_Date," & _
                " TSPL_ITEM_PRICE_MASTER.Price_Code, Price_Comp1, Price_Comp_Desc1, Price_Rate1, Price_Amount1, Price_Comp2, Price_Comp_Desc2, Price_Rate2, Price_Amount2, Price_Comp3, Price_Comp_Desc3, Price_Rate3, Price_Amount3, Price_Comp4, Price_Comp_Desc4, Price_Rate4, Price_Amount4, Price_Comp5, Price_Comp_Desc5, Price_Rate5, Price_Amount5, Price_Comp6, Price_Comp_Desc6, Price_Rate6, Price_Amount6, Price_Comp7, Price_Comp_Desc7, Price_Rate7, Price_Amount7, Price_Comp8, Price_Comp_Desc8, Price_Rate8, Price_Amount8, Price_Comp9, Price_Comp_Desc9, Price_Rate9, Price_Amount9, Price_Comp10, Price_Comp_Desc10, Price_Rate10, Price_Amount10, " & _
                " Is_With_Tax as [Calculate With Tax (Y/N)], Tax_group, TAX1, TAX1_Rate, TAX1_Amt, TAX2, TAX2_Rate, TAX2_Amt, TAX3, TAX3_Rate, TAX3_Amt, TAX4, TAX4_Rate, TAX4_Amt, TAX5, TAX5_Rate, TAX5_Amt, TAX6, TAX6_Rate, TAX6_Amt, TAX7, TAX7_Rate, TAX7_Amt, TAX8, TAX8_Rate, TAX8_Amt, TAX9, TAX9_Rate, TAX9_Amt, TAX10, TAX10_Rate, TAX10_Amt,TSPL_ITEM_PRICE_MASTER.Location_Code,case when PriceMapping.Transfer =1 then 'T' else 'S' end as Type,TSPL_ITEM_PRICE_MASTER.Is_Active " & _
                " from TSPL_ITEM_PRICE_MASTER" & _
                " LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code" & _
                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_item_master.item_code and TSPL_ITEM_UOM_DETAIL.uom_code=tspl_item_master.unit_code " & _
                " right join (select distinct TSPL_PRICE_COMPONENT_MAPPING.Price_Code,TSPL_PRICE_COMPONENT_MAPPING.Transfer    from TSPL_PRICE_COMPONENT_MAPPING )as PriceMapping" & _
                " on PriceMapping.Price_Code =TSPL_item_price_master.Price_Code  where Price_Category='Auto'"
            End If
            gv1.DataSource = clsDBFuncationality.GetDataTable(Sql)
            gv1.BestFitColumns()
            gv1.EnableFiltering = True
            'transportSql.ExporttoExcel(Sql, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmPriceMasterExport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.StartPosition = FormStartPosition.CenterParent
        rbtnRefresh.Visible = False
        gv1.DataSource = Nothing
        radioAll.IsChecked = Not IsForBackCalculation
        chkBackCalculation.IsChecked = IsForBackCalculation
        Panel1.Visible = Not IsForBackCalculation
        If Not IsForBackCalculation Then
            If isExportPriceCode = False Then
                '------------------given selection of principle and price code before export------------------------------------------------
                Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,(Add1+' '+Add2+' '+Add3) as Address,Vendor_Group_Code as [Vendor Group],Vendor_Group_Code_Desc as [Group Description],City_Code_Desc as City,State,(Phone1+' '+Phone2) as Telephone,Contact_Person_Name as [Contact Person],Contact_Person_Phone as [Contact No.] from TSPL_VENDOR_MASTER"

                vendrcode = clsCommon.ShowSelectForm("VNDFND", qry, "Code", "", vendrcode, "Code", True)

                If clsCommon.myLen(vendrcode) <= 0 Then
                    If Not clsCommon.MyMessageBoxShow(Me, "Are You Sure,Want To Export Without Any Principle Code/Price Code Filter Selection?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Return
                    End If
                Else
                    qry = "Select Distinct Price_Code as Code, Price_Code_Desc as [Description]  from TSPL_PRICE_COMPONENT_MAPPING "
                    prcecode = clsCommon.ShowSelectForm("PriceCode@PriceMaster", qry, "Code", " vendor_code='" + vendrcode + "'", prcecode, "", True)
                    If clsCommon.myLen(prcecode) <= 0 Then
                        If Not clsCommon.MyMessageBoxShow(Me, "Are You Sure,Want To Export Without Any Price Code Filter Selection?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            Return
                        Else
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " where vendor_code='" + vendrcode + "'")
                            Dim strcode As String = ""
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                For Each dr As DataRow In dt.Rows()
                                    strcode = strcode + "','" + clsCommon.myCstr(dr("Code"))
                                Next
                            End If

                            Try
                                If strcode.Substring(0, 2) = "'," Then
                                    strcode = strcode.Substring(2, strcode.Length - 2)
                                End If
                            Catch exx As Exception
                            End Try
                            If clsCommon.myLen(strcode) > 0 Then
                                prcecode = " and TSPL_ITEM_PRICE_MASTER.price_code in (" + strcode + "')"
                            Else
                                prcecode = ""
                            End If
                        End If
                    Else
                        vendrcode = " and TSPL_PRICE_COMPONENT_MAPPING.vendor_code='" + vendrcode + "'"
                        prcecode = " and TSPL_ITEM_PRICE_MASTER.price_code='" + prcecode + "'"
                    End If
                End If
                '-------------------------------------------------------------------------------------
            Else
                '================Added By Rohit==============================================================
                Dim frmFilter As New frmFilterToExport()
                frmFilter.qry = sql_price
                frmFilter.whrCls = " Where 2=2 "
                frmFilter.ShowDialog()
                If frmFilter.isCancel Then
                    ' Dim ee As System.EventArgs
                    rbtnClose_Click("", Nothing)
                End If
                sql_price = frmFilter.qry

                'Dim qry As String = "Select Distinct Price_Code as Code, Price_Code_Desc as [Description]  from TSPL_PRICE_COMPONENT_MAPPING "
                'prcecode = clsCommon.ShowSelectForm("PriceCode@PriceMaster", qry, "Code", "", prcecode, "", True)
                'If clsCommon.myLen(prcecode) <= 0 Then
                '    If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Export Without Any Price Code Filter Selection?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                '        Return
                '    Else
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql_price)
                Dim strcode As String = ""
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows()
                        strcode = strcode + "','" + clsCommon.myCstr(dr("Code"))
                    Next
                End If

                Try
                    If strcode.Substring(0, 2) = "'," Then
                        strcode = strcode.Substring(2, strcode.Length - 2)
                    End If
                Catch exx As Exception
                End Try
                If clsCommon.myLen(strcode) > 0 Then
                    prcecode = " and TSPL_ITEM_PRICE_MASTER.price_code in (" + strcode + "')"
                Else
                    prcecode = ""
                End If
                'End If
                '    Else
                '    prcecode = " and TSPL_ITEM_PRICE_MASTER.price_code='" + prcecode + "'"
                'End If
                '=======================================================================================
            End If
        End If

        rbtnRefresh_Click(sender, e)
        Sql = ""
    End Sub

    Private Sub radioReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioReset.Click
        gv1.DataSource = Nothing
        radioAll.IsChecked = True
        rbtnRefresh_Click(sender, e)
        Sql = ""
    End Sub

    Private Sub radioAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles radioAll.ToggleStateChanged
        rbtnRefresh_Click(sender, args)
    End Sub

    Private Sub radioLandingCostCst_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles radioLandingCostCst.ToggleStateChanged
        rbtnRefresh_Click(sender, args)
    End Sub

    Private Sub radioLandingCostVat_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles radioLandingCostVat.ToggleStateChanged
        rbtnRefresh_Click(sender, args)
    End Sub

    Private Sub radioMrpCst_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles radioMrpCst.ToggleStateChanged
        rbtnRefresh_Click(sender, args)
    End Sub

    Private Sub radioMrpVat_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles radioMrpVat.ToggleStateChanged
        rbtnRefresh_Click(sender, args)
    End Sub

    
End Class

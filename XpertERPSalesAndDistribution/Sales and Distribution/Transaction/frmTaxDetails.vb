Imports Telerik.WinControls.UI
Imports XpertERPEngine
Public Class FrmTaxDetails
    Dim sql As String
    Dim ds As DataSet
    Public locationCode As String
    Public gridRow As GridViewRowInfo
    Public taxGroupCode As String
    Public totalItemTax As String
    Public assessibleAmount As String
    Public outGridRow As GridViewRowInfo
    Public btnFlag As Boolean
    Public taxDetails As New ArrayList()
    Private Sub gvTaxDetails_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTaxDetails.CellValueChanged
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        Dim column As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        If column.Name = "taxRate" Then
            Dim mrp As Decimal = 0.0
            Dim basic As Decimal = 0.0
            Dim netAmount As Decimal = gridRow.Cells("itemNetAmount").Value
            calculateTax(assessibleAmount, netAmount, locationCode)

        End If
    End Sub
    Private Sub gvTaxDetails_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gvTaxDetails.EditorRequired
        If TypeOf gvTaxDetails.CurrentColumn Is GridViewComboBoxColumn Then
            Dim coltaxrate As GridViewComboBoxColumn = TryCast(gvTaxDetails.Columns("taxRate"), GridViewComboBoxColumn)
            sql = "select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Code='" + gvTaxDetails.CurrentRow.Cells("taxAuthority").Value + "' AND Tax_Type='S'"
            ds = connectSql.RunSQLReturnDS(sql)
            ' coltaxrate.DisplayMember = "Tax_Rate"
            coltaxrate.ValueMember = "Tax_Rate"
            coltaxrate.DataSource = ds.Tables(0)
            'End If
        End If
    End Sub
    Private Sub calculateTax(ByVal assessible As Decimal, ByVal basic As Decimal, ByVal location As String)
        sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + location + "'"
        Dim strexcisable As String = connectSql.RunScalar(sql)
        If gvTaxDetails.RowCount <> 0 Then
            If strexcisable = "T" Or strexcisable = "Y" Then
                For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                    grow.Cells(3).Value = basic.ToString()
                    If grow.Index = 0 Then
                        grow.Cells(4).Value = Math.Round(assessible, 2)
                    ElseIf grow.Cells("surtax").Value = "N" Then
                        Dim taxabletaxTotal As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Index < grow.Index Then
                                If common.clsCommon.CompairString(common.clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = common.CompairStringResult.Equal Then
                                    taxabletaxTotal = taxabletaxTotal + CDec(gro.Cells(5).Value)
                                End If
                            End If
                        Next
                        grow.Cells(4).Value = basic + taxabletaxTotal
                    ElseIf grow.Cells("surtax").Value = "Y" Then
                        Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                        Dim assess As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Cells(0).Value = strSurtaxCode Then
                                assess = gro.Cells(5).Value
                                Exit For
                            End If
                        Next
                        grow.Cells(4).Value = Math.Round(assess, 2)

                    End If
                    grow.Cells(5).Value = Math.Round((CDec(grow.Cells(4).Value) * CDec(grow.Cells(2).Value) / 100), 2).ToString()
                Next
            Else
                For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                    grow.Cells(3).Value = basic.ToString()
                    If grow.Index = 0 Then
                        grow.Cells(4).Value = basic
                    ElseIf grow.Cells("surtax").Value = "N" Then
                        Dim taxabletaxTotal As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Index < grow.Index Then
                                If common.clsCommon.CompairString(common.clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = common.CompairStringResult.Equal Then
                                    taxabletaxTotal = taxabletaxTotal + CDec(gro.Cells(5).Value)
                                End If
                            End If
                        Next
                        grow.Cells(4).Value = basic + taxabletaxTotal
                    ElseIf grow.Cells("surtax").Value = "Y" Then
                        Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                        Dim assess As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Cells(0).Value = strSurtaxCode Then
                                assess = gro.Cells(5).Value
                                Exit For
                            End If
                        Next
                        grow.Cells(4).Value = Math.Round(assess, 2)

                    End If
                    grow.Cells(5).Value = Math.Round((CDec(grow.Cells(4).Value) * CDec(grow.Cells(2).Value) / 100), 2).ToString()
                Next
            End If
        Else

        End If
    End Sub

    Private Sub FrmTaxDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
           " WHERE G.Tax_Group_Code = '" + taxGroupCode + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " ORDER BY G.Trans_Code"
        ds = RunSQLReturnDS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvTaxDetails.DataSource = ds.Tables(0)
            Dim mrp As Decimal = 0.0
            Dim basic As Decimal = 0.0
            Dim netAmount As Decimal = gridRow.Cells("itemNetAmount").Value
            Dim totalAssessibleAmt As Decimal = 0
            calculateTax(assessibleAmount, netAmount, locationCode)
        End If
        For Each grow As GridViewRowInfo In gvTaxDetails.Rows
            grow.Cells("taxRate").ReadOnly = True
            If Not gridRow.Cells("Tax" + (grow.Index + 1).ToString() + "Rate").Value = "" Then
                grow.Cells("taxRate").Value = gridRow.Cells("Tax" + (grow.Index + 1).ToString() + "Rate").Value
            End If
        Next
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim totalTax As Decimal = 0.0
        For Each gr As GridViewRowInfo In gvTaxDetails.Rows
            totalTax = totalTax + CDec(gr.Cells(5).Value)
        Next
        totalItemTax = totalTax
        'gridRow.Cells("taxAmount").Value = totalTax
        For Each grow As GridViewRowInfo In gvTaxDetails.Rows
            Dim taxes(2) As String
            taxes(0) = grow.Cells("taxRate").Value
            taxes(1) = grow.Cells("assessibleAmount").Value
            taxes(2) = grow.Cells("taxAmount").Value
            'gridRow.Cells("Tax" + (grow.Index + 1).ToString() + "Rate").Value = grow.Cells("taxRate").Value
            'gridRow.Cells("assess" + (grow.Index + 1).ToString()).Value = grow.Cells("assessibleAmount").Value
            'gridRow.Cells("Tax" + (grow.Index + 1).ToString() + "Amt").Value = grow.Cells("taxAmount").Value
            taxDetails.Add(taxes)
        Next
        btnFlag = True
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnFlag = False
        Me.Close()
    End Sub
End Class

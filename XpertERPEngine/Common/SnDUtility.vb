Imports common
Imports Excel = Microsoft.Office.Interop.Excel
Imports Telerik.WinControls.UI

Public Class SnDUtility

    Public Shared Sub calculateTax(ByVal assessible As Decimal, ByVal basic As Decimal, ByVal location As String, ByVal gvLoadOut As RadGridView, ByVal gvTaxDetails As RadGridView)
        Dim sql As String = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + location + "'"
        Dim strexcisable As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
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
                                If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
                                    taxabletaxTotal = taxabletaxTotal + CDec(gro.Cells(5).Value)
                                End If
                            End If
                        Next
                        If basic = 0 Then
                            grow.Cells(4).Value = basic
                        Else
                            grow.Cells(4).Value = basic + taxabletaxTotal
                        End If
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
                                If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
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



    Public Shared Function calculateItemTax(ByVal assessible As Decimal, ByVal basic As Decimal, ByVal location As String, ByVal gvLoadOut As RadGridView, ByVal gvTaxDetails As RadGridView, Optional ByVal discper As Decimal = 0) As Decimal
        Dim sql As String = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + location + "'"
        Dim strexcisable As String = clsDBFuncationality.getSingleValue(sql)
        Dim tottalItemTax As Decimal = 0
        Dim nonexcise As String = String.Empty
        If gvTaxDetails.RowCount <> 0 Then
            If strexcisable = "T" Or strexcisable = "Y" Then
                For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                    If discper = 100 Then
                        nonexcise = clsDBFuncationality.getSingleValue("select Excisable from TSPL_TAX_MASTER where Tax_Code = '" + grow.Cells("taxAuthority").Value.ToString() + "'")
                        If nonexcise = "Y" Then
                            grow.Cells("basicAmount").Value = basic.ToString()
                            If grow.Index = 0 Then
                                grow.Cells("assessibleAmount").Value = Math.Round(assessible, 4, MidpointRounding.ToEven)
                            ElseIf grow.Cells("surtax").Value = "N" Then
                                Dim taxabletaxTotal As Decimal = 0
                                For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                                    If gro.Index < grow.Index Then
                                        If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
                                            taxabletaxTotal = taxabletaxTotal + CDec(gro.Cells("taxAmount").Value)
                                        End If
                                    End If
                                Next
                                grow.Cells("assessibleAmount").Value = basic + taxabletaxTotal
                            ElseIf grow.Cells("surtax").Value = "Y" Then
                                Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                                Dim assess As Decimal = 0
                                For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                                    If gro.Cells("taxAuthority").Value = strSurtaxCode Then
                                        assess = gro.Cells("taxAmount").Value
                                        Exit For
                                    End If
                                Next
                                grow.Cells("assessibleAmount").Value = Math.Round(assess, 4, MidpointRounding.ToEven)
                            End If
                            grow.Cells("taxAmount").Value = Math.Round((CDec(grow.Cells("assessibleAmount").Value) * CDec(grow.Cells("taxRate").Value) / 100), 4, MidpointRounding.ToEven).ToString()
                            tottalItemTax = tottalItemTax + CDec(grow.Cells("taxAmount").Value)
                        Else

                        End If
                    Else
                        grow.Cells("basicAmount").Value = basic.ToString()
                        If grow.Index = 0 Then
                            grow.Cells("assessibleAmount").Value = Math.Round(assessible, 4, MidpointRounding.ToEven)
                        ElseIf grow.Cells("surtax").Value = "N" Then
                            Dim taxabletaxTotal As Decimal = 0
                            For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                                If gro.Index < grow.Index Then
                                    If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
                                        taxabletaxTotal = taxabletaxTotal + CDec(gro.Cells("taxAmount").Value)
                                    End If
                                End If
                            Next
                            grow.Cells("assessibleAmount").Value = basic + taxabletaxTotal
                        ElseIf grow.Cells("surtax").Value = "Y" Then
                            Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                            Dim assess As Decimal = 0
                            For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                                If gro.Cells("taxAuthority").Value = strSurtaxCode Then
                                    assess = gro.Cells("taxAmount").Value
                                    Exit For
                                End If
                            Next
                            grow.Cells("assessibleAmount").Value = Math.Round(assess, 4, MidpointRounding.ToEven)
                        End If
                        grow.Cells("taxAmount").Value = Math.Round((CDec(grow.Cells("assessibleAmount").Value) * CDec(grow.Cells("taxRate").Value) / 100), 4, MidpointRounding.ToEven).ToString()
                        tottalItemTax = tottalItemTax + CDec(grow.Cells("taxAmount").Value)

                    End If

                Next
                Return tottalItemTax
            Else
                For Each grow As GridViewRowInfo In gvTaxDetails.Rows
                    grow.Cells("basicAmount").Value = basic.ToString()
                    If grow.Index = 0 Then
                        grow.Cells("assessibleAmount").Value = basic.ToString()
                    ElseIf grow.Cells("surtax").Value = "N" Then
                        Dim taxabletaxTotal As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Index < grow.Index Then
                                If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
                                    taxabletaxTotal = taxabletaxTotal + CDec(gro.Cells("taxAmount").Value)
                                End If
                            End If
                        Next
                        grow.Cells("assessibleAmount").Value = basic + taxabletaxTotal
                    ElseIf grow.Cells("surtax").Value = "Y" Then
                        Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                        Dim assess As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTaxDetails.Rows
                            If gro.Cells("taxAuthority").Value = strSurtaxCode Then
                                assess = gro.Cells("taxAmount").Value
                                Exit For
                            End If
                        Next
                        grow.Cells("assessibleAmount").Value = Math.Round(assess, 4, MidpointRounding.ToEven)
                    End If
                    grow.Cells("taxAmount").Value = Math.Round((CDec(grow.Cells("assessibleAmount").Value) * CDec(grow.Cells("taxRate").Value) / 100), 4, MidpointRounding.ToEven).ToString()
                    tottalItemTax = tottalItemTax + CDec(tottalItemTax + grow.Cells("taxAmount").Value)
                Next
                Return tottalItemTax
            End If
        Else
            Return 0
        End If
    End Function
    Public Enum EnuChartType
        Bar = 1
        Pie = 8
        Line = 4
        Area = 5
    End Enum

    Public Shared Sub GenerateExcelChart(ByVal dt As DataTable, ByVal EnuChartType As Integer, ByVal Title As String, ByVal LabelColumn As String, ByVal ValuColumn1 As String, Optional ByVal ValuColumn2 As String = "", Optional ByVal ValuColumn3 As String = "")
        Try
            Dim excel As New Excel.Application
            excel.Visible = True
            excel.Workbooks.Add()
            excel.Range("A1").Value2 = LabelColumn
            excel.Range("B1").Value2 = ValuColumn1
            If clsCommon.myLen(ValuColumn2) > 0 Then
                excel.Range("C1").Value2 = ValuColumn2
            End If
            If clsCommon.myLen(ValuColumn3) > 0 Then
                excel.Range("D1").Value2 = ValuColumn3
            End If

            Dim ii As Integer = 2
            For Each dr As DataRow In dt.Rows
                excel.Range("A" & ii).Value2 = dr(LabelColumn)
                excel.Range("B" & ii).Value2 = dr(ValuColumn1)
                If clsCommon.myLen(ValuColumn2) > 0 Then
                    excel.Range("C" & ii).Value2 = dr(ValuColumn2)
                End If
                If clsCommon.myLen(ValuColumn3) > 0 Then
                    excel.Range("D" & ii).Value2 = dr(ValuColumn3)
                End If
                ii += 1
            Next
            Dim range As Excel.Range = excel.Range("A1")
            Dim chart As Excel.Chart = excel.ActiveWorkbook.Charts.Add()
            chart.ChartWizard(Source:=range.CurrentRegion, Title:=Title)
            If EnuChartType = 1 Then
                chart.ChartStyle = 32
                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumnClustered
            ElseIf EnuChartType = 8 Then
                chart.ChartStyle = 34
                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie
            ElseIf EnuChartType = 4 Then
                chart.ChartStyle = 34
                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine
            ElseIf EnuChartType = 5 Then
                chart.ChartStyle = 34
                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DArea
            End If
            'chart.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowLabel)
            chart.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowValue)
            excel.Visible = True
            'chart.Ind
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

End Class

Public Class DateCheck
    Public Shared Function chkValidFromToDate(ByVal dt1 As String, ByVal dt2 As String) As Boolean
        If clsCommon.myCDate(dt1) > clsCommon.myCDate(dt2) Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
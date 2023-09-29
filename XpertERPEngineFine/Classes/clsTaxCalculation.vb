Imports System.Data.SqlClient

Public Class clsTaxCalculation
    Public Tax_Group_Code As String = Nothing
    Public Tax_Group_Desc As String = Nothing
    Public Tax_Group_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Is_Back_Calculation As Boolean = False
    Public Back_Calculation_Type As String = Nothing
    Public ItemBaseAmt As Double = Nothing
    Public Item_MRP As Double = Nothing
    Public dblTotalNonTabxableRate As Double = 0
    Public dblTotalNonTabxableAmt As Double = 0
    Public TaxBaseAmt As Double = 0

    Public Arr As List(Of clsTaxDetails) = Nothing


    Public Shared Function GetTax(ByVal PriceId As Integer, ByVal trans As SqlTransaction) As clsTaxCalculation
        Dim obj As clsTaxCalculation
        Try
            obj = New clsTaxCalculation()
            Dim StrQry As String = "select TSPL_ITEM_PRICE_PLAN_HEADER.Is_Back_Calculation,TSPL_ITEM_PRICE_PLAN_HEADER.Back_Calculation_Type,TSPL_ITEM_PRICE_MASTER.item_code, TSPL_ITEM_PRICE_MASTER.Tax_group,TSPL_ITEM_PRICE_MASTER.Item_MRP,TSPL_ITEM_PRICE_MASTER.Item_Basic_Price, TSPL_ITEM_PRICE_MASTER.Tax1, TSPL_ITEM_PRICE_MASTER.TAX1_Amt, TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  TSPL_ITEM_PRICE_MASTER.Tax2, TSPL_ITEM_PRICE_MASTER.TAX2_Amt, TSPL_ITEM_PRICE_MASTER.TAX2_Rate,  TSPL_ITEM_PRICE_MASTER.Tax3, TSPL_ITEM_PRICE_MASTER.TAX3_Amt, TSPL_ITEM_PRICE_MASTER.TAX3_Rate,  TSPL_ITEM_PRICE_MASTER.Tax4, TSPL_ITEM_PRICE_MASTER.TAX4_Amt, TSPL_ITEM_PRICE_MASTER.TAX4_Rate,  TSPL_ITEM_PRICE_MASTER.Tax5, TSPL_ITEM_PRICE_MASTER.TAX5_Amt, TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  TSPL_ITEM_PRICE_MASTER.Tax6, TSPL_ITEM_PRICE_MASTER.TAX6_Amt, TSPL_ITEM_PRICE_MASTER.TAX6_Rate,  TSPL_ITEM_PRICE_MASTER.Tax7, TSPL_ITEM_PRICE_MASTER.TAX7_Amt, TSPL_ITEM_PRICE_MASTER.TAX7_Rate,  TSPL_ITEM_PRICE_MASTER.Tax8, TSPL_ITEM_PRICE_MASTER.TAX8_Amt, TSPL_ITEM_PRICE_MASTER.TAX8_Rate,  TSPL_ITEM_PRICE_MASTER.Tax9, TSPL_ITEM_PRICE_MASTER.TAX9_Amt, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, TSPL_ITEM_PRICE_MASTER.Tax10, TSPL_ITEM_PRICE_MASTER.TAX10_Amt, TSPL_ITEM_PRICE_MASTER.TAX10_Rate from TSPL_ITEM_PRICE_MASTER left join TSPL_ITEM_PRICE_PLAN_DETAIL on TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code = TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_TR_Code  left join TSPL_ITEM_PRICE_PLAN_HEADER on TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_Code=TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code  where Item_Price_ID=" + clsCommon.myCstr(PriceId)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj.Is_Back_Calculation = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Back_Calculation")) = 1, True, False)
                obj.Back_Calculation_Type = clsCommon.myCstr(dt.Rows(0)("Is_Back_Calculation"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("item_code"))
                obj.Tax_Group_Code = clsCommon.myCstr(dt.Rows(0)("Tax_group"))
                obj.ItemBaseAmt = clsCommon.myCstr(dt.Rows(0)("Item_Basic_Price"))
                obj.Item_MRP = clsCommon.myCstr(dt.Rows(0)("Item_MRP"))
                obj.Arr = New List(Of clsTaxDetails)
                For i As Integer = 1 To 10
                    Dim objTr As New clsTaxDetails()
                    objTr.Tax_Code = clsCommon.myCstr(dt.Rows(0)("TAX" + clsCommon.myCstr(i)))
                    objTr.Tax_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX" + clsCommon.myCstr(i) + "_Rate"))
                    objTr.Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX" + clsCommon.myCstr(i) + "_Amt"))
                    If objTr.Tax_Rate > 0 Then
                        objTr.Tax_BaseAmt = objTr.Tax_Amt / (objTr.Tax_Rate / 100)
                    Else
                        objTr.Tax_BaseAmt = 0
                    End If
                    StrQry = "select TSPL_TAX_MASTER.Type as CodeType,TSPL_TAX_GROUP_DETAILS.Tax_Group_Type,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Taxable,TSPL_TAX_GROUP_DETAILS.Surtax,TSPL_TAX_GROUP_DETAILS.Surtax_Tax_Code,TSPL_TAX_GROUP_DETAILS.Surtax_Tax_Code_Desc,TSPL_TAX_GROUP_DETAILS.Tax_On_Base_Amount from TSPL_TAX_GROUP_DETAILS left join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + obj.Tax_Group_Code + "' and TSPL_TAX_GROUP_DETAILS.Tax_Code='" + objTr.Tax_Code + "'"
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrQry, trans)
                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        objTr.CodeType = clsCommon.myCstr(dt1.Rows(0)("CodeType"))
                        objTr.Tax_Group_Type = clsCommon.myCstr(dt1.Rows(0)("Tax_Group_Type"))
                        objTr.Tax_Code_Desc = clsCommon.myCstr(dt1.Rows(0)("Tax_Code_Desc"))
                        objTr.Tax_Code_Desc = clsCommon.myCstr(dt1.Rows(0)("Tax_Code_Desc"))
                        objTr.Taxable = clsCommon.myCstr(dt1.Rows(0)("Taxable"))
                        objTr.Surtax = clsCommon.myCstr(dt1.Rows(0)("Surtax"))
                        objTr.Surtax_Tax_Code = clsCommon.myCstr(dt1.Rows(0)("Surtax_Tax_Code"))
                        objTr.Surtax_Tax_Code_Desc = clsCommon.myCstr(dt1.Rows(0)("Surtax_Tax_Code_Desc"))
                        objTr.Tax_On_Base_Amount = clsCommon.myCstr(dt1.Rows(0)("Tax_On_Base_Amount"))

                    End If

                    obj.Arr.Add(objTr)
                Next


            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


        Return obj
    End Function
    Public Shared Function CalTaxForGhee(ByVal obj As clsTaxCalculation, ByVal IsKKF As Boolean, ByVal isMNDTax As Boolean, trans As SqlTransaction)

        Try
            obj.TaxBaseAmt = obj.Item_MRP

            For Each item As clsTaxDetails In obj.Arr
                If clsCommon.myLen(item.Tax_Code) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCdbl(item.Taxable), "Y") = CompairStringResult.Equal Then
                        obj.dblTotalNonTabxableRate += item.Tax_Rate
                    End If
                End If
            Next
            If obj.Is_Back_Calculation Then
                obj.dblTotalNonTabxableAmt = (obj.TaxBaseAmt * 100) / (100 + obj.dblTotalNonTabxableRate)
            Else
                obj.dblTotalNonTabxableAmt = obj.TaxBaseAmt
            End If
            Dim indexofKKF As Integer = 0
            Dim indexofMND As Integer = 0
            Dim count As Integer = 0
            For Each item As clsTaxDetails In obj.Arr
                count += 1
                If clsCommon.myLen(item.Tax_Code) > 0 Then
                    If clsCommon.CompairString(item.CodeType, "K") = CompairStringResult.Equal Then
                        If Not IsKKF Then
                            item.Tax_Rate = 0
                            item.Tax_Amt = 0
                            item.Tax_BaseAmt = obj.ItemBaseAmt
                            indexofKKF = count

                        End If
                    ElseIf clsCommon.CompairString(item.CodeType, "M") = CompairStringResult.Equal Then
                        If Not isMNDTax Then
                            item.Tax_Rate = 0
                            item.Tax_Amt = 0
                            item.Tax_BaseAmt = obj.ItemBaseAmt
                            indexofMND = count
                        End If
                    Else
                        If clsCommon.CompairString(item.Taxable, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(item.Tax_On_Base_Amount, "N") = CompairStringResult.Equal Then
                            item.Tax_BaseAmt = obj.ItemBaseAmt + obj.Arr(indexofKKF - 1).Tax_Amt + obj.Arr(indexofMND - 1).Tax_Amt
                            item.Tax_Amt = item.Tax_BaseAmt * (item.Tax_Rate / 100)

                        ElseIf clsCommon.CompairString(item.Taxable, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(item.Tax_On_Base_Amount, "Y") = CompairStringResult.Equal Then
                            item.Tax_BaseAmt = obj.ItemBaseAmt
                            item.Tax_Amt = item.Tax_BaseAmt * (item.Tax_Rate / 100)

                        End If



                    End If
                End If


            Next

        Catch ex As Exception

        End Try

        Return Nothing
    End Function

    'Public Function GetOtherTaxAmt(ByVal i As Integer, ByVal arrTaxableAuth As List(Of clsTaxDetails)) As Double
    '    Dim dblRet As Decimal = 0
    '    For Each strTaxAuth As clsTaxDetails In arrTaxableAuth
    '        For ii As Integer = 1 To i
    '            If clsCommon.CompairString(strTaxAuth.Tax_Code, clsCommon.myCstr(gv1.CurrentRow.Cells(colTax + clsCommon.myCstr(ii)).Value)) = CompairStringResult.Equal Then
    '                dblRet = dblRet + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt + clsCommon.myCstr(ii)).Value)
    '            End If
    '        Next
    '    Next
    '    Return dblRet
    'End Function

End Class

Public Class clsTaxDetails
    Public Tax_Code As String = Nothing
    Public Tax_BaseAmt As Decimal = Nothing
    Public Tax_Rate As Decimal = Nothing
    Public Tax_Amt As Decimal = Nothing

    Public CodeType As String = Nothing
    Public Tax_Group_Type As String = Nothing
    Public Tax_Code_Desc As String = Nothing
    Public Taxable As String = Nothing
    Public Surtax As String = Nothing
    Public Surtax_Tax_Code As String = Nothing
    Public Surtax_Tax_Code_Desc As String = Nothing
    Public Tax_On_Base_Amount As String = Nothing


End Class

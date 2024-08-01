Imports common
Imports System.Data.SqlClient

Public Class clsPricePlanHead
#Region "Variables"
    Public Plan_Code As String
    Public Plan_Date As DateTime
    Public Loc_Code As String
    Public Start_Date As DateTime
    Public End_Date As Date?
    Public Is_ALL_UOM As Boolean
    Public Is_FOR_Price As Boolean
    Public Is_Back_Calculation As Boolean
    Public Back_Calculation_Type As Integer
    Public Post_Status As ERPTransactionStatus
    Public Remarks As String = String.Empty
    Public PricePlanCopyNo As String = String.Empty
    Public Arr As List(Of clsPricePlanDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsPricePlanHead, ByVal IsNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If Not IsNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Plan_Code, "TSPL_ITEM_PRICE_PLAN_HEADER", "Plan_Code", "TSPL_ITEM_PRICE_PLAN_DETAIL", "Plan_Code", trans)
            End If

            Dim qry As String = "delete from TSPL_ITEM_PRICE_PLAN_DETAIL where Plan_Code='" + obj.Plan_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Plan_Date", clsCommon.GetPrintDate(obj.Plan_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Is_ALL_UOM", IIf(obj.Is_ALL_UOM, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_FOR_Price", IIf(obj.Is_FOR_Price, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Back_Calculation", IIf(obj.Is_Back_Calculation, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Back_Calculation_Type", obj.Back_Calculation_Type)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "PricePlanCopyNo", obj.PricePlanCopyNo, True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If IsNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                obj.Plan_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Plan_Date), clsDocType.PricePlan, "", obj.Loc_Code)
                If clsCommon.myLen(obj.Plan_Code) <= 0 Then
                    Throw New Exception("Error in code generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Plan_Code", obj.Plan_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PRICE_PLAN_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PRICE_PLAN_HEADER", OMInsertOrUpdate.Update, "Plan_Code='" & obj.Plan_Code & "'", trans)
            End If
            Dim objtr As New clsPricePlanDetail
            objtr.SaveData(obj.Plan_Code, obj.Arr, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As clsPricePlanHead
        Dim qry As String = "select * from TSPL_ITEM_PRICE_PLAN_HEADER Where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = (select MIN(Plan_Code) from TSPL_ITEM_PRICE_PLAN_HEADER where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = (select Max(Plan_Code) from TSPL_ITEM_PRICE_PLAN_HEADER where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = (select Min(Plan_Code) from TSPL_ITEM_PRICE_PLAN_HEADER where Plan_Code>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = (select Max(Plan_Code) from TSPL_ITEM_PRICE_PLAN_HEADER where Plan_Code<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = '" + strDocNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Dim obj As clsPricePlanHead = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPricePlanHead()
            obj.Plan_Code = clsCommon.myCstr(dt.Rows(0)("Plan_Code"))
            obj.Plan_Date = clsCommon.myCDate(dt.Rows(0)("Plan_Date"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.PricePlanCopyNo = clsCommon.myCstr(dt.Rows(0)("PricePlanCopyNo"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            End If
            obj.Is_ALL_UOM = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_ALL_UOM")) = 1, True, False)
            obj.Is_FOR_Price = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_FOR_Price")) = 1, True, False)
            obj.Is_Back_Calculation = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Back_Calculation")) = 1, True, False)
            obj.Back_Calculation_Type = clsCommon.myCdbl(dt.Rows(0)("Back_Calculation_Type"))
            obj.Post_Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Post_Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            qry = "select *,(select top 1 item_Price_ID from TSPL_ITEM_PRICE_MASTER where TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code=TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_TR_Code and TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_PRICE_PLAN_DETAIL.UOM) as item_Price_ID  from TSPL_ITEM_PRICE_PLAN_DETAIL where Plan_Code='" + obj.Plan_Code + "' order by SNo "
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsPricePlanDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsPricePlanDetail
                    objtr.SNo = clsCommon.myCdbl(dr("SNo"))
                    objtr.Plan_TR_Code = clsCommon.myCstr(dr("Plan_TR_Code"))
                    objtr.Plan_Code = clsCommon.myCstr(dr("Plan_Code"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.UOM = clsCommon.myCstr(dr("UOM"))
                    objtr.Item_MRP = clsCommon.myCdbl(dr("Item_MRP"))
                    objtr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                    objtr.Price_Comp1 = clsCommon.myCstr(dr("Price_Comp1"))
                    objtr.Price_Rate1 = clsCommon.myCdbl(dr("Price_Rate1"))
                    objtr.Price_Amount1 = clsCommon.myCdbl(dr("Price_Amount1"))
                    objtr.Price_Comp2 = clsCommon.myCstr(dr("Price_Comp2"))
                    objtr.Price_Rate2 = clsCommon.myCdbl(dr("Price_Rate2"))
                    objtr.Price_Amount2 = clsCommon.myCdbl(dr("Price_Amount2"))
                    objtr.Price_Comp3 = clsCommon.myCstr(dr("Price_Comp3"))
                    objtr.Price_Rate3 = clsCommon.myCdbl(dr("Price_Rate3"))
                    objtr.Price_Amount3 = clsCommon.myCdbl(dr("Price_Amount3"))
                    objtr.Price_Comp4 = clsCommon.myCstr(dr("Price_Comp4"))
                    objtr.Price_Rate4 = clsCommon.myCdbl(dr("Price_Rate4"))
                    objtr.Price_Amount4 = clsCommon.myCdbl(dr("Price_Amount4"))
                    objtr.Price_Comp5 = clsCommon.myCstr(dr("Price_Comp5"))
                    objtr.Price_Rate5 = clsCommon.myCdbl(dr("Price_Rate5"))
                    objtr.Price_Amount5 = clsCommon.myCdbl(dr("Price_Amount5"))
                    objtr.Price_Comp6 = clsCommon.myCstr(dr("Price_Comp6"))
                    objtr.Price_Rate6 = clsCommon.myCdbl(dr("Price_Rate6"))
                    objtr.Price_Amount6 = clsCommon.myCdbl(dr("Price_Amount6"))
                    objtr.Price_Comp7 = clsCommon.myCstr(dr("Price_Comp7"))
                    objtr.Price_Rate7 = clsCommon.myCdbl(dr("Price_Rate7"))
                    objtr.Price_Amount7 = clsCommon.myCdbl(dr("Price_Amount7"))
                    objtr.Price_Comp8 = clsCommon.myCstr(dr("Price_Comp8"))
                    objtr.Price_Rate8 = clsCommon.myCdbl(dr("Price_Rate8"))
                    objtr.Price_Amount8 = clsCommon.myCdbl(dr("Price_Amount8"))
                    objtr.Price_Comp9 = clsCommon.myCstr(dr("Price_Comp9"))
                    objtr.Price_Rate9 = clsCommon.myCdbl(dr("Price_Rate9"))
                    objtr.Price_Amount9 = clsCommon.myCdbl(dr("Price_Amount9"))
                    objtr.Price_Comp10 = clsCommon.myCstr(dr("Price_Comp10"))
                    objtr.Price_Rate10 = clsCommon.myCdbl(dr("Price_Rate10"))
                    objtr.Price_Amount10 = clsCommon.myCdbl(dr("Price_Amount10"))
                    objtr.Item_Basic_Price = clsCommon.myCstr(dr("Item_Basic_Price"))
                    objtr.Tax_group = clsCommon.myCstr(dr("Tax_group"))
                    objtr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objtr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objtr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objtr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objtr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objtr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objtr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objtr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objtr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objtr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objtr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objtr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objtr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objtr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objtr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objtr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objtr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objtr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objtr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objtr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objtr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objtr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objtr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objtr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objtr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objtr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objtr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objtr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objtr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objtr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objtr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objtr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objtr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objtr.Item_Selling_Price = clsCommon.myCdbl(dr("Item_Selling_Price"))
                    objtr.ItemPriceID = clsCommon.myCstr(dr("item_Price_ID"))
                    objtr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(dr("Against_Item_Wise_Tax_Rate"))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Order No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsPricePlanHead = clsPricePlanHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Plan_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Post_Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            CreatePrice(obj, trans)

            Dim qry As String = "Update TSPL_ITEM_PRICE_PLAN_HEADER set Post_Status=1, Post_Date='" + strPostDate + "',Post_By='" + objCommonVar.CurrentUserCode + "' where Plan_Code='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_ITEM_PRICE_MASTER set Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Against_Plan_TR_Code in (select Plan_TR_Code from TSPL_ITEM_PRICE_PLAN_DETAIL where Plan_Code='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Shared Sub CreatePrice(ByVal objPP As clsPricePlanHead, ByVal tran As SqlTransaction)
        Dim Arr As New List(Of clsPriceMaster)
        Dim dt As DataTable = Nothing
        Dim qry As String = Nothing
        For Each objPD As clsPricePlanDetail In objPP.Arr
            Dim dtUOMPrice As New DataTable()
            dtUOMPrice.Columns.Add("SNo", GetType(Integer))
            dtUOMPrice.Columns.Add("UOM", GetType(String))
            Dim ii As Integer = 1
            dtUOMPrice.Rows.Add(ii, objPD.UOM)
            If objPP.Is_ALL_UOM Then
                qry = "select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" + objPD.Item_Code + "' and UOM_Code not in ('" + objPD.UOM + "')"
                dt = clsDBFuncationality.GetDataTable(qry, tran)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        ii += 1
                        dtUOMPrice.Rows.Add(ii, clsCommon.myCstr(dr("UOM_Code")))
                    Next
                End If
            End If
            dt = clsPriceMaster.GetAllConversionOfItem(objPD.Item_Code, tran)
            Dim obj As clsPriceMaster = Nothing
            For Each dr As DataRow In dtUOMPrice.Rows
                obj = New clsPriceMaster()
                dt.DefaultView.RowFilter = "UOM2='" & objPD.UOM & "' AND UOM1='" & dr("UOM") & "'"
                If dt.DefaultView.ToTable Is Nothing OrElse dt.DefaultView.ToTable.Rows.Count <= 0 Then
                    Throw New Exception("Invalid UOM [" + objPD.UOM + "] for Item [" + objPD.Item_Code + "]")
                End If

                obj.Item_Price_ID = ""
                If clsCommon.myLen(obj.Item_Price_ID) > 0 Then
                    obj.IsNewEntry = False
                Else
                    obj.IsNewEntry = True
                End If
                obj.Remarks = ""
                obj.Item_Code = objPD.Item_Code
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.Item_Basic_Price_Type = objPD.Item_Basic_Price
                obj.Markup_On = "None"
                obj.Markup_Percent = 0
                obj.Basic_Price_On = "Landing Cost"
                obj.Landing_Cost = 0
                obj.Purchase_Cost = 0
                obj.Start_Date = objPP.Start_Date
                obj.End_Date = objPP.End_Date
                obj.Is_Active = 1
                obj.Is_For_Price = IIf(objPP.Is_FOR_Price, 1, 0)

                If objPP.Is_Back_Calculation Then
                    obj.Price_Category = "Auto"
                End If
                obj.Is_With_Tax = IIf(objPP.Back_Calculation_Type = 1, "Y", "N")
                obj.Tax_Manipulation_On = "Basic Price"
                obj.Location_Code = objPP.Loc_Code
                obj.Tax_group = objPD.Tax_group

                obj.Against_Plan_TR_Code = objPD.Plan_TR_Code
                obj.TAX1 = objPD.TAX1
                obj.TAX1_Rate = objPD.TAX1_Rate
                obj.TAX1_Amt = objPD.TAX1_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX2 = objPD.TAX2
                obj.TAX2_Rate = objPD.TAX2_Rate
                obj.TAX2_Amt = objPD.TAX2_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX3 = objPD.TAX3
                obj.TAX3_Rate = objPD.TAX3_Rate
                obj.TAX3_Amt = objPD.TAX3_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX4 = objPD.TAX4
                obj.TAX4_Rate = objPD.TAX4_Rate
                obj.TAX4_Amt = objPD.TAX4_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX5 = objPD.TAX5
                obj.TAX5_Rate = objPD.TAX5_Rate
                obj.TAX5_Amt = objPD.TAX5_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX6 = objPD.TAX6
                obj.TAX6_Rate = objPD.TAX6_Rate
                obj.TAX6_Amt = objPD.TAX6_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX7 = objPD.TAX7
                obj.TAX7_Rate = objPD.TAX7_Rate
                obj.TAX7_Amt = objPD.TAX7_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX8 = objPD.TAX8
                obj.TAX8_Rate = objPD.TAX8_Rate
                obj.TAX8_Amt = objPD.TAX8_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX9 = objPD.TAX9
                obj.TAX9_Rate = objPD.TAX9_Rate
                obj.TAX9_Amt = objPD.TAX9_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.TAX10 = objPD.TAX10
                obj.TAX10_Rate = objPD.TAX10_Rate
                obj.TAX10_Amt = objPD.TAX10_Amt * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))

                obj.Against_ItemwiseTaxCode = objPD.Against_Item_Wise_Tax_Rate

                obj.Price_Code = objPD.Price_Code
                qry = "SELECT distinct Price_code,Price_Code_Desc,Transfer  FROM [TSPL_PRICE_COMPONENT_MAPPING] where 2=2  and Price_code='" + objPD.Price_Code + "'"
                Dim dtPrice As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                If dtPrice IsNot Nothing AndAlso dtPrice.Rows.Count > 0 Then
                    obj.Price_Code_Desc = clsCommon.myCstr(dtPrice.Rows(0)("Price_Code_Desc"))
                    If clsCommon.myCdbl(dtPrice.Rows(0)("Transfer")) = 1 Then
                        obj.type = "T"
                    Else
                        obj.type = "S"
                    End If
                End If
                If clsCommon.myLen(objPD.Price_Comp1) > 0 Then
                    obj.Price_Comp1 = objPD.Price_Comp1
                    obj.Price_Comp_Desc1 = clsPriceComponent.GetName(objPD.Price_Comp1, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp1, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate1 = objPD.Price_Rate1 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate1 = objPD.Price_Rate1
                    End If
                    obj.Price_Amount1 = objPD.Price_Amount1 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp2) > 0 Then
                    obj.Price_Comp2 = objPD.Price_Comp2
                    obj.Price_Comp_Desc2 = clsPriceComponent.GetName(objPD.Price_Comp2, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp2, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate2 = objPD.Price_Rate2 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate2 = objPD.Price_Rate2
                    End If
                    obj.Price_Amount2 = objPD.Price_Amount2 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp3) > 0 Then
                    obj.Price_Comp3 = objPD.Price_Comp3
                    obj.Price_Comp_Desc3 = clsPriceComponent.GetName(objPD.Price_Comp3, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp3, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate3 = objPD.Price_Rate3 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate3 = objPD.Price_Rate3
                    End If
                    obj.Price_Amount3 = objPD.Price_Amount3 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp4) > 0 Then
                    obj.Price_Comp4 = objPD.Price_Comp4
                    obj.Price_Comp_Desc4 = clsPriceComponent.GetName(objPD.Price_Comp4, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp4, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate4 = objPD.Price_Rate4 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate4 = objPD.Price_Rate4
                    End If
                    obj.Price_Amount4 = objPD.Price_Amount4 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp5) > 0 Then
                    obj.Price_Comp5 = objPD.Price_Comp5
                    obj.Price_Comp_Desc5 = clsPriceComponent.GetName(objPD.Price_Comp5, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp5, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate5 = objPD.Price_Rate5 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate5 = objPD.Price_Rate5
                    End If
                    obj.Price_Amount5 = objPD.Price_Amount5 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp6) > 0 Then
                    obj.Price_Comp6 = objPD.Price_Comp6
                    obj.Price_Comp_Desc6 = clsPriceComponent.GetName(objPD.Price_Comp6, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp6, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate6 = objPD.Price_Rate6 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate6 = objPD.Price_Rate6
                    End If
                    obj.Price_Amount6 = objPD.Price_Amount6 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp7) > 0 Then
                    obj.Price_Comp7 = objPD.Price_Comp7
                    obj.Price_Comp_Desc7 = clsPriceComponent.GetName(objPD.Price_Comp7, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp7, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate7 = objPD.Price_Rate7 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate7 = objPD.Price_Rate7
                    End If
                    obj.Price_Amount7 = objPD.Price_Amount7 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp8) > 0 Then
                    obj.Price_Comp8 = objPD.Price_Comp8
                    obj.Price_Comp_Desc8 = clsPriceComponent.GetName(objPD.Price_Comp8, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp8, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate8 = objPD.Price_Rate8 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate8 = objPD.Price_Rate8
                    End If
                    obj.Price_Amount8 = objPD.Price_Amount8 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp9) > 0 Then
                    obj.Price_Comp9 = objPD.Price_Comp9
                    obj.Price_Comp_Desc9 = clsPriceComponent.GetName(objPD.Price_Comp9, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp9, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate9 = objPD.Price_Rate9 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate9 = objPD.Price_Rate9
                    End If
                    obj.Price_Amount9 = objPD.Price_Amount9 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If
                If clsCommon.myLen(objPD.Price_Comp10) > 0 Then
                    obj.Price_Comp10 = objPD.Price_Comp10
                    obj.Price_Comp_Desc10 = clsPriceComponent.GetName(objPD.Price_Comp10, tran)
                    If clsCommon.CompairString(clsPriceComponent.GetMethod(obj.Price_Code, objPD.Price_Comp10, tran), "Amount") = CompairStringResult.Equal Then
                        obj.Price_Rate10 = objPD.Price_Rate10 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                    Else
                        obj.Price_Rate10 = objPD.Price_Rate10
                    End If
                    obj.Price_Amount10 = objPD.Price_Amount10 * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                End If

                obj.Item_Basic_Net = objPD.Item_MRP * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                obj.Item_MRP = objPD.Item_MRP * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                obj.Item_Basic_Price = Math.Round(objPD.Item_Basic_Price * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor")), 6, MidpointRounding.ToEven)
                obj.Abatement_Rate = 100
                obj.Abatement = objPD.Item_MRP * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                obj.Can_Edit = "Y"
                obj.Item_Selling_Price = objPD.Item_Selling_Price * clsCommon.myCdbl(dt.DefaultView.ToTable.Rows(0)("Conversion_Factor"))
                Arr.Add(obj)
            Next
        Next
        clsPriceMaster.SaveData(Arr, False, objPP.Is_Back_Calculation, tran)
    End Sub

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Plan not found to Delete")
        End If
        Dim obj As clsPricePlanHead = clsPricePlanHead.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Plan_Code) > 0) Then
            Try
                If (obj.Post_Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted")
                End If
                clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_ITEM_PRICE_PLAN_HEADER", "Plan_Code", "TSPL_ITEM_PRICE_PLAN_DETAIL", "Plan_Code", trans)
                Dim qry As String = "delete from TSPL_ITEM_PRICE_PLAN_DETAIL where Plan_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_ITEM_PRICE_PLAN_HEADER where Plan_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
End Class

Public Class clsPricePlanDetail
#Region "Variables"
    Public SNo As Integer
    Public Plan_TR_Code As String
    Public Plan_Code As String
    Public Item_Code As String
    Public UOM As String
    Public Item_MRP As Double
    Public Price_Code As String
    Public PriceType As String
    Public Price_Code_Desc As String
    Public Price_Comp1 As String
    Public Price_Rate1 As Double
    Public Price_Amount1 As Double
    Public Price_Comp2 As String
    Public Price_Rate2 As Double
    Public Price_Amount2 As Double
    Public Price_Comp3 As String
    Public Price_Rate3 As Double
    Public Price_Amount3 As Double
    Public Price_Comp4 As String
    Public Price_Rate4 As Double
    Public Price_Amount4 As Double
    Public Price_Comp5 As String
    Public Price_Rate5 As Double
    Public Price_Amount5 As Double
    Public Price_Comp6 As String
    Public Price_Rate6 As Double
    Public Price_Amount6 As Double
    Public Price_Comp7 As String
    Public Price_Rate7 As Double
    Public Price_Amount7 As Double
    Public Price_Comp8 As String
    Public Price_Rate8 As Double
    Public Price_Amount8 As Double
    Public Price_Comp9 As String
    Public Price_Rate9 As Double
    Public Price_Amount9 As Double
    Public Price_Comp10 As String
    Public Price_Rate10 As Double
    Public Price_Amount10 As Double
    Public Item_Basic_Price As Double
    Public Tax_group As String
    Public TAX1_Base_Amt As Double
    Public TAX1 As String
    Public TAX1_Rate As Double
    Public TAX1_Amt As Double
    Public TAX2_Base_Amt As Double
    Public TAX2 As String
    Public TAX2_Rate As Double
    Public TAX2_Amt As Double
    Public TAX3_Base_Amt As Double
    Public TAX3 As String
    Public TAX3_Rate As Double
    Public TAX3_Amt As Double
    Public TAX4_Base_Amt As Double
    Public TAX4 As String
    Public TAX4_Rate As Double
    Public TAX4_Amt As Double
    Public TAX5_Base_Amt As Double
    Public TAX5 As String
    Public TAX5_Rate As Double
    Public TAX5_Amt As Double
    Public TAX6_Base_Amt As Double
    Public TAX6 As String
    Public TAX6_Rate As Double
    Public TAX6_Amt As Double
    Public TAX7_Base_Amt As Double
    Public TAX7 As String
    Public TAX7_Rate As Double
    Public TAX7_Amt As Double
    Public TAX8_Base_Amt As Double
    Public TAX8 As String
    Public TAX8_Rate As Double
    Public TAX8_Amt As Double
    Public TAX9_Base_Amt As Double
    Public TAX9 As String
    Public TAX9_Rate As Double
    Public TAX9_Amt As Double
    Public TAX10_Base_Amt As Double
    Public TAX10 As String
    Public TAX10_Rate As Double
    Public TAX10_Amt As Double
    Public Total_Tax_Amt As Double
    Public Item_Selling_Price As Double
    Public ItemPriceID As String ''Not a TAble column
    Public Against_Item_Wise_Tax_Rate As String
#End Region

    Public Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPricePlanDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ii As Integer = 0
            For Each objtr As clsPricePlanDetail In Arr
                ii += 1
                Dim coll As New Hashtable()

                objtr.Plan_TR_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(Plan_TR_Code) from TSPL_ITEM_PRICE_PLAN_DETAIL", trans))
                If clsCommon.myLen(objtr.Plan_TR_Code) <= 0 Then
                    objtr.Plan_TR_Code = "TR0000000000000001"
                Else
                    objtr.Plan_TR_Code = clsCommon.incval(objtr.Plan_TR_Code)
                End If
                If clsCommon.myLen(objtr.Plan_TR_Code) <= 0 Then
                    Throw New Exception("Error in code generation of Detail table")
                End If
                clsCommon.AddColumnsForChange(coll, "SNo", ii)
                clsCommon.AddColumnsForChange(coll, "Plan_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Plan_TR_Code", objtr.Plan_TR_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", objtr.UOM)
                clsCommon.AddColumnsForChange(coll, "Item_MRP", objtr.Item_MRP)
                clsCommon.AddColumnsForChange(coll, "Price_Code", objtr.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Price_Comp1", objtr.Price_Comp1)
                clsCommon.AddColumnsForChange(coll, "Price_Rate1", objtr.Price_Rate1)
                clsCommon.AddColumnsForChange(coll, "Price_Amount1", objtr.Price_Amount1)
                clsCommon.AddColumnsForChange(coll, "Price_Comp2", objtr.Price_Comp2)
                clsCommon.AddColumnsForChange(coll, "Price_Rate2", objtr.Price_Rate2)
                clsCommon.AddColumnsForChange(coll, "Price_Amount2", objtr.Price_Amount2)
                clsCommon.AddColumnsForChange(coll, "Price_Comp3", objtr.Price_Comp3)
                clsCommon.AddColumnsForChange(coll, "Price_Rate3", objtr.Price_Rate3)
                clsCommon.AddColumnsForChange(coll, "Price_Amount3", objtr.Price_Amount3)
                clsCommon.AddColumnsForChange(coll, "Price_Comp4", objtr.Price_Comp4)
                clsCommon.AddColumnsForChange(coll, "Price_Rate4", objtr.Price_Rate4)
                clsCommon.AddColumnsForChange(coll, "Price_Amount4", objtr.Price_Amount4)
                clsCommon.AddColumnsForChange(coll, "Price_Comp5", objtr.Price_Comp5)
                clsCommon.AddColumnsForChange(coll, "Price_Rate5", objtr.Price_Rate5)
                clsCommon.AddColumnsForChange(coll, "Price_Amount5", objtr.Price_Amount5)
                clsCommon.AddColumnsForChange(coll, "Price_Comp6", objtr.Price_Comp6)
                clsCommon.AddColumnsForChange(coll, "Price_Rate6", objtr.Price_Rate6)
                clsCommon.AddColumnsForChange(coll, "Price_Amount6", objtr.Price_Amount6)
                clsCommon.AddColumnsForChange(coll, "Price_Comp7", objtr.Price_Comp7)
                clsCommon.AddColumnsForChange(coll, "Price_Rate7", objtr.Price_Rate7)
                clsCommon.AddColumnsForChange(coll, "Price_Amount7", objtr.Price_Amount7)
                clsCommon.AddColumnsForChange(coll, "Price_Comp8", objtr.Price_Comp8)
                clsCommon.AddColumnsForChange(coll, "Price_Rate8", objtr.Price_Rate8)
                clsCommon.AddColumnsForChange(coll, "Price_Amount8", objtr.Price_Amount8)
                clsCommon.AddColumnsForChange(coll, "Price_Comp9", objtr.Price_Comp9)
                clsCommon.AddColumnsForChange(coll, "Price_Rate9", objtr.Price_Rate9)
                clsCommon.AddColumnsForChange(coll, "Price_Amount9", objtr.Price_Amount9)
                clsCommon.AddColumnsForChange(coll, "Price_Comp10", objtr.Price_Comp10)
                clsCommon.AddColumnsForChange(coll, "Price_Rate10", objtr.Price_Rate10)
                clsCommon.AddColumnsForChange(coll, "Price_Amount10", objtr.Price_Amount10)
                clsCommon.AddColumnsForChange(coll, "Item_Basic_Price", objtr.Item_Basic_Price)
                clsCommon.AddColumnsForChange(coll, "Tax_group", objtr.Tax_group)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", objtr.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1", objtr.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", objtr.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", objtr.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", objtr.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", objtr.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", objtr.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", objtr.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", objtr.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", objtr.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", objtr.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", objtr.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", objtr.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", objtr.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", objtr.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", objtr.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", objtr.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", objtr.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", objtr.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", objtr.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", objtr.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", objtr.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", objtr.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", objtr.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", objtr.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", objtr.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", objtr.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", objtr.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", objtr.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", objtr.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", objtr.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", objtr.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", objtr.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", objtr.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", objtr.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", objtr.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", objtr.TAX10_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", objtr.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", objtr.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", objtr.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", objtr.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", objtr.Item_Selling_Price)
                clsCommon.AddColumnsForChange(coll, "Against_Item_Wise_Tax_Rate", objtr.Against_Item_Wise_Tax_Rate, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PRICE_PLAN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

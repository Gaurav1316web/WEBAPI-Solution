Imports common
Imports System.Data.SqlClient

Public Class clsSchemeMasterDairy
#Region "Variables"
    Public Structure_Code As String = Nothing
    Public Structure_Unit As String = Nothing
    Public MaxlimitStart_Date As Date?
    Public MaxlimitEnd_Date As Date?
    Public Scheme_Code As String = Nothing
    Public Scheme_Desc As String = Nothing
    Public Start_Date As Date
    Public End_Date As Date?
    Public Scheme_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    Public Item_Qty As Double = 0.0
    Public MRP As Double = 0.0
    Public Basic_Price As Double = 0.0
    Public Amount As Double = 0.0
    Public Percentage As Double = 0.0
    Public Status As String = Nothing
    Public Criteria As String = Nothing
    Public Criteria_Code As String = Nothing
    Public Criteria_Desc As String = Nothing
    Public Comments As String = Nothing
    Public CASHDISVOL_UOM As String = Nothing
    Public CASHDISVOL_RANGE_UOM As String = Nothing
    Public ArrDTL As List(Of clsSchemeDetailDairy) = Nothing
    Public ArrSchmBen As List(Of clsSchemeBenificiaryDairy) = Nothing
    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Target_Sub_Type As String = Nothing
    Public Apply_Slab As Boolean = False
    Public arrSLAB As List(Of clsSchemeMasterDairySlab)
    Public arrQuantativeSLAB As List(Of clsSchemeMasterDairyQuantativeSlab)
    Public Quantative_Scheme_In_Slab As Boolean = False
    Public Quantum As Integer = 0
    Public arrVolumeSLAB As List(Of clsSchemeMasterVolumeSlab)

    Public Quantitive_Type As Integer
    Public ArrQuantitiveStructureCode As ArrayList
    Public Quantitive_Type_Structure_Main_Qty As Decimal
    Public Quantitive_Type_Structure_Main_UOM As String
    Public Quantitive_Type_Structure_Free_Item As String
    Public Quantitive_Type_Structure_Free_Qty As Decimal
    Public Quantitive_Type_Structure_Free_UOM As String
    Public ArrCashDiscountVolumneDetails As List(Of clsCashDiscountVolumneDetail) = Nothing
    Public ArrCashDisSturctureMapping As ArrayList
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_SCHEME_MASTER_NEW.Scheme_Code as [Code] ,TSPL_SCHEME_MASTER_NEW.Scheme_Desc as [Scheme Description] ,TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date as [From Date],TSPL_SCHEME_MASTER_NEW.MaxlimitEnd_Date as [To Date] ,TSPL_SCHEME_MASTER_NEW.Scheme_Type as [Scheme Type] ,TSPL_SCHEME_MASTER_NEW.Item_Qty as [Item Qty] ,TSPL_SCHEME_MASTER_NEW.MRP as [MRP] ,TSPL_SCHEME_MASTER_NEW.Basic_Price as [Basic Price] ,TSPL_SCHEME_MASTER_NEW.Percentage as [Percentage] ,TSPL_SCHEME_MASTER_NEW.Amount as [Amount] ,TSPL_SCHEME_MASTER_NEW.Status as [Status] ,TSPL_SCHEME_MASTER_NEW.Criteria as [Criteria] ,TSPL_SCHEME_MASTER_NEW.Criteria_Code as [Criteria Code] ,TSPL_SCHEME_MASTER_NEW.Comments as [Comments] ,TSPL_SCHEME_MASTER_NEW.Created_By as [Created By] ,TSPL_SCHEME_MASTER_NEW.Created_Date as [Created Date] ,TSPL_SCHEME_MASTER_NEW.Modify_By as [Modify By] ,TSPL_SCHEME_MASTER_NEW.Modify_Date as [Modify Date] ,TSPL_SCHEME_MASTER_NEW.Comp_Code as [Company Code]  From TSPL_SCHEME_MASTER_NEW Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SCHEME_MASTER_NEW.Item_Code   "
        str = clsCommon.ShowSelectForm("SCHMMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Function SaveData(ByVal obj As clsSchemeMasterDairy, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsSchemeMasterDairy, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_QUANTITIVE_STRUCTURE WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_DETAIL_NEW WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_MASTER_NEW_SLAB WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_MASTER_NEW_QUANTATIVE_SLAB WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_MASTER_VOLUME_SLAB WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_MASTER_CASHDISVOLUME_SLAB WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_CASHDIS_ITEM_STRUCTURE WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Scheme_Desc", obj.Scheme_Desc)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Qty", obj.Item_Qty)
            clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
            clsCommon.AddColumnsForChange(coll, "Basic_Price", obj.Basic_Price)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Percentage", obj.Percentage)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            If clsCommon.CompairString(obj.Status, "InActive") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "CASHDISVOL_RANGE_UOM", obj.CASHDISVOL_RANGE_UOM, True)
            clsCommon.AddColumnsForChange(coll, "CASHDISVOL_UOM", obj.CASHDISVOL_UOM, True)

            clsCommon.AddColumnsForChange(coll, "Criteria", obj.Criteria)
            clsCommon.AddColumnsForChange(coll, "Criteria_Code", obj.Criteria_Code)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If obj.MaxlimitStart_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "MaxlimitStart_Date", clsCommon.GetPrintDate(obj.MaxlimitStart_Date, "dd-MMM-yyyy"))
            End If
            If obj.MaxlimitEnd_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "MaxlimitEnd_Date", clsCommon.GetPrintDate(obj.MaxlimitEnd_Date, "dd-MMM-yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Apply_Slab", IIf(obj.Apply_Slab, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Quantative_Scheme_In_Slab", IIf(obj.Quantative_Scheme_In_Slab, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Quantum", obj.Quantum)
            clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
            clsCommon.AddColumnsForChange(coll, "Structure_Unit", obj.Structure_Unit)
            clsCommon.AddColumnsForChange(coll, "Quantitive_Type", obj.Quantitive_Type)

            clsCommon.AddColumnsForChange(coll, "Quantitive_Type_Structure_Main_Qty", obj.Quantitive_Type_Structure_Main_Qty)
            clsCommon.AddColumnsForChange(coll, "Quantitive_Type_Structure_Main_UOM", obj.Quantitive_Type_Structure_Main_UOM, True)
            clsCommon.AddColumnsForChange(coll, "Quantitive_Type_Structure_Free_Item", obj.Quantitive_Type_Structure_Free_Item, True)
            clsCommon.AddColumnsForChange(coll, "Quantitive_Type_Structure_Free_Qty", obj.Quantitive_Type_Structure_Free_Qty)
            clsCommon.AddColumnsForChange(coll, "Quantitive_Type_Structure_Free_UOM", obj.Quantitive_Type_Structure_Free_UOM, True)
            If isNewEntry Then
                obj.Scheme_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Scheme_Code) from TSPL_SCHEME_MASTER_NEW", trans))
                If clsCommon.myLen(obj.Scheme_Code) <= 0 Then
                    obj.Scheme_Code = "SCHM000001"
                Else
                    obj.Scheme_Code = clsCommon.incval(obj.Scheme_Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Target_Sub_Type", obj.Target_Sub_Type)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW", OMInsertOrUpdate.Update, "Scheme_Code='" + obj.Scheme_Code + "'", trans)
            End If
            clsSchemeDetailDairy.SaveData(obj.Scheme_Code, obj.ArrDTL, obj.Scheme_Type, trans)
            clsSchemeBenificiaryDairy.SaveData(obj.Scheme_Code, obj.ArrSchmBen, obj.Scheme_Type, trans)
            clsCustomFieldValues.SaveData(obj.Form_ID, obj.Scheme_Code, obj.arrCustomFields, trans)
            If obj.Apply_Slab And obj.Quantative_Scheme_In_Slab Then
                clsSchemeMasterDairySlab.SaveData(obj.Scheme_Code, obj.arrSLAB, trans)
                clsSchemeMasterDairyQuantativeSlab.SaveData(obj.Scheme_Code, obj.arrQuantativeSLAB, trans)
            ElseIf obj.Apply_Slab Then
                clsSchemeMasterDairySlab.SaveData(obj.Scheme_Code, obj.arrSLAB, trans)
            ElseIf clsCommon.CompairString(obj.Scheme_Type, "VolumeSlab") = CompairStringResult.Equal Then
                clsSchemeMasterVolumeSlab.SaveData(obj.Scheme_Code, obj.arrVolumeSLAB, trans)
            End If
            clsSchemeQuantitiveStructure.SaveData(obj.Scheme_Code, obj.ArrQuantitiveStructureCode, trans)
            clsCashDiscountVolumneDetail.SaveData(obj.Scheme_Code, obj.ArrCashDiscountVolumneDetails, trans)
            clsCashDisSturctureMapping.SaveData(obj.Scheme_Code, obj.ArrCashDisSturctureMapping, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strSchemeCode As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsSchemeMasterDairy
        Dim obj As clsSchemeMasterDairy = Nothing
        Dim qry As String = "Select TSPL_SCHEME_MASTER_NEW.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_UNIT_MASTER.Unit_Desc, Case When Criteria='Customer Group' Then TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc When Criteria='Customer Category' then TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC Else '' End as Criteria_Desc  from TSPL_SCHEME_MASTER_NEW LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SCHEME_MASTER_NEW.Item_Code" & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_SCHEME_MASTER_NEW.Criteria_Code" & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_SCHEME_MASTER_NEW.Criteria_Code" & _
                    " LEFT OUTER JOIN TSPL_UNIT_MASTER ON TSPL_UNIT_MASTER.Unit_Code=TSPL_SCHEME_MASTER_NEW.Unit_Code where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code=(select MIN(Scheme_Code) from TSPL_SCHEME_MASTER_NEW Where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code=(select Max(Scheme_Code) from TSPL_SCHEME_MASTER_NEW Where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code=(select Min(Scheme_Code) from TSPL_SCHEME_MASTER_NEW where Scheme_Code > '" + strSchemeCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code=(select Max(Scheme_Code) from TSPL_SCHEME_MASTER_NEW where Scheme_Code < '" + strSchemeCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code='" + strSchemeCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSchemeMasterDairy()
            If dt.Rows(0)("MaxlimitStart_Date") IsNot DBNull.Value Then
                obj.MaxlimitStart_Date = clsCommon.myCDate(dt.Rows(0)("MaxlimitStart_Date"))
            Else
                obj.MaxlimitStart_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            End If
            If dt.Rows(0)("MaxlimitEnd_Date") IsNot DBNull.Value Then
                obj.MaxlimitEnd_Date = clsCommon.myCDate(dt.Rows(0)("MaxlimitEnd_Date"))
            Else
                obj.MaxlimitEnd_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date")).AddYears(1)
            End If
            If dt.Rows(0)("Target_Sub_Type") IsNot DBNull.Value Then
                obj.Target_Sub_Type = clsCommon.myCstr(dt.Rows(0)("Target_Sub_Type"))
            End If
            obj.Structure_Code = clsCommon.myCstr(dt.Rows(0)("Structure_Code"))
            obj.Structure_Unit = clsCommon.myCstr(dt.Rows(0)("Structure_Unit"))
            obj.Scheme_Code = clsCommon.myCstr(dt.Rows(0)("Scheme_Code"))
            obj.Scheme_Desc = clsCommon.myCstr(dt.Rows(0)("Scheme_Desc"))
            If dt.Rows(0)("Start_Date") IsNot DBNull.Value Then
                obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            End If
            obj.Scheme_Type = clsCommon.myCstr(dt.Rows(0)("Scheme_Type"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            obj.Unit_Desc = clsCommon.myCstr(dt.Rows(0)("Unit_Desc"))
            obj.Item_Qty = clsCommon.myCdbl(dt.Rows(0)("Item_Qty"))
            obj.MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))
            obj.Basic_Price = clsCommon.myCdbl(dt.Rows(0)("Basic_Price"))
            obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
            obj.Percentage = clsCommon.myCdbl(dt.Rows(0)("Percentage"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            If clsCommon.CompairString(obj.Status, "InActive") = CompairStringResult.Equal Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            End If
            obj.Criteria = clsCommon.myCstr(dt.Rows(0)("Criteria"))
            obj.Criteria_Code = clsCommon.myCstr(dt.Rows(0)("Criteria_Code"))
            obj.Criteria_Desc = clsCommon.myCstr(dt.Rows(0)("Criteria_Desc"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Apply_Slab = (clsCommon.myCdbl(dt.Rows(0)("Apply_Slab")) = 1)
            obj.Quantative_Scheme_In_Slab = (clsCommon.myCdbl(dt.Rows(0)("Quantative_Scheme_In_Slab")) = 1)
            obj.Quantum = CInt(clsCommon.myCdbl(dt.Rows(0)("Quantum")))

            obj.Quantitive_Type = clsCommon.myCdbl(dt.Rows(0)("Quantitive_Type"))

            obj.Quantitive_Type_Structure_Main_Qty = clsCommon.myCdbl(dt.Rows(0)("Quantitive_Type_Structure_Main_Qty"))
            obj.Quantitive_Type_Structure_Main_UOM = clsCommon.myCstr(dt.Rows(0)("Quantitive_Type_Structure_Main_UOM"))
            obj.Quantitive_Type_Structure_Free_Item = clsCommon.myCstr(dt.Rows(0)("Quantitive_Type_Structure_Free_Item"))
            obj.Quantitive_Type_Structure_Free_Qty = clsCommon.myCdbl(dt.Rows(0)("Quantitive_Type_Structure_Free_Qty"))
            obj.Quantitive_Type_Structure_Free_UOM = clsCommon.myCstr(dt.Rows(0)("Quantitive_Type_Structure_Free_UOM"))
            obj.CASHDISVOL_RANGE_UOM = clsCommon.myCstr(dt.Rows(0)("CASHDISVOL_RANGE_UOM"))
            obj.CASHDISVOL_UOM = clsCommon.myCstr(dt.Rows(0)("CASHDISVOL_UOM"))



            qry = "Select 1 as [Select],TSPL_SCHEME_DETAIL_NEW.Max_Limit,TSPL_SCHEME_DETAIL_NEW.Increment_Value,TSPL_SCHEME_DETAIL_NEW.Amount,TSPL_SCHEME_DETAIL_NEW.CashDisc_Amount,TSPL_SCHEME_DETAIL_NEW.CasdDisc_Percentage,TSPL_SCHEME_DETAIL_NEW.MainItem_Code,TSPL_SCHEME_DETAIL_NEW.MainQty,TSPL_SCHEME_DETAIL_NEW.MainUnit_Code,Scheme_Code, " & _
            "MainItemTable.Item_Desc as MainItem_Desc, TSPL_SCHEME_DETAIL_NEW.Item_Code, TSPL_ITEM_MASTER.Item_Desc, Qty, TSPL_SCHEME_DETAIL_NEW.Unit_Code, MRP, " & _
            "Price_Date, Basic_Price, Remarks, MainItemTable.Structure_Code as mainItemStrCode,MainItemStr.Structure_Descq  as mainItemStrDesc, " & _
            "TSPL_ITEM_MASTER.Structure_Code  as SchemeItemStrCode,SchemeItemStr.Structure_Descq  as SchemeItemStrDesc   from TSPL_SCHEME_DETAIL_NEW LEFT OUTER JOIN TSPL_ITEM_MASTER ON  " & _
            "TSPL_ITEM_MASTER.Item_Code=TSPL_SCHEME_DETAIL_NEW.Item_Code " & _
            "LEFT OUTER JOIN TSPL_ITEM_MASTER as MainItemTable ON MainItemTable.Item_Code=TSPL_SCHEME_DETAIL_NEW.MainItem_Code " & _
            "left outer join TSPL_STRUCTURE_MASTER MainItemStr on MainItemTable.Structure_Code=MainItemStr.Structure_Code " & _
            "left outer join TSPL_STRUCTURE_MASTER SchemeItemStr on TSPL_ITEM_MASTER.Structure_Code=SchemeItemStr.Structure_Code " & _
            "WHERE Scheme_Code = '" + obj.Scheme_Code + "'"
            If Not (clsCommon.CompairString(obj.Scheme_Type, "MaxLimit") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Scheme_Type, "Fixed") = CompairStringResult.Equal) Then
                If clsCommon.CompairString(obj.Scheme_Type, "Discount") = CompairStringResult.Equal Then
                    qry += "Union all " & _
               " select   0 as [Select],0 as Max_Limit,0 as Increment_Value,0 as Amount,0 as CashDisc_Amount, 0 as CasdDisc_Percentage,TSPL_ITEM_MASTER.Item_Code as MainItem_Code,0 as MainQty,tspl_item_uom_detail.UOM_Code as MainUnit_Code,'' as Scheme_Code, " & _
               "Item_Desc as MainItem_Desc,'' as Item_Code,'' as Item_Desc,0 as Qty,'' as Unit_Code,0 as MRP, " & _
               "'' as Price_Date,0 as Basic_Price,'' as Remarks,TSPL_ITEM_MASTER.Structure_Code as mainItemStrCode,MainItemStr.Structure_Descq  as mainItemStrDesc,''  as SchemeItemStrCode,''   as SchemeItemStrDesc  from TSPL_ITEM_MASTER left outer join tspl_item_uom_detail on " & _
               "TSPL_ITEM_MASTER.item_code=tspl_item_uom_detail.item_code and tspl_item_uom_detail.Default_UOM=1 " & _
               "left outer join TSPL_STRUCTURE_MASTER MainItemStr on TSPL_ITEM_MASTER.Structure_Code=MainItemStr.Structure_Code where  CSA_TYPE='" & obj.Item_Code & "'  " & _
               "and TSPL_ITEM_MASTER.Item_Code not in (select MainItem_Code from TSPL_SCHEME_DETAIL_NEW where Scheme_Code='" + obj.Scheme_Code + "')"
                Else
                    qry += "Union all " & _
               " select   0 as [Select],0 as Max_Limit,0 as Increment_Value,0 as Amount,0 as CashDisc_Amount, 0 as CasdDisc_Percentage,Item_Code as MainItem_Code,0 as MainQty,'' as MainUnit_Code,'' as Scheme_Code, " & _
               "Item_Desc as MainItem_Desc,'' as Item_Code,'' as Item_Desc,0 as Qty,'' as Unit_Code,0 as MRP, " & _
               "'' as Price_Date,0 as Basic_Price,'' as Remarks,TSPL_ITEM_MASTER.Structure_Code as mainItemStrCode,MainItemStr.Structure_Descq  as mainItemStrDesc,''  as SchemeItemStrCode,''   as SchemeItemStrDesc  from TSPL_ITEM_MASTER  " & _
               "left outer join TSPL_STRUCTURE_MASTER MainItemStr on TSPL_ITEM_MASTER.Structure_Code=MainItemStr.Structure_Code  where  CSA_TYPE='" & obj.Item_Code & "'  " & _
               "and Item_Code not in (select MainItem_Code from TSPL_SCHEME_DETAIL_NEW where Scheme_Code='" + obj.Scheme_Code + "')"
                End If

            End If

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrDTL = New List(Of clsSchemeDetailDairy)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsSchemeDetailDairy
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSchemeDetailDairy()
                    objTr.ColSelect = clsCommon.myCdbl(dr("Select"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Basic_Price = clsCommon.myCdbl(dr("Basic_Price"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    'objTr.Price_Date = clsCommon.myCstr(dr("Price_Date"))
                    'clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

                    If dr("Price_Date") IsNot Nothing AndAlso IsDBNull(dr("Price_Date")) = False Then
                        objTr.Price_Date = clsCommon.myCstr(clsCommon.GetPrintDate(dr("Price_Date"), "dd/MM/yyyy"))
                    End If
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.MainItem_Code = clsCommon.myCstr(dr("MainItem_Code"))
                    objTr.MainItem_Desc = clsCommon.myCstr(dr("MainItem_Desc"))
                    objTr.MainUnit_Code = clsCommon.myCstr(dr("MainUnit_Code"))
                    objTr.Amount = clsCommon.myCstr(dr("Amount"))
                    objTr.MainItemStrCode = clsCommon.myCstr(dr("mainItemStrCode"))
                    objTr.MainItemStrDesc = clsCommon.myCstr(dr("mainItemStrDesc"))
                    objTr.SchemeItemStrCode = clsCommon.myCstr(dr("SchemeItemStrCode"))
                    objTr.SchemeItemStrDesc = clsCommon.myCstr(dr("SchemeItemStrDesc"))
                    objTr.MainQty = clsCommon.myCdbl(dr("MainQty"))
                    objTr.CashDisc_Amount = clsCommon.myCdbl(dr("CashDisc_Amount"))
                    objTr.CasdDisc_Percentage = clsCommon.myCdbl(dr("CasdDisc_Percentage"))

                    objTr.Max_Limit = clsCommon.myCdbl(dr("Max_Limit"))
                    objTr.Increment_Value = clsCommon.myCdbl(dr("Increment_Value"))

                    obj.ArrDTL.Add(objTr)
                Next
            End If

            If clsCommon.CompairString(obj.Criteria, "Customer Group") = CompairStringResult.Equal Then
                qry = "Select Cast(Case When ISNULL(XXX.Cust_Code,'')<>'' Then 1 Else 0 End as Bit) As [Select], XXX.Scheme_Code, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN ( Select TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_SCHEME_BENEFICIARY.Scheme_Code from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code='" + obj.Scheme_Code + "') XXX ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code WHERE TSPL_CUSTOMER_MASTER.Cust_Group_Code='" + obj.Criteria_Code + "'"
            ElseIf clsCommon.CompairString(obj.Criteria, "Customer Category") = CompairStringResult.Equal Then
                qry = "Select Cast(Case When ISNULL(XXX.Cust_Code,'')<>'' Then 1 Else 0 End as Bit) As [Select], XXX.Scheme_Code, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN ( Select TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_SCHEME_BENEFICIARY.Scheme_Code from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code='" + obj.Scheme_Code + "') XXX ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code WHERE TSPL_CUSTOMER_MASTER.Cust_Category_Code='" + obj.Criteria_Code + "'"
            Else
                qry = "Select Cast(1 as bit) as [Select], TSPL_SCHEME_BENEFICIARY.Scheme_Code, TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SCHEME_BENEFICIARY LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCHEME_BENEFICIARY.Cust_Code WHERE TSPL_SCHEME_BENEFICIARY.Scheme_Code = '" + obj.Scheme_Code + "'"
            End If

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrSchmBen = New List(Of clsSchemeBenificiaryDairy)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsSchemeBenificiaryDairy
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSchemeBenificiaryDairy()
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.check = clsCommon.myCdbl(dr("Select"))
                    objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Customer_Name = clsCommon.myCstr(dr("Customer_Name"))
                    obj.ArrSchmBen.Add(objTr)
                Next
            End If
            obj.arrSLAB = clsSchemeMasterDairySlab.GetData(obj.Scheme_Code, trans)
            obj.arrQuantativeSLAB = clsSchemeMasterDairyQuantativeSlab.GetData(obj.Scheme_Code, trans)
            obj.arrVolumeSLAB = clsSchemeMasterVolumeSlab.GetData(obj.Scheme_Code, trans)
            obj.ArrQuantitiveStructureCode = clsSchemeQuantitiveStructure.GetData(obj.Scheme_Code, trans)

            obj.ArrCashDiscountVolumneDetails = clsCashDiscountVolumneDetail.GetData(obj.Scheme_Code, trans)
            obj.ArrCashDisSturctureMapping = clsCashDisSturctureMapping.GetData(obj.Scheme_Code, trans)
        End If
        Return obj
    End Function

    Public Shared Function fundelete(ByVal strSchemeCode As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsSchemeMasterDairy
            If clsCommon.myLen(strSchemeCode) > 0 Then
                obj = clsSchemeMasterDairy.GetData(strSchemeCode, NavigatorType.Current, trans)
            Else
                Throw New Exception("Document not found to delete.")
            End If
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_QUANTITIVE_STRUCTURE WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_MASTER_VOLUME_SLAB WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_MASTER_NEW_QUANTATIVE_SLAB WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_MASTER_NEW_SLAB WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_SCHEME_DETAIL_NEW Where Scheme_Code='" + strSchemeCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_SCHEME_BENEFICIARY Where Scheme_Code='" + strSchemeCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_SCHEME_MASTER_NEW Where Scheme_Code='" + strSchemeCode + "'", trans)
            clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Scheme_Code, trans)
            'trans.Commit()
            'Return True
        Catch ex As Exception
            'trans.Rollback()
            'clsCommon.MyMessageBoxShow(ex.Message)
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsSchemeDetailDairy
#Region "Variables"
    Public Scheme_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0.0
    Public Unit_Code As String = Nothing
    Public MRP As Double = 0.0
    Public Basic_Price As Double = 0.0
    Public Price_Date As Date
    Public Remarks As String = Nothing
    Public MainItem_Code As String = Nothing
    Public MainItem_Desc As String = Nothing
    Public MainQty As Double = 0.0
    Public MainUnit_Code As String = Nothing
    Public CasdDisc_Percentage As Double = 0
    Public CashDisc_Amount As Double = 0
    Public Amount As Double = 0
    Public MainItemStrCode As String = Nothing
    Public MainItemStrDesc As String = Nothing
    Public SchemeItemStrCode As String = Nothing
    Public SchemeItemStrDesc As String = Nothing
    Public ColSelect As Double = 0

    Public Max_Limit As Decimal
    Public Increment_Value As Decimal
#End Region

    Public Shared Function SaveData(ByVal strSchemeCode As String, ByVal Arr As List(Of clsSchemeDetailDairy), ByVal ReceiptType As String, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSchemeDetailDairy In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", strSchemeCode)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Basic_Price", obj.Basic_Price)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "MainItem_Code", obj.MainItem_Code)
                clsCommon.AddColumnsForChange(coll, "MainQty", obj.MainQty)
                clsCommon.AddColumnsForChange(coll, "MainUnit_Code", obj.MainUnit_Code)
                clsCommon.AddColumnsForChange(coll, "CashDisc_Amount", obj.CashDisc_Amount)
                clsCommon.AddColumnsForChange(coll, "CasdDisc_Percentage", obj.CasdDisc_Percentage)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)

                clsCommon.AddColumnsForChange(coll, "Max_Limit", obj.Max_Limit)
                clsCommon.AddColumnsForChange(coll, "Increment_Value", obj.Increment_Value)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class

Public Class clsSchemeBenificiaryDairy
#Region "Variables"
    Public Scheme_Code As String = Nothing
    Public check As Integer = 0
    Public Cust_Code As String
    Public Customer_Name As String
#End Region

    Public Shared Function SaveData(ByVal strSchemeCode As String, ByVal Arr As List(Of clsSchemeBenificiaryDairy), ByVal ReceiptType As String, ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSchemeBenificiaryDairy In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", strSchemeCode)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class


Public Class clsSchemeMasterDairySlab
#Region "variables"
    Public Scheme_Code As String = Nothing
    Public Min_Range As Double
    Public Value As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsSchemeMasterDairySlab), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsSchemeMasterDairySlab In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Scheme_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Min_Range", obj.Min_Range)
                    clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW_SLAB", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsSchemeMasterDairySlab)
        Dim arr As New List(Of clsSchemeMasterDairySlab)
        Dim qry As String = "Select TSPL_SCHEME_MASTER_NEW_SLAB.* from TSPL_SCHEME_MASTER_NEW_SLAB Where Scheme_Code='" + strCode + "' order by Min_Range "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsSchemeMasterDairySlab()
                obj.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                obj.Min_Range = clsCommon.myCdbl(dr("Min_Range"))
                obj.Value = clsCommon.myCdbl(dr("Value"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class


Public Class clsSchemeMasterDairyQuantativeSlab
#Region "variables"
    Public Scheme_Code As String = Nothing
    Public Min_Range As Double
    Public Value As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsSchemeMasterDairyQuantativeSlab), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsSchemeMasterDairyQuantativeSlab In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Scheme_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Min_Range", obj.Min_Range)
                    clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW_QUANTATIVE_SLAB", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsSchemeMasterDairyQuantativeSlab)
        Dim arr As New List(Of clsSchemeMasterDairyQuantativeSlab)
        Dim qry As String = "Select TSPL_SCHEME_MASTER_NEW_QUANTATIVE_SLAB.* from TSPL_SCHEME_MASTER_NEW_QUANTATIVE_SLAB Where Scheme_Code='" + strCode + "' order by Min_Range "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsSchemeMasterDairyQuantativeSlab()
                obj.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                obj.Min_Range = clsCommon.myCdbl(dr("Min_Range"))
                obj.Value = clsCommon.myCdbl(dr("Value"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class
Public Class clsSchemeMasterVolumeSlab
#Region "variables"
    Public Scheme_Code As String = Nothing
    Public Min_Range As Double
    Public Max_Range As Double
    Public Item_Code As String = Nothing
    Public Qty As String = Nothing
    Public Unit_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsSchemeMasterVolumeSlab), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsSchemeMasterVolumeSlab In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Scheme_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Min_Range", obj.Min_Range)
                    clsCommon.AddColumnsForChange(coll, "Max_Range", obj.Max_Range)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_VOLUME_SLAB", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsSchemeMasterVolumeSlab)
        Dim arr As New List(Of clsSchemeMasterVolumeSlab)
        Dim qry As String = "Select TSPL_SCHEME_MASTER_VOLUME_SLAB.* from TSPL_SCHEME_MASTER_VOLUME_SLAB Where Scheme_Code='" + strCode + "' order by Min_Range "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsSchemeMasterVolumeSlab()
                obj.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                obj.Min_Range = clsCommon.myCdbl(dr("Min_Range"))
                obj.Max_Range = clsCommon.myCdbl(dr("Max_Range"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                obj.Qty = clsCommon.myCdbl(dr("Qty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class



Public Class clsSchemeQuantitiveStructure
#Region "variables"
    Public Scheme_Code As String = Nothing
    Public Structure_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For ii As Integer = 0 To Arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Scheme_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Structure_Code", clsCommon.myCstr(Arr(ii)))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_QUANTITIVE_STRUCTURE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "Select TSPL_SCHEME_QUANTITIVE_STRUCTURE.* from TSPL_SCHEME_QUANTITIVE_STRUCTURE Where Scheme_Code='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("Structure_Code")))
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsCashDiscountVolumneDetail
#Region "Variables"
    Public Scheme_Code As String = Nothing
    Public SNO As Integer = 0
    Public FROM_RANGE As Double = 0.0
    Public TO_RANGE As Double = 0.0
    Public Discount As Double = 0.0

#End Region

    Public Shared Function SaveData(ByVal strSchemeCode As String, ByVal Arr As List(Of clsCashDiscountVolumneDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCashDiscountVolumneDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", strSchemeCode)
                clsCommon.AddColumnsForChange(coll, "FROM_RANGE", obj.FROM_RANGE)
                clsCommon.AddColumnsForChange(coll, "TO_RANGE", obj.TO_RANGE)
                clsCommon.AddColumnsForChange(coll, "Discount", obj.Discount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_CASHDISVOLUME_SLAB", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCashDiscountVolumneDetail)
        Dim arr As New List(Of clsCashDiscountVolumneDetail)
        Dim qry As String = "Select TSPL_SCHEME_MASTER_CASHDISVOLUME_SLAB.* from TSPL_SCHEME_MASTER_CASHDISVOLUME_SLAB Where Scheme_Code='" + strCode + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsCashDiscountVolumneDetail()
                obj.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                obj.FROM_RANGE = clsCommon.myCdbl(dr("FROM_RANGE"))
                obj.TO_RANGE = clsCommon.myCdbl(dr("TO_RANGE"))
                obj.Discount = clsCommon.myCdbl(dr("Discount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function


End Class

Public Class clsCashDisSturctureMapping
#Region "variables"
    Public Scheme_CODE As String = Nothing
    Public STRUCTURE_CODE As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each strItemStrctCode As String In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Scheme_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "STRUCTURE_CODE", strItemStrctCode)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_CASHDIS_ITEM_STRUCTURE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "Select TSPL_SCHEME_CASHDIS_ITEM_STRUCTURE.* from TSPL_SCHEME_CASHDIS_ITEM_STRUCTURE Where SCHEME_CODE='" + strCode + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("STRUCTURE_CODE")))
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsMilkQuantitySlab
#Region "variables"
    Public Min_Range As Double
    Public Max_Range As Double
    Public Line_No As Integer = 0

#End Region

    Public Function SaveData(ByVal Arr As List(Of clsMilkQuantitySlab)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(Arr, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function SaveData(ByVal Arr As List(Of clsMilkQuantitySlab), ByVal trans As SqlTransaction) As Boolean
        Try
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_Milk_Quantity_Slab", trans)
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsMilkQuantitySlab In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Min_Range", clsCommon.myCdbl(obj.Min_Range))
                    clsCommon.AddColumnsForChange(coll, "Max_Range", clsCommon.myCdbl(obj.Max_Range))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Quantity_Slab", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData() As List(Of clsMilkQuantitySlab)
        Dim arr As New List(Of clsMilkQuantitySlab)
        Dim qry As String = "Select TSPL_Milk_Quantity_Slab.* from TSPL_Milk_Quantity_Slab Where 1=1 order by Line_No "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMilkQuantitySlab()
                obj.Line_No = CInt(dr("Line_No"))
                obj.Min_Range = clsCommon.myCdbl(dr("Min_Range"))
                obj.Max_Range = clsCommon.myCdbl(dr("Max_Range"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
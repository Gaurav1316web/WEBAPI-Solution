Imports common
Imports System.Data.SqlClient

Public Class clsWarehouseBreakage

#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Description As String = Nothing
    Public Posting_Date As DateTime = Nothing
    Public Reference As String = Nothing
    Public Is_Post As Integer = 0
    Public Loc_Code As String = Nothing
    Public Loc_Desc As String = Nothing
    Public Arr As List(Of clsWarehouseDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsWarehouseBreakage, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, "", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsWarehouseBreakage, ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, clsUserMgtCode.frmAdjProductionEntry, obj.Loc_Code, obj.Document_Date, trans)

            Dim qry As String = "delete from TSPL_WH_BREAKAGE_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            obj.Loc_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER Where Location_Code='" + obj.Loc_Code + "'", trans))
            clsCommon.AddColumnsForChange(coll, "Loc_Desc", obj.Loc_Desc)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.warehouseBreakage, "", obj.Loc_Code)
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WH_BREAKAGE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WH_BREAKAGE_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsWarehouseDetail.SaveData(obj.Document_No, Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New clsWarehouseBreakage()
            obj = obj.GetData(strDocNo, NavigatorType.Current, trans)
            If obj Is Nothing Then
                Throw New Exception("No Data Found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmAdjProductionEntry, obj.Loc_Code, obj.Document_Date, trans)
            If obj.Is_Post = 1 Then
                Throw New Exception("Already Posted Transaction :" + strDocNo)
            End If
            '-----------------------------------------------
            Dim qry As String = ""
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim intCounter As Integer = 0
            For Each objTr As clsWarehouseDetail In obj.Arr
                intCounter = intCounter + 1
                Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If

                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_Code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_Code)
                End If

                Dim objLocationDetails As New clsItemLocationDetails()
                objTr.Item_Description = clsItemMaster.GetItemName(objTr.Item_Code, trans)
                objLocationDetails = New clsItemLocationDetails()
                objLocationDetails.Item_Code = objTr.Item_Code
                objLocationDetails.Item_Desc = objTr.Item_Description
                objLocationDetails.Location_Code = obj.Loc_Code
                objLocationDetails.Location_Desc = obj.Loc_Desc
                Dim ItemQty As Double = objTr.Leakage_Qty + objTr.Breakage_Qty + objTr.Shortage_Qty
                objLocationDetails.Item_Qty = ItemQty * -1
                objLocationDetails.Amount = objTr.Item_Cost * ItemQty
                objLocationDetails.MRP = objTr.mrp * ConvFac
                objLocationDetails.ItemType = strItemTypeToSave
                ArrLocationDetails.Add(objLocationDetails)

                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt = New clsInventoryMovement()
                objInventoryMovemnt.InOut = "O"
                objInventoryMovemnt.Location_Code = obj.Loc_Code
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Description
                objInventoryMovemnt.Qty = ItemQty
                objInventoryMovemnt.UOM = objTr.Unit_Code
                objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                objInventoryMovemnt.Add_Cost = 0
                objInventoryMovemnt.Net_Cost = objTr.Item_Cost * ItemQty
                objInventoryMovemnt.ItemType = strItemTypeToSave
                objInventoryMovemnt.MRP = objTr.mrp
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Next

            clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
            clsInventoryMovement.SaveData("WH", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            '-----------------------------------------------

            qry = " update TSPL_WH_BREAKAGE_HEAD  set Is_Post=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "' where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function


    Public Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsWarehouseBreakage

        Dim obj As clsWarehouseBreakage = Nothing
        Dim qry As String = "SELECT * from TSPL_WH_BREAKAGE_HEAD where 2=2"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_WH_BREAKAGE_HEAD.Document_No = (select MIN(Document_No) from TSPL_WH_BREAKAGE_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_WH_BREAKAGE_HEAD.Document_No = (select Max(Document_No) from TSPL_WH_BREAKAGE_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_WH_BREAKAGE_HEAD.Document_No = (select Min(Document_No) from TSPL_WH_BREAKAGE_HEAD where Document_No >'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_WH_BREAKAGE_HEAD.Document_No = (select Max(Document_No) from TSPL_WH_BREAKAGE_HEAD where Document_No <'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_WH_BREAKAGE_HEAD.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsWarehouseBreakage()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Desc = clsCommon.myCstr(dt.Rows(0)("Loc_Desc"))

            qry = "SELECT  * from TSPL_WH_BREAKAGE_DETAIL where  Document_No='" + obj.Document_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsWarehouseDetail)
                Dim objTr As clsWarehouseDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsWarehouseDetail()
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Description = clsCommon.myCstr(dr("Item_Description"))
                    objTr.Item_Quantity = clsCommon.myCdbl(dr("Item_Quantity"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.mrp = clsCommon.myCdbl(dr("mrp"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    objTr.Breakage = clsCommon.myCstr(dr("Breakage"))
                    objTr.Breakage_Qty = clsCommon.myCdbl(dr("Breakage_Qty"))
                    objTr.Leakage_Qty = clsCommon.myCdbl(dr("Leakage_Qty"))
                    objTr.Shortage_Qty = clsCommon.myCdbl(dr("Shortage_Qty"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New clsWarehouseBreakage()
        obj = obj.GetData(strDocNo, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmAdjProductionEntry, obj.Loc_Code, obj.Document_Date, trans)
                If (obj.Is_Post = 1) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If
                Dim qry As String = "delete from TSPL_WH_BREAKAGE_DETAIL where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_WH_BREAKAGE_HEAD where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    ''To be Uncomment
    'Public Shared Sub PrintData(ByVal strAdjustmentNo As String, ByVal IsPreprinted As Boolean, ByVal IsEmpty As Boolean)
    '    Try
    '        Dim qry As String = "select TSPL_WH_BREAKAGE_HEAD.Document_No,TSPL_WH_BREAKAGE_HEAD.Document_Date,TSPL_WH_BREAKAGE_HEAD.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as address,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_WH_BREAKAGE_DETAIL.Unit_Code ,TSPL_WH_BREAKAGE_DETAIL.mrp,TSPL_WH_BREAKAGE_DETAIL.Item_Quantity  ,TSPL_WH_BREAKAGE_DETAIL.Breakage,TSPL_WH_BREAKAGE_DETAIL.Breakage_Qty,TSPL_WH_BREAKAGE_DETAIL.Leakage_Qty,TSPL_WH_BREAKAGE_DETAIL.Shortage_Qty "
    '        qry += ",TSPL_WH_BREAKAGE_HEAD.Created_By as [Created By] ,TSPL_WH_BREAKAGE_HEAD.Modified_By as [Modified By] from tspl_wh_breakage_head left outer join tspl_wh_breakage_detail on tspl_wh_breakage_head.Document_No=tspl_wh_breakage_detail.Document_No left outer join TSPL_LOCATION_MASTER on tspl_wh_breakage_head.Loc_code= TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_COMPANY_MASTER on tspl_wh_breakage_head.comp_code=TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_ITEM_MASTER on TSPL_WH_BREAKAGE_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code where TSPL_WH_BREAKAGE_HEAD.Document_No='" + strAdjustmentNo + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        InventryViewer.funreport(dt, "crptWareHouseBreakage", "Ware Houes Breakage")

    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message)
    '    End Try
    'End Sub
End Class

Public Class clsWarehouseDetail

#Region "Variables"
    Public Document_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Description As String = Nothing
    Public Unit_Code As String = Nothing
    Public Item_Quantity As Double = 0
    Public Item_Cost As Double = 0
    Public mrp As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Breakage As String = Nothing
    Public Breakage_Qty As Double = 0
    Public Leakage_Qty As Double = 0
    Public Shortage_Qty As Double = 0
    Public Remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsWarehouseDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each objtr As clsWarehouseDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", counter)
                counter += 1
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                objtr.Item_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER WHERE Item_Code = '" + objtr.Item_Code + "'", trans))
                clsCommon.AddColumnsForChange(coll, "Item_Description", objtr.Item_Description)
                clsCommon.AddColumnsForChange(coll, "Item_Quantity", objtr.Item_Quantity)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", objtr.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "mrp", objtr.mrp)
                If objtr.MFG_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(objtr.MFG_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Breakage", objtr.Breakage)
                clsCommon.AddColumnsForChange(coll, "Breakage_Qty", objtr.Breakage_Qty)
                clsCommon.AddColumnsForChange(coll, "Leakage_Qty", objtr.Leakage_Qty)
                clsCommon.AddColumnsForChange(coll, "Shortage_Qty", objtr.Shortage_Qty)
                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WH_BREAKAGE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class





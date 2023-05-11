Imports common
Imports System.Data.SqlClient
Public Class clsVendorItemDetail
#Region "Variables"
    Public Item_Type As String = Nothing
    Public vendor_code As String = Nothing
    Public vendor_desc As String = Nothing
    Public item_code As String = Nothing
    Public item_desc As String = Nothing
    Public UOM As String = Nothing
    Public MRP As Double = Nothing
    Public item_rate As Double = Nothing
    Public vendor_item_no As String = Nothing
    Public comp_code As String = Nothing
    Public Start_Date As Date? = Nothing
    Public End_Date As Date? = Nothing
    Public version As Integer
    Public AbatementRate As Double = 0
    Public UOMWeight As String = Nothing
    Public UOMWeightValue As String = Nothing
    Public Bin_No As String = Nothing
    Public loccode As String = Nothing
    Public locname As String = Nothing
#End Region

    Public Function SaveData(ByVal VendorCode As String, ByVal VendorDesc As String, ByVal company As String, ByVal Arr As List(Of clsVendorItemDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Try
            clsVendorItemDetailHistory.SaveDataHistory(VendorCode, Arr, trans)
            Dim qry As String = "delete from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + VendorCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            For Each obj As clsVendorItemDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "vendor_code", VendorCode)
                clsCommon.AddColumnsForChange(coll, "vendor_desc", VendorDesc)
                clsCommon.AddColumnsForChange(coll, "item_no", obj.item_code)
                clsCommon.AddColumnsForChange(coll, "item_desc", obj.item_desc)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "item_rate", obj.item_rate)
                clsCommon.AddColumnsForChange(coll, "AbatementRate", obj.AbatementRate)
                clsCommon.AddColumnsForChange(coll, "vendor_item_no", obj.vendor_item_no)
                clsCommon.AddColumnsForChange(coll, "comp_code", company)
                clsCommon.AddColumnsForChange(coll, "location_code", obj.loccode)
                clsCommon.AddColumnsForChange(coll, "location_name", obj.locname)

                If clsCommon.myLen(obj.Start_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd-MMM-yyyy"))
                End If

                If clsCommon.myLen(obj.End_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd-MMM-yyyy"))
                End If

                clsCommon.AddColumnsForChange(coll, "Version", version)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String) As List(Of clsVendorItemDetail)
        Dim obj As clsVendorItemDetail = Nothing
        Dim qry As String = "select item_no ,item_desc ,uom ,MRP ,item_rate ,vendor_item_no, Start_Date, End_Date,AbatementRate,location_code  from TspL_vendor_item_detail WHERE vendor_code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of clsVendorItemDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As clsVendorItemDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsVendorItemDetail()
                objTr.item_code = clsCommon.myCstr(dr("item_no"))
                objTr.item_desc = clsCommon.myCstr(dr("item_desc"))
                objTr.UOM = clsCommon.myCstr(dr("uom"))
                objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                objTr.item_rate = clsCommon.myCdbl(dr("Item_rate"))
                objTr.AbatementRate = clsCommon.myCdbl(dr("AbatementRate"))
                objTr.vendor_item_no = clsCommon.myCstr(dr("vendor_item_no"))
                objTr.loccode = clsCommon.myCstr(dr("location_code"))
                objTr.locname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + objTr.loccode + "'"))

                If clsCommon.myLen(dr("Start_Date")) <= 0 Then
                    objTr.Start_Date = Nothing
                Else
                    objTr.Start_Date = clsCommon.GetPrintDate(dr("Start_Date"))
                End If

                If clsCommon.myLen(dr("End_Date")) <= 0 Then
                    objTr.End_Date = Nothing
                Else
                    objTr.End_Date = clsCommon.GetPrintDate(dr("End_Date"))
                End If


                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Requistion No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

            Dim obj As clsRequistionHead = clsRequistionHead.GetData(strDocNo, NavigatorType.Current, trans, "N")
            Dim totDrAmt As Double = 0
            Dim totCrAmt As Double = 0

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Requisition_Id) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold = 1) Then
                Throw New Exception("Requistion No " + obj.Requisition_Id + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = "Update TSPL_REQUISITION_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Requisition_Id='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Requisition No not found to Delete")
        End If
        Dim obj As clsRequistionHead = clsRequistionHead.GetData(strCode, NavigatorType.Current, "")
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Requisition_Id) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_REQUISITION_DETAIL where Requisition_Id='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_REQUISITION_HEAD where Requisition_Id='" + strCode + "'"
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

    Public Shared Function IsValidVendorForRequitionItem(ByVal strReqNo As String, ByVal strICode As String, ByVal strVendorCode As String) As Boolean
        Dim qry As String = "select 1 from TSPL_REQUISITION_DETAIL where Requisition_Id ='" + strReqNo + "' and Item_Code='" + strICode + "' and Vendor_Code not in ('','" + strVendorCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function GetItemRateAndMRP(ByVal strVendorCode As String, ByVal strICode As String, ByVal strUOM As String) As clsVendorItemDetail
        Dim obj As clsVendorItemDetail = Nothing

        Dim qry As String = "select Item_Rate,MRP,AbatementRate from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + strVendorCode + "' and item_no='" + strICode + "' and UOM='" + strUOM + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsVendorItemDetail()
            obj.item_rate = clsCommon.myCdbl(dt.Rows(0)("Item_Rate"))
            obj.MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))
            obj.AbatementRate = clsCommon.myCdbl(dt.Rows(0)("AbatementRate"))
        End If
        Return obj
    End Function

    Public Shared Function GetItemRate(ByVal strVendorCode As String, ByVal strICode As String, ByVal strUOM As String, ByVal transDate As DateTime) As clsVendorItemDetail
        Dim obj As clsVendorItemDetail = Nothing

        Dim qry As String = "select Item_Rate,MRP,AbatementRate from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + strVendorCode + "' and item_no='" + strICode + "' and UOM='" + strUOM + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsVendorItemDetail()
            obj.item_rate = clsCommon.myCdbl(dt.Rows(0)("Item_Rate"))
            obj.MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))
            obj.AbatementRate = clsCommon.myCdbl(dt.Rows(0)("AbatementRate"))
        End If
        Return obj
    End Function

    Public Shared Function GetRate(ByVal strVendorCode As String, ByVal strICode As String, ByVal strUOM As String, ByVal MRP As Double) As Double
        Dim qry As String = " select  (Item_Rate /TSPL_ITEM_UOM_DETAIL.Conversion_Factor)*OuterUOM.Conversion_Factor from TSPL_VENDOR_ITEM_DETAIL left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_VENDOR_ITEM_DETAIL.item_no and TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_VENDOR_ITEM_DETAIL.uom left outer join TSPL_ITEM_UOM_DETAIL as OuterUOM on OuterUOM.Item_Code=TSPL_VENDOR_ITEM_DETAIL.item_no and OuterUOM.UOM_Code='" + strUOM + "' "
        qry += " where vendor_code='" + strVendorCode + "' and item_no='" + strICode + "' and MRP='" + clsCommon.myCstr(MRP) + "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function Finder(ByVal strVendorCode As String, ByVal TransDate As DateTime, ByVal loccode As String, ByVal isMCCPurchase As Boolean, ByVal itemType As String) As clsVendorItemDetail
        Dim obj As clsVendorItemDetail = Nothing
        Dim qry As String = " select item_no as Code,Rack_No as Bin_no,TSPL_ITEM_MASTER.Weight_UOM as [Weight_UOM] ,TSPL_ITEM_MASTER.Weight_Value as [Weight_Value] ,TSPL_ITEM_MASTER.Item_Desc as Description,uom,MRP,item_rate,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code], TSPL_VENDOR_ITEM_DETAIL.vendor_item_no as [Vendor Item],TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as PrincipleCode,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as PrincipleDesc "
        qry += " from TSPL_VENDOR_ITEM_DETAIL "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_VENDOR_ITEM_DETAIL.item_no"
        qry += " left  outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values "
        Dim WhrCls As String = " vendor_code='" + strVendorCode + "' and TSPL_ITEM_MASTER.Active=1 and TSPL_ITEM_MASTER.Item_Type in ('" + itemType.Trim() + "') and start_Date <= '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' and (End_Date>'" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' or End_Date is null)"
        If Not isMCCPurchase Then
            WhrCls &= " and TSPL_VENDOR_ITEM_DETAIL.location_code='" & loccode & "'"
        End If
        '' Anubhooti 21-Aug-2014 On Settings
        Dim Is_Purchaseable As Integer = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='Is_Purchaseable_Item'")
        If clsCommon.myCdbl(Is_Purchaseable) = 1 Then
            WhrCls &= " AND TSPL_ITEM_MASTER.Is_Purchaseable='1' "
        End If
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("VendroItede", qry + " where " + WhrCls)
        If dr IsNot Nothing Then
            obj = New clsVendorItemDetail()
            obj.item_code = clsCommon.myCstr(dr("Code"))
            obj.item_desc = clsCommon.myCstr(dr("Description"))
            obj.UOM = clsCommon.myCstr(dr("uom"))
            obj.MRP = clsCommon.myCdbl(dr("MRP"))
            obj.Bin_No = clsCommon.myCdbl(dr("Bin_No"))
            obj.item_rate = clsCommon.myCdbl(dr("item_rate"))
            obj.UOMWeight = clsCommon.myCstr(dr("Weight_UOM"))
            obj.UOMWeightValue = clsCommon.myCdbl(dr("Weight_Value"))

        End If
        Return obj
    End Function
    Public Shared Function FinderWithoutItemType(ByVal strVendorCode As String, ByVal TransDate As DateTime, ByVal loccode As String, ByVal isMCCPurchase As Boolean) As clsVendorItemDetail
        Dim obj As clsVendorItemDetail = Nothing
        Dim qry As String = " select item_no as Code, TSPL_ITEM_MASTER.Item_Type,Rack_No as Bin_no,TSPL_ITEM_MASTER.Weight_UOM as [Weight_UOM] ,TSPL_ITEM_MASTER.Weight_Value as [Weight_Value] ,TSPL_ITEM_MASTER.Item_Desc as Description,uom,MRP,item_rate,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code], TSPL_VENDOR_ITEM_DETAIL.vendor_item_no as [Vendor Item],TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as PrincipleCode,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as PrincipleDesc "
        qry += " from TSPL_VENDOR_ITEM_DETAIL "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_VENDOR_ITEM_DETAIL.item_no"
        qry += " left  outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values "
        Dim WhrCls As String = " vendor_code='" + strVendorCode + "' and TSPL_ITEM_MASTER.Active=1  and start_Date <= '" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' and (End_Date>'" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' or End_Date is null)"
        If Not isMCCPurchase Then
            WhrCls &= " and TSPL_VENDOR_ITEM_DETAIL.location_code='" & loccode & "'"
        End If
        '' Anubhooti 21-Aug-2014 On Settings
        Dim Is_Purchaseable As Integer = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='Is_Purchaseable_Item'")
        If clsCommon.myCdbl(Is_Purchaseable) = 1 Then
            WhrCls &= " AND TSPL_ITEM_MASTER.Is_Purchaseable='1' "
        End If
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("VendroItede", qry + " where " + WhrCls)
        If dr IsNot Nothing Then
            obj = New clsVendorItemDetail()
            obj.item_code = clsCommon.myCstr(dr("Code"))
            obj.item_desc = clsCommon.myCstr(dr("Description"))
            obj.UOM = clsCommon.myCstr(dr("uom"))
            obj.MRP = clsCommon.myCdbl(dr("MRP"))
            obj.Bin_No = clsCommon.myCdbl(dr("Bin_No"))
            obj.item_rate = clsCommon.myCdbl(dr("item_rate"))
            obj.UOMWeight = clsCommon.myCstr(dr("Weight_UOM"))
            obj.UOMWeightValue = clsCommon.myCdbl(dr("Weight_Value"))
            obj.Item_Type = clsCommon.myCstr(dr("Item_Type"))
        End If
        Return obj
    End Function
End Class

Public Class clsVendorItemDetailHistory
    Public Shared Function SaveDataHistory(ByVal VendorCode As String, ByVal Arr As List(Of clsVendorItemDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qryInsert As String = "select vendor_code, vendor_desc, item_no, item_desc, uom, MRP, item_rate, vendor_item_no, Comp_Code, Start_Date, End_Date, Version,AbatementRate  from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + VendorCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryInsert, trans)
            If dt.Rows.Count <= 0 Then
                Return False
            Else
                For Each obj As clsVendorItemDetail In Arr
                    For Each dr As DataRow In dt.Rows
                        If dr("item_no") = obj.item_code And (dr("item_rate") <> obj.item_rate Or dr("Start_Date") <> obj.Start_Date) Then
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "vendor_code", dr("vendor_code"))
                            clsCommon.AddColumnsForChange(coll, "vendor_desc", dr("vendor_desc"))
                            clsCommon.AddColumnsForChange(coll, "item_no", dr("item_no"))
                            clsCommon.AddColumnsForChange(coll, "item_desc", dr("item_desc"))
                            clsCommon.AddColumnsForChange(coll, "UOM", dr("UOM"))
                            clsCommon.AddColumnsForChange(coll, "MRP", dr("MRP"))
                            clsCommon.AddColumnsForChange(coll, "item_rate", dr("item_rate"))
                            clsCommon.AddColumnsForChange(coll, "AbatementRate", dr("AbatementRate"))
                            clsCommon.AddColumnsForChange(coll, "vendor_item_no", dr("vendor_item_no"))
                            clsCommon.AddColumnsForChange(coll, "comp_code", dr("comp_code"))

                            If dr("Start_Date") IsNot DBNull.Value Then
                                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(dr("Start_Date"), "dd-MMM-yyyy"), True)
                                'Dim obj As New clsVendorItemDetail
                            End If


                            Dim Enddate As Date = clsCommon.myCDate(obj.Start_Date).AddDays(-1)
                            clsCommon.AddColumnsForChange(coll, "end_Date", clsCommon.GetPrintDate(Enddate, "dd-MMM-yyyy"), True)



                            clsCommon.AddColumnsForChange(coll, "History_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Version", dr("Version"))
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_ITEM_DETAIL_Hist", OMInsertOrUpdate.Insert, "", trans)
                        End If

                    Next
                Next
            End If

            If isSaved Then
                'trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            'trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function



End Class













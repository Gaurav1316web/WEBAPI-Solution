Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsWreckageBooking
#Region "Variables"

    Public PRODUCTION_ENTRY_CODE As String = Nothing
    Public SNO As Integer = 0
    Public Description As String = Nothing
    Public Item_Code As String = Nothing
    Public PROD_DATE As DateTime
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    Public Posted As String = Nothing
    Public Posting_Date As String = Nothing
    'Public Item_Type As String = Nothing
    'Public Product_Type As String
    Public BACK_QTY As Decimal = 0
    Public WRECKAGE_QTY As Decimal = 0
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public Avail_FAT_Per As Decimal = 0
    Public Avail_SNF_Per As Decimal = 0
    Public Avail_FAT_KG As Decimal = 0
    Public Avail_SNF_KG As Decimal = 0
    Public Remarks As String = Nothing
    Public Comp_Code As String = Nothing
    Public Comment As String = Nothing
    Public Product_Type As String = Nothing
    Public Item_Type As String = Nothing
    Public CONSM_SECTION_CODE As String = Nothing

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public Wrekage_ENTRY_CODE As String
    Public SNF_Amt As Decimal = 0
    Public FINAL_PRODUCTION_QTY As Decimal = 0

    Public START_TIME As DateTime? = Nothing
    Public END_TIME As DateTime? = Nothing

    Public MFG_DATE As Date
    Public EXP_DATE As Date
    'Public TR_TYPE As String
    'Public MO_CODE As String
    Public FIFO_Cost As Decimal
    Public LIFO_Cost As Decimal
    Public AVG_Cost As Decimal
    Public Costing_Method As Integer
    Public Publish As Boolean
    Public FAT_Per As Decimal
    Public SNF_Per As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Category As String = Nothing
    Public Scrap_Qty As Decimal = 0
    Public ScrapLocation As String
    '' production costing columns
  
    Public WFBook As List(Of clsWreckageBooking) = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As clsWreckageBooking, ByVal isNewEntry As Boolean, ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsWreckageBooking, ByVal isNewEntry As Boolean, ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmInvoiceBulkSale, obj.Location_Code, obj.PROD_DATE, trans)
            Dim isSaved As Boolean = True
            If isNewEntry Then
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.Wrekage_ENTRY_CODE = clsERPFuncationality.GetNextCode(trans, obj.PROD_DATE, clsDocType.WreckageBooking, "", obj.Location_Code)
                End If
            Else
                obj.Wrekage_ENTRY_CODE = strCode
            End If

            Dim strDocNo As String = ""
            'If (clsCommon.myLen(obj.Wrekage_ENTRY_CODE) <= 0) Then
            '    Throw New Exception("Error in Document Code Generation")
            'End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "WRECKAGE_ENTRY_CODE", obj.Wrekage_ENTRY_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", clsCommon.myCstr(obj.Description))
            clsCommon.AddColumnsForChange(coll, "PROD_DATE", clsCommon.GetPrintDate(obj.PROD_DATE, "dd/MMM/yyyy"))

            ' clsCommon.AddColumnsForChange(coll, "RECEIVED_BY", clsCommon.myCstr(obj.RECEIVED_BY), True)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(obj.Location_Code))
            clsCommon.AddColumnsForChange(coll, "COMMENTS", clsCommon.myCstr(obj.Comment))
            clsCommon.AddColumnsForChange(coll, "CONSM_SECTION_CODE", clsCommon.myCstr(obj.CONSM_SECTION_CODE))
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Category", clsCommon.myCstr(obj.Category))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then


                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE = '" & obj.Wrekage_ENTRY_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WRECKAGE_ENTRY", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.Wrekage_ENTRY_CODE + " Is Already Exist")


                End If
            Else
                HistoryUpdate(obj.Wrekage_ENTRY_CODE, trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WRECKAGE_ENTRY", OMInsertOrUpdate.Update, "TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE='" + obj.Wrekage_ENTRY_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsWreckage.SaveData(obj, trans)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_WRECKAGE_ENTRY", "WRECKAGE_ENTRY_CODE", "TSPL_WRECKAGE_BOOKING", "WRECKAGE_CODE", trans)
        Return True
    End Function
    Public Shared Function GetWreckageDetail(ByVal WRECKAGE_CODE As String, ByVal trans As SqlTransaction) As List(Of clsWreckageBooking)
        Dim objWFList As New List(Of clsWreckageBooking)
        Dim qry As String = "select TSPL_WRECKAGE_BOOKING.*,TSPL_ITEM_MASTER.ITEM_DESC,TSPL_ITEM_MASTER.ITEM_TYPE,isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc from TSPL_WRECKAGE_BOOKING left join TSPL_ITEM_MASTER on TSPL_WRECKAGE_BOOKING.Item_Code=TSPL_ITEM_MASTER.ITEM_CODE  left join TSPL_UNIT_MASTER on TSPL_WRECKAGE_BOOKING.Unit_Code=TSPL_UNIT_MASTER.Unit_Code  where TSPL_WRECKAGE_BOOKING.comp_code='" + objCommonVar.CurrentCompanyCode + "' and WRECKAGE_CODE='" + WRECKAGE_CODE + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsWreckageBooking()
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.BACK_QTY = dr("BACK_QTY")
                objtr.WRECKAGE_QTY = dr("WRECKAGE_QTY")
                objtr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objtr.Avail_SNF_KG = dr("Avail_SNF_KG")
                objtr.Avail_SNF_Per = dr("Avail_SNF_Per")
                objtr.Avail_FAT_KG = dr("Avail_FAT_KG")
                objtr.Avail_FAT_Per = dr("Avail_FAT_Per")
                objtr.Comp_Code = clsCommon.myCstr(dr("Comp_Code"))
                objtr.Scrap_Qty = clsCommon.myCdbl(dr("ScrapQty"))
                objtr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))

                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                objtr.PRODUCTION_ENTRY_CODE = clsCommon.myCstr(dr("WRECKAGE_CODE"))
                objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objtr.Unit_Desc = clsCommon.myCstr(dr("Unit_Desc"))
                objtr.ScrapLocation = clsCommon.myCstr(dr("ScrapLocation"))
                objtr.Scrap_Qty = clsCommon.myCdbl(dr("ScrapQty"))
                '' production costing
                objtr.Fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))

                objWFList.Add(objtr)
            Next
        End If
        Return objWFList
    End Function
    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso UnpostData(strCode, FormId, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function UnpostData(ByVal strCode As String, ByVal FormId As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select PROD_DATE,LOCATION_CODE from TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmInvoiceBulkSale, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("PROD_DATE")), trans)


            End If
            Dim issaved As Boolean = True

            Dim qry As String
            HistoryUpdate(strCode, trans)
            ''RICHA AGARWAL 17 aUG,2018 BHA/17/08/18-000454
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where  Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            ''-----------

            qry = "update tspl_batch_Item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & strCode & "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            ''TEC/14/02/19-000426 by Richa on 14/02/2019
            Dim strFormID As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when Category='Scrap' then 'Prod-Scrap' else 'PROD_WR' end from TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE ='" & strCode & "'", trans))

            qry = "delete from tspl_inventory_movement where trans_type='" + strFormID + "' and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='" + strFormID + "' and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_WRECKAGE_ENTRY set Posted='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where WRECKAGE_ENTRY_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, Optional ByVal Trans As SqlTransaction = Nothing) As clsWreckageBooking
        Try
            ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim obj As New clsWreckageBooking()

            Dim LocCond As String = " where 1=1 "
            If clsCommon.myLen(arrloc) > 0 Then
                LocCond = LocCond & " and e1.LOCATION_CODE in (" + arrloc + ")"
            End If

            Dim qry As String = "SELECT e1.COMMENTS,e1.comp_code,e1.CONSM_LOCATION_CODE,e1.CONSM_SECTION_CODE,e1.DESCRIPTION,e1.POSTED,e1.POSTING_DATE,e1.PROD_DATE,e1.WRECKAGE_ENTRY_CODE,e2.Avail_FAT_KG,e2.Avail_FAT_Per,e2.Avail_SNF_KG,e2.Avail_SNF_Per,e2.BACK_QTY,e2.Fat_Amt,e2.Fat_Rate,e2.Item_Code,e1.Location_Code,e2.Remarks,e2.SNF_Amt,e2.SNF_Rate,e2.SNO,e2.Unit_Code,e2.WRECKAGE_CODE,e2.WRECKAGE_QTY,e3.Location_Desc,e1.Category,e2.ScrapLocation,e2.ScrapQty FROM TSPL_WRECKAGE_ENTRY as e1 inner join TSPL_WRECKAGE_BOOKING as e2 on e1.WRECKAGE_ENTRY_CODE=e2.WRECKAGE_CODE INNER JOIN TSPL_LOCATION_MASTER e3 ON e1.LOCATION_CODE=e3.LOCATION_CODE" & LocCond & " "

            Select Case NavType
                Case NavigatorType.First
                    qry += " AND WRECKAGE_ENTRY_CODE = (select MIN(WRECKAGE_ENTRY_CODE) from TSPL_WRECKAGE_ENTRY where location_code in (" + arrloc + "))"
                Case NavigatorType.Last
                    qry += " AND WRECKAGE_ENTRY_CODE = (select Max(WRECKAGE_ENTRY_CODE) from TSPL_WRECKAGE_ENTRY where location_code in (" + arrloc + "))"
                Case NavigatorType.Next
                    qry += " AND WRECKAGE_ENTRY_CODE = (select Min(WRECKAGE_ENTRY_CODE) from TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE>'" + strCode + "' and location_code in (" + arrloc + "))"
                Case NavigatorType.Previous
                    qry += " AND WRECKAGE_ENTRY_CODE = (select Max(WRECKAGE_ENTRY_CODE) from TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE<'" + strCode + "' and location_code in (" + arrloc + "))"
                Case NavigatorType.Current
                    qry += " AND WRECKAGE_ENTRY_CODE = '" + strCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj.Wrekage_ENTRY_CODE = dt.Rows(0)("WRECKAGE_ENTRY_CODE")
                obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
                obj.PROD_DATE = clsCommon.myCDate(dt.Rows(0)("PROD_DATE"))
                obj.Comment = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Category = clsCommon.myCstr(dt.Rows(0)("Category"))
                obj.CONSM_SECTION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_SECTION_CODE"))
                obj.Publish = clsCommon.myCstr(dt.Rows(0)("POSTED"))
                strCode = dt.Rows(0)("WRECKAGE_CODE")
                'obj.ScrapLocation = dt.Rows(0)("ScrapLocation")
                'obj.Scrap_Qty = dt.Rows(0)("ScrapQty")
                If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                Else
                    obj.Posting_Date = Nothing
                End If
            End If
            ' Trans.Commit()
            obj.WFBook = clsWreckageBooking.GetWreckageDetail(strCode, Trans)

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function CheckValidCode(ByVal Doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select count(*) from TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE='" & Doc_No & "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " SELECT TSPL_WRECKAGE_ENTRY.[WRECKAGE_ENTRY_CODE] as Code,TSPL_WRECKAGE_ENTRY.[PROD_DATE],TSPL_WRECKAGE_ENTRY.[Batch_Code] as BatchCode,TSPL_WRECKAGE_ENTRY.[comp_code] as Company,TSPL_WRECKAGE_ENTRY.Category as Category, Created_By as [Created By] ,convert(varchar, Created_Date,103) as [Created Date], Modified_By as [Modified By], convert(varchar, Modified_Date,103) as [Modified Date] FROM [TSPL_WRECKAGE_ENTRY] "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_WRECKAGE_ENTRY.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_WRECKAGE_ENTRY.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("STD", qry, "Code", whrCls, currCode, "Code", isButtonClicked, "PROD_DATE")

        Return str
    End Function
  
    Public Shared Function Finder(ByVal whrcls As String, ByVal StrLocation As String, ByVal ArrList As ArrayList) As ArrayList
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " Location_Code='" & Loc_Code & "'"
        Else
            whrcls = whrcls
        End If

        Dim qry As String = "SELECT Location_Code as Code,Location_Desc as Description,Location_Category as Category,Location_Type as Type,Main_Location_Code FROM TSPL_LOCATION_MASTER WHERE Main_Location_Code='" & StrLocation & "' AND Is_Section='Y' AND Is_Consumption_Location=1 "

        Dim arr1 As New ArrayList
        arr1 = clsCommon.ShowMultipleSelectForm("scjs", qry, "Code", "Code", ArrList, Nothing)

        Return arr1
    End Function
    Public Shared Function GetFind(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""

        Dim qry As String = "SELECT Location_Code as Code,Location_Desc as Description,Location_Category as Category,Location_Type as Type,Main_Location_Code FROM TSPL_LOCATION_MASTER"
        If clsCommon.myLen(whrcls) > 0 Then
            whrcls = " Main_Location_Code='" + curcode + "' AND Is_Section='Y' AND Is_Consumption_Location='1' "
        Else
            whrcls = " Main_Location_Code='" + curcode + "' AND Is_Section='Y' AND Is_Consumption_Location='1' "
        End If
        str = clsCommon.ShowSelectForm("LOCMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function
    Public Shared Function getSectionStockItemMultipleFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal TransType As String, ByVal ArrList As ArrayList) As ArrayList
        Dim str As String = ""
        Dim qry As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " Location_Code='" & Loc_Code & "'"
        Else
            whrcls = whrcls
        End If
        If clsCommon.myCstr(TransType) = "Scrap" Then
            qry = " select *  from ( select distinct Item_Code as Code,Item_Desc as [Name],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                          " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                          "  group by Item_Code,Item_Desc,Stock_UOM " & _
                          " union all " & _
                          " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                          " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " & _
                          " group by Item_Code,Item_Desc,Stock_UOM) as finder "
        Else
            qry = " select *  from ( select distinct Item_Code as Code,Item_Desc as [Name],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
                          " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
                          " where Location_Code='" & Loc_Code & "'  group by Item_Code,Item_Desc,Stock_UOM " & _
                          " union all " & _
                          " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
                          " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW where Location_Code='" & Loc_Code & "' " & _
                          " group by Item_Code,Item_Desc,Stock_UOM) as finder "

        End If
        'Dim qry As String = " select *  from ( select distinct Item_Code as Code,Item_Desc as [Name],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," & _
        '                   " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " & _
        '                   " where Location_Code='" & Loc_Code & "'  group by Item_Code,Item_Desc,Stock_UOM " & _
        '                   " union all " & _
        '                   " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " & _
        '                   " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW where Location_Code='" & Loc_Code & "' " & _
        '                   " group by Item_Code,Item_Desc,Stock_UOM) as finder "

        Dim arr1 As New ArrayList
        arr1 = clsCommon.ShowMultipleSelectForm("scjs", qry, "Code", "Name", ArrList, Nothing)

        Return arr1
    End Function
    Public Shared Function getItemFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal TransType As String, ByVal ArrList As ArrayList) As ArrayList
        Dim str As String = ""
        Dim qry As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " Location_Code='" & Loc_Code & "'"
        Else
            whrcls = whrcls
        End If
        qry = "Select Item_Code as Code, Item_Desc as Name from TSPL_ITEM_MASTER where tspl_item_master.Active ='1'  "
        Dim arr1 As New ArrayList
        arr1 = clsCommon.ShowMultipleSelectForm("Item_Finder", qry, "Code", "Name", ArrList, Nothing)
        Return arr1
    End Function

    Public Shared Function getFGItemFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal TransType As String, ByVal ArrList As ArrayList) As ArrayList
        Dim str As String = ""
        Dim qry As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " Location_Code='" & Loc_Code & "'"
        Else
            whrcls = whrcls
        End If
        qry = "Select Item_Code as Code, Item_Desc as Name from TSPL_ITEM_MASTER where  Item_Category_Struct_Code = 'FG' and  tspl_item_master.Active ='1'  "
        Dim arr1 As New ArrayList
        arr1 = clsCommon.ShowMultipleSelectForm("FG@Item_Finder", qry, "Code", "Name", ArrList, Nothing)
        Return arr1
    End Function

    Public Shared Function GetName(ByVal whrcls As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "SELECT Location_Desc as Description FROM TSPL_LOCATION_MASTER"
            If clsCommon.myLen(whrcls) > 0 Then
                whrcls = " Location_Code='" + strLocation + "' AND Is_Section='Y' AND Is_Consumption_Location='1' "

            Else
                whrcls = " Location_Code='" + strLocation + "' AND Is_Section='Y' AND Is_Consumption_Location='1' "
            End If
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Shared Function Post(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(Form_Id, strDocNo, arrloc, isCheckForPosted, trans, "")
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction, ByVal strVourcherNoForRecreateOnly As String) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If

        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
        Dim obj As clsWreckageBooking = clsWreckageBooking.GetData(strDocNo, arrloc, NavigatorType.Current, trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmInvoiceBulkSale, obj.Location_Code, obj.PROD_DATE, trans)


        If (obj Is Nothing OrElse clsCommon.myLen(obj.Wrekage_ENTRY_CODE) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (isCheckForPosted AndAlso obj.Posted = 1) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If
        HistoryUpdate(strDocNo, trans)
        clsWreckageBooking.UpdateInventoryMovement(Form_Id, obj.Wrekage_ENTRY_CODE, obj.Category, arrloc, trans)
        ''richa BHA/03/08/18-000385
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans), "1") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Category, "Warehouse WRECKAGE") = CompairStringResult.Equal Then
            JournalEntryWIP(trans, obj.Wrekage_ENTRY_CODE, strVourcherNoForRecreateOnly)
        End If
        ''------------
        Dim qry As String = "Update TSPL_WRECKAGE_ENTRY set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where WRECKAGE_ENTRY_CODE ='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function
    Public Shared Function JournalEntryWIP(ByVal trans As SqlTransaction, ByVal Doc_Code As String, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Dim isSaved As Boolean = True
        Dim VoucherDesc As String = ""
        Dim SourceDocDesc As String = ""
        Dim SourceDocNo As String
        Dim Comments As String
        Dim Remarks As String
        Dim RecoControlACC As String = ""
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            RecoControlACC = "I"
        End If
        Dim i As Integer = 0
        Try
            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim TotalDebitAmt As Decimal = 0
            Dim TotalCreditAmt As Decimal = 0
            Dim obj As clsWreckageBooking = clsWreckageBooking.GetData(Doc_Code, "", NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            VoucherDesc = "Financial Entry for Wreckage -" & obj.Wrekage_ENTRY_CODE & " "
            SourceDocDesc = "Production Wreckage"
            SourceDocNo = obj.Wrekage_ENTRY_CODE
            Comments = "Production Wreckage"
            Remarks = "Production Wreckage"


            'If clsCommon.CompairString(obj.Category, "Wreckage") = CompairStringResult.Equal Then
            qry = " Select TSPL_WRECKAGE_ENTRY.Category,TSPL_WRECKAGE_BOOKING.Item_Code,TSPL_ITEM_MASTER.Product_Type,TSPL_WRECKAGE_BOOKING.BACK_QTY,TSPL_WRECKAGE_BOOKING.ScrapQty ,TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY  from TSPL_WRECKAGE_ENTRY " & _
            " left outer join  TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE " & _
            " left outer join TSPL_ITEM_MASTER on TSPL_WRECKAGE_BOOKING.Item_Code =TSPL_ITEM_MASTER.Item_Code " & _
            " where TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE ='" & obj.Wrekage_ENTRY_CODE & "'"
            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                qry = "  Select TSPL_PURCHASE_ACCOUNTS.WIP_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Wrekage_Account  from TSPL_PURCHASE_ACCOUNTS left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code  ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.myCdbl(grow.Item("WRECKAGE_QTY")) > 0 Then
                        If clsCommon.myLen(dt.Rows(0)("Wrekage_Account")) <= 0 Then
                            Throw New Exception("Wreckage Control Account not found for Item " & clsCommon.myCstr(grow.Item("Item_Code")) & "")
                        End If
                    ElseIf clsCommon.myCdbl(grow.Item("BACK_QTY")) > 0 Or clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                        If clsCommon.myLen(dt.Rows(0)("Inv_Control_Account")) <= 0 Then
                            Throw New Exception("Inventory Control Account not found for Item " & clsCommon.myCstr(grow.Item("Item_Code")) & "")
                        End If
                    End If

                    If clsCommon.myLen(dt.Rows(0)("WIP_Account")) <= 0 Then
                        Throw New Exception("WIP Account not found for Item " & clsCommon.myCstr(grow.Item("Item_Code")) & "")
                    End If
                    Dim strMainItemWreckageAcc As String = String.Empty
                    If clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                        Dim strMainItemOfScrapItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Scrap_Item_Code from TSPL_ITEM_MASTER where Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' and Is_Scrap_Item ='1'", trans))
                        If clsCommon.myLen(strMainItemOfScrapItem) > 0 Then
                            strMainItemWreckageAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select TSPL_PURCHASE_ACCOUNTS.Wrekage_Account from TSPL_PURCHASE_ACCOUNTS left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code in (Select Scrap_Item_Code from TSPL_ITEM_MASTER where Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' and Is_Scrap_Item ='1')", trans))
                            If clsCommon.myLen(strMainItemWreckageAcc) <= 0 Then
                                Throw New Exception("Wreckage Control not found for Item " & strMainItemOfScrapItem & "")
                            End If
                        Else
                            Throw New Exception("Please Map Main item for scrap Item " & clsCommon.myCstr(grow.Item("Item_Code")) & " in Item Master")
                        End If

                    End If
                    Dim AVG_COST As Double = 0
                    If clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Item("Product_Type")), "MI") = CompairStringResult.Equal Then
                            AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT_NEW WHERE Source_Doc_No ='" & obj.Wrekage_ENTRY_CODE & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "'", trans))
                        Else
                            AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT WHERE Source_Doc_No ='" & obj.Wrekage_ENTRY_CODE & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' ", trans))
                        End If
                    Else
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Item("Product_Type")), "MI") = CompairStringResult.Equal Then
                            AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT_NEW WHERE Source_Doc_No ='" & obj.Wrekage_ENTRY_CODE & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' AND InOut ='O'", trans))
                        Else
                            AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" sELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT WHERE Source_Doc_No ='" & obj.Wrekage_ENTRY_CODE & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("Item_Code")) & "' ", trans))
                        End If
                    End If

                    Dim CreditAcc As String = String.Empty
                    If clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                        CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(strMainItemWreckageAcc), obj.Location_Code, trans)
                    Else
                        If clsCommon.CompairString(obj.Category, "Warehouse Wreckage") = CompairStringResult.Equal Then
                            CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account")), obj.Location_Code, trans)
                        Else
                            CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("WIP_Account")), obj.Location_Code, trans)
                        End If

                    End If
                    If clsCommon.myLen(CreditAcc) > 0 Then
                        Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(AVG_COST)}
                        ArryLstGLAC.Add(Acc2)
                    End If

                    Dim DebitAcc As String = String.Empty
                    If clsCommon.myCdbl(grow.Item("WRECKAGE_QTY")) > 0 Then
                        DebitAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Wrekage_Account")), obj.Location_Code, trans)
                    ElseIf clsCommon.myCdbl(grow.Item("BACK_QTY")) > 0 Or clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                        DebitAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account")), obj.Location_Code, trans)
                    End If
                    ''TEC/14/02/19-000426 by Richa on 14/02/2019
                    If clsCommon.myCdbl(grow.Item("BACK_QTY")) > 0 Or clsCommon.myCdbl(grow.Item("ScrapQty")) > 0 Then
                        If clsCommon.myLen(DebitAcc) > 0 Then
                            Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(AVG_COST), "", "", "", "", "", "", RecoControlACC}
                            ArryLstGLAC.Add(Acc2)
                            If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(Doc_Code), IIf(clsCommon.CompairString(obj.Category, "Scrap") = CompairStringResult.Equal, "Prod-Scrap", "PROD_WR"), clsCommon.myCstr(grow.Item("Item_Code")), DebitAcc, "", "I", trans)
                            End If
                        End If
                        ''------------------
                    Else
                        If clsCommon.myLen(DebitAcc) > 0 Then
                            Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(AVG_COST)}
                            ArryLstGLAC.Add(Acc2)
                        End If
                    End If


                    TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(AVG_COST)
                    TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(AVG_COST)
                End If

            Next



            Dim GLDesc As String = "Journal Entry Against Production Wreckage- Doc No." & obj.Wrekage_ENTRY_CODE & " "

            Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PP-WR' and Source_Doc_No='" & obj.Wrekage_ENTRY_CODE & "'", trans))
            If clsCommon.myLen(VoucherNo) > 0 Then
                isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, VoucherNo, trans, obj.PROD_DATE, GLDesc, "PP-WR", "Production Wreckage", obj.Wrekage_ENTRY_CODE, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
            Else
                isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.PROD_DATE, GLDesc, "PP-WR", "Production Wreckage", obj.Wrekage_ENTRY_CODE, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
            End If
            'End If
            Return isSaved
        Catch ex As Exception

            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function GetIssueAvgRate(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As MIlkComponentType
        Dim obj As New MIlkComponentType
        Dim qry As String = "select coalesce(sum(Fat_Amt)/case when sum(Avail_FAT_KG)=0 then 1 else sum(Avail_FAT_KG) end,0) as Fat_Rate," & _
                " coalesce(sum(SNF_Amt)/case when sum(Avail_SNF_KG)=0 then 1 else sum(Avail_SNF_KG) end,0) as SNF_Rate from ( " & _
                " select Avail_FAT_KG,Avail_SNF_KG,Fat_Amt,SNF_Amt from TSPL_PP_PE_ISSUE_ITEM_DETAIL where PROD_ENTRY_CODE='" & Doc_Code & "' " & _
                " union all " & _
                " select Avail_FAT_KG,Avail_SNF_KG,Fat_Amt,SNF_Amt from TSPL_PP_PE_WRECKAGE_FLASHING where PROD_ENTRY_CODE='" & Doc_Code & "' " & _
                " ) as TotalIssue"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            obj.FAT_Cost = dt.Rows(0).Item("Fat_Rate")
            obj.SNF_Cost = dt.Rows(0).Item("SNF_Rate")
        End If
        Return obj
    End Function

    Public Shared Function UpdateInventoryMovement(ByVal form_id As String, ByVal ReceiptCode As String, ByVal Category As String, ByVal arrloc As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As clsInventoryMovement = Nothing
            Dim obj1 As clsWreckageBooking = Nothing
            Dim objNew As clsInventoryMovementNew = Nothing
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim strq As String = ""
            Dim objRec As clsWreckageBooking = clsWreckageBooking.GetData(ReceiptCode, arrloc, NavigatorType.Current, trans)
            Dim objListProd As List(Of clsWreckageBooking) = objRec.WFBook
            Dim objListBack As List(Of clsWreckageBooking) = objRec.WFBook

            If clsCommon.myCstr(Category) = "Wreckage" Then
                '' in item qty on back to location
                If (objListBack IsNot Nothing AndAlso objListBack.Count > 0) Then
                    For Each objBack As clsWreckageBooking In objListBack
                        If clsCommon.myCdbl(objBack.BACK_QTY) <= 0 Then
                            Continue For
                        End If
                        Dim strItemTypeToSave As String = ""
                        Dim strItemType As String
                        Dim strProductType As String
                        '' in produced item into selected back to location
                        strProductType = clsItemMaster.GetItemProductType(objBack.Item_Code, trans)
                        If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                            objNew = New clsInventoryMovementNew
                            objNew.Trans_Type = "Production-Return"
                            objNew.InOut = "I"
                            objNew.Location_Code = objBack.Location_Code
                            objNew.Item_Code = objBack.Item_Code
                            objNew.Item_Desc = objBack.Item_Desc
                            objNew.Qty = objBack.BACK_QTY
                            objNew.UOM = objBack.Unit_Code
                            objNew.Source_Doc_No = objRec.Wrekage_ENTRY_CODE
                            objNew.Source_Doc_Date = clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy")

                            objNew.FAT_Per = objBack.Avail_FAT_Per
                            objNew.SNF_Per = objBack.Avail_SNF_Per
                            objNew.FAT_KG = objBack.Avail_FAT_KG
                            objNew.SNF_KG = objBack.Avail_SNF_KG
                            '  objNew.Batch_No = objRec.Batch_Code

                            '' UPDATE PRODUCTION COST
                            objNew.Fat_Rate = objBack.Fat_Rate
                            objNew.SNF_Rate = objBack.SNF_Rate
                            objNew.Fat_Amt = objBack.Fat_Amt
                            objNew.SNF_Amt = objBack.SNF_Amt

                            strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
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
                            objNew.ItemType = strItemTypeToSave

                            objNew.Basic_Cost = 0
                            objNew.MRP = 0
                            objNew.Add_Cost = 0
                            objNew.MRP = 0
                            objNew.MFG_Date = objRec.PROD_DATE
                            objNew.IS_CONSUMPTION = 3

                            ''richa 9 Aug,2018 KDI/23/08/18-000421

                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                                Dim objCost As New MIlkComponentType
                                objCost = clsInventoryMovementNew.GetAvgCost(clsItemMaster.GetItemProductType(objBack.Item_Code, trans), objBack.Item_Code, IIf(clsCommon.myLen(objRec.CONSM_SECTION_CODE) > 0, objRec.CONSM_SECTION_CODE, objRec.Location_Code), objBack.BACK_QTY, objBack.Unit_Code, objBack.Avail_FAT_KG, objBack.Avail_SNF_KG, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), False, trans)
                                objNew.Fat_Rate = objCost.FAT_Cost / IIf(objBack.Avail_FAT_KG <= 0, 1, objBack.Avail_FAT_KG)
                                objNew.SNF_Rate = objCost.SNF_Cost / IIf(objBack.Avail_SNF_KG <= 0, 1, objBack.Avail_SNF_KG)
                                objNew.Fat_Amt = objCost.FAT_Cost
                                objNew.SNF_Amt = objCost.SNF_Cost
                                objNew.Avg_Cost = objNew.Fat_Amt + objNew.SNF_Amt
                                objNew.LIFO_Cost = objNew.Avg_Cost
                                objNew.FIFO_Cost = objNew.Avg_Cost
                                objNew.CalculateAvgCost = False
                            End If

                            ''------------------

                            ArrInventoryMovementNew.Add(objNew)
                        Else
                            obj = New clsInventoryMovement
                            obj.Trans_Type = "Production-Return"
                            obj.InOut = "I"
                            obj.Location_Code = objBack.Location_Code
                            obj.Item_Code = objBack.Item_Code
                            obj.Item_Desc = objBack.Item_Desc
                            obj.Qty = objBack.BACK_QTY
                            obj.UOM = objBack.Unit_Code
                            obj.Source_Doc_No = objRec.Wrekage_ENTRY_CODE
                            obj.Source_Doc_Date = clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy")

                            strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)

                            '  obj.Batch_No = objRec.Batch_Code

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

                            ''==============================================================
                            obj.FAT_Per = objBack.Avail_FAT_Per
                            obj.SNF_Per = objBack.Avail_SNF_Per
                            obj.FAT_KG = objBack.Avail_FAT_KG
                            obj.SNF_KG = objBack.Avail_SNF_KG

                            '' UPDATE PRODUCTION COST
                            obj.Fat_Rate = objBack.Fat_Rate
                            obj.SNF_Rate = objBack.SNF_Rate
                            obj.Fat_Amt = objBack.Fat_Amt
                            obj.SNF_Amt = objBack.SNF_Amt
                            ''==============================================================

                            obj.ItemType = strItemTypeToSave

                            obj.Basic_Cost = 0
                            obj.MRP = 0
                            obj.Add_Cost = 0
                            obj.MRP = 0
                            obj.MFG_Date = objRec.PROD_DATE
                            obj.IS_CONSUMPTION = 3

                            ''richa 9 Aug,2018
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                                Dim QtyInStockUOM As Decimal = objBack.BACK_QTY * clsItemMaster.GetConvertionFactor(objBack.Item_Code, objBack.Unit_Code, trans)
                                obj.Avg_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objBack.Item_Code, IIf(clsCommon.myLen(objRec.CONSM_SECTION_CODE) > 0, objRec.CONSM_SECTION_CODE, objRec.Location_Code), QtyInStockUOM, objRec.PROD_DATE, clsCommon.GETSERVERDATE(trans), True, trans)
                                obj.LIFO_Cost = obj.Avg_Cost
                                obj.FIFO_Cost = obj.Avg_Cost
                                obj.CalculateAvgCost = False
                            End If
                            ''------------------
                            ArrInventoryMovement.Add(obj)
                        End If
                    Next
                End If
                '' out item qty from section location
                If (objListBack IsNot Nothing AndAlso objListBack.Count > 0) Then
                    For Each objBack As clsWreckageBooking In objListBack
                        If clsCommon.myCdbl(objBack.BACK_QTY) <= 0 And clsCommon.myCdbl(objBack.WRECKAGE_QTY) <= 0 Then
                            Continue For
                        End If
                        Dim strItemTypeToSave As String = ""
                        Dim strItemType As String
                        Dim strProductType As String
                        '' in produced item into selected back to location
                        strProductType = clsItemMaster.GetItemProductType(objBack.Item_Code, trans)
                        If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                            objNew = New clsInventoryMovementNew
                            objNew.Trans_Type = form_id '"Production-Return"
                            objNew.InOut = "O"
                            '  objNew.Location_Code = objRec.CONSM_SECTION_CODE
                            objNew.Item_Code = objBack.Item_Code
                            objNew.Item_Desc = objBack.Item_Desc
                            objNew.Qty = IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY)
                            objNew.UOM = objBack.Unit_Code
                            objNew.Source_Doc_No = objRec.Wrekage_ENTRY_CODE
                            objNew.Source_Doc_Date = clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy")

                            objNew.FAT_Per = objBack.Avail_FAT_Per
                            objNew.SNF_Per = objBack.Avail_SNF_Per
                            objNew.FAT_KG = objBack.Avail_FAT_KG
                            objNew.SNF_KG = objBack.Avail_SNF_KG
                            '  objNew.Batch_No = objRec.Batch_Code

                            '' UPDATE PRODUCTION COST
                            objNew.Fat_Rate = objBack.Fat_Rate
                            objNew.SNF_Rate = objBack.SNF_Rate
                            objNew.Fat_Amt = objBack.Fat_Amt
                            objNew.SNF_Amt = objBack.SNF_Amt
                            If clsCommon.myLen(objRec.CONSM_SECTION_CODE) > 0 Then
                                objNew.Location_Code = objRec.CONSM_SECTION_CODE
                            Else
                                objNew.Location_Code = objRec.Location_Code
                            End If
                            strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
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
                            objNew.ItemType = strItemTypeToSave

                            objNew.Basic_Cost = 0
                            objNew.MRP = 0
                            objNew.Add_Cost = 0
                            objNew.MRP = 0
                            objNew.MFG_Date = objRec.PROD_DATE
                            If objBack.BACK_QTY > 0 Then
                                objNew.IS_CONSUMPTION = 3
                            Else
                                objNew.IS_CONSUMPTION = 2
                            End If

                            ''richa 9 Aug,2018
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                                Dim objCost As New MIlkComponentType
                                objCost = clsInventoryMovementNew.GetAvgCost(clsItemMaster.GetItemProductType(objBack.Item_Code, trans), objBack.Item_Code, IIf(clsCommon.myLen(objRec.CONSM_SECTION_CODE) > 0, objRec.CONSM_SECTION_CODE, objRec.Location_Code), IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY), objBack.Unit_Code, objBack.Avail_FAT_KG, objBack.Avail_SNF_KG, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), False, trans)
                                objNew.Fat_Rate = objCost.FAT_Cost / IIf(objBack.Avail_FAT_KG <= 0, 1, objBack.Avail_FAT_KG)
                                objNew.SNF_Rate = objCost.SNF_Cost / IIf(objBack.Avail_SNF_KG <= 0, 1, objBack.Avail_SNF_KG)
                                objNew.Fat_Amt = objCost.FAT_Cost
                                objNew.SNF_Amt = objCost.SNF_Cost

                                objNew.Avg_Cost = objNew.Fat_Amt + objNew.SNF_Amt
                                objNew.LIFO_Cost = objNew.Avg_Cost
                                objNew.FIFO_Cost = objNew.Avg_Cost

                                objNew.CalculateAvgCost = False
                            End If
                            ArrInventoryMovementNew.Add(objNew)
                        Else
                            obj = New clsInventoryMovement
                            obj.Trans_Type = form_id '"Production-Return"
                            obj.InOut = "O"

                            obj.Item_Code = objBack.Item_Code
                            obj.Item_Desc = objBack.Item_Desc
                            obj.Qty = IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY)
                            obj.UOM = objBack.Unit_Code
                            obj.Source_Doc_No = objRec.Wrekage_ENTRY_CODE
                            obj.Source_Doc_Date = clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy")

                            ''===================================================================================
                            obj.FAT_Per = objBack.Avail_FAT_Per
                            obj.SNF_Per = objBack.Avail_SNF_Per
                            obj.FAT_KG = objBack.Avail_FAT_KG
                            obj.SNF_KG = objBack.Avail_SNF_KG

                            '' UPDATE PRODUCTION COST
                            obj.Fat_Rate = objBack.Fat_Rate
                            obj.SNF_Rate = objBack.SNF_Rate
                            obj.Fat_Amt = objBack.Fat_Amt
                            obj.SNF_Amt = objBack.SNF_Amt
                            ''===================================================================================

                            strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
                            If clsCommon.myLen(objRec.CONSM_SECTION_CODE) > 0 Then
                                obj.Location_Code = objRec.CONSM_SECTION_CODE
                            Else
                                obj.Location_Code = objRec.Location_Code
                            End If

                            '  obj.Batch_No = objRec.Batch_Code

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
                            obj.ItemType = strItemTypeToSave

                            obj.Basic_Cost = 0
                            obj.MRP = 0
                            obj.Add_Cost = 0
                            obj.MRP = 0
                            obj.MFG_Date = objRec.PROD_DATE
                            If objBack.BACK_QTY > 0 Then
                                obj.IS_CONSUMPTION = 3
                            Else
                                obj.IS_CONSUMPTION = 2
                            End If
                            ArrInventoryMovement.Add(obj)
                        End If
                    Next
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(Category), "Warehouse Wreckage") = CompairStringResult.Equal Then
                If (objListBack IsNot Nothing AndAlso objListBack.Count > 0) Then
                    For Each objBack As clsWreckageBooking In objListBack
                        If clsCommon.myCdbl(objBack.WRECKAGE_QTY) <= 0 Then
                            Continue For
                        End If
                        Dim strItemTypeToSave As String = ""
                        Dim strItemType As String
                        Dim strProductType As String
                        '' in produced item into selected back to location
                        strProductType = clsItemMaster.GetItemProductType(objBack.Item_Code, trans)
                        If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                            objNew = New clsInventoryMovementNew
                            objNew.Trans_Type = form_id  'form_id '"Production-Return"
                            objNew.InOut = "O"
                            '  objNew.Location_Code = objRec.CONSM_SECTION_CODE
                            objNew.Item_Code = objBack.Item_Code
                            objNew.Item_Desc = objBack.Item_Desc
                            objNew.Qty = objBack.WRECKAGE_QTY 'IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY)
                            objNew.UOM = objBack.Unit_Code
                            objNew.Source_Doc_No = objRec.Wrekage_ENTRY_CODE
                            objNew.Source_Doc_Date = clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy")

                            objNew.FAT_Per = objBack.Avail_FAT_Per
                            objNew.SNF_Per = objBack.Avail_SNF_Per
                            objNew.FAT_KG = objBack.Avail_FAT_KG
                            objNew.SNF_KG = objBack.Avail_SNF_KG
                            '  objNew.Batch_No = objRec.Batch_Code

                            '' UPDATE PRODUCTION COST
                            objNew.Fat_Rate = objBack.Fat_Rate
                            objNew.SNF_Rate = objBack.SNF_Rate
                            objNew.Fat_Amt = objBack.Fat_Amt
                            objNew.SNF_Amt = objBack.SNF_Amt
                            'If clsCommon.myLen(objRec.CONSM_SECTION_CODE) > 0 Then
                            '    objNew.Location_Code = objRec.CONSM_SECTION_CODE
                            'Else
                            objNew.Location_Code = objRec.Location_Code
                            'End If
                            strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
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
                            objNew.ItemType = strItemTypeToSave

                            objNew.Basic_Cost = 0
                            objNew.MRP = 0
                            objNew.Add_Cost = 0
                            objNew.MRP = 0
                            objNew.MFG_Date = objRec.PROD_DATE
                            If objBack.BACK_QTY > 0 Then
                                objNew.IS_CONSUMPTION = 3
                            Else
                                objNew.IS_CONSUMPTION = 2
                            End If

                            ''richa 9 Aug,2018
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                                Dim objCost As New MIlkComponentType
                                objCost = clsInventoryMovementNew.GetAvgCost(clsItemMaster.GetItemProductType(objBack.Item_Code, trans), objBack.Item_Code, IIf(clsCommon.myLen(objRec.CONSM_SECTION_CODE) > 0, objRec.CONSM_SECTION_CODE, objRec.Location_Code), IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY), objBack.Unit_Code, objBack.Avail_FAT_KG, objBack.Avail_SNF_KG, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), False, trans)
                                objNew.Fat_Rate = objCost.FAT_Cost / IIf(objBack.Avail_FAT_KG <= 0, 1, objBack.Avail_FAT_KG)
                                objNew.SNF_Rate = objCost.SNF_Cost / IIf(objBack.Avail_SNF_KG <= 0, 1, objBack.Avail_SNF_KG)
                                objNew.Fat_Amt = objCost.FAT_Cost
                                objNew.SNF_Amt = objCost.SNF_Cost

                                objNew.Avg_Cost = objNew.Fat_Amt + objNew.SNF_Amt
                                objNew.LIFO_Cost = objNew.Avg_Cost
                                objNew.FIFO_Cost = objNew.Avg_Cost

                                objNew.CalculateAvgCost = False
                            End If
                            ArrInventoryMovementNew.Add(objNew)
                        Else
                            obj = New clsInventoryMovement
                            obj.Trans_Type = form_id '"Production-Return"
                            obj.InOut = "O"

                            obj.Item_Code = objBack.Item_Code
                            obj.Item_Desc = objBack.Item_Desc
                            obj.Qty = objBack.WRECKAGE_QTY 'IIf(objBack.BACK_QTY > 0, objBack.BACK_QTY, objBack.WRECKAGE_QTY)
                            obj.UOM = objBack.Unit_Code
                            obj.Source_Doc_No = objRec.Wrekage_ENTRY_CODE
                            obj.Source_Doc_Date = clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy")

                            ''===================================================================================
                            obj.FAT_Per = objBack.Avail_FAT_Per
                            obj.SNF_Per = objBack.Avail_SNF_Per
                            obj.FAT_KG = objBack.Avail_FAT_KG
                            obj.SNF_KG = objBack.Avail_SNF_KG

                            '' UPDATE PRODUCTION COST
                            obj.Fat_Rate = objBack.Fat_Rate
                            obj.SNF_Rate = objBack.SNF_Rate
                            obj.Fat_Amt = objBack.Fat_Amt
                            obj.SNF_Amt = objBack.SNF_Amt
                            ''===================================================================================

                            strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
                            'If clsCommon.myLen(objRec.CONSM_SECTION_CODE) > 0 Then
                            'obj.Location_Code = objRec.CONSM_SECTION_CODE
                            'Else
                            obj.Location_Code = objRec.Location_Code
                            'End If

                            '  obj.Batch_No = objRec.Batch_Code

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
                            obj.ItemType = strItemTypeToSave

                            obj.Basic_Cost = 0
                            obj.MRP = 0
                            obj.Add_Cost = 0
                            obj.MRP = 0
                            obj.MFG_Date = objRec.PROD_DATE
                            If objBack.BACK_QTY > 0 Then
                                obj.IS_CONSUMPTION = 3
                            Else
                                obj.IS_CONSUMPTION = 2
                            End If
                            ArrInventoryMovement.Add(obj)
                        End If
                    Next
                End If
            Else
                '' in item qty on back to location
                If (objListBack IsNot Nothing AndAlso objListBack.Count > 0) Then

                    For Each objBack As clsWreckageBooking In objListBack
                        If clsCommon.myCdbl(objBack.Scrap_Qty) <= 0 Then
                            Continue For
                        End If
                        Dim strItemTypeToSave As String = ""
                        Dim strItemType As String
                        Dim strProductType As String
                        '' in produced item into selected back to location
                        strProductType = clsItemMaster.GetItemProductType(objBack.Item_Code, trans)
                        If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                            objNew = New clsInventoryMovementNew
                            objNew.Trans_Type = "Prod-Scrap"
                            objNew.InOut = "I"
                            objNew.Location_Code = objBack.ScrapLocation
                            objNew.Item_Code = objBack.Item_Code
                            objNew.Item_Desc = objBack.Item_Desc
                            objNew.Qty = objBack.Scrap_Qty
                            objNew.UOM = objBack.Unit_Code
                            objNew.Source_Doc_No = objRec.Wrekage_ENTRY_CODE
                            objNew.Source_Doc_Date = clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy")

                            objNew.FAT_Per = objBack.Avail_FAT_Per
                            objNew.SNF_Per = objBack.Avail_SNF_Per
                            objNew.FAT_KG = objBack.Avail_FAT_KG
                            objNew.SNF_KG = objBack.Avail_SNF_KG


                            '' UPDATE PRODUCTION COST
                            objNew.Fat_Rate = objBack.Fat_Rate
                            objNew.SNF_Rate = objBack.SNF_Rate
                            objNew.Fat_Amt = objBack.Fat_Amt
                            objNew.SNF_Amt = objBack.SNF_Amt

                            strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
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
                            objNew.ItemType = strItemTypeToSave

                            objNew.Basic_Cost = 0
                            objNew.MRP = 0
                            objNew.Add_Cost = 0
                            objNew.MRP = 0
                            objNew.MFG_Date = objRec.PROD_DATE
                            objNew.IS_CONSUMPTION = 3
                            ''richa 9 Aug,2018
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                                Dim objCost As New MIlkComponentType
                                objCost = clsInventoryMovementNew.GetAvgCost(clsItemMaster.GetItemProductType(objBack.Item_Code, trans), objBack.Item_Code, objBack.ScrapLocation, objBack.Scrap_Qty, objBack.Unit_Code, objBack.Avail_FAT_KG, objBack.Avail_SNF_KG, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), False, trans)
                                objNew.Fat_Rate = objCost.FAT_Cost / IIf(objBack.Avail_FAT_KG <= 0, 1, objBack.Avail_FAT_KG)
                                objNew.SNF_Rate = objCost.SNF_Cost / IIf(objBack.Avail_SNF_KG <= 0, 1, objBack.Avail_SNF_KG)
                                objNew.Fat_Amt = objCost.FAT_Cost
                                objNew.SNF_Amt = objCost.SNF_Cost
                                objNew.Avg_Cost = objNew.Fat_Amt + objNew.SNF_Amt
                                objNew.LIFO_Cost = objNew.Avg_Cost
                                objNew.FIFO_Cost = objNew.Avg_Cost
                                objNew.CalculateAvgCost = False
                            End If
                            ''------------------
                            ArrInventoryMovementNew.Add(objNew)
                        Else
                            obj = New clsInventoryMovement
                            form_id = "Prod-Scrap"
                            obj.Trans_Type = form_id
                            obj.InOut = "I"
                            obj.Location_Code = objBack.ScrapLocation
                            obj.Item_Code = objBack.Item_Code
                            obj.Item_Desc = objBack.Item_Desc
                            obj.Qty = objBack.Scrap_Qty
                            obj.UOM = objBack.Unit_Code
                            obj.Source_Doc_No = objRec.Wrekage_ENTRY_CODE
                            obj.Source_Doc_Date = clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy")

                            ''==================================================================================
                            obj.FAT_Per = objBack.Avail_FAT_Per
                            obj.SNF_Per = objBack.Avail_SNF_Per
                            obj.FAT_KG = objBack.Avail_FAT_KG
                            obj.SNF_KG = objBack.Avail_SNF_KG

                            '' UPDATE PRODUCTION COST
                            obj.Fat_Rate = objBack.Fat_Rate
                            obj.SNF_Rate = objBack.SNF_Rate
                            obj.Fat_Amt = objBack.Fat_Amt
                            obj.SNF_Amt = objBack.SNF_Amt
                            ''==================================================================================
                            strItemType = clsItemMaster.GetItemType(objBack.Item_Code, trans)
                            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                                strItemTypeToSave = "RM"
                            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                                strItemTypeToSave = "OT"
                            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                                strItemTypeToSave = "FT"
                            Else
                                strItemTypeToSave = strItemType
                            End If
                            obj.ItemType = strItemTypeToSave

                            obj.Basic_Cost = 0
                            obj.MRP = 0
                            obj.Add_Cost = 0
                            obj.MRP = 0
                            obj.MFG_Date = objRec.PROD_DATE
                            obj.IS_CONSUMPTION = 3
                            ''richa 9 Aug,2018
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                                Dim QtyInStockUOM As Decimal = objBack.Scrap_Qty * clsItemMaster.GetConvertionFactor(objBack.Item_Code, objBack.Unit_Code, trans)
                                obj.Avg_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objBack.Item_Code, objBack.ScrapLocation, QtyInStockUOM, objRec.PROD_DATE, clsCommon.GETSERVERDATE(trans), True, trans)
                                obj.LIFO_Cost = obj.Avg_Cost
                                obj.FIFO_Cost = obj.Avg_Cost
                                obj.CalculateAvgCost = False
                            End If
                            ''------------------
                            ArrInventoryMovement.Add(obj)
                        End If
                    Next
                End If
            End If



            If ArrInventoryMovement.Count > 0 Then
                clsInventoryMovement.SaveData(form_id, ReceiptCode, objRec.PROD_DATE, clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If
            If ArrInventoryMovementNew.Count > 0 Then
                clsInventoryMovementNew.SaveData(form_id, ReceiptCode, objRec.PROD_DATE, clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select PROD_DATE,LOCATION_CODE from TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmInvoiceBulkSale, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("PROD_DATE")), trans)


            End If
            HistoryUpdate(strCode, trans)
            ' clsSerializeInvenotry.DeleteData("Production", strCode, trans)
            Dim qry As String = String.Empty
            qry = "delete from TSPL_WRECKAGE_BOOKING where WRECKAGE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_WRECKAGE_ENTRY_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where WRECKAGE_ENTRY_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    
    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        '' created by Panch Raj against ticket No- KDI/21/05/18-000321 on date 01-06-2018
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            '' table list 
            ''1. TSPL_WRECKAGE_BOOKING
            ''2. TSPL_WRECKAGE_ENTRY            
            ''3. TSPL_CUSTOM_FIELD_VALUES
            ''4. TSPL_INVENTORY_MOVEMENT_NEW ( no need of history)
            ''5. TSPL_INVENTORY_MOVEMENT     ( no need of history)
            ''6. TSPL_JOURNAL_DETAILS
            ''7. TSPL_JOURNAL_MASTER
            '' steps for checking the items stock and batch wise stock
            Dim obj As clsWreckageBooking = clsWreckageBooking.GetData(Doc_No, "", NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.Wrekage_ENTRY_CODE) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

           
            clsItemLocationDetails.CheckCancelInventoryBalance(Form_Id, Doc_No, trans)
            '' transfer data into cancel table

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_WRECKAGE_ENTRY", "WRECKAGE_ENTRY_CODE", "TSPL_WRECKAGE_BOOKING", "WRECKAGE_CODE", trans)
            
            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No='" & Doc_No & "'"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If


            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & Form_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_WRECKAGE_BOOKING where WRECKAGE_CODE='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_WRECKAGE_ENTRY where WRECKAGE_ENTRY_CODE='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
            '' release objects 
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsWreckage
    Public Shared Function SaveData(ByVal obj As clsWreckageBooking) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsWreckageBooking, ByVal trans As SqlTransaction) As Boolean
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_WRECKAGE_BOOKING where comp_code='" + objCommonVar.CurrentCompanyCode + "' and WRECKAGE_CODE='" + obj.Wrekage_ENTRY_CODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            If obj.WFBook IsNot Nothing AndAlso obj.WFBook.Count > 0 Then
                For Each objtr As clsWreckageBooking In obj.WFBook
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "WRECKAGE_CODE", obj.Wrekage_ENTRY_CODE)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)

                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)

                    clsCommon.AddColumnsForChange(coll, "BACK_QTY", objtr.BACK_QTY)
                    clsCommon.AddColumnsForChange(coll, "WRECKAGE_QTY", objtr.WRECKAGE_QTY)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_Per", objtr.Avail_FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_KG", objtr.Avail_FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_Per", objtr.Avail_SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_KG", objtr.Avail_SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "ScrapQty", objtr.Scrap_Qty)
                    clsCommon.AddColumnsForChange(coll, "ScrapLocation", objtr.ScrapLocation)

                    '' production costing columns
                    If objtr.WRECKAGE_QTY > 0 Then
                        Dim objCost As New MIlkComponentType
                        objCost = clsInventoryMovementNew.GetAvgCost(clsItemMaster.GetItemProductType(objtr.Item_Code, trans), objtr.Item_Code, obj.Location_Code, objtr.BACK_QTY, objtr.Unit_Code, objtr.Avail_FAT_KG, objtr.Avail_SNF_KG, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), False, trans)
                        objtr.Fat_Rate = objCost.FAT_Cost / IIf(objtr.Avail_FAT_KG <= 0, 1, objtr.Avail_FAT_KG)
                        objtr.SNF_Rate = objCost.SNF_Cost / IIf(objtr.Avail_SNF_KG <= 0, 1, objtr.Avail_SNF_KG)
                        objtr.Fat_Amt = objCost.FAT_Cost
                        objtr.SNF_Amt = objCost.SNF_Cost
                        clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                        clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                        clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                        clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                    End If

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WRECKAGE_BOOKING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            ' trans.Commit()

            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function UpdateData(ByVal obj As clsWreckageBooking, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal strCode As String = "") As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_WRECKAGE_BOOKING where comp_code='" + objCommonVar.CurrentCompanyCode + "' and WRECKAGE_CODE='" + obj.Wrekage_ENTRY_CODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            If obj.WFBook IsNot Nothing AndAlso obj.WFBook.Count > 0 Then
                For Each objtr As clsWreckageBooking In obj.WFBook
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "WRECKAGE_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)

                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)

                    clsCommon.AddColumnsForChange(coll, "BACK_QTY", objtr.BACK_QTY)
                    clsCommon.AddColumnsForChange(coll, "WRECKAGE_QTY", objtr.WRECKAGE_QTY)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", objtr.Location_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_Per", objtr.Avail_FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "Avail_FAT_KG", objtr.Avail_FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_Per", objtr.Avail_SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "Avail_SNF_KG", objtr.Avail_SNF_KG)

                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)

                    '' production costing columns
                    If objtr.WRECKAGE_QTY > 0 Then
                        Dim objCost As New MIlkComponentType
                        objCost = clsInventoryMovementNew.GetAvgCost(clsItemMaster.GetItemProductType(objtr.Item_Code, trans), objtr.Item_Code, objtr.Location_Code, objtr.BACK_QTY, objtr.Unit_Code, objtr.Avail_FAT_KG, objtr.Avail_SNF_KG, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), False, trans)
                        objtr.Fat_Rate = objCost.FAT_Cost / IIf(objtr.Avail_FAT_KG <= 0, 1, objtr.Avail_FAT_KG)
                        objtr.SNF_Rate = objCost.SNF_Cost / IIf(objtr.Avail_SNF_KG <= 0, 1, objtr.Avail_SNF_KG)
                        objtr.Fat_Amt = objCost.FAT_Cost
                        objtr.SNF_Amt = objCost.SNF_Cost
                        clsCommon.AddColumnsForChange(coll, "Fat_Rate", objtr.Fat_Rate)
                        clsCommon.AddColumnsForChange(coll, "SNF_Rate", objtr.SNF_Rate)
                        clsCommon.AddColumnsForChange(coll, "Fat_Amt", objtr.Fat_Amt)
                        clsCommon.AddColumnsForChange(coll, "SNF_Amt", objtr.SNF_Amt)
                    End If
                    obj.Wrekage_ENTRY_CODE = strCode
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_WRECKAGE_BOOKING", OMInsertOrUpdate.Update, "TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE='" & obj.Wrekage_ENTRY_CODE & "'", trans)
                Next
            End If
            ' trans.Commit()

            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetCategoryTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "Wreckage"
        DR("Code") = "Wreckage"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Scrap"
        DR("Code") = "Scrap"
        DT.Rows.Add(DR)


        DT.AcceptChanges()

        Return DT
    End Function
End Class


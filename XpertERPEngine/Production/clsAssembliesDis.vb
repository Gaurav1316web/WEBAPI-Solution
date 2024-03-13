Imports System.Data.SqlClient
Imports common
Imports System.Data
''=============Ticket No: BM00000010010 Parteek
Public Class clsAssembliesDis
#Region "variables"
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public CODE As String = ""
    Public MainItemStatus As String = Nothing
    Public ASSEMBLY_DATE As Date? = Nothing
    Public TRANSACTION_TYPE As String = ""
    Public DISASSEMBLY_TYPE As String = ""
    Public ASSEMBLY_CODE As String = ""
    Public ASSEMBLY_DESC As String = ""
    Public DESCRIPTION As String = ""
    Public COMMENTS As String = ""
    Public Main_Item_Code As String = ""
    Public Main_Item_Desc As String = ""
    Public BOM_CODE As String = ""
    Public BOM_DESC As String = ""
    Public COMP_ASSEMBL_METHOD As String = ""
    Public LOCATION_CODE As String = ""
    Public LOCATION_DESC As String = ""
    Public BUILD_QUANTITY As Decimal = 0
    Public QUANTITY As Decimal = 0
    Public DISASSEMBLY_COST As Decimal = 0
    Public BUILD_ITEM_UNIT_CODE As String = ""

    Public BOM_PROD_ITEM_UNIT_CODE As String = "" '' read only columns used for some calc in form
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
    Public Product_Type As String = Nothing
    Public Comp_Code As String = Nothing
    Public POSTED As Boolean = False
    Public Posting_Date As Date? = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public objList As List(Of clsAssembliesDisDetail) = Nothing
    Public Serial_No As String = ""
    Public FAT_PER As Decimal = 0
    Public SNF_PER As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public SNF_KG As Decimal = 0
#End Region
#Region "Functions"

    Public Shared Function SaveData(ByVal obj As clsAssembliesDis, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = ""

        Try
            '' sanjeet 02-jan-2018 (Locked Transaction)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmAssembDis, obj.LOCATION_CODE, obj.ASSEMBLY_DATE, trans)
            ''
            Dim coll As New Hashtable()

            If isNewEntry Then
                If clsCommon.myLen(obj.CODE) = 0 Then
                    obj.CODE = clsERPFuncationality.GetNextCode(trans, obj.ASSEMBLY_DATE, clsDocType.PROD_Assemblies, obj.TRANSACTION_TYPE, obj.LOCATION_CODE)
                Else
                    obj.CODE = clsCommon.myCstr(obj.CODE)
                End If
            End If

            qry = "DELETE FROM TSPL_PROD_ASSEMBLIES_ITEM_DETAIL WHERE ASSEMBLY_CODE='" + obj.CODE + "'"
            Dim isSaved As Boolean = True

            isSaved = isSaved AndAlso clsBatchInventory.DeleteData("Disassembly", obj.CODE, trans)
            isSaved = isSaved AndAlso clsBatchInventory.DeleteData("Assembly", obj.CODE, trans)

            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
            clsCommon.AddColumnsForChange(coll, "ASSEMBLY_DATE", clsCommon.GetPrintDate(obj.ASSEMBLY_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "TRANSACTION_TYPE", obj.TRANSACTION_TYPE)
            clsCommon.AddColumnsForChange(coll, "DISASSEMBLY_TYPE", obj.DISASSEMBLY_TYPE, True)
            clsCommon.AddColumnsForChange(coll, "ASSEMBLY_CODE", obj.ASSEMBLY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "Main_Item_Code", obj.Main_Item_Code)
            clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE, True)
            clsCommon.AddColumnsForChange(coll, "COMP_ASSEMBL_METHOD", obj.COMP_ASSEMBL_METHOD)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "BUILD_QUANTITY", obj.BUILD_QUANTITY)
            clsCommon.AddColumnsForChange(coll, "QUANTITY", obj.QUANTITY)
            clsCommon.AddColumnsForChange(coll, "DISASSEMBLY_COST", obj.DISASSEMBLY_COST)
            clsCommon.AddColumnsForChange(coll, "BUILD_ITEM_UNIT_CODE", obj.BUILD_ITEM_UNIT_CODE)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
            clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No, True)
            clsCommon.AddColumnsForChange(coll, "MainItem_Type", obj.MainItemStatus)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Posted", obj.POSTED)
            '' fat and snf
            clsCommon.AddColumnsForChange(coll, "FAT_PER", obj.FAT_PER)
            clsCommon.AddColumnsForChange(coll, "SNF_PER", obj.SNF_PER)
            clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
            clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROD_ASSEMBLIES", OMInsertOrUpdate.Insert, "", trans)
            Else
                HistoryUpdate(obj.CODE, trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROD_ASSEMBLIES", OMInsertOrUpdate.Update, "TSPL_PROD_ASSEMBLIES.CODE='" + obj.CODE + "'", trans)
            End If

            '' saving assemblies item detail
            isSaved = isSaved AndAlso clsAssembliesDisDetail.SaveData(obj.CODE, clsCommon.GetPrintDate(obj.ASSEMBLY_DATE, "dd/MMM/yyyy"), obj.TRANSACTION_TYPE, obj.objList, trans)
            '' saving custom fields
            If clsCommon.CompairString(obj.TRANSACTION_TYPE, "Disassembly") = CompairStringResult.Equal Then
                isSaved = isSaved AndAlso clsBatchInventory.SaveData("Disassembly", obj.CODE, obj.ASSEMBLY_DATE, "O", obj.Main_Item_Code, obj.LOCATION_CODE, 1, 0, obj.BUILD_ITEM_UNIT_CODE, obj.arrBatchItem, trans)
            Else
                isSaved = isSaved AndAlso clsBatchInventory.SaveData("Assembly", obj.CODE, obj.ASSEMBLY_DATE, "I", obj.Main_Item_Code, obj.LOCATION_CODE, 1, 0, obj.BUILD_ITEM_UNIT_CODE, obj.arrBatchItem, trans)
            End If


            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.CODE, obj.arrCustomFields, trans)
        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PROD_ASSEMBLIES", "CODE", "TSPL_PROD_ASSEMBLIES_ITEM_DETAIL", "ASSEMBLY_CODE", trans)
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select ASSEMBLY_DATE,LOCATION_CODE from TSPL_PROD_ASSEMBLIES where code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmAssembDis, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("ASSEMBLY_DATE")), trans)

            End If
            HistoryUpdate(strCode, trans)
            Dim qry As String
            Dim qry1 As String
            qry1 = "delete from TSPL_PROD_ASSEMBLIES where code ='" + strCode + "'"
            qry = "delete from TSPL_PROD_ASSEMBLIES_ITEM_DETAIL where ASSEMBLY_CODE ='" + strCode + "'"

            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry1, trans)
            isSaved = isSaved AndAlso clsBatchInventory.DeleteData("Disassembly", strCode, trans)
            isSaved = isSaved AndAlso clsBatchInventory.DeleteData("Assembly", strCode, trans)
            qry = "update TSPL_PROD_ASSEMBLIES_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
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
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select ASSEMBLY_DATE,LOCATION_CODE from TSPL_PROD_ASSEMBLIES where code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmAssembDis, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("ASSEMBLY_DATE")), trans)

            End If
            Dim issaved As Boolean = True

            Dim qry As String
            ''RICHA AGARWAL 27 aUG,2018 BHA/24/08/18-000481
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where  Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            ''-----------
            HistoryUpdate(strCode, trans)
            qry = "update tspl_batch_Item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & strCode & "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement where trans_type in ('Assembly','Disassembly') and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type in ('Assembly','Disassembly') and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_PROD_ASSEMBLIES set Posted='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean
        Return PostData(strDocNo, isCheckForPosted, trans, "")
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction, ByVal strVoucherNo As String) As Boolean
        Return PostData(strDocNo, isCheckForPosted, trans, strVoucherNo, False)
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction, ByVal strVoucherNo As String, ByVal isUpdateDocumentCost As Boolean) As Boolean
        'Try
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If
        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
        Dim obj As clsAssembliesDis = clsAssembliesDis.GetData(strDocNo, NavigatorType.Current, trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmAssembDis, obj.LOCATION_CODE, obj.ASSEMBLY_DATE, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.CODE) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (isCheckForPosted AndAlso obj.POSTED = 1) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If
        If isUpdateDocumentCost Then
            If clsCommon.CompairString(obj.TRANSACTION_TYPE, "Disassembly") = CompairStringResult.Equal Then
                If obj.objList.Count = 1 Then
                    obj.DISASSEMBLY_COST = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.Main_Item_Code, obj.LOCATION_CODE, obj.QUANTITY * clsItemMaster.GetConvertionFactor(obj.Main_Item_Code, obj.BUILD_ITEM_UNIT_CODE, trans), obj.ASSEMBLY_DATE, obj.ASSEMBLY_DATE, False, trans)
                    obj.objList(0).Item_Amount = obj.DISASSEMBLY_COST
                    clsAssembliesDis.SaveData(obj, False, trans)
                    obj = clsAssembliesDis.GetData(strDocNo, NavigatorType.Current, trans)
                End If
            End If
        End If

        UpdateInventoryMovement(strDocNo, trans)
        JournalEntryWIP(trans, strDocNo, strVoucherNo)
        HistoryUpdate(strDocNo, trans)
        Dim qry As String = "Update TSPL_PROD_ASSEMBLIES set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where CODE ='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        'Catch ex As Exception

        '    Throw New Exception(ex.Message)
        'End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsAssembliesDis
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAssembliesDis
        Dim obj As clsAssembliesDis = Nothing
        Dim Arr As List(Of clsAssembliesDis) = Nothing
        Dim qry As String = "select TSPL_PROD_ASSEMBLIES.CODE,TSPL_PROD_ASSEMBLIES.ASSEMBLY_DATE,TSPL_PROD_ASSEMBLIES.TRANSACTION_TYPE,TSPL_PROD_ASSEMBLIES.DISASSEMBLY_TYPE, " &
        " TSPL_PROD_ASSEMBLIES.ASSEMBLY_CODE,TSPL_PROD_ASSEMBLIES.DESCRIPTION ,TSPL_PROD_ASSEMBLIES.COMMENTS, " &
        " TSPL_PROD_ASSEMBLIES1.DESCRIPTION AS ASSEMBLY_DESC,TSPL_PROD_ASSEMBLIES.Main_Item_Code,TSPL_ITEM_MASTER.Item_Desc AS Main_Item_Desc, " &
        " TSPL_PROD_ASSEMBLIES.BOM_CODE,TSPL_PP_BOM_HEAD.DESCRIPTION AS Bom_Desc,TSPL_PROD_ASSEMBLIES.COMP_ASSEMBL_METHOD,TSPL_PROD_ASSEMBLIES.LOCATION_CODE, " &
        " TSPL_LOCATION_MASTER.Location_Desc,TSPL_PROD_ASSEMBLIES.BUILD_QUANTITY,TSPL_PROD_ASSEMBLIES.QUANTITY,TSPL_PROD_ASSEMBLIES.DISASSEMBLY_COST, " &
        " TSPL_PROD_ASSEMBLIES.BUILD_ITEM_UNIT_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE as BOM_PROD_ITEM_UNIT_CODE,TSPL_PROD_ASSEMBLIES.POSTED,TSPL_PROD_ASSEMBLIES.POSTING_DATE,TSPL_PROD_ASSEMBLIES.Serial_No,TSPL_ITEM_MASTER.Product_Type,TSPL_PROD_ASSEMBLIES.FAT_PER,TSPL_PROD_ASSEMBLIES.SNF_PER,TSPL_PROD_ASSEMBLIES.FAT_KG,TSPL_PROD_ASSEMBLIES.SNF_KG from TSPL_PROD_ASSEMBLIES left join TSPL_PP_BOM_HEAD ON TSPL_PROD_ASSEMBLIES.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE " &
        " LEFT JOIN TSPL_PROD_ASSEMBLIES AS TSPL_PROD_ASSEMBLIES1 ON TSPL_PROD_ASSEMBLIES.ASSEMBLY_CODE=TSPL_PROD_ASSEMBLIES1.CODE " &
        " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_PROD_ASSEMBLIES.Main_Item_Code=TSPL_ITEM_MASTER.Item_Code " &
        " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_PROD_ASSEMBLIES.LOCATION_CODE=TSPL_LOCATION_MASTER.Location_Code " &
        " where 2=2 "

        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PROD_ASSEMBLIES.CODE = (select MIN(CODE) from TSPL_PROD_ASSEMBLIES WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_PROD_ASSEMBLIES.CODE = (select Max(CODE) from TSPL_PROD_ASSEMBLIES WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_PROD_ASSEMBLIES.CODE = (select top 1 CODE from TSPL_PROD_ASSEMBLIES WHERE 1=1 " + whrclas + " and CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_PROD_ASSEMBLIES.CODE = (select Min(CODE) from TSPL_PROD_ASSEMBLIES where CODE>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_PROD_ASSEMBLIES.CODE = (select Max(CODE) from TSPL_PROD_ASSEMBLIES where CODE<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAssembliesDis()
            obj.CODE = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.ASSEMBLY_DATE = dt.Rows(0)("ASSEMBLY_DATE")
            obj.TRANSACTION_TYPE = clsCommon.myCstr(dt.Rows(0)("TRANSACTION_TYPE"))
            obj.DISASSEMBLY_TYPE = clsCommon.myCstr(dt.Rows(0)("DISASSEMBLY_TYPE"))
            obj.ASSEMBLY_CODE = clsCommon.myCstr(dt.Rows(0)("ASSEMBLY_CODE"))
            obj.ASSEMBLY_DESC = clsCommon.myCstr(dt.Rows(0)("ASSEMBLY_DESC"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.Main_Item_Code = clsCommon.myCstr(dt.Rows(0)("Main_Item_Code"))
            obj.Main_Item_Desc = clsCommon.myCstr(dt.Rows(0)("Main_Item_Desc"))
            obj.BOM_CODE = clsCommon.myCstr(dt.Rows(0)("BOM_CODE"))
            obj.BOM_DESC = clsCommon.myCstr(dt.Rows(0)("BOM_DESC"))
            obj.COMP_ASSEMBL_METHOD = clsCommon.myCstr(dt.Rows(0)("COMP_ASSEMBL_METHOD"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_DESC = clsCommon.myCstr(dt.Rows(0)("LOCATION_DESC"))
            obj.BUILD_QUANTITY = clsCommon.myCdbl(dt.Rows(0)("BUILD_QUANTITY"))
            obj.QUANTITY = clsCommon.myCdbl(dt.Rows(0)("QUANTITY"))
            obj.DISASSEMBLY_COST = clsCommon.myCdbl(dt.Rows(0)("DISASSEMBLY_COST"))
            obj.BUILD_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("BUILD_ITEM_UNIT_CODE"))
            obj.BOM_PROD_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("BOM_PROD_ITEM_UNIT_CODE"))
            obj.Serial_No = clsCommon.myCstr(dt.Rows(0)("Serial_No"))
            obj.Product_Type = clsCommon.myCstr(dt.Rows(0)("Product_Type"))
            obj.POSTED = dt.Rows(0)("POSTED")
            If IsDBNull(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = Nothing
            Else
                obj.Posting_Date = dt.Rows(0)("Posting_Date")
            End If
            obj.FAT_PER = clsCommon.myCdbl(dt.Rows(0)("FAT_PER"))
            obj.SNF_PER = clsCommon.myCdbl(dt.Rows(0)("SNF_PER"))

            obj.FAT_KG = clsCommon.myCdbl(dt.Rows(0)("FAT_KG"))
            obj.SNF_KG = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
            If clsCommon.CompairString(obj.TRANSACTION_TYPE, "Disassembly") = CompairStringResult.Equal Then
                obj.arrBatchItem = clsBatchInventory.GetData("Disassembly", obj.CODE, obj.Main_Item_Code, 1, trans)
            Else
                obj.arrBatchItem = clsBatchInventory.GetData("Assembly", obj.CODE, obj.Main_Item_Code, 1, trans)
            End If



            obj.objList = clsAssembliesDisDetail.GetBomAssembliesDetail(obj.CODE, obj.TRANSACTION_TYPE, trans)
        End If

        Return obj
    End Function
    Public Shared Function GetTransactionTypeTable() As DataTable
        Dim DT_TrType As DataTable = New DataTable
        DT_TrType.Columns.Add("Code", GetType(String))
        DT_TrType.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_TrType.NewRow()
        DR("Name") = "Assembly"
        DR("Code") = "Assembly"
        DT_TrType.Rows.Add(DR)

        DR = DT_TrType.NewRow()
        DR("Name") = "Disassembly"
        DR("Code") = "Disassembly"
        DT_TrType.Rows.Add(DR)

        DT_TrType.AcceptChanges()

        Return DT_TrType
    End Function
    Public Shared Function GetDisassemblyTypeTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "BOM"
        DR("Code") = "BOM"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Assembly"
        DR("Code") = "Assembly"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Other"
        DR("Code") = "Other"
        DT.Rows.Add(DR)

        DT.AcceptChanges()

        Return DT
    End Function
    Public Shared Function GetAssemblyTypeTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "BOM"
        DR("Code") = "BOM"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Other"
        DR("Code") = "Other"
        DT.Rows.Add(DR)

        DT.AcceptChanges()

        Return DT
    End Function
    Public Shared Function GetComponentAssemblyMethodTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "None"
        DR("Code") = "0"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "ALL COMPONENT MASTER ITEMS"
        DR("Code") = "1"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "COMPONENT MASTER ITEMS WITH INSUFFICIENT QUANTITIES"
        DR("Code") = "2"
        DT.Rows.Add(DR)

        DT.AcceptChanges()

        Return DT
    End Function
    Public Shared Function CheckUom(ByVal UnitCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim strq As String
        strq = "select unit_code from TSPL_UNIT_MASTER where unit_code='" & UnitCode & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetLocationStock(ByVal strLoc As String, ByVal strdate As DateTime, ByVal Item As String, ByVal UOM As String, Optional ByVal ProductType As String = "", Optional ByVal trans As SqlTransaction = Nothing) As String

        Dim qry As String = ""
        Dim dt As String = ""
        If clsCommon.myCstr(ProductType) = "MI" Then
            qry = "select * from (select convert(decimal(18,2),SUM(qty*RI)) as StockQty from ( select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg from ( select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from( select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from " &
     " ( select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(TSPL_INVENTORY_MOVEMENT.Stock_Qty  ) as qty  ,TSPL_INVENTORY_MOVEMENT.Stock_Uom as UOMNew ,0 as fat_kg,0 as snf_kg from TSPL_INVENTORY_MOVEMENT " &
    " left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code  " &
    " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" & Item & "'  " &
    " and TSPL_INVENTORY_MOVEMENT.Punching_Date<=convert(date,'" & strdate & "',103) " &
    " and tspl_location_master.Location_Code='" & strLoc & "' " &
    " union all  select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(TSPL_INVENTORY_MOVEMENT_new.Stock_Qty ) as qty  ,TSPL_INVENTORY_MOVEMENT_new.Stock_Uom as UOMNew ,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & strdate & "',103) then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & strdate & "',103) then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg from TSPL_INVENTORY_MOVEMENT_new " &
    " left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code  " &
    " where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" & Item & "' " &
    " and tspl_location_master.Location_Code='" & strLoc & "' " &
    " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & strdate & "',103))ax)axa " &
     " group by Item_Code,Location_Code,UOMNew)xx " &
    " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM " &
    " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" & UOM & "')axx group by axx.Location,axx.ICode)final where 2=2 "

            dt = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Else
            qry = "select * from (select convert(decimal(18,2),SUM(qty*RI)) as StockQty from ( select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg from ( select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from( select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from " &
    " ( select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(TSPL_INVENTORY_MOVEMENT.Stock_Qty  ) as qty  ,TSPL_INVENTORY_MOVEMENT.Stock_Uom as UOMNew ,0 as fat_kg,0 as snf_kg from TSPL_INVENTORY_MOVEMENT " &
   " left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code  " &
   " where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" & Item & "'  " &
   " and TSPL_INVENTORY_MOVEMENT.Punching_Date<=convert(date,'" & strdate & "',103) " &
   " and tspl_location_master.Location_Code='" & strLoc & "' " &
   " union all  select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(TSPL_INVENTORY_MOVEMENT_new.Stock_Qty ) as qty  ,TSPL_INVENTORY_MOVEMENT_new.Stock_Uom as UOMNew ,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & strdate & "',103) then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & strdate & "',103) then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg from TSPL_INVENTORY_MOVEMENT_new " &
   " left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code  " &
   " where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" & Item & "' " &
   " and tspl_location_master.Location_Code='" & strLoc & "' " &
   " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & strdate & "',103))ax)axa " &
    " group by Item_Code,Location_Code,UOMNew)xx " &
   " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM " &
   " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" & UOM & "')axx group by axx.Location,axx.ICode)final where 2=2 "

            dt = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        End If


        Return dt
    End Function

    Public Shared Function UpdateInventoryMovement(ByVal docNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsInventoryMovement
        Dim objMilk As New clsInventoryMovementNew
        '' get data
        Dim objData As clsAssembliesDis = clsAssembliesDis.GetData(docNo, NavigatorType.Current, trans)
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementMilk As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
        Dim strq As String = ""
        Dim strItemTypeToSave As String = ""
        Dim strItemType As String
        Dim Product_Type As String = ""

        If objData.TRANSACTION_TYPE = "Assembly" Then
            '' in produced item
            Product_Type = clsItemMaster.GetItemProductType(objData.Main_Item_Code, trans)
            If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal Then
                obj = New clsInventoryMovement
                obj.Trans_Type = "Assembly"
                obj.InOut = "I"
                obj.Location_Code = objData.LOCATION_CODE
                obj.Item_Code = objData.Main_Item_Code
                obj.Item_Desc = objData.Main_Item_Desc
                obj.Qty = objData.QUANTITY
                obj.UOM = objData.BUILD_ITEM_UNIT_CODE
                obj.Source_Doc_No = docNo
                obj.Source_Doc_Date = objData.ASSEMBLY_DATE

                strItemType = clsItemMaster.GetItemType(objData.Main_Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "AT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                obj.ItemType = strItemTypeToSave
                obj.Basic_Cost = If(objData.QUANTITY > 0, objData.DISASSEMBLY_COST / objData.QUANTITY, 0)
                obj.MRP = objData.DISASSEMBLY_COST
                obj.Add_Cost = 0
                obj.Net_Cost = objData.DISASSEMBLY_COST
                obj.MFG_Date = objData.ASSEMBLY_DATE

                obj.FAT_Per = objData.FAT_PER
                obj.SNF_Per = objData.SNF_PER
                obj.FAT_KG = objData.FAT_KG
                obj.SNF_KG = objData.SNF_KG

                ArrInventoryMovement.Add(obj)
            Else
                objMilk = New clsInventoryMovementNew
                objMilk.Trans_Type = "Assembly"
                objMilk.InOut = "I"
                objMilk.Location_Code = objData.LOCATION_CODE
                objMilk.Item_Code = objData.Main_Item_Code
                objMilk.Item_Desc = objData.Main_Item_Desc
                objMilk.Qty = objData.QUANTITY
                objMilk.UOM = objData.BUILD_ITEM_UNIT_CODE
                objMilk.Source_Doc_No = docNo
                objMilk.Source_Doc_Date = objData.ASSEMBLY_DATE

                strItemType = clsItemMaster.GetItemType(objData.Main_Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "AT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                objMilk.ItemType = strItemTypeToSave
                objMilk.Basic_Cost = objData.DISASSEMBLY_COST
                objMilk.MRP = objMilk.Basic_Cost
                objMilk.Add_Cost = 0
                objMilk.Net_Cost = objData.DISASSEMBLY_COST
                objMilk.MFG_Date = objData.ASSEMBLY_DATE

                objMilk.FAT_Per = objData.FAT_PER
                objMilk.SNF_Per = objData.SNF_PER
                objMilk.FAT_KG = objData.FAT_KG
                objMilk.SNF_KG = objData.SNF_KG

                If objData.DISASSEMBLY_COST > 0 Then
                    objMilk.Fat_Amt = Math.Round(objData.DISASSEMBLY_COST * 2 / 3, 2)
                    objMilk.SNF_Amt = objData.DISASSEMBLY_COST - objMilk.Fat_Amt
                    objMilk.Fat_Rate = If(objMilk.FAT_KG > 0, objMilk.Fat_Amt / objMilk.FAT_KG, 0)
                    objMilk.SNF_Rate = If(objMilk.SNF_KG > 0, objMilk.SNF_Amt / objMilk.SNF_KG, 0)
                End If

                '' UPDATE PRODUCTION COST
                'objInventoryMovemntMilk.Fat_Rate = objTr.FAT_Rate
                'objInventoryMovemntMilk.SNF_Rate = objTr.SNF_Rate
                'objInventoryMovemntMilk.Fat_Amt = objTr.Fat_Amt
                'objInventoryMovemntMilk.SNF_Amt = objTr.SNF_Amt

                'objInventoryMovemntMilk.Avg_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                'objInventoryMovemntMilk.FIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                'objInventoryMovemntMilk.LIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                'If clsCommon.CompairString(objInventoryMovemntMilk.InOut, "I") = CompairStringResult.Equal Then
                'objInventoryMovemntMilk.Basic_Cost = (objTr.Fat_Amt + objTr.SNF_Amt) / IIf(objTr.FINAL_PRODUCTION_QTY = 0, 1, objTr.FINAL_PRODUCTION_QTY)
                'objInventoryMovemntMilk.Net_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                'End If
                ArrInventoryMovementMilk.Add(objMilk)
            End If



            For Each objTr As clsAssembliesDisDetail In objData.objList
                obj = New clsInventoryMovement
                Product_Type = clsItemMaster.GetItemProductType(objTr.CONSM_ITEM_CODE, trans)
                If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal Then
                    obj.Trans_Type = "Assembly"
                    obj.InOut = "O"
                    obj.Location_Code = objTr.LOCATION_CODE
                    obj.Item_Code = objTr.CONSM_ITEM_CODE
                    obj.Item_Desc = objTr.ITEM_DESCRIPTION
                    obj.Qty = objTr.CONSM_QUANTITY
                    obj.UOM = objTr.CONSM_ITEM_UNIT_CODE
                    obj.Source_Doc_No = objTr.ASSEMBLY_CODE
                    obj.Source_Doc_Date = objData.ASSEMBLY_DATE

                    obj.itemstatus = ""
                    obj.itemtypeinventry = ""



                    strItemType = clsItemMaster.GetItemType(obj.Item_Code, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                        strItemTypeToSave = "AT"
                    Else
                        strItemTypeToSave = strItemType

                    End If
                    obj.ItemType = strItemTypeToSave
                    obj.Basic_Cost = If(objTr.CONSM_QUANTITY > 0, objTr.Item_Amount / objTr.CONSM_QUANTITY, 0)
                    obj.MRP = objTr.Item_Amount
                    obj.Add_Cost = 0
                    obj.Net_Cost = objTr.Item_Amount

                    obj.FAT_Per = objTr.FAT_PER
                    obj.SNF_Per = objTr.SNF_PER
                    obj.FAT_KG = objTr.FAT_KG
                    obj.SNF_KG = objTr.SNF_KG
                    ArrInventoryMovement.Add(obj)
                Else
                    objMilk.Trans_Type = "Assembly"
                    objMilk.InOut = "O"
                    objMilk.Location_Code = objTr.LOCATION_CODE
                    objMilk.Item_Code = objTr.CONSM_ITEM_CODE
                    objMilk.Item_Desc = objTr.ITEM_DESCRIPTION
                    objMilk.Qty = objTr.CONSM_QUANTITY
                    objMilk.UOM = objTr.CONSM_ITEM_UNIT_CODE
                    objMilk.Source_Doc_No = docNo
                    objMilk.Source_Doc_Date = objData.ASSEMBLY_DATE

                    objMilk.itemstatus = ""
                    objMilk.itemtypeinventry = ""

                    objMilk.FAT_Per = objTr.FAT_PER
                    objMilk.SNF_Per = objTr.SNF_PER
                    objMilk.FAT_KG = objTr.FAT_KG
                    objMilk.SNF_KG = objTr.SNF_KG

                    '' UPDATE PRODUCTION COST
                    'objInventoryMovemntMilk.Fat_Rate = objTr.FAT_Rate
                    'objInventoryMovemntMilk.SNF_Rate = objTr.SNF_Rate
                    'objInventoryMovemntMilk.Fat_Amt = objTr.Fat_Amt
                    'objInventoryMovemntMilk.SNF_Amt = objTr.SNF_Amt

                    'objInventoryMovemntMilk.Avg_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                    'objInventoryMovemntMilk.FIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                    'objInventoryMovemntMilk.LIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                    'If clsCommon.CompairString(objInventoryMovemntMilk.InOut, "I") = CompairStringResult.Equal Then
                    'objInventoryMovemntMilk.Basic_Cost = (objTr.Fat_Amt + objTr.SNF_Amt) / IIf(objTr.FINAL_PRODUCTION_QTY = 0, 1, objTr.FINAL_PRODUCTION_QTY)
                    'objInventoryMovemntMilk.Net_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                    'End If

                    strItemType = clsItemMaster.GetItemType(obj.Item_Code, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                        strItemTypeToSave = "AT"
                    Else
                        strItemTypeToSave = strItemType

                    End If
                    objMilk.ItemType = strItemTypeToSave
                    objMilk.Basic_Cost = objTr.Item_Amount
                    objMilk.MRP = objMilk.Basic_Cost
                    objMilk.Add_Cost = 0
                    objMilk.Net_Cost = objTr.Item_Amount
                    ArrInventoryMovementMilk.Add(objMilk)
                End If
            Next
            clsInventoryMovement.SaveData("Assembly", docNo, clsCommon.GetPrintDate(objData.ASSEMBLY_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objData.ASSEMBLY_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            clsInventoryMovementNew.SaveData("Assembly", docNo, clsCommon.GetPrintDate(objData.ASSEMBLY_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objData.ASSEMBLY_DATE, "dd/MM/yyyy"), ArrInventoryMovementMilk, trans)

            If clsCommon.myLen(objData.Serial_No) > 0 Then
                Dim objList As New List(Of clsSerializeInvenotry)
                Dim objsri As New clsSerializeInvenotry
                objsri.Auto_Sr_No = objData.Serial_No
                objsri.Document_Code = docNo
                objsri.Document_Date = objData.ASSEMBLY_DATE
                objsri.Document_Type = "Assembly"
                objsri.In_Out_Type = "I"
                objsri.Item_Code = objData.Main_Item_Code
                objsri.Line_No = 1
                objsri.Location_Code = objData.LOCATION_CODE
                objsri.Parent_Line_No = 1
                objsri.Tag_No = ""
                objList.Add(objsri)
                clsSerializeInvenotry.SaveData("Assembly", docNo, objData.ASSEMBLY_DATE, "I", objData.Main_Item_Code, objData.LOCATION_CODE, 1, objList, trans)
            End If
        Else
            '' in consumed item

            Product_Type = clsItemMaster.GetItemProductType(objData.Main_Item_Code, trans)
            If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal Then
                obj = New clsInventoryMovement
                obj.Trans_Type = "Disassembly"
                obj.InOut = "O"
                obj.Location_Code = objData.LOCATION_CODE
                obj.Item_Code = objData.Main_Item_Code
                obj.Item_Desc = objData.Main_Item_Desc
                obj.Qty = objData.QUANTITY
                obj.UOM = objData.BUILD_ITEM_UNIT_CODE
                obj.Source_Doc_No = docNo
                obj.Source_Doc_Date = objData.ASSEMBLY_DATE

                strItemType = clsItemMaster.GetItemType(objData.Main_Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "AT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                obj.ItemType = strItemTypeToSave
                obj.Basic_Cost = If(objData.QUANTITY > 0, objData.DISASSEMBLY_COST / objData.QUANTITY, 0)
                obj.MRP = objData.DISASSEMBLY_COST
                obj.Add_Cost = 0
                obj.Net_Cost = objData.DISASSEMBLY_COST
                obj.Avg_Cost = objData.DISASSEMBLY_COST
                obj.LIFO_Cost = obj.Avg_Cost
                obj.FIFO_Cost = obj.Avg_Cost
                obj.CalculateAvgCost = False
                obj.FAT_Per = objData.FAT_PER
                obj.SNF_Per = objData.SNF_PER
                obj.FAT_KG = objData.FAT_KG
                obj.SNF_KG = objData.SNF_KG

                ArrInventoryMovement.Add(obj)
            Else
                objMilk = New clsInventoryMovementNew
                objMilk.Trans_Type = "Disassembly"
                objMilk.InOut = "O"
                objMilk.Location_Code = objData.LOCATION_CODE
                objMilk.Item_Code = objData.Main_Item_Code
                objMilk.Item_Desc = objData.Main_Item_Desc
                objMilk.Qty = objData.QUANTITY
                objMilk.UOM = objData.BUILD_ITEM_UNIT_CODE
                objMilk.Source_Doc_No = objData.ASSEMBLY_CODE
                objMilk.Source_Doc_Date = objData.ASSEMBLY_DATE

                strItemType = clsItemMaster.GetItemType(objData.Main_Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "AT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                objMilk.ItemType = strItemTypeToSave
                objMilk.Basic_Cost = objData.DISASSEMBLY_COST
                objMilk.MRP = objMilk.Basic_Cost
                objMilk.Add_Cost = 0
                objMilk.Net_Cost = objData.DISASSEMBLY_COST

                objMilk.FAT_Per = objData.FAT_PER
                objMilk.SNF_Per = objData.SNF_PER
                objMilk.FAT_KG = objData.FAT_KG
                objMilk.SNF_KG = objData.SNF_KG
                'If objData.DISASSEMBLY_COST > 0 Then
                '    objMilk.Fat_Amt = Math.Round(objData.DISASSEMBLY_COST * 2 / 3, 2)
                '    objMilk.SNF_Amt = objData.DISASSEMBLY_COST - objMilk.Fat_Amt
                '    objMilk.Fat_Rate = If(objMilk.FAT_KG > 0, objMilk.Fat_Amt / objMilk.FAT_KG, 0)
                '    objMilk.SNF_Rate = If(objMilk.SNF_KG > 0, objMilk.SNF_Amt / objMilk.SNF_KG, 0)
                'End If
                ArrInventoryMovementMilk.Add(objMilk)
            End If

            For Each objTr As clsAssembliesDisDetail In objData.objList
                Product_Type = clsItemMaster.GetItemProductType(objTr.CONSM_ITEM_CODE, trans)
                If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal Then
                    obj = New clsInventoryMovement
                    obj.Trans_Type = "Disassembly"
                    obj.InOut = "I"
                    obj.Location_Code = objTr.LOCATION_CODE
                    obj.Item_Code = objTr.CONSM_ITEM_CODE
                    obj.Item_Desc = objTr.ITEM_DESCRIPTION
                    obj.Qty = objTr.CONSM_QUANTITY
                    obj.UOM = objTr.CONSM_ITEM_UNIT_CODE
                    obj.Source_Doc_No = docNo
                    obj.Source_Doc_Date = objData.ASSEMBLY_DATE

                    obj.itemstatus = ""
                    obj.itemtypeinventry = ""
                    If clsCommon.CompairString(objData.TRANSACTION_TYPE, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objData.DISASSEMBLY_TYPE, "Other") = CompairStringResult.Equal Then
                        obj.itemtypeinventry = objTr.ItemStatus
                        obj.itemstatus = "OLD"
                    End If

                    strItemType = clsItemMaster.GetItemType(obj.Item_Code, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                        strItemTypeToSave = "AT"
                    Else
                        strItemTypeToSave = strItemType
                        'Throw New Exception("Item Type not found: " + strItemType)
                    End If
                    obj.ItemType = strItemTypeToSave
                    obj.Basic_Cost = If(objTr.CONSM_QUANTITY > 0, objTr.Item_Amount / objTr.CONSM_QUANTITY, 0)
                    obj.MRP = objTr.Item_Amount
                    obj.Add_Cost = 0
                    obj.Net_Cost = objTr.Item_Amount
                    obj.MFG_Date = objData.ASSEMBLY_DATE

                    '' fat , snf %  BHA/15/10/18-000625 by balwinder on 15/10/2018
                    obj.FAT_Per = objTr.FAT_PER
                    obj.SNF_Per = objTr.SNF_PER
                    obj.FAT_KG = objTr.FAT_KG
                    obj.SNF_KG = objTr.SNF_KG

                    obj.Avg_Cost = objTr.Item_Amount
                    If objTr.Item_Amount <= 0 Then
                        ''richa 9 Aug,2018
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                            Dim QtyInStockUOM As Decimal = objTr.CONSM_QUANTITY * clsItemMaster.GetConvertionFactor(objTr.CONSM_ITEM_CODE, objTr.CONSM_ITEM_UNIT_CODE, trans)
                            obj.Avg_Cost = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, objTr.CONSM_ITEM_CODE, objTr.LOCATION_CODE, QtyInStockUOM, objData.ASSEMBLY_DATE, clsCommon.GETSERVERDATE(trans), True, trans)
                        End If
                        ''------------------
                    End If

                    obj.LIFO_Cost = obj.Avg_Cost
                    obj.FIFO_Cost = obj.Avg_Cost
                    obj.CalculateAvgCost = False



                    If clsCommon.CompairString(obj.itemtypeinventry, "SCRAP") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objData.TRANSACTION_TYPE, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objData.DISASSEMBLY_TYPE, "Other") = CompairStringResult.Equal Then
                        ArrInventoryMovement.Add(obj)
                    Else
                        ArrInventoryMovement.Add(obj)
                    End If
                Else
                    objMilk = New clsInventoryMovementNew
                    objMilk.Trans_Type = "Disassembly"
                    objMilk.InOut = "I"
                    objMilk.Location_Code = objTr.LOCATION_CODE
                    objMilk.Item_Code = objTr.CONSM_ITEM_CODE
                    objMilk.Item_Desc = objTr.ITEM_DESCRIPTION
                    objMilk.Qty = objTr.CONSM_QUANTITY
                    objMilk.UOM = objTr.CONSM_ITEM_UNIT_CODE
                    objMilk.Source_Doc_No = docNo
                    objMilk.Source_Doc_Date = objData.ASSEMBLY_DATE

                    objMilk.itemstatus = ""
                    objMilk.itemtypeinventry = ""
                    If clsCommon.CompairString(objData.TRANSACTION_TYPE, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objData.DISASSEMBLY_TYPE, "Other") = CompairStringResult.Equal Then
                        objMilk.itemtypeinventry = objTr.ItemStatus
                        objMilk.itemstatus = "OLD"
                    End If

                    strItemType = clsItemMaster.GetItemType(objMilk.Item_Code, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                        strItemTypeToSave = "AT"
                    Else
                        strItemTypeToSave = strItemType
                        'Throw New Exception("Item Type not found: " + strItemType)
                    End If
                    objMilk.ItemType = strItemTypeToSave
                    objMilk.Basic_Cost = If(objTr.CONSM_QUANTITY > 0, objTr.Item_Amount / objTr.CONSM_QUANTITY, 0)
                    objMilk.MRP = objMilk.Basic_Cost
                    objMilk.Add_Cost = 0
                    objMilk.Net_Cost = objTr.Item_Amount
                    objMilk.MFG_Date = objData.ASSEMBLY_DATE

                    objMilk.FAT_Per = objTr.FAT_PER 'obj.FAT_Per
                    objMilk.SNF_Per = objTr.SNF_PER 'obj.SNF_Per
                    objMilk.FAT_KG = objTr.FAT_KG ' obj.FAT_KG
                    objMilk.SNF_KG = objTr.SNF_KG 'obj.SNF_KG

                    'If objTr.Item_Amount > 0 Then
                    '    'objMilk.Fat_Amt = Math.Round(objTr.Item_Amount * 2 / 3.0, 2)
                    '    'objMilk.SNF_Amt = objTr.Item_Amount - objMilk.Fat_Amt
                    '    'objMilk.Fat_Rate = If(objMilk.FAT_KG > 0, Math.Round(objMilk.Fat_Amt / objMilk.FAT_KG, 2), 0)
                    '    'objMilk.SNF_Rate = If(objMilk.SNF_KG > 0, Math.Round(objMilk.SNF_Amt / objMilk.SNF_KG, 2), 0)
                    'End If


                    objMilk.Fat_Rate = objTr.FAT_Rate
                    objMilk.SNF_Rate = objTr.SNF_Rate
                    objMilk.Fat_Amt = objTr.FAT_KG * objTr.FAT_Rate
                    objMilk.SNF_Amt = objTr.SNF_KG * objTr.SNF_Rate

                    If (objMilk.Fat_Amt + objMilk.SNF_Amt) <= 0 Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0 Then
                            Dim objCost As New MIlkComponentType
                            objCost = clsInventoryMovementNew.GetAvgCost(clsItemMaster.GetItemProductType(objTr.CONSM_ITEM_CODE, trans), objTr.CONSM_ITEM_CODE, objTr.LOCATION_CODE, objTr.CONSM_QUANTITY, objTr.CONSM_ITEM_UNIT_CODE, objTr.FAT_KG, objTr.SNF_KG, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), False, trans)
                            objMilk.Fat_Amt = objCost.FAT_Cost
                            objMilk.SNF_Amt = objCost.SNF_Cost
                            objMilk.Fat_Rate = objCost.FAT_Cost / IIf(objTr.FAT_KG <= 0, 1, objTr.FAT_KG)
                            objMilk.SNF_Rate = objCost.SNF_Cost / IIf(objTr.SNF_KG <= 0, 1, objTr.SNF_KG)
                            objMilk.Avg_Cost = objMilk.Fat_Amt + objMilk.SNF_Amt
                            objMilk.LIFO_Cost = objMilk.Avg_Cost
                            objMilk.FIFO_Cost = objMilk.Avg_Cost
                            objMilk.CalculateAvgCost = False
                        End If
                    End If

                    ''------------------
                    If clsCommon.CompairString(obj.itemtypeinventry, "SCRAP") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objData.TRANSACTION_TYPE, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objData.DISASSEMBLY_TYPE, "Other") = CompairStringResult.Equal Then
                        ArrInventoryMovementMilk.Add(objMilk)
                    Else
                        ArrInventoryMovementMilk.Add(objMilk)
                    End If
                End If
            Next
            clsInventoryMovement.SaveData("Disassembly", docNo, clsCommon.GetPrintDate(objData.ASSEMBLY_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objData.ASSEMBLY_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            clsInventoryMovementNew.SaveData("Disassembly", docNo, clsCommon.GetPrintDate(objData.ASSEMBLY_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objData.ASSEMBLY_DATE, "dd/MM/yyyy"), ArrInventoryMovementMilk, trans)
        End If
        Return True
    End Function
    Public Shared Function JournalEntryWIP(ByVal trans As SqlTransaction, ByVal Doc_Code As String, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, trans), "1") = CompairStringResult.Equal Then
            Dim VoucherDesc As String = ""
            Dim SourceDocDesc As String = ""
            Dim SourceDocNo As String
            Dim Comments As String
            Dim Remarks As String
            Dim dblDebitCreditTotal As Double = 0.0
            Dim i As Integer = 0
            Try
                Dim Count As Integer = 0
                Dim qry As String
                Dim dtGL As DataTable
                Dim TotalDebitAmt As Decimal = 0
                Dim TotalCreditAmt As Decimal = 0
                Dim obj As clsAssembliesDis = clsAssembliesDis.GetData(Doc_Code, NavigatorType.Current, trans)
                Dim ArryLstGLAC As ArrayList = New ArrayList()
                VoucherDesc = "Financial Entry for Assemblies/Disassemblies -" & Doc_Code & " "
                SourceDocDesc = "Production Assemblies/Disassemblies"
                SourceDocNo = Doc_Code
                Comments = "Production Assemblies/Disassemblies"
                Remarks = "Production Assemblies/Disassemblies"



                qry = " Select TSPL_PROD_ASSEMBLIES.TRANSACTION_TYPE ,TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE ,TSPL_ITEM_MASTER.Product_Type,TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY ,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account from TSPL_PROD_ASSEMBLIES " &
                " left outer join  TSPL_PROD_ASSEMBLIES_ITEM_DETAIL  ON TSPL_PROD_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE " &
                " left outer join TSPL_ITEM_MASTER on TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE =TSPL_ITEM_MASTER.Item_Code " &
                " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
                " where TSPL_PROD_ASSEMBLIES.CODE ='" & Doc_Code & "'"
                dtGL = clsDBFuncationality.GetDataTable(qry, trans)
                For Each grow As DataRow In dtGL.Rows

                    Dim strIO As String = "I"
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Item("TRANSACTION_TYPE")), "Assembly") = CompairStringResult.Equal Then
                        strIO = "O"
                    End If

                    If clsCommon.myLen(grow.Item("Inv_Control_Account")) <= 0 Then
                        Throw New Exception("Inventory Control Account not found for Item " & clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")) & "")
                    End If

                    Dim AVG_COST As Double = 0
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Item("Product_Type")), "MI") = CompairStringResult.Equal Then
                        qry = "SELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT_NEW WHERE Source_Doc_No ='" & Doc_Code & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")) & "' and InOut='" + strIO + "'"
                    Else
                        qry = "SELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT WHERE Source_Doc_No ='" & Doc_Code & "' AND Item_Code ='" & clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")) & "' and InOut='" + strIO + "' "
                    End If

                    AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                    dblDebitCreditTotal += AVG_COST
                    Dim DebitAcc As String = String.Empty
                    DebitAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("Inv_Control_Account")), obj.LOCATION_CODE, trans)
                    If clsCommon.CompairString(grow.Item("TRANSACTION_TYPE"), "Disassembly") = CompairStringResult.Equal Then
                        If clsCommon.myLen(DebitAcc) > 0 Then
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                                Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(AVG_COST)}
                                ArryLstGLAC.Add(Acc2)
                            Else
                                Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(AVG_COST), "", "", "", "", "", "", "I"}
                                ArryLstGLAC.Add(Acc2)

                                ''richa agarwal 7 Dec,2018 BHA/27/11/18-000717
                                clsInventoryMovement.UpdateInvControlAccount(Doc_Code, grow.Item("TRANSACTION_TYPE"), clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")), DebitAcc, "", "", trans)
                                '------------------
                            End If
                        End If
                    Else
                        If clsCommon.myLen(DebitAcc) > 0 Then
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                                Dim Acc2() As String = {DebitAcc, -1 * clsCommon.myCdbl(AVG_COST)}
                                ArryLstGLAC.Add(Acc2)
                            Else
                                Dim Acc2() As String = {DebitAcc, -1 * clsCommon.myCdbl(AVG_COST), "", "", "", "", "", "", "I"}
                                ArryLstGLAC.Add(Acc2)

                                ''richa agarwal 7 Dec,2018 BHA/27/11/18-000717
                                clsInventoryMovement.UpdateInvControlAccount(Doc_Code, grow.Item("TRANSACTION_TYPE"), clsCommon.myCstr(grow.Item("CONSM_ITEM_CODE")), "", DebitAcc, "", trans)
                                '------------------
                            End If
                        End If
                    End If

                Next

                '' fro header item 
                qry = "  Select TSPL_PURCHASE_ACCOUNTS.Loss_Ac,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account  from TSPL_PURCHASE_ACCOUNTS left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code  ='" & clsCommon.myCstr(obj.Main_Item_Code) & "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim strIO As String = "O"
                    If clsCommon.CompairString(obj.TRANSACTION_TYPE, "Assembly") = CompairStringResult.Equal Then
                        strIO = "I"
                    End If
                    If clsCommon.myLen(dt.Rows(0)("Inv_Control_Account")) <= 0 Then
                        Throw New Exception("Inventory Control Account not found for Item " & clsCommon.myCstr(obj.Main_Item_Code) & "")
                    End If

                    Dim AVG_COST As Double = 0
                    If clsCommon.CompairString(clsCommon.myCstr(clsItemMaster.GetItemProductType(obj.Main_Item_Code, trans)), "MI") = CompairStringResult.Equal Then
                        qry = "SELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT_NEW WHERE Source_Doc_No ='" & obj.CODE & "' AND Item_Code ='" & clsCommon.myCstr(obj.Main_Item_Code) & "' and InOut='" + strIO + "'"
                    Else
                        qry = "SELECT Avg_Cost FROM TSPL_INVENTORY_MOVEMENT WHERE Source_Doc_No ='" & obj.CODE & "' AND Item_Code ='" & clsCommon.myCstr(obj.Main_Item_Code) & "' and InOut='" + strIO + "'"
                    End If
                    AVG_COST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                    Dim CreditAcc As String = String.Empty
                    CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account")), obj.LOCATION_CODE, trans)
                    Dim RI As Integer = 1
                    If clsCommon.CompairString(obj.TRANSACTION_TYPE, "Disassembly") = CompairStringResult.Equal Then
                        If clsCommon.myLen(CreditAcc) > 0 Then

                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                                Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(AVG_COST)}
                                ArryLstGLAC.Add(Acc2)
                            Else
                                Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(AVG_COST), "", "", "", "", "", "", "I"}
                                ArryLstGLAC.Add(Acc2)

                                ''richa agarwal 7 Dec,2018 BHA/27/11/18-000717
                                clsInventoryMovement.UpdateInvControlAccount(Doc_Code, obj.TRANSACTION_TYPE, clsCommon.myCstr(obj.Main_Item_Code), "", CreditAcc, "", trans)
                                '------------------
                            End If
                        End If
                        RI = 1
                    Else
                        If clsCommon.myLen(CreditAcc) > 0 Then

                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                                Dim Acc2() As String = {CreditAcc, 1 * clsCommon.myCdbl(AVG_COST)}
                                ArryLstGLAC.Add(Acc2)
                            Else
                                Dim Acc2() As String = {CreditAcc, 1 * clsCommon.myCdbl(AVG_COST), "", "", "", "", "", "", "I"}
                                ArryLstGLAC.Add(Acc2)

                                ''richa agarwal 7 Dec,2018 BHA/27/11/18-000717
                                clsInventoryMovement.UpdateInvControlAccount(Doc_Code, obj.TRANSACTION_TYPE, clsCommon.myCstr(obj.Main_Item_Code), CreditAcc, "", "", trans)
                                '------------------
                            End If
                        End If
                        RI = -1
                    End If

                    If Math.Round(dblDebitCreditTotal, 2) <> Math.Round(AVG_COST, 2) Then
                        If clsCommon.myLen(dt.Rows(0)("Loss_Ac")) <= 0 Then
                            Throw New Exception("Gain Loss Account not found for Item " & clsCommon.myCstr(obj.Main_Item_Code) & "")
                        End If

                        '' for gain loss account
                        If Math.Round(dblDebitCreditTotal, 2) < Math.Round(AVG_COST, 2) Then
                            CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Loss_Ac")), obj.LOCATION_CODE, trans)
                            If clsCommon.myLen(CreditAcc) > 0 Then
                                Dim Acc2() As String = {CreditAcc, 1 * clsCommon.myCdbl(AVG_COST - dblDebitCreditTotal) * RI}
                                ArryLstGLAC.Add(Acc2)
                            End If
                        ElseIf Math.Round(dblDebitCreditTotal, 2) > Math.Round(AVG_COST, 2) Then
                            CreditAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Loss_Ac")), obj.LOCATION_CODE, trans)
                            If clsCommon.myLen(CreditAcc) > 0 Then
                                Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(dblDebitCreditTotal - AVG_COST) * RI}
                                ArryLstGLAC.Add(Acc2)
                            End If
                        End If
                    End If
                End If


                Dim GLDesc As String = "Journal Entry Against Production Assemblies/Disassemblies- Doc No." & obj.CODE & " "

                Dim VoucherNo As String = ""
                If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                    VoucherNo = strVourcherNoForRecreateOnly
                Else
                    VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PP-AD' and Source_Doc_No='" & obj.CODE & "'", trans))
                End If
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, VoucherNo, trans, obj.ASSEMBLY_DATE, GLDesc, "PP-AD", "Production Assemblies/Disassemblies", obj.CODE, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
                Else
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, trans, obj.ASSEMBLY_DATE, GLDesc, "PP-AD", "Production Assemblies/Disassemblies", obj.CODE, Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function
    Public Shared Function CancelData(ByVal Doc_No As String) As Boolean
        Dim qry As String = ""
        '' created by Panch Raj against ticket No- KDI/21/05/18-000325 on date 31-05-2018
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            '' table list 
            '' 1. TSPL_PROD_ASSEMBLIES_ITEM_DETAIL
            '' 2. TSPL_PROD_ASSEMBLIES
            '' 3. TSPL_CUSTOM_FIELD_VALUES
            '' 4. TSPL_INVENTORY_MOVEMENT_NEW
            '' 5. TSPL_INVENTORY_MOVEMENT
            '' 6. TSPL_JOURNAL_DETAILS
            '' 7. TSPL_JOURNAL_MASTER
            '' steps for checking the items stcock and batch wise stock
            Dim obj As clsAssembliesDis = clsAssembliesDis.GetData(Doc_No, NavigatorType.Current, trans)
            If obj Is Nothing Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If
            clsItemLocationDetails.CheckCancelInventoryBalance(obj.TRANSACTION_TYPE, Doc_No, trans)
            '' transfer data into cancel table

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PROD_ASSEMBLIES", "Code", "TSPL_PROD_ASSEMBLIES_ITEM_DETAIL", "ASSEMBLY_CODE", trans)
            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No='" & Doc_No & "'"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If


            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", clsUserMgtCode.frmAssembDis, trans)
            '' delete data from original table
            qry = "delete from TSPL_PROD_ASSEMBLIES_ITEM_DETAIL where ASSEMBLY_CODE='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code='" & Doc_No & "' and Program_Code='" & clsUserMgtCode.frmAssembDis & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & obj.TRANSACTION_TYPE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & Doc_No & "' and Trans_Type='" & obj.TRANSACTION_TYPE & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PROD_ASSEMBLIES where Code='" & Doc_No & "'"
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

#End Region

End Class

Public Class clsAssembliesDisDetail
#Region "Variables"
    Public ASSEMBLY_CODE As String
    Public BOM_CODE As String
    Public LINE_NO As String
    Public CONSM_ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public CONSM_QUANTITY As String
    Public CONSM_ITEM_UNIT_CODE As String
    Public LOCATION_CODE As String
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public ItemStatus As String = Nothing
    Public ItemType As String = Nothing
    Public Product_Type As String = Nothing
    Public Serial_No As String = Nothing
    Public Item_Amount As Decimal = 0
    Public FAT_PER As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public FAT_Rate As Decimal = 0

    Public SNF_PER As Decimal = 0
    Public SNF_KG As Decimal = 0
    Public SNF_Rate As Decimal = 0

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal DocDate As DateTime, ByVal strTransactionType As String, ByVal Arr As List(Of clsAssembliesDisDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssembliesDisDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ASSEMBLY_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE, True)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", obj.CONSM_ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "CONSM_QUANTITY", obj.CONSM_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_UNIT_CODE", obj.CONSM_ITEM_UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
                clsCommon.AddColumnsForChange(coll, "Item_Type", obj.ItemType)
                clsCommon.AddColumnsForChange(coll, "Item_Status", obj.ItemStatus)
                clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No, True)
                clsCommon.AddColumnsForChange(coll, "Item_Amount", obj.Item_Amount)

                clsCommon.AddColumnsForChange(coll, "FAT_PER", obj.FAT_PER, True)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG, True)
                clsCommon.AddColumnsForChange(coll, "FAT_Rate", obj.FAT_Rate)

                clsCommon.AddColumnsForChange(coll, "SNF_PER", obj.SNF_PER, True)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG, True)
                clsCommon.AddColumnsForChange(coll, "SNF_Rate", obj.SNF_Rate)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROD_ASSEMBLIES_ITEM_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE='" + strDocNo + "'", trans)


                If clsCommon.CompairString(strTransactionType, "Disassembly") = CompairStringResult.Equal Then
                    clsBatchInventory.SaveData("Disassembly", strDocNo, DocDate, "I", obj.CONSM_ITEM_CODE, obj.LOCATION_CODE, 1, 0, obj.CONSM_ITEM_UNIT_CODE, obj.arrBatchItem, trans)
                Else
                    clsBatchInventory.SaveData("Assembly", strDocNo, DocDate, "O", obj.CONSM_ITEM_CODE, obj.LOCATION_CODE, 1, 0, obj.CONSM_ITEM_UNIT_CODE, obj.arrBatchItem, trans)
                End If

            Next

        End If

        Return True
    End Function
    Public Shared Function GetBomAssembliesDetail(ByVal strCode As String, ByVal strTransactionType As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsAssembliesDisDetail)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "select *,TSPL_ITEM_MASTER.Product_Type from TSPL_PROD_ASSEMBLIES_ITEM_DETAIL left join TSPL_ITEM_MASTER  on tspl_item_master.Item_Code =TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE  where TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE='" & strCode & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsAssembliesDisDetail
        Dim ObjList As New List(Of clsAssembliesDisDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsAssembliesDisDetail()
                objtr.ASSEMBLY_CODE = clsCommon.myCstr(dr("ASSEMBLY_CODE"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.LINE_NO = clsCommon.myCdbl(dr("LINE_NO"))
                objtr.CONSM_ITEM_CODE = clsCommon.myCstr(dr("CONSM_ITEM_CODE"))
                objtr.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(dr("CONSM_ITEM_UNIT_CODE"))
                objtr.CONSM_QUANTITY = clsCommon.myCstr(dr("CONSM_QUANTITY"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.LOCATION_CODE = clsCommon.myCstr(dr("LOCATION_CODE"))

                objtr.ItemStatus = clsCommon.myCstr(dr("Item_Status"))
                objtr.ItemType = clsCommon.myCstr(dr("item_type"))
                objtr.Serial_No = clsCommon.myCstr(dr("Serial_No"))
                objtr.Item_Amount = clsCommon.myCdbl(dr("Item_Amount"))
                objtr.Product_Type = clsCommon.myCstr(dr("Product_Type"))

                objtr.FAT_PER = clsCommon.myCdbl(dr("FAT_PER"))
                objtr.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                objtr.FAT_Rate = clsCommon.myCdbl(dr("FAT_Rate"))

                objtr.SNF_PER = clsCommon.myCdbl(dr("SNF_PER"))
                objtr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                If clsCommon.CompairString(strTransactionType, "Disassembly") = CompairStringResult.Equal Then
                    objtr.arrBatchItem = clsBatchInventory.GetData("Disassembly", objtr.ASSEMBLY_CODE, objtr.CONSM_ITEM_CODE, 1, trans)
                Else
                    objtr.arrBatchItem = clsBatchInventory.GetData("Assembly", objtr.ASSEMBLY_CODE, objtr.CONSM_ITEM_CODE, 1, trans)
                End If



                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

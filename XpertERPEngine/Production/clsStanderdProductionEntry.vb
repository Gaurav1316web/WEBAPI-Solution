Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsStanderdProductionEntry

#Region "Variables"
    Public PROD_ENTRY_CODE As String
    Public DESCRIPTION As String
    Public PROD_DATE As Date
    Public Batch_Code As String
    Public Batch_Code_Manual As String
    Public BATCH_DATE As Date
    Public RECEIVED_BY As String
    Public RECEIVED_BY_NAME As String
    Public LOCATION_CODE As String
    Public LOCATION_NAME As String
    Public COMMENTS As String
    Public CREATED_BY As String
    Public APPROVED_BY As String
    Public POSTED As Boolean
    Public Posting_Date As Date
    Public Section_Stage_Map_Code As String
    Public CONSM_LOCATION_CODE As String
    Public CONSM_SECTION_CODE As String
    Public Structure_Code As String
    Public Structure_Desc As String
    Public CONSM_LOCATION_CODE_Other As String
    Public Shift_Code As String
    Public ArrBatchItem As New List(Of clsStanderdProductionEntryDetail)
    Public ArrConsm As New List(Of clsStanderdProductionEntryConsumption)
    Public ArrConsmCost As New List(Of clsStanderdProductionEntryConsumptionCost)

#End Region



    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType) As clsStanderdProductionEntry
        Return GetData(strCode, arrloc, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As New clsStanderdProductionEntry
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select PROD_DATE,LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY where PROD_ENTRY_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("PROD_DATE")), trans)
            End If

            'Dim obj As New clsStanderdProductionEntry
            obj = clsStanderdProductionEntry.GetData(strCode, "", NavigatorType.Current, trans)

            If (obj.POSTED = True) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If

            clsSerializeInvenotry.DeleteData("Production", strCode, trans)

            Dim qry As String
            qry = "delete from TSPL_SPP_PRODUCTION_ENTRY_DETAIL where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_SPP_CONSUMPTION_WITHOUT_BATCH WHERE PROD_ENTRY_CODE='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_SPP_COST_WITHOUT_BATCH WHERE PROD_ENTRY_CODE='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_PE_ISSUE_ITEM_DETAIL where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_PE_STAGE_DETAIL where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_PE_STAGE_QC_LOG_SHEET where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_PE_WRECKAGE_FLASHING where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_PE_QC_DETAIL where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_PE_SCRAP_DETAIL where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_PRODUCTION_ENTRY where PROD_ENTRY_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsStanderdProductionEntry
        Dim obj As New clsStanderdProductionEntry()
        Dim objtr As New clsStanderdProductionEntryDetail()

        Dim LocCond As String = " where 1=1 "
        If clsCommon.myLen(arrloc) > 0 Then
            LocCond = LocCond & " and T1.LOCATION_CODE in (" + arrloc + ")"
        End If

        Dim qry As String = "SELECT T1.Shift_Code,T1.PROD_ENTRY_CODE,T1.DESCRIPTION,T1.PROD_DATE,T1.Batch_Code,T1.Batch_Code_Manual, T1.BATCH_DATE,T1.RECEIVED_BY,T2.EMP_NAME,T1.LOCATION_CODE,T3.LOCATION_DESC,T1.COMMENTS,"
        qry += " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE,T1.Section_Stage_Map_Code,T1.CONSM_LOCATION_CODE,T1.CONSM_LOCATION_CODE_Other,T1.CONSM_SECTION_CODE,T1.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq as Structure_Desc FROM TSPL_SPP_PRODUCTION_ENTRY  T1 " &
        " left JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.RECEIVED_BY=T2.EMP_CODE left JOIN TSPL_LOCATION_MASTER T3 ON T1.LOCATION_CODE=T3.LOCATION_CODE " &
        " left join TSPL_STRUCTURE_MASTER on T1.Structure_Code=TSPL_STRUCTURE_MASTER.Structure_Code " & LocCond & " "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND PROD_ENTRY_CODE = (select MIN(PROD_ENTRY_CODE) from TSPL_SPP_PRODUCTION_ENTRY where location_code in (" + arrloc + "))"
            Case NavigatorType.Last
                qry += " AND PROD_ENTRY_CODE = (select Max(PROD_ENTRY_CODE) from TSPL_SPP_PRODUCTION_ENTRY where location_code in (" + arrloc + "))"
            Case NavigatorType.Next
                qry += " AND PROD_ENTRY_CODE = (select Min(PROD_ENTRY_CODE) from TSPL_SPP_PRODUCTION_ENTRY where PROD_ENTRY_CODE>'" + strCode + "' and location_code in (" + arrloc + "))"
            Case NavigatorType.Previous
                qry += " AND PROD_ENTRY_CODE = (select Max(PROD_ENTRY_CODE) from TSPL_SPP_PRODUCTION_ENTRY where PROD_ENTRY_CODE<'" + strCode + "' and location_code in (" + arrloc + "))"
            Case NavigatorType.Current
                qry += " AND PROD_ENTRY_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.PROD_ENTRY_CODE = dt.Rows(0)("PROD_ENTRY_CODE")
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.PROD_DATE = clsCommon.GetPrintDate(dt.Rows(0)("PROD_DATE"), "dd/MMM/yyyy")
            obj.Batch_Code = clsCommon.myCstr(dt.Rows(0)("Batch_Code"))
            obj.Batch_Code_Manual = clsCommon.myCstr(dt.Rows(0)("Batch_Code_Manual"))
            obj.BATCH_DATE = clsCommon.GetPrintDate(dt.Rows(0)("BATCH_DATE"), "dd/MMM/yyyy")
            obj.RECEIVED_BY = clsCommon.myCstr(dt.Rows(0)("RECEIVED_BY"))
            obj.RECEIVED_BY_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_NAME = clsCommon.myCstr(dt.Rows(0)("LOCATION_DESC"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.Section_Stage_Map_Code = clsCommon.myCstr(dt.Rows(0)("Section_Stage_Map_Code"))
            obj.CONSM_LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_LOCATION_CODE"))
            obj.CONSM_LOCATION_CODE_Other = clsCommon.myCstr(dt.Rows(0)("CONSM_LOCATION_CODE_Other"))
            obj.CONSM_SECTION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_SECTION_CODE"))
            obj.Structure_Code = clsCommon.myCstr(dt.Rows(0)("Structure_Code"))
            obj.Structure_Desc = clsCommon.myCstr(dt.Rows(0)("Structure_Desc"))
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.APPROVED_BY = clsCommon.myCstr(dt.Rows(0)("APPROVED_BY"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            obj.Shift_Code = clsCommon.myCstr(dt.Rows(0)("Shift_Code"))
            strCode = dt.Rows(0)("PROD_ENTRY_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If

        obj.ArrBatchItem = clsStanderdProductionEntryDetail.GetProductionEntryDetail(strCode, trans)
        obj.ArrConsm = clsStanderdProductionEntryConsumption.GetConsumption(strCode, trans)
        obj.ArrConsmCost = clsStanderdProductionEntryConsumptionCost.GetConsumptionCost(strCode, trans)

        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsStanderdProductionEntry, ByVal objList As List(Of clsStanderdProductionEntryDetail), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.PROD_ENTRY_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.PROD_DATE, "dd/MMM/yyyy"), clsDocType.ppProductionEntry, "", obj.LOCATION_CODE)
                Else
                    obj.PROD_ENTRY_CODE = strCode
                End If
            End If
            If (clsCommon.myLen(obj.PROD_ENTRY_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If


            Dim qry As String = ""
            qry = "SELECT POSTED FROM TSPL_SPP_PRODUCTION_ENTRY WHERE PROD_ENTRY_CODE='" & obj.PROD_ENTRY_CODE & "'"
            obj.POSTED = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))

            If (obj.POSTED = True) Then
                Throw New Exception("Document -" & obj.PROD_ENTRY_CODE & " is already posted.")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntry, obj.LOCATION_CODE, obj.PROD_DATE, trans)

            qry = "delete from TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL where PROD_ENTRY_CODE='" & obj.PROD_ENTRY_CODE & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_CONSUMPTION_WITHOUT_BATCH where PROD_ENTRY_CODE='" & obj.PROD_ENTRY_CODE & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SPP_COST_WITHOUT_BATCH where PROD_ENTRY_CODE='" & obj.PROD_ENTRY_CODE & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "PROD_ENTRY_CODE", obj.PROD_ENTRY_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "PROD_DATE", clsCommon.GetPrintDate(obj.PROD_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Batch_Code", obj.Batch_Code, True)
            clsCommon.AddColumnsForChange(coll, "Batch_Code_Manual", obj.Batch_Code_Manual, True)
            clsCommon.AddColumnsForChange(coll, "BATCH_DATE", clsCommon.GetPrintDate(obj.BATCH_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "RECEIVED_BY", clsCommon.myCstr(obj.RECEIVED_BY), True)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(obj.LOCATION_CODE))
            clsCommon.AddColumnsForChange(coll, "COMMENTS", clsCommon.myCstr(obj.COMMENTS))
            clsCommon.AddColumnsForChange(coll, "Section_Stage_Map_Code", clsCommon.myCstr(obj.Section_Stage_Map_Code), True)
            clsCommon.AddColumnsForChange(coll, "CONSM_LOCATION_CODE", clsCommon.myCstr(obj.CONSM_LOCATION_CODE), True)
            clsCommon.AddColumnsForChange(coll, "CONSM_LOCATION_CODE_Other", clsCommon.myCstr(obj.CONSM_LOCATION_CODE_Other), True)
            clsCommon.AddColumnsForChange(coll, "CONSM_SECTION_CODE", clsCommon.myCstr(obj.CONSM_SECTION_CODE), True)
            clsCommon.AddColumnsForChange(coll, "Structure_Code", clsCommon.myCstr(obj.Structure_Code), True)

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Shift_Code", obj.Shift_Code)
            If isNewEntry Then


                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_SPP_PRODUCTION_ENTRY where PROD_ENTRY_CODE = '" & obj.PROD_ENTRY_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_PRODUCTION_ENTRY", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.PROD_ENTRY_CODE + " Is Already Exist")


                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_PRODUCTION_ENTRY", OMInsertOrUpdate.Update, "TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE='" + obj.PROD_ENTRY_CODE + "'", trans)
            End If

            isSaved = isSaved AndAlso clsStanderdProductionEntryDetail.SaveDetailData(obj.PROD_ENTRY_CODE, obj, objList, trans)
            isSaved = isSaved AndAlso clsStanderdProductionEntryConsumption.SaveConsumption(obj.PROD_ENTRY_CODE, obj.PROD_DATE, obj.ArrConsm, trans)
            isSaved = isSaved AndAlso clsStanderdProductionEntryConsumptionCost.SaveConsumptionCost(obj.PROD_ENTRY_CODE, obj.ArrConsmCost, trans)
            trans.Commit()

        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin
            PostData(Form_Id, strDocNo, arrloc, isCheckForPosted, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean

        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If
        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
        Dim obj As clsStanderdProductionEntry = clsStanderdProductionEntry.GetData(strDocNo, arrloc, NavigatorType.Current, trans)

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntry, obj.LOCATION_CODE, obj.PROD_DATE, trans)



        If (obj Is Nothing OrElse clsCommon.myLen(obj.PROD_ENTRY_CODE) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (isCheckForPosted AndAlso obj.POSTED = True) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If
        Dim isSaved As Boolean = True
        UpdateInventoryMovement(Form_Id, obj, arrloc, trans)
        Dim qry As String = "Update TSPL_SPP_PRODUCTION_ENTRY set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where PROD_ENTRY_CODE ='" + strDocNo + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        isSaved = isSaved And JournalEntry(trans, obj)
        Return isSaved

    End Function


    Public Shared Function ReCreateJE(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin
            ReCreateJE(Form_Id, strDocNo, arrloc, isCheckForPosted, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReCreateJE(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean
        Return ReCreateJE(Form_Id, strDocNo, arrloc, isCheckForPosted, trans, False)
    End Function
    Public Shared Function ReCreateJE(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction, ByVal SkipInventory As Boolean) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If
        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
        Dim obj As clsStanderdProductionEntry = clsStanderdProductionEntry.GetData(strDocNo, arrloc, NavigatorType.Current, trans)

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntry, obj.LOCATION_CODE, obj.PROD_DATE, trans)
        If Not SkipInventory Then
            UpdateInventoryMovement(Form_Id, obj, arrloc, trans)
        End If
        JournalEntry(trans, obj)
        Return True
    End Function

    Public Shared Function UpdateInventoryMovement(ByVal Form_Id As String, ByVal obj As clsStanderdProductionEntry, ByVal arrloc As String, ByVal trans As SqlTransaction) As Boolean
        clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.PROD_ENTRY_CODE & "'", trans)
        clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.PROD_ENTRY_CODE & "'", trans)

        clsStanderdProductionEntryRM.SaveRM(obj.PROD_ENTRY_CODE, arrloc, trans)
        clsStanderdProductionEntryRM.UpdateInventoryMovement(Form_Id, obj.PROD_ENTRY_CODE, arrloc, trans)
        Return True
    End Function

    Public Shared Function CheckValidCode(ByVal Doc_No As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "select count(*) from TSPL_SPP_PRODUCTION_ENTRY where PROD_ENTRY_CODE='" & Doc_No & "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "SELECT TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE AS Code,TSPL_SPP_PRODUCTION_ENTRY.DESCRIPTION,TSPL_SPP_PRODUCTION_ENTRY.Batch_Code as [Batch Code],TSPL_SPP_PRODUCTION_ENTRY.Batch_Code_Manual AS [Manual Batch No],LOCATION_CODE as [Location Code],CONSM_LOCATION_CODE,CONSM_SECTION_CODE,Section_Stage_Map_Code,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE, "
        qry += " TSPL_SPP_PRODUCTION_ENTRY.MODIFIED_BY AS APPROVED_BY,TSPL_SPP_PRODUCTION_ENTRY.Created_By,TSPL_SPP_PRODUCTION_ENTRY.POSTED,TSPL_SPP_PRODUCTION_ENTRY.POSTING_DATE,TSPL_SHIFT_MASTER.SHIFT_NAME as [Shift Name] FROM TSPL_SPP_PRODUCTION_ENTRY 
                 left outer join TSPL_SHIFT_MASTER on TSPL_SHIFT_MASTER.SHIFT_CODE=TSPL_SPP_PRODUCTION_ENTRY.SHIFT_CODE "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_SPP_PRODUCTION_ENTRY.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_SPP_PRODUCTION_ENTRY.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("STD", qry, "Code", whrCls, currCode, "Code", isButtonClicked)

        Return str
    End Function
    Public Shared Function GetProductionReportData(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim qry As String = ""
            qry = " DECLARE @STRQ VARCHAR(MAX); "
            qry += " EXEC TSPL_DATEWISE_PRODUCTION '" & clsCommon.GetPrintDate(FromDate.AddDays(-1), "dd/MMM/yyyy") & "' ,'" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "',@STRQ OUTPUT; "
            qry += " SELECT @STRQ; "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = clsDBFuncationality.GetDataTable(clsCommon.myCstr(dt.Rows(0)(0)))
            Else
                dt = New DataTable
            End If
        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function checkStock(ByVal TR_Type As String, ByVal BO_MO_Code As String, ByVal ProdQty As Decimal, ByVal PROD_DATE As Date, ByVal Location_Code As String, ByVal PROD_ENTRY_CODE As String) As Boolean
        Dim strq As String = ""
        strq = ""
        If TR_Type = "BO" Then

            strq = " SELECT TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE as PROD_ITEM_CODE,SUM(TSPL_MF_BATCH_PP_DETAIL.BATCH_QTY) AS PROD_QTY,TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE," &
                   " SUM(TSPL_MF_BATCH_ORDER_DETAIL.QTY) AS BOM_QTY,((SUM(TSPL_MF_BATCH_ORDER_DETAIL.QTY)/SUM(TSPL_MF_BATCH_PP_DETAIL.BATCH_QTY))* " & ProdQty & ") as REQUIR_QTY,TSPL_MF_BATCH_ORDER_DETAIL.UNIT_CODE " &
                   " FROM TSPL_MF_BATCH_ORDER_DETAIL inner join TSPL_MF_BATCH_ORDER on TSPL_MF_BATCH_ORDER.Batch_Code=TSPL_MF_BATCH_ORDER_DETAIL.Batch_Code " &
                   " inner join TSPL_MF_BATCH_PP_DETAIL on TSPL_MF_BATCH_ORDER.Batch_Code=TSPL_MF_BATCH_PP_DETAIL.Batch_Code " &
                   " and TSPL_MF_BATCH_ORDER_DETAIL.Shift_Code=TSPL_MF_BATCH_PP_DETAIL.Shift_Code " &
                   " and TSPL_MF_BATCH_ORDER_DETAIL.Section_Code=TSPL_MF_BATCH_PP_DETAIL.Section_Code " &
                   " WHERE TSPL_MF_BATCH_ORDER.Batch_Code='" & BO_MO_Code & "' " &
                   " GROUP BY TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE,TSPL_MF_BATCH_ORDER_DETAIL.ITEM_CODE,TSPL_MF_BATCH_ORDER_DETAIL.UNIT_CODE "
        ElseIf TR_Type = "MO" Then
            strq = " select TSPL_MF_MANUFACTURING_ORDER.ITEM_CODE as PROD_ITEM_CODE,TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK as PROD_QTY," &
                   " TSPL_MF_MO_MATERIAL.CONSM_ITEM_CODE as ITEM_CODE,TSPL_MF_MO_MATERIAL.BOM_QUANTITY AS BOM_QTY, " &
                   " ((TSPL_MF_MO_MATERIAL.CONSM_QUANTITY * " & ProdQty & ")/TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK)  AS REQUIR_QTY, " &
                   " TSPL_MF_MO_MATERIAL.CONSM_ITEM_UNIT_CODE AS UNIT_CODE from TSPL_MF_MO_MATERIAL inner join TSPL_MF_MANUFACTURING_ORDER " &
                   " on TSPL_MF_MO_MATERIAL.MO_CODE=TSPL_MF_MANUFACTURING_ORDER.MO_CODE WHERE TSPL_MF_MANUFACTURING_ORDER.MO_CODE='" & BO_MO_Code & "'"
        End If

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        For Each dr As DataRow In dt.Rows
            Dim availQty As Double = 0
            Dim reqQty As Double = 0

            availQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(dr.Item("ITEM_CODE"), Location_Code, PROD_ENTRY_CODE, PROD_DATE, Nothing, dr.Item("Unit_Code"))
            reqQty = dr.Item("REQUIR_QTY") ''clsCommon.myCdbl(dr.Cells(colItemCode)) * (clsCommon.myCdbl(Me.txtQuantity.Text) / clsCommon.myCdbl(Me.txtBuildQty.Text))
            If availQty < reqQty Then
                clsCommon.MyMessageBoxShow("Item Code: " & dr.Item("ITEM_CODE") & " ; Required Qty : " & reqQty & " ; Available Qty : " & availQty & "")
                Return False
            End If
        Next

        Return True
    End Function
    Public Shared Function GetCategorywiseProduction(ByVal FromBODate As Date, ByVal ToBODate As Date, ByVal figure As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""

            qry = "SELECT TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE AS 'Category', CONVERT(Decimal(18,2), SUM(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY)/" + figure + ") AS 'Produced Qty', " &
                 " SUM(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.REJ_QTY) AS 'Rejected Qty',SUM(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BREAKAGE_QTY) as 'Break Qty' " &
                 " FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL INNER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE  " &
                 " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
                 " where TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'  " &
                 " GROUP BY TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE "


            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetCategorywiseProductionDetail(ByVal FromBODate As Date, ByVal ToBODate As Date, ByVal Category As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""

            qry = "SELECT TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE AS 'Production No',TSPL_SPP_PRODUCTION_ENTRY.DESCRIPTION AS 'Description',TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE AS 'Production Date',TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE AS 'Category',(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY) AS 'Produced Qty', " &
                 " (TSPL_SPP_PRODUCTION_ENTRY_DETAIL.REJ_QTY) AS 'Rejected Qty',(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BREAKAGE_QTY) as 'Break Qty' " &
                 " FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL INNER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE  " &
                 " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
                 " where TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE between '" & clsCommon.GetPrintDate(FromBODate, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(ToBODate, "dd/MMM/yyyy") & "'  " &
                 " and TSPL_ITEM_MASTER.PROD_ITEM_CATEGORY_CODE='" & Category & "'"


            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetIssueAgainstBatch(ByVal Batch_Code As String, ByVal Doc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = " select TSPL_PP_ISSUE_HEAD.Issue_Code,TSPL_PP_SP_ISSUE_ITEM_DETAIL.From_Loaction_Code,TSPL_PP_SP_ISSUE_ITEM_DETAIL.To_Location_Code," &
         " TSPL_PP_ISSUE_HEAD.Main_Location_Code, TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type," &
         " isnull(TSPL_ITEM_MASTER.Product_Type,'') as Product_Type,TSPL_UNIT_MASTER.Unit_Desc,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code," &
         " TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_Qty as Issue_Qty, " &
         " TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_KG as Avail_FAT_KG,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_Per as Avail_FAT_Pers, " &
         " TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_KG as Avail_SNF_KG,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_Per as Avail_SNF_Pers, " &
         " ((TSPL_PP_SP_ISSUE_ITEM_DETAIL.FAT_Amt)/(case when TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_KG=0 then 1 else TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_FAT_KG end)) as Issued_FAT_Rate,((TSPL_PP_SP_ISSUE_ITEM_DETAIL.SNF_Amt)/(case when TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_KG=0 then 1 else TSPL_PP_SP_ISSUE_ITEM_DETAIL.Avail_SNF_KG end)) as Issued_SNF_Rate,(TSPL_PP_SP_ISSUE_ITEM_DETAIL.FAT_Amt) as Issued_FAT_Amt,(TSPL_PP_SP_ISSUE_ITEM_DETAIL.SNF_Amt) as Issued_SNF_Amt " &
         " from TSPL_PP_SP_ISSUE_ITEM_DETAIL " &
         " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE=TSPL_PP_SP_ISSUE_ITEM_DETAIL.STAGE_PROCESS_CODE " &
         " inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_SP_ISSUE_ITEM_DETAIL.Issue_Code " &
         " left join TSPL_ITEM_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
         " left join TSPL_UNIT_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code " &
         " where TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code='" & Batch_Code & "'  "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function getIssueItemFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " comp_code='" + objCommonVar.CurrentCompanyCode + "' "
        Else
            whrcls = whrcls ' + " and comp_code='" + objCommonVar.CurrentCompanyCode + "'" ' & "  and Active='1' "because in master all items should show whether it is active or inactive but in transaction only active items come
        End If

        Dim qry As String = " select TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Description, " &
         " TSPL_ITEM_MASTER.Item_Type as [Item Type]," &
         " isnull(TSPL_ITEM_MASTER.Product_Type,'') as [Product Type],TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code as [Unit Code],TSPL_UNIT_MASTER.Unit_Desc as [Unit Description] " &
         " from TSPL_PP_SP_ISSUE_ITEM_DETAIL " &
         " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE=TSPL_PP_SP_ISSUE_ITEM_DETAIL.STAGE_PROCESS_CODE " &
         " inner join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_SP_ISSUE_ITEM_DETAIL.Issue_Code " &
         " left join TSPL_ITEM_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
         " left join TSPL_UNIT_MASTER on TSPL_PP_SP_ISSUE_ITEM_DETAIL.Unit_Code=TSPL_UNIT_MASTER.Unit_Code "

        str = clsCommon.ShowSelectForm("ITMIssueFind", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getSectionStockItemFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " Location_Code='" & Loc_Code & "'"
        Else
            whrcls = whrcls
        End If

        Dim qry As String = " select * from ( select Item_Code as Code,Item_Desc as [Item Description],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," &
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " &
                           " where Location_Code='" & Loc_Code & "'  group by Item_Code,Item_Desc,Stock_UOM " &
                           " union all " &
                           " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " &
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW where Location_Code='" & Loc_Code & "' " &
                           " group by Item_Code,Item_Desc,Stock_UOM) as finder "

        str = clsCommon.ShowSelectForm("ITMIssueFind", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getSectionStockItemMultipleFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal ArrList As ArrayList, Optional ByVal TransType As String = "") As ArrayList
        Dim str As String = ""
        Dim qry As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            'whrcls = " Location_Code='" & Loc_Code & "'"
        Else
            whrcls = whrcls
        End If
        If clsCommon.myCstr(TransType) = "Scrap" Then
            qry = " select *  from ( select Item_Code as Code,Item_Desc as [Name],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," &
                         " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " &
                         "  group by Item_Code,Item_Desc,Stock_UOM " &
                         " union all " &
                         " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " &
                         " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " &
                         " group by Item_Code,Item_Desc,Stock_UOM) as finder "
        Else
            qry = " select *  from ( select Item_Code as Code,Item_Desc as [Name],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," &
                          " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT  " &
                          " where Location_Code='" & Loc_Code & "'  group by Item_Code,Item_Desc,Stock_UOM " &
                          " union all " &
                          " select Item_Code,Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " &
                          " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW where Location_Code='" & Loc_Code & "' " &
                          " group by Item_Code,Item_Desc,Stock_UOM) as finder "
        End If


        Dim arr1 As New ArrayList
        arr1 = clsCommon.ShowMultipleSelectForm("scjs", qry, "Code", "Name", ArrList, Nothing)

        Return arr1
    End Function
    Public Shared Function getItemFinder(ByVal whrcls As String, ByVal Loc_Code As String, ByVal ArrList As ArrayList, Optional ByVal TransType As String = "") As ArrayList
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
    Public Shared Function getLocationFinderWithBalance(ByVal whrcls As String, ByVal Curr_code As String, ByVal Item_Code As String, ByVal ShowHavingAvailBalanceOnly As Boolean, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrcls.Trim) = 0 Then
            whrcls = " TSPL_LOCATION_MASTER.Location_Type='Physical'  "
        End If

        If ShowHavingAvailBalanceOnly Then
            whrcls = whrcls & " and len(Code)>0 and Balance>0"
        Else
            'whrcls = whrcls & " and len(Code)>0 "
        End If

        Dim qry As String = " select TSPL_LOCATION_MASTER.Location_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as Name,TSPL_LOCATION_MASTER.Is_Section as [Is Section], " &
                            " TSPL_LOCATION_MASTER.Is_Sub_Location as [Is Sub Location],TSPL_LOCATION_MASTER.Section_Code as [Section Code],TSPL_LOCATION_MASTER.Main_Location_Code as [Main Location],finder.Balance " &
                            " from TSPL_LOCATION_MASTER left join ( select Location_Code as Code,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " &
                            " sum((case when  inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT   where Item_Code='" & Item_Code & "' " &
                            " group by Location_Code,Stock_UOM  union all  select Location_Code,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " &
                            " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & Item_Code & "' " &
                            " group by Location_Code,Stock_UOM) as finder on finder.Code=TSPL_LOCATION_MASTER.Location_Code  "

        str = clsCommon.ShowSelectForm("LocationFinder", qry, "Code", whrcls, Curr_code, "", isButtonClicked)

        Return str
    End Function
    Public Shared Function GetBatchConsumptionSection(ByVal Batch_Loc As String, ByVal Section_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim qry As String = "SELECT Location_Code FROM TSPL_LOCATION_MASTER WHERE Main_Location_Code='" & Batch_Loc & "' AND Is_Section='Y' AND Is_Consumption_Location=1 and Section_Code ='" & Section_Code & "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetSectionConsumptionSection(ByVal Location_Code As String, ByVal Section_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim qry As String = "SELECT Location_Code FROM TSPL_LOCATION_MASTER WHERE Main_Location_Code='" & Location_Code & "' AND Is_Section='Y' AND Is_Consumption_Location=1 and Section_Code='" & Section_Code & "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select PROD_DATE,LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY where PROD_ENTRY_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("PROD_DATE")), trans)

            End If
            Dim issaved As Boolean = True

            Dim qry As String = "delete from tspl_inventory_movement where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from tspl_inventory_movement_new where trans_type='" + FormId + "' and source_doc_no='" + strCode + "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL WHERE PROD_ENTRY_CODE='" & strCode & "'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in  (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No = '" & strCode & "')"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = " delete from TSPL_JOURNAL_MASTER where Source_Doc_No = '" & strCode & "' "
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_SPP_PRODUCTION_ENTRY set Posted='0',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where PROD_ENTRY_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetSectionStock(ByVal Section_Loc As String, Optional ByVal Item_Code As String = "", Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Try
            Dim Cond1 As String = "Location_Code='" & Section_Loc & "'"
            If clsCommon.myLen(Item_Code) > 0 Then
                Cond1 = Cond1 & " and TSPL_INVENTORY_MOVEMENT.Item_Code='" & Item_Code & "' "
            End If
            Dim Cond2 As String = "Location_Code='" & Section_Loc & "'"
            If clsCommon.myLen(Item_Code) > 0 Then
                Cond2 = Cond2 & " and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" & Item_Code & "' "
            End If
            Dim qry As String = " select TSPL_INVENTORY_MOVEMENT.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Description],sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed," &
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT.Stock_UOM from TSPL_INVENTORY_MOVEMENT  " &
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code  " &
                           " where " & Cond1 & "  group by TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT.Stock_UOM " &
                           " union all " &
                           " select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,sum(case when inout='I'  then Stock_Qty else 0 end) as Received,sum(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " &
                           " sum((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " &
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code=TSPL_ITEM_MASTER.Item_Code  " &
                           " where " & Cond2 & " group by TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function GetSectionStockHistory(ByVal Section_Loc As String, ByVal Item_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Try
            Dim qry As String = "select * from ( select TSPL_INVENTORY_MOVEMENT.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Description],Trans_Type as [Transaction Type],Source_Doc_No as [Doc No],Source_Doc_Date as [Doc Date],(case when inout='I'  then Stock_Qty else 0 end) as Received,(case when inout='O'  then Stock_Qty else 0 end) as Consumed," &
                           " ((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT.Stock_UOM from TSPL_INVENTORY_MOVEMENT  " &
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code  " &
                           " where Location_Code='" & Section_Loc & "' and TSPL_INVENTORY_MOVEMENT.Item_Code='" & Item_Code & "' " &
                           " union all " &
                           " select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Trans_Type as [Transaction Type],Source_Doc_No as [Doc No],Source_Doc_Date as [Doc Date],(case when inout='I'  then Stock_Qty else 0 end) as Received,(case when inout='O'  then Stock_Qty else 0 end) as Consumed, " &
                           " ((case when inout='I'  then Stock_Qty else -Stock_Qty end)) as Balance,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM from TSPL_INVENTORY_MOVEMENT_NEW " &
                           " left join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code=TSPL_ITEM_MASTER.Item_Code  " &
                           " where Location_Code='" & Section_Loc & "' and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" & Item_Code & "' ) as SectionHist order by [Item Code],convert(date,[Doc Date],103) "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function JournalEntry(ByVal trans As SqlTransaction, Optional ByVal obj As clsStanderdProductionEntry = Nothing, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Dim isSaved As Boolean = True
        Dim VoucherDesc As String = ""
        Dim SourceDocDesc As String = ""
        Dim SourceDocNo As String
        Dim Comments As String
        Dim Remarks As String

        Dim i As Integer = 0
        Try
            'Dim JRNL_DATE As Date = clsCommon.GETSERVERDATE(trans)
            Dim Count As Integer = 0
            Dim qry As String
            Dim dtGL As DataTable
            Dim TotalDebitAmt As Decimal = 0
            Dim TotalCreditAmt As Decimal = 0
            Dim dclPLAmt As Decimal = 0
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            VoucherDesc = "Financial Entry for ST. Production Entry -" & obj.PROD_ENTRY_CODE & " "
            SourceDocDesc = obj.DESCRIPTION
            SourceDocNo = obj.PROD_ENTRY_CODE
            Comments = obj.COMMENTS
            Remarks = obj.DESCRIPTION

            '' credit wip account of consumption items
            qry = " SELECT Consm.CONSM_ITEM_CODE,cast ( (Consm.Avg_Cost) as  decimal(18,2)) as Avg_Cost,TSPL_ITEM_MASTER.Item_Desc,TSPL_PURCHASE_ACCOUNTS.RM_Consumption AS CreditAccount " &
                  " FROM TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL  Consm " &
                  " left join TSPL_ITEM_MASTER on Consm.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
                  " WHERE Consm.PROD_ENTRY_CODE='" & obj.PROD_ENTRY_CODE & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("WIP Account not found for Item " & grow.Item("CONSM_ITEM_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If

                TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
                dclPLAmt += -1 * clsCommon.myCdbl(grow("Avg_Cost"))
            Next

            '' credit wip account of overhead cost
            qry = " select Cost.COST_CODE,Cost.OverHead_Cost as Avg_Cost,TSPL_OVERHEAD_COST.GL_Acc as CreditAccount from TSPL_SPP_COST_WITHOUT_BATCH Cost " &
                  " inner join TSPL_OVERHEAD_COST on Cost.COST_CODE=TSPL_OVERHEAD_COST.COST_CODE " &
                  " WHERE Cost.PROD_ENTRY_CODE='" & obj.PROD_ENTRY_CODE & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("CreditAccount")) <= 0 Then
                    Throw New Exception("GL Account not found for Cost Code " & grow.Item("COST_CODE") & "")
                End If
                Dim CreditAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("CreditAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(CreditAcc) > 0 Then
                    Dim Acc2() As String = {CreditAcc, -1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If
                dclPLAmt += -1 * clsCommon.myCdbl(grow("Avg_Cost"))
                TotalCreditAmt = TotalCreditAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            Next

            '' credit wip account of production items
            qry = " select PED.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,PED.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as DebitAccount,TSPL_PURCHASE_ACCOUNTS.Loss_Ac,TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code from TSPL_SPP_PRODUCTION_ENTRY_DETAIL PED " &
                  " left join TSPL_ITEM_MASTER on PED.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
                  " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
                  " WHERE PED.PROD_ENTRY_CODE='" & obj.PROD_ENTRY_CODE & "'"

            dtGL = clsDBFuncationality.GetDataTable(qry, trans)
            For Each grow As DataRow In dtGL.Rows
                '' check for account setting  exist or not
                If clsCommon.myLen(grow.Item("DebitAccount")) <= 0 Then
                    Throw New Exception("Inventory Control account not found for Item Code " & grow.Item("ITEM_CODE") & "")
                End If
                Dim DebitAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(grow.Item("DebitAccount")), obj.LOCATION_CODE, trans)
                If clsCommon.myLen(DebitAcc) > 0 Then
                    Dim Acc2() As String = {DebitAcc, 1 * clsCommon.myCdbl(grow("Avg_Cost"))}
                    ArryLstGLAC.Add(Acc2)
                End If
                dclPLAmt += clsCommon.myCdbl(grow("Avg_Cost"))
                TotalDebitAmt = TotalDebitAmt + clsCommon.myCdbl(grow.Item("Avg_Cost"))
            Next
            If dclPLAmt <> 0 Then
                Dim ACCCode As String = clsCommon.myCstr(dtGL.Rows(0).Item("Loss_Ac"))
                If clsCommon.myLen(ACCCode) <= 0 Then
                    Throw New Exception("Gain/Loss account not found purchase Account set-" + clsCommon.myCstr(dtGL.Rows(0).Item("Purchase_Class_Code")) + " for Item Code " & dtGL.Rows(0).Item("ITEM_CODE") & "")
                End If
                ACCCode = clsERPFuncationality.ChangeGLAccountLocationSegment(ACCCode, obj.LOCATION_CODE, trans)

                Dim Acc4() As String = {ACCCode, -1 * dclPLAmt} ''It should be last account 
                ArryLstGLAC.Add(Acc4)
            End If

            Dim GLDesc As String = "Journal Entry Against ST. Production Entry- Doc No." & obj.PROD_ENTRY_CODE & " "

            Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-ER' and Source_Doc_No='" & obj.PROD_ENTRY_CODE & "'", trans))
            If clsCommon.myLen(VoucherNo) > 0 Then
                isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, VoucherNo, trans, obj.PROD_DATE, GLDesc, "PR-ER", "Production Entry", obj.PROD_ENTRY_CODE, obj.DESCRIPTION, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
            Else
                isSaved = isSaved AndAlso clsJournalMaster.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, trans, obj.PROD_DATE, GLDesc, "PR-ER", "Production Entry", obj.PROD_ENTRY_CODE, obj.DESCRIPTION, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
            End If

            Return isSaved
        Catch ex As Exception

            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function GetPendingBatchQty(ByVal batch_Code As String, ByVal Prod_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = ""
        qry = " select sum(Quantity) as Pending_Batch_Qty from (" &
              " select Quantity from TSPL_PP_BATCH_ORDER_BOM_DETAIL where Batch_Code='" & batch_Code & "' and Item_Code='" & Item_Code & "'" &
              " union all " &
              " select sum(-TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Produced_Qty from TSPL_SPP_PRODUCTION_ENTRY left join TSPL_SPP_PRODUCTION_ENTRY_DETAIL " &
              " on TSPL_SPP_PRODUCTION_ENTRY.Prod_Entry_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Prod_Entry_Code  " &
              " where Batch_Code='" & batch_Code & "' and TSPL_SPP_PRODUCTION_ENTRY.Prod_Entry_Code not in ('" & Prod_Code & "') " &
              " and Item_Code='" & Item_Code & "') as t1"
        Dim Qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return Qty
    End Function
    Public Shared Function GetPrevProductionQty(ByVal batch_Code As String, ByVal Prod_Code As String, ByVal Item_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = ""
        qry = " select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Produced_Qty from TSPL_SPP_PRODUCTION_ENTRY left join TSPL_SPP_PRODUCTION_ENTRY_DETAIL " &
              " on TSPL_SPP_PRODUCTION_ENTRY.Prod_Entry_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Prod_Entry_Code  " &
              " where Batch_Code='" & batch_Code & "' and TSPL_SPP_PRODUCTION_ENTRY.Prod_Entry_Code not in ('" & Prod_Code & "') " &
              " and Item_Code='" & Item_Code & "'"
        Dim Qty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return Qty
    End Function
    Public Shared Function getBOMCost(ByVal Bom_Code As String, ByVal Uom As String, ByVal trans As SqlClient.SqlTransaction) As List(Of clsBomCostMappingDetails)
        Dim oblList As New List(Of clsBomCostMappingDetails)
        Dim objtr As New clsBomCostMappingDetails
        Dim qry As String = " select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_PP_BOM_HEAD.PROD_QUANTITY,Overhead.SNO," &
                            " Overhead.COST_CODE,Overhead.OverHead_Cost,ProdUOM.Conversion_Factor as Conv_ProdUom,stockUom.Conversion_Factor as Conv_StockUom," &
                            " OtherUOM.Conversion_Factor as Conv_OtherUom,(Overhead.OverHead_Cost/(ProdUOM.Conversion_Factor*TSPL_PP_BOM_HEAD.PROD_QUANTITY))*OtherUOM.Conversion_Factor as OverHead_CostOtherUom from TSPL_PP_BOM_HEAD " &
                            " left join TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS Overhead on TSPL_PP_BOM_HEAD.BOM_CODE=Overhead.Document_Code " &
                            " left join TSPL_ITEM_UOM_DETAIL ProdUOM on TSPL_PP_BOM_HEAD.PROD_ITEM_CODE=ProdUOM.Item_Code and ProdUOM.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE " &
                            " left join TSPL_ITEM_UOM_DETAIL stockUom on TSPL_PP_BOM_HEAD.PROD_ITEM_CODE=stockUom.Item_Code and stockUom.Stocking_Unit='Y' " &
                            " left join TSPL_ITEM_UOM_DETAIL OtherUOM on TSPL_PP_BOM_HEAD.PROD_ITEM_CODE=OtherUOM.Item_Code and OtherUOM.UOM_Code='" & Uom & "' " &
                            " where Document_Code='" & Bom_Code & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dt.Rows
            objtr = New clsBomCostMappingDetails
            objtr.SNO = clsCommon.myCdbl(dr.Item("sno"))
            objtr.COST_CODE = clsCommon.myCstr(dr.Item("COST_CODE"))
            objtr.COST_DESC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_OVERHEAD_COST where COST_CODE='" & objtr.COST_CODE & "'", trans))
            objtr.Overheadcost = clsCommon.myCdbl(dr.Item("OverHead_CostOtherUom"))
            oblList.Add(objtr)
        Next
        Return oblList
    End Function
End Class


Public Class clsStanderdProductionEntryDetail
#Region "Variables"
    '' grid columns details
    Public PROD_ENTRY_CODE As String
    Public Shift_Code As String
    Public Section_Code As String
    Public BOM_CODE As String

    Public ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public BATCH_QTY As Decimal
    Public UNIT_CODE As String
    Public RECEIPT_QTY As Decimal

    Public REJ_HEAD As String
    Public REJ_QTY As Decimal

    Public BREAKAGE_HEAD As String
    Public BREAKAGE_QTY As Decimal

    Public LAB_TESTING As String
    Public FINAL_PRODUCTION_QTY As Decimal = 0
    Public FINAL_PRODUCTION_QTY_BAG As Decimal = 0
    Public LOCATION_CODE As String
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

    Public FAT_Per As Decimal
    Public SNF_Per As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
    Public UOM_Bag As String
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
#End Region

    Public Shared Function SaveDetailData(ByVal strDocNo As String, ByVal objRec As clsStanderdProductionEntry, ByVal Arr As List(Of clsStanderdProductionEntryDetail), ByVal trans As SqlTransaction) As Boolean

        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim qry As String = "DELETE FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL WHERE PROD_ENTRY_CODE='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                For Each obj As clsStanderdProductionEntryDetail In Arr

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PROD_ENTRY_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Shift_Code", obj.Shift_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)

                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "BATCH_QTY", obj.BATCH_QTY)
                    clsCommon.AddColumnsForChange(coll, "RECEIPT_QTY", obj.FINAL_PRODUCTION_QTY)
                    clsCommon.AddColumnsForChange(coll, "REJ_HEAD", obj.REJ_HEAD)
                    clsCommon.AddColumnsForChange(coll, "REJ_QTY", obj.REJ_QTY)
                    clsCommon.AddColumnsForChange(coll, "BREAKAGE_HEAD", obj.BREAKAGE_HEAD)
                    clsCommon.AddColumnsForChange(coll, "BREAKAGE_QTY", obj.BREAKAGE_QTY)
                    clsCommon.AddColumnsForChange(coll, "LAB_TESTING", obj.LAB_TESTING)

                    clsCommon.AddColumnsForChange(coll, "FINAL_PRODUCTION_QTY", obj.FINAL_PRODUCTION_QTY)
                    'clsCommon.AddColumnsForChange(coll, "FINAL_PRODUCTION_QTY_BAG", obj.FINAL_PRODUCTION_QTY_BAG)
                    'clsCommon.AddColumnsForChange(coll, "FINAL_PRODUCTION_QTY_MAX", obj.FINAL_PRODUCTION_QTY_Max)
                    clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)

                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)

                    clsCommon.AddColumnsForChange(coll, "MFG_DATE", clsCommon.GetPrintDate(obj.MFG_DATE, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "EXP_DATE", clsCommon.GetPrintDate(obj.EXP_DATE, "dd/MMM/yyyy"))

                    clsCommon.AddColumnsForChange(coll, "FAT_Per", obj.FAT_Per)
                    clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_PRODUCTION_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE='" + strDocNo + "' ", trans)
                    clsSerializeInvenotry.SaveData("Production", strDocNo, objRec.PROD_DATE, "I", obj.ITEM_CODE, objRec.LOCATION_CODE, (Arr.IndexOf(obj) + 1), obj.arrSrItem, trans)
                Next

                clsStanderdProductionEntryRM.ValidateProductionItems(strDocNo, trans)

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try

        Return True
    End Function
    Public Shared Function GetProductionEntryDetail(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsStanderdProductionEntryDetail)
        Dim qry As String
        qry = "SELECT T1.*,case when Conversion_Factor=50 then UOM_Code else '' end as UOM_Bag,Conversion_Factor,coalesce(TSPL_PURCHASE_ACCOUNTS.Costing_Method,0) as Costing_Method FROM  TSPL_SPP_PRODUCTION_ENTRY_DETAIL T1 " &
        " left join TSPL_ITEM_MASTER on T1.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
        "inner  join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=T1.ITEM_CODE and   TSPL_ITEM_UOM_DETAIL.UOM_Code='bag'" &
        " left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code WHERE 2=2 " &
        " AND T1.PROD_ENTRY_CODE = '" + strCode + "' ORDER BY T1.ITEM_CODE"

        Dim objtr As New clsStanderdProductionEntryDetail
        Dim ObjList As New List(Of clsStanderdProductionEntryDetail)
        Dim dt As New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsStanderdProductionEntryDetail()
                objtr.Shift_Code = clsCommon.myCstr(dr("Shift_Code"))
                objtr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))

                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.BATCH_QTY = clsCommon.myCstr(dr("BATCH_QTY"))
                objtr.RECEIPT_QTY = clsCommon.myCdbl(dr("RECEIPT_QTY"))
                objtr.REJ_HEAD = clsCommon.myCstr(dr("REJ_HEAD"))
                objtr.REJ_QTY = clsCommon.myCdbl(dr("REJ_QTY"))
                objtr.BREAKAGE_HEAD = clsCommon.myCstr(dr("BREAKAGE_HEAD"))
                objtr.BREAKAGE_QTY = clsCommon.myCdbl(dr("BREAKAGE_QTY"))
                objtr.LAB_TESTING = clsCommon.myCstr(dr("LAB_TESTING"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))

                objtr.FINAL_PRODUCTION_QTY = clsCommon.myCdbl(dr("FINAL_PRODUCTION_QTY"))
                'If clsCommon.myCdbl(dr("FINAL_PRODUCTION_QTY")) > 0 Then
                '    objtr.FINAL_PRODUCTION_QTY_BAG = clsCommon.myCdbl(dr("FINAL_PRODUCTION_QTY")) / 50
                'End If
                'objtr.FINAL_PRODUCTION_QTY_BAG = clsCommon.myCdbl(dr("FINAL_PRODUCTION_QTY_BAG"))
                'objtr.FINAL_PRODUCTION_QTY_Max = clsCommon.myCdbl(dr("FINAL_PRODUCTION_QTY_MAX"))
                objtr.LOCATION_CODE = clsCommon.myCstr(dr("LOCATION_CODE"))

                objtr.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
                objtr.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
                objtr.AVG_Cost = clsCommon.myCdbl(dr("AVG_Cost"))
                objtr.Costing_Method = clsCommon.myCdbl(dr("Costing_Method"))

                objtr.FAT_Per = clsCommon.myCdbl(dr("FAT_Per"))
                objtr.SNF_Per = clsCommon.myCdbl(dr("SNF_Per"))
                objtr.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                objtr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))

                objtr.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
                objtr.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
                objtr.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
                objtr.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))
                objtr.UOM_Bag = clsCommon.myCstr(dr.Item("UOM_Bag"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class
Public Class clsStanderdProductionEntryRM
#Region "Variables"
    Dim PROD_ENTRY_CODE As String
    Dim CONSM_ITEM_CODE As String
    Dim CONSM_QTY As Decimal
    Dim LOCATION_CODE As String
    Dim UNIT_CODE As String
    Dim FIFO_COST As Decimal
    Dim LIFO_COST As Decimal
    Dim AVG_COST As Decimal

    Public FAT_Per As Decimal
    Public SNF_Per As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal

    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
#End Region
    Public Shared Function GetRM(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = ""
        Dim MI_Consm_Type As String = clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilk, trans)
        Dim MP_Consm_Type As String = clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilkProduct, trans)
        Dim Othr_Consm_Type As String = clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeOther, trans)

        '' ticket No: BM00000007861 by Panch Raj(MP Consumption)
        qry = " select PP.PROD_ENTRY_CODE,Avg(PP.AVG_COST) as AVG_COST,PP.CONSM_ITEM_CODE,sum(PP.CONSM_QTY) as CONSM_QTY,sum(PP.Fat_Amt) as Fat_Amt, " &
              " sum(PP.FAT_KG) as FAT_KG,(case when sum(PP.CONSM_QTY)<=0 then 0 else (sum(PP.FAT_KG)/sum(PP.CONSM_QTY))*100 end) as FAT_Per, " &
              " (case when sum(PP.FAT_KG)<=0 then 0 else (sum(PP.Fat_Amt)/sum(PP.FAT_KG)) end) as Fat_Rate,SUM(PP.FIFO_COST) AS FIFO_COST, " &
              " SUM(PP.LIFO_COST)  AS LIFO_COST,PP.LOCATION_CODE AS CONSM_LOCATION_CODE," &
              " SUM(PP.SNF_Amt) AS SNF_Amt,SUM(PP.SNF_KG) AS SNF_KG,(case when sum(PP.CONSM_QTY)<=0 then 0 else (sum(PP.SNF_KG)/sum(PP.CONSM_QTY))*100 end) as SNF_Per, " &
              " (case when sum(PP.SNF_KG)<=0 then 0 else (sum(PP.SNF_Amt)/sum(PP.SNF_KG)) end) as SNF_Rate,PP.UNIT_CODE AS Consm_Unit_Code from TSPL_SPP_CONSUMPTION_WITHOUT_BATCH PP " &
              " where PP.PROD_ENTRY_CODE='" & ReceiptCode & "' GROUP BY  PP.PROD_ENTRY_CODE,PP.CONSM_ITEM_CODE,PP.UNIT_CODE,PP.LOCATION_CODE"

        Return clsDBFuncationality.GetDataTable(qry, trans)
    End Function
    Public Shared Function SaveRM(ByVal ReceiptCode As String, ByVal arrLoc As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim objRec As clsStanderdProductionEntry
        objRec = clsStanderdProductionEntry.GetData(ReceiptCode, arrLoc, NavigatorType.Current, trans)
        If objRec Is Nothing Then
            Return False
        End If
        If clsCommon.myLen(objRec.PROD_ENTRY_CODE) <= 0 Then
            Return False
        End If
        ValidateProductionItems(ReceiptCode, trans)
        Dim dtIssue As DataTable

        dtIssue = GetRM(ReceiptCode, trans) 'clsDBFuncationality.GetDataTable(qry, trans)
        Dim totalFifoCost As Decimal = 0
        Dim totalLifoCost As Decimal = 0
        Dim totalAvgCost As Decimal = 0

        clsDBFuncationality.ExecuteNonQuery("delete TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL where PROD_ENTRY_CODE = '" + ReceiptCode + "'  ", trans)
        For Each dr As DataRow In dtIssue.Rows
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PROD_ENTRY_CODE", ReceiptCode)
            clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", clsCommon.myCstr(dr.Item("Consm_Item_Code")))

            clsCommon.AddColumnsForChange(coll, "CONSM_QTY", clsCommon.myCDecimal(dr.Item("Consm_Qty")))
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), True)
            clsCommon.AddColumnsForChange(coll, "UNIT_CODE", clsCommon.myCstr(dr.Item("Consm_Unit_Code")))

            clsCommon.AddColumnsForChange(coll, "FAT_Per", clsCommon.myCdbl(dr.Item("FAT_Per")))
            clsCommon.AddColumnsForChange(coll, "SNF_Per", clsCommon.myCdbl(dr.Item("SNF_Per")))
            clsCommon.AddColumnsForChange(coll, "FAT_KG", clsCommon.myCdbl(dr.Item("FAT_KG")))
            clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(dr.Item("SNF_KG")))
            Dim cost As Decimal = clsCommon.myCDecimal(dr.Item("AVG_COST"))
            'Dim Amt As Decimal = (clsCommon.myCDecimal(dr.Item("AVG_COST")) * clsCommon.myCDecimal(dr.Item("Consm_Qty")))
            clsCommon.AddColumnsForChange(coll, "AVG_COST", cost)
            Dim BalanceQty As Decimal = 0


            Dim Product_Type As String = clsItemMaster.GetItemProductType(dr.Item("Consm_Item_Code"), trans)

            '' check item balance 
            If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsLocation.GetMainLocationMilk(clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), trans), clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), ReceiptCode, objRec.PROD_DATE, trans, clsCommon.myCstr(dr.Item("Consm_Unit_Code")))
            Else
                BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), ReceiptCode, objRec.PROD_DATE, trans, clsCommon.myCstr(dr.Item("Consm_Unit_Code")), 0)
            End If
            ' commented by priti on 18/09/2018 discussed with ranjana
            If clsCommon.myCdbl(dr.Item("Consm_Qty")) > BalanceQty Then
                Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Consm_Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")) & " Available Qty: " & BalanceQty & "  Consumed Qty: " & clsCommon.myCdbl(dr.Item("Consm_Qty")) & " ")
            End If


            '' production costing cols
            clsCommon.AddColumnsForChange(coll, "FAT_Amt", clsCommon.myCdbl(dr.Item("FAT_Amt")))
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", clsCommon.myCdbl(dr.Item("SNF_Amt")))
            clsCommon.AddColumnsForChange(coll, "FAT_Rate", If(clsCommon.myCdbl(dr.Item("FAT_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("FAT_Amt")) / clsCommon.myCdbl(dr.Item("FAT_KG"))))
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", If(clsCommon.myCdbl(dr.Item("SNF_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("SNF_Amt")) / clsCommon.myCdbl(dr.Item("SNF_KG"))))


            'cost = clsCommon.myCdbl(dr.Item("FAT_Amt")) + clsCommon.myCdbl(dr.Item("SNF_Amt"))
            clsCommon.AddColumnsForChange(coll, "FIFO_COST", cost)
            totalFifoCost = totalFifoCost + cost

            'clsCommon.AddColumnsForChange(coll, "AVG_COST", cost)
            totalAvgCost = totalAvgCost + cost

            clsCommon.AddColumnsForChange(coll, "LIFO_COST", cost)
            totalLifoCost = totalLifoCost + cost

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
        Next
        Dim objRate As New MIlkComponentType
        Dim ProdCost As Decimal = 0

        For Each objProd As clsStanderdProductionEntryDetail In objRec.ArrBatchItem
            objRate = New MIlkComponentType
            ProdCost = totalAvgCost ''objProd.AVG_Cost  'GetConsumptionCost(ReceiptCode, objProd.ITEM_CODE, trans)
            objRate.FAT_Kg = objProd.FAT_KG
            objRate.SNF_Kg = objProd.SNF_KG

            objRate.FAT_Cost = (ProdCost * 2 / 3.0) / IIf(objRate.FAT_Kg <= 0, 1, objRate.FAT_Kg)
            objRate.SNF_Cost = (ProdCost * 1 / 3.0) / IIf(objRate.SNF_Kg <= 0, 1, objRate.SNF_Kg)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "FIFO_Cost", ProdCost)
            clsCommon.AddColumnsForChange(coll, "AVG_Cost", ProdCost)
            clsCommon.AddColumnsForChange(coll, "LIFO_Cost", ProdCost)

            '' update production avg cost
            clsCommon.AddColumnsForChange(coll, "Fat_Rate", objRate.FAT_Cost)
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", objRate.SNF_Cost)
            clsCommon.AddColumnsForChange(coll, "Fat_Amt", objRate.FAT_Cost * objProd.FAT_KG)
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", objRate.SNF_Cost * objProd.SNF_KG)


            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_PRODUCTION_ENTRY_DETAIL", OMInsertOrUpdate.Update, "TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE='" + objRec.PROD_ENTRY_CODE + "' and TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE='" & objProd.BOM_CODE & "' AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE='" & objProd.ITEM_CODE & "'   AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE='" & objProd.UNIT_CODE & "'", trans)
            'totalProduced = totalProduced + objProd.RECEIPT_QTY
        Next

        Return True
    End Function


    'Public Shared Function SaveRMForJE(ByVal ReceiptCode As String, ByVal arrLoc As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
    '    Dim objRec As clsStanderdProductionEntry
    '    objRec = clsStanderdProductionEntry.GetData(ReceiptCode, arrLoc, NavigatorType.Current, trans)
    '    If objRec Is Nothing Then
    '        Return False
    '    End If
    '    If clsCommon.myLen(objRec.PROD_ENTRY_CODE) <= 0 Then
    '        Return False
    '    End If
    '    ValidateProductionItems(ReceiptCode, trans)
    '    Dim dtIssue As DataTable

    '    dtIssue = GetRM(ReceiptCode, trans) 'clsDBFuncationality.GetDataTable(qry, trans)
    '    Dim totalFifoCost As Decimal = 0
    '    Dim totalLifoCost As Decimal = 0
    '    Dim totalAvgCost As Decimal = 0
    '    clsDBFuncationality.ExecuteNonQuery(" delete TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL where PROD_ENTRY_CODE = '" + ReceiptCode + "'  ", trans)
    '    For Each dr As DataRow In dtIssue.Rows
    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "PROD_ENTRY_CODE", ReceiptCode)
    '        clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", clsCommon.myCstr(dr.Item("Consm_Item_Code")))

    '        clsCommon.AddColumnsForChange(coll, "CONSM_QTY", clsCommon.myCdbl(dr.Item("Consm_Qty")))
    '        clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), True)
    '        clsCommon.AddColumnsForChange(coll, "UNIT_CODE", clsCommon.myCstr(dr.Item("Consm_Unit_Code")))

    '        clsCommon.AddColumnsForChange(coll, "FAT_Per", clsCommon.myCdbl(dr.Item("FAT_Per")))
    '        clsCommon.AddColumnsForChange(coll, "SNF_Per", clsCommon.myCdbl(dr.Item("SNF_Per")))
    '        clsCommon.AddColumnsForChange(coll, "FAT_KG", clsCommon.myCdbl(dr.Item("FAT_KG")))
    '        clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(dr.Item("SNF_KG")))
    '        clsCommon.AddColumnsForChange(coll, "AVG_COST", clsCommon.myCdbl(dr.Item("AVG_COST")))
    '        Dim BalanceQty As Decimal = 0

    '        Dim cost As Decimal = 0
    '        Dim Product_Type As String = clsItemMaster.GetItemProductType(dr.Item("Consm_Item_Code"), trans)

    '        '' check item balance 
    '        'If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
    '        '    BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsLocation.GetMainLocationMilk(clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), trans), clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), ReceiptCode, objRec.PROD_DATE, trans, clsCommon.myCstr(dr.Item("Consm_Unit_Code")))
    '        'Else
    '        '    BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(dr.Item("Consm_Item_Code")), clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")), ReceiptCode, objRec.PROD_DATE, trans, clsCommon.myCstr(dr.Item("Consm_Unit_Code")), 0)
    '        'End If
    '        ' commented by priti on 18/09/2018 discussed with ranjana
    '        'If clsCommon.myCdbl(dr.Item("Consm_Qty")) > BalanceQty Then
    '        '    Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Consm_Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("CONSM_LOCATION_CODE")) & " Available Qty: " & BalanceQty & "  Consumed Qty: " & clsCommon.myCdbl(dr.Item("Consm_Qty")) & " ")
    '        'End If


    '        '' production costing cols
    '        clsCommon.AddColumnsForChange(coll, "FAT_Amt", clsCommon.myCdbl(dr.Item("FAT_Amt")))
    '        clsCommon.AddColumnsForChange(coll, "SNF_Amt", clsCommon.myCdbl(dr.Item("SNF_Amt")))
    '        clsCommon.AddColumnsForChange(coll, "FAT_Rate", If(clsCommon.myCdbl(dr.Item("FAT_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("FAT_Amt")) / clsCommon.myCdbl(dr.Item("FAT_KG"))))
    '        clsCommon.AddColumnsForChange(coll, "SNF_Rate", If(clsCommon.myCdbl(dr.Item("SNF_KG")) <= 0, 0, clsCommon.myCdbl(dr.Item("SNF_Amt")) / clsCommon.myCdbl(dr.Item("SNF_KG"))))


    '        cost = clsCommon.myCdbl(dr.Item("FAT_Amt")) + clsCommon.myCdbl(dr.Item("SNF_Amt"))
    '        clsCommon.AddColumnsForChange(coll, "FIFO_COST", cost)
    '        totalFifoCost = totalFifoCost + cost

    '        'clsCommon.AddColumnsForChange(coll, "AVG_COST", cost)
    '        totalAvgCost = totalAvgCost + cost

    '        clsCommon.AddColumnsForChange(coll, "LIFO_COST", cost)
    '        totalLifoCost = totalLifoCost + cost

    '        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
    '    Next
    '    Dim objRate As New MIlkComponentType
    '    Dim ProdCost As Decimal = 0

    '    For Each objProd As clsStanderdProductionEntryDetail In objRec.ArrBatchItem
    '        objRate = New MIlkComponentType
    '        ProdCost = objProd.AVG_Cost  'GetConsumptionCost(ReceiptCode, objProd.ITEM_CODE, trans)
    '        objRate.FAT_Kg = objProd.FAT_KG
    '        objRate.SNF_Kg = objProd.SNF_KG

    '        objRate.FAT_Cost = (ProdCost * 2 / 3.0) / IIf(objRate.FAT_Kg <= 0, 1, objRate.FAT_Kg)
    '        objRate.SNF_Cost = (ProdCost * 1 / 3.0) / IIf(objRate.SNF_Kg <= 0, 1, objRate.SNF_Kg)

    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "FIFO_Cost", ProdCost)
    '        clsCommon.AddColumnsForChange(coll, "AVG_Cost", ProdCost)
    '        clsCommon.AddColumnsForChange(coll, "LIFO_Cost", ProdCost)

    '        '' update production avg cost
    '        clsCommon.AddColumnsForChange(coll, "Fat_Rate", objRate.FAT_Cost)
    '        clsCommon.AddColumnsForChange(coll, "SNF_Rate", objRate.SNF_Cost)
    '        clsCommon.AddColumnsForChange(coll, "Fat_Amt", objRate.FAT_Cost * objProd.FAT_KG)
    '        clsCommon.AddColumnsForChange(coll, "SNF_Amt", objRate.SNF_Cost * objProd.SNF_KG)


    '        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_PRODUCTION_ENTRY_DETAIL", OMInsertOrUpdate.Update, "TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE='" + objRec.PROD_ENTRY_CODE + "' and TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE='" & objProd.BOM_CODE & "' AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE='" & objProd.ITEM_CODE & "'   AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE='" & objProd.UNIT_CODE & "'", trans)
    '        'totalProduced = totalProduced + objProd.RECEIPT_QTY
    '    Next

    '    Return True
    'End Function



    Public Shared Function GetConsumptionCost(ByVal Doc_Code As String, ByVal Prod_Item_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = " select sum(Cost) as Cost from (select Main_ITEM_CODE,(case when (Consm.Fat_Amt+Consm.SNF_Amt)<=0 then Consm.Avg_Cost else (Consm.Fat_Amt+Consm.SNF_Amt) end) as Cost " &
                            " from TSPL_SPP_CONSUMPTION_WITHOUT_BATCH Consm  " &
                            " left join TSPL_ITEM_MASTER Item on Consm.CONSM_ITEM_CODE=Item.Item_Code where Consm.PROD_ENTRY_CODE='" & Doc_Code & "' and Consm.Main_ITEM_CODE='" & Prod_Item_Code & "' " &
                            " union all select Main_ITEM_CODE,SUM(Cost.OverHead_Cost) AS OverHead_Cost from TSPL_SPP_COST_WITHOUT_BATCH Cost  where PROD_ENTRY_CODE='" & Doc_Code & "' and Main_ITEM_CODE='" & Prod_Item_Code & "' " &
                            " group by Main_ITEM_CODE " &
                            " ) as Final "
        Dim cost As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return cost
    End Function
    Public Shared Function ValidateProductionItems(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As Decimal
        Dim qry As String = " select ITEM_CODE,count(*) as Total from TSPL_SPP_PRODUCTION_ENTRY_DETAIL " &
                            " where PROD_ENTRY_CODE='" & Doc_Code & "' group by ITEM_CODE having count(*)>1 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Dim strItems As String = ""
            For Each dr As DataRow In dt.Rows
                If Len(strItems) > 0 Then
                    strItems = strItems & "," & clsCommon.myCstr(dr.Item("ITEM_CODE"))
                Else
                    strItems = clsCommon.myCstr(dr.Item("ITEM_CODE"))
                End If
            Next
            Throw New Exception("Item (" & strItems & ") Duplicated in Production Tab.")
        End If
        Return True
    End Function
    Public Shared Function GetConsumedRM(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsStanderdProductionEntryRM)
        Dim qry As String = "select PROD_ENTRY_CODE,CONSM_ITEM_CODE,CONSM_QTY,LOCATION_CODE,UNIT_CODE,FIFO_COST,LIFO_COST,AVG_COST,FAT_Per,FAT_KG,SNF_Per,SNF_KG,Fat_Rate,SNF_Rate,FAT_Amt,SNF_Amt from TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL where PROD_ENTRY_CODE='" & ReceiptCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim obj As clsStanderdProductionEntryRM
        Dim objList As New List(Of clsStanderdProductionEntryRM)
        For Each dr As DataRow In dt.Rows
            obj = New clsStanderdProductionEntryRM
            obj.PROD_ENTRY_CODE = dr.Item("PROD_ENTRY_CODE")
            obj.CONSM_ITEM_CODE = dr.Item("CONSM_ITEM_CODE")
            obj.CONSM_QTY = dr.Item("CONSM_QTY")
            obj.LOCATION_CODE = clsCommon.myCstr(dr.Item("LOCATION_CODE"))
            obj.UNIT_CODE = dr.Item("UNIT_CODE")
            obj.FIFO_COST = dr.Item("FIFO_COST")
            obj.LIFO_COST = dr.Item("LIFO_COST")
            obj.AVG_COST = dr.Item("AVG_COST")

            obj.FAT_Per = dr.Item("FAT_Per")
            obj.FAT_KG = dr.Item("FAT_KG")
            obj.SNF_Per = dr.Item("SNF_Per")
            obj.SNF_KG = dr.Item("SNF_KG")

            obj.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
            obj.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
            obj.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
            obj.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))


            objList.Add(obj)
        Next
        Return objList
    End Function
    Public Shared Function UpdateInventoryMovement(ByVal form_id As String, ByVal ReceiptCode As String, ByVal arrloc As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As clsInventoryMovement = Nothing
            Dim objNew As clsInventoryMovementNew = Nothing
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            Dim strq As String = ""
            Dim objRec As clsStanderdProductionEntry = clsStanderdProductionEntry.GetData(ReceiptCode, arrloc, NavigatorType.Current, trans)
            Dim objListProd As List(Of clsStanderdProductionEntryDetail) = objRec.ArrBatchItem

            If (objListProd IsNot Nothing AndAlso objListProd.Count > 0) Then
                For Each objProd As clsStanderdProductionEntryDetail In objListProd
                    Dim strItemTypeToSave As String = ""
                    Dim strItemType As String
                    Dim strProductType As String
                    '' in produced item

                    strProductType = clsItemMaster.GetItemProductType(objProd.ITEM_CODE, trans)

                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        objNew = New clsInventoryMovementNew
                        objNew.Trans_Type = "Production"
                        objNew.InOut = "I"
                        objNew.Location_Code = objRec.LOCATION_CODE 'objProd.LOCATION_CODE
                        objNew.Item_Code = objProd.ITEM_CODE
                        objNew.Item_Desc = objProd.ITEM_DESCRIPTION
                        objNew.Qty = objProd.FINAL_PRODUCTION_QTY
                        objNew.UOM = objProd.UNIT_CODE
                        objNew.Source_Doc_No = objRec.PROD_ENTRY_CODE
                        objNew.Source_Doc_Date = objRec.PROD_DATE

                        objNew.FAT_Per = objProd.FAT_Per
                        objNew.SNF_Per = objProd.SNF_Per
                        objNew.FAT_KG = objProd.FAT_KG
                        objNew.SNF_KG = objProd.SNF_KG
                        objNew.Batch_No = objRec.Batch_Code_Manual

                        '' UPDATE PRODUCTION COST
                        objNew.Fat_Rate = objProd.Fat_Rate
                        objNew.SNF_Rate = objProd.SNF_Rate
                        objNew.Fat_Amt = objProd.Fat_Amt
                        objNew.SNF_Amt = objProd.SNF_Amt

                        objNew.Avg_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        objNew.FIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        objNew.LIFO_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        If clsCommon.CompairString(objNew.InOut, "I") = CompairStringResult.Equal Then
                            objNew.Basic_Cost = (objProd.Fat_Amt + objProd.SNF_Amt) / IIf(objProd.FINAL_PRODUCTION_QTY = 0, 1, objProd.FINAL_PRODUCTION_QTY)
                            objNew.Net_Cost = objProd.Fat_Amt + objProd.SNF_Amt
                        End If

                        strItemType = clsItemMaster.GetItemType(objProd.ITEM_CODE, trans)
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
                        objNew.MFG_Date = objRec.PROD_DATE
                        ArrInventoryMovementNew.Add(objNew)
                    Else
                        obj = New clsInventoryMovement
                        obj.Trans_Type = "Production"
                        obj.InOut = "I"
                        obj.Location_Code = objRec.LOCATION_CODE 'objProd.LOCATION_CODE
                        obj.Item_Code = objProd.ITEM_CODE
                        obj.Item_Desc = objProd.ITEM_DESCRIPTION
                        obj.Qty = objProd.FINAL_PRODUCTION_QTY
                        obj.UOM = objProd.UNIT_CODE
                        obj.Source_Doc_No = objRec.PROD_ENTRY_CODE
                        obj.Source_Doc_Date = objRec.PROD_DATE

                        strItemType = clsItemMaster.GetItemType(objProd.ITEM_CODE, trans)
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
                        obj.Batch_No = objRec.Batch_Code_Manual

                        ''===========================================================
                        obj.FAT_Per = objProd.FAT_Per
                        obj.SNF_Per = objProd.SNF_Per
                        obj.FAT_KG = objProd.FAT_KG
                        obj.SNF_KG = objProd.SNF_KG

                        obj.Fat_Rate = objProd.Fat_Rate
                        obj.SNF_Amt = objProd.SNF_Amt
                        obj.Fat_Amt = objProd.Fat_Amt
                        obj.SNF_Rate = objProd.SNF_Rate
                        ''==================================================================

                        obj.Avg_Cost = objProd.AVG_Cost
                        obj.FIFO_Cost = objProd.AVG_Cost
                        obj.LIFO_Cost = objProd.AVG_Cost
                        If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                            obj.Basic_Cost = (objProd.AVG_Cost) / IIf(objProd.RECEIPT_QTY <= 0, 1, objProd.RECEIPT_QTY)
                            obj.Net_Cost = objProd.AVG_Cost
                        End If
                        obj.MFG_Date = objRec.PROD_DATE

                        ArrInventoryMovement.Add(obj)
                    End If
                Next
            End If


            '' get data

            Dim objData As List(Of clsStanderdProductionEntryRM) = clsStanderdProductionEntryRM.GetConsumedRM(ReceiptCode, trans)
            For Each dr As clsStanderdProductionEntryRM In objData
                Dim strItemTypeToSave As String = ""
                Dim strItemType As String
                Dim strProductType As String
                If clsCommon.myLen(dr.LOCATION_CODE) <= 0 Then
                    Continue For
                End If
                '' out consumed item
                strProductType = clsItemMaster.GetItemProductType(dr.CONSM_ITEM_CODE, trans)
                If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                    objNew = New clsInventoryMovementNew
                    objNew.Trans_Type = "Consumption"
                    objNew.InOut = "O"
                    objNew.Location_Code = dr.LOCATION_CODE
                    objNew.Item_Code = dr.CONSM_ITEM_CODE
                    objNew.Item_Desc = clsItemMaster.GetItemName(dr.CONSM_ITEM_CODE, trans)
                    objNew.Qty = dr.CONSM_QTY
                    objNew.UOM = dr.UNIT_CODE
                    objNew.Source_Doc_No = dr.PROD_ENTRY_CODE
                    objNew.Source_Doc_Date = objRec.PROD_DATE

                    objNew.FAT_Per = dr.FAT_Per
                    objNew.SNF_Per = dr.SNF_Per
                    objNew.FAT_KG = dr.FAT_KG
                    objNew.SNF_KG = dr.SNF_KG
                    objNew.Batch_No = objRec.Batch_Code_Manual

                    '' UPDATE PRODUCTION COST
                    objNew.Fat_Rate = dr.Fat_Rate
                    objNew.SNF_Rate = dr.SNF_Rate
                    objNew.Fat_Amt = dr.Fat_Amt
                    objNew.SNF_Amt = dr.SNF_Amt

                    objNew.Avg_Cost = dr.Fat_Amt + dr.SNF_Amt
                    objNew.FIFO_Cost = dr.Fat_Amt + dr.SNF_Amt
                    objNew.LIFO_Cost = dr.Fat_Amt + dr.SNF_Amt

                    strItemType = clsItemMaster.GetItemType(dr.CONSM_ITEM_CODE, trans)
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
                    objNew.Add_Cost = 0
                    objNew.MRP = 0
                    objNew.IS_CONSUMPTION = 1
                    ArrInventoryMovementNew.Add(objNew)

                Else
                    obj = New clsInventoryMovement
                    obj.Trans_Type = "Consumption"
                    obj.InOut = "O"
                    obj.Location_Code = dr.LOCATION_CODE
                    obj.Item_Code = dr.CONSM_ITEM_CODE
                    obj.Item_Desc = clsItemMaster.GetItemName(dr.CONSM_ITEM_CODE, trans)
                    obj.Qty = dr.CONSM_QTY
                    obj.UOM = dr.UNIT_CODE
                    obj.Source_Doc_No = dr.PROD_ENTRY_CODE
                    obj.Source_Doc_Date = objRec.PROD_DATE
                    obj.Batch_No = objRec.Batch_Code_Manual


                    ''======================================================================
                    obj.FAT_Per = dr.FAT_Per
                    obj.SNF_Per = dr.SNF_Per
                    obj.FAT_KG = dr.FAT_KG
                    obj.SNF_KG = dr.SNF_KG

                    '' UPDATE PRODUCTION COST
                    obj.Fat_Rate = dr.Fat_Rate
                    obj.SNF_Rate = dr.SNF_Rate
                    obj.Fat_Amt = dr.Fat_Amt
                    obj.SNF_Amt = dr.SNF_Amt
                    ''===============================================================

                    obj.Avg_Cost = dr.AVG_COST
                    obj.FIFO_Cost = dr.AVG_COST
                    obj.LIFO_Cost = dr.AVG_COST

                    strItemType = clsItemMaster.GetItemType(dr.CONSM_ITEM_CODE, trans)
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
                    obj.Add_Cost = 0
                    obj.MRP = 0
                    obj.IS_CONSUMPTION = 1
                    ArrInventoryMovement.Add(obj)

                End If

            Next


            If ArrInventoryMovement.Count > 0 Then
                clsInventoryMovement.SaveData(form_id, ReceiptCode, clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If
            If ArrInventoryMovementNew.Count > 0 Then
                clsInventoryMovementNew.SaveData(form_id, ReceiptCode, clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objRec.PROD_DATE, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try
        Return True
    End Function

End Class

Public Class clsStanderdProductionEntryConsumption
#Region "Variables"
    Public PROD_ENTRY_CODE As String
    Public Main_ITEM_CODE As String
    Public Main_ITEM_Desc As String
    Public BOM_CODE As String
    Public BOM_Desc As String
    Public MAIN_UOM As String
    Public MAIN_UOM_Desc As String
    Public CONSM_ITEM_CODE As String
    Public CONSM_ITEM_Desc As String
    Public Consm_Product_Type As String
    Public CONSM_QTY_Original As Decimal
    Public CONSM_QTY As Decimal
    Public LOCATION_CODE As String
    Public LOCATION_Desc As String
    Public UNIT_CODE As String
    Public FIFO_COST As Decimal
    Public LIFO_COST As Decimal
    Public AVG_COST As Decimal

    Public FAT_Per As Decimal
    Public SNF_Per As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public UOM_Bag As String
    '' production costing columns
    Public Fat_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Fat_Amt As Decimal = 0
    Public SNF_Amt As Decimal = 0
    Public FINAL_PRODUCTION_QTY_Min As Decimal = 0
    Public FINAL_PRODUCTION_QTY_Max As Decimal = 0
#End Region
    Public Shared Function GetConsumption(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsStanderdProductionEntryConsumption)
        Dim qry As String = ""
        Dim objList As New List(Of clsStanderdProductionEntryConsumption)
        Dim objtr As New clsStanderdProductionEntryConsumption
        qry = "select PP.FINAL_PRODUCTION_QTY_MIN, PP.FINAL_PRODUCTION_QTY_MAX, PP.PROD_ENTRY_CODE,PP.AVG_COST,PP.BOM_CODE,PP.CONSM_ITEM_CODE,Consm_Item.Item_Desc as Consm_Item_Desc,Consm_Item.Product_Type as Consm_Product_Type,PP.CONSM_QTY,PP.CONSM_QTY_Original
,PP.Fat_Amt,PP.FAT_KG,PP.FAT_Per,PP.Fat_Rate,PP.FIFO_COST,PP.LIFO_COST,PP.LOCATION_CODE,TSPL_LOCATION_MASTER.Location_Desc," &
            " PP.Main_ITEM_CODE,PP.MAIN_UOM,PP.SNF_Amt,PP.SNF_KG,PP.SNF_Per,PP.SNF_Rate,PP.UNIT_CODE,TSPL_ITEM_MASTER.ITEM_DESC AS Main_ITEM_Desc,TSPL_MF_BOM_HEAD.DESCRIPTION AS BOM_Desc,TSPL_UNIT_MASTER.UNIT_DESC AS  MAIN_UOM_Desc from TSPL_SPP_CONSUMPTION_WITHOUT_BATCH PP " &
            " LEFT JOIN TSPL_ITEM_MASTER ON PP.Main_ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE " &
            " LEFT JOIN TSPL_MF_BOM_HEAD ON PP.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE " &
            " LEFT JOIN TSPL_UNIT_MASTER ON PP.MAIN_UOM=TSPL_UNIT_MASTER.UNIT_CODE " &
            " LEFT JOIN TSPL_ITEM_MASTER Consm_Item ON PP.CONSM_ITEM_CODE=Consm_Item.ITEM_CODE " &
            " left join TSPL_LOCATION_MASTER ON PP.LOCATION_CODE=TSPL_LOCATION_MASTER.LOCATION_CODE " &
            " where PP.PROD_ENTRY_CODE='" & ReceiptCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dt.Rows
            objtr = New clsStanderdProductionEntryConsumption
            objtr.AVG_COST = clsCommon.myCdbl(dr.Item("AVG_COST"))
            objtr.BOM_CODE = clsCommon.myCstr(dr.Item("BOM_CODE"))
            objtr.BOM_Desc = clsCommon.myCstr(dr.Item("BOM_Desc"))
            objtr.CONSM_ITEM_CODE = clsCommon.myCstr(dr.Item("CONSM_ITEM_CODE"))
            objtr.CONSM_ITEM_Desc = clsCommon.myCstr(dr.Item("Consm_Item_Desc"))
            objtr.Consm_Product_Type = clsCommon.myCstr(dr.Item("Consm_Product_Type"))
            objtr.CONSM_QTY = clsCommon.myCdbl(dr.Item("CONSM_QTY"))
            objtr.CONSM_QTY_Original = clsCommon.myCdbl(dr.Item("CONSM_QTY_Original"))
            objtr.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
            objtr.FAT_KG = clsCommon.myCdbl(dr.Item("FAT_KG"))
            objtr.FAT_Per = clsCommon.myCdbl(dr.Item("FAT_Per"))
            objtr.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
            objtr.FIFO_COST = clsCommon.myCdbl(dr.Item("FIFO_COST"))
            objtr.LIFO_COST = clsCommon.myCdbl(dr.Item("LIFO_COST"))
            objtr.LOCATION_CODE = clsCommon.myCstr(dr.Item("LOCATION_CODE"))
            objtr.LOCATION_Desc = clsCommon.myCstr(dr.Item("Location_Desc"))
            objtr.Main_ITEM_CODE = clsCommon.myCstr(dr.Item("Main_ITEM_CODE"))
            objtr.Main_ITEM_Desc = clsCommon.myCstr(dr.Item("Main_ITEM_Desc"))

            objtr.MAIN_UOM = clsCommon.myCstr(dr.Item("MAIN_UOM"))
            objtr.MAIN_UOM_Desc = clsCommon.myCstr(dr.Item("MAIN_UOM_Desc"))
            objtr.PROD_ENTRY_CODE = clsCommon.myCstr(dr.Item("PROD_ENTRY_CODE"))
            objtr.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))
            objtr.SNF_KG = clsCommon.myCdbl(dr.Item("SNF_KG"))
            objtr.SNF_Per = clsCommon.myCdbl(dr.Item("SNF_Per"))
            objtr.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
            objtr.UNIT_CODE = clsCommon.myCstr(dr.Item("UNIT_CODE"))
            objtr.FINAL_PRODUCTION_QTY_Min = clsCommon.myCdbl(dr.Item("FINAL_PRODUCTION_QTY_Min"))
            objtr.FINAL_PRODUCTION_QTY_Max = clsCommon.myCdbl(dr.Item("FINAL_PRODUCTION_QTY_Max"))
            objList.Add(objtr)
        Next
        Return objList
    End Function
    Public Shared Function SaveConsumption(ByVal ReceiptCode As String, ByVal Trans_Date As Date, ByVal arrConsumption As List(Of clsStanderdProductionEntryConsumption), Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        If arrConsumption Is Nothing Then
            Return True
        End If

        Dim totalFifoCost As Decimal = 0
        Dim totalLifoCost As Decimal = 0
        Dim totalAvgCost As Decimal = 0

        For Each objtr As clsStanderdProductionEntryConsumption In arrConsumption
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PROD_ENTRY_CODE", ReceiptCode)

            clsCommon.AddColumnsForChange(coll, "Main_ITEM_CODE", objtr.Main_ITEM_CODE)
            clsCommon.AddColumnsForChange(coll, "BOM_CODE", objtr.BOM_CODE)
            clsCommon.AddColumnsForChange(coll, "MAIN_UOM", objtr.MAIN_UOM)

            clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", objtr.CONSM_ITEM_CODE)

            clsCommon.AddColumnsForChange(coll, "CONSM_QTY", objtr.CONSM_QTY)
            clsCommon.AddColumnsForChange(coll, "CONSM_QTY_Original", objtr.CONSM_QTY_Original)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", objtr.LOCATION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "UNIT_CODE", objtr.UNIT_CODE)

            clsCommon.AddColumnsForChange(coll, "FAT_Per", objtr.FAT_Per)
            clsCommon.AddColumnsForChange(coll, "SNF_Per", objtr.SNF_Per)
            clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.FAT_KG)
            clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.SNF_KG)
            clsCommon.AddColumnsForChange(coll, "AVG_COST", objtr.AVG_COST)

            clsCommon.AddColumnsForChange(coll, "FINAL_PRODUCTION_QTY_MIN", objtr.FINAL_PRODUCTION_QTY_Min)
            clsCommon.AddColumnsForChange(coll, "FINAL_PRODUCTION_QTY_MAX", objtr.FINAL_PRODUCTION_QTY_Max)

            Dim BalanceQty As Decimal = 0

            Dim cost As Decimal = 0
            Dim objCost As New MIlkComponentType

            Dim Product_Type As String = clsItemMaster.GetItemProductType(objtr.CONSM_ITEM_CODE, trans)
            'Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(objtr.CONSM_ITEM_CODE) & "' "))
            'objCost = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(objtr.CONSM_ITEM_CODE, objtr.LOCATION_CODE, 1, Trans_Date, Trans_Date, False, Nothing, "TSPL_INVENTORY_MOVEMENT", clsCommon.myCstr(objtr.UNIT_CODE)) 'clsInventoryMovementNew.GetAvgCost(Product_Type, objtr.CONSM_ITEM_CODE, objtr.LOCATION_CODE, objtr.CONSM_QTY, objtr.UNIT_CODE, objtr.FAT_KG, objtr.SNF_KG, Trans_Date, clsCommon.GETSERVERDATE(trans), False, trans)

            '' production costing cols
            clsCommon.AddColumnsForChange(coll, "FAT_Amt", 0)
            clsCommon.AddColumnsForChange(coll, "SNF_Amt", 0)
            clsCommon.AddColumnsForChange(coll, "FAT_Rate", 0)
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", 0)


            cost = objCost.FAT_Cost + objCost.SNF_Cost
            clsCommon.AddColumnsForChange(coll, "FIFO_COST", cost)
            totalFifoCost = totalFifoCost + cost

            'clsCommon.AddColumnsForChange(coll, "AVG_COST", cost)
            totalAvgCost = totalAvgCost + cost

            clsCommon.AddColumnsForChange(coll, "LIFO_COST", cost)
            totalLifoCost = totalLifoCost + cost

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_CONSUMPTION_WITHOUT_BATCH", OMInsertOrUpdate.Insert, "", trans)
        Next

        Return True
    End Function

End Class
Public Class clsStanderdProductionEntryConsumptionCost
#Region "Variables"
    Public PROD_ENTRY_CODE As String
    Public Main_ITEM_CODE As String
    Public Main_ITEM_Desc As String
    Public BOM_CODE As String
    Public BOM_Desc As String
    Public MAIN_UOM As String
    Public MAIN_UOM_Desc As String
    Public COST_CODE As String
    Public COST_CODE_Desc As String
    Public OverHead_Cost As Decimal

#End Region
    Public Shared Function GetConsumptionCost(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsStanderdProductionEntryConsumptionCost)
        Dim qry As String = ""
        Dim objList As New List(Of clsStanderdProductionEntryConsumptionCost)
        Dim objtr As New clsStanderdProductionEntryConsumptionCost
        qry = "select PP.*,TSPL_ITEM_MASTER.ITEM_DESC AS Main_ITEM_Desc,TSPL_MF_BOM_HEAD.DESCRIPTION AS BOM_Desc,TSPL_UNIT_MASTER.UNIT_DESC AS  MAIN_UOM_Desc,TSPL_OVERHEAD_COST.Description AS COST_CODE_Desc from TSPL_SPP_COST_WITHOUT_BATCH PP " &
            " LEFT JOIN TSPL_ITEM_MASTER ON PP.Main_ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE " &
            " LEFT JOIN TSPL_MF_BOM_HEAD ON PP.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE " &
            " LEFT JOIN TSPL_UNIT_MASTER ON PP.MAIN_UOM=TSPL_UNIT_MASTER.UNIT_CODE " &
            " LEFT JOIN TSPL_OVERHEAD_COST ON PP.COST_CODE=TSPL_OVERHEAD_COST.COST_CODE where PP.PROD_ENTRY_CODE='" & ReceiptCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dt.Rows
            objtr = New clsStanderdProductionEntryConsumptionCost
            objtr.OverHead_Cost = clsCommon.myCdbl(dr.Item("OverHead_Cost"))
            objtr.BOM_CODE = clsCommon.myCstr(dr.Item("BOM_CODE"))
            objtr.BOM_Desc = clsCommon.myCstr(dr.Item("BOM_Desc"))
            objtr.COST_CODE = clsCommon.myCstr(dr.Item("COST_CODE"))
            objtr.COST_CODE_Desc = clsCommon.myCstr(dr.Item("COST_CODE_Desc"))

            objtr.Main_ITEM_CODE = clsCommon.myCstr(dr.Item("Main_ITEM_CODE"))
            objtr.Main_ITEM_Desc = clsCommon.myCstr(dr.Item("Main_ITEM_Desc"))
            objtr.MAIN_UOM = clsCommon.myCstr(dr.Item("MAIN_UOM"))
            objtr.MAIN_UOM_Desc = clsCommon.myCstr(dr.Item("MAIN_UOM_Desc"))
            objtr.PROD_ENTRY_CODE = clsCommon.myCstr(dr.Item("PROD_ENTRY_CODE"))
            objList.Add(objtr)
        Next
        Return objList
    End Function
    Public Shared Function SaveConsumptionCost(ByVal ReceiptCode As String, ByVal arrConsCost As List(Of clsStanderdProductionEntryConsumptionCost), Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        If arrConsCost Is Nothing Then
            Return True
        End If
        Dim totalFifoCost As Decimal = 0
        Dim totalLifoCost As Decimal = 0
        Dim totalAvgCost As Decimal = 0

        For Each objtr As clsStanderdProductionEntryConsumptionCost In arrConsCost
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PROD_ENTRY_CODE", ReceiptCode)

            clsCommon.AddColumnsForChange(coll, "Main_ITEM_CODE", objtr.Main_ITEM_CODE)
            clsCommon.AddColumnsForChange(coll, "BOM_CODE", objtr.BOM_CODE)
            clsCommon.AddColumnsForChange(coll, "MAIN_UOM", objtr.MAIN_UOM)

            clsCommon.AddColumnsForChange(coll, "COST_CODE", objtr.COST_CODE)

            clsCommon.AddColumnsForChange(coll, "OverHead_Cost", objtr.OverHead_Cost)

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_COST_WITHOUT_BATCH", OMInsertOrUpdate.Insert, "", trans)
        Next

        Return True
    End Function

End Class
Public Class clsStanderdProductionEntryImportTemplate
    Public Seq_No As Integer
    Public Prod_Date As Date
    Public Location_Code As String
    Public Category As String
    Public Consm_Loc_Milk As String
    Public Consm_Loc_Other As String
    Public BOM_Code As String
    Public Item_Code As String
    Public Item_Desc As String
    Public UOM As String
    Public Prod_Qty As Decimal
    Public objList As New List(Of clsStanderdProductionEntryImportTemplate)
    Public Shared Function SaveData(ByVal Arr As List(Of clsStanderdProductionEntryImportTemplate)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin

        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsStanderdProductionEntryImportTemplate In Arr

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Seq_No", obj.Seq_No)
                    clsCommon.AddColumnsForChange(coll, "Prod_Date", clsCommon.GetPrintDate(obj.Prod_Date, "dd-MMM-yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Category", obj.Category, True)
                    clsCommon.AddColumnsForChange(coll, "Consm_Loc_Milk", obj.Consm_Loc_Milk)
                    clsCommon.AddColumnsForChange(coll, "Consm_Loc_Other", obj.Consm_Loc_Other, True)

                    clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_Code)

                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Prod_Qty", obj.Prod_Qty)

                    clsCommon.AddColumnsForChange(coll, "Import_Status", "N")
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SPP_PRODUCTION_IMPORT", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
End Class

Public Class clsBOMRecursiveitems

#Region "Variables"
    Public ITEM_CODE As String
    Public QUANTITY As Decimal
    Public UNIT_CODE As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal

    Public Byproduct_Item_Code As String
    Public Byproduct_Item_UOM As String
    Public Byproduct_Item_Qty As Decimal
#End Region

    Public Shared Function GetItemOfBOM(ByRef Arr As List(Of clsBOMRecursiveitems), ByVal strICode As String, ByVal dblQty As Double, ByVal strUOM As String, ByVal strJobLocationCode As String, ByVal strVendorCode As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction, ByVal intLvl As Integer, Optional ByVal Is_For_Production As Boolean = False, Optional ByVal BOM_Code As String = "") As Boolean
        Return GetItemOfBOM(0, 0, Arr, strICode, dblQty, strUOM, strJobLocationCode, strVendorCode, TransDate, trans, intLvl, Is_For_Production, BOM_Code)
    End Function
    Public Shared Function GetItemOfBOM(ByRef dclExtaFATKG As Decimal, ByRef dclExtaSNFKG As Decimal, ByRef Arr As List(Of clsBOMRecursiveitems), ByVal strICode As String, ByVal dblQty As Double, ByVal strUOM As String, ByVal strJobLocationCode As String, ByVal strVendorCode As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction, ByVal intLvl As Integer, Optional ByVal Is_For_Production As Boolean = False, Optional ByVal BOM_Code As String = "") As Boolean
        Return GetItemOfBOM("", dclExtaFATKG, dclExtaSNFKG, Arr, strICode, dblQty, strUOM, strJobLocationCode, strVendorCode, TransDate, trans, intLvl, Is_For_Production, BOM_Code)
    End Function
    Public Shared Function GetItemOfBOM(ByRef strLastICode As String, ByRef dclExtaFATKG As Decimal, ByRef dclExtaSNFKG As Decimal, ByRef Arr As List(Of clsBOMRecursiveitems), ByVal strICode As String, ByVal dblQty As Double, ByVal strUOM As String, ByVal strJobLocationCode As String, ByVal strVendorCode As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction, ByVal intLvl As Integer, Optional ByVal Is_For_Production As Boolean = False, Optional ByVal BOM_Code As String = "") As Boolean
        'Dim qry As String = "select ITEM_CODE,QUANTITY*ConvFactor as QUANTITY,UNIT_CODE,FAT,SNF,FAT_KG*ConvFactor as FAT_KG,SNF_KG*ConvFactor as SNF_KG,Byproduct_Item_Code,Byproduct_Item_UOM,Byproduct_Item_Qty*ConvFactor as Byproduct_Item_Qty  from (" + Environment.NewLine +
        '       "select (" + clsCommon.myCstr(dblQty) + "/TSPL_PP_BOM_HEAD.PROD_QUANTITY)*(MulConversion.Conversion_Factor/DivideConversion.Conversion_Factor) as ConvFactor ,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_QUANTITY,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE, TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.FAT,TSPL_PP_BOM_ITEM_DETAIL.SNF,TSPL_PP_BOM_ITEM_DETAIL.FAT_KG,TSPL_PP_BOM_ITEM_DETAIL.SNF_KG,TSPL_PP_BOM_HEAD.Byproduct_Item_Code,TSPL_PP_BOM_HEAD.Byproduct_Item_Qty,TSPL_PP_BOM_HEAD.Byproduct_Item_UOM from (" + Environment.NewLine +
        '       "select top 1 TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE where  TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + strICode + "' and TSPL_PP_BOM_HEAD.Is_Post=1 " + Environment.NewLine
        'If intLvl = 1 Then
        '    If Is_For_Production Then
        '        qry += " and TSPL_PP_BOM_HEAD.BOM_CODE='" & BOM_Code & "' and TSPL_PP_BOM_HEAD.Valid_FROM_DATE<='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' " + Environment.NewLine +
        '               " and 2=(case when TSPL_PP_BOM_HEAD.Valid_UPTO_DATE is null then 2 else case when TSPL_PP_BOM_HEAD.Valid_UPTO_DATE>='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' then 2 else 1 end end)  "
        '    End If
        'End If
        'If Not Is_For_Production Then
        '    qry += " and TSPL_PP_BOM_HEAD.JobWork_Loc='" + strJobLocationCode + "' and TSPL_PP_BOM_HEAD.Vendor_Code='" + strVendorCode + "' and TSPL_PP_BOM_HEAD.Valid_FROM_DATE<='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' " + Environment.NewLine +
        '               " and 2=(case when TSPL_PP_BOM_HEAD.Valid_UPTO_DATE is null then 2 else case when TSPL_PP_BOM_HEAD.Valid_UPTO_DATE>='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' then 2 else 1 end end)  "
        'End If


        'qry += " order by TSPL_PP_BOM_HEAD.Valid_FROM_DATE desc ) TabBomNo " + Environment.NewLine +
        '       " inner join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TabBomNo.BOM_CODE " + Environment.NewLine +
        '       " left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE" + Environment.NewLine +
        '       " left outer join TSPL_ITEM_UOM_DETAIL as MulConversion on  MulConversion.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE  and MulConversion.UOM_Code='" + strUOM + "' " + Environment.NewLine +
        '       " left outer join TSPL_ITEM_UOM_DETAIL as DivideConversion on DivideConversion.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and DivideConversion.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE" + Environment.NewLine +
        '       " )xx"

        ' cast( ( " + clsCommon.myCstr(dblQty) + "  *  ( ( Target_UOM.Conversion_Factor/ nullif (Source_UOM.Conversion_Factor,0) ))  else 0 end )) as decimal(18,2) ) 
        'when TSPL_MF_BOM_DETAIL.CONSM_QUANTITY > 0 then  cast( ( " + clsCommon.myCstr(dblQty) + "  *  ( ( Target_UOM.Conversion_Factor/ nullif (Source_UOM.Conversion_Factor,0) )) ) as decimal(18,2)) else 0 end ) 
        Dim qry As String = "  select TSPL_MF_BOM_HEAD.PROD_ITEM_CODE as ITEM_CODE, " + clsCommon.myCstr(dblQty) + " as  QUANTITY,TSPL_MF_BOM_HEAD.PROD_ITEM_UNIT_CODE as UNIT_CODE, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE as Byproduct_Item_Code ,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as Byproduct_Item_UOM, ( case when TSPL_MF_BOM_DETAIL.Percentage > 0 then  cast( ((TSPL_MF_BOM_DETAIL.Percentage /100) * " + clsCommon.myCstr(dblQty) + " ) as decimal(18,2)) 
                               when TSPL_MF_BOM_DETAIL.CONSM_QUANTITY > 0 then cast( ( " + clsCommon.myCstr(dblQty) + "  *  TSPL_MF_BOM_DETAIL.CONSM_QUANTITY/TSPL_MF_BOM_HEAD.PROD_QUANTITY) as decimal(18,2)) else 0 end ) 
                               as Byproduct_Item_Qty from TSPL_MF_BOM_DETAIL 
                               inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE = TSPL_MF_BOM_DETAIL.BOM_CODE
                               left outer join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE and Source_UOM.UOM_Code = TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE
                               left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE 
							   and Target_UOM.UOM_Code = '" + strUOM + "'
                               where TSPL_MF_BOM_HEAD.PROD_ITEM_CODE = '" + strICode + "' and  PROD_ITEM_UNIT_CODE = '" + strUOM + "' and  TSPL_MF_BOM_HEAD.BOM_CODE = '" + BOM_Code + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        intLvl += 1
        Dim isFound As Boolean = False
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            isFound = True
            For Each dr As DataRow In dt.Rows
                Dim strBOMItemCode As String = clsCommon.myCstr(dr("ITEM_CODE"))
                Dim strBOMItemUOM As String = clsCommon.myCstr(dr("UNIT_CODE"))
                Dim dclBOMItemQty As Decimal = clsCommon.myCdbl(dr("QUANTITY"))
                Dim dclBOMFATKg As Decimal = 0 'clsCommon.myCdbl(dr("FAT_KG"))
                Dim dclBOMSNFKg As Decimal = 0 'clsCommon.myCdbl(dr("SNF_KG"))
                Dim dclBalItemQty As Decimal = 0
                Dim dclBalFATKg As Decimal = 0
                Dim dclBalSNFKg As Decimal = 0
                If Not Is_For_Production Then
                    qry = "select CL_QTY,CL_FAT_KG,CL_SNF_KG from TSPL_FUN_ITEM_LOC_BALANCE('" + strBOMItemCode + "','" + strJobLocationCode + "','" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy hh:mm tt") + "')"
                    Dim dtStock As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtStock IsNot Nothing AndAlso dtStock.Rows.Count > 0 Then
                        Dim dclStockQty As Decimal = clsCommon.myCdbl(dtStock.Rows(0)("CL_QTY"))
                        dclBalFATKg = clsCommon.myCdbl(dtStock.Rows(0)("CL_FAT_KG"))
                        dclBalSNFKg = clsCommon.myCdbl(dtStock.Rows(0)("CL_SNF_KG"))
                        Try
                            dclStockQty = dclStockQty / clsItemMaster.GetConvertionFactor(strBOMItemCode, strBOMItemUOM, trans)
                        Catch ex As Exception
                            dclStockQty = 0
                        End Try
                        If clsCommon.myCdbl(dclStockQty) > 0 Then
                            dclBalItemQty = dclStockQty - dclBOMItemQty
                            Dim obj As New clsBOMRecursiveitems
                            obj.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                            If dclBalItemQty < 0 Then
                                obj.QUANTITY = dclStockQty
                                dclBOMFATKg = dclBOMFATKg / dclBOMItemQty * dclStockQty
                                dclBOMSNFKg = dclBOMSNFKg / dclBOMItemQty * dclStockQty

                                dclBOMItemQty = Math.Abs(dclBalItemQty)
                            Else
                                obj.QUANTITY = dclBOMItemQty
                                obj.Byproduct_Item_Code = clsCommon.myCstr(dr("Byproduct_Item_Code"))
                                obj.Byproduct_Item_UOM = clsCommon.myCstr(dr("Byproduct_Item_UOM"))
                                obj.Byproduct_Item_Qty = clsCommon.myCdbl(dr("Byproduct_Item_Qty"))
                            End If
                            obj.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                            obj.FAT = clsCommon.myCdbl(dr("FAT"))
                            obj.SNF = clsCommon.myCdbl(dr("SNF"))
                            If dclBOMFATKg > dclBalFATKg Then
                                obj.FAT_KG = dclBalFATKg
                                dclExtaFATKG += dclBOMFATKg - dclBalFATKg
                            Else
                                obj.FAT_KG = dclBOMFATKg
                            End If
                            If dclBOMSNFKg > dclBalSNFKg Then
                                obj.SNF_KG = dclBalSNFKg
                                dclExtaSNFKG += dclBOMSNFKg - dclBalSNFKg
                            Else
                                obj.SNF_KG = dclBOMSNFKg
                            End If


                            Arr.Add(obj)
                            If dclBalItemQty > 0 Then
                                Continue For
                            End If
                        End If
                    End If
                End If
                'Dim isFoundInner As Boolean = True
                'If (clsCommon.myLen(strLastICode) > 0 AndAlso clsCommon.CompairString(strLastICode, strBOMItemCode) = CompairStringResult.Equal) Then
                '    isFoundInner = False
                'Else
                '    isFoundInner = GetItemOfBOM(strLastICode, dclExtaFATKG, dclExtaSNFKG, Arr, strBOMItemCode, dclBOMItemQty, strBOMItemUOM, strJobLocationCode, strVendorCode, TransDate, trans, intLvl, Is_For_Production)
                'End If

                If 2 = 2 Then 'Not isFoundInner
                    Dim obj As New clsBOMRecursiveitems
                    obj.ITEM_CODE = clsCommon.myCstr(dr("Byproduct_Item_Code")) 'strBOMItemCode
                    obj.QUANTITY = clsCommon.myCdbl(dr("Byproduct_Item_Qty")) 'dclBOMItemQty
                    obj.UNIT_CODE = clsCommon.myCstr(dr("Byproduct_Item_UOM")) 'strBOMItemUOM
                    obj.FAT = 0 'clsCommon.myCdbl(dr("FAT"))
                    obj.SNF = 0 'clsCommon.myCdbl(dr("SNF"))
                    obj.FAT_KG = dclBOMFATKg
                    obj.SNF_KG = dclBOMSNFKg

                    obj.Byproduct_Item_Code = clsCommon.myCstr(dr("Byproduct_Item_Code"))
                    obj.Byproduct_Item_UOM = clsCommon.myCstr(dr("Byproduct_Item_UOM"))
                    obj.Byproduct_Item_Qty = clsCommon.myCdbl(dr("Byproduct_Item_Qty"))
                    Arr.Add(obj)
                End If

            Next
        End If
        Return isFound
    End Function



End Class

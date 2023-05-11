Imports System.Data.SqlClient
Imports common


Public Class clsAssemblies
#Region "variables"
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

    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
    Public Comp_Code As String = Nothing
    Public POSTED As Boolean = False
    Public Posting_Date As Date? = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public objList As List(Of clsAssembliesItemDetail) = Nothing
    Public Serial_No As String = ""
#End Region
#Region "Functions"

    Public Shared Function SaveData(ByVal obj As clsAssemblies, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = ""

        Try
            Dim coll As New Hashtable()
            ''(Locked Transaction) 05/02/2018
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "MM Assemblies/Disassemblies", obj.LOCATION_CODE, clsCommon.myCDate(obj.ASSEMBLY_DATE), trans)
            If isNewEntry Then
                If clsCommon.myLen(obj.CODE) = 0 Then
                    obj.CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.Assemblies, "", "")
                Else
                    obj.CODE = clsCommon.myCstr(obj.CODE)
                End If
            End If
            '' delete TSPL_PJC_ASSEMBLIES_ITEM_DETAIL
            qry = "DELETE FROM TSPL_PJC_ASSEMBLIES_ITEM_DETAIL WHERE ASSEMBLY_CODE='" + obj.CODE + "'"
            Dim isSaved As Boolean = True

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
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_ASSEMBLIES", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_ASSEMBLIES", OMInsertOrUpdate.Update, "TSPL_PJC_ASSEMBLIES.CODE='" + obj.CODE + "'", trans)
            End If

            '' saving assemblies item detail
            isSaved = isSaved AndAlso clsAssembliesItemDetail.SaveData(obj.CODE, obj.objList, trans)
            '' saving custom fields
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.CODE, obj.arrCustomFields, trans)
        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_PJC_ASSEMBLIES where code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean

        'Try
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If
        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
        Dim obj As clsAssemblies = clsAssemblies.GetData(strDocNo, NavigatorType.Current, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.CODE) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (isCheckForPosted AndAlso obj.POSTED = 1) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If

        Dim qry As String = "Update TSPL_PJC_ASSEMBLIES set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where CODE ='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        'Catch ex As Exception

        '    Throw New Exception(ex.Message)
        'End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsAssemblies
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAssemblies
        Dim obj As clsAssemblies = Nothing
        Dim Arr As List(Of clsAssemblies) = Nothing
        Dim qry As String = "select TSPL_PJC_ASSEMBLIES.CODE,TSPL_PJC_ASSEMBLIES.ASSEMBLY_DATE,TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE,TSPL_PJC_ASSEMBLIES.DISASSEMBLY_TYPE, " & _
        " TSPL_PJC_ASSEMBLIES.ASSEMBLY_CODE,TSPL_PJC_ASSEMBLIES.DESCRIPTION ,TSPL_PJC_ASSEMBLIES.COMMENTS, " & _
        " TSPL_PJC_ASSEMBLIES1.DESCRIPTION AS ASSEMBLY_DESC,TSPL_PJC_ASSEMBLIES.Main_Item_Code,TSPL_ITEM_MASTER.Item_Desc AS Main_Item_Desc, " & _
        " TSPL_PJC_ASSEMBLIES.BOM_CODE,TSPL_MF_BOM_HEAD.DESCRIPTION AS Bom_Desc,TSPL_PJC_ASSEMBLIES.COMP_ASSEMBL_METHOD,TSPL_PJC_ASSEMBLIES.LOCATION_CODE, " & _
        " TSPL_LOCATION_MASTER.Location_Desc,TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY,TSPL_PJC_ASSEMBLIES.QUANTITY,TSPL_PJC_ASSEMBLIES.DISASSEMBLY_COST, " & _
        " TSPL_PJC_ASSEMBLIES.BUILD_ITEM_UNIT_CODE,TSPL_PJC_ASSEMBLIES.POSTED,TSPL_PJC_ASSEMBLIES.POSTING_DATE,TSPL_PJC_ASSEMBLIES.Serial_No from TSPL_PJC_ASSEMBLIES left join TSPL_MF_BOM_HEAD ON TSPL_PJC_ASSEMBLIES.BOM_CODE=TSPL_PJC_ASSEMBLIES.BOM_CODE " & _
        " LEFT JOIN TSPL_PJC_ASSEMBLIES AS TSPL_PJC_ASSEMBLIES1 ON TSPL_PJC_ASSEMBLIES.ASSEMBLY_CODE=TSPL_PJC_ASSEMBLIES1.CODE " & _
        " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_PJC_ASSEMBLIES.Main_Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_PJC_ASSEMBLIES.LOCATION_CODE=TSPL_LOCATION_MASTER.Location_Code " & _
        " where 2=2 "

        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PJC_ASSEMBLIES.CODE = (select MIN(CODE) from TSPL_PJC_ASSEMBLIES WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_PJC_ASSEMBLIES.CODE = (select Max(CODE) from TSPL_PJC_ASSEMBLIES WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_PJC_ASSEMBLIES.CODE = (select top 1 CODE from TSPL_PJC_ASSEMBLIES WHERE 1=1 " + whrclas + " and CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_PJC_ASSEMBLIES.CODE = (select Min(CODE) from TSPL_PJC_ASSEMBLIES where CODE>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_PJC_ASSEMBLIES.CODE = (select Max(CODE) from TSPL_PJC_ASSEMBLIES where CODE<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAssemblies()
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
            obj.Serial_No = clsCommon.myCstr(dt.Rows(0)("Serial_No"))
            obj.POSTED = dt.Rows(0)("POSTED")
            If IsDBNull(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = Nothing
            Else
                obj.Posting_Date = dt.Rows(0)("Posting_Date")
            End If
            obj.objList = clsAssembliesItemDetail.GetBomAssembliesDetail(obj.CODE, trans)
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
#End Region

End Class

Public Class clsAssembliesItemDetail
#Region "Variables"
    Public ASSEMBLY_CODE As String
    Public BOM_CODE As String
    Public LINE_NO As String
    Public CONSM_ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public CONSM_QUANTITY As String
    Public CONSM_ITEM_UNIT_CODE As String
    Public LOCATION_CODE As String
    Public ItemStatus As String = Nothing
    Public ItemType As String = Nothing
    Public Serial_No As String = Nothing
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsAssembliesItemDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsAssembliesItemDetail In Arr
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

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_ASSEMBLIES_ITEM_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PJC_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetBomAssembliesDetail(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsAssembliesItemDetail)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "select * from TSPL_PJC_ASSEMBLIES_ITEM_DETAIL where TSPL_PJC_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE='" & strCode & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsAssembliesItemDetail
        Dim ObjList As New List(Of clsAssembliesItemDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsAssembliesItemDetail()
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

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsBillOfMaterial


#Region "Variables"

    Public BOM_CODE As String
    Public DESCRIPTION As String
    Public BOM_DATE As Date
    Public REVISION_NO As String
    Public START_DATE As Date
    Public END_DATE As Date? = Nothing
    Public STATUS As String
    Public IS_DEFAULT As Boolean
    Public ATTACHED_DOC As Byte()
    Public ATTACHED_DOC_PATH As String
    Public PROD_ITEM_CODE As String
    Public PROD_ITEM_NAME As String
    Public PROD_QUANTITY As String
    Public PROD_ITEM_UNIT_CODE As String
    Public MIN_BATCH_SIZE As Decimal
    Public CREATED_BY As String
    Public APPROVED_BY As String
    Public LOCATION_CODE As String
    Public POSTED As Boolean
    Public Posting_Date As Date


    '' grid columns
    Public Line_No As Integer
    Public CONSM_ITEM_CATEGORY_CODE As String
    Public CONSM_ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public CONSM_QUANTITY As Decimal = 0.0
    Public CONSM_ITEM_UNIT_CODE As String
    Public SCRAP_PERCENT As Decimal
    Public WASTAGE_PERCENT As Decimal
    Public REMARKS As String
    Public Is_Principle As String = Nothing
    Public ITEM_TYPE As String
    Public CONSM_Drawing_No As String = Nothing
    Public Percentage As Decimal = 0.0
    Public Alternate_Item_Code As String = Nothing

    Public Shared ObjList As List(Of clsBillOfMaterial)
    Public ObjListOP As List(Of clsBOMOperations)
    Public ObjListRes As List(Of clsBOMResources)
    Public ObjListToolType As List(Of clsBOMToolTypes)
    Public ObjListCosting As List(Of clsBOMCosting)
    Public PROD_Drawing_No As String = Nothing
    'Public Arr As New List(Of clsEmpSalaryPayHeadDetails)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBillOfMaterial
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_MF_BOM_DETAIL where BOM_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_BOM_OPERATIONS
            qry = "DELETE FROM TSPL_MF_BOM_OPERATIONS WHERE BOM_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_BOM_RESOURCES
            qry = "DELETE FROM TSPL_MF_BOM_RESOURCES WHERE BOM_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_BOM_TOOLTYPES
            qry = "DELETE FROM TSPL_MF_BOM_TOOLTYPES WHERE BOM_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_BOM_COSTING
            qry = "DELETE FROM TSPL_MF_BOM_COSTING WHERE BOM_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_BOM_HEAD where BOM_CODE ='" + strCode + "' and trans_type='BOM'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBillOfMaterial
        Dim arrLoc As String = Nothing
        Dim objLoc As New clsMCCCodes()
        objLoc = clsMCCCodes.GetData()

        If objLoc.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(objLoc.arrLocCodes) > 0 Then
            arrLoc = objLoc.arrLocCodes
        End If

        Dim obj As New clsBillOfMaterial()
        Dim objtr As New clsBillOfMaterial()

        ObjList = New List(Of clsBillOfMaterial)

        Dim qry As String = "SELECT T1.PROD_Drawing_No,T1.BOM_CODE,T1.DESCRIPTION,T1.BOM_DATE,T1.REVISION_NO,T1.START_DATE,T1.END_DATE,T1.STATUS,"
        qry += " T1.IS_DEFAULT,T1.ATTACHED_DOC,T1.ATTACHED_DOC_PATH,T1.PROD_ITEM_CODE,T2.ITEM_DESC AS PROD_ITEM_NAME,T2.BomBuildQty,T1.PROD_ITEM_UNIT_CODE,"
        qry += " T1.MIN_BATCH_SIZE,T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE,T1.LOCATION_CODE FROM TSPL_MF_BOM_HEAD  T1 INNER JOIN TSPL_ITEM_MASTER T2  ON T1.PROD_ITEM_CODE=T2.ITEM_CODE where 2=2 and trans_type='BOM'"
        Dim WhrCls As String = " and trans_type='BOM' "
        If clsCommon.myLen(arrLoc) > 0 Then
            WhrCls += " and Location_Code in (" + arrLoc + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " AND BOM_CODE = (select MIN(BOM_CODE) from TSPL_MF_BOM_HEAD where 1=1 " + WhrCls + ")"
            Case NavigatorType.Last
                qry += " AND BOM_CODE = (select Max(BOM_CODE) from TSPL_MF_BOM_HEAD where 1=1 " + WhrCls + ")"
            Case NavigatorType.Next
                qry += " AND BOM_CODE = (select Min(BOM_CODE) from TSPL_MF_BOM_HEAD where  BOM_CODE>'" + strCode + "'  " + WhrCls + ")"
            Case NavigatorType.Previous
                qry += " AND BOM_CODE = (select Max(BOM_CODE) from TSPL_MF_BOM_HEAD where BOM_CODE<'" + strCode + "'  " + WhrCls + ")"
            Case NavigatorType.Current
                qry += " AND BOM_CODE = '" + strCode + "'"

        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.BOM_CODE = clsCommon.myCstr(dt.Rows(0)("BOM_CODE"))
            obj.PROD_Drawing_No = clsCommon.myCstr(dt.Rows(0)("PROD_Drawing_No"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.BOM_DATE = clsCommon.GetPrintDate(dt.Rows(0)("BOM_DATE"), "dd/MMM/yyyy")
            obj.REVISION_NO = clsCommon.myCstr(dt.Rows(0)("REVISION_NO"))
            obj.START_DATE = clsCommon.GetPrintDate(dt.Rows(0)("START_DATE"), "dd/MMM/yyyy")
            If IsDBNull(dt.Rows(0)("END_DATE")) = True Then
                obj.END_DATE = Nothing
            Else
                obj.END_DATE = clsCommon.GetPrintDate(dt.Rows(0)("END_DATE"), "dd/MMM/yyyy")
            End If

            obj.STATUS = clsCommon.myCstr(dt.Rows(0)("STATUS"))
            obj.IS_DEFAULT = clsCommon.myCBool(dt.Rows(0)("IS_DEFAULT"))
            If IsDBNull(dt.Rows(0)("ATTACHED_DOC")) = True Then
                obj.ATTACHED_DOC = Nothing
            Else
                obj.ATTACHED_DOC = CType(dt.Rows(0)("ATTACHED_DOC"), Byte())
            End If

            obj.ATTACHED_DOC_PATH = clsCommon.myCstr(dt.Rows(0)("ATTACHED_DOC_PATH"))
            obj.PROD_ITEM_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_ITEM_CODE"))
            obj.PROD_ITEM_NAME = clsCommon.myCstr(dt.Rows(0)("PROD_ITEM_NAME"))
            obj.PROD_QUANTITY = clsCommon.myCdbl(dt.Rows(0)("BomBuildQty"))
            obj.PROD_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_ITEM_UNIT_CODE"))
            obj.MIN_BATCH_SIZE = clsCommon.myCdbl(dt.Rows(0)("MIN_BATCH_SIZE"))
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.APPROVED_BY = clsCommon.myCstr(dt.Rows(0)("APPROVED_BY"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            strCode = dt.Rows(0)("BOM_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = "SELECT T1.CONSM_Drawing_No,T1.BOM_CODE,T1.LINE_NO,T1.CONSM_ITEM_CATEGORY_CODE,T1.CONSM_ITEM_CODE,T1.ITEM_DESCRIPTION, "
        qry += " T1.CONSM_QUANTITY,T1.Percentage,T1.Alternate_Item_Code,T1.CONSM_ITEM_UNIT_CODE,T1.SCRAP_PERCENT,T1.WASTAGE_PERCENT,T1.REMARKS,Is_Principle,TSPL_ITEM_MASTER.ITEM_TYPE " &
        " FROM TSPL_MF_BOM_DETAIL T1 LEFT JOIN TSPL_ITEM_MASTER ON T1.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  WHERE 2=2"
        qry += " AND T1.BOM_CODE = '" + strCode + "' ORDER BY T1.LINE_NO"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsBillOfMaterial()

                objtr.Line_No = clsCommon.myCdbl(dr("LINE_NO"))
                objtr.CONSM_Drawing_No = clsCommon.myCstr(dr("CONSM_Drawing_No"))
                objtr.CONSM_ITEM_CATEGORY_CODE = clsCommon.myCstr(dr("CONSM_ITEM_CATEGORY_CODE"))
                objtr.CONSM_ITEM_CODE = clsCommon.myCstr(dr("CONSM_ITEM_CODE"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.CONSM_QUANTITY = clsCommon.myCdbl(dr("CONSM_QUANTITY"))
                objtr.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(dr("CONSM_ITEM_UNIT_CODE"))
                objtr.SCRAP_PERCENT = clsCommon.myCdbl(dr("SCRAP_PERCENT"))
                objtr.WASTAGE_PERCENT = clsCommon.myCdbl(dr("WASTAGE_PERCENT"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))
                objtr.ITEM_TYPE = clsCommon.myCstr(dr("ITEM_TYPE"))
                objtr.Percentage = clsCommon.myCdbl(dr("Percentage"))
                objtr.Alternate_Item_Code = clsCommon.myCstr(dr("Alternate_Item_Code"))
                If dr("Is_Principle") Is DBNull.Value Then
                    objtr.Is_Principle = "0"
                Else
                    objtr.Is_Principle = clsCommon.myCstr(dr("Is_Principle"))
                End If

                ObjList.Add(objtr)
            Next
        End If

        clsBillOfMaterial.ObjList = ObjList
        obj.ObjListOP = clsBOMOperations.GetBomOperations(strCode, trans)
        'obj.ObjListRes = clsBOMResources.GetBomResources(strCode, trans)
        'obj.ObjListToolType = clsBOMToolTypes.GetBomToolTypes(strCode, trans)
        obj.ObjListCosting = clsBOMCosting.GetBomCosting(strCode, trans)

        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsBillOfMaterial, ByVal objList As List(Of clsBillOfMaterial), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If isNewEntry Then
                If strCode = "" Then
                    obj.BOM_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.BOM_DATE, "dd/MMM/yyyy"), clsDocType.BOM, clsDocTransactionType.SNQuotationOther, "")
                Else
                    obj.BOM_CODE = strCode
                End If
            End If
            '' delete TSPL_MF_BOM_DETAIL
            Dim qry As String = "DELETE FROM TSPL_MF_BOM_DETAIL WHERE BOM_CODE='" + obj.BOM_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_BOM_OPERATIONS
            qry = "DELETE FROM TSPL_MF_BOM_OPERATIONS WHERE BOM_CODE='" + obj.BOM_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_BOM_RESOURCES
            qry = "DELETE FROM TSPL_MF_BOM_RESOURCES WHERE BOM_CODE='" + obj.BOM_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_BOM_TOOLTYPES
            qry = "DELETE FROM TSPL_MF_BOM_TOOLTYPES WHERE BOM_CODE='" + obj.BOM_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_BOM_COSTING
            qry = "DELETE FROM TSPL_MF_BOM_COSTING WHERE BOM_CODE='" + obj.BOM_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.BOM_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "BOM_DATE", clsCommon.GetPrintDate(obj.BOM_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "REVISION_NO", obj.REVISION_NO)
            clsCommon.AddColumnsForChange(coll, "START_DATE", clsCommon.GetPrintDate(obj.START_DATE, "dd/MMM/yyyy"))
            If Not obj.END_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "END_DATE", clsCommon.GetPrintDate(obj.END_DATE, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "END_DATE", "", True)
            End If

            clsCommon.AddColumnsForChange(coll, "STATUS", clsCommon.myCstr(obj.STATUS))
            clsCommon.AddColumnsForChange(coll, "IS_DEFAULT", clsCommon.myCdbl(obj.IS_DEFAULT))
            'clsCommon.AddColumnsForChange(coll, "ATTACHED_DOC", obj.ATTACHED_DOC)
            clsCommon.AddColumnsForChange(coll, "ATTACHED_DOC_PATH", clsCommon.myCstr(obj.ATTACHED_DOC_PATH))
            clsCommon.AddColumnsForChange(coll, "PROD_ITEM_CODE", clsCommon.myCstr(obj.PROD_ITEM_CODE))
            clsCommon.AddColumnsForChange(coll, "PROD_QUANTITY", clsCommon.myCdbl(obj.PROD_QUANTITY))
            clsCommon.AddColumnsForChange(coll, "PROD_ITEM_UNIT_CODE", clsCommon.myCstr(obj.PROD_ITEM_UNIT_CODE))
            clsCommon.AddColumnsForChange(coll, "MIN_BATCH_SIZE", clsCommon.myCdbl(obj.MIN_BATCH_SIZE))
            'clsCommon.AddColumnsForChange(coll, "APPROVED_BY", clsCommon.myCstr(obj.APPROVED_BY), True)
            clsCommon.AddColumnsForChange(coll, "PROD_Drawing_No", obj.PROD_Drawing_No)

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            'clsCommon.AddColumnsForChange(coll, "Approved_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "trans_type", "BOM")
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(obj.LOCATION_CODE))
            If isNewEntry Then

                'clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MF_BOM_HEAD where BOM_CODE = '" & obj.BOM_CODE & "' and trans_type='BOM'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BOM_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.BOM_CODE + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BOM_HEAD", OMInsertOrUpdate.Update, "TSPL_MF_BOM_HEAD.BOM_CODE='" + obj.BOM_CODE + "' and trans_type='BOM'", trans)
            End If
            isSaved = isSaved AndAlso clsBOMDetail.SaveData(obj.BOM_CODE, obj.REVISION_NO, objList, trans)
            '' saving bom operations 
            If Not obj.ObjListOP Is Nothing Then
                isSaved = isSaved AndAlso clsBOMOperations.SaveData(obj.BOM_CODE, obj.ObjListOP, trans)
            End If

            '' saving bom resources 
            If Not obj.ObjListRes Is Nothing Then
                isSaved = isSaved AndAlso clsBOMResources.SaveData(obj.BOM_CODE, obj.ObjListRes, trans)
            End If

            '' saving bom resources 
            If Not obj.ObjListToolType Is Nothing Then
                isSaved = isSaved AndAlso clsBOMToolTypes.SaveData(obj.BOM_CODE, obj.ObjListToolType, trans)
            End If

            '' saving bom COSTING 
            If Not obj.ObjListCosting Is Nothing Then
                isSaved = isSaved AndAlso clsBOMCosting.SaveData(obj.BOM_CODE, obj.ObjListCosting, trans)
            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetBOMRevisionNo(ByVal Main_Item_Code As String, ByVal Trans_Type As String, ByVal Location_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        '' REVISION NO
        Dim rev_no As String = ""
        Dim STRQ As String
        STRQ = "SELECT max(revision_no) as rev FROM TSPL_MF_BOM_HEAD WHERE PROD_ITEM_CODE='" & Main_Item_Code & "' and trans_type='" & Trans_Type & "' and LOCATION_CODE='" & Location_Code & "' "
        rev_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue(STRQ, trans))
        If clsCommon.myLen(rev_no) <= 0 Then
            rev_no = Main_Item_Code + "/001"
        Else
            rev_no = clsCommon.incval(rev_no)
        End If
        Return rev_no
    End Function

    Public Shared Function FindBOMRevisionNo(ByVal BOM_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        '' REVISION NO
        Dim rev_no As String
        Dim STRQ As String
        STRQ = "SELECT REVISION_NO FROM TSPL_MF_BOM_HEAD WHERE BOM_Code='" & BOM_Code & "'  "
        rev_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue(STRQ, trans))
        Return rev_no
    End Function

    Public Shared Function GetBOMLocationCode(ByVal BOM_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        '' REVISION NO
        Dim Location_Code As String = ""
        Dim STRQ As String
        STRQ = "SELECT Location_Code FROM TSPL_MF_BOM_HEAD WHERE BOM_Code='" & BOM_Code & "'  "
        Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(STRQ, trans))
        Return Location_Code
    End Function

    Public Shared Function GetMainItemBOMList(ByVal Main_Item_Code As String, ByVal Trans_Type As String, ByVal Location_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of String)
        '' REVISION NO
        Dim BOMList As New List(Of String)
        Dim STRQ As String = ""
        STRQ = "SELECT BOM_CODE FROM TSPL_MF_BOM_HEAD WHERE PROD_ITEM_CODE='" & Main_Item_Code & "' and trans_type='" & Trans_Type & "' and LOCATION_CODE='" & Location_Code & "'  "
        Dim dt_rev As DataTable
        dt_rev = clsDBFuncationality.GetDataTable(STRQ, trans)
        For Each row As DataRow In dt_rev.Rows
            BOMList.Add(row.Item("BOM_CODE"))
        Next
        Return BOMList
    End Function
    Public Shared Function GetMainItem(ByVal BOM_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        '' REVISION NO
        Dim BOMList As New List(Of String)
        Dim STRQ As String = ""
        STRQ = "SELECT PROD_ITEM_CODE FROM TSPL_MF_BOM_HEAD WHERE BOM_CODE='" & BOM_CODE & "' "
        Dim PROD_ITEM_CODE As String
        PROD_ITEM_CODE = clsDBFuncationality.getSingleValue(STRQ, trans)

        Return PROD_ITEM_CODE
    End Function
    Public Shared Function CheckBOMCode(ByVal strICode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select count(BOM_CODE) from TSPL_MF_BOM_HEAD where BOM_CODE='" + strICode + "' "
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetLineNo(ByVal BOM_Code As String, ByVal trans As SqlTransaction) As Integer
        Dim qry As String = "select (count(CONSM_ITEM_CODE)+1) from TSPL_MF_BOM_DETAIL where BOM_CODE='" & BOM_Code & "' "
        Dim Line_No As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return Line_No
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsBillOfMaterial = clsBillOfMaterial.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.BOM_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MF_BOM_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where BOM_CODE ='" + strDocNo + "' and trans_type='BOM'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function FinderForBOM(ByVal strCode As String, ByVal isButtonClicked As Boolean, Optional ByVal Item_Code As String = "") As clsBillOfMaterial
        Dim obj As clsBillOfMaterial = Nothing
        Dim strTransType As String = ""
        Dim strIndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing))
        If clsCommon.CompairString(strIndustryType, "R") = CompairStringResult.Equal Then
            strTransType = "Rice"
        Else
            strTransType = "BOM"
        End If

        Dim qry As String = "select BOM_CODE as Code,DESCRIPTION as Name,BOM_DATE,REVISION_NO,PROD_ITEM_CODE AS ITEM_CODE,PROD_QUANTITY AS BUILD_QTY,START_DATE,END_DATE,STATUS,IS_DEFAULT  from TSPL_MF_BOM_HEAD"
        Dim WhrCls As String = "STATUS='APPROVED' and trans_type='" & strTransType & "'"
        If clsCommon.myLen(Item_Code) > 0 Then
            WhrCls = WhrCls & " and PROD_ITEM_CODE='" & Item_Code & "'"
        End If
        strCode = clsCommon.ShowSelectForm("BOMFINDER", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select T1.BOM_CODE as Code,T1.DESCRIPTION as Name,T1.REVISION_NO,T1.PROD_ITEM_CODE AS ITEM_CODE,T2.ITEM_DESC,PROD_ITEM_UNIT_CODE AS UNIT_CODE,PROD_QUANTITY AS BUILD_QTY FROM TSPL_MF_BOM_HEAD T1 LEFT JOIN TSPL_ITEM_MASTER T2 ON  T1.PROD_ITEM_CODE=T2.ITEM_CODE  WHERE T1.BOM_CODE='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsBillOfMaterial()
                obj.BOM_CODE = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.REVISION_NO = clsCommon.myCstr(dt.Rows(0)("REVISION_NO"))
                obj.PROD_ITEM_CODE = clsCommon.myCstr(dt.Rows(0)("ITEM_CODE"))
                obj.ITEM_DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("ITEM_DESC"))
                obj.PROD_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("UNIT_CODE"))
                obj.PROD_QUANTITY = clsCommon.myCdbl(dt.Rows(0)("BUILD_QTY"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function FinderForItem(ByVal strCode As String, ByVal WhrCls As String, ByVal isButtonClicked As Boolean) As clsBillOfMaterial
        Dim obj As clsBillOfMaterial = Nothing
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name,ITEM_TYPE AS [Item Type],Drawing_No as [Drawing No] ,TSPL_Item_Category.Category_Name as [Item Category] ,TSPL_ITEM_SUB_CATEGORY.Description as [Sub Category] from  TSPL_ITEM_MASTER"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code =TSPL_ITEM_MASTER.item_category "
        qry += "  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code  =TSPL_ITEM_MASTER.Sub_item_category "

        strCode = clsItemMaster.getFinder(WhrCls, strCode, isButtonClicked) ' clsCommon.ShowSelectForm("STDBOMItemFinder", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,item_type,Item_Desc,Unit_Code,drawing_no from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsBillOfMaterial()
                obj.PROD_ITEM_CODE = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.ITEM_DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.PROD_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                obj.ITEM_TYPE = clsCommon.myCstr(dt.Rows(0)("item_type"))
                obj.CONSM_Drawing_No = clsCommon.myCstr(dt.Rows(0)("drawing_no"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function GetPrincipleItemCount(ByVal Bom_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim qry As String = " select BOM_CODE,CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Type,coalesce(TSPL_MF_BOM_DETAIL.Is_Principle,0) as Is_Principle from TSPL_MF_BOM_DETAIL " &
                            " inner join TSPL_ITEM_MASTER  on TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
                            " where TSPL_MF_BOM_DETAIL.BOM_CODE='" & Bom_Code & "'"
        Dim totalPrincipal As Integer = 0
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dt.Rows
            If clsCommon.CompairString(dr.Item("Item_Type"), "S") = CompairStringResult.Equal Or clsCommon.CompairString(dr.Item("Item_Type"), "F") = CompairStringResult.Equal Then

                Dim dt1 As DataTable = clsManufacturingOrder.GetItemBOM(dr.Item("CONSM_ITEM_CODE"), trans)
                If dt1.Rows.Count > 0 Then
                    If clsCommon.myLen(dt1.Rows(0).Item("BOM_Code")) > 0 Then
                        totalPrincipal = totalPrincipal + GetPrincipleItemCount(dt1.Rows(0).Item("BOM_Code"))
                    End If
                End If

            Else
                If clsCommon.myCdbl(dr.Item("Is_Principle")) = 1 Then
                    totalPrincipal = totalPrincipal + 1
                End If
            End If
        Next
        Return totalPrincipal
    End Function
End Class


Public Class clsBOMDetail
#Region "Variables"

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Revision_No As String, ByVal Arr As List(Of clsBillOfMaterial), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBillOfMaterial In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CATEGORY_CODE", obj.CONSM_ITEM_CATEGORY_CODE, True)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", obj.CONSM_ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "CONSM_QUANTITY", obj.CONSM_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_UNIT_CODE", obj.CONSM_ITEM_UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "SCRAP_PERCENT", obj.SCRAP_PERCENT)
                clsCommon.AddColumnsForChange(coll, "WASTAGE_PERCENT", obj.WASTAGE_PERCENT)
                clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                clsCommon.AddColumnsForChange(coll, "REVISION_NO", Revision_No)
                clsCommon.AddColumnsForChange(coll, "Is_Principle", obj.Is_Principle)
                clsCommon.AddColumnsForChange(coll, "CONSM_Drawing_No", obj.CONSM_Drawing_No)
                clsCommon.AddColumnsForChange(coll, "Percentage", obj.Percentage)
                clsCommon.AddColumnsForChange(coll, "Alternate_Item_Code", obj.Alternate_Item_Code, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BOM_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MF_BOM_DETAIL.BOM_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function SaveDataImport(ByVal Arr As List(Of clsBillOfMaterial), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBillOfMaterial In Arr
                Dim qry As String = "DELETE FROM TSPL_MF_BOM_DETAIL WHERE BOM_CODE='" + obj.BOM_CODE + "' AND CONSM_ITEM_CODE='" & obj.CONSM_ITEM_CODE & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CATEGORY_CODE", obj.CONSM_ITEM_CATEGORY_CODE, True)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", obj.CONSM_ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "CONSM_QUANTITY", obj.CONSM_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_UNIT_CODE", obj.CONSM_ITEM_UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "SCRAP_PERCENT", obj.SCRAP_PERCENT)
                clsCommon.AddColumnsForChange(coll, "WASTAGE_PERCENT", obj.WASTAGE_PERCENT)
                clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                clsCommon.AddColumnsForChange(coll, "REVISION_NO", obj.REVISION_NO)
                clsCommon.AddColumnsForChange(coll, "Is_Principle", obj.Is_Principle)
                clsCommon.AddColumnsForChange(coll, "Percentage", obj.Percentage)
                clsCommon.AddColumnsForChange(coll, "Alternate_Item_Code", obj.Alternate_Item_Code, True)
                ''AND TSPL_MF_BOM_DETAIL.LINE_NO=" + obj.Line_No + "
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BOM_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MF_BOM_DETAIL.BOM_CODE='" + obj.BOM_CODE + "' AND CONSM_ITEM_CODE='" & obj.CONSM_ITEM_CODE & "'", trans)
            Next

        End If

        Return True
    End Function

End Class

Public Class clsBOMResources
#Region "Variables"
    Public BOM_CODE As String
    Public OPERATION_CODE As String
    Public WORK_CENTER_CODE As String
    Public RESOURCE_CODE As String
    Public RESOURCE_Desc As String
    Public RESOURCE_Type As String
    Public QUANTITY As Decimal
    Public UNIT_COST_UOM As String
    Public UNIT_COST As Decimal
    Public TOTAL_COST As Decimal

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBOMResources), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBOMResources In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "BOM_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", obj.OPERATION_CODE)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                clsCommon.AddColumnsForChange(coll, "RESOURCE_CODE", obj.RESOURCE_CODE)
                clsCommon.AddColumnsForChange(coll, "QUANTITY", obj.QUANTITY)
                clsCommon.AddColumnsForChange(coll, "UNIT_COST_UOM", obj.UNIT_COST_UOM)
                clsCommon.AddColumnsForChange(coll, "UNIT_COST", obj.UNIT_COST)
                clsCommon.AddColumnsForChange(coll, "TOTAL_COST", obj.TOTAL_COST)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BOM_RESOURCES", OMInsertOrUpdate.Insert, "TSPL_MF_BOM_RESOURCES.BOM_CODE='" + strDocNo + "' and TSPL_MF_BOM_RESOURCES.OPERATION_CODE='" + obj.OPERATION_CODE + "' and  TSPL_MF_BOM_RESOURCES.WORK_CENTER_CODE='" + obj.WORK_CENTER_CODE + "' and  TSPL_MF_BOM_RESOURCES.RESOURCE_CODE='" + obj.RESOURCE_CODE + "'", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetBomResources(ByVal BOM_Code As String, ByVal Operation_Code As String, ByVal Work_Center_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBOMResources)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "select TSPL_MF_BOM_RESOURCES.BOM_CODE,TSPL_MF_BOM_RESOURCES.OPERATION_CODE,TSPL_MF_BOM_RESOURCES.WORK_CENTER_CODE,TSPL_MF_BOM_RESOURCES.RESOURCE_CODE,TSPL_MF_BOM_RESOURCES.QUANTITY," &
              " TSPL_MF_BOM_RESOURCES.UNIT_COST,TSPL_MF_BOM_RESOURCES.TOTAL_COST,TSPL_MF_RESOURCE_MASTER.DESCRIPTION AS RESOURCE_DESC,TSPL_MF_RESOURCE_MASTER.RESOURCE_TYPE,TSPL_MF_BOM_RESOURCES.UNIT_COST_UOM " &
              " from TSPL_MF_BOM_RESOURCES LEFT JOIN TSPL_MF_RESOURCE_MASTER ON TSPL_MF_BOM_RESOURCES.RESOURCE_CODE=TSPL_MF_RESOURCE_MASTER.RESOURCE_CODE " &
              " where TSPL_MF_BOM_RESOURCES.BOM_CODE='" & BOM_Code & "' and TSPL_MF_BOM_RESOURCES.Operation_Code='" & Operation_Code & "' and TSPL_MF_BOM_RESOURCES.Work_Center_Code='" & Work_Center_Code & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsBOMResources
        Dim ObjList As New List(Of clsBOMResources)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsBOMResources()

                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.OPERATION_CODE = clsCommon.myCstr(dr("OPERATION_CODE"))
                objtr.WORK_CENTER_CODE = clsCommon.myCstr(dr("WORK_CENTER_CODE"))
                objtr.RESOURCE_CODE = clsCommon.myCstr(dr("RESOURCE_CODE"))
                objtr.RESOURCE_Desc = clsCommon.myCstr(dr("RESOURCE_Desc"))
                objtr.RESOURCE_Type = clsCommon.myCstr(dr("RESOURCE_TYPE"))
                objtr.QUANTITY = clsCommon.myCdbl(dr("QUANTITY"))
                objtr.UNIT_COST = clsCommon.myCdbl(dr("UNIT_COST"))
                objtr.TOTAL_COST = clsCommon.myCdbl(dr("TOTAL_COST"))
                objtr.UNIT_COST_UOM = clsCommon.myCstr(dr("UNIT_COST_UOM"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

Public Class clsBOMToolTypes
#Region "Variables"
    Public BOM_CODE As String
    Public OPERATION_CODE As String
    Public WORK_CENTER_CODE As String
    Public TOOL_TYPE_CODE As String
    Public TOOL_TYPE_DESC As String
    Public QUANTITY As Decimal
    Public UNIT_COST As Decimal
    Public TOTAL_COST As Decimal
    Public UNIT_COST_UOM As String

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBOMToolTypes), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBOMToolTypes In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "BOM_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", obj.OPERATION_CODE)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                clsCommon.AddColumnsForChange(coll, "TOOL_TYPE_CODE", obj.TOOL_TYPE_CODE)
                clsCommon.AddColumnsForChange(coll, "QUANTITY", obj.QUANTITY)
                clsCommon.AddColumnsForChange(coll, "UNIT_COST", obj.UNIT_COST)
                clsCommon.AddColumnsForChange(coll, "TOTAL_COST", obj.TOTAL_COST)
                clsCommon.AddColumnsForChange(coll, "UNIT_COST_UOM", obj.UNIT_COST_UOM)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BOM_TOOLTYPES", OMInsertOrUpdate.Insert, "TSPL_MF_BOM_TOOLTYPES.BOM_CODE='" + strDocNo + "' and TSPL_MF_BOM_TOOLTYPES.OPERATION_CODE='" + obj.OPERATION_CODE + "' and  TSPL_MF_BOM_TOOLTYPES.WORK_CENTER_CODE='" + obj.WORK_CENTER_CODE + "' and  TSPL_MF_BOM_TOOLTYPES.TOOL_TYPE_CODE='" + obj.TOOL_TYPE_CODE + "'", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetBomToolTypes(ByVal BOM_Code As String, ByVal Operation_Code As String, ByVal Work_Center_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBOMToolTypes)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "select TSPL_MF_BOM_TOOLTYPES.BOM_CODE,TSPL_MF_BOM_TOOLTYPES.OPERATION_CODE,TSPL_MF_BOM_TOOLTYPES.WORK_CENTER_CODE,TSPL_MF_BOM_TOOLTYPES.TOOL_TYPE_CODE,TSPL_MF_BOM_TOOLTYPES.QUANTITY," &
              " TSPL_MF_BOM_TOOLTYPES.UNIT_COST,TSPL_MF_BOM_TOOLTYPES.TOTAL_COST,TSPL_MF_TOOL_TYPE.DESCRIPTION AS TOOL_TYPE_DESC,TSPL_MF_BOM_TOOLTYPES.UNIT_COST_UOM " &
              " from TSPL_MF_BOM_TOOLTYPES LEFT JOIN TSPL_MF_TOOL_TYPE ON TSPL_MF_BOM_TOOLTYPES.TOOL_TYPE_CODE=TSPL_MF_TOOL_TYPE.TOOL_TYPE_CODE " &
              " where TSPL_MF_BOM_TOOLTYPES.BOM_CODE='" & BOM_Code & "' and TSPL_MF_BOM_TOOLTYPES.Operation_Code='" & Operation_Code & "' and TSPL_MF_BOM_TOOLTYPES.Work_Center_Code='" & Work_Center_Code & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsBOMToolTypes
        Dim ObjList As New List(Of clsBOMToolTypes)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsBOMToolTypes()

                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.OPERATION_CODE = clsCommon.myCstr(dr("OPERATION_CODE"))
                objtr.WORK_CENTER_CODE = clsCommon.myCstr(dr("WORK_CENTER_CODE"))
                objtr.TOOL_TYPE_CODE = clsCommon.myCstr(dr("TOOL_TYPE_CODE"))
                objtr.TOOL_TYPE_DESC = clsCommon.myCstr(dr("TOOL_TYPE_DESC"))
                objtr.QUANTITY = clsCommon.myCdbl(dr("QUANTITY"))
                objtr.UNIT_COST = clsCommon.myCdbl(dr("UNIT_COST"))
                objtr.TOTAL_COST = clsCommon.myCdbl(dr("TOTAL_COST"))
                objtr.UNIT_COST_UOM = clsCommon.myCstr(dr("UNIT_COST_UOM"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

Public Class clsBOMOperations
#Region "Variables"
    Public BOM_CODE As String
    Public Line_No As Integer
    Public OPERATION_CODE As String
    Public WORK_CENTER_CODE As String
    Public SETUP_TIME_MINUTES As Decimal
    Public RUN_TIME_MINUTES As Decimal
    Public CLEAN_TIME_MINUTES As Decimal
    Public WAIT_TIME_MINUTES As Decimal
    Public OVERLAP_PER As Decimal
    Public REMARKS As String

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBOMOperations), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBOMOperations In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "BOM_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", obj.OPERATION_CODE)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                clsCommon.AddColumnsForChange(coll, "SETUP_TIME_MINUTES", obj.SETUP_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "RUN_TIME_MINUTES", obj.RUN_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "CLEAN_TIME_MINUTES", obj.CLEAN_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "WAIT_TIME_MINUTES", obj.WAIT_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "OVERLAP_PER", obj.OVERLAP_PER)
                clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)


                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BOM_OPERATIONS", OMInsertOrUpdate.Insert, "TSPL_MF_BOM_OPERATIONS.BOM_CODE='" + strDocNo + "' and TSPL_MF_BOM_OPERATIONS.OPERATION_CODE='" + obj.OPERATION_CODE + "' and  TSPL_MF_BOM_OPERATIONS.WORK_CENTER_CODE='" + obj.WORK_CENTER_CODE + "' ", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetBomOperations(ByVal BOM_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBOMOperations)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_MF_BOM_OPERATIONS where BOM_CODE='" & BOM_Code & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsBOMOperations
        Dim ObjList As New List(Of clsBOMOperations)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsBOMOperations()

                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                objtr.OPERATION_CODE = clsCommon.myCstr(dr("OPERATION_CODE"))
                objtr.WORK_CENTER_CODE = clsCommon.myCstr(dr("WORK_CENTER_CODE"))
                objtr.SETUP_TIME_MINUTES = clsCommon.myCdbl(dr("SETUP_TIME_MINUTES"))
                objtr.RUN_TIME_MINUTES = clsCommon.myCdbl(dr("RUN_TIME_MINUTES"))
                objtr.CLEAN_TIME_MINUTES = clsCommon.myCdbl(dr("CLEAN_TIME_MINUTES"))
                objtr.WAIT_TIME_MINUTES = clsCommon.myCdbl(dr("WAIT_TIME_MINUTES"))
                objtr.OVERLAP_PER = clsCommon.myCdbl(dr("OVERLAP_PER"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class
Public Class clsBOMCosting
#Region "Variables"
    Public BOM_CODE As String
    Public CALC_TYPE As String
    Public DIRECT_MATERIAL_COST As Decimal
    Public PACKAGING_MATERIAL_COST As Decimal
    Public SETUP_LABOR_COST As Decimal
    Public DIRECT_LABOR_COST As Decimal
    Public OVERHEAD_COST As Decimal
    Public SUBCONTRACT_COST As Decimal
    Public TOOL_COST As Decimal
    Public TOTAL_COST As Decimal

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBOMCosting), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBOMCosting In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "BOM_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "CALC_TYPE", obj.CALC_TYPE)
                clsCommon.AddColumnsForChange(coll, "DIRECT_MATERIAL_COST", obj.DIRECT_MATERIAL_COST)
                clsCommon.AddColumnsForChange(coll, "PACKAGING_MATERIAL_COST", obj.PACKAGING_MATERIAL_COST)
                clsCommon.AddColumnsForChange(coll, "SETUP_LABOR_COST", obj.SETUP_LABOR_COST)
                clsCommon.AddColumnsForChange(coll, "DIRECT_LABOR_COST", obj.DIRECT_LABOR_COST)
                clsCommon.AddColumnsForChange(coll, "OVERHEAD_COST", obj.OVERHEAD_COST)
                clsCommon.AddColumnsForChange(coll, "SUBCONTRACT_COST", obj.SUBCONTRACT_COST)
                clsCommon.AddColumnsForChange(coll, "TOOL_COST", obj.TOOL_COST)
                clsCommon.AddColumnsForChange(coll, "TOTAL_COST", obj.TOTAL_COST)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_BOM_COSTING", OMInsertOrUpdate.Insert, "TSPL_MF_BOM_COSTING.BOM_CODE='" + strDocNo + "' ", trans)

            Next

        End If

        Return True
    End Function
    Public Shared Function GetBomCosting(ByVal BOM_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBOMCosting)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_MF_BOM_COSTING " &
              " where TSPL_MF_BOM_COSTING.BOM_CODE='" & BOM_Code & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsBOMCosting
        Dim ObjList As New List(Of clsBOMCosting)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsBOMCosting()

                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.CALC_TYPE = clsCommon.myCstr(dr("CALC_TYPE"))
                objtr.DIRECT_MATERIAL_COST = clsCommon.myCstr(dr("DIRECT_MATERIAL_COST"))
                objtr.DIRECT_LABOR_COST = clsCommon.myCstr(dr("DIRECT_LABOR_COST"))
                objtr.OVERHEAD_COST = clsCommon.myCstr(dr("OVERHEAD_COST"))
                objtr.PACKAGING_MATERIAL_COST = clsCommon.myCstr(dr("PACKAGING_MATERIAL_COST"))
                objtr.SETUP_LABOR_COST = clsCommon.myCdbl(dr("SETUP_LABOR_COST"))
                objtr.SUBCONTRACT_COST = clsCommon.myCdbl(dr("SUBCONTRACT_COST"))
                objtr.TOOL_COST = clsCommon.myCdbl(dr("TOOL_COST"))
                objtr.TOTAL_COST = clsCommon.myCstr(dr("TOTAL_COST"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

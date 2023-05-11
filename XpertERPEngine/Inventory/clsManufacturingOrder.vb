Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
<Serializable()> _
Public Class clsManufacturingOrder
    Implements ICloneable

#Region "Variables"

    Public MO_CODE As String
    Public MO_STATUS As String
    Public ITEM_CODE As String
    Public ITEM_NAME As String
    Public QTY_ORDERED As String
    Public UNIT_CODE As String
    Public QTY_ORDERED_STOCK As Decimal
    Public UNIT_CODE_STOCK As String
    Public MO_REFERENCE As String
    Public DESCRIPTION As String

    Public BOM_CODE As String
    Public BOM_REVISION_NO As String
    Public MO_DATE As Date
    Public MO_DUE_DATE As Date
    Public PRODUCTION_AREA As String = ""
    Public PLANNER As String
    Public IN_CHARGE As String

    Public PLANNED_START_DATE As Date? = Nothing
    Public PLANNED_END_DATE As Date? = Nothing
    Public ACTUAL_START_DATE As Date? = Nothing
    Public ACTUAL_END_DATE As Date? = Nothing

    Public ATTACHED_DOC As Byte()
    Public ATTACHED_DOC_PATH As String

    Public CREATED_BY As String
    Public APPROVED_BY As String = ""
    Public RELEASED_BY As String = ""
    Public CLOSED_BY As String = ""

    Public POSTED As Boolean
    Public Posting_Date As Date

    Public Created_Date As Date
    Public APPROVED_DATE As Date? = Nothing
    Public RELEASED_DATE_DATE As Date? = Nothing
    Public CLOSED_DATE As Date? = Nothing
    Public COMMENTS As String
    Public LOCATION_CODE As String = Nothing
    Public LOCATION_Desc As String = Nothing
    Public SOURCE_DOC_TYPE As String = "Individual"
    Public PROD_PLAN_CODE As String = Nothing
    Public SO_CODE As String = Nothing
    Public PROD_PLAN_DESC As String = Nothing
    Public SO_DESC As String = Nothing
    Public PARENT_MO_CODE As String = ""

    Public ObjListMaterial As List(Of clsMOMaterial)
    Public ObjListOP As List(Of clsMOOperations)
    Public ObjListRes As List(Of clsMOResources)
    Public ObjListToolType As List(Of clsMOToolTypes)
    Public ObjListCosting As List(Of clsMOCosting)

#End Region
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim m As New MemoryStream()
        Dim f As New BinaryFormatter()
        f.Serialize(m, Me)
        m.Seek(0, SeekOrigin.Begin)
        Return f.Deserialize(m)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsManufacturingOrder
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
            qry = "delete from TSPL_MF_MO_MATERIAL where MO_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_MO_OPERATIONS
            qry = "DELETE FROM TSPL_MF_MO_OPERATIONS WHERE MO_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_MO_RESOURCES
            qry = "DELETE FROM TSPL_MF_MO_RESOURCES WHERE MO_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_MO_TOOLTYPES
            qry = "DELETE FROM TSPL_MF_MO_TOOLTYPES WHERE MO_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MF_MO_COSTING
            qry = "DELETE FROM TSPL_MF_MO_COSTING WHERE MO_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_MANUFACTURING_ORDER where MO_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsManufacturingOrder
        Dim obj As New clsManufacturingOrder()

        Dim qry As String = "SELECT T1.MO_CODE,T1.DESCRIPTION,T1.MO_REFERENCE,T1.MO_DATE,T1.MO_DUE_DATE,T1.BOM_CODE,T1.BOM_REVISION_NO,T1.PRODUCTION_AREA,T1.PLANNED_START_DATE,T1.PLANNED_END_DATE,T1.ACTUAL_START_DATE,T1.ACTUAL_END_DATE,T1.MO_STATUS,"
        qry += " T1.ATTACHED_DOC,T1.ATTACHED_DOC_PATH,T1.ITEM_CODE,T1.UNIT_CODE_STOCK,T2.ITEM_DESC AS ITEM_NAME,T1.QTY_ORDERED,T1.UNIT_CODE,"
        qry += " T1.QTY_ORDERED_STOCK,T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.RELEASED_BY,T1.CLOSED_BY,T1.POSTED,T1.Created_Date, " & _
        " T1.APPROVED_DATE,T1.RELEASED_DATE_DATE,T1.CLOSED_DATE,T1.POSTING_DATE,T1.PLANNER,T1.IN_CHARGE,T1.COMMENTS,T1.LOCATION_CODE,LOC.Location_Desc, " & _
        " T1.SOURCE_DOC_TYPE,T1.PROD_PLAN_CODE,T1.SO_CODE,PP.Description AS PROD_PLAN_DESC,SO.Description AS SO_DESC,T1.PARENT_MO_CODE " & _
        " FROM TSPL_MF_MANUFACTURING_ORDER  T1 INNER JOIN TSPL_ITEM_MASTER T2  ON T1.ITEM_CODE=T2.ITEM_CODE  " & _
        " LEFT JOIN TSPL_LOCATION_MASTER LOC ON T1.LOCATION_CODE=LOC.LOCATION_CODE " & _
        " LEFT JOIN TSPL_MF_PRODUCTION_PLAN_HEAD PP ON T1.PROD_PLAN_CODE=PP.PROD_PLAN_CODE " & _
        " LEFT JOIN TSPL_SD_SALES_ORDER_HEAD SO ON T1.SO_CODE=SO.Document_Code " & _
        " where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND MO_CODE = (select MIN(MO_CODE) from TSPL_MF_MANUFACTURING_ORDER)"
            Case NavigatorType.Last
                qry += " AND MO_CODE = (select Max(MO_CODE) from TSPL_MF_MANUFACTURING_ORDER)"
            Case NavigatorType.Next
                qry += " AND MO_CODE = (select Min(MO_CODE) from TSPL_MF_MANUFACTURING_ORDER where  MO_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND MO_CODE = (select Max(MO_CODE) from TSPL_MF_MANUFACTURING_ORDER where MO_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND MO_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.MO_CODE = dt.Rows(0)("MO_CODE")
            obj.MO_STATUS = dt.Rows(0)("MO_STATUS")
            obj.QTY_ORDERED = dt.Rows(0)("QTY_ORDERED")
            obj.QTY_ORDERED_STOCK = dt.Rows(0)("QTY_ORDERED_STOCK")
            obj.UNIT_CODE = dt.Rows(0)("UNIT_CODE")
            obj.UNIT_CODE_STOCK = dt.Rows(0)("UNIT_CODE_STOCK")
            obj.ITEM_CODE = clsCommon.myCstr(dt.Rows(0)("ITEM_CODE"))
            obj.ITEM_NAME = clsCommon.myCstr(dt.Rows(0)("ITEM_NAME"))
            obj.MO_REFERENCE = clsCommon.myCstr(dt.Rows(0)("MO_REFERENCE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))

            obj.BOM_CODE = clsCommon.myCstr(dt.Rows(0)("BOM_CODE"))
            obj.BOM_REVISION_NO = clsCommon.myCstr(dt.Rows(0)("BOM_REVISION_NO"))
            obj.PRODUCTION_AREA = clsCommon.myCstr(dt.Rows(0)("PRODUCTION_AREA"))
            obj.MO_DATE = clsCommon.GetPrintDate(dt.Rows(0)("MO_DATE"), "dd/MMM/yyyy")
            obj.MO_DUE_DATE = clsCommon.GetPrintDate(dt.Rows(0)("MO_DUE_DATE"), "dd/MMM/yyyy")

            obj.PLANNER = clsCommon.myCstr(dt.Rows(0)("PLANNER"))
            obj.IN_CHARGE = clsCommon.myCstr(dt.Rows(0)("IN_CHARGE"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.SOURCE_DOC_TYPE = clsCommon.myCstr(dt.Rows(0)("SOURCE_DOC_TYPE"))
            obj.PROD_PLAN_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_PLAN_CODE"))
            obj.SO_CODE = clsCommon.myCstr(dt.Rows(0)("SO_CODE"))

            obj.PROD_PLAN_DESC = clsCommon.myCstr(dt.Rows(0)("PROD_PLAN_DESC"))
            obj.SO_DESC = clsCommon.myCstr(dt.Rows(0)("SO_DESC"))
            obj.PARENT_MO_CODE = clsCommon.myCstr(dt.Rows(0)("PARENT_MO_CODE"))

            '' PLAN START DATE
            If IsDBNull(dt.Rows(0)("PLANNED_START_DATE")) = True Then
                obj.PLANNED_START_DATE = Nothing
            Else
                obj.PLANNED_START_DATE = clsCommon.GetPrintDate(dt.Rows(0)("PLANNED_START_DATE"), "dd/MMM/yyyy")
            End If

            '' PLAN END DATE
            If IsDBNull(dt.Rows(0)("PLANNED_END_DATE")) = True Then
                obj.PLANNED_END_DATE = Nothing
            Else
                obj.PLANNED_END_DATE = clsCommon.GetPrintDate(dt.Rows(0)("PLANNED_END_DATE"), "dd/MMM/yyyy")
            End If

            '' ACTUAL START DATE
            If IsDBNull(dt.Rows(0)("ACTUAL_START_DATE")) = True Then
                obj.ACTUAL_START_DATE = Nothing
            Else
                obj.ACTUAL_START_DATE = clsCommon.GetPrintDate(dt.Rows(0)("ACTUAL_START_DATE"), "dd/MMM/yyyy")
            End If

            '' ACTUAL END DATE
            If IsDBNull(dt.Rows(0)("ACTUAL_END_DATE")) = True Then
                obj.ACTUAL_END_DATE = Nothing
            Else
                obj.ACTUAL_END_DATE = clsCommon.GetPrintDate(dt.Rows(0)("ACTUAL_END_DATE"), "dd/MMM/yyyy")
            End If

            If IsDBNull(dt.Rows(0)("ATTACHED_DOC")) = True Then
                obj.ATTACHED_DOC = Nothing
            Else
                obj.ATTACHED_DOC = CType(dt.Rows(0)("ATTACHED_DOC"), Byte())
            End If

            obj.ATTACHED_DOC_PATH = clsCommon.myCstr(dt.Rows(0)("ATTACHED_DOC_PATH"))


            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.APPROVED_BY = clsCommon.myCstr(dt.Rows(0)("APPROVED_BY"))
            obj.RELEASED_BY = clsCommon.myCstr(dt.Rows(0)("RELEASED_BY"))
            obj.CLOSED_BY = clsCommon.myCstr(dt.Rows(0)("CLOSED_BY"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))

            strCode = dt.Rows(0)("MO_CODE")

            '' POSTING DATE
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If

            '' CREATED DATE
            If clsCommon.myLen(dt.Rows(0)("Created_Date")) > 0 Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            Else
                obj.Created_Date = Nothing
            End If

            '' APPROVED DATE
            If clsCommon.myLen(dt.Rows(0)("APPROVED_DATE")) > 0 Then
                obj.APPROVED_DATE = clsCommon.myCDate(dt.Rows(0)("APPROVED_DATE"))
            Else
                obj.APPROVED_DATE = Nothing
            End If

            '' RELEASE DATE
            If clsCommon.myLen(dt.Rows(0)("RELEASED_DATE_DATE")) > 0 Then
                obj.RELEASED_DATE_DATE = clsCommon.myCDate(dt.Rows(0)("RELEASED_DATE_DATE"))
            Else
                obj.RELEASED_DATE_DATE = Nothing
            End If

            '' CLOSE DATE
            If clsCommon.myLen(dt.Rows(0)("CLOSED_DATE")) > 0 Then
                obj.CLOSED_DATE = clsCommon.myCDate(dt.Rows(0)("CLOSED_DATE"))
            Else
                obj.CLOSED_DATE = Nothing
            End If

            '' LOCATION CODE AND DESC (NEW) BY PANCH RAJ
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_Desc = clsCommon.myCstr(dt.Rows(0)("LOCATION_Desc"))

        End If

        obj.ObjListMaterial = clsMOMaterial.GetMOMaterial(strCode, trans)
        obj.ObjListOP = clsMOOperations.GetMOOperations(strCode, trans)
        obj.ObjListCosting = clsMOCosting.GetMOCosting(strCode, trans)

        Return obj
    End Function
    Public Shared Function SaveData(ByVal obj As clsManufacturingOrder, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim isSaved As Boolean = True
            isSaved = obj.SaveData(obj, isNewEntry, trans, strCode)
            If isSaved = True Then
                trans.Commit()
            End If
            Return True
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
            Return False
        End Try

    End Function

    Public Function SaveData(ByVal obj As clsManufacturingOrder, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        If isNewEntry Then
            If strCode = "" Then
                obj.MO_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MO, "", "")
            Else
                obj.MO_CODE = strCode
            End If
        End If
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Standard Production", "Manufacturing Order", obj.LOCATION_CODE, obj.PLANNED_START_DATE, trans)


        '' delete TSPL_MF_MO_MATERIAL
        Dim qry As String = "DELETE FROM TSPL_MF_MO_MATERIAL WHERE MO_CODE='" + obj.MO_CODE + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        '' delete TSPL_MF_MO_OPERATIONS
        qry = "DELETE FROM TSPL_MF_MO_OPERATIONS WHERE MO_CODE='" + obj.MO_CODE + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        '' delete TSPL_MF_MO_RESOURCES
        qry = "DELETE FROM TSPL_MF_MO_RESOURCES WHERE MO_CODE='" + obj.MO_CODE + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        '' delete TSPL_MF_MO_TOOLTYPES
        qry = "DELETE FROM TSPL_MF_MO_TOOLTYPES WHERE MO_CODE='" + obj.MO_CODE + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        '' delete TSPL_MF_MO_COSTING
        qry = "DELETE FROM TSPL_MF_MO_COSTING WHERE MO_CODE='" + obj.MO_CODE + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim strDocNo As String = ""

        If (clsCommon.myLen(obj.MO_CODE) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()

        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "MO_CODE", obj.MO_CODE)
        clsCommon.AddColumnsForChange(coll, "MO_STATUS", obj.MO_STATUS)
        clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
        clsCommon.AddColumnsForChange(coll, "QTY_ORDERED", obj.QTY_ORDERED)
        clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
        clsCommon.AddColumnsForChange(coll, "QTY_ORDERED_STOCK", obj.QTY_ORDERED_STOCK)
        clsCommon.AddColumnsForChange(coll, "UNIT_CODE_STOCK", obj.UNIT_CODE_STOCK)
        clsCommon.AddColumnsForChange(coll, "MO_REFERENCE", obj.MO_REFERENCE)
        clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)

        clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
        clsCommon.AddColumnsForChange(coll, "BOM_REVISION_NO", obj.BOM_REVISION_NO)
        clsCommon.AddColumnsForChange(coll, "MO_DATE", clsCommon.GetPrintDate(obj.MO_DATE, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "MO_DUE_DATE", clsCommon.GetPrintDate(obj.MO_DUE_DATE, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "PRODUCTION_AREA", obj.PRODUCTION_AREA)
        clsCommon.AddColumnsForChange(coll, "PLANNER", obj.PLANNER, True)
        clsCommon.AddColumnsForChange(coll, "IN_CHARGE", obj.IN_CHARGE, True)

        clsCommon.AddColumnsForChange(coll, "SOURCE_DOC_TYPE", obj.SOURCE_DOC_TYPE)
        clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", obj.PROD_PLAN_CODE, True)
        clsCommon.AddColumnsForChange(coll, "SO_CODE", obj.SO_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PARENT_MO_CODE", obj.PARENT_MO_CODE, True)

        If obj.PLANNED_START_DATE IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "PLANNED_START_DATE", clsCommon.GetPrintDate(obj.PLANNED_START_DATE, "dd/MMM/yyyy"))
        End If
        If obj.PLANNED_END_DATE IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "PLANNED_END_DATE", clsCommon.GetPrintDate(obj.PLANNED_END_DATE, "dd/MMM/yyyy"))
        End If
        If obj.ACTUAL_START_DATE IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "ACTUAL_START_DATE", clsCommon.GetPrintDate(obj.ACTUAL_START_DATE, "dd/MMM/yyyy"))
        End If
        If obj.ACTUAL_END_DATE IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "ACTUAL_END_DATE", clsCommon.GetPrintDate(obj.ACTUAL_END_DATE, "dd/MMM/yyyy"))
        End If

        clsCommon.AddColumnsForChange(coll, "ATTACHED_DOC_PATH", clsCommon.myCstr(obj.ATTACHED_DOC_PATH))

        clsCommon.AddColumnsForChange(coll, "APPROVED_BY", clsCommon.myCstr(obj.APPROVED_BY), True)
        clsCommon.AddColumnsForChange(coll, "RELEASED_BY", clsCommon.myCstr(obj.RELEASED_BY), True)
        clsCommon.AddColumnsForChange(coll, "CLOSED_BY", clsCommon.myCstr(obj.CLOSED_BY), True)

        If obj.APPROVED_DATE IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "APPROVED_DATE", clsCommon.GetPrintDate(obj.APPROVED_DATE, "dd/MMM/yyyy"))
        End If
        If obj.RELEASED_DATE_DATE IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "RELEASED_DATE_DATE", clsCommon.GetPrintDate(obj.RELEASED_DATE_DATE, "dd/MMM/yyyy"))
        End If
        If obj.CLOSED_DATE IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "CLOSED_DATE", clsCommon.GetPrintDate(obj.CLOSED_DATE, "dd/MMM/yyyy"))
        End If
        clsCommon.AddColumnsForChange(coll, "COMMENTS", clsCommon.myCstr(obj.COMMENTS))

        clsCommon.AddColumnsForChange(coll, "POSTED", "0")
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

        clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", clsCommon.myCstr(obj.LOCATION_CODE), True)
        If isNewEntry Then

            'clsCommon.AddColumnsForChange(coll, "MO_CODE", obj.MO_CODE)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            Dim Strqry As String = "SELECT Count(*) FROM TSPL_MF_MANUFACTURING_ORDER where MO_CODE = '" & obj.MO_CODE & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
            If check = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_MANUFACTURING_ORDER", OMInsertOrUpdate.Insert, "", trans)
            Else
                Throw New Exception("This Code:" + obj.MO_CODE + " Is Already Exist")
                Exit Function
            End If
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_MANUFACTURING_ORDER", OMInsertOrUpdate.Update, "TSPL_MF_MANUFACTURING_ORDER.MO_CODE='" + obj.MO_CODE + "'", trans)
        End If
        '' saving MO Material
        isSaved = isSaved AndAlso clsMOMaterial.SaveData(obj.MO_CODE, obj.ObjListMaterial, trans)
        '' saving MO operations 
        If Not obj.ObjListOP Is Nothing Then
            isSaved = isSaved AndAlso clsMOOperations.SaveData(obj.MO_CODE, obj.ObjListOP, trans)
        End If

        '' saving MO resources 
        If Not obj.ObjListRes Is Nothing Then
            isSaved = isSaved AndAlso clsMOResources.SaveData(obj.MO_CODE, obj.ObjListRes, trans)
        End If

        '' saving MO resources 
        If Not obj.ObjListToolType Is Nothing Then
            isSaved = isSaved AndAlso clsMOToolTypes.SaveData(obj.MO_CODE, obj.ObjListToolType, trans)
        End If

        '' saving MO COSTING 
        If Not obj.ObjListCosting Is Nothing Then
            isSaved = isSaved AndAlso clsMOCosting.SaveData(obj.MO_CODE, obj.ObjListCosting, trans)
        End If
        '' SAVE CHILD MO
        If isNewEntry Then
            For Each objMat As clsMOMaterial In obj.ObjListMaterial
                If clsCommon.CompairString(objMat.ITEM_TYPE, "S") = CompairStringResult.Equal Then
                    Dim dt As DataTable = GetItemBOM(objMat.CONSM_ITEM_CODE, trans)
                    If dt.Rows.Count > 0 Then
                        Dim objSFGMO As clsManufacturingOrder = obj.Clone

                        objSFGMO.BOM_CODE = clsCommon.myCstr(dt.Rows(0).Item("BOM_CODE"))
                        objSFGMO.BOM_REVISION_NO = clsCommon.myCstr(dt.Rows(0).Item("REVISION_NO"))
                        objSFGMO.ITEM_CODE = objMat.CONSM_ITEM_CODE
                        objSFGMO.ITEM_NAME = objMat.ITEM_DESCRIPTION
                        objSFGMO.QTY_ORDERED = objMat.CONSM_QUANTITY
                        objSFGMO.QTY_ORDERED_STOCK = objMat.CONSM_QUANTITY
                        objSFGMO.UNIT_CODE = objMat.CONSM_ITEM_UNIT_CODE
                        objSFGMO.UNIT_CODE_STOCK = objMat.CONSM_ITEM_UNIT_CODE
                        objSFGMO.PARENT_MO_CODE = obj.MO_CODE
                        Dim objSfgMOMatList As New List(Of clsMOMaterial)
                        Dim objSfgMatTr As clsMOMaterial
                        Dim objSFGBOM As clsBillOfMaterial = clsBillOfMaterial.GetData(objSFGMO.BOM_CODE, NavigatorType.Current, trans)
                        For Each objsfgMat As clsBillOfMaterial In clsBillOfMaterial.ObjList
                            objSfgMatTr = New clsMOMaterial
                            objSfgMatTr.BOM_QUANTITY = objsfgMat.CONSM_QUANTITY

                            objSfgMatTr.CONSM_ITEM_CATEGORY_CODE = objsfgMat.CONSM_ITEM_CATEGORY_CODE
                            objSfgMatTr.CONSM_ITEM_CODE = objsfgMat.CONSM_ITEM_CODE
                            objSfgMatTr.CONSM_ITEM_UNIT_CODE = objsfgMat.CONSM_ITEM_UNIT_CODE
                            objSfgMatTr.CONSM_QUANTITY = (objsfgMat.CONSM_QUANTITY * obj.QTY_ORDERED_STOCK) / clsBillOfMaterial.GetData(obj.BOM_CODE, NavigatorType.Current, trans).PROD_QUANTITY
                            objSfgMatTr.ITEM_DESCRIPTION = objsfgMat.ITEM_DESCRIPTION
                            objSfgMatTr.ITEM_TYPE = objsfgMat.ITEM_TYPE
                            objSfgMatTr.Line_No = objsfgMat.Line_No
                            objSfgMatTr.REMARKS = objsfgMat.REMARKS
                            objSfgMatTr.SCRAP_PERCENT = objsfgMat.SCRAP_PERCENT
                            objSfgMatTr.STOCK_QUANTITY = clsItemLocationDetails.getBalance(objsfgMat.CONSM_ITEM_CODE, obj.LOCATION_CODE, objSFGMO.MO_CODE, obj.MO_DATE, trans, objsfgMat.CONSM_ITEM_UNIT_CODE, 0)
                            objSfgMatTr.WASTAGE_PERCENT = objsfgMat.WASTAGE_PERCENT
                            objSfgMOMatList.Add(objSfgMatTr)
                        Next
                        objSFGMO.ObjListMaterial = objSfgMOMatList

                        '' operation assignment from bom
                        Dim objSfgMOOprList As New List(Of clsMOOperations)
                        Dim objSfgOprTr As clsMOOperations

                        If Not objSFGBOM.ObjListOP Is Nothing AndAlso objSFGBOM.ObjListOP.Count > 0 Then
                            For Each objsfgOP As clsBOMOperations In objSFGBOM.ObjListOP
                                objSfgOprTr = New clsMOOperations
                                objSfgOprTr.ACTUAL_CLEAN_TIME_MINUTES = objsfgOP.CLEAN_TIME_MINUTES
                                objSfgOprTr.ACTUAL_RUN_TIME_MINUTES = objsfgOP.RUN_TIME_MINUTES
                                objSfgOprTr.ACTUAL_SETUP_TIME_MINUTES = objsfgOP.SETUP_TIME_MINUTES
                                objSfgOprTr.ACTUAL_WAIT_TIME_MINUTES = objsfgOP.WAIT_TIME_MINUTES
                                objSfgOprTr.Line_No = objsfgOP.Line_No
                                objSfgOprTr.OPERATION_CODE = objsfgOP.OPERATION_CODE
                                objSfgOprTr.PROJECTED_CLEAN_TIME_MINUTES = objsfgOP.CLEAN_TIME_MINUTES
                                objSfgOprTr.PROJECTED_RUN_TIME_MINUTES = objsfgOP.RUN_TIME_MINUTES
                                objSfgOprTr.PROJECTED_SETUP_TIME_MINUTES = objsfgOP.SETUP_TIME_MINUTES
                                objSfgOprTr.PROJECTED_WAIT_TIME_MINUTES = objsfgOP.WAIT_TIME_MINUTES
                                objSfgOprTr.WORK_CENTER_CODE = objsfgOP.WORK_CENTER_CODE

                                objSfgMOOprList.Add(objSfgOprTr)
                            Next
                            objSFGMO.ObjListOP = objSfgMOOprList
                        Else
                            objSFGMO.ObjListOP = Nothing
                        End If


                        '' Resource assignment from bom
                        Dim objSfgMOResiList As New List(Of clsMOResources)
                        Dim objSfgResTr As clsMOResources

                        If Not objSFGBOM.ObjListRes Is Nothing AndAlso objSFGBOM.ObjListRes.Count > 0 Then
                            For Each objsfgRes As clsBOMResources In objSFGBOM.ObjListRes
                                objSfgResTr = New clsMOResources
                                objSfgResTr.ACTUAL_COST = objsfgRes.TOTAL_COST
                                objSfgResTr.COST_VARIENCE_PER = 0
                                objSfgResTr.OPERATION_CODE = objsfgRes.OPERATION_CODE
                                objSfgResTr.QTY_VARIENCE_PER = 0
                                objSfgResTr.REQUIRED_QUANTITY = objsfgRes.QUANTITY
                                objSfgResTr.RESOURCE_CODE = objsfgRes.RESOURCE_CODE
                                objSfgResTr.RESOURCE_Desc = objsfgRes.RESOURCE_Desc
                                objSfgResTr.RESOURCE_Type = objsfgRes.RESOURCE_Type
                                objSfgResTr.STD_COST = objsfgRes.TOTAL_COST
                                objSfgResTr.UNIT_COST_UOM = objsfgRes.UNIT_COST_UOM
                                objSfgResTr.UNIT_COST = objsfgRes.UNIT_COST
                                objSfgResTr.USED_QUANTITY = objsfgRes.QUANTITY
                                objSfgResTr.WORK_CENTER_CODE = objsfgRes.WORK_CENTER_CODE

                                objSfgMOResiList.Add(objSfgResTr)
                            Next
                            objSFGMO.ObjListRes = objSfgMOResiList
                        Else
                            objSFGMO.ObjListRes = Nothing
                        End If


                        '' tool type assignment from bom
                        Dim objSfgMOToolList As New List(Of clsMOToolTypes)
                        Dim objSfgToolTr As clsMOToolTypes

                        If Not objSFGBOM.ObjListToolType Is Nothing AndAlso objSFGBOM.ObjListToolType.Count > 0 Then
                            For Each objsfgTool As clsBOMToolTypes In objSFGBOM.ObjListToolType
                                objSfgToolTr = New clsMOToolTypes
                                objSfgToolTr.ACTUAL_COST = objsfgTool.TOTAL_COST
                                objSfgToolTr.COST_VARIENCE_PER = 0
                                objSfgToolTr.OPERATION_CODE = objsfgTool.OPERATION_CODE
                                objSfgToolTr.QTY_VARIENCE_PER = 0
                                objSfgToolTr.REQUIRED_QUANTITY = objsfgTool.QUANTITY
                                objSfgToolTr.STD_COST = objsfgTool.TOTAL_COST
                                objSfgToolTr.TOOL_TYPE_CODE = objsfgTool.TOOL_TYPE_CODE
                                objSfgToolTr.TOOL_TYPE_DESC = objsfgTool.TOOL_TYPE_DESC
                                objSfgToolTr.UNIT_COST = objsfgTool.UNIT_COST
                                objSfgToolTr.UNIT_COST_UOM = objsfgTool.UNIT_COST_UOM
                                objSfgToolTr.USED_QUANTITY = objsfgTool.QUANTITY
                                objSfgToolTr.WORK_CENTER_CODE = objsfgTool.WORK_CENTER_CODE

                                objSfgMOToolList.Add(objSfgToolTr)
                            Next
                            objSFGMO.ObjListToolType = objSfgMOToolList
                        Else

                        End If


                        '' cost assignment from bom
                        Dim objSfgMOCostList As New List(Of clsMOCosting)
                        Dim objSfgCostTr As clsMOCosting

                        If Not objSFGBOM.ObjListCosting Is Nothing AndAlso objSFGBOM.ObjListCosting.Count > 0 Then
                            For Each objsfgCost As clsBOMCosting In objSFGBOM.ObjListCosting
                                objSfgCostTr = New clsMOCosting
                                objSfgCostTr.BOM_CODE = objsfgCost.BOM_CODE
                                objSfgCostTr.CALC_TYPE = objsfgCost.CALC_TYPE
                                objSfgCostTr.DIRECT_LABOR_COST = objsfgCost.DIRECT_LABOR_COST
                                objSfgCostTr.DIRECT_MATERIAL_COST = objsfgCost.DIRECT_MATERIAL_COST
                                objSfgCostTr.OVERHEAD_COST = objsfgCost.OVERHEAD_COST
                                objSfgCostTr.PACKAGING_MATERIAL_COST = objsfgCost.PACKAGING_MATERIAL_COST
                                objSfgCostTr.SETUP_LABOR_COST = objsfgCost.SETUP_LABOR_COST
                                objSfgCostTr.SUBCONTRACT_COST = objsfgCost.SUBCONTRACT_COST
                                objSfgCostTr.TOOL_COST = objsfgCost.TOOL_COST
                                objSfgCostTr.TOTAL_COST = objsfgCost.TOTAL_COST

                                objSfgMOCostList.Add(objSfgCostTr)
                            Next
                            objSFGMO.ObjListCosting = objSfgMOCostList
                        Else
                            objSFGMO.ObjListCosting = Nothing
                        End If


                        '' save child mo
                        objSFGMO.SaveData(objSFGMO, isNewEntry, trans, "")
                    End If
                End If
            Next
        End If


        Return isSaved
    End Function
    Public Shared Function GetItemBOM(ByVal Item_Code As String, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = "select BOM_CODE,MAX(REVISION_NO) as Revision_No from TSPL_MF_BOM_HEAD where POSTED=1 " & _
        " and PROD_ITEM_CODE='" & Item_Code & "' group by BOM_CODE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsManufacturingOrder = clsManufacturingOrder.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.MO_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MF_MANUFACTURING_ORDER set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where MO_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetMOStatusTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "Open"
        DR("Code") = "Open"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Approved"
        DR("Code") = "Approved"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "On Hold"
        DR("Code") = "On Hold"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Released"
        DR("Code") = "Released"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Closed"
        DR("Code") = "Closed"
        DT.Rows.Add(DR)

        DT.AcceptChanges()

        Return DT
    End Function
    Public Shared Function GetMOSourceDocTypeTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "Production Plan"
        DR("Code") = "PP"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Sales Order"
        DR("Code") = "SO"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Individual"
        DR("Code") = "Individual"
        DT.Rows.Add(DR)

        DT.AcceptChanges()

        Return DT
    End Function
    Public Shared Function FinderForFinishedGoods(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsItemMaster
        Dim obj As clsItemMaster = Nothing
        Dim qry As String = "SELECT TSPL_ITEM_MASTER.Item_Code AS Code, TSPL_ITEM_MASTER.Item_Desc AS Name, TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_MASTER.ITF_CODE as [ITF Code] "
        qry += " FROM TSPL_ITEM_MASTER  "

        Dim WhrCls As String = "(Item_Type in ('F','S')) and TSPL_ITEM_MASTER.Active=1 "
        strCode = clsCommon.ShowSelectForm("ItemFinderFinishedGoods", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            qry = "select Item_Code,Item_Desc,Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsItemMaster()
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function FinderForSO(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String

        Dim qry As String = " select Document_Code as Code,Document_Date as [Date],SalesOrder_Type as [Type],Customer_Code as [Customer Code]," & _
                            " Description ,Remarks,Bill_To_Location as [Location],Ship_To_Location as [Shipping Location]," & _
                            " Total_Amt as [Total Amount],Discount_Amt as [Discount],Amount_Less_Discount as [Net Amount],Total_Tax_Amt as [Tax Amount] " & _
                            " from TSPL_SD_SALES_ORDER_HEAD"

        Dim WhrCls As String = "TSPL_SD_SALES_ORDER_HEAD.Status=1 "
        strCode = clsCommon.ShowSelectForm("SO", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
        Return strCode
    End Function
    Public Shared Function GetSODescription(ByVal strCode As String) As String
        Dim qry As String = " select Description from TSPL_SD_SALES_ORDER_HEAD where Document_Code='" & strCode & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function GetFinerForSOItem(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As DataTable
        Dim qry As String = " select TSPL_SD_SALES_ORDER_DETAIL.ITEM_CODE as Code, " & _
                            " TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALES_ORDER_DETAIL.Qty, TSPL_SD_SALES_ORDER_DETAIL.UNIT_CODE " & _
                            " from TSPL_SD_SALES_ORDER_HEAD " & _
                            " inner join TSPL_SD_SALES_ORDER_DETAIL on  TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code " & _
                            " left join TSPL_ITEM_MASTER on TSPL_SD_SALES_ORDER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code "

        Dim str As String = ""
        Dim dt As New DataTable
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_SD_SALES_ORDER_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_SD_SALES_ORDER_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("PP", qry, "Code", whrCls, currCode, "Code", isButtonClicked)
        If clsCommon.myLen(str) > 0 Then
            qry = qry & " where " & whrCls & " and TSPL_SD_SALES_ORDER_DETAIL.ITEM_CODE='" & str & "'"
            dt = clsDBFuncationality.GetDataTable(qry)
        End If
        Return dt
    End Function
End Class

<Serializable()> _
Public Class clsMOMaterial
#Region "Variables"
    '' raw material grid columns
    Public Line_No As Integer
    Public CONSM_ITEM_CATEGORY_CODE As String
    Public CONSM_ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public ITEM_TYPE As String
    Public CONSM_QUANTITY As Decimal
    Public BOM_QUANTITY As Decimal
    Public STOCK_QUANTITY As Decimal
    Public CONSM_ITEM_UNIT_CODE As String
    Public SCRAP_PERCENT As Decimal
    Public WASTAGE_PERCENT As Decimal
    Public REMARKS As String
    Public SubItemType As String
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMOMaterial), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMOMaterial In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "MO_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CATEGORY_CODE", obj.CONSM_ITEM_CATEGORY_CODE, True)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_CODE", obj.CONSM_ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)

                clsCommon.AddColumnsForChange(coll, "CONSM_QUANTITY", obj.CONSM_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "BOM_QUANTITY", obj.BOM_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "CONSM_ITEM_UNIT_CODE", obj.CONSM_ITEM_UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "SCRAP_PERCENT", obj.SCRAP_PERCENT)
                clsCommon.AddColumnsForChange(coll, "WASTAGE_PERCENT", obj.WASTAGE_PERCENT)
                clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                clsCommon.AddColumnsForChange(coll, "STOCK_QUANTITY", obj.STOCK_QUANTITY)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_MO_MATERIAL", OMInsertOrUpdate.Insert, "TSPL_MF_MO_MATERIAL.MO_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetMOMaterial(ByVal MO_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOMaterial)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT T1.MO_CODE,T1.LINE_NO,T1.CONSM_ITEM_CATEGORY_CODE,T1.CONSM_ITEM_CODE,T1.ITEM_DESCRIPTION, "
        qry += " T1.CONSM_QUANTITY,T1.BOM_QUANTITY,T1.CONSM_ITEM_UNIT_CODE,T1.SCRAP_PERCENT,T1.WASTAGE_PERCENT, " & _
        " T1.REMARKS,itm.SubItemType,T1.STOCK_QUANTITY,itm.ITEM_TYPE FROM TSPL_MF_MO_MATERIAL T1 " & _
        " left join TSPL_ITEM_MASTER itm on t1.CONSM_ITEM_CODE=itm.item_code "
        qry += " WHERE 2=2 AND T1.MO_CODE = '" + MO_CODE + "' ORDER BY T1.LINE_NO"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim ObjList As New List(Of clsMOMaterial)
        Dim objtr As clsMOMaterial

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMOMaterial()

                objtr.Line_No = clsCommon.myCdbl(dr("LINE_NO"))
                objtr.CONSM_ITEM_CATEGORY_CODE = clsCommon.myCstr(dr("CONSM_ITEM_CATEGORY_CODE"))
                objtr.CONSM_ITEM_CODE = clsCommon.myCstr(dr("CONSM_ITEM_CODE"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.CONSM_QUANTITY = clsCommon.myCdbl(dr("CONSM_QUANTITY"))
                objtr.BOM_QUANTITY = clsCommon.myCdbl(dr("BOM_QUANTITY"))
                objtr.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(dr("CONSM_ITEM_UNIT_CODE"))
                objtr.SCRAP_PERCENT = clsCommon.myCdbl(dr("SCRAP_PERCENT"))
                objtr.WASTAGE_PERCENT = clsCommon.myCdbl(dr("WASTAGE_PERCENT"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))
                objtr.SubItemType = clsCommon.myCstr(dr("SubItemType"))
                objtr.STOCK_QUANTITY = clsCommon.myCdbl(dr("STOCK_QUANTITY"))
                objtr.ITEM_TYPE = clsCommon.myCstr(dr("ITEM_TYPE"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

<Serializable()> _
Public Class clsMOResources
#Region "Variables"
    Public MO_CODE As String
    Public OPERATION_CODE As String
    Public WORK_CENTER_CODE As String
    Public RESOURCE_CODE As String
    Public RESOURCE_Desc As String
    Public RESOURCE_Type As String
    Public REQUIRED_QUANTITY As Decimal
    Public USED_QUANTITY As Decimal
    Public STD_COST As Decimal
    Public ACTUAL_COST As Decimal
    Public QTY_VARIENCE_PER As Decimal
    Public COST_VARIENCE_PER As Decimal
    Public UNIT_COST_UOM As String
    Public UNIT_COST As Decimal
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMOResources), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMOResources In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "MO_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", obj.OPERATION_CODE)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                clsCommon.AddColumnsForChange(coll, "RESOURCE_CODE", obj.RESOURCE_CODE)
                clsCommon.AddColumnsForChange(coll, "REQUIRED_QUANTITY", obj.REQUIRED_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "USED_QUANTITY", obj.USED_QUANTITY)

                clsCommon.AddColumnsForChange(coll, "STD_COST", obj.STD_COST)
                clsCommon.AddColumnsForChange(coll, "ACTUAL_COST", obj.ACTUAL_COST)
                clsCommon.AddColumnsForChange(coll, "QTY_VARIENCE_PER", obj.QTY_VARIENCE_PER)
                clsCommon.AddColumnsForChange(coll, "COST_VARIENCE_PER", obj.COST_VARIENCE_PER)

                clsCommon.AddColumnsForChange(coll, "UNIT_COST_UOM", obj.UNIT_COST_UOM)
                clsCommon.AddColumnsForChange(coll, "UNIT_COST", obj.UNIT_COST)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_MO_RESOURCES", OMInsertOrUpdate.Insert, "TSPL_MF_MO_RESOURCES.MO_CODE='" + strDocNo + "' and TSPL_MF_MO_RESOURCES.OPERATION_CODE='" + obj.OPERATION_CODE + "' and  TSPL_MF_MO_RESOURCES.WORK_CENTER_CODE='" + obj.WORK_CENTER_CODE + "' and  TSPL_MF_MO_RESOURCES.RESOURCE_CODE='" + obj.RESOURCE_CODE + "'", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetMOResources(ByVal MO_CODE As String, ByVal Operation_Code As String, ByVal Work_Center_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOResources)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "select TSPL_MF_MO_RESOURCES.MO_CODE,TSPL_MF_MO_RESOURCES.OPERATION_CODE,TSPL_MF_MO_RESOURCES.WORK_CENTER_CODE,TSPL_MF_MO_RESOURCES.RESOURCE_CODE," & _
              " TSPL_MF_MO_RESOURCES.UNIT_COST,TSPL_MF_MO_RESOURCES.REQUIRED_QUANTITY,TSPL_MF_MO_RESOURCES.USED_QUANTITY,TSPL_MF_MO_RESOURCES.STD_COST,TSPL_MF_RESOURCE_MASTER.DESCRIPTION AS RESOURCE_DESC,TSPL_MF_RESOURCE_MASTER.RESOURCE_TYPE,TSPL_MF_MO_RESOURCES.UNIT_COST_UOM " & _
              " ,TSPL_MF_MO_RESOURCES.ACTUAL_COST,TSPL_MF_MO_RESOURCES.QTY_VARIENCE_PER,TSPL_MF_MO_RESOURCES.COST_VARIENCE_PER from TSPL_MF_MO_RESOURCES LEFT JOIN TSPL_MF_RESOURCE_MASTER ON TSPL_MF_MO_RESOURCES.RESOURCE_CODE=TSPL_MF_RESOURCE_MASTER.RESOURCE_CODE " & _
              " where TSPL_MF_MO_RESOURCES.MO_CODE='" & MO_CODE & "' and TSPL_MF_MO_RESOURCES.Operation_Code='" & Operation_Code & "' and TSPL_MF_MO_RESOURCES.Work_Center_Code='" & Work_Center_Code & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsMOResources
        Dim ObjList As New List(Of clsMOResources)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMOResources()

                objtr.MO_CODE = clsCommon.myCstr(dr("MO_CODE"))
                objtr.OPERATION_CODE = clsCommon.myCstr(dr("OPERATION_CODE"))
                objtr.WORK_CENTER_CODE = clsCommon.myCstr(dr("WORK_CENTER_CODE"))
                objtr.RESOURCE_CODE = clsCommon.myCstr(dr("RESOURCE_CODE"))
                objtr.RESOURCE_Desc = clsCommon.myCstr(dr("RESOURCE_Desc"))
                objtr.RESOURCE_Type = clsCommon.myCstr(dr("RESOURCE_TYPE"))
                objtr.REQUIRED_QUANTITY = clsCommon.myCdbl(dr("REQUIRED_QUANTITY"))
                objtr.USED_QUANTITY = clsCommon.myCdbl(dr("USED_QUANTITY"))
                objtr.STD_COST = clsCommon.myCdbl(dr("STD_COST"))
                objtr.ACTUAL_COST = clsCommon.myCdbl(dr("ACTUAL_COST"))
                objtr.QTY_VARIENCE_PER = clsCommon.myCdbl(dr("QTY_VARIENCE_PER"))
                objtr.COST_VARIENCE_PER = clsCommon.myCdbl(dr("COST_VARIENCE_PER"))

                objtr.UNIT_COST = clsCommon.myCdbl(dr("UNIT_COST"))
                objtr.UNIT_COST_UOM = clsCommon.myCstr(dr("UNIT_COST_UOM"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

<Serializable()> _
Public Class clsMOToolTypes
#Region "Variables"
    Public MO_CODE As String
    Public OPERATION_CODE As String
    Public WORK_CENTER_CODE As String
    Public TOOL_TYPE_CODE As String
    Public TOOL_TYPE_DESC As String
    Public REQUIRED_QUANTITY As Decimal
    Public USED_QUANTITY As Decimal
    Public STD_COST As Decimal
    Public ACTUAL_COST As Decimal
    Public QTY_VARIENCE_PER As Decimal
    Public COST_VARIENCE_PER As Decimal
    Public UNIT_COST_UOM As String
    Public UNIT_COST As Decimal

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMOToolTypes), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMOToolTypes In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "MO_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", obj.OPERATION_CODE)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                clsCommon.AddColumnsForChange(coll, "TOOL_TYPE_CODE", obj.TOOL_TYPE_CODE)
                clsCommon.AddColumnsForChange(coll, "REQUIRED_QUANTITY", obj.REQUIRED_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "USED_QUANTITY", obj.USED_QUANTITY)

                clsCommon.AddColumnsForChange(coll, "STD_COST", obj.STD_COST)
                clsCommon.AddColumnsForChange(coll, "ACTUAL_COST", obj.ACTUAL_COST)
                clsCommon.AddColumnsForChange(coll, "QTY_VARIENCE_PER", obj.QTY_VARIENCE_PER)
                clsCommon.AddColumnsForChange(coll, "COST_VARIENCE_PER", obj.COST_VARIENCE_PER)

                clsCommon.AddColumnsForChange(coll, "UNIT_COST_UOM", obj.UNIT_COST_UOM)
                clsCommon.AddColumnsForChange(coll, "UNIT_COST", obj.UNIT_COST)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_MO_TOOLTYPES", OMInsertOrUpdate.Insert, "TSPL_MF_MO_TOOLTYPES.MO_CODE='" + strDocNo + "' and TSPL_MF_MO_TOOLTYPES.OPERATION_CODE='" + obj.OPERATION_CODE + "' and  TSPL_MF_MO_TOOLTYPES.WORK_CENTER_CODE='" + obj.WORK_CENTER_CODE + "' and  TSPL_MF_MO_TOOLTYPES.TOOL_TYPE_CODE='" + obj.TOOL_TYPE_CODE + "'", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetMOToolTypes(ByVal MO_CODE As String, ByVal Operation_Code As String, ByVal Work_Center_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOToolTypes)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "select TSPL_MF_MO_TOOLTYPES.MO_CODE,TSPL_MF_MO_TOOLTYPES.OPERATION_CODE,TSPL_MF_MO_TOOLTYPES.WORK_CENTER_CODE,TSPL_MF_MO_TOOLTYPES.TOOL_TYPE_CODE,TSPL_MF_MO_TOOLTYPES.REQUIRED_QUANTITY," & _
              " TSPL_MF_MO_TOOLTYPES.USED_QUANTITY,TSPL_MF_MO_TOOLTYPES.STD_COST,TSPL_MF_MO_TOOLTYPES.ACTUAL_COST,TSPL_MF_MO_TOOLTYPES.QTY_VARIENCE_PER,TSPL_MF_MO_TOOLTYPES.COST_VARIENCE_PER,TSPL_MF_MO_TOOLTYPES.UNIT_COST,TSPL_MF_TOOL_TYPE.DESCRIPTION AS TOOL_TYPE_DESC,TSPL_MF_MO_TOOLTYPES.UNIT_COST_UOM " & _
              " from TSPL_MF_MO_TOOLTYPES LEFT JOIN TSPL_MF_TOOL_TYPE ON TSPL_MF_MO_TOOLTYPES.TOOL_TYPE_CODE=TSPL_MF_TOOL_TYPE.TOOL_TYPE_CODE " & _
              " where TSPL_MF_MO_TOOLTYPES.MO_CODE='" & MO_CODE & "' and TSPL_MF_MO_TOOLTYPES.Operation_Code='" & Operation_Code & "' and TSPL_MF_MO_TOOLTYPES.Work_Center_Code='" & Work_Center_Code & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsMOToolTypes
        Dim ObjList As New List(Of clsMOToolTypes)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMOToolTypes()

                objtr.MO_CODE = clsCommon.myCstr(dr("MO_CODE"))
                objtr.OPERATION_CODE = clsCommon.myCstr(dr("OPERATION_CODE"))
                objtr.WORK_CENTER_CODE = clsCommon.myCstr(dr("WORK_CENTER_CODE"))
                objtr.TOOL_TYPE_CODE = clsCommon.myCstr(dr("TOOL_TYPE_CODE"))
                objtr.TOOL_TYPE_DESC = clsCommon.myCstr(dr("TOOL_TYPE_DESC"))
                objtr.REQUIRED_QUANTITY = clsCommon.myCdbl(dr("REQUIRED_QUANTITY"))
                objtr.USED_QUANTITY = clsCommon.myCdbl(dr("USED_QUANTITY"))
                objtr.STD_COST = clsCommon.myCdbl(dr("STD_COST"))
                objtr.ACTUAL_COST = clsCommon.myCdbl(dr("ACTUAL_COST"))
                objtr.QTY_VARIENCE_PER = clsCommon.myCdbl(dr("QTY_VARIENCE_PER"))
                objtr.COST_VARIENCE_PER = clsCommon.myCdbl(dr("COST_VARIENCE_PER"))

                objtr.UNIT_COST = clsCommon.myCdbl(dr("UNIT_COST"))
                objtr.UNIT_COST_UOM = clsCommon.myCstr(dr("UNIT_COST_UOM"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class

<Serializable()> _
Public Class clsMOOperations
#Region "Variables"
    Public MO_CODE As String
    Public Line_No As Integer
    Public OPERATION_CODE As String
    Public WORK_CENTER_CODE As String
    Public PROJECTED_SETUP_TIME_MINUTES As Decimal
    Public PROJECTED_RUN_TIME_MINUTES As Decimal
    Public PROJECTED_CLEAN_TIME_MINUTES As Decimal
    Public PROJECTED_WAIT_TIME_MINUTES As Decimal

    Public ACTUAL_SETUP_TIME_MINUTES As Decimal
    Public ACTUAL_RUN_TIME_MINUTES As Decimal
    Public ACTUAL_CLEAN_TIME_MINUTES As Decimal
    Public ACTUAL_WAIT_TIME_MINUTES As Decimal

    Public START_DATE As Date? = Nothing
    Public END_DATE As Date? = Nothing
    Public REMARKS As String

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMOOperations), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMOOperations In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "MO_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", obj.OPERATION_CODE)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)

                clsCommon.AddColumnsForChange(coll, "PROJECTED_SETUP_TIME_MINUTES", obj.PROJECTED_SETUP_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "PROJECTED_RUN_TIME_MINUTES", obj.PROJECTED_RUN_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "PROJECTED_CLEAN_TIME_MINUTES", obj.PROJECTED_CLEAN_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "PROJECTED_WAIT_TIME_MINUTES", obj.PROJECTED_WAIT_TIME_MINUTES)

                clsCommon.AddColumnsForChange(coll, "ACTUAL_SETUP_TIME_MINUTES", obj.ACTUAL_SETUP_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "ACTUAL_RUN_TIME_MINUTES", obj.ACTUAL_RUN_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "ACTUAL_CLEAN_TIME_MINUTES", obj.ACTUAL_CLEAN_TIME_MINUTES)
                clsCommon.AddColumnsForChange(coll, "ACTUAL_WAIT_TIME_MINUTES", obj.ACTUAL_WAIT_TIME_MINUTES)

                clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_MO_OPERATIONS", OMInsertOrUpdate.Insert, "TSPL_MF_MO_OPERATIONS.MO_CODE='" + strDocNo + "' and TSPL_MF_MO_OPERATIONS.OPERATION_CODE='" + obj.OPERATION_CODE + "' and  TSPL_MF_MO_OPERATIONS.WORK_CENTER_CODE='" + obj.WORK_CENTER_CODE + "' ", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetMOOperations(ByVal MO_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOOperations)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_MF_MO_OPERATIONS where MO_CODE='" & MO_CODE & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsMOOperations
        Dim ObjList As New List(Of clsMOOperations)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMOOperations()

                objtr.MO_CODE = clsCommon.myCstr(dr("MO_CODE"))
                objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                objtr.OPERATION_CODE = clsCommon.myCstr(dr("OPERATION_CODE"))
                objtr.WORK_CENTER_CODE = clsCommon.myCstr(dr("WORK_CENTER_CODE"))
                objtr.PROJECTED_SETUP_TIME_MINUTES = clsCommon.myCdbl(dr("PROJECTED_SETUP_TIME_MINUTES"))
                objtr.PROJECTED_RUN_TIME_MINUTES = clsCommon.myCdbl(dr("PROJECTED_RUN_TIME_MINUTES"))
                objtr.PROJECTED_CLEAN_TIME_MINUTES = clsCommon.myCdbl(dr("PROJECTED_CLEAN_TIME_MINUTES"))
                objtr.PROJECTED_WAIT_TIME_MINUTES = clsCommon.myCdbl(dr("PROJECTED_WAIT_TIME_MINUTES"))

                objtr.ACTUAL_SETUP_TIME_MINUTES = clsCommon.myCdbl(dr("ACTUAL_SETUP_TIME_MINUTES"))
                objtr.ACTUAL_RUN_TIME_MINUTES = clsCommon.myCdbl(dr("ACTUAL_RUN_TIME_MINUTES"))
                objtr.ACTUAL_CLEAN_TIME_MINUTES = clsCommon.myCdbl(dr("ACTUAL_CLEAN_TIME_MINUTES"))
                objtr.ACTUAL_WAIT_TIME_MINUTES = clsCommon.myCdbl(dr("ACTUAL_WAIT_TIME_MINUTES"))

                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class
<Serializable()> _
Public Class clsMOCosting
#Region "Variables"
    Public MO_CODE As String
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


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMOCosting), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMOCosting In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "MO_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                clsCommon.AddColumnsForChange(coll, "CALC_TYPE", obj.CALC_TYPE)
                clsCommon.AddColumnsForChange(coll, "DIRECT_MATERIAL_COST", obj.DIRECT_MATERIAL_COST)
                clsCommon.AddColumnsForChange(coll, "PACKAGING_MATERIAL_COST", obj.PACKAGING_MATERIAL_COST)
                clsCommon.AddColumnsForChange(coll, "SETUP_LABOR_COST", obj.SETUP_LABOR_COST)
                clsCommon.AddColumnsForChange(coll, "DIRECT_LABOR_COST", obj.DIRECT_LABOR_COST)
                clsCommon.AddColumnsForChange(coll, "OVERHEAD_COST", obj.OVERHEAD_COST)
                clsCommon.AddColumnsForChange(coll, "SUBCONTRACT_COST", obj.SUBCONTRACT_COST)
                clsCommon.AddColumnsForChange(coll, "TOOL_COST", obj.TOOL_COST)
                clsCommon.AddColumnsForChange(coll, "TOTAL_COST", obj.TOTAL_COST)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_MO_COSTING", OMInsertOrUpdate.Insert, "TSPL_MF_MO_COSTING.MO_CODE='" + strDocNo + "' ", trans)

            Next

        End If

        Return True
    End Function
    Public Shared Function GetMOCosting(ByVal MO_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOCosting)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_MF_MO_COSTING " & _
              " where TSPL_MF_MO_COSTING.MO_CODE='" & MO_CODE & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsMOCosting
        Dim ObjList As New List(Of clsMOCosting)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMOCosting()

                objtr.MO_CODE = clsCommon.myCstr(dr("MO_CODE"))
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
    Public Shared Function GetMOActualDirectMaterialCost(ByVal MO_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim dcost As Decimal
        Dim qry As String = ""
        qry = " select coalesce(sum(tspl_mf_issue_detail.avg_cost),0) as actual_cost from tspl_mf_issue_detail " & _
              " inner join tspl_mf_issue on tspl_mf_issue_detail.issue_code=tspl_mf_issue.issue_code where tspl_mf_issue_detail.MO_CODE='" & MO_CODE & "'"
        dcost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        Return dcost
    End Function
End Class

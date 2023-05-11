Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsProductionRequisition

#Region "Variables"

    Public REQ_CODE As String
    Public REQUESTED_BY As String
    Public DESCRIPTION As String
    Public COMMENTS As String
    Public REQ_DATE As Date
    Public EXP_DATE As Date
    Public LOCATION_CODE As String
    Public POSTED As Boolean
    Public ISUSED As Boolean
    Public POSTING_DATE As Date

    Public LOCATION_NAME As String
    Public REQUESTED_BY_NAME As String
    Public TR_TYPE As String

    Public ObjList As List(Of clsProductionRequisitionDetail)
#End Region

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProductionRequisition
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
            qry = "delete from TSPL_MF_REQ_DETAIL where REQ_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MF_REQUISITION where REQ_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionRequisition
        Dim obj As New clsProductionRequisition()
        Dim objtr As New clsProductionRequisitionDetail()
        ObjList = New List(Of clsProductionRequisitionDetail)

        Dim qry As String = " SELECT TSPL_MF_REQUISITION.*,TSPL_LOCATION_MASTER.Location_Desc,TSPL_EMPLOYEE_MASTER.Emp_Name FROM TSPL_MF_REQUISITION  "
        qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_MF_REQUISITION.LOCATION_CODE "
        qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_MF_REQUISITION.REQUESTED_BY "
        qry += "where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and REQ_CODE = (select MIN(REQ_CODE) from TSPL_MF_REQUISITION)"
            Case NavigatorType.Last
                qry += " and REQ_CODE = (select Max(REQ_CODE) from TSPL_MF_REQUISITION)"
            Case NavigatorType.Next
                qry += " and REQ_CODE = (select Min(REQ_CODE) from TSPL_MF_REQUISITION where REQ_CODE > '" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and REQ_CODE = (select Max(REQ_CODE) from TSPL_MF_REQUISITION where REQ_CODE < '" + strCode + "')"
            Case NavigatorType.Current
                qry += " and REQ_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.REQ_CODE = clsCommon.myCstr(dt.Rows(0)("REQ_CODE"))
            obj.REQUESTED_BY = clsCommon.myCstr(dt.Rows(0)("REQUESTED_BY"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))

            obj.TR_TYPE = clsCommon.myCstr(dt.Rows(0)("TR_TYPE"))

            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.REQ_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("REQ_DATE"), "dd/MMM/yyyy"))
            obj.EXP_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("EXP_DATE"), "dd/MMM/yyyy"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            obj.ISUSED = clsCommon.myCBool(dt.Rows(0)("ISUSED"))
            If clsCommon.myLen(dt.Rows(0)("POSTING_DATE")) > 0 Then
                obj.POSTING_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("POSTING_DATE"), "dd/MMM/yyyy"))
            Else
                obj.POSTING_DATE = Nothing
            End If
            obj.LOCATION_NAME = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.REQUESTED_BY_NAME = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.ObjList = objtr.GetData(obj.REQ_CODE, trans)
        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsProductionRequisition, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            If isNewEntry Then
                If (clsCommon.myLen(obj.REQ_CODE) <= 0) Then
                    obj.REQ_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.REQ_DATE, "dd/MMM/yyyy"), clsDocType.ProductionRequisition, "", "")
                End If
            End If

            If (clsCommon.myLen(obj.REQ_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "REQUESTED_BY", obj.REQUESTED_BY)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION ", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "TR_TYPE", obj.TR_TYPE)

            clsCommon.AddColumnsForChange(coll, "REQ_DATE", clsCommon.GetPrintDate(obj.REQ_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "EXP_DATE", clsCommon.GetPrintDate(obj.EXP_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "REQ_CODE", obj.REQ_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_REQUISITION", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_REQUISITION", OMInsertOrUpdate.Update, "TSPL_MF_REQUISITION.REQ_CODE='" + obj.REQ_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsProductionRequisitionDetail.SaveData(obj.REQ_CODE, obj.ObjList, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As New clsProductionRequisition
            obj = obj.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.REQ_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1 AndAlso obj.ISUSED = 1) Then
                Throw New Exception("Already Post on :" + obj.POSTING_DATE)
            End If
            Dim qry As String = "Update TSPL_MF_REQUISITION set POSTED=1, POSTING_DATE ='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where REQ_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsProductionRequisitionDetail
#Region "Variables"

    Public REQ_CODE As String
    Public BO_CODE As String
    Public MO_CODE As String
    Public PRODUCTION_LINE_CODE As String
    Public BOM_CODE As String

    Public ITEM_DESCRIPTION As String
    Public ITEM_CODE As String
    Public BATCH_QTY As Double
    Public REQ_QTY As Double
    Public UNIT_CODE As String
    Public REMARKS As String
    Public TR_TYPE As String

    Public ObjList As List(Of clsProductionRequisitionDetail)
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsProductionRequisitionDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String
            qry = " delete from TSPL_MF_REQ_DETAIL where REQ_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsProductionRequisitionDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "REQ_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "BO_CODE", obj.BO_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                    clsCommon.AddColumnsForChange(coll, "TR_TYPE", obj.TR_TYPE)
                    clsCommon.AddColumnsForChange(coll, "MO_CODE", obj.MO_CODE, True)

                    clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "BATCH_QTY", obj.BATCH_QTY)
                    clsCommon.AddColumnsForChange(coll, "REQ_QTY", obj.REQ_QTY)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_REQ_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsProductionRequisitionDetail)
        Dim qry As String = " "
        qry += " select * FROM TSPL_MF_REQ_DETAIL "
        qry += " where REQ_CODE = '" + strDocNo + "'"
        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsProductionRequisitionDetail
        ObjList = New List(Of clsProductionRequisitionDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsProductionRequisitionDetail()
                objtr.REQ_CODE = clsCommon.myCstr(dr("REQ_CODE"))
                objtr.BO_CODE = clsCommon.myCstr(dr("BO_CODE"))
                objtr.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr("PRODUCTION_LINE_CODE"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))

                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.BATCH_QTY = clsCommon.myCdbl(dr("BATCH_QTY"))
                objtr.REQ_QTY = clsCommon.myCdbl(dr("REQ_QTY"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))
                objtr.TR_TYPE = clsCommon.myCstr(dr("TR_TYPE"))
                objtr.MO_CODE = clsCommon.myCstr(dr("MO_CODE"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function
End Class

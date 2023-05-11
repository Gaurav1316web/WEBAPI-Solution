Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsProductionReturn

#Region "Variables"

    Public RETURN_CODE As String
    Public DESCRIPTION As String
    Public RETURNED_BY As String
    Public RETURNED_BY_NAME As String
    Public RETURNED_TO As String
    Public RETURNED_TO_NAME As String
    Public COMMENTS As String
    Public RETURN_DATE As Date
    Public EXP_DATE As Date
    Public LOCATION_CODE As String
    Public LOCATION_NAME As String
    Public LOCATION_CODE_FROM As String
    Public LOCATION_FROM_NAME As String
    Public POSTED As Boolean
    Public POSTING_DATE As Date
    Public ObjList As List(Of clsProductionReturnDetail)
    Public TR_TYPE As String
#End Region

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProductionReturn
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
            qry = "delete from TSPL_MF_RETURN_DETAIL where RETURN_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MF_RETURN where RETURN_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsSerializeInvenotry.DeleteData("PROD_RN", strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionReturn
        Dim obj As New clsProductionReturn()
        Dim objtr As New clsProductionReturnDetail()
        ObjList = New List(Of clsProductionReturnDetail)

        Dim qry As String = " SELECT TSPL_MF_RETURN.*,TSPL_LOCATION_MASTER.Location_Desc,TSPL_EMPLOYEE_MASTER.Emp_Name,T1.Location_Desc AS [LOCATION_FROM_NAME], T2.Emp_Name AS [RETURNED_TO_NAME] FROM TSPL_MF_RETURN "
        qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_MF_RETURN.LOCATION_CODE "
        qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER T1 ON T1.Location_Code = TSPL_MF_RETURN.LOCATION_CODE_FROM "
        qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_MF_RETURN.RETURNED_BY "
        qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER T2 ON T2.EMP_CODE = TSPL_MF_RETURN.RETURNED_TO "
        qry += " where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and RETURN_CODE = (select MIN(RETURN_CODE) from TSPL_MF_RETURN)"
            Case NavigatorType.Last
                qry += " and RETURN_CODE = (select Max(RETURN_CODE) from TSPL_MF_RETURN)"
            Case NavigatorType.Next
                qry += " and RETURN_CODE = (select Min(RETURN_CODE) from TSPL_MF_RETURN where RETURN_CODE > '" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and RETURN_CODE = (select Max(RETURN_CODE) from TSPL_MF_RETURN where RETURN_CODE < '" + strCode + "')"
            Case NavigatorType.Current
                qry += " and RETURN_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.RETURN_CODE = clsCommon.myCstr(dt.Rows(0)("RETURN_CODE"))
            obj.RETURNED_BY = clsCommon.myCstr(dt.Rows(0)("RETURNED_BY"))
            obj.RETURNED_BY_NAME = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.RETURNED_TO = clsCommon.myCstr(dt.Rows(0)("RETURNED_TO"))
            obj.RETURNED_TO_NAME = clsCommon.myCstr(dt.Rows(0)("RETURNED_TO_NAME"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_NAME = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.LOCATION_CODE_FROM = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE_FROM"))
            obj.LOCATION_FROM_NAME = clsCommon.myCstr(dt.Rows(0)("LOCATION_FROM_NAME"))
            obj.TR_TYPE = clsCommon.myCstr(dt.Rows(0)("TR_TYPE"))

            obj.RETURN_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("RETURN_DATE"), "dd/MMM/yyyy"))
            obj.EXP_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("EXP_DATE"), "dd/MMM/yyyy"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))

            If clsCommon.myLen(dt.Rows(0)("POSTING_DATE")) > 0 Then
                obj.POSTING_DATE = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("POSTING_DATE"), "dd/MMM/yyyy"))
            Else
                obj.POSTING_DATE = Nothing
            End If
            obj.ObjList = objtr.GetData(obj.RETURN_CODE, trans)
        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsProductionReturn, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If isNewEntry Then
                If (clsCommon.myLen(obj.RETURN_CODE) <= 0) Then
                    obj.RETURN_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.RETURN_DATE, "dd/MMM/yyyy"), clsDocType.ProductionReturn, "", "")
                End If
            End If
            If (clsCommon.myLen(obj.RETURN_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Standard Production", "Production Return", obj.LOCATION_CODE, obj.RETURN_DATE, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "RETURNED_BY", obj.RETURNED_BY)
            clsCommon.AddColumnsForChange(coll, "RETURNED_TO", obj.RETURNED_TO)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE_FROM", obj.LOCATION_CODE_FROM)
            clsCommon.AddColumnsForChange(coll, "TR_TYPE", obj.TR_TYPE)

            clsCommon.AddColumnsForChange(coll, "RETURN_DATE", clsCommon.GetPrintDate(obj.RETURN_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "EXP_DATE", clsCommon.GetPrintDate(obj.EXP_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "RETURN_CODE", obj.RETURN_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RETURN", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RETURN", OMInsertOrUpdate.Update, "TSPL_MF_RETURN.RETURN_CODE='" + obj.RETURN_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsProductionReturnDetail.SaveData(obj.RETURN_CODE, obj.ObjList, trans)
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
            Dim obj As New clsProductionReturn
            obj = obj.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.RETURN_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.POSTING_DATE)
            End If
            If obj.SaveDataInIssueReturn(obj, "PROD_RN", True) Then
                Dim qry As String = "Update TSPL_MF_RETURN set POSTED=1, POSTING_DATE ='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where RETURN_CODE ='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            Else
                Throw New Exception("Data Not Saved in Issue/Return/Transfer.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveDataInIssueReturn(ByVal obj As clsProductionReturn, ByVal DOCTYPE As String, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            'Dim qry As String = "delete from TSPL_IssueReturn_DETAIL where Doc_No='" + obj.Doc_No + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            Dim strDesc As String = "Entery from Production Return. Remark : "
            If isNewEntry Then
                strDocNo = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RETURN_DATE), clsDocType.IssueReturn, clsDocTransactionType.ItemReturn, obj.LOCATION_CODE)
            End If

            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.RETURN_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", "Return")
            clsCommon.AddColumnsForChange(coll, "From_Location", obj.LOCATION_CODE_FROM)
            clsCommon.AddColumnsForChange(coll, "To_Location", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "Remarks", strDesc + obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "Issue_To", obj.RETURNED_TO)
            clsCommon.AddColumnsForChange(coll, "Request_By", obj.RETURNED_BY)
            'clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            'clsCommon.AddColumnsForChange(coll, "Req_IssueNo", obj.ISSUE_CODE)
            'clsCommon.AddColumnsForChange(coll, "RequisitionNo", obj.RequisitionNo)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_HEAD", OMInsertOrUpdate.Update, "TSPL_IssueReturn_HEAD.Doc_No='" + strDocNo + "'", trans)
            End If
            isSaved = isSaved AndAlso SaveDataInIssueReturnDetail(strDocNo, obj.ObjList, DOCTYPE, trans)
            isSaved = isSaved AndAlso clsIssueReturnHead.PostData(strDocNo, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveDataInIssueReturnDetail(ByVal strDocNo As String, ByVal Arr As List(Of clsProductionReturnDetail), ByVal DocType As String, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim LineNo As Int16 = 0
            For Each obj As clsProductionReturnDetail In Arr
                Dim coll As New Hashtable()
                LineNo = LineNo + 1
                clsCommon.AddColumnsForChange(coll, "Doc_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", LineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.ITEM_DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "Required_Qty", obj.REQ_QTY)
                clsCommon.AddColumnsForChange(coll, "Issued_Qty", obj.ISSUE_QTY)
                clsCommon.AddColumnsForChange(coll, "Issued_Qty_AgainstRet", obj.RETURN_QTY)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "Req_IssueNo", obj.ISSUE_CODE)
                clsCommon.AddColumnsForChange(coll, "Cost_Code", obj.PRODUCTION_LINE_CODE, True)
                'clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                'clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                'clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                'clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                'clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                'clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                'clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                'clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                'clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                'clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                'clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                'clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                'clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                'clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                'clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                'clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function

End Class

Public Class clsProductionReturnDetail
#Region "Variables"
    
    Public RETURN_CODE As String
    Public ISSUE_CODE As String
    Public PRODUCTION_LINE_CODE As String
    Public BOM_CODE As String

    Public ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public BATCH_QTY As Double
    Public REQ_QTY As Double
    Public ISSUE_QTY As Double
    Public UNIT_CODE As String
    Public RETURN_QTY As Double
    Public WASTAGE_QTY As Double
    Public CONSUMED_QTY As String
    Public REMARKS As String
    Public ObjList As List(Of clsProductionReturnDetail)
    Public TR_TYPE As String

    Public Doc_Date As DateTime
    Public From_Location As String
    Public To_Location As String
    Public PostingDate As DateTime
    Public IsPosted As Boolean
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsProductionReturnDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String
            qry = " delete from TSPL_MF_RETURN_DETAIL where RETURN_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsSerializeInvenotry.DeleteData("PROD_RN", strDocNo, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsProductionReturnDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "RETURN_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "ISSUE_CODE", obj.ISSUE_CODE)
                    clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                    clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "BATCH_QTY", obj.BATCH_QTY)
                    clsCommon.AddColumnsForChange(coll, "REQ_QTY", obj.REQ_QTY)
                    clsCommon.AddColumnsForChange(coll, "ISSUE_QTY", obj.ISSUE_QTY)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "RETURN_QTY", obj.RETURN_QTY)
                    clsCommon.AddColumnsForChange(coll, "WASTAGE_QTY", obj.WASTAGE_QTY)
                    clsCommon.AddColumnsForChange(coll, "CONSUMED_QTY", obj.CONSUMED_QTY)
                    clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                    clsCommon.AddColumnsForChange(coll, "TR_TYPE", obj.TR_TYPE)
                    

                    '' saving production costing 
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.FIFO, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans))
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.LIFO, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans))
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.ITEM_CODE, obj.From_Location, obj.ISSUE_QTY, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), obj.IsPosted, trans))

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RETURN_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    If Not obj.arrSrItem Is Nothing AndAlso obj.arrSrItem.Count > 0 Then

                        clsSerializeInvenotry.SaveData("PROD_RN", strDocNo, obj.Doc_Date, "O", obj.ITEM_CODE, obj.From_Location, (Arr.IndexOf(obj) + 1), obj.arrSrItem, trans) 'out from Fromlocation
                        clsSerializeInvenotry.SaveData("PROD_RN", strDocNo, obj.Doc_Date, "I", obj.ITEM_CODE, obj.To_Location, (Arr.IndexOf(obj) + 1), obj.arrSrItem, trans) 'In at To Location
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsProductionReturnDetail)
        Dim qry As String = " "
        qry += " select * FROM TSPL_MF_RETURN_DETAIL "
        qry += " where RETURN_CODE = '" + strDocNo + "'"
        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsProductionReturnDetail
        ObjList = New List(Of clsProductionReturnDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsProductionReturnDetail()
                objtr.RETURN_CODE = clsCommon.myCstr(dr("RETURN_CODE"))
                objtr.ISSUE_CODE = clsCommon.myCstr(dr("ISSUE_CODE"))
                objtr.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr("PRODUCTION_LINE_CODE"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.BATCH_QTY = clsCommon.myCdbl(dr("BATCH_QTY"))
                objtr.REQ_QTY = clsCommon.myCdbl(dr("REQ_QTY"))
                objtr.ISSUE_QTY = clsCommon.myCdbl(dr("ISSUE_QTY"))
                objtr.RETURN_QTY = clsCommon.myCdbl(dr("RETURN_QTY"))
                objtr.WASTAGE_QTY = clsCommon.myCdbl(dr("WASTAGE_QTY"))
                objtr.CONSUMED_QTY = clsCommon.myCdbl(dr("CONSUMED_QTY"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))
                objtr.TR_TYPE = clsCommon.myCstr(dr("TR_TYPE"))

                'If clsCommon.myLen(objtr.PRODUCTION_LINE_CODE) <= 0 Then
                '    objtr.PRODUCTION_LINE_CODE = "1"
                'End If
                objtr.arrSrItem = clsSerializeInvenotry.GetData("PROD_RN", objtr.RETURN_CODE, objtr.ITEM_CODE, (dt.Rows.IndexOf(dr) + 1), trans)
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function
End Class

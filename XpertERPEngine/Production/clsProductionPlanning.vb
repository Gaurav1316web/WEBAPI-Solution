Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsProductionPlanning

#Region "Variables"
    Public PROD_PLAN_CODE As String
    Public DESCRIPTION As String
    Public COMMENTS As String
    Public PLANNING_DATE As Date
    Public PLANNED_BY As String
    Public PLANNED_BY_NAME As String
    Public PLAN_FOR_DATE As Date
    Public PLAN_TO_DATE As Date? = Nothing
    Public CREATED_BY As String
    Public APPROVED_BY As String

    Public POSTED As Boolean
    Public Posting_Date As Date


    '' grid columns
    
    Public Line_No As Integer
    Public PRODUCTION_LINE_CODE As String
    Public BOM_CODE As String
    Public BOM_REVISION_NO As String
    Public ITEM_CODE As String
    Public ITEM_DESCRIPTION As String
    Public MIN_QUANTITY As Decimal
    Public MAX_QUANTITY As Decimal
    Public PLAN_QTY As Decimal
    Public UNIT_CODE As String
    Public REMARKS As String
    Public SO_Id As String = Nothing
    Public SO_Desc As String = Nothing
    Public Buffer_Qty As Double = Nothing
    Public Extra_Add_Qty As Double = Nothing
    Public Net_Req_Qty As Double = Nothing
    Public Location_Code As String = Nothing
    Public stock_qty As Double = Nothing

    Public Shared ObjList As List(Of clsProductionPlanning)
    'Public Arr As New List(Of clsEmpSalaryPayHeadDetails)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProductionPlanning
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetItemBalance(ByVal Location_Code As String, ByVal Item_Code As String, ByVal Unit_Code As String) As Double
        Dim qty As Double = 0

        If clsCommon.myLen(Location_Code) > 0 AndAlso clsCommon.myLen(Item_Code) > 0 AndAlso clsCommon.myLen(Unit_Code) > 0 Then
            Dim qry As String = "select sum(qty) as qty from (" & _
            "select axa.item_code,(case when finalconversion.conversion_factor>0 then (isnull(axa.qty,0)*TSPL_ITEM_UOM_DETAIL.conversion_factor)/finalconversion.conversion_factor else 0 end) as qty from " & _
            "(select item_code,uom,(case when inout='I' then qty else case when inout='O' then (0-qty) end end) as qty from TSPL_INVENTORY_MOVEMENT where location_code='" + Location_Code + "' and item_code='" + Item_Code + "')axa " & _
            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=axa.item_code and TSPL_ITEM_UOM_DETAIL.uom_code=axa.uom " & _
            " left outer join TSPL_ITEM_UOM_DETAIL finalconversion on finalconversion.item_code=axa.item_code and finalconversion.uom_code='" + Unit_Code + "')axa1 "
            qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        End If

        Return qty
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean
            isSaved = True

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_MF_PROD_PLAN_DETAIL where PROD_PLAN_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_PRODUCTION_PLAN_HEAD where PROD_PLAN_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionPlanning
        Dim obj As New clsProductionPlanning()
        Dim objtr As New clsProductionPlanning()

        ObjList = New List(Of clsProductionPlanning)

        Dim qry As String = "SELECT T1.Location_Code,T1.PROD_PLAN_CODE,T1.DESCRIPTION,T1.COMMENTS,T1.PLANNING_DATE,T1.PLANNED_BY,T2.EMP_NAME AS PLLANED_BY_NAME,T1.PLAN_FOR_DATE,T1.PLAN_TO_DATE,"
        qry += " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE FROM TSPL_MF_PRODUCTION_PLAN_HEAD  T1 INNER JOIN TSPL_EMPLOYEE_MASTER T2  ON T1.PLANNED_BY=T2.EMP_CODE WHERE 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND PROD_PLAN_CODE = (select MIN(PROD_PLAN_CODE) from TSPL_MF_PRODUCTION_PLAN_HEAD)"
            Case NavigatorType.Last
                qry += " AND PROD_PLAN_CODE = (select Max(PROD_PLAN_CODE) from TSPL_MF_PRODUCTION_PLAN_HEAD)"
            Case NavigatorType.Next
                qry += " AND PROD_PLAN_CODE = (select Min(PROD_PLAN_CODE) from TSPL_MF_PRODUCTION_PLAN_HEAD where  PROD_PLAN_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND PROD_PLAN_CODE = (select Max(PROD_PLAN_CODE) from TSPL_MF_PRODUCTION_PLAN_HEAD where PROD_PLAN_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND PROD_PLAN_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.PROD_PLAN_CODE = dt.Rows(0)("PROD_PLAN_CODE")
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.PLANNING_DATE = clsCommon.GetPrintDate(dt.Rows(0)("PLANNING_DATE"), "dd/MMM/yyyy")
            obj.PLANNED_BY = clsCommon.myCstr(dt.Rows(0)("PLANNED_BY"))
            obj.PLANNED_BY_NAME = clsCommon.myCstr(dt.Rows(0)("PLLANED_BY_NAME"))
            obj.PLAN_FOR_DATE = clsCommon.GetPrintDate(dt.Rows(0)("PLAN_FOR_DATE"), "dd/MMM/yyyy")
            If IsDBNull(dt.Rows(0)("PLAN_TO_DATE")) = False Then
                obj.PLAN_TO_DATE = clsCommon.GetPrintDate(dt.Rows(0)("PLAN_TO_DATE"), "dd/MMM/yyyy")
            Else
                obj.PLAN_TO_DATE = Nothing
            End If

            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.APPROVED_BY = clsCommon.myCstr(dt.Rows(0)("APPROVED_BY"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))

            strCode = dt.Rows(0)("PROD_PLAN_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = "SELECT T1.stock_qty,T1.SO_Id,tspl_sd_sales_order_head.description as so_desc,T1.Buffer_Qty,T1.Extra_Add_Qty,T1.Net_Req_Qty,T1.PROD_PLAN_CODE,T1.LINE_NO,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T1.BOM_REVISION_NO,T1.ITEM_CODE,T1.ITEM_DESCRIPTION, "
        qry += " T1.MIN_QUANTITY,T1.MAX_QUANTITY,T1.PLAN_QTY,T1.UNIT_CODE,T1.REMARKS FROM TSPL_MF_PROD_PLAN_DETAIL T1 "
        qry += " left outer join tspl_sd_sales_order_head on tspl_sd_sales_order_head.document_code=T1.so_id "
        qry += "  WHERE 2=2 AND T1.PROD_PLAN_CODE = '" + strCode + "' ORDER BY T1.LINE_NO"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsProductionPlanning()

                objtr.Line_No = clsCommon.myCdbl(dr("LINE_NO"))
                objtr.PRODUCTION_LINE_CODE = clsCommon.myCstr(dr("PRODUCTION_LINE_CODE"))
                objtr.BOM_CODE = clsCommon.myCstr(dr("BOM_CODE"))
                objtr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objtr.BOM_REVISION_NO = clsCommon.myCstr(dr("BOM_REVISION_NO"))
                objtr.ITEM_DESCRIPTION = clsCommon.myCstr(dr("ITEM_DESCRIPTION"))
                objtr.MIN_QUANTITY = clsCommon.myCdbl(dr("MIN_QUANTITY"))
                objtr.MAX_QUANTITY = clsCommon.myCdbl(dr("MAX_QUANTITY"))
                objtr.PLAN_QTY = clsCommon.myCdbl(dr("PLAN_QTY"))
                objtr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))

                objtr.SO_Id = clsCommon.myCstr(dr("so_id"))
                objtr.SO_Desc = clsCommon.myCstr(dr("so_desc"))
                objtr.Buffer_Qty = clsCommon.myCdbl(dr("buffer_qty"))
                objtr.Extra_Add_Qty = clsCommon.myCdbl(dr("Extra_Add_Qty"))
                objtr.Net_Req_Qty = clsCommon.myCdbl(dr("Net_Req_Qty"))
                objtr.stock_qty = clsCommon.myCdbl(dr("stock_qty"))

                ObjList.Add(objtr)
            Next
        End If

        clsProductionPlanning.ObjList = ObjList
        Return obj
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select PROD_PLAN_CODE as Code,DESCRIPTION as Description,COMMENTS as Comments,PLANNING_DATE as [Planning Date]," & _
                            " PLAN_FOR_DATE as [From Date],PLAN_TO_DATE as [To Date] from TSPL_MF_PRODUCTION_PLAN_HEAD "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_MF_PRODUCTION_PLAN_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_MF_PRODUCTION_PLAN_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("PP", qry, "Code", whrCls, currCode, "YEAR(PLANNING_DATE) desc , Code  desc", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetFinerForPlanItem(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As DataTable
        Dim qry As String = " select TSPL_MF_PROD_PLAN_DETAIL.ITEM_CODE as Code,TSPL_MF_PROD_PLAN_DETAIL.BOM_CODE,TSPL_MF_BOM_HEAD.DESCRIPTION as Description,TSPL_MF_PROD_PLAN_DETAIL.BOM_REVISION_NO," & _
                            " TSPL_MF_PROD_PLAN_DETAIL.ITEM_DESCRIPTION, TSPL_MF_PROD_PLAN_DETAIL.PLAN_QTY, TSPL_MF_PROD_PLAN_DETAIL.UNIT_CODE " & _
                            " from TSPL_MF_PRODUCTION_PLAN_HEAD " & _
                            " inner join TSPL_MF_PROD_PLAN_DETAIL on  TSPL_MF_PRODUCTION_PLAN_HEAD.PROD_PLAN_CODE=TSPL_MF_PROD_PLAN_DETAIL.PROD_PLAN_CODE" & _
                            " left join TSPL_MF_BOM_HEAD on TSPL_MF_PROD_PLAN_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE "

        Dim str As String = ""
        Dim dt As New DataTable
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_MF_PRODUCTION_PLAN_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_MF_PRODUCTION_PLAN_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("PP", qry, "Code", whrCls, currCode, "Code", isButtonClicked)
        If clsCommon.myLen(str) > 0 Then
            qry = qry & " where " & whrCls & "  and TSPL_MF_PROD_PLAN_DETAIL.ITEM_CODE='" & str & "'"
            dt = clsDBFuncationality.GetDataTable(qry)
        End If
        Return dt
    End Function

    Public Function SaveData(ByVal obj As clsProductionPlanning, ByVal objList As List(Of clsProductionPlanning), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, objList, isNewEntry, trans, strCode)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SaveData(ByVal obj As clsProductionPlanning, ByVal objList As List(Of clsProductionPlanning), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        If isNewEntry Then
            Dim strIndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, trans))

            If strCode = "" Then
                If clsCommon.CompairString(strIndustryType, "R") = CompairStringResult.Equal Then
                    obj.PROD_PLAN_CODE = clsERPFuncationality.GetNextCode(trans, obj.PLANNING_DATE, clsDocType.StandardProductionPlanning, "", "")
                Else
                    obj.PROD_PLAN_CODE = clsERPFuncationality.GetNextCode(trans, obj.PLANNING_DATE, clsDocType.ProductionPlanning, "", obj.Location_Code)
                End If

            Else
                obj.PROD_PLAN_CODE = strCode
            End If
        End If


        Try

            Dim qry As String = "DELETE FROM TSPL_MF_PROD_PLAN_DETAIL WHERE PROD_PLAN_CODE='" + obj.PROD_PLAN_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.PROD_PLAN_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", obj.PROD_PLAN_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "PLANNING_DATE", clsCommon.GetPrintDate(obj.PLANNING_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "PLANNED_BY", obj.PLANNED_BY)
            clsCommon.AddColumnsForChange(coll, "PLAN_FOR_DATE", clsCommon.GetPrintDate(obj.PLAN_FOR_DATE, "dd/MMM/yyyy"))
            If Not obj.PLAN_TO_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "PLAN_TO_DATE", clsCommon.GetPrintDate(obj.PLAN_TO_DATE, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MF_PRODUCTION_PLAN_HEAD where PROD_PLAN_CODE = '" & obj.PROD_PLAN_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRODUCTION_PLAN_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.PROD_PLAN_CODE + " Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRODUCTION_PLAN_HEAD", OMInsertOrUpdate.Update, "TSPL_MF_PRODUCTION_PLAN_HEAD.PROD_PLAN_CODE='" + obj.PROD_PLAN_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsProductionPlanningDetail.SaveData(obj.PROD_PLAN_CODE, obj.PLANNED_BY, objList, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
            objList = Nothing
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsProductionPlanning = clsProductionPlanning.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.PROD_PLAN_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MF_PRODUCTION_PLAN_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where PROD_PLAN_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckCode(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select count(PROD_PLAN_CODE) from TSPL_MF_PRODUCTION_PLAN_HEAD where PROD_PLAN_CODE='" + strCode + "' "
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class


Public Class clsProductionPlanningDetail
#Region "Variables"

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal PLANNED_BY As String, ByVal Arr As List(Of clsProductionPlanning), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsProductionPlanning In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "BOM_REVISION_NO", obj.BOM_REVISION_NO)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                    clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)
                    clsCommon.AddColumnsForChange(coll, "MIN_QUANTITY", obj.MIN_QUANTITY)
                    clsCommon.AddColumnsForChange(coll, "MAX_QUANTITY", obj.MAX_QUANTITY)
                    clsCommon.AddColumnsForChange(coll, "PLAN_QTY", obj.PLAN_QTY)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                    clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                    clsCommon.AddColumnsForChange(coll, "SO_Id", obj.SO_Id)
                    clsCommon.AddColumnsForChange(coll, "Buffer_Qty", obj.Buffer_Qty)
                    clsCommon.AddColumnsForChange(coll, "Extra_Add_Qty", obj.Extra_Add_Qty)
                    clsCommon.AddColumnsForChange(coll, "Net_Req_Qty", obj.Net_Req_Qty)
                    clsCommon.AddColumnsForChange(coll, "stock_qty", obj.stock_qty)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PROD_PLAN_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MF_PROD_PLAN_DETAIL.PROD_PLAN_CODE='" + strDocNo + "'", trans)
                Next

            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
   
    Public Shared Function SaveDataImport(ByVal Arr As List(Of clsProductionPlanning), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsProductionPlanning In Arr
                Dim qry As String = "DELETE FROM TSPL_MF_PROD_PLAN_DETAIL WHERE BOM_CODE='" + obj.BOM_CODE + "' AND PROD_PLAN_CODE='" & obj.PROD_PLAN_CODE & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", obj.PROD_PLAN_CODE)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE)
                clsCommon.AddColumnsForChange(coll, "SO_Id", obj.SO_Id, True)
                clsCommon.AddColumnsForChange(coll, "PRODUCTION_LINE_CODE", obj.PRODUCTION_LINE_CODE, True)
                clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "ITEM_DESCRIPTION", obj.ITEM_DESCRIPTION)                
                clsCommon.AddColumnsForChange(coll, "BOM_REVISION_NO", obj.BOM_REVISION_NO)
                clsCommon.AddColumnsForChange(coll, "Buffer_Qty", obj.Buffer_Qty)
                clsCommon.AddColumnsForChange(coll, "PLAN_QTY", obj.PLAN_QTY)
                clsCommon.AddColumnsForChange(coll, "Extra_Add_Qty", obj.Extra_Add_Qty)
                clsCommon.AddColumnsForChange(coll, "stock_qty", obj.stock_qty)
                clsCommon.AddColumnsForChange(coll, "Net_Req_Qty", obj.Net_Req_Qty)
                clsCommon.AddColumnsForChange(coll, "MIN_QUANTITY", obj.MIN_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "MAX_QUANTITY", obj.MAX_QUANTITY)
                clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PROD_PLAN_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MF_PROD_PLAN_DETAIL.PROD_PLAN_CODE='" + obj.PROD_PLAN_CODE + "' AND BOM_CODE='" & obj.BOM_CODE & "'", trans)
            Next
        End If
        Return True
    End Function
End Class

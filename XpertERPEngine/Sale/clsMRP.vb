Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMRP

#Region "Variables"
    
    Public MRP_CODE As String
    Public MRP_REMARKS As String
    Public MRP_DESCRIPTION As String
    Public MRP_DAYS As Integer
    Public MRP_QTY As Decimal
    Public BOM_CODE As String
    Public MRP_ITEM_UNIT_CODE As String
    Public Item_Code As String

    Public MRP_DATE As Date
    Public MRP_FROM As Date
    Public MRP_TO As Date
    Public MRP_Location As String
    Public PACK_SIZE As Decimal = 0

    Public CREATED_BY As String
    Public POSTED As Boolean
    Public Posting_Date As Date
    Public Created_Date As Date
    Public REQUISITION_ID As String
    Public PROD_PLAN_CODE As String
    Public PROD_PLAN_DESC As String

    Public ObjListPO As List(Of clsMRPPO)
    Public ObjListMRPSRN As List(Of clsMRPSRN)
    Public ObjListMRPDetail As List(Of clsMRPDetail)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMRP
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String

            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_PO_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_SRN_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            
            qry = "delete from TSPL_MRP_HEAD where MRP_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMRP
        Dim obj As New clsMRP()

        Dim qry As String = "SELECT TSPL_MRP_HEAD.MRP_CODE,TSPL_MRP_HEAD.MRP_DESCRIPTION,TSPL_MRP_HEAD.MRP_ITEM_UNIT_CODE,TSPL_MRP_HEAD.MRP_DATE, " & _
        " TSPL_MRP_HEAD.MRP_FROM,TSPL_MRP_HEAD.Item_Code,TSPL_MRP_HEAD.MRP_TO,TSPL_MRP_HEAD.MRP_QTY,TSPL_MRP_HEAD.MRP_REMARKS," & _
        " TSPL_MRP_HEAD.MRP_Location,TSPL_MRP_HEAD.BOM_CODE,TSPL_MRP_HEAD.MRP_DAYS,TSPL_MRP_HEAD.PACK_SIZE," & _
        " TSPL_MRP_HEAD.MODIFIED_BY,TSPL_MRP_HEAD.Created_By,TSPL_MRP_HEAD.POSTED,TSPL_MRP_HEAD.Created_Date,TSPL_MRP_HEAD.POSTING_DATE, " & _
        " TSPL_MRP_HEAD.REQUISITION_ID,TSPL_MRP_HEAD.PROD_PLAN_CODE,TSPL_MF_PRODUCTION_PLAN_HEAD.DESCRIPTION AS PROD_PLAN_DESC FROM TSPL_MRP_HEAD " & _
        " LEFT JOIN TSPL_MF_PRODUCTION_PLAN_HEAD ON TSPL_MRP_HEAD.PROD_PLAN_CODE=TSPL_MRP_HEAD.PROD_PLAN_CODE " & _
        " where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND MRP_CODE = (select MIN(MRP_CODE) from TSPL_MRP_HEAD)"
            Case NavigatorType.Last
                qry += " AND MRP_CODE = (select Max(MRP_CODE) from TSPL_MRP_HEAD)"
            Case NavigatorType.Next
                qry += " AND MRP_CODE = (select Min(MRP_CODE) from TSPL_MRP_HEAD where  MRP_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND MRP_CODE = (select Max(MRP_CODE) from TSPL_MRP_HEAD where MRP_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND MRP_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.MRP_CODE = dt.Rows(0)("MRP_CODE")

            obj.MRP_DESCRIPTION = dt.Rows(0)("MRP_DESCRIPTION")
            obj.MRP_QTY = dt.Rows(0)("MRP_QTY")
            obj.PACK_SIZE = dt.Rows(0)("PACK_SIZE")

            obj.BOM_CODE = dt.Rows(0)("BOM_CODE")
            obj.MRP_DAYS = clsCommon.myCstr(dt.Rows(0)("MRP_DAYS"))
            obj.MRP_ITEM_UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("MRP_ITEM_UNIT_CODE"))
            obj.MRP_REMARKS = clsCommon.myCstr(dt.Rows(0)("MRP_REMARKS"))

            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.MRP_DATE = clsCommon.GetPrintDate(dt.Rows(0)("MRP_DATE"), "dd/MMM/yyyy")
            obj.MRP_FROM = clsCommon.GetPrintDate(dt.Rows(0)("MRP_FROM"), "dd/MMM/yyyy")
            '' MRP_TO
            obj.MRP_TO = clsCommon.GetPrintDate(dt.Rows(0)("MRP_TO"), "dd/MMM/yyyy")
            obj.MRP_Location = clsCommon.myCstr(dt.Rows(0)("MRP_Location"))

            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))
            obj.REQUISITION_ID = clsCommon.myCstr(dt.Rows(0)("REQUISITION_ID"))
            obj.PROD_PLAN_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_PLAN_CODE"))
            obj.PROD_PLAN_DESC = clsCommon.myCstr(dt.Rows(0)("PROD_PLAN_DESC"))

            strCode = dt.Rows(0)("MRP_CODE")

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

        End If
        obj.ObjListPO = clsMRPPO.GetMRPPO(strCode, trans)
        obj.ObjListMRPSRN = clsMRPSRN.GetMRPSRN(strCode, trans)
        obj.ObjListMRPDetail = clsMRPDetail.GetMRPDetail(strCode, trans)

        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsMRP, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        If isNewEntry Then
            If clsCommon.myLen(strCode) <= 0 Then
                obj.MRP_CODE = clsERPFuncationality.GetNextCode(trans, obj.MRP_DATE, clsDocType.MRP, "", obj.MRP_Location)
            Else
                obj.MRP_CODE = strCode
            End If
        End If

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Standard Production", "MRP", obj.MRP_Location, obj.MRP_DATE, trans)
        Try
            Dim qry As String
            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_PO_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_SRN_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.MRP_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "MRP_CODE", obj.MRP_CODE)
            clsCommon.AddColumnsForChange(coll, "MRP_DESCRIPTION", obj.MRP_DESCRIPTION)

            clsCommon.AddColumnsForChange(coll, "MRP_QTY", obj.MRP_QTY)
            clsCommon.AddColumnsForChange(coll, "BOM_CODE", obj.BOM_CODE, True)
            clsCommon.AddColumnsForChange(coll, "MRP_ITEM_UNIT_CODE", obj.MRP_ITEM_UNIT_CODE)
            clsCommon.AddColumnsForChange(coll, "MRP_REMARKS", obj.MRP_REMARKS)

            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "MRP_DATE", clsCommon.GetPrintDate(obj.MRP_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MRP_FROM", clsCommon.GetPrintDate(obj.MRP_FROM, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MRP_DAYS", obj.MRP_DAYS)
            clsCommon.AddColumnsForChange(coll, "MRP_TO", clsCommon.GetPrintDate(obj.MRP_TO, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MRP_Location", clsCommon.myCstr(obj.MRP_Location), True)
            clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", clsCommon.myCstr(obj.PROD_PLAN_CODE), True)
            clsCommon.AddColumnsForChange(coll, "PACK_SIZE", obj.PACK_SIZE)

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then

                'clsCommon.AddColumnsForChange(coll, "MRP_CODE", obj.MRP_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MRP_HEAD where MRP_CODE = '" & obj.MRP_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.MRP_CODE + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_HEAD", OMInsertOrUpdate.Update, "TSPL_MRP_HEAD.MRP_CODE='" + obj.MRP_CODE + "'", trans)
            End If

            '' saving ff salary
            If Not obj.ObjListMRPDetail Is Nothing Then
                isSaved = isSaved AndAlso clsMRPDetail.SaveData(obj.MRP_CODE, obj.ObjListMRPDetail, trans)
            End If
            If Not obj.ObjListPO Is Nothing Then
                isSaved = isSaved AndAlso clsMRPPO.SaveData(obj.MRP_CODE, obj.ObjListPO, trans)
            End If

            If Not obj.ObjListMRPSRN Is Nothing Then
                isSaved = isSaved AndAlso clsMRPSRN.SaveData(obj.MRP_CODE, obj.ObjListMRPSRN, trans)
            End If
           
        Catch err As Exception
            Throw New Exception(err.Message)
            Return False
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
            Dim obj As clsMRP = clsMRP.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.MRP_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MRP_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where MRP_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select 1 from TSPL_MRP_HEAD where MRP_CODE='" + strCode + "' and POSTED=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If

            qry = "update TSPL_MRP_HEAD set POSTED=0,Posting_Date=null where MRP_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Shared Function GetBomCodeFromItemCode(ByVal ItemCode As String, Optional ByVal trans As SqlTransaction = Nothing) As clsBillOfMaterial
        Dim obj As clsBillOfMaterial = Nothing
        Dim qry As String = "select BOM_CODE as Code,DESCRIPTION as Description,BOM_DATE as [BOM Date],REVISION_NO as [Revision No] from TSPL_MF_BOM_HEAD "
        Dim WhrCls As String = " STATUS='Approved' and POSTED=1 "
        Dim strCode As String
        strCode = clsCommon.ShowSelectForm("BOMItemFINDER", qry, "Code", WhrCls, ItemCode, "Code", False)
        If clsCommon.myLen(strCode) > 0 Then
            obj = clsBillOfMaterial.GetData(strCode, NavigatorType.Current)

        End If
        Return obj
    End Function
End Class

Public Class clsMRPDetail
#Region "Variables"

    Public MRP_CODE As String
    Public Item_Code As String
    Public Item_Desc As String
    Public RM_UNIT_CODE As String
    Public Opening_Qty As Decimal
    Public PO_Qty As Decimal
    Public SRN_Qty As Decimal
    Public Total_Avail_Qty As Decimal
    Public Total_Requird_Qty As Decimal
    Public Net_Requird_Qty As Decimal
    Public COST As Decimal = 0
    Public TOTAL_COST As Decimal = 0
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPDetail), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMRPDetail In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "RM_UNIT_CODE", obj.RM_UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "Opening_Qty", obj.Opening_Qty)
                clsCommon.AddColumnsForChange(coll, "PO_Qty", obj.PO_Qty)
                clsCommon.AddColumnsForChange(coll, "SRN_Qty", obj.SRN_Qty)

                clsCommon.AddColumnsForChange(coll, "Total_Avail_Qty", obj.Total_Avail_Qty)
                clsCommon.AddColumnsForChange(coll, "Total_Requird_Qty", obj.Total_Requird_Qty)
                clsCommon.AddColumnsForChange(coll, "Net_Requird_Qty", obj.Net_Requird_Qty)
                clsCommon.AddColumnsForChange(coll, "COST", obj.COST)
                clsCommon.AddColumnsForChange(coll, "TOTAL_COST", obj.TOTAL_COST)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MRP_DETAIL.MRP_CODE='" + strDocNo + "' ", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetMRPDetail(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPDetail)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_MRP_DETAIL where MRP_CODE='" & MRP_CODE & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsMRPDetail
        Dim ObjList As New List(Of clsMRPDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMRPDetail()

                objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objtr.RM_UNIT_CODE = clsCommon.myCstr(dr("RM_UNIT_CODE"))
                objtr.Opening_Qty = clsCommon.myCdbl(dr("Opening_Qty"))
                objtr.PO_Qty = clsCommon.myCdbl(dr("PO_Qty"))
                objtr.SRN_Qty = clsCommon.myCdbl(dr("SRN_Qty"))

                objtr.Total_Avail_Qty = clsCommon.myCdbl(dr("Total_Avail_Qty"))
                objtr.Total_Requird_Qty = clsCommon.myCdbl(dr("Total_Requird_Qty"))
                objtr.Net_Requird_Qty = clsCommon.myCdbl(dr("Net_Requird_Qty"))
                objtr.COST = clsCommon.myCdbl(dr("COST"))
                objtr.TOTAL_COST = clsCommon.myCdbl(dr("TOTAL_COST"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function
    Public Shared Function CalculateMRPDetail(ByVal BOMCode As String, ByVal UnitCode As String, ByVal PackSize As String, ByVal ProduceQty As Decimal, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPDetail)
        Dim qryInnerBom As String
        Dim qryInnerPO As String
        Dim qryInnerSRN As String
        Dim qryInnerOB As String
        Dim qry As String
        '" & clsProductionPlanning.GetUnitConversion(BOMCode, UnitCode, PackSize) & "
        'qryInnerBom = " SELECT CONSM_ITEM_CODE AS ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,((CONSM_QUANTITY/(case when PROD_QUANTITY=0 then 1 " & _
        '      " else PROD_QUANTITY end))* " & ProduceQty & ") AS Total_Requird_Qty ,CONSM_ITEM_UNIT_CODE AS RM_UNIT_CODE " & _
        '      " FROM PROD_BOM_RM inner join TSPL_ITEM_MASTER on PROD_BOM_RM.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " & _
        '      " WHERE BOM_CODE='" & BOMCode & "'"

        qryInnerBom = " SELECT CONSM_ITEM_CODE AS ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,((CONSM_QUANTITY/(case when PROD_QUANTITY=0 then 1 " &
              " else PROD_QUANTITY end))* " & ProduceQty & ") AS Total_Requird_Qty ,CONSM_ITEM_UNIT_CODE AS RM_UNIT_CODE " &
              " FROM TSPL_MF_BOM_detail left join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_detail.BOM_CODE
                inner join TSPL_ITEM_MASTER on TSPL_MF_BOM_detail.CONSM_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
              " WHERE TSPL_MF_BOM_HEAD.BOM_CODE='" & BOMCode & "'"

        qryInnerPO = "select TSPL_PURCHASE_ORDER_DETAIL.Item_Code,sum(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty) as PO_Qty " & _
        " from TSPL_PURCHASE_ORDER_HEAD left join TSPL_SRN_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_SRN_HEAD.Against_PO " & _
        " inner join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
        " where TSPL_SRN_HEAD.Against_PO is null and TSPL_PURCHASE_ORDER_DETAIL.Item_Code in " & _
        " (select CONSM_ITEM_CODE from TSPL_MF_BOM_DETAIL where BOM_CODE='" & BOMCode & "') " & _
        " group by TSPL_PURCHASE_ORDER_DETAIL.Item_Code"

        qryInnerSRN = "select TSPL_SRN_DETAIL.Item_Code,sum(TSPL_SRN_DETAIL.SRN_Qty) as SRN_Qty from TSPL_SRN_HEAD " & _
        " inner join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No " & _
        " where  TSPL_SRN_DETAIL.Item_Code in (select CONSM_ITEM_CODE from TSPL_MF_BOM_DETAIL " & _
        " where BOM_CODE='" & BOMCode & "') and TSPL_SRN_HEAD.Status=0 " & _
        " group by TSPL_SRN_DETAIL.Item_Code"

        qryInnerOB = "SELECT Item_Code,(SUM(ITEM_BAL.IN_QTY)-SUM(ITEM_BAL.OUT_QTY)) AS Opening_Bal FROM (" & _
              " SELECT InOut,Item_Code,SUM(CASE WHEN InOut='I' THEN QTY ELSE 0 END) AS IN_QTY, " & _
              " SUM(CASE WHEN InOut='O' THEN QTY ELSE 0 END) AS OUT_QTY FROM TSPL_INVENTORY_MOVEMENT " & _
              " where Item_Code in (select CONSM_ITEM_CODE from TSPL_MF_BOM_DETAIL where BOM_CODE='" & BOMCode & "') " & _
              " GROUP BY InOut,Item_Code ) AS ITEM_BAL GROUP BY Item_Code "

        qry = " select RM.*,COALESCE(ITEM_BAL.Opening_Bal,0) AS Opening_Bal,COALESCE(ITEM_PO.PO_Qty,0) AS PO_Qty,COALESCE(ITEM_SRN.SRN_QTY,0) AS SRN_QTY  FROM (" & qryInnerBom & ") AS RM " & _
              " LEFT JOIN ( " & qryInnerOB & ") AS ITEM_BAL ON RM.ITEM_CODE=ITEM_BAL.Item_Code" & _
              " LEFT JOIN ( " & qryInnerPO & ") AS ITEM_PO ON RM.ITEM_CODE=ITEM_PO.Item_Code" & _
              " LEFT JOIN ( " & qryInnerSRN & ") AS ITEM_SRN ON RM.ITEM_CODE=ITEM_SRN.Item_Code"

        Dim dtRM As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsMRPDetail
        Dim ObjList As New List(Of clsMRPDetail)
        For Each drRM As DataRow In dtRM.Rows
            objtr = New clsMRPDetail()
            'objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
            objtr.Item_Code = clsCommon.myCstr(drRM("Item_Code"))
            objtr.Item_Desc = clsCommon.myCstr(drRM("Item_Desc"))
            objtr.RM_UNIT_CODE = clsCommon.myCstr(drRM("RM_UNIT_CODE"))
            objtr.Total_Requird_Qty = clsCommon.myCdbl(drRM("Total_Requird_Qty"))
            objtr.Opening_Qty = clsCommon.myCdbl(drRM("Opening_Bal"))
            objtr.PO_Qty = clsCommon.myCdbl(drRM("PO_Qty"))
            objtr.SRN_Qty = clsCommon.myCdbl(drRM("SRN_Qty"))

            ObjList.Add(objtr)
        Next
        Return ObjList
    End Function
End Class

Public Class clsMRPPO
#Region "Variables"
    
    Public MRP_CODE As String
    Public PurchaseOrder_No As String
    Public Bill_To_Location As String
    Public Vendor_Code As String
    Public PurchaseOrder_Date As Date
    Public Vendor_Name As String

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPPO), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMRPPO In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No)
                clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Date", clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
                
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_PO_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MRP_PO_DETAIL.MRP_CODE='" + strDocNo + "' ", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetMRPPO(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPPO)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_MRP_PO_DETAIL where MRP_CODE='" & MRP_CODE & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsMRPPO
        Dim ObjList As New List(Of clsMRPPO)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMRPPO()

                objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                objtr.PurchaseOrder_No = clsCommon.myCstr(dr("PurchaseOrder_No"))
                objtr.Bill_To_Location = clsCommon.myCstr(dr("Bill_To_Location"))
                objtr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                objtr.PurchaseOrder_Date = clsCommon.myCDate(dr("PurchaseOrder_Date"))
                objtr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function
    Public Shared Function GetPO(ByVal ItemCode As String, ByVal BOMCode As String) As DataTable
        Dim qry As String = ""
        qry = "select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date," & _
           " TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Name," & _
           " TSPL_SRN_HEAD.Against_PO from TSPL_PURCHASE_ORDER_HEAD left join TSPL_SRN_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_SRN_HEAD.Against_PO " & _
           " inner join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
           " where TSPL_SRN_HEAD.Against_PO is null and TSPL_PURCHASE_ORDER_DETAIL.Item_Code in (select CONSM_ITEM_CODE from TSPL_MF_BOM_DETAIL where BOM_CODE='" & BOMCode & "')"
        Dim dtPO As DataTable
        dtPO = clsDBFuncationality.GetDataTable(qry)
        Return dtPO
    End Function

End Class

Public Class clsMRPSRN
#Region "Variables"

    Public MRP_CODE As String
    Public SRN_No As String
    Public Bill_To_Location As String
    Public Vendor_Code As String
    Public SRN_Date As Date
    Public Vendor_Name As String

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPSRN), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMRPSRN In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No)
                clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "SRN_Date", clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_SRN_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MRP_SRN_DETAIL.MRP_CODE='" + strDocNo + "' ", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetMRPSRN(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPSRN)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_MRP_SRN_DETAIL where MRP_CODE='" & MRP_CODE & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsMRPSRN
        Dim ObjList As New List(Of clsMRPSRN)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMRPSRN()

                objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                objtr.SRN_No = clsCommon.myCstr(dr("SRN_No"))
                objtr.Bill_To_Location = clsCommon.myCstr(dr("Bill_To_Location"))
                objtr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                objtr.SRN_Date = clsCommon.myCDate(dr("SRN_Date"))
                objtr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function
    Public Shared Function GetSRN(ByVal ItemCode As String, ByVal BOMCode As String) As DataTable
        Dim qry As String = ""
        qry = "select TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date,TSPL_SRN_HEAD.Bill_To_Location,TSPL_SRN_HEAD.Vendor_Code, " & _
        " TSPL_SRN_HEAD.Vendor_Name,TSPL_SRN_HEAD.Against_PO from TSPL_SRN_HEAD inner join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No" & _
        " where  TSPL_SRN_DETAIL.Item_Code in (select CONSM_ITEM_CODE from TSPL_MF_BOM_DETAIL where BOM_CODE='" & BOMCode & "') and TSPL_SRN_HEAD.Status=0"

        Dim dtSRN As DataTable
        dtSRN = clsDBFuncationality.GetDataTable(qry)
        Return dtSRN
    End Function
End Class

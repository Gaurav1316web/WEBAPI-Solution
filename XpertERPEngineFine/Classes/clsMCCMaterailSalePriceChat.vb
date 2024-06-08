Imports System.Data.SqlClient


Public Class clsMCCMaterailSalePriceChat
#Region "Variables"
    Public Code As String = ""
    Public DOCDate As DateTime = Nothing
    Public Effective_Date As DateTime = Nothing
    Public Arr As List(Of clsMCCMaterailSalePriceChatDetail) = Nothing
    Public ArrMCCRate As ArrayList = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsMCCMaterailSalePriceChat, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsMCCMaterailSalePriceChat, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_MCC_RATE_UPLOADER_master", "Code", "TSPL_MCC_RATE_UPLOADER_Detail", "Code", "TSPL_MCC_RATE_UPLOADER_MCC", "Code", trans)

            End If
            Dim StrQry As String = "delete from TSPL_MCC_RATE_UPLOADER_Detail where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)
            StrQry = "delete from TSPL_MCC_RATE_UPLOADER_MCC where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(obj.DOCDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Effective_date", clsCommon.GetPrintDate(obj.Effective_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.DOCDate, clsDocType.FrmMCCMaterialSalePriceChart, "", "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_MASTER", OMInsertOrUpdate.Insert, "", trans)

            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_MASTER", OMInsertOrUpdate.Update, "TSPL_MCC_RATE_UPLOADER_MASTER.code='" & obj.Code & "'", trans)
            End If
            clsMCCMaterailSalePriceChatDetail.SaveData(obj.Code, obj.Arr, trans)
            clsMCCRATEUPLOADERMCC.SaveData(obj.Code, obj.ArrMCCRate, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsMCCMaterailSalePriceChat
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMCCMaterailSalePriceChat
        Dim obj As clsMCCMaterailSalePriceChat = Nothing
        Dim qry = "SELECT  TSPL_MCC_RATE_UPLOADER_MASTER.*  FROM TSPL_MCC_RATE_UPLOADER_MASTER where 2=2 "
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MCC_RATE_UPLOADER_MASTER.Code = (select MIN(Document_No) from TSPL_MCC_RATE_UPLOADER_MASTER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MCC_RATE_UPLOADER_MASTER.Code = (select Max(Code) from TSPL_MCC_RATE_UPLOADER_MASTER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MCC_RATE_UPLOADER_MASTER.Code = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_MCC_RATE_UPLOADER_MASTER.Code = (select Min(Code) from TSPL_MCC_RATE_UPLOADER_MASTER where Code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MCC_RATE_UPLOADER_MASTER.Code = (select Max(Code) from TSPL_MCC_RATE_UPLOADER_MASTER where Code<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMCCMaterailSalePriceChat()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.DOCDate = clsCommon.myCDate(dt.Rows(0)("date"))
            obj.Effective_Date = clsCommon.myCDate(dt.Rows(0)("Effective_Date"))

            qry = "select tspl_item_master.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],TSPL_UNIT_MASTER. unit_code as [Unit Code]" _
        & " ,Unit_Desc as [Unit Desc],Price,1 as Issaved from tspl_item_master left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=tspl_item_master.item_code" _
        & " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code left join TSPL_MCC_RATE_UPLOADER_Detail on " _
        & " TSPL_MCC_RATE_UPLOADER_Detail.item_code=TSPL_ITEM_MASTER.item_code and TSPL_MCC_RATE_UPLOADER_Detail.rate_uom=TSPL_UNIT_MASTER.Unit_Code " _
        & " where code='" & obj.Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMCCMaterailSalePriceChatDetail)
                Dim objTr As clsMCCMaterailSalePriceChatDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMCCMaterailSalePriceChatDetail
                    objTr.Item_Code = clsCommon.myCstr(dr("Item Code"))
                    objTr.Item_Name = clsCommon.myCstr(dr("Item Name"))
                    objTr.Rate_UOM = clsCommon.myCstr(dr("Unit Code"))
                    objTr.Unit_DESC = clsCommon.myCstr(dr("Unit Desc"))
                    objTr.Price = clsCommon.myCstr(dr("Price"))
                    objTr.issaved = clsCommon.myCstr(dr("Issaved"))

                    obj.Arr.Add(objTr)
                Next
            End If
            qry = "select MCC_Code from TSPL_MCC_RATE_UPLOADER_MCC where code='" + obj.Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrMCCRate = New ArrayList()

                For Each dr As DataRow In dt.Rows
                    obj.ArrMCCRate.Add(clsCommon.myCstr(dr("MCC_Code")))
                Next
            End If
        End If
            Return obj
    End Function

End Class
Public Class clsMCCMaterailSalePriceChatDetail
#Region "Variable"
    Public Item_Code As String = ""
    Public Item_Name As String = ""
    Public Rate_UOM As String = ""
    Public Unit_DESC As String = ""
    Public Price As Double = 0
    Public issaved As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMCCMaterailSalePriceChatDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMCCMaterailSalePriceChatDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Rate_UOM", obj.Rate_UOM)
                clsCommon.AddColumnsForChange(coll, "Price", obj.Price)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
Public Class clsMCCRATEUPLOADERMCC
#Region "Variable"
    Public mcc_code As String = ""

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strmcc As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "mcc_code", strmcc)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_RATE_UPLOADER_MCC", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class


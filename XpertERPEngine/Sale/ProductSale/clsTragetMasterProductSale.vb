
Imports common
Imports System.Data.SqlClient
Public Class clsTragetMasterHeadProductSale
#Region "Variables"
    Public Target_Code As String = Nothing
    Public Traget_Desc As String = Nothing
    Public Traget_Type As String = Nothing
    Public FromDate As Date = Nothing
    Public ToDate As Date = Nothing
    Public Form_ID As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public UOM As String = Nothing
    Public Quantity As Double = Nothing
    Public SchemeQty As Double = Nothing
    Public Criteria As String = Nothing
    Public Criteria_Code As String = Nothing
    Public Criteria_Desc As String = Nothing
    Public DocumentDate As Date = Nothing
    Public ArrDTL As List(Of clsTragetMasterDetailProductSale) = Nothing
    Public ArrSchm As List(Of clsTragetMasterScheme) = Nothing


#End Region
    Public Function SaveData(ByVal obj As clsTragetMasterHeadProductSale, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsTragetMasterHeadProductSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_TARGET_MASTER_Detail WHERE Target_Code ='" + obj.Target_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_Target_Master_Scheme WHERE Target_Code ='" + obj.Target_Code + "'", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Target_Desc", obj.Traget_Desc)
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.ToDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Target_Type", obj.Traget_Type)
            clsCommon.AddColumnsForChange(coll, "Criteria", obj.Criteria)
            clsCommon.AddColumnsForChange(coll, "Criteria_Code", obj.Criteria_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "DocumentDate", clsCommon.GetPrintDate(obj.DocumentDate, "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.Target_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Target_Code) from TSPL_TARGET_MASTER_HEAD", trans))
                If clsCommon.myLen(obj.Target_Code) <= 0 Then
                    obj.Target_Code = "TR000001"
                Else
                    obj.Target_Code = clsCommon.incval(obj.Target_Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Target_Code", obj.Target_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TARGET_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TARGET_MASTER_HEAD", OMInsertOrUpdate.Update, "Target_Code='" + obj.Target_Code + "'", trans)
            End If

            isSaved = isSaved AndAlso clsTragetMasterDetailProductSale.SaveData(obj.Target_Code, obj.ArrDTL, trans)
            isSaved = isSaved AndAlso clsTragetMasterScheme.SaveData(obj.Target_Code, obj.ArrSchm, trans)

            'isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Scheme_Code, obj.arrCustomFields, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function fundelete(ByVal strTargetCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsTragetMasterHeadProductSale
            If clsCommon.myLen(strTargetCode) > 0 Then
                obj = clsTragetMasterHeadProductSale.GetData(strTargetCode, NavigatorType.Current, trans)
            Else
                Throw New Exception("Document not found to delete.")
            End If
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_Target_Master_Scheme Where Target_Code='" + strTargetCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_TARGET_MASTER_Detail Where Target_Code='" + strTargetCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_TARGET_MASTER_HEAD Where Target_Code='" + strTargetCode + "'", trans)
            clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Target_Code, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function


    Public Shared Function GetData(ByVal strTragetCode As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsTragetMasterHeadProductSale
        Dim obj As clsTragetMasterHeadProductSale = Nothing
        Dim qry As String = "select TSPL_TARGET_MASTER_HEAD.Target_Code ,TSPL_TARGET_MASTER_HEAD.Target_Desc ,TSPL_TARGET_MASTER_HEAD.Target_Type ,TSPL_TARGET_MASTER_HEAD.From_Date ,TSPL_TARGET_MASTER_HEAD.To_Date,TSPL_TARGET_MASTER_HEAD.Criteria ,TSPL_TARGET_MASTER_HEAD.Criteria_Code,Case When Criteria='Customer Group' Then TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc When Criteria='Customer Category' then TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC Else '' End as Criteria_Desc ,TSPL_TARGET_MASTER_Detail.LINE_NO  ,TSPL_TARGET_MASTER_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_TARGET_MASTER_Detail.Unit_Code ,TSPL_TARGET_MASTER_Detail.Qty ,TSPL_TARGET_MASTER_Detail.Scheme_Qty,TSPL_TARGET_MASTER_Detail.SchemeItem_Code,TSPL_TARGET_MASTER_Detail.SchemeQty,TSPL_TARGET_MASTER_Detail.SchemeUOM,TSPL_TARGET_MASTER_Detail.To_Qty,TSPL_TARGET_MASTER_HEAD.DocumentDate  from TSPL_TARGET_MASTER_HEAD"
        qry += " left outer join TSPL_TARGET_MASTER_Detail on TSPL_TARGET_MASTER_Detail.Target_Code =TSPL_TARGET_MASTER_HEAD.Target_Code "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_TARGET_MASTER_Detail.Item_Code "
        qry += " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_TARGET_MASTER_HEAD.Criteria_Code"
        qry += " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_TARGET_MASTER_HEAD.Criteria_Code  where  2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_TARGET_MASTER_HEAD.Target_Code=(select MIN(Target_Code) from TSPL_TARGET_MASTER_HEAD Where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_TARGET_MASTER_HEAD.Target_Code=(select Max(Target_Code) from TSPL_TARGET_MASTER_HEAD Where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_TARGET_MASTER_HEAD.Target_Code=(select Min(Target_Code) from TSPL_TARGET_MASTER_HEAD where Target_Code > '" + strTragetCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_TARGET_MASTER_HEAD.Target_Code=(select Max(Target_Code) from TSPL_TARGET_MASTER_HEAD where Target_Code < '" + strTragetCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_TARGET_MASTER_HEAD.Target_Code='" + strTragetCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTragetMasterHeadProductSale()
            obj.Target_Code = clsCommon.myCstr(dt.Rows(0)("Target_Code"))
            obj.Traget_Desc = clsCommon.myCstr(dt.Rows(0)("Target_Desc"))
            obj.FromDate = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.ToDate = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Traget_Type = clsCommon.myCstr(dt.Rows(0)("Target_Type"))
         
            obj.Criteria = clsCommon.myCstr(dt.Rows(0)("Criteria"))
            obj.Criteria_Code = clsCommon.myCstr(dt.Rows(0)("Criteria_Code"))
            obj.Criteria_Desc = clsCommon.myCstr(dt.Rows(0)("Criteria_Desc"))
            obj.DocumentDate = clsCommon.myCDate(dt.Rows(0)("DocumentDate"))

            qry = "select TSPL_TARGET_MASTER_Detail.Target_Code ,TSPL_TARGET_MASTER_Detail.LINE_NO ,TSPL_TARGET_MASTER_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_TARGET_MASTER_Detail.Unit_Code,TSPL_TARGET_MASTER_Detail.Qty ,TSPL_TARGET_MASTER_Detail.Scheme_Qty,TSPL_TARGET_MASTER_Detail.SchemeItem_Code,TSPL_TARGET_MASTER_Detail.SchemeQty,TSPL_TARGET_MASTER_Detail.SchemeUOM,TSPL_TARGET_MASTER_Detail.To_Qty,SchemeItem.Item_Desc as SchemeItemDesc   from TSPL_TARGET_MASTER_Detail "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_TARGET_MASTER_Detail.Item_Code   left outer join TSPL_ITEM_MASTER as SchemeItem on SchemeItem.Item_Code =TSPL_TARGET_MASTER_Detail.SchemeItem_Code  WHERE TSPL_TARGET_MASTER_Detail.Target_Code = '" + obj.Target_Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrDTL = New List(Of clsTragetMasterDetailProductSale)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsTragetMasterDetailProductSale
                For Each dr As DataRow In dt.Rows
                    objTr = New clsTragetMasterDetailProductSale()
                    objTr.LineNo = clsCommon.myCstr(dr("LINE_NO"))
                    objTr.TragetCode = clsCommon.myCstr(dr("Target_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.UOM = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Quantity = clsCommon.myCdbl(dr("Qty"))
                    objTr.SchemeQty = clsCommon.myCdbl(dr("Scheme_Qty"))

                    objTr.To_Qty = clsCommon.myCdbl(dr("To_Qty"))
                    objTr.SchemeUOM = clsCommon.myCstr(dr("SchemeUOM"))
                    objTr.SchemeItem_Code = clsCommon.myCstr(dr("SchemeItem_Code"))
                    objTr.Scheme_Qty = clsCommon.myCdbl(dr("SchemeQty"))
                    objTr.SchemeItem_desc = clsCommon.myCstr(dr("SchemeItemDesc"))

                    obj.ArrDTL.Add(objTr)
                Next
            End If

            If clsCommon.CompairString(obj.Criteria, "Customer Group") = CompairStringResult.Equal Then
                qry = "Select Cast(Case When ISNULL(XXX.Cust_Code,'')<>'' Then 1 Else 0 End as Bit) As [Select], XXX.Target_Code, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN ( Select TSPL_Target_Master_Scheme.Cust_Code, TSPL_Target_Master_Scheme.Target_Code from TSPL_Target_Master_Scheme WHERE Target_Code='" + obj.Target_Code + "') XXX ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code WHERE TSPL_CUSTOMER_MASTER.Cust_Group_Code='" + obj.Criteria_Code + "'"
            ElseIf clsCommon.CompairString(obj.Criteria, "Customer Category") = CompairStringResult.Equal Then
                qry = "Select Cast(Case When ISNULL(XXX.Cust_Code,'')<>'' Then 1 Else 0 End as Bit) As [Select], XXX.Target_Code, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN ( Select TSPL_Target_Master_Scheme.Cust_Code, TSPL_Target_Master_Scheme.Target_Code from TSPL_Target_Master_Scheme WHERE Target_Code='" + obj.Target_Code + "') XXX ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code WHERE TSPL_CUSTOMER_MASTER.Cust_Category_Code='" + obj.Criteria_Code + "'"
            Else
                qry = "Select Cast(1 as bit) as [Select], TSPL_Target_Master_Scheme.Target_Code, TSPL_Target_Master_Scheme.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_Target_Master_Scheme LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Target_Master_Scheme.Cust_Code WHERE TSPL_Target_Master_Scheme.Target_Code = '" + obj.Target_Code + "'"
            End If

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrSchm = New List(Of clsTragetMasterScheme)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsTragetMasterScheme
                For Each dr As DataRow In dt.Rows
                    objTr = New clsTragetMasterScheme()
                    objTr.Target_Code = clsCommon.myCstr(dr("Target_Code"))
                    objTr.check = clsCommon.myCdbl(dr("Select"))
                    objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Customer_Name = clsCommon.myCstr(dr("Customer_Name"))
                    obj.ArrSchm.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

End Class
Public Class clsTragetMasterDetailProductSale
#Region "Variables"
    Public TragetCode As String = Nothing
    Public Item_Code As String = Nothing
    Public LineNo As String = Nothing
    Public Item_Desc As String = Nothing
    Public UOM As String = Nothing
    Public Quantity As Double = Nothing
    Public SchemeQty As Double = Nothing
    Public SchemeItem_Code As String = Nothing
    Public SchemeItem_desc As String = Nothing
    Public Scheme_Qty As Double = Nothing
    Public SchemeUOM As String = Nothing
    Public To_Qty As Double = Nothing
#End Region

    Public Shared Function SaveData(ByVal strTragetCode As String, ByVal Arr As List(Of clsTragetMasterDetailProductSale), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTragetMasterDetailProductSale In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Target_Code", strTragetCode)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Quantity)
                clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.SchemeQty)
                clsCommon.AddColumnsForChange(coll, "SchemeItem_Code", obj.SchemeItem_Code)
                clsCommon.AddColumnsForChange(coll, "SchemeQty", obj.Scheme_Qty)
                clsCommon.AddColumnsForChange(coll, "SchemeUOM", obj.SchemeUOM)
                clsCommon.AddColumnsForChange(coll, "To_Qty", obj.To_Qty)
                clsCommon.AddColumnsForChange(coll, "SchemeItem_Desc", obj.SchemeItem_desc)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TARGET_MASTER_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class

Public Class clsTragetMasterScheme
#Region "Variables"
    Public Target_Code As String = Nothing
    Public check As Integer = 0
    Public Cust_Code As String
    Public Customer_Name As String
#End Region

    Public Shared Function SaveData(ByVal strTragetCode As String, ByVal Arr As List(Of clsTragetMasterScheme), ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTragetMasterScheme In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Target_Code", strTragetCode)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Target_Master_Scheme", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class

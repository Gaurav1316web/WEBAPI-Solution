Imports common
Imports System.Data.SqlClient

Public Class clsSchemeMaster
#Region "Variables"
    Public Scheme_Code As String = Nothing
    Public Scheme_Desc As String = Nothing
    Public Start_Date As Date
    Public End_Date As Date?
    Public Scheme_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Unit_Desc As String = Nothing
    Public Item_Qty As Double = 0.0
    Public MRP As Double = 0.0
    Public Basic_Price As Double = 0.0
    Public Amount As Double = 0.0
    Public Percentage As Double = 0.0
    Public Status As String = Nothing
    Public Criteria As String = Nothing
    Public Criteria_Code As String = Nothing
    Public Criteria_Desc As String = Nothing
    Public Comments As String = Nothing
    Public ArrDTL As List(Of clsSchemeDettail) = Nothing
    Public ArrSchmBen As List(Of clsSchemeBenificiary) = Nothing
    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_SCHEME_MASTER_NEW.Scheme_Code as [Code] ,TSPL_SCHEME_MASTER_NEW.Scheme_Desc as [Scheme Description] ,TSPL_SCHEME_MASTER_NEW.Start_Date as [Start Date] ,TSPL_SCHEME_MASTER_NEW.End_Date as [End Date] ,TSPL_SCHEME_MASTER_NEW.Scheme_Type as [Scheme Type] ,TSPL_SCHEME_MASTER_NEW.Item_Code +' - '+TSPL_ITEM_MASTER.Item_Desc as Item ,TSPL_SCHEME_MASTER_NEW.Unit_Code as [Unit Code] ,TSPL_SCHEME_MASTER_NEW.Item_Qty as [Item Qty] ,TSPL_SCHEME_MASTER_NEW.MRP as [MRP] ,TSPL_SCHEME_MASTER_NEW.Basic_Price as [Basic Price] ,TSPL_SCHEME_MASTER_NEW.Percentage as [Percentage] ,TSPL_SCHEME_MASTER_NEW.Amount as [Amount] ,TSPL_SCHEME_MASTER_NEW.Status as [Status] ,TSPL_SCHEME_MASTER_NEW.Criteria as [Criteria] ,TSPL_SCHEME_MASTER_NEW.Criteria_Code as [Criteria Code] ,TSPL_SCHEME_MASTER_NEW.Comments as [Comments] ,TSPL_SCHEME_MASTER_NEW.Created_By as [Created By] ,TSPL_SCHEME_MASTER_NEW.Created_Date as [Created Date] ,TSPL_SCHEME_MASTER_NEW.Modify_By as [Modify By] ,TSPL_SCHEME_MASTER_NEW.Modify_Date as [Modify Date] ,TSPL_SCHEME_MASTER_NEW.Comp_Code as [Company Code]  From TSPL_SCHEME_MASTER_NEW Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SCHEME_MASTER_NEW.Item_Code   "
        str = clsCommon.ShowSelectForm("SCHMMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Function SaveData(ByVal obj As clsSchemeMaster, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsSchemeMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_DETAIL_NEW WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code ='" + obj.Scheme_Code + "'", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Scheme_Desc", obj.Scheme_Desc)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Qty", obj.Item_Qty)
            clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
            clsCommon.AddColumnsForChange(coll, "Basic_Price", obj.Basic_Price)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Percentage", obj.Percentage)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            If clsCommon.CompairString(obj.Status, "InActive") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Criteria", obj.Criteria)
            clsCommon.AddColumnsForChange(coll, "Criteria_Code", obj.Criteria_Code)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                obj.Scheme_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Scheme_Code) from TSPL_SCHEME_MASTER_NEW", trans))
                If clsCommon.myLen(obj.Scheme_Code) <= 0 Then
                    obj.Scheme_Code = "SCHM000001"
                Else
                    obj.Scheme_Code = clsCommon.incval(obj.Scheme_Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW", OMInsertOrUpdate.Update, "Scheme_Code='" + obj.Scheme_Code + "'", trans)
            End If

            isSaved = isSaved AndAlso clsSchemeDettail.SaveData(obj.Scheme_Code, obj.ArrDTL, obj.Scheme_Type, trans)
            isSaved = isSaved AndAlso clsSchemeBenificiary.SaveData(obj.Scheme_Code, obj.ArrSchmBen, obj.Scheme_Type, trans)

            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Scheme_Code, obj.arrCustomFields, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strSchemeCode As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsSchemeMaster
        Dim obj As clsSchemeMaster = Nothing
        Dim qry As String = "Select Scheme_Code, Scheme_Desc, Start_Date, End_Date, Scheme_Type, TSPL_SCHEME_MASTER_NEW.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_SCHEME_MASTER_NEW.Unit_Code, TSPL_UNIT_MASTER.Unit_Desc," & _
                    " Item_Qty, MRP, Basic_Price, Percentage, Amount, Status, Criteria, Criteria_Code, Case When Criteria='Customer Group' Then TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc When Criteria='Customer Category' then TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC Else '' End as Criteria_Desc," & _
                    " Comments  from TSPL_SCHEME_MASTER_NEW LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SCHEME_MASTER_NEW.Item_Code" & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_SCHEME_MASTER_NEW.Criteria_Code" & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_SCHEME_MASTER_NEW.Criteria_Code" & _
                    " LEFT OUTER JOIN TSPL_UNIT_MASTER ON TSPL_UNIT_MASTER.Unit_Code=TSPL_SCHEME_MASTER_NEW.Unit_Code where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code=(select MIN(Scheme_Code) from TSPL_SCHEME_MASTER_NEW Where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code=(select Max(Scheme_Code) from TSPL_SCHEME_MASTER_NEW Where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code=(select Min(Scheme_Code) from TSPL_SCHEME_MASTER_NEW where Scheme_Code > '" + strSchemeCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code=(select Max(Scheme_Code) from TSPL_SCHEME_MASTER_NEW where Scheme_Code < '" + strSchemeCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code='" + strSchemeCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSchemeMaster()
            obj.Scheme_Code = clsCommon.myCstr(dt.Rows(0)("Scheme_Code"))
            obj.Scheme_Desc = clsCommon.myCstr(dt.Rows(0)("Scheme_Desc"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            obj.Scheme_Type = clsCommon.myCstr(dt.Rows(0)("Scheme_Type"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            obj.Unit_Desc = clsCommon.myCstr(dt.Rows(0)("Unit_Desc"))
            obj.Item_Qty = clsCommon.myCdbl(dt.Rows(0)("Item_Qty"))
            obj.MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))
            obj.Basic_Price = clsCommon.myCdbl(dt.Rows(0)("Basic_Price"))
            obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
            obj.Percentage = clsCommon.myCdbl(dt.Rows(0)("Percentage"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            If clsCommon.CompairString(obj.Status, "InActive") = CompairStringResult.Equal Then
                obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            End If
            obj.Criteria = clsCommon.myCstr(dt.Rows(0)("Criteria"))
            obj.Criteria_Code = clsCommon.myCstr(dt.Rows(0)("Criteria_Code"))
            obj.Criteria_Desc = clsCommon.myCstr(dt.Rows(0)("Criteria_Desc"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))

            qry = "Select Scheme_Code, TSPL_SCHEME_DETAIL_NEW.Item_Code, TSPL_ITEM_MASTER.Item_Desc, Qty, TSPL_SCHEME_DETAIL_NEW.Unit_Code, MRP, Price_Date, Basic_Price, Remarks  from TSPL_SCHEME_DETAIL_NEW LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SCHEME_DETAIL_NEW.Item_Code WHERE Scheme_Code = '" + obj.Scheme_Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrDTL = New List(Of clsSchemeDettail)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsSchemeDettail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSchemeDettail()
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Basic_Price = clsCommon.myCdbl(dr("Basic_Price"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Price_Date = clsCommon.myCstr(dr("Price_Date"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    obj.ArrDTL.Add(objTr)
                Next
            End If

            If clsCommon.CompairString(obj.Criteria, "Customer Group") = CompairStringResult.Equal Then
                qry = "Select Cast(Case When ISNULL(XXX.Cust_Code,'')<>'' Then 1 Else 0 End as Bit) As [Select], XXX.Scheme_Code, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN ( Select TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_SCHEME_BENEFICIARY.Scheme_Code from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code='" + obj.Scheme_Code + "') XXX ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code WHERE TSPL_CUSTOMER_MASTER.Cust_Group_Code='" + obj.Criteria_Code + "'"
            ElseIf clsCommon.CompairString(obj.Criteria, "Customer Category") = CompairStringResult.Equal Then
                qry = "Select Cast(Case When ISNULL(XXX.Cust_Code,'')<>'' Then 1 Else 0 End as Bit) As [Select], XXX.Scheme_Code, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN ( Select TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_SCHEME_BENEFICIARY.Scheme_Code from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code='" + obj.Scheme_Code + "') XXX ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code WHERE TSPL_CUSTOMER_MASTER.Cust_Category_Code='" + obj.Criteria_Code + "'"
            Else
                qry = "Select Cast(1 as bit) as [Select], TSPL_SCHEME_BENEFICIARY.Scheme_Code, TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SCHEME_BENEFICIARY LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCHEME_BENEFICIARY.Cust_Code WHERE TSPL_SCHEME_BENEFICIARY.Scheme_Code = '" + obj.Scheme_Code + "'"
            End If

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            obj.ArrSchmBen = New List(Of clsSchemeBenificiary)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim objTr As clsSchemeBenificiary
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSchemeBenificiary()
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.check = clsCommon.myCdbl(dr("Select"))
                    objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Customer_Name = clsCommon.myCstr(dr("Customer_Name"))
                    obj.ArrSchmBen.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function fundelete(ByVal strSchemeCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsSchemeMaster
            If clsCommon.myLen(strSchemeCode) > 0 Then
                obj = clsSchemeMaster.GetData(strSchemeCode, NavigatorType.Current, trans)
            Else
                Throw New Exception("Document not found to delete.")
            End If
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_SCHEME_DETAIL_NEW Where Scheme_Code='" + strSchemeCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_SCHEME_BENEFICIARY Where Scheme_Code='" + strSchemeCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_SCHEME_MASTER_NEW Where Scheme_Code='" + strSchemeCode + "'", trans)
            clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Scheme_Code, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function



End Class

Public Class clsSchemeDettail
#Region "Variables"
    Public Scheme_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0.0
    Public Unit_Code As String = Nothing
    Public MRP As Double = 0.0
    Public Basic_Price As Double = 0.0
    Public Price_Date As Date
    Public Remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strSchemeCode As String, ByVal Arr As List(Of clsSchemeDettail), ByVal ReceiptType As String, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSchemeDettail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", strSchemeCode)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Basic_Price", obj.Basic_Price)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class

Public Class clsSchemeBenificiary
#Region "Variables"
    Public Scheme_Code As String = Nothing
    Public check As Integer = 0
    Public Cust_Code As String
    Public Customer_Name As String
#End Region

    Public Shared Function SaveData(ByVal strSchemeCode As String, ByVal Arr As List(Of clsSchemeBenificiary), ByVal ReceiptType As String, ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSchemeBenificiary In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", strSchemeCode)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class

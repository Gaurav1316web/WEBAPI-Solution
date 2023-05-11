Imports System.Data.SqlClient
Imports common

Public Class ClsServiceEnquiry

#Region "Variables"
    Public Service_Enquiry_Code As String = Nothing
    Public Service_Enquiry_Date As String = Nothing
    Public Cust_Group_Code As String = Nothing
    Public Dealer_Code As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public DateOfSale As String = Nothing
    Public EngineNo As String = Nothing
    Public Item_Part_No As String = Nothing
    Public Remarks As String = Nothing
    Public Issued_Notice As String = Nothing
   

    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public ObjList As List(Of ClsServiceEnquiryMainItem) = Nothing
    Dim objDetail As New ClsServiceEnquiryMainItem()
    Public ObjChildL As List(Of ClsServiceEnquiryChildItem) = Nothing
    Dim objChild As New ClsServiceEnquiryChildItem()
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT Service_Enquiry_Code AS [Code],CONVERT (VARCHAR,Service_Enquiry_Date ,103) AS [Document Date],Cust_Group_Code AS [Customer Group Code],Dealer_Code AS [Dealer Code],Vehicle_Code AS [Vehicle Code],CONVERT (VARCHAR,Date_Of_Sale,103) AS [Date Of Sale],ISNULL(Allocated,0) AS Allocated,Created_By AS [Created By],CONVERT(VARCHAR,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_SW_SERVICE_ENQUIRY  "
        str = clsCommon.ShowSelectForm("SWSerEnq", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of ClsServiceEnquiry)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            If ClsServiceEnquiry.SaveData(arr, trans, Nothing) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsServiceEnquiry), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsServiceEnquiry In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Enquiry_Date", clsCommon.GetPrintDate(obj.Service_Enquiry_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code, True)
                clsCommon.AddColumnsForChange(coll, "Date_Of_Sale", clsCommon.GetPrintDate(obj.DateOfSale, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Dealer_Code", obj.Dealer_Code, True)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code, True)
                clsCommon.AddColumnsForChange(coll, "EngineNo", obj.EngineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Part_No", obj.Item_Part_No, True)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Issued_Notice", obj.Issued_Notice, True)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_SW_SERVICE_ENQUIRY WHERE Service_Enquiry_Code='" + obj.Service_Enquiry_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_SW_SERVICE_ENQUIRY WHERE Service_Enquiry_Code= '" & obj.Service_Enquiry_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        obj.Service_Enquiry_Code = clsERPFuncationality.GetNextCode(trans, obj.Service_Enquiry_Date, clsDocType.SWServiceEnquiry, "", "")
                        clsCommon.AddColumnsForChange(coll, "Service_Enquiry_Code", obj.Service_Enquiry_Code)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_ENQUIRY", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")
                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_ENQUIRY", OMInsertOrUpdate.Update, "Service_Enquiry_Code='" + obj.Service_Enquiry_Code + "'", trans)
                End If

                ClsServiceEnquiryMainItem.SaveData(obj.Service_Enquiry_Code, obj.ObjList, trans)
                ClsServiceEnquiryChildItem.SaveData(obj.Service_Enquiry_Code, obj.ObjChildL, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsServiceEnquiry
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsServiceEnquiry
        Dim obj As ClsServiceEnquiry = Nothing

        Dim qry As String = "Select * From TSPL_SW_SERVICE_ENQUIRY where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SW_SERVICE_ENQUIRY.Service_Enquiry_Code = (select MIN(Service_Enquiry_Code) from TSPL_SW_SERVICE_ENQUIRY)"
            Case NavigatorType.Last
                qry += " and TSPL_SW_SERVICE_ENQUIRY.Service_Enquiry_Code = (select Max(Service_Enquiry_Code) from TSPL_SW_SERVICE_ENQUIRY)"
            Case NavigatorType.Next
                qry += " and TSPL_SW_SERVICE_ENQUIRY.Service_Enquiry_Code = (select Min(Service_Enquiry_Code) from TSPL_SW_SERVICE_ENQUIRY where  Service_Enquiry_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SW_SERVICE_ENQUIRY.Service_Enquiry_Code = (select Max(Service_Enquiry_Code) from TSPL_SW_SERVICE_ENQUIRY where Service_Enquiry_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SW_SERVICE_ENQUIRY.Service_Enquiry_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsServiceEnquiry()
            obj.Service_Enquiry_Code = clsCommon.myCstr(dt.Rows(0)("Service_Enquiry_Code"))
            obj.Service_Enquiry_Date = clsCommon.myCDate(dt.Rows(0)("Service_Enquiry_Date"))
            obj.Cust_Group_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Group_Code"))
            obj.Dealer_Code = clsCommon.myCstr(dt.Rows(0)("Dealer_Code"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.DateOfSale = clsCommon.myCDate(dt.Rows(0)("Date_Of_Sale"))
            obj.EngineNo = clsCommon.myCstr(dt.Rows(0)("EngineNo"))
            obj.Item_Part_No = clsCommon.myCstr(dt.Rows(0)("Item_Part_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Issued_Notice = clsCommon.myCstr(dt.Rows(0)("Issued_Notice"))

            obj.ObjList = ClsServiceEnquiryMainItem.GetData(obj.Service_Enquiry_Code, trans)

        End If
        Return obj
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

            qry = "Delete From TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM Where Service_Enquiry_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete From TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM Where Service_Enquiry_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete From TSPL_SW_SERVICE_ENQUIRY Where Service_Enquiry_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    ' ----------------- Get_Warranty_Status ------------------------
    Public Shared Function GetWarranty() As DataTable
        Dim DT_Warranty As DataTable = New DataTable
        DT_Warranty.Columns.Add("Code", GetType(String))
        DT_Warranty.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Warranty.NewRow()
        DR("Name") = "Under Warranty"
        DR("Code") = "UW"
        DT_Warranty.Rows.Add(DR)

        'DR = DT_Warranty.NewRow()
        'DR("Name") = "Out Of Warranty"
        'DR("Code") = "OW"

        DR = DT_Warranty.NewRow()
        DR("Name") = "NA"
        DR("Code") = "NA"

        DT_Warranty.Rows.Add(DR)
        DT_Warranty.AcceptChanges()

        Return DT_Warranty
    End Function
    ' ----------------- Get_Charge_Status ------------------------
    Public Shared Function GetCharge() As DataTable
        Dim DT_Charge As DataTable = New DataTable
        DT_Charge.Columns.Add("Code", GetType(String))
        DT_Charge.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Charge.NewRow()
        DR("Name") = "FOC"
        DR("Code") = "FOC"
        DT_Charge.Rows.Add(DR)

        DR = DT_Charge.NewRow()
        DR("Name") = "Chargable"
        DR("Code") = "C"
        DT_Charge.Rows.Add(DR)

        'DR = DT_Charge.NewRow()
        'DR("Name") = "Others"
        'DR("Code") = "O"

        'DT_Charge.Rows.Add(DR)
        DT_Charge.AcceptChanges()

        Return DT_Charge
    End Function
End Class


' ============================================== Service Enquiry Main Item ====================================================
Public Class ClsServiceEnquiryMainItem
#Region "Variables"
    Public Service_Enquiry_Code As String = String.Empty
    Public Main_Item_Code As String = String.Empty
    Public Child_Item_Code As String = String.Empty
    Public Main_Serial_No As String = String.Empty
    Public Serial_No As String = String.Empty
    Public Issued_Code As String = String.Empty
    Public Warranty_Status As String = Nothing
    Public Charge_Status As String = Nothing
    Public BOM_Revision_No As String = Nothing
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " DELETE FROM TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM WHERE Service_Enquiry_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjListChk As List(Of ClsServiceEnquiryMainItem), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM WHERE Service_Enquiry_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsServiceEnquiryMainItem In ObjListChk
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Enquiry_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Main_Item_Code", obj.Main_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Child_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Main_Serial_No", obj.Main_Serial_No)
                clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No)
                clsCommon.AddColumnsForChange(coll, "Issued_Code", obj.Issued_Code)
                clsCommon.AddColumnsForChange(coll, "Warranty_Status", obj.Warranty_Status)
                clsCommon.AddColumnsForChange(coll, "Charge_Status", obj.Charge_Status)
                clsCommon.AddColumnsForChange(coll, "BOM_Revision_No", obj.BOM_Revision_No)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsServiceEnquiryMainItem)
        Dim obj As ClsServiceEnquiryMainItem = Nothing
        Dim ObjListChk As New List(Of ClsServiceEnquiryMainItem)
        Dim qry As String = " select *  from TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM WHERE Service_Enquiry_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsServiceEnquiryMainItem()
                obj.Service_Enquiry_Code = clsCommon.myCstr(dr("Service_Enquiry_Code"))
                obj.Main_Item_Code = clsCommon.myCstr(dr("Main_Item_Code"))
                obj.Child_Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Main_Serial_No = clsCommon.myCstr(dr("Main_Serial_No"))
                obj.Serial_No = clsCommon.myCstr(dr("Serial_No"))
                obj.Issued_Code = clsCommon.myCstr(dr("Issued_Code"))
                obj.Warranty_Status = clsCommon.myCstr(dr("Warranty_Status"))
                obj.Charge_Status = clsCommon.myCstr(dr("Charge_Status"))
                obj.BOM_Revision_No = clsCommon.myCstr(dr("BOM_Revision_No"))
                ObjListChk.Add(obj)
            Next
        End If
        Return ObjListChk
    End Function
End Class
' ============================================== Service Enquiry Child Item ====================================================
Public Class ClsServiceEnquiryChildItem
#Region "Variables"
    Public Service_Enquiry_Code As String = String.Empty
    Public Main_Item_Code As String = String.Empty
    Public Serial_No As String = String.Empty
    Public Issued_Code As String = String.Empty
    Public Child_Item_Code As String = String.Empty
    Public Warranty_Status As String = Nothing
    Public Charge_Status As String = Nothing
    Public BOM_Revision_No As String = Nothing
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " DELETE FROM TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM WHERE Service_Enquiry_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjListChk As List(Of ClsServiceEnquiryChildItem), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM WHERE Service_Enquiry_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsServiceEnquiryChildItem In ObjListChk
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Enquiry_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Main_Item_Code", obj.Main_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Child_Item_Code", obj.Child_Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No)
                clsCommon.AddColumnsForChange(coll, "Issued_Code", obj.Issued_Code)
                clsCommon.AddColumnsForChange(coll, "Warranty_Status", obj.Warranty_Status)
                clsCommon.AddColumnsForChange(coll, "Charge_Status", obj.Charge_Status)
                clsCommon.AddColumnsForChange(coll, "BOM_Revision_No", obj.BOM_Revision_No)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_ENQUIRY_CHILD_ITEM", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsServiceEnquiryChildItem)
        Dim obj As ClsServiceEnquiryChildItem = Nothing
        Dim ObjListChk As New List(Of ClsServiceEnquiryChildItem)
        Dim qry As String = " select *  from TSPL_SW_SERVICE_ENQUIRY_MAIN_ITEM WHERE Service_Enquiry_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsServiceEnquiryChildItem()
                obj.Service_Enquiry_Code = clsCommon.myCstr(dr("Service_Enquiry_Code"))
                obj.Main_Item_Code = clsCommon.myCstr(dr("Main_Item_Code"))
                obj.Child_Item_Code = clsCommon.myCstr(dr("Child_Item_Code"))
                obj.Serial_No = clsCommon.myCstr(dr("Serial_No"))
                obj.Issued_Code = clsCommon.myCstr(dr("Issued_Code"))
                obj.Warranty_Status = clsCommon.myCstr(dr("Warranty_Status"))
                obj.Charge_Status = clsCommon.myCstr(dr("Charge_Status"))
                obj.BOM_Revision_No = clsCommon.myCstr(dr("BOM_Revision_No"))
                ObjListChk.Add(obj)
            Next
        End If
        Return ObjListChk
    End Function
End Class
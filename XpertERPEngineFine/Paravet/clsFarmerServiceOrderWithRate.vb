Imports common
Imports System.Data.SqlClient
Public Class clsFarmerServiceOrderHeader
#Region "Variables"
    Public Service_Order_No As String = Nothing
    Public Service_Order_Date As Date? = Nothing
    Public Service_Provider_Type_ID As String = Nothing
    Public Service_Provider_Name As String = Nothing
    Public HO As String = Nothing
    Public ZONE As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Region As String = Nothing
    Public Area As String = Nothing
    Public Branch As String = Nothing
    Public MCC As String = Nothing
    Public PMC As String = Nothing
    Public Farmer_Id As String = Nothing
    Public Cattle_Id As String = Nothing
    Public Staff As String = Nothing
    Public Bill_Amount As Double = 0
    Public Paid_Amount As Double = 0
    Public Comp_Code As String = Nothing
    Public Posting_Date As Date?
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
    Public Arr As List(Of clsFarmerServiceOrderDetails) = Nothing
#End Region


    Public Function SaveData(ByVal obj As clsFarmerServiceOrderHeader, ByVal isNewEntry As Boolean, ByVal Arr As List(Of clsFarmerServiceOrderDetails)) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete from TSPL_Farmer_Service_Order_Details where Service_Order_No='" + obj.Service_Order_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Service_Order_Date", clsCommon.GetPrintDate(obj.Service_Order_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Service_Provider_Type_ID", obj.Service_Provider_Type_ID)
            clsCommon.AddColumnsForChange(coll, "Service_Provider_Name", obj.Service_Provider_Name)
            clsCommon.AddColumnsForChange(coll, "HO", obj.HO)
            clsCommon.AddColumnsForChange(coll, "ZONE", obj.ZONE)
            clsCommon.AddColumnsForChange(coll, "Region", obj.Region)
            clsCommon.AddColumnsForChange(coll, "Area", obj.Area)
            clsCommon.AddColumnsForChange(coll, "Branch", obj.Branch)
            clsCommon.AddColumnsForChange(coll, "MCC", obj.MCC)
            clsCommon.AddColumnsForChange(coll, "PMC", obj.PMC)
            clsCommon.AddColumnsForChange(coll, "Farmer_Id", obj.Farmer_Id)
            clsCommon.AddColumnsForChange(coll, "Cattle_Id", obj.Cattle_Id)
            clsCommon.AddColumnsForChange(coll, "Staff", obj.Staff)
            clsCommon.AddColumnsForChange(coll, "Bill_Amount", obj.Bill_Amount)
            clsCommon.AddColumnsForChange(coll, "Paid_Amount", obj.Paid_Amount)
            'clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Service_Order_No", obj.Service_Order_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Farmer_Service_Order_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Farmer_Service_Order_Head", OMInsertOrUpdate.Update, "TSPL_Farmer_Service_Order_Head.Service_Order_No='" + obj.Service_Order_No + "'", trans)
            End If


            isSaved = isSaved AndAlso clsFarmerServiceOrderDetails.SaveData(obj.Service_Order_No, Arr, trans)


            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal Trans As SqlTransaction) As clsFarmerServiceOrderHeader
        Dim obj As clsFarmerServiceOrderHeader = Nothing
        Dim qry As String = " select Service_Order_No, convert (varchar, service_Order_Date,103) as service_Order_Date ,Service_Provider_Type_ID,Service_Provider_Name,Farmer_Id,isnull (Bill_Amount,0) as Bill_Amount ,isnull (Paid_Amount,0) as Paid_Amount,MCC,PMC,HO,ZONE,Region,Area,Branch from TSPL_Farmer_Service_Order_Head  where Service_Order_No = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsFarmerServiceOrderHeader
            obj.Service_Order_No = clsCommon.myCstr(dt.Rows(0)("Service_Order_No"))
            If dt.Rows(0)("service_Order_Date") IsNot DBNull.Value Then
                obj.Service_Order_Date = clsCommon.myCDate(dt.Rows(0)("service_Order_Date"))
            End If
            obj.Service_Provider_Type_ID = clsCommon.myCstr(dt.Rows(0)("Service_Provider_Type_ID"))
            obj.Service_Provider_Name = clsCommon.myCstr(dt.Rows(0)("Service_Provider_Name"))
            obj.Farmer_Id = clsCommon.myCstr(dt.Rows(0)("Farmer_Id"))
            obj.Bill_Amount = clsCommon.myCdbl(dt.Rows(0)("Bill_Amount"))
            obj.Paid_Amount = clsCommon.myCstr(dt.Rows(0)("Paid_Amount"))
            obj.MCC = clsCommon.myCstr(dt.Rows(0)("MCC"))
            obj.PMC = clsCommon.myCstr(dt.Rows(0)("PMC"))
            obj.HO = clsCommon.myCstr(dt.Rows(0)("HO"))
            obj.ZONE = clsCommon.myCstr(dt.Rows(0)("ZONE"))
            obj.Region = clsCommon.myCstr(dt.Rows(0)("Region"))
            obj.Area = clsCommon.myCstr(dt.Rows(0)("Area"))
            obj.Branch = clsCommon.myCstr(dt.Rows(0)("Branch"))

            qry = " select Service_Order_No,Service_Id,Group_Name,Service_Name,Cattle_Type_Code,Cattle_Type_Name,Breed_Type_Code,Breed_Type_Name,Cattle_Tag_Id,Service_Price from TSPL_Farmer_Service_Order_Details where  Service_Order_No ='" + strCode + "' order by Service_Id"
            dt = clsDBFuncationality.GetDataTable(qry, Trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsFarmerServiceOrderDetails)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As clsFarmerServiceOrderDetails = New clsFarmerServiceOrderDetails()
                    objtr.Service_Id = clsCommon.myCstr(dr("Service_Id"))
                    objtr.Group_Name = clsCommon.myCstr(dr("Group_Name"))
                    objtr.Service_Name = clsCommon.myCstr(dr("Service_Name"))
                    objtr.Cattle_Type_Code = clsCommon.myCstr(dr("Cattle_Type_Code"))
                    objtr.Cattle_Type_Name = clsCommon.myCstr(dr("Cattle_Type_Name"))
                    objtr.Breed_Type_Code = clsCommon.myCstr(dr("Breed_Type_Code"))
                    objtr.Breed_Type_Name = clsCommon.myCstr(dr("Breed_Type_Name"))
                    objtr.Cattle_Tag_Id = clsCommon.myCstr(dr("Cattle_Tag_Id"))
                    objtr.Service_Price = clsCommon.myCdbl(dr("Service_Price"))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry2 As String = "delete from TSPL_Farmer_Service_Order_Head where Service_Order_No='" & strDocNo & "'"
            Dim qry1 As String = "delete from TSPL_Farmer_Service_Order_Details where Service_Order_No='" & strDocNo & "'"
            Dim isDeleted As Boolean = True
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry2, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class
Public Class clsFarmerServiceOrderDetails
#Region "Variables"
    Public Service_Order_No As String = Nothing
    Public Service_Id As String = Nothing
    Public Group_Name As String = Nothing
    Public Service_Name As String = Nothing
    Public Cattle_Type_Code As String = Nothing
    Public Cattle_Type_Name As String = Nothing
    Public Breed_Type_Code As String = Nothing
    Public Breed_Type_Name As String = Nothing
    Public Cattle_Tag_Id As String = Nothing
    Public Service_Price As Double = 0
    Public Service_Status As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strOrderNo As String, ByVal Arr As List(Of clsFarmerServiceOrderDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsFarmerServiceOrderDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Order_No", strOrderNo)
                clsCommon.AddColumnsForChange(coll, "Service_Id", obj.Service_Id)
                clsCommon.AddColumnsForChange(coll, "Group_Name", obj.Group_Name)
                clsCommon.AddColumnsForChange(coll, "Service_Name", obj.Service_Name)
                clsCommon.AddColumnsForChange(coll, "Cattle_Type_Code", obj.Cattle_Type_Code)
                clsCommon.AddColumnsForChange(coll, "Cattle_Type_Name", obj.Cattle_Type_Name)
                clsCommon.AddColumnsForChange(coll, "Breed_Type_Code", obj.Breed_Type_Code)
                clsCommon.AddColumnsForChange(coll, "Breed_Type_Name", obj.Breed_Type_Name)
                clsCommon.AddColumnsForChange(coll, "Cattle_Tag_Id", obj.Cattle_Tag_Id)
                clsCommon.AddColumnsForChange(coll, "Service_Price", obj.Service_Price)
                clsCommon.AddColumnsForChange(coll, "Service_Status", obj.Service_Status)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Farmer_Service_Order_Details", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

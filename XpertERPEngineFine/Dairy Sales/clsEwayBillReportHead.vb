Imports System.Data.SqlClient
Imports Newtonsoft.Json

Public Class clsEwayBillReportHead
#Region "Variables"
    Public ewbno As String = ""
    Public ewayBillDate As String = ""
    Public genMode As String = ""
    Public userGstin As String = ""
    Public supplyType As String = ""
    Public subSupplyType As String = ""
    Public docType As String = ""
    Public docNo As String = ""
    Public docDate As String = ""
    Public fromGstin As String = ""
    Public fromTrdName As String = ""
    Public fromAddr1 As String = ""
    Public fromAddr2 As String = ""
    Public fromPlace As String = ""
    Public fromPincode As String = ""
    Public fromStateCode As String = ""
    Public toGstin As String = ""
    Public toTrdName As String = ""
    Public toAddr1 As String = ""
    Public toAddr2 As String = ""
    Public toPlace As String = ""
    Public toPincode As String = ""
    Public toStateCode As String = ""
    Public totalValue As String = ""
    Public totInvValue As String = ""
    Public cgstValue As String = ""
    Public sgstValue As String = ""
    Public igstValue As String = ""
    Public cessValue As String = ""
    Public transporterId As String = ""
    Public transporterName As String = ""
    Public status As String = ""
    Public actualDist As String = ""
    Public noValidDays As String = ""
    Public validUpto As String = ""
    Public extendedTimes As String = ""
    Public rejectStatus As String = ""
    Public vehicleType As String = ""
    Public actFromStateCode As String = ""
    Public actToStateCode As String = ""
    Public transactionType As String = ""
    Public otherValue As String = ""
    Public cessNonAdvolValue As String = ""
    Public ItemList As List(Of clsEwayBillItem) = Nothing
    Public VehiclListDetails As List(Of clsEwayBillVehicle) = Nothing
#End Region
    Public Shared Function SaveData(ByVal strewbJson As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(strewbJson, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal strewbJson As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ewayBill As clsEwayBillReportHead = JsonConvert.DeserializeObject(Of clsEwayBillReportHead)(strewbJson)
            If ewayBill IsNot Nothing Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ewbno", ewayBill.ewbno)
                clsCommon.AddColumnsForChange(coll, "ewayBillDate", ewayBill.ewayBillDate)
                clsCommon.AddColumnsForChange(coll, "genMode", ewayBill.genMode)
                clsCommon.AddColumnsForChange(coll, "userGstin", ewayBill.userGstin)
                clsCommon.AddColumnsForChange(coll, "supplyType", ewayBill.supplyType)
                clsCommon.AddColumnsForChange(coll, "subSupplyType", ewayBill.subSupplyType)
                clsCommon.AddColumnsForChange(coll, "docType", ewayBill.docType)
                clsCommon.AddColumnsForChange(coll, "docNo", ewayBill.docNo)
                clsCommon.AddColumnsForChange(coll, "docDate", ewayBill.docDate)
                clsCommon.AddColumnsForChange(coll, "fromGstin", ewayBill.fromGstin)
                clsCommon.AddColumnsForChange(coll, "fromTrdName", ewayBill.fromTrdName)
                clsCommon.AddColumnsForChange(coll, "fromAddr1", ewayBill.fromAddr1)
                clsCommon.AddColumnsForChange(coll, "fromAddr2", ewayBill.fromAddr2)
                clsCommon.AddColumnsForChange(coll, "fromPlace", ewayBill.fromPlace)
                clsCommon.AddColumnsForChange(coll, "fromPincode", ewayBill.fromPincode)
                clsCommon.AddColumnsForChange(coll, "fromStateCode", ewayBill.fromStateCode)
                clsCommon.AddColumnsForChange(coll, "toGstin", ewayBill.toGstin)
                clsCommon.AddColumnsForChange(coll, "toTrdName", ewayBill.toTrdName)
                clsCommon.AddColumnsForChange(coll, "toAddr1", ewayBill.toAddr1)
                clsCommon.AddColumnsForChange(coll, "toAddr2", ewayBill.toAddr2)
                clsCommon.AddColumnsForChange(coll, "toPlace", ewayBill.toPlace)
                clsCommon.AddColumnsForChange(coll, "toPincode", ewayBill.toPincode)
                clsCommon.AddColumnsForChange(coll, "toStateCode", ewayBill.toStateCode)
                clsCommon.AddColumnsForChange(coll, "totalValue", ewayBill.totalValue)
                clsCommon.AddColumnsForChange(coll, "totInvValue", ewayBill.totInvValue)
                clsCommon.AddColumnsForChange(coll, "cgstValue", ewayBill.cgstValue)
                clsCommon.AddColumnsForChange(coll, "sgstValue", ewayBill.sgstValue)
                clsCommon.AddColumnsForChange(coll, "igstValue", ewayBill.igstValue)
                clsCommon.AddColumnsForChange(coll, "cessValue", ewayBill.cessValue)
                clsCommon.AddColumnsForChange(coll, "transporterId", ewayBill.transporterId)
                clsCommon.AddColumnsForChange(coll, "transporterName", ewayBill.transporterName)
                clsCommon.AddColumnsForChange(coll, "status", ewayBill.status)
                clsCommon.AddColumnsForChange(coll, "actualDist", ewayBill.actualDist)
                clsCommon.AddColumnsForChange(coll, "noValidDays", ewayBill.noValidDays)
                clsCommon.AddColumnsForChange(coll, "validUpto", ewayBill.validUpto)
                clsCommon.AddColumnsForChange(coll, "extendedTimes", ewayBill.extendedTimes)
                clsCommon.AddColumnsForChange(coll, "rejectStatus", ewayBill.rejectStatus)
                clsCommon.AddColumnsForChange(coll, "vehicleType", ewayBill.vehicleType)
                clsCommon.AddColumnsForChange(coll, "actFromStateCode", ewayBill.actFromStateCode)
                clsCommon.AddColumnsForChange(coll, "actToStateCode", ewayBill.actToStateCode)
                clsCommon.AddColumnsForChange(coll, "transactionType", ewayBill.transactionType)
                clsCommon.AddColumnsForChange(coll, "otherValue", ewayBill.otherValue)
                clsCommon.AddColumnsForChange(coll, "cessNonAdvolValue", ewayBill.cessNonAdvolValue)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EWAY_BILL_REPORT_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                clsEwayBillItem.SaveData(ewayBill.ewbno, ewayBill.ItemList, trans)
                clsEwayBillVehicle.SaveData(ewayBill.ewbno, ewayBill.VehiclListDetails, trans)

            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strewbno As String) As clsEwayBillReportHead
        Dim obj As clsEwayBillReportHead = Nothing
        Try
            Dim strQry As String = "select * from TSPL_EWAY_BILL_REPORT_DETAIL where ewbno='" & strewbno & "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsEwayBillReportHead()
                obj.ewbno = clsCommon.myCstr(dt.Rows(0)("ewbno"))
                obj.ewayBillDate = clsCommon.myCstr(dt.Rows(0)("ewayBillDate"))
                obj.genMode = clsCommon.myCstr(dt.Rows(0)("genMode"))
                obj.userGstin = clsCommon.myCstr(dt.Rows(0)("userGstin"))
                obj.supplyType = clsCommon.myCstr(dt.Rows(0)("supplyType"))
                obj.subSupplyType = clsCommon.myCstr(dt.Rows(0)("subSupplyType"))
                obj.docType = clsCommon.myCstr(dt.Rows(0)("docType"))
                obj.docNo = clsCommon.myCstr(dt.Rows(0)("docNo"))
                obj.docDate = clsCommon.myCstr(dt.Rows(0)("docDate"))
                obj.fromGstin = clsCommon.myCstr(dt.Rows(0)("fromGstin"))
                obj.fromTrdName = clsCommon.myCstr(dt.Rows(0)("fromTrdName"))
                obj.fromAddr1 = clsCommon.myCstr(dt.Rows(0)("fromAddr1"))
                obj.fromAddr2 = clsCommon.myCstr(dt.Rows(0)("fromAddr2"))
                obj.fromPlace = clsCommon.myCstr(dt.Rows(0)("fromPlace"))
                obj.fromPincode = clsCommon.myCstr(dt.Rows(0)("fromPincode"))
                obj.fromStateCode = clsCommon.myCstr(dt.Rows(0)("fromStateCode"))
                obj.toGstin = clsCommon.myCstr(dt.Rows(0)("toGstin"))
                obj.toTrdName = clsCommon.myCstr(dt.Rows(0)("toTrdName"))
                obj.toAddr1 = clsCommon.myCstr(dt.Rows(0)("toAddr1"))
                obj.toAddr2 = clsCommon.myCstr(dt.Rows(0)("toAddr2"))
                obj.toPlace = clsCommon.myCstr(dt.Rows(0)("toPlace"))
                obj.toPincode = clsCommon.myCstr(dt.Rows(0)("toPincode"))
                obj.toStateCode = clsCommon.myCstr(dt.Rows(0)("toStateCode"))
                obj.totalValue = clsCommon.myCstr(dt.Rows(0)("totalValue"))
                obj.totInvValue = clsCommon.myCstr(dt.Rows(0)("totInvValue"))
                obj.cgstValue = clsCommon.myCstr(dt.Rows(0)("cgstValue"))
                obj.sgstValue = clsCommon.myCstr(dt.Rows(0)("sgstValue"))
                obj.igstValue = clsCommon.myCstr(dt.Rows(0)("igstValue"))
                obj.cessValue = clsCommon.myCstr(dt.Rows(0)("cessValue"))
                obj.transporterId = clsCommon.myCstr(dt.Rows(0)("transporterId"))
                obj.transporterName = clsCommon.myCstr(dt.Rows(0)("transporterName"))
                obj.status = clsCommon.myCstr(dt.Rows(0)("status"))
                obj.actualDist = clsCommon.myCstr(dt.Rows(0)("actualDist"))
                obj.noValidDays = clsCommon.myCstr(dt.Rows(0)("noValidDays"))
                obj.validUpto = clsCommon.myCstr(dt.Rows(0)("validUpto"))
                obj.extendedTimes = clsCommon.myCstr(dt.Rows(0)("extendedTimes"))
                obj.rejectStatus = clsCommon.myCstr(dt.Rows(0)("rejectStatus"))
                obj.vehicleType = clsCommon.myCstr(dt.Rows(0)("vehicleType"))
                obj.actFromStateCode = clsCommon.myCstr(dt.Rows(0)("actFromStateCode"))
                obj.actToStateCode = clsCommon.myCstr(dt.Rows(0)("actToStateCode"))
                obj.transactionType = clsCommon.myCstr(dt.Rows(0)("transactionType"))
                obj.otherValue = clsCommon.myCstr(dt.Rows(0)("otherValue"))
                obj.cessNonAdvolValue = clsCommon.myCstr(dt.Rows(0)("cessNonAdvolValue"))
                obj.ItemList = clsEwayBillItem.GetData(obj.ewbno, Nothing)
                obj.VehiclListDetails = clsEwayBillVehicle.GetData(obj.ewbno, Nothing)


            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

End Class
Public Class clsEwayBillItem
#Region "Variables"
    Public ewbno As String = ""
    Public itemNo As String = ""
    Public productId As String = ""
    Public productName As String = ""
    Public productDesc As String = ""
    Public hsnCode As String = ""
    Public quantity As String = ""
    Public qtyUnit As String = ""
    Public cgstRate As String = ""
    Public sgstRate As String = ""
    Public igstRate As String = ""
    Public cessRate As String = ""
    Public cessNonAdvol As String = ""
    Public taxableAmount As String = ""
#End Region
    Public Shared Function SaveData(ByVal strewbNo As String, ByVal arr As List(Of clsEwayBillItem), ByVal trans As SqlTransaction) As Boolean
        Try
            If (arr IsNot Nothing AndAlso arr.Count > 0) Then
                For Each obj As clsEwayBillItem In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "ewbno", strewbNo)
                    clsCommon.AddColumnsForChange(coll, "itemNo", obj.itemNo)
                    clsCommon.AddColumnsForChange(coll, "productId", obj.productId)
                    clsCommon.AddColumnsForChange(coll, "productDesc", obj.productDesc)
                    clsCommon.AddColumnsForChange(coll, "hsnCode", obj.hsnCode)
                    clsCommon.AddColumnsForChange(coll, "quantity", obj.quantity)
                    clsCommon.AddColumnsForChange(coll, "qtyUnit", obj.qtyUnit)
                    clsCommon.AddColumnsForChange(coll, "cgstRate", obj.cgstRate)
                    clsCommon.AddColumnsForChange(coll, "sgstRate", obj.sgstRate)
                    clsCommon.AddColumnsForChange(coll, "igstRate", obj.igstRate)
                    clsCommon.AddColumnsForChange(coll, "cessRate", obj.cessRate)
                    clsCommon.AddColumnsForChange(coll, "taxableAmount", obj.taxableAmount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EWAY_BILL_REPORT_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strewbno As String, ByVal trans As SqlTransaction) As List(Of clsEwayBillItem)
        Dim arr As List(Of clsEwayBillItem) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select * from TSPL_EWAY_BILL_REPORT_ITEM_DETAIL where ewbno='" & strewbno & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsEwayBillItem)
                Dim objTr As clsEwayBillItem
                For Each dr As DataRow In dt.Rows
                    objTr = New clsEwayBillItem
                    objTr.itemNo = clsCommon.myCstr(dr("itemNo"))
                    objTr.productId = clsCommon.myCstr(dr("productId"))
                    objTr.productDesc = clsCommon.myCstr(dr("productDesc"))
                    objTr.hsnCode = clsCommon.myCstr(dr("hsnCode"))
                    objTr.quantity = clsCommon.myCstr(dr("quantity"))
                    objTr.qtyUnit = clsCommon.myCstr(dr("qtyUnit"))
                    objTr.cgstRate = clsCommon.myCstr(dr("cgstRate"))
                    objTr.sgstRate = clsCommon.myCstr(dr("sgstRate"))
                    objTr.igstRate = clsCommon.myCstr(dr("igstRate"))
                    objTr.cessRate = clsCommon.myCstr(dr("cessRate"))
                    objTr.taxableAmount = clsCommon.myCstr(dr("taxableAmount"))
                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function
End Class
Public Class clsEwayBillVehicle
#Region "Variables"
    Public ewbno As String = ""
    Public updMode As String = ""
    Public vehicleNo As String = ""
    Public fromPlace As String = ""
    Public fromState As String = ""
    Public tripshtNo As String = ""
    Public userGSTINTransin As String = ""
    Public enteredDate As String = ""
    Public transMode As String = ""
    Public transDocNo As String = ""
    Public transDocDate As String = ""
    Public groupNo As String = ""
#End Region
    Public Shared Function SaveData(ByVal strewbNo As String, ByVal arr As List(Of clsEwayBillVehicle), ByVal trans As SqlTransaction) As Boolean
        Try
            If (arr IsNot Nothing AndAlso arr.Count > 0) Then
                For Each obj As clsEwayBillVehicle In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "ewbno", strewbNo)
                    clsCommon.AddColumnsForChange(coll, "updMode", obj.updMode)
                    clsCommon.AddColumnsForChange(coll, "vehicleNo", obj.vehicleNo)
                    clsCommon.AddColumnsForChange(coll, "fromPlace", obj.fromPlace)
                    clsCommon.AddColumnsForChange(coll, "fromState", obj.fromState)
                    clsCommon.AddColumnsForChange(coll, "tripshtNo", obj.tripshtNo)
                    clsCommon.AddColumnsForChange(coll, "userGSTINTransin", obj.userGSTINTransin)
                    clsCommon.AddColumnsForChange(coll, "enteredDate", obj.enteredDate)
                    clsCommon.AddColumnsForChange(coll, "transMode", obj.transMode)
                    clsCommon.AddColumnsForChange(coll, "transDocNo", obj.transDocNo)
                    clsCommon.AddColumnsForChange(coll, "transDocDate", obj.transDocDate)
                    clsCommon.AddColumnsForChange(coll, "groupNo", obj.groupNo)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EWAY_BILL_REPORT_VEHICLE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strewbno As String, ByVal trans As SqlTransaction) As List(Of clsEwayBillVehicle)
        Dim arr As List(Of clsEwayBillVehicle) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select * from TSPL_EWAY_BILL_REPORT_VEHICLE_DETAIL where ewbno='" & strewbno & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsEwayBillVehicle)
                Dim objTr As clsEwayBillVehicle
                For Each dr As DataRow In dt.Rows
                    objTr = New clsEwayBillVehicle
                    objTr.updMode = clsCommon.myCstr(dr("updMode"))
                    objTr.vehicleNo = clsCommon.myCstr(dr("vehicleNo"))
                    objTr.fromPlace = clsCommon.myCstr(dr("fromPlace"))
                    objTr.fromState = clsCommon.myCstr(dr("fromState"))
                    objTr.tripshtNo = clsCommon.myCstr(dr("tripshtNo"))
                    objTr.userGSTINTransin = clsCommon.myCstr(dr("userGSTINTransin"))
                    objTr.enteredDate = clsCommon.myCstr(dr("enteredDate"))
                    objTr.transMode = clsCommon.myCstr(dr("transMode"))
                    objTr.transDocNo = clsCommon.myCstr(dr("transDocNo"))
                    objTr.transDocDate = clsCommon.myCstr(dr("transDocDate"))
                    objTr.groupNo = clsCommon.myCstr(dr("groupNo"))

                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function
End Class

Public Class clsEwayBillsByDateDetail
    Public Property ewbNo As Object
    Public Property ewbDate As String
    Public Property status As String
    Public Property genGstin As String
    Public Property docNo As String
    Public Property docDate As String
    Public Property delPinCode As Integer
    Public Property delStateCode As Integer
    Public Property delPlace As String
    Public Property validUpto As String
    Public Property extendedTimes As Integer
    Public Property rejectStatus As String
End Class

Public Class clsEwayBillsByDate
    Public Property data As List(Of clsEwayBillsByDateDetail)
End Class


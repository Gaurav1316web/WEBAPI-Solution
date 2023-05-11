Imports common
Imports System.Data.SqlClient
Public Class clsBookingEntry

#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As Date?
    Public Posted As Integer = 0
    Public Arr As List(Of clsBookingDetail) = Nothing
    Public CreateDO_Automatic As Integer = 0

#End Region
    Public Function SaveData(ByVal obj As clsBookingEntry, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsBookingEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj1 As clsBookingDetail In Arr
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fresh Sale", "Fresh Booking Entry", obj1.Loc_Code, obj.Document_Date, trans)
                Next
            End If
            Dim isSaved As Boolean = True


            qry = "delete from TSPL_BOOKING_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.BookingEntry, "", "")
            End If

            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)

            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If
            'clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_MATSER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_MATSER", OMInsertOrUpdate.Update, "TSPL_BOOKING_MATSER.Document_No='" + obj.Document_No + "'", trans)
            End If

            isSaved = isSaved AndAlso clsBookingDetail.SaveData(obj.Document_No, Arr, trans)
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsBookingEntry
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal Trans As SqlTransaction) As clsBookingEntry
        Dim obj As clsBookingEntry = Nothing
        Dim qry As String = "select distinct Document_No,Document_Date,Posted,CreateDO_Automatic from TSPL_BOOKING_MATSER where comp_code='" + objCommonVar.CurrentCompanyCode + "' and "

        Select Case NavType
            Case NavigatorType.Current
                qry += "  Document_No='" + strDocumentNo + "'"
            Case NavigatorType.Next
                qry += " Document_No in (select min(t.Document_No) from TSPL_BOOKING_MATSER  as t where t.Document_No  >'" + strDocumentNo + "')"
            Case NavigatorType.First
                qry += " Document_No in (select min(t.Document_No ) from TSPL_BOOKING_MATSER  as t )"
            Case NavigatorType.Last
                qry += " Document_No in (select max(t.Document_No ) from TSPL_BOOKING_MATSER  as t )"
            Case NavigatorType.Previous
                qry += " Document_No in (select max(t.Document_No ) from TSPL_BOOKING_MATSER  as t where t.Document_No  <'" + strDocumentNo + "')"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBookingEntry
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.CreateDO_Automatic = clsCommon.myCdbl(dt.Rows(0)("CreateDO_Automatic"))
        End If
        Return obj


    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = String.Empty
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Booking Order No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsBookingEntry = clsBookingEntry.GetData(strCode, NavigatorType.Current, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                qry = "delete from TSPL_BOOKING_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_BOOKING_MATSER where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            Finally
                qry = Nothing
            End Try
        End If
        Return isSaved

    End Function
End Class
Public Class clsBookingDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    Public Cust_Code As String = Nothing
    Public Loc_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Booking_Qty As Double = 0
    Public DO_Qty As Double = 0
    Public DocumentAmount As Double = 0
    Public Short_Description As String = Nothing
    Public Unit_code As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Item_Rate As Double = 0
    Public Total_Qty As Double = 0
    Public Sampling As Integer = 0
   
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBookingDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBookingDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Booking_Qty", obj.Booking_Qty)
                clsCommon.AddColumnsForChange(coll, "DO_Qty", obj.Booking_Qty)
                clsCommon.AddColumnsForChange(coll, "Short_Description", obj.Short_Description)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
                clsCommon.AddColumnsForChange(coll, "DocumentAmount", obj.DocumentAmount)
                clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
                clsCommon.AddColumnsForChange(coll, "Total_Qty", obj.Total_Qty)
                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
        Arr = Nothing
    End Function

End Class

Public Class clsBookingTemp
    Public ItemCode As String = Nothing
    Public UnitCode As String = Nothing
    Public ShortDesc As String = Nothing
    Public RouteNo As String = Nothing
    Public Transporter As String = Nothing
    Public PriceCode As String = Nothing
    Public Price_Date As Date?
    Public ItemRate As Double = 0
    Public MRP As Double = 0
    Public CustCOde As String = Nothing
    Public CustName As String = Nothing
    Public VehicleCode As String = Nothing
    Public VehicleDesc As String = Nothing
    Public TotalAmt As Double = 0
    Public DeliveryNo As String = Nothing
    Public CreditLimit As Double = 0
    Public DOStatus As Integer = 0
    Public DObalance As Double = 0
    Public Disc_Scheme_Code As String = Nothing
    Public Disc_Scheme_Type As String = Nothing
    Public Disc_Scheme_Pers As Double = 0
    Public Disc_Scheme_Amount As Double = 0
    Public OrgRate As Double = 0
    Public Booking_Status As Integer = 0
    Public Posted As Integer = 0
    Public PerformaInvoiceBookingNo As String = Nothing
    Public dblOutstandingAmt As Double = 0
    Public SellingPrice As Double = 0
    Public BookingNo As String = Nothing
End Class

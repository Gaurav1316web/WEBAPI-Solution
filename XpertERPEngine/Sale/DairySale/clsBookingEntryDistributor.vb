Imports common
Imports System.Data.SqlClient
Public Class clsBookingEntryDistributor

#Region "Variable"
    Public Cust_Group_Code As String = Nothing
    Public Document_No As String = Nothing
    Public location_code As String = Nothing
    Public Document_Date As Date?
    Public Posted As Integer = 0
    Public Arr As List(Of clsBookingEntryDistributorDetail) = Nothing
    Public CreateDO_Automatic As Integer = 0
    Public Main_Booking_No As String = Nothing
    Public Distributor_Code As String = Nothing
    Public Cust_Code As String = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsBookingEntryDistributor, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsBookingEntryDistributor, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj1 As clsBookingEntryDistributorDetail In Arr
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Dairy Sale", "Fresh Booking Entry", obj1.Loc_Code, obj.Document_Date, trans)
                Next
            End If
            Dim isSaved As Boolean = True

            qry = "delete from TSPL_BOOKING_DETAIL where Main_Booking_No='" + obj.Main_Booking_No + "'  and Document_No='" + obj.Document_No + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDairySaleBooking, "", obj.location_code)
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
            clsCommon.AddColumnsForChange(coll, "location_code", obj.location_code)
            clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Main_Booking_No", obj.Main_Booking_No)
                clsCommon.AddColumnsForChange(coll, "Distributor_Code", obj.Distributor_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_MATSER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_MATSER", OMInsertOrUpdate.Update, "TSPL_BOOKING_MATSER.Main_Booking_No='" + obj.Main_Booking_No + "'", trans)
            End If

            isSaved = isSaved AndAlso clsBookingEntryDistributorDetail.SaveData(obj.Document_No, Arr, trans, isNewEntry, obj.Document_Date)
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsBookingEntryDistributor
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal Trans As SqlTransaction) As clsBookingEntryDistributor
        Dim obj As clsBookingEntryDistributor = Nothing
        Dim qry As String = "select distinct TSPL_BOOKING_MATSER.Main_Booking_No,TSPL_BOOKING_MATSER.Document_Date,TSPL_BOOKING_MATSER.Posted,CreateDO_Automatic,TSPL_BOOKING_MATSER.location_code,Cust_Group_Code,TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_MATSER inner join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Main_Booking_No=TSPL_BOOKING_DETAIL.Main_Booking_No where TSPL_BOOKING_MATSER.comp_code='" + objCommonVar.CurrentCompanyCode + "' and "

        Select Case NavType
            Case NavigatorType.Current
                qry += "  TSPL_BOOKING_MATSER.Main_Booking_No='" + strDocumentNo + "'"
            Case NavigatorType.Next
                qry += " TSPL_BOOKING_MATSER.Main_Booking_No in (select min(t.Main_Booking_No) from TSPL_BOOKING_MATSER  as t where t.Main_Booking_No  >'" + strDocumentNo + "')"
            Case NavigatorType.First
                qry += " TSPL_BOOKING_MATSER.Main_Booking_No in (select min(t.Main_Booking_No ) from TSPL_BOOKING_MATSER  as t )"
            Case NavigatorType.Last
                qry += " TSPL_BOOKING_MATSER.Main_Booking_No in (select max(t.Main_Booking_No ) from TSPL_BOOKING_MATSER  as t )"
            Case NavigatorType.Previous
                qry += " TSPL_BOOKING_MATSER.Main_Booking_No in (select max(t.Main_Booking_No ) from TSPL_BOOKING_MATSER  as t where t.Main_Booking_No  <'" + strDocumentNo + "')"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBookingEntryDistributor
            obj.Main_Booking_No = clsCommon.myCstr(dt.Rows(0)("Main_Booking_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.CreateDO_Automatic = clsCommon.myCdbl(dt.Rows(0)("CreateDO_Automatic"))
            obj.location_code = clsCommon.myCstr(dt.Rows(0)("location_code"))
            obj.Cust_Group_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Group_Code"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
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
        Dim obj As clsBookingEntryDistributor = clsBookingEntryDistributor.GetData(strCode, NavigatorType.Current, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Main_Booking_No) > 0) Then
            Try
                qry = "delete from TSPL_BOOKING_DETAIL where Main_Booking_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_BOOKING_MATSER where Main_Booking_No='" + strCode + "'"
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
Public Class clsBookingEntryDistributorDetail
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
    Public Disc_Scheme_Code As String = Nothing
    Public Disc_Scheme_Type As String = Nothing
    Public Disc_Scheme_Pers As Double = 0
    Public Disc_Scheme_Amount As Double = 0
    Public OrgRate As Double = 0
    Public Booking_Status As Integer = 1
    Public CreditApproval_Reqd As String = Nothing
    Public Posted As Integer = 0
    Public SchemeType As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Double = 0
    Public Scheme_Item_UOM As String = Nothing
    Public Item_Basic_Rate As Double = 0
    Public SellingPrice As Double = 0
    Public Scheme_Code As String = Nothing
    Public Main_Booking_No As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBookingEntryDistributorDetail), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean, ByVal Docdate As String) As Boolean
        Dim LineNo As Integer = 0
        Dim SchemeType As String = Nothing
        Dim Scheme_Item_Code As String = Nothing
        Dim Scheme_Qty As Double = 0
        Dim Scheme_Item_UOM As String = Nothing
        Dim arrRepeat As New List(Of String)


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBookingEntryDistributorDetail In Arr
                If arrRepeat.Contains(obj.Cust_Code) Then
                    LineNo += 1
                Else
                    arrRepeat.Add(obj.Cust_Code)
                    LineNo = 0
                    LineNo += 1
                End If


                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", LineNo)
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
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Amount", obj.Disc_Scheme_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Code", obj.Disc_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Pers", obj.Disc_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Disc_Scheme_Type", obj.Disc_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "Booking_Status", obj.Booking_Status)
                clsCommon.AddColumnsForChange(coll, "CreditApproval_Reqd", obj.CreditApproval_Reqd)
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.SchemeType)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", obj.Scheme_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.Scheme_Qty)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", obj.Scheme_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Item_Selling_Price", obj.SellingPrice)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", "N")
                clsCommon.AddColumnsForChange(coll, "FOC_Item", "0")
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Main_Booking_No", obj.Main_Booking_No)
                If clsCommon.myLen(clsCommon.myCstr(obj.SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.SchemeType), "Quantitive") = CompairStringResult.Equal Then

                End If


                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                If clsCommon.myLen(clsCommon.myCstr(obj.SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.SchemeType), "Quantitive") = CompairStringResult.Equal Then
                    Dim colll As New Hashtable()
                    Dim DocAmt As Integer = 0
                    LineNo += 1
                    clsCommon.AddColumnsForChange(colll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(colll, "Line_No", LineNo)
                    clsCommon.AddColumnsForChange(colll, "Cust_Code", obj.Cust_Code)
                    clsCommon.AddColumnsForChange(colll, "Loc_Code", obj.Loc_Code)
                    clsCommon.AddColumnsForChange(colll, "Item_Code", obj.Scheme_Item_Code)
                    clsCommon.AddColumnsForChange(colll, "Booking_Qty", obj.Scheme_Qty)
                    clsCommon.AddColumnsForChange(colll, "DO_Qty", obj.Scheme_Qty)
                    ' clsCommon.AddColumnsForChange(colll, "Short_Description", obj.Short_Description)
                    clsCommon.AddColumnsForChange(colll, "Unit_code", obj.Scheme_Item_UOM)
                    clsCommon.AddColumnsForChange(colll, "Vehicle_Code", obj.Vehicle_Code)
                    clsCommon.AddColumnsForChange(colll, "Booking_Status", obj.Booking_Status)
                    clsCommon.AddColumnsForChange(colll, "Total_Qty", obj.Scheme_Qty)


                    clsCommon.AddColumnsForChange(colll, "Scheme_Type", obj.SchemeType)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Qty", obj.Booking_Qty)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Item_UOM", obj.Unit_code)
                    clsCommon.AddColumnsForChange(colll, "Scheme_Code", obj.Scheme_Code)

                    clsCommon.AddColumnsForChange(colll, "Scheme_Item", "Y")
                    clsCommon.AddColumnsForChange(colll, "FOC_Item", "1")
                    clsCommon.AddColumnsForChange(colll, "Main_Booking_No", obj.Main_Booking_No)
                    clsCommonFunctionality.UpdateDataTable(colll, "TSPL_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                End If
            Next


        End If
        Return True
        Arr = Nothing
    End Function

End Class



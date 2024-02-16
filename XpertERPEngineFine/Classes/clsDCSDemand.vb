Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class clsDCSDemand
#Region "Variable"
    Public Document_No As String = ""
    Public Document_Date As DateTime = Nothing
    Public Route_No As String = ""
    Public Location As String = ""
    Public Categories As String = ""
    Public Posted As Integer = 0
    Public Arr As List(Of clsDCSDemandDetail) = Nothing


#End Region
    Public Function SaveData(ByVal obj As clsDCSDemand, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsDCSDemand, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            If clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0 Then
                qry = "delete from TSPL_DCS_DEMAND_BOOKING_DETAIL where tr_code in (select tr_code from TSPL_DCS_DEMAND_BOOKING_DETAIL where Document_No='" + obj.Document_No + "') "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Categories", obj.Categories)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDCSDemandBooking, "", obj.Location)
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_DEMAND_BOOKING_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_DEMAND_BOOKING_MASTER", OMInsertOrUpdate.Update, "TSPL_DCS_DEMAND_BOOKING_MASTER.Document_No='" + obj.Document_No + "'", trans)
            End If

            clsDCSDemandDetail.SaveData(obj.Document_No, obj.Document_Date, obj.Arr, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDCSDemand
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDCSDemand
        Dim obj As clsDCSDemand = Nothing
        Dim qry = "SELECT  TSPL_DCS_DEMAND_BOOKING_MASTER.*  FROM TSPL_DCS_DEMAND_BOOKING_MASTER where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DCS_DEMAND_BOOKING_MASTER.Document_No = (select MIN(Document_No) from TSPL_DCS_DEMAND_BOOKING_MASTER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_DCS_DEMAND_BOOKING_MASTER.Document_No = (select Max(Document_No) from TSPL_DCS_DEMAND_BOOKING_MASTER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_DCS_DEMAND_BOOKING_MASTER.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_DCS_DEMAND_BOOKING_MASTER.Document_No = (select Min(Document_No) from TSPL_DCS_DEMAND_BOOKING_MASTER where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DCS_DEMAND_BOOKING_MASTER.Document_No = (select Max(Document_No) from TSPL_DCS_DEMAND_BOOKING_MASTER where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDCSDemand()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.Categories = clsCommon.myCstr(dt.Rows(0)("Categories"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))

            qry = "SELECT TSPL_DCS_DEMAND_BOOKING_DETAIL.*,tspl_item_master.Item_Desc FROM TSPL_DCS_DEMAND_BOOKING_DETAIL left outer join tspl_item_master on  " &
                "TSPL_DCS_DEMAND_BOOKING_DETAIL.item_code=tspl_item_master.item_code where Document_No='" & obj.Document_No & "' order by TSPL_DCS_DEMAND_BOOKING_DETAIL.VLC_Uploader asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDCSDemandDetail)
                Dim objTr As clsDCSDemandDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDCSDemandDetail
                    objTr.VLC_Uploader = clsCommon.myCstr(dr("VLC_Uploader"))
                    objTr.CreditType = clsCommon.myCstr(dr("CreditType"))
                    objTr.OutStandingAmt = clsCommon.myCstr(dr("OutStandingAmt"))
                    If dt.Rows(0)("LastMilkDate") IsNot DBNull.Value Then
                        objTr.LastMilkDate = clsCommon.myCstr(dr("LastMilkDate"))

                    End If
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))

                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))

                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            If clsCommon.myLen(strCode) > 0 Then
                Dim flag As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_DCS_DEMAND_BOOKING_MASTER where Document_No='" + strCode + "'", trans)) = 0, True, False)
                If flag Then
                    Throw New Exception(" Document Not Found!")
                End If
            End If
            qry = "delete from TSPL_DCS_DEMAND_BOOKING_DETAIL where tr_code in (select tr_code from TSPL_DCS_DEMAND_BOOKING_DETAIL where Document_No='" + strCode + "') "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DCS_DEMAND_BOOKING_MASTER where  Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("DCS Demand Booking No not found to Post")
            End If
            Dim obj As clsDCSDemand = clsDCSDemand.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If obj.Posted = 1 Then
                Throw New Exception("Docuemnt is already posted")
            End If
            Dim dtNow As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt")
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted", 1)
            clsCommon.AddColumnsForChange(coll, "Posting_Date", dtNow)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_DEMAND_BOOKING_MASTER", OMInsertOrUpdate.Update, "TSPL_DCS_DEMAND_BOOKING_MASTER.Document_No='" + obj.Document_No + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select Posted from TSPL_DCS_Demand_BOOKING_MASTER where Document_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Qry = "Update TSPL_DCS_Demand_BOOKING_MASTER set Posted = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsDCSDemandDetail

#Region "Variable"
    Public TR_Code As String = ""
    Public Document_No As String = ""
    Public Line_No As Double = 0
    Public VLC_Uploader As String = ""
    Public CreditType As String = ""
    Public OutStandingAmt As Double = 0
    Public LastMilkDate As Date? = Nothing
    Public Item_Code As String = ""
    Public Qty As Double = 0
    Public Unit_code As String = ""
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal DocDate As Date, ByVal Arr As List(Of clsDCSDemandDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsDCSDemandDetail In Arr

                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "VLC_Uploader", obj.VLC_Uploader)
                clsCommon.AddColumnsForChange(coll, "CreditType", obj.CreditType)
                clsCommon.AddColumnsForChange(coll, "OutStandingAmt", obj.OutStandingAmt)
                If obj.LastMilkDate IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "LastMilkDate", clsCommon.GetPrintDate(obj.LastMilkDate, "dd/MMM/yyyy "), True)
                Else
                    clsCommon.AddColumnsForChange(coll, "LastMilkDate", Nothing, True)
                End If

                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_DEMAND_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

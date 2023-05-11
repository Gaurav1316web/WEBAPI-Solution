'-23/01/2013-10:11AM--Created By--Pankaj Kumar---Table Used-[TSPL_ASSET_SEGMENT]
Imports common
Imports System.Data.SqlClient


Public Class clsAssetSegment
#Region "variables"
    Public No_Of_Segment As Integer = 0
    Public Id_Segment As String = Nothing
    Public Segment_Seperator As Char = Nothing
    Public Segment_No As String = Nothing
    Public Length As String = Nothing
    Public Link_To As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal ArrSeg As List(Of clsAssetSegment)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (ArrSeg IsNot Nothing AndAlso ArrSeg.Count > 0) Then
                For Each objSeg As clsAssetSegment In ArrSeg
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "No_Of_Segment", objSeg.No_Of_Segment)
                    clsCommon.AddColumnsForChange(coll, "Id_Segment", objSeg.Id_Segment)
                    clsCommon.AddColumnsForChange(coll, "Segment_Seperator", objSeg.Segment_Seperator)
                    clsCommon.AddColumnsForChange(coll, "Segment_No", objSeg.Segment_No)
                    clsCommon.AddColumnsForChange(coll, "Length", objSeg.Length)
                    clsCommon.AddColumnsForChange(coll, "Link_To", objSeg.Link_To)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ASSET_SEGMENT", OMInsertOrUpdate.Insert, "", trans)
                Next
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData()
        Dim ArrSeg As New List(Of clsAssetSegment)
        Dim qry As String = "Select No_Of_Segment, Id_Segment, Segment_Seperator, Segment_No, Length, Link_To from TSPL_ASSET_SEGMENT WHERE 1=1"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                Dim objTr As New clsAssetSegment()
                objTr.No_Of_Segment = clsCommon.myCdbl(dr("No_Of_Segment"))
                objTr.Id_Segment = clsCommon.myCstr(dr("Id_Segment"))
                objTr.Segment_Seperator = clsCommon.myCstr(dr("Segment_Seperator"))
                objTr.Segment_No = clsCommon.myCstr(dr("Segment_No"))
                objTr.Length = clsCommon.myCstr(dr("Length"))
                objTr.Link_To = clsCommon.myCstr(dr("Link_To"))
                ArrSeg.Add(objTr)
            Next
        End If
        Return ArrSeg
    End Function
End Class

Public Class clsSecondaryCustomerSale
    Public Cust_Code As String
    Public Year As String
    Public Month As String
    Public Pack As String
    Public Sale As Double = Nothing
    Public CurrentDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy hh:mm tt")

    Public Shared Function SaveData(ByVal Arr As List(Of clsSecondaryCustomerSale)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Try
            If Arr.Count > 0 Then
                For Each obj As clsSecondaryCustomerSale In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Sale", obj.Sale)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", obj.CurrentDate)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    Dim count As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_SECONDARY_CUSTOMER_SALE WHERE Cust_Code='" + obj.Cust_Code + "' AND Year=" + obj.Year + " AND Month=" + obj.Month + " AND Pack='" + obj.Pack + "'", trans)
                    If count <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                        clsCommon.AddColumnsForChange(coll, "Year", obj.Year)
                        clsCommon.AddColumnsForChange(coll, "Month", obj.Month)
                        clsCommon.AddColumnsForChange(coll, "Pack", obj.Pack)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", obj.CurrentDate)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_CUSTOMER_SALE", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_CUSTOMER_SALE", OMInsertOrUpdate.Update, "Cust_Code='" + obj.Cust_Code + "' AND Year=" + obj.Year + " AND Month=" + obj.Month + " AND Pack='" + obj.Pack + "'", trans)
                    End If
                Next
            Else
                trans.Rollback()
                Throw New Exception("No data found to save.")
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCustCode As String, ByVal Year As String, ByVal Month As String) As DataTable
        Try
            Dim arr As List(Of clsSecondaryCustomerSale) = Nothing
            Dim qry As String = "select inv_class_code as Code, Inv_Class_Desc as Description, XXX.Sale from TSPL_INV_CLASS_DETAILS " & _
                " LEFT OUTER JOIN (Select Pack, Sale from TSPL_SECONDARY_CUSTOMER_SALE WHERE TSPL_SECONDARY_CUSTOMER_SALE.Cust_Code='" + strCustCode + "' AND TSPL_SECONDARY_CUSTOMER_SALE.Year=" + Year + " AND Month=" + Month + "" & _
                " ) as XXX on XXX.Pack=TSPL_INV_CLASS_DETAILS.Inv_Class_Code WHERE TSPL_INV_CLASS_DETAILS.Inv_Class_Name='Size'"
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCustCode As String, ByVal Year As String, ByVal Month As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            If clsCommon.myLen(strCustCode) > 0 Then
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_SECONDARY_CUSTOMER_SALE Where Cust_Code='" + strCustCode + "' AND Year=" + Year + " AND Month=" + Month + " ", trans)
                Return True
            Else
                Throw New Exception("Customer not found to delete.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class



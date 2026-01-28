Imports System.Data.SqlClient
Imports common

Public Class clsMobileDCSDemand
#Region "Variables"
    Public PK_ID As Integer
    Public Document_Date As String = Nothing
    Public VLC_Code_VLC_Uploader As String = Nothing
    Public VLC_Code As String = Nothing
    Public VLC_Name As String = Nothing
    Public Zone_Code As String = Nothing
    Public Zone_Name As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Approve_Qty As Decimal = 0
    Public Qty As Decimal = 0
    Public UOM As String = Nothing
    Public Price_Code As String = Nothing
    Public Rate As Decimal = 0
    Public Amount As Decimal = 0
    Public Status As Integer = 0
    Public Arr As List(Of clsMobileDCSDemand) = Nothing
    Public dtError As DataTable = New DataTable()
#End Region
    Public Function SaveData(ByVal Arr As List(Of clsMobileDCSDemand)) As Boolean
        Dim isSaved As Boolean = True

        dtError.Columns.Add("RowNo", GetType(Integer))
        dtError.Columns.Add("Error", GetType(String))
        Dim trans As SqlTransaction = Nothing
        Try
            Dim linno As Integer = 0
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsMobileDCSDemand In Arr
                    Dim coll As New Hashtable()
                    Dim isApproved As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select isnull(Status,0) as Status from TSPL_MOBILE_DCS_DEMAND where PK_ID = " & obj.PK_ID & "", trans) > 0)
                    If isApproved Then
                        Try
                            Throw New Exception("PK ID [" & clsCommon.myCstr(obj.PK_ID) & "] is already approved.")
                        Catch ex As Exception
                            Dim dr As DataRow = dtError.NewRow()
                            dr("RowNo") = linno
                            dr("Error") = ex.Message
                            dtError.Rows.Add(dr)
                        End Try
                    Else
                        clsCommon.AddColumnsForChange(coll, "Approve_Qty", obj.Approve_Qty)
                        clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                        clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                        clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MOBILE_DCS_DEMAND", OMInsertOrUpdate.Update, "TSPL_MOBILE_DCS_DEMAND.PK_ID='" & obj.PK_ID & "'", trans)
                        If isSaved Then
                            ApproveData(obj.PK_ID, trans)
                        End If
                    End If
                    linno += 1
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Function ApproveData(ByVal PK_ID As Integer, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(PK_ID) <= 0) Then
                Throw New Exception("No Data found To Approve")
            End If
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_MOBILE_DCS_DEMAND Set Status= 1, Approved_By = '" & objCommonVar.CurrentUserCode & "',Approved_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "'  where PK_ID='" & PK_ID & "'", trans)
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

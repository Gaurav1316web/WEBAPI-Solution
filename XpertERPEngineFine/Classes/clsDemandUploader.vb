Imports System.Data.SqlClient
Public Class clsDemandUploader
#Region "Variable"

    'Public Sno As String = ""
    'Public Qty_In As String = ""
    'Public Route As String = ""
    'Public Booth As String = ""
    'Public TM500 As Double = 0
    'Public TM1LT As Double = 0
    'Public SM500 As Double = 0
    'Public SM1LT As Double = 0
    'Public GM500 As Double = 0
    'Public GM1LT As Double = 0
    'Public CHHACH As Double = 0
    'Public TM6LT As Double = 0
    'Public GM6LT As Double = 0
    'Public SL400 As Double = 0
    'Public SL6LT As Double = 0
    'Public TotalAmount As Double = 0
    ' Public lstitem As List(Of clsDemandItemDetails)
    Public Key As String
    Public lstValue As List(Of clsDemandItemDetails)
#End Region

End Class
Public Class clsDemandItemDetails
#Region "Variables"
    Public Value As String
#End Region
End Class
Public Class clsDemandUploaderDetails
    Public Route As String = ""
    Public Booth As String = ""
    Public Item_Code As String = ""
    Public Unit_Code As String = ""
    Public Vehicle_Code As String = ""
    Public Price_code As String = ""
    Public City_Code As String = ""
    Public Qty As Double = 0
    Public Item_Rate As Double = 0
    Public TotalCrates_ItemWise As Double = 0
    Public TotalLtr_ItemWise As Double = 0
    Public ItemNetAmount As Double = 0


End Class
Public Class clsDemandUploaderSave
#Region "Variables"
    Public Document_No As String = ""
    Public Document_Date As DateTime = Nothing
    Public ShiftType As String = ""
    Public Location_Code As String = ""


#End Region

    Public Function SaveData(ByVal obj As clsDemandUploaderSave, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsDemandUploaderSave, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            'Dim StrQry As String = "delete from TSPL_DEMAND_ADJUSTMENT_DETAIL where Document_Code='" + obj.Document_No + "'"
            'clsDBFuncationality.ExecuteNonQuery(StrQry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDemandUploader, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_UPLOADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_UPLOADER", OMInsertOrUpdate.Update, "TSPL_DEMAND_UPLOADER.Document_No='" + clsCommon.myCstr(obj.Document_No) + "' ", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDemandUploaderSave
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDemandUploaderSave
        Dim obj As clsDemandUploaderSave = Nothing
        Dim qry = "SELECT  TSPL_DEMAND_UPLOADER.*  FROM TSPL_DEMAND_UPLOADER where 2=2 "
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DEMAND_UPLOADER.Document_No = (select MIN(Document_No) from TSPL_DEMAND_UPLOADER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_DEMAND_UPLOADER.Document_No = (select Max(Document_No) from TSPL_DEMAND_UPLOADER WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_DEMAND_UPLOADER.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_DEMAND_UPLOADER.Document_No = (select Min(Document_Code) from TSPL_DEMAND_UPLOADER where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DEMAND_UPLOADER.Document_No = (select Max(Document_Code) from TSPL_DEMAND_UPLOADER where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDemandUploaderSave()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.ShiftType = clsCommon.myCstr(dt.Rows(0)("ShiftType"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))

        End If
        Return obj
    End Function
End Class

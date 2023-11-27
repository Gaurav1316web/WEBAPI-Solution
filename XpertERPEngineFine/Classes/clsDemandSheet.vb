Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Public Class clsDemandSheet
#Region "Variable"
    Public DEMAND_Date As Date = Nothing
    Public ShiftType As String = Nothing
    Public Cust_Code As String = Nothing
    Public Set_Zero As Decimal = 0
    Public Item_Code As String = Nothing
    Public Qty As Decimal = 0

#End Region
    Public Function SaveData(ByVal obj As clsDemandSheet) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsDemandSheet, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim colCount As Decimal = 0
            Dim StrQry As String = "select Count(*) from TSPL_DEMAND_SHEET where DEMAND_Date='" + clsCommon.GetPrintDate(obj.DEMAND_Date) + "' and ShiftType='" + obj.ShiftType + "' and Item_Code='" + obj.Item_Code + "' and Cust_Code='" + obj.Cust_Code + "'"
            colCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(StrQry, trans))
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DEMAND_Date", clsCommon.GetPrintDate(obj.DEMAND_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Set_Zero", obj.Set_Zero)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If colCount = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_SHEET", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_SHEET", OMInsertOrUpdate.Update, "TSPL_DEMAND_SHEET.DEMAND_Date='" + clsCommon.GetPrintDate(obj.DEMAND_Date) + "' and TSPL_DEMAND_SHEET.ShiftType='" + obj.ShiftType + "' and TSPL_DEMAND_SHEET.Item_Code='" + obj.Item_Code + "'  and TSPL_DEMAND_SHEET.Cust_Code='" + obj.Cust_Code + "'", trans)
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Imports common
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class clsStockAgeingFilters
#Region "Variables"
    Public CutOffDate As Date = Nothing
    Public AsOnDate As Date = Nothing
    Public ReportType As String = Nothing
    Public InventoryType As String = Nothing '' Milk, Non Milk,All
    Public AgeingColumns As String = Nothing '' Qty,Value,Qty+Value,Fat-SNF,All
    Public Item_Status As String = Nothing
    Public arrItemType As ArrayList = Nothing
    Public arrTransaction As ArrayList = Nothing
    Public arrItemGroup As ArrayList = Nothing
    Public arrItem As ArrayList = Nothing
    Public UOM_Code As String = Nothing
    Public SelectLocation As Boolean = False
    Public arrLocation As List(Of clsCode) = Nothing
    Public arrLoc As ArrayList
    Public arrAgeingBucket As ArrayList
    Public SelectCategory As Boolean = False
    Public arrCategory As List(Of clsCode) = Nothing
#End Region
End Class
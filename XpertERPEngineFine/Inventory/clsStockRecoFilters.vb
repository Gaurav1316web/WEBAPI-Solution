Imports common
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class clsStockRecoFilters
#Region "Variables"
    Public From_Date As Date = Nothing
    Public To_Date As Date = Nothing
    Public InOut As String = Nothing
    Public arrTransaction As ArrayList = Nothing
    Public arrItemGroup As ArrayList = Nothing
    Public arrItem As ArrayList = Nothing
    Public ReportType As String = Nothing
    Public arrItemType As ArrayList = Nothing
    Public UOM_Code As String = Nothing
    Public MRPWise As Boolean = False
    Public FatSNF As Boolean = True
    Public IncludeGIT As Boolean = False
    Public ExcludeConsumptionLoc As Boolean = False
    Public DisplayMethod As String = "None"
    '====================add by Monika26/03/2017==================
    Public IsProduction_WIP As Boolean = False
    Public FAT_SNF_TYPE As String = "M" ''Manual
    Public ChkPartialyLoadData As Boolean = False
    Public isPrintCrystal As Integer = 0
    ''===================================================================

    Public SelectLocation As Boolean = False
    Public arrLocation As List(Of clsCode) = Nothing
    Public SelectCategory As Boolean = False
    Public arrCategory As List(Of clsCode) = Nothing
    Public FORMTYPE As String
    Public arrLoc As ArrayList

#End Region
End Class
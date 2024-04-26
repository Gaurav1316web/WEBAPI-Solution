Imports common
Imports System.Data.SqlClient
'Imports Telerik.WinControls.UI
'Imports Telerik.WinControls
Imports System.Data
Imports System.Windows.Forms
Imports System.Configuration
'Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Public Class clsItemMasterCategory
    Public Item_code As String = ""
    Public SNO As Integer = 0
    Public Item_Category_Code As String = ""
    Public Item_Category_Code_Desc As String = ""
    Public Item_Cagetory_Values As String = ""
    Public Item_Cagetory_Values_Desc As String = ""
    Public Item_Cagetory_Values_BIN_NO As String = ""
    Public Master_Value As String = Nothing
    Public SKU_Value As String = Nothing

    Public Shared Function SaveData(ByVal strICode As String, ByVal ArrItemMasterCategory As List(Of clsItemMasterCategory), ByVal ArrDatabase As List(Of String), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim ii As Integer = 1
        For Each obj As clsItemMasterCategory In ArrItemMasterCategory
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
            clsCommon.AddColumnsForChange(coll, "SNO", ii)
            clsCommon.AddColumnsForChange(coll, "Item_Category_Code", obj.Item_Category_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Cagetory_Values", obj.Item_Cagetory_Values)
            clsCommon.AddColumnsForChange(coll, "Master_Value", obj.Master_Value)
            clsCommon.AddColumnsForChange(coll, "SKU_Value", obj.SKU_Value)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, ArrDatabase, "TSPL_ITEM_MASTER_CATEGORY", OMInsertOrUpdate.Insert, "", trans)
            ii = ii + 1
        Next
        Return isSaved
    End Function
End Class
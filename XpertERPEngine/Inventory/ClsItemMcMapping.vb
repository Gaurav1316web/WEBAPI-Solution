Imports common
Imports System.Data.SqlClient
Public Class ClsItemMcMapping
#Region "Variables"
    Public itemcode As String = Nothing
    Public name As String = Nothing
    Public TAX2_Amt As Double = 0
    Public dmc As Double = 0
    Public vmoh As Double = 0
    Public royality As Double = 0
    Public pfreight As Double = 0
    Public sfreight As Double = 0
    Public vsdrouter As Double = 0
    Public vsdLoUnlo As Double = 0
#End Region

    Public Function SaveData(ByVal Arr As List(Of ClsItemMcMapping), ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete from TSPL_ITEM_MC_MAPPING"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                For Each obj As ClsItemMcMapping In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Item_code", obj.itemcode)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.name)
                    clsCommon.AddColumnsForChange(coll, "DMC", obj.dmc)
                    clsCommon.AddColumnsForChange(coll, "VMOH", obj.vmoh)
                    clsCommon.AddColumnsForChange(coll, "Royalty", obj.royality)
                    clsCommon.AddColumnsForChange(coll, "Primary_Freight", obj.pfreight)
                    clsCommon.AddColumnsForChange(coll, "Secondary_Freight", obj.sfreight)
                    clsCommon.AddColumnsForChange(coll, "VS_D_Router", obj.vsdrouter)
                    clsCommon.AddColumnsForChange(coll, "VS_D_Loading_ULoading", obj.vsdLoUnlo)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MC_MAPPING", OMInsertOrUpdate.Insert, "", trans)

                Next
                If isSaved Then
                    trans.Commit()

                Else
                    trans.Rollback()

                End If
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved

    End Function

    Public Shared Function GetData()
        Dim obj As ClsItemMcMapping = Nothing
        Dim qry As String = "select item_code, Item_Desc , dmc,vmoh,royalty,primary_freight,secondary_freight,vs_d_router,vs_d_loading_uloading from TSPL_ITEM_MC_MAPPING "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of ClsItemMcMapping)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As ClsItemMcMapping
            For Each dr As DataRow In dt.Rows
                objTr = New ClsItemMcMapping()
                objTr.itemcode = clsCommon.myCstr(dr("Item_code"))
                objTr.name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.dmc = clsCommon.myCdbl(dr("DMC"))
                objTr.vmoh = clsCommon.myCdbl(dr("VMOH"))
                objTr.royality = clsCommon.myCdbl(dr("Royalty"))
                objTr.pfreight = clsCommon.myCdbl(dr("Primary_Freight"))
                objTr.sfreight = clsCommon.myCdbl(dr("Secondary_Freight"))
                objTr.vsdrouter = clsCommon.myCdbl(dr("VS_D_Router"))
                objTr.vsdLoUnlo = clsCommon.myCdbl(dr("VS_D_Loading_ULoading"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class

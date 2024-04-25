
Imports System.Data.SqlClient
Public Class clsItemMasterQCParameter
    Public FATRate As Decimal
    Public SNFRate As Decimal
    Public FATPer As Decimal
    Public SNFPer As Decimal

    Public Shared Function GetStandardFATSNFRate(ByVal Item_Code As String, ByVal trans As SqlTransaction) As clsItemMasterQCParameter
        Dim obj As New clsItemMasterQCParameter
        obj.FATRate = 0
        obj.SNFRate = 0
        Dim qry As String = "select TSPL_PARAMETER_MASTER.Type, TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate, TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range from TSPL_ITEM_QC_PARAMETER_MASTER " + Environment.NewLine +
                   "left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code" + Environment.NewLine +
                   "where Item_Code='" + Item_Code + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "FAT") = CompairStringResult.Equal Then
                    obj.FATRate = clsCommon.myCdbl(dr("StandardRate"))
                    obj.FATPer = clsCommon.myCdbl(dr("Actual_Range"))
                End If
                If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "SNF") = CompairStringResult.Equal Then
                    obj.SNFRate = clsCommon.myCdbl(dr("StandardRate"))
                    obj.SNFPer = clsCommon.myCdbl(dr("Actual_Range"))
                End If
            Next
        End If
        Return obj
    End Function
End Class
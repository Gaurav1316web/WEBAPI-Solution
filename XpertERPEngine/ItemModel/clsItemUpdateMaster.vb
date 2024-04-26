Imports System.Data.SqlClient
Public Class clsItemUpdateMaster
    Public ArrTr As List(Of clsItemUpdateDetail) = Nothing

    Public Function Update(ByVal obj As clsItemUpdateMaster) As Boolean
        Dim isSaved As Boolean = True
        isSaved = clsItemUpdateDetail.UpdateData(obj.ArrTr)
        Return isSaved
    End Function
End Class
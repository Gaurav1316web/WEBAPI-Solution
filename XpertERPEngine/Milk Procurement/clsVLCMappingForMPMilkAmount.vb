Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsVLCMappingForMPMilkAmount

    Public Shared Function SaveData(ByVal strMCC As String, ByVal arr As ArrayList) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Delete from TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT where MCC_Code='" + strMCC + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each strVLC As String In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "MCC_Code", strMCC)
                clsCommon.AddColumnsForChange(coll, "VLC_Code", strVLC)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT", OMInsertOrUpdate.Insert, "", trans)
            Next
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strMCC As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Delete from TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT where MCC_Code='" + strMCC + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strMCC As String, ByVal NavType As NavigatorType, ByRef strOutMCCCode As String) As ArrayList
        Dim arr As ArrayList = Nothing
        Try
            strOutMCCCode = strMCC
            Dim qry As String = "select MCC_Code from TSPL_MCC_MASTER where 2=2 "
            Dim whrClas As String = ""
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_MCC_MASTER.MCC_Code = (select MIN(MCC_Code) from TSPL_MCC_MASTER where 1=1 " + whrClas + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_MCC_MASTER.MCC_Code = (select Max(MCC_Code) from TSPL_MCC_MASTER where 1=1 " + whrClas + ")"
                Case NavigatorType.Next
                    qry += " and TSPL_MCC_MASTER.MCC_Code = (select Min(MCC_Code) from TSPL_MCC_MASTER where MCC_Code>'" + strMCC + "' " + whrClas + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_MCC_MASTER.MCC_Code = (select Max(MCC_Code) from TSPL_MCC_MASTER where MCC_Code<'" + strMCC + "' " + whrClas + ")"
                Case NavigatorType.Current
                    qry += " and TSPL_MCC_MASTER.MCC_Code = '" + strMCC + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                strOutMCCCode = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                qry = "select TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT.VLC_Code from TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT where MCC_Code='" + strOutMCCCode + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    arr = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                    Next
                End If
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return arr
    End Function
End Class


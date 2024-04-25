Imports common
Imports System.Data.SqlClient
'Imports Telerik.WinControls.UI
Imports System.Windows.Forms
Public Class clsCustomFieldValues
#Region "Variables"
    Public Program_Code As String = Nothing
    Public Transaction_Code As String = Nothing
    Public Custom_Field_Code As String = Nothing
    Public Value As String = Nothing
    Public Line_N0_For_Detail As Integer = 0
    Public ValueDescription As String = Nothing ''Not a table filed
#End Region

    Public Shared Function SaveData(ByVal strProgramCode As String, ByVal strTransactionCode As String, ByVal Arr As List(Of clsCustomFieldValues), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" + strProgramCode + "' and Transaction_Code='" + strTransactionCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsCustomFieldValues In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Program_Code", strProgramCode)
                    clsCommon.AddColumnsForChange(coll, "Transaction_Code", strTransactionCode)
                    clsCommon.AddColumnsForChange(coll, "Custom_Field_Code", obj.Custom_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                    clsCommon.AddColumnsForChange(coll, "Line_N0_For_Detail", obj.Line_N0_For_Detail)
                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_VALUES", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strProgramCode As String, ByVal strTransactionCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" + strProgramCode + "' and Transaction_Code='" + strTransactionCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getQueryStringForRPT(ByVal strProgramCode As String, ByVal strTransactionCode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" + strProgramCode + "' and Transaction_Code='" + strTransactionCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strProgramCode As String, ByVal strTransactionCode As String, ByVal isForDetail As Boolean) As List(Of clsCustomFieldValues)
        Dim Arr As List(Of clsCustomFieldValues) = Nothing
        Dim qry As String = "SELECT TSPL_CUSTOM_FIELD_VALUES.*,TSPL_CUSTOM_FIELD_DETAIL.Description as ValueDescription  FROM TSPL_CUSTOM_FIELD_VALUES  left outer join TSPL_CUSTOM_FIELD_DETAIL on TSPL_CUSTOM_FIELD_DETAIL.Custom_Field_Code=TSPL_CUSTOM_FIELD_VALUES.Custom_Field_Code  and TSPL_CUSTOM_FIELD_DETAIL.Value=TSPL_CUSTOM_FIELD_VALUES.Value where Program_Code='" + strProgramCode + "' and Transaction_Code='" + strTransactionCode + "' "
        If isForDetail Then
            qry += " and Line_N0_For_Detail >0"
        Else
            qry += " and Line_N0_For_Detail =0"
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsCustomFieldValues)
            Dim objTr As clsCustomFieldValues
            For Each dr As DataRow In dt.Rows
                objTr = New clsCustomFieldValues
                objTr.Program_Code = clsCommon.myCstr(dr("Program_Code"))
                objTr.Transaction_Code = clsCommon.myCstr(dr("Transaction_Code"))
                objTr.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                objTr.Value = clsCommon.myCstr(dr("Value"))
                objTr.ValueDescription = clsCommon.myCstr(dr("ValueDescription"))
                objTr.Line_N0_For_Detail = clsCommon.myCstr(dr("Line_N0_For_Detail"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class
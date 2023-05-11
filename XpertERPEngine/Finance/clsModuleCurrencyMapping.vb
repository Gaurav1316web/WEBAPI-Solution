' -- Ticket No- BM00000002142 by Puran Singh Negi
' -- Ticket No - BM00000002228 by Puran Singh Negi

Imports common
Imports System.Data.SqlClient


Public Class clsModuleCurrencyMapping
#Region "Variables"
    Public Module_Code As String = Nothing
    Public Module_Name As String = Nothing
    Public Apply As Boolean

#End Region

    Public Shared Function SaveData(ByVal Arr As List(Of clsModuleCurrencyMapping)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_Module_Currency_Mapping where comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsModuleCurrencyMapping In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Module_Code", obj.Module_Code)
                    clsCommon.AddColumnsForChange(coll, "Module_Name", obj.Module_Name)
                    clsCommon.AddColumnsForChange(coll, "Apply", IIf(obj.Apply, 1, 0))

                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Module_Currency_Mapping", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Public Shared Function GetData() As List(Of clsModuleCurrencyMapping)
        Dim Arr As List(Of clsModuleCurrencyMapping) = Nothing
        Dim qry As String = "SELECT module.Module_Code as Module_Code,module.Module_Name as Module_Name,mapping.Apply from  " & _
        " (select distinct Program_Code as Module_Code,Program_Name AS Module_Name  from TSPL_PROGRAM_MASTER where Type='M' and Program_Code not in ('MFavourite','MUtility','MSysAdmin','MSales','MMSPS','MSaleDairy')) as module  " & _
        " left join (select * from TSPL_Module_Currency_Mapping where comp_code='" + objCommonVar.CurrentCompanyCode + "') as mapping on module.Module_Code=mapping.Module_Code  order by Module_Code"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsModuleCurrencyMapping)
            Dim objTr As clsModuleCurrencyMapping
            For Each dr As DataRow In dt.Rows
                objTr = New clsModuleCurrencyMapping
                objTr.Module_Code = clsCommon.myCstr(dr("Module_Code"))
                objTr.Module_Name = clsCommon.myCstr(dr("Module_Name"))
                objTr.Apply = clsCommon.myCBool(dr("Apply"))

                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
    Public Shared Function CheckMultiCurrency(ByVal Module_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim strq As String
        Dim dt As DataTable
        strq = "SELECT ApplyMultiCurrency FROM TSPL_COMPANY_MASTER WHERE Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count = 0 Then
            Return False
        Else
            If dt.Rows(0).Item("ApplyMultiCurrency") = True Then
                strq = "select * from tspl_module_currency_mapping where comp_code='" + objCommonVar.CurrentCompanyCode + "' and module_code='" & Module_Code & "'"
                dt = clsDBFuncationality.GetDataTable(strq, trans)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("Apply") = True Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
                'Return True
            Else
                Return False
            End If
        End If
        'strq = "select * from tspl_module_currency_mapping where module_code='" & Module_Code & "'"
        'dt = clsDBFuncationality.GetDataTable(strq)
        'If dt.Rows.Count > 0 Then
        '    If dt.Rows(0).Item("Apply") = True Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'Else
        '    Return False
        'End If
    End Function
    Public Shared Function GetLatestCurConvRateDT(ByVal TransDate As Date, ByVal CurrencyCode As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim strq As String
        strq = "select  top 1 FROM_CURRENCY,Rate,FROM_DATE from TSPL_CURRENCY_CONVERSION_RATE where TO_CURRENCY='" & objCommonVar.BaseCurrencyCode & "' and FROM_CURRENCY='" & CurrencyCode & "' and FROM_DATE<='" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "' order by FROM_DATE desc"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)

        Return dt
    End Function
    Public Shared Function GetmulticurrencyDecimalPlaces()
        Dim strq As Integer
        strq = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='GetMulitcurrencyDecimalPlaces' and TYPE='MulticurrencyDecimalPlaces'")
        'Dim dt As DataTable
        'dt = clsDBFuncationality.GetDataTable(strq)
        Return strq
    End Function
End Class



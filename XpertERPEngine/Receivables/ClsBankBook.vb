''created by richa against ticket no ERO/22/10/19-001074
Imports System.Data.SqlClient
Imports common
Public Class ClsBankBook

#Region "Variables"
    Public SOURCEDOC_NO As String = Nothing
    Public SOURCEDOC_DATE As Date
    Public SOURCE_CODE As String = Nothing
    Public SOURCE_NAME As String = Nothing
    Public BANK_CODE As String = Nothing
    Public BANK_NAME As String = Nothing
    Public LOC_CODE As String = Nothing
    Public LOC_NAME As String = Nothing
    Public BANKGL_Account_Code As String = Nothing
    Public BANKGL_Account_Name As String = Nothing
    Public GL_Account_Code As String = Nothing
    Public GL_Account_Name As String = Nothing
    Public CHEQUE_NO As String = Nothing
    Public CHEQUE_DATE As String = Nothing
    Public NARR_MASTER As String = Nothing
    Public NARR_DETAIL As String = Nothing
    Public Debit_Amount As Double = 0
    Public Credit_Amount As Double = 0
    Public DocType As String = Nothing
    Public TransactionType As String = Nothing
    Public line_No As Integer = 0
    Public Currency As String = Nothing
    Public Base_Currency As String = Nothing
    Public Cheque_Dated As Date? = Nothing
    Public Conversion_Rate As Double = 0.0
    Public Remarks As String = Nothing
    Public Bank_charges As Double = 0.0

    'Public Set_Off_Date As Date?
    'Public SetOffSkipJE As Boolean

#End Region

    'Public Shared Function SaveData(ByVal obj As ClsBankBook, ByVal isNewEntry As Boolean) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        SaveData(obj, isNewEntry, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function


    Public Shared Function SaveData(ByVal ArrBankBook As List(Of ClsBankBook), ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsBankBook In ArrBankBook

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SOURCEDOC_NO", obj.SOURCEDOC_NO)
                clsCommon.AddColumnsForChange(coll, "SOURCEDOC_DATE", clsCommon.GetPrintDate(obj.SOURCEDOC_DATE, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "SOURCE_CODE", obj.SOURCE_CODE)
                clsCommon.AddColumnsForChange(coll, "SOURCE_NAME", obj.SOURCE_NAME)
                clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE)
                clsCommon.AddColumnsForChange(coll, "BANK_NAME", obj.BANK_NAME)
                clsCommon.AddColumnsForChange(coll, "LOC_CODE", obj.LOC_CODE)
                clsCommon.AddColumnsForChange(coll, "LOC_NAME", obj.LOC_NAME)
                clsCommon.AddColumnsForChange(coll, "BANKGL_Account_Code", obj.BANKGL_Account_Code)
                clsCommon.AddColumnsForChange(coll, "BANKGL_Account_Name", obj.BANKGL_Account_Name)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Name", obj.GL_Account_Name)
                clsCommon.AddColumnsForChange(coll, "CHEQUE_NO", obj.CHEQUE_NO)
                If clsCommon.myLen(obj.CHEQUE_DATE) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.CHEQUE_DATE, "dd/MM/yyyy"), True)
                Else
                    clsCommon.AddColumnsForChange(coll, "Cheque_Date", "", True)
                End If

                clsCommon.AddColumnsForChange(coll, "NARR_MASTER", obj.NARR_MASTER)
                clsCommon.AddColumnsForChange(coll, "NARR_DETAIL", obj.NARR_DETAIL)
                clsCommon.AddColumnsForChange(coll, "Debit_Amount", obj.Debit_Amount)
                clsCommon.AddColumnsForChange(coll, "Credit_Amount", obj.Credit_Amount)
                clsCommon.AddColumnsForChange(coll, "DocType", obj.DocType)
                clsCommon.AddColumnsForChange(coll, "TransactionType", obj.TransactionType)
                clsCommon.AddColumnsForChange(coll, "line_No", obj.line_No)
                clsCommon.AddColumnsForChange(coll, "Currency", obj.Currency)
                clsCommon.AddColumnsForChange(coll, "Base_Currency", obj.Base_Currency)

                ''richa GKD/07/11/19-000188
                'If clsCommon.myLen(obj.Cheque_Dated) > 0 Then
                '    clsCommon.AddColumnsForChange(coll, "Cheque_Dated", clsCommon.GetPrintDate(obj.Cheque_Dated, "dd/MM/yyyy"), True)
                'Else
                '    clsCommon.AddColumnsForChange(coll, "Cheque_Dated", "", True)
                'End If
                clsCommon.AddColumnsForChange(coll, "Conversion_Rate", obj.Conversion_Rate)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Bank_charges", obj.Bank_charges)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_BOOK", OMInsertOrUpdate.Insert, "", trans)
            Next

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function fundelete(ByVal strSourceDocNo As String, ByVal strDocType As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            fundelete(strSourceDocNo, strDocType, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function fundelete(ByVal strSourceDocNo As String, ByVal strDocType As String, ByVal trans As SqlTransaction) As Boolean
        Try

            clsDBFuncationality.ExecuteNonQuery("Delete from  TSPL_BANK_BOOK where  SOURCEDOC_NO ='" + strSourceDocNo + "' and DocType='" & strDocType & "'", trans)
            Return True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
End Class
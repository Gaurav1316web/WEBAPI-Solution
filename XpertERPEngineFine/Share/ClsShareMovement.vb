Imports System.Data.SqlClient
Public Class ClsShareMovement

#Region "Variables"
    Public PK_Id As Integer = 0
    Public Source_Code As String = Nothing
    Public Source_Date As DateTime
    Public Source_Type As String = Nothing
    Public Share_Code As String = Nothing
    Public Certificate_No As String = Nothing
    Public RI As Integer = 0
    Public Rate As Decimal = 0
    Public Amount As Decimal = 0
    Public Status As Integer = 0
    Public Created_By As String = Nothing
    Public Created_Date As DateTime

#End Region


    'Public Shared Function SaveData(ByVal strDocNo As String) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        SaveData(strDocNo, trans)
    '        'trans.Commit()
    '    Catch ex As Exception
    '        'trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    Public Shared Function SaveData(ByVal Arr As List(Of ClsShareMovement), ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            ''First delete
            Dim objTr As New ClsShareMovement()
            Dim StrQry As String = "delete from TSPL_SHARE_MOVEMENT where Source_Code='" + objtr.Source_Code + "'  and Source_Type='" + objtr.Source_Type + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Source_Code", objTr.Source_Code)
                    clsCommon.AddColumnsForChange(coll, "Source_Date", clsCommon.GetPrintDate(objTr.Source_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Source_Type", objTr.Source_Type)
                    clsCommon.AddColumnsForChange(coll, "Share_Code", objTr.Share_Code)
                    clsCommon.AddColumnsForChange(coll, "Certificate_No", objTr.Certificate_No)
                    'clsCommon.AddColumnsForChange(coll, "Certificate_No", ((Integer.Parse(obj.Certificate_No) + i) - 1).ToString()) ' Increment Certificate_No
                    clsCommon.AddColumnsForChange(coll, "RI", objTr.RI)
                    clsCommon.AddColumnsForChange(coll, "Rate", objTr.Rate)
                    clsCommon.AddColumnsForChange(coll, "Amount", objTr.Rate)
                    clsCommon.AddColumnsForChange(coll, "Status", objTr.Status)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHARE_MOVEMENT", OMInsertOrUpdate.Insert, "Share_Code='" + clsCommon.myCstr(objTr.Share_Code) + "'", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsShareMovement
        Dim obj1 As ClsShareMovement = Nothing

        Try
            Dim strQry As String = "select * from TSPL_SHARE_MOVEMENT where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Share_Code = (select MIN(Code) from TSPL_SHARE_MOVEMENT where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Share_Code = (Select Max(Code) from TSPL_SHARE_MOVEMENT where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Share_Code = (Select Min(Code) from TSPL_SHARE_MOVEMENT where Share_Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Share_Code = (select Max(Code) from TSPL_SHARE_MOVEMENT where Share_Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Share_Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj1 = New ClsShareMovement()
                obj1.Source_Code = clsCommon.myCstr(dt.Rows(0)("Source_Code"))
                obj1.Source_Date = clsCommon.myCstr(dt.Rows(0)("Source_Date"))
                obj1.Source_Type = clsCommon.myCstr(dt.Rows(0)("Source_Type"))
                obj1.Share_Code = clsCommon.myCstr(dt.Rows(0)("Share_Code"))
                obj1.Certificate_No = clsCommon.myCstr(dt.Rows(0)("Certificate_No"))
                obj1.RI = clsCommon.myCstr(dt.Rows(0)("RI"))
                obj1.Rate = clsCommon.myCstr(dt.Rows(0)("Rate"))
                obj1.Amount = clsCommon.myCstr(dt.Rows(0)("Amount"))
                obj1.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                obj1.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
                obj1.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj1
    End Function


End Class

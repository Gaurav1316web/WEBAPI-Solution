Imports System.Data.SqlClient
Public Class clsBullInsuranceMaster
    Public Code As String = Nothing
    Public Name As String = Nothing


    Public Function SaveData(ByVal obj As clsBullInsuranceMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsBullInsuranceMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullBreedMaster, "", objCommonVar.strCurrUserLocations)
                'If clsCommon.myLen(obj.Code) <= 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_INSURANCE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_INSURANCE_MASTER", OMInsertOrUpdate.Update, "TSPL_BULL_INSURANCE_MASTER.Code='" + obj.Code + "'", trans)
            End If

            'IsSaved = IsSaved AndAlso clsNotificationDetails.SaveData(obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullInsuranceMaster
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsBullInsuranceMaster = GetData(strCode, NavType, trans)
            trans.Commit()

            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBullInsuranceMaster
        Dim obj As clsBullInsuranceMaster = Nothing

        Try
            Dim strQry As String = "select * from TSPL_BULL_INSURANCE_MASTER where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Code = (select MIN(Code) from TSPL_BULL_INSURANCE_MASTER where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Code = (Select Max(Code) from TSPL_BULL_INSURANCE_MASTER where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Code = (Select Min(Code) from TSPL_BULL_INSURANCE_MASTER where Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Code = (select Max(Code) from TSPL_BULL_INSURANCE_MASTER where Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBullInsuranceMaster()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BULL_INSURANCE_MASTER where code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

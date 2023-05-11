Imports common
Imports System.Data.SqlClient

Public Class clsPartNoMaster
#Region "variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Brand As String = Nothing
    Public Type As String = Nothing
    Public Released_By As String = Nothing
    Public Sub_Part As String = Nothing
    Public Released_Date As Date = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select Code,Description,Brand ,Type ,Released_By as [Released By],convert(varchar,Released_Date,103) as [Released date],Sub_Part as [Sub Part],Created_By as [Created By],created_date as [Created Date] from TSPL_PART_NO_MASTER "
        str = clsCommon.ShowSelectForm("PARTNOFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsPartNoMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsPartNoMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Brand", obj.Brand)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Released_By", obj.Released_By)
            clsCommon.AddColumnsForChange(coll, "Released_Date", clsCommon.GetPrintDate(obj.Released_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Sub_Part", obj.Sub_Part)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PART_NO_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PART_NO_MASTER", OMInsertOrUpdate.Update, " code='" + obj.Code + "'", trans)
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_PART_NO_MASTER where code='" + strCode + "'", trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsPartNoMaster
        Dim obj As New clsPartNoMaster()
        Dim dt As New DataTable()
        Try
            Dim qry As String = "select * from tspl_part_no_master where 1=1 "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and code in (select min(code) from tspl_part_no_master)"
                Case NavigatorType.Last
                    qry += " and code in (select max(code) from tspl_part_no_master)"
                Case NavigatorType.Next
                    qry += " and code in (select min(code) from tspl_part_no_master where code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and code in (select max(code) from tspl_part_no_master where code>'" + strCode + "')"
            End Select
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Code = clsCommon.myCstr(dt.Rows(0)("code"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.Brand = clsCommon.myCstr(dt.Rows(0)("Brand"))
                obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
                obj.Released_By = clsCommon.myCstr(dt.Rows(0)("Released_By"))
                obj.Released_Date = clsCommon.myCDate(dt.Rows(0)("Released_Date"))
                obj.Sub_Part = clsCommon.myCstr(dt.Rows(0)("Sub_Part"))
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try
        Return obj
    End Function
End Class

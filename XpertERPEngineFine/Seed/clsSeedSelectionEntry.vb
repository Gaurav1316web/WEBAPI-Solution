Imports System.Data.SqlClient
Public Class clsSeedSelectionEntry
    Public Document_No As String = Nothing
    Public Document_Date As DateTime? = Nothing
    Public PO_Code As String = Nothing
    Public Grower_Code As String = String.Empty
    Public Crop_Reg_Code As String = Nothing
    Public Crop_Location As String = Nothing
    Public Area As String = Nothing
    Public Item_Code As String = Nothing
    Public Khasra_No As String = Nothing
    Public Season As String = Nothing
    Public Village_Code As String = Nothing
    Public DISTRICT_Code As String = Nothing
    Public Selected_Flag As String
    Public Sowing_Week_Month As String
    Public Own_Land As Decimal = 0
    Public Family_Land As Decimal = 0
    Public Lease_Land As Decimal = 0
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending

    Public Function SaveData(ByVal obj As clsSeedSelectionEntry, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsSeedSelectionEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try

            IsSaved = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PO_Code", obj.PO_Code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Grower_Code", obj.Grower_Code, True)
            clsCommon.AddColumnsForChange(coll, "Crop_Reg_Code", obj.Crop_Reg_Code)
            clsCommon.AddColumnsForChange(coll, "Crop_Location", obj.Crop_Location)
            clsCommon.AddColumnsForChange(coll, "Village_Code", obj.Village_Code)
            clsCommon.AddColumnsForChange(coll, "Area", obj.Area)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "Khasra_No", obj.Khasra_No)
            clsCommon.AddColumnsForChange(coll, "Season", obj.Season)
            clsCommon.AddColumnsForChange(coll, "DISTRICT_Code", obj.DISTRICT_Code, True)
            clsCommon.AddColumnsForChange(coll, "Own_Land", obj.Own_Land)
            clsCommon.AddColumnsForChange(coll, "Family_Land", obj.Family_Land)
            clsCommon.AddColumnsForChange(coll, "Lease_Land", obj.Lease_Land)
            clsCommon.AddColumnsForChange(coll, "Selected_Flag", obj.Selected_Flag)
            clsCommon.AddColumnsForChange(coll, "Sowing_Week_Month", obj.Sowing_Week_Month)
            'clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SeedSelectionEntry, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SEED_GROWER_SELECTION_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SEED_GROWER_SELECTION_ENTRY", OMInsertOrUpdate.Update, "TSPL_SEED_GROWER_SELECTION_ENTRY.Document_No='" + obj.Document_No + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSeedSelectionEntry
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsSeedSelectionEntry = GetData(strCode, NavType, trans)
            trans.Commit()

            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSeedSelectionEntry
        Dim obj As clsSeedSelectionEntry = Nothing

        Try
            Dim strQry As String = "select * from TSPL_SEED_GROWER_SELECTION_ENTRY where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Document_No = (select MIN(Document_No) from TSPL_SEED_GROWER_SELECTION_ENTRY where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Document_No = (Select Max(Document_No) from TSPL_SEED_GROWER_SELECTION_ENTRY where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Document_No = (Select Min(Document_No) from TSPL_SEED_GROWER_SELECTION_ENTRY where Document_No>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Document_No = (select Max(Document_No) from TSPL_SEED_GROWER_SELECTION_ENTRY where Document_No<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Document_No = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsSeedSelectionEntry()
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.PO_Code = clsCommon.myCstr(dt.Rows(0)("PO_Code"))
                obj.Grower_Code = clsCommon.myCstr(dt.Rows(0)("Grower_Code"))
                obj.Crop_Reg_Code = clsCommon.myCstr(dt.Rows(0)("Crop_Reg_Code"))
                obj.Crop_Location = clsCommon.myCstr(dt.Rows(0)("Crop_Location"))
                obj.Area = clsCommon.myCstr(dt.Rows(0)("Area"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Khasra_No = clsCommon.myCstr(dt.Rows(0)("Khasra_No"))
                obj.Season = clsCommon.myCstr(dt.Rows(0)("Season"))
                obj.Village_Code = clsCommon.myCstr(dt.Rows(0)("Village_Code"))
                obj.DISTRICT_Code = clsCommon.myCstr(dt.Rows(0)("DISTRICT_Code"))
                obj.Own_Land = clsCommon.myCstr(dt.Rows(0)("Own_Land"))
                obj.Family_Land = clsCommon.myCstr(dt.Rows(0)("Family_Land"))
                obj.Lease_Land = clsCommon.myCstr(dt.Rows(0)("Lease_Land"))
                obj.Selected_Flag = clsCommon.myCstr(dt.Rows(0)("Selected_Flag"))
                obj.Sowing_Week_Month = clsCommon.myCstr(dt.Rows(0)("Sowing_Week_Month"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No. not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim qry As String = "Update TSPL_SEED_GROWER_SELECTION_ENTRY set Status=1,Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim isResponse As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                isResponse = True
            Else
                isResponse = False
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsSeedSelectionEntry = clsSeedSelectionEntry.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                isResponse = False
            End If

            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                isResponse = False
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SEED_GROWER_SELECTION_ENTRY", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
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
            Dim qry As String = "delete from TSPL_SEED_GROWER_SELECTION_ENTRY where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Imports System.Data.SqlClient

Public Class clsSilageFarmerMaster

#Region "Variables"
    Public Farmer_Code As String = Nothing
    Public Entr_Code As String = Nothing
    Public Area_Code As String = Nothing
    '===================================================
    Public Name As String = Nothing
    Public Gender As String = Nothing
    Public IsMarried As String = Nothing
    Public Add1 As String = Nothing
    Public Add2 As String = Nothing
    Public Add3 As String = Nothing
    Public Vlg_Code As String = Nothing
    Public Vlg_Name As String = Nothing
    Public PostOfc As String = Nothing
    Public Tehsil As String = Nothing
    Public Dst_Code As String = Nothing
    Public District As String = Nothing
    Public Phone As String = Nothing
    Public Mobile As String = Nothing
    Public FodderArea As Decimal = 0
    Public TotalArea As Decimal = 0
    Public PAN As String = Nothing
    Public Tin_No As String = Nothing
    '===================================================
    Public Account_No As String = Nothing
    Public Bank_Code As String = Nothing
    Public Bank_Name As String = Nothing
    Public Branch_Name As String = Nothing
    Public IFSC_Code As String = Nothing
    Public Bnk_Country As String = Nothing
    Public Bnk_State As String = Nothing
    Public Bnk_City As String = Nothing
    '===================================================
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public Arr_clsSilageFarmerDetail As List(Of clsSilageFarmerDetail) = Nothing
    Public Arr_clsSilageFarmerChildVendor As List(Of clsSilageFarmerChildVendor) = Nothing



#End Region

    Public Shared Function SaveData(ByVal obj As clsSilageFarmerMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlClient.SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            If (SaveData(obj, isNewEntry, trans)) = True Then
                trans.Commit()
                Return True
            Else
                trans.Rollback()
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsSilageFarmerMaster, ByVal IsNewEntry As Boolean, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim coll As Hashtable = Nothing
        Dim isImport As Boolean = False
        Try
            '== CASE: WHEN SAVING FROM IMPORT FROM EXCEL ==================================
            If obj.Farmer_Code.Length > 0 Then
                IsNewEntry = False
                If obj.Entr_Code.Length > 0 Then
                    clsSilageFarmerChildVendor.SaveData(obj, IsNewEntry, trans, False)

                End If
              End If
            '== CASE: WHEN NEW ENTRY SAVE FROM FORM ==================================

            If IsNewEntry Then
                obj.Farmer_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.frmSilageFarmerSelection, "", "")
            End If

            '==========================================================================
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Farmer_Code", clsCommon.myCstr(obj.Farmer_Code))
            clsCommon.AddColumnsForChange(coll, "Entr_Code", clsCommon.myCstr(obj.Entr_Code))
            clsCommon.AddColumnsForChange(coll, "Area_Code", clsCommon.myCstr(obj.Area_Code))
            '===================================================
            clsCommon.AddColumnsForChange(coll, "Name", clsCommon.myCstr(obj.Name), True)
            clsCommon.AddColumnsForChange(coll, "Gender", clsCommon.myCstr(obj.Gender), True)
            clsCommon.AddColumnsForChange(coll, "IsMarried", clsCommon.myCstr(obj.IsMarried), True)
            clsCommon.AddColumnsForChange(coll, "Add1", clsCommon.myCstr(obj.Add1), True)
            clsCommon.AddColumnsForChange(coll, "Add2", clsCommon.myCstr(obj.Add2), True)
            clsCommon.AddColumnsForChange(coll, "Add3", clsCommon.myCstr(obj.Add3), True)
            clsCommon.AddColumnsForChange(coll, "Vlg_Code", clsCommon.myCstr(obj.Vlg_Code), True)
            clsCommon.AddColumnsForChange(coll, "Vlg_Name", clsCommon.myCstr(obj.Vlg_Name), True)
            clsCommon.AddColumnsForChange(coll, "PostOfc", clsCommon.myCstr(obj.PostOfc), True)
            clsCommon.AddColumnsForChange(coll, "Tehsil", clsCommon.myCstr(obj.Tehsil), True)
            clsCommon.AddColumnsForChange(coll, "Dst_Code", clsCommon.myCstr(obj.Dst_Code), True)
            clsCommon.AddColumnsForChange(coll, "District", clsCommon.myCstr(obj.District), True)
            clsCommon.AddColumnsForChange(coll, "Phone", clsCommon.myCstr(obj.Phone), True)
            clsCommon.AddColumnsForChange(coll, "Mobile", clsCommon.myCstr(obj.Mobile), True)
            clsCommon.AddColumnsForChange(coll, "FodderArea", clsCommon.myCdbl(obj.FodderArea), True)
            clsCommon.AddColumnsForChange(coll, "TotalArea", clsCommon.myCdbl(obj.TotalArea), True)
            clsCommon.AddColumnsForChange(coll, "PAN", clsCommon.myCstr(obj.PAN), True)
            clsCommon.AddColumnsForChange(coll, "Tin_No", clsCommon.myCstr(obj.Tin_No), True)
            '===================================================
            clsCommon.AddColumnsForChange(coll, "Account_No", clsCommon.myCstr(obj.Account_No), True)
            clsCommon.AddColumnsForChange(coll, "Bank_Code", clsCommon.myCstr(obj.Bank_Code), True)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", clsCommon.myCstr(obj.Bank_Name), True)
            clsCommon.AddColumnsForChange(coll, "Branch_Name", clsCommon.myCstr(obj.Branch_Name), True)
            clsCommon.AddColumnsForChange(coll, "IFSC_Code", clsCommon.myCstr(obj.IFSC_Code), True)
            clsCommon.AddColumnsForChange(coll, "Bnk_Country", clsCommon.myCstr(obj.Bnk_Country), True)
            clsCommon.AddColumnsForChange(coll, "Bnk_State", clsCommon.myCstr(obj.Bnk_State), True)
            clsCommon.AddColumnsForChange(coll, "Bnk_City", clsCommon.myCstr(obj.Bnk_City), True)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If IsNewEntry Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_FARMER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                clsSilageFarmerDetail.DeleteData(obj.Entr_Code, trans)
            Else
                If isImport Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_FARMER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    clsSilageFarmerDetail.DeleteData(obj.Entr_Code, trans)
                Else
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_FARMER_MASTER", OMInsertOrUpdate.Update, "TSPL_SILAGE_CRITERIA_FARMER_MASTER.Farmer_Code  = '" & obj.Farmer_Code & "'", trans)
                    clsSilageFarmerDetail.DeleteData(obj.Entr_Code, trans)
                End If
            End If
            '========================================================================================

            If obj.Entr_Code.Length > 0 Then
                If isImport Then

                Else
                    clsSilageFarmerChildVendor.SaveData(obj, IsNewEntry, trans, False)
                End If

            End If

            ' == FARMER CRITERIAS DETAILS ===

            clsSilageFarmerDetail.SaveData(obj, IsNewEntry, trans)
            
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlClient.SqlTransaction) As clsSilageFarmerMaster
        Dim obj As clsSilageFarmerMaster
        Dim objD As clsSilageFarmerDetail
        Dim objV As clsSilageFarmerChildVendor

        Dim arrTables() As String = {"TSPL_SILAGE_CRITERIA_FARMER_MASTER", "TSPL_SILAGE_CRITERIA_FARMER_DETAIL"}
        Dim colName As String = "Farmer_Code"
        Try

            Dim selectQry = "SELECT * FROM TSPL_SILAGE_CRITERIA_FARMER_MASTER WHERE 1=1"

            Select Case NavType
                Case NavigatorType.First
                    selectQry += " and " & arrTables(0) & "." & colName & "  = (select MIN(" & colName & ") from " & arrTables(0) & " where 1=1 )"
                Case NavigatorType.Last
                    selectQry += " and " & arrTables(0) & "." & colName & "  = (select Max(" & colName & ") from " & arrTables(0) & " where 1=1 )"
                Case NavigatorType.Next
                    selectQry += " and " & arrTables(0) & "." & colName & "  = (select Min(" & colName & ") from " & arrTables(0) & " where " & colName & ">'" & strCode & "' )"
                Case NavigatorType.Previous
                    selectQry += " and " & arrTables(0) & "." & colName & "  = (select Max(" & colName & ") from " & arrTables(0) & " where " & colName & "<'" & strCode & "' )"
                Case NavigatorType.Current
                    selectQry += " and " & arrTables(0) & "." & colName & "  = '" + strCode + "'"
            End Select

            obj = New clsSilageFarmerMaster()
            '==================================================
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(selectQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows

                    obj.Farmer_Code = clsCommon.myCstr(dt.Rows(0)("Farmer_Code"))
                    obj.Entr_Code = clsCommon.myCstr(dt.Rows(0)("Entr_Code"))
                    obj.Area_Code = clsCommon.myCstr(dt.Rows(0)("Area_Code"))
                    '===================================================
                    obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                    obj.Gender = clsCommon.myCstr(dt.Rows(0)("Gender"))
                    obj.IsMarried = clsCommon.myCstr(dt.Rows(0)("IsMarried"))
                    obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                    obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                    obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                    obj.Vlg_Code = clsCommon.myCstr(dt.Rows(0)("Vlg_Code"))
                    obj.Vlg_Name = clsCommon.myCstr(dt.Rows(0)("Vlg_Name"))
                    obj.PostOfc = clsCommon.myCstr(dt.Rows(0)("PostOfc"))
                    obj.Tehsil = clsCommon.myCstr(dt.Rows(0)("Tehsil"))
                    obj.Dst_Code = clsCommon.myCstr(dt.Rows(0)("Dst_Code"))
                    obj.District = clsCommon.myCstr(dt.Rows(0)("District"))
                    obj.Phone = clsCommon.myCstr(dt.Rows(0)("Phone"))
                    obj.Mobile = clsCommon.myCstr(dt.Rows(0)("Mobile"))
                    obj.FodderArea = clsCommon.myCdbl(dt.Rows(0)("FodderArea"))
                    obj.TotalArea = clsCommon.myCdbl(dt.Rows(0)("TotalArea"))
                    obj.PAN = clsCommon.myCstr(dt.Rows(0)("PAN"))
                    obj.Tin_No = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
                    '===================================================
                    obj.Account_No = clsCommon.myCstr(dt.Rows(0)("Account_No"))
                    obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
                    obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
                    obj.Branch_Name = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
                    obj.IFSC_Code = clsCommon.myCstr(dt.Rows(0)("IFSC_Code"))
                    obj.Bnk_Country = clsCommon.myCstr(dt.Rows(0)("Bnk_Country"))
                    obj.Bnk_State = clsCommon.myCstr(dt.Rows(0)("Bnk_State"))
                    obj.Bnk_City = clsCommon.myCstr(dt.Rows(0)("Bnk_City"))
                    '===================================================
                    obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                    obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
                    obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
                    obj.Modified_Date = clsCommon.myCstr(dt.Rows(0)("Modified_Date"))
                Next
            End If

            objD = New clsSilageFarmerDetail()
            '==================================================
            obj.Arr_clsSilageFarmerDetail = New List(Of clsSilageFarmerDetail)
            Dim dtD As DataTable = clsSilageFarmerDetail.GetData(obj.Farmer_Code, NavType, trans)
            If (dtD IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dtD.Rows
                    objD.Farmer_Code = clsCommon.myCstr(dtD.Rows(0)("Farmer_Code"))
                    objD.Entr_Code = clsCommon.myCstr(dtD.Rows(0)("Entr_Code"))
                    objD.Area_Code = clsCommon.myCstr(dtD.Rows(0)("Area_Code"))
                    objD.Criteria_Code = clsCommon.myCstr(dtD.Rows(0)("Criteria_Code"))
                    objD.Value = clsCommon.myCstr(dtD.Rows(0)("Value"))
                    objD.Description = clsCommon.myCstr(dtD.Rows(0)("Description"))
                    obj.Arr_clsSilageFarmerDetail.Add(objD)
                Next
            End If

            objV = New clsSilageFarmerChildVendor()
            '==============================================================
            obj.Arr_clsSilageFarmerChildVendor = New List(Of clsSilageFarmerChildVendor)
            Dim dtV = clsSilageFarmerChildVendor.GetData(obj.Farmer_Code, NavType, trans)
            If (dtV IsNot Nothing AndAlso dtV.Rows.Count > 0) Then
                For Each dr As DataRow In dtV.Rows
                    objV.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objV.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    ' objV.State_Code = clsCommon.myCstr(dr("State_Code"))
                    ' objV.Country_Code = clsCommon.myCstr(dr("Country_Code"))
                    objV.Is_Parent_Vendor = clsCommon.myCstr(dr("Is_Parent_Vendor"))
                    objV.Parent_Vendor_Code = clsCommon.myCstr(dr("Parent_Vendor_Code"))
                    objV.Vendor_Type = clsCommon.myCstr(dr("Vendor_Type"))
                    objV.Pin_Code = clsCommon.myCstr(dr("Pin_Code"))
                    objV.Add1 = clsCommon.myCstr(dr("Add1"))
                    objV.Add2 = clsCommon.myCstr(dr("Add2"))
                    objV.Add3 = clsCommon.myCstr(dr("Add3"))
                    ' objV.City_Code = clsCommon.myCstr(dr("City_Code"))
                    ' objV.State = clsCommon.myCstr(dr("State"))
                    ' objV.Country = clsCommon.myCstr(dr("Country"))
                    objV.Phone1 = clsCommon.myCstr(dr("Phone1"))
                    objV.Phone2 = clsCommon.myCstr(dr("Phone2"))
                    ' objV.Email = clsCommon.myCstr(dr("Email"))
                    ' objV.WebSite = clsCommon.myCstr(dr("WebSite"))
                    objV.Transporter = clsCommon.myCstr(dr("Transporter"))
                    objV.Created_By = clsCommon.myCstr(dr("Created_By"))
                    objV.Created_Date = clsCommon.myCstr(dr("Created_Date"))
                    objV.Modify_By = clsCommon.myCstr(dr("Modify_By"))
                    objV.Modify_Date = clsCommon.myCstr(dr("Modify_Date"))
                    obj.Arr_clsSilageFarmerChildVendor.Add(objV)
                Next
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal farmerCode As String) As Boolean

        Dim transaction As SqlClient.SqlTransaction = clsDBFuncationality.GetTransactin
        Dim obj As clsSilageFarmerMaster = clsSilageFarmerMaster.GetData(farmerCode, NavigatorType.Current, transaction)
        '=========================================================================================================
        Try
            If (clsCommon.myLen(farmerCode) <= 0) Then
                Throw New Exception("Supplied blank value is not valid to delete ")
                Return False
            Else
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Farmer_Code) > 0 Then

                    If (clsSilageFarmerDetail.DeleteData(farmerCode, transaction)) = True Then
                        Dim deleteQuery As String = "DELETE FROM TSPL_SILAGE_CRITERIA_FARMER_MASTER WHERE Farmer_Code ='" & farmerCode & "'"
                        '=============================================================================================================================
                        If (clsDBFuncationality.ExecuteNonQuery(deleteQuery, transaction)) Then
                            transaction.Commit()
                            Return True
                        Else
                            transaction.Rollback()
                            Return False
                        End If
                    End If
                End If
            End If

        Catch Err As Exception
            Throw New Exception(Err.Message)
            transaction.Rollback()
            Return False
        End Try

        Return True
    End Function

End Class

Public Class clsSilageFarmerDetail
#Region " Variables"
    Public Farmer_Code As String = Nothing
    Public Entr_Code As String = Nothing
    Public Area_Code As String = Nothing
    Public Criteria_Code As String = Nothing
    Public Value As String = Nothing
    Public Description As String = Nothing
#End Region

    Public Shared Function GetData(Farmer_Code As String, NavType As NavigatorType, ByVal trans As SqlClient.SqlTransaction) As DataTable
        Dim dt As DataTable = Nothing

        Dim arrTables() As String = {"TSPL_SILAGE_CRITERIA_FARMER_MASTER", "TSPL_SILAGE_CRITERIA_FARMER_DETAIL"}
        Dim colName As String = "Farmer_Code"
        Try

            Dim selectQry = "SELECT * FROM TSPL_SILAGE_CRITERIA_FARMER_DETAIL WHERE 1=1"

            Select Case NavType
                Case NavigatorType.First
                    selectQry += " and " & arrTables(1) & "." & colName & "  = (select MIN(" & colName & ") from " & arrTables(1) & " where 1=1 )"
                Case NavigatorType.Last
                    selectQry += " and " & arrTables(1) & "." & colName & "  = (select Max(" & colName & ") from " & arrTables(1) & " where 1=1 )"
                Case NavigatorType.Next
                    selectQry += " and " & arrTables(1) & "." & colName & "  = (select Min(" & colName & ") from " & arrTables(1) & " where " & colName & ">'" & Farmer_Code & "' )"
                Case NavigatorType.Previous
                    selectQry += " and " & arrTables(1) & "." & colName & "  = (select Max(" & colName & ") from " & arrTables(1) & " where " & colName & "<'" & Farmer_Code & "' )"
                Case NavigatorType.Current
                    selectQry += " and " & arrTables(1) & "." & colName & "  = '" + Farmer_Code + "'"
            End Select
            dt = clsDBFuncationality.GetDataTable(selectQry, trans)
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Public Shared Function SaveData(ByVal obj As clsSilageFarmerMaster, ByVal isNewEntry As Boolean, trans As SqlClient.SqlTransaction) As Boolean
        Dim coll As Hashtable = Nothing
        Try

            If obj.Arr_clsSilageFarmerDetail IsNot Nothing AndAlso obj.Arr_clsSilageFarmerDetail.Count > 0 Then

                For Each oDtl As clsSilageFarmerDetail In obj.Arr_clsSilageFarmerDetail
                    coll = New Hashtable()
                    '=======================================================================
                    clsCommon.AddColumnsForChange(coll, "Farmer_Code ", obj.Farmer_Code)
                    clsCommon.AddColumnsForChange(coll, "Entr_Code ", obj.Entr_Code)
                    clsCommon.AddColumnsForChange(coll, "Area_Code ", oDtl.Area_Code)
                    clsCommon.AddColumnsForChange(coll, "Criteria_Code ", oDtl.Criteria_Code)
                    clsCommon.AddColumnsForChange(coll, "Value ", oDtl.Value)
                    clsCommon.AddColumnsForChange(coll, "Description ", oDtl.Description)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_FARMER_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    'If isNewEntry Then
                    '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_FARMER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    'Else
                    '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_FARMER_DETAIL", OMInsertOrUpdate.Update, "TSPL_SILAGE_CRITERIA_FARMER_DETAIL.Farmer_Code = '" & oDtl.Farmer_Code & "'", trans)
                    'End If
                Next
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal farmerCode As String, ByVal transaction As SqlClient.SqlTransaction) As Boolean
        Dim deleteQuery As String = String.Empty
        Try

            If (clsSilageFarmerChildVendor.DeleteData(farmerCode, transaction)) Then
                '   deleteQuery = "DELETE FROM TSPL_SILAGE_CRITERIA_FARMER_DETAIL WHERE Farmer_Code ='" & farmerCode & "'"
                deleteQuery = "DELETE from TSPL_SILAGE_CRITERIA_FARMER_DETAIL WHERE Entr_Code ='" & farmerCode & "'"
                '========================================================================================================
                If (clsDBFuncationality.ExecuteNonQuery(deleteQuery, transaction)) Then
                    Return True
                Else
                    Return False
                End If

            End If


        Catch Err As Exception
            Throw New Exception(Err.Message)
            Return False
        End Try
        Return True
    End Function

End Class

Public Class clsSilageFarmerChildVendor

#Region "Variables of Table :- TSPL_VENDOR_MASTER "

    Public Vendor_Code As String = Nothing
    Public Farmer_Code As String = Nothing
    Public Transporter As String = Nothing
    Public Tehsil As String = Nothing
    Public Branch_Name As String = Nothing
    Public Account_No As String = Nothing
    Public Is_Parent_Vendor As String = Nothing
    Public Parent_Vendor_Code As String = Nothing
    Public Bank_Name As String = Nothing
    Public IFSC_Code As String = Nothing
    Public Vendor_Type As String = Nothing
    Public Pin_Code As String = Nothing
    Public PAN As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Add1 As String = Nothing
    Public Add2 As String = Nothing
    Public Add3 As String = Nothing
    Public Phone1 As String = Nothing
    Public Phone2 As String = Nothing
    Public Tin_No As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing


#End Region

    Public Shared Function GetData(ByVal objM As clsSilageFarmerMaster, trans As SqlClient.SqlTransaction) As clsSilageFarmerMaster
        Dim dt As DataTable = Nothing
        Dim obj As clsSilageFarmerChildVendor = New clsSilageFarmerChildVendor()
        Dim qry As String = "SELECT  *  FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" & objM.Entr_Code & "'"

        Try
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objM.Arr_clsSilageFarmerChildVendor = New List(Of clsSilageFarmerChildVendor)
                    obj.Transporter = clsCommon.myCstr(dt.Rows(0)("Transporter"))
                    'obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
                    'obj.Country_Code = clsCommon.myCstr(dt.Rows(0)("Country_Code"))
                    obj.Tehsil = clsCommon.myCstr(dt.Rows(0)("Tehsil"))
                    obj.Branch_Name = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
                    '  obj.IFCI_Code = clsCommon.myCstr(dt.Rows(0)("IFCI_Code"))
                    obj.Account_No = clsCommon.myCstr(dt.Rows(0)("Account_No"))
                    obj.Is_Parent_Vendor = clsCommon.myCstr(dt.Rows(0)("Is_Parent_Vendor"))
                    obj.Parent_Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Parent_Vendor_Code"))
                    obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
                    obj.IFSC_Code = clsCommon.myCstr(dt.Rows(0)("IFSC_Code"))
                    obj.Vendor_Type = clsCommon.myCstr(dt.Rows(0)("Vendor_Type"))
                    obj.Pin_Code = clsCommon.myCstr(dt.Rows(0)("Pin_Code"))
                    obj.PAN = clsCommon.myCstr(dt.Rows(0)("PAN"))
                    ' obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                    obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                    obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                    obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                    obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                    ' obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                    ' obj.State = clsCommon.myCstr(dt.Rows(0)("State"))
                    ' obj.Country = clsCommon.myCstr(dt.Rows(0)("Country"))
                    obj.Phone1 = clsCommon.myCstr(dt.Rows(0)("Phone1"))
                    obj.Phone2 = clsCommon.myCstr(dt.Rows(0)("Phone2"))
                    '  obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
                    '  obj.WebSite = clsCommon.myCstr(dt.Rows(0)("WebSite"))
                    obj.Tin_No = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
                    objM.Arr_clsSilageFarmerChildVendor.Add(obj)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return objM
    End Function

    Public Shared Function SaveData(ByVal obj As clsSilageFarmerMaster, isNewEntry As Boolean, trans As SqlTransaction, ByVal isImport As Boolean) As Boolean

        Dim IsRecExist As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER where Vendor_Code = '" & clsCommon.myCstr(obj.Farmer_Code) & "'", trans)
        If IsRecExist > 0 Then
            isNewEntry = False
        Else
            isNewEntry = True
        End If
        Try
            If obj.Arr_clsSilageFarmerChildVendor IsNot Nothing Then
                For Each oVendor As clsSilageFarmerChildVendor In obj.Arr_clsSilageFarmerChildVendor
                    If obj.Arr_clsSilageFarmerChildVendor IsNot Nothing AndAlso obj.Arr_clsSilageFarmerChildVendor.Count > 0 Then
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Vendor_Code", clsCommon.myCstr(obj.Farmer_Code))
                        clsCommon.AddColumnsForChange(coll, "Transporter", clsCommon.myCstr(oVendor.Transporter))
                        clsCommon.AddColumnsForChange(coll, "Tehsil", clsCommon.myCstr(oVendor.Tehsil))
                        clsCommon.AddColumnsForChange(coll, "Branch_Name", clsCommon.myCstr(oVendor.Branch_Name))
                        clsCommon.AddColumnsForChange(coll, "Account_No", clsCommon.myCstr(oVendor.Account_No))
                        clsCommon.AddColumnsForChange(coll, "Is_Parent_Vendor", clsCommon.myCstr(oVendor.Is_Parent_Vendor))
                        clsCommon.AddColumnsForChange(coll, "Parent_Vendor_Code", clsCommon.myCstr(oVendor.Parent_Vendor_Code))
                        clsCommon.AddColumnsForChange(coll, "Bank_Name", clsCommon.myCstr(oVendor.Bank_Name))
                        clsCommon.AddColumnsForChange(coll, "IFSC_Code", clsCommon.myCstr(oVendor.IFSC_Code))
                        clsCommon.AddColumnsForChange(coll, "Vendor_Type", clsCommon.myCstr(oVendor.Vendor_Type))
                        clsCommon.AddColumnsForChange(coll, "Pin_Code", clsCommon.myCstr(oVendor.Pin_Code))
                        clsCommon.AddColumnsForChange(coll, "PAN", clsCommon.myCstr(oVendor.PAN))
                        clsCommon.AddColumnsForChange(coll, "Vendor_Name", clsCommon.myCstr(oVendor.Vendor_Name))
                        clsCommon.AddColumnsForChange(coll, "Add1", clsCommon.myCstr(oVendor.Add1))
                        clsCommon.AddColumnsForChange(coll, "Add2", clsCommon.myCstr(oVendor.Add2))
                        clsCommon.AddColumnsForChange(coll, "Add3", clsCommon.myCstr(oVendor.Add3))
                        clsCommon.AddColumnsForChange(coll, "Phone1", clsCommon.myCstr(oVendor.Phone1))
                        clsCommon.AddColumnsForChange(coll, "Phone2", clsCommon.myCstr(oVendor.Phone2))
                        clsCommon.AddColumnsForChange(coll, "Tin_No", clsCommon.myCstr(oVendor.Tin_No))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        If isNewEntry = True Then
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "TSPL_VENDOR_MASTER.Vendor_Code = '" & oVendor.Vendor_Code & "'", trans)
                        End If
                    End If
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Shared Function GetData(ByVal entrpreneurCode As String, navigatorType As NavigatorType, ByVal transaction As SqlTransaction) As DataTable
        Dim obj As clsGenerateVendor = Nothing
        Dim selectQry As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim columnName As String = "Vendor_Code"
        Dim table As String = "TSPL_VENDOR_MASTER"
        '===================================================================
        Try
            selectQry = "SELECT * FROM " & table & " WHERE 1=1"

            Select Case navigatorType
                Case navigatorType.First
                    selectQry += " and " & table & "." & columnName & "  = (select MIN(" & columnName & ") from " & table & " where 1=1 )"
                Case navigatorType.Last
                    selectQry += " and " & table & "." & columnName & "  = (select Max(" & columnName & ") from " & table & " where 1=1 )"
                Case navigatorType.Next
                    selectQry += " and " & table & "." & columnName & "  = (select Min(" & columnName & ") from " & table & " where " & columnName & ">'" & entrpreneurCode & "' )"
                Case navigatorType.Previous
                    selectQry += " and " & table & "." & columnName & "  = (select Max(" & columnName & ") from " & table & " where " & columnName & "<'" & entrpreneurCode & "' )"
                Case navigatorType.Current
                    selectQry += " and " & table & "." & columnName & "  = '" + entrpreneurCode + "'"
            End Select
            dt = clsDBFuncationality.GetDataTable(selectQry, transaction)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return dt
    End Function

    Public Shared Function DeleteData(ByVal vendorCode As String, ByVal transaction As SqlClient.SqlTransaction) As Boolean
        Dim deleteQuery As String = String.Empty
        Try

            deleteQuery = "DELETE FROM TSPL_VENDOR_MASTER WHERE Vendor_Code = '" & vendorCode & "'"
            '=============================================================================================================================
            If (clsDBFuncationality.ExecuteNonQuery(deleteQuery, transaction)) Then
                Return True
            Else
                Return False
            End If

        Catch Err As Exception
            Throw New Exception(Err.Message)
            Return False
        End Try
        Return True
    End Function

End Class
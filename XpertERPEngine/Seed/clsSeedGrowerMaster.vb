Imports System.Data.SqlClient
Public Class clsSeedGrowerMaster
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public Father_Name As String = String.Empty
    Public Registration_No As String = Nothing
    Public Add1 As String = Nothing
    Public Add2 As String = Nothing
    Public Add3 As String = Nothing
    Public Village_Code As String = String.Empty
    Public Tehsil As String = Nothing
    Public District_Code As String = String.Empty
    Public Mobile_No As String = Nothing
    Public Own_Land As Decimal = 0
    Public Family_Land As Decimal = 0
    Public Lease_Land As Decimal = 0
    Public Total_Land As Decimal = 0
    Public Khasra_No As String = Nothing
    Public Aadhar_No As String = Nothing
    Public PAN As String = Nothing
    Public Bank_Name As String = Nothing
    Public IFSC_Code As String = Nothing
    Public Branch_Name As String = Nothing
    Public Account_No As String = Nothing


    Public Function SaveData(ByVal obj As clsSeedGrowerMaster, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsSeedGrowerMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try

            IsSaved = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Father_Name", obj.Father_Name)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.Add3)
            clsCommon.AddColumnsForChange(coll, "Village_Code", obj.Village_Code)
            clsCommon.AddColumnsForChange(coll, "Tehsil", obj.Tehsil)
            clsCommon.AddColumnsForChange(coll, "DISTRICT_Code", obj.District_Code, True)
            clsCommon.AddColumnsForChange(coll, "Mobile_No", obj.Mobile_No)
            clsCommon.AddColumnsForChange(coll, "Registration_No", obj.Registration_No)
            clsCommon.AddColumnsForChange(coll, "Khasra_No", obj.Khasra_No)
            clsCommon.AddColumnsForChange(coll, "PAN", obj.PAN)
            clsCommon.AddColumnsForChange(coll, "Aadhar_No", obj.Aadhar_No)
            clsCommon.AddColumnsForChange(coll, "Own_Land", obj.Own_Land)
            clsCommon.AddColumnsForChange(coll, "Family_Land", obj.Family_Land)
            clsCommon.AddColumnsForChange(coll, "Lease_Land", obj.Lease_Land)
            clsCommon.AddColumnsForChange(coll, "Total_Land", obj.Total_Land)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)
            clsCommon.AddColumnsForChange(coll, "IFSC_Code", obj.IFSC_Code)
            clsCommon.AddColumnsForChange(coll, "Branch_Name", obj.Branch_Name)
            clsCommon.AddColumnsForChange(coll, "Account_No", obj.Account_No)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                CreateCustomer(obj, trans)
                CreateVendor(obj, trans)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHEED_GROWER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHEED_GROWER_MASTER", OMInsertOrUpdate.Update, "TSPL_SHEED_GROWER_MASTER.Code='" + obj.Code + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSeedGrowerMaster
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsSeedGrowerMaster = GetData(strCode, NavType, trans)
            trans.Commit()

            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSeedGrowerMaster
        Dim obj As clsSeedGrowerMaster = Nothing

        Try
            Dim strQry As String = "select * from TSPL_SHEED_GROWER_MASTER where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Code = (select MIN(Code) from TSPL_SHEED_GROWER_MASTER where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Code = (Select Max(Code) from TSPL_SHEED_GROWER_MASTER where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Code = (Select Min(Code) from TSPL_SHEED_GROWER_MASTER where Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Code = (select Max(Code) from TSPL_SHEED_GROWER_MASTER where Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsSeedGrowerMaster()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.Father_Name = clsCommon.myCstr(dt.Rows(0)("Father_Name"))
                obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                obj.Village_Code = clsCommon.myCstr(dt.Rows(0)("Village_Code"))
                obj.Tehsil = clsCommon.myCstr(dt.Rows(0)("Tehsil"))
                obj.District_Code = clsCommon.myCstr(dt.Rows(0)("DISTRICT_Code"))
                obj.Mobile_No = clsCommon.myCstr(dt.Rows(0)("Mobile_No"))
                obj.Registration_No = clsCommon.myCstr(dt.Rows(0)("Registration_No"))
                obj.Khasra_No = clsCommon.myCstr(dt.Rows(0)("Khasra_No"))
                obj.Aadhar_No = clsCommon.myCstr(dt.Rows(0)("Aadhar_No"))
                obj.PAN = clsCommon.myCstr(dt.Rows(0)("PAN"))
                obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
                obj.Branch_Name = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
                obj.IFSC_Code = clsCommon.myCstr(dt.Rows(0)("IFSC_Code"))
                obj.Account_No = clsCommon.myCstr(dt.Rows(0)("Account_No"))
                obj.Own_Land = clsCommon.myCstr(dt.Rows(0)("Own_Land"))
                obj.Family_Land = clsCommon.myCstr(dt.Rows(0)("Family_Land"))
                obj.Lease_Land = clsCommon.myCstr(dt.Rows(0)("Lease_Land"))
                obj.Total_Land = clsCommon.myCstr(dt.Rows(0)("Total_Land"))

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
            Dim qry As String = "delete from TSPL_SHEED_GROWER_MASTER where code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function CreateCustomer(ByVal obj As clsSeedGrowerMaster, ByVal trans As SqlTransaction)
        Try
            Dim strCmd As String = "select Cust_Code from tspl_customer_master where CUSTOMER_FORM_TYPE='ALL' and Is_Default_Grower=1"
            Dim strcode As String = clsDBFuncationality.getSingleValue(strCmd, trans)
            If clsCommon.myLen(strcode) < 0 Then
                Throw New Exception("To make first customer defautl grower")
            End If

            Dim objCustomer As clsCustomerMaster = clsCustomerMaster.GetData(strcode, trans)
            objCustomer.Cust_Code = obj.Code
            objCustomer.Customer_Name = obj.Name
            objCustomer.CUSTOMER_FORM_TYPE = "GRO"
            objCustomer.SaveData(objCustomer, Nothing, True, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateVendor(ByVal obj As clsSeedGrowerMaster, ByVal trans As SqlTransaction)
        Try

            Dim strCmd As String = "select Vendor_Code from TSPL_VENDOR_MASTER where Is_Default_Grower=1 "
            Dim strcode As String = clsDBFuncationality.getSingleValue(strCmd, trans)
            If clsCommon.myLen(strcode) < 0 Then
                Throw New Exception("To make first vendor default grower")
            End If
            Dim objVendor As clsVendorMaster = clsVendorMaster.GetVendorData(strcode, trans)
            objVendor.Vendor_Code = obj.Code
            objVendor.Vendor_Name = obj.Name
            objVendor.Form_Type = "GRO"
            objVendor.SaveData(objVendor, True, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

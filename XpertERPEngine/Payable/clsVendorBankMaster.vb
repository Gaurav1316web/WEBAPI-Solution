Imports common
Imports System.Data.SqlClient

Public Class clsVendorBankMaster
#Region "Variables"
    Public Bank_code As String = Nothing
    Public Bank_Name As String = Nothing
    Public Branch_Code As String = Nothing
    Public Branch_Name As String = Nothing
    Public IFSC_Code As String = Nothing
    Public add1 As String = Nothing
    Public add2 As String = Nothing
    Public add3 As String = Nothing
    Public country_code As String = Nothing
    Public country_name As String = Nothing
    Public state_code As String = Nothing
    Public state_name As String = Nothing
    Public city_code As String = Nothing
    Public city_name As String = Nothing
    Public arrVendorBranchDetail As List(Of clsVendorBankBranchDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        ' Dim qry As String = " select  tspl_vendor_bank_master.Bank_Code as [BankCode] ,tspl_vendor_bank_master.Bank_Name as [Bank Name] ,tspl_vendor_bank_master.Branch_Code as [Branch Code] ,tspl_vendor_bank_master.Branch_Name as [Branch Name] ,tspl_vendor_bank_master.IFSC_Code as [IFSC Code] ,tspl_vendor_bank_master.Add1 as [Address1] ,tspl_vendor_bank_master.Add2 as [Address2] ,tspl_vendor_bank_master.Add3 as [Address3] ,tspl_vendor_bank_master.Country_Code as [Country Code] ,tspl_vendor_bank_master.State_Code as [State Code] ,tspl_vendor_bank_master.City_Code as [City Code] ,tspl_vendor_bank_master.Created_By as [Created By] ,tspl_vendor_bank_master.Created_Date as [Created Date] ,tspl_vendor_bank_master.Modified_By as [Modified By] ,tspl_vendor_bank_master.Modified_Date as [Modified Date],tspl_vendor_bank_master.Comp_Code as [Company Code]  From tspl_vendor_bank_master "
        Dim qry As String = " select  tspl_vendor_bank_master.Bank_Code as [BankCode] ,tspl_vendor_bank_master.Bank_Name as [Bank Name] ,tspl_vendor_bank_master.Add1 as [Address1] ,tspl_vendor_bank_master.Add2 as [Address2] ,tspl_vendor_bank_master.Add3 as [Address3] ,tspl_vendor_bank_master.Country_Code as [Country Code] ,tspl_vendor_bank_master.State_Code as [State Code] ,tspl_vendor_bank_master.City_Code as [City Code] ,tspl_vendor_bank_master.Created_By as [Created By] ,tspl_vendor_bank_master.Created_Date as [Created Date] ,tspl_vendor_bank_master.Modified_By as [Modified By] ,tspl_vendor_bank_master.Modified_Date as [Modified Date],tspl_vendor_bank_master.Comp_Code as [Company Code]  From tspl_vendor_bank_master "
        str = clsCommon.ShowSelectForm("VENBNKMFND", qry, "BankCode", whrCls, CurrCode, "BankCode", isButtonClicked)

        Return str
    End Function

    Public Shared Function getFinderBranch(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Branch_Name , Bank_IFSC_Code ,Bank_Code  from TSPL_Vendor_Bank_Branch_Details   "
        str = clsCommon.ShowSelectForm("BNKBRNCHMST", qry, "Branch_Name", whrcls, curcode, "Branch_Name", isButtonClicked)
        Return str
    End Function

    '===changes by shivani against ticket[BM00000008650] 
    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsVendorBankMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_Vendor_Bank_Branch_Details where Bank_Code='" & obj.Bank_code & "'", trans)

            If isNewEntry AndAlso clsCommon.myLen(obj.Bank_code) <= 0 Then
                obj.Bank_code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.VendorBankMaster, "", ""))
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_code)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)

            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code, False)
            clsCommon.AddColumnsForChange(coll, "Branch_Name", obj.Branch_Name, False)
            clsCommon.AddColumnsForChange(coll, "IFSC_Code", obj.IFSC_Code, False)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.add2)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.add3)
            clsCommon.AddColumnsForChange(coll, "Country_Code", obj.country_code)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.state_code)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.city_code)
            
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Master", OMInsertOrUpdate.Update, " Bank_Code='" + obj.Bank_code + "'", trans)
            End If
            clsVendorBankBranchDetail.saveData(obj.arrVendorBranchDetail, obj.Bank_code, trans)

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveDataImport(ByVal isNewEntry As Boolean, ByVal obj As clsVendorBankMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If isNewEntry AndAlso clsCommon.myLen(obj.Bank_code) <= 0 Then
                obj.Bank_code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.VendorBankMaster, "", ""))
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_code)
            clsCommon.AddColumnsForChange(coll, "Bank_Name", obj.Bank_Name)

            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code, True)
            clsCommon.AddColumnsForChange(coll, "Branch_Name", obj.Branch_Name, True)
            clsCommon.AddColumnsForChange(coll, "IFSC_Code", obj.IFSC_Code, True)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.add2)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.add3)
            clsCommon.AddColumnsForChange(coll, "Country_Code", obj.country_code)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.state_code)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.city_code)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Master", OMInsertOrUpdate.Update, " Bank_Code='" + obj.Bank_code + "'", trans)
            End If
            clsVendorBankBranchDetail.saveData(obj.arrVendorBranchDetail, obj.Bank_code, trans)

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsVendorBankMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsVendorBankMaster
        Try
            Dim obj As New clsVendorBankMaster
            Dim qry As String = "select * from TSPL_Vendor_Bank_Master where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and Bank_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and Bank_code in (select min(Bank_code) from tspl_Vendor_Bank_Master)"
                Case NavigatorType.Last
                    qry += " and Bank_code in (select max(Bank_code) from tspl_Vendor_Bank_Master)"
                Case NavigatorType.Next
                    qry += " and Bank_code in (select min(Bank_code) from tspl_Vendor_Bank_Master where Bank_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and Bank_code in (select max(Bank_code) from tspl_Vendor_Bank_Master where Bank_code<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Bank_code = clsCommon.myCstr(dt.Rows(0)("Bank_code"))
                obj.Bank_Name = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
                obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
                obj.Branch_Name = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
                obj.IFSC_Code = clsCommon.myCstr(dt.Rows(0)("IFSC_Code"))
                obj.add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                obj.add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                obj.add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                obj.country_code = clsCommon.myCstr(dt.Rows(0)("Country_Code"))
                obj.country_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + obj.country_code + "'", trans))
                obj.state_code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
                obj.state_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + obj.state_code + "' and country_code='" + obj.country_code + "'", trans))
                obj.city_code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                obj.city_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where state_code='" + obj.state_code + "' and city_code='" + obj.city_code + "'", trans))
            End If
            obj.arrVendorBranchDetail = clsVendorBankBranchDetail.getData(obj.Bank_code, trans)
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Document Not Found.")
            End If
            Dim qry As String = String.Empty
            qry = "delete from TSPL_Vendor_Bank_Branch_Details where Bank_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Vendor_Bank_Master where Bank_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsVendorBankBranchDetail
    Public Bank_Code As String = Nothing
    Public Branch_Name As String = Nothing
    Public Bank_IFSC_Code As String = Nothing
    Public Bank_Swift_Code As String = Nothing
  
    Public Shared Function saveData(ByVal arrObj As List(Of clsVendorBankBranchDetail), ByVal strBankNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsVendorBankBranchDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Bank_Code", strBankNo)
                    clsCommon.AddColumnsForChange(coll, "Branch_Name", obj.Branch_Name)
                    clsCommon.AddColumnsForChange(coll, "Bank_IFSC_Code", obj.Bank_IFSC_Code)
                    clsCommon.AddColumnsForChange(coll, "Bank_Swift_Code", obj.Bank_Swift_Code)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Branch_Details", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
   
    Public Shared Function getData(ByVal strBankNo As String, ByVal trans As SqlTransaction) As List(Of clsVendorBankBranchDetail)
        Try
            Dim arrObj As List(Of clsVendorBankBranchDetail) = Nothing
            Dim obj As clsVendorBankBranchDetail = Nothing
            Dim qry As String = "Select * from  TSPL_Vendor_Bank_Branch_Details where Bank_Code='" & strBankNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsVendorBankBranchDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsVendorBankBranchDetail()
                    obj.Bank_Code = clsCommon.myCstr(dt.Rows(i)("Bank_Code"))
                    obj.Branch_Name = clsCommon.myCstr(dt.Rows(i)("Branch_Name"))
                    obj.Bank_IFSC_Code = clsCommon.myCstr(dt.Rows(i)("Bank_IFSC_Code"))
                    obj.Bank_Swift_Code = clsCommon.myCstr(dt.Rows(i)("Bank_Swift_Code"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
  

End Class
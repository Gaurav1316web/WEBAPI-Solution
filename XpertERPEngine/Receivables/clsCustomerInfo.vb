'-23/10/2012-Updation By--Pankaj Kumar--Update Customer Name In Customer MAster if Exist WHile namae is Updated from Customer Information---_Fwd By--Ranjana Mam
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsCustomerInfo
#Region "Variables"
    Public Shared arrDBName As New List(Of String)
    Public ArrVisi As New List(Of String)
    Public Cust_Code As String
    Public Customer_Name As String
    Public Add1 As String
    Public Add2 As String
    Public Add3 As String
    Public Cust_Group_Code As String
    Public City_Code As String
    Public State As String
    Public Country As String
    Public Phone1 As String
    Public Phone2 As String
    Public Fax As String
    Public PIN_NO As String
    Public Email As String
    Public WebSite As String
    Public Contact_Person_Name As String
    Public Contact_Person_Phone As String
    Public Contact_Person_Fax As String
    Public Contact_Person_Website As String
    Public Contact_Person_Email As String
    Public Terms_Code As String
    Public Cust_Account As String
    Public Tax_Group As String
    Public TAX1 As String
    Public TAX1_Rate As Double
    Public TAX2 As String
    Public TAX2_Rate As Double
    Public TAX3 As String
    Public TAX3_Rate As Double
    Public TAX4 As String
    Public TAX4_Rate As Double
    Public TAX5 As String
    Public TAX5_Rate As Double
    Public TAX6 As String
    Public TAX6_Rate As Double
    Public TAX7 As String
    Public TAX7_Rate As Double
    Public TAX8 As String
    Public TAX8_Rate As Double
    Public TAX9 As String
    Public TAX9_Rate As Double
    Public TAX10 As String
    Public TAX10_Rate As Double
    Public Payment_Code As String
    Public Service_Tax_No As String
    Public Tin_No As String
    Public Lst_No As String
    Public Form_Type As String
    Public Status As Char
    Public OnHold As Char
    Public Cust_Type As String
    Public Remarks1 As String
    Public Remarks2 As String
    Public Additional1 As String
    Public Additional2 As String
    Public Additional3 As String
    Public Credit_Limit As Double
    Public Created_By As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
    Public Created_Date As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
    Public Modify_By As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
    Public Modify_Date As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
    Public Comp_Code As String = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
    Public CST As String
    Public ECC As String
    Public Range As String
    Public Collectorate As String
    Public PAN As String
    Public Division As String
    Public Parent_Customer_No As String
    Public Credit_Customer As String
    Public Inter_Branch As Char
    Public chkdis As Char
    Public custdis As String
    Public prntcustyn As String
#End Region
    Public Shared Function DeleteData(ByVal strCustCode As String) As Boolean
        Try
            If (clsCommon.myLen(strCustCode) <= 0) Then
                Throw New Exception("Customer Code No not found to Delete")
            End If

            Dim Check As String = "Select " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER Where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code='" + strCustCode + "'"
            Check = clsCommon.GetQueryWithAllSelectedDataBase(Check, GetSelectedDatabase(), False)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Check)
            If dt.Rows.Count > 0 Then
                Throw New Exception("Sorry! This Customer is Already Used")
            Else
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOMER_INFO Where Cust_Code='" + strCustCode + "'")
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function GetSelectedDatabase() As List(Of String)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select DataBase_Name  from TSPL_COMPANY_MASTER ")
        For Each dr As DataRow In dt.Rows
            arrDBName.Add(clsCommon.myCstr(dr("DataBase_Name")))
        Next
        Return arrDBName
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCustomerInfo
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerInfo
        Dim obj As clsCustomerInfo = Nothing
        Dim qry As String = "SELECT [Cust_Code], [Customer_Name],[Add1],[Add2],[Add3], [Cust_Group_Code] , [City_Code],[State],[Country],[Phone1],[Phone2],[Fax],[Email],[WebSite],[Contact_Person_Name],[Contact_Person_Phone],[Contact_Person_Fax] ,[Contact_Person_Website],[Contact_Person_Email],[Terms_Code],[Cust_Account],[Tax_Group],[TAX1],[TAX1_Rate],[TAX2],[TAX2_Rate],[TAX3],[TAX3_Rate],[TAX4],[TAX4_Rate],[TAX5],[TAX5_Rate],[TAX6],[TAX6_Rate] ,[TAX7],[TAX7_Rate],[TAX8],[TAX8_Rate],[TAX9],[TAX9_Rate],[TAX10],[TAX10_Rate],[Payment_Code],[Service_Tax_No] , [Tin_No],[Lst_No],[Form_Type], [Status],[OnHold], [Cust_Type], [Remarks1], [Remarks2],[Additional1],[Additional2],[Additional3], [Credit_Limit],  [CST], [ECC], [Range], [Collectorate], [PAN],[Division], [Parent_Customer_No], [credit_customer], [Inter_branch],[Distributor],[CustDist] FROM TSPL_CUSTOMER_INFO Where 1=1"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Cust_Code = (select MIN(Cust_Code) from TSPL_CUSTOMER_INFO)"
            Case NavigatorType.Last
                qry += " and Cust_Code = (select Max(Cust_Code) from TSPL_CUSTOMER_INFO)"
            Case NavigatorType.Next
                qry += " and Cust_Code = (select Min(Cust_Code) from TSPL_CUSTOMER_INFO where  Cust_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Cust_Code = (select Max(Cust_Code) from TSPL_CUSTOMER_INFO where Cust_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Cust_Code = '" + strCode + "'"
        End Select
        ',[Agg_Made_Date],[Agg_Close_Date]
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Try

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsCustomerInfo()
                obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                obj.Cust_Group_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Group_Code"))
                obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                obj.State = clsCommon.myCstr(dt.Rows(0)("State"))
                obj.Country = clsCommon.myCstr(dt.Rows(0)("Country"))
                obj.Phone1 = clsCommon.myCstr(dt.Rows(0)("Phone1"))
                obj.Phone2 = clsCommon.myCstr(dt.Rows(0)("Phone2"))
                obj.Fax = clsCommon.myCstr(dt.Rows(0)("Fax"))
                obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
                obj.WebSite = clsCommon.myCstr(dt.Rows(0)("Website"))
                obj.Contact_Person_Name = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Name"))
                obj.Contact_Person_Phone = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Phone"))
                obj.Contact_Person_Fax = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Fax"))
                obj.Contact_Person_Website = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Website"))
                obj.Contact_Person_Email = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Email"))
                obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                obj.Cust_Account = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
                obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
                obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
                obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
                obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
                obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
                obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
                obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
                obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
                obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
                obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
                obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
                obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
                obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
                obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
                obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
                obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
                obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
                obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
                obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
                obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
                obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
                obj.Service_Tax_No = clsCommon.myCstr(dt.Rows(0)("Service_Tax_No"))
                obj.Tin_No = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
                obj.Lst_No = clsCommon.myCstr(dt.Rows(0)("Lst_No"))
                obj.Form_Type = clsCommon.myCstr(dt.Rows(0)("Form_Type"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.OnHold = clsCommon.myCstr(dt.Rows(0)("OnHold"))
                obj.Cust_Type = clsCommon.myCstr(dt.Rows(0)("Cust_Type"))
                obj.Remarks1 = clsCommon.myCstr(dt.Rows(0)("Remarks1"))
                obj.Remarks2 = clsCommon.myCstr(dt.Rows(0)("Remarks2"))
                obj.Additional1 = clsCommon.myCstr(dt.Rows(0)("Additional1"))
                obj.Additional2 = clsCommon.myCstr(dt.Rows(0)("Additional2"))
                obj.Additional3 = clsCommon.myCstr(dt.Rows(0)("Additional3"))
                obj.Credit_Limit = clsCommon.myCdbl(dt.Rows(0)("Credit_Limit"))
                obj.CST = clsCommon.myCstr(dt.Rows(0)("CST"))
                obj.ECC = clsCommon.myCstr(dt.Rows(0)("ECC"))
                obj.Range = clsCommon.myCstr(dt.Rows(0)("Range"))
                obj.Collectorate = clsCommon.myCstr(dt.Rows(0)("Collectorate"))
                obj.PAN = clsCommon.myCstr(dt.Rows(0)("PAN"))
                obj.Division = clsCommon.myCstr(dt.Rows(0)("Division"))
                obj.Parent_Customer_No = clsCommon.myCstr(dt.Rows(0)("Parent_Customer_No"))
                obj.Credit_Customer = clsCommon.myCstr(dt.Rows(0)("Credit_Customer"))
                obj.Inter_Branch = clsCommon.myCstr(dt.Rows(0)("Inter_Branch"))
                obj.chkdis = clsCommon.myCstr(dt.Rows(0)("Distributor"))
                obj.custdis = clsCommon.myCstr(dt.Rows(0)("CustDist"))
                'If clsCommon.myLen(dt.Rows(0)("Agg_Made_Date")) > 0 Then
                '    obj.Agg_Made_Date = clsCommon.myCDate(dt.Rows(0)("Agg_Made_Date"))
                'End If
                'If clsCommon.myLen(dt.Rows(0)("Agg_Close_Date")) > 0 Then
                '    obj.Agg_Close_Date = clsCommon.myCDate(dt.Rows(0)("Agg_Close_Date"))
                'End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsCustomerInfo, ByVal arrVisi As List(Of String), ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.Add3)
            clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
            clsCommon.AddColumnsForChange(coll, "State", obj.State)
            clsCommon.AddColumnsForChange(coll, "Country", obj.Country)
            clsCommon.AddColumnsForChange(coll, "Phone1", obj.Phone1)
            clsCommon.AddColumnsForChange(coll, "Phone2", obj.Phone2)
            clsCommon.AddColumnsForChange(coll, "Fax", obj.Fax)
            clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
            clsCommon.AddColumnsForChange(coll, "WebSite", obj.WebSite)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Name", obj.Contact_Person_Name)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Phone", obj.Contact_Person_Phone)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Fax", obj.Contact_Person_Fax)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Website", obj.Contact_Person_Website)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Email", obj.Contact_Person_Email)
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            clsCommon.AddColumnsForChange(coll, "Service_Tax_No", obj.Service_Tax_No)
            clsCommon.AddColumnsForChange(coll, "Tin_No", obj.Tin_No)
            clsCommon.AddColumnsForChange(coll, "Lst_No", obj.Lst_No)
            clsCommon.AddColumnsForChange(coll, "Form_Type", obj.Form_Type)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "OnHold", obj.OnHold)
            clsCommon.AddColumnsForChange(coll, "Cust_Type", obj.Cust_Type)
            clsCommon.AddColumnsForChange(coll, "Remarks1", obj.Remarks1)
            clsCommon.AddColumnsForChange(coll, "Remarks2", obj.Remarks2)
            clsCommon.AddColumnsForChange(coll, "Additional1", obj.Additional1)
            clsCommon.AddColumnsForChange(coll, "Additional2", obj.Additional2)
            clsCommon.AddColumnsForChange(coll, "Additional3", obj.Additional3)
            clsCommon.AddColumnsForChange(coll, "Credit_Limit", obj.Credit_Limit)
            clsCommon.AddColumnsForChange(coll, "Modify_By", obj.Modify_By)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", obj.Modify_Date)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
            clsCommon.AddColumnsForChange(coll, "CST", obj.CST)
            clsCommon.AddColumnsForChange(coll, "ECC", obj.ECC)
            clsCommon.AddColumnsForChange(coll, "Range", obj.Range)
            clsCommon.AddColumnsForChange(coll, "PAN", obj.PAN)
            clsCommon.AddColumnsForChange(coll, "Collectorate", obj.Collectorate)
            clsCommon.AddColumnsForChange(coll, "Division", obj.Division)
            clsCommon.AddColumnsForChange(coll, "Parent_Customer_No", obj.Parent_Customer_No)
            clsCommon.AddColumnsForChange(coll, "Credit_Customer", obj.Credit_Customer)
            clsCommon.AddColumnsForChange(coll, "Inter_Branch", obj.Inter_Branch)
            clsCommon.AddColumnsForChange(coll, "parent_customer_yn", obj.prntcustyn)
            clsCommon.AddColumnsForChange(coll, "Distributor", obj.chkdis)
            clsCommon.AddColumnsForChange(coll, "CustDist", obj.custdis)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INFO", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INFO", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_INFO.Cust_Code='" + obj.Cust_Code + "'", trans)
            End If
            '---------------------Visi-Detail--------------------------Update Visi Master----
            Dim collVisi As New Hashtable()
            clsCommon.AddColumnsForChange(collVisi, "Customer_name", obj.Customer_Name)
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_VISI_MASTER set Customer_Id='' , Customer_name='' Where Customer_Id='" + obj.Cust_Code + "'", trans)
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(collVisi, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_Id IN (" + clsCommon.GetMulcallString(arrVisi) + ")", trans)
            '----------------------------------------------------------------------------------
            '23/12/2012-------Customer-------------Update Customer Name in Customer Master While It is Changed From Customer Information----
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select DataBase_Name  from TSPL_COMPANY_MASTER", trans)
            arrDBName.Clear()
            For Each dr As DataRow In dt.Rows
                arrDBName.Add(clsCommon.myCstr(dr("DataBase_Name")))
            Next
            If dt.Rows.Count > 0 Then
                Dim collCustomerMaster As New Hashtable()
                clsCommon.AddColumnsForChange(collCustomerMaster, "Customer_name", obj.Customer_Name)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTableInSelectedDatabase(collCustomerMaster, arrDBName, "TSPL_CUSTOMER_MASTER", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_MASTER.Cust_Code ='" + obj.Cust_Code + "'", trans)
            End If
            arrDBName.Clear()
            '----------------------------------------------------------------------------------
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            trans.Rollback()
        End Try
        Return isSaved
    End Function

End Class

Public Class clsSecondaryCustomer
#Region "Variables"
    Public Cust_Code As String
    Public Customer_Name As String
    Public Distributor As String
    Public Add1 As String
    Public Add2 As String
    Public Add3 As String
    Public City_Code As String
    Public State As String
    Public Country As String
    Public Phone1 As String
    Public Phone2 As String
    Public Fax As String
    Public Email As String
    Public WebSite As String
    Public Credit_Limit As Double = 0
    Public CURRENCY_CODE As String
    Public Status As String
    Public Created_By As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
    Public Created_Date As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
    Public Modify_By As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
    Public Modify_Date As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
    Public Comp_Code As String = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code as [Code] ,TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name as [Customer Name] ,TSPL_SECONDARY_CUSTOMER_MASTER.Add1 as [Address1] ,TSPL_SECONDARY_CUSTOMER_MASTER.Add2 as [Address2] ,TSPL_SECONDARY_CUSTOMER_MASTER.Add3 as [Address3] ,TSPL_SECONDARY_CUSTOMER_MASTER.Distributor as [Distributor] ,TSPL_SECONDARY_CUSTOMER_MASTER.City_Code as [City Code] ,TSPL_SECONDARY_CUSTOMER_MASTER.State as [State] ,TSPL_SECONDARY_CUSTOMER_MASTER.Country as [Country] ,TSPL_SECONDARY_CUSTOMER_MASTER.Phone1 as [Phone1] ,TSPL_SECONDARY_CUSTOMER_MASTER.Phone2 as [Phone2] ,TSPL_SECONDARY_CUSTOMER_MASTER.Fax as [Fax] ,TSPL_SECONDARY_CUSTOMER_MASTER.Email as [Email] ,TSPL_SECONDARY_CUSTOMER_MASTER.WebSite as [Website] ,TSPL_SECONDARY_CUSTOMER_MASTER.Credit_Limit as [Credit Limit] ,TSPL_SECONDARY_CUSTOMER_MASTER.CURRENCY_CODE as [Currency Code] ,TSPL_SECONDARY_CUSTOMER_MASTER.Status as [Status] ,TSPL_SECONDARY_CUSTOMER_MASTER.Created_By as [Created By] ,TSPL_SECONDARY_CUSTOMER_MASTER.Created_Date as [Created Date] ,TSPL_SECONDARY_CUSTOMER_MASTER.Modified_By as [Modified By] ,TSPL_SECONDARY_CUSTOMER_MASTER.Modified_Date as [Modified Date] ,TSPL_SECONDARY_CUSTOMER_MASTER.Comp_Code as [Company Code]  From TSPL_SECONDARY_CUSTOMER_MASTER   "
        str = clsCommon.ShowSelectForm("SECCUSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function DeleteData(ByVal strCustCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim obj As clsSecondaryCustomer
            If clsCommon.myLen(strCustCode) > 0 Then
                obj = clsSecondaryCustomer.GetData(strCustCode, NavigatorType.Current)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Cust_Code) > 0) Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_SECONDARY_CUSTOMER_MASTER Where Cust_Code='" + strCustCode + "'")
                    clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Cust_Code, trans)
                Else
                    Throw New Exception("Document not found to delete.")
                End If
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSecondaryCustomer
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSecondaryCustomer
        Dim obj As clsSecondaryCustomer = Nothing
        Dim qry As String = "SELECT [Cust_Code], [Customer_Name],[Add1],[Add2],[Add3], [Distributor] , [City_Code],[State],[Country],[Phone1],[Phone2],[Fax],[Email],[WebSite], [Credit_Limit], [CURRENCY_CODE], [Status] FROM TSPL_SECONDARY_CUSTOMER_MASTER Where 1=1"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Cust_Code = (select MIN(Cust_Code) from TSPL_SECONDARY_CUSTOMER_MASTER)"
            Case NavigatorType.Last
                qry += " and Cust_Code = (select Max(Cust_Code) from TSPL_SECONDARY_CUSTOMER_MASTER)"
            Case NavigatorType.Next
                qry += " and Cust_Code = (select Min(Cust_Code) from TSPL_SECONDARY_CUSTOMER_MASTER where  Cust_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Cust_Code = (select Max(Cust_Code) from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Cust_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Try
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsSecondaryCustomer()
                obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                obj.Distributor = clsCommon.myCstr(dt.Rows(0)("Distributor"))
                obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                obj.State = clsCommon.myCstr(dt.Rows(0)("State"))
                obj.Country = clsCommon.myCstr(dt.Rows(0)("Country"))
                obj.Phone1 = clsCommon.myCstr(dt.Rows(0)("Phone1"))
                obj.Phone2 = clsCommon.myCstr(dt.Rows(0)("Phone2"))
                obj.Fax = clsCommon.myCstr(dt.Rows(0)("Fax"))
                obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
                obj.WebSite = clsCommon.myCstr(dt.Rows(0)("Website"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.Credit_Limit = clsCommon.myCdbl(dt.Rows(0)("Credit_Limit"))
                obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsSecondaryCustomer)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each obj As clsSecondaryCustomer In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
                clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
                clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
                clsCommon.AddColumnsForChange(coll, "Add3", obj.Add3)
                clsCommon.AddColumnsForChange(coll, "Distributor", obj.Distributor)
                clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
                clsCommon.AddColumnsForChange(coll, "State", obj.State)
                clsCommon.AddColumnsForChange(coll, "Country", obj.Country)
                clsCommon.AddColumnsForChange(coll, "Phone1", obj.Phone1)
                clsCommon.AddColumnsForChange(coll, "Phone2", obj.Phone2)
                clsCommon.AddColumnsForChange(coll, "Fax", obj.Fax)
                clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
                clsCommon.AddColumnsForChange(coll, "WebSite", obj.WebSite)
                clsCommon.AddColumnsForChange(coll, "Credit_Limit", obj.Credit_Limit)
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE)
                clsCommon.AddColumnsForChange(coll, "Modified_By", obj.Modify_By)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(obj.Modify_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)

                Dim Count As Integer = clsDBFuncationality.getSingleValue("select Count(*) from TSPL_SECONDARY_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Cust_Code + "'", trans)
                If Count <= 0 Then
                    obj.Cust_Code = clsDBFuncationality.getSingleValue("select isnull(max(Cust_Code),'') from TSPL_SECONDARY_CUSTOMER_MASTER", trans)
                    If clsCommon.myLen(obj.Cust_Code) <= 0 Then
                        obj.Cust_Code = "C0001"
                    Else
                        obj.Cust_Code = clsCommon.incval(obj.Cust_Code)
                    End If
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_CUSTOMER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_CUSTOMER_MASTER", OMInsertOrUpdate.Update, "TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code='" + obj.Cust_Code + "'", trans)
                End If
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Cust_Code, obj.arrCustomFields, trans)
            Next
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            trans.Rollback()
        End Try
        Return isSaved
    End Function

    Public Shared Function getCustomerName(ByVal strCustCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER WHERE Cust_Code='" + strCustCode + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getCityName(ByVal strCityCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER WHERE City_Code='" + strCityCode + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getStateName(ByVal strStateCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STATE_NAME from TSPL_STATE_MASTER WHERE STATE_CODE='" + strStateCode + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getDistributorName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + strCode + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class


Public Class clsCustomerMasterNew

#Region "Variables"
    Public ArrVisi As New List(Of String)
    Public address As String = Nothing
    Public Cust_Code As String
    Public Customer_Name As String
    Public Add1 As String
    Public Add2 As String
    Public Add3 As String
    Public Closing_Date As String
    Public Cust_Category_Code As String
    Public Cust_Group_Code As String
    Public Cust_Type_Code As String
    Public Route_No As String
    Public City_Code As String
    Public State As String
    Public Country As String
    Public Phone1 As String
    Public Phone2 As String
    Public Fax As String
    Public Email As String
    Public WebSite As String
    Public Contact_Person_Name As String
    Public Contact_Person_Phone As String
    Public Contact_Person_Fax As String
    Public Contact_Person_Website As String
    Public Contact_Person_Email As String
    Public Terms_Code As String
    Public Cust_Account As String
    Public Tax_Group As String
    Public TAX1 As String
    Public TAX1_Rate As Double
    Public TAX2 As String
    Public TAX2_Rate As Double
    Public TAX3 As String
    Public TAX3_Rate As Double
    Public TAX4 As String
    Public TAX4_Rate As Double
    Public TAX5 As String
    Public TAX5_Rate As Double
    Public TAX6 As String
    Public TAX6_Rate As Double
    Public TAX7 As String
    Public TAX7_Rate As Double
    Public TAX8 As String
    Public TAX8_Rate As Double
    Public TAX9 As String
    Public TAX9_Rate As Double
    Public TAX10 As String
    Public TAX10_Rate As Double
    Public Payment_Code As String
    Public Service_Tax_No As String
    Public Tin_No As String
    Public Lst_No As String
    Public Form_Type As String
    Public Channel_Code As String
    Public Status As Char
    Public OnHold As Char
    Public Remarks1 As String
    Public Remarks2 As String
    Public Additional1 As String
    Public Additional2 As String
    Public Additional3 As String
    Public Salesman_Code As String
    Public OutLet_Commossion As Double
    Public Balance_ToDate As Double
    Public Credit_Limit As Double
    Public Credit_Limit_Alert_Type As String
    'Public Created_By As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
    'Public Created_Date As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
    'Public Modify_By As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
    'Public Modify_Date As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
    'Public Comp_Code As String = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)



    Public Created_By As String
    Public Created_Date As String
    Public Modify_By As String
    Public Modify_Date As String
    Public Comp_Code As String
    Public Route_Group As String
    Public CST As String
    Public ECC As String
    Public Range As String
    Public Collectorate As String
    Public PAN As String
    Public Division As String
    Public Parent_Customer_No As String
    Public Customer_Class As String
    Public Credit_Customer As String
    Public LastInvoice_No As String
    Public LastInvoice_Date As String
    Public Price_Code As String
    Public Price_CodeNon As String
    Public Inter_Branch As Char
    Public Transaction_Type As String
    Public isCustRouteType As Boolean
    Public custdis As String


    Public PIN_Code As String = ""
    Public Cust_DOB As DateTime? = Nothing
    Public Cust_Spouse_DOB As DateTime? = Nothing
    Public Anniversary_Date As DateTime? = Nothing
    Public Gender As String = ""
    Public Occation As String = ""


    Public ArrItem As List(Of clsCustomerItemdetail) = Nothing
#End Region
    Public Function SaveDataPOS(ByVal obj As clsCustomerMasterNew, ByVal objShip As clsShipToLocation, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveDataPOS(obj, isNewEntry, trans)
            clsShipToLocation.SaveData(objShip, trans)
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveDataPOS(ByVal obj As clsCustomerMasterNew, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Phone1", obj.Phone1)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
            clsCommon.AddColumnsForChange(coll, "State", obj.State)
            clsCommon.AddColumnsForChange(coll, "Country", obj.Country)
            clsCommon.AddColumnsForChange(coll, "PIN_Code", obj.PIN_Code)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Email", obj.Contact_Person_Email)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Name", obj.Contact_Person_Name)

            If obj.Cust_DOB Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Cust_DOB", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "Cust_DOB", clsCommon.GetPrintDate(obj.Cust_DOB, "dd/MMM/yyyy"), True)
            End If

            If obj.Cust_Spouse_DOB Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Cust_Spouse_DOB", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "Cust_Spouse_DOB", clsCommon.GetPrintDate(obj.Cust_Spouse_DOB, "dd/MMM/yyyy"), True)
            End If
            If obj.Anniversary_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "Anniversary_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "Anniversary_Date", clsCommon.GetPrintDate(obj.Anniversary_Date, "dd/MMM/yyyy"), True)
            End If
            'clsCommon.AddColumnsForChange(coll, "Cust_DOB", IIf(obj.Cust_DOB IsNot Nothing, clsCommon.GetPrintDate(obj.Cust_DOB, "dd/MMM/yyyy"), Nothing), True)
            'clsCommon.AddColumnsForChange(coll, "Cust_Spouse_DOB", IIf(obj.Cust_Spouse_DOB IsNot Nothing, clsCommon.GetPrintDate(obj.Cust_Spouse_DOB, "dd/MMM/yyyy"), Nothing), True)
            'clsCommon.AddColumnsForChange(coll, "Anniversary_Date", IIf(obj.Anniversary_Date IsNot Nothing, clsCommon.GetPrintDate(obj.Anniversary_Date, "dd/MMM/yyyy"), Nothing), True)
            clsCommon.AddColumnsForChange(coll, "Gender", obj.Gender)
            clsCommon.AddColumnsForChange(coll, "Occation", obj.Occation)
            clsCommon.AddColumnsForChange(coll, "OnHold", clsCommon.myCstr(obj.OnHold))
            clsCommon.AddColumnsForChange(coll, "Modify_By", obj.Modify_By)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", obj.Modify_Date)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            'clsCommon.AddColumnsForChange(coll, "Add3", obj.Add3)
            'clsCommon.AddColumnsForChange(coll, "Cust_Category_Code", obj.Cust_Category_Code)
            'clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code)
            'clsCommon.AddColumnsForChange(coll, "Cust_Type_Code", obj.Cust_Type_Code)
            'clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            'Dim Route_Desc As String = clsDBFuncationality.getSingleValue("Select Route_Desc from TSPL_ROUTE_MASTER Where Route_No='" + obj.Route_No + "'", trans)
            'clsCommon.AddColumnsForChange(coll, "Route_Desc", Route_Desc)


            'clsCommon.AddColumnsForChange(coll, "Phone1", obj.Phone1)
            'clsCommon.AddColumnsForChange(coll, "Phone2", obj.Phone2)
            'clsCommon.AddColumnsForChange(coll, "Fax", obj.Fax)
            'clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
            'clsCommon.AddColumnsForChange(coll, "WebSite", obj.WebSite)


            'clsCommon.AddColumnsForChange(coll, "Contact_Person_Phone", obj.Contact_Person_Phone)
            'clsCommon.AddColumnsForChange(coll, "Contact_Person_Fax", obj.Contact_Person_Fax)
            'clsCommon.AddColumnsForChange(coll, "Contact_Person_Website", obj.Contact_Person_Website)

            'clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            'clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account)
            'clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            'clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            'clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            'clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            'clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            'clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            'clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            'clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            'clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            'clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            'clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            'clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            'clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            'clsCommon.AddColumnsForChange(coll, "Service_Tax_No", obj.Service_Tax_No)
            'clsCommon.AddColumnsForChange(coll, "Tin_No", obj.Tin_No)
            'clsCommon.AddColumnsForChange(coll, "Lst_No", obj.Lst_No)
            'clsCommon.AddColumnsForChange(coll, "Form_Type", obj.Form_Type)
            'clsCommon.AddColumnsForChange(coll, "Channel_Code", obj.Channel_Code)
            'Dim Channel_Desc As String = clsDBFuncationality.getSingleValue("Select Channel_Name  from TSPL_CHANNEL_MASTER where Channel_Id='" + obj.Channel_Code + "'", trans)
            'clsCommon.AddColumnsForChange(coll, "Channel_Desc", Channel_Desc)
            'clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            'If clsCommon.CompairString(obj.Status, "Y") = CompairStringResult.Equal Then
            '    clsCommon.AddColumnsForChange(coll, "Closing_Date", obj.Closing_Date)
            'End If

            'clsCommon.AddColumnsForChange(coll, "Remarks1", obj.Remarks1)
            'clsCommon.AddColumnsForChange(coll, "Remarks2", obj.Remarks2)
            'clsCommon.AddColumnsForChange(coll, "Additional1", obj.Additional1)
            'clsCommon.AddColumnsForChange(coll, "Additional2", obj.Additional2)
            'clsCommon.AddColumnsForChange(coll, "Additional3", obj.Additional3)
            'clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
            'Dim Salesman_Desc As String = clsDBFuncationality.getSingleValue("Select Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + obj.Salesman_Code + "'", trans)
            'clsCommon.AddColumnsForChange(coll, "Salesman_Desc", Salesman_Desc)
            'clsCommon.AddColumnsForChange(coll, "OutLet_Commossion", obj.OutLet_Commossion)
            'clsCommon.AddColumnsForChange(coll, "Balance_ToDate", obj.Balance_ToDate)
            'clsCommon.AddColumnsForChange(coll, "Credit_Limit", obj.Credit_Limit)
            'clsCommon.AddColumnsForChange(coll, "Credit_Limit_Alert_Type", obj.Credit_Limit_Alert_Type)
            'clsCommon.AddColumnsForChange(coll, "Modify_By", obj.Modify_By)
            'clsCommon.AddColumnsForChange(coll, "Modify_Date", obj.Modify_Date)
            'clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
            'clsCommon.AddColumnsForChange(coll, "Route_Group", obj.Route_Group)
            'clsCommon.AddColumnsForChange(coll, "CST", obj.CST)
            'clsCommon.AddColumnsForChange(coll, "ECC", obj.ECC)
            'clsCommon.AddColumnsForChange(coll, "Range", obj.Range)
            'clsCommon.AddColumnsForChange(coll, "Collectorate", obj.Collectorate)
            'clsCommon.AddColumnsForChange(coll, "PAN", obj.PAN)
            'clsCommon.AddColumnsForChange(coll, "Division", obj.Division)
            'clsCommon.AddColumnsForChange(coll, "Parent_Customer_No", obj.Parent_Customer_No)
            'clsCommon.AddColumnsForChange(coll, "Customer_Class", obj.Customer_Class)
            'clsCommon.AddColumnsForChange(coll, "Credit_Customer", obj.Credit_Customer)
            'clsCommon.AddColumnsForChange(coll, "LastInvoice_No", obj.LastInvoice_No)
            'clsCommon.AddColumnsForChange(coll, "LastInvoice_Date", obj.LastInvoice_Date)
            'clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            'clsCommon.AddColumnsForChange(coll, "Price_CodeNon", obj.Price_CodeNon)
            'clsCommon.AddColumnsForChange(coll, "Inter_Branch", obj.Inter_Branch)
            'clsCommon.AddColumnsForChange(coll, "Transaction_Type", obj.Transaction_Type)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_MASTER", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.Cust_Code + "'", trans)
            End If
            '----------------------If Routed Customer then Ite Updates Details In These Selected Database in Array------



        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsCustomerMasterNew, ByVal arrVisi As List(Of String), ByVal isNewEntry As Boolean, ByVal arrDBName As List(Of String)) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveHistory(obj.Cust_Code, isNewEntry, arrDBName, trans) Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
                clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
                clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
                clsCommon.AddColumnsForChange(coll, "Add3", obj.Add3)

                If obj.Cust_Category_Code = "" Then
                    'clsCommon.AddColumnsForChange(coll, "Cust_Category_Code", "NULL")
                Else
                    clsCommon.AddColumnsForChange(coll, "Cust_Category_Code", obj.Cust_Category_Code)
                End If

                clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code)
                clsCommon.AddColumnsForChange(coll, "Cust_Type_Code", obj.Cust_Type_Code)

                If obj.Route_No <> "" Then
                    clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
                    Dim Route_Desc As String = clsDBFuncationality.getSingleValue("Select Route_Desc from TSPL_ROUTE_MASTER Where Route_No='" + obj.Route_No + "'", trans)
                    clsCommon.AddColumnsForChange(coll, "Route_Desc", Route_Desc)

                End If



                clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
                clsCommon.AddColumnsForChange(coll, "State", obj.State)
                clsCommon.AddColumnsForChange(coll, "Country", obj.Country)
                clsCommon.AddColumnsForChange(coll, "Phone1", obj.Phone1)
                clsCommon.AddColumnsForChange(coll, "Phone2", obj.Phone2)
                clsCommon.AddColumnsForChange(coll, "Fax", obj.Fax)
                clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
                clsCommon.AddColumnsForChange(coll, "WebSite", obj.WebSite)
                clsCommon.AddColumnsForChange(coll, "Contact_Person_Name", obj.Contact_Person_Name)
                clsCommon.AddColumnsForChange(coll, "Contact_Person_Phone", obj.Contact_Person_Phone)
                clsCommon.AddColumnsForChange(coll, "Contact_Person_Fax", obj.Contact_Person_Fax)
                clsCommon.AddColumnsForChange(coll, "Contact_Person_Website", obj.Contact_Person_Website)
                clsCommon.AddColumnsForChange(coll, "Contact_Person_Email", obj.Contact_Person_Email)
                clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
                clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account)
                clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
                clsCommon.AddColumnsForChange(coll, "Service_Tax_No", obj.Service_Tax_No)
                clsCommon.AddColumnsForChange(coll, "Tin_No", obj.Tin_No)
                clsCommon.AddColumnsForChange(coll, "Lst_No", obj.Lst_No)
                clsCommon.AddColumnsForChange(coll, "Form_Type", obj.Form_Type)
                clsCommon.AddColumnsForChange(coll, "Channel_Code", obj.Channel_Code)
                Dim Channel_Desc As String = clsDBFuncationality.getSingleValue("Select Channel_Name  from TSPL_CHANNEL_MASTER where Channel_Id='" + obj.Channel_Code + "'", trans)
                clsCommon.AddColumnsForChange(coll, "Channel_Desc", Channel_Desc)
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                If clsCommon.CompairString(obj.Status, "Y") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Closing_Date", obj.Closing_Date)
                End If
                clsCommon.AddColumnsForChange(coll, "OnHold", obj.OnHold)
                clsCommon.AddColumnsForChange(coll, "Remarks1", obj.Remarks1)
                clsCommon.AddColumnsForChange(coll, "Remarks2", obj.Remarks2)
                clsCommon.AddColumnsForChange(coll, "Additional1", obj.Additional1)
                clsCommon.AddColumnsForChange(coll, "Additional2", obj.Additional2)
                clsCommon.AddColumnsForChange(coll, "Additional3", obj.Additional3)
                clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
                Dim Salesman_Desc As String = clsDBFuncationality.getSingleValue("Select Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + obj.Salesman_Code + "'", trans)
                clsCommon.AddColumnsForChange(coll, "Salesman_Desc", Salesman_Desc)
                clsCommon.AddColumnsForChange(coll, "OutLet_Commossion", obj.OutLet_Commossion)
                clsCommon.AddColumnsForChange(coll, "Balance_ToDate", obj.Balance_ToDate)
                clsCommon.AddColumnsForChange(coll, "Credit_Limit", obj.Credit_Limit)
                clsCommon.AddColumnsForChange(coll, "Credit_Limit_Alert_Type", obj.Credit_Limit_Alert_Type)
                clsCommon.AddColumnsForChange(coll, "Modify_By", obj.Modify_By)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", obj.Modify_Date)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
                clsCommon.AddColumnsForChange(coll, "Route_Group", obj.Route_Group)
                clsCommon.AddColumnsForChange(coll, "CST", obj.CST)
                clsCommon.AddColumnsForChange(coll, "ECC", obj.ECC)
                clsCommon.AddColumnsForChange(coll, "Range", obj.Range)
                clsCommon.AddColumnsForChange(coll, "Collectorate", obj.Collectorate)
                clsCommon.AddColumnsForChange(coll, "PAN", obj.PAN)
                clsCommon.AddColumnsForChange(coll, "Division", obj.Division)
                clsCommon.AddColumnsForChange(coll, "Parent_Customer_No", obj.Parent_Customer_No)
                clsCommon.AddColumnsForChange(coll, "Customer_Class", obj.Customer_Class)
                clsCommon.AddColumnsForChange(coll, "Credit_Customer", obj.Credit_Customer)
                clsCommon.AddColumnsForChange(coll, "LastInvoice_No", obj.LastInvoice_No)
                clsCommon.AddColumnsForChange(coll, "LastInvoice_Date", obj.LastInvoice_Date)
                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                clsCommon.AddColumnsForChange(coll, "Price_CodeNon", obj.Price_CodeNon)
                clsCommon.AddColumnsForChange(coll, "Inter_Branch", obj.Inter_Branch)
                clsCommon.AddColumnsForChange(coll, "Transaction_Type", obj.Transaction_Type)


                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, arrDBName, "TSPL_CUSTOMER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTableInSelectedDatabase(coll, arrDBName, "TSPL_CUSTOMER_MASTER", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.Cust_Code + "'", trans)
                End If
                '----------------------If Routed Customer then Ite Updates Details In These Selected Database in Array------

                If arrDBName IsNot Nothing Then
                    If arrDBName.Count <= 0 Then
                        Throw New Exception("Please Select Atleast Single database")
                    ElseIf obj.isCustRouteType Then
                        If arrDBName.Count > 1 Then
                            Dim Msg As String = "This Customer Has Type 'Route', It can not be inserted into multiple DataBase " + Environment.NewLine + ""
                            Msg += "Please Select only a Single DataBase"
                            Throw New Exception(Msg)
                        Else
                            Dim arrDBName1 As New List(Of String)
                            Dim dtDb As DataTable = clsDBFuncationality.GetDataTable("Select DataBase_Name  from TSPL_COMPANY_MASTER Where DataBase_Name not in (" + clsCommon.GetMulcallString(arrDBName) + ")", trans)
                            For Each drdb As DataRow In dtDb.Rows
                                arrDBName1.Add(clsCommon.myCstr(drdb("DataBase_Name")))
                            Next
                            For ii As Integer = 0 To arrDBName1.Count - 1
                                Dim qry As String = "update " + clsCommon.myCstr(arrDBName1(ii)) + ".dbo.TSPL_CUSTOMER_MASTER set Status='Y',Closing_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' where Cust_Code='" + obj.Cust_Code + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            Next
                        End If
                    End If
                End If
                '------------------------------------------------------------------------------------------------------------

                '---------------------Visi-Detail--------------------------Update Visi Master----

                If arrVisi IsNot Nothing Then
                    Dim collVisi As New Hashtable()
                    clsCommon.AddColumnsForChange(collVisi, "Customer_Id", obj.Cust_Code)
                    clsCommon.AddColumnsForChange(collVisi, "Customer_name", obj.Customer_Name)
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_VISI_MASTER set Customer_Id='' , Customer_name='' Where Customer_Id='" + obj.Cust_Code + "'", trans)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTableInSelectedDatabase(collVisi, arrDBName, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_Id IN (" + clsCommon.GetMulcallString(arrVisi) + ")", trans)

                End If

                '----------------------------------------------------------------------------------

                If clsCustomerItemdetail.SaveData(Cust_Code, arrDBName, ArrItem, trans) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function SaveHistory(ByVal Cust_Code As String, ByVal isNewEntry As Boolean, ByVal arrDBName As List(Of String), ByVal trans As SqlTransaction) As Boolean
        Try
            If isNewEntry = False Then
                If common.clsCommon.MyMessageBoxShow("Do you want to save Amendment history?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                    Dim Qry As String = "INSERT INTO TSPL_CUSTOMER_MASTER_HISTORY (Cust_Code, Customer_Name, Add1, Add2, Add3, Closing_Date, Cust_Category_Code, Cust_Group_Code, "
                    Qry += "Cust_Type_Code, Route_No, Route_Desc, Price_Code, City_Code, State, Country, Phone1, Phone2, Fax, Email, WebSite, Contact_Person_Name, Contact_Person_Phone, Contact_Person_Fax, "
                    Qry += "Contact_Person_Website, Contact_Person_Email, Terms_Code, Cust_Account, Tax_Group, TAX1, TAX1_Rate , "
                    Qry += "TAX2, TAX2_Rate, TAX3, TAX3_Rate, TAX4, TAX4_Rate, TAX5, TAX5_Rate, TAX6, TAX6_Rate, TAX7, TAX7_Rate, TAX8, TAX8_Rate, TAX9, TAX9_Rate, "
                    Qry += "Tax10, TAX10_Rate, Payment_Code, Service_Tax_No, Tin_No, Lst_No, Form_Type, Channel_Code, Channel_Desc, Status, OnHold, Remarks1, Remarks2, "
                    Qry += "Additional1, additional2, Additional3, Salesman_Code, Salesman_Desc, OutLet_Commossion, Balance_ToDate, Credit_Limit, Created_By, "
                    Qry += "Created_Date, Modify_By, Modify_Date,Comp_Code, Route_Group,CST, ECC, Range, Collectorate, PAN, Division, Amendment_Date )"
                    Qry += "  "
                    Qry += "SELECT Cust_code, Customer_Name, Add1, Add2, Add3, Closing_Date, Cust_Category_Code, Cust_Group_Code, Cust_Type_Code, Route_No, Route_Desc, "
                    Qry += "Price_Code, City_Code, State, Country, Phone1, Phone2, Fax, Email, WebSite, Contact_Person_Name, Contact_Person_Phone, "
                    Qry += "Contact_Person_Fax, Contact_Person_Website, Contact_Person_Email, Terms_Code, Cust_Account, Tax_Group, TAX1, TAX1_Rate , TAX2, TAX2_Rate, "
                    Qry += "TAX3, TAX3_Rate, TAX4, TAX4_Rate, TAX5, TAX5_Rate, TAX6, TAX6_Rate, TAX7, TAX7_Rate, TAX8, TAX8_Rate, TAX9, TAX9_Rate, TAX10, TAX10_Rate, Payment_Code, Service_Tax_No, Tin_No, Lst_No, Form_Type, Channel_Code, Channel_Desc, Status, OnHold, Remarks1, Remarks2, Additional1, "
                    Qry += "additional2, Additional3, Salesman_Code, Salesman_Desc, OutLet_Commossion, Balance_ToDate, Credit_Limit, '" + objCommonVar.CurrentUserCode + "', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "', "
                    Qry += " '" + objCommonVar.CurrentUserCode + "', '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "', Comp_Code, Route_Group, CST, ECC, Range, Collectorate, PAN, Division, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' "
                    Qry += "FROM TSPL_CUSTOMER_MASTER Where Cust_Code='" + Cust_Code + "'"
                    clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(Qry, arrDBName, trans)

                    'connectSql.RunSp("sp_TSPL_CUSTOMER_MASTER_HISTORY_INSERT", New SqlParameter("@Cust_Code", obj.Cust_Code), New SqlParameter("@Customer_Name", Me.txtCustomerName.Text), New SqlParameter("@Add1", Me.txtAdd1.Text), New SqlParameter("@Add2", Me.txtAdd2.Text), New SqlParameter("@Add3", Me.txtAdd3.Text), New SqlParameter("@Closing_Date", Format(Me.dtClosing.Value, "dd/MM/yyyy")), New SqlParameter("@Cust_Category_Code", Me.fndCusCategory.Value), New SqlParameter("@Cust_Group_Code", Me.fndCusgrp.Value), New SqlParameter("@Cust_Type_Code", Me.fndCusType.Value), New SqlParameter("@Route_No", Me.fndRoute.Value), New SqlParameter("@Route_Desc", Me.txtRoute.Text), New SqlParameter("@Price_Code", Me.txtPriceCode.Value), New SqlParameter("@City_Code", Me.fndCity.Value), New SqlParameter("@State", state_code), New SqlParameter("@Country", Me.txtCountry.Text), New SqlParameter("@Phone1", Me.txtPhone1.Text), New SqlParameter("@Phone2", Me.txtPhone2.Text), New SqlParameter("@Fax", Me.txtfax.Text), New SqlParameter("@Email", Me.txtEmail.Text), New SqlParameter("@WebSite", Me.txtWeb.Text), New SqlParameter("@Contact_Person_Name", Me.txtContactName.Text), New SqlParameter("@Contact_Person_Phone", Me.txtContPhone.Text), New SqlParameter("@Contact_Person_Fax", Me.txtContactFax.Text), New SqlParameter("@Contact_Person_Website", Me.txtContactWeb.Text), New SqlParameter("@Contact_Person_Email", Me.txtContactEmail.Text), New SqlParameter("@Terms_Code", Me.fndTrmsCode.Value), New SqlParameter("@Cust_Account", Me.fndAccntSet.Value), New SqlParameter("@Tax_Group", Me.fndTxGrp.Value), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Payment_Code", Me.fndPayCode.Value), New SqlParameter("@Service_Tax_No", Me.txtStaxNo.Text), New SqlParameter("@Tin_No", Me.txtTinNo.Text), New SqlParameter("@Lst_No", Me.txtLstNo.Text), New SqlParameter("@Form_Type", Me.drpformtype.Text), New SqlParameter("@Channel_Code", Me.fndChannel.Value), New SqlParameter("@Channel_Desc", Me.txtChannel.Text), New SqlParameter("@Status", strStatus), New SqlParameter("@OnHold", strHold), New SqlParameter("@Remarks1", Me.txtRemarks1.Text), New SqlParameter("@Remarks2", Me.txtRemarks2.Text), New SqlParameter("@Additional1", Me.txtAddInfo1.Text), New SqlParameter("@Additional2", Me.txtAddInfo2.Text), New SqlParameter("@Additional3", Me.txtAddInfo3.Text), New SqlParameter("@Salesman_Code", Me.fndSalePerson.Value), New SqlParameter("@Salesman_Desc", Me.txtSalesPerson.Text), New SqlParameter("@Visi_Id", Me.fndVisi.Value), New SqlParameter("@Visi_Desc", Me.txtVisi.Text), New SqlParameter("@OutLet_Commossion", OutComm), New SqlParameter("@Balance_ToDate", Bal1), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@routegroup", Me.fndroutegroup.Value), New SqlParameter("@cst", txtcst.Text.ToString()), New SqlParameter("@ecc", txtecc.Text), New SqlParameter("@range", txtrange.Text), New SqlParameter("@collectorate", txtcollect.Text), New SqlParameter("@pan", txtpan.Text), New SqlParameter("@division", txtdivision.Text), New SqlParameter("@Amendment_Date", connectSql.serverDate()))

                    '--------------------For Saving History Of Visi Master-------------------
                    Dim AmndNO As Integer = 0
                    Dim qryInsert As String = "select Visi_Id, VisiMake, Visi_Chasis_No, Visi_Installation_date, Created_By, Created_Date, Modify_By, Modify_Date, Comp_Code, Customer_Id, Customer_name  from TSPL_VISI_MASTER Where Customer_Id='" + Cust_Code + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryInsert, trans)
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim qry1 As String = "select isNULL(MAX(Amendment_No), 0) as AmendmntNO  from TSPL_VISI_MASTER_HISTORY Where TSPL_VISI_MASTER_HISTORY.customer_Id='" + Cust_Code + "' And Visi_Id='" + dr("Visi_Id") + "'"
                            AmndNO = clsDBFuncationality.getSingleValue(qry1, trans)
                            AmndNO = AmndNO + 1
                            Dim collv As New Hashtable()
                            clsCommon.AddColumnsForChange(collv, "Visi_Id", dr("Visi_Id"))
                            clsCommon.AddColumnsForChange(collv, "VisiMake", dr("VisiMake"))
                            clsCommon.AddColumnsForChange(collv, "Visi_Chasis_No", dr("Visi_Chasis_No"))
                            clsCommon.AddColumnsForChange(collv, "Visi_Installation_date", dr("Visi_Installation_date"))
                            clsCommon.AddColumnsForChange(collv, "Created_By", dr("Created_By"))
                            clsCommon.AddColumnsForChange(collv, "Created_Date", dr("Created_Date"))
                            clsCommon.AddColumnsForChange(collv, "Modify_By", dr("Modify_By"))
                            clsCommon.AddColumnsForChange(collv, "Modify_Date", dr("Modify_Date"))
                            clsCommon.AddColumnsForChange(collv, "Comp_Code", dr("Customer_Id"))
                            clsCommon.AddColumnsForChange(collv, "Customer_Id", dr("Customer_Id"))
                            clsCommon.AddColumnsForChange(collv, "Customer_name", dr("Customer_name"))
                            clsCommon.AddColumnsForChange(collv, "Amendment_No", AmndNO)
                            clsCommon.AddColumnsForChange(collv, "History_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy hh:mm tt"))
                            clsCommonFunctionality.UpdateDataTableInSelectedDatabase(collv, arrDBName, "TSPL_VISI_MASTER_History", OMInsertOrUpdate.Insert, "", trans)
                        Next
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
            Throw New Exception(ex.Message)
        End Try
        '--------------------------------------Code Ends Here----------------------------------
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As clsCustomerMasterNew
        Return GetData(strCode, NavigatorType.Current, trans)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerMasterNew
        Dim obj As clsCustomerMasterNew = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_MASTER.* from TSPL_CUSTOMER_MASTER where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and Cust_Code = (select MIN(Cust_Code) from TSPL_CUSTOMER_MASTER )"
            Case NavigatorType.Last
                qry += " and Cust_Code = (select Max(Cust_Code) from TSPL_CUSTOMER_MASTER )"
            Case NavigatorType.Next
                qry += " and Cust_Code = (select Min(Cust_Code) from TSPL_CUSTOMER_MASTER where Cust_Code > '" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Cust_Code = (select Max(Cust_Code) from TSPL_CUSTOMER_MASTER where Cust_Code < '" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Cust_Code = '" + strCode + "'"
        End Select


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsCustomerMasterNew()
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
            obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
            obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
            obj.Closing_Date = clsCommon.myCstr(dt.Rows(0)("Closing_Date"))
            obj.Cust_Category_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Category_Code"))
            obj.Cust_Group_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Group_Code"))
            obj.Cust_Type_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Type_Code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            'obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
            obj.State = clsCommon.myCstr(dt.Rows(0)("State"))
            obj.Country = clsCommon.myCstr(dt.Rows(0)("Country"))
            obj.Phone1 = clsCommon.myCstr(dt.Rows(0)("Phone1"))
            obj.Phone2 = clsCommon.myCstr(dt.Rows(0)("Phone2"))
            obj.Fax = clsCommon.myCstr(dt.Rows(0)("Fax"))
            obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
            obj.WebSite = clsCommon.myCstr(dt.Rows(0)("WebSite"))
            obj.Contact_Person_Name = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Name"))
            obj.Contact_Person_Phone = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Phone"))
            obj.Contact_Person_Fax = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Fax"))
            obj.Contact_Person_Website = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Website"))
            obj.Contact_Person_Email = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Email"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Cust_Account = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Service_Tax_No = clsCommon.myCstr(dt.Rows(0)("Service_Tax_No"))
            obj.Tin_No = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
            obj.Lst_No = clsCommon.myCstr(dt.Rows(0)("Lst_No"))
            obj.Form_Type = clsCommon.myCstr(dt.Rows(0)("Form_Type"))
            obj.Channel_Code = clsCommon.myCstr(dt.Rows(0)("Channel_Code"))
            'obj.Channel_Desc = clsCommon.myCstr(dt.Rows(0)("Channel_Desc"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.OnHold = clsCommon.myCstr(dt.Rows(0)("OnHold"))
            obj.Remarks1 = clsCommon.myCstr(dt.Rows(0)("Remarks1"))
            obj.Remarks2 = clsCommon.myCstr(dt.Rows(0)("Remarks2"))
            obj.Additional1 = clsCommon.myCstr(dt.Rows(0)("Additional1"))
            obj.Additional2 = clsCommon.myCstr(dt.Rows(0)("Additional2"))
            obj.Additional3 = clsCommon.myCstr(dt.Rows(0)("Additional3"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            'obj.Salesman_Desc = clsCommon.myCstr(dt.Rows(0)("Salesman_Desc"))
            'obj.Visi_Id = clsCommon.myCstr(dt.Rows(0)("Visi_Id"))
            'obj.Visi_Desc = clsCommon.myCstr(dt.Rows(0)("Visi_Desc"))
            obj.OutLet_Commossion = clsCommon.myCdbl(dt.Rows(0)("OutLet_Commossion"))


            obj.Balance_ToDate = clsCommon.myCdbl(dt.Rows(0)("Balance_ToDate"))



            obj.Credit_Limit = clsCommon.myCdbl(dt.Rows(0)("Credit_Limit"))

            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Route_Group = clsCommon.myCstr(dt.Rows(0)("Route_Group"))
            obj.CST = clsCommon.myCstr(dt.Rows(0)("CST"))
            obj.ECC = clsCommon.myCstr(dt.Rows(0)("ECC"))
            obj.Range = clsCommon.myCstr(dt.Rows(0)("Range"))
            obj.Collectorate = clsCommon.myCstr(dt.Rows(0)("Collectorate"))
            obj.PAN = clsCommon.myCstr(dt.Rows(0)("PAN"))
            obj.Division = clsCommon.myCstr(dt.Rows(0)("Division"))
            obj.Parent_Customer_No = clsCommon.myCstr(dt.Rows(0)("Parent_Customer_No"))
            obj.Customer_Class = clsCommon.myCstr(dt.Rows(0)("Customer_Class"))
            obj.Credit_Customer = clsCommon.myCstr(dt.Rows(0)("Credit_Customer"))
            obj.LastInvoice_No = clsCommon.myCstr(dt.Rows(0)("LastInvoice_No"))
            obj.LastInvoice_Date = clsCommon.myCstr(dt.Rows(0)("LastInvoice_Date"))
            obj.Price_CodeNon = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
            obj.Inter_Branch = clsCommon.myCstr(dt.Rows(0)("Inter_Branch"))
            obj.Transaction_Type = clsCommon.myCstr(dt.Rows(0)("TRANSACTION_TYPE"))
            obj.Credit_Limit_Alert_Type = clsCommon.myCstr(dt.Rows(0)("Credit_Limit_Alert_Type"))
            obj.PIN_Code = clsCommon.myCstr(dt.Rows(0)("PIN_Code"))
            If dt.Rows(0)("Cust_DOB") IsNot DBNull.Value Then
                obj.Cust_DOB = clsCommon.myCDate(dt.Rows(0)("Cust_DOB"))
            End If
            If dt.Rows(0)("Cust_Spouse_DOB") IsNot DBNull.Value Then
                obj.Cust_Spouse_DOB = clsCommon.myCDate(dt.Rows(0)("Cust_Spouse_DOB"))
            End If
            If dt.Rows(0)("Anniversary_Date") IsNot DBNull.Value Then
                obj.Anniversary_Date = clsCommon.myCDate(dt.Rows(0)("Anniversary_Date"))
            End If
            obj.Gender = clsCommon.myCstr(dt.Rows(0)("Gender"))
            obj.Occation = clsCommon.myCstr(dt.Rows(0)("Occation"))

        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String, ByVal Trans As SqlTransaction) As String
        Dim qry As String = "select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Trans))
    End Function
    Public Shared Function GetOutStandingQry(ByVal AgeOfDate As DateTime, ByVal CutOfDate As DateTime, ByVal ActiveInactiveCustomer As String, ByVal DocTypeList As ArrayList, ByVal IsOnDueDate As String, Optional ByVal strCurrency As String = "", Optional ByVal CustomerList As ArrayList = Nothing, Optional ByVal LocationList As ArrayList = Nothing, Optional ByVal CustomerGroupList As ArrayList = Nothing, Optional ByVal IsParentCustomer As Boolean = False, Optional ByVal ParentCustomerList As ArrayList = Nothing, Optional ByVal Is_Security As String = "", Optional ByVal ISShowCustomerInvoiceorDocNo As Boolean = False, Optional ByVal LoginUserMappCustCategoryListInUserMaster As ArrayList = Nothing, Optional ByVal CustomerCategory As ArrayList = Nothing) As String
        Try

            Dim Qry As String = " Select MAX(xxx.Comp_Code) AS Comp_Code, [Customer Id], MAX([Parent Code]) AS [Parent Code],MAX(Parent_Master.Customer_Name) as ParentName, MAX([Customer Name]) AS [Customer Name], MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code) AS Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) AS Cust_Group_Desc, [Document Id], MAX([Desc]) as [Desc]," &
             " SUM([Due Amount]*" & strCurrency & ")  AS [Due Amount]," &
"" & IIf(strCurrency = "1", "MAX(xxx.CURRENCY_CODE)", "'INR'") & "  as Currency,max(xxx.CURRENCY_CODE) As CURRENCY_CODE,MAX(xxx.ConvRate) As ConvRate, MAX([Due Date]) AS [Due Date], MAX(type) AS type, MAX([Document Date]) AS [Document Date], MAX(Ageing_Days) AS Ageing_Days, MAX(Document_Type) AS Document_Type, MAX(Location) AS Location,max(originalAmt) as originalAmt  from ("


            Qry += " SELECT  TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_Customer_Invoice_Head.Comp_Code,TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," &
         " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name]," &
         " " + IIf(ISShowCustomerInvoiceorDocNo = True, " TSPL_Customer_Invoice_Head.Document_No ", "(case when ISNULL( Against_Sale_No,'')<>'' then Against_Sale_No when ISNULL(Against_Sale_Return_No,'')<>'' then TSPL_Customer_Invoice_Head.Document_No  when ISNULL( AgainstScrap,'')<>'' then AgainstScrap else TSPL_Customer_Invoice_Head.Document_No end)") + "  as [Document Id], " &
         " TSPL_Customer_Invoice_Head.Description as [Desc], (case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then TSPL_Customer_Invoice_Head.Document_Total Else (TSPL_Customer_Invoice_Head.Document_Total-  ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R','K') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  where TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') ),0) " &
         "  - ISNULL((Select SUM(TSPL_RECEIPT_HEADER.Receipt_Amount) from TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' and  ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A') AND TSPL_RECEIPT_HEADER.Applied_RECEIPT=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)),0) " &
         ") *-1 end " &
         " - case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R','K') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  where TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') ),0) else 0 end -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SD_SALE_RETURN_HEAD.Status,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE  left outer join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo   LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No  where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Posted ,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN.Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap =TSPL_SCRAPINVOICE_HEAD.Invoice_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_SCRAPSALE_HEAD_RETURN.ispost,'0') =1),0) -isnull((Select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_Receipt_Adjustment_Header  LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head  RECEIPTADJ ON ISNULL(RECEIPTADJ.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' and RECEIPTADJ.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_Receipt_Adjustment_Header.Adjustment_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)),0) +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where CONVERT(DATE,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SD_SALE_RETURN_HEAD.Status,'0') =1 and innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) ) as [Due Amount],TSPL_Customer_Invoice_Head.CURRENCY_CODE, case when isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) <>0 then isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) else TSPL_Customer_Invoice_Head.ConvRate end as ConvRate  ,"


            Qry += "  TSPL_Customer_Invoice_Head.due_date as [Due Date],'' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date], "

            If clsCommon.CompairString(IsOnDueDate, "DueDate") = CompairStringResult.Equal Then
                Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Due_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
            ElseIf clsCommon.CompairString(IsOnDueDate, "DocumentDate") = CompairStringResult.Equal Then
                Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
            End If

            Qry += " case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," &
           " TSPL_Customer_Invoice_Head.Loc_Code as Location ,TSPL_Customer_Invoice_Head.Document_Total as originalAmt FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No " &
           " left outer join (  Select  TSPL_REVALUATION_DETAIL.AR_Invoice_No,TSPL_REVALUATION_HEAD.Currency_Rate,TSPL_REVALUATION_HEAD.Document_Date,TSPL_REVALUATION_HEAD.Document_No  " &
           " from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD  on TSPL_REVALUATION_DETAIL.Document_No =TSPL_REVALUATION_HEAD.Document_No and TSPL_REVALUATION_HEAD.Document_No in  (select top 1 h.Document_No  from TSPL_REVALUATION_HEAD as h  left outer join TSPL_REVALUATION_DETAIL d on d.Document_No=h.Document_No" &
           " where d.AR_Invoice_No=TSPL_REVALUATION_DETAIL.AR_Invoice_No and CONVERT(DATE,h.Document_Date,103)<=CONVERT(DATE,'" & AgeOfDate & "',103) and isnull(d.AR_Invoice_No ,'')<>'' order by h.Document_Date desc) where CONVERT(DATE,TSPL_REVALUATION_HEAD .Document_Date,103)<=CONVERT(DATE,'" & AgeOfDate & "',103) and isnull(AR_Invoice_No ,'')<>'')TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD .AR_Invoice_No =TSPL_Customer_Invoice_Head .Document_No where TSPL_Customer_Invoice_Head.Status='1' " & ActiveInactiveCustomer & " " &
           " UNION ALL SELECT ''  as ARINvoiceNo,  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name] ,Document_No as [Document Id]  ,Description as [Desc] ,(Total_Order_Amt)*-1 as [Due Amount] ,'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date]  , DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location,TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt  as originalAmt  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  " & ActiveInactiveCustomer & " " &
           " UNION ALL select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END, 'INR' AS CURRENCY_CODE, 1 AS ConvRate ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment,isnull(TSPL_Customer_Invoice_Head.Document_Total,0) as originalAmt from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') = TSPL_VCGL_Head.Document_No  where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1  " & ActiveInactiveCustomer & "   AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''" &
           " UNION ALL select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END, 'INR' AS CURRENCY_CODE, 1 AS ConvRate ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment,isnull(TSPL_Customer_Invoice_Head.Document_Total,0) as originalAmt from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') = TSPL_VCGL_Detail.Document_No  where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer'  " & ActiveInactiveCustomer & "  AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL ,'') =''" &
           " UNION ALL select ''  as ARINvoiceNo, TSPL_RECEIPT_HEADER.Comp_Code ,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc , "

            ''richa agarwal change on 5 Nov,2019 ERO/07/11/19-001090
            ' Qry += " (Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " & _
            '" COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " & _
            '" AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND APP_REC.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No and " & _
            '" CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z))*-1 End),"

            Qry += " (Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then " & Environment.NewLine &
" (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " &
        " COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " &
        " AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No and " &
        " CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z)) " &
        " Else (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " &
        " COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " &
        " AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND APP_REC.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No and " &
        " CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z))*-1 End),"




            ' Qry += " (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " & _
            '" COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " & _
            '" AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND APP_REC.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No and " & _
            '" CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z)) * CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 1 ELSE  -1 END ,"


            Qry += "TSPL_RECEIPT_HEADER.CURRENCY_CODE  AS CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate AS ConvRate ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" & AgeOfDate & "',103)), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location,TSPL_RECEIPT_HEADER.Receipt_Amount  as originalAmt  from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M', 'A','R') and TSPL_RECEIPT_HEADER.Posted='Y'  " & ActiveInactiveCustomer & "  " + Is_Security + "" &
           "  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') " &
            "  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') " &
                        " AND TSPL_RECEIPT_HEADER.Receipt_No NOT IN ( " & Environment.NewLine &
                                  " sELECT Applied_Receipt  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P')" & Environment.NewLine &
                                  " union all" & Environment.NewLine &
                                  " sELECT Receipt_No  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and  CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') )" & Environment.NewLine
            '" union all " & Environment.NewLine & _
            '" Select distinct TSPL_RECEIPT_DETAIL.Document_No  from TSPL_RECEIPT_DETAIL left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL .Receipt_No where TSPL_RECEIPT_DETAIL.Document_No in (Select ISNULL(Receipt_No ,'') from TSPL_RECEIPT_HEADER where Receipt_Type ='F' and isnull(Applied_Receipt ,'')='') and  CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))"




            ''richa agarwal use TSPL_Receipt_Adjustment_Header.ARInvoiceNo in place of Adjustment_no in below line to show ar invoice no BM00000007349
            ' Qry += " UNION ALL Select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_Receipt_Adjustment_Header.ARInvoiceNo as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], case when TSPL_Customer_Invoice_Head.Document_Type='C' then 0 else TSPL_Receipt_Adjustment_Header.Adjustment_Amount end *-1 as [Due Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
            ''            Qry += " UNION ALL Select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_Receipt_Adjustment_Header.ARInvoiceNo as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else TSPL_Receipt_Adjustment_Header.Adjustment_Amount * -1 end  as [Due Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as Location,TSPL_Customer_Invoice_Head.Document_Total as originalAmt from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' " & ActiveInactiveCustomer & " " &
            Qry += " UNION ALL SELECT ''  as ARINvoiceNo, TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name],Document_No as [Document Id] ,Description as [Desc] , Empty_Value*-1 AS [Due Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location,TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt as  originalAmt from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  " & ActiveInactiveCustomer & " " &
                              " UNION ALL SELECT '' as ARINvoiceNo,  TSPL_ADJUSTMENT_HEADER.Comp_Code,TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end,'INR' AS CURRENCY_CODE, 1 AS ConvRate,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code),0 as  originalAmt FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')=''  " & ActiveInactiveCustomer & "  " &
                          " ) XXX LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON XXX.[Customer Id]=TSPL_CUSTOMER_MASTER.Cust_Code" &
                          " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=XXX.[Parent Code]" &
                          " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code" &
                          " where  XXX.Document_Type in (" + clsCommon.GetMulcallString(DocTypeList) + "  ) " &
                          " and convert(date,XXX.[Document Date] ,103) <= convert(date,'" & CutOfDate & "',103)"
            If IsParentCustomer Then
                If ParentCustomerList IsNot Nothing AndAlso ParentCustomerList.Count > 0 Then
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " AND ((XXX.[Parent Code] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + ") and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ")) or XXX.[Customer Id] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + "))"
                    Else
                        Qry += " AND (XXX.[Parent Code] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + ") or XXX.[Customer Id] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + "))"
                    End If

                Else
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
                    End If
                End If
            Else
                Dim AllowtoSHOWParentChildCustomer As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSHOWParentChildCustomer, clsFixedParameterCode.AllowtoSHOWParentChildCustomer, Nothing)) = 1, True, False))
                If AllowtoSHOWParentChildCustomer = True Then
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and (XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") or XXX.[Parent Code]  in (" + clsCommon.GetMulcallString(CustomerList) + ") ) "
                    End If
                Else
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
                    End If
                End If

            End If

            'If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
            '    Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
            'End If
            If LocationList IsNot Nothing AndAlso LocationList.Count > 0 Then
                Qry += " and XXX.Location in (" + clsCommon.GetMulcallString(LocationList) + ") "
            End If
            If CustomerGroupList IsNot Nothing AndAlso CustomerGroupList.Count > 0 Then
                Qry += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(CustomerGroupList) + ") "
            End If
            If LoginUserMappCustCategoryListInUserMaster IsNot Nothing AndAlso LoginUserMappCustCategoryListInUserMaster.Count > 0 Then
                Qry += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (" + clsCommon.GetMulcallString(LoginUserMappCustCategoryListInUserMaster) + ") "
            End If
            If CustomerCategory IsNot Nothing AndAlso CustomerCategory.Count > 0 Then
                Qry += " and TSPL_CUSTOMER_MASTER.cust_category_code in (" + clsCommon.GetMulcallString(CustomerCategory) + ") "
            End If

            ''richa 12 Dec, 2016
            'Qry += " AND XXX.[Document Id]  NOT IN (sELECT TSPL_RECEIPT_DETAIL.Document_No FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER rh  ON TSPL_RECEIPT_DETAIL.Receipt_No  =rh.Receipt_No left outer join TSPL_BANK_REVERSE on isnull(TSPL_BANK_REVERSE.Document_No,'') = rh.Receipt_No  WHERE TSPL_RECEIPT_DETAIL.Document_No=" + Environment.NewLine & _
            '" XXX.[Document Id] AND TSPL_RECEIPT_DETAIL.Applied_Amount =case when XXX.[Due Amount]  <0 then XXX.[Due Amount]* -1 else XXX.[Due Amount] end" + Environment.NewLine & _
            '" AND XXX.Document_Type IN ('CR','DR') AND rh.Posted='Y' AND ISNULL(TSPL_RECEIPT_DETAIL.Document_No,'')<>'' AND Convert(Date, rh.Receipt_Date , 103) <=convert(date,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Document_No,'')<>rh.Receipt_No )"
            Qry += " AND XXX.ARINvoiceNo  NOT IN (sELECT TSPL_RECEIPT_DETAIL.Document_No FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER rh  ON TSPL_RECEIPT_DETAIL.Receipt_No  =rh.Receipt_No left outer join TSPL_BANK_REVERSE on isnull(TSPL_BANK_REVERSE.Document_No,'') = rh.Receipt_No  WHERE TSPL_RECEIPT_DETAIL.Document_No=" + Environment.NewLine &
           " XXX.ARINvoiceNo AND TSPL_RECEIPT_DETAIL.Applied_Amount =case when XXX.[Due Amount]  <0 then XXX.[Due Amount]* -1 else XXX.[Due Amount] end" + Environment.NewLine &
           " AND XXX.Document_Type IN ('CR','DR') AND rh.Posted='Y' AND ISNULL(TSPL_RECEIPT_DETAIL.Document_No,'')<>'' AND Convert(Date, rh.Receipt_Date , 103) <=convert(date,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Document_No,'')<>rh.Receipt_No )"


            ''--------
            Qry += " Group By XXX.[Customer Id], XXX.[Document Id]"
            'Qry += "AND [Due Amount] <> 0 Group By XXX.[Customer Id], XXX.[Document Id]"

            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    '    Public Shared Function GetOutStandingQry(ByVal AgeOfDate As DateTime, ByVal CutOfDate As DateTime, ByVal IsInactiveCustomer As Boolean, ByVal DocTypeList As ArrayList, ByVal IsOnDueDate As String, Optional ByVal strCurrency As String = "", Optional ByVal CustomerList As ArrayList = Nothing, Optional ByVal LocationList As ArrayList = Nothing, Optional ByVal CustomerGroupList As ArrayList = Nothing, Optional ByVal IsParentCustomer As Boolean = False, Optional ByVal ParentCustomerList As ArrayList = Nothing, Optional ByVal Is_Security As String = "", Optional ByVal ISShowCustomerInvoiceorDocNo As Boolean = False) As String
    '        Try

    '            Dim Qry As String = " Select MAX(xxx.Comp_Code) AS Comp_Code, [Customer Id], MAX([Parent Code]) AS [Parent Code],MAX(Parent_Master.Customer_Name) as ParentName, MAX([Customer Name]) AS [Customer Name], MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code) AS Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) AS Cust_Group_Desc, [Document Id], MAX([Desc]) as [Desc]," & _
    '             " SUM([Due Amount]*" & strCurrency & ")  AS [Due Amount]," & _
    '"" & IIf(strCurrency = "1", "MAX(xxx.CURRENCY_CODE)", "'INR'") & "  as Currency,max(xxx.CURRENCY_CODE) As CURRENCY_CODE,MAX(xxx.ConvRate) As ConvRate, MAX([Due Date]) AS [Due Date], MAX(type) AS type, MAX([Document Date]) AS [Document Date], MAX(Ageing_Days) AS Ageing_Days, MAX(Document_Type) AS Document_Type, MAX(Location) AS Location,max(originalAmt) as originalAmt  from ("

    '            '   Qry += " SELECT  TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_Customer_Invoice_Head.Comp_Code,TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
    '            '" TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name]," & _
    '            '" " + IIf(ISShowCustomerInvoiceorDocNo = True, " TSPL_Customer_Invoice_Head.Document_No ", "(case when ISNULL( Against_Sale_No,'')<>'' then Against_Sale_No when ISNULL(Against_Sale_Return_No,'')<>'' then TSPL_Customer_Invoice_Head.Document_No  when ISNULL( AgainstScrap,'')<>'' then AgainstScrap else TSPL_Customer_Invoice_Head.Document_No end)") + "  as [Document Id], " & _
    '            '" TSPL_Customer_Invoice_Head.Description as [Desc], (case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then TSPL_Customer_Invoice_Head.Document_Total Else (TSPL_Customer_Invoice_Head.Document_Total-  ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' ),0) " & _
    '            '"  - ISNULL((Select SUM(TSPL_RECEIPT_HEADER.Receipt_Amount) from TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' and  ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A') AND TSPL_RECEIPT_HEADER.Applied_RECEIPT=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)),0) " & _
    '            '") *-1 end " & _
    '            '" - case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' ),0) else 0 end -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SD_SALE_RETURN_HEAD.Status,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE  left outer join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo   LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No  where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Posted ,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN.Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap =TSPL_SCRAPINVOICE_HEAD.Invoice_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_SCRAPSALE_HEAD_RETURN.ispost,'0') =1),0)) as [Due Amount],TSPL_Customer_Invoice_Head.CURRENCY_CODE, case when isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) <>0 then isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) else TSPL_Customer_Invoice_Head.ConvRate end as ConvRate  ,"

    '            Qry += " SELECT  TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_Customer_Invoice_Head.Comp_Code,TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
    '         " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name]," & _
    '         " " + IIf(ISShowCustomerInvoiceorDocNo = True, " TSPL_Customer_Invoice_Head.Document_No ", "(case when ISNULL( Against_Sale_No,'')<>'' then Against_Sale_No when ISNULL(Against_Sale_Return_No,'')<>'' then TSPL_Customer_Invoice_Head.Document_No  when ISNULL( AgainstScrap,'')<>'' then AgainstScrap else TSPL_Customer_Invoice_Head.Document_No end)") + "  as [Document Id], " & _
    '         " TSPL_Customer_Invoice_Head.Description as [Desc], (case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then TSPL_Customer_Invoice_Head.Document_Total Else (TSPL_Customer_Invoice_Head.Document_Total-  ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  where TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') ),0) " & _
    '         "  - ISNULL((Select SUM(TSPL_RECEIPT_HEADER.Receipt_Amount) from TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' and  ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A') AND TSPL_RECEIPT_HEADER.Applied_RECEIPT=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)),0) " & _
    '         ") *-1 end " & _
    '         " - case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  where TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') ),0) else 0 end -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SD_SALE_RETURN_HEAD.Status,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE  left outer join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo   LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No  where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Posted ,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN.Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap =TSPL_SCRAPINVOICE_HEAD.Invoice_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_SCRAPSALE_HEAD_RETURN.ispost,'0') =1),0)) as [Due Amount],TSPL_Customer_Invoice_Head.CURRENCY_CODE, case when isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) <>0 then isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) else TSPL_Customer_Invoice_Head.ConvRate end as ConvRate  ,"


    '            Qry += "  TSPL_Customer_Invoice_Head.due_date as [Due Date],'' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date], "

    '            If clsCommon.CompairString(IsOnDueDate, "DueDate") = CompairStringResult.Equal Then
    '                Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Due_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
    '            ElseIf clsCommon.CompairString(IsOnDueDate, "DocumentDate") = CompairStringResult.Equal Then
    '                'Qry += " DATEDIFF(dd,convert(date,Invoice_Entry_Date,103),'" & StrAgeOfDate & "') as datedifference, "
    '                ' Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head." + IIf(IsOnDueDate = True, "Due_Date", "Document_Date") + ",103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
    '                Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
    '            End If

    '            Qry += " case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," & _
    '           " TSPL_Customer_Invoice_Head.Loc_Code as Location ,TSPL_Customer_Invoice_Head.Document_Total as originalAmt FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No " & _
    '           " left outer join (  Select  TSPL_REVALUATION_DETAIL.AR_Invoice_No,TSPL_REVALUATION_HEAD.Currency_Rate,TSPL_REVALUATION_HEAD.Document_Date,TSPL_REVALUATION_HEAD.Document_No  " & _
    '           " from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD  on TSPL_REVALUATION_DETAIL.Document_No =TSPL_REVALUATION_HEAD.Document_No and TSPL_REVALUATION_HEAD.Document_No in  (select top 1 h.Document_No  from TSPL_REVALUATION_HEAD as h  left outer join TSPL_REVALUATION_DETAIL d on d.Document_No=h.Document_No" & _
    '           " where d.AR_Invoice_No=TSPL_REVALUATION_DETAIL.AR_Invoice_No and CONVERT(DATE,h.Document_Date,103)<=CONVERT(DATE,'" & AgeOfDate & "',103) and isnull(d.AR_Invoice_No ,'')<>'' order by h.Document_Date desc) where CONVERT(DATE,TSPL_REVALUATION_HEAD .Document_Date,103)<=CONVERT(DATE,'" & AgeOfDate & "',103) and isnull(AR_Invoice_No ,'')<>'')TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD .AR_Invoice_No =TSPL_Customer_Invoice_Head .Document_No where TSPL_Customer_Invoice_Head.Status='1' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
    '           " UNION ALL SELECT ''  as ARINvoiceNo,  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name] ,Document_No as [Document Id]  ,Description as [Desc] ,(Total_Order_Amt)*-1 as [Due Amount] ,'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date]  , DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location,TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt  as originalAmt  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
    '           " UNION ALL select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END, 'INR' AS CURRENCY_CODE, 1 AS ConvRate ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment,isnull(TSPL_Customer_Invoice_Head.Document_Total,0) as originalAmt from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') = TSPL_VCGL_Head.Document_No  where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'  AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''" & _
    '           " UNION ALL select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END, 'INR' AS CURRENCY_CODE, 1 AS ConvRate ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment,isnull(TSPL_Customer_Invoice_Head.Document_Total,0) as originalAmt from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') = TSPL_VCGL_Detail.Document_No  where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL ,'') =''" & _
    '           " UNION ALL select ''  as ARINvoiceNo, TSPL_RECEIPT_HEADER.Comp_Code ,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc , "

    '            ''richa agarwal change on 5 Nov,2019 ERO/07/11/19-001090
    '            ' Qry += " (Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " & _
    '            '" COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " & _
    '            '" AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND APP_REC.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No and " & _
    '            '" CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z))*-1 End),"

    '            Qry += " (Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then " & Environment.NewLine & _
    '" (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " & _
    '        " COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " & _
    '        " AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No and " & _
    '        " CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z)) " & _
    '        " Else (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " & _
    '        " COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " & _
    '        " AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND APP_REC.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No and " & _
    '        " CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z))*-1 End),"




    '            ' Qry += " (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " & _
    '            '" COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " & _
    '            '" AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND APP_REC.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No and " & _
    '            '" CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z)) * CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 1 ELSE  -1 END ,"


    '            Qry += "TSPL_RECEIPT_HEADER.CURRENCY_CODE  AS CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate AS ConvRate ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" & AgeOfDate & "',103)), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location,TSPL_RECEIPT_HEADER.Receipt_Amount  as originalAmt  from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M', 'A','R') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" + Is_Security + "" & _
    '           "  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') " & _
    '            "  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') " & _
    '                        " AND TSPL_RECEIPT_HEADER.Receipt_No NOT IN ( " & Environment.NewLine & _
    '                                  " sELECT Applied_Receipt  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P')" & Environment.NewLine & _
    '                                  " union all" & Environment.NewLine & _
    '                                  " sELECT Receipt_No  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and  CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') )" & Environment.NewLine
    '            '" union all " & Environment.NewLine & _
    '            '" Select distinct TSPL_RECEIPT_DETAIL.Document_No  from TSPL_RECEIPT_DETAIL left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL .Receipt_No where TSPL_RECEIPT_DETAIL.Document_No in (Select ISNULL(Receipt_No ,'') from TSPL_RECEIPT_HEADER where Receipt_Type ='F' and isnull(Applied_Receipt ,'')='') and  CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))"




    '            ''richa agarwal use TSPL_Receipt_Adjustment_Header.ARInvoiceNo in place of Adjustment_no in below line to show ar invoice no BM00000007349
    '            ' Qry += " UNION ALL Select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_Receipt_Adjustment_Header.ARInvoiceNo as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], case when TSPL_Customer_Invoice_Head.Document_Type='C' then 0 else TSPL_Receipt_Adjustment_Header.Adjustment_Amount end *-1 as [Due Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
    '            Qry += " UNION ALL Select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_Receipt_Adjustment_Header.ARInvoiceNo as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else TSPL_Receipt_Adjustment_Header.Adjustment_Amount * -1 end  as [Due Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as Location,TSPL_Customer_Invoice_Head.Document_Total as originalAmt from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
    '                              " UNION ALL SELECT ''  as ARINvoiceNo, TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name],Document_No as [Document Id] ,Description as [Desc] , Empty_Value*-1 AS [Due Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location,TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt as  originalAmt from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
    '                              " UNION ALL SELECT '' as ARINvoiceNo,  TSPL_ADJUSTMENT_HEADER.Comp_Code,TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end,'INR' AS CURRENCY_CODE, 1 AS ConvRate,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code),0 as  originalAmt FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')='' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
    '                          " ) XXX LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON XXX.[Customer Id]=TSPL_CUSTOMER_MASTER.Cust_Code" & _
    '                          " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=XXX.[Parent Code]" & _
    '                          " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code" & _
    '                          " where  XXX.Document_Type in (" + clsCommon.GetMulcallString(DocTypeList) + "  ) " & _
    '                          " and convert(date,XXX.[Document Date] ,103) <= convert(date,'" & CutOfDate & "',103)"
    '            If IsParentCustomer Then
    '                If ParentCustomerList IsNot Nothing AndAlso ParentCustomerList.Count > 0 Then
    '                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
    '                        Qry += " AND ((XXX.[Parent Code] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + ") and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ")) or XXX.[Customer Id] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + "))"
    '                    Else
    '                        Qry += " AND (XXX.[Parent Code] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + ") or XXX.[Customer Id] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + "))"
    '                    End If

    '                Else
    '                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
    '                        Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
    '                    End If
    '                End If
    '            Else
    '                Dim AllowtoSHOWParentChildCustomer As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSHOWParentChildCustomer, clsFixedParameterCode.AllowtoSHOWParentChildCustomer, Nothing)) = 1, True, False))
    '                If AllowtoSHOWParentChildCustomer = True Then
    '                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
    '                        Qry += " and (XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") or XXX.[Parent Code]  in (" + clsCommon.GetMulcallString(CustomerList) + ") ) "
    '                    End If
    '                Else
    '                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
    '                        Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
    '                    End If
    '                End If

    '            End If

    '            'If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
    '            '    Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
    '            'End If
    '            If LocationList IsNot Nothing AndAlso LocationList.Count > 0 Then
    '                Qry += " and XXX.Location in (" + clsCommon.GetMulcallString(LocationList) + ") "
    '            End If
    '            If CustomerGroupList IsNot Nothing AndAlso CustomerGroupList.Count > 0 Then
    '                Qry += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(CustomerGroupList) + ") "
    '            End If
    '            ''richa 12 Dec, 2016
    '            'Qry += " AND XXX.[Document Id]  NOT IN (sELECT TSPL_RECEIPT_DETAIL.Document_No FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER rh  ON TSPL_RECEIPT_DETAIL.Receipt_No  =rh.Receipt_No left outer join TSPL_BANK_REVERSE on isnull(TSPL_BANK_REVERSE.Document_No,'') = rh.Receipt_No  WHERE TSPL_RECEIPT_DETAIL.Document_No=" + Environment.NewLine & _
    '            '" XXX.[Document Id] AND TSPL_RECEIPT_DETAIL.Applied_Amount =case when XXX.[Due Amount]  <0 then XXX.[Due Amount]* -1 else XXX.[Due Amount] end" + Environment.NewLine & _
    '            '" AND XXX.Document_Type IN ('CR','DR') AND rh.Posted='Y' AND ISNULL(TSPL_RECEIPT_DETAIL.Document_No,'')<>'' AND Convert(Date, rh.Receipt_Date , 103) <=convert(date,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Document_No,'')<>rh.Receipt_No )"
    '            Qry += " AND XXX.ARINvoiceNo  NOT IN (sELECT TSPL_RECEIPT_DETAIL.Document_No FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER rh  ON TSPL_RECEIPT_DETAIL.Receipt_No  =rh.Receipt_No left outer join TSPL_BANK_REVERSE on isnull(TSPL_BANK_REVERSE.Document_No,'') = rh.Receipt_No  WHERE TSPL_RECEIPT_DETAIL.Document_No=" + Environment.NewLine & _
    '           " XXX.ARINvoiceNo AND TSPL_RECEIPT_DETAIL.Applied_Amount =case when XXX.[Due Amount]  <0 then XXX.[Due Amount]* -1 else XXX.[Due Amount] end" + Environment.NewLine & _
    '           " AND XXX.Document_Type IN ('CR','DR') AND rh.Posted='Y' AND ISNULL(TSPL_RECEIPT_DETAIL.Document_No,'')<>'' AND Convert(Date, rh.Receipt_Date , 103) <=convert(date,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Document_No,'')<>rh.Receipt_No )"


    '            ''--------
    '            Qry += " Group By XXX.[Customer Id], XXX.[Document Id]"
    '            'Qry += "AND [Due Amount] <> 0 Group By XXX.[Customer Id], XXX.[Document Id]"

    '            Return Qry
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Function

End Class

Public Class clsRouteCustomerSequenceMaster
    Public ROUTE_NO As String = Nothing
    Public DocDate As Date? = Nothing
    Public ArrRouteCustomerSequence As List(Of clsRouteCustomerSequence) = Nothing

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select Cust_Code as [Code] ,Customer_Name as [Customer Name] ,Add1 as [Address1] ,Add2 as [Address2] ,Add3 as [Address3] ,City_Code as [City Code] ,State as [State] ,Country as [Country] ,Phone1 as [Phone1] ,Phone2 as [Phone2] ,Fax as [Fax] ,Email as [Email] ,WebSite as [Website] ,Credit_Limit as [Credit Limit] ,CURRENCY_CODE as [Currency Code] ,Status as [Status] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modified By] ,Modify_Date as [Modified Date] ,Comp_Code as [Company Code]  From TSPL_CUSTOMER_MASTER   "
        str = clsCommon.ShowSelectForm("SECCUSTFIND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Function SaveData(ByVal obj As clsRouteCustomerSequenceMaster, ByVal trans As SqlTransaction) As Boolean ', Optional ByVal import As Boolean = False, Optional ByVal isMakeAbandomentNo As Boolean = False
        clsRouteCustomerSequence.SaveData(obj.ROUTE_NO, obj.ArrRouteCustomerSequence, obj.DocDate, trans)
        Return True
    End Function

    Public Shared Function GetData(ByVal RouteCode As String, Optional ByVal trans As SqlTransaction = Nothing) As clsRouteCustomerSequenceMaster
        Dim obj As New clsRouteCustomerSequenceMaster
        obj.ROUTE_NO = RouteCode
        obj.ArrRouteCustomerSequence = clsRouteCustomerSequence.GetData(RouteCode, trans)
        Return obj
    End Function
End Class

Public Class clsRouteCustomerSequence
#Region "Variables"
    Public TR_CODE As String = Nothing
    Public SNO As Integer = 0
    Public CUSTOMER_CODE As String = Nothing
    Public CUSTOMER_NAME As String = Nothing
    'Public ROUTE_NO As String = Nothing

#End Region


    Public Shared Function SaveData(ByVal RouteCode As String, ByVal Arr As List(Of clsRouteCustomerSequence), ByVal dtDocDate As DateTime, ByVal trans As SqlTransaction) As Boolean

        Dim strqry1 As String = "delete from TSPL_ROUTE_CUSTOMER_SEQUENCE WHERE ROUTE_NO='" + RouteCode + "'"
        clsDBFuncationality.ExecuteNonQuery(strqry1, trans)

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsRouteCustomerSequence In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "CUSTOMER_CODE", obj.CUSTOMER_CODE)
                clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "ROUTE_NO", RouteCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_CUSTOMER_SEQUENCE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal RouteCode As String, ByVal trans As SqlTransaction) As List(Of clsRouteCustomerSequence)
        Dim arr As New List(Of clsRouteCustomerSequence)
        Dim qry As String = "Select TSPL_ROUTE_CUSTOMER_SEQUENCE.TR_Code,TSPL_ROUTE_CUSTOMER_SEQUENCE.ROUTE_NO,TSPL_ROUTE_CUSTOMER_SEQUENCE.SNO,TSPL_ROUTE_CUSTOMER_SEQUENCE.CUSTOMER_CODE,TSPL_CUSTOMER_MASTER.Customer_Name as CUSTOMER_NAME from TSPL_ROUTE_CUSTOMER_SEQUENCE LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ROUTE_CUSTOMER_SEQUENCE.CUSTOMER_CODE Where TSPL_ROUTE_CUSTOMER_SEQUENCE.ROUTE_NO='" + RouteCode + "' order by SNO "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsRouteCustomerSequence()
                obj.TR_CODE = clsCommon.myCstr(dr("TR_CODE"))
                obj.CUSTOMER_CODE = clsCommon.myCstr(dr("CUSTOMER_CODE"))
                obj.CUSTOMER_NAME = clsCommon.myCstr(dr("CUSTOMER_NAME"))
                obj.SNO = clsCommon.myCdbl(dr("SNO"))
                'obj.ROUTE_NO = clsCommon.myCdbl(dr("ROUTE_NO"))
                arr.Add(obj)
            Next
        End If
        Return arr

    End Function


End Class


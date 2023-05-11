Imports System.Data
Imports System.Data.SqlClient
Imports common
'Created By Prabhakar ; Ticket Ref : BM00000009801 '
Public Class clsSecondaryCustomerMasterInfo
#Region "Variable"
    Public Cust_Code As String
    Public Customer_Name As String
    Public Add1 As String
    Public Add2 As String
    Public Add3 As String
    Public Distributor As String
    Public City_Code As String
    Public State As String
    Public Country As String
    Public Phone1 As String
    Public Phone2 As String
    Public Fax As String
    Public Email As String
    Public WebSite As String
    Public Credit_Limit As Double
    Public CURRENCY_CODE As String
    Public Status As String
    Public Created_By As String
    Public Created_Date As Date
    Public Modified_By As String
    Public Modified_Date As Date
    Public Comp_Code As String
    Public Closing_Date As String
    Public Cust_Category_Code As String
    Public Cust_Group_Code As String
    Public Cust_Type_Code As String
    Public Route_No As String
    Public Route_Desc As String
    Public Price_Code As String
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
    Public Channel_Desc As String
    Public OnHold As Char
    Public Remarks1 As String
    Public Remarks2 As String
    Public Additional1 As String
    Public Additional2 As String
    Public Additional3 As String
    Public Salesman_Code As String
    Public Salesman_Desc As String
    Public Visi_Id As String
    Public Visi_Desc As String
    Public OutLet_Commossion As Double
    Public Balance_ToDate As Double
    Public price_CodeNon As String
    Public Credit_Limit_Alert_Type As Char
    Public PIN_Code As String
    Public Cust_DOB As Date = Nothing
    Public Cust_Spouse_DOB As Date = Nothing
    Public Anniversary_Date As Date = Nothing
    Public Gender As String
    Public Occation As String
    Public CST As String
    Public ECC As String
    Public Range As String
    Public Collectorate As String
    Public PAN As String
    Public Division As String
    Public Parent_Customer_No As String
    Public Route_Group As String
    Public Customer_Class As String
    Public Credit_Customer As Char
    Public LastInvoice_No As String
    Public LastInvoice_Date As Date = Nothing
    Public Inter_Branch As Char
    Public TRANSACTION_TYPE As Char
    Public CUSTOMER_TYPE As Char
    Public Agg_Made_Date As Date = Nothing
    Public Agg_Close_Date As Date = Nothing
    Public Parent_Customer_YN As Char
    Public Service_Dealer_Code As String
    Public TDM_Code As String
    Public IsDistributor As Char
    Public Price_Group_Code As String
    Public CSA_Type As Char
    Public Category_Struct_Code As String
    Public TempCreditLimit As Double
    Public TempCreditLimitFrom As Date? = Nothing
    Public TempCreditLimitTo As Date? = Nothing
    Public Alies_Name As String
    Public Zone_Code As String
    Public CheckCreditLimit As Integer
    Public PIN_NO As String
    Public Struct_Code As String
    Public Crate_Opening As Double
    Public Crate_Opening_Date As Date? = Nothing
    Public Franchise_Code As String
    Public Other_For_PAN As Integer
    Public Cust_IntegratedCRM As String
    Public POS_Type As String = Nothing
    Public Registered As Integer = 0
    Public GSTNO As String = Nothing
    Public GSTEntity As String = Nothing
    Public GSTBlank As String = Nothing
    Public GSTDigit As String = Nothing
    Public BusinessType As Char
    Public Arr_CompetitorDetail As List(Of clsCustomerCompetitorDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsSecondaryCustomerMasterInfo, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsSecondaryCustomerMasterInfo, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "Alies_Name", obj.Alies_Name)

            If IsNothing(obj.Agg_Made_Date) <> True Then
                clsCommon.AddColumnsForChange(coll, "Agg_Made_Date", clsCommon.GetPrintDate(obj.Agg_Made_Date, "MM/dd/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Agg_Made_Date", Nothing, True)
            End If

            If IsNothing(obj.Agg_Close_Date) <> True Then
                clsCommon.AddColumnsForChange(coll, "Agg_Close_Date", clsCommon.GetPrintDate(obj.Agg_Close_Date, "MM/dd/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Agg_Close_Date", Nothing, True)
            End If


            clsCommon.AddColumnsForChange(coll, "CUSTOMER_TYPE", obj.CUSTOMER_TYPE)
            ' clsCommon.AddColumnsForChange(coll, "Agg_Made_Date", clsCommon.GetPrintDate(obj.Agg_Made_Date, "MM/dd/yyyy"), True)
            'clsCommon.AddColumnsForChange(coll, "Agg_Close_Date", clsCommon.GetPrintDate(obj.Agg_Close_Date, "MM/dd/yyyy"), True)
            clsCommon.AddColumnsForChange(coll, "Transaction_Type", obj.TRANSACTION_TYPE)
            clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.Add3)
            clsCommon.AddColumnsForChange(coll, "Country", obj.Country)
            clsCommon.AddColumnsForChange(coll, "State", obj.State)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
            clsCommon.AddColumnsForChange(coll, "Phone1", obj.Phone1)
            clsCommon.AddColumnsForChange(coll, "Phone2", obj.Phone2)
            clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
            clsCommon.AddColumnsForChange(coll, "WebSite", obj.WebSite)
            clsCommon.AddColumnsForChange(coll, "Fax", obj.Fax)
            clsCommon.AddColumnsForChange(coll, "PIN_NO", obj.PIN_NO)


            
            clsCommon.AddColumnsForChange(coll, "Cust_Category_Code", obj.Cust_Category_Code, True)

            clsCommon.AddColumnsForChange(coll, "Cust_Type_Code", obj.Cust_Type_Code, True)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No, True)
            'Dim Route_Desc As String = clsDBFuncationality.getSingleValue("Select Route_Desc from TSPL_ROUTE_MASTER Where Route_No='" + obj.Route_No + "'", trans)
            'clsCommon.AddColumnsForChange(coll, "Route_Desc", Route_Desc)





            clsCommon.AddColumnsForChange(coll, "BusinessType", obj.BusinessType, True)

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
            clsCommon.AddColumnsForChange(coll, "Other_For_PAN", obj.Other_For_PAN)
            'Dim Channel_Desc As String = clsDBFuncationality.getSingleValue("Select Channel_Name  from TSPL_CHANNEL_MASTER where Channel_Id='" + obj.Channel_Code + "'", trans)
            'clsCommon.AddColumnsForChange(coll, "Channel_Desc", Channel_Desc)
            'clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            'If clsCommon.CompairString(obj.Status, "Y") = CompairStringResult.Equal Then
            '    clsCommon.AddColumnsForChange(coll, "Closing_Date", obj.Closing_Date)
            'Else
            '    clsCommon.AddColumnsForChange(coll, "Closing_Date", Nothing)
            'End If
            clsCommon.AddColumnsForChange(coll, "OnHold", obj.OnHold)
            clsCommon.AddColumnsForChange(coll, "Remarks1", obj.Remarks1)
            clsCommon.AddColumnsForChange(coll, "Remarks2", obj.Remarks2)
            clsCommon.AddColumnsForChange(coll, "Additional1", obj.Additional1)
            clsCommon.AddColumnsForChange(coll, "Additional2", obj.Additional2)
            clsCommon.AddColumnsForChange(coll, "Additional3", obj.Additional3)
            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
            'Dim Salesman_Desc As String = clsDBFuncationality.getSingleValue("Select Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + obj.Salesman_Code + "'", trans)
            'clsCommon.AddColumnsForChange(coll, "Salesman_Desc", Salesman_Desc)
            'clsCommon.AddColumnsForChange(coll, "OutLet_Commossion", obj.OutLet_Commossion)
            'clsCommon.AddColumnsForChange(coll, "Balance_ToDate", obj.Balance_ToDate)
            clsCommon.AddColumnsForChange(coll, "Credit_Limit", obj.Credit_Limit)
            'clsCommon.AddColumnsForChange(coll, "TempCreditLimit", obj.TempCreditLimit, True)

            clsCommon.AddColumnsForChange(coll, "Route_Group", obj.Route_Group)
            clsCommon.AddColumnsForChange(coll, "Modified_By", obj.Modified_By)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "MM/dd/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
            'clsCommon.AddColumnsForChange(coll, "Route_Group", obj.Route_Group)
            clsCommon.AddColumnsForChange(coll, "CST", obj.CST)
            clsCommon.AddColumnsForChange(coll, "ECC", obj.ECC)
            clsCommon.AddColumnsForChange(coll, "Range", obj.Range)
            clsCommon.AddColumnsForChange(coll, "Collectorate", obj.Collectorate)
            clsCommon.AddColumnsForChange(coll, "PAN", obj.PAN)
            clsCommon.AddColumnsForChange(coll, "Division", obj.Division)
            clsCommon.AddColumnsForChange(coll, "TempCreditLimit", obj.TempCreditLimit, True)
            If obj.TempCreditLimitFrom IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "TempCreditLimitFrom", clsCommon.GetPrintDate(obj.TempCreditLimitFrom, "MM/dd/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "TempCreditLimitFrom", Nothing, True)
            End If
            If obj.TempCreditLimitTo IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "TempCreditLimitTo", clsCommon.GetPrintDate(obj.TempCreditLimitTo, "MM/dd/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "TempCreditLimitTo", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "CheckCreditLimit", obj.CheckCreditLimit)














            clsCommon.AddColumnsForChange(coll, "Parent_Customer_No", obj.Parent_Customer_No)

            clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Code)
            ' ''------------------------
            ''''' clsCommon.AddColumnsForChange(coll, "Customer_Class", obj.Customer_Class)
            clsCommon.AddColumnsForChange(coll, "Credit_Customer", obj.Credit_Customer)
            'clsCommon.AddColumnsForChange(coll, "LastInvoice_No", obj.LastInvoice_No)
            'clsCommon.AddColumnsForChange(coll, "LastInvoice_Date", obj.LastInvoice_Date)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Price_CodeNon", obj.price_CodeNon)
            clsCommon.AddColumnsForChange(coll, "Price_Group_Code", obj.Price_Group_Code)
            'clsCommon.AddColumnsForChange(coll, "Inter_Branch", obj.Inter_Branch)

           
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE)
            'clsCommon.AddColumnsForChange(coll, "Franchise_CODE", obj.Franchise_Code, True)
            'clsCommon.AddColumnsForChange(coll, "TDM_Code", obj.TDM_Code, True)

            clsCommon.AddColumnsForChange(coll, "IsDistributor", obj.IsDistributor)

            clsCommon.AddColumnsForChange(coll, "POS_Type", obj.POS_Type)
            'clsCommon.AddColumnsForChange(coll, "CSA_Type", obj.CSA_Type, True)
            'clsCommon.AddColumnsForChange(coll, "Category_Struct_Code", obj.Category_Struct_Code, True)

            'clsCommon.AddColumnsForChange(coll, "Cust_IntegratedCRM", obj.TempCreditLimitFrom, True)

            clsCommon.AddColumnsForChange(coll, "Registered", obj.Registered)
            clsCommon.AddColumnsForChange(coll, "GSTNO", obj.GSTNO)
            clsCommon.AddColumnsForChange(coll, "GSTEntity", obj.GSTEntity)
            clsCommon.AddColumnsForChange(coll, "GSTBlank", obj.GSTBlank)
            clsCommon.AddColumnsForChange(coll, "GSTDigit", obj.GSTDigit)


            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" & obj.Cust_Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Cust_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.SecondaryCustomerMaster, "", "")
                        If clsCommon.myLen(obj.Cust_Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "MM/dd/yyyy"))


                Dim qry As String = "SELECT Count(*) FROM TSPL_SECONDARY_CUSTOMER_MASTER  where Cust_Code= '" & obj.Cust_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                If check = 0 Then

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_CUSTOMER_MASTER", OMInsertOrUpdate.Insert, "", trans)

                Else
                    Throw New Exception("This Customer Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_CUSTOMER_MASTER", OMInsertOrUpdate.Update, "Cust_Code='" + obj.Cust_Code + "' ", trans)
            End If
            isSaved = isSaved AndAlso clsCustomerCompetitorDetail.SaveData(obj.Cust_Code, obj.Arr_CompetitorDetail, trans)
            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Cust_Code, "TSPL_SECONDARY_CUSTOMER_MASTER", "Cust_Code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSecondaryCustomerMasterInfo
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSecondaryCustomerMasterInfo
        Dim obj As clsSecondaryCustomerMasterInfo = Nothing
        Dim objCompetitor As clsCustomerCompetitorDetail = Nothing
        Dim dtCompetitor As DataTable
        Dim qry As String = "SELECT [Cust_Code] ,[Customer_Name],[Add1],[Add2],[Add3],[Distributor],[City_Code],[State],[Country] ,[Phone1],[Phone2],[Fax],[Email],[WebSite],[Credit_Limit] ,[CURRENCY_CODE] ,[Status],[Created_By],[Created_Date],[Modified_By],[Modified_Date] ,[Comp_Code],[Closing_Date],[Cust_Category_Code],[Cust_Group_Code] ,[Cust_Type_Code],[Route_No],[Route_Desc],[Price_Code],[Contact_Person_Name],[Contact_Person_Phone],[Contact_Person_Fax],[Contact_Person_Website] ,[Contact_Person_Email],[Terms_Code],[Cust_Account],[Tax_Group],[TAX1],[TAX1_Rate],[TAX2],[TAX2_Rate],[TAX3],[TAX3_Rate],[TAX4] ,[TAX4_Rate],[TAX5] ,[TAX5_Rate] ,[TAX6],[TAX6_Rate],[TAX7],[TAX7_Rate],[TAX8],[TAX8_Rate],[TAX9],[TAX9_Rate],[TAX10],[TAX10_Rate],[Payment_Code],[Service_Tax_No],[Tin_No],[Lst_No],[Form_Type] ,[Channel_Code],[Channel_Desc],[OnHold],[Remarks1],[Remarks2],[Additional1],[Additional2],[Additional3],[Salesman_Code],[Salesman_Desc],[Visi_Id],[Visi_Desc] ,[OutLet_Commossion],[Balance_ToDate],[price_CodeNon],[Credit_Limit_Alert_Type],[PIN_Code],[Cust_DOB],[Cust_Spouse_DOB],[Anniversary_Date],[Gender],[Occation],[CST],[ECC],[Range] ,[Collectorate] ,[PAN],[Division],[Parent_Customer_No],[Route_Group],[Customer_Class],[Credit_Customer],[LastInvoice_No],[LastInvoice_Date] ,[Inter_Branch],[TRANSACTION_TYPE],[CUSTOMER_TYPE],[Agg_Made_Date],[Agg_Close_Date],[Parent_Customer_YN],[Service_Dealer_Code],[TDM_Code],[IsDistributor],[Price_Group_Code],[CSA_Type],[Category_Struct_Code],[TempCreditLimit],[TempCreditLimitFrom],[TempCreditLimitTo],[Alies_Name],[Zone_Code],[CheckCreditLimit],[PIN_NO],[Struct_Code],[Crate_Opening],[Crate_Opening_Date],[Franchise_Code],[Other_For_PAN],[Cust_IntegratedCRM],[POS_Type],Registered,GSTNO,GSTEntity,GSTBlank,GSTDigit,[BusinessType]  FROM TSPL_SECONDARY_CUSTOMER_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Cust_Code =  (select MIN(Cust_Code) from TSPL_SECONDARY_CUSTOMER_MASTER)"
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

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSecondaryCustomerMasterInfo()

            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Alies_Name = clsCommon.myCstr(dt.Rows(0)("Alies_Name"))
            If IsDBNull(dt.Rows(0)("Agg_Made_Date")) = False AndAlso dt.Rows(0)("Agg_Made_Date") <> Nothing Then
                obj.Agg_Made_Date = clsCommon.GetPrintDate(dt.Rows(0)("Agg_Made_Date"), "dd/MM/yyyy")
            End If
            If IsDBNull(dt.Rows(0)("Agg_Close_Date")) = False AndAlso dt.Rows(0)("Agg_Close_Date") <> Nothing Then
                obj.Agg_Close_Date = clsCommon.GetPrintDate(dt.Rows(0)("Agg_Close_Date"), "dd/MM/yyyy")
            End If
            obj.TRANSACTION_TYPE = clsCommon.myCstr(dt.Rows(0)("TRANSACTION_TYPE"))
            obj.CUSTOMER_TYPE = clsCommon.myCstr(dt.Rows(0)("CUSTOMER_TYPE"))
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.Cust_Group_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Group_Code"))
            obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
            obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
            obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
            obj.Country = clsCommon.myCstr(dt.Rows(0)("Country"))
            obj.State = clsCommon.myCstr(dt.Rows(0)("State"))
            obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
            obj.Phone1 = clsCommon.myCstr(dt.Rows(0)("Phone1"))
            obj.Phone2 = clsCommon.myCstr(dt.Rows(0)("Phone2"))
            obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
            obj.WebSite = clsCommon.myCstr(dt.Rows(0)("WebSite"))
            obj.Fax = clsCommon.myCstr(dt.Rows(0)("Fax"))
            obj.PIN_NO = clsCommon.myCstr(dt.Rows(0)("PIN_NO"))
            obj.Parent_Customer_No = clsCommon.myCstr(dt.Rows(0)("Parent_Customer_No"))
            'Contact Person'
            obj.Contact_Person_Name = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Name"))
            obj.Contact_Person_Phone = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Phone"))
            obj.Contact_Person_Fax = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Fax"))
            obj.Contact_Person_Website = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Website"))
            obj.Contact_Person_Email = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Email"))
            'Process'
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Cust_Account = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Service_Tax_No = clsCommon.myCstr(dt.Rows(0)("Service_Tax_No"))
            obj.Lst_No = clsCommon.myCstr(dt.Rows(0)("Lst_No"))
            obj.Credit_Limit = clsCommon.myCdbl(dt.Rows(0)("Credit_Limit"))
            obj.Form_Type = clsCommon.myCstr(dt.Rows(0)("Form_Type"))
            obj.BusinessType = clsCommon.myCstr(dt.Rows(0)("BusinessType"))
            obj.TempCreditLimit = clsCommon.myCdbl(dt.Rows(0)("TempCreditLimit"))

            If IsDBNull(dt.Rows(0)("TempCreditLimitFrom")) = False AndAlso dt.Rows(0)("TempCreditLimitFrom") <> Nothing Then
                obj.TempCreditLimitFrom = clsCommon.GetPrintDate(dt.Rows(0)("TempCreditLimitFrom"), "dd/MM/yyyy")
            End If
            'obj.TempCreditLimitFrom = clsCommon.myCstr(dt.Rows(0)("TempCreditLimitFrom"))
            If IsDBNull(dt.Rows(0)("TempCreditLimitTo")) = False AndAlso dt.Rows(0)("TempCreditLimitTo") <> Nothing Then
                obj.TempCreditLimitTo = clsCommon.GetPrintDate(dt.Rows(0)("TempCreditLimitTo"), "dd/MM/yyyy")
            End If
            'obj.TempCreditLimitTo = clsCommon.myCstr(dt.Rows(0)("TempCreditLimitTo"))
            obj.Form_Type = clsCommon.myCstr(dt.Rows(0)("Form_Type"))
            obj.TempCreditLimit = clsCommon.myCdbl(dt.Rows(0)("TempCreditLimit"))

            'obj.TempCreditLimitFrom = clsCommon.myCstr(dt.Rows(0)("TempCreditLimitFrom"))
            obj.CheckCreditLimit = clsCommon.myCdbl(dt.Rows(0)("CheckCreditLimit"))
            obj.CST = clsCommon.myCstr(dt.Rows(0)("CST"))
            obj.ECC = clsCommon.myCstr(dt.Rows(0)("ECC"))
            obj.Range = clsCommon.myCstr(dt.Rows(0)("Range"))
            obj.Collectorate = clsCommon.myCstr(dt.Rows(0)("Collectorate"))
            obj.PAN = clsCommon.myCstr(dt.Rows(0)("PAN"))
            obj.Division = clsCommon.myCstr(dt.Rows(0)("Division"))
            obj.Tin_No = clsCommon.myCstr(dt.Rows(0)("Tin_No"))
            obj.Other_For_PAN = clsCommon.myCstr(dt.Rows(0)("Other_For_PAN"))
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

            obj.Cust_Category_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Category_Code"))
            obj.Cust_Type_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Type_Code"))
            obj.POS_Type = clsCommon.myCstr(dt.Rows(0)("POS_Type"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Zone_Code = clsCommon.myCstr(dt.Rows(0)("Zone_Code"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Price_Group_Code = clsCommon.myCstr(dt.Rows(0)("Price_Group_Code"))
            obj.price_CodeNon = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
            obj.Channel_Code = clsCommon.myCstr(dt.Rows(0)("Channel_Code"))
            obj.Route_Group = clsCommon.myCstr(dt.Rows(0)("Route_Group"))
            obj.Remarks1 = clsCommon.myCstr(dt.Rows(0)("Remarks1"))
            obj.Remarks2 = clsCommon.myCstr(dt.Rows(0)("Remarks2"))
            obj.Additional1 = clsCommon.myCstr(dt.Rows(0)("Additional1"))
            obj.Additional2 = clsCommon.myCstr(dt.Rows(0)("Additional2"))
            obj.Additional3 = clsCommon.myCstr(dt.Rows(0)("Additional3"))
            'Modify 06 oct 2016 By Prabhakar'
            obj.OnHold = clsCommon.myCstr(dt.Rows(0)("OnHold"))









            obj.Distributor = clsCommon.myCstr(dt.Rows(0)("Distributor"))
            obj.Credit_Limit = clsCommon.myCdbl(dt.Rows(0)("Credit_Limit"))
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            If IsDBNull(dt.Rows(0)("Created_Date")) = False AndAlso dt.Rows(0)("Created_Date") <> Nothing Then
                obj.Created_Date = clsCommon.GetPrintDate(dt.Rows(0)("Created_Date"), "dd/MM/yyyy")
            End If

            obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
            If IsDBNull(dt.Rows(0)("Modified_Date")) = False AndAlso dt.Rows(0)("Modified_Date") <> Nothing Then
                obj.Created_Date = clsCommon.GetPrintDate(dt.Rows(0)("Modified_Date"), "dd/MM/yyyy")
            End If

            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            If IsDBNull(dt.Rows(0)("Closing_Date")) = False AndAlso dt.Rows(0)("Closing_Date") <> Nothing Then
                obj.Created_Date = clsCommon.GetPrintDate(dt.Rows(0)("Closing_Date"), "dd/MM/yyyy")
            End If
            obj.Cust_Category_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Category_Code"))
            obj.City_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Type_Code")) '06 oct' 

            obj.Registered = clsCommon.myCdbl(dt.Rows(0)("Registered"))
            obj.GSTEntity = clsCommon.myCstr(dt.Rows(0)("GSTEntity"))
            obj.GSTBlank = clsCommon.myCstr(dt.Rows(0)("GSTBlank"))
            obj.GSTDigit = clsCommon.myCstr(dt.Rows(0)("GSTDigit"))
            obj.GSTNO = clsCommon.myCstr(dt.Rows(0)("GSTNO"))


            qry = "SELECT TSPL_TRADER_COMPETITOR_DETAIL.* FROM TSPL_TRADER_COMPETITOR_DETAIL  where TSPL_TRADER_COMPETITOR_DETAIL.cust_Code='" + obj.Cust_Code + "' order by Line_No "
            dtCompetitor = New DataTable()
            dtCompetitor = clsDBFuncationality.GetDataTable(qry, trans)
            If (dtCompetitor IsNot Nothing AndAlso dtCompetitor.Rows.Count > 0) Then
                obj.Arr_CompetitorDetail = New List(Of clsCustomerCompetitorDetail)
                Dim objTr As clsCustomerCompetitorDetail
                For Each dr As DataRow In dtCompetitor.Rows
                    objTr = New clsCustomerCompetitorDetail
                    objTr.Line_No = dr("Line_No")
                    objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Competitor_Code = clsCommon.myCstr(dr("Competitor_Code"))
                    objTr.Milk = clsCommon.myCdbl(dr("Milk"))
                    objTr.Curd = clsCommon.myCdbl(dr("Curd"))
                    objTr.Others = clsCommon.myCdbl(dr("Others"))
                    obj.Arr_CompetitorDetail.Add(objTr)
                Next
            End If

        End If
        Return obj

    End Function

   
    Public Shared Function GetVendorNextCode(ByVal TableName As String, ByVal FieldName As String, ByVal StrVenName As String, ByVal trans As SqlTransaction) As String

        If clsCommon.myLen(StrVenName) <= 0 Then
            Throw New Exception("Please enter Description")
        End If
        StrVenName = StrVenName.Substring(0, 1)
        Dim qry As String = ""
        Dim DigitLen As String = ""
        Dim Digits As Double
        Dim strRetCode As String = ""

        Dim strLocatinSegmentCode As String = ""
        ' Dim dt As DataTable
        If clsCommon.myLen(StrVenName) > 0 Then
            ' Dim dt1 As DataTable
            Dim qry1 As String = "Select COUNT(*) AS Row From " + TableName + "  Where " + FieldName & " like '" + StrVenName + "%'"
            Dim VNameSeries As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1, trans))
            If clsCommon.CompairString(TableName, "TSPL_VENDOR_MASTER") = CompairStringResult.Equal Then
                Digits = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedDigitsForVendor, clsFixedParameterCode.AutoGeneratedDigitsForVendor, trans))
            ElseIf clsCommon.CompairString(TableName, "TSPL_CUSTOMER_MASTER") = CompairStringResult.Equal Then
                Digits = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedDigitsForCustomer, clsFixedParameterCode.AutoGeneratedDigitsForCustomer, trans))
            ElseIf clsCommon.CompairString(TableName, "TSPL_SECONDARY_CUSTOMER_MASTER") = CompairStringResult.Equal Then
                Digits = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedDigitsForCustomer, clsFixedParameterCode.AutoGeneratedDigitsForCustomer, trans))
            End If

            Digits -= clsCommon.myLen(VNameSeries)
            If clsCommon.myLen(Digits) > 0 Then
                For dig As Integer = 1 To Digits
                    DigitLen += "0"
                Next
            End If

            If VNameSeries = 0 Then
                VNameSeries = 1
            Else
                VNameSeries = 1 + VNameSeries
            End If

            strRetCode = StrVenName.ToUpper() + DigitLen + clsCommon.myCstr(VNameSeries)
            Dim dt As DataTable = Nothing
            Dim blCondition As Boolean = True
            While blCondition
                If clsCommon.CompairString(TableName, "TSPL_VENDOR_MASTER") = CompairStringResult.Equal Then
                    dt = clsDBFuncationality.GetDataTable("Select 1  From tspl_vendor_master where vendor_code='" + strRetCode + "'", trans)
                ElseIf clsCommon.CompairString(TableName, "TSPL_CUSTOMER_MASTER") = CompairStringResult.Equal Then
                    dt = clsDBFuncationality.GetDataTable("Select 1  From TSPL_CUSTOMER_MASTER where Cust_Code='" + strRetCode + "'", trans)
                ElseIf clsCommon.CompairString(TableName, "TSPL_SECONDARY_CUSTOMER_MASTER") = CompairStringResult.Equal Then
                    dt = clsDBFuncationality.GetDataTable("Select 1  From TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + strRetCode + "'", trans)
                End If

                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    blCondition = False
                Else
                    blCondition = True
                    strRetCode = clsCommon.incval(strRetCode)
                End If
            End While

        End If
        Return strRetCode
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim obj As clsSecondaryCustomerMasterInfo = clsSecondaryCustomerMasterInfo.GetData(strCode, NavigatorType.Current)

            Dim qry As String

            qry = "delete from TSPL_TRADER_COMPETITOR_DETAIL where Cust_Code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "delete from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetLedgerSummaryDt(ByVal From_Date As Date, ByVal To_Date As Date, ByVal lstCNF As ArrayList, ByVal lstDistr As ArrayList, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = GetLedgerSummaryQry(From_Date, To_Date, lstCNF, lstDistr)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetLedgerDetailDt(ByVal From_Date As Date, ByVal To_Date As Date, ByVal lstCNF As ArrayList, ByVal lstDistr As ArrayList, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = GetLedgerDetailQry(From_Date, To_Date, lstCNF, lstDistr)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Public Shared Function GetLedgerSummaryQry(ByVal From_Date As Date, ByVal To_Date As Date, ByVal lstCNF As ArrayList, ByVal lstDistr As ArrayList) As String
        Dim BaseQry As String = ""
        Dim Qry As String = ""
        BaseQry = GetLedgerBaseQry(lstCNF, lstDistr)

        Qry = " select Distributor_Code,Customer_Name,CNF_Code,CNF_Name,sum(Opening) as Opening,sum(Debit) as Debit,sum(Credit) as Credit,sum(Opening+Debit-Credit) as Closing from (" & _
              " select Opening.Distributor_Code,Distr.Customer_Name,Distr.Parent_Customer_No as CNF_Code,Cust.Customer_Name as CNF_Name,Null as Trans_Type,null as Doc_No," & _
              " Null as Doc_Date,'Opening' as Remarks,sum(Opening.Debit-Opening.Credit) as Opening,0 as Debit,0 as Credit,sum(Opening.Debit-Opening.Credit) as Balance " & _
              " from (" & BaseQry & ") as Opening " & _
              " left join TSPL_SECONDARY_CUSTOMER_MASTER Distr on Opening.Distributor_Code=Distr.Cust_Code " & _
              " left join TSPL_CUSTOMER_MASTER Cust on Distr.Parent_Customer_No=Cust.Cust_Code " & _
              " where Opening.Document_Date <'" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' " & _
              " group by Opening.Distributor_Code,Distr.Customer_Name,Distr.Parent_Customer_No,Cust.Customer_Name " & _
              " union all " & _
              " select Trans.Distributor_Code,Distr.Customer_Name,Distr.Parent_Customer_No as CNF_Code,Cust.Customer_Name as CNF_Name,Trans.Trans_Type,Trans.Document_Code as Doc_No, " & _
              " Trans.Document_Date as Doc_Date,Trans.Remarks,0 as Opening,Trans.Debit,Trans.Credit,(Trans.Debit-Trans.Credit) as Balance " & _
              " from (" & BaseQry & ") as Trans " & _
              " left join TSPL_SECONDARY_CUSTOMER_MASTER Distr on Trans.Distributor_Code=Distr.Cust_Code " & _
              " left join TSPL_CUSTOMER_MASTER Cust on Distr.Parent_Customer_No=Cust.Cust_Code " & _
              " where Trans.Document_Date >= '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' and Trans.Document_Date<= '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "' " & _
              " ) as Final where 2=2 group by Distributor_Code,Customer_Name,CNF_Code,CNF_Name"

        Return Qry
    End Function
    Public Shared Function GetLedgerDetailQry(ByVal From_Date As Date, ByVal To_Date As Date, ByVal lstCNF As ArrayList, ByVal lstDistr As ArrayList) As String
        Dim BaseQry As String = ""
        Dim Qry As String = ""
        BaseQry = GetLedgerBaseQry(lstCNF, lstDistr)

        Qry = " select *,row_number() over (partition by Distributor_Code order by Distributor_Code,Doc_Date) as Tr_id from (" & _
              " select Opening.Distributor_Code,Distr.Customer_Name,Distr.Parent_Customer_No as CNF_Code,Distr.Customer_Name as CNF_Name,Null as Trans_Type,null as Doc_No," & _
              " Null as Doc_Date,'Opening' as Remarks,sum(Opening.Debit-Opening.Credit) as Debit,0 as Credit,sum(Opening.Debit-Opening.Credit) as Balance " & _
              " from (" & BaseQry & ") as Opening " & _
              " left join TSPL_SECONDARY_CUSTOMER_MASTER Distr on Opening.Distributor_Code=Distr.Cust_Code " & _
              " left join TSPL_CUSTOMER_MASTER Cust on Distr.Parent_Customer_No=Cust.Cust_Code " & _
              " where Opening.Document_Date <'" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' " & _
              " group by Opening.Distributor_Code,Distr.Customer_Name,Distr.Parent_Customer_No,Distr.Customer_Name " & _
              " union all " & _
              " select Trans.Distributor_Code,Distr.Customer_Name,Distr.Parent_Customer_No as CNF_Code,Cust.Customer_Name as CNF_Name,Trans.Trans_Type,Trans.Document_Code as Doc_No, " & _
              " Trans.Document_Date as Doc_Date,Trans.Remarks,Trans.Debit,Trans.Credit,(Trans.Debit-Trans.Credit) as Balance " & _
              " from (" & BaseQry & ") as Trans " & _
              " left join TSPL_SECONDARY_CUSTOMER_MASTER Distr on Trans.Distributor_Code=Distr.Cust_Code " & _
              " left join TSPL_CUSTOMER_MASTER Cust on Distr.Parent_Customer_No=Cust.Cust_Code " & _
              " where Trans.Document_Date >= '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' and Trans.Document_Date<= '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "' " & _
              " ) as Final where 2=2 "
        Qry = "select *,sum(outermost.Balance) over (partition by outermost.Distributor_Code order by outermost.Distributor_Code,outermost.Tr_Id) as RunningBalance from (" & Qry & ") as Outermost"
        Return Qry
    End Function
    Public Shared Function GetLedgerBaseQry(ByVal lstCNF As ArrayList, ByVal lstDistr As ArrayList) As String
        Dim Qry As String = ""
        Dim CondDisp As String = ""
        Dim CondRec As String = ""
        If Not lstCNF Is Nothing AndAlso lstCNF.Count > 0 Then
            CondDisp = CondDisp & " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" & clsCommon.GetMulcallString(lstCNF) & ")"
            CondRec = CondRec & " and TSPL_RECEIPT_HEADER.Cust_Code in (" & clsCommon.GetMulcallString(lstCNF) & ")"
        End If
        If Not lstDistr Is Nothing AndAlso lstDistr.Count > 0 Then
            CondDisp = CondDisp & " and TSPL_BOOKING_MATSER.Distributor_Code in (" & clsCommon.GetMulcallString(lstDistr) & ")"
            CondRec = CondRec & "and TSPL_RECEIPT_HEADER.distr_Code in (" & clsCommon.GetMulcallString(lstDistr) & ")"
        End If
        ''+coalesce(tspl_item_master.CNF_Commission,0)
        ''" and TSPL_SD_SHIPMENT_DETAIL.Line_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No " & _
        Qry = " select 'Invoice' as Trans_Type,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code," &
              " TSPL_BOOKING_MATSER.Distributor_Code,TSPL_SD_SALE_INVOICE_HEAD.Description as Remarks,sum(case when TSPL_SD_SALE_INVOICE_DETAIL.SCHEME_ITEM='N' then TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt else 0 end) as Debit,0 as Credit " &
              " from TSPL_SD_SALE_INVOICE_HEAD " &
              " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " &
              " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No " &
              " Left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Line_No=TSPL_SD_SALE_INVOICE_DETAIL.Line_No " &
              " left join TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " &
              " and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code " &
              "  and TSPL_SD_SHIPMENT_DETAIL.Unit_code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code " &
              " left join TSPL_BOOKING_MATSER on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No=TSPL_BOOKING_MATSER.Document_No " &
              " left join TSPL_Dispatch_Distributor on TSPL_BOOKING_MATSER.Document_No=TSPL_Dispatch_Distributor.Booking_No and TSPL_Dispatch_Distributor.Document_Type= 'CD_To_Dis' " &
              " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code " &
              " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code " &
              " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " &
              " LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan  from TSPL_SECONDARY_CUSTOMER_MASTER as TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
              " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE  " &
              " ) p_cust on p_cust.P_cust_code=TSPL_BOOKING_MATSER.Distributor_Code " &
              " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " &
              " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
              " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1  " &
              " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2 " &
              " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  " &
              " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4 " &
              " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  " &
              " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6 " &
              " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 " &
              " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " &
              " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " &
              " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD.TAX10 " &
              " left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " &
              " left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM " &
              " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
              " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
              " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
              " left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " &
              " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state " &
              " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads " &
              " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state " &
              " where 2=2   " & CondDisp & " " &
              " group by TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_BOOKING_MATSER.Distributor_Code,TSPL_SD_SALE_INVOICE_HEAD.Description " &
              " union all " &
              " select 'Receipt' as Trans_Type,Receipt_No,convert(date,Receipt_Date,103) as Receipt_Date,cust_Code,distr_Code,TSPL_RECEIPT_HEADER.Entry_Desc as Remarks,0 as Debit ,Receipt_Amount as Credit from TSPL_RECEIPT_HEADER " &
              " where len(coalesce(distr_Code,''))>0 " & CondRec & ""

        'Qry = "select *, row_number() over (partition by Distributor_Code order by Distributor_Code,Document_Date) as Tr_id from (" & Qry & ") Fin"
        ''cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as date) between '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") & "'
        ''and TSPL_RECEIPT_HEADER.Receipt_Date between '" & clsCommon.GetPrintDate(From_Date, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(To_Date, "dd/MMM/yyyy") &
        Return Qry
    End Function

End Class


Public Class clsCustomerCompetitorDetail
    Public Line_No As Integer = 0
    Public Cust_Code As String = Nothing
    Public Competitor_Code As String = Nothing
    Public Milk As Decimal = 0
    Public Curd As Decimal = 0
    Public Others As Decimal = 0


    Public Shared Function SaveData(ByVal Cust_Code As String, ByVal Arr As List(Of clsCustomerCompetitorDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim qry = "Delete from TSPL_TRADER_COMPETITOR_DETAIL where cust_code='" & Cust_Code & "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerCompetitorDetail In Arr
                Try
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", Cust_Code)
                    clsCommon.AddColumnsForChange(coll, "Competitor_Code", obj.Competitor_Code)
                    clsCommon.AddColumnsForChange(coll, "Milk", obj.Milk)
                    clsCommon.AddColumnsForChange(coll, "Curd", obj.Curd)
                    clsCommon.AddColumnsForChange(coll, "Others", obj.Others)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRADER_COMPETITOR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Next
        End If
        Return True
    End Function
End Class
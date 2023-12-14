Imports System.Data.SqlClient
Imports common


Public Class ClsSupplierRegistration
#Region "Variables"

    Public Registration_No As String = Nothing
    Public Registration_Date As String = Nothing
    Public Supplier_Name As String = Nothing
    Public Supplier_Address As String = Nothing
    Public Supplier_Address2 As String = Nothing
    Public Product As String = Nothing
    Public Category As String = Nothing
    Public Email As String = Nothing
    Public Phone_No_Work As String = Nothing
    Public Fax_No_Work As String = Nothing
    Public Working_Hrs_Work As String = Nothing
    Public Weekly_Holiday_Work As String = Nothing

    Public Phone_No_HO As String = Nothing
    Public Fax_No_HO As String = Nothing
    Public Working_Hrs_HO As String = Nothing
    Public Weekly_Holiday_HO As String = Nothing

    Public Name_DWH As String = Nothing
    Public Desgination_DWH As String = Nothing
    Public Phone_No_DWH As String = Nothing
    Public Name_AWH As String = Nothing
    Public Designation_AWH As String = Nothing
    Public Phone_No_AWH As String = Nothing
    Public Nature_Of_Industry As String = Nothing

    Public Year_Of_Establishment As String = Nothing
    Public Turn_Over As String = Nothing
    Public State_Sales_Tax_No As String = Nothing
    Public State_Sales_Tax_Date As String = Nothing
    Public Centra_Excise_Regn_No As String = Nothing
    Public Centra_Excise_Regn_Date As String = Nothing
    Public ECC_No As String = Nothing
    Public SSI_Regn_No As String = Nothing
    Public SSI_Regn_No_Date As String = Nothing
    Public Is_Certified As String = Nothing
    Public System_Certification As String = Nothing
    Public Other_Certification As String = Nothing
    Public Production_Exec As String = Nothing
    Public Production_Skilled As String = Nothing
    Public Production_UnSkilled As String = Nothing
    Public Production_Total As String = Nothing
    Public QC_Exec As String = Nothing
    Public QC_Skilled As String = Nothing
    Public QC_UnSkilled As String = Nothing
    Public QC_Total As String = Nothing
    Public Total_Room_Exec As String = Nothing
    Public Total_Room_Skilled As String = Nothing
    Public Total_Room_UnSkilled As String = Nothing
    Public Total_Room_Total As String = Nothing
    Public DE_Exec As String = Nothing
    Public DE_Skilled As String = Nothing
    Public DE_UnSkilled As String = Nothing
    Public DE_Total As String = Nothing
    Public Others_Exec As String = Nothing
    Public Others_Skilled As String = Nothing
    Public Others_UnSkilled As String = Nothing
    Public Others_Total As String = Nothing
    Public Customer_1 As String = Nothing
    Public Customer_2 As String = Nothing
    Public Customer_3 As String = Nothing
    Public Customer_4 As String = Nothing
    Public Installed_Power_Capacity As String = Nothing
    Public Own_Power_Generation_Capacity As String = Nothing
    Public Terms_Code As String = Nothing
    Public Inspection_Facilities As String = Nothing
    Public Material_Testing_Facilities As String = Nothing
    Public Capacity As String = Nothing
    Public Process_Capability As String = Nothing
    Public Minimum_Batch_Size As String = Nothing
    Public Customer_Complaint As String = Nothing
    Public Internal_Rejection As String = Nothing
    Public Is_MSDS As String = Nothing
    Public Customer_Objection As String = Nothing
    Public Comments As String = Nothing
    Public Approval_For_Pilot As String = Nothing
    Public Approval_For_Pilot_Date As String = Nothing
    Public Quantity As Integer = 0
    Public QA_Department As String = Nothing
    Public Regular_Lot_Status As String = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region

    Public Shared Function SaveData(ByVal arr As List(Of ClsSupplierRegistration)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            If ClsSupplierRegistration.SaveData(arr, trans, Nothing) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsSupplierRegistration), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsSupplierRegistration In arr
                Dim coll As New Hashtable()
                ' clsCommon.AddColumnsForChange(coll, "Registration_No", obj.Registration_No)
                clsCommon.AddColumnsForChange(coll, "Registration_Date", clsCommon.GetPrintDate(obj.Registration_Date, "dd/MMM/yyyy hh:mm tt"), True)
                clsCommon.AddColumnsForChange(coll, "Supplier_Name", obj.Supplier_Name, True)
                clsCommon.AddColumnsForChange(coll, "Supplier_Address", obj.Supplier_Address, True)
                clsCommon.AddColumnsForChange(coll, "Supplier_Address2", obj.Supplier_Address2, True)
                clsCommon.AddColumnsForChange(coll, "Product", obj.Product, True)
                clsCommon.AddColumnsForChange(coll, "Category", obj.Category, True)
                clsCommon.AddColumnsForChange(coll, "Email", obj.Email, True)
                clsCommon.AddColumnsForChange(coll, "Phone_No_Work", obj.Phone_No_Work, True)
                clsCommon.AddColumnsForChange(coll, "Fax_No_Work", obj.Fax_No_Work, True)
                clsCommon.AddColumnsForChange(coll, "Working_Hrs_Work", obj.Working_Hrs_Work, True)
                clsCommon.AddColumnsForChange(coll, "Weekly_Holiday_Work", obj.Weekly_Holiday_Work, True)
                clsCommon.AddColumnsForChange(coll, "Phone_No_HO", obj.Phone_No_HO, True)
                clsCommon.AddColumnsForChange(coll, "Fax_No_HO", obj.Fax_No_HO, True)
                clsCommon.AddColumnsForChange(coll, "Working_Hrs_HO", obj.Working_Hrs_HO, True)
                clsCommon.AddColumnsForChange(coll, "Weekly_Holiday_HO", obj.Weekly_Holiday_HO, True)
                clsCommon.AddColumnsForChange(coll, "Name_DWH", obj.Name_DWH, True)
                clsCommon.AddColumnsForChange(coll, "Desgination_DWH", obj.Desgination_DWH, True)
                clsCommon.AddColumnsForChange(coll, "Phone_No_DWH", obj.Phone_No_DWH, True)
                clsCommon.AddColumnsForChange(coll, "Name_AWH", obj.Name_AWH, True)
                clsCommon.AddColumnsForChange(coll, "Designation_AWH", obj.Designation_AWH, True)
                clsCommon.AddColumnsForChange(coll, "Phone_No_AWH", obj.Phone_No_AWH, True)
                clsCommon.AddColumnsForChange(coll, "Nature_Of_Industry", obj.Nature_Of_Industry, True)
                clsCommon.AddColumnsForChange(coll, "Year_Of_Establishment", obj.Year_Of_Establishment, True)
                clsCommon.AddColumnsForChange(coll, "Turn_Over", obj.Turn_Over, True)
                clsCommon.AddColumnsForChange(coll, "State_Sales_Tax_No", obj.State_Sales_Tax_No, True)
                clsCommon.AddColumnsForChange(coll, "State_Sales_Tax_Date", clsCommon.GetPrintDate(obj.State_Sales_Tax_Date, "dd/MMM/yyyy hh:mm tt"), True)
                clsCommon.AddColumnsForChange(coll, "Centra_Excise_Regn_No", obj.Centra_Excise_Regn_No, True)
                clsCommon.AddColumnsForChange(coll, "Centra_Excise_Regn_Date", clsCommon.GetPrintDate(obj.Centra_Excise_Regn_Date, "dd/MMM/yyyy hh:mm tt"), True)
                clsCommon.AddColumnsForChange(coll, "ECC_No", obj.ECC_No, True)
                clsCommon.AddColumnsForChange(coll, "SSI_Regn_No", obj.SSI_Regn_No, True)
                clsCommon.AddColumnsForChange(coll, "SSI_Regn_No_Date", clsCommon.GetPrintDate(obj.SSI_Regn_No_Date, "dd/MMM/yyyy hh:mm tt"), True)
                clsCommon.AddColumnsForChange(coll, "Is_Certified", obj.Is_Certified)
                clsCommon.AddColumnsForChange(coll, "System_Certification", obj.System_Certification, True)
                clsCommon.AddColumnsForChange(coll, "Other_Certification", obj.Other_Certification, True)
                clsCommon.AddColumnsForChange(coll, "Production_Exec", obj.Production_Exec, True)
                clsCommon.AddColumnsForChange(coll, "Production_Skilled", obj.Production_Skilled, True)
                clsCommon.AddColumnsForChange(coll, "Production_UnSkilled", obj.Production_UnSkilled, True)
                clsCommon.AddColumnsForChange(coll, "Production_Total", obj.Production_Total, True)
                clsCommon.AddColumnsForChange(coll, "QC_Exec", obj.QC_Exec, True)
                clsCommon.AddColumnsForChange(coll, "QC_Skilled", obj.QC_Skilled, True)
                clsCommon.AddColumnsForChange(coll, "QC_UnSkilled", obj.QC_UnSkilled, True)
                clsCommon.AddColumnsForChange(coll, "QC_Total", obj.QC_Total, True)
                clsCommon.AddColumnsForChange(coll, "Total_Room_Exec", obj.Total_Room_Exec, True)
                clsCommon.AddColumnsForChange(coll, "Total_Room_Skilled", obj.Total_Room_Skilled, True)
                clsCommon.AddColumnsForChange(coll, "Total_Room_UnSkilled", obj.Total_Room_UnSkilled, True)
                clsCommon.AddColumnsForChange(coll, "Total_Room_Total", obj.Total_Room_Total, True)
                clsCommon.AddColumnsForChange(coll, "DE_Exec", obj.DE_Exec, True)
                clsCommon.AddColumnsForChange(coll, "DE_Skilled", obj.DE_Skilled, True)
                clsCommon.AddColumnsForChange(coll, "DE_UnSkilled", obj.DE_UnSkilled, True)
                clsCommon.AddColumnsForChange(coll, "DE_Total", obj.DE_Total, True)
                clsCommon.AddColumnsForChange(coll, "Others_Exec", obj.Others_Exec, True)
                clsCommon.AddColumnsForChange(coll, "Others_Skilled", obj.Others_Skilled, True)
                clsCommon.AddColumnsForChange(coll, "Others_UnSkilled", obj.Others_UnSkilled, True)
                clsCommon.AddColumnsForChange(coll, "Others_Total", obj.Others_Total, True)

                clsCommon.AddColumnsForChange(coll, "Customer_1", obj.Customer_1, True)
                clsCommon.AddColumnsForChange(coll, "Customer_2", obj.Customer_2, True)
                clsCommon.AddColumnsForChange(coll, "Customer_3", obj.Customer_3, True)
                clsCommon.AddColumnsForChange(coll, "Customer_4", obj.Customer_4, True)

                clsCommon.AddColumnsForChange(coll, "Installed_Power_Capacity", obj.Installed_Power_Capacity, True)
                clsCommon.AddColumnsForChange(coll, "Own_Power_Generation_Capacity", obj.Own_Power_Generation_Capacity, True)
                clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code, True)
                clsCommon.AddColumnsForChange(coll, "Inspection_Facilities", obj.Inspection_Facilities, True)
                clsCommon.AddColumnsForChange(coll, "Material_Testing_Facilities", obj.Material_Testing_Facilities, True)
                clsCommon.AddColumnsForChange(coll, "Capacity", obj.Capacity, True)
                clsCommon.AddColumnsForChange(coll, "Process_Capability", obj.Process_Capability, True)
                clsCommon.AddColumnsForChange(coll, "Minimum_Batch_Size", obj.Minimum_Batch_Size, True)
                clsCommon.AddColumnsForChange(coll, "Customer_Complaint", obj.Customer_Complaint, True)
                clsCommon.AddColumnsForChange(coll, "Internal_Rejection", obj.Internal_Rejection, True)

                clsCommon.AddColumnsForChange(coll, "Is_MSDS", obj.Is_MSDS, True)
                clsCommon.AddColumnsForChange(coll, "Customer_Objection", obj.Customer_Objection, True)
                clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments, True)

                'clsCommon.AddColumnsForChange(coll, "Approval_For_Pilot", obj.Approval_For_Pilot, True)
                'clsCommon.AddColumnsForChange(coll, "Approval_For_Pilot_Date", clsCommon.GetPrintDate(obj.Approval_For_Pilot_Date, "dd/MMM/yyyy hh:mm tt"), True)
                'clsCommon.AddColumnsForChange(coll, "Regular_Lot_Status", obj.Regular_Lot_Status, True)
                'clsCommon.AddColumnsForChange(coll, "Quantity", obj.Quantity, True)
                'clsCommon.AddColumnsForChange(coll, "QA_Department", obj.QA_Department, True)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))


                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_SUPPLIER_REGISTRATION WHERE Registration_No='" + obj.Registration_No + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_SUPPLIER_REGISTRATION where Registration_No= '" & obj.Registration_No & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        obj.Registration_No = clsERPFuncationality.GetNextCode(trans, obj.Registration_Date, "Supplier Registration", "", "")
                        clsCommon.AddColumnsForChange(coll, "Registration_No", obj.Registration_No)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SUPPLIER_REGISTRATION", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")
                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SUPPLIER_REGISTRATION", OMInsertOrUpdate.Update, "Registration_No='" + obj.Registration_No + "'", trans)
                End If

                'If Not isNewEntry Then
                '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Registration_No), "TSPL_SUPPLIER_REGISTRATION", "Registration_No", trans)
                'End If

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select Registration_No As Code,Convert(varchar,Registration_Date,103) AS [Registration Date],Supplier_Name AS [Supplier Name] ,Supplier_Address AS [Supplier Address1], Supplier_Address2 AS [Supplier Address2],Phone_No_Work As [Phone No Work],Fax_No_Work AS [Fax No Work],Working_Hrs_Work AS [Working Hrs Work],Weekly_Holiday_Work As [Weekly Holiday Work],Phone_No_HO AS [Phone No HO],Fax_No_HO As [Fax No HO],Working_Hrs_HO AS [Working Hrs HO],Name_DWH AS [Name DWH],Desgination_DWH AS [Desgination DWH],Phone_No_DWH As [Phone No DWH],Name_AWH AS [Name AWH],Designation_AWH AS [Designation AWH],Phone_No_AWH As [Phone No AWH] ,Case When ISNULL(Nature_Of_Industry,'') ='SS'  Then 'Small Sacle' When ISNULL(Nature_Of_Industry,'') ='PvtL' Then 'Pvt. Limited'  When ISNULL(Nature_Of_Industry,'') ='PS' Then 'Prop. Ship' When ISNULL(Nature_Of_Industry,'') ='PubL' Then 'Public Limited' End  [Nature Of Industry],Year_Of_Establishment As [Year Of Establishment],Turn_Over As [Turn Over],State_Sales_Tax_No AS [State Sales Tax No], Convert(varchar,State_Sales_Tax_Date,103) As [State Sales Tax Date],Centra_Excise_Regn_No AS [Centra Excise Regn No] ,Convert(varchar,Centra_Excise_Regn_Date ,103) As [Centra Excise Regn Date] , ECC_No AS [ECC No],SSI_Regn_No AS [SSI Regn No],Convert(varchar,SSI_Regn_No_Date  ,103) As [SSI Regn No Date],Is_Certified AS [Is Certified],System_Certification As [System Certification],Other_Certification As [Other Certification],Installed_Power_Capacity As [Installed Power Capacity],Own_Power_Generation_Capacity AS [Own Power Generation Capacity],Terms_Code As [Terms Code],Inspection_Facilities As [Inspection Facilities],Material_Testing_Facilities As [Material Testing Facilities],Capacity,Process_Capability As [Process Capability],Minimum_Batch_Size As [Minimum Batch Size],Internal_Rejection AS [Internal Rejection],Posted ,Approved From TSPL_SUPPLIER_REGISTRATION  "
        str = clsCommon.ShowSelectForm("VRegNo", qry, "Code", "  Approved =1 AND Posted =1 ", CurrCode, "Code", isButtonClicked)

        Return str
    End Function
    Public Shared Function GetDataForVendor(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsSupplierRegistration
        Dim obj As ClsSupplierRegistration = Nothing

        Dim qry As String = "Select Registration_No As Code,Convert(varchar,Registration_Date,103) AS [Registration Date],Supplier_Name AS [Supplier Name] ,Supplier_Address AS [Supplier Address1], Supplier_Address2 AS [Supplier Address2],Phone_No_Work As [Phone No Work],Fax_No_Work AS [Fax No Work],Working_Hrs_Work AS [Working Hrs Work],Weekly_Holiday_Work As [Weekly Holiday Work],Phone_No_HO AS [Phone No HO],Fax_No_HO As [Fax No HO],Working_Hrs_HO AS [Working Hrs HO],Name_DWH AS [Name DWH],Desgination_DWH AS [Desgination DWH],Phone_No_DWH As [Phone No DWH],Name_AWH AS [Name AWH],Designation_AWH AS [Designation AWH],Phone_No_AWH As [Phone No AWH] ,Case When ISNULL(Nature_Of_Industry,'') ='SS'  Then 'Small Sacle' When ISNULL(Nature_Of_Industry,'') ='PvtL' Then 'Pvt. Limited'  When ISNULL(Nature_Of_Industry,'') ='PS' Then 'Prop. Ship' When ISNULL(Nature_Of_Industry,'') ='PubL' Then 'Public Limited' End  [Nature Of Industry],Year_Of_Establishment As [Year Of Establishment],Turn_Over As [Turn Over],State_Sales_Tax_No AS [State Sales Tax No], Convert(varchar,State_Sales_Tax_Date,103) As [State Sales Tax Date],Centra_Excise_Regn_No AS [Centra Excise Regn No] ,Convert(varchar,Centra_Excise_Regn_Date ,103) As [Centra Excise Regn Date] , ECC_No AS [ECC No],SSI_Regn_No AS [SSI Regn No],Convert(varchar,SSI_Regn_No_Date  ,103) As [SSI Regn No Date],Is_Certified AS [Is Certified],System_Certification As [System Certification],Other_Certification As [Other Certification],Installed_Power_Capacity As [Installed Power Capacity],Own_Power_Generation_Capacity AS [Own Power Generation Capacity],Terms_Code As [Terms Code],Inspection_Facilities As [Inspection Facilities],Material_Testing_Facilities As [Material Testing Facilities],Capacity,Process_Capability As [Process Capability],Minimum_Batch_Size As [Minimum Batch Size],Internal_Rejection AS [Internal Rejection],Posted ,Approved From TSPL_SUPPLIER_REGISTRATION WHERE 1=1 AND Approved =1 AND Posted =1  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsSupplierRegistration()
            obj.Registration_No = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Registration_Date = clsCommon.myCDate(dt.Rows(0)("Registration Date"))
            obj.Supplier_Name = clsCommon.myCstr(dt.Rows(0)("Supplier Name"))
            obj.Supplier_Address = clsCommon.myCstr(dt.Rows(0)("Supplier Address1"))
            obj.Supplier_Address2 = clsCommon.myCstr(dt.Rows(0)("Supplier Address2"))
            'obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))

            obj.Phone_No_Work = clsCommon.myCstr(dt.Rows(0)("Phone No Work"))
            obj.Fax_No_Work = clsCommon.myCstr(dt.Rows(0)("Fax No Work"))
            obj.Working_Hrs_Work = clsCommon.myCstr(dt.Rows(0)("Working Hrs Work"))
            obj.Weekly_Holiday_Work = clsCommon.myCstr(dt.Rows(0)("Weekly Holiday Work"))
            obj.Phone_No_HO = clsCommon.myCstr(dt.Rows(0)("Phone No HO"))
            obj.Fax_No_HO = clsCommon.myCstr(dt.Rows(0)("Fax No HO"))
            obj.Working_Hrs_HO = clsCommon.myCstr(dt.Rows(0)("Working Hrs HO"))
            obj.Weekly_Holiday_Work = clsCommon.myCstr(dt.Rows(0)("Weekly Holiday Work"))
            obj.Phone_No_HO = clsCommon.myCstr(dt.Rows(0)("Phone No HO"))
            obj.Fax_No_HO = clsCommon.myCstr(dt.Rows(0)("Fax No HO"))
            obj.Working_Hrs_HO = clsCommon.myCstr(dt.Rows(0)("Working Hrs HO"))
            obj.Name_DWH = clsCommon.myCstr(dt.Rows(0)("Name DWH"))
            obj.Desgination_DWH = clsCommon.myCstr(dt.Rows(0)("Desgination DWH"))
            obj.Phone_No_DWH = clsCommon.myCstr(dt.Rows(0)("Phone No DWH"))
            obj.Name_AWH = clsCommon.myCstr(dt.Rows(0)("Name DWH"))
            obj.Designation_AWH = clsCommon.myCstr(dt.Rows(0)("Designation AWH"))
            obj.Phone_No_AWH = clsCommon.myCstr(dt.Rows(0)("Phone No AWH"))
            obj.Nature_Of_Industry = clsCommon.myCstr(dt.Rows(0)("Nature Of Industry"))
            obj.Year_Of_Establishment = clsCommon.myCstr(dt.Rows(0)("Year Of Establishment"))
            obj.Turn_Over = clsCommon.myCstr(dt.Rows(0)("Turn Over"))

            obj.ECC_No = clsCommon.myCstr(dt.Rows(0)("ECC No"))

            obj.System_Certification = clsCommon.myCstr(dt.Rows(0)("System Certification"))
            obj.Other_Certification = clsCommon.myCstr(dt.Rows(0)("Other Certification"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms Code"))

        End If
        Return obj
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsSupplierRegistration
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsSupplierRegistration
        Dim obj As ClsSupplierRegistration = Nothing

        Dim qry As String = "Select * From TSPL_SUPPLIER_REGISTRATION where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SUPPLIER_REGISTRATION.Registration_No = (select MIN(Applicant_Registration_NoCode) from TSPL_SUPPLIER_REGISTRATION)"
            Case NavigatorType.Last
                qry += " and TSPL_SUPPLIER_REGISTRATION.Registration_No = (select Max(Registration_No) from TSPL_SUPPLIER_REGISTRATION)"
            Case NavigatorType.Next
                qry += " and TSPL_SUPPLIER_REGISTRATION.Registration_No = (select Min(Registration_No) from TSPL_SUPPLIER_REGISTRATION where  Registration_No >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SUPPLIER_REGISTRATION.Registration_No = (select Max(Registration_No) from TSPL_SUPPLIER_REGISTRATION where Registration_No <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SUPPLIER_REGISTRATION.Registration_No = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsSupplierRegistration()
            obj.Registration_No = clsCommon.myCstr(dt.Rows(0)("Registration_No"))
            obj.Registration_Date = clsCommon.myCDate(dt.Rows(0)("Registration_Date"))
            obj.Supplier_Name = clsCommon.myCstr(dt.Rows(0)("Supplier_Name"))
            obj.Supplier_Address = clsCommon.myCstr(dt.Rows(0)("Supplier_Address"))
            obj.Supplier_Address2 = clsCommon.myCstr(dt.Rows(0)("Supplier_Address2"))
            obj.Product = clsCommon.myCstr(dt.Rows(0)("Product"))
            obj.Category = clsCommon.myCstr(dt.Rows(0)("Category"))
            obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))

            obj.Phone_No_Work = clsCommon.myCstr(dt.Rows(0)("Phone_No_Work"))
            obj.Fax_No_Work = clsCommon.myCstr(dt.Rows(0)("Fax_No_Work"))
            obj.Working_Hrs_Work = clsCommon.myCstr(dt.Rows(0)("Working_Hrs_Work"))
            obj.Weekly_Holiday_Work = clsCommon.myCstr(dt.Rows(0)("Weekly_Holiday_Work"))
            obj.Phone_No_HO = clsCommon.myCstr(dt.Rows(0)("Phone_No_HO"))
            obj.Fax_No_HO = clsCommon.myCstr(dt.Rows(0)("Fax_No_HO"))
            obj.Working_Hrs_HO = clsCommon.myCstr(dt.Rows(0)("Working_Hrs_HO"))
            obj.Weekly_Holiday_Work = clsCommon.myCstr(dt.Rows(0)("Weekly_Holiday_Work"))
            obj.Phone_No_HO = clsCommon.myCstr(dt.Rows(0)("Phone_No_HO"))
            obj.Fax_No_HO = clsCommon.myCstr(dt.Rows(0)("Fax_No_HO"))
            obj.Working_Hrs_HO = clsCommon.myCstr(dt.Rows(0)("Working_Hrs_HO"))
            obj.Weekly_Holiday_HO = clsCommon.myCstr(dt.Rows(0)("Weekly_Holiday_HO"))
            obj.Name_DWH = clsCommon.myCstr(dt.Rows(0)("Name_DWH"))
            obj.Desgination_DWH = clsCommon.myCstr(dt.Rows(0)("Desgination_DWH"))
            obj.Phone_No_DWH = clsCommon.myCstr(dt.Rows(0)("Phone_No_DWH"))
            obj.Name_AWH = clsCommon.myCstr(dt.Rows(0)("Name_DWH"))
            obj.Designation_AWH = clsCommon.myCstr(dt.Rows(0)("Designation_AWH"))
            obj.Phone_No_AWH = clsCommon.myCstr(dt.Rows(0)("Phone_No_AWH"))
            obj.Nature_Of_Industry = clsCommon.myCstr(dt.Rows(0)("Nature_Of_Industry"))
            obj.Year_Of_Establishment = clsCommon.myCstr(dt.Rows(0)("Year_Of_Establishment"))
            obj.Turn_Over = clsCommon.myCstr(dt.Rows(0)("Turn_Over"))
            obj.State_Sales_Tax_No = clsCommon.myCstr(dt.Rows(0)("State_Sales_Tax_No"))
            obj.State_Sales_Tax_Date = clsCommon.myCDate(dt.Rows(0)("State_Sales_Tax_Date"))
            obj.Centra_Excise_Regn_No = clsCommon.myCstr(dt.Rows(0)("Centra_Excise_Regn_No"))
            obj.Centra_Excise_Regn_Date = clsCommon.myCDate(dt.Rows(0)("Centra_Excise_Regn_Date"))
            obj.ECC_No = clsCommon.myCstr(dt.Rows(0)("ECC_No"))
            obj.SSI_Regn_No = clsCommon.myCstr(dt.Rows(0)("SSI_Regn_No"))
            obj.SSI_Regn_No_Date = clsCommon.myCstr(dt.Rows(0)("SSI_Regn_No_Date"))
            obj.Is_Certified = clsCommon.myCdbl(dt.Rows(0)("Is_Certified"))
            obj.Is_MSDS = clsCommon.myCdbl(dt.Rows(0)("Is_MSDS"))
            obj.System_Certification = clsCommon.myCstr(dt.Rows(0)("System_Certification"))
            obj.Other_Certification = clsCommon.myCstr(dt.Rows(0)("Other_Certification"))
            obj.Production_Exec = clsCommon.myCdbl(dt.Rows(0)("Production_Exec"))
            obj.Production_Skilled = clsCommon.myCdbl(dt.Rows(0)("Production_Skilled"))
            obj.Production_UnSkilled = clsCommon.myCdbl(dt.Rows(0)("Production_UnSkilled"))
            obj.Production_Total = clsCommon.myCdbl(dt.Rows(0)("Production_Total"))
            obj.QC_Exec = clsCommon.myCdbl(dt.Rows(0)("QC_Exec"))
            obj.QC_Skilled = clsCommon.myCdbl(dt.Rows(0)("QC_Skilled"))
            obj.QC_UnSkilled = clsCommon.myCdbl(dt.Rows(0)("QC_UnSkilled"))
            obj.QC_Total = clsCommon.myCdbl(dt.Rows(0)("QC_Total"))
            obj.Total_Room_Exec = clsCommon.myCdbl(dt.Rows(0)("Total_Room_Exec"))
            obj.Total_Room_Skilled = clsCommon.myCdbl(dt.Rows(0)("Total_Room_Skilled"))
            obj.Total_Room_UnSkilled = clsCommon.myCdbl(dt.Rows(0)("Total_Room_UnSkilled"))
            obj.Total_Room_Total = clsCommon.myCdbl(dt.Rows(0)("Total_Room_Total"))
            obj.DE_Exec = clsCommon.myCdbl(dt.Rows(0)("DE_Exec"))
            obj.DE_Skilled = clsCommon.myCdbl(dt.Rows(0)("DE_Skilled"))
            obj.DE_UnSkilled = clsCommon.myCdbl(dt.Rows(0)("DE_UnSkilled"))
            obj.DE_Total = clsCommon.myCdbl(dt.Rows(0)("DE_Total"))
            obj.Others_Exec = clsCommon.myCdbl(dt.Rows(0)("Others_Exec"))
            obj.Others_Skilled = clsCommon.myCdbl(dt.Rows(0)("Others_Skilled"))
            obj.Others_UnSkilled = clsCommon.myCdbl(dt.Rows(0)("Others_UnSkilled"))
            obj.Others_Total = clsCommon.myCdbl(dt.Rows(0)("Others_Total"))
            obj.Customer_1 = clsCommon.myCstr(dt.Rows(0)("Customer_1"))
            obj.Customer_2 = clsCommon.myCstr(dt.Rows(0)("Customer_2"))
            obj.Customer_3 = clsCommon.myCstr(dt.Rows(0)("Customer_3"))
            obj.Customer_4 = clsCommon.myCstr(dt.Rows(0)("Customer_4"))
            obj.Installed_Power_Capacity = clsCommon.myCstr(dt.Rows(0)("Installed_Power_Capacity"))
            obj.Own_Power_Generation_Capacity = clsCommon.myCstr(dt.Rows(0)("Own_Power_Generation_Capacity"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Inspection_Facilities = clsCommon.myCstr(dt.Rows(0)("Inspection_Facilities"))
            obj.Material_Testing_Facilities = clsCommon.myCstr(dt.Rows(0)("Material_Testing_Facilities"))
            obj.Capacity = clsCommon.myCstr(dt.Rows(0)("Capacity"))
            obj.Process_Capability = clsCommon.myCstr(dt.Rows(0)("Process_Capability"))
            obj.Minimum_Batch_Size = clsCommon.myCstr(dt.Rows(0)("Minimum_Batch_Size"))
            obj.Customer_Complaint = clsCommon.myCstr(dt.Rows(0)("Customer_Complaint"))
            obj.Internal_Rejection = clsCommon.myCstr(dt.Rows(0)("Internal_Rejection"))
            obj.Customer_Objection = clsCommon.myCstr(dt.Rows(0)("Customer_Objection"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String


            qry = "Delete From TSPL_SUPPLIER_REGISTRATION Where Registration_No ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Registration No not found to Post")
            End If
            Dim obj As ClsSupplierRegistration = ClsSupplierRegistration.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Registration_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_SUPPLIER_REGISTRATION set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "'" & _
            " WHERE Registration_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ' ----------------- Get Nature Of Industry ------------------------
    Public Shared Function GetNatureOfIndustry() As DataTable
        Dim DT_Nature As DataTable = New DataTable
        DT_Nature.Columns.Add("Code", GetType(String))
        DT_Nature.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Nature.NewRow()
        DR("Name") = "Small Scale"
        DR("Code") = "SS"
        DT_Nature.Rows.Add(DR)

        DR = DT_Nature.NewRow()
        DR("Name") = "Prop. Ship"
        DR("Code") = "PS"
        DT_Nature.Rows.Add(DR)

        DR = DT_Nature.NewRow()
        DR("Name") = "Pvt. Limited"
        DR("Code") = "PvtL"
        DT_Nature.Rows.Add(DR)

        DR = DT_Nature.NewRow()
        DR("Name") = "Public Limited"
        DR("Code") = "PubL"
        DT_Nature.Rows.Add(DR)

        DT_Nature.AcceptChanges()

        Return DT_Nature
    End Function
    ' ----------------- Get System Certification ------------------------
    Public Shared Function GetSal() As DataTable
        Dim DT_Cert As DataTable = New DataTable
        DT_Cert.Columns.Add("Code", GetType(String))
        DT_Cert.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Cert.NewRow()
        DR("Name") = "ISO-9000"
        DR("Code") = "ISO-9000"
        DT_Cert.Rows.Add(DR)

        DR = DT_Cert.NewRow()
        DR("Name") = "QS-9000"
        DR("Code") = "QS-9000"
        DT_Cert.Rows.Add(DR)

        DR = DT_Cert.NewRow()
        DR("Name") = "TS:16949:2002"
        DR("Code") = "TS:16949:2002"
        DT_Cert.Rows.Add(DR)

        DR = DT_Cert.NewRow()
        DR("Name") = "Any Other"
        DR("Code") = "Any Other"
        DT_Cert.Rows.Add(DR)

        DT_Cert.AcceptChanges()

        Return DT_Cert
    End Function
   
End Class

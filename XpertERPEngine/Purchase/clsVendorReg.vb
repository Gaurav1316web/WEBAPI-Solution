Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsVendorReg

#Region "Variables"
    Public Code As String
    Public Name As String
    Public Address1 As String
    Public Address2 As String
    Public Name_Owners As String
    Public Organization_Type As String
    Public Sub_Contractor As String
    Public Phone_No As String
    Public Fax_No As String
    Public Turn_Over As String
    Public Contact_Person_Name As String
    Public Contact_Person_Phone_No As String
    Public Organization_Details As String
    Public Manufacturing_Facilities As String
    Public Captive_Power As String
    Public Captive_Power_Details As String
    Public Is_SeparateSection_Responsible As String
    Public WhoisAuthorised As String
    Public Is_full_doc_QS_Available As String
    Public Is_Std_drawing_Available As String
    Public Is_RM_Inspection_Available As String
    Public Is_Pro_Inspection_Available As String
    Public Is_Ins_despatch As String
    Public Is_Keep_Record As String
    Public Is_temp_perm_deviation_record As String
    Public Is_equip_calibrated_periodically As String
    Public Is_defined_QP As String
    Public Defined_QP_Details As String
    Public Is_Pro_Storage_Sys As String
    Public Is_RM_Identified As String
    Public Is_Facilities_Available As String
    Public Is_Packing_defined As String
    Public Packing_Def_Details As String
    Public NameAndAddress_Banker As String
    Public Salestax_No As String
    Public Central_Excise_No As String
    Public Payment_Terms As String
    Public Vendor_Name As String
    Public Vendor_Desig As String
    Public Vendor_Sign As Byte()
    Public Vendor_Date As DateTime = Nothing
    Public Assessor_Name As String
    Public Assessor_Desig As String
    Public Assessor_Sign As Byte()
    Public Assessor_Date As DateTime = Nothing
    Public Visited_By As String
    Public Suitablefor As String
    Public Suitablefor_Vendor As String
    Public Result_Approved As String
    Public Approved_Name As String
    Public Approved_Desig As String
    Public Approved_Sign As Byte()
    Public Approved_Date As DateTime = Nothing
    Public Is_VendorRegApproved As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public ArrMachinery As List(Of clsVendorRegMachineryDetail) = Nothing
    Public ArrCust As List(Of clsVendorRegCustDetail) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_VENDORREGISTRATION_MASTER.Registration_No as [Code],TSPL_VENDORREGISTRATION_MASTER.Name as [Name],TSPL_VENDORREGISTRATION_MASTER.Created_By as [Created By],TSPL_VENDORREGISTRATION_MASTER.Created_Date as [Created Date],TSPL_VENDORREGISTRATION_MASTER.Modified_By as [Modify By],TSPL_VENDORREGISTRATION_MASTER.Modified_Date as [Modify Date]  from TSPL_VENDORREGISTRATION_MASTER"
        str = clsCommon.ShowSelectForm("STMTRND", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "TSPL_VENDORREGISTRATION_MASTER.Created_Date")
        Return str

    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsVendorReg
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsVendorReg
        Dim obj As clsVendorReg = Nothing
        Dim qry As String = "select * FROM TSPL_VENDORREGISTRATION_MASTER  whERE 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VENDORREGISTRATION_MASTER.Registration_No = (select MIN(TSPL_VENDORREGISTRATION_MASTER.Registration_No) from TSPL_VENDORREGISTRATION_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_VENDORREGISTRATION_MASTER.Registration_No = (select Max(TSPL_VENDORREGISTRATION_MASTER.Registration_No) from TSPL_VENDORREGISTRATION_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_VENDORREGISTRATION_MASTER.Registration_No = (select Min(TSPL_VENDORREGISTRATION_MASTER.Registration_No) from TSPL_VENDORREGISTRATION_MASTER where  TSPL_VENDORREGISTRATION_MASTER.Registration_No>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_VENDORREGISTRATION_MASTER.Registration_No = (select Max(TSPL_VENDORREGISTRATION_MASTER.Registration_No) from TSPL_VENDORREGISTRATION_MASTER where TSPL_VENDORREGISTRATION_MASTER.Registration_No<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_VENDORREGISTRATION_MASTER.Registration_No = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVendorReg()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Registration_No"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            obj.Address1 = clsCommon.myCstr(dt.Rows(0)("Address1"))
            obj.Address2 = clsCommon.myCstr(dt.Rows(0)("Address2"))
            obj.Name_Owners = clsCommon.myCstr(dt.Rows(0)("Name_Owners"))
            obj.Organization_Type = clsCommon.myCstr(dt.Rows(0)("Organization_Type"))
            obj.Sub_Contractor = clsCommon.myCstr(dt.Rows(0)("Sub_Contractor"))
            obj.Phone_No = clsCommon.myCstr(dt.Rows(0)("Phone_No"))
            obj.Fax_No = clsCommon.myCstr(dt.Rows(0)("Fax_No"))
            obj.Turn_Over = clsCommon.myCstr(dt.Rows(0)("Turn_Over"))
            obj.Contact_Person_Name = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Name"))
            obj.Contact_Person_Phone_No = clsCommon.myCstr(dt.Rows(0)("Contact_Person_Phone_No"))
            obj.Organization_Details = clsCommon.myCstr(dt.Rows(0)("Organization_Details"))
            obj.Manufacturing_Facilities = clsCommon.myCstr(dt.Rows(0)("Manufacturing_Facilities"))
            obj.Captive_Power = clsCommon.myCstr(dt.Rows(0)("Captive_Power"))
            obj.Captive_Power_Details = clsCommon.myCstr(dt.Rows(0)("Captive_Power_Details"))
            obj.Is_SeparateSection_Responsible = clsCommon.myCstr(dt.Rows(0)("Is_SeparateSection_Responsible"))
            obj.WhoisAuthorised = clsCommon.myCstr(dt.Rows(0)("WhoisAuthorised"))
            obj.Is_full_doc_QS_Available = clsCommon.myCstr(dt.Rows(0)("Is_full_doc_QS_Available"))
            obj.Is_Std_drawing_Available = clsCommon.myCstr(dt.Rows(0)("Is_Std_drawing_Available"))
            obj.Is_RM_Inspection_Available = clsCommon.myCstr(dt.Rows(0)("Is_RM_Inspection_Available"))
            obj.Is_Pro_Inspection_Available = clsCommon.myCstr(dt.Rows(0)("Is_Pro_Inspection_Available"))
            obj.Is_Ins_despatch = clsCommon.myCstr(dt.Rows(0)("Is_Ins_despatch"))
            obj.Is_Keep_Record = clsCommon.myCstr(dt.Rows(0)("Is_Keep_Record"))
            obj.Is_temp_perm_deviation_record = clsCommon.myCstr(dt.Rows(0)("Is_temp_perm_deviation_record"))
            obj.Is_equip_calibrated_periodically = clsCommon.myCstr(dt.Rows(0)("Is_equip_calibrated_periodically"))
            obj.Is_defined_QP = clsCommon.myCstr(dt.Rows(0)("Is_defined_QP"))
            obj.Defined_QP_Details = clsCommon.myCstr(dt.Rows(0)("Defined_QP_Details"))
            obj.Is_Pro_Storage_Sys = clsCommon.myCstr(dt.Rows(0)("Is_Pro_Storage_Sys"))
            obj.Is_RM_Identified = clsCommon.myCstr(dt.Rows(0)("Is_RM_Identified"))
            obj.Is_Facilities_Available = clsCommon.myCstr(dt.Rows(0)("Is_Facilities_Available"))
            obj.Is_Packing_defined = clsCommon.myCstr(dt.Rows(0)("Is_Packing_defined"))
            obj.Packing_Def_Details = clsCommon.myCstr(dt.Rows(0)("Packing_Def_Details"))
            obj.NameAndAddress_Banker = clsCommon.myCstr(dt.Rows(0)("NameAndAddress_Banker"))
            obj.Salestax_No = clsCommon.myCstr(dt.Rows(0)("Salestax_No"))
            obj.Central_Excise_No = clsCommon.myCstr(dt.Rows(0)("Central_Excise_No"))
            obj.Payment_Terms = clsCommon.myCstr(dt.Rows(0)("Payment_Terms"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Vendor_Desig = clsCommon.myCstr(dt.Rows(0)("Vendor_Desig"))
            obj.Vendor_Sign = DirectCast(dt.Rows(0)("Vendor_Sign"), Byte())
            obj.Vendor_Date = clsCommon.myCDate(dt.Rows(0)("Vendor_Date"))
            obj.Assessor_Name = clsCommon.myCstr(dt.Rows(0)("Assessor_Name"))
            obj.Assessor_Desig = clsCommon.myCstr(dt.Rows(0)("Assessor_Desig"))
            obj.Assessor_Sign = DirectCast(dt.Rows(0)("Assessor_Sign"), Byte())
            obj.Assessor_Date = clsCommon.myCDate(dt.Rows(0)("Assessor_Date"))
            obj.Visited_By = clsCommon.myCstr(dt.Rows(0)("Visited_By"))
            obj.Suitablefor = clsCommon.myCstr(dt.Rows(0)("Suitablefor"))
            obj.Suitablefor_Vendor = clsCommon.myCstr(dt.Rows(0)("Suitablefor_Vendor"))
            obj.Result_Approved = clsCommon.myCstr(dt.Rows(0)("Result_Approved"))
            obj.Approved_Name = clsCommon.myCstr(dt.Rows(0)("Approved_Name"))
            obj.Approved_Desig = clsCommon.myCstr(dt.Rows(0)("Approved_Desig"))
            obj.Approved_Sign = DirectCast(dt.Rows(0)("Approved_Sign"), Byte())
            obj.Approved_Date = clsCommon.myCDate(dt.Rows(0)("Approved_Date"))
            obj.Is_VendorRegApproved = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_VendorRegApproved")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            qry = "SELECT * from TSPL_VENDORREGISTRATION_MACHINERY_DETAILS where TSPL_VENDORREGISTRATION_MACHINERY_DETAILS.Registration_No='" + obj.Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrMachinery = New List(Of clsVendorRegMachineryDetail)
                Dim objTr As clsVendorRegMachineryDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsVendorRegMachineryDetail
                    objTr.Registration_No = clsCommon.myCstr(dr("Registration_No"))
                    objTr.Machine_Description = clsCommon.myCstr(dr("Machine_Description"))
                    objTr.MakeandModel = clsCommon.myCstr(dr("MakeandModel"))
                    objTr.NoofInst = clsCommon.myCstr(dr("NoofInst"))
                    objTr.YearofPurchase = clsCommon.myCstr(dr("YearofPurchase"))
                    objTr.Type = clsCommon.myCstr(dr("Type"))
                    obj.ArrMachinery.Add(objTr)
                Next
            End If

            qry = "SELECT * from TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS where TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS.Registration_No='" + obj.Code + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrCust = New List(Of clsVendorRegCustDetail)
                Dim objCust As clsVendorRegCustDetail
                For Each dr As DataRow In dt.Rows
                    objCust = New clsVendorRegCustDetail
                    objCust.Registration_No = clsCommon.myCstr(dr("Registration_No"))
                    objCust.CustomerNameandAddress = clsCommon.myCstr(dr("CustomerNameandAddress"))
                    obj.ArrCust.Add(objCust)
                Next
            End If

        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsVendorReg, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                isSaved = True
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsVendorReg, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_VENDORREGISTRATION_MACHINERY_DETAILS where TSPL_VENDORREGISTRATION_MACHINERY_DETAILS.Registration_No='" + obj.Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS where TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS.Registration_No='" + obj.Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Address1", obj.Address1)
            clsCommon.AddColumnsForChange(coll, "Address2", obj.Address2)
            clsCommon.AddColumnsForChange(coll, "Name_Owners", obj.Name_Owners)
            clsCommon.AddColumnsForChange(coll, "Organization_Type", obj.Organization_Type)
            clsCommon.AddColumnsForChange(coll, "Sub_Contractor", obj.Sub_Contractor)
            clsCommon.AddColumnsForChange(coll, "Phone_No", obj.Phone_No)
            clsCommon.AddColumnsForChange(coll, "Fax_No", obj.Fax_No)
            clsCommon.AddColumnsForChange(coll, "Turn_Over", obj.Turn_Over)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Name", obj.Contact_Person_Name)
            clsCommon.AddColumnsForChange(coll, "Contact_Person_Phone_No", obj.Contact_Person_Phone_No)
            clsCommon.AddColumnsForChange(coll, "Organization_Details", obj.Organization_Details)
            clsCommon.AddColumnsForChange(coll, "Manufacturing_facilities", obj.Manufacturing_Facilities)
            clsCommon.AddColumnsForChange(coll, "Captive_Power", obj.Captive_Power)
            clsCommon.AddColumnsForChange(coll, "Captive_Power_Details", obj.Captive_Power_Details)
            clsCommon.AddColumnsForChange(coll, "Is_SeparateSection_Responsible", obj.Is_SeparateSection_Responsible)
            clsCommon.AddColumnsForChange(coll, "WhoisAuthorised", obj.WhoisAuthorised)
            clsCommon.AddColumnsForChange(coll, "Is_full_doc_qs_available", obj.Is_full_doc_QS_Available)
            clsCommon.AddColumnsForChange(coll, "Is_std_drawing_available", obj.Is_Std_drawing_Available)
            clsCommon.AddColumnsForChange(coll, "Is_RM_Inspection_available", obj.Is_RM_Inspection_Available)
            clsCommon.AddColumnsForChange(coll, "Is_Pro_Inspection_available", obj.Is_Pro_Inspection_Available)
            clsCommon.AddColumnsForChange(coll, "Is_Ins_Despatch", obj.Is_Ins_despatch)
            clsCommon.AddColumnsForChange(coll, "Is_Keep_Record", obj.Is_Keep_Record)
            clsCommon.AddColumnsForChange(coll, "Is_temp_perm_deviation_record", obj.Is_temp_perm_deviation_record)
            clsCommon.AddColumnsForChange(coll, "Is_equip_calibrated_periodically", obj.Is_equip_calibrated_periodically)
            clsCommon.AddColumnsForChange(coll, "is_defined_QP", obj.Is_defined_QP)
            clsCommon.AddColumnsForChange(coll, "Defined_QP_Details", obj.Defined_QP_Details)
            clsCommon.AddColumnsForChange(coll, "Is_Pro_Storage_sys", obj.Is_Pro_Storage_Sys)
            clsCommon.AddColumnsForChange(coll, "Is_RM_Identified", obj.Is_RM_Identified)
            clsCommon.AddColumnsForChange(coll, "Is_facilities_available", obj.Is_Facilities_Available)
            clsCommon.AddColumnsForChange(coll, "Is_packing_defined", obj.Is_Packing_defined)
            clsCommon.AddColumnsForChange(coll, "Packing_Def_Details", obj.Packing_Def_Details)
            clsCommon.AddColumnsForChange(coll, "NameAndAddress_Banker", obj.NameAndAddress_Banker)
            clsCommon.AddColumnsForChange(coll, "Salestax_No", obj.Salestax_No)
            clsCommon.AddColumnsForChange(coll, "Central_Excise_No", obj.Central_Excise_No)
            clsCommon.AddColumnsForChange(coll, "Payment_Terms", obj.Payment_Terms)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "Vendor_Sign", obj.Vendor_Sign)
            clsCommon.AddColumnsForChange(coll, "Vendor_Desig", obj.Vendor_Desig)
            clsCommon.AddColumnsForChange(coll, "Vendor_Date", clsCommon.GetPrintDate(obj.Vendor_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Assessor_Name", obj.Assessor_Name)
            clsCommon.AddColumnsForChange(coll, "Assessor_Desig", obj.Assessor_Desig)
            clsCommon.AddColumnsForChange(coll, "Assessor_Sign", obj.Assessor_Sign)
            clsCommon.AddColumnsForChange(coll, "Assessor_Date", clsCommon.GetPrintDate(obj.Assessor_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Visited_By", obj.Visited_By)
            clsCommon.AddColumnsForChange(coll, "Suitablefor", obj.Suitablefor)
            clsCommon.AddColumnsForChange(coll, "Suitablefor_Vendor", obj.Suitablefor_Vendor)
            clsCommon.AddColumnsForChange(coll, "Result_Approved", obj.Result_Approved)
            clsCommon.AddColumnsForChange(coll, "Approved_Desig", obj.Approved_Desig)
            clsCommon.AddColumnsForChange(coll, "Approved_Sign", obj.Approved_Sign)
            clsCommon.AddColumnsForChange(coll, "Approved_Date", clsCommon.GetPrintDate(obj.Approved_Date, "dd/MMM/yyyy hh:mm tt"))


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"), clsDocType.VENREG, "", "")
                clsCommon.AddColumnsForChange(coll, "Registration_No", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                qry = "SELECT Count(*) FROM TSPL_VENDORREGISTRATION_MASTER where TSPL_VENDORREGISTRATION_MASTER.Registration_No= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDORREGISTRATION_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Registration No Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDORREGISTRATION_MASTER", OMInsertOrUpdate.Update, "TSPL_VENDORREGISTRATION_MASTER.Registration_No='" + obj.Code + "'", trans)
            End If

            isSaved = isSaved AndAlso clsVendorRegMachineryDetail.SaveData(obj.Code, obj.ArrMachinery, trans)
            isSaved = isSaved AndAlso clsVendorRegCustDetail.SaveData(obj.Code, obj.ArrCust, trans)
            '--------------------------------------------------------------------------------------------------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim qry As String

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            qry = "delete from TSPL_VENDORREGISTRATION_MACHINERY_DETAILS where Registration_No ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS where Registration_No ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_VENDORREGISTRATION_MASTER where Registration_No ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_VENDORREGISTRATION_MASTER.Registration_No from TSPL_VENDORREGISTRATION_MASTER where TSPL_VENDORREGISTRATION_MASTER.Registration_No ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class

Public Class clsVendorRegMachineryDetail

#Region "Variables"
    Public Registration_No As String
    Public Machine_Description As String
    Public MakeandModel As String
    Public NoofInst As String
    Public YearofPurchase As String
    Public Type As String
#End Region

    Public Shared Function SaveData(ByVal strRegNo As String, ByVal Arr As List(Of clsVendorRegMachineryDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsVendorRegMachineryDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Registration_No", strRegNo)
                clsCommon.AddColumnsForChange(coll, "Machine_Description", obj.Machine_Description)
                clsCommon.AddColumnsForChange(coll, "MakeandModel", obj.MakeandModel)
                clsCommon.AddColumnsForChange(coll, "NoofInst", obj.NoofInst)
                clsCommon.AddColumnsForChange(coll, "YearofPurchase", obj.YearofPurchase)
                clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDORREGISTRATION_MACHINERY_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class

Public Class clsVendorRegCustDetail

#Region "Variables"
    Public Registration_No As String
    Public CustomerNameandAddress As String
#End Region

    Public Shared Function SaveData(ByVal strRegNo As String, ByVal Arr As List(Of clsVendorRegCustDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsVendorRegCustDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Registration_No", strRegNo)
                clsCommon.AddColumnsForChange(coll, "CustomerNameandAddress", obj.CustomerNameandAddress)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDORREGISTRATION_CUSTOMER_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class

Public Class clsTRANSPORT_MASTER
#Region "Veriable"
    Public Transport_Id As String = String.Empty
    Public Transporter_Name As String = String.Empty
    Public city As String = String.Empty
    Public state As String = String.Empty
    Public pincode As String = String.Empty
    Public panno As String = String.Empty
    Public Phone As String = String.Empty
    Public Add1 As String = String.Empty
    Public Add2 As String = String.Empty
    Public Email As String = String.Empty


#End Region
    Public Shared Function SaveData(ByVal obj As clsTRANSPORT_MASTER, ByVal IsNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Transporter_Name", obj.Transporter_Name)
            clsCommon.AddColumnsForChange(coll, "city", obj.city)
            clsCommon.AddColumnsForChange(coll, "state", obj.state)
            clsCommon.AddColumnsForChange(coll, "pincode", obj.pincode)
            clsCommon.AddColumnsForChange(coll, "panno", obj.panno)
            clsCommon.AddColumnsForChange(coll, "Phone", obj.Phone)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
            clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", connectSql.serverDate(trans))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If IsNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Transport_Id", obj.Transport_Id)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", connectSql.serverDate(trans))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPORT_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPORT_MASTER", OMInsertOrUpdate.Update, "TSPL_TRANSPORT_MASTER.Transport_Id='" + obj.Transport_Id + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

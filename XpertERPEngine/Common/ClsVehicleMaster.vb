Imports common
Imports System.Data.SqlClient

Public Class ClsVehicleMaster
#Region "Variables"
    Public SequenceNo As Integer = 0
    Public EmployeeNo As String = Nothing
    Public VehicleCode As String = Nothing
    Public Description As String = Nothing
    Public Number As String = Nothing
    Public Vehicle_Type As String = Nothing
    Public Vehicle_Reg_no As String = Nothing
    Public Vehicle_chasis_No As String = Nothing
    Public Model As String = Nothing
    Public Capacity As Double = Nothing
    Public Tran_type As String = Nothing
    Public Registered_On As String = Nothing
    Public EngineNo As String = Nothing
    Public Fitness_valid_from As Date? = Nothing
    Public Fitness_valid_Till As Date? = Nothing
    Public Insurance_valid_from As Date? = Nothing
    Public Insurance_valid_Till As Date? = Nothing
    Public Pollutionchk_valid_from As Date? = Nothing
    Public Pollutionchk_valid_Till As Date? = Nothing
    Public RoadTax_valid_from As Date? = Nothing
    Public RoadTax_valid_Till As Date? = Nothing
    Public VehicleBrand As String = Nothing
    Public VehicleName As String = Nothing
    Public Vehicle_No As String = Nothing
    ' Public InsuranceExpirydate As Date = Today
    Public Location As String = Nothing
    Public TransportId As String = Nothing
    Public CrateCapacity As Double = 0
    Public chagrshift As Decimal = Nothing
    Public avgrate As Decimal = Nothing
    Public dieselrate As Decimal = Nothing
    Public RentalType As String = String.Empty
    Public RentalAmount As Double = 0
    Public Rate_Type As String = String.Empty
    Public Price_Ltr_KG As Double = 0
    Public pricekm As Decimal = Nothing
    Public Is_Additional As Boolean = False
    Public status As String = Nothing
    Public MTCapacity As Double = 0
    Public MTValue As Double = 0
    Public Vehicle_Weight As Double = 0
    Public Column_Crate As Double = 0
#End Region


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsVehicleMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_VEHICLE_MASTER where Vehicle_Id='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsVehicleMaster
        Dim obj As New ClsVehicleMaster()
        Dim qry As String = "Select TSPL_VEHICLE_MASTER.Vehicle_id as [Vehicle Id],TSPL_VEHICLE_MASTER.Status,TSPL_VEHICLE_MASTER.price_km,TSPL_VEHICLE_MASTER.price_ltr_kg,TSPL_VEHICLE_MASTER.Rate_type,TSPL_VEHICLE_MASTER.Shift_Charges,TSPL_VEHICLE_MASTER.Avg_Km_Ltr,TSPL_VEHICLE_MASTER.Diesel_Rate,TSPL_VEHICLE_MASTER.Rental_Type,TSPL_VEHICLE_MASTER.Rental_Amount, TSPL_VEHICLE_MASTER.Description,  TSPL_VEHICLE_MASTER.Model, " &
        "TSPL_VEHICLE_MASTER.Vehicle_Weight,TSPL_VEHICLE_MASTER.Vehicle_No,TSPL_VEHICLE_MASTER.CrateCapacity, Case When ISNULL(TSPL_VEHICLE_MASTER.Number,'')='' Then TSPL_VEHICLE_MASTER.Description Else TSPL_VEHICLE_MASTER.Number End As Number, TSPL_VEHICLE_MASTER.Vehicle_Type ,TSPL_VEHICLE_MASTER.Vehicle_Reg_No ,TSPL_VEHICLE_MASTER.Vehicle_Chesis_No ," &
"TSPL_VEHICLE_MASTER.Capacity,TSPL_VEHICLE_MASTER.Trans_type,TSPL_VEHICLE_MASTER.transport_id ," &
"TSPL_VEHICLE_MASTER.RegisteredOn ,TSPL_VEHICLE_MASTER.Vehicle_Brand ,TSPL_VEHICLE_MASTER.Vehicle_Name ,TSPL_VEHICLE_MASTER.Engine_NO ," &
"TSPL_VEHICLE_MASTER.Insurance_valid_From ,TSPL_VEHICLE_MASTER.insurance_valid_Till,TSPL_VEHICLE_MASTER.Fitness_valid_From ," &
"TSPL_VEHICLE_MASTER.Fitness_valid_Till ,TSPL_VEHICLE_MASTER.pollutionCheck_valid_From ,TSPL_VEHICLE_MASTER.PollutionCheck_valid_till ," &
"TSPL_VEHICLE_MASTER.Roadtax_valid_From ,TSPL_VEHICLE_MASTER.Roadtax_valid_till ,TSPL_VEHICLE_MASTER.Location,TSPL_VEHICLE_MASTER.SequenceNo,TSPL_VEHICLE_MASTER.Employee_Id,TSPL_VEHICLE_MASTER.MTCapacity,TSPL_VEHICLE_MASTER.MTValue,TSPL_VEHICLE_MASTER.Is_Additional,TSPL_VEHICLE_MASTER.Column_Crate  from TSPL_VEHICLE_MASTER " &
"WHERE 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VEHICLE_MASTER.Vehicle_id = (select MIN( TSPL_VEHICLE_MASTER.Vehicle_id) from TSPL_VEHICLE_MASTER)"
            Case NavigatorType.Last
                qry += "  and  TSPL_VEHICLE_MASTER.Vehicle_id= (select Max( TSPL_VEHICLE_MASTER.Vehicle_id) from TSPL_VEHICLE_MASTER)"
            Case NavigatorType.Next
                qry += "  and  TSPL_VEHICLE_MASTER.Vehicle_id = (select Min( TSPL_VEHICLE_MASTER.Vehicle_id) from TSPL_VEHICLE_MASTER where  TSPL_VEHICLE_MASTER.Vehicle_id>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and  TSPL_VEHICLE_MASTER.Vehicle_id= (select Max( TSPL_VEHICLE_MASTER.Vehicle_id) from TSPL_VEHICLE_MASTER where TSPL_VEHICLE_MASTER.Vehicle_id<'" + strCode + "')"
            Case NavigatorType.Current
                qry += "  and   TSPL_VEHICLE_MASTER.Vehicle_id = '" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.SequenceNo = clsCommon.myCdbl(dt.Rows(0)("SequenceNo"))
            obj.VehicleCode = dt.Rows(0)("Vehicle Id")
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Number = clsCommon.myCstr(dt.Rows(0)("Number"))
            obj.Vehicle_Type = clsCommon.myCstr(dt.Rows(0)("Vehicle_Type"))
            obj.Vehicle_Reg_no = clsCommon.myCstr(dt.Rows(0)("Vehicle_Reg_no"))
            obj.TransportId = clsCommon.myCstr(dt.Rows(0)("Transport_id"))
            obj.Vehicle_chasis_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_chesis_No"))
            obj.Model = clsCommon.myCstr(dt.Rows(0)("Model"))
            obj.Capacity = clsCommon.myCdbl(dt.Rows(0)("Capacity"))
            obj.Tran_type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            obj.Registered_On = clsCommon.myCstr(dt.Rows(0)("RegisteredOn"))
            '   obj.InsuranceExpirydate = clsCommon.myCDate(dt.Rows(0)("InsuranceExpDate"))
            obj.VehicleBrand = clsCommon.myCstr(dt.Rows(0)("Vehicle_Brand"))
            obj.VehicleName = clsCommon.myCstr(dt.Rows(0)("Vehicle_Name"))
            obj.EngineNo = clsCommon.myCstr(dt.Rows(0)("Engine_NO"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.CrateCapacity = clsCommon.myCdbl(dt.Rows(0)("CrateCapacity"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.EmployeeNo = clsCommon.myCstr(dt.Rows(0)("Employee_Id"))
            obj.MTCapacity = clsCommon.myCdbl(dt.Rows(0)("MTCapacity"))
            obj.MTValue = clsCommon.myCdbl(dt.Rows(0)("MTValue"))
            If dt.Rows(0)("Insurance_valid_From") IsNot DBNull.Value Then
                obj.Insurance_valid_from = clsCommon.myCDate(dt.Rows(0)("Insurance_valid_From"))
            Else
                obj.Insurance_valid_from = Nothing

            End If

            If dt.Rows(0)("Insurance_valid_Till") IsNot DBNull.Value Then
                obj.Insurance_valid_Till = clsCommon.myCDate(dt.Rows(0)("Insurance_valid_Till"))
            Else
                obj.Insurance_valid_Till = Nothing
            End If

            If dt.Rows(0)("Fitness_valid_From") IsNot DBNull.Value Then
                obj.Fitness_valid_from = clsCommon.myCDate(dt.Rows(0)("Fitness_valid_From"))
            Else
                obj.Fitness_valid_from = Nothing
            End If

            If dt.Rows(0)("Fitness_valid_Till") IsNot DBNull.Value Then
                obj.Fitness_valid_Till = clsCommon.myCDate(dt.Rows(0)("Fitness_valid_Till"))
            Else
                obj.Fitness_valid_Till = Nothing
            End If


            If dt.Rows(0)("pollutionCheck_valid_From") IsNot DBNull.Value Then
                obj.Pollutionchk_valid_from = clsCommon.myCDate(dt.Rows(0)("pollutionCheck_valid_From"))
            Else
                obj.Pollutionchk_valid_from = Nothing
            End If


            If dt.Rows(0)("PollutionCheck_valid_till") IsNot DBNull.Value Then
                obj.Pollutionchk_valid_Till = clsCommon.myCDate(dt.Rows(0)("PollutionCheck_valid_till"))
            Else
                obj.Pollutionchk_valid_Till = Nothing
            End If


            If dt.Rows(0)("RoadTax_valid_from") IsNot DBNull.Value Then
                obj.RoadTax_valid_from = clsCommon.myCDate(dt.Rows(0)("RoadTax_valid_from"))
            Else
                obj.RoadTax_valid_from = Nothing
            End If


            If dt.Rows(0)("RoadTax_valid_Till") IsNot DBNull.Value Then
                obj.RoadTax_valid_Till = clsCommon.myCDate(dt.Rows(0)("RoadTax_valid_Till"))
            Else
                obj.RoadTax_valid_Till = Nothing
            End If

            obj.status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.chagrshift = clsCommon.myCdbl(dt.Rows(0)("Shift_Charges"))
            obj.avgrate = clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))
            obj.dieselrate = clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))


            obj.Price_Ltr_KG = clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG"))
            obj.Rate_Type = clsCommon.myCstr(dt.Rows(0)("Rate_Type"))


            obj.RentalType = clsCommon.myCstr(dt.Rows(0)("Rental_Type"))
            obj.RentalAmount = clsCommon.myCdbl(dt.Rows(0)("Rental_Amount"))
            obj.pricekm = clsCommon.myCdbl(dt.Rows(0)("price_km"))

            obj.Vehicle_Weight = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Weight"))
            obj.Is_Additional = IIf(clsCommon.myCstr(dt.Rows(0)("Is_Additional")) = "T", True, False)
            If clsCommon.myCdbl(dt.Rows(0)("Column_Crate")) > 0 Then
                obj.Column_Crate = clsCommon.myCdbl(dt.Rows(0)("Column_Crate"))
            Else
                obj.Column_Crate = 0
            End If
        End If
        Return obj

    End Function

    Public Function SaveData(ByVal obj As ClsVehicleMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try

            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "Vehicle_id", obj.VehicleCode)
            clsCommon.AddColumnsForChange(coll, "SequenceNo", obj.SequenceNo)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Number", obj.Number)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Type", obj.Vehicle_Type)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Reg_no", obj.Vehicle_Reg_no)
            clsCommon.AddColumnsForChange(coll, "Transport_id", obj.TransportId)
            clsCommon.AddColumnsForChange(coll, "Vehicle_chesis_No", obj.Vehicle_chasis_No)
            clsCommon.AddColumnsForChange(coll, "Model", obj.Model)
            clsCommon.AddColumnsForChange(coll, "Capacity", obj.Capacity)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Tran_type)
            clsCommon.AddColumnsForChange(coll, "RegisteredOn", obj.Registered_On)
            '  clsCommon.AddColumnsForChange(coll, "InsuranceExpDate", clsCommon.myCDate(obj.InsuranceExpirydate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Name", obj.VehicleName)
            clsCommon.AddColumnsForChange(coll, "Engine_NO", obj.EngineNo)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Brand", obj.VehicleBrand)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.VehicleName)
            clsCommon.AddColumnsForChange(coll, "CrateCapacity", obj.CrateCapacity, True)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Employee_Id", obj.EmployeeNo, True)
            clsCommon.AddColumnsForChange(coll, "MTCapacity", obj.MTCapacity)
            clsCommon.AddColumnsForChange(coll, "MTValue", obj.MTValue)
            If obj.Insurance_valid_from IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Insurance_valid_From", clsCommon.GetPrintDate(obj.Insurance_valid_from, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Insurance_valid_From", Nothing, True)
            End If

            If obj.Insurance_valid_Till IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Insurance_valid_Till", clsCommon.GetPrintDate(obj.Insurance_valid_Till, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Insurance_valid_Till", Nothing, True)
            End If

            If obj.Fitness_valid_from IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Fitness_valid_From", clsCommon.GetPrintDate(obj.Fitness_valid_from, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Fitness_valid_From", Nothing, True)
            End If

            If obj.Fitness_valid_Till IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Fitness_valid_Till", clsCommon.GetPrintDate(obj.Fitness_valid_Till, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Fitness_valid_Till", Nothing, True)
            End If

            If obj.Pollutionchk_valid_from IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "pollutionCheck_valid_From", clsCommon.GetPrintDate(obj.Pollutionchk_valid_from, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "pollutionCheck_valid_From", Nothing, True)
            End If

            If obj.Pollutionchk_valid_Till IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "PollutionCheck_valid_till", clsCommon.GetPrintDate(obj.Pollutionchk_valid_Till, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "PollutionCheck_valid_till", Nothing, True)
            End If

            If obj.RoadTax_valid_from IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Roadtax_valid_From", clsCommon.GetPrintDate(obj.RoadTax_valid_from, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Roadtax_valid_From", Nothing, True)
            End If

            If obj.RoadTax_valid_Till IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Roadtax_valid_Till", clsCommon.GetPrintDate(obj.RoadTax_valid_Till, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Roadtax_valid_Till", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "STATUS", obj.status)
            clsCommon.AddColumnsForChange(coll, "Shift_Charges", obj.chagrshift) 'txt_chrg
            clsCommon.AddColumnsForChange(coll, "Avg_Km_Ltr", obj.avgrate) 'txtavgkm
            clsCommon.AddColumnsForChange(coll, "Diesel_Rate", obj.dieselrate) 'txtdiesel

            clsCommon.AddColumnsForChange(coll, "Rental_type", clsCommon.myCstr(obj.RentalType))
            clsCommon.AddColumnsForChange(coll, "Rental_Amount", clsCommon.myCdbl(obj.RentalAmount))

            clsCommon.AddColumnsForChange(coll, "Price_Ltr_KG", clsCommon.myCdbl(obj.Price_Ltr_KG))
            clsCommon.AddColumnsForChange(coll, "Rate_Type", clsCommon.myCstr(obj.Rate_Type))
            clsCommon.AddColumnsForChange(coll, "Is_Additional", IIf(obj.Is_Additional = True, "T", "F"))
            clsCommon.AddColumnsForChange(coll, "Price_KM", obj.pricekm) 'txt_km
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Weight", obj.Vehicle_Weight)
            clsCommon.AddColumnsForChange(coll, "Column_Crate", obj.Column_Crate)
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vehicle_MASTER where Vehicle_Id='" & obj.VehicleCode & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.VehicleCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.VehicleMaster, "", "")
                        If clsCommon.myLen(obj.VehicleCode) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.VehicleCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)
                Dim qry As String = "SELECT Count(*) FROM TSPL_Vehicle_MASTER where Vehicle_Id= '" & obj.VehicleCode & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vehicle_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.VehicleCode, "TSPL_Vehicle_MASTER", "Vehicle_Id", trans)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vehicle_MASTER", OMInsertOrUpdate.Update, "Vehicle_Id='" + obj.VehicleCode + "'", trans)
            End If
            If trans IsNot Nothing Then
                If isSaved = True Then
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
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Vehicle_Id from TSPL_Vehicle_MASTER where Vehicle_Id ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select tspl_vehicle_master.Vehicle_Id as Code,tspl_vehicle_master.Number as Name from tspl_vehicle_master   "
        str = clsCommon.ShowSelectForm("VEHICLEMASTER", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetName(ByVal strCode As String, ByVal Trans As SqlTransaction) As String
        Dim qry As String = "select tspl_vehicle_master.Number as Name from tspl_vehicle_master where Vehicle_Id='" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Trans))
    End Function
End Class

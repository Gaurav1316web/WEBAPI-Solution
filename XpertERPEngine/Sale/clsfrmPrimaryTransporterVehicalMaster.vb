Imports common
Imports System.Data.SqlClient
Public Class clsfrmPrimaryTransporterVehicalMaster
#Region "Variables"
    Public docno As String = Nothing
    Public desc As String = Nothing
    Public primarycode As String = Nothing
    Public primaryname As String = Nothing
    Public mcccode As String = Nothing
    Public mccname As String = Nothing
    Public vehicleno As String = Nothing
    Public year As String = Nothing
    Public capacity As Decimal = Nothing
    Public pricekm As Decimal = Nothing
    Public priceltr As Decimal = Nothing
    Public ltr_kg As Decimal = Nothing
    Public chagrshift As Decimal = Nothing
    Public avgrate As Decimal = Nothing
    Public dieselrate As Decimal = Nothing
    Public dayrental As Decimal = Nothing
    Public weekrental As Decimal = Nothing
    Public monthrental As Decimal = Nothing
    Public From1to10 As Decimal = Nothing
    Public From10to20 As Decimal = Nothing
    Public Above20 As Decimal = Nothing
    Public status As String = Nothing
    Public RentalType As String = String.Empty
    Public RentalAmount As Double = 0
    Public Rate_Type As String = String.Empty
    Public Price_Ltr_KG As Double = 0
    Public Route_Code As String = Nothing
    Public Route_Name As String = Nothing
    Public Is_Additional As Boolean = False
    Public Vehicle_Weight As Decimal
    'DATE : 24-JAN-2017 > CLIENT : SAHAYOG
    Public Veh_Insurance_No As String = Nothing
    Public Veh_Insurance_Date As String = Nothing
    Public Veh_Fitness_No As String = Nothing
    Public Veh_Fitness_Date As String = Nothing
    Public Two_Way As Boolean
    Public Effective_Start_Date As DateTime? = Nothing
    Public Vehicle As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal obj As clsfrmPrimaryTransporterVehicalMaster) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(strCode, isNewEntry, obj, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal obj As clsfrmPrimaryTransporterVehicalMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            obj.docno = strCode
            If Not isNewEntry Then
                Dim settApplyEffectiveStartDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, trans)) > 0, True, False)
                If settApplyEffectiveStartDate Then
                    Dim qry As String = "select 1 from TSPL_PRIMARY_VEHICLE_MASTER where vehicle_code='" + strCode + "' and Effective_Start_Date ='" + clsCommon.GetPrintDate(obj.Effective_Start_Date, "dd/MMM/yyyy") + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Dim Hist_Version As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select max(Hist_Version) from TSPL_PRIMARY_VEHICLE_MASTER_ESD where vehicle_code='" + strCode + "'", trans))
                        Hist_Version += 1
                        Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_PRIMARY_VEHICLE_MASTER", trans)
                        qry = "INSERT INTO TSPL_PRIMARY_VEHICLE_MASTER_ESD(" + strInvColumns + ",Hist_By,Hist_On,Hist_Version) SELECT " + strInvColumns + ",'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + clsCommon.myCstr(Hist_Version) + "' FROM TSPL_Primary_Vehicle_Master WHERE vehicle_code='" + strCode + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "vehicle_code", strCode)
            clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.primarycode)
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.mcccode)
            clsCommon.AddColumnsForChange(coll, "Storage_Capacity", obj.capacity)
            clsCommon.AddColumnsForChange(coll, "Manufacturing_Year", obj.year)
            clsCommon.AddColumnsForChange(coll, "Price_KM", obj.pricekm) 'txt_km
            clsCommon.AddColumnsForChange(coll, "Rate_Type", clsCommon.myCstr(obj.Rate_Type))
            clsCommon.AddColumnsForChange(coll, "Price_Ltr_KG", clsCommon.myCdbl(obj.Price_Ltr_KG))
            clsCommon.AddColumnsForChange(coll, "Shift_Charges", obj.chagrshift) 'txt_chrg
            clsCommon.AddColumnsForChange(coll, "Avg_Km_Ltr", obj.avgrate) 'txtavgkm
            clsCommon.AddColumnsForChange(coll, "Diesel_Rate", obj.dieselrate) 'txtdiesel
            clsCommon.AddColumnsForChange(coll, "Rental_type", clsCommon.myCstr(obj.RentalType))
            clsCommon.AddColumnsForChange(coll, "Rental_Amount", clsCommon.myCdbl(obj.RentalAmount))
            clsCommon.AddColumnsForChange(coll, "From_1_to_10", obj.From1to10) 'Txt1to10
            clsCommon.AddColumnsForChange(coll, "From_10_to_20", obj.From10to20) 'Txt10to20
            clsCommon.AddColumnsForChange(coll, "Above_20", obj.Above20) 'TxtAbove20
            clsCommon.AddColumnsForChange(coll, "Two_Way", IIf(obj.Two_Way, 1, 0))
            clsCommon.AddColumnsForChange(coll, "STATUS", obj.status)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
            clsCommon.AddColumnsForChange(coll, "Is_Additional", IIf(obj.Is_Additional = True, "T", "F"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Weight", obj.Vehicle_Weight)
            'DATE : 24-JAN-2017 > CLIENT : SAHAYOG
            clsCommon.AddColumnsForChange(coll, "Veh_Insurance_No", clsCommon.myCstr(obj.Veh_Insurance_No))
            clsCommon.AddColumnsForChange(coll, "Veh_Insurance_Date", clsCommon.myCstr(obj.Veh_Insurance_Date))
            clsCommon.AddColumnsForChange(coll, "Veh_Fitness_No", clsCommon.myCstr(obj.Veh_Fitness_No))
            clsCommon.AddColumnsForChange(coll, "Veh_Fitness_Date", clsCommon.myCstr(obj.Veh_Fitness_Date))
            If obj.Effective_Start_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Effective_Start_Date", clsCommon.GetPrintDate(obj.Effective_Start_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Effective_Start_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Vehicle", clsCommon.myCstr(obj.Vehicle), True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Primary_Vehicle_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Primary_Vehicle_Master", OMInsertOrUpdate.Update, " TSPL_Primary_Vehicle_Master.vehicle_code='" + strCode + "'", trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_Primary_Vehicle_Master", "vehicle_code", trans)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As clsfrmPrimaryTransporterVehicalMaster
        Dim obj As New clsfrmPrimaryTransporterVehicalMaster()
        Try
            Dim whrcls As String = ""
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " and TSPL_PRIMARY_VEHICLE_MASTER.mcc_code in (" + arrLoc + ")"
            End If
            Dim qry As String = "select Vehicle_Weight,From_1_To_10,From_10_To_20,Above_20,Shift_Charges,Avg_Km_Ltr,Diesel_Rate,Rental_Type,Rental_Amount" _
            & " ,TSPL_Primary_Vehicle_Master.vehicle_code,TSPL_Primary_Vehicle_Master.description,TSPL_Primary_Vehicle_Master.vendor_code,TSPL_Primary_Vehicle_Master.mcc_code" _
            & " ,TSPL_Primary_Vehicle_Master.Status,TSPL_Primary_Vehicle_Master.storage_capacity,TSPL_Primary_Vehicle_Master.manufacturing_year," _
            & " TSPL_Primary_Vehicle_Master.price_km,TSPL_Primary_Vehicle_Master.price_ltr_kg,Is_Additional,TSPL_Primary_Vehicle_Master.Rate_type,tspl_vendor_master.vendor_name," _
            & " tspl_mcc_master.mcc_name,TSPL_MCC_ROUTE_MASTER.route_Code,TSPL_MCC_ROUTE_MASTER.route_Name ," _
            & " TSPL_Primary_Vehicle_Master.Veh_Fitness_No , TSPL_Primary_Vehicle_Master.Veh_Fitness_Date , TSPL_Primary_Vehicle_Master.Veh_Insurance_No,TSPL_Primary_Vehicle_Master.Veh_Insurance_Date,TSPL_Primary_Vehicle_Master.Two_Way ,TSPL_Primary_Vehicle_Master                        .Effective_Start_Date" _
            & " ,TSPL_Primary_Vehicle_Master.Vehicle from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code" _
            & " and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code " _
            & " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Vehicle_Code=TSPL_Primary_Vehicle_Master.Vehicle_Code"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_Primary_Vehicle_Master.vehicle_code='" + strCode + "' " + whrcls + ""
                Case NavigatorType.First
                    qry += " where TSPL_Primary_Vehicle_Master.vehicle_code in (select min(vehicle_code) from TSPL_Primary_Vehicle_Master where 2=2 " + whrcls + ")"
                Case NavigatorType.Last
                    qry += " where TSPL_Primary_Vehicle_Master.vehicle_code in (select max(vehicle_code) from TSPL_Primary_Vehicle_Master where 2=2 " + whrcls + ")"
                Case NavigatorType.Next
                    qry += " where TSPL_Primary_Vehicle_Master.vehicle_code in (select min(vehicle_code) from TSPL_Primary_Vehicle_Master where vehicle_code>'" + strCode + "' " + whrcls + ")"
                Case NavigatorType.Previous
                    qry += " where TSPL_Primary_Vehicle_Master.vehicle_code in (select max(vehicle_code) from TSPL_Primary_Vehicle_Master where vehicle_code<'" + strCode + "' " + whrcls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsfrmPrimaryTransporterVehicalMaster()
                obj.docno = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
                obj.desc = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.primarycode = clsCommon.myCstr(dt.Rows(0)("vendor_code"))
                obj.primaryname = clsCommon.myCstr(dt.Rows(0)("vendor_name"))
                obj.mcccode = clsCommon.myCstr(dt.Rows(0)("mcc_code"))
                obj.mccname = clsCommon.myCstr(dt.Rows(0)("mcc_name"))
                obj.capacity = clsCommon.myCdbl(dt.Rows(0)("storage_capacity"))
                obj.year = clsCommon.myCstr(dt.Rows(0)("manufacturing_year"))
                obj.pricekm = clsCommon.myCdbl(dt.Rows(0)("price_km"))
                obj.Price_Ltr_KG = clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG"))
                obj.Rate_Type = clsCommon.myCstr(dt.Rows(0)("Rate_Type"))
                obj.chagrshift = clsCommon.myCdbl(dt.Rows(0)("Shift_Charges"))
                obj.avgrate = clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))
                obj.dieselrate = clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))
                obj.Two_Way = (clsCommon.myCdbl(dt.Rows(0)("Two_Way")) = 1)
                obj.RentalType = clsCommon.myCstr(dt.Rows(0)("Rental_Type"))
                obj.RentalAmount = clsCommon.myCdbl(dt.Rows(0)("Rental_Amount"))
                obj.From1to10 = clsCommon.myCdbl(dt.Rows(0)("From_1_To_10"))
                obj.From10to20 = clsCommon.myCdbl(dt.Rows(0)("From_10_To_20"))
                obj.Above20 = clsCommon.myCdbl(dt.Rows(0)("Above_20"))
                obj.status = clsCommon.myCstr(dt.Rows(0)("Status"))
                obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
                obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("Route_Name"))
                obj.Vehicle_Weight = clsCommon.myCdbl(dt.Rows(0)("Vehicle_Weight"))
                obj.Is_Additional = IIf(clsCommon.myCstr(dt.Rows(0)("Is_Additional")) = "T", True, False)
                'DATE : 25-JAN-2017 > CLIENT : SAHAYOG 
                obj.Veh_Fitness_No = clsCommon.myCstr(dt.Rows(0)("Veh_Fitness_No"))
                obj.Veh_Fitness_Date = clsCommon.myCstr(dt.Rows(0)("Veh_Fitness_Date"))
                obj.Veh_Insurance_No = clsCommon.myCstr(dt.Rows(0)("Veh_Insurance_No"))
                obj.Veh_Insurance_Date = clsCommon.myCstr(dt.Rows(0)("Veh_Insurance_Date"))
                If dt.Rows(0)("Effective_Start_Date") IsNot DBNull.Value Then
                    obj.Effective_Start_Date = clsCommon.myCDate(dt.Rows(0)("Effective_Start_Date"))
                End If
                obj.Vehicle = clsCommon.myCstr(dt.Rows(0)("Vehicle"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
End Class

Imports common
Imports System.Data.SqlClient
'' Changed by Panch Raj against ticket No: BM00000009871
Public Class clsfrmMilkRouteMaster


#Region "Variables"
    Public code As String = ""
    Public desc As String = Nothing
    Public Short_Description As String = Nothing
    Public vehiclecode As String = Nothing
    Public vehiclename As String = Nothing
    Public mcccode As String = Nothing
    Public mccname As String = Nothing
    Public supervisorname As String = Nothing
    Public contactno As String = Nothing
    Public add1 As String = Nothing
    Public add2 As String = Nothing
    Public add3 As String = Nothing
    Public countrycode As String = Nothing
    Public countryname As String = Nothing
    Public statecode As String = Nothing
    Public statename As String = Nothing
    Public citycode As String = Nothing
    Public cityname As String = Nothing
    Public email As String = Nothing
    Public effectivedate As String = Nothing
    Public Active As Integer = 1              '' added by Rohit against ticket no: BM00000004431
    Public Self_Route As Boolean = False
    Public kilometer As Decimal = Nothing

    Public kilometer_Morning As Decimal = Nothing
    Public kilometer_Evening As Decimal = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public Last_VLC_To_MCC_Distance As Decimal = Nothing
    Public MCC_Reaching_Time_M As DateTime?
    Public MCC_Reaching_Time_E As DateTime?
    Public Effective_Start_Date As DateTime? = Nothing
    Public Shared arr_VLC_Detail As List(Of cls_VLC_Route_Detail)

#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Name],TSPL_MCC_ROUTE_MASTER.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name],TSPL_MCC_ROUTE_MASTER.KiloMeter,TSPL_MCC_ROUTE_MASTER.supervisor_name as [Supervisor Name],TSPL_MCC_ROUTE_MASTER.contact_no as [Contact No],(TSPL_MCC_ROUTE_MASTER.add1+' '+TSPL_MCC_ROUTE_MASTER.add2+' '+TSPL_MCC_ROUTE_MASTER.add3) as Address,TSPL_MCC_ROUTE_MASTER.country_code as [Country Code],tspl_country_master.country_name as [Country Name],TSPL_MCC_ROUTE_MASTER.state_code as [State Code],tspl_state_master.state_name as [State Name],TSPL_MCC_ROUTE_MASTER.city_code as [City Code],tspl_city_master.city_name as [City Name],TSPL_MCC_ROUTE_MASTER.email_id as [Email ID],TSPL_MCC_ROUTE_MASTER.effective_date as [Effective Date],TSPL_MCC_ROUTE_MASTER.created_By as [Created By],TSPL_MCC_ROUTE_MASTER.created_Date as [Created Date],TSPL_MCC_ROUTE_MASTER.modified_by as [Modified By],TSPL_MCC_ROUTE_MASTER.modified_date as [Modified Date] from TSPL_MCC_ROUTE_MASTER left outer join tspl_mcc_master on Tspl_mcc_master.mcc_code=TSPL_MCC_ROUTE_MASTER.mcc_code left outer join tspl_country_master on TSPL_MCC_ROUTE_MASTER.country_code=tspl_country_master.country_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_MCC_ROUTE_MASTER.state_code left outer join tspl_city_master on tspl_city_master.city_code=TSPL_MCC_ROUTE_MASTER.city_code LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code"
            str = clsCommon.ShowSelectForm("MCCMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "tspl_mcc_route_master.Created_Date")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function


    Public Shared Function SaveData(ByVal strCode As String, ByVal obj As clsfrmMilkRouteMaster, ByVal isNewEntry As Boolean, Optional ByVal isSaveHeaderDataOnly As Boolean = False) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(strCode, trans, obj, isNewEntry, isSaveHeaderDataOnly)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal obj As clsfrmMilkRouteMaster, ByVal isNewEntry As Boolean, ByVal isSaveHeaderDataOnly As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            If Not isNewEntry Then
                Dim settApplyEffectiveStartDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, trans)) > 0, True, False)
                If settApplyEffectiveStartDate Then
                    Dim qry As String = "select 1 from TSPL_MCC_ROUTE_MASTER where route_code='" + obj.code + "' and Effective_Start_Date ='" + clsCommon.GetPrintDate(obj.Effective_Start_Date, "dd/MMM/yyyy") + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Dim Hist_Version As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select max(Hist_Version) from TSPL_MCC_ROUTE_MASTER_ESD where Route_Code='" + obj.code + "'", trans))
                        Hist_Version += 1
                        Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_MCC_ROUTE_MASTER", trans)
                        qry = "INSERT INTO TSPL_MCC_ROUTE_MASTER_ESD(" + strInvColumns + ",Hist_By,Hist_On,Hist_Version) SELECT " + strInvColumns + ",'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + clsCommon.myCstr(Hist_Version) + "' FROM TSPL_MCC_ROUTE_MASTER WHERE Route_Code='" + obj.code + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        strInvColumns = clsERPFuncationality.GetTableColumnNameForQry("TSPL_MCC_ROUTE_VLC_MAPPING", trans)
                        qry = "INSERT INTO TSPL_MCC_ROUTE_VLC_MAPPING_ESD(" + strInvColumns + ",Hist_Version) SELECT " + strInvColumns + ",'" + clsCommon.myCstr(Hist_Version) + "' FROM TSPL_MCC_ROUTE_VLC_MAPPING WHERE Route_CODE='" + obj.code + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
            End If


            UpdateEditLog(obj, trans)

            If isNewEntry Then
                obj.code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy"), clsDocType.MILKROUTEMASTER, "", ""))
                obj.code = obj.mcccode + "/" + obj.code
                strCode = obj.code
            End If

            Dim coll As New Hashtable()
            obj.code = strCode
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "route_code", obj.code)
            clsCommon.AddColumnsForChange(coll, "route_name", obj.desc)
            clsCommon.AddColumnsForChange(coll, "Short_Description", obj.Short_Description)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.vehiclecode)
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.mcccode)
            clsCommon.AddColumnsForChange(coll, "Kilometer", obj.kilometer)
            clsCommon.AddColumnsForChange(coll, "kilometer_Morning", obj.kilometer_Morning)
            clsCommon.AddColumnsForChange(coll, "kilometer_Evening", obj.kilometer_Evening)
            clsCommon.AddColumnsForChange(coll, "Supervisor_Name", obj.supervisorname)
            clsCommon.AddColumnsForChange(coll, "Contact_No", obj.contactno)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.add1, True)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.add2, True)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.add3, True)
            clsCommon.AddColumnsForChange(coll, "Country_Code", obj.countrycode, True)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.statecode, True)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.citycode, True)
            clsCommon.AddColumnsForChange(coll, "Active", obj.Active)
            clsCommon.AddColumnsForChange(coll, "Self_Route", IIf(obj.Self_Route, 1, 0))
            'If obj.Active = 0 Then
            '    clsCommon.AddColumnsForChange(coll, "InActive_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
            'End If
            clsCommon.AddColumnsForChange(coll, "Email_Id", obj.email)

            clsCommon.AddColumnsForChange(coll, "Last_VLC_To_MCC_Distance", obj.Last_VLC_To_MCC_Distance)
            If clsCommon.myLen(obj.MCC_Reaching_Time_M) > 0 Then
                clsCommon.AddColumnsForChange(coll, "MCC_Reaching_Time_M", clsCommon.GetPrintDate(obj.MCC_Reaching_Time_M, "dd/MMM/yyyy hh:mm tt"))
            End If
            If clsCommon.myLen(obj.MCC_Reaching_Time_E) > 0 Then
                clsCommon.AddColumnsForChange(coll, "MCC_Reaching_Time_E", clsCommon.GetPrintDate(obj.MCC_Reaching_Time_E, "dd/MMM/yyyy hh:mm tt"))
            End If
            If clsCommon.myLen(obj.effectivedate) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(obj.effectivedate, "dd/MMM/yyyy"))
            End If
            If obj.Effective_Start_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Effective_Start_Date", clsCommon.GetPrintDate(obj.Effective_Start_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Effective_Start_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_mcc_route_master", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_mcc_route_master", OMInsertOrUpdate.Update, " tspl_mcc_route_master.route_code='" + obj.code + "'", trans)
            End If
            If Not isSaveHeaderDataOnly Then
                isSaved = isSaved AndAlso cls_VLC_Route_Detail.SaveData(obj.code, clsfrmMilkRouteMaster.arr_VLC_Detail, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.code, obj.arrCustomFields, trans)
            End If
            isSaved = isSaved AndAlso clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.code, "TSPL_MCC_ROUTE_MASTER", "Route_Code", trans)
            'trans.Commit()
            'Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function UpdateEditLog(ByVal obj As clsfrmMilkRouteMaster, ByVal trans As SqlTransaction)
        Try
            Dim squery As String
            squery = "select active from tspl_mcc_route_master where  tspl_mcc_route_master.route_code='" + obj.code + "'"
            Dim active_state As Integer = clsDBFuncationality.getSingleValue(squery, trans)
            If obj.Active <> active_state Then
                squery = "update tspl_mcc_route_master set inactive_date='" & clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")) & "',edit_log = cast(edit_log as varchar(mAX)) + '    '   + cast( '" & clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy hh:mm:ss")) & " -  " & IIf(obj.Active = 0, "Inactive", "Active") & "' as varchar) where  tspl_mcc_route_master.route_code='" + obj.code + "'"
                clsDBFuncationality.ExecuteNonQuery(squery, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetName(ByVal RouteCode As String, ByVal trans As SqlTransaction) As String
        Dim RouteName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select route_name from tspl_mcc_route_master where Route_Code='" & RouteCode & "'", trans))
        Return RouteName
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As clsfrmMilkRouteMaster
        Try
            Dim whrcls As String = ""
            Dim objtr As New cls_VLC_Route_Detail

            arr_VLC_Detail = New List(Of cls_VLC_Route_Detail)

            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " and TSPL_MCC_ROUTE_MASTER.mcc_code in (" + arrLoc + ")"
            Else
                whrcls = ""
            End If

            Dim qry As String = "select vm.vehicle_code as [Vehicle Code],vm.Description as [Vehicle Name], TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Name],TSPL_MCC_ROUTE_MASTER.Short_Description,TSPL_MCC_ROUTE_MASTER.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],TSPL_MCC_ROUTE_MASTER.kilometer,TSPL_MCC_ROUTE_MASTER.kilometer_Morning,TSPL_MCC_ROUTE_MASTER.kilometer_Evening,TSPL_MCC_ROUTE_MASTER.supervisor_name as [Supervisor Name],TSPL_MCC_ROUTE_MASTER.contact_no as [Contact No],TSPL_MCC_ROUTE_MASTER.add1 as Address1,TSPL_MCC_ROUTE_MASTER.add2 as Address2,TSPL_MCC_ROUTE_MASTER.add3 as Address3,TSPL_MCC_ROUTE_MASTER.country_code as [Country Code],tspl_country_master.country_name as [Country Name],TSPL_MCC_ROUTE_MASTER.state_code as [State Code],tspl_state_master.state_name as [State Name],TSPL_MCC_ROUTE_MASTER.city_code as [City Code],tspl_city_master.city_name as [City Name],TSPL_MCC_ROUTE_MASTER.email_id as [Email ID],TSPL_MCC_ROUTE_MASTER.effective_date as [Effective Date],TSPL_MCC_ROUTE_MASTER.Active,TSPL_MCC_ROUTE_MASTER.Self_Route,TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M,TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E,TSPL_MCC_ROUTE_MASTER.Last_VLC_To_MCC_Distance,TSPL_MCC_ROUTE_MASTER.Effective_Start_Date 
from TSPL_MCC_ROUTE_MASTER 
left outer join tspl_mcc_master on Tspl_mcc_master.mcc_code=TSPL_MCC_ROUTE_MASTER.mcc_code 
left outer join tspl_country_master on TSPL_MCC_ROUTE_MASTER.country_code=tspl_country_master.country_code 
left outer join tspl_state_master on tspl_state_master.state_code=TSPL_MCC_ROUTE_MASTER.state_code 
left outer join tspl_city_master on tspl_city_master.city_code=TSPL_MCC_ROUTE_MASTER.city_code 
left join TSPL_Primary_VEHICLE_MASTER vm on vm.Vehicle_code=TSPL_MCC_ROUTE_MASTER.vehicle_code"
            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_MCC_ROUTE_MASTER.route_code='" + strCode + "' " + whrcls + ""
                Case NavigatorType.First
                    qry += " where TSPL_MCC_ROUTE_MASTER.route_code in (select min(route_code) from TSPL_MCC_ROUTE_MASTER where 1=1 " + whrcls + ")"
                Case NavigatorType.Last
                    qry += " where TSPL_MCC_ROUTE_MASTER.route_code in (select max(route_code) from TSPL_MCC_ROUTE_MASTER where 1=1 " + whrcls + ")"
                Case NavigatorType.Previous
                    qry += " where TSPL_MCC_ROUTE_MASTER.route_code in (select max(route_code) from TSPL_MCC_ROUTE_MASTER where route_code<'" + strCode + "' " + whrcls + ")"
                Case NavigatorType.Next
                    qry += " where TSPL_MCC_ROUTE_MASTER.route_code in (select min(route_code) from TSPL_MCC_ROUTE_MASTER where route_code>'" + strCode + "' " + whrcls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim obj As clsfrmMilkRouteMaster = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsfrmMilkRouteMaster()

                obj.code = clsCommon.myCstr(dt.Rows(0)("code"))
                obj.desc = clsCommon.myCstr(dt.Rows(0)("Route Name"))
                obj.Short_Description = clsCommon.myCstr(dt.Rows(0)("Short_Description"))
                obj.vehiclecode = clsCommon.myCstr(dt.Rows(0)("Vehicle Code"))
                obj.vehiclename = clsCommon.myCstr(dt.Rows(0)("Vehicle Name"))
                obj.mcccode = clsCommon.myCstr(dt.Rows(0)("MCC Code"))
                obj.mccname = clsCommon.myCstr(dt.Rows(0)("MCC Name"))
                obj.kilometer = clsCommon.myCdbl(dt.Rows(0)("Kilometer"))
                obj.kilometer_Morning = clsCommon.myCdbl(dt.Rows(0)("kilometer_Morning"))
                obj.kilometer_Evening = clsCommon.myCdbl(dt.Rows(0)("kilometer_Evening"))

                obj.supervisorname = clsCommon.myCstr(dt.Rows(0)("Supervisor Name"))
                obj.contactno = clsCommon.myCstr(dt.Rows(0)("Contact No"))
                obj.add1 = clsCommon.myCstr(dt.Rows(0)("Address1"))
                obj.add2 = clsCommon.myCstr(dt.Rows(0)("Address2"))
                obj.add3 = clsCommon.myCstr(dt.Rows(0)("Address3"))
                obj.countrycode = clsCommon.myCstr(dt.Rows(0)("Country Code"))
                obj.countryname = clsCommon.myCstr(dt.Rows(0)("Country Name"))
                obj.statecode = clsCommon.myCstr(dt.Rows(0)("State Code"))
                obj.statename = clsCommon.myCstr(dt.Rows(0)("State Name"))
                obj.citycode = clsCommon.myCstr(dt.Rows(0)("City Code"))
                obj.cityname = clsCommon.myCstr(dt.Rows(0)("City Name"))
                obj.email = clsCommon.myCstr(dt.Rows(0)("Email ID"))
                obj.effectivedate = clsCommon.myCstr(dt.Rows(0)("Effective Date"))
                obj.Active = clsCommon.myCstr(dt.Rows(0)("Active"))
                obj.Self_Route = (clsCommon.myCDecimal(dt.Rows(0)("Self_Route")) = 1)
                If dt.Rows(0)("MCC_Reaching_Time_M") IsNot DBNull.Value Then
                    obj.MCC_Reaching_Time_M = clsCommon.myCDate(dt.Rows(0)("MCC_Reaching_Time_M"))
                End If
                If dt.Rows(0)("MCC_Reaching_Time_E") IsNot DBNull.Value Then
                    obj.MCC_Reaching_Time_E = clsCommon.myCDate(dt.Rows(0)("MCC_Reaching_Time_E"))
                End If
                If dt.Rows(0)("Effective_Start_Date") IsNot DBNull.Value Then
                    obj.Effective_Start_Date = clsCommon.myCDate(dt.Rows(0)("Effective_Start_Date"))
                End If
                obj.Last_VLC_To_MCC_Distance = clsCommon.myCdbl(dt.Rows(0)("Last_VLC_To_MCC_Distance"))
                '' query changed by Panch raj against ticket no: KDI/21/05/18-000323. inactive vlc mapping must not be loaded in grid
                qry = "select  TSPL_MCC_ROUTE_VLC_MAPPING.Out_Route_KM,TSPL_MCC_ROUTE_VLC_MAPPING.Out_Route,TSPL_MCC_ROUTE_VLC_MAPPING.SNo, TSPL_VLC_MASTER_HEAD.*,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_VLC_MAPPING.Distance,TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll as Mor_Mik_Coll_New,TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll as Eve_Milk_Coll_New,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_UPLOADER " +
                    " from TSPL_MCC_ROUTE_VLC_MAPPING left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.vlc_code=TSPL_MCC_ROUTE_VLC_MAPPING.vlc_code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code where TSPL_MCC_ROUTE_VLC_MAPPING.route_Code='" & obj.code & "' and TSPL_MCC_ROUTE_VLC_MAPPING.Is_Active=1 order by TSPL_MCC_ROUTE_VLC_MAPPING.SNo"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)

                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        objtr = New cls_VLC_Route_Detail
                        objtr.Route_CODE = strCode
                        objtr.VLC_Uploader_code = clsCommon.myCstr(dr("VLC_CODE_VLC_UPLOADER"))
                        objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                        objtr.VLC_NAME = clsCommon.myCstr(dr("VLC_Name"))
                        objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                        objtr.VSP_NAME = clsCommon.myCstr(dr("Vendor_name"))
                        objtr.Status = IIf(clsCommon.myCstr(dr("Active")) = "1", "Open", "Closed")
                        objtr.VEHICLE_NAME = clsCommon.myCstr(dr("VEHICaL_name"))
                        objtr.Opening_Date = clsCommon.myCstr(dr("Created_Date"))

                        objtr.SNo = clsCommon.myCstr(dr("SNo"))
                        objtr.Distance = clsCommon.myCdbl(dr("Distance"))
                        If dr("Mor_Mik_Coll_New") IsNot DBNull.Value Then
                            objtr.Mor_Mik_Coll = clsCommon.myCDate(dr("Mor_Mik_Coll_New"))
                        End If
                        If dr("Eve_Milk_Coll_New") IsNot DBNull.Value Then
                            objtr.Eve_Milk_Coll = clsCommon.myCDate(dr("Eve_Milk_Coll_New"))
                        End If
                        If clsCommon.CompairString(objtr.Status, "Closed") = CompairStringResult.Equal Then
                            objtr.Closing_Date = clsCommon.myCstr(dr("Modified_Date"))
                        End If
                        objtr.Out_Route = (clsCommon.myCdbl(dr("Out_Route")) = 1)
                        objtr.Out_Route_KM = clsCommon.myCdbl(dr("Out_Route_KM"))
                        arr_VLC_Detail.Add(objtr)
                    Next
                End If
                clsfrmMilkRouteMaster.arr_VLC_Detail = arr_VLC_Detail
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CheckSequenceOfVLC(ByVal RouteCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select SNo from TSPL_MCC_ROUTE_VLC_MAPPING where route_code='" + RouteCode + "' order by sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If clsCommon.myCdbl(dt.Rows(ii)(0)) <> (ii + 1) Then
                    Throw New Exception("Sequence No of VLC is not correct for route no " + RouteCode)
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Sub DeleteVlc(ByVal vlc_Code As String, ByVal Route_Code As String)
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim sQuery As String = "delete from TSPL_MCC_ROUTE_VLC_MAPPING where route_Code='" & Route_Code & "' and vlc_Code='" & vlc_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)

            sQuery = "update TSPL_VLC_MASTER_HEAD set route_code='' where vlc_code='" & vlc_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
End Class

'Public Class clsMCCCodes
'#Region "Variables"
'    Public arrLocCodes As String = Nothing
'    Public LocType As String = Nothing
'    Shared xvalues As String = Nothing
'    Public Default_LocCode As String = Nothing
'    Public Default_LocName As String = Nothing
'    Public Default_HO As Boolean = Nothing
'#End Region

'    Public Shared Function GetData(ByVal trans As SqlTransaction, Optional ByVal isMCC As Boolean = False) As clsMCCCodes
'        Try
'            Dim obj As New clsMCCCodes()
'            obj.arrLocCodes = ""

'            Dim qry As String = "select tspl_location_master.location_category,tspl_user_master.default_location from tspl_user_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
'            Dim dt As DataTable = (clsDBFuncationality.GetDataTable(qry, trans))

'            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'                For Each dr As DataRow In dt.Rows
'                    xvalues = clsCommon.myCstr(dr("default_location"))
'                    obj.Default_LocCode = clsCommon.myCstr(dr("default_location"))
'                    obj.LocType = clsCommon.myCstr(dr("location_category"))

'                    obj.arrLocCodes = obj.arrLocCodes + "','" + xvalues
'                Next
'            End If

'            obj.Default_LocName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.Default_LocCode + "'", trans))
'            'obj.Default_HO = True
'            '--------------if default location is MCC then check whether user is mapped with location segment
'            '----------------if mapped,then pick all that locations which are mapped with that segment code.
'            'If clsCommon.CompairString(obj.LocType, "MCC") = CompairStringResult.Equal Then
'            obj.Default_HO = False
'            qry = "select TSPL_LOCATION_MASTER.Location_Code,TSPL_GL_SEGMENT_PERMISSION.GL_Segment,TSPL_GL_SEGMENT_PERMISSION.Segment_Code from TSPL_LOCATION_MASTER left outer join TSPL_GL_SEGMENT_PERMISSION on TSPL_GL_SEGMENT_PERMISSION.Segment_Code=TSPL_LOCATION_MASTER.Loc_Segment_Code where TSPL_GL_SEGMENT_PERMISSION.User_Code='" + objCommonVar.CurrentUserCode + "' and TSPL_GL_SEGMENT_PERMISSION.GL_Segment in (select Seg_No from TSPL_GL_SEGMENT_CODE where Seg_no='7') and tspl_location_master.location_code<>'" + xvalues + "'"
'            dt = New DataTable()
'            dt = clsDBFuncationality.GetDataTable(qry, trans)

'            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'                For Each dr As DataRow In dt.Rows
'                    xvalues = clsCommon.myCstr(dr("Location_Code"))

'                    obj.arrLocCodes = obj.arrLocCodes + "','" + xvalues
'                Next
'            End If
'            'Else
'            'obj.Default_HO = True
'            'obj.arrLocCodes = ""
'            'End If

'            If clsCommon.myLen(obj.arrLocCodes) > 0 Then
'                obj.arrLocCodes = obj.arrLocCodes + "'"
'                If obj.arrLocCodes.Substring(0, 3) = "','" Then
'                    obj.arrLocCodes = obj.arrLocCodes.Substring(2, obj.arrLocCodes.Length - 2)
'                End If
'            End If

'            If isMCC = True Then
'                qry = "select * from tspl_mcc_master where mcc_code='" & obj.Default_LocCode & "'"
'                dt = clsDBFuncationality.GetDataTable(qry, trans)
'                If dt.Rows.Count <= 0 Then
'                    obj.Default_LocCode = "_"
'                    obj.Default_LocName = "_"
'                End If
'            End If

'            Return obj

'        Catch ex As Exception
'            Throw New Exception(ex.Message)
'        End Try
'    End Function

'    Public Shared Function GetData(Optional ByVal isMCC As Boolean = False) As clsMCCCodes
'        Try
'            Dim obj As New clsMCCCodes()
'            obj.arrLocCodes = ""

'            Dim qry As String = "select tspl_location_master.location_category,tspl_user_master.default_location from tspl_user_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
'            Dim dt As DataTable = (clsDBFuncationality.GetDataTable(qry))

'            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'                For Each dr As DataRow In dt.Rows
'                    xvalues = clsCommon.myCstr(dr("default_location"))
'                    obj.Default_LocCode = clsCommon.myCstr(dr("default_location"))
'                    obj.LocType = clsCommon.myCstr(dr("location_category"))

'                    obj.arrLocCodes = obj.arrLocCodes + "','" + xvalues
'                Next
'            End If

'            obj.Default_LocName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.Default_LocCode + "'"))
'            'obj.Default_HO = True
'            '--------------if default location is MCC then check whether user is mapped with location segment
'            '----------------if mapped,then pick all that locations which are mapped with that segment code.
'            'If clsCommon.CompairString(obj.LocType, "MCC") = CompairStringResult.Equal Then
'            obj.Default_HO = False
'            qry = "select TSPL_LOCATION_MASTER.Location_Code,TSPL_GL_SEGMENT_PERMISSION.GL_Segment,TSPL_GL_SEGMENT_PERMISSION.Segment_Code from TSPL_LOCATION_MASTER left outer join TSPL_GL_SEGMENT_PERMISSION on TSPL_GL_SEGMENT_PERMISSION.Segment_Code=TSPL_LOCATION_MASTER.Loc_Segment_Code where TSPL_GL_SEGMENT_PERMISSION.User_Code='" + objCommonVar.CurrentUserCode + "' and TSPL_GL_SEGMENT_PERMISSION.GL_Segment in (select Seg_No from TSPL_GL_SEGMENT_CODE where Seg_no='7') and tspl_location_master.location_code<>'" + xvalues + "'"
'            dt = New DataTable()
'            dt = clsDBFuncationality.GetDataTable(qry)

'            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
'                For Each dr As DataRow In dt.Rows
'                    xvalues = clsCommon.myCstr(dr("Location_Code"))

'                    obj.arrLocCodes = obj.arrLocCodes + "','" + xvalues
'                Next
'            End If
'            'Else
'            'obj.Default_HO = True
'            'obj.arrLocCodes = ""
'            'End If

'                If clsCommon.myLen(obj.arrLocCodes) > 0 Then
'                obj.arrLocCodes = obj.arrLocCodes + "'"
'                If obj.arrLocCodes.Substring(0, 3) = "','" Then
'                    obj.arrLocCodes = obj.arrLocCodes.Substring(2, obj.arrLocCodes.Length - 2)
'                End If
'            End If

'            If isMCC = True Then
'                qry = "select * from tspl_mcc_master where mcc_code='" & obj.Default_LocCode & "'"
'                dt = clsDBFuncationality.GetDataTable(qry)
'                If dt.Rows.Count <= 0 Then
'                    obj.Default_LocCode = "_"
'                    obj.Default_LocName = "_"
'                End If
'            End If

'            Return obj

'        Catch ex As Exception
'            Throw New Exception(ex.Message)
'        End Try
'    End Function

'    Public Shared Function checkisMcc(ByVal mcc_code As String)
'        Dim squery As String = "select mcc_code from tspl_mcc_master where mcc_code='" & mcc_code & "'"
'        Dim mcc_code_return As String = clsDBFuncationality.getSingleValue(squery)
'        Return mcc_code_return
'    End Function
'End Class


Public Class cls_VLC_Route_Detail
#Region "Variables"
    Public Route_CODE As String
    Public VLC_Uploader_code As String ''Not a table column
    Public VLC_CODE As String
    Public VLC_NAME As String
    Public VSP_CODE As String
    Public VSP_NAME As String
    Public Status As String
    Public VEHICLE_CODE As String
    Public VEHICLE_NAME As String
    Public Opening_Date As String
    Public Closing_Date As String
    Public SNo As Integer
    Public Distance As Decimal
    Public Mor_Mik_Coll As DateTime? = Nothing
    Public Eve_Milk_Coll As DateTime? = Nothing
    Public Out_Route As Boolean
    Public Out_Route_KM As Decimal
#End Region


    Public Shared Function AddVlcTORoute(ByVal strDocNo As String, ByVal obj As cls_VLC_Route_Detail, ByVal trans As SqlTransaction) As Boolean
        Dim sQuery As String = "delete from TSPL_MCC_ROUTE_VLC_MAPPING where vlc_Code='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        sQuery = "update tspl_vlc_master_head set Route_Code=null where vlc_Code='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
        clsCommon.AddColumnsForChange(coll, "ROute_CODE", obj.Route_CODE)
        clsCommon.AddColumnsForChange(coll, "VLC_CODE", strDocNo)
        clsCommon.AddColumnsForChange(coll, "Is_Active", "1")
        clsCommon.AddColumnsForChange(coll, "Distance", obj.Distance)
        If obj.Mor_Mik_Coll IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "Mor_Mik_Coll", clsCommon.GetPrintDate(obj.Mor_Mik_Coll, "dd/MMM/yyyy hh:mm tt"))
        End If
        If obj.Eve_Milk_Coll IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "Eve_Milk_Coll", clsCommon.GetPrintDate(obj.Eve_Milk_Coll, "dd/MMM/yyyy hh:mm tt"))
        End If
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, "TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE='" + obj.Route_CODE + "'", trans)
        sQuery = "update TSPL_VLC_MASTER_HEAD set route_code='" & obj.Route_CODE & "' where vlc_code='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        Return True
    End Function

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of cls_VLC_Route_Detail), ByVal trans As SqlTransaction) As Boolean
        Dim sQuery As String = "delete from TSPL_MCC_ROUTE_VLC_MAPPING where route_Code='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        sQuery = "update tspl_vlc_master_head set Route_Code=null where Route_Code='" & strDocNo & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As cls_VLC_Route_Detail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "ROute_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
                clsCommon.AddColumnsForChange(coll, "Is_Active", "1")
                clsCommon.AddColumnsForChange(coll, "Distance", obj.Distance)
                clsCommon.AddColumnsForChange(coll, "Out_Route", IIf(obj.Out_Route, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Out_Route_KM", obj.Out_Route_KM)
                If obj.Mor_Mik_Coll IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Mor_Mik_Coll", clsCommon.GetPrintDate(obj.Mor_Mik_Coll, "dd/MMM/yyyy hh:mm tt"))
                End If
                If obj.Eve_Milk_Coll IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Eve_Milk_Coll", clsCommon.GetPrintDate(obj.Eve_Milk_Coll, "dd/MMM/yyyy hh:mm tt"))
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, "TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE='" + strDocNo + "'", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_MCC_ROUTE_VLC_MAPPING", "Route_CODE", trans)
                sQuery = "update TSPL_VLC_MASTER_HEAD set route_code='" & strDocNo & "' where vlc_code='" & obj.VLC_CODE & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            Next
        End If
        Return True
    End Function
End Class

Public Class clsDCSSupervisor
    Public MCC_Code As String = Nothing
    Public arr As List(Of clsDCSSupervisorTagging) = Nothing


    Public Shared Function SaveData(ByVal strCode As String, ByVal obj As clsDCSSupervisor) As Boolean
        Try

            clsDCSSupervisorTagging.SaveData(strCode, obj)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function GetData(ByVal strCode As String) As clsDCSSupervisor
        Try
            Dim whrcls As String = ""
            Dim obj As New clsDCSSupervisor
            Dim arr_VLC_Detail = New List(Of clsDCSSupervisorTagging)
            Dim objtr As New clsDCSSupervisorTagging

            Dim qry As String = "select ROW_NUMBER() OVER(PARTITION BY 1 ORDER BY TSPL_VLC_MASTER_HEAD.VLC_CODE) AS Sno,TSPL_VLC_Supervisor_Tagging.Mcc_code
                ,TSPL_VLC_Supervisor_Tagging.Supervisor_code,TSPL_EMPLOYEE_MASTER.Emp_Name Supervisor_Name, TSPL_VLC_MASTER_HEAD.*
                ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_UPLOADER  
                from TSPL_VLC_MASTER_HEAD 
                left join TSPL_VLC_Supervisor_Tagging on TSPL_VLC_MASTER_HEAD.vlc_code=TSPL_VLC_Supervisor_Tagging.vlc_code
                left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.emp_code=TSPL_VLC_Supervisor_Tagging.Supervisor_code
                where TSPL_VLC_MASTER_HEAD.mcc='" & strCode & "' order by SNo"
            Dim dt As DataTable
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsDCSSupervisorTagging
                    objtr.MCC_Code = clsCommon.myCstr(dr("MCC_Code"))
                    objtr.VLC_Uploader_code = clsCommon.myCstr(dr("VLC_CODE_VLC_UPLOADER"))
                    objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                    objtr.VLC_NAME = clsCommon.myCstr(dr("VLC_Name"))
                    objtr.Supervisor_Code = clsCommon.myCstr(dr("Supervisor_Code"))
                    objtr.Supervisor_Name = clsCommon.myCstr(dr("Supervisor_Name"))
                    objtr.SNo = clsCommon.myCdbl(dr("SNo"))

                    arr_VLC_Detail.Add(objtr)
                Next
            End If
            obj.MCC_Code = strCode
            obj.arr = arr_VLC_Detail

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class

Public Class clsDCSSupervisorTagging
    Public MCC_Code As String = Nothing
    Public VLC_CODE As String = Nothing
    Public Supervisor_Code As String = Nothing
    Public VLC_Uploader_code As String = Nothing
    Public VLC_NAME As String = Nothing
    Public Supervisor_Name As String = Nothing
    Public SNo As Double = 0



    Public Shared Function SaveData(ByVal StrMcc As String, ByVal obj As clsDCSSupervisor) As Boolean
        Dim trans As SqlTransaction = Nothing
        trans = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_VLC_Supervisor_Tagging where MCC_Code='" + StrMcc + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            coll = New Hashtable()

            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                For Each objtr As clsDCSSupervisorTagging In obj.arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "SNo", objtr.SNo)
                    clsCommon.AddColumnsForChange(coll, "MCC_Code", objtr.MCC_Code)
                    clsCommon.AddColumnsForChange(coll, "VLC_CODE", objtr.VLC_CODE)
                    clsCommon.AddColumnsForChange(coll, "Supervisor_Code", objtr.Supervisor_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))



                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_Supervisor_Tagging", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class


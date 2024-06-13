Imports common
Imports System.Data.SqlClient
Imports XpertERPEngineFine
Public Class clsLocation
    Public bankBank As String = Nothing
    Public bankBranch As String = Nothing
    Public bankACType As String = Nothing
    Public bankaccno As String = Nothing
    Public bankifsccode As String = Nothing
    Public accountholdername As String = Nothing
    Public BankUPI_ID As String = Nothing
    Public Is_Registered As Integer = 0
    Public DairyDispatchFromDO As Integer = 0
    Public CSA_Commission_RS_PERS As String = Nothing
    Public Is_Consumption_Location As Integer = Nothing
    Public Vendor_Commsn_ACC As String = ""
    Public Vendor_Commsn_ACC_Desc As String = ""
    Public Loc_code As String = ""
    Public IsParlour As String = "N"
    Public SNO As Integer = 0
    Public Loc_Cat_structr_Code As String = Nothing
    Public Loc_cat_Structr_desc As String = Nothing
    Public Loc_Category_Code As String = ""
    Public Loc_Category_Code_Desc As String = ""
    Public Loc_Cagetory_Values As String = ""
    Public Loc_Cagetory_Values_Desc As String = ""
    Public ArrCategoryStr As New List(Of clsLocation)
    Public ArrLocCustMap As New List(Of clsLocationCustomerMapping)
    Public ArrLocItemMap As New List(Of clsLocationItemMapping)
    Public csa_commision_rate As Decimal = Nothing
    Public csa_commision_type As String = Nothing
    Public UseInJobWork As Integer = 0
    Public TAX1_Rate As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX10_Rate As Double = 0
    Public loc_categry As String = ""
    Public vendrocode As String = ""
    Public vendorname As String = ""
    Public Location_Code As String = ""
    Public Location_Desc As String = ""
    Public Add1 As String = ""
    Public Add2 As String = ""
    Public Add3 As String = ""
    Public Add4 As String = ""
    Public HOAdd1 As String = ""
    Public HOAdd2 As String = ""
    Public City_Code As String = ""
    Public State As String = ""
    Public Pin_Code As String = ""
    Public Country As String = ""
    Public Telphone As String = ""
    Public Email As String = ""
    Public Location_Type As String = ""
    Public Loc_Status As String = ""
    Public Status_Date As String = ""
    Public Excisable As String = ""
    Public Loc_Segment_Code As String = ""
    Public Type As String = ""
    Public Purchase_Tax_Group As String = ""
    Public Sales_Tax_Group As String = ""
    Public Ecc_Number As String = ""
    Public Registration_Number As String = ""
    Public Commissionerate As String = ""
    Public Range_Code As String = ""
    Public Range_Name As String = ""
    Public Range_Address As String = ""
    Public Division_Code As String = ""
    Public Division_Name As String = ""
    Public Division_Address As String = ""
    Public Created_By As String = ""
    Public Created_Date As String = ""
    Public Modify_By As String = ""
    Public Modify_Date As String = ""
    Public Comp_code As String = ""
    Public TIN_No As String = ""
    Public TAN_No As String = ""
    Public TCAN_No As String = ""
    Public Service_Tax_Reg_No As String = ""
    Public DutyPaid As String = ""
    Public Purchase_Tax_GroupIS As String = ""
    Public Sales_Tax_GroupIS As String = ""
    Public Stock_Transfer_Filled_Ac As String = ""
    Public Stock_Transfer_Empty_Ac As String = ""
    Public GIT_Location As String = ""
    Public GIT_Type As String = ""
    Public CST_No As String = ""
    Public Phone1 As String = ""
    Public Phone2 As String = ""
    Public isNewEntry As Boolean = False
    Public CSA_Type As String = Nothing
    Public Rejected_Type As String = Nothing
    Public Rejected_Location As String = Nothing
    Public Cust_Code As String = Nothing
    Public Code As String = ""
    Public Name As String = ""
    Public Is_Section As String = ""
    Public Is_Sub_Location As String = ""
    Public Section_Code As String = ""
    Public Main_Location_Code As String = ""
    Public NearestCity As String = ""
    Public Stock_Transfer_ac As String = ""
    Public Loss_Ac As String = ""
    Public ESIC_No As String = String.Empty
    Public PF_No As String = String.Empty
    Public Arr As List(Of clsLocationWiseTax) = Nothing
    Public arr_JobworkItem As List(Of ClsLocation_JobworkItem) = Nothing
    Public ArrItem As List(Of clsLocationWiseItems) = Nothing
    Public Is_JobWork As String = String.Empty
    Public JobWork_Vendor As String = String.Empty
    Public Jobwork_Item As String = String.Empty
    Public Short_Name As String = Nothing
    Public ArrMappingPlantDepot As List(Of clsLocationPlantDepotMapping) = Nothing
    Public PAN_No As String = Nothing
    Public GSTNO As String = Nothing
    Public GSTEntity As String = Nothing
    Public GSTBlank As String = Nothing
    Public GSTDegit As String = Nothing
    Public Silo_Capacity As Decimal = 0
    Public Is_Insurance As Integer = 0
    Public InsuranceNo As String = ""
    Public InsuranceFromDate As Date?
    Public InsuranceToDate As Date?
    Public IsSubLocationWise As String = ""
    Public IsMainPlant As Integer = 0
    Public MP_Collection_Running_Date As Date? = Nothing
    Public Uploader_No As String = Nothing
    Public No_Of_Shift As Integer = Nothing
    Public QC_IS As String = Nothing
    Public CMA_CML As String = Nothing
    Public ValidUpto As String = Nothing
    Public GradeType As String = Nothing
    Public QC_StartDate As Date? = Nothing
    Public ManagerName As String = Nothing
    Public ManagerDestination As String = Nothing
    Public Remarks As String = Nothing
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        'If clsCommon.myLen(whrcls) > 0 Then
        '    whrcls = whrcls + " and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        'Else
        '    whrcls = " comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        'End If
        'Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code],IsSubLocationWise as [Is Sub Location Wise] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   "
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim whrcl As String = " Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ") "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcl += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        str = clsCommon.ShowSelectForm("LOCMSTFND", qry, "Code", whrcl, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getLocSegFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1,hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C]  from TSPL_Location_MASTER     "
        str = clsCommon.ShowSelectForm("LOCSEGMSTFND", qry, "LocationSegmentCode", whrcls, curcode, "LocationSegmentCode", isButtonClicked)
        Return str
    End Function
    Public Shared Function getLocSegFinderFullRow(ByVal whrcls As String) As DataRow
        Dim str As String = ""
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1,hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C]  from TSPL_Location_MASTER "
        If clsCommon.myLen(whrcls) > 0 Then
            qry += " where " + whrcls
        End If
        Return clsCommon.ShowSelectFormForRow("LOCSEGMSTrf", qry)
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function FinderForPhysicalLoaction(ByVal strCode As String, ByVal isButtonClicked As Boolean) As clsLocation
        Dim obj As clsLocation = Nothing
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = "Location_Type='Physical'"
            strCode = clsCommon.ShowSelectForm("PRLocation", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
            If clsCommon.myLen(strCode) > 0 Then
                qry = "select Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Code ='" + strCode + "' "
                obj = New clsLocation()
                obj.Code = strCode
                obj.Name = clsDBFuncationality.getSingleValue(qry)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function PhysicalLoaction(ByVal strCode As String, ByVal locationCode As String, ByVal isButtonClicked As Boolean) As clsLocation
        Dim obj As clsLocation = Nothing
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = "Location_Type='Physical' and Loc_Segment_Code='" & locationCode & "'"
            strCode = clsCommon.ShowSelectForm("PRLocation", qry, "Code", WhrCls, strCode, "Code", isButtonClicked)
            If clsCommon.myLen(strCode) > 0 Then
                qry = "select Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Code ='" + strCode + "' "
                obj = New clsLocation()
                obj.Code = strCode
                obj.Name = clsDBFuncationality.getSingleValue(qry)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function isLocatinExcisable(ByVal strLocation As String) As Boolean
        Return isLocatinExcisable(strLocation, Nothing)
    End Function
    Public Shared Function isLocatinExcisable(ByVal strLocation As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + strLocation + "'"
            If Not clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry, trans), "T") = CompairStringResult.Equal Then
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetName(ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLocation + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetGITMainLocation(ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select Location_Code from TSPL_LOCATION_MASTER where GIT_Location='" + strLocation + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetMainLocationMilk(ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select Main_Location_Code from TSPL_LOCATION_MASTER where Location_Code='" + strLocation + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetPlantNameFromMCC(ByVal strMCC As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "SELECT Location_Desc FROM TSPL_LOCATION_MASTER inner join TSPL_MCC_MASTER on TSPL_MCC_MASTER.Plant_Code=TSPL_LOCATION_MASTER.Location_Code where TSPL_MCC_MASTER.mcc_code='" + strMCC + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetLocationSegments() As DataTable
        Try
            Dim qry As String = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
            qry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
            qry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
            qry += " order by xxx.Loc_Segment_Code"
            Return clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetRejectedLocation(ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select Rejected_Location  from tspl_location_master where Location_Code='" + strLocation + "' "
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetLocationSegmentsStatewise(ByVal strState As String) As DataTable
        Dim qry As String = ""
        Try
            qry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
            qry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0  " & strState & "  group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
            qry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
            qry += " order by xxx.Loc_Segment_Code"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return clsDBFuncationality.GetDataTable(qry)
    End Function
    Public Shared Function GetSegmentCode(ByVal ArrLocation As ArrayList, ByVal trans As SqlTransaction) As ArrayList
        Dim qry As String = ""
        Dim arrReturn As ArrayList = Nothing
        Try
            qry = "select distinct Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in ( " + clsCommon.GetMulcallString(ArrLocation) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrReturn = New ArrayList
                For Each dr As DataRow In dt.Rows
                    arrReturn.Add(clsCommon.myCstr(dr("Loc_Segment_Code")))
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arrReturn
    End Function
    Public Shared Function GetSegmentCode(ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = ""
        Try
            qry = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + strLocation + "'"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetSectionCode(ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = ""
        Try
            qry = "select SECTION_CODE from TSPL_LOCATION_MASTER where Location_Code='" + strLocation + "'"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function SaveData(ByVal obj As clsLocation, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal isImport As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Dim i As Integer = 1
        Dim Qry As String
        Try
            clsCommonFunctionality.SaveHistoryData(False, objCommonVar.CurrentUserCode, obj.Location_Code, "TSPL_LOCATION_MASTER", "Location_Code", "TSPL_LOCATION_WISE_TAX_MASTER", "Location_Code", "TSPL_LOCATION_MASTER_Jobwork_Item", "Main_Location_Code", "TSPL_LOCATION_CATEGORY_MASTER", "Location_code", "", "", "", "", "", "", trans)
            If Not isImport Then
                Qry = "Delete from TSPL_LOCATION_WISE_TAX_MASTER where Location_Code='" & obj.Location_Code & "' "
                clsDBFuncationality.getSingleValue(Qry, trans)
                ''richa agarwal
                Qry = "Delete from TSPL_LOCATION_WISE_ITEM_MASTER where Location_Code='" & obj.Location_Code & "' "
                clsDBFuncationality.getSingleValue(Qry, trans)
                ''-----------------------
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Loc_Short_Name", obj.Short_Name)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)
            clsCommon.AddColumnsForChange(coll, "UseInJobWork", obj.UseInJobWork)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.Add3)
            clsCommon.AddColumnsForChange(coll, "Add4", obj.Add4)
            clsCommon.AddColumnsForChange(coll, "HoAdd1", obj.HOAdd1)
            clsCommon.AddColumnsForChange(coll, "HoAdd2", obj.HOAdd2)
            clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendrocode)
            clsCommon.AddColumnsForChange(coll, "City_Code", obj.City_Code)
            clsCommon.AddColumnsForChange(coll, "State", obj.State)
            clsCommon.AddColumnsForChange(coll, "Pin_Code", obj.Pin_Code)
            clsCommon.AddColumnsForChange(coll, "Country", obj.Country)
            clsCommon.AddColumnsForChange(coll, "Telphone", obj.Telphone)
            clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
            clsCommon.AddColumnsForChange(coll, "Location_Type", obj.Location_Type)
            clsCommon.AddColumnsForChange(coll, "Loc_Status", obj.Loc_Status)
            clsCommon.AddColumnsForChange(coll, "Status_Date", obj.Status_Date)
            clsCommon.AddColumnsForChange(coll, "Excisable", obj.Excisable)
            clsCommon.AddColumnsForChange(coll, "IsParlour", obj.IsParlour)
            clsCommon.AddColumnsForChange(coll, "Loc_Segment_Code", obj.Loc_Segment_Code)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Purchase_Tax_Group", obj.Purchase_Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Sales_Tax_Group", obj.Sales_Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Ecc_Number", obj.Ecc_Number)
            clsCommon.AddColumnsForChange(coll, "Registration_Number", obj.Registration_Number)
            clsCommon.AddColumnsForChange(coll, "Commissionerate", obj.Commissionerate)
            clsCommon.AddColumnsForChange(coll, "Range_Code", obj.Range_Code)
            clsCommon.AddColumnsForChange(coll, "Range_Name", obj.Range_Name)
            clsCommon.AddColumnsForChange(coll, "Range_Address", obj.Range_Address)
            clsCommon.AddColumnsForChange(coll, "Division_Code", obj.Division_Code)
            clsCommon.AddColumnsForChange(coll, "Division_Name", obj.Division_Name)
            clsCommon.AddColumnsForChange(coll, "Division_Address", obj.Division_Address)
            clsCommon.AddColumnsForChange(coll, "NearestCity", obj.NearestCity)
            clsCommon.AddColumnsForChange(coll, "ESIC_No", obj.ESIC_No)
            clsCommon.AddColumnsForChange(coll, "PF_No", obj.PF_No)
            'clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
            'clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)
            'clsCommon.AddColumnsForChange(coll, "Comp_code", obj.Comp_code)
            clsCommon.AddColumnsForChange(coll, "TIN_No", obj.TIN_No)
            clsCommon.AddColumnsForChange(coll, "TAN_No", obj.TAN_No)
            clsCommon.AddColumnsForChange(coll, "TCAN_No", obj.TCAN_No)
            clsCommon.AddColumnsForChange(coll, "Service_Tax_Reg_No", obj.Service_Tax_Reg_No)
            clsCommon.AddColumnsForChange(coll, "DutyPaid", obj.DutyPaid)
            clsCommon.AddColumnsForChange(coll, "Purchase_Tax_GroupIS", obj.Purchase_Tax_GroupIS)
            clsCommon.AddColumnsForChange(coll, "Sales_Tax_GroupIS", obj.Sales_Tax_GroupIS)
            clsCommon.AddColumnsForChange(coll, "Stock_Transfer_Filled_Ac", obj.Stock_Transfer_Filled_Ac)
            clsCommon.AddColumnsForChange(coll, "Stock_Transfer_Empty_Ac", obj.Stock_Transfer_Empty_Ac)
            clsCommon.AddColumnsForChange(coll, "GIT_Location", obj.GIT_Location)
            clsCommon.AddColumnsForChange(coll, "GIT_Type", obj.GIT_Type)
            clsCommon.AddColumnsForChange(coll, "CST_No", obj.CST_No)
            clsCommon.AddColumnsForChange(coll, "Phone1", obj.Phone1)
            clsCommon.AddColumnsForChange(coll, "Phone2", obj.Phone2)
            clsCommon.AddColumnsForChange(coll, "Location_Category", obj.loc_categry)
            clsCommon.AddColumnsForChange(coll, "Rejected_Location", obj.Rejected_Location)
            clsCommon.AddColumnsForChange(coll, "Rejected_Type", obj.Rejected_Type)
            clsCommon.AddColumnsForChange(coll, "CSA_Type", obj.CSA_Type)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Category_Struct_Code", obj.Loc_Cat_structr_Code)
            clsCommon.AddColumnsForChange(coll, "CSA_Commision_Type", obj.csa_commision_type)
            clsCommon.AddColumnsForChange(coll, "CSA_Commision_Rate", obj.csa_commision_rate)
            clsCommon.AddColumnsForChange(coll, "Commision_Acc", obj.Vendor_Commsn_ACC)
            clsCommon.AddColumnsForChange(coll, "CSA_Commission_RS_PERS", obj.CSA_Commission_RS_PERS)
            clsCommon.AddColumnsForChange(coll, "DairyDispatchFromDO", obj.DairyDispatchFromDO)
            clsCommon.AddColumnsForChange(coll, "Bank", obj.bankBank, True)
            clsCommon.AddColumnsForChange(coll, "Branch", obj.bankBranch, True)
            clsCommon.AddColumnsForChange(coll, "ACType", obj.bankACType, True)
            clsCommon.AddColumnsForChange(coll, "bankaccno", obj.bankaccno, True)
            clsCommon.AddColumnsForChange(coll, "bankifsccode", obj.bankifsccode, True)
            clsCommon.AddColumnsForChange(coll, "accountholdername", obj.accountholdername, True)
            clsCommon.AddColumnsForChange(coll, "BankUPI_ID", obj.BankUPI_ID, True)
            clsCommon.AddColumnsForChange(coll, "Is_Section", obj.Is_Section)
            clsCommon.AddColumnsForChange(coll, "Is_Sub_Location", obj.Is_Sub_Location)
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code, True)
            clsCommon.AddColumnsForChange(coll, "Main_Location_Code", obj.Main_Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "stock_transfer_ac", clsCommon.myCstr(obj.Stock_Transfer_ac))
            clsCommon.AddColumnsForChange(coll, "Loss_Ac", clsCommon.myCstr(obj.Loss_Ac))
            clsCommon.AddColumnsForChange(coll, "Is_Consumption_Location", obj.Is_Consumption_Location)
            '========Added By Rohit ,July 27,2015=============
            clsCommon.AddColumnsForChange(coll, "Is_Jobwork", obj.Is_JobWork)
            clsCommon.AddColumnsForChange(coll, "Jobwork_Item", obj.Jobwork_Item)
            clsCommon.AddColumnsForChange(coll, "JobWork_Vendor", obj.JobWork_Vendor)
            '============================================
            '========Added By Preeti Gupta [For GST]=============
            clsCommon.AddColumnsForChange(coll, "GSTNO", obj.GSTNO)
            clsCommon.AddColumnsForChange(coll, "PAN_NO", obj.PAN_No)
            clsCommon.AddColumnsForChange(coll, "GSTEntity", obj.GSTEntity)
            clsCommon.AddColumnsForChange(coll, "GSTBlank", obj.GSTBlank)
            clsCommon.AddColumnsForChange(coll, "GSTDegit", obj.GSTDegit)
            clsCommon.AddColumnsForChange(coll, "Registered", obj.Is_Registered)
            '============================================
            '========Added by Mohd Suhail [for QC],9 Feb,2024======
            clsCommon.AddColumnsForChange(coll, "QC_IS", obj.QC_IS)
            clsCommon.AddColumnsForChange(coll, "CMA_CML", obj.CMA_CML)
            clsCommon.AddColumnsForChange(coll, "ValidUpto", obj.ValidUpto)
            clsCommon.AddColumnsForChange(coll, "GradeType", obj.GradeType)
            clsCommon.AddColumnsForChange(coll, "QCStartDate", clsCommon.GetPrintDate(obj.QC_StartDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Manager_Name", obj.ManagerName)
            clsCommon.AddColumnsForChange(coll, "Manager_Destination", obj.ManagerDestination)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            '============================================
            'Ticket- BHA/27/07/18-000198
            clsCommon.AddColumnsForChange(coll, "Silo_Capacity", obj.Silo_Capacity)
            'BHA/27/07/18-000198
            clsCommon.AddColumnsForChange(coll, "Is_Insurance", obj.Is_Insurance)
            If obj.Is_Insurance = 1 Then
                clsCommon.AddColumnsForChange(coll, "InsuranceNo", obj.InsuranceNo, True)
                clsCommon.AddColumnsForChange(coll, "InsuranceFromDate", clsCommon.GetPrintDate(obj.InsuranceFromDate, "dd/MMM/yyyy"), True)
                clsCommon.AddColumnsForChange(coll, "InsuranceToDate", clsCommon.GetPrintDate(obj.InsuranceToDate, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "InsuranceNo", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "InsuranceFromDate", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "InsuranceToDate", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "IsSubLocationWise", obj.IsSubLocationWise)
            clsCommon.AddColumnsForChange(coll, "IsMainPlant", obj.IsMainPlant)
            clsCommon.AddColumnsForChange(coll, "No_Of_Shift", obj.No_Of_Shift, True)
            If obj.MP_Collection_Running_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "MP_Collection_Running_Date", clsCommon.GetPrintDate(obj.MP_Collection_Running_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "MP_Collection_Running_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Uploader_No", obj.Uploader_No)
            'clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            'clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            Qry = "SELECT Count(*) FROM TSPL_LOCATION_MASTER where Location_Code = '" & obj.Location_Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(Qry, trans)
            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_code", obj.Comp_code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCATION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCATION_MASTER ", OMInsertOrUpdate.Update, " Location_Code='" + obj.Location_Code + "'", trans)
            End If
            If Not isImport Then
                isSaved = isSaved AndAlso clsLocationWiseTax.SaveData(obj.Location_Code, obj.Arr, trans)
                isSaved = isSaved AndAlso clsLocationWiseItems.SaveData(obj.Location_Code, obj.ArrItem, trans)
                isSaved = isSaved AndAlso ClsLocation_JobworkItem.SaveData(obj.Location_Code, obj.arr_JobworkItem, trans)
                isSaved = isSaved AndAlso clsLocationPlantDepotMapping.SaveData(obj, obj.ArrMappingPlantDepot, trans)
                isSaved = isSaved AndAlso SaveLocationCategory(obj.Location_Code, obj.Loc_Cat_structr_Code, obj.ArrCategoryStr, trans)
            End If
            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Location_Code, "TSPL_MULTIPLE_DEDUCTION_HEAD", "Document_No", "TSPL_MULTIPLE_DEDUCTION_DETAIL", "Document_No", trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveLocationCategory(ByVal Locationcode As String, ByVal Cat_Struct_Code As String, ByVal arr As List(Of clsLocation), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_LOCATION_CATEGORY_MASTER where location_code='" + Locationcode + "' and Category_Struct_Code='" + Cat_Struct_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim isSaved As Boolean = True
            If arr IsNot Nothing And arr.Count > 0 Then
                For Each objtr As clsLocation In arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Location_code", Locationcode)
                    clsCommon.AddColumnsForChange(coll, "Category_Struct_Code", Cat_Struct_Code)
                    clsCommon.AddColumnsForChange(coll, "Category_Code", objtr.Loc_Category_Code)
                    clsCommon.AddColumnsForChange(coll, "Category_Code_Values", objtr.Loc_Cagetory_Values)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCATION_CATEGORY_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = "Delete from TSPL_LOCATION_MASTER where Location_Code='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
    Public Shared Function GetData(ByVal locCode As String, Optional ByVal navType As NavigatorType = NavigatorType.Current) As clsLocation
        Dim obj As clsLocation = Nothing
        Try
            Dim qst As String = "select  *   from  TSPL_Location_MASTER "
            Select Case navType
                Case NavigatorType.Current
                    qst += " where  Location_Code ='" & locCode & "' "
                Case NavigatorType.Next
                    qst += " where  Location_Code in (select min(t.Location_Code ) from TSPL_LOCATION_MASTER  as t where t.Location_Code  >'" + locCode + "')"
                Case NavigatorType.First
                    qst += " where  Location_Code in (select min(t.Location_Code ) from TSPL_LOCATION_MASTER  as t)"
                Case NavigatorType.Last
                    qst += " where  Location_Code in (select max(t.Location_Code ) from TSPL_LOCATION_MASTER  as t)"
                Case NavigatorType.Previous
                    qst += " where  Location_Code in (select max(t.Location_Code ) from TSPL_LOCATION_MASTER  as t where t.Location_Code  <'" + locCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsLocation()
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.Short_Name = clsCommon.myCstr(dt.Rows(0)("Loc_Short_Name"))
                obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                obj.Add4 = clsCommon.myCstr(dt.Rows(0)("Add4"))
                obj.HOAdd1 = clsCommon.myCstr(dt.Rows(0)("HOAdd1"))
                obj.HOAdd2 = clsCommon.myCstr(dt.Rows(0)("HOAdd2"))
                obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                obj.State = clsCommon.myCstr(dt.Rows(0)("State"))
                obj.Pin_Code = clsCommon.myCstr(dt.Rows(0)("Pin_Code"))
                obj.Country = clsCommon.myCstr(dt.Rows(0)("Country"))
                obj.Telphone = clsCommon.myCstr(dt.Rows(0)("Telphone"))
                obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
                obj.Location_Type = clsCommon.myCstr(dt.Rows(0)("Location_Type"))
                obj.Loc_Status = clsCommon.myCstr(dt.Rows(0)("Loc_Status"))
                obj.NearestCity = clsCommon.myCstr(dt.Rows(0)("NearestCity"))
                obj.ESIC_No = clsCommon.myCstr(dt.Rows(0)("ESIC_No"))
                obj.PF_No = clsCommon.myCstr(dt.Rows(0)("PF_No"))
                obj.Is_JobWork = clsCommon.myCstr(dt.Rows(0)("Is_JobWork"))
                obj.JobWork_Vendor = clsCommon.myCstr(dt.Rows(0)("Jobwork_vendor"))
                obj.Jobwork_Item = clsCommon.myCstr(dt.Rows(0)("Jobwork_Item"))
                If clsCommon.myLen(dt.Rows(0)("Status_Date")) > 0 Then
                    obj.Status_Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Status_Date")), "dd/MMM/yyyy")
                End If
                obj.Is_Consumption_Location = CInt(clsCommon.myCdbl(dt.Rows(0)("Is_Consumption_Location")))
                obj.Excisable = clsCommon.myCstr(dt.Rows(0)("Excisable"))
                obj.Loc_Segment_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Segment_Code"))
                obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
                obj.Purchase_Tax_Group = clsCommon.myCstr(dt.Rows(0)("Purchase_Tax_Group"))
                obj.Sales_Tax_Group = clsCommon.myCstr(dt.Rows(0)("Sales_Tax_Group"))
                obj.Ecc_Number = clsCommon.myCstr(dt.Rows(0)("Ecc_Number"))
                obj.Registration_Number = clsCommon.myCstr(dt.Rows(0)("Registration_Number"))
                obj.Commissionerate = clsCommon.myCstr(dt.Rows(0)("Commissionerate"))
                obj.Range_Code = clsCommon.myCstr(dt.Rows(0)("Range_Code"))
                obj.Range_Name = clsCommon.myCstr(dt.Rows(0)("Range_Name"))
                obj.Range_Address = clsCommon.myCstr(dt.Rows(0)("Range_Address"))
                obj.Division_Code = clsCommon.myCstr(dt.Rows(0)("Division_Code"))
                obj.Division_Name = clsCommon.myCstr(dt.Rows(0)("Division_Name"))
                obj.Division_Address = clsCommon.myCstr(dt.Rows(0)("Division_Address"))
                obj.TIN_No = clsCommon.myCstr(dt.Rows(0)("TIN_No"))
                obj.TAN_No = clsCommon.myCstr(dt.Rows(0)("TAN_NO"))
                obj.TCAN_No = clsCommon.myCstr(dt.Rows(0)("TCAN_NO"))
                obj.Service_Tax_Reg_No = clsCommon.myCstr(dt.Rows(0)("Service_Tax_Reg_No"))
                obj.DutyPaid = clsCommon.myCstr(dt.Rows(0)("DutyPaid"))
                obj.Purchase_Tax_GroupIS = clsCommon.myCstr(dt.Rows(0)("Purchase_Tax_GroupIS"))
                obj.Sales_Tax_GroupIS = clsCommon.myCstr(dt.Rows(0)("Sales_Tax_GroupIS"))
                obj.Stock_Transfer_Filled_Ac = clsCommon.myCstr(dt.Rows(0)("Stock_Transfer_Filled_Ac"))
                obj.Stock_Transfer_Empty_Ac = clsCommon.myCstr(dt.Rows(0)("Stock_Transfer_Empty_Ac"))
                obj.GIT_Location = clsCommon.myCstr(dt.Rows(0)("GIT_Location"))
                obj.GIT_Type = clsCommon.myCstr(dt.Rows(0)("GIT_Type"))
                obj.CST_No = clsCommon.myCstr(dt.Rows(0)("CST_No"))
                obj.Phone1 = clsCommon.myCstr(dt.Rows(0)("Phone1"))
                obj.Phone2 = clsCommon.myCstr(dt.Rows(0)("Phone2"))
                obj.vendrocode = clsCommon.myCstr(dt.Rows(0)("vendor_code"))
                obj.csa_commision_rate = clsCommon.myCdbl(dt.Rows(0)("CSA_Commision_Rate"))
                obj.csa_commision_type = clsCommon.myCstr(dt.Rows(0)("CSA_Commision_Type"))
                obj.CSA_Commission_RS_PERS = clsCommon.myCstr(dt.Rows(0)("CSA_Commission_RS_PERS"))
                obj.DairyDispatchFromDO = clsCommon.myCdbl(dt.Rows(0)("DairyDispatchFromDO"))
                obj.IsSubLocationWise = clsCommon.myCstr(dt.Rows(0)("IsSubLocationWise"))
                '' Anubhooti 31-July-2014
                obj.Is_Section = clsCommon.myCstr(dt.Rows(0)("Is_Section"))
                obj.Is_Sub_Location = clsCommon.myCstr(dt.Rows(0)("Is_Sub_Location"))
                obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
                obj.Main_Location_Code = clsCommon.myCstr(dt.Rows(0)("Main_Location_Code"))
                obj.Stock_Transfer_ac = clsCommon.myCstr(dt.Rows(0)("Stock_Transfer_ac"))
                obj.Loss_Ac = clsCommon.myCstr(dt.Rows(0)("Loss_Ac"))
                'obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
                'obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
                'obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
                'obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
                'obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
                'obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
                'obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
                'obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
                'obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
                'obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
                obj.GSTNO = clsCommon.myCstr(dt.Rows(0)("Loss_Ac"))
                obj.PAN_No = clsCommon.myCstr(dt.Rows(0)("PAN_No"))
                obj.GSTEntity = clsCommon.myCstr(dt.Rows(0)("GSTEntity"))
                obj.GSTDegit = clsCommon.myCstr(dt.Rows(0)("GSTDegit"))
                obj.GSTBlank = clsCommon.myCstr(dt.Rows(0)("GSTBlank"))
                obj.Silo_Capacity = clsCommon.myCdbl(dt.Rows(0)("Silo_Capacity"))
                obj.Is_Insurance = clsCommon.myCdbl(dt.Rows(0)("Is_Insurance"))
                If clsCommon.myCdbl(dt.Rows(0)("Is_Insurance")) > 0 Then
                    obj.InsuranceNo = clsCommon.myCstr(dt.Rows(0)("InsuranceNo"))
                    obj.InsuranceFromDate = clsCommon.myCDate(dt.Rows(0)("InsuranceFromDate"), "dd/MMM/yyyy")
                    obj.InsuranceToDate = clsCommon.myCDate(dt.Rows(0)("InsuranceToDate"), "dd/MMM/yyyy")
                End If
                If obj.vendrocode IsNot Nothing AndAlso obj.vendrocode IsNot DBNull.Value Then
                    obj.vendorname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where vendor_code='" + obj.vendrocode + "'"))
                End If
                obj.IsMainPlant = clsCommon.myCdbl(dt.Rows(0)("IsMainPlant"))
                obj.No_Of_Shift = IIf(clsCommon.myCdbl(dt.Rows(0)("No_Of_Shift")) > 0, clsCommon.myCdbl(dt.Rows(0)("No_Of_Shift")), Nothing)
                If dt.Rows(0)("MP_Collection_Running_Date") IsNot DBNull.Value Then
                    obj.MP_Collection_Running_Date = clsCommon.myCDate(dt.Rows(0)("MP_Collection_Running_Date"))
                Else
                    obj.MP_Collection_Running_Date = Nothing
                End If
                obj.Uploader_No = clsCommon.myCstr(dt.Rows(0)("Uploader_No"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function CheckCode(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select count(Location_Code) from TSPL_LOCATION_MASTER where LOCATION_CODE='" + strCode + "' "
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetGSTLocationInterState(ByVal strVendorCustomerLocationCode As String, ByVal strLocationCode As String, ByVal strTaxType As String, ByVal tran As SqlTransaction) As Boolean
        Dim qry As String = "select case when MIN(xx.State)=MAX(xx.State) then 'L' else 'I' end as LocalOrInterState,max(Is_GST_UT)  as Is_GST_UT from  ( select x.*,TSPL_STATE_MASTER.Is_GST_UT from ( select State  from TSPL_LOCATION_MASTER where Location_Code='" + strLocationCode + "'  union all"
        If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
            qry += "  select State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerLocationCode + "' "
        ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
            qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerLocationCode + "' "
        ElseIf clsCommon.CompairString("T", strTaxType) = CompairStringResult.Equal Then
            qry += " select State  from TSPL_LOCATION_MASTER where Location_Code='" + strVendorCustomerLocationCode + "' "
        Else
            Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S' or 'T'")
        End If
        qry += " )x left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=x.State)xx"
        Dim dtLorI As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dtLorI IsNot Nothing AndAlso dtLorI.Rows.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(dtLorI.Rows(0)("LocalOrInterState")), "L") = CompairStringResult.Equal Then
                Return False
            End If
        Else
            Throw New Exception("Not able to decide location wise tax Local Or Inter State")
        End If
        dtLorI = Nothing
        Return True
    End Function
    Public Shared Function IsJobWorkLocation(ByVal strCode As String, ByVal tran As SqlTransaction) As Boolean
        Return (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Jobwork from TSPL_LOCATION_MASTER where Location_Code='" + strCode + "'", tran)) = 1)
    End Function
End Class


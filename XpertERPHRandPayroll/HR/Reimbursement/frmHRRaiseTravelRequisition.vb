'' Created By Anubhooti BM00000006296 
Imports common
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine

Public Class FrmHRRaiseTravelRequisition
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRRaiseTravelRequisition)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub Reset()
        txtCode.Value = ""
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        Me.RadPageView1.SelectedPage = RadPageViewPage1
        '' Organizational Detail
        dtpDate.Value = clsCommon.GETSERVERDATE()
        TxtTravelPur.Text = ""
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        TxtTCategory.Value = ""
        LblTCategory.Text = ""
        TxtLocFrom.Value = ""
        LblLocFrom.Text = ""
        TxtLocTo.Value = ""
        LblLocTo.Text = ""
        dtpDepartureDate.Value = clsCommon.GETSERVERDATE()
        dtpArrivalDate.Value = clsCommon.GETSERVERDATE()
        TxtTravelMode.Value = ""
        LblTravelMode.Text = ""
        TxtTClass.Value = ""
        LblTClass.Text = ""
        TxtTFlightNo.Text = ""
        TxtTCouponNo.Text = ""
        TxtBookedByName.Value = ""
        LblBookedByName.Text = ""
        TxtTravelPur.Value = ""
        LblTravelPur.Text = ""
        TxtBookingFor.Value = ""
        LblBookingFor.Text = ""
        TxtTRemarks.Text = ""
        dtpHStayFrom.Value = clsCommon.GETSERVERDATE()
        dtpStayTo.Value = clsCommon.GETSERVERDATE()
        TxtHHotelRating.Value = ""
        LblHotelRating.Text = ""
        LblDays.Text = ""
        LblNight.Text = ""
        TxtTFlightNo.Text = ""
        dtpPeriodFrom.Value = clsCommon.GETSERVERDATE()
        dtpPeriodTo.Value = clsCommon.GETSERVERDATE()
        TxtTypesCar.Value = ""
        LblTypesOfCar.Text = ""
        TxtFromLoc.Value = ""
        LblFromLoc.Text = ""
        TxtToLoc.Value = ""
        LblToLoc.Text = ""
        TxtAmount.Value = 0
        TxtCRemarks.Text = ""
        rbtnDomestic.IsChecked = True
        rbtnInternational.IsChecked = False

        lblCompCode.Text = objCommonVar.CurrentCompanyCode
        LblCompName.Text = objCommonVar.CurrentCompanyName

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT ISNULL(TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,'') As [Department Code],ISNULL(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME ,'') AS [Department Name], " & _
                            " ISNULL(Designation,'') As [Designation Code],ISNULL(TSPL_DESIGNATION_MASTER.Designation_Desc,'') As [Desgination Name] FROM TSPL_EMPLOYEE_MASTER LEFT OUTER JOIN " & _
                            " TSPL_DEPARTMENT_MASTER ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE " & _
                            " LEFT OUTER JOIN TSPL_DESIGNATION_MASTER ON TSPL_DESIGNATION_MASTER.Designation_id = TSPL_EMPLOYEE_MASTER.Designation WHERE USER_CODE='" & objCommonVar.CurrentUserCode & "'")

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblDeptCode.Text = clsCommon.myCstr(dt.Rows(0)("Department Code"))
            LblDeptName.Text = clsCommon.myCstr(dt.Rows(0)("Department Name"))
            lblDesgCode.Text = clsCommon.myCstr(dt.Rows(0)("Designation Code"))
            LblDesgName.Text = clsCommon.myCstr(dt.Rows(0)("Desgination Name"))
        Else
            lblDeptCode.Text = ""
            LblDeptName.Text = ""
            lblDesgCode.Text = ""
            LblDesgName.Text = ""
        End If

        Me.cmbTravelRqst.DataSource = ClsHRRaiseTravelRequisition.GetTReq
        Me.cmbTravelRqst.DisplayMember = "Name"
        Me.cmbTravelRqst.ValueMember = "Code"

        Me.CmbTBookedBy.DataSource = ClsHRRaiseTravelRequisition.GetBookedBy
        Me.CmbTBookedBy.DisplayMember = "Name"
        Me.CmbTBookedBy.ValueMember = "Code"

        Me.CmbBookByHotel.DataSource = ClsHRRaiseTravelRequisition.GetBookedBy
        Me.CmbBookByHotel.DisplayMember = "Name"
        Me.CmbBookByHotel.ValueMember = "Code"

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtCode.MyReadOnly = False

        txtCode.Focus()
        txtCode.Select()
    End Sub
    Function AllowToSave() As Boolean
        Try
            Dim ToDate As Date? = Nothing
            Dim FromDate As Date? = Nothing
            Dim StayFrom As Date? = Nothing
            Dim StayTo As Date? = Nothing
            Dim IsSamePeriod As Integer = 0

            btnsave.Focus()
            If clsCommon.myLen(txtcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill Code", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                txtcode.Focus()
                txtcode.Select()
                Return False
            End If
            If clsCommon.myLen(TxtTravelPur.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill travel purpose", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                TxtTravelPur.Focus()
                TxtTravelPur.Select()
                Return False
            End If
            If clsCommon.myLen(TxtBookingFor.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill booking for in organizational frame", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                TxtBookingFor.Focus()
                TxtBookingFor.Select()
                Return False
            End If
            If clsCommon.myLen(TxtTCategory.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill category in organizational frame", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                TxtTCategory.Focus()
                TxtTCategory.Select()
                Return False
            End If
            If clsCommon.myLen(TxtBookedByName.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill booking by name in ticket travelling frame", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                TxtBookedByName.Focus()
                TxtBookedByName.Select()
                Return False
            End If
            If clsCommon.myLen(TxtFromLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill from location in ticket travelling frame", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                TxtFromLoc.Focus()
                TxtFromLoc.Select()
                Return False
            End If
            If clsCommon.myLen(TxtToLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill to location in ticket travelling frame", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                TxtToLoc.Focus()
                TxtToLoc.Select()
                Return False
            End If

            IsSamePeriod = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) AS ROW FROM TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY WHERE Travel_Period_From ='" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd-MMM-yyyy") & "' And Travel_Period_To ='" & clsCommon.GetPrintDate(dtpToDate.Value, "dd-MMM-yyyy") & "' AND travel_req_code NOT IN ('" & txtcode.Value & "')"))
            If IsSamePeriod > 0 Then
                clsCommon.MyMessageBoxShow("Please check you have already applied requisition for this travel period", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                dtpFromDate.Focus()
                dtpFromDate.Select()
                Return False
            End If
            '' Arrival Date And Departure Date 
            ToDate = clsCommon.myCDate(dtpArrivalDate.Value)
            FromDate = clsCommon.myCDate(dtpDepartureDate.Value)
            If FromDate > ToDate Then
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("Please check Arrival date should be greater than from Departure date.")
            End If
            If clsCommon.myCDate(dtpFromDate.Value) > clsCommon.myCDate(dtpToDate.Value) Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Please check To date should be greater than From date at line no.")
            End If
            If clsCommon.myCDate(dtpArrivalDate.Value) >= clsCommon.myCDate(dtpFromDate.Value) And clsCommon.myCDate(dtpArrivalDate.Value) <= clsCommon.myCDate(dtpToDate.Value) Then
            Else
                clsCommon.MyMessageBoxShow("Please check ! Arrival Date should be between period from date and period to date", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                dtpArrivalDate.Focus()
                dtpArrivalDate.Select()
                Return False
            End If
            If clsCommon.myCDate(dtpDepartureDate.Value) >= clsCommon.myCDate(dtpFromDate.Value) And clsCommon.myCDate(dtpDepartureDate.Value) <= clsCommon.myCDate(dtpToDate.Value) Then
            Else
                clsCommon.MyMessageBoxShow("Please check ! Departure Date should be between period from date and period to date", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                dtpDepartureDate.Focus()
                dtpDepartureDate.Select()
                Return False
            End If

            '' Stay From And Stay To
            StayTo = clsCommon.myCDate(dtpStayTo.Value)
            StayFrom = clsCommon.myCDate(dtpHStayFrom.Value)
            If StayFrom > StayTo Then
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("Please check Stay To date should be greater than Stay From date.")
            End If

            If clsCommon.myCDate(dtpHStayFrom.Value) >= clsCommon.myCDate(dtpFromDate.Value) And clsCommon.myCDate(dtpHStayFrom.Value) <= clsCommon.myCDate(dtpToDate.Value) Then
            Else
                clsCommon.MyMessageBoxShow("Please check ! Date of Stay from should be between period from date and period to date", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                dtpHStayFrom.Focus()
                dtpHStayFrom.Select()
                Return False
            End If
            If clsCommon.myCDate(dtpStayTo.Value) >= clsCommon.myCDate(dtpFromDate.Value) And clsCommon.myCDate(dtpStayTo.Value) <= clsCommon.myCDate(dtpToDate.Value) Then
            Else
                clsCommon.MyMessageBoxShow("Please check ! Date of Stay to should be between period from date and period to date", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage3
                dtpStayTo.Focus()
                dtpStayTo.Select()
                Return False
            End If
            '' Car Frame
            If clsCommon.myCDate(dtpPeriodFrom.Value) >= clsCommon.myCDate(dtpFromDate.Value) And clsCommon.myCDate(dtpPeriodFrom.Value) <= clsCommon.myCDate(dtpToDate.Value) Then
            Else
                clsCommon.MyMessageBoxShow("Please check ! From Date(Car Frame) to should be between period from date and period to date", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage4
                dtpPeriodFrom.Focus()
                dtpPeriodFrom.Select()
                Return False
            End If

            If clsCommon.myCDate(dtpPeriodTo.Value) >= clsCommon.myCDate(dtpFromDate.Value) And clsCommon.myCDate(dtpPeriodTo.Value) <= clsCommon.myCDate(dtpToDate.Value) Then
            Else
                clsCommon.MyMessageBoxShow("Please check ! To Date(Car Frame) to should be between period from date and period to date", Me.Text)
                Me.RadPageView1.SelectedPage = RadPageViewPage4
                dtpPeriodTo.Focus()
                dtpPeriodTo.Select()
                Return False
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            Dim obj As New ClsHRRaiseTravelRequisition()

            obj.Travel_Req_Code = clsCommon.myCstr(txtcode.Value)
            obj.Document_Date = dtpDate.Value
            obj.Travel_Purpose_Code = clsCommon.myCstr(TxtTravelPur.Value)
            obj.Travel_Period_From = dtpFromDate.Value
            obj.Travel_Period_To = dtpToDate.Value
            obj.Travel_Category_Code = clsCommon.myCstr(TxtTCategory.Value)
            obj.Travel_Request = clsCommon.myCstr(cmbTravelRqst.SelectedValue)
            obj.Travel_Class_Code_Travel = clsCommon.myCstr(TxtTClass.Value)

            obj.Loc_From_Travel = clsCommon.myCstr(TxtFromLoc.Value)
            obj.Loc_To_Travel = clsCommon.myCstr(TxtToLoc.Value)
            obj.Departure_Date = dtpDepartureDate.Value
            obj.Arrival_Date = dtpArrivalDate.Value
            obj.Travel_Mode_Code = clsCommon.myCstr(TxtTravelMode.Value)
            obj.Days = clsCommon.myCstr(LblDays.Text)
            obj.Night = clsCommon.myCstr(LblNight.Text)
            obj.Flight_No_Travel = clsCommon.myCstr(TxtTFlightNo.Text)
            obj.Coupon_No_Travel = clsCommon.myCstr(TxtTCouponNo.Text)
            obj.Booked_By_Travel = clsCommon.myCstr(CmbTBookedBy.SelectedValue)
            obj.Booked_By_Name_Code = clsCommon.myCstr(TxtBookedByName.Value)
            obj.Remarks_Travel = clsCommon.myCstr(TxtTRemarks.Text)
            obj.Booking_For_Code = clsCommon.myCstr(TxtBookingFor.Value)

            obj.Date_Of_Stay_From_Hotel = dtpHStayFrom.Value
            obj.Date_Of_Stay_To_Hotel = dtpStayTo.Value
            obj.Hotel_Rating_Code_Hotel = clsCommon.myCstr(TxtHHotelRating.Value)
            obj.Booked_By_Name_Hotel = clsCommon.myCstr(CmbBookByHotel.SelectedValue)

            obj.Travel_Room_Type = clsCommon.myCstr(TxtRoomType.Value)
            obj.Period_From_Car = clsCommon.myCstr(dtpPeriodFrom.Value)
            obj.Period_To_Car = clsCommon.myCstr(dtpPeriodTo.Value)
            obj.Travel_Car_Code = clsCommon.myCstr(TxtTypesCar.Value)
            obj.Loc_From_Car = clsCommon.myCstr(TxtLocFrom.Value)
            obj.Loc_To_Car = clsCommon.myCstr(TxtLocTo.Value)
            obj.Amount = clsCommon.myCdbl(TxtAmount.Value)
            obj.Remarks_Car = clsCommon.myCstr(TxtCRemarks.Text)

            If rbtnDomestic.IsChecked = True Then
                obj.Is_Domesctic = 1
                obj.Is_International = 0
            ElseIf rbtnInternational.IsChecked = True Then
                obj.Is_International = 1
                obj.Is_Domesctic = 0
            End If

            If ClsHRRaiseTravelRequisition.SaveData(obj, txtcode.Value) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnsave.Text = "Update"
                btndelete.Enabled = True
                LoadData(obj.Travel_Req_Code, NavigatorType.Current)
                txtcode.MyReadOnly = True
            Else
                txtcode.MyReadOnly = False
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New ClsHRRaiseTravelRequisition()
            txtCode.Value = strCode
            obj = ClsHRRaiseTravelRequisition.GetData(strCode, NavTyep)

            lblCompCode.Text = objCommonVar.CurrentCompanyCode
            LblCompName.Text = objCommonVar.CurrentCompanyName
            ' LblTBookingFor.Text = objCommonVar.CurrentUserCode
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT ISNULL(TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,'') As [Department Code],ISNULL(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME ,'') AS [Department Name], " & _
                                " ISNULL(Designation,'') As [Designation Code],ISNULL(TSPL_DESIGNATION_MASTER.Designation_Desc,'') As [Desgination Name] FROM TSPL_EMPLOYEE_MASTER LEFT OUTER JOIN " & _
                                " TSPL_DEPARTMENT_MASTER ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE " & _
                                " LEFT OUTER JOIN TSPL_DESIGNATION_MASTER ON TSPL_DESIGNATION_MASTER.Designation_id = TSPL_EMPLOYEE_MASTER.Designation WHERE USER_CODE='" & objCommonVar.CurrentUserCode & "'")

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblDeptCode.Text = clsCommon.myCstr(dt.Rows(0)("Department Code"))
                LblDeptName.Text = clsCommon.myCstr(dt.Rows(0)("Department Name"))
                lblDesgCode.Text = clsCommon.myCstr(dt.Rows(0)("Designation Code"))
                LblDesgName.Text = clsCommon.myCstr(dt.Rows(0)("Desgination Name"))
            Else
                lblDeptCode.Text = ""
                LblDeptName.Text = ""
                lblDesgCode.Text = ""
                LblDesgName.Text = ""
            End If


            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Travel_Req_Code) > 0 Then
                txtcode.Value = clsCommon.myCstr(obj.Travel_Req_Code)
                lblCompCode.Text = objCommonVar.CurrentCompanyCode
                LblCompName.Text = objCommonVar.CurrentCompanyName

                dtpDate.Value = obj.Document_Date
                TxtTravelPur.Text = clsCommon.myCstr(obj.Travel_Purpose_Code)
                dtpFromDate.Value = clsCommon.myCstr(obj.Travel_Period_From)
                dtpToDate.Value = clsCommon.myCstr(obj.Travel_Period_To)
                TxtTravelPur.Value = clsCommon.myCstr(obj.Travel_Purpose_Code)
                If clsCommon.myLen(TxtTravelPur.Value) > 0 Then
                    LblTravelPur.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Travel_Desp,'') AS Travel_Desp FROM TSPL_HR_TRAVEL_PURPOSE_MASTER WHERE Travel_Code ='" + TxtTravelPur.Value + "'")
                Else
                    LblTravelPur.Text = ""
                End If
                TxtBookedByName.Value = clsCommon.myCstr(obj.Booked_By_Name_Code)
                If clsCommon.myLen(TxtBookedByName.Value) > 0 Then
                    LblBookedByName.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Emp_Name,'') AS Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE ='" + TxtBookedByName.Value + "'")
                Else
                    LblBookedByName.Text = ""
                End If
                TxtBookingFor.Value = clsCommon.myCstr(obj.Booking_For_Code)
                If clsCommon.myLen(TxtBookingFor.Value) > 0 Then
                    LblBookingFor.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Emp_Name,'') AS Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE ='" + TxtBookingFor.Value + "'")
                Else
                    LblBookingFor.Text = ""
                End If
                TxtTCategory.Value = clsCommon.myCstr(obj.Travel_Category_Code)
                If clsCommon.myLen(TxtTCategory.Value) > 0 Then
                    LblTCategory.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_HR_TRAVEL_CATEGORY_MASTER WHERE Travel_Category_Code ='" + TxtTCategory.Value + "'")
                Else
                    LblTCategory.Text = ""
                End If
                If obj.Is_Domesctic = 1 Then
                    rbtnDomestic.IsChecked = True
                Else
                    rbtnInternational.IsChecked = False
                End If
                If obj.Is_International = 1 Then
                    rbtnInternational.IsChecked = True
                Else
                    rbtnInternational.IsChecked = False
                End If

                TxtFromLoc.Value = clsCommon.myCstr(obj.Loc_From_Travel)
                If clsCommon.myLen(TxtFromLoc.Value) > 0 Then
                    LblFromLoc.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_HR_TRAVEL_CITY_MASTER WHERE Travel_City_Code ='" + TxtFromLoc.Value + "'")
                Else
                    LblFromLoc.Text = ""
                End If
                TxtToLoc.Value = clsCommon.myCstr(obj.Loc_To_Travel)
                If clsCommon.myLen(TxtToLoc.Value) > 0 Then
                    LblToLoc.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_HR_TRAVEL_CITY_MASTER WHERE Travel_City_Code ='" + TxtToLoc.Value + "'")
                Else
                    LblToLoc.Text = ""
                End If
                dtpDepartureDate.Value = obj.Departure_Date
                dtpArrivalDate.Value = obj.Arrival_Date
                TxtTravelMode.Value = clsCommon.myCstr(obj.Travel_Mode_Code)
                If clsCommon.myLen(TxtTravelMode.Value) > 0 Then
                    LblTravelMode.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_HR_TRAVEL_MODE_TYPE_MASTER WHERE Travel_Mode_Code ='" + TxtTravelMode.Value + "'")
                Else
                    LblTravelMode.Text = ""
                End If

                TxtTFlightNo.Text = clsCommon.myCstr(obj.Flight_No_Travel)
                TxtTCouponNo.Text = clsCommon.myCstr(obj.Coupon_No_Travel)
                CmbTBookedBy.SelectedValue = clsCommon.myCstr(obj.Booked_By_Travel)
                TxtTRemarks.Text = clsCommon.myCstr(obj.Remarks_Travel)
                TxtTClass.Value = clsCommon.myCstr(obj.Travel_Class_Code_Travel)
                If clsCommon.myLen(TxtTClass.Value) > 0 Then
                    LblTClass.Text = clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_HR_TRAVEL_CLASS_TYPE_MASTER WHERE Travel_Class_Code='" + TxtTClass.Value + "'")
                Else
                    LblTClass.Text = ""
                End If

                dtpHStayFrom.Value = obj.Date_Of_Stay_From_Hotel
                dtpStayTo.Value = obj.Date_Of_Stay_To_Hotel
                TxtHHotelRating.Value = clsCommon.myCstr(obj.Hotel_Rating_Code_Hotel)
                CmbBookByHotel.SelectedValue = clsCommon.myCstr(obj.Booked_By_Name_Hotel)
                LblDays.Text = clsCommon.myCdbl(obj.Days)
                LblNight.Text = clsCommon.myCdbl(obj.Night)

                TxtRoomType.Value = clsCommon.myCstr(obj.Travel_Room_Type)
                If clsCommon.myLen(TxtRoomType.Value) > 0 Then
                    LblRoomType.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_HR_TRAVEL_MODE_TYPE_MASTER WHERE Travel_Mode_Code ='" + TxtRoomType.Value + "'")
                Else
                    LblRoomType.Text = ""
                End If

                dtpPeriodFrom.Value = obj.Period_From_Car
                dtpPeriodTo.Value = obj.Period_To_Car
                TxtTypesCar.Value = clsCommon.myCstr(obj.Travel_Car_Code)
                If clsCommon.myLen(TxtTypesCar.Value) > 0 Then
                    LblTypesOfCar.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_HR_TRAVEL_CAR_TYPE_MASTER WHERE Travel_Car_Code ='" + TxtTypesCar.Value + "'")
                Else
                    LblTypesOfCar.Text = ""
                End If
                TxtLocFrom.Value = clsCommon.myCstr(obj.Loc_From_Car)
                TxtLocTo.Value = clsCommon.myCstr(obj.Loc_To_Car)
                TxtAmount.Value = clsCommon.myCdbl(obj.Amount)
                TxtCRemarks.Text = clsCommon.myCstr(obj.Remarks_Car)

                txtcode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Code not found to delete")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            Dim IsAppRej As Integer = 0
            IsAppRej = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) AS ROW FROM TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY WHERE Approved=1 OR Rejected=1"))
            If IsAppRej = 0 Then
                If (myMessages.deleteConfirm()) Then
                    If (ClsHRRaiseTravelRequisition.DeleteData(txtcode.Value)) Then
                        common.clsCommon.MyMessageBoxShow("Data Deleted Successfully.")
                        Reset()
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow("Record is in use.")
            End If
            
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
#End Region
#Region "Events"
    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY where Travel_Req_Code='" + txtcode.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtcode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtcode.MyReadOnly = False
            End If
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY where Travel_Req_Code ='" + txtcode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtcode.MyReadOnly = False
            Else
                txtcode.MyReadOnly = True
            End If
            If txtcode.MyReadOnly OrElse isButtonClicked Then

                txtcode.Value = ClsHRRaiseTravelRequisition.GetFinder("", txtcode.Value, isButtonClicked)
                If clsCommon.myLen(txtcode.Value) > 0 Then
                    btndelete.Enabled = True
                    btnsave.Text = "Update"
                    txtcode.MyReadOnly = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                    txtcode.MyReadOnly = False
                End If
            End If
            LoadData(txtcode.Value, NavigatorType.Current)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Reset()
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If AllowToSave() Then
            SaveData()
        End If
    End Sub
#End Region

    Private Sub FrmHRRaiseTravelRequisition_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            If AllowToSave() Then
                SaveData()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso BtnClose.Enabled Then
            Me.Close()
            GC.Collect()
        End If
    End Sub
    Private Sub FrmHRRaiseTravelRequisition_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub TxtFromLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFromLoc._MYValidating
        TxtFromLoc.Value = ClsHRTravelCityMaster.GetFinder("", TxtFromLoc.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtFromLoc.Value) > 0 Then
            LblFromLoc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_HR_TRAVEL_CITY_MASTER where Travel_City_Code='" + TxtFromLoc.Value + "'")
        Else
            LblFromLoc.Text = ""
        End If
    End Sub
    Private Sub TxtToLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtToLoc._MYValidating
        TxtToLoc.Value = ClsHRTravelCityMaster.GetFinder("", TxtToLoc.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtToLoc.Value) > 0 Then
            LblToLoc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_HR_TRAVEL_CITY_MASTER where Travel_City_Code='" + TxtToLoc.Value + "'")
        Else
            LblToLoc.Text = ""
        End If
    End Sub
    Private Sub TxtLocFrom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtLocFrom._MYValidating
        TxtLocFrom.Value = ClsHRTravelCityMaster.GetFinder("", TxtLocFrom.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtLocFrom.Value) > 0 Then
            LblLocFrom.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_HR_TRAVEL_CITY_MASTER where Travel_City_Code='" + TxtLocFrom.Value + "'")
        Else
            LblLocFrom.Text = ""
        End If
    End Sub
    Private Sub TxtLocTo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtLocTo._MYValidating
        TxtLocTo.Value = ClsHRTravelCityMaster.GetFinder("", TxtLocTo.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtLocTo.Value) > 0 Then
            LblLocTo.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_HR_TRAVEL_CITY_MASTER where Travel_City_Code='" + TxtLocTo.Value + "'")
        Else
            LblLocTo.Text = ""
        End If
    End Sub

    Private Sub TxtRoomType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtRoomType._MYValidating
        TxtRoomType.Value = ClsHRTravelRoomTypeMaster.GetFinder("", TxtRoomType.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtRoomType.Value) > 0 Then
            LblRoomType.Text = clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_HR_TRAVEL_ROOM_TYPE_MASTER WHERE Travel_Room_Code='" + TxtRoomType.Value + "'")
        Else
            LblRoomType.Text = ""
        End If
    End Sub

    Private Sub TxtTCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTCategory._MYValidating
        TxtTCategory.Value = ClsHRTravelCategoryMaster.GetFinder("", TxtTCategory.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTCategory.Value) > 0 Then
            LblTCategory.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Description FROM TSPL_HR_TRAVEL_CATEGORY_MASTER WHERE Travel_Category_Code='" + TxtTCategory.Value + "'")
        Else
            LblTCategory.Text = ""
        End If
    End Sub

    Private Sub TxtTravelMode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTravelMode._MYValidating
        TxtTravelMode.Value = ClsHRTravelModeTypeMaster.GetFinder("", TxtTravelMode.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTravelMode.Value) > 0 Then
            LblTravelMode.Text = clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_HR_TRAVEL_MODE_TYPE_MASTER WHERE Travel_Mode_Code='" + TxtTravelMode.Value + "'")
        Else
            LblTravelMode.Text = ""
        End If
    End Sub

    Private Sub TxtTypesCar__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTypesCar._MYValidating
        TxtTypesCar.Value = ClsHRTravelCarTypeMaster.GetFinder("", TxtTypesCar.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTypesCar.Value) > 0 Then
            LblTypesOfCar.Text = clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_HR_TRAVEL_CAR_TYPE_MASTER WHERE Travel_Car_Code='" + TxtTypesCar.Value + "'")
        Else
            LblTypesOfCar.Text = ""
        End If
    End Sub

    Private Sub TxtHHotelRating__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtHHotelRating._MYValidating
        TxtHHotelRating.Value = ClsHRHotelRatingMaster.GetFinder("", TxtHHotelRating.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtHHotelRating.Value) > 0 Then
            LblHotelRating.Text = clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_HR_HOTEL_RATING_MASTER WHERE Hotel_Rating_Code='" + TxtHHotelRating.Value + "'")
        Else
            LblHotelRating.Text = ""
        End If
    End Sub

    Private Sub dtpArrivalDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpArrivalDate.ValueChanged
        Dim ArrivalDate As DateTime = dtpArrivalDate.Value
        Dim DepartureDate As DateTime = dtpDepartureDate.Value

        Dim DaysStayed As Int32 = ArrivalDate.Subtract(DepartureDate).Days
        LblTNoOfDays.Text = clsCommon.myCdbl(DaysStayed)
    End Sub
    Private Sub dtpDepartureDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDepartureDate.ValueChanged
        Dim ArrivalDate As DateTime = dtpArrivalDate.Value
        Dim DepartureDate As DateTime = dtpDepartureDate.Value

        Dim DaysStayed As Int32 = ArrivalDate.Subtract(DepartureDate).Days
        LblTNoOfDays.Text = clsCommon.myCdbl(DaysStayed)

    End Sub

    Private Sub dtpHStayFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpHStayFrom.ValueChanged
        Dim StayFrom As DateTime = dtpHStayFrom.Value
        Dim StayTo As DateTime = dtpStayTo.Value

        Dim DaysStayed As Int32 = StayTo.Subtract(StayFrom).Days
        If StayTo > StayFrom Then
            LblDays.Text = clsCommon.myCdbl(DaysStayed)
            LblNight.Text = clsCommon.myCdbl(DaysStayed) - 1
        Else
            LblDays.Text = "0"
            LblNight.Text = "0"
        End If
        
    End Sub

    Private Sub dtpStayTo_ValueChanged(sender As Object, e As EventArgs) Handles dtpStayTo.ValueChanged
        Dim StayFrom As DateTime = dtpHStayFrom.Value
        Dim StayTo As DateTime = dtpStayTo.Value

        Dim DaysStayed As Int32 = StayTo.Subtract(StayFrom).Days
        LblDays.Text = clsCommon.myCdbl(DaysStayed) + 1
        LblNight.Text = clsCommon.myCdbl(DaysStayed)
    End Sub

    Private Sub TxtTClass__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTClass._MYValidating
        TxtTClass.Value = ClsHRTravelClassTypeMaster.GetFinder("", TxtTClass.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTClass.Value) > 0 Then
            LblTClass.Text = clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_HR_TRAVEL_CLASS_TYPE_MASTER WHERE Travel_Class_Code='" + TxtTClass.Value + "'")
        Else
            LblTClass.Text = ""
        End If
    End Sub

    Private Sub TxtTravelPur__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTravelPur._MYValidating
        TxtTravelPur.Value = ClsHRTravelPurposeMaster.GetFinder("", TxtTravelPur.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTravelPur.Value) > 0 Then
            LblTravelPur.Text = clsDBFuncationality.getSingleValue("select Travel_Desp from TSPL_HR_TRAVEL_PURPOSE_MASTER where Travel_Code='" + TxtTravelPur.Value + "'")
        Else
            LblTravelPur.Text = ""
        End If
    End Sub
    Private Sub TxtBookedByName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtBookedByName._MYValidating
        TxtBookedByName.Value = clsEmployeeMaster.getFinder("", TxtBookedByName.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtBookedByName.Value) > 0 Then
            LblBookedByName.Text = clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + TxtTravelPur.Value + "'")
        Else
            LblBookedByName.Text = ""
        End If
    End Sub

    Private Sub TxtBookingFor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtBookingFor._MYValidating
        TxtBookingFor.Value = clsEmployeeMaster.getFinder("", TxtBookingFor.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtBookingFor.Value) > 0 Then
            LblBookingFor.Text = clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + TxtBookingFor.Value + "'")
        Else
            LblBookingFor.Text = ""
        End If
    End Sub
End Class

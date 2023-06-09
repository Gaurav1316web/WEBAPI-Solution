Imports common

Public Class FrmGenerateFreshBooking
    Inherits FrmMainTranScreen
    Dim SettTagMultipleRouteWithCustomer As Boolean = False

    Dim ButtonTooltip As New ToolTip()
    Dim isshowMessage As Boolean = True
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub FrmBulkPostingNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SettTagMultipleRouteWithCustomer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TagMultipleRouteWithCustomer, clsFixedParameterCode.TagMultipleRouteWithCustomer, Nothing))
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C for Close the Window")
        ButtonTooltip.SetToolTip(btnShow, "Press Alt+R for Refresh the Data")
        SetUserMgmtNew()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtBookingDate.Value = clsCommon.GETSERVERDATE().AddDays(1)
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False
        ''richa VIJ/03/12/19-000092
        cmbBookingType.Text = "CD"
        cmbBookingType.Enabled = False
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocation.Text = clsCommon.myCstr(clsLocation.GetName(txtLocation.Value, Nothing))
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If clsCommon.CompairString(cmbBookingType.Text, "CD") <> CompairStringResult.Equal Then
                If gv1.CurrentColumn Is gv1.Columns("Reference Booking No") Then
                    clsOpenTransactionForm.OpenTransacionForm("", clsCommon.myCstr(gv1.CurrentRow.Cells("Reference Booking No").Value))
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Dim qry As String = "Select Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
        txtRouteNo.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
        Dim sql As String = "Select Route_Desc,Employee_Code from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "'"
        Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then
            lblRouteDesc.Text = dr1.Rows(0)(0).ToString()
        Else
            lblRouteDesc.Text = String.Empty
        End If
    End Sub


    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
        Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
          
            'If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
            '    Throw New Exception("Please select Route")
            'End If

            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.CompairString(cmbBookingType.Text, "Select") = CompairStringResult.Equal Then
                Throw New Exception("Please select Booking Type")
            End If
            isshowMessage = True
            LoadBooking()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub LoadBooking()
        Try
            Dim strqry As String = String.Empty
            Dim strBookingType As String = cmbBookingType.Text

            'strqry = "select cast(1 as bit) as Sel ,*,case when ActualBookingType='CD' then case when number1 =No_Of_Days then 'CD_Free' else 'Normal' end  else 'Normal'  end booking_type from (select [Customer Code] , max(Customer_Name) as [Customer Name],max(Card_SALE_No ) as Card_SALE_No,max([Reference Booking No]) as [Reference Booking No],  Against_Booking_No,count(Against_Booking_No)+1 as number1,max(No_Of_Days) as  No_Of_Days,convert(varchar,max(Document_Date),103) as Document_Date,max(ActualBookingType) as ActualBookingType, max (route_no) as route_no from (select distinct TSPL_BOOKING_DETAIL.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_MATSER.card_sale_no,TSPL_CARD_SALE.No_Of_Days ,TSPL_BOOKING_MATSER.Document_No as [Reference Booking No], TSPL_BOOKING_MATSER.Against_Booking_No,TSPL_BOOKING_MATSER.Document_Date as Document_Date ,isnull(tspl_booking_matser.booking_type,'') as ActualBookingType, TSPL_BOOKING_DETAIL.route_no  from TSPL_BOOKING_MATSER " & Environment.NewLine & _
            ' " left outer join TSPL_CARD_SALE on TSPL_CARD_SALE.Card_No =TSPL_BOOKING_MATSER.card_sale_no " & Environment.NewLine & _
            ' " left outer join TSPL_BOOKING_DETAIL on tspl_booking_matser.Document_No =TSPL_BOOKING_DETAIL.Document_No " & Environment.NewLine & _
            ' " left outer join TSPL_CUSTOMER_MASTER on  TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code" & Environment.NewLine & _
            ' " where tspl_booking_matser.Posted =1 and tspl_booking_matser.booking_type='" & strBookingType & "' and tspl_booking_matser.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_MATSER.From_Screen_code='" & clsUserMgtCode.frmbookingdairyFreshSale & "' " & Environment.NewLine

            strqry = "select cast(1 as bit) as Sel ,*,case when ActualBookingType='CD' then case when number1 =No_Of_Days then 'CD_Free' else 'Normal' end  else 'Normal'  end booking_type from (select [Customer Code] , max(Customer_Name) as [Customer Name],max(Card_SALE_No ) as Card_SALE_No,max([Reference Booking No]) as [Reference Booking No],  Against_Booking_No,count(Against_Booking_No)+1 as number1,max(No_Of_Days) as  No_Of_Days,convert(varchar,max(Document_Date),103) as Document_Date,max(ActualBookingType) as ActualBookingType, max (route_no) as route_no from (select distinct TSPL_BOOKING_DETAIL.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_MATSER.card_sale_no,TSPL_CARD_SALE.No_Of_Days ,TSPL_BOOKING_MATSER.Document_No as [Reference Booking No], TSPL_BOOKING_MATSER.Against_Booking_No,TSPL_BOOKING_MATSER.Document_Date as Document_Date ,isnull(tspl_booking_matser.booking_type,'') as ActualBookingType, TSPL_BOOKING_DETAIL.route_no  from TSPL_BOOKING_MATSER " & Environment.NewLine &
             " left outer join TSPL_CARD_SALE on TSPL_CARD_SALE.Card_No =TSPL_BOOKING_MATSER.card_sale_no " & Environment.NewLine &
             " left outer join TSPL_BOOKING_DETAIL on tspl_booking_matser.Document_No =TSPL_BOOKING_DETAIL.Document_No " & Environment.NewLine &
             " left outer join TSPL_CUSTOMER_MASTER on  TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code" & Environment.NewLine &
             " where  tspl_booking_matser.booking_type='" & strBookingType & "' and tspl_booking_matser.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_MATSER.From_Screen_code='" & "' " & Environment.NewLine



            If txtCustomerNo.arrValueMember IsNot Nothing AndAlso txtCustomerNo.arrValueMember.Count > 0 Then
                strqry += " and TSPL_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomerNo.arrValueMember) + ")  "
            End If
            If fndRouteNo.arrValueMember IsNot Nothing AndAlso fndRouteNo.arrValueMember.Count > 0 Then
                strqry += " and TSPL_BOOKING_DETAIL.route_no in (" + clsCommon.GetMulcallString(fndRouteNo.arrValueMember) + " ) "
            End If

            strqry += "  and Against_Booking_No in (select BM.Document_No from TSPL_BOOKING_MATSER BM where BM.booking_type='" & strBookingType & "' and BM.location_code ='" & txtLocation.Value & "'  and BM.From_Screen_code='" & "') " & Environment.NewLine
            If isshowMessage = False Then
                strqry += " UNION " & Environment.NewLine &
                " select distinct TSPL_BOOKING_DETAIL.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_MATSER.card_sale_no,TSPL_CARD_SALE.No_Of_Days ,TSPL_BOOKING_MATSER.Document_No as [Reference Booking No], TSPL_BOOKING_MATSER.Against_Booking_No,TSPL_BOOKING_MATSER.Document_Date as Document_Date ,isnull(tspl_booking_matser.booking_type,'') as ActualBookingType,TSPL_BOOKING_DETAIL.route_no  from TSPL_BOOKING_MATSER " & Environment.NewLine &
                " left outer join TSPL_CARD_SALE on TSPL_CARD_SALE.Card_No =TSPL_BOOKING_MATSER.card_sale_no " & Environment.NewLine &
                " left outer join TSPL_BOOKING_DETAIL on tspl_booking_matser.Document_No =TSPL_BOOKING_DETAIL.Document_No " & Environment.NewLine &
                " left outer join TSPL_CUSTOMER_MASTER on  TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code" & Environment.NewLine &
                " where  tspl_booking_matser.booking_type='" & strBookingType & "' and tspl_booking_matser.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_MATSER.From_Screen_code='" & "' " & Environment.NewLine

                If txtCustomerNo.arrValueMember IsNot Nothing AndAlso txtCustomerNo.arrValueMember.Count > 0 Then
                    strqry += " and TSPL_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomerNo.arrValueMember) + ")  "
                End If
                If fndRouteNo.arrValueMember IsNot Nothing AndAlso fndRouteNo.arrValueMember.Count > 0 Then
                    strqry += " and TSPL_BOOKING_DETAIL.route_no in (" + clsCommon.GetMulcallString(fndRouteNo.arrValueMember) + " ) "
                End If
                strqry += "   and Against_Booking_No in (select BM.Document_No from TSPL_BOOKING_MATSER BM where BM.booking_type='" & strBookingType & "' and BM.location_code ='" & txtLocation.Value & "'  and BM.From_Screen_code='" & "' )"

            End If



            strqry += "  )AgainstBooking group by Against_Booking_No,[Customer Code] having count(Against_Booking_No)+1<=max(No_Of_Days) " & Environment.NewLine &
                           " union all " & Environment.NewLine &
            " select distinct TSPL_BOOKING_DETAIL.Cust_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_MATSER.card_sale_no,TSPL_BOOKING_MATSER.Document_No as [Reference Booking No], TSPL_BOOKING_MATSER.Against_Booking_No,1 as number1,case when isnull(tspl_booking_matser.booking_type,'')='CD' THEN TSPL_CARD_SALE.No_Of_Days ELSE 1 END as No_Of_Days,convert(varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date ,isnull(tspl_booking_matser.booking_type,'') as ActualBookingType,TSPL_BOOKING_DETAIL.route_no  from TSPL_BOOKING_MATSER" & Environment.NewLine &
            " left outer join TSPL_CARD_SALE on TSPL_CARD_SALE.Card_No =TSPL_BOOKING_MATSER.card_sale_no" & Environment.NewLine &
            " left outer join TSPL_BOOKING_DETAIL on tspl_booking_matser.Document_No =TSPL_BOOKING_DETAIL.Document_No " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on  TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code" & Environment.NewLine &
            " where  tspl_booking_matser.Posted =1 and " & Environment.NewLine &
            " tspl_booking_matser.booking_type='" & strBookingType & "' and tspl_booking_matser.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_MATSER.From_Screen_code='" & "' " & Environment.NewLine
            If txtCustomerNo.arrValueMember IsNot Nothing AndAlso txtCustomerNo.arrValueMember.Count > 0 Then
                strqry += " and TSPL_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomerNo.arrValueMember) + ")  "
            End If
            If fndRouteNo.arrValueMember IsNot Nothing AndAlso fndRouteNo.arrValueMember.Count > 0 Then
                strqry += " and TSPL_BOOKING_DETAIL.route_no in (" + clsCommon.GetMulcallString(fndRouteNo.arrValueMember) + " ) "
            End If

            strqry += "  and  isnull(Against_Booking_No,'') ='' and TSPL_BOOKING_MATSER.Document_No not in (select distinct  TSPL_BOOKING_MATSER.Against_Booking_No   from TSPL_BOOKING_MATSER" & Environment.NewLine &
            " left outer join TSPL_CARD_SALE on TSPL_CARD_SALE.Card_No =TSPL_BOOKING_MATSER.card_sale_no" & Environment.NewLine &
            " left outer join TSPL_BOOKING_DETAIL on tspl_booking_matser.Document_No =TSPL_BOOKING_DETAIL.Document_No " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on  TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code" & Environment.NewLine &
            " where  tspl_booking_matser.booking_type='" & strBookingType & "' and tspl_booking_matser.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_MATSER.From_Screen_code='" & "' " & Environment.NewLine

            '" where tspl_booking_matser.Posted =1 and tspl_booking_matser.booking_type='" & strBookingType & "' and tspl_booking_matser.location_code ='" & txtLocation.Value & "' " & Environment.NewLine

            If txtCustomerNo.arrValueMember IsNot Nothing AndAlso txtCustomerNo.arrValueMember.Count > 0 Then
                strqry += " and TSPL_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomerNo.arrValueMember) + ")  "
            End If
            If fndRouteNo.arrValueMember IsNot Nothing AndAlso fndRouteNo.arrValueMember.Count > 0 Then
                strqry += " and TSPL_BOOKING_DETAIL.route_no  in (" + clsCommon.GetMulcallString(fndRouteNo.arrValueMember) + " ) "
            End If

            strqry += " and Against_Booking_No in (select BM.Document_No from TSPL_BOOKING_MATSER BM where BM.booking_type='" & strBookingType & "' and BM.location_code ='" & txtLocation.Value & "' and BM.From_Screen_code='" & "' ) ) ) final " & Environment.NewLine &
                " where final.Document_Date='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.DataSource = dt
                gv1.BestFitColumns()
                For j As Integer = 1 To gv1.MasterTemplate.Columns.Count - 1
                    gv1.MasterTemplate.Columns(j).ReadOnly = True

                    gv1.Columns("number1").IsVisible = False
                    gv1.Columns("No_Of_Days").IsVisible = False
                    gv1.Columns("ActualBookingType").IsVisible = False
                    gv1.Columns("booking_type").IsVisible = False
                Next

                isshowMessage = False
            Else
                gv1.DataSource = Nothing
                If isshowMessage = True Then
                    Throw New Exception("No data found to Generate Booking.")
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub



    Private Sub txtCustomerNo__My_Click(sender As Object, e As EventArgs) Handles txtCustomerNo._My_Click
        Try
            'If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please first select route")
            '    txtRouteNo.Focus()
            '    Exit Sub
            'End If
            Dim qry As String = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Name],TSPL_CUSTOMER_MASTER.Alies_Name as [Short Name],TSPL_CUSTOMER_MASTER.Route_No" & Environment.NewLine & _
             " from TSPL_CUSTOMER_MASTER "

            txtCustomerNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSelCust", qry, "Code", "Name", txtCustomerNo.arrValueMember, txtCustomerNo.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            isshowMessage = True
            fndRouteNo.arrValueMember = Nothing
            txtRouteNo.Value = ""
            txtLocation.Value = ""
            cmbBookingType.Text = "CD"
            cmbBookingType.Enabled = False
            txtDate.Value = clsCommon.GETSERVERDATE()
            txtBookingDate.Value = clsCommon.GETSERVERDATE().AddDays(1)
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            ''richa VIJ/03/12/19-000092
            txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsCommon.myCstr(clsLocation.GetName(txtLocation.Value, Nothing))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim obj As clsBookingEntryDairySale = Nothing
        Dim objDairySale As clsBookingEntryDairySale = Nothing
        Dim objTr As clsBookingDetailDairySale = Nothing
        Try
            If gv1.Rows.Count > 0 Then
                Dim counter As Integer = 0
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells("Sel").Value) = True Then
                        obj = New clsBookingEntryDairySale
                        objDairySale = New clsBookingEntryDairySale
                        objDairySale = clsBookingEntryDairySale.GetData(grow.Cells("Reference Booking No").Value, NavigatorType.Current, clsUserMgtCode.frmDairyBookingCustomer)
                        Dim dairyDocumentExist As Integer = 0
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Against_Booking_No").Value)) > 0 Then
                            dairyDocumentExist = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BOOKING_MATSER where Against_Booking_No ='" & clsCommon.myCstr(grow.Cells("Against_Booking_No").Value) & "' and Booking_Type ='CD' and From_Screen_Code ='BOOK-DS_FSH' and Document_Date ='" & clsCommon.GetPrintDate(txtBookingDate.Value, "dd/MMM/yyyy") & "'"))
                        Else
                            dairyDocumentExist = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BOOKING_MATSER where Against_Booking_No ='" & clsCommon.myCstr(grow.Cells("Reference Booking No").Value) & "' and Booking_Type ='CD' and From_Screen_Code ='BOOK-DS_FSH' and Document_Date ='" & clsCommon.GetPrintDate(txtBookingDate.Value, "dd/MMM/yyyy") & "'"))
                        End If
                        If dairyDocumentExist > 0 Then
                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Against_Booking_No").Value)) > 0 Then
                                Throw New Exception("Booking already generated for Against Booking No " & clsCommon.myCstr(grow.Cells("Against_Booking_No").Value) & " and Booking Date " & txtBookingDate.Value & " ")
                            Else
                                Throw New Exception("Booking already generated for Against Booking No " & clsCommon.myCstr(grow.Cells("Reference Booking No").Value) & " and Booking Date " & txtBookingDate.Value & " ")
                            End If

                        End If

                        If (objDairySale IsNot Nothing AndAlso clsCommon.myLen(objDairySale.Document_No) > 0) Then
                            '-----------------start

                            ''obj.Document_No = txtDocNo.Value
                            obj.Document_Date = txtBookingDate.Value
                            obj.location_code = objDairySale.location_code
                            obj.Is_Taxable = 2   ' 2 for Taxable and NonTaxable item in a single booking
                          
                            obj.TRANSACTION_TYPE = objDairySale.TRANSACTION_TYPE
                            'obj.From_Screen_code = clsUserMgtCode.frmbookingdairyFreshSale
                            obj.SalesmanCode = objDairySale.SalesmanCode
                            obj.Cust_PO_No = objDairySale.Cust_PO_No
                            obj.Podate = objDairySale.Podate


                            obj.TotalCAN = objDairySale.TotalCAN
                            obj.TotalCrate = objDairySale.TotalCrate
                            obj.TotalBox = objDairySale.TotalBox
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("booking_type").Value), "CD_Free") = CompairStringResult.Equal Then
                                obj.IsSampling = 1
                            Else
                                obj.IsSampling = objDairySale.IsSampling
                            End If
                            obj.Booking_Type = objDairySale.Booking_Type
                            obj.Ex_Factory_Date = objDairySale.Ex_Factory_Date

                            obj.Card_SALE_No = objDairySale.Card_SALE_No
                            obj.CardSale_FROM_DATE = objDairySale.CardSale_FROM_DATE
                            obj.CardSale_TO_DATE = objDairySale.CardSale_TO_DATE

                            obj.Reference_No = objDairySale.Reference_No
                            obj.Counter_No = objDairySale.Counter_No
                            obj.Payment_Mode = objDairySale.Payment_Mode
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("ActualBookingType").Value), "CD") = CompairStringResult.Equal Then
                                If clsCommon.myLen(clsCommon.myCstr(objDairySale.Against_Booking_No)) <= 0 Then
                                    obj.Against_Booking_No = objDairySale.Document_No
                                Else
                                    obj.Against_Booking_No = objDairySale.Against_Booking_No
                                End If
                            End If

                            Dim qry As String = "SELECT TSPL_BOOKING_DETAIL.* FROM TSPL_BOOKING_DETAIL  WHERE Document_No='" + grow.Cells("Reference Booking No").Value + "' and scheme_item='N' "
                            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                            obj.Arr = New List(Of clsBookingDetailDairySale)
                            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                                For Each dt As DataRow In dt2.Rows
                                    objTr = New clsBookingDetailDairySale()
                                    objTr.Line_No = dt.Item("Line_No")
                                    objTr.Cust_Code = clsCommon.myCstr(dt.Item("Cust_Code"))
                                    objTr.Loc_Code = clsCommon.myCstr(dt.Item("Loc_Code"))
                                    objTr.Item_Code = clsCommon.myCstr(dt.Item("Item_Code"))

                                    objTr.Booking_Qty = clsCommon.myCdbl(dt.Item("Booking_Qty"))
                                    objTr.Short_Description = clsCommon.myCstr(dt.Item("Short_Description"))
                                    objTr.Unit_code = clsCommon.myCstr(dt.Item("Unit_code"))
                                    objTr.DocumentAmount = clsCommon.myCdbl(dt.Item("DocumentAmount"))
                                    objTr.Vehicle_Code = clsCommon.myCstr(dt.Item("Vehicle_Code"))
                                    objTr.Item_Rate = clsCommon.myCdbl(dt.Item("Item_Rate"))

                                    objTr.Total_Qty = clsCommon.myCdbl(dt.Item("Total_Qty"))
                                    ' objTr.Sampling = clsCommon.myCstr(dt.Item("Sampling"))
                                    If clsCommon.CompairString(obj.IsSampling, "1") = CompairStringResult.Equal Then
                                        objTr.Sampling = 1
                                    Else
                                        objTr.Sampling = 0
                                    End If

                                    objTr.Disc_Scheme_Code = clsCommon.myCstr(dt.Item("Disc_Scheme_Code"))
                                    objTr.Disc_Scheme_Type = clsCommon.myCstr(dt.Item("Disc_Scheme_Type"))
                                    objTr.Disc_Scheme_Pers = clsCommon.myCdbl(dt.Item("Disc_Scheme_Pers"))
                                    objTr.Disc_Scheme_Amount = clsCommon.myCdbl(dt.Item("Disc_Scheme_Amount"))
                                    objTr.OrgRate = clsCommon.myCdbl(dt.Item("OrgRate"))

                                    'objTr.Scheme_Item_Code = clsCommon.myCstr(dt.Item("Scheme_Item_Code"))
                                    'objTr.Scheme_Qty = clsCommon.myCdbl(dt.Item("Scheme_Qty"))
                                    'objTr.Scheme_Item_UOM = clsCommon.myCstr(dt.Item("Scheme_Item_UOM"))
                                    ' objTr.Scheme_Code = clsCommon.myCstr(dt.Item("Scheme_Code"))
                                    objTr.SellingPrice = clsCommon.myCdbl(dt.Item("Item_Selling_Price"))
                                    objTr.Tax_On_Amount = clsCommon.myCdbl(dt.Item("Tax_On_Amount"))
                                    objTr.Tax_Amount = clsCommon.myCdbl(dt.Item("Tax_Amount"))
                                    ' objTr.SchemeType = clsCommon.myCstr(dt.Item("Scheme_Type"))

                                    objTr.Price_with_Tax = clsCommon.myCdbl(dt.Item("Price_with_Tax"))
                                    objTr.Amount_with_Tax = clsCommon.myCdbl(dt.Item("Amount_with_Tax"))

                                    objTr.Item_Price_ID = clsCommon.myCstr(dt.Item("Item_Price_ID"))
                                    If clsCommon.myLen(clsCommon.myCstr(dt.Item("Price_IdStartDate"))) > 0 Then
                                        objTr.Price_IdStartDate = clsCommon.myCDate(dt.Item("Price_IdStartDate"))
                                    End If

                                    objTr.PricePlanNo = clsCommon.myCstr(dt.Item("PricePlanNo"))


                                    Dim DOCdateCurrent As Date? = Nothing
                                    DOCdateCurrent = clsCommon.GETSERVERDATE()
                                    ' Query to get scheme type of Item
                                    Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
                                    qryScheme += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
                                    qryScheme += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
                                    qryScheme += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
                                    qryScheme += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + clsCommon.myCstr(dt.Item("Item_Code")) + "' and Cust_Code='" + clsCommon.myCstr(dt.Item("Cust_Code")) + "'))a where a.sno=1)"
                                    qryScheme += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
                                    qryScheme += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + clsCommon.myCstr(dt.Item("Item_Code")) + "' "


                                    qryScheme += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
                                    Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme)
                                    If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                        Dim objD As clsSchemeApplyOnDairy = Nothing
                                        objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(dt.Item("Item_Code")), clsCommon.myCstr(dt.Item("Unit_code")), clsCommon.myCdbl(dt.Item("Booking_Qty")), clsCommon.myCstr(dt.Item("Cust_Code")), clsCommon.myCstr(dt.Item("Scheme_Type")), Nothing, Nothing)

                                        If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                            For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                                objTr.SchemeType = objtrScheme.schm_Type
                                                objTr.Scheme_Item_Code = objtrScheme.Schm_Icode
                                                objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                                objTr.Scheme_Item_UOM = objtrScheme.Schm_Item_Uom
                                                objTr.Scheme_Code = objtrScheme.Schm_Code
                                            Next

                                        End If
                                    End If
                                    'End of Scheme Type of Detail




                                    objTr.Tax_NonTax = clsCommon.myCstr(dt.Item("Tax_NonTax"))
                                    objTr.FreshAmbient = clsCommon.myCstr(dt.Item("FreshAmbient"))
                                    objTr.Remarks = clsCommon.myCstr(dt.Item("Remarks"))
                                    objTr.Route_No = clsCommon.myCstr(dt.Item("route_no"))
                                    obj.Arr.Add(objTr)
                                Next

                            End If

                        End If

                        If (obj.SaveData(obj, True)) = True Then
                            'counter = grow.Index
                            'gv1.Rows.RemoveAt(counter)

                            'If gv1.Rows.Count = 0 Then
                            '    gv1.Rows.AddNew()
                            'End If
                        End If
                    End If
                Next
                clsCommon.MyMessageBoxShow("Booking Created Successfully")
            End If
            LoadBooking()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDate_ValueChanged(sender As Object, e As EventArgs) Handles txtDate.ValueChanged
        Try
            txtBookingDate.Value = txtDate.Value.AddDays(1)
            gv1.DataSource = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndRouteNo__My_Click(sender As Object, e As EventArgs) Handles fndRouteNo._My_Click
        Try
            
            Dim qry As String = " Select Route_No as Code,Route_Desc as Name,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER "
            fndRouteNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSelRoute", qry, "Code", "Name", fndRouteNo.arrValueMember, fndRouteNo.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class

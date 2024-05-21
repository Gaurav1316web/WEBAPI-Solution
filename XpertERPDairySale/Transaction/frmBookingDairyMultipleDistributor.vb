' '' '' ''Created By priti for ticket BM00000003094
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net

Public Class frmBookingDairyMultipleDistributor
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim blnPageLoad As Boolean = False
    Dim intChangeColumn As Integer = 0
    Public StrDocNo As String = ""
    Public arrBookingItem As List(Of clsBookingTemp) = Nothing
    Dim blnSaveTotalQTy As Boolean = False
    Dim DOmsg As String = ""
    Private isNewEntry As Boolean = False
    Private DOCreated As Boolean = False
    Dim AllowWo_Outstanding As Boolean
    Dim CheckOutstandingOnbooking As Integer = 0
    Dim ShowItemLocationWiseonBooking As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colLocCode As String = "colLocCode"
    Const colLocName As String = "colLocName"
    Const ReportID As String = "DairyBookingGrid"
    Const colQty As String = "colQty"
    Const colICode As String = "colICode"
    Const colIDesc As String = "colIDesc"
    Const colUnit As String = "colUnit"
    Const colTotalQty As String = "colTotalQty"
    Const colISeqNo As String = "colISeqNo"
    Const colIGroup As String = "colIGroup"
    Dim strSql As String
    Dim dblCustOutstandingAmt As Double = 0

#End Region

    Private Sub FrmBookingEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(1) Then
                gv1.CurrentColumn = gv1.Columns(2)
                OpenCustomerFinder(True)
                gv1.CurrentColumn = gv1.Columns(1)
            ElseIf gv1.CurrentColumn Is gv1.Columns(3) Then
                gv1.CurrentColumn = gv1.Columns(4)
                OpenLocationFinder(True)
                gv1.CurrentColumn = gv1.Columns(3)
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            Post()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub FrmBookingEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            blnPageLoad = True
            btnCopyOrder.Visible = False
            btnPerformaInvoice.Visible = False
            ShowItemLocationWiseonBooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowItemLocationWiseonDairyBooking, clsFixedParameterCode.ShowItemLocationWiseonDairyBooking, Nothing))
            CheckOutstandingOnbooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckOutstandingCreditLimitOnBooking, clsFixedParameterCode.CheckOutstandingCreditLimitOnBooking, Nothing))
            AllowWo_Outstanding = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDispatchOutstandingFS & "'")) = 0, False, True)
            AddNew()
            SetUserMgmtNew()
            If clsCommon.myLen(StrDocNo) > 0 Then
                LoadData(StrDocNo, NavigatorType.Current)
            End If
            If clsCommon.myLen(Me.Tag) > 0 Then
                LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
            End If
            txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            End If
            Dim strCust = clsDBFuncationality.getSingleValue("select top 1 Customer_Code from TSPL_CUSTOMER_LOCATION_MAPPING where Location_Code='" & txtLocation.Value & "'")
            'If clsCommon.myLen(strCust) > 0 Then
            '    LoadBlankGrid()
            'End If
            blnPageLoad = False

            If txtCustGrp.Enabled = False Then
                txtCustGrp.Enabled = True
            End If

            If txtLocation.Enabled = False Then
                txtLocation.Enabled = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmbookingdairy)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag

    End Sub
    Sub LoadBlankGrid()
        blnPageLoad = True
        Dim strItemcode As String = String.Empty
        Dim strtotal As String = String.Empty
        Dim strshortDesp As String = String.Empty
        Dim strtotalShort As String = String.Empty
        Dim strCustCodeString As String = ""
        Dim ISFresh As Integer = 0
        Dim FreshItem As String = String.Empty
        Dim dt1 As DataTable = Nothing
        Dim qry As String = String.Empty
        Dim dt As DataTable = Nothing

        Dim ItemTypeForBooking = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.ItemTypeForDairyBooking & "'"))
        Dim strItem As String = ""
        If ItemTypeForBooking = "B" Then
            strItem = "(Is_FreshItem =1 or Is_Ambient=1)"
        ElseIf ItemTypeForBooking = "F" Then
            strItem = "Is_FreshItem =1 "
        ElseIf ItemTypeForBooking = "A" Then
            strItem = "Is_Ambient=1"
        End If

        Try
            qry = "select TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code as Customer_Code,Vehicle_Id,TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_LOCATION_MAPPING left outer join" & _
                  " TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_ROUTE_MASTER on" & _
                  " TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id" & _
                  " left Outer Join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SECONDARY_CUSTOMER_MASTER.Parent_Customer_No where" & _
                  " TSPL_SECONDARY_CUSTOMER_MASTER.Parent_Customer_No='" & txtCustomer.Value & "' and" & _
                 " TSPL_CUSTOMER_LOCATION_MAPPING.Location_Code='" & txtLocation.Value & "' and TSPL_CUSTOMER_MASTER.Cust_Group_Code='" & txtCustGrp.Value & "' order by TSPL_VEHICLE_MASTER.SequenceNo,TSPL_CUSTOMER_LOCATION_MAPPING.SequenceNo "

            dt = clsDBFuncationality.GetDataTable(qry)

            For Each dr As DataRow In dt.Rows
                strshortDesp = clsCommon.myCstr(dr("Customer_Code"))
                If clsCommon.myLen(strshortDesp) > 0 Then
                    strtotalShort = strtotalShort + "," + "[" + strshortDesp + "]"
                    strCustCodeString = strCustCodeString + "," + "  isnull(" & "Sum(" & "[" & strshortDesp & "]" & " ) " & ",0)  " & "as  " & "[" & strshortDesp & "]" & ""
                End If
            Next

            If clsCommon.myLen(strtotalShort) = 0 Then
                gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "Please MAP Customer with Location . " & txtLocation.Value, Me.Text)
                blnPageLoad = False
                Exit Sub
            End If

            Try
                If strtotalShort.Substring(0, 1) = "," Then
                    strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
                    strCustCodeString = strCustCodeString.Substring(1, strCustCodeString.Length - 1)
                End If
            Catch ex As Exception
                blnPageLoad = False
            End Try
            If ShowItemLocationWiseonBooking = 0 Then
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    qry = "select max(line_no) as Line_No,Item_Code,max(Item_Desc) as Item_Desc,max(CSA_TYPE) as CSA_TYPE,max(UOM_Code) as UOM_Code,0 as Total_Qty,max(Sku_Seq) as Sku_Seq," & strCustCodeString & "  from (select  distinct Line_No,tspl_item_master.Item_Code,tspl_item_master.Item_Desc,tspl_item_master.CSA_TYPE ,TSPL_BOOKING_DETAIL.Unit_code as UOM_Code,0 as Total_Qty,Sku_Seq,isnull(TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code,'') as Distributor_Code, isnull(Booking_Qty,0) as Qty  from TSPL_BOOKING_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER  ON TSPL_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_LOCATION_MASTER  ON TSPL_BOOKING_DETAIL.Loc_Code =TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN TSPL_ROUTE_MASTER  ON TSPL_ROUTE_MASTER.Route_No   =TSPL_CUSTOMER_MASTER.Route_No LEFT OUTER JOIN TSPL_VEHICLE_MASTER  ON TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_BOOKING_DETAIL.Vehicle_Code left Outer Join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_SECONDARY_CUSTOMER_MASTER.Parent_Customer_No=TSPL_CUSTOMER_MASTER.Cust_Code left Outer Join TSPL_BOOKING_MATSER on   TSPL_BOOKING_Matser.Main_Booking_No=TSPL_BOOKING_DETAIL.Main_Booking_No and TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_Matser.Distributor_Code and TSPL_BOOKING_Matser.Document_No=TSPL_BOOKING_DETAIL.Document_No  where isnull(TSPL_BOOKING_DETAIL.Scheme_Item,'N')<>'Y' and isnull(TSPL_BOOKING_DETAIL.FOC_Item,'0')<>'1' and  TSPL_BOOKING_Matser.Main_Booking_No='" & txtDocNo.Value & "' union all " & _
                        "select  1 as Line_No ,tspl_item_master.Item_Code,tspl_item_master.Item_Desc,tspl_item_master.CSA_TYPE,TSPL_ITEM_UOM_DETAIL.UOM_Code,0.00 as Total_Qty ,Sku_Seq,'' as Cust_Code, 0.00 as Qty from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on tspl_item_master.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  where " & strItem & "  and tspl_item_master.Item_Code not in (select TSPL_BOOKING_DETAIL.Item_Code from TSPL_BOOKING_Matser left Outer join TSPL_BOOKING_DETAIL on  TSPL_BOOKING_Matser.Main_Booking_No=TSPL_BOOKING_DETAIL.Main_Booking_No and TSPL_BOOKING_Matser.Document_No=TSPL_BOOKING_DETAIL.Document_No where TSPL_BOOKING_Matser.Main_Booking_No='" & txtDocNo.Value & "' ) " & _
                        ") as t pivot(sum(Qty) for Distributor_Code in (" & strtotalShort & ") ) as pivot1 group by Item_Code order by Sku_Seq asc"
                Else
                    qry = "select * from (select  1 as Line_No ,tspl_item_master.Item_Code,tspl_item_master.Item_Desc,tspl_item_master.CSA_TYPE ,TSPL_ITEM_UOM_DETAIL.UOM_Code,0.00 as Total_Qty,Sku_Seq,'' as Distributor_Code, 0.00 as Qty  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on tspl_item_master.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  where " & strItem & " ) as t pivot(max(Qty) for Distributor_Code in (" & strtotalShort & ") ) as pivot1 order by Sku_Seq asc"
                End If
            Else
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    qry = "select select max(line_no) as Line_No,Item_Code,max(Item_Desc) as Item_Desc,max(CSA_TYPE) as CSA_TYPE,max(UOM_Code) as UOM_Code,0 as Total_Qty,max(Sku_Seq) as Sku_Seq," & strCustCodeString & " from (select  distinct Line_No,tspl_item_master.Item_Code,tspl_item_master.Item_Desc,tspl_item_master.CSA_TYPE ,TSPL_BOOKING_DETAIL.Unit_code as UOM_Code,0.00 as Total_Qty,Sku_Seq,isnull(tspl_customer_master.Cust_Code,'') as Cust_Code, isnull(Booking_Qty,0) as Qty  from TSPL_BOOKING_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER  ON TSPL_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_LOCATION_MASTER  ON TSPL_BOOKING_DETAIL.Loc_Code =TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN TSPL_ROUTE_MASTER  ON TSPL_ROUTE_MASTER.Route_No   =TSPL_CUSTOMER_MASTER.Route_No LEFT OUTER JOIN TSPL_VEHICLE_MASTER  ON TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_BOOKING_DETAIL.Vehicle_Code  where isnull(TSPL_BOOKING_DETAIL.Scheme_Item,'N')<>'Y' and isnull(TSPL_BOOKING_DETAIL.FOC_Item,'0')<>'1' and Main_Booking_No='" & txtDocNo.Value & "' union all " & _
                        "select  1 as Line_No ,tspl_item_master.Item_Code,tspl_item_master.Item_Desc,tspl_item_master.CSA_TYPE,TSPL_ITEM_UOM_DETAIL.UOM_Code,0.00 as Total_Qty ,Sku_Seq,'' as Distributor_Code, 0.00 as Qty from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on tspl_item_master.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 left outer join TSPL_Location_ItemMAPPING on TSPL_ITEM_MASTER.Item_Code=TSPL_Location_ItemMAPPING.Item_code   where " & strItem & " and TSPL_Location_ItemMAPPING.Location_Code='" & txtLocation.Value & "' and tspl_item_master.Item_Code not in (select TSPL_BOOKING_DETAIL.Item_Code from TSPL_BOOKING_DETAIL where Main_Booking_No='" & txtDocNo.Value & "' ) " & _
                        ") as t pivot(sum(Qty) for Distributor_Code     in (" & strtotalShort & ") ) as pivot1 group by Item_Code  order by Sku_Seq asc"
                Else
                    qry = "select * from (select  1 as Line_No ,tspl_item_master.Item_Code,tspl_item_master.Item_Desc,tspl_item_master.CSA_TYPE ,TSPL_ITEM_UOM_DETAIL.UOM_Code,0.00 as Total_Qty,Sku_Seq,'' as Distributor_Code, 0.00 as Qty  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on tspl_item_master.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 left outer join TSPL_Location_ItemMAPPING on TSPL_ITEM_MASTER.Item_Code=TSPL_Location_ItemMAPPING.Item_code where " & strItem & " and TSPL_Location_ItemMAPPING.Location_Code='" & txtLocation.Value & "') as t pivot(max(Qty) for Distributor_Code in (" & strtotalShort & ") ) as pivot1 order by Sku_Seq asc"
                End If
            End If


            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = clsDBFuncationality.GetDataTable(qry)
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.Columns("Line_No").HeaderText = "Line No"
            gv1.Columns("Line_No").AllowFiltering = False
            gv1.Columns("Line_No").Width = 40
            gv1.Columns("Line_No").IsPinned = True
            gv1.Columns("Line_No").Name = colLineNo


            gv1.Columns("Item_Code").HeaderText = "Item Code"
            gv1.Columns("Item_Code").ReadOnly = True
            gv1.Columns("Item_Code").Width = 100
            'gv1.Columns("Item_Code").PinPosition = PinnedColumnPosition.Left
            gv1.Columns("Item_Code").IsPinned = True
            gv1.Columns("Item_Code").Name = colICode


            gv1.Columns("Item_Desc").HeaderText = "Item Desc"
            gv1.Columns("Item_Desc").ReadOnly = True
            gv1.Columns("Item_Desc").Width = 200
            gv1.Columns("Item_Desc").IsPinned = True
            'gv1.Columns("Item_Desc").PinPosition = PinnedColumnPosition.Left
            gv1.Columns("Item_Desc").Name = colIDesc

            gv1.Columns("CSA_TYPE").HeaderText = "Item Group"
            gv1.Columns("CSA_TYPE").ReadOnly = True
            gv1.Columns("CSA_TYPE").Width = 80
            gv1.Columns("CSA_TYPE").IsPinned = True
            'gv1.Columns("CSA_TYPE").PinPosition = PinnedColumnPosition.Left
            gv1.Columns("CSA_TYPE").Name = colIGroup


            gv1.Columns("UOM_Code").HeaderText = "Unit"
            gv1.Columns("UOM_Code").ReadOnly = True
            gv1.Columns("UOM_Code").Width = 50
            gv1.Columns("UOM_Code").IsPinned = True
            'gv1.Columns("UOM_Code").PinPosition = PinnedColumnPosition.Left
            gv1.Columns("UOM_Code").Name = colUnit


            gv1.Columns("Sku_Seq").HeaderText = "Sku_Seq"
            gv1.Columns("Sku_Seq").ReadOnly = True
            gv1.Columns("Sku_Seq").Width = 80
            gv1.Columns("Sku_Seq").IsVisible = False
            'gv1.Columns("Sku_Seq").PinPosition = PinnedColumnPosition.Left
            gv1.Columns("Sku_Seq").Name = colISeqNo


            gv1.Columns("Total_Qty").HeaderText = "Total Qty"
            gv1.Columns("Total_Qty").ReadOnly = True
            gv1.Columns("Total_Qty").Width = 50
            gv1.Columns("Total_Qty").ReadOnly = True
            gv1.Columns("Total_Qty").IsPinned = True
            'gv1.Columns("Total_Qty").PinPosition = PinnedColumnPosition.Left
            gv1.Columns("Total_Qty").Name = colTotalQty


            Dim intLine As Integer = 0
            Dim summaryRowItem As New GridViewSummaryRowItem()

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim dblTotal As Double = 0
                intLine += 1
                gv1.Rows(ii).Cells(colLineNo).Value = intLine
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    For jj As Integer = 0 To gv1.Columns.Count - 1
                        If jj >= 7 Then
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(jj).Value) > 0 Then
                                dblTotal += clsCommon.myCdbl(gv1.Rows(ii).Cells(jj).Value)
                            End If
                        End If
                    Next
                    gv1.Rows(ii).Cells(colTotalQty).Value = dblTotal

                End If
            Next


            For ii As Integer = 0 To gv1.Columns.Count - 1

                If ii >= 7 Then
                    Dim strCustVehicleCode As String = ""
                    Dim strRoute As String = ""
                    Dim strCustomerDesc As String = ""
                    Dim DOstatus As Integer = 0
                    Dim strDeliveryNo As String = Nothing
                    Dim strVehicleCode As String = Nothing
                    Dim strPerformaInvoiceNo As String = Nothing
                    Dim strVehicleNumber As String = Nothing
                    Dim strTransport As String = Nothing
                    Dim strBookingStatus As Integer = 0
                    Dim strBookingPostStatus As Integer = 0
                    Dim dblAmount As Double = 0
                    Dim strBookingNo As String = Nothing

                    gv1.Columns(ii).BestFit()
                    gv1.Columns(ii).Width = "200"
                    'gv1.Columns(ii).WrapText = True
                    gv1.Columns(ii).AllowFiltering = False
                    Dim strCustomer = clsCommon.myCstr(gv1.Columns(ii).Name)

                    qry = "select TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name,TSPL_ROUTE_MASTER.vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Number,TSPL_CUSTOMER_MASTER." & _
                          " Zone_Code,TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join " & _
                          " TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left Outer join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_SECONDARY_CUSTOMER_MASTER." & _
                          " Parent_Customer_No=TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_CUSTOMER_MASTER.Cust_Code='" + txtCustomer.Value + "' and TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code='" & clsCommon.myCstr(gv1.Columns(ii).Name) & "'"
                    Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If (dt3 IsNot Nothing AndAlso dt3.Rows.Count > 0) Then
                        strCustomerDesc = clsCommon.myCstr(dt3.Rows(0)("Customer_Name"))
                        strCustVehicleCode = clsCommon.myCstr(dt3.Rows(0)("Number"))
                        strRoute = clsCommon.myCstr(dt3.Rows(0)("Route_No"))
                    End If

                    Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtCustomer.Value) & "'")

                    Dim item8 As New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item8)


                    If clsCommon.myLen(txtDocNo.Value) > 0 Then
                        qry = "select tspl_booking_detail.Document_No,delivery_no,do_posted,Vehicle_Code,Number,Total_Qty,DocumentAmount,tspl_booking_detail.Posted,tspl_booking_detail.Booking_Status,tspl_booking_detail.Performance_Invoice_no from TSPL_BOOKING_MATSER left Outer Join tspl_booking_detail on TSPL_BOOKING_MATSER.Main_Booking_No=tspl_booking_detail.Main_Booking_No and TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No left outer join TSPL_VEHICLE_MASTER ON " & _
                            " tspl_booking_detail.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  where TSPL_BOOKING_MATSER.Main_Booking_No='" & txtDocNo.Value & "' and TSPL_BOOKING_MATSER.Distributor_Code='" & strCustomer & "'  and tspl_booking_detail.FOC_Item=0"
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                            strDeliveryNo = clsCommon.myCstr(dt2.Rows(0)("delivery_no"))
                            strVehicleCode = clsCommon.myCstr(dt2.Rows(0)("Vehicle_Code"))
                            strPerformaInvoiceNo = clsCommon.myCstr(dt2.Rows(0)("Performance_Invoice_no"))
                            DOstatus = clsCommon.myCdbl(dt2.Rows(0)("do_posted"))
                            dblAmount = clsCommon.myCdbl(dt2.Rows(0)("DocumentAmount"))
                            strVehicleNumber = clsCommon.myCstr(dt2.Rows(0)("Number"))
                            strCustVehicleCode = strVehicleNumber
                            strTransport = strVehicleCode
                            strBookingStatus = clsCommon.myCdbl(dt2.Rows(0)("Booking_Status"))
                            strBookingPostStatus = clsCommon.myCdbl(dt2.Rows(0)("Posted"))
                            strBookingNo = clsCommon.myCstr(dt2.Rows(0)("Document_No"))

                            If clsCommon.myLen(strDeliveryNo) = 0 Then
                                'strDeliveryNo = "No"
                            End If

                        End If

                        Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
                        objBookingItem.CustCOde = strCustomer
                        objBookingItem.VehicleCode = strVehicleCode
                        objBookingItem.VehicleDesc = strCustVehicleCode
                        objBookingItem.PriceCode = Price_code
                        objBookingItem.CustName = strCustomerDesc
                        objBookingItem.RouteNo = strRoute

                        objBookingItem.DeliveryNo = strDeliveryNo
                        objBookingItem.DOStatus = DOstatus
                        objBookingItem.Transporter = strTransport
                        objBookingItem.TotalAmt = dblAmount
                        objBookingItem.Booking_Status = strBookingStatus
                        objBookingItem.PerformaInvoiceBookingNo = strPerformaInvoiceNo
                        objBookingItem.BookingNo = strBookingNo

                        If CheckOutstandingOnbooking = 0 Then
                            CustomerOutstandingAmount(strCustomer, dblAmount, Nothing, strDeliveryNo, True, ii)
                        Else
                            CustomerOutstandingAmount(strCustomer, dblAmount, Nothing, txtDocNo.Value, True, ii)
                        End If
                        objBookingItem.dblOutstandingAmt = dblCustOutstandingAmt
                        gv1.Columns(ii).Tag = objBookingItem

                    Else
                        Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
                        objBookingItem.CustCOde = strCustomer
                        objBookingItem.PriceCode = Price_code
                        objBookingItem.CustName = strCustomerDesc
                        objBookingItem.VehicleDesc = strCustVehicleCode
                        objBookingItem.VehicleCode = strVehicleCode
                        objBookingItem.RouteNo = strRoute
                        If CheckOutstandingOnbooking = 0 Then
                            CustomerOutstandingAmount(strCustomer, dblAmount, Nothing, strDeliveryNo, True, ii)
                        Else
                            CustomerOutstandingAmount(strCustomer, dblAmount, Nothing, txtDocNo.Value, True, ii)
                        End If
                        objBookingItem.dblOutstandingAmt = dblCustOutstandingAmt
                        gv1.Columns(ii).Tag = objBookingItem
                    End If
                    Dim strDOstatus As String = ""
                    Dim strBOstatus As String = ""
                    Dim strBOPosted As String = ""
                    If DOstatus = 1 Then
                        strDOstatus = "Open"
                    ElseIf DOstatus = 2 Then
                        strDOstatus = "Pending"
                    ElseIf DOstatus = 3 Then
                        strDOstatus = "Approved"
                    ElseIf DOstatus = 4 Then
                        strDOstatus = "Posted"
                    End If

                    If dblAmount > 0 Then
                        If (strBookingStatus = 1 OrElse strBookingStatus = 0) Then
                            strBOstatus = "Open"
                            'ElseIf strBookingStatus = 2 Then
                            '    btnPost.Enabled = True
                            '    strBOstatus = "Park"
                            'ElseIf strBookingStatus = 3 Then
                            '    btnPost.Enabled = True
                            '    strBOstatus = "Approved"
                        ElseIf strBookingStatus = 4 Then
                            strBOstatus = "Posted"
                        ElseIf strBookingStatus = 5 Then
                            strBOstatus = "Rejected"
                        End If
                    End If

                    gv1.Columns(ii).HeaderText = strCustomerDesc + Environment.NewLine + "Vehicle - " + strCustVehicleCode + Environment.NewLine + "Performa Inv No - " + strPerformaInvoiceNo + Environment.NewLine + "DO No - " + strDeliveryNo + Environment.NewLine + "DO Status - " + clsCommon.myCstr(strDOstatus) + Environment.NewLine + "Amount - " + clsCommon.myCstr(dblAmount) + Environment.NewLine + "Booking No - " + clsCommon.myCstr(strBookingNo)

                End If
            Next



            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            gv1.TableElement.TableHeaderHeight = 130
            'gv1.AutoSize = True
            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            'gv1.EnableSorting = True
            gv1.EnableFiltering = True

            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            strItemcode = Nothing
            strtotal = Nothing
            strshortDesp = Nothing
            strtotalShort = Nothing
            FreshItem = String.Empty
            dt1 = Nothing
            qry = String.Empty
            dt = Nothing
            blnPageLoad = False
        End Try


    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        'detect up arrow key
        Try
            If keyData = Keys.Up AndAlso gv1.CurrentRow.IsSelected Then
                gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index - 1)
            ElseIf keyData = Keys.Down Then
                gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False

        End Try
    End Function

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub AddNew()
        Try
            intChangeColumn = 0
            blnSaveTotalQTy = False
            btnCreateDO.Enabled = False
            isNewEntry = True
            btnSave.Text = "Save"
            txtDocNo.Value = ""
            btnSave.Enabled = True
            btnDelete.Enabled = True
            btnPost.Enabled = True
            txtCustGrp.Value = ""
            lblCustGrp.Text = ""
            txtDate.Value = clsCommon.GETSERVERDATE()
            txtLocation.Value = Nothing
            lblLocation.Text = ""
            lblCustGrp.Text = ""
            txtCustGrp.Value = Nothing
            txtCustomer.Value = Nothing
            lblCustomerName.Text = ""
            'gv1.DataSource = Nothing
            'LoadBlankGrid()
            chkCreateDO.Checked = False
            If txtCustGrp.Enabled = False Then
                txtCustGrp.Enabled = True
            End If

            If txtLocation.Enabled = False Then
                txtLocation.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If e.Column.Index >= 6 Then
                    Dim ii As Integer = e.Column.Index
                    Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                    Dim frm As New frmDeliveryNoteDairySale
                    frm.SetUserMgmt(clsUserMgtCode.frmDeliveryOrderDairy)
                    frm.Show()
                    frm.LoadData(objBookingCust.DeliveryNo, NavigatorType.Current)
                    'Dim strVehicle = clsDBFuncationality.getSingleValue("select Vehicle_Code from TSPL_BOOKING_DETAIL where Document_No ='" & txtDocNo.Value & "' and cust_code='" & objBookingCust.CustCOde & "'")
                    'If Not clsCommon.CompairString(objBookingCust.VehicleCode, strVehicle) = CompairStringResult.Equal Then
                    '    LoadBlankGrid()
                    'End If
                End If
            End If

        Catch ex As Exception

        End Try


    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.Index > 5 Then
                        If clsCommon.myLen(txtLocation.Value) = 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please select Location first", Me.Text)
                            txtLocation.Focus()
                            Exit Sub
                        End If
                        If isLoadData = False Then
                            intChangeColumn = gv1.CurrentColumn.Index
                            CheckBookingQty(False)
                            CalculateItemRate(gv1.CurrentRow.Index, gv1.CurrentColumn.Index)
                        End If
                    End If


                End If
                isCellValueChangedOpen = False
                isInsideLoadData = False
            End If
        Catch ex As Exception
            isInsideLoadData = False
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            isCellValueChangedOpen = False
        End Try
    End Sub
    '' Anubhooti 16-Mar-2015 (Show Alies Name In Finder Query)
    Sub OpenCustomerFinder(ByVal isButtonClick As Boolean)
        Dim qry As String = String.Empty
        Dim whrCls As String = "TSPL_CUSTOMER_MASTER.Status='N'"
        Try
            qry = " select Cust_Code as Code,Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Add1 ,Add2,Add3,City_Code as [City],Closing_Date as [Closing Date],Cust_Category_Code as [Customer Category Code],Cust_Group_Code as [Customer Group Code],Cust_Type_Code as [Customer Type Code],Route_No as [Route No],Route_Desc as [Route Description],Price_Code as [Price Code],CSA_Type as [CSA Type],City_Code as [City Code],State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person Fax],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Cust_Account as [Customer Account],Tax_Group as [Tax Group],TAX1,TAX1_Rate as [Tax1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Payment_Code as [Payment Code],Service_Tax_No as [Service Tax No],Tin_No as [Tin No],Lst_No as [LST No],Form_Type as [Form Type],Channel_Code as [Channel Code],Channel_Desc as [Channel Description],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status],OnHold as [On Hold],Remarks1,Remarks2,Additional1,Additional2,Additional3,Salesman_Code as [Salesman Code],Salesman_Desc as [Salesman Description],Visi_Id as [Visi ID],Visi_Desc as [Visi Description],OutLet_Commossion as [Outlet Commission], Balance_ToDate as [Balance To Date],Credit_Limit as [Credit Limit],Created_By as [Created By],Created_Date as [Created Date],Modify_By as[Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Route_Group as [Route Group],CST,ECC,Range,Collectorate,PAN,Division,Parent_Customer_No as [Parent Customer No],Customer_Class as [Customer Class],Credit_Customer as [Credit Customer],LastInvoice_No as [Last Invoice No],LastInvoice_Date as [Last Invoice Date],price_CodeNon as [Price Code Non],Inter_Branch as [Inter Branch],TRANSACTION_TYPE as [Transaction Type],Credit_Limit_Alert_Type as [Credit Limit alert Type],PIN_Code as [Pin Code],Cust_DOB as [Customer DOB],Cust_Spouse_DOB as [Customer Spouse DOB],Anniversary_Date as [Anniversary Date],Gender,Occation,Agg_Made_Date as [Agg Made Date],Agg_Close_Date as [Agg Close Date],CURRENCY_CODE as [Currency Code],Parent_Customer_YN as [Is Parent Customer],Service_Dealer_Code as [Service Dealer Code],TDM_Code as [TDM Code],Distributor_Code as [Distributor Code],IsDistributor as [Is Distributor],Price_Group_Code as [Price Group Code] from tspl_customer_master"

            gv1.CurrentRow.Cells(1).Value = clsCommon.ShowSelectForm("FSBookCustFnd", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value), "Code", isButtonClick)

            '' Anubhooti 10-Sep-2014
            If clsCommon.myLen(gv1.CurrentRow.Cells(1).Value) > 0 Then
                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value) & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    gv1.CurrentRow.Cells("Customer Name").Value = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                    gv1.CurrentRow.Cells("Vehicle Id").Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
                    gv1.CurrentRow.Cells("Vehicle No").Value = clsDBFuncationality.getSingleValue("select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" & clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle Id").Value) & "' ")
                    gv1.CurrentRow.Cells("Zone_Code").Value = clsCommon.myCstr(dt.Rows(0)("Zone_Code"))
                    Dim strRoute = clsCommon.myCstr(dt.Rows(0)("Route_No"))
                    If clsCommon.myLen(strRoute) =
                        0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please Map Route for customer " & gv1.CurrentRow.Cells("Customer Name").Value, Me.Text)
                    End If
                End If
            Else
                gv1.CurrentRow.Cells("Customer Name").Value = ""
                gv1.CurrentRow.Cells("Vehicle Id").Value = ""
                gv1.CurrentRow.Cells("Vehicle No").Value = ""
            End If
            ''
            If clsCommon.myLen(gv1.CurrentRow.Cells(1).Value) <= 0 Then
                Return
            End If
            'gv1.CurrentRow.Cells(1).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_desc from tspl_item_master where item_code='" + gv1.CurrentRow.Cells(0).Value + "'"))
        Catch ex As Exception
        Finally
            whrCls = Nothing
            qry = Nothing
        End Try

    End Sub
    Sub OpenVehicleFinder(ByVal isButtonClick As Boolean)
        Dim qry As String = String.Empty
        Dim whrCls As String = String.Empty
        Try
            qry = "Select distinct  vehicle_id as Code ,Description from TSPL_VEHICLE_MASTER"

            gv1.CurrentRow.Cells("Vehicle Id").Value = clsCommon.ShowSelectForm("BookVehicleFnd", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle Id").Value), "Code", isButtonClick)
            gv1.CurrentRow.Cells("Vehicle No").Value = clsDBFuncationality.getSingleValue("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle Id").Value) + "'")
        Catch ex As Exception
        Finally
            qry = Nothing
            whrCls = Nothing
        End Try

    End Sub
    Sub OpenLocationFinder(ByVal isButtonClick As Boolean)
        Dim qry As String = String.Empty
        Dim whrCls As String = String.Empty
        Try
            qry = " select Location_Code as [Code],Location_Desc as [Description],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER   "
            ''updated by richa agarwal Against Ticket No BM00000004363
            '' Dim whrCls As String = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' "
            whrCls = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            ''=======================
            gv1.CurrentRow.Cells(3).Value = clsCommon.ShowSelectForm("FSBookLocFnd", qry, "code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(3).Value), "code", isButtonClick)

            '' Anubhooti 10-Sep-2014
            If clsCommon.myLen(gv1.CurrentRow.Cells(3).Value) > 0 Then
                gv1.CurrentRow.Cells("Location Name").Value = clsDBFuncationality.getSingleValue("Select Location_Desc From TSPL_Location_MASTER Where Location_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(3).Value) & "'")
            Else
                gv1.CurrentRow.Cells("Location Name").Value = ""
            End If
            ''
            If clsCommon.myLen(gv1.CurrentRow.Cells(3).Value) <= 0 Then
                Return
            End If
            'gv1.CurrentRow.Cells(1).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_desc from tspl_item_master where item_code='" + gv1.CurrentRow.Cells(0).Value + "'"))
        Catch ex As Exception
        Finally
            qry = Nothing
            whrCls = Nothing
        End Try

    End Sub
    Sub CheckBookingQty(ByVal isButtonClick As Boolean)
        Dim strItemCode As String = String.Empty
        Dim strUnit As String = String.Empty
        Dim strCrate As String = String.Empty
        Try
            If blnSaveTotalQTy = False Then
                Dim dblBookingQty As Double = 0
                Dim dblTotal As Double = 0
                Dim Qty As Double = 0
                Dim dblTotalQty As Double = 0

                'Dim dblVehiclecapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select crateCapacity  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle Id").Value) + "'"))
                For ii As Integer = 7 To gv1.Columns.Count - 1
                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(ii).Value) > 0 Then
                        Qty = gv1.CurrentRow.Cells(ii).Value
                        dblTotalQty = dblTotalQty + Qty
                    End If
                Next
                gv1.CurrentRow.Cells(colTotalQty).Value = dblTotalQty
                'For Each grow As GridViewRowInfo In gv1.Rows
                '    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Vehicle Id").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle Id").Value)) = CompairStringResult.Equal Then
                '        For ii As Integer = 15 To gv1.Columns.Count - 1
                '            If clsCommon.myCdbl(grow.Cells(ii).Value) > 0 Then
                '                strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Code  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 Where Short_Description + '  ' +  TSPL_ITEM_UOM_DETAIL.UOM_Code='" & gv1.Columns(ii).Name.ToString() & "'"))
                '                strUnit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from  TSPL_ITEM_UOM_DETAIL  where Default_UOM=1 and Item_Code='" & strItemCode & "'"))
                '                strCrate = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Crate_Type  from  TSPL_UNIT_MASTER where Unit_Code='" & strUnit & "'"))
                '                If clsCommon.CompairString(strCrate, "Y") = CompairStringResult.Equal Then
                '                    dblTotal += clsCommon.myCdbl(grow.Cells(ii).Value)
                '                End If

                '            End If
                '        Next
                '    End If

                'Next

                'If dblTotal > dblVehiclecapacity Then
                '    If clsCommon.MyMessageBoxShow("Crate qty is exceeded do you want to change vehicle.", "Booking", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '        gv1.CurrentColumn = gv1.Columns("Vehicle Id")
                '        'OpenVehicleFinder(False)
                '    End If

                'End If


            End If
        Catch ex As Exception
        Finally
            strCrate = Nothing
            strUnit = Nothing
            strItemCode = Nothing
        End Try

    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    gv1.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()
        '        gv1.CurrentRow = gv1.Rows(intCurrRow)
        '    End If
        'End If
        If blnSaveTotalQTy = False Then
            If gv1.CurrentColumn.Index > 6 AndAlso intChangeColumn > 0 Then
                Dim intCurrCoumnn As Integer = gv1.CurrentColumn.Index
                intCurrCoumnn = intCurrCoumnn - 1
                Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(intChangeColumn).Tag, clsBookingTemp)
                Dim strCustCode = objBookingCust.CustCOde
                Dim strBookingNO = objBookingCust.BookingNo
                Dim dblAmount As Double = objBookingCust.TotalAmt
                If clsCommon.myLen(txtDocNo.Value) > 0 And objBookingCust.Booking_Status = 2 Then
                    objBookingCust.Booking_Status = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  Booking_Status from TSPL_BOOKING_DETAIL where Main_Booking_No='" & txtDocNo.Value & "'  and Cust_Code='" & strCustCode & "' and Document_No='" + strBookingNO + "'"))
                End If

                'If objBookingCust.Booking_Status <> 3 Then
                '    CustomerOutstandingAmount(strCustCode, dblAmount, Nothing, txtDocNo.Value, True, intChangeColumn)
                'End If

            End If
        End If
    End Sub

    Private Sub gv1_HeaderCellToggleStateChanged(sender As Object, e As GridViewHeaderCellEventArgs) Handles gv1.HeaderCellToggleStateChanged

    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            'If gv1.CurrentColumn Is gv1.Columns(colCustCode) Then
            '    gv1.CurrentColumn = gv1.Columns(colCustName)
            '    OpenCustomerFinder(True)
            '    gv1.CurrentColumn = gv1.Columns(colCustCode)
            'ElseIf gv1.CurrentColumn Is gv1.Columns(colLocCode) Then
            '    gv1.CurrentColumn = gv1.Columns(colLocName)
            '    OpenLocationFinder(True)
            '    gv1.CurrentColumn = gv1.Columns(colLocCode)

            'End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled = True Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled = True Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            btnAddNew.PerformClick()
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        AddNew()
        gv1.DataSource = Nothing
        'LoadBlankGrid()
    End Sub
    Function AllowToSave() As Boolean

        Return True

    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()

            If txtCustGrp.Enabled = False Then
                txtCustGrp.Enabled = True
            End If

            If txtLocation.Enabled = False Then
                txtLocation.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CalculateItemRate(ByVal intRow As Integer, ByVal intColumn As Integer)
        Dim strCustCode As String = Nothing
        Dim strCustName As String = Nothing
        Dim strVehicleCode As String = ""
        Dim strVehicleDesc As String = ""
        Dim strRoute As String = ""
        Dim qry As String = Nothing
        Dim dblQty As Double = 0
        Dim dblTotalQty As Double = 0
        Dim dblTotalAmount As Double = 0
        Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(intColumn).Tag, clsBookingTemp)
        strCustCode = objBookingCust.CustCOde
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCdbl(gv1.Rows(ii).Cells(intColumn).Value) > 0 Then
                dblQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(intColumn).Value)
                qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number from TSPL_CUSTOMER_MASTER left outer join " & _
                  "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " & _
                  "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & txtCustomer.Value & "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    strVehicleCode = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                    strVehicleDesc = clsCommon.myCstr(dt1.Rows(0)("Number"))
                    strRoute = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                    strCustName = clsCommon.myCstr(dt1.Rows(0)("Customer_Name"))
                End If

                If clsCommon.myLen(strCustCode) > 0 Then
                    If clsCommon.myLen(strVehicleCode) = 0 Then
                        Throw New Exception("Please enter Vehicle or Map Route for customer " & txtCustomer.Value & " at Row No" + clsCommon.myCstr(intRow + 1))
                        blnSaveTotalQTy = False
                        Exit Sub
                    End If
                    If clsCommon.myLen(strRoute) = 0 Then
                        Throw New Exception("Please Map Route for customer " & txtCustomer.Value)
                        blnSaveTotalQTy = False
                        Exit Sub
                    End If
                End If


                Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
                Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & txtCustomer.Value & "'")
                objBookingItem.PriceCode = Price_code
                objBookingItem.ItemCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                objBookingItem.UnitCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                objBookingItem.CustCOde = strCustCode
                objBookingItem.VehicleCode = strVehicleCode
                Dim strCrate = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Crate_Type  from  TSPL_UNIT_MASTER where Unit_Code='" & objBookingItem.UnitCode & "'"))

                Dim strItemGroup = clsDBFuncationality.getSingleValue("select  CSA_TYPE from TSPL_ITEM_MASTER where item_code='" & objBookingItem.ItemCode & "'")
                Dim strTaxType = clsDBFuncationality.getSingleValue("select  iif(IsTaxable=1,'Yes','No') as Taxable from TSPL_ITEM_MASTER where item_code='" & objBookingItem.ItemCode & "'")
                Dim dt As New DataTable()
                Dim dblRate As Double = 0
                Dim dblTotal As Double = 0

                qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price from ( " & _
    "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
    "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
    "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
    "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
    "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " & _
    "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & objBookingItem.UnitCode & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objBookingItem.ItemCode & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " & _
    ") XXXE WHERE RowNo=1  "
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count > 0 Then
                    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                Else
                    Throw New Exception("Please create Price chart for customer " & strCustName & " for Location " & txtLocation.Value & "  for item " & gv1.Rows(intRow).Cells(colICode).Value & ".")
                    gv1.CurrentCell.Focus()
                    blnSaveTotalQTy = False
                    Exit Sub
                End If
                'End If
                objBookingItem.OrgRate = dblRate
                Dim obj_Cash As clsSchemeApplyOnDairy = Nothing
                obj_Cash = clsSchemeApplyOnDairy.GetDiscountSchemeData(objBookingItem.ItemCode, objBookingItem.UnitCode, dblQty, strCustCode)
                If obj_Cash IsNot Nothing Then
                    objBookingItem.Disc_Scheme_Amount = obj_Cash.Cash_Amt
                    objBookingItem.Disc_Scheme_Pers = obj_Cash.Cash_Pers
                    objBookingItem.Disc_Scheme_Code = obj_Cash.Schm_Code
                    If clsCommon.myCdbl(objBookingItem.Disc_Scheme_Pers) <> 0 Then
                        objBookingItem.Disc_Scheme_Type = "P"
                        objBookingItem.Disc_Scheme_Amount = System.Math.Round((dblRate * objBookingItem.Disc_Scheme_Pers) / 100, 2)
                    ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) <> 0 Then
                        objBookingItem.Disc_Scheme_Type = "A"
                    End If
                    dblRate = dblRate - objBookingItem.Disc_Scheme_Amount
                End If

                objBookingItem.ItemRate = dblRate
                gv1.Rows(intRow).Cells(intColumn).Tag = objBookingItem
                Dim dblAmount As Double = dblRate * dblQty
                dblTotalAmount += dblAmount
                Dim objBooking As clsBookingTemp = TryCast(gv1.Columns(intColumn).Tag, clsBookingTemp)
                Dim strDeliveryNo As String = objBooking.DeliveryNo
                Dim DOStatus As Integer = objBooking.DOStatus
                Dim strBookingStatus As Integer = objBooking.Booking_Status
                Dim strBOstatus As String = ""
                Dim strDOStatus As String = ""
                Dim dblOutstanding As Double = objBooking.dblOutstandingAmt
                Dim strPerformaInvoiceNo As String = objBooking.PerformaInvoiceBookingNo
                Dim strBookingNo As String = objBooking.BookingNo
                Dim strSecondaryCustName As String = objBooking.CustName
                If DOStatus = 1 Then
                    strDOStatus = "Open"
                ElseIf DOStatus = 2 Then
                    strDOStatus = "Pending"
                ElseIf DOStatus = 3 Then
                    strDOStatus = "Approved"
                ElseIf DOStatus = 4 Then
                    strDOStatus = "Posted"
                Else
                    strDOStatus = ""
                End If
                If dblAmount > 0 Then
                    If (strBookingStatus = 1 OrElse strBookingStatus = 0) Then
                        strBOstatus = "Open"
                        'ElseIf strBookingStatus = 2 Then
                        '    btnPost.Enabled = True
                        '    strBOstatus = "Park"
                        'ElseIf strBookingStatus = 3 Then
                        '    btnPost.Enabled = True
                        '    strBOstatus = "Approved"
                    ElseIf strBookingStatus = 4 Then
                        strBOstatus = "Posted"
                    ElseIf strBookingStatus = 5 Then
                        strBOstatus = "Rejected"
                    End If
                End If
                Dim dblVehiclecapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select crateCapacity  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + strVehicleCode + "'"))
                If clsCommon.CompairString(strCrate, "Y") = CompairStringResult.Equal Then
                    dblTotalQty += dblQty
                End If

                If dblTotalQty > dblVehiclecapacity Then
                    Dim strItem = clsCommon.myCstr(gv1.Rows(intRow).Cells(colIDesc).Value)
                    gv1.Rows(intRow).Cells(intColumn).Value = 0
                    Throw New Exception("Crate Capacity for Vehicle " + strVehicleDesc + " is " + clsCommon.myCstr(dblVehiclecapacity) + " and entered Quantity is " + clsCommon.myCstr(dblTotalQty) + Environment.NewLine +
                                        "Crate qty is exceeded For Customer " + strCustName)
                End If

                gv1.Columns(intColumn).HeaderText = strSecondaryCustName + Environment.NewLine + "Vehicle - " + strVehicleDesc + Environment.NewLine + "Performa Inv No - " + strPerformaInvoiceNo + Environment.NewLine + "DO No - " + strDeliveryNo + Environment.NewLine + "DO Status - " + clsCommon.myCstr(strDOStatus) + Environment.NewLine + "Amount - " + clsCommon.myCstr(dblTotalAmount) + Environment.NewLine + "Booking No - " + clsCommon.myCstr(strBookingNo)
                'gv1.Columns(intColumn).HeaderText = strCustName + Environment.NewLine + strVehicleDesc + Environment.NewLine + strDeliveryNo + Environment.NewLine + clsCommon.myCstr(strDOStatus) + Environment.NewLine + clsCommon.myCstr(dblTotalAmount)
                objBooking.TotalAmt = dblTotalAmount
            End If
        Next
    End Sub
    Sub SaveData()
        Dim strMainBookingCode As String = ""
        If clsCommon.myLen(txtLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location first", Me.Text)
            txtLocation.Focus()
            Exit Sub
        End If
        GC.Collect()
        blnSaveTotalQTy = True
        Dim qry As String = String.Empty
        Dim obj As New clsBookingEntryDistributor()
        '' start here

        For ii As Integer = 7 To gv1.Columns.Count - 1
            Dim strCustCode As String = Nothing
            Dim strCustName As String = Nothing
            Dim strVehicleCode As String = ""
            Dim strRoute As String = ""
            Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
            strCustCode = objBookingCust.CustCOde
            Dim dblBookingAmount As Double = objBookingCust.TotalAmt
            If dblBookingAmount > 0 Then
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCdbl(grow.Cells(ii).Value) > 0 Then
                        Dim dblQty As Double = clsCommon.myCdbl(grow.Cells(ii).Value)
                        If clsCommon.myLen(objBookingCust.PerformaInvoiceBookingNo) = 0 Then
                            qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & txtCustomer.Value & "'"
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                                strVehicleCode = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                                strRoute = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                                strCustName = clsCommon.myCstr(dt1.Rows(0)("Customer_Name"))
                            End If

                            If clsCommon.myLen(strCustCode) > 0 Then
                                If clsCommon.myLen(strVehicleCode) = 0 Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Vehicle or Map Route for customer " & txtCustomer.Value & " at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                                    blnSaveTotalQTy = False
                                    Exit Sub
                                End If
                                If clsCommon.myLen(strRoute) = 0 Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Please Map Route for customer " & txtCustomer.Value, Me.Text)
                                    blnSaveTotalQTy = False
                                    Exit Sub
                                End If
                            End If
                        Else
                            strVehicleCode = clsDBFuncationality.getSingleValue("select Vehicle_Code from TSPL_BOOKING_DETAIL where Main_Booking_No ='" & txtDocNo.Value & "' and cust_code='" & txtCustomer.Value & "'")
                        End If



                        Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
                        Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & txtCustomer.Value & "'")
                        objBookingItem.PriceCode = Price_code
                        objBookingItem.ItemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objBookingItem.UnitCode = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objBookingItem.CustCOde = strCustCode
                        objBookingItem.VehicleCode = strVehicleCode

                        Dim dt As New DataTable()
                        Dim dblRate As Double = 0
                        Dim dblSellingRate As Double = 0
                        Dim dblTotal As Double = 0

                        qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price from ( " & _
           "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
           "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
           "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
           "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
           "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'   and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null) and  " & _
           "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & objBookingItem.UnitCode & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objBookingItem.ItemCode & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " & _
           ") XXXE WHERE RowNo=1  "
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt.Rows.Count > 0 Then
                            dblSellingRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                            dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                        Else
                            clsCommon.MyMessageBoxShow(Me, "Please create Price chart for customer " & txtCustomer.Value & " for Location " & txtLocation.Value & "  for item " & grow.Cells(colICode).Value & ".", Me.Text)
                            gv1.CurrentCell.Focus()
                            blnSaveTotalQTy = False
                            Exit Sub
                        End If
                        'End If
                        objBookingItem.OrgRate = dblRate
                        objBookingItem.SellingPrice = dblSellingRate
                        Dim obj_Cash As clsSchemeApplyOnDairy = Nothing
                        obj_Cash = clsSchemeApplyOnDairy.GetDiscountSchemeData(objBookingItem.ItemCode, objBookingItem.UnitCode, dblQty, strCustCode)
                        If obj_Cash IsNot Nothing Then
                            objBookingItem.Disc_Scheme_Amount = obj_Cash.Cash_Amt
                            objBookingItem.Disc_Scheme_Pers = obj_Cash.Cash_Pers
                            objBookingItem.Disc_Scheme_Code = obj_Cash.Schm_Code
                            If clsCommon.myCdbl(objBookingItem.Disc_Scheme_Pers) <> 0 Then
                                objBookingItem.Disc_Scheme_Type = "P"
                                objBookingItem.Disc_Scheme_Amount = System.Math.Round((dblRate * objBookingItem.Disc_Scheme_Pers) / 100, 2)
                            ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) <> 0 Then
                                objBookingItem.Disc_Scheme_Type = "A"
                            End If
                            dblRate = dblRate - objBookingItem.Disc_Scheme_Amount
                            dblSellingRate = dblSellingRate - objBookingItem.Disc_Scheme_Amount
                        End If

                        objBookingItem.ItemRate = dblRate
                        objBookingItem.SellingPrice = dblSellingRate
                        grow.Cells(ii).Tag = objBookingItem

                    End If


                Next

            End If
        Next

        '' ends here
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim blnRatezero As Boolean = False
            obj.Arr = New List(Of clsBookingEntryDistributorDetail)
            Dim objTr As New clsBookingEntryDistributorDetail()
            Dim objBookingCust As clsBookingTemp
            Dim BlankData As Integer = 0
            Dim MainBookingNo As String = Nothing
            DOmsg = ""

            If (AllowToSave()) Then
                If btnSave.Text = "Save" Then
                    MainBookingNo = clsDBFuncationality.getSingleValue("select isnull(max(Main_Booking_No),'') as 'Main_Booking_No' from TSPL_BOOKING_MATSER", trans)
                    If MainBookingNo = "" Then
                        MainBookingNo = "MBK000000001"
                    Else
                        MainBookingNo = clsCommon.incval(MainBookingNo)
                    End If
                Else
                    MainBookingNo = txtDocNo.Value
                End If
                strMainBookingCode = MainBookingNo
                Dim intarr As Integer = 0
                Dim intLine As Integer = 0
                For ii As Integer = 7 To gv1.Columns.Count - 1
                    Dim dblTotal As Double = 0
                    objBookingCust = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)

                    obj.Main_Booking_No = MainBookingNo


                    obj.Document_No = objBookingCust.BookingNo
                    obj.Document_Date = txtDate.Value
                    obj.location_code = txtLocation.Value
                    obj.Cust_Group_Code = txtCustGrp.Value
                    obj.Distributor_Code = objBookingCust.CustCOde
                    Dim dblBookAmount As Double = objBookingCust.TotalAmt
                    If dblBookAmount > 0 Then
                        For Each grow As GridViewRowInfo In gv1.Rows
                            objTr = New clsBookingEntryDistributorDetail
                            objTr.Booking_Qty = clsCommon.myCdbl(grow.Cells(ii).Value)

                            If objTr.Booking_Qty > 0 Then

                                BlankData = BlankData + 1
                                intLine += 1
                                objTr.Line_No = grow.Cells(colLineNo).Value

                                objTr.Cust_Code = txtCustomer.Value
                                objTr.Sampling = 0
                                objTr.Loc_Code = txtLocation.Value
                                objTr.Total_Qty = clsCommon.myCdbl(grow.Cells(colTotalQty).Value)

                                Dim objBookingitem As clsBookingTemp = TryCast(grow.Cells(ii).Tag, clsBookingTemp)
                                objTr.Vehicle_Code = objBookingitem.VehicleCode
                                objTr.Item_Code = objBookingitem.ItemCode

                                objTr.Short_Description = ""
                                objTr.Unit_code = objBookingitem.UnitCode
                                Dim dblRate As Double = 0

                                dblRate = objBookingitem.ItemRate
                                objTr.Item_Rate = dblRate
                                objTr.Disc_Scheme_Amount = objBookingitem.Disc_Scheme_Amount
                                objTr.Disc_Scheme_Code = objBookingitem.Disc_Scheme_Code
                                objTr.Disc_Scheme_Pers = objBookingitem.Disc_Scheme_Pers
                                objTr.Disc_Scheme_Type = objBookingitem.Disc_Scheme_Type
                                objTr.OrgRate = objBookingitem.OrgRate
                                objTr.SellingPrice = objBookingitem.SellingPrice


                                objTr.Main_Booking_No = MainBookingNo

                                If objBookingCust.Booking_Status = 0 Then
                                    objTr.Booking_Status = 1
                                    objBookingCust.Booking_Status = 1
                                End If
                                Dim strBookingStatus As Integer = 0
                                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                                    strBookingStatus = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  Booking_Status from TSPL_BOOKING_DETAIL where Document_No='" & txtDocNo.Value & "'  and Cust_Code='" & objTr.Cust_Code & "'", trans))
                                End If

                                If strBookingStatus = 3 Then
                                    objTr.Booking_Status = 3
                                    objBookingCust.Booking_Status = 3
                                ElseIf strBookingStatus = 5 Then
                                    objTr.Booking_Status = 5
                                    objBookingCust.Booking_Status = 5
                                Else
                                    objTr.Booking_Status = objBookingCust.Booking_Status
                                End If

                                objTr.DocumentAmount = clsCommon.myCdbl(dblRate * clsCommon.myCdbl(grow.Cells(ii).Value))
                                dblTotal += objTr.DocumentAmount
                                objTr.DocumentAmount = dblTotal

                                gv1.Rows(0).Cells(ii).Tag = dblTotal
                                Dim DOCdateCurrent As Date? = Nothing
                                DOCdateCurrent = clsCommon.GETSERVERDATE(trans)
                                ' Query to get scheme type of Item
                                Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
                                qryScheme += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
                                qryScheme += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
                                qryScheme += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
                                qryScheme += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + objBookingitem.ItemCode + "' and Cust_Code='" + objTr.Cust_Code + "'))a where a.sno=1)"
                                qryScheme += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
                                qryScheme += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + objBookingitem.ItemCode + "' "


                                qryScheme += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
                                Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                    Dim objD As clsSchemeApplyOnDairy = Nothing
                                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(objBookingitem.ItemCode), clsCommon.myCstr(objTr.Unit_code), clsCommon.myCstr(objTr.Booking_Qty), objTr.Cust_Code, clsCommon.myCstr(SchemeType), Nothing, Nothing, trans)

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
                                If (clsCommon.myLen(objTr.Cust_Code) > 0) Then
                                    obj.Arr.Add(objTr)
                                End If
                            End If
                            If clsCommon.myLen(obj.Document_No) = 0 Then
                                isNewEntry = True
                            Else
                                isNewEntry = False
                            End If
                        Next
                        'Save Code

                        If (obj.SaveData(obj, isNewEntry, trans)) = True Then

                            Dim intSampling As Integer = 0

                            Dim dblQty As Double = 0
                            Dim dblRate As Double = 0
                            Dim dblAmount As Double = 0
                            Dim dblTotal1 As Double = 0
                            Dim BookingNoQry As String = "select isnull(Document_No,'') as 'Booking_No' from TSPL_BOOKING_MATSER where Main_Booking_No='" + obj.Main_Booking_No + "' and Distributor_Code='" + objBookingCust.CustCOde + "'"
                            Dim BookingNoValue As String = clsDBFuncationality.getSingleValue(BookingNoQry, trans)

                            Dim strPerformaInvoiceNo = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(txtDate.Value), clsDocType.frmPerformaInvoiceBooking, "", txtLocation.Value)
                            qry = "Update TSPL_BOOKING_DETAIL set DocumentAmount=" & gv1.Rows(0).Cells(ii).Tag & ",Performance_Invoice_no='" & strPerformaInvoiceNo & "' where   Main_Booking_No='" & obj.Main_Booking_No & "' and Cust_Code='" & txtCustomer.Value & "' and Document_No='" + BookingNoValue + "' and    Loc_Code='" & clsCommon.myCstr(txtLocation.Value) & "' and isnull(Scheme_Item,'N')='N' and isnull(FOC_Item,0)=0 "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            obj = Nothing
                            objTr = Nothing
                            obj = New clsBookingEntryDistributor()
                            objTr = New clsBookingEntryDistributorDetail()
                            obj.Arr = New List(Of clsBookingEntryDistributorDetail)

                        End If

                        intarr += 1
                    End If
                Next
                If blnRatezero = True Then
                    trans.Rollback()
                    clsCommon.MyMessageBoxShow(Me, DOmsg, Me.Text)
                    Return
                End If
                If (BlankData <= 0) Then
                    trans.Rollback()
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Booking", Me.Text)
                    Return
                End If

            End If
            trans.Commit()
            LoadData(strMainBookingCode, NavigatorType.Current)
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            blnSaveTotalQTy = False
        Catch ex As Exception
            trans.Rollback()
            blnSaveTotalQTy = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
    End Sub
    Public Shared Sub CreateSMSContent(ByVal strSMSContent As String, ByVal trans As SqlTransaction)
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.SaveData(clsUserMgtCode.frmbookingdairy, objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub
    Public Shared Sub CreateEmailContent(ByVal strEmailContent As String, ByVal trans As SqlTransaction)
        'MailCode Start
        If clsCommon.myLen(strEmailContent) > 0 Then
            Dim qry As String = "SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'"
            Dim EmailSubject As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" & clsUserMgtCode.frmbookingdairy & "'", trans))
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Text = strEmailContent
            objSMSH.Email_Subject = EmailSubject
            objSMSH.arrEMail = New List(Of String)()
            objSMSH.SaveData(clsUserMgtCode.frmbookingdairy, objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub
    Private Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal dblTotal As Double, ByVal Trans As SqlTransaction, ByVal strDoc As String, ByVal blnBeforSave As Boolean, Optional ByVal intColumn As Integer = 0) As Boolean
        Dim qry As String = String.Empty
        Try
            Dim dblOutstandingAmt As Double = 0
            Dim dblCreditLimit As Double = 0
            Dim dblSecurityAmount As Double = 0
            Dim dblPendingDeliveryAmt As Double = 0
            Dim dblAmt As Double = 0

            If CheckOutstandingOnbooking = 0 Then
                qry = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
           "select SUM(isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & _
            "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1 and Sampling=0  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "'  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No not in ('" & strDoc & "')  " & _
           " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
          "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
          " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
          " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
           " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
          " union all " & _
          "select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
          " union all " & _
          "select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

            Else
                qry = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
           "select SUM(isnull (isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) * isnull(TSPL_BOOKING_DETAIL.Item_Rate,0) ,0) ) as OutStandingAmt , 1 as RI from TSPL_BOOKING_DETAIL " & _
           "where  TSPL_BOOKING_DETAIL.Cust_Code='" & strCustomer & "'  and TSPL_BOOKING_DETAIL.Document_No not in ('" & strDoc & "')  " & _
           " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
          "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
          " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
          " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
           " union all " & _
          "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
          "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
          " union all " & _
          "select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
          " union all " & _
          "select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

            End If

            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'", Trans))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDepositType  in ('S')  and Posted='Y' and Cust_Code='" & strCustomer & "'", Trans))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where  posted=0 and Sampling=0 and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No <> '" & strDoc & "' and Customer_Code='" & strCustomer & "'", Trans))

            dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt
            dblCustOutstandingAmt = dblAmt

            If blnSaveTotalQTy = True Then
                If dblAmt < dblTotal Then
                    Dim dblNewCredtitLimit = dblAmt - dblTotal
                    'common.clsCommon.MyMessageBoxShow("Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit))
                    Return False
                End If
            Else
                If dblAmt < dblTotal AndAlso dblTotal > 0 Then
                    If blnPageLoad = False Then
                        Dim strCustName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER  where Cust_Code='" & strCustomer & "'", Trans))
                        clsCommon.MyMessageBoxShow(Me, "Please increase your credit limit for customer " + strCustName)
                    End If
                End If
            End If

            'Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        Finally
            qry = Nothing
        End Try
        Return True
    End Function

    Private Function CreateDO(ByVal ChekPostBtn As Boolean, ByVal trans As SqlTransaction, ByVal strBookingNo As String)
        Try
            blnSaveTotalQTy = True
            Dim qry As String = String.Empty
            DOmsg = String.Empty
            Dim dblTotal_Qty As Double = 0
            Dim blnRatezero As Boolean = False
            If (AllowToSave()) Then


                For ii As Integer = 7 To gv1.Columns.Count - 1
                    Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                    If objBookingCust.TotalAmt > 0 Then
                        If (clsCommon.myLen(objBookingCust.DeliveryNo) > 0 AndAlso objBookingCust.DOStatus <> 4) OrElse (clsCommon.myLen(objBookingCust.DeliveryNo) = 0 And objBookingCust.TotalAmt > 0) Then
                            Dim dblCustTotalQty As Double = 0
                            For Each grow As GridViewRowInfo In gv1.Rows
                                If (clsCommon.myCdbl(grow.Cells(ii).Value)) > 0 Then
                                    dblCustTotalQty += grow.Cells(ii).Value
                                End If
                            Next
                            If clsCommon.myCdbl(dblCustTotalQty) > 0 Then
                                strBookingNo = objBookingCust.BookingNo
                                If chkCreateDO.Checked = False OrElse (chkCreateDO.Checked = True AndAlso clsCommon.myLen(objBookingCust.DeliveryNo) = 0) Then
                                    Dim obj As New clsDeliveryNoteDairySale
                                    dblTotal_Qty = 0
                                    'obj.Price_code = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & clsCommon.myCstr(grow.Cells(1).Value) & "'", trans)
                                    obj.Credit_Limit = 0
                                    obj.Document_Date = txtDate.Value
                                    obj.Customer_Code = txtCustomer.Value
                                    obj.Document_No = objBookingCust.DeliveryNo
                                    obj.Status = objBookingCust.DOStatus
                                    obj.Location_Code = txtLocation.Value
                                    dblTotal_Qty = dblCustTotalQty
                                    obj.Sampling = 0
                                    obj.Booking_No = strBookingNo
                                    obj.Booking_Date = txtDate.Value
                                    obj.Vehicle_Capacity = 0
                                    obj.Lorry_No = objBookingCust.VehicleCode
                                    obj.Route_No = objBookingCust.RouteNo
                                    obj.Transporter_Name = obj.Lorry_No
                                    obj.Price_code = objBookingCust.PriceCode
                                    obj.Freight = ""
                                    obj.Freight_Amount = 0
                                    obj.Comments = ""
                                    obj.OnHold = "N"
                                    obj.Short_Close = "N"
                                    obj.Total_Amt = 0
                                    obj.Arr = New List(Of clsDeliveryNoteDairySaleDetail)
                                    Dim intLineNo As Integer = 1
                                    Dim dblTotal As Double = 0
                                    blnRatezero = False
                                    DOCreated = False
                                    For Each grow As GridViewRowInfo In gv1.Rows
                                        Dim objTr As New clsDeliveryNoteDairySaleDetail()
                                        If (clsCommon.myCdbl(grow.Cells(ii).Value)) > 0 Then

                                            objTr.Line_No = intLineNo
                                            objTr.Document_No = obj.Document_No
                                            objTr.Sampling = 0
                                            Dim objBookingitem As clsBookingTemp = TryCast(grow.Cells(ii).Tag, clsBookingTemp)
                                            objTr.Item_Code = grow.Cells(colICode).Value
                                            objTr.Unit_code = grow.Cells(colUnit).Value
                                            objTr.Booking_No = strBookingNo
                                            objTr.Qty = clsCommon.myCdbl(grow.Cells(ii).Value)
                                            objTr.BookQty = clsCommon.myCdbl(grow.Cells(ii).Value)
                                            objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(ii).Value)

                                            Dim objBookingItemRate As clsBookingTemp = TryCast(grow.Cells(ii).Tag, clsBookingTemp)
                                            objTr.Rate = objBookingItemRate.ItemRate
                                            objTr.MRP = objBookingItemRate.MRP
                                            objTr.Price_Date = objBookingItemRate.Price_Date
                                            objTr.Disc_Scheme_Amount = objBookingItemRate.Disc_Scheme_Amount
                                            objTr.Disc_Scheme_Code = objBookingItemRate.Disc_Scheme_Code
                                            objTr.Disc_Scheme_Pers = objBookingItemRate.Disc_Scheme_Pers
                                            objTr.Disc_Scheme_Type = objBookingItemRate.Disc_Scheme_Type
                                            objTr.OrgRate = objBookingItemRate.OrgRate
                                            objTr.SellingPrice = objBookingItemRate.SellingPrice
                                            objTr.Amount = clsCommon.myCdbl(objTr.Rate * clsCommon.myCdbl(grow.Cells(ii).Value))
                                            dblTotal += objTr.Amount

                                            'End If
                                            If objTr.Rate = 0 Then
                                                blnRatezero = True
                                                DOmsg += "Please create Price chart for customer " & objBookingCust.CustCOde & " for Location " & txtLocation.Value & "  for item " & objTr.Item_Code & "." + Environment.NewLine
                                            End If
                                            objTr.Price_Code = obj.Price_code
                                            objTr.OrgUnit_code = clsCommon.myCstr(grow.Cells(ii).Value)

                                            If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                                                obj.Arr.Add(objTr)
                                            End If
                                            objTr.OrgRate = objTr.OrgRate
                                            intLineNo += 1
                                        End If

                                        obj.Total_Amt = dblTotal

                                        qry = "Update TSPL_BOOKING_DETAIL set DO_Qty=" & objTr.Qty & ",Booking_Qty=" & objTr.Qty & ",Total_Qty=" & dblTotal_Qty & " where item_code='" & objTr.Item_Code & "' and Scheme_Item<>'Y' and vehicle_code='" & obj.Lorry_No & "' and  Document_No='" & txtDocNo.Value & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "' and Sampling =" & obj.Sampling & ""
                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                        If clsCommon.myLen(obj.Document_No) = 0 Then
                                            isNewEntry = True
                                        Else
                                            isNewEntry = False
                                        End If

                                    Next

                                    If (obj.Arr IsNot Nothing OrElse obj.Arr.Count > 0) Then
                                        If blnRatezero = False Then
                                            If CheckOutstandingOnbooking = 0 Then
                                                If AllowWo_Outstanding = False Then
                                                    If CustomerOutstandingAmount(txtCustomer.Value, dblTotal, trans, "", False) = False Then
                                                        obj.CreditApproval_Reqd = "Y"
                                                        obj.Status = 2
                                                    End If
                                                End If
                                            End If
                                            If (obj.SaveData(obj, isNewEntry, trans)) Then
                                                qry = "Update TSPL_BOOKING_DETAIL set DO_Posted=" & obj.Status & ", Delivery_No='" & obj.Document_No & "',DocumentAmount=" & obj.Total_Amt & " where Document_No='" & obj.Booking_No & "' and    Loc_Code='" & obj.Location_Code & "' and vehicle_code='" & obj.Lorry_No & "' and Sampling=" & obj.Sampling & ""
                                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                                qry = "Update TSPL_BOOKING_MATSER set CreateDO_Automatic=1 where Document_No='" & obj.Booking_No & "' "
                                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                                DOCreated = True
                                                If (clsDeliveryNoteDairySale.PostData("DEL-NOTE-FS", obj.Document_No, trans, 1)) Then
                                                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")

                                                End If
                                            End If
                                        End If
                                    End If

                                    'DO STATUS 1 -open 2 - pending 3 - approved 4 - posted
                                ElseIf chkCreateDO.Checked = False OrElse (chkCreateDO.Checked = True AndAlso objBookingCust.DOStatus = 3) Then
                                    If (clsDeliveryNoteDairySale.PostData("DEL-NOTE-FS", objBookingCust.DeliveryNo, trans, 1)) Then
                                        'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                                        DOCreated = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            blnSaveTotalQTy = False
            'clsCommon.MyMessageBoxShow(msg)
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            blnSaveTotalQTy = False
            Return False
        End Try
    End Function
    Private Sub LoadDataForPivot()
        Dim dt As DataTable
        Dim finalQuery As String = Nothing
        '=============================================pivot variables================================================
        Dim strPivotForInternal As String = Nothing
        Dim strPivotForOuter As String = Nothing
        Dim StrPivotForLastDay As String = Nothing
        Dim StrPivotForLastSumDay As String = Nothing
        Dim strPivotForInternalquery As String = Nothing
        Dim strPivotForOuterquery As String = Nothing
        Dim StrPivotForLastDayquery As String = Nothing
        Dim StrPivotForLastDaySumquery As String = Nothing
        Dim StrDateDiffQuery As String = Nothing
        Dim StrDateDiff As String = Nothing
        Dim StrToatlQuery As String = Nothing
        Dim StrToatl As String = Nothing
        Dim StrToatlSumQuery As String = Nothing
        Dim StrToatlsum As String = Nothing

        Dim dtExtraColumn As DataTable = Nothing
        dtExtraColumn = clsDBFuncationality.GetDataTable("select distinct  tspl_item_master.short_description from TSPL_BOOKING_DETAIL left join tspl_item_master on tspl_item_master.item_code = TSPL_BOOKING_DETAIL.item_code where Document_No ='" & txtDocNo.Value & "'  ")


        strPivotForInternalquery = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+ tspl_item_master.short_description+']' from TSPL_BOOKING_DETAIL"
        strPivotForInternalquery += " left join tspl_item_master on tspl_item_master.item_code = TSPL_BOOKING_DETAIL.item_code where Document_No ='" & txtDocNo.Value & "'  for xml path ('')) as strr)as a "
        strPivotForInternal = clsDBFuncationality.getSingleValue(strPivotForInternalquery, Nothing)

        strPivotForOuterquery = " (select(select distinct + ',sum(isnull(['+ tspl_item_master.short_description+'],0)) as '+'['+ tspl_item_master.short_description+']' from TSPL_BOOKING_DETAIL"
        strPivotForOuterquery += " left join tspl_item_master on tspl_item_master.item_code = TSPL_BOOKING_DETAIL.item_code where Document_No ='" & txtDocNo.Value & "'  for xml path ('')) as strr)"
        strPivotForOuter = clsDBFuncationality.getSingleValue(strPivotForOuterquery, Nothing)

        StrToatlQuery = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+sum(isnull(['+ tspl_item_master.short_description+'],0))' from TSPL_BOOKING_DETAIL"
        StrToatlQuery += " left join tspl_item_master on tspl_item_master.item_code = TSPL_BOOKING_DETAIL.item_code where Document_No ='" & txtDocNo.Value & "'  for xml path ('')) as strr)as a"
        StrToatl = clsDBFuncationality.getSingleValue(StrToatlQuery, Nothing)


        '================================================================end here=============================================================================
        Dim qry As String = " select tspl_vehicle_master.Number as Vechicle_Name,TSPL_ROUTE_MASTER.Route_Desc, tspl_item_master.short_description,TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Document_Date ,TSPL_BOOKING_MATSER.Location_code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Route_No ,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_BOOKING_DETAIL.DocumentAmount ,TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_BOOKING_DETAIL.Booking_Qty,isnull(TSPL_BOOKING_DETAIL.Scheme_item,'N') as 'Scheme_item' from TSPL_BOOKING_DETAIL "
        qry += " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No"
        qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code"
        qry += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code "
        qry += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code "
        qry += " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No"
        qry += " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code"
        qry += " where TSPL_BOOKING_MATSER.Document_No='" & txtDocNo.Value & "' and Booking_Status not in ('2','5')"

        finalQuery = " select max(Document_No ) as Document_No,max(Document_Date ) as Document_Date ,max(Location_code ) as Location_code ,max(Location_Desc) as Location_Desc,(Cust_Code ) as Cust_Code,Route_No,max(Route_Desc) as Route_Desc ,Vehicle_Code,max(Vechicle_Name) as Vechicle_Name ,max(Customer_Name ) as Customer_Name,MAX(DocumentAmount ) as DocumentAmount " + strPivotForOuter + " " + StrToatl + " as total_Qty from ("
        finalQuery += "select * from ( "
        finalQuery += " select max(Vechicle_Name) as Vechicle_Name, max(Route_Desc) as Route_Desc, max(xx.short_description) as short_description,xx.Document_No ,max(xx.Document_Date ) as Document_Date,Location_code ,max(Location_Desc ) as Location_Desc,Cust_Code ,max(Customer_Name) as Customer_Name,Route_No,Vehicle_Code,max(DocumentAmount ) as DocumentAmount,Item_Code ,max(Item_Desc ) as Item_Desc,sum(Booking_Qty ) as Booking_Qty,Scheme_item from ("
        finalQuery += "" + qry + ""
        finalQuery += ") as xx group by xx.Document_No ,xx.Location_code ,xx.Cust_Code ,xx.Route_No ,xx.Vehicle_Code,xx.Item_Code,xx.Scheme_item "
        finalQuery += " ) as pp pivot (sum(pp.booking_qty) for short_description in (" + strPivotForInternal + "))t "
        finalQuery += " ) as aa group by Scheme_item,Cust_Code , Route_No ,Vehicle_Code"

        dt = clsDBFuncationality.GetDataTable(finalQuery)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        gv2.DataSource = dt
        gv2.GroupDescriptors.Clear()
        gv2.MasterTemplate.SummaryRowsBottom.Clear()
        FormatGrid(dtExtraColumn)


        'RadPageView1.SelectedPage = RadPageViewPage2
        'ReStoreGridLayout()
    End Sub
    Sub FormatGrid(ByVal dtExtraColumn As DataTable)
        ' Dim strItemCode, head2 As String

        gv2.TableElement.TableHeaderHeight = 25
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            gv2.Columns(ii).Width = 80

        Next

        gv2.Columns("Document_No").IsVisible = False
        gv2.Columns("Document_No").Width = 40
        gv2.Columns("Document_No").HeaderText = "Document_No"

        gv2.Columns("Document_Date").IsVisible = False
        gv2.Columns("Document_Date").Width = 40
        gv2.Columns("Document_Date").HeaderText = "Document_Date"

        gv2.Columns("Location_code").IsVisible = False
        gv2.Columns("Location_code").Width = 40
        gv2.Columns("Location_code").HeaderText = "Location_code"

        gv2.Columns("Location_Desc").IsVisible = False
        gv2.Columns("Location_Desc").Width = 40
        gv2.Columns("Location_Desc").HeaderText = "Location_Desc"

        gv2.Columns("Cust_Code").IsVisible = False
        gv2.Columns("Cust_Code").Width = 40
        gv2.Columns("Cust_Code").HeaderText = "Cust_Code"

        gv2.Columns("Route_No").IsVisible = False
        gv2.Columns("Route_No").Width = 40
        gv2.Columns("Route_No").HeaderText = "Route No"

        gv2.Columns("Route_Desc").IsVisible = True
        gv2.Columns("Route_Desc").Width = 100
        gv2.Columns("Route_Desc").HeaderText = "Route Name"


        gv2.Columns("Vehicle_Code").IsVisible = True
        gv2.Columns("Vehicle_Code").Width = 100
        gv2.Columns("Vehicle_Code").HeaderText = "Vehicle Code"

        gv2.Columns("Vechicle_Name").IsVisible = True
        gv2.Columns("Vechicle_Name").Width = 100
        gv2.Columns("Vechicle_Name").HeaderText = "Vehicle Name"

        gv2.Columns("Customer_Name").IsVisible = True
        gv2.Columns("Customer_Name").Width = 100
        gv2.Columns("Customer_Name").HeaderText = "Customer Name"

        gv2.Columns("DocumentAmount").IsVisible = True
        gv2.Columns("DocumentAmount").Width = 100
        gv2.Columns("DocumentAmount").HeaderText = "DO Amount"

        gv2.Columns("total_Qty").IsVisible = True
        gv2.Columns("total_Qty").Width = 100
        gv2.Columns("total_Qty").HeaderText = "Total Qty"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim intCount As Integer = 0
        If dtExtraColumn IsNot Nothing AndAlso dtExtraColumn.Rows.Count > 0 Then
            For Each dr As DataRow In dtExtraColumn.Rows
                Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
        End If

        Dim summaryItem1 As New GridViewSummaryItem()

        Dim item2 As New GridViewSummaryItem("DocumentAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        gv2.GroupDescriptors.Add(New GridGroupByExpression("Vehicle_Code as Item format ""{0}: {1}"" Group By Vehicle_Code"))


        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True

        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv2.MasterTemplate.ShowTotals = True
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom




    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim IsbtnCopyOrder_Click As Boolean = False
        Try
            AddNew()
            ' KUNAL > TICKET : BM00000009943 > DATE : 5-OCT-2016
            If strCode.Length > 0 AndAlso strCode IsNot Nothing AndAlso strCode.Contains("btnCopyOrder_Click") Then
                strCode = strCode.Replace("btnCopyOrder_Click", "")
                IsbtnCopyOrder_Click = True
            End If

            Dim obj As New clsBookingEntryDistributor
            obj = clsBookingEntryDistributor.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Main_Booking_No) > 0) Then

                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                isInsideLoadData = True
                isLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                txtDocNo.Value = obj.Main_Booking_No

                txtDate.Value = obj.Document_Date
                txtLocation.Value = obj.location_code
                txtCustGrp.Value = obj.Cust_Group_Code
                txtCustomer.Value = obj.Cust_Code
                lblCustGrp.Text = clsDBFuncationality.getSingleValue("SELECT Cust_Group_Desc as [Description] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + txtCustGrp.Value + "'")
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Customer_Name as Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'"))
                chkCreateDO.Checked = IIf(obj.CreateDO_Automatic, True, False)
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnCreateDO.Enabled = True
                    Dim DOStatus = clsDBFuncationality.getSingleValue("select top 1  Main_Booking_No from TSPL_BOOKING_DETAIL where DO_Posted <> 4 and Main_Booking_No='" & txtDocNo.Value & "'")
                    If clsCommon.myLen(DOStatus) = 0 Then
                        btnCreateDO.Enabled = False
                    End If
                End If
                '''''''''' FOr Grid''''''''
                LoadBlankGrid()

                ' KUNAL > TICKET : BM00000009943 > DATE : 5-OCT-2016
                If strCode.Length > 0 AndAlso StrDocNo IsNot Nothing AndAlso IsbtnCopyOrder_Click = True Then
                    txtDocNo.Value = ""
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            isLoadData = False
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BOOKING_MATSER where Main_Booking_No='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            ' Dim qry As String = "select distinct TSPL_BOOKING_MATSER.Document_No as DocumentNo,convert(varchar(12),TSPL_BOOKING_MATSER.Document_date,103) as Document_date,case when Posted=1 then 'posted' else 'Unposted' end as Posted from TSPL_BOOKING_MATSER"
            Dim qry As String = "select distinct TSPL_BOOKING_MATSER.Main_Booking_No as DocumentNo,convert(varchar(12),TSPL_BOOKING_MATSER.Document_date,103) as Document_date,(select isnull((Select distinct '['+TSPL_LOCATION_MASTER.Location_Desc +']  ' from TSPL_booking_detail left outer join TSPL_LOCATION_MASTER on TSPL_BOOKING_DETAIL.Loc_Code=TSPL_LOCATION_MASTER.Location_Code where TSPL_booking_detail.Main_Booking_No=TSPL_BOOKING_MATSER.Main_Booking_No   for xml path('')),'')  ) as Location  ,case when Posted=1 then 'posted' else 'Unposted' end as Posted from TSPL_BOOKING_MATSER"
            Dim whrClas As String = " TSPL_BOOKING_MATSER.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBookDocNo", qry, "DocumentNo", whrClas, txtDocNo.Value, "DocumentNo", isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseData()
    End Sub
    Sub CloseData()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            DeleteData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsBookingEntryDistributor.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            LoadData(txtDocNo.Value, NavigatorType.Current)
            Export()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Export()
        If gv1.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel()
        Try
            Dim strCreatedBy As String = clsDBFuncationality.getSingleValue("Select Created_By from TSPL_BOOKING_MATSER where Main_Booking_No='" + txtDocNo.Value + "'")
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Booking No : " + txtDocNo.Value
            arrHeader.Add(strtemp)
            strtemp = "Booking Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            strtemp = "Created By : " + strCreatedBy
            arrHeader.Add(strtemp)

            clsCommon.MyExportToExcelGrid("Booking Entry", gv1, arrHeader, Me.Text)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Post()
    End Sub
    Sub Post()
        Try
            If (myMessages.postConfirm()) Then
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Dim qry = "Update TSPL_BOOKING_MATSER set Posted=1, " & _
                     "Modified_By='" + objCommonVar.CurrentUserCode + "', " & _
                     "Modified_Date='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' " & _
                     "where Main_Booking_No='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    For ii As Integer = 7 To gv1.Columns.Count - 1
                        Dim objBooking As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                        Dim dblTotalAmt As Double = objBooking.TotalAmt
                        Dim strBookingStatus As Double = objBooking.Booking_Status
                        If dblTotalAmt > 0 Then
                            qry = "Update TSPL_BOOKING_DETAIL set Booking_Status=4 where Cust_Code='" & objBooking.CustCOde & "' and Main_Booking_No='" + txtDocNo.Value + "' "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    Next

                    trans.Commit()
                    Dim msg = "Successfully Posted"
                    common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    btnCreateDO.Enabled = True
                Else
                    Throw New Exception("No Data found to Post")
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        chkCreateDO.Checked = False
        'blnSaveTotalQTy = True
        isNewEntry = True
        btnSave.Text = "Save"
        txtDocNo.Value = ""

        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        btnCreateDO.Enabled = False
        blnSaveTotalQTy = False
        For ii As Integer = 6 To gv1.Columns.Count - 1
            isLoadData = True
            Dim strDOstatus As String = Nothing
            Dim strDeliveryNo As String = Nothing
            Dim dblAmount As Double = 0
            If ii >= 7 Then
                Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                Dim strCustCode = objBookingCust.CustCOde
                Dim strCustomerDesc = objBookingCust.CustName
                Dim strCustVehicleCode = objBookingCust.VehicleDesc
                Dim dblCustOutstandingAmt As Double = objBookingCust.dblOutstandingAmt
                If CheckOutstandingOnbooking = 0 Then
                    CustomerOutstandingAmount(strCustomerDesc, dblAmount, Nothing, strDeliveryNo, True, ii)
                Else
                    CustomerOutstandingAmount(strCustomerDesc, dblAmount, Nothing, txtDocNo.Value, True, ii)
                End If
                gv1.Columns(ii).HeaderText = strCustomerDesc + Environment.NewLine + "Vehicle - " + strCustVehicleCode + Environment.NewLine + "Performa Inv No - " + "" + Environment.NewLine + "DO No - " + "" + Environment.NewLine + "DO Status - " + "" + Environment.NewLine + "Amount - " + clsCommon.myCstr(0) + Environment.NewLine + "Booking No - " + clsCommon.myCstr("")
                'gv1.Columns(ii).HeaderText = strCustomerDesc + Environment.NewLine + strCustVehicleCode + Environment.NewLine + strDeliveryNo + Environment.NewLine + clsCommon.myCstr(strDOstatus) + Environment.NewLine + clsCommon.myCstr(dblAmount)
                For jj As Integer = 0 To gv1.Rows.Count - 1

                    If clsCommon.myCdbl(gv1.Rows(jj).Cells(ii).Value) > 0 Then
                        gv1.Rows(jj).Cells(ii).Value = 0
                    End If
                Next

            Else
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCdbl(grow.Cells(colTotalQty).Value) > 0 Then
                        grow.Cells(colTotalQty).Value = 0
                    End If
                Next
            End If

            isLoadData = False
        Next

        If txtCustGrp.Enabled = False Then
            txtCustGrp.Enabled = True
        End If

        If txtLocation.Enabled = False Then
            txtLocation.Enabled = True
        End If
    End Sub



    Private Sub btnLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub btnSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub btnCreateDO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateDO.Click
        '' start here
        Dim qry As String = String.Empty
        For ii As Integer = 7 To gv1.Columns.Count - 1
            Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
            Dim strCustCode = txtCustomer.Value
            Dim Price_code = objBookingCust.PriceCode
            If objBookingCust.TotalAmt > 0 Then
                'Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCdbl(grow.Cells(colTotalQty).Value) > 0 Then
                        Dim dblQty As Double = clsCommon.myCdbl(grow.Cells(ii).Value)
                        qry = "Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price from ( " & _
                                  "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
                                  "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
                                  "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
                                  "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
                                  "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code    where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null) and  " & _
                                  "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & grow.Cells(colUnit).Value & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & grow.Cells(colICode).Value & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " & _
                                  ") XXXE WHERE RowNo=1  "
                        Dim dt As New DataTable()
                        Dim dblRate As Double = 0
                        Dim dblSellingRate As Double = 0
                        Dim dblTotal As Double = 0
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt.Rows.Count > 0 Then
                            dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                            dblSellingRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                            Dim objBookingItemRate As clsBookingTemp = New clsBookingTemp()
                            objBookingItemRate.ItemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
                            objBookingItemRate.UnitCode = clsCommon.myCstr(grow.Cells(colUnit).Value)
                            objBookingItemRate.ItemRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                            objBookingItemRate.SellingPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                            objBookingItemRate.MRP = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Net"))
                            objBookingItemRate.Price_Date = clsCommon.myCstr(dt.Rows(0).Item("Start_Date"))
                            '' disc start here
                            Dim obj_Cash As clsSchemeApplyOnDairy = Nothing
                            obj_Cash = clsSchemeApplyOnDairy.GetDiscountSchemeData(objBookingItemRate.ItemCode, objBookingItemRate.UnitCode, dblQty, strCustCode)
                            If obj_Cash IsNot Nothing Then
                                objBookingItemRate.Disc_Scheme_Amount = obj_Cash.Cash_Amt
                                objBookingItemRate.Disc_Scheme_Pers = obj_Cash.Cash_Pers
                                objBookingItemRate.Disc_Scheme_Code = obj_Cash.Schm_Code
                                If clsCommon.myCdbl(objBookingItemRate.Disc_Scheme_Pers) <> 0 Then
                                    objBookingItemRate.Disc_Scheme_Type = "P"
                                    objBookingItemRate.Disc_Scheme_Amount = System.Math.Round((dblRate * objBookingItemRate.Disc_Scheme_Pers) / 100, 2)
                                ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) <> 0 Then
                                    objBookingItemRate.Disc_Scheme_Type = "A"
                                End If
                                dblRate = dblRate - objBookingItemRate.Disc_Scheme_Amount
                                dblSellingRate = dblSellingRate - objBookingItemRate.Disc_Scheme_Amount
                            End If
                            '' disc ends here
                            objBookingItemRate.ItemRate = dblRate
                            objBookingItemRate.SellingPrice = dblSellingRate

                            grow.Cells(ii).Tag = objBookingItemRate
                        End If
                    End If
                Next
            End If
        Next

        '' ends here

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            If CreateDO(False, trans, txtDocNo.Value) Then

                trans.Commit()
                If clsCommon.myLen(DOmsg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, DOmsg, Me.Text)
                End If
                If DOCreated = True Then
                    Dim msg = "Successfully created"
                    common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    msg = Nothing
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub gv1_MouseDown(sender As Object, e As MouseEventArgs) Handles gv1.MouseDown
        'clsCommon.MyMessageBoxShow("MouseDown")
    End Sub

    Private Sub gv1_MouseEnter(sender As Object, e As EventArgs) Handles gv1.MouseEnter


        ' clsCommon.MyMessageBoxShow("MouseEnter")
    End Sub

    Private Sub gv1_MouseHover(sender As Object, e As EventArgs) Handles gv1.MouseHover

        'clsCommon.MyMessageBoxShow("MouseHover")
    End Sub

    Private Sub gv1_MouseMove(sender As Object, e As MouseEventArgs) Handles gv1.MouseMove
        'clsCommon.MyMessageBoxShow("MouseMove")
    End Sub

    Private Sub gv1_MouseUp(sender As Object, e As MouseEventArgs) Handles gv1.MouseUp
        'clsCommon.MyMessageBoxShow("MouseUO")
    End Sub

    Private Sub gv1_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        'If Not IsDBNull(e.RowElement.RowInfo.Cells("DO_Posted").Value) Then
        '    If clsCommon.CompairString(e.RowElement.RowInfo.Cells("DO_Posted").Value, "Posted") = CompairStringResult.Equal Then
        '        'e.RowElement.Enabled = False
        '        'e.RowElement.DrawFill = True
        '        'e.RowElement.GradientStyle = GradientStyles.Solid
        '        'e.RowElement.ForeColor = Color.Black
        '        'e.RowElement.BackColor = Color.LightGreen
        '        'ElseIf clsCommon.CompairString(e.RowElement.RowInfo.Cells("DO_Posted").Value, "Pending") = CompairStringResult.Equal Then
        '        '    e.RowElement.Enabled = False
        '        '    e.RowElement.DrawFill = True
        '        '    e.RowElement.GradientStyle = GradientStyles.Solid
        '        '    e.RowElement.ForeColor = Color.Black
        '        '    e.RowElement.BackColor = Color.MistyRose
        '        'Else
        '        '    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
        '        '    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
        '        '    e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
        '        '    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
        '    End If
        'End If
    End Sub


    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim qry As String = "select count(*) from TSPL_BOOKING_DETAIL"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim ISFresh As Integer = 0
        Dim FreshItem As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim strItemcode As String = String.Empty

        Dim strtotal As String = String.Empty
        Dim strshortDesp As String = String.Empty
        Dim strtotalShort As String = String.Empty

        ISFresh = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(Item_Code) AS Item_Code  from tspl_item_master where is_freshitem=1"))
        If ISFresh = 0 Then
            clsCommon.MyMessageBoxShow(Me, "There is No Items For Fresh Sale..", Me.Text)
            Exit Sub
        End If
        FreshItem = clsCommon.myCstr("select Isnull(Item_Code,'') As Item_Code from tspl_item_master where is_freshitem=1 ")
        dt = clsDBFuncationality.GetDataTable(FreshItem)

        For Each dr1 As DataRow In dt.Rows
            strItemcode = clsCommon.myCstr(dr1("Item_Code"))
            If clsCommon.myLen(strItemcode) > 0 Then
                strtotal = strtotal + ", '' as " + "[" + strItemcode + "]"
            End If
        Next

        If check <= 0 Then
            qry = "select  '' as [Customer Code],'' as [Location Code],0 as [Sampling] " & strtotal & " "
        Else
            qry = "select  '' as [Customer Code],'' as [Location Code],0 as [Sampling] " & strtotal & " "
        End If
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gv As New RadGridView()
        Dim qry As String = "select count(*) from TSPL_BOOKING_DETAIL"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim ISFresh As Integer = 0
        Dim FreshItem As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim strItemcode As String = String.Empty

        Dim strtotal As String = String.Empty
        Dim strshortDesp As String = String.Empty
        Dim strtotalShort As String = String.Empty
        Dim intCount As Integer = 0
        ISFresh = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(Item_Code) AS Item_Code  from tspl_item_master where is_freshitem=1"))
        If ISFresh = 0 Then
            clsCommon.MyMessageBoxShow(Me, "There is No Items For Fresh Sale..", Me.Text)
            Exit Sub
        End If
        FreshItem = clsCommon.myCstr("select Isnull(Item_Code,'') As Item_Code from tspl_item_master where is_freshitem=1 ")
        dt = clsDBFuncationality.GetDataTable(FreshItem)

        For Each dr1 As DataRow In dt.Rows
            strItemcode = clsCommon.myCstr(dr1("Item_Code"))
            If clsCommon.myLen(strItemcode) > 0 Then
                strtotal = strtotal + strItemcode + ","
            End If
            intCount += 1
        Next
        'strtotal = clsDBFuncationality.getSingleValue("select (select isnull((Select distinct ''+TSPL_ITEM_MASTER.Item_Code +',' from TSPL_ITEM_MASTER where Is_FreshItem=1  for xml path('')),'')  ) as ItemCode ")

        strtotal = "," + strtotal.Substring(0, strtotal.Length - 1)
        'strtotal = strtotal.Substring(1, strtotal.Length - 2)

        'Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)

        If transportSql.importExcelPivot(gv, intCount, strtotal, "Customer Code", "Location Code", "Sampling") Then
            Dim linno As Integer = 1
            Dim obj As New clsBookingEntryDistributor()
            If gv.Rows.Count >= 1 Then
                For ii As Integer = 3 To gv.Columns.Count - 1

                    Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
                    objBookingItem.ItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Code  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 Where TSPL_ITEM_MASTER.Item_Code='" & gv.Columns(ii).Name.ToString() & "'"))
                    objBookingItem.UnitCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from  TSPL_ITEM_UOM_DETAIL  where Default_UOM=1 and Item_Code='" & objBookingItem.ItemCode & "'"))
                    objBookingItem.ShortDesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description  from tspl_item_master where item_code='" & objBookingItem.ItemCode & "'"))
                    gv.Columns(ii).Tag = objBookingItem


                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & clsCommon.myCstr(grow.Cells("Customer Code").Value) & "'")
                        qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price from ( " & _
                "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
                "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
                "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
                "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
                "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null) and  " & _
                "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & objBookingItem.UnitCode & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objBookingItem.ItemCode & "' AND Location_Code='" & clsCommon.myCstr(grow.Cells("Location Code").Value) & "'  " & _
                ") XXXE WHERE RowNo=1  "
                        'Dim dt As New DataTable()
                        Dim dblRate As Double = 0
                        Dim dblTotal As Double = 0
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt.Rows.Count > 0 Then
                            dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                        End If
                        If dblRate > 0 Then
                            grow.Cells(ii).Tag = dblRate
                        End If

                    Next


                Next


                Dim strVehicleCode As String = ""
                Dim strRoute As String = ""
                Dim strCustName As String = ""
                For ii As Integer = 0 To gv.Rows.Count - 1
                    Dim strCustCode = clsCommon.myCstr(gv.Rows(ii).Cells("Customer Code").Value)
                    qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & strCustCode & "'"
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        strVehicleCode = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                        strRoute = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                    End If
                    Dim strLocCode = clsCommon.myCstr(gv.Rows(ii).Cells("Location Code").Value)
                    Dim intSampling As Integer = clsCommon.myCdbl(gv.Rows(ii).Cells("Sampling").Value)

                    If clsCommon.myLen(strCustCode) > 0 Then
                        If clsCommon.myLen(strLocCode) = 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please enter Location at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                            Exit Sub
                        End If
                        If clsCommon.myLen(strVehicleCode) = 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please enter Vehicle or Map Route for customer " & txtCustomer.Value & " at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                            Exit Sub
                        End If
                        If clsCommon.myLen(strRoute) = 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please Map Route for customer " & txtCustomer.Value, Me.Text)
                        End If

                        For jj As Integer = ii + 1 To gv.Rows.Count - 1
                            Dim strInCustCode = clsCommon.myCstr(gv.Rows(jj).Cells("Customer Code").Value)
                            Dim strInLocCode = clsCommon.myCstr(gv.Rows(jj).Cells("Location Code").Value)

                            Dim intInnerSampling As Double = clsCommon.myCdbl(gv.Rows(jj).Cells("Sampling").Value)

                            If clsCommon.CompairString(intSampling, intInnerSampling) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strCustCode, strInCustCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strLocCode, strInLocCode) = CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow(Me, "Customer Code " + strCustCode + " and Location Code " + strLocCode + "  repeat at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1), Me.Text)
                                Exit Sub
                            End If
                        Next
                    End If
                Next

                '' ends here
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim blnRatezero As Boolean = False
                    DOmsg = ""

                    obj.Document_No = txtDocNo.Value
                    obj.Document_Date = txtDate.Value
                    obj.Arr = New List(Of clsBookingEntryDistributorDetail)
                    Dim intarr As Integer = 0
                    For ii As Integer = 3 To gv.Columns.Count - 1
                        Dim Qty As Double = 0
                        Dim dblTotalQty As Double = 0

                        For Each grow As GridViewRowInfo In gv.Rows
                            Dim objTr As New clsBookingEntryDistributorDetail()
                            objTr.Booking_Qty = clsCommon.myCdbl(grow.Cells(ii).Value)
                            Dim LineNo As Integer = 0
                            If objTr.Booking_Qty > 0 Then
                                For intcount1 As Integer = 3 To gv.Columns.Count - 1
                                    If clsCommon.myCdbl(grow.Cells(intcount1).Value) > 0 Then
                                        Qty = grow.Cells(intcount1).Value
                                        dblTotalQty = dblTotalQty + Qty
                                    End If
                                Next
                                LineNo += 1
                                objTr.Line_No = LineNo
                                objTr.Cust_Code = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                                objTr.Sampling = clsCommon.myCdbl(grow.Cells("Sampling").Value)
                                objTr.Loc_Code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                                objTr.Total_Qty = dblTotalQty
                                objTr.Vehicle_Code = clsDBFuncationality.getSingleValue("select vehicle_code from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & objTr.Cust_Code & "'", trans)

                                Dim objBookingitem As clsBookingTemp = TryCast(gv.Columns(ii).Tag, clsBookingTemp)
                                objTr.Item_Code = objBookingitem.ItemCode
                                objTr.Short_Description = objBookingitem.ShortDesc
                                objTr.Unit_code = objBookingitem.UnitCode

                                Dim dblRate As Double = 0
                                Dim dblTotal As Double = 0

                                dblRate = grow.Cells(ii).Tag
                                objTr.Item_Rate = dblRate
                                objTr.DocumentAmount = clsCommon.myCdbl(dblRate * clsCommon.myCdbl(grow.Cells(ii).Value))
                                dblTotal += objTr.DocumentAmount
                                objTr.DocumentAmount = dblTotal
                                'End If
                                If dblRate = 0 Then
                                    blnRatezero = True
                                    DOmsg += "Please create Price chart for customer " & grow.Cells(1).Value & " for Location " & grow.Cells(3).Value & "  for item " & objTr.Item_Code & "." + Environment.NewLine
                                End If


                            End If
                            If (clsCommon.myLen(objTr.Cust_Code) > 0) Then
                                obj.Arr.Add(objTr)
                            End If
                        Next
                        intarr += 1
                    Next
                    If blnRatezero = True Then
                        trans.Rollback()
                        clsCommon.MyMessageBoxShow(Me, DOmsg, Me.Text)
                        Return
                    End If
                    If (obj.Document_No Is Nothing OrElse obj.Arr.Count <= 0) Then
                        trans.Rollback()
                        common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Booking", Me.Text)
                        Return
                    End If

                    If (obj.SaveData(obj, True, trans)) = True Then

                        Dim dblQty As Double = 0
                        Dim dblRate As Double = 0
                        Dim dblAmount As Double = 0
                        Dim dblTotal As Double = 0
                        For Each grow As GridViewRowInfo In gv.Rows
                            dblTotal = 0
                            If clsCommon.myLen(grow.Cells("Customer Code").Value) > 0 AndAlso clsCommon.myLen(grow.Cells("Location Code").Value) > 0 Then

                                For ii As Integer = 3 To gv.Columns.Count - 1
                                    Dim objBookingitem As clsBookingTemp = TryCast(gv.Columns(ii).Tag, clsBookingTemp)

                                    If (clsCommon.myCdbl(grow.Cells(ii).Value)) > 0 Then
                                        'strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Code  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 Where Short_Description + '  ' +  TSPL_ITEM_UOM_DETAIL.UOM_Code='" & gv1.Columns(ii).Name.ToString() & "'", trans))
                                        'strUnit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from  TSPL_ITEM_UOM_DETAIL  where Default_UOM=1 and Item_Code='" & strItemCode & "'", trans))
                                        dblQty = clsCommon.myCdbl(grow.Cells(ii).Value)

                                        dblRate = grow.Cells(ii).Tag
                                        dblAmount = clsCommon.myCdbl(dblRate * clsCommon.myCdbl(grow.Cells(ii).Value))
                                        dblTotal += dblAmount
                                        'End If
                                    End If
                                Next
                                qry = "Update TSPL_BOOKING_DETAIL set DocumentAmount=" & dblTotal & " where   Document_No='" & obj.Document_No & "' and Cust_Code='" & clsCommon.myCstr(grow.Cells("Customer Code").Value) & "' and    Loc_Code='" & clsCommon.myCstr(grow.Cells("Location Code").Value) & "' and Sampling=" & clsCommon.myCdbl(grow.Cells("Sampling").Value) & ""
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                        Next
                        trans.Commit()
                        'LoadData(obj.Document_No, NavigatorType.Current)
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        trans.Rollback()
                    End If

                    blnSaveTotalQTy = False

                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

                Catch ex As Exception
                    clsCommon.MyMessageBoxShow(Me, ex.Message & " At Line No : " & linno, Me.Text)
                    trans.Rollback()

                End Try
            End If
        End If
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        If clsCommon.myLen(txtCustGrp.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Customer Group first", Me.Text)
            'txtCustGrp.Focus()
            Exit Sub
        End If
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
        Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        If Not clsCommon.CompairString(txtLocation.Value, objCommonVar.CurrLocationCode) = CompairStringResult.Equal And clsCommon.myLen(txtCustGrp.Value) > 0 Then
            LoadBlankGrid()
        End If

    End Sub

    Private Sub btnPrintBooking_Click(sender As Object, e As EventArgs) Handles btnPrintBooking.Click
        LoadDataForPivot()
        If gv2.Rows.Count > 0 Then
            ExportToExcelForBooking()
        End If
    End Sub
    Private Sub ExportToExcelForBooking()
        Try
            Dim strCreatedBy As String = clsDBFuncationality.getSingleValue("Select Created_By from TSPL_BOOKING_MATSER where Main_Booking_No='" + txtDocNo.Value + "'")
            Dim strLocationName As String = clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where Location_Code ='" + txtLocation.Value + "'")
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Main Booking No : " + txtDocNo.Value
            arrHeader.Add(strtemp)
            strtemp = "Booking Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            strtemp = "Branch Name : " + strLocationName
            arrHeader.Add(strtemp)

            clsCommon.MyExportToExcelGrid("Booking Entry", gv2, arrHeader, Me.Text)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub txtCustGrp__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustGrp._MYValidating
        Dim qry As String = " SELECT Cust_Group_Code as [CustomerGruopCode],Cust_Group_Desc as [Description]," & _
                          " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] "
        txtCustGrp.Value = clsCommon.ShowSelectForm("CUSGRP_CODE", qry, "CustomerGruopCode", "", txtCustGrp.Value, "", isButtonClicked)
        lblCustGrp.Text = clsDBFuncationality.getSingleValue(" SELECT Cust_Group_Desc as [Description] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + txtCustGrp.Value + "'")

        If clsCommon.myLen(txtCustGrp.Value) > 0 AndAlso clsCommon.myLen(txtLocation.Value) > 0 Then
            'LoadBlankGrid()
        End If
    End Sub
#Region "Mail SMS Setting"
    Private Sub btnSendEmailSMS_Click(sender As Object, e As EventArgs) Handles btnSendEmailSMS.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmbookingdairy
        frm.ShowDialog()
    End Sub
    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        Try

            Dim strContactperson As String = ""
            Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmbookingdairy)

            If obj Is Nothing Then
                clsCommon.MyMessageBoxShow(Me, "First do setting of email", Me.Text)
                Return
            End If

            If clsCommon.myLen(obj.mailsubjct) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "First do setting of email", Me.Text)
                Return
            End If
            For ii As Integer = 7 To gv1.Columns.Count - 1
                Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                If objBookingCust.Booking_Status = 2 Then

                    '------------------------code for attchament-------------------------------------
                    Dim strRptPath As String = ""
                    If obj.atchmnt = "Y" Then
                        Dim objAtch As New frmSaleInvoiceProductSale()
                        objAtch.funPrint(clsCommon.myCstr(txtDocNo.Value), True)
                        strRptPath = objAtch.strrptpath
                    End If
                    '---------------------------------------------------------------------------

                    For Each strUser As String In lstUsers
                        'lstUsers.Add(dr("User_Code").ToString())
                        Dim lstReceiptents As New List(Of String)
                        Dim qry As String = ""
                        Dim emailId As String = ""
                        If isSendForApproval Then
                            strContactperson = strUser
                            qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                            emailId = clsDBFuncationality.getSingleValue(qry)
                        Else
                            strContactperson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
                            emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
                        End If

                        'strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactperson)
                        'lstReceiptents.Add(emailId)

                        'Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

                        'clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)

                    Next
                End If
            Next


            clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

#End Region


    Private Sub btnPerformaInvoice_Click(sender As Object, e As EventArgs) Handles btnPerformaInvoice.Click
        ''=====================Added by preeti Gupta against ticket no [BM00000009856]
        'If clsCommon.myLen(txtDocNo.Value) <= 0 Then
        '    myMessages.blankValue("Invoice not found to Print")
        'Else
        '    funPrint(txtDocNo.Value)
        'End If

        ' PRITI GUPTA ; DATE : 5-OCT-2016
        Dim BookingNo As String = txtDocNo.Value
        If BookingNo IsNot Nothing AndAlso BookingNo.Length > 0 Then
            Dim frm As New FrmMultipleCustomerforDo(BookingNo)
            frm.Show()
            frm.WindowState = System.Windows.Forms.FormWindowState.Maximized

        End If
    End Sub
    Public Sub funPrint(ByVal strDocNo As String)

        Try
            Dim Qry As String = GetQuery()
            Qry += " where TSPL_BOOKING_MATSER.Document_No ='" + strDocNo + "'"

            'Qry = "Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDSBoking", "Performa Invoice", "rptCompanyAddress.rpt")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function GetQuery() As String
        Dim Qry As String = " select TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.logo_Img,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,TSPL_COMPANY_MASTER.Comp_Name AS CompName ," & _
                            " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP," & _
                            " TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER.STATE_NAME   )>0 then ', '+ TSPL_STATE_MASTER.STATE_NAME  else ' ' end    as Location_Address_GP, " & _
                            " TSPL_BOOKING_DETAIL.item_code,tspl_item_master.item_desc,TSPL_BOOKING_DETAIL.booking_Qty,TSPL_BOOKING_DETAIL.unit_Code,TSPL_BOOKING_DETAIL.item_Rate,TSPL_BOOKING_DETAIL.documentAmount,TSPL_BOOKING_DETAIL.vehicle_code,tspl_vehicle_master.Number as Vechicle_Name,TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Document_Date,TSPL_BOOKING_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.customer_name " & _
                            " from TSPL_BOOKING_DETAIL  left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " & _
                            " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_BOOKING_MATSER.comp_code " & _
                            " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
                            " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " & _
                            " left join tspl_location_master on TSPL_LOCATION_MASTER .Location_Code =TSPL_BOOKING_MATSER.location_code " & _
                            " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER .STATE_CODE =tspl_location_master.State " & _
                            " left join tspl_item_master on tspl_item_master.item_code=TSPL_BOOKING_DETAIL.item_code " & _
                            " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code" & _
                            " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .cust_code=TSPL_BOOKING_DETAIL.cust_code "
        Return Qry
    End Function
    ' KUNAL > TICKET : BM00000009943 > DATE : 5-OCT-2016
    Private Sub btnCopyOrder_Click(sender As Object, e As EventArgs) Handles btnCopyOrder.Click
        Try

            Dim qry As String = "select distinct TSPL_BOOKING_MATSER.Document_No as DocumentNo,convert(varchar(12),TSPL_BOOKING_MATSER.Document_date,103) as Document_date,(select isnull((Select distinct '['+TSPL_LOCATION_MASTER.Location_Desc +']  ' from TSPL_booking_detail left outer join TSPL_LOCATION_MASTER on TSPL_BOOKING_DETAIL.Loc_Code=TSPL_LOCATION_MASTER.Location_Code where TSPL_booking_detail.Document_No=TSPL_BOOKING_MATSER.Document_No   for xml path('')),'')  ) as Location  ,case when Posted=1 then 'posted' else 'Unposted' end as Posted from TSPL_BOOKING_MATSER"
            Dim whrClas As String = " TSPL_BOOKING_MATSER.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Reset()
            Dim document As String = clsCommon.ShowSelectForm("frmBkDryCopyBtn", qry, "DocumentNo", whrClas, txtDocNo.Value, "DocumentNo", True)
            LoadData(document + "btnCopyOrder_Click", NavigatorType.Current)

            If txtCustGrp.Enabled = True Then
                txtCustGrp.Enabled = False
            End If

            If txtLocation.Enabled = True Then
                txtLocation.Enabled = False
            End If

            isNewEntry = True
            btnSave.Text = "Save"
            btnSave.Enabled = True
            btnDelete.Enabled = True
            btnPost.Enabled = True
            For ii As Integer = 7 To gv1.Columns.Count - 1
                Dim objBooking As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                Dim strDeliveryNo As String = ""
                Dim DOStatus As Integer = 0
                Dim strBookingStatus As Integer = 0
                Dim strBOstatus As String = ""
                Dim strDOStatus As String = ""
                Dim dblOutstanding As Double = 0
                Dim strPerformaInvoiceNo As String = ""
                Dim strCustCode = objBooking.CustCOde
                Dim strCustomerDesc = objBooking.CustName
                Dim strCustVehicleCode = objBooking.VehicleDesc
                Dim strBookingNo As String = ""
                'Dim dblCustOutstandingAmt As Double = objBooking.dblOutstandingAmt
                Dim dblAmount As Double = objBooking.TotalAmt

                If CheckOutstandingOnbooking = 0 Then
                    CustomerOutstandingAmount(strCustCode, dblAmount, Nothing, "", True, ii)
                Else
                    CustomerOutstandingAmount(strCustCode, dblAmount, Nothing, "", True, ii)
                End If

                If DOStatus = 1 Then
                    strDOStatus = "Open"
                ElseIf DOStatus = 2 Then
                    strDOStatus = "Pending"
                ElseIf DOStatus = 3 Then
                    strDOStatus = "Approved"
                ElseIf DOStatus = 4 Then
                    strDOStatus = "Posted"
                Else
                    strDOStatus = ""
                End If
                If dblAmount > 0 Then
                    If (strBookingStatus = 1 OrElse strBookingStatus = 0) Then
                        strBOstatus = "Open"
                        'ElseIf strBookingStatus = 2 Then
                        '    btnPost.Enabled = True
                        '    strBOstatus = "Park"
                        'ElseIf strBookingStatus = 3 Then
                        '    btnPost.Enabled = True
                        '    strBOstatus = "Approved"
                    ElseIf strBookingStatus = 4 Then
                        strBOstatus = "Posted"
                    ElseIf strBookingStatus = 5 Then
                        strBOstatus = "Rejected"
                    End If
                End If
                Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
                objBookingItem.DeliveryNo = ""
                objBookingItem.DOStatus = 0
                objBookingItem.Booking_Status = 0
                objBookingItem.BookingNo = ""
                objBookingItem.dblOutstandingAmt = dblCustOutstandingAmt
                objBookingItem.PerformaInvoiceBookingNo = ""
                objBookingItem.CustCOde = strCustCode
                objBookingItem.CustName = strCustomerDesc
                objBookingItem.VehicleDesc = strCustVehicleCode
                objBookingItem.TotalAmt = dblAmount
                gv1.Columns(ii).Tag = objBookingItem

                gv1.Columns(ii).HeaderText = strCustomerDesc + Environment.NewLine + "Vehicle - " + strCustVehicleCode + Environment.NewLine + "Performa Inv No - " + strPerformaInvoiceNo + Environment.NewLine + "DO No - " + strDeliveryNo + Environment.NewLine + "DO Status - " + clsCommon.myCstr(strDOStatus) + Environment.NewLine + "Amount - " + clsCommon.myCstr(dblAmount) + Environment.NewLine + "Booking No - " + clsCommon.myCstr(strBookingNo)
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        If clsCommon.myLen(txtCustGrp.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Customer Group first", Me.Text)
            Exit Sub
        End If
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        Dim WhrCls As String = "Cust_Group_Code='" + txtCustGrp.Value + "'"
        txtCustomer.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtCustomer.Value, "Code", isButtonClicked)
        lblCustomerName.Text = clsDBFuncationality.getSingleValue(" select Customer_Name as Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'")
        If clsCommon.myLen(txtCustGrp.Value) > 0 AndAlso clsCommon.myLen(txtLocation.Value) > 0 Then
            LoadBlankGrid()
        End If
    End Sub
End Class

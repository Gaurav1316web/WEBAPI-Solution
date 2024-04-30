
Imports common
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports System.IO
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports XpertERPEngine
Imports System

Public Class frmItemSublocationMapping
    Inherits FrmMainTranScreen
#Region "Variables"

    Dim RecordCount As Integer = 0

    Dim blnPageLoad As Boolean = False

    Private isCellValueChangedOpen As Boolean = False

    Private isInsideLoadData As Boolean = False
    Private isPageLoadData As Boolean = False

    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colIShortName As String = "COLISHORTNAME"
    Const colSubLocationCode As String = "colSubLocationCode"
    Const colSubLocationName As String = "colSubLocationName"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub


    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isPageLoadData = True
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        AddNew()
        isPageLoadData = False
    End Sub

    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim TAX_PAID As New GridViewComboBoxColumn
        Dim BOOK_RATE_UOM As New GridViewTextBoxColumn

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        'repoICode.HeaderImage = My.Resources.search4
        'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.ReadOnly = True
        repoICode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoIShortName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIShortName.FormatString = ""
        repoIShortName.HeaderText = "Item Short Description"
        repoIShortName.Name = colIShortName
        repoIShortName.Width = 150
        repoIShortName.ReadOnly = True
        repoIShortName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoIShortName)



        Dim repoSubLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSubLocation.FormatString = ""
        repoSubLocation.HeaderText = "Sub Location Code"
        repoSubLocation.Name = colSubLocationCode
        repoSubLocation.Width = 150
        repoSubLocation.HeaderImage = My.Resources.search4
        repoSubLocation.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSubLocation.ReadOnly = False
        repoSubLocation.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoSubLocation)

        Dim repoSubLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSubLocationName.FormatString = ""
        repoSubLocationName.HeaderText = "Sub Location Name"
        repoSubLocationName.Name = colSubLocationName
        repoSubLocationName.Width = 150
        repoSubLocationName.ReadOnly = True
        repoSubLocationName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoSubLocationName)



        'clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        'ReStoreGridLayout()
    End Sub
    'Private Sub ReStoreGridLayout()
    '    Try
    '        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
    '            Dim obj As clsGridLayout = New clsGridLayout()
    '            obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
    '            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
    '                Dim ii As Integer
    '                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
    '                    gv1.Columns(ii).IsVisible = False
    '                    gv1.Columns(ii).VisibleInColumnChooser = True
    '                Next
    '                gv1.LoadLayout(obj.GridLayout)
    '                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '            End If
    '        End If
    '    Catch err As Exception
    '        MessageBox.Show(err.Message)
    '    End Try
    'End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    'If e.Column.FieldName.StartsWith("_CFLD_") Then
                    '    clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    'End If

                    If e.Column Is gv1.Columns(colSubLocationCode) Then
                        If clsCommon.myLen(txtLocation.Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please select Location First", Me.Text)
                            isCellValueChangedOpen = False
                            Exit Sub
                        End If

                        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                        If clsCommon.myLen(strICode) > 0 Then
                            OpenSubLocation(False)
                        Else
                            isCellValueChangedOpen = False
                            Exit Sub
                        End If

                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Sub RefreshSerialNo()
        Dim intSerialNo As Integer
        For intCount As Integer = 0 To gv1.Rows.Count - 1
            intSerialNo += 1
            gv1.Rows(intCount).Cells(colLineNo).Value = intSerialNo
        Next
    End Sub

    Sub OpenSubLocation(ByVal isButtonClick As Boolean)
        'Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        Dim qry As String = "select Location_Code as Code , Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim whrCls As String = "Main_Location_Code='" + txtLocation.Value + "'"
        gv1.CurrentRow.Cells(colSubLocationCode).Value = clsCommon.ShowSelectForm("SubLocationFinder@ItemMapping", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colSubLocationCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colSubLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colSubLocationCode).Value) + "'"))
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub


    Sub AddNew()
        LoadBlankGrid()

        txtLocation.Value = ""
        lblLocation.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True

        txtLocation.Enabled = True
        btnGo.Enabled = True

        btnSave.Enabled = False


    End Sub

    Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(txtLocation.Value) = 0 Then
                Throw New Exception("Please enter Location ")
                Exit Function
            End If

            Dim dblNoOfItemSubLocationMapping As Integer = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim strSubLocation As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSubLocationCode).Value)
                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strSubLocation) > 0 Then
                    dblNoOfItemSubLocationMapping += 1
                End If
            Next
            If dblNoOfItemSubLocationMapping = 0 Then
                Throw New Exception("No Item found mapped with Sub location")
                Exit Function
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData(False)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                btnAddNew.Focus()
            End If
        End If
    End Sub

    Private Function SaveData(ByVal isDoAbandomentNo As Boolean) As Boolean
        Try
            Dim qry As String = ""
            If (AllowToSave(Nothing)) Then
                Dim obj As New clsItemSublocationMapping()
                obj.Location_Code = txtLocation.Value
                obj.Arr = New List(Of clsItemSublocationMappingDetail)
                Dim intLine As Integer = 0
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsItemSublocationMappingDetail()
                    intLine += 1

                    objTr.Item_code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Sub_Location_Code = clsCommon.myCstr(grow.Cells(colSubLocationCode).Value)
                    If (clsCommon.myLen(objTr.Sub_Location_Code) > 0) AndAlso (clsCommon.myLen(objTr.Item_code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please fill at list one Sub Location", Me.Text)
                    Return False
                End If
                If (obj.SaveData(obj)) = True Then
                    'LoadData(obj.Document_No, NavigatorType.Current)
                    'clsCommon.MyMessageBoxShow("saved successfully", Me.Text)
                    Return True
                End If

            Else
                Return False
            End If

        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
    End Function




    'Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '    Try
    '        Dim dblTotalDocAmt As Decimal = 0
    '        Dim qry As String = ""
    '        Dim obj As New clsItemSublocationMappingDetail
    '        'Dim intRow As Integer
    '        obj = clsItemSublocationMappingDetail.getData(txtLocation.Value, Nothing)

    '        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

    '            btnSave.Enabled = True
    '            btnPost.Enabled = True
    '            btnDelete.Enabled = True
    '            isInsideLoadData = True
    '            btnCopy.Enabled = False
    '            isNewEntry = False
    '            btnSave.Text = "Update"
    '            BlankAllControls()

    '            LoadBlankGrid()
    '            chkSampling.Checked = IIf(obj.IsSampling = 1, True, False)
    '            chkGatePass.Checked = IIf(obj.AgainstGatePass = 1, True, False)
    '            txtLocation.Enabled = False
    '            txtVendorNo.Enabled = False
    '            txtDocNo.Value = obj.Document_No
    '            txtDate.Value = obj.Document_Date
    '            If EnableCustomerPODetailonDairyBooking = 1 Then
    '                txtSalesman.Value = obj.SalesmanCode
    '                If obj.Podate IsNot Nothing Then
    '                    txtCustPODate.Value = obj.Podate
    '                    txtCustPODate.Checked = True
    '                End If
    '                txtPONo.Text = obj.Cust_PO_No
    '            End If

    '            lblDONumber.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Delivery_No,'') from TSPL_BOOKING_DETAIL where Document_No='" + txtDocNo.Value + "'"))
    '            'DOStatus = clsDBFuncationality.getSingleValue("select isnull(DO_Posted,0) from TSPL_BOOKING_DETAIL where Document_No ='" & txtDocNo.Value & "' and cust_code='" & txtVendorNo.Value & "'")
    '            If obj.Posted = 1 Then
    '                btnSave.Enabled = False
    '                btnDelete.Enabled = False
    '                btnPost.Enabled = False
    '                btnCreateDO.Enabled = True
    '                Dim DOStatus1 = clsDBFuncationality.getSingleValue("select top 1  Document_No from TSPL_BOOKING_DETAIL where DO_Posted <> 4 and Document_No='" & txtDocNo.Value & "'")
    '                If clsCommon.myLen(DOStatus1) = 0 Then
    '                    btnCreateDO.Enabled = False
    '                End If
    '            End If

    '            'If obj.TRANSACTION_TYPE = "FS" Then
    '            '    rbtn_Fresh.IsChecked = True
    '            'ElseIf obj.TRANSACTION_TYPE = "PS" Then
    '            '    rbtn_Ambient.IsChecked = True
    '            'End If


    '            If clsCommon.myLen(obj.Ex_Factory_Date) = 0 Then
    '                txtEx_Factory_Date.Checked = False
    '            Else
    '                txtEx_Factory_Date.Checked = True
    '                txtEx_Factory_Date.Value = obj.Ex_Factory_Date
    '            End If
    '            lblCreatedDateAndTime.Text = obj.Created_Date
    '            ''        cmbBookingType.Text = IIf(obj.Booking_Type = "", "Select", obj.Booking_Type)
    '            txtLocation.Value = obj.location_code
    '            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    '            txtCan.Text = obj.TotalCAN
    '            txtCrate.Text = obj.TotalCrate
    '            txtBox.Text = obj.TotalBox
    '            txtShipToLocation.Value = obj.Ship_To_Location
    '            lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_SHIP_TO_LOCATION.Ship_To_Desc FROM  TSPL_SHIP_TO_LOCATION WHERE Ship_To_Code  ='" + txtShipToLocation.Value + "'"))
    '            If obj.Uploading_date IsNot Nothing Then
    '                lblUploadingDate.Text = obj.Uploading_date
    '            End If

    '            'Sanjay ERO/12/07/18-000371
    '            Is_Cancelled = obj.Is_Cancelled
    '            lblCancelStatus.Text = IIf(obj.Is_Cancelled = 1, "Cancel", "")
    '            If obj.Is_Cancelled = 1 Then
    '                btnCancel.Enabled = False
    '            Else
    '                btnCancel.Enabled = True
    '            End If
    '            'Sanjay ERO/12/07/18-000371

    '            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)

    '            ''''''''''''''''''''''''''''
    '            lblCreditLimit.Text = clsCommon.myCdbl(obj.Credit_Limit)
    '            lblAdvanceSecurity.Text = clsCommon.myCdbl(obj.Advance_Security)
    '            lblReverseAdvanceSec.Text = clsCommon.myCdbl(obj.Revese_Adv_Security)
    '            lblARSecurity.Text = clsCommon.myCdbl(obj.AR_Credit_Security)
    '            lblPendingDO.Text = clsCommon.myCdbl(obj.Pending_Posted_DO)
    '            lblShortcloseDO.Text = clsCommon.myCdbl(obj.UnPostedDispatch)
    '            lblLedgerOutstanding.Text = clsCommon.myCdbl(obj.Ledger_Outstansing)
    '            lblRefund.Text = clsCommon.myCdbl(obj.Refund_Security)
    '            lblReverseRefund.Text = clsCommon.myCdbl(obj.Reverse_Refund_Sec)
    '            lblTotalOutstansing.Text = clsCommon.myCdbl(obj.Total_Outstanding)
    '            lbltotalOutstanding1.Text = clsCommon.myCdbl(obj.Total_Outstanding)
    '            lblTotalSecurity11.Text = clsCommon.myCdbl(obj.Advance_Security) - clsCommon.myCdbl(obj.Revese_Adv_Security) - clsCommon.myCdbl(obj.Refund_Security) + clsCommon.myCdbl(obj.Reverse_Refund_Sec) - clsCommon.myCdbl(obj.AR_Credit_Security)
    '            '''''''''''''''''''''''''''

    '            qry = "SELECT TSPL_BOOKING_DETAIL.*,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_ROUTE_MASTER.Route_Desc FROM TSPL_BOOKING_DETAIL LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code left outer join " &
    '                    " TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No WHERE Document_No='" + txtDocNo.Value + "' and scheme_item='N '"
    '            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            txtVendorNo.Value = clsCommon.myCstr(dt2.Rows(0)("Cust_Code"))
    '            lblVendorName.Text = clsCommon.myCstr(dt2.Rows(0)("Customer_Name"))

    '            If clsCommon.CompairString(obj.Booking_Type, "") = CompairStringResult.Equal Then
    '                cmbBookingType.Text = ""
    '            ElseIf clsCommon.CompairString(obj.Booking_Type, "PS") = CompairStringResult.Equal Then
    '                cmbBookingType.Text = "PARLOR SALES"
    '            ElseIf clsCommon.CompairString(obj.Booking_Type, "FN") = CompairStringResult.Equal Then
    '                cmbBookingType.Text = "FORENOON"
    '            ElseIf clsCommon.CompairString(obj.Booking_Type, "UP") = CompairStringResult.Equal Then
    '                cmbBookingType.Text = "UP COUNTRY"
    '            Else
    '                cmbBookingType.Text = obj.Booking_Type
    '            End If


    '            '====================Added by preeti Gupta Against Ticket no[BHA/01/08/18-000206]=
    '            lblBoothStation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Zone_Code,'') +  case when len(oldname )>0 then ', ' +isnull(oldname,'') else '' end from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"))
    '            lblroutecode.Text = clsCommon.myCstr(dt2.Rows(0)("Route_No"))
    '            lblroutename.Text = clsCommon.myCstr(dt2.Rows(0)("Route_Desc"))
    '            strRoutecode = clsCommon.myCstr(dt2.Rows(0)("Route_No"))
    '            strRouteDesc = clsCommon.myCstr(dt2.Rows(0)("Route_Desc"))
    '            '===============================================================
    '            lblTotRAmt1.Text = clsCommon.myCstr(dt2.Rows(0)("DocumentAmount"))
    '            BookingStatus = clsCommon.myCstr(dt2.Rows(0)("Booking_Status"))
    '            DOStatus = clsCommon.myCstr(dt2.Rows(0)("DO_Posted"))
    '            ''richa agarwal ERO/21/05/19-000609 21 May,2019 add updated vehicle No according to DO
    '            LblUpdatedVehicleCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Lorry_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Booking_No ='" + txtDocNo.Value + "'"))
    '            LblUpdatedVehicleDesc.Text = clsCommon.myCstr(ClsVehicleMaster.GetName(LblUpdatedVehicleCode.Text, Nothing))
    '            setRouteDetail(txtVendorNo.Value, lblroutecode.Text)
    '            ''richa TEC/01/10/19-001025
    '            txtRouteNo.Value = clsCommon.myCstr(dt2.Rows(0)("Route_No"))
    '            lblRouteDesc.Text = clsCommon.myCstr(dt2.Rows(0)("Route_Desc"))
    '            ' done by priti BHA/14/06/18-000052
    '            If clsCommon.myLen(BookingStatus) > 0 Then
    '                If BookingStatus = 1 Then
    '                    txtBOstatus.Text = "Open"
    '                ElseIf BookingStatus = 2 Then
    '                    txtBOstatus.Text = "Pending"
    '                ElseIf BookingStatus = 3 Then
    '                    txtBOstatus.Text = "Approved"
    '                ElseIf BookingStatus = 4 Then
    '                    txtBOstatus.Text = "Posted"
    '                ElseIf BookingStatus = 5 Then
    '                    txtBOstatus.Text = "Rejected"
    '                End If
    '            End If
    '            If clsCommon.myLen(BookingStatus) > 0 Then
    '                If DOStatus = 1 Then
    '                    txtDOStatus.Text = "Open"
    '                ElseIf DOStatus = 2 Then
    '                    txtDOStatus.Text = "Pending"
    '                ElseIf DOStatus = 3 Then
    '                    txtDOStatus.Text = "Approved"
    '                ElseIf DOStatus = 4 Then
    '                    txtDOStatus.Text = "Posted"
    '                ElseIf DOStatus = 5 Then
    '                    txtDOStatus.Text = "Rejected"
    '                End If
    '            End If
    '            For jj As Integer = 0 To dt2.Rows.Count() - 1
    '                gv1.Rows.AddNew()

    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count 'clsCommon.myCdbl(dt2.Rows(jj)("Line_No"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dt2.Rows(jj)("Item_Code"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(dt2.Rows(jj)("Item_Code")) + "'"))
    '                'SKG
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Short_Description from tspl_item_master where item_code='" + clsCommon.myCstr(dt2.Rows(jj)("Item_Code")) + "'"))
    '                'SKG
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt2.Rows(jj)("Item_Code")), Nothing)
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(ColAvailableQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(dt2.Rows(jj)("Item_Code")), txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(dt2.Rows(jj)("Unit_Code")), 0)
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colPreviousQty).Value = clsCommon.myCdbl(dt2.Rows(jj)("PreviousBookingQty"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dt2.Rows(jj)("Booking_Qty"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("OrgRate"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dt2.Rows(jj)("Unit_Code"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("Item_Rate"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colSellingRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("Item_Selling_Price"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = Math.Round(clsCommon.myCdbl(dt2.Rows(jj)("Booking_Qty")) * clsCommon.myCdbl(dt2.Rows(jj)("Item_Rate")), 2)
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt2.Rows(jj)("Tax_On_Amount"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt2.Rows(jj)("Tax_Amount"))
    '                UpdateCurrentRowAvgQty(gv1.CurrentRow.Index)
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Amount).Value = clsCommon.myCdbl(dt2.Rows(jj)("Disc_Scheme_Amount"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Code).Value = clsCommon.myCstr(dt2.Rows(jj)("Disc_Scheme_Code"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Pers).Value = clsCommon.myCdbl(dt2.Rows(jj)("Disc_Scheme_Pers"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Type).Value = clsCommon.myCstr(dt2.Rows(jj)("Disc_Scheme_Type"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeType).Value = clsCommon.myCstr(dt2.Rows(jj)("Scheme_Type"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCdbl(dt2.Rows(jj)("Tax_NonTax"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dt2.Rows(jj)("FreshAmbient"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(dt2.Rows(jj)("Remarks"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemBasicPrice).Value = clsCommon.myCdbl(dt2.Rows(jj)("Price_with_Tax"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmountWithTax).Value = clsCommon.myCdbl(dt2.Rows(jj)("Amount_with_Tax"))
    '                dblTotalDocAmt = dblTotalDocAmt + clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAmountWithTax).Value)
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceId).Value = clsCommon.myCstr(dt2.Rows(jj)("Item_Price_ID"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceIDAppDate).Value = clsCommon.myCstr(dt2.Rows(jj)("Price_IdStartDate"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colPricePlanNo).Value = clsCommon.myCstr(dt2.Rows(jj)("PricePlanNo"))
    '                ''RICHA 06 JUNE,2020
    '                If chkSampling.Checked = False And DonotAllowtoChangeUOMinDairyBookingCustomer = True Then
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).ReadOnly = True
    '                Else
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).ReadOnly = False
    '                End If
    '            Next
    '            lblTotalDocAmt.Text = Math.Round(clsCommon.myCdbl(dblTotalDocAmt), 2)
    '            Try
    '                lblTCSAmount.Text = Math.Round(Math.Round(clsCommon.myCdbl(dblTotalDocAmt), 2) * GetTCSRate(txtVendorNo.Value) / 100, 2)
    '            Catch ex As Exception
    '            End Try

    '            ''to show all items other than booking in case of customer type other than others 25 Feb,2020
    '            If clsCommon.CompairString(txtBOstatus.Text, "Posted") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(txtBOstatus.Text, "Rejected") <> CompairStringResult.Equal Then
    '                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") <> CompairStringResult.Equal Then
    '                    qry = "  select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,TSPL_ITEM_UOM_DETAIL.UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
    '                    " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,0 as Booking_Qty,tspl_item_master.Sku_Seq   from tspl_item_master " & Environment.NewLine &
    '                    " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
    '                    " where isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 " & Environment.NewLine &
    '                    " and tspl_item_master.Item_Code not in ( select tspl_booking_detail.item_code from tspl_booking_detail" & Environment.NewLine &
    '                    " LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine &
    '                    " where tspl_booking_detail.Document_No='" & clsCommon.myCstr(txtDocNo.Value) & "' AND tspl_booking_detail.Scheme_Item ='N') order by Sku_Seq" & Environment.NewLine
    '                    dt2 = clsDBFuncationality.GetDataTable(qry)
    '                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
    '                        For Each dr As DataRow In dt2.Rows
    '                            isCellValueChangedOpen = True
    '                            gv1.Rows.AddNew()
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(dr("Short_Description"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsCommon.myCstr(dr("HSN_Code"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("UOM_Code"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCstr(dr("IsTaxable"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dr("IsFreshAmbient"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPreviousQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
    '                            If clsCommon.myCdbl(clsCommon.myCdbl(dr("Booking_Qty"))) > 0 Then
    '                                ItemPrice(clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(dr("Unit_code")), clsCommon.myCdbl(dr("Booking_Qty")), gv1.Rows.Count - 1)
    '                            End If
    '                            isCellValueChangedOpen = False
    '                        Next
    '                    End If
    '                Else
    '                    qry = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,TSPL_ITEM_UOM_DETAIL.UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
    '                  " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,0 as Booking_Qty,tspl_item_master.Marketing_Seq   from tspl_item_master " & Environment.NewLine &
    '                  " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
    '                  " where isnull(tspl_item_master.Item_Type,'')='F' and isnull(tspl_item_master.TypeOfItm,'')='A' and tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 " & Environment.NewLine &
    '                  " and tspl_item_master.Item_Code not in ( select tspl_booking_detail.item_code from tspl_booking_detail" & Environment.NewLine &
    '                  " LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine &
    '                  " where tspl_booking_detail.Document_No='" & clsCommon.myCstr(txtDocNo.Value) & "' AND tspl_booking_detail.Scheme_Item ='N')" & Environment.NewLine &
    '                  " order by Marketing_Seq"

    '                    dt2 = clsDBFuncationality.GetDataTable(qry)
    '                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
    '                        For Each dr As DataRow In dt2.Rows
    '                            isCellValueChangedOpen = True
    '                            gv1.Rows.AddNew()
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(dr("Short_Description"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsCommon.myCstr(dr("HSN_Code"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("UOM_Code"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCstr(dr("IsTaxable"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dr("IsFreshAmbient"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPreviousQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
    '                            If clsCommon.myCdbl(clsCommon.myCdbl(dr("Booking_Qty"))) > 0 Then
    '                                ItemPrice(clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(dr("Unit_code")), clsCommon.myCdbl(dr("Booking_Qty")), gv1.Rows.Count - 1)
    '                            End If
    '                            isCellValueChangedOpen = False
    '                        Next
    '                    End If
    '                End If
    '            End If
    '            ItemTypePanel.Enabled = False

    '        End If

    '        CustomerOutstandingAmount(txtVendorNo.Value, Nothing)
    '        gv1.Rows.AddNew()
    '        If gv1.Rows.Count > 0 Then
    '            gv1.Focus()
    '            gv1.CurrentRow = gv1.Rows(0)
    '            gv1.CurrentColumn = gv1.Columns(colQty)
    '        End If
    '        'End If

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    Finally
    '        isInsideLoadData = False
    '    End Try
    'End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub






    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub





    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown


        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        End If
    End Sub






    'Sub Export()
    '    If gv1.Rows.Count > 0 Then
    '        ExportToExcel()
    '    Else
    '        common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
    '    End If
    'End Sub
    'Private Sub ExportToExcel()
    '    Try

    '        Dim arrHeader As List(Of String) = New List(Of String)()
    '        Dim strtemp As String = "Booking No : " + txtDocNo.Value
    '        arrHeader.Add(strtemp)
    '        strtemp = "Booking Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")
    '        arrHeader.Add(strtemp)
    '        strtemp = "Customer Name : " + clsCommon.myCstr(lblVendorName.Text)
    '        arrHeader.Add(strtemp)
    '        strtemp = "Location : " + clsCommon.myCstr(lblLocation.Text)
    '        arrHeader.Add(strtemp)
    '        strtemp = "Document Amount : " + clsCommon.myCdbl(lblTotRAmt1.Text)
    '        arrHeader.Add(strtemp)
    '        strtemp = "Created By : " + strCreatedBy
    '        arrHeader.Add(strtemp)
    '        'strtemp = "Transaction Type : " + IIf(rbtn_Fresh.IsChecked = True, "Fresh", "Ambient")
    '        'arrHeader.Add(strtemp)

    '        clsCommon.MyExportToExcelGrid("Booking Entry", gv1, arrHeader, Me.Text)

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
    '    End Try
    'End Sub



    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow

        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        'RefreshReqNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub





    Dim i As Integer


    'Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
    '    If gv1.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gv1.CurrentRow.Index
    '        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '        If intCurrRow = gv1.Rows.Count - 1 Then
    '            gv1.Rows.AddNew()

    '            gv1.CurrentRow = gv1.Rows(intCurrRow)
    '        End If
    '    End If
    'End Sub




    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y' and IsSubLocationWise = 'Y' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("ItemsubLocationMapping", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            gv1.DataSource = Nothing
            If gv1.Rows.Count > 0 Then
                gv1.Focus()
                gv1.Rows(0).Cells(1).BeginEdit()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                Dim qry As String = "  select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description , TSPL_Item_Sublocation_Mapping.Sub_Location_Code , TSPL_LOCATION_MASTER.Location_Desc as Sub_Location_Name from tspl_item_master
                               left outer join (select TSPL_Item_Sublocation_Mapping.Main_Location_Code, TSPL_Item_Sublocation_Mapping.Item_code ,TSPL_Item_Sublocation_Mapping.Sub_Location_Code  from TSPL_Item_Sublocation_Mapping where Main_Location_Code = '" + txtLocation.Value + "') as TSPL_Item_Sublocation_Mapping on TSPL_Item_Sublocation_Mapping.Item_Code = tspl_item_master.item_code
                               left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_Item_Sublocation_Mapping.Sub_Location_Code  
                               where isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1  order by tspl_item_master.Sku_Seq "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For jj As Integer = 0 To dt.Rows.Count() - 1
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dt.Rows(jj)("Item_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dt.Rows(jj)("Item_Desc"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(dt.Rows(jj)("Short_Description"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSubLocationCode).Value = clsCommon.myCstr(dt.Rows(jj)("Sub_Location_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSubLocationName).Value = clsCommon.myCstr(dt.Rows(jj)("Sub_Location_Name"))

                    Next
                    btnGo.Enabled = False
                    txtLocation.Enabled = False
                    btnSave.Enabled = True
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Item Found", Me.Text)
                End If

            Else
                clsCommon.MyMessageBoxShow(Me, "Please slect location first.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please slect location first.", Me.Text)
                Exit Sub
            End If
            Dim strSql As String = " select  '" + txtLocation.Value + "' as [Location Code], '" + lblLocation.Text + "' as [Location Name] ,tspl_item_master.item_code as [Item Code], tspl_item_master.Item_Desc as [Item Name],tspl_item_master.Short_Description as [Item Short Description] , TSPL_Item_Sublocation_Mapping.Sub_Location_Code as [SubLocationCode] , TSPL_LOCATION_MASTER.Location_Desc  as [Sub Location Name] from tspl_item_master
                               Left outer join (Select TSPL_Item_Sublocation_Mapping.Main_Location_Code, TSPL_Item_Sublocation_Mapping.Item_code, TSPL_Item_Sublocation_Mapping.Sub_Location_Code  from TSPL_Item_Sublocation_Mapping where Main_Location_Code = '" + txtLocation.Value + "') as TSPL_Item_Sublocation_Mapping on TSPL_Item_Sublocation_Mapping.Item_Code = tspl_item_master.item_code
            Left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_Item_Sublocation_Mapping.Sub_Location_Code  
                                   "

            transportSql.ExporttoExcel(strSql, " and isnull(tspl_item_master.Chilled_Freezen, 0) = 1 And isnull(tspl_item_master.item_type,'')='F'  and tspl_item_master.Active=1", Me) ' tspl_item_master.Sku_Seq
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please slect location first.", Me.Text)
            Exit Sub
        End If
        Dim gv As New RadGridView()

        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Location Code", "Location Name", "Item Code", "Item Name", "Item Short Description", "SubLocationCode", "Sub Location Name") Then

            Dim linno As Integer = 0
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()


                'clsCommon.ProgressBarShow()
                'For Each grow1 As GridViewRowInfo In gv.Rows
                '    Dim strLocationCode As String = clsCommon.myCstr(grow1.Cells("Location Code").Value)
                '    Dim strItemCode As String = clsCommon.myCstr(grow1.Cells("Item Code").Value)
                '    Dim strSubLocationCode As String = clsCommon.myCstr(grow1.Cells("Sub Location Code").Value)
                '    If String.IsNullOrEmpty(strLocationCode) Then

                '        ShowMsg("To date should be greater than or equal to from date")
                '            Exit Sub

                '    End If

                'Next
                Dim obj As New clsItemSublocationMapping()
                obj.Location_Code = txtLocation.Value
                obj.Arr = New List(Of clsItemSublocationMappingDetail)

                For Each grow As GridViewRowInfo In gv.Rows

                    'Dim obj As New clsItemSublocationMappingDetail()
                    Dim objTr As New clsItemSublocationMappingDetail()

                    linno += 1
                    Dim strLocationCode As String = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    If (String.IsNullOrEmpty(strLocationCode)) Then
                        Throw New Exception("Location Code can't be blank. At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.CompairString(strLocationCode, txtLocation.Value) <> CompairStringResult.Equal Then
                        Throw New Exception("Import Location (" + strLocationCode + ") Code Should be same slected location (" + txtLocation.Value + ") on Screen. At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    objTr.Main_Location_Code = strLocationCode

                    Dim strItemCode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If String.IsNullOrEmpty(strItemCode) Then
                        Throw New Exception("Item Code can't be blank. At Line No.  " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim chkValidItem As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select count (*) from tspl_item_master where isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1 and Item_Code = '" + strItemCode + "'", trans))
                    If chkValidItem = False Then
                        Throw New Exception("Invalid Item Code - (1). Item Should be exist in Item Master (2). Item Should be Chilled Freezen (3).Item Type Should be F (4). Item Should be Active  . At Line No.  " + clsCommon.myCstr(linno) + ".")
                    End If
                    objTr.Item_code = strItemCode

                    Dim strSubLocationCode As String = clsCommon.myCstr(grow.Cells("SubLocationCode").Value)
                    'If String.IsNullOrEmpty(strSubLocationCode) Then
                    '    Throw New Exception("Sub Location Code can't be blank. At Line No.  " + clsCommon.myCstr(linno) + ".")
                    'End If
                    If clsCommon.myLen(strSubLocationCode) Then
                        Dim chkValidSubLocation As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) as Name from TSPL_LOCATION_MASTER where Main_Location_Code='" + txtLocation.Value + "' and Location_Code = '" + strSubLocationCode + "'", trans))
                        If chkValidSubLocation = False Then
                            Throw New Exception("Invalid Sub Location. At Line No.  " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    objTr.Sub_Location_Code = strSubLocationCode

                    If (clsCommon.myLen(objTr.Sub_Location_Code) > 0) AndAlso (clsCommon.myLen(objTr.Item_code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    'clsDBFuncationality.ExecuteNonQuery("delete from TSPL_Item_Sublocation_Mapping where Main_Location_Code = '" + strLocationCode + "' and item_Code = '" + strItemCode + "' and Sub_Location_Code = '" + strSubLocationCode + "'", trans)
                    'clsDBFuncationality.ExecuteNonQuery("", trans)



                Next

                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please fill at list one Sub Location")
                End If
                If (obj.SaveData(obj, trans)) = True Then
                    trans.Commit()
                End If


                'clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                'clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)

    End Sub

    'Private Function CheckItemtaxType() As Boolean
    '    Throw New NotImplementedException
    'End Function



    'Private Sub txtLocation_Leave(sender As Object, e As EventArgs)
    '    If clsCommon.myLen(txtLocation.Value) > 0 Then
    '        If gv1.Rows.Count > 0 Then
    '            gv1.Focus()
    '            gv1.CurrentRow = gv1.Rows(0)
    '            gv1.CurrentColumn = gv1.Columns(colICode)
    '        End If
    '        'gv1.Rows(0).Cells(colICode).IsSelected = True
    '    End If
    'End Sub

End Class

' '' '' ''Created By priti for ticket BM00000003094
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net

Public Class FrmBookingEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Public arrBookingItem As List(Of clsBookingTemp) = Nothing
    Dim dblOutstandingAmt As Double = 0
    Dim dblCreditLimit As Double = 0
    Dim dblSecurityAmount As Double = 0
    Dim dblPendingDeliveryAmt As Double = 0
    Dim dblAmt As Double = 0
    Dim dblShortCloseDoDispatch As Double = 0
    Dim dblReverseSecurityAmount As Double = 0
    Dim dblRefundAmount As Double = 0
    Dim dblReverseRefundAmount As Double = 0
    Dim blnSaveTotalQTy As Boolean = False
    Dim DOmsg As String = ""
    Private isNewEntry As Boolean = False
    Private DOCreated As Boolean = False
    Dim AllowWo_Outstanding As Boolean
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colLocCode As String = "colLocCode"
    Const colLocName As String = "colLocName"
    Const ReportID As String = "BookingGrid"
    Const colQty As String = "colQty"
    Dim strSql As String
    Dim dblCustOutstandingAmt As Double = 0
    Dim ItemMasterPostedData As Boolean
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
            LoadBlankGrid()
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
        AllowWo_Outstanding = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDispatchOutstandingFS & "'")) = 0, False, True)

        AddNew()
        LoadBlankGrid()
        SetUserMgmtNew()
        ItemMasterPostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPriceListMasterPostedData, clsFixedParameterCode.AllowPriceListMasterPostedData, Nothing)) = 1, True, False)
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBookingEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag

    End Sub
    Sub LoadBlankGrid()
        Dim strItemcode As String = String.Empty
        Dim strtotal As String = String.Empty
        Dim strshortDesp As String = String.Empty
        Dim strtotalShort As String = String.Empty
        Dim ISFresh As Integer = 0
        Dim FreshItem As String = String.Empty
        Dim dt1 As DataTable = Nothing
        Dim qry As String = String.Empty
        Dim dt As DataTable = Nothing
        Try
            'Dim qry As String = "select Item_Code from tspl_item_master where is_freshitem=1"
            '' Anubhooti 10-Sep-2014 BM00000003847
            ISFresh = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(Item_Code) AS Item_Code  from tspl_item_master where is_freshitem=1"))
            If ISFresh = 0 Then
                clsCommon.MyMessageBoxShow("There is No Items For Fresh Sale..")
                Exit Sub
            End If
            FreshItem = clsCommon.myCstr("select Isnull(Item_Code,'') As Item_Code from tspl_item_master where is_freshitem=1 and Posted='1' And Isnull(Short_Description,'')=''")
            dt1 = clsDBFuncationality.GetDataTable(FreshItem)

            For Each dr1 As DataRow In dt1.Rows
                strItemcode = clsCommon.myCstr(dr1("Item_Code"))
                If clsCommon.myLen(strItemcode) > 0 Then
                    strtotal = strtotal + "," + "[" + strItemcode + "]"
                End If
            Next
            If strtotal.Length > 0 Then
                If strtotal.Substring(0, 1) = "," Then
                    strtotal = strtotal.Substring(1, strtotal.Length - 1)
                End If
            End If

            If strtotal.Length > 0 Then
                clsCommon.MyMessageBoxShow("Please enter short description for fresh items (" & strtotal & ")")
                Exit Sub
            End If
            qry = "select Isnull(Short_Description + '  ' +  TSPL_ITEM_UOM_DETAIL.UOM_Code,'') As Short_Description  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  where is_freshitem=1  "

            qry += " order by Sku_Seq asc"
            dt = clsDBFuncationality.GetDataTable(qry)

            For Each dr As DataRow In dt.Rows
                strshortDesp = clsCommon.myCstr(dr("Short_Description"))
                If clsCommon.myLen(strshortDesp) > 0 Then
                    strtotalShort = strtotalShort + "," + "[" + strshortDesp + "]"
                End If
            Next
            'If strtotal.Length > 0 Then
            '    If strtotalShort.Length <= 0 Then
            '        'clsCommon.MyMessageBoxShow("There is No Items For Fresh Sale..")
            '        clsCommon.MyMessageBoxShow("Please enter short description for fresh items (" & strtotal & ")")
            '        Exit Sub
            '    End If
            'End If
            Try
                If strtotalShort.Substring(0, 1) = "," Then
                    strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
                End If
            Catch ex As Exception
            End Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                ' qry = "select * from (select  distinct Line_No,TSPL_BOOKING_DETAIL.Cust_Code as [Customer Code],TSPL_BOOKING_DETAIL.Loc_Code as [Location Code],TSPL_BOOKING_DETAIL.Item_Code, isnull(Booking_Qty,0) as Qty from TSPL_BOOKING_DETAIL where Document_No='" & txtDocNo.Value & "') as t pivot(max(Qty) for item_code in (" & strtotal & ") ) as pivot1"
                '' Anubhooti 10-Sep-2014 BM00000003847 (Remarks Change Item_Code to Short_Description In Both Queries)
                If btnPost.Enabled = True And chkCreateDO.Checked = False Then
                    qry = "select * from (select  distinct Line_No,TSPL_BOOKING_DETAIL.Cust_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],TSPL_BOOKING_DETAIL.Loc_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc AS [Location Name],Zone_Code,TSPL_BOOKING_DETAIL.Vehicle_Code As [Vehicle Id],Number as [Vehicle No],Cast(Sampling as bit) as [Sampling], isnull(Delivery_No,'') as Delivery_No,isnull(Cust_Outstanding,0) as Cust_Outstanding,isnull(DocumentAmount,0) as DocumentAmount,DO_Posted,Balance,Total_Qty,isnull(TSPL_ITEM_MASTER.Short_Description+ '  '  + TSPL_ITEM_UOM_DETAIL.UOM_Code,'') AS Short_Description, isnull(DO_Qty,0) as Qty from TSPL_BOOKING_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER  ON TSPL_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_LOCATION_MASTER  ON TSPL_BOOKING_DETAIL.Loc_Code =TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN TSPL_ROUTE_MASTER  ON TSPL_ROUTE_MASTER.Route_No   =TSPL_CUSTOMER_MASTER.Route_No LEFT OUTER JOIN TSPL_VEHICLE_MASTER  ON TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_BOOKING_DETAIL.Vehicle_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  where Document_No='" & txtDocNo.Value & "') as t pivot(max(Qty) for Short_Description in (" & strtotalShort & ") ) as pivot1"
                Else
                    qry = "select * from (select  distinct Line_No,TSPL_BOOKING_DETAIL.Cust_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],TSPL_BOOKING_DETAIL.Loc_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc AS [Location Name],Zone_Code,TSPL_BOOKING_DETAIL.Vehicle_Code As [Vehicle Id],Number as [Vehicle No],Cast(Sampling as bit) as [Sampling],isnull(Delivery_No,'') as Delivery_No,isnull(Cust_Outstanding,0) as Cust_Outstanding,isnull(DocumentAmount,0) as DocumentAmount,case when DO_Posted=1 then 'Open' when DO_Posted=2 then 'Pending' when DO_Posted=3 then 'Approved' when DO_Posted=4 then 'Posted' else '' end As DO_Posted,Balance,Total_Qty,isnull(TSPL_ITEM_MASTER.Short_Description+ '  '  + TSPL_ITEM_UOM_DETAIL.UOM_Code,'') AS Short_Description, isnull(DO_Qty,0) as Qty from TSPL_BOOKING_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER  ON TSPL_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_LOCATION_MASTER  ON TSPL_BOOKING_DETAIL.Loc_Code =TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN TSPL_ROUTE_MASTER  ON TSPL_ROUTE_MASTER.Route_No   =TSPL_CUSTOMER_MASTER.Route_No LEFT OUTER JOIN TSPL_VEHICLE_MASTER  ON TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_BOOKING_DETAIL.Vehicle_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  where Document_No='" & txtDocNo.Value & "') as t pivot(max(Qty) for Short_Description in (" & strtotalShort & ") ) as pivot1"
                End If
            Else
                qry = "select * from (select 1 as Line_No ,'' as [Customer Code],'' AS [Customer Name],'' as [Location Code],'' AS [Location Name],'' as Zone_Code,'' As [Vehicle Id],'' as [Vehicle No],Cast(0 as bit) as [Sampling],'' as Delivery_No,0 as Cust_Outstanding,0 as DocumentAmount,'' as DO_Posted,0 as Balance,0 as Total_Qty,'' As [Short_Description] , 0 as Qty  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1) as t pivot(max(Qty) for Short_Description in (" & strtotalShort & ") ) as pivot1"
            End If
            gv1.DataSource = Nothing
            gv1.DataSource = clsDBFuncationality.GetDataTable(qry)
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.Columns("Line_No").HeaderText = "Line No"
            gv1.Columns("Line_No").AllowFiltering = False

            gv1.Columns("Customer Code").HeaderImage = Global.ERP.My.Resources.Resources.search4
            gv1.Columns("Customer Code").TextImageRelation = TextImageRelation.TextBeforeImage
            gv1.Columns("Customer Code").Width = 80

            gv1.Columns("Location Code").HeaderImage = Global.ERP.My.Resources.Resources.search4
            gv1.Columns("Location Code").TextImageRelation = TextImageRelation.TextBeforeImage
            gv1.Columns("Location Code").Width = 80

            '' Anubhooti 10-Sep-2014
            gv1.Columns("Customer Name").HeaderText = "Customer Name"
            gv1.Columns("Customer Name").ReadOnly = True
            gv1.Columns("Customer Name").Width = 100

            gv1.Columns("Location Name").HeaderText = "Location Name"
            gv1.Columns("Location Name").ReadOnly = True
            gv1.Columns("Location Name").Width = 100

            gv1.Columns("Vehicle Id").HeaderText = "Vehicle Id"
            gv1.Columns("Vehicle Id").ReadOnly = False
            gv1.Columns("Vehicle Id").Width = 80

            gv1.Columns("Vehicle No").HeaderText = "Vehicle No"
            gv1.Columns("Vehicle No").ReadOnly = True
            gv1.Columns("Vehicle No").Width = 80

            gv1.Columns("Zone_Code").HeaderText = "Zone"
            gv1.Columns("Zone_Code").ReadOnly = True
            gv1.Columns("Zone_Code").Width = 80

            gv1.Columns("Delivery_No").HeaderText = "Delivery No"
            gv1.Columns("Delivery_No").ReadOnly = True
            gv1.Columns("Delivery_No").Width = 80
            gv1.Columns("Delivery_No").ReadOnly = True

            gv1.Columns("Cust_Outstanding").HeaderText = "Credit Limit"
            gv1.Columns("Cust_Outstanding").ReadOnly = True
            gv1.Columns("Cust_Outstanding").Width = 80
            gv1.Columns("Cust_Outstanding").ReadOnly = True

            gv1.Columns("DocumentAmount").HeaderText = "DO Amount"
            gv1.Columns("DocumentAmount").ReadOnly = True
            gv1.Columns("DocumentAmount").Width = 80
            gv1.Columns("DocumentAmount").ReadOnly = True

            gv1.Columns("Balance").HeaderText = "Balance"
            gv1.Columns("Balance").ReadOnly = True
            gv1.Columns("Balance").Width = 80
            gv1.Columns("Balance").ReadOnly = True

            gv1.Columns("DO_Posted").HeaderText = "DO Status"
            gv1.Columns("DO_Posted").ReadOnly = True
            gv1.Columns("DO_Posted").Width = 80
            gv1.Columns("DO_Posted").ReadOnly = True

            gv1.Columns("Total_Qty").HeaderText = "Total Qty"
            gv1.Columns("Total_Qty").ReadOnly = True
            gv1.Columns("Total_Qty").Width = 80
            gv1.Columns("Total_Qty").ReadOnly = True



            gv1.Columns("Delivery_No").IsVisible = False
            'gv1.Columns("Cust_Outstanding").IsVisible = False
            'gv1.Columns("DocumentAmount").IsVisible = False
            'gv1.Columns("Balance").IsVisible = False
            gv1.Columns("DO_Posted").IsVisible = False
            gv1.Columns("Customer Code").ReadOnly = False
            gv1.Columns("Location Name").ReadOnly = False
            gv1.Columns("Line_No").ReadOnly = True

            Dim intCountColumn As Integer = 15
            If btnPost.Enabled = False And btnCreateDO.Enabled = True Then
                gv1.Columns("Delivery_No").IsVisible = True
                'gv1.Columns("Cust_Outstanding").IsVisible = True
                gv1.Columns("DocumentAmount").IsVisible = True
                gv1.Columns("Balance").IsVisible = True
                gv1.Columns("DO_Posted").IsVisible = True
                gv1.Columns("Customer Code").ReadOnly = True
                gv1.Columns("Location Name").ReadOnly = True
                intCountColumn = 15
            End If


            Dim summaryRowItem As New GridViewSummaryRowItem()
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).BestFit()
                'If ii >= 14 Then
                '    Dim strItem = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Code  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 Where Short_Description + '  ' +  TSPL_ITEM_UOM_DETAIL.UOM_Code='" & gv1.Columns(ii).Name.ToString() & "'", trans))
                '    Dim strUnitCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from  TSPL_ITEM_UOM_DETAIL  where Default_UOM=1 and Item_Code='" & objTr.Item_Code & "'", trans))
                'End If
            Next
            For ii As Integer = intCountColumn To gv1.Columns.Count - 1
                Dim item8 As New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item8)
            Next
            'If btnPost.Enabled = False And chkCreateDO.Checked Then
            '    Dim intCountPosted As Integer = 0
            '    Dim intCountPostedinner As Integer = 0
            '    For ii As Integer = 0 To gv1.Rows.Count - 1
            '        If clsCommon.myLen(gv1.Rows(ii).Cells("Delivery_No").Value) > 0 Then
            '            CustomerOutstandingAmount(gv1.Rows(ii).Cells("Customer Code").Value, gv1.Rows(ii).Cells("DocumentAmount").Value, Nothing, gv1.Rows(ii).Cells("Delivery_No").Value)
            '            gv1.Rows(ii).Cells("Cust_Outstanding").Value = dblCustOutstandingAmt
            '            gv1.Rows(ii).Cells("Balance").Value = clsCommon.myCdbl(gv1.Rows(ii).Cells("Cust_Outstanding").Value) - clsCommon.myCdbl(gv1.Rows(ii).Cells("DocumentAmount").Value)
            '            If clsCommon.CompairString(gv1.Rows(ii).Cells("DO_Posted").Value, "Posted") = CompairStringResult.Equal Then
            '                intCountPostedinner += 1
            '            End If
            '        End If
            '        intCountPostedinner += 1
            '    Next
            '    If intCountPosted = intCountPostedinner Then
            '        btnCreateDO.Enabled = False
            '    End If
            'End If


            Dim intCountPosted As Integer = 0
            Dim intCountPostedinner As Integer = 0
            Dim intLine As Integer = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                intLine += 1
                gv1.Rows(ii).Cells("Line_No").Value = intLine
                CustomerOutstandingAmount(gv1.Rows(ii).Cells("Customer Code").Value, gv1.Rows(ii).Cells("DocumentAmount").Value, Nothing, gv1.Rows(ii).Cells("Delivery_No").Value)
                gv1.Rows(ii).Cells("Cust_Outstanding").Value = dblCustOutstandingAmt
                gv1.Rows(ii).Cells("Balance").Value = clsCommon.myCdbl(gv1.Rows(ii).Cells("Cust_Outstanding").Value) - clsCommon.myCdbl(gv1.Rows(ii).Cells("DocumentAmount").Value)

                If clsCommon.myLen(gv1.Rows(ii).Cells("Delivery_No").Value) > 0 Then
                    If clsCommon.CompairString(gv1.Rows(ii).Cells("DO_Posted").Value, "Posted") = CompairStringResult.Equal Then
                        intCountPostedinner += 1
                    End If
                End If
                intCountPosted += 1
            Next
            If intCountPosted = intCountPostedinner Then
                btnCreateDO.Enabled = False
            End If


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

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
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            strItemcode = Nothing
            strtotal = Nothing
            strshortDesp = Nothing
            strtotalShort = Nothing
            FreshItem = String.Empty
            dt1 = Nothing
            qry = String.Empty
            dt = Nothing
        End Try

    End Sub
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
        blnSaveTotalQTy = False
        btnCreateDO.Enabled = False
        isNewEntry = True
        btnSave.Text = "Save"
        txtDocNo.Value = ""
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        'LoadBlankGrid()
        chkCreateDO.Checked = False
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If e.Column.Name = "Cust_Outstanding" Then
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells("Total_Qty").Value)
            If dblQty > 0 Then
                CustomerOutstandingAmount(clsCommon.myCstr(gv1.Rows(e.RowIndex).Cells("Customer Code").Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells("Total_Qty").Value), Nothing, "")
                Dim frm As New frmFreshCreditLimit
                frm.dblCreditLimit = dblCreditLimit
                frm.dblSecurityAmount = dblSecurityAmount
                frm.dblReverseSecurityAmount = dblReverseSecurityAmount
                frm.dblPendingDeliveryAmt = dblPendingDeliveryAmt
                frm.dblOutstandingAmt = dblOutstandingAmt
                frm.dblRefundAmount = dblRefundAmount
                frm.dblReverseRefundAmount = dblReverseRefundAmount
                frm.dblAmt = dblAmt
                frm.dblShortCloseDoDispatch = dblShortCloseDoDispatch
                frm.Show()

            End If
          
        End If

    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(1) Then
                        OpenCustomerFinder(False)
                    ElseIf e.Column Is gv1.Columns(3) Then
                        OpenLocationFinder(False)
                    ElseIf e.Column Is gv1.Columns(6) Then
                        OpenVehicleFinder(False)
                    ElseIf e.Column.Index > 12 And isLoadData = False Then
                        CheckBookingQty(False)
                    End If
                End If
                isCellValueChangedOpen = False
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
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
                qry = "select Customer_Name,vehicle_code,Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value) & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    gv1.CurrentRow.Cells("Customer Name").Value = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                    gv1.CurrentRow.Cells("Vehicle Id").Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
                    gv1.CurrentRow.Cells("Vehicle No").Value = clsDBFuncationality.getSingleValue("select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" & clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle Id").Value) & "' ")
                    gv1.CurrentRow.Cells("Zone_Code").Value = clsCommon.myCstr(dt.Rows(0)("Zone_Code"))
                    Dim strRoute = clsCommon.myCstr(dt.Rows(0)("Route_No"))
                    If clsCommon.myLen(strRoute) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please Map Route for customer " & gv1.CurrentRow.Cells("Customer Name").Value, Me.Text)
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

                Dim dblVehiclecapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select crateCapacity  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle Id").Value) + "'"))

                For ii As Integer = 15 To gv1.Columns.Count - 1
                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(ii).Value) > 0 Then
                        Qty = gv1.CurrentRow.Cells(ii).Value
                        dblTotalQty = dblTotalQty + Qty
                    End If
                Next
                gv1.CurrentRow.Cells("Total_Qty").Value = dblTotalQty
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Vehicle Id").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle Id").Value)) = CompairStringResult.Equal Then
                        For ii As Integer = 15 To gv1.Columns.Count - 1
                            If clsCommon.myCdbl(grow.Cells(ii).Value) > 0 Then
                                strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Code  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 Where Short_Description + '  ' +  TSPL_ITEM_UOM_DETAIL.UOM_Code='" & gv1.Columns(ii).Name.ToString() & "'"))
                                strUnit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from  TSPL_ITEM_UOM_DETAIL  where Default_UOM=1 and Item_Code='" & strItemCode & "'"))
                                strCrate = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Crate_Type  from  TSPL_UNIT_MASTER where Unit_Code='" & strUnit & "'"))
                                If clsCommon.CompairString(strCrate, "Y") = CompairStringResult.Equal Then
                                    dblTotal += clsCommon.myCdbl(grow.Cells(ii).Value)
                                End If

                            End If
                        Next
                    End If

                Next

                If dblTotal > dblVehiclecapacity Then
                    If clsCommon.MyMessageBoxShow("Crate qty is exceeded do you want to change vehicle.", "Booking", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        gv1.CurrentColumn = gv1.Columns("Vehicle Id")
                        'OpenVehicleFinder(False)
                    End If

                End If


            End If
        Catch ex As Exception
        Finally
            strCrate = Nothing
            strUnit = Nothing
            strItemCode = Nothing
        End Try

    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colCustCode) Then
                gv1.CurrentColumn = gv1.Columns(colCustName)
                OpenCustomerFinder(True)
                gv1.CurrentColumn = gv1.Columns(colCustCode)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colLocCode) Then
                gv1.CurrentColumn = gv1.Columns(colLocName)
                OpenLocationFinder(True)
                gv1.CurrentColumn = gv1.Columns(colLocCode)

            End If
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
        AddNew()
        LoadBlankGrid()
    End Sub
    Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Dim strCustCode As String = String.Empty
        Dim strCustName As String = String.Empty
        Dim strLocCode As String = String.Empty
        Dim strVehicleCode As String = String.Empty
        Dim strInCustCode As String = String.Empty
        Dim strInLocCode As String = String.Empty
        Dim strInnerVehicleCode As String = String.Empty
        Try

            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(txtDate.Value, trans) = False Then
                txtDate.Select()
                trans.Rollback()
                Return False
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                strCustCode = clsCommon.myCstr(gv1.Rows(ii).Cells(1).Value)
                strCustName = clsCommon.myCstr(gv1.Rows(ii).Cells(2).Value)
                strLocCode = clsCommon.myCstr(gv1.Rows(ii).Cells(3).Value)
                strVehicleCode = clsCommon.myCstr(gv1.Rows(ii).Cells("Vehicle Id").Value)
                Dim intSampling As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells("Sampling").Value)
                If clsCommon.myLen(strCustCode) > 0 Then
                    If clsCommon.myLen(strLocCode) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter Location at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If clsCommon.myLen(strVehicleCode) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter Vehicle or Map Route for customer " & strCustName & " at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If

                    For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                        strInCustCode = clsCommon.myCstr(gv1.Rows(jj).Cells(1).Value)
                        strInLocCode = clsCommon.myCstr(gv1.Rows(jj).Cells(3).Value)
                        strInnerVehicleCode = clsCommon.myCstr(gv1.Rows(jj).Cells("Vehicle Id").Value)
                        Dim intInnerSampling As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells("Sampling").Value)

                        If clsCommon.CompairString(intSampling, intInnerSampling) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strCustCode, strInCustCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strVehicleCode, strInnerVehicleCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strLocCode, strInLocCode) = CompairStringResult.Equal Then
                            common.clsCommon.MyMessageBoxShow("Customer Code " + strCustCode + " and Location Code " + strLocCode + " and Vehicle Code " + strVehicleCode + " repeat at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1), Me.Text)
                            Return False
                        End If
                    Next

                    Dim Qty As Double = 0
                    Dim dblTotalQty As Double = 0

                    For intcount As Integer = 15 To gv1.Columns.Count - 1
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(intcount).Value) > 0 Then
                            Qty = gv1.Rows(ii).Cells(intcount).Value
                            dblTotalQty = dblTotalQty + Qty
                        End If
                    Next
                    gv1.Rows(ii).Cells("Total_Qty").Value = dblTotalQty
                End If
            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        Finally
            strCustCode = Nothing
            strLocCode = Nothing
            strVehicleCode = Nothing
            strInCustCode = Nothing
            strInLocCode = Nothing
            strInnerVehicleCode = Nothing
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SaveData()

        GC.Collect()
        blnSaveTotalQTy = True
        Dim qry As String = String.Empty
        Dim obj As New clsBookingEntry()
        '' start here

        For ii As Integer = 15 To gv1.Columns.Count - 1

            Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
            objBookingItem.ItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Code  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 Where Short_Description + '  ' +  TSPL_ITEM_UOM_DETAIL.UOM_Code='" & gv1.Columns(ii).Name.ToString() & "'"))
            objBookingItem.UnitCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from  TSPL_ITEM_UOM_DETAIL  where Default_UOM=1 and Item_Code='" & objBookingItem.ItemCode & "'"))
            objBookingItem.ShortDesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description  from tspl_item_master where item_code='" & objBookingItem.ItemCode & "'"))
            gv1.Columns(ii).Tag = objBookingItem

            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(grow.Cells(ii).Value) > 0 Then
                    Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & clsCommon.myCstr(grow.Cells(1).Value) & "'")
                    qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code from ( " & _
            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
            "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
            "Item_Basic_Price,Item_Basic_Net,Price_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null) and  " & _
            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & objBookingItem.UnitCode & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objBookingItem.ItemCode & "' AND Location_Code='" & clsCommon.myCstr(grow.Cells(3).Value) & "'"

                    If ItemMasterPostedData = True Then
                        qry += " and Posted='1'  "
                    End If
                    qry += ") XXXE WHERE RowNo=1  "


                    Dim dt As New DataTable()
                    Dim dblRate As Double = 0
                    Dim dblTotal As Double = 0
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt.Rows.Count > 0 Then
                        dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                    End If
                    grow.Cells(ii).Tag = dblRate
                Else
                    grow.Cells(ii).Tag = 0
                End If

            Next


        Next

        '' ends here
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim blnRatezero As Boolean = False
            DOmsg = ""
            If (AllowToSave(trans)) Then


                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Arr = New List(Of clsBookingDetail)
                Dim intarr As Integer = 0
                For ii As Integer = 15 To gv1.Columns.Count - 1

                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim objTr As New clsBookingDetail()
                        objTr.Booking_Qty = clsCommon.myCdbl(grow.Cells(ii).Value)

                        If objTr.Booking_Qty > 0 Then
                            objTr.Line_No = clsCommon.myCdbl(grow.Cells(0).Value)
                            objTr.Cust_Code = clsCommon.myCstr(grow.Cells(1).Value)
                            objTr.Sampling = clsCommon.myCdbl(grow.Cells("Sampling").Value)
                            objTr.Loc_Code = clsCommon.myCstr(grow.Cells(3).Value)
                            objTr.Total_Qty = clsCommon.myCdbl(grow.Cells("Total_Qty").Value)
                            objTr.Vehicle_Code = clsCommon.myCstr(grow.Cells("Vehicle Id").Value)
                            Dim objBookingitem As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
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
                            If ItemMasterPostedData = True Then
                                If dblRate = 0 Then
                                    blnRatezero = True
                                    DOmsg += "Please create and Post Price chart For customer " & grow.Cells(1).Value & "  Location - " & grow.Cells(3).Value & "   item -  " & objTr.Item_Code & "  Unit - " & objTr.Unit_code & "." + Environment.NewLine
                                End If
                            Else
                                If dblRate = 0 Then
                                    blnRatezero = True
                                    DOmsg += "Please create Price chart For customer " & grow.Cells(1).Value & "  Location -  " & grow.Cells(3).Value & "  for item -  " & objTr.Item_Code & " Unit - " & objTr.Unit_code & "." + Environment.NewLine
                                End If
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
                    clsCommon.MyMessageBoxShow(DOmsg)
                    Return
                End If
                If (obj.Document_No Is Nothing OrElse obj.Arr.Count <= 0) Then
                    trans.Rollback()
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Booking")
                    Return
                End If

                If (obj.SaveData(obj, isNewEntry, trans)) = True Then
                    Dim intSampling As Integer = 0
                    Dim dblQty As Double = 0
                    Dim dblRate As Double = 0
                    Dim dblAmount As Double = 0
                    Dim dblTotal As Double = 0
                    For Each grow As GridViewRowInfo In gv1.Rows
                        dblTotal = 0
                        If clsCommon.myLen(grow.Cells(1).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(3).Value) > 0 Then
                            intSampling = clsCommon.myCdbl(grow.Cells("Sampling").Value)

                            For ii As Integer = 15 To gv1.Columns.Count - 1
                                Dim objBookingitem As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)

                                If (clsCommon.myCdbl(grow.Cells(ii).Value)) > 0 Then
                                    dblQty = clsCommon.myCdbl(grow.Cells(ii).Value)
                                    dblRate = grow.Cells(ii).Tag
                                    dblAmount = clsCommon.myCdbl(dblRate * clsCommon.myCdbl(grow.Cells(ii).Value))
                                    dblTotal += dblAmount
                                End If
                            Next
                            qry = "Update TSPL_BOOKING_DETAIL set DocumentAmount=" & dblTotal & " where  vehicle_code='" & clsCommon.myCstr(grow.Cells("Vehicle Id").Value) & "' and  Document_No='" & obj.Document_No & "' and Cust_Code='" & clsCommon.myCstr(grow.Cells(1).Value) & "' and    Loc_Code='" & clsCommon.myCstr(grow.Cells(3).Value) & "' and Sampling=" & intSampling & ""
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    Next
                    trans.Commit()
                    LoadData(obj.Document_No, NavigatorType.Current)
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                Else
                    trans.Rollback()
                End If
            End If
            blnSaveTotalQTy = False
        Catch ex As Exception
            'trans.Rollback()
            blnSaveTotalQTy = False
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
    End Sub
    Private Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal dblTotal As Double, ByVal Trans As SqlTransaction, ByVal strDoc As String) As Boolean
        Dim qry As String = String.Empty
        Try
            dblOutstandingAmt = 0
            dblCreditLimit = 0
            dblSecurityAmount = 0
            dblPendingDeliveryAmt = 0
            dblAmt = 0
            dblShortCloseDoDispatch = 0
            dblReverseSecurityAmount = 0
            dblRefundAmount = 0
            dblReverseRefundAmount = 0

            ' qry = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
            ' "select SUM(isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & _
            ' "where isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='N'  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1 and Sampling=0  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "'  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No not in ('" & strDoc & "')  " & _
            ' " union all " & _
            ' "select SUM(isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_SD_SHIPMENT_HEAD  " & _
            ' "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
            ' "left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No " & _
            ' "where  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "' and  " & _
            ' "isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='Y'  " & _
            ' " union all " & _
            '"select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
            '"TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
            '"where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
            '" union all " & _
            '"select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
            '"where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
            '" union all " & _
            '"select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
            '"where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
            ' " union all " & _
            '"select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
            '"where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
            '" union all " & _
            '"select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
            '" union all " & _
            '"select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "
         
            Dim strcustomerfilter As String = String.Empty
            strcustomerfilter = "'" + strCustomer + "'"
            qry = "Select  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt From ( " & _
                "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode,  " & _
                "null as ConvRate, SUM(DrAmt* Final.ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote,  " & _
                "0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,  " & _
                "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from   "
            '"(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", strcustomerfilter, True, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"), "", False, False, True, Trans) & "   " & _

            qry += "(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", strcustomerfilter, False, clsCommon.GetPrintDate(objCommonVar.ERPStartDate, "dd/MMM/yyyy"), clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"), False, False, True, Trans) & "   " &
" ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " &
                "where  CONVERT(DATE,final.DocDate,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND LEN(ACode)>0 and ACode in ('" & strCustomer & "')   AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode " &
                ") XXX GROUP BY ACode ORDER BY ACode"


            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'", Trans))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", Trans))
            dblReverseSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and  Receipt_Type='P' and  SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", Trans))
            dblRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='F' and SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", Trans))
            dblReverseRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and Receipt_Type='F' and SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", Trans))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where isnull(TSPL_DELIVERY_NOTE_Master_FRESHSALE.Short_Close,'N')='N' and  posted=0 and Sampling=0 and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No <> '" & strDoc & "' and Customer_Code='" & strCustomer & "'", Trans))

            qry = "select SUM(isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_SD_SHIPMENT_HEAD  " & _
                  "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
                  "left outer join TSPL_DELIVERY_NOTE_Master_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_Master_FRESHSALE.Document_No " & _
                  "where  TSPL_DELIVERY_NOTE_Master_FRESHSALE.Customer_Code='" & strCustomer & "' and  " & _
                  "isnull(TSPL_DELIVERY_NOTE_Master_FRESHSALE.Short_Close,'N')='Y' and  TSPL_SD_SHIPMENT_HEAD.Status=0 and Trans_Type='FS' "
            dblShortCloseDoDispatch = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))

            dblAmt = dblCreditLimit + dblSecurityAmount - dblReverseSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt - dblShortCloseDoDispatch - dblRefundAmount + dblReverseRefundAmount
            'dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt
            dblCustOutstandingAmt = dblAmt

            If dblAmt < dblTotal Then
                Dim dblNewCredtitLimit = dblAmt - dblTotal
                'common.clsCommon.MyMessageBoxShow("Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit))
                Return False

            End If

            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Function
    'Private Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal dblTotal As Double, ByVal Trans As SqlTransaction, ByVal strDoc As String) As Boolean
    '    Dim qry As String = String.Empty
    '    Try
    '        Dim dblOutstandingAmt As Double = 0
    '        Dim dblCreditLimit As Double = 0
    '        Dim dblSecurityAmount As Double = 0
    '        Dim dblPendingDeliveryAmt As Double = 0
    '        Dim dblAmt As Double = 0

    '        qry = "Select  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt From ( " & _
    '                 "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode,  " & _
    '                 "null as ConvRate, SUM(DrAmt* Final.ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote,  " & _
    '                 "0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,  " & _
    '                 "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from   " & _
    '                 "(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", False, False, True) & "   " & _
    '                 " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " & _
    '                 "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " & _
    '                 "where  CONVERT(DATE,final.DocDate,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND LEN(ACode)>0 and ACode in ('" & strCustomer & "')   AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode " & _
    '                 ") XXX GROUP BY ACode ORDER BY ACode"

    '        ' qry = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
    '        ' "select SUM(isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & _
    '        ' "where isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='N'  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1 and Sampling=0  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "'  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No not in ('" & strDoc & "')  " & _
    '        ' " union all " & _
    '        ' "select SUM(isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_SD_SHIPMENT_HEAD  " & _
    '        ' "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " & _
    '        ' "left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No " & _
    '        ' "where  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "' and  " & _
    '        ' "isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='Y'  " & _
    '        ' " union all " & _
    '        '"select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
    '        '"TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
    '        '"where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
    '        '" union all " & _
    '        '"select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '        '"where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
    '        '" union all " & _
    '        '"select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '        '"where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
    '        ' " union all " & _
    '        '"select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '        '"where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
    '        '" union all " & _
    '        '"select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
    '        '" union all " & _
    '        '"select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

    '        dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
    '        dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'", Trans))
    '        dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", Trans))
    '        qry = "select sum(Total_Amt) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE  " & _
    '        "where  posted=1 and Sampling=0  and document_no not in (Select isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,'') from TSPL_SD_SHIPMENT_HEAD   " & _
    '        "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 and Trans_Type='FS' )  " & _
    '        "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.customer_code='" & strCustomer & "'"
    '        dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))

    '        dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt
    '        dblCustOutstandingAmt = dblAmt

    '        If dblAmt < dblTotal Then
    '            Dim dblNewCredtitLimit = dblAmt - dblTotal
    '            'common.clsCommon.MyMessageBoxShow("Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit))
    '            Return False

    '        End If

    '        Return True
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    Finally
    '        qry = Nothing
    '    End Try
    'End Function

    Private Function CreateDO(ByVal ChekPostBtn As Boolean, ByVal trans As SqlTransaction, ByVal strBookingNo As String)
        Try
            blnSaveTotalQTy = True
            Dim qry As String = String.Empty
            DOmsg = String.Empty
            Dim dblTotal_Qty As Double = 0
            Dim blnRatezero As Boolean = False
            If (AllowToSave(trans)) Then



                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(1).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(3).Value) > 0 Then
                        If chkCreateDO.Checked = False OrElse (chkCreateDO.Checked = True AndAlso clsCommon.myLen(grow.Cells("Delivery_No").Value) = 0) Then
                            Dim obj As New clsDeliveryNoteFreshSale
                            dblTotal_Qty = 0
                            obj.Credit_Limit = 0
                            obj.Document_Date = txtDate.Value
                            obj.Customer_Code = clsCommon.myCstr(grow.Cells(1).Value)
                            obj.Location_Code = clsCommon.myCstr(grow.Cells(3).Value)
                            dblTotal_Qty = clsCommon.myCdbl(grow.Cells("Total_Qty").Value)
                            obj.Sampling = clsCommon.myCdbl(grow.Cells("Sampling").Value)
                            obj.Booking_No = strBookingNo
                            obj.Booking_Date = txtDate.Value
                            obj.Vehicle_Capacity = 0
                            obj.Lorry_No = clsCommon.myCstr(grow.Cells("Vehicle Id").Value) 'clsDBFuncationality.getSingleValue("select vehicle_code from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(grow.Cells(1).Value) & "'", trans)
                            'obj.Route_No = clsDBFuncationality.getSingleValue("select TSPL_ROUTE_MASTER.Route_No from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(grow.Cells(1).Value) & "'", trans)
                            'obj.Transporter_Name = clsDBFuncationality.getSingleValue("Select Transporter_Name from TSPL_TRANSPORT_MASTER where Transport_Id =(Select Transport_Id from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & obj.Lorry_No & "')", trans)
                            Dim objBookingCust As clsBookingTemp = TryCast(grow.Tag, clsBookingTemp)
                            obj.Route_No = objBookingCust.RouteNo
                            obj.Transporter_Name = objBookingCust.Transporter
                            obj.Price_code = objBookingCust.PriceCode
                            obj.Freight = ""
                            obj.Freight_Amount = 0
                            obj.Comments = ""
                            obj.OnHold = "N"
                            obj.Short_Close = "N"
                            obj.Total_Amt = 0
                            obj.Arr = New List(Of clsDeliveryNoteFreshSaleDetail)
                            Dim intLineNo As Integer = 1
                            Dim dblTotal As Double = 0
                            blnRatezero = False
                            DOCreated = False
                            For ii As Integer = 15 To gv1.Columns.Count - 1
                                Dim objTr As New clsDeliveryNoteFreshSaleDetail()
                                If (clsCommon.myCdbl(grow.Cells(ii).Value)) > 0 Then

                                    objTr.Line_No = intLineNo
                                    objTr.Sampling = clsCommon.myCdbl(grow.Cells("Sampling").Value)
                                    Dim objBookingitem As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                                    objTr.Item_Code = objBookingitem.ItemCode
                                    objTr.Unit_code = objBookingitem.UnitCode
                                    objTr.Booking_No = strBookingNo
                                    objTr.Qty = clsCommon.myCdbl(grow.Cells(ii).Value)
                                    objTr.BookQty = clsCommon.myCdbl(grow.Cells(ii).Value)
                                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(ii).Value)

                                    Dim objBookingItemRate As clsBookingTemp = TryCast(grow.Cells(ii).Tag, clsBookingTemp)
                                    objTr.Rate = objBookingItemRate.ItemRate
                                    objTr.MRP = objBookingItemRate.MRP
                                    objTr.Price_Date = objBookingItemRate.Price_Date
                                    objTr.Amount = clsCommon.myCdbl(objTr.Rate * clsCommon.myCdbl(grow.Cells(ii).Value))
                                    dblTotal += objTr.Amount

                                    'End If
                                    If objTr.Rate = 0 Then
                                        blnRatezero = True
                                        DOmsg += "Please create Price chart for customer " & grow.Cells(1).Value & " for Location " & grow.Cells(3).Value & "  for item " & objTr.Item_Code & "." + Environment.NewLine
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

                                qry = "Update TSPL_BOOKING_DETAIL set DO_Qty=" & objTr.Qty & ",Booking_Qty=" & objTr.Qty & ",Total_Qty=" & dblTotal_Qty & " where item_code='" & objTr.Item_Code & "' and vehicle_code='" & obj.Lorry_No & "' and  Document_No='" & txtDocNo.Value & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "' and Sampling =" & obj.Sampling & ""
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                            Next
                            'If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                            '    'common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")

                            'End If
                            If (obj.Arr IsNot Nothing OrElse obj.Arr.Count > 0) Then
                                If blnRatezero = False Then
                                    If AllowWo_Outstanding = False Then
                                        If clsCommon.myCdbl(grow.Cells("Sampling").Value) = 0 Then
                                            If CustomerOutstandingAmount(grow.Cells(1).Value, dblTotal, trans, "") = False Then
                                                obj.CreditApproval_Reqd = "Y"
                                                obj.Status = 2
                                            End If
                                        End If
                                    End If
                                    If (obj.SaveData(obj, True, trans)) Then
                                        qry = "Update TSPL_BOOKING_DETAIL set DO_Posted=" & obj.Status & ", Delivery_No='" & obj.Document_No & "',DocumentAmount=" & obj.Total_Amt & " where Document_No='" & txtDocNo.Value & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "' and vehicle_code='" & obj.Lorry_No & "' and Sampling=" & obj.Sampling & ""
                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                        qry = "Update TSPL_BOOKING_MATSER set CreateDO_Automatic=1 where Document_No='" & txtDocNo.Value & "' "
                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                        DOCreated = True
                                        If (clsDeliveryNoteFreshSale.PostData("DEL-NOTE-FS", obj.Document_No, trans, 1)) Then
                                            'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")

                                        End If
                                    End If
                                End If
                            End If


                        ElseIf chkCreateDO.Checked = False OrElse (chkCreateDO.Checked = True AndAlso grow.Cells("DO_Posted").Value = "Approved") Then
                            If (clsDeliveryNoteFreshSale.PostData("DEL-NOTE-FS", grow.Cells("Delivery_No").Value, trans, 1)) Then
                                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                                DOCreated = True
                            End If
                        End If
                    End If
                Next
            End If
            blnSaveTotalQTy = False
            'clsCommon.MyMessageBoxShow(msg)
            Return True


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            blnSaveTotalQTy = False
            Return False
        End Try
    End Function

    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()


            Dim obj As New clsBookingEntry
            obj = clsBookingEntry.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                isInsideLoadData = True
                isLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"

                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                chkCreateDO.Checked = IIf(obj.CreateDO_Automatic, True, False)
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnCreateDO.Enabled = True
                End If
                '''''''''' FOr Grid''''''''
                LoadBlankGrid()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
            isLoadData = False
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BOOKING_MATSER where Document_No='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select * from ( select TSPL_BOOKING_MATSER.Document_No as DocumentNo,convert(varchar(12),max(TSPL_BOOKING_MATSER.Document_date),103) as Document_date" + Environment.NewLine + _
          " ,case when min(TSPL_BOOKING_DETAIL.Loc_Code)=max(TSPL_BOOKING_DETAIL.Loc_Code) then max(TSPL_BOOKING_DETAIL.Loc_Code) else max(TSPL_BOOKING_DETAIL.Loc_Code)+' *' end as Location_Code " + Environment.NewLine + _
          ",case when min(TSPL_BOOKING_DETAIL.Loc_Code)=max(TSPL_BOOKING_DETAIL.Loc_Code) then max(TSPL_LOCATION_MASTER.Location_Desc) else max(TSPL_LOCATION_MASTER.Location_Desc)+' *' end as Location " + Environment.NewLine + _
          " ,case when max(TSPL_BOOKING_MATSER.Posted)=1 then 'posted' else 'Unposted' end as Posted  " + Environment.NewLine + _
          " from TSPL_BOOKING_DETAIL" + Environment.NewLine + _
          " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BOOKING_DETAIL.Loc_Code " + Environment.NewLine + _
          " left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No " + Environment.NewLine + _
          " group by TSPL_BOOKING_MATSER.Document_No)xxx"
        Reset()
        LoadData(clsCommon.ShowSelectForm("FSBookDocNo", qry, "DocumentNo", "", txtDocNo.Value, "convert(datetime,Document_date,103) desc", isButtonClicked), NavigatorType.Current)
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
            clsCommon.MyMessageBoxShow(ex.Message)
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
                If (clsBookingEntry.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                    LoadBlankGrid()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub Export()
        If gv1.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel()
        Try
            Dim strCreatedBy As String = clsDBFuncationality.getSingleValue("Select Created_By from TSPL_BOOKING_MATSER where Document_No='" + txtDocNo.Value + "'")
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Booking No : " + txtDocNo.Value
            arrHeader.Add(strtemp)
            strtemp = "Booking Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            strtemp = "Created By : " + strCreatedBy
            arrHeader.Add(strtemp)

            clsCommon.MyExportToExcelGrid("Booking Entry", gv1, arrHeader, Me.Text)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            Post()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub Post()
        Try
            If (myMessages.postConfirm()) Then
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Dim qry = "Update TSPL_BOOKING_MATSER set Posted=1, " & _
                     "Modified_By='" + objCommonVar.CurrentUserCode + "', " & _
                     "Modified_Date='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' " & _
                     "where Document_No='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    trans.Commit()
                    Dim msg = "Successfully Posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    btnCreateDO.Enabled = True
                Else
                    Throw New Exception("No Data found to Post")
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        chkCreateDO.Checked = False
        blnSaveTotalQTy = True
        gv1.Columns(9).IsVisible = False
        gv1.Columns(10).IsVisible = False
        gv1.Columns(11).IsVisible = False
        gv1.Columns(12).IsVisible = False
        gv1.Columns(13).IsVisible = False
        For ii As Integer = 0 To gv1.Rows.Count - 1

            For jj As Integer = 15 To gv1.Columns.Count - 1
                gv1.Rows(ii).Cells("Total_Qty").Value = 0
                gv1.Rows(ii).Cells(jj).Value = 0
            Next
        Next
        isNewEntry = True
        btnSave.Text = "Save"
        txtDocNo.Value = ""
        btnSave.Enabled = True
        blnSaveTotalQTy = False
        btnDelete.Enabled = True
        btnPost.Enabled = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        btnCreateDO.Enabled = False
        blnSaveTotalQTy = False
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub btnCreateDO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateDO.Click
        '' start here
        Dim qry As String = String.Empty
        For ii As Integer = 15 To gv1.Columns.Count - 1

            Dim objBookingItem As clsBookingTemp = New clsBookingTemp()
            objBookingItem.ItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Code  from tspl_item_master left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 Where Short_Description + '  ' +  TSPL_ITEM_UOM_DETAIL.UOM_Code='" & gv1.Columns(ii).Name.ToString() & "'"))
            objBookingItem.UnitCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code  from  TSPL_ITEM_UOM_DETAIL  where Default_UOM=1 and Item_Code='" & objBookingItem.ItemCode & "'"))
            gv1.Columns(ii).Tag = objBookingItem

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & clsCommon.myCstr(grow.Cells(1).Value) & "'")
                If clsCommon.myCdbl(grow.Cells(ii).Value) > 0 Then
                    qry = "Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code from ( " & _
                              "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
                              "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
                              "Item_Basic_Price,Item_Basic_Net,Price_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
                              "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
                              "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code    where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_Date is null) and  " & _
                              "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & objBookingItem.UnitCode & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objBookingItem.ItemCode & "' AND Location_Code='" & clsCommon.myCstr(grow.Cells(3).Value) & "'  " & _
                              ") XXXE WHERE RowNo=1  "
                    Dim dt As New DataTable()
                    Dim dblRate As Double = 0
                    Dim dblTotal As Double = 0
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt.Rows.Count > 0 Then
                        dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                        Dim objBookingItemRate As clsBookingTemp = New clsBookingTemp()
                        objBookingItemRate.ItemRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                        objBookingItemRate.MRP = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Net"))
                        objBookingItemRate.Price_Date = clsCommon.myCstr(dt.Rows(0).Item("Start_Date"))
                        grow.Cells(ii).Tag = objBookingItemRate
                    Else
                        grow.Cells(ii).Tag = 0
                    End If

                End If



                Dim objBookingCust As clsBookingTemp = New clsBookingTemp()

                objBookingCust.RouteNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ROUTE_MASTER.Route_No from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & clsCommon.myCstr(grow.Cells(1).Value) & "'"))

                objBookingCust.PriceCode = Price_code
                objBookingCust.Transporter = clsDBFuncationality.getSingleValue("Select Transporter_Name from TSPL_TRANSPORT_MASTER where Transport_Id =(Select Transport_Id from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & clsCommon.myCstr(grow.Cells("Vehicle Id").Value) & "')")
                grow.Tag = objBookingCust
            Next


        Next

        '' ends here

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            If CreateDO(False, trans, txtDocNo.Value) Then

                trans.Commit()
                If clsCommon.myLen(DOmsg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(DOmsg)
                End If
                If DOCreated = True Then
                    Dim msg = "Successfully created"
                    common.clsCommon.MyMessageBoxShow(msg)
                    msg = Nothing
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub gv1_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        If Not IsDBNull(e.RowElement.RowInfo.Cells("DO_Posted").Value) Then
            If clsCommon.CompairString(e.RowElement.RowInfo.Cells("DO_Posted").Value, "Posted") = CompairStringResult.Equal Then
                'e.RowElement.Enabled = False
                'e.RowElement.DrawFill = True
                'e.RowElement.GradientStyle = GradientStyles.Solid
                'e.RowElement.ForeColor = Color.Black
                'e.RowElement.BackColor = Color.LightGreen
                'ElseIf clsCommon.CompairString(e.RowElement.RowInfo.Cells("DO_Posted").Value, "Pending") = CompairStringResult.Equal Then
                '    e.RowElement.Enabled = False
                '    e.RowElement.DrawFill = True
                '    e.RowElement.GradientStyle = GradientStyles.Solid
                '    e.RowElement.ForeColor = Color.Black
                '    e.RowElement.BackColor = Color.MistyRose
                'Else
                '    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                '    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                '    e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                '    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            End If
        End If
    End Sub

  
End Class

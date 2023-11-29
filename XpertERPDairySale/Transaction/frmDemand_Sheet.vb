Imports System.Data.SqlClient
Imports System.IO
Imports common
Public Class frmDemand_Sheet
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim SetShiftTimeOut As String = ""

    Dim isInsideLoadData As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colLineNo As String = "colLineNo"
    Const colCustCode As String = "colCustCode"
    Const colCustPhone As String = "colCustPhone"
    Const colRouteNo As String = "colRouteNo"
    Const colSetZero As String = "colSetZero"
    Const colItemCode As String = "colItemCode"

#End Region
    'Public Sub SetUserMgmtNew()
    ''MyBase.SetUserMgmt(clsUserMgtCode.frmbookingdairy)
    ' If Not (MyBase.isReadFlag) Then
    '    Throw New Exception("Permission Denied")
    '    Me.Close()
    '    Exit Sub
    'End If
    'btnSave.Visible = MyBase.isModifyFlag
    ''btnPost.Visible = MyBase.isPostFlag
    'If MyBase.isReverse Then
    '    btnreverse.Enabled = True
    'Else
    '    btnreverse.Enabled = False
    'End If
    'End Sub
    Private Sub frmDemandSheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetShiftTimeOut = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SetShiftTimeOut, clsFixedParameterCode.SetShiftTimeOut, Nothing))
        AddNew()
        ' SetUserMgmtNew()
        DemandSheetTable()
        LoadData(txtDate.Value, txtShift.Text, objCommonVar.CurrentUserCode)
        isInsideLoadData = False

    End Sub
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.AddNew()
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsPinned = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Booth Code"
        repoCustCode.Name = colCustCode
        repoCustCode.HeaderImage = My.Resources.search4
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCustCode.Width = 100
        repoCustCode.IsVisible = True
        repoCustCode.IsPinned = True
        repoCustCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoCustPhone As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustPhone.FormatString = ""
        repoCustPhone.HeaderText = "Phone No"
        repoCustPhone.Name = colCustPhone
        repoCustPhone.Width = 150
        repoCustPhone.IsVisible = True
        repoCustPhone.IsPinned = True
        repoCustPhone.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustPhone)
        Dim repoRouteNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteNo.FormatString = ""
        repoRouteNo.HeaderText = "Route No"
        repoRouteNo.Name = colRouteNo
        repoRouteNo.Width = 150
        repoRouteNo.IsVisible = True
        repoRouteNo.IsPinned = True
        repoRouteNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRouteNo)
        Dim repoSetZero As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSetZero = New GridViewDecimalColumn()
        repoSetZero.FormatString = ""
        repoSetZero.HeaderText = "Set Zero"
        repoSetZero.Name = colSetZero
        repoSetZero.Width = 50
        repoSetZero.ReadOnly = False
        repoSetZero.IsPinned = True
        repoSetZero.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSetZero)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "select * from (select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code,tspl_item_master.Short_Description ,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate')
    union
    select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq  from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate')
    union all
    select 'Ambient' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description  as ItemDescNew,2 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_Ambient=1   and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Default_UOM=1
    )z order by RowNo,Sku_Seq,Item_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim obj As ItemValueClass = New ItemValueClass()
            For Each dr As DataRow In dt.Rows
                repoIName = New GridViewTextBoxColumn()
                repoIName.FormatString = ""
                repoIName.HeaderText = clsCommon.myCstr(dr("ItemDescNew"))
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.itemDesc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                obj.IsFreshAmbient = clsCommon.myCstr(dr("FreshAmbient"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                repoIName.Tag = obj
                repoIName.Name = colItemCode + clsCommon.myCstr(i)
                repoIName.Width = 150
                repoIName.IsVisible = True
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoIName)
            Next
        End If
        gv1.BestFitColumns()
    End Sub
    Sub AddNew()
        LoadBlankGrid()
        Dim CurrDateTime As DateTime = clsCommon.GETSERVERDATE
        Dim EndTime As DateTime = clsCommon.GetPrintDate(SetShiftTimeOut, "dd/MMM/yyyy hh:mm tt")
        If CurrDateTime.TimeOfDay < EndTime.TimeOfDay Then
            txtDate.Value = clsCommon.GetPrintDate(CurrDateTime)
            txtShift.Text = "Evening"
        Else
            txtDate.Value = clsCommon.GetPrintDate(CurrDateTime.AddDays(1))
            txtShift.Text = "Morning"
        End If
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.Name = colCustCode Then
                        gv1.CurrentRow.Cells(colCustCode).Value = clsDistributorRouteTagging.getFinder(" IsDistributor='N' and form_type not in('TPT','VSP') ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), False)
                        gv1.CurrentRow.Cells(colCustPhone).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) + "'"))
                        gv1.CurrentRow.Cells(colRouteNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) + "'"))
                        gv1.CurrentRow.Cells(colSetZero).Value = 1

                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    End If
                    If e.Column.Name = colSetZero Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    End If
                    If e.Column.Index >= 4 AndAlso gv1.Rows.Count > 0 Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateColumnTotal()
                    End If
                    isCellValueChangedOpen = False
                End If
                isInsideLoadData = False
            End If
            'SetGridFocus()
        Catch ex As Exception
            isInsideLoadData = False
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        If gv1.Rows(IntRowNo).Cells(colSetZero).Value = 0 Then
            For dbColumn As Integer = 4 To gv1.Columns.Count - 1
                gv1.Rows(IntRowNo).Cells(dbColumn).Value = "0"
            Next
        Else
            If gv1.Rows(IntRowNo).Cells(colCustCode).Value <> "" Then
                Dim obj As New clsDemandSheet()
                obj.DEMAND_Date = clsCommon.GetPrintDate(txtDate.Value)
                obj.Cust_Code = gv1.Rows(IntRowNo).Cells(colCustCode).Value
                obj.Route_No = gv1.Rows(IntRowNo).Cells(colRouteNo).Value
                obj.Set_Zero = gv1.Rows(IntRowNo).Cells(colSetZero).Value
                obj.ShiftType = txtShift.Text
                Dim k As Integer = 1
                For dblcolumns As Integer = 5 To gv1.Columns.Count - 1
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                    k = k + 1
                    If obj1 IsNot Nothing Then
                        If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 Then  'AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(dblcolumns).Value) > 0
                            obj.Item_Code = clsCommon.myCstr(obj1.itemCode)

                            If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSetZero).Value) = 0 Then
                                obj.Qty = 0
                                Try
                                    Dim status As Boolean = obj.SaveData(obj)
                                Catch ex As Exception
                                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                                End Try
                            ElseIf clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(dblcolumns).Value) > 0 Then
                                obj.Qty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(dblcolumns).Value)
                                Try
                                    Dim status As Boolean = obj.SaveData(obj)
                                Catch ex As Exception
                                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                                End Try
                            End If

                        End If
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub UpdateAllTotals()
        'UpdateColumnTotal()
    End Sub
    Private Sub UpdateColumnTotal()
        Try
            'Dim summaryRowItem As New GridViewSummaryRowItem()

            ' gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.Rows.Count > 0 Then
            If gv1.CurrentRow.Index = gv1.Rows.Count - 1 Then
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 2)
            End If
        End If
    End Sub
    Public Sub DemandSheetTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("DEMAND_Date", "datetime Not null")
        coll.Add("ShiftType", "VARCHAR(200)")
        coll.Add("Cust_Code", "varchar(12) null references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Route_No", "varchar(12) NULL REFERENCES TSPL_ROUTE_MASTER (Route_No)")
        coll.Add("Set_Zero", "integer NOT NULL")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Qty", "Decimal (18,2) NULL")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "datetime  Not NULL")
        coll.Add("Modify_By", "varchar(12)  Not NULL")
        coll.Add("Modify_Date", "datetime  Not NULL")
        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_DEMAND_SHEET", coll, "", True)
    End Sub
    Public Sub LoadData(ByVal CurrDate As Date, ByVal Shift As String, ByVal CurrUser As String)
        Try
            isInsideLoadData = True
            Dim IntRowNo As Decimal = 0
            Dim lstobj As List(Of clsDemandSheet)
            lstobj = clsDemandSheet.GetData(CurrDate, Shift, objCommonVar.CurrentUserCode, Nothing)
            If (lstobj IsNot Nothing AndAlso lstobj.Count > 0) Then
                For Each obj As clsDemandSheet In lstobj
                    gv1.Rows(IntRowNo).Cells(colLineNo).Value = IntRowNo + 1
                    gv1.Rows(IntRowNo).Cells(colCustCode).Value = obj.Cust_Code
                    gv1.Rows(IntRowNo).Cells(colCustPhone).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Cust_Code + "'"))
                    'gv1.Rows(IntRowNo).Cells(colSetZero).Value = obj.Set_Zero

                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        For Each objTr As clsDemandSheetDetails In obj.Arr
                            gv1.Rows(IntRowNo).Cells(colRouteNo).Value = objTr.Route_No
                            gv1.Rows(IntRowNo).Cells(colSetZero).Value = objTr.Set_Zero
                            'For dblrows As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colCustCode).Value), objTr.Cust_Code) = CompairStringResult.Equal Then
                                Dim k As Integer = 1
                                For columns = 5 To gv1.Columns.Count - 1
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                    k = k + 1
                                    If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal Then
                                        gv1.Rows(IntRowNo).Cells(columns).Value = objTr.Qty
                                    End If
                                Next
                            End If
                            ' Next
                        Next
                    End If
                    IntRowNo += 1
                    gv1.Rows.AddNew()
                Next
            End If
            GvRowFridge()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For colcount As Integer = 5 To gv1.Columns.Count - 1
                gv1.Columns(colItemCode + clsCommon.myCstr(colcount - 4)).FormatString = "{0:n2}"
            Next
            For colcount As Integer = 5 To gv1.Columns.Count - 1
                Dim TotalCount As New GridViewSummaryItem(colItemCode + clsCommon.myCstr(colcount - 4), "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalCount)
            Next
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.AutoSizeRows = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData()
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Sub SaveData()
        Try
            For Each grow As GridViewRowInfo In gv1.Rows
                If grow.Cells(colCustCode).Value <> "" Then


                    Dim RouteNo As String = clsCommon.myCstr(grow.Cells(colRouteNo).Value)
                    Dim strQry As String = "select TSPL_DEMAND_BOOKING_MASTER.Document_No from TSPL_DEMAND_BOOKING_MASTER
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
where TSPL_DEMAND_BOOKING_MASTER.Document_Date>='" + clsCommon.GetPrintDate(txtDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(txtDate.Value) + "'
and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + RouteNo + "' and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code='" + grow.Cells(colCustCode).Value + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" + txtShift.Text + "'"
                    Dim DocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry))
                    If clsCommon.myLen(DocumentNo) > 0 Then
                        Dim obj As New clsDemandBookingSale
                        obj = clsDemandBookingSale.GetData(DocumentNo, NavigatorType.Current, Nothing)
                        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                                For Each objTr As clsDemandBookingSaleDetail In obj.Arr
                                    If clsCommon.CompairString(grow.Cells(colCustCode).Value, objTr.Cust_Code) = CompairStringResult.Equal Then
                                        Dim k As Integer = 1
                                        For columns = 5 To gv1.Columns.Count - 1
                                            Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                            k = k + 1
                                            If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal Then
                                                objTr.Qty = clsCommon.myCdbl(grow.Cells(columns).Value)
                                                obj.SaveData(obj, False)
                                                'ElseIf clsCommon.myCdbl(grow.Cells(columns).Value) > 0 Then
                                                '    Dim obj2 As New clsDemandBookingSale
                                                '    obj2.Document_No = obj.Document_No
                                                '    obj2.Document_Date = obj.Document_Date
                                                '    obj2.Arr = New List(Of clsDemandBookingSaleDetail)
                                                '    Dim objTr1 As New clsDemandBookingSaleDetail()
                                                '    objTr1.Item_Code = obj1.itemCode
                                                '    objTr1.Unit_code = clsCommon.myCstr(obj1.Unit_code)
                                                '    objTr1.Cust_Code = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                                                '    objTr1.ShiftType = clsCommon.myCstr(txtShift.Text)
                                                '    objTr1.Qty = grow.Cells(columns).Value
                                                '    obj2.Arr.Add(objTr1)
                                                '    clsDemandBookingSaleDetail.SaveData(obj2.Document_No, obj2.Document_Date, obj2.Arr, Nothing, obj.Location_Code, obj.ShiftType, False)
                                                'ElseIf clsCommon.myCdbl(grow.Cells(colSetZero).Value) = 1 Then
                                                '    objTr.Qty = clsCommon.myCdbl(grow.Cells(columns).Value)
                                                '    obj.SaveData(obj, False)
                                            End If
                                        Next
                                    End If
                                    'obj.SaveData(obj, False)
                                Next
                            End If
                        End If
                        '                Else
                        '                    Dim obj1 As New clsDemandBookingSale()
                        '                    Dim VehicleNo As String = ""
                        '                    Dim TranspoterName As String = ""
                        '                    obj1.ShiftType = txtShift.Text
                        '                    obj1.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
                        '                    Dim qry As String = "select TSPL_ROUTE_MASTER.City_Code,Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon,tspl_transport_master.Transporter_Name from TSPL_CUSTOMER_MASTER left outer join 
                        '            TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on 
                        '            TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join tspl_transport_master on tspl_transport_master.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id 
                        'where TSPL_ROUTE_MASTER.Route_No ='" + RouteNo + "'"
                        '                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                        '                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        '                        VehicleNo = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                        '                        TranspoterName = clsCommon.myCstr(dt1.Rows(0)("Transporter_Name"))
                        '                        obj1.City_Code = clsCommon.myCstr(dt1.Rows(0)("City_Code"))
                        '                    End If
                        '                    obj1.Document_Date = txtDate.Value
                        '                    obj1.Route_No = RouteNo
                        '                    obj1.ItemType = "Both"
                        '                    obj1.Arr = New List(Of clsDemandBookingSaleDetail)
                        '                    Dim k As Integer = 1
                        '                    For columns = 4 To gv1.Columns.Count - 1
                        '                        Dim objTr As New clsDemandBookingSaleDetail()
                        '                        objTr.Cust_Code = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                        '                        objTr.ShiftType = clsCommon.myCstr(txtShift.Text)
                        '                        objTr.Vehicle_Code = VehicleNo


                        '                        Dim objItemValue As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                        '                        k = k + 1
                        '                        'objTr.Line_No = k - 1
                        '                        objTr.Item_Code = clsCommon.myCstr(objItemValue.itemCode)
                        '                        objTr.Unit_code = clsCommon.myCstr(objItemValue.Unit_code)
                        '                        If clsCommon.myCdbl(grow.Cells(colSetZero).Value) = 0 Then
                        '                            objTr.Qty = 0
                        '                            obj1.Arr.Add(objTr)
                        '                        ElseIf clsCommon.myCdbl(grow.Cells(columns).Value) > 0 Then
                        '                            objTr.Qty = clsCommon.myCstr(grow.Cells(columns).Value)
                        '                            obj1.Arr.Add(objTr)
                        '                        End If

                        '                    Next
                        '                    obj1.SaveData(obj1, True)

                    End If
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub SetGridFocus()
        If gv1.CurrentCell IsNot Nothing Then
            Dim setNxtRow As Boolean = False
            Dim k As Integer = gv1.CurrentColumn.Index
            Dim r As Integer = 0
            If gv1.CurrentCell.ColumnInfo.Name = colCustCode Then
                gv1.CurrentColumn = gv1.Columns(colItemCode + clsCommon.myCstr(1))
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colCustPhone Then
                gv1.CurrentColumn = gv1.Columns(colItemCode + clsCommon.myCstr(1))
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colSetZero Then
                gv1.CurrentColumn = gv1.Columns(colItemCode + clsCommon.myCstr(1))
            End If
            If k >= 5 Then
                Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k - 4)).Tag, ItemValueClass)
                k = k - 3
                r = k
                If obj1 IsNot Nothing Then
                    If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso r <= gv1.Columns.Count - 5 Then  'AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(dblcolumns).Value) > 0
                        If gv1.CurrentCell.ColumnInfo.Name = colItemCode + clsCommon.myCstr(k - 1) Then
                            Dim obj2 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(r)).Tag, ItemValueClass)
                            If obj2 IsNot Nothing Then
                                gv1.CurrentColumn = gv1.Columns(colItemCode + clsCommon.myCstr(r))
                                Exit Sub
                            Else
                                setNxtRow = True
                            End If
                        End If
                    Else
                        setNxtRow = True
                    End If
                End If
                If setNxtRow Then
                    If gv1.Rows.Count > gv1.CurrentRow.Index + 1 Then
                        gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
                    End If
                    gv1.CurrentColumn = gv1.Columns(colCustCode)
                    GvRowFridge()
                End If
            End If
        End If
    End Sub

    Private Sub frmDemand_Sheet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                SetGridFocus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub GvRowFridge()
        Try
            If gv1.Rows.Count > 2 Then
                For rowcount As Integer = 0 To gv1.Rows.Count - 3
                    For colcount As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Rows(rowcount).Cells(colcount).ReadOnly = True
                    Next
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


End Class
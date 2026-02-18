Imports System.Data.SqlClient
Imports common

Public Class FrmProductQuickDemandBooking
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim UOMCrate As String = ""
    Dim isLastcellvaluechanged As Boolean = False

    Dim isInsideLoadData As Boolean = False
    Dim allowtoselectShift As Boolean = False
    Dim ConvertPouchtoCrate As Boolean = False
    Dim DontCreateForPouch As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colLineNo As String = "colLineNo"
    Const colCustCode As String = "colCustCode"
    Const colCustPhone As String = "colCustPhone"
    Const colRouteNo As String = "colRouteNo"
    Const colSetZero As String = "colSetZero"
    Const colItemCode As String = "colItemCode"
    Const colCrate As String = "colCrate"
#End Region
    Public Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.frmProductQuickDemand)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnExport.Visible = MyBase.isExport
        ''btnPost.Visible = MyBase.isPostFlag
        'If MyBase.isExport = True Then
        '    rmiImport.Enabled = True
        '    rmiExcel.Enabled = True
        'Else
        '    rmiImport.Enabled = False
        '    rmiExcel.Enabled = False
        'End If
    End Sub

    Private Sub FrmProductQuickDemandBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        CreateTable()
        AddNew()
        rbtnProduct.IsChecked = True
    End Sub
    Sub LoadBlankGrid(ByVal strType As String)
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsPinned = True
        'repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        'repoSetZero.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoSetZero)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "select * from (Select 'Ambient' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description  as ItemDescNew,2 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_Ambient=1 and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 and TypeOfItm='" & strType & "' "
        qry += " )z order by RowNo,Sku_Seq,Item_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim obj As ItemValueClass = New ItemValueClass()
            For Each dr As DataRow In dt.Rows
                repoIName = New GridViewTextBoxColumn()
                repoIName.FormatString = ""
                repoIName.HeaderText = clsCommon.myCstr(dr("UOM_Code"))
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
        gv1.Rows.AddNew()
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        View()
    End Sub
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup("Booth"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colLineNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustCode).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustPhone).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colRouteNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colSetZero).Name)
                view.ColumnGroups(0).IsPinned = True
                Dim TempColGroupCount As Integer = 1
                For dblcolumns As Integer = 5 To gv1.Columns.Count - 1
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                    If obj1 IsNot Nothing Then
                        view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(obj1.ShortDesc)))
                        view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                        view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                        TempColGroupCount += 1
                    End If
                Next
                view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
                view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(TempColGroupCount).IsPinned = True
                view.ColumnGroups(TempColGroupCount).PinPosition = PinnedColumnPosition.Right
                'MergeHorizontally(gv1, 0, gv1.Rows.Count - 1)
                gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub AddNew()
        txtBoothName.Text = ""
        txtDistributor.Text = ""
        txtAddress.Text = ""
    End Sub

    Private Sub rbtnProduct_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnProduct.ToggleStateChanged
        If rbtnProduct.IsChecked Then
            LoadBlankGrid("P")
            AddNew()
        End If
    End Sub

    Private Sub rbtnIceCream_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnIceCream.ToggleStateChanged
        If rbtnIceCream.IsChecked Then
            LoadBlankGrid("I")
            AddNew()
        End If
    End Sub

    Private Sub GenerateLineNo()
        Dim count As Integer = 1
        For Each grow As GridViewRowInfo In gv1.Rows
            grow.Cells(colLineNo).Value = count
            count += 1
        Next
    End Sub
    Public Sub GetBoothDetail()
        Try
            If clsCommon.myLen(gv1.CurrentRow.Cells(colCustCode)) > 0 Then
                txtBoothName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) + "'"))
                txtDistributor.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code =(select Distributor_Code from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) + "')"))
                txtAddress.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Add1+Add2+Add3 from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) + "'"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Sub SetGridFocus(ByVal isSave As Boolean)
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
            ElseIf k >= 5 AndAlso k < gv1.Columns.Count - 1 Then
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(gv1.CurrentRow.Index).Cells(colCustCode).Value)) > 0 Then
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k - 4)).Tag, ItemValueClass)
                    k = k - 3
                    r = k
                    If obj1 IsNot Nothing Then
                        If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso r <= gv1.Columns.Count - 5 Then
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
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select Booth")
                    LoadBlankGrid(IIf(rbtnProduct.IsChecked, "P", "I"))
                End If
            Else
                If isSave OrElse Not isLastcellvaluechanged Then
                    If clsCommon.myLen(gv1.Rows(0).Cells(colCustCode).Value) > 0 Then
                        SetDemandBooking(clsCommon.myCstr(gv1.Rows(0).Cells(colCustCode).Value), clsCommon.GETSERVERDATE, 0)
                    End If
                    gv1.CurrentColumn = gv1.Columns(colCustCode)

                End If

            End If
        End If
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.Name = colCustCode Then
                        GenerateLineNo()
                        Dim qry As String = "select Cust_Code,Status from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) & "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso clsCommon.CompairString(dt.Rows(0)("Status"), "N") <> CompairStringResult.Equal Then
                            Dim cust_code As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value)
                            LoadBlankGrid(IIf(rbtnProduct.IsChecked, "P", "I"))
                            Throw New Exception("Inactive Customer [ " & cust_code & " ]")
                        End If
                        gv1.CurrentRow.Cells(colCustCode).Value = clsDistributorRouteTagging.getFinder(" Status='N' and IsDistributor='N' and form_type not in('TPT','VSP') ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), False)
                        gv1.CurrentRow.Cells(colCustPhone).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) & "'"))
                        If rbtnProduct.IsChecked Then
                            qry = "select P_Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) & "'"
                        ElseIf rbtnIceCream.IsChecked Then
                            qry = "select I_Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value) & "'"
                        End If
                        gv1.CurrentRow.Cells(colRouteNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        gv1.CurrentRow.Cells(colSetZero).Value = 1
                        GetBoothDetail()
                        qry = "select top 1 TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No from TSPL_PRODUCT_DEMAND_BOOKING_MASTER 
where TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Route_No='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colRouteNo).Value) & "' and TSPL_PRODUCT_DEMAND_BOOKING_MASTER.posted=0 and TSPL_PRODUCT_DEMAND_BOOKING_MASTER.IsIndividualCustomer=0"
                        Dim isDemandPosted As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        Dim Route_no As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRouteNo).Value)
                        If clsCommon.myLen(isDemandPosted) <= 0 Then
                            LoadBlankGrid(IIf(rbtnProduct.IsChecked, "P", "I"))
                            Throw New Exception("Demand Already Posted/Not Created for Route_No Code [" + clsCommon.myCstr(Route_no) + "]")
                        End If
                        FindDemand(gv1.CurrentRow.Cells(colCustCode).Value, gv1.CurrentRow.Cells(colRouteNo).Value)

                    End If
                    If e.Column.Name = colSetZero Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    End If
                    If e.Column.Index >= 5 Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        SetGridFocus(True)
                    End If

                    isCellValueChangedOpen = False
                End If
                isInsideLoadData = False
            End If
            'SetGridFocus()
        Catch ex As Exception
            isInsideLoadData = False
            isCellValueChangedOpen = False
            'LoadBlankGrid()
            SetGridFocus(False)
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub FindDemand(ByVal CustCode As String, ByVal routeNo As String)
        Try
            Dim strQry As String = String.Empty
            If clsCommon.myLen(CustCode) > 0 Then
                Dim ExistsCust As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(PK_ID) from TSPL_PRODUCT_DEMAND_SHEET where convert(date,DEMAND_Date,103)='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' and Route_No='" & routeNo & "' and Cust_Code='" & CustCode & "'and Created_By='" & objCommonVar.CurrentUserCode & "'"))
                If ExistsCust = 0 Then
                    Dim document As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_NO from TSPL_Product_DEMAND_BOOKING_MASTER where Route_No='" & clsCommon.myCstr(routeNo) & "' and IsIndividualCustomer=0 and Posted=0"))
                    If clsCommon.myLen(document) > 0 Then
                        strQry = " select Item_Code,Unit_Code,Qty from TSPL_Product_DEMAND_BOOKING_DETAIL where Document_No='" & document & "' and Cust_Code='" & CustCode & "' "
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim obj As New clsProductDemandSheet()
                            For Each dr As DataRow In dt.Rows
                                obj.DEMAND_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
                                obj.Cust_Code = clsCommon.myCstr(CustCode)
                                obj.Route_No = clsCommon.myCstr(routeNo)
                                obj.Set_Zero = clsCommon.myCdbl(1)
                                obj.Item_Code = clsCommon.myCstr(dr.Item("Item_Code"))
                                obj.Unit_Code = clsCommon.myCstr(dr.Item("Unit_Code"))
                                obj.Qty = clsCommon.myCdbl(dr.Item("qty"))
                                Dim k As Integer = 1
                                For dblcolumns As Integer = 5 To gv1.Columns.Count - 2
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                    k = k + 1
                                    If obj1 IsNot Nothing Then
                                        If clsCommon.CompairString(obj.Item_Code, obj1.itemCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Unit_Code, obj1.Unit_code) = CompairStringResult.Equal Then
                                            If obj.Qty > 0 Then
                                                gv1.CurrentRow.Cells(dblcolumns).Value = obj.Qty
                                            End If
                                        End If
                                    End If
                                Next
                                Try
                                    obj.SaveData(obj)
                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            Next
                        Else
                            Dim k As Integer = 1
                            For dblcolumns As Integer = 5 To gv1.Columns.Count - 2
                                Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                k = k + 1
                                If obj1 IsNot Nothing Then
                                    gv1.CurrentRow.Cells(dblcolumns).Value = 0
                                End If
                            Next

                        End If
                    Else
                        Dim k As Integer = 1
                        For dblcolumns As Integer = 5 To gv1.Columns.Count - 2
                            Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                            k = k + 1
                            If obj1 IsNot Nothing Then
                                gv1.CurrentRow.Cells(dblcolumns).Value = 0
                            End If
                        Next
                    End If
                Else

                    strQry = "select Item_Code,Unit_Code,Qty from TSPL_Product_DEMAND_SHEET where convert(date,DEMAND_Date,103)='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' and Cust_Code='" & CustCode & "'and Created_By='" & objCommonVar.CurrentUserCode & "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim k As Integer = 1
                            For dblcolumns As Integer = 5 To gv1.Columns.Count - 2
                                Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                k = k + 1
                                If obj1 IsNot Nothing Then
                                    If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Item_Code")), obj1.itemCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dr.Item("Unit_Code")), obj1.Unit_code) = CompairStringResult.Equal Then
                                        gv1.CurrentRow.Cells(dblcolumns).Value = clsCommon.myCdbl(dr.Item("qty"))
                                    End If
                                End If
                            Next
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("DEMAND_Date", "datetime Not null")
        coll.Add("Cust_Code", "varchar(12) null references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Route_No", "varchar(12) NULL REFERENCES TSPL_ROUTE_MASTER (Route_No)")
        coll.Add("Set_Zero", "integer NOT NULL")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Unit_Code", "Varchar(12) null")
        coll.Add("Qty", "Decimal (18,2) NULL")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "datetime  Not NULL")
        coll.Add("Modify_By", "varchar(12)  Not NULL")
        coll.Add("Modify_Date", "datetime  Not NULL")
        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_Product_DEMAND_SHEET", coll, "", True)
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim obj As New clsProductDemandSheet()
            obj.DEMAND_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
            obj.Cust_Code = gv1.Rows(IntRowNo).Cells(colCustCode).Value
            obj.Route_No = gv1.Rows(IntRowNo).Cells(colRouteNo).Value
            obj.Set_Zero = gv1.Rows(IntRowNo).Cells(colSetZero).Value
            If gv1.Rows(IntRowNo).Cells(colSetZero).Value = 0 Then
                For dbColumn As Integer = 4 To gv1.Columns.Count - 2
                    gv1.Rows(IntRowNo).Cells(dbColumn).Value = "0"
                    Dim k As Integer = 1
                    For dblcolumns As Integer = 5 To gv1.Columns.Count - 2
                        Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                        k = k + 1
                        If obj1 IsNot Nothing Then
                            If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 Then
                                obj.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                obj.Unit_Code = clsCommon.myCstr(obj1.Unit_code)
                                obj.Qty = 0
                                Try
                                    If clsCommon.myLen(obj.Cust_Code) > 0 Then
                                        obj.SaveData(obj)
                                    End If
                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            End If
                        End If
                    Next
                Next
            Else
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colCustCode).Value)) > 0 Then
                    Dim k As Integer = 1
                    Dim ccount As Integer = 0
                    For dblcolumns As Integer = 5 To gv1.Columns.Count - 2
                        Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                        k = k + 1
                        If obj1 IsNot Nothing Then
                            If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 Then
                                obj.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                Dim cellValue As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(dblcolumns).Value)

                                Dim CrateConvFactor As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' "))
                                Dim ItemConvFactor As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                If CrateConvFactor > 0 AndAlso ItemConvFactor > 0 Then
                                    Dim DispatchQty As Decimal = clsCommon.myCDecimal(gv1.Rows(IntRowNo).Cells(dblcolumns).Value) * ItemConvFactor
                                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(AllowEntryInDecimal) from TSPL_ITEM_MASTER where Item_Code='" & obj1.itemCode & "' and  AllowEntryInDecimal=1")) = 0 Then
                                        If cellValue.Contains(".") OrElse cellValue.Contains(",") Then
                                            gv1.Rows(IntRowNo).Cells(dblcolumns).Value = ""
                                            Throw New Exception("Decimal values are not allowed.")
                                        End If
                                    Else
                                        Dim ItemCFofDecimalUom As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.Decimal_UOM ='1'"))
                                        If DispatchQty Mod ItemCFofDecimalUom <> 0 Then
                                            gv1.Rows(IntRowNo).Cells(dblcolumns).Value = ""
                                            Throw New Exception("Please Enter Valid Qty for " + obj1.ShortDesc)
                                        End If
                                    End If
                                End If
                                obj.Qty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(dblcolumns).Value)
                                obj.Unit_Code = clsCommon.myCstr(obj1.Unit_code)
                                Try
                                    obj.SaveData(obj)
                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                                ccount += obj.Qty
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Public Sub SetDemandBooking(ByVal strCustCode As String, ByVal DemandData As DateTime, ByVal intRow As Integer)
        Try
            Dim qry As String = String.Empty
            If rbtnProduct.IsChecked Then
                qry = "select P_Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(strCustCode) & "'"
            ElseIf rbtnIceCream.IsChecked Then
                qry = "select I_Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(strCustCode) & "'"
            End If
            Dim RouteNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(RouteNo) > 0 Then
                qry = "Select Document_No from TSPL_Product_DEMAND_BOOKING_MASTER where Route_No = '" & RouteNo & "' and IsIndividualCustomer=0 and Posted=0"
            End If

            Dim DocumentNO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, DocumentNO, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", "Document_No", "TSPL_Product_DEMAND_BOOKING_DETAIL", "Document_No", Nothing)
            ' clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, DocumentNO, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", "Document_No", "TSPL_Product_DEMAND_BOOKING_DETAIL", "Document_No", Nothing)

            Dim lineNo As Integer = 1
            If clsCommon.myLen(DocumentNO) > 0 Then
                Dim location_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Product_DEMAND_BOOKING_MASTER where Document_No='" & DocumentNO & "'"))
                qry = "select * from TSPL_PRODUCT_DEMAND_SHEET where Cust_Code='" & strCustCode & "' and convert(date,demand_date,103)='" & clsCommon.GetPrintDate(DemandData, "dd/MMM/yyyy") & "' and Created_By='" & objCommonVar.CurrentUserCode & "'"
                Dim dtDS As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtDS IsNot Nothing AndAlso dtDS.Rows.Count > 0 Then
                    Dim DObj As New List(Of clsProductDemandBookingSaleDetail)
                    For Each dr As DataRow In dtDS.Rows
                        Dim objDBD As New clsProductDemandBookingSaleDetail
                        objDBD.Line_No = lineNo
                        objDBD.Document_No = DocumentNO
                        objDBD.Cust_Code = strCustCode
                        objDBD.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objDBD.Item_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'"))
                        objDBD.Unit_code = clsCommon.myCstr(dr("Unit_Code"))
                        objDBD.Qty = clsCommon.myCdbl(dr("Qty"))
                        objDBD.Price_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustCode + "'"))
                        objDBD.Vehicle_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vehicle_No from TSPL_VEHICLE_MASTER left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_ROUTE_MASTER.Route_No='" + RouteNo + "'"))

                        qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, XXXE.Tax_group,XXXE.TAX1_Rate, " &
                        " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                        "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                        " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                        " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                        " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                        " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt, XXXE.TAX5_Amt,XXXE.TAX6_Amt,XXXE.TAX7_Amt,XXXE.TAX8_Amt,XXXE.TAX9_Amt,XXXE.TAX10_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                        "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                        "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                        "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, TSPL_ITEM_PRICE_MASTER.Tax_group,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                        "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                        " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                        " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                        " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                        " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.TAX5_Amt,TSPL_ITEM_PRICE_MASTER.TAX6_Amt,TSPL_ITEM_PRICE_MASTER.TAX7_Amt,   TSPL_ITEM_PRICE_MASTER.TAX8_Amt,TSPL_ITEM_PRICE_MASTER.TAX9_Amt,TSPL_ITEM_PRICE_MASTER.TAX10_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                        "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                        "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  2=( case when CONVERT(date,Start_Date,103)='" + clsCommon.GetPrintDate(DemandData) + "' and Shift_Type='Morning' then 2 else ( case when CONVERT(date,Start_Date,103)<='" + clsCommon.GetPrintDate(DemandData.AddDays(-1)) + "' then 2 else 3 end)  end)  and (End_Date >= '" & clsCommon.GetPrintDate(DemandData, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                        "TSPL_ITEM_PRICE_MASTER.Price_Code='" & objDBD.Price_Code & "' and UOM='" & objDBD.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objDBD.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(location_Code) & "'" &
                        ") XXXE WHERE RowNo=1  "
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt2.Rows.Count > 0 Then
                            Dim dblRate As Decimal = clsCommon.myCDecimal(dt2.Rows(0).Item("Item_Basic_Price"))
                            If dblRate = 0 Then
                                Throw New Exception("Please Fill Selling Price for Location " & location_Code & "  for item " & clsCommon.myCstr(objDBD.Item_Desc) & Environment.NewLine)
                            End If
                            objDBD.Rate = dblRate
                            objDBD.ItemNetAmount = Math.Round(clsCommon.myCdbl(objDBD.Qty) * clsCommon.myCdbl(dblRate), 2)
                            objDBD.TAX_Group = clsCommon.myCstr(dt2.Rows(0).Item("TAX_Group"))
                            objDBD.TAX1 = clsCommon.myCstr(dt2.Rows(0).Item("TAX1"))
                            objDBD.TAX1_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX1_Rate"))
                            objDBD.TAX1_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX1_Rate / 100), 2)
                            objDBD.TAX1_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX2 = clsCommon.myCstr(dt2.Rows(0).Item("TAX2"))
                            objDBD.TAX2_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX2_Rate"))
                            objDBD.TAX2_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX2_Rate / 100), 2)
                            objDBD.TAX2_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX3 = clsCommon.myCstr(dt2.Rows(0).Item("TAX3"))
                            objDBD.TAX3_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX3_Rate"))
                            objDBD.TAX3_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX3_Rate / 100), 2)
                            objDBD.TAX3_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX4 = clsCommon.myCstr(dt2.Rows(0).Item("TAX4"))
                            objDBD.TAX4_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX4_Rate"))
                            objDBD.TAX4_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX4_Rate / 100), 2)
                            objDBD.TAX4_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX5 = clsCommon.myCstr(dt2.Rows(0).Item("TAX5"))
                            objDBD.TAX5_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX5_Rate"))
                            objDBD.TAX5_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX5_Rate / 100), 2)
                            objDBD.TAX5_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX6 = clsCommon.myCstr(dt2.Rows(0).Item("TAX6"))
                            objDBD.TAX6_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX6_Rate"))
                            objDBD.TAX6_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX6_Rate / 100), 2)
                            objDBD.TAX6_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX7 = clsCommon.myCstr(dt2.Rows(0).Item("TAX7"))
                            objDBD.TAX7_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX7_Rate"))
                            objDBD.TAX7_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX7_Rate / 100), 2)
                            objDBD.TAX7_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX8 = clsCommon.myCstr(dt2.Rows(0).Item("TAX8"))
                            objDBD.TAX8_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX8_Rate"))
                            objDBD.TAX8_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX8_Rate / 100), 2)
                            objDBD.TAX8_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX9 = clsCommon.myCstr(dt2.Rows(0).Item("TAX9"))
                            objDBD.TAX9_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX9_Rate"))
                            objDBD.TAX9_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX9_Rate / 100), 2)
                            objDBD.TAX9_Base_Amt = objDBD.ItemNetAmount
                            objDBD.TAX10 = clsCommon.myCstr(dt2.Rows(0).Item("TAX10"))
                            objDBD.TAX10_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX10_Rate"))
                            objDBD.TAX10_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX10_Rate / 100), 2)
                            objDBD.TAX10_Base_Amt = objDBD.ItemNetAmount
                            DObj.Add(objDBD)
                        End If
                        lineNo += 1
                    Next
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        Dim obj As clsProductDemandBookingSale = clsProductDemandBookingSale.GetData(DocumentNO, NavigatorType.Current, trans)
                        qry = "delete from TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where tr_code in (select tr_code from TSPL_PRODUCT_DEMAND_BOOKING_DETAIL where Document_No='" & obj.Document_No & "' and Cust_Code='" & strCustCode & "') "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        clsProductDemandBookingSaleDetail.SaveData(DocumentNO, DemandData, DObj, trans, location_Code, True, RouteNo)
                        'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, DocumentNO, "TSPL_PRODUCT_DEMAND_BOOKING_MASTER", "Document_No", "TSPL_Product_DEMAND_BOOKING_DETAIL", "Document_No", trans)
                        trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
                        LoadBlankGrid(IIf(rbtnProduct.IsChecked, "P", "I"))
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If gv1.Rows.Count > 0 Then
                If clsCommon.myLen(gv1.Rows(0).Cells(colCustCode).Value) > 0 Then
                    SetDemandBooking(clsCommon.myCstr(gv1.Rows(0).Cells(colCustCode).Value), clsCommon.GETSERVERDATE, 0)
                    LoadBlankGrid(IIf(rbtnProduct.IsChecked, "P", "I"))
                Else
                    Throw New Exception("Please fill data at line no 1.")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
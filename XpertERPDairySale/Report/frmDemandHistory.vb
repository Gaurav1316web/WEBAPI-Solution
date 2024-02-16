Imports common
Public Class frmDemandHistory
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim buttontooltip As ToolTip = New ToolTip()
    Const colSLNo As String = "COLLNO"
    Const colHistVer As String = "colHistVer"
    Const colDemandNo As String = "colDemandNo"
    Const colDocumentNo As String = "colDocumentNo"
    Const colRouteNo As String = "colRouteNo"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colShiftName As String = "colShiftName"
    Const colItemCode As String = "colItemCode"
    Const colAmt As String = "colAmt"
    Const colHistBy As String = "colHistBy"
    Const colHistOn As String = "colHistOn"
    Const ReportID As String = "DemandHistoryGrid"

#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPurchaseHistory)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub frmDemandHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        buttontooltip.SetToolTip(btnGo, "Press Alt+R for Summary ")
        buttontooltip.SetToolTip(btnreset, "Press Alt+E for Reset ")
        buttontooltip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub frmDemandHistory_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnGo.Enabled Then
            'fillGridReport(txtDate.Value, cmbShift.Text, txtBooth.Value)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnreset.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub Reset()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtBooth.Value = ""
        cmbShift.SelectedIndex = 0
        txtBoothDesc.Text = ""
        gv1.DataSource = Nothing
        gv1.Visible = False
    End Sub

    Private Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.AddNew()

        Dim repoSLNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSLNo.FormatString = ""
        repoSLNo.HeaderText = "SNo"
        repoSLNo.Name = colSLNo
        repoSLNo.Width = 50
        repoSLNo.ReadOnly = True
        repoSLNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSLNo)

        Dim repoHistVer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHistVer.FormatString = ""
        repoHistVer.HeaderText = "History Version"
        repoHistVer.Name = colHistVer
        repoHistVer.Width = 45
        repoHistVer.ReadOnly = True
        repoHistVer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoHistVer)

        Dim repoDemandNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDemandNo.FormatString = ""
        repoDemandNo.HeaderText = "Demand No"
        repoDemandNo.Name = colDemandNo
        repoDemandNo.Width = 50
        repoDemandNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDemandNo)

        Dim repoDocumentNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocumentNo.FormatString = ""
        repoDocumentNo.HeaderText = "Document No"
        repoDocumentNo.Name = colDocumentNo
        repoDocumentNo.Width = 50
        repoDocumentNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDocumentNo)

        Dim repoRouteNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteNo.FormatString = ""
        repoRouteNo.HeaderText = "Route No"
        repoRouteNo.Name = colRouteNo
        repoRouteNo.Width = 50
        repoRouteNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRouteNo)

        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Booth"
        repoCustCode.Name = colCustCode
        repoCustCode.Width = 50
        repoCustCode.IsVisible = True
        repoCustCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCName.FormatString = ""
        repoCName.HeaderText = "Booth Name"
        repoCName.Name = colCustName
        repoCName.Width = 150
        repoCName.ReadOnly = True
        repoCName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCName)

        Dim repoShiftName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShiftName.FormatString = ""
        repoShiftName.HeaderText = "Shift Type"
        repoShiftName.Name = colShiftName
        repoShiftName.Width = 100
        repoShiftName.IsVisible = True
        repoShiftName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoShiftName)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "select * from (select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code,tspl_item_master.Short_Description ,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate','Pouch','LTR')
    union
    select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq  from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate','Pouch','LTR')
    union all
    select 'Ambient' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,2 as RowNo,tspl_item_master.Sku_Seq   from tspl_item_master 
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
                repoIName.HeaderText = clsCommon.myCstr(dr("UOM_Code"))
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.itemDesc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                obj.IsFreshAmbient = clsCommon.myCstr(dr("FreshAmbient"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                repoIName.Tag = obj
                repoIName.Name = colItemCode + clsCommon.myCstr(i)
                repoIName.Width = 80
                repoIName.IsVisible = True
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoIName)
            Next
        End If
        Dim repoAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 50
        repoAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmt)
        Dim repoHistBy As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHistBy.FormatString = ""
        repoHistBy.HeaderText = "History By"
        repoHistBy.Name = colHistBy
        repoHistBy.Width = 50
        repoHistBy.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHistBy)

        Dim repoHistOn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHistOn.FormatString = ""
        repoHistOn.HeaderText = "History On"
        repoHistOn.Name = colHistOn
        repoHistOn.Width = 50
        repoHistOn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHistOn)

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
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colSLNo).Name)
                'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colHistBy).Name)
                'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colHistOn).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colHistVer).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colDemandNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colDocumentNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colRouteNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustCode).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustName).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colShiftName).Name)
                Dim TempColGroupCount As Integer = 1
                Dim obj As ItemValueClass = New ItemValueClass()
                Dim i As Integer = 1
                Dim group As Integer
                For dblcolumns As Integer = 8 To gv1.Columns.Count - 3
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                    If obj1 IsNot Nothing Then
                        If clsCommon.CompairString(obj1.IsFreshAmbient, "Fresh") = CompairStringResult.Equal Then
                            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(obj1.ShortDesc)))
                            view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            dblcolumns = dblcolumns + 1
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            dblcolumns = dblcolumns + 1
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            TempColGroupCount += 1
                            group = TempColGroupCount
                        Else
                            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(obj1.ShortDesc)))
                            view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            TempColGroupCount += 1
                            group = TempColGroupCount
                        End If
                    End If
                Next
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colHistBy).Name)
                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(colHistOn).Name)

                gv1.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub


    Private Sub LoadGridData(ByVal frmdate As Date, ByVal Shift As String, ByVal BoothCode As String)
        Try
            Dim lstobj As List(Of clsDemandHistoryMaster)
            gv1.DataSource = Nothing
            gv1.Visible = False
            Dim shiftType As String = ""
            If clsCommon.CompairString(Shift, "Morning") = CompairStringResult.Equal Then
                shiftType = "AM"
            ElseIf clsCommon.CompairString(Shift, "Evening") = CompairStringResult.Equal Then
                shiftType = "PM"
            End If
            lstobj = clsDemandHistoryMaster.GetData(frmdate, shiftType, BoothCode, Nothing)
            If (lstobj IsNot Nothing AndAlso lstobj.Count > 0) Then
                gv1.Visible = True
                LoadBlankGrid()
                Dim dblrows As Integer = 0
                Dim dblAmt As Decimal = 0
                For Each obj As clsDemandHistoryMaster In lstobj

                    gv1.Rows(dblrows).Cells(colSLNo).Value = dblrows + 1
                    gv1.Rows(dblrows).Cells(colHistVer).Value = obj.History_Version
                    gv1.Rows(dblrows).Cells(colDemandNo).Value = obj.Demand_No
                    gv1.Rows(dblrows).Cells(colDocumentNo).Value = obj.Document_No
                    gv1.Rows(dblrows).Cells(colRouteNo).Value = obj.Route_No
                    gv1.Rows(dblrows).Cells(colCustCode).Value = obj.Cust_Code
                    gv1.Rows(dblrows).Cells(colCustName).Value = obj.Cust_Name
                    gv1.Rows(dblrows).Cells(colShiftName).Value = obj.ShiftType
                    gv1.Rows(dblrows).Cells(colHistBy).Value = obj.History_By
                    gv1.Rows(dblrows).Cells(colHistOn).Value = obj.History_ON

                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        For Each objTr As clsDemandHistoryDetail In obj.Arr
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCustCode).Value), objTr.Cust_Code) = CompairStringResult.Equal Then
                                Dim k As Integer = 1
                                For columns = 8 To gv1.Columns.Count - 4
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                    k = k + 1
                                    If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Unit_Code, clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal Then
                                        gv1.Rows(dblrows).Cells(columns).Value = clsCommon.myCDecimal(objTr.Qty)
                                    End If
                                    dblAmt = objTr.Amount
                                Next

                            End If
                        Next
                    End If
                    gv1.Rows(dblrows).Cells(colAmt).Value = dblAmt
                    dblrows += 1
                    gv1.Rows.AddNew()

                Next

                gv1.BestFitColumns()

                gv1.ReadOnly = True
                SetRouteColumns()
                ReStoreGridLayout()
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found for this Booth  ", Me.Text)
                gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub SetRouteColumns()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select isnull(Entry_UOM,0) as Entry_UOM from TSPL_ROUTE_MASTER where Route_No IN( select Route_No from TSPL_CUSTOMER_MASTER where Cust_Code = '" & txtBooth.Value & "') ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For dblcolumns As Integer = 8 To gv1.Columns.Count - 4
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                    If obj1 IsNot Nothing Then
                        If clsCommon.CompairString(obj1.IsFreshAmbient, "Fresh") = CompairStringResult.Equal Then
                            If clsCommon.myCDecimal(dt.Rows(0)("Entry_UOM")) = 0 Then
                                gv1.Columns(dblcolumns).IsVisible = True
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = False
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = True
                            ElseIf clsCommon.myCDecimal(dt.Rows(0)("Entry_UOM")) = 1 Then
                                gv1.Columns(dblcolumns).IsVisible = True
                                gv1.Columns(dblcolumns).Width = 60
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = False
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = False
                            ElseIf clsCommon.myCDecimal(dt.Rows(0)("Entry_UOM")) = 2 Then
                                gv1.Columns(dblcolumns).IsVisible = False
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = True
                                gv1.Columns(dblcolumns).Width = 60
                                dblcolumns += 1
                                gv1.Columns(dblcolumns).IsVisible = False
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
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

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(txtBooth.Value) > 0 AndAlso clsCommon.myLen(cmbShift.Text) > 0 Then
                'fillGridReport(txtDate.Value, cmbShift.Text, txtBooth.Value)
                LoadGridData(txtDate.Value, cmbShift.Text, txtBooth.Value)
            Else
                Throw New Exception(" Please Select Shift/Booth ")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtBooth__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBooth._MYValidating
        Try
            Dim StrQry As String = "select Cust_Code as Code,Customer_Name as [Customer Name],Route_No as [Route No],Cust_Group_Code as [Customer Group] from TSPL_CUSTOMER_MASTER "
            Dim WhrCls As String = " Status='N' and  CUSTOMER_FORM_TYPE = 'ALL'"
            txtBooth.Value = clsCommon.ShowSelectForm("BoothDetails", StrQry, "Code", WhrCls, txtBooth.Value, "Code", isButtonClicked)
            txtBoothDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER  where Cust_Code ='" + txtBooth.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmDemandHistory & "'"))
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy"))
            If clsCommon.CompairString(cmbShift.Text, "Morning") = CompairStringResult.Equal Then
                arrHeader.Add("Shift Type : Morning")
            ElseIf clsCommon.CompairString(cmbShift.Text, "Evening") = CompairStringResult.Equal Then
                arrHeader.Add("Shift Type : Evening")
            End If
            arrHeader.Add("Booth : " + txtBoothDesc.Text)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
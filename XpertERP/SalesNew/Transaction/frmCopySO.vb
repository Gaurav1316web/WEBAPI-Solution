Imports common
Public Class FrmCopySO

#Region "Variables"
    Public strFirstSO As String = Nothing
    Public strCurrCode As String = Nothing
    Public ArrReturn As List(Of clsSNSalesOrderDetail) = Nothing
    Dim IsInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colRowType As String = "COLTYPE"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "Assessable"
    Const colAbatementRate As String = "colAbatementRate"
    Const colDQty As String = "colDQty"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
#End Region

    

    Private Sub FrmCopySO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
    End Sub
    Sub LoadHeadData(ByVal dtAllData As DataTable)
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("Document_Date"))
            End If
        Next
        IsInsideLoadData = False
    End Sub
    Sub LoadBlankHeadGrid()
        gvHead.Rows.Clear()
        gvHead.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colHSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "SO No"
        repoCode.Name = colHCode
        repoCode.Width = 150
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 100
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = False
        gvHead.AllowRowReorder = False
        gvHead.EnableSorting = False
        gvHead.EnableAlternatingRowColor = True
        gvHead.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvHead.MasterTemplate.ShowRowHeaderColumn = False
        gvHead.TableElement.TableHeaderHeight = 40
    End Sub
    Sub LoadBlankGridDetail()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        repoUnit.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoUnit)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoRate.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.ReadOnly = True
        repoMRP.IsVisible = True
        repoMRP.WrapText = True
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessable.FormatString = ""
        repoAssessable.HeaderText = "Assessable"
        repoAssessable.Name = colDAssessable
        repoAssessable.ReadOnly = True
        repoAssessable.IsVisible = False
        repoAssessable.WrapText = True
        repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessable)



        Dim AbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        AbatementRate = New GridViewDecimalColumn()
        AbatementRate.FormatString = ""
        AbatementRate.HeaderText = "Abatement Rate"
        AbatementRate.Name = colAbatementRate
        AbatementRate.ReadOnly = True
        AbatementRate.IsVisible = False
        AbatementRate.WrapText = True
        AbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(AbatementRate)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colDQty
        repoQty.ReadOnly = True
        repoQty.IsVisible = True
        repoQty.WrapText = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)
        gv1.BestFitColumns()
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub


    

    Private Sub txtCustomerNo__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustomerNo._MYValidating
        Dim qry As String = "select Cust_Code as code ,Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        txtCustomerNo.Value = clsCommon.ShowSelectForm("cpSOCustFdr", qry, "Code", "", txtCustomerNo.Value, "code", isButtonClicked)
        lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomerNo.Value + "'"))
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Sub btnOKPressed()
        ArrReturn = New List(Of clsSNSalesOrderDetail)
        Dim obj As clsSNSalesOrderDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            obj = New clsSNSalesOrderDetail()
            obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
            obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
            obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
            obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
            obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
            obj.Assessable = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDAssessable).Value)
            'obj.AbatementRate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAbatementRate).Value)
            obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDQty).Value)
            If (obj.Balance_Qty > 0) Then
                ArrReturn.Add(obj)
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one item")
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        If clsCommon.myLen(txtCustomerNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Customer")
            Exit Sub
        End If

        Dim qry As String = "Select CAST(0 as bit) as Sel,TSPL_SD_SALES_ORDER_DETAIL.Document_Code as Code,TSPL_SD_SALES_ORDER_HEAD.Document_Date "
        qry += " from TSPL_SD_SALES_ORDER_DETAIL  "
        qry += " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code =TSPL_SD_SALES_ORDER_DETAIL.Document_Code  where  TSPL_SD_SALES_ORDER_HEAD.Customer_Code='" + txtCustomerNo.Value + "' and TSPL_SD_SALES_ORDER_HEAD.Document_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SD_SALES_ORDER_HEAD.Document_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_SD_SALES_ORDER_HEAD.Document_Date"

        

        LoadHeadData(clsDBFuncationality.GetDataTable(qry))
        LoadBlankGridDetail()

    End Sub
    Sub LoadDetailData()
        LoadBlankGridDetail()
        Dim arrSONo As New ArrayList()
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                arrSONo.Add(clsCommon.myCstr(gvHead.Rows(ii).Cells(colHCode).Value))
            End If
        Next
        If arrSONo Is Nothing OrElse arrSONo.Count <= 0 Then
            Exit Sub
        Else
            strFirstSO = arrSONo(0)
        End If

        Dim qry As String = " select ICode,MAX(IName) as IName,MAX(IType) as IType,SUM(Qty) as Qty,MAX(Unit) as Unit,Max(Rate) as Rate,MAX(Assessable) as Assessable,MRP  as MRP  from (Select TSPL_SD_SALES_ORDER_DETAIL.Document_Code  as Code,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_SD_SALES_ORDER_DETAIL.Row_Type as IType,(TSPL_SD_SALES_ORDER_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Qty,(select UOM_Code from TSPL_ITEM_UOM_DETAIL as innUOM where innUOM.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code and innUOM.Stocking_Unit='Y') as Unit,TSPL_SD_SALES_ORDER_DETAIL.Location as Location,TSPL_SD_SALES_ORDER_DETAIL.Item_Cost as Rate,TSPL_SD_SALES_ORDER_HEAD.Tax_Group,ISNULL(TSPL_SD_SALES_ORDER_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_SD_SALES_ORDER_DETAIL.MRP,0) as MRP ,TSPL_ITEM_MASTER.Item_Desc " + Environment.NewLine
        qry += " from TSPL_SD_SALES_ORDER_DETAIL  " + Environment.NewLine
        qry += " left outer join TSPL_SD_SALES_ORDER_HEAD  on TSPL_SD_SALES_ORDER_HEAD.Document_Code =TSPL_SD_SALES_ORDER_DETAIL.Document_Code" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALES_ORDER_DETAIL.Item_Code " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALES_ORDER_DETAIL.Unit_code" + Environment.NewLine
        qry += " where  TSPL_SD_SALES_ORDER_HEAD.Document_Code in (" + clsCommon.GetMulcallString(arrSONo) + ")" + Environment.NewLine
        qry += " )xxx group by ICode,MRP"
        Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtAllData IsNot Nothing AndAlso dtAllData.Rows.Count > 0 Then
            For Each dr As DataRow In dtAllData.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDAssessable).Value = clsCommon.myCdbl(dr("Assessable"))
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = clsCommon.myCdbl(dr("AbatementRate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDQty).Value = clsCommon.myCdbl(dr("Qty"))
            Next
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        Try
            If Not IsInsideLoadData Then
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                        gvHead.CurrentRow.Cells(colHSelect).Value = e.NewValue
                    End If
                    LoadDetailData()
                    isCellValueChanged = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class

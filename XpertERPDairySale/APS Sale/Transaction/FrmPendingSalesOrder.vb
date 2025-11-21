Imports common
Public Class FrmPendingSalesOrder
#Region "Variables"
    Public Property DocCode As String = ""
    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False
    Public ArrReturn As List(Of clsCustomerTenderOrderDetail) = Nothing
    Const colHSelect As String = "SELECT"
    Const colSalesOrder As String = "colSalesOrder"
    Const colDocDate As String = "colDocDate"
    Const colRALNO As String = "colRALNO"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colLocation As String = "colLocation"
    Const colSubLocation As String = "colSubLocation"
    Const colHICode As String = "colHICode"
    Const colHIRate As String = "colHIRate"
    Const colDSelect As String = "colDSelect"
    Const colSalesordDtl As String = "colSalesordDtl"
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colRowType As String = "colRowType"
    Const colUnitCode As String = "colUnitCode"
    Const colItemRate As String = "colItemRate"
    Const colQty As String = "colTotQty"
    Const colPendingQty As String = "colPendingQty"
    Const colTotTaxAmt As String = "colTotTaxAmt"
    Const colTotalAmt As String = "colTotalAmt"
    Const colPKID As String = "colPKID"
#End Region
    Private Sub FrmPendingSalesOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllDataOnLoadAndButtonGoClick()
    End Sub
    Sub LoadBlankHeadGrid()
        Try
            gvHead.Rows.Clear()
            gvHead.Columns.Clear()
            Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoSelect.HeaderText = " "
            repoSelect.Name = colHSelect
            repoSelect.ReadOnly = False
            repoSelect.Width = 25
            repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gvHead.MasterTemplate.Columns.Add(repoSelect)
            Dim repoSalesOrd As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSalesOrd.FormatString = ""
            repoSalesOrd.HeaderText = "Sales Order"
            repoSalesOrd.Name = colSalesOrder
            repoSalesOrd.Width = 170
            repoSalesOrd.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoSalesOrd)
            Dim repoDocDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoDocDate.FormatString = ""
            repoDocDate.HeaderText = "Document Date"
            repoDocDate.Name = colDocDate
            repoDocDate.Width = 70
            repoDocDate.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoDocDate)
            Dim repoRalno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoRalno.FormatString = ""
            repoRalno.HeaderText = "RAL No"
            repoRalno.Name = colRALNO
            repoRalno.Width = 170
            repoRalno.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoRalno)
            Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCustCode.FormatString = ""
            repoCustCode.HeaderText = "Customer Code"
            repoCustCode.Name = colCustCode
            repoCustCode.Width = 170
            repoCustCode.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoCustCode)
            Dim repoCustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCustName.FormatString = ""
            repoCustName.HeaderText = "Customer Name"
            repoCustName.Name = colCustName
            repoCustName.Width = 170
            repoCustName.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoCustName)
            Dim repoLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLoc.FormatString = ""
            repoLoc.HeaderText = "Location"
            repoLoc.Name = colLocation
            repoLoc.Width = 170
            repoLoc.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(repoLoc)
            Dim reposubLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            reposubLoc.FormatString = ""
            reposubLoc.HeaderText = "Sub Locaiton"
            reposubLoc.Name = colSubLocation
            reposubLoc.Width = 170
            reposubLoc.ReadOnly = True
            gvHead.MasterTemplate.Columns.Add(reposubLoc)
            Dim repoHICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoHICode.FormatString = ""
            repoHICode.HeaderText = "Item Code"
            repoHICode.Name = colHICode
            repoHICode.Width = 170
            repoHICode.ReadOnly = True
            repoHICode.IsVisible = False
            gvHead.MasterTemplate.Columns.Add(repoHICode)
            Dim repoHIRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoHIRate.FormatString = ""
            repoHIRate.HeaderText = "Item Rate"
            repoHIRate.Name = colHIRate
            repoHIRate.Width = 170
            repoHIRate.ReadOnly = True
            repoHIRate.IsVisible = False
            repoHIRate.VisibleInColumnChooser = False
            gvHead.MasterTemplate.Columns.Add(repoHIRate)
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
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            LoadAllDataOnLoadAndButtonGoClick()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadAllDataOnLoadAndButtonGoClick()
        Try
            Dim qry As String = "select PK_Id, max(Final.Document_Code) as Document_Code,max(Final.Document_Date) as Document_Date,max(Final.Cust_Code) as Cust_Code,max(Final.Customer_Name) as Customer_Name,max(Final.RAL_No) as RAL_No,max(Final.Location_Code) as Location_Code,max(Final.Sub_Location)as Sub_Location,max(Final.Item_Code) as Item_Code,max(Final.Short_Description) as Short_Description,max(Final.RowType) as RowType,max(Final.Unit_Code) as Unit_Code,max(Final.Item_Rate) as Item_Rate
,sum(Final.Qty * case when RI=1 then 1 else 0 end) as Qty,sum(Final.Qty * case when RI=-1 then 1 else 0 end) as UsedQty
,sum(Final.Qty*RI) as PendingQty

from (

select TSPL_CUSTOMER_TENDER_ORDER.Document_Code,Convert(varchar(20),TSPL_CUSTOMER_TENDER_ORDER.Document_Date,103) as Document_Date,TSPL_CUSTOMER_TENDER_ORDER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_TENDER_ORDER.RAL_No,TSPL_CUSTOMER_TENDER_ORDER.Location_Code,TSPL_CUSTOMER_TENDER_ORDER.Sub_Location,
TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_CUSTOMER_TENDER_ORDER_DETAIL.RowType,TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Unit_Code,TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Item_Rate,TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Qty, isnull(TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Total_Tax_Amt,0) as Total_Tax_Amt,TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Total_Amt,TSPL_CUSTOMER_TENDER_ORDER_DETAIL.PK_Id,1 as RI,1 as chk
from TSPL_CUSTOMER_TENDER_ORDER 
left join TSPL_CUSTOMER_TENDER_ORDER_DETAIL on TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Document_Code=TSPL_CUSTOMER_TENDER_ORDER.Document_Code
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_TENDER_ORDER.Cust_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Item_Code
where TSPL_CUSTOMER_TENDER_ORDER.status=1 and isnull(TSPL_CUSTOMER_TENDER_ORDER.close_yn,'N')='N'

union all

select '' as Document_Code,convert(varchar(20),TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,'' as RAL_No,'' as Location_Code,'' as Sub_Location,
TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_SD_SHIPMENT_DETAIL.Row_Type as RowType,TSPL_SD_SHIPMENT_DETAIL.Unit_Code,TSPL_SD_SHIPMENT_DETAIL.Item_Cost as Item_Rate,TSPL_SD_SHIPMENT_DETAIL.Qty,isnull(TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt,0) as Total_Tax_Amt,TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt as Total_Amt,TSPL_SD_SHIPMENT_DETAIL.Against_Cust_Ord_PK_ID as PK_ID,-1 as ri,0 as chk
from TSPL_SD_SHIPMENT_HEAD 
left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Document_Code=TSPL_SD_SHIPMENT_HEAD.Document_Code
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
where   TSPL_SD_SHIPMENT_DETAIL.Against_Cust_Ord_PK_ID is not null and TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE not in ('" & DocCode & "')

) Final
group by Final.PK_ID having sum(chk)>0 and sum(Final.Qty*RI)>0
"
            dtAllData = clsDBFuncationality.GetDataTable(qry)
            If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Sales order not found!")
                Me.Close()
            End If
            LoadHeadData()
            LoadBlankGridDetail()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadHeadData()
        Try
            IsInsideLoadData = True
            LoadBlankHeadGrid()
            Dim arr As New List(Of String)
            For Each dr As DataRow In dtAllData.Rows
                Dim strCode As String = clsCommon.myCstr(dr("Document_Code"))
                If Not arr.Contains(strCode) Then
                    arr.Add(strCode)
                    gvHead.Rows.AddNew()
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colSalesOrder).Value = clsCommon.myCstr(dr("Document_Code"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colDocDate).Value = clsCommon.myCstr(dr("Document_Date"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colRALNO).Value = clsCommon.myCstr(dr("RAL_No"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colCustCode).Value = clsCommon.myCstr(dr("Cust_Code"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_Name"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colLocation).Value = clsCommon.myCstr(dr("Location_Code"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colSubLocation).Value = clsCommon.myCstr(dr("Sub_Location"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gvHead.Rows(gvHead.RowCount - 1).Cells(colHIRate).Value = clsCommon.myCstr(dr("Item_Rate"))
                End If
            Next
            IsInsideLoadData = False
        Catch ex As Exception
            IsInsideLoadData = False
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadBlankGridDetail()
        Try
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoSelect.HeaderText = " "
            repoSelect.Name = colDSelect
            repoSelect.ReadOnly = False
            repoSelect.Width = 25
            repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoSelect)
            Dim repoSalesordDtl As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSalesordDtl.FormatString = ""
            repoSalesordDtl.HeaderText = "Sales Order No"
            repoSalesordDtl.Name = colSalesordDtl
            repoSalesordDtl.Width = 170
            repoSalesordDtl.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoSalesordDtl.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoSalesordDtl)
            Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoItemCode.FormatString = ""
            repoItemCode.HeaderText = "Item Code"
            repoItemCode.Name = colItemCode
            repoItemCode.Width = 80
            repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoItemCode.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoItemCode)
            Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoItemDesc.FormatString = ""
            repoItemDesc.HeaderText = "Item Desc"
            repoItemDesc.Name = colItemDesc
            repoItemDesc.Width = 170
            repoItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoItemDesc.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoItemDesc)
            Dim repoRowType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoRowType.FormatString = ""
            repoRowType.HeaderText = "RowType"
            repoRowType.Name = colRowType
            repoRowType.Width = 100
            repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoRowType.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoRowType)
            Dim repoUnitCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoUnitCode.FormatString = ""
            repoUnitCode.HeaderText = "Unit Code"
            repoUnitCode.Name = colUnitCode
            repoUnitCode.Width = 100
            repoUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoUnitCode.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoUnitCode)
            Dim repoItemRate As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoItemRate.FormatString = ""
            repoItemRate.HeaderText = "Item Rate"
            repoItemRate.Name = colItemRate
            repoItemRate.Width = 80
            repoItemRate.ReadOnly = True
            repoItemRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoItemRate.FormatString = "{0:n6}"
            repoItemRate.DecimalPlaces = 6
            gv1.MasterTemplate.Columns.Add(repoItemRate)
            Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoQty.FormatString = ""
            repoQty.HeaderText = "Qty"
            repoQty.Name = colQty
            repoQty.Width = 80
            repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoQty.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoQty)
            Dim repoPQty As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoPQty.FormatString = ""
            repoPQty.HeaderText = "Pending Qty"
            repoPQty.Name = colPendingQty
            repoPQty.Width = 80
            repoPQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoPQty.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoPQty)
            Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoTotTaxAmt.FormatString = ""
            repoTotTaxAmt.HeaderText = "Total Tax Amt"
            repoTotTaxAmt.Name = colTotTaxAmt
            repoTotTaxAmt.Width = 80
            repoTotTaxAmt.ReadOnly = True
            repoTotTaxAmt.IsVisible = False
            repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoTotTaxAmt.FormatString = "{0:n6}"
            repoTotTaxAmt.DecimalPlaces = 6
            gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)
            Dim repoTotamt As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoTotamt.FormatString = ""
            repoTotamt.HeaderText = "Total Amt"
            repoTotamt.Name = colTotalAmt
            repoTotamt.Width = 170
            repoTotamt.ReadOnly = True
            repoTotamt.IsVisible = False
            repoTotamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoTotamt.FormatString = "{0:n6}"
            repoTotamt.DecimalPlaces = 6
            gv1.MasterTemplate.Columns.Add(repoTotamt)
            Dim repoPKID As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoPKID.FormatString = ""
            repoPKID.HeaderText = "PKID"
            repoPKID.Name = colPKID
            repoPKID.Width = 60
            repoPKID.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoPKID.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoPKID)
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
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentCell Is gv1.Columns(colSalesordDtl) Then
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colSalesordDtl).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colSalesordDtl).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub
    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        Try
            If Not IsInsideLoadData Then
                Dim strCode As String
                strCode = clsCommon.myCstr(gvHead.CurrentRow.Cells(colSalesOrder).Value)
                If clsCommon.myLen(strCode) > 0 Then
                    LoadDetailData(e.NewValue, strCode)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Document_Code"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSalesordDtl).Value = clsCommon.myCstr(dr("Document_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Short_Description"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsCommon.myCstr(dr("RowType"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemRate).Value = clsCommon.myCDecimal(dr("Item_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCDecimal(dr("Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsCommon.myCDecimal(dr("PendingQty"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = clsCommon.myCDecimal(dr("Total_Tax_Amt"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAmt).Value = clsCommon.myCDecimal(dr("Total_Amt"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPKID).Value = clsCommon.myCstr(dr("PK_ID"))
                End If
            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colSalesordDtl).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        btnOKPressed()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub
    Private Function isAllowed() As Boolean

        Dim arrVendor As New List(Of String)
        Dim arrICode As New List(Of String)
        Dim arrIRate As New List(Of String)

        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colCustCode).Value)
                Dim strICode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHICode).Value)
                Dim strIRate As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHIRate).Value)
                For jj As Integer = ii + 1 To gvHead.RowCount - 1
                    If clsCommon.myCBool(gvHead.Rows(jj).Cells(colHSelect).Value) Then
                        If clsCommon.CompairString(strCode, clsCommon.myCstr(gvHead.Rows(jj).Cells(colCustCode).Value)) <> CompairStringResult.Equal Then
                            arrVendor.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colCustCode).Value))
                        End If
                    End If
                    If clsCommon.myCBool(gvHead.Rows(jj).Cells(colHSelect).Value) Then
                        If clsCommon.CompairString(strICode, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHICode).Value)) <> CompairStringResult.Equal Then
                            arrICode.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHICode).Value))
                        End If
                    End If
                    If clsCommon.myCBool(gvHead.Rows(jj).Cells(colHSelect).Value) Then
                        If clsCommon.CompairString(strIRate, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHIRate).Value)) <> CompairStringResult.Equal Then
                            arrIRate.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHIRate).Value))
                        End If
                    End If
                Next
                If arrVendor.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "More then one customer are not allowed.", Me.Text)
                    Return False
                End If
                If arrICode.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "More then one item are not allowed.", Me.Text)
                    Return False
                End If
                If arrIRate.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Different item rate are not allowed.", Me.Text)
                    Return False
                End If
                Return True
            End If
        Next ''ii
        Return True
    End Function
    Sub btnOKPressed()
        btnOk.Focus()
        If Not isAllowed() Then
            Exit Sub
        End If

        ArrReturn = New List(Of clsCustomerTenderOrderDetail)
        Dim obj As clsCustomerTenderOrderDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsCustomerTenderOrderDetail()
                obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSalesordDtl).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
                obj.RowType = clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value)
                obj.Item_Rate = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colItemRate).Value)
                obj.Unit_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitCode).Value)
                obj.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colPendingQty).Value)
                If (obj.Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
            Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero pending Qty", Me.Text)
        Else
            Me.Close()
        End If
    End Sub
    Sub btnCancelPressed()
        Me.Close()
    End Sub
End Class
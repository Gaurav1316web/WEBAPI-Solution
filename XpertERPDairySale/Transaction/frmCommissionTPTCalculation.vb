Imports common
Public Class frmCommissionTPTCalculation
#Region "Variables"
    Const colLineNo As String = "COLLNO"
    Const colDoc As String = "COLDOC"
    Const colDate As String = "COLDDATE"
    Const colRoute As String = "COLROUTE"
    Const colQty As String = "COLQTY"
    Const colAmt As String = "COLAMT"
    Const colInvoice As String = "COLINVOICE"
    Const colItem As String = "COLITEM"
#End Region

    Private Sub frmCommissionTPTCalculation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AddNew()
            CreateAllTable()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub CreateAllTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Document_Date", "DateTime not NULL")
        coll.Add("From_Date", "Date not NULL")
        coll.Add("To_Date", "Date not NULL")
        coll.Add("Customer_Code", "varchar(12) Not NULL references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Commission_TPT_Rate", "Decimal(18,2) NULL")
        coll.Add("Remarks", "varchar(200) NULL")
        coll.Add("Total_Amount", "Decimal(18,2) NULL")
        coll.Add("Status", "integer not null")
        coll.Add("Created_By", "varchar(12)  NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modify_By", "varchar(12) NOT NULL")
        coll.Add("Modify_Date", "Datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "Datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_COMMISSION_TPT", coll, Nothing, True, True, "", "Document_Code", "Document_Date", True)

        coll = New Dictionary(Of String, String)
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "varchar(30) NOT NULL references TSPL_CUSTOMER_COMMISSION_TPT(Document_Code)")
        coll.Add("Date", "Date not NULL")
        coll.Add("Route_Code", "varchar(12) NULL references TSPL_ROUTE_MASTER(Route_No)")
        coll.Add("Quantity", "Decimal(18,2) NULL")
        coll.Add("Amount", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_COMMISSION_TPT_DETAIL", coll, Nothing, True, True, "TSPL_CUSTOMER_COMMISSION_TPT", "DOCUMENT_CODE", "", True)

        coll = New Dictionary(Of String, String)
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "varchar(30) NOT NULL references TSPL_CUSTOMER_COMMISSION_TPT(Document_Code)")
        coll.Add("Invoice_Code", "Varchar(30) null References TSPL_SD_SALE_INVOICE_HEAD(DOCUMENT_CODE)")
        coll.Add("Item_Code", "varchar(50) NULL References TSPL_ITEM_MASTER(ITEM_CODE) ")
        coll.Add("Quantity", "Decimal(18,2) NULL")
        'coll.Add("Ref_PK_ID", "integer Not NULL references TSPL_CUSTOMER_COMMISSION_TPT_DETAIL(PK_ID)")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_COMMISSION_TPT_INVOICE", coll, Nothing, True, True, "TSPL_CUSTOMER_COMMISSION_TPT", "DOCUMENT_CODE", "", True)


    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLine As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLine.FormatString = ""
        repoLine.HeaderText = "S.No."
        repoLine.Name = colLineNo
        'repoLine.HeaderImage = My.Resources.search4
        'repoLine.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLine.Width = 50
        repoLine.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoLine)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colDate
        repoDate.Width = 100
        repoDate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoDate)

        Dim repoRoute As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRoute.FormatString = ""
        repoRoute.HeaderText = "Route"
        repoRoute.Name = colRoute
        repoRoute.Width = 100
        repoRoute.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRoute.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRoute)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoQty.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 200
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmt.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAmt)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ReadOnly = True
    End Sub

    'Sub LoadBlankGridInvoice()
    '    gvInvoice.Rows.Clear()
    '    gvInvoice.Columns.Clear()

    '    Dim repoLine As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoLine.FormatString = ""
    '    repoLine.HeaderText = "S.No."
    '    repoLine.Name = colLineNo
    '    repoLine.Width = 50
    '    'repoLine.ReadOnly = False
    '    gvInvoice.MasterTemplate.Columns.Add(repoLine)

    '    Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoDate.FormatString = ""
    '    repoDate.HeaderText = "Document Code"
    '    repoDate.Name = colDoc
    '    repoDate.Width = 150
    '    'repoDate.ReadOnly = False
    '    gvInvoice.MasterTemplate.Columns.Add(repoDate)

    '    Dim repoInvoice As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoInvoice.FormatString = ""
    '    repoInvoice.HeaderText = "Invoice No"
    '    repoInvoice.Name = colInvoice
    '    repoInvoice.Width = 150
    '    repoInvoice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'repoInvoice.ReadOnly = False
    '    gvInvoice.MasterTemplate.Columns.Add(repoInvoice)

    '    Dim repoItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoItem.FormatString = ""
    '    repoItem.HeaderText = "Item Code"
    '    repoItem.Name = colItem
    '    repoItem.Width = 100
    '    repoItem.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'repoItem.ReadOnly = False
    '    gvInvoice.MasterTemplate.Columns.Add(repoItem)

    '    Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoQty.FormatString = ""
    '    repoQty.HeaderText = "Quantity"
    '    repoQty.Name = colQty
    '    repoQty.Width = 100
    '    repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'repoQty.ReadOnly = False
    '    gvInvoice.MasterTemplate.Columns.Add(repoQty)


    '    gvInvoice.AllowAddNewRow = False
    '    gvInvoice.ShowGroupPanel = False
    '    gvInvoice.AllowColumnReorder = True
    '    gvInvoice.AllowRowReorder = False
    '    gvInvoice.EnableSorting = False
    '    gvInvoice.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gvInvoice.MasterTemplate.ShowRowHeaderColumn = False
    '    gvInvoice.TableElement.TableHeaderHeight = 40
    '    gvInvoice.ReadOnly = True
    'End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If obj IsNot Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = True
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Try
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddNew()
        txtDocNo.Value = Nothing
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtRemarks.Text = Nothing
        UsLock1.Status = ERPTransactionStatus.Pending
        txtFromDate.Value = txtDate.Value
        txtToDate.Value = txtDate.Value
        txtMultRoute.arrValueMember = Nothing
        txtMultItems.arrValueMember = Nothing
        txtDistributorCode.Value = Nothing
        lblDistributorName.Text = Nothing
        txtCorTPTRate.Value = 0
        RadGroupBox2.Enabled = True
        txtRemarks.Enabled = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        Reset()
        LoadBlankGrid()
        'LoadBlankGridInvoice()
    End Sub

    Sub DisableFields()
        RadGroupBox2.Enabled = False
        txtRemarks.Enabled = False
        btnSave.Enabled = False
        btnPost.Enabled = False
        btnDelete.Enabled = False
    End Sub

    Sub DisableFielsOnGoButton()
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
        txtMultRoute.Enabled = False
        txtMultItems.Enabled = False
        txtDistributorCode.Enabled = False
        lblDistributorName.Enabled = False
        txtCorTPTRate.Enabled = False
        btnGo.Enabled = False
    End Sub

    Sub EnableFielsOnGoButton()
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        txtMultRoute.Enabled = True
        txtMultItems.Enabled = True
        txtDistributorCode.Enabled = True
        lblDistributorName.Enabled = True
        txtCorTPTRate.Enabled = True
        btnGo.Enabled = True
    End Sub
    Sub Reset()
        EnableFielsOnGoButton()
        LoadBlankGrid()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultRoute._My_Click
        Try
            Dim qry As String = "select Route_No As [Route Code],Route_Desc As [Route Name] from TSPL_ROUTE_MASTER"
            txtMultRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("@Route", qry, "Route Code", "Route Name", txtMultRoute.arrValueMember, txtMultRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDistributorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDistributorCode._MYValidating
        Try
            Dim strQry As String = "select Cust_Code As [Code],Customer_Name As [Name] from TSPL_Customer_Master"
            txtDistributorCode.Value = clsCommon.ShowSelectForm("@Distrinbutor", strQry, "Code", Nothing, txtDistributorCode.Value, "Code", isButtonClicked)
            lblDistributorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_Customer_Master Where Cust_Code='" & txtDistributorCode.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultItems__My_Click(sender As Object, e As EventArgs) Handles txtMultItems._My_Click
        Try
            Dim qry As String = " select Item_Code As [Item Code],Item_Desc As [Item Name] from TSPL_ITEM_MASTER order by Item_Code "
            txtMultItems.arrValueMember = clsCommon.ShowMultipleSelectForm("@ItemMulSel", qry, "Item Code", "Item Name", txtMultItems.arrValueMember, txtMultItems.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strBaseQry As String = "select Document_Code,max(Document_Date) as Document_Date,max(Route_No) as Route_No,Item_Code,max(Item_Desc) as Item_Desc,SUM(QtyInLtr) as QtyInLtr,max(CUOM_Code) as CUOM_Code,1 as RI,1 as Chk from ("
            strBaseQry += "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,Convert(Date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,
(TSPL_SD_SALE_INVOICE_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/CinLTR.Conversion_Factor) As QtyInLtr,CinLTR.UOM_Code As CUOM_Code
from TSPL_SD_SALE_INVOICE_DETAIL
Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
Left Outer Join TSPL_ITEM_UOM_DETAIL As CinLTR on CinLTR.Item_Code=TSPL_ITEM_MASTER.Item_Code And CinLTR.UOM_Code='LTR'
where 1=1 "
            If clsCommon.myCDate(txtFromDate.Value) <= clsCommon.myCDate(txtToDate.Value) Then
                strBaseQry += " And TSPL_SD_SALE_INVOICE_HEAD.Document_Date >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' And TSPL_SD_SALE_INVOICE_HEAD.Document_Date <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "
            Else
                clsCommon.MyMessageBoxShow(Me, "From Date can't be greater then To Date !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If
            If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
                strBaseQry += " And TSPL_SD_SALE_INVOICE_HEAD.Route_No In (" & clsCommon.GetMulcallString(txtMultRoute.arrValueMember) & ")"
            Else
                clsCommon.MyMessageBoxShow(Me, "Route can't be blank !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If

            If txtMultItems.arrValueMember IsNot Nothing AndAlso txtMultItems.arrValueMember.Count > 0 Then
                strBaseQry += " And TSPL_SD_SALE_INVOICE_DETAIL.Item_Code In (" & clsCommon.GetMulcallString(txtMultItems.arrValueMember) & ")"
            Else
                clsCommon.MyMessageBoxShow(Me, "Items can't be blank !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If

            If clsCommon.myLen(txtDistributorCode.Value) > 0 Then
                strBaseQry += " And TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" & clsCommon.myCstr(txtDistributorCode.Value) & "' "
            Else
                clsCommon.MyMessageBoxShow(Me, "Distributor can't be blank !", Me.Text)
#Disable Warning
                Exit Sub
#Enable Warning
            End If

            strBaseQry += " )xx group by Document_Code,Item_Code"

            Dim finalQry As String
            'Dim finalQry As String = "Select ROW_NUMBER() Over (Order By Document_Date) As SNo ,Convert(Varchar(10),Document_Date,103)Document_Date,Route_No,SUM(QtyInLtr)QtyInLtr,(SUM(QtyInLtr)*" & clsCommon.myCstr(txtCorTPTRate.Value) & ")  As Amt from (" & strBaseQry & ")Final Group By Document_Date,Route_No  Order By Document_Date,Route_No"
            finalQry = "select ROW_NUMBER() Over (Order By Document_Code) As SNo ,Document_Code,Convert(Varchar(10),MAX(Document_Date),103)as Document_Date,MAX(Route_No) as Route_No,Item_Code,MAX(Item_Desc) as Item_Desc	,SUM(QtyInLtr*RI) as QtyInLtr,max(CUOM_Code) as CUOM_Code,(SUM(QtyInLtr)*" & clsCommon.myCstr(txtCorTPTRate.Value) & ")  As Amt from ( "
            finalQry += strBaseQry
            finalQry += "  union all "
            finalQry += " select Invoice_Code as Document_Code,null as Document_Date,null as Route_No,Item_Code,null as Item_Desc,Quantity as QtyInLtr,null as CUOM_Code,-1 as RI ,0 as chk from TSPL_CUSTOMER_COMMISSION_TPT_INVOICE where TSPL_CUSTOMER_COMMISSION_TPT_INVOICE.Document_Code not in ('asda')
) xx group by Document_Code,Item_Code --having SUM(QtyInLtr*RI)>0 and SUM(chk)>0 "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(finalQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                LoadBlankGrid()
                For Each rows In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(rows("SNo"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = clsCommon.myCstr(rows("Document_Date"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRoute).Value = clsCommon.myCstr(rows("Route_No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(rows("QtyInLtr"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = clsCommon.myCstr(rows("Amt"))
                Next
                DisableFielsOnGoButton()
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
            dt = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowData() As Boolean
        Try
            If clsCommon.myCDate(txtFromDate.Value) > clsCommon.myCDate(txtToDate.Value) Then
                clsCommon.MyMessageBoxShow(Me, "From date can't be grater then To Date !", Me.Text)
                txtFromDate.Focus()
                Return False
            End If

            If txtMultRoute.arrValueMember Is Nothing AndAlso txtMultRoute.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Route can't be blank !", Me.Text)
                txtMultRoute.Focus()
                Return False
            End If

            If txtMultItems.arrValueMember Is Nothing AndAlso txtMultItems.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Item can't be blank !", Me.Text)
                txtMultItems.Focus()
                Return False
            End If

            If clsCommon.myLen(txtDistributorCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Distributor can't be blank !", Me.Text)
                txtDistributorCode.Focus()
                Return False
            End If

            If gv1 Is Nothing OrElse gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Details can't be blank !", Me.Text)
                gv1.Focus()
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            If AllowData() Then
                Dim obj As New clsCommissionTPTCalculation()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.From_Date = txtFromDate.Value
                obj.To_Date = txtToDate.Value
                obj.Distributor_Code = txtDistributorCode.Value
                obj.Commission_TPT_Rate = txtCorTPTRate.Value
                obj.Remarks = txtRemarks.Text

                If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                    obj.Arr = New List(Of clsCommissionTPTCalculationDetail)
                    For Each row In gv1.Rows
                        Dim objTr As New clsCommissionTPTCalculationDetail()
                        objTr.Detail_Date = row.Cells(colDate).Value
                        objTr.Route_No = row.Cells(colRoute).Value
                        objTr.Qty_In_Ltr = row.Cells(colQty).Value
                        objTr.Amt = row.Cells(colAmt).Value
                        If clsCommon.myLen(objTr.Detail_Date) > 0 Then
                            obj.Arr.Add(objTr)
                        End If
                    Next
                End If

                'If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 Then
                '    obj.Arr_Invoice = New List(Of clsCommissionTPTCalculationDetailInvoice)
                '    For Each rowInv In gvInvoice.Rows
                '        Dim objTrInvoice As New clsCommissionTPTCalculationDetailInvoice()
                '        objTrInvoice.Invoice_No = rowInv.Cells(colInvoice).Value
                '        objTrInvoice.Item_Code = rowInv.Cells(colItem).Value
                '        objTrInvoice.Qty_In_Ltr = rowInv.Cells(colQty).Value
                '        If clsCommon.myLen(objTrInvoice.Invoice_No) > 0 Then
                '            obj.Arr_Invoice.Add(objTrInvoice)
                '        End If
                '    Next
                'End If

                Dim isNewEntry As Boolean = False
                If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                    isNewEntry = True
                End If
                If obj.SaveData(obj, isNewEntry) Then
                    If isPost Then
                        If obj.PostData(obj.Document_Code) Then
                            clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully.", Me.Text)
                        End If
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
                    End If
                    LoadData(obj.Document_Code, Nothing)
                End If
                obj = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData(ByVal strDoc As String, ByVal NavType As NavigatorType)
        Dim obj As New clsCommissionTPTCalculation()
        Try
            AddNew()
            obj = obj.GetData(strDoc, NavType)
            If obj IsNot Nothing Then
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                txtMultRoute.arrValueMember = obj.Route_No
                txtMultItems.arrValueMember = obj.Item_Code
                txtDistributorCode.Value = obj.Distributor_Code
                lblDistributorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name From TSPL_Customer_Master Where Cust_Code='" & obj.Distributor_Code & "'"))
                txtCorTPTRate.Value = obj.Commission_TPT_Rate
                txtRemarks.Text = obj.Remarks
                If obj.Status = 1 Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    DisableFields()
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    DisableFielsOnGoButton()
                End If
                Dim i As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each row In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = clsCommon.GetPrintDate(row.Detail_Date, "dd/MM/yyyy")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRoute).Value = row.Route_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = row.Qty_In_Ltr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = row.Amt
                        i += 1
                    Next
                End If
                'If obj.Arr_Invoice IsNot Nothing AndAlso obj.Arr_Invoice.Count > 0 Then
                '    i = 1
                '    For Each rowInv In obj.Arr_Invoice
                '        gvInvoice.Rows.AddNew()
                '        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colLineNo).Value = i
                '        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colDoc).Value = rowInv.Document_Code
                '        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvoice).Value = rowInv.Invoice_No
                '        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colItem).Value = rowInv.Item_Code
                '        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colQty).Value = rowInv.Qty_In_Ltr
                '        i += 1
                '    Next
                'End If
            End If
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim obj As New clsCommissionTPTCalculation()
            If obj.DeleteData(txtDocNo.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Data deleted successfully.")
                AddNew()
            End If
            obj = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            SaveData(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim strQry As String = "Select TSPL_CUSTOMER_COMMISSION_TPT.Document_Code As [DocumentCode],Convert(Varchar(12),TSPL_CUSTOMER_COMMISSION_TPT.Document_Date,103) As [Document Date],Convert(Varchar(12),TSPL_CUSTOMER_COMMISSION_TPT.From_Date,103) As [From Date], Convert(Varchar(12),TSPL_CUSTOMER_COMMISSION_TPT.To_Date,103) As [To Date],TSPL_CUSTOMER_COMMISSION_TPT.Customer_Code As [Distributor Code],TSPL_CUSTOMER_MASTER.Customer_Name As [Distributor Name],Case When IsNull(TSPL_CUSTOMER_COMMISSION_TPT.Status,0)=0 Then 'Pending' Else 'Approved' End As [Status] 
from TSPL_CUSTOMER_COMMISSION_TPT
Inner Join TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_COMMISSION_TPT.Customer_Code"
            LoadData(clsCommon.ShowSelectForm("@Doc", strQry, "DocumentCode", "", txtDocNo.Value, "TSPL_CUSTOMER_COMMISSION_TPT.Document_Date desc", isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            '""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            '""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class
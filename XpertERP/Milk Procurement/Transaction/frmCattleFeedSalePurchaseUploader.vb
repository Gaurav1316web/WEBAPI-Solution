Imports System.Data.SqlClient
Imports common
Imports Telerik
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class FrmCattleFeedSalePurchaseUploader
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim colStartIndex As Integer = 7
    Dim colEndIndex As Integer = 6
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colSNo As String = "colSNo"
    Const colSRNDispatchDate As String = "colSRNDispatchDate"
    Const colDCSUploaderCode As String = "colDCSUploaderCode"
    Const colDCSCode As String = "colDCSCode"
    Const colDCSName As String = "colDCSName"
    Const colZone As String = "colZone"
    Const colGRNNo As String = "colGRNNo"
    Const colTruckNo As String = "colTruckNo"
    Const colChallanNo As String = "colChallanNo"
    Const colFreight As String = "colFreight"
    Const colBillNo As String = "colBillNo"
    Const colType As String = "colType"
    Const colTotalSaleAmount As String = "colTotalSaleAmount"
    Const colItemCode As String = "colItemCode"
    Const colPurchaseQty As String = "colPurchaseQty"
    Const colPurchaseRate As String = "colPurchaseRate"
    Const colPurchaseAmt As String = "colPurchaseAmt"
    Dim isLoadData As Boolean = False
    Dim obj As New clsCattleFeedSalePurchaseUploader()
    Dim objtr As New clsCattleFeedSalePurchaseUploaderDetail()

#End Region
    Private Sub FrmCattleFeedSalePurchaseUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_No", "varchar(30) Not NULL Primary Key")
        coll.Add("Document_date", "DateTime Not NULL")
        coll.Add("Location_Code", "varchar(12) null")
        coll.Add("Sub_Location_Code", "varchar(12) NULL")
        coll.Add("Remarks", "varchar(200) Null")
        coll.Add("Status", "int not null default 0")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "DateTime  Not NULL")
        coll.Add("Modified_By", "varchar(12)  Not NULL")
        coll.Add("Modified_Date", "datetime  Not NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", coll, "", True, False, Nothing, Nothing, Nothing, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "Integer Not NULL identity primary key")
        coll.Add("Document_No", "VARCHAR(30) Not NULL REFERENCES TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER(Document_No)")
        coll.Add("SRN_Dispatch_Date", "Date null")
        coll.Add("VLC_Code", "Varchar(30) not null references TSPL_VLC_MASTER_HEAD(VLC_Code)")
        coll.Add("Zone_Code", "Varchar(30) null references TSPL_ZONE_MASTER (Zone_Code)")
        coll.Add("GRN_No", "Varchar(30) null")
        coll.Add("Truck_No", "Varchar(30) null")
        coll.Add("Challan_No", "Varchar(30) null")
        coll.Add("Freight", "Decimal(18,2) NULL")
        coll.Add("Bill_No", "Varchar(30) null ")
        coll.Add("Sale_Type", "Varchar(10) null ")
        coll.Add("Total_Sale_Amt", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL", coll, Nothing, True, False, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", "Document_No", "")

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "Integer Not NULL identity (1,1) primary key")
        coll.Add("Ref_PK_ID", "integer not NULL references TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_DETAIL (PK_Id) ")
        coll.Add("Document_No", "VARCHAR(30) Not NULL REFERENCES TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER(Document_No)")
        coll.Add("Item_Code", "Varchar(50) not null references TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Unit_code", "varchar(12) NULL")
        coll.Add("Purchase_Qty", "Decimal(18,2) NULL")
        coll.Add("Purchase_Rate", "Decimal(18,2) NULL")
        coll.Add("Purchase_Amt", "Decimal(18,2) NULL")
        coll.Add("Sale_Amt", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_ITEM_DETAIL", coll, "", True, False, "TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER", "Document_No", "")

        SetUserMgmtNew()
        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
        End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Dim repoText As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "SNo"
        repoText.Name = colSNo
        repoText.Width = 40
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd/MM/yyyy"
        repoDate.FormatString = "{0:dd/MM/yyyy}"
        repoDate.HeaderText = "SRN/Dispatch Date"
        repoDate.Name = colSRNDispatchDate
        repoDate.Width = 100
        repoDate.ReadOnly = False
        repoDate.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoDate)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "DCS Uploader Code"
        repoText.Name = colDCSUploaderCode
        repoText.Width = 80
        repoText.ReadOnly = False
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "DCS Code"
        repoText.Name = colDCSCode
        repoText.Width = 100
        repoText.ReadOnly = True
        repoText.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "DCS Name"
        repoText.Name = colDCSName
        repoText.Width = 120
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Zone Code"
        repoText.Name = colZone
        repoText.Width = 70
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "GRN No"
        repoText.Name = colGRNNo
        repoText.Width = 70
        repoText.ReadOnly = False
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_ITEM_MASTER.Item_Code,Short_Description,BulkUom.UOM_Code FROM TSPL_ITEM_MASTER left join (select Item_Code,UOM_Code from  TSPL_ITEM_UOM_DETAIL where Bulk_UOM=1 ) as BulkUom on BulkUom.Item_Code = TSPL_ITEM_MASTER.Item_Code where FG_for_CF = 1 order by Sku_Seq ")
        If dt.Rows.Count > 0 Then
            Dim i As Integer = 1
            For Each dr As DataRow In dt.Rows
                Dim obj As ItemValueClass = New ItemValueClass()
                repoText = New GridViewTextBoxColumn()
                repoText.FormatString = ""
                repoText.HeaderText = dr("Short_Description") + Environment.NewLine + dr("UOM_Code")
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                repoText.Tag = obj
                repoText.Name = colPurchaseQty + clsCommon.myCstr(i)
                repoText.Width = 150
                repoText.ReadOnly = False
                repoText.IsVisible = True
                Gv1.MasterTemplate.Columns.Add(repoText)

                repoText = New GridViewTextBoxColumn()
                repoText.FormatString = ""
                repoText.HeaderText = dr("Short_Description") + Environment.NewLine + "Rate"
                repoText.Name = colPurchaseRate + clsCommon.myCstr(i)
                repoText.Width = 150
                repoText.ReadOnly = True
                repoText.IsVisible = False
                Gv1.MasterTemplate.Columns.Add(repoText)

                repoText = New GridViewTextBoxColumn()
                repoText.FormatString = ""
                repoText.HeaderText = dr("Short_Description") + Environment.NewLine + "Amt"
                repoText.Name = colPurchaseAmt + clsCommon.myCstr(i)
                repoText.Width = 150
                repoText.ReadOnly = False
                repoText.IsVisible = True
                i = i + 1
                Gv1.MasterTemplate.Columns.Add(repoText)
            Next
        End If

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Truck No"
        repoText.Name = colTruckNo
        repoText.Width = 70
        repoText.ReadOnly = False
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Challan No"
        repoText.Name = colChallanNo
        repoText.Width = 70
        repoText.ReadOnly = False
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        Dim repoFreight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreight.FormatString = "{0:n2}"
        repoFreight.HeaderText = "Freight"
        repoFreight.Name = colFreight
        repoFreight.Width = 100
        repoFreight.ReadOnly = False
        repoFreight.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoFreight)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Bill No"
        repoText.Name = colBillNo
        repoText.Width = 100
        repoText.ReadOnly = False
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        Dim repoType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Type"
        repoType.Name = colType
        repoType.Width = 70
        repoType.ReadOnly = False
        repoType.IsVisible = True
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoType.DataSource = GetSaleType()
        repoType.ValueMember = "Code"
        repoType.DisplayMember = "Code"
        Gv1.MasterTemplate.Columns.Add(repoType)

        Dim repoSaleAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSaleAmount.FormatString = "{0:n2}"
        repoSaleAmount.HeaderText = "Total Sale Amount"
        repoSaleAmount.Name = colTotalSaleAmount
        repoSaleAmount.Width = 120
        repoSaleAmount.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoSaleAmount)

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = True
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.AutoSizeRows = False
        Gv1.AllowColumnChooser = True
        Gv1.Rows.AddNew()
        ReStoreGridLayoutgv1()
    End Sub

    Public Shared Function GetSaleType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Cash"
        dr("Name") = "Cash"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Credit"
        dr("Name") = "Credit"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Private Sub ReStoreGridLayoutgv1()
        Try
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To Gv1.Columns.Count - 1 Step ii & 1
                    Gv1.Columns(ii).IsVisible = False
                    Gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                Gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                    If clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                        Throw New Exception("Please select Sub Location")
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                obj = New clsCattleFeedSalePurchaseUploader()
                obj.Document_No = txtDocumentNo.Value
                obj.Document_date = clsCommon.myCDate(txtDocumentDate.Value)
                obj.Location_Code = txtLocation.Value
                obj.Sub_Location_Code = txtSubLocation.Value
                obj.Remarks = txtRemarks.Text
                obj.Arr = New List(Of clsCattleFeedSalePurchaseUploaderDetail)

                For Each grow As GridViewRowInfo In Gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colDCSCode).Value)) > 0 Then
                        Dim objTr As New clsCattleFeedSalePurchaseUploaderDetail()
                        objTr.SRN_Dispatch_Date = clsCommon.myCDate(grow.Cells(colSRNDispatchDate).Value)
                        objTr.VLC_Code = clsCommon.myCstr((grow.Cells(colDCSCode).Value))
                        objTr.Zone_Code = clsCommon.myCstr((grow.Cells(colZone).Value))
                        objTr.GRN_No = clsCommon.myCstr((grow.Cells(colGRNNo).Value))
                        objTr.Truck_No = clsCommon.myCstr((grow.Cells(colTruckNo).Value))
                        objTr.Challan_No = clsCommon.myCstr((grow.Cells(colChallanNo).Value))
                        objTr.Freight = clsCommon.myCDecimal((grow.Cells(colFreight).Value))
                        objTr.Bill_No = clsCommon.myCstr((grow.Cells(colBillNo).Value))
                        objTr.Sale_Type = clsCommon.myCstr((grow.Cells(colType).Value))
                        objTr.Total_Sale_Amt = clsCommon.myCDecimal((grow.Cells(colTotalSaleAmount).Value))
                        obj.Arr.Add(objTr)
                        Dim k As Integer = 1
                        objTr.ArrItemDetails = New List(Of clsCattleFeedSalePurchaseUploaderItemDetail)
                        For dblcolumns As Integer = colStartIndex To Gv1.Columns.Count - colEndIndex
                            If clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseQty")) Then
                                k = clsCommon.myCstr(Gv1.Columns(dblcolumns).Name).Substring(14)
                                Dim obj1 As ItemValueClass = TryCast(Gv1.Columns(colPurchaseQty & clsCommon.myCstr(k)).Tag, ItemValueClass)
                                If obj1 IsNot Nothing AndAlso (clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(grow.Cells(dblcolumns).Value) > 0) Then
                                    Dim objItem As New clsCattleFeedSalePurchaseUploaderItemDetail()
                                    If clsCommon.myCdbl(grow.Cells(dblcolumns).Value) > 0 Then
                                        objItem.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                        objItem.Unit_Code = clsCommon.myCstr(obj1.Unit_code)
                                        objItem.Purchase_Qty = clsCommon.myCDecimal(grow.Cells("" + colPurchaseQty + clsCommon.myCstr(k)).Value)
                                        objItem.Purchase_Rate = clsCommon.myCDecimal(grow.Cells("" + colPurchaseRate + clsCommon.myCstr(k)).Value)
                                        objItem.Purchase_Amt = clsCommon.myCDecimal(grow.Cells("" + colPurchaseAmt + clsCommon.myCstr(k)).Value)
                                        objItem.Sale_Rate = clsEkoPro.GetRateMccSale(txtLocation.Value, clsCommon.myCstr(obj1.itemCode), clsCommon.myCstr(obj1.Unit_code), txtDocumentDate.Value)
                                        objItem.Sale_Amt = clsCommon.myCDecimal(objItem.Purchase_Qty * objItem.Sale_Rate)
                                    End If
                                    objTr.ArrItemDetails.Add(objItem)
                                End If
                            End If
                        Next
                    End If

                Next
                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        isNewEntry = True
        txtDocumentNo.Value = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()
        btnSave.Text = "Save"
        txtRemarks.Text = ""
        LoadBlankGrid()
        isInsideLoadData = False
        btnDelete.Enabled = True
        lblStatus.Status = ERPTransactionStatus.Pending
        ReStoreGridLayoutgv1()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click

        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" & txtDocumentNo.Value & "]" & Environment.NewLine & "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                obj.PostData(MyBase.Form_ID, txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                If (obj.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    btnAddNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        Try
            If clsCommon.myLen(txtDocumentNo) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
            End If
            obj = New clsCattleFeedSalePurchaseUploader()
            txtDocumentNo.Value = obj.getFinder(txtDocumentNo.Value, isButtonClicked)
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER where Document_No='" & txtDocumentNo.Value & "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            Addnew()
            obj = New clsCattleFeedSalePurchaseUploader()
            obj = obj.GetData(strCode, NavTyep, Nothing)
            txtSubLocation.Enabled = True
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0) Then
                isLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                txtSubLocation.Enabled = False
                If obj.Status = 1 Then
                    lblStatus.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnSelectSheet.Enabled = False
                Else
                    lblStatus.Status = ERPTransactionStatus.Pending
                    btnDelete.Enabled = True
                    btnSelectSheet.Enabled = True
                End If
                txtDocumentNo.Value = obj.Document_No
                txtDocumentDate.Value = obj.Document_date
                txtLocation.Value = obj.Location_Code
                lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
                txtSubLocation.Value = obj.Sub_Location_Code
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                txtRemarks.Text = obj.Remarks
                If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                        txtSubLocation.Enabled = True
                    Else
                        txtSubLocation.Enabled = False
                    End If
                End If
                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    For Each objtr As clsCattleFeedSalePurchaseUploaderDetail In obj.Arr
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSNo).Value = Gv1.Rows.Count
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSRNDispatchDate).Value = objtr.SRN_Dispatch_Date
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDCSCode).Value = objtr.VLC_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDCSUploaderCode).Value = objtr.VLC_Code_VLC_Uploader
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDCSName).Value = objtr.VLC_Name
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colZone).Value = objtr.Zone_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colGRNNo).Value = objtr.GRN_No
                        Dim k As Integer = 1
                        If objtr.ArrItemDetails IsNot Nothing AndAlso objtr.ArrItemDetails.Count > 0 Then
                            For Each objItem As clsCattleFeedSalePurchaseUploaderItemDetail In objtr.ArrItemDetails
                                For dblcolumns As Integer = colStartIndex To Gv1.Columns.Count - colEndIndex
                                    If clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseQty")) Then
                                        Dim obj1 As ItemValueClass = New ItemValueClass()
                                        obj1.itemCode = objItem.Item_Code
                                        obj1.Unit_code = clsCommon.myCstr(objItem.Unit_Code)
                                        Gv1.Rows(Gv1.Rows.Count - 1).Cells("" + colPurchaseQty + clsCommon.myCstr(k)).Value = clsCommon.myCDecimal(objItem.Purchase_Qty)
                                        Gv1.Rows(Gv1.Rows.Count - 1).Cells("" + colPurchaseRate + clsCommon.myCstr(k)).Value = clsCommon.myCDecimal(objItem.Purchase_Rate)
                                        Gv1.Rows(Gv1.Rows.Count - 1).Cells("" + colPurchaseAmt + clsCommon.myCstr(k)).Value = clsCommon.myCDecimal(objItem.Purchase_Amt)
                                        Gv1.Rows(Gv1.Rows.Count - 1).Cells("" + colPurchaseQty + clsCommon.myCstr(k)).Tag = obj1
                                        Exit For
                                    End If
                                Next
                                k += 1
                            Next
                        End If
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTruckNo).Value = objtr.Truck_No
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colChallanNo).Value = objtr.Challan_No
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colFreight).Value = objtr.Freight
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colBillNo).Value = objtr.Bill_No
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colType).Value = objtr.Sale_Type
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTotalSaleAmount).Value = clsCommon.myCDecimal(objtr.Total_Sale_Amt)
                        Gv1.Rows.AddNew()
                    Next
                End If

            End If
            isInsideLoadData = True
            isInsideLoadData = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Private Sub FrmCattleFeedSalePurchaseUploader_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnSave.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIR
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseUnpost.Visible = True
            End If
            'Dim frmCustPenaltyInvoice = New FrmCattleFeedSalePurchaseUploaderInvoiceDetails()
            'If isLoadData Then
            '    frmCustPenaltyInvoice.arr = obj.ArrInvoiceDetails
            'Else
            '    frmCustPenaltyInvoice.arr = objtr.ArrInvoiceAllDetails
            'End If
            'frmCustPenaltyInvoice.ShowDialog()
        End If
    End Sub

    Private Sub btnReverseUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseUnpost.Click
        Try
            obj = New clsCattleFeedSalePurchaseUploader()
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" & Environment.NewLine & "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If obj.ReverseAndUnpost(txtDocumentNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim ii As Integer = 0
        For dblcolumns As Integer = colStartIndex To Gv1.Columns.Count - colEndIndex
            Dim dblPurchaseQty As Double = 0
            Dim dblPurchaseRate As Double = 0
            Dim dblPurchaseAmt As Double = 0
            If clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(IntRowNo).Cells(colDCSCode).Value)) > 0 Then
                If clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseQty")) OrElse clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseAmt")) Then
                    ii = clsCommon.myCstr(Gv1.Columns(dblcolumns).Name).Substring(14)
                    dblPurchaseQty = Gv1.Rows(IntRowNo).Cells("colPurchaseQty" + clsCommon.myCstr(ii)).Value
                    dblPurchaseAmt = Gv1.Rows(IntRowNo).Cells("colPurchaseAmt" + clsCommon.myCstr(ii)).Value
                    dblPurchaseRate = dblPurchaseAmt / dblPurchaseQty
                    If dblPurchaseRate > 0 Then
                        Gv1.Rows(IntRowNo).Cells("colPurchaseRate" + clsCommon.myCstr(ii)).Value = dblPurchaseRate
                    Else
                        Gv1.Rows(IntRowNo).Cells("colPurchaseRate" + clsCommon.myCstr(ii)).Value = 0
                    End If
                End If
            End If
        Next
        UpdateAllTotals()
    End Sub
    Sub UpdateAllTotals()
        Dim ii As Integer = 0
        For j As Integer = 0 To Gv1.Rows.Count - 1
            Dim dblTotalSaleAmt As Decimal = 0
            Dim Sale_Rate As Decimal = 0
            If clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(j).Cells(colDCSCode).Value)) > 0 Then
                For dblcolumns As Integer = colStartIndex To Gv1.Columns.Count - colEndIndex
                    If clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseQty")) Then
                        ii = clsCommon.myCstr(Gv1.Columns(dblcolumns).Name).Substring(14)
                        Dim obj1 As ItemValueClass = TryCast(Gv1.Columns(colPurchaseQty & clsCommon.myCstr(ii)).Tag, ItemValueClass)
                        If obj1 IsNot Nothing AndAlso (clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0) Then
                            Sale_Rate = clsEkoPro.GetRateMccSale(txtLocation.Value, clsCommon.myCstr(obj1.itemCode), clsCommon.myCstr(obj1.Unit_code), txtDocumentDate.Value)
                            dblTotalSaleAmt += Gv1.Rows(j).Cells("colPurchaseQty" + clsCommon.myCstr(ii)).Value * Sale_Rate
                        End If
                    End If
                Next
                Gv1.Rows(j).Cells(colTotalSaleAmount).Value = dblTotalSaleAmt
            End If
        Next
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        Dim qry As String = " select  '" & objCommonVar.CurrentUser & "' as User_Code, ROW_NUMBER( ) over( order by TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_date) as SNo,TSPL_COMPANY_MASTER.Comp_Name,convert(varchar,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_detail.Invoice_Date,103) as Invoice_Date,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_detail.Sale_Amt,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_detail.Deposit_Amt,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_detail.Curr_Balance_Amt,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_detail.Balance_Amt,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_detail.Penalty,convert(varchar,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_date,103) as Document_date,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,
        convert(varchar,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.From_Date,103) as From_Date,convert(varchar,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.To_Date,103) as To_Date,TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Penalty_Per from  TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_detail
        left join TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER on TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No = TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER_detail.Document_No left join TSPL_COMPANY_MASTER on 1=1 left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code = TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Cust_Code
        where TSPL_CATTLE_FEED_SALE_PURCHASE_UPLOADER.Document_No='" & txtDocumentNo.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(MyBase.Form_ID, False, CrystalReportFolder.KwalitySalesReport, dt, "rptCustomerPenalty", "Customer Penalty", clsCommon.myCDate(dt.Rows(0)("Document_date")))
            frmCRV = Nothing
        End If
    End Sub

    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        Dim gvimport As New UserControls.MyRadGridView
        Me.Controls.Add(gvimport)
        LoadBlankGrid()
        Dim arr As New List(Of String)
        arr.Add(Gv1.Columns(colSNo).HeaderText)
        arr.Add(Gv1.Columns(colSRNDispatchDate).HeaderText)
        arr.Add(Gv1.Columns(colDCSUploaderCode).HeaderText)
        arr.Add(Gv1.Columns(colGRNNo).HeaderText)

        For dblcolumns As Integer = colStartIndex To Gv1.Columns.Count - colEndIndex
            If clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseQty")) OrElse clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseAmt")) Then
                arr.Add(Gv1.Columns(dblcolumns).HeaderText)
            End If
        Next
        arr.Add(Gv1.Columns(colTruckNo).HeaderText)
        arr.Add(Gv1.Columns(colChallanNo).HeaderText)
        arr.Add(Gv1.Columns(colFreight).HeaderText)
        arr.Add(Gv1.Columns(colBillNo).HeaderText)
        arr.Add(Gv1.Columns(colType).HeaderText)
        arr.Add(Gv1.Columns(colTotalSaleAmount).HeaderText)

        Dim check As Integer = 0
        Dim DCS_Uploader_Code As String = ""
        Dim Type As String = ""
        Dim Dispatch_Date As String = ""
        Dim lineno As Integer = 1
        arr = New List(Of String)
        If transportSql.importExcel(gvimport, arr.ToArray()) Then
            Dim index As Integer = 0
            Dim dtError As New DataTable
            dtError.Columns.Add("RowNo", GetType(Integer))
            dtError.Columns.Add("Error", GetType(String))
            Try
                If gvimport IsNot Nothing AndAlso gvimport.Rows.Count > 0 Then
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gvimport.Rows
                        Try
                            index += 1
                            DCS_Uploader_Code = clsCommon.myCstr(grow.Cells("DCS Uploader Code").Value)
                            Type = (clsCommon.myCstr(grow.Cells("Type").Value))
                            Dispatch_Date = (clsCommon.myCstr(grow.Cells("SRN/Dispatch Date").Value))
                            clsCommon.ProgressBarPercentUpdate(index, Gv1.Rows.Count, "Validating Data...")

                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("DCS Uploader Code").Value)) <= 0 Then
                                Throw New Exception("DCS Uploader Code can't be blank !")
                            ElseIf clsCommon.myLen(DCS_Uploader_Code) > 0 Then
                                check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" & DCS_Uploader_Code & "'"))
                                If check <= 0 Then
                                    Throw New Exception("DCS Uploader Code not found in master at line no. " + clsCommon.myCstr(lineno) + "")
                                End If
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Type").Value)) <= 0 Then
                                Throw New Exception("Sale Type can't be blank !")
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("SRN/Dispatch Date").Value)) <= 0 Then
                                Throw New Exception("SRN/Dispatch Date can't be blank !")
                            End If
                            arr.Add(DCS_Uploader_Code)
                        Catch ex As Exception
                            Dim dr As DataRow = dtError.NewRow()
                            dr("RowNo") = index
                            dr("Error") = ex.Message
                            dtError.Rows.Add(dr)
                        End Try
                    Next
                    clsCommon.ProgressBarPercentHide()
                End If

                Try
                    If dtError.Rows.Count > 0 Then
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = MyBase.Form_ID
                        ff.Text = Me.Text
                        ff.dt = dtError
                        ff.ShowDialog()
                    ElseIf arr IsNot Nothing AndAlso arr.Count > 0 Then
                        Try
                            Dim qry As String = "Valid Row [" + clsCommon.myCstr(arr.Count) + "]" + Environment.NewLine + " Do You want to Proceed"
                            If clsCommon.MyMessageBoxShow(Me, qry, Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                clsCommon.ProgressBarPercentShow()
                                For ii As Integer = 0 To gvimport.Rows.Count - 1
                                    clsCommon.ProgressBarPercentUpdate((gvimport.Rows(ii).Index + 1) * 100 / (gvimport.Rows.Count + 1), "Importing  : " & (gvimport.Rows(ii).Index + 1) & "/" & gvimport.Rows.Count & "")
                                    Gv1.Rows(ii).Cells(colSNo).Value = ii + 1
                                    Gv1.Rows(ii).Cells(colSRNDispatchDate).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("SRN/Dispatch Date").Value)
                                    Gv1.Rows(ii).Cells(colDCSUploaderCode).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("DCS Uploader Code").Value)
                                    Gv1.Rows(ii).Cells(colGRNNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("GRN No").Value)
                                    Dim k As Integer = 1
                                    For j As Integer = 4 To gvimport.Columns.Count - colEndIndex
                                        For dblcolumns As Integer = colStartIndex To Gv1.Columns.Count - colEndIndex
                                            If clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseQty")) Then
                                                If clsCommon.CompairString(clsCommon.myCstr(Gv1.Columns(dblcolumns).HeaderText), gvimport.Columns(j).Name) = CompairStringResult.Equal Then
                                                    k = clsCommon.myCstr(Gv1.Columns(dblcolumns).Name).Substring(14)
                                                    Gv1.Rows(Gv1.Rows.Count - 1).Cells("colPurchaseQty" + clsCommon.myCstr(k)).Value = clsCommon.myCDecimal(gvimport.Rows(ii).Cells(j).Value)
                                                End If
                                            ElseIf clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseAmt")) Then
                                                If clsCommon.CompairString(clsCommon.myCstr(Gv1.Columns(dblcolumns).HeaderText), gvimport.Columns(j).Name) = CompairStringResult.Equal Then
                                                    k = clsCommon.myCstr(Gv1.Columns(dblcolumns).Name).Substring(14)
                                                    Gv1.Rows(Gv1.Rows.Count - 1).Cells("colPurchaseAmt" + clsCommon.myCstr(k)).Value = clsCommon.myCDecimal(gvimport.Rows(ii).Cells(j).Value)
                                                End If
                                            End If
                                        Next
                                    Next

                                    Gv1.Rows(ii).Cells(colTruckNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Truck No").Value)
                                    Gv1.Rows(ii).Cells(colChallanNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Challan No").Value)
                                    Gv1.Rows(ii).Cells(colFreight).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Freight").Value)
                                    Gv1.Rows(ii).Cells(colBillNo).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Bill No").Value)
                                    Gv1.Rows(ii).Cells(colType).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Type").Value)
                                Next
                                clsCommon.ProgressBarPercentHide()
                                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                            End If
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try
                    End If
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception(ex.Message)
                End Try
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

            End Try
        End If
        Me.Controls.Remove(Gv1)
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = " select Location_Code AS Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            txtLocation.Value = clsCommon.ShowSelectForm("CFSPUPLoc", qry, "Code", "", txtLocation.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
            Else
                lblLocation.Text = ""
            End If
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                txtSubLocation.Enabled = True
            Else
                txtSubLocation.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        If clsCommon.myLen(txtLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Location code before sub location", Me.Text)
            Exit Sub
        End If
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            txtSubLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + txtLocation.Value + "'", txtSubLocation.Value, isButtonClicked)
        End If
        If clsCommon.myLen(txtSubLocation.Value) > 0 Then
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
        Else
            lblSubLocation.Text = ""
        End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.Name.Contains(colPurchaseQty) OrElse e.Column.Name.Contains(colPurchaseAmt) Then
                        UpdateCurrentRow(Gv1.CurrentRow.Index) ''-1 is for current row
                    End If
                    If e.Column Is Gv1.Columns(colDCSUploaderCode) Then
                        OpenVLCCode(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub OpenVLCCode(ByVal isButtonClick As Boolean)
        Dim qry As String
        qry = " select VLC_Code as [DCS Code],VLC_Code_VLC_Uploader as DCSUploaderCode,VLC_Name as [DCS Name],TSPL_VENDOR_MASTER.Zone_Code as [Zone Code] from TSPL_VLC_MASTER_HEAD LEFT outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code  "
        Dim whrcls As String = " where VLC_Code_VLC_Uploader = '" + Gv1.CurrentRow.Cells(colDCSCode).Value + "'"
        Gv1.CurrentRow.Cells(colDCSUploaderCode).Value = clsCommon.ShowSelectForm("DCSFND", qry, "DCSUploaderCode", "", Gv1.CurrentRow.Cells(colDCSUploaderCode).Value, "DCSUploaderCode", isButtonClick)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        qry += "  " & whrcls & " "
        Dim dr As DataRow() = clsCommon.MyDTSelect(dt, "DCSUploaderCode='" + Gv1.CurrentRow.Cells(colDCSUploaderCode).Value + "'")
        If dr.Length > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(dr(0).Item("DCSUploaderCode"))) > 0 Then
                Gv1.CurrentRow.Cells(colDCSUploaderCode).Value = clsCommon.myCstr(dr(0).Item("DCSUploaderCode"))
                Gv1.CurrentRow.Cells(colDCSCode).Value = clsCommon.myCstr(dr(0).Item("DCS Code"))
                Gv1.CurrentRow.Cells(colDCSName).Value = clsCommon.myCstr(dr(0).Item("DCS Name"))
                Gv1.CurrentRow.Cells(colZone).Value = clsCommon.myCstr(dr(0).Item("Zone Code"))
            End If
        Else
            SetBlankOfDCSColumns()
        End If
    End Sub
    Sub SetBlankOfDCSColumns()
        Gv1.CurrentRow.Cells(colDCSUploaderCode).Value = ""
        Gv1.CurrentRow.Cells(colDCSCode).Value = ""
        Gv1.CurrentRow.Cells(colDCSName).Value = ""
        Gv1.CurrentRow.Cells(colZone).Value = ""
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles Gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles Gv1.UserDeletedRow
        RefeshSNO()
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To Gv1.Rows.Count
            Gv1.Rows(ii - 1).Cells(colSNo).Value = ii
        Next
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles Gv1.CurrentColumnChanged
        Try
            If Gv1.RowCount > 0 Then
                Dim intCurrRow As Integer = Gv1.CurrentRow.Index
                Gv1.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = Gv1.Rows.Count - 1 Then
                    Gv1.Rows.AddNew()
                    Gv1.CurrentRow = Gv1.Rows(intCurrRow)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExportFormat_Click(sender As Object, e As EventArgs) Handles btnExportFormat.Click
        Dim whrcls As String = ""
        Dim qry As String = "select '' as SNo, '' as 'SRN/Dispatch Date','' as [DCS Uploader Code]" ','' as [DCS Code] , '' as [DCS Name],'' as [Zone Code],
        qry += " ,'' as [GRN No], "

        For dblcolumns As Integer = colStartIndex To Gv1.Columns.Count - colEndIndex
            If clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseQty")) OrElse clsCommon.myCstr(Gv1.Columns(dblcolumns).Name.Contains("colPurchaseAmt")) Then
                qry += "0 as '" + Gv1.Columns(dblcolumns).HeaderText + "',"
            End If
        Next
        qry += " '' as [Truck No],'' as [Challan No],0 as Freight,'' as [Bill No],'' as [Type],0 as [Total Sale Amount] "
        transportSql.ExporttoExcel(qry, "", "", Me)
    End Sub
End Class



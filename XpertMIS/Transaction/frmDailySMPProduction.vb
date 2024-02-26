Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmDailySMPProduction
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ReportID As String = clsUserMgtCode.frmDailySMPProduction
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colIPowderType As String = "IPOWDER"
    Const colProduction As String = "PRODUCTION"
    Const colSale As String = "COLSALE"
    Const colClosingStock As String = "CLOSINSTOCK"
    Private isLoadGrid As Boolean = False
    Public Const colSource As String = "colSource"
    Public Const colCode As String = "colCode"
    Dim colTextBox As GridViewTextBoxColumn = Nothing
    Private Const MaxColumns As Integer = 12
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmDailySMPProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        RadPageView1.SelectedPage = RadPageViewPage1
        isLoadGrid = True
        AddNew()
    End Sub
    Sub LoadBlankGrid()
        Try
            LoadBlankGridResource()
            Dim query As String
            Dim dtr As DataTable = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].Code,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','RAJSAMAND','BANSWARA') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            query = ""
            For ii As Integer = 0 To dtr.Rows.Count - 1
                If ii > 0 Then
                    query += " UNION ALL "
                End If
                query += "select '" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [Source Name],'" + clsCommon.myCstr(dtr.Rows(ii).Item("Code")) + "' AS Code"

            Next
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gv1.DataSource = Nothing
                gv1.AutoGenerateColumns = False
                gv1.DataSource = dt
                gv1.Columns(colSource).FieldName = "Source Name"
                gv1.Columns(colCode).FieldName = "Code"


            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                gv1.DataSource = Nothing
            End If
            If Not isLoadGrid Then
                dt.Clear()
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadBlankGridResource()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Source Name"
        colTextBox.Name = colSource
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Code"
        colTextBox.Name = colCode
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(colTextBox)


        Dim transactionType As New GridViewComboBoxColumn()
        transactionType.HeaderText = "Transaction Type"
        transactionType.Name = "TransactionType"
        transactionType.Width = 120
        transactionType.DataSource = GetTransactionType()
        transactionType.DisplayMember = "Code"
        transactionType.ValueMember = "Code"
        gv1.Columns.Add(transactionType)

        Dim quanity As GridViewDecimalColumn = New GridViewDecimalColumn()
        quanity = New GridViewDecimalColumn()
        quanity.FormatString = ""
        quanity.HeaderText = "Quantity"
        quanity.Name = "Quantity"
        quanity.Width = 100
        quanity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(quanity)

        Dim repoFat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFat = New GridViewDecimalColumn()
        repoFat.FormatString = ""
        repoFat.HeaderText = "FAT%"
        repoFat.Name = "FAT"
        repoFat.Width = 100
        repoFat.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(repoFat)

        Dim repoSnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSnf = New GridViewDecimalColumn()
        repoSnf.FormatString = ""
        repoSnf.HeaderText = "SNF%"
        repoSnf.Name = "SNF"
        repoSnf.Width = 100
        repoSnf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(repoSnf)

        Dim totalJobWork As GridViewDecimalColumn = New GridViewDecimalColumn()
        totalJobWork = New GridViewDecimalColumn()
        totalJobWork.FormatString = ""
        totalJobWork.HeaderText = "Total Job Work"
        totalJobWork.Name = "TotalJobWork"
        totalJobWork.Width = 100
        totalJobWork.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(totalJobWork)

        Dim powderPurchase As GridViewDecimalColumn = New GridViewDecimalColumn()
        powderPurchase = New GridViewDecimalColumn()
        powderPurchase.FormatString = ""
        powderPurchase.HeaderText = "Powder Purchase(MT)"
        powderPurchase.Name = "PowderPurchase"
        powderPurchase.Width = 150
        powderPurchase.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(powderPurchase)


        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = False
        Gv1.ShowFilteringRow = False
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()


        For ii As Integer = 0 To Gv1.Columns.Count - 1
            'Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtReportDate.Focus()
        txtReportingDate.Focus()
        PowderBlankAllControls()
        '' gv1.Rows.AddNew()

    End Sub

    Sub BlankAllControls()
        txtDocNo.MyReadOnly = False
        txtDocNo.Value = Nothing
        txtReportingDate.Value = clsCommon.GETSERVERDATE()
        txtReportDate.Value = DateTime.Today.AddDays(-1)

    End Sub
    Public Sub PowderBlankAllControls()
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        gv2.ReadOnly = False

        Dim repoPWDRType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPWDRType.FormatString = ""
        repoPWDRType.HeaderText = "Powder Type"
        repoPWDRType.Width = 100
        repoPWDRType.Name = colIPowderType
        repoPWDRType.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoPWDRType)
        If isLoadGrid Then
            gv2.MasterTemplate.Rows.Add("SMP")
            gv2.MasterTemplate.Rows.Add("WMP")
        End If


        Dim repoProduction As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProduction = New GridViewDecimalColumn()
        repoProduction.FormatString = ""
        repoProduction.HeaderText = "Production"
        repoProduction.Name = colProduction
        repoProduction.Width = 100
        repoProduction.Minimum = 0
        repoProduction.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoProduction)

        Dim repoSale As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSale = New GridViewDecimalColumn()
        repoSale.FormatString = ""
        repoSale.HeaderText = "Sale"
        repoSale.Name = colSale
        repoSale.Width = 100
        repoSale.Minimum = 0
        repoSale.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoSale)

        Dim repoClosingStock As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoClosingStock = New GridViewDecimalColumn()
        repoClosingStock.FormatString = ""
        repoClosingStock.HeaderText = "Closing Stock"
        repoClosingStock.Name = colClosingStock
        repoClosingStock.Width = 100
        repoClosingStock.Minimum = 0
        repoClosingStock.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoClosingStock)

        gv2.AllowDeleteRow = False
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = True
        gv2.AllowRowReorder = False
        gv2.EnableSorting = True
        gv2.EnableFiltering = False
        gv2.EnableAlternatingRowColor = True
        gv2.AutoSizeRows = False
        gv2.AllowRowResize = True
        gv2.VerticalScrollState = ScrollState.AutoHide
        gv2.HorizontalScrollState = ScrollState.AutoHide
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.TableElement.TableHeaderHeight = 40
        gv2.ShowFilteringRow = True

    End Sub

    Private Function GetTransactionType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim transCodeTable As DataTable = clsDBFuncationality.GetDataTable("SELECT Trans_Code FROM TSPL_MIS_PRODUCTION_TRANSACTION_TYPE")

        For Each row As DataRow In transCodeTable.Rows
            Dim dr As DataRow = dt.NewRow()
            dr("Code") = row("Trans_Code").ToString()
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        isLoadGrid = True
        AddNew()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Private Function SaveData(ByVal ChekPostBtn As Boolean)
        Try
            Dim obj As New clsDailySMPProduction()
            obj.Document_No = txtDocNo.Value
            obj.Report_Date = txtReportDate.Value
            obj.Reporting_Date = txtReportingDate.Value

            obj.Arr = New List(Of clsDailySMPProductionDetails)()
            obj.ArrPw = New List(Of clsDailySMPProductionDetailsPowder)()
            Dim isFirstTime As Boolean = True
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsDailySMPProductionDetails()
                objTr.App_Code = clsCommon.myCstr(grow.Cells(colCode).Value)
                objTr.Trans_Type = clsCommon.myCstr(grow.Cells("TransactionType").Value)
                objTr.Qty = clsCommon.myCdbl(grow.Cells("Quantity").Value)
                objTr.FAT = clsCommon.myCdbl(grow.Cells("FAT").Value)
                objTr.SNF = clsCommon.myCdbl(grow.Cells("SNF").Value)
                objTr.Total_Job_Work = clsCommon.myCdbl(grow.Cells("TotalJobWork").Value)
                objTr.Party_Name1 = clsCommon.myCdbl(grow.Cells("PowderPurchase").Value)
                objTr.arrParty_Name = New List(Of ArrayList)()
                If grow.Cells.Count > grow.Cells("PowderPurchase").ColumnInfo.Index Then
                    Dim startIndex As Integer = clsCommon.myCdbl(grow.Cells("PowderPurchase").ColumnInfo.Index) + 1
                    Dim columnCount As Integer = grow.Cells.Count - startIndex
                    Dim arrayList As New ArrayList()
                    For ii As Integer = startIndex To grow.Cells.Count - 1
                        arrayList.Add(clsCommon.myCdbl(grow.Cells(ii).Value))

                    Next
                    objTr.arrParty_Name.Add(arrayList)
                    objTr.columnCount = columnCount
                End If
                If (clsCommon.myLen(objTr.App_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            For Each grow As GridViewRowInfo In gv2.Rows
                Dim objPw As New clsDailySMPProductionDetailsPowder()
                objPw.Powder_Type = clsCommon.myCstr(grow.Cells(colIPowderType).Value)
                objPw.Production = clsCommon.myCDecimal(grow.Cells(colProduction).Value)
                objPw.Sale = clsCommon.myCdbl(grow.Cells(colSale).Value)
                objPw.Closing_Stock = clsCommon.myCdbl(grow.Cells(colClosingStock).Value)

                If (clsCommon.myLen(objPw.Powder_Type) > 0) Then
                    obj.ArrPw.Add(objPw)
                End If
            Next
            Dim sqlqry As String = "select count(1) from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD where Document_No ='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sqlqry))
            If count = 0 Then
                isNewEntry = True
            Else
                isNewEntry = False
            End If
            If (obj.SaveData(obj, isNewEntry)) Then
                If ChekPostBtn = False Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                End If

            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating

        Dim str As String = "select count(*) from TSPL_MIS_DAILY_SMP_PRODUCTION_HEAD where Document_No ='" + txtDocNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtDocNo.MyReadOnly = False

        Else
            txtDocNo.MyReadOnly = True
        End If
        If txtDocNo.MyReadOnly OrElse isButtonClicked Then

            txtDocNo.Value = clsDailySMPProduction.getFinder("", txtDocNo.Value, isButtonClicked)
            If txtDocNo.Value <> "" Then
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                AddNew()
            End If
        End If
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        txtDocNo.Value = clsDailySMPProduction.DocNO_Navigation(NavType, txtDocNo.Value)
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub
    Sub LoadData(ByVal docno As String, ByVal navtype As NavigatorType)
        Try
            isInsideLoadData = True
            txtDocNo.MyReadOnly = True
            isLoadGrid = False

            Dim obj As New clsDailySMPProduction()
            obj = clsDailySMPProduction.GetData(docno, navtype)
            If (obj IsNot Nothing) Then
                BlankAllControls()
                LoadBlankGrid()
                PowderBlankAllControls()
                btnSave.Text = "Update"
                txtDocNo.Value = obj.Document_No
                txtReportDate.Value = obj.Report_Date
                txtReportingDate.Value = obj.Reporting_Date
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                UsLock1.Status = obj.Status
                For Each objow As clsDailySMPProductionDetails In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSource).Value = objow.Source_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = objow.App_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells("TransactionType").Value = objow.Trans_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells("Quantity").Value = objow.Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells("FAT").Value = objow.FAT
                    gv1.Rows(gv1.Rows.Count - 1).Cells("SNF").Value = objow.SNF
                    gv1.Rows(gv1.Rows.Count - 1).Cells("TotalJobWork").Value = objow.Total_Job_Work
                    gv1.Rows(gv1.Rows.Count - 1).Cells("PowderPurchase").Value = objow.Party_Name1

                Next

                For Each objow As clsDailySMPProductionDetailsPowder In obj.ArrPw
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colIPowderType).Value = objow.Powder_Type
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colProduction).Value = objow.Production
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colSale).Value = objow.Sale
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colClosingStock).Value = objow.Closing_Stock
                Next

            End If
            isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If (clsDailySMPProduction.DeleteData(txtDocNo.Value)) Then
                clsCommon.MyMessageBoxShow(Me, "Delete Data successfully", Me.Text)
                isLoadGrid = True
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsDailySMPProduction.PostData(txtDocNo.Value)) Then
                    msg = "Successfully Posted"

                End If
                If clsCommon.myLen(msg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(msg)
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnAddParty_Click(sender As Object, e As EventArgs) Handles btnAddParty.Click

        If gv1.Columns.Count >= MaxColumns Then
            MessageBox.Show("Maximum number of columns reached.")
            Exit Sub
        End If
        Dim newColumnName As String = InputBox("Enter new column name:", "New Column")

        If Not String.IsNullOrEmpty(newColumnName) Then
            Dim newColumn As New GridViewTextBoxColumn()
            newColumn.Name = newColumnName
            newColumn.HeaderText = newColumnName
            newColumn.Width = 100
            gv1.Columns.Add(newColumn)
        End If
    End Sub

    Private Sub btnAddSource_Click(sender As Object, e As EventArgs) Handles btnAddSource.Click
        SetFocusGrid()
    End Sub
    Sub SetFocusGrid()
        If gv1.CurrentCell IsNot Nothing Then
            Dim selectedIndex As Integer = gv1.CurrentRow.Index

            Dim sourceName As String = gv1.CurrentRow.Cells(colSource).Value.ToString()
            Dim code As String = gv1.CurrentRow.Cells(colCode).Value.ToString()

            Dim newRow As GridViewDataRowInfo = gv1.Rows.AddNew()

            newRow.Cells(colSource).Value = sourceName
            newRow.Cells(colCode).Value = code

            gv1.CurrentRow = newRow
            gv1.CurrentColumn = gv1.Columns("Code")
        Else
            MessageBox.Show("Please select a cell in the grid.")
        End If
    End Sub
End Class

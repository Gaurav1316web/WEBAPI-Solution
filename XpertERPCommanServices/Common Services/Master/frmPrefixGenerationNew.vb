Imports common
Imports System.Data.SqlClient

Public Class FrmPrefixGenerationNew
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variables"
    Dim isInsideLoadData As Boolean = False
    Const colTransType As String = "TRANSTYPE"
    Const colLoaction As String = "LOCATION"
    Const colRouteNo As String = "RouteNo"
    Const colPrefix As String = "PREFIX"
    Const colDontAddPrefix As String = "colDontAddPrefix"
    Const colNextNumber As String = "NEXTNO"
    Const colNextNumberCOPY As String = "NEXTNOCOPY"
    Const colSeprator As String = "SEPRATOR"
    Const colIsChangeMonthly As String = "APPLYMONTHLY"
    Const colMonth As String = "MONTHLY"
    Const colIsOldEntry As String = "ISOLDENTRY"
    Const colIsValueChanged As String = "COLISVALUECHANGED"
    Const colIsChangeDaily As String = "APPLYDAILY"
    Const colCurrentDate As String = "COLCURRENTDATE"
    Const coldontDisplayYearInSeries As String = "coldontDisplayYearInSeries"
    Const colShortFiscalYear As String = "colShortFiscalYear" ''UDL/16/05/19-000295 by balwinder on 15/05/2019
    Const colMinSizeofSeries As String = "colMinSizeofSeries"
    Const colOldTransType As String = "COLOLDTRANSTYPE"
    Const colOldLoaction As String = "COLOLDLOACTION"
    Const colYearSeprator As String = "colYearSeprator"
    Dim PageMode As String
    Dim change As Boolean = True
    Dim isInsideCellValueChaged As Boolean = False

    Dim isLocationReadOnly As Boolean = False
    Dim isTransactionTypeReadOnly As Boolean = False
    Dim isLoadForm As Boolean = False
    Dim isInsidechkFinancialYearStyle As Boolean = False
#End Region

    Private Sub FrmPrefixGenerationNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLoadForm = True
        clsDocType.SetDefaultValues()
        InsertPrefixForItemType()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        LoadFiscalYear()
        'LoadDocumentType()

        Dim dtCurr As Date = clsCommon.GETSERVERDATE()
        Dim dtCurrYear As Integer = dtCurr.Year
        If dtCurr.Month = 1 OrElse dtCurr.Month = 2 OrElse dtCurr.Month = 3 Then
            dtCurrYear -= 1
        End If
        cboFiscalYear.SelectedValue = clsCommon.myCstr(dtCurrYear)
        cboFiscalYear.Enabled = False
        SetUserMgmtNew()
        Dim qry As String = "select Description from TSPL_FIXED_PARAMETER where Type='" + clsFixedParameterType.CounterFinancialYearStyle + "' and Code='" + clsFixedParameterCode.CounterFinancialYearStyle + "'"
        chkFinancialYearStyle.Checked = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1
        isLoadForm = False
    End Sub
    Private Sub FrmPrefixGenerationNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    'Insert New Prefix for Newly Created ItemType
    Private Sub InsertPrefixForItemType()
        Dim Qry = String.Empty
        Qry = "WITH A AS(select * from TSPL_DOCUMENT_TYPE    where Doc_Type='Purchase Order' ) "
        Qry += " INSERT INTO TSPL_DOCUMENT_TYPE (Doc_Type,Doc_Trans_Type,Is_State_Wise,Is_Location_Wise) "
        Qry += " SELECT 'Purchase Order',PREFIX_CODE,0,1 from A T RIGHT JOIN TSPL_ITEM_TYPE_MASTER M ON M.PREFIX_CODE=T.Doc_Trans_Type  WHERE Doc_Trans_Type IS NULL "
        clsDBFuncationality.ExecuteNonQuery(Qry)

        Qry = "WITH A AS(select * from TSPL_DOCUMENT_TYPE    where Doc_Type='Gate Receipt Note' ) "
        Qry += " INSERT INTO TSPL_DOCUMENT_TYPE (Doc_Type,Doc_Trans_Type,Is_State_Wise,Is_Location_Wise) "
        Qry += " SELECT 'Gate Receipt Note',PREFIX_CODE,0,1 from A T RIGHT JOIN TSPL_ITEM_TYPE_MASTER M ON M.PREFIX_CODE=T.Doc_Trans_Type  WHERE Doc_Trans_Type IS NULL "
        clsDBFuncationality.ExecuteNonQuery(Qry)

        Qry = "WITH A AS(select * from TSPL_DOCUMENT_TYPE    where Doc_Type='Material Receipt Note' ) "
        Qry += " INSERT INTO TSPL_DOCUMENT_TYPE (Doc_Type,Doc_Trans_Type,Is_State_Wise,Is_Location_Wise) "
        Qry += " SELECT 'Material Receipt Note',PREFIX_CODE,0,1 from A T RIGHT JOIN TSPL_ITEM_TYPE_MASTER M ON M.PREFIX_CODE=T.Doc_Trans_Type  WHERE Doc_Trans_Type IS NULL "
        clsDBFuncationality.ExecuteNonQuery(Qry)

        Qry = "WITH A AS(select * from TSPL_DOCUMENT_TYPE    where Doc_Type='Store Receipt Note' ) "
        Qry += " INSERT INTO TSPL_DOCUMENT_TYPE (Doc_Type,Doc_Trans_Type,Is_State_Wise,Is_Location_Wise) "
        Qry += " SELECT 'Store Receipt Note',PREFIX_CODE,0,1 from A T RIGHT JOIN TSPL_ITEM_TYPE_MASTER M ON M.PREFIX_CODE=T.Doc_Trans_Type  WHERE Doc_Trans_Type IS NULL "
        clsDBFuncationality.ExecuteNonQuery(Qry)

        Qry = "WITH A AS(select * from TSPL_DOCUMENT_TYPE    where Doc_Type='PO Invoice' ) "
        Qry += " INSERT INTO TSPL_DOCUMENT_TYPE (Doc_Type,Doc_Trans_Type,Is_State_Wise,Is_Location_Wise) "
        Qry += " SELECT 'PO Invoice',PREFIX_CODE,0,1 from A T RIGHT JOIN TSPL_ITEM_TYPE_MASTER M ON M.PREFIX_CODE=T.Doc_Trans_Type  WHERE Doc_Trans_Type IS NULL "
        clsDBFuncationality.ExecuteNonQuery(Qry)

        Qry = "WITH A AS(select * from TSPL_DOCUMENT_TYPE    where Doc_Type='Merchant Purchase Invoice' ) "
        Qry += " INSERT INTO TSPL_DOCUMENT_TYPE (Doc_Type,Doc_Trans_Type,Is_State_Wise,Is_Location_Wise) "
        Qry += " SELECT 'Merchant Purchase Invoice',PREFIX_CODE,0,1 from A T RIGHT JOIN TSPL_ITEM_TYPE_MASTER M ON M.PREFIX_CODE=T.Doc_Trans_Type  WHERE Doc_Trans_Type IS NULL "
        clsDBFuncationality.ExecuteNonQuery(Qry)

        Qry = "WITH A AS(select * from TSPL_DOCUMENT_TYPE    where Doc_Type='Purchase Return' ) "
        Qry += " INSERT INTO TSPL_DOCUMENT_TYPE (Doc_Type,Doc_Trans_Type,Is_State_Wise,Is_Location_Wise) "
        Qry += " SELECT 'Purchase Return',PREFIX_CODE,0,1 from A T RIGHT JOIN TSPL_ITEM_TYPE_MASTER M ON M.PREFIX_CODE=T.Doc_Trans_Type  WHERE Doc_Trans_Type IS NULL "
        clsDBFuncationality.ExecuteNonQuery(Qry)

    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Sub LoadFiscalYear()
        cboFiscalYear.DataSource = GetFiscalYears()
        cboFiscalYear.ValueMember = "Code"
        cboFiscalYear.DisplayMember = "Name"
    End Sub

    Public Shared Function GetFiscalYears() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        For Year As Integer = 2000 To 2050
            Dim dr1 As DataRow = dt.NewRow()
            dr1("Code") = clsCommon.myCstr(Year)
            dr1("Name") = clsCommon.myCstr(Year) + "-" + clsCommon.myCstr(Year + 1 - 2000)
            dt.Rows.Add(dr1)
        Next
        Return dt
    End Function

    'Sub LoadDocumentType()
    '    Dim qry As String = "select Distinct(Doc_Type) as  Code from TSPL_DOCUMENT_TYPE order by Code"
    '    cboDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cboDocument.ValueMember = "Code"
    '    cboDocument.DisplayMember = "Code"
    'End Sub

    'Private Sub cboDocument_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocument.SelectedIndexChanged, cboFiscalYear.SelectedIndexChanged
    '    LoadData()
    'End Sub

    'Private Sub cboDocument_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocument.SelectedValueChanged, cboFiscalYear.SelectedValueChanged
    '    LoadData()
    'End Sub

    Sub LoadData()
        isInsideLoadData = True
        LoadBlankGrid()
        Dim qry As String = "select Doc_Trans_Type,Location_Code,RouteNo,Doc_Prfeix,Next_Number,Separator,Is_Change_Monthly,Curr_Month,Is_Change_Daily,Curr_Date,dontDisplayYearInSeries,Short_Fiscal_Year,MinSizeofSeries,Year_Separator,isnull(Dont_Add_Prefix,0) as Dont_Add_Prefix from TSPL_DOCPREFIX_MASTER where Doc_Type='" + clsCommon.myCstr(txtDocument.Value) + "'"
        If chkFinYear.Checked = True Then
            qry += " and Fin_Year='" + clsCommon.myCstr(cboFiscalYear.SelectedValue) + "'"
        End If
        qry += " order by Doc_Trans_Type,Location_Code "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        isLocationReadOnly = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Location_Wise from TSPL_DOCUMENT_TYPE where TSPL_DOCUMENT_TYPE.Doc_Type='" + clsCommon.myCstr(txtDocument.Value) + "' ")) > 0, False, True)
        isTransactionTypeReadOnly = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when SUM(1)>0 then 1 else 0 end  from TSPL_DOCUMENT_TYPE where TSPL_DOCUMENT_TYPE.Doc_Type='" + clsCommon.myCstr(txtDocument.Value) + "' and len(isnull(Doc_Trans_Type,''))>0")) > 0, False, True)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTransType).Value = clsCommon.myCstr(dr("Doc_Trans_Type"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLoaction).Value = clsCommon.myCstr(dr("Location_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteNo).Value = clsCommon.myCstr(dr("RouteNo"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPrefix).Value = clsCommon.myCstr(dr("Doc_Prfeix"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDontAddPrefix).Value = (clsCommon.myCDecimal(dr("Dont_Add_Prefix")) = 1)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colNextNumber).Value = clsCommon.myCdbl(dr("Next_Number"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colNextNumberCOPY).Value = clsCommon.myCdbl(dr("Next_Number"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSeprator).Value = clsCommon.myCstr(dr("Separator"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsChangeMonthly).Value = clsCommon.myCBool(dr("Is_Change_Monthly"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth).Value = clsCommon.myCdbl(dr("Curr_Month"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsChangeDaily).Value = clsCommon.myCBool(dr("Is_Change_Daily"))
                If dr("Curr_Date") IsNot DBNull.Value Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrentDate).Value = clsCommon.myCDate(dr("Curr_Date"))
                End If
                gv1.Rows(gv1.Rows.Count - 1).Cells(coldontDisplayYearInSeries).Value = clsCommon.myCBool(dr("dontDisplayYearInSeries"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colShortFiscalYear).Value = clsCommon.myCBool(dr("Short_Fiscal_Year"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colYearSeprator).Value = clsCommon.myCstr(dr("Year_Separator"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMinSizeofSeries).Value = clsCommon.myCdbl(dr("MinSizeofSeries"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsOldEntry).Value = True
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsValueChanged).Value = False
                gv1.Rows(gv1.Rows.Count - 1).Cells(colOldTransType).Value = clsCommon.myCstr(dr("Doc_Trans_Type"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colOldLoaction).Value = clsCommon.myCstr(dr("Location_Code"))
            Next
        End If
        gv1.Rows.AddNew()
        funSetFirstRow()
        isInsideLoadData = False
    End Sub

    Public Sub funSetFirstRow()
        If gv1.Rows.Count > 0 Then
            gv1.CurrentRow = gv1.Rows(0)
        End If
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoTransType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTransType.FormatString = ""
        repoTransType.HeaderText = "Transaction Type"
        repoTransType.Name = colTransType
        repoTransType.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoTransType.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTransType.Width = 200
        gv1.MasterTemplate.Columns.Add(repoTransType)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = colLoaction
        repoLocation.MaxLength = 10
        repoLocation.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoLocation.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLocation.Width = 200
        gv1.MasterTemplate.Columns.Add(repoLocation)

        Dim repoRouteNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteNo.FormatString = ""
        repoRouteNo.HeaderText = "Route No"
        repoRouteNo.Name = colRouteNo
        repoRouteNo.MaxLength = 10
        repoRouteNo.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoRouteNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoRouteNo.Width = 200
        gv1.MasterTemplate.Columns.Add(repoRouteNo)

        repoTransType = New GridViewTextBoxColumn()
        repoTransType.FormatString = ""
        repoTransType.HeaderText = "Old Transactin Type"
        repoTransType.Name = colOldTransType
        repoTransType.IsVisible = False
        repoTransType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTransType)

        repoLocation = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Old Location"
        repoLocation.Name = colOldLoaction
        repoLocation.ReadOnly = True
        repoLocation.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLocation)

        Dim repoPrefix As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrefix.FormatString = ""
        repoPrefix.HeaderText = "Prefix"
        repoPrefix.Name = colPrefix
        'repoPrefix.MaxLength = 10
        repoPrefix.MaxLength = 15
        repoPrefix.Width = 100
        gv1.MasterTemplate.Columns.Add(repoPrefix)

        Dim repodontDisplayYearInSeries As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repodontDisplayYearInSeries.FormatString = ""
        repodontDisplayYearInSeries.HeaderText = "Don't Add Prefix"
        repodontDisplayYearInSeries.Name = colDontAddPrefix
        repodontDisplayYearInSeries.Width = 50
        gv1.MasterTemplate.Columns.Add(repodontDisplayYearInSeries)

        Dim repoNxtNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNxtNo.FormatString = ""
        repoNxtNo.DecimalPlaces = 0
        repoNxtNo.HeaderText = "Next No"
        repoNxtNo.Name = colNextNumber
        repoNxtNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoNxtNo)

        Dim repoNxtNoCOPY As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNxtNoCOPY.FormatString = ""
        repoNxtNoCOPY.DecimalPlaces = 0
        repoNxtNoCOPY.HeaderText = "Next No Copy"
        repoNxtNoCOPY.ReadOnly = True
        repoNxtNoCOPY.Name = colNextNumberCOPY
        repoNxtNoCOPY.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoNxtNoCOPY)

        Dim repoSeprator As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSeprator.FormatString = ""
        repoSeprator.HeaderText = "Separator"
        repoSeprator.Name = colSeprator
        repoSeprator.MaxLength = 1
        repoSeprator.Width = 100
        gv1.MasterTemplate.Columns.Add(repoSeprator)

        Dim repoApplyMonthly As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoApplyMonthly.FormatString = ""
        repoApplyMonthly.HeaderText = "Apply Monthly"
        repoApplyMonthly.Name = colIsChangeMonthly
        repoApplyMonthly.Width = 100
        gv1.MasterTemplate.Columns.Add(repoApplyMonthly)

        Dim repoMonth As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMonth.FormatString = ""
        repoMonth.DecimalPlaces = 0
        repoMonth.HeaderText = "Month"
        repoMonth.Name = colMonth
        repoMonth.Minimum = 0
        repoMonth.Maximum = 12
        repoMonth.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoMonth)

        Dim repoOldEntry As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoOldEntry.HeaderText = "Is Old Entry"
        repoOldEntry.Name = colIsOldEntry
        repoOldEntry.ReadOnly = True
        repoOldEntry.Width = 25
        repoOldEntry.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoOldEntry.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoOldEntry)

        repoOldEntry = New GridViewCheckBoxColumn()
        repoOldEntry.HeaderText = "Is Value Changed"
        repoOldEntry.Name = colIsValueChanged
        repoOldEntry.ReadOnly = True
        repoOldEntry.Width = 25
        repoOldEntry.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoOldEntry.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoOldEntry)

        Dim repoApplyDaily As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoApplyDaily.FormatString = ""
        repoApplyDaily.HeaderText = "Apply Daily"
        repoApplyDaily.Name = colIsChangeDaily
        repoApplyDaily.Width = 100
        gv1.MasterTemplate.Columns.Add(repoApplyDaily)

        Dim repoCurrDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoCurrDate.Format = DateTimePickerFormat.Custom
        repoCurrDate.CustomFormat = "dd-MM-yyyy"
        repoCurrDate.HeaderText = "Current Date"
        repoCurrDate.FormatString = "{0:d}"
        repoCurrDate.Name = colCurrentDate
        repoCurrDate.WrapText = True
        repoCurrDate.ReadOnly = False
        repoCurrDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoCurrDate)

        repodontDisplayYearInSeries = New GridViewCheckBoxColumn()
        repodontDisplayYearInSeries.FormatString = ""
        repodontDisplayYearInSeries.HeaderText = "Don't Add Fiscal Year"
        repodontDisplayYearInSeries.Name = coldontDisplayYearInSeries
        repodontDisplayYearInSeries.Width = 100
        gv1.MasterTemplate.Columns.Add(repodontDisplayYearInSeries)

        repodontDisplayYearInSeries = New GridViewCheckBoxColumn()
        repodontDisplayYearInSeries.FormatString = ""
        repodontDisplayYearInSeries.HeaderText = "Short Fiscal Year"
        repodontDisplayYearInSeries.Name = colShortFiscalYear
        repodontDisplayYearInSeries.Width = 100
        gv1.MasterTemplate.Columns.Add(repodontDisplayYearInSeries)

        Dim repoYearSeprator As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoYearSeprator.FormatString = ""
        repoYearSeprator.HeaderText = "Year Separator"
        repoYearSeprator.Name = colYearSeprator
        repoYearSeprator.MaxLength = 1
        repoYearSeprator.Width = 100
        gv1.MasterTemplate.Columns.Add(repoYearSeprator)

        Dim repoMinSizeofSeries As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMinSizeofSeries.FormatString = ""
        repoMinSizeofSeries.DecimalPlaces = 0
        repoMinSizeofSeries.HeaderText = "Numeric Series Length"
        repoMinSizeofSeries.Name = colMinSizeofSeries
        repoMinSizeofSeries.Width = 100
        gv1.MasterTemplate.Columns.Add(repoMinSizeofSeries)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.ShowFilteringRow = True
        gv1.EnableFiltering = True
        'gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData AndAlso e.RowIndex >= 0) Then
                If (Not isInsideCellValueChaged) Then
                    isInsideCellValueChaged = True
                    If (clsCommon.CompairString(e.Column.Name, colTransType) = CompairStringResult.Equal) Then
                        Dim PurchaseCounterOnTransactionType As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseCounterOnTransactionType, clsFixedParameterCode.PurchaseCounterOnTransactionType, Nothing)) = 0, False, True)
                        Dim ShowItemAllStructureWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowItemAllStructureWise, clsFixedParameterCode.ShowItemAllStructureWise, Nothing)) = 0, False, True)
                        Dim DocType As String = clsCommon.myCstr(txtDocument.Value)
                        If (((clsCommon.CompairString(DocType, clsDocType.PurchaserOrder) = CompairStringResult.Equal AndAlso Not ShowItemAllStructureWise) OrElse (clsCommon.CompairString(DocType, clsDocType.GRN) = CompairStringResult.Equal AndAlso Not ShowItemAllStructureWise) OrElse (clsCommon.CompairString(DocType, clsDocType.MRN) = CompairStringResult.Equal AndAlso Not ShowItemAllStructureWise) OrElse (clsCommon.CompairString(DocType, clsDocType.SRN) = CompairStringResult.Equal AndAlso Not ShowItemAllStructureWise) OrElse (clsCommon.CompairString(DocType, clsDocType.POInvoice) = CompairStringResult.Equal AndAlso Not ShowItemAllStructureWise) OrElse (clsCommon.CompairString(DocType, clsDocType.MT_POInvoice) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(DocType, clsDocType.PurchaseReturn) = CompairStringResult.Equal AndAlso Not ShowItemAllStructureWise)) AndAlso PurchaseCounterOnTransactionType = False) Then
                            Dim qry As String = "SELECT PREFIX_CODE AS Code,ITEM_TYPE_NAME AS Description FROM TSPL_ITEM_TYPE_MASTER   "
                            gv1.CurrentRow.Cells(colTransType).Value = clsCommon.ShowSelectForm("CounterTrans", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colTransType).Value), "Code", False)
                        Else
                            Dim qry As String = "select Distinct(Doc_Trans_Type) as Code from TSPL_DOCUMENT_TYPE   "
                            gv1.CurrentRow.Cells(colTransType).Value = clsCommon.ShowSelectForm("CounterTrans", qry, "Code", "Doc_Type='" + clsCommon.myCstr(txtDocument.Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTransType).Value), "Code", False)
                        End If
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colLoaction) = CompairStringResult.Equal) Then
                        Dim qry As String = "select Is_State_Wise,Is_MCC_Wise from TSPL_DOCUMENT_TYPE where Doc_Type='" + clsCommon.myCstr(txtDocument.Value) + "' and ISNULL(Doc_Trans_Type,'')='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTransType).Value) + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Doc Transaction Type is not correct", Me.Text)
                            gv1.CurrentRow.Cells(colLoaction).Value = ""
                            Exit Sub
                        End If
                        If clsCommon.myCdbl(dt.Rows(0)("Is_State_Wise")) = 1 Then
                            qry = "select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
                            gv1.CurrentRow.Cells(colLoaction).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("StatePrefix", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colLoaction).Value), "", False))
                        ElseIf clsCommon.myCdbl(dt.Rows(0)("Is_MCC_Wise")) = 1 Then
                            qry = "select MCC_Code as Code,MCC_NAME as Name from TSPL_MCC_MASTER"
                            gv1.CurrentRow.Cells(colLoaction).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("MCCPrefix", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colLoaction).Value), "", False))
                        Else
                            qry = "select Segment_code as [SegmnentCode],Description as [Description] from TSPL_GL_SEGMENT_CODE"
                            gv1.CurrentRow.Cells(colLoaction).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("PrefixGen", qry, "SegmnentCode", "Seg_No='7'", clsCommon.myCstr(gv1.CurrentRow.Cells(colLoaction).Value), "SegmnentCode", False))
                        End If
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colRouteNo) = CompairStringResult.Equal) Then
                        Dim qry As String = "select Is_Route_Wise from TSPL_DOCUMENT_TYPE where Doc_Type='" + clsCommon.myCstr(txtDocument.Value) + "' and ISNULL(Doc_Trans_Type,'')='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTransType).Value) + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Doc Transaction Type is not correct", Me.Text)
                            gv1.CurrentRow.Cells(colLoaction).Value = ""
                            Exit Sub
                        End If
                        If clsCommon.myCdbl(dt.Rows(0)("Is_Route_Wise")) = 1 Then
                            qry = "select Route_No as Code,Route_Desc as Name from TSPL_ROUTE_MASTER"
                            gv1.CurrentRow.Cells(colRouteNo).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("StatePrefix", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colRouteNo).Value), "", False))

                        End If
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colNextNumber) = CompairStringResult.Equal) Then
                        Dim dblVal As Double = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colNextNumber).Value), 0, MidpointRounding.AwayFromZero)
                        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsOldEntry).Value) Then
                            Dim frm As New FrmPWD(Nothing)
                            frm.strCode = "PrefixGenerator"
                            frm.strType = "PZ"
                            frm.ShowDialog()
                            If frm.isPasswordCorrect Then
                                gv1.CurrentRow.Cells(colNextNumber).Value = dblVal
                                gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                            Else
                                gv1.CurrentRow.Cells(colNextNumber).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colNextNumberCOPY).Value)
                            End If
                        Else
                            gv1.CurrentRow.Cells(colNextNumber).Value = dblVal
                            gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                        End If
                    ElseIf (clsCommon.CompairString(e.Column.Name, colPrefix) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colDontAddPrefix) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colSeprator) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colIsChangeMonthly) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colMonth) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colIsChangeDaily) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colCurrentDate) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, coldontDisplayYearInSeries) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colShortFiscalYear) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colYearSeprator) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    ElseIf (clsCommon.CompairString(e.Column.Name, colMinSizeofSeries) = CompairStringResult.Equal) Then
                        gv1.CurrentRow.Cells(colIsValueChanged).Value = True
                    End If
                    isInsideCellValueChaged = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.PrefixGeneration, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                If gv1.Rows.Count > 0 Then
                    Dim Arr As New List(Of clsDocPrefix)
                    For ii As Integer = 0 To gv1.RowCount - 1
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsValueChanged).Value) Then
                            Dim obj As New clsDocPrefix()
                            obj.Doc_Trans_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colTransType).Value)
                            obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colLoaction).Value)
                            obj.RouteNo = clsCommon.myCstr(gv1.Rows(ii).Cells(colRouteNo).Value)
                            obj.Doc_Prfeix = clsCommon.myCstr(gv1.Rows(ii).Cells(colPrefix).Value)
                            obj.Dont_Add_Prefix = clsCommon.myCBool(gv1.Rows(ii).Cells(colDontAddPrefix).Value)
                            obj.Next_Number = clsCommon.myCstr(gv1.Rows(ii).Cells(colNextNumber).Value)
                            obj.Separator = clsCommon.myCstr(gv1.Rows(ii).Cells(colSeprator).Value)
                            obj.Is_Change_Monthly = clsCommon.myCBool(gv1.Rows(ii).Cells(colIsChangeMonthly).Value)
                            If obj.Is_Change_Monthly Then
                                obj.Curr_Month = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMonth).Value)
                            End If
                            obj.Is_Change_Daily = clsCommon.myCBool(gv1.Rows(ii).Cells(colIsChangeDaily).Value)
                            If obj.Is_Change_Daily Then
                                obj.Curr_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colCurrentDate).Value)
                            End If
                            obj.dontDisplayYearInSeries = clsCommon.myCBool(gv1.Rows(ii).Cells(coldontDisplayYearInSeries).Value)
                            obj.Short_Fiscal_Year = clsCommon.myCBool(gv1.Rows(ii).Cells(colShortFiscalYear).Value)
                            obj.Year_Separator = clsCommon.myCstr(gv1.Rows(ii).Cells(colYearSeprator).Value)
                            obj.MinSizeofSeries = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMinSizeofSeries).Value)

                            obj.isNewEntry = Not clsCommon.myCBool(gv1.Rows(ii).Cells(colIsOldEntry).Value)
                            obj.OldDocTransType = clsCommon.myCstr(gv1.Rows(ii).Cells(colOldTransType).Value)
                            obj.OldLocationCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colOldLoaction).Value)

                            If clsCommon.myLen(obj.Doc_Prfeix) > 0 Then
                                Arr.Add(obj)
                            End If
                        End If
                    Next
                    If clsDocPrefix.SaveData(clsCommon.myCstr(txtDocument.Value), IIf(chkFinYear.Checked = True, Convert.ToInt32(clsCommon.myCdbl(cboFiscalYear.SelectedValue)), 0), Arr) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Counters set successfully", Me.Text)
                        LoadData()
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Error occured in Counters", Me.Text)
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colPrefix).Value) > 0 Then
                If Not isLocationReadOnly AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colLoaction).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter location on line No:" + clsCommon.myCstr(ii + 1) + "", Me.Text)
                    Return False
                End If
                If Not isTransactionTypeReadOnly AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colTransType).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter transaction type on line No:" + clsCommon.myCstr(ii + 1) + "", Me.Text)
                    Return False
                End If
                If clsCommon.myCBool(gv1.Rows(ii).Cells(coldontDisplayYearInSeries).Value) AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells(colShortFiscalYear).Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Select one at a time(Don't Add Fiscal Year/Short Fiscal Year) at line No:" + clsCommon.myCstr(ii + 1) + "", Me.Text)
                    Return False
                End If
                For jj As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(jj).Cells(colPrefix).Value) <= 0 Then
                        Continue For
                    End If
                    If ii = jj Then
                        Continue For
                    End If
                    Dim strTranType As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colTransType).Value)
                    Dim strLocation As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colLoaction).Value)

                    Dim strPrefix As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colPrefix).Value)
                    'Dim strLocation As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colLoaction).Value)


                    If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsChangeMonthly).Value) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colTransType).Value), strTranType) = CompairStringResult.Equal _
                        AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colLoaction).Value), strLocation) = CompairStringResult.Equal _
                        AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colMonth).Value) = clsCommon.myCdbl(gv1.Rows(jj).Cells(colMonth).Value) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Transaction Type : " + strTranType + " and Location : " + strLocation + " and Month : " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colMonth).Value)) + "  Repeted on Line No:" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                            Return False
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colPrefix).Value), strPrefix) = CompairStringResult.Equal _
                        AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colMonth).Value) = clsCommon.myCdbl(gv1.Rows(jj).Cells(colMonth).Value) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Prefix : " + strPrefix + " and Month : " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colMonth).Value)) + " Repeted on Line No:" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                            Return False
                        End If
                    ElseIf clsCommon.myCBool(gv1.Rows(ii).Cells(colIsChangeDaily).Value) Then
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colCurrentDate).Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please select current date  at Line No:" + clsCommon.myCstr(ii + 1), Me.Text)
                            Return False
                        End If
                        If clsCommon.myLen(gv1.Rows(jj).Cells(colCurrentDate).Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please select current date  at Line No:" + clsCommon.myCstr(jj + 1), Me.Text)
                            Return False
                        End If
                        Dim dtOuterDate As Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colCurrentDate).Value, "dd/MMM/yyyy")
                        Dim dtInnerDate As Date = clsCommon.myCDate(gv1.Rows(jj).Cells(colCurrentDate).Value, "dd/MMM/yyyy")
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colTransType).Value), strTranType) = CompairStringResult.Equal _
                        AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colLoaction).Value), strLocation) = CompairStringResult.Equal _
                        AndAlso dtOuterDate = dtInnerDate Then
                            common.clsCommon.MyMessageBoxShow(Me, "Transaction Type : " + strTranType + " and Location : " + strLocation + " and current Date : " + clsCommon.GetPrintDate(dtOuterDate, "dd/MM/yyyy") + "  Repeted on Line No:" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                            Return False
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colPrefix).Value), strPrefix) = CompairStringResult.Equal _
                        AndAlso dtOuterDate = dtInnerDate Then
                            common.clsCommon.MyMessageBoxShow(Me, "Prefix : " + strPrefix + " and current Date : " + clsCommon.GetPrintDate(dtOuterDate, "dd/MM/yyyy") + " Repeted on Line No : " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                            Return False
                        End If
                    Else
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colTransType).Value), strTranType) = CompairStringResult.Equal _
                        AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colLoaction).Value), strLocation) = CompairStringResult.Equal Then
                            common.clsCommon.MyMessageBoxShow(Me, "Transaction Type : " + strTranType + " and Location :" + strLocation + " Repeted on Line No:" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                            Return False
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colPrefix).Value), strPrefix) = CompairStringResult.Equal Then
                            common.clsCommon.MyMessageBoxShow(Me, "Prefix : " + strPrefix + " Repeted on Line No:" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "", Me.Text)
                            Return False
                        End If
                    End If
                Next
            End If
        Next
        Return True
    End Function

    'priti added on 01-06-2011 --- To implement the access control
    Private Function funSetUserAccess() As Boolean
        Try
            'If funCheckLoginStatus() = False Then Exit Function
            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = clsUserMgtCode.PrefixGeneration
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                funSetUserAccess = False
                blnRead = False
                Throw New Exception("Permission Denied")
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access
                btnSave.Enabled = False
            End If
            If strTemp(2) = "0" Then 'Grant modify access
            End If

            funSetUserAccess = True
        Catch er As Exception
            clsCommon.MyMessageBoxShow(Me, er.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colMonth) Then
                    gv1.CurrentRow.Cells(colMonth).ReadOnly = Not clsCommon.myCBool(gv1.CurrentRow.Cells(colIsChangeMonthly).Value)
                ElseIf e.Column Is gv1.Columns(colTransType) Then
                    gv1.CurrentRow.Cells(colTransType).ReadOnly = isTransactionTypeReadOnly
                ElseIf e.Column Is gv1.Columns(colLoaction) Then
                    gv1.CurrentRow.Cells(colLoaction).ReadOnly = isLocationReadOnly
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            If clsCommon.myLen(txtDocument.Value) <= 0 Then
                Exit Sub
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim qry As String = "delete from TSPL_DOCPREFIX_MASTER where Doc_Type= '" + clsCommon.myCstr(txtDocument.Value) + "' and Doc_Trans_Type=ISNULL('" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTransType).Value) + "','')  and Location_Code=isnull('" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLoaction).Value) + "','')"
                If chkFinYear.Checked = True Then
                    qry += " and Fin_Year='" + clsCommon.myCstr(cboFiscalYear.SelectedValue) + "'"
                End If

                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub chkFinancialYearStyle_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkFinancialYearStyle.ToggleStateChanged
        Try
            If isLoadForm = False AndAlso isInsidechkFinancialYearStyle = False Then
                isInsidechkFinancialYearStyle = True
                If (common.clsCommon.MyMessageBoxShow("Do you want to change counter financial year style?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    Dim qry As String = "update TSPL_FIXED_PARAMETER set Description=" + IIf(chkFinancialYearStyle.Checked, "'1'", "'0'") + " where Type='" + clsFixedParameterType.CounterFinancialYearStyle + " ' and Code='" + clsFixedParameterCode.CounterFinancialYearStyle + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                Else
                    Dim qry As String = "select Description from TSPL_FIXED_PARAMETER where Type='" + clsFixedParameterType.CounterFinancialYearStyle + "' and Code='" + clsFixedParameterCode.CounterFinancialYearStyle + "'"
                    chkFinancialYearStyle.Checked = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1
                End If
                isInsidechkFinancialYearStyle = False
            End If
        Catch ex As Exception
            isInsidechkFinancialYearStyle = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If chkFinYear.Checked = True Then  
            Dim frm As New FrmFreeCombo1()
            frm.ShowDialog()
            If clsCommon.myCdbl(frm.strFiscalYear) > 0 Then
                Dim str As String = "select * from ( select Doc_Type,Doc_Trans_Type,Location_Code,max(Doc_Prfeix) as Doc_Prfeix,max(Next_Number) as Next_Number,max(Separator) as Separator,MAX( Is_Change_Monthly) as Is_Change_Monthly,max(Curr_Month) as Curr_Month,max(Is_Change_Daily) as Is_Change_Daily,max(Curr_Date) as Curr_Date,max(dontDisplayYearInSeries) as dontDisplayYearInSeries,max(MinSizeofSeries) as MinSizeofSeries,max(Year_Separator) as Year_Separator,max(Short_Fiscal_Year) as Short_Fiscal_Year from " & _
                "( select TSPL_DOCPREFIX_MASTER.Doc_Type,TSPL_DOCPREFIX_MASTER.Doc_Trans_Type,Location_Code,Doc_Prfeix,Next_Number,Separator, Is_Change_Monthly,Curr_Month,Is_Change_Daily,Curr_Date,dontDisplayYearInSeries,MinSizeofSeries,Year_Separator,Short_Fiscal_Year from TSPL_DOCPREFIX_MASTER LEFT JOIN TSPL_DOCUMENT_TYPE ON TSPL_DOCUMENT_TYPE.Doc_Type=TSPL_DOCPREFIX_MASTER.Doc_Type where TSPL_DOCUMENT_TYPE.Master_Prefix=0 and Fin_Year='" + frm.strFiscalYear + "' " & _
                " union all " & _
                " select Doc_Type,Doc_Trans_Type,Segment_code as Location_Code,'' as Doc_Prfeix,0 as Next_Number,'' as Separator,0 as Is_Change_Monthly,0 as Curr_Month,0 as Is_Change_Daily,null as Curr_Date,0 as dontDisplayYearInSeries,0 as MinSizeofSeries,'' as Year_Separator,0 as Short_Fiscal_Year   from TSPL_DOCUMENT_TYPE,TSPL_GL_SEGMENT_CODE" & _
                " where TSPL_DOCUMENT_TYPE.Is_Location_Wise=1 and TSPL_GL_SEGMENT_CODE.Seg_No='7' and TSPL_GL_SEGMENT_CODE.GIT='N' and TSPL_DOCUMENT_TYPE.Master_Prefix=0" & _
                " union all " & _
                " select Doc_Type,Doc_Trans_Type,TSPL_STATE_MASTER.STATE_CODE as Location_Code,'' as Doc_Prfeix,0 as Next_Number,'' as Separator,0 as Is_Change_Monthly,0 as Curr_Month,0 as Is_Change_Daily,null as Curr_Date,0 as dontDisplayYearInSeries,0 as MinSizeofSeries,'' as Year_Separator,0 as Short_Fiscal_Year   from TSPL_DOCUMENT_TYPE,TSPL_STATE_MASTER " & _
                " where TSPL_DOCUMENT_TYPE.Is_State_Wise=1 and TSPL_DOCUMENT_TYPE.Master_Prefix=0" & _
                " union all select Doc_Type,Doc_Trans_Type,'' as Location_Code,'' as Doc_Prfeix,0 as Next_Number,'' as Separator,0 as Is_Change_Monthly,0 as Curr_Month,0 as Is_Change_Daily,null as Curr_Date,0 as dontDisplayYearInSeries,0 as MinSizeofSeries,'' as Year_Separator,0 as Short_Fiscal_Year   from TSPL_DOCUMENT_TYPE  where TSPL_DOCUMENT_TYPE.Is_State_Wise=0 and TSPL_DOCUMENT_TYPE.Is_Location_Wise=0 and TSPL_DOCUMENT_TYPE.Master_Prefix=0 " & _
                " ) xxx	" & _
                " group by Doc_Type,Doc_Trans_Type,Location_Code )xxxx"

                transportSql.ExporttoExcelWithoutFilter(str, "", "Doc_Type, Doc_Trans_Type, Location_Code", Me)
            End If
        Else
            Dim str As String = "select * from ( select Doc_Type,Doc_Trans_Type,Location_Code,max(Doc_Prfeix) as Doc_Prfeix,max(Next_Number) as Next_Number,max(Separator) as Separator,MAX( Is_Change_Monthly) as Is_Change_Monthly,max(Curr_Month) as Curr_Month,max(Is_Change_Daily) as Is_Change_Daily,max(Curr_Date) as Curr_Date,max(dontDisplayYearInSeries) as dontDisplayYearInSeries,max(MinSizeofSeries) as MinSizeofSeries,max(Year_Separator) as Year_Separator,max(Short_Fiscal_Year) as Short_Fiscal_Year from " & _
                           "( select TSPL_DOCPREFIX_MASTER.Doc_Type,TSPL_DOCPREFIX_MASTER.Doc_Trans_Type,Location_Code,Doc_Prfeix,Next_Number,Separator, Is_Change_Monthly,Curr_Month,Is_Change_Daily,Curr_Date,dontDisplayYearInSeries,MinSizeofSeries,Year_Separator,Short_Fiscal_Year from TSPL_DOCPREFIX_MASTER LEFT JOIN TSPL_DOCUMENT_TYPE ON TSPL_DOCUMENT_TYPE.Doc_Type=TSPL_DOCPREFIX_MASTER.Doc_Type where TSPL_DOCUMENT_TYPE.Master_Prefix=1 " & _
                           " union all " & _
                           " select Doc_Type,Doc_Trans_Type,Segment_code as Location_Code,'' as Doc_Prfeix,0 as Next_Number,'' as Separator,0 as Is_Change_Monthly,0 as Curr_Month,0 as Is_Change_Daily,null as Curr_Date,0 as dontDisplayYearInSeries,0 as MinSizeofSeries,'' as Year_Separator,0 as Short_Fiscal_Year   from TSPL_DOCUMENT_TYPE,TSPL_GL_SEGMENT_CODE" & _
                           " where TSPL_DOCUMENT_TYPE.Is_Location_Wise=1 and TSPL_GL_SEGMENT_CODE.Seg_No='7' and TSPL_GL_SEGMENT_CODE.GIT='N' and TSPL_DOCUMENT_TYPE.Master_Prefix=1" & _
                           " union all " & _
                           " select Doc_Type,Doc_Trans_Type,TSPL_STATE_MASTER.STATE_CODE as Location_Code,'' as Doc_Prfeix,0 as Next_Number,'' as Separator,0 as Is_Change_Monthly,0 as Curr_Month,0 as Is_Change_Daily,null as Curr_Date,0 as dontDisplayYearInSeries,0 as MinSizeofSeries,'' as Year_Separator,0 as Short_Fiscal_Year   from TSPL_DOCUMENT_TYPE,TSPL_STATE_MASTER " & _
                           " where TSPL_DOCUMENT_TYPE.Is_State_Wise=1 and TSPL_DOCUMENT_TYPE.Master_Prefix=1 " & _
                           " union all select Doc_Type,Doc_Trans_Type,'' as Location_Code,'' as Doc_Prfeix,0 as Next_Number,'' as Separator,0 as Is_Change_Monthly,0 as Curr_Month,0 as Is_Change_Daily,null as Curr_Date,0 as dontDisplayYearInSeries,0 as MinSizeofSeries,'' as Year_Separator,0 as Short_Fiscal_Year   from TSPL_DOCUMENT_TYPE  where TSPL_DOCUMENT_TYPE.Is_State_Wise=0 and TSPL_DOCUMENT_TYPE.Is_Location_Wise=0 and TSPL_DOCUMENT_TYPE.Master_Prefix=1 " & _
                           " ) xxx	" & _
                           " group by Doc_Type,Doc_Trans_Type,Location_Code )xxxx"

            transportSql.ExporttoExcelWithoutFilter(str, "", "Doc_Type, Doc_Trans_Type, Location_Code", Me)
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim frm As New FrmPrefixImport()
        If chkFinYear.Checked = True Then
            frm.cboFiscalYear.Enabled = True
        Else
            frm.cboFiscalYear.Enabled = False
        End If
        frm.ShowDialog()
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub chkFinYear_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkFinYear.ToggleStateChanged
        btnReset.PerformClick()
        If chkFinYear.Checked = True Then
            cboFiscalYear.Enabled = True
        Else
            cboFiscalYear.Enabled = False
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtDocument.Value = ""
        LoadBlankGrid()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If clsCommon.myLen(txtDocument.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select document type.", Me.Text)
            txtDocument.Select()
            Exit Sub
        End If
        LoadData()
    End Sub

    Private Sub txtDocument__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocument._MYValidating
        Dim qry As String = "select Distinct(Doc_Type) as  Code from TSPL_DOCUMENT_TYPE"
        Dim StrWhere As String = ""
        If chkFinYear.Checked = True Then
            StrWhere = "Master_Prefix=0"
        Else
            StrWhere = "Master_Prefix=1"
        End If
        txtDocument.Value = clsCommon.ShowSelectForm("PREDOCFndr", qry, "Code", StrWhere, txtDocument.Value, "Code", isButtonClicked)
        LoadBlankGrid()
    End Sub

    Private Sub cboFiscalYear_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboFiscalYear.SelectedIndexChanged
        If isLoadForm = False Then
            LoadBlankGrid()
        End If
    End Sub

End Class

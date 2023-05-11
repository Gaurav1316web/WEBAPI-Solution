Imports common
Imports System.Data.SqlClient
Imports System.IO
'' Changed by Panch Raj against Ticket:BM00000009827
Public Class frmIncentiveMaster
    Inherits FrmMainTranScreen
    Const colLineNo As String = "colLineNo"
    Const colINCENTIVE_TYPE As String = "colINCENTIVE_TYPE"
    Const colSLAB_FROM As String = "colSLAB_FROM"
    Const colSLAB_TO As String = "colSLAB_TO"

    Const colFATFrom As String = "colFATFrom"
    Const colFATTo As String = "colFATTo"
    Const colSNFFrom As String = "colSNFFrom"
    Const colSNFTo As String = "colSNFTo"


    Const colTS_FROM As String = "colTS_FROM"
    Const colTS_TO As String = "colTS_TO"

    Const colRATE As String = "colRATE"
    Const colRATE_UOM As String = "colRATE_UOM"
    Const colFOR_PERIOD As String = "colFOR_PERIOD"
    Const colParameterType As String = "colParameterType"
    Const colOperaterType As String = "colOperaterType"
    Const colType As String = "colType"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim obj As New clsIncentiveMaster
    Private ObjList As New List(Of clsIncentiveMasterDetail)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog


    Sub LoadGridColumns()
        gvPP.Rows.Clear()
        gvPP.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim INCENTIVE_TYPE As New GridViewComboBoxColumn
        Dim SLAB_FROM As New GridViewDecimalColumn
        Dim SLAB_TO As New GridViewDecimalColumn
        Dim TS_FROM As New GridViewDecimalColumn
        Dim TS_TO As New GridViewDecimalColumn
        Dim RATE As New GridViewDecimalColumn
        Dim RATE_UOM As New GridViewTextBoxColumn
        Dim FOR_PERIOD As New GridViewTextBoxColumn

        Dim ParameterType As New GridViewComboBoxColumn
        Dim OperaterType As New GridViewComboBoxColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(LineNo)

        INCENTIVE_TYPE.FormatString = ""
        INCENTIVE_TYPE.HeaderText = "Incentive Type"
        INCENTIVE_TYPE.Name = colINCENTIVE_TYPE
        INCENTIVE_TYPE.Width = 100
        INCENTIVE_TYPE.ReadOnly = True
        'INCENTIVE_TYPE.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        INCENTIVE_TYPE.DataSource = LoadIncentiveType()
        INCENTIVE_TYPE.ValueMember = "Code"
        INCENTIVE_TYPE.DisplayMember = "Name"
        gvPP.Columns.Add(INCENTIVE_TYPE)

        ParameterType.FormatString = ""
        ParameterType.HeaderText = "Parameter Type"
        ParameterType.Name = colParameterType
        ParameterType.Width = 100
        ParameterType.ReadOnly = False        
        ParameterType.DataSource = loadParameterTypeGrid()
        ParameterType.ValueMember = "Code"
        ParameterType.DisplayMember = "Name"
        gvPP.Columns.Add(ParameterType)

        SLAB_FROM.FormatString = ""
        SLAB_FROM.HeaderText = "From"
        SLAB_FROM.Name = colSLAB_FROM
        SLAB_FROM.Width = 100
        SLAB_FROM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(SLAB_FROM)

        SLAB_TO.FormatString = ""
        SLAB_TO.HeaderText = "To"
        SLAB_TO.Name = colSLAB_TO
        SLAB_TO.Width = 100
        SLAB_TO.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(SLAB_TO)




        TS_FROM.FormatString = ""
        TS_FROM.HeaderText = "TS From"
        TS_FROM.Name = colTS_FROM
        TS_FROM.Width = 100
        TS_FROM.IsVisible = False
        TS_FROM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(TS_FROM)

        TS_TO.FormatString = ""
        TS_TO.HeaderText = "TS To"
        TS_TO.Name = colTS_TO
        TS_TO.Width = 100
        TS_TO.IsVisible = False
        TS_TO.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(TS_TO)

        TS_FROM = New GridViewDecimalColumn
        TS_FROM.FormatString = ""
        TS_FROM.HeaderText = "From FAT %"
        TS_FROM.Name = colFATFrom
        TS_FROM.Width = 100
        TS_FROM.IsVisible = False
        TS_FROM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(TS_FROM)

        TS_TO = New GridViewDecimalColumn
        TS_TO.FormatString = ""
        TS_TO.HeaderText = "To FAT %"
        TS_TO.Name = colFATTo
        TS_TO.Width = 100
        TS_TO.IsVisible = False
        TS_TO.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(TS_TO)

        TS_FROM = New GridViewDecimalColumn
        TS_FROM.FormatString = ""
        TS_FROM.HeaderText = "From SNF %"
        TS_FROM.Name = colSNFFrom
        TS_FROM.Width = 100
        TS_FROM.IsVisible = False
        TS_FROM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(TS_FROM)

        TS_TO = New GridViewDecimalColumn
        TS_TO.FormatString = ""
        TS_TO.HeaderText = "To SNF %"
        TS_TO.Name = colSNFTo
        TS_TO.Width = 100
        TS_TO.IsVisible = False
        TS_TO.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(TS_TO)

        RATE.FormatString = ""
        RATE.HeaderText = "Rate"
        RATE.Name = colRATE
        RATE.Width = 100
        RATE.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(RATE)

        RATE_UOM.FormatString = ""
        RATE_UOM.HeaderText = "Rate UOM"
        RATE_UOM.Name = colRATE_UOM
        RATE_UOM.Width = 120
        RATE_UOM.ReadOnly = False
        RATE_UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPP.Columns.Add(RATE_UOM)

        FOR_PERIOD.FormatString = ""
        FOR_PERIOD.HeaderText = "For Period"
        FOR_PERIOD.Name = colFOR_PERIOD
        FOR_PERIOD.Width = 130
        FOR_PERIOD.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        FOR_PERIOD.ReadOnly = True
        FOR_PERIOD.IsVisible = False
        gvPP.Columns.Add(FOR_PERIOD)

        OperaterType.FormatString = ""
        OperaterType.HeaderText = "Operater Type"
        OperaterType.Name = colOperaterType
        OperaterType.Width = 100
        OperaterType.ReadOnly = False
        OperaterType.DataSource = loadOperaterTypeGrid()
        OperaterType.ValueMember = "Code"
        OperaterType.DisplayMember = "Name"
        gvPP.Columns.Add(OperaterType)

        OperaterType = New GridViewComboBoxColumn
        OperaterType.FormatString = ""
        OperaterType.HeaderText = "Type"
        OperaterType.Name = colType
        OperaterType.Width = 100
        OperaterType.ReadOnly = False
        OperaterType.DataSource = clsMilkReceiptMCC.GetMilkType()
        OperaterType.ValueMember = "Code"
        OperaterType.DisplayMember = "Name"
        gvPP.Columns.Add(OperaterType)

    End Sub


    Private Sub frmIncentiveMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()

        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmIncentiveMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim coll = New Dictionary(Of String, String)()
        'coll.Add("FAT_FROM", "decimal(18,2) null")
        'coll.Add("FAT_TO", "decimal(18,2) null")
        'coll.Add("SNF_FROM", "decimal(18,2) null")
        'coll.Add("SNF_TO", "decimal(18,2) null")
        'coll.Add("Type", "Varchar(1)  null")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_INCENTIVE_DETAIL", coll, Nothing, False, False)



        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")

        funReset()

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmIncentiveMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()

        Me.txtDescription.Text = ""

        Dim serverDate As Date
        serverDate = clsCommon.GETSERVERDATE
        isCellValueChangedOpen = True
        Me.cboIncentiveType.DataSource = loadincentiveType()
        isCellValueChangedOpen = False
        Me.cboIncentiveType.ValueMember = "Code"
        Me.cboIncentiveType.DisplayMember = "Name"

        Me.cboSchemeFor.DataSource = loadSchemeForType()
        Me.cboSchemeFor.ValueMember = "Code"
        Me.cboSchemeFor.DisplayMember = "Name"        
        Me.cboSchemeFor.SelectedValue = "PC"
        Me.cboSchemeFor.Enabled = False


        Me.CmbCalculationFlatorAvg.DataSource = LoadCalculationType()
        Me.CmbCalculationFlatorAvg.ValueMember = "Code"
        Me.CmbCalculationFlatorAvg.DisplayMember = "Name"


        Me.CmbrateType.DataSource = LoadRateType()
        Me.CmbrateType.ValueMember = "Code"
        Me.CmbrateType.DisplayMember = "Name"

        Me.ddlQtyType.DataSource = LoadQuantityType()
        Me.ddlQtyType.ValueMember = "Code"
        Me.ddlQtyType.DisplayMember = "Name"

        Me.dtpIncentiveDate.Value = serverDate
        Me.dtpStartDate.Value = serverDate
        Me.dtpEndDate.Value = serverDate
        'Me.cboSchemeFor.SelectedIndex = -1
        Me.txtDescription.Text = ""
        Me.cboIncentiveType.SelectedIndex = -1
        Me.CmbCalculationFlatorAvg.SelectedValue = "F" ''TEC/17/05/19-000508 by balwinder on 24/06/2019
        Me.CmbrateType.SelectedIndex = -1
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        Me.CmbEndingShift.DataSource = ClsOpenMCCShift.GetShift
        Me.CmbEndingShift.DisplayMember = "Name"
        Me.CmbEndingShift.ValueMember = "Code"
        Me.CmbEndingShift.SelectedIndex = -1
        Me.CmbSTartingShift.DataSource = ClsOpenMCCShift.GetShift
        Me.CmbSTartingShift.DisplayMember = "Name"
        Me.CmbSTartingShift.ValueMember = "Code"
        Me.CmbSTartingShift.SelectedIndex = -1
        Me.ddlQtyType.SelectedValue = "ACTQ"
        ReStoreGridLayout()

        Me.gvPP.Rows.Clear()
        Me.gvPP.Rows.AddNew()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(Me.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Me.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvPP.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvPP.Columns.Count - 1 Step ii + 1
                        gvPP.Columns(ii).IsVisible = False
                        gvPP.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvPP.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        obj = clsIncentiveMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.INCENTIVE_CODE) > 0) Then

            isNewEntry = False
            btnsave.Text = "Update"

            Dim ii As Int16 = 0
            'LoadGridColumns()
            txtCode.Value = obj.INCENTIVE_CODE
            Me.txtDescription.Text = clsCommon.myCstr(obj.DESCRIPTION)
            Me.dtpIncentiveDate.Value = obj.INCENTIVE_DATE
            Me.dtpStartDate.Value = obj.START_DATE
            Me.dtpEndDate.Value = obj.END_DATE
            Me.cboIncentiveType.SelectedValue = obj.INCENTIVE_TYPE
            Me.cboSchemeFor.SelectedValue = obj.SCHEME_FOR
            Me.CmbCalculationFlatorAvg.SelectedValue = obj.Calc_Type
            Me.CmbrateType.SelectedValue = obj.Rate_type
            Me.CmbSTartingShift.SelectedValue = obj.Starting_Shift
            Me.CmbEndingShift.SelectedValue = obj.Ending_Shift
            Me.ddlQtyType.SelectedValue = obj.Qty_Type
            gvPP.Rows.Clear()
            If (clsIncentiveMaster.ObjList IsNot Nothing AndAlso clsIncentiveMaster.ObjList.Count > 0) Then
                For Each objTr As clsIncentiveMasterDetail In clsIncentiveMaster.ObjList
                    gvPP.Rows.AddNew()

                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colLineNo).Value = objTr.LINE_NO
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colINCENTIVE_TYPE).Value = objTr.INCENTIVE_TYPE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colRATE_UOM).Value = objTr.RATE_UOM
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colRATE).Value = objTr.RATE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colFOR_PERIOD).Value = objTr.FOR_PERIOD
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colSLAB_FROM).Value = objTr.SLAB_FROM
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colSLAB_TO).Value = objTr.SLAB_TO

                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colTS_FROM).Value = objTr.TS_FROM
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colTS_TO).Value = objTr.TS_TO


                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colFATFrom).Value = objTr.FAT_FROM
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colFATTo).Value = objTr.FAT_TO
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colSNFFrom).Value = objTr.SNF_FROM
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colSNFTo).Value = objTr.SNF_TO
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colType).Value = objTr.Type

                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colParameterType).Value = objTr.Parameter_Type
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colOperaterType).Value = objTr.OPERATER_TYPE

                Next
            Else
                gvPP.Rows.AddNew()
            End If
        End If

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmIncentiveMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return False
                End If
            End If

            Dim obj As New clsIncentiveMaster
            obj.INCENTIVE_CODE = Me.txtCode.Value
            obj.DESCRIPTION = Me.txtDescription.Text
            obj.INCENTIVE_TYPE = Me.cboIncentiveType.SelectedValue
            obj.INCENTIVE_DATE = Me.dtpIncentiveDate.Value
            obj.START_DATE = Me.dtpStartDate.Value
            obj.END_DATE = Me.dtpEndDate.Value
            obj.SCHEME_FOR = Me.cboSchemeFor.SelectedValue
            obj.Calc_Type = Me.CmbCalculationFlatorAvg.SelectedValue
            obj.Rate_type = Me.CmbrateType.SelectedValue
            obj.Starting_Shift = Me.CmbSTartingShift.SelectedValue
            obj.Ending_Shift = Me.CmbEndingShift.SelectedValue
            obj.Qty_Type = Me.ddlQtyType.SelectedValue

            Dim obj1 As clsIncentiveMasterDetail
            ObjList = New List(Of clsIncentiveMasterDetail)
            For Each grow As GridViewRowInfo In gvPP.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLineNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colINCENTIVE_TYPE).Value)) > 0 And clsCommon.myLen(grow.Cells(colSLAB_FROM).Value) > 0 Then
                    obj1 = New clsIncentiveMasterDetail()

                    obj1.INCENTIVE_CODE = txtCode.Value
                    obj1.LINE_NO = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    obj1.INCENTIVE_TYPE = clsCommon.myCstr(grow.Cells(colINCENTIVE_TYPE).Value)
                    obj1.RATE_UOM = clsCommon.myCstr(grow.Cells(colRATE_UOM).Value)
                    obj1.RATE = clsCommon.myCstr(grow.Cells(colRATE).Value)
                    obj1.FOR_PERIOD = Me.cboSchemeFor.SelectedValue
                    obj1.SLAB_FROM = clsCommon.myCdbl(grow.Cells(colSLAB_FROM).Value)
                    obj1.SLAB_TO = clsCommon.myCdbl(grow.Cells(colSLAB_TO).Value)
                    obj1.TS_FROM = clsCommon.myCdbl(grow.Cells(colTS_FROM).Value)
                    obj1.TS_TO = clsCommon.myCdbl(grow.Cells(colTS_TO).Value)

                    obj1.FAT_FROM = clsCommon.myCdbl(grow.Cells(colFATFrom).Value)
                    obj1.FAT_TO = clsCommon.myCdbl(grow.Cells(colFATTo).Value)
                    obj1.SNF_FROM = clsCommon.myCdbl(grow.Cells(colSNFFrom).Value)
                    obj1.SNF_TO = clsCommon.myCdbl(grow.Cells(colSNFTo).Value)
                    obj1.Type = clsCommon.myCstr(grow.Cells(colType).Value)

                    obj1.Parameter_Type = clsCommon.myCstr(grow.Cells(colParameterType).Value)
                    obj1.OPERATER_TYPE = clsCommon.myCstr(grow.Cells(colOperaterType).Value)
                    ObjList.Add(obj1)
                End If
            Next
            Dim issaved As Boolean = False
            issaved = obj.SaveData(obj, ObjList, isNewEntry)
            If issaved = True Then
                'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                LoadData(obj.INCENTIVE_CODE, NavigatorType.Current)
                Return True
            End If

            Return False
        End If
    End Function
    Function loadSchemeForType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Day"
        dr("Name") = "Day"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Month"
        dr("Name") = "Month"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Year"
        dr("Name") = "Year"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "PC"
        dr("Name") = "Current Payment Cycle"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "PPC"
        dr("Name") = "Previous Payment Cycle"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "DPC"
        dr("Name") = "Difference of Payment Cycle" '======Difference of Current Payment Cycle and previous Payment Cycle.
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt

    End Function

    Function LoadIncentiveType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "QB"
        dr("Name") = "Quantity Based"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "QSLAB"
        dr("Name") = "Slab Based"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "TSB"
        dr("Name") = "TS Based"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "QTSSLAB"
        dr("Name") = "Quantity and TS Based"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "QSLABTSSLAB"
        dr("Name") = "Slab and TS Based"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "QLTY"
        dr("Name") = "Quality Based"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SLQLTYTYPE"
        dr("Name") = "Slab,Quality and Type Based"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt

    End Function

    Function LoadCalculationType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Flat"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Avg."
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt

    End Function

    Function LoadRateType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "FAT Rate"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Q"
        dr("Name") = "Quantitative"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt

    End Function
    Function LoadQuantityType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "ACTQ"
        dr("Name") = "Actual Quantity"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "STDQ"
        dr("Name") = "Standard Quantity"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt

    End Function


    Function loadParameterTypeGrid() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "FAT"
        dr("Name") = "FAT"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SNF"
        dr("Name") = "SNF"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CLR"
        dr("Name") = "CLR"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt

    End Function
    Function loadOperaterTypeGrid() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "None"
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Continue"
        dr("Name") = "Continue"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OR"
        dr("Name") = "OR"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "XOR"
        dr("Name") = "XOR"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AND"
        dr("Name") = "AND"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt

    End Function
    Function AllowToSave() As Boolean
        If clsCommon.myLen(ddlQtyType.Text) <= 0 Then
            myMessages.blankValue("Quantity Type")
            ddlQtyType.Focus()
            Return False
        End If
        If clsCommon.myLen(cboIncentiveType.Text) <= 0 Then
            myMessages.blankValue("Incentive Type")
            txtCode.Focus()
            Return False
        End If
        If clsCommon.myLen(cboSchemeFor.Text) <= 0 Then
            myMessages.blankValue("Scheme For")
            cboSchemeFor.Focus()
            Return False
        End If
        If clsCommon.myLen(CmbSTartingShift.SelectedValue) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Starting Shift", Me.Text)
            CmbSTartingShift.Focus()
            Return False
        End If
        If clsCommon.myLen(CmbEndingShift.SelectedValue) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Ending Shift", Me.Text)
            CmbEndingShift.Focus()
            Return False
        End If
        If clsCommon.myLen(CmbCalculationFlatorAvg.SelectedValue) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Calculation Type", Me.Text)
            CmbCalculationFlatorAvg.Focus()
            Return False
        End If
        If clsCommon.CompairString(cboIncentiveType.SelectedValue, "QLTY") = CompairStringResult.Equal Then
            Return True
        End If
        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvPP.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colINCENTIVE_TYPE).Value)) > 0 And clsCommon.myLen(grow.Cells(colSLAB_FROM).Value) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colINCENTIVE_TYPE).Value), "SLQLTYTYPE") = CompairStringResult.Equal Then
                    If gvPP.Columns(colFATTo).IsVisible Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFATTo).Value)) <= 0 And gvPP.Columns(colFATTo).IsVisible Then
                            myMessages.blankValue("TS From Value is blank at row no " & (grow.Index + 1) & "")
                            Return False
                        End If
                        If clsCommon.myCdbl(grow.Cells(colFATTo).Value) <= clsCommon.myCdbl(grow.Cells(colFATFrom).Value) Then
                            myMessages.blankValue("FAT To Value should be Greater than FAT From Value at row no " & (grow.Index + 1) & "")
                        End If
                    End If
                    If gvPP.Columns(colSNFTo).IsVisible Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSNFTo).Value)) <= 0 And gvPP.Columns(colSNFTo).IsVisible Then
                            myMessages.blankValue("TS From Value is blank at row no " & (grow.Index + 1) & "")
                            Return False
                        End If
                        If clsCommon.myCdbl(grow.Cells(colSNFTo).Value) <= clsCommon.myCdbl(grow.Cells(colSNFFrom).Value) Then
                            myMessages.blankValue("SNF To Value should be Greater than SNF From Value at row no " & (grow.Index + 1) & "")
                        End If
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colType).Value)) <= 0 Then
                        myMessages.blankValue("Type Value is blank at row no " & (grow.Index + 1) & "")
                        Return False
                    End If
                Else
                    For Each growIn As GridViewRowInfo In gvPP.Rows
                        If grow.Index <> growIn.Index Then
                            Dim ParamType1 As String = growIn.Cells(colParameterType).Value
                            Dim ParamType2 As String = grow.Cells(colParameterType).Value
                            Dim Operater1 As String = growIn.Cells(colOperaterType).Value
                            Dim Operater2 As String = grow.Cells(colOperaterType).Value
                            If gvPP.Columns(colTS_TO).IsVisible Then
                                If clsCommon.myLen(clsCommon.myCstr(growIn.Cells(colINCENTIVE_TYPE).Value)) > 0 And ParamType1 = ParamType2 And Operater1 = Operater2 And clsCommon.myCdbl(grow.Cells(colTS_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colTS_FROM).Value) And clsCommon.myCdbl(grow.Cells(colTS_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colTS_FROM).Value) And clsCommon.myCstr(grow.Cells(colRATE_UOM).Value) = clsCommon.myCstr(growIn.Cells(colRATE_UOM).Value) Then
                                    If clsCommon.myLen(clsCommon.myCstr(growIn.Cells(colINCENTIVE_TYPE).Value)) > 0 And clsCommon.myCdbl(grow.Cells(colSLAB_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colSLAB_FROM).Value) And clsCommon.myCdbl(grow.Cells(colSLAB_TO).Value) > clsCommon.myCdbl(growIn.Cells(colSLAB_FROM).Value) And clsCommon.myCstr(grow.Cells(colRATE_UOM).Value) = clsCommon.myCstr(growIn.Cells(colRATE_UOM).Value) Then
                                        clsCommon.MyMessageBoxShow("Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                        Return False
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(growIn.Cells(colINCENTIVE_TYPE).Value)) > 0 And ParamType1 = ParamType2 And Operater1 = Operater2 And clsCommon.myCdbl(grow.Cells(colSLAB_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colSLAB_TO).Value) And clsCommon.myCdbl(grow.Cells(colSLAB_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colSLAB_TO).Value) And gvPP.Columns(colSLAB_TO).IsVisible And clsCommon.myCstr(grow.Cells(colRATE_UOM).Value) = clsCommon.myCstr(growIn.Cells(colRATE_UOM).Value) Then
                                        clsCommon.MyMessageBoxShow("Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                        Return False
                                    End If
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(growIn.Cells(colINCENTIVE_TYPE).Value)) > 0 And ParamType1 = ParamType2 And Operater1 = Operater2 And clsCommon.myCdbl(grow.Cells(colTS_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colTS_TO).Value) And clsCommon.myCdbl(grow.Cells(colTS_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colTS_TO).Value) And gvPP.Columns(colTS_TO).IsVisible And clsCommon.myCstr(grow.Cells(colRATE_UOM).Value) = clsCommon.myCstr(growIn.Cells(colRATE_UOM).Value) Then
                                    If clsCommon.myLen(clsCommon.myCstr(growIn.Cells(colINCENTIVE_TYPE).Value)) > 0 And clsCommon.myCdbl(grow.Cells(colSLAB_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colSLAB_FROM).Value) And clsCommon.myCdbl(grow.Cells(colSLAB_TO).Value) > clsCommon.myCdbl(growIn.Cells(colSLAB_FROM).Value) And clsCommon.myCstr(grow.Cells(colRATE_UOM).Value) = clsCommon.myCstr(growIn.Cells(colRATE_UOM).Value) Then
                                        clsCommon.MyMessageBoxShow("Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                        Return False
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(growIn.Cells(colINCENTIVE_TYPE).Value)) > 0 And ParamType1 = ParamType2 And Operater1 = Operater2 And clsCommon.myCdbl(grow.Cells(colSLAB_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colSLAB_TO).Value) And clsCommon.myCdbl(grow.Cells(colSLAB_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colSLAB_TO).Value) And gvPP.Columns(colSLAB_TO).IsVisible And clsCommon.myCstr(grow.Cells(colRATE_UOM).Value) = clsCommon.myCstr(growIn.Cells(colRATE_UOM).Value) Then
                                        clsCommon.MyMessageBoxShow("Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                        Return False
                                    End If
                                End If
                                'End If
                            Else
                                If clsCommon.myLen(clsCommon.myCstr(growIn.Cells(colINCENTIVE_TYPE).Value)) > 0 And ParamType1 = ParamType2 And Operater1 = Operater2 And ((clsCommon.myCdbl(grow.Cells(colSLAB_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colSLAB_FROM).Value) And clsCommon.myCdbl(grow.Cells(colSLAB_TO).Value) > clsCommon.myCdbl(growIn.Cells(colSLAB_FROM).Value)) Or clsCommon.myCdbl(grow.Cells(colSLAB_FROM).Value) = clsCommon.myCdbl(growIn.Cells(colSLAB_FROM).Value)) And clsCommon.myCstr(grow.Cells(colRATE_UOM).Value) = clsCommon.myCstr(growIn.Cells(colRATE_UOM).Value) Then
                                    clsCommon.MyMessageBoxShow("Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                    Return False
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(growIn.Cells(colINCENTIVE_TYPE).Value)) > 0 And ParamType1 = ParamType2 And Operater1 = Operater2 And clsCommon.myCdbl(grow.Cells(colSLAB_FROM).Value) < clsCommon.myCdbl(growIn.Cells(colSLAB_TO).Value) And clsCommon.myCdbl(grow.Cells(colSLAB_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colSLAB_TO).Value) And gvPP.Columns(colSLAB_TO).IsVisible And clsCommon.myCstr(grow.Cells(colRATE_UOM).Value) = clsCommon.myCstr(growIn.Cells(colRATE_UOM).Value) Then
                                    clsCommon.MyMessageBoxShow("Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                    Return False
                                End If

                            End If
                        End If
                    Next
                End If

                If gvPP.Columns(colTS_FROM).IsVisible Then
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colTS_FROM).Value)) <= 0 And gvPP.Columns(colTS_FROM).IsVisible Then
                        myMessages.blankValue("TS From Value is blank at row no " & (grow.Index + 1) & "")
                        Return False
                    End If
                End If

                If gvPP.Columns(colTS_TO).IsVisible Then
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colTS_TO).Value)) <= 0 And gvPP.Columns(colTS_TO).IsVisible Then
                        myMessages.blankValue("TS From Value is blank at row no " & (grow.Index + 1) & "")
                        Return False
                    End If
                End If


                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSLAB_TO).Value)) <= 0 And gvPP.Columns(colSLAB_TO).IsVisible Then
                    myMessages.blankValue("TS From Value is blank at row no " & (grow.Index + 1) & "")
                    Return False
                End If

                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colRATE).Value)) <= 0 Then
                    myMessages.blankValue("Rate is blank at row no " & (grow.Index + 1) & "")
                    Return False
                End If

                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colRATE_UOM).Value)) <= 0 Then
                    myMessages.blankValue("Rate UOM is blank at row no " & (grow.Index + 1) & "")
                    Return False
                End If




                ii += 1
            End If
        Next
        Return True
    End Function



    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
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
                If (clsIncentiveMaster.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function


    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim check As Boolean = False
        check = clsIncentiveMaster.CheckValidCode(Me.txtCode.Value)

        If check Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsIncentiveMaster.GetFinder("", txtCode.Value, isButtonClicked))
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            funReset()
        End If


    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        Try
            If (Save()) Then
                If ChekBtnPost = False Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funClose()
    End Sub

    Private Sub gvPP_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvPP.CellValueChanged
        If gvPP.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If gvPP.CurrentRow.Cells(0).Value = "" Then
            gvPP.CurrentRow.Cells(0).Value = gvPP.RowCount
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvPP.Columns(colINCENTIVE_TYPE) Then
                If clsCommon.CompairString(Me.gvPP.CurrentRow.Cells(colINCENTIVE_TYPE).Value, "QB") = CompairStringResult.Equal Then
                    gvPP.CurrentRow.Cells(colSLAB_TO).Value = 0
                    gvPP.CurrentRow.Cells(colSLAB_TO).ReadOnly = True
                Else
                    gvPP.CurrentRow.Cells(colSLAB_TO).ReadOnly = False
                End If
            End If

            If e.Column Is gvPP.Columns(colRATE_UOM) Then
                Me.gvPP.CurrentRow.Cells(colRATE_UOM).Value = clsUOMInfo.GetFinder("", clsCommon.myCstr(gvPP.CurrentRow.Cells(colRATE_UOM).Value), False)
                'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Unit_Code) > 0 Then

                'End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub


    Private Sub gvBOM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvPP.KeyDown
        If e.KeyData = Keys.Enter Then
            Me.gvPP.Rows.Add(1)

            gvPP.Rows(gvPP.RowCount - 1).Cells(0).Value = gvPP.RowCount
        End If
        If e.KeyData = Keys.Right And gvPP.CurrentCell.ColumnIndex = gvPP.Columns.Count - 1 Then
            Me.gvPP.Rows.Add(1)
            gvPP.Rows(gvPP.RowCount - 1).Cells(0).Value = gvPP.RowCount
        End If
    End Sub

    Private Sub gvPP_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvPP.CurrentColumnChanged
        If gvPP.RowCount > 0 Then
            Dim intCurrRow As Integer = gvPP.CurrentRow.Index
            gvPP.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvPP.Rows.Count - 1 Then
                gvPP.Rows.AddNew()
                If clsCommon.myLen(gvPP.CurrentRow.Cells(colINCENTIVE_TYPE).Value) <= 0 Then
                    gvPP.CurrentRow.Cells(colINCENTIVE_TYPE).Value = Me.cboIncentiveType.SelectedValue
                End If

                gvPP.CurrentRow.Cells(colFOR_PERIOD).Value = Me.cboSchemeFor.SelectedValue
                gvPP.CurrentRow = gvPP.Rows(intCurrRow)
                If clsCommon.CompairString(Me.cboIncentiveType.SelectedValue, "QTSSLAB") = CompairStringResult.Equal Or clsCommon.CompairString(Me.cboIncentiveType.SelectedValue, "QSLABTSSLAB") = CompairStringResult.Equal Then
                    gvPP.CurrentRow.Cells(colINCENTIVE_TYPE).ReadOnly = False
                Else
                    gvPP.CurrentRow.Cells(colINCENTIVE_TYPE).ReadOnly = True
                    gvPP.CurrentRow.Cells(colINCENTIVE_TYPE).Value = Me.cboIncentiveType.SelectedValue
                End If
                If clsCommon.CompairString(Me.cboIncentiveType.SelectedValue, "QLTY") = CompairStringResult.Equal Then
                    gvPP.CurrentRow.Cells(colParameterType).ReadOnly = False
                Else
                    gvPP.CurrentRow.Cells(colParameterType).ReadOnly = True
                End If
            End If
        End If
    End Sub


    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(Me.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Delete layout successfully", "Information")
    End Sub

    Private Sub RadMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(Me.Form_ID) > 0 Then
            gvPP.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = Me.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvPP.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvPP.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub cboIncentiveType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboIncentiveType.SelectedIndexChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If cboIncentiveType.SelectedValue = "QTSSLAB" Then
                    gvPP.Columns(colSLAB_TO).IsVisible = False
                    gvPP.Columns(colSLAB_FROM).HeaderText = "Qty"
                    gvPP.Columns(colTS_FROM).IsVisible = True
                    gvPP.Columns(colTS_TO).IsVisible = True
                    gvPP.Columns(colParameterType).ReadOnly = True
                    gvPP.Columns(colOperaterType).ReadOnly = True
                    gvPP.Columns(colFATFrom).IsVisible = False
                    gvPP.Columns(colFATTo).IsVisible = False
                    gvPP.Columns(colSNFFrom).IsVisible = False
                    gvPP.Columns(colSNFTo).IsVisible = False
                    gvPP.Columns(colType).IsVisible = False
                ElseIf cboIncentiveType.SelectedValue = "SLQLTYTYPE" Then
                    gvPP.Columns(colSLAB_TO).IsVisible = True
                    gvPP.Columns(colSLAB_TO).HeaderText = "Slab To"
                    gvPP.Columns(colSLAB_FROM).IsVisible = True
                    gvPP.Columns(colSLAB_FROM).HeaderText = "Slab From"
                    gvPP.Columns(colTS_FROM).IsVisible = False
                    gvPP.Columns(colTS_TO).IsVisible = False
                    gvPP.Columns(colFATFrom).IsVisible = True
                    gvPP.Columns(colFATTo).IsVisible = True
                    gvPP.Columns(colSNFFrom).IsVisible = True
                    gvPP.Columns(colSNFTo).IsVisible = True
                    gvPP.Columns(colParameterType).ReadOnly = True
                    gvPP.Columns(colOperaterType).ReadOnly = True
                    gvPP.Columns(colType).IsVisible = True
                ElseIf cboIncentiveType.SelectedValue = "QSLABTSSLAB" Then
                    gvPP.Columns(colSLAB_TO).IsVisible = True
                    gvPP.Columns(colTS_FROM).IsVisible = True
                    gvPP.Columns(colTS_TO).IsVisible = True
                    gvPP.Columns(colSLAB_TO).HeaderText = "Slab To"
                    gvPP.Columns(colSLAB_FROM).HeaderText = "Slab From"
                    gvPP.Columns(colParameterType).ReadOnly = True
                    gvPP.Columns(colOperaterType).ReadOnly = True
                    gvPP.Columns(colFATFrom).IsVisible = False
                    gvPP.Columns(colFATTo).IsVisible = False
                    gvPP.Columns(colSNFFrom).IsVisible = False
                    gvPP.Columns(colSNFTo).IsVisible = False
                    gvPP.Columns(colType).IsVisible = False
                ElseIf cboIncentiveType.SelectedValue = "QB" Then
                    gvPP.Columns(colSLAB_TO).IsVisible = False
                    gvPP.Columns(colTS_FROM).IsVisible = False
                    gvPP.Columns(colTS_TO).IsVisible = False
                    gvPP.Columns(colSLAB_FROM).HeaderText = "Qty"
                    gvPP.Columns(colParameterType).ReadOnly = True
                    gvPP.Columns(colOperaterType).ReadOnly = True
                    gvPP.Columns(colFATFrom).IsVisible = False
                    gvPP.Columns(colFATTo).IsVisible = False
                    gvPP.Columns(colSNFFrom).IsVisible = False
                    gvPP.Columns(colSNFTo).IsVisible = False
                    gvPP.Columns(colType).IsVisible = False
                ElseIf cboIncentiveType.SelectedValue = "QLTY" Then                    
                    gvPP.Columns(colINCENTIVE_TYPE).ReadOnly = True
                    gvPP.Columns(colParameterType).ReadOnly = False
                    gvPP.Columns(colOperaterType).ReadOnly = False
                    gvPP.Columns(colSLAB_TO).IsVisible = True
                    gvPP.Columns(colTS_FROM).IsVisible = False
                    gvPP.Columns(colTS_TO).IsVisible = False
                    gvPP.Columns(colSLAB_TO).HeaderText = "To"
                    gvPP.Columns(colSLAB_FROM).HeaderText = "From"
                    gvPP.Columns(colFATFrom).IsVisible = False
                    gvPP.Columns(colFATTo).IsVisible = False
                    gvPP.Columns(colSNFFrom).IsVisible = False
                    gvPP.Columns(colSNFTo).IsVisible = False
                    gvPP.Columns(colType).IsVisible = False
                Else
                    gvPP.Columns(colSLAB_TO).IsVisible = True
                    gvPP.Columns(colTS_FROM).IsVisible = False
                    gvPP.Columns(colTS_TO).IsVisible = False
                    gvPP.Columns(colSLAB_TO).HeaderText = "To"
                    gvPP.Columns(colSLAB_FROM).HeaderText = "From"
                    gvPP.Columns(colParameterType).ReadOnly = True
                    gvPP.Columns(colOperaterType).ReadOnly = True
                    gvPP.Columns(colFATFrom).IsVisible = False
                    gvPP.Columns(colFATTo).IsVisible = False
                    gvPP.Columns(colSNFFrom).IsVisible = False
                    gvPP.Columns(colSNFTo).IsVisible = False
                    gvPP.Columns(colType).IsVisible = False
                End If
                isCellValueChangedOpen = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString, "cboIncentiveType_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        Dim sQuery As String = "select TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE as [Incentive Code],Description,INCENTIVE_DATE as [Incentive Date],START_DATE as [Start Date]," _
        & " end_date as [End Date],case when starting_shift='M' then 'Morning' else 'Evening' end as [Starting Shift],case when ending_shift='M' then" _
        & " 'Morning' else 'Evening' end as [Ending Shift],case when TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='QB' then 'Quantity Based' " _
         & " when  TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='QSLAB' then 'Slab Based'" _
         & " when  TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='TSB' then 'TS Based' " _
         & " when  TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='QTSSLAB' then 'Quantity And TS Based'" _
         & " when  TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_TYPE='QSLABTSSLAB' then 'Slab and TS Based' end as [Incentive Type],Case when Calc_Type='F' then 'Flat' when Calc_Type='A' then 'Avg' end as [Calculation Type]," _
        & " SCHEME_FOR as [Scheme For],case when Rate_Type='Q' then 'Quantitative' when rate_type='F' then 'Fat Rate' end as [Rate Type],case when TSPL_INCENTIVE_DETAIL.INCENTIVE_TYPE='QB' then SLAB_FROM else 0 end as [Qty]," _
        & " case when TSPL_INCENTIVE_DETAIL.INCENTIVE_TYPE<>'QB' then SLAB_FROM else 0 end as [Slab From],SLAB_TO as [Slab To],TS_FROM as [TS From]," _
        & " TS_TO as [TS To],Rate,RATE_UOM as [Rate UOM],TSPL_INCENTIVE_MASTER_HEAD.Qty_Type as [Quantity Type],TSPL_INCENTIVE_DETAIL.Parameter_Type as [Parameter Type],TSPL_INCENTIVE_DETAIL.OPERATER_TYPE as [Operater Type] from TSPL_INCENTIVE_MASTER_HEAD left join TSPL_INCENTIVE_DETAIL on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE" _
        & " =TSPL_INCENTIVE_DETAIL.INCENTIVE_CODE"
        transportSql.ExporttoExcel(sQuery, Me)
    End Sub

    Private Sub BtnImport_Click(sender As Object, e As EventArgs) Handles BtnImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Incentive Code", "Description", "Incentive Date", "Start Date", "End Date", "Starting Shift", "Ending Shift", "Incentive Type", "Calculation Type", "Scheme For", "Rate Type", "Qty", "Slab From", "Slab To", "TS From", "TS To", "Rate", "Rate UOM", "Quantity Type", "Parameter Type", "Operater Type") Then
            Try

                Dim obj As clsIncentiveMaster = Nothing

                Dim counter As Integer = 1

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    'counter += 1

                    obj = New clsIncentiveMaster()
                    obj.INCENTIVE_CODE = clsCommon.myCstr(grow.Cells("Incentive Code").Value)
                    If clsCommon.myLen(obj.INCENTIVE_CODE) <= 0 Then 'Or clsCommon.myLen(obj.code) > 30 Then
                        obj.INCENTIVE_CODE = clsERPFuncationality.GetNextCode(Nothing, dtpIncentiveDate.Value, clsDocType.IncentiveMaster, "", "") '    Throw New Exception("Length Of Price Code Should Not Exceed 30 Characters At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    obj.DESCRIPTION = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(obj.DESCRIPTION) <= 0 Or clsCommon.myLen(obj.DESCRIPTION) > 150 Then
                        Throw New Exception("Length Of Price Description Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Incentive Date").Value)) > 0 Then
                        obj.INCENTIVE_DATE = clsCommon.myCDate(grow.Cells("Incentive Date").Value)
                    Else
                        Throw New Exception("Please Enter Incentive Date At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Start Date").Value)) > 0 Then
                        obj.START_DATE = clsCommon.myCDate(grow.Cells("Start Date").Value)
                    Else
                        Throw New Exception("Please Enter Start Date At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("End Date").Value)) > 0 Then
                        obj.END_DATE = clsCommon.myCDate(grow.Cells("End Date").Value)
                    Else
                        Throw New Exception("Please End End Date At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Starting Shift").Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Starting Shift").Value), "Morning") = CompairStringResult.Equal Then
                            obj.Starting_Shift = "M"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Starting Shift").Value), "Evening") = CompairStringResult.Equal Then
                            obj.Starting_Shift = "E"
                        Else
                            Throw New Exception("Please End Starting Shift At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                    Else
                        Throw New Exception("Please End Starting Shift At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Ending Shift").Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Ending Shift").Value), "Morning") = CompairStringResult.Equal Then
                            obj.Ending_Shift = "M"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Ending Shift").Value), "Evening") = CompairStringResult.Equal Then
                            obj.Ending_Shift = "E"
                        Else
                            Throw New Exception("Please End Ending Shift At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                    Else
                        Throw New Exception("Please End Ending Shift At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Incentive Type").Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "Quantity Based") = CompairStringResult.Equal Then
                            obj.INCENTIVE_TYPE = "QB"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "Slab Based") = CompairStringResult.Equal Then
                            obj.INCENTIVE_TYPE = "QSLAB"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "TS Based") = CompairStringResult.Equal Then
                            obj.INCENTIVE_TYPE = "TSB"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "Quantity and TS Based") = CompairStringResult.Equal Then
                            obj.INCENTIVE_TYPE = "QTSSLAB"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Type").Value), "Slab and TS Based") = CompairStringResult.Equal Then
                            obj.INCENTIVE_TYPE = "QSLABTSSLAB"
                        Else
                            Throw New Exception("Please End Incentive Type At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                    Else
                        Throw New Exception("Please End Incentive Type At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Calculation Type").Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Calculation Type").Value), "Flat") = CompairStringResult.Equal Then
                            obj.Calc_Type = "F"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Calculation Type").Value), "Avg") = CompairStringResult.Equal Then
                            obj.Calc_Type = "A"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Calculation Type").Value), "Avg.") = CompairStringResult.Equal Then
                            obj.Calc_Type = "A"
                        Else
                            Throw New Exception("Please End Calculation Type At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If

                    Else
                        Throw New Exception("Please End Calculation Type At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    '' validation for Quantity Type 
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Quantity Type").Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Quantity Type").Value), "ACTQ") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Quantity Type").Value), "Actual Quantity") = CompairStringResult.Equal Then
                            obj.Calc_Type = "ACTQ"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Quantity Type").Value), "STDQ") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Quantity Type").Value), "Standard Quantity") = CompairStringResult.Equal Then
                            obj.Calc_Type = "STDQ"
                        Else
                            Throw New Exception("Please enter valid Quantity Type At Line No. " + clsCommon.myCstr(counter) + ".")
                        End If
                    Else
                        Throw New Exception("Please enter Quantity Type At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Scheme For").Value)) > 0 Then
                        obj.SCHEME_FOR = clsCommon.myCstr(grow.Cells("Scheme For").Value)
                    Else
                        Throw New Exception("Please End Scheme For At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Rate Type").Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rate Type").Value), "FAT Rate") = CompairStringResult.Equal Then
                            obj.Rate_type = "F"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rate Type").Value), "Quantitative") = CompairStringResult.Equal Then
                            obj.Rate_type = "Q"
                        End If
                    Else
                        Throw New Exception("Please Enter Rate Type At Line No. " + clsCommon.myCstr(counter) + ".")
                    End If
                    '===============Detail=============================================
                    Dim obj1 As clsIncentiveMasterDetail
                    ObjList = New List(Of clsIncentiveMasterDetail)
                    Dim line_No As Integer = 1
                    For Each grows As GridViewRowInfo In gv.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Incentive Code").Value), clsCommon.myCstr(grows.Cells("Incentive Code").Value)) = CompairStringResult.Equal Then
                            obj1 = New clsIncentiveMasterDetail()

                            obj1.INCENTIVE_CODE = obj.INCENTIVE_CODE
                            obj1.LINE_NO = line_No 'clsCommon.myCdbl(grows.Cells(colLineNo).Value)
                            obj1.INCENTIVE_TYPE = clsCommon.myCstr(grows.Cells("Incentive Type").Value)
                            obj1.RATE_UOM = clsCommon.myCstr(grows.Cells("Rate Uom").Value)
                            obj1.RATE = clsCommon.myCstr(grows.Cells("Rate").Value)
                            obj1.FOR_PERIOD = clsCommon.myCstr(grows.Cells("Scheme For").Value)

                            obj1.SLAB_TO = clsCommon.myCdbl(grows.Cells("Slab To").Value)

                            obj1.INCENTIVE_TYPE = obj.INCENTIVE_TYPE
                            obj1.TS_FROM = clsCommon.myCdbl(grows.Cells("TS From").Value)
                            obj1.TS_TO = clsCommon.myCdbl(grows.Cells("TS To").Value)
                            obj1.Parameter_Type = clsCommon.myCstr(grows.Cells("Parameter Type").Value)
                            obj1.OPERATER_TYPE = clsCommon.myCstr(grows.Cells("Operater Type").Value)

                            If obj.INCENTIVE_TYPE = "QB" Or obj.INCENTIVE_TYPE = "QTSSLAB" Then
                                obj1.SLAB_FROM = clsCommon.myCdbl(grows.Cells("Qty").Value)
                                If obj1.SLAB_FROM <= 0 Then
                                    Throw New Exception("Please Enter Quantity At Line No. " + clsCommon.myCstr(counter) + ".")
                                End If
                                If obj.INCENTIVE_TYPE = "QTSSLAB" Then
                                    If obj1.TS_FROM <= 0 Or obj1.TS_TO <= 0 Then
                                        Throw New Exception("Please Enter TS At Line No. " + clsCommon.myCstr(counter) + ".")
                                    End If
                                End If
                            Else
                                obj1.SLAB_FROM = clsCommon.myCdbl(grows.Cells("Slab From").Value)
                                If obj.INCENTIVE_TYPE = "TSB" Or obj.INCENTIVE_TYPE = "QSLABTSSLAB" Then
                                    If obj1.TS_FROM <= 0 Or obj1.TS_TO <= 0 Then
                                        Throw New Exception("Please Enter TS At Line No. " + clsCommon.myCstr(counter) + ".")
                                    End If
                                End If
                                If obj.INCENTIVE_TYPE = "QSLAB" Or obj.INCENTIVE_TYPE = "QSLABTSSLAB" Then
                                    If obj1.SLAB_FROM <= 0 AndAlso obj1.SLAB_TO <= 0 Then
                                        Throw New Exception("Please Enter Slab Details At Line No. " + clsCommon.myCstr(counter) + ".")
                                    End If
                                End If
                                '' update [Parameter Type] and [Operater Type]
                                If obj.INCENTIVE_TYPE = "QLTY" Then
                                    If clsCommon.myLen(obj1.Parameter_Type) <= 0 Then
                                        Throw New Exception("Please Enter Parameter Type At Line No. " & clsCommon.myCstr(counter) & ".")
                                    ElseIf Not (clsCommon.CompairString(obj1.Parameter_Type, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.Parameter_Type, "SNF") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.Parameter_Type, "CLR") = CompairStringResult.Equal) Then
                                        Throw New Exception("Parameter Type entered at line No. " & clsCommon.myCstr(counter) & " is not valid(FAT,SNF,CLR).")
                                    ElseIf Not (clsCommon.CompairString(obj1.OPERATER_TYPE, "None") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.OPERATER_TYPE, "Continue") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.OPERATER_TYPE, "OR") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.OPERATER_TYPE, "XOR") = CompairStringResult.Equal Or clsCommon.CompairString(obj1.OPERATER_TYPE, "AND") = CompairStringResult.Equal) Then
                                        Throw New Exception("Operater Type entered at line No. " & clsCommon.myCstr(counter) & " is not valid(None,Continue,OR,AND).")
                                    End If
                                End If
                            End If

                            line_No += 1
                            ObjList.Add(obj1)
                        End If
                    Next
                    '==================================================================
                    Dim qry As String = "select count(*) from TSPL_Incentive_MASTER_Head where Incentive_code='" + obj.INCENTIVE_CODE + "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                    If check > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If

                    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    If obj.SaveData(obj, ObjList, isNewEntry) Then
                        counter += 1
                    End If
                Next

                clsCommon.ProgressBarHide()
                Reset()
                clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
    End Sub
End Class
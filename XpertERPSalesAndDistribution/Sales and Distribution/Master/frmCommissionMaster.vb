Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine
'--preeti gupta-ticket no.[BM00000003133]
Public Class FrmCommissionMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public IsbtnSaveClicked As Boolean = False
    Public UomCode As String


#Region "Variables"
    Dim userCode, companyCode As String
    Dim sql As String
    Dim ds As DataSet
    Const colcode As String = "Group Code"
    Const coldesc As String = "Description"
    Const colcomm As String = "Commission"
    Const colchk As Boolean = False
    Const colStartDate As String = "Start date"

    Dim objstr As String = "Tecxpert Software Pvt Ltd."
    ' Dim dt1 As Date = Date.Today
    ' Dim dt As Date = connectSql.serverDate()
    ' Dim realDate As Date = Date.ParseExact(dt, "mm/dd/yyyy", culture)
#End Region
#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#End Region
    Private Sub FrmCommissionMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fndItemCode.MyMaxLength = 50
        txtitemdesc.MaxLength = 100
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+R for Print Preview")

        SetUserMgmtNew()
        LoadBlankGrid()
        loadgroup()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If



    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCommissionMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGrid()

        rgvCustGrp.AddNewRowPosition = SystemRowPosition.Bottom
        rgvCustGrp.Rows.Clear()
        rgvCustGrp.Columns.Clear()

        'Dim uom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'Dim chk As GridViewCheckBoxColumn = New GridViewCheckBoxColumn
        ''chk.FormatString = ""
        'chk.HeaderText = ""
        'chk.Name = colchk
        'chk.Width = 70
        'chk.ReadOnly = False
        'chk.TextImageRelation = TextImageRelation.TextBeforeImage
        ''chk.HeaderImage = Global.XpertERPSalesAndDistribution.My.Resources.Resources.search4
        ''chk.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        'rgvCustGrp.MasterTemplate.Columns.Add(chk)

        Dim item_code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.HeaderText = "Customer Group Code"
        item_code.Name = colcode
        item_code.Width = 140
        item_code.ReadOnly = True
        item_code.TextImageRelation = TextImageRelation.TextBeforeImage
        'item_code.HeaderImage = Global.XpertERPSalesAndDistribution.My.Resources.Resources.search4
        item_code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        rgvCustGrp.MasterTemplate.Columns.Add(item_code)

        Dim item_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_desc.FormatString = ""
        item_desc.HeaderText = "Description"
        item_desc.Name = coldesc
        item_desc.Width = 210
        item_desc.ReadOnly = True
        item_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        rgvCustGrp.MasterTemplate.Columns.Add(item_desc)


        Dim commision As GridViewDecimalColumn = New GridViewDecimalColumn()
        commision = New GridViewDecimalColumn()
        commision.FormatString = ""
        commision.HeaderText = "Commission"
        commision.Name = colcomm
        commision.IsVisible = True
        commision.ShowUpDownButtons = False
        commision.Step = 0
        commision.Width = 100
        commision.Minimum = 0
        commision.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        commision.ReadOnly = False
        rgvCustGrp.MasterTemplate.Columns.Add(commision)

        Dim StartDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        StartDate.Format = DateTimePickerFormat.Custom
        StartDate.CustomFormat = "dd-MM-yyyy"
        StartDate.HeaderText = "Start Date"
        StartDate.FormatString = "{0:d}"
        StartDate.Name = colStartDate
        StartDate.WrapText = True
        StartDate.ReadOnly = False
        StartDate.Width = 80
        rgvCustGrp.MasterTemplate.Columns.Add(StartDate)

        rgvCustGrp.AllowDeleteRow = True
        rgvCustGrp.AllowAddNewRow = False
        rgvCustGrp.ShowGroupPanel = False
        rgvCustGrp.AllowColumnReorder = False
        rgvCustGrp.AllowRowReorder = False
        rgvCustGrp.EnableSorting = False
        rgvCustGrp.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        rgvCustGrp.MasterTemplate.ShowRowHeaderColumn = False

    End Sub


    Public Sub loadgroup()
        Try

            Dim qry As String = "select Cust_Group_Code  ,Cust_Group_Desc  from TSPL_CUSTOMER_GROUP_MASTER "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            For Each dr As DataRow In dt.Rows
                rgvCustGrp.Rows.AddNew()
                rgvCustGrp.Rows(rgvCustGrp.Rows.Count - 1).Cells(colcode).Value = clsCommon.myCstr(dr("Cust_Group_Code"))
                rgvCustGrp.Rows(rgvCustGrp.Rows.Count - 1).Cells(coldesc).Value = clsCommon.myCstr(dr("Cust_Group_Desc"))

            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub





    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub



    Private Sub MasterTemplate_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        'Dim grow As GridViewRowInfo
        'If e.Column.Index = 1 Then ''CustomerGroup

        '    Dim qry As String = "select Cust_Group_Code  as GroupCode,Cust_Group_Desc as GroupDescription  from TSPL_CUSTOMER_GROUP_MASTER "
        '    Dim str As String = clsCommon.myCstr(clsCommon.ShowSelectForm("CommissionMaster", qry, "GroupCode", "", clsCommon.myCstr(dgvHierarchy.CurrentRow.Cells("CustomerGroup").Value)))
        '    dgvHierarchy.CurrentRow.Cells("CustomerGroup").Value = str


        '    Dim comm As String = "select commission  from TSPL_Commission_Master where item_code='" + fndItemCode.Value + "' and uom='" + fndunit.Value + "' and hierarchy='" + dgvHierarchy.CurrentRow.Cells(0).Value.ToString() + "' and cust_group='" + dgvHierarchy.CurrentRow.Cells(1).Value.ToString() + "' "

        '    Dim dr As SqlDataReader
        '    Dim strvalue As Decimal

        '    dr = connectSql.RunSqlReturnDR(comm)
        '    While dr.Read()
        '        strvalue = dr(0).ToString()
        '    End While
        '    If strvalue <> 0 Then
        '        dgvHierarchy.CurrentRow.Cells(2).Value = strvalue
        '    Else
        '        dgvHierarchy.CurrentRow.Cells(2).Value = 0
        '    End If
        'End If
    End Sub


    Private Sub fndItemCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndItemCode.ConnectionString = connectSql.SqlCon()
        'fndItemCode.Query = "Select Sale_Invoice_No as 'Invoice No',Shipment_No as 'Shipment No',Cust_Code as 'Customer Code',Cust_Name as 'Customer Name' FROM TSPL_SALE_INVOICE_HEAD ORDER BY Sale_Invoice_No Desc"
        'fndItemCode.ValueToSelect = "Invoice No"
        'fndItemCode.ValueToSelect1 = "Invoice No"
        'fndItemCode.Caption = "Sales Invoices"
    End Sub

    


    Private Sub RadLabel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadLabel3.Click

    End Sub



    Private Sub fndunit__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndunit._MYValidating

        If fndItemCode.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Select the Item Code")

        Else
            Dim qry As String = "select UOM_Code as Code ,UOM_Description as Name from TSPL_ITEM_UOM_DETAIL "
            fndunit.Value = clsCommon.ShowSelectForm("unitcommisionfnd", qry, "Code", "Item_Code='" + fndItemCode.Value + "' ", fndunit.Value, "Code", isButtonClicked)
            'fndunit.Value = clsCommon.ShowSelectForm("unitcommision", qry, "Code", "", fndunit.Value, "Code", isButtonClicked)
            funfill()
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()

    End Sub
    Sub SaveData()
        If (AllowToSave()) Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                IsbtnSaveClicked = True
                funinsert(fndItemCode.Value, txtitemdesc.Text, trans)
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                funfill()
            Catch ex As Exception
                trans.Rollback()
                common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Finally
                IsbtnSaveClicked = False
            End Try
        End If
    End Sub


    Public Function AllowToSave() As Boolean
        Try

            If fndItemCode.Value = "" Then
                common.clsCommon.MyMessageBoxShow("Select Item Code")
                fndItemCode.Focus()
                Return False
            End If

            If fndunit.Value = "" Then
                common.clsCommon.MyMessageBoxShow("Select Unit Code")
                fndunit.Focus()
                Return False
            End If

            If ddlhier.Text = "Select" Then
                common.clsCommon.MyMessageBoxShow("Select Hierarchy")
                ddlhier.Focus()
                Return False
            End If
            For ii As Integer = 0 To rgvCustGrp.Rows.Count - 1
                Dim strCode As String = clsCommon.myCstr(rgvCustGrp.Rows(ii).Cells(colcode).Value)
                Dim Commission As Double = clsCommon.myCdbl(rgvCustGrp.Rows(ii).Cells(colcomm).Value)
                If (Commission > 0) And clsCommon.myLen(rgvCustGrp.Rows(ii).Cells(colStartDate).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Enter Start date For '" + strCode + "'")
                    Return False
                    ''ElseIf (Commission = Nothing Or Commission <= 0) And clsCommon.myLen(rgvCustGrp.Rows(ii).Cells(colStartDate).Value) > 0 Then
                    ''    common.clsCommon.MyMessageBoxShow("Please Enter Commission For '" + strCode + "'")
                    ''    Return False
                End If
            Next

            Dim Count As Integer = 0
            For ii As Integer = 0 To rgvCustGrp.Rows.Count - 1
                If clsCommon.myLen(rgvCustGrp.Rows(ii).Cells(colcomm).Value) > 0 AndAlso clsCommon.myLen(rgvCustGrp.Rows(ii).Cells(colStartDate).Value) > 0 Then
                    Count = Count + 1
                End If
            Next
            If Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Provide Commission For Atleast Single Customer Group")
                Return False
            End If

            'Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Public Sub funreset()
        fndItemCode.Value = ""
        txtitemdesc.Text = ""
        fndunit.Value = ""
        ddlhier.Text = "HOS"
        LoadBlankGrid()
        loadgroup()


        fndItemCode.Focus()
    End Sub

    ''Public Sub funinsert()
    ''    For i As Integer = 0 To dgvHierarchy.Rows.Count - 1
    ''        If dgvHierarchy.Rows(i).Cells(1).Value <> "" Then

    ''            Dim comm As String = "select commission  from TSPL_Commission_Master where item_code='" + fndItemCode.Value + "' and uom='" + fndunit.Value + "' and hierarchy='" + dgvHierarchy.Rows(i).Cells(0).Value.ToString() + "' and cust_group='" + dgvHierarchy.Rows(i).Cells(1).Value.ToString() + "' "
    ''            Dim dr As SqlDataReader
    ''            Dim strvalue As Decimal = 0
    ''            dr = connectSql.RunSqlReturnDR(comm)
    ''            While dr.Read()
    ''                strvalue = dr(0).ToString()
    ''            End While
    ''            If strvalue <> 0 Then
    ''                Dim qry As String = "update TSPL_Commission_Master set item_code='" + fndItemCode.Value + "',item_desc='" + txtitemdesc.Text + "',UOM='" + fndunit.Value + "',Hierarchy='" + dgvHierarchy.Rows(i).Cells(0).Value.ToString() + "',cust_group='" + dgvHierarchy.Rows(i).Cells(1).Value.ToString() + "',Commission='" + dgvHierarchy.Rows(i).Cells(2).Value.ToString() + "',created_by='" + userCode + "',created_date='" + connectSql.serverDate() + "',modify_by='" + userCode + "',modify_date='" + connectSql.serverDate() + "',comp_code='" + companyCode + "' where item_code='" + fndItemCode.Value + "' and uom='" + fndunit.Value + "' and hierarchy='" + dgvHierarchy.Rows(i).Cells(0).Value.ToString() + "' and cust_group='" + dgvHierarchy.Rows(i).Cells(1).Value.ToString() + "' "
    ''                connectSql.RunSql(qry)
    ''            Else
    ''                If dgvHierarchy.Rows(i).Cells(1).Value <> "" Then

    ''                    Dim qry As String = "insert into TSPL_Commission_Master(item_code,item_desc,UOM,Hierarchy,cust_group,Commission,created_by,created_date,modify_by,modify_date,comp_code) values ('" + fndItemCode.Value + "','" + txtitemdesc.Text + "','" + fndunit.Value + "','" + dgvHierarchy.Rows(i).Cells(0).Value.ToString() + "','" + dgvHierarchy.Rows(i).Cells(1).Value.ToString() + "','" + dgvHierarchy.Rows(i).Cells(2).Value.ToString() + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "')"
    ''                    connectSql.RunSql(qry)
    ''                End If
    ''            End If
    ''        End If
    ''    Next
    ''End Sub


    Public Sub funinsert(ByVal itemCode As String, ByVal ItemDesc As String, ByVal trans As SqlTransaction)
        Try
            Dim qrytb As String = "select Item_Code,Item_Desc,UOM,Hierarchy,Cust_Group,Commission, Start_date  from TSPL_Commission_Master where item_code='" + itemCode + "' and  uom='" + fndunit.Value + "'  and Hierarchy='" + ddlhier.Text + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qrytb, trans)
            If dt.Rows.Count > 0 AndAlso IsbtnSaveClicked = True Then
                For ii As Integer = 0 To rgvCustGrp.Rows.Count - 1
                    Dim cm As Double = clsCommon.myCdbl(rgvCustGrp.Rows(ii).Cells(colcomm).Value)
                    Dim gpcode As String = rgvCustGrp.Rows(ii).Cells(colcode).Value.ToString()
                    Dim Enddate As String = Nothing
                    Dim Startdate As String = Nothing
                    If clsCommon.myLen(rgvCustGrp.Rows(ii).Cells(colStartDate).Value) <= 0 Then
                        Enddate = Nothing
                    Else
                        Startdate = clsCommon.GetPrintDate(rgvCustGrp.Rows(ii).Cells(colStartDate).Value)
                        Enddate = clsCommon.GetPrintDate(clsCommon.myCDate(rgvCustGrp.Rows(ii).Cells(colStartDate).Value).AddDays(-1), "dd/MMM/yyyy")

                    End If

                    For j As Integer = 0 To dt.Rows.Count - 1
                        Dim strStartDate As String
                        If clsCommon.myLen(dt.Rows(j)("Start_date")) <= 0 Then
                            strStartDate = Nothing
                        Else
                            strStartDate = clsCommon.GetPrintDate(dt.Rows(j)("Start_date"))
                        End If
                        If (clsCommon.myCdbl(dt.Rows(j)("Commission")) <> cm Or strStartDate <> Startdate) And dt.Rows(j)("Cust_Group").ToString() = gpcode Then
                            Dim strqry As String = "insert into TSPL_Commission_Master_History(item_code,item_desc,UOM,Hierarchy,cust_group,Commission,created_by,created_date,History_date,comp_code, Start_Date, End_Date) values ('" + itemCode + "','" + ItemDesc + "','" + clsCommon.myCstr(fndunit.Value) + "','" + clsCommon.myCstr(ddlhier.Text) + "','" + clsCommon.myCstr(rgvCustGrp.Rows(ii).Cells(colcode).Value.ToString()) + "','" + dt.Rows(j)("Commission").ToString() + "','" + userCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + companyCode + "', '" + strStartDate + "', " + IIf(clsCommon.myLen(Enddate) > 0, "'" + Enddate + "'", "Null") + ")"
                            clsDBFuncationality.ExecuteNonQuery(strqry, trans)
                        End If
                    Next
                Next
            End If


            Dim str As String = "delete from TSPL_Commission_Master where item_code='" + itemCode + "' and  uom='" + fndunit.Value + "'  and Hierarchy='" + ddlhier.Text + "'"
            clsDBFuncationality.ExecuteNonQuery(str, trans)
            For i As Integer = 0 To rgvCustGrp.Rows.Count - 1
                Dim com As String = clsCommon.myCdbl(rgvCustGrp.Rows(i).Cells(colcomm).Value)
                Dim strStartDate As String = Nothing
                If clsCommon.myLen(rgvCustGrp.Rows(i).Cells(colStartDate).Value) > 0 Then
                    strStartDate = clsCommon.GetPrintDate(rgvCustGrp.Rows(i).Cells(colStartDate).Value, "dd/MMM/yyyy")
                End If
                If clsCommon.myLen(rgvCustGrp.Rows(i).Cells(colcomm).Value) > 0 Or clsCommon.myCdbl(rgvCustGrp.Rows(i).Cells(colcomm).Value) > 0 Then
                    Dim qry As String = "insert into TSPL_Commission_Master(item_code,item_desc,UOM,Hierarchy,cust_group,Commission,created_by,created_date,modify_by,modify_date,comp_code, Start_Date) values ('" + itemCode + "','" + ItemDesc + "','" + clsCommon.myCstr(fndunit.Value) + "','" + clsCommon.myCstr(ddlhier.Text) + "','" + clsCommon.myCstr(rgvCustGrp.Rows(i).Cells(colcode).Value.ToString()) + "','" + com + "','" + userCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + userCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + companyCode + "'," + IIf(clsCommon.myLen(strStartDate) > 0, "'" + strStartDate + "'", "Null") + " )"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub funfill()
                        Try
            Dim         qry As String = "select Item_Code,Item_Desc,UOM,Hierarchy,Cust_Group,Commission, Start_Date  from TSPL_Commission_Master where item_code='" + fndItemCode.Value + "' and  uom='" + fndunit.Value + "'  and Hierarchy='" + ddlhier.Text + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count <= 0 Then
                LoadBlankGrid()
                loadgroup()
            Else
                For i As Integer = 0 To rgvCustGrp.Rows.Count - 1
                    Dim code As String = rgvCustGrp.Rows(i).Cells(colcode).Value.ToString()
                    rgvCustGrp.Rows(i).Cells(colcomm).Value = Nothing
                    rgvCustGrp.Rows(i).Cells(colStartDate).Value = Nothing
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(j)("Cust_Group").ToString() = code Then
                            rgvCustGrp.Rows(i).Cells(colcomm).Value = dt.Rows(j)("Commission").ToString()
                            If dt.Rows(j)("Start_date") IsNot DBNull.Value Then
                                rgvCustGrp.Rows(i).Cells(colStartDate).Value = dt.Rows(j)("Start_date").ToString()
                            End If
                        End If
                    Next
                Next
            End If

                Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        funreset()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndItemCode.Value = "" Then
            myMessages.blankValue(Me, "Item Code", Me.Text)

        ElseIf fndunit.Value = "" Then

            myMessages.blankValue(Me, "UOM", Me.Text)

        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            funreset()

        End If
    End Sub


    Public Sub fundelete()
        Try
            Dim str As String = "delete from TSPL_Commission_Master where item_code='" + fndItemCode.Value + "' and  uom='" + fndunit.Value + "'  and Hierarchy='" + ddlhier.Text + "'"
            clsDBFuncationality.ExecuteNonQuery(str)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub funexport()
        Dim qry As String = "select Item_Code as 'ItemCode',Item_Desc as 'ItemDesc',UOM as 'UOM',Hierarchy as 'Hierarchy' ,Cust_Group as 'CustomerGroup',Commission as 'Commission', Start_Date as 'Start date'  from TSPL_Commission_Master "
        ExporttoExcel(qry, Me)
    End Sub

    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        funexport()
    End Sub
    Public Sub funImport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "ItemCode", "ItemDesc", "UOM", "Hierarchy", "CustomerGroup", "Commission", "Start date") Then
            Dim trans As SqlTransaction = Nothing
            Try
                Dim LineNo As Integer = 0
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = LineNo + 1
                    Dim ItemCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim ItemDesc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim uom As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim Hierarchy As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim CustGroup As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim commision As String = clsCommon.myCstr(grow.Cells(5).Value)
                    Dim StartDate As String

                    If clsCommon.myLen(grow.Cells(6).Value) <= 0 Then
                        Throw New Exception("Please Enter a Valid date Against Item '" + ItemCode + "' at Line No '" + LineNo.ToString() + "'")
                    Else
                        StartDate = clsCommon.GetPrintDate(grow.Cells(6).Value, "dd/MMM/yyyy")
                    End If

                    If String.IsNullOrEmpty(ItemCode) And clsCommon.myLen(ItemCode) > 50 Then
                        Throw New Exception("Item Name has some error")
                    End If
                    If clsCommon.myLen(ItemDesc) > 100 Then
                        Throw New Exception("Item Desc has some error")
                    End If
                    If String.IsNullOrEmpty(uom) And clsCommon.myLen(uom) > 5 Then
                        Throw New Exception("UOM has some error")
                    End If
                    If String.IsNullOrEmpty(Hierarchy) And clsCommon.myLen(Hierarchy) > 10 Then
                        Throw New Exception(" Hierarchy has some error")
                    End If
                    If clsCommon.myLen(CustGroup) > 30 Then
                        Throw New Exception(" Customer Group has some error")
                    End If
                    Dim sql1 As String = "select count(*) from TSPL_Commission_Master where item_Code='" + ItemCode + "'and uom='" + uom + "'and Hierarchy ='" + Hierarchy + "'and Cust_Group ='" + CustGroup + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        Dim Sql As String = "insert into TSPL_Commission_Master(Item_Code,Item_Desc,UOM,Hierarchy,Cust_Group ,Commission,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code, Start_Date ) values ('" + ItemCode + "', '" + ItemDesc + "', '" + uom + "','" + Hierarchy + "', '" + CustGroup + "', '" + commision + "','" + userCode + "', '" + connectSql.serverDate(trans) + "', '" + userCode + "', '" + connectSql.serverDate(trans) + "', '" + companyCode + "', '" + StartDate + "' )"
                        clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                    Else
                        Dim sql As String = "update  TSPL_Commission_Master set item_code='" + ItemCode + "', Item_Desc = '" + ItemDesc + "' , UOM='" + uom + "',Hierarchy='" + Hierarchy + "',Cust_Group='" + CustGroup + "',Commission='" + commision + "',modify_by='" + userCode + "',modify_date='" + connectSql.serverDate(trans) + "' , Start_Date='" + StartDate + "'where Item_Code= '" + ItemCode + "'and UOM='" + uom + "'and Hierarchy='" + Hierarchy + "'and Cust_Group='" + CustGroup + "' "
                        clsDBFuncationality.ExecuteNonQuery(sql, trans)
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub

    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        funImport()
    End Sub



    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "COMM-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        Me.Close()
    End Sub



    'Private Sub dgvHierarchy_CellClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)

    '    If dgvHierarchy.CurrentRow.Cells("Hierarchy").Value = "HOS" Then
    '        gpcustgroup.Text = "Customer Group for HOS"
    '    ElseIf dgvHierarchy.CurrentRow.Cells("Hierarchy").Value = "TDM" Then
    '        gpcustgroup.Text = "Customer Group for TDM"
    '    ElseIf dgvHierarchy.CurrentRow.Cells("Hierarchy").Value = "ADC" Then
    '        gpcustgroup.Text = "Customer Group for ADC"
    '    ElseIf dgvHierarchy.CurrentRow.Cells("Hierarchy").Value = "CE" Then
    '        gpcustgroup.Text = "Customer Group for CE"
    '    ElseIf dgvHierarchy.CurrentRow.Cells("Hierarchy").Value = "RA" Then
    '        gpcustgroup.Text = "Customer Group for RA"

    '    End If


    'End Sub

    Private Sub ddlhier_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlhier.SelectedIndexChanged
        Try

            funfill()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        If fndItemCode.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Select the Item Code")
        ElseIf fndunit.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Select the UOM Code")
        ElseIf ddlhier.Text = "" Then
            common.clsCommon.MyMessageBoxShow("Select the Hierarchy Code")
        Else
            funprint()
        End If


    End Sub


    Public Sub funprint()
        Try
            'Dim qrytb As String = "select Item_Code,Item_Desc,UOM,Hierarchy,Cust_Group,Commission  from TSPL_Commission_Master_History where item_code='" + fndItemCode.Value + "' and  uom='" + fndunit.Value + "'  and Hierarchy='" + ddlhier.Text + "'"
            Dim qry As String = "  select TSPL_Commission_Master_History.Item_Code,TSPL_Commission_Master_History.Item_Desc,TSPL_Commission_Master_History.UOM,TSPL_Commission_Master_History.Hierarchy,TSPL_Commission_Master_History.Cust_Group,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc,TSPL_Commission_Master_History.Commission ,TSPL_Commission_Master_History.History_date,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Add1 from TSPL_Commission_Master_History " & _
                                 "  left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Commission_Master_History.Comp_Code " & _
                                 "  left Outer Join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_Commission_Master_History.Cust_Group where " & _
                                 "  TSPL_Commission_Master_History.Item_Code='" + fndItemCode.Value + "' and UOM='" + fndunit.Value + "' and TSPL_Commission_Master_History.Hierarchy='" + ddlhier.Text + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptCommissionMasterHistory", "Commission History Report")


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReplicate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplicate.Click
        Try
            If AllowToSave() Then
                Dim frm As New FrmItemSelector2
                frm.UomCode = fndunit.Value
                frm.ShowDialog()
                If frm.Arr.Count > 0 Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        For i As Integer = 0 To frm.Arr.Count - 1
                            Dim strItemCode As String = frm.Arr.Item(i)
                            Dim StrItemDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER Where Item_Code='" + strItemCode + "'", trans))
                            funinsert(strItemCode, strItemDesc, trans)
                        Next
                        trans.Commit()
                        common.clsCommon.MyMessageBoxShow("Data  Raplicated Successfully")
                    Catch ex As Exception
                        trans.Rollback()
                        common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
                    End Try
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmCommissionMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            funprint()
        End If
    End Sub

    Private Sub fndItemCode__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndItemCode._MYValidating

        Dim qry As String = "select item_code as [Code],item_desc as [Description] from TSPL_item_master "
        fndItemCode.Value = clsCommon.ShowSelectForm("ItemCodefnd", qry, "Code", "Item_Type='F'", fndItemCode.Value, "Code", isButtonClicked)
        txtitemdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from TSPL_item_master where item_code='" + fndItemCode.Value + "'"))
        fndunit.Value = ""
        ddlhier.Text = "HOS"
        LoadBlankGrid()
        loadgroup()

    End Sub

    Private Sub fndItemCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndItemCode._MYNavigator
        Dim qst As String = "select item_code as [Code],item_desc as [Description] from TSPL_item_master  where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and item_code in (select min(item_code) from TSPL_item_master where item_code>'" + fndItemCode.Value + "'  and Item_Type='F'  ) "
            Case NavigatorType.First
                qst += "and item_code in (select MIN(item_code) from TSPL_item_master where  Item_Type='F' )"
            Case NavigatorType.Last
                qst += "and item_code in (select Max(item_code) from TSPL_item_master where Item_Type='F'  )"
            Case NavigatorType.Previous
                qst += "and item_code in (select max(item_code) from TSPL_item_master where item_code<'" + fndItemCode.Value + "' and Item_Type='F'  )"
        End Select

        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndItemCode.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            txtitemdesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        'TextChanged()
        If fndItemCode.Value IsNot Nothing Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False

        End If
        fndunit.Value = ""
        ddlhier.Text = "HOS"
        LoadBlankGrid()
        loadgroup()
    End Sub

End Class


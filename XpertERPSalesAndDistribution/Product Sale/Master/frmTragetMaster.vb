Imports common
Imports System.Data.SqlClient
''==================Created by Preeti Gupta===========Ticket No[] Inherits FrmMainTranScreen

Public Class FrmTragetMaster

    Const colLineNo As String = "LineNo"
    Const colICode As String = "ItemCode"
    Const colIName As String = "ItemName"
    Const colUOM As String = "UnitCode"
    Const colPriceDate As String = "PriceDate"
    'Const colQty As String = "Quantity"
    'Const colSchemeqty As String = "Schemeqty"
    Const colSchemeItemCode As String = "SchemeItem Code"
    Const colSchemeItemdesc As String = "SchemeItem Desc"
    Const colSchemeQty As String = "SchemeQty"
    Const colSchemeUom As String = "SchemeItem UOM"
    Const colfromQty As String = "From Quantity"
    Const colToQty As String = "To Quantity"

    Const colSelect As String = "Select"
    Const colCustCode As String = "Customer Code"
    Const colCustName As String = "CustName"

    Private isInsideLoadData As Boolean = False
    Private isFromLoad As Boolean = False
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim isNewEntry As Boolean = True
    Sub LoadBlankItemGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = XpertERPSalesAndDistribution.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gvItem.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 200
        repoIName.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnitCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnitCode.HeaderText = "Unit Code"
        repoUnitCode.Name = colUOM
        repoUnitCode.Width = 80
        repoUnitCode.HeaderImage = XpertERPSalesAndDistribution.My.Resources.Resources.search4
        repoUnitCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoUnitCode.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoUnitCode)


        Dim repoReqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReqQty.FormatString = ""
        repoReqQty.HeaderText = "From Quantity"
        repoReqQty.Name = colfromQty
        repoReqQty.Width = 80
        repoReqQty.Minimum = 0
        repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReqQty)

        Dim repoReqSchemeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReqSchemeQty.FormatString = ""
        repoReqSchemeQty.HeaderText = "To Quantity"
        repoReqSchemeQty.Name = colToQty
        repoReqSchemeQty.Width = 80
        repoReqSchemeQty.Minimum = 0
        repoReqSchemeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReqSchemeQty)

        Dim repoSchemeICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeICode.FormatString = ""
        repoSchemeICode.HeaderText = "Scheme Item Code"
        repoSchemeICode.Name = colSchemeItemCode
        repoSchemeICode.Width = 100
        repoSchemeICode.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoSchemeICode)

        Dim repoSchemeIDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeIDesc.FormatString = ""
        repoSchemeIDesc.HeaderText = "Scheme Item Description"
        repoSchemeIDesc.Name = colSchemeItemdesc
        repoSchemeIDesc.Width = 200
        repoSchemeIDesc.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSchemeIDesc)

        Dim repoSchemeUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeUOM.FormatString = ""
        repoSchemeUOM.HeaderText = "UOM"
        repoSchemeUOM.Name = colSchemeUom
        repoSchemeUOM.Width = 80
        repoSchemeUOM.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoSchemeUOM)

        Dim repoSchemeIQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeIQty.FormatString = ""
        repoSchemeIQty.HeaderText = "Qty"
        repoSchemeIQty.Name = colSchemeQty
        repoSchemeIQty.Width = 80
        repoSchemeIQty.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoSchemeIQty)

        gvItem.AllowDeleteRow = True
        gvItem.AllowAddNewRow = False
        gvItem.ShowGroupPanel = False
        gvItem.AllowColumnReorder = False
        gvItem.AllowRowReorder = False
        gvItem.EnableSorting = False
        gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvItem.MasterTemplate.ShowRowHeaderColumn = False
        gvItem.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmTragetMaster)
        If Not (MyBase.isReadFlag) Then

            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag

        'If btnSave.Visible = True Then
        '    rmiImport.Enabled = True
        '    rmiExport.Enabled = True
        'Else
        '    rmiImport.Enabled = False
        '    rmiExport.Enabled = False
        'End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadCriteria()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Customer Group")
        dt.Rows.Add("Customer Category")
        'dt.Rows.Add("Customer")
        ddlCriteria.DataSource = dt
        ddlCriteria.ValueMember = "Code"
        ddlCriteria.DisplayMember = "Code"
    End Sub
    Sub LoadBlankCustomerGrid()
        gvCustomer.DataSource = Nothing
        gvCustomer.Rows.Clear()
        gvCustomer.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCustomer.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 100
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvCustomer.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCCode.FormatString = ""
        repoCCode.HeaderText = "Customer Code"
        repoCCode.Name = colCustCode
        repoCCode.HeaderImage = XpertERPSalesAndDistribution.My.Resources.Resources.search4
        repoCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCCode.Width = 150
        gvCustomer.MasterTemplate.Columns.Add(repoCCode)

        Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCName.FormatString = ""
        repoCName.HeaderText = "Customer Name"
        repoCName.Name = colCustName
        repoCName.Width = 300
        repoCName.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoCName)

        gvCustomer.AllowDeleteRow = True
        gvCustomer.AllowAddNewRow = False
        gvCustomer.ShowGroupPanel = False
        gvCustomer.AllowColumnReorder = False
        gvCustomer.AllowRowReorder = False
        gvCustomer.EnableSorting = False
        gvCustomer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCustomer.MasterTemplate.ShowRowHeaderColumn = False
        gvCustomer.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadTragetTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Product")
        dt.Rows.Add("Fresh")
        ddlTragetType.DataSource = dt
        ddlTragetType.ValueMember = "Code"
        ddlTragetType.DisplayMember = "Code"
    End Sub
    Private Sub FrmTragetMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isFromLoad = True
        Dim ButtonToolTip As ToolTip = New ToolTip()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtfromDate.Value = txtToDate.Value.AddMonths(-1)
        SetUserMgmtNew()
        LoadCriteria()
        LoadBlankItemGrid()
        LoadTragetTypes()
        gvItem.Rows.AddNew()
        LoadBlankCustomerGrid()
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub ddlCriteria_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlCriteria.SelectedIndexChanged
        Try
            If Not isFromLoad Then
                txtCriteria.Value = ""
                lblCriteria.Text = ""
                If clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Group") = CompairStringResult.Equal Or clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Category") = CompairStringResult.Equal Then
                    txtCriteria.Enabled = True
                    LoadBlankCustomerGrid()
                Else
                    txtCriteria.Enabled = False
                    LoadBlankCustomerGrid()
                    gvCustomer.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub LoadCustomerData(ByVal dt As DataTable)
        Try
            gvCustomer.DataSource = dt
            If dt.Rows.Count > 0 Then
                btnSelect.Text = "UnSelect All"
            Else
                btnSelect.Text = "Select All"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub FillCustomerGrid(ByVal strCriteria As String)
        Try
            gvCustomer.DataSource = Nothing
            gvCustomer.Rows.Clear()
            gvCustomer.Columns.Clear()
            If clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Group") = CompairStringResult.Equal Then
                qry = "Select ROW_NUMBER() Over (order by Cust_Code) as [LineNo], Cast(1 as bit) as [Select], Cust_Code as [Customer Code], Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER WHERE Cust_Group_Code='" + strCriteria + "'"
                LoadCustomerData(clsDBFuncationality.GetDataTable(qry))
            ElseIf clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Category") = CompairStringResult.Equal Then
                qry = "select ROW_NUMBER() Over (order by Cust_Code) as [LineNo], Cast(1 as bit) as [Select], Cust_Code as [Customer Code], Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER WHERE Cust_Category_Code='" + strCriteria + "'"
                LoadCustomerData(clsDBFuncationality.GetDataTable(qry))
            Else
            End If
            If gvCustomer.Rows.Count > 0 Then
                gvCustomer.Columns("LineNo").Width = 100
                gvCustomer.Columns("Select").Width = 100
                gvCustomer.Columns("Customer Code").Width = 150
                gvCustomer.Columns("Customer Name").Width = 300
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtCriteria__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCriteria._MYValidating
        If clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Group") = CompairStringResult.Equal Then
            qry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Description from TSPL_CUSTOMER_GROUP_MASTER"
            txtCriteria.Value = clsCommon.ShowSelectForm("CriteriaFinder@SCHMM", qry, "Code", "", txtCriteria.Value, "", isButtonClicked)
            lblCriteria.Text = clsDBFuncationality.getSingleValue("Select Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER WHERE Cust_Group_Code ='" + txtCriteria.Value + "'")
        ElseIf clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Category") = CompairStringResult.Equal Then
            qry = "Select CUST_CATEGORY_CODE as Code, CUST_CATEGORY_DESC as Description from TSPL_CUSTOMER_CATEGORY_MASTER"
            txtCriteria.Value = clsCommon.ShowSelectForm("CriteriaFinder@SCHMM", qry, "Code", "", txtCriteria.Value, "", isButtonClicked)
            lblCriteria.Text = clsDBFuncationality.getSingleValue("Select CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER WHERE CUST_CATEGORY_CODE ='" + txtCriteria.Value + "'")
        End If
        FillCustomerGrid(txtCriteria.Value)
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        If clsCommon.CompairString(btnSelect.Text, "Select All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvCustomer.Rows
                grow.Cells(colSelect).Value = True
            Next
            btnSelect.Text = "UnSelect All"
        Else
            For Each grow As GridViewRowInfo In gvCustomer.Rows
                grow.Cells(colSelect).Value = False
            Next
            btnSelect.Text = "Select All"
        End If
    End Sub

    Private Sub gvCustomer_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCustomer.CellValueChanged
        If (Not isInsideLoadData) Then
            If e.Column Is gvCustomer.Columns(colCustCode) Then
                qry = "Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER"
                gvCustomer.CurrentRow.Cells(colCustCode).Value = clsCommon.ShowSelectForm("CustFinder@SM", qry, "Code", "", gvCustomer.CurrentRow.Cells(colCustCode).Value, "Code", False)
                gvCustomer.CurrentRow.Cells(colCustName).Value = clsCustomerMaster.GetName(gvCustomer.CurrentRow.Cells(colCustCode).Value, Nothing)
                If clsCommon.myLen(gvCustomer.CurrentRow.Cells(colCustCode).Value) > 0 Then
                    gvCustomer.CurrentRow.Cells(colSelect).Value = True
                Else
                    gvCustomer.CurrentRow.Cells(colSelect).Value = False
                End If
            End If
        End If
    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        If (Not isInsideLoadData) Then
            If e.Column Is gvItem.Columns(colICode) Then
                qry = "Select Item_Code as Code, Item_Desc as Description from TSPL_ITEM_MASTER"
                gvItem.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("CustFinder@SM", qry, "Code", "", gvItem.CurrentRow.Cells(colICode).Value, "Code", False)
                gvItem.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(gvItem.CurrentRow.Cells(colICode).Value, Nothing)
            End If
            If e.Column Is gvItem.Columns(colUOM) Then
                qry = "select distinct UOM_Description  from TSPL_ITEM_UOM_DETAIL "

                Dim whrCls As String = " Item_Code = '" + gvItem.CurrentRow.Cells(colICode).Value + "' "
                gvItem.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("CustFinder", qry, "UOM_Description", whrCls, gvItem.CurrentRow.Cells(colUOM).Value, "UOM_Description", False)
            End If
            If e.Column Is gvItem.Columns(colSchemeItemCode) Then
                qry = "Select Item_Code as Code, Item_Desc as Description from TSPL_ITEM_MASTER"
                gvItem.CurrentRow.Cells(colSchemeItemCode).Value = clsCommon.ShowSelectForm("CustFin@SM", qry, "Code", "", gvItem.CurrentRow.Cells(colSchemeItemCode).Value, "Code", False)
                gvItem.CurrentRow.Cells(colSchemeItemdesc).Value = clsItemMaster.GetItemName(gvItem.CurrentRow.Cells(colSchemeItemCode).Value, Nothing)
            End If
            If e.Column Is gvItem.Columns(colSchemeUom) Then
                qry = "select distinct UOM_Description  from TSPL_ITEM_UOM_DETAIL "

                Dim whrCls As String = " Item_Code = '" + gvItem.CurrentRow.Cells(colSchemeItemCode).Value + "' "
                gvItem.CurrentRow.Cells(colSchemeUom).Value = clsCommon.ShowSelectForm("CustUOMFinder", qry, "UOM_Description", whrCls, gvItem.CurrentRow.Cells(colSchemeUom).Value, "UOM_Description", False)
            End If
        End If

    End Sub
    Private Sub gvItem_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvItem.CurrentColumnChanged
       
        If gvItem.RowCount > 0 Then
            Dim intCurrRow As Integer = gvItem.CurrentRow.Index
            gvItem.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gvItem.Rows.Count - 1 Then
                gvItem.Rows.AddNew()
                gvItem.CurrentRow = gvItem.Rows(intCurrRow)
            End If
        End If
    End Sub

    'Private Sub gvItem_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gvItem.EditorRequired
    '    Try

    '        Dim unit As GridViewComboBoxColumn = TryCast(gvItem.Columns(colUOM), GridViewComboBoxColumn)

    '        If gvItem.CurrentColumn.Name = colUOM Then
    '            qry = "select distinct UOM_Description  from TSPL_ITEM_UOM_DETAIL  where Item_Code = '" + gvItem.CurrentRow.Cells(colICode).Value + "'"
    '            unit.DataSource = clsDBFuncationality.GetDataTable(qry)
    '            unit.ValueMember = "UOM_Description"
    '            unit.DisplayMember = "UOM_Description"
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    Function AllowToSave() As Boolean
       
        Dim linno As Integer = 0
        If txtfromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtfromDate.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please enter Traget Description")
            txtDesc.Focus()
            Return False
        ElseIf clsCommon.myLen(ddlTragetType.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Traget Type")
            ddlTragetType.Focus()
            Return False
        End If
        Dim Icode As String = ""
        Dim oldicode As String = ""
        If clsCommon.myLen(colICode) <= 0 Then
            gvItem.CurrentRow = gvItem.Rows(0)
            clsCommon.MyMessageBoxShow("Fill atleast one row in grid.")
            Return False
        End If

        For ii As Integer = 0 To gvItem.Rows.Count - 1
            Icode = clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value)

            If clsCommon.myLen(Icode) > 0 Then

                For jj As Integer = ii + 1 To gvItem.Rows.Count - 1
                    oldicode = clsCommon.myCstr(gvItem.Rows(jj).Cells(colICode).Value)

                    If clsCommon.CompairString(Icode, oldicode) = CompairStringResult.Equal Then
                        gvItem.CurrentRow = gvItem.Rows(jj + 1)
                        clsCommon.MyMessageBoxShow("Duplicate item at row no. " + clsCommon.myCstr(jj + 1) + "")
                        Return False
                    End If
                Next
            End If
        Next

       
        ''
        Return True
    End Function
    Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmTragetMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsTragetMasterHeadProductSale()
                obj.Target_Code = clsCommon.myCstr(fndTragetCode.Value)
                obj.Traget_Desc = clsCommon.myCstr(txtDesc.Text)
                obj.FromDate = clsCommon.myCDate(txtfromDate.Value)
                obj.ToDate = clsCommon.myCDate(txtToDate.Value)
                obj.Traget_Type = clsCommon.myCstr(ddlTragetType.SelectedValue)
                obj.Criteria = ddlCriteria.SelectedValue
                obj.Criteria_Code = clsCommon.myCstr(txtCriteria.Value)
                obj.DocumentDate = clsCommon.myCDate(txtDate.Value)

                obj.ArrDTL = New List(Of clsTragetMasterDetailProductSale)

                ''For Custom Fields
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Target_Code) from TSPL_TARGET_MASTER_HEAD where Target_Code='" + obj.Target_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                obj.Form_ID = MyBase.Form_ID
                'obj.arrCustomFields = New List(Of clsCustomFieldValues)
                'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                '    UcCustomFields1.GetData(obj.arrCustomFields)
                'End If
                ''End of For Custom Fields

                '============================Scheme Item Information==============================
                Dim Count As Integer = 0

                obj.ArrDTL = New List(Of clsTragetMasterDetailProductSale)
                For Each grow As GridViewRowInfo In gvItem.Rows
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                        Dim objTr As New clsTragetMasterDetailProductSale()
                        objTr.TragetCode = clsCommon.myCstr(fndTragetCode.Value)
                        objTr.LineNo = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Quantity = clsCommon.myCdbl(grow.Cells(colfromQty).Value)
                        objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.SchemeQty = clsCommon.myCdbl(grow.Cells(colSchemeQty).Value)

                        objTr.SchemeItem_Code = clsCommon.myCstr(grow.Cells(colSchemeItemCode).Value)
                        objTr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colSchemeQty).Value)
                        objTr.SchemeUOM = clsCommon.myCstr(grow.Cells(colSchemeUom).Value)
                        objTr.To_Qty = clsCommon.myCdbl(grow.Cells(colToQty).Value)
                        objTr.SchemeItem_desc = clsCommon.myCstr(grow.Cells(colSchemeItemdesc).Value)
                        obj.ArrDTL.Add(objTr)
                        Count += 1
                    End If
                Next


                '-----------------Customer Information-----------------
                Count = 0
                obj.ArrSchm = New List(Of clsTragetMasterScheme)
                For Each grow As GridViewRowInfo In gvCustomer.Rows
                    If grow.Cells(colSelect).Value = True Then
                        Dim objTr As New clsTragetMasterScheme()
                        objTr.Target_Code = clsCommon.myCstr(fndTragetCode.Value)
                        objTr.Cust_Code = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                        obj.ArrSchm.Add(objTr)
                        Count += 1
                    End If
                Next
                If Count <= 0 Then
                    If clsCommon.myLen(txtCriteria.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please select atleast singl customer.")
                        txtCriteria.Focus()
                        Exit Sub
                    End If
                End If

                '==================Detail Section Ends Here=======================
                obj.SaveData(obj, isNewEntry)
                LoadData(obj.Target_Code, NavigatorType.Current)

                isNewEntry = False
                clsCommon.MyMessageBoxShow("Data Saved Successfully.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strSecCustomerCode As String, ByVal NavType As NavigatorType)
        Try
            Reset()
            Dim obj As New clsTragetMasterHeadProductSale
            obj = clsTragetMasterHeadProductSale.GetData(strSecCustomerCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Target_Code) > 0 Then
                isInsideLoadData = True
                fndTragetCode.Value = obj.Target_Code
                txtDesc.Text = obj.Traget_Desc
                txtfromDate.Value = obj.FromDate
                txtToDate.Value = obj.ToDate
                ddlTragetType.SelectedValue = obj.Traget_Type
                ddlCriteria.SelectedValue = obj.Criteria
                txtCriteria.Value = obj.Criteria_Code
                lblCriteria.Text = obj.Criteria_Desc
                txtDate.Value = obj.DocumentDate
                ' ''For Custom Fields
                'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                '    UcCustomFields1.GetData(obj.arrCustomFields)
                'End If
                ' ''End of For Custom Fields
                Dim LineNo As Integer = 0
                For Each objDTL As clsTragetMasterDetailProductSale In obj.ArrDTL
                    LineNo += 1
                    gvItem.CurrentRow.Cells(colLineNo).Value = objDTL.LineNo
                    gvItem.CurrentRow.Cells(colICode).Value = objDTL.Item_Code
                    gvItem.CurrentRow.Cells(colIName).Value = objDTL.Item_Desc
                    gvItem.CurrentRow.Cells(colfromQty).Value = objDTL.Quantity
                    gvItem.CurrentRow.Cells(colSchemeqty).Value = objDTL.SchemeQty
                    gvItem.CurrentRow.Cells(colUOM).Value = objDTL.UOM

                    gvItem.CurrentRow.Cells(colSchemeUom).Value = objDTL.SchemeUOM
                    gvItem.CurrentRow.Cells(colToQty).Value = objDTL.To_Qty
                    gvItem.CurrentRow.Cells(colSchemeItemCode).Value = objDTL.SchemeItem_Code
                    gvItem.CurrentRow.Cells(colSchemeItemdesc).Value = objDTL.SchemeItem_desc
                    gvItem.CurrentRow.Cells(colSchemeQty).Value = objDTL.Scheme_Qty
                    gvItem.Rows.AddNew()
                Next

                LineNo = 0
                For Each objSchmBen As clsTragetMasterScheme In obj.ArrSchm
                    gvCustomer.Rows.AddNew()
                    LineNo += 1
                    gvCustomer.CurrentRow.Cells(colLineNo).Value = LineNo
                    gvCustomer.CurrentRow.Cells(colSelect).Value = objSchmBen.check
                    gvCustomer.CurrentRow.Cells(colCustCode).Value = objSchmBen.Cust_Code
                    gvCustomer.CurrentRow.Cells(colCustName).Value = objSchmBen.Customer_Name
                Next
                isNewEntry = False
                btnSave.Text = "Update"
                isInsideLoadData = False
                fndTragetCode.MyReadOnly = True
            Else
                isNewEntry = True
                btnSave.Text = "Save"
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub DeleteData(ByVal strTargetCode As String)
        Try
            If clsCommon.myLen(strTargetCode) > 0 Then
                If clsTragetMasterHeadProductSale.fundelete(strTargetCode) Then
                    clsCommon.MyMessageBoxShow("Data deleted successfully.")
                    reset()
                End If
            Else
                clsCommon.MyMessageBoxShow("No Scheme found to delete.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(fndTragetCode.Value)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmTragetMaster_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress

    End Sub

  
    Private Sub fndTragetCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndTragetCode._MYNavigator
        Try
            LoadData(fndTragetCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndTragetCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTragetCode._MYValidating
        Dim qry As String = ""
        qry = "select Target_Code as Code,Target_Desc   from TSPL_TARGET_MASTER_HEAD"
        fndTragetCode.Value = clsCommon.ShowSelectForm("Target", qry, "Code", "", fndTragetCode.Value, "TSPL_TARGET_MASTER_HEAD.Target_Code", isButtonClicked)
        If clsCommon.myLen(fndTragetCode.Value) > 0 Then
            Dim objOT As clsTragetMasterHeadProductSale
            objOT = clsTragetMasterHeadProductSale.GetData(fndTragetCode.Value, NavigatorType.Current)
            If Not objOT Is Nothing Then
                LoadData(fndTragetCode.Value, NavigatorType.Current)
            End If
        End If

    End Sub
    Sub reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtfromDate.Value = txtToDate.Value.AddMonths(-1)
        fndTragetCode.Value = ""
        txtDesc.Text = ""
        gvItem.DataSource = Nothing
        gvCustomer.DataSource = Nothing
        LoadCriteria()
        LoadBlankItemGrid()
        LoadTragetTypes()
        gvItem.Rows.AddNew()
        LoadBlankCustomerGrid()
        lblCriteria.Text = ""
        btnSave.Text = "Save"
        fndTragetCode.MyReadOnly = False
        txtCriteria.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()

    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub RadMenuInport_Click(sender As Object, e As EventArgs) Handles RadMenuInport.Click
        ''import whole sheet
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim isSaved As Boolean = True
            Dim counter As Integer = 0

            If transportSql.importExcel(gv, "Target Description", "Document Date", "Target Type", "Start Date", "End Date", "Criteria", "Criteria Code", "Criteria Description", "Item Code", "UOM", "From Qty", "To Qty", "Scheme Item Code", "Qty", "Scheme UOM", "Customer Code") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim Target_Desc As String = Nothing
                    Dim Document_Date As String = Nothing
                    Dim Target_Type As String = Nothing
                    Dim StartDate As String = Nothing
                    Dim EndDate As String = Nothing
                    Dim Criteria As String = Nothing
                    Dim Criteria_Code As String = Nothing
                    Dim Criteria_Desc As String = Nothing
                    Dim Item_Code As String = Nothing
                    Dim ItemCodeUOM As String = Nothing
                    Dim From_Qty As Decimal = Nothing
                    Dim To_Qty As Decimal = Nothing
                    Dim SchemeItem_Code As String = Nothing
                    Dim SchemeItem_Desc As String = Nothing
                    Dim Scheme_Qty As Decimal = Nothing
                    Dim SchemeItem_UOM As String = Nothing
                    Dim Customer_Code As String = Nothing
                    Dim Target_Code As String = Nothing
                    Dim isNewEntry As Boolean = True

                    If isNewEntry Then
                        Target_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MAX(Target_Code) from TSPL_TARGET_MASTER_HEAD", trans))
                        If clsCommon.myLen(Target_Code) <= 0 Then
                            Target_Code = "TR000001"
                        Else
                            Target_Code = clsCommon.incval(Target_Code)
                        End If
                    End If

                    For Each grow As GridViewRowInfo In gv.Rows

                        Target_Desc = clsCommon.myCstr(grow.Cells("Target Description").Value)
                        ''--------------------------------------------------------------------------------------------------------
                        Document_Date = clsCommon.myCDate(grow.Cells("Document Date").Value)
                        If clsCommon.myLen(Document_Date) <= 0 Then
                            Throw New Exception("Please Fill Document Date !")
                        End If
                        ''--------------------------------------------------------------------------------------------------------
                        Target_Type = clsCommon.myCstr(grow.Cells("Target Type").Value)
                        If clsCommon.myLen(Target_Type) <= 0 Then
                            Throw New Exception("Please Fill Target Type !")
                        End If
                        ''--------------------------------------------------------------------------------------------------------
                        StartDate = clsCommon.myCDate(grow.Cells("Start Date").Value)
                        If clsCommon.myLen(StartDate) <= 0 Then
                            Throw New Exception("Please Fill Start Date !")
                        End If
                        ''--------------------------------------------------------------------------------------------------------
                        EndDate = clsCommon.myCDate(grow.Cells("End Date").Value)
                        If clsCommon.myLen(EndDate) <= 0 Then
                            Throw New Exception("Please Fill End Date !")
                        End If
                        ''------------------------------------------------------------------------------------------------------------
                        Criteria = clsCommon.myCstr(grow.Cells("Criteria").Value)
                        If clsCommon.myLen(Criteria) <= 0 Then
                            Throw New Exception("Please Fill Criteria !")
                        End If
                        ''------------------------------------------------------------------------------------------------------------
                        Criteria_Code = clsCommon.myCstr(grow.Cells("Criteria Code").Value)
                        If clsCommon.myLen(Criteria_Code) > 0 Then
                            Dim qry As String = "select CUST_CATEGORY_CODE from TSPL_CUSTOMER_CATEGORY_MASTER where CUST_CATEGORY_CODE='" + Criteria_Code + "'"
                            Criteria_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Criteria_Code) <= 0 Then
                                Throw New Exception("Criteria invalid !")
                            End If
                        Else
                            Throw New Exception("Please Fill Criteria !")
                        End If
                        Criteria_Desc = clsCommon.myCstr(grow.Cells("Criteria Description").Value)
                        ''------------------------------------------------------------------------------------------------------------
                        Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                        If clsCommon.myLen(Item_Code) > 0 Then
                            Dim qry As String = "select Item_Code from tspl_item_master where Item_Code='" + Item_Code + "'"
                            Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Item_Code) <= 0 Then
                                Throw New Exception("Item Code invalid !")
                            End If
                        Else
                            Throw New Exception("Please Fill Item Code !")
                        End If

                        ItemCodeUOM = clsCommon.myCstr(grow.Cells("UOM").Value)
                        If clsCommon.myLen(ItemCodeUOM) > 0 Then
                            Dim qry As String = "select Unit_Code from tspl_unit_master where Unit_Code='" + ItemCodeUOM + "'"
                            Dim ItemCode_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(ItemCode_UOM) <= 0 Then
                                Throw New Exception("Unit Code invalid !")
                            End If
                        Else
                            Throw New Exception("Please Fill Unit Code !")
                        End If
                        ''--------------------------------------------------------------------------------------------------------
                        From_Qty = clsCommon.myCdbl(grow.Cells("From Qty").Value)
                        If clsCommon.myLen(From_Qty) <= 0 Then
                            Throw New Exception("Please Fill From Qty !")
                        End If
                        ''--------------------------------------------------------------------------------------------------------
                        To_Qty = clsCommon.myCdbl(grow.Cells("To Qty").Value)
                        If clsCommon.myLen(To_Qty) <= 0 Then
                            Throw New Exception("Please Fill To Qty !")
                        End If
                        ''------------------------------------------------------------------------------------------------------------
                        SchemeItem_Code = clsCommon.myCstr(grow.Cells("Scheme Item Code").Value)
                        If clsCommon.myLen(SchemeItem_Code) > 0 Then
                            Dim qry As String = "select Item_Code from tspl_item_master where Item_Code='" + SchemeItem_Code + "'"
                            SchemeItem_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(SchemeItem_Code) <= 0 Then
                                Throw New Exception("Schmeme Item Code invalid !")
                            End If
                        Else
                            Throw New Exception("Please Fill Scheme Item Code !")
                        End If

                        SchemeItem_UOM = clsCommon.myCstr(grow.Cells("Scheme UOM").Value)
                        If clsCommon.myLen(SchemeItem_UOM) > 0 Then
                            Dim qry As String = "select Unit_Code from tspl_unit_master where Unit_Code='" + SchemeItem_UOM + "'"
                            Dim Scheme_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Scheme_UOM) <= 0 Then
                                Throw New Exception("Scheme Unit Code invalid !")
                            End If
                        Else
                            Throw New Exception("Please Fill Scheme Unit Code !")
                        End If
                        ''--------------------------------------------------------------------------------------------------------
                        Scheme_Qty = clsCommon.myCdbl(grow.Cells("Qty").Value)
                        If clsCommon.myLen(Scheme_Qty) <= 0 Then
                            Throw New Exception("Please Fill Scheme Qty !")
                        End If
                        ''-----------------------------------------------------------------------------------------------------------
                        Customer_Code = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                        If clsCommon.myLen(Customer_Code) > 0 Then
                            Dim qry As String = "select Cust_Code from TSPL_CUSTOMER_MASTER where Cust_Code='" + Customer_Code + "'"
                            Customer_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Customer_Code) <= 0 Then
                                Throw New Exception("Customer Code invalid !")
                            End If
                        Else
                            Throw New Exception("Please Fill Customer Code !")
                        End If

                        ''-------------------------------------------------------------------------------------------------------------


                        Dim collSchemeMaster As New Hashtable()
                        ''------------- Target Head
                        Dim qrycheck As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Target_code from tspl_target_master_head where Target_code='" & Target_Code & "'", trans))
                        If clsCommon.myLen(qrycheck) <= 0 Then
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Target_Code", Target_Code)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Target_Desc", Target_Desc)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "DocumentDate", clsCommon.GetPrintDate(Document_Date, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Target_Type", Target_Type)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "From_Date", clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(collSchemeMaster, "To_Date", clsCommon.GetPrintDate(EndDate, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Criteria", Criteria)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Criteria_Code", Criteria_Code)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Modify_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            clsCommonFunctionality.UpdateDataTable(collSchemeMaster, "TSPL_TARGET_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
                        End If
                        ''------------- Detail Target
                        Dim collDetail As New Hashtable()
                        Dim checkTargetDetail As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_code from tspl_target_master_detail where Target_code='" & Target_Code & "' and Item_Code='" & Item_Code & "'", trans))
                        If clsCommon.myLen(checkTargetDetail) <= 0 Then
                            clsCommon.AddColumnsForChange(collDetail, "Line_No", counter)
                            clsCommon.AddColumnsForChange(collDetail, "Target_Code", Target_Code)
                            clsCommon.AddColumnsForChange(collDetail, "Item_Code", Item_Code)
                            clsCommon.AddColumnsForChange(collDetail, "Unit_Code", ItemCodeUOM)
                            clsCommon.AddColumnsForChange(collDetail, "Qty", From_Qty)
                            clsCommon.AddColumnsForChange(collDetail, "To_Qty", To_Qty)
                            clsCommon.AddColumnsForChange(collDetail, "SchemeItem_Code", SchemeItem_Code)
                            clsCommon.AddColumnsForChange(collDetail, "SchemeQty", Scheme_Qty)
                            clsCommon.AddColumnsForChange(collDetail, "SchemeUOM", SchemeItem_UOM)
                            clsCommonFunctionality.UpdateDataTable(collDetail, "TSPL_TARGET_MASTER_Detail", OMInsertOrUpdate.Insert, "", trans)
                        End If

                        ''------------- Customer Scheme Target
                        Dim collScheme As New Hashtable()
                        clsCommon.AddColumnsForChange(collScheme, "Target_Code", Target_Code)
                        clsCommon.AddColumnsForChange(collScheme, "Cust_Code", Customer_Code)
                        clsCommonFunctionality.UpdateDataTable(collScheme, "TSPL_TARGET_MASTER_Scheme", OMInsertOrUpdate.Insert, "", trans)
                        counter += 1

                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub RadMenuExport_Click(sender As Object, e As EventArgs) Handles RadMenuExport.Click
        Dim qry As String = "select TSPL_TARGET_MASTER_HEAD.Target_Desc as [Target Description],convert(varchar,TSPL_TARGET_MASTER_HEAD.DocumentDate,103) as [Document Date],TSPL_TARGET_MASTER_HEAD.Target_Type as [Target Type] ,convert(varchar,TSPL_TARGET_MASTER_HEAD.From_Date,103) as [Start Date],convert(varchar,TSPL_TARGET_MASTER_HEAD.To_Date,103) as [End Date],TSPL_TARGET_MASTER_HEAD.Criteria  ,TSPL_TARGET_MASTER_HEAD.Criteria_Code as [Criteria Code],Case When Criteria='Customer Group' Then TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc When Criteria='Customer Category' then TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC Else '' End as [Criteria Description]  ,TSPL_TARGET_MASTER_Detail.Item_Code as [Item Code] ,TSPL_TARGET_MASTER_Detail.Unit_Code as [UOM] ,TSPL_TARGET_MASTER_Detail.Qty as [From Qty] ,TSPL_TARGET_MASTER_Detail.To_Qty as [To Qty],TSPL_TARGET_MASTER_Detail.SchemeItem_Code as [Scheme Item Code],TSPL_TARGET_MASTER_Detail.SchemeQty [Qty],TSPL_TARGET_MASTER_Detail.SchemeUOM as[Scheme UOM],TSPL_Target_Master_Scheme.Cust_Code as [Customer Code]  from TSPL_TARGET_MASTER_HEAD left outer join TSPL_TARGET_MASTER_Detail on TSPL_TARGET_MASTER_Detail.Target_Code =TSPL_TARGET_MASTER_HEAD.Target_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_TARGET_MASTER_Detail.Item_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_TARGET_MASTER_HEAD.Criteria_Code LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_TARGET_MASTER_HEAD.Criteria_Code left outer join TSPL_Target_Master_Scheme on TSPL_TARGET_MASTER_scheme.Target_Code=TSPL_TARGET_MASTER_HEAD.Target_Code "
        transportSql.ExporttoExcel(qry, Me)
    End Sub
End Class

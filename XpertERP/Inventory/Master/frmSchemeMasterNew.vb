'--Created By-[Pankaj Kumar Chaudhary]--against Ticket No -[]
Imports common
Imports System.Data.SqlClient

Public Class FrmSchemeMasterNew
    Inherits FrmMainTranScreen
    
    Const colLineNo As String = "LineNo"
    Const colICode As String = "ItemCode"
    Const colIName As String = "ItemName"
    Const colUOM As String = "UnitCode"
    Const colPriceDate As String = "PriceDate"
    Const colQty As String = "Quantity"
    Const colMRP As String = "MRP"
    Const colRemarks As String = "Remarks"

    Const colSelect As String = "Select"
    Const colCustCode As String = "Customer Code"
    Const colCustName As String = "CustName"

    Private isInsideLoadData As Boolean = False
    Private isFromLoad As Boolean = False
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim isNewEntry As Boolean = True

    Private Sub FrmSecondaryCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData(fndScheme.Value)
        End If
    End Sub

    Private Sub FrmSchemeMasterNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isFromLoad = True
        Dim ButtonToolTip As ToolTip = New ToolTip()
        SetUserMgmtNew()
        ValidateLength()
        LoadTypes()
        LoadCriteria()
        LoadBlankItemGrid()
        gvItem.Rows.AddNew()
        LoadBlankCustomerGrid()
        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
        isFromLoad = False
        txtAmount.Enabled = False
        txtPercentage.Enabled = False
        dtpInactive.Enabled = False
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmSchemeMasterNew)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub ValidateLength()
        txtDesc.MaxLength = 200
        txtComment.MaxLength = 200
    End Sub

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
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
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

        Dim repoUnitCode As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoUnitCode.HeaderText = "Unit Code"
        repoUnitCode.Name = colUOM
        repoUnitCode.Width = 80
        repoUnitCode.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoUnitCode)

        Dim repoPriceDate As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoPriceDate.FormatString = ""
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.Name = colPriceDate
        repoPriceDate.Width = 100
        gvItem.MasterTemplate.Columns.Add(repoPriceDate)

        Dim repoReqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReqQty.FormatString = ""
        repoReqQty.HeaderText = "Quantity"
        repoReqQty.Name = colQty
        repoReqQty.Width = 80
        repoReqQty.Minimum = 0
        repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReqQty)

        Dim repoMRP As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        gvItem.MasterTemplate.Columns.Add(repoMRP)

        Dim repoRemark As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemark.FormatString = ""
        repoRemark.HeaderText = "Remark"
        repoRemark.Name = colRemarks
        repoRemark.Width = 80
        gvItem.MasterTemplate.Columns.Add(repoRemark)

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
        repoCCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
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

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Quantitive")
        dt.Rows.Add("Cash")
        ddlType.DataSource = dt
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Code"
    End Sub

    Sub LoadCriteria()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Customer Group")
        dt.Rows.Add("Customer Category")
        dt.Rows.Add("Customer")
        ddlCriteria.DataSource = dt
        ddlCriteria.ValueMember = "Code"
        ddlCriteria.DisplayMember = "Code"
    End Sub

    Private Sub txtMainItem__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtMainItem._MYValidating
        qry = "Select Item_Code as Code, Item_Desc as Description from [TSPL_ITEM_MASTER]"
        txtMainItem.Value = clsCommon.ShowSelectForm("mainItemFinder@SCHMM", qry, "Code", "", txtMainItem.Value, "", isButtonClicked)
        lblMainItemDesc.Text = clsItemMaster.GetItemName(txtMainItem.Value, Nothing)
        txtUnitCode.Value = clsDBFuncationality.getSingleValue("select TOP(1) TSPL_ITEM_UOM_DETAIL.UOM_Code from TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + txtMainItem.Value + "'")
        lblUnitCodeDesc.Text = clsItemUOMDetails.GetName(txtUnitCode.Value)
        gvItem.Rows.Clear()
        'gvItem.Columns.Clear()
        gvItem.Rows.AddNew()
        fillMRP()
    End Sub

    Private Sub txtUnitCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtUnitCode._MYValidating
        qry = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code, TSPL_UNIT_MASTER.Unit_Desc as Description from TSPL_ITEM_UOM_DETAIL LEFT OUTER JOIN TSPL_UNIT_MASTER ON TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code"
        txtUnitCode.Value = clsCommon.ShowSelectForm("UOMFinder@SCHMM", qry, "Code", "Item_Code='" + txtMainItem.Value + "'", txtUnitCode.Value, "", isButtonClicked)
        lblUnitCodeDesc.Text = clsItemUOMDetails.GetName(txtUnitCode.Value)
        fillMRP()
    End Sub

    Private Sub txtCriteria__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCriteria._MYValidating
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

    Dim IsMRPLoad As Boolean = False
    Private Sub fillMRP()
        Try
            IsMRPLoad = True
            qry = "select distinct Item_Basic_Net  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + txtMainItem.Value + "' AND Uom='" + txtUnitCode.Value + "'"
            ddlmrp.DataSource = clsDBFuncationality.GetDataTable(qry)
            ddlmrp.ValueMember = "Item_Basic_Net"
            ddlmrp.DisplayMember = "Item_Basic_Net"
            IsMRPLoad = False
            FillBasicPrice()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
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
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlType.SelectedIndexChanged
        Try
            txtAmount.Text = ""
            txtPercentage.Text = ""
            LoadBlankItemGrid()
            If Not isFromLoad Then
                If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
                    txtAmount.Enabled = False
                    txtPercentage.Enabled = False
                    gvItem.Rows.AddNew()
                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal Then
                    txtAmount.Enabled = True
                    txtPercentage.Enabled = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub ddlmrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlmrp.SelectedIndexChanged
        If Not IsMRPLoad Then
            FillBasicPrice()
        End If
    End Sub

    Private Sub FillBasicPrice()
        If clsCommon.myLen(ddlmrp.SelectedValue) > 0 Then
            qry = "select distinct Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + txtMainItem.Value + "' AND Uom='" + txtUnitCode.Value + "' AND Item_Basic_Net='" + clsCommon.myCstr(ddlmrp.SelectedValue) + "'"
            ddlBasicPrice.DataSource = clsDBFuncationality.GetDataTable(qry)
            ddlBasicPrice.ValueMember = "Item_Basic_Price"
            ddlBasicPrice.DisplayMember = "Item_Basic_Price"
        End If
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

    Private Sub fndScheme__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndScheme._MYValidating

        'qry = "Select Scheme_Code as Code, Scheme_Desc as Description, Scheme_Type, TSPL_SCHEME_MASTER_NEW.Item_Code+' - '+TSPL_ITEM_MASTER.Item_Desc as Item, Status from TSPL_SCHEME_MASTER_NEW Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SCHEME_MASTER_NEW.Item_Code"
        'fndScheme.Value = clsCommon.ShowSelectForm("SchemeFinder@SCHM", qry, "Code", "", fndScheme.Value, "", isButtonClicked)
        fndScheme.Value = clsSchemeMaster.getFinder("", fndScheme.Value, isButtonClicked)
        LoadData(fndScheme.Value, NavigatorType.Current)
    End Sub

    Private Sub fndScheme__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndScheme._MYNavigator
        Try
            qry = "select count(*) from TSPL_SCHEME_MASTER_NEW where Scheme_Code='" + fndScheme.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                fndScheme.MyReadOnly = False
            Else
                fndScheme.MyReadOnly = True
            End If
            LoadData(fndScheme.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strSecCustomerCode As String, ByVal NavType As NavigatorType)
        Try
            Reset()
            Dim obj As New clsSchemeMaster
            obj = clsSchemeMaster.GetData(strSecCustomerCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Scheme_Code) > 0 Then
                isInsideLoadData = True
                fndScheme.Value = obj.Scheme_Code
                txtDesc.Text = obj.Scheme_Desc
                dtpScheme.Value = obj.Start_Date
                ddlType.SelectedValue = obj.Scheme_Type
                txtMainItem.Value = obj.Item_Code
                lblMainItemDesc.Text = obj.Item_Desc
                txtUnitCode.Value = obj.Unit_Code
                lblUnitCodeDesc.Text = obj.Unit_Desc
                txtQty.Text = obj.Item_Qty
                fillMRP()
                '' Anubhooti 07-Oct-2014 BM00000004181 (MRP value should be fetched using selectedvalue not from text property)
                ddlmrp.SelectedValue = obj.MRP
                ddlBasicPrice.Text = obj.Basic_Price
                txtPercentage.Text = obj.Percentage
                txtAmount.Text = obj.Amount
                chkInactive.Checked = IIf(clsCommon.CompairString(obj.Status, "InActive") = CompairStringResult.Equal, True, False)
                If chkInactive.Checked Then
                    dtpInactive.Value = obj.End_Date
                    dtpInactive.Enabled = True
                Else
                    dtpInactive.Enabled = False
                End If
                ddlCriteria.SelectedValue = obj.Criteria
                txtCriteria.Value = obj.Criteria_Code
                lblCriteria.Text = obj.Criteria_Desc
                txtComment.Text = obj.Comments

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields
                Dim LineNo As Integer = 0
                For Each objDTL As clsSchemeDettail In obj.ArrDTL
                    LineNo += 1
                    gvItem.CurrentRow.Cells(colLineNo).Value = LineNo
                    gvItem.CurrentRow.Cells(colICode).Value = objDTL.Item_Code
                    gvItem.CurrentRow.Cells(colIName).Value = objDTL.Item_Desc
                    gvItem.CurrentRow.Cells(colQty).Value = objDTL.Qty
                    gvItem.CurrentRow.Cells(colUOM).Value = objDTL.Unit_Code
                    gvItem.CurrentRow.Cells(colMRP).Value = objDTL.MRP
                    gvItem.CurrentRow.Cells(colPriceDate).Value = objDTL.Price_Date
                    gvItem.CurrentRow.Cells(colRemarks).Value = objDTL.Remarks
                    gvItem.Rows.AddNew()
                Next

                LineNo = 0
                For Each objSchmBen As clsSchemeBenificiary In obj.ArrSchmBen
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
            Else
                isNewEntry = True
                btnSave.Text = "Save"
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Dim MainPriceCode As Double
        Dim GridPriceCode As Double
        Dim linno As Integer = 0

        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please enter Scheme Description")
            txtDesc.Focus()
            Return False
        ElseIf clsCommon.myLen(txtMainItem.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select main item.")
            txtMainItem.Focus()
            Return False
        ElseIf clsCommon.myLen(txtUnitCode) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select unit code.")
            txtUnitCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtQty.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please enter Main Item Quantity.")
            txtQty.Focus()
            Return False
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
            If gvItem.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast single item.")
                gvItem.Focus()
                Return False
            End If
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(txtPercentage.Text) <= 0 And clsCommon.myCdbl(txtAmount.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please enter Percentage or Amount.")
                txtAmount.Focus()
                Return False
            End If
        ElseIf dtpScheme.Value.Date < dtpInactive.Value.Date Then
            common.clsCommon.MyMessageBoxShow(Me, "inactive date can not be before than scheme date.")
            dtpInactive.Focus()
            Return False
        End If
        '' Anubhooti 07-Oct-2014 BM00000004207
        If clsCommon.myLen(txtMainItem.Value) > 0 Then
            MainPriceCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) From TSPL_ITEM_PRICE_MASTER where Item_Code ='" & clsCommon.myCstr(txtMainItem.Value) & "'"))
            If MainPriceCode = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please make price code for main item ( " & clsCommon.myCstr(txtMainItem.Value) & " ).")
                txtMainItem.Focus()
                Return False
            End If
        End If
        For Each grow As GridViewRowInfo In gvItem.Rows
            If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                linno += 1
                GridPriceCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) From TSPL_ITEM_PRICE_MASTER where Item_Code ='" & clsCommon.myCstr(grow.Cells(colICode).Value) & "'"))
                If GridPriceCode = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please make price code for item ( " & clsCommon.myCstr(grow.Cells(colICode).Value) & " ) at line no. " + clsCommon.myCstr(linno) + ".")
                    Return False
                End If
            End If
        Next
        ''
        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmSchemeMasterNew, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsSchemeMaster()
                obj.Scheme_Code = clsCommon.myCstr(fndScheme.Value)
                obj.Scheme_Desc = clsCommon.myCstr(txtDesc.Text)
                obj.Start_Date = clsCommon.myCDate(dtpScheme.Value)
                obj.Scheme_Type = clsCommon.myCstr(ddlType.SelectedValue)
                obj.Item_Code = clsCommon.myCstr(txtMainItem.Value)
                obj.Unit_Code = clsCommon.myCstr(txtUnitCode.Value)
                obj.Item_Qty = clsCommon.myCdbl(txtQty.Text)
                obj.MRP = clsCommon.myCdbl(ddlmrp.SelectedValue)
                obj.Basic_Price = clsCommon.myCdbl(ddlBasicPrice.SelectedValue)
                obj.Percentage = clsCommon.myCdbl(txtPercentage.Text)
                obj.Amount = clsCommon.myCdbl(txtAmount.Text)
                obj.Status = IIf(chkInactive.Checked, "InActive", "Active")
                If chkInactive.Checked Then
                    obj.End_Date = clsCommon.myCDate(dtpInactive.Value)
                End If
                obj.Criteria = ddlCriteria.SelectedValue
                obj.Criteria_Code = clsCommon.myCstr(txtCriteria.Value)
                obj.Comments = clsCommon.myCstr(txtComment.Text)
                obj.ArrDTL = New List(Of clsSchemeDettail)

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields

                '============================Scheme Item Information==============================
                Dim Count As Integer = 0
                If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
                    obj.ArrDTL = New List(Of clsSchemeDettail)
                    For Each grow As GridViewRowInfo In gvItem.Rows
                        If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                            Dim objTr As New clsSchemeDettail()
                            objTr.Scheme_Code = clsCommon.myCstr(fndScheme.Value)
                            objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                            objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                            objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                            objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                            objTr.Price_Date = grow.Cells(colPriceDate).Value
                            objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                            obj.ArrDTL.Add(objTr)
                            Count += 1
                        End If
                    Next
                End If

                '-----------------Customer Information-----------------
                Count = 0
                obj.ArrSchmBen = New List(Of clsSchemeBenificiary)
                For Each grow As GridViewRowInfo In gvCustomer.Rows
                    If grow.Cells(colSelect).Value = True Then
                        Dim objTr As New clsSchemeBenificiary()
                        objTr.Scheme_Code = clsCommon.myCstr(fndScheme.Value)
                        objTr.Cust_Code = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                        obj.ArrSchmBen.Add(objTr)
                        Count += 1
                    End If
                Next
                If Count <= 0 Then
                    If clsCommon.myLen(txtCriteria.Value) <= 0 Then
                        Throw New Exception("Please select atleast sing customer.")
                    End If
                End If
                '==================Detail Section Ends Here=======================
                obj.SaveData(obj, isNewEntry)
                LoadData(obj.Scheme_Code, NavigatorType.Current)
                isNewEntry = False
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(fndScheme.Value)
    End Sub

    Private Sub DeleteData(ByVal strSchemeCode As String)
        Try
            If clsCommon.myLen(strSchemeCode) > 0 Then
                If clsSchemeMaster.fundelete(strSchemeCode) Then
                    clsCommon.MyMessageBoxShow(Me, "Data deleted successfully.")
                    Reset()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Scheme found to delete.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub Reset()
        fndScheme.Value = ""
        txtDesc.Text = ""
        txtMainItem.Value = ""
        lblMainItemDesc.Text = ""
        txtQty.Text = ""
        txtUnitCode.Value = ""
        lblUnitCodeDesc.Text = ""
        ddlmrp.DataSource = Nothing
        ddlBasicPrice.DataSource = Nothing
        txtComment.Text = ""
        txtCriteria.Value = ""
        lblCriteria.Text = ""
        LoadBlankItemGrid()
        If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
            gvItem.Rows.AddNew()
            txtPercentage.Enabled = False
            txtAmount.Enabled = False
        End If
        LoadBlankCustomerGrid()
        gvCustomer.DataSource = Nothing
        chkInactive.Checked = False
        dtpInactive.Enabled = False
        dtpScheme.Value = CurrentDate
        dtpInactive.Value = CurrentDate
        txtPercentage.Text = ""
        txtAmount.Text = ""
        txtComment.Text = ""
        btnSelect.Text = "Select All"
        btnSave.Text = "Save"
        isNewEntry = True
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        
    End Sub
    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        
    End Sub
  

    Private Sub rmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiClose.Click
        Me.Close()
    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Dim MainFreshType As String = ""
        Dim MainMilkType As String = ""
        Dim MainProdType As Double = 0
        Dim Whr As String = ""
        '' Anubhooti 07-Oct-2014 BM00000004181 (Fresh/Milk Item Should fill fresh/Milk Items in grid else if item is not both fresh/milk then it will be consider as product type)
        If clsCommon.myLen(gvItem.CurrentRow.Cells(colICode).Value) > 0 Then
            MainFreshType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Is_FreshItem,'') As Is_FreshItem From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(txtMainItem.Value) & "'"))
            If clsCommon.CompairString(MainFreshType, "1") = CompairStringResult.Equal Then
                Whr = " ISNULL(Is_FreshItem,'')=1"
            End If
            MainMilkType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Product_Type,'') As Product_Type  From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(txtMainItem.Value) & "'"))
            If clsCommon.CompairString(MainMilkType, "MI") = CompairStringResult.Equal Then
                Whr = " ISNULL(Product_Type,'') ='MI'"
            End If
            MainProdType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*)  From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(txtMainItem.Value) & "' AND (ISNULL(Is_FreshItem,'')<>1 AND ISNULL(Product_Type,'') <>'MI')"))
            If MainProdType > 0 Then
                Whr = " ISNULL(Is_FreshItem,'')<>1 AND ISNULL(Product_Type,'') <>'MI'"
            End If
        End If
        If (Not isInsideLoadData) Then
            If e.Column Is gvItem.Columns(colICode) Then
                qry = "Select Item_Code as Code, Item_Desc as Description from TSPL_ITEM_MASTER"
                gvItem.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("CustFinder@SM", qry, "Code", Whr, gvItem.CurrentRow.Cells(colICode).Value, "Code", False)
                gvItem.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(gvItem.CurrentRow.Cells(colICode).Value, Nothing)
            End If
        End If
    End Sub

    Private Sub gvItem_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvItem.CurrentColumnChanged
        If gvCustomer.RowCount > 0 Then
            Dim intCurrRow As Integer = gvItem.CurrentRow.Index
            gvItem.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvCustomer.Rows.Count - 1 Then
                gvCustomer.Rows.AddNew()
                gvCustomer.CurrentRow = gvCustomer.Rows(intCurrRow)
                gvCustomer.CurrentRow.Cells(colSelect).Value = True
            End If
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

    Private Sub gvCustomer_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvCustomer.CurrentColumnChanged
        If gvCustomer.RowCount > 0 Then
            Dim intCurrRow As Integer = gvCustomer.CurrentRow.Index
            gvCustomer.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvCustomer.Rows.Count - 1 Then
                gvCustomer.Rows.AddNew()
                gvCustomer.CurrentRow = gvCustomer.Rows(intCurrRow)
            End If
        End If
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

    Private Sub gvItem_EditorRequired(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gvItem.EditorRequired
        Try
            Dim mrp As GridViewComboBoxColumn = TryCast(gvItem.Columns(colMRP), GridViewComboBoxColumn)
            Dim unit As GridViewComboBoxColumn = TryCast(gvItem.Columns(colUOM), GridViewComboBoxColumn)
            Dim priceDate As GridViewComboBoxColumn = TryCast(gvItem.Columns(colPriceDate), GridViewComboBoxColumn)
            Dim ds As New DataSet()
            If gvItem.CurrentColumn.Name = colUOM Then
                qry = "select distinct Uom  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + gvItem.CurrentRow.Cells(colICode).Value + "'"
                unit.DataSource = clsDBFuncationality.GetDataTable(qry)
                unit.ValueMember = "Uom"
            ElseIf gvItem.CurrentColumn.Name = colPriceDate Then
                qry = "select distinct convert(varchar(10), Start_date,103) as Start_Date  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + gvItem.CurrentRow.Cells(colICode).Value + "' AND Uom='" + gvItem.CurrentRow.Cells(colUOM).Value + "'"
                priceDate.DataSource = clsDBFuncationality.GetDataTable(qry)
                priceDate.ValueMember = "Start_Date"
            ElseIf gvItem.CurrentColumn.Name = colMRP Then
                qry = "select distinct Item_Basic_Net  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + gvItem.CurrentRow.Cells(colICode).Value + "' AND Start_Date='" + Format(CDate(gvItem.CurrentRow.Cells(colPriceDate).Value), "MM/dd/yyyy") + "' AND Uom='" + gvItem.CurrentRow.Cells(colUOM).Value + "'"
                mrp.DataSource = clsDBFuncationality.GetDataTable(qry)
                mrp.ValueMember = "Item_Basic_Net"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtPercentage_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPercentage.Leave
        If clsCommon.myCdbl(txtPercentage.Text) > 0 Then
            txtAmount.Text = ""
        End If
    End Sub

    Private Sub txtAmount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.Leave
        If clsCommon.myCdbl(txtAmount.Text) > 0 Then
            txtPercentage.Text = ""
        End If
    End Sub

    Private Sub chkInactive_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        dtpInactive.Enabled = chkInactive.Checked
    End Sub

    
    Private Sub export_main_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles export_main.Click
        Try
            qry = "select count(*) from TSPL_SCHEME_MASTER_NEW"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select TSPL_SCHEME_MASTER_NEW.Scheme_Code as [Scheme Code],TSPL_SCHEME_MASTER_NEW.Scheme_Desc as [Description],TSPL_SCHEME_MASTER_NEW.Start_Date as [Start Date],TSPL_SCHEME_MASTER_NEW.End_Date as [End Date],TSPL_SCHEME_MASTER_NEW.Status as [Active Status],TSPL_SCHEME_MASTER_NEW.Item_Code as [Main Item Code],tspl_item_master.item_desc as [Main Item Name],TSPL_SCHEME_MASTER_NEW.Unit_Code as [Unit Code],tspl_unit_master.unit_desc as [Unit],TSPL_SCHEME_MASTER_NEW.Scheme_Type as [Scheme Type],TSPL_SCHEME_MASTER_NEW.Item_Qty as [Main Qty],TSPL_SCHEME_MASTER_NEW.Basic_Price as [Basic price],TSPL_SCHEME_MASTER_NEW.Percentage,TSPL_SCHEME_MASTER_NEW.MRP,TSPL_SCHEME_MASTER_NEW.Amount,TSPL_SCHEME_MASTER_NEW.Comments from TSPL_SCHEME_MASTER_NEW left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_MASTER_NEW.Item_Code left outer join tspl_unit_master on tspl_unit_master.unit_code=TSPL_SCHEME_MASTER_NEW.Unit_Code"
            Else
                qry = "select '' as [Scheme Code],'' as [Description],'' as [Start Date],'' as [End Date],'' as [Active Status],'' as [Main Item Code],'' as [Main Item Name],'' as [Unit Code],'' as [Unit],'' as [Scheme Type],'' as [Main Qty],'' as [Basic price],'' as Percentage,'' as MRP,'' as Amount,'' as Comments"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub export_beneficial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles export_beneficial.Click
        Try
            qry = "select count(*) from TSPL_SCHEME_MASTER_NEW"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select TSPL_SCHEME_MASTER_NEW.Scheme_Code as [Scheme Code],TSPL_SCHEME_MASTER_NEW.scheme_desc as [Scheme Description],TSPL_SCHEME_DETAIL_NEW.Item_Code as [Item Code],tspl_item_master.item_desc as [Item Name],TSPL_SCHEME_DETAIL_NEW.Qty,TSPL_SCHEME_DETAIL_NEW.Unit_Code as [Unit Code],tspl_unit_master.unit_code as Unit,TSPL_SCHEME_DETAIL_NEW.MRP,TSPL_SCHEME_DETAIL_NEW.Price_Date as [Price Date],TSPL_SCHEME_DETAIL_NEW.Basic_Price as [Basic Price],TSPL_SCHEME_DETAIL_NEW.Remarks from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code left outer join tspl_unit_master on tspl_unit_master.unit_code=TSPL_SCHEME_DETAIL_NEW.Unit_Code"
            Else
                qry = "select '' as [Scheme Code],'' as [Scheme Description],'' as [Item Code],'' as [Item Name],'' as Qty,'' as [Unit Code],'' as Unit,'' as MRP,'' as [Price Date],'' as [Basic Price],'' as Remarks"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Export_Criteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export_Criteria.Click
        Try
            qry = "select count(*) from TSPL_SCHEME_MASTER_NEW"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select TSPL_SCHEME_MASTER_NEW.scheme_code as [Scheme Code],TSPL_SCHEME_MASTER_NEW.scheme_desc as [Scheme Description],TSPL_SCHEME_MASTER_NEW.Criteria_Code as [Criteria Code],TSPL_SCHEME_MASTER_NEW.Criteria as Criteria,TSPL_SCHEME_BENEFICIARY.cust_code as [Customer Code],tspl_customer_master.customer_name as [Cutomer Name],tspl_customer_master.add1 as Add1,tspl_customer_master.add2 as Add2,tspl_customer_master.add3 as Add3 from TSPL_SCHEME_BENEFICIARY left outer join TSPL_SCHEME_MASTER_NEW on TSPL_SCHEME_MASTER_NEW.scheme_code=TSPL_SCHEME_BENEFICIARY.scheme_code left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SCHEME_BENEFICIARY.cust_code"
            Else
                qry = "select '' as [Scheme Code],'' as [Scheme Description],'' as [Criteria Code],'' as Criteria,'' as [Customer Code],'' as [Cutomer Name],'' as Add1,'' as Add2,'' as Add3"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Import_Main_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import_Main.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim isSaved As Boolean = True
            If transportSql.importExcel(gv, "Scheme Code", "Description", "Start Date", "End Date", "Active Status", "Main Item Code", "Main Item Name", "Unit Code", "Unit", "Scheme Type", "Main Qty", "Basic price", "Percentage", "MRP", "Amount", "Comments") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    
                    Dim Scheme_Code As String = Nothing
                    Dim Scheme_Desc As String = Nothing
                    Dim Start_Date As Date = Nothing
                    Dim End_Date As Date = Nothing
                    Dim Scheme_Type As String = Nothing
                    Dim Item_Code As String = Nothing
                    Dim Item_Desc As String = Nothing
                    Dim Unit_Code As String = Nothing
                    Dim Unit_Desc As String = Nothing
                    Dim Item_Qty As Double = 0.0
                    Dim MRP As Double = 0.0
                    Dim Basic_Price As Double = 0.0
                    Dim Amount As Double = 0.0
                    Dim Percentage As Double = 0.0
                    Dim Status As String = Nothing
                    Dim comnt As String = ""
                    Dim counter As Integer = 0

                    For Each grow As GridViewRowInfo In gv.Rows
                        Scheme_Code = clsCommon.myCstr(grow.Cells("Scheme Code").Value)

                        If clsCommon.myLen(Scheme_Code) <= 0 Then
                            Throw New Exception("Please Fill Scheme Code")
                        End If
                        Scheme_Desc = clsCommon.myCstr(grow.Cells("Description").Value)
                        Try
                            Start_Date = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("start date").Value), "dd/MMM/yyyy")
                        Catch exx As Exception
                        End Try

                        Try
                            End_Date = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("end date").Value), "dd/MMM/yyyy")
                        Catch exx1 As Exception
                        End Try
                        Status = clsCommon.myCstr(grow.Cells("Active Status").Value)

                        If clsCommon.myLen(Status) <= 0 Then
                            Throw New Exception("Please Fill Status(Active/InActive)")
                        End If

                        Item_Code = clsCommon.myCstr(grow.Cells("main item code").Value)
                        Item_Desc = clsCommon.myCstr(grow.Cells("main item name").Value)

                        If clsCommon.myLen(Item_Code) <= 0 AndAlso clsCommon.myLen(Item_Desc) <= 0 Then
                            Throw New Exception("Please Fill Item Code Or Item Description")
                        End If

                        If clsCommon.myLen(Item_Desc) > 0 Then
                            Dim qry As String = "select item_code from tspl_item_master where item_desc='" & Item_Desc & "'"
                            Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(Item_Code) <= 0 Then
                            Throw New Exception("Please Fill Item Code Of Item [" + Item_Desc + "],Or Make Its Master First")
                        End If

                        Unit_Code = clsCommon.myCstr(grow.Cells("unit code").Value)
                        Unit_Desc = clsCommon.myCstr(grow.Cells("unit").Value)

                        If clsCommon.myLen(Unit_Code) <= 0 AndAlso clsCommon.myLen(Unit_Desc) <= 0 Then
                            Throw New Exception("Please Fill Unit Code/Unit")
                        End If

                        If clsCommon.myLen(Unit_Desc) > 0 Then
                            qry = "select unit_code from tspl_unit_master where unit_desc='" + Unit_Desc + "'"
                            Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(Unit_Code) <= 0 Then
                            Throw New Exception("Please Fill Unit Code For Unit[" + Unit_Desc + "] Or Make Unit Master First")
                        End If

                        Scheme_Type = clsCommon.myCstr(grow.Cells("scheme type").Value)
                        Item_Qty = clsCommon.myCdbl(grow.Cells("main qty").Value)
                        Basic_Price = clsCommon.myCdbl(grow.Cells("basic price").Value)
                        Percentage = clsCommon.myCdbl(grow.Cells("percentage").Value)
                        MRP = clsCommon.myCdbl(grow.Cells("Mrp").Value)
                        Amount = clsCommon.myCdbl(grow.Cells("amount").Value)
                        comnt = clsCommon.myCstr(grow.Cells("comments").Value)

                        'If counter = 0 Then
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_MASTER_NEW WHERE Scheme_Code ='" + Scheme_Code + "'", trans)
                        'End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Scheme_Desc", Scheme_Desc)
                        clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(Start_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Scheme_Type", Scheme_Type)
                        clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                        clsCommon.AddColumnsForChange(coll, "Unit_Code", Unit_Code)
                        clsCommon.AddColumnsForChange(coll, "Item_Qty", Item_Qty)
                        clsCommon.AddColumnsForChange(coll, "MRP", MRP)
                        clsCommon.AddColumnsForChange(coll, "Basic_Price", Basic_Price)
                        clsCommon.AddColumnsForChange(coll, "Amount", Amount)
                        clsCommon.AddColumnsForChange(coll, "Percentage", Percentage)
                        clsCommon.AddColumnsForChange(coll, "Status", Status)
                        If clsCommon.CompairString(Status, "InActive") = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(End_Date, "dd/MMM/yyyy"))
                        End If

                        clsCommon.AddColumnsForChange(coll, "Comments", comnt)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Scheme_Code", Scheme_Code)
                        clsCommon.AddColumnsForChange(coll, "criteria", "Customer")
                        clsCommon.AddColumnsForChange(coll, "criteria_code", "Customer")

                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW", OMInsertOrUpdate.Update, " TSPL_SCHEME_MASTER_NEW.scheme_code='" + Scheme_Code + "'", trans)
                        End If
                        counter += 1
                    Next
                    trans.Commit()

                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub import_beneficial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles import_beneficial.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim issaved As Boolean = True
            If transportSql.importExcel(gv, "Scheme Code", "Scheme Description", "Item Code", "Item Name", "Qty", "Unit Code", "Unit", "MRP", "Price Date", "Basic Price", "Remarks") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try

                    Dim Scheme_Code As String = Nothing
                    Dim desc As String = Nothing
                    Dim item_code As String = Nothing
                    Dim item_desc As String = Nothing
                    Dim qty As Decimal = Nothing
                    Dim unit_code As String = Nothing
                    Dim unit_desc As String = Nothing
                    Dim MRP As Decimal = Nothing
                    Dim Price_date As Date = Nothing
                    Dim basic_price As Decimal = Nothing
                    Dim remarks As String = Nothing
                    Dim counter As Integer = 0

                    For Each grow As GridViewRowInfo In gv.Rows
                        Scheme_Code = clsCommon.myCstr(grow.Cells("Scheme Code").Value)
                        desc = clsCommon.myCstr(grow.Cells("scheme description").Value)
                        If clsCommon.myLen(Scheme_Code) <= 0 AndAlso clsCommon.myLen(desc) <= 0 Then
                            Throw New Exception("Please Fill Scheme Code/Scheme Description")
                        End If

                        If clsCommon.myLen(desc) > 0 Then
                            Dim qry As String = "select scheme_code from TSPL_SCHEME_MASTER_NEW where scheme_desc='" + desc + "'"
                            Scheme_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Scheme_Code) <= 0 Then
                                Throw New Exception("Please Fill Scheme Code For Scheme [" + desc + "] Or Make Scheme Detail Entry First")
                            End If
                        End If

                        item_code = clsCommon.myCstr(grow.Cells("item code").Value)
                        item_desc = clsCommon.myCstr(grow.Cells("item name").Value)

                        If clsCommon.myLen(item_code) <= 0 AndAlso clsCommon.myLen(item_desc) <= 0 Then
                            Throw New Exception("Please Fill Item Code Or Item Description")
                        End If

                        If clsCommon.myLen(item_desc) > 0 Then
                            qry = "select item_code from tspl_item_master where item_desc='" & item_desc & "'"
                            item_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(item_code) <= 0 Then
                            Throw New Exception("Please Fill Item Code Of Item [" + item_desc + "],Or Make Its Master First")
                        End If

                        unit_code = clsCommon.myCstr(grow.Cells("unit code").Value)
                        unit_desc = clsCommon.myCstr(grow.Cells("unit").Value)

                        If clsCommon.myLen(unit_code) <= 0 AndAlso clsCommon.myLen(unit_desc) <= 0 Then
                            Throw New Exception("Please Fill Unit Code/Unit")
                        End If

                        If clsCommon.myLen(unit_desc)>0 Then
                            qry = "select unit_code from tspl_unit_master where unit_desc='" + unit_desc + "'"
                            unit_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(unit_code) <= 0 Then
                            Throw New Exception("Please Fill Unit Code For Unit[" + unit_desc + "] Or Make Unit Master First")
                        End If

                        qty = clsCommon.myCdbl(grow.Cells("qty").Value)
                        MRP = clsCommon.myCdbl(grow.Cells("mrp").Value)
                        basic_price = clsCommon.myCdbl(grow.Cells("basic price").Value)
                        remarks = clsCommon.myCstr(grow.Cells("remarks").Value)
                        Price_date = clsCommon.myCDate(grow.Cells("price date").Value)

                        'If counter = 0 Then
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_DETAIL_NEW WHERE Scheme_Code ='" + Scheme_Code + "' and item_code='" + item_code + "'", trans)
                        'End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "scheme_code", Scheme_Code)
                        clsCommon.AddColumnsForChange(coll, "item_code", item_code)
                        clsCommon.AddColumnsForChange(coll, "qty", qty)
                        clsCommon.AddColumnsForChange(coll, "unit_code", unit_code)
                        clsCommon.AddColumnsForChange(coll, "mrp", MRP)
                        clsCommon.AddColumnsForChange(coll, "price_date", clsCommon.GetPrintDate(Price_date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "basic_price", basic_price)
                        clsCommon.AddColumnsForChange(coll, "remarks", remarks)

                        If check <= 0 Then
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Update, " TSPL_SCHEME_DETAIL_NEW.scheme_code='" + Scheme_Code + "' and TSPL_SCHEME_DETAIL_NEW.item_code='" + item_code + "'", trans)
                        End If

                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub import_criteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles import_criteria.Click

        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim issaved As Boolean = True
            If transportSql.importExcel(gv, "Scheme Code", "Scheme Description", "Criteria Code", "Criteria", "Customer Code", "Cutomer Name", "Add1", "Add2", "Add3") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try

                    Dim Scheme_Code As String = Nothing
                    Dim desc As String = Nothing
                    Dim code As String = Nothing
                    Dim criteria As String = Nothing
                    Dim cust_code As String = Nothing
                    Dim cust_name As String = Nothing
                    Dim add1 As String = Nothing
                    Dim add2 As String = Nothing
                    Dim add3 As String = Nothing
                    Dim counter As Integer = 0

                    For Each grow As GridViewRowInfo In gv.Rows
                        Scheme_Code = clsCommon.myCstr(grow.Cells("Scheme Code").Value)
                        desc = clsCommon.myCstr(grow.Cells("scheme description").Value)
                        If clsCommon.myLen(Scheme_Code) <= 0 AndAlso clsCommon.myLen(desc) <= 0 Then
                            Throw New Exception("Please Fill Scheme Code/Scheme Description")
                        End If

                        Dim qry As String = ""

                        If clsCommon.myLen(desc) > 0 Then
                            qry = "select scheme_code from TSPL_SCHEME_MASTER_NEW where scheme_desc='" + desc + "'"
                            Scheme_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(Scheme_Code) <= 0 Then
                            Throw New Exception("Please Fill Scheme Code For Scheme [" + desc + "] Or Make Scheme Detail Entry First")
                        End If

                        code = clsCommon.myCstr(grow.Cells("Criteria Code").Value)
                        criteria = clsCommon.myCstr(grow.Cells("Criteria").Value)

                        If clsCommon.myLen(criteria) <= 0 Then
                            Throw New Exception("Please Fill Criteria(Customer/Customer Group)")
                        End If

                        cust_code = clsCommon.myCstr(grow.Cells("customer code").Value)
                        cust_name = clsCommon.myCstr(grow.Cells("cutomer name").Value)

                        If clsCommon.myLen(cust_code) <= 0 AndAlso clsCommon.myLen(cust_name) <= 0 Then
                            Throw New Exception("Please Fill Customer Code Or Customer Name")
                        End If

                        If clsCommon.myLen(cust_name) > 0 Then
                            qry = "select cust_code from tspl_customer_master where customer_name='" + cust_name + "'" ' and (add1+' '+add2+' '+add3)=(" + add1 + "+' '+" + add2 + "+' '+" + add3 + ")"
                            cust_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(cust_code) <= 0 Then
                            Throw New Exception("Please Fill Customer Code Of Customer [" + cust_name + "],Or Make Its Master First")
                        End If

                        add1 = clsCommon.myCstr(grow.Cells("add1").Value)
                        add2 = clsCommon.myCstr(grow.Cells("add2").Value)
                        add3 = clsCommon.myCstr(grow.Cells("add3").Value)


                        'If counter = 0 Then
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code ='" + Scheme_Code + "' and cust_code='" + cust_code + "'", trans)
                        'End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "scheme_code", Scheme_Code)
                        clsCommon.AddColumnsForChange(coll, "cust_code", cust_code)

                        If check <= 0 Then
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Update, " TSPL_SCHEME_BENEFICIARY.Scheme_Code ='" + Scheme_Code + "' and TSPL_SCHEME_BENEFICIARY.cust_code='" + cust_code + "'", trans)
                        End If

                        qry = "update TSPL_SCHEME_MASTER_NEW set Criteria='" + criteria + "',Criteria_Code='" + code + "' where scheme_code='" + Scheme_Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
End Class


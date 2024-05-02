Imports common
Imports System.Data.SqlClient

Public Class FrmCustomerItemDetails
    Inherits FrmMainTranScreen

#Region "Variables"
    Public isFromApprovalForm As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colitemno As String = "Item No"
    Const coldesc As String = "Description"
    Const coluom As String = "UOM"
    Const colrate As String = "Item Rate"
    Const colMRP As String = "MRP"
     Const colisMRPMandatory As String = "colisMRPMandatory"
    Const colDiscount As String = "COLDISCOUNT"
    Const colDiscount2 As String = "COLDISCOUNT2"
    Const colMinRate As String = "COLMinRate"
    Const colCustitem As String = "Vendor Item No"
    Const colStartDate As String = "StartDate"
    Const colEndDate As String = "EndDate"
    Const colApprovalRate As String = "ApprovalRate"
#End Region

    Private Sub frmVendorItemDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblVersion.Visible = False
        lblVersionDesc.Visible = False
        LoadBlankGrid()
        btndelete.Enabled = True
        btnsave.Enabled = True
        SetUserMgmtNew()
       
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D to Delete")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S to Save")


        If isFromApprovalForm Then
            Me.Text = "Approval Customer Item Details"
        End If

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsCustomeritemDetails()
                obj.Customer_Code = fndCustomer.Value
                obj.Customer_Desc = txtdesc.Text
                obj.comp_code = objCommonVar.CurrentCompanyCode
                Dim qry As String = "select ISNULL(MAX(version), 0) from TSPL_CUSTOMER_ITEM_DETAIL where Customer_Code='" + fndCustomer.Value + "' "
                obj.version = clsDBFuncationality.getSingleValue(qry)
                If (obj.version <= 0 Or clsCommon.myLen(obj.version) <= 0) Then
                    obj.version = 1
                Else
                    obj.version = obj.version + 1
                End If

                Dim Arr As New List(Of clsCustomeritemDetails)
                For Each grow As GridViewRowInfo In dgvitem.Rows
                    Dim objTr As New clsCustomeritemDetails()
                    objTr.item_code = clsCommon.myCstr(grow.Cells(colitemno).Value)
                    objTr.item_desc = clsCommon.myCstr(grow.Cells(coldesc).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(coluom).Value)
                    objTr.item_rate = clsCommon.myCdbl(grow.Cells(colrate).Value)
                    objTr.Item_MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    If clsCommon.myCdbl(grow.Cells(colApprovalRate).Value) = 0 Then
                        objTr.Approval_Item_Rate = clsCommon.myCdbl(grow.Cells(colrate).Value)
                    Else
                        objTr.Approval_Item_Rate = clsCommon.myCdbl(grow.Cells(colApprovalRate).Value)
                    End If
                    objTr.Discount_Per = clsCommon.myCdbl(grow.Cells(colDiscount).Value)
                    objTr.Discount_Per_Level2 = clsCommon.myCdbl(grow.Cells(colDiscount2).Value)
                    objTr.Min_Rate = objTr.Approval_Item_Rate * ((100 - objTr.Discount_Per) / 100)
                    objTr.customer_item_no = clsCommon.myCstr(grow.Cells(colCustitem).Value)
                    If clsCommon.myLen(grow.Cells(colStartDate).Value) <= 0 Then
                        objTr.Start_Date = Nothing
                    Else
                        objTr.Start_Date = clsCommon.myCDate(grow.Cells(colStartDate).Value, "dd-MMM-yyyy")
                    End If
                    If clsCommon.myLen(grow.Cells(colEndDate).Value) <= 0 Then
                        objTr.End_Date = Nothing
                    Else
                        objTr.End_Date = clsCommon.myCDate(grow.Cells(colEndDate).Value, "dd-MMM-yyyy")
                    End If
                    objTr.version = obj.version
                    If (clsCommon.myLen(objTr.item_code) > 0) Then
                        Arr.Add(objTr)
                    End If
                Next

                If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at least one Item")
                    Return
                End If
                If (obj.SaveData(chkApplyOnGroup.Checked, fndCustomer.Value, txtdesc.Text, objCommonVar.CurrentCompanyCode, Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Customer No")
                fndCustomer.Focus()
                Return False
            End If

            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To dgvitem.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colitemno).Value)
                Dim strIName As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(coldesc).Value)
                If clsCommon.myLen(clsCommon.myCstr(dgvitem.Rows(ii).Cells(colStartDate).Value)) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Enter Start date For Item '" + strICode + "'")
                    Return False
                End If
                If clsCommon.myCBool(dgvitem.Rows(ii).Cells(colisMRPMandatory).Value) AndAlso clsCommon.myCdbl(dgvitem.Rows(ii).Cells(colMRP).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter MRP for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                    Return False
                End If
                For jj As Integer = 0 To dgvitem.Rows.Count - 1
                    If (ii = jj) Then
                        Continue For
                    End If
                    If (clsCommon.CompairString(strICode, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colitemno).Value)) = CompairStringResult.Equal) Then
                        common.clsCommon.MyMessageBoxShow("Already selected Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                        Return False
                    End If
                Next
                If Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub LoadData(ByVal customerCode As String)
        Try
            btnsave.Enabled = True
            btndelete.Enabled = True
            isInsideLoadData = True
            txtdesc.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER Where Cust_Code='" + customerCode + "'")
            LoadBlankGrid()
            Dim Arr As List(Of clsCustomeritemDetails) = clsCustomeritemDetails.GetData(customerCode)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As clsCustomeritemDetails In Arr
                    dgvitem.Rows.AddNew()
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colitemno).Value = objTr.item_code
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coldesc).Value = objTr.item_desc
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coluom).Value = objTr.UOM
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colDiscount).Value = objTr.Discount_Per
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colDiscount2).Value = objTr.Discount_Per_Level2
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colMinRate).Value = objTr.Min_Rate
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colrate).Value = objTr.item_rate
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colMRP).Value = objTr.Item_MRP
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objTr.item_code)
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colApprovalRate).Value = objTr.Approval_Item_Rate
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colCustitem).Value = objTr.customer_item_no
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colStartDate).Value = objTr.Start_Date
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colEndDate).Value = objTr.End_Date
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Public Sub funreset()
        fndCustomer.Value = ""
        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        delete()
    End Sub

    Public Sub delete()
        Try
            If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("No customer found to delete.")
                fndCustomer.Focus()
            Else
                If clsCustomeritemDetails.DeleteData(fndCustomer.Value, isFromApprovalForm) Then
                    clsCommon.MyMessageBoxShow("Data deleted successfully.")
                    LoadData(fndCustomer.Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndvendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Description,TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc from TSPL_CUSTOMER_MASTER left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code"
        fndCustomer.Value = clsCommon.ShowSelectForm("CustSel1", qry, "Code", "", fndCustomer.Value, "Code", isButtonClicked)
        txtdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "'"))
        LoadData(fndCustomer.Value)
    End Sub

    Private Sub dgvitem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvitem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is dgvitem.Columns(colitemno) Then
                        OpenICodeList(False)
                    ElseIf e.Column Is dgvitem.Columns(coluom) Then
                        openuom(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Sub openuom(ByVal isButtonClick As Boolean)
        dgvitem.CurrentRow.Cells(coluom).Value = clsItemMaster.FinderForuom(clsCommon.myCstr(dgvitem.CurrentRow.Cells(coluom).Value), clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value), isButtonClick)
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value), "F", isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            dgvitem.CurrentRow.Cells(colitemno).Value = obj.Item_Code
            dgvitem.CurrentRow.Cells(coldesc).Value = obj.Item_Desc
            dgvitem.CurrentRow.Cells(coluom).Value = obj.Unit_Code
            dgvitem.CurrentRow.Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code)

        Else
            dgvitem.CurrentRow.Cells(colitemno).Value = ""
            dgvitem.CurrentRow.Cells(coldesc).Value = ""
            dgvitem.CurrentRow.Cells(coldesc).Value = ""
        End If

    End Sub

    Sub LoadBlankGrid()

        dgvitem.AddNewRowPosition = SystemRowPosition.Bottom
        dgvitem.Rows.Clear()
        dgvitem.Columns.Clear()

        Dim item_code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.HeaderText = "Item No"
        item_code.Name = colitemno
        item_code.Width = 90
        item_code.ReadOnly = False
        item_code.TextImageRelation = TextImageRelation.TextBeforeImage
        item_code.HeaderImage = Global.ERP.My.Resources.Resources.search4
        item_code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_code)

        Dim item_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_desc.FormatString = ""
        item_desc.HeaderText = "Description"
        item_desc.Name = coldesc
        item_desc.Width = 200
        item_desc.ReadOnly = True
        item_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_desc)

        Dim uom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        uom.FormatString = ""
        uom.HeaderText = "UOM"
        uom.Name = coluom
        uom.Width = 70
        uom.ReadOnly = False
        uom.TextImageRelation = TextImageRelation.TextBeforeImage
        uom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        uom.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(uom)

        Dim itemrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemrate.FormatString = ""
        itemrate.HeaderText = "Item Rate"
        itemrate.Name = colrate
        itemrate.Width = 70
        itemrate.ReadOnly = False
        itemrate.ShowUpDownButtons = False
        itemrate.Step = 0
        dgvitem.MasterTemplate.Columns.Add(itemrate)


        Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        repoIsMRPMandatory.Name = colisMRPMandatory
        repoIsMRPMandatory.IsVisible = False
        repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsMRPMandatory.ReadOnly = True
        dgvitem.MasterTemplate.Columns.Add(repoIsMRPMandatory)

        Dim itemMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemMRP.FormatString = ""
        itemMRP.HeaderText = "Item MRP"
        itemMRP.Name = colMRP
        itemMRP.Width = 70
        itemMRP.ReadOnly = False
        itemMRP.ShowUpDownButtons = False
        itemMRP.Step = 0
        dgvitem.MasterTemplate.Columns.Add(itemMRP)


        Dim itemApprovalRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        itemApprovalRate.FormatString = ""
        itemApprovalRate.HeaderText = "Approval Rate"
        itemApprovalRate.Name = colApprovalRate
        itemApprovalRate.Width = 100
        itemApprovalRate.ReadOnly = False
        itemApprovalRate.ShowUpDownButtons = False
        itemApprovalRate.Step = 0
        itemApprovalRate.IsVisible = isFromApprovalForm
        dgvitem.MasterTemplate.Columns.Add(itemApprovalRate)

        Dim repoDiscountPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscountPer.FormatString = ""
        repoDiscountPer.HeaderText = "Discount %"
        repoDiscountPer.Name = colDiscount
        repoDiscountPer.Width = 70
        repoDiscountPer.ReadOnly = False
        repoDiscountPer.IsVisible = Not isFromApprovalForm
        repoDiscountPer.Minimum = 0
        repoDiscountPer.ShowUpDownButtons = False
        repoDiscountPer.Step = 0
        dgvitem.MasterTemplate.Columns.Add(repoDiscountPer)

        Dim repoDiscountPer2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscountPer2.FormatString = ""
        repoDiscountPer2.HeaderText = "Discount %"
        repoDiscountPer2.Name = colDiscount2
        repoDiscountPer2.Width = 70
        repoDiscountPer2.ReadOnly = False
        repoDiscountPer2.Minimum = 0
        repoDiscountPer2.IsVisible = isFromApprovalForm
        repoDiscountPer2.ShowUpDownButtons = False
        repoDiscountPer2.Step = 0
        dgvitem.MasterTemplate.Columns.Add(repoDiscountPer2)

        Dim repoMinRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMinRate.FormatString = ""
        repoMinRate.HeaderText = "Min Rate"
        repoMinRate.Name = colMinRate
        repoMinRate.Width = 70
        repoMinRate.ReadOnly = True
        repoMinRate.Minimum = 0
        repoMinRate.ShowUpDownButtons = False
        repoMinRate.Step = 0
        repoMinRate.IsVisible = False
        dgvitem.MasterTemplate.Columns.Add(repoMinRate)

        Dim venitem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        venitem.FormatString = ""
        venitem.HeaderText = "Customer Item No"
        venitem.Name = colCustitem
        venitem.Width = 150
        venitem.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(venitem)

        Dim StartDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        StartDate.Format = DateTimePickerFormat.Custom
        StartDate.CustomFormat = "dd-MM-yyyy"
        StartDate.HeaderText = "Start Date"
        StartDate.FormatString = "{0:d}"
        StartDate.Name = colStartDate
        StartDate.WrapText = True
        StartDate.ReadOnly = False
        StartDate.Width = 80
        dgvitem.MasterTemplate.Columns.Add(StartDate)

        Dim EndDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        EndDate.Format = DateTimePickerFormat.Custom
        EndDate.CustomFormat = "dd-MM-yyyy"
        EndDate.HeaderText = "End Date"
        EndDate.FormatString = "{0:d}"
        EndDate.Name = colEndDate
        EndDate.WrapText = True
        EndDate.ReadOnly = False
        EndDate.Width = 80
        dgvitem.MasterTemplate.Columns.Add(EndDate)


        dgvitem.AllowDeleteRow = True
        dgvitem.AllowAddNewRow = Not isFromApprovalForm
        dgvitem.ShowGroupPanel = False
        dgvitem.AllowColumnReorder = False
        dgvitem.AllowRowReorder = False
        dgvitem.EnableSorting = False
        dgvitem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvitem.MasterTemplate.ShowRowHeaderColumn = False


    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VendorItemDetails)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            Import.Enabled = True
            Export.Enabled = True
        Else
            Import.Enabled = False
            Export.Enabled = False
        End If
        '--------------------------------------------------
        ' btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    Private Function funSetUserAccess() As Boolean
        Try

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = clsUserMgtCode.VendorItemDetails
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access
                btnsave.Enabled = False
            End If
            If strTemp(2) = "0" Then 'Grant modify access
                btndelete.Enabled = False
            End If

            funSetUserAccess = True
        Catch er As Exception
            myMessages.myExceptions(er)
        End Try
    End Function

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Sub BlankAllControls()
        fndCustomer.Value = ""
        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub

    Private Sub Export_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        Dim str As String
        str = "select Customer_Code as [Customer Code],Customer_Desc as [Customer Description] ,item_no as [Item No],item_desc as [Item Description],uom as [UOM],item_rate as [Rate],Customer_item_no as [Customer Item No], REPLACE( Convert(varchar(11) ,Start_Date,102),'.','-') as [Start Date], " & _
        "REPLACE( Convert(varchar(11) ,End_Date,102),'.','-') as [End Date],Version,Discount_Per as [Disc Percentage], " & _
        "Approval_Item_Rate as [Approval Item Rate],Min_Rate as [Min Rate],Discount_Per_Level2  as [Disc Percentage Level2] " & _
        "from TSPL_CUSTOMER_ITEM_DETAIL  "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub Import_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Customer Code", "Customer Description", "Item No", "Item Description", "UOM", "Rate", "Customer Item No", "Start Date", "End Date", "Disc Percentage", "Approval Item Rate", "Min Rate", "Disc Percentage Level2") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim Custcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If Custcode.Length > 50 Then
                        Throw New Exception("Check the length of 'Cust Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim CountCustCode As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + Custcode + "'", trans)

                    If CountCustCode = "" OrElse Custcode Is Nothing Then
                        Throw New Exception("This  '" + Custcode + "'  Item does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If


                    Dim CustDesc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim itemno As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If itemno.Length > 50 Then
                        Throw New Exception("Check the length of 'Item Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim countItemCode As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_MASTER where item_code='" + itemno + "'", trans)
                    If countItemCode = "" OrElse countItemCode Is Nothing Then
                        Throw New Exception("This  '" + itemno + "'  Item does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim qryDesc As String = "select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + itemno + "' "
                    Dim itemdesc As String = clsDBFuncationality.getSingleValue(qryDesc, trans)
                    'Dim itemdesc As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If itemdesc.Length > 100 Then
                        Throw New Exception("Check the length of 'Item Description' In Customer Master.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim uom As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim rate As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If rate.Length < 18 And IsNumeric(rate) Then
                    Else
                        Throw New Exception("Check the value of 'Item Rate'.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim Custitemno As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If Custitemno.Length > 50 Then
                        Throw New Exception("Check the length of 'Customer Item No'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim StrstartDate As String = Nothing
                    If (grow.Cells(7).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(7).Value) > 0 And clsCommon.myLen(grow.Cells(7).Value) < 11) Then
                        StrstartDate = clsCommon.GetPrintDate((grow.Cells(7).Value), "dd-MM-yyyy")
                        ''Else
                        ''    Throw New Exception("Please insert Date in Format- i.e. (yyyy-MM-dd)")
                    End If

                    Dim StrEndDate As String = Nothing
                    If (grow.Cells(8).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(8).Value) > 0 And clsCommon.myLen(grow.Cells(8).Value) < 11) Then
                        StrEndDate = clsCommon.GetPrintDate((grow.Cells(8).Value), "dd-MM-yyyy")
                    End If

                    Dim DiscPer As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If Not DiscPer.Length < 18 And IsNumeric(DiscPer) Then
                        Throw New Exception("Check the value of 'Discount Percentage'.")
                        trans.Rollback()
                        Exit Sub
                    End If


                    Dim AppItemRate As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If Not AppItemRate.Length < 18 And IsNumeric(AppItemRate) Then
                        Throw New Exception("Check the value of 'Approval Item Rate'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim Min_Rate As String = clsCommon.myCstr(grow.Cells(11).Value)
                    If Not Min_Rate.Length < 18 And IsNumeric(Min_Rate) Then
                        Throw New Exception("Check the value of 'Minimum Rate'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim DiscPerLevel2 As String = clsCommon.myCstr(grow.Cells(12).Value)
                    If Not DiscPerLevel2.Length < 18 And IsNumeric(DiscPerLevel2) Then
                        Throw New Exception("Check the value of 'Discount Percentage Level 2'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_CUSTOMER_ITEM_DETAIL where Customer_Code='" + Custcode + "' and item_no='" + itemno + "' "
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))

                    If (i = 0) Then
                        Dim qry As String = "insert into TSPL_CUSTOMER_ITEM_DETAIL( Customer_Code ,Customer_Desc  ,item_no ,item_desc ,uom  ,item_rate ,Customer_item_no ,comp_code, Start_Date, End_Date,Discount_Per,Approval_Item_Rate,Min_Rate,Discount_Per_Level2 ) values('" + Convert.ToString(Custcode) + "','" + Convert.ToString(CustDesc) + "','" + Convert.ToString(itemno) + "','" + Convert.ToString(itemdesc) + "','" + Convert.ToString(uom) + "','" + Convert.ToString(rate) + "','" + Convert.ToString(Custitemno) + "','" + Convert.ToString(objCommonVar.CurrentCompanyCode) + "'," + IIf(clsCommon.myLen(StrstartDate) > 0, "Convert(Date, '" + StrstartDate + "', 103)", "Null") + " ," + IIf(clsCommon.myLen(StrEndDate) > 0, "Convert(Date, '" + StrEndDate + "', 103)", "Null") + ",'" + Convert.ToString(DiscPer) + "','" + Convert.ToString(AppItemRate) + "','" + Convert.ToString(Min_Rate) + "','" + Convert.ToString(DiscPerLevel2) + "' )"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Else
                        Dim qry As String = "update TSPL_CUSTOMER_ITEM_DETAIL set Customer_Desc= '" + Convert.ToString(CustDesc) + "'  ,item_desc= '" + Convert.ToString(itemdesc) + "',uom= '" + Convert.ToString(uom) + "',item_rate='" + Convert.ToString(rate) + "' ,Customer_item_no='" + Convert.ToString(Custitemno) + "' ,comp_code='" + Convert.ToString(objCommonVar.CurrentCompanyCode) + "', Start_Date=" + IIf(clsCommon.myLen(StrstartDate) > 0, "Convert(Date, '" + StrstartDate + "', 103)", "Null") + ", End_Date=" + IIf(clsCommon.myLen(StrEndDate) > 0, "Convert(Date, '" + StrEndDate + "', 103)", "Null") + ",Discount_Per='" + Convert.ToString(DiscPer) + "' ,Approval_Item_Rate='" + Convert.ToString(AppItemRate) + "' ,Min_Rate='" + Convert.ToString(Min_Rate) + "' ,Discount_Per_Level2='" + Convert.ToString(DiscPerLevel2) + "'  where Customer_Code= '" + Convert.ToString(Custcode) + "' and item_no='" + Convert.ToString(itemno) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    ''Private Sub frmVendorItemDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    ''    If e.KeyCode = Keys.F2 AndAlso dgvitem.CurrentCell IsNot Nothing Then
    ''        isCellValueChangedOpen = True
    ''        If dgvitem.CurrentColumn Is dgvitem.Columns(colitemno) Then
    ''            'dgvitem.CurrentColumn = dgvitem.Columns(colTo)
    ''            OpenFromList(True)
    ''            dgvitem.CurrentColumn = dgvitem.Columns(coldesc)

    ''        End If
    ''    End If
    ''End Sub

    ''Sub OpenFromList(ByVal isButtonClick As Boolean)
    ''    Dim qry As String = "SELECT item_no  as Code,Description from tspl_vendor "
    ''    dgvitem.CurrentRow.Cells(colitemno).Value = clsCommon.ShowSelectForm("Items", qry, "Code", "item_no='" + clsCommon.myCstr(dgvitem.CurrentRow.Cells("colitemno").Value) + "'", clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value), "Code", isButtonClick)
    ''End Sub



    Private Sub frmVendorItemDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled AndAlso MyBase.isDeleteFlag Then
            delete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclear.Enabled Then
            Me.Close()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            Print()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub Print()

        If clsCommon.myLen(fndCustomer.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Customer")
            fndCustomer.Focus()
            Return
        End If
        Try
            Dim Qry As String = "Select '" + clsCommon.GETSERVERDATE() + "' as PrintDate,  (Customer_Code+' - '+ convert(varchar, Customer_desc, 103)) as Customer, item_no, item_desc, uom, item_rate, Customer_item_no, Convert(date,Start_Date, 103) as Start_Date , COnvert(date,End_Date, 103) as End_Date, Comp_Name, Logo_Img, Logo_Img2,CONVERT(date, History_Date, 103) as History_Date    from TSPL_CUSTOMER_ITEM_DETAIL_HIST Left outer join TSPL_COMPANY_MASTER on TSPL_CUSTOMER_ITEM_DETAIL_HIST.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  Where Customer_Code='" + fndCustomer.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
                Exit Sub
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.Purchase, dt, "crptCustomerItemHistory", "Customer Item History Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dgvitem_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvitem.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub


    Private Sub dgvitem_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles dgvitem.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is dgvitem.Columns(colApprovalRate)) Then
                    dgvitem.CurrentRow.Cells(colApprovalRate).ReadOnly = Not isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(colitemno)) Then
                    dgvitem.CurrentRow.Cells(colitemno).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(coluom)) Then
                    dgvitem.CurrentRow.Cells(coluom).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(colrate)) Then
                    dgvitem.CurrentRow.Cells(colrate).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(colMRP)) Then
                    dgvitem.CurrentRow.Cells(colMRP).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(colDiscount)) Then
                    dgvitem.CurrentRow.Cells(colDiscount).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(colCustitem)) Then
                    dgvitem.CurrentRow.Cells(colCustitem).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(colStartDate)) Then
                    dgvitem.CurrentRow.Cells(colStartDate).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(colEndDate)) Then
                    dgvitem.CurrentRow.Cells(colEndDate).ReadOnly = isFromApprovalForm
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub
End Class


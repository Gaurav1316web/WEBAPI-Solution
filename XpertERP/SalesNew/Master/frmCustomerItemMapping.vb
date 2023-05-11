Imports common
Imports System.Data.SqlClient

Public Class frmCustomerItemMapping
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
    Const colItemPartNo As String = "ItemPartNo"
    Const colCustomerPartNo As String = "CustomerPartNo"
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
                Dim obj As New clsCustomeritemMapping()
                obj.Customer_Code = fndCustomer.Value
                obj.Customer_Desc = txtdesc.Text
                obj.comp_code = objCommonVar.CurrentCompanyCode
   
                Dim Arr As New List(Of clsCustomeritemMapping)
                For Each grow As GridViewRowInfo In dgvitem.Rows
                    Dim objTr As New clsCustomeritemMapping()
                    objTr.item_code = clsCommon.myCstr(grow.Cells(colitemno).Value)
                    objTr.item_desc = clsCommon.myCstr(grow.Cells(coldesc).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(coluom).Value)
                    objTr.customer_item_no = clsCommon.myCstr(grow.Cells(colCustitem).Value)
                    objTr.Item_Part_No = clsCommon.myCstr(grow.Cells(colItemPartNo).Value)
                    objTr.Customer_Part_No = clsCommon.myCstr(grow.Cells(colCustomerPartNo).Value)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            Dim Arr As List(Of clsCustomeritemMapping) = clsCustomeritemMapping.GetData(customerCode)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As clsCustomeritemMapping In Arr
                    dgvitem.Rows.AddNew()
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colitemno).Value = objTr.item_code
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coldesc).Value = objTr.item_desc
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coluom).Value = objTr.UOM
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colCustitem).Value = objTr.customer_item_no
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colItemPartNo).Value = objTr.Item_Part_No
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colCustomerPartNo).Value = objTr.Customer_Part_No
                Next

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If clsCustomeritemMapping.DeleteData(fndCustomer.Value, isFromApprovalForm) Then
                    clsCommon.MyMessageBoxShow("Data deleted successfully.")
                    LoadData(fndCustomer.Value)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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


        Else
            dgvitem.CurrentRow.Cells(colitemno).Value = ""
            dgvitem.CurrentRow.Cells(coldesc).Value = ""
            dgvitem.CurrentRow.Cells(coluom).Value = ""
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

        'Dim itemrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        'itemrate.FormatString = ""
        'itemrate.HeaderText = "Item Rate"
        'itemrate.Name = colrate
        'itemrate.Width = 70
        'itemrate.ReadOnly = False
        'itemrate.ShowUpDownButtons = False
        'itemrate.Step = 0
        'dgvitem.MasterTemplate.Columns.Add(itemrate)


        'Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        'repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        'repoIsMRPMandatory.Name = colisMRPMandatory
        'repoIsMRPMandatory.IsVisible = False
        'repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        'repoIsMRPMandatory.ReadOnly = True
        'dgvitem.MasterTemplate.Columns.Add(repoIsMRPMandatory)

        'Dim itemMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        'itemMRP.FormatString = ""
        'itemMRP.HeaderText = "Item MRP"
        'itemMRP.Name = colMRP
        'itemMRP.Width = 70
        'itemMRP.ReadOnly = False
        'itemMRP.ShowUpDownButtons = False
        'itemMRP.Step = 0
        'dgvitem.MasterTemplate.Columns.Add(itemMRP)



        Dim venitem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        venitem.FormatString = ""
        venitem.HeaderText = "Customer Item No"
        venitem.Name = colCustitem
        venitem.Width = 150
        venitem.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(venitem)

        Dim venCustomerPartNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        venCustomerPartNo.FormatString = ""
        venCustomerPartNo.HeaderText = "Customer Part No"
        venCustomerPartNo.Name = colCustomerPartNo
        venCustomerPartNo.Width = 150
        venCustomerPartNo.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(venCustomerPartNo)

        Dim venitemPartNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        venitemPartNo.FormatString = ""
        venitemPartNo.HeaderText = "Item Part No"
        venitemPartNo.Name = colItemPartNo
        venitemPartNo.Width = 150
        venitemPartNo.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(venitemPartNo)

      

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
        str = "select Customer_Code as [Customer Code],Customer_Desc as [Customer Description] ,item_no as [Item No],item_desc as [Item Description],uom as [UOM],Customer_item_no as [Customer Item No],Item_Part_No as [Item Part No] ,customer_part_no as [Customer Part No] from TSPL_CUSTOMER_ITEM_MAPPING  "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub Import_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim i As Integer = 0
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Customer Code", "Customer Description", "Item No", "Item Description", "UOM", "Customer Item No", "Item Part No", "Customer Part No") Then
            Dim trans As SqlTransaction = Nothing
            Try

                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsCustomeritemMapping)
                Dim obj As New clsCustomeritemMapping()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsCustomeritemMapping
                    Arr = New List(Of clsCustomeritemMapping)
                    i = i + 1
                    Dim strName As String = ""
                    Dim strCode As String = ""

                    strCode = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                    If strCode.Length > 12 Then
                        Throw New Exception("length of Customer can not be greater than 12")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER  where Cust_Code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception("Customer (" & strCode & ") does not exist in Customer Master . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.Customer_Code = strCode

                    strName = clsCommon.myCstr(grow.Cells("Customer Description").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Customer Description can not be greater than 50 ")

                    End If
                    obj.Customer_Desc = strName



                    strCode = clsCommon.myCstr(grow.Cells("Item No").Value)
                    If strCode.Length > 50 Then
                        Throw New Exception("length of Item No not be greater than 50")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER  where Item_Code ='" & strCode & "'", trans)
                            If strName <= 0 Then
                                Throw New Exception(" Item No (" & strCode & ") does not exist in Item Master . Please make it entry first.")
                            Else
                                strName = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_ITEM_MAPPING where Customer_Code ='" & obj.Customer_Code & "' and item_no ='" & strCode & "'", trans)
                                If strName > 0 Then
                                    Throw New Exception("Already selected Item (" & strCode & ") for this customer (" & obj.Customer_Code & ") ")
                                End If
                            End If
                        End If
                    End If


                    obj.item_code = strCode


                    strName = clsCommon.myCstr(grow.Cells("Item Description").Value)
                    If strName.Length > 100 Then
                        Throw New Exception("Length of Item Description can not be greater than 100 ")

                    End If
                    obj.item_desc = strName

                    strName = clsCommon.myCstr(grow.Cells("UOM").Value)
                    If strName.Length > 12 Then
                        Throw New Exception("Length of UOM can not be greater than 12 ")

                    End If
                    obj.UOM = strName



                    strName = clsCommon.myCstr(grow.Cells("Customer Item No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Customer Item No can not be greater than 50 ")

                    End If
                    obj.customer_item_no = strName

                    strName = clsCommon.myCstr(grow.Cells("Item Part No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Item Part No can not be greater than 50 ")

                    End If
                    obj.Item_Part_No = strName

                    strName = clsCommon.myCstr(grow.Cells("Customer Part No").Value)
                    If strName.Length > 50 Then
                        Throw New Exception("Length of Customer Part No can not be greater than 50 ")

                    End If
                    obj.Customer_Part_No = strName

                    If (clsCommon.myLen(obj.item_code) > 0) Then
                        Arr.Add(obj)
                    End If

                    '' end kdil and viney

                    obj.SaveData(chkApplyOnGroup.Checked, obj.Customer_Code, obj.Customer_Desc, objCommonVar.CurrentCompanyCode, Arr)

                Next
                'obj.SaveData(chkApplyOnGroup.Checked, obj.Customer_Code, txtdesc.Text, objCommonVar.CurrentCompanyCode, Arr)
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
               clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message & " At Line No : " & i)
           
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
   


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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If (e.Column Is dgvitem.Columns(colitemno)) Then
                    dgvitem.CurrentRow.Cells(colitemno).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(coluom)) Then
                    dgvitem.CurrentRow.Cells(coluom).ReadOnly = isFromApprovalForm
                ElseIf (e.Column Is dgvitem.Columns(colCustitem)) Then
                    dgvitem.CurrentRow.Cells(colCustitem).ReadOnly = isFromApprovalForm

                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class


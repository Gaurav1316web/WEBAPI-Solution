Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports Telerik

Public Class frmBulkSaleFreightMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colSNo As String = "colSNo"
    Const colTenderQty As String = "colTenderQty"
    Const colRate As String = "colRate"
    Const colProRate As String = "colProRate"
    Const colDieselPetrol As String = "colDieselPetrol"
    Const colApplicableRate As String = "colApplicableRate"
    Const colGPSKM As String = "colGPSKM"
    Const colPayableAmount As String = "colPayableAmount"
    Dim isLoadData As Boolean = False
    Dim isCopyData As Boolean = False
#End Region

    Private Sub frmBulkSaleFreightMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)

        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Document_Date", "DateTime not NULL")
        coll.Add("Customer_Code", "varchar(12) NOT NULL")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Start_Date", "Date Not null")

        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "datetime NOT NULL")
        coll.Add("Modify_By", "varchar(12) NOT NULL")
        coll.Add("Modify_Date", "datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posting_Date", "datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_BK_FREIGHT_MASTER", coll, Nothing, True, False, Nothing, Nothing, Nothing, True)

        coll = New Dictionary(Of String, String)()
        coll.Add("SNo", "integer null")
        coll.Add("Document_Code", "Varchar(30) not null REFERENCES TSPL_BK_FREIGHT_MASTER(Document_Code)")
        coll.Add("Tender_Qty", "decimal (18,2) NULL")
        coll.Add("Rate", "decimal(18, 2) NULL")
        coll.Add("Pro_Rate", "decimal(18, 2) NULL")
        coll.Add("DieselPetrol", "decimal (18,2) NULL")
        coll.Add("Applicable_Rate", "decimal (18,2) NULL")
        coll.Add("GPS_KM", "decimal(18.2) NULL")
        coll.Add("Payable_Amount", "decimal (18,2) NULL")

        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_BK_FREIGHT_DETAIL", coll, Nothing, True, False, "TSPL_BK_FREIGHT_MASTER", "Document_Code", "Document_Date", True)

        SetUserMgmtNew()

        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        Dim qry As String = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        txtCustomer.Value = clsCommon.ShowSelectForm("BlkFreightMstr", qry, "Code", "", txtCustomer.Value, "Code", isButtonClicked)
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HEAD_LOAD where Document_No='" + txtDocumentNo.Value + "' "
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

    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        If clsCommon.myLen(txtDocumentNo) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
        End If
        txtDocumentNo.Value = clsBulkSaleFreightMaster.getFinder(txtDocumentNo.Value, isButtonClicked)
        LoadData(txtDocumentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub frmBulkSaleFreightMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnsave.Enabled AndAlso MyBase.isDeleteFlag Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseUnpost.Visible = True
                End If
            Else
                MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        End If
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CancelPressed()
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click

        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocumentNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsBulkSaleFreightMaster.PostData(MyBase.Form_ID, txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean

        'If clsCommon.myLen(txtDescription.Text) <= 0 Then
        '    txtDescription.Focus()
        '    Throw New Exception("Description can't be blank.")
        'End If
        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsBulkSaleFreightMaster()
                obj.Document_Code = txtDocumentNo.Value
                obj.Document_date = clsCommon.myCDate(txtdate.Value)
                obj.Start_Date = clsCommon.myCDate(txtStartDate.Value)
                obj.Arr = New List(Of clsBulkSaleFreightDetail)

                For Each grow As GridViewRowInfo In gv1.Rows

                    Dim objTr As New clsBulkSaleFreightDetail()
                    objTr.Tender_Qty = clsCommon.myCstr((grow.Cells("Tender_Qty").Value))
                    objTr.Rate = clsCommon.myCDecimal(grow.Cells("Rate").Value)

                    obj.Arr.Add(objTr)


                Next

                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Addnew()
        isNewEntry = True
        txtDocumentNo.Value = ""
        btnsave.Enabled = True
        btnPost.Enabled = True
        txtdate.Value = clsCommon.GETSERVERDATE()
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
        LoadBlankGrid()
        isInsideLoadData = False
        btndelete.Enabled = True
        lblStatus.Status = ERPTransactionStatus.Pending
        ReStoreGridLayout()
    End Sub
    Private Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()


        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.Name = colSNo
        repoNumBox.Width = 50
        repoNumBox.DecimalPlaces = 0
        repoNumBox.Minimum = 0
        repoNumBox.Step = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.ReadOnly = True
        repoNumBox.HeaderText = "SNO"
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoTenderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTenderQty.FormatString = ""
        repoTenderQty.HeaderText = "Tender Qty"
        repoTenderQty.Name = colTenderQty
        repoTenderQty.IsVisible = False
        repoTenderQty.Minimum = 0
        repoTenderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTenderQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTenderQty)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate Per 9KL"
        repoRate.Name = colRate
        repoRate.IsVisible = False
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoProRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProRate.FormatString = ""
        repoProRate.HeaderText = "Pro-Rate Payable Rate"
        repoProRate.Name = colProRate
        repoProRate.IsVisible = False
        repoProRate.Minimum = 0
        repoProRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoProRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoProRate)

        Dim repoDieselPetrol As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDieselPetrol = New GridViewDecimalColumn()
        repoDieselPetrol.FormatString = ""
        repoDieselPetrol.HeaderText = "Diesel Hike/Red."
        repoDieselPetrol.Name = colDieselPetrol
        repoDieselPetrol.IsVisible = False
        repoDieselPetrol.Minimum = 0
        repoDieselPetrol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDieselPetrol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDieselPetrol)

        Dim repoApplicableRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoApplicableRate.FormatString = ""
        repoApplicableRate.HeaderText = "Applicable Rate"
        repoApplicableRate.Name = colApplicableRate
        repoApplicableRate.IsVisible = False
        repoApplicableRate.Minimum = 0
        repoApplicableRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoApplicableRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoApplicableRate)

        Dim repoGPSKM As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGPSKM.FormatString = ""
        repoGPSKM.HeaderText = "GPS KM"
        repoGPSKM.Name = colGPSKM
        repoGPSKM.IsVisible = False
        repoGPSKM.Minimum = 0
        repoGPSKM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoGPSKM.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGPSKM)

        Dim repoPayableAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPayableAmount.FormatString = ""
        repoPayableAmount.HeaderText = "Payable Amount"
        repoPayableAmount.Name = colPayableAmount
        repoPayableAmount.IsVisible = False
        repoPayableAmount.Minimum = 0
        repoPayableAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPayableAmount.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPayableAmount)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        gv1.Rows.AddNew()
        ReStoreGridLayout()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnsave.Enabled = True
            btnPost.Enabled = True
            LoadBlankGrid()
            Dim obj As New clsBulkSaleFreightMaster()
            obj = clsBulkSaleFreightMaster.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_Code)) > 0) Then
                If isCopyData = False Then
                    isNewEntry = False
                    btnsave.Text = "Update"
                    If obj.Status = 1 Then
                        lblStatus.Status = ERPTransactionStatus.Approved
                        btndelete.Enabled = False
                        btnsave.Enabled = False
                        btnPost.Enabled = False
                    Else
                        lblStatus.Status = ERPTransactionStatus.Pending
                        btndelete.Enabled = True
                    End If
                Else
                    isNewEntry = True
                    btnsave.Text = "Save"
                    lblStatus.Status = ERPTransactionStatus.Pending
                    btndelete.Enabled = True
                End If


                txtDocumentNo.Value = obj.Document_Code
                txtdate.Value = obj.Document_date
                txtStartDate.Value = obj.Start_Date

            End If

            isLoadData = True
            isInsideLoadData = True
            isInsideLoadData = False

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try

            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("Document No not found to delete")
            End If
            If (myMessages.deleteConfirm()) Then
                clsPricePlanHead.DeleteData(txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
End Class
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class frmBulkSaleAcknowledgementUploader

#Region "Variables"

    Dim paramcount As Integer = 0
    Dim intStartParam As Integer = 0
    Dim isdipmarkingmendatory As Boolean = False
    Public Const colIsValidated As String = "colIsValidated"
    Public Const colErrorStatus As String = "colErrorStatus"
    Public ValidatedCount As Integer = 0
    Dim isNewEntry As Boolean = False
    Public TextCol As GridViewTextBoxColumn = Nothing
    Public TextColl As GridViewTextBoxColumn = Nothing
    Public Const colSlNo As String = "SlNo"
    Public Const colDispatchDate As String = "colDispatchDate"
    Public Const colDispatchNo As String = "colDispatchNo"
    Public Const colCustomer As String = "colCustomer"
    Public Const colDispatchQty As String = "colDispatchQty"
    Public Const colDispatchFatKG As String = "colDispatchFatKG"
    Public Const colDispatchSNFKG As String = "colDispatchSNFKG"
    Public Const colDispatchFatRate As String = "colDispatchFatRate"
    Public Const colDispatchSNFRate As String = "colDispatchSNFRate"
    Public Const colDispatchAmount As String = "colDispatchAmount"
    Public Const colDispatchFatPer As String = "colDispatchFatPer"
    Public Const colDispatchSNFPer As String = "colDispatchSNFPer"

    Public Const colACKQty As String = "colACKQty"
    Public Const colACKFatKG As String = "colACKFatKG"
    Public Const colACKSNFKG As String = "colACKSNFKG"
    Public Const colACKFatRate As String = "colACKFatRate"
    Public Const colACKSNFRate As String = "colACKSNFRate"
    Public Const colACKAmount As String = "colACKAmount"
    Public Const colACKDiffAmount As String = "colACKDiffAmount"
    Public Const colACKFatPer As String = "colACKFatPer"
    Public Const colACKSNFPer As String = "colACKSNFPer"

    Const ReportID As String = "BulkSaleAckUploader"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Public ChkBoxColumn As GridViewCheckBoxColumn = Nothing
    Dim arrBulkProParameter As New Dictionary(Of String, clsfrmParameterMaster)
#End Region
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmbookingdairy)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag


        'If MyBase.isReverse Then
        '    btnreverse.Enabled = True
        'Else
        '    btnreverse.Enabled = False
        'End If
    End Sub
    Private Sub frmBulkSaleAcknowledgementUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        btnSave.Enabled = True
        btnValidate.Enabled = True
        SetUserMgmtNew()
    End Sub
    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        Dim qry As String = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        txtCustomer.Value = clsCommon.ShowSelectForm("BulkAckUploaders", qry, "Code", "", txtCustomer.Value, "Code", isButtonClicked)

    End Sub

    Private Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        Reset()
    End Sub
    Sub Reset()
        btnSave.Enabled = True
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.Value = ""
        isNewEntry = True
        gv1.DataSource = Nothing
        gv1.Rows.AddNew()
        btnSave.Text = "Save"
        LoadBlankGrid()
        Gv1.Rows.AddNew()
        btnValidate.Enabled = True
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


    Private Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv1.RowCount - 1
            If Gv1.Rows(ii).Cells(colIsValidated).Value = False Then

                clsCommon.MyMessageBoxShow(Me, "No Validated Rows found to save", Me.Text)

                Return False
                Exit For
            End If
        Next

        Return True
    End Function


    Private Sub FrmBulkSaleAcknowledegmentUploader_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.Escape Then
            CancelPressed()

        End If
    End Sub

    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs)
        RefeshSNO()
    End Sub


    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSlNo).Value = ii
        Next
    End Sub

    Sub CheckAndValidate()
        Try
            Dim ValidateStatus As String = String.Empty
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "There are no row is grid please select a sheet to import", Me.Text)
            End If
            If ValidatedCount = Gv1.Rows.Count Then
                clsCommon.MyMessageBoxShow(Me, "All Rows are already validated", Me.Text)
                Exit Sub
            End If
            ValidatedCount = 0
            Dim strCellValue

            For i As Integer = 0 To gv1.Rows.Count - 1
                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & gv1.Rows.Count)
                ValidateStatus = ""

                strCellValue = clsCommon.myCDate(gv1.Rows(i).Cells("Dispatch Date").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch Date  Must not be Blank" & Environment.NewLine
                End If

                If IsDate(strCellValue) Then
                Else
                    ValidateStatus = ValidateStatus & "Dispatch Date Must  be a Date Time Value" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(Gv1.Rows(i).Cells("Tanker No").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Tanker No  Must not be Blank" & Environment.NewLine
                End If
                strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells("Dispatch No").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch No  Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BULK_SALE_ACKNOWLEDGEMENT where Bulk_Dispatch_Document  = '" & strCellValue & "'")) > 0 Then
                    ValidateStatus = ValidateStatus & "Dispatch No (" & strCellValue & ") is already used " & Environment.NewLine
                End If
                strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells("Customer").Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code = '" & strCellValue & "'")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer not found in master" & Environment.NewLine
                End If

                Dim DispatchAmt As Double = clsCommon.myCdbl(Gv1.Rows(i).Cells("Dispatch Amount").Value)
                Dim ACKAmt As Double = clsCommon.myCdbl(Gv1.Rows(i).Cells("ACK Amount").Value)
                Dim ACKQty As Double = clsCommon.myCdbl(Gv1.Rows(i).Cells("ACK Qty").Value)

                Dim jj As Integer = 0
                jj = intStartParam
                Dim intCOunt As Integer = intStartParam

                Dim ACKFATPer As Double = clsCommon.myCdbl(gv1.Rows(i).Cells("ACK FatPer").Value)
                Dim ACKSNFPer As Double = clsCommon.myCdbl(gv1.Rows(i).Cells("ACK SNFPer").Value)
                If ACKFATPer > 0 Then
                    gv1.Rows(i).Cells("ACK FAT Rate").Value = clsCommon.myCdbl(gv1.Rows(i).Cells("ACK FAT Rate").Value)
                    gv1.Rows(i).Cells("ACK FATKG").Value = (ACKQty * ACKFATPer) / 100
                    gv1.Rows(i).Cells("ACK FATPer").Value = clsCommon.myCdbl(gv1.Rows(i).Cells("ACK FATPer").Value)
                End If
                If ACKSNFPer > 0 Then
                    gv1.Rows(i).Cells("ACK SNF Rate").Value = clsCommon.myCdbl(gv1.Rows(i).Cells("ACK SNF Rate").Value)
                    gv1.Rows(i).Cells("ACK SNFKG").Value = (ACKQty * ACKSNFPer) / 100
                    gv1.Rows(i).Cells("ACK SNFPer").Value = clsCommon.myCdbl(gv1.Rows(i).Cells("ACK SNFPer").Value)
                End If
                gv1.Rows(i).Cells("ACK Amount").Value = Math.Round((gv1.Rows(i).Cells("ACK FAT Rate").Value * gv1.Rows(i).Cells("ACK FATKG").Value) + (gv1.Rows(i).Cells("ACK SNF Rate").Value * gv1.Rows(i).Cells("ACK SNFKG").Value), 2)
                Gv1.Rows(i).Cells("ACK Diff Amount").Value = DispatchAmt - ACKAmt

                If clsCommon.myLen(ValidateStatus) <= 0 Then
                    gv1.Rows(i).Cells(colIsValidated).Value = True
                    ValidatedCount = ValidatedCount + 1
                    gv1.Rows(i).Cells(colErrorStatus).Style.ForeColor = Color.White
                    gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    gv1.Rows(i).Cells(colErrorStatus).Style.Font = New Font("Arial", 9, FontStyle.Regular)
                Else
                    gv1.Rows(i).Cells(colIsValidated).Value = False
                    gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    gv1.Rows(i).Cells(colErrorStatus).Style.ForeColor = Color.Blue
                    gv1.Rows(i).Cells(colErrorStatus).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                End If
            Next

            gv1.BestFitColumns()
            gv1.AutoSizeRows = True

            clsCommon.ProgressBarPercentHide()
            ValidateStatus = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Public Function SaveData() As Boolean
        Dim trans As SqlTransaction = Nothing
        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0

        Try
            If (AllowToSave()) Then
                clsCommon.ProgressBarPercentShow()

                Dim dt As Date = Nothing
                For Each grow As GridViewRowInfo In gv1.Rows
                    trans = clsDBFuncationality.GetTransactin()
                    dt = clsCommon.GetPrintDate(grow.Cells("Dispatch Date").Value, "dd/MMM/yyyy hh:mm:ss tt")
                    j = j + 1
                    clsCommon.ProgressBarPercentUpdate(j / ValidatedCount * 100, " Saving and posting Record(s) " & j & " of Total " & ValidatedCount)
                    Dim obj As New clsBulkSaleAcknowledgement()
                    obj.Document_Date = dt
                    obj.Bulk_Dispatch_Document = clsCommon.myCstr(grow.Cells("Dispatch No").Value)
                    obj.Qty = clsCommon.myCdbl(grow.Cells("ACK Qty").Value)
                    obj.FAT = clsCommon.myCdbl(grow.Cells("ACK FatPer").Value)
                    obj.FAT_KG = clsCommon.myCdbl(grow.Cells("ACK FATKG").Value)
                    obj.SNF = clsCommon.myCdbl(grow.Cells("ACK SNFPer").Value)
                    obj.SNF_KG = clsCommon.myCdbl(grow.Cells("ACK SNFKG").Value)
                    obj.FAT_Rate = clsCommon.myCdbl(grow.Cells("ACK FAT Rate").Value)
                    obj.SNF_Rate = clsCommon.myCdbl(grow.Cells("ACK SNF Rate").Value)
                    obj.Amount = clsCommon.myCdbl(grow.Cells("ACK Amount").Value)
                    obj.Diff_Amount = clsCommon.myCdbl(grow.Cells("ACK Diff Amount").Value)
                    obj.Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)

                    clsBulkSaleAcknowledgement.SaveData(obj, True, trans)
                    trans.Commit()


                Next
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                clsCommon.ProgressBarPercentHide()
                btnSave.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colSlNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        ChkBoxColumn = New GridViewCheckBoxColumn()
        ChkBoxColumn.Name = colIsValidated
        ChkBoxColumn.HeaderText = "Validated"
        ChkBoxColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

        TextCol = New GridViewTextBoxColumn()
        TextCol.Name = colErrorStatus
        TextCol.HeaderText = "Error Status"
        TextCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Insert(2, TextCol)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Customer"
        repoTextBox.Name = colCustomer
        repoTextBox.Width = 150
        repoTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoDateBox As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDateBox.Format = DateTimePickerFormat.Custom
        repoDateBox.CustomFormat = "dd/MM/yyyy"
        repoDateBox.FormatString = "{0:dd/MM/yyyy}"
        repoDateBox.HeaderText = "Dispatch Date"
        repoDateBox.Name = colDispatchDate
        repoDateBox.ReadOnly = False
        repoDateBox.IsVisible = True
        repoDateBox.Width = 100
        gv1.MasterTemplate.Columns.Add(repoDateBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Dispatch No"
        repoTextBox.Name = colDispatchNo
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Dispatch Qty"
        repoTextBox.Name = colDispatchQty
        repoTextBox.Width = 150

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Dispatch FatPer"
        repoTextBox.Name = colDispatchFatPer
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Dispatch SNFPer"
        repoTextBox.Name = colDispatchSNFPer
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoDecimalColumn As GridViewDecimalColumn
        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colDispatchFatKG
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 3
        repoDecimalColumn.HeaderText = "Dispatch FATKG"
        repoDecimalColumn.Tag = colDispatchFatKG
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colDispatchFatRate
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 2
        repoDecimalColumn.HeaderText = "Dispatch FAT Rate"
        repoDecimalColumn.Tag = colDispatchFatRate
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)


        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colDispatchSNFKG
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 3
        repoDecimalColumn.HeaderText = "Dispatch SNFKG"
        repoDecimalColumn.Tag = colDispatchSNFKG
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colDispatchSNFRate
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 2
        repoDecimalColumn.HeaderText = " Dispatch SNF Rate"
        repoDecimalColumn.Tag = colDispatchSNFRate
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)


        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colDispatchAmount
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 120
        repoDecimalColumn.DecimalPlaces = 2
        repoDecimalColumn.HeaderText = "Dispatch Amount"
        repoDecimalColumn.Tag = colDispatchAmount
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)


        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "ACK Qty"
        repoTextBox.Name = colACKQty
        repoTextBox.Width = 150

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "ACK FatPer"
        repoTextBox.Name = colACKFatPer
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "ACK SNFPer"
        repoTextBox.Name = colACKSNFPer
        repoTextBox.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoDecimalColumn1 As GridViewDecimalColumn
        repoDecimalColumn1 = New GridViewDecimalColumn()
        repoDecimalColumn1.Name = colACKFatKG
        repoDecimalColumn1.FormatString = "{0:n2}"
        repoDecimalColumn1.Width = 120
        repoDecimalColumn1.DecimalPlaces = 3
        repoDecimalColumn1.HeaderText = "ACK FATKG"
        repoDecimalColumn1.Tag = colACKFatKG
        repoDecimalColumn1.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn1)

        repoDecimalColumn1 = New GridViewDecimalColumn()
        repoDecimalColumn1.Name = colACKFatRate
        repoDecimalColumn1.FormatString = "{0:n2}"
        repoDecimalColumn1.Width = 120
        repoDecimalColumn1.DecimalPlaces = 2
        repoDecimalColumn1.HeaderText = "ACK FAT Rate"
        repoDecimalColumn1.Tag = colACKFatRate
        repoDecimalColumn1.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn1)


        repoDecimalColumn1 = New GridViewDecimalColumn()
        repoDecimalColumn1.Name = colACKSNFKG
        repoDecimalColumn1.FormatString = "{0:n2}"
        repoDecimalColumn1.Width = 120
        repoDecimalColumn1.DecimalPlaces = 3
        repoDecimalColumn1.HeaderText = "ACK SNFKG"
        repoDecimalColumn1.Tag = colACKSNFKG
        repoDecimalColumn1.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn1)

        repoDecimalColumn1 = New GridViewDecimalColumn()
        repoDecimalColumn1.Name = colACKSNFRate
        repoDecimalColumn1.FormatString = "{0:n2}"
        repoDecimalColumn1.Width = 120
        repoDecimalColumn1.DecimalPlaces = 2
        repoDecimalColumn1.HeaderText = " ACK SNF Rate"
        repoDecimalColumn1.Tag = colACKSNFRate
        repoDecimalColumn1.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn1)

        repoDecimalColumn1 = New GridViewDecimalColumn()
        repoDecimalColumn1.Name = colACKAmount
        repoDecimalColumn1.FormatString = "{0:n2}"
        repoDecimalColumn1.Width = 120
        repoDecimalColumn1.DecimalPlaces = 2
        repoDecimalColumn1.HeaderText = "ACK Amount"
        repoDecimalColumn1.Tag = colACKAmount
        repoDecimalColumn1.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn1)

        repoDecimalColumn1 = New GridViewDecimalColumn()
        repoDecimalColumn1.Name = colACKDiffAmount
        repoDecimalColumn1.FormatString = "{0:n2}"
        repoDecimalColumn1.Width = 120
        repoDecimalColumn1.DecimalPlaces = 2
        repoDecimalColumn1.HeaderText = "ACK Diff Amount"
        repoDecimalColumn1.Tag = colACKDiffAmount
        repoDecimalColumn1.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn1)

        gv1.Columns.Add("colRemarks", "Remarks")
        gv1.Columns("colRemarks").Width = 300
        gv1.Columns("colRemarks").ReadOnly = False
        gv1.Columns("colRemarks").Tag = "REM"
        gv1.Columns("colRemarks").WrapText = True

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        gv1.AllowDeleteRow = True
        ReStoreGridLayout()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        Gv1.Columns.Clear()
        Gv1.DataSource = Nothing
        btnSave.Enabled = True
        Dim arr As New List(Of String)
        arr.Add("Customer")
        arr.Add("Tanker No")
        arr.Add("Dispatch Date")
        arr.Add("Dispatch No")
        arr.Add("Dispatch Qty")
        arr.Add("Dispatch FatPer")
        arr.Add("Dispatch SNFPer")
        arr.Add("Dispatch FATKG")
        arr.Add("Dispatch FAT Rate")
        arr.Add("Dispatch SNFKG")
        arr.Add("Dispatch SNF Rate")
        arr.Add("Dispatch Amount")
        arr.Add("ACK Qty")
        arr.Add("ACK FatPer")
        arr.Add("ACK SNFPer")
        arr.Add("ACK FATKG")
        arr.Add("ACK FAT Rate")
        arr.Add("ACK SNFKG")
        arr.Add("ACK SNF Rate")
        arr.Add("ACK Amount")
        arr.Add("ACK Diff Amount")
        arr.Add("Remarks")
        For Each key As String In arrBulkProParameter.Keys
            arr.Add(key)
        Next

        If transportSql.importExcel(Gv1, arr.ToArray()) Then
            If Gv1.Columns.Count > 0 Then
                TextCol = New GridViewTextBoxColumn()
                TextCol.Name = colSlNo
                TextCol.HeaderText = "SNo"
                TextCol.ReadOnly = True
                Gv1.MasterTemplate.Columns.Insert(0, TextCol)

                ChkBoxColumn = New GridViewCheckBoxColumn()
                ChkBoxColumn.Name = colIsValidated
                ChkBoxColumn.HeaderText = "Validated"
                ChkBoxColumn.ReadOnly = True
                Gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

                TextCol = New GridViewTextBoxColumn()
                TextCol.Name = colErrorStatus
                TextCol.HeaderText = "Error Status"
                TextCol.ReadOnly = True
                Gv1.MasterTemplate.Columns.Insert(2, TextCol)

                For i As Integer = 0 To Gv1.Rows.Count - 1
                    Gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                    Gv1.Rows(i).Cells(colIsValidated).Value = False
                    ValidatedCount = 0
                    Gv1.Rows(i).Cells(colErrorStatus).Value = ""
                Next
                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).ReadOnly = True
                Next
                Gv1.AllowAddNewRow = False
                Gv1.AllowDeleteRow = True
                Gv1.EnableFiltering = True
                Gv1.EnableSorting = False
                Gv1.EnableGrouping = False
                Gv1.AllowColumnChooser = False
                Gv1.AllowColumnReorder = True
                Gv1.BestFitColumns()
                Gv1.AutoSizeRows = False
                Gv1.TableElement.TableHeaderHeight = 30
            End If
        End If

    End Sub

    Private Sub btnExportFormat_Click(sender As Object, e As EventArgs) Handles btnExportFormat.Click
        Dim whrcls As String = ""
        If clsCommon.myLen(txtCustomer.Value) > 0 Then
            whrcls = " and  Customer_Code = '" & txtCustomer.Value & "'"
        End If
        Dim ordByCls As String = " [Dispatch Date] , [Dispatch No]"
        whrcls += " and convert(date,document_date , 103 ) >= convert(date,'" & txtFromDate.Value & "',103) and  convert(date,document_date , 103 ) <= convert(date,'" & txtToDate.Value & "',103)"
        Dim qry As String = "select TSPL_Dispatch_BulkSale.Customer_Code as Customer,TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker No] ,convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as 'Dispatch Date',TSPL_Dispatch_BulkSale.Document_No as 'Dispatch No' , TSPL_Dispatch_Detail_BulkSale.Qty as 'Dispatch Qty',convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.FatPer) as 'Dispatch FatPer',
            convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNFPer) as 'Dispatch SNFPer' ,convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.Fat_KG) AS 'Dispatch FATKG',convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.FatRate) AS 'Dispatch FAT Rate', convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNF_KG) AS 'Dispatch SNFKG',convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNFRate) AS 'Dispatch SNF Rate',
            convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.Amount) AS 'Dispatch Amount' , '' as 'ACK Qty','' AS  'ACK FatPer','' AS 'ACK SNFPer' , '' AS 'ACK FATKG','' AS 'ACK FAT Rate','' AS 'ACK SNFKG', '' AS 'ACK SNF Rate' , '' AS 'ACK Amount' , '' AS 'ACK Diff Amount' ,'' AS Remarks from TSPL_Dispatch_Detail_BulkSale  
            Left Outer Join TSPL_Dispatch_BulkSale On TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No  "
        transportSql.ExporttoExcel(qry, whrcls, ordByCls, Me)

    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        CheckAndValidate()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadDataBulkSale()
    End Sub
    Sub LoadDataBulkSale()
        Try
            Dim whrcls As String = ""
            If clsCommon.myLen(txtCustomer.Value) > 0 Then
                whrcls = " And Customer_Code = '" & txtCustomer.Value & "' "
        End If
            whrcls += " and convert(date,document_date , 103 ) >= convert(date,'" & txtFromDate.Value & "',103) and  convert(date,document_date , 103 ) <= convert(date,'" & txtToDate.Value & "',103) "
            Dim qry As String = "select ROW_NUMBER() over(order by (TSPL_Dispatch_BulkSale.Document_Date)) as 'SNO.', TSPL_Dispatch_BulkSale.Customer_Code as Customer,TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker No] ,convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as 'Dispatch Date',TSPL_Dispatch_BulkSale.Document_No as 'Dispatch No' , TSPL_Dispatch_Detail_BulkSale.Qty as 'Dispatch Qty',convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.FatPer) as 'Dispatch FatPer',
            convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNFPer) as 'Dispatch SNFPer' ,convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.Fat_KG) AS 'Dispatch FATKG',convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.FatRate) AS 'Dispatch FAT Rate', convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNF_KG) AS 'Dispatch SNFKG',convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNFRate) AS 'Dispatch SNF Rate',
            convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.Amount) AS 'Dispatch Amount' , '' as 'ACK Qty','' AS  'ACK FatPer','' AS 'ACK SNFPer' , '' AS 'ACK FATKG','' AS 'ACK FAT Rate','' AS 'ACK SNFKG', '' AS 'ACK SNF Rate' , '' AS 'ACK Amount' , '' AS 'ACK Diff Amount' ,'' AS Remarks from TSPL_Dispatch_Detail_BulkSale  
            Left Outer Join TSPL_Dispatch_BulkSale On TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No  Where 1=1 " & whrcls & " order by TSPL_Dispatch_BulkSale.Document_Date,TSPL_Dispatch_BulkSale.Document_No "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = Nothing
            If dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                ChkBoxColumn = New GridViewCheckBoxColumn()
                ChkBoxColumn.Name = colIsValidated
                ChkBoxColumn.HeaderText = "Validated"
                ChkBoxColumn.ReadOnly = True
                Gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

                TextCol = New GridViewTextBoxColumn()
                TextCol.Name = colErrorStatus
                TextCol.HeaderText = "Error Status"
                TextCol.ReadOnly = True
                Gv1.MasterTemplate.Columns.Insert(2, TextCol)
                setGridFormation()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to display", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Private Sub setGridFormation()
        Gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        Gv1.EnableFiltering = True
        Gv1.ShowRowHeaderColumn = True
        For ii As Integer = 3 To Gv1.Columns.Count - 1

            Gv1.Columns(ii).Width = 150
            Gv1.Columns(ii).FormatString = "{0:F2}"
        Next
        Gv1.Columns("SNO.").Width = 40
        Gv1.Columns("Dispatch Date").Width = 100
    End Sub
End Class
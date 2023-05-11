'--------Created By Richa 27/02/2015 Against Ticket No BM00000005757
Imports common
Imports System.Data.SqlClient

Public Class FrmBulkDispatchReturnSale
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isnewentry As Boolean = False
    Public Const colSlNo As String = "colSlNo"
    Public Const colDispatchNo As String = "colDispatchNo"
    Public Const colGateEntryNo As String = "colGateEntryNo"
    Public Const colDispatchDaTe As String = "colDispatchDaTe"
    Public Const colSelect As String = "colSelect"
    Public Const colLocCode As String = "colLocCode"
    Public Const colLocDesc As String = "colLocDesc"
    Public Const colCustomerCode As String = "colCustomerCode"
    Public Const colCustomerDesc As String = "colCustomerDesc"
    Public Const colTankerNo As String = "colTankerNo"
    Public Const colQcNo As String = "colQcNo"
    Public Const colWeighmentNo As String = "colWeighmentNo"
    Public Const colbtnCol As String = "colbtnCol"
    Dim arrLoc As String = Nothing

#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'lblLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    Sub loadBlankGrid()
        Try
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Dim colChkBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            colChkBox.HeaderText = "Select "
            colChkBox.Name = colSelect
            colChkBox.ReadOnly = False
            colChkBox.Width = 50
            colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(colChkBox)

            Dim repoSLNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSLNo.FormatString = ""
            repoSLNo.HeaderText = "SL.No"
            repoSLNo.Name = colSlNo
            repoSLNo.Width = 60
            repoSLNo.ReadOnly = True
            repoSLNo.BestFit()
            Gv1.MasterTemplate.Columns.Add(repoSLNo)



            Dim repoTnkrNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTnkrNo.FormatString = ""
            repoTnkrNo.HeaderText = "Tanker No"
            repoTnkrNo.Name = colTankerNo
            repoTnkrNo.Width = 100
            repoTnkrNo.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoTnkrNo)

            Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "Dispatch No"
            repoSRNNO.Name = colDispatchNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)

            Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoSRNDate.FormatString = "{0:d}"
            repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
            repoSRNDate.HeaderText = "Dispatch Date"
            repoSRNDate.Name = colDispatchDaTe
            repoSRNDate.Width = 100
            repoSRNDate.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNDate)

            repoSRNNO = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "Gate Entry No"
            repoSRNNO.Name = colGateEntryNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)

            repoSRNNO = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "Weighment No"
            repoSRNNO.Name = colWeighmentNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)


            repoSRNNO = New GridViewTextBoxColumn()
            repoSRNNO.FormatString = ""
            repoSRNNO.HeaderText = "QC No"
            repoSRNNO.Name = colQcNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)


            Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLCode.FormatString = ""
            repoLCode.HeaderText = "Loc Code"
            repoLCode.Name = colLocCode
            repoLCode.Width = 100
            repoLCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLCode)



            Dim repoLDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoLDesc.FormatString = ""
            repoLDesc.HeaderText = "Loc Desc"
            repoLDesc.Name = colLocDesc
            repoLDesc.Width = 100
            repoLDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoLDesc)


            Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVCode.FormatString = ""
            repoVCode.HeaderText = "Customer Code"
            repoVCode.Name = colCustomerCode
            repoVCode.Width = 100
            repoVCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVCode)

            Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVDesc.FormatString = ""
            repoVDesc.HeaderText = "Customer Desc"
            repoVDesc.Name = colCustomerDesc
            repoVDesc.Width = 100
            repoVDesc.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVDesc)


            Dim RepobtnCol As GridViewCommandColumn = New GridViewCommandColumn()
            RepobtnCol.HeaderText = "Details "
            RepobtnCol.Name = colbtnCol
            RepobtnCol.ReadOnly = False
            RepobtnCol.Width = 150
            RepobtnCol.DefaultText = "Click Here..."
            RepobtnCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            Gv1.MasterTemplate.Columns.Add(RepobtnCol)

            Gv1.AllowAddNewRow = False
            Gv1.AllowColumnChooser = True
            Gv1.ShowGroupPanel = False
            Gv1.AllowColumnReorder = True
            Gv1.AllowRowReorder = True
            Gv1.EnableSorting = True
            Gv1.MasterTemplate.ShowRowHeaderColumn = False
            Gv1.MasterTemplate.ShowColumnHeaders = True
            Gv1.EnableAlternatingRowColor = True
            Gv1.TableElement.TableHeaderHeight = 20
            Gv1.EnableFiltering = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadDispatchDATA()
        Try
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
            Dim qry As String = "Select TSPL_Dispatch_BulkSale.Document_No,TSPL_Dispatch_BulkSale.Document_Date,TSPL_Quality_Check_BulkSale.GateEntry_Document_No ," & _
            " TSPL_Quality_Check_BulkSale.Weighment_No ,TSPL_Dispatch_BulkSale.QC_Code,TSPL_Dispatch_BulkSale.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            " TSPL_Dispatch_BulkSale.Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_Dispatch_BulkSale.Tanker_Code from TSPL_Dispatch_BulkSale " & _
            " Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.QC_No=TSPL_Dispatch_BulkSale.QC_Code " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Dispatch_BulkSale.Customer_Code  " & _
            " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_Dispatch_BulkSale.Location_Code " & _
            " where TSPL_Dispatch_BulkSale.Posted =1  and not exists (Select Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code =TSPL_Dispatch_BulkSale.Document_No) and TSPL_Dispatch_BulkSale.Document_Date between "
            If DateTime = "1" Then
                qry += " CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103)"
            Else
                qry += " CONVERT(date,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "',103) and CONVERT(date,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ,103)"

            End If
            qry += " and isnull(ReverseFlag,'')='N'  "

            Dim whrcls As String = ""

            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " and TSPL_Dispatch_BulkSale.Location_Code in ( " & arrLoc & ")"
            End If
            qry = qry & whrcls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                loadBlankGrid()
                For i As Integer = 0 To dt.Rows.Count - 1
                    Gv1.Rows.AddNew()
                    Gv1.Rows(i).Cells(colSelect).Value = False
                    Gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                    Gv1.Rows(i).Cells(colDispatchNo).Value = dt.Rows(i)("Document_No")
                    Gv1.Rows(i).Cells(colDispatchDaTe).Value = dt.Rows(i)("Document_Date")
                    Gv1.Rows(i).Cells(colTankerNo).Value = dt.Rows(i)("Tanker_Code")
                    Gv1.Rows(i).Cells(colLocCode).Value = dt.Rows(i)("Location_Code")
                    Gv1.Rows(i).Cells(colLocDesc).Value = dt.Rows(i)("Location_Desc")
                    Gv1.Rows(i).Cells(colCustomerCode).Value = dt.Rows(i)("Customer_Code")
                    Gv1.Rows(i).Cells(colCustomerDesc).Value = dt.Rows(i)("Customer_Name")
                    Gv1.Rows(i).Cells(colGateEntryNo).Value = dt.Rows(i)("GateEntry_Document_No")
                    Gv1.Rows(i).Cells(colWeighmentNo).Value = dt.Rows(i)("Weighment_No")
                    Gv1.Rows(i).Cells(colQcNo).Value = dt.Rows(i)("QC_Code")
                    Gv1.Rows(i).Cells(colbtnCol).Value = "Click Here..."
                Next
            Else
                clsCommon.MyMessageBoxShow("No Dispatch Found")
                btnReset.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmBulkDispatchReturnSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnReturn.Enabled Then
            ' SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Private Sub FrmBulkDispatchReturnSale_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnReturn, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBulkDispatchReturnSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Sub Reset()
        Dim dt As Date = clsCommon.GETSERVERDATE()
        dtpDispatchReturnDate.Value = dt
        dtpFromDate.Value = clsCommon.GetPrintDate(DateAdd(DateInterval.Month, -1, dt), "dd/MM/yyyy hh:mm:ss tt")
        dtpToDate.Value = dt


        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            dtpFromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpToDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpFromDate.CustomFormat = "dd/MM/yyyy"
            dtpToDate.CustomFormat = "dd/MM/yyyy"
        End If
        loadBlankGrid()
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        Try
            If dtpFromDate.Value > dtpToDate.Value Then
                Throw New Exception("'From Date' can't be larger than 'To Date'")
            End If
            LoadDispatchDATA()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub Gv1_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellClick
        If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colDispatchNo).Value) > 0 Then
            Dim frm As New FrmDispatchBulkSale
            frm.SetUserMgmt(clsUserMgtCode.FrmDispatchBulkSale)
            frm.DocumentNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDispatchNo).Value)
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
        End If
    End Sub
    Private Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckAll()
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        UnCheckAll()
    End Sub
    Sub UnCheckAll()
        If Gv1 IsNot Nothing AndAlso Gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To Gv1.ChildRows.Count - 1
                Gv1.ChildRows(i).Cells(colSelect).Value = False
            Next
        End If
    End Sub
    Sub CheckAll()
        If Gv1 IsNot Nothing AndAlso Gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To Gv1.ChildRows.Count - 1
                Gv1.ChildRows(i).Cells(colSelect).Value = True
            Next
        End If
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        If AllowToSave() Then
            SaveAndPostData()
        End If
    End Sub
    Sub SaveAndPostData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
        Dim obj As ClsDispatchBulkSale = Nothing
        Dim isSaved As Boolean = False
        Try
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    obj = New ClsDispatchBulkSale()
                   
                    obj.Document_No = Gv1.Rows(i).Cells(colDispatchNo).Value
                    obj = ClsDispatchBulkSale.GetData(obj.Document_No, "", NavigatorType.Current, trans)
                    obj.ReverseFlag = "Y"
                    ''Sanjeet (To Check Locked Data)
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Bulk Sale", "Bulk Dispatch Return", obj.Location_Code, clsCommon.myCDate(Gv1.Rows(i).Cells(colDispatchDaTe).Value), trans)

                    If obj IsNot Nothing Then
                        ClsDispatchBulkSale.updateJournalEntry("DS-BS", obj.Document_No, 0, trans)
                        '' Delete data from inventory movement 
                        clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT_NEW  where Source_Doc_No ='" & obj.Document_No & "' and Trans_Type  ='DispatchBS' ", trans)
                        isSaved = ClsDispatchBulkSale.SaveDataHistory(obj, True, trans)
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_Dispatch_BulkSale set Posted=0,ReverseFlag='Y',Modified_By='" & objCommonVar.CurrentUserCode & "' ,Modified_Date ='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Document_No ='" & obj.Document_No & "'", trans)
                    End If
                End If
            Next
            If isSaved Then
                trans.Commit()
                clsCommon.MyMessageBoxShow("Saved Successfully.")
                btnReset.PerformClick()
            Else
                trans.Rollback()
                clsCommon.MyMessageBoxShow("Could not Saved.")
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    'Public Shared Function updateJournalEntry(ByVal Source_type As String, ByVal Doc_No As String, ByVal amount As Double, ByVal trans As SqlTransaction)
    '    Dim sQuery As String = String.Empty
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_journal_master where source_code='" & Source_type & "' and source_doc_no='" & Doc_No & "'", trans)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
    '        sQuery = "update tspl_journal_master set total_debit_amt=" & amount & ",total_credit_amt=" & amount & ",modify_by='" & objCommonVar.CurrentUserCode & "',modify_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "',posting_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss") & "' where voucher_No='" & clsCommon.myCstr(dt.Rows(0).Item("voucher_No")) & "'"
    '        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '        sQuery = "update tspl_journal_details set amount=case when coalesce(amount,0)<0 then -1 else 1 end*" & amount & " where voucher_No='" & clsCommon.myCstr(dt.Rows(0).Item("voucher_No")) & "'"
    '        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
    '    End If
    'End Function
    Function AllowToSave() As Boolean
        Dim isSaved As Boolean = False
        Try
            ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
            If AllowFutureDateTransaction(dtpDispatchReturnDate.Value, Nothing) = False Then

                dtpDispatchReturnDate.Select()
                Return False
            End If

            If dtpFromDate.Value > dtpToDate.Value Then
                Throw New Exception("'From Date' can't be larger than 'To Date'")
            End If
            If Gv1 Is Nothing OrElse Gv1.Rows.Count = 0 Then
                Throw New Exception("No SRN Found")
            End If
            Dim c As Integer = 0
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    c = c + 1
                End If
            Next
            If c = 0 Then
                Throw New Exception("Please select at least  one Dispatch to return")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub
End Class

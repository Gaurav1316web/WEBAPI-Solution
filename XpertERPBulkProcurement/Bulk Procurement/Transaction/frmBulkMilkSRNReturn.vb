Imports common
Imports System.Data.SqlClient
Public Class FrmBulkMilkSRNReturn
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isnewentry As Boolean = False
    Public Const colSlNo As String = "colSlNo"
    Public Const colSRNNo As String = "colSRNNo"
    Public Const colGateEntryNo As String = "colGateEntryNo"
    Public Const colSRNDATe As String = "colSRNDATe"
    Public Const colSelect As String = "colSelect"
    Public Const colLocCode As String = "colLocCode"
    Public Const colLocDesc As String = "colLocDesc"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorDesc As String = "colVendorDesc"
    Public Const colTankerNo As String = "colTankerNo"
    Public Const colQcNo As String = "colQcNo"
    Public Const colWeighmentNo As String = "colWeighmentNo"
    Public Const colbtnCol As String = "colbtnCol"


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBulkMilkSRNReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
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
            repoSRNNO.HeaderText = "SRN No"
            repoSRNNO.Name = colSRNNo
            repoSRNNO.Width = 100
            repoSRNNO.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoSRNNO)

            Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoSRNDate.FormatString = "{0:d}"
            repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
            repoSRNDate.HeaderText = "SRN Date"
            repoSRNDate.Name = colSRNDATe
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
            repoVCode.HeaderText = "Vendor Code"
            repoVCode.Name = colVendorCode
            repoVCode.Width = 100
            repoVCode.ReadOnly = True
            Gv1.MasterTemplate.Columns.Add(repoVCode)

            Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVDesc.FormatString = ""
            repoVDesc.HeaderText = "Vendor Desc"
            repoVDesc.Name = colVendorDesc
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadSRNDATA()
        Try
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
            Dim qry As String = " select tspl_bulk_milk_srn.SRN_NO,tspl_bulk_milk_srn.SRN_DATE,	tspl_bulk_milk_srn.GATE_ENTRY_NO,tspl_bulk_milk_srn.WEIGHMENT_NO,tspl_bulk_milk_srn.QC_NO,tspl_bulk_milk_srn.VENDOR_CODE,tspl_vendor_master.VENDOR_NAME,tspl_bulk_milk_srn.LOC_CODE,tspl_location_master.LOCATION_DESC,tspl_bulk_milk_srn.TANKER_NO from tspl_bulk_milk_srn LEFT OUTER join tspl_vendor_master on tspl_vendor_master.vendor_code=tspl_bulk_milk_srn.VENDOR_CODE lEFT OUTER join tspl_location_master on tspl_location_master.location_code=tspl_bulk_milk_srn.loc_code where isPosted=1 and srn_no not in (select srn_no from tspl_bulk_milk_purchase_invoice_detail ) and tspl_bulk_milk_srn.SRN_Date between "
            If DateTime = "1" Then
                qry += " CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103)"
            Else
                qry += " CONVERT(date,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "',103) and CONVERT(date,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ,103)"
            End If
            qry += " and isnull(srn_return_no,'')='' " & _
            " and  tspl_bulk_milk_srn.FormType ='BulkMilkSRN' "
            Dim whrcls As String = ""
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrcls = " and TSPL_Bulk_MILK_SRN.Loc_Code in ( " & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qry = qry & whrcls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                loadBlankGrid()
                For i As Integer = 0 To dt.Rows.Count - 1
                    Gv1.Rows.AddNew()
                    Gv1.Rows(i).Cells(colSelect).Value = False
                    Gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                    Gv1.Rows(i).Cells(colSRNNo).Value = dt.Rows(i)("SRN_NO")
                    Gv1.Rows(i).Cells(colSRNDATe).Value = dt.Rows(i)("SRN_DATE")
                    Gv1.Rows(i).Cells(colTankerNo).Value = dt.Rows(i)("TANKER_NO")
                    Gv1.Rows(i).Cells(colLocCode).Value = dt.Rows(i)("LOC_CODE")
                    Gv1.Rows(i).Cells(colLocDesc).Value = dt.Rows(i)("LOCATION_DESC")
                    Gv1.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("VENDOR_CODE")
                    Gv1.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("VENDOR_NAME")
                    Gv1.Rows(i).Cells(colGateEntryNo).Value = dt.Rows(i)("GATE_ENTRY_NO")
                    Gv1.Rows(i).Cells(colWeighmentNo).Value = dt.Rows(i)("WEIGHMENT_NO")
                    Gv1.Rows(i).Cells(colQcNo).Value = dt.Rows(i)("QC_NO")
                    Gv1.Rows(i).Cells(colbtnCol).Value = "Click Here..."
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No SRN Found", Me.Text)
                btnReset.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        Dim dt As Date = clsCommon.GETSERVERDATE()
        dtpDate.Value = dt
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

    Function AllowToSave() As Boolean
        Dim isSaved As Boolean = False

        Try
           
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
                Throw New Exception("Please select at least  one SRN to return")
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        Try
            '= KUNAL > TICKET : BM00000009575 =====
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                dtpDate.Focus()
                Return False
            End If

        Catch ex1 As Exception
            clsCommon.MyMessageBoxShow(Me, ex1.Message, Me.Text)
        End Try
        Return True
    End Function
    Private Sub FrmBulkMilkSRNReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnReturn.Enabled AndAlso MyBase.isModifyFlag Then
            btnReturn.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose.PerformClick()
        End If
    End Sub
    Private Sub FrmBulkMilkSRNReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnReturn, "Press Alt+S for Save & Post ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for Reset ")
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        Try
            If dtpFromDate.Value > dtpToDate.Value Then
                Throw New Exception("'From Date' can't be larger than 'To Date'")
            End If
            LoadSRNDATA()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub Gv1_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellClick
        If Gv1.Rows.Count > 0 AndAlso e.RowIndex >= 0 AndAlso e.Column Is Gv1.Columns(colbtnCol) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells(colSRNNo).Value) > 0 Then
            Dim frm As New FrmBulkMilkSRN
            frm.SetUserMgmt(clsUserMgtCode.frmBulkMilkSRN)
            frm.DocumentNo = clsCommon.myCstr(Gv1.CurrentRow.Cells(colSRNNo).Value)
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        If AllowToSave() Then
            SaveAndPostData()
        End If
    End Sub
    Sub SaveAndPostData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
        Dim obj As clsBulkMilkSRNReturn = Nothing
        Dim isSaved As Boolean = False
        Try
            For i As Integer = 0 To Gv1.Rows.Count - 1
                If Gv1.Rows(i).Cells(colSelect).Value = True Then
                    obj = New clsBulkMilkSRNReturn()
                    obj.SRN_Return_NO = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy"), clsDocType.BulkMilkSRNReturn, "", Gv1.Rows(i).Cells(colLocCode).Value)
                    If clsCommon.myLen(obj.SRN_Return_NO) <= 0 Then
                        Throw New Exception("Error In SRN Return No Genertion")
                    End If
                    obj.SRN_NO = Gv1.Rows(i).Cells(colSRNNo).Value
                    obj.SRN_Return_Date = clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy")
                    isSaved = clsBulkMilkSRNReturn.saveData(obj, trans)
                End If
            Next
            If isSaved Then
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Saved Successfully.", Me.Text)
                btnReset.PerformClick()
            Else
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, "Could not Saved.", Me.Text)
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
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
End Class

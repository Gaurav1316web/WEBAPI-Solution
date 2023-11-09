Imports common
Public Class FrmBankRecoHide
    Public strRecoID As String
    Public strBankCode As String
    Public strBankName As String

    Private Sub FrmBankRecoHide_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblRecoID.Text = strRecoID
            lblBankCode.Text = strBankCode
            lblBankName.Text = strBankName
            Dim qry As String = "select cast( is_Hide as bit) as Sel,cast( is_Hide as bit) as is_Hide, Document_No,Document_Date,Document_Type,Entry_Type,Payment_Code_reco,Cheque_No,Cheque_Date,Description,Customer_Name,Withdrawal,Deposit from tspl_bankreco_Detail where Reconciliation_Id='" + strRecoID + "' and Reconciliation_Status='O' and  Document_Type<>'BankTransfer' " + Environment.NewLine + _
            " and not exists(select 1 from tspl_bankreco_Detail as inn where inn.Document_No=tspl_bankreco_Detail.Document_No and inn.Document_Type=tspl_bankreco_Detail.Document_Type  and Reconciliation_Status='C') order by Document_Date "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No data found")
            End If
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.DataSource = dt

            gv1.Columns("Sel").HeaderText = "Select"
            gv1.Columns("is_Hide").HeaderText = "Hide"
            gv1.Columns("is_Hide").IsVisible = False
            gv1.Columns("Document_No").HeaderText = "DocumentNo"
            gv1.Columns("Document_Date").HeaderText = "DocumentDate"
            gv1.Columns("Document_Type").HeaderText = "DocumentType"
            gv1.Columns("Entry_Type").HeaderText = "EntryType"
            gv1.Columns("Payment_Code_reco").HeaderText = "PaymentCodeReco"
            gv1.Columns("Cheque_No").HeaderText = "ChequeNo"
            gv1.Columns("Cheque_Date").HeaderText = "ChequeDate"
            gv1.Columns("Description").HeaderText = "Description"
            gv1.Columns("Customer_Name").HeaderText = "CustomerName"
            gv1.Columns("Withdrawal").HeaderText = "Withdrawal"
            gv1.Columns("Deposit").HeaderText = "Deposit"

            gv1.AllowDeleteRow = False
            gv1.AllowAddNewRow = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = True
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
            gv1.BestFitColumns()

            For ii As Integer = 1 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Me.Close()
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            Dim Arr As New List(Of clsBankRecoDetails)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value) <> clsCommon.myCBool(gv1.Rows(ii).Cells("is_Hide").Value) Then
                    Dim obj As New clsBankRecoDetails
                    obj.Document_No = clsCommon.myCstr(gv1.Rows(ii).Cells("Document_No").Value)
                    obj.Document_Type = clsCommon.myCstr(gv1.Rows(ii).Cells("Document_Type").Value)
                    obj.Entry_Type = clsCommon.myCstr(gv1.Rows(ii).Cells("Entry_Type").Value)
                    obj.Deposit = clsCommon.myCdbl(gv1.Rows(ii).Cells("Deposit").Value)
                    obj.Withdrawal = clsCommon.myCdbl(gv1.Rows(ii).Cells("Withdrawal").Value)
                    obj.is_Hide = clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value)
                    Arr.Add(obj)
                End If
            Next
            If Arr.Count <= 0 Then
                Throw New Exception("Please Hide/Unhide at least one transaction")
            End If
            clsBankReco.SetHideEntry(Arr)
            clsCommon.MyMessageBoxShow(Me, "Successfully applied", Me.Text)
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            clsCommon.MyExportToExcelGrid("", gv1, Nothing, Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    
    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Dim gv As New RadGridView
        Me.Controls.Add(gv)
        Dim SNO As Integer = 0
        Try
            Dim Strs As List(Of String) = New List(Of String)
            For jj As Integer = 0 To gv1.Columns.Count - 1
                If gv1.Columns(jj).IsVisible Then
                    Strs.Add(gv1.Columns(jj).HeaderText)
                End If
            Next
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each gvRow As GridViewRowInfo In gv.Rows
                        SNO += 1
                        clsCommon.ProgressBarPercentUpdate((gvRow.Index + 1) * 100 / (gv.Rows.Count + 1), "Importing  : " & (gvRow.Index + 1) & "/" & gv.Rows.Count & "")
                        Dim strDocNo As String = clsCommon.myCstr(gvRow.Cells(gv1.Columns("Document_No").HeaderText).Value)
                        Dim val As Boolean = clsCommon.CompairString("TRUE", clsCommon.myCstr(gvRow.Cells(gv1.Columns("Sel").HeaderText).Value)) = CompairStringResult.Equal
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.CompairString(strDocNo, clsCommon.myCstr(gv1.Rows(jj).Cells("Document_No").Value)) = CompairStringResult.Equal Then
                                gv1.Rows(jj).Cells("Sel").Value = val
                            End If
                        Next
                    Next
                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data imported successfully", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        ApplyONOffAll(True)
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        ApplyONOffAll(False)
    End Sub

    Sub ApplyONOffAll(ByVal val As Boolean)
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                gv1.Rows(ii).Cells("Sel").Value = val
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        Try
            Dim ArrHide As New ArrayList
            Dim ArrUnHide As New ArrayList
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value) <> clsCommon.myCBool(gv1.Rows(ii).Cells("is_Hide").Value) Then
                    If clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value) Then
                        ArrHide.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("Document_No").Value))
                    Else
                        ArrUnHide.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("Document_No").Value))
                    End If
                End If
            Next
            Dim isOK As Boolean = False
            If ArrHide IsNot Nothing AndAlso ArrHide.Count > 0 Then
                isOK = True
                Dim qry As String = clsBankReco.VerifyAllReco(ArrHide, "", True)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frm As New FrmFreeGrid
                    frm.dt = dt
                    frm.arr = ArrHide
                    frm.strFormName = "Mismatch bank reco Unhide to hide"
                    frm.ReportID = "FrmBankRecoHide"
                    frm.WindowState = FormWindowState.Maximized
                    frm.ShowDialog()
                    Exit Sub
                End If
            End If
            If ArrUnHide IsNot Nothing AndAlso ArrUnHide.Count > 0 Then
                isOK = True
                Dim qry As String = clsBankReco.VerifyAllReco(ArrUnHide, "", True)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frm As New FrmFreeGrid
                    frm.dt = dt
                    frm.arr = ArrUnHide
                    frm.strFormName = "Mismatch bank reco hide to Unhide "
                    frm.ReportID = "FrmBankRecoUnHide"
                    frm.WindowState = FormWindowState.Maximized
                    frm.ShowDialog()
                    Exit Sub
                End If
            End If
            If isOK Then
                clsCommon.MyMessageBoxShow("Verification is ok", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

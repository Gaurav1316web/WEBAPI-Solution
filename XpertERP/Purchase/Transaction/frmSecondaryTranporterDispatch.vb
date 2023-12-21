Imports common
Imports System.IO

Public Class frmSecondaryTranporterDispatch
#Region "Variables"
    Public strDeductionCode As String = ""
    Public strDeductionName As String = ""
    Public strVendorCode As String = ""
    Public strVendorName As String = ""
    Public strSegmentCode As String = ""
    Public strSegmentName As String = ""
    Public dtFrom As Date
    Public dtTo As Date
    Public dblAMT As Double

    Const ColSNo As String = "COLSNO"
    Const colDispatchNo As String = "COLBATCHNO"
    Const colAMT As String = "COLQTY"

    Public arr As List(Of clsAPSecondaryTranporterDeductionDetail) = Nothing
    Const ReportID As String = "STDisChaln"
    Public isCencelButtonClicked As Boolean = False
    Public isInsideLoadData As Boolean = False
    Public isCellValueChangedOpen As Boolean = False

#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isInsideLoadData = True
        lblIDeductionCode.Text = strDeductionCode
        lblDeductionName.Text = strDeductionName
        lblVendorCode.Text = strVendorCode
        lblVendorName.Text = strVendorName
        lblLocationSegmentCode.Text = strSegmentCode
        lblLocationSegmentName.Text = strSegmentName
        lblFromDate.Text = clsCommon.GetPrintDate(dtFrom, "dd/MM/yyyy")
        lblToDate.Text = clsCommon.GetPrintDate(dtTo, "dd/MM/yyyy")
        lblAmt.Text = clsCommon.myFormat(dblAMT)
        LoadBlankGrid()
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            Dim ii As Integer = 1
            For Each obj As clsAPSecondaryTranporterDeductionDetail In arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = ii
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchNo).Value = obj.DC_Challan_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAMT).Value = obj.Amount
                ii += 1
            Next
        End If
        gv1.Rows.AddNew()
        RefeshSNO()
        If gv1.RowCount > 0 Then
            gv1.CurrentRow = gv1.Rows(0)
            gv1.CurrentColumn = gv1.Columns(colDispatchNo)
        End If
        gv1.Focus()
        isInsideLoadData = False
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SrNo"
        repoLineNo.Name = ColSNo
        repoLineNo.Width = 20
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.FormatString = ""
        repoLocationName.HeaderText = "Dispatch No"
        repoLocationName.Name = colDispatchNo
        repoLocationName.ReadOnly = False
        repoLocationName.IsVisible = True
        repoLocationName.Width = 150
        gv1.MasterTemplate.Columns.Add(repoLocationName)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:n2}"
        repoQty.HeaderText = "Amount"
        repoQty.Name = colAMT
        repoQty.ReadOnly = False
        repoQty.IsVisible = True
        repoQty.Width = 80
        gv1.MasterTemplate.Columns.Add(repoQty)

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

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        OKPressed()
    End Sub

    Sub OKPressed()
        Try
            If AllowToSave() Then
                arr = New List(Of clsAPSecondaryTranporterDeductionDetail)
                For ii As Integer = 0 To gv1.RowCount - 1
                    Dim obj As clsAPSecondaryTranporterDeductionDetail = New clsAPSecondaryTranporterDeductionDetail()
                    obj.DC_Challan_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDispatchNo).Value)
                    obj.Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAMT).Value)
                    If clsCommon.myLen(obj.DC_Challan_No) > 0 AndAlso obj.Amount <> 0 Then
                        arr.Add(obj)
                    End If
                Next
                Me.Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Dim dblTotAmt As Double = 0
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colDispatchNo).Value) > 0 Then
                dblTotAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colAMT).Value)
            End If
        Next
        If clsCommon.myCdbl(lblAmt.Text) <> dblTotAmt Then
            Throw New Exception("Total Amount " + lblAmt.Text + Environment.NewLine + "But Dispatch deduction amount" + clsCommon.myCstr(dblTotAmt) + " Both sould be match")
        End If
        Return True
    End Function

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            OKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            CancelPressed()
        End If
    End Sub

    Sub CancelPressed()
        isCencelButtonClicked = True
        Me.Close()
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        RefeshSNO()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If gv1.RowCount <= clsCommon.myCdbl(lblAmt.Text) Then
            e.Cancel = True
            Exit Sub
        End If

        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        CancelPressed()
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colDispatchNo) Then
                        Dim qry As String = "select TSPL_MCC_Dispatch_Challan.Chalan_NO as Code,convert(varchar, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Date ,TSPL_MCC_Dispatch_Challan.MCC_Code,TSPL_MCC_Dispatch_Challan.MCC_Name,TSPL_MCC_Dispatch_Challan.Tare_Weight,TSPL_MCC_Dispatch_Challan.Gross_Weight,TSPL_MCC_Dispatch_Challan.Net_Qty,TSPL_MCC_Dispatch_Challan.control_sample_fat,TSPL_MCC_Dispatch_Challan.control_sample_snf,TSPL_MCC_Dispatch_Challan.Payment_Amount,TSPL_MCC_Dispatch_Challan.Tanker_No,TSPL_TANKER_MASTER.Tanker_Transporter_Code as TransporterCode,TSPL_TANKER_MASTER.Description as TansporterName" + Environment.NewLine + _
                        " from TSPL_MCC_Dispatch_Challan " + Environment.NewLine + _
                        " left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MCC_Dispatch_Challan.Tanker_No" + Environment.NewLine + _
                        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_Dispatch_Challan.MCC_Code"
                        Dim whrclas As String = " TSPL_MCC_Dispatch_Challan.isPosted=1 and TSPL_TANKER_MASTER.Tanker_Transporter_Code='" + strVendorCode + "' and TSPL_MCC_Dispatch_Challan.Dispatch_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MCC_Dispatch_Challan.Dispatch_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm tt") + "' and  Loc_Segment_Code='" + lblLocationSegmentCode.Text + "'"
                        gv1.CurrentRow.Cells(colDispatchNo).Value = clsCommon.ShowSelectForm("STDMAPDisno", qry, "Code", whrclas, clsCommon.myCstr(gv1.CurrentRow.Cells(colDispatchNo).Value), "", False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

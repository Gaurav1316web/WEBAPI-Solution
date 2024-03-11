Imports common
Imports System.IO
Imports XpertERPEngine

Public Class FrmCreateRemittance
    Inherits FrmMainTranScreen

    Const ReportID As String = "CreateRemittance"
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnCreateRemittance)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")

        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCreateRemittance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso MyBase.isDeleteFlag Then
            Close()
        End If
    End Sub

    Private Sub FrmCreateRemittance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        cboFilterBy.SelectedIndex = 0
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()

    End Sub

    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CREAT_REMIT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            'rdbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        isInsideLoadData = True
        RefreshData()
        UpdateTotals(Nothing)
        isInsideLoadData = False
    End Sub

    Private Sub RefreshData()
        If clsCommon.myLen(txtSection.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Section", Me.Text)
            Exit Sub
        End If
        'If clsCommon.myLen(txtBranch.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select Branch", Me.Text)
        '    Exit Sub
        'End If
        Dim dt As DataTable = clsRemittance.GetDataForCreateRemittance(cboFilterBy.Text, txtFrom.Value, txtTo.Value, txtFromDate.Value, txtToDate.Value, txtBranch.Value, txtSection.Value, rbtnCompany.IsChecked)
        'dt.Columns.Add("AllRemitCode", GetType(String))

        gv1.DataSource = dt
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).Width = 100
        Next
        gv1.Columns("Post").ReadOnly = False
        gv1.Columns("Post").Width = 40
        gv1.Columns("AllRemitCode").IsVisible = False

        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells("Document No").Value)
            Dim qry As String = "select Remittance_Code  from Tspl_remittance where Document_No in(Select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocNo='" + strCode + "')"
            Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim isFirstTime As Boolean = True
            If dtNew IsNot Nothing AndAlso dtNew.Rows.Count > 0 Then
                For Each dr As DataRow In dtNew.Rows
                    If Not isFirstTime Then
                        gv1.Rows(ii).Cells("AllRemitCode").Value += ","
                    End If



                    Try
                        ' gv1.Rows(ii).Cells("AllRemitCode").Value = "'dads'"
                        gv1.Rows(ii).Cells("AllRemitCode").Value += "'" + clsCommon.myCstr(dr("Remittance_Code")) + "'"
                    Catch ex As Exception

                    End Try
                    isFirstTime = False
                Next
            End If
        Next
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
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim strDrNodeAgainstAP As String = ""
            Dim arr As New List(Of String)
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If (clsCommon.myCdbl(gv1.Rows(ii).Cells("Post").Value)) Then
                        arr.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value))
                        If clsCommon.myLen(gv1.Rows(ii).Cells("AllRemitCode").Value) > 0 Then
                            strDrNodeAgainstAP += "," + clsCommon.myCstr(gv1.Rows(ii).Cells("AllRemitCode").Value)
                        End If
                    End If
                Next
            End If
            If (arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Remit is selected to Post", Me.Text)
                Return
            End If

            If (common.clsCommon.MyMessageBoxShow("Post the selected " + clsCommon.myCstr(arr.Count) + " Remit.Are you sure ? ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                If (clsRemittance.PostRemit(arr, strDrNodeAgainstAP)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Remit Posted successfully", Me.Text)
                    RefreshData()
                    UpdateTotals(Nothing)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnRemittAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemittAll.Click
        SetCheckButtonOfGrid(True)
        UpdateTotals(Nothing)
    End Sub

    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        SetCheckButtonOfGrid(False)
        UpdateTotals(Nothing)
    End Sub

    Private Sub SetCheckButtonOfGrid(ByVal val As Boolean)
        If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                gv1.Rows(ii).Cells("Post").Value = val
            Next
        End If
    End Sub

    Private Sub gv1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.SelectionChanged
        If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 AndAlso gv1.CurrentRow IsNot Nothing Then
            lblRDTDSAmt.Text = clsCommon.myFormat(gv1.CurrentRow.Cells("Actual TDS").Value)
            lblRDSurcharge.Text = clsCommon.myFormat(gv1.CurrentRow.Cells("Actual Surcharge").Value)
            lblRDEduCess.Text = clsCommon.myFormat(gv1.CurrentRow.Cells("Actual Edu Cess").Value)
            lblRDSecEduCess.Text = clsCommon.myFormat(gv1.CurrentRow.Cells("Actual Sec Educess").Value)
            lblRDTotal.Text = clsCommon.myFormat(gv1.CurrentRow.Cells("Actual Total TDS").Value)
        End If
    End Sub

    Private Sub gv1_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gv1.ValueChanging
        If Not isInsideLoadData Then
            UpdateTotals(e)
        End If
    End Sub

    Private Sub UpdateTotals(ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs)
        If Not isInsideLoadData Then
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                Dim dblTotTDSAmt As Double = 0
                Dim dblTotSurhcargeAmt As Double = 0
                Dim dblTotEduCessAmt As Double = 0
                Dim dblTotSecEduCessAmt As Double = 0
                Dim dblTotAmt As Double = 0
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If ((clsCommon.myCdbl(gv1.Rows(ii).Cells("Post").Value)) OrElse (e IsNot Nothing AndAlso gv1.CurrentRow.Index = ii AndAlso e.NewValue = True)) Then
                        dblTotTDSAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells("Actual TDS").Value)
                        dblTotSurhcargeAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells("Actual Surcharge").Value)
                        dblTotEduCessAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells("Actual Edu Cess").Value)
                        dblTotSecEduCessAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells("Actual Sec Educess").Value)
                        dblTotAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells("Actual Total TDS").Value)
                    End If
                Next
                lblRSTDSAmt.Text = clsCommon.myFormat(dblTotTDSAmt)
                lblRSSurcharge.Text = clsCommon.myFormat(dblTotSurhcargeAmt)
                lblRSEduCess.Text = clsCommon.myFormat(dblTotEduCessAmt)
                lblRSSecEduCess.Text = clsCommon.myFormat(dblTotSecEduCessAmt)
                lblRSTotal.Text = clsCommon.myFormat(dblTotAmt)
            End If
        End If
    End Sub

    Private Sub cboFilterBy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboFilterBy.SelectedIndexChanged
        txtFrom.Value = ""
        txtTo.Value = ""
        pnlDateRange.Visible = False
        pnlFinderRange.Visible = True
        If clsCommon.CompairString("Document Date", cboFilterBy.Text) = CompairStringResult.Equal Then
            pnlDateRange.Visible = True
            pnlFinderRange.Visible = False
        End If
    End Sub

    Private Sub txtFrom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFrom._MYValidating
        Try
            '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
            If clsCommon.CompairString("Vendor", cboFilterBy.Text) = CompairStringResult.Equal Then
                Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name] from TSPL_VENDOR_MASTER"
                txtFrom.Value = clsCommon.ShowSelectForm("CRVendor", qry, "Code", "", txtFrom.Value, "Code", isButtonClicked)
            ElseIf clsCommon.CompairString("Document No", cboFilterBy.Text) = CompairStringResult.Equal Then
                Dim qry As String = "select distinct(Document_No) as Docuemnt from TSPL_REMITTANCE"
                txtFrom.Value = clsCommon.ShowSelectForm("CRDocumentNo", qry, "Docuemnt", "Remit_TDS='N'", txtFrom.Value, "Docuemnt", isButtonClicked)
            ElseIf clsCommon.CompairString("Nature Of Deduction", cboFilterBy.Text) = CompairStringResult.Equal Then
                Dim qry As String = "select Deduction_Code as Code,Description from TSPL_TDS_DEDUCTION_HEAD"
                Dim whrCls As String = ""
                If clsCommon.myLen(txtSection.Value) Then
                    whrCls = "TDS_Section='" + txtSection.Value + "'"
                End If
                txtFrom.Value = clsCommon.ShowSelectForm("CRNatureOfDeduction", qry, "Code", whrCls, txtFrom.Value, "Code", isButtonClicked)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTo._MYValidating
        Try
            '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
            If clsCommon.CompairString("Vendor", cboFilterBy.Text) = CompairStringResult.Equal Then
                Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name] from TSPL_VENDOR_MASTER"
                txtTo.Value = clsCommon.ShowSelectForm("CRVendor", qry, "Code", "", txtTo.Value, "Code", isButtonClicked)
            ElseIf clsCommon.CompairString("Document No", cboFilterBy.Text) = CompairStringResult.Equal Then
                Dim qry As String = "select distinct(Document_No) as Docuemnt from TSPL_REMITTANCE"
                txtTo.Value = clsCommon.ShowSelectForm("CRDocumentNo", qry, "Docuemnt", "Remit_TDS='N'", txtTo.Value, "Docuemnt", isButtonClicked)
            ElseIf clsCommon.CompairString("Nature Of Deduction", cboFilterBy.Text) = CompairStringResult.Equal Then
                Dim qry As String = "select Deduction_Code as Code,Description from TSPL_TDS_DEDUCTION_HEAD"
                Dim whrCls As String = ""
                If clsCommon.myLen(txtSection.Value) Then
                    whrCls = "TDS_Section='" + txtSection.Value + "'"
                End If
                txtTo.Value = clsCommon.ShowSelectForm("CRNatureOfDeduction", qry, "Code", whrCls, txtTo.Value, "Code", isButtonClicked)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtSection__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSection._MYValidating
        Dim qry As String = "select TDS_Group as Code ,Description from TSPL_TDS_SECTION_MASTER"
        txtSection.Value = clsCommon.ShowSelectForm("CreRemSectionFinder", qry, "Code", "", txtSection.Value, "", isButtonClicked)
        qry = "select Description from TSPL_TDS_SECTION_MASTER where TDS_Group ='" + txtSection.Value + "'"
        lblSectionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub

    Private Sub txtBranch__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBranch._MYValidating
        Dim qry As String = "select Branch_Code as Code,Branch_Name as Name from TSPL_TDS_BRANCH_MASTER"
        txtBranch.Value = clsCommon.ShowSelectForm("CreRemBranchFinder", qry, "Code", "", txtBranch.Value, "", isButtonClicked)
        qry = "select Branch_Name from TSPL_TDS_BRANCH_MASTER where Branch_Code ='" + txtBranch.Value + "'"
        lblBranchName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub

    Private Sub gv1_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        ''If Font Is Nothing Then
        ''    Font = New Font(e.RowElement.Font, FontStyle.Bold)
        ''End If
        If clsCommon.myLen(e.RowElement.RowInfo.Cells("AllRemitCode").Value) > 0 Then
            'e.RowElement.Font = Font
            'e.RowElement.BackColor = Color.Blue
            e.RowElement.ForeColor = Color.Blue
            'e.RowElement.Font = New Font(e.RowElement.Font, FontStyle.Bold)
            'e.RowElement.DrawFill = True
        Else
            e.RowElement.ForeColor = Color.Black
            'e.RowElement.Font = New Font(e.RowElement.Font, FontStyle.Regular)
            'e.RowElement.Font.Bold = False
            ''e.RowElement.ResetValue(LightVisualElement.FontProperty, ValueResetFlags.Local)
            ''e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            ''e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Dim strAllRemitCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("AllRemitCode").Value)
        If clsCommon.myLen(gv1.CurrentRow.Cells("AllRemitCode").Value) > 0 Then
            Dim frm As FrmShowAllDrNote = New FrmShowAllDrNote()
            frm.strAllRemitcode = strAllRemitCode
            frm.ShowDialog()
        End If
    End Sub

    Private Sub mbtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj = Nothing
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub mbtnDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnDeleteLayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
End Class
